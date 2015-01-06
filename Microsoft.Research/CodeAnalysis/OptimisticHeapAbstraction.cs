// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using Microsoft.Research.DataStructures;


namespace Microsoft.Research.CodeAnalysis
{

  using Temp = System.Int32; // Stack locations
  using Dest = System.Int32; // Stack location
  using Source = System.Int32; // Stack location
  using SubroutineContext = FList<Microsoft.Research.DataStructures.STuple<CFGBlock, CFGBlock, string>>;
  using SubroutineEdge = Microsoft.Research.DataStructures.STuple<CFGBlock, CFGBlock, string>;
  using EdgeData = IFunctionalMap<SymbolicValue, FList<SymbolicValue>>;
  using System.Diagnostics.Contracts;
  using System.Diagnostics.CodeAnalysis;

  static class Extensions
  {
    /// <summary>
    /// Returns true if the type is represented with "ValueOf" to get the contents, i.e.,
    /// it is not a struct with fields.
    /// </summary>
    public static bool HasValueRepresentation<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, Type type)
    {
      return !mdDecoder.IsStruct(type) || mdDecoder.IsPrimitive(type) || mdDecoder.IsEnum(type);
    }

    public static string ToString<T>(this IEnumerable<T> data, string separator)
    {
      var sb = new StringBuilder();
      bool notFirst = false;
      foreach (var elem in data)
      {
        if (notFirst)
        {
          sb.Append(separator);
        }
        sb.Append(elem.ToString());
        notFirst = true;
      }
      return sb.ToString();
    }
  }

  public struct SymbolicValue : IEquatable<SymbolicValue>, IComparable<SymbolicValue>, IComparable
  {
    internal readonly ESymValue symbol;

    public static int GetUniqueKey(SymbolicValue v) { return ESymValue.GetUniqueKey(v.symbol); }
    public static int Compare(SymbolicValue left, SymbolicValue right) { return left.symbol.UniqueId - right.symbol.UniqueId; }

    internal SymbolicValue(ESymValue symbol)
    {
      this.symbol = symbol;
    }

    #region IEquatable<SymbolicValue> Members

    //^ [StateIndependent]
    public bool Equals(SymbolicValue other)
    {
      return symbol == other.symbol;
    }

    public override int GetHashCode()
    {
      return symbol != null ? symbol.GlobalId : -1;
    }

    public override bool Equals(object obj)
    {
      if (obj is SymbolicValue)
      {
        SymbolicValue that = (SymbolicValue)obj;
        return this.symbol.Equals(that.symbol);
      }
      return false;
    }
    #endregion

    public override string ToString()
    {
      if (symbol == null) return "<null>";
      return symbol.ToString();
    }

    public bool IsNull
    {
      get { return symbol == null; }
    }

    public int MethodLocalKey { get { return this.symbol.UniqueId; } }

    #region IComparable<SymbolicValue> Members

    public Source CompareTo(SymbolicValue other)
    {
      return this.symbol.CompareTo(other.symbol);
    }

    #endregion

    #region IComparable Members

    public Source CompareTo(object obj)
    {
      if (obj is SymbolicValue)
      {
        return this.CompareTo((SymbolicValue)obj);
      }
      return 1;
    }

    #endregion
  }

  #region Path Elements
  [Serializable]
  public abstract class PathElement
  {
    public virtual bool IsBooleanTyped { get { return false; } }
    public virtual bool IsDeref { get { return false; } }
    public virtual bool IsMethodCall { get { return false; } }
    public virtual bool IsGetter { get { return false; } }
    public virtual bool IsStatic { get { return false; } }
    public virtual bool IsUnmanagedPointer { get { return false; } }
    public virtual bool IsManagedPointer { get { return false; } }
    public virtual bool HasExtraDeref { get { return false; } }
    public virtual bool IsParameter { get { return false; } }

    public virtual bool IsParameterOut { get { return false; } }
    public virtual bool IsReturnValue { get { return false; } }
    public virtual bool IsParameterRef { get { return false; } }
    public virtual bool IsModel { get { return false; } }
    public abstract PathElement SubstituteElement(object from, object to);

    /// <returns>The parameter in the path element, if any</returns>
    public virtual bool TryParameter<Parameter>(out Parameter p)
    {
      p = default(Parameter);
      return false;
    }

    /// <returns>The field in the PathElement, if any </returns>
    public virtual bool TryField<Field>(out Field f)
    {
      f = default(Field);
      return false;
    }

    public virtual bool TryMethod<Method>(out Method m)
    {
      m = default(Method);
      return false;
    }

    /// <summary>
    /// Returns the type of the result of the path element (if possible).
    /// </summary>
    public abstract bool TryGetResultType<Type>(out Type type);

    /// <summary>
    /// Set to the type name it should be cast to on output
    /// </summary>
    virtual public string CastTo { get { return ""; } } // default is: no cast

    /// <summary>
    /// returns true if the path element implicitly takes the address of the element, i.e,
    /// local variable, parameter, or field (static or instance)
    /// </summary>
    public abstract bool IsAddressOf { get; }
    public abstract Result Decode<Data, Result, Visitor, PC, Local, Parameter, Method, Field, Type>(PC label, Visitor query, Data data)
      where Visitor : ICodeQuery<PC, Local, Parameter, Method, Field, Type, Data, Result>;

    public abstract override string ToString();

  }

  public static class PathExtensions
  {
    public static Set<Field> FieldsIn<Field>(this FList<PathElement> path)
    {
      var result = new Set<Field>();
      if (path != null)
      {
        foreach (var element in path.GetEnumerable())
        {
          Field f;
          if (element.TryField(out f))
          {
            result.Add(f);
          }
        }
      }

      return result;
    }

    [ContractVerification(false)]
    public static string ToCodeString(this PathElement[] path)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return PathToString(path);
    }

    public static string ToCodeString(this FList<PathElement> path)
    {
      return PathToString(path);
    }

    private static string PathToString(FList<PathElement> path)
    {
      var first = true;
      var endsInAddressOf = false;
      var sb = new StringBuilder();
      var bEndsWithUnmanaged = false;

      for (; path != null; path = path.Tail)
      {
        PathElement c = path.Head;
        Contract.Assume(c != null);
        if (c.IsMethodCall && !c.IsGetter && c.IsStatic)
        {
          var arg = sb.ToString();
          sb = new StringBuilder();
          sb.Append(c.ToString());
          sb.Append('(');
          sb.Append(arg);
          sb.Append(')');
          first = false;
        }
        else
        {
          if (!string.IsNullOrEmpty(c.CastTo))
          {
            var arg = sb.ToString();
            sb = new StringBuilder();
            sb.Append("((");
            sb.Append(c.CastTo);
            // this test is not pretty, it should be included in the CastTo field. But come on, unsafe pointers are not pretty either
            if (bEndsWithUnmanaged)
              sb.Append('*');
            sb.Append(')');
            sb.Append(arg);
            sb.Append(')');
          }

          if (first)
            first = false;
          else
          {
            if (bEndsWithUnmanaged)
              sb.Append("->");
            else
              sb.Append('.');
          }

          sb.Append(c.ToString());
          if (c.IsMethodCall && !c.IsGetter)
          {
            sb.Append("()");
          }
        }
        int iter = (c.IsAddressOf ? 1 : 0) + (c.IsUnmanagedPointer ? 1 : 0) + (c.IsManagedPointer ? 1 : 0);
        bEndsWithUnmanaged = c.IsUnmanagedPointer;

        if (c.IsAddressOf)
        {
          for (int j = 0; j < iter; ++j)
          {
            if (path.Tail != null)
            {
              if (path.Tail.Head.IsDeref)
              {
                // skip deref
                path = path.Tail;
              }
            }
            else
            {
              // end of path
              endsInAddressOf = true;
            }
          }
        }
      }
      if (endsInAddressOf)
      {
        if (bEndsWithUnmanaged)
          return sb.ToString(); // address of dereference = &*, so output nothing
        else
          return "&" + sb.ToString();
      }
      else
      {
        if (bEndsWithUnmanaged)
          return "*" + sb.ToString();
        else
          return sb.ToString();
      }
    }

    private static string PathToString(PathElement[] path)
    {
      Contract.Requires(path != null);
      Contract.Requires(Contract.ForAll(path, p => p != null));

      var first = true;
      var endsInAddressOf = false;
      var sb = new StringBuilder();
      var bEndsWithUnmanaged = false;

      if (path.Length == 1)
      {
        return path[0].ToString();
      }

      for (int i = 0; i < path.Length; i++)
      {
        var c = path[i];
        if (c.IsMethodCall && !c.IsGetter && c.IsStatic)
        {
          var arg = sb.ToString();
          sb = new StringBuilder();
          sb.Append(c.ToString());
          sb.Append('(');
          sb.Append(arg);
          sb.Append(')');
          first = false;
        }
        else
        {
          if (!string.IsNullOrEmpty(c.CastTo))
          {
            var arg = sb.ToString();
            sb = new StringBuilder();
            sb.Append("((");
            sb.Append(c.CastTo);
            // this test is not pretty, it should be included in the CastTo field. But come on, unsafe pointers are not pretty either
            if (bEndsWithUnmanaged)
              sb.Append('*');
            sb.Append(')');
            sb.Append(arg);
            sb.Append(')');
          }

          if (first)
            first = false;
          else
          {
            if (bEndsWithUnmanaged)
              sb.Append("->");
            else
              sb.Append('.');
          }

          if (c.IsParameterOut)
          {
            sb.AppendFormat("Contract.ValueAtReturn(out {0})", c.ToString());
          }
          else
          {
            sb.Append(c.ToString());
          }

          if (c.IsMethodCall && !c.IsGetter)
          {
            sb.Append("()");
          }
        }
        int iter = (c.IsAddressOf ? 1 : 0) + (c.IsUnmanagedPointer ? 1 : 0) + (c.IsManagedPointer ? 1 : 0);
        bEndsWithUnmanaged = c.IsUnmanagedPointer;
        //if (c.IsAddressOf) // can be a managed pointer (out parameter)
        for (int j = 0; j < iter; ++j)
        {
          if (i + 1 < path.Length)
          {
            if (path[i + 1].IsDeref)
            {
              // skip deref
              i++;
            }
          }
          else
          {
            // end of path
            endsInAddressOf = true;
          }
        }
      }

      if (endsInAddressOf)
      {
        if (bEndsWithUnmanaged)
          return sb.ToString(); // address of dereference = &*, so output nothing
        else
          return "&" + sb.ToString();
      }
      else
      {
        if (bEndsWithUnmanaged)
          return "*" + sb.ToString();
        else
          return sb.ToString();
      }
    }
  }


  #endregion

  /// <summary>
  /// This abstraction computes must aliasing while making optimistic assumptions about non-aliasing of unrelated
  /// objects.
  /// It produces a symbolic name for all intermediate values such that subsequent analyses can focus on value
  /// relations rather than dealing with the stack and heap model of IL. 
  /// </summary>
  public class OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
    : IAnalysis<APC, OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Domain, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Temp, Temp, OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Domain, OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.Domain>, Unit>
    where Type : IEquatable<Type>
  {

    #region Privates

    struct TypeCache
    {
      private bool cacheIsValid;
      private bool haveType;
      private Type cache;
      string fullName;

      [ContractInvariantMethod]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
      private void ObjectInvariant()
      {
        Contract.Invariant(cache != null || !cacheIsValid || !haveType);
      }

      public TypeCache(string fullName)
      {
        cacheIsValid = false;
        haveType = false;
        cache = default(Type);
        this.fullName = fullName;
      }
      public bool TryGet(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, out Type type)
      {
        Contract.Ensures(Contract.ValueAtReturn(out type) != null || !Contract.Result<bool>());

        if (!cacheIsValid)
        {
          cacheIsValid = true;
          haveType = mdDecoder.TryGetSystemType(fullName, out cache);
        }
        type = cache;
        return haveType;
      }
    }

    /// <summary>
    /// This must be true, otherwise, we have renaming problems
    /// </summary>
    private const bool EagerCaching = true;

    private readonly ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Temp, Temp, IStackContext<Field, Method>, Unit> stackLayer;

    IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder
    {
      get
      {
        Contract.Ensures(Contract.Result<IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>>() != null);

        return this.stackLayer.MetaDataDecoder;
      }
    }
    IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder { get { return this.stackLayer.ContractDecoder; } }
    IStackContext<Field, Method> context { get { return this.stackLayer.Decoder.Context; } }
    Method currentMethod { get { return this.context.MethodContext.CurrentMethod; } }

    private bool TryComputeFromJoinCache(Domain inDomain, Domain outDomain, APC joinPoint, out IFunctionalMap<ESymValue, FList<ESymValue>> forward)
    {
      IMergeInfo mi;
      forward = null;
      if (this.mergeInfoCache.TryGetValue(joinPoint, out mi))
      {
        if (mi != null)
        {
          if (outDomain.IsResultEGraph(mi))
          {
            if (inDomain.IsGraph1(mi))
            {
              forward = mi.ForwardG1Map;
              return true;
            }
            if (inDomain.IsGraph2(mi))
            {
              forward = mi.ForwardG2Map;
              return true;
            }
          }
        }
      }
      return false;
    }

    Dictionary<Pair<APC, APC>, IFunctionalMap<SymbolicValue, FList<SymbolicValue>>> forwardRenamings = new Dictionary<Pair<APC, APC>, IFunctionalMap<SymbolicValue, FList<SymbolicValue>>>();

    /// <summary>
    /// Key1 is source APC, Key2 is target APC (edge). We use this so we can enumerate all targets of a particular block where we have renamings
    /// </summary>
    DoubleTable<APC, APC, Unit> renamePoints = new DoubleTable<APC, APC, Unit>();

    /// <summary>
    /// Maps join point -> incoming point -> IMergeInfo from joins. This is a way to cache renamings if possible.
    /// If the points map to null, then it is the original incoming edge and is still valid, i.e., the identity renaming.
    /// </summary>
    Dictionary<APC, IMergeInfo> mergeInfoCache = new Dictionary<APC, IMergeInfo>();

    #endregion

    public OptimisticHeapAnalyzer(
      ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Temp, Temp, IStackContext<Field, Method>, Unit> stackLayer,
      bool debugEGraph)
    {
      Contract.Requires(stackLayer != null);

      this.stackLayer = stackLayer;
      if (debugEGraph) { Domain.Debug = true; }

      // hack: NOOOOOOOOOOOOOOOOOOOO!
      Domain.AbstractType.MetadataDecoder = this.mdDecoder;
    }

    #region Configuration

    public bool TurnArgumentExceptionThrowsIntoAssertFalse { get; set; }
    public bool IgnoreExplicitAssumptions { get; set; }
    public bool TraceAssumptions { get; set; }

    TypeCache ArgumentExceptionTypeCache = new TypeCache("System.ArgumentException");


    #endregion

    private IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Temp, Temp, Domain, Domain> GetVisitor()
    {
      return new Domain.AnalysisDecoder(this);
    }

    /// <summary>
    /// Computes symbolic variable renamings on edges leading to join points
    /// </summary>
    public IFunctionalMap<SymbolicValue, FList<SymbolicValue>>/*?*/ EdgeRenaming(Pair<APC, APC> edge, bool joinPoint)
    {
      IFunctionalMap<SymbolicValue, FList<SymbolicValue>> result;
      if (this.forwardRenamings.TryGetValue(edge, out result))
      {
        return result;
      }
      result = null;
      // compute renaming.
      Domain start;
      
      if(!this.PostStateLookup(edge.One, out start))
      {
        return null;
      }

      if (start.IsBottom) return null;

      Domain end;
      this.PreStateLookup(edge.Two, out end);
      IFunctionalMap<ESymValue, FList<ESymValue>>/*?*/ forward;

      if (/*start != null && */end != null)
      {
        // see if we can use cache from join
        if (!TryComputeFromJoinCache(start, end, edge.Two, out forward))
        {
          IFunctionalMap<ESymValue, ESymValue>/*?*/ backward;
          if (!start.LessEqual(end, out forward, out backward))
          {
            Contract.Assume(false, "egraphs in renaming are not related properly"); // means egraphs don't correspond
            throw new Exception("Should never happen");
          }
          if (joinPoint && forward == null)
          {
            // make sure to still call edge conversion since abstract domains might perform some other juggling
            // prior to the join
            forward = start.GetForwardIdentityMap();
          }
        }
        // special case if forward is null, we use that as identity map and don't have to rename
        if (forward != null)
        {
          result = FunctionalIntKeyMap<SymbolicValue, FList<SymbolicValue>>.Empty(SymbolicValue.GetUniqueKey);
          foreach (ESymValue source in forward.Keys)
          {
            // omit identities
            FList<SymbolicValue> targets = null;
            for (var origTargets = forward[source]; origTargets != null; origTargets = origTargets.Tail)
            {
              targets = targets.Cons(new SymbolicValue(origTargets.Head));
            }
            if (targets != null)
            {
              result = result.Add(new SymbolicValue(source), targets);
            }
          }
        }
      }

      if (Domain.Debug && result != null)
      {
        this.PrintRenaming(" * ", Console.Out, s => s.ToString(), edge, result);
      }
      this.forwardRenamings.Add(edge, result);
      return result;
    }

    public Domain InitialValue()
    {
      return new Domain(this);
    }

    /// <summary>
    /// Because we need to have a uniform type for constructors (edge labels) in the egraph, we need to inject the various
    /// types such as Local, Temp, Parameter, Field, into a new type Constructor. One tricky thing is that we want to avoid
    /// allocating these wrappers over and over, so we need to have a table somewhere. We can't have the table on each instance
    /// of the heap abstraction, and we don't want it static. What we will do is use a shared table among all the instances from
    /// a common root.
    ///
    /// For each abstract location, we track its current type in the underlying type representation.
    /// </summary>
    public class Domain :
      IAbstractValue<Domain>
    {
      #region Analsyis State private to this Domain value
      /// <summary>
      /// egraph == null represents bottom
      ///
      /// The egraph is used to keep track of must aliasing. It also associates a Type with each symbolic value
      /// to do some type recovery that is useful in later phases.
      /// </summary>
      readonly EGraph<Constructor, AbstractType> egraph;

      /// <summary>
      /// We use this map to determine if a symbolic value is a constant and what type it has
      /// </summary>
      IFunctionalMap<ESymValue, Constructor> constantLookup;

      /// <summary>
      /// Captures objects and field addresses that have not been modified since entry
      /// </summary>
      IFunctionalSet<ESymValue> unmodifiedSinceEntry;

      /// <summary>
      /// Captures objects and field addresses that have not been modified since entry.
      /// This set contains objects that have had fields updated, but the remaining fields
      /// (not materialized) have not been updated.
      /// </summary>
      IFunctionalSet<ESymValue> unmodifiedSinceEntryForFields;

      /// <summary>
      /// A set of locations created at a call site that are assumed to be havoced. We use
      /// this set in old rename computations within the post condition of that call.
      /// Thus, the set only needs to be maintained at program points within contracts, but not 
      /// once we hit the outermost method again.
      /// </summary>
      IFunctionalSet<ESymValue> modifiedAtCall;

      /// <summary>
      /// Old handling. We count the level of nested olds here
      /// </summary>
      int insideOld;

      /// <summary>
      /// When inside Old, we keep a second old domain state around. It is only initialized
      /// when insideOld crosses from 0 to 1 and all nested olds use it. It gets set to null
      /// when insideOld goes back to 0.
      /// </summary>
      Domain oldDomain;
      APC beginOldPC;

      internal Domain OldDomain { get { return this.oldDomain; } }
      internal APC BeginOldPC { get { return this.beginOldPC; } }

      #endregion

      public bool IsBottomPlaceHolder(ESymValue value)
      {
        return this.egraph.BottomPlaceHolder == value;
      }

      #region Analysis State shared among all Domain values in a method

      /// <summary>
      /// Cache for function symbols uses as edge lables in the egraph
      /// </summary>
      private readonly WrapTable Constructors;

      /// <summary>
      /// Used to save the state at BeginOld APCs. This map is not per domain, but 
      /// shared among all derived states from the initial method state.
      /// </summary>
      private readonly Dictionary<APC, Domain> BeginOldSavedStates;

      private readonly OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> parent;
      #endregion

      #region Constructor representation for edges in EGraph
      class WrapTable
      {
        Dictionary<Local, Constructor.Wrapper<Local>> locals;
        Dictionary<Parameter, Constructor.Wrapper<Parameter>> parameters;
        Dictionary<Field, Constructor.Wrapper<Field>> fields;
        Dictionary<Method, Constructor.Wrapper<Method>> pseudoFields;
        Dictionary<Temp, Constructor.Wrapper<Temp>> temps;
        Dictionary<string, Constructor.Wrapper<string>> strings;
        Dictionary<object, Constructor.Wrapper<object>> programConstants;
        Dictionary<Method, Constructor.Wrapper<Method>> methodPointers;
        Dictionary<Type, Constructor.Wrapper<Type>> typeDefaultValues;
        Dictionary<BinaryOperator, Constructor.Wrapper<BinaryOperator>> binaryOps;
        Dictionary<UnaryOperator, Constructor.Wrapper<UnaryOperator>> unaryOps;

        int idgen;

        private Constructor.Wrapper<T> For<T>(T value, Dictionary<T, Constructor.Wrapper<T>> cache)
        {
          Contract.Ensures(Contract.Result<Constructor.Wrapper<T>>() != null);

          Constructor.Wrapper<T>/*?*/ result;
          if (!cache.TryGetValue(value, out result))
          {
            result = Constructor.For(value, ref idgen, this.mdDecoder, this.contractDecoder);
            cache.Add(value, result);
          }
          else
          {
            Contract.Assume(result != null);
          }
          return result;
        }

        public Constructor For(Local v) { return For(v, locals); }
        public Constructor For(Parameter v) { return For(v, parameters); }
        public Constructor For(Field v)
        {
          v = this.mdDecoder.Unspecialized(v);
          return For(v, fields);
        }
        public Constructor For(Method v) { return For(v, pseudoFields); }
        public Constructor For(Temp v) { return For(v, temps); }
        public Constructor For(string v) { return For(v, strings); }
        public Constructor For(BinaryOperator v) { return For(v, binaryOps); }
        public Constructor For(UnaryOperator v) { return For(v, unaryOps); }
        public Constructor ForConstant(object constant, Type type)
        {
          var c = For(constant, programConstants);
          c.Type = type;
          return c;
        }
        /// <summary>
        /// Used by LdFtn
        /// </summary>
        /// <param name="method">the method</param>
        /// <param name="type">UIntPtr</param>
        public Constructor ForMethod(Method method, Type type)
        {
          var c = For(method, methodPointers);
          c.Type = type;
          return c;
        }

        /// <summary>
        /// Used by InitObj
        /// </summary>
        /// <param name="method">the method</param>
        /// <param name="type">UIntPtr</param>
        public Constructor ForTypeDefaultValue(Type type)
        {
          Constructor.Wrapper<Type> c = For(type, typeDefaultValues);
          return c;
        }

        /// <summary>
        /// Returns true if the symbolic constant represents a program constant
        /// </summary>
        public bool IsConstantOrMethod(Constructor c)
        {
          Constructor.Wrapper<object> cwrapper = c as Constructor.Wrapper<object>;
          if (cwrapper != null)
          {
            if (this.programConstants.ContainsKey(cwrapper.Value))
            {
              return true;
            }
          }
          Constructor.Wrapper<Method> mwrapper = c as Constructor.Wrapper<Method>;
          if (mwrapper != null)
          {
            if (this.methodPointers.ContainsKey(mwrapper.Value))
            {
              return true;
            }
          }
          return false;
        }

        /// <summary>
        /// Returns true if the symbolic constant represents a program constant
        /// </summary>
        public bool IsConstant(Constructor c, out Type type, out object value)
        {
          Constructor.Wrapper<object> cwrapper = c as Constructor.Wrapper<object>;
          if (cwrapper != null)
          {
            if (this.programConstants.ContainsKey(cwrapper.Value))
            {
              type = cwrapper.Type;
              value = cwrapper.Value;
              Contract.Assume(!this.mdDecoder.Equal(type, this.mdDecoder.System_Int32) || value is Int32);
              return true;
            }
          }
          type = default(Type);
          value = null;
          return false;
        }

        /// <summary>
        /// Used to indirect from address to value
        /// </summary>
        public readonly Constructor ValueOf;

        /// <summary>
        /// Used to retain old value across havocs
        /// </summary>
        public readonly Constructor OldValueOf;

        /// <summary>
        /// Special model field for structs used to have a symbolic identity
        /// </summary>
        public readonly Constructor StructId;

        /// <summary>
        /// Special model field for objects used to indicate a version (updated on mutation)
        /// </summary>
        public readonly Constructor ObjectVersion;

        public readonly Constructor NullValue;
        /// <summary>
        /// Used as a pure function for array element addresses ElementAddr(array,index)
        /// </summary>
        public readonly Constructor ElementAddress;
        /// <summary>
        /// Model field for array length
        /// </summary>
        public readonly Constructor Length;
        /// <summary>
        /// Model field for writable extent of pointers
        /// </summary>
        public readonly Constructor WritableBytes;
        /// <summary>
        /// dummy struct address for all void values
        /// </summary>
        public readonly Constructor VoidAddr;
        public readonly Constructor ZeroValue;
        public readonly Constructor UnaryNot;
        public readonly Constructor NeZero;
        public readonly Constructor IsInst;
        /// <summary>
        /// Special way to cache box operations on same values
        /// </summary>
        public readonly Constructor BoxOperator;
        /// <summary>
        /// Special field in delegates to hold method pointer
        /// </summary>
        public readonly Constructor FunctionPointer;
        /// <summary>
        /// Special field in delegates to hold target object
        /// </summary>
        public readonly Constructor ClosureObject;
        /// <summary>
        /// Breadcrumb to tag values resulting from calls. (target doesn't matter)
        /// </summary>
        public readonly Constructor ResultOfCall;
        /// <summary>
        /// Breadcrumb to tag values resulting from pure calls. (target doesn't matter)
        /// </summary>
        public readonly Constructor ResultOfPureCall;
        /// <summary>
        /// Breadcrumb to tag values resulting from ldelem. (target doesn't matter)
        /// </summary>
        public readonly Constructor ResultOfLdelem;
        /// <summary>
        /// Breadcrumb to tag values resulting from cast. (target is original value being cast)
        /// </summary>
        public readonly Constructor ResultOfCast;
        /// <summary>
        /// Breadcrumb to tag values resulting from out parameters. (target does not matter)
        /// </summary>
        public readonly Constructor ResultOfOutParameter;

        /// <summary>
        /// Breadcrumb to tag values resulting for fields after calling a method on "this"
        /// </summary>
        public readonly Constructor ResultOfCallThisHavoc;

        /// <summary>
        /// Breadcrumb to tag values resulting from OldValue
        /// </summary>
        public readonly Constructor ResultOfOldValue;
#if false
        /// <summary>
        /// Used to approximate hash consing across join points of ElementAddr(a,i)
        /// </summary>
        public readonly Constructor LastArrayIndex;
        public readonly Constructor LastArrayElemAddress;
#endif
        readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;
        readonly IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder;
        public WrapTable(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder)
        {
          this.mdDecoder = mdDecoder;
          this.contractDecoder = contractDecoder;

          this.locals = new Dictionary<Local, Constructor.Wrapper<Local>>();
          this.parameters = new Dictionary<Parameter, Constructor.Wrapper<Parameter>>();
          this.fields = new Dictionary<Field, Constructor.Wrapper<Field>>();
          this.pseudoFields = new Dictionary<Method, Constructor.Wrapper<Method>>();
          this.temps = new Dictionary<int, Constructor.Wrapper<Temp>>();
          this.strings = new Dictionary<string, Constructor.Wrapper<string>>();
          this.programConstants = new Dictionary<object, Constructor.Wrapper<object>>();
          this.methodPointers = new Dictionary<Method, Constructor.Wrapper<Method>>();
          this.typeDefaultValues = new Dictionary<Type, Constructor.Wrapper<Type>>();
          this.binaryOps = new Dictionary<BinaryOperator, Constructor.Wrapper<BinaryOperator>>();
          this.unaryOps = new Dictionary<UnaryOperator, Constructor.Wrapper<UnaryOperator>>();


          ValueOf = For("$Value");
          OldValueOf = For("$OldValue");
          StructId = For("$StructId");
          ObjectVersion = For("$ObjectVersion");
          NullValue = For("$Null");
          ElementAddress = For("$Element");
          Length = For("$Length");
          WritableBytes = For("$WritableBytes");
          VoidAddr = For("$VoidAddr");
          UnaryNot = For("$UnaryNot");
          IsInst = For("$IsInst");
          NeZero = For("$NeZero");
          BoxOperator = For("$Box");
          FunctionPointer = For("$FnPtr");
          ClosureObject = For("$Closure");

          // Breadcrumbs: we use these to tag values (target doesn't matter)
          ResultOfCall = For("$ResultOfCall");
          ResultOfLdelem = For("$ResultOfLdElem");
          ResultOfCast = For("$ResultOfCast");
          ResultOfOutParameter = For("$ResultOfOut");
          ResultOfCallThisHavoc = For("$ResultOfCallThisHavoc");
          ResultOfPureCall = For("$ResultOfPureCall");
          ResultOfOldValue = For("$ResultOfOldValue");

          ZeroValue = ForConstant((int)0, this.mdDecoder.System_Int32);
        }
      }


      [Serializable]
      [ContractVerification(false)]
      internal abstract class Constructor : IEquatable<Constructor>, IConstantInfo, IVisibilityCheck<Method>
      {
        public readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;

        protected Constructor(ref int idgen, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
        {
          this.mdDecoder = mdDecoder;
        }

        public abstract bool IsStackTemp { get; }

        public abstract bool ActsAsField { get; }

        public abstract bool IsVirtualMethod { get; }

        public abstract bool KeepAsBottomField { get; }

        public abstract bool ManifestField { get; }

        public abstract Type FieldAddressType(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder);

        /// <param name="tryCompact">true if pure method accessors should be compressed to avoid the addres/ldind pair</param>
        public abstract PathElementBase ToPathElement(bool tryCompact);

        public abstract bool HasExtraDeref { get; }

        public abstract bool IsAsVisibleAs(Method method);

        public abstract bool IsVisibleFrom(Method method);

        public abstract bool RootImpliesParameterOrResultOrStatic { get; }

        public abstract bool IsTopOfStack(int stackDepth);

        /// <summary>
        /// For fields and properties, this return whether they are static. Otherwise false.
        /// </summary>
        public abstract bool IsStatic { get; }

        [Serializable]
        [ContractVerification(false)]
        public class Wrapper<T> : Constructor
        {

          public readonly T Value;
          private Type type;
          public Type Type { get { return this.type; } set { this.type = value; } }

          public Wrapper(T value, ref int idgen, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
            : base(ref idgen, mdDecoder)
          {
            this.Value = value;
          }

          public override bool IsStackTemp
          {
            get
            {
              if (typeof(T).Equals(typeof(int))) return true;
              return false;
            }
          }

          public override bool ActsAsField
          {
            get
            {
              return (this.Value is Field);
            }
          }

          public override bool KeepAsBottomField
          {
            get
            {
              string s = this.Value as string;
              if (s == null) return true;
              return (s != "$UnaryNot" && s != "$NeZero");
            }
          }

          public override bool ManifestField
          {
            get
            {
              // manifest $Value and fields and pseudo fields
              string s = this.Value as string;
              if (s != null) { return s == "$Value" || s == "$Length"; }
              return (this.Value is Field || this.Value is Method);
            }
          }

          public override Type FieldAddressType(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
          {
            if (this.Value is Field)
            {
              return mdDecoder.ManagedPointer(mdDecoder.FieldType((Field)(object)this.Value));
            }
            throw new NotImplementedException();
          }

          public override bool IsStatic
          {
            get
            {
              if (this.Value is Field)
              {
                return this.mdDecoder.IsStatic((Field)(object)this.Value);
              }
              if (this.Value is Method)
              {
                return this.mdDecoder.IsStatic((Method)(object)this.Value);
              }
              return false;
            }
          }

          public override bool IsVirtualMethod
          {
            get
            {
              if (this.Value is Method)
              {
                return this.mdDecoder.IsVirtual((Method)(object)this.Value);
              }
              return false;
            }
          }

          //^ [Confined]
          public override string ToString()
          {
            if (typeof(T).Equals(typeof(Temp)))
            {
              return String.Format("s{0}", Value);
            }
            if (typeof(T).Equals(typeof(Field)))
            {
              Field f = (Field)(object)Value;
              if (mdDecoder.IsStatic(f))
              {
                //return String.Format("{0}.{1}", mdDecoder.FullName(mdDecoder.DeclaringType(f)), mdDecoder.Name(f));
                return String.Format("{0}.{1}", OutputPrettyCS.TypeHelper.TypeFullName(mdDecoder, mdDecoder.DeclaringType(f)), mdDecoder.Name(f));
              }
              return mdDecoder.Name(f);
            }
            if (typeof(T).Equals(typeof(Method)))
            {
              Method m = (Method)(object)Value;
              string name;

              if (mdDecoder.IsPropertyGetter(m) || mdDecoder.IsPropertySetter(m))
              {
                Property prop = mdDecoder.GetPropertyFromAccessor(m);
                if (prop != null)
                {
                  name = mdDecoder.Name(prop);
                }
                else
                {
                  name = mdDecoder.Name(m);
                  if (name.StartsWith("get_") || name.StartsWith("set_"))
                  {
                    name = name.Substring(4);
                  }
                }
              }
              else
              {
                name = this.mdDecoder.Name(m);
              }
              if (mdDecoder.IsStatic(m))
              {
                //name = String.Format("{0}.{1}", mdDecoder.FullName(mdDecoder.DeclaringType(m)), name);
                name = String.Format("{0}.{1}", OutputPrettyCS.TypeHelper.TypeFullName(mdDecoder, mdDecoder.DeclaringType(m)), name);
              }
              return name;
            }
            if (typeof(T).Equals(typeof(Local)))
            {
              Local l = (Local)(object)Value;
              return mdDecoder.Name(l);
            }

            // Handle string constants.
            var stringConstant = Value as string;
            if (stringConstant != null && Type != null && Type.ToString() == "System.String")
            {
              using (var writer = new StringWriter())
              {
                using (var provider = System.CodeDom.Compiler.CodeDomProvider.CreateProvider("CSharp"))
                {
                  provider.GenerateCodeFromExpression(new System.CodeDom.CodePrimitiveExpression(stringConstant), writer, null);
                  return writer.ToString();
                }
              }
            }

            return Value.ToString();
          }

          [NonSerialized]
          protected PathElementBase pathElementCache;
          public override PathElementBase ToPathElement(bool tryCompact)
          {
            if (pathElementCache == null)
            {
              string value = Value as string;
              if (value != null)
              {
                if (value == "$Length")
                {
                  pathElementCache = new SpecialPathElement(SpecialPathElementKind.Length, this);
                }
                else if (value == "$Value")
                {
                  // don't cache this, as we have different types on each path element
                  return new SpecialPathElement(SpecialPathElementKind.Deref, this);
                }
                else if (value == "$WritableBytes")
                {
                  pathElementCache = new SpecialPathElement(SpecialPathElementKind.WritableBytes, this);
                }
              }
              else
              {
                pathElementCache = new PathElement<T>(this.Value, this.ToString(), this);
              }
            }
            return pathElementCache;
          }

          public override bool IsAsVisibleAs(Method method)
          {
            if (this.Value is Field)
            {
              Field f = (Field)(object)this.Value;
              return mdDecoder.IsAsVisibleAs(f, method);
            }

            if (this.Value is Method)
            {
              Method m = (Method)(object)this.Value;
              return mdDecoder.IsAsVisibleAs(m, method);
            }

            if (mdDecoder.IsConstructor(method) && this.Value is Parameter)
            {
              var name = mdDecoder.Name((Parameter)(object)this.Value);
              if (name == "this") return false;
            }
            return true;
          }

          public override bool RootImpliesParameterOrResultOrStatic
          {
            get
            {
              if (this.Value is Local)
              {
                return false;
              }

              if (this.Value is int)
              {
                var stackOffset = (int)(object)this.Value;
                return stackOffset == 0;
              }
              return true;
#if false
              if (this.Value is Field)
              {
                return true;
                //Field f = (Field)(object)this.Value;
                //return !mdDecoder.IsStatic(f);
              }

              if (this.Value is Method)
              {
                return true;
                //Method m = (Method)(object)this.Value;
                //return !mdDecoder.IsStatic(m);
              }

              if (this.Value is Parameter)
              {
                return true;
              }


              if (this.Value is string)
              {
                // Special case, $Length, $Value or $WritteableByte, always visible
                return true;
              }
              return true;
#endif
            }
          }

          public override bool IsTopOfStack(int localStackDepth)
          {
            if (this.Value is Local)
            {
              return false;
            }

            if (this.Value is int)
            {
              var stackOffset = (int)(object)this.Value;
              return stackOffset == localStackDepth - 1;
            }

            return false;
          }
          /// <summary>
          /// Tests only the last access, traverse the path if you want to be sure
          /// that the actual full element is visible
          /// </summary>
          public override bool IsVisibleFrom(Method method)
          {
            var declaringType = mdDecoder.DeclaringType(method);
            if (this.Value is Field)
            {
              Field f = (Field)(object)this.Value;
              return !mdDecoder.IsCompilerGenerated(f) && mdDecoder.IsVisibleFrom(f, declaringType);
            }

            if (this.Value is Method)
            {
              Method m = (Method)(object)this.Value;
              return mdDecoder.IsVisibleFrom(m, declaringType);
            }

            if (this.Value is Parameter)
            {
              Parameter p = (Parameter)(object)this.Value;
              return mdDecoder.Equal(mdDecoder.DeclaringMethod(p), method);
            }

            if (this.Value is Local)
            {
              Local l = (Local)(object)this.Value;
              var locs = mdDecoder.Locals(method);
              for (int i = 0; i < locs.Count; ++i)
              {
                if (locs[i].Equals(l))
                  return true;
              }
              return false;
            }

            if (this.Value is string)
            {
              // Special case, $Length, $Value or $WritteableByte, always visible
              return true;
            }

            return true;
          }

          public override bool HasExtraDeref
          {
            get { return this.Value is Local || this.Value is Field; }
          }

        }

        [Serializable]
        public class PureMethodConstructor : Constructor.Wrapper<Method>
        {
          private readonly bool isModel;
          private readonly bool actsAsField;

          public PureMethodConstructor(Method method, ref int idgen, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder)
            : base(method, ref idgen, mdDecoder)
          {
            this.isModel = contractDecoder.IsModel(this.Value);
            this.actsAsField = mdDecoder.Parameters(method).Count <= 1;
          }

          [NonSerialized]
          MethodCallPathElement uncompressedCache;

          public override bool ActsAsField
          {
            get
            {
              return true;
            }
          }

          public override Type FieldAddressType(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
          {
            return mdDecoder.ManagedPointer(mdDecoder.ReturnType(this.Value));
          }

          public override PathElementBase ToPathElement(bool tryCompact)
          {
            if (!actsAsField) return null;
            if (tryCompact)
            {
              if (this.pathElementCache == null)
              {
                pathElementCache = new MethodCallPathElement(this.Value, this.IsGetter, this.IsBooleanTyped, this.mdDecoder.IsStatic(this.Value), this.isModel, this.ToString(), this, true);
              }
              return this.pathElementCache;
            }
            if (uncompressedCache == null)
            {
              uncompressedCache = new MethodCallPathElement(this.Value, this.IsGetter, this.IsBooleanTyped, this.mdDecoder.IsStatic(this.Value), this.isModel, this.ToString(), this, false);
            }
            return this.uncompressedCache;
          }

          private bool IsBooleanTyped
          {
            get
            {
              return this.mdDecoder.Equal(this.mdDecoder.ReturnType(this.Value), this.mdDecoder.System_Boolean);
            }
          }

          private bool IsGetter
          {
            get
            {
              return this.mdDecoder.IsPropertyGetter(this.Value);
            }
          }

        }

        [Serializable]
        public class ParameterConstructor : Constructor.Wrapper<Parameter>
        {
          Parameter p { get { return this.Value; } }
          [NonSerialized]
          int argumentIndex;

          [OnDeserialized]
          protected void OnDeserialized(StreamingContext context)
          {
            this.argumentIndex = this.mdDecoder.ArgumentIndex(this.p);
          }

          public ParameterConstructor(Parameter p, ref int idgen, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
            : base(p, ref idgen, mdDecoder)
          {
            this.argumentIndex = mdDecoder.ArgumentIndex(p);
          }

          public override bool ActsAsField
          {
            get { return false; }
          }

          public override Type FieldAddressType(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
          {
            throw new NotImplementedException();
          }

          public override bool Equals(object obj)
          {
            ParameterConstructor pc = obj as ParameterConstructor;
            if (pc == null) return false;
            return pc.argumentIndex == this.argumentIndex;
          }

          public override int GetHashCode()
          {
            return this.argumentIndex;
          }

          public override string ToString()
          {
            return mdDecoder.Name(p);
          }

          public override PathElementBase ToPathElement(bool tryCompact)
          {
            if (pathElementCache == null)
            {
              this.pathElementCache = new PathElement<Parameter>(this.p, this.mdDecoder.Name(this.p), this);
            }
            return pathElementCache;
          }

          public override bool HasExtraDeref
          {
            get { return /*true*/ false; } // Mic: parameter have an extra deref only if they are out or ref, but it's covered by the IsAddressOf field
          }
        }

        internal static Constructor.Wrapper<T> For<T>(T value, ref int idgen, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder)
        {
          Contract.Ensures(Contract.Result<Constructor.Wrapper<T>>() != null);

          if (value is Parameter)
          {
            return (Constructor.Wrapper<T>)(object)new ParameterConstructor((Parameter)(object)value, ref idgen, mdDecoder);
          }
          if (typeof(T).Equals(typeof(Method)) && value is Method)
          {
            return (Constructor.Wrapper<T>)(object)new PureMethodConstructor((Method)(object)value, ref idgen, mdDecoder, contractDecoder);
          }
          return new Constructor.Wrapper<T>(value, ref idgen, mdDecoder);
        }

        #region IEquatable<Constructor> Members

        public bool Equals(Constructor other)
        {
          return this == other;
        }

        #endregion
      }
      #endregion

      #region PathElement Implementations
      [Serializable]
      internal abstract class PathElementBase : PathElement
      {
        internal readonly Constructor Constructor;
        [NonSerialized]
        protected bool hasExtraDeref; // locals and fields have an extra deref path
        // we do not serialize hasExtraDeref because we can get this info from the Constructor (see OnDeserialized below)

        // just after the deserialization
        [OnDeserialized]
        protected void OnDeserialized(StreamingContext context)
        {
          this.hasExtraDeref = this.Constructor.HasExtraDeref;
        }

        protected PathElementBase(Constructor origConstructor)
        {
          this.Constructor = origConstructor;
          this.hasExtraDeref = origConstructor.HasExtraDeref;
        }

        public override bool HasExtraDeref
        {
          get
          {
            return hasExtraDeref;
          }
        }

        /// <summary>
        /// The expected type is the type of the result of the path element so far. We use this so that we can
        /// type "Deref" nodes by providing the non-address taken type of the prior element. E.g., paths must
        /// start with a local, parameter, or static field which inherently know their own type.
        /// </summary>
        internal abstract bool TrySetType(Type expectedType, out Type value);

      }

      [Serializable]
      [ContractVerification(false)]
      internal class ReturnValuePathElement : PathElementBase
      {
        private Type type;
        private string typeName;

        public ReturnValuePathElement(Type type, string typeName, Constructor origC)
          : base(origC)
        {
          this.type = type;
          this.typeName = typeName;
        }

        public override bool IsReturnValue { get { return true; } }

        public override PathElement SubstituteElement(object from, object to)
        {
          return this;
        }

        public override bool TryGetResultType<Type2>(out Type2 type)
        {
          type = (Type2)(object)this.type;
          return true;
        }

        public override bool IsAddressOf
        {
          get { return false; }
        }

        public override Result Decode<Data, Result, Visitor, PC, Local2, Parameter2, Method2, Field2, Type2>(PC label, Visitor query, Data data)
        {
          return query.Ldresult(label, (Type2)(object)this.type, Unit.Value, Unit.Value, data);
        }

        public override string ToString()
        {
          //          return String.Format("Contract.Result<{0}>()", this.typeName);
          return String.Format("Contract.Result<{0}>()", PrettyPrint(this.type.ToString()));
        }

        internal override bool TrySetType(Type expectedType, out Type value)
        {
          throw new NotImplementedException();
        }

        private string PrettyPrint(string typeName)
        {
          // We want to transform CCI string: "System.Func`2<type parameter.T,System.Object>[]" into ""System.Func<T,System.Object>[]"
          const string typePar = "type parameter.";

          // First, remove all the occorrunces of "type parameter."
          var result = typeName.Replace(typePar, "");

          if (!object.ReferenceEquals(result, typeName))
          {
            var str = new StringBuilder();
            for (var i = 0; i < result.Length; i++)
            {
              var curr = result[i];
              if (curr == '`')
              {
                // skip
                while(i < result.Length && char.IsDigit(result[i+1]))
                {
                  i++;
                }
              }
              else
              {
                str.Append(curr);
              }
            }

            return str.ToString();
          }
          else
          {
            return typeName;
          }
        }

      }

      /// <summary>
      /// NOTE: when used on a parameter, signifies ldarga. When ldarg is preferred, the special type ParameterPathElement
      /// is used.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      [ContractVerification(false)]
      [Serializable]
      internal class PathElement<T> : PathElementBase
      {
        protected Type resultType;
        protected string mCastPreviousTo;
        private T element;
        override public string CastTo { get { return mCastPreviousTo; } }
        public T Element { get { return this.element; } }
        protected bool isStatic;
        protected bool isUnmanagedPointer, isManagedPointer;
        public readonly string AsString;

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
          Contract.Invariant(AsString != null);
        }

        public PathElement(T p, string s, Constructor c)
          : base(c)
        {
          Contract.Requires(s != null);

          this.element = p;
          this.AsString = s;
          this.isStatic = false;
          this.isUnmanagedPointer = false;
          this.isManagedPointer = false;
        }

        public override bool IsAddressOf
        {
          get { return true; }
        }

        public override bool IsStatic
        {
          get
          {
            return this.isStatic;
          }
        }

        public override bool IsParameterRef
        {
          get
          {
            return typeof(T) == typeof(Parameter);
          }
        }

        public override bool IsParameterOut
        {
          get
          {
            return typeof(T) == typeof(Parameter) && this.Constructor.mdDecoder.IsOut((Parameter)(object)this.Element);
          }
        }

        Type ResultType
        {
          get
          {
            return resultType;
          }
        }

        public override bool TryParameter<Parameter2>(out Parameter2 p)
        {
          if (typeof(T) == typeof(Parameter2))
          {
            p = (Parameter2)(object)this.Element;
            return true;
          }
          return base.TryParameter(out p);
        }

        public override bool TryField<Field2>(out Field2 f)
        {
          if (typeof(T) == typeof(Field2))
          {
            f = (Field2)(object)this.Element;
            return true;
          }
          f = default(Field2);
          return false;
        }

        public override bool TryMethod<Method2>(out Method2 m)
        {
          if(typeof(T) == typeof(Method2))
          {
            m = (Method2)(object)this.Element;
            return true;
          }

          return base.TryMethod(out m);
        }

        public override bool TryGetResultType<Type2>(out Type2 type)
        {
          if (this.resultType is Type2)
          {
            type = (Type2)(object)this.resultType;
            return true;
          }
          type = default(Type2);
          return false;
        }

        public override bool IsUnmanagedPointer
        {
          get
          {
            return isUnmanagedPointer;
          }
        }

        public override bool IsManagedPointer
        {
          get
          {
            return isManagedPointer;
          }
        }

        internal override bool TrySetType(Type prevType, out Type value)
        {
          var mdDecoder = this.Constructor.mdDecoder;

          if (typeof(T).Equals(typeof(Parameter)))
          {
            var p = (Parameter)(object)Element;
            var pt = mdDecoder.ParameterType(p);

            this.isUnmanagedPointer = mdDecoder.IsUnmanagedPointer(pt);
            this.isManagedPointer = mdDecoder.IsManagedPointer(pt);
            value = mdDecoder.ManagedPointer(pt);
            this.resultType = value;
            return true;
          }
          if (typeof(T).Equals(typeof(Field)))
          {
            var f = (Field)(object)Element;
            var ft = mdDecoder.FieldType(f);

            this.isStatic = mdDecoder.IsStatic(f);
            this.isUnmanagedPointer = mdDecoder.IsUnmanagedPointer(ft);
            this.isManagedPointer = mdDecoder.IsManagedPointer(ft);
            value = mdDecoder.ManagedPointer(ft);
            this.resultType = value;

            // Store the type we want to see prior to this access
            var dt = mdDecoder.DeclaringType(f);
            if (mdDecoder.IsManagedPointer(prevType) || mdDecoder.IsUnmanagedPointer(prevType))
              prevType = mdDecoder.ElementType(prevType);

            // HACK: get the unspecialized type until type instantiation is fixed in the heap analysis
            prevType = mdDecoder.Unspecialized(prevType);

            if (
              !mdDecoder.IsStatic(f) &&
              !dt.Equals(prevType) &&
              !(mdDecoder.DerivesFrom(prevType, dt) && (mdDecoder.IsProtected(f) || mdDecoder.IsPublic(f)))
              )
            {
              this.mCastPreviousTo = OutputPrettyCS.TypeHelper.TypeFullName(mdDecoder, dt);
            }

            return true;
          }
          if (typeof(T).Equals(typeof(Local)))
          {
            var l = (Local)(object)Element;
            var lt = mdDecoder.LocalType(l);

            this.isUnmanagedPointer = mdDecoder.IsUnmanagedPointer(lt);
            this.isManagedPointer = mdDecoder.IsManagedPointer(lt);
            value = mdDecoder.ManagedPointer(lt);
            this.resultType = value;

            return true;
          }
          if (typeof(T).Equals(typeof(Method)))
          {
            var m = (Method)(object)Element;
            if (this.IsAddressOf)
            {
              value = mdDecoder.ManagedPointer(mdDecoder.ReturnType(m));
            }
            else
            {
              value = mdDecoder.ReturnType(m);
            }
            this.resultType = value;

            // Store the type we want to see prior to this access
            if (mdDecoder.IsManagedPointer(prevType) || mdDecoder.IsUnmanagedPointer(prevType))
              prevType = mdDecoder.ElementType(prevType);

            // HACK: get the unspecialized type until type instantiation is fixed in the heap analysis
            prevType = mdDecoder.Unspecialized(prevType);

            var dt = mdDecoder.DeclaringType(m);
            if (
              !mdDecoder.IsStatic(m) &&
              !dt.Equals(prevType) &&
              !(mdDecoder.DerivesFrom(prevType, dt) && (mdDecoder.IsProtected(m) || mdDecoder.IsPublic(m)))
              )
            {
              this.mCastPreviousTo = OutputPrettyCS.TypeHelper.TypeFullName(mdDecoder, dt);
            }

            return true;
          }
          value = default(Type);
          this.resultType = value;
          return false;
        }

#if false
        public override Result Decode<Data, Result, PC, Type2, Method2>(PC label, ICodeQuery<PC, Local, Parameter, Method2, Field, Type2, Data, Result> query, Data data)
        {
          if (typeof(T).Equals(typeof(Method2)))
          {
            return query.Call(data, label, (Method2)(object)this.Element, true); // TODO: cleanup paths to just be expressions and keep track of virt flag
          }
          return query.Atomic(data, label);
        }
#endif
        public override Result Decode<Data, Result, Visitor, PC, Local2, Parameter2, Method2, Field2, Type2>(PC pc, Visitor visitor, Data data)
        {
          if (typeof(T).Equals(typeof(Field2)))
          {
            Field2 f = (Field2)(object)Element;
            if (this.isStatic)
            {
              return visitor.Ldsflda(pc, f, Unit.Value, data);
            }
            else
            {
              return visitor.Ldflda(pc, f, Unit.Value, Unit.Value, data);
            }
          }
          if (typeof(T).Equals(typeof(Local2)))
          {
            Local2 l = (Local2)(object)Element;
            return visitor.Ldloca(pc, l, Unit.Value, data);
          }
          if (typeof(T).Equals(typeof(Method2)))
          {
            Contract.Assume(!this.IsAddressOf);
            Method2 m = (Method2)(object)Element;
            var virt = this.Constructor.IsVirtualMethod;
            return visitor.Call(pc, m, false, virt, EmptyIndexable<Type2>.Empty, Unit.Value, EmptyIndexable<Unit>.Empty, data);
          }
          if (typeof(T).Equals(typeof(Parameter2)))
          {
            Parameter2 p = (Parameter2)(object)Element;
            return visitor.Ldarga(pc, p, false, Unit.Value, data);
          }
          throw new InvalidOperationException();
        }

        public virtual bool IsCallerVisible(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
        {
          if (typeof(T).Equals(typeof(Parameter))) return true;
          return false;
        }

        public override string ToString()
        {
          return AsString;
        }

        public override PathElement SubstituteElement(object from, object to)
        {
          if (this.Element.Equals(from))
          {
            var copy = (PathElement<T>)this.MemberwiseClone();
            copy.element = (T)to;
            return copy;
          }
          return this;
        }
      }

      /// <summary>
      /// This class differs from PathElement of Parameter in that it decodes to ldarg, not ldarga. We optimize
      /// the access paths to compress the ldarga,ldind into ldarg using this element.
      /// </summary>
      [Serializable]
      class ParameterPathElement : PathElement<Parameter>
      {
        public ParameterPathElement(Parameter p, string s, Constructor c, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
          : base(p, s, c)
        {
          Contract.Requires(s != null);

          Type ptype = mdDecoder.ParameterType(p);
          this.resultType = mdDecoder.ManagedPointer(ptype);

          if (mdDecoder.IsUnmanagedPointer(ptype))
            isUnmanagedPointer = true;
          else
            isUnmanagedPointer = false;

          if (mdDecoder.IsManagedPointer(ptype)
            /*|| mdDecoder.IsUnmanagedPointer(ptype)*/) // The unmanaged pointers are treated separately
          {
            isManagedPointer = true; // parameter is a pointer or a ref, so even its very node is a pointer
            if (mdDecoder.IsUnmanagedPointer(mdDecoder.ElementType(ptype))) // it is also an unmanaged pointer
              isUnmanagedPointer = true;
          }
          else
            isManagedPointer = false;
        }

        public override bool IsParameter
        {
          get
          {
            return true;
          }
        }

        [ContractVerification(false)]
        public override Result Decode<Data, Result, Visitor, PC, Local2, Parameter2, Method2, Field2, Type2>(PC pc, Visitor visitor, Data data)
        {
          Parameter2 p = (Parameter2)(object)Element;
          return visitor.Ldarg(pc, p, false, Unit.Value, data);
        }

        public override bool IsAddressOf
        {
          get
          {
            return false;
          }
        }

        /*private bool mbisManagedPointer;
        public override bool IsManagedPointer
        {
          get
          {
            return mbisManagedPointer;
          }
        }

        private bool mbisUnmanagedPointer;
        public override bool IsUnmanagedPointer
        {
          get
          {
            return mbisUnmanagedPointer;
          }
        }*/

        public override bool IsCallerVisible(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
        {
          return true;
        }
      }

      [Serializable]
      internal class MethodCallPathElement : PathElement<Method>
      {
        /// <summary>
        /// True if this property represents the actual property call. Otherwise, it represents the pseudofield address
        /// </summary>
        bool compressed;
        bool isGetter;
        bool isBooleanTyped;
        bool isModel;

        internal MethodCallPathElement(Method method, bool isGetter, bool isBooleanTyped, bool isStatic, bool isModel, string s, Constructor c, bool compress)
          : base(method, s, c)
        {
          Contract.Requires(s != null);

          this.compressed = compress;
          this.isGetter = isGetter;
          this.isBooleanTyped = isBooleanTyped;
          this.isStatic = isStatic;
          this.isModel = isModel;
        }

        public override bool IsAddressOf
        {
          get
          {
            return !compressed;
          }
        }

        public override bool IsMethodCall
        {
          get
          {
            return true;
          }
        }

        public override bool IsModel
        {
          get
          {
            return this.isModel;
          }
        }

        public override bool IsGetter
        {
          get
          {
            return this.isGetter;
          }
        }

        public override bool IsBooleanTyped
        {
          get
          {
            return this.isBooleanTyped;
          }
        }

      }

      [Serializable]
      class SpecialPathElement : PathElement<SpecialPathElementKind>
      {
        object type; // used for Deref nodes

        public SpecialPathElement(SpecialPathElementKind k, Constructor c)
          : base(k, k.ToString(), c)
        {
          mCastPreviousTo = "";
        }

        public override bool IsAddressOf
        {
          get
          {
            return false;
          }
        }

        public override bool IsDeref
        {
          get
          {
            return this.Element == SpecialPathElementKind.Deref;
          }
        }

        public override bool TryGetResultType<Type2>(out Type2 type)
        {
          if (this.type is Type2)
          {
            type = (Type2)this.type;
            return true;
          }
          type = default(Type2);
          return false;
        }

        internal override bool TrySetType(Type prevType, out Type result)
        {
          var mdDecoder = this.Constructor.mdDecoder;

          switch (this.Element)
          {
            case SpecialPathElementKind.Deref:
              if (mdDecoder.IsManagedPointer(prevType))
              {
                var type = mdDecoder.ElementType(prevType);
                this.type = type;
                result = type;
                return true;
              }
              if (mdDecoder.IsUnmanagedPointer(prevType))
              {
                var type = mdDecoder.ElementType(prevType);
                this.type = type;
                result = type;
                return true;
              }
              result = default(Type);
              return false;

            case SpecialPathElementKind.Length:
              // Mic: if it's not an array, cast it to array (string.Length does not use this particular case)
              if (!mdDecoder.IsArray(prevType) && !mdDecoder.System_String.Equals(prevType))
                mCastPreviousTo = "System.Array";
              else
                mCastPreviousTo = ""; // reset because the variable is somewhat shared
              result = mdDecoder.System_Int32;
              return true;

            case SpecialPathElementKind.WritableBytes:
              result = mdDecoder.System_Int32;
              return true;

            default:
              result = default(Type);
              return false;
          }
        }

        public override Result Decode<Data, Result, Visitor, PC, Local2, Parameter2, Method2, Field2, Type2>(PC pc, Visitor visitor, Data data)
        {
          switch (this.Element)
          {
            case SpecialPathElementKind.Deref:
              return visitor.Ldind(pc, (Type2)type, false, Unit.Value, Unit.Value, data);

            case SpecialPathElementKind.Length:
              return visitor.Ldlen(pc, Unit.Value, Unit.Value, data);

            case SpecialPathElementKind.WritableBytes:
              return visitor.Unary(pc, UnaryOperator.WritableBytes, false, true, Unit.Value, Unit.Value, data);

            default:
              throw new NotImplementedException("Unknown kind");
          }
        }
      }

      [Serializable]
      internal enum SpecialPathElementKind
      {
        WritableBytes,
        Length,
        Deref
      }

      public static bool IsRootedInParameter(FList<PathElement> path)
      {
        Contract.Requires(path != null);

        return path.Head is PathElement<Parameter>;
      }

      public static bool IsRootedInReturnValue(FList<PathElement> path)
      {
        Contract.Requires(path != null);
        return path.Head is ReturnValuePathElement;
      }

      #endregion

      #region Privates

      /// <summary>
      /// Copy Constructor
      /// </summary>
      private Domain(
        EGraph<Constructor, AbstractType> newEgraph,
        IFunctionalMap<ESymValue, Constructor> constantMap,
        IFunctionalSet<ESymValue> unmodifiedSinceEntry,
        IFunctionalSet<ESymValue> unmodifiedSinceEntryForFields,
        IFunctionalSet<ESymValue> modifiedAtCall,
        Domain from,
        Domain oldDomain)
      {
        this.egraph = newEgraph;
        this.Constructors = from.Constructors;
        this.parent = from.parent;
        this.BeginOldSavedStates = from.BeginOldSavedStates;
        this.constantLookup = constantMap;
        this.unmodifiedSinceEntry = unmodifiedSinceEntry;
        this.unmodifiedSinceEntryForFields = unmodifiedSinceEntryForFields;
        this.modifiedAtCall = modifiedAtCall;
        this.insideOld = from.insideOld;
        this.oldDomain = oldDomain;
        this.beginOldPC = from.beginOldPC;
      }

      private ESymValue Globals { get { return this.egraph.Root; } }


      public ESymValue Null
      {
        get
        {
          return this.egraph[this.Constructors.NullValue];
        }
      }

      public ESymValue Zero
      {
        get
        {
          return this.egraph[this.Constructors.ZeroValue];
        }
      }

      public ESymValue VoidAddr
      {
        get
        {
          return this.egraph[this.Constructors.VoidAddr];
        }
      }

      private void SetType(ESymValue sv, FlatDomain<Type> t)
      {
        AbstractType at = this.egraph[sv];
        if (!at.IsZero)
        {
          this.egraph[sv] = at.With(t);
        }
      }
      private void SetTypeIfUnknown(ESymValue sv, FlatDomain<Type> t)
      {
        AbstractType at = this.egraph[sv];
        if (!at.IsZero)
        {
          if (!at.Type.IsNormal || at.Type.Equals(this.mdDecoder.System_IntPtr) || at.Type.Equals(this.mdDecoder.System_UIntPtr))
          {
            this.egraph[sv] = at.With(t);
          }
        }
      }

      private ESymValue ConstantValue(object constant, Type t)
      {
        Constructor c = Constructors.ForConstant(constant, t);
        ESymValue cval = this.egraph.TryLookup(c);
        if (cval == null)
        {
          cval = this.egraph[c];
          SetType(cval, t);
          this.constantLookup = this.constantLookup.Add(cval, c);
        }
        return cval;
      }

      private ESymValue MethodPointer(Method method, Type t)
      {
        Constructor c = Constructors.ForMethod(method, t);
        ESymValue cval = this.egraph.TryLookup(c);
        if (cval == null)
        {
          cval = this.egraph[c];
          SetType(cval, t);
          this.constantLookup = this.constantLookup.Add(cval, c);
        }
        return cval;
      }

      private ESymValue TypeDefaultValue(Type t)
      {
        Constructor c = Constructors.ForTypeDefaultValue(t);
        return this.egraph[c];
      }

      internal bool IsMethodPointer(ESymValue sval, out Method method)
      {
        Constructor.Wrapper<Method> mcon = this.constantLookup[sval] as Constructor.Wrapper<Method>;
        if (mcon != null) { method = mcon.Value; return true; }
        method = default(Method);
        return false;
      }

      private ESymValue Address(Constructor v, Type t)
      {
        ESymValue addr = this.egraph.TryLookup(v);
        if (addr == null)
        {
          addr = this.egraph[v];
          SetType(addr, t);
        }
        return addr;
      }

      private ESymValue Address(Constructor v)
      {
        return this.egraph[v];
      }

      ESymValue Address(Temp t)
      {
        return Address(Constructors.For(t));
      }

      ESymValue Address(Parameter p)
      {
        Constructor loc = Constructors.For(p);
        ESymValue addr = this.egraph.TryLookup(loc);
        if (addr == null)
        {
          addr = this.egraph[loc];
          SetType(addr, mdDecoder.ManagedPointer(mdDecoder.ParameterType(p)));
        }
        return addr;
      }

      ESymValue Address(Local l)
      {
        Constructor loc = Constructors.For(l);
        ESymValue addr = this.egraph.TryLookup(loc);
        if (addr == null)
        {
          addr = this.egraph[loc];
          SetType(addr, mdDecoder.ManagedPointer(mdDecoder.LocalType(l)));
        }
        return addr;
      }

      private ESymValue TryAddress(Constructor v)
      {
        return this.egraph.TryLookup(v);
      }

      private ESymValue Value(ESymValue loc)
      {
        bool fresh;
        ESymValue result = this.egraph.LookupOrManifest(this.Constructors.ValueOf, loc, out fresh);
        if (fresh && this.IsUnmodified(loc))
        {
          // make sure to propagate unmodified property
          this.MakeUnmodified(result);
        }
        return result;
      }

      private ESymValue TryValue(ESymValue loc)
      {
        ESymValue result = this.egraph.TryLookup(this.Constructors.ValueOf, loc);
        //if (result != null && this.IsZero(result)) return this.Zero;
        return result;
      }

      /// <summary>
      /// Like TryValue, but checks if address is a struct address. In that case, it returns
      /// the address itself.
      /// </summary>
      private ESymValue TryCorrespondingValue(ESymValue loc)
      {
        if (IsStructAddress(this.egraph[loc])) return loc;
        return TryValue(loc);
      }

      private ESymValue Value(Parameter v)
      {
        return Value(Address(Constructors.For(v)));
      }

      private ESymValue Value(Temp v)
      {
        return Value(Address(Constructors.For(v)));
      }

      internal FList<PathElement> GetBestAccessPath(ESymValue sv, AccessPathFilter<Method, Type> filter, bool compress, bool allowLocal, bool preferLocal,
        Predicate<FList<PathElement>> additionalFilter = null)
      {
        Contract.Requires(filter != null);

        FList<PathElement> shortestParamPath = null;
        FList<PathElement> shortestLocalPath = null;
        FList<PathElement> backupPath = null;
        FList<PathElement> returnPath = null;

        foreach (var p in GetAccessPathsFiltered(sv, filter, compress))
        {
          if (p != null)
          {
            var path = p.Coerce<PathElementBase, PathElement>();
            if (additionalFilter != null && additionalFilter(path) == false) continue;
            if (path.Head is PathElement<Parameter>)
            {
              if (shortestParamPath == null || shortestParamPath.Length() > path.Length())
              {
                shortestParamPath = path;
              }
            }
            else if (/*Mic: change to the true meaning: if accessibility should not be tested from outside (was: !accessedFrom.IsValid)*/
              (filter.AllowLocal && allowLocal) // TODO MAF: changed meaning, review
              && path.Head is PathElement<Local>)
            {
              if (filter.AllowCompilerLocal || !path.Head.ToString().Contains("$"))
              {
                if (shortestLocalPath == null || shortestLocalPath.Length() > path.Length())
                {
                  shortestLocalPath = path;
                }
              }
            }
            else if (path.Head is PathElement<Field> && p.Head.Constructor.IsStatic)
            {
              // static field 
              if (backupPath == null || backupPath.Length() > path.Length())
              {
                backupPath = path;
              }
            }
            else if (path.Head is PathElement<Method> && p.Head.Constructor.IsStatic)
            {
              // static property 
              if (backupPath == null || backupPath.Length() > path.Length())
              {
                backupPath = path;
              }
            }
            else if (path.Head.IsReturnValue)
            {
              returnPath = path;
            }
          }
        }

        if (returnPath != null) { return returnPath; }
        if (preferLocal && shortestLocalPath != null) { return shortestLocalPath; }
        if (shortestParamPath != null) { return shortestParamPath; }
        // emit local path if we don't have visibility issues
        if (allowLocal && shortestLocalPath != null) { return shortestLocalPath; }
        if (backupPath != null) { return backupPath; }
        return null;
      }

      private bool TryPropagateTypeInfo(FList<PathElementBase> path, AccessPathFilter<Method, Type> filter, out FList<PathElementBase> result)
      {
        if (path == null)
        {
          result = null;
          return true;
        }

        var mdd = this.mdDecoder;

        Type headTypeRef;
        PathElementBase head = path.Head;
        if (filter != null && filter.AllowReturnValue && head.Constructor.IsTopOfStack(filter.LocalStackDepth))
        {
          if(mdd.System_Void.Equals(filter.ReturnValueType))
          {
            result = null;
            return false;
          }

          // replace it with Return value
          head = new ReturnValuePathElement(filter.ReturnValueType, mdd.Name(filter.ReturnValueType), head.Constructor);
          headTypeRef = mdd.ManagedPointer(filter.ReturnValueType);
        }
        else
        {
          if (!head.TrySetType(mdd.System_IntPtr, out headTypeRef))
          {
            result = null;
            return false;
          }
        }
        FList<PathElementBase> tail;
        if (!TryPropagateTypeInfoRecurse(path.Tail, headTypeRef, out tail))
        {
          result = null;
          return false;
        }

        // Compress parameter head ldarga,ldind into ldarg
        if (head.IsAddressOf && head is PathElement<Parameter> && tail != null && tail.Head.IsDeref)
        {
          var pe = (PathElement<Parameter>)head;
          Contract.Assume(pe.AsString != null, "follows from the object invariant of pe, which we do not assume here");
          result = tail.Tail.Cons(new ParameterPathElement(pe.Element, pe.AsString, pe.Constructor, mdd));
          return true;
        }
        else
        {
          if (head is ReturnValuePathElement && tail != null && tail.Head.IsDeref)
          {
            result = tail.Tail.Cons(head);
          }
          else
          {
            result = tail.Cons(head);
          }
          return true;
        }
      }

      private bool TryPropagateTypeInfoRecurse(FList<PathElementBase> path, Type prevType, out FList<PathElementBase> result)
      {
        if (path == null)
        {
          result = null;
          return true;
        }

        var head = path.Head;
        if (!head.TrySetType(prevType, out prevType))
        {
          result = null;
          return false;
        }

        FList<PathElementBase> recursive_result;
        if (TryPropagateTypeInfoRecurse(path.Tail, prevType, out recursive_result))
        {
          result = recursive_result.Cons(head);
          return true;
        }
        else
        {
          result = null;
          return false;
        }

      }

      private IEnumerable<FList<PathElementBase>> GetAccessPathsTyped(ESymValue sv, AccessPathFilter<Method, Type> filter, bool compress)
      {
        Set<ESymValue> visited = new Set<ESymValue>();

        foreach (var path in GetAccessPathsRaw(sv, null, visited, filter, compress))
        {
          FList<PathElementBase> result;
          if (TryPropagateTypeInfo(path, filter, out result))
          {
            yield return result;
          }
        }
      }

      internal IEnumerable<FList<PathElementBase>> GetAccessPathsFiltered(ESymValue sv, AccessPathFilter<Method, Type> filter, bool compress)
      {
        foreach (var path in GetAccessPathsTyped(sv, filter, compress))
        {
          if (PathIsVisibleAccordingToFilter(path, filter))
          {
            yield return path;
          }
        }
      }

      // Mic: check the existence of the elements in the fullpath
      private class ErrorMemberDoesNotExist : Exception { }

      /// <summary>
      /// Check for getters only, not setters
      /// Returns false if the property does not exist in the type (or base classes as protected)
      /// </summary>
      public bool CheckPropertyExists(Type t, string name, bool allowPrivate)
      {
        // for now treat t as unspecialized
        t = mdDecoder.Unspecialized(t);

        foreach (var prop in mdDecoder.Properties(t))
        {
          if (mdDecoder.Name(prop) == name)
          {
            Method getter;
            if (mdDecoder.HasGetter(prop, out getter))
            {
              if (!allowPrivate && mdDecoder.IsPrivate(getter))
                return false;
              return true; // found
            }
            else
              return false;
          }
        }

        // check in the base class, protected or public
        if (mdDecoder.HasBaseClass(t))
          return CheckPropertyExists(mdDecoder.BaseClass(t), name, false);
        else
          return false;
      }

      /// <summary>
      /// Returns false if the property does not exist in the type (or base classes as protected)
      /// </summary>
      public bool CheckPropertyExists(Type t, Property prop, bool allowPrivate)
      {
        // for now treat t as unspecialized
        t = mdDecoder.Unspecialized(t);

        foreach (var p in mdDecoder.Properties(t))
        {
          if (mdDecoder.Equal(prop, p))
            //if (mdDecoder.Name(p) == mdDecoder.Name(prop))
            return true; // found
        }

        // check in the base class, protected or public
        if (mdDecoder.HasBaseClass(t))
          return CheckPropertyExists(mdDecoder.BaseClass(t), prop, false);
        else
          return false;
      }

      /// <summary>
      /// Returns false if the method does not exist in the type (or base classes as protected)
      /// </summary>
      public bool CheckMethodExists(Type t, Method meth, bool allowPrivate)
      {
        // for now treat t as unspecialized
        t = mdDecoder.Unspecialized(t);

        foreach (var m in mdDecoder.Methods(t))
        {
          if (mdDecoder.Equal(m, meth))
            //if (mdDecoder.Name(meth) == mdDecoder.Name(m))
            return true; // found
        }

        // check in the base class, protected or public
        if (mdDecoder.HasBaseClass(t))
          return CheckMethodExists(mdDecoder.BaseClass(t), meth, false);
        else
          return false;
      }

      /// <summary>
      /// Returns false if the field does not exist in the type (or base classes as protected)
      /// </summary>
      public bool CheckFieldExists(Type t, Field field, bool allowPrivate)
      {
        // for now treat t as unspecialized
        t = mdDecoder.Unspecialized(t);

        foreach (var f in mdDecoder.Fields(t))
        {
          if (mdDecoder.Equal(f, field))
            //if (mdDecoder.Name(f) == mdDecoder.Name(field))
            return true; // found
        }

        // check in the base class, protected or public
        if (mdDecoder.HasBaseClass(t))
          return CheckFieldExists(mdDecoder.BaseClass(t), field, false);
        else
          return false;
      }

      /// <summary>
      /// Test for the CS1540 rule
      /// </summary>
      /// <param name="C">Class from which the call is made</param>
      /// <param name="ST">Type of the accessed object</param>
      /// <param name="DT">Declaring type of the field accessed in the accessed object</param>
      /// <param name="accessProtected">Allow or not to access protected members</param>
      /// <returns>Returns false if the error would occur.</returns>
      private bool CheckFor1540(Type C, Type ST, Type DT, bool accessProtected)
      {
        if (C == null || ST == null || DT == null)
          return true; // Type in another assembly, System...

        if (C.Equals(ST) && ST.Equals(DT))
          return true; // the field is in the declaring type, ok

        // Check for the CS1540
        if (accessProtected && mdDecoder.DerivesFrom(ST, DT))
        {
          // the field is accessed because of derivation and protected
          // Check if the owner is from the C hierarchy
          if (!mdDecoder.DerivesFrom(ST, C))
            return false;
        }

        return true;
      }

      /// <summary>
      /// Input path must be typed!!!!
      /// </summary>
      private bool PathIsVisibleAccordingToFilter(FList<PathElementBase> path, AccessPathFilter<Method, Type> filter)
      {
        if (path == null /*|| path.Length() == 0 */|| !filter.HasVisibilityMember)
          return true;

        Method mfrom = filter.VisibilityMember;
        Type declaringFrom = mdDecoder.DeclaringType(mfrom);

        Type t = default(Type);
        Type prev_type;
        bool first = true;
        for (; path != null; path = path.Tail, first = false)
        {
          prev_type = t;

          PathElement pelem = path.Head;

          //** Mic: NOTE:
          // I removed all the tests I made for the accessibility since it is no longer
          // necessary: the "CastTo" facility in PathElement allows to cast locally in
          // path output, so accesses are always guaranteed to be ok.

          //if (pelem is SpecialPathElement) // Deref, length, writteable bytes
          //{
          //  SpecialPathElement spe = pelem as SpecialPathElement;
          //  var spek = spe.Element;
          //  if (spek == SpecialPathElementKind.Length)
          //  {
          //    // Check if previous type has a length property
          //    // Mic: TODO: cf. above
          //    if (mdDecoder.IsArray(prev_type))
          //      continue; // accept

          //    if (prev_type != null && !prev_type.Equals(default(Type)))
          //    {
          //      if (!CheckPropertyExists(prev_type, "Length", true))
          //        return false;
          //    }
          //  }
          //  continue;
          //}

          if (!pelem.TryGetResultType(out t)) return true; // avoid problems downstream

          // Mic: HACK: 'while' because at some places, CCI reports a type "type@@"!?
          while (mdDecoder.IsManagedPointer(t))
            t = mdDecoder.ElementType(t); // we skip deref, so don't care about pointer or not

          if (pelem.ToString() == "this")
            continue; // always accessible

          if (pelem is PathElement<Method>)
          {
            PathElement<Method> pelem_meth = pelem as PathElement<Method>;
            Method m = pelem_meth.Element;

            // cf. note above
            //if (!first)
            //{
            //  if (!CheckMethodExists(prev_type, m, true))
            //    return false;
            //}

            if (!mdDecoder.IsVisibleFrom(m, declaringFrom))
              return false;

            if (!first)
            {
              Type C = mdDecoder.DeclaringType(mfrom);
              Type ST = prev_type;
              Type DT = mdDecoder.DeclaringType(m);

              if (!CheckFor1540(C, ST, DT, mdDecoder.IsProtected(m)))
                return false;
            }
          }
          else if (pelem is PathElement<Property>)
          {
            PathElement<Property> pelem_prop = pelem as PathElement<Property>;
            Property p = pelem_prop.Element;

            // cf. note above
            //if (!first)
            //{
            //  if (!CheckPropertyExists(prev_type, p, true))
            //    return false;
            //}

            Method m;
            if (!mdDecoder.HasGetter(p, out m))
              mdDecoder.HasSetter(p, out m);

            if (!mdDecoder.IsVisibleFrom(m, declaringFrom))
              return false;

            if (!first)
            {
              Type C = mdDecoder.DeclaringType(mfrom);
              Type ST = prev_type;
              Type DT = mdDecoder.DeclaringType(m);

              if (!CheckFor1540(C, ST, DT, mdDecoder.IsProtected(m)))
                return false;
            }
          }
          else if (pelem is PathElement<Field>)
          {
            PathElement<Field> pelem_f = pelem as PathElement<Field>;
            Field f = pelem_f.Element;

            // cf. note above
            //if (!first)
            //{
            //  if (!CheckFieldExists(prev_type, f, true))
            //    return false;
            //}

            if (!mdDecoder.IsVisibleFrom(f, declaringFrom))
              return false;

            if (filter.AvoidCompilerGenerated && (mdDecoder.IsCompilerGenerated(f) || mdDecoder.IsCompilerGenerated(mdDecoder.DeclaringType(f))))
            {
              return false;
            }

            if (!first)
            {
              Type C = mdDecoder.DeclaringType(mfrom);
              Type ST = prev_type;
              Type DT = mdDecoder.DeclaringType(f);

              if (!CheckFor1540(C, ST, DT, mdDecoder.IsProtected(f)))
                return false;
            }
          }
        }

        return true;
      }

      /// <summary>
      /// Completes the path back to a local/parameter if possible.
      /// </summary>
      IEnumerable<FList<PathElementBase>> GetAccessPathsRaw(ESymValue sv, AccessPathFilter<Method, Type> filter, bool compress)
      {
        Set<ESymValue> visited = new Set<ESymValue>();

        return GetAccessPathsRaw(sv, null, visited, filter, compress);
      }

      IEnumerable<FList<PathElementBase>> GetAccessPathsRaw(ESymValue sv, FList<PathElementBase> path, Set<ESymValue> visited, AccessPathFilter<Method, Type> filter, bool compress)
      {
        if (sv == this.egraph.Root)
        {
          yield return path;
          yield break;
        }
        if (visited.Contains(sv))
        {
          // already visited 
          yield break;
        }
        visited.Add(sv);
        foreach (EGraphTerm<Constructor> et in this.egraph.EqTerms(sv))
        {
          if (et.Function is Constructor.Wrapper<object>) continue; // ignore program constants (0x0, 1, etc.)
          PathElementBase next = et.Function.ToPathElement(compress);

          if (next == null // element we don't understand ($array element e.g.)
              || filter.FilterOutPathElement(et.Function))
#if false
            || (accessedFrom.IsValid && accessedFrom.GetOption == AccessPathFilter<Method>.Option.FROM_CALLING_METHOD && !et.Function.IsAsVisibleAs(accessedFrom.Value)) // don't want (not as visible as the caller)
            || (accessedFrom.IsValid && accessedFrom.GetOption == AccessPathFilter<Method>.Option.FROM_INSIDE_METHOD && !et.Function.IsVisibleFrom (accessedFrom.Value))) // don't want, not as visible as the context in which it is used
#endif
          {
            continue;
          }
          FList<PathElementBase> newPath;
          // Filter out extra deref on method calls to properties
          if (path != null && compress && next is PathElement<Method> && path.Head.IsDeref)
          {
            newPath = path.Tail.Cons(next); // remove extra deref
          }
          else
          {
            newPath = path.Cons(next);
          }

          foreach (FList<PathElementBase> fullpath in GetAccessPathsRaw(et.Args[0], newPath, visited, filter, compress))
          {
#if false
// MAF TODO move this to separate Filtered method
            // Double check if the path is valid, because the previous calls to IsVisibleFrom or IsAsVisibleAs only take the last path element into account
            // Here, the entire path is checked (check for the CS1540 error, and also for the issue with contracts inferred after wild casts)
            if (accessedFrom.GetOption != AccessPathFilter<Method>.Option.NO_FILTER)
            {
              if (PathVisibleFrom(fullpath, accessedFrom.Value))
                yield return fullpath;
            }
            else
#endif
            yield return fullpath;
          }
        }
      }


      void HavocIfStruct(ESymValue address)
      {
        AbstractType prevType = this.egraph[address];
        if (prevType.IsBottom || (prevType.IsNormal && mdDecoder.IsStruct(prevType.Value)))
        {
          Havoc(address);
        }
      }

      /// <summary>
      /// Don't forget to havoc array elements too and struct ids
      /// </summary>
      void HavocMutableFields(Constructor accessedVia, ESymValue address, ref IFunctionalSet<ESymValue> havoced)
      {
        Contract.Ensures(havoced != null);

        HavocFields(accessedVia, address, ref havoced, false);
      }

      /// <summary>
      /// Used on this in constructors
      /// </summary>
      void HavocAllFields(ESymValue address, ref IFunctionalSet<ESymValue> havoced)
      {
        HavocFields(null, address, ref havoced, true);
      }

      void HavocFields(ESymValue address, IEnumerable<Field> fields, ref IFunctionalSet<ESymValue> havoced)
      {
        foreach (var field in fields)
        {
          HavocConstructor(null, address, Constructors.For(field), ref havoced, false);
        }
      }

      /// <summary>
      /// </summary>
      /// <param name="address"></param>
      /// <param name="havoced"></param>
      /// <param name="havocImmutable"></param>
      void HavocFields(Constructor accessedVia, ESymValue address, ref IFunctionalSet<ESymValue> havoced, bool havocImmutable)
      {
        Contract.Ensures(havoced != null);

        if (havoced.Contains(address)) return; // no need to repeatedly havoc
        havoced = havoced.Add(address);
        this.MakeTotallyModified(address);
        foreach (Constructor c in this.egraph.Functions(address))
        {
          HavocConstructor(accessedVia, address, c, ref havoced, havocImmutable);
        }
      }

      private void HavocConstructor(Constructor accessedVia, ESymValue address, Constructor c, ref IFunctionalSet<ESymValue> havoced, bool havocImmutable)
      {
        Contract.Ensures(havoced != null);

        if (c == Constructors.ValueOf)
        {
          // retain as old value
          this.egraph[Constructors.OldValueOf, address] = this.egraph[c, address];
        }
        if (c == Constructors.ValueOf ||
            c == Constructors.ObjectVersion ||
            c == Constructors.StructId)
        {
          this.egraph.Eliminate(c, address);
          havoced = havoced.Add(address);
          return;
        }

        Constructor.Wrapper<Field> field = c as Constructor.Wrapper<Field>;
        if (field != null && field != accessedVia)
        {
          if (!havocImmutable && mdDecoder.IsReadonly(field.Value))
            return;
          // havoc the value/nested struct
          HavocFields(accessedVia, this.egraph[c, address], ref havoced, havocImmutable);
          return;
        }
        Constructor.Wrapper<Method> method = c as Constructor.Wrapper<Method>;
        if (method != null && method != accessedVia)
        {
          // havoc the value/nested struct
          HavocFields(accessedVia, this.egraph[c, address], ref havoced, havocImmutable);
          return;
        }
      }


      void HavocPseudoFields(ESymValue address)
      {
        IFunctionalSet<ESymValue> havoced = FunctionalSet<ESymValue>.Empty(ESymValue.GetUniqueKey);
        HavocPseudoFields((Constructor)null, address, ref havoced);
      }

      void HavocPseudoFields(Constructor except, ESymValue address, ref IFunctionalSet<ESymValue> havoced)
      {
        Contract.Requires(havoced != null);
        Contract.Ensures(havoced != null);

        if (havoced.Contains(address)) return; // no need to repeatedly havoc
        havoced = havoced.Add(address);

        // remove object from unmodified set
        if (this.IsUnmodified(address))
        {
          this.MakeModified(address);
          this.RetainForUnmodifiedFields(address);
        }

        // havoc struct/object Id
        this.egraph.Eliminate(Constructors.ObjectVersion, address);
        this.egraph.Eliminate(Constructors.StructId, address);

        foreach (Constructor c in this.egraph.Functions(address))
        {
          if (c == except) continue;
          Constructor.Wrapper<Method> method = c as Constructor.Wrapper<Method>;
          if (method != null)
          {
            // havoc the value/nested struct
            HavocMutableFields(except, this.egraph[c, address], ref havoced);
            continue;
          }
        }
      }

      void HavocPseudoFields(IEnumerable<Method> getters, ESymValue address)
      {
        IFunctionalSet<ESymValue> havoced = FunctionalSet<ESymValue>.Empty(ESymValue.GetUniqueKey);
        HavocPseudoFields((Constructor)null, getters, address, ref havoced);
      }

      void HavocPseudoFields(Constructor except, IEnumerable<Method> getters, ESymValue address, ref IFunctionalSet<ESymValue> havoced)
      {
        Contract.Requires(getters != null);

        if (havoced.Contains(address)) return; // no need to repeatedly havoc
        havoced = havoced.Add(address);

        // remove object from unmodified set
        if (this.IsUnmodified(address))
        {
          this.MakeModified(address);
          this.RetainForUnmodifiedFields(address);
        }

        // havoc struct/object Id
        this.egraph.Eliminate(Constructors.ObjectVersion, address);
        this.egraph.Eliminate(Constructors.StructId, address);

        foreach (var getter in getters)
        {
          // havoc the value/nested struct
          HavocMutableFields(except, this.egraph[Constructors.For(getter), address], ref havoced);
        }
      }

      void HavocModelProperties(ESymValue address, ref IFunctionalSet<ESymValue> havoced)
      {
        if (havoced.Contains(address)) return; // no need to repeatedly havoc
        havoced = havoced.Add(address);

        // remove object from unmodified set
        if (this.IsUnmodified(address))
        {
          this.MakeModified(address);
          this.RetainForUnmodifiedFields(address);
        }

#if false
        // havoc struct/object Id
        this.egraph.Eliminate(Constructors.ObjectVersion, address);
        this.egraph.Eliminate(Constructors.StructId, address);
#endif

        foreach (Constructor c in this.egraph.Functions(address))
        {
          Constructor.Wrapper<Method> method = c as Constructor.Wrapper<Method>;
          if (method != null && this.parent.contractDecoder.IsModel(method.Value))
          {
            // havoc the value/nested struct
            HavocMutableFields(null, this.egraph[c, address], ref havoced);
            continue;
          }
        }
      }


      void Havoc(ESymValue address)
      {
        this.egraph.EliminateAll(address);
      }

      void Havoc(Temp t)
      {
        Havoc(Address(t));
      }

      /// <summary>
      /// Assume given value is true/false
      /// </summary>
      Domain Assume(Temp t, bool truth)
      {
        return Assume(this.Value(t), truth);
      }

      Domain Assume(ESymValue eSymValue, bool polarity)
      {
        Contract.Ensures(Contract.Result<Domain>() != null);

        if (polarity == false)
        {
          if (this.IsNonZero(eSymValue))
          {
            // contradiction
            return Bottom;
          }
          // assume esymvalue to be zero
          this.egraph[eSymValue] = this.egraph[eSymValue].ButZero;
          // recurse
          foreach (EGraphTerm<Constructor> eterm in this.egraph.EqTerms(eSymValue))
          {
            if (eterm.Function == Constructors.UnaryNot)
            {
              return Assume(eterm.Args[0], true);
            }
            if (eterm.Function == Constructors.NeZero)
            {
              return Assume(eterm.Args[0], false);
            }
          }
        }
        else
        {
          // assume esymvalue to be non-zero.
          if (this.egraph[eSymValue].IsZero)
          {
            // contradiction
            return Bottom;
          }
          // recurse
          foreach (EGraphTerm<Constructor> eterm in this.egraph.EqTerms(eSymValue))
          {
            if (eterm.Function == Constructors.UnaryNot)
            {
              return Assume(eterm.Args[0], false);
            }
            if (eterm.Function == Constructors.NeZero)
            {
              return Assume(eterm.Args[0], true);
            }
          }
        }
        return this;
      }

      private bool IsNonZero(ESymValue eSymValue)
      {
        foreach (var eterm in this.egraph.EqTerms(eSymValue))
        {
          Constructor.Wrapper<object> cons = eterm.Function as Constructor.Wrapper<object>;
          if (cons != null)
          {
            // grab constant
            if (cons.Value is int)
            {
              int value = (int)cons.Value;
              if (value != 0) return true;
            }
          }
        }
        return false;
      }


      void AssignNull(Temp t)
      {
        AssignNull(Address(Constructors.For(t)));
      }

      void AssignNull(ESymValue addr)
      {
        Havoc(addr);
        this.egraph[Constructors.ValueOf, addr] = Null;
      }

      void AssignZero(ESymValue addr)
      {
        this.egraph[Constructors.ValueOf, addr] = Zero;
      }

      void AssignZeroEquivalent(ESymValue dest, Type t)
      {
        if (mdDecoder.IsReferenceConstrained(t))
        {
          AssignNull(dest);
        }
        else if (mdDecoder.HasValueRepresentation(t))
        {
          AssignZero(dest);
        }
        // TODO initialize struct fields?
      }

      internal bool IsStructAddress(AbstractType ltype)
      {
        if (!ltype.IsNormal) return false;
        Type type = ltype.Value;
        if (mdDecoder.IsManagedPointer(type) || mdDecoder.IsUnmanagedPointer(type))
        {
          type = mdDecoder.ElementType(type);
          return !mdDecoder.HasValueRepresentation(type);
        }
        return false;
      }
      private FlatDomain<Type> TargetType(FlatDomain<Type> addrType)
      {
        if (!addrType.IsNormal) return addrType;
        Type type = addrType.Value;
        if (mdDecoder.IsManagedPointer(type) || mdDecoder.IsUnmanagedPointer(type))
        {
          return mdDecoder.ElementType(type);
        }
        return FlatDomain<Type>.TopValue;
      }
      bool IsStructWithFields(FlatDomain<Type> valueType)
      {
        if (!valueType.IsNormal) return false;
        Type type = valueType.Value;
        return !mdDecoder.HasValueRepresentation(type);
      }

      private void Assign(ESymValue address, FlatDomain<Type> addrType)
      {
        Havoc(address);
        SetType(address, addrType);
        FlatDomain<Type> targetType = TargetType(addrType);
        if (!IsStructWithFields(targetType))
        {
          ESymValue fresh = egraph.FreshSymbol();
          SetType(fresh, targetType);
          this.egraph[Constructors.ValueOf, address] = fresh;
          // if typeOfValue is an array or string, we need to possibly manifest the length
          if (targetType.IsNormal)
          {
            if (NeedsArrayLengthManifested(targetType.Value))
            {
              this.ManifestArrayLength(fresh);
            }
            else if (this.mdDecoder.IsManagedPointer(targetType.Value) || this.mdDecoder.IsUnmanagedPointer(targetType.Value))
            {
              this.ManifestWritableBytes(fresh);
            }
          }

        }
      }


      bool NeedsArrayLengthManifested(Type type)
      {
        return this.mdDecoder.IsArray(type) || this.mdDecoder.System_String.Equals(type); // || this.IsIEnumerable(type);
      }

      TypeCache IEnumerableType = new TypeCache("System.Collections.IEnumerable");
      TypeCache IEnumerable1Type = new TypeCache("System.Collections.Generic.IEnumerable`1");

      private bool IsIEnumerable(Type type)
      {
        Type ienumerable;
        if (IEnumerable1Type.TryGet(this.mdDecoder, out ienumerable))
        {
          if (mdDecoder.DerivesFrom(type, ienumerable)) return true;
        }
        if (IEnumerableType.TryGet(this.mdDecoder, out ienumerable))
        {
          if (mdDecoder.DerivesFrom(type, ienumerable)) return true;
        }
        return false;
      }

      public bool DerivesFromIEnumerable(Type t)
      {
        if (IsIEnumerable(t)) return true;
        foreach (var intf in mdDecoder.Interfaces(t))
        {
          if (IsIEnumerable(intf)) return true;
        }
        return false;
      }

      private void AssignValue(ESymValue address, FlatDomain<Type> typeOfValue, Type fromType)
      {
        Havoc(address);
        SetType(address, (typeOfValue.IsNormal) ? mdDecoder.ManagedPointer(typeOfValue.Value) : typeOfValue);
        if (!IsStructWithFields(typeOfValue))
        {
          ESymValue fresh = egraph.FreshSymbol();
          SetType(fresh, typeOfValue);
          this.egraph[Constructors.ValueOf, address] = fresh;
          // if typeOfValue is an array, we need to possibly manifest the length
          if (typeOfValue.IsNormal)
          {
            if (NeedsArrayLengthManifested(typeOfValue.Value))
            {
              this.ManifestArrayLength(fresh);
            }
            if (!mdDecoder.IsArray(typeOfValue.Value) && DerivesFromIEnumerable(typeOfValue.Value))
            {
              this.ManifestModel(fresh, typeOfValue.Value, fromType: fromType);
            }

            else if (this.mdDecoder.IsManagedPointer(typeOfValue.Value) || this.mdDecoder.IsUnmanagedPointer(typeOfValue.Value))
            {
              this.ManifestWritableBytes(fresh);
            }
          }
        }
      }


      private void ManifestModel(ESymValue value, Type type, Type fromType)
      {
        ManifestModel(value, type, false, fromType: fromType);
      }
      private void ManifestModel(ESymValue value, Type type, bool isArray, Type fromType)
      {
        // First try to find model property in given type. If it is an array type, synthesize one

        ManifestModelProperties(value, type, isArray, fromType: fromType);
        if (this.mdDecoder.IsInterface(type))
        // [MAF] 
        // Types must explicitly implement the model for IEnumerable in mscorlib
        // Right now, this works only for types defined in mscorlib.Contracts. TODO... fix for all types.
        {
          foreach (var intf in mdDecoder.Interfaces(type))
          {
            ManifestModelProperties(value, intf, isArray, fromType: fromType);
          }
        }

        if (mdDecoder.IsArray(type))
        {
          // arrays are magically implementing IEnumerable<T>
          foreach (var intf in mdDecoder.Interfaces(type))
          {
            if (this.IsIEnumerable(intf))
            {
              ManifestModel(value, intf, true, fromType: fromType);
            }
          }
        }
      }

      private void ManifestModelProperties(ESymValue value, Type type, bool isArray, Type fromType)
      {
        foreach (var modelMethod in parent.contractDecoder.ModelMethods(type))
        {
          // manifest the property
          Type propertyType;
          ESymValue propAddr = this.PseudoFieldAddress(value, modelMethod, out propertyType, false, fromType: fromType);
          ESymValue modelValue;
          if (isArray)
          {
            // model value is the actual array
            this.egraph[Constructors.ValueOf, propAddr] = value;
            modelValue = value;
          }
          else
          {
            modelValue = this.Value(propAddr); // manifest value
          }
          if (mdDecoder.IsManagedPointer(propertyType) && mdDecoder.IsArray(mdDecoder.ElementType(propertyType)))
          {
            ManifestArrayLength(modelValue);
          }
        }
        return;

      }

      /// <summary>
      /// Create a fresh value, but retain nullness and WritableBytes if possible
      /// </summary>
      private void AssignValueAndNullnessAtConv_IU(ESymValue address, bool unsigned, AbstractType typeOfValueAndNullness, ESymValue sourceValue)
      {
        Havoc(address);
        FlatDomain<Type> typeOfValue = typeOfValueAndNullness.Type;
        if (!IsStructWithFields(typeOfValue))
        {
          ESymValue fresh = egraph.FreshSymbol();
          // fix up type if source is string, then dest is char*, otherwise, use IntPtr,UIntPtr
          if (typeOfValue.IsNormal)
          {
            if (this.mdDecoder.Equal(typeOfValue.Value, this.mdDecoder.System_String))
            {
              typeOfValueAndNullness = new AbstractType(this.mdDecoder.UnmanagedPointer(this.mdDecoder.System_Char), typeOfValueAndNullness.IsZero);
              typeOfValue = typeOfValueAndNullness.Type;
            }
            else
            {
              if (unsigned)
              {
                typeOfValueAndNullness = new AbstractType(this.mdDecoder.System_UIntPtr, typeOfValueAndNullness.IsZero);
              }
              else
              {
                typeOfValueAndNullness = new AbstractType(this.mdDecoder.System_IntPtr, typeOfValueAndNullness.IsZero);
              }
            }
          }
          SetType(address, (typeOfValue.IsNormal) ? mdDecoder.ManagedPointer(typeOfValue.Value) : typeOfValue);
          this.egraph[fresh] = typeOfValueAndNullness;
          this.egraph[Constructors.ValueOf, address] = fresh;
          // retain WritableBytes if present in source
          ESymValue sourceWritableBytes = this.egraph.TryLookup(this.Constructors.WritableBytes, sourceValue);
          if (sourceWritableBytes != null)
          {
            this.egraph[this.Constructors.WritableBytes, fresh] = sourceWritableBytes;
          }
          else
          {
            // manifest if necessary
            if (typeOfValue.IsNormal)
            {
              if (this.mdDecoder.IsManagedPointer(typeOfValue.Value) || this.mdDecoder.IsUnmanagedPointer(typeOfValue.Value))
              {
                this.ManifestWritableBytes(fresh);
              }
            }
            //if (this.mdDecoder.IsArray(typeOfValue.Value) || mdDecoder.Equal(typeOfValue.Value, mdDecoder.System_String))
            //{
            //              this.ManifestArrayLength(fresh);
            //}
          }
        }
        else
        {
          SetType(address, (typeOfValue.IsNormal) ? mdDecoder.ManagedPointer(typeOfValue.Value) : typeOfValue);
        }
      }

      void AssignConst(Temp dest, Type typeOfConst, object constant)
      {
        ESymValue addr = Address(Constructors.For(dest));
        AssignConst(addr, typeOfConst, constant);
      }

      void AssignConst(ESymValue addr, Type typeOfConst, object constant)
      {
        ESymValue cval = ConstantValue(constant, typeOfConst);
        SetType(addr, mdDecoder.ManagedPointer(typeOfConst));
        this.egraph[Constructors.ValueOf, addr] = cval;
        string stringConst = constant as String;
        if (stringConst != null)
        {
          this.egraph[Constructors.Length, cval] = ConstantValue(stringConst.Length, this.mdDecoder.System_Int32);
        }
      }

      void Assign(Parameter dest, Type typeOfValue, Type fromType)
      {
        ESymValue addr = Address(Constructors.For(dest));
        AssignValue(addr, typeOfValue, fromType: fromType);
      }

      void CopyParameterIntoShadow(Parameter p)
      {
        ESymValue shadowAddr = Address(Constructors.For("$paramShadow_" + this.mdDecoder.Name(p)));
        CopyValue(shadowAddr, Address(Constructors.For(p)));
      }

      public ESymValue OldValueAddress(Parameter p)
      {
        return Address(Constructors.For("$paramShadow_" + this.mdDecoder.Name(p)));
      }

      void AssignTokenValue<T>(Temp dest, Type typeOfToken, T token)
      {
        var addr = Address(dest);
        ESymValue cval = ConstantValue((object)token, typeOfToken);
        SetType(addr, mdDecoder.ManagedPointer(typeOfToken));
        this.egraph[Constructors.StructId, addr] = cval;
      }

      void AssignMethodPointer(Temp dest, Type uintPtr, Method method)
      {
        var addr = Address(dest);
        ESymValue cval = MethodPointer(method, uintPtr);
        SetType(addr, mdDecoder.ManagedPointer(uintPtr));
        this.egraph[Constructors.ValueOf, addr] = cval;
      }

      void AssignValue(Temp dest, FlatDomain<Type> typeOfValue, Type fromType)
      {
        ESymValue addr = Address(Constructors.For(dest));
        AssignValue(addr, typeOfValue, fromType: fromType);
      }

      /// <summary>
      /// Same as AssignValue, but leave behind a breadcrumb on result value to indicate
      /// that we materialized it.
      /// </summary>
      void AssignReturnValue(Temp dest, Type typeOfValue, Type fromType)
      {
        AssignValue(dest, typeOfValue, fromType: fromType);
#if false
        // F: I did it
                MaterializeAccordingToType(Address(dest), mdDecoder.ManagedPointer(typeOfValue), -1, Constructors.ResultOfCall, fromType, overrideFromType:true);
#endif
        MaterializeAccordingToType(Address(dest), mdDecoder.ManagedPointer(typeOfValue), 0, Constructors.ResultOfCall, fromType, overrideFromType: false);
      }

      void AssignValueAndNullnessAtConv_IU(Temp dest, Temp source, bool unsigned)
      {
        AbstractType typeOfValueAndNullness = CurrentType(source);
        ESymValue addr = Address(Constructors.For(dest));
        AssignValueAndNullnessAtConv_IU(addr, unsigned, typeOfValueAndNullness, Value(source));
      }

      void AssignSpecialUnary(Temp dest, Constructor op, Temp source, FlatDomain<Type> typeOfValue, Type fromType)
      {
        ESymValue result = GetSpecialUnary(op, source, typeOfValue);
        AssignSpecialUnary(dest, result, typeOfValue, fromType: fromType);
      }

      void AssignSpecialUnary(Temp dest, ESymValue result, FlatDomain<Type> typeOfValue, Type fromType)
      {
        ESymValue destaddr = this.Address(dest);
        AssignValue(destaddr, typeOfValue, fromType: fromType);
        this.egraph[Constructors.ValueOf, destaddr] = result;
      }

      ESymValue GetSpecialUnary(Constructor op, Temp source, FlatDomain<Type> type)
      {
        ESymValue src = this.Value(source);
        var result = this.egraph[op, src];
        SetType(result, type);
        return result;
      }

      ESymValue GetIsInstValue(Type t, Temp source)
      {
        ESymValue src = this.Value(source);
        ESymValue tval = ConstantValue((object)t, mdDecoder.System_Type);
        var result = this.egraph[Constructors.IsInst, src, tval];
        SetType(result, t);
        return result;
      }

      void AssignIsInst(Type t, Temp source, Temp dest, Type fromType)
      {
        var isInstVal = GetIsInstValue(t, source);
        ESymValue destaddr = this.Address(dest);
        AssignValue(destaddr, t, fromType: fromType);
        this.egraph[Constructors.ValueOf, destaddr] = isInstVal;
      }

      void AssignTop(Temp dest)
      {
        AssignValue(dest, mdDecoder.System_Void, default(Type)); // should not materialize anything in this case.
      }

      AbstractType CurrentType(ESymValue address)
      {
        return this.egraph[address];
      }

      AbstractType CurrentType(Temp t)
      {
        return CurrentType(Value(Address(this.Constructors.For(t))));
      }

      ESymValue LookupAddressAndManifestType(ESymValue sv, Constructor c, Type/*!*/ type, out bool fresh)
      {
        ESymValue addr = egraph.TryLookup(c, sv);
        if (addr != null)
        {
          fresh = false;
          return addr;
        }
        // manifest
        fresh = true;
        addr = egraph[c, sv];
        SetType(addr, type);
        return addr;
      }

      public void AssignPureUnary(int dest, UnaryOperator op, FlatDomain<Type> typeOpt, int arg)
      {
        var c = this.Constructors.For(op);
        Type type;
        if (typeOpt.IsNormal)
        {
          type = typeOpt.Value;
        }
        else
        {
          type = mdDecoder.System_Int32;
        }
        var result = this.egraph.TryLookup(c, Value(arg));
        if (result == null)
        {
          result = this.egraph[c, Value(arg)];
          this.egraph[result] = AbstractType.TopValue.With(type);
        }
        var destAddr = Address(dest);
        SetType(destAddr, mdDecoder.ManagedPointer(type));
        this.egraph[Constructors.ValueOf, destAddr] = result;
      }

      public void AssignPureBinary(int dest, BinaryOperator op, FlatDomain<Type> typeOpt, int arg1, int arg2)
      {
        var args = new ESymValue[] { Value(arg1), Value(arg2) };
        var c = this.Constructors.For(op);
        bool fresh;
        Type type;
        if (typeOpt.IsNormal)
        {
          type = typeOpt.Value;
        }
        else
        {
          type = mdDecoder.System_Int32;
        }
        var result = LookupAddressAndManifestType(args, c, type, out fresh);
        var destAddr = Address(dest);
        SetType(destAddr, mdDecoder.ManagedPointer(type));
        this.egraph[Constructors.ValueOf, destAddr] = result;
        if (this.mdDecoder.IsUnmanagedPointer(type))
        {
          this.ManifestWritableBytes(result);
        }
      }


      ESymValue LookupAddressAndManifestType(ESymValue[] args, Constructor c, Type/*!*/ type, out bool fresh)
      {
        ESymValue addr = egraph.TryLookup(c, args);
        if (addr != null)
        {
          fresh = false;
          return addr;
        }
        // manifest
        fresh = true;
        Contract.Assume(args.Length > 1);
        addr = egraph[c, args];
        SetType(addr, type);
        return addr;
      }

      private void CopyValue(ESymValue destAddr, ESymValue srcAddr)
      {
        AbstractType abstype = this.egraph[srcAddr];
        CopyValue(destAddr, srcAddr, abstype.Type);
      }

      private void CopyValue(ESymValue destAddr, ESymValue srcAddr, FlatDomain<Type> addrType)
      {
        CopyValue(destAddr, srcAddr, addrType, true, false);
      }

      private void CopyOldValue(APC pc, Temp dest, Temp source, Domain target, bool atEndOld)
      {
        CopyOldValue(pc, target.Address(dest), this.Address(source), target, atEndOld);
      }

      private void CopyOldValue(APC pc, ESymValue destAddr, ESymValue srcAddr, Domain target, bool atEndOld)
      {
        AbstractType abstype = this.egraph[srcAddr];
        CopyOldValue(pc, destAddr, srcAddr, abstype.Type, target, atEndOld);
      }

      private void CopyOldValue(APC pc, ESymValue destAddr, ESymValue srcAddr, FlatDomain<Type> addrType, Domain target, bool atEndOld)
      {
        CopyOldValue(pc, destAddr, srcAddr, addrType, true, atEndOld, target);
      }

      private void CopyValueAndCast(ESymValue destAddr, ESymValue srcAddr, FlatDomain<Type> addrType)
      {
        CopyValue(destAddr, srcAddr, addrType, true, true);
      }

      private void CopyValue(ESymValue destAddr, ESymValue srcAddr, FlatDomain<Type> addrType, bool setTargetAddrType, bool cast)
      {
        this.MakeTotallyModified(destAddr);
        if (destAddr != srcAddr) { HavocIfStruct(destAddr); }
        if (setTargetAddrType) SetType(destAddr, addrType);
        SetValue(destAddr, srcAddr, addrType, cast);
      }

      private void SetValue(ESymValue destAddr, ESymValue srcAddr, FlatDomain<Type> addrType, bool cast)
      {
        FlatDomain<Type> elementType = TargetType(addrType);
        if (IsStructWithFields(elementType))
        {
          CopyStructValue(destAddr, srcAddr, elementType.Value);
        }
        else
        {
          CopyPrimValue(destAddr, srcAddr, cast, elementType);
        }
      }

      private void CopyPrimValue(ESymValue destAddr, ESymValue srcAddr, bool cast, FlatDomain<Type> elementType)
      {
        ESymValue svalue = this.egraph[Constructors.ValueOf, srcAddr];
        if (cast)
        {
          SetType(svalue, elementType);
        }
        else
        {
          SetTypeIfUnknown(svalue, elementType);
        }
        if (elementType.IsNormal)
        {
          if (NeedsArrayLengthManifested(elementType.Value))
          {
            // manifest array length
            this.ManifestArrayLength(svalue);
          }
          else if (this.mdDecoder.IsManagedPointer(elementType.Value) || this.mdDecoder.IsUnmanagedPointer(elementType.Value))
          {
            this.ManifestWritableBytes(svalue);
          }
        }
        this.egraph[Constructors.ValueOf, destAddr] = svalue;
      }

      private void CopyOldValue(APC pc, ESymValue destAddr, ESymValue srcAddr, FlatDomain<Type> addrType, bool setTargetAddrType, bool atEndOld, Domain target)
      {
        target.MakeTotallyModified(destAddr);
        if (destAddr != srcAddr) { target.HavocIfStruct(destAddr); }
        if (setTargetAddrType) target.SetType(destAddr, addrType);
        FlatDomain<Type> elementType = TargetType(addrType);
        if (IsStructWithFields(elementType))
        {
          CopyOldStructValue(pc, destAddr, srcAddr, elementType.Value, target, atEndOld);
        }
        else if (atEndOld && elementType.IsNormal && mdDecoder.IsManagedPointer(elementType.Value))
        {
          // special case. Copying @T to @T, this usually means that we need to make a deep copy
          // recurse on both sides.
          srcAddr = this.TryValue(srcAddr);
          if (srcAddr == null) return;
          destAddr = target.Value(destAddr);
          CopyOldValue(pc, destAddr, srcAddr, elementType, setTargetAddrType, atEndOld, target);
        }
        else
        {
          ESymValue svalue = this.egraph.TryLookup(Constructors.ValueOf, srcAddr);
          if (svalue == null)
          {
            if (this.egraph.IsConstant)
            {
              return;
            }
            svalue = this.egraph[Constructors.ValueOf, srcAddr]; // manifest
          }

          CopyOldValueToDest(pc, destAddr, svalue, addrType, target);
        }
      }

      private void CopyOldValueToDest(APC pc, ESymValue destAddr, ESymValue svalue, FlatDomain<Type> addrType, Domain target)
      {
        //
        // enumerate through all access paths in old state for old value and try to find an unmodified
        // accesspath in the new state. This would correspond to the same value in the new state.
        //
        bool callInsideContract;
        if (pc.IsInsideEnsuresAtCall(out callInsideContract) || pc.IsInsideInvariantAtCall(out callInsideContract))
        {
          Type constType;
          object constValue;
          if (this.IsConstant(svalue, out constType, out constValue))
          {
            // just look up the new constant
            var newConstant = target.ConstantValue(constValue, constType);
            target.CopyNonStructWithFieldValue(destAddr, newConstant, addrType);
            return;
          }
          // must be inside ensures around call
          ESymValue best = null;

          foreach (var accesspath in this.GetAccessPathsRaw(svalue, AccessPathFilter<Method, Type>.NoFilter, false))
          {
            ESymValue candidate;
            if (FindOldCandidateAfterCall(target, accesspath, callInsideContract, out candidate))
            {
              if (best == null)
              {
                best = candidate;
              }
              else
              {
                if (candidate.GlobalId < best.GlobalId)
                {
                  best = candidate;
                }
              }
            }
          }
          if (best != null)
          {
            target.CopyNonStructWithFieldValue(destAddr, best, addrType);
            return;
          }
        }
        else
        {
          // must be inside ensures on exit
          foreach (var accesspath in this.GetAccessPathsRaw(svalue, AccessPathFilter<Method, Type>.NoFilter, false))
          {
            ESymValue targetSym;
            if (target.PathExistsUnmodifiedSinceEntry(accesspath, out targetSym))
            {
              target.CopyNonStructWithFieldValue(destAddr, targetSym, addrType);
              return;
            }
          }
        }
        // backup approach
        if (target.IsValidSymbol(svalue))
        {
          target.CopyNonStructWithFieldValue(destAddr, svalue, addrType);
        }
        else
        {
          target.Assign(destAddr, addrType);
        }
        return;
      }

      private static bool FindOldCandidateAfterCall(Domain target, FList<PathElementBase> accesspath, bool callInsideContract, out ESymValue candidate)
      {
        if (callInsideContract)
        {
          // call couldn't modify anything
          return target.PathExistsAtCall(accesspath, out candidate);
        }
        else
        {
          return target.PathExistsUnmodifiedAtCall(accesspath, out candidate);
        }
      }

      private static void CopyStructId(APC pc, ESymValue destAddr, ESymValue srcAddr, Domain source, Domain target)
      {
        // TODO: do the same as in oldvalue copy, but we need access paths to struct ids
#if true
        target.ManifestStructId(destAddr);
#else
        ESymValue identity = this.egraph[Constructors.StructId, srcAddr];
        //
        // enumerate through all access paths in old state for old value and try to find an unmodified
        // accesspath in the new state. This would correspond to the same value in the new state.
        //
        if (pc.InsideEnsuresAtCall)
        {
          // must be inside ensures around call
          foreach (var accesspath in this.GetAccessPaths(identity, new Optional<Method>(), false))
          {
            ESymValue targetSym;
            if (target.PathExistsUnmodifiedAtCall(accesspath, out targetSym))
            {
              target.CopyNonStructWithFieldValue(destAddr, targetSym, addrType);
              return;
            }
          }
        }
        else
        {
          // must be inside ensures on exit
          foreach (var accesspath in this.GetAccessPaths(identity, new Optional<Method>(), false))
          {
            ESymValue targetSym;
            if (target.PathExistsUnmodifiedSinceEntry(accesspath, out targetSym))
            {
              target.CopyNonStructWithFieldValue(destAddr, targetSym, addrType);
              return;
            }
          }
        }
        // backup approach
        if (target.IsValidSymbol(identity))
        {
          target.CopyNonStructWithFieldValue(destAddr, identity, addrType);
        }
        else
        {
          target.AssignValue(destAddr, addrType);
        }
#endif
      }

      private void CopyStructValue(ESymValue destAddr, ESymValue srcAddr, Type type)
      //^ requires mdDecoder.IsStruct(type);
      {
        if (destAddr == null) return;
        // copy struct id
        this.egraph[Constructors.StructId, destAddr] = this.egraph[Constructors.StructId, srcAddr];

        foreach (Constructor key in this.egraph.Functions(srcAddr))
        {
          if (!key.ActsAsField) continue;
          Type t = key.FieldAddressType(mdDecoder);

          ESymValue destFld = this.egraph[key, destAddr];
          ESymValue srcFld = this.egraph[key, srcAddr];
          CopyValue(destFld, srcFld, t);
        }
      }

      private void CopyOldStructValue(APC pc, ESymValue destAddr, ESymValue srcAddr, Type type, Domain target, bool atEndOld)
      //^ requires mdDecoder.IsStruct(type);
      {
        if (destAddr == null) return;
        // copy old struct id
        CopyStructId(pc, destAddr, srcAddr, this, target);

        foreach (Constructor key in this.egraph.Functions(srcAddr))
        {
          if (!key.ActsAsField) continue;
          Type t = key.FieldAddressType(mdDecoder);

          ESymValue destFld = target.egraph[key, destAddr];
          ESymValue srcFld = this.egraph[key, srcAddr];
          CopyOldValue(pc, destFld, srcFld, t, target, atEndOld);
        }
      }


      private ESymValue FieldAddress(ESymValue ptr, Field f)
      {
        Type fieldAddressType;
        return FieldAddress(ptr, f, out fieldAddressType);
      }

      private ESymValue FieldAddress(ESymValue ptr, Field f, out Type fieldAddressType)
      {
        fieldAddressType = mdDecoder.ManagedPointer(mdDecoder.FieldType(f));
        bool fresh;
        ESymValue fldaddr = LookupAddressAndManifestType(ptr, Constructors.For(f), fieldAddressType, out fresh);
        if (fresh)
        {
          if (this.IsUnmodifiedForFields(ptr) || this.mdDecoder.IsReadonly(f))
          {
            this.MakeUnmodified(fldaddr);
          }
          if (this.IsModifiedAtCall(ptr))
          {
            this.modifiedAtCall = this.modifiedAtCall.Add(fldaddr);
          }
        }
        return fldaddr;
      }

      private ESymValue PseudoFieldAddress(ESymValue value, Method m, Type fromType)
      {
        Type psuedoFieldAddressType;
        return PseudoFieldAddress(value, m, out psuedoFieldAddressType, false, fromType: fromType);
      }

      private ESymValue PseudoFieldAddress(ESymValue[] args, Method m, out Type pseudoFieldAddressType, bool materializeStructFields, Type fromType)
      {
        if (args.Length == 0)
        {
          return PseudoFieldAddress(Globals, m, out pseudoFieldAddressType, materializeStructFields, fromType: fromType);
        }
        if (args.Length == 1)
        {
          return PseudoFieldAddress(args[0], m, out pseudoFieldAddressType, materializeStructFields, fromType: fromType);
        }
        pseudoFieldAddressType = mdDecoder.ManagedPointer(mdDecoder.ReturnType(m));
        m = mdDecoder.Unspecialized(m);
        bool fresh;
        ESymValue fldaddr = LookupAddressAndManifestType(args, Constructors.For(m), pseudoFieldAddressType, out fresh);
        if (fresh)
        {
          if (AreUnmodified(args))
          {
            this.MakeUnmodified(fldaddr);
          }
          if (AnyAreModifiedAtCall(args))
          {
            this.modifiedAtCall = this.modifiedAtCall.Add(fldaddr);
          }
          if (materializeStructFields)
          {
            MaterializeAccordingToType(fldaddr, pseudoFieldAddressType, 0, Constructors.ResultOfPureCall, fromType: fromType);
          }
        }
        return fldaddr;
      }

      private ESymValue PseudoFieldAddress(ESymValue value, Method m, out Type pseudoFieldAddressType, bool materialize, Type fromType)
      {
        var returnType = mdDecoder.ReturnType(m);
        pseudoFieldAddressType = mdDecoder.ManagedPointer(returnType);
        m = mdDecoder.Unspecialized(m);
        bool fresh;
        ESymValue fldaddr = LookupAddressAndManifestType(value, Constructors.For(m), pseudoFieldAddressType, out fresh);
        if (fresh)
        {
          if (this.IsUnmodified(value))
          {
            this.MakeUnmodified(fldaddr);
          }
          if (this.IsModifiedAtCall(value))
          {
            this.modifiedAtCall = this.modifiedAtCall.Add(fldaddr);
          }
        }
        if (materialize)
        {
          MaterializeAccordingToType(fldaddr, pseudoFieldAddressType, 0, Constructors.ResultOfPureCall, fromType: fromType);
        }
        return fldaddr;
      }

      public void MaterializeOutParameterAccordingToType(ESymValue value, Type t, int depth, Type fromType)
      {
        MaterializeAccordingToType(value, t, depth, Constructors.ResultOfOutParameter, fromType: fromType);
      }

      public void MaterializeThisAtCallAccordingToType(ESymValue value, Type t, int depth, Type fromType)
      {
        MaterializeAccordingToType(value, t, depth, Constructors.ResultOfCallThisHavoc, fromType: fromType);
      }


      public void MaterializeNewObjAccordingToType(ESymValue value, Type t, int depth, Type fromType)
      {
        MaterializeAccordingToType(value, t, depth, null, fromType: fromType);
      }

      // F: I added overrideFromType
      private void MaterializeAccordingToType(ESymValue value, Type t, int depth, Constructor breadCrumb, Type fromType, bool overrideFromType = false)
      {
        SetType(value, t);
        if (depth > 2) return;
        if (mdDecoder.IsManagedPointer(t))
        {
          Type elementType = mdDecoder.ElementType(t);
          if (!mdDecoder.HasValueRepresentation(elementType))
          {
            // manifest struct id
            this.ManifestStructId(value);
            // manifest field addresses
            foreach (Field f in mdDecoder.Fields(elementType))
            {
              if (mdDecoder.IsStatic(f)) continue;
              if (!mdDecoder.IsVisibleFrom(f, fromType)) continue;
              // manifest the field
              Type fieldType;
              ESymValue fieldAddr = FieldAddress(value, f, out fieldType);
              MaterializeAccordingToType(fieldAddr, fieldType, depth + 1, null, fromType, overrideFromType);
            }
            foreach (Property p in mdDecoder.Properties(elementType))
            {
              if (mdDecoder.IsStatic(p)) continue;
              Method getter;
              if (!mdDecoder.HasGetter(p, out getter)) continue;
              if (mdDecoder.Parameters(getter).Count > 0) continue;
              if (!mdDecoder.IsVisibleFrom(getter, fromType)) continue;
              // skip string.get_Length, as we use $Length for that
              if (mdDecoder.Equal(elementType, mdDecoder.System_String) && mdDecoder.Name(getter) == "get_Length")
              {
                continue;
              }

              // manifest the pseudo field
              Type propertyType;
              ESymValue pseudoFieldAddr = PseudoFieldAddress(value, getter, out propertyType, false, fromType: fromType);
              MaterializeAccordingToType(pseudoFieldAddr, propertyType, depth + 1, null, fromType, overrideFromType);
            }
            foreach (var m in parent.contractDecoder.ModelMethods(elementType))
            {
              if (mdDecoder.IsStatic(m)) continue;
              // manifest the pseudo field
              Type propertyType;
              ESymValue pseudoFieldAddr = PseudoFieldAddress(value, m, out propertyType, false, fromType: fromType);
              MaterializeAccordingToType(pseudoFieldAddr, propertyType, depth + 1, null, fromType, overrideFromType);
            }
          }
          else
          {
            // manifest the value
            var derefedValue = Value(value);
            MaterializeAccordingToType(derefedValue, elementType, depth + 1, null, fromType, overrideFromType);
            if (breadCrumb != null)
            {
              // leave breadcrumb
              egraph[breadCrumb, derefedValue] = Zero;
            }
          }
          return;
        }

        if (!mdDecoder.IsArray(t) && DerivesFromIEnumerable(t))
        {
          ManifestModel(value, t, fromType: fromType);
        }

        if (mdDecoder.IsClass(t))
        {
          foreach (Field f in mdDecoder.Fields(t))
          {
            if (mdDecoder.IsStatic(f)) continue;
            if (overrideFromType || mdDecoder.IsVisibleFrom(f, fromType))
            {
              // manifest the field
              Type fieldType;
              ESymValue fieldAddr = this.FieldAddress(value, f, out fieldType);
              MaterializeAccordingToType(fieldAddr, fieldType, depth + 1, breadCrumb, fromType, overrideFromType);
            }
          }
          if (breadCrumb == Constructors.ResultOfCallThisHavoc && mdDecoder.HasBaseClass(t))
          {
            // materialize base fields
            var baseType = mdDecoder.BaseClass(t);
            foreach (Field f in mdDecoder.Fields(baseType))
            {
              if (mdDecoder.IsStatic(f)) continue;
              if (mdDecoder.IsVisibleFrom(f, fromType))
              {
                // manifest the field
                Type fieldType;
                ESymValue fieldAddr = this.FieldAddress(value, f, out fieldType);
                MaterializeAccordingToType(fieldAddr, fieldType, depth + 1, breadCrumb, fromType, overrideFromType);
              }
            }
          }
        }

        foreach (Property p in mdDecoder.Properties(t))
        {
          if (mdDecoder.IsStatic(p)) continue;
          Method getter;
          if (!mdDecoder.HasGetter(p, out getter)) continue;
          if (mdDecoder.Parameters(getter).Count > 0) continue;
          if (!mdDecoder.IsVisibleFrom(getter, fromType)) continue;
          // skip string.get_Length, as we use $Length for that
          if (mdDecoder.Equal(t, mdDecoder.System_String) && mdDecoder.Name(getter) == "get_Length")
          {
            continue;
          }

          // manifest the pseudo field
          Type propertyType;
          ESymValue pseudoFieldAddr = PseudoFieldAddress(value, getter, out propertyType, false, fromType: fromType);
          MaterializeAccordingToType(pseudoFieldAddr, propertyType, depth + 1, null, fromType, overrideFromType);

          // manifest aliased interface properties
          foreach (var intfmethod in mdDecoder.ImplementedMethods(getter))
          {
            var normalizedMethod = mdDecoder.Unspecialized(intfmethod);
            this.egraph[Constructors.For(normalizedMethod), value] = pseudoFieldAddr;
          }
        }

        if (NeedsArrayLengthManifested(t))
        {
          ManifestArrayLength(value);
          return;
        }

        if (mdDecoder.IsUnmanagedPointer(t))
        {
          ManifestWritableBytes(value);
          return;
        }
      }


      #region Unmodified since entry
      private void MakeUnmodified(ESymValue value)
      {
        this.unmodifiedSinceEntry = this.unmodifiedSinceEntry.Add(value);
      }

      private void MakeModified(ESymValue value)
      {
        this.unmodifiedSinceEntry = this.unmodifiedSinceEntry.Remove(value);
      }

      private void MakeTotallyModified(ESymValue value)
      {
        this.unmodifiedSinceEntry = this.unmodifiedSinceEntry.Remove(value);
        this.unmodifiedSinceEntryForFields = this.unmodifiedSinceEntryForFields.Remove(value);
      }

      private void RetainForUnmodifiedFields(ESymValue value)
      {
        this.unmodifiedSinceEntryForFields = this.unmodifiedSinceEntryForFields.Add(value);
      }

      private bool IsUnmodified(ESymValue value)
      {
        return this.unmodifiedSinceEntry.Contains(value);
      }

      private bool AreUnmodified(ESymValue[] values)
      {
        for (int i = 0; i < values.Length; i++)
        {
          if (!IsUnmodified(values[i])) return false;
        }
        return true;
      }

      private bool AnyAreModifiedAtCall(ESymValue[] values)
      {
        for (int i = 0; i < values.Length; i++)
        {
          if (IsModifiedAtCall(values[i])) return true;
        }
        return false;
      }

      private bool IsUnmodifiedForFields(ESymValue value)
      {
        return this.unmodifiedSinceEntry.Contains(value) || this.unmodifiedSinceEntryForFields.Contains(value);
      }
      #endregion

      #region Array support
      private ESymValue ElementAddress(ESymValue array, ESymValue index, Type/*!*/ elementAddrType)
      {
        var arrayversion = this.egraph[Constructors.ObjectVersion, array];
        var args = new[] { arrayversion, index };
        var elementAddr = this.egraph.TryLookup(Constructors.ElementAddress, args);

        if (elementAddr == null)
        {
          elementAddr = this.egraph[Constructors.ElementAddress, args]; // manifest
          SetType(elementAddr, elementAddrType);
          // leave breadcrumb
          this.egraph[Constructors.ResultOfLdelem, elementAddr] = this.Zero;
        }
        return elementAddr;
      }


      void AssignArrayLength(ESymValue destAddr, ESymValue array)
      {
        ESymValue len = egraph[Constructors.Length, array];
        SetType(len, mdDecoder.System_Int32);
        egraph[Constructors.ValueOf, destAddr] = len;
        SetType(destAddr, mdDecoder.ManagedPointer(mdDecoder.System_Int32));
      }

      void AssignWritableBytes(ESymValue destAddr, ESymValue pointer)
      {
        ESymValue len = egraph[Constructors.WritableBytes, pointer];
        SetType(len, mdDecoder.System_UInt64);
        egraph[Constructors.ValueOf, destAddr] = len;
        SetType(destAddr, mdDecoder.ManagedPointer(mdDecoder.System_UInt64));
      }

      ESymValue CreateArray(Type type, ESymValue len)
      {
        ESymValue array = egraph.FreshSymbol();
        egraph[Constructors.Length, array] = len;
        SetType(array, mdDecoder.ArrayType(type, 1));
        return array;
      }

      ESymValue LocalAlloc(ESymValue len)
      {
        ESymValue buffer = egraph.FreshSymbol();
        egraph[Constructors.WritableBytes, buffer] = len;
        SetType(buffer, mdDecoder.System_UIntPtr);
        return buffer;
      }

      #endregion

      ESymValue CreateObject(Type type)
      {
        ESymValue obj = egraph.FreshSymbol();
        SetType(obj, type);
        return obj;
      }

      /// <returns>The address of a new value object</returns>
      ESymValue CreateValue(Type type)
      {
        ESymValue addr = egraph.FreshSymbol();
        SetType(addr, this.mdDecoder.ManagedPointer(type));
        return addr;
      }

      private void CopyStackAddress(ESymValue destAddr, Temp temporaryForWhichAddressIsTaken)
      {
        ESymValue srcAddr = Address(temporaryForWhichAddressIsTaken);
        this.egraph[Constructors.ValueOf, destAddr] = srcAddr;
        AbstractType atype = CurrentType(srcAddr);
        FlatDomain<Type> addrType;
        if (atype.IsNormal)
        {
          addrType = mdDecoder.ManagedPointer(atype.Type.Value);
        }
        else
        {
          addrType = new FlatDomain<Type>();
        }
        SetType(destAddr, addrType);
      }

      private void CopyAddress(ESymValue destAddr, ESymValue srcAddr, Type typeOfAddressValue)
      {
        this.egraph[Constructors.ValueOf, destAddr] = srcAddr;
        SetType(destAddr, mdDecoder.ManagedPointer(typeOfAddressValue));
      }

        /// <summary>
        /// Like copy, but leaves ResultOfOldValue breadcrumb
        /// </summary>
      void CopyOld(Temp dest, Temp source, Type type)
      {
          Copy(dest, source);
          if (!IsStructWithFields(type))
          {
              // leave breadcrumb
              var derefedValue = this.Value(dest);
              this.egraph[Constructors.ResultOfOldValue, derefedValue] = Null;
          }
      }

      void Copy(Temp dest, Temp source)
      {
        ESymValue destAddr = Address(Constructors.For(dest));
        ESymValue srcAddr = Address(Constructors.For(source));
        CopyValue(destAddr, srcAddr);
      }

      #region Operator Type Conversions

      /// <summary>
      /// Currently, we don't take nullness/zeroness into account
      /// </summary>
      FlatDomain<Type> BinaryResultType(BinaryOperator op, AbstractType t1, AbstractType t2)
      {
        switch (op)
        {
          case BinaryOperator.Add:
          case BinaryOperator.Add_Ovf:
          case BinaryOperator.Add_Ovf_Un:
            if (t1.IsNormal &&
                (mdDecoder.IsUnmanagedPointer(t1.Value)))
            {
              return t1.Type;
            }
            if (t2.IsNormal && (mdDecoder.IsUnmanagedPointer(t2.Value)))
            {
              return t2.Type;
            }
            if (t1.IsNormal && mdDecoder.IsManagedPointer(t1.Value))
            {
              return mdDecoder.UnmanagedPointer(mdDecoder.ElementType(t1.Value));
            }
            if (t1.IsZero) return t2.Type;
            return t1.Type;

          case BinaryOperator.Sub:
          case BinaryOperator.Sub_Ovf:
          case BinaryOperator.Sub_Ovf_Un:
            if (t1.IsNormal && (mdDecoder.IsUnmanagedPointer(t1.Value)))
            {
              if (t2.IsNormal && (mdDecoder.IsUnmanagedPointer(t2.Value)))
              {
                return mdDecoder.System_Int32;
              }
              return t1.Type;
            }
            if (t2.IsNormal && (mdDecoder.IsUnmanagedPointer(t2.Value)))
            {
              return t2.Type;
            }
            if (t1.IsNormal && mdDecoder.IsManagedPointer(t1.Value))
            {
              return mdDecoder.UnmanagedPointer(mdDecoder.ElementType(t1.Value));
            }
            return t1.Type;

          case BinaryOperator.Ceq:
          case BinaryOperator.Cobjeq:
          case BinaryOperator.Cge:
          case BinaryOperator.Cge_Un:
          case BinaryOperator.Cgt:
          case BinaryOperator.Cgt_Un:
          case BinaryOperator.Cle:
          case BinaryOperator.Cle_Un:
          case BinaryOperator.Clt:
          case BinaryOperator.Clt_Un:
          case BinaryOperator.Cne_Un:
            return mdDecoder.System_Boolean;

          default:
            return t1.Type;
        }
      }

      Type UnaryResultType(UnaryOperator op, AbstractType type)
      {
        switch (op)
        {
          case UnaryOperator.Conv_i1: return this.mdDecoder.System_Int8;
          case UnaryOperator.Conv_i2: return this.mdDecoder.System_Int16;
          case UnaryOperator.Conv_i4: return this.mdDecoder.System_Int32;
          case UnaryOperator.Conv_i8: return this.mdDecoder.System_Int64;
          case UnaryOperator.Conv_u1: return this.mdDecoder.System_UInt8;
          case UnaryOperator.Conv_u2: return this.mdDecoder.System_UInt16;
          case UnaryOperator.Conv_u4: return this.mdDecoder.System_UInt32;
          case UnaryOperator.Conv_u8:
          case UnaryOperator.WritableBytes:
            return this.mdDecoder.System_UInt64;
          case UnaryOperator.Conv_r8:
          case UnaryOperator.Conv_r_un:
            return this.mdDecoder.System_Double;
          case UnaryOperator.Conv_r4:
            return this.mdDecoder.System_Single;
          case UnaryOperator.Conv_dec:
            return this.mdDecoder.System_Decimal;
        }
        if (type.IsNormal)
        {
          return type.Value;
        }
        return mdDecoder.System_Int32;
      }

      #endregion

      #region Metadata helpers
      bool IsPointer(Type t)
      {
        return mdDecoder.IsManagedPointer(t) || mdDecoder.IsUnmanagedPointer(t);
      }
      #endregion

      [ThreadStatic]
      private static bool debug;

      private IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder { get { return parent.mdDecoder; } }

      /// <summary>
      /// Determines if the temporary refers to "this"
      /// </summary>
      private bool IsThis(Method method, Temp arg)
      {
        if (mdDecoder.IsStatic(method)) return false;
        ESymValue thisValue = this.Value(this.mdDecoder.This(method));
        ESymValue argValue = this.Value(arg);
        return thisValue == argValue;
      }

      private void ManifestArrayLength(ESymValue arrayValue)
      {
        ESymValue len = egraph[Constructors.Length, arrayValue];
        SetType(len, mdDecoder.System_Int32);
      }

      private void ManifestWritableBytes(ESymValue pointerValue)
      {
        ESymValue len = egraph[Constructors.WritableBytes, pointerValue];
        SetType(len, mdDecoder.System_UInt64);
      }

      private void AddOldSavedState(APC pc, Domain data)
      {
        this.BeginOldSavedStates[pc] = data;
      }

      private Domain GetStateAt(APC aPC)
      {
        Domain d;
        this.parent.PreStateLookup(aPC, out d);
        return d;
      }

      private bool IsValidSymbol(ESymValue sv)
      {
        return this.egraph.IsValidSymbolc(sv);
      }

      private void CopyNonStructWithFieldValue(ESymValue address, ESymValue sv, FlatDomain<Type> addressType)
      {
        this.egraph[Constructors.ValueOf, address] = sv;
        SetType(address, addressType);
      }

      private Domain GetSavedStateAtBeginOld(APC matchingBegin)
      {
        return this.BeginOldSavedStates[matchingBegin];
      }

      #endregion // Privates

      #region Debugging
      public static bool Debug
      {
        get { return debug; }
        set { debug = value; EGraph<Constructor, AbstractType>.DoDebug = value; }
      }

      public IEnumerable<ESymValue> Variables
      {
        get
        {
          return this.egraph.SymbolicValues;
        }
      }
      #endregion

      /// <summary>
      /// Constructing the initial domain value for a method
      /// </summary>
      /// <param name="parent"></param>
      [ContractVerification(false)]
      internal Domain(OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> parent)
      {
        this.parent = parent;
        this.egraph = new EGraph<Constructor, AbstractType>(AbstractType.TopValue, AbstractType.BottomValue);
        this.constantLookup = FunctionalIntKeyMap<ESymValue, Constructor>.Empty(ESymValue.GetUniqueKey);
        this.unmodifiedSinceEntry = FunctionalSet<ESymValue>.Empty(ESymValue.GetUniqueKey);
        this.unmodifiedSinceEntryForFields = FunctionalSet<ESymValue>.Empty(ESymValue.GetUniqueKey);
        this.modifiedAtCall = FunctionalSet<ESymValue>.Empty(ESymValue.GetUniqueKey);

        this.Constructors = new WrapTable(parent.mdDecoder, parent.contractDecoder);
        this.BeginOldSavedStates = new Dictionary<APC, Domain>();
        // manifest some constants with special properties
        ESymValue nullvalue = this.egraph.FreshSymbol();
        this.egraph[Constructors.NullValue] = nullvalue;
        this.egraph[nullvalue] = AbstractType.BottomValue;
        ESymValue zerovalue = this.ConstantValue(0, mdDecoder.System_Int32);
        // fix type to remember it is 0
        this.egraph[zerovalue] = new AbstractType(mdDecoder.System_Int32, true);

        ESymValue voidaddr = this.VoidAddr;
      }

      internal Type GetArrayElementType(Type type, int array)
      {
        Type elementType = type;
        if (!mdDecoder.IsStruct(type))
        {
          ESymValue arrayval = this.Value(array);
          AbstractType at = this.GetType(arrayval);
          if (at.IsNormal && mdDecoder.IsArray(at.Value))
          {
            elementType = mdDecoder.ElementType(at.Value);
            if (elementType == null) elementType = type;
          }
        }
        return elementType;
      }

      #region Heap analysis per instruction actions

      /// <summary>
      /// Performs the actual heap analysis for each instruction kind
      /// </summary>
      internal struct AnalysisDecoder : IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Temp, Temp, Domain, Domain>
      {
        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
          Contract.Invariant(this.parent != null);
        }


        #region Privates
        private OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> parent;
        #endregion

        public AnalysisDecoder(OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> parent)
        {
          this.parent = parent;

          Contract.Assume(this.parent != null);
        }

        IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder { get { return this.parent.contractDecoder; } }
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder
        {
          get
          {
            Contract.Ensures(Contract.Result<IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>>() != null);
            return this.parent.mdDecoder;
          }
        }

        #region IVisitMSIL<APC,Local,Parameter,Method,Field,Type,int,int,OptimisticHeapAbstraction<APC,Local,Parameter,Method,Field,Type>,OptimisticHeapAbstraction<APC,Local,Parameter,Method,Field,Type>> Members

        public Domain Arglist(APC pc, int dest, Domain data)
        {
          var arghandletype = data.mdDecoder.System_RuntimeArgumentHandle;
          data.AssignValue(dest, arghandletype, this.CurrentMethodDeclaringType);
          if (data.oldDomain != null)
          {
            data.oldDomain.AssignValue(dest, arghandletype, this.CurrentMethodDeclaringType);
          }
          return data;
        }

        public Domain Binary(APC pc, BinaryOperator op, int dest, Temp s1, Temp s2, Domain data)
        {
          Contract.Assume(s2 > s1);// F: Added

          data = BinaryEffect(pc, op, dest, s1, s2, data, this.CurrentMethodDeclaringType);
          if (data.oldDomain != null)
          {
            data.oldDomain = BinaryEffect(pc, op, dest, s1, s2, data.oldDomain, this.CurrentMethodDeclaringType);
          }
          return data;
        }
        private static Domain BinaryEffect(APC pc, BinaryOperator op, int dest, Temp s1, Temp s2, Domain data, Type fromType)
        {
          Contract.Requires(s2 > s1); // F: Added from Clousot precondition suggestion

          System.Diagnostics.Debug.Assert(s2 > s1);
          FlatDomain<Type> resultType = data.BinaryResultType(op, data.CurrentType(s1), data.CurrentType(s2));
          ESymValue sv1, sv2;
          //
          // Capture comparisons against zero/null and record them so that we can refine
          // branches for nullness.
          //
          switch (op)
          {
            case BinaryOperator.Ceq:
            case BinaryOperator.Cobjeq:
              sv1 = data.Value(s1);
              if (data.IsZero(sv1))
              {
                // result is true/non-zero if s2 is zero, false/zero otherwise
                // thus we can use unaryNot here
                data.AssignSpecialUnary(dest, data.Constructors.UnaryNot, s2, resultType, fromType: fromType);
              }
              else
              {
                sv2 = data.Value(s2);
                if (data.IsZero(sv2))
                {
                  // result is true/non-zero if s1 is zero, false/zero otherwise
                  // thus we can use unaryNot here
                  data.AssignSpecialUnary(dest, data.Constructors.UnaryNot, s1, resultType, fromType: fromType);
                }
                else
                  goto default;
              }
              break;

            case BinaryOperator.Cne_Un:
              sv1 = data.Value(s1);
              if (data.IsZero(sv1))
              {
                data.AssignSpecialUnary(dest, data.Constructors.NeZero, s2, resultType, fromType: fromType);
              }
              else
              {
                sv2 = data.Value(s2);
                if (data.IsZero(sv2))
                {
                  data.AssignSpecialUnary(dest, data.Constructors.NeZero, s1, resultType, fromType: fromType);
                }
                else
                  goto default;
              }
              break;

            default:
              data.AssignPureBinary(dest, op, resultType, s1, s2);
              break;
          }
          data.Havoc(s2);
          return data;
        }

        public Domain BranchCond(APC pc, APC target, BranchOperator bop, int value1, int value2, Domain data)
        {
          throw new NotImplementedException("Should not see Branches, should see Assumes");
        }

        public Domain BranchTrue(APC pc, APC target, int cond, Domain data)
        {
          throw new NotImplementedException("Should not see Branches, should see Assumes");
        }

        public Domain BranchFalse(APC pc, APC target, int cond, Domain data)
        {
          throw new NotImplementedException("Should not see Branches, should see Assumes");
        }

        public Domain Branch(APC pc, APC target, bool leave, Domain data)
        {
          throw new NotImplementedException("Should not see Branches, should see Assumes");
        }

        public Domain Break(APC pc, Domain data)
        {
          return data;
        }

        public Domain Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Temp dest, ArgList args, Domain data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<int>
        {
          Type declaringType = mdDecoder.DeclaringType(method);
          if (data.oldDomain != null)
          {
            // perform call in old state and map result back
            data.oldDomain = CallFactored(pc, method, tail, virt, extraVarargs, dest, args, data.oldDomain, declaringType, false);

            if (!mdDecoder.IsVoidMethod(method))
            {
              data.oldDomain.CopyOldValue(data.oldDomain.beginOldPC, dest, dest, data, true);
            }
            return data;
          }
          else
          {
            return CallFactored<TypeList, ArgList>(pc, method, tail, virt, extraVarargs, dest, args, data, declaringType, false);
          }
        }

        private Type CurrentMethodDeclaringType
        {
          get
          {
            return this.mdDecoder.DeclaringType(this.parent.currentMethod);
          }
        }
        [ContractVerification(false)]
        private Domain CallFactored<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Temp dest, ArgList args, Domain data, Type methodDeclaringType, bool constrainedCall)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<int>
        {
          Contract.Assume(args.Count >= mdDecoder.Parameters(method).Count + extraVarargs.Count);
          Contract.Assume(args.Count <= mdDecoder.Parameters(method).Count + extraVarargs.Count + 1);
          Contract.Assume(!mdDecoder.IsStatic(method) || args.Count == mdDecoder.Parameters(method).Count + extraVarargs.Count);

          Type declaringType = methodDeclaringType;

          if (!pc.InsideContract)
          {
            data.ResetModifiedAtCall();
          }

          #region Compute a type map for type instantiations
          var substitution = ComputeTypeInstantiationMap(pc, method);
          #endregion
          #region Devirtualize as far as possible
          bool dereferenceThis = false;
          if (virt)
          {
            if (mdDecoder.IsStruct(methodDeclaringType))
            {
              // constrained call virt. Let's use the constraint to find the better method implementing the method
              DevirtualizeImplementingMethod(methodDeclaringType, ref method);
            }
            else
            {
              // figure out if constrained call and we know it's a reference type
              if (constrainedCall)
              {
                var specialized = Specialize(substitution, methodDeclaringType);
                if (mdDecoder.IsReferenceType(specialized))
                {
                  // need extra dereference of "this"
                  dereferenceThis = true;
                }
              }
              // let's use the receiver
              ESymValue obj = data.Value(args[0]);
              if (dereferenceThis)
              {
                obj = data.Value(obj);
              }
              var thisType = data.GetType(obj);
              if (thisType.IsNormal)
              {
                // find method in thisType that implements method.
                DevirtualizeImplementingMethod(thisType.Value, ref method);
              }
            }
          }
          #endregion

          #region Special case methods
          string methodName = mdDecoder.Name(method);
          if (args.Count > 0)
          {
            if ((mdDecoder.Equal(declaringType, mdDecoder.System_String) ||
              mdDecoder.Equal(declaringType, mdDecoder.System_Array))
              && (methodName == "get_Length" || methodName == "get_LongLength"))
            {
              // assign length to dest
              data.AssignArrayLength(data.Address(dest), data.Value(args[0]));
              return data;
            }
            if (mdDecoder.Equal(declaringType, mdDecoder.System_Object)
              && methodName == "MemberwiseClone")
            {
              Type thisType = data.GetType(data.Value(args[0])).Value;
              ESymValue obj = data.CreateObject(thisType);
              data.CopyStructValue(obj, data.Value(args[0]), thisType); // cheat. Use struct copy to do memberwise copy here.
              data.CopyAddress(data.Address(dest), obj, thisType);
              return data;
            }

            #region Identity functions

            // Array.GetLowerBound(0), assume it is 0
            if (methodName == "GetLowerBound" && mdDecoder.Name(declaringType) == "Array" && args.Count > 1)
            {
              ESymValue arg = data.Value(args[1]);
              if (data.IsZero(arg))
              {
                data.CopyNonStructWithFieldValue(data.Address(dest), arg, mdDecoder.ManagedPointer(mdDecoder.System_Int32));
                return data;
              }
            }

            #endregion
          }
          if (extraVarargs.Count == 0 && !mdDecoder.IsVoid(mdDecoder.ReturnType(method)) && (contractDecoder.IsPure(method) || contractDecoder.IsModel(method)))
          {
            // NOTE: Need to check whether the method is a model method because IsHeapIndependent might not be able to find the property that the model method (getter)
            // belongs to.
            var nonOutArgs = GetNonOutArgs(method);
            if (args.Count <= 1 && nonOutArgs == args.Count)
            {
              // treat like a field provided it isn't marked as fresh
              if (contractDecoder.IsFreshResult(method))
              {
                data.AssignReturnValue(dest, mdDecoder.ReturnType(method), this.CurrentMethodDeclaringType); // void is handled by assign
                return data;
              }
              ESymValue obj;
              if (args.Count == 0)
              {
                Contract.Assume(mdDecoder.IsStatic(method));
                obj = data.Globals;
              }
              else if (mdDecoder.IsStatic(method) && !mdDecoder.HasValueRepresentation(mdDecoder.ParameterType(mdDecoder.Parameters(method)[0])))
              {
                // struct by value parameter
                obj = data.StructId(data.Address(args[0]));
              }
              else
              {
                obj = null;
                if (!mdDecoder.IsStatic(method) && !mdDecoder.HasValueRepresentation(mdDecoder.DeclaringType(method)) && IsStructValue(data, args[0]))
                {
                  // struct instance call
                  // special case if we have a value on the stack rather than the address
                  obj = data.Address(args[0]); // use the address of the stack local
                }
                if (obj == null) {
                  // ordinary non-struct parameter or by-ref struct
                  obj = data.Value(args[0]);
                  if (dereferenceThis)
                  {
                    obj = data.Value(obj);
                  }
                }
              }
              Type pseudoFieldAddressType;
              ESymValue fieldAddr = data.PseudoFieldAddress(obj, method, out pseudoFieldAddressType, true, this.CurrentMethodDeclaringType);
              data.CopyValue(data.Address(dest), fieldAddr, pseudoFieldAddressType);
              return data;
            }
            else
            {
              // multi variable uninterpreted function
              ESymValue[] symArgs = new ESymValue[nonOutArgs];
              for (int i = 0, index = 0; i < args.Count; i++)
              {
                bool isOut;

                var key = KeyForPureFunctionArgument(method, i, args[i], data, substitution, out isOut);
                if (!isOut)
                {
                  symArgs[index++] = key;
                }
              }

              Type pseudoFieldAddressType;
              ESymValue fieldAddr = data.PseudoFieldAddress(symArgs, method, out pseudoFieldAddressType, true, this.CurrentMethodDeclaringType);
              AssignAllOutParameters(data, fieldAddr, method, args);
              data.CopyValue(data.Address(dest), fieldAddr, pseudoFieldAddressType);
              return data;

            }
          }
          if (mdDecoder.IsPropertySetter(method))
          {
            Property property = mdDecoder.GetPropertyFromAccessor(method);
            if (args.Count <= 2)
            {
              Method getter;
              if (mdDecoder.HasGetter(property, out getter))
              {
                // treat like a field
                Type pseudoFieldAddressType;
                ESymValue obj;
                ESymValue valueAddr;
                if (args.Count == 1)
                {
                  Contract.Assume(mdDecoder.IsStatic(getter));
                  obj = data.Globals;
                  valueAddr = data.Address(args[0]);
                }
                else
                {
                  obj = data.Value(args[0]);
                  if (dereferenceThis)
                  {
                    obj = data.Value(obj);
                  }
                  valueAddr = data.Address(args[1]);
                }

                // havoc whatever the setter havocs if we are inside the same class
                if (this.mdDecoder.Equal(this.mdDecoder.DeclaringType(this.parent.currentMethod), this.mdDecoder.Unspecialized(this.mdDecoder.DeclaringType(method))))
                {
                  data.HavocUp(obj, ref data.modifiedAtCall, false);
                  if (mdDecoder.IsAutoPropertyMember(method))
                  {
                    // set the backing field
                    foreach (var backingField in this.parent.context.MethodContext.Modifies(method))
                    {
                      Type backingFieldAddressType;
                      ESymValue backingFieldAddr = data.FieldAddress(obj, backingField, out backingFieldAddressType);
                      data.CopyValue(backingFieldAddr, valueAddr, backingFieldAddressType);
                    }
                  }
                  else
                  {
                    data.HavocFields(obj, this.parent.context.MethodContext.Modifies(method), ref data.modifiedAtCall);
                  }
                }

                ESymValue fieldAddr = data.PseudoFieldAddress(obj, getter, out pseudoFieldAddressType, true, this.CurrentMethodDeclaringType);
                data.CopyValue(fieldAddr, valueAddr, pseudoFieldAddressType);
                // don't forget to set dest, otherwise downstream decoding will miss some void address
                data.AssignValue(dest, mdDecoder.System_Void, this.CurrentMethodDeclaringType); // void is handled by assign

                return data;
              }
            }
            else // args > 2
            {
              Method getter;
              if (mdDecoder.HasGetter(property, out getter))
              {
                // multi variable uninterpreted function
                var nonOutArgs = GetNonOutArgs(method) - 1;
                ESymValue[] symArgs = new ESymValue[nonOutArgs];
                for (int i = 0, index = 0; i < args.Count - 1; i++)
                {
                  bool isOut;
                  Contract.Assume(!mdDecoder.IsStatic(method) || i < mdDecoder.Parameters(method).Count, "assuming IL is well-formed");

                  var key = KeyForPureFunctionArgument(method, i, args[i], data, substitution, out isOut);
                  if (!isOut)
                  {
                    symArgs[index++] = key;
                  }
                }

                ESymValue obj = data.Value(args[0]);
                if (dereferenceThis)
                {
                  obj = data.Value(obj);
                }
                Type pseudoFieldAddressType;
                ESymValue fieldAddr = data.PseudoFieldAddress(symArgs, getter, out pseudoFieldAddressType, false, this.CurrentMethodDeclaringType);
                AssignAllOutParameters(data, fieldAddr, method, args);
                var valueAddress = data.Address(args[args.Count - 1]);
                data.CopyValue(fieldAddr, valueAddress, pseudoFieldAddressType);

                // havoc whatever the setter havocs if we are inside the same class
                if (this.mdDecoder.Equal(this.mdDecoder.DeclaringType(this.parent.currentMethod), this.mdDecoder.Unspecialized(this.mdDecoder.DeclaringType(method))))
                {
                  data.HavocUp(obj, ref data.modifiedAtCall, false);
                  data.HavocFields(obj, this.parent.context.MethodContext.Modifies(method), ref data.modifiedAtCall);
                }
                else
                {
                  data.HavocModelProperties(obj, ref data.modifiedAtCall);
                }

              }
              else
              {
                // no getter, what should we do?
              }
              // don't forget to set dest, otherwise downstream decoding will miss some void address
              data.AssignValue(dest, mdDecoder.System_Void, this.CurrentMethodDeclaringType); // void is handled by assign

              return data;
            }
          }
          #endregion

          bool insideConstructor = this.mdDecoder.IsConstructor(this.parent.currentMethod);

          #region Havoc parameters
          HavocParameters<TypeList, ArgList>(pc, method, extraVarargs, args, data, declaringType, insideConstructor, false, dereferenceThis);
          #endregion

          // Deal with return type
          data.AssignReturnValue(dest, mdDecoder.ReturnType(method), this.CurrentMethodDeclaringType); // void is handled by assign

          return data;
        }

        private bool IsStructValue(Domain data, Temp source)
        {
          var raddr = data.Address(source);
          var rtype = data.GetType(raddr);
          if (rtype.IsNormal)
          {
            if (mdDecoder.IsManagedPointer(rtype.Value) && !mdDecoder.IsManagedPointer(mdDecoder.ElementType(rtype.Value)))
            {
              return true;
            }
          }
          return false;
        }
        
        private void AssignAllOutParameters<ArgList>(Domain data, ESymValue fieldAddr, Method method, ArgList args)
          where ArgList : IIndexable<int>
        {
          int index = 0;
          if (!mdDecoder.IsStatic(method)) { index++; } // skip this parameter actual

          foreach (var p in mdDecoder.Parameters(method).Enumerate())
          {
            if (mdDecoder.IsOut(p))
            {
              // we grab a pseudo sub structure of fieldAddr as the value of this out parameter
              var parameterType = mdDecoder.ParameterType(p);
              var outFieldAddress = data.PseudoFieldAddressOfOutParameter(index, fieldAddr, parameterType, this.CurrentMethodDeclaringType);
              Contract.Assume(index < args.Count, "invariant assuming bytecode is well formed and number of arguments at calls is correct");
              var dest = data.Value(args[index]);
              data.CopyValue(dest, outFieldAddress, parameterType);
            }
            index++;
          }
        }

        private int GetNonOutArgs(Method method)
        {
          int count = this.mdDecoder.IsStatic(method) ? 0 : 1;
          foreach (var p in mdDecoder.Parameters(method).Enumerate())
          {
            if (!mdDecoder.IsOut(p)) count++;
          }
          return count;
        }

        private Func<Type, Type> Compose(IFunctionalMap<Type, Type> map, Func<Type, Type> inner)
        {
          if (map.Count == 0) return inner;
          if (inner == null)
          {
            return (type) => (map.Contains(type)) ? map[type] : type;
          }
          return (type) =>
          {
            type = inner(type);
            if (map.Contains(type)) return map[type];
            return type;
          };
        }

        private Func<Type, Type> ComputeTypeInstantiationMap(APC pc, Method method)
        {
          Func<Type, Type> substitution = null;
          for (var context = pc.SubroutineContext; context != null; context = context.Tail)
          {
            Method calledMethod;
            bool isNewObj;
            bool isVirtualCall;
            if (context.Head.One.IsMethodCallBlock(out calledMethod, out isNewObj, out isVirtualCall))
            {
              IFunctionalMap<Type, Type> map = FunctionalMap<Type, Type>.Empty;
              mdDecoder.IsSpecialized(calledMethod, ref map);
              substitution = Compose(map, substitution);
            }
            else if (context.Head.Two.IsMethodCallBlock(out calledMethod, out isNewObj, out isVirtualCall))
            {
              IFunctionalMap<Type, Type> map = FunctionalMap<Type, Type>.Empty;
              mdDecoder.IsSpecialized(calledMethod, ref map);
              substitution = Compose(map, substitution);
            }
          }
          return substitution;
        }

        [SuppressMessage("Microsoft.Contracts", "RequiresAtCall-!mdDecoder.IsStatic(method) || index < mdDecoder.Parameters(method).Count")]
        private ESymValue KeyForPureFunctionArgument(Method method, int index, Temp arg, Domain data, Func<Type, Type> specialization, out bool isOut)
        {
          Contract.Requires(index >= 0);
          Contract.Requires(index <= mdDecoder.Parameters(method).Count);
          Contract.Requires(!mdDecoder.IsStatic(method) || index < mdDecoder.Parameters(method).Count);

          bool isPrimitive;
          bool isByRef;
          Type type;
          bool isThis;
          if (!ParameterHasValueRepresentation(method, index, specialization, out isPrimitive, out isOut, out isByRef, out type, out isThis))
          {
            if (isThis && isByRef && IsStructValue(data, arg))
            {
              // special case for calls on structs, where we have the struct value on the stack rather than the address
              return data.StructId(data.Address(arg));
            }
            if (!isByRef)
            {
              // struct by value parameter
              return data.StructId(data.Address(arg));
            }
            else
            {
              // struct by ref
              return data.StructId(data.Value(arg));
            }
          }
          else
          {
            // ordinary non-struct parameter (but possibly primitive)
            var ptr = data.Value(arg);
            if (isByRef)
            {
              ptr = data.Value(ptr);
            }
            if (isPrimitive || IsImmutableType(type) || contractDecoder.IsMutableHeapIndependent(method))
            {
              return ptr;
            }
            else
            {
              return data.ObjectVersion(ptr);
            }
          }
        }

        private bool IsImmutableType(Type type)
        {
          return this.mdDecoder.IsDelegate(type);
        }

        private void DevirtualizeImplementingMethod(Type type, ref Method method)
        {
          Method implementing;
          if (mdDecoder.TryGetImplementingMethod(type, method, out implementing))
          {
            method = implementing;
          }
        }


        Type Specialize(Func<Type, Type> specialization, Type type)
        {
          if (specialization != null)
          {
            return specialization(type);
          }
          return type;
        }

        private bool ParameterHasValueRepresentation(Method method, int paramIndex, Func<Type, Type> specialization, out bool isPrimitive, out bool isOut, out bool isByRef, out Type type, out bool isThis)
        {
          Contract.Requires(paramIndex >= 0);
          Contract.Requires(paramIndex <= mdDecoder.Parameters(method).Count);
          Contract.Requires(!mdDecoder.IsStatic(method) || paramIndex < mdDecoder.Parameters(method).Count);

          if (mdDecoder.IsStatic(method))
          {
            isThis = false;
            var parameter = mdDecoder.Parameters(method)[paramIndex];
            type = mdDecoder.ParameterType(parameter);
            type = Specialize(specialization, type);
            isOut = mdDecoder.IsOut(parameter);
            if (mdDecoder.IsManagedPointer(type))
            {
              isByRef = true;
              type = mdDecoder.ElementType(type);
            }
            else
            {
              isByRef = false;
            }
            isPrimitive = mdDecoder.IsPrimitive(type);
            return mdDecoder.HasValueRepresentation(type);
          }
          else
          {
            // shift
            if (paramIndex == 0)
            {
              // instance arg 
              isThis = true;
              isOut = false;
              type = mdDecoder.DeclaringType(method);
              isPrimitive = mdDecoder.IsPrimitive(type);
              var hasValueRep = mdDecoder.HasValueRepresentation(type);
              if (isPrimitive || !hasValueRep)
              {
                isByRef = true;
              }
              else
              {
                isByRef = false;
              }
              return hasValueRep;
            }
            else
            {
              isThis = false;
              var parameter = mdDecoder.Parameters(method)[paramIndex - 1];
              type = mdDecoder.ParameterType(parameter);
              type = Specialize(specialization, type);
              if (mdDecoder.IsManagedPointer(type))
              {
                isByRef = true;
                type = mdDecoder.ElementType(type);
              }
              else
              {
                isByRef = false;
              }
              isPrimitive = mdDecoder.IsPrimitive(type);
              isOut = mdDecoder.IsOut(parameter);
              return mdDecoder.HasValueRepresentation(type);
            }
          }
        }

        [ContractVerification(false)]
        private void HavocParameters<TypeList, ArgList>(
          APC pc,
          Method method,
          TypeList extraVarargs,
          ArgList args,
          Domain data,
          Type declaringType,
          bool insideConstructor,
          bool thisArgMissing, // this happens in newObj
          bool derefThis // true if constrained virtcall and we know it's on a reference type
        )
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<int>
        {
          // Deal with by-ref parameters: note, we have to handle "this" and varargs too here.
          IIndexable<Parameter> parameters = this.mdDecoder.Parameters(method);
          IIndexable<Parameter> genericMethodParameters = null;
          if (this.mdDecoder.IsSpecialized(method))
          {
            genericMethodParameters = this.mdDecoder.Parameters(this.mdDecoder.Unspecialized(method));
            Contract.Assume(parameters.Count == genericMethodParameters.Count);
          }

          for (int i = 0, param = 0, optThis = 0; i < args.Count; i++)
          {
            Contract.Assert(param <= i);
            Contract.Assert(optThis <= 1);

            // find type corresponding to args[i]
            bool isOut = false;
            bool materializeAfterCall = true;
            bool isNonFirstThis = false;
            Temp arg = args[i];
            Type pt;
            bool isGenericArgType = false;
            bool isThisConstructorCall = false; // constructor call on same class : this(...)

            if (i == 0 && !thisArgMissing && !mdDecoder.IsStatic(method))
            {
              optThis = 1;
              Parameter thisParam = mdDecoder.This(method);
              pt = mdDecoder.ParameterType(thisParam);
              if (this.mdDecoder.IsConstructor(method))
              {
                // a base call is a constructor call from a constructor with "this", and the target constructor is declared in a different type
                if (insideConstructor &&
                    data.IsThis(this.parent.currentMethod, arg)
                  )
                {
                  if (TypesEqualModuloInstantiation(declaringType, this.mdDecoder.DeclaringType(this.parent.currentMethod)))
                  {
                    // havoc even readonly fields.
                    isThisConstructorCall = true;
                  }
                  else
                  {
                    // base call, don't havoc "this"
                    ESymValue pvalue = data.Value(arg);
                    var ptype = data.GetType(pvalue);
                    if (ptype.IsNormal) { pt = ptype.Value; }
                    data.MaterializeThisAtCallAccordingToType(pvalue, pt, 0, this.CurrentMethodDeclaringType);
                    continue;
                  }
                }
                if (this.mdDecoder.IsStruct(declaringType))
                {
                  // a struct constructor call is similar to newObj, so we should materialize here
                  isOut = true;
                }
              }
              if (mdDecoder.IsPrimitive(declaringType) || mdDecoder.Equal(declaringType, mdDecoder.System_Object))
              {
                // primitive types (Int32,...) are mostly structs and we call their methods by ref, but
                // they don't change the value, so let's not havoc them
                // Furthermore, let's materialize their value
                // Also, methods on them don't change the receiver.
                if (mdDecoder.IsStruct(declaringType))
                {
                  data.Value(data.Value(args[0]));
                }
                continue;
              }
              if (data.IsThis(this.parent.currentMethod, arg))
              {
                // call on this. rematerialize fields afterwards
                materializeAfterCall = true;
              }
            }
            else if (param < parameters.Count)
            {
              // non-first parameter or non-instance: don't havoc "this" argument
              if (data.IsThis(this.parent.currentMethod, arg)) { isNonFirstThis = true; }

              Parameter p = parameters[param];
              isOut = mdDecoder.IsOut(p);
              pt = mdDecoder.ParameterType(p);
              if (genericMethodParameters != null)
              {
                Contract.Assume(parameters.Count == genericMethodParameters.Count, "Clousot should be able to prove this");
                Parameter gp = genericMethodParameters[param];
                var gpt = mdDecoder.ParameterType(gp);
                isGenericArgType = (mdDecoder.IsFormalTypeParameter(gpt) || mdDecoder.IsMethodFormalTypeParameter(gpt));
              }
              param++;
            }
            else
            {
              // vararg parameter: don't havoc "this" argument
              if (data.IsThis(this.parent.currentMethod, arg)) { continue; }

              Contract.Assume(i - param - optThis < extraVarargs.Count, "assuming IL is well-formed");
              pt = extraVarargs[i - param - optThis];
            }
            // havoc the parameter if it isn't a struct, the method isn't pure, and the method isn't a method on object.
            // and we are not in a contract
            if (!pc.InsideContract)
            {
              bool aggressive = AggressiveUpHavocMethod(method);
              if (aggressive || isOut || MustHavocParameter(i, method, declaringType, pt, isGenericArgType, isNonFirstThis))
              {
                ESymValue pvalue = data.Value(arg);
                if (derefThis && i == 0 && optThis == 1)
                {
                  pvalue = data.Value(pvalue);
                }
                var ptype = data.GetType(pvalue);
                if (ptype.IsNormal)
                {
                  pt = ptype.Value; // specialize type so we can materialize more meaningful fields
                }
                data.HavocObjectAtCall(pvalue, ref data.modifiedAtCall, aggressive, isThisConstructorCall);
                if (isOut)
                {
                  // materialize
                  data.MaterializeOutParameterAccordingToType(pvalue, pt, 0, this.CurrentMethodDeclaringType);
                }
                else if (materializeAfterCall)
                {
                  data.MaterializeThisAtCallAccordingToType(pvalue, pt, 0, this.CurrentMethodDeclaringType);
                }
              }
            }
            // consume the args
            data.Havoc(arg);
          }
        }

        /// <summary>
        /// Duplicate in ControlFlow.cs
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private bool AggressiveUpHavocMethod(Method method)
        {
          var t = mdDecoder.DeclaringType(method);
          if (mdDecoder.Name(t) != "Monitor") return false;
          var name = mdDecoder.Name(method);
          if (name == "Exit" || name == "Wait") return true;
          return false;
        }



        private bool TypesEqualModuloInstantiation(Type a, Type b)
        {
          a = mdDecoder.Unspecialized(a);
          b = mdDecoder.Unspecialized(b);
          return this.mdDecoder.Equal(a, b);
        }

        private bool MustHavocParameter(int parameterPosition, Method method, Type declaringType, Type pt, bool parameterHasGenericType, bool nonFirstThis)
        {
          Contract.Requires(parameterPosition >= 0);

          // parameters of generic type in generic code are generally not havoced, so use that heuristic here
          if (parameterHasGenericType) return false;
          if (mdDecoder.IsStruct(pt)) return false;
          if (contractDecoder.IsPure(method)) return false;
          if (mdDecoder.IsPure(method, parameterPosition)) return false;
          if (mdDecoder.Equal(declaringType, mdDecoder.System_Object)) return false;
          if (IsImmutable(pt)) return false; // immutable types not modded
          if (nonFirstThis) return false; // this passed as arg usually not modded
          if (mdDecoder.Equal(pt, mdDecoder.System_Object)) return false; // object typed parameters usually not modded
          return true;
        }

        private bool IsImmutable(Type pt)
        {
          if (mdDecoder.Equal(pt, mdDecoder.System_String)) return true;
          return false;
        }

        public Domain Calli<TypeList, ArgList>(APC pc, Type returnType, TypeList argTypes, bool tail, bool isInstance, int dest, int fp, ArgList args, Domain data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<int>
        {
          if (data.oldDomain == null)
          {
            return CalliEffect(pc, returnType, argTypes, tail, isInstance, dest, fp, args, data);
          }
          else
          {
            // perform call on old state and map result back (if any)
            data.oldDomain = CalliEffect(pc, returnType, argTypes, tail, isInstance, dest, fp, args, data.oldDomain);
            if (!mdDecoder.IsVoid(returnType))
            {
              data.oldDomain.CopyOldValue(data.oldDomain.beginOldPC, dest, dest, data, true);
            }
            return data;
          }
        }
        [ContractVerification(false)]
        private Domain CalliEffect<TypeList, ArgList>(APC pc, Type returnType, TypeList argTypes, bool tail, bool isInstance, int dest, int fp, ArgList args, Domain data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<int>
        {
          Contract.Assume(args.Count >= argTypes.Count);
          Contract.Assume(args.Count <= argTypes.Count + 1);
          Contract.Assume(!isInstance || args.Count == argTypes.Count + 1);
          Contract.Assume(isInstance || args.Count == argTypes.Count);

          // Deal with by-ref parameters
          for (int i = 0; i < args.Count; i++)
          {
            Temp arg = args[i];
            // find type corresponding to args[i]
            Type pt;
            if (isInstance)
            {
              if (i > 0)
              {
                pt = argTypes[i - 1];
              }
              else
              {
                // no static type info, must grab it from our state:
                var self = data.Value(arg);
                var atype = data.GetType(self);
                if (atype.IsNormal)
                {
                  pt = atype.Value;
                }
                else
                {
                  pt = mdDecoder.System_Object;
                }
              }
            }
            else
            {
              pt = argTypes[i];
            }
            if (!mdDecoder.IsStruct(pt))
            {
              data.ResetModifiedAtCall();
              data.HavocObjectAtCall(data.Value(arg), ref data.modifiedAtCall, false, false);
            }
            // consume the args
            data.Havoc(arg);
          }
          // Deal with result
          data.AssignValue(dest, returnType, this.CurrentMethodDeclaringType);

          return data;
        }

        public Domain Ckfinite(APC pc, int dest, int source, Domain data)
        {
          data.Copy(dest, source);
          if (data.oldDomain != null)
          {
            data.oldDomain.Copy(dest, source);
          }
          return data;
        }

        public Domain Cpblk(APC pc, bool @volatile, int destaddr, int srcaddr, int len, Domain data)
        {

          Contract.Assume(data.insideOld == 0);
          // consume the args
          data.Havoc(destaddr);
          data.Havoc(srcaddr);
          data.Havoc(len);
          return data;
        }

        public Domain Endfilter(APC pc, int decision, Domain data)
        {
          Contract.Assume(data.insideOld == 0);
          data.Havoc(decision);
          return data;
        }

        public Domain Endfinally(APC pc, Domain data)
        {
          Contract.Assume(data.insideOld == 0);

          return data;
        }

        public Domain Initblk(APC pc, bool @volatile, int destaddr, int value, int len, Domain data)
        {
          Contract.Assume(data.insideOld == 0);

          // consume the args
          data.Havoc(destaddr);
          data.Havoc(value);
          data.Havoc(len);
          return data;
        }

        public Domain Jmp(APC pc, Method method, Domain data)
        {
          Contract.Assume(data.insideOld == 0);

          return data;
        }

        public Domain Ldarg(APC pc, Parameter argument, bool isOld, int dest, Domain data)
        {
          LdargEffect(argument, isOld, dest, data);
          if (data.oldDomain != null)
          {
            LdargEffect(argument, isOld, dest, data.oldDomain);
          }
          return data;
        }

        private void LdargEffect(Parameter argument, bool isOld, int dest, Domain data)
        {
          if (isOld)
          {
            data.CopyValue(data.Address(dest), data.OldValueAddress(argument), mdDecoder.ManagedPointer(mdDecoder.ParameterType(argument)));
          }
          else
          {
            data.CopyValue(data.Address(dest), data.Address(argument), mdDecoder.ManagedPointer(mdDecoder.ParameterType(argument)));
          }
        }

        public Domain Ldarga(APC pc, Parameter argument, bool isOld, int dest, Domain data)
        {
          // if we do this in the old state, we find the parameter shadow.
          LdargaEffect(argument, isOld, dest, data);
          if (data.oldDomain != null)
          {
            LdargaEffect(argument, isOld, dest, data.oldDomain);
          }
          return data;
        }

        private void LdargaEffect(Parameter argument, bool isOld, int dest, Domain data)
        {
          var address = isOld ? data.OldValueAddress(argument) : data.Address(argument);

          data.CopyAddress(data.Address(dest), address, mdDecoder.ManagedPointer(mdDecoder.ParameterType(argument)));
          data.ManifestWritableBytes(data.Value(dest));
        }

        public Domain Ldconst(APC pc, object constant, Type type, int dest, Domain data)
        {
          data.AssignConst(dest, type, constant);
          if (data.oldDomain != null)
          {
            data.oldDomain.AssignConst(dest, type, constant);
          }
          return data;
        }

        public Domain Ldnull(APC pc, int dest, Domain data)
        {
          data.AssignNull(dest);
          if (data.oldDomain != null)
          {
            data.oldDomain.AssignNull(dest);
          }
          return data;
        }

        public Domain Ldftn(APC pc, Method method, int dest, Domain data)
        {
          data.AssignMethodPointer(dest, mdDecoder.System_UIntPtr, method);
          if (data.oldDomain != null)
          {
            data.oldDomain.AssignMethodPointer(dest, mdDecoder.System_UIntPtr, method);
          }
          return data;
        }

        /// <summary>
        /// Note: type here only indicates CLR runtime type, not the actual content type if it is an object type.
        /// </summary>
        public Domain Ldind(APC pc, Type type, bool @volatile, int dest, int ptr, Domain data)
        {

          if (data.oldDomain != null)
          {
            // perform in old state and map to new state
            LdindEffect(type, dest, ptr, data.oldDomain);
            data.oldDomain.CopyOldValue(data.oldDomain.beginOldPC, dest, dest, data, true);
          }
          else
          {
            LdindEffect(type, dest, ptr, data);
          }
          return data;
        }

        private void LdindEffect(Type type, int dest, int ptr, Domain data)
        {
          ESymValue srcPtr = data.Value(ptr);
          ESymValue destAddr = data.Address(dest);
          if (mdDecoder.Equal(type, mdDecoder.System_Object))
          {
            // ldind.ref has no type information
            data.CopyValue(destAddr, srcPtr, data.GetType(srcPtr).Type);
          }
          else
          {
            data.CopyValue(destAddr, srcPtr, mdDecoder.ManagedPointer(type));
          }
        }
        public Domain Ldobj(APC pc, Type type, bool @volatile, int dest, int ptr, Domain data)
        {
          return data;
        }



        public Domain Ldloc(APC pc, Local local, int dest, Domain data)
        {
          LdlocEffect(local, dest, data);
          if (data.oldDomain != null)
          {
            // local variables were likely assigned since the old program point and need to be 
            // read from the current state.
            data.CopyValueToOldState(data.oldDomain.beginOldPC, mdDecoder.LocalType(local), dest, dest, data.oldDomain);
          }
          return data;
        }

        private void LdlocEffect(Local local, int dest, Domain data)
        {
          data.CopyValue(data.Address(dest), data.Address(local), mdDecoder.ManagedPointer(mdDecoder.LocalType(local)));
        }

        public Domain Ldloca(APC pc, Local local, int dest, Domain data)
        {
          LdlocaEffect(local, dest, data);
          if (data.oldDomain != null)
          {
            LdlocaEffect(local, dest, data.oldDomain);
          }
          return data;
        }

        private void LdlocaEffect(Local local, int dest, Domain data)
        {
          data.CopyAddress(data.Address(dest), data.Address(local), mdDecoder.ManagedPointer(mdDecoder.LocalType(local)));
          data.ManifestWritableBytes(data.Value(dest));
        }

        public Domain Localloc(APC pc, int dest, int size, Domain data)
        {
          Contract.Assume(data.insideOld == 0);

          ESymValue buffer = data.LocalAlloc(data.Value(size));
          data.CopyAddress(data.Address(dest), buffer, mdDecoder.System_UIntPtr);

          return data;
        }

        public Domain Nop(APC pc, Domain data)
        {
          return data;
        }

        public Domain Pop(APC pc, int source, Domain data)
        {
          data.Havoc(source);
          if (data.oldDomain != null)
          {
            data.oldDomain.Havoc(source);
          }
          return data;
        }

        public Domain Return(APC pc, int source, Domain data)
        {
          Contract.Assume(data.insideOld == 0);
          data.Havoc(source);
          return data;
        }

        public Domain Starg(APC pc, Parameter argument, int source, Domain data)
        {
          Contract.Assume(data.insideOld == 0);
          data.CopyValue(data.Address(argument), data.Address(source), mdDecoder.ManagedPointer(mdDecoder.ParameterType(argument)));
          data.Havoc(source);
          return data;
        }

        public Domain Stind(APC pc, Type type, bool @volatile, int ptr, int value, Domain data)
        {
          Contract.Assume(data.insideOld == 0);
          ESymValue valueAddr = data.Address(value);
          ESymValue destAddr = data.Value(ptr);
          if (mdDecoder.Equal(type, mdDecoder.System_Object))
          {
            // no info on type ldind.ref
            data.CopyValue(destAddr, valueAddr, data.GetType(valueAddr).Type, false, false);
          }
          else
          {
            data.CopyValue(destAddr, valueAddr, mdDecoder.ManagedPointer(type), false, false);
          }
          data.Havoc(ptr);
          data.Havoc(value);
          return data;
        }

        public Domain Stloc(APC pc, Local local, int source, Domain data)
        {
          StlocEffect(local, source, data);
          if (data.oldDomain != null)
          {
            StlocEffect(local, source, data.oldDomain);
          }
          return data;
        }

        private void StlocEffect(Local local, int source, Domain data)
        {
          data.CopyValue(data.Address(local), data.Address(source), this.mdDecoder.ManagedPointer(this.mdDecoder.LocalType(local)));
          data.Havoc(source);
        }

        public Domain Switch(APC pc, Type type, IEnumerable<Pair<object, APC>> cases, int value, Domain data)
        {
          throw new NotImplementedException("Should only see assumes");
        }

        public Domain Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, int dest, int source, Domain data)
        {
          UnaryEffect(op, dest, source, data);
          if (data.oldDomain != null)
          {
            UnaryEffect(op, dest, source, data.oldDomain);
          }
          return data;
        }

        private void UnaryEffect(UnaryOperator op, int dest, int source, Domain data)
        {
          switch (op)
          {
            case UnaryOperator.WritableBytes:
              data.AssignWritableBytes(data.Address(dest), data.Value(source));
              break;

            case UnaryOperator.Conv_u:
              // retain type and nullness
              //var type = data.GetType(data.Value(source));
              //if (type.IsNormal && parent.mdDecoder.IsStruct(type.Value))
              //{
              //  goto default;
              //}
              data.AssignValueAndNullnessAtConv_IU(dest, source, true);
              break;

            case UnaryOperator.Conv_i:
              // retain type and nullness
              data.AssignValueAndNullnessAtConv_IU(dest, source, false);
              break;

            case UnaryOperator.Not:
              data.AssignSpecialUnary(dest, data.Constructors.UnaryNot, source, mdDecoder.System_Int32, this.CurrentMethodDeclaringType);
              break;

            default:
              data.AssignPureUnary(dest, op, data.UnaryResultType(op, data.CurrentType(source)), source);
              break;
          }
        }

        public Domain Box(APC pc, Type type, int dest, int source, Domain data)
        {
          BoxEffect(type, dest, source, data);
          if (data.oldDomain != null)
          {
            BoxEffect(type, dest, source, data.oldDomain);
          }
          return data;
        }

        private void BoxEffect(Type type, int dest, int source, Domain data)
        {
#if false
          if (this.mdDecoder.IsStruct(type))
          {
            // definitely a struct
            data.AssignValue(dest, mdDecoder.System_Object);
          }
          else
#endif
          if (this.mdDecoder.IsReferenceConstrained(type))
          {
            // definitely a reference type
            data.Copy(dest, source); // noop
          }
          else
          {
            var sourceAddress = data.Address(source);
            var objecttype = mdDecoder.System_Object;
            var specialBoxValue = data.GetSpecialUnary(data.Constructors.BoxOperator, source, objecttype);
            data.SetValue(specialBoxValue, sourceAddress, mdDecoder.ManagedPointer(type), false);
            data.AssignSpecialUnary(dest, specialBoxValue, objecttype, this.CurrentMethodDeclaringType);
          }
        }

        public Domain ConstrainedCallvirt<TypeList, ArgList>(APC pc, Method method, bool tail, Type constraint, TypeList extraVarargs, int dest, ArgList args, Domain data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<int>
        {
          if (data.oldDomain != null)
          {
            // perform call in old state and map result back
            data.oldDomain = CallFactored<TypeList, ArgList>(pc, method, tail, true, extraVarargs, dest, args, data.oldDomain, constraint, true);
            if (!mdDecoder.IsVoidMethod(method))
            {
              data.oldDomain.CopyOldValue(data.oldDomain.beginOldPC, dest, dest, data, true);
            }
            return data;
          }
          else
          {
            return CallFactored<TypeList, ArgList>(pc, method, tail, true, extraVarargs, dest, args, data, constraint, true);
          }
        }

        public Domain Castclass(APC pc, Type type, int dest, int obj, Domain data)
        {
          CastclassEffect(type, dest, obj, data);
          if (data.oldDomain != null)
          {
            CastclassEffect(type, dest, obj, data.oldDomain);
          }
          return data;
        }

        private void CastclassEffect(Type type, int dest, int obj, Domain data)
        {
          ESymValue destaddr = data.Address(dest);
          Type targetAddrType = mdDecoder.ManagedPointer(type);
          data.CopyValueAndCast(destaddr, data.Address(obj), targetAddrType);
        }

        public Domain Cpobj(APC pc, Type type, int destptr, int srcptr, Domain data)
        {
          Contract.Assume(data.insideOld == 0);
          data.CopyValue(data.Value(destptr), data.Value(srcptr), mdDecoder.ManagedPointer(type));
          data.Havoc(destptr);
          data.Havoc(srcptr);
          return data;
        }

        public Domain Initobj(APC pc, Type type, int ptr, Domain data)
        {
          Contract.Assume(data.insideOld == 0);
          // sets all fields of the given struct to null or
          // if generic and of reference type, the entire thing to null
          ESymValue destaddr = data.Value(ptr);
          data.InitObj(destaddr, type);
          data.Havoc(ptr);
          return data;
        }

        public Domain Isinst(APC pc, Type type, int dest, int obj, Domain data)
        {
          IsInstEffect(type, dest, obj, data);
          if (data.oldDomain != null)
          {
            IsInstEffect(type, dest, obj, data.oldDomain);
          }
          return data;
        }

        private void IsInstEffect(Type type, int dest, int source, Domain data)
        {
          if (mdDecoder.IsStruct(type))
          {
            data.AssignValue(dest, mdDecoder.System_Object, this.CurrentMethodDeclaringType);
          }
          else
          {
            var value = data.Value(source);
            var stype = data.GetType(value);
            if (stype.IsNormal)
            {
              if (this.mdDecoder.DerivesFromIgnoringTypeArguments(stype.Value, type))
              {
                // will succeed
                data.Copy(dest, source);
                return;
              }
            }
            data.AssignIsInst(type, source, dest, this.CurrentMethodDeclaringType);
          }
        }

        public Domain Ldelem(APC pc, Type type, int dest, int array, int index, Domain data)
        {
          Contract.Assume(index > array, "needed by LdelemEffect below");

          if (data.oldDomain != null)
          {
            LdelemEffect(type, dest, array, index, data.oldDomain);
            data.oldDomain.CopyOldValue(data.oldDomain.beginOldPC, dest, dest, data, true);
          }
          else
          {
            LdelemEffect(type, dest, array, index, data);
          }
          return data;
        }

        private void LdelemEffect(Type type, int dest, int array, int index, Domain data)
        {
          Contract.Requires(index > array);// F: Added from Clousot precondition suggestion

          Type elementType = data.GetArrayElementType(type, array);
          Type elementAddrType = mdDecoder.ManagedPointer(elementType);
          data.CopyValue(data.Address(dest), data.ElementAddress(data.Value(array), data.Value(index), elementAddrType), elementAddrType);
          System.Diagnostics.Debug.Assert(index > array);
          data.Havoc(index);
        }


        public Domain Ldelema(APC pc, Type type, bool @readonly, int dest, int array, int index, Domain data)
        {
          Contract.Assume(index > array, "needed by LdelemaEffect below");

          LdelemaEffect(type, dest, array, index, data);
          if (data.oldDomain != null)
          {
            LdelemaEffect(type, dest, array, index, data.oldDomain);
          }
          return data;
        }

        private void LdelemaEffect(Type type, int dest, int array, int index, Domain data)
        {
          Contract.Requires(index > array);

          Type elementAddrType = mdDecoder.ManagedPointer(type);
          data.CopyAddress(data.Address(dest), data.ElementAddress(data.Value(array), data.Value(index), elementAddrType), elementAddrType);
          data.ManifestWritableBytes(data.Value(dest));
          System.Diagnostics.Debug.Assert(index > array);
          data.Havoc(index);
        }

        public Domain Ldfld(APC pc, Field field, bool @volatile, int dest, int obj, Domain data)
        {
          if (data.oldDomain != null)
          {
            // perform on old state and copy back
            LdfldEffect(field, dest, obj, data.oldDomain);
            data.oldDomain.CopyOldValue(data.oldDomain.beginOldPC, dest, dest, data, true);
          }
          else
          {
            LdfldEffect(field, dest, obj, data);
          }
          return data;
        }

        /// <summary>
        /// There are 2 cases to handle here.
        /// 1) the common case is that there is a pointer on the stack (to an object or a struct)
        /// 2) the uncommon case, when there is an actual struct value on the stack.
        /// </summary>
        private void LdfldEffect(Field field, int dest, int obj, Domain data)
        {
          Type declaringType = mdDecoder.DeclaringType(field);
          ESymValue source;
          if (mdDecoder.IsStruct(declaringType))
          {
            // inspect the stack type
            AbstractType objType = data.GetType(data.Address(obj));
            if (objType.IsNormal && mdDecoder.IsManagedPointer(objType.Value) && mdDecoder.IsStruct(mdDecoder.ElementType(objType.Value)))
            {
              // use address of stack instead for struct
              source = data.Address(obj);
            }
            else
            {
              source = data.Value(obj);
            }
          }
          else
          {
            source = data.Value(obj);
          }
          Type fieldAddressType;
          ESymValue fieldAddr = data.FieldAddress(source, field, out fieldAddressType);
          data.CopyValue(data.Address(dest), fieldAddr, fieldAddressType);
        }

        public Domain Ldflda(APC pc, Field field, int dest, int obj, Domain data)
        {
          LdfldaEffect(pc, field, dest, obj, data);
          if (data.oldDomain != null)
          {
            LdfldaEffect(pc, field, dest, obj, data.oldDomain);
          }
          return data;
        }

        private void LdfldaEffect(APC pc, Field field, int dest, int obj, Domain data)
        {
          var objectValue = data.Value(obj);
          Type fieldAddressType;
          ESymValue fieldAddr = data.FieldAddress(objectValue, field, out fieldAddressType);
          data.CopyAddress(data.Address(dest), fieldAddr, fieldAddressType);
          // invalidate properties that may depend on "field" unless we are in a contract
          if (!pc.InsideContract)
          {
            if (this.mdDecoder.Equal(this.mdDecoder.DeclaringType(this.parent.currentMethod),
                                     this.mdDecoder.Unspecialized(this.mdDecoder.DeclaringType(field))))
            {
              data.HavocPseudoFields(this.parent.context.MethodContext.AffectedGetters(field), objectValue);
              // data.HavocUp(objectValue);
            }
          }
          data.ManifestWritableBytes(data.Value(dest));
        }

        public Domain Ldlen(APC pc, int dest, int array, Domain data)
        {
          LdlenEffect(dest, array, data);
          if (data.oldDomain != null)
          {
            LdlenEffect(dest, array, data.oldDomain);
          }
          return data;
        }

        private static void LdlenEffect(int dest, int array, Domain data)
        {
          data.AssignArrayLength(data.Address(dest), data.Value(array));
        }

        public Domain Ldsfld(APC pc, Field field, bool @volatile, int dest, Domain data)
        {
          if (data.oldDomain != null)
          {
            // perform on old state and copy back
            LdsfldEffect(field, dest, data.oldDomain);
            data.oldDomain.CopyOldValue(data.oldDomain.beginOldPC, dest, dest, data, true);
          }
          else
          {
            LdsfldEffect(field, dest, data);
          }
          return data;
        }

        private void LdsfldEffect(Field field, int dest, Domain data)
        {
          ESymValue fieldAddr = data.TryGetFieldAddress(data.Globals, field);

          // handle the static anonymous delegate caches as follows:
          //  - the lazy initialization causes us trouble if we merge the init/uninit branches
          //  - so we try to detect when we don't know the closure being loaded and in that case set the result to null
          //  - this will analyze the uninit branch and ignore the init branch at the merge (because OptHeap understands null)
          if (ReInitCachedDelegate(fieldAddr, field, data) || ReInitDynamicCallSite(fieldAddr, field, data))
          {
            // make sure to take the initializing path
            data.AssignNull(dest);
            return;
          }

          Type fieldAddrType;
          fieldAddr = data.FieldAddress(data.Globals, field, out fieldAddrType);
          data.CopyValue(data.Address(dest), fieldAddr, fieldAddrType);
        }

        private bool ReInitDynamicCallSite(ESymValue fieldAddr, Field field, Domain data)
        {
          var declType = mdDecoder.DeclaringType(field);
          declType = mdDecoder.Unspecialized(declType);
          if (!this.mdDecoder.IsCompilerGenerated(declType)) return false;
          if (!this.mdDecoder.Name(field).Contains("__Site")) return false;
          if (fieldAddr == null)
          {
            // first time we see it          
            return true;
          }
          var val = data.TryValue(fieldAddr);
          if (val == null) return true;
          var typ = data.GetType(val);
          if (!typ.IsNormal) return true;
          var callsite = mdDecoder.Unspecialized(typ.Value);
          if (!mdDecoder.Name(callsite).StartsWith("CallSite")) return true;
          return false;
        }

        private bool ReInitCachedDelegate(ESymValue fieldAddr, Field field, Domain data)
        {
          if (!this.mdDecoder.Name(field).Contains("__CachedAnonymousMethodDelegate")) return false;
          if (fieldAddr == null)
          {
            // first time we see it          
            return true;
          }
          var val = data.TryValue(fieldAddr);
          if (val == null) return true;
          ESymValue targetObject;
          Method targetMethod;
          if (!data.IsDelegateValue(val, out targetObject, out targetMethod)) return true;

          return false;
        }

        public Domain Ldsflda(APC pc, Field field, int dest, Domain data)
        {
          LdsfldaEffect(field, dest, data);
          if (data.oldDomain != null)
          {
            LdsfldaEffect(field, dest, data.oldDomain);
          }
          return data;
        }

        private static void LdsfldaEffect(Field field, int dest, Domain data)
        {
          Type fieldAddrType;
          ESymValue fieldAddr = data.FieldAddress(data.Globals, field, out fieldAddrType);

          data.CopyAddress(data.Address(dest), fieldAddr, fieldAddrType);
          data.ManifestWritableBytes(data.Value(dest));
        }

        public Domain Ldtypetoken(APC pc, Type type, int dest, Domain data)
        {
          data.AssignTokenValue(dest, mdDecoder.System_RuntimeTypeHandle, type);
          if (data.oldDomain != null)
          {
            data.oldDomain.AssignTokenValue(dest, mdDecoder.System_RuntimeTypeHandle, type);
          }
          return data;
        }

        public Domain Ldfieldtoken(APC pc, Field field, int dest, Domain data)
        {
          data.AssignTokenValue(dest, mdDecoder.System_RuntimeFieldHandle, field);
          if (data.oldDomain != null)
          {
            data.oldDomain.AssignTokenValue(dest, mdDecoder.System_RuntimeFieldHandle, field);
          }
          return data;
        }

        public Domain Ldmethodtoken(APC pc, Method method, int dest, Domain data)
        {
          data.AssignTokenValue(dest, mdDecoder.System_RuntimeMethodHandle, method);
          if (data.oldDomain != null)
          {
            data.oldDomain.AssignTokenValue(dest, mdDecoder.System_RuntimeMethodHandle, method);
          }
          return data;
        }

        public Domain Ldvirtftn(APC pc, Method method, int dest, int obj, Domain data)
        {
          data.AssignMethodPointer(dest, mdDecoder.System_UIntPtr, method);
          if (data.oldDomain != null)
          {
            data.oldDomain.AssignMethodPointer(dest, mdDecoder.System_UIntPtr, method);
          }
          return data;
        }

        public Domain Mkrefany(APC pc, Type type, int dest, int obj, Domain data)
        {
          data.AssignValue(dest, mdDecoder.System_DynamicallyTypedReference, this.CurrentMethodDeclaringType);
          if (data.oldDomain != null)
          {
            data.oldDomain.AssignValue(dest, mdDecoder.System_DynamicallyTypedReference, this.CurrentMethodDeclaringType);
          }
          return data;
        }

        public Domain Newarray<ArgList>(APC pc, Type type, int dest, ArgList lengths, Domain data)
          where ArgList : IIndexable<int>
        {
          NewarrayEffect<ArgList>(type, dest, lengths, data);
          if (data.oldDomain != null)
          {
            NewarrayEffect<ArgList>(type, dest, lengths, data.oldDomain);
          }
          return data;
        }

        private void NewarrayEffect<ArgList>(Type type, int dest, ArgList lengths, Domain data) where ArgList : IIndexable<int>
        {
          if (lengths.Count == 1)
          {
            ESymValue array = data.CreateArray(type, data.Value(lengths[0]));
            data.CopyAddress(data.Address(dest), array, mdDecoder.ArrayType(type, 1));
          }
          else
          {
            data.AssignValue(dest, type, this.CurrentMethodDeclaringType);
          }
        }

        public Domain Newobj<ArgList>(APC pc, Method ctor, int dest, ArgList args, Domain data)
          where ArgList : IIndexable<int>
        {
          NewobjEffect<ArgList>(pc, ctor, dest, args, data);
          if (data.oldDomain != null)
          {
            NewobjEffect<ArgList>(pc, ctor, dest, args, data.oldDomain);
          }
          return data;
        }

        private void NewobjEffect<ArgList>(APC pc, Method ctor, int dest, ArgList args, Domain data) where ArgList : IIndexable<int>
        {
          var savedClosure = args.Count >= 2 ? data.TryValue(data.Address(args[0])) : null;
          var savedFnPtr = args.Count >= 2 ? data.TryValue(data.Address(args[1])) : null;

          Type declaringType = mdDecoder.DeclaringType(ctor);
          if (mdDecoder.IsStruct(declaringType))
          {
            ESymValue addr = data.CreateValue(declaringType);
            data.MaterializeNewObjAccordingToType(addr, mdDecoder.ManagedPointer(declaringType), 0, this.CurrentMethodDeclaringType);

            this.HavocParameters(pc, ctor, EmptyIndexable<Type>.Empty, args, data, declaringType, false, true, false);

            data.CopyValue(data.Address(dest), addr, mdDecoder.ManagedPointer(declaringType));
          }
          else
          {
            ESymValue obj = data.CreateObject(declaringType);
            data.MaterializeNewObjAccordingToType(obj, declaringType, 0, this.CurrentMethodDeclaringType);
            if (mdDecoder.IsDelegate(declaringType) && savedFnPtr != null)
            {
              data.SetDelegateDetails(obj, savedClosure, savedFnPtr);
            }
            this.HavocParameters(pc, ctor, EmptyIndexable<Type>.Empty, args, data, declaringType, false, true, false);

            data.CopyAddress(data.Address(dest), obj, declaringType);
          }
        }

        public Domain Refanytype(APC pc, int dest, int source, Domain data)
        {
          data.AssignValue(dest, mdDecoder.System_Type, this.CurrentMethodDeclaringType);
          if (data.oldDomain != null)
          {
            data.oldDomain.AssignValue(dest, mdDecoder.System_Type, this.CurrentMethodDeclaringType);
          }
          return data;
        }

        public Domain Refanyval(APC pc, Type type, int dest, int source, Domain data)
        {
          data.AssignValue(dest, mdDecoder.ManagedPointer(type), this.CurrentMethodDeclaringType);
          if (data.oldDomain != null)
          {
            data.oldDomain.AssignValue(dest, mdDecoder.ManagedPointer(type), this.CurrentMethodDeclaringType);
          }
          return data;
        }

        public Domain Rethrow(APC pc, Domain data)
        {
          return data.Bottom;
        }

        public Domain Sizeof(APC pc, Type type, int dest, Domain data)
        {
          data.AssignValue(dest, mdDecoder.System_Int32, this.CurrentMethodDeclaringType);
          if (data.oldDomain != null)
          {
            data.oldDomain.AssignValue(dest, mdDecoder.System_Int32, this.CurrentMethodDeclaringType);
          }
          return data;
        }

        public Domain Stelem(APC pc, Type type, int array, int index, int value, Domain data)
        {
          // can happen in old state due to params calls
          StelemEffect(type, array, index, value, data);
          if (data.oldDomain != null)
          {
            StelemEffect(type, array, index, value, data.oldDomain);
          }
          return data;
        }

        private void StelemEffect(Type type, int array, int index, int value, Domain data)
        {
          Type elementAddrType = mdDecoder.ManagedPointer(type);
          var arrayValue = data.Value(array);
          var indexValue = data.Value(index);
          data.HavocArrayAtIndex(arrayValue, indexValue);
          data.CopyValue(data.ElementAddress(arrayValue, indexValue, elementAddrType), data.Address(value), elementAddrType);
          data.Havoc(array);
          data.Havoc(index);
          data.Havoc(value);
        }

        public Domain Stfld(APC pc, Field field, bool @volatile, int obj, int value, Domain data)
        {
          Contract.Assume(data.insideOld == 0);

          data.CopyValue(data.FieldAddress(data.Value(obj), field), data.Address(value));
          // invalidate properties that may depend on "field"
          if (!mdDecoder.IsCompilerGenerated(field))
          {
            data.HavocPseudoFields(data.Value(obj));
          }
          data.Havoc(obj);
          data.Havoc(value);
          return data;
        }

        public Domain Stsfld(APC pc, Field field, bool @volatile, int value, Domain data)
        {
          Contract.Assume(data.insideOld == 0);

          data.CopyValue(data.FieldAddress(data.Globals, field), data.Address(value));
          data.Havoc(value);
          return data;
        }

        public Domain Throw(APC pc, int exn, Domain data)
        {
          data.Havoc(exn);
          return data.Bottom;
        }

        public Domain Unbox(APC pc, Type type, int dest, int obj, Domain data)
        {
          data.AssignValue(dest, mdDecoder.ManagedPointer(type), this.CurrentMethodDeclaringType);
          if (data.oldDomain != null)
          {
            data.oldDomain.AssignValue(dest, mdDecoder.ManagedPointer(type), this.CurrentMethodDeclaringType);
          }
          return data;
        }

        public Domain Unboxany(APC pc, Type type, int dest, int obj, Domain data)
        {
          if (this.mdDecoder.IsReferenceConstrained(type))
          {
            // acts like a cast
            return Castclass(pc, type, dest, obj, data);
          }
          data.AssignValue(dest, type, this.CurrentMethodDeclaringType);
          if (data.oldDomain != null)
          {
            data.oldDomain.AssignValue(dest, type, this.CurrentMethodDeclaringType);
          }
          return data;
        }

        #endregion

        #region IVisitSynthIL<int,OptimisticHeapAbstraction<APC,Local,Parameter,Method,Field,Type>,OptimisticHeapAbstraction<APC,Local,Parameter,Method,Field,Type>> Members

        private Domain AssertEffect(APC pc, string tag, int source, Domain data)
        {
          data = data.Assume(source, true);
          if (!data.IsBottom)
          {
            data.Havoc(source);
          }
          return data;
        }
        public Domain Assert(APC pc, string tag, int source, object provenance, Domain data)
        {
          data = AssertEffect(pc, tag, source, data);
          if (data.oldDomain != null)
          {
            data.oldDomain = AssertEffect(pc, tag, source, data.oldDomain);
          }
          return data;
        }

        private static Domain AssumeEffect(APC pc, string tag, int source, Domain data)
        {
          if (tag == "false")
          {
            data = data.Assume(source, false);
          }
          else
          {
            data = data.Assume(source, true);
          }
          if (!data.IsBottom)
          {
            data.Havoc(source);
          }
          return data;
        }

        public Domain Assume(APC pc, string tag, int source, object provenance, Domain data)
        {
          if (this.parent.IgnoreExplicitAssumptions && tag == "assume")
          {
            return data;
          }
          data = AssumeEffect(pc, tag, source, data);
          if (data.oldDomain != null)
          {
            data.oldDomain = AssumeEffect(pc, tag, source, data.oldDomain);
          }
          return data;
        }

        private void MaterializeLocal(Local l, Domain data, Method method)
        {
          var t = mdDecoder.LocalType(l);
          data.AssignZeroEquivalent(data.Address(l), t);

          // Added by Francesco: Materialize up to one depth
#if false
          if (mdDecoder.IsClass(t))
          {
            foreach (var field in mdDecoder.Fields(t))
            {
              Type fieldType;
              var fieldAddress = data.FieldAddress(data.Address(l), field, out fieldType);
              data.MakeUnmodified(fieldAddress);
              data.SetType(fieldAddress, fieldType);
            }
          } 
#endif
        }

        private void MaterializeParameter(Parameter p, Domain data, Type fromType, bool aggressiveMaterialization)
        {
          var t = mdDecoder.ParameterType(p);
          data.Assign(p, t, this.CurrentMethodDeclaringType);
          MaterializeParameterInfo(data.Address(p), mdDecoder.ManagedPointer(t), 0, data, fromType, aggressiveMaterialization);
          data.CopyParameterIntoShadow(p);
        }

        private void MaterializeParameterInfo(ESymValue value, Type t, int depth, Domain data, Type fromType, bool aggressiveMaterialization)
        {
          data.MakeUnmodified(value);
          data.SetType(value, t);
          if (depth > 2 && !aggressiveMaterialization) return;
          if (depth > 5) return;
          if (mdDecoder.IsManagedPointer(t))
          {
            Type elementType = mdDecoder.ElementType(t);
            if (!mdDecoder.HasValueRepresentation(elementType))
            {
              // manifest struct Id
              data.ManifestStructId(value);
              // manifest field addresses
              foreach (Field f in mdDecoder.Fields(elementType))
              {
                if (mdDecoder.IsStatic(f)) continue;
                if (mdDecoder.IsVisibleFrom(f, fromType))
                {
                  // manifest the field
                  Type fieldType;
                  ESymValue fieldAddr = data.FieldAddress(value, f, out fieldType);
                  MaterializeParameterInfo(fieldAddr, fieldType, depth + 1, data, fromType, aggressiveMaterialization);
                }
              }
              ManifestProperties(value, depth + 1, data, fromType, aggressiveMaterialization, elementType);
            }
            else
            {
              // manifest the value
              MaterializeParameterInfo(data.Value(value), elementType, depth + 1, data, fromType, aggressiveMaterialization);
            }
            return;
          }

          if (data.DerivesFromIEnumerable(t))
          {
            ManifestProperties(value, depth, data, fromType, true, t);
          }

          if (mdDecoder.IsClass(t))
          {
            foreach (Field f in mdDecoder.Fields(t))
            {
              if (mdDecoder.IsStatic(f)) continue;
              if (mdDecoder.IsVisibleFrom(f, fromType))
              {
                // manifest the field
                Type fieldType;
                ESymValue fieldAddr = data.FieldAddress(value, f, out fieldType);
                MaterializeParameterInfo(fieldAddr, fieldType, depth + 1, data, fromType, aggressiveMaterialization);
              }
            }
            if (aggressiveMaterialization && mdDecoder.HasBaseClass(t))
            {
              // materialize base fields
              var baseType = mdDecoder.BaseClass(t);
              foreach (Field f in mdDecoder.Fields(baseType))
              {
                if (mdDecoder.IsStatic(f)) continue;
                if (mdDecoder.IsVisibleFrom(f, fromType))
                {
                  // manifest the field
                  Type fieldType;
                  ESymValue fieldAddr = data.FieldAddress(value, f, out fieldType);
                  MaterializeParameterInfo(fieldAddr, fieldType, depth + 1, data, fromType, aggressiveMaterialization);
                }
              }
            }
            return;
          }

          if (data.NeedsArrayLengthManifested(t))
          {
            data.ManifestArrayLength(value);
            return;
          }

          if (mdDecoder.IsUnmanagedPointer(t))
          {
            data.ManifestWritableBytes(value);
            return;
          }
        }


        private bool IsIEnumerable(Type t)
        {
          return mdDecoder.Name(t).Contains("IEnumerable");
        }

        private void ManifestProperties(ESymValue value, int depth, Domain data, Type fromType, bool aggressiveMaterialization, Type type)
        {
          Contract.Requires(data != null);

#if false
          if (!mdDecoder.IsArray(type) && data.DerivesFromIEnumerable(type))
          {
            data.ManifestModel(value, type);
          }
#endif
          foreach (Property p in mdDecoder.Properties(type))
          {
            if (mdDecoder.IsStatic(p)) continue;
            Method getter;
            if (!mdDecoder.HasGetter(p, out getter)) continue;

            // skip string.get_Length, as we use $Length for that
            if (mdDecoder.Equal(type, mdDecoder.System_String) && mdDecoder.Name(getter) == "get_Length") {
              continue;
            }
            if (mdDecoder.IsVisibleFrom(getter, fromType))
            {
              if (mdDecoder.Parameters(getter).Count > 0) continue;

              // manifest the property
              Type propertyType;
              ESymValue propAddr = data.PseudoFieldAddress(value, getter, out propertyType, false, this.CurrentMethodDeclaringType);
              MaterializeParameterInfo(propAddr, propertyType, depth + 1, data, fromType, aggressiveMaterialization);
            }
          }
          foreach (var m in parent.contractDecoder.ModelMethods(type))
          {
            if (mdDecoder.IsVisibleFrom(m, fromType))
            {
              // manifest the method
              Type methodType;
              ESymValue propAddr = data.PseudoFieldAddress(value, m, out methodType, false, this.CurrentMethodDeclaringType);
              MaterializeParameterInfo(propAddr, methodType, depth + 1, data, fromType, aggressiveMaterialization);
            }
          }
          if (this.mdDecoder.IsInterface(type))
          {
            foreach (var intf in mdDecoder.Interfaces(type))
            {
              if (mdDecoder.IsVisibleFrom(intf, fromType))
              {
                ManifestProperties(value, depth, data, fromType, aggressiveMaterialization, intf);
              }
            }
          }
        }

        public Domain Entry(APC pc, Method method, Domain data)
        {
          Contract.Assume(data.oldDomain == null);

          // meterialize all locals
          IIndexable<Local> locals = this.mdDecoder.Locals(method);
          for (int i = 0; i < locals.Count; i++)
          {
            MaterializeLocal(locals[i], data, method);
          }
          var declaringType = mdDecoder.DeclaringType(method);

          // materialize all parameters.
          IIndexable<Parameter> parameters = this.mdDecoder.Parameters(method);
          for (int i = 0; i < parameters.Count; i++)
          {
            MaterializeParameter(parameters[i], data, declaringType, false);
          }
          // this
          if (!mdDecoder.IsStatic(method))
          {
            MaterializeParameter(this.mdDecoder.This(method), data, declaringType, true);
          }
          // for constructors, set all fields to 0
          if (this.mdDecoder.IsConstructor(method))
          {
            Parameter thisParam = this.mdDecoder.This(method);
            ESymValue thisValue = data.Value(thisParam);
            foreach (Field f in this.mdDecoder.Fields(declaringType))
            {
              if (mdDecoder.IsStatic(f)) continue;
              Type fieldType = mdDecoder.FieldType(f);
              if (mdDecoder.IsStruct(fieldType))
              {
                data.AssignConst(data.FieldAddress(thisValue, f), fieldType, 0);
              }
              else
              {
                data.AssignNull(data.FieldAddress(thisValue, f));
              }
            }
            foreach (Property p in this.mdDecoder.Properties(declaringType))
            {
              if (mdDecoder.IsStatic(p)) continue;
              Method getter;
              if (mdDecoder.HasGetter(p, out getter))
              {
                // REVIEW: really IsCompilerGenerated ? Not the opposite ? [MAF:3/26/2013]
                if (mdDecoder.IsCompilerGenerated(getter) && mdDecoder.Parameters(getter).Count == 0)
                {
                  Type propType = mdDecoder.ReturnType(getter);
                  if (mdDecoder.IsStruct(propType))
                  {
                    data.AssignConst(data.PseudoFieldAddress(thisValue, getter, this.CurrentMethodDeclaringType), propType, 0);
                  }
                  else
                  {
                    data.AssignNull(data.PseudoFieldAddress(thisValue, getter, this.CurrentMethodDeclaringType));
                  }
                }
              }
            }
          }
          else if (MethodToBeSimulated(method))
          {
            InitializePhantonFields(method, data);
          }
          return data;
        }

        private bool IsMoveNext(Method method)
        {
          string name = this.mdDecoder.Name(method);
          bool nameMatch = name == "MoveNext";
          if (!nameMatch) return false;
          foreach (Method m in mdDecoder.OverriddenAndImplementedMethods(method))
          {
            Type t = mdDecoder.DeclaringType(m);
            string n = mdDecoder.Name(t);
            Contract.Assume(n != null);
            if (n.Contains("Enumerable") || n.Contains("Enumerator")) return true;
          }
          return false;
        }

        private bool MethodToBeSimulated(Method method)
        {
          foreach (Attribute attr in mdDecoder.GetAttributes(method))
          {
            Type attrType = mdDecoder.AttributeType(attr);
            string attrName = mdDecoder.Name(attrType);
            if (attrName == "SimulationMethod")
            {
              return true;
            }
          }
          return false;
        }

        private void InitializePhantonFields(Method method, Domain data)
        {
          Parameter thisParam = this.mdDecoder.This(method);
          ESymValue thisValue = data.Value(thisParam);
          string mname = mdDecoder.Name(method);
          foreach (Field f in this.mdDecoder.Fields(this.mdDecoder.DeclaringType(method)))
          {
            foreach (Attribute attr in mdDecoder.GetAttributes(f))
            {
              Type attrType = mdDecoder.AttributeType(attr);
              if (mdDecoder.Name(attrType) == "PhantonField")
              {
                IIndexable<object> args = mdDecoder.PositionalArguments(attr);
                if (args != null && args.Count == 1 && (string)args[0] == mname)
                {
                  data.FieldAddress(thisValue, f);
                  break;
                }
              }
            }
          }
        }

        public Domain Ldstack(APC pc, int offset, int dest, int source, bool isOld, Domain data)
        {
          if (isOld)
          {
            // New implementation using CopyOldValue
            Domain oldState = FindOldState(pc, data);

            if(oldState == null)
            {
              return data;
            }

            oldState.CopyOldValue(pc, dest, source, data, false);
            if (data.oldDomain != null)
            {
              oldState.CopyOldValue(pc, dest, source, data.oldDomain, false);
            }
          }
          else
          {
            data.Copy(dest, source);
            if (data.oldDomain != null)
            {
              data.oldDomain.Copy(dest, source);
            }
          }
          return data;
        }

        private static void LdstackaEffect(APC pc, int offset, int dest, int source, Domain data)
        {
          data.CopyStackAddress(data.Address(dest), source);
        }
        public Domain Ldstacka(APC pc, int offset, int dest, int source, Type type, bool isOld, Domain data)
        {
          if (isOld)
          {
            // it makes no sense to take the address of the actual old stack location since this really means we'll
            // eventually read something from there. So what we do is create a dummy new location into which we make a shallow
            // copy of the old stack locations data and then produce a pointer to that dummy location.
            Type dummyLocType = mdDecoder.ManagedPointer(type);

            Domain oldState = FindOldState(pc, data);

            if(oldState == null)
            {
              return data;
            }

            var oldStackAddress = oldState.Address(source);
            var dummyDest = data.CreateValue(type);
            oldState.CopyOldValue(pc, dummyDest, oldStackAddress, data, true);
            data.CopyAddress(data.Address(dest), dummyDest, dummyLocType);

            if (data.oldDomain != null)
            {
              var addrType = mdDecoder.ManagedPointer(dummyLocType);
              oldState.CopyOldValueToDest(pc, data.oldDomain.Address(dest), oldStackAddress, addrType, data.oldDomain);
            }
          }
          else
          {
            LdstackaEffect(pc, offset, dest, source, data);
            if (data.oldDomain != null)
            {
              LdstackaEffect(pc, offset, dest, source, data.oldDomain);
            }
          }
          return data;
        }

        public static Domain FindOldState(APC pc, Domain data)
        {
          Contract.Requires(data != null);

          // walk the subroutine context to determine if we are in an ensures of a method exit, or at a call-return
          // and grab the correct context pc for that state.
          SubroutineContext scontext = pc.SubroutineContext;
          while (scontext != null)
          {
            SubroutineEdge head = scontext.Head;
            if (head.Three == "exit")
            {
              // inside method exit.
              Domain oldState = data.GetStateAt(new APC(head.One.Subroutine.EntryAfterRequires, 0, scontext.Tail));
              return oldState;
            }
            if (head.Three.StartsWith("after") || head.Three.StartsWith("assumeInvariant"))
            {
              // after call or after newObj
              // we are in an ensures around a method call. Find the calling context and the from block, which is the method call
              Domain oldState = data.GetStateAt(new APC(head.One, 0, scontext.Tail));
              return oldState;
            }
            scontext = scontext.Tail;
          }
          return null;
        }

        /// <summary>
        /// This is were OLD semantics are actually implemented.
        ///
        /// Here, we have to distinguish 2 cases
        /// 1) we are evaluating the ensures assert at the end of a method
        /// 2) we are evaluating the ensures assume at the end of a call
        ///
        /// For 1), we find the heap state at the beginning of the current method, after pre-conditions have been evaluated.
        /// For 2), we find the heap state at the call instruction of the call on the subroutine stack
        ///
        /// We clone the found heap state and return it as the current state for the evaluation of the instructions modelling
        /// the "old" expression.
        ///
        /// We also have to store away our current state so as to retrieve it at the matchin EndOld.
        ///
        /// Note about DFA dependency. if the data flow state changes that we look up here, then we will re-evaluate this point
        /// as well because the call site or method entry point dominates the current pc.
        /// </summary>
        public Domain BeginOld(APC pc, APC matchingEnd, Domain data)
        {
          // Nested begin old can happen if we use a property in an old context and the property itself has a post condition
          // with old wrapping.
          if (data.insideOld++ == 0)
          {
            Domain oldDomain = FindOldState(pc, data);
            if (oldDomain == null)
            {
              throw new InvalidOperationException("begin_old in weird calling context");
            }
            oldDomain = oldDomain.Clone();
            oldDomain.beginOldPC = pc;
            data.oldDomain = oldDomain;
          }
          Contract.Assume(data.insideOld > 0);

#if false
          // Store away the current state so we can retrieve it at the matching EndOld.
          data.AddOldSavedState(pc, data);

          data = oldDomain.Clone();
          data.inOld = true;
#endif
          return data;
        }

        /// <summary>
        /// Here we have to load source in the current "old" state, then retrieve the dataflow state for the matching
        /// BeginOld. We then have 2 situations
        /// 1) the source symbol from the "old" state is known in the retrieved state. If so, we assign it to dest in the
        ///    retrieved state.
        /// 2) the source symbol from the "old" state is NOT known in the retrieved state. If so, we assign a fresh
        ///    symbol to dest in the retrieved state.
        ///
        /// In both cases, we continue with the retrieved state.
        /// </summary>
        public Domain EndOld(APC pc, APC matchingBegin, Type type, Temp dest, Temp source, Domain data)
        {
          Contract.Assume(data.insideOld > 0);

          Domain oldDomain = data.oldDomain;

          if (--data.insideOld == 0)
          {
            // outermost end old
            data.oldDomain = null;
          }
          data.CopyOld(dest, source, type);
          return data;
        }

        public Domain Ldresult(APC pc, Type type, int dest, int source, Domain data)
        {
          return this.Ldstack(pc, 0 /* not important */, dest, source, false, data);
        }

        #endregion
      }
      #endregion


      #region IAbstractValue<OptimisticHeapAbstraction<APC,Local,Parameter,Method,Field,Type>> Members

      public Domain Top
      {
        get
        {
          return new Domain(this.egraph.Top,
                            FunctionalIntKeyMap<ESymValue, Constructor>.Empty(ESymValue.GetUniqueKey),
                            FunctionalSet<ESymValue>.Empty(ESymValue.GetUniqueKey),
                            FunctionalSet<ESymValue>.Empty(ESymValue.GetUniqueKey),
                            FunctionalSet<ESymValue>.Empty(ESymValue.GetUniqueKey),
                            this, null);
        }
      }

      [ThreadStatic] // Because this.egraph.Bottom is ThreadStatic
      private static Domain BottomValue;

      public Domain Bottom
      {
        get
        {
          if (BottomValue == null)
          {
            BottomValue = new Domain(this.egraph.Bottom,
                                     FunctionalIntKeyMap<ESymValue, Constructor>.Empty(ESymValue.GetUniqueKey),
                                     FunctionalSet<ESymValue>.Empty(ESymValue.GetUniqueKey),
                                     FunctionalSet<ESymValue>.Empty(ESymValue.GetUniqueKey),
                                     FunctionalSet<ESymValue>.Empty(ESymValue.GetUniqueKey),
                                     this, null);
            Bottom.ImmutableVersion();
          }
          return BottomValue;
        }
      }

      public bool IsTop
      {
        get { return this.egraph.IsTop; }
      }

      public bool IsBottom
      {
        get
        {
          return this == BottomValue || this.egraph.IsBottom
            || this.oldDomain != null && this.oldDomain.IsBottom;
        }
      }

      public Domain ImmutableVersion()
      {
        this.egraph.ImmutableVersion(); // mark immutable
        return this;
      }

      public Domain Clone()
      {
        Contract.Ensures(Contract.Result<Domain>() != null);

        return new Domain(this.egraph.Clone(),
                          this.constantLookup,
                          this.unmodifiedSinceEntry,
                          this.unmodifiedSinceEntryForFields,
                          this.modifiedAtCall,
                          this,
                          this.oldDomain == null ? null : this.oldDomain.Clone()
                   );
      }

      public Domain Join(Domain newState, out bool weaker, bool widen)
      {
        IMergeInfo mi;
        return this.Join(newState, out weaker, out mi, widen);
      }

      internal Domain Join(Domain newState, out bool weaker, out IMergeInfo mergeInfo, bool widen)
      {
        EGraph<Constructor, AbstractType> newEgraph = this.egraph.Join(newState.egraph, out mergeInfo, widen);
        weaker = mergeInfo.Changed;
        IFunctionalSet<ESymValue> newUnmodifiedSinceEntry, newUnmodifiedSinceEntryForFields, newModifiedAtCall;
        ComputeJoinOfSets(mergeInfo, this, newState, out newUnmodifiedSinceEntry, out newUnmodifiedSinceEntryForFields, out newModifiedAtCall, ref weaker);

        Domain oldDomainJoin = null;
        if (this.oldDomain != null)
        {
          Contract.Assume(newState.oldDomain != null);
          bool weaker2;
          IMergeInfo mergeInfo2;
          oldDomainJoin = this.oldDomain.Join(newState.oldDomain, out weaker2, out mergeInfo2, widen);
        }
        return new Domain(newEgraph, RecomputeConstantMap(newEgraph),
                          newUnmodifiedSinceEntry,
                          newUnmodifiedSinceEntryForFields,
                          newModifiedAtCall,
                          this, oldDomainJoin);
      }

      private void ComputeJoinOfSets(IMergeInfo mergeInfo, Domain oldState, Domain newState, out IFunctionalSet<ESymValue> resultUnmodifiedSinceEntry, out IFunctionalSet<ESymValue> resultUnmodifiedSinceEntryForFields, out IFunctionalSet<ESymValue> resultModifiedAtCall, ref bool weaker)
      {
        resultUnmodifiedSinceEntry = FunctionalSet<ESymValue>.Empty(ESymValue.GetUniqueKey);
        resultUnmodifiedSinceEntryForFields = FunctionalSet<ESymValue>.Empty(ESymValue.GetUniqueKey);
        resultModifiedAtCall = FunctionalSet<ESymValue>.Empty(ESymValue.GetUniqueKey);

        foreach (var tuple in mergeInfo.MergeTriples)
        {
          var oldsym = tuple.One;
          var newsym = tuple.Two;
          var resultsym = tuple.Three;

          // Must be able to handle oldsym or newsym being null. 
          // This happens when we materialize one or the other.
          bool oldUnmodifiedSinceEntry = SetContains(oldState.unmodifiedSinceEntry, oldsym);
          bool oldUnmodifiedSinceEntryForFields = SetContains(oldState.unmodifiedSinceEntryForFields, oldsym);
          bool oldModifiedAtCall = SetContains(oldState.modifiedAtCall, oldsym);

          #region Unmodified
          if (oldUnmodifiedSinceEntry)
          {
            // newsym == null means we manifested it. Let's assume that it wasn't modified on the new branch!
            if (newsym == null || newState.IsBottomPlaceHolder(newsym) || SetContains(newState.unmodifiedSinceEntry, newsym))
            {
              // found intersection
              resultUnmodifiedSinceEntry = resultUnmodifiedSinceEntry.Add(resultsym);
            }
            else
            {
              // check if newState has the symbol in the ForFields set
              if (SetContains(newState.unmodifiedSinceEntryForFields, newsym))
              {
                // add it to the result ForFields set
                resultUnmodifiedSinceEntryForFields = resultUnmodifiedSinceEntryForFields.Add(resultsym);
              }
            }
          }

          #endregion

          #region Modified
          if (oldModifiedAtCall)
          {
            // just rename from old to result. We don't care about join, as this set only matters
            // within scope of call + ensures.
            resultModifiedAtCall = resultModifiedAtCall.Add(resultsym);
          }
          #endregion

        }
        #region Check if join is weaker
        if (SetCount(oldState.unmodifiedSinceEntry) > resultUnmodifiedSinceEntry.Count)
        {
          // weaker join
          weaker = true;
          if (Domain.Debug)
          {
            Console.WriteLine("---Result changed due to fewer unmodified locations since entry");
          }
        }
        else if (SetCount(oldState.unmodifiedSinceEntryForFields) > resultUnmodifiedSinceEntryForFields.Count)
        {
          // weaker join
          weaker = true;
          if (Domain.Debug)
          {
            Console.WriteLine("---Result changed due to fewer unmodified locations for fields since entry");
          }
        }
        #endregion
      }




      private static bool SetContains(IFunctionalSet<ESymValue> set, ESymValue value)
      {
        if (set == null) return false;
        if (value == null) return false;
        return set.Contains(value);
      }

      private static int SetCount(IFunctionalSet<ESymValue> set)
      {
        if (set == null) return 0;
        return set.Count;
      }

      IFunctionalMap<ESymValue, Constructor> RecomputeConstantMap(EGraph<Constructor, AbstractType> egraph)
      {
        IFunctionalMap<ESymValue, Constructor> result = FunctionalIntKeyMap<ESymValue, Constructor>.Empty(ESymValue.GetUniqueKey);

        foreach (Constructor c in egraph.Constants)
        {
          if (Constructors.IsConstantOrMethod(c))
          {
            result = result.Add(egraph[c], c);
          }
        }
        return result;
      }

      public Domain Meet(Domain b)
      {
        EGraph<Constructor, AbstractType> newEgraph = this.egraph.Meet(b.egraph);
        return new Domain(newEgraph, RecomputeConstantMap(newEgraph),
                          ComputeMeetUnmodifiedSinceEntry(),
                          ComputeMeetUnmodifiedSinceEntry(),
                          null,
                          this, this.oldDomain);
        // NOTE: do we need to meet oldDomains ?
      }

      private IFunctionalSet<ESymValue> ComputeMeetUnmodifiedSinceEntry()
      {
        throw new NotImplementedException();
      }

      public void Dump(TextWriter tw)
      {
        this.egraph.Dump(tw);
        tw.WriteLine("Unmodified locs");
        foreach (ESymValue sv in this.unmodifiedSinceEntry.Elements)
        {
          tw.Write(sv); tw.Write(' ');
        }
        tw.WriteLine();
        tw.WriteLine("Unmodified locs for fields");
        foreach (ESymValue sv in this.unmodifiedSinceEntryForFields.Elements)
        {
          tw.Write(sv); tw.Write(' ');
        }
        tw.WriteLine();
        if (this.modifiedAtCall != null)
        {
          tw.WriteLine("Modified locs at last call");
          foreach (ESymValue sv in this.modifiedAtCall.Elements)
          {
            tw.Write(sv); tw.Write(' ');
          }
          tw.WriteLine();
        }
        if (this.oldDomain != null)
        {
          tw.WriteLine("## has old domain");
          this.oldDomain.egraph.Dump(tw);
          tw.WriteLine("## end old domain");
        }
      }

      public bool LessEqual(Domain that)
      {
        return this.egraph.LessEqual(that.egraph);
      }

      public bool LessEqual(Domain that,
                            out IFunctionalMap<ESymValue, FList<ESymValue>>/*?*/ forward,
                            out IFunctionalMap<ESymValue, ESymValue>/*?*/ backward)
      {
        Contract.Requires(that != null);

        return this.egraph.LessEqual(that.egraph, out forward, out backward);
      }

      #endregion

      #region Accessors for Heap Abstraction Results
      /// <summary>
      /// Represents the CLR type of a symbolic value as well as wether the value is known to be zero/null
      /// </summary>
      internal struct AbstractType : IAbstractValueForEGraph<AbstractType>, IEquatable<AbstractType>
      {
        private readonly bool isZero;
        private readonly FlatDomain<Type> value;

        public AbstractType(FlatDomain<Type> value, bool isZero)
        {
          this.value = value;
          this.isZero = isZero;
        }

        #region Forwarding to FlatDomainOE<Type>

        public AbstractType Top { get { return new AbstractType(FlatDomain<Type>.TopValue, false); } }
        public AbstractType Bottom { get { return new AbstractType(FlatDomain<Type>.BottomValue, true); } }
        public bool IsTop { get { return !isZero && value.IsTop; } }
        public bool IsBottom { get { return isZero && value.IsBottom; } }
        public AbstractType ImmutableVersion() { return this; }
        public AbstractType Clone() { return this; }
        public AbstractType Join(AbstractType that, out bool weaker, bool widen)
        {
          // special hack: if one of the two values is Zero, we ignore the type and treat it as bottom
          if (that.isZero)
          {
            weaker = false;
            if (this.value.IsBottom)
            {
              return new AbstractType(that.value, this.isZero);
            }
            return this;
          }
          else if (this.isZero)
          {
            weaker = true;
            if (that.value.IsBottom)
            {
              return new AbstractType(this.value, that.isZero);
            }
            return that;
          }
          else
          {
            FlatDomain<Type> resultType = value.Join(that.value, out weaker, widen);
            RecoverResultTypeInfo(ref resultType, this.value, that.value);
            return new AbstractType(resultType, false);
          }
        }

        [ThreadStatic]
        internal static IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetadataDecoder;

        private static void RecoverResultTypeInfo(ref FlatDomain<Type> resultType, FlatDomain<Type> thisType, FlatDomain<Type> thatType)
        {
          if (resultType.IsTop)
          {
            if (thisType.IsNormal && thatType.IsNormal)
            {
              Type t1 = thisType.Value;
              Type t2 = thatType.Value;
              // check if we have T@ and T*
              if (MetadataDecoder.IsManagedPointer(t1) && MetadataDecoder.IsUnmanagedPointer(t2) &&
                  MetadataDecoder.ElementType(t1).Equals(MetadataDecoder.ElementType(t2)))
              {
                resultType = thatType;
                return;
              }
              // check if we have T* and T@
              if (MetadataDecoder.IsUnmanagedPointer(t1) && MetadataDecoder.IsManagedPointer(t2) &&
                  MetadataDecoder.ElementType(t1).Equals(MetadataDecoder.ElementType(t2)))
              {
                resultType = thisType;
                return;
              }
            }
          }
        }

        public AbstractType Meet(AbstractType that) { return new AbstractType(value.Meet(that.value), this.isZero || that.isZero); }
        public bool LessEqual(AbstractType that)
        {
          return this.isZero || !that.isZero && TypeLeq(ref that);
        }

        private bool TypeLeq(ref AbstractType that)
        {
          if (value.LessEqual(that.value)) return true;
          // recover if there are managed/unmanaged pointer differences:
          // T@ <= T*
          if (this.value.IsNormal && that.value.IsNormal)
          {
            Type t1 = this.value.Value;
            Type t2 = that.value.Value;
            // check if we have T@ and T*
            if (MetadataDecoder.IsManagedPointer(t1) && MetadataDecoder.IsUnmanagedPointer(t2) &&
                MetadataDecoder.ElementType(t1).Equals(MetadataDecoder.ElementType(t2)))
            {
              return true;
            }
          }
          return false;
        }

        public void Dump(TextWriter tw)
        {
          if (isZero) { tw.Write("(Zero) "); }
          value.Dump(tw);
        }

        public static readonly AbstractType TopValue = new AbstractType(FlatDomain<Type>.TopValue, false); // Thread-safe because MetadataDecoder is ThreadStatic
        //        public static AbstractType ForManifestedFieldValue { get { return new AbstractType(FlatDomain<Type>.BottomValue, false); } }
        public static AbstractType ForManifestedFieldValue { get { return TopValue; } }
        public static readonly AbstractType BottomValue = new AbstractType(FlatDomain<Type>.BottomValue, true); // Thread-safe because MetadataDecoder is ThreadStatic
        public Type Value { get { return this.value.Value; } }
        public bool IsNormal { get { return this.value.IsNormal; } }

        #endregion Forwarding to FlatDomain<Type>

        // public static implicit operator AbstractType(Type type) { return new AbstractType(type); }

        public FlatDomain<Type> Type { get { return this.value; } }

        public override string ToString()
        {
          if (isZero) { return "(Zero) " + value.ToString(); }
          return value.ToString();
        }


        #region IAbstractValueForEGraph<AbstractType> Members

        public bool HasAllBottomFields
        {
          get { return this.isZero; }
        }

        public AbstractType ForManifestedField()
        {
          return ForManifestedFieldValue;
        }

        #endregion

        internal AbstractType With(FlatDomain<Type> type)
        {
          return new AbstractType(type, this.isZero);
        }

        internal AbstractType ButZero
        {
          get
          {
            return new AbstractType(this.value, true);
          }
        }
        internal bool IsZero { get { return this.isZero; } }



        #region IEquatable<AbstractType> Members

        public bool Equals(AbstractType that)
        {
          return this.isZero == that.isZero && this.value.Equals(that.value);
        }

        #endregion


      }

      internal AbstractType GetType(ESymValue value)
      {
        return this.egraph[value];
      }

      internal bool TryGetArrayLength(ESymValue arrayValue, out ESymValue lengthValue)
      {
        lengthValue = egraph.TryLookup(Constructors.Length, arrayValue);
        return lengthValue != null;
      }

      internal bool TryGetWritableBytes(ESymValue arrayValue, out ESymValue lengthValue)
      {
        lengthValue = egraph.TryLookup(Constructors.WritableBytes, arrayValue);
        return lengthValue != null;
      }

      internal bool IsZero(ESymValue value)
      {
        return egraph[value].IsZero;
      }

      internal bool IsConstant(ESymValue sv, out Type type, out object value)
      {
        Constructor c = this.constantLookup[sv];
        if (c != null)
        {
          return this.Constructors.IsConstant(c, out type, out value); // may fail because constantLookup also contains method pointers which we do not treat as constants
        }
        type = default(Type);
        value = null;
        return false;
      }

      /// <summary>
      /// Besides getting the path, it also propagates type info in preparation for path decoding as MSIL code
      /// </summary>
      internal FList<PathElement> GetAccessPathList(ESymValue sv, AccessPathFilter<Method, Type> filter, bool allowLocal, bool preferLocal)
      {
        var path = GetBestAccessPath(sv, filter, true, allowLocal, preferLocal);
        return path;
      }

      private bool TryGetCorrespondingValueAbstraction(Constructor v, out SymbolicValue sv)
      {
        if (this.IsBottom) { goto noValue; }
        ESymValue addr = TryAddress(v);
        if (addr == null) goto noValue;

        var value = TryCorrespondingValue(addr); // provides Value indirection or address depending on addr type.

        if (value == null) goto noValue;
        sv = new SymbolicValue(value);
        return true;

      noValue: ;
        sv = default(SymbolicValue);
        return false;
      }

      internal ESymValue TopOfStackValue()
      {
        if (this.IsBottom) { goto noValue; }
        ESymValue addr = TryAddress(Constructors.For((Temp)0));
        if (addr == null) goto noValue;

        var value = TryCorrespondingValue(addr); // provides Value indirection or address depending on addr type.

        if (value == null) goto noValue;
        return value;

      noValue: ;
        return null;
      }

      internal ESymValue LoadValue(Temp v)
      {
        return Value(v);
      }

      internal SymbolicValue LoadValue(ESymValue addr)
      {
        return new SymbolicValue(Value(addr));
      }

      internal SymbolicValue TryLoadValue(ESymValue addr)
      {
        var value = this.TryValue(addr);
        if (value != null) return new SymbolicValue(value);
        else return new SymbolicValue(addr);
      }

      /// <summary>
      /// For struct values, we return the address of the local. For everything else,
      /// including primitives, we return the value
      /// </summary>
      internal bool TryGetCorrespondingValueAbstraction(Temp v, out SymbolicValue sv)
      {
        return TryGetCorrespondingValueAbstraction(Constructors.For(v), out sv);
      }

      /// <summary>
      /// For struct values, we return the address of the local. For everything else,
      /// including primitives, we return the value
      /// </summary>
      internal bool TryGetCorrespondingValueAbstraction(Local v, out SymbolicValue sv)
      {
        return TryGetCorrespondingValueAbstraction(Constructors.For(v), out sv);
      }

      internal bool TryGetUnboxedValue(Temp v, out SymbolicValue result)
      {
        var address = this.Address(v);
        var box = this.TryValue(address);
        return TryGetUnboxedValue(box, out result);
      }

      internal bool TryGetUnboxedValue(ESymValue box, out SymbolicValue result)
      {
        if (box != null)
        {
          var unbox = this.TryValue(box);
          if (unbox != null && this.egraph.TryLookup(this.Constructors.BoxOperator, unbox) == box)
          {
            result = new SymbolicValue(unbox);
            return true;
          }
        }
        result = default(SymbolicValue);
        return false;
      }

      /// <summary>
      /// Returns address of entity
      /// </summary>
      private bool TryGetCorrespondingAddressAbstraction(Constructor v, out SymbolicValue sv)
      {
        ESymValue addr = TryAddress(v);
        if (addr == null)
        {
          sv = default(SymbolicValue);
          return false;
        }
        sv = new SymbolicValue(addr);
        return true;
      }

      /// <summary>
      /// Returns address of Local
      /// </summary>
      internal bool TryGetCorrespondingAddressAbstraction(Local v, out SymbolicValue sv)
      {
        return TryGetCorrespondingAddressAbstraction(Constructors.For(v), out sv);
      }

      /// <summary>
      /// For struct values, we return the address of the local. For everything else,
      /// including primitives, we return the value
      /// </summary>
      internal bool TryGetCorrespondingValueAbstraction(Parameter v, out SymbolicValue sv)
      {
        return TryGetCorrespondingValueAbstraction(Constructors.For(v), out sv);
      }

      /// <summary>
      /// Returns address value of parameter
      /// </summary>
      internal bool TryGetCorrespondingAddressAbstraction(Parameter v, out SymbolicValue sv)
      {
        return TryGetCorrespondingAddressAbstraction(Constructors.For(v), out sv);
      }

      /// <summary>
      /// Returns an access path from a parameter or local to the given value.
      /// </summary>
      internal string GetAccessPath(ESymValue sv)
      {
        var path = GetBestAccessPath(sv, AccessPathFilter<Method, Type>.NoFilter, true, true, false);
        if (path == null) { return null; }
        return path.ToCodeString();
      }


      internal bool IsResultEGraph(IMergeInfo mi)
      {
        return mi.IsResult(this.egraph);
      }

      internal bool IsGraph1(IMergeInfo mi)
      {
        return mi.IsGraph1(this.egraph);
      }

      internal bool IsGraph2(IMergeInfo mi)
      {
        return mi.IsGraph2(this.egraph);
      }
      #endregion


      internal bool IsUnmodifiedSinceEntry(ESymValue sym)
      {
        return IsUnmodified(sym);
      }

      /// <summary>
      /// True if it is a parameter, static field, or static method (getter)
      /// </summary>
      /// <param name="pathElement">Head of path to test!</param>
      /// <param name="sym">on success, contains the corresponding address (for parameter or field), and 
      /// actual value for property</param>
      internal bool IsTopLevelVisibleAtMethodEntry(PathElement pathElement, out ESymValue sym)
      {
        if (pathElement.IsReturnValue)
        {
          sym = default(ESymValue);
          return false;
        }
        bool unmodified;
        sym = AccessPathHead(pathElement, out unmodified, false, false);
        return unmodified;
      }

      internal ESymValue AccessPathHead(PathElement pathElement, out bool unModifiedSinceEntry, bool useModifiedSet, bool includeLocals)
      {
        PathElement<Parameter> p = pathElement as PathElement<Parameter>;
        unModifiedSinceEntry = true; // cheating for static fields and properties
        ESymValue sym;
        if (p != null)
        {
          sym = this.Address(p.Element);
          if (useModifiedSet)
          {
            unModifiedSinceEntry = !SetContains(this.modifiedAtCall, sym);
          }
          else
          {
            unModifiedSinceEntry = this.IsUnmodifiedSinceEntry(sym);
          }
          if (!p.IsAddressOf)
          {
            // compressed parameter ldarga,ldind
            sym = this.Value(sym);
          }
          return sym;
        }
        if (includeLocals)
        {
          PathElement<Local> l = pathElement as PathElement<Local>;
          if (l != null)
          {
            sym = this.Address(l.Element);
            return sym;
          }
        }
        PathElement<Field> f = pathElement as PathElement<Field>;
        if (f != null) // must be static if first element
        {
          sym = this.FieldAddress(this.egraph.Root, f.Element);
          return sym;
        }
        PathElement<Method> m = pathElement as PathElement<Method>;
        if (m != null) // must be static if first element
        {
          Type pseudoFieldAddressType;
          // recall that paths have a stripped out deref for pseudo fields (getters)
          var pseudoFieldAddress = this.PseudoFieldAddress(this.egraph.Root, m.Element, out pseudoFieldAddressType, false, default(Type)); // shouldn't try to materialize
          if (mdDecoder.HasValueRepresentation(mdDecoder.ElementType(pseudoFieldAddressType)))
          {
            sym = this.Value(pseudoFieldAddress);
          }
          else
          {
            sym = pseudoFieldAddress;
          }
          return sym;
        }
        return null;
      }

      internal ESymValue TryAccessOldValue(ESymValue sym, PathElement pathElement)
      {

        PathElementBase pbase = (PathElementBase)pathElement;
        Contract.Assume(pbase.Constructor == Constructors.ValueOf); // F: made into an assumption
        //System.Diagnostics.Debug.Assert(pbase.Constructor == Constructors.ValueOf);
        return this.egraph.TryLookup(Constructors.OldValueOf, sym);
      }

      internal ESymValue TryAccess(ESymValue sym, PathElement pathElement)
      {
        PathElementBase pbase = (PathElementBase)pathElement;
        ESymValue value = this.egraph.TryLookup(pbase.Constructor, sym);
        if (value == null) return null;
        // since we stripped out the extra deref on Method getter pseudo fields, we have to perform
        // the extra deref here
        PathElement<Method> pseudoField = pbase as PathElement<Method>;
        if (pseudoField != null && !pseudoField.IsAddressOf)
        {
          return this.TryCorrespondingValue(value);
        }
        // extra deref on compressed parameter paths
        ParameterPathElement parameter = pbase as ParameterPathElement;
        if (parameter != null)
        {
          return this.TryCorrespondingValue(value);
        }
        return value;
      }

      internal bool CheckIfPathUnmodified(ESymValue sym, FList<PathElement> path)
      {
        while (path != null)
        {
          if (sym == null) return true; // not materialized
          if (path.Head.IsDeref)
          {
            if (!this.IsUnmodifiedSinceEntry(sym)) return false;
          }
          else
          {
            // handle the case of a property getter/pure method. We have to check the location
            // explicitly
            var pseudoField = path.Head as MethodCallPathElement;
            if (pseudoField != null && !pseudoField.IsAddressOf)
            {
              ESymValue pseudoFieldAddress = this.egraph.TryLookup(pseudoField.Constructor, sym);
              if (pseudoFieldAddress == null) return false; // can't check
              if (!this.IsUnmodifiedSinceEntry(pseudoFieldAddress)) return false;
            }
          }
          sym = this.TryAccess(sym, path.Head);
          path = path.Tail;
        }
        return true;
      }

      internal bool TryGetFieldAddress(SymbolicValue obj, Field field, out SymbolicValue address)
      {
        ESymValue addr = this.TryGetFieldAddress(obj.symbol, field);
        if (addr != null) { address = new SymbolicValue(addr); return true; }
        address = default(SymbolicValue);
        return false;
      }

      private ESymValue TryGetFieldAddress(ESymValue obj, Field field)
      {
        return egraph.TryLookup(Constructors.For(field), obj);
      }

      internal bool TryLoadIndirect(SymbolicValue addr, out SymbolicValue value)
      {
        ESymValue val = this.egraph.TryLookup(Constructors.ValueOf, addr.symbol);
        if (val != null) { value = new SymbolicValue(val); return true; }
        value = default(SymbolicValue);
        return false;
      }


      internal bool PathExistsUnmodifiedSinceEntry(FList<PathElementBase> accesspath, out ESymValue targetSym)
      {
        targetSym = null;

        bool unmodified;
        ESymValue sym = AccessPathHead(accesspath.Head, out unmodified, false, false);
        if (sym == null || !unmodified)
        {
          return false;
        }

        var path = accesspath.Tail;
        while (path != null)
        {
          if (path.Head.IsDeref)
          {
            if (!this.IsUnmodifiedSinceEntry(sym)) return false;
          }
          sym = this.TryAccess(sym, path.Head);
          if (sym == null) return false;
          path = path.Tail;
        }
        targetSym = sym;
        return true;
      }

      internal void ResetModifiedAtCall()
      {
        this.modifiedAtCall = FunctionalSet<ESymValue>.Empty(ESymValue.GetUniqueKey);
      }

      /// <summary>
      /// Try to find a target value corresponding to old value. We are allowed to traverse 1 $OldValue
      /// </summary>
      /// <param name="accesspath"></param>
      /// <param name="targetSym"></param>
      /// <returns></returns>
      internal bool PathExistsUnmodifiedAtCall(FList<PathElementBase> accesspath, out ESymValue targetSym)
      {
        targetSym = null;

        bool unmodified;
        ESymValue sym = AccessPathHead(accesspath.Head, out unmodified, true, true);
        if (sym == null || !unmodified)
        {
          return false;
        }

        var path = accesspath.Tail;
        while (path != null)
        {
          if (path.Head.IsDeref)
          {
            if (this.IsModifiedAtCall(sym))
            {
              sym = this.TryAccessOldValue(sym, path.Head);
              if (sym == null) return false;
              else
              {
                // continue
                path = path.Tail;
                continue;
              }
            }
          }
          sym = this.TryAccess(sym, path.Head);
          if (sym == null) return false;
          path = path.Tail;
        }
        targetSym = sym;
        return true;
      }

      /// <summary>
      /// Try to find a target value corresponding to old value. Call did not modify anything
      /// </summary>
      internal bool PathExistsAtCall(FList<PathElementBase> accesspath, out ESymValue targetSym)
      {
        targetSym = null;

        bool unmodified;
        ESymValue sym = AccessPathHead(accesspath.Head, out unmodified, true, true);
        if (sym == null)
        {
          return false;
        }

        var path = accesspath.Tail;
        while (path != null)
        {
          sym = this.TryAccess(sym, path.Head);
          if (sym == null) return false;
          path = path.Tail;
        }
        targetSym = sym;
        return true;
      }


      private bool IsModifiedAtCall(ESymValue sym)
      {
        return (this.modifiedAtCall != null && this.modifiedAtCall.Contains(sym));
      }

      internal PathContextFlags PathContexts(ESymValue eSymValue)
      {
        Set<ESymValue> visited = new Set<ESymValue>();
        return AccumulatePathContexts(eSymValue, PathContextFlags.None, visited);
      }

      PathContextFlags AccumulatePathContexts(ESymValue sv, PathContextFlags sofar, Set<ESymValue> visited)
      {
        if (sofar == PathContextFlags.All) return sofar;
        if (visited.Contains(sv)) return sofar;
        visited.Add(sv);
        // check if this value directly is tagged
        if (egraph.TryLookup(Constructors.ResultOfCall, sv) != null) { sofar |= PathContextFlags.ViaMethodReturn; }
        if (egraph.TryLookup(Constructors.ResultOfLdelem, sv) != null) { sofar |= PathContextFlags.ViaArray; }
        if (egraph.TryLookup(Constructors.ResultOfPureCall, sv) != null) { sofar |= PathContextFlags.ViaPureMethodReturn; }
        if (egraph.TryLookup(Constructors.ResultOfCast, sv) != null) { sofar |= PathContextFlags.ViaCast; }
        if (egraph.TryLookup(Constructors.ResultOfOutParameter, sv) != null) { sofar |= PathContextFlags.ViaOutParameter; }
        if (egraph.TryLookup(Constructors.ResultOfCallThisHavoc, sv) != null) { sofar |= PathContextFlags.ViaCallThisHavoc; }
        if (egraph.TryLookup(Constructors.ResultOfOldValue, sv) != null) { sofar |= PathContextFlags.ViaOldValue; }

        if (sofar == PathContextFlags.All) return sofar;
        // look through access paths
        foreach (var eterm in egraph.EqTerms(sv))
        {
          var constructor = eterm.Function;
          if (constructor.ActsAsField)
          {
            sofar |= PathContextFlags.ViaField;
            sofar = AccumulatePathContexts(eterm.Args[0], sofar, visited);
          }
          else if (
              constructor == Constructors.ValueOf ||
              constructor == Constructors.WritableBytes ||
              constructor == Constructors.Length
             )
          {
            sofar = AccumulatePathContexts(eterm.Args[0], sofar, visited);
          }
          if (sofar == PathContextFlags.All) return sofar;
        }
        // look through array access paths
        foreach (var eterm in egraph.EqMultiTerms(sv))
        {
          var constructor = eterm.Function;
          if (
              constructor == Constructors.ElementAddress
             )
          {
            sofar = AccumulatePathContexts(eterm.Args[0], sofar, visited);
          }
          if (sofar == PathContextFlags.All) return sofar;
        }
        return sofar;
      }

      internal ESymValue StructId(ESymValue structAddr)
      {
        return this.egraph[Constructors.StructId, structAddr];
      }

      internal ESymValue ObjectVersion(ESymValue objectPtr)
      {
        return this.egraph[Constructors.ObjectVersion, objectPtr];
      }

      internal void ManifestStructId(ESymValue structAddr)
      {
        var dummy = StructId(structAddr);
      }

      internal void HavocArrayAtIndex(ESymValue arrayValue, ESymValue indexValue)
      {
        // TODO: be smarter about what needs to be havoced in the future.
        // for now, we havoc everything.
        //
        // We could be smarter whenever we see a constant index, we could leave all distinct 
        // constant elements untouched (carry them forward to the new version)
        this.egraph.Eliminate(Constructors.ObjectVersion, arrayValue);
      }

      internal IFunctionalMap<ESymValue, FList<ESymValue>> GetForwardIdentityMap()
      {
        return this.egraph.GetForwardIdentityMap();
      }

      internal void HavocObjectAtCall(ESymValue obj, ref IFunctionalSet<ESymValue> havocedAtCall, bool havocFields, bool havocReadonlyFields)
      {
        this.HavocFields(null, obj, ref havocedAtCall, havocReadonlyFields);
        this.HavocUp(obj, ref havocedAtCall, havocFields);
      }

      /// <summary>
      /// Consider access paths to this object from other objects and mutate their object reference
      /// </summary>
      private void HavocUp(ESymValue obj, ref IFunctionalSet<ESymValue> havocedAtCall, bool havocFields)
      {
        Contract.Requires(havocedAtCall != null);
        Contract.Ensures(havocedAtCall != null);

        // invariant: obj in havocedAtCall
        foreach (var term in this.egraph.EqTerms(obj))
        {
          var c = term.Function;
          if (c == Constructors.ValueOf)
          {
            HavocUpField(term.Args[0], ref havocedAtCall, havocFields);
          }
        }
      }

      private void HavocUpField(ESymValue addr, ref IFunctionalSet<ESymValue> havocedAtCall, bool havocFields)
      {
        Contract.Requires(havocedAtCall != null);
        Contract.Ensures(havocedAtCall != null);

        foreach (var term in this.egraph.EqTerms(addr))
        {
          var c = term.Function;

          Constructor.Wrapper<Field> field = c as Constructor.Wrapper<Field>;
          if (field != null)
          {
            HavocUpObjectVersion(c, term.Args[0], ref havocedAtCall, havocFields);
            continue;
          }
          Constructor.Wrapper<Method> method = c as Constructor.Wrapper<Method>;
          if (method != null)
          {
            // havoc up, provided this is an instance method
            if (!mdDecoder.IsStatic(method.Value) && mdDecoder.IsPropertyGetter(method.Value) && mdDecoder.Name(method.Value) != "get_Current")
            {
              HavocUpObjectVersion(c, term.Args[0], ref havocedAtCall, havocFields);
            }
            continue;
          }

        }
      }

      private void HavocUpObjectVersion(Constructor accessedVia, ESymValue obj, ref IFunctionalSet<ESymValue> havocedAtCall, bool havocFields)
      {
        Contract.Requires(havocedAtCall != null);
        Contract.Ensures(havocedAtCall != null);

        if (havocedAtCall.Contains(obj)) return; // guard against cycles

        this.egraph.Eliminate(Constructors.ObjectVersion, obj);

        if (havocFields)
        {
          this.HavocMutableFields(accessedVia, obj, ref havocedAtCall);
        }
        else
        {
          this.HavocPseudoFields(accessedVia, obj, ref havocedAtCall);
        }
        this.HavocUp(obj, ref havocedAtCall, false); // only havoc fields up one level
      }

      internal void CopyValueToOldState(APC pc, Type type, int dest, int source, Domain target)
      {
        var destAddr = target.Address(dest);
        var srcAddr = this.Address(source);
        Type addrType;
        var currType = this.GetType(srcAddr);
        if (currType.IsNormal)
        {
          addrType = currType.Value;
        }
        else
        {
          addrType = mdDecoder.ManagedPointer(type);
        }
        CopyValueToOldState(pc, addrType, destAddr, srcAddr, target);
      }

      internal void CopyValueToOldState(APC pc, Type addrType, ESymValue destAddr, ESymValue srcAddr, Domain target)
      {
        target.MakeTotallyModified(destAddr);

        if (destAddr != srcAddr) { target.HavocIfStruct(destAddr); }
        target.SetType(destAddr, addrType);
        FlatDomain<Type> elementType = TargetType(addrType);

        if (IsStructWithFields(elementType))
        {
          this.CopyStructValueToOldState(pc, destAddr, srcAddr, elementType, target);
        }
        else
        {
          ESymValue svalue = this.egraph.TryLookup(Constructors.ValueOf, srcAddr);
          if (svalue == null)
          {
            if (this.egraph.IsConstant)
            {
              return;
            }
            svalue = this.egraph[Constructors.ValueOf, srcAddr]; // manifest
          }

          this.CopyPrimitiveValueToOldState(pc, addrType, destAddr, svalue, target);
        }
      }

      private void CopyPrimitiveValueToOldState(APC pc, Type addrType, ESymValue destAddr, ESymValue svalue, Domain target)
      {
        // just grab the value and see if it is valid
        if (target.IsValidSymbol(svalue))
        {
          target.CopyNonStructWithFieldValue(destAddr, svalue, addrType);
        }
        else
        {
          target.Assign(destAddr, addrType);
        }
      }



      private void CopyStructValueToOldState(APC pc, ESymValue destAddr, ESymValue srcAddr, FlatDomain<Type> type, Domain target)
      {
        if (destAddr == null) return;
        // copy old struct id
        CopyStructId(pc, destAddr, srcAddr, this, target);

        foreach (Constructor key in this.egraph.Functions(srcAddr))
        {
          if (!key.ActsAsField) continue;
          Type t = key.FieldAddressType(mdDecoder);

          ESymValue destFld = target.egraph[key, destAddr];
          ESymValue srcFld = this.egraph[key, srcAddr];
          CopyValueToOldState(pc, t, destFld, srcFld, target);
        }
      }


      internal ESymValue PseudoFieldAddressOfOutParameter(int index, ESymValue fieldAddr, Type parameterType, Type fromType)
      {
        var pseudoField = Constructors.ForConstant(index, mdDecoder.System_Int32);
        var result = this.egraph[pseudoField, fieldAddr];
        MaterializeAccordingToType(result, parameterType, 2, Constructors.ResultOfOutParameter, fromType: fromType);
        return result;
      }

      internal void SetDelegateDetails(ESymValue obj, ESymValue closure, ESymValue fnPtrValue)
      {
        this.egraph[Constructors.FunctionPointer, obj] = fnPtrValue;
        if (closure != null)
        {
          this.egraph[Constructors.ClosureObject, obj] = closure;
        }
      }

      internal bool IsPureFunctionCall(SymbolicValue value, out Method method, out SymbolicValue[] args)
      {
        // we are given the "value", but the egraph stores the address. so we first have to get up one level
        foreach (var addr in this.egraph.EqTerms(value.symbol))
        {
          if (addr.Function == Constructors.ValueOf)
          {
            var fldAddr = addr.Args[0];
            foreach (var term in this.egraph.EqTerms(fldAddr))
            {
              var methodCtor = term.Function as Constructor.PureMethodConstructor;
              if (methodCtor != null)
              {
                method = methodCtor.Value;
                args = new SymbolicValue[term.Args.Length];
                for (int i = 0; i < term.Args.Length; i++)
                {
                  args[i] = new SymbolicValue(term.Args[i]);
                }
                return true;
              }
            }
            foreach (var term in this.egraph.EqMultiTerms(fldAddr))
            {
              var methodCtor = term.Function as Constructor.PureMethodConstructor;
              if (methodCtor != null)
              {
                method = methodCtor.Value;
                args = new SymbolicValue[term.Args.Length];
                for (int i = 0; i < term.Args.Length; i++)
                {
                  args[i] = new SymbolicValue(term.Args[i]);
                }
                return true;
              }
            }
          }
        }
        method = default(Method);
        args = null;
        return false;
      }

      internal bool IsDelegateValue(SymbolicValue value, out SymbolicValue targetObject, out Method targetMethod)
      {
        ESymValue closure;
        var result = IsDelegateValue(value.symbol, out closure, out targetMethod);
        targetObject = new SymbolicValue(closure);
        return result;
      }

      internal bool IsDelegateValue(ESymValue value, out ESymValue targetObject, out Method targetMethod)
      {
        var fnptr = this.egraph.TryLookup(Constructors.FunctionPointer, value);
        if (fnptr != null)
        {
          if (this.IsMethodPointer(fnptr, out targetMethod))
          {
            targetObject = this.egraph.TryLookup(Constructors.ClosureObject, value);
            return true;
          }
        }
        targetMethod = default(Method);
        targetObject = default(ESymValue);
        return false;
      }

      internal bool TryGetArrayFromArrayElementAddress(SymbolicValue elementAddress, out SymbolicValue array, out SymbolicValue index)
      {
        foreach (var candidate in this.egraph.EqMultiTerms(elementAddress.symbol))
        {
          if (candidate.Function == Constructors.ElementAddress)
          {
            index = new SymbolicValue(candidate.Args[1]);
            // grab object version and then find corresponding object
            return TryGetObjectFromObjectVersion(new SymbolicValue(candidate.Args[0]), out array);
          }
        }
        array = default(SymbolicValue);
        index = default(SymbolicValue);
        return false;
      }

      internal bool TryGetObjectFromObjectVersion(SymbolicValue objectVersion, out SymbolicValue objectValue)
      {
        foreach (var obj in this.egraph.EqTerms(objectVersion.symbol))
        {
          if (obj.Function == Constructors.ObjectVersion)
          {
            objectValue = new SymbolicValue(obj.Args[0]);
            return true;
          }
        }
        objectValue = default(SymbolicValue);
        return false;
      }

      internal ESymValue TryGetPropertyNamed(ESymValue pointer, string propertyName)
      {
        foreach (var ctor in this.egraph.Functions(pointer))
        {
          var method = ctor as Constructor.PureMethodConstructor;
          if (method == null) continue;
          if (this.mdDecoder.IsPropertyGetter(method.Value))
          {
            Property prop = this.mdDecoder.GetPropertyFromAccessor(method.Value);
            if (prop != null && this.mdDecoder.Name(prop) == propertyName)
            {
              return this.TryValue(this.egraph[ctor, pointer]);
            }
          }
        }
        return null;
      }

      internal ESymValue TryGetModelMethod(ESymValue pointer, string modelMethodName)
      {
        foreach (var ctor in this.egraph.Functions(pointer))
        {
          var method = ctor as Constructor.PureMethodConstructor;
          if (method == null) continue;
          if (!this.mdDecoder.IsPropertyGetter(method.Value)) continue;
          var methodName = this.mdDecoder.Name(method.Value).Substring(4);
          if (!methodName.Equals(modelMethodName)) continue;
          var t = this.mdDecoder.DeclaringType(method.Value);
          foreach (var modelMethod in this.parent.contractDecoder.ModelMethods(t))
          {
            if (this.mdDecoder.IsPropertyGetter(modelMethod))
            {
              var name = this.mdDecoder.Name(modelMethod);
              if (4 < name.Length && name.Substring(4).Equals(modelMethodName))
              {
                var result = this.TryValue(this.egraph[ctor, pointer]);
                return result;
              }
            }
          }
        }
        return null;
      }


      internal void InitObj(ESymValue destaddr, Type type)
      {
        Havoc(destaddr);
        SetType(destaddr, this.mdDecoder.ManagedPointer(type));
        this.egraph[Constructors.ValueOf, destaddr] = this.TypeDefaultValue(type);
        foreach (Field f in mdDecoder.Fields(type))
        {
          ESymValue fldaddr = this.FieldAddress(destaddr, f);
          this.AssignZeroEquivalent(fldaddr, mdDecoder.FieldType(f));
        }
        if (this.mdDecoder.IsReferenceConstrained(type))
        {
          this.AssignZeroEquivalent(destaddr, type);
        }
      }

      /// <summary>
      /// Returns the reversed access path witness.
      /// </summary>
      internal FList<ESymValue> AccessPathWitnessReversed(Domain d, FList<PathElement> ap)
      {
        Contract.Requires(ap != null);
        Contract.Ensures(Contract.Result<FList<ESymValue>>() == null || Contract.Result<FList<ESymValue>>().Length() == ap.Length());

        var result = FList<ESymValue>.Empty;
        bool unmodifiedSinceEntry;
        var head = d.AccessPathHead(ap.Head, out unmodifiedSinceEntry, true, true);
        if (head == null) return null;
        result = result.Cons(head);
        ap = ap.Tail;
        var current = head;
        while (ap != null)
        {
          var next = d.TryAccess(current, ap.Head);
          if (next == null) return null;
          result = result.Cons(next);
          ap = ap.Tail;
          current = next;
        }
        return result;
      }
    }


    #region IAnalysis<APC,OptimisticHeapAnalyzer<Label,Local,Parameter,Method,Field,Type>,IVisitMSIL<APC,Local,Parameter,Method,Field,Type,int,int,OptimisticHeapAnalyzer<Label,Local,Parameter,Method,Field,Type>,OptimisticHeapAnalyzer<Label,Local,Parameter,Method,Field,Type>>> Members

    public Domain Join(Pair<APC, APC> edge, Domain newState, Domain prevState, out bool weaker, bool widen)
    {
      if (Domain.Debug)
      {
        Console.WriteLine("---------OPT join at {0}", edge.ToString());
        Console.WriteLine("---------Existing state:");
        prevState.Dump(Console.Out);
        Console.WriteLine("---------New state:");
        newState.Dump(Console.Out);
      }
      IMergeInfo mi;
      Domain result = prevState.Join(newState, out weaker, out mi, widen);
      // update mergeinfo cache
      // We must change the entry from being null, as this is not a single pre-decessor point
      if (weaker)
      {
        IMergeInfo existingMI;
        if (this.mergeInfoCache.TryGetValue(edge.Two, out existingMI))
        {
          if (existingMI == null)
          {
            this.mergeInfoCache[edge.Two] = mi;
          }
        }
      }
      else
      {
        // update with info, as next state is in fact the merged state.
        this.mergeInfoCache[edge.Two] = mi;
      }

      if (Domain.Debug)
      {
        Console.WriteLine("---------Result state: changed = {0} (widen = {1})", weaker, widen);
        result.Dump(Console.Out);
        Console.WriteLine("------------------------------------");
      }
      return result;
    }

    /// <summary>
    /// Record the edges on which we perform renamings.
    /// </summary>
    public Domain EdgeConversion(APC from, APC next, bool joinPoint, Unit edgeData, Domain edgestate)
    {
      if (joinPoint || !EagerCaching)
      {
        this.renamePoints[from, next] = Unit.Value;
        // record first incoming edge
        if (!this.mergeInfoCache.ContainsKey(next))
        {
          this.mergeInfoCache.Add(next, null); // sentinel for 1st and eventually only edge into join point
        }
      }

      if (Domain.Debug)
      {
        Console.WriteLine("---------Edge Conversion on {0}->{1}", from, next);
        edgestate.Dump(Console.Out);
        Console.WriteLine("------------------------------------");
      }
      return edgestate;
    }

    public IVisitMSIL<APC, Local, Parameter, Method, Field, Type, int, int, Domain, Domain> Visitor()
    {
      return this.GetVisitor();
    }

    IFixpointInfo<APC, Domain> fixpointInfo;

    bool PreStateLookup(APC label, out Domain ifFound) {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out ifFound) != null);

      Contract.Assume(this.fixpointInfo != null);

      return this.fixpointInfo.PreState(label, out ifFound); 
    }
    bool PostStateLookup(APC label, out Domain ifFound) {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out ifFound) != null);

      Contract.Assume(this.fixpointInfo != null);

      return this.fixpointInfo.PostState(label, out ifFound);
    }

    public bool IsUnreachable(APC pc)
    {
      Contract.Assume(this.fixpointInfo != null);

      Domain d;
      if (!this.fixpointInfo.PreState(pc, out d) || d.IsBottom)
      {
        return true;
      }
      return false;
    }

    public IEnumerable<SubroutineContext> GetContexts(CFGBlock block)
    {
      Contract.Assume(this.fixpointInfo != null);

      return this.fixpointInfo.CachedContexts(block);
    }

    Domain GetPreState(APC at)
    {
      Domain d;
      PreStateLookup(at, out d);
      return d;
    }

    public Predicate<APC> CacheStates(IFixpointInfo<APC, Domain> fixpointInfo)
    {
      this.fixpointInfo = fixpointInfo;
      return delegate(APC pc) { return EagerCaching; };
    }

    public void Dump(Pair<Domain, TextWriter> statetw)
    {
      statetw.One.Dump(statetw.Two);
    }

    public void Dump(APC apc)
    {
      var prestate = GetPreState(apc);
      if (prestate != null)
      {
        Console.WriteLine(@"<egraph state=""pre"">");
        prestate.Dump(Console.Out);
        Console.WriteLine("</egraph>");
      }

      Domain poststate;
      PostStateLookup(apc, out poststate);
      if (poststate != null)
      {
        Console.WriteLine(@"<egraph state=""post"">");
        poststate.Dump(Console.Out);
        Console.WriteLine("</egraph>");
      }
    }

    public Domain ImmutableVersion(Domain d)
    {
      return d.ImmutableVersion();
    }

    public Domain MutableVersion(Domain d)
    {
      if (d.IsBottom) return d; // cannot have a mutable version of bottom
      return d.Clone();
    }

    public bool IsBottom(APC pc, Domain d)
    {
      return d.IsBottom;
    }

    public bool IsTop(APC pc, Domain d)
    {
      return d.IsTop;
    }

    #endregion

    #region ILDecoder
    struct SymbolicAdapter<Data, Result, Visitor> : IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Temp, Temp, Data, Result>
      where Visitor : IVisitMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, Data, Result>
    {
      public SymbolicAdapter(
        OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> parent,
        Visitor delegatee
      )
      {
        this.parent = parent;
        this.delegatee = delegatee;
      }

      #region Privates
      OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> parent;
      Visitor delegatee;

      bool IsZero(APC pc, SymbolicValue sv)
      {
        // lookup the given source in the egraph for this program point
        Domain d;
        if (!this.parent.PreStateLookup(pc, out d))
        {
          return false;
        }
        return d.IsZero(sv.symbol);
      }

      SymbolicValue ConvertSource(APC pc, Temp source)
      {
        // lookup the given source in the egraph for this program point
        Domain d;
        if (!this.parent.PreStateLookup(pc, out d) || d.IsBottom)
        {
          return default(SymbolicValue);
        }
        // handle the special case of a return void here. The source is -1 in that case.
        if (source < 0)
        {
          return new SymbolicValue(d.VoidAddr);
        }
        SymbolicValue result;
        d.TryGetCorrespondingValueAbstraction(source, out result);
        return result;
      }

      [Pure]
      bool HasSourceType(APC pc, Temp source, out Type type, out SymbolicValue falseSymValue)
      {
        // lookup the given source in the egraph for this program point
        falseSymValue = default(SymbolicValue);
        Domain d;
        if (!this.parent.PreStateLookup(pc, out d) || d.IsBottom)
        {
          type = default(Type);
          return false;
        }
        SymbolicValue result;
        if (!d.TryGetCorrespondingValueAbstraction(source, out result))
        {
          type = default(Type);
          return false;
        }
        var at = d.GetType(result.symbol);
        if (at.IsNormal)
        {
          type = at.Value;
          falseSymValue = new SymbolicValue(d.Zero);
          return true;
        }
        type = default(Type);
        return false;
      }


      SymbolicValue ConvertOldSource(APC pc, Temp source)
      {
        // lookup the given source in the egraph for this program point
        Domain d;
        if (!this.parent.PreStateLookup(pc, out d) || d.IsBottom)
        {
          return default(SymbolicValue);
        }
        // handle the special case of a return void here. The source is -1 in that case.
        if (source < 0)
        {
          return new SymbolicValue(d.VoidAddr);
        }
        Domain old = Domain.AnalysisDecoder.FindOldState(pc, d);
        if (old != null)
        {
          SymbolicValue result;
          old.TryGetCorrespondingValueAbstraction(source, out result);
          return result;
        }
        return new SymbolicValue(d.VoidAddr);
      }

      /// <summary>
      /// Temp holds the address of a primitive typed struct
      /// Lookup the temp in the pre-state, and the struct value in the post state.
      /// </summary>
      SymbolicValue ConvertSourceDeref(APC pc, Temp source)
      {
        // lookup the given source in the egraph for this program point
        Domain d;
        if (!this.parent.PreStateLookup(pc, out d))
        {
          return default(SymbolicValue);
        }
        // handle the special case of a return void here. The source is -1 in that case.
        if (source < 0)
        {
          return new SymbolicValue(d.VoidAddr);
        }
        ESymValue refvalue = d.LoadValue(source);
        if (!this.parent.PostStateLookup(pc, out d))
        {
          return default(SymbolicValue);
        }
        return d.TryLoadValue(refvalue);
      }

      SymbolicValue TryConvertUnbox(APC pc, Temp source)
      {
        // lookup the given source in the egraph for this program point
        Domain d;
        if (!this.parent.PreStateLookup(pc, out d))
        {
          return default(SymbolicValue);
        }
        // handle the special case of a return void here. The source is -1 in that case.
        if (source < 0)
        {
          return new SymbolicValue(d.VoidAddr);
        }
        SymbolicValue result;
        if (!d.TryGetUnboxedValue(source, out result))
        {
          d.TryGetCorrespondingValueAbstraction(source, out result);
        }
        return result;
      }

      struct Sources<ArgList> : IIndexable<SymbolicValue>
        where ArgList : IIndexable<Temp>
      {
        private ArgList underlying;
        private readonly Domain astate;

        public Sources(ArgList underlying, Domain astate)
        {
          this.underlying = underlying;
          this.astate = astate;
        }

        #region IIndexable<SymbolicValue> Members

        public int Count
        {
          get
          {
            Contract.Ensures(Contract.Result<int>() == underlying.Count);

            return underlying.Count;
          }
        }

        public SymbolicValue this[int index]
        {
          get
          {
            Contract.Assert(index < this.Count);
            Contract.Assert(this.Count == this.underlying.Count);
            return Map(underlying[index]);
          }
        }

        private SymbolicValue Map(Temp temp)
        {
          if (astate == null) return default(SymbolicValue);
          SymbolicValue result;
          this.astate.TryGetCorrespondingValueAbstraction(temp, out result);
          return result;
        }

        #endregion

      }

      bool TryGetPostState(APC pc, out Domain postState)
      {
        return this.parent.PostStateLookup(pc, out postState);
      }
      bool TryGetPreState(APC pc, out Domain preState)
      {
        return this.parent.PreStateLookup(pc, out preState);
      }

      bool InsideOld(APC pc)
      {
        Domain prestate;
        if (TryGetPreState(pc, out prestate))
        {
          if (prestate.OldDomain != null)
          {
            return true;
          }
        }
        return false;
      }

      SymbolicValue ConvertDest(APC pc, Temp dest)
      {
        Domain postState;
        if (!this.parent.PostStateLookup(pc, out postState))
        {
          return default(SymbolicValue);
        }
        SymbolicValue result;
        postState.TryGetCorrespondingValueAbstraction(dest, out result);
        return result;
      }

      SymbolicValue ConvertOldDest(APC pc, Temp dest)
      {
        Domain postState;
        if (!this.parent.PostStateLookup(pc, out postState))
        {
          return default(SymbolicValue);
        }
        postState = postState.OldDomain;
        Contract.Assume(postState != null);
        SymbolicValue result;
        postState.TryGetCorrespondingValueAbstraction(dest, out result);
        return result;
      }

      Sources<ArgList> ConvertSources<ArgList>(APC pc, ArgList sources)
        where ArgList : IIndexable<Temp>
      {
        return new Sources<ArgList>(sources, this.parent.GetPreState(pc));
      }
      #endregion

      #region MSIL delegation
      public Result Assert(APC pc, string tag, Source condition, object provenance, Data data)
      {
        return delegatee.Assert(pc, tag, ConvertSource(pc, condition), provenance, data);
      }

      public Result Assume(APC pc, string tag, Source source, object provenance, Data data)
      {
        if (this.parent.IgnoreExplicitAssumptions && tag == "assume")
        {
          return delegatee.Nop(pc, data);
        }
        SymbolicValue sv = ConvertSource(pc, source);
        return delegatee.Assume(pc, tag, sv, provenance, data);
      }

      public Result Arglist(APC pc, Dest dest, Data data)
      {
        return delegatee.Arglist(pc, ConvertDest(pc, dest), data);
      }

      public Result Binary(APC pc, BinaryOperator op, Dest dest, Source s1, Source s2, Data data)
      {
        switch (op)
        {
          case BinaryOperator.Cobjeq:
            // handle boxing/unboxing here
            var value1 = TryConvertUnbox(pc, s1);
            var value2 = TryConvertUnbox(pc, s2);
            return delegatee.Binary(pc, op, ConvertDest(pc, dest), value1, value2, data);

          default:
            return delegatee.Binary(pc, op, ConvertDest(pc, dest), ConvertSource(pc, s1), ConvertSource(pc, s2), data);
        }
      }

      public Result BranchCond(APC pc, APC target, BranchOperator bop, Source value1, Source value2, Data data)
      {
        return delegatee.BranchCond(pc, target, bop, ConvertSource(pc, value1), ConvertSource(pc, value2), data);
      }

      public Result BranchTrue(APC pc, APC target, Source cond, Data data)
      {
        return delegatee.BranchTrue(pc, target, ConvertSource(pc, cond), data);
      }

      public Result BranchFalse(APC pc, APC target, Source cond, Data data)
      {
        return delegatee.BranchFalse(pc, target, ConvertSource(pc, cond), data);
      }

      public Result Branch(APC pc, APC target, bool leave, Data data)
      {
        return delegatee.Branch(pc, target, leave, data);
      }

      public Result Break(APC pc, Data data)
      {
        return delegatee.Break(pc, data);
      }

      public Result Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Dest dest, ArgList args, Data data)
        where TypeList : IIndexable<Type>
        where ArgList : IIndexable<Source>
      {
        if (!parent.mdDecoder.IsVoidMethod(method) && InsideOld(pc))
        {
          // we mapped back result from old state, so use old.ldstack here so abstract domain gets a chance as well.
          return delegatee.Ldstack(pc, 0, ConvertDest(pc, dest), ConvertOldDest(pc, dest), true, data);
        }
        return DelegateCall<TypeList, ArgList>(pc, method, tail, virt, extraVarargs, dest, args, data);
      }

      private Result DelegateCall<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Dest dest, ArgList args, Data data)
        where TypeList : IIndexable<Type>
        where ArgList : IIndexable<int>
      {
        Type declaringType = parent.mdDecoder.DeclaringType(method);
        if (args.Count == 2)
        {
          var methodName = parent.mdDecoder.Name(method);
          if (methodName == "Equals")
          {
            // special case equal here
            // NOTE: mdDecoder may classify String as primitive
            if (parent.mdDecoder.IsStruct(declaringType) && !parent.mdDecoder.IsStatic(method) && parent.mdDecoder.HasValueRepresentation(declaringType))
            {
              return delegatee.Binary(pc, BinaryOperator.Cobjeq, ConvertDest(pc, dest), ConvertSourceDeref(pc, args[0]), TryConvertUnbox(pc, args[1]), data);
            }
            else
            {
              return delegatee.Binary(pc, BinaryOperator.Cobjeq, ConvertDest(pc, dest), TryConvertUnbox(pc, args[0]), TryConvertUnbox(pc, args[1]), data);
            }
          }
          else if (parent.mdDecoder.IsReferenceType(parent.mdDecoder.DeclaringType(method)) || parent.mdDecoder.IsNativePointerType(parent.mdDecoder.DeclaringType(method)))
          {
            if (methodName == "op_Inequality")
            {
              return delegatee.Binary(pc, BinaryOperator.Cne_Un, ConvertDest(pc, dest), ConvertSource(pc, args[0]), ConvertSource(pc, args[1]), data);
            }
            else if (methodName == "op_Equality")
            {
              if (parent.mdDecoder.IsNativePointerType(parent.mdDecoder.DeclaringType(method)))
              {
                return delegatee.Binary(pc, BinaryOperator.Ceq, ConvertDest(pc, dest), ConvertSource(pc, args[0]), ConvertSource(pc, args[1]), data);
              }
              else
              {
                return delegatee.Binary(pc, BinaryOperator.Cobjeq, ConvertDest(pc, dest), ConvertSource(pc, args[0]), ConvertSource(pc, args[1]), data);
              }
            }
          }
        }
        return delegatee.Call(pc, method, tail, virt, extraVarargs, ConvertDest(pc, dest), ConvertSources(pc, args), data);
      }

      public Result Calli<TypeList, ArgList>(APC pc, Type returnType, TypeList argTypes, bool tail, bool isInstance, Dest dest, Source fp, ArgList args, Data data)
        where TypeList : IIndexable<Type>
        where ArgList : IIndexable<Source>
      {
        if (!parent.mdDecoder.IsVoid(returnType) && InsideOld(pc))
        {
          // we mapped back result from old state, so use old.ldstack here so abstract domain gets a chance as well.
          return delegatee.Ldstack(pc, 0, ConvertDest(pc, dest), ConvertOldDest(pc, dest), true, data);
        }

        return delegatee.Calli(pc, returnType, argTypes, tail, isInstance, ConvertDest(pc, dest), ConvertSource(pc, fp), ConvertSources(pc, args), data);
      }

      public Result Ckfinite(APC pc, Dest dest, Source source, Data data)
      {
        return delegatee.Ckfinite(pc, ConvertDest(pc, dest), ConvertSource(pc, source), data);
      }

      public Result Cpblk(APC pc, bool @volatile, Source destaddr, Source srcaddr, Source len, Data data)
      {
        return delegatee.Cpblk(pc, @volatile, ConvertSource(pc, destaddr), ConvertSource(pc, srcaddr), ConvertSource(pc, len), data);
      }

      public Result Endfilter(APC pc, Source decision, Data data)
      {
        return delegatee.Endfilter(pc, ConvertSource(pc, decision), data);
      }

      public Result Endfinally(APC pc, Data data)
      {
        return delegatee.Endfinally(pc, data);
      }

      public Result Entry(APC pc, Method method, Data data)
      {
        return delegatee.Entry(pc, method, data);
      }

      public Result Initblk(APC pc, bool @volatile, Source destaddr, Source value, Source len, Data data)
      {
        return delegatee.Initblk(pc, @volatile, ConvertSource(pc, destaddr), ConvertSource(pc, value), ConvertSource(pc, len), data);
      }

      public Result Jmp(APC pc, Method method, Data data)
      {
        return delegatee.Jmp(pc, method, data);
      }

      public Result Ldarg(APC pc, Parameter argument, bool isOld, Dest dest, Data data)
      {
        return delegatee.Ldarg(pc, argument, isOld, ConvertDest(pc, dest), data);
      }

      public Result Ldarga(APC pc, Parameter argument, bool isOld, Dest dest, Data data)
      {
        return delegatee.Ldarga(pc, argument, isOld, ConvertDest(pc, dest), data);
      }

      public Result Ldconst(APC pc, object constant, Type type, Dest dest, Data data)
      {
        return delegatee.Ldconst(pc, constant, type, ConvertDest(pc, dest), data);
      }

      public Result Ldnull(APC pc, Dest dest, Data data)
      {
        return delegatee.Ldnull(pc, ConvertDest(pc, dest), data);
      }

      public Result Ldftn(APC pc, Method method, Dest dest, Data data)
      {
        return delegatee.Ldftn(pc, method, ConvertDest(pc, dest), data);
      }

      public Result Ldind(APC pc, Type type, bool @volatile, Dest dest, Source ptr, Data data)
      {
        if (InsideOld(pc))
        {
          // we mapped back result from old state, so use old.ldstack here so abstract domain gets a chance as well.
          return delegatee.Ldstack(pc, 0, ConvertDest(pc, dest), ConvertOldDest(pc, dest), true, data);
        }

        return delegatee.Ldind(pc, type, @volatile, ConvertDest(pc, dest), ConvertSource(pc, ptr), data);
      }

      public Result Ldloc(APC pc, Local local, Dest dest, Data data)
      {
        return delegatee.Ldloc(pc, local, ConvertDest(pc, dest), data);
      }

      public Result Ldloca(APC pc, Local local, Dest dest, Data data)
      {
        return delegatee.Ldloca(pc, local, ConvertDest(pc, dest), data);
      }

      public Result Ldresult(APC pc, Type type, int dest, int source, Data data)
      {
        return delegatee.Ldresult(pc, type, ConvertDest(pc, dest), ConvertSource(pc, source), data);
      }

      public Result Ldstack(APC pc, int offset, Dest dest, Source source, bool isOld, Data data)
      {
        if (isOld)
        {
          return delegatee.Ldstack(pc, offset, ConvertDest(pc, dest), ConvertOldSource(pc, source), isOld, data);
        }
        else
        {
          return delegatee.Ldstack(pc, offset, ConvertDest(pc, dest), ConvertSource(pc, source), isOld, data);
        }
      }

      public Result Ldstacka(APC pc, int offset, Dest dest, Source source, Type type, bool old, Data data)
      {
        return delegatee.Ldstacka(pc, offset, ConvertDest(pc, dest), ConvertSource(pc, source), type, old, data);
      }

      public Result Localloc(APC pc, Dest dest, Source size, Data data)
      {
        return delegatee.Localloc(pc, ConvertDest(pc, dest), ConvertSource(pc, size), data);
      }

      public Result Nop(APC pc, Data data)
      {
        return delegatee.Nop(pc, data);
      }

      public Result Pop(APC pc, Source source, Data data)
      {
        return delegatee.Pop(pc, ConvertSource(pc, source), data);
      }

      public Result Return(APC pc, Source source, Data data)
      {
        return delegatee.Return(pc, ConvertSource(pc, source), data);
      }

      public Result Starg(APC pc, Parameter argument, Source source, Data data)
      {
        return delegatee.Starg(pc, argument, ConvertSource(pc, source), data);
      }

      public Result Stind(APC pc, Type type, bool @volatile, Source ptr, Source value, Data data)
      {
        return delegatee.Stind(pc, type, @volatile, ConvertSource(pc, ptr), ConvertSource(pc, value), data);
      }

      public Result Stloc(APC pc, Local local, Source source, Data data)
      {
        return delegatee.Stloc(pc, local, ConvertSource(pc, source), data);
      }

      public Result Switch(APC pc, Type type, IEnumerable<Pair<object, APC>> cases, Source value, Data data)
      {
        return delegatee.Switch(pc, type, cases, ConvertSource(pc, value), data);
      }

      public Result Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Dest dest, Source source, Data data)
      {
        return delegatee.Unary(pc, op, overflow, unsigned, ConvertDest(pc, dest), ConvertSource(pc, source), data);
      }

      public Result Box(APC pc, Type type, Dest dest, Source source, Data data)
      {
        return delegatee.Box(pc, type, ConvertDest(pc, dest), ConvertSource(pc, source), data);
      }

      public Result ConstrainedCallvirt<TypeList, ArgList>(APC pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Dest dest, ArgList args, Data data)
        where TypeList : IIndexable<Type>
        where ArgList : IIndexable<Source>
      {
        if (!parent.mdDecoder.IsVoidMethod(method) && InsideOld(pc))
        {
          // we mapped back result from old state, so use old.ldstack here so abstract domain gets a chance as well.
          return delegatee.Ldstack(pc, 0, ConvertDest(pc, dest), ConvertOldDest(pc, dest), true, data);
        }

        return delegatee.ConstrainedCallvirt(pc, method, tail, constraint, extraVarargs, ConvertDest(pc, dest), ConvertSources(pc, args), data);
      }

      public Result Castclass(APC pc, Type type, Dest dest, Source obj, Data data)
      {
        return delegatee.Castclass(pc, type, ConvertDest(pc, dest), ConvertSource(pc, obj), data);
      }

      public Result Cpobj(APC pc, Type type, Source destptr, Source srcptr, Data data)
      {
        return delegatee.Cpobj(pc, type, ConvertSource(pc, destptr), ConvertSource(pc, srcptr), data);
      }

      public Result Initobj(APC pc, Type type, Source ptr, Data data)
      {
        return delegatee.Initobj(pc, type, ConvertSource(pc, ptr), data);
      }

      public Result Isinst(APC pc, Type type, Dest dest, Source obj, Data data)
      {
        return delegatee.Isinst(pc, type, ConvertDest(pc, dest), ConvertSource(pc, obj), data);
      }

      Type ArrayElementType(APC pc, Type type, Source array)
      {
        if (parent.mdDecoder.IsStruct(type)) return type;
        Domain domain;
        if (!this.parent.PreStateLookup(pc, out domain))
        {
          return type;
        }
        return domain.GetArrayElementType(type, array);
      }

      public Result Ldelem(APC pc, Type type, Dest dest, Source array, Source index, Data data)
      {
        if (InsideOld(pc))
        {
          // we mapped back result from old state, so use old.ldstack here so abstract domain gets a chance as well.
          return delegatee.Ldstack(pc, 0, ConvertDest(pc, dest), ConvertOldDest(pc, dest), true, data);
        }
        type = ArrayElementType(pc, type, array);
        return delegatee.Ldelem(pc, type, ConvertDest(pc, dest), ConvertSource(pc, array), ConvertSource(pc, index), data);
      }

      public Result Ldelema(APC pc, Type type, bool @readonly, Dest dest, Source array, Source index, Data data)
      {
        type = ArrayElementType(pc, type, array);
        return delegatee.Ldelema(pc, type, @readonly, ConvertDest(pc, dest), ConvertSource(pc, array), ConvertSource(pc, index), data);
      }

      public Result Ldfld(APC pc, Field field, bool @volatile, Dest dest, Source obj, Data data)
      {
        if (InsideOld(pc))
        {
          // we mapped back result from old state, so use old.ldstack here so abstract domain gets a chance as well.
          return delegatee.Ldstack(pc, 0, ConvertDest(pc, dest), ConvertOldDest(pc, dest), true, data);
        }

        return delegatee.Ldfld(pc, field, @volatile, ConvertDest(pc, dest), ConvertSource(pc, obj), data);
      }

      public Result Ldflda(APC pc, Field field, Dest dest, Source obj, Data data)
      {
        return delegatee.Ldflda(pc, field, ConvertDest(pc, dest), ConvertSource(pc, obj), data);
      }

      public Result Ldlen(APC pc, Dest dest, Source array, Data data)
      {
        return delegatee.Ldlen(pc, ConvertDest(pc, dest), ConvertSource(pc, array), data);
      }

      public Result Ldsfld(APC pc, Field field, bool @volatile, Dest dest, Data data)
      {
        if (InsideOld(pc))
        {
          // we mapped back result from old state, so use old.ldstack here so abstract domain gets a chance as well.
          return delegatee.Ldstack(pc, 0, ConvertDest(pc, dest), ConvertOldDest(pc, dest), true, data);
        }

        return delegatee.Ldsfld(pc, field, @volatile, ConvertDest(pc, dest), data);
      }

      public Result Ldsflda(APC pc, Field field, Dest dest, Data data)
      {
        return delegatee.Ldsflda(pc, field, ConvertDest(pc, dest), data);
      }

      public Result Ldtypetoken(APC pc, Type type, Dest dest, Data data)
      {
        return delegatee.Ldtypetoken(pc, type, ConvertDest(pc, dest), data);
      }

      public Result Ldfieldtoken(APC pc, Field field, Dest dest, Data data)
      {
        return delegatee.Ldfieldtoken(pc, field, ConvertDest(pc, dest), data);
      }

      public Result Ldmethodtoken(APC pc, Method method, Dest dest, Data data)
      {
        return delegatee.Ldmethodtoken(pc, method, ConvertDest(pc, dest), data);
      }

      public Result Ldvirtftn(APC pc, Method method, Dest dest, Source obj, Data data)
      {
        return delegatee.Ldvirtftn(pc, method, ConvertDest(pc, dest), ConvertSource(pc, obj), data);
      }

      public Result Mkrefany(APC pc, Type type, Dest dest, Source obj, Data data)
      {
        return delegatee.Mkrefany(pc, type, ConvertDest(pc, dest), ConvertSource(pc, obj), data);
      }

      public Result Newarray<ArgList>(APC pc, Type type, Dest dest, ArgList lengths, Data data)
        where ArgList : IIndexable<Source>
      {
        return delegatee.Newarray(pc, type, ConvertDest(pc, dest), ConvertSources(pc, lengths), data);
      }

      public Result Newobj<ArgList>(APC pc, Method ctor, Dest dest, ArgList args, Data data)
        where ArgList : IIndexable<Source>
      {
        return delegatee.Newobj(pc, ctor, ConvertDest(pc, dest), ConvertSources(pc, args), data);
      }

      public Result Refanytype(APC pc, Dest dest, Source source, Data data)
      {
        return delegatee.Refanytype(pc, ConvertDest(pc, dest), ConvertSource(pc, source), data);
      }

      public Result Refanyval(APC pc, Type type, Dest dest, Source source, Data data)
      {
        return delegatee.Refanyval(pc, type, ConvertDest(pc, dest), ConvertSource(pc, source), data);
      }

      public Result Rethrow(APC pc, Data data)
      {
        return delegatee.Rethrow(pc, data);
      }

      public Result Sizeof(APC pc, Type type, Dest dest, Data data)
      {
        return delegatee.Sizeof(pc, type, ConvertDest(pc, dest), data);
      }

      public Result Stelem(APC pc, Type type, Source array, Source index, Source value, Data data)
      {
        type = ArrayElementType(pc, type, array);
        return delegatee.Stelem(pc, type, ConvertSource(pc, array), ConvertSource(pc, index), ConvertSource(pc, value), data);
      }

      public Result Stfld(APC pc, Field field, bool @volatile, Source obj, Source value, Data data)
      {
        return delegatee.Stfld(pc, field, @volatile, ConvertSource(pc, obj), ConvertSource(pc, value), data);
      }

      public Result Stsfld(APC pc, Field field, bool @volatile, Source value, Data data)
      {
        return delegatee.Stsfld(pc, field, @volatile, ConvertSource(pc, value), data);
      }

      public Result Throw(APC pc, Source exn, Data data)
      {
        if (this.parent.TurnArgumentExceptionThrowsIntoAssertFalse)
        {
          Type argExcType;
          if (this.parent.ArgumentExceptionTypeCache.TryGet(this.parent.mdDecoder, out argExcType))
          {
            Type type;
            SymbolicValue falseSymValue;
            if (HasSourceType(pc, exn, out type, out falseSymValue))
            {
              if (this.parent.mdDecoder.DerivesFrom(type, argExcType))
              {
                // turn into assert false.
                //return delegatee.Assert(pc, "assert", falseSymValue, null, data);
                return delegatee.Assert(pc, "assert", falseSymValue, "throw instruction", data);

              }
            }
          }
        }
        return delegatee.Throw(pc, ConvertSource(pc, exn), data);
      }

      public Result Unbox(APC pc, Type type, Dest dest, Source obj, Data data)
      {
        return delegatee.Unbox(pc, type, ConvertDest(pc, dest), ConvertSource(pc, obj), data);
      }

      public Result Unboxany(APC pc, Type type, Dest dest, Source obj, Data data)
      {
        return delegatee.Unboxany(pc, type, ConvertDest(pc, dest), ConvertSource(pc, obj), data);
      }

      #region IVisitSynthIL<APC,Method,int,int,Data,Result> Members


      public Result BeginOld(APC pc, APC matchingEnd, Data data)
      {
        return delegatee.BeginOld(pc, matchingEnd, data);
      }

      public Result EndOld(APC pc, APC matchingBegin, Type type, int dest, int source, Data data)
      {
        return delegatee.Nop(pc, data);
        //        return delegatee.EndOld(pc, matchingBegin, type, ConvertDest(pc, dest), ConvertSource(pc, source), data);
      }

      #endregion
      #endregion
    }

    class ValueContext<Context>
      : IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>
      , IValueContextData<Local, Parameter, Method, Field, Type, SymbolicValue>
      where Context : IStackContext<Field, Method>
    {
      #region Privates
      OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> parent;
      Context underlying;
      #endregion

      public ValueContext(
        OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> parent,
        Context underlying
      )
      {
        this.parent = parent;
        this.underlying = underlying;
      }

      #region IValueContext<APC,Method,Type,SymbolicValue> Members

      IValueContextData<Local, Parameter, Method, Field, Type, SymbolicValue> IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>.ValueContext { get { return this; } }

      public bool TryZero(APC at, out SymbolicValue value)
      {
        Domain d;
        if (!this.parent.PreStateLookup(at, out d) || d.IsBottom)
        {
          value = default(SymbolicValue);
          return false;
        }
        value = new SymbolicValue(d.Zero);
        return true;
      }

      public bool TryNull(APC at, out SymbolicValue value)
      {
        Domain d;
        if (!this.parent.PreStateLookup(at, out d) || d.IsBottom)
        {
          value = default(SymbolicValue);
          return false;
        }
        value = new SymbolicValue(d.Null);
        return true;
      }

      public FlatDomain<Type> GetType(APC at, SymbolicValue v)
      {
        try
        {
          Domain d;
          if (!this.parent.PreStateLookup(at, out d) || d.IsBottom)
          {
            return FlatDomain<Type>.TopValue;
          }
          return d.GetType(v.symbol).Type;
        }
        catch(NullReferenceException)
        {
#if DEBUG
          Console.WriteLine("[OptimisticHeapAnalysis]: Found a Null pointer exception in GetType for {0} @ PC = {1}. Returning Top type", v, at);
#endif
          return FlatDomain<Type>.TopValue; // Should we return Bottom?
        }
      }

      public bool TryGetArrayLength(APC at, SymbolicValue array, out SymbolicValue length)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          length = default(SymbolicValue);
          return false;
        }
        ESymValue len;
        bool success = d.TryGetArrayLength(array.symbol, out len);
        length = new SymbolicValue(len);
        return success;
      }

      public bool TryGetModelArray(APC at, SymbolicValue enumerable, out SymbolicValue modelArray)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          modelArray = default(SymbolicValue);
          return false;
        }
        ESymValue model = d.TryGetModelMethod(enumerable.symbol, "Model");
        if (model == null)
        {
          model = d.TryGetModelMethod(enumerable.symbol, "System.Collections.IEnumerable.Model");
        }
        modelArray = new SymbolicValue(model);
        return model != null;
      }

      public bool TryGetObjectFromObjectVersion(APC at, SymbolicValue objectVersion, out SymbolicValue objectValue)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          objectValue = default(SymbolicValue);
          return false;
        }
        return d.TryGetObjectFromObjectVersion(objectVersion, out objectValue);
      }

      public bool TryGetArrayFromElementAddress(APC at, SymbolicValue elementAddress, out SymbolicValue array, out SymbolicValue index)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          array = default(SymbolicValue);
          index = default(SymbolicValue);
          return false;
        }
        return d.TryGetArrayFromArrayElementAddress(elementAddress, out array, out index);
      }

      public bool TryUnbox(APC pc, SymbolicValue box, out SymbolicValue contents)
      {
        // lookup the given source in the egraph for this program point
        Domain d;
        if (!this.parent.PreStateLookup(pc, out d))
        {
          contents = default(SymbolicValue);
          return false;
        }
        return d.TryGetUnboxedValue(box.symbol, out contents);
      }

      public bool TryGetWritableBytes(APC at, SymbolicValue pointer, out SymbolicValue length)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          length = default(SymbolicValue);
          return false;
        }
        ESymValue len;
        bool success = d.TryGetWritableBytes(pointer.symbol, out len);
        length = new SymbolicValue(len);
        return success;
      }

      public bool IsZero(APC at, SymbolicValue value)
      {
        Domain entry = this.parent.GetPreState(at);
        if (entry == null) return true; // unreached
        return entry.IsZero(value.symbol);
      }

      public bool TryStackValue(APC at, Temp stackIndex, out SymbolicValue sv)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          sv = default(SymbolicValue);
          return false;
        }
        return d.TryGetCorrespondingValueAbstraction(stackIndex, out sv);
      }

      public bool TryLocalValue(APC at, Local local, out SymbolicValue sv)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          sv = default(SymbolicValue);
          return false;
        }
        return d.TryGetCorrespondingValueAbstraction(local, out sv);
      }

      public bool TryLocalAddress(APC at, Local local, out SymbolicValue sv)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          sv = default(SymbolicValue);
          return false;
        }
        return d.TryGetCorrespondingAddressAbstraction(local, out sv);
      }

      public bool TryParameterAddress(APC at, Parameter parameter, out SymbolicValue sv)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          sv = default(SymbolicValue);
          return false;
        }
        return d.TryGetCorrespondingAddressAbstraction(parameter, out sv);
      }

      public bool TryParameterValue(APC at, Parameter parameter, out SymbolicValue sv)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          sv = default(SymbolicValue);
          return false;
        }
        return d.TryGetCorrespondingValueAbstraction(parameter, out sv);
      }

      public bool TryFieldAddress(APC at, SymbolicValue obj, Field field, out SymbolicValue address)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          address = default(SymbolicValue);
          return false;
        }
        return d.TryGetFieldAddress(obj, field, out address);
      }

      public bool TryLoadIndirect(APC at, SymbolicValue addr, out SymbolicValue value)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          value = default(SymbolicValue);
          return false;
        }
        return d.TryLoadIndirect(addr, out value);
      }

      public bool TryResultValue(APC at, out SymbolicValue resultValue)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          resultValue = default(SymbolicValue);
          return false;
        }

        return d.TryGetCorrespondingValueAbstraction(0, out resultValue); // top of stack
      }

      public bool IsPureFunctionCall(APC at, SymbolicValue value, out Method method, out SymbolicValue[] args)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          method = default(Method);
          args = null;
          return false;
        }
        return d.IsPureFunctionCall(value, out method, out args);
      }

      public bool IsDelegateValue(APC at, SymbolicValue value, out SymbolicValue targetObject, out Method targetMethod)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          targetMethod = default(Method);
          targetObject = default(SymbolicValue);
          return false;
        }
        return d.IsDelegateValue(value, out targetObject, out targetMethod);
      }

      public bool IsValid(SymbolicValue sv)
      {
        return sv.symbol != null;
      }

      public string AccessPath(APC at, SymbolicValue value)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          throw new ArgumentException("pc was not visited");
        }
        return d.GetAccessPath(value.symbol);
      }

      public FList<PathElement> AccessPathList(APC at, SymbolicValue value, bool allowLocal, bool preferLocal, Type allowReturnValueFromCall = default(Type))
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          throw new ArgumentException("pc was not visited");
        }

        return AccessPathList(at, value, allowLocal, preferLocal, allowReturnValueFromCall, d);
      }

      private FList<PathElement> AccessPathList(APC at, SymbolicValue value, bool allowLocal, bool preferLocal, Type allowReturnValueFromCall, Domain d)
      {
        AccessPathFilter<Method, Type> opt;
        if (allowReturnValueFromCall != null && !allowReturnValueFromCall.Equals(default(Type)))
        {
          var stackDepth = this.parent.context.StackContext.LocalStackDepth(at);
          // use the stackDepth in the filter to determine if the value is the top-of-the-stack


          opt = AccessPathFilter<Method, Type>.IsVisibleFromMethodOrCalledPost(this.MethodContext.CurrentMethod, allowReturnValueFromCall, stackDepth);
        }
        else
        {
          opt = AccessPathFilter<Method, Type>.IsVisibleFrom(this.MethodContext.CurrentMethod);
        }
        return d.GetAccessPathList(value.symbol, opt, allowLocal, preferLocal);
      }

      public FList<PathElement> AccessPathListAndWitness(APC at, SymbolicValue value, bool allowLocal, bool preferLocal, out FList<SymbolicValue> witness, Type allowReturnValueFromCall = default(Type))
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          throw new ArgumentException("pc was not visited");
        }

        var ap = AccessPathList(at, value, allowLocal, preferLocal, allowReturnValueFromCall, d);
        if (ap == null) { witness = null; return null; }

        var rev = d.AccessPathWitnessReversed(d, ap);
        if (rev == null)
        {
          // something is wrong
          witness = null;
          return null;
        }
        witness = rev.Reverse(sv => new SymbolicValue(sv));
        return ap;
      }

      public IEnumerable<FList<PathElement>> AccessPaths(APC at, SymbolicValue value, AccessPathFilter<Method, Type> filter)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          throw new ArgumentException("pc was not visited");
        }
        foreach (var path in d.GetAccessPathsFiltered(value.symbol, filter, true))
        {
          yield return path.Coerce<Domain.PathElementBase, PathElement>();
        }
      }

      public FList<PathElement> VisibleAccessPathListFromPre(APC at, SymbolicValue value)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          return null;
        }
        var opt = AccessPathFilter<Method, Type>.FromPrecondition(this.MethodContext.CurrentMethod);
        return d.GetBestAccessPath(value.symbol, opt, true, false, false, path => PathSuitableInRequires(at, path));
      }

      public FList<PathElement> VisibleAccessPathListFromPost(APC at, SymbolicValue value, bool allowReturnValue = true)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          return null;
        }
        var opt = AccessPathFilter<Method, Type>.FromPostcondition(this.MethodContext.CurrentMethod, this.parent.mdDecoder.ReturnType(this.MethodContext.CurrentMethod));
        return d.GetBestAccessPath(value.symbol, opt, true, false, false, path => PathSuitableInEnsures(at, path, allowReturnValue));
      }


      public bool PathUnmodifiedSinceEntry(APC at, FList<PathElement> path)
      {
        if (path == null) return false;
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          throw new ArgumentException("pc was not visited");
        }
        ESymValue sym;
        if (d.IsTopLevelVisibleAtMethodEntry(path.Head, out sym))
        {
          return d.CheckIfPathUnmodified(sym, path.Tail);
        }
        return false;
      }

      public bool PathSuitableInRequires(APC at, FList<PathElement> path)
      {
        if (path == null) return false;
        var pe = path.Head as Domain.MethodCallPathElement;
        if (pe != null)
        {
          // must be a static property. Ignore it.
          return false;
        }
        return PathUnmodifiedSinceEntry(at, path);
      }

      public bool PathSuitableInEnsures(APC at, FList<PathElement> path, bool allowReturnValue)
      {
        if (path == null) return false;
        var pe = path.Head as Domain.MethodCallPathElement;
        if (pe != null)
        {
          // must be a static property. Ignore it.
          return false;
        }
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          throw new ArgumentException("pc was not visited");
        }
        ESymValue sym;
        if (d.IsTopLevelVisibleAtMethodEntry(path.Head, out sym))
        {
          return true;
        }
        if (allowReturnValue && path.Head.IsReturnValue) return true;
        return false;
      }


      public bool IsConstant(APC at, SymbolicValue sv, out Type type, out object value)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          throw new ArgumentException("pc was not visited");
        }
        return d.IsConstant(sv.symbol, out type, out value);
      }

      public bool IsRuntimeType(APC at, SymbolicValue sv, out Type type)
      {
        SymbolicValue[] args;
        Method method;

        type = default(Type);
        if (!IsPureFunctionCall(at, sv, out method, out args)) return false;
        if (this.parent.mdDecoder.Name(method) != "GetTypeFromHandle") return false;
        Type dummy;
        object value;
        if (args.Length != 1) return false;
        if (!IsConstant(at, args[0], out dummy, out value)) return false;
        if (!(value is Type)) return false;

        type = (Type)value;
        return true;
      }

      public bool IsRootedInParameter(FList<PathElement> path)
      {
        return Domain.IsRootedInParameter(path);
      }

      public bool IsRootedInReturnValue(FList<PathElement> path)
      {
        return Domain.IsRootedInReturnValue(path);
      }

      public PathContextFlags PathContexts(APC at, SymbolicValue sv)
      {
        Domain d;
        if (!parent.PreStateLookup(at, out d))
        {
          throw new ArgumentException("pc was not visited");
        }
        return d.PathContexts(sv.symbol);
      }

      #endregion

      #region IMethodContext<Method> Members
      public IStackContextData<Field, Method> StackContext { get { return this.underlying.StackContext; } }
      public IMethodContextData<Field, Method> MethodContext { get { return this.underlying.MethodContext; } }

      #endregion
    }

    class Decoder<ContextData> :
      IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>, EdgeData>
      where ContextData : IStackContext<Field, Method>
    {
      #region Privates
      OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> parent;
      IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Temp, Temp, ContextData, Unit> underlying;
      #endregion

      public Decoder(
        OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> parent,
        IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Temp, Temp, ContextData, Unit> underlying
        )
      {
        this.parent = parent;
        this.underlying = underlying;
      }

      #region IMSILDecoder<APC,Local,Parameter,Method,Field,Type,SymbolicValue,SymbolicValue> Members

      public Result ForwardDecode<Data, Result, Visitor>(APC lab, Visitor visitor, Data data) where Visitor : IVisitMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, Data, Result>
      {
        return this.underlying.ForwardDecode<Data, Result, SymbolicAdapter<Data, Result, Visitor>>(lab, new SymbolicAdapter<Data, Result, Visitor>(this.parent, visitor), data);
      }

      ValueContext<ContextData> context;
      public IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue> Context
      {
        get
        {
          if (context == null)
          {
            context = new ValueContext<ContextData>(this.parent, this.underlying.Context);
          }
          return context;
        }
      }

      public EdgeData EdgeData(APC from, APC to)
      {
        // avoid renaming when not necessary
        if (!parent.renamePoints.ContainsKey(from, to)) return null;

        // optizmize identity renaming
        if (this.parent.mergeInfoCache.ContainsKey(to) && this.parent.mergeInfoCache[to] == null)
        {
          return null;
        }

        // revisit the issue of the fact that EdgeRenaming may provide different info based on whether it is a join point or not.
        return this.parent.EdgeRenaming(new Pair<APC, APC>(from, to), this.context.MethodContext.CFG.IsJoinPoint(to));
      }

      public void Display(TextWriter tw, string prefix, EdgeData edgeData)
      {
        if (edgeData == null) return;
        edgeData.Visit(delegate(SymbolicValue key, FList<SymbolicValue> targets)
        {
          tw.Write("  {0} -> ", key);
          foreach (var target in targets.GetEnumerable())
          {
            tw.Write("{0} ", target);
          }
          tw.WriteLine();
          return VisitStatus.ContinueVisit;
        });
      }

      public bool IsUnreachable(APC pc)
      {
        return this.parent.IsUnreachable(pc);
      }
      #endregion

    }

    #endregion

    public IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>, EdgeData>
      GetDecoder<Context>(
        IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Temp, Temp, Context, Unit> underlying
      )
      where Context : IStackContext<Field, Method>
    {
      return new Decoder<Context>(this, underlying);
    }

    #region Value adapter
#if false
    class ValueDriver<AState> :
      IAnalysis<APC, AState, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Temp, Temp, AState, AState>, Unit>
    {
      IValueAnalysis<APC, AState, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, AState, AState>, SymbolicValue, IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>> valuedriver;
      OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> parent;
      IVisitMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, AState, AState> delegatee;

      public ValueDriver(
        IValueAnalysis<APC, AState, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, AState, AState>, SymbolicValue, IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>> valuedriver,
        OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> parent
      )
      {
        this.valuedriver = valuedriver;
        this.parent = parent;
      }

    #region IAnalysis<APC,AState,IVisitMSIL<APC,Local,Parameter,Method,Field,Type,int,int,AState,AState>> Members


      /// <summary>
      /// This is the trickiest part of the whole abstraction. Along the given edge, the heap state was possibly renamed, and
      /// a single symbolic name might correspond to multiple names in the target state. We have to perform the same renaming
      /// and constraining of the abstract state here.
      /// </summary>
      /// <param name="edge">Edge along there might be renaming of the egraph symbolic values</param>
      /// <param name="state">Abstract client state</param>
      /// <returns>New abstract client state renamed to target vocabulary</returns>
      public AState EdgeConversion(APC from, APC next, bool joinPoint, Unit edgeData, AState state)
      {
        // We only need to do the renaming if next is a join point since we cached all 
        // original program states.
        if (!this.parent.renamePoints.ContainsKey(from, next)) return state;
        
        // optimize Identity renaming case
        if (this.parent.mergeInfoCache.ContainsKey(next) && this.parent.mergeInfoCache[next] == null)
        {
          return state;
        }

        Pair<APC, APC> edge = new Pair<APC, APC>(from, next);
        IFunctionalMap<SymbolicValue, FList<SymbolicValue>>/*?*/ renaming = this.parent.EdgeRenaming(edge, joinPoint);

        if (renaming != null) {
          state = this.valuedriver.ParallelAssign(edge, renaming, state);
        }
        else {
          // identity renaming
        }

        return state;
      }

      /// <summary>
      /// The states joined already use the same variable vocabulary, as th Edge renaming has already happened.
      /// Thus, we can just use the original joiner, no need to interpose.
      /// </summary>
      public AState Join(Pair<APC, APC> edge, AState newState, AState prevState, out bool weaker, bool widen)
      {
        return valuedriver.Join(edge, newState, prevState, out weaker, widen);
      }

      public IVisitMSIL<APC, Local, Parameter, Method, Field, Type, int, int, AState, AState> Visitor()
      {
        this.delegatee = valuedriver.Visitor();

        return new SymbolicAdapter<AState, AState, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, AState, AState>>(this.parent, this.delegatee);
      }

      public Predicate<APC> CacheStates(IFixpointInfo<APC, AState> fixpointInfo)
      {
        return this.valuedriver.CacheStates(fixpointInfo);
      }

      public void Dump(Pair<AState, TextWriter> stateAndWriter)
      {
        this.valuedriver.Dump(stateAndWriter);
      }

      public AState ImmutableVersion(AState state)
      {
        return this.valuedriver.ImmutableVersion(state);
      }

      public AState MutableVersion(AState state)
      {
        return this.valuedriver.MutableVersion(state);
      }

      public bool IsBottom(APC pc, AState state)
      {
        Domain d;
        if (!parent.PreStateLookup(pc, out d))
        {
          return true;
        }
        if (d.IsBottom) return true;
        return this.valuedriver.IsBottom(pc, state);
      }

      #endregion



    }
#endif
    #endregion

#if false
    public IAnalysis<APC, AState, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Temp, Temp, AState, AState>, Unit>
      GetValueDriver<AState>(
        IValueAnalysis<APC, AState, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, AState, AState>, SymbolicValue, IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>> valuedriver)
    {
      return new ValueDriver<AState>(valuedriver, this);
    }
#endif

    public BlockInfoPrinter<APC> GetEdgePrinter()
    {
      Converter<SymbolicValue, string> sourcePrinter = delegate(SymbolicValue sv) { return sv.ToString(); };
      return GetBlockInfoPrinter(sourcePrinter);
    }

    public BlockInfoPrinter<APC> GetBlockInfoPrinter(Converter<SymbolicValue, string> source2string)
    {
      return delegate(APC blockEnd, string prefix, TextWriter tw)
      {
        BlockInfoPrinter(blockEnd, prefix, tw, source2string);
      };
    }

    public FlatDomain<Type> SymbolTypeAtPreState(APC pc, SymbolicValue sym)
    {
      Domain d;
      if (PreStateLookup(pc, out d))
      {
        return d.GetType(sym.symbol).Type;
      }
      return FlatDomain<Type>.TopValue;
    }

    public void BlockInfoPrinter(APC blockEnd, string prefix, TextWriter tw, Converter<SymbolicValue, string> source2string)
    {
      foreach (APC blockTarget in this.renamePoints.Keys2(blockEnd))
      {
        Pair<APC, APC> edge = new Pair<APC, APC>(blockEnd, blockTarget);
        IFunctionalMap<SymbolicValue, FList<SymbolicValue>>/*?*/ renaming = this.EdgeRenaming(edge, blockTarget.Block.Subroutine.IsJoinPoint(blockTarget.Block));
        if (renaming != null)
        {
          edge = PrintRenaming(prefix, tw, source2string, edge, renaming);
        }
      }
    }

    private Pair<APC, APC> PrintRenaming(string prefix, TextWriter tw, Converter<SymbolicValue, string> source2string, Pair<APC, APC> edge, IFunctionalMap<SymbolicValue, FList<SymbolicValue>> renaming)
    {
      tw.WriteLine("{0}Rebinding on {1}", prefix, edge);
      foreach (SymbolicValue source in renaming.Keys.OrderBy(s => s))
      {
        FList<SymbolicValue> targets = renaming[source];
        while (targets != null)
        {
          SymbolicValue target = targets.Head;
          tw.WriteLine("{0}  {1} := {2} {3}", prefix, target, source2string(source), SymbolTypeAtPreState(edge.One, source));
          Domain d;
          this.PreStateLookup(edge.One, out d);
          if (d != null)
          {
            string path = d.GetAccessPath(source.symbol);
            if (path != null)
            {
              tw.WriteLine("{0}     Access path to {1} = {2}", prefix, source2string(source), path);
            }
          }
          targets = targets.Tail;
        }
      }
      return edge;
    }

    public IEnumerable<APC> RenamingTargets(APC from)
    {
      return this.renamePoints.Keys2(from);
    }

    /// <summary>
    /// null result means identity, thus no renaming needed
    /// </summary>
    internal IFunctionalMap<SymbolicValue, SymbolicValue> BackwardEdgeRenaming(APC from, APC to)
    {
      // We only need to do the renaming if next is a join point since we cached all 
      // original program states.
      IFunctionalMap<SymbolicValue, SymbolicValue> result;
      Pair<APC, APC> edge = new Pair<APC, APC>(from, to);

      if (this.backwardRenamings.TryGetValue(edge, out result))
      {
        return result;
      }

      if (!this.renamePoints.ContainsKey(to, from))
      {
        if (from.SubroutineContext != to.SubroutineContext) return null;
        if (!to.Block.Last.Equals(to)) return null;
        if (!from.Block.First.Equals(from)) return null;

        APC pred;
        if (!this.context.MethodContext.CFG.HasSinglePredecessor(from, out pred, skipContracts: false)) return null;
        if (pred.Equals(to)) return null;
        // looks like we skipped over contracts. 

        result = null;
        // compute renaming.
        Domain start;
        this.PostStateLookup(to, out start);
        Domain end;
        this.PreStateLookup(from, out end);
        IFunctionalMap<ESymValue, FList<ESymValue>>/*?*/ forward;
        IFunctionalMap<ESymValue, ESymValue>/*?*/ backward;
        if (start != null && end != null)
        {
          if (!end.LessEqual(start, out forward, out backward))
          {
            // failed to compute a renaming
            return null;
          }
          if (forward != null)
          {
            result = FunctionalIntKeyMap<SymbolicValue, SymbolicValue>.Empty(SymbolicValue.GetUniqueKey);
            foreach (ESymValue source in forward.Keys)
            {
              var candidates = forward[source];
              if (candidates != null)
              {
                result = result.Add(new SymbolicValue(source), new SymbolicValue(candidates.Head));
              }
            }
          }
        }
        this.backwardRenamings.Add(edge, result);
        return result;
      }
      else
      {
        result = null;
        // compute renaming.
        Domain start;
        this.PostStateLookup(to, out start);
        Domain end;
        this.PreStateLookup(from, out end);
        IFunctionalMap<ESymValue, FList<ESymValue>>/*?*/ forward;
        IFunctionalMap<ESymValue, ESymValue>/*?*/ backward;
        if (start != null && end != null)
        {
          if (!start.LessEqual(end, out forward, out backward))
          {
            Contract.Assume(false, "egraphs don't correspond at joins"); // means egraphs don't correspond
            throw new Exception("Should never happen");
          }
          if (backward != null)
          {
            result = FunctionalIntKeyMap<SymbolicValue, SymbolicValue>.Empty(SymbolicValue.GetUniqueKey);
            foreach (ESymValue source in backward.Keys)
            {
              result = result.Add(new SymbolicValue(source), new SymbolicValue(backward[source]));
            }
          }
        }
        this.backwardRenamings.Add(edge, result);
        return result;
      }
    }
    Dictionary<Pair<APC, APC>, IFunctionalMap<SymbolicValue, SymbolicValue>> backwardRenamings = new Dictionary<Pair<APC, APC>, IFunctionalMap<SymbolicValue, SymbolicValue>>();
  }
}

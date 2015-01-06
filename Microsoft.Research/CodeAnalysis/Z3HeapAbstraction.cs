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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  using Temp = System.Int32; // Stack locations
  using Dest = System.Int32; // Stack location
  using Source = System.Int32; // Stack location
  using SubroutineContext = FList<Tuple<CFGBlock, CFGBlock, string>>;
  using SubroutineEdge = Tuple<CFGBlock, CFGBlock, string>;

  /// <summary>
  /// The Z3 Heap analysis is structured as follows:
  /// - First, an egraph based approximation of aliasing is computed for the sole purpose of estiimating reachability at call
  ///   sites and estimating resulting modifies at these sites.
  /// - Then, we compute a Z3 model by breaking back-edges and inserting havocs in loop heads. We compute the loop modifies at the 
  ///   same time, again approximately using finitization of what can be modified.
  /// - In order to present a symbolic model to abstract interpretation that has stable variables, we need to compute best/equivalent
  ///   terms along straight lines of code for lookups.
  /// </summary>
  public class Z3HeapAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
    where Type:IEquatable<Type>
  {
    Data data;

    class Data
    {
    }

    class Modifies
    {
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
        Dictionary<BinaryOperator, Constructor.Wrapper<BinaryOperator>> binaryOps;
        Dictionary<UnaryOperator, Constructor.Wrapper<UnaryOperator>> unaryOps;

        int idgen;

        private Constructor.Wrapper<T> For<T>(T value, Dictionary<T, Constructor.Wrapper<T>> cache)
        {
          Constructor.Wrapper<T>/*?*/ result;
          if (!cache.TryGetValue(value, out result))
          {
            result = Constructor.For(value, ref idgen, this.mdDecoder);
            cache.Add(value, result);
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
          Constructor.Wrapper<object> c = For(constant, programConstants);
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
          Constructor.Wrapper<Method> c = For(method, methodPointers);
          c.Type = type;
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
              System.Diagnostics.Debug.Assert(!this.mdDecoder.Equal(type, this.mdDecoder.System_Int32) || value is Int32);
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
        /// Breadcrumb to tag values resulting from ldelem. (target doesn't matter)
        /// </summary>
        public readonly Constructor ResultOfLdelem;

#if false
        /// <summary>
        /// Used to approximate hash consing across join points of ElementAddr(a,i)
        /// </summary>
        public readonly Constructor LastArrayIndex;
        public readonly Constructor LastArrayElemAddress;
#endif
        readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;

        public WrapTable(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
        {
          this.mdDecoder = mdDecoder;

          this.locals = new Dictionary<Local, Constructor.Wrapper<Local>>();
          this.parameters = new Dictionary<Parameter, Constructor.Wrapper<Parameter>>();
          this.fields = new Dictionary<Field, Constructor.Wrapper<Field>>();
          this.pseudoFields = new Dictionary<Method, Constructor.Wrapper<Method>>();
          this.temps = new Dictionary<int, Constructor.Wrapper<Temp>>();
          this.strings = new Dictionary<string, Constructor.Wrapper<string>>();
          this.programConstants = new Dictionary<object, Constructor.Wrapper<object>>();
          this.methodPointers = new Dictionary<Method, Constructor.Wrapper<Method>>();
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
          NeZero = For("$NeZero");
          BoxOperator = For("$Box");
          FunctionPointer = For("$FnPtr");
          ClosureObject = For("$Closure");

          // Breadcrumbs: we use these to tag values (target doesn't matter)
          ResultOfCall = For("$ResultOfCall");
          ResultOfLdelem = For("$ResultOfLdElem");

          ZeroValue = ForConstant((int)0, this.mdDecoder.System_Int32);
        }
      }


      internal abstract class Constructor : IEquatable<Constructor>, IConstantInfo, IVisibilityCheck<Method>
      {
        readonly int id;
        protected readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;

        public int GetId() { return id; }

        protected Constructor(ref int idgen, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
        {
          this.id = idgen++;
          this.mdDecoder = mdDecoder;
        }

        public abstract bool ActsAsField { get; }

        public abstract bool IsVirtualMethod { get; }

        public abstract bool KeepAsBottomField { get; }

        public abstract bool ManifestField { get; }

        public abstract Type FieldAddressType(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder);

        public abstract bool HasExtraDeref { get; }

        public abstract bool IsAsVisibleAs(Method method);

        public abstract bool IsVisibleFrom(Method method);

        public abstract bool IfRootIsParameter { get; }
        /// <summary>
        /// For fields and properties, this return whether they are static. Otherwise false.
        /// </summary>
        public abstract bool IsStatic { get; }

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
              string name = this.mdDecoder.Name(m);

              Property prop;
              if (mdDecoder.IsPropertyGetter(m, out prop) || mdDecoder.IsPropertySetter(m, out prop))
              {
                name = mdDecoder.Name(prop);
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
            return Value.ToString();
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

          public override bool IfRootIsParameter
          {
            get
            {
              if (this.Value is Field)
              {
                Field f = (Field)(object)this.Value;
                return !mdDecoder.IsStatic(f);
              }

              if (this.Value is Method)
              {
                Method m = (Method)(object)this.Value;
                return !mdDecoder.IsStatic(m);
              }

              if (this.Value is Parameter)
              {
                return true;
              }

              if (this.Value is Local)
              {
                return false;
              }

              if (this.Value is string)
              {
                // Special case, $Length, $Value or $WritteableByte, always visible
                return true;
              }
              return true;
            }
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

        public class PureMethodConstructor : Constructor.Wrapper<Method>
        {
          public PureMethodConstructor(Method method, ref int idgen, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
            : base(method, ref idgen, mdDecoder)
          {
          }

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
              Property prop;
              return this.mdDecoder.IsPropertyGetter(this.Value, out prop);
            }
          }

        }

        public class ParameterConstructor : Constructor.Wrapper<Parameter>
        {
          Parameter p { get { return this.Value; } }
          int argumentIndex;

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

          public override bool HasExtraDeref
          {
            get { return /*true*/ false; } // Mic: parameter have an extra deref only if they are out or ref, but it's covered by the IsAddressOf field
          }
        }

        internal static Constructor.Wrapper<T> For<T>(T value, ref int idgen, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
        {
          if (value is Parameter)
          {
            return (Constructor.Wrapper<T>)(object)new ParameterConstructor((Parameter)(object)value, ref idgen, mdDecoder);
          }
          if (typeof(T).Equals(typeof(Method)) && value is Method)
          {
            return (Constructor.Wrapper<T>)(object)new PureMethodConstructor((Method)(object)value, ref idgen, mdDecoder);
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

      class AbstractType : IEquatable<AbstractType>, IAbstractValueForEGraph<AbstractType>
      {
        #region IEquatable<AbstractType> Members

        public bool Equals(AbstractType other)
        {
          throw new NotImplementedException();
        }

        #endregion

        #region IAbstractValueForEGraph<AbstractType> Members

        public bool HasAllBottomFields
        {
          get { throw new NotImplementedException(); }
        }

        public AbstractType ForManifestedField()
        {
          throw new NotImplementedException();
        }

        #endregion

        #region IAbstractValue<AbstractType> Members

        public AbstractType Top
        {
          get { throw new NotImplementedException(); }
        }

        public AbstractType Bottom
        {
          get { throw new NotImplementedException(); }
        }

        public bool IsTop
        {
          get { throw new NotImplementedException(); }
        }

        public bool IsBottom
        {
          get { throw new NotImplementedException(); }
        }

        public AbstractType ImmutableVersion()
        {
          throw new NotImplementedException();
        }

        public AbstractType Clone()
        {
          throw new NotImplementedException();
        }

        public AbstractType Join(AbstractType newState, out bool weaker, bool widen)
        {
          throw new NotImplementedException();
        }

        public AbstractType Meet(AbstractType that)
        {
          throw new NotImplementedException();
        }

        public bool LessEqual(AbstractType that)
        {
          throw new NotImplementedException();
        }

        public void Dump(System.IO.TextWriter tw)
        {
          throw new NotImplementedException();
        }

        #endregion
      }

      public class Domain : IAbstractValue<Domain>
      {
        #region State shared among all instances
        static Domain top;
        static Domain bot;
        #endregion

        #region State private to this region
        /// <summary>
        /// The egraph is used to keep track of must aliasing. It also associates a Type with each symbolic value
        /// to do some type recovery that is useful in later phases.
        /// </summary>
        readonly EGraph<Constructor, AbstractType> egraph;

        #endregion

        #region Constructors
        private Domain() { }

        private Domain(
          Domain from,
          EGraph<Constructor, AbstractType> egraph
          )
        {
          this.egraph = egraph;
        }
        #endregion

        #region IAbstractValue<Domain> Members

        public Domain Top
        {
          get
          {
            if (top == null)
            {
              top = new Domain();
            }
            return top;
          }
        }

        public Domain Bottom
        {
          get
          {
            if (bot == null)
            {
              bot = new Domain();
            }
            return bot;
          }
        }

        public bool IsTop
        {
          get
          {
            return this == top;
          }
        }

        public bool IsBottom
        {
          get
          {
            return this == bot;
          }
        }

        public Domain ImmutableVersion()
        {
          if (this.egraph != null)
          {
            this.egraph.ImmutableVersion(); // mark immutable
          }
          return this;
        }

        public Domain Clone()
        {
          return new Domain(this,
            this.egraph.Clone()
            );
        }

        public Domain Join(Domain newState, out bool weaker, bool widen)
        {
          throw new NotImplementedException();
        }

        public Domain Meet(Domain that)
        {
          throw new NotImplementedException();
        }

        public bool LessEqual(Domain that)
        {
          throw new NotImplementedException();
        }

        public void Dump(System.IO.TextWriter tw)
        {
          throw new NotImplementedException();
        }

        #endregion
      }

      public class Analysis :
        IAnalysisDriver<APC, Domain, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Temp, Temp, Domain, Domain>, IStackContext<APC, Field, Method>>,
        IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Source, Source, Domain, Domain>
      {

        #region State
        const bool EagerCaching = true;
        IStackContext<APC, Field, Method> context;
        IFixpointInfo<APC, Domain> fixpointInfo;
        #endregion

        #region IAnalysisDriver<APC,Domain,IVisitMSIL<APC,Local,Parameter,Method,Field,Type,int,int,Domain,Domain>,IStackContext<APC,Field,Method>> Members

        public Domain EdgeConversion(APC from, APC next, bool joinPoint, Domain newState)
        {
          throw new NotImplementedException();
        }

        public Joiner<APC, Domain> Joiner()
        {
          throw new NotImplementedException();
        }

        public Converter<Domain, Domain> MutableVersion()
        {
          throw new NotImplementedException();
        }

        public Converter<Domain, Domain> ImmutableVersion()
        {
          throw new NotImplementedException();
        }

        public Action<Pair<Domain, System.IO.TextWriter>> Dumper()
        {
          throw new NotImplementedException();
        }

        public Func<APC, Domain, bool> IsBottom()
        {
          return (apc, domain) => domain.IsBottom;
        }

        public IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Source, Source, Domain, Domain> Visitor(IStackContext<APC, Field, Method> context)
        {
          this.context = context;
          return this;
        }

        public Predicate<APC> CacheStates(IFixpointInfo<APC, Domain> fixpointInfo)
        {
          this.fixpointInfo = fixpointInfo;
          return delegate(APC pc) { return EagerCaching; };
        }

        #endregion

        #region Per instruction semantics for heap abstraction analysis
        #region IVisitMSIL<APC,Local,Parameter,Method,Field,Type,int,int,Domain,Domain> Members

        public Domain Arglist(APC pc, Source dest, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain BranchCond(APC pc, APC target, BranchOperator bop, Source value1, Source value2, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain BranchTrue(APC pc, APC target, Source cond, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain BranchFalse(APC pc, APC target, Source cond, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Branch(APC pc, APC target, bool leave, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Break(APC pc, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Source dest, ArgList args, Domain data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<Source>
        {
          throw new NotImplementedException();
        }

        public Domain Calli<TypeList, ArgList>(APC pc, Type returnType, TypeList argTypes, bool tail, Source dest, Source fp, ArgList args, Domain data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<Source>
        {
          throw new NotImplementedException();
        }

        public Domain Ckfinite(APC pc, Source dest, Source source, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Cpblk(APC pc, bool @volatile, Source destaddr, Source srcaddr, Source len, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Endfilter(APC pc, Source decision, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Endfinally(APC pc, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Initblk(APC pc, bool @volatile, Source destaddr, Source value, Source len, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Jmp(APC pc, Method method, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldarg(APC pc, Parameter argument, bool isOld, Source dest, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldarga(APC pc, Parameter argument, bool isOld, Source dest, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldftn(APC pc, Method method, Source dest, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldind(APC pc, Type type, bool @volatile, Source dest, Source ptr, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldloc(APC pc, Local local, Source dest, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldloca(APC pc, Local local, Source dest, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Localloc(APC pc, Source dest, Source size, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Nop(APC pc, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Pop(APC pc, Source source, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Return(APC pc, Source source, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Starg(APC pc, Parameter argument, Source source, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Stind(APC pc, Type type, bool @volatile, Source ptr, Source value, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Stloc(APC pc, Local local, Source source, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Switch(APC pc, Type type, IEnumerable<Pair<object, APC>> cases, Source value, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Box(APC pc, Type type, Source dest, Source source, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain ConstrainedCallvirt<TypeList, ArgList>(APC pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Source dest, ArgList args, Domain data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<Source>
        {
          throw new NotImplementedException();
        }

        public Domain Castclass(APC pc, Type type, Source dest, Source obj, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Cpobj(APC pc, Type type, Source destptr, Source srcptr, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Initobj(APC pc, Type type, Source ptr, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldelem(APC pc, Type type, Source dest, Source array, Source index, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldelema(APC pc, Type type, bool @readonly, Source dest, Source array, Source index, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldfld(APC pc, Field field, bool @volatile, Source dest, Source obj, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldflda(APC pc, Field field, Source dest, Source obj, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldlen(APC pc, Source dest, Source array, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldsfld(APC pc, Field field, bool @volatile, Source dest, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldsflda(APC pc, Field field, Source dest, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldtypetoken(APC pc, Type type, Source dest, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldfieldtoken(APC pc, Field field, Source dest, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldmethodtoken(APC pc, Method method, Source dest, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldvirtftn(APC pc, Method method, Source dest, Source obj, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Mkrefany(APC pc, Type type, Source dest, Source obj, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Newarray<ArgList>(APC pc, Type type, Source dest, ArgList len, Domain data) where ArgList : IIndexable<Source>
        {
          throw new NotImplementedException();
        }

        public Domain Newobj<ArgList>(APC pc, Method ctor, Source dest, ArgList args, Domain data) where ArgList : IIndexable<Source>
        {
          throw new NotImplementedException();
        }

        public Domain Refanytype(APC pc, Source dest, Source source, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Refanyval(APC pc, Type type, Source dest, Source source, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Rethrow(APC pc, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Stelem(APC pc, Type type, Source array, Source index, Source value, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Stfld(APC pc, Field field, bool @volatile, Source obj, Source value, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Stsfld(APC pc, Field field, bool @volatile, Source value, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Throw(APC pc, Source exn, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Unbox(APC pc, Type type, Source dest, Source obj, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Unboxany(APC pc, Type type, Source dest, Source obj, Domain data)
        {
          throw new NotImplementedException();
        }

        #endregion

        #region IVisitSynthIL<APC,Method,Type,int,int,Domain,Domain> Members

        public Domain Entry(APC pc, Method method, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Assume(APC pc, string tag, Source condition, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Assert(APC pc, string tag, Source condition, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldstack(APC pc, Source offset, Source dest, Source source, bool isOld, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldstacka(APC pc, Source offset, Source dest, Source source, Type origParamType, bool isOld, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldresult(APC pc, Source dest, Source source, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain BeginOld(APC pc, APC matchingEnd, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain EndOld(APC pc, APC matchingBegin, Type type, Source dest, Source source, Domain data)
        {
          throw new NotImplementedException();
        }

        #endregion

        #region IVisitExprIL<APC,Type,int,int,Domain,Domain> Members

        public Domain Binary(APC pc, BinaryOperator op, Source dest, Source s1, Source s2, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Isinst(APC pc, Type type, Source dest, Source obj, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldconst(APC pc, object constant, Type type, Source dest, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Ldnull(APC pc, Source dest, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Sizeof(APC pc, Type type, Source dest, Domain data)
        {
          throw new NotImplementedException();
        }

        public Domain Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Source dest, Source source, Domain data)
        {
          throw new NotImplementedException();
        }

        #endregion
        #endregion
      }
    }
  }
}

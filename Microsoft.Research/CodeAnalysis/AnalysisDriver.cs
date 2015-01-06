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
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  using Provenance = IEnumerable<ProofObligation>;
  using System.Diagnostics.CodeAnalysis;

  public class BasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>
    : IBasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>
    where LogOptions : IFrameworkLogOptions
    where Type : IEquatable<Type>
  {
    #region Protected

    protected readonly LogOptions options;
    protected readonly IOutput output;

    protected readonly MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> methodCache;
    protected readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;
    protected readonly IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder;
    #endregion

    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.options != null);
    }

    public BasicAnalysisDriver(
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
      IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder,
      IOutput output,
      LogOptions options
    )
    {
      Contract.Requires(mdDecoder != null);
      Contract.Requires(contractDecoder != null);
      Contract.Requires(output != null);
      Contract.Requires(options != null);

      this.methodCache = new MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly>(mdDecoder, contractDecoder, output.WriteLine);
      this.mdDecoder = mdDecoder;
      this.contractDecoder = contractDecoder;
      this.output = output;
      this.options = options;
    }

    public LogOptions Options { get { return this.options; } }

    public MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> MethodCache { get { return this.methodCache; } }

    public IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder { get { return this.mdDecoder; } }

    public IDecodeContracts<Local, Parameter, Method, Field, Type> ContractDecoder { get { return this.contractDecoder; } }

    public IOutput Output { get { return this.output; } }

    //public abstract IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions> BasicMethodDriver(Method method);

  }

  public abstract class AnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult>
    : IBasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>
    where LogOptions : IFrameworkLogOptions
    where Type : IEquatable<Type>
  {
    #region Invariant

    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.mCDrivers != null);
    }
    
    #endregion

    #region Protected

    // public (anyone can register a contract for pretty output, or ask for them)
    protected readonly OutputPrettyCS.ContractsHandlerMgr<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult> mContractsHandlerMgr;
    public OutputPrettyCS.ContractsHandlerMgr<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult> ContractsHandlerManager { get { return this.mContractsHandlerMgr; } }

    protected readonly Dictionary<Type, WeakReference> mCDrivers;
    protected int mMaxClassDriversCount;
    /// class drivers actually stored (ie. non-null in the map, so != mCDrivers.Count)
    protected int mCDriversCount
    {
      get
      {
        return this.mCDrivers.Where(pair => pair.Value != null && pair.Value.IsAlive).Count();
      }
    }
    protected IBasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions> basicDriver;
    #endregion

    protected AnalysisDriver(
      IBasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions> basicDriver
    )
    {
      this.basicDriver = basicDriver;
      this.mContractsHandlerMgr = new OutputPrettyCS.ContractsHandlerMgr<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult>(this, this.Output);
      this.mCDrivers = new Dictionary<Type, WeakReference>();
      this.mMaxClassDriversCount = 0;
    }

    public void InstallPostconditions(List<BoxedExpression.AssertExpression> inferredPostconditions, Method method)
    {
      Contract.Requires(inferredPostconditions != null);

      if (inferredPostconditions.Count == 0) return;
      //this.Output.WriteLine("Trying to install {0} inferred postconditions.", inferredPostconditions.Count);
      // Ordering by ToString should avoid in most of the cases the non-determinism problem we have 
      // when the order of inferred post conditions changes, causing the hash to change in the caching.
      var ordered = inferredPostconditions.OrderBy(post => post.ToString()).ToList();
      var seq = BoxedExpression.Sequence(ordered.Cast<BoxedExpression>());
      BoxedExpression.PC postStart = new BoxedExpression.PC(seq, 0);
      bool result = this.MethodCache.AddPostCondition(method, postStart, ClousotExpressionCodeProvider<Local, Parameter, Method, Field, Type>.Decoder);
      if (result)
      {
        foreach (var post in ordered)
        {
          this.mContractsHandlerMgr.RegisterMethodPostCondition(method, post.Condition);
        }
      }

    }

    public void InstallPreconditions(List<BoxedExpression.AssumeExpression> inferredPreconditions, Method method)
    {
      Contract.Requires(inferredPreconditions != null);

      if (inferredPreconditions.Count == 0) return;
      //this.Output.WriteLine("Trying to install {0} inferred preconditions.", inferredPreconditions.Count);

      // Ordering by ToString should avoid in most of the cases the non-determinism problem we have 
      // when the order of inferred pre conditions changes, causing the hash to change in the caching.
      var ordered = inferredPreconditions.OrderBy(pre => pre.ToString()).ToList();
      var seq = BoxedExpression.Sequence(ordered.Cast<BoxedExpression>());
      BoxedExpression.PC preStart = new BoxedExpression.PC(seq, 0);
      if (this.MethodCache.AddPreCondition(method, preStart, ClousotExpressionCodeProvider<Local, Parameter, Method, Field, Type>.Decoder))
      {
        foreach (var pre in ordered)
        {
          this.mContractsHandlerMgr.RegisterMethodPreCondition(method, pre.Condition);
        }
      }
    }

    public void InstallObjectInvariants(List<BoxedExpression> objectInvariants, Type type)
    {
      Contract.Requires(objectInvariants != null);

      if (objectInvariants.Count == 0)
      {
        return;
      }
      // Ordering by ToString should avoid in most of the cases the non-determinism problem we have 
      // when the order of inferred invariants, causing the hash to change in the caching.
      var ordered = objectInvariants.OrderBy(inv => inv.ToString()).ToList();


      // TODO: COMMENT
      Console.WriteLine("Installing the object invariants");
      foreach(var e in ordered)
      {
        Console.WriteLine("   {0}", e);
      }
      // END TODO

      var seq = BoxedExpression.Sequence(ordered);
      var pc = new BoxedExpression.PC(seq, 0);
      if (this.MethodCache.AddInvariant(type, pc, ClousotExpressionCodeProvider<Local, Parameter, Method, Field, Type>.Decoder))
      {
        foreach (var inv in ordered)
        {
          this.mContractsHandlerMgr.RegisterClassInvariant(type, inv);
        }
      }
    }


    /// <summary>
    /// Responsibility to make sure the assume can be properly decoded lies with the serializer/deserializer.
    /// The SER/DES should have enough detail and checks to make sure a deserialized expression can be decoded without
    /// failing due to stack mismatch or failed type assumptions.
    /// </summary>
    public void InstallCalleeAssumes(ICFG cfg, IEnumerable<Pair<BoxedExpression, Method>> inferredAssumes, Method method)
    {
      Contract.Requires(inferredAssumes != null);

#if false
      if (!inferredAssumes.Any())
        return;
      //this.Output.WriteLine("Trying to install {0} inferred assumes.", inferredAssumes.Count);

      // We lookup by name
      var locs = this.MetaDataDecoder.Locals(method).Enumerate().Select(local => this.MetaDataDecoder.Name(local));
      var paramz = this.MetaDataDecoder.Parameters(method).Enumerate().Select(parameter => this.MetaDataDecoder.Name(parameter));
      var fields = this.MetaDataDecoder.Fields(this.MetaDataDecoder.DeclaringType(method)).Select(m => this.MetaDataDecoder.Name(method));
#endif
      // TODO: can the name / expr matching get mixed up during deserialization?
      foreach (var inferredAssume in inferredAssumes)
      {
        var assume = inferredAssume.One;
        //this.Output.WriteLine("Installing assume " + assume);
#if false
        #region Ensure that all var's in the assume expression still exist in the new version of the function

        // Check that the variables appearing in the assume to install exists in the scope.
        // We use the name of the variables to make sure they are the same variable
        var foundAllVariables = assume.Variables().TrueForAll(v =>
          {
            var vToString = v.ToString();

            // check for var in locs
            var found = locs.Any(loc => vToString == loc);

            // check for var in params
            found = found || paramz.Any(param => vToString == param);
            //string paramStr = this.MetaDataDecoder.ParameterType(paramz[j]) + " " + v.ToString(); // create str with same format as parameter string; hacky, but needed

            // check for var in fields
            found = found || fields.Any(fName =>
            {
              return vToString == fName // "normal" case
                  || vToString == String.Format("this.{0}", fName); // case where field prefixed with "this"
            });

            return found;
          });
        if (!foundAllVariables)
        {
          continue;
        }
        #endregion
        // found all locals, going to try to install this assume
#endif
        var dummyAPC = cfg.Entry; // TODO: do something better here! we don't know the APC yet, so we just pick some legitimate APC to avoid null ptr deref when looking up src context
        Provenance provenance = null;
        var be = new BoxedExpression.AssumeExpression(assume, "assume", dummyAPC, provenance, assume.ToString<Type>(type => OutputPrettyCS.TypeHelper.TypeFullName(this.MetaDataDecoder, type)));
        var pc = new BoxedExpression.PC(be, 0);

        var calleeName = inferredAssume.Two;

        this.MethodCache.AddCalleeAssumeAsPostCondition(cfg, method, calleeName, pc, ClousotExpressionCodeProvider<Local, Parameter, Method, Field, Type>.Decoder);   
      }
    }

    public void InstallEntryAssumes(ICFG cfg, IEnumerable<Pair<BoxedExpression, Method>> inferredAssumes, Method method)
    {
      Contract.Requires(inferredAssumes != null);

      foreach (var inferredAssume in inferredAssumes)
      {
        var assume = inferredAssume.One;
        var dummyAPC = cfg.Entry; // TODO: do something better here! we don't know the APC yet, so we just pick some legitimate APC to avoid null ptr deref when looking up src context
        Provenance provenance = null;
        var be = new BoxedExpression.AssumeExpression(assume, "assume", dummyAPC, provenance, assume.ToString<Type>(type => OutputPrettyCS.TypeHelper.TypeFullName(this.MetaDataDecoder, type)));
        var pc = new BoxedExpression.PC(be, 0);

        this.MethodCache.AddEntryAssume(cfg, method, pc, ClousotExpressionCodeProvider<Local, Parameter, Method, Field, Type>.Decoder);
      }
    }


    public void RemoveInferredPrecondition(Method method)
    {
      this.MethodCache.RemovePreCondition(method);
    }

    public void RemoveContractsFor(Method method)
    {
      this.MethodCache.RemoveContractsFor(method);
    }

    public abstract IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> MethodDriver(
      Method method,
      IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult> classDriver,
      bool removeInferredPrecondition = false);

    #region Class driver
    public IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult> ClassDriver(Type type, bool inferInvariantsJustForReadonlyFields = true)
    {
      WeakReference reference;
      CDriver result;
      if (mCDrivers.TryGetValue(type, out reference) && reference != null && reference.IsAlive)
      {
        result = reference.Target as CDriver;
        return result;
      }
      else
      {
        if (!inferInvariantsJustForReadonlyFields || this.MetaDataDecoder.Fields(type).Where(this.MetaDataDecoder.IsReadonly).Any())
        {

          var cd = new CDriver(this, MetaDataDecoder, type);
          mCDrivers[type] = new WeakReference(cd);
          this.mMaxClassDriversCount = Math.Max(mCDriversCount, this.mMaxClassDriversCount);

#if DEBUG && false
          var old = Console.ForegroundColor; 
          Console.ForegroundColor = ConsoleColor.Red; 
          Console.WriteLine("-- ADD CLASS DRIVER -- Max Class Drivers Count is: {0} ; class drivers count is: {1}", this.mMaxClassDriversCount, mCDriversCount); // DEBUG
          Console.ForegroundColor = old; 
#endif

          return cd;
        }
        else
        {
#if DEBUG && false
          this.Output.WriteLine("The type {0} does not contain any readonly field. As a consequence we do not need a class driver", MetaDataDecoder.Name(type));
#endif
          return null;
        }
      }
    }

    public int MaxClassDriversCount { get { return mMaxClassDriversCount; } }

    public void RemoveClassDriver(IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult> cdriver)
    {
      Contract.Requires(cdriver != null);

      mCDrivers[cdriver.ClassType] = null; // don't need the class driver anymore, but don't need to actually rebuild it anymore either

#if DEBUG && false
      ConsoleColor old = Console.ForegroundColor; // DEBUG
      Console.ForegroundColor = ConsoleColor.Yellow; // DEBUG
      Console.WriteLine("-- REMOVE CLASS DRIVER -- Max Class Drivers Count is: {0} ; class drivers count is: {1}", this.mMaxClassDriversCount, mCDriversCount); // DEBUG
      Console.ForegroundColor = old; // DEBUG
#endif
    }
    #endregion

    ///////////////////////////////////////////
    // CDriver
    ///////////////////////////////////////////
    protected class CDriver : IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult>
    {
      #region Privates
      public IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder { get; private set; }

      public Type ClassType { get; private set; }
      public LogOptions Options { get; private set; }

      private List<BoxedExpression> mInvariants;
#if false
      private List<WeakReference> weakConstructors; // weak references to Method
#endif
      public List<Method> Constructors { get; private set; }
#if false
      private readonly Dictionary<Method, MethodInfo> mConstructorsInfo;
#endif
      private int mMethodsCount;
      private int mAnalyzedMethodsCount;
      /// <summary>
      /// Stores all the information about the analysis of each the class method
      /// </summary>
      private readonly Dictionary<Method, MethodAnalysisStatus> mMass;

      public IConstructorsAnalysisStatus<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult> ConstructorsStatus { get; private set; }
      #endregion

      public CDriver(AnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult> parent, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, Type type)
      {
        Contract.Requires(mdDecoder != null);

        this.ParentDriver = parent;
        this.MetaDataDecoder = mdDecoder;
        this.mInvariants = new List<BoxedExpression>();
        this.ClassType = type;
        
        var nonStaticMethods = mdDecoder.Methods(type).Where(m => !mdDecoder.IsStatic(m));
        this.mMethodsCount = nonStaticMethods.Count();
        this.Constructors = nonStaticMethods.Where(mdDecoder.IsConstructor).ToList();
        var constructorsCount = this.Constructors.Count();

        this.mAnalyzedMethodsCount = 0;
#if false
        this.mConstructorsInfo = new Dictionary<Method, MethodInfo>(capacity: constructorsCount);
#endif
        this.ConstructorsStatus = new ConstructorsAnalysisStatus(type, constructorsCount);
        this.mMass = new Dictionary<Method, AnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult>.MethodAnalysisStatus>();
      }

      public AnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult> ParentDriver { get; private set; }

#if false
      public List<Method> Constructors
      {
        get
        {
          List<Method> result = new List<Method>();
          if (this.weakConstructors != null)
          {
            foreach (var wm in this.weakConstructors)
            {
              var m = wm.Target;
              if (m == null)
                break;
              result.Add((Method)m);
            }
            if (result.Count == this.weakConstructors.Count)
              return result;
            this.weakConstructors.Clear();
          }
          else
          {
            this.weakConstructors = new List<WeakReference>();
          }
          result.Clear();
          foreach (var m in this.MetaDataDecoder.Methods(this.ClassType))
            if (this.MetaDataDecoder.IsConstructor(m) && !this.MetaDataDecoder.IsStatic(m))
            {
              result.Add(m);
              this.weakConstructors.Add(new WeakReference(m));
            }
          return result;
        }
      }
#endif

      public bool IsExistingInvariant(string invariantString)
      {
        foreach (var be in mInvariants)
        {
          if (be.ToString() == invariantString)
            return true;
        }
        return false;
      }

      public bool AddInvariant(BoxedExpression boxedExpression)
      {
        if (CanAddInvariants() && !IsExistingInvariant(boxedExpression.ToString()))
        {
          mInvariants.Add(boxedExpression);
          return true;
        }
        return false;
      }

      public bool CanAddInvariants()
      {
        return MetaDataDecoder.IsClass(ClassType);
      }

      public bool InstallInvariantsAsConstructorPostconditions(Parameter methodThis, IEnumerable<Pair<BoxedExpression, Provenance>> boxedExpressions, Method method)
      {
        if (boxedExpressions == null)
          return false;
        var beArray = boxedExpressions.ToArray();
        if (beArray.Length == 0)
          return true;
        var result = true;
        foreach (var constructor in this.Constructors)
        {
          var ctorThis = this.MetaDataDecoder.This(constructor);
          var pc = this.NormalExitPC(constructor);
          // We need to replace any 'this' occuring in the expression by the 'this' of the constructor
          var postConditions = beArray
            .Select(be => {
              var exp = this.SubstituteThis(be.One, methodThis, ctorThis);
              var definingMethod = this.MetaDataDecoder.Name(method);
              return new BoxedExpression.AssertExpression(exp, "invariant", pc, be.Two ?? new List<ProofObligation>() { new FakeProofObligationForAssertionFromTheCache(be.One, definingMethod, null) },
                exp.ToString<Type>(type => OutputPrettyCS.TypeHelper.TypeFullName(this.MetaDataDecoder, type)));
            }) // We use  trick or returning a non-empty proof obligation to signal that the assertion has been inferred, even if we lost the context 
            .ToList();
          //          var postConditions = beArray.Select(be => new BoxedExpression.AssertExpression(this.SubstituteThis(be.One, methodThis, ctorThis), "ensures", pc, be.Two)).ToList();
          this.ParentDriver.InstallPostconditions(postConditions, constructor);
        }
        return result;
      }

      [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant", Justification="Bug in Clousot")]
      private PathElement[] SubstituteThis(PathElement[] path, Parameter from, Parameter to)
      {
        PathElement[] result = null;
        for (int i = 0; i < path.Length; i++)
        {
          var pathElement = path[i];

          Contract.Assume(pathElement != null);

          var subst = pathElement.SubstituteElement(from, to);
          if (subst != pathElement) {
            if (result == null)
              path.CopyTo(result = new PathElement[path.Length], 0);
            result[i] = subst;
          }
        }
        return result ?? path;
      }

      private BoxedExpression SubstituteThis(BoxedExpression be, Parameter from, Parameter to)
      {
        return be.Substitute<object>((_, variableExpression) =>
          {
            var accessPath = SubstituteThis(variableExpression.AccessPath, from, to);
            if (accessPath == variableExpression.AccessPath)
              return variableExpression;
            BoxedExpression writableBytes;
            object type;
            if (!variableExpression.TryGetType(out type))
              type = null;
            if (variableExpression.TryGetAssociatedInfo(AssociatedInfo.WritableBytes, out writableBytes))
              return new BoxedExpression.VariableExpression(variableExpression.UnderlyingVariable, accessPath, writableBytes, type);
            return new BoxedExpression.VariableExpression(variableExpression.UnderlyingVariable, accessPath, type);
          });
      }

      public void MethodHasBeenAnalyzed(
        string analyze_name,
        MethodResult result,
        Method method)
      {
        MethodAnalysisStatus mas;
        if (!mMass.TryGetValue(method, out mas))
        {
          mas = new MethodAnalysisStatus(method);
          mMass.Add(method, mas);
        }
        else
        {
          Contract.Assume(mas != null);
        }
        mas.AddMethodResult(analyze_name, result);
      }

      public void MethodHasBeenFullyAnalyzed(Method method)
      {
        var isStatic = this.MetaDataDecoder.IsStatic(method);
        var isConstructor = this.MetaDataDecoder.IsConstructor(method) && !isStatic;

        if (!isStatic)
          this.mAnalyzedMethodsCount++;

        if (!mMass.ContainsKey(method))
          return; // don't have any info about this method! shouldn't happen

        if (isConstructor)
          ConstructorsStatus.ConstructorAnalyzed(mMass[method]);
      }

#if false
      class MethodInfo
      {
        public readonly APC NormalExit;
        public readonly PathElement This;

        public MethodInfo(IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver)
        {
          Contract.Requires(mdriver != null);

          this.NormalExit = mdriver.CFG.NormalExit;

          var entryAfterRequires = mdriver.CFG.EntryAfterRequires;
          var parameterThis = mdriver.MetaDataDecoder.This(mdriver.CurrentMethod);
          Variable symbolicThis;
          if (mdriver.Context.ValueContext.TryParameterValue(entryAfterRequires, parameterThis, out symbolicThis))
          {
            var pathThis = mdriver.Context.ValueContext.AccessPathList(mdriver.CFG.EntryAfterRequires, symbolicThis, false, false);
            if (pathThis != null)
              this.This = pathThis.Head;
          }
        }
      }
#endif
      private APC NormalExitPC(Method method)
      {
          var cfg = this.ParentDriver.MethodCache.GetCFG(method);
          return cfg.NormalExit;
      }

#if false
      private MethodInfo GetConstructorInfo(Method method)
      {
        MethodInfo result;
        if (this.mConstructorsInfo.TryGetValue(method, out result))
          return result;
        var mdriver = this.ParentDriver.MethodDriver(method, this);
        mdriver.RunHeapAndExpressionAnalyses();
        result = new MethodInfo(mdriver);
        this.mConstructorsInfo.Add(method, result);
        return result;
      }
#endif

      public int PendingConstructors { get { return ConstructorsStatus.Pending; } }

      public bool IsClassFullyAnalyzed()
      {
        return this.mAnalyzedMethodsCount == this.mMethodsCount;
      }
    }

    public class ConstructorsAnalysisStatus
      : IConstructorsAnalysisStatus<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult>
    {
      [ContractInvariantMethod]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.AnalysesResults != null);
      }


      public ConstructorsAnalysisStatus(Type type, int totalnb)
      {
        this.ClassType = type;
        Pending = totalnb;
        TotalNb = totalnb;
        AnalysesResults = new Dictionary<Method, IMethodAnalysisStatus<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult>>();
      }

      /// <summary>
      /// Decrease the count of pending constructors to analyze by one
      /// </summary>
      /// <returns>The new pending count (maybe 0)</returns>
      private int DecreasePendingCount()
      {
        return --Pending;
      }

      public void ConstructorAnalyzed(IMethodAnalysisStatus<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult> mas)
      {
        if (!AnalysesResults.ContainsKey(mas.MethodObj)) // protect in case we analyze twice the same method
        {
          AnalysesResults.Add(mas.MethodObj, mas);
          DecreasePendingCount(); // a constructor was analyzed
        }
      }

      public Type ClassType { get; private set; }
      public int Pending { get; private set; } /// number of constructors left to analyze before starting the ObjectInvariant analysis
      public int TotalNb { get; private set; } /// total number of constructors for this type
      public Dictionary<Method, IMethodAnalysisStatus<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult>> AnalysesResults { get; private set; }
    }


    public class MethodAnalysisStatus
      : IMethodAnalysisStatus<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult>
    {

      #region Object invariant
      
      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.Results != null);
      }
      #endregion

      public MethodAnalysisStatus(
        Method method)
      {
        this.MethodObj = method;
        Results = new Dictionary<string, MethodResult>();
      }

      public void AddMethodResult(string analyze_name, MethodResult result)
      {
        if (!Results.ContainsKey(analyze_name))
          Results.Add(analyze_name, result);
      }

      public Method MethodObj { get; private set; }
      public Dictionary<string, MethodResult> Results { get; private set; }
    }


    #region delegate IBasicAnalysisDriver<Local,Parameter,Method,Field,Property,Event,Type,Attribute,Assembly,LogOptions> Members

    public LogOptions Options
    {
      get { return basicDriver.Options; }
    }

    public MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> MethodCache
    {
      get 
      {
        return basicDriver.MethodCache; 
      }
    }

    public IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder
    {
      get {
        return basicDriver.MetaDataDecoder; 
      }
    }

    public IDecodeContracts<Local, Parameter, Method, Field, Type> ContractDecoder
    {
      get { return basicDriver.ContractDecoder; }
    }

    public IOutput Output { get { return basicDriver.Output; } }

    #endregion
  }
}

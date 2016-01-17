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

// This file contains the abstract class with some stuff which factors out the other value analyses
// In general, one just believes what in this class, and focuses himself in writing his own analysis

// #define TRACE_AD_PERFORMANCES
//#define TRACE_WIDENING
// #define TRACE_JOIN

using System;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains.Numerical;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using System.Linq;


namespace Microsoft.Research.CodeAnalysis
{
  /// <summary>
  /// Used to transmit options to analyses derived from GenericBounds analysis
  /// </summary>
  public interface IValueAnalysisOptions 
    : IAIOptions
  {
    ILogOptions LogOptions { get; }
    bool NoProofObligations { get; }
  }

  public static partial class AnalysisWrapper
  {
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      /// <summary>
      /// The entry point for running a bounds analysis
      /// </summary>
      internal static IMethodResult<Variable> RunTheAnalysis<AbstractDomain, Options>
      (
        string methodName,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
        GenericValueAnalysis<AbstractDomain, Options> analysis, DFAController controller
      )
        where AbstractDomain : IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>
        where Options : IValueAnalysisOptions
      {
        var closure = driver.HybridLayer.CreateForward<AbstractDomain>(
          analysis, new DFAOptions { Trace = driver.Options.TraceDFA, Timeout = driver.Options.Timeout, SymbolicTimeout = driver.Options.SymbolicTimeout, EnforceFairJoin = driver.Options.EnforceFairJoin, IterationsBeforeWidening = driver.Options.IterationsBeforeWidening, TraceTimePerInstruction = driver.Options.TraceTimings, TraceMemoryPerInstruction = driver.Options.TraceMemoryConsumption }, controller
          );

        closure(analysis.GetTopValue());   // Do the analysis 

        analysis.ClearCaches();

        return analysis;
      }

      /// <summary>
      /// 
      /// The generic value analysis
      /// </summary>
      /// <typeparam name="AbstractDomain">The abstract domain, which must implement <code>IAssignInParallel</code></typeparam>
      [ContractClass(typeof(GenericValueAnalysisContracts<,>))]
      public abstract class GenericValueAnalysis<AbstractDomain, AnalysisOptions> :
        MSILVisitor<APC, Local, Parameter, Method, Field, Type, Variable, Variable, AbstractDomain, AbstractDomain>,
        IAbstractAnalysis<Local, Parameter, Method, Field, Property, Type, Expression, Attribute, Assembly, AbstractDomain, Variable>,
        IMethodResult<Variable>
        where AbstractDomain : IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>
        where AnalysisOptions : IValueAnalysisOptions
      {
        #region Some statistics    

#if TRACE_AD_PERFORMANCES
        [ThreadStatic]
        protected static int MaxSize = -1;
#endif

        #endregion

        #region State

        readonly private string methodName;
        readonly protected Predicate<APC> cachePCs;
        readonly private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> methodDriver;       
        readonly private Set<APC> PCAlreadyVisited;
        readonly private AnalysisOptions options;
        readonly protected ConstantEvaluator constantEval;

        private IExpressionEncoder<BoxedVariable<Variable>, BoxedExpression> encoder;
        private BoxedExpressionDecoder<Type, Variable, Expression> decoderForExpressions;
        private ExpressionManagerWithEncoder<BoxedVariable<Variable>, BoxedExpression> expressionManager;

        protected GenericValueAnalysisCacher cacheManager { private set; get; } 

        protected IFixpointInfo<APC, AbstractDomain> fixpointInfo;
        protected List<IFixpointInfo<APC, AbstractDomain>> fixpointInfo_List;

        // Mic: public to be accessible from AbstractOperationsImplementation
        public bool PreStateLookup(APC label, out AbstractDomain ifFound) { return this.fixpointInfo.PreState(label, out ifFound); }

        // Mic: keep track of what analysis this result comes from
        public IMethodAnalysis MethodAnalysis { get; set; }

        #endregion

        #region Invariant

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
          Contract.Invariant(this.options != null);
          Contract.Invariant(this.cacheManager != null);
          Contract.Invariant(this.constantEval != null);
        }
        
        #endregion

        #region Constructor

        protected GenericValueAnalysis
        (
          string methodName, 
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          AnalysisOptions options,
          Predicate<APC> cachePCs
          )
        {
          Contract.Requires(options != null);
          Contract.Requires(options.LogOptions != null);

          Contract.Ensures(this.options != null);
          Contract.Ensures(object.Equals(this.options, options));
          Contract.Ensures(this.cacheManager != null);

          ThresholdDB.Reset();

          // F: Just for tracing during debugging
          BoxedVariable<Variable>.ResetFreshVariableCounter();         

          this.methodName = methodName;
          this.methodDriver = mdriver;
          this.cachePCs = cachePCs;
          
          this.PCAlreadyVisited = new Set<APC>();
          this.options = options;

          this.fixpointInfo_List = null;

          this.cacheManager = new GenericValueAnalysisCacher(options.LogOptions.ExpCaching, this.Context, this.Decoder.Outdecoder);
          this.constantEval = new ConstantEvaluator(this.Context, this.DecoderForMetaData);

          PrintAbstractDomainPerformanceStatistics();
        }
        #endregion

        #region To be overridden by clients!

        /// <summary>
        /// Invoked to get postconditions that are specific to the analysis
        /// </summary>
        abstract public bool SuggestAnalysisSpecificPostconditions(
          ContractInferenceManager inferenceManager, 
          IFixpointInfo<APC, AbstractDomain> fixpointInfo, 
          List<BoxedExpression> postconditions);

        /// <summary>
        /// Invoked to get postconditions for a parameter p, and which are specific to the analysis
        /// </summary>
        abstract public bool TrySuggestPostconditionForOutParameters(
          IFixpointInfo<APC, AbstractDomain> fixpointInfo, List<BoxedExpression> postconditions, Variable p, FList<PathElement> path);

        #endregion

        #region Getters

        protected string MethodName
        {
          get
          {
            return this.methodName;
          }
        }

        protected IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> DecoderForMetaData
        {
          get
          {
            Contract.Ensures(Contract.Result<IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>>() != null);

            Contract.Assume(this.methodDriver.MetaDataDecoder != null);

            return this.methodDriver.MetaDataDecoder; 
          }
        }

        protected IDecodeContracts<Local, Parameter, Method, Field, Type> DecoderForContracts
        {
          get
          {
            Contract.Ensures(Contract.Result<IDecodeContracts<Local, Parameter, Method, Field, Type>>() != null);

            return this.methodDriver.StackLayer.ContractDecoder;
          }
        }

        public IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> MethodDriver
        {
          get
          {
            Contract.Ensures(Contract.Result<IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions>>() != null);

            return this.methodDriver;
          }
        }

        protected IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> Context
        {
          get
          {
            Contract.Ensures(Contract.Result<IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable>>() != null);

            return this.MethodDriver.Context;
          }
        }

        protected IExpressionEncoder<BoxedVariable<Variable>, BoxedExpression> Encoder
        {
          get
          {
            Contract.Ensures(Contract.Result<IExpressionEncoder<BoxedVariable<Variable>, BoxedExpression>>() != null);

            if (this.encoder == null)
            {
              Contract.Assert(this.DecoderForMetaData != null);
              Contract.Assert(this.Context != null);

              this.encoder = BoxedExpressionEncoder<Variable>.Encoder(this.DecoderForMetaData, this.Context);
            }

            return this.encoder;
          }
        }

        protected BoxedExpressionDecoder<Type, Variable, Expression> Decoder
        {
          get
          {
            Contract.Ensures(Contract.Result<BoxedExpressionDecoder<Type, Variable, Expression>>() != null);

            if (this.decoderForExpressions == null)
            {
              this.decoderForExpressions = BoxedExpressionDecoder<Variable>.Decoder(new ValueExpDecoder(this.Context, this.DecoderForMetaData), this.TypeFor);
            }

            return this.decoderForExpressions;
          }
        }

        protected ExpressionManagerWithEncoder<BoxedVariable<Variable>, BoxedExpression> ExpressionManager
        {
          get
          {
            Contract.Ensures(Contract.Result<ExpressionManagerWithEncoder<BoxedVariable<Variable>, BoxedExpression>>() != null);

            if (this.expressionManager == null)
            {
              this.expressionManager = new ExpressionManagerWithEncoder<BoxedVariable<Variable>, BoxedExpression>(DFARoot.TimeOut, this.Decoder, this.Encoder, this.Log);
            }

            return this.expressionManager;
          }
        }


        protected AnalysisOptions Options 
        { 
          get 
          {
            Contract.Ensures(Contract.Result<AnalysisOptions>() != null);

            return this.options; 
          } 
        }

        #endregion

        #region IValueAnalysis implementation

        public virtual IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, AbstractDomain, AbstractDomain>
          Visitor()
        {
          return this;
        }

        #region Join / Widening
        /// <summary>
        /// The generic join/widening
        /// </summary>

        virtual public AbstractDomain Join(Microsoft.Research.DataStructures.Pair<APC, APC> edge, AbstractDomain newState, AbstractDomain prevState, out bool changed, bool widen)
        {
          UpdateMaxSize(newState);
          UpdateMaxSize(prevState);

          AbstractDomain joinedState;

#if TRACE_WIDENING || TRACE_JOIN
          Console.WriteLine("{0}:{1}->{2}", widen ? "widening" : "join", edge.One, edge.Two);
#endif
          if (!widen)
          {
            joinedState = HelperForJoin(newState, prevState, edge);
            changed = true;
          }
          else
          {
#if TRACE_WIDENING
            Console.WriteLine("new State : {0}", newState);
            Console.WriteLine("prev State : {0}", prevState);
#endif
      
            joinedState = HelperForWidening(newState, prevState, edge);

#if TRACE_WIDENING
            Console.WriteLine("prev State (after widening): {0}", prevState);
            Console.WriteLine("result : {0}", joinedState);
#endif

            changed = !joinedState.LessEqual(prevState);

#if TRACE_WIDENING || TRACE_JOIN
            Console.WriteLine("changed? {0}", changed);
#endif
          }

          UpdateMaxSize(joinedState);

          return joinedState;
        }

        internal virtual AbstractDomain HelperForJoin(AbstractDomain newState, AbstractDomain prevState, Microsoft.Research.DataStructures.Pair<APC, APC> edge)
        {
          Contract.Ensures(Contract.Result<AbstractDomain>() != null);

          return (AbstractDomain)newState.Join(prevState);
        }

        internal virtual AbstractDomain HelperForWidening(AbstractDomain newState, AbstractDomain prevState, Microsoft.Research.DataStructures.Pair<APC, APC> edge)
        {
          Contract.Ensures(Contract.Result<AbstractDomain>() != null);

          return (AbstractDomain)newState.Widening(prevState);
        }
        
        #endregion

        #region Edge conversion

#if DEBUG
        [ThreadStatic]
        private static int countPA = 0;
#endif

        virtual public AbstractDomain EdgeConversion(APC from, APC to, bool isJoin, IFunctionalMap<Variable, FList<Variable>> sourceTargetMap, AbstractDomain state)
        {
          if (sourceTargetMap == null)
          {
            return state;
          }

          UpdateMaxSize(state);

#if DEBUG
          Log("Parallel Assign count {0}", () => { countPA++; return countPA.ToString(); });
#endif

          //RenameTypes(sourceTargetMap);

          // F: Uncommenting those lines causes a regression: it seems that even if it is the identity, we still can get some new constant
          // the reason for that seems to be that the e-graph is doing some clever reasoning e.g. "a !=0 && !(a) == b ==> b == 0" which the abstract domains do not
          //if (sourceTargetMap.IsIdentity())
          //{
          //  return state;
          //}

          var edge = new Pair<APC, APC>(from, to);          

          var refinedMap = this.cacheManager.GetRefinedMap(from, to, sourceTargetMap, this.MethodDriver.AdditionalSyntacticInformation.Renamings, options.LogOptions.MaxVarsInSingleRenaming);

#if DEBUG
          if (refinedMap.Count < sourceTargetMap.Count)
          {
            Console.WriteLine("[AssignInParallel] We removed some renaming because it contained only dead variables!");
          }
#endif

          var result = HelperForAssignInParallel(state, edge, refinedMap,
            delegate(BoxedVariable<Variable> bv)
            {
              Variable v;
              if (bv.TryUnpackVariable(out v))
              {
                return this.ToBoxedExpression(edge.One, v);
              }
              else
              {
                // Should be unreached ...
                return this.encoder.VariableFor(bv);
              }
            }
          );

          UpdateMaxSize(state);

          return result;
        }


        public virtual AbstractDomain
          HelperForAssignInParallel(AbstractDomain state, 
          Pair<APC, APC> edge, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap, Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          Contract.Requires(refinedMap != null);
          Contract.Requires(convert != null);

          state.AssignInParallel(refinedMap, convert);

          return state;
        }

        #endregion

        public override AbstractDomain Assume(APC pc, string tag, Variable source, object provenance, AbstractDomain data)
        {
          Func<BoxedExpression, BoxedExpression> MakeUnsignedConstExplicit = (BoxedExpression exp) =>ExpandUnsignedConstants(pc, exp, false, this.DecoderForMetaData.System_Void, 16);
          var refinedExp = this.ToBoxedExpression(pc, source, MakeUnsignedConstExplicit);

          // The expression is too depth
          if (refinedExp == null)
          {
            return data;
          }

          AbstractDomain result;
          if (tag != "false")
          {
            bool value;
            if (refinedExp.IsTrivialCondition(out value))
            {
              return value ? data : (AbstractDomain)data.Bottom;
            }
          }
          else
          {
            if (IsConstantNonZeroExpression(refinedExp))
            {
              return (AbstractDomain)data.Bottom;
            }
          }

          if (this.options.UseMorePreciseWidening)
          {
            List<int> thresholds;
            if (!this.PCAlreadyVisited.Contains(pc)
              && AbstractDomainsHelper.TryToGetAThreshold(refinedExp, out thresholds, this.decoderForExpressions))
            {
              this.PCAlreadyVisited.Add(pc);  // Next time we hit this PC, we will avoid looking for thresholds
              ThresholdDB.Add(thresholds);
            }
          }

          switch (tag)
          {
            case "true":        // for true branches
            case "requires":    // for preconditions
            case "assume":      // for the assume statements
            case "ensures":     // for the postcondition of a method call
            case "invariant":   // for class invariants
              result = (AbstractDomain)data.TestTrue(refinedExp);
              result = FixImmutablePseudoFields(pc, result, refinedExp);
              break;

            case "false":       // for false branches
              result = (AbstractDomain)data.TestFalse(refinedExp);
              break;

            default:
              result = data;
              break;
          }


          // Handling of booleans, we assume source == 1, when source is a Boolean value
          if (tag != "false")
          {
            Log("State before assuming {0} == 1:\n{1}", source.ToString, result.ToString);

            var tryType = this.Context.ValueContext.GetType(this.Context.MethodContext.CFG.Post(pc), source);
            if (tryType.IsNormal && tryType.Value.Equals(this.DecoderForMetaData.System_Boolean))
            {
              var conditionToOne = BoxedExpression.Binary(
                BinaryOperator.Ceq, refinedExp, BoxedExpression.Const(1, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));
              result = (AbstractDomain)result.TestTrue(conditionToOne);
            }

            Log("State after the assumption:\n{0}", result.ToString);
          }

          // Handling of disjunctions
          if (tag != "false" && refinedExp.IsVariable)
          {
            Log("Trying to refine the variable {0} to one containing logical connectives", refinedExp.UnderlyingVariable.ToString);

            BoxedExpression refinedExpWithConnectives;
            if (TryToBoxedExpressionWithBooleanConnectives(pc, tag, source, false,
              data as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>, out refinedExpWithConnectives))
            {
              Log("Succeeded. Got {0}", refinedExpWithConnectives.ToString); 
              result = (AbstractDomain)result.TestTrue(refinedExpWithConnectives);
            }
          }
          if (this.methodDriver.DisjunctiveExpressionRefiner != null)
          {
            Log("Trying to use modus ponens to push information to the other abstract domains");

            List<BoxedExpression> consequences;

            var convertedExp = BoxedExpression.Convert(this.Context.ExpressionContext.Refine(pc, source), this.methodDriver.ExpressionDecoder, MAXDEPTH: 16);

            if (convertedExp != null) // It can fail if the expression to convert is too deep
            {
              if (tag != "false")
              {
                convertedExp = BoxedExpression.UnaryLogicalNot(convertedExp);
              }

              if (this.methodDriver.DisjunctiveExpressionRefiner.TryApplyModusPonens(this.Context.MethodContext.CFG.Post(pc),
                convertedExp, GetSimpleDecisionProcedure(data), out consequences))
              {
                Log("Discovered {0} new consequences. Pushing them to the numerical domain", consequences.Count.ToString);

                foreach (var consequence in consequences)
                {
                  Contract.Assume(consequence != null, "Weakness in handling of enumerators");
                  result = (AbstractDomain)result.TestTrue(consequence);
                }
              }
            }
          }

          UpdateMaxSize(result);

          return result;
        }

        public override AbstractDomain Assert(APC pc, string tag, Variable condition, object provenance, AbstractDomain data)
        {
          var refinedExp = this.ToBoxedExpression(pc, condition);

#if DEBUG
          var savedPreState = (AbstractDomain) data.Clone();
#endif

          bool triviality;
          if (refinedExp.IsTrivialCondition(out triviality))
          {
            return triviality? data: (AbstractDomain)data.Bottom;
          }
          else
          {
            data = (AbstractDomain)data.TestTrue(refinedExp);
          }

          // Hack for handling booleans
          var tryType = this.Context.ValueContext.GetType(this.Context.MethodContext.CFG.Post(pc), condition);
          if (tryType.IsNormal && tryType.Value.Equals(this.DecoderForMetaData.System_Boolean))
          {
            var conditionToOne = BoxedExpression.Binary(
              BinaryOperator.Ceq, refinedExp, BoxedExpression.Const(1,this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));
            data = (AbstractDomain) data.TestTrue(conditionToOne);
          }

          return data;
        }

        public override AbstractDomain EndOld(APC pc, APC matchingBegin, Type type, Variable dest, Variable source, AbstractDomain data)
        {
          data = (AbstractDomain) data.TestTrue(
            this.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Equal,
                        this.ToBoxedExpression(this.Context.MethodContext.CFG.Post(pc), dest),
                        this.ToBoxedExpression(pc, source)));
          return data;
        }

        public bool IsBottom(APC pc, AbstractDomain state)
        {
          return state.IsBottom;
        }

        public bool IsTop(APC pc, AbstractDomain state)
        {
          return state.IsTop;
        }

        /// <summary>
        /// Default: do nothing, and return just the state
        /// </summary>
        protected override AbstractDomain Default(APC pc, AbstractDomain data)
        {
          return data;
        }

        #region Private methods

        private Predicate<BoxedExpression> GetSimpleDecisionProcedure(AbstractDomain data)
        {
          if (data == null || !(data is INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>))
          {
            return exp => false;
          }

          var numDom = data as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;

          return exp => data.CheckIfHolds(exp).IsTrue();
        }

        /// <summary>
        /// A fix so that if we know that arr1 == arr2, then we infer arr1.Length == arr2.Length
        /// The same for WBs
        /// </summary>
        private AbstractDomain FixImmutablePseudoFields(APC pc, AbstractDomain adomain, BoxedExpression refinedExp)
        {
          BinaryOperator bop;
          BoxedExpression left, right;

          if (refinedExp.IsBinaryExpression(out bop, out left, out right) && bop == BinaryOperator.Ceq)
          {
            APC postPC = this.Context.MethodContext.CFG.Post(pc);

            if (left.UnderlyingVariable is Variable && right.UnderlyingVariable is Variable)
            {
              var leftVar = (Variable)left.UnderlyingVariable;
              var rightVar = (Variable)right.UnderlyingVariable;

              Variable leftLength, rightLength;

              // Array lengths
              if (this.Context.ValueContext.TryGetArrayLength(postPC, leftVar, out leftLength)
                && this.Context.ValueContext.TryGetArrayLength(postPC, rightVar, out rightLength))
              {
                BoxedExpression eq = BoxedExpression.Binary(
                  BinaryOperator.Ceq, this.ToBoxedExpression(postPC, leftLength), this.ToBoxedExpression(postPC, rightLength));

                adomain = (AbstractDomain) adomain.TestTrue(eq);
              }

              // WritableBytes
              if (this.Context.ValueContext.TryGetWritableBytes(postPC, leftVar, out leftLength)
                && this.Context.ValueContext.TryGetWritableBytes(postPC, rightVar, out rightLength))
              {
                BoxedExpression eq = BoxedExpression.Binary(
                   BinaryOperator.Ceq, this.ToBoxedExpression(postPC, leftLength), this.ToBoxedExpression(postPC, rightLength));
                adomain = (AbstractDomain)adomain.TestTrue(eq);
              }

            }
          }

          return adomain;
        }


        private bool IsConstantNonZeroExpression(BoxedExpression refinedExp)
        {
          var intv =
            new IntervalEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager).BoundsFor(refinedExp);

          return intv.IsNormal && intv.IsSingleton && intv.LowerBound > 0;
        }

        #endregion

        #region Conditional defined methods for printing statistics
        [Conditional("TRACE_AD_PERFORMANCES")]
        private void UpdateMaxSize(int count)
        {
#if TRACE_AD_PERFORMANCES
          if (count > MaxSize)
          {
            MaxSize = count;

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("New max size for variables {0}", MaxSize);
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
          }
#endif
        }

        [Conditional("TRACE_AD_PERFORMANCES")]
        protected void UpdateMaxSize(AbstractDomain d)
        {
          UpdateMaxSize(d.Variables.Count);
        }

        [Conditional("TRACE_AD_PERFORMANCES")]
        protected void PrintAbstractDomainPerformanceStatistics()
        {
          // For the moment we just print the max number of variables 
#if TRACE_AD_PERFORMANCES
          Console.WriteLine("MaxSize so far {0}", MaxSize);
#endif

          // Add other performance stuff there
        }
        #endregion

        #region DFA-related methods
        public AbstractDomain ImmutableVersion(AbstractDomain state)
        {
          return state;
        }

        virtual public AbstractDomain MutableVersion(AbstractDomain state)
        {
         return (AbstractDomain)state.Clone();
        }

        static protected bool PreState(APC label, IFixpointInfo<APC, AbstractDomain> fixpointInfo, out AbstractDomain ifFound) 
        { 
          return fixpointInfo.PreState(label, out ifFound); 
        }

        public virtual Predicate<APC> CacheStates(IFixpointInfo<APC, AbstractDomain> fixpointInfo)
        {
          this.fixpointInfo = fixpointInfo;
          return this.cachePCs;
        }

        public void Dump(Microsoft.Research.DataStructures.Pair<AbstractDomain, System.IO.TextWriter> stateAndWriter)
        {
          stateAndWriter.Two.WriteLine(stateAndWriter.One.ToString());
        }
        #endregion

        #endregion

        #region IAbstractAnalysis <..> Members
        public abstract AbstractDomain GetTopValue();
        public AbstractDomain GetBottomValue()
        {
          // Acording to Francesco, this is the way to do it.
          return (AbstractDomain)GetTopValue().Bottom;
        }

        public virtual IFactQuery<BoxedExpression, Variable> FactQuery()
        {
          return this.FactQuery(this.fixpointInfo);
        }

        IFactQuery<BoxedExpression, Variable> IMethodResult<Variable>.FactQuery { get { return this.FactQuery(); } }

        public abstract IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC, AbstractDomain> fixpoint);
        #endregion 

        #region IMethodResult<Variable> Members

#if false
        public ProofOutcome ValidateExplicitAssertion(APC pc, Variable value)
        {
          return this.FactQuery.IsTrue(pc, this.ToBoxedExpression(pc, value));
        }

        virtual public void ValidateImplicitAssertions(IFactQuery<BoxedExpression, Variable> facts, IOutputResults output)
        {
          if (!this.Options.NoProofObligations && this.Obligations != null)
          {
            this.Obligations.Validate(output, facts);
          }
        }
#endif

        #endregion

        #region Caching interface
        public void ClearCaches()
        {
          this.cacheManager.ClearCaches();
        }

        public void ShareCachesWith(GenericValueAnalysisCacher otherCacheManager)
        {
          Contract.Requires(otherCacheManager != null);
          Contract.Ensures(this.cacheManager == otherCacheManager);

          this.cacheManager = otherCacheManager;
        }

        #endregion

        #region Simplification of renamings

        #region Privates

        private OnDemandCache<Pair<APC, APC>, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>>> refinedNumericalMaps;

        protected Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> SimplifyRefinedMap(ref Pair<APC, APC> edge, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap)
        {
          if (refinedMap.Count >= Thresholds.Renamings.TooManyRenamings)
          {
            Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> result;

            if (this.refinedNumericalMaps.TryGetValue(edge, out result))
            {
              return result;
            }
            else
            {
              result = new Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>>();

              var vc = this.MethodDriver.Context.ValueContext;
              var md = this.MethodDriver.MetaDataDecoder;

              foreach (var pair in refinedMap)
              {
                Variable frameworkVar;
                if (pair.Key.TryUnpackVariable(out frameworkVar))
                {
                  var t = vc.GetType(edge.One, frameworkVar);
                  if (t.IsNormal)
                  {
                    if (md.IsReferenceType(t.Value))
                    {
                      // Remove reference types
                      continue;
                    }
                  }
                }
                // Worst case: we add it 
                result[pair.Key] = pair.Value;
              }

#if DEBUG && false
              Console.WriteLine("[RENAMING] Found a too big renaming. Original renaming count {0}, New renaming count {1}", refinedMap.Count, result.Count);
#endif
              this.refinedNumericalMaps.Add(edge, result); // Add to the cache

              return result;
            }
          }
          else
          {
            return refinedMap;
          }
        }

        #endregion
        
        #endregion

        #region Conversions to BoxedExpressions

        protected bool TryToBoxedExpressionWithBooleanConnectives(APC pc, string tag, Variable var, bool tryRefineToMethodEntry,
          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> oracle, out BoxedExpression result)
        {
          if (oracle == null)
          {
            result = null;
            return false;
          }

          BoxedExpression exp;
          var tmp = true;
          
          var index = new Pair<APC, Variable>(pc, var);
          if(!this.cacheManager.RefinedDisjunctions.TryGetValue(index, out exp))
          {
            tmp = BooleanExpressionRefiner.TryRefine(pc, var, tryRefineToMethodEntry, this.MethodDriver, out exp);
            if (tmp)
            {
              this.cacheManager.RefinedDisjunctions[index] = exp;
            }
          }

          BinaryOperator bop;
          BoxedExpression dummy1, dummy2;
          if (tmp && exp.IsBinaryExpression(out bop, out dummy1, out dummy2) && (bop == BinaryOperator.LogicalAnd || bop == BinaryOperator.LogicalOr))
          {
            result = BooleanExpressionsSimplificator.Simplify(exp, oracle, this.methodDriver);
            return true;
          }
          else
          {
            result = null;
            return false;
          }
        }

        // Wrapper to make the cache manager transparent
        [Pure]
        protected BoxedExpression ToBoxedExpression(APC pc, Variable var, Func<BoxedExpression, BoxedExpression> extraTransformation = null)
        {
          return this.cacheManager.ToBoxedExpression(pc, var, extraTransformation);
        }

        [Pure]
        protected BoxedExpression ToBoxedExpression(APC pc, BoxedVariable<Variable> bv, Func<BoxedExpression, BoxedExpression> extraTransformation = null)
        {
          Variable v;
          if (bv.TryUnpackVariable(out v))
          {
            return this.ToBoxedExpression(pc, v, extraTransformation);
          }
          else
          {
            return this.encoder.VariableFor(bv);
          }
        }

        [Pure]
        protected BoxedVariable<Variable> ToBoxedVariable(Variable v)
        {
          return new BoxedVariable<Variable>(v);
        }

        [Pure]
        protected BoxedExpression ToBoxedExpressionWithConstantRefinement(APC pc, Variable v)
        {
          Type type;
          object value;

          if (Context.ValueContext.IsConstant(pc, v, out type, out value))
          {
            if (this.DecoderForMetaData.System_Int32.Equals(value))
            {
              return BoxedExpression.Const(value, type, this.DecoderForMetaData);
            }
          }

          return this.ToBoxedExpression(pc, v);
        }

        [Pure]
        protected BoxedExpression ToBoxedExpression(Rational rational)
        {
          return rational.ToExpression(this.Encoder); 
        }

        #endregion

        #region TypeForVars
        protected ExpressionType TypeFor(object exp)
        {
          var be = exp as BoxedExpression;
          if(be != null && be.IsVariable)
          {
            object o;
            if (be.TryGetType(out o) && o is Type)
            {
              Type type = (Type)o;
              if (o.Equals(this.DecoderForMetaData.System_Int8))
              {
                return ExpressionType.Int8;
              }
              if (o.Equals(this.DecoderForMetaData.System_Int16))
              {
                return ExpressionType.Int16;
              }
              if (o.Equals(this.DecoderForMetaData.System_Int32))
              {
                return ExpressionType.Int32;
              }
              if (o.Equals(this.DecoderForMetaData.System_Int64))
              {
                return ExpressionType.Int64;
              }
              if (o.Equals(this.DecoderForMetaData.System_UInt8))
              {
                return ExpressionType.UInt8;
              }
              if (o.Equals(this.DecoderForMetaData.System_Char))
              {
                return ExpressionType.UInt16;              }

              if (o.Equals(this.DecoderForMetaData.System_UInt16))
              {
                return ExpressionType.UInt16;
              }
              if (o.Equals(this.DecoderForMetaData.System_UInt32))
              {
                return ExpressionType.UInt32;
              }
              if (o.Equals(this.DecoderForMetaData.System_UInt64))
              {
                return ExpressionType.UInt64;
              }
              if (o.Equals(this.DecoderForMetaData.System_Single))
              {
                return ExpressionType.Float32;
              }
              if (o.Equals(this.DecoderForMetaData.System_Double))
              {
                return ExpressionType.Float64;
              }

            }
          }

          return ExpressionType.Unknown;
        }
        #endregion

        #region Utils



#if false
        public virtual AnalysisStatistics Statistics()
        {
          var obl = this.Obligations;

          if (obl != null)
          {
            return obl.Statistics;
          }
          else
          {
            return default(AnalysisStatistics);
          }
        }
#endif

        public void Log(string format, params StringClosure[] args)
        {
#if DEBUG
          if (this.options.TraceNumericalAnalysis)
          {
            try
            {
              var strings = new string[args.Length];
              for (var i = 0; i < args.Length; i++)
              {
                strings[i] = args[i]();
              }

              Console.WriteLine(format, strings);
            }
            catch (Exception)
            {
              Console.WriteLine("Exception during logging");
            }
          }
#endif
        }

        protected Variable Strip(APC pc, Variable var)
        {
          Variable result;

          var exp = this.ToBoxedExpression(pc, var);
          if (exp.IsUnary && exp.UnaryOp == UnaryOperator.Conv_i4 && exp.UnaryArgument.TryGetFrameworkVariable(out result))
          {
            return result;
          }

          return var;
        }

        protected bool TryMatchVariableConstant(APC pc, BinaryOperator op, Variable s1, Variable s2,
          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> oracle,
          out Variable var, out int value)
        {
          var s1Exp = this.ToBoxedExpression(pc, s1);
          var s2Exp = this.ToBoxedExpression(pc, s2);

          return TryMatchVariableConstant(pc, op, s1, s2, s1Exp, s2Exp, oracle, out var, out value);
        }

        protected bool TryMatchVariableConstant(APC pc, BinaryOperator op,
          Variable s1, Variable s2, BoxedExpression s1Exp, BoxedExpression s2Exp,
          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> oracle,
          out Variable var, out int value)
        {
          int s1value, s2Value;

          var s1IsConst = this.IsConstantInt32(pc, s1, out s1value);
          var s2IsConst = this.IsConstantInt32(pc, s2, out s2Value);

          if (s1IsConst || s2IsConst)
          {
            // k op var
            if (op != BinaryOperator.Sub    // F: At the moment we do not consider 'k - var' 
              && s1IsConst && !s2IsConst)
            {
              if (s1value != 0)
              {
                var = s2;
                value = (Int32)s1value;

                return true;
              }
              else if (s2Exp.IsBinary)// 0 + s2
              {
                Variable s2Left, s2Right;
                if (s2Exp.BinaryLeft.TryGetFrameworkVariable(out s2Left) && s2Exp.BinaryRight.TryGetFrameworkVariable(out s2Right))
                {
                  return TryMatchVariableConstant(pc,
                    s2Exp.BinaryOp, s2Left, s2Right,
                    s2Exp.BinaryLeft, s2Exp.BinaryRight, oracle, out var, out value);
                }
              }
            }
            if (!s1IsConst && s2IsConst)
            {
              var = s1;
              value = s2Value;

              if (op == BinaryOperator.Sub) // k - v --> k + (-v)
              {
                value = -value;
              }

              return true;
            }

            // F: we do not handle the case where both are singletons, but maybe we should
          }

          var = default(Variable);
          value = default(int);
          return false;
        }

        #endregion

        #region Postcondition suggestion

        /// <summary>
        /// Driver to infer postconditions
        /// </summary>
        public bool SuggestPostcondition(ContractInferenceManager inferenceManager)
        {
          if (this.fixpointInfo != null)
          {
            return SuggestPostcondition(inferenceManager, this.fixpointInfo);
          }

          return false;
        }

        public bool SuggestPostcondition(ContractInferenceManager inferenceManager, IFixpointInfo<APC, AbstractDomain> fixpointInfo)
        {
          Contract.Requires(inferenceManager != null);
          Contract.Requires(fixpointInfo != null);

          BoxedExpression bottomCondition;
          if (TrySuggestBottomCondition(out bottomCondition))
          {
            FlushInferredPostconditionsToInferenceManager(new List<BoxedExpression>() { bottomCondition }, inferenceManager);

            // We are done
            return false;
          }

          var postconditions = new List<BoxedExpression>();

          if (!this.DecoderForMetaData.IsVoidMethod(this.MethodDriver.CurrentMethod))
          {
            // Can we read the return expression in the PostState?
            BoxedExpression postExpression;
            if (TrySuggestExpressionPostcondition(out postExpression))
            {
              postconditions.Add(postExpression);
            }
          }

          // ask the specific analysis to provide a postcondition
          var witnessNeeded = SuggestAnalysisSpecificPostconditions(inferenceManager, fixpointInfo, postconditions);

          SuggestPostconditionsFromParameters(postconditions);

          // Get rid of redundant postconditions
          var reduced_postconditions = ReducePostconditions(postconditions);

          // Fix postconditions to be nicer
          var decompiled_postconditions = DecompilePostconditions(reduced_postconditions);

          // Flush the inferred postconditions into the postcondition manager
          FlushInferredPostconditionsToInferenceManager(decompiled_postconditions, inferenceManager);

          return witnessNeeded;
        }
        
        /// <summary>
         /// Try to read the expression for the result in the poststate. 
         /// It does not need a fixpointInfo
         /// </summary>
         protected bool TrySuggestExpressionPostcondition(out BoxedExpression postcondition)
         {
           var md = this.methodDriver;
           var context = this.Context;
           var normalExit = context.MethodContext.CFG.NormalExit;

           Variable retVar;
           if (context.ValueContext.TryResultValue(normalExit, out retVar))
           {
             // We should completely convert the expression to be sure that the pretty printing and the serialization work
             var retExp = BoxedExpression.Convert(context.ExpressionContext.Refine(normalExit, retVar), md.ExpressionDecoder, 10);

             if (retExp != null && TryMakeReturnPostcondition(retExp, out postcondition))
             {
               return true;
             }
#if true
               /* as the disjunctive recover analysis works, we need to fetch the symbolic expression in the post state */
             else if(md.MetaDataDecoder.ReturnType(md.CurrentMethod).Equals(md.MetaDataDecoder.System_Boolean) 
               && md.DisjunctiveExpressionRefiner.TryRefineExpression(normalExit.Post(), retVar, out retExp))             
             {
               return TryMakeReturnPostcondition(retExp, out postcondition);  
             }
#endif
           }
           postcondition = null;
           return false;
         }

         private bool TryMakeReturnPostcondition(BoxedExpression retExp, out BoxedExpression postcondition)
         {
           Details details;
           var expInPostState = new BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly>(this.Context, this.DecoderForMetaData);

           postcondition = expInPostState.ExpressionInPostState(retExp, false, true, false, out details);

           if (postcondition == null || details.HasReturnVariable)
           {
             return false;
           }
           else
           {
             var md = this.methodDriver;
             var retType = md.MetaDataDecoder.ReturnType(md.CurrentMethod);
             postcondition = BoxedExpression.Binary(BinaryOperator.Ceq, BoxedExpression.Result(retType), postcondition);

             return true;
           }
         }

         protected void FlushInferredPostconditionsToInferenceManager(List<BoxedExpression> postconditions, ContractInferenceManager inferenceManager)
         {
           Contract.Requires(postconditions != null);
           Contract.Requires(inferenceManager != null);

           var context = this.Context;

           var isCurrentMethodAProperty = this.DecoderForMetaData.IsPropertyGetterOrSetter(context.MethodContext.CurrentMethod);
           var propagateInferredRequires = inferenceManager.PropagateInferredRequires(isCurrentMethodAProperty);
           var propagateInferredEnsures = inferenceManager.PropagateInferredEnsures(isCurrentMethodAProperty);
           var checkPostconditions = inferenceManager.CheckFalsePostconditions;

           // If we should not propagate/infer or check postconditions, then we are done
           if (!propagateInferredEnsures && !inferenceManager.SuggestInferredEnsures(isCurrentMethodAProperty) && !checkPostconditions)
           {
             return;
           }

           var entryAfterRequires = this.Context.MethodContext.CFG.EntryAfterRequires;
           var normalExit = this.Context.MethodContext.CFG.NormalExit;
           foreach (var be in postconditions)
           {
             // Can we read be as a precondition? 
             var pre = PreconditionSuggestion.ExpressionInPreState(be, context, this.DecoderForMetaData, normalExit, allowedKinds: ExpressionInPreStateKind.MethodPrecondition);

             // If we can read be in the prestate, then we do not need to suggest it as a postcondition
             if (pre != null && pre.hasVariables && inferenceManager.InferPreconditionsFromPostconditions)
             {
               var beAsPrecondition = pre.expr;
               // If the precondition is already implied by the entry state, we simply discard it
               AbstractDomain adom;
               if (PreState(entryAfterRequires, this.fixpointInfo, out adom))
               {
                 if (adom.CheckIfHolds(beAsPrecondition).IsTop)
                 {
                   inferenceManager.AddPreconditionOrAssume(new EmptyProofObligation(entryAfterRequires), new List<BoxedExpression>() { beAsPrecondition }, ProofOutcome.Top);
                 }
               }
             }

             if (pre == null || !pre.hasVariables || (pre.hasAccessPath && !pre.hasOnlyImmutableVariables))
             {
               inferenceManager.PostCondition.AddPostconditions(new List<BoxedExpression>() { be });
             }
           }
         }

         /// <summary>
         /// If the exist state is unreachable, it returns a boxed expression for "false"
         /// </summary>
         protected bool TrySuggestBottomCondition(out BoxedExpression bottomCondition)
         {
           AbstractDomain astate;
           if (!PreState(this.Context.MethodContext.CFG.NormalExit, this.fixpointInfo, out astate) || astate.IsBottom)
           {
             bottomCondition = BoxedExpression.Const(false, this.DecoderForMetaData.System_Boolean, this.DecoderForMetaData);
             return true;
           }

           bottomCondition = null;
           return false;
         }

        /// <summary>
        /// Try to suggest postconditions involving the value of the values of the fields of this object
        /// </summary>
        protected bool SuggestPostconditionsFromParameters(List<BoxedExpression> postconditions)
        {
          var expInPostState =
            new BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly>(this.Context, this.DecoderForMetaData);

          var added = false;

          foreach (var p in this.DecoderForMetaData.Parameters(this.Context.MethodContext.CurrentMethod).Enumerate())
          {
            Variable varForp;
            Variable paramAddr;
            if (!this.Context.ValueContext.TryParameterAddress(this.Context.MethodContext.CFG.NormalExit, p, out paramAddr)) { continue; }
            if (!this.Context.ValueContext.TryLoadIndirect(this.Context.MethodContext.CFG.NormalExit, paramAddr, out varForp)) { continue; }

            var pathForp = this.Context.ValueContext.AccessPathList(this.Context.MethodContext.CFG.NormalExit, varForp, false, false);

            if (!this.Context.ValueContext.PathUnmodifiedSinceEntry(this.Context.MethodContext.CFG.NormalExit, pathForp))
            {
              continue;
            }

            Details details;
            var expForp = expInPostState.ExpressionInPostState(this.ToBoxedExpression(this.Context.MethodContext.CFG.NormalExit, varForp), false, out details);

            if (!details.HasVariables || expForp == null)
            {
              continue;
            }

            foreach (var path in this.Context.ValueContext.AccessPaths(this.Context.MethodContext.CFG.NormalExit, varForp, AccessPathFilter<Method, Type>.FromPostcondition(this.Context.MethodContext.CurrentMethod, this.DecoderForMetaData.ReturnType(this.Context.MethodContext.CurrentMethod))))
            {
              var right = BoxedExpression.Var(path, path, this.DecoderForMetaData.ParameterType(p));

              if (path.Length() > 2)
              {
                var eq = BoxedExpression.Binary(BinaryOperator.Ceq, expForp, right);

                postconditions.Add(eq);
                added = true;
              }
            }
          }

          return added;
        }

        /// <summary>
        /// Filters the postconditions, trying to find a core set
        /// </summary>
        protected List<BoxedExpression> ReducePostconditions(List<BoxedExpression> postconditions)
        {
          Contract.Requires(postconditions != null);
          Contract.Ensures(Contract.Result<List<BoxedExpression>>() != null);

          // Heuristic: we sort postconditions according to their relation symbol
          var sort_postconditions = SortPostconditions(postconditions);
          var result = new List<BoxedExpression>(postconditions.Count);
          
          var adom = this.GetTopValue();
          var topState = this.GetTopValue();

          Variable returnVariable;
          BoxedExpression returnExpression;
          if (this.Context.ValueContext.TryResultValue(this.Context.MethodContext.CFG.NormalExit, out returnVariable))
          {
            var retType = this.DecoderForMetaData.ReturnType(this.Context.MethodContext.CurrentMethod);
            returnExpression = BoxedExpression.Result(retType);
          }
          else
          {
            returnExpression = null;
          }

          var stateForPostconditions = StateForPostCondition();
          var checkWithPosts = !stateForPostconditions.IsTop;

          var stringsForEnsuresAlreadyInTheMethod = new Set<string>(this.methodDriver.AdditionalSyntacticInformation.TestsInTheMethod.Where(test => test.Tag == "ensures").Select(test => test.PC.ExtractAssertionCondition()));


          foreach (var exp_iter in sort_postconditions)
          {
            Contract.Assume(exp_iter != null);    // Need quantified invariants here

            // let's skip the inference if we already have one syntactically the same there
            if(stringsForEnsuresAlreadyInTheMethod.Contains(exp_iter.ToString()))
            {
              continue;
            }

            var exp = exp_iter;
            if (IsTrivialPostcondition(ref exp))
            {
              continue;
            }

            if (returnExpression != null)
            {

              // We express be in terms of the return symbolic value
              var be = exp_iter.Substitute(returnExpression, this.ToBoxedExpression(this.Context.MethodContext.CFG.NormalExit, returnVariable));

              // F: I hate the special cases below. 
              // I should think to a better way of avoiding removing Contract.Result == exp or forcing removing it...
              if (checkWithPosts)
              {
                // try to keep it
                if (!CommonChecks.CheckTrivialEquality(be, this.Decoder))
                {
                  // the postconditions already there prove it
                  if (stateForPostconditions.CheckIfHolds(be).IsTrue())
                  {
                    continue;
                  }
                }

                // try to remove it
                // we have a problem when we have code like:
                //
                //  Ensures(Result<>() == this.someVar)
                //  ...
                //  return this.someVar
                // 
                // because the heap analysis gives the same name to the result and someVar, and so the abstract domains (namely karr) ignore the equality, and we cannot recover anymore

                BinaryOperator bopExp;
                BoxedExpression leftExp, rightExp;
                if(be.IsBinaryExpression(out bopExp, out leftExp, out rightExp) && bopExp == BinaryOperator.Ceq && 
                  (returnVariable.Equals(leftExp.UnderlyingVariable) || returnVariable.Equals(rightExp.UnderlyingVariable)))
                { 
               
                foreach (var postCondition in this.MethodDriver.AdditionalSyntacticInformation.AssertedPostconditions)
                {
                  BinaryOperator bop;
                  if (postCondition.IsBinaryExpression(out bop, out leftExp, out rightExp) 
                    && (bop == BinaryOperator.Ceq || bop == BinaryOperator.Cobjeq) 
                    && (returnVariable.Equals(leftExp.UnderlyingVariable) || returnVariable.Equals(rightExp.UnderlyingVariable)))
                  {
                    goto next;
                  }
                }
                }

              }

              // We keep the postcondition if one of the two:
              // 1. the postcondition in terms of the symbolic value of the return value (svret) is not implied by the current abstract state
              // 2. it's a trivial postcondition when read with svret (but it wasn't before)
              if (adom.CheckIfHolds(be).IsTop || topState.CheckIfHolds(be).IsTrue())
              {
                adom = (AbstractDomain)adom.TestTrue(be);
                result.Add(exp);
              }
            }
            else
            {
              if (adom.CheckIfHolds(exp).IsTop || topState.CheckIfHolds(exp).IsTrue())
              {
                adom = (AbstractDomain)adom.TestTrue(exp);
                result.Add(exp);
              }
            }

          next:
            ;
          }

          return result.SyntacticReductionRemoval().ToList();
        }

        protected List<BoxedExpression> DecompilePostconditions(List<BoxedExpression> postconditions)
        {
          var decompiler = new BooleanExpressionsDecompiler<ILogOptions>(this.methodDriver);
          var result = new List<BoxedExpression>(postconditions.Count);

          var normalExit = this.Context.MethodContext.CFG.NormalExit;
          foreach (var exp in postconditions)
          {
            BoxedExpression expr;
            if (decompiler.FixIt(normalExit, exp, out expr))
              result.Add(expr);
          }

          return result;
        }

        /// <returns>
        /// An abstract state capturing the user-provided postcondition
        /// </returns>
        protected AbstractDomain StateForPostCondition()
        {
          AbstractDomain post, ensures;

          if (this.fixpointInfo.TryAStateForPostCondition(this.GetTopValue(), out post) 
            && this.fixpointInfo.PreState(this.methodDriver.CFG.EntryAfterRequires, out ensures)
            )
          {
            if (ensures.IsTop)
              return post;

            var be = ensures.To(
              new BoxedExpressionFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable>(this.methodDriver.MetaDataDecoder));

            foreach (var exp in be.SplitConjunctions())
            {
              post = (AbstractDomain)post.TestTrue(exp);
            }

            return post;
          }

          return this.GetTopValue();
        }

        protected List<BoxedExpression> SortPostconditions(List<BoxedExpression> postconditions)
        {
          var eq = new List<BoxedExpression>();
          var strict = new List<BoxedExpression>();
          var non_strict = new List<BoxedExpression>();
          var others = new List<BoxedExpression>();

          var view = new Set<string>(postconditions.Count);

          foreach (var exp in postconditions)
          {
            var str = exp.ToString();
            if (view.Contains(str))
            {
              continue;
            }
            else
            {
              view.Add(str);
            }

            #region The cases
            if (exp.IsBinary)
            {
              switch (exp.BinaryOp)
              {
                case BinaryOperator.Ceq:
                case BinaryOperator.Cobjeq:
                  eq.Add(exp);
                  break;


                case BinaryOperator.Cgt:
                case BinaryOperator.Cgt_Un:
                case BinaryOperator.Clt:
                case BinaryOperator.Clt_Un:
                  strict.Add(exp);
                  break;

                case BinaryOperator.Cge:
                case BinaryOperator.Cge_Un:
                case BinaryOperator.Cle:
                  non_strict.Add(exp);
                  break;

                default:
                  others.Add(exp);
                  break;
              }
            }
            else
            {
              others.Add(exp);
            }
            #endregion
          }

          var concat = new List<BoxedExpression>();
          concat.AddRange(eq);
          concat.AddRange(strict);
          concat.AddRange(non_strict);
          concat.AddRange(others);

          return concat;
        }

        public bool IsTrivialBound(BoxedExpression be)
        {
          // Try Matching var <= k
          BinaryOperator bop;
          BoxedExpression left, right;
          if (be.IsBinaryExpression(out bop, out left, out right))
          {
            Int64 k64 = 0;

            // left <= k or left >= k
            if ((bop == BinaryOperator.Cle || bop == BinaryOperator.Cge))
            {
              if (left.IsVariable && right.IsConstantInt64(out k64))
              {
                Variable v;
                if (left.TryGetFrameworkVariable(out v))
                {
                  var type = this.Context.ValueContext.GetType(this.Context.MethodContext.CFG.NormalExit, v);
                  if (type.IsNormal)
                  {
                    var intv = this.DecoderForMetaData.GetIntervalForType(type.Value);

                    switch (bop)
                    {
                      case BinaryOperator.Cle:
                        { // v <= k, we want k < intv.UpperBound
                          return k64 >= intv.UpperBound;
                        }
                      case BinaryOperator.Cge:
                        { // v >= k, we want k > int.LowerBound
                          return k64 <= intv.LowerBound;
                        }
                    }
                  }
                }
              }
              else if (left.IsConstantInt64(out k64) && right.IsVariable)
              {
                Variable v;
                if (right.TryGetFrameworkVariable(out v))
                {
                  var type = this.Context.ValueContext.GetType(this.Context.MethodContext.CFG.NormalExit, v);
                  if (type.IsNormal)
                  {
                    var intv = this.DecoderForMetaData.GetIntervalForType(type.Value);
                    switch (bop)
                    {
                      case BinaryOperator.Cle:
                        { // k <= v
                          return k64 <= intv.LowerBound;
                        }
                      case BinaryOperator.Cge:
                        { // k >= v
                          return k64 >= intv.UpperBound;
                        }
                    }
                  }
                }
              }
            }
          }
          return false;
        }

        public /* public to make it accessible to class invariants */
        List<BoxedExpression> ToListOfBoxedExpressions(AbstractDomain astate,
          BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly> expInPostState)
        {
          Contract.Ensures(Contract.Result<List<BoxedExpression>>() != null);

          var expressions = new List<BoxedExpression>();
          var be = astate.To(new BoxedExpressionFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable>(this.DecoderForMetaData));
          var beSplitted = be.SplitConjunctions();

          foreach (var exp in beSplitted)
          {
            Details details;

            // Note: For doubles we want to keep the bounds, because this imply the value is not NaN or +/- oo
            if (IsTrivialBound(exp))
            {
              continue;
            }

            var post =
              expInPostState.ExpressionInPostState(exp.MakeItPrettier(this.Decoder, this.Encoder), true, true, true, out details);

            if (post != null
              && !IsTrivialBound(post)              // Filter all the postconditions from types
              && !post.IsConstantTrue()
              && (!details.HasOldVariable || details.HasCompoundExp)) // HasOldVariable ==> HasCompoundExp
            {
              expressions.Add(post);
            }
          }

          return expressions;
        }

        /// <summary>
        /// Heuristics to filter out trivial postconditions
        /// </summary>
        protected bool IsTrivialPostcondition(ref BoxedExpression exp)
        {
          Contract.Requires(exp != null);

          BinaryOperator bop;
          BoxedExpression left, right;

          if (exp.IsConstant)
            return true;

          if (exp.IsBinaryExpression(out bop, out left, out right))
          {
            // if left is unsigned, then left {>=, >} 0 is trivial
            if (IsConstantZero(right))
            {
              switch (bop)
              {
                case BinaryOperator.Cge:
                case BinaryOperator.Cge_Un:
                case BinaryOperator.Cgt:
                case BinaryOperator.Cgt_Un:
                  return IsOfUnsignedType(left) || IsTrivialBoolean(exp);

                case BinaryOperator.Cne_Un:
                case BinaryOperator.Ceq:
                  return Specialize(bop, left, ref exp) || IsTrivialBoolean(exp);

                default:
                  // do nothing;
                  break;
              }
            }
            if (IsConstantZero(left))
            {
              // if right is unsigned, then 0 {<=, <} right is trivial
              switch (bop)
              {
                case BinaryOperator.Cle:
                case BinaryOperator.Cle_Un:
                case BinaryOperator.Clt:
                case BinaryOperator.Clt_Un:
                  return IsOfUnsignedType(right) || IsTrivialBoolean(exp);

                case BinaryOperator.Cne_Un:
                case BinaryOperator.Ceq:
                  return Specialize(bop, right, ref exp) || IsTrivialBoolean(exp);

                default:
                  // do nothing;
                  break;
              }
            }

            return IsTrivialBoolean(exp);
          }

          return false;
        }

        protected bool IsTrivialBoolean(BoxedExpression exp)
        {
          BinaryOperator bop;
          BoxedExpression left, right;
          if (!exp.IsBinaryExpression(out bop, out left, out right))
          {
            return false;
          }

          switch (bop)
          {
            case BinaryOperator.Ceq:
            case BinaryOperator.Cne_Un:
            case BinaryOperator.Cobjeq:
            case BinaryOperator.Clt:
            case BinaryOperator.Clt_Un:
            case BinaryOperator.Cgt:
            case BinaryOperator.Cgt_Un:
              {
                return false;
              }

            case BinaryOperator.Cge:
            case BinaryOperator.Cge_Un:
            case BinaryOperator.Cle:
            case BinaryOperator.Cle_Un:
              {
                int leftValue, rightValue;
                
                var isIntLeft = left.IsConstantInt(out leftValue);
                var isIntRight = right.IsConstantInt(out rightValue);

                var isBoolLeft = IsBoolean(left);
                var isBoolRight = IsBoolean(right);

                  return IsTrivialBooleanInequality(bop, isIntLeft, isIntRight, isBoolLeft, isBoolRight, leftValue, rightValue);
              }

            default:
              {
                return false;
              }
          }
        }

        private bool IsTrivialBooleanInequality(BinaryOperator bop, bool isIntLeft, bool isIntRight, bool isBoolLeft, bool isBoolRight, int leftValue, int rightValue)
        {
          if ((isIntLeft || isIntRight) && (isBoolLeft || isBoolRight))
          {
            switch (bop)
            {
              case BinaryOperator.Cge:
              case BinaryOperator.Cge_Un:
                return (leftValue == 0 || rightValue == 0) // 0 >= b || b >= 0 
                    || (leftValue >= 1 && isBoolRight)             // 1 >= b
                    || (rightValue <= -1 && isBoolLeft);            // b >= -1

              case BinaryOperator.Cle:
              case BinaryOperator.Cle_Un:
                return (leftValue == 0 || rightValue == 0) // b >= 0 || 0 >= b 
                  || (leftValue <= -1 && isBoolRight)     // -1 <= b
                  || (rightValue >= 1 && isBoolLeft);     // b >= 1

              default:
                return false;
            }
          }

          return false;
        }

        protected static bool IsConstantZero(BoxedExpression exp)
        {
          return exp.IsConstant && exp.Constant is Int32 && ((Int32)exp.Constant == 0);
        }

        protected bool IsConstantInt32(APC pc, Variable var, out int value)
        {
          object obj_value;
          Type type;

          if (this.Context.ValueContext.IsConstant(pc, var, out type, out obj_value) && this.DecoderForMetaData.System_Int32.Equals(type))
          {
            value = (int)obj_value;

            return true;
          }

          value = default(int);
          return false;
        }

        protected bool IsBoolean(BoxedExpression exp)
        {
          Variable var;

          if (exp.TryGetFrameworkVariable(out var))
          {
            var p = this.Context.ValueContext.AccessPathList(this.Context.MethodContext.CFG.NormalExit, var, true, false);
            var last = (PathElement)null;

            for (var e = p; e != null; e = e.Tail)
            {
              last = e.Head;
            }

            Type t;
            if (last != null && last.TryGetResultType(out t))
            {
              return this.DecoderForMetaData.System_Boolean.Equals(t);
            }
          }

          return false;
        }

        protected bool IsOfUnsignedType(BoxedExpression exp)
        {
          if (!exp.IsVariable)
            return false;

          Variable v;
          if (exp.UnderlyingVariable is Variable)
          {
            v = (Variable)exp.UnderlyingVariable;
          }
          else
          {
            var bv = exp.UnderlyingVariable as BoxedVariable<Variable>;

            if (bv == null || !bv.TryUnpackVariable(out v))
            {
              return false;
            }
          }

          var type = this.Context.ValueContext.GetType(this.Context.MethodContext.CFG.NormalExit, v);

          if (type.IsNormal)
          {
            if (type.Value.Equals(this.DecoderForMetaData.System_Char))
              return true;
            if (type.Value.Equals(this.DecoderForMetaData.System_UInt16))
              return true;
            if (type.Value.Equals(this.DecoderForMetaData.System_UInt32))
              return true;
            if (type.Value.Equals(this.DecoderForMetaData.System_UInt64))
              return true;
          }


          return false;

        }

        /// <summary> 
        /// Eval the expression bop(left, 0)
        /// </summary>
        /// <returns>true iff it is a tautology</returns>
        protected bool Specialize(BinaryOperator bop, BoxedExpression left, ref BoxedExpression exp)
        {
          Contract.Requires(left != null);
          Contract.Requires(exp != null);
          Contract.Requires(!left.Equals(exp));

          BinaryOperator bop_left;
          BoxedExpression left1, left2;

          // Special case to check if "this != 0", and hence remove the tautology
          if (bop == BinaryOperator.Cne_Un && left.IsVariable && left.ToString() == "this")
          {
            exp = null;
            return true;
          }

          if (left.IsBinaryExpression(out bop_left, out left1, out left2))
          {
            switch (bop_left)
            {
              case BinaryOperator.Ceq:
                switch (bop)
                {
                  case BinaryOperator.Ceq:
                    // (left1 == left2) == 0 => left1 != left2
                    exp = BoxedExpression.Binary(BinaryOperator.Cne_Un, left1, left2);
                    return false;

                  case BinaryOperator.Cne_Un:
                    // (left1 == left2) != 0 => left1 == left2
                    exp = left;
                    return false;

                  default:
                    return false;
                }

              case BinaryOperator.Cne_Un:
                switch (bop)
                {
                  case BinaryOperator.Ceq:
                    // (left1 == left2) != 0 => left1 == left2
                    exp = BoxedExpression.Binary(BinaryOperator.Ceq, left1, left2);
                    return false;

                  case BinaryOperator.Cne_Un:
                    if (IsConstantZero(left2))
                    { // (left1 != 0) != 0 => left1 != 0
                      exp = left;
                      return Specialize(bop, left1, ref exp);
                    }
                    else
                    {
                      exp = left;
                      return false;
                    }

                  default:
                    return false;
                }
              case BinaryOperator.Cge:
              case BinaryOperator.Cge_Un:
              case BinaryOperator.Cgt:
              case BinaryOperator.Cgt_Un:
              case BinaryOperator.Cle:
              case BinaryOperator.Cle_Un:
              case BinaryOperator.Clt:
              case BinaryOperator.Clt_Un:
                switch (bop)
                {
                  case BinaryOperator.Ceq:
                    // (left == 0) => !(left)
                    exp = BoxedExpression.Binary(Not(bop_left), left1, left2);
                    return false;

                  case BinaryOperator.Cne_Un:
                    // (left != 0) => left
                    exp = left;
                    return false;

                  default:
                    return false;
                }

              default:
                return false;
            }
          }

          return false;
        }

        protected BinaryOperator Not(BinaryOperator bop)
        {
          switch (bop)
          {
            case BinaryOperator.Add:
            case BinaryOperator.Add_Ovf:
            case BinaryOperator.Add_Ovf_Un:
            case BinaryOperator.And:
            case BinaryOperator.Div:
            case BinaryOperator.Div_Un:
            case BinaryOperator.Mul:
            case BinaryOperator.Mul_Ovf:
            case BinaryOperator.Mul_Ovf_Un:
            case BinaryOperator.Or:
            case BinaryOperator.Rem:
            case BinaryOperator.Rem_Un:
            case BinaryOperator.Shl:
            case BinaryOperator.Shr:
            case BinaryOperator.Sub:
            case BinaryOperator.Sub_Ovf:
            case BinaryOperator.Sub_Ovf_Un:
            case BinaryOperator.Xor:
              return bop;

            case BinaryOperator.Ceq:
              return BinaryOperator.Cne_Un;

            case BinaryOperator.Cge:
              return BinaryOperator.Clt;

            case BinaryOperator.Cge_Un:
              return BinaryOperator.Clt_Un;

            case BinaryOperator.Cgt:
              return BinaryOperator.Cle;

            case BinaryOperator.Cgt_Un:
              return BinaryOperator.Cle_Un;

            case BinaryOperator.Cle:
              return BinaryOperator.Cgt;

            case BinaryOperator.Cle_Un:
              return BinaryOperator.Cgt_Un;

            case BinaryOperator.Clt:
              return BinaryOperator.Cge;

            case BinaryOperator.Clt_Un:
              return BinaryOperator.Cge_Un;

            case BinaryOperator.Cne_Un:
              return BinaryOperator.Ceq;

            case BinaryOperator.Cobjeq:
              return BinaryOperator.Cobjeq;

            default:
              throw new InvalidOperationException();
          }
        }

        #endregion

        #region Analysis specific precondition suggestion
        virtual public void SuggestPrecondition(ContractInferenceManager inferenceManager)
        {
          return;
        }

        [Pure]
        protected BoxedExpression ReadVariableInPreStateForRequires(APC pc, Variable var)
        {
          return ReadVariableInPreStateForRequires(pc, new BoxedVariable<Variable>(var));
        }

        [Pure]
        protected BoxedExpression ReadVariableInPreStateForRequires(APC pc, BoxedVariable<Variable> var)
        {
          var pre = PreconditionSuggestion.ExpressionInPreState(
            ToBoxedExpression(pc, var), this.Context, this.DecoderForMetaData, pc,
            allowedKinds: ExpressionInPreStateKind.MethodPrecondition);
          return pre == null ? null : pre.expr;
        }

        [Pure]
        protected BoxedExpression ReadVariableInPostState(APC pc, BoxedVariable<Variable> var)
        {
          var reader = new BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly>
            (this.Context, this.DecoderForMetaData);
          Variable v;
          if (var.TryUnpackVariable(out v))
          {
            Details dummy2;
            return reader.ExpressionInPostState(ToBoxedExpression(pc, v), true, out dummy2);
          }
          return null;
        }

        [Pure]
        protected string ReadVariableInPostState(APC pc, SetOfConstraints<BoxedVariable<Variable>> var)
        {
          Contract.Requires(var != null);

          if (var.IsTop) return "top";
          if (var.IsBottom) return "bottom";

          var strBuilder = new StringBuilder();

          foreach (var x in var.Values)
          {
            var be = ReadVariableInPostState(pc, x);
            strBuilder.Append(be != null? be.ToString(): "<null>" + " ");
          }

          return strBuilder.ToString();
        }


        #endregion

        #region Postconditions for backwards propagation

        public IEnumerable<BoxedExpression> GetPostconditionAsExpression()
        {
          AbstractDomain adom;
          if (this.fixpointInfo.TryAStateForPostCondition(this.GetTopValue(), out adom))
          {
            var post = adom.To(new BoxedExpressionFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue>(this.DecoderForMetaData));

            if (post != null)
            {
              foreach (var p in post.SplitConjunctions())
                yield return p;
            }
          }
        }
        
        #endregion

        #region Utils

        protected Type GetTypeOrObject(APC PC, BoxedVariable<Variable> arrayBoxed)
        {
          Variable arrayVar;
          if (arrayBoxed.TryUnpackVariable(out arrayVar))
          {
            var t = this.MethodDriver.Context.ValueContext.GetType(PC, arrayVar);
            if (t.IsNormal)
            {
              return t.Value;
            }
          }
          return this.DecoderForMetaData.System_Object;
        }

        [ContractVerification(true)]
        protected BoxedExpression ExpandUnsignedConstants(APC pc, BoxedExpression input, bool hasContext, Type context, int depth)
        {
          Contract.Requires(input != null);
          Contract.Requires(depth >= 0);

          if (depth == 0)
          {
            return null;
          }

          Variable v1, v2;
          BoxedExpression exp1, exp2;
          UnaryOperator uop;
          BinaryOperator bop;

          if (input.IsConstant)
          {
            if (hasContext)
            {
              var constVal = input.Constant;
              // if we are in an unsigned context, we want to replace negative int values with large unsigned
              if (constVal is Int32 && this.DecoderForMetaData.IsUnsignedIntegerType(context))
              {
                var intVal = (Int32)constVal;
                if(intVal < 0)
                {
                  return BoxedExpression.Const((uint)intVal, context, this.DecoderForMetaData);
                }
              }
            }
          }
          else if (input.IsUnaryExpression(out uop, out exp1))
          {
            var recursive = ExpandUnsignedConstants(pc, exp1, hasContext, context, depth -1);

            if (recursive == null)
            {
              return recursive;
            }

            if (!exp1.Equals(recursive))
            {
              return BoxedExpression.Unary(uop, recursive);
            }
          }
          else if (input.IsBinaryExpression(out bop, out exp1, out exp2))
          {
            if (exp1.TryGetFrameworkVariable(out v1) && exp2.TryGetFrameworkVariable(out v2))
            {
              var exp1Prime = ExpandUnsignedConstantForBinary(pc, v2, exp1, depth -1);

              if (exp1Prime == null)
              {
                return null;
              }

              var exp2Prime = ExpandUnsignedConstantForBinary(pc, v1, exp2, depth -1);

              if (exp2Prime == null)
              {
                return null;
              }

              Contract.Assert(exp1Prime != null);
              Contract.Assert(exp2Prime != null);

              if (!exp1Prime.Equals(exp1) || !exp2Prime.Equals(exp2))
              {
                return BoxedExpression.Binary(bop, exp1Prime, exp2Prime, input.UnderlyingVariable); // keep the same framework variable
              }
            }
          }

          return input;
        }

        private BoxedExpression ExpandUnsignedConstantForBinary(APC pc, Variable v1, BoxedExpression other, int depth)
        {
          Contract.Requires(other != null);
          Contract.Requires(depth >= 0);

          BoxedExpression result = null;

          if (depth == 0)
          {
            return null;
          }
          
          var t1 = this.methodDriver.Context.ValueContext.GetType(pc, v1);
          if (t1.IsNormal && this.DecoderForMetaData.IsUnsignedIntegerType(t1.Value))
          {
            result = ExpandUnsignedConstants(pc, other, true, t1.Value, depth -1);
          }
          else
          {
            result = ExpandUnsignedConstants(pc, other, false, this.DecoderForMetaData.System_Void, depth -1);
          }

          // The result can be null
          return result;
        }

        #endregion

        #region Pretty printing

        [Pure]
        protected string VariablesToPrettyNames(APC pc, BoxedVariable<Variable> var)
        {

          Contract.Requires(var != null);
          
          var be = VariablesToPrettyNames(pc, var);

          return be != null ? be.ToString() : null;
        }

        [Pure]
        protected string VariablesToPrettyNames(APC pc, FlatAbstractDomain<BoxedVariable<Variable>> var)
        {
          Contract.Requires(var != null);

          if (var.IsTop)
            return "Top";
          if (var.IsBottom)
            return "Bottom";
          return VariablesToPrettyNames(pc, var.BoxedElement);
        }
        #endregion
      }

      [ContractClassFor(typeof(GenericValueAnalysis<,>))]
      abstract class GenericValueAnalysisContracts<AbstractDomain, AnalysisOptions>
        : GenericValueAnalysis<AbstractDomain, AnalysisOptions>
        where AbstractDomain : IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>
        where AnalysisOptions : IValueAnalysisOptions
      {
        GenericValueAnalysisContracts() : base(null, null, default(AnalysisOptions), null) { }

        public override bool SuggestAnalysisSpecificPostconditions(ContractInferenceManager inferenceManager, IFixpointInfo<APC, AbstractDomain> fixpointInfo, List<BoxedExpression> postconditions)
        {
          Contract.Requires(inferenceManager != null);
          Contract.Requires(postconditions != null);

          throw new NotImplementedException();
        }

        public override bool TrySuggestPostconditionForOutParameters(IFixpointInfo<APC, AbstractDomain> fixpointInfo, List<BoxedExpression> postconditions, Variable p, FList<PathElement> path)
        {
          Contract.Requires(fixpointInfo != null);
          Contract.Requires(postconditions != null);

          return false;
        }

        public override IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC, AbstractDomain> fixpoint)
        {
          Contract.Requires(fixpoint != null);

          throw new NotImplementedException();
        }
      }
    }
  }
}

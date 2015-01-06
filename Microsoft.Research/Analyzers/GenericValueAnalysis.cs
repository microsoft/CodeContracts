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

using System;
using Generics = System.Collections.Generic;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains.Numerical;
using System.Collections.Generic;
using System.Text;


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
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>
      where Variable : IEquatable<Variable>
      where ExternalExpression : IEquatable<ExternalExpression>
      where Type : IEquatable<Type>
    {
      /// <summary>
      /// The entry point for running a bounds analysis
      /// </summary>
      internal static IMethodResult<Variable> RunTheAnalysis<AbstractDomain>
      (
        string methodName,
        IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions> driver,
        GenericValueAnalysis<AbstractDomain> analysis
      )
        where AbstractDomain : IAbstractDomainForEnvironments<BoxedExpression>
      {
        // *** The next two lines must be strictly sequential ***
        Action<AbstractDomain> closure = driver.CreateForward<AbstractDomain>(analysis);

        // At this point, CreateForward has called the Visitor, so the context has been created, so that now we can call initValue
        AbstractDomain initValue = analysis.InitialValue;

        closure(initValue);   // Do the analysis 

        return analysis;
      }

      /// <summary>
      /// 
      /// The generic value analysis
      /// </summary>
      /// <typeparam name="AbstractDomain">The abstract domain, which must implement <code>IAssignInParallel</code></typeparam>
      public abstract class GenericValueAnalysis<AbstractDomain> :
        MSILVisitor<APC, Local, Parameter, Method, Field, Type, Variable, Variable, AbstractDomain, AbstractDomain>,
        IMoveNextOnePassAnalysis<Local, Parameter, Method, Field, Property, Type, ExternalExpression, Attribute, Assembly, AbstractDomain, Variable>,
        //IValueAnalysis<APC, AbstractDomain,
        //  IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, AbstractDomain, AbstractDomain>,
        //    Variable, IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable>>,
        IMethodResult<Variable>
        where AbstractDomain : IAbstractDomainForEnvironments<BoxedExpression>
      {
        #region Some statics

        protected static readonly Interval CharRange = Interval.For(UInt16.MinValue, UInt16.MaxValue);
        protected static readonly Interval Int16Range = Interval.For(Int16.MinValue, Int16.MaxValue);
        protected static readonly Interval Int32Range = Interval.For(Int32.MinValue, Int32.MaxValue);
        protected static readonly Interval ByteRange = Interval.For(Byte.MinValue, Byte.MaxValue);

        protected static int MaxSize = -1;

        public static int PreconditionsDiscoved
        {
          get
          {
            return ProofObligationWithDecoder.PreconditionsDiscovered;
          }
        }

        #endregion

        #region Private state
        protected string methodName;
        
        private IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions> methodDriver;
        
        protected BoxedExpressionDecoder<Type, ExternalExpression> decoderForExpressions;

        private Set<APC> PCAlreadyVisited;

        protected IValueAnalysisOptions options;
        #endregion

        private IExpressionEncoder<BoxedExpression> encoder;

        #region Constructor
        protected GenericValueAnalysis(
          string methodName, 
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions> mdriver, 
          IValueAnalysisOptions options)
        {
          ThresholdDB.Reset();

          this.methodName = methodName;
          this.methodDriver = mdriver;
          this.decoderForExpressions = BoxedExpressionDecoder.Decoder(new ValueExpDecoder(this.Context, this.DecoderForMetaData), 
            this.TypeFor);
          
          this.PCAlreadyVisited = new Set<APC>();
          this.options = options;

          this.fixpointInfo_List = null; 

          PrintAbstractDomainPerformanceStatistics();
        }
        #endregion

        #region To be overridden by clients!
        /// <summary>
        /// The initial state for the analysis is top
        /// </summary>
        abstract internal protected AbstractDomain InitialValue { get; }

        #endregion

        #region Getters
        /// <summary>
        /// Get the metadata decoder
        /// </summary>
        protected IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> DecoderForMetaData
        {
          get
          {
            return this.methodDriver.MetaDataDecoder; 
          }
        }

        protected IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions> MethodDriver
        {
          get
          {
            return this.methodDriver;
          }
        }

        /// <summary>
        /// Get the context
        /// </summary>
        protected IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> Context
        {
          get
          {
            return this.MethodDriver.Context;
          }
        }

        /// <summary>
        /// Expression decoder
        /// </summary>
        protected IExpressionEncoder<BoxedExpression> Encoder
        {
          get
          {
            if (this.encoder == null)
            {
              Debug.Assert(this.DecoderForMetaData != null);
              Debug.Assert(this.Context != null);

              this.encoder = BoxedExpressionEncoder.Encoder(this.DecoderForMetaData, this.Context);
            }

            return this.encoder;
          }
        }

        #endregion

        #region IValueAnalysis implementation

        public virtual IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, AbstractDomain, AbstractDomain>
          Visitor(IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context)
        {
          return this;
        }

        /// <summary>
        /// The generic join/widening
        /// </summary>
        public AbstractDomain Join(Microsoft.Research.DataStructures.Pair<APC, APC> edge, AbstractDomain newState, AbstractDomain prevState, out bool changed, bool widen)
        {
          ALog.BeginJoin(PrettyPrintPC(edge.One), widen, StringClosure.For(newState), StringClosure.For(prevState));

          UpdateMaxSize(newState);
          UpdateMaxSize(prevState);

          AbstractDomain joinedState;

          if (!widen) {
            joinedState = HelperForJoin(newState, prevState, edge);

            changed = true;
          } else {
            joinedState = HelperForWidening(newState, prevState, edge);
            changed = !joinedState.LessEqual(prevState);
          }

          ALog.EndJoin(changed, StringClosure.For(joinedState));

          UpdateMaxSize(joinedState);

          return joinedState;
        }

        protected virtual AbstractDomain HelperForJoin(AbstractDomain newState, AbstractDomain prevState, Microsoft.Research.DataStructures.Pair<APC, APC> edge)
        {
          return (AbstractDomain)newState.Join(prevState);
        }

        protected virtual AbstractDomain HelperForWidening(AbstractDomain newState, AbstractDomain prevState, Microsoft.Research.DataStructures.Pair<APC, APC> edge)
        {
          return (AbstractDomain)newState.Widening(prevState);
        }
        
        /// <summary>
        /// The parallel assignment
        /// </summary>
        public AbstractDomain ParallelAssign(Pair<APC, APC> edge, IFunctionalMap<Variable, FList<Variable>> sourceTargetMap, AbstractDomain state)
        {
          ALog.BeginParallelAssign(StringClosure.For(edge), StringClosure.For(state));

          UpdateMaxSize(state);

          var refinedMap = RefineMapToExpressions(edge.One, sourceTargetMap);

          state.AssignInParallel(refinedMap);

          UpdateMaxSize(state);

          ALog.EndParallelAssign(StringClosure.For(state));

          return state;
        }

        private bool CannotSatisfy(BoxedExpression refinedExp)
        {      
          // "assume false"
          int value;
          if (this.decoderForExpressions.IsConstant(refinedExp) 
            && this.decoderForExpressions.TryValueOf<int>(refinedExp, ExpressionType.Int32, out value)
            && value == 0)
          {
            return true;
          }

          // "assume NaN == NaN"
          if (this.decoderForExpressions.IsBinaryExpression(refinedExp)
            && this.decoderForExpressions.OperatorFor(refinedExp) == ExpressionOperator.Equal)
          {
            var left = this.decoderForExpressions.LeftExpressionFor(refinedExp);
            var right = this.decoderForExpressions.RightExpressionFor(refinedExp);

            if (this.decoderForExpressions.IsNaN(left) || this.decoderForExpressions.IsNaN(right))
            {
              return true;
            }
          }

          return false;
        }

        /// <summary>
        /// The assumptions in the code: either the branches or the user-provided
        /// </summary>
        public override AbstractDomain/*!*/ Assume(APC pc, string tag, Variable source, AbstractDomain data)
        {
          BoxedExpression refinedExp = ToBoxedExpression(pc, source);
          AbstractDomain/*!*/ result;

          ALog.BeginTransferFunction(StringClosure.For("assume") ,
            StringClosure.For("({0}) {1}", StringClosure.For(tag), ExpressionPrinter.ToStringClosure(refinedExp, this.decoderForExpressions)),
            PrettyPrintPC(pc), StringClosure.For(data));
          ALog.Assume(StringClosure.For(tag), ExpressionPrinter.ToStringClosure(refinedExp, this.decoderForExpressions));

          if (tag != "false" && CannotSatisfy(refinedExp))
          {
            ALog.EndTransferFunction(StringClosure.For("<bottom>"));
            return (AbstractDomain)data.Bottom;
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

          ALog.EndTransferFunction(StringClosure.For(result));

          UpdateMaxSize(result);

          return result;
        }



        public override AbstractDomain Assert(APC pc, string tag, Variable condition, AbstractDomain data)
        {
          BoxedExpression refinedExp = ToBoxedExpression(pc, condition);

          ALog.BeginTransferFunction(StringClosure.For("assert"),
            StringClosure.For("{0} {1}", StringClosure.For(tag), ExpressionPrinter.ToStringClosure(refinedExp, this.decoderForExpressions)), PrettyPrintPC(pc),
            StringClosure.For(data));

          if (CannotSatisfy(refinedExp))
          {
            data = (AbstractDomain)data.Bottom;
          }
          else
          {
            data = (AbstractDomain)data.TestTrue(refinedExp);
          }

          ALog.EndTransferFunction(StringClosure.For(data));

          return data;
        }

        public override AbstractDomain EndOld(APC pc, APC matchingBegin, Type type, Variable dest, Variable source, AbstractDomain data)
        {
          data.TestTrue(
            this.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Equal,
                        ToBoxedExpression(this.Context.Post(pc), dest), 
                        ToBoxedExpression(pc, source)));
          return data;
        }

        public bool IsBottom(APC pc, AbstractDomain state)
        {
          return state.IsBottom;
        }

        /// <summary>
        /// Default: do nothing, and return just the state
        /// </summary>
        protected sealed override AbstractDomain Default(APC pc, AbstractDomain data)
        {
          return data;
        }

        #region Private methods
        /// <summary>
        /// Construct an isomorphic dictionary, by (1) refining the sources, and (2) transforming variables into expressions
        /// </summary>
        private Generics.Dictionary<BoxedExpression, FList<BoxedExpression>> RefineMapToExpressions(APC pc, IFunctionalMap<Variable, FList<Variable>> sourceTargetMap)
        {
          var result = new Generics.Dictionary<BoxedExpression, FList<BoxedExpression>>(sourceTargetMap.Count);

          foreach (Variable v in sourceTargetMap.Keys)
          {
            BoxedExpression vRefined = ToBoxedExpression( pc, v);

            result[vRefined] = FList<Variable>.Map<BoxedExpression>(delegate(Variable target) { return BoxedExpression.For(this.Context.For(target), this.decoderForExpressions.Outdecoder); }, sourceTargetMap[v]);
          }

          return result;
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
            APC postPC = this.Context.Post(pc);

            if (left.Variable is Variable && right.Variable is Variable)
            {
              Variable leftVar = (Variable)left.Variable;
              Variable rightVar = (Variable)right.Variable;

              Variable leftLength, rightLength;

              // Array lengths
              if (this.Context.TryGetArrayLength(postPC, leftVar, out leftLength)
                && this.Context.TryGetArrayLength(postPC, rightVar, out rightLength))
              {
                BoxedExpression eq = BoxedExpression.Binary(
                  BinaryOperator.Ceq, ToBoxedExpression(postPC, leftLength), ToBoxedExpression(postPC, rightLength));

                adomain = (AbstractDomain) adomain.TestTrue(eq);
              }

              // WritableBytes
              if (this.Context.TryGetWritableBytes(postPC, leftVar, out leftLength)
                && this.Context.TryGetWritableBytes(postPC, rightVar, out rightLength))
              {
                BoxedExpression eq = BoxedExpression.Binary(
                   BinaryOperator.Ceq, ToBoxedExpression(postPC, leftLength), ToBoxedExpression(postPC, rightLength));
                adomain = (AbstractDomain)adomain.TestTrue(eq);
              }

            }
          }

          return adomain;
        }

        #endregion


        #region Conditional defined methods for printing statistics
        [Conditional("TRACE_AD_PERFORMANCES")]
        private void UpdateMaxSize(int count)
        {
          if (count > MaxSize)
          {
            MaxSize = count;

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("New max size for variables {0}", MaxSize);
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
          }
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
          Console.WriteLine("MaxSize so far {0}", MaxSize);

          // Add other performance stuff there
        }
        #endregion


        #region Not important methods
        public AbstractDomain ImmutableVersion(AbstractDomain state)
        {
          return state;
        }

        virtual public AbstractDomain MutableVersion(AbstractDomain state)
        {
         return (AbstractDomain)state.Clone();
        }

        protected IFixpointInfo<APC, AbstractDomain> fixpointInfo;
        protected List<IFixpointInfo<APC, AbstractDomain>> fixpointInfo_List;

        protected bool PreState(APC label, out AbstractDomain ifFound) { return this.fixpointInfo.PreState(label, out ifFound); }

        public Predicate<APC> CacheStates(IFixpointInfo<APC, AbstractDomain> fixpointInfo)
        {
          this.fixpointInfo = fixpointInfo;

          return this.Obligations.PCWithProofObligation;
        }

        public void Dump(Microsoft.Research.DataStructures.Pair<AbstractDomain, System.IO.TextWriter> stateAndWriter)
        {
          stateAndWriter.Two.WriteLine(stateAndWriter.One.ToString());
        }
        #endregion

        #endregion

        #region IMoveNextOnePassAnalysis <..> Members
        public AbstractDomain GetInitialValue(Converter<Variable, int> key) {
          return this.InitialValue;
        }
        public AbstractDomain ReturnState {
          get {
            AbstractDomain result = this.InitialValue;
            if (this.fixpointInfo.PreState(this.Context.NormalExit, out result))
              return result;
            return result;
          }
        }
        public IMethodAnalysisFixPoint<Variable> ExtractResult() {
          return this;
        }
        public virtual IMoveNextOnePassAnalysis<Local, Parameter, Method, Field, Property, Type, ExternalExpression, Attribute, Assembly, AbstractDomain, Variable> Duplicate() {
          return this;
        }
        #endregion 

        #region IMethodResult<Variable> Members

        protected abstract ProofObligations<Local, Parameter, Method, Field, Property, Type, Variable, ExternalExpression, Attribute, Assembly, BoxedExpression, ProofObligationBase<BoxedExpression, Variable>>
          Obligations { get; }

        public abstract void ValidateImplicitAssertions(IOutputResults output);

        public abstract IFactQuery<BoxedExpression, Variable> FactQuery
        {
          get;
        }

        public ProofOutcome ValidateExplicitAssertion(APC pc, Variable value)
        {
          return this.FactQuery.IsTrue(pc, ToBoxedExpression(pc, value));
        }

        /// <summary>
        /// Default: Do not suggest anything...
        /// </summary>
        public virtual void SuggestPostcondition(IOutputResults output)
        {
          // Do nothing
        }



        #endregion

        #region Some Utils
        protected StringClosure PrettyPrintPC(APC pc)
        {
          return StringClosure.For("{0} (line {1})", StringClosure.For(pc), StringClosure.For(this.Context.SourceStartLine(pc)));
        }

        /// <returns>
        /// A boxed expression standing for the variable <code>var</code>
        /// </returns>
        protected BoxedExpression ToBoxedExpression(APC pc, Variable var)
        {
          return BoxedExpression.For(this.Context.Refine(pc, var), this.decoderForExpressions.Outdecoder);
        }

        protected BoxedExpression ToBoxedExpressionWithConstantRefinement(APC pc, Variable v)
        {
          Type type;
          object value;

          if (Context.IsConstant(pc, v, out type, out value))
          {
            if (this.DecoderForMetaData.System_Int32.Equals(value))
            {
              return BoxedExpression.Const(value, type, this.DecoderForMetaData);
            }
          }

          return ToBoxedExpression(pc, v);
        }

        /// <returns>
        /// A boxed expression standing for the rational <code>rational</code>
        /// </returns>
        protected BoxedExpression ToBoxedExpression(Rational rational)
        {
          return rational.ToExpression(this.Encoder); 
        }

        /// <returns>
        /// An invariant that holds at the program point <code>pc</code>
        /// </returns>
        public string InvariantAt(APC pc)
        {
          AbstractDomain adomain;
          if (PreState(pc, out adomain))
          {
            return adomain.ToRewritingRule();
          }

          return "1";
        }

        public virtual AnalysisStatistics Statistics()
        {
          return this.Obligations.Statistics;
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

              if (o.Equals(this.DecoderForMetaData.System_Char))
              {
                return ExpressionType.UInt8;
              }
              if (o.Equals(this.DecoderForMetaData.System_Double))
              {
                return ExpressionType.Float64;
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
              if (o.Equals(this.DecoderForMetaData.System_Int8))
              {
                return ExpressionType.Int8;
              }
              if (o.Equals(this.DecoderForMetaData.System_Single))
              {
                return ExpressionType.Float32;
              }
              if (o.Equals(this.DecoderForMetaData.System_UInt16))
              {
                return ExpressionType.UInt16;
              }
              if (o.Equals(this.DecoderForMetaData.System_UInt32))
              {
                return ExpressionType.UInt32;
              }
            }
          }

          return ExpressionType.Unknown;
        }
        #endregion
      }

      /// <summary>
      /// The common superclass for the bounds and unsafe analyses
      /// </summary>
      public abstract class GenericNumericalAnalysis<AbstractDomain> :
        GenericValueAnalysis<AbstractDomain>
        where AbstractDomain : INumericalAbstractDomain<BoxedExpression>
      {
        #region Constructor
        internal GenericNumericalAnalysis
        (
          string methodName,
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions> mdriver,
          IValueAnalysisOptions options
        )
          : base(methodName, mdriver, options)
        {  
          
          #region Parameters initialisation for SubPolyhedra

          switch (options.Algorithm)
          {
            case ReductionAlgorithm.Fast:
              SubPolyhedra<BoxedExpression>.Algorithm = SubPolyhedra<BoxedExpression>.ReductionAlgorithm.Fast;
              break;
            case ReductionAlgorithm.Simplex:
              SubPolyhedra<BoxedExpression>.Algorithm = SubPolyhedra<BoxedExpression>.ReductionAlgorithm.Simplex;
              break;

            case ReductionAlgorithm.SimplexOptima:
              SubPolyhedra<BoxedExpression>.Algorithm = SubPolyhedra<BoxedExpression>.ReductionAlgorithm.SimplexOptima;
              break;

            case ReductionAlgorithm.Complete:
              SubPolyhedra<BoxedExpression>.Algorithm = SubPolyhedra<BoxedExpression>.ReductionAlgorithm.Complete;
              break;
            default:
              SubPolyhedra<BoxedExpression>.Algorithm = SubPolyhedra<BoxedExpression>.ReductionAlgorithm.Fast;
              break;
          }

          SubPolyhedra<BoxedExpression>.StrongPrecision = options.UseMorePreciseTransferFunction;
          if (options.Use2DConvexHull)
          {
            if (options.InferOctagonConstraints)
            {
              SubPolyhedra<BoxedExpression>.Inference = SubPolyhedra<BoxedExpression>.JoinConstraintInference.CHOct;
            }
            else
            {
              SubPolyhedra<BoxedExpression>.Inference = SubPolyhedra<BoxedExpression>.JoinConstraintInference.ConvexHull2D;
            }
          }
          else
          {
            if (options.InferOctagonConstraints)
            {
              SubPolyhedra<BoxedExpression>.Inference = SubPolyhedra<BoxedExpression>.JoinConstraintInference.Octagons;
            }
            else
            {
              SubPolyhedra<BoxedExpression>.Inference = SubPolyhedra<BoxedExpression>.JoinConstraintInference.Standard;
            }
          }
          #endregion
        }
        #endregion

        #region Abstract Methods
        abstract override protected internal AbstractDomain InitialValue 
        { 
          get; 
        }

        abstract override protected ProofObligations<Local, Parameter, Method, Field, Property, Type, Variable, ExternalExpression, Attribute, Assembly, BoxedExpression, ProofObligationBase<BoxedExpression, Variable>> Obligations
        {
          get;
        }

        override sealed public void ValidateImplicitAssertions(IOutputResults output)
        {
          if (this.options.NoProofObligations)
          {
            return;
          }

          this.Obligations.Validate(output, this.FactQuery);
        }


        #endregion

        #region Common code

        /// <summary>
        /// Assume a.length >= 0 forall the arrays a
        /// </summary>
        public override AbstractDomain Entry(APC pc, Method method, AbstractDomain data)
        {
          foreach (var param in this.DecoderForMetaData.Parameters(method).Enumerate())
          {
            Variable symb;
            if (this.Context.TryParameterValue(Context.Post(pc), param, out symb))
            {
              var typeForParam = this.Context.GetType(Context.Post(pc), symb);

              Variable symb_Length;

              if (typeForParam.IsNormal && this.DecoderForMetaData.IsArray(typeForParam.Value) && this.Context.TryGetArrayLength(Context.Post(pc), symb, out symb_Length))
              {
                data = (AbstractDomain)data.TestTrueGeqZero(ToBoxedExpression(Context.Post(pc), symb_Length));
              }
            }
          }

          return data;
        }

        public override AbstractDomain Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Variable source, AbstractDomain data)
        {
          data = base.Unary(pc, op, overflow, unsigned, dest, source, data);

          switch (op)
          {
            case UnaryOperator.Conv_i:
            case UnaryOperator.Conv_i1:
            case UnaryOperator.Conv_i2:
            case UnaryOperator.Conv_i4:
            case UnaryOperator.Conv_i8:
            case UnaryOperator.Conv_r_un:
            case UnaryOperator.Conv_r4:
            case UnaryOperator.Conv_r8:
            case UnaryOperator.Conv_u:
            case UnaryOperator.Conv_u1:
            case UnaryOperator.Conv_u2:
            case UnaryOperator.Conv_u4:
            case UnaryOperator.Conv_u8:           
            ALog.Message(StringClosure.For("Assuming {0} == (" + op + ") {1}", 
                new StringClosure[] { StringClosure.For(ToBoxedExpression(pc, dest)), StringClosure.For(ToBoxedExpression(pc, source)) } ));

            Type dummyType;
            object dummyValue;

              // F: We do not want to assume constant conversions to constants.
              // In general, we assume that this will be already handled by the underlying Interval domain, so we do this check to speed up the analysis
            if (!this.Context.IsConstant(pc, source, out dummyType, out dummyValue))
            {
              return (AbstractDomain)data.TestTrueEqual(ToBoxedExpression(pc, dest), ToBoxedExpression(pc, source));
            }

            return data;

            default:
              return data;
          }
        }

        /// <summary>
        /// Assumes that dest >= 0
        /// </summary>
        public override AbstractDomain Ldlen(APC pc, Variable dest, Variable array, AbstractDomain data)
        {
          AbstractDomain result;

          BoxedExpression refinedDest = ToBoxedExpression(pc, dest);

          ALog.BeginTransferFunction(StringClosure.For("Ldlen"), 
            ExpressionPrinter.ToStringClosure(refinedDest, decoderForExpressions),
            PrettyPrintPC(pc), StringClosure.For(data));

          result = (AbstractDomain) data.TestTrueGeqZero(refinedDest);

          ALog.EndTransferFunction(StringClosure.For(data));

          return result;
        }

        /// <summary>
        /// Here we encode a more refined transfer function for some common methods (e.g. the length of a string is >= 0).
        /// In the future this is doomed to became smaller, and hopefully disappear, when contracts will be everywhere ...
        /// </summary>
        public override AbstractDomain Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, AbstractDomain data)
          // where TypeList : IIndexable<Type> 
          // where ArgList : IIndexable<Variable>
        {
          ALog.BeginTransferFunction(StringClosure.For("Call"), StringClosure.For(this.DecoderForMetaData.Name(method)),
            PrettyPrintPC(pc), StringClosure.For(data));

          AbstractDomain result = base.Call(pc, method, tail, virt, extraVarargs, dest, args, data);

          if (this.MethodReturnsNonNegative(method, this.DecoderForMetaData))
          {
            result = (AbstractDomain) result.TestTrueGeqZero(ToBoxedExpression(pc, dest));
          }

          var methodname = this.DecoderForMetaData.FullName(method);

          // Ad-hoc handling of functions in System.Math
          if(this.IsCallToMethodInMath(methodname))
          {
            result = HandleMathematicalFunction(pc, method, tail, virt, extraVarargs, dest, args, data);
          }

          // Ad-hoc handling of multi-dimensional arrays
          if (this.IsCallToMethodInArray(methodname))
          {
            result = HandleArrayFunction(pc, method, tail, virt, extraVarargs, dest, args, data);
          }

          if (methodname.Equals("System.Collections.ICollection.get_Count"))
          {
            // If it is an array ...
            var type = this.Context.GetType(pc, args[0]);
            Variable len;
            if (type.IsNormal && this.DecoderForMetaData.IsArray(type.Value) 
              && this.Context.TryGetArrayLength(this.Context.Post(pc), args[0], out len))
            {
              var lenAsBoxed = ToBoxedExpression(pc, len);
              var destAsBoxed = ToBoxedExpression(pc, dest);

              result = (AbstractDomain) data.TestTrueEqual(lenAsBoxed, destAsBoxed);
            }

          }

          if (this.options.LogOptions.ShowInvariants && this.IsCallToShowInvariant(this.DecoderForMetaData.FullName(method))) 
          {
            ShowInvariant(pc, method, tail, virt, extraVarargs, dest, args, data);
          }

          ALog.EndTransferFunction(StringClosure.For(result));

          return result;
        }


        public override AbstractDomain Starg(APC pc, Parameter argument, Variable source, AbstractDomain data)
        {
          Variable sv;
          if (this.Context.TryParameterValue(pc, argument, out sv))
          {
            Variable len1, len2;
            if (this.Context.TryGetArrayLength(this.Context.Post(pc), source, out len1)
              && this.Context.TryGetArrayLength(this.Context.Post(pc), sv, out len2))
            {
              var be1 = ToBoxedExpression(pc, len1);
              var be2 = ToBoxedExpression(pc, len2);

              data = (AbstractDomain) data.TestTrueEqual(be1, be2);
            }
          }

          return data;
        }

        #endregion

        #region Handling for Mathematical functions
        private AbstractDomain HandleMathematicalFunction<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, AbstractDomain data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<Variable>
        {
          Debug.Assert(this.DecoderForMetaData.FullName(method).StartsWith("System.Math."));

          string methodName = this.DecoderForMetaData.Name(method);

          BoxedExpression destAsExp = ToBoxedExpression(pc, dest);
          BoxedExpression leftAsExp, rightAsExp; 

          switch (methodName)
          {
            case "Min":
              leftAsExp = ToBoxedExpression(pc, args[0]);
              rightAsExp = ToBoxedExpression(pc, args[1]);
              return HandleMin(pc, destAsExp, leftAsExp, rightAsExp, data);

            case "Max":
              leftAsExp = ToBoxedExpression(pc, args[0]);
              rightAsExp = ToBoxedExpression(pc, args[1]);
              return HandleMax(pc, destAsExp, leftAsExp, rightAsExp, data);
       
            //case "Sign":
            //  leftAsExp = ToBoxedExpression(pc, args[0]);
            //  return HandleSign(pc, destAsExp, leftAsExp, data);

            default:
              return data; 
          }
        }

        private AbstractDomain HandleSign(APC pc, BoxedExpression destAsExp, BoxedExpression leftAsExp, AbstractDomain data)
        {
          var bound = data.BoundsFor(leftAsExp);

          if (bound.IsNormal())
          {
            if (bound.IsSingleton && bound.LowerBound.IsZero)
            {
              data.AssignInterval(destAsExp, Interval.For(0));
            }
            else if(bound.LowerBound > 0)
            {
              data.AssignInterval(destAsExp, Interval.For(1));
            }
            else if (bound.LowerBound >= 0)
            {
              data.AssignInterval(destAsExp, Interval.For(0, 1));
            }
            else if (bound.UpperBound < 0)
            {
              data.AssignInterval(destAsExp, Interval.For(-1));
            }
            else if (bound.UpperBound <= 0)
            {
              data.AssignInterval(destAsExp, Interval.For(-1, 0));
            }
          }
          
            return data;
          
        }

        private AbstractDomain HandleMin(APC pc, BoxedExpression dest, BoxedExpression left, BoxedExpression right, AbstractDomain data)
        {
          AbstractDomain clonedLeft = (AbstractDomain)data.Clone();
          AbstractDomain clonedRight = (AbstractDomain)data.Clone();

          // case 1. left <= right => dest = left
          clonedLeft = (AbstractDomain)clonedLeft.TestTrueLessEqualThan(left, right);
          clonedLeft = (AbstractDomain)clonedLeft.TestTrue(BoxedExpression.Binary(BinaryOperator.Ceq, dest, left));

          // case 2. left > right => dest = right
          clonedRight = (AbstractDomain)clonedRight.TestTrueLessThan(right, left);
          clonedRight = (AbstractDomain)clonedRight.TestTrue(BoxedExpression.Binary(BinaryOperator.Ceq, dest, right));

          AbstractDomain result = (AbstractDomain)clonedLeft.Join(clonedRight);

          ALog.Message(StringClosure.For("Join of" + Environment.NewLine + "{0} and" + Environment.NewLine + "{1} is" + Environment.NewLine + "{2}", StringClosure.For(clonedLeft), StringClosure.For(clonedRight), StringClosure.For(result)));

          // We add some well known facts, as the join below may lose some precision (or because we do not want to perform an expensive reduction in the underlying domains)
          // dest <= left
          result = (AbstractDomain)result.TestTrue(BoxedExpression.Binary(BinaryOperator.Cle, dest, left));

          // dest <= right 
          result = (AbstractDomain)result.TestTrue(BoxedExpression.Binary(BinaryOperator.Cle, dest, right));

          ALog.Message(StringClosure.For("State after the hard-coded transfer functions {0}", StringClosure.For(result)));

          return result;
        }

        private AbstractDomain HandleMax(APC pc, BoxedExpression dest, BoxedExpression left, BoxedExpression right, AbstractDomain data)
        {
          AbstractDomain clonedLeft = (AbstractDomain)data.Clone();
          AbstractDomain clonedRight = (AbstractDomain)data.Clone();

          // case 1. left <= right => dest = right
          clonedLeft = (AbstractDomain)clonedLeft.TestTrueLessEqualThan(left, right);
          clonedLeft = (AbstractDomain)clonedLeft.TestTrue(BoxedExpression.Binary(BinaryOperator.Ceq, dest, right));

          // case 2. left > right => dest = left
          clonedRight = (AbstractDomain)clonedRight.TestTrueLessThan(right, left);
          clonedRight = (AbstractDomain)clonedRight.TestTrue(BoxedExpression.Binary(BinaryOperator.Ceq, dest, left));

          return (AbstractDomain)clonedLeft.Join(clonedRight);
        }

        #endregion

        #region Handling for Array functions
        private AbstractDomain HandleArrayFunction<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, AbstractDomain data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<Variable>
        {
          Debug.Assert(this.DecoderForMetaData.FullName(method).StartsWith("System.Array"));

          string methodName = this.DecoderForMetaData.Name(method);

          switch (methodName)
          {
            case "GetUpperBound":
              {
                // We want to have x.GetUpperBound(0) to be x.Length-1, when x is of type T[]              
                var type = this.Context.GetType(pc, args[0]);
                Variable arrayLength;

                if (type.IsNormal && this.DecoderForMetaData.IsArray(type.Value)
                  && this.Context.IsZero(pc, args[1])
                  && this.Context.TryGetArrayLength(pc, args[0], out arrayLength)
                  )
                {
                  var destAsExp = ToBoxedExpression(pc, dest);
                  var arrayLengthAsExp = ToBoxedExpression(pc, arrayLength);

                  // constraint is "destAsExp == arrayLengthAsExp -1"
                  var constraint = BoxedExpression.Binary(BinaryOperator.Ceq,
                    destAsExp,
                    BoxedExpression.Binary(BinaryOperator.Sub,
                    arrayLengthAsExp,
                    BoxedExpression.Const(1, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData)));

                  data = (AbstractDomain)data.TestTrue(constraint);
                }
                return data;
              }

            case "get_Rank":
              {
                var type = this.Context.GetType(pc, args[0]);

                if (type.IsNormal && this.DecoderForMetaData.IsArray(type.Value))
                {
                  int rank = this.DecoderForMetaData.Rank(type.Value);

                  // Assume dest == rank
                  data = (AbstractDomain) data.TestTrueEqual(ToBoxedExpression(pc, dest), BoxedExpression.Const(rank, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));
                }

                return data;
              }

            default:
              return data;
          }
        }



        #endregion

        #region Postcondition propagation and printing
        override public void SuggestPostcondition(IOutputResults output)
        {
          // TODO: avoid adding postconditions if the method already has similar

          BoxedExpression bottomCondition;
          if (TrySuggestBottomCondition(out bottomCondition))
          {
            UseInferredPostconditions(new List<BoxedExpression>() { bottomCondition }, output);

            // We are done
            return;
          }

          var postconditions = new List<BoxedExpression>();

          if (!this.DecoderForMetaData.IsVoidMethod(this.MethodDriver.CurrentMethod))
          {
            // Easy postcondition,
            // We keep it here because of we want to get bounds on expressions that can be refined but are not in the domain            
            SuggestIntervalPostcondition(postconditions);

            // Can we read the return expression in the PostState?
            BoxedExpression postExpression;
            if (SuggestExpressionPostcondition(out postExpression))
            {
              postconditions.Add(postExpression);
            }
          }

          SuggestPostconditionsFromReturnState(postconditions);

          SuggestPostconditionsFromParameters(postconditions);

          var reduced_postconditions = ReducePostconditions(postconditions);

          UseInferredPostconditions(reduced_postconditions, output);
        }

        private AbstractDomain StateForPostCondition()
        {
          AbstractDomain post;

          if (this.fixpointInfo.TryAStateForPostCondition(this.InitialValue, out post))
          {
            // ok
            return post;
          }

          return this.InitialValue;
        }

        private bool SuggestPostconditionsFromParameters(List<BoxedExpression> postconditions)
        {
           var expInPostState = 
             new BoxedExpressionReader<APC, Local, Parameter, Method, Field, Property, Type, Variable, ExternalExpression, Attribute, Assembly>(this.Context, this.DecoderForMetaData);

           bool added = false;

           foreach (Parameter p in this.DecoderForMetaData.Parameters(this.Context.CurrentMethod).Enumerate())
           {            
             Variable varForp;
             Variable paramAddr;
             if (!this.Context.TryParameterAddress(this.Context.NormalExit, p, out paramAddr)) { continue; }
             if (!this.Context.TryLoadIndirect(this.Context.NormalExit, paramAddr, out varForp)) { continue; }

             FList<PathElement> pathForp = this.Context.AccessPathList(this.Context.NormalExit, varForp);

             if(!this.Context.PathUnmodifiedSinceEntry(this.Context.NormalExit, pathForp))
             {
               continue;
             }
             
             Details details;
             BoxedExpression expForp = expInPostState.ExpressionInPostState(
               ToBoxedExpression(this.Context.NormalExit, varForp), false, out details);

             if (!details.HasVariables|| expForp == null)
             {
               continue;
             }

             foreach (var path in this.Context.AccessPaths(this.Context.NormalExit, varForp))
             {
               if (this.Context.IsRootedInParameter(path) && path.Length()>2)
               {
                 BoxedExpression right = BoxedExpression.Var(path , path);
                 BoxedExpression eq = BoxedExpression.Binary(BinaryOperator.Ceq, expForp, right);

                 postconditions.Add(eq);
                 added = true;
               }
             }
           }

          return added;
        }

        /// <summary>
        /// Outputs the postconditions on <code>output</code>, and on the VS Pex suggestion window
        /// </summary>
        private void UseInferredPostconditions(List<BoxedExpression> postconditions, IOutputResults output)
        {
          bool isCurrentMethodAProperty = this.DecoderForMetaData.IsPropertyGetterOrSetter(this.Context.CurrentMethod);

          bool propagateInferredEnsures = 
            output.LogOptions.PropagateInferredEnsures(isCurrentMethodAProperty);

          bool propagateInferredRequires =
            output.LogOptions.PropagateInferredRequires(isCurrentMethodAProperty);

          // This un-necessary test is here just to speed up the analysis
          if (!output.LogOptions.SuggestEnsures(isCurrentMethodAProperty) && !propagateInferredEnsures)
          {
            return;
          }


          foreach (BoxedExpression be in postconditions)
          {
            // Can we read be as a precondition? 
            bool hasVariables, hasAccessPath;
            var beAsPrecondition 
              = PreconditionSuggestion.ExpressionInPreState(be, this.Context, this.DecoderForMetaData, out hasVariables, out hasAccessPath, this.Context.NormalExit);

            // If we can read be in the prestate, then we do not need to suggest it as a postcondition
            if (beAsPrecondition != null && hasVariables && output.LogOptions.InferPreconditionsFromPostconditions)
            {
              // If the precondition is already implied by the entry state, we simply discard it
              AbstractDomain adom;
              if (PreState(this.Context.EntryAfterRequires, out adom))
              {
                if (adom.CheckIfHolds(beAsPrecondition).IsTop)
                {
                  SuggestedCodeFixes.AddPrecondition(this.Context, beAsPrecondition.ToString(),
                    SuggestedCodeFixes.SUGGESTED_PRECONDITION + 2, "Suggested precondition", output, this.DecoderForMetaData);

                  if (propagateInferredRequires)
                  {
                    this.MethodDriver.AddPreCondition(beAsPrecondition, beAsPrecondition.ToString(), this.Context.EntryAfterRequires);
                  }
                }
              }
            }           

            if(beAsPrecondition == null || !hasVariables || hasAccessPath)            
            {
              SuggestedCodeFixes.AddPostCondition(this.Context, be.ToString(),
                SuggestedCodeFixes.SUGGESTED_POSTCONDITION, "Suggested postcondition", output, this.DecoderForMetaData);

              if (propagateInferredEnsures)
              {
                this.MethodDriver.AddPostCondition(be, be.ToString(), this.Context.NormalExit);
              }
            }
          }
        }

        /// <summary>
        /// Filters the postconditions, trying to find a core set
        /// </summary>
        private List<BoxedExpression> ReducePostconditions(List<BoxedExpression> postconditions)
        {
          // Heuristic: we sort postconditions according to their relation symbol
          var sort_postconditions = SortPostconditions(postconditions);
          var result = new List<BoxedExpression>(postconditions.Count);
          var adom = this.InitialValue; 

          Variable returnVariable;
          BoxedExpression returnExpression;
          if (this.Context.TryResultValue(out returnVariable))
          {
            returnExpression = BoxedExpression.Result(this.DecoderForMetaData.ReturnType(this.Context.CurrentMethod));
          }
          else
          {
            returnExpression = null;
          }

          var stateForPostconditions = StateForPostCondition();
          var checkWithPosts = !stateForPostconditions.IsTop;

          foreach (BoxedExpression exp_iter in sort_postconditions)
          {
            var exp = exp_iter;
            if (IsTrivialPostcondition(ref exp))
            {
              continue;
            }

            if (returnExpression != null)
            {
              var be = exp_iter.Substitute(returnExpression, ToBoxedExpression(this.Context.NormalExit, returnVariable));

              // F: I hate this special case below. 
              // I should think to a better way of avoiding removing Contract.Result == exp
              if (checkWithPosts && !CommonChecks.CheckTrivialEquality(be, this.decoderForExpressions))
              {
                // the postconditions already there prove it
                if (stateForPostconditions.CheckIfHolds(be).IsTrue())
                {
                  continue;
                }
              }
            }

            if (adom.CheckIfHolds(exp).IsTop)
            {
              adom.TestTrue(exp);
              result.Add(exp);
            }
          }

          return result;
        }

        /// <summary>
        /// Heuristics to filter out trivial postconditions
        /// </summary>
        private bool IsTrivialPostcondition(ref BoxedExpression exp)
        {
          Debug.Assert(exp != null);

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
                  return IsOfUnsignedType(left);

                case BinaryOperator.Cne_Un:
                case BinaryOperator.Ceq:
                  return Specialize(bop, left, ref exp);

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
                  return IsOfUnsignedType(right);

                case BinaryOperator.Cne_Un:
                case BinaryOperator.Ceq:
                return Specialize(bop, right, ref exp);

                default:
                  // do nothing;
                  break;
              }
            }
          }

          return false;
        }

        private static bool IsConstantZero(BoxedExpression exp)
        {
          return exp.IsConstant && exp.Constant is Int32 && ((Int32)exp.Constant == 0);
        }

        /// <summary> 
        /// Eval the expression bop(left, 0)
        /// </summary>
        /// <returns>true iff it is a tautology</returns>
        private bool Specialize(BinaryOperator bop, BoxedExpression left, ref BoxedExpression exp)
        {
          Debug.Assert(left != null);
          Debug.Assert(exp != null);
          Debug.Assert(!left.Equals(exp));

          BinaryOperator bop_left;
          BoxedExpression left1, left2;

          // Special case to check if "this != 0", and hence remove the tautology
          if (bop == BinaryOperator.Cne_Un && left.IsVariable && left.ToString() == "this")
          {
            exp = null;
            return true;
          }

          if(left.IsBinaryExpression(out bop_left, out left1, out left2))
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

        private BinaryOperator Not(BinaryOperator bop)
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

        private bool IsOfUnsignedType(BoxedExpression exp)
        {
          if (!exp.IsVariable)
            return false;

          var type = this.Context.GetType(this.Context.NormalExit, (Variable) exp.Variable);

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
        private List<BoxedExpression> SortPostconditions(List<BoxedExpression> postconditions)
        {
          var eq = new List<BoxedExpression>();
          var strict = new List<BoxedExpression>();
          var non_strict = new List<BoxedExpression>();
          var others = new List<BoxedExpression>();

          foreach (var exp in postconditions)
          {
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

        /// <summary>
        /// If the exist state is unreachable, it returns a boxed expression for "false"
        /// </summary>
        private bool TrySuggestBottomCondition(out BoxedExpression bottomCondition)
        {
          AbstractDomain astate;
          if (!PreState(this.Context.NormalExit, out astate))
          {
            bottomCondition = BoxedExpression.Const(false, this.DecoderForMetaData.System_Boolean, this.DecoderForMetaData);
            return true;
          }

          bottomCondition = null;
          return false;
        }

        private bool SuggestPostconditionsFromReturnState(List<BoxedExpression> expressions)
        {
          Debug.Assert(expressions != null);

          AbstractDomain astate;
          if (PreState(this.Context.NormalExit, out astate))
          {
            // Use a copy
            astate = (AbstractDomain) astate.Clone();
            var expInPostState
              = new BoxedExpressionReader<APC, Local, Parameter, Method, Field, Property, Type, Variable, ExternalExpression, Attribute, Assembly>(this.Context, this.DecoderForMetaData);
  
            // Project all the variables which are not visible in the poststate
            foreach (BoxedExpression var in astate.Variables)
            {
              Details details;
              BoxedExpression asBE = expInPostState.ExpressionInPostState(var, true, true, out details);
              if (asBE == null || !details.HasInterestingVariables)
              {
                astate.RemoveVariable(var);
              }
            }

            if (!astate.IsTop && !astate.IsBottom)
            {
              expressions.AddRange(ToListOfBoxedExpressions(astate, expInPostState));

              return true;
            }
          }

          return false;
        }

        private List<BoxedExpression> ToListOfBoxedExpressions(AbstractDomain astate, 
          BoxedExpressionReader<APC, Local, Parameter, Method, Field, Property, Type, Variable, ExternalExpression, Attribute, Assembly> expInPostState)
        {
          var expressions = new List<BoxedExpression>(); 
          var be = astate.To(new BoxedExpressionFactory<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>(this.DecoderForMetaData));
          var beSplitted = be.SplitConjunctions();

          foreach (var exp in beSplitted)
          {
            Details details;
            var post = 
              expInPostState.ExpressionInPostState(exp.MakeItPrettier(this.decoderForExpressions, this.Encoder), true, true, out details);
            if (post != null 
              && !post.IsConstantTrue()
              && (!details.HasOldVariable || details.HasCompoundExp)) // HasOldVariable ==> HasCompoundExp
            {
              expressions.Add(post);
            }
          }

          return expressions;
        }


        /// <summary>
        /// Try to suggest an interval for the return valued of the method
        /// </summary>
        /// <param name="postcondition">Should be not null</param>
        protected bool SuggestIntervalPostcondition(List<BoxedExpression> postconditions)
        {
          Debug.Assert(postconditions != null);

          AbstractDomain adomain;
          if (PreState(this.Context.NormalExit, out adomain))
          {
            Variable retVar;
            if (this.Context.TryResultValue(out retVar))
            {
              #region Intervals
              Interval retValue = adomain.BoundsFor(BoxedExpression.Var(retVar));

              Type retType = this.MethodDriver.MetaDataDecoder.ReturnType(this.MethodDriver.CurrentMethod);

              // Not all the values are interesting for all the types, so we filter them
              if (IsInterestingPostcondition(retType, retValue))
              {
                if (retValue.IsBottom)
                {
                  // "false"
                  postconditions.Add(BoxedExpression.Const(false, this.DecoderForMetaData.System_Boolean, this.DecoderForMetaData));
                }
                // Some smarter pretty printing
                else if (retValue.IsSingleton)
                {
                  // ret == retValue
                  BoxedExpression eq = BoxedExpression.Binary(BinaryOperator.Ceq, BoxedExpression.Result(retType), ToBoxedExpression(retValue.LowerBound));
                  postconditions.Add(eq);
                }
                else
                {
                  if (!retValue.LowerBound.IsInfinity)
                  {
                    // ret >= retValue.LowerBound
                    BoxedExpression geq = BoxedExpression.Binary(BinaryOperator.Cge, BoxedExpression.Result(retType), ToBoxedExpression(retValue.LowerBound));
                    postconditions.Add(geq);
                  }

                  if (!retValue.UpperBound.IsInfinity)
                  {
                    // ret <= retValue.UpperBound
                    BoxedExpression leq = BoxedExpression.Binary(BinaryOperator.Cle, BoxedExpression.Result(retType), ToBoxedExpression(retValue.UpperBound));
                    postconditions.Add(leq);
                  }

                  Debug.Assert(postconditions.Count > 0);
                }
                #endregion

                #region Symbolic upper bounds

                Set<BoxedExpression> upp = adomain.UpperBoundsFor(BoxedExpression.Var(retVar), true);

                if (upp.Count > 0)
                {
                  var expInPostState = new BoxedExpressionReader<APC, Local, Parameter, Method, Field, Property, Type, Variable, ExternalExpression, Attribute, Assembly>(this.Context, this.DecoderForMetaData);

                  foreach (BoxedExpression be in upp)
                  {
                    Details details;
                    BoxedExpression tryBe = expInPostState.ExpressionInPostState(be, false, out details);

                    if (tryBe != null && details.HasVariables)
                    {
                      BoxedExpression newExp = BoxedExpression.Binary(BinaryOperator.Clt, BoxedExpression.Result(retType), tryBe);

                      postconditions.Add(newExp);
                    }
                  }
                }

                #endregion

                return true;
              }
            }
          }

          return false;
        }

        /// <summary>
        /// Try to read the expression for the result in the poststate. 
        /// </summary>
        protected bool SuggestExpressionPostcondition(out BoxedExpression postcondition)
        {
          BoxedExpressionReader<APC, Local, Parameter, Method, Field, Property, Type, Variable, ExternalExpression, Attribute, Assembly> expInPostState
            = new BoxedExpressionReader<APC, Local, Parameter, Method, Field, Property, Type, Variable, ExternalExpression, Attribute, Assembly>(this.Context, this.DecoderForMetaData);
          
          Variable retVar;
          if (this.Context.TryResultValue(out retVar))
          {
            BoxedExpression retExp = ToBoxedExpression(this.Context.NormalExit, retVar);

            
            Details details;

            postcondition = expInPostState.ExpressionInPostState(retExp, false, true, out details);

            if (postcondition == null /*|| !hasVariable*/)
            {
              return false;
            }
            else
            {
              Type retType = this.MethodDriver.MetaDataDecoder.ReturnType(this.MethodDriver.CurrentMethod);
              postcondition  = BoxedExpression.Binary(BinaryOperator.Ceq, BoxedExpression.Result(retType), postcondition);

              return true;
            }
          }

          postcondition = null;
          return false;
        }

        /// <returns>
        /// true iff the interval value is interesting for the type
        /// </returns>
        protected bool IsInterestingPostcondition(Type type, Interval value)
        {
          if (value.IsTop)
          {
            return false;
          }

          if (type.Equals(this.MethodDriver.MetaDataDecoder.System_Boolean))
          {
            return value.IsSingleton;   // If it is a singleton, we are happy, otherwise it is of no interest
          }

          return true;
        }
        #endregion

        #region Handling of invariant printing

        private void ShowInvariant<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, AbstractDomain data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<Variable>
        {

          IDictionary<string, string> renamings = new Dictionary<string, string>();
          
          for(int i= 0; i < args.Count; i++)
          {
            string tryAccessPath = this.Context.AccessPath(pc, args[i]);

            string key  = args[i].ToString();

            ALog.Message(StringClosure.For("{0} -> {1}" + Environment.NewLine , StringClosure.For(key), StringClosure.For(tryAccessPath)));  

            renamings[key] = tryAccessPath != null? tryAccessPath : string.Format("_Param#{0}", i);
          }

          SimpleInfixStringsFactory factory = new SimpleInfixStringsFactory(renamings);

          string dataAsString = data.To(factory);

          Console.WriteLine(" <<< Invariant @ line {0}", this.Context.SourceStartLine(pc));

          Console.WriteLine(dataAsString);

          Console.WriteLine(" >>> ");
        }


        #endregion

        #region Helpers

        /// <summary>
        /// A built-in contract that specifies that the getter Count returns a value >= 0.
        /// One day they will disappear ...
        /// </summary>
        protected bool MethodReturnsNonNegative(Method method, IDecodeMetaData<Local,Parameter,Method,Field,Property,Type,Attribute,Assembly> mdDecoder)
        {
          Type declaringType = mdDecoder.DeclaringType(method);
          if (mdDecoder.Equal(declaringType, mdDecoder.System_String))
          {
            return mdDecoder.Name(method) == "get_Length";
          }
          if (mdDecoder.FullName(declaringType).StartsWith("System.Collections"))
          {
            return mdDecoder.Name(method).EndsWith("get_Count");
          }
          return false;
        }

        /// <returns>
        /// true iff p starts with System.Math.
        /// </returns>
        private bool IsCallToMethodInMath(string p)
        {
          return p.StartsWith("System.Math.");
        }

        private bool IsCallToMethodInArray(string methodname)
        {
          return  methodname.StartsWith("System.Array");
        }

        private bool IsCallToShowInvariant(string p)
        {
          return p.StartsWith("SageProofOfConcept.ShowInvariant.Here");
        }

        #endregion
      }        

      #region All the Proof obligations

      static ProofOutcome ToProofOutcome(FlatAbstractDomain<bool> result)
      {
        if (result.IsBottom)
        {
          return ProofOutcome.Bottom;
        }
        else if (result.IsTop)
        {
          return ProofOutcome.Top;
        }
        else if (result.BoxedElement == true)
        {
          return ProofOutcome.True;
        }
        else if (result.BoxedElement == false)
        {
          return ProofOutcome.False;
        }
        else
        {
          throw new AbstractInterpretationException("Impossible result?");
        }
      }

      abstract internal class ProofObligationWithDecoder : ProofObligationBase<BoxedExpression, Variable>
      {
        private BoxedExpressionDecoder decoder;
        protected readonly IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions> MethodDriver;

        private static int precondititionsDiscovered = 0;

        protected ProofObligationWithDecoder(APC pc, BoxedExpressionDecoder decoder,
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions> mdriver
        )
          : base(pc)
        {
          Debug.Assert(mdriver != null);

          this.decoder = decoder;
          this.MethodDriver = mdriver;
        }

        protected BoxedExpressionDecoder DecoderForExpressions
        {
          get
          {
            return this.decoder;
          }
        }

        protected IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> Context
        {
          get
          {
            return this.MethodDriver.Context;
          }
        }

        protected IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> DecoderForMetaData
        {
          get
          {
            return this.MethodDriver.MetaDataDecoder;
          }
        }

        static public int PreconditionsDiscovered
        {
          set { precondititionsDiscovered = value; }
          get { return precondititionsDiscovered; }
        }

        protected bool TryDischargeProofObligationWithWeakestPreconditions(BoxedExpression condition, IFactQuery<BoxedExpression, Variable> query, IOutputResults output)
        {
          var options = output.LogOptions;

          // Try to validate the assertion by WP inference
          if (options.UseWeakestPreconditions)
          {
            var path = WeakestPreconditionProver.Discharge(this.PC, condition, options.MaxPathSize, this.MethodDriver, query);
            return path == null;
          }
          return false;
        }

      }

      #endregion

      #region The visitor for ContainsNull
      private class Void
      {
        static Void Value { get { return null; } }
      }

      private class VisitorForExpContainsNull : GenericExpressionVisitor<Void, bool, BoxedExpression>
      {
        public VisitorForExpContainsNull(IExpressionDecoder<BoxedExpression>/*!*/ decoder)
          : base(decoder)
        { }

        public override bool VisitAnd(BoxedExpression left, BoxedExpression right, BoxedExpression original, Void data)
        {
          return Visit(left, data) || Visit(right, data);
        }

        public override bool VisitEqual(BoxedExpression left, BoxedExpression right, BoxedExpression original, Void data)
        {
          return Visit(left, data) || Visit(right, data);
        }

        public override bool VisitNot(BoxedExpression left, Void data)
        {
          return Visit(left, data);
        }

        public override bool VisitOr(BoxedExpression left, BoxedExpression right, BoxedExpression original, Void data)
        {
          return Visit(left, data) || Visit(right, data);
        }

        public override bool VisitConstant(BoxedExpression left, Void data)
        {
          return this.Decoder.IsNull(left);
        }

        protected override bool Default(Void data)
        {
          return false;
        }
      }

      #endregion

    }
  }
}

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

using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.AbstractDomains;
using System;
using Microsoft.Research.DataStructures;
using System.Collections.Generic;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics;
using Microsoft.Research.CodeAnalysis;
using System.Linq;

//using INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> = Microsoft.Research.AbstractDomains.INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;


// Shortcuts
using ADomainKind = Microsoft.Research.CodeAnalysis.Analyzers.DomainKind;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      /// <summary>
      /// The common superclass for the bounds and unsafe analyses
      /// </summary>
      public abstract class GenericNumericalAnalysis<Options> :
        GenericValueAnalysis<INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>, Options>
        where Options : Analyzers.ValueAnalysisOptions<Options>
      {               
        #region State

        protected bool HasSwitchedToAdaptativeMode { get; private set; }
        protected OnDemandMap<APC, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>> callSiteCache;
        protected Dictionary<Variable, Variable> BoxedVariables; // Hack, to remember which variables have been boxed and then unboxed
        
        readonly private ADomainKind abstractDomain;
        protected ADomainKind AbstractDomain
        {
          get
          {
            return this.abstractDomain;
          }
        }
        
        #endregion

        internal GenericNumericalAnalysis
        (
          string methodName,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          Options options,
          Predicate<APC> cachePCs
        )
          : base(methodName, mdriver, options, cachePCs)
        {
          this.HasSwitchedToAdaptativeMode = false;
          this.abstractDomain = options.Type;
          this.BoxedVariables = new Dictionary<Variable, Variable>();

#if DEBUG
          // This is only for regression, to test that the analyzer continues with other analyses even when a timeout exception is thrown
          if (options.CrashWithTimeOut)
          {
            throw new TimeoutExceptionFixpointComputation();
          }
#endif

          InitializeSubPolyhedra();
        }


        #region Initialization

        private void InitializeSubPolyhedra()
        {
          Contract.Assert(this.Options != null);

          switch (this.Options.Algorithm)
          {
            case ReductionAlgorithm.Fast:
              SubPolyhedra.Algorithm = SubPolyhedra.ReductionAlgorithm.Fast;
              break;
            
            case ReductionAlgorithm.Simplex:
              SubPolyhedra.Algorithm = SubPolyhedra.ReductionAlgorithm.Simplex;
              break;

            case ReductionAlgorithm.SimplexFast:
              SubPolyhedra.Algorithm = SubPolyhedra.ReductionAlgorithm.MixSimplexFast;
              break;

            case ReductionAlgorithm.SimplexOptima:
              SubPolyhedra.Algorithm = SubPolyhedra.ReductionAlgorithm.SimplexOptima;
              break;

            case ReductionAlgorithm.Complete:
              SubPolyhedra.Algorithm = SubPolyhedra.ReductionAlgorithm.Complete;
              break;

            default:
              SubPolyhedra.Algorithm = SubPolyhedra.ReductionAlgorithm.Fast;
              break;
          }

          if (this.Options.Use2DConvexHull)
          {
            if (this.Options.InferOctagonConstraints)
            {
              SubPolyhedra.Inference = SubPolyhedra.JoinConstraintInference.CHOct;
            }
            else
            {
              SubPolyhedra.Inference = SubPolyhedra.JoinConstraintInference.ConvexHull2D;
            }
          }
          else
          {
            if (this.Options.InferOctagonConstraints)
            {
              SubPolyhedra.Inference = SubPolyhedra.JoinConstraintInference.Octagons;
            }
            else
            {
              SubPolyhedra.Inference = SubPolyhedra.JoinConstraintInference.Standard;
            }
          }

          SubPolyhedra.MaxVariablesInOctagonsConstraintInference = this.Options.LogOptions.MaxVarsForOctagonInference;

          LinearEqualitiesEnvironment.AdaptiveRenamingInSubpolyhedraThreshold = this.Options.SubpolyhedraRenamingThreshold;

          // To help debugging
          this.Encoder.ResetFreshVariableCounter();
        }

        #endregion 

        #region Implementation of the overridden
        override public INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> GetTopValue()
        {
          Contract.Ensures(Contract.Result<INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>() != null);

          
          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> result;
          string reason;
          if (this.TryAdaptiveNumericalDomain(out result, out reason))
          {

            if (!this.HasSwitchedToAdaptativeMode)
            {
#if DEBUG
              Console.WriteLine("Info: The method {0} seems too complex (reason: {1}). Consider a refactoring simplyfying its structure",
                this.Context.MethodContext.CurrentMethod.ToString(),
                reason);

              Console.WriteLine("Method CFG is too complex. Overriding the default numerical abstract domain. Using Pentagons instead");
#endif
            }

            this.HasSwitchedToAdaptativeMode = true;

            return result;
          }

          #region 1. Build the initial abstract element
          switch (this.abstractDomain)
          {
            case ADomainKind.Intervals:
              {
                result = new IntervalEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
              }
              break;

            case ADomainKind.Disintervals:
              {
                result = new DisIntervalEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
              }
              break;

            case ADomainKind.Karr:
              {
                result = new LinearEqualitiesEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
              }
              break;

            case ADomainKind.Leq:
              {
                result = new WeakUpperBoundsEqual<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
              }
              break;

            case ADomainKind.Octagons:
              {
                result = new OctagonEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager, OctagonEnvironment<BoxedVariable<Variable>, BoxedExpression>.OctagonPrecision.FullPrecision);
              }
              break;

            case ADomainKind.Pentagons:
              {
                var intervalEnvironment = this.InitialIntervalAbstraction(this.Options.TrackDisequalities);
                var weakUpperBoundsEnvironment = new WeakUpperBounds<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);

                result = new Pentagons<BoxedVariable<Variable>, BoxedExpression>(intervalEnvironment, weakUpperBoundsEnvironment, this.ExpressionManager);
              }
              break;

            case ADomainKind.PentagonsKarr:
              {
                var intervalEnvironment = this.InitialIntervalAbstraction(this.Options.TrackDisequalities);
                var weakUpperBoundsEnvironment = new WeakUpperBounds<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
                var pentagonEnvironment = new Pentagons<BoxedVariable<Variable>, BoxedExpression>(intervalEnvironment, weakUpperBoundsEnvironment, this.ExpressionManager);
                var karrEnvironment = new LinearEqualitiesEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);

                result = new NumericalDomainWithKarr<BoxedVariable<Variable>, BoxedExpression>(karrEnvironment, pentagonEnvironment, this.ExpressionManager);
              }
              break;

            case ADomainKind.PentagonsKarrLeq:
              {
                var intervalEnvironment = this.InitialIntervalAbstraction(this.Options.TrackDisequalities);
                var weakUpperBoundsEnvironment = new WeakUpperBounds<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
                var pentagonEnvironment = new Pentagons<BoxedVariable<Variable>, BoxedExpression>(intervalEnvironment, weakUpperBoundsEnvironment, this.ExpressionManager);
                var karrEnvironment = new LinearEqualitiesEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
                var leq = new WeakUpperBoundsEqual<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
                var refinedPentagonEnvironment = new PentagonsPlus<BoxedVariable<Variable>, BoxedExpression>(leq, pentagonEnvironment, this.ExpressionManager);

                result = new NumericalDomainWithKarr<BoxedVariable<Variable>, BoxedExpression>(karrEnvironment, refinedPentagonEnvironment, this.ExpressionManager);
              }
              break;

            case ADomainKind.PentagonsKarrLeqOctagons:
              {
                var intervalEnvironment = this.InitialIntervalAbstraction(this.Options.TrackDisequalities);
                var weakUpperBoundsEnvironment = new WeakUpperBounds<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
                var pentagonEnvironment = new Pentagons<BoxedVariable<Variable>, BoxedExpression>(intervalEnvironment, weakUpperBoundsEnvironment, this.ExpressionManager);
                var karrEnvironment = new LinearEqualitiesEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
                var leq = new WeakUpperBoundsEqual<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
                var refinedPentagonEnvironment = new PentagonsPlus<BoxedVariable<Variable>, BoxedExpression>(leq, pentagonEnvironment, this.ExpressionManager);

                var octagonEnvironment = new OctagonEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager, OctagonEnvironment<BoxedVariable<Variable>, BoxedExpression>.OctagonPrecision.JustTests);

                result = new RefinedWithOctagons<INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>(octagonEnvironment, refinedPentagonEnvironment, this.ExpressionManager);
              }
              break;

            case ADomainKind.SubPolyhedra:
              {
                var karrSubpolyEnvironment = new LinearEqualitiesForSubpolyhedraEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
                var intervals = new IntervalEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
                result = new SubPolyhedra<BoxedVariable<Variable>, BoxedExpression>(karrSubpolyEnvironment, intervals, this.ExpressionManager);
              }
              break;

#if POLYHEDRA
              case ADomainKind.Polyhedra:
                PolyhedraEnvironment<BoxedExpression>.Init(this.Decoder, this.Encoder);

                result = new PolyhedraEnvironment<BoxedExpression>(this.Decoder, this.Encoder);
                break;
#endif
            case ADomainKind.WeakUpperBounds:
              {
                result = new WeakUpperBounds<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
              }
              break;

            default:
              throw new AbstractInterpretationException(string.Format("Unknown abstract domain : {0}", this.abstractDomain));
          }

          #endregion

          #region 2. Refine the domain according to command line options

          // F: TODO integrate subpolyhedra with DisIntervals
          if (this.Options.TrackDisequalities)
          {
            if (this.abstractDomain == Analyzers.DomainKind.SubPolyhedra)
            {
              var simpleDiseq = new DisIntervalEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
              result = new RefinedGeneric<
                DisIntervalEnvironment<BoxedVariable<Variable>, BoxedExpression>,
                INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>,
                BoxedVariable<Variable>, BoxedExpression>
                (simpleDiseq, result, this.ExpressionManager);
            }
          }

          if (this.Options.UseTracePartitioning)
          {
            result = new BoundedDisjunction<BoxedVariable<Variable>, BoxedExpression>(result);
          }
          #endregion

          return result;
        }

        private IIntervalAbstraction<BoxedVariable<Variable>, BoxedExpression> InitialIntervalAbstraction(bool disjunctions)
        {
          Contract.Ensures(Contract.Result<IIntervalAbstraction<BoxedVariable<Variable>, BoxedExpression>>() != null);

          if (disjunctions)
          {
            return new DisIntervalEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
          }
          else
          {
            return new IntervalEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
          }
        }

        #endregion

        #region Logic to chose the numerical abstract domain in an adaptive way
        protected bool TryAdaptiveNumericalDomain(
          out INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> dom,
          out string reason)
        {          
          var sc = this.MethodDriver.SyntacticComplexity; 
          if (sc.IsValid)
          {
            if (sc.TooManyLoops || sc.TooManyJoins || sc.TooManyInstructionsPerJoin)
            {
              if (sc.TooManyLoops)
              {
                reason = "too many loops";
              }
              else if (sc.TooManyJoins)
              {                
                reason = sc.WayTooManyJoins? "WAY too many joins" : "too many join points";
              }
              else
              {
                reason = "too many instructions for join";
              }


              var intv = new DisIntervalEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);

              /*
                              var intv = new IntervalEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);

              if (sc.WayTooManyJoins)
              {
                dom = intv;
              }
              else*/
              {
                var wub = new WeakUpperBounds<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);

                dom = new Pentagons<BoxedVariable<Variable>, BoxedExpression>(intv, wub, this.ExpressionManager);
              }

              return true;
            }
          }

          dom = null; reason = "";
          return false;
        }
        #endregion

        #region Transfer functions

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HelperForAssignInParallel(INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> state, Pair<APC, APC> edge, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap, Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          var simplifiedMap = base.SimplifyRefinedMap(ref edge, refinedMap);

          return base.HelperForAssignInParallel(state, edge, simplifiedMap, convert);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Binary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var x = ToBoxedExpression(pc, dest);
          var leftExp = ToBoxedExpressionWithConstantRefinement(pc, s1);
          var rightExp = ToBoxedExpressionWithConstantRefinement(pc, s2);

          // If it is a reminder or division operation, we assume that the dividend is not zero, if the type is not a float
          // (Some other analysis will prove this)
          data = AssumeNonZeroDenominator(op, data, rightExp);

          long value;
          if (this.constantEval.TryEvalToConstant(pc, dest, op, leftExp, rightExp, out value))
          {
            data.AssumeInDisInterval(ToBoxedVariable(dest), DisInterval.For(value));

            return data;
          }

          // If it is a comparison, just do nothing
          if (!op.IsComparisonBinaryOperator())
          {
            var exp = BoxedExpression.Binary(op, leftExp, rightExp, dest);
            if (op.IsOverflowChecked())
            {
              data = AssumeNonOverflow(pc, op, dest, exp, data);
            }

            // Push the assignment to the underlying abstract domain, so that the domains can treat it more precisely, if they want
            data.Assign(x, exp, data.Clone() as INumericalAbstractDomainQuery<BoxedVariable<Variable>, BoxedExpression>);
          }

          UpdateMaxSize(data);

          return data;
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Box(APC pc, Type type, Variable dest, Variable source, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          // remember that dest is the boxed version of source
          this.BoxedVariables[dest] = source;

          return data;
        }

        /// <summary>
        /// Here we encode a more refined transfer function for some common methods (e.g. the length of a string is >= 0).
        /// In the future this is doomed to became smaller, and hopefully disappear, when contracts will be everywhere ...
        /// </summary>
        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        // where TypeList : IIndexable<Type> 
        // where ArgList : IIndexable<Variable>
        {
          callSiteCache[pc] = data.Clone() as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>; // cache for potential old state lookup. (override due to fixpoint)

          var result = base.Call(pc, method, tail, virt, extraVarargs, dest, args, data);
          var mdd = this.DecoderForMetaData;
          var context = this.Context.ValueContext;
          var cfg = this.Context.MethodContext.CFG;

          // Assume the type
          var destType = context.GetType(cfg.Post(pc), dest);
          if (destType.IsNormal &&
            (
              mdd.IsPrimitive(destType.Value) ||
              mdd.IsEnum(destType.Value)
            )
           )
          {
            result = SetInitialRange(dest, destType.Value, result);
          }

          var methodname = mdd.Name(method);
          var containingType = mdd.DeclaringType(method);
          var containingTypeName = mdd.Name(containingType);
          var nsName = mdd.Namespace(containingType);

          if (nsName == null)
          {
            return result;
          }

          // Ad-hoc handling of functions in System.Math
          if (containingTypeName != null &&
            nsName.Equals("System") && containingTypeName.Equals("Math"))
          {
            result = HandleMathematicalFunction(pc, method, tail, virt, extraVarargs, dest, args, data);
          }

          // Ad-hoc handling of IsNaN
          if (mdd.IsCallToIsNaN(method))
          {
            Type type; object value;
            if (context.IsConstant(pc, args[0], out type, out value))
            {
              if ((value is Double && Double.IsNaN((Double)value)) || (value is Single && Single.IsNaN((Single)value)))
              {
                data.AssumeInDisInterval(ToBoxedVariable(dest), DisInterval.For(1)); // set it to true
              }
              else
              {
                data.AssumeInDisInterval(ToBoxedVariable(dest), DisInterval.For(0)); // set it to false
              }
            }
            else
            {
              var argument = ToBoxedVariable(args[0]);

              if (data.BoundsFor(argument).IsNormal())  // that is, it's not NaN
              {
                data.AssumeInDisInterval(ToBoxedVariable(dest), DisInterval.For(0));  // set it to false
              }
            }
          }

          // Ad-hoc handling of multi-dimensional arrays
          if (containingTypeName != null &&
            nsName.Equals("System") && containingTypeName.Equals("Array"))
          {
            result = HandleArrayFunction(pc, method, tail, virt, extraVarargs, dest, args, data);
          }

          // Hack to handle both ICollection and ICollection<T> 
          if (
             (nsName.Equals("System.Collections") || nsName.Equals("System.Collections.Generic"))
            && containingTypeName.Equals("ICollection")
            && methodname.Equals("get_Count"))
          {
            // If it is an array ...
            var type = context.GetType(pc, args[0]);
            Variable len;
            if (type.IsNormal && mdd.IsArray(type.Value)
              && context.TryGetArrayLength(cfg.Post(pc), args[0], out len))
            {
              var lenAsBoxed = ToBoxedExpression(pc, len);
              var destAsBoxed = ToBoxedExpression(pc, dest);

              result = data.TestTrueEqual(lenAsBoxed, destAsBoxed);
            }
          }

#if false
          if(nsName != null 
            && nsName.Equals("System.Collections") || nsName.Equals("System.Collections.Generic")
            && methodname.Equals("Contains"))
          {
            if (mdd.IsConstructor(this.MethodDriver.CurrentMethod))
            {
            }
          }
#endif
          if (nsName.StartsWith("Microsoft.VisualBasic"))
          {
            result = HandleVBFunction(pc, method, tail, virt, extraVarargs, dest, args, data);
          }

          // Is it a string?
          // TODO: move the string reasoning into the compound array analysis
          if (nsName.Equals("System") && containingTypeName.Equals("String"))
          {
            result = HandleStringFunction(pc, method, tail, virt, extraVarargs, dest, args, data);
          }

          if (containingTypeName != null && containingTypeName.Equals("Enumerable") && methodname == "Count<System.Int32>")
          {
            Variable arrLen;
            if (this.Context.ValueContext.TryGetArrayLength(pc.Post(), args[0], out arrLen))
            {
              result = result.TestTrueEqual(ToBoxedExpression(pc, arrLen), ToBoxedExpression(pc, dest));
            }
          }

          return result;
        }

        /// <summary>
        /// Assume a.length >= 0 forall the arrays a
        /// </summary>
        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Entry(APC pc, Method method, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var mdd = this.DecoderForMetaData;
          var context = this.Context;
          var methodContext = context.MethodContext;
          var valueContext = context.ValueContext;

          foreach (var param in mdd.Parameters(method).Enumerate())
          {
            Variable symb;
            var postPC = methodContext.CFG.Post(pc);
            if (valueContext.TryParameterValue(postPC, param, out symb))
            {
              var typeForParam = valueContext.GetType(postPC, symb);

              Variable symb_Length;

              if (typeForParam.IsNormal)
              {
                if (mdd.IsArray(typeForParam.Value) && valueContext.TryGetArrayLength(postPC, symb, out symb_Length))
                {
                  data = data.TestTrueGeqZero(ToBoxedExpression(postPC, symb_Length));
                }
                else if (mdd.IsPrimitive(typeForParam.Value))
                {
                  data = SetInitialRange(symb, typeForParam.Value, data);
                }
              }
            }
          }

          return data;
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Isinst(APC pc,Type superType, Variable dest, Variable obj, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var subType = this.Context.ValueContext.GetType(pc, obj);
          if (subType.IsNormal)
          {
            if (this.DecoderForMetaData.DerivesFrom(subType.Value, superType))
            {
              // It is a subtype, so the result evaluates to a value != 0
              data.AssumeInDisInterval(new BoxedVariable<Variable>(dest), DisInterval.For(1));
            }
            else if (this.DecoderForMetaData.IsSealed(subType.Value))
            {
              // It is not a subtype, and the type is sealed so we are sure it is always false
              data.AssumeInDisInterval(new BoxedVariable<Variable>(dest), DisInterval.Zero);
            }
          }

          return data;
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldconst(APC pc, object constant, Type type, Variable dest, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var xExp = ToBoxedExpression(pc, dest);

          var x = new BoxedVariable<Variable>(dest);

          if (this.DecoderForMetaData.IsPrimitive(type))
          {
            if (this.DecoderForMetaData.System_Int8.Equals(type))
            {
              Contract.Assume(constant is SByte);

              data.AssumeInDisInterval(x, DisInterval.For((SByte)constant));
            }
            else if (this.DecoderForMetaData.System_Int16.Equals(type))
            {
              Contract.Assume(constant is Int16);

              data.AssumeInDisInterval(x, DisInterval.For((Int16)constant));
            }
            else if (this.DecoderForMetaData.System_Int32.Equals(type))
            {
              Contract.Assume(constant is Int32);

              data.AssumeInDisInterval(x, DisInterval.For((Int32)constant));
            }
            else if (this.DecoderForMetaData.System_Int64.Equals(type))
            {
              Contract.Assume(constant is Int64, "expecting an int64 value");

              var value = GetInt64(constant);
              if (Rational.CanRepresentExactly(value))
              {
                data.AssumeInDisInterval(x, DisInterval.For(Rational.For(value)));
              }
              else
              {
                if (value > 0)
                {
                  data.AssumeInDisInterval(x, DisInterval.For(Int32.MaxValue - 1, Int32.MaxValue));
                }
                else
                {
                  data.AssumeInDisInterval(x, DisInterval.For(Int32.MinValue, Int32.MinValue + 1));
                }
              }
            }
            else if (this.DecoderForMetaData.System_Double.Equals(type))
            {
              Contract.Assume(constant is System.Double);

              var value = (Double)constant;
              if (Rational.CanRepresentExactly(value))
              {
                data.AssumeInDisInterval(x, DisInterval.For(Rational.For(value)));
              }
            }
            else if (this.DecoderForMetaData.System_Single.Equals(type))
            {
              Contract.Assume(constant is System.Single);

              var value = (Single)constant;
              if (Rational.CanRepresentExactly(value))
              {
                data.AssumeInDisInterval(x, DisInterval.For(Rational.For(value)));
              }
            }
            else
            {
              // do nothing
            }
          }

          return data;
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldlen(APC pc, Variable dest, Variable array, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          return data.TestTrueGeqZero(ToBoxedExpression(pc, dest));
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          data.AssumeInDisInterval(ToBoxedVariable(dest), this.DecoderForMetaData.GetDisIntervalForType(type));

          return data;
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldsfld(APC pc, Field field, bool @volatile, Variable dest, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          data = base.Ldsfld(pc, field, @volatile, dest, data);

          var type = this.Context.ValueContext.GetType(this.Context.MethodContext.CFG.Post(pc), dest);

          if (type.IsNormal)
          {
            var intv = this.DecoderForMetaData.GetDisIntervalForType(type.Value);
            data.AssumeInDisInterval(ToBoxedVariable(dest), intv);
          }
          return data;
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          data = base.Ldfld(pc, field, @volatile, dest, obj, data); 

          var type = this.Context.ValueContext.GetType(this.Context.MethodContext.CFG.Post(pc), dest);

          if (type.IsNormal)
          {
            var intv = this.DecoderForMetaData.GetDisIntervalForType(type.Value);
            data.AssumeInDisInterval(ToBoxedVariable(dest), intv);
          }
          return data;        
        }
        
        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldarg(APC pc, Parameter argument, bool isOld, Variable dest, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          data = base.Ldarg(pc, argument, isOld, dest, data);
          data = AssumeType(pc, dest, data);
          return data;
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldstack(APC pc, int offset, Variable dest, Variable source, bool isOld, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          if (isOld)
          {
            // grab information from prior to call site (source) and apply it to dest
            INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> oldState;
            APC oldPC;
            if (TryFindOldState(pc, out oldState, out oldPC))
            {
              var interval = oldState.BoundsFor(ToBoxedExpression(oldPC, source));
              if (interval.IsNormal)
              {
                var destExpr = ToBoxedExpression(this.Context.MethodContext.CFG.Post(pc), dest);
                if (!interval.LowerBound.IsInfinity)
                {
                  data = data.TestTrueLessEqualThan(ToBoxedExpression(interval.LowerBound), destExpr);
                }

                if (!interval.UpperBound.IsInfinity)
                {
                  data.TestTrueLessEqualThan(destExpr, ToBoxedExpression(interval.UpperBound));
                }
              }
            }
          }
          return base.Ldstack(pc, offset, dest, source, isOld, data);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Unary( APC pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Variable source, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          data = base.Unary(pc, op, overflow, unsigned, dest, source, data);

          data = AssumeType(this.Context.MethodContext.CFG.Post(pc), dest, data);

          // Assumme arg != MinValue (accorging to its type)
          if (op == UnaryOperator.Neg)
          {
            var type = this.Context.ValueContext.GetType(pc, source);
            if (type.IsNormal)
            {
              var argAsBoxedExpression = ToBoxedExpression(pc, source);
              object minValueForType;

              if (this.DecoderForMetaData.Equal(type.Value, this.DecoderForMetaData.System_Int32))
              {
                minValueForType = Int32.MinValue;
              }
              else if (this.DecoderForMetaData.Equal(type.Value, this.DecoderForMetaData.System_Int64))
              {
                minValueForType = Int64.MinValue;
              }
              else if (this.DecoderForMetaData.Equal(type.Value, this.DecoderForMetaData.System_Int16))
              {
                minValueForType = Int16.MinValue;
              }
              else if (this.DecoderForMetaData.Equal(type.Value, this.DecoderForMetaData.System_Int8))
              {
                minValueForType = SByte.MinValue;
              }
              else
              {
                goto nothingToDo;
              }

              var condition = BoxedExpression.Binary(BinaryOperator.Cne_Un, argAsBoxedExpression, BoxedExpression.Const(minValueForType, type.Value, this.DecoderForMetaData));
              data = data.TestTrue(condition) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;
            }
          }

        nothingToDo:
          ;

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
              {
                Type dummyType;
                object dummyValue;

                // F: We do not want to assume constant conversions to constants.
                // In general, we assume that this will be already handled by the underlying Interval domain, so we do this check to speed up the analysis
                if (!this.Context.ValueContext.IsConstant(pc, source, out dummyType, out dummyValue))
                {
                  // 1. Make sure we have a range for the type we convert from (useful for e.g. (long)int32Exp)
                  data = AssumeType(pc, source, data);

                  var exp = ToBoxedExpression(pc, source);
                  data = data.TestTrueEqual(ToBoxedExpression(pc, dest), BoxedExpression.Unary(op, exp));
                }
              }
              break;
          }

          long k;
          if (this.constantEval.TryEvalToConstant(pc, ToBoxedExpression(pc, source), out k))
          {
            if (op == UnaryOperator.Not)
            {
              var v = k != 0 ? 0 : 1;
              data = data.AssumeInInterval(ToBoxedExpression(pc, dest), Interval.For(v), this.Encoder);
            }
          }

          var repacked = BoxedExpression.Unary(op, ToBoxedExpression(pc, source));

          data.Assign(ToBoxedExpression(pc, dest), repacked);

          return data;
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Unboxany(APC pc, Type type, Variable dest, Variable obj, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          Variable original;
          if (this.BoxedVariables.TryGetValue(obj, out original))
          {
            data = CopyBounds(dest, original, data);
          }

          return data;
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Unbox(APC pc, Type type, Variable dest, Variable obj, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          Variable original;
          if (this.BoxedVariables.TryGetValue(obj, out original))
          {
            data = CopyBounds(dest, original, data);
          }

          return data;
        }

        #endregion

        #region Handling for Mathematical functions
        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleMathematicalFunction<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<Variable>
        {
          //Contract.Requires(this.DecoderForMetaData.FullName(method).StartsWith("System.Math."));

          var methodName = this.DecoderForMetaData.Name(method);
          var destAsExp = ToBoxedExpression(pc, dest);

          switch (methodName)
          {
            case "Min":
              {
                var leftAsExp = ToBoxedExpression(pc, args[0]);
                var rightAsExp = ToBoxedExpression(pc, args[1]);
                
                return HandleMin(pc, destAsExp, leftAsExp, rightAsExp, data);
              }

            case "Max":
              {
                var leftAsExp = ToBoxedExpression(pc, args[0]);
                var rightAsExp = ToBoxedExpression(pc, args[1]);
                
                return HandleMax(pc, destAsExp, leftAsExp, rightAsExp, data);
              }

            case "Abs":
              {
                var leftAsExp = ToBoxedExpression(pc, args[0]);

                return HandleAbs(pc, destAsExp, leftAsExp, data);
              }
            //case "Sign":
            //  leftAsExp = ToBoxedExpression(pc, args[0]);
            //  return HandleSign(pc, destAsExp, leftAsExp, data);

            default:
              return data;
          }
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleSign(APC pc, BoxedVariable<Variable> dest, BoxedExpression leftAsExp, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var bound = data.BoundsFor(leftAsExp);

          if (bound.IsNormal())
          {
            if (bound.IsSingleton && bound.LowerBound.IsZero)
            {
              data.AssumeInDisInterval(dest, DisInterval.For(0));
            }
            else if (bound.LowerBound > 0)
            {
              data.AssumeInDisInterval(dest, DisInterval.For(1));
            }
            else if (bound.LowerBound >= 0)
            {
              data.AssumeInDisInterval(dest, DisInterval.For(0, 1));
            }
            else if (bound.UpperBound < 0)
            {
              data.AssumeInDisInterval(dest, DisInterval.For(-1));
            }
            else if (bound.UpperBound <= 0)
            {
              data.AssumeInDisInterval(dest, DisInterval.For(-1, 0));
            }
          }

          return data;

        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleMin(APC pc, BoxedExpression dest, BoxedExpression left, BoxedExpression right, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var clonedLeft = data.Clone() as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;
          var clonedRight = data.Clone() as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;

          // case 1. left <= right => dest = left
          clonedLeft = clonedLeft.TestTrueLessEqualThan(left, right);
          clonedLeft = clonedLeft.TestTrue(BoxedExpression.Binary(BinaryOperator.Ceq, dest, left)) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;

          // case 2. left > right => dest = right
          clonedRight = clonedRight.TestTrueLessThan(right, left);
          clonedRight = clonedRight.TestTrue(BoxedExpression.Binary(BinaryOperator.Ceq, dest, right)) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;

          var result = clonedLeft.Join(clonedRight) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;

          // We add some well known facts, as the join below may lose some precision (or because we do not want to perform an expensive reduction in the underlying domains)
          // dest <= left
          result = result.TestTrue(BoxedExpression.Binary(BinaryOperator.Cle, dest, left)) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;

          // dest <= right 
          result = result.TestTrue(BoxedExpression.Binary(BinaryOperator.Cle, dest, right)) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;


          return result;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> 
          HandleMax(APC pc, BoxedExpression dest, BoxedExpression left, BoxedExpression right, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var clonedLeft = data.Clone() as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;
          var clonedRight = data.Clone() as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;

          // case 1. left <= right => dest = right
          clonedLeft = clonedLeft.TestTrueLessEqualThan(left, right);
          clonedLeft = clonedLeft.TestTrue(BoxedExpression.Binary(BinaryOperator.Ceq, dest, right)) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;

          // case 2. left > right => dest = left
          clonedRight = clonedRight.TestTrueLessThan(right, left);
          clonedRight = clonedRight.TestTrue(BoxedExpression.Binary(BinaryOperator.Ceq, dest, left)) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;

          return clonedLeft.Join(clonedRight) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> 
          HandleAbs(APC pc, BoxedExpression dest, BoxedExpression left, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          // if left != 0 ==> dest > 0
          if (data.CheckIfNonZero(left).IsTrue())
          {
            // Assume dest > 0
            data = data.TestTrue(BoxedExpression.Binary(BinaryOperator.Cgt, dest, BoxedExpression.Const(0, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData)))
              as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;
          }

          return data;
        }

        #endregion

        #region Handling for Array functions
        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleArrayFunction<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<Variable>
        {
          //Contract.Requires(this.DecoderForMetaData.FullName(method).StartsWith("System.Array"));

          var methodName = this.DecoderForMetaData.Name(method);

          switch (methodName)
          {
            case "GetUpperBound":
              {
                // We want to have x.GetUpperBound(0) to be x.Length-1, when x is of type T[]              
                var type = this.Context.ValueContext.GetType(pc, args[0]);
                Variable arrayLength;

                if (type.IsNormal && this.DecoderForMetaData.IsArray(type.Value)
                  && this.Context.ValueContext.IsZero(pc, args[1])
                  && this.Context.ValueContext.TryGetArrayLength(pc, args[0], out arrayLength)
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

                  data = data.TestTrue(constraint) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;
                }
                return data;
              }

            case "get_Rank":
              {
                var type = this.Context.ValueContext.GetType(pc, args[0]);

                if (type.IsNormal && this.DecoderForMetaData.IsArray(type.Value))
                {
                  int rank = this.DecoderForMetaData.Rank(type.Value);

                  // Assume dest == rank
                  data = (INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>)data.TestTrueEqual(ToBoxedExpression(pc, dest), BoxedExpression.Const(rank, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));
                }

                return data;
              }

            default:
              return data;
          }
        }

        #endregion

        #region Handling for String functions

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleStringFunction<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt,  TypeList extraVarargs, Variable dest, ArgList args, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<Variable>
        {
          var methodname = this.DecoderForMetaData.Name(method);

          switch (methodname)
          {
            case "get_Chars":
              {
                return HandleGetChars(pc, args[0], args[1], dest, data);
              }

            case "Contains":
                {
                  return HandleContains(pc, args[0], args[1], dest, data);
                }

            case "IsNullOrEmpty":
                {
                  return HandleIsNullOrEmpty(pc, args[0], dest, data);
                }

            default:
              break;
          }

          return data;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleGetChars(APC pc, Variable inputString, Variable index, Variable dest, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var value = ToBoxedExpression(pc, inputString).Constant as String;
          if (value != null)
          {
            var values = DisInterval.UnreachedInterval;
            foreach (var ch in value)
            {
              values = values.Join(DisInterval.For(ch));
            }
            if (values.IsNormal)
            {
              data.AssumeInDisInterval(ToBoxedVariable(dest), values);
            }
          }

          return data;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleContains(APC pc, Variable who, Variable what, Variable dest, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var ec = this.MethodDriver.ExpressionLayer.Decoder.Context.AssumeNotNull().ExpressionContext;
          var whoExp = BoxedExpression.Convert(ec.Refine(pc, who), this.MethodDriver.ExpressionDecoder);
          if (whoExp == null) return data;
          var whatExp = BoxedExpression.Convert(ec.Refine(pc, what), this.MethodDriver.ExpressionDecoder);
          if (whatExp == null) return data;

          string whoString, whatString;
          if (whoExp.IsConstantString(out whoString) && whatExp.IsConstantString(out whatString))
          {
            if (whoString != null && whatString != null)
            {
              var result = whoString.Contains(whatString) ? DisInterval.NotZero : DisInterval.Zero;
              data.AssumeInDisInterval(ToBoxedVariable(dest), result);
            }
          }

          return data;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleIsNullOrEmpty(APC pc, Variable what, Variable dest, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          string whatString;
          if (ToBoxedExpression(pc, what).IsConstantString(out whatString))
          {
            DisInterval result;
            if (String.IsNullOrEmpty(whatString))
            {
              result = DisInterval.NotZero;
              data.AssumeInDisInterval(ToBoxedVariable(dest), result);
            }
            else
            {
              result = DisInterval.Zero;
              data.AssumeInDisInterval(ToBoxedVariable(dest), result);
            }
          }

          return data;

        }


        #endregion

        #region Handling for VB functions
        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleVBFunction<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<Variable>
        {
          var methodName = this.DecoderForMetaData.Name(method);

          var result = data;

          switch (methodName)
          {
            case "IIf":
              result = HandleIIf(pc, method, tail, virt, extraVarargs, dest, args, data);
              break;

            case "ToInteger":
              result = HandleToInteger(pc, method, tail, virt, extraVarargs, dest, args, data);
              break;

            default:
              break;
          }

          return result;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleIIf<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<Variable>
        {
          var result = data;

          var cond = args[0];
          var left = args[1];
          var right = args[2];

          var destAsBE= ToBoxedExpression(pc, dest);

          var leftDom = ((INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>)result).TestTrue(ToBoxedExpression(pc, cond));
          leftDom = ((INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>)leftDom).TestTrueEqual(destAsBE, (ToBoxedExpression(pc, left)));

          var rightDom = ((INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>)result).TestFalse(ToBoxedExpression(pc, cond));
          rightDom = ((INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>)rightDom).TestTrueEqual(destAsBE, ToBoxedExpression(pc, right));

          result = (INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>)leftDom.Join(rightDom);

          return result;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleToInteger<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<Variable>
        {
          var bounds = data.BoundsFor(ToBoxedExpression(pc, args[0]));
          data.AssumeInDisInterval(ToBoxedVariable(dest), bounds);

          return data;
        }
        #endregion        

        #region Private methods

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> CopyBounds(Variable dest, Variable source, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var bounds = data.BoundsFor(ToBoxedVariable(source));
          if (bounds.IsNormal)
          {
            data.AssumeInDisInterval(ToBoxedVariable(dest), bounds);
          }

          return data;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> SetInitialRange(Variable v, Type p, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var tryIntvRange = this.DecoderForMetaData.GetIntervalForType(p);

          // F: TODOTODOTODO !!!!! if we get the int32 then there is some bug aftwerwards !!!!!
          if (tryIntvRange.IsNormal && !p.Equals(this.DecoderForMetaData.System_Int32))
          {
            data.AssumeInDisInterval(new BoxedVariable<Variable>(v), tryIntvRange.AsDisInterval);
          }

          return data;
        }

        protected bool TryFindOldState(APC pc, out INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> old, out APC oldPC)
        {
          // walk the subroutine context to determine if we are in an ensures of a method exit, or at a call-return
          // and grab the correct context pc for that state.
          var scontext = pc.SubroutineContext;
          while (scontext != null)
          {
            var head = scontext.Head;
            if (head.Three.StartsWith("after"))
            {
              // after call or after newObj
              // we are in an ensures around a method call. Find the calling context and the from block, which is the method call
              oldPC = new APC(head.One, 0, scontext.Tail);
              return callSiteCache.TryGetValue(oldPC, out old);
            }
            scontext = scontext.Tail;
          }
          old = default(INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>);
          oldPC = default(APC);
          return false;
        }
        
        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> AssumeNonZeroDenominator(
          BinaryOperator op, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data, BoxedExpression rightExp)
        {
          // F: we use this TypeOf and not GetType, because it seems that sometimes GetType returns a Int32 when the Conv_r8 unary operator is applied
          if (this.Decoder.TypeOf(rightExp).IsFloatingPointType())
          {
            return data;
          }

          switch (op)
          {
            case BinaryOperator.Div:
            case BinaryOperator.Div_Un:
            case BinaryOperator.Rem:
            case BinaryOperator.Rem_Un:
              {
                data = data.TestTrue(this.Encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.NotEqual, rightExp, this.Encoder.ConstantFor(0))) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;
              }
              break;

            default:
              // do nothing...
              break;
          }
          return data;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> AssumeNonOverflow(APC pc, BinaryOperator op, Variable dest, BoxedExpression exp, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var type = this.Context.ValueContext.GetType(this.Context.MethodContext.CFG.Post(pc), dest);

          if(type.IsNormal)
          {
            var intv = this.DecoderForMetaData.GetIntervalForType(type.Value);
            data = data.AssumeInInterval(exp, intv, this.Encoder);
          }

          return data;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> AssumeType(APC pc, Variable dest, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          // We use this trick here to evaluate dest to an interval.
          // If we get a non-finite value, it means we already saw dest before in the analysis and hence we do not need to assume the bound its values with the type.
          // Essentially we assume the previous steps have done the right thing.
          // Assuming the range when we already have one may also be incorrect in the case of a downcast, e.g. dest is a disinterval over longs with some holes and if we 
          // cast it to int in the naif way (without passing to the Eval of intervals to be exact) then we may get a wrong result - We have a test in the regression
          // to check it.
          var bound = data.BoundsFor(ToBoxedExpression(pc, dest));
          if (bound.IsFinite)
          {
            return data;
          }

          var type = this.Context.ValueContext.GetType(this.Context.MethodContext.CFG.Post(pc), dest);

          if (type.IsNormal)
          {
            var intv = this.DecoderForMetaData.GetDisIntervalForType(type.Value);
            data.AssumeInDisInterval(ToBoxedVariable(dest), intv);
          }
          else
          {
            var refined = ToBoxedExpression(pc, dest);
            UnaryOperator uop;
            BoxedExpression box;
            if (refined.IsUnaryExpression(out uop, out box))
            {
              var t = FlatDomain<Type>.TopValue;
              switch (uop)
              {
                case UnaryOperator.Conv_i1:
                  {
                    t = new FlatDomain<Type>(this.DecoderForMetaData.System_Int8);
                    break;
                  }
                case UnaryOperator.Conv_i2:
                  {
                    t = new FlatDomain<Type>(this.DecoderForMetaData.System_Int16);
                    break;
                  }

                case UnaryOperator.Conv_i4:
                  {
                    t = new FlatDomain<Type>(this.DecoderForMetaData.System_Int32);
                    break;
                  }

                case UnaryOperator.Conv_i8:
                  {
                    t = new FlatDomain<Type>(this.DecoderForMetaData.System_Int64);
                    break;
                  }

                case UnaryOperator.Conv_u1:
                  {
                    t = new FlatDomain<Type>(this.DecoderForMetaData.System_UInt8);
                    break;
                  }

                case UnaryOperator.Conv_u2:
                  {
                    t = new FlatDomain<Type>(this.DecoderForMetaData.System_UInt16);
                    break;
                  }

                case UnaryOperator.Conv_u4:
                  {
                    t = new FlatDomain<Type>(this.DecoderForMetaData.System_UInt32);
                    break;
                  }

                case UnaryOperator.Conv_u8:
                  {
                    t = new FlatDomain<Type>(this.DecoderForMetaData.System_UInt64);
                    break;
                  }

                case UnaryOperator.Conv_i:
                case UnaryOperator.Conv_r_un:
                case UnaryOperator.Conv_r4:
                case UnaryOperator.Conv_r8:
                case UnaryOperator.Conv_u:
                  {
                    return data;
                  }
              }
              // It should always be the case, but we test it to make the code more robust
              if (t.IsNormal)
              {
                var intv = this.DecoderForMetaData.GetDisIntervalForType(t.Value);
                data.AssumeInDisInterval(ToBoxedVariable(dest), intv);
              }
            }
          }
          return data;
        }

        private Int64 GetInt64(object constant)
        {
          if (constant is Int64)
            return (Int64)constant;
          if (constant is Int32)
            return (Int64)(Int32)constant;

          throw new NotSupportedException();
        }


        #endregion

        #region Postcondition suggestion

        override public bool SuggestAnalysisSpecificPostconditions(
          ContractInferenceManager inferenceManager, 
          IFixpointInfo<APC, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>> fixpointInfo,
          List<BoxedExpression> postconditions)
        {
          if (!this.DecoderForMetaData.IsVoidMethod(this.MethodDriver.CurrentMethod))
          {
            // Easy postcondition,
            // We keep it here because of we want to get bounds on expressions that can be refined but are not in the domain            
            SuggestIntervalPostcondition(fixpointInfo, postconditions);
          }

          SuggestPostconditionsFromReturnState(fixpointInfo, postconditions);

          return false;
        }

        public override bool TrySuggestPostconditionForOutParameters(
          IFixpointInfo<APC, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>> fixpointInfo,
          List<BoxedExpression> postconditions, Variable p, FList<PathElement> path)
        { 
          var exitPC = this.Context.MethodContext.CFG.NormalExit;         

          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> adomain;
          if (PreState(exitPC, fixpointInfo, out adomain))
          {
            Interval range = null;
            var type = this.Context.ValueContext.GetType(exitPC, p);
            if(type.IsNormal)
            {
              range = this.DecoderForMetaData.GetIntervalForType(type.Value);
            }
            var value = adomain.BoundsFor(BoxedExpression.Var(p));
            if (!value.IsTop)
            {
              var newpost = GenerateExpressionFromInterval(BoxedExpression.Var(path, path), value.AsInterval, range.IsNormal ? range : null);
              postconditions.AddRange(newpost);
              return true;
            }
          }

          return false;
        }

        /// <summary>
        /// Try to suggest an interval for the return valued of the method
        /// </summary>
        /// <param name="postcondition">Should be not null</param>
        protected bool SuggestIntervalPostcondition(
          IFixpointInfo<APC, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>> fixpointInfo, 
          List<BoxedExpression> postconditions)
        {
          Contract.Requires(fixpointInfo != null);
          Contract.Requires(postconditions != null);

          var normalExitPC = this.Context.MethodContext.CFG.NormalExit;

          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> adomain;
          if (PreState(normalExitPC, fixpointInfo, out adomain))
          {
            Variable retVar;
            if (this.Context.ValueContext.TryResultValue(normalExitPC, out retVar))
            {              
              var retValue = adomain.BoundsFor(BoxedExpression.Var(retVar));

              var retType = this.MethodDriver.MetaDataDecoder.ReturnType(this.MethodDriver.CurrentMethod);
              // Not all the values are interesting for all the types, so we filter them
              if (IsInterestingPostcondition(retType, retValue))
              {
                // Type range
                var typeRange = this.MethodDriver.MetaDataDecoder.GetIntervalForType(retType);

                // Intervals
                var newpost = GenerateExpressionFromInterval(BoxedExpression.Result(retType), retValue.AsInterval, typeRange.IsNormal() ? typeRange : null);
                             
                postconditions.AddRange(newpost);

                // Symbolic upper bounds
                var upp = adomain.UpperBoundsFor(BoxedExpression.Var(retVar), true);

                if (upp.Any())
                {
                  var expInPostState = new BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly>(this.Context, this.DecoderForMetaData);

                  foreach (var be in upp)
                  {
                    Details details;
                    var tryBe = expInPostState.ExpressionInPostState(be, false, out details);

                    if (tryBe != null && details.HasVariables)
                    {
                      var newExp = BoxedExpression.Binary(BinaryOperator.Clt, BoxedExpression.Result(retType), tryBe);

                      postconditions.Add(newExp);
                    }
                  }
                }

                return true;
              }
            }
          }

          return false;
        }
        
        /// <summary>
        /// Suggest the postconditions using the return value of the method
        /// </summary>
        protected bool SuggestPostconditionsFromReturnState(
          IFixpointInfo<APC, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>> fixpointInfo,
          List<BoxedExpression> expressions)
        {
          Contract.Requires(fixpointInfo != null);
          Contract.Requires(expressions != null);

          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate;
          if (PreState(this.Context.MethodContext.CFG.NormalExit, fixpointInfo, out astate))
          {
            // Use a copy
            astate = (INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>)astate.Clone();
            var expInPostState
              = new BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly>(this.Context, this.DecoderForMetaData, this.MethodDriver.DisjunctiveExpressionRefiner);

            // Project all the variables which are not visible in the poststate
            foreach (var var in astate.Variables)
            {
              Details details;

              Variable varAsVar;
              if (!var.TryUnpackVariable(out varAsVar))
              {
                continue;
              }

              var varAsBe = this.Encoder.VariableFor(var);

              var asBE = expInPostState.ExpressionInPostState(varAsBe, true, true, true, out details);
              if (asBE == null || !details.HasInterestingVariables)
              {
                astate.RemoveVariable(var);
              }
              else
              {
                // remove uninteresting boolean values
                var t = this.Context.ValueContext.GetType(this.Context.MethodContext.CFG.NormalExit, varAsVar);
                if (
                  (t.IsNormal && t.Value.Equals(this.DecoderForMetaData.System_Boolean))
                  ||
                  (details.HasReturnVariable && this.DecoderForMetaData.ReturnType(this.Context.MethodContext.CurrentMethod).Equals(this.DecoderForMetaData.System_Boolean))
                  )
                {
                  var intv = astate.BoundsFor(asBE);
                  if (!intv.IsNormal ||
                    (intv.LowerBound < 0 && intv.UpperBound >= 0) ||
                    (intv.LowerBound <= 0 && intv.UpperBound > 0))
                  {
                    astate.RemoveVariable(var);
                  }
                }

                // remove non-type consistent type 
                // F: TODO, change interval assignment implementation to have this check in earlier stages
                if (!CheckRangeTypeCompatibility(t, astate.BoundsFor(asBE)))
                {
                  astate.RemoveVariable(var);
                }
              }
            }

            if (astate.IsNormal())
            {
              expressions.AddRange(ToListOfBoxedExpressions(astate, expInPostState));

              return true;
            }
          }

          return false;
        }

        protected bool CheckRangeTypeCompatibility(FlatDomain<Type> t, DisInterval ranges)
        {
          Contract.Requires(ranges != null);

          if (t.IsNormal && ranges.IsNormal)
          {
            var type = t.Value;

            if (this.DecoderForMetaData.System_Int32.Equals(type))
            {
              return ranges.IsInt32;
            }

            // F: TODO, the other types
          }

          return true;
        }

        /// <returns>
        /// true iff the interval value is interesting for the type
        /// </returns>
        protected bool IsInterestingPostcondition(Type type, DisInterval value)
        {
          Contract.Requires(value != null);

          if (value.IsTop)
          {
            return false;
          }

          if (type.Equals(this.MethodDriver.MetaDataDecoder.System_Boolean))
          {
            return value.IsSingleton;   // If it is a singleton, we are happy, otherwise it is of no interest
          }

          if (value.Equals(this.DecoderForMetaData.GetIntervalForType(type)))
          {
            return false;
          }


          if (type.Equals(this.MethodDriver.MetaDataDecoder.System_Int32))
          {
            return value.IsInt32;
          }

          return true;
        }

        // TODO: Make it more powerful using DisInterval
        private List<BoxedExpression> GenerateExpressionFromInterval(BoxedExpression exp, Interval value, Interval typeBounds)
        {
          Contract.Ensures(Contract.Result<List<BoxedExpression>>() != null);

          var postconditions = new List<BoxedExpression>();

          if (value.IsTop)
          {
            return postconditions;
          }

          if (value.IsBottom)
          {
            // "false"
            postconditions.Add(BoxedExpression.Const(false, this.DecoderForMetaData.System_Boolean, this.DecoderForMetaData));
          }
          // Some smarter pretty printing
          else if (value.IsSingleton)
          {
            // ret == retValue
            var eq = BoxedExpression.Binary(BinaryOperator.Ceq, exp, ToBoxedExpression(value.LowerBound));
            postconditions.Add(eq);
          }
          else
          {
            if (!value.LowerBound.IsInfinity && (typeBounds == null || value.LowerBound > typeBounds.LowerBound))
            {
              // ret >= retValue.LowerBound
              var geq = BoxedExpression.Binary(BinaryOperator.Cge, exp, ToBoxedExpression(value.LowerBound));
              postconditions.Add(geq);
            }

            if (!value.UpperBound.IsInfinity && (typeBounds == null || value.UpperBound < typeBounds.UpperBound))
            {
              // ret <= retValue.UpperBound
              var leq = BoxedExpression.Binary(BinaryOperator.Cle, exp, ToBoxedExpression(value.UpperBound));
              postconditions.Add(leq);
            }

          // We may generate no postcondition;
          }

          return postconditions;
        }

        #endregion
      }
    }
  }
}
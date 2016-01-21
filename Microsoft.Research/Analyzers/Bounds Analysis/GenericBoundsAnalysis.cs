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

// The instantiation for the numerical analysis, based on the optimistic heap abstraction

using System;
using Generics = System.Collections.Generic;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;
using Microsoft.Research.CodeAnalysis;
using System.Diagnostics;
using System.Collections.Generic;
using ADomainKind = Microsoft.Research.CodeAnalysis.Analyzers.DomainKind;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{

  /// <summary>
  /// The wrapper for the interval analysis
  /// </summary>
  public static partial class AnalysisWrapper
  {
    /// <summary>
    /// The entry point for the analysis
    /// </summary>
    /// <returns>
    /// An analysis report object, with the results of the analysis
    /// </returns>
    public static IMethodResult<Variable> 
      RunTheAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Options>        
      (
      string methodName, 
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver, 
      List<Options> options,
      Predicate<APC> cachePCs, DFAController controller
    )
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
      where Options : Analyzers.ValueAnalysisOptions<Options>
    {
      var analysis = 
        new TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.NumericalAnalysis<Options>(methodName, driver, options, cachePCs, controller);

      return TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.RunTheAnalysis(methodName, driver, analysis, controller); 
    }

    public static IMethodResult<Variable> 
      RunTheAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Options>
      (
      string methodName,
      ADomainKind adomain,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
      Options options,
      Predicate<APC> cachePCs, DFAController controller
      )
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
      where Options : Analyzers.ValueAnalysisOptions<Options>
    {
      var analysis =
        new TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.NumericalAnalysis<Options>(methodName, adomain, driver, options, cachePCs, controller);

      return TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.RunTheAnalysis(methodName, driver, analysis, controller);
    }


    /// <summary>
    /// This class is just for binding types for the internal classes
    /// </summary>
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type:IEquatable<Type>
    {
      #region Facility to forward operations on the abstract domain (implementation of IAbstractDomainOperations)
      public class AbstractOperationsImplementationNumerical<Options> : IAbstractDomainOperations<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>
        where Options : Analyzers.ValueAnalysisOptions<Options>
      {
        public bool LookupState(IMethodResult<Variable> mr, APC pc, out INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate)
        {
          astate = null;
          var an = mr as NumericalAnalysis<Options>;
          if (an == null)
            return false;

          return an.PreStateLookup(pc, out astate);
        }

        public INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Join(IMethodResult<Variable> mr, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate1, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate2)
        {
          var an = mr as NumericalAnalysis<Options>;
          if (an == null)
            return null;

          bool bWeaker;
          return an.Join(new Pair<APC, APC>(), astate1, astate2, out bWeaker, false);
        }

        public List<BoxedExpression> ExtractAssertions(
          IMethodResult<Variable> mr,
          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate,
          IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder)
        {
          NumericalAnalysis<Options> an = mr as NumericalAnalysis<Options>;
          if (an == null)
            return null;

          BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly> br = new BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly>(context, metaDataDecoder);

          return an.ToListOfBoxedExpressions(astate, br);
        }

        public bool AssignInParallel(IMethodResult<Variable> mr, ref INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> mapping, Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          NumericalAnalysis<Options> an = mr as NumericalAnalysis<Options>;
          if (an == null)
            return false;

          astate.AssignInParallel(mapping, convert);
          return true;
        }
      }
      #endregion

      /// <summary>
      /// The generic class for the numerical analysis of a piece of IL
      /// </summary>
      public class NumericalAnalysis<Options> :
        GenericNumericalAnalysis<Options>
        where Options : Analyzers.ValueAnalysisOptions<Options>
      {
        #region Protected static and cached values

        protected readonly List<Options> optionsList;

        #endregion

        #region Constructor
        internal NumericalAnalysis
        (
          string methodName,
          ADomainKind abstractDomain,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable,ILogOptions> mdriver,
          Options options,
          Predicate<APC> cachePCs, DFAController controller
        )
          : base(methodName, mdriver, options, cachePCs, controller)
        {
          this.optionsList = new List<Options>();

          WeakUpperBoundsEqual<BoxedVariable<Variable>, BoxedExpression>.DefaultClosureSteps = mdriver.Options.Steps; 

          NumericalDomainWithKarr<BoxedVariable<Variable>, BoxedExpression>.DefaultClosureSteps = mdriver.Options.Steps;
          NumericalDomainWithKarr<BoxedVariable<Variable>, BoxedExpression>.MaxPairWiseEqualitiesInClosure = options.ClosurePairs;
        }

        /// <summary>
        /// Method to be invoked when we want to run different analyses
        /// </summary>
        internal NumericalAnalysis
        (
          string methodName,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          List<Options> optionsList,
          Predicate<APC> cachePCs, DFAController controller
        )
          : this(methodName, optionsList[0].Type, mdriver, optionsList[0], cachePCs, controller)
        {
          //Contract.Requires(optionsList.Count > 0);

          this.optionsList = optionsList;
        }

        #endregion

        #region IAbstractAnalysis<APC, ... > Members

        #endregion 

        #region HotPoints for array accesses : NewArray, Ldlen, Ldelem, Ldelema and Stelem

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldlen(APC pc, Variable dest, Variable array, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          // assume len <= Int32.MaxValue
          data = data.TestTrueLessEqualThan(ToBoxedExpression(pc, dest), BoxedExpression.Const(Int32.MaxValue, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));

          return base.Ldlen(pc, dest, array, data);
        }

        /// <summary>
        /// We check that the length of the new array is greater than zero
        /// </summary>
        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Newarray<ArgList>(APC pc, Type type, Variable dest, ArgList lengths, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          if (lengths.Count == 1)
          {
            return HandleAllocations(pc, dest, lengths[0], data);
          }
          else
          {
            return data; // TODO multidimensional array
          }
        }

        /// <summary>
        /// Here we check the array accesses
        /// </summary>
        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          data = base.Ldelem(pc, type, dest, array, index, data);

          return HandleArrayAccesses("Ldelem", pc, type, array, index, data);
        }

        /// <summary>
        /// We convert it into a call to <code>ldelem</code>
        /// </summary>
        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldelema(APC pc, Type type, bool @readonly, Variable dest, Variable array, Variable index, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          data = base.Ldelema(pc, type, @readonly, dest, array, index, data);

          return HandleArrayAccesses("Ldelema", pc, type, array, index, data);
        }

        /// <summary>
        /// Here we check the array accesses
        /// </summary>
        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Stelem(APC pc, Type type, Variable array, Variable index, Variable value, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          return HandleArrayAccesses("Stelem", pc, type, array, index, data);
        }

        #endregion

        #region Private Methods : HandleAllocations, HandleArrayAccesses

        /// <summary>
        /// Create the continuation to check that <code>0 &le; len </code>, and assumes it in the rest of the program
        /// </summary>
        /// <param name="dest">unused up to now..</param>
        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleAllocations(APC pc, Variable dest, Variable len, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var lenAsExp = ToBoxedExpression(pc, len);

          // We assume that len >= 0
          var refinedDomain = data.TestTrueGeqZero(lenAsExp);

          Variable arrayLengthVar;
          if(this.Context.ValueContext.TryGetArrayLength(this.Context.MethodContext.CFG.Post(pc), dest, out arrayLengthVar))
          {
            var arrayLenghtExp = ToBoxedExpression(pc, arrayLengthVar);
            refinedDomain = refinedDomain.TestTrueEqual(lenAsExp, arrayLenghtExp);
          }
 
          return refinedDomain;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleArrayAccesses(string name, APC pc, Type type, Variable array, Variable index, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          Variable arrayLength;
          if (this.Context.ValueContext.TryGetArrayLength(pc, array, out arrayLength))
          {
            // Refine the variables to expressions
            var expForIndex = ToBoxedExpression(pc, index);
            var expForArrayLength = ToBoxedExpression(pc, arrayLength);

            // Assume that index >= 0
            var refinedDomain = data.TestTrueGeqZero(expForIndex);

            // Assume that index < array.Length
            refinedDomain = refinedDomain.TestTrueLessThan(expForIndex, expForArrayLength);

            return refinedDomain;
          }
          else
          {
            return data;
          }
        }

        #endregion       

        #region Proof obligations

        private IEnumerable<IFixpointInfo<APC, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>> FixpointInfo()
        {
          if (fixpointInfo_List == null)
          {
            fixpointInfo_List = new List<IFixpointInfo<APC, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>>();
          }

          var cached_Count = fixpointInfo_List.Count;

          for (var i = 1; i < this.optionsList.Count; i++)
          {
            var result = null as IFixpointInfo<APC, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>;
            if (cached_Count > 0 && i <= cached_Count)
            {
              result = fixpointInfo_List[i - 1];
            }
            else
            {
              var opt = this.optionsList[i];

              try
              {
#if DEBUG
                var stopWatch = new CustomStopwatch();
                stopWatch.Start();
                Console.WriteLine("[NUMERICAL] Running a refined analysis");
#endif


                if (this.Options.LogOptions.ShowPhases)
                {
                  Console.WriteLine("===== Running a refined analysis");
                }

                string why;
                if (!this.MethodDriver.AdditionalSyntacticInformation.Renamings.TooManyRenamingsForRefinedAnalysis(!this.Options.LogOptions.IsAdaptiveAnalysis, out why))
                {

                  var run = AnalysisWrapper.RunTheAnalysis(this.MethodName, opt.Type, this.MethodDriver, opt,
                    (APC pc) => false, null) as NumericalAnalysis<Options>;
                  // TODO(wuestholz): Maybe we should pass a non-null controller in the line above.
                  result = run.fixpointInfo;
                }
                else
                {
#if DEBUG
                  var methodName = this.DecoderForMetaData.Name(this.MethodDriver.CurrentMethod);
                  Console.WriteLine("[REFINED ANALYSIS] Skipping refined analysis as the method {0} contains too many renamings ({1}). To force the more precise analysis add the option -adaptive=false", methodName, why);
#endif
                  result = null;
                }
#if DEBUG
                if (this.Options.LogOptions.ShowPhases)
                {
                  Console.WriteLine("     Refined analysis done. Elapsed {0}", stopWatch.Elapsed);
                }
#endif
              }
              catch (TimeoutExceptionFixpointComputation)
              {
#if  DEBUG
                Console.WriteLine("on demand fixpoint computation timed out!");
#endif
              }
              // Cache the fixpoint
              this.fixpointInfo_List.Add(result);
            }

            if (result != null)
            {
              yield return result;
            }
          }
        }

        public override IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>> fixpoint)
        {
          if (this.optionsList.Count <= 1 || this.HasSwitchedToAdaptativeMode)
          {
#if DEBUG
            // Just for tracing
            if (this.optionsList.Count > 1)
            {
              Console.WriteLine(string.Format("Skipped numerical domain refinement. Reason: We are running an adaptive analysis"));
            }
#endif
            return new AILogicInference<INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>(this.Decoder,
              this.Options, fixpoint, this.Context, this.DecoderForMetaData, this.Options.TraceChecks);
          }
          else
          {
            return new AILogicInferenceWithRefinements<INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>(this.Decoder,
              this.Options, fixpoint, this.Context, this.DecoderForMetaData, FixpointInfo, this.Options.TraceChecks);
          }
        }

        #endregion
      }
    }
  }
}

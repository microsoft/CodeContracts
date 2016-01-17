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
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.DataStructures;
using System.Collections.Generic;

namespace Microsoft.Research.CodeAnalysis
{


  public static partial class AnalysisWrapper
  {
    public static IMethodResult<Variable> AnalyzeStrings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
    (
      string methodName,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver, IValueAnalysisOptions options,
      Predicate<APC> cachePCs, DFAController controller
    )
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      // We call the helper as a syntactic convenience, as there are too many type parameters!
      return TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.HelperForStringAnalysis(methodName, driver, options, cachePCs, controller);
    }

    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      /// <summary>
      /// It runs the analysis. 
      /// It is there because so we can use the typebinding, and make the code less verbose
      /// </summary>
      internal static IMethodResult<Variable> HelperForStringAnalysis
      (
        string methodName,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver, 
        IValueAnalysisOptions options,
        Predicate<APC> cachePCs, DFAController controller
      )
      {
        var analysis = new StringValueAnalysis(methodName, driver, options, cachePCs);

        var closure = driver.HybridLayer.CreateForward(analysis, new DFAOptions { Trace = false }, controller);

        closure(analysis.GetTopValue());   // Do the analysis 

        return analysis;
      }

      #region Facility to forward operations on the abstract domain (implementation of IAbstractDomainOperations)
      public class AbstractOperationsImplementationString : IAbstractDomainOperations<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>
      {
        public bool LookupState(IMethodResult<Variable> mr, APC pc, out SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate)
        {
          astate = null;
          StringValueAnalysis an = mr as StringValueAnalysis;
          if (an == null)
            return false;

          return an.PreStateLookup(pc, out astate);
        }

        public SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Join(IMethodResult<Variable> mr, SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate1, SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate2)
        {
          StringValueAnalysis an = mr as StringValueAnalysis;
          if (an == null)
            return null;

          bool bWeaker;
          return an.Join(new Pair<APC, APC>(), astate1, astate2, out bWeaker, false);
        }

        public List<BoxedExpression> ExtractAssertions(
          IMethodResult<Variable> mr,
          SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate,
          IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder)
        {
          StringValueAnalysis an = mr as StringValueAnalysis;
          if (an == null)
            return null;

          BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly> br = new BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly>(context, metaDataDecoder);

          return an.ToListOfBoxedExpressions(astate, br);
        }

        public bool AssignInParallel(IMethodResult<Variable> mr, ref SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> mapping, Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          StringValueAnalysis an = mr as StringValueAnalysis;
          if (an == null)
            return false;

          astate.AssignInParallel(mapping, convert);
          return true;
        }
      }
      
      #endregion


      /// <summary>
      /// The analysis for the value of strings
      /// </summary>
      internal class StringValueAnalysis :
        GenericValueAnalysis<SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression>, IValueAnalysisOptions>
      {
        #region Constructor
        internal StringValueAnalysis(
          string methodName, 
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable,ILogOptions> methodDriver, 
          IValueAnalysisOptions options,
          Predicate<APC> cachePCs
        )
          : base(methodName, methodDriver, options, cachePCs)
        {
        }
        #endregion

        /// <summary>
        /// Here we catch the calls to methods of String, so that we can apply operations on string, as concatenations. etc.
        /// </summary>
        public override SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        // where ArgList : IIndexable<int>
        {
          string methodName = this.DecoderForMetaData.FullName(method);


          SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression> result;
          var baseResult =  base.Call<TypeList, ArgList>(pc, method, tail, virt, extraVarargs, dest, args, data);

          if (IsACallToString(methodName))
          {
            result = HandleCallToString(pc, methodName, dest, args, baseResult);
          }
          else
          {
            result = baseResult;
          }

          return result;
        }

        #region Implementation of the abstract interface

        public override bool SuggestAnalysisSpecificPostconditions(ContractInferenceManager inferenceManager, IFixpointInfo<APC, SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression>> fixpointInfo, List<BoxedExpression> postconditions)
        {
          // does nothing
          return false;
        }

        public override bool TrySuggestPostconditionForOutParameters(IFixpointInfo<APC, SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression>> fixpointInfo, List<BoxedExpression> postconditions, Variable p, FList<PathElement> path)
        {
          return false;
        }

        public override SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression> GetTopValue()
        {
          return new SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression>(this.Decoder);
        }
        #endregion

        #region Private methods

        private const string System_String = "System.String.";

        private SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression>/*!*/ HandleCallToString<ArgList>(APC pc, string/*!*/ fullMethodName, Variable/*!*/ dest, ArgList/*!*/ args, SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression>/*!*/ state)
          where ArgList : IIndexable<Variable>
        {
          string method = fullMethodName.Substring(System_String.Length);
          method = method.Contains("(")?  method.Substring(0, method.IndexOf("(")) : method;  // removes the arguments

          BoxedExpression one, two, three;
          BoxedExpression x = BoxedExpression.For(this.Context.ExpressionContext.Refine(pc, dest), this.Decoder.Outdecoder);

          switch (method)
          {
            case "CompareTo":
              ExtractActualParameters(pc, args[0], args[1], out one, out two);
              /* does nothing */ state.CompareTo(one, two);

              // does nothing as it returns a FlatAbstractDomain<bool>
              break;

            case "Contains":
              // does nothing as it returns a FlatAbstractDomain<bool>
              ExtractActualParameters(pc, args[0], args[1], out one, out two);
              /* does nothing */ state.Contains(one, two);
              break;

            case "Concat":
              if (args.Count == 2)
              {
                ExtractActualParameters(pc, args[0], args[1], out one, out two);

                state.Concat(x, one, two);
              }
              else
              {
                // It is a call to String.Concat(String[]) which we ignore
              }
              break;

            case "Insert":
              ExtractActualParameters(pc, args[0], args[1], args[2], out one, out two, out three);
              state.Insert(x, one, two, three);   // !!!!!! Here I am switching the params, I think is a bug, ask Manuel
              break;

            case "StartsWith":
              ExtractActualParameters(pc, args[0], args[1], out one, out two);
              /* does nothing */ state.StartsWith(one, two);

              // does nothing, as it returns a FlatAbstractDomain<bool>
              break;

            case "Trim":
              ExtractActualParameters(pc, args[0], out one);
              state.Trim(x, one);
              break;

            default:
              break;
          }

          return state;
        }

        private void ExtractActualParameters(APC pc, Variable p1, out BoxedExpression one)
        {
          one = BoxedExpression.For(this.Context.ExpressionContext.Refine(pc, p1), this.Decoder.Outdecoder);
        }

        private void ExtractActualParameters(APC pc, Variable p1, Variable p2, out BoxedExpression one, out BoxedExpression two)
        {
          one = BoxedExpression.For(this.Context.ExpressionContext.Refine(pc, p1), this.Decoder.Outdecoder);
          two = BoxedExpression.For(this.Context.ExpressionContext.Refine(pc, p2), this.Decoder.Outdecoder);
        }

        private void ExtractActualParameters(APC pc, Variable p1, Variable p2, Variable p3, out BoxedExpression one, out BoxedExpression two, out BoxedExpression three)
        {
          one = BoxedExpression.For(this.Context.ExpressionContext.Refine(pc, p1), this.Decoder.Outdecoder);
          two = BoxedExpression.For(this.Context.ExpressionContext.Refine(pc, p2), this.Decoder.Outdecoder);
          three = BoxedExpression.For(this.Context.ExpressionContext.Refine(pc, p3), this.Decoder.Outdecoder);
        }

        /// <returns>
        /// <code>true</code> iff <code>methodName</code> starts with "System.String"
        /// </returns>
        private bool IsACallToString(string/*!*/ methodName)
        {
          return methodName.StartsWith(System_String);
        }
        #endregion

        #region IMethodResult<Label,Expression> Members

        public ProofOutcome ValidateExplicitAssertion(APC pc, Expression expr)
        {
          throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        public override IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC, SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression>> fixpoint)
        {
          return new SimpleLogicInference<Local, Parameter, Method, Field, Type, Expression, Variable>(this.Context, 
            new FactBase(fixpoint), null, null, this.MethodDriver.BasicFacts.IsUnreachable, null);
        }

        class FactBase : IFactBase<Variable>
        {
          IFixpointInfo<APC, SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression>> fixpoint;

          public FactBase(IFixpointInfo<APC, SimpleStringAbstractDomain<BoxedVariable<Variable>, BoxedExpression>> fixpoint)
          {
            this.fixpoint = fixpoint;
          }

          #region IFactBase<Variable> Members

          public ProofOutcome IsNull(APC pc, Variable variable)
          {
            return ProofOutcome.Top;
          }

          public ProofOutcome IsNonNull(APC pc, Variable variable)
          {
            return ProofOutcome.Top;
          }

          public bool IsUnreachable(APC pc)
          {
            return false;
          }

          public FList<BoxedExpression> InvariantAt(APC pc, FList<Variable> filter, bool replaceVarsWithAccessPaths = true)
          {
            return FList<BoxedExpression>.Empty;
          }

          #endregion
        }

      }


    }
  }
}
   

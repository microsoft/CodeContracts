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

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class Analyzers {

#if DEBUG
    public class Strings : IMethodAnalysis {

      class StringOptions : OptionParsing, IValueAnalysisOptions
      {
        #region Overrides
        protected override bool ParseUnknown(string option, string[] args, ref int index, string optionEqualsArgument)
        {
          return false;
        }
        protected override bool UseDashOptionPrefix
        {
          get
          {
            return false;
          }
        }
        #endregion

        internal StringOptions(ILogOptions logoptions)
        {
          this.logoptions = logoptions;
        }

        #region private state (non-options)
        ILogOptions logoptions;
        #endregion

        #region Options
        [OptionDescription("Don't generate implicit proof-obligations")]
        public bool noObl = false;
        #endregion

        #region Accessors
        public ILogOptions LogOptions { get { return this.logoptions; } }

        

        public int Steps { get { return 0; } }

        public bool Use2DConvexHull { get { return false; } }

        public bool InferOctagonConstraints { get { return false; } }

        public bool UseMorePreciseTransferFunction { get { return false; } }

        public bool UseTracePartitioning { get { return false; } }

        public bool TrackDisequalities { get { return false; } }

        public bool UseZ3 { get { return false; } }

        public bool UseMorePreciseWidening { get { return false; } }

        public bool NoProofObligations { get { return noObl; } }

        public ReductionAlgorithm Algorithm
        {
          get { return ReductionAlgorithm.None; }
        }

        #endregion
      }
     
      #region IMethodAnalysis Members

      public string Name { get { return "String"; } }

      public IMethodResult<Variable> 
        Analyze<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable>(
          string fullMethodName,
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
        where Variable : IEquatable<Variable>
        where Expression : IEquatable<Expression>
        where Type : IEquatable<Type>
      {
        return AnalysisWrapper.AnalyzeStrings(fullMethodName, mdriver, options);
      }

      public void PrintAnalysisSpecificStatistics(IOutput output)
      {
        throw new Exception("The method or operation is not implemented.");
      }

      StringOptions options;

      public bool Initialize(ILogOptions logoptions, string[] args)
      {
        this.options = new StringOptions(logoptions);
        this.options.Parse(args);
        return !this.options.HasErrors;
      }


      public void PrintOptions(string indent)
      {
        StringOptions options = new StringOptions(null);
        options.PrintOptions(indent);
        options.PrintDerivedOptions(indent);
      }

      #endregion
    }
#endif
  }

  public static partial class AnalysisWrapper
  {
    public static IMethodResult<Variable> AnalyzeStrings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>
        (string methodName,
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions> driver, IValueAnalysisOptions options)
      where Variable : IEquatable<Variable>
      where ExternalExpression : IEquatable<ExternalExpression>
      where Type : IEquatable<Type>
    {
      // We call the helper as a syntactic convenience, as there are too many type parameters!
      return TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>.HelperForStringAnalysis(methodName, driver, options);
    }

    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>
      where Variable : IEquatable<Variable>
      where ExternalExpression : IEquatable<ExternalExpression>
      where Type : IEquatable<Type>
    {
      /// <summary>
      /// It runs the analysis. 
      /// It is there because so we can use the typebinding, and make the code less verbose
      /// </summary>
      internal static IMethodResult<Variable> HelperForStringAnalysis(string methodName,
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable,ILogOptions> driver, IValueAnalysisOptions options)
      {
        StringValueAnalysis analysis;
        analysis = new StringValueAnalysis(methodName, driver, options);

        // *** The next lines must be strictly sequential ***
        Action<SimpleStringAbstractDomain<BoxedExpression>> closure = driver.CreateForward<SimpleStringAbstractDomain<BoxedExpression>>(analysis);

        // At this point, CreateForward has called the Visitor, so the context has been created, so that now we can call initValue

        SimpleStringAbstractDomain<BoxedExpression> initValue = analysis.InitialValue;

        closure(initValue);   // Do the analysis 

        return analysis;
      }

      /// <summary>
      /// The analysis for the value of strings
      /// </summary>
      internal class StringValueAnalysis :
        GenericValueAnalysis<SimpleStringAbstractDomain<BoxedExpression>>,
        IFactBase<Variable>
      {
        #region Constructor
        internal StringValueAnalysis(
          string methodName, 
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable,ILogOptions> methodDriver, 
          IValueAnalysisOptions options)
          : base(methodName, methodDriver, options)
        {
        }
        #endregion

        /// <summary>
        /// Here we catch the calls to methods of String, so that we can apply operations on string, as concatenations. etc.
        /// </summary>
        public override SimpleStringAbstractDomain<BoxedExpression> Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, SimpleStringAbstractDomain<BoxedExpression> data)
        // where ArgList : IIndexable<int>
        {
          string methodName = this.DecoderForMetaData.FullName(method);
          ALog.BeginTransferFunction(StringClosure.For("call"), StringClosure.For(methodName), 
            StringClosure.For(pc), StringClosure.For(data));

          SimpleStringAbstractDomain<BoxedExpression> result;
          SimpleStringAbstractDomain<BoxedExpression> baseResult =  base.Call<TypeList, ArgList>(pc, method, tail, virt, extraVarargs, dest, args, data);

          if (IsACallToString(methodName))
          {
            result = HandleCallToString(pc, methodName, dest, args, baseResult);
          }
          else
          {
            result = baseResult;
          }

          ALog.EndTransferFunction(StringClosure.For(result));

          return result;
        }

        #region Implementation of the abstract interface

        internal protected override SimpleStringAbstractDomain<BoxedExpression> InitialValue
        {
          get 
          {
            return new SimpleStringAbstractDomain<BoxedExpression>(this.decoderForExpressions);
          }
        }

        public override void ValidateImplicitAssertions(IOutputResults output)
        {
          throw new Exception("not implemented");
        }

        protected override ProofObligations<Local, Parameter, Method, Field, Property, Type, Variable, ExternalExpression, Attribute, Assembly, BoxedExpression, ProofObligationBase<BoxedExpression, Variable>> Obligations
        {
          get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion

        #region Private methods

        static private readonly string System_String = "System.String.";

        private SimpleStringAbstractDomain<BoxedExpression>/*!*/ HandleCallToString<ArgList>(APC pc, string/*!*/ fullMethodName, Variable/*!*/ dest, ArgList/*!*/ args, SimpleStringAbstractDomain<BoxedExpression>/*!*/ state)
          where ArgList : IIndexable<Variable>
        {
          string method = fullMethodName.Substring(System_String.Length);
          method = method.Contains("(")?  method.Substring(0, method.IndexOf("(")) : method;  // removes the arguments

          BoxedExpression one, two, three;
          BoxedExpression x = BoxedExpression.For(this.Context.Refine(pc, dest), this.decoderForExpressions.Outdecoder);

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
          one = BoxedExpression.For(this.Context.Refine(pc, p1), this.decoderForExpressions.Outdecoder);
        }

        private void ExtractActualParameters(APC pc, Variable p1, Variable p2, out BoxedExpression one, out BoxedExpression two)
        {
          one = BoxedExpression.For(this.Context.Refine(pc, p1), this.decoderForExpressions.Outdecoder);
          two = BoxedExpression.For(this.Context.Refine(pc, p2), this.decoderForExpressions.Outdecoder);
        }

        private void ExtractActualParameters(APC pc, Variable p1, Variable p2, Variable p3, out BoxedExpression one, out BoxedExpression two, out BoxedExpression three)
        {
          one = BoxedExpression.For(this.Context.Refine(pc, p1), this.decoderForExpressions.Outdecoder);
          two = BoxedExpression.For(this.Context.Refine(pc, p2), this.decoderForExpressions.Outdecoder);
          three = BoxedExpression.For(this.Context.Refine(pc, p3), this.decoderForExpressions.Outdecoder);
        }

        /// <returns>
        /// <code>true</code> iff <code>methodName</code> starts with "System.String"
        /// </returns>
        private bool IsACallToString(string/*!*/ methodName)
        {
          return methodName.StartsWith(System_String);
        }
        #endregion

        #region IMethodResult<Label,ExternalExpression> Members

        public ProofOutcome ValidateExplicitAssertion(APC pc, ExternalExpression expr)
        {
          throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        public override IFactQuery<BoxedExpression, Variable> FactQuery
        {
          get { return new SimpleLogicInference<Local, Parameter, Method, Field, Type, ExternalExpression, Variable>(this.Context, this, this.MethodDriver.IsUnreachable); }
        }

        #region IFactBase<Variable> Members

        public ProofOutcome IsNull(APC pc, Variable value)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome IsNonNull(APC pc, Variable value)
        {
          return ProofOutcome.Top;
        }

        public bool IsUnreachable(APC pc)
        {
          return false; // unknown
        }

        #endregion
      }
    }
  }
}
   

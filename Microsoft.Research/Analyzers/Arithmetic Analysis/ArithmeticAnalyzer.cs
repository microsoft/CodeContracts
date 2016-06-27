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

// The analyzer for arithmetic errors (Division-by-zero, underflow, overflow, ...)

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains;

namespace Microsoft.Research.CodeAnalysis 
{
  public static partial class Analyzers 
  {
    /// <summary>
    /// There exists one instance of this class during analysis, provided that -arithmetic is chosen on the command line. 
    /// </summary>
    public class Arithmetic
      : ValueAnalyzer<Arithmetic, Arithmetic.ArithmeticOptions>
      , IMethodAnalysis 
    {
      public class ArithmeticOptions
        : ValueAnalysisOptions<ArithmeticOptions>
      {
        public enum Obligations { intOverflow, floatOverflow, div0, divOverflow, negMin, floatEq, floatIsNaN }

        public ArithmeticOptions(ILogOptions logoptions) 
          : base(logoptions)
        {
          this.type = DomainKind.Pentagons;
          this.diseq = true;
        }

        #region Options (public fields)
        [OptionDescription("Set of obligations to produce")]
        public List<Obligations> obl = new List<Obligations>(new Obligations[] { 
          Obligations.div0, Obligations.negMin, Obligations.floatEq, 
          Obligations.divOverflow });

        [OptionDescription("Enable analysis of floats")]
        public bool fp = true;

        #endregion

        #region Getters

        public bool AnalyzeFloats { get { return this.fp; } }
        public bool Div0Obligations { get { return this.obl.Contains(Obligations.div0); } }
        public bool DivOverflowObligations { get { return this.obl.Contains(Obligations.divOverflow); } }
        public bool ArithmeticOverflow { get { return this.obl.Contains(Obligations.intOverflow); } }
        public bool FloatingPointOverflow { get { return this.obl.Contains(Obligations.floatOverflow); } }
        public bool FloatingPointIsNaN { get { return this.obl.Contains(Obligations.floatIsNaN); } }
        public bool NegMinObligations { get { return this.obl.Contains(Obligations.negMin); } }
        public bool FloatEqualityObligations { get { return this.obl.Contains(Obligations.floatEq); } }

        #endregion
      }

      public override string Name 
      {
        get 
        {
          return "Arithmetic";
        }
      }

      protected override Arithmetic.ArithmeticOptions CreateOptions(ILogOptions options)
      {
        return new ArithmeticOptions(options);
      }

      public override IProofObligations<Variable, BoxedExpression> GetProofObligations<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      (
        string fullMethodName, 
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver
      )
      {
        var result = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.ArithmeticObligations(mdriver, this.options[0]);
        return result;
      }

      public override T Instantiate<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T>(
        string methodName,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
        Predicate<APC> cachePCs, 
        IMethodAnalysisClientFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T> factory, DFAController controller
      )
      {
        var adomain = options[0].Type;
        var analysis = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.ArithmeticAnalysis(methodName, driver, options[0], adomain, cachePCs, controller);
        return factory.Create(analysis, controller);
      }
      /// <summary>
      ///  Run the analysis for the arithmetic checking
      /// </summary>
      public override IMethodResult<Variable> Analyze<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      (
        string methodFullName,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> methodDriver,
        Predicate<APC> cachePCs, DFAController controller
      )
        //where Variable : IEquatable<Variable>
        //where Expression : IEquatable<Expression>
        //where Type : IEquatable<Type> 
      {
        // F: For the moment we do not have iterative application of the analysis for those
        var result = AnalysisWrapper.RunTheArithmeticAnalysis(methodFullName, methodDriver, options[0].Type, options[0], cachePCs, controller);
        return result;
      }

      override public bool ExecuteAbstractDomainFunctor<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Options, Result, Data>(
        IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Options, IMethodResult<Variable>> cdriver,
        IResultsFunctor<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Result, Data> functor,
        Data data,
        out Result result)
          //where Variable : IEquatable<Variable>
          //where Expression : IEquatable<Expression>
          //where Type : IEquatable<Type>
          //where Options : IFrameworkLogOptions
      {
        var aoi = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.AbstractOperationsImplementationArithmetic();
        return functor.Execute(aoi, data, out result);
      }

    }
  }
}
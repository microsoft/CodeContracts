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
using System.Text;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class Analyzers
  {
    /// <summary>
    /// There exists one instance of this class during analysis, provided that -bounds is chosen on the command line. 
    /// </summary>
    public class Bounds 
      : ValueAnalyzer<Bounds, Bounds.BoundsOptions>
    {
      public class BoundsOptions
        : ValueAnalysisOptions<BoundsOptions>
      {
        public BoundsOptions(ILogOptions logoptions)
          : base(logoptions)
        {
        }
      }

      public override string Name
      {
        get
        {
          return "Bounds";
        }
      }

      public List<BoundsOptions> Options
      {
        get
        {
          return this.options;
        }
      }

      protected override Bounds.BoundsOptions CreateOptions(ILogOptions options)
      {
        return new Bounds.BoundsOptions(options);
      }

      public override T Instantiate<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T>(
        string methodName, 
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver, 
        Predicate<APC> cachePCs,
        IMethodAnalysisClientFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T> factory
      )
      {
        var analysis =
          new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.
            NumericalAnalysis<BoundsOptions>(methodName, driver, options, cachePCs);
        return factory.Create(analysis);
      }

      /// <summary>
      ///  Run the analysis for the array bounds
      /// </summary>
      public override IMethodResult<Variable> Analyze<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      (
        string methodFullName, 
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> methodDriver,
        Predicate<APC> cachePCs
      )
          //where Variable : IEquatable<Variable>
          //where Expression : IEquatable<Expression>
          //where Type : IEquatable<Type>
      {
        var result = AnalysisWrapper.RunTheAnalysis(methodFullName, methodDriver, options, cachePCs);
        return result;
      }

      public override IProofObligations<Variable, BoxedExpression> GetProofObligations<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(string fullMethodName, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
      {
        var result =
          new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.BoundsObligations(mdriver, this.options[0].NoProofObligations);
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
        var aoi = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.AbstractOperationsImplementationNumerical<Bounds.BoundsOptions>();
        return functor.Execute(aoi, data, out result);
      }

    }

  }

}
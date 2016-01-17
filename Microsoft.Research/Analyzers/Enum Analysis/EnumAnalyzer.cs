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

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class Analyzers
  {
    public class Enum :
      ValueAnalyzer<Enum, Enum.Options>
    {
      public class Options :
        ValueAnalysisOptions<Options>
      {
        public Options(ILogOptions options)
          : base(options)
        {
        }
      }

      public override bool ExecuteAbstractDomainFunctor<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Opts, Result, Data>(IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Opts, IMethodResult<Variable>> cdriver, IResultsFunctor<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Result, Data> functor, Data data, 
        out Result result)
      {
        // F: TODO: implement it
        result = default(Result);
        return false;
      }

      protected override Options CreateOptions(ILogOptions options)
      {
        return new Options(options);
      }

      public override string Name
      {
        get { return "Enum"; }
      }

      public override T Instantiate<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T>(
        string methodName, 
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
        Predicate<APC> cachePCs,
        IMethodAnalysisClientFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T> factory, DFAController controller
      )
      {
        var analysis = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.EnumAnalysis
                       (methodName, driver, options[0], cachePCs);

        return factory.Create(analysis, controller);
      }

      public override IMethodResult<Variable> 
        Analyze<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
        string fullMethodName, 
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
        Predicate<APC> cachePCs, DFAController controller
      )
          //where Type : IEquatable<Type>
          //where Expression : IEquatable<Expression>
          //where Variable : IEquatable<Variable>
      {
        var result = AnalysisWrapper.RunEnumAnalysis(fullMethodName, mdriver, this.options, cachePCs, controller);
        return result;
      }

      public override IProofObligations<Variable, BoxedExpression> GetProofObligations<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
        string fullMethodName, 
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver
      )
      {
        var result = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.EnumObligations(mdriver, this.options[0].NoProofObligations);
        return result;
      }
    }
  }
}
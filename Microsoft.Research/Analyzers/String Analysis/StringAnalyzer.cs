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
using Microsoft.Research.CodeAnalysis;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class Analyzers
  {
#if DEBUG
    public class Strings
      : ValueAnalyzer<Strings, Strings.StringOptions>
    {
      public class StringOptions
        : ValueAnalysisOptions<StringOptions>
      {
        internal StringOptions(ILogOptions logoptions)
          : base(logoptions)
        {
        }
      }

      override public string Name { get { return "String"; } }

      override protected Strings.StringOptions CreateOptions(ILogOptions options)
      {
        return new StringOptions(options);
      }

      public override IProofObligations<Variable, BoxedExpression> GetProofObligations<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(string fullMethodName, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
      {
        return null;
      }

      public override T Instantiate<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T>(
        string methodName, 
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver, 
        Predicate<APC> cachePCs,
        IMethodAnalysisClientFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T> factory
      )
      {
        var analysis = new AnalysisWrapper.TypeBindings<Local,Parameter,Method,Field,Property,Event,Type,Attribute,Assembly,Expression,Variable>.
          StringValueAnalysis(methodName, driver, options[0], cachePCs);
        return factory.Create(analysis);
      }

      override public IMethodResult<Variable>
        Analyze<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      (
        string fullMethodName,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
        Predicate<APC> cachePCs
      )
       // where Variable : IEquatable<Variable>
       // where Expression : IEquatable<Expression>
       // where Type : IEquatable<Type>
      {
        var result = AnalysisWrapper.AnalyzeStrings(fullMethodName, mdriver, this.options[0], cachePCs);
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
        var aoi = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.AbstractOperationsImplementationString();
        return functor.Execute(aoi, data, out result);
      }
    }
#endif
  }
}

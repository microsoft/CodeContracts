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

#if INCLUDE_UNSAFE_ANALYSIS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.DataStructures;
using DomainKind = Microsoft.Research.CodeAnalysis.Analyzers.DomainKind;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class Analyzers
  {
    public class Buffers
      : ValueAnalyzer<Buffers, Buffers.Options>
    {
      public override string Name
      {
        get
        {
          return "buffers";
        }
      }

      protected override Buffers.Options CreateOptions(ILogOptions options)
      {
        return new Buffers.Options(options);
      }

      public override T Instantiate<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T>(
        string methodName,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
        Predicate<APC> cachePCs,
        IMethodAnalysisClientFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T> factory, DFAController controller
      )
      {
        var analysis =
         new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.BufferAnalysis(methodName, driver, options, cachePCs, controller);
        return factory.Create(analysis, controller);
      }

      public override IProofObligations<Variable, BoxedExpression> GetProofObligations<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      (
        string fullMethodName, 
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver
      )
      {
        var result = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.BufferObligations(mdriver, this.options[0].NoProofObligations);

        return result;
      }

      public override IMethodResult<Variable> Analyze<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      (
        string fullMethodName, 
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
        Predicate<APC> cachePCs, DFAController controller
      )
        //where Type : IEquatable<Type>
        //where Expression : IEquatable<Expression>
        //where Variable : IEquatable<Variable>
      {
        var result = AnalysisWrapper.RunBufferAnalysis(fullMethodName, mdriver, this.options, cachePCs, controller);
        return result;
      }

      override public bool ExecuteAbstractDomainFunctor<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Options, Result, Data>
      (
        IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Options, IMethodResult<Variable>> cdriver,
        IResultsFunctor<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Result, Data> functor,
        Data data,
        out Result result
      )
          //where Variable : IEquatable<Variable>
          //where Expression : IEquatable<Expression>
          //where Type : IEquatable<Type>
          //where Options : IFrameworkLogOptions
      {
        var aoi = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.AbstractOperationsImplementationBuffer();
        return functor.Execute(aoi, data, out result);
      }

      public class Options
        : ValueAnalysisOptions<Options>
      {
        public Options(ILogOptions options)
          : base(options)
        {
          this.type = DomainKind.SubPolyhedra;
        }

        [OptionWitness]
        [OptionDescription("Should the analyzer handle the Binary ops in a precise way?")]
        public bool fastcheck = true;

        public bool FastCheck
        {
          get { return this.fastcheck; }
        }
      }

    }
  }
}

#endif
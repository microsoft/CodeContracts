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

// The instantiation for the Polyhedral analysis, based on the optimistic heap abstraction

using System;
using Generics = System.Collections.Generic;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;
using System.Collections.Generic;


namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {

    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>
      where Variable : IEquatable<Variable>
      where ExternalExpression : IEquatable<ExternalExpression>
      where Type : IEquatable<Type>
    {
#if POLYHEDRA
      internal partial class PolyhedraUnsafeAnalysis : UnsafeCodeAnalysis<PolyhedraEnvironment<BoxedExpression>>
      {

        public PolyhedraUnsafeAnalysis(
          string methodName,
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable,ILogOptions> mdriver,
          IValueAnalysisOptions options,
          IOverallUnsafeStatistics overallStats
          )
          : base(methodName, mdriver, options, overallStats)
        {
        }


        protected internal override PolyhedraEnvironment<BoxedExpression> InitialValue
        {
          get 
          {
            IExpressionEncoder<BoxedExpression> encoder = BoxedExpressionEncoder.Encoder(this.DecoderForMetaData, this.Context);

            PolyhedraEnvironment<BoxedExpression>.Init(this.decoderForExpressions, encoder);

            return new PolyhedraEnvironment<BoxedExpression>(this.decoderForExpressions, encoder);
          }
        }

        protected override PolyhedraEnvironment<BoxedExpression> AssumeGreaterEqualThanZero(BoxedExpression exp, PolyhedraEnvironment<BoxedExpression> data)
        {
          return (PolyhedraEnvironment<BoxedExpression>)data.TestTrueGeqZero(exp);
        }
      }
    #else
      internal partial class PolyhedraUnsafeAnalysis : UnsafeCodeAnalysis
      {
        public PolyhedraUnsafeAnalysis(
          string methodName,
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions> mdriver,
          List<Analyzers.Unsafe.UnsafeOptions> options,
          IOverallUnsafeStatistics overallStats
          )
          : base(methodName, mdriver, options, overallStats)
        {
        }


        protected internal override INumericalAbstractDomain<BoxedExpression> InitialValue
        {
          get
          { // This should be unreached code
            throw new AbstractInterpretationException("Not available in the current distribution");
          }
        }

        protected override INumericalAbstractDomain<BoxedExpression> AssumeGreaterEqualThanZero(BoxedExpression exp,
          INumericalAbstractDomain<BoxedExpression> data)
        {
          return (INumericalAbstractDomain<BoxedExpression>)data.TestTrueGeqZero(exp);
        }
      }
  #endif
    }
  }
}


#endif
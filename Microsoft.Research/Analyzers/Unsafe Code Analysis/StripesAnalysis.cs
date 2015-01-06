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

// The instantiation for the interval analysis, based on the optimistic heap abstraction

using System;
using Generics = System.Collections.Generic;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;
using System.Diagnostics;
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
      internal partial class StripeAnalysis 
        : UnsafeCodeAnalysis
      {
        public StripeAnalysis(
          string methodName,
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions> mdriver,
          List<Analyzers.Unsafe.UnsafeOptions> options,
          IOverallUnsafeStatistics overallStats
          )
          : base(methodName, mdriver, options, overallStats)
        {
        }

        /// <summary>
        /// The initial value for the analysis
        /// </summary>
        internal protected override INumericalAbstractDomain<BoxedExpression> InitialValue
        {
          get
          {
            return new Stripe<BoxedExpression, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>>(this.Decoder, this.DecoderForMetaData);
          }
        }
        /// <summary>
        /// Assume the property <code>exp >= 0</code>
        /// </summary>
        protected override INumericalAbstractDomain<BoxedExpression> AssumeGreaterEqualThanZero(BoxedExpression exp,
          INumericalAbstractDomain<BoxedExpression> data)
        {
          return data;
        }

      }
    }
  }
}


#endif
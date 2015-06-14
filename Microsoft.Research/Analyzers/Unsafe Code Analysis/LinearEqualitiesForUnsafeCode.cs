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
using System.Text;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.AbstractDomains;

namespace Microsoft.Research.CodeAnalysis
{

  public static partial class AnalysisWrapper
  {
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>
        where Variable : IEquatable<Variable>
      where ExternalExpression : IEquatable<ExternalExpression>
      where Type : IEquatable<Type>
    {
      /// <summary>
      /// This class refines the LinearEqualities domain dealing with some particular cases of the unsafe code analysis
      /// </summary>
      public class LinearEqualitiesForUnsafeCode : LinearEqualitiesEnvironment<BoxedExpression> 
      {
        protected IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context;
        protected BoxedExpressionDecoder<Type, ExternalExpression>/*!*/ bdecoder;
        protected IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder;

        public LinearEqualitiesForUnsafeCode(
          LinearEqualitiesEnvironment<BoxedExpression> /*!*/ val,
          BoxedExpressionDecoder<Type, ExternalExpression> decoder, 
          IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder)
          : base(val)
        {
          this.bdecoder = decoder;
          this.context = context;
          this.mdDecoder = mdDecoder;
        }

        private bool isZero(BoxedExpression a, IntervalsForUnsafeCode oracle)
        {
          Interval interval = oracle.BoundsFor(a);
          return interval.LowerBound == 0 && interval.UpperBound == 0;
        }

        public LinearEqualitiesForUnsafeCode Join(LinearEqualitiesForUnsafeCode prevState, Set<BoxedExpression> bottompointers, APC pc)
        {
          LinearEqualitiesForUnsafeCode thiscloned = (LinearEqualitiesForUnsafeCode)this.Clone();
          LinearEqualitiesForUnsafeCode prevcloned = (LinearEqualitiesForUnsafeCode)prevState.Clone();
          IAbstractDomain joined = base.Join(prevState);
          var result = new LinearEqualitiesForUnsafeCode((LinearEqualitiesEnvironment<BoxedExpression>)joined, this.bdecoder, this.context, this.mdDecoder);

          Set<BoxedExpression> thisvariables = thiscloned.Variables;
          Set<BoxedExpression> prevvariables = prevcloned.Variables;

          //For each pointer such that in a branch it is null and in the other is assigned to an allocated area of memory, we trace information only from
          //this branch, ignoring the other one
          foreach (BoxedExpression ptr in bottompointers)
          {
            BoxedExpression temp;
            if (ptr.TryGetAssociatedInfo(pc, AssociatedInfo.WritableBytes, out temp) || 
                ptr.TryGetAssociatedInfo(pc, AssociatedInfo.ArrayLength, out temp))
            {
              if (this.Variables.Contains(temp) && prevState.Variables.Contains(temp))
              {
                // Francesco: I added this check because it seems that something is wrong with the tracking for the null pointers 
                // (method System.Web.UI.WebControls.ObjectDataSourceView.GetResolvedMethodData(...) of System.Web.dll)
                // The check should not influence the soundness (we are losing information)
                continue;
              }
              else
              {
                RefineWithNullPointerInformation(pc, temp, thiscloned, prevcloned, ref result);
              }
            }
          }
          return result;
        }

        /// <summary>
        /// Given the length of a pointer that is null in one of the two branches of a join, add it to the domain all the contraints
        /// of the length that are on the other branch of the join
        /// </summary>
        /// <param name="pc">The pc at the join point</param>
        /// <param name="length">The length of the null pointer</param>
        /// <param name="result">The state on which we have to add the constraints</param>
        private void RefineWithNullPointerInformation(APC pc, BoxedExpression length, LinearEqualitiesForUnsafeCode thiscloned, LinearEqualitiesForUnsafeCode prevcloned, ref LinearEqualitiesForUnsafeCode result)
        {
          Set<Polynomial<BoxedExpression>> eq = thiscloned.EqualsTo(length);
          foreach (Polynomial<BoxedExpression> pol in eq)
          {
            IExpressionEncoder<BoxedExpression> encoder = BoxedExpressionEncoder.Encoder(this.mdDecoder, this.context);
            BoxedExpression right = pol.ToPureExpression(encoder);
            result = new LinearEqualitiesForUnsafeCode(result.TestTrueEqual(length, right), this.bdecoder, this.context, this.mdDecoder);
          }
          eq = prevcloned.EqualsTo(length);
          foreach (Polynomial<BoxedExpression> pol in eq)
          {
            IExpressionEncoder<BoxedExpression> encoder = BoxedExpressionEncoder.Encoder(this.mdDecoder, this.context);
            BoxedExpression right = pol.ToPureExpression(encoder);
            result = new LinearEqualitiesForUnsafeCode(result.TestTrueEqual(length, right), this.bdecoder, this.context, this.mdDecoder);
          }
        }

        public override object Clone()
        {
          return new LinearEqualitiesForUnsafeCode((LinearEqualitiesEnvironment<BoxedExpression>)base.Clone(), this.bdecoder, this.context, this.mdDecoder);
        }

      }

    }
  }
}
#endif
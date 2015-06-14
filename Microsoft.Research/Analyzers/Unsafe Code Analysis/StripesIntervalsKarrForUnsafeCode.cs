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
using System.Text;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  using System.Collections.Generic;
  using System.Diagnostics;

  public static partial class AnalysisWrapper
  {
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>
      where Variable : IEquatable<Variable>
      where ExternalExpression : IEquatable<ExternalExpression>
      where Type : IEquatable<Type>
    {

      public class StripeIntervalsKarrForUnsafeCode 
        : StripeIntervalsKarr<BoxedExpression, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>>
      {
        protected IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context;
        new BoxedExpressionDecoder<Type, ExternalExpression> decoder;
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder;

        /// <summary>
        /// It traces during the AssignInParallel function the pointers that are assigned to null
        /// </summary>
        private Set<BoxedExpression> nullPointers = new Set<BoxedExpression>();

        public StripeIntervalsKarrForUnsafeCode(
          StripeWithIntervalsForUnsafeCode/*!*/ left,
          LinearEqualitiesForUnsafeCode/*!*/ right,
          BoxedExpressionDecoder<Type, ExternalExpression> decoder, 
          IExpressionEncoder<BoxedExpression> encoder, 
          IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder
        )
          : base(left, right, decoder, encoder)
        {
          this.decoder = decoder;
          this.context = context;
          this.mdDecoder = mdDecoder;
        }

        public override IAbstractDomain Widening(IAbstractDomain prev)
        {
          StripeIntervalsKarr<BoxedExpression, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>> wid = (StripeIntervalsKarr<BoxedExpression, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>>)base.Widening(prev);

          return new StripeIntervalsKarrForUnsafeCode((StripeWithIntervalsForUnsafeCode) wid.Left, (LinearEqualitiesForUnsafeCode) wid.Right, this.decoder, this.Encoder, this.context, this.mdDecoder);
        }

        public override void AssignInParallel(Dictionary<BoxedExpression, FList<BoxedExpression>>/*!*/ sourcesToTargets)
        {
          foreach (BoxedExpression exp in sourcesToTargets.Keys)
          {
            for (FList<BoxedExpression> targets = sourcesToTargets[exp]; targets != null; targets = targets.Tail)
            {
              BoxedExpression vars = targets.Head;
              //It traces all the pointers that in a branch are assigned to null and in the other are assigned to an allocated area of memory
              //This information is used in order to refine the Join, as otherwise we would lose the size of the allocated memory
              if (!vars.Equals(exp))
                if (exp.IsNull || this.isZero(exp))
                {
                  BoxedExpression temp;
                  if (exp.TryGetAssociatedInfo(AssociatedInfo.WritableBytes, out temp) ||
                      exp.TryGetAssociatedInfo(AssociatedInfo.ArrayLength, out temp))
                  {
                    this.nullPointers.Add(vars);
                  }
                }
            }
          }
          this.Left.AssignInParallel(sourcesToTargets);
          ((LinearEqualitiesForUnsafeCode)this.Right).AssignInParallel(sourcesToTargets);
        }

        private bool isZero(BoxedExpression a)
        {
          Interval interval=((IntervalsForUnsafeCode)this.Left.Left).BoundsFor(a);
          return interval.LowerBound==0 && interval.UpperBound==0;
        }

        /// <summary>
        /// It dispatches the join operator to the improved version in order to manage pointers that are null in one of the two branches
        /// </summary>
        public StripeIntervalsKarrForUnsafeCode Join(StripeIntervalsKarrForUnsafeCode prevState, APC pc)
        {
          StripeWithIntervalsForUnsafeCode thisState_Left = this.Left as StripeWithIntervalsForUnsafeCode;
          StripeWithIntervalsForUnsafeCode prevState_Left = prevState.Left as StripeWithIntervalsForUnsafeCode;

          LinearEqualitiesForUnsafeCode thisState_Right = this.Right as LinearEqualitiesForUnsafeCode;
          LinearEqualitiesForUnsafeCode prevState_Right = prevState.Right as LinearEqualitiesForUnsafeCode;

          Debug.Assert(thisState_Left != null & thisState_Right != null & prevState_Left != null & prevState_Right != null);

         // thisState_Right = PropagateConstants(thisState_Left.Left, thisState_Right);

          StripeWithIntervalsForUnsafeCode joinLeftPart = (StripeWithIntervalsForUnsafeCode)thisState_Left.Join(prevState_Left, pc);
          LinearEqualitiesForUnsafeCode joinRightPart = (LinearEqualitiesForUnsafeCode) thisState_Right.Join(prevState_Right, this.nullPointers.Union(prevState.nullPointers), pc);
          
          nullPointers = new Set<BoxedExpression>();
          return (StripeIntervalsKarrForUnsafeCode)prevState.Reduce(joinLeftPart, joinRightPart);
        }

        private LinearEqualitiesForUnsafeCode PropagateConstants(IntervalEnvironment<BoxedExpression> intervals, LinearEqualitiesForUnsafeCode equalities)
        {
          foreach (var x in intervals.Variables)
          {
            Interval value = intervals[x];
            if (!value.IsTop && !value.IsBottom && value.IsSingleton)
            {
              BoxedExpression valueAsExp = this.Encoder.ConstantFor((Int32) value.LowerBound);
              BoxedExpression toAssume = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Equal, x, valueAsExp);

              equalities =  (LinearEqualitiesForUnsafeCode) equalities.TestTrue(toAssume);
            }
          }

          return equalities;
        }

        protected override ReducedCartesianAbstractDomain<StripeWithIntervals<BoxedExpression, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>>, LinearEqualitiesEnvironment<BoxedExpression>> Factory(StripeWithIntervals<BoxedExpression, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>> left, LinearEqualitiesEnvironment<BoxedExpression> right)
        {
          StripeWithIntervalsForUnsafeCode l;
          if (left is StripeWithIntervalsForUnsafeCode)
          {
            l = (StripeWithIntervalsForUnsafeCode)left;
          }
          else
          {
            l = new StripeWithIntervalsForUnsafeCode((IntervalsForUnsafeCode)left.Left, (StripeForUnsafeCode)left.Right, this.decoder, this.context, this.mdDecoder);
          }
            
            LinearEqualitiesForUnsafeCode r;
          if (right is LinearEqualitiesForUnsafeCode) r = (LinearEqualitiesForUnsafeCode) right;
          else r = new LinearEqualitiesForUnsafeCode(right, this.decoder, this.context, this.mdDecoder);
          StripeIntervalsKarrForUnsafeCode result = new StripeIntervalsKarrForUnsafeCode(l, r, decoder, encoder, context, this.mdDecoder);
          foreach (BoxedExpression exp in this.nullPointers)
            result.nullPointers.Add(exp);
          return result;
        }

      }

    }
  }
}
#endif
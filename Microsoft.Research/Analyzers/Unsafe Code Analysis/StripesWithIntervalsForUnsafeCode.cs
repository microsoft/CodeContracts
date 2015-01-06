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

  public static partial class AnalysisWrapper
  {
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>
      where Variable : IEquatable<Variable>
      where ExternalExpression : IEquatable<ExternalExpression>
      where Type : IEquatable<Type>
    {
      /// <summary>
      /// This class improved the precision of the base class in order to manage some special cases of the unsafe code analysis
      /// </summary>
      public class StripeWithIntervalsForUnsafeCode 
        : StripeWithIntervals<BoxedExpression, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>>
      {
        protected IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context;
        protected readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder;

        //It is used by the AssignInParallel method to trace the pointers that are null
        private Set<BoxedExpression> nullPointers = new Set<BoxedExpression>();

        public StripeWithIntervalsForUnsafeCode(
          IntervalsForUnsafeCode/*!*/ left, 
          StripeForUnsafeCode/*!*/ right, 
          IExpressionDecoder<BoxedExpression> decoder, 
          IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context, 
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder
        )
          : base(left, right, decoder)
        {
          this.context = context;
          this.mdDecoder = mdDecoder;
        }

        /// <summary>
        /// It traces the null pointers, then it use the AssingInParallel method of the base class
        /// </summary>
        public override void AssignInParallel(Dictionary<BoxedExpression, FList<BoxedExpression>>/*!*/ sourcesToTargets)
        {
          foreach (BoxedExpression exp in sourcesToTargets.Keys)
          {
            for (FList<BoxedExpression> targets = sourcesToTargets[exp]; targets != null; targets = targets.Tail)
            {
              BoxedExpression vars = targets.Head;
              //It traces all the pointers that are assigned to a null value
              if (!vars.Equals(exp))
              {
                if (exp.IsNull || this.IsZero(exp))
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
          }
          //Then it use the standard AssignInParallel function
          base.AssignInParallel(sourcesToTargets);
        }

        /// <summary>
        /// The refined version of the TestFalse function
        /// </summary>
        public override IAbstractDomainForEnvironments<BoxedExpression> TestFalse(BoxedExpression guard)
        {
          StripeWithIntervalsForUnsafeCode result=(StripeWithIntervalsForUnsafeCode) base.TestFalse(guard);
          return RefineTest(result);

        }

        /// <summary>
        /// The refined version of the TestTrue function
        /// </summary>
        public override IAbstractDomainForEnvironments<BoxedExpression> TestTrue(BoxedExpression guard)
        {
          StripeWithIntervalsForUnsafeCode result = (StripeWithIntervalsForUnsafeCode)base.TestTrue(guard);
          return RefineTest(result);
        }

        private IAbstractDomainForEnvironments<BoxedExpression> RefineTest(StripeWithIntervalsForUnsafeCode result)
        {
          //For each variable whose value has been modified we check if it is possible to refine the state of the intervals with the relational domain
          Set<BoxedExpression> modified = ((IntervalsForUnsafeCode)result.Left).NewValues;
          IntervalsForUnsafeCode intres = (IntervalsForUnsafeCode)result.Left;

          foreach (BoxedExpression val in modified)
          {
            StripeForUnsafeCode left = (StripeForUnsafeCode)new StripeForUnsafeCode(((StripeForUnsafeCode)result.Right).GetConstraintsOfAVariable(val), this.context);
            foreach (BoxedExpression key in left.Keys)
              foreach (AtMostTwoExpressions<BoxedExpression> constr in left[key].EmbeddedValues_Unsafe)
              {
                intres = RefineIntervalsWithConstraint(intres, key, constr, this.mdDecoder);
              }
          }

          //We reset the modified values
          ((IntervalsForUnsafeCode)this.Left).NewValues = new Set<BoxedExpression>();
          return (IAbstractDomainForEnvironments<BoxedExpression>) this.Factory(intres, result.Right);
        }

        private static IntervalsForUnsafeCode RefineIntervalsWithConstraint(
          IntervalsForUnsafeCode intres, 
          BoxedExpression key, 
          AtMostTwoExpressions<BoxedExpression> constr,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder
        )
        {
          if (constr.IsBottom || constr.IsTop) return intres;
          BoxedExpression toBeChecked = MakeCondition(key, constr, mdDecoder);
          intres = (IntervalsForUnsafeCode)intres.TestTrue(toBeChecked);
          return intres;
        }

        /// <summary>
        /// Given a constraint it returns the InternalExpression representing the given constraints (i.e. key-constr>0)
        /// </summary>
        public static BoxedExpression MakeCondition(BoxedExpression key, AtMostTwoExpressions<BoxedExpression> constr, IDecodeMetaData<Local,Parameter,Method,Field,Property,Type,Attribute,Assembly> mdDecoder)
        {
          BoxedExpression cons = BoxedExpression.Const(constr.N, mdDecoder.System_Int32, mdDecoder);
          BoxedExpression vars = constr.Exp1;

          if (constr.Exp2 != null)
          {
            vars = BoxedExpression.Binary(BinaryOperator.Add, vars, constr.Exp2);
          }

          BoxedExpression constant = BoxedExpression.Const(constr.Constant, mdDecoder.System_Int32, mdDecoder);
          BoxedExpression right = BoxedExpression.Binary(BinaryOperator.Mul, cons, vars);
          right = BoxedExpression.Binary(BinaryOperator.Add, right, constant);
          BoxedExpression toBeChecked = BoxedExpression.Binary(BinaryOperator.Cgt, key, right);
          return toBeChecked;
        }

        /// <summary>
        /// Check, through the context of the interval domain, if a variable is equal to zero (and so if a pointer is null)
        /// </summary>
        private bool IsZero(BoxedExpression a)
        {
          Interval interval=((IntervalsForUnsafeCode)this.Left).BoundsFor(a);
          return interval.LowerBound==0 && interval.UpperBound==0;
        }

        /// <summary>
        /// Join that performs trickery to keep bounds on pointers if one side is null
        /// </summary>
        /// <param name="pc">PC of join point</param>
        public StripeWithIntervalsForUnsafeCode Join(StripeWithIntervalsForUnsafeCode prevState, APC pc)
        {
          IntervalsForUnsafeCode thisInterval = (IntervalsForUnsafeCode)this.Left.Clone();

          IntervalsForUnsafeCode prevInterval = (IntervalsForUnsafeCode)prevState.Left.Clone();

          IntervalsForUnsafeCode joinLeftPart = (IntervalsForUnsafeCode)((IntervalsForUnsafeCode)this.Left).Join(prevState.Left, this.nullPointers.Union(prevState.nullPointers), pc);

          // Pietro's
          // if (!light)
          if(true)
          {

            //Before permorming the join, it refines the information contained in the domain
            StripeForUnsafeCode thisRightRefined = (StripeForUnsafeCode)((StripeForUnsafeCode)this.Right).RefineInternally();
            StripeForUnsafeCode prevRightRefined = (StripeForUnsafeCode)((StripeForUnsafeCode)prevState.Right).RefineInternally();
            StripeForUnsafeCode joinRightPart = (StripeForUnsafeCode)thisRightRefined.Join(prevRightRefined, this.nullPointers.Union(prevState.nullPointers), thisInterval, prevInterval, pc);

            //It resets the set of the bottom pointers
            nullPointers = new Set<BoxedExpression>();
            return (StripeWithIntervalsForUnsafeCode)prevState.Reduce(joinLeftPart, joinRightPart);
          }
          //else
          //{
          //  StripeForUnsafeCode joinRightPart = (StripeForUnsafeCode)this.Right.Join(prevState.Right, this.nullPointers.Union(prevState.nullPointers));
          //  nullPointers = new Set<BoxedExpression>();
          //  return (StripeWithIntervalsForUnsafeCode)prevState.Reduce(joinLeftPart, joinRightPart);
          //}
        }

        public StripeWithIntervalsForUnsafeCode Widening(StripeWithIntervalsForUnsafeCode prevState, APC pc)
        {
          IntervalsForUnsafeCode thisInterval = (IntervalsForUnsafeCode)this.Left.Clone();

          IntervalsForUnsafeCode prevInterval = (IntervalsForUnsafeCode)prevState.Left.Clone();

          if (this.IsBottom)
            return prevState;
          if (prevState.IsBottom)
            return this;

          IntervalsForUnsafeCode joinLeftPart = (IntervalsForUnsafeCode)((IntervalsForUnsafeCode)this.Left).Widening(prevState.Left);

          // Pietro's
          //if (!light)
          if(true)
          {

            //Since our domain is of finite height, we can use the version of join operator improved with the information abount null pointers
            StripeForUnsafeCode thisRightRefined = (StripeForUnsafeCode)((StripeForUnsafeCode)this.Right).RefineInternally();
            StripeForUnsafeCode prevRightRefined = (StripeForUnsafeCode)((StripeForUnsafeCode)prevState.Right).RefineInternally();
            StripeForUnsafeCode joinRightPart = (StripeForUnsafeCode)thisRightRefined.Join(prevRightRefined, this.nullPointers.Union(prevState.nullPointers), thisInterval, prevInterval, pc);

            //It resets the set of the bottom pointers
            nullPointers = new Set<BoxedExpression>();
            return (StripeWithIntervalsForUnsafeCode)prevState.Reduce(joinLeftPart, joinRightPart);
          }
          //else
          //{
          //  StripeForUnsafeCode joinRightPart = (StripeForUnsafeCode)this.Right.Join(prevState.Right, this.nullPointers.Union(prevState.nullPointers));
          //  nullPointers = new Set<BoxedExpression>();
          //  return (StripeWithIntervalsForUnsafeCode)prevState.Reduce(joinLeftPart, joinRightPart);
          //}

        }

        protected override ReducedCartesianAbstractDomain<IntervalEnvironment<BoxedExpression>, Stripe<BoxedExpression, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>>> Factory(IntervalEnvironment<BoxedExpression> left, Stripe<BoxedExpression, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>> right)
        {
          StripeWithIntervalsForUnsafeCode result = new StripeWithIntervalsForUnsafeCode((IntervalsForUnsafeCode)left, (StripeForUnsafeCode) right, decoder, context, this.mdDecoder);
          foreach (BoxedExpression exp in this.nullPointers)
            result.nullPointers.Add(exp);
          return result;
        }

      }

    }
  }
}
#endif
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// The implementation of the environment of intervals

using System;
using System.Text;
using System.Diagnostics;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.AbstractDomains.Numerical
{
    [ContractVerification(true)]
    sealed public class DisIntervalEnvironment<Variable, Expression>
     : IntervalEnvironment_Base<DisIntervalEnvironment<Variable, Expression>, Variable, Expression, DisInterval, Rational>
    {
        #region Constructors

        public DisIntervalEnvironment(ExpressionManager<Variable, Expression> expManager)
          : base(expManager, new DisintervalsCheckIfHoldsVisitor(expManager.Decoder))
        {
            Contract.Requires(expManager != null);
        }

        private DisIntervalEnvironment(DisIntervalEnvironment<Variable, Expression> original)
          : base(original)
        {
            Contract.Requires(original != null);
        }

        #endregion

        #region TestTrue*

        public override DisIntervalEnvironment<Variable, Expression> TestTrueGeqZero(Expression exp)
        {
            var newConstraints = IntervalInference.InferConstraints_GeqZero(exp, this.ExpressionManager.Decoder, this);

            foreach (var pair in newConstraints)
            {
                this[pair.One] = pair.Two;
            }

            return this;
        }

        public override DisIntervalEnvironment<Variable, Expression> TestTrueLessThan(Expression exp1, Expression exp2)
        {
            return HelperForTestTrueLessThanSignedOrUnsigned(true, exp1, exp2);
        }

        public override DisIntervalEnvironment<Variable, Expression> TestTrueLessThan_Un(Expression exp1, Expression exp2)
        {
            return HelperForTestTrueLessThanSignedOrUnsigned(false, exp1, exp2);
        }

        private DisIntervalEnvironment<Variable, Expression> HelperForTestTrueLessThanSignedOrUnsigned(bool isSigned, Expression exp1, Expression exp2)
        {
            bool isBottom;
            var newConstraints = IntervalInference.InferConstraints_LT(isSigned, exp1, exp2, this.ExpressionManager.Decoder, this, out isBottom);

            if (isBottom)
            {
                return this.Bottom;
            }

            Assume(newConstraints);

            return this;
        }

        public override DisIntervalEnvironment<Variable, Expression> TestTrueLessEqualThan(Expression exp1, Expression exp2)
        {
            return HelperForTestTrueLessEqualThanSignedOrUnsigned(true, exp1, exp2);
        }

        public override DisIntervalEnvironment<Variable, Expression> TestTrueLessEqualThan_Un(Expression exp1, Expression exp2)
        {
            return HelperForTestTrueLessEqualThanSignedOrUnsigned(false, exp1, exp2);
        }

        private DisIntervalEnvironment<Variable, Expression> HelperForTestTrueLessEqualThanSignedOrUnsigned(bool isSigned, Expression exp1, Expression exp2)
        {
            bool isBottom;
            var newConstraints = IntervalInference.InferConstraints_Leq(isSigned, exp1, exp2, this.ExpressionManager.Decoder, this, out isBottom);

            if (isBottom)
            {
                return this.Bottom;
            }

            Assume(newConstraints);

            return this;
        }

        protected override DisIntervalEnvironment<Variable, Expression> TestNotEqualToZero(Expression guard)
        {
            var val = Eval(guard).Meet(DisInterval.NotZero);

            var guardVar = this.ExpressionManager.Decoder.UnderlyingVariable(guard);

            this.RefineWith(guardVar, val);

            return this;
        }

        protected override DisIntervalEnvironment<Variable, Expression> TestNotEqualToZero(Variable v)
        {
            return TestTrueEqualToDisinterval(v, DisInterval.NotZero);
        }

        protected override DisIntervalEnvironment<Variable, Expression> TestEqualToZero(Variable v)
        {
            return TestTrueEqualToDisinterval(v, DisInterval.For(0));
        }

        private DisIntervalEnvironment<Variable, Expression> TestTrueEqualToDisinterval(Variable v, DisInterval dis)
        {
            Contract.Requires(dis != null);
            Contract.Ensures(Contract.Result<DisIntervalEnvironment<Variable, Expression>>() != null);

            DisInterval prevVal;
            if (this.TryGetValue(v, out prevVal))
            {
                dis = prevVal.Meet(dis);
            }

            this[v] = dis;

            return this;
        }

        public override DisIntervalEnvironment<Variable, Expression> TestNotEqual(Expression e1, Expression e2)
        {
            var v2 = this.Eval(e2);
            if (v2.IsSingleton)
            {
                var notV2 = DisInterval.NotInThisInterval(v2);

                var e1Var = this.ExpressionManager.Decoder.UnderlyingVariable(this.ExpressionManager.Decoder.Stripped(e1));

                this.RefineWith(e1Var, notV2);
            }

            bool isBottomLT, isBottomGT;

            var constraintsLT = IntervalInference.InferConstraints_LT(true, e1, e2, this.ExpressionManager.Decoder, this, out isBottomLT);
            var constraintsGT = IntervalInference.InferConstraints_LT(true, e2, e1, this.ExpressionManager.Decoder, this, out isBottomGT);

            if (isBottomLT)
            {
                // Bottom join Bottom = Bottom
                if (isBottomGT)
                {
                    return this.Bottom;
                }
                this.TestTrueListOfFacts(constraintsGT);
            }
            else if (isBottomGT)
            {
                this.TestTrueListOfFacts(constraintsLT);
            }
            else
            {
                var join = JoinConstraints(constraintsLT, constraintsGT);

                this.TestTrueListOfFacts(join);
            }

            return this;
        }

        protected override void AssumeKLessThanRight(DisInterval k, Variable right)
        {
            DisInterval refined;
            if (IntervalInference.TryRefine_KLessThanRight(true, k, right, Rational.For(1), this, out refined))
            {
                this[right] = refined;
            }
        }

        protected override void AssumeLeftLessThanK(Variable left, DisInterval k)
        {
            DisInterval refined;
            if (IntervalInference.TryRefine_LeftLessThanK(true, left, k, this, out refined))
            {
                this[left] = refined;
            }
        }

        /// <summary>
        /// Assume the constraints <code>newConstraints</code>.
        /// It works with side effects
        /// </summary>
        private void Assume(List<Pair<Variable, DisInterval>> newConstraints)
        {
            Contract.Requires(newConstraints != null);

            foreach (var pair in newConstraints)
            {
                Contract.Assume(pair.Two != null);
                this.RefineWith(pair.One, pair.Two);
            }
        }

        #endregion

        #region Bounds 

        public override DisInterval BoundsFor(Expression exp)
        {
            return this.Eval(exp);
        }

        public override DisInterval BoundsFor(Variable v)
        {
            DisInterval d;
            if (this.TryGetValue(v, out d))
            {
                return d;
            }

            return DisInterval.UnknownInterval;
        }

        protected override void AssumeInDisInterval_Internal(Variable x, DisInterval value)
        {
            if (value.IsTop)
            {
                return;
            }
            DisInterval prev, next;
            if (this.TryGetValue(x, out prev))
            {
                next = prev.Meet(value);
            }
            else
            {
                next = value;
            }

            if (next.IsBottom)
            {
                this.State = AbstractState.Bottom;
            }
            else
            {
                this[x] = next;
            }
        }

        #endregion

        #region Arithmetics

        public override bool IsGreaterEqualThanZero(Rational val)
        {
            return val >= 0;
        }

        public override bool IsGreaterThanZero(Rational val)
        {
            return val > 0;
        }

        public override bool IsLessThanZero(Rational val)
        {
            return val < 0;
        }

        public override bool IsLessEqualThanZero(Rational val)
        {
            return val <= 0;
        }

        public override bool IsLessThan(Rational val1, Rational val2)
        {
            return val1 < val2;
        }

        public override bool IsLessEqualThan(Rational val1, Rational val2)
        {
            return val1 <= val2;
        }

        public override bool IsZero(Rational val)
        {
            return val.IsZero;
        }

        public override bool IsNotZero(Rational val)
        {
            return val.IsNotZero;
        }

        public override bool IsPlusInfinity(Rational val)
        {
            return val.IsPlusInfinity;
        }

        public override bool IsMinusInfinity(Rational val)
        {
            return val.IsMinusInfinity;
        }

        public override bool AreEqual(DisInterval left, DisInterval right)
        {
            return left.IsNormal && right.IsNormal && left.LessEqual(right) && right.LessEqual(left);
        }

        public override Rational PlusInfinity
        {
            get
            {
                return Rational.PlusInfinity;
            }
        }

        public override Rational MinusInfinty
        {
            get
            {
                return Rational.MinusInfinity;
            }
        }

        public override bool TryAdd(Rational left, Rational right, out Rational result)
        {
            if (Rational.TryAdd(left, right, out result))
            {
                return true;
            }
            else
            {
                result = default(Rational);
                return false;
            }
        }

        public override FlatAbstractDomain<bool> IsNotZero(DisInterval intv)
        {
            Contract.Assume(intv != null); // F: just lazy

            if (intv.IsSingleton && intv.LowerBound.IsZero)
            {
                return CheckOutcome.False;
            }

            if (intv.Meet(this.IntervalZero).IsBottom)
            {
                return CheckOutcome.True;
            }

            return CheckOutcome.Top;
        }

        public override bool IsMaxInt32(DisInterval intv)
        {
            return intv.IsSingleton && intv.LowerBound.IsInteger && ((Int32)(intv.LowerBound.NextInt32)) == Int32.MaxValue;
        }

        public override bool IsMinInt32(DisInterval intv)
        {
            return intv.IsSingleton && intv.LowerBound.IsInteger && ((Int32)(intv.LowerBound.NextInt32)) == Int32.MinValue;
        }

        public override DisInterval IntervalUnknown
        {
            get
            {
                return DisInterval.UnknownInterval;
            }
        }

        public override DisInterval IntervalZero
        {
            get
            {
                return DisInterval.For(0);
            }
        }

        public override DisInterval IntervalOne
        {
            get
            {
                return DisInterval.For(1);
            }
        }

        public override DisInterval Interval_Positive
        {
            get { return DisInterval.For(Interval.PositiveInterval); }
        }

        public override DisInterval Interval_StrictlyPositive
        {
            get { return this.IntervalRightOpen(Rational.For(1)); }
        }

        public override DisInterval IntervalGreaterEqualThanMinusOne
        {
            get { return DisInterval.For(Interval.For(-1, Rational.PlusInfinity)); }
        }

        public override DisInterval IntervalSingleton(Rational val)
        {
            return DisInterval.For(val);
        }

        public override DisInterval IntervalRightOpen(Rational inf)
        {
            return DisInterval.For(inf, Rational.PlusInfinity);
        }

        public override DisInterval IntervalLeftOpen(Rational sup)
        {
            return DisInterval.For(Rational.MinusInfinity, sup);
        }

        public override DisInterval Interval_Add(DisInterval left, DisInterval right)
        {
            return left + right;
        }

        public override DisInterval Interval_Div(DisInterval left, DisInterval right)
        {
            return left / right;
        }

        public override DisInterval Interval_Sub(DisInterval left, DisInterval right)
        {
            return left - right;
        }

        public override DisInterval Interval_Mul(DisInterval left, DisInterval right)
        {
            return left * right;
        }

        public override DisInterval Interval_UnaryMinus(DisInterval left)
        {
            return -left;
        }

        public override DisInterval Interval_Not(DisInterval left)
        {
            if (!left.IsNormal)
            {
                return left;
            }
            // !(!0) is 0
            if (left.IsNotZero)
            {
                return DisInterval.For(0);
            }
            // !(0) is !=0
            if (left.IsZero)
            {
                return DisInterval.NotZero;
            }
            // !([0, +oo]) is [-oo, -1]
            if (left.IsPositiveOrZero)
            {
                return DisInterval.Negative;
            }

            return left;
        }

        public override DisInterval Interval_Rem(DisInterval left, DisInterval right)
        {
            return left % right;
        }

        public override DisInterval Interval_BitwiseAnd(DisInterval left, DisInterval right)
        {
            return left & right;
        }

        public override DisInterval Interval_BitwiseXor(DisInterval left, DisInterval right)
        {
            return left ^ right;
        }

        public override DisInterval Interval_BitwiseOr(DisInterval left, DisInterval right)
        {
            return left | right;
        }

        public override DisInterval Interval_ShiftLeft(DisInterval left, DisInterval right)
        {
            return DisInterval.ShiftLeft(left, right);
        }

        public override DisInterval Interval_ShiftRight(DisInterval left, DisInterval right)
        {
            return DisInterval.ShiftRight(left, right);
        }

        protected override DisInterval ApplyConversion(ExpressionOperator conversionType, DisInterval val)
        {
            return val.Map(intv => Interval.ApplyConversion(conversionType, intv));
        }

        #endregion

        #region For*

        public override DisInterval For(byte v)
        {
            return DisInterval.For(v);
        }

        public override DisInterval For(double d)
        {
            return DisInterval.For(d);
        }

        public override DisInterval For(short v)
        {
            return DisInterval.For(v);
        }

        public override DisInterval For(int v)
        {
            return DisInterval.For(v);
        }

        public override DisInterval For(long v)
        {
            return DisInterval.For(v);
        }

        public override DisInterval For(sbyte s)
        {
            return DisInterval.For(s);
        }

        public override DisInterval For(ushort u)
        {
            return DisInterval.For(u);
        }

        public override DisInterval For(uint u)
        {
            return DisInterval.For(u);
        }

        public override DisInterval For(Rational r)
        {
            return DisInterval.For(r);
        }

        public override DisInterval For(Rational inf, Rational sup)
        {
            return DisInterval.For(inf, sup);
        }

        #endregion

        #region Overridden

        public override List<Pair<Variable, Int32>> IntConstants
        {
            get
            {
                if (this.IsBottom || this.IsTop)
                {
                    return new List<Pair<Variable, Int32>>();
                }

                var result = new List<Pair<Variable, Int32>>();
                foreach (var pair in this.Elements)
                {
                    Contract.Assume(pair.Value != null);

                    if (pair.Value.IsInt32 && pair.Value.IsSingleton)
                    {
                        result.Add(pair.Key, (Int32)pair.Value.LowerBound);
                    }
                }

                return result;
            }
        }

        protected override T To<T>(Rational n, IFactory<T> factory)
        {
            return factory.Constant(n);
        }

        protected override DisIntervalEnvironment<Variable, Expression> Factory()
        {
            return new DisIntervalEnvironment<Variable, Expression>(this.ExpressionManager);
        }

        protected override DisIntervalEnvironment<Variable, Expression> DuplicateMe()
        {
            return new DisIntervalEnvironment<Variable, Expression>(this);
        }

        protected override DisIntervalEnvironment<Variable, Expression> NewInstance(
          ExpressionManager<Variable, Expression> expManager)
        {
            return new DisIntervalEnvironment<Variable, Expression>(this.ExpressionManager);
        }

        protected override DisInterval ConvertInterval(Interval intv)
        {
            return DisInterval.For(intv);
        }
        #endregion

        #region Specialized CheckIfHolds Visitor

        private class DisintervalsCheckIfHoldsVisitor
          : IntervalsCheckIfHoldsVisitor
        {
            public DisintervalsCheckIfHoldsVisitor(IExpressionDecoder<Variable, Expression> decoder)
              : base(decoder)
            {
                Contract.Requires(decoder != null);
            }

            public override FlatAbstractDomain<bool> VisitNotEqual(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
            {
                // Special case for the special semantics of NaN
                if (left.Equals(right) && !this.Decoder.IsNaN(left) && !this.Decoder.IsNaN(right))
                {
                    return CheckOutcome.False;
                }

                var direct = base.VisitNotEqual(left, right, original, data);

                if (direct.IsNormal())
                {
                    return direct;
                }

                var leftDis = Domain.Eval(left);
                var rightDis = Domain.Eval(right);

                if (leftDis.Meet(rightDis).IsBottom)
                {
                    return CheckOutcome.True;
                }

                return CheckOutcome.Top;
            }
        }
        #endregion
    }
}
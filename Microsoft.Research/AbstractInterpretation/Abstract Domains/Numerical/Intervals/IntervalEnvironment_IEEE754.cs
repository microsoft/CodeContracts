// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// The implementation of the environment of intervals

using System;
using System.Text;
using System.Diagnostics;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;
using System.Collections.Generic;

namespace Microsoft.Research.AbstractDomains.Numerical
{
    /// <summary>
    /// An interval environment is a map from Variables to intervals
    /// </summary>
    public class IntervalEnvironment_IEEE754<Variable, Expression>
       : IntervalEnvironment_Base<IntervalEnvironment_IEEE754<Variable, Expression>, Variable, Expression, Interval_IEEE754, Double>
    {
        #region Constructor

        public IntervalEnvironment_IEEE754(ExpressionManager<Variable, Expression> expManager)
          : base(expManager)
        {
        }

        public IntervalEnvironment_IEEE754(IntervalEnvironment_IEEE754<Variable, Expression> original)
          : base(original)
        {
        }
        #endregion

        protected override IntervalEnvironment_IEEE754<Variable, Expression> DuplicateMe()
        {
            return new IntervalEnvironment_IEEE754<Variable, Expression>(this);
        }

        protected override IntervalEnvironment_IEEE754<Variable, Expression> NewInstance(ExpressionManager<Variable, Expression> expManager)
        {
            return new IntervalEnvironment_IEEE754<Variable, Expression>(expManager);
        }

        public override void AssumeDomainSpecificFact(DomainSpecificFact fact)
        {
            base.AssumeDomainSpecificFact(fact);

            Variable var;
            Interval_IEEE754 intv;
            if (fact.IsAssumeInFloatInterval(out var, out intv))
            {
                Interval_IEEE754 prev;
                if (this.TryGetValue(var, out prev))
                {
                    intv = prev.Meet(intv);
                }

                this[var] = intv;
            }
        }

        public override IntervalEnvironment_IEEE754<Variable, Expression> TestTrueGeqZero(Expression exp)
        {
            return this;
        }

        public override IntervalEnvironment_IEEE754<Variable, Expression> TestTrueLessThan_Un(Expression exp1, Expression exp2)
        {
            return this.TestTrueLessThan(exp1, exp2);
        }

        public override IntervalEnvironment_IEEE754<Variable, Expression> TestTrueLessThan(Expression exp1, Expression exp2)
        {
            var v1 = this.Eval(exp1);
            var v2 = this.Eval(exp2);

            if (v1.IsNotNaN)
            {
                this.AssumeKLessThanRight(v1, this.ExpressionManager.Decoder.UnderlyingVariable(exp2));
            }

            if (v2.IsNotNaN)
            {
                this.AssumeLeftLessThanK(this.ExpressionManager.Decoder.UnderlyingVariable(exp1), v2);
            }

            return this;
        }

        public override IntervalEnvironment_IEEE754<Variable, Expression> TestNotEqual(Expression exp1, Expression exp2)
        {
            var v1 = this.Eval(exp1);
            var v2 = this.Eval(exp2);

            if (v1.IsNotNaN)
            {
                var exp2Var = this.ExpressionManager.Decoder.UnderlyingVariable(exp2);

                var intv1 = this.IntervalForKLessThanRight(v1, exp2Var);
                var intv2 = this.IntervalForLeftLessThanK(exp2Var, v1);

                var join = intv1.Join(intv2);

                if (join.IsNormal)
                {
                    this[exp2Var] = join;
                }
            }

            if (v2.IsNotNaN)
            {
                var exp1Var = this.ExpressionManager.Decoder.UnderlyingVariable(exp1);

                var intv1 = this.IntervalForKLessThanRight(v2, exp1Var);
                var intv2 = this.IntervalForLeftLessThanK(exp1Var, v2);

                var join = intv1.Join(intv2);

                if (join.IsNormal)
                {
                    this[exp1Var] = join;
                }
            }
            return this;
        }

        public override IntervalEnvironment_IEEE754<Variable, Expression> TestTrueLessEqualThan_Un(Expression exp1, Expression exp2)
        {
            return TestTrueLessEqualThan(exp1, exp2);
        }

        public override IntervalEnvironment_IEEE754<Variable, Expression> TestTrueLessEqualThan(Expression exp1, Expression exp2)
        {
            var intv1 = this.Eval(exp1);
            var intv2 = this.Eval(exp2);

            if (intv1.IsNormal)
            {
                var v2 = this.ExpressionManager.Decoder.UnderlyingVariable(exp2);
                this.AssumeKLessEqualThanRight(intv1, v2);
                this.AssumeKLessEqualThanRight(intv1, this.ExpressionManager.Decoder.UnderlyingVariable(this.ExpressionManager.Decoder.Stripped(exp2)));
            }

            if (intv2.IsNormal)
            {
                var v1 = this.ExpressionManager.Decoder.UnderlyingVariable(exp1);
                this.AssumeLeftLessEqualThanK(v1, intv2);
                this.AssumeLeftLessEqualThanK(this.ExpressionManager.Decoder.UnderlyingVariable(this.ExpressionManager.Decoder.Stripped(exp1)), intv2);
            }

            return this;
        }

        protected override IntervalEnvironment_IEEE754<Variable, Expression> TestNotEqualToZero(Expression guard)
        {
            return this;
        }

        protected override IntervalEnvironment_IEEE754<Variable, Expression> TestNotEqualToZero(Variable v)
        {
            // cannot capture v != 0
            return this;
        }

        protected override IntervalEnvironment_IEEE754<Variable, Expression> TestEqualToZero(Variable v)
        {
            var intv = Interval_IEEE754.For(0);
            Interval_IEEE754 prevVal;
            if (this.TryGetValue(v, out prevVal))
            {
                intv = prevVal.Meet(intv);
            }

            this[v] = intv;

            return this;
        }

        protected override void AssumeKLessThanRight(Interval_IEEE754 k, Variable leftExp)
        {
            if (k.IsSingleton)
            {
                if (k.LowerBound.IsZero())
                {
                    var kPlusEpsilon = Interval_IEEE754.For(Double.Epsilon, Double.PositiveInfinity);

                    Interval_IEEE754 prevIntv;
                    if (this.TryGetValue(leftExp, out prevIntv))
                    {
                        this[leftExp] = prevIntv.Meet(kPlusEpsilon);
                    }
                    else
                    {
                        this[leftExp] = kPlusEpsilon;
                    }
                }
                else  // We overapproximate
                {
                    var intv = Interval_IEEE754.For(k.LowerBound, Double.PositiveInfinity);
                    Interval_IEEE754 prevIntv;
                    if (this.TryGetValue(leftExp, out prevIntv))
                    {
                        this[leftExp] = prevIntv.Meet(intv);
                    }
                    else
                    {
                        this[leftExp] = intv;
                    }
                }
            }
        }

        private Interval_IEEE754 IntervalForKLessThanRight(Interval_IEEE754 k, Variable leftExp)
        {
            if (k.IsSingleton)
            {
                if (k.LowerBound.IsZero())
                {
                    var kPlusEpsilon = Interval_IEEE754.For(Double.Epsilon, Double.PositiveInfinity);

                    Interval_IEEE754 prevIntv;
                    if (this.TryGetValue(leftExp, out prevIntv))
                    {
                        return prevIntv.Meet(kPlusEpsilon);
                    }
                    else
                    {
                        return kPlusEpsilon;
                    }
                }
                else  // We overapproximate
                {
                    var intv = Interval_IEEE754.For(k.LowerBound, Double.PositiveInfinity);
                    Interval_IEEE754 prevIntv;
                    if (this.TryGetValue(leftExp, out prevIntv))
                    {
                        return prevIntv.Meet(intv);
                    }
                    else
                    {
                        return intv;
                    }
                }
            }

            return Interval_IEEE754.UnknownInterval;
        }

        protected override void AssumeLeftLessThanK(Variable leftExp, Interval_IEEE754 k)
        {
            if (k.IsSingleton)
            {
                Interval_IEEE754 intv;
                if (k.LowerBound.IsZero())
                { // leftExp < 0
                    intv = Interval_IEEE754.For(Double.NegativeInfinity, -Double.Epsilon);
                }
                else  // We overapproximate
                {
                    intv = Interval_IEEE754.For(Double.NegativeInfinity, k.LowerBound - double.Epsilon);
                }

                Interval_IEEE754 prevIntv;
                if (this.TryGetValue(leftExp, out prevIntv))
                {
                    this[leftExp] = prevIntv.Meet(intv);
                }
                else
                {
                    this[leftExp] = intv;
                }
            }
        }

        private Interval_IEEE754 IntervalForLeftLessThanK(Variable leftExp, Interval_IEEE754 k)
        {
            return Interval_IEEE754.UnknownInterval;
        }

        public override bool IsGreaterEqualThanZero(double val)
        {
            return val >= 0.0;
        }

        public override bool IsGreaterThanZero(double val)
        {
            return val > 0.0;
        }

        public override bool IsLessThanZero(double val)
        {
            return val < 0.0;
        }

        public override bool IsLessEqualThanZero(double val)
        {
            return val <= 0.0;
        }

        public override bool IsLessThan(double val1, double val2)
        {
            return val1 < val2;
        }

        public override bool IsLessEqualThan(double val1, double val2)
        {
            return val1 <= val2;
        }

        public override bool IsZero(double val)
        {
            return val == 0.0;
        }

        public override bool IsNotZero(double val)
        {
            return val != 0.0;
        }

        public override bool IsMaxInt32(Interval_IEEE754 intv)
        {
            return false;
        }

        public override bool IsMinInt32(Interval_IEEE754 intv)
        {
            return false;
        }

        public override bool IsPlusInfinity(double val)
        {
            return Double.IsPositiveInfinity(val);
        }

        public override bool IsMinusInfinity(double val)
        {
            return Double.IsNegativeInfinity(val);
        }

        public override double PlusInfinity
        {
            get { return Double.PositiveInfinity; }
        }

        public override double MinusInfinty
        {
            get { return Double.NegativeInfinity; }
        }

        public override bool TryAdd(double left, double right, out double result)
        {
            result = left + right;

            return true;
        }

        public override Interval_IEEE754 IntervalUnknown
        {
            get { return Interval_IEEE754.UnknownInterval; }
        }

        public override Interval_IEEE754 IntervalZero
        {
            get { return Interval_IEEE754.ZeroInterval_IEEE754; }
        }

        public override Interval_IEEE754 IntervalOne
        {
            get { return Interval_IEEE754.OneInterval_IEEE754; }
        }

        public override Interval_IEEE754 Interval_Positive
        {
            get { return Interval_IEEE754.PositiveInterval; }
        }

        public override Interval_IEEE754 Interval_StrictlyPositive
        {
            get { return this.IntervalRightOpen(1.0); }
        }

        public override Interval_IEEE754 IntervalGreaterEqualThanMinusOne
        {
            get { return Interval_IEEE754.For(-1, double.PositiveInfinity); }
        }

        public override Interval_IEEE754 IntervalSingleton(double val)
        {
            return Interval_IEEE754.For(val);
        }

        public override Interval_IEEE754 IntervalRightOpen(double inf)
        {
            return Interval_IEEE754.For(inf, double.PositiveInfinity);
        }

        public override Interval_IEEE754 IntervalLeftOpen(double sup)
        {
            return Interval_IEEE754.For(double.NegativeInfinity, sup);
        }

        public override bool AreEqual(Interval_IEEE754 left, Interval_IEEE754 right)
        {
            return left.IsNormal && right.IsNormal && left.LessEqual(right) && right.LessEqual(left);
        }

        public override Interval_IEEE754 Interval_Add(Interval_IEEE754 left, Interval_IEEE754 right)
        {
            return left + right;
        }

        public override Interval_IEEE754 Interval_Div(Interval_IEEE754 left, Interval_IEEE754 right)
        {
            return left / right;
        }

        public override Interval_IEEE754 Interval_Sub(Interval_IEEE754 left, Interval_IEEE754 right)
        {
            return left - right;
        }

        public override Interval_IEEE754 Interval_Mul(Interval_IEEE754 left, Interval_IEEE754 right)
        {
            return left * right;
        }

        public override Interval_IEEE754 Interval_UnaryMinus(Interval_IEEE754 left)
        {
            return -left;
        }

        public override Interval_IEEE754 Interval_Not(Interval_IEEE754 left)
        {
            double value;
            if (left.TryGetSingletonValue(out value) && value != 0.0)
            {
                return Interval_IEEE754.ZeroInterval_IEEE754;
            }

            return Interval_IEEE754.UnknownInterval;
        }

        public override Interval_IEEE754 Interval_Rem(Interval_IEEE754 left, Interval_IEEE754 right)
        {
            return left % right;
        }

        public override Interval_IEEE754 Interval_BitwiseAnd(Interval_IEEE754 left, Interval_IEEE754 right)
        {
            return left & right;
        }

        public override Interval_IEEE754 Interval_BitwiseOr(Interval_IEEE754 left, Interval_IEEE754 right)
        {
            return left | right;
        }

        public override Interval_IEEE754 Interval_BitwiseXor(Interval_IEEE754 left, Interval_IEEE754 right)
        {
            return left ^ right;
        }

        public override Interval_IEEE754 Interval_ShiftLeft(Interval_IEEE754 left, Interval_IEEE754 right)
        {
            return Interval_IEEE754.ShiftLeft(left, right);
        }

        public override Interval_IEEE754 Interval_ShiftRight(Interval_IEEE754 left, Interval_IEEE754 right)
        {
            return Interval_IEEE754.ShiftRight(left, right);
        }

        public override Interval_IEEE754 For(byte v)
        {
            return Interval_IEEE754.For(v);
        }

        public override Interval_IEEE754 For(short v)
        {
            return Interval_IEEE754.For(v);
        }

        public override Interval_IEEE754 For(int v)
        {
            return Interval_IEEE754.For(v);
        }

        public override Interval_IEEE754 For(long v)
        {
            return Interval_IEEE754.For(v);
        }

        public override Interval_IEEE754 For(sbyte s)
        {
            return Interval_IEEE754.For(s);
        }

        public override Interval_IEEE754 For(ushort u)
        {
            return Interval_IEEE754.For(u);
        }

        public override Interval_IEEE754 For(uint u)
        {
            return Interval_IEEE754.For(u);
        }

        public override Interval_IEEE754 For(Rational r)
        {
            return Interval_IEEE754.For(r);
        }

        public override Interval_IEEE754 For(double d)
        {
            return For(d, d);
        }

        public override Interval_IEEE754 For(double inf, double sup)
        {
            return Interval_IEEE754.For(inf, sup);
        }

        /// <summary>
        ///  Abstraction ... For the moment is the identity
        /// </summary>
        protected override Interval_IEEE754 ApplyConversion(ExpressionOperator conversionType, Interval_IEEE754 val)
        {
            switch (conversionType)
            {
                case ExpressionOperator.ConvertToFloat32:
                case ExpressionOperator.ConvertToFloat64:
                    return val;

                default:
                    return val.Top;
            }
        }

        protected override T To<T>(double n, IFactory<T> factory)
        {
            return factory.Constant(n);
        }

        protected override IntervalEnvironment_IEEE754<Variable, Expression> Factory()
        {
            return new IntervalEnvironment_IEEE754<Variable, Expression>(this.ExpressionManager);
        }

        public override DisInterval BoundsFor(Expression exp)
        {
            return ConvertInterval_IEEE754(this.Eval(exp));
        }

        public override DisInterval BoundsFor(Variable var)
        {
            Interval_IEEE754 res;
            if (this.TryGetValue(var, out res))
            {
                return ConvertInterval_IEEE754(res);
            }
            else
            {
                return ConvertInterval_IEEE754(Interval_IEEE754.UnknownInterval);
            }
        }

        protected override void AssumeInDisInterval_Internal(Variable x, DisInterval disIntv)
        {
            Rational r;
            if (disIntv.TryGetSingletonValue(out r))
            {
                Interval_IEEE754 value;
                long intValue;
                if (r.TryInt64(out intValue))
                {
                    value = Interval_IEEE754.For(intValue);
                }
                else
                {
                    value = Interval_IEEE754.For((double)r);
                }

                Interval_IEEE754 prev;
                if (this.TryGetValue(x, out prev))
                {
                    value = value.Meet(prev);
                }

                this[x] = value;
            }
        }

        protected override Interval_IEEE754 ConvertInterval(Interval intv)
        {
            return Interval_IEEE754.ConvertInterval(intv);
        }

        protected DisInterval ConvertInterval_IEEE754(Interval_IEEE754 intv)
        {
            return Interval_IEEE754.ConvertInterval(intv);
        }
    }
}
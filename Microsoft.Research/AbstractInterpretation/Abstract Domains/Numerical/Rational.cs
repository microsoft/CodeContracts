// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// This file contains an implementation for rational numbers

// Define this symbol if you want to profile rationals
//#define PROFILE_RATIONALS    

// Rationals implemented as structs ara way slower than classes
// #define STRUCTRATIONALS

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.AbstractDomains.Numerical
{
    /// <summary>
    /// A class for rational numbers.
    /// </summary>

    public
#if !STRUCTRATIONALS
    sealed class
#else
    struct
#endif
    Rational
    {
        #region State
        private enum Kind { Normal, PlusInfinty, MinusInfinity }

        #endregion

        #region Statics
        // Cached values, as they are used very often!
        static private readonly Rational Zero = new Rational((long)0);
        static private readonly Rational One = new Rational(1);
        static private readonly Rational MinusOne = new Rational(-1);

        static private readonly Rational plusInfinity = new Rational(Kind.PlusInfinty);
        static private readonly Rational minusInfinity = new Rational(Kind.MinusInfinity);

        static private readonly Rational maxValue = new Rational(long.MaxValue);
        static private readonly Rational minValue = new Rational(long.MinValue);

        static private readonly Rational[] cachedPowersOfTwo;

        [ThreadStatic]
        static private ulong allocated; // How many Rational we allocated?
        #endregion

        #region
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(kind != Kind.Normal || down != 0);
            Contract.Invariant(kind == Kind.Normal || down == 0);
        }
        #endregion

        #region Privates
        private readonly Kind kind;

        private readonly Int64 up;
        private readonly Int64 down;

        #endregion

        #region Static getters
        static public Rational PlusInfinity
        {
            get
            {
                Contract.Ensures(!object.Equals(Contract.Result<Rational>(), null));

                return plusInfinity;
            }
        }

        static public Rational MinusInfinity
        {
            get
            {
                Contract.Ensures(!object.Equals(Contract.Result<Rational>(), null));

                return minusInfinity;
            }
        }

        static public Rational MaxValue
        {
            get
            {
                Contract.Ensures(!object.Equals(Contract.Result<Rational>(), null));

                return maxValue;
            }
        }

        static public Rational MinValue
        {
            get
            {
                Contract.Ensures(!object.Equals(Contract.Result<Rational>(), null));

                return minValue;
            }
        }

        static public int MaxBits
        {
            get
            {
                return 64;
            }
        }

        internal static ulong Allocated
        {
            get
            {
                return allocated;
            }
        }

        #endregion

        #region Static constructor
        static Rational()
        {
            allocated = 0;

            #region Initialize the array with the powers of two
            cachedPowersOfTwo = new Rational[Rational.MaxBits - 1];

            var soFar = new Rational(1);
            var two = new Rational(2);

            // We unroll the loop, as the naif way of writing it causes a Aithmetic overflow at the last iteration
            cachedPowersOfTwo[0] = soFar;
            for (int i = 1; i < Rational.MaxBits - 1; i++)
            {
                soFar *= two;
                cachedPowersOfTwo[i] = soFar;
            }

            #endregion
        }
        #endregion

        #region Constructor

        private Rational(Kind kind)
        {
            Contract.Requires(kind != Kind.Normal);

            this.kind = kind;
            up = 0;
            down = 0;
        }

        /// <summary>
        /// Construct the rational number <code>numerator</code> / <code>denominator</code>
        /// </summary>
        private Rational(Int64 numerator, Int64 denominator)
        {
            Contract.Requires(!(numerator == 0 && denominator == 0), "Cannot build the rational 0/0 !!!");

            allocated++;

            // k / 0
            if (denominator == 0)
            {
                kind = numerator > 0 ? Kind.PlusInfinty : Kind.MinusInfinity;

                up = 0;
                down = 0;

                return;
            }

            Contract.Assert(denominator != 0);

            // 0 / k
            if (numerator == 0)
            {
                kind = Kind.Normal;

                up = 0;
                down = 1;

                IncreaseCount("0");

                return;
            }

            // The sign of the result

            var sign = Math.Sign(numerator) * Math.Sign(denominator);

            Contract.Assume(sign != 0);

            // Normalize
            if (numerator != Int64.MinValue)
            {
                numerator = sign * Math.Abs(numerator);
            }
            else
            {
                // Should be unreached 
                numerator = sign >= 0 ? Int64.MaxValue : Int64.MinValue;
            }

            if (denominator != Int64.MinValue)
            {
                denominator = Math.Abs(denominator);
                Contract.Assume(denominator > 0); // f: too weak postcondition on Abs. We need "x != 0 ==> result > 0"
            }
            else
            {
                numerator = 0;
                denominator = 1;
            }

            Contract.Assert(denominator > 0); // We want the invariant the sign is kept by the numerator

            kind = Kind.Normal;

            if (numerator % denominator == 0)
            {
                up = numerator / denominator;
                down = 1;
            }
            else
            {
                Int64 gcd =
                  numerator != 0 && denominator != 0 &&
                  numerator != Int64.MaxValue && numerator != Int64.MinValue && denominator != Int64.MaxValue
                  ? GCD(Math.Abs(numerator), Math.Abs(denominator)) : 1;


                up = (Int64)(numerator / gcd);
                down = (Int64)(denominator / gcd);
            }

            IncreaseCount(this.ToString());
        }

        private Rational(Int64 numerator)
        {
            allocated++;

            kind = Kind.Normal;

            up = numerator;
            down = 1;

            IncreaseCount(this.ToString());
        }

        #endregion

        #region Factory for rationals

        private static SimpleHashtable<Int64, Rational> cache = new SimpleHashtable<Int64, Rational>();

        /// <returns>A normal rational representing <code>i</code></returns>
        static public Rational For(Int64 i)
        {
            Contract.Ensures(((object)Contract.Result<Rational>()) != null);

            switch (i)
            {
                case 0:
                    return Zero;
                case 1:
                    return One;
                case -1:
                    return MinusOne;
                default:
                    {
                        Rational result;

                        if (cache.TryGetValue(i, out result))
                        {
                            Contract.Assume(((object)result) != null);      // F: need array
                            return result;
                        }

                        result = new Rational(i);
                        cache[i] = result;

                        return result;
                    }
            }
        }

        /// <returns>
        /// A rational representing <code>numerator / denominator</code>.
        /// If denominator == 0, an exception is thrown
        /// </returns>
        static internal Rational For(Int64 numerator, Int64 denominator)
        {
            Contract.Ensures((object)Contract.Result<Rational>() != null);

            switch (denominator)
            {
                case 0:
                    throw new ArithmeticExceptionRational();
                case 1:
                    return Rational.For(numerator);
                default:
                    return new Rational(numerator, denominator);
            }
        }

        static public Rational For(Byte b)
        {
            Contract.Ensures((object)Contract.Result<Rational>() != null);

            return Rational.For((Int64)b);
        }

        static public Rational For(UInt16 u16)
        {
            Contract.Ensures((object)Contract.Result<Rational>() != null);

            return Rational.For((Int64)u16);
        }

        static public Rational For(UInt32 u32)
        {
            Contract.Ensures((object)Contract.Result<Rational>() != null);

            return Rational.For((Int64)u32);
        }

        static public Rational For(Double f64)
        {
            Contract.Ensures((object)Contract.Result<Rational>() != null);

            // return Rational.For((Int32)Math.Truncate(f32));
            return Rational.For((Int64)Math.Truncate(f64));
        }
        #endregion

        #region Common Arithmetic functions (abs, min, max)

        /// <returns>
        /// The absolute value for <code>value</code>
        /// </returns>
        public static Rational Abs(Rational value)
        {
            Contract.Requires((object)value != null);
            Contract.Requires(!value.IsMinValue);

            Contract.Ensures((object)Contract.Result<Rational>() != null);

            if (value.IsPlusInfinity)
                return MinusInfinity;

            if (value.IsMinusInfinity)
                return PlusInfinity;

            if (value.IsZero || value > 0)
                return value;

            return -value;
        }

        /// <returns>
        /// The smallest value between <code>x</code> and <code>y</code>
        /// </returns>
        public static Rational Min(Rational x, Rational y)
        {
            Contract.Requires(((object)x) != null);
            Contract.Requires(((object)y) != null);
            Contract.Ensures((object)Contract.Result<Rational>() != null);

            return x < y ? x : y;
        }

        /// <returns>
        /// The largest value between <code>x</code> and <code>y</code>
        /// </returns>
        public static Rational Max(Rational x, Rational y)
        {
            Contract.Requires(((object)x) != null);
            Contract.Requires(((object)y) != null);
            Contract.Ensures(((object)Contract.Result<Rational>()) != null);

            return x < y ? y : x;
        }

        /// <summary>
        /// Computes <code>2 ^ k</code>
        /// </summary>
        /// <param name="k">Should be positive</param>
        public static Rational ToThePowerOfTwo(int k)
        {
            Contract.Requires(k >= 0);
            Contract.Ensures(((object)Contract.Result<Rational>()) != null);

            if (k >= Rational.MaxBits - 1)
            {
                throw new ArithmeticExceptionRational();
            }

            var result = Rational.cachedPowersOfTwo[k];

            Contract.Assume(((object)result) != null);    // F: We need arrays here

            return result;
        }

        public static bool CanRepresentExactly(float value)
        {
            if (Single.IsInfinity(value) || Single.IsNaN(value))
                return false;

            // It's an integral value?
            if (Math.Truncate(value).Equals(value))
            {
                return true;
            }

            return false;
        }

        public static bool CanRepresentExactly(Double value)
        {
            if (Double.IsInfinity(value) || Double.IsNaN(value))
                return false;

            // It's an integral value?
            if (Math.Truncate(value).Equals(value))
                return Int64.MinValue < value && value < Int64.MaxValue;

            return false;
        }

        #endregion

        #region Checking methods, NextInt32, PrevInt32, PlusInfinity, MinusInfinity

        /// <summary>
        /// A rational is Infinity if it is PlusInfinity or MinusInfinity
        /// </summary>
        public bool IsInfinity
        {
            get
            {
                Contract.Ensures(Contract.Result<bool>() || down != 0);

                return this.IsPlusInfinity || this.IsMinusInfinity;
            }
        }



        #endregion

        #region Implementation of the superclass abstract methods

        /// <summary>
        /// The sign of this Rational (can be +1 or -1)
        /// </summary>
        public int Sign
        {
            get
            {
                if (this.IsPlusInfinity)
                    return 1;
                else if (this.IsMinusInfinity)
                    return -1;
                else
                    return Math.Sign(down) * Math.Sign(up);
            }
        }

        /// <summary>
        /// Is true iff this rational is an integer number 
        /// </summary>
        public bool IsInteger
        {
            get
            {
                if (this.IsInfinity)
                {
                    return false;
                }
                Contract.Assert(down != 0);

                return (up % down) == 0;
            }
        }


        public bool IsInt32
        {
            get
            {
                if (!this.IsInteger)
                    return false;

                var value = up / down;
                return Int32.MinValue <= value && value <= Int32.MaxValue;
            }
        }

        /// <summary>
        /// Return the smallest <code>Int32</code> greater than this rational
        /// </summary>
        public Rational NextInt32
        {
            get
            {
                Contract.Ensures(((object)Contract.Result<Rational>()) != null);

                if (this.IsInfinity)
                    return this;

                var next = (Int64)Math.Ceiling((double)this);

                return next < Int32.MaxValue ? Rational.For(next) : Rational.For((Int64)Int32.MaxValue);
            }
        }

        public Rational NextInt64
        {
            get
            {
                Contract.Ensures(((object)Contract.Result<Rational>()) != null);

                if (this.IsInfinity)
                    return this;

                var next = Math.Ceiling((double)this);

                return next < Int64.MaxValue ? Rational.For(next) : Rational.For(Int64.MaxValue);
            }
        }


        /// <summary>
        /// Return the smallest <code>Int32</code> smaller than this rational
        /// </summary>
        public Rational PreviousInt32
        {
            get
            {
                Contract.Ensures(((object)Contract.Result<Rational>()) != null);

                if (this.IsInfinity)
                    return this;

                if (down == 1)
                {
                    return this;
                }

                var prev = (Int64)Math.Floor((double)this);

                return prev > Int32.MinValue ? Rational.For(prev) : Rational.For((Int64)Int32.MinValue);
            }
        }

        public Rational PreviousInt64
        {
            get
            {
                Contract.Ensures(((object)Contract.Result<Rational>()) != null);

                if (this.IsInfinity)
                    return this;

                if (down == 1)
                {
                    return this;
                }

                var prev = Math.Floor((double)this);

                return prev > Int64.MinValue ? Rational.For(prev) : Rational.For(Int64.MinValue);
            }
        }


        public double Ceiling
        {
            get
            {
                if (this.IsMinusInfinity)
                    return Double.NegativeInfinity;
                if (this.IsPlusInfinity)
                    return Double.PositiveInfinity;
                if (this.IsZero)
                    return +0.0;

                return Math.Ceiling(FloatingPointExtensions.Div_Up(up, down));
            }
        }

        public double Floor
        {
            get
            {
                if (this.IsMinusInfinity)
                    return Double.NegativeInfinity;
                if (this.IsPlusInfinity)
                    return Double.PositiveInfinity;
                if (this.IsZero)
                    return +0.0;

                return Math.Floor(FloatingPointExtensions.Div_Low(up, down));
            }
        }

        public bool IsZero
        {
            get
            {
                return kind == Kind.Normal && up == 0;
            }
        }

        public bool IsNotZero
        {
            get
            {
                return kind == Kind.Normal && up != 0;
            }
        }

        /// <summary>
        /// A rational is +oo when one of the two:
        ///  1. the denominator is 0 and the numerator is > 0
        ///  2. the denominator is 1 and the numerator is Int32.MaxValue
        /// </summary>
        public bool IsPlusInfinity
        {
            get
            {
                Contract.Ensures(!Contract.Result<bool>() || down == 0);
                Contract.Ensures(Contract.Result<bool>() == (kind == Kind.PlusInfinty));

                return kind == Kind.PlusInfinty;
            }
        }

        /// <summary>
        /// A rational is -oo when one of the two:
        ///  1. the denominator is 0 and the numerator is \less 0
        ///  2. the denominator is 1 and the numerator is Int32.MinValue
        /// </summary>
        public bool IsMinusInfinity
        {
            get
            {
                Contract.Ensures(!Contract.Result<bool>() || down == 0);
                Contract.Ensures(Contract.Result<bool>() == (kind == Kind.MinusInfinity));

                return kind == Kind.MinusInfinity;
            }
        }

        public bool IsMaxValue
        {
            get
            {
                return kind == Kind.Normal && up == long.MaxValue && down == 1;
            }
        }

        public bool IsMinValue
        {
            get
            {
                return kind == Kind.Normal && up == long.MinValue && down == 1;
            }
        }

        public Int64 Up
        {
            get
            {
                if (kind != Kind.Normal)
                {
                    throw new InvalidOperationException();
                }
                return up;
            }
        }

        public Int64 Down
        {
            get
            {
                if (kind != Kind.Normal)
                {
                    throw new InvalidOperationException();
                }
                return down;
            }
        }


        public Expression ToExpression<Variable, Expression>(IExpressionEncoder<Variable, Expression> encoder)
        {
            Contract.Requires(encoder != null);
            Contract.Ensures(Contract.Result<Expression>() != null);

            if (this.IsInteger)
            {
                return encoder.ConstantFor((long)this);
            }
            if (this.IsPlusInfinity)
            {
                return encoder.CompoundExpressionFor(ExpressionType.Int64, ExpressionOperator.Division, encoder.ConstantFor(1), encoder.ConstantFor(0));
            }
            if (this.IsMinusInfinity)
            {
                return encoder.CompoundExpressionFor(ExpressionType.Int64, ExpressionOperator.Division, encoder.ConstantFor(-1), encoder.ConstantFor(0));
            }
            else
            {
                var upAsExp = encoder.ConstantFor(up);
                var downAsExp = encoder.ConstantFor(down);

                return encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Division, upAsExp, downAsExp);
            }
        }

        public T To<T>(IFactory<T> factory)
        {
            if (this.IsInteger)
            {
                var asInt64 = this.NextInt64;
                if (Int32.MinValue <= asInt64 && asInt64 <= Int32.MaxValue)
                {
                    return factory.Constant((Int32)asInt64);
                }
                else
                {
                    return factory.Constant((Int64)asInt64);
                }
            }
            else
            {
                T upConst = factory.Constant(up);
                T downConst = factory.Constant(down);

                return factory.Div(upConst, downConst);
            }
        }

        #endregion

        #region Try* version of arithmetic operators

        #region TryAdd*
        public static bool TryAdd(Rational r1, Rational r2, out Rational result)
        {
            Contract.Requires(!object.ReferenceEquals(r1, null));
            Contract.Requires(!object.ReferenceEquals(r2, null));

            Contract.Ensures(!Contract.Result<bool>() || (!(object.ReferenceEquals(Contract.ValueAtReturn(out result), null))));

            // Special cases (Using IsZero, as it is faster than operator ==)
            // 0 + r2 = r2
            if (r1.IsZero)
            {
                result = r2;
                return true;
            }

            // r1 + 0 = r1
            if (r2.IsZero)
            {
                result = r1;
                return true;
            }

            // oo + r2 = oo
            if (r1.IsInfinity)
            {
                result = r1;
                return true;
            }

            // r1 + oo = oo
            if (r2.IsInfinity)
            {
                result = r2;
                return true;
            }
            // maxval + pos = infinity
            if ((r1.IsMaxValue && r2 > 0) || (r2.IsMaxValue && r1 > 0))
            {
                result = Rational.PlusInfinity;
                return true;
            }

            // minval + neg = -infinity
            if ((r1.IsMinValue && r2 < 0) || (r2.IsMinValue && r1 < 0))
            {
                result = Rational.MinusInfinity;
                return true;
            }

            Int64 returnUp;
            Int64 returnDown;

            try
            {
                if (r1.down == r2.down)
                {
                    // Special case for (a/b + a/b) = (a / (b/2)
                    if (r1.up == r2.up && r1.down % 2 == 0)
                    {
                        returnUp = r1.up;
                        returnDown = checked(r1.down / 2);
                    }
                    else
                    {
                        returnUp = checked(r1.up + r2.up);
                        returnDown = checked(r1.down);
                    }
                }
                else
                {
                    var tmp1 = checked(r1.up * r2.down);
                    var tmp2 = checked(r2.up * r1.down);
                    returnUp = checked(tmp1 + tmp2);

                    returnDown = checked(r1.down * r2.down);
                }
            }
            catch (ArithmeticException)
            {
                {
                    try
                    {
                        long gcd = GCD(r1.down, r2.down);

                        long tmp1 = checked(r1.up * (r2.down / gcd));
                        long tmp2 = checked(r2.up * (r1.down / gcd));

                        returnUp = checked(tmp1 + tmp2);
                        returnDown = checked(r1.down * r2.down);
                    }
                    catch (ArithmeticException)
                    {
                        result = default(Rational);
                        return false;
                    }
                }
            }

            // Small inlining, as operator+ is called so many times
            result = returnDown == 1 ? Rational.For(returnUp) : Rational.For(returnUp, returnDown);

            return true;
        }

        public static bool TryAdd(Rational r1, Int64 i, out Rational result)
        {
            Contract.Requires(!object.ReferenceEquals(r1, null));

            Contract.Ensures(!Contract.Result<bool>() || (!(object.ReferenceEquals(Contract.ValueAtReturn(out result), null))));

            if (r1.kind == Kind.Normal && r1.down == 1)
            {
                try
                {
                    result = Rational.For(checked(r1.up + i));
                }
                catch (ArithmeticException)
                { // slow path
                    return TryAdd(r1, Rational.For(i), out result);
                }

                return true;
            }
            else
            {
                return TryAdd(r1, Rational.For(i), out result);
            }
        }

        public static bool TryAdd(Int64 i, Rational r2, out Rational result)
        {
            Contract.Ensures(!Contract.Result<bool>() || ((object)Contract.ValueAtReturn(out result)) != null);

            return TryAdd(r2, i, out result);
        }
        #endregion

        public static bool TrySub(Rational r1, Rational r2, out Rational result)
        {
            Contract.Requires(!object.ReferenceEquals(r1, null));
            Contract.Requires(!object.ReferenceEquals(r2, null));

            Contract.Ensures(!Contract.Result<bool>() || (!(object.ReferenceEquals(Contract.ValueAtReturn(out result), null))));

            Int64 returnUp;
            Int64 returnDown;

            // r1 - 0 = r1
            if (r2.IsZero)
            {
                result = r1;
                return true;
            }

            // 0 - r2 = -r2
            if (r1.IsZero)
            {
                result = -r2;
                return true;
            }

            // r1 - r1 = 0
            if (r1 == r2)
            {
                result = Zero;
                return true;
            }

            // r1 - (-r2) = r1 + r2
            if (r2 < 0 && !r2.IsMinValue)
            {
                // result = r1 + Rational.Abs(r2);
                return TryAdd(r1, Rational.Abs(r2), out result);
            }

            // oo - r2 = oo
            if (r1.IsInfinity)
            {
                result = r1;
                return true;
            }

            // r1 - oo = -oo
            if (r2.IsInfinity)
            {
                result = -r2;
                return true;
            }

            if (r1.IsMinValue && r2 > 0)
            {
                result = Rational.MinusInfinity;
                return true;
            }

            try
            {
                // This optimization is to avoid the creation of large numbers (and also to save 3 multiplications)
                if (r1.down == r2.down)
                {
                    returnUp = checked(r1.up - r2.up);
                    returnDown = r1.down;
                }
                else
                {
                    long tmp1 = checked(r1.up * r2.down);
                    long tmp2 = checked(r2.up * r1.down);
                    returnUp = checked(tmp1 - tmp2);

                    returnDown = checked(r1.down * r2.down);
                }
            }
            catch (ArithmeticException)
            {
                result = default(Rational);
                return false;
            }

            result = Rational.For(returnUp, returnDown);
            return true;
        }

        // This is always going to succeed (because of our representation of Rationals), but to make the code easier to read, I made it a Try*
        public static bool TryUnaryMinus(Rational r, out Rational result)
        {
            Contract.Requires(!object.ReferenceEquals(r, null));

            Contract.Ensures(!Contract.Result<bool>() || (!(object.ReferenceEquals(Contract.ValueAtReturn(out result), null))));

            // -0 = 0
            if (r.IsZero)
            {
                result = r;
                return true;
            }

            // - (-oo) = oo
            if (r.IsMinusInfinity)
            {
                result = Rational.PlusInfinity;
                return true;
            }

            // - (+ oo) = -oo
            if (r.IsPlusInfinity)
            {
                result = Rational.MinusInfinity;
                return true;
            }

            if (r.IsMinValue)
            {
                result = Rational.MaxValue;
                return true;
            }

            if (r.IsMaxValue)
            {
                result = Rational.MinValue;
                return true;
            }
            if (r.down == 1)
            {
                result = Rational.For(-r.up);
                return true;
            }
            else
            {
                result = Rational.For(-r.up, r.down);
                return true;
            }
        }

        public static bool TryMul(Rational r1, Rational r2, out Rational result)
        {
            Contract.Requires(!object.ReferenceEquals(r1, null));
            Contract.Requires(!object.ReferenceEquals(r2, null));

            Contract.Ensures(!Contract.Result<bool>() || (!(object.ReferenceEquals(Contract.ValueAtReturn(out result), null))));

            // 0 * r2 = r1 * 0 = 0
            if (r1.IsZero || r2.IsZero)
            {
                result = Zero;
                return true;
            }

            // 1 * r2 = r2
            if (r1 == 1)
            {
                result = r2;
                return true;
            }

            // r1 * 1 = r1
            if (r2 == 1)
            {
                result = r1;
                return true;
            }

            if (r1.IsPlusInfinity)
            {
                if (r2.IsPlusInfinity)
                {
                    result = Rational.PlusInfinity;
                }
                else if (r2.IsMinusInfinity)
                {
                    result = Rational.MinusInfinity;
                }
                else
                {
                    result = r2.Sign > 0 ? Rational.PlusInfinity : (r2.Sign < 0 ? Rational.MinusInfinity : Zero);
                }
                return true;
            }

            if (r1.IsMinusInfinity)
            {
                if (r2.IsPlusInfinity)
                {
                    result = Rational.MinusInfinity;
                }
                else if (r2.IsMinusInfinity)
                {
                    result = Rational.PlusInfinity;
                }
                else
                {
                    result = r2.Sign > 0 ? Rational.MinusInfinity : (r2.Sign < 0 ? Rational.PlusInfinity : Zero);
                }
                return true;
            }
            if (r2.IsPlusInfinity)
            {
                Contract.Assume(!r1.IsInfinity);
                result = r1.Sign > 0 ? Rational.PlusInfinity : (r1.Sign < 0 ? Rational.MinusInfinity : Zero);
                return true;
            }

            if (r2.IsMinusInfinity)
            {
                Contract.Assume(!r1.IsInfinity);
                result = r1.Sign > 0 ? Rational.MinusInfinity : (r1.Sign < 0 ? Rational.PlusInfinity : Zero);
                return true;
            }

            // To do : add the entries with MaxValue

            Int64 returnUp;
            Int64 returnDown;

            try
            {
                // If we want to compute a/b * c/d we make the rational smaller by computing a/d * c/b
                var crossRational1 = Rational.For(r1.up, r2.down);
                var crossRational2 = Rational.For(r2.up, r1.down);

                returnUp = checked(crossRational1.up * crossRational2.up);
                returnDown = checked(crossRational1.down * crossRational2.down);
            }
            catch (ArithmeticException)
            {
                result = default(Rational);
                return false;
            }

            result = Rational.For(returnUp, returnDown);
            return true;
        }

        public static bool TryDiv(Rational r1, Rational r2, out Rational result)
        {
            Contract.Requires(!object.ReferenceEquals(r1, null));
            Contract.Requires(!object.ReferenceEquals(r2, null));

            Contract.Ensures(!Contract.Result<bool>() || (!(object.ReferenceEquals(Contract.ValueAtReturn(out result), null))));

            Int64 returnUp;
            Int64 returnDown;

            // r1 / 1 = r1 
            if (r2 == 1)
            {
                result = r1;
                return true;
            }
            // r2 / 0 = "overflow"
            if (r2.IsZero)
            {
                result = default(Rational);
                return false;
            }

            // 0 / r1 =0
            if (r1.IsZero)
            {
                result = Zero;
                return true;
            }

            // Note: At this point we know that r2 != 0;

            // + oo / k = sign(k) * oo
            if (r1.IsPlusInfinity && !r2.IsInfinity)
            {
                result = r2.Sign >= 0 ? Rational.PlusInfinity : Rational.MinusInfinity;
                return true;
            }

            // - oo / k = -1 * sign(k) * oo 
            if (r1.IsMinusInfinity && !r2.IsInfinity)
            {
                result = r2.Sign >= 0 ? Rational.MinusInfinity : Rational.PlusInfinity;
                return true;
            }
            // Note: At this point we know that r1 != 0 (and 1)

            // k / oo = 0
            if (r2.IsInfinity)
            {
                result = Zero;
                return true;
            }

            if (r1.up == r2.up)
            { // (a /b) / (a / c) = c / b
                returnUp = r2.down;
                returnDown = r1.down;
            }
            else if (r1.down == r2.down)
            { // (a / b) / (c / b) = a / c
                returnUp = r1.up;
                returnDown = r2.up;
            }
            else
            {
                try
                {
                    returnUp = checked(r1.up * r2.down);
                    returnDown = checked(r1.down * r2.up);
                }
                catch (ArithmeticException)
                {
                    result = default(Rational);
                    return false;
                }
            }

            result = Rational.For(returnUp, returnDown);
            return true;
        }

        #endregion

        #region Arithmetic operations

        public static Rational operator +(Rational r1, Rational r2)
        {
            Contract.Requires(!object.ReferenceEquals(r1, null));
            Contract.Requires(!object.ReferenceEquals(r2, null));

            Contract.Ensures(!object.ReferenceEquals(Contract.Result<Rational>(), null));

            Rational result;
            if (TryAdd(r1, r2, out result))
            {
                return result;
            }
            else
            {
                throw new ArithmeticExceptionRational();
            }
        }

        public static Rational operator -(Rational r1, Rational r2)
        {
            Contract.Requires(!object.ReferenceEquals(r1, null));
            Contract.Requires(!object.ReferenceEquals(r2, null));

            Contract.Ensures(!object.ReferenceEquals(Contract.Result<Rational>(), null));

            Rational result;
            if (TrySub(r1, r2, out result))
            {
                return result;
            }
            else
            {
                throw new ArithmeticExceptionRational();
            }
        }

        public static Rational operator -(Rational r)
        {
            Contract.Requires(!object.ReferenceEquals(r, null));

            Contract.Ensures(!object.ReferenceEquals(Contract.Result<Rational>(), null));

            Rational result;
            if (TryUnaryMinus(r, out result))
            {
                return result;
            }
            else
            {
                throw new ArithmeticExceptionRational();
            }
        }

        public static Rational operator *(Rational r1, Rational r2)
        {
            Contract.Requires((object)r1 != null);
            Contract.Requires((object)r2 != null);

            Contract.Ensures(((object)Contract.Result<Rational>()) != null);

            Rational result;
            if (TryMul(r1, r2, out result))
            {
                return result;
            }
            else
            {
                throw new ArithmeticExceptionRational();
            }
        }

        public static Rational MultiplyWithDefault(Rational r1, Rational r2, Rational def)
        {
            Contract.Requires((object)r1 != null);
            Contract.Requires((object)r2 != null);
            Contract.Requires((object)def != null);

            Contract.Ensures((object)Contract.Result<Rational>() != null);

            try
            {
                return r1 * r2;
            }
            catch (ArithmeticExceptionRational)
            {
                return def;
            }
        }


        public static Rational operator /(Rational r1, Rational r2)
        {
            Contract.Requires((object)r1 != null);
            Contract.Requires((object)r2 != null);

            Contract.Ensures(((object)Contract.Result<Rational>()) != null);

            Rational result;

            if (r2.IsZero)
            {
                throw new DivideByZeroException();
            }

            if (TryDiv(r1, r2, out result))
            {
                return result;
            }
            else
            {
                throw new ArithmeticExceptionRational();
            }
        }

        public static Rational DivideWithDefault(Rational r1, Rational r2, Rational def)
        {
            Contract.Requires((object)r1 != null);
            Contract.Requires((object)r2 != null);
            Contract.Requires((object)def != null);

            Contract.Ensures((object)Contract.Result<Rational>() != null);

            try
            {
                return r1 / r2;
            }
            catch (ArithmeticExceptionRational)
            {
                return def;
            }
        }

        #endregion

        #region Specialized arithmetic operations
        public static Rational operator +(Rational r, Int64 i)
        {
            Contract.Requires((object)r != null);

            Contract.Ensures((object)Contract.Result<Rational>() != null);

            Rational result;
            if (TryAdd(r, i, out result))
            {
                return result;
            }
            else
            {
                throw new ArithmeticExceptionRational();
            }
        }

        public static Rational operator +(Int64 i, Rational r)
        {
            Contract.Requires((object)r != null);

            Contract.Ensures((object)Contract.Result<Rational>() != null);

            return r + i;
        }

        public static Rational operator -(Rational r, Int64 i)
        {
            Contract.Requires((object)r != null);

            Contract.Ensures((object)Contract.Result<Rational>() != null);

            if (r.kind == Kind.Normal && r.down == 1)
            {
                try
                {
                    return Rational.For(checked(r.up - i));
                }
                catch (ArithmeticException)
                { // Slow path
                    return r - Rational.For(i);
                }
            }
            else
            {
                return r - Rational.For(i);
            }
        }

        public static Rational operator -(Int64 i, Rational r)
        {
            Contract.Requires((object)r != null);

            Contract.Ensures(((object)Contract.Result<Rational>()) != null);

            if (r.kind == Kind.Normal && r.down == 1)
            {
                try
                {
                    return Rational.For(checked(i - r.up));
                }
                catch (ArithmeticException)
                { // Slow path
                    return Rational.For(i) - r;
                }
            }
            else
            {
                return Rational.For(i) - r;
            }
        }

        static public Rational operator *(Rational r, Int64 i)
        {
            Contract.Requires((object)r != null);

            Contract.Ensures(((object)Contract.Result<Rational>()) != null);

            if (r.kind == Kind.Normal)
            {
                try
                {
                    return Rational.For(checked(r.up) * i, r.down);
                }
                catch (ArithmeticException)
                {
                    return r * Rational.For(i);
                }
            }
            else
            {
                return r * Rational.For(i);
            }
        }

        static public Rational operator *(Int64 i, Rational r)
        {
            Contract.Requires((object)r != null);

            Contract.Ensures(((object)Contract.Result<Rational>()) != null);

            return r * i;
        }

        static public Rational operator /(Rational r, Int64 i)
        {
            Contract.Requires((object)r != null);

            Contract.Ensures(((object)Contract.Result<Rational>()) != null);

            if (r.kind == Kind.Normal)
            {
                try
                {
                    return Rational.For(r.up, checked(r.down * i));
                }
                catch (ArithmeticException)
                {
                    return r / Rational.For(i);
                }
            }
            else
            {
                return r / Rational.For(i);
            }
        }

        #endregion

        #region Comparison operators
        public static bool operator ==(Rational r1, Rational r2)
        {
            Contract.Requires(!object.Equals(r1, null));
            Contract.Requires(!object.Equals(r2, null));

            // Some very common checks
            if (object.ReferenceEquals(r1, r2))
                return true;

            if (r1.IsZero)
                return r2.IsZero;

            if (r2.IsZero)
                return false;

            if (r1.IsPlusInfinity)
                return r2.IsPlusInfinity;

            if (r2.IsPlusInfinity)
                return false;

            if (r1.IsMinusInfinity)
                return r2.IsMinusInfinity;

            if (r2.IsMinusInfinity)
                return false;

            // Special case for syntactical equality 
            if (r1.up == r2.up && r1.down == r2.down)
                return true;

            // General case...
            try
            {
                return checked(r1.up * r2.down == r1.down * r2.up);
            }
            catch (ArithmeticException)
            {
                return ((decimal)r1.up / (decimal)r1.down) == ((decimal)r2.up / (decimal)r2.down);
            }
        }

        public static bool operator !=(Rational r1, Rational r2)
        {
            Contract.Requires(!object.Equals(r1, null));
            Contract.Requires(!object.Equals(r2, null));

            // Some very common checks
            if (object.ReferenceEquals(r1, r2))
                return false;

            if (r1.IsZero)
                return !r2.IsZero;

            if (r2.IsZero)
                return true;

            return !(r1 == r2);
        }

        public static bool operator <=(Rational r1, Rational r2)
        {
            if (object.ReferenceEquals(r1, r2))
            {
                return true;
            }

            if (r1.IsMinusInfinity || r2.IsPlusInfinity)
            {
                return true;
            }

            if (r1.IsPlusInfinity || r2.IsMinusInfinity)
            {
                return false;
            }

            try
            {
                if (r1.down == r2.down)
                    return r1.up <= r2.up;

                return checked(r1.up * r2.down <= r1.down * r2.up);
            }
            catch (ArithmeticException)
            {
                return ((decimal)r1.up) / ((decimal)r1.down) <= ((decimal)r2.up) / ((decimal)r2.down);
            }
        }

        public static bool operator >=(Rational r1, Rational r2)
        {
            return r2 <= r1;
        }

        public static bool operator <(Rational r1, Rational r2)
        {
            if (object.ReferenceEquals(r1, r2))
                return false;

            if (r1.IsMinusInfinity && !r2.IsMinusInfinity)
                return true;

            if (r2.IsPlusInfinity && !r1.IsPlusInfinity)
                return true;

            if (r1.IsPlusInfinity || r2.IsMinusInfinity)
                return false;

            else
            {
                // Special cases to make it faster?  (avoid the multiplications)
                if (r1.down == r2.down)
                    return r1.up < r2.up;

                if (r1.up <= 0 && r2.up > 0)
                    return true;

                if (r1.up < 0 && r2.up == 0)
                    return true;

                try
                {
                    // else
                    {
                        return checked((Int64)r1.up * (Int64)r2.down < (Int64)r1.down * (Int64)r2.up);
                    }
                }
                catch (ArithmeticException)
                {
                    return ((decimal)r1.up / (decimal)r1.down) < ((decimal)r2.up) / ((decimal)r2.down);
                }
            }
        }

        public static bool operator >(Rational r1, Rational r2)
        {
            return r2 < r1;
        }

        #endregion

        #region Specialized comparison operators
        public static bool operator ==(Rational r, Int64 i)
        {
            Contract.Requires((object)r != null);

            switch (r.kind)
            {
                case Kind.MinusInfinity:
                case Kind.PlusInfinty:
                    return false;

                case Kind.Normal:
                default:
                    try
                    {
                        return checked(r.up == i * r.down);
                    }
                    catch (ArithmeticException)
                    {
                        return checked((decimal)r.up / (decimal)r.down == i);
                    }
            }
        }

        public static bool operator ==(Int64 i, Rational r)
        {
            Contract.Requires(((object)r) != null);

            return r == i;
        }

        public static bool operator !=(Rational r, Int64 i)
        {
            Contract.Requires((object)r != null);

            switch (r.kind)
            {
                case Kind.MinusInfinity:
                case Kind.PlusInfinty:
                    return false;

                case Kind.Normal:
                default:
                    try
                    {
                        return checked(r.up != i * r.down);
                    }
                    catch (ArithmeticException)
                    {
                        return checked((decimal)r.up / (decimal)r.down != i);
                    }
            }
        }

        public static bool operator !=(Int64 i, Rational r)
        {
            Contract.Requires((object)r != null);
            return r != i;
        }

        public static bool operator <=(Rational r, Int64 i)
        {
            Contract.Requires((object)r != null);

            switch (r.kind)
            {
                case Kind.MinusInfinity:
                    return true;

                case Kind.PlusInfinty:
                    return false;

                case Kind.Normal:
                default:
                    try
                    {
                        return checked(r.up <= r.down * i);
                    }
                    catch (ArithmeticException)
                    {
                        return checked((decimal)r.up / (decimal)r.down <= i);
                    }
            }
        }

        public static bool operator <=(Int64 i, Rational r)
        {
            Contract.Requires((object)r != null);

            switch (r.kind)
            {
                case Kind.MinusInfinity:
                    return false;

                case Kind.PlusInfinty:
                    return true;

                case Kind.Normal:
                default:
                    try
                    {
                        return checked(i * r.down <= r.up);
                    }
                    catch (ArithmeticException)
                    {
                        return checked((i <= (decimal)r.up / (decimal)r.down));
                    }
            }
        }

        public static bool operator >=(Rational r, Int64 i)
        {
            Contract.Requires((object)r != null);

            return i <= r;
        }

        public static bool operator >=(Int64 i, Rational r)
        {
            Contract.Requires((object)r != null);

            return r <= i;
        }

        public static bool operator <(Int64 i, Rational r)
        {
            switch (r.kind)
            {
                case Kind.MinusInfinity:
                    return false;

                case Kind.PlusInfinty:
                    return true;

                case Kind.Normal:
                default:
                    try
                    {
                        return checked(i * r.down < r.up);
                    }
                    catch
                    {
                        return checked(i < (decimal)r.up / (decimal)r.down);
                    }
            }
        }

        public static bool operator <(Rational r, Int64 i)
        {
            switch (r.kind)
            {
                case Kind.MinusInfinity:
                    return true;

                case Kind.PlusInfinty:
                    return false;

                case Kind.Normal:
                default:
                    try
                    {
                        return checked(r.up < i * r.down);
                    }
                    catch (ArithmeticException)
                    {
                        return checked(((decimal)r.up / (decimal)r.down) < i);
                    }
            }
        }

        public static bool operator >(Rational r, Int64 i)
        {
            return i < r;
        }

        public static bool operator >(Int64 i, Rational r)
        {
            return r < i;
        }
        #endregion

        #region InRange
        public bool IsInRange(Rational low, Rational upp)
        {
            return low <= this && this <= upp;
        }

        public bool IsInRange(Int32 low, Int32 upp)
        {
            return low <= this && this <= upp;
        }

        public bool IsInRange(Int64 low, Int64 upp)
        {
            return low <= this && this <= upp;
        }

        public bool IsInRange(UInt32 low, UInt32 upp)
        {
            return low <= this && this <= upp;
        }

        #endregion

        #region Conversion Operators (casts to and from Int32 and double)

        private static Dictionary<object, ulong> conversions;

        // This method is quite resource demanding, and we are using it just to profile the allocation of new Rationals
        [Conditional("PROFILE_RATIONALS")]
        public static void IncreaseCount(object o)
        {
            if (conversions == null)
            {
                conversions = new Dictionary<object, ulong>();
            }

            if (conversions.ContainsKey(o))
            {
                conversions[o] = conversions[o] + 1;
            }
            else
            {
                conversions[o] = 1;
            }
        }

        /// <summary>
        /// Some performance statistics on rationals
        /// </summary>
        public static string Statistics
        {
            get
            {
                var result = new StringBuilder();

                result.AppendFormat("# of allocated rationals : {0}" + Environment.NewLine, Rational.Allocated);
                result.AppendFormat("# of thrown exceptions : {0}" + Environment.NewLine, ArithmeticExceptionRational.ThrownExceptions);

                if (conversions != null)
                {
                    var array = new KeyValuePair<object, ulong>[conversions.Count];

                    var count = 0;
                    foreach (var pair in conversions)
                    {
                        array[count++] = pair;
                    }

                    Array.Sort(array, delegate (KeyValuePair<object, ulong> x, KeyValuePair<object, ulong> y) { return x.Value > y.Value ? -1 : 1; });

                    ulong sum = 0;
                    foreach (var pair in array)
                    {
                        result.AppendFormat("{0} -> {1}\n", pair.Key.ToString(), pair.Value.ToString());
                        sum += pair.Value;
                    }
                    result.AppendFormat("(sum of the above: {0})", sum);
                }

                return result.ToString();
            }
        }

        ///<summary>
        ///By default does the rounding to the closest Int32 value
        ///</summary>
        public static explicit operator Int32(Rational r)
        {
            if (r.down == 0)
                return r.up < 0 ? Int32.MaxValue : Int32.MinValue;

            else
                return (Int32)Math.Round((double)r.up / (double)r.down);
        }


        public static explicit operator Int64(Rational r)
        {
            if (r.down == 0)
                return r.up < 0 ? Int64.MaxValue : Int64.MinValue;
            else
                return r.IsInteger ? r.up : (Int64)Math.Round((double)r.up / (double)r.down);
        }

        public bool TryInt64(out Int64 value)
        {
            if (this.IsInfinity)
            {
                value = default(Int64);
                return false;
            }

            value = this.IsInteger ? up : (Int64)Math.Round((double)up / (double)down);
            return true;
        }

        public static explicit operator Double(Rational r)
        {
            if (r.IsPlusInfinity)
                return Double.PositiveInfinity;
            if (r.IsMinusInfinity)
                return Double.NegativeInfinity;

            return (double)r.up / (double)r.down;
        }

        public static Interval ConvertFromDouble(Double d)
        {
            Contract.Ensures(Contract.Result<Interval>() != null);

            if (d.Equals(Double.NaN))
            {
                return Interval.UnknownInterval;
            }

            long l = (long)d;
            if (d == (Double)l)
            {
                return Interval.For(Rational.For(l, 1));
            }
            else
            {
                Interval candidate;
                if (d > 0)
                {
                    candidate = Interval.For(Rational.For(l, 1), Rational.For(l + 1, 1));
                }
                else
                {
                    candidate = Interval.For(Rational.For(l - 1, 1), Rational.For(l, 1));
                }

                // Check for conversion errors
                if (candidate.LowerBound.Ceiling > d || candidate.UpperBound.Floor < d)
                {
                    return Interval.UnknownInterval;
                }

                return candidate;
            }
        }

        #endregion

        #region Overridden methods

        public override bool Equals(object obj)
        {
#if STRUCTRATIONALS
            if (!(obj is Rational))
                return false;

            return this == (Rational)obj;
#else
            Rational r2 = obj as Rational;
            if ((object)r2 != (object)null) return this == r2;
            return false;
#endif
        }

        public override int GetHashCode()
        {
            return (int)(down + up);
        }

        #endregion

        #region The Euclide's algorithm
        //private static Int64 GCD_Internal(Int64 x, Int64 y)
        //{
        //  Contract.Requires(x > 0);
        //  Contract.Requires(y > 0);

        //  Contract.Ensures(Contract.Result<Int64>() > 0);

        //  while (true)
        //  {
        //    if (x < y)
        //    {
        //      y %= x;
        //      if (y == 0)
        //      {
        //        return x;
        //      }
        //    }
        //    else
        //    {
        //      x %= y;
        //      if (x == 0)
        //      {
        //        return y;
        //      }
        //    }
        //  }
        //}

        public static Int64 GCD(Int64 x, Int64 y)
        {
            Contract.Requires(x > 0);
            Contract.Requires(y > 0);

            Contract.Ensures(Contract.Result<Int64>() > 0);

            var u = (UInt64)x;
            var v = (UInt64)y;

            int shift;

            for (shift = 0; ((u | v) & 1) == 0; shift++)
            {
                u >>= 1;
                v >>= 1;
            }

            while ((u & 1) == 0)
            {
                u >>= 1;
            }

            do
            {
                while ((v & 1) == 0)
                {
                    v >>= 1;
                }

                if (u < v)
                {
                    v -= u;
                }
                else
                {
                    var diff = u - v;
                    u = v;
                    v = diff;
                }
                v >>= 1;
            } while (v != 0);

            var r1 = (Int64)u << shift;


            Contract.Assume(r1 > 0); // F: the reason for that are quite complicated arithmetical properties
            return r1;
        }

        #endregion

        #region Private methods

        private static Int64 Abs(Int64 value)
        {
            if (value >= 0)
                return value;
            else if (value == Int64.MinValue)
                return Int64.MaxValue;
            else
                return -value;
        }



        #endregion

        #region ToString
        public override string ToString()
        {
            if (this.IsMinusInfinity)
                return "-oo" + ((up != -1 && down != 0) ? string.Format("({0} / {1})", up, down) : "");
            else if (this.IsPlusInfinity)
                return "+oo" + ((up != 1 && down != 0) ? string.Format("({0} / {1})", up, down) : "");
            else if (this.IsInteger)
                return (up).ToString();
            else
                return up + "/" + down;
        }
        #endregion
    }

    [Serializable]
    public class ArithmeticExceptionRational
      : ArithmeticException
    {
        #region Private state
        [ThreadStatic]
        private static uint exceptions;
        #endregion

        #region Constructor
        public ArithmeticExceptionRational()
        {
            exceptions++;
        }
        #endregion

        #region Getters
        /// <summary>
        /// The number of Rational exceptions thrown so far
        /// </summary>
        static public uint ThrownExceptions
        {
            get
            {
                return exceptions;
            }
        }
        #endregion

    }
}


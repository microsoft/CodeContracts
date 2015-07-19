// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// The implementation of the intervals

// #define TRACE_PERFORMANCE

#define CACHE

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Text;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.AbstractDomains.Numerical
{
#if DEBUG
    using ReadOnlyIntervalList = ReadOnlyCollection<Interval>;
#else
    using ReadOnlyIntervalList = List<Interval>;
#endif
    /// <summary>
    /// An interval is a pair<code>(LowerBound, UpperBound)</code>.
    /// LowerBound and UpperBound are rational numbers over <code>Int32</code>.
    /// They can be +oo or -oo
    /// </summary>
    [ContractVerification(true)]
    public sealed class Interval
     : IntervalBase<Interval, Rational>
    {
        #region Private state
        readonly private bool isBottom;
        readonly private bool isTop;
        #endregion

        #region Static Constructor

#if CACHE
        static private readonly Dictionary<Pair<Rational, Rational>, Interval> common;
#endif

        static private readonly Dictionary<Pair<Rational, Rational>, int> countIntv;

        private static readonly Interval cachedBottom;
        private static readonly Interval cachedTop;
        private static readonly Interval cachedPositiveInterval;
        private static readonly Interval cachedNegativeInterval;

        static Interval()
        {
            countIntv = new Dictionary<Pair<Rational, Rational>, int>();

            common = new Dictionary<Pair<Rational, Rational>, Interval>();

            common.Add(new Pair<Rational, Rational>(Rational.For(0), Rational.For(0)), new Interval(Rational.For(0), Rational.For(0)));
            common.Add(new Pair<Rational, Rational>(Rational.For(1), Rational.For(1)), new Interval(Rational.For(1), Rational.For(1)));
            common.Add(new Pair<Rational, Rational>(Rational.For(0), Rational.PlusInfinity), new Interval(Rational.For(0), Rational.PlusInfinity));
            common.Add(new Pair<Rational, Rational>(Rational.MinusInfinity, Rational.For(0)), new Interval(Rational.MinusInfinity, Rational.For(0)));
            common.Add(new Pair<Rational, Rational>(Rational.MinusInfinity, Rational.PlusInfinity), new Interval(Rational.MinusInfinity, Rational.PlusInfinity));

            cachedBottom = new Interval(Rational.PlusInfinity, Rational.MinusInfinity);
            cachedTop = new Interval(Rational.MinusInfinity, Rational.PlusInfinity);
            cachedPositiveInterval = new Interval(0, Rational.PlusInfinity);
            cachedNegativeInterval = new Interval(Rational.MinusInfinity, 0);
        }

        public static Interval UnknownInterval
        {
            get
            {
                Contract.Ensures(Contract.Result<Interval>() != null);

                return cachedTop;
            }
        }

        public static Interval UnreachedInterval
        {
            get
            {
                Contract.Ensures(Contract.Result<Interval>() != null);

                return cachedBottom;
            }
        }


        public static Interval PositiveInterval
        {
            get
            {
                Contract.Ensures(Contract.Result<Interval>() != null);

                return cachedPositiveInterval;
            }
        }

        public static Interval NegativeInterval
        {
            get
            {
                Contract.Ensures(Contract.Result<Interval>() != null);

                return cachedNegativeInterval;
            }
        }


        #endregion

        #region For

        static public Interval For(Rational lower, Rational upper)
        {
            Contract.Requires(!object.Equals(lower, null));
            Contract.Requires(!object.Equals(upper, null));

            Contract.Ensures(Contract.Result<Interval>() != null);

#if CACHE
            var key = new Pair<Rational, Rational>(lower, upper);

            Interval val;

            if (common.TryGetValue(key, out val))
            {
                Contract.Assume(val != null);

                return val;
            }

#endif
            var res = new Interval(lower, upper);

            return res;
        }

        static public Interval For(Rational r)
        {
            Contract.Requires(!object.Equals(r, null));

            Contract.Ensures(Contract.Result<Interval>() != null);

            return For(r, r);
        }

        static public Interval For(Double lower, Double upper, bool dummy)
        {
            Contract.Ensures(Contract.Result<Interval>() != null);

            // We do not cache intervals for doubles
            return new Interval(lower, upper, dummy);
        }

        static public Interval For(Int64 lower, Rational upper)
        {
            Contract.Requires((object)upper != null);

            Contract.Ensures(Contract.Result<Interval>() != null);

            return For(Rational.For(lower), upper);
        }

        static public Interval For(Rational lower, Int64 upper)
        {
            Contract.Requires((object)lower != null);

            Contract.Ensures(Contract.Result<Interval>() != null);

            return For(lower, Rational.For(upper));
        }

        static public Interval For(Int64 lower, Int64 upper)
        {
            Contract.Ensures(Contract.Result<Interval>() != null);

            return For(Rational.For(lower), Rational.For(upper));
        }

        static public Interval For(UInt32 lower, UInt32 upper)
        {
            Contract.Ensures(Contract.Result<Interval>() != null);

            return For(Rational.For(lower), Rational.For(upper));
        }

        static public Interval For(Int64 i)
        {
            Contract.Ensures(Contract.Result<Interval>() != null);

            var r = Rational.For(i);
            return For(r, r);
        }

        // [SuppressMessage("Microsoft.Contracts", "Ensures-26-45", Justification="We know that getting an interval from a byte always gives a normal interval")]
        static public Interval For(Byte i)
        {
            Contract.Ensures(Contract.Result<Interval>() != null);
            Contract.Ensures(Contract.Result<Interval>().IsNormal);

            var r = Rational.For(i);

            return For(r, r);
        }

        static public Interval For(UInt32 i)
        {
            Contract.Ensures(Contract.Result<Interval>() != null);

            var r = Rational.For(i);
            return For(r, r);
        }

        static public Interval For(UInt64 i)
        {
            Contract.Ensures(Contract.Result<Interval>() != null);

            var r = Rational.For(i);
            return For(r, r);
        }

        #endregion

        #region Constructors
        private Interval(Int64 lower, Rational upper)
          : this(Rational.For(lower), upper)
        {
            Contract.Requires(((object)upper) != null);
        }

        private Interval(Rational lower, Int64 upper)
          : this(lower, Rational.For(upper))
        {
            Contract.Requires(((object)lower) != null);
        }

        private Interval(Int64 lower, Int64 upper)
          : this(Rational.For(lower), Rational.For(upper))
        {
        }

        public static string Stats
        {
            get
            {
                var x = new Pair<int, Pair<Rational, Rational>>[countIntv.Count];
                int i = 0;

                foreach (var pair in countIntv)
                {
                    Contract.Assert(x.Length == countIntv.Count);
                    // F: we should assume it because we have no way of relating the iteration counter, i  and the enumerator for countIntv
                    Contract.Assume(i < x.Length);

                    x[i] = new Pair<int, Pair<Rational, Rational>>(pair.Value, pair.Key);
                    i++;
                }

                Array.Sort(x, (k1, k2) => k1.One < k2.One ? 1 : (k1.One == k2.One ? 0 : -1));

                StringBuilder res = new StringBuilder();

                res.Append("Intervals allocation statistics" + Environment.NewLine);

                res.AppendFormat("Total allocated intervals: {0}" + Environment.NewLine, x.Length);

                for (i = 0; i < x.Length; i++)
                {
                    res.AppendFormat("[{0},{1}] : {2}" + Environment.NewLine, x[i].Two.One, x[i].Two.Two, x[i].One);
                }

                return res.ToString();
            }
        }

        [Conditional("TRACE_PERFORMANCE")]
        private void UpdateStatistics(Rational lower, Rational upper)
        {
            // begin debug
            int val;
            if (countIntv.TryGetValue(new Pair<Rational, Rational>(lower, upper), out val))
            {
                val++;
            }
            else
            {
                val = 1;
            }

            countIntv[new Pair<Rational, Rational>(lower, upper)] = val;
            // end debug
        }

        private Interval(Rational lower, Rational upper)
          : base(lower, upper)
        {
            Contract.Requires(!object.Equals(lower, null));
            Contract.Requires(!object.Equals(upper, null));

            UpdateStatistics(lower, upper);

            if (lower.IsPlusInfinity && upper.IsPlusInfinity) // the case [+oo, +oo]
            {
                this.lowerBound = lower - 1;
                this.upperBound = upper;
            }
            else if (lower.IsMinusInfinity && upper.IsMinusInfinity) // the case [-oo, -oo]
            {
                this.lowerBound = lower;
                this.upperBound = Rational.PlusInfinity;

                Contract.Assert(!object.Equals(this.lowerBound, null));
                Contract.Assert(!object.Equals(this.upperBound, null));
            }
            else
            {
                this.lowerBound = lower;
                this.upperBound = upper;
            }
            isBottom = this.lowerBound > this.upperBound;
            isTop = this.lowerBound.IsMinusInfinity && this.upperBound.IsPlusInfinity;

            Contract.Assert(!object.Equals(this.lowerBound, null));
            Contract.Assert(!object.Equals(this.upperBound, null));
        }

        private Interval(Double inf, Double sup, bool dummy)
          : base(Rational.MinusInfinity, Rational.PlusInfinity)
        {
            if (Double.IsNaN(inf) || Double.IsNaN(sup))
            {
                this.lowerBound = Rational.MinusInfinity;
                this.upperBound = Rational.PlusInfinity;
                return;
            }

            if (Double.IsNegativeInfinity(inf))
            {
                this.lowerBound = Rational.MinusInfinity;
            }
            else
            {
                var tmp = (Int64)Math.Floor(inf);

                // Overflow checking
                if (OppositeSigns(tmp, inf))
                {
                    this.lowerBound = Rational.MinusInfinity;
                }
                else
                {
                    this.lowerBound = Rational.For(tmp);
                }
            }

            if (Double.IsPositiveInfinity(sup))
            {
                this.upperBound = Rational.PlusInfinity;
            }
            else
            {
                var tmp = (Int64)Math.Ceiling(sup);
                if (OppositeSigns(tmp, sup))
                {
                    this.upperBound = Rational.PlusInfinity;
                }
                else
                {
                    this.upperBound = Rational.For(tmp);
                }
            }
            isBottom = this.lowerBound > this.upperBound;
            isTop = this.LowerBound.IsMinusInfinity && this.UpperBound.IsPlusInfinity;
        }

        private Interval(Rational r)
          : this(r, r)
        {
            Contract.Requires((object)r != null);
        }


        private Interval(uint lower, uint upper)
          : this(Rational.For(lower), Rational.For(upper))
        { }

        /// <summary>
        /// Useful to check overflows
        /// </summary>
        private bool OppositeSigns(long tmp, double inf)
        {
            return (tmp < 0 && inf > 0) || (tmp > 0 && inf < 0);
        }
        #endregion

        #region Overridden

        override public bool IsLowerBoundMinusInfinity
        {
            get
            {
                return this.LowerBound.IsMinusInfinity;
            }
        }

        override public bool IsUpperBoundPlusInfinity
        {
            get
            {
                return this.UpperBound.IsPlusInfinity;
            }
        }

        public override bool IsNormal
        {
            get { return !this.IsBottom && !this.IsTop; }
        }

        public override bool IsInt32
        {
            get
            {
                if (this.IsNormal)
                {
                    try
                    {
                        var blow = this.IsLowerBoundMinusInfinity || this.lowerBound >= Int32.MinValue;
                        var bupp = this.IsUpperBoundPlusInfinity || this.upperBound <= Int32.MaxValue;

                        return blow && bupp;
                    }
                    catch (ArithmeticException)
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public override bool IsInt64
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

        #region Interval Members

        public bool IsFiniteAndInt64(out Int64 low, out Int64 upp)
        {
            if (base.IsFinite && this.LowerBound.IsInteger && this.UpperBound.IsInteger)
            {
                try
                {
                    checked
                    {
                        low = (Int64)this.LowerBound;
                        upp = (Int64)this.UpperBound;
                    }
                    return true;
                }
                catch (ArithmeticException)
                {
                    low = upp = default(Int64);
                    return false;
                }
            }

            low = upp = default(Int64);
            return false;
        }

        public bool IsFiniteAndInt32(out Int32 low, out Int32 upp)
        {
            if (base.IsFinite && this.LowerBound.IsInt32 && this.UpperBound.IsInt32)
            {
                try
                {
                    checked
                    {
                        low = (Int32)this.LowerBound;
                        upp = (Int32)this.UpperBound;
                    }
                    return true;
                }
                catch (ArithmeticException)
                {
                    low = upp = default(Int32);
                    return false;
                }
            }

            low = upp = default(Int32);
            return false;
        }

        public bool IsFiniteAndInt32Singleton(out Int32 value)
        {
            int low, upp;
            if (this.IsFiniteAndInt32(out low, out upp) && low == upp)
            {
                value = low;
                return true;
            }

            value = -567895;
            return false;
        }

        public bool IsFiniteAndInt64Singleton(out Int64 value)
        {
            long low, upp;
            if (this.IsFiniteAndInt64(out low, out upp) && low == upp)
            {
                value = low;
                return true;
            }

            value = -567895;
            return false;
        }


        /// <returns>
        /// true iff <code>x</code> is not included in this interval
        /// </returns>
        public bool DoesNotInclude(int x)
        {
            return !this.IsBottom && (x < this.LowerBound || x > this.UpperBound);
        }

        public bool DoesInclude(int x)
        {
            return this.IsNormal && (this.LowerBound <= x && x <= this.UpperBound);
        }

        public bool OverlapsWith(Interval other)
        {
            Contract.Requires(other != null);

            return !this.Meet(other).IsBottom;
        }

        public bool OnTheLeftOf(Interval other)
        {
            Contract.Requires(other != null);

            if (!this.IsNormal || !other.IsNormal)
            {
                return false;
            }

            return this.UpperBound <= other.LowerBound;
        }

        #endregion

        #region Arithmetic operations on Intervals

        /// <summary>
        /// The addition of intervals.
        /// If the evaluation causes an overflow, the top interval is returned
        /// </summary>
        /// <returns></returns>
        static public Interval operator +(Interval left, Interval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<Interval>() != null);

            if (left.IsBottom || right.IsBottom)    // Propagate bottom
                return UnreachedInterval;

            if (left.IsTop || right.IsTop)          // Propagate top
                return UnknownInterval;

            Rational lower, upper;

            if (Rational.TryAdd(left.LowerBound, right.LowerBound, out lower)
              && Rational.TryAdd(left.UpperBound, right.UpperBound, out upper))
            {
                return Interval.For(lower, upper);
            }
            else
            {
                return UnknownInterval;
            }
        }

        static public Interval operator -(Interval left, Interval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<Interval>() != null);

            if (left.IsBottom || right.IsBottom)    // Propagate bottom
                return left.Bottom;

            if (left.IsTop || right.IsTop)          // Propagate top
                return UnknownInterval;

            Rational lower, upper;

            if (Rational.TrySub(left.LowerBound, right.UpperBound, out lower)
              && Rational.TrySub(left.UpperBound, right.LowerBound, out upper))
            {
                return new Interval
                  (
                    lower.IsPlusInfinity ? Rational.MinusInfinity : lower,
                    upper.IsMinusInfinity ? Rational.PlusInfinity : upper
                  );
            }
            else
            {
                return UnknownInterval;
            }
        }

        static public Interval operator -(Interval left)
        {
            Contract.Requires(left != null);

            Contract.Ensures(Contract.Result<Interval>() != null);

            if (!left.IsNormal)
            {
                return left;
            }
            else
            {
                return Interval.For(-left.UpperBound, -left.LowerBound);
            }
        }

        static public Interval operator *(Interval left, Interval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<Interval>() != null);

            if (left.IsBottom || right.IsBottom)    // Propagate bottom
                return UnreachedInterval;

            if (left.IsTop || right.IsTop)          // Propagate top
                return UnknownInterval;

            Rational llrl, lurl, llru, luru;
            Rational lower, upper;

            if (Rational.TryMul(left.LowerBound, right.LowerBound, out llrl)
              && Rational.TryMul(left.UpperBound, right.LowerBound, out lurl)
              && Rational.TryMul(left.LowerBound, right.UpperBound, out llru)
              && Rational.TryMul(left.UpperBound, right.UpperBound, out luru))
            {
                lower = Rational.Min(Rational.Min(llrl, lurl), Rational.Min(llru, luru));
                upper = Rational.Max(Rational.Max(llrl, lurl), Rational.Max(llru, luru));

                return Interval.For(lower, upper);
            }
            else
            {
                return UnknownInterval;
            }
        }

        static public Interval operator /(Interval left, Interval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<Interval>() != null);

            if (left.IsBottom || right.IsBottom)    // Propagate bottom
                return UnreachedInterval;

            if (left.IsTop || right.IsTop)          // Propagate top
                return UnknownInterval;

            Rational llrl, lurl, llru, luru;
            Rational lower, upper;

            if (right.LowerBound.IsZero || right.UpperBound.IsZero)
            {
                return UnknownInterval;
            }

            if (Rational.TryDiv(left.LowerBound, right.LowerBound, out llrl)
              && Rational.TryDiv(left.UpperBound, right.LowerBound, out lurl)
              && Rational.TryDiv(left.LowerBound, right.UpperBound, out llru)
              && Rational.TryDiv(left.UpperBound, right.UpperBound, out luru))
            {
                lower = Rational.Min(Rational.Min(llrl, lurl), Rational.Min(llru, luru));
                upper = Rational.Max(Rational.Max(llrl, lurl), Rational.Max(llru, luru));

                return Interval.For(lower, upper);
            }
            else
            {
                return UnknownInterval;
            }
        }

        static public Interval operator %(Interval left, Interval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<Interval>() != null);

            if (left.IsBottom || right.IsBottom)    // Propagate bottom
                return left.Bottom;

            int leftLow, leftUpp, rightLow, rightUpp;

            try
            {
                // Easy cases
                if (left.IsFiniteAndInt32(out leftLow, out leftUpp) &&
                  right.IsFiniteAndInt32(out rightLow, out rightUpp) &&
                  rightLow != 0 && rightUpp != 0)
                {
                    // k % [a,b]
                    if (leftLow == leftUpp)
                    {
                        var ll = leftLow % rightLow;
                        var lu = leftLow % rightUpp;
                        var ul = leftUpp % rightLow;
                        var uu = leftUpp % rightUpp;

                        var min = Math.Min(Math.Min(ll, lu), Math.Min(ul, uu));
                        var max = Math.Max(Math.Max(ll, lu), Math.Max(ul, uu));

                        return Interval.For(min, max);
                    }

                    // [a,b] % [c, d]
                    {
                        var ll = leftLow % rightLow;
                        var lu = leftLow % rightUpp;
                        var ul = leftUpp % rightLow;
                        var uu = leftUpp % rightUpp;

                        // We need to min with 0 and max with (rightUpp-1) because % is no monotonic
                        var min = Math.Min(0, Math.Min(Math.Min(ll, lu), Math.Min(ul, uu)));
                        var max = Math.Max(Math.Max(Math.Max(ll, lu), Math.Max(ul, uu)), rightUpp - 1);

                        return Interval.For(min, max);
                    }
                }

                if (right.IsTop)
                {
                    if (left.LowerBound >= 0)
                    { // We ignore the case when right == 0, this should be proven by some other analysis
                        return Interval.For(0, Rational.PlusInfinity);
                    }
                    else
                    { // Propagate top
                        return UnknownInterval;
                    }
                }
                // left % 0
                if (right.LowerBound.IsZero && right.UpperBound.IsZero)
                {
                    return UnknownInterval;
                }

                if (!right.UpperBound.IsInfinity)
                {
                    if (left.IsSingleton && right.IsSingleton)
                    {
                        if (right.LowerBound.IsNotZero && left.LowerBound.IsInteger && right.LowerBound.IsInteger)
                        {
                            Contract.Assume(((Int32)right.LowerBound.PreviousInt32) != 0);
                            return new Interval(Rational.For(((Int32)left.LowerBound) % ((Int32)right.LowerBound.PreviousInt32)));
                        }
                        else
                        {
                            return Interval.UnknownInterval;
                        }
                    }

                    if (right.UpperBound.IsMinusInfinity || right.UpperBound.IsMinValue)
                    {
                        return Interval.UnknownInterval;
                    }

                    var absUpperBound = Rational.Abs(right.UpperBound);
                    if (left.LowerBound >= 0)
                    {
                        return Interval.For(0, absUpperBound - 1);
                    }
                    else if (left.UpperBound <= 0)
                    {
                        return Interval.For(-absUpperBound + 1, 0);
                    }
                    else
                    {
                        return Interval.For(-absUpperBound + 1, absUpperBound - 1);
                    }
                }
                else if (right.LowerBound > 0)
                {
                    if (left.LowerBound >= 0)
                    {
                        return Interval.PositiveInterval;
                    }
                    else
                    {
                        return Interval.For(Rational.MinusInfinity, 0);
                    }
                }
            }
            catch (ArithmeticException)
            {
                return UnknownInterval;
            }

            return UnknownInterval;
        }

        static public Interval operator &(Interval left, Interval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<Interval>() != null);

            if (left.IsBottom || right.IsBottom)
            {
                return left.Bottom;
            }

            if (right.IsTop)
            {
                return right;
            }

            long l1, l2, r1, r2;
            if (left.IsFiniteAndInt64(out l1, out l2) && right.IsFiniteAndInt64(out r1, out r2))
            {
                // Is it a singleton?
                if (l1 == l2 && r1 == r2)
                {
                    return new Interval(Rational.For(l1 & r1));
                }

                // If one of the two is non negative, the result will be non-negative
                if (l1 >= 0 || r1 >= 0)
                {
                    // if l1 >= 0 && r1 >= 0 && l2 < r1 then the result is zero
                    if (l1 >= 0 && r1 >= 0 && l2 < r1)
                    {
                        return new Interval(0, 0);
                    }

                    return new Interval(0, Math.Min(l2, r2));
                }

                return new Interval(Math.Min(l1, r1), Math.Max(l2, r2));
            }

            if (right.LowerBound >= 0)
            {
                if (left.IsTop)
                { // [-oo, +oo] & [0, r.Upp]
                    return Interval.For(0, right.UpperBound);
                }

                if (left.UpperBound < 0)
                { // [-oo, a] & [0, r.Upp]
                    return Interval.For(0, right.UpperBound);
                }

                if (left.LowerBound >= 0)
                { //  [0 <= a, +oo] & [0, r.Upp]
                    return Interval.For(0, right.UpperBound);
                }

                return Interval.PositiveInterval;
            }

            if (left.LowerBound >= 0)
            {
                if (right.IsTop)
                { // [0, r.Upp] & [-oo, +oo]  
                    return Interval.For(0, left.UpperBound);
                }

                if (right.UpperBound < 0)
                { // [0, r.Upp] & [-oo, a] 
                    return Interval.For(0, left.UpperBound);
                }

                if (right.LowerBound >= 0)
                { //  [0, r.Upp] & [0 <= a, +oo] 
                    return Interval.For(0, left.UpperBound);
                }

                return Interval.PositiveInterval;
            }


            return UnknownInterval;
        }

        static public Interval operator |(Interval left, Interval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<Interval>() != null);

            if (left.IsBottom || right.IsBottom)
            {
                return UnreachedInterval;
            }
            if (left.IsTop || right.IsTop)
            {
                return UnknownInterval;
            }

            Interval posInterval;
            Rational k;

            if (left.LowerBound.IsInteger && left.UpperBound.IsInteger && right.LowerBound.IsInteger && right.UpperBound.IsInteger)
            {
                if (left.LowerBound >= 0 && right.LowerBound >= 0 && !left.UpperBound.IsInfinity && !right.UpperBound.IsInfinity)
                {
                    // F: Assumptions below follow form the 
                    Contract.Assert(left.LowerBound.IsInteger);
                    Contract.Assert(left.UpperBound.IsInteger);
                    Contract.Assert(right.LowerBound.IsInteger);
                    Contract.Assert(right.UpperBound.IsInteger);

                    uint min, max;
                    WarrenAlgorithmForBitewiseOr(ToUint(left.LowerBound), ToUint(left.UpperBound), ToUint(right.LowerBound), ToUint(right.UpperBound), out min, out max);

                    return Interval.For(min, max);
                }
                else if (MatchWithPositiveAndConstant(left, right, out posInterval, out k))
                {
                    if (k > 0)
                    {// we have ([k1, +oo] | k2) >= max(k1, k2)
                        return Interval.For(Rational.Max(posInterval.LowerBound, k), Rational.PlusInfinity);
                    }
                    else if (k < 0)
                    {
                        return Interval.For(k, Rational.PlusInfinity);
                    }
                    else
                    {
                        return posInterval;
                    }
                }
            }

            return UnknownInterval;
        }

        static public Interval operator ^(Interval left, Interval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<Interval>() != null);

            if (left.IsBottom || right.IsBottom)
            {
                return UnreachedInterval;
            }
            if (left.IsTop || right.IsTop)
            {
                return UnknownInterval;
            }

            long leftVal, rightVal;
            if (left.IsFiniteAndInt64Singleton(out leftVal) && right.IsFiniteAndInt64Singleton(out rightVal))
            {
                var result = leftVal ^ rightVal;
                return new Interval(Rational.For(result));
            }

            return UnknownInterval;
        }

        static public Interval ShiftLeft(Interval left, Interval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<Interval>() != null);

            if (left.IsBottom || right.IsBottom)
            {
                return UnreachedInterval;
            }
            if (left.IsTop || right.IsTop)
            {
                return UnknownInterval;
            }

            Rational valForRight;
            if (right.TryGetSingletonValue(out valForRight))
            {
                if (valForRight > 32 || !valForRight.IsInteger)
                { // According to the ECMA standard, Partition III pag. 92
                    return UnknownInterval;
                }
                else
                { // It is a multiplication by 2^(valForRight)
                    double d = Math.Ceiling(Math.Pow(2, (double)valForRight.NextInt32));     // We know it will not raise an exception...
                    int pow = (int)d;
                    return left * (Interval.For(pow));
                }
            }
            else
            {
                return UnknownInterval;
            }
        }

        static public Interval ShiftRight(Interval left, Interval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<Interval>() != null);

            if (left.IsBottom || right.IsBottom)
            {
                return left.Bottom;
            }
            if (left.IsTop || right.IsTop)
            {
                return UnknownInterval;
            }

            if (left.LowerBound >= 0)
            {
                if (left.IsFinite && right.IsFinite)
                {
                    var low = ((Int64)left.LowerBound.PreviousInt64) >> (Int32)right.UpperBound.NextInt32;
                    var upp = ((Int64)left.UpperBound.NextInt64) >> (Int32)right.LowerBound.PreviousInt32;

                    return Interval.For(low, upp);
                }

                Rational v;
                if (right.TryGetSingletonValue(out v) && v.IsInteger)
                {
                    Rational sq;

                    try
                    {
                        var value = (int)v;
                        Contract.Assume(value >= 0);

                        sq = Rational.ToThePowerOfTwo(value);
                    }
                    catch (ArithmeticExceptionRational)
                    {
                        return UnknownInterval;
                    }

                    if (sq <= 0)
                    {
                        return UnknownInterval;
                    }

                    try
                    {
                        var newLower = left.LowerBound / sq;
                        var newUpper = left.UpperBound / sq;

                        return Interval.For(newLower, newUpper);
                    }
                    catch (ArithmeticExceptionRational)
                    {
                        return UnknownInterval;
                    }
                }

                return PositiveInterval;
            }
            return UnknownInterval;
        }


        #endregion

        #region Arithmetic operations onRationals and Intervals

        public static Interval operator +(Rational r, Interval i)
        {
            Contract.Requires(i != null);
            Contract.Requires((object)r != null);

            Contract.Ensures(Contract.Result<Interval>() != null);


            if (i.IsTop || i.IsBottom)
                return i;

            try
            {
                return Interval.For(r + i.LowerBound, r + i.UpperBound);
            }
            catch (ArithmeticExceptionRational)
            {
                return Interval.UnknownInterval;
            }
        }

        public static Interval operator +(Interval i, Rational r)
        {
            Contract.Requires(i != null);
            Contract.Requires((object)r != null);

            Contract.Ensures(Contract.Result<Interval>() != null);

            return r + i;
        }

        public static Interval operator -(Rational r, Interval i)
        {
            Contract.Requires(i != null);
            Contract.Requires((object)r != null);

            Contract.Ensures(Contract.Result<Interval>() != null);


            if (i.IsTop || i.IsBottom)
                return i;

            try
            {
                return Interval.For(r - i.UpperBound, r - i.LowerBound);
            }
            catch (ArithmeticExceptionRational)
            {
                return Interval.UnknownInterval;
            }
        }

        public static Interval operator *(Rational r, Interval i)
        {
            Contract.Requires(i != null);
            Contract.Requires((object)r != null);

            Contract.Ensures(Contract.Result<Interval>() != null);


            if (i.IsTop || i.IsBottom)
                return i;

            if (r.IsZero)
                return Interval.For(0);

            if (r.Sign < 0)
            {
                return Interval.For(
                  Rational.MultiplyWithDefault(r, i.UpperBound, Rational.MinusInfinity),
                  Rational.MultiplyWithDefault(r, i.LowerBound, Rational.PlusInfinity));
            }
            else
            {
                return Interval.For(
                  Rational.MultiplyWithDefault(r, i.LowerBound, Rational.MinusInfinity),
                  Rational.MultiplyWithDefault(r, i.UpperBound, Rational.PlusInfinity));
            }
        }

        public static Interval operator *(Interval i, Rational r)
        {
            Contract.Requires(i != null);
            Contract.Requires((object)r != null);

            Contract.Ensures(Contract.Result<Interval>() != null);


            return r * i;
        }

        public static Interval operator /(Interval i, Rational r)
        {
            Contract.Requires(i != null);
            Contract.Requires((object)r != null);

            Contract.Ensures(Contract.Result<Interval>() != null);

            if (i.IsTop || i.IsBottom)
                return i;

            if (r.IsZero)
                return Interval.UnknownInterval;

            if (r.Sign < 0)
            {
                return Interval.For(
                  Rational.DivideWithDefault(i.UpperBound, r, Rational.MinusInfinity),
                  Rational.DivideWithDefault(i.LowerBound, r, Rational.PlusInfinity));
            }
            else
            {
                return Interval.For(
                  Rational.DivideWithDefault(i.LowerBound, r, Rational.MinusInfinity),
                  Rational.DivideWithDefault(i.UpperBound, r, Rational.PlusInfinity));
            }
        }

        #endregion

        #region Conversion

        public DisInterval AsDisInterval
        {
            get
            {
                Contract.Ensures(Contract.Result<DisInterval>() != null);

                return DisInterval.For(this);
            }
        }


        public override Interval ToUnsigned()
        {
            int val;
            if (this.IsFiniteAndInt32Singleton(out val))
            {
                if (val >= 0)
                {
                    return this;
                }
                else
                {
                    return Interval.For((uint)val);
                }
            }
            else
            {
                return ApplyConversion(ExpressionOperator.ConvertToUInt32, this);
            }
        }

        public Interval ApplyConversion(ExpressionOperator conversionType)
        {
            return ApplyConversion(conversionType, this);
        }

        static public Interval ApplyConversion(ExpressionOperator conversionType, Interval val)
        {
            Contract.Requires(val != null);
            Contract.Ensures(Contract.Result<Interval>() != null);

            if (val.IsBottom)
            {
                return val;
            }

            switch (conversionType)
            {
                case ExpressionOperator.ConvertToInt8:
                    {
                        return val.RefineIntervalWithTypeRanges(SByte.MinValue, SByte.MaxValue);
                    }

                case ExpressionOperator.ConvertToInt16:
                    {
                        return val.RefineIntervalWithTypeRanges(Int16.MinValue, Int16.MaxValue);
                    }

                case ExpressionOperator.ConvertToInt32:
                    {
                        return val.RefineIntervalWithTypeRanges(Int32.MinValue, Int32.MaxValue);
                    }

                case ExpressionOperator.ConvertToInt64:
                    {
                        return val.RefineIntervalWithTypeRanges(Int64.MinValue, Int64.MaxValue);
                    }

                case ExpressionOperator.ConvertToUInt8:
                    {
                        return val.RefineIntervalWithTypeRanges(Byte.MinValue, Byte.MaxValue);
                    }

                case ExpressionOperator.ConvertToUInt16:
                    {
                        return val.RefineIntervalWithTypeRanges(UInt16.MinValue, UInt16.MaxValue);
                    }

                case ExpressionOperator.ConvertToUInt32:
                    {
                        if (val.IsSingleton && val.LowerBound.IsInteger)
                        {
                            if (val.LowerBound < Int32.MinValue || val.UpperBound > Int32.MaxValue)
                            {
                                return val;
                            }
                            else
                            {
                                var asInt = (Int32)val.LowerBound;

                                var asUint = (UInt32)(Int32)val.LowerBound;
                                if (asUint < Int32.MaxValue)
                                {
                                    return Interval.For((int)asUint);
                                }
                                else
                                {
                                    return Interval.For(asUint);
                                }
                            }
                        }
                        else
                        {
                            if (val.LowerBound >= 0)
                            { // (UInt32)[a, b], con a >= 0 
                                return val;
                            }
                            else
                            {
                                return new Interval(0, (long)UInt32.MaxValue);
                            }
                        }
                    }

                case ExpressionOperator.ConvertToUInt64:
                // TODO
                case ExpressionOperator.ConvertToFloat32:
                case ExpressionOperator.ConvertToFloat64:
                default:
                    return val;
            }
        }

        public Interval RefineIntervalWithTypeRanges(Int32 min, Int32 max)
        {
            var low = this.LowerBound.IsInfinity || !this.LowerBound.IsInRange(min, max)
              ? Rational.MinusInfinity
            : this.LowerBound.PreviousInt32;

            var upp = this.UpperBound.IsInfinity || !this.UpperBound.IsInRange(min, max)
              ? Rational.PlusInfinity
              : this.UpperBound.NextInt32;

            return Interval.For(low, upp);
        }

        public Interval RefineIntervalWithTypeRanges(Int64 min, Int64 max)
        {
            var low = this.LowerBound.IsInfinity || !this.LowerBound.IsInRange(min, max)
              ? Rational.MinusInfinity
            : this.LowerBound.PreviousInt64;

            var upp = this.UpperBound.IsInfinity || !this.UpperBound.IsInRange(min, max)
              ? Rational.PlusInfinity
              : this.UpperBound.NextInt64;

            return Interval.For(low, upp);
        }

        public Interval RefineIntervalWithTypeRanges(UInt32 min, UInt32 max)
        {
            var low = this.LowerBound.IsInfinity || !this.LowerBound.IsInRange(min, max)
              ? Rational.MinusInfinity
              : this.LowerBound.PreviousInt32;

            var upp = this.UpperBound.IsInfinity || !this.UpperBound.IsInRange(min, max)
              ? Rational.PlusInfinity
              : this.UpperBound.NextInt32;

            return Interval.For(low, upp);
        }
        #endregion

        #region IAbstractDomain Members

        override public bool IsBottom
        {
            get
            {
                return isBottom;
                // return this.lowerBound > this.upperBound;
            }
        }

        override public bool IsTop
        {
            get
            {
                return isTop;
                //return this.upperBound.IsPlusInfinity && this.lowerBound.IsMinusInfinity;
            }
        }

        public override Interval Bottom
        {
            get { return UnreachedInterval; }
        }

        public override Interval Top
        {
            get { return UnknownInterval; }
        }

        [Pure]
        override public bool LessEqual(Interval right)
        {
            bool result;
            if (AbstractDomainsHelper.TryTrivialLessEqual(this, right, out result))
            {
                return result;
            }

            return this.LowerBound >= right.LowerBound && this.UpperBound <= right.UpperBound;
        }

        [Pure]
        public bool LessEqual(ReadOnlyIntervalList right)
        {
            Contract.Requires(right != null);

            foreach (var intv in right)
            {
                Contract.Assume(intv != null);

                if (this.LessEqual(intv))
                {
                    return true;
                }
            }

            return false;
        }

        [Pure]
        override public Interval/*!*/ Join(Interval/*!*/ right)
        {
            Interval/*!*/ result;
            if (AbstractDomainsHelper.TryTrivialJoin(this, right, out result))
            {
                return result;
            }

            var joinInf = Rational.Min(this.LowerBound, right.LowerBound);
            var joinSup = Rational.Max(this.UpperBound, right.UpperBound);

            return Interval.For(joinInf, joinSup);
        }

        [Pure]
        override public Interval/*!*/ Meet(Interval/*!*/ right)
        {
            Interval trivialMeet;
            if (AbstractDomainsHelper.TryTrivialMeet(this, right, out trivialMeet))
            {
                return trivialMeet;
            }

            var meetInf = Rational.Max(this.LowerBound, right.LowerBound);
            var meetSup = Rational.Min(this.UpperBound, right.UpperBound);

            return Interval.For(meetInf, meetSup);
        }

        [Pure]
        override public Interval/*!*/ Widening(Interval/*!*/ prev)
        {
            // Trivial cases
            if (this.IsBottom)
                return prev;
            if (this.IsTop)
                return this;
            if (prev.IsBottom)
                return this;
            if (prev.IsTop)
                return prev;

            var wideningInf = this.LowerBound < prev.LowerBound ? ThresholdDB.GetPrevious(this.LowerBound) : prev.LowerBound;
            Contract.Assert(((object)wideningInf) != null);

            var wideningSup = this.UpperBound > prev.UpperBound ? ThresholdDB.GetNext(this.upperBound) : prev.UpperBound;
            Contract.Assert(((object)wideningSup) != null);

            return Interval.For(wideningInf, wideningSup);
        }
        #endregion

        #region ICloneable Members

        public override Interval DuplicateMe()
        {
            return Interval.For(this.lowerBound, this.upperBound);
        }

        #endregion

        #region Overridden (To, ToString, GetHash, Equals)

        public override T To<T>(IFactory<T> factory)
        {
            T varName;
            if (this.IsBottom)
            {
                return factory.Constant(false);
            }
            else if (this.IsTop)
            {
                return factory.Constant(true);
            }
            else if (factory.TryGetName(out varName))
            {
                T low = default(T), upp = default(T);
                bool lowSet = false, uppSet = false;

                if (this.lowerBound.IsInteger)
                {
                    low = factory.LessEqualThan(this.lowerBound.To(factory), varName);
                    lowSet = true;
                }
                if (this.upperBound.IsInteger)
                {
                    upp = factory.LessEqualThan(varName, this.upperBound.To(factory));
                    uppSet = true;
                }
                if (lowSet && uppSet)
                {
                    return factory.And(low, upp);
                }
                return lowSet ? low : upp;
            }

            return factory.Constant(true);
        }

        public override int GetHashCode()
        {
            return (Int32)(this.lowerBound + this.upperBound);
        }

        [Pure]
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            var asIntv = obj as Interval;

            if (asIntv == null)
            {
                return false;
            }

            Contract.Assume(((object)asIntv.upperBound) != null);
            Contract.Assume(((object)asIntv.lowerBound) != null);

            return this.upperBound == asIntv.upperBound && this.lowerBound == asIntv.lowerBound;
        }

        #endregion

        #region Utils
        static public bool AreConsecutiveIntegers(Interval prev, Interval next)
        {
            Contract.Requires(prev != null);
            Contract.Requires(next != null);

            if (!prev.IsNormal || !next.IsNormal)
            {
                return false;
            }

            return prev.UpperBound.IsInteger && next.LowerBound.IsInteger && prev.UpperBound + 1 == next.LowerBound;
        }
        #endregion

        #region Private, helper methods
        [Pure]
        static private uint ToUint(Rational r)
        {
            Contract.Requires(!object.ReferenceEquals(r, null));
            Contract.Requires(r.IsInteger);

            return (uint)((int)r);
        }

        static private bool MatchWithPositiveAndConstant(Interval left, Interval right, out Interval posInterval, out Rational k)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out posInterval) != null);
            Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k), null));

            bool result;
            if (left.LowerBound >= 0 && right.IsSingleton)
            {
                posInterval = left;
                k = right.LowerBound;
                result = true;
            }
            else if (right.LowerBound >= 0 && left.IsSingleton)
            {
                posInterval = right;
                k = left.LowerBound;
                result = true;
            }
            else
            {
                posInterval = null;
                k = default(Rational);
                result = false;
            }
            return result;
        }


        private static bool IsPowerOfTwoMinusOne(Rational r)
        {
            Contract.Requires((object)r != null);

            return r == 15 || r == 31 || r == 63 || r == 127 || r == 255 || r == 511 || r == 1023 || r == 2047 || r == 4095 || r == 8191 || r == 16385 || r == 32767
            || r == 65535 || r == 131071 || r == 262143 || r == 524287 || r == 1048575 || r == 2097151 || r == 4194303 || r == 8388607 || r == 16777215;
        }

        /// <summary>
        /// The Warren algorithm to find the min and max of two intervals.
        /// Given <code> a \leq x \leq b</code> and <code>c \leq y \leq d</code>, it finds the <code>min</code> and <code>max</code> of <code>x | y</code>
        /// </summary>
        static private void WarrenAlgorithmForBitewiseOr(uint a, uint b, uint c, uint d, out uint min, out uint max)
        {
            min = MinOr(a, b, c, d);
            max = MaxOr(a, b, c, d);
        }

        static private uint MinOr(uint a, uint b, uint c, uint d)
        {
            uint m, tmp;
            m = 0x80000000;
            while (m != 0)
            {
                if ((~a & c & m) != 0)
                {
                    tmp = (a | m) & ~m;
                    if (tmp <= b)
                    {
                        a = tmp;
                        break;
                    }
                }
                else if ((a & ~c & m) != 0)
                {
                    tmp = (c | m) & ~m;
                    if (tmp <= d)
                    {
                        c = tmp;
                        break;
                    }
                }
                m = m >> 1;
            }
            return a | c;
        }

        static private uint MaxOr(uint a, uint b, uint c, uint d)
        {
            uint m, tmp;
            m = 0x80000000;
            while (m != 0)
            {
                if ((b & d & m) != 0)
                {
                    tmp = (b - m) | (m - 1);
                    if (tmp >= a)
                    {
                        b = tmp;
                        break;
                    }
                    tmp = (d - m) | (m - 1);
                    if (tmp >= c)
                    {
                        d = tmp;
                        break;
                    }
                }
                m = m >> 1;
            }
            return b | d;
        }
        #endregion

        #region Ranges for basic types
        static public class Ranges
        {
            private static readonly Interval uint8Range = Interval.For(Byte.MinValue, Byte.MaxValue);
            private static readonly Interval uint16Range = Interval.For(UInt16.MinValue, UInt16.MaxValue);
            private static readonly Interval uint32Range = Interval.For(UInt32.MinValue, UInt32.MaxValue);
            private static readonly Interval uint64Range = Interval.For(0, Rational.PlusInfinity);

            private static readonly Interval int8Range = Interval.For(SByte.MinValue, SByte.MaxValue);
            private static readonly Interval int16Range = Interval.For(Int16.MinValue, Int16.MaxValue);
            private static readonly Interval int32Range = Interval.For(Int32.MinValue, Int32.MaxValue);
            private static readonly Interval int64Range = Interval.For(Int64.MinValue, Int64.MaxValue);

            public static Interval UInt8Range
            {
                get
                {
                    return uint8Range;
                }
            }

            public static Interval UInt16Range
            {
                get
                {
                    return uint16Range;
                }
            }

            public static Interval UInt32Range
            {
                get
                {
                    return uint32Range;
                }
            }

            public static Interval UInt64Range
            {
                get
                {
                    return uint64Range;
                }
            }

            public static Interval Int8Range
            {
                get
                {
                    return int8Range;
                }
            }

            public static Interval Int16Range
            {
                get
                {
                    return int16Range;
                }
            }

            public static Interval Int32Range
            {
                get
                {
                    return int32Range;
                }
            }

            public static Interval Int64Range
            {
                get
                {
                    return int64Range;
                }
            }

            public static Interval EnumRange<Type>(Type t, Func<Type, List<int>> enumranges)
            {
                Contract.Requires(enumranges != null);

                Contract.Ensures(Contract.Result<Interval>() != null);

                var ranges = enumranges(t);
                if (ranges != null)
                {
                    var min = Int32.MaxValue;
                    var max = Int32.MinValue;

                    foreach (var x in ranges)
                    {
                        if (x < min)
                        {
                            min = x;
                        }
                        if (x > max)
                        {
                            max = x;
                        }
                    }

                    if (min <= max)
                    {
                        return Interval.For(min, max);
                    }
                }

                return Ranges.Int32Range;
            }
        }
        #endregion
    }
}

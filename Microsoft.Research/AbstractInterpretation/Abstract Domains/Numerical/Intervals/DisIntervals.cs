// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// The implementation of the disjunction of intervals


using System;
using System.Text;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains.Numerical
{
#if DEBUG
    using ReadOnlyIntervalList = ReadOnlyCollection<Interval>;
#else
    using ReadOnlyIntervalList = List<Interval>;
#endif
    [SuppressMessage("Microsoft.Contracts", "InvariantInMethod-this.CheckInvariant()", Justification = "Out of reach of the static checker")]
    [SuppressMessage("Microsoft.Contracts", "InvariantInMethod-Contract.ForAll(this.intervals, x => x != null)", Justification = "At the moment, the array analysis does not understand Lists")]
    public sealed class DisInterval
      : IntervalBase<DisInterval, Rational>
    {
        #region ObjectInvariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(joinInterval != null);
            Contract.Invariant(intervals != null);
            Contract.Invariant(Contract.ForAll(intervals, x => x != null));
            Contract.Invariant(this.CheckInvariant());
        }

        #endregion

        #region Private statics

        private static readonly DisInterval cachedTop;
        private static readonly DisInterval cachedBottom;
        private static readonly DisInterval cachedZero;
        private static readonly DisInterval cachedNotZero;
        private static readonly DisInterval cachedPositive;
        private static readonly DisInterval cachedNegative;

        static DisInterval()
        {
            cachedTop = new DisInterval(State.Top);
            cachedBottom = new DisInterval(State.Bottom);
            cachedZero = new DisInterval(Interval.For(0));
            cachedNotZero = new DisInterval(new List<Interval>() {
            Interval.For(Rational.MinusInfinity, Rational.For(-1)),
            Interval.For(Rational.For(1), Rational.PlusInfinity)
          });
            cachedPositive = Interval.PositiveInterval.AsDisInterval;
            cachedNegative = Interval.NegativeInterval.AsDisInterval;
        }

        #endregion

        #region Private state

        private enum State { Bottom, Normal, Top }

        private readonly ReadOnlyIntervalList intervals;
        private readonly Interval joinInterval;
        private readonly State state;

        #endregion

        #region For

        static public DisInterval For(Rational lower, Rational upper)
        {
            Contract.Requires(!object.Equals(lower, null));
            Contract.Requires(!object.Equals(upper, null));

            Contract.Ensures(Contract.Result<DisInterval>() != null);

            var intv = Interval.For(lower, upper);

            return new DisInterval(intv);
        }

        static public DisInterval For(Rational r)
        {
            Contract.Requires(!object.Equals(r, null));

            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return For(r, r);
        }

        static public DisInterval For(Rational lower, Int64 upper)
        {
            Contract.Requires(!object.Equals(lower, null));

            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return For(lower, Rational.For(upper));
        }

        static public DisInterval For(Int64 lower, Rational upper)
        {
            Contract.Requires(!object.Equals(upper, null));

            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return For(Rational.For(lower), upper);
        }

        static public DisInterval For(Int64 lower, Int64 upper)
        {
            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return For(Rational.For(lower), Rational.For(upper));
        }

        static public DisInterval For(UInt32 lower, UInt32 upper)
        {
            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return For(Rational.For(lower), Rational.For(upper));
        }

        static public DisInterval For(Int64 i)
        {
            Contract.Ensures(Contract.Result<DisInterval>() != null);

            var r = Rational.For(i);
            return For(r, r);
        }

        static public DisInterval For(Byte i)
        {
            Contract.Ensures(Contract.Result<DisInterval>() != null);

            var r = Rational.For(i);
            return For(r, r);
        }

        static public DisInterval For(UInt32 i)
        {
            Contract.Ensures(Contract.Result<DisInterval>() != null);

            var r = Rational.For(i);
            return For(r, r);
        }

        static public DisInterval For(UInt64 i)
        {
            Contract.Ensures(Contract.Result<DisInterval>() != null);

            var r = Rational.For(i);
            return For(r, r);
        }

        static public DisInterval For(Double d)
        {
            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return DisInterval.For(d.ConvertFromDouble());
        }

        static public DisInterval For(Interval intv)
        {
            Contract.Requires(intv != null);
            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return new DisInterval(intv);
        }

        static public DisInterval For(List<Interval> intervals)
        {
            Contract.Requires(intervals != null);
            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return new DisInterval(intervals);
        }

        static public DisInterval For(List<int> values)
        {
            Contract.Requires(values != null);
            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return new DisInterval(values.ConvertAll(x => Interval.For(x)));
        }

        public static DisInterval NotInThisInterval(DisInterval intv)
        {
            Contract.Requires(intv != null);
            Contract.Ensures(Contract.Result<DisInterval>() != null);

            var low = Interval.For(Rational.MinusInfinity, intv.lowerBound - 1);
            var upp = Interval.For(intv.UpperBound + 1, Rational.PlusInfinity);

            var bLow = low.IsNormal;
            var bUpp = upp.IsNormal;

            if (bLow && bUpp)
            {
                return For(new List<Interval>() { low, upp });
            }

            if (bLow)
            {
                return For(low);
            }

            if (bUpp)
            {
                return For(upp);
            }

            return DisInterval.UnknownInterval;
        }

        #endregion

        #region Constructors

        private DisInterval(Interval intv)
          : base(intv.LowerBound, intv.UpperBound)
        {
            Contract.Requires(intv != null);

            var list = new List<Interval>();

            if (intv.IsTop)
            {
                state = State.Top;
            }
            else if (intv.IsBottom)
            {
                state = State.Bottom;
            }
            else
            {
                state = State.Normal;

                list.Add(intv);
            }
            intervals = AsReadOnly(list);
            joinInterval = intv;
        }

        private static readonly ReadOnlyIntervalList EmptyReadOnlyList = AsReadOnly(new List<Interval>(0));

        private static ReadOnlyIntervalList AsReadOnly(List<Interval> list)
        {
            Contract.Requires(list != null);

#if DEBUG
            return list.AsReadOnly();
#else
            return list;
#endif
        }

        private DisInterval(List<Interval> intervals)
          : base(Rational.MinusInfinity, Rational.PlusInfinity)
        {
            Contract.Requires(intervals != null);

            bool isBottom;
            this.intervals = Normalize(intervals, out isBottom);

            if (isBottom)
            {
                joinInterval = Interval.UnreachedInterval;
                state = State.Bottom;
            }
            else
            {
                joinInterval = JoinAll(intervals);

                if (joinInterval.IsBottom)
                {
                    state = State.Bottom;
                }
                else if (joinInterval.IsTop)
                {
                    state = this.intervals.Count <= 1 ? State.Top : State.Normal;
                }
                else
                {
                    this.lowerBound = joinInterval.LowerBound;
                    this.upperBound = joinInterval.UpperBound;

                    state = State.Normal;
                }
            }
        }

        private DisInterval(DisInterval original)
          : base(original.lowerBound, original.upperBound)
        {
            Contract.Requires(original != null);

            intervals = original.intervals;
            joinInterval = original.joinInterval.DuplicateMe();
            state = original.state;
        }

        private DisInterval(State state)
          : base(Rational.MinusInfinity, Rational.PlusInfinity)
        {
            Contract.Requires(state != State.Normal);

            this.state = state;
            joinInterval = this.state == State.Bottom ? Interval.UnreachedInterval : Interval.UnknownInterval;
            intervals = EmptyReadOnlyList;
        }

        #endregion

        #region Implementation of the abstract methods
        public override bool IsInt32
        {
            get
            {
                if (!this.IsNormal)
                {
                    return false;
                }

                var blow = this.IsLowerBoundMinusInfinity || this.lowerBound >= Int32.MinValue;
                var bupp = this.IsUpperBoundPlusInfinity || this.upperBound <= Int32.MaxValue;

                return blow && bupp;
            }
        }

        public override bool IsInt64
        {
            get
            {
                if (!this.IsNormal)
                {
                    return false;
                }

                var blow = this.IsLowerBoundMinusInfinity || this.lowerBound >= Int64.MinValue;
                var bupp = this.IsUpperBoundPlusInfinity || this.upperBound <= Int64.MaxValue;

                return blow && bupp;
            }
        }

        public override bool IsLowerBoundMinusInfinity
        {
            get
            {
                return this.lowerBound.IsMinusInfinity;
            }
        }

        public override bool IsUpperBoundPlusInfinity
        {
            get
            {
                return this.upperBound.IsPlusInfinity;
            }
        }

        public override DisInterval Bottom
        {
            get
            {
                return DisInterval.UnreachedInterval;
            }
        }

        static public DisInterval IntervalUnknown
        {
            get
            {
                return DisInterval.UnknownInterval;
            }
        }

        public override DisInterval Top
        {
            get
            {
                return DisInterval.UnknownInterval;
            }
        }

        public override bool IsNormal
        {
            get
            {
                return state == State.Normal;
            }
        }

        public bool IsNotZero
        {
            get
            {
                if (!this.IsNormal)
                {
                    return false;
                }

                foreach (var intv in intervals)
                {
                    if (intv.DoesInclude(0))
                        return false;
                }

                return true;
            }
        }

        public bool IsZero
        {
            get
            {
                if (!this.IsNormal)
                {
                    return false;
                }
                Rational value;
                if (this.TryGetSingletonValue(out value) && value.IsZero)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsPositiveOrZero
        {
            get
            {
                if (this.IsNormal)
                {
                    Int64 lower;
                    return this.LowerBound.TryInt64(out lower) && lower >= 0;
                }

                return false;
            }
        }

        public bool IsNegativeOrZero
        {
            get
            {
                if (this.IsNormal)
                {
                    Int64 upper;
                    return this.UpperBound.TryInt64(out upper) && upper <= 0;
                }

                return false;
            }
        }


        public override bool IsBottom
        {
            get
            {
                return state == State.Bottom;
            }
        }

        public override bool IsTop
        {
            get
            {
                return state == State.Top;
            }
        }

        #endregion

        #region Abstract domain operators
        public override bool LessEqual(DisInterval a)
        {
            bool trivial;
            if (AbstractDomainsHelper.TryTrivialLessEqual(this, a, out trivial))
            {
                return trivial;
            }

            if (!joinInterval.LessEqual(a.joinInterval))
            {
                return false;
            }

            var lastRight = 0;

            for (var i = 0; i < intervals.Count; i++)
            {
                for (; lastRight < a.intervals.Count; lastRight++)
                {
                    if (intervals[i].LessEqual(a.intervals[lastRight]))
                    {
                        goto next;
                    }
                }

                return false; // If we reach this point, we were unable to find un upper interval in right, so we return false

            next:
                ;
            }

            return true;
        }

        public override DisInterval Join(DisInterval a)
        {
            DisInterval trivial;
            if (AbstractDomainsHelper.TryTrivialJoin(this, a, out trivial))
            {
                return trivial;
            }

            var joinIntervals = Join(intervals, a.intervals);

            if (joinIntervals.Count == 0)
            {
                return Top;
            }

            return new DisInterval(joinIntervals);
        }

        public override DisInterval Meet(DisInterval a)
        {
            DisInterval trivial;
            if (AbstractDomainsHelper.TryTrivialMeet(this, a, out trivial))
            {
                return trivial;
            }

            bool isBottom;
            var meetIntervals = Meet(intervals, a.intervals, out isBottom);

            if (isBottom)
            {
                return Bottom;
            }
            if (meetIntervals.Count == 0)
            {
                return Top;
            }

            return new DisInterval(meetIntervals);
        }

        public override DisInterval Widening(DisInterval prev)
        {
            if (this.IsTop || prev.IsTop)
            {
                return Top;
            }

            var widenIntervals = Widening(intervals, prev.intervals);

            return new DisInterval(widenIntervals);
        }

        public override DisInterval DuplicateMe()
        {
            return new DisInterval(this);
        }
        #endregion

        #region Arithmetic operations on DisIntervals

        private delegate Interval IntervalBinOp(Interval left, Interval right);

        static private DisInterval OperatorLifting(DisInterval left, DisInterval right, IntervalBinOp binop, bool propagateTop)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);
            Contract.Requires(binop != null);

            Contract.Ensures(Contract.Result<DisInterval>() != null);

            if (left.IsBottom || right.IsBottom)    // Propagate bottom
            {
                return UnreachedInterval;
            }

            if (propagateTop && (left.IsTop || right.IsTop))          // Propagate top
            {
                return UnknownInterval;
            }

            if (left.IsTop && right.IsTop)
            {
                return UnknownInterval;
            }

            Contract.Assume(left.intervals != null);
            Contract.Assume(right.intervals != null);

            var result = new List<Interval>(left.intervals.Count + right.intervals.Count);

            bool isBottom = true;

            if (propagateTop || (left.IsNormal && right.IsNormal))
            {
                for (var i = 0; i < left.intervals.Count; i++)
                {
                    var intvLeft = left.intervals[i];

                    for (var j = 0; j < right.intervals.Count; j++)
                    {
                        var opResult = binop(intvLeft, right.intervals[j]);
                        if (opResult.IsTop)
                        {
                            return left.Top;
                        }

                        if (opResult.IsBottom)
                        {
                            continue;
                        }

                        isBottom = false;
                        result.Add(opResult);
                    }
                }
            }
            else
            {
                var nonTopArgument = left.IsTop ? right : left;
                var nonTopIsLeft = !left.IsTop;

                for (var i = 0; i < nonTopArgument.intervals.Count; i++)
                {
                    var opResult = nonTopIsLeft ?
                      binop(nonTopArgument.intervals[i], Interval.UnknownInterval) :
                      binop(Interval.UnknownInterval, nonTopArgument.intervals[i]);

                    if (opResult.IsTop)
                    {
                        return UnknownInterval;
                    }
                    if (opResult.IsBottom)
                    {
                        continue;
                    }

                    isBottom = false;
                    result.Add(opResult);
                }
            }

            return isBottom ? DisInterval.UnreachedInterval : new DisInterval(result);
        }

        static public DisInterval operator +(DisInterval left, DisInterval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return OperatorLifting(left, right, (a, b) => a + b, true);
        }

        static public DisInterval operator -(DisInterval left, DisInterval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return OperatorLifting(left, right, (a, b) => a - b, true);
        }

        static public DisInterval operator -(DisInterval left)
        {
            Contract.Requires(left != null);

            Contract.Ensures(Contract.Result<DisInterval>() != null);

            if (left.IsBottom || left.IsTop)
            {
                return left;
            }

            var result = new List<Interval>();
            for (var i = 0; i < left.intervals.Count; i++)
            {
                result.Add(-left.intervals[i]);
            }

            result.Reverse();
            return new DisInterval(result);
        }

        static public DisInterval operator *(DisInterval left, DisInterval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return OperatorLifting(left, right, (a, b) => a * b, true);
        }

        static public DisInterval operator /(DisInterval left, DisInterval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return OperatorLifting(left, right, (a, b) => a / b, true);
        }

        static public DisInterval operator %(DisInterval left, DisInterval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return OperatorLifting(left, right, (a, b) => a % b, false);
        }

        static public DisInterval operator &(DisInterval left, DisInterval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return OperatorLifting(left, right, (a, b) => a & b, false);
        }

        static public DisInterval operator |(DisInterval left, DisInterval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return OperatorLifting(left, right, (a, b) => a | b, true);
        }

        static public DisInterval operator ^(DisInterval left, DisInterval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return OperatorLifting(left, right, (a, b) => a ^ b, true);
        }

        static public DisInterval ShiftLeft(DisInterval left, DisInterval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return OperatorLifting(left, right, Interval.ShiftLeft, true);
        }

        static public DisInterval ShiftRight(DisInterval left, DisInterval right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return OperatorLifting(left, right, Interval.ShiftRight, true);
        }

        #endregion

        #region Conversion

        public override DisInterval ToUnsigned()
        {
            if (this.IsNormal)
            {
                var result = new List<Interval>(intervals.Count);

                foreach (var x in intervals)
                {
                    result.Add(x.ToUnsigned());
                }

                return new DisInterval(result);
            }

            return this;
        }

        public Interval AsInterval
        {
            get
            {
                Contract.Ensures(Contract.Result<Interval>() != null);

                return joinInterval;
            }
        }
        #endregion

        #region Map

        public DisInterval Map(Func<Interval, Interval> f)
        {
            Contract.Requires(f != null);
            Contract.Ensures(Contract.Result<DisInterval>() != null);

            if (this.IsBottom)
            {
                return this;
            }

            if (this.IsTop)
            {
                return new DisInterval(f(Interval.UnknownInterval));
            }

            var result = new List<Interval>();
            for (var i = 0; i < intervals.Count; i++)
            {
                var res = f(intervals[i]);

                if (res.IsBottom)
                {
                    return this.Bottom;
                }
                if (res.IsTop)
                {
                    return this.Top;
                }
                result.Add(res);
            }

            return new DisInterval(result);
        }

        #endregion

        #region Utils

        static private ReadOnlyIntervalList Normalize(List<Interval> intervals, out bool isBottom)
        {
            Contract.Requires(intervals != null);

            Contract.Ensures(Contract.Result<ReadOnlyIntervalList>() != null);

            if (intervals.Count <= 1)
            {
                isBottom = false;
                return AsReadOnly(intervals);
            }

            intervals.Sort((a, b) =>
              a.Equals(b) ?
              0 :
             (a.UpperBound <= b.LowerBound ? -1 : 1));

            var result = new List<Interval>(intervals.Count);
            var bottoms = 0;

            var prev = null as Interval;

            for (var i = 0; i < intervals.Count; i++)
            {
                var intv = intervals[i];

                // Skip repetitions
                if (prev == intv)
                {
                    continue;
                }

                if (intv.IsBottom)
                {
                    bottoms++;

                    continue;
                }

                if (intv.IsTop)
                {
                    isBottom = false;

                    return EmptyReadOnlyList;
                }

                if (prev != null)
                {
                    var refined = true;
                    while (refined && result.Count > 0)
                    {
                        prev = result[result.Count - 1];

                        if (Interval.AreConsecutiveIntegers(prev, intv))         // The intervals are consecutive, so we join them together
                        {
                            result.RemoveAt(result.Count - 1);  // remove the last element
                            intv = prev.Join(intv);
                        }
                        else if (prev.LessEqual(intv))        // The last element we added is subsumed by the next one
                        {
                            result.RemoveAt(result.Count - 1); // remove the last element
                        }
                        else if (intv.LessEqual(prev))        // The last element we added subsumes the next one
                        {
                            goto nextIteration;
                        }
                        else if (prev.OverlapsWith(intv))    // If they are not included by they overlap, we take the join
                        {
                            result.RemoveAt(result.Count - 1);  // remove the last element
                            intv = prev.Join(intv);
                        }
                        else
                        {
                            refined = false;                    // Done, we cannot do better
                        }
                    }
                }
                prev = intv;
                result.Add(intv);

            nextIteration:
                ;
            }

            isBottom = bottoms == intervals.Count;

            return AsReadOnly(result);
        }

        static private Interval JoinAll(List<Interval> list)
        {
            Contract.Requires(list != null);

            Contract.Ensures(Contract.Result<Interval>() != null);

            if (list.Count == 0)
            {
                return Interval.UnknownInterval;
            }

            var result = list[0];

            for (var i = 1; i < list.Count; i++)
            {
                result = result.Join(list[i]);
            }

            return result;
        }

        static private List<Interval> Join(ReadOnlyIntervalList left, ReadOnlyIntervalList right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<List<Interval>>() != null);

            var result = new List<Interval>(left.Count + right.Count);

            var i = 0;
            var j = 0;

            while (i < left.Count && j < right.Count)
            {
                var leftIntv = left[i];
                var rigthIntv = right[j];

                if (leftIntv.IsTop || rigthIntv.IsTop)
                {
                    return new List<Interval>();  // Top
                }

                // Consume bottoms
                if (leftIntv.IsBottom)
                {
                    i++;

                    continue;
                }

                if (rigthIntv.IsBottom)
                {
                    j++;

                    continue;
                }

                // Check for inclusion
                if (leftIntv.LessEqual(rigthIntv))
                {
                    result.Add(rigthIntv);
                    i++; j++;

                    continue;
                }

                if (rigthIntv.LessEqual(leftIntv))
                {
                    result.Add(leftIntv);
                    i++; j++;

                    continue;
                }

                // Check if the intervals overlap or touch
                if (rigthIntv.OverlapsWith(leftIntv))
                {
                    result.Add(rigthIntv.Join(leftIntv));
                    i++; j++;

                    continue;
                }

                // Add the leftmost interval
                if (leftIntv.OnTheLeftOf(rigthIntv))
                {
                    result.Add(leftIntv);
                    i++;

                    continue;
                }

                if (rigthIntv.OnTheLeftOf(leftIntv)) // F: should always be the case at this point
                {
                    result.Add(rigthIntv);
                    j++;

                    continue;
                }
            }

            while (i < left.Count)
            {
                result.Add(left[i]);
                i++;
            }

            while (j < right.Count)
            {
                result.Add(right[j]);
                j++;
            }

            return result;
        }

        static private List<Interval> Meet(ReadOnlyIntervalList left, ReadOnlyIntervalList right, out bool isBottom)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<List<Interval>>() != null);

            isBottom = true;

            var result = new List<Interval>();

            for (var i = 0; i < left.Count; i++)
            {
                var leftIntv = left[i];
                for (var j = 0; j < right.Count; j++)
                {
                    var meet = leftIntv.Meet(right[j]);
                    if (meet.IsNormal)
                    {
                        isBottom = false;
                        result.Add(meet);
                    }
                }
            }

            return result;
        }

        static private List<Interval> Widening(ReadOnlyIntervalList next, ReadOnlyIntervalList prev)
        {
            Contract.Requires(next != null);
            Contract.Requires(prev != null);

            Contract.Ensures(Contract.Result<List<Interval>>() != null);

            // One of the two is top
            if (next.Count == 0 || prev.Count == 0)
            {
                return new List<Interval>();
            }

            // Boths are one interval
            if (next.Count == 1 && prev.Count == 1)
            {
                var intv = next[0].Widening(prev[0]);

                return new List<Interval>() { intv };
            }

            // One of the two contains just one interval
            if (next.Count == 1 && prev.Count > 1)
            {
                if (next[0].LessEqual(prev))
                {
                    return new List<Interval>(prev);
                }

                var intv = next[0].Widening(prev[0].Join(prev[prev.Count - 1]));

                return new List<Interval>() { intv };
            }

            if (next.Count > 1 && prev.Count == 1)
            {
                var intv = (next[0].Join(next[next.Count - 1]).Widening(prev[0]));

                return new List<Interval>() { intv };
            }

            Contract.Assert(next.Count >= 2);
            Contract.Assert(prev.Count >= 2);

            // We widen the extremes, and keep the internal intervals which are stable among two iterations           
            var leftMostInterval = next[0].Widening(prev[0]);
            var rightMostInterval = next[next.Count - 1].Widening(prev[prev.Count - 1]);

            var result = new List<Interval>();

            result.Add(leftMostInterval);

            for (var j = 1; j < prev.Count - 1; j++)
            {
                for (var i = 1; i < next.Count - 1; i++)
                {
                    if (next[i].LessEqual(prev[j]))
                    {
                        result.Add(prev[j]);

                        break;
                    }
                }
            }

            result.Add(rightMostInterval);

            return result;
        }

        [Pure]
        [ContractVerification(false)] // This method is only for checking
        private bool CheckInvariant()
        {
            if (this.IsBottom || this.IsTop)
            {
                return true;
            }

            for (var i = 0; i < intervals.Count - 1; i++)
            {
                if (intervals[i].UpperBound > intervals[i + 1].LowerBound)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Statics

        public static DisInterval UnknownInterval
        {
            get
            {
                Contract.Ensures(Contract.Result<DisInterval>() != null);

                return cachedTop;
            }
        }

        public static DisInterval UnreachedInterval
        {
            get
            {
                Contract.Ensures(Contract.Result<DisInterval>() != null);

                return cachedBottom;
            }
        }

        public static DisInterval Zero
        {
            get
            {
                Contract.Ensures(Contract.Result<DisInterval>() != null);

                return cachedZero;
            }
        }

        public static DisInterval NotZero
        {
            get
            {
                Contract.Ensures(Contract.Result<DisInterval>() != null);

                return cachedNotZero;
            }
        }

        public static DisInterval Positive
        {
            get
            {
                Contract.Ensures(Contract.Result<DisInterval>() != null);

                return cachedPositive;
            }
        }

        public static DisInterval Negative
        {
            get
            {
                Contract.Ensures(Contract.Result<DisInterval>() != null);

                return cachedNegative;
            }
        }


        #endregion

        #region To

        public override T To<T>(IFactory<T> factory)
        {
            if (this.IsBottom)
                return factory.IdentityForOr; // false
            if (this.IsTop)
                return factory.IdentityForAnd; // true
            T name;
            if (factory.TryGetName(out name))
            {
                // special cases for 0 and !=0 , as they are so common
                if (this.IsNotZero)
                {
                    return factory.NotEqualTo(name, factory.Constant(0));
                }
                else if (this.IsZero)
                {
                    return factory.EqualTo(name, factory.Constant(0));
                }
                else
                {
                    if (this.AsInterval.IsNormal)
                    {
                        return this.AsInterval.To(factory);
                    }
                }
            }

            return factory.IdentityForAnd;
        }

        #endregion

        #region ToSting, Equals, GetHashCode

        public override string ToString()
        {
            if (this.IsTop)
                return "Top";

            if (this.IsBottom)
                return "_|_";

            if (intervals.Count == 1)
            {
                return intervals[0].ToString();
            }

            return string.Format("({0})", ToString(intervals));
        }

        private string ToString(ReadOnlyIntervalList roCollection)
        {
            if (roCollection == null)
            {
                return "null";
            }
            var result = new StringBuilder();
            var count = 0;

            foreach (var itv in roCollection)
            {
                if (count != roCollection.Count - 1)
                {
                    result.Append(itv + " ");
                }
                else
                {
                    result.Append(itv.ToString());
                }

                count++;
            }

            return result.ToString();
        }

        [ContractVerification(true)]
        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = obj as DisInterval;
            if (other == null)
            {
                // it can be an interval
                var otherAsIntv = obj as Interval;
                if (otherAsIntv != null)
                {
                    return this.Equals(DisInterval.For(otherAsIntv));
                }

                return false;
            }


            Contract.Assume(other.intervals != null, "Assuming the object invariant of the other");

            return state == other.state && joinInterval.Equals(other.joinInterval) && HaveSameIntervals(intervals, other.intervals);
        }

        [Pure]
        private static bool HaveSameIntervals(ReadOnlyIntervalList left, ReadOnlyIntervalList right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            if (left.Count != right.Count)
            {
                return false;
            }

            for (var i = 0; i < left.Count; i++)
            {
                if (!left[i].Equals(right[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return state.GetHashCode() + joinInterval.GetHashCode();
        }
        #endregion

        static public class Ranges
        {
            private static readonly DisInterval uint8Range = DisInterval.For(Interval.Ranges.UInt8Range);
            private static readonly DisInterval uint16Range = DisInterval.For(Interval.Ranges.UInt16Range);
            private static readonly DisInterval uint32Range = DisInterval.For(Interval.Ranges.UInt32Range);
            private static readonly DisInterval uint64Range = DisInterval.For(Interval.Ranges.UInt64Range);

            private static readonly DisInterval int8Range = DisInterval.For(Interval.Ranges.Int8Range);
            private static readonly DisInterval int16Range = DisInterval.For(Interval.Ranges.Int16Range);
            private static readonly DisInterval int32Range = DisInterval.For(Interval.Ranges.Int32Range);
            private static readonly DisInterval int64Range = DisInterval.For(Interval.Ranges.Int64Range);

            public static DisInterval UInt8Range
            {
                get
                {
                    return uint8Range;
                }
            }

            public static DisInterval UInt16Range
            {
                get
                {
                    return uint16Range;
                }
            }

            public static DisInterval UInt32Range
            {
                get
                {
                    return uint32Range;
                }
            }

            public static DisInterval UInt64Range
            {
                get
                {
                    return uint64Range;
                }
            }

            public static DisInterval Int8Range
            {
                get
                {
                    return int8Range;
                }
            }

            public static DisInterval Int16Range
            {
                get
                {
                    return int16Range;
                }
            }

            public static DisInterval Int32Range
            {
                get
                {
                    return int32Range;
                }
            }

            public static DisInterval Int64Range
            {
                get
                {
                    return int64Range;
                }
            }

            public static DisInterval EnumValues<Type>(Type t, Func<Type, List<int>> enumranges)
            {
                var ranges = enumranges(t);
                if (ranges != null)
                {
                    return DisInterval.For(ranges.ConvertAll<Interval>(x => Interval.For(x)));
                }

                return Ranges.Int32Range;
            }
        }
    }
}
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// The Interface and the implementation for ints of the thresholds to be used to improved the precision of widenings

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Research.AbstractDomains.Numerical;
using System.Diagnostics.Contracts;


namespace Microsoft.Research.AbstractDomains
{
    /// <summary>
    /// A threshold gives some "points" where to jump during widening
    /// </summary>
    internal interface IThreshold<Type>
    {
        void Add(Type threshold);
        Type GetNext(Type aValue);
        Type GetPrevious(Type aValue);
        Type[] ToArray();
    }

    public class SimpleThreshold
    {
        [ThreadStatic]
        protected static List<int> userProvided;

        protected SimpleThreshold()
        {
        }

        static public List<int> UserProvided
        {
            get
            {
                if (userProvided == null)
                    userProvided = new List<int>();
                return userProvided;
            }
            set
            {
                userProvided = value;
            }
        }
    }

    abstract public class SimpleThreshold<Type> :
      SimpleThreshold,
      IThreshold<Type>
    {
        #region Constants
        protected const int DEFAULT_SIZE = 10;
        #endregion

        #region Private state

        protected int nextFree;           // The pointer to the first free position in the array
        protected readonly Type[] values;

        #endregion

        #region Protected State
        protected SimpleThreshold(int size)
        {
            Contract.Requires(size > 5);

            this.values = new Type[size];

            this.values[0] = this.MinusInfinity;
            this.values[1] = this.Zero;
            this.values[2] = this.PlusInfinity;

            this.nextFree = 3;

            var converted = UserProvided.ConvertAll<Type>(this.From);

            this.AddRange(converted);
        }
        #endregion

        #region To be implemented by the subclasses

        [Pure]
        abstract protected bool LessThan(Type t1, Type t2);

        abstract protected Type From(int x);

        abstract protected Type MinusInfinity
        {
            get;
        }

        abstract protected Type PlusInfinity
        {
            get;
        }

        abstract protected Type Zero
        {
            get;
        }

        #endregion

        #region Public methods
        /// <summary>
        /// Add the threshold to our list.
        /// </summary>
        /// <param name="threshold"></param>
        public void Add(Type threshold)
        {
            // TODO4: a better policy, for elimitaning the thresholds that are too close

            if (this.nextFree == this.values.Length)     // no more space, give up...
                return;

            int i = 0;
            for (; i < this.nextFree && LessThan(this.values[i], threshold); i++)     // search for the right position for the threshold
                ;

            if (this.values[i].Equals(threshold))        // Is the threshold already there?
                return;

            int j;
            for (j = this.nextFree; i < j; j--)     // Move all the elements one position on the right
                this.values[j] = this.values[j - 1];

            this.values[i] = threshold;
            this.nextFree++;
        }

        public void AddRange(IEnumerable<Type> from)
        {
            foreach (var x in from)
            {
                this.Add(x);
            }
        }

        #endregion

        #region IThreshold<Type, Type> Members

        public int Length
        {
            get
            {
                return this.values.Length;
            }
        }

        /// <summary>
        /// Get the smallest threshold larger than <code>aValue</code>
        /// </summary>
        public Type GetNext(Type aValue)
        {
            Contract.Assume(this.nextFree >= 1);

            return BinarySearch(aValue, 0, this.nextFree - 1);
        }

        public Type GetPrevious(Type aValue)
        {
            for (int i = 0; i < this.nextFree; i++)
            {
                if (LessThan(aValue, this.values[i]))
                {
                    return this.values[i - 1];
                }
            }

            return this.values[0];
        }

        public Type[] ToArray()
        {
            Type[] copy = new Type[this.values.Length];
            Array.Copy(this.values, copy, this.values.Length);

            return copy;
        }

        #endregion

        #region Private methods (BinarySearch)
        [Pure]
        private Type BinarySearch(Type inputValue, int inf, int sup)
        {
            Contract.Requires(inf >= 0);
            Contract.Requires(inf <= sup);

            if (inf == sup)
            {
                return this.values[sup];
            }
            else
            {
                int halfWay = (sup + inf) / 2;

                if (LessThan(inputValue, this.values[halfWay]))
                {
                    return BinarySearch(inputValue, inf, halfWay);
                }
                else if (LessThan(this.values[halfWay], inputValue))
                {
                    return BinarySearch(inputValue, halfWay + 1, sup);
                }
                else // if (this.values[halfWay] == inputvalue)
                {
                    return inputValue;
                }
            }
        }
        #endregion

        #region Overridden
        //^ [Confined]
        public override string/*!*/ ToString()
        {
            var result = new StringBuilder();
            var prev = this.MinusInfinity;

            for (var i = 0; i < this.values.Length && LessThan(prev, this.values[i]); i++)
            {
                result.Append(this.values[i] + ", ");
                prev = this.values[i];
            }

            if (result.Length >= 2)
            {
                result.Remove(result.Length - 2, 2);
            }

            return result.ToString();
        }
        #endregion
    }

    /// <summary>
    /// The implementation for a threshold of ints.
    /// It uses a static allocated array, so it cannot grow 
    /// </summary>
#if SUBPOLY_ONLY
    internal
#else
    public
#endif
 class SimpleRationalThreshold : SimpleThreshold<Rational>
    {
        #region Constructors
        /// <summary>
        /// Construct a Threshold object of default size
        /// </summary>
        public SimpleRationalThreshold()
          : this(DEFAULT_SIZE)
        { }

        /// <param name="size">must be > 2</param>
        public SimpleRationalThreshold(int size)
          : base(size)
        {
            Contract.Requires(size > 5);
        }
        #endregion

        #region Implementation of Abstact methods
        protected override bool LessThan(Rational t1, Rational t2)
        {
            return t1 < t2;
        }

        protected override Rational MinusInfinity
        {
            get { return Rational.MinusInfinity; }
        }

        protected override Rational PlusInfinity
        {
            get { return Rational.PlusInfinity; }
        }

        protected override Rational Zero
        {
            get { return Rational.For(0); }
        }

        protected override Rational From(int x)
        {
            return Rational.For(x);
        }
        #endregion
    }

    /// <summary>
    /// The implementation for a threshold of ints.
    /// It uses a static allocated array, so it cannot grow 
    /// </summary>
#if SUBPOLY_ONLY
    internal
#else
    public
#endif
 class SimpleDoubleThreshold : SimpleThreshold<Double>
    {
        #region Constructors
        /// <summary>
        /// Construct a Threshold object of default size
        /// </summary>
        public SimpleDoubleThreshold()
          : this(DEFAULT_SIZE)
        { }

        /// <param name="size">must be > 2</param>
        public SimpleDoubleThreshold(int size)
          : base(size)
        {
            Contract.Requires(size > 5);
        }
        #endregion

        #region Implementation of Abstact methods
        protected override bool LessThan(Double t1, Double t2)
        {
            return t1 < t2;
        }

        protected override double MinusInfinity
        {
            get { return Double.NegativeInfinity; }
        }

        protected override double PlusInfinity
        {
            get { return Double.PositiveInfinity; }
        }

        protected override double Zero
        {
            get { return 0.0; }
        }

        protected override double From(int x)
        {
            return (double)x;
        }

        #endregion
    }


    /// <summary>
    /// The implementation for a threshold of ints.
    /// It uses a static allocated array, so it cannot grow 
    /// </summary>
#if SUBPOLY_ONLY
    internal
#else
    public
#endif
 class IntThreshold : SimpleThreshold<Int32>
    {
        #region Constructors
        /// <summary>
        /// Construct a Threshold object of default size
        /// </summary>
        public IntThreshold()
          : this(DEFAULT_SIZE)
        { }

        /// <param name="size">must be > 2</param>
        public IntThreshold(int size)
          : base(size)
        {
        }
        #endregion

        #region Overridden

        protected override bool LessThan(int t1, int t2)
        {
            return t1 < t2;
        }

        protected override int MinusInfinity
        {
            get { return Int32.MinValue; }
        }

        protected override int PlusInfinity
        {
            get { return Int32.MaxValue; }
        }

        protected override int Zero
        {
            get { return 0; }
        }

        protected override int From(int x)
        {
            return x;
        }
        #endregion
    }


    /// <summary>
    /// A database that contains some thresholds using for the analysis
    /// </summary>
#if SUBPOLY_ONLY
    internal
#else
    public
#endif
    static class ThresholdDB
    {
        // [ThreadStatic]
        static private SimpleRationalThreshold thresholds_Rational;
        // [ThreadStatic]
        static private SimpleDoubleThreshold thresholds_Double;

        // Reset always called before these fields are used?
        public static void Reset()
        {
            thresholds_Rational = new SimpleRationalThreshold();
            thresholds_Double = new SimpleDoubleThreshold();
        }

        public static void Add(List<int> values)
        {
            foreach (var val in values)
            {
                thresholds_Rational.Add(Rational.For(val));
            }
        }

        public static void Add(Double val)
        {
            thresholds_Double.Add(val);
        }

        public static Rational GetNext(Rational v)
        {
            Contract.Requires(((object)v) != null);
            Contract.Ensures(((object)Contract.Result<Rational>()) != null);

            var tmp = thresholds_Rational.GetNext(v);

            Contract.Assume(((object)tmp) != null);

            return tmp;
        }

        public static Double GetNext(Double v)
        {
            return thresholds_Double.GetNext(v);
        }

        public static Rational GetPrevious(Rational v)
        {
            Contract.Requires(((object)v) != null);
            Contract.Ensures(((object)Contract.Result<Rational>()) != null);

            var tmp = thresholds_Rational.GetPrevious(v);

            Contract.Assume(((object)tmp) != null);

            return tmp;
        }

        public static Double GetPrevious(Double v)
        {
            return thresholds_Double.GetPrevious(v);
        }
    }
}
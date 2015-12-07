// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// The implementation of Octagons. 
// There are two classes: CoreOctagon that is the plain octagon domain and OctagonEnvironment that is the generic one for the analysis

#define NOArray
#define PRINT_PLAIN_OCTAGON

#if DEBUG
//#define TRACE_AD_PERFORMANCES // In release builds, we do not want to knwo how many time we spend in joining, meeting, etc.
#endif

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics.Contracts;
using Microsoft.Research.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains.Numerical
{
    /// <summary>
    /// The Core implementation of the Octagons abstract domain 
    /// F: I decupled the implementation of CoreOctagon from OctagonEnvironment to simplify the code, which otheriwse would have
    /// caused a "too big" class
    /// </summary>
    public abstract class CoreOctagon
    {
        #region Const
        protected const string STR_FOR_DIMENSION = "d";
        protected const int DimensionGrowthStep = 4;        // The dimensions to be added at each step in which the octagon is enlarged
        #endregion

        #region Constants used by the octagon
        internal protected enum OctagonState { EMPTY, NORMAL, CLOSED, BOTTOM }
        #endregion

        #region Getters for Profile informations

#if TRACE_AD_PERFORMANCES
        public static TimeSpan TimeSpentInDuplicate
        {
            get
            {
                return timeSpentInDuplicate;
            }
        }

        public static TimeSpan TimeSpentInDoClousure
        {
            get
            {
                return timeSpentInDoClosure;
            }
        }

        public static TimeSpan TimeSpentInDomainOperations
        {
            get
            {
                return timeSpentInDomainOperations;
            }
        }

        public static TimeSpan TimeSpentInAssignments
        {
            get
            {
                return timeSpentInAssignments;
            }
        }

        public static long CountDoClosureInvocations
        {
            get
            {
                return countDoClosureInvocations;
            }
        }

        public static long CountDomainOperationsInvocations
        {
            get
            {
                return countDomainOperationsInvocations;
            }
        }

#endif

        virtual public string Statistics
        {
            get
            {
                StringBuilder output = new StringBuilder();
#if TRACE_AD_PERFORMANCES
                output.AppendLine("# of closure invocations              : " + CountDoClosureInvocations);
                output.AppendLine("# of domain operation invocations     : " + CountDomainOperationsInvocations);
                output.AppendLine("Time spent in closing the octagon     : " + TimeSpentInDoClousure.ToString());
                output.AppendLine("Time spent in duplicating the octagon : " + TimeSpentInDuplicate.ToString());
                output.AppendLine("Time spent in doing the octassignment : " + timeSpentInAssignments.ToString());
                output.AppendLine("Time spent in LessEq/Join/Widen/Meet  : " + TimeSpentInDomainOperations.ToString());
#endif
                return output.ToString();
            }
        }

        [Conditional("TRACE_AD_PERFORMANCES")]
        protected void AddTimeElapsedFromStart(string msg, ref TimeSpan counter, TimeSpan elapsed)
        {
            counter += elapsed;

            //Log("{0}, {1} total time", msg, timeSpentInDoClosure.ToString());
        }

        [Conditional("TRACE_AD_PERFORMANCES")]
        protected static void Increase(ref long value)
        {
            value++;
        }

        #endregion

        #region Private fields: CoreOctagon State Variables

#if TRACE_AD_PERFORMANCES
        // Begin Profiling variables
        private static TimeSpan timeSpentInDuplicate = new TimeSpan(0);                       // The time spent for duplicating an octagon
        private static TimeSpan timeSpentInDoClosure = new TimeSpan(0);                 // The time spent for closing the octagons
        protected static TimeSpan timeSpentInDomainOperations = new TimeSpan(0);        // The time spent for doing Join, Meet and Widenings
        protected static TimeSpan timeSpentInAssignments = new TimeSpan(0);             // The time spent in doing the assignments in the octagon
        private static long countDoClosureInvocations = 0;                                    // Count how many times we invoked DoClosure
        protected static long countDomainOperationsInvocations = 0;                      // Count how many times we invoker the LessEqual/Join/Meet/Widening
                                                                                         // End Profiling variables
#endif

        private int bottomDimension = -1;                       // Helper that tell us, in the Closure, which index is bottom

        protected Set<int> availableDimensions;                     // The set of unused variables

        private int dimensions;                                               // The dimensions of the octagon

        internal protected OctagonState state;                            // The state of the octagon

        internal protected SparseRationalArray octagon;

        // We use the optimization of allocation a vector of the size that is the double of the required one, so to reduce the overhead in adding new dimensions

        #endregion

        #region Protected (Must override) members

        abstract protected CoreOctagon Factory(int dim);

        abstract protected CoreOctagon Factory(int dim, bool allocateMatrix);

        public abstract object Clone();

        #endregion

        #region Properties about this octagon
        /// <summary>
        /// The number of dimensions of the octagon
        /// </summary>
        protected int Dimensions
        {
            get
            {
                return dimensions;
            }
        }

        //^ invariant this.FreeDimensions.Count + this.AllocatedDimensions.Count == this.Dimensions;

        ///<summary>
        /// The dimensions not allocated
        ///</summary>
        ///<return>A fresh set containing the dimensions that are constrained by this octagon</return>
        public Set<int> FreeDimensions
        {
            get
            {
                return new Set<int>(this.availableDimensions);
            }
        }

        /// <summary>
        /// The allocated dimensions
        /// </summary>
        /// <returns>A fresh set containing the dimensions that are "free"</returns>
        public Set<int> AllocatedDimensions
        {
            get
            {
                Set<int> result = new Set<int>();
                for (int i = 0; i < this.Dimensions; i++)
                {
                    if (!this.availableDimensions.Contains(i))
                        result.Add(i);
                }
                return result;
            }
        }

        ///<summary>
        ///The number of constraints in this octagon
        ///</summary>
        public int NumberOfConstraints
        {
            get
            {
                if (this.state == OctagonState.EMPTY || this.state == OctagonState.BOTTOM)
                    return 0;

                int size = Matsize(dimensions);
                int count = 0;

                for (int i = 0; i < size; i++)
                {
                    if (!this.octagon[i].IsInfinity)
                    {
                        count++;
                    }
                }

                return count - 2 * dimensions; // remove the 2*n constraints of the form x(i) - x(i) <= 0
            }
        }

        ///<summary>
        /// The octagon is in a closed form?
        ///</summary>
        public bool IsClosed
        {
            get
            {
                return this.state != OctagonState.NORMAL;
            }
        }

        ///<summary>
        /// The octagon is empty?
        ///</summary>
        public bool IsEmpty
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Construct an octagon of size n. 
        /// The constraints are set to +oo, execept for those relating the variables themselves.
        /// 
        /// <param name="n"> The number of dimensions of the octagon </param>
        /// <remarks>It can throw the exception <code>OutOfMemoryException</code>if n is too large</remarks>
        /// </summary>
        public CoreOctagon(int n)
        {
            dimensions = n;

            try
            {
                this.octagon = SparseRationalArray.New(Matsize(n), Rational.PlusInfinity);
            }
            catch (OutOfMemoryException)
            {
                // If we cannot allocate an array of double the size, let's jsut go with the usual one
                this.octagon = SparseRationalArray.New(Matsize(n), Rational.PlusInfinity);
            }

            this.availableDimensions = new Set<int>();

            for (int i = 0; i < 2 * n; i++)
                this.octagon[Matpos(i, i)] = Rational.For(0);

            this.state = OctagonState.CLOSED;
        }

        /// <summary>
        /// A private octagon, used by duplicate. It does not allocate space for the new matrix.
        /// </summary>
        /// <param name="n">The number of dimensions for the octagon</param>
        /// <param name="allocateMatrix">Must be fals</param>
        protected CoreOctagon(int n, bool allocateMatrix)
        {
            Contract.Assert(!allocateMatrix, "Error: something is wrong, not supposed to be there...");

            dimensions = n;
            this.state = OctagonState.EMPTY;
            this.octagon = null;
            this.availableDimensions = null;
        }

        ///<summary>
        /// Creates a copy of the octagon from the parameter.
        /// The representation of the octagon is shared
        ///</summary>
        /// <param name="oct">The source octagon</param>
        protected CoreOctagon(CoreOctagon oct)
        {
            dimensions = oct.dimensions;
            this.state = oct.state;
            this.octagon = oct.octagon != null ? oct.octagon.Duplicate() : null;  // The octagon representation may be null when it is bottom
            this.availableDimensions = oct.availableDimensions;
        }

        protected void CloneFromThis(CoreOctagon oct)
        {
            dimensions = oct.dimensions;
            this.state = oct.state;

            Contract.Assert(oct.octagon != null || oct.state == OctagonState.BOTTOM, "The octagon should not be null there...");

            this.octagon = oct.octagon;

            this.availableDimensions = oct.availableDimensions;
        }
        #endregion

        #region Conversion methods to Intervals
        ///<summary>
        /// Get the lower and the upper bounds for a variable
        ///</summary>
        ///<param name="dim"> The dimension corresponding to the variable</param>
        ///<returns>An interval containing the lower and the upper bound for a variable in the octagon</returns>
        public Interval BoundsFor(int dim)
        {
            Contract.Assert(IsAllocatedDimension(dim)); // , "Error: the dimension " + dim + " is not in the octagon");  

            this.DoClosure();       // close the octagon

            if (this.IsBottom)
            {
                return Interval.UnknownInterval;
            }

            Rational lowerBound, upperBound;

            if (!this.octagon[Matpos(2 * dim, 2 * dim + 1)].IsInfinity)
            {           // -dim <= -v , v != -oo
                Rational tmp = this.octagon[Matpos(2 * dim, 2 * dim + 1)] / 2;
                lowerBound = -tmp;
            }
            else    // -oo = dim
            {
                lowerBound = Rational.MinusInfinity;
            }

            if (!this.octagon[Matpos(2 * dim + 1, 2 * dim)].IsInfinity)     // dim <= v  , v != +oo
            {
                upperBound = this.octagon[Matpos(2 * dim + 1, 2 * dim)] / 2;
            }
            else    // dim = +oo
            {
                upperBound = Rational.PlusInfinity;
            }

            return Interval.For(lowerBound, upperBound);
        }

        #endregion

        #region Implementation for IsBottom and IsTop
        /// <returns>
        /// True iff this octagon is empty.
        /// We may want to override it, so we make it virtual
        /// </returns>
        public virtual bool IsBottom
        {
            get
            {
                return this.state == OctagonState.BOTTOM || this.IsEmpty;
            }
        }

        /// <returns>
        /// True iff all the constraints are +oo
        /// We may want to override it, so we make it virtual
        /// </returns>
        public virtual bool IsTop
        {
            get
            {
                int n2 = dimensions * 2;

                if (this.state == OctagonState.EMPTY)
                {
                    return true;
                }

                if (this.state == OctagonState.BOTTOM)
                {
                    return false;
                }

                int pos = 0;
                for (int i = 0; i < n2; i++)
                {
                    int ii = i | 1;
                    for (int j = 0; j <= ii; j++, pos++)
                    {
                        if (!this.octagon[pos].IsInfinity && i != j)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        #endregion

        #region Closure, Equivalence, duplication, etc. on octagon

        ///<summary>
        /// Check if the given dimension is "allocated"
        ///</summary>
        protected bool IsAllocatedDimension(int dimension)
        {
            return dimension < this.Dimensions && !this.availableDimensions.Contains(dimension);
        }

        ///<summary>
        /// Duplicate the octagon.
        /// It makes a lazy copy of the octagon.
        /// The difference with Clone its a more precise return type (<code>CoreOctagon</code>)
        ///</summary>
        virtual public CoreOctagon Duplicate()
        {
            var watch = new CustomStopwatch();
            watch.Start();

            CoreOctagon octCopy = Factory(dimensions, false);

            octCopy.state = this.state;

            octCopy.octagon = this.state == OctagonState.BOTTOM ? null : this.octagon.Duplicate();

            octCopy.availableDimensions = new Set<int>(this.availableDimensions);
#if TRACE_AD_PERFORMANCES
            AddTimeElapsedFromStart("Duplicate", ref timeSpentInDuplicate, watch.Elapsed);
#endif
            return octCopy;
        }

        ///<summary>
        /// Close the octagon, by propagating all the constraints.
        /// The update is in place.
        ///</summary>
        ///<remarks>It may set the octagon representation to NULL (when it finds a contraddiction), so, when you use it, you should check if the octagon is not bottom</remarks>
        public void DoClosure()
        {
#if TRACE_AD_PERFORMANCES
            Increase(ref countDoClosureInvocations);
#endif
            if (this.state != OctagonState.NORMAL)
            {
                return; /* There is nothing to do */
            }

            var watch = new CustomStopwatch();
            watch.Start();

            var n2 = dimensions * 2;

            var buf1 = new Rational[n2];
            var buf2 = new Rational[n2];

            for (int k = 0; k < n2; k += 2)
            {
                #region Ck step

                Rational kk1, kk2;

                kk1 = this.octagon[Matpos(k + 1, k)]; // xk + xk
                kk2 = this.octagon[Matpos(k, k + 1)]; // -xk - xk

                int i;
                for (i = 0; i <= k; i += 2)
                {
                    buf1[i] = this.octagon[Matpos(k + 1, i + 1)]; // -xi + xk
                    buf2[i] = this.octagon[Matpos(k, i + 1)];    // -xi + xk
                    buf1[i + 1] = this.octagon[Matpos(k + 1, i)]; //  xi+xk 
                    buf2[i + 1] = this.octagon[Matpos(k, i)]; //  xi-xk 
                }

                for (; i < n2; i += 2)
                {
                    buf1[i] = this.octagon[Matpos(i, k)]; /*  xk-xi */
                    buf2[i] = this.octagon[Matpos(i, k + 1)]; /* -xk-xi */
                    buf1[i + 1] = this.octagon[Matpos(i + 1, k)]; /*  xk+xi */
                    buf2[i + 1] = this.octagon[Matpos(i + 1, k + 1)]; /* -xk+xi */
                }

                int count_d;
                for (i = 0, count_d = 0; i < n2; i++)
                {
                    var ii = i | 1;

                    for (int j = 0; j <= ii; j++, count_d++)
                    {
                        int jj = j ^ 1;

                        var ij1 = Add(buf1[i], buf2[jj]);
                        var ij2 = Add3(buf2[i], kk1, buf2[jj]);
                        var ij3 = Add(buf2[i], buf1[jj]);
                        var ij4 = Add3(buf1[i], kk2, buf1[jj]);

                        ij1 = Rational.Min(Rational.Min(ij1, ij2), Rational.Min(ij3, ij4));

                        // this.octagon[count_d] = Rational.Min(this.octagon[count_d], ij1);

                        DebugSet(count_d, Rational.Min(this.octagon[count_d], ij1));
                    }
                }
                #endregion

                count_d = 0;

                #region  S Step

                for (i = 0; i < n2; i += 2)
                {
                    buf1[i] = this.octagon[Matpos(i + 1, i)] / 2;   /* ( xi+xi)/2 */
                    buf1[i + 1] = this.octagon[Matpos(i, i + 1)] / 2;   /* (-xi-xi)/2 */
                }

                for (i = 0; i < n2; i++)
                {
                    var ii = i | 1;
                    var ii2 = i ^ 1;
                    for (int j = 0; j <= ii; j++, count_d++)
                    {
                        var ij1 = Add(buf1[j], buf1[ii2]);
                        //this.octagon[count_d] = Rational.Min(this.octagon[count_d], ij1);

                        DebugSet(count_d, Rational.Min(this.octagon[count_d], ij1));
                    }
                }
                #endregion

                #region Emptyness checking
                for (i = 0; i < n2; i += 2)
                {
                    if (this.octagon[Matpos(i, i)] < 0)
                    {
                        this.state = OctagonState.BOTTOM;
                        this.octagon = null;
                        bottomDimension = i;
#if TRACE_AD_PERFORMANCES
                        AddTimeElapsedFromStart("Closure", ref timeSpentInDoClosure, start);
#endif
                        return;
                    }
                }
                #endregion

                this.state = OctagonState.CLOSED;
            }

#if TRACE_AD_PERFORMANCES
            AddTimeElapsedFromStart(string.Format("Closure done in {0} ({1} variables)", watch.Elapsed, this.Dimensions), ref timeSpentInDoClosure, start);
#endif
        }

        private void DebugSet(int where, Rational val)
        {
            this.octagon[where] = val;
        }

        /// <summary>
        /// Debugging info: Which is the i such that this.octagon[Matpos(i, i)] \lt 0 ?
        /// </summary>
        public int WhyBottom
        {
            get
            {
                return bottomDimension;
            }
        }

        private static Rational Add(Rational x, Rational y)
        {
            try
            {
                return x + y;
            }
            catch (ArithmeticExceptionRational)
            {
                return Rational.PlusInfinity;
            }
        }

        private static Rational Add3(Rational x, Rational y, Rational z)
        {
            try
            {
                return x + y + z;
            }
            catch (ArithmeticExceptionRational)
            {
                return Rational.PlusInfinity;
            }
        }

        #endregion

        #region Transfer functions (projection, add a constraint, etc.)

        ///<summary>
        ///Project out a variable from the constraints (set it to top).
        ///The resulting octagon is then closed
        ///</summary>
        ///<param name="k">The dimension addociated to the variable to be projected</param>
        protected void ProjectDimension(int k)
        {
            Contract.Requires(k <= this.Dimensions, "Error: Trying to remove a variable not in the octagon");

            int k2 = 2 * k;
            int n2 = 2 * dimensions;

            this.DoClosure();

            if (this.state == OctagonState.BOTTOM || this.state == OctagonState.EMPTY)
            {
                return;
            }

            // Change the result matrix 
            for (var i = 0; i < k2; i++)
            {
                this.octagon[Matpos(k2, i)] = Rational.PlusInfinity;
                this.octagon[Matpos(k2 + 1, i)] = Rational.PlusInfinity;
            }

            for (var i = k2 + 2; i < n2; i++)
            {
                this.octagon[Matpos(i, k2)] = Rational.PlusInfinity;
                this.octagon[Matpos(i, k2 + 1)] = Rational.PlusInfinity;
            }

            this.octagon[Matpos(k2, k2 + 1)] = Rational.PlusInfinity;
            this.octagon[Matpos(k2 + 1, k2)] = Rational.PlusInfinity;
        }

        ///<summary>
        /// Remove a variable from the octagon
        ///</summary>
        ///<param name="dim">The dimension addociated to the variable to be projected</param>
        public void RemoveDimension(int dim)
        {
            this.ProjectDimension(dim);
            this.availableDimensions.Add(dim);      // Remember the removed varaible
        }

        ///<summary>
        /// Add a constraint to this octagon.
        /// If the octagon is Bottom, it's a nop
        ///</summary>
        virtual public void AddConstraint(OctagonConstraint constraint)
        {
            Contract.Assert(this.state == OctagonState.BOTTOM || constraint.X <= dimensions);

            if (this.state != OctagonState.BOTTOM)
            {
                bool hasChanged = constraint.AddToOctagon(this);

                if (hasChanged)
                {
                    this.state = OctagonState.NORMAL;
                }
            }
        }

        /// <summary>
        /// Add a set of constraints to this octagon
        /// </summary>
        /// <param name="constraints">The constraints to add</param>
        /// <returns>The affected dimensions </returns>
        public IMutableSet<int> AddConstraints(IMutableSet<OctagonConstraint> constraints)
        {
            bool hasChanged = false;

            if (this.state == OctagonState.BOTTOM)
                return new Set<int>();

            var affectedDimensions = new Set<int>();

            foreach (var cons in constraints)
            {
                Contract.Assert(cons.X <= dimensions);//, "Error: Variable index greater than the dimensions of the octagon");

                hasChanged = cons.AddToOctagon(this, affectedDimensions) || hasChanged;
            }
            // Improve: Use DoIncrementalClosure for closing the octagon if just one constraint has changed

            if (hasChanged && this.state != OctagonState.BOTTOM)    // The CoreOctagon is no more in the closed form
            {
                this.state = OctagonState.NORMAL;
            }

            return affectedDimensions;
        }

        ///<summary>
        /// Use this function to get a new dimension in the octagon, that can be associated with a memory address.
        /// If no dimension is available, then the octagon is enlarged, with n new dimensions (n is specified by DimensionGrowthStep)
        /// The returned dimensions is removed from the set of available locations
        ///</summary>
        ///<returns>A non-zero integer index of an available dimension, otherwise a negative number</returns>
        public int GetAvailableDimension()
        {
            if (this.availableDimensions.IsEmpty)
            {
                // No variable is left
                this.AddDimensions(DimensionGrowthStep);    // Add a new variable
                return this.GetAvailableDimension();          // return a fresh variable
            }
            else
            {
                // Return a "free" dimension in the octagon                

                int i = this.availableDimensions.PickAnElement();
                this.availableDimensions.Remove(i);

                return i;
            }
        }

        ///<summary>
        /// Add new dimensions to this octagon, without any constraint. 
        ///</summary>
        public void AddDimensions(int newDimensions)
        {
            int n1 = Matsize(dimensions);
            int n2 = Matsize(dimensions + newDimensions);

            if (this.state != OctagonState.BOTTOM)
            {
                SparseRationalArray newOctagon;

                if (this.octagon.Length <= n2)
                {
                    newOctagon = SparseRationalArray.New(n2 + 1, Rational.PlusInfinity);
                    newOctagon.CopyFrom(this.octagon);

                    // For the garbage collection
                    this.octagon = null;
                }
                else
                {
                    newOctagon = this.octagon;
                }

                for (int i = dimensions; i < 2 * (dimensions + newDimensions); i++)
                    newOctagon[Matpos(i, i)] = Rational.For(0);

                this.octagon = newOctagon;
            }

            // The new, added, dimensions are ready for use 
            for (int i = dimensions; i < dimensions + newDimensions; i++)
            {
                this.availableDimensions.Add(i);
            }
            dimensions += newDimensions;
        }

        ///<summary>
        /// Return the constraints in the octagon
        ///</summary>
        public ICollection<OctagonConstraint> ToConstraints()
        {
            var retValue = new List<OctagonConstraint>();
            OctagonConstraint con;

            this.DoClosure();

            if (this.IsBottom)
                return retValue;

            foreach (int i in this.AllocatedDimensions)
            {
                if (this.octagon[Matpos(2 * i, 2 * i)].IsNotZero)
                {
                    con = new OctagonConstraintXMinusY(i, i, Matpos(2 * i, 2 * 1));
                    retValue.Add(con);
                }
                if (this.octagon[Matpos(2 * i + 1, 2 * i + 1)].IsNotZero)
                {
                    con = new OctagonConstraintXMinusY(i, i, Matpos(2 * i + 1, 2 * i + 1));
                    retValue.Add(con);
                }
                if (!this.octagon[Matpos(2 * i + 1, 2 * i)].IsInfinity)
                {
                    con = new OctagonConstraintX(i, this.octagon[Matpos(2 * i + 1, 2 * i)] / 2);
                    retValue.Add(con);
                }
                if (!this.octagon[Matpos(2 * i, 2 * i + 1)].IsInfinity)
                {
                    con = new OctagonConstraintMinusX(i, this.octagon[Matpos(2 * i, 2 * i + 1)] / 2);
                    retValue.Add(con);
                }
            }

            for (int i = 0; i < dimensions; i++)
                for (int j = i + 1; j < dimensions; j++)
                {
                    if (!this.octagon[Matpos(2 * j, 2 * i)].IsInfinity)
                    {
                        con = new OctagonConstraintXMinusY(i, j, this.octagon[Matpos(2 * j, 2 * i)]);
                        retValue.Add(con);
                    }
                    if (!this.octagon[Matpos(2 * j, 2 * i + 1)].IsInfinity)
                    {
                        con = new OctagonConstraintMinusXMinusY(i, j, this.octagon[Matpos(2 * j, 2 * i + 1)]);
                        retValue.Add(con);
                    }
                    if (!this.octagon[Matpos(2 * j + 1, 2 * i)].IsInfinity)
                    {
                        con = new OctagonConstraintXY(i, j, this.octagon[Matpos(2 * j + 1, 2 * i)]);
                        retValue.Add(con);
                    }
                    if (!this.octagon[Matpos(2 * j + 1, 2 * i + 1)].IsInfinity)
                    {
                        con = new OctagonConstraintXMinusY(j, i, this.octagon[Matpos(2 * j + 1, 2 * i + 1)]);
                        retValue.Add(con);
                    }
                }
            return retValue;
        }

        #endregion

        #region Private methods

        ///<summary>
        /// The number of elements in a matrix with n variables
        ///</summary>s
        protected static int Matsize(int n)
        {
            return (2 * n * (n + 1));
        }

        ///<summary>
        /// Return the position of the matrix element (<code>i</code>,<code>j</code>) in the flat representation
        ///</summary>
        protected static int Matpos(int i, int j)
        {
            return (j + ((i + 1) * (i + 1)) / 2);
        }

        protected static int Matpos2(int i, int j)
        {
            return j > i ? Matpos((j ^ 1), (i ^ 1)) : Matpos(i, j);
        }

        #endregion

        #region Overridden (ToString())

        override public string ToString()
        {
            return ToStringRaw();
        }

        public string ToStringRaw()
        {
#if VERBOSEOUTPUT
            string postFix = dimensions + "-dimensions octagon) " + Environment.NewLine;
#else
            string postFix = "octagon)";
#endif

            if (this.state == OctagonState.EMPTY)
            {
                return "(empty " + postFix;
            }
            else if (this.state == OctagonState.BOTTOM)
            {
                return "(bottom " + postFix;
            }

            String retStr = "";

#if VERBOSEOUTPUT
            if (this.state == OctagonState.CLOSED)
                retStr = "(closed " + postFix;
            else
                retStr = "(NOT closed " + postFix;

            retStr += "(# of Constraints: " + this.NumberOfConstraints + ", ";
            retStr += "length of the representation: " + this.octagon.Length + ") " + Environment.NewLine;
            retStr += this.octagon.ToString() + Environment.NewLine;

#endif
#if PRINT_PLAIN_OCTAGON     // print the octagon in the plain form

            for (int i = 0; i < dimensions; i++)
            {
                if (this.octagon[Matpos(2 * i, 2 * i)] != 0)
                    retStr += STR_FOR_DIMENSION + i + " - " + STR_FOR_DIMENSION + i + " <= " + this.octagon[Matpos(2 * i, 2 * i)] + Environment.NewLine + " ";
                if (this.octagon[Matpos(2 * i + 1, 2 * i + 1)] != 0)
                    retStr += "-" + STR_FOR_DIMENSION + i + " + " + STR_FOR_DIMENSION + i + " <= " + this.octagon[Matpos(2 * i + 1, 2 * i + 1)] + Environment.NewLine + " ";
                if (!this.octagon[Matpos(2 * i + 1, 2 * i)].IsInfinity)
                    retStr += STR_FOR_DIMENSION + i + " <= " + this.octagon[Matpos(2 * i + 1, 2 * i)] / 2 + Environment.NewLine + " ";
                if (!this.octagon[Matpos(2 * i, 2 * i + 1)].IsInfinity)
                    retStr += "-" + STR_FOR_DIMENSION + i + " <= " + this.octagon[Matpos(2 * i, 2 * i + 1)] / 2 + Environment.NewLine + " ";
            }

            for (int i = 0; i < dimensions; i++)
                for (int j = i + 1; j < dimensions; j++)
                {
                    if (!this.octagon[Matpos(2 * j, 2 * i)].IsInfinity)
                        retStr += STR_FOR_DIMENSION + i + " - " + STR_FOR_DIMENSION + j + " <= " + this.octagon[Matpos(2 * j, 2 * i)] + Environment.NewLine + " ";
                    if (!this.octagon[Matpos(2 * j, 2 * i + 1)].IsInfinity)
                        retStr += "-" + STR_FOR_DIMENSION + i + " - " + STR_FOR_DIMENSION + j + " <= " + this.octagon[Matpos(2 * j, 2 * i + 1)] + Environment.NewLine + " ";
                    if (!this.octagon[Matpos(2 * j + 1, 2 * i)].IsInfinity)
                        retStr += STR_FOR_DIMENSION + i + " + " + STR_FOR_DIMENSION + j + " <= " + this.octagon[Matpos(2 * j + 1, 2 * i)] + Environment.NewLine + " ";
                    if (!this.octagon[Matpos(2 * j + 1, 2 * i + 1)].IsInfinity)
                        retStr += STR_FOR_DIMENSION + j + " - " + STR_FOR_DIMENSION + i + " <= " + this.octagon[Matpos(2 * j + 1, 2 * i + 1)] + Environment.NewLine + " ";
                }

#else               // a nicer way of printing the octagon constraints...
      if (this.IsBottom)
      {
        retStr = "Bottom (octagon)";
      }
      else
      {
        for (int i = 0; i < this.dimensions; i++)
        {
          if (this.octagon[Matpos(2 * i, 2 * i)] != 0)
          {
            retStr += STR_FOR_DIMENSION + i + " - " + STR_FOR_DIMENSION + i + " <= " + this.octagon[Matpos(2 * i, 2 * i)] + ", " + Environment.NewLine;
          }
          if (this.octagon[Matpos(2 * i + 1, 2 * i + 1)] != 0)
          {
            retStr += "-" + STR_FOR_DIMENSION + i + " + " + STR_FOR_DIMENSION + i + " <= " + this.octagon[Matpos(2 * i + 1, 2 * i + 1)] + ", " + Environment.NewLine;
          }
          if (!this.octagon[Matpos(2 * i, 2 * i + 1)].IsInfinity)         // -v <= k
          {
            retStr += -this.octagon[Matpos(2 * i, 2 * i + 1)]/2 + " <= "; // + STR_FOR_DIMENSION + i;
          }

          if (!this.octagon[Matpos(2 * i + 1, 2 * i)].IsInfinity)         // v <= k
          {
            retStr += STR_FOR_DIMENSION + i + " <= " + this.octagon[Matpos(2 * i + 1, 2 * i)] / 2  + ", " + Environment.NewLine;
          }
          else
          {
            if (!this.octagon[Matpos(2 * i, 2 * i + 1)].IsInfinity)
            {
              retStr += STR_FOR_DIMENSION + i + "," + Environment.NewLine;
            }
          }
        }

        for (int i = 0; i < this.dimensions; i++)
          for (int j = i + 1; j < this.dimensions; j++)
          {
            if (!this.octagon[Matpos(2 * j, 2 * i)].IsInfinity)         // vi - vj <= k
            {
              if (this.octagon[Matpos(2 * j, 2 * i)] == 0)        // vi -vj <= 0
              {
                retStr += STR_FOR_DIMENSION + i + " <= " + STR_FOR_DIMENSION + j + ", " + Environment.NewLine;
              }
              else
              {
                retStr += STR_FOR_DIMENSION + i + " - " + STR_FOR_DIMENSION + j + " <= " + this.octagon[Matpos(2 * j, 2 * i)] + ", " + Environment.NewLine;
              }
            }
            if (!this.octagon[Matpos(2 * j, 2 * i + 1)].IsInfinity)     // -vi - vj <= k
            {    // retStr += "-" + STR_FOR_DIMENSION + i + " - " + STR_FOR_DIMENSION + j + " <= " + this.octagon[Matpos(2 * j, 2 * i + 1)] + Environment.NewLine + " ";
              retStr += -this.octagon[Matpos(2 * j, 2 * i + 1)] + " <= ";// +STR_FOR_DIMENSION + i + " + v" + j;
            }
            if (!this.octagon[Matpos(2 * j + 1, 2 * i)].IsInfinity)     // vi + vj <= k
            {
              retStr += STR_FOR_DIMENSION + i + " + " + STR_FOR_DIMENSION + j + " <= " + this.octagon[Matpos(2 * j + 1, 2 * i)] + ", " + Environment.NewLine;
            }
            else
            {
              if (!this.octagon[Matpos(2 * j, 2 * i + 1)].IsInfinity)
              {
                retStr += STR_FOR_DIMENSION + i + " + " + STR_FOR_DIMENSION + j + ", " + Environment.NewLine;
              }
            }
            if (!this.octagon[Matpos(2 * j + 1, 2 * i + 1)].IsInfinity)
            {
              retStr += STR_FOR_DIMENSION + j + " - " + STR_FOR_DIMENSION + i + " <= " + this.octagon[Matpos(2 * j + 1, 2 * i + 1)] + ", " + Environment.NewLine;
            }
          }
      }
#endif

            return retStr;
        }
        #endregion

        protected void CheckConsistency()
        {
            // Invariant : this.octagon == null ==> this.state = Bottom
            Contract.Assert(this.octagon != null || this.state == OctagonState.BOTTOM, "I was not expecting null here...");
        }
    }

    /// <summary>
    /// The implementation of an abstraction of a set of program states <code>var -> val</code>, 
    /// where <code>var</code> is of type <code>Expression</code> and <code>val</code> is an <code>Int32</code>.
    /// 
    /// TODO: In the future consider to make this class generic w.r.t. the values 
    /// </summary>
    public sealed partial class OctagonEnvironment<Variable, Expression> :
      CoreOctagon, INumericalAbstractDomain<Variable, Expression>
    {
        #region OctagonPrecision
        public enum OctagonPrecision { JustTests, FullPrecision } // Meaning: justTests: ignores the assignments, and handles just the Test*
        #endregion

        #region Statics for OctagonEnvironment configuration
        private const int DEFAULTSIZE = 8;

        // The thresholds are shared by all the instances of OctagonEnvironment<Variable, Expression>
        [ThreadStatic]
        static private IThreshold<Rational> thresholds;                    // At the beginning we assume widening thresholds [0, +oo]
#if false // unused?
        [ThreadStatic]
        static private int MaxDim;      // The maximum size number of dimensions ever hold in an octagon
#endif

        private static IThreshold<Rational> Thresholds // Lazy initialization because of ThreadStatic
        {
            get
            {
                if (thresholds == null)
                    thresholds = new SimpleRationalThreshold();
                return thresholds;
            }
        }
        #endregion

        #region Private state
        readonly private OctagonPrecision precision;   // The precision in octagons, by default we keep just tests

        readonly private ExpressionManagerWithEncoder<Variable, Expression> expManager;

        readonly private OctagonsTrueTestVisitor<Variable, Expression> trueTestVisitor;
        readonly private OctagonsTrueTestNotRefiningVisitor<Variable, Expression> trueTestNotRefiningVisitor;
        readonly private OctagonsFalseTestVisitor<Variable, Expression> falseTestVisitor;

        readonly private IntervalEnvironment_Base<IntervalEnvironment<Variable, Expression>, Variable, Expression, Interval, Rational>.EvalConstantVisitor constantVisitor;

        readonly private List<IConstraintsForAssignment<Variable, Expression, INumericalAbstractDomainQuery<Variable, Expression>>> constraintsForAssignment;

        private BijectiveMap<Variable, int> variables2dimensions;        // The conversion map between variables and dimensions in the octagon

        #endregion

        #region Dimension for a variable
        /// <returns>
        /// The dimension associated with the variable, if any.
        /// Otherwise it extends this octagon, and add the new variable
        /// </returns>
        internal int DimensionForVar(Variable var)
        {
            if (!variables2dimensions.ContainsKey(var))
            {
                this.AddVariable(var);
            }

            // Should be there now!
            return variables2dimensions[var];
        }

        #endregion

        #region Precision in the handling of assignments

        /// <summary>
        /// Set or get if we have to precision we have to use for the assigment octagons
        /// </summary>
        public OctagonPrecision Precision
        {
            get { return precision; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// The starting point for an environment approximated by an octagon
        /// </summary>
        public OctagonEnvironment(ExpressionManagerWithEncoder<Variable, Expression> expManager, OctagonPrecision precision)
          : this(DEFAULTSIZE, expManager, precision)
        {
            this.state = OctagonState.EMPTY;

            Contract.Assert(this.state == OctagonState.EMPTY);
        }

        /// <summary>
        /// Construct an octagon with <code>n</code> dimensions and no map between variables and dimensions in it
        /// </summary>
        private OctagonEnvironment(int n, ExpressionManagerWithEncoder<Variable, Expression> expManager, OctagonPrecision precision)
          : base(n)
        {
            this.expManager = expManager;

            this.precision = precision;

            trueTestVisitor = new OctagonsTrueTestVisitor<Variable, Expression>(this.expManager.Decoder);
            trueTestNotRefiningVisitor = new OctagonsTrueTestNotRefiningVisitor<Variable, Expression>(this.expManager.Decoder);
            falseTestVisitor = new OctagonsFalseTestVisitor<Variable, Expression>(this.expManager.Decoder);
            trueTestVisitor.FalseVisitor = falseTestVisitor;
            trueTestNotRefiningVisitor.FalseVisitor = falseTestVisitor;
            falseTestVisitor.TrueVisitor = trueTestVisitor;

            constraintsForAssignment = new List<IConstraintsForAssignment<Variable, Expression, INumericalAbstractDomainQuery<Variable, Expression>>>()
      {
        new LessEqualThanConstraints<Variable, Expression>(this.expManager),
        new LessThanConstraints<Variable, Expression>(this.expManager),
        new GreaterEqualThanZeroConstraints<Variable, Expression>(this.expManager)
      };

            constantVisitor = new IntervalEnvironment_Base<IntervalEnvironment<Variable, Expression>, Variable, Expression, Interval, Rational>.EvalConstantVisitor(this.expManager.Decoder);

            variables2dimensions = new BijectiveMap<Variable, int>(n);

            Contract.Assert(this.availableDimensions != null);

            for (int i = 0; i < n; i++)
            {
                this.availableDimensions.Add(i);
            }
        }

        /// <summary>
        /// Copy constructor #1
        /// </summary>
        private OctagonEnvironment(OctagonEnvironment<Variable, Expression> oct)
          : base(oct)
        {
            expManager = oct.expManager;
            precision = oct.precision;

            trueTestVisitor = oct.trueTestVisitor;
            trueTestNotRefiningVisitor = oct.trueTestNotRefiningVisitor;
            falseTestVisitor = oct.falseTestVisitor;

            constantVisitor = oct.constantVisitor;

            constraintsForAssignment = oct.constraintsForAssignment;

            variables2dimensions = oct.variables2dimensions;

            // Contract.Assert(this.octagon != null, "Octagon should not be null there...");
        }

        private void CloneFromThis(OctagonEnvironment<Variable, Expression> oct)
        {
            base.CloneFromThis(oct);
            variables2dimensions = oct.variables2dimensions;
        }

        /// <summary>
        /// Copy constructor #2
        /// </summary>
        private OctagonEnvironment(int n, bool allocateMatrix, ExpressionManagerWithEncoder<Variable, Expression> expManager, OctagonPrecision precision)
          : base(n, allocateMatrix)
        {
            this.expManager = expManager;

            this.precision = precision;

            trueTestVisitor = new OctagonsTrueTestVisitor<Variable, Expression>(this.expManager.Decoder);
            trueTestNotRefiningVisitor = new OctagonsTrueTestNotRefiningVisitor<Variable, Expression>(this.expManager.Decoder);
            falseTestVisitor = new OctagonsFalseTestVisitor<Variable, Expression>(this.expManager.Decoder);
            trueTestVisitor.FalseVisitor = falseTestVisitor;
            trueTestNotRefiningVisitor.FalseVisitor = falseTestVisitor;
            falseTestVisitor.TrueVisitor = trueTestVisitor;

            constraintsForAssignment = new List<IConstraintsForAssignment<Variable, Expression, INumericalAbstractDomainQuery<Variable, Expression>>>()
      {
        new LessEqualThanConstraints<Variable,Expression>(this.expManager),
        new LessThanConstraints<Variable,Expression>(this.expManager),
        new GreaterEqualThanZeroConstraints<Variable, Expression>(this.expManager)
      };

            constantVisitor = new IntervalEnvironment_Base<IntervalEnvironment<Variable, Expression>, Variable, Expression, Interval, Rational>.EvalConstantVisitor(this.expManager.Decoder);

            variables2dimensions = null; // This will be set by Clone
        }

        #endregion

        #region Conversion methods for Intervals

        /// <summary>
        /// Get the bounds for the variable <code>v</code>
        /// </summary>
        public DisInterval BoundsFor(Expression exp)
        {
            return this.BoundsFor(expManager.Decoder.UnderlyingVariable(exp));
        }

        public DisInterval BoundsFor(Variable v)
        {
            if (this.state != OctagonState.EMPTY && this.state != OctagonState.BOTTOM && variables2dimensions.Keys.Contains(v))
            {
                return base.BoundsFor(variables2dimensions[v]).AsDisInterval;
            }
            else
            {
                return DisInterval.UnknownInterval;
            }
        }

        public void AssumeInDisInterval(Variable x, DisInterval value)
        {
            CheckConsistency();

            if (this.Precision == OctagonPrecision.FullPrecision)
            {
                if (!value.IsNormal)
                { // TODO: for the moment we ignore the "false" interval
                    this.RemoveVariable(x);
                }
                else
                {
                    if (!value.LowerBound.IsInfinity)
                    { // Add the constraint -x <= -value
                        this.AddConstraint(new OctagonConstraintMinusX(this.DimensionForVar(x), -value.LowerBound));
                    }

                    if (!value.UpperBound.IsInfinity)
                    { // Add the constraint x <= value
                        this.AddConstraint(new OctagonConstraintX(this.DimensionForVar(x), value.UpperBound));
                    }
                }
            }
            CheckConsistency();
        }

        public void AssumeDomainSpecificFact(DomainSpecificFact fact)
        {
        }

        /// <summary>
        /// Abstract an OctagonEnvironment to an IntervalEnvironment
        /// </summary>
        public IntervalEnvironment<Variable, Expression> ToIntervalEnvironment()
        {
            var result = new IntervalEnvironment<Variable, Expression>(expManager);

            foreach (var x in variables2dimensions.Keys)
            {
                result.AssumeInDisInterval(x, this.BoundsFor(x));
            }
            return result;
        }

        #endregion

        #region IAbstractDomain Members: Bottom, Top, Join, Meet, Widening 

        private enum BinaryOperationType { Join, Meet, Widening };

        private delegate Rational BinaryOperation(Rational left, Rational right);

        private static readonly BinaryOperation pointwiseWidening = delegate (Rational current, Rational prev)
            {
                if (Rational.Min(current, prev) == current)
                {   // The bound is stable
                    return prev;
                }
                else
                {   // The bound is not stable, take
                    var candidate = Thresholds.GetNext(current.NextInt32);
                    return candidate < Int32.MaxValue ? candidate : Rational.PlusInfinity;  // Gets the smaller value in the thresholds that is larger than b
                }
            };

        /// <summary>
        /// An OctagonEnvironment is bottom when the variable map is not empty, and the octagon is
        /// </summary>
        public override bool IsBottom
        {
            get
            {
#if TRACE_AD_PERFORMANCES

                countDomainOperationsInvocations++;

                var watch = new CustomStopwatch();
                watch.Start();
#endif

                var result = this.state == OctagonState.BOTTOM || (variables2dimensions.Count != 0 && base.IsBottom);

#if TRACE_AD_PERFORMANCES

                timeSpentInDomainOperations += watch.Elapsed;
#endif

                return result;
            }
        }

        public IAbstractDomain Bottom
        {
            get
            {
#if TRACE_AD_PERFORMANCES
                countDomainOperationsInvocations++;

                var watch = new CustomStopwatch();
                watch.Start();
#endif

                var bot = this.FactoryForOctagonEnvironment(2);
                bot.state = OctagonState.BOTTOM;

                if (variables2dimensions.Count > 0)
                {
                    bot.variables2dimensions = new BijectiveMap<Variable, int>(variables2dimensions);
                }
                else
                {
                    bot.variables2dimensions = new BijectiveMap<Variable, int>(1);
                    this.state = OctagonState.BOTTOM;
                }

#if TRACE_AD_PERFORMANCES
                timeSpentInDomainOperations += watch.Elapsed;
#endif
                return bot;
            }
        }

        /// <summary>
        /// An OctagonEnvironment is top when the variable map is empty or when the octagon has no constraints
        /// </summary>
        public override bool IsTop
        {
            get
            {
#if TRACE_AD_PERFORMANCES
                countDomainOperationsInvocations++;

                var watch = new CustomStopwatch();
                watch.Start();
#endif
                if (this.state == OctagonState.BOTTOM)
                    return false;

                bool result = variables2dimensions.Count == 0 || base.IsTop;
#if TRACE_AD_PERFORMANCES
                timeSpentInDomainOperations += watch.Elapsed;
#endif
                return result;
            }
        }

        public IAbstractDomain Top
        {
            get
            {
                OctagonEnvironment<Variable, Expression> tmp = this.FactoryForOctagonEnvironment(1);
                tmp.ProjectDimension(0);

                return tmp;
            }
        }

        bool IAbstractDomain.LessEqual(IAbstractDomain abs)
        {
            return this.LessEqual((OctagonEnvironment<Variable, Expression>)abs);
        }

        public bool LessEqual(OctagonEnvironment<Variable, Expression> right)
        {
#if TRACE_AD_PERFORMANCES

            Increase(ref countDomainOperationsInvocations);
#endif
            bool result;
            if (AbstractDomainsHelper.TryTrivialLessEqual(this, right, out result))
            {
                return result;
            }

            var watch = new CustomStopwatch();
            watch.Start();

            result = HelperForLessEqual(right);
#if TRACE_AD_PERFORMANCES

            AddTimeElapsedFromStart("LessEqual", ref timeSpentInDomainOperations, watch.Elapsed);
#endif
            return result;
        }

        IAbstractDomain IAbstractDomain.Join(IAbstractDomain a)
        {
            return this.Join((OctagonEnvironment<Variable, Expression>)a);
        }

        public OctagonEnvironment<Variable, Expression> Join(OctagonEnvironment<Variable, Expression> right)
        {
#if TRACE_AD_PERFORMANCES

            Increase(ref countDomainOperationsInvocations);
#endif
            OctagonEnvironment<Variable, Expression> result;
            if (AbstractDomainsHelper.TryTrivialJoin(this, right, out result))
            {
                return result;
            }

            var watch = new CustomStopwatch();
            watch.Start();

            HelperForBinaryDomainOperation(BinaryOperationType.Join, Rational.Max, right, out result);
#if TRACE_AD_PERFORMANCES

            AddTimeElapsedFromStart("Join", ref timeSpentInDomainOperations, watch.Elapsed);
#endif
            return result;
        }

        IAbstractDomain IAbstractDomain.Meet(IAbstractDomain a)
        {
            return this.Meet((OctagonEnvironment<Variable, Expression>)a);
        }

        public OctagonEnvironment<Variable, Expression> Meet(OctagonEnvironment<Variable, Expression> right)
        {
#if TRACE_AD_PERFORMANCES

            Increase(ref countDomainOperationsInvocations);
#endif
            OctagonEnvironment<Variable, Expression> result;
            if (AbstractDomainsHelper.TryTrivialMeet(this, right, out result))
            {
                return result;
            }

            var watch = new CustomStopwatch();
            watch.Start();
            HelperForBinaryDomainOperation(BinaryOperationType.Meet, Rational.Min, right, out result);
#if TRACE_AD_PERFORMANCES

            AddTimeElapsedFromStart("Meet", ref timeSpentInDomainOperations, watch.Elapsed);
#endif
            return result;
        }

        IAbstractDomain IAbstractDomain.Widening(IAbstractDomain prev)
        {
            return this.Widening((OctagonEnvironment<Variable, Expression>)prev);
        }

        /// <summary>
        /// Widening with thresholds.
        /// Please note that all the instances of OctagonEnvironment shares the same thresholds for widening
        /// </summary>
        /// <param name="prev"></param>
        /// <returns></returns>
        public OctagonEnvironment<Variable, Expression> Widening(OctagonEnvironment<Variable, Expression> prev)
        {
#if TRACE_AD_PERFORMANCES

            Increase(ref countDomainOperationsInvocations);
#endif
            OctagonEnvironment<Variable, Expression> result;

            if (AbstractDomainsHelper.TryTrivialJoin(this, prev, out result))
            {
                return result;
            }

            var watch = new CustomStopwatch();
            watch.Start();
            HelperForBinaryDomainOperation(BinaryOperationType.Widening, pointwiseWidening, prev, out result);

#if TRACE_AD_PERFORMANCES

            AddTimeElapsedFromStart("Widening", ref timeSpentInDomainOperations, watch.Elapsed);
#endif
            return result;
        }
        #endregion

        #region IPureExpressionAssignments Members (ProjectVariable, AddVariable, RemoveVariable, RenameVariable)

        public List<Variable> Variables
        {
            get
            {
                return new List<Variable>(variables2dimensions.Keys);
            }
        }

        /// <summary>
        /// Project the value of the variable <code>var</code>.
        /// It is as setting its value to top, or playing havoc 
        /// </summary>
        public void ProjectVariable(Variable var)
        {
            if (variables2dimensions.Keys.Contains(var))
            {
                base.ProjectDimension(variables2dimensions[var]);
            }
            else
            {
                // do nothing...
            }
        }

        /// <summary>
        /// Add a new variable to the environment.
        /// The variable must not be 
        /// </summary>
        public void AddVariable(Variable var)
        {
            // If the variable is already in the Octagon, then we have nothing to do
            if (variables2dimensions.ContainsKey(var))
            {
                return;
            }
            else
            {
                int newDim = base.GetAvailableDimension();
                variables2dimensions[var] = newDim;

                this.state = this.state != OctagonState.BOTTOM ? OctagonState.NORMAL : OctagonState.BOTTOM;
            }
        }

        /// <summary>
        /// Remove the variable from the current environment.
        /// Its values is projected, so that the relations are kept.
        /// The variable is removed from the octagon
        /// </summary>
        public void RemoveVariable(Variable var)
        {
            if (variables2dimensions.Keys.Contains(var))
            {
                base.RemoveDimension(variables2dimensions[var]);
                variables2dimensions.Remove(var);
            }
            else
            {
                // do nothing..
            }
        }

        /// <summary>
        /// Renames the variable <code>oldName</code> to <code>newName</code>
        /// </summary>
        public void RenameVariable(Variable oldName, Variable newName)
        {
            variables2dimensions[newName] = variables2dimensions[oldName];
            variables2dimensions.Remove(oldName);
        }

        #endregion

        #region IPureExpressionAssignmentsWithBackward<Expression> Members

        /// <summary>
        /// Performs the backward assignment for Octagons
        /// </summary>
        /// <param name="x">The variable to be assigned</param>
        /// <param name="exp">The source expression</param>
        public void AssignBackward(Expression x, Expression exp)
        {
            throw new AbstractInterpretationTODOException();
        }

        public void AssignBackward(Expression x, Expression exp, IAbstractDomain preState, out IAbstractDomain refinedPreState)
        {
            throw new AbstractInterpretationTODOException();
        }

        #endregion

        #region IPureExpressionTest Members (TestTrue)

        void IPureExpressionTest<Variable, Expression>.AssumeDomainSpecificFact(DomainSpecificFact fact)
        {
            this.AssumeDomainSpecificFact(fact);
        }

        #endregion

        #region Factories 

        override protected CoreOctagon Factory(int dim)
        {
            return this.FactoryForOctagonEnvironment(dim);
        }

        private OctagonEnvironment<Variable, Expression> FactoryForOctagonEnvironment(int dim)
        {
            return new OctagonEnvironment<Variable, Expression>(dim, expManager, this.Precision);
        }

        protected override CoreOctagon Factory(int dim, bool allocateMatrix)
        {
            return this.FactoryForOctagonEnvironment(dim, allocateMatrix);
        }

        private OctagonEnvironment<Variable, Expression> FactoryForOctagonEnvironment(int dim, bool allocateMatrix)
        {
            return new OctagonEnvironment<Variable, Expression>(dim, allocateMatrix, expManager, this.Precision);
        }

        new public OctagonEnvironment<Variable, Expression> Duplicate()
        {
            OctagonEnvironment<Variable, Expression> result = base.Duplicate() as OctagonEnvironment<Variable, Expression>;

            Contract.Assert(result != null, "Expecting an instance of OctagonEnvironment..."); //^ assert result != null;
            Contract.Assert(result != this, "Duplicate must create a fresh octagon reference");

            result.variables2dimensions = new BijectiveMap<Variable, int>(variables2dimensions);     // for this we are eager...

            return result;
        }

        public override object Clone()
        {
            return this.Duplicate();
        }

        public override bool Equals(object obj)
        {
            var that = obj as OctagonEnvironment<Variable, Expression>;
            if (that == null) return false;

            return (that.LessEqual(this) && this.LessEqual(that));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Does nothing
        /// </summary>
        /// <param name="oracle"></param>
        /// <returns></returns>
        public INumericalAbstractDomain<Variable, Expression> RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
        {
            return this.Duplicate();
        }

        #endregion

        #region ToString


        public T To<T>(IFactory<T> factory)
        {
            var result = factory.IdentityForAnd;

            this.DoClosure();

            if (this.IsBottom)
            {
                return factory.Constant(false);
            }

            if (this.IsTop)
            {
                return factory.Constant(true);
            }


            for (int i = 0; i < this.Dimensions; i++)
            {
                if (!variables2dimensions.ContainsValue(i))
                {
                    continue;
                }

                var x = variables2dimensions.KeyForValue(i);

                if (this.octagon[Matpos(2 * i, 2 * i)] != 0)
                {
                    // STR_FOR_DIMENSION + i + " - " + STR_FOR_DIMENSION + i + " <= " + this.octagon[Matpos(2 * i, 2 * i)] 
                    var tmp = ToOctagonConstraint(1, x, -1, x, this.octagon[Matpos(2 * i, 2 * i)], factory);
                    result = factory.And(result, tmp);
                }

                if (this.octagon[Matpos(2 * i + 1, 2 * i + 1)] != 0)
                {
                    // STR_FOR_DIMENSION + i + " + " + STR_FOR_DIMENSION + i + " <= " + this.octagon[Matpos(2 * i + 1, 2 * i + 1)] 
                    var tmp = ToOctagonConstraint(1, x, 1, x, this.octagon[Matpos(2 * i + 1, 2 * i + 1)], factory);
                    result = factory.And(result, tmp);
                }
                if (!this.octagon[Matpos(2 * i + 1, 2 * i)].IsInfinity)
                {
                    // STR_FOR_DIMENSION + i + " <= " + this.octagon[Matpos(2 * i + 1, 2 * i)] + "/2" ;
                    var tmp = ToOctagonConstraint(1, x, this.octagon[Matpos(2 * i + 1, 2 * i)] / 2, factory);
                    result = factory.And(result, tmp);
                }
                if (!this.octagon[Matpos(2 * i, 2 * i + 1)].IsInfinity)
                {
                    // "-" + STR_FOR_DIMENSION + i + " <= " + this.octagon[Matpos(2 * i, 2 * i + 1)] + "/2" + Environment.NewLine + " ";
                    var tmp = ToOctagonConstraint(-1, x, this.octagon[Matpos(2 * i + 1, 2 * i + 1)] / 2, factory);
                    result = factory.And(result, tmp);
                }
            }

            for (int i = 0; i < this.Dimensions; i++)
            {
                if (!variables2dimensions.ContainsValue(i))
                {
                    continue;
                }

                var x = variables2dimensions.KeyForValue(i);

                for (int j = i + 1; j < this.Dimensions; j++)
                {
                    if (!variables2dimensions.ContainsValue(j))
                    {
                        continue;
                    }

                    var y = variables2dimensions.KeyForValue(j);

                    if (!this.octagon[Matpos(2 * j, 2 * i)].IsInfinity)
                    {
                        // STR_FOR_DIMENSION + i + " - " + STR_FOR_DIMENSION + j + " <= " + this.octagon[Matpos(2 * j, 2 * i)]           
                        var tmp = ToOctagonConstraint(1, x, -1, y, this.octagon[Matpos(2 * j, 2 * i)], factory);
                        result = factory.And(result, tmp);
                    }

                    if (!this.octagon[Matpos(2 * j, 2 * i + 1)].IsInfinity)
                    {
                        // "-" + STR_FOR_DIMENSION + i + " - " + STR_FOR_DIMENSION + j + " <= " + this.octagon[Matpos(2 * j, 2 * i + 1)] 
                        var tmp = ToOctagonConstraint(-1, x, -1, y, this.octagon[Matpos(2 * j, 2 * i + 1)], factory);
                        result = factory.And(result, tmp);
                    }

                    if (!this.octagon[Matpos(2 * j + 1, 2 * i)].IsInfinity)
                    {
                        // STR_FOR_DIMENSION + i + " + " + STR_FOR_DIMENSION + j + " <= " + this.octagon[Matpos(2 * j + 1, 2 * i)] 
                        var tmp = ToOctagonConstraint(1, x, 1, y, this.octagon[Matpos(2 * j + 1, 2 * i)], factory);
                        result = factory.And(result, tmp);
                    }

                    if (!this.octagon[Matpos(2 * j + 1, 2 * i + 1)].IsInfinity)
                    {
                        // STR_FOR_DIMENSION + j + " - " + STR_FOR_DIMENSION + i + " <= " + this.octagon[Matpos(2 * j + 1, 2 * i + 1)] 
                        var tmp = ToOctagonConstraint(1, y, -1, x, this.octagon[Matpos(2 * j + 1, 2 * i + 1)], factory);
                        result = factory.And(result, tmp);
                    }
                }
            }

            return result;
        }

        public T ToOctagonConstraint<T>(int a, Variable x, int b, Variable y, Rational k, IFactory<T> factory)
        {
            T left, right;

            if (a != 0 && b != 0)
            {
                var m1 = factory.Mul(factory.Constant(a), factory.Variable(x));
                var m2 = factory.Mul(factory.Constant(b), factory.Variable(y));

                left = factory.Add(m1, m2);
            }
            else if (a != 0)
            { // b == 0
                left = factory.Mul(factory.Constant(a), factory.Variable(x));
            }
            else if (b != 0)
            { // a== 0
                left = factory.Mul(factory.Constant(b), factory.Variable(y));
            }
            else
            {
                throw new AbstractInterpretationException("Impossible case");
            }

            right = factory.Constant(k);

            return factory.LessEqualThan(left, right);
        }

        public T ToOctagonConstraint<T>(int a, Variable x, Rational k, IFactory<T> factory)
        {
            return ToOctagonConstraint<T>(a, x, 0, default(Variable), k, factory);
        }

        public override string ToString()
        {
            var oct = RenameDimensions(base.ToString());

            var env = new StringBuilder();

            foreach (var var in variables2dimensions.Keys)
            {
                env.Append("(" + var + ", " + variables2dimensions[var] + "),");
            }
            var env2str = env.ToString();
            var indexLastComma = env2str.LastIndexOf(",");
            env2str = indexLastComma > 0 ? env2str.Remove(indexLastComma) : env2str;

            return env2str + Environment.NewLine + oct;
        }

        public string ToString(Expression exp)
        {
            if (expManager.Decoder != null)
            {
                return ExpressionPrinter.ToString(exp, expManager.Decoder);
            }
            else
            {
                return "< missing expression decoder >";
            }
        }

        /// <summary>
        /// Takes the result of <code>base.ToString()</code> and rename all the occurrences of <code>vi</code> with the correct variable
        /// </summary>
        private string RenameDimensions(string oct)
        {
            // Contract.Assert(oct.CompareTo(base.ToString()) == 0, "You can invoke RenameDimensions just on the result of CoreOctagon.ToString()");

            var result = new StringBuilder(oct);

            foreach (var x in variables2dimensions.Keys)
            {
                Contract.Assert(x != null);
                //^ assert x != null;

                int i = variables2dimensions[x];

                result.Replace(STR_FOR_DIMENSION + i + " ", x.ToString());  // Trick: we need to pattern match the space!
            }

            return result.ToString();
        }

        #endregion

        #region Performance and debugging helpers
        // This is just for checking the soundness of the operation
        private Set<int> visitedIndexes = new Set<int>();

        // Used only to monitor the accesses to the octagon, for debugging
        private Rational this[int i]
        {
            set
            {
                this.octagon[i] = value;

                if (visitedIndexes.Contains(i))
                {
                    System.Diagnostics.Debugger.Break();
                }

                visitedIndexes.Add(i);
            }
        }

        private static void UpdateMaxDim(int x, int y)
        {
#if false // unused?
            MaxDim = Math.Max(MaxDim, Math.Max(x, y));
#endif
        }

        #endregion

        #region Helpers for Domain operations (LessEq, Join, Meet and Widening)
        /// <summary>
        /// The core method for OctagonEnvironments Join, Meet and Widening
        /// </summary>
        /// <param name="whichOperation">Which operation have we to apply?</param>
        /// <param name="pointwiseOp">The operation to be applyed, pointwise, to each element</param>
        /// <param name="right">The "right" OctagonEnvironment. It must have the SAME dimensions than this octagon</param>
        /// <param name="result">Meaningful iff whichOperation \in { Join, Meet, Widening }</param>
        private void HelperForBinaryDomainOperation(BinaryOperationType whichOperation, BinaryOperation pointwiseOp, OctagonEnvironment<Variable, Expression> right, out OctagonEnvironment<Variable, Expression> result)
        {
            // PRECONDITION: !!!! The two octagons must have the same number of allocated variables !!!!

            CheckConsistency();

            OctagonEnvironment<Variable, Expression> left = this;

            OctagonEnvironment<Variable, Expression> resultOctagon;

            UpdateMaxDim(left.octagon.Length, right.octagon.Length);

            #region 1. Close the octagon, depending on the kind of the operation...
            if (left == right)
            {
                result = this;

                return;
            }

            // To ensure convergence for widening we have to pay attention when to close
            if (whichOperation == BinaryOperationType.Join || whichOperation == BinaryOperationType.Meet)
            {
                // for join and meet we can always close to have precision
                left.DoClosure();
                right.DoClosure();

                if (whichOperation == BinaryOperationType.Join && AbstractDomainsHelper.TryTrivialJoin(left, right, out result))
                {
                    return /*result*/;
                }
                else if (whichOperation == BinaryOperationType.Meet && AbstractDomainsHelper.TryTrivialMeet(left, right, out result))
                {
                    return /*result*/;
                }
            }
            else if (whichOperation == BinaryOperationType.Widening)
            {
                // For widenings we have to close just the previous iteration!
                right.DoClosure();

                if (AbstractDomainsHelper.TryTrivialJoin(left, right, out result))
                {
                    return /*result*/;
                }
            }
            else
            {
                Contract.Assert(false, "I do not know the operation + " + whichOperation);
            }

            #endregion

            #region 2. Create the result octagon

            // Compute an overapproximation of the variables in the result
            var varsInResult = new List<Variable>();
            switch (whichOperation)
            {
                case BinaryOperationType.Join:
                case BinaryOperationType.Widening:
                    {
                        varsInResult = left.Variables.SetIntersection(right.Variables);
                    }
                    break;

                case BinaryOperationType.Meet:
                    {
                        varsInResult = left.Variables.SetUnion(right.Variables);
                    }
                    break;

                default:
                    throw new AbstractInterpretationException("Unknown octagon operation: " + whichOperation);
            }

            resultOctagon = new OctagonEnvironment<Variable, Expression>(varsInResult.Count, expManager, this.Precision);    // The new result octagon

            // Now add them
            foreach (var x in varsInResult)
            {
                resultOctagon.AddVariable(x);
            }

            #endregion

            #region 3. Iterates over all the constraints in this OctagonEnvironment, find the corresponding in right, apply the binary operation and writes the result in the output
            foreach (var i in left.AllocatedDimensions)
            {
                var var = left.variables2dimensions.KeyForValue(i);                     // Gets the variable corresponding to i

                if (!varsInResult.Contains(var))
                {
                    continue;
                }

                var dimInRight = right.DimensionForVar(var);   // Get the dimension corresponding to this variable in the right octagon
                var dimInResult = resultOctagon.DimensionForVar(var); // Get the dimension corresponding to this variable in the result octagon

                Rational thisK, rightK;
                Rational resultOfCombination;

                // We consider all the constraints for the variable var

                // 1. OctagonConstraint XMinusY(i, i), this.octagon[Matpos(2 * i, 2 * i)]
                thisK = left.ConstantForXMinusY(i, i);
                rightK = right.ConstantForXMinusY(dimInRight, dimInRight);
                resultOfCombination = pointwiseOp(thisK, rightK);
                resultOctagon/*.octagon*/[Matpos2(2 * dimInResult, 2 * dimInResult)] = resultOfCombination;

                // 2. OctagonConstraint MinusXY, this.octagon[Matpos(2 * i + 1, 2 * i + 1)]
                thisK = left.ConstantForMinusXY(i, i);
                rightK = right.ConstantForMinusXY(dimInRight, dimInRight);
                resultOfCombination = pointwiseOp(thisK, rightK);
                resultOctagon/*.octagon*/[Matpos2(2 * dimInResult + 1, 2 * dimInResult + 1)] = resultOfCombination;

                // 3. OctagonConstraint X, this.octagon[Matpos(2 * i + 1, 2 * i)])
                thisK = left.ConstantForX(i);
                rightK = right.ConstantForX(dimInRight);
                resultOfCombination = pointwiseOp(thisK, rightK);
                resultOctagon/*.octagon*/[Matpos2(2 * dimInResult + 1, 2 * dimInResult)] = resultOfCombination;

                // 4. OctagonConstraint MinusX, this.octagon[Matpos(2 * i, 2 * i + 1)]
                thisK = left.ConstantForMinusX(i);
                rightK = right.ConstantForMinusX(dimInRight);
                resultOfCombination = pointwiseOp(thisK, rightK);
                resultOctagon/*.octagon*/[Matpos2(2 * dimInResult, 2 * dimInResult + 1)] = resultOfCombination;
            }

            // I use this method, instead of two iterators, as it is faster (I do not have to visit the dimensions*dimensions cases, but just 1/2(dimensions * dimensions)
            for (int i = 0; i < left.Dimensions; i++)
            {
                // If it is a dimension without a variable associated to it, then skip it
                if (!left.variables2dimensions.ContainsValue(i))
                {
                    continue;
                }

                var varForI = left.variables2dimensions.KeyForValue(i);     // The variable corresponding to the dimension i
                var dimInRightForI = right.DimensionForVar(varForI);          // The dimension, in right, corresponding to the variable varForI    
                var dimInResultForI = resultOctagon.DimensionForVar(varForI); // The dimension, in result, corresponding to the variable varForI

                for (var j = i + 1; j < left.Dimensions; j++)
                {
                    // If it is a dimension without a variable associated to it, then skip it
                    if (!left.variables2dimensions.ContainsValue(j))
                    {
                        continue;
                    }

                    var varForJ = left.variables2dimensions.KeyForValue(j);      // The variable corresponding to the dimension j         
                    var dimInRightForJ = right.DimensionForVar(varForJ);           // The dimension, in right, corresponding to the variable varForJ                
                    var dimInResultForJ = resultOctagon.DimensionForVar(varForJ);  // The dimension, in result, corresponding to the variable varForJ

                    Rational thisK, rightK;
                    Rational resultOfCombination;

                    // 1. OctagonConstraint XMinusY, this.octagon[Matpos(2 * j, 2 * i)])
                    thisK = left.ConstantForXMinusY(j, i);
                    rightK = right.ConstantForXMinusY(dimInRightForJ, dimInRightForI);
                    resultOfCombination = pointwiseOp(thisK, rightK);
                    resultOctagon/*.octagon*/[IndexForXMinusY(dimInResultForJ, dimInResultForI)] = resultOfCombination;

                    // 2. OctagonConstraint MinusXMinusY, this.octagon[Matpos(2 * j, 2 * i + 1)])
                    thisK = left.ConstantForMinusXMinusY(j, i);
                    rightK = right.ConstantForMinusXMinusY(dimInRightForJ, dimInRightForI);
                    resultOfCombination = pointwiseOp(thisK, rightK);
                    resultOctagon/*.octagon*/[IndexForMinusXMinusY(dimInResultForJ, dimInResultForI)] = resultOfCombination;

                    // 3. OctagonConstraint XY, this.octagon[Matpos(2 * j + 1, 2 * i)])                    
                    thisK = left.ConstantForXY(j, i);
                    rightK = right.ConstantForXY(dimInRightForJ, dimInRightForI);
                    resultOfCombination = pointwiseOp(thisK, rightK);
                    resultOctagon/*.octagon*/[IndexForXY(dimInResultForJ, dimInResultForI)] = resultOfCombination;

                    // 4. OctagonConstraint MinusXY, this.octagon[Matpos(2 * j + 1, 2 * i + 1)]))
                    thisK = left.ConstantForMinusXY(j, i);
                    rightK = right.ConstantForMinusXY(dimInRightForJ, dimInRightForI);
                    resultOfCombination = pointwiseOp(thisK, rightK);
                    resultOctagon/*.octagon*/[IndexForMinusXY(dimInResultForJ, dimInResultForI)] = resultOfCombination;
                }
            }

            #endregion

            result = resultOctagon;
        }

        private bool HelperForLessEqual(OctagonEnvironment<Variable, Expression> a)
        {
            // PRECONDITION: !!!! The two octagons must have the same number of allocated variables !!!!

            // We have to check the copies in order to avoid NON-termination when combined with widening...

            var left = this.Duplicate();
            var right = a.Duplicate();

            left.DoClosure();
            right.DoClosure();

            // we are adding those two as the truvual check done before does not enforce the closure

            if (left.IsBottom)
                return true;

            if (right.IsBottom)
                return false;

            #region 1. Compute the inverse map of this Var -> dimensions
            // TODO4: Improve perfomances with caching 

            var inverseMap = new Dictionary<int, Variable>(variables2dimensions.Count);                      // Dimensions -> Variables

            foreach (var x in variables2dimensions.Keys)
            {
                inverseMap[left.variables2dimensions[x]] = x;
            }

            #endregion

            #region 2. Iterates over all the constraints in this OctagonEnvironment, find the corresponding in right, check the leq property
            foreach (int i in left.AllocatedDimensions)
            {
                var var = inverseMap[i];                     // Gets the variable corresponding to i

                var dimInRight = right.DimensionForVar(var);   // Get the dimension corresponding to this variable in the right octagon

                Rational thisK, rightK;


                // We consider all the constraints for the variable <code>var</var>

                // 1. OctagonConstraint XMinusY(i, i), this.octagon[Matpos(2 * i, 2 * i)]
                thisK = left.ConstantForXMinusY(i, i);
                rightK = right.ConstantForXMinusY(dimInRight, dimInRight);
                if (rightK < thisK)
                    return false;

                // 2. OctagonConstraint MinusXY, this.octagon[Matpos(2 * i + 1, 2 * i + 1)]
                thisK = left.ConstantForMinusXY(i, i);
                rightK = right.ConstantForMinusXY(dimInRight, dimInRight);
                if (rightK < thisK)
                    return false;

                // 3. OctagonConstraint X, this.octagon[Matpos(2 * i + 1, 2 * i)])
                thisK = left.ConstantForX(i);
                rightK = right.ConstantForX(dimInRight);
                if (rightK < thisK)
                    return false;

                // 4. OctagonConstraint MinusX, this.octagon[Matpos(2 * i, 2 * i + 1)]
                thisK = left.ConstantForMinusX(i);
                rightK = right.ConstantForMinusX(dimInRight);
                if (rightK < thisK)
                    return false;
            }

            // I use this method, instead of two iterators, as it is faster (I do not have to visit the dimensions*dimensions cases, but just 1/2(dimensions * dimensions)
            for (int i = 0; i < left.Dimensions; i++)
            {
                // If it is a dimension without a variable associated to it, then skip it
                if (!inverseMap.ContainsKey(i))
                {
                    continue;
                }

                var varForI = inverseMap[i];                         // The variable corresponding to the dimension i
                var dimInRightForI = right.DimensionForVar(varForI);   // The dimension, in right, corresponding to the variable varForI    

                for (int j = i + 1; j < left.Dimensions; j++)
                {
                    // If it is a dimension without a variable associated to it, then skip it
                    if (!inverseMap.ContainsKey(j))
                    {
                        continue;
                    }

                    var varForJ = inverseMap[j];                         // The variable corresponding to the dimension j         
                    var dimInRightForJ = right.DimensionForVar(varForJ);   // The dimension, in right, corresponding to the variable varForJ                

                    Rational thisK, rightK;

                    // 1. OctagonConstraint XMinusY, this.octagon[Matpos(2 * j, 2 * i)])
                    thisK = left.ConstantForXMinusY(j, i);
                    rightK = right.ConstantForXMinusY(dimInRightForJ, dimInRightForI);
                    if (rightK < thisK)
                        return false;

                    // 2. OctagonConstraint MinusXMinusY, this.octagon[Matpos(2 * j, 2 * i + 1)])
                    thisK = left.ConstantForMinusXMinusY(j, i);
                    rightK = right.ConstantForMinusXMinusY(dimInRightForJ, dimInRightForI);
                    if (rightK < thisK)
                        return false;

                    // 3. OctagonConstraint XY, this.octagon[Matpos(2 * j + 1, 2 * i)])                    
                    thisK = left.ConstantForXY(j, i);
                    rightK = right.ConstantForXY(dimInRightForJ, dimInRightForI);
                    if (rightK < thisK)
                        return false;

                    // 4. OctagonConstraint MinusXY, this.octagon[Matpos(2 * j + 1, 2 * i + 1)]))
                    thisK = left.ConstantForMinusXY(j, i);
                    rightK = right.ConstantForMinusXY(dimInRightForJ, dimInRightForI);
                    if (rightK < thisK)
                        return false;
                }
            }
            #endregion

            return true;
        }

        #endregion

        #region Public Queries

        /// <summary>
        /// x + y \leq val
        /// </summary>
        /// <returns><code>true</code> if it succeeds</returns>
        internal bool TryConstantForXY(Variable x, Variable y, out Rational val)
        {
            if (variables2dimensions.ContainsKey(x) && variables2dimensions.ContainsKey(y))
            {
                val = this.ConstantForXY(DimensionForVar(x), DimensionForVar(y));

                return !val.IsInfinity;
            }
            else
            {
                val = default(Rational);
                return false;
            }
        }

        /// <summary>
        /// x - y \leq val
        /// </summary>
        /// <returns><code>true</code> if it succeeds</returns>
        internal bool TryConstantForXMinusY(Variable x, Variable y, out Rational val)
        {
            if (variables2dimensions.ContainsKey(x) && variables2dimensions.ContainsKey(y))
            {
                val = this.ConstantForXMinusY(DimensionForVar(x), DimensionForVar(y));
                return !val.IsInfinity;
            }
            else
            {
                val = default(Rational);
                return false;
            }
        }

        /// <summary>
        /// -x - y \leq val
        /// </summary>
        /// <returns><code>true</code> if it succeeds</returns>
        internal bool TryConstantForMinusXMinusY(Variable x, Variable y, out Rational val)
        {
            if (variables2dimensions.ContainsKey(x) && variables2dimensions.ContainsKey(y))
            {
                val = this.ConstantForMinusXMinusY(DimensionForVar(x), DimensionForVar(y));
                return !val.IsInfinity;
            }
            else
            {
                val = default(Rational);
                return false;
            }
        }

        /// <summary>
        /// x \leq val
        /// </summary>
        /// <returns><code>true</code> if it succeeds</returns>
        internal bool TryConstantForX(Variable x, out Rational val)
        {
            if (variables2dimensions.ContainsKey(x))
            {
                val = this.ConstantForX(DimensionForVar(x));
                val = val / 2;    // This is because of the octagons encoding

                return !val.IsInfinity;
            }
            else
            {
                val = default(Rational);
                return false;
            }
        }

        /// <summary>
        /// -x \leq val
        /// </summary>
        /// <returns><code>true</code> if it succeeds</returns>
        internal bool TryConstantForMinusX(Variable x, out Rational val)
        {
            if (variables2dimensions.ContainsKey(x))
            {
                val = this.ConstantForMinusX(DimensionForVar(x));
                val = val / 2;  // This is because of the octagons encoding

                return !val.IsInfinity;
            }
            else
            {
                val = default(Rational);
                return false;
            }
        }

        #endregion

        #region Private Queries 

        /// <summary>
        /// Gets the constant (k) corresponding to the constraint X + Y \leq k
        /// </summary>
        private Rational ConstantForXY(int x, int y)
        {
            int j = 2 * x;
            int i = 2 * y + 1;
            return this.octagon[Matpos2(i, j)];
        }

        /// <summary>
        /// Gets the constant (k) corresponding to the constraint X - Y \leq k
        /// </summary>
        private Rational ConstantForXMinusY(int x, int y)
        {
            int j = 2 * x;
            int i = 2 * y;
            return this.octagon[Matpos2(i, j)];
        }

        /// <summary>
        /// Gets the constant (k) corresponding to the constraint - X + Y \leq k
        /// </summary>
        private Rational ConstantForMinusXY(int x, int y)
        {
            int j = 2 * x + 1;
            int i = 2 * y + 1;
            return this.octagon[Matpos2(i, j)];
        }

        /// <summary>
        /// Gets the constant (k) corresponding to the constraint - X - Y \leq k
        /// </summary>
        private Rational ConstantForMinusXMinusY(int x, int y)
        {
            int j = 2 * x + 1;
            int i = 2 * y;
            return this.octagon[Matpos2(i, j)];
        }

        /// <summary>
        /// Gets the constant (k) corresponding to the constraint X \leq k
        /// </summary>
        /// <returns>k is TWICE the real value, because of "two times" representation</returns>
        private Rational ConstantForX(int x)
        {
            int j = 2 * x;
            int i = 2 * x + 1;
            return this.octagon[Matpos2(i, j)];
        }

        /// <summary>
        /// Gets the constant (k) corresponding to the constraint - X \leq k
        /// </summary>
        /// <returns>k is TWICE the real value, because of "two times" representation</returns>
        private Rational ConstantForMinusX(int x)
        {
            int j = 2 * x + 1;
            int i = 2 * x;
            return this.octagon[Matpos2(i, j)];
        }

        #endregion

        #region Index computation
        /// <summary>
        /// Gets the index corresponding to the constraint X + Y \leq k
        /// </summary>
        static private int IndexForXY(int x, int y)
        {
            int j = 2 * x;
            int i = 2 * y + 1;
            return Matpos2(i, j);
        }

        /// <summary>
        /// Gets the index corresponding to the constraint X - Y \leq k
        /// </summary>
        static private int IndexForXMinusY(int x, int y)
        {
            int j = 2 * x;
            int i = 2 * y;
            return Matpos2(i, j);
        }

        /// <summary>
        /// Gets the index corresponding to the constraint - X + Y \leq k
        /// </summary>
        static private int IndexForMinusXY(int x, int y)
        {
            int j = 2 * x + 1;
            int i = 2 * y + 1;
            return Matpos2(i, j);
        }

        /// <summary>
        /// Gets the index corresponding to the constraint - X - Y \leq k
        /// </summary>
        static private int IndexForMinusXMinusY(int x, int y)
        {
            int j = 2 * x + 1;
            int i = 2 * y;
            return Matpos2(i, j);
        }

        #endregion

        #region TestTrue Members

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueGeqZero(Expression exp)
        {
            return this.TestTrueGeqZero(exp);
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueLessThan(Expression exp1, Expression exp2)
        {
            return this.TestTrueLessThan(exp1, exp2);
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueLessEqualThan(Expression exp1, Expression exp2)
        {
            return this.TestTrueLessEqualThan(exp1, exp2);
        }

        public OctagonEnvironment<Variable, Expression> TestTrueGeqZero(Expression exp)
        {
            if (!expManager.Decoder.IsConstant(exp))
            { // Create the constraint "exp >= 0"
                var newConstraint = new OctagonConstraintMinusX(this.DimensionForVar(expManager.Decoder.UnderlyingVariable(exp)), 0);
                this.AddConstraint(newConstraint);
            }

            return this;
        }

        public OctagonEnvironment<Variable, Expression> TestTrueLessThan(Expression e1, Expression e2)
        {
            // Add the direct

            // e1 < e2 is equivalent to e1 - e2 <= -1
            var intvForE1 = constantVisitor.Visit(e1, new IntervalEnvironment<Variable, Expression>(expManager));
            var intvForE2 = constantVisitor.Visit(e2, new IntervalEnvironment<Variable, Expression>(expManager));

            Rational valForE1, valForE2;
            var b1 = intvForE1.TryGetSingletonValue(out valForE1);
            var b2 = intvForE2.TryGetSingletonValue(out valForE2);

            var e1Var = expManager.Decoder.UnderlyingVariable(e1);
            var e2Var = expManager.Decoder.UnderlyingVariable(e2);

            OctagonConstraint directConstraint = null;

            // The four cases...
            if (b1 && !b2)
            { // k - e2 <= -1
                if (valForE1.IsInteger)
                {
                    directConstraint = OctagonConstraint.For(-1, this.DimensionForVar(e2Var), -1 - ((Int32)valForE1));
                }
            }
            else if (!b1 && b2)
            { // e1 - k <= -1
                if (valForE2.IsInteger)
                {
                    directConstraint = OctagonConstraint.For(1, this.DimensionForVar(e1Var), -1 + ((Int32)valForE2));
                }
            }
            else if (b1 && b2)
            {
                if (!(valForE1 < valForE2))
                {
                    return (OctagonEnvironment<Variable, Expression>)this.Bottom;
                }
            }
            else
            {
                directConstraint = OctagonConstraint.For(1, this.DimensionForVar(e1Var), -1, this.DimensionForVar(e2Var), Rational.For(-1));
            }

            if (directConstraint != null)
            {
                this.AddConstraint(directConstraint);
            }

            TestTrueLessThanSpecialCaseForPartitionAnalysis(e1, e2);

            // Now go inside an simplify it
            PolynomialOfInt<Variable, Expression> e1AsPoly, e2AsPoly, combined;
            if (PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(e1, expManager.Decoder, out e1AsPoly)
              && PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(e2, expManager.Decoder, out e2AsPoly)
              && PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessThan, e1AsPoly, e2AsPoly, out combined))
            {
                var newConstraints = new Set<OctagonConstraint>();
                if (new InferOctagonConstraints<Variable, Expression>().Visit(combined, this, ref newConstraints))
                {
                    this.AddConstraints(newConstraints);
                }
            }

            return this;
        }

        /// <summary>
        /// Special case needed for the partition analysis which wants to infer octogonal constraints on conditions in the form
        /// slackVar \lt (exp + k), without refining exp
        /// </summary>
        private void TestTrueLessThanSpecialCaseForPartitionAnalysis(Expression e1, Expression e2)
        {
            TestTrueLessThanOrLessEqualThanCaseSpecialCaseForPartitionAnalysis(e1, e2, 1);
        }

        private void TestTrueLessEqualThanSpecialCaseForPartitionAnalysis(Expression e1, Expression e2)
        {
            TestTrueLessThanOrLessEqualThanCaseSpecialCaseForPartitionAnalysis(e1, e2, 0);
        }

        private void TestTrueLessThanOrLessEqualThanCaseSpecialCaseForPartitionAnalysis(Expression e1, Expression e2, int offset)
        {
            // case slackVar \lt exp + k

            var e1Var = expManager.Decoder.UnderlyingVariable(e1);

            if (expManager.Decoder.IsSlackVariable(e1Var) && expManager.Decoder.IsBinaryExpression(e2))
            {
                var op = expManager.Decoder.OperatorFor(e2);

                var left = expManager.Decoder.LeftExpressionFor(e2);
                var right = expManager.Decoder.RightExpressionFor(e2);

                var leftIntv = constantVisitor.Visit(left, new IntervalEnvironment<Variable, Expression>(expManager));
                var rightIntv = constantVisitor.Visit(right, new IntervalEnvironment<Variable, Expression>(expManager));

                Rational v1, v2;

                var b1 = leftIntv.TryGetSingletonValue(out v1);
                var b2 = rightIntv.TryGetSingletonValue(out v2);

                OctagonConstraint newConstraint = null;

                switch (op)
                {
                    case ExpressionOperator.Addition:
                        {
                            if (b1 && !b2)
                            { // case x < v1 + right
                                newConstraint = OctagonConstraint.For(1, this.DimensionForVar(e1Var), -1, this.DimensionForVar(expManager.Decoder.UnderlyingVariable(right)), v1 - offset);
                            }
                            if (!b1 && b2)
                            {  // case x < left + v2
                                newConstraint = OctagonConstraint.For(1, this.DimensionForVar(e1Var), -1, this.DimensionForVar(expManager.Decoder.UnderlyingVariable(left)), v2 - offset);
                            }
                        }
                        break;

                    case ExpressionOperator.Subtraction:
                        {
                            if (b1 && !b2)
                            { // case x <= v1 - right
                                newConstraint = OctagonConstraint.For(1, this.DimensionForVar(e1Var), -1, this.DimensionForVar(expManager.Decoder.UnderlyingVariable(right)), -(v1 - offset));
                            }
                            if (!b1 && b2)
                            {  // case x <= left - v2
                                newConstraint = OctagonConstraint.For(1, this.DimensionForVar(e1Var), -1, this.DimensionForVar(expManager.Decoder.UnderlyingVariable(left)), -(v2 - offset));
                            }
                        }
                        break;

                    default:
                        // do nothing.
                        return;
                        //break;
                }
                if (newConstraint == null)
                {
                    return;
                }
                else
                {
                    this.AddConstraint(newConstraint);
                }
            }

            // case e1 \lt slackVar
            var e2Var = expManager.Decoder.UnderlyingVariable(e2);
            if (expManager.Decoder.IsSlackVariable(e2Var) && expManager.Decoder.IsBinaryExpression(e1))
            {
                var op = expManager.Decoder.OperatorFor(e1);

                var left = expManager.Decoder.LeftExpressionFor(e1);
                var right = expManager.Decoder.RightExpressionFor(e1);

                var leftIntv = constantVisitor.Visit(left, new IntervalEnvironment<Variable, Expression>(expManager));
                var rightIntv = constantVisitor.Visit(right, new IntervalEnvironment<Variable, Expression>(expManager));

                Rational v1, v2;

                var b1 = leftIntv.TryGetSingletonValue(out v1);
                var b2 = rightIntv.TryGetSingletonValue(out v2);

                OctagonConstraint newConstraint = null;

                switch (op)
                {
                    case ExpressionOperator.Addition:
                        {
                            if (b1 && !b2)
                            { // case v1 + right < e2
                                newConstraint = OctagonConstraint.For(1, this.DimensionForVar(expManager.Decoder.UnderlyingVariable(right)), -1, this.DimensionForVar(e2Var), -v1 - offset);
                            }
                            if (!b1 && b2)
                            {  // case left + v2 < e2
                                newConstraint = OctagonConstraint.For(1, this.DimensionForVar(expManager.Decoder.UnderlyingVariable(left)), -1, this.DimensionForVar(e2Var), -v2 - offset);
                            }
                        }
                        break;

                    case ExpressionOperator.Subtraction:
                        {
                            if (b1 && !b2)
                            { // case v1 - right < e2
                                newConstraint = OctagonConstraint.For(-1, this.DimensionForVar(expManager.Decoder.UnderlyingVariable(right)), -1, this.DimensionForVar(e2Var), -v1 - offset);
                            }
                            if (!b1 && b2)
                            {  // case left - v2 < e2
                                newConstraint = OctagonConstraint.For(1, this.DimensionForVar(expManager.Decoder.UnderlyingVariable(left)), -1, this.DimensionForVar(e2Var), v2 - offset);
                            }
                        }
                        break;

                    default:
                        // do nothing
                        return;
                        //break;
                }
                if (newConstraint == null)
                {
                    return;
                }
                else
                {
                    this.AddConstraint(newConstraint);
                }
            }
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueEqual(Expression exp1, Expression exp2)
        {
            return this.TestTrueEqual(exp1, exp2);
        }

        public OctagonEnvironment<Variable, Expression> TestTrueEqual(Expression exp1, Expression exp2)
        {
            this.CheckConsistency();

            return this.TestTrueLessEqualThan(exp1, exp2).TestTrueLessEqualThan(exp2, exp1);
        }

        /// <summary>
        /// A Special case of TestTrue that is needed for the partition analysis.
        /// </summary>
        public OctagonEnvironment<Variable, Expression> TestTrueEqualNotRefininingLeftArguments(Expression exp1, Expression exp2)
        {
            this.CheckConsistency();

            var res = this.TestTrueLessEqualThan(exp1, exp2).TestTrueLessEqualThan(exp2, exp1);

            return res.TestTrueEqualNotRefininingLeftArgumentsInternal(exp1, exp2);
        }

        public OctagonEnvironment<Variable, Expression> TestTrueNotEqual(Expression e1, Expression e2)
        {
            var e1Var = expManager.Decoder.UnderlyingVariable(e1);
            var e2Var = expManager.Decoder.UnderlyingVariable(e2);

            if (!variables2dimensions.ContainsKey(e1Var) || !variables2dimensions.ContainsKey(e2Var))
            {
                return this;
            }

            Rational k;

            if (this.TryConstantForXMinusY(e1Var, e2Var, out k) && k.IsZero)
            { // It is the case that exp1 <= exp2
                this.AddConstraint(new OctagonConstraintXMinusY(this.DimensionForVar(e1Var), this.DimensionForVar(e2Var), -1));
            }
            else if (this.TryConstantForXMinusY(e2Var, e1Var, out k) && k.IsZero)
            { // It is the case that exp2 <= exp1
                this.AddConstraint(new OctagonConstraintXMinusY(this.DimensionForVar(e2Var), this.DimensionForVar(e1Var), -1));
            }

            return this;
        }

        #endregion

        #region CheckIfHolds methods

        public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
        {
            return new OctagonsCheckIfHoldsVisitor<Variable, Expression>(expManager.Decoder).Visit(exp, this);
        }

        /// <summary>
        /// Check if the expression <code>exp</code> is greater than zero
        /// </summary>
        public FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
        {
            var result = CommonChecks.CheckGreaterEqualThanZero(exp, expManager.Decoder);
            if (!result.IsTop)
            {
                return result;
            }
            else
            {
                var tmp = this.Duplicate();
                tmp.DoClosure();

                if (tmp.IsBottom)
                    return CheckOutcome.Bottom;
                else
                    return tmp.ToIntervalEnvironment().CheckIfGreaterEqualThanZero(exp);
            }
        }

        /// <summary>
        /// Check if the expression <code>e1</code> is strictly smaller than <code>e2</code>
        /// </summary>
        public FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
        {
            var result = CommonChecks.CheckLessThan(e1, e2, this, expManager.Decoder);

            if (result.IsTop)
            { // Then we go deeper in the expression ...
              // We need to make a clone of this to make it simpler
                var duplicate = this.Duplicate();
                duplicate.DoClosure();

                if (duplicate.IsBottom)
                {
                    return CheckOutcome.Bottom;
                }
                else
                {
                    // Capture the cases when e1 or e2 are constants. To do it, we simply use the intervals
                    result = duplicate.ToIntervalEnvironment().CheckIfLessThan(e1, e2);
                }

                if (result.IsTop)
                {
                    PolynomialOfInt<Variable, Expression> e1AsPoly, e2AsPoly, tmp;
                    if (PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(e1, expManager.Decoder, out e1AsPoly)
                      && PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(e2, expManager.Decoder, out e2AsPoly)
                      && PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessThan, e1AsPoly, e2AsPoly, out tmp))
                    {
                        if (new CheckLessThanOrLessEqualThan<Variable, Expression>().Visit(tmp, duplicate, ref result))
                        {
                            return result;
                        }
                    }
                    else
                    {
                        var e1Var = expManager.Decoder.UnderlyingVariable(e1);
                        var e2Var = expManager.Decoder.UnderlyingVariable(e2);

                        var leftList = new List<MonomialOfInt<Variable>>() { new MonomialOfInt<Variable>(e1Var), new MonomialOfInt<Variable>(-1, e2Var) };
                        var rightList = new List<MonomialOfInt<Variable>>() { new MonomialOfInt<Variable>(-1) };

                        if (PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessEqualThan, leftList, rightList, out tmp))
                        {
                            if (new CheckLessThanOrLessEqualThan<Variable, Expression>().Visit(tmp, duplicate, ref result))
                            {
                                return result;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public FlatAbstractDomain<bool> CheckIfLessThan(Variable v1, Variable v2)
        {
            return this.CheckIfLessThan(expManager.Encoder.VariableFor(v1), expManager.Encoder.VariableFor(v2));
        }

        public FlatAbstractDomain<bool> CheckIfLessThan_Un(Expression e1, Expression e2)
        {
            return this.CheckIfLessThan(e1, e2);
        }

        /// <summary>
        /// Check if the expression <code>e1</code> is smaller equal than <code>e2</code>
        /// </summary>
        public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
        {
            var result = CommonChecks.CheckLessEqualThan(e1, e2, this, expManager.Decoder);

            if (!result.IsTop)
            {
                return result;
            }

            // We need to make a clone of this to make it simpler
            var duplicate = this.Duplicate();
            duplicate.DoClosure();

            if (duplicate.IsBottom)
            {
                return CheckOutcome.Bottom;
            }
            else
            {
                // Capture the cases when e1 or e2 are constants. To do it, we simply use the intervals
                result = duplicate.ToIntervalEnvironment().CheckIfLessEqualThan(e1, e2);
            }

            if (result.IsTop)
            {
                PolynomialOfInt<Variable, Expression> e1AsPoly, e2AsPoly, tmp;

                if (PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(e1, expManager.Decoder, out e1AsPoly)
                  && PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(e2, expManager.Decoder, out e2AsPoly)
                  && PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessEqualThan, e1AsPoly, e2AsPoly, out tmp))
                {
                    if (new CheckLessThanOrLessEqualThan<Variable, Expression>().Visit(tmp, duplicate, ref result))
                    {
                        return result;
                    }
                }
                else
                {
                    var e1Var = expManager.Decoder.UnderlyingVariable(e1);
                    var e2Var = expManager.Decoder.UnderlyingVariable(e2);

                    var leftList = new List<MonomialOfInt<Variable>>() { new MonomialOfInt<Variable>(e1Var), new MonomialOfInt<Variable>(-1, e2Var) };
                    var rightList = new List<MonomialOfInt<Variable>>() { new MonomialOfInt<Variable>(0) };

                    if (PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessEqualThan, leftList, rightList, out tmp))
                    {
                        if (new CheckLessThanOrLessEqualThan<Variable, Expression>().Visit(tmp, duplicate, ref result))
                        {
                            return result;
                        }
                    }
                }
            }

            return result;
        }

        public FlatAbstractDomain<bool> CheckIfLessThanIncomplete(Expression e1, Expression e2)
        {
            // TODO: provide a faster implementation
            return this.CheckIfLessThan(e1, e2);
        }
        public FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Expression e1, Expression e2)
        {
            return this.CheckIfLessEqualThan(e1, e2);
        }

        public FlatAbstractDomain<bool> CheckIfEqual(Expression e1, Expression e2)
        {
            FlatAbstractDomain<bool> c1, c2;
            if ((c1 = CheckIfLessEqualThan(e1, e2)).IsNormal() && (c2 = CheckIfLessEqualThan(e2, e1)).IsNormal())
            {
                return new FlatAbstractDomain<bool>(c1.BoxedElement && c2.BoxedElement);
            }

            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfEqualIncomplete(Expression e1, Expression e2)
        {
            return this.CheckIfEqual(e1, e2);
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan(Variable v1, Variable v2)
        {
            return this.CheckIfLessEqualThan(expManager.Encoder.VariableFor(v1), expManager.Encoder.VariableFor(v2));
        }

        /// <summary>
        /// Check if <code>e != 0</code>
        /// </summary>
        public FlatAbstractDomain<bool> CheckIfNonZero(Expression e)
        {
            // We need to make a clone of this to make it simpler
            OctagonEnvironment<Variable, Expression> duplicate = this.Duplicate();
            duplicate.DoClosure();

            if (duplicate.IsBottom)
            {
                return CheckOutcome.Bottom;
            }
            else
            {
                return duplicate.ToIntervalEnvironment().CheckIfNonZero(e);
            }
        }
        #endregion

        #region INumericalAbstractDomain<int,Expression> Members

        public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
        {
            this.CheckConsistency();

            if (this.IsBottom)
            {
                return;
            }

            var tmp = new OctagonEnvironment<Variable, Expression>(this);

            var rewrittenVariables = new Set<Variable>();

            tmp.CheckConsistency();

            foreach (var x in this.Variables)
            {
                if (!sourcesToTargets.ContainsKey(x))
                {
                    if (rewrittenVariables.Contains(x))
                    {
                        continue;
                    }

                    tmp.RemoveVariable(x);

                    tmp.CheckConsistency();

                    continue;
                }

                // Chose the canonical element
                var target = (sourcesToTargets[x].Length() > 1 && x.Equals(sourcesToTargets[x].Head))
                  ? sourcesToTargets[x].Tail.Head
                  : sourcesToTargets[x].Head;

                if (x.Equals(target))
                {
                    // If it is the identity there is nothing to do...
                    continue;
                }

                if (tmp.variables2dimensions.ContainsKey(target))
                {
                    tmp.RemoveVariable(target);

                    rewrittenVariables.Add(target);

                    tmp.CheckConsistency();
                }

                int dimX = tmp.variables2dimensions[x];

                tmp.variables2dimensions.Remove(x);

                tmp.variables2dimensions[target] = dimX;

                this.CheckConsistency();
                tmp.CheckConsistency();
            }

            // Add the equalities between the same source
            foreach (var source in sourcesToTargets.Keys)
            {
                if (sourcesToTargets[source].Length() <= 1)
                {
                    continue;
                }

                var target = source.Equals(sourcesToTargets[source].Head)
                  ? sourcesToTargets[source].Tail.Head
                  : sourcesToTargets[source].Head;

                foreach (var otherTarget in sourcesToTargets[source].GetEnumerable())
                {
                    tmp.CheckConsistency();
                    tmp = tmp.TestTrueEqual(convert(target), convert(otherTarget));
                    tmp.CheckConsistency();
                }
            }

            tmp.CheckConsistency();

            // We change the state of this, using tmp
            CloneFromThis(tmp);

            this.CheckConsistency();
        }

        public Set<Expression> EqualsTo(Expression x)
        {
            var result = new Set<Expression>();

            if (expManager.Encoder == null)
            {
                return result;
            }

            var xVar = expManager.Decoder.UnderlyingVariable(x);

            int varDim;
            if (variables2dimensions.TryGetValue(xVar, out varDim))
            {
                // 0. clone it before closing

                var dup = this.Duplicate();
                dup.DoClosure();

                // 1. try to get a constant
                var intvX = dup.BoundsFor(x);
                Rational val;
                if (intvX.TryGetSingletonValue(out val))
                {
                    //var eq = this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Equal, x, val.ToExpression(this.encoder));
                    var eq = val.ToExpression(expManager.Encoder);
                    result.Add(eq);
                }

                // 2. try to get a relation betwee x and y
                foreach (var y in dup.variables2dimensions.Keys)
                {
                    if (x.Equals(y))
                    {
                        continue;
                    }

                    var dimY = dup.variables2dimensions[y];

                    // 2.1 try x - y == k
                    Rational r1, r2;
                    var b1 = dup.TryConstantForXMinusY(xVar, y, out r1);    // x - y <= r1
                    var b2 = dup.TryConstantForXMinusY(y, xVar, out r2);   // y - x <= r2 is -(x - y) <= r2 is -r2 <= (x - y) 

                    if (b1 && b2 && r1 == -r2)
                    {
                        var eq = expManager.Encoder.CompoundExpressionFor(ExpressionType.Int32,
                          ExpressionOperator.Addition, expManager.Encoder.VariableFor(y), r1.ToExpression(expManager.Encoder));

                        result.Add(eq);
                    }

                    // 2.2 try x + y = k

                    b1 = dup.TryConstantForXY(xVar, y, out r1);              // x + y <= r1
                    b2 = dup.TryConstantForMinusXMinusY(xVar, y, out r2);    // -(x +y) <= r2 is -r2 <= x + y
                    if (b1 && b2 && r1 == -r2)
                    {
                        var eq = expManager.Encoder.CompoundExpressionFor(ExpressionType.Int32,
                          ExpressionOperator.Subtraction, r1.ToExpression(expManager.Encoder), expManager.Encoder.VariableFor(y));

                        result.Add(eq);
                    }
                }
            }

            return result;
        }

        public List<Pair<Variable, Int32>> IntConstants
        {
            get
            {
                // TODO
                return new List<Pair<Variable, Int32>>();
            }
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        public IEnumerable<Expression> LowerBoundsFor(Expression v, bool strict)
        {
            // TODO
            return new Set<Expression>(); // not implemented
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        public IEnumerable<Expression> UpperBoundsFor(Expression v, bool strict)
        {
            // TODO
            return new Set<Expression>(); // not implemented
        }


        /// <summary>
        /// Not implemented
        /// </summary>
        public IEnumerable<Expression> LowerBoundsFor(Variable v, bool strict)
        {
            // TODO
            return new Set<Expression>(); // not implemented
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        public IEnumerable<Expression> UpperBoundsFor(Variable v, bool strict)
        {
            // TODO
            return new Set<Expression>(); // not implemented
        }

        public IEnumerable<Variable> EqualitiesFor(Variable v)
        {
            // TODO
            return new Set<Variable>();
        }
        #endregion

        #region Floating point types

        public void SetFloatType(Variable v, ConcreteFloat f)
        {
            // does nothing
        }

        public FlatAbstractDomain<ConcreteFloat> GetFloatType(Variable v)
        {
            return FloatTypes<Variable, Expression>.Unknown;
        }

        #endregion
    }

    // public class ClosedOctagonEnvironment<Expression>
}
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// The file contains the constraints that are understood by the CoreOctagon abstract domain

using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.AbstractDomains.Numerical
{
    ///<summary>
    /// The common superclass for octagon constraints.
    /// An CoreOctagon constraint is in the form s * X + s' * Y &lt; C, where s, s' \in { +, -, 0 }, X, Y are variables and C is an integer constant 
    /// </summary>
    public abstract class OctagonConstraint
    {
        #region Private fields: The X, Y, C in the octagonal constraint
        protected int x;
        public int X
        {
            get
            {
                return this.x;
            }
        }

        protected int y;
        public int Y
        {
            get
            {
                return this.y;
            }
        }

        protected Rational c;
        public Rational C
        {
            get
            {
                return this.c;
            }
        }
        #endregion

        /// <summary>
        /// Obtain an octagon constraint equivalent to <code>a.x \leq k</code>
        /// </summary>
        static public OctagonConstraint For(int a, int x, int k)
        {
            if (a == 1)
            { //  x <= k
                return new OctagonConstraintX(x, k);
            }
            else if (a == -1)
            { // -x <= k
                return new OctagonConstraintMinusX(x, k);
            }
            else
            {
                //^ assert false;
                Debug.Assert(false, "I do not know how to handle this constraints");

                return null;
            }
        }

        static public OctagonConstraint For(int a, int x, Rational k)
        {
            return OctagonConstraint.For(a, x, (Int32)k);
        }

        /// <summary>
        /// Obtain an octagon constraint equivalent to <code>a.x + b.y \leq k</code>
        /// </summary>
        static public OctagonConstraint For(int a, int x, int b, int y, int k)
        {
            if (a == 1 && b == 1)
            {   // x + y <= k 
                return new OctagonConstraintXY(x, y, k);
            }
            else if (a == 1 && b == -1)
            {   // x - y <= k
                return new OctagonConstraintXMinusY(x, y, k);
            }
            else if (a == -1 && b == 1)
            {   // -x + y <= k
                return new OctagonConstraintMinusXY(x, y, k);
            }
            else if (a == -1 && b == -1)
            {   // -x - y <= k
                return new OctagonConstraintMinusXMinusY(x, y, k);
            }
            else if (a == 0)
            {   // y <= k or -y <= k
                return For(b, y, k);
            }
            else if (b == 0)
            {   // x <= k or -x <= k
                return For(a, x, k);
            }
            else
            {
                //^ assert false;
                Debug.Assert(false, " I do not know how to handle this constraints");

                return null;
            }
        }

        static public OctagonConstraint For(int a, int x, int b, int y, Rational k)
        {
            return OctagonConstraint.For(a, x, b, y, (Int32)k);
        }

        ///<summary>
        /// Add this constraint to the octagon passed as a parameter
        ///</summary>
        ///<param name="oct">The octagon where to add the constraints</param>
        ///<returns>True iff the constraint has been added</returns>
        public bool AddToOctagon(CoreOctagon oct)
        {
            return this.AddToOctagon(oct, new Set<int>());
        }

        ///<summary>
        /// Add this constraint to the octagon passed as parameter
        /// </summary>
        ///<param name="oct">the octagon where to add the constraint</param>
        ///<param name="affectedDimensions">The dimensions affected by the constraints</param>
        ///<returns>true iff the constraint has been added</returns>
        abstract public bool AddToOctagon(CoreOctagon oct, IMutableSet<int> affectedDimensions);

        ///<summary>
        /// Create a set of octagonal constraints from a given interval, and an integer that stands for the dimension to constraint
        ///</summary>
        static public ICollection<OctagonConstraint> CreateConstraintsFromInterval(int dim, Interval interval)
        {
            ICollection<OctagonConstraint> constraints = new List<OctagonConstraint>();

            if (interval.IsBottom)
                return constraints;

            if (interval.LowerBound == interval.UpperBound)
            {       // it is 'dim = val'
                OctagonConstraint con = new OctagonConstraintXEqualConst(dim, interval.LowerBound);
                constraints.Add(con);
            }

            // Do the lower bound
            if (!interval.LowerBound.IsInfinity)
            {
                OctagonConstraint con = new OctagonConstraintMinusX(dim, -interval.LowerBound);
                constraints.Add(con);
            }

            // Do the upper bound
            if (!interval.UpperBound.IsInfinity)
            {
                OctagonConstraint con = new OctagonConstraintX(dim, interval.UpperBound);
                constraints.Add(con);
            }

            return constraints;
        }

        #region "Utility" methods
        protected int Matpos2(int i, int j)
        {
            return j > i ? Matpos(j ^ 1, i ^ 1) : Matpos(i, j);
        }

        private int Matpos(int i, int j)
        {
            return (j + ((i + 1) * (i + 1)) / 2);
        }

        #endregion
    }

    ///<summary>
    /// A constraint in the form of <code>x + y &lt;= c</code>
    ///</summary>
    public class OctagonConstraintXY : OctagonConstraint
    {
        ///<summary>
        ///Constructs a constraint x + y &lt;= c
        ///</summary>
        public OctagonConstraintXY(int x, int y, Rational c)
        {
            this.x = x;
            this.y = y;
            this.c = c;
        }

        public OctagonConstraintXY(int x, int y, Int32 c)
          : this(x, y, Rational.For(c))
        {
        }
        ///<summary>
        /// Add this constraint to the octagon <code>oct</code>
        ///</summary>
        ///<param name="affectedDimensions">The dimensions "affected" by this constraint</param>
        ///<returns>True if the octagon is modified</returns>
        override public bool AddToOctagon(CoreOctagon oct, IMutableSet<int> affectedDimensions)
        {
            int j = 2 * this.x;
            int i = 2 * this.y + 1;

            affectedDimensions.Add(this.x);
            affectedDimensions.Add(this.y);

            if (this.c <= oct.octagon[Matpos2(i, j)])
            {
                oct.octagon[Matpos2(i, j)] = this.c;
                return true;
            }
            return false;
        }

        //^ [Confined]
        override public string/*!*/ ToString()
        {
            return "v" + this.x + " + v" + this.y + " <= " + this.c + Environment.NewLine;
        }
    }

    ///<summary>
    /// A constraint in the form of x - y &lt;= c
    ///</summary>
    public class OctagonConstraintXMinusY : OctagonConstraint
    {
        ///<summary>
        /// Constructs a constraint <code>x - y &lt;= c </code>
        ///</summary>
        public OctagonConstraintXMinusY(int x, int y, Rational c)
        {
            this.x = x;
            this.y = y;
            this.c = c;
        }
        public OctagonConstraintXMinusY(int x, int y, Int32 c)
          : this(x, y, Rational.For(c))
        {
        }

        override public bool AddToOctagon(CoreOctagon oct, IMutableSet<int> affectedDimensions)
        {
            int j = 2 * this.x;
            int i = 2 * this.y;

            affectedDimensions.Add(this.x);
            affectedDimensions.Add(this.y);

            if (this.c <= oct.octagon[Matpos2(i, j)])
            {
                oct.octagon[Matpos2(i, j)] = this.c;
                return true;
            }
            return false;
        }

        //^ [Confined]
        override public string/*!*/ ToString()
        {
            return "v" + this.x + " - v" + this.y + " <= " + this.c + Environment.NewLine;
        }
    }

    /// <summary>
    /// A constraint in the form of -x + y &lt;= c, with c <code>int</code> constant
    /// </summary>
    public class OctagonConstraintMinusXY : OctagonConstraint
    {
        ///<summary>
        ///Constructs a constraint -x + y &lt;= c
        ///</summary>
        public OctagonConstraintMinusXY(int x, int y, Rational c)
        {
            this.x = x;
            this.y = y;
            this.c = c;
        }

        public OctagonConstraintMinusXY(int x, int y, Int32 c)
      : this(x, y, Rational.For(c))
        {
        }

        override public bool AddToOctagon(CoreOctagon oct, IMutableSet<int> affectedDimensions)
        {
            int j = 2 * this.x + 1;
            int i = 2 * this.y + 1;

            affectedDimensions.Add(this.x);
            affectedDimensions.Add(this.y);

            if (this.c <= oct.octagon[Matpos2(i, j)])
            {
                oct.octagon[Matpos2(i, j)] = this.c;
                return true;
            }
            return false;
        }

        //^ [Confined]
        override public string/*!*/ ToString()
        {
            return "-v" + this.x + " + v" + this.y + " <= " + this.c + Environment.NewLine;
        }
    }

    /// <summary>
    /// A constraint in  the form -x - y &lt;= c
    /// </summary>
    public class OctagonConstraintMinusXMinusY : OctagonConstraint
    {
        public OctagonConstraintMinusXMinusY(int x, int y, Rational c)
        {
            this.x = x;
            this.y = y;
            this.c = c;
        }

        public OctagonConstraintMinusXMinusY(int x, int y, Int32 c)
          : this(x, y, Rational.For(c))
        {
        }

        override public bool AddToOctagon(CoreOctagon oct, IMutableSet<int> affectedDimensions)
        {
            int j = 2 * this.x + 1;
            int i = 2 * this.y;

            affectedDimensions.Add(this.x);
            affectedDimensions.Add(this.y);

            if (this.c <= oct.octagon[Matpos2(i, j)])
            {
                oct.octagon[Matpos2(i, j)] = this.c;
                return true;
            }
            return false;
        }

        //^ [Confined]
        override public string/*!*/ ToString()
        {
            return "-v" + this.x + " - v" + this.y + " <= " + this.c + Environment.NewLine;
        }
    }

    ///<summary>
    /// A constraint in the form of x == y
    ///</summary>
    public class OctagonConstraintXEqualY : OctagonConstraint
    {
        private OctagonConstraint cachedXLeqY;
        private OctagonConstraint cachedYLeqX;

        /// <summary>
        /// Creates the octagonal constraint x = y
        /// </summary>
        public OctagonConstraintXEqualY(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.c = Rational.For(0);

            cachedXLeqY = new OctagonConstraintXMinusY(this.x, this.y, 0); // x <= y
            cachedYLeqX = new OctagonConstraintXMinusY(this.y, this.x, 0);  // y <= x
        }

        override public bool AddToOctagon(CoreOctagon oct, IMutableSet<int> affectedDimensions)
        {
            bool b1 = cachedXLeqY.AddToOctagon(oct, affectedDimensions);
            bool b2 = cachedYLeqX.AddToOctagon(oct, affectedDimensions);

            return b1 || b2;
        }

        //^ [Confined]
        override public string/*!*/ ToString()
        {
            return "v" + this.x + " == v" + this.y;
        }
    }

    /// <summary>
    /// A constraint in the form of x == c, with c <code>int</code> constant
    /// </summary>
    public class OctagonConstraintXEqualConst : OctagonConstraint
    {
        protected OctagonConstraint inf;
        protected OctagonConstraint sup;
        protected IMutableSet<OctagonConstraint> toAdd;

        ///<summary>
        /// Construct an object for the equality x == c
        ///</summary>
        public OctagonConstraintXEqualConst(int i, Rational c)
        {
            this.x = i;
            this.y = -1;
            this.c = c;

            this.inf = new OctagonConstraintMinusX(i, -this.c);
            this.sup = new OctagonConstraintX(i, this.c);

            this.toAdd = new Set<OctagonConstraint>();

            this.toAdd.Add(this.inf);
            this.toAdd.Add(this.sup);
        }

        override public bool AddToOctagon(CoreOctagon oct, IMutableSet<int> affectedDimensions)
        {
            bool bInf = this.inf.AddToOctagon(oct, affectedDimensions);
            bool bSup = this.sup.AddToOctagon(oct, affectedDimensions);

            return bInf || bSup;
        }

        //^ [Confined]
        override public string/*!*/ ToString()
        {
            return "x" + this.x + " = " + this.c;
        }
    }

    /// <summary>
    /// A constraint in the form of x &lt;= c, with c <code>int</code> constant
    /// </summary>
    public class OctagonConstraintX : OctagonConstraint
    {
        public OctagonConstraintX(int x, Rational c)
        {
            this.x = x;
            this.y = -12;
            this.c = c;
        }

        public OctagonConstraintX(int x, Int32 c)
          : this(x, Rational.For(c))
        {
        }

        override public bool AddToOctagon(CoreOctagon oct, IMutableSet<int> affectedDimensions)
        {
            int j = 2 * this.x;
            int i = 2 * this.x + 1;

            try
            {
                Rational c = this.c * 2;

                affectedDimensions.Add(this.x);

                if (c <= oct.octagon[Matpos2(i, j)])
                {
                    oct.octagon[Matpos2(i, j)] = c;
                    return true;
                }
            }
            catch (ArithmeticExceptionRational)
            {
                // We could not add the constraint because of an arithmetic exception

                return false;
            }

            return false;
        }

        //^ [Confined]
        override public string/*!*/ ToString()
        {
            return "v" + this.x + " <= " + this.c + Environment.NewLine;
        }
    }

    ///<summary>
    ///A constraint of the form of -x &lt;= c, with x a variable and <code>c</code> an integer
    ///</summary>
    public class OctagonConstraintMinusX : OctagonConstraint
    {
        public OctagonConstraintMinusX(int x, Rational c)
        {
            this.x = x;
            this.y = -1;
            this.c = c;
        }

        public OctagonConstraintMinusX(int x, Int32 c)
          : this(x, Rational.For(c))
        {
        }

        ///<summary>
        /// Add this constraint to the octagon
        ///</summary>
        override public bool AddToOctagon(CoreOctagon oct, IMutableSet<int> affectedDimensions)
        {
            int j = 2 * this.x + 1;
            int i = 2 * this.x;

            affectedDimensions.Add(this.x);

            try
            {
                Rational c = this.c * 2;

                if (Rational.Min(c, oct.octagon[Matpos2(i, j)]) == c)
                {
                    oct.octagon[Matpos2(i, j)] = c;
                    return true;
                }
            }
            catch (ArithmeticExceptionRational)
            {
                // We could not add the constraint because of an arithmetic exception

                return false;
            }

            return false;
        }

        //^ [Confined]
        override public string/*!*/ ToString()
        {
            return "-v" + this.x + " <= " + this.c + Environment.NewLine;
        }
    }
}
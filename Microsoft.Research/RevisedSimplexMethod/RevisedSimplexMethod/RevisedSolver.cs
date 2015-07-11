// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Microsoft.Glee.Optimization
{
    /// <summary>
    /// Following "Linear Programming", Vasek Chvatal.  Solves the linear program Ax=b,  maximize costs*x, lowBound \leq x \leq upperBounds. 
    /// The last inequalities hold only for non-free variables
    /// </summary>
    internal class RevisedSolver
    {
        #region Object Invariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_constraints != null);
            Contract.Invariant(y.Length == A.NumberOfRows);
            Contract.Invariant(y.Length > 0);
            Contract.Invariant(_refactorizationPeriod != 0); // F: should be inferred
            Contract.Invariant(_pivotEpsilon > 0.0);
        }

        #endregion

        //double det = 1;
        internal UMatrix U; //the U matrix of the factorization
        readonly internal int[] forbiddenPairs;
        private readonly double _epsilon = 10.0e-8;
        private readonly double _pivotEpsilon = 10.0e-8;
        private readonly double _etaEpsilon = 1.0e-6;
        internal const int notInBasis = -1;
        internal const int forgottenVar = -2;
        private const int freeVar = -3;
        private const double infinity = Double.MaxValue;

        /// <summary>
        /// the basis array
        /// </summary>
        internal int[] basis;

        readonly internal int[] index;

        private int _iterations;
        private readonly int _refactorizationPeriod = 20;
        internal ExtendedConstraintMatrix A;
        private readonly int _nVars;
        internal double[] y; //the array to keep right sides of the systems and their solutions
                             //Length of y should be equal to the number of columns in A
        private Factorization _factorization;
        private readonly List<Constraint> _constraints;

        internal Factorization Factorization
        {
            get { return _factorization; }
            set { _factorization = value; }
        }

        internal void ForgetVariable(int i)
        {
            if (i < index.Length)
                index[i] = forgottenVar;
        }

        internal void ReplaceInBasis(int entering, int leaving)
        {
            Contract.Requires(leaving >= 0);
            Contract.Assume(index[leaving] >= 0);   // F: Need quantified invariants
            Contract.Assume(index[entering] == notInBasis); // F: Need quantified invariants


            int k = index[leaving];
            Contract.Assume(basis[k] == leaving); // F: Need ForAll

            basis[k] = entering;
            index[entering] = k;
            index[leaving] = notInBasis;
        }

        /// <summary>
        /// We have basis[index[i]]=i for i belonging to the basis
        /// </summary>
        internal IEnumerable<int> NBasis()
        {
            //take care here of free variables
            for (int i = 0; i < _nVars; i++)
                if (index[i] == notInBasis && NotForbidden(i))
                    yield return i;
        }

        [Pure]
        internal IEnumerable<int> NBasis(int start)
        {
            //take care here of free variables
            for (int i = start; i < _nVars; i++)
                if (index[i] == notInBasis && NotForbidden(i))
                    yield return i;
        }

        private bool NotForbidden(int i)
        {
            return
                forbiddenPairs == null
                ||
                i >= forbiddenPairs.Length
                ||
                index[forbiddenPairs[i]] < 0; //the pair of i is not in the basis
        }


        private readonly double[] _costs;

        internal double[] Costs
        {
            get { return _costs; }
            //            set { costs = value; }
        }

        private readonly int _artificialsStart;
        private Status _status = Status.Unknown;

        private readonly bool[] _lowBoundIsSet;
        private readonly double[] _lowBounds;

        private readonly bool[] _upperBoundIsSet;
        private readonly double[] _upperBounds;


        internal Status Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private int BasisSize
        {
            get { return A.NumberOfRows; }
        }


        private readonly double[] _xStar; //a feasible solution, first time is set in the constructor

        internal double[] XStar
        {
            get { return _xStar; }
            //            set { xStar = value; }
        }

        internal RevisedSolver(int[] basisArray,
                               double[] xSt,
                               int startOfArtificials,
                               ExtendedConstraintMatrix APar,
                               double[] costsPar,
                               bool[] lowBounds,
                               double[] lowBoundValues,
                               bool[] upperBounds,
                               double[] upperBoundValues,
                               int[] forbiddenPairsPar,
                               List<Constraint> constrs,
                               double etaEps
            )
        {
            _etaEpsilon = etaEps;
            _constraints = constrs;
            forbiddenPairs = forbiddenPairsPar;
            basis = basisArray;
            _xStar = xSt;
            _artificialsStart = startOfArtificials;
            _nVars = APar.NumberOfColumns;
            A = APar;
            _costs = costsPar;
            y = new double[BasisSize];
            index = new int[_nVars];
            for (int i = 0; i < _nVars; i++)
                index[i] = notInBasis;
            for (int i = 0; i < basis.Length; i++)
                index[basis[i]] = i; //that is the i-th element of basis

            _lowBoundIsSet = lowBounds;
            _lowBounds = lowBoundValues;
            _upperBoundIsSet = upperBounds;
            _upperBounds = upperBoundValues;

            U = new UMatrix(basis.Length);
        }

#if DEBUGGLEE
        double CurrentCost{
            get{
                double r=0;
                for(int i=0;i<costs.Length;i++){
                    r+=this.xStar[i]*this.costs[i];
                }
                return r;
                    
            }
        }
#endif

        [ContractVerification(false)]
        internal void Solve()
        {
            if (_constraints.Count == 0)
            {
                Status = Status.Optimal;
                return;
            }
            _iterations = 0;

            Contract.Assert(y.Length > 0);

            var yCopy = new double[y.Length];
            do
            {
                if (_iterations++ % _refactorizationPeriod == 0 || _factorization == null)
                {
                    var f = Factorization.CreateFactorization(new BMatrix(basis, A), U);
                    if (f != null)
                        _factorization = f;
                    else
                    {
                        _status = Status.FloatingPointError;
                        return;
                    }
                }
                bool leavingIsFound = false;
                FillCostsB(y);
                _factorization.Solve_yBEquals_cB(y);


                Contract.Assume(0 <= yCopy.Length - ((IList<double>)y).Count);
                y.CopyTo(yCopy, 0);
                int startLookingForEnteringFrom = 0;
                do
                {
                    bool enteringHasToGrow;
                    var entering = ChooseEnteringVariable(y, out enteringHasToGrow, startLookingForEnteringFrom);
                    if (entering == -1)
                    {
                        _status = Status.Optimal;
                        return; //! returning from the middle of the loop in a good mood !
                    }

                    A.FillColumn(entering, y);
                    _factorization.Solve_BdEqualsa(y);

                    var enteringT = BoundOnEnteringVar(entering, enteringHasToGrow);
                    //the value of the entering variable
                    var t = enteringT;
                    var leaving = FindLeavingVariableAndT(y, ref t, enteringHasToGrow);
                    if (leaving == -1 && t == infinity)
                    {
                        _status = Status.Unbounded;
                        //increasing the entering variable is always feasible, and the product costs*x grows infinitely
                        return; //again returning from the middle of the loop
                    }

                    if (leaving == -1 || Math.Abs(y[index[leaving]]) > _etaEpsilon)
                    {
                        //check that the diagonal element in the eta-matrix is big enough 
                        leavingIsFound = true;
                        FindNewXStarAndFactorization(leaving, entering, enteringHasToGrow ? t : -t, y, t < enteringT);
                        if (leaving >= _artificialsStart)
                        {
                            Contract.Assume(_artificialsStart >= 0);
                            ForgetVariable(leaving);
                        }
                    }
                    else
                    {
                        startLookingForEnteringFrom = entering + 1;
                        //restore y
                        yCopy.CopyTo(y, 0);
                    }
                } while (!leavingIsFound);
                //  if (StandardFactorization.calls >= 680) {
                //    ((StandardFactorization)factorization).CheckFactorization(new BMatrix(this.basis, A));
                // }
            } while (_status != Status.Unbounded && _status != Status.Optimal);
        }

#if DEBUGGLEE
        private void CheckCorrectness() {
            for (int i = 0; i < this.y.Length; i++)
                y[i] = this.xStar[basis[i]];

            BMatrix B = new BMatrix(basis, A);
           Vector rs = (Vector)(B * (new Vector(y)));

            double err = 0;
            for (int i = 0; i < y.Length; i++) {
                double d = Math.Abs(this.constraints[i].rightSide - rs[i]);
                if (d > err)
                    err = d;
            }

            if (err > this.pivotEpsilon)
                Console.WriteLine("error is big {0}", err);
        }

#endif

        private double BoundOnEnteringVar(int enteringVariable, bool enteringHasToGrow)
        {
            if (enteringHasToGrow)
            {
                if (_upperBoundIsSet[enteringVariable])
                    return _upperBounds[enteringVariable] - _xStar[enteringVariable];

                return Double.MaxValue;
            }
            // enteringHasToGrow==false
            if (_lowBoundIsSet[enteringVariable])
                return _xStar[enteringVariable] - _lowBounds[enteringVariable];

            return Double.MaxValue;
        }


        private void FindNewXStarAndFactorization(int leavingVariable, int enteringVariable, double t, double[] d,
                                          bool changeBasis)
        {
            Contract.Requires(leavingVariable >= -1);

            _xStar[enteringVariable] += t;
            int basisSize = BasisSize;
            for (int i = 0; i < basisSize; i++)
            {
                var b = basis[i];
                Contract.Assume(b >= 0);
                Contract.Assume(b < XStar.Length);
                XStar[b] -= d[i] * t;
            }
            if (changeBasis)
            {
                Contract.Assume(leavingVariable >= 0);
                ReplaceInBasis(enteringVariable, leavingVariable);
                int etaIndex = index[enteringVariable];
                var clonedD = d.Clone() as double[];
                Contract.Assume(clonedD != null);
                var e = new EtaMatrix(etaIndex, clonedD);
                ////debug
                //det *= d[etaIndex];
                //Console.WriteLine("det={0}", det);
                ////end debug
                _factorization.AddEtaMatrix(e);
            }
        }


        private void FillCostsB(double[] cb)
        {
            int i = 0;

            foreach (int j in basis)
            {
                Contract.Assume(i < cb.Length);
                cb[i++] = Costs[j];
            }
        }

        //private Vector yB0EqualCb() {
        //    throw new Exception("The method or operation is not implemented.");
        //}

        private readonly double _zeroTolerance = 10.0e-5;
        //double accuracyEpsilon = 1.0e-6; //used to compare A*xStar with b
        /// <summary>
        /// later provide more intelligent implementation
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        private unsafe int ChooseEnteringVariable(double[] yp, out bool enteringHasToGrow, int startLookingFrom)
        {
            Contract.Requires(yp != null);

            /* the safe version 
            int k = 1;
            int ret = -1;
            enteringHasToGrow = false;// have to make this assignment because of the compiler
            double maxCost = zeroTolerance;
            foreach (int i in NBasis(startLookingFrom)) {
                if (i >= startLookingFrom) {
                    double t = this.Costs[i] - dotWithColumn(y, A, i);
                    if (t > maxCost && VarCanGrow(i)) {
                        enteringHasToGrow = true;
                        ret = i;
                        maxCost = t;
                        k--;
                        if (k == 0)
                            break;
                    } else if (t < -maxCost && VarCanShrink(i)) {
                        enteringHasToGrow = false;
                        ret = i;
                        maxCost = -t;
                        k--;
                        if (k == 0)
                            break;
                    }
                }
            }

            return ret;
             */
            int ret = -1;
            enteringHasToGrow = false; // have to make this assignment because of the compiler
            double maxCost = _zeroTolerance;
            fixed (double* costsPin = _costs)
            {
                foreach (int i in NBasis(startLookingFrom))
                {
                    if (i >= startLookingFrom)
                    {
                        double t = *(costsPin + i) - A.DotWithColumn(yp, i);
                        if (t > maxCost && VarCanGrow(i))
                        {
                            enteringHasToGrow = true;
                            ret = i;
                            maxCost = t;
                        }
                        else if (t < -maxCost && VarCanShrink(i))
                        {
                            enteringHasToGrow = false;
                            ret = i;
                            maxCost = -t;
                            break;
                        }
                    }
                }
            }

            return ret;
        }

        [Pure]
        private bool VarCanShrink(int i)
        {
            Contract.Requires(i >= 0);
            return _lowBoundIsSet[i] == false || _xStar[i] > _lowBounds[i] + _epsilon;
        }

        [Pure]
        private bool VarCanGrow(int i)
        {
            Contract.Requires(i >= 0);
            return _upperBoundIsSet[i] == false || _xStar[i] < _upperBounds[i] - _epsilon;
        }

        //static double dotWithColumn(double[] y, Matrix A, int j) {
        //    ExtendedConstraintMatrix exConstraintMatrix = A as ExtendedConstraintMatrix;
        //    if (exConstraintMatrix != null) {
        //        return exConstraintMatrix.DotWithColumn(y, j);
        //    }
        //    {
        //        double r = 0;
        //        for (int i = 0; i < y.Length; i++)
        //            r += y[i] * A[i, j];
        //        return r;
        //    }
        //}


        /// <summary>
        /// If i is returned then basis[i] is going away from the basis, except for the case when j=basis[i] just jumps to from on end of its domain to the another
        /// </summary>
        /// <param name="d"></param>
        /// <param name="leavingVariable"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private int FindLeavingVariableAndT(double[] d, ref double t, bool usePositiveT)
        {
            Contract.Requires(d != null);
            Contract.Ensures(Contract.Result<int>() >= -1);

            int ret = -1;
            for (int i = 0; i < d.Length; i++)
            {
                int j = basis[i]; //basis var
                Contract.Assume(j >= 0);
                double p = BoundOnVariableJ(j, usePositiveT ? -d[i] : d[i]);
                if (p < t)
                {
                    t = p;
                    ret = j;
                }
            }
            return ret;
        }

        /// <summary>
        /// finds max t such xStar[j]+p*t stands in its basis
        /// </summary>
        /// <param name="j"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private double BoundOnVariableJ(int j, double p)
        {
            Contract.Requires(0 <= j);
            if (p > _pivotEpsilon)
            {
                if (_upperBoundIsSet[j])
                    return (_upperBounds[j] - _xStar[j]) / p;
                return infinity;
            }

            if (p < -_pivotEpsilon)
            {
                if (_lowBoundIsSet[j])
                    return (_lowBounds[j] - _xStar[j]) / p;
            }
            return infinity;
        }
    }
}
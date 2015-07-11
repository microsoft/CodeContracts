// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics.Contracts;

/*
Follows "Introduction to Operations Research",Hillier Liberman, eight edition,
 chapter "Quadratic Programming"
 */

namespace Microsoft.Glee.Optimization
{
    /// <summary>
    /// Solves: maximize c*x-0.5xt*Q*x; where xt is transpose of x
    /// and Q is a symmetric matrix, Q is also positive semidefinite: that is xt*Qx*x>=0 for any x>=0.
    /// Unknown x is not-negative and is a subject to linear constraints added by AddConstraint method.
    /// </summary>
    public class QP : IQuadraticProgram
    {
        #region Object Invariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_Q != null);
            Contract.Invariant(_lp != null);
            Contract.Invariant(_n >= 0);
            Contract.Invariant(_m >= 0);
            Contract.Invariant(_A.Count == _b.Count);
        }
        #endregion

        private readonly List<double[]> _A = new List<double[]>();
        private readonly List<double> _b = new List<double>();
        private readonly Dictionary<IntPair, double> _Q = new Dictionary<IntPair, double>();
        private double[] _c;
        private double[] _solution;
        private int _m; //dimensions
        private int _n;

        private Status _status = Status.Infeasible;

        private readonly LinearProgramInterface _lp = LpFactory.CreateLP();


        /// <summary>
        /// add a linear constraint in the form coeffs*x relation rightSide
        /// </summary>
        /// <param name="coeffs"></param>
        /// <param name="relation">relation can be only "less or equal" or 
        /// "greater or equal"</param>
        /// <param name="rightSide"></param>
        public void AddConstraint(double[] coeffs, Relation relation, double rightSide)
        {
            if (coeffs == null || relation == Relation.Equal)
                throw new InvalidDataException();

            coeffs = (double[])coeffs.Clone();
            //we need to bring every relation to "less or equal"
            if (relation == Relation.GreaterOrEqual)
            {
                for (int i = 0; i < coeffs.Length; i++)
                    coeffs[i] *= -1;

                rightSide = -rightSide;
            }

            _A.Add(coeffs);
            _b.Add(rightSide);
        }

        /// <summary>
        /// sets i,j element of Q
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="qij"></param>
        public void SetQMember(int i, int j, double qij)
        {
            //use here the fact that the matrix is symmetric
            if (i <= j)
                _Q[new IntPair(i, j)] = qij;
            else
                _Q[new IntPair(j, i)] = qij;

            if (i + 1 > _n)
                _n = i + 1;
            if (j + 1 > _n)
                _n = j + 1;
        }

        /// <summary>
        /// adds d to i,j element of Q
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="qij"></param>
        public void AddToQMember(int i, int j, double d)
        {
            IntPair pair = (i <= j) ? new IntPair(i, j) : new IntPair(j, i);
            double val = 0;
            if (_Q.TryGetValue(pair, out val))
            {
                _Q[pair] = val + d;
            }
            else
                _Q[pair] = d;


            if (i + 1 > _n)
                _n = i + 1;
            if (j + 1 > _n)
                _n = j + 1;
        }


        public void SetLinearCosts(double[] costs)
        {
            if (costs == null)
                throw new InvalidDataException();
            _c = (double[])costs.Clone();
        }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public double[] Solution
        {
            get
            {
                if (_solution != null)
                    return _solution;
                double[] sol = CalculateSolution();
                if (sol == null)
                {
                    return null;
                }
                _solution = new double[_n];
                for (int i = 0; i < _n; i++)
                {
                    _solution[i] = sol[i];
                }
                _status = Status.Optimal;
                return _solution;
            }
        }

        public Status Status
        {
            get { return _status; }
        }

        private double[] CalculateSolution()
        {
            Contract.Ensures(Contract.Result<double[]>() == null || Contract.Result<double[]>().Length == _n);

            //we need to create an LP system which is KKT condtitions 
            //of the quadratic problem.
            //The conditions look like 
            //Qx+Atu-y=ct
            //Ax+v=b
            //So, we have a vector of variables (x,y,u,v).
            //Q is an n by n matrix, x has length n.
            //Matrix A is a m by n: u's length is m
            //y has dimension n, v has length m
            //xy+uv=0

            _m = _A.Count;
            double t;
            var totalVars = 2 * (_n + _m);
            //we put variables in the order x,y,u,v
            //make Qx+Au-y=c. There are n equalities here

            //coefficients before x
            for (var i = 0; i < _n; i++)
            {//creating the i-th equality
                var cf = new double[totalVars];
                for (var j = 0; j < _n; j++)
                {
                    if (TryGetQMember(i, j, out t))
                        cf[j] = t;
                }
                //coefficients before y are all -1, and start from the index n
                cf[i + _n] = -1;
                //coefficients before u start from 2*n

                for (var j = 0; j < _m; j++)
                {
                    cf[2 * _n + j] = _A[j][i];
                }
                if (_c != null)
                {
                    Contract.Assume(i < _c.Length);
                    _lp.AddConstraint(cf, Relation.Equal, _c[i]);
                }
                else
                {
                    _lp.AddConstraint(cf, Relation.Equal, 0);
                }
            }

            for (int i = 0; i < totalVars; i++)
                _lp.LimitVariableFromBelow(i, 0);

            //creating Ax+v=b
            for (int i = 0; i < _m; i++)
            {
                //creating the i-th equality
                var cf = new double[totalVars];
                var AI = _A[i];
                //coefficients before x
                for (int j = 0; j < _n; j++)
                {
                    Contract.Assume(j < AI.Length);
                    cf[j] = AI[j];
                }
                //coefficients before v are all 1
                // they start from 2*n+m
                cf[i + 2 * _n + _m] = 1;
                _lp.AddConstraint(cf, Relation.Equal, _b[i]);
            }
            //the pairs forbidden for complimentary slackness
            var forbiddenPairs = new int[totalVars];
            for (int i = 0; i < _n; i++)
            {
                forbiddenPairs[i] = _n + i; //taking care that x[i]y[i]=0
                forbiddenPairs[_n + i] = i;
            }

            for (int i = 0; i < _m; i++)
            {
                forbiddenPairs[2 * _n + i] = 2 * _n + _m + i;
                forbiddenPairs[2 * _n + _m + i] = 2 * _n + i;
            }

            _lp.ForbiddenPairs = forbiddenPairs;
            _lp.EpsilonForArtificials = Double.MaxValue;//don't care
            _lp.EpsilonForReducedCosts = 0.0001;

            double[] ret = _lp.FeasibleSolution();
            if (_lp.Status == Status.Feasible)
                return ret;

            return null;
        }

        [Pure]
        private bool TryGetQMember(int i, int j, out double t)
        {
            if (i <= j)
                return _Q.TryGetValue(new IntPair(i, j), out t);
            return _Q.TryGetValue(new IntPair(j, i), out t);
        }
    }
}

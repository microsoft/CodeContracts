// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics.Contracts;

/*
  The algorithm is taken from book
"Combinatorial Optimization. Algorithms and Complexity"
  Christos H. Papadimitriou, Kenneth Steigitz
*/

namespace Microsoft.Glee.Optimization
{
    /// <summary>
    /// The problem is to find min c*x under constraint Ax=b,x>=0 where A|b=X is the tableau
    /// Solver solves LP starting with a given feasible solution given by the basis array
    /// a tableau and a cost. 
    /// </summary>

    internal class Solver
    {
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tableau != null);
        }


        private int[] _forbiddenPairs;

        internal int[] ForbiddenPairs
        {
            get { return _forbiddenPairs; }
            set { _forbiddenPairs = value; }
        }
        internal Status status = Status.Unknown;
        readonly internal Microsoft.Glee.Optimization.Tableau tableau = new Microsoft.Glee.Optimization.Tableau();


        private double[] _c;

        internal void SetCosts(double[] costs)
        {
            Contract.Requires(costs != null);

            _c = costs;
            tableau.SetNumberOfVariables(_c.Length);
        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="basis">basis[i] is the var chosen for the constraint i</param>
        /// <param name="X">the tableau, the tableau will be changed in place</param>
        /// <param name="c">cost</param>
        internal Solver(int[] basis, double[] X, double[] x, double[] c, double epsForReducedCosts)
        {
            Contract.Requires(x.Length > 0);

            _reducedCostEps = epsForReducedCosts;
#if DEBUGSIMPLEX
      if(x.Length==0)
        throw new InvalidOperationException();//"wrong lengths");
#endif

            tableau.UpdateMatrix(X, x.Length, X.Length / x.Length);
            tableau.SetSolution(x);

            //	if(X.Length/x.Length!=c.Length)
            //	  throw new System.Exception("wrong lengths");
#if DEBUGSIMPLEX
			for(int i=0;i<basis.Length;i++)
				if(basis[i]>=c.Length||basis[i]<0)
					throw new InvalidOperationException();//"wrong basis");
#endif

            for (int i = 0; i < basis.Length; i++)
            {
                Contract.Assume(basis[i] >= 0);// F: we need quantified invariants here
                tableau.PutVarToBasis(i, basis[i]);
            }

            _c = c;
        }

        private bool CanPutInBasis(int j)
        {
            return ForbiddenPairs == null
              ||
              j >= ForbiddenPairs.Length
              ||
              !tableau.BasisSet[ForbiddenPairs[j]];
        }
        private readonly double _reducedCostEps = 1.0e-8;


        internal void Solve()
        {
            double[] rc = new double[_c.Length];

            while (status == Status.Unknown)
            {
                tableau.CalcReducedCosts(_c, rc);

                double min = Single.MaxValue;

                int j = -1; //the minimal reduced cost
                for (int i = 0; i < rc.Length; i++)
                    if (rc[i] < min)
                    {
                        if (CanPutInBasis(i))
                        {
                            j = i;
                            min = rc[i];
                        }
                    }

                Contract.Assert(j >= -1);

                if (min > -_reducedCostEps)
                { //is it still correct if ForbiddenPairs were found?
                    status = Status.Optimal;
                    break;
                }

                Contract.Assume(j >= 0);    // if j == -1, then min = Single.MaxValue, and hence we will not reach this point

                int row = tableau.ChooseLeavingRow(j);
                if (row == -1)
                {
                    status = Status.Unbounded;
                    break;
                }

                tableau.Pivot(row, j);
            }
        }
    }
}

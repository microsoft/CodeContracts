// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

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


    int[] forbiddenPairs;

    internal int[] ForbiddenPairs
    {
      get { return forbiddenPairs; }
      set { forbiddenPairs = value; }
    }
    internal Status status = Status.Unknown;
    readonly internal Microsoft.Glee.Optimization.Tableau tableau = new Microsoft.Glee.Optimization.Tableau();


    double[] c;

    internal void SetCosts(double[] costs)
    {
      Contract.Requires(costs != null);

      this.c = costs;
      tableau.SetNumberOfVariables(c.Length);
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

      this.reducedCostEps = epsForReducedCosts;
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

      this.c = c;

    }

    private bool CanPutInBasis(int j)
    {
      return ForbiddenPairs == null
        ||
        j >= ForbiddenPairs.Length
        ||
        !tableau.BasisSet[ForbiddenPairs[j]];
    }
    readonly double reducedCostEps = 1.0e-8;


    internal void Solve()
    {

      double[] rc = new double[c.Length];

      while (status == Status.Unknown)
      {
        tableau.CalcReducedCosts(c, rc);

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

        if (min > -reducedCostEps)
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

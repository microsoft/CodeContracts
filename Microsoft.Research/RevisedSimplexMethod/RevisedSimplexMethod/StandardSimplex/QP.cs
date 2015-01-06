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
    void ObjectInvariant()
    {
      Contract.Invariant(Q != null);
      Contract.Invariant(lp != null);
      Contract.Invariant(n >= 0);
      Contract.Invariant(m >= 0);
      Contract.Invariant(A.Count == b.Count);
    }
    #endregion

    readonly List<double[]> A = new List<double[]>();
    readonly List<double> b = new List<double>();
    readonly Dictionary<IntPair, double> Q = new Dictionary<IntPair, double>();
    double[] c;
    double[] solution;
    int m; //dimensions
    int n;

    Status status = Status.Infeasible;

    readonly LinearProgramInterface lp = LpFactory.CreateLP();


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

      A.Add(coeffs);
      b.Add(rightSide);


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
        Q[new IntPair(i, j)] = qij;
      else
        Q[new IntPair(j, i)] = qij;

      if (i + 1 > n)
        n = i + 1;
      if (j + 1 > n)
        n = j + 1;
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
      if (Q.TryGetValue(pair, out val))
      {
        Q[pair] = val + d;
      }
      else
        Q[pair] = d;


      if (i + 1 > n)
        n = i + 1;
      if (j + 1 > n)
        n = j + 1;
    }


    public void SetLinearCosts(double[] costs)
    {
      if (costs == null)
        throw new InvalidDataException();
      this.c = (double[])costs.Clone();
    }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
    public double[] Solution
    {
      get
      {
        if (solution != null)
          return solution;
        double[] sol = CalculateSolution();
        if (sol == null)
        {
          return null;
        }
        solution = new double[n];
        for (int i = 0; i < n; i++)
        {
          solution[i] = sol[i];
        }
        this.status = Status.Optimal;
        return solution;
      }
    }

    public Status Status
    {
      get { return this.status; }
    }

    double[] CalculateSolution()
    {
      Contract.Ensures(Contract.Result<double[]>() == null || Contract.Result<double[]>().Length == this.n);

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

      m = A.Count;
      double t;
      var totalVars = 2 * (n + m);
      //we put variables in the order x,y,u,v
      //make Qx+Au-y=c. There are n equalities here

      //coefficients before x
      for (var i = 0; i < n; i++)
      {//creating the i-th equality
        var cf = new double[totalVars];
        for (var j = 0; j < n; j++)
        {
          if (TryGetQMember(i, j, out t))
            cf[j] = t;
        }
        //coefficients before y are all -1, and start from the index n
        cf[i + n] = -1;
        //coefficients before u start from 2*n

        for (var j = 0; j < m; j++)
        {
          cf[2 * n + j] = A[j][i];
        }
        if (c != null)
        {
          Contract.Assume(i < c.Length);
          lp.AddConstraint(cf, Relation.Equal, this.c[i]);
        }
        else
        {
          lp.AddConstraint(cf, Relation.Equal, 0);
        }
      }

      for (int i = 0; i < totalVars; i++)
        lp.LimitVariableFromBelow(i, 0);

      //creating Ax+v=b
      for (int i = 0; i < m; i++)
      {
        //creating the i-th equality
        var cf = new double[totalVars];
        var AI = A[i];
        //coefficients before x
        for (int j = 0; j < n; j++)
        {
          Contract.Assume(j < AI.Length);
          cf[j] = AI[j];
        }
        //coefficients before v are all 1
        // they start from 2*n+m
        cf[i + 2 * n + m] = 1;
        lp.AddConstraint(cf, Relation.Equal, this.b[i]);
      }
      //the pairs forbidden for complimentary slackness
      var forbiddenPairs = new int[totalVars];
      for (int i = 0; i < n; i++)
      {
        forbiddenPairs[i] = n + i; //taking care that x[i]y[i]=0
        forbiddenPairs[n + i] = i;
      }

      for (int i = 0; i < m; i++)
      {
        forbiddenPairs[2 * n + i] = 2 * n + m + i;
        forbiddenPairs[2 * n + m + i] = 2 * n + i;
      }

      lp.ForbiddenPairs = forbiddenPairs;
      lp.EpsilonForArtificials = Double.MaxValue;//don't care
      lp.EpsilonForReducedCosts = 0.0001;

      double[] ret = lp.FeasibleSolution();
      if (lp.Status == Status.Feasible)
        return ret;

      return null;
    }

    [Pure]
    bool TryGetQMember(int i, int j, out double t)
    {
      if (i <= j)
        return Q.TryGetValue(new IntPair(i, j), out t);
      return Q.TryGetValue(new IntPair(j, i), out t);
    }
  }
}

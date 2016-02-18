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
    void ObjectInvariant()
    {
      Contract.Invariant(this.constraints != null);
      Contract.Invariant(y.Length == A.NumberOfRows);
      Contract.Invariant(y.Length > 0);
      Contract.Invariant(refactorizationPeriod != 0); // F: should be inferred
      Contract.Invariant(pivotEpsilon > 0.0);
    }

    #endregion

    //double det = 1;
    internal UMatrix U; //the U matrix of the factorization
    readonly internal int[] forbiddenPairs;
    readonly double epsilon = 10.0e-8;
    readonly double pivotEpsilon = 10.0e-8;
    readonly double etaEpsilon = 1.0e-6;
    internal const int notInBasis = -1;
    internal const int forgottenVar = -2;
    const int freeVar = -3;
    const double infinity = Double.MaxValue;

    /// <summary>
    /// the basis array
    /// </summary>
    internal int[] basis;

    readonly internal int[] index;

    int iterations;
    readonly int refactorizationPeriod = 20;
    internal ExtendedConstraintMatrix A;
    readonly int nVars;
    internal double[] y; //the array to keep right sides of the systems and their solutions
    //Length of y should be equal to the number of columns in A
    Factorization factorization;
    readonly List<Constraint> constraints;

    internal Factorization Factorization
    {
      get { return factorization; }
      set { factorization = value; }
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
      for (int i = 0; i < nVars; i++)
        if (index[i] == notInBasis && NotForbidden(i))
          yield return i;
    }

    [Pure]
    internal IEnumerable<int> NBasis(int start)
    {
      //take care here of free variables
      for (int i = start; i < nVars; i++)
        if (index[i] == notInBasis && NotForbidden(i))
          yield return i;
    }

    bool NotForbidden(int i)
    {
      return
          forbiddenPairs == null
          ||
          i >= forbiddenPairs.Length
          ||
          index[forbiddenPairs[i]] < 0; //the pair of i is not in the basis
    }


    readonly double[] costs;

    internal double[] Costs
    {
      get { return costs; }
      //            set { costs = value; }
    }

    readonly int artificialsStart;
    Status status = Status.Unknown;

    readonly bool[] lowBoundIsSet;
    readonly double[] lowBounds;

    readonly bool[] upperBoundIsSet;
    readonly double[] upperBounds;


    internal Status Status
    {
      get { return status; }
      set { status = value; }
    }

    int BasisSize
    {
      get { return A.NumberOfRows; }
    }


    readonly double[] xStar; //a feasible solution, first time is set in the constructor

    internal double[] XStar
    {
      get { return xStar; }
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
      etaEpsilon = etaEps;
      constraints = constrs;
      forbiddenPairs = forbiddenPairsPar;
      basis = basisArray;
      xStar = xSt;
      artificialsStart = startOfArtificials;
      nVars = APar.NumberOfColumns;
      A = APar;
      costs = costsPar;
      y = new double[BasisSize];
      index = new int[nVars];
      for (int i = 0; i < nVars; i++)
        index[i] = notInBasis;
      for (int i = 0; i < basis.Length; i++)
        index[basis[i]] = i; //that is the i-th element of basis

      lowBoundIsSet = lowBounds;
      this.lowBounds = lowBoundValues;
      upperBoundIsSet = upperBounds;
      this.upperBounds = upperBoundValues;

      U = new UMatrix(basis.Length);
    }

    [ContractVerification(false)]
    internal void Solve()
    {
      if (constraints.Count == 0)
      {
        Status = Status.Optimal;
        return;
      }
      iterations = 0;

      Contract.Assert(y.Length > 0);

      var yCopy = new double[y.Length];
      do
      {
        if (iterations++ % refactorizationPeriod == 0 || factorization == null)
        {
          var f = Factorization.CreateFactorization(new BMatrix(basis, A), U);
          if (f != null)
            factorization = f;
          else
          {
            status = Status.FloatingPointError;
            return;
          }
        }
        bool leavingIsFound = false;
        FillCostsB(y);
        factorization.Solve_yBEquals_cB(y);


        Contract.Assume(0 <= yCopy.Length - ((IList<double>) y).Count);
        y.CopyTo(yCopy, 0);
        int startLookingForEnteringFrom = 0;
        do
        {
          bool enteringHasToGrow;
          var entering = ChooseEnteringVariable(y, out enteringHasToGrow, startLookingForEnteringFrom);
          if (entering == -1)
          {
            status = Status.Optimal;
            return; //! returning from the middle of the loop in a good mood !
          }

          A.FillColumn(entering, y);
          factorization.Solve_BdEqualsa(y);

          var enteringT = BoundOnEnteringVar(entering, enteringHasToGrow);
          //the value of the entering variable
          var t = enteringT;
          var leaving = FindLeavingVariableAndT(y, ref t, enteringHasToGrow);
          if (leaving == -1 && t == infinity)
          {
            status = Status.Unbounded;
            //increasing the entering variable is always feasible, and the product costs*x grows infinitely
            return; //again returning from the middle of the loop
          }

          if (leaving == -1 || Math.Abs(y[index[leaving]]) > etaEpsilon)
          {
            //check that the diagonal element in the eta-matrix is big enough 
            leavingIsFound = true;
            FindNewXStarAndFactorization(leaving, entering, enteringHasToGrow ? t : -t, y, t < enteringT);
            if (leaving >= artificialsStart)
            {
              Contract.Assume(artificialsStart >= 0);
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
      } while (status != Status.Unbounded && status != Status.Optimal);
    }

    double BoundOnEnteringVar(int enteringVariable, bool enteringHasToGrow)
    {
      if (enteringHasToGrow)
      {
        if (upperBoundIsSet[enteringVariable])
          return upperBounds[enteringVariable] - xStar[enteringVariable];

        return Double.MaxValue;
      }
      // enteringHasToGrow==false
      if (lowBoundIsSet[enteringVariable])
        return xStar[enteringVariable] - lowBounds[enteringVariable];

      return Double.MaxValue;
    }


    void FindNewXStarAndFactorization(int leavingVariable, int enteringVariable, double t, double[] d,
                                      bool changeBasis)
    {
      Contract.Requires(leavingVariable >= -1);

      xStar[enteringVariable] += t;
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
        factorization.AddEtaMatrix(e);
      }
    }


    void FillCostsB(double[] cb)
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

    readonly double zeroTolerance = 10.0e-5;
    //double accuracyEpsilon = 1.0e-6; //used to compare A*xStar with b
    /// <summary>
    /// later provide more intelligent implementation
    /// </summary>
    /// <param name="y"></param>
    /// <returns></returns>
    unsafe int ChooseEnteringVariable(double[] yp, out bool enteringHasToGrow, int startLookingFrom)
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
      double maxCost = zeroTolerance;
      fixed (double* costsPin = costs)
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
    bool VarCanShrink(int i)
    {
      Contract.Requires(i >= 0);
      return lowBoundIsSet[i] == false || xStar[i] > lowBounds[i] + epsilon;
    }

    [Pure]
    bool VarCanGrow(int i)
    {
      Contract.Requires(i >= 0);
      return upperBoundIsSet[i] == false || xStar[i] < upperBounds[i] - epsilon;
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
    int FindLeavingVariableAndT(double[] d, ref double t, bool usePositiveT)
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
    double BoundOnVariableJ(int j, double p)
    {
      Contract.Requires(0 <= j);
      if (p > pivotEpsilon)
      {
        if (upperBoundIsSet[j])
          return (upperBounds[j] - xStar[j]) / p;
        return infinity;
      }

      if (p < -pivotEpsilon)
      {
        if (lowBoundIsSet[j])
          return (lowBounds[j] - xStar[j]) / p;
      }
      return infinity;
    }
  }
}
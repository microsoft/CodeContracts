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
  [Serializable]
  public class RevisedSimplexMethod : LinearProgramInterface
  {

    #region Object Invariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(nVars >= 0);
      Contract.Invariant(nSlacks >= 0);
      //Contract.Invariant(epsilon > 0.0f);
      Contract.Invariant(maxForConstraint > 0.0f);
      Contract.Invariant(this.constraints != null);
    }
    #endregion

    #region Fields

    readonly List<Constraint> constraints = new List<Constraint>();
    double[] costs;
    readonly double epsilon = 10.0E-8;
    double epsilonForArtificials = 0.0001;
    double etaEpsilon = 1.0E-7;
    int[] forbiddenPairs;
    Dictionary<int, double> knownValues;
    bool lookForZeroColumns;
    bool[] lowBoundIsSet;
    double[] lowBounds;

    public RevisedSimplexMethod()
    {
    }

    /// <summary>
    /// each constraint is scaled into maxInterval 
    /// </summary>
    readonly double maxForConstraint = 100;

    readonly double minForConstraint = 100;
    int nArtificials;
    int nSlacks;
    int nVars;
    [NonSerialized]
    RevisedSolver solver;
    Status status = Status.Unknown;

    bool[] upperBoundIsSet;
    double[] upperBounds;
    bool useScaling;
    double[] varScaleFactors; //we replace each variable x[i] by varScaleFactors[i]*x[i]

    public bool UseScaling
    {
      get { return useScaling; }
      set { useScaling = value; }
    }

    internal int NVars
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return nVars;
      }
      set
      {
        Contract.Requires(value >= 0);

        nVars = value;
        lowBoundIsSet = new bool[nVars];
        lowBounds = new double[nVars];
        upperBoundIsSet = new bool[nVars];
        upperBounds = new double[nVars];
      }
    }

    public bool LookForZeroColumns
    {
      get { return lookForZeroColumns; }
      set { lookForZeroColumns = value; }
    }

    #endregion


    #region LinearProgramInterface Members

    public double GetMinimalValue()
    {
      if (Status == Status.Unknown || Status == Status.Feasible)
      {
        Minimize();
      }
      if (Status == Status.Optimal)
      {
        double[] sol = MinimalSolution();

        return -LP.DotProduct(sol, costs, costs.Length); //minus since we reversed the costs
      }
      return 0;
    }


    public int[] ForbiddenPairs
    {
      get { return forbiddenPairs; }
      set { forbiddenPairs = value; }
    }

    public double Epsilon
    {
      get { return epsilon; }
      //set { epsilon = value; }
    }

    [ContractVerification(false)]
    public double[] FeasibleSolution()
    {
      int reps = 0;
      Status stat = status;
      do
      {
        if (reps > 0)
          status = stat;
        reps++;
        if (status == Status.Optimal)
          return MinimalSolution();

        StageOne();
        etaEpsilon *= 10;
        if (reps > 5)
          throw new InvalidOperationException();
      } while (solver.Status == Status.FloatingPointError);

      if (solver.Status != Status.Optimal || status == Status.Infeasible)
      {
        status = Status.Infeasible;
        return null;
      }
      status = Status.Feasible;
      var ret = new double[nVars];
      for (int i = 0; i < nVars; i++)
        ret[i] = solver.XStar[i];
      return ret;
    }

    public double[] MinimalSolution()
    {
      if (costs == null)
        throw new InvalidOperationException(
#if DEBUG
"costs are not set"
#endif
);
      Minimize();
      if (Status == Status.Optimal)
      {
        var ret = new double[NVars];
        if (UseScaling)
          for (int i = 0; i < NVars; i++)
          {
            Contract.Assume(varScaleFactors[i] != 0);
            ret[i] = solver.XStar[i] / varScaleFactors[i];
          }
        else
          for (int i = 0; i < NVars; i++)
            ret[i] = solver.XStar[i];

        return ret;
      }
      return null;
    }


    /// <summary>
    /// Call this method only in case when the program is infeasible.
    /// The linear program will be transformed to the form Ax=b. 
    /// The corresponding quadratic program is 
    /// minimize||Ax-b||, x>=0
    /// where ||.|| is the Euclidean norm.
    /// The solution always exists.
    /// Null is returned however if the matrix is too big
    /// </summary>
    /// <returns>approximation to solution</returns>
    public double[]
        LeastSquareSolution()
    {
      //calculate matrix A row length

      Contract.Assume(constraints.Count > 0);

      int l = constraints[0].coeffs.Length;
      int n = l;
      foreach (Constraint c in constraints)
        if (c.relation != Relation.Equal)
          n++;
      //this is code without any optimizations, well, almost any
      int m = constraints.Count; //number of rows in A

      var A = new double[m * n];
      int offset = 0;
      int slackVarOffset = 0;

      for (int i = 0; i < m; i++)
      {
        double[] coeff = constraints[i].coeffs;
        //copy coefficients
        for (int j = 0; j < l; j++)
          A[offset + j] = coeff[j];

        Relation r = constraints[i].relation;
        if (r == Relation.LessOrEqual)
          A[offset + l + slackVarOffset++] = 1; //c[i]*x+1*ui=b[i]
        else if (r == Relation.GreaterOrEqual)
          A[offset + l + slackVarOffset++] = -1; //c[i]*x-1*ui=b[i]
        offset += n;
      }


      var qp = new QP();
      //setting Q
      for (int i = 0; i < n; i++)
        for (int j = i; j < n; j++)
        {
          double q = 0;
          for (offset = 0; offset < A.Length; offset += n)
            q += A[offset + i] * A[offset + j];
          if (i == j)
            qp.SetQMember(i, j, q);
          else
            qp.SetQMember(i, j, q / 2);
        }

      var linearCost = new double[n];
      for (int k = 0; k < n; k++)
      {
        double c = 0;
        offset = k;
        for (int j = 0; j < m; j++, offset += n)
          c += constraints[j].rightSide * A[offset];

        linearCost[k] = c;
      }
      qp.SetLinearCosts(linearCost);

      //we have no constraints in qp except the default: solution has to be not negative
      double[] sol = qp.Solution;
      if (qp.Status != Status.Optimal)
        throw new InvalidOperationException();

      var ret = new double[l];
      for (int i = 0; i < l; i++)
        ret[i] = sol[i];
      return ret;
    }


    public void InitCosts(double[] costsParam)
    {
      Contract.Assume(NVars == 0 || NVars == costsParam.Length);
      if (NVars == 0)
        NVars = costsParam.Length;
      if (costs == null)
        costs = new double[costsParam.Length];
      //flip the costs signs since we are maximizing inside of the solver

      Contract.Assume(costs.Length >= NVars);

      for (int i = 0; i < NVars; i++)
      {
        Contract.Assert(i < costs.Length);
        Contract.Assume(i < costsParam.Length);

        costs[i] = -costsParam[i];
      }
      if (solver != null)
      {
        if (solver.Costs != null)
        {
          for (int i = 0; i < costsParam.Length; i++)
            solver.Costs[i] = -costsParam[i];
        }
        if (status == Status.Unbounded || status == Status.Optimal)
        {
          status = Status.Feasible;
          solver.Status = status;
        }
      }

    }

    public void SetVariable(int i, double val)
    {
      if (knownValues == null)
        knownValues = new Dictionary<int, double>();

      knownValues[i] = val;
    }

    /// <summary>
    /// Adds a constraint to the linear program.
    /// </summary>
    /// <param name="coeff"></param>
    /// <param name="relation"></param>
    /// <param name="rightSide"></param>
    public void AddConstraint(double[] coeff, Relation relation, double rightSide)
    {
      Contract.Assume(NVars == 0 || NVars == coeff.Length);
      if (NVars == 0)
        NVars = coeff.Length;

      var c = new Constraint(coeff.Clone() as double[], relation, rightSide);

      Contract.Assume(c.coeffs != null);

      if (UseScaling)
        ScaleConstraint(c);
      constraints.Add(c);
    }


    public void Minimize()
    {
      int reps = 0;
      Status stat = Status;
      do
      {
        Status = stat;
        if (Status == Status.Infeasible || Status == Status.Optimal)
          return;
        if (Status != Status.Feasible)
          StageOne();

        if (Status == Status.Feasible)
          StageTwo();
        etaEpsilon *= 10;
        reps++;
        if (reps > 5)
          throw new InvalidOperationException();
      } while (Status == Status.FloatingPointError);
    }


    public Status Status
    {
      get { return status; }
      set { status = value; }
    }

    public void LimitVariableFromBelow(int var, double l)
    {
      Contract.Assume(upperBoundIsSet[var] == false || upperBounds[var] >= l);

      lowBoundIsSet[var] = true;
      lowBounds[var] = l;
    }

    public void LimitVariableFromAbove(int var, double l)
    {
      Contract.Assume(lowBoundIsSet[var] == false || lowBounds[var] <= l);

      upperBoundIsSet[var] = true;
      upperBounds[var] = l;
    }


    public double EpsilonForArtificials
    {
      get { return epsilonForArtificials; }
      set { epsilonForArtificials = value; }
    }

    public double EpsilonForReducedCosts
    {
      get { return 0; }
      set { }
    }

    #endregion

    T[] DupArray<T>(T[] source)
    {
      if (source == null)
        return null;

      var result = new T[source.Length];
      Array.Copy(source, result, source.Length);

      return result;
    }


    void StageOne()
    {
      #region  Preparing the first stage solver

      if (UseScaling)
        IntroduceVariableScaleFactors();


      var xStar = new double[NVars];
      FillAcceptableValues(xStar);
      CountSlacksAndArtificials(xStar);
      int artificialsStart = NVars + nSlacks;
      int totalVars = NVars + nSlacks + nArtificials;

      Contract.Assume(totalVars > 0); // F: it seems to be some property of the implementation

      var nxstar = new double[totalVars];
      xStar.CopyTo(nxstar, 0);
      xStar = nxstar;

      var basis = new int[constraints.Count];

      var solverLowBoundIsSet = new bool[totalVars];
      var solverLowBounds = new double[totalVars];
      var solverUpperBoundIsSet = new bool[totalVars];
      var solverUpperBounds = new double[totalVars];
      var solverCosts = new double[totalVars];

      lowBoundIsSet.CopyTo(solverLowBoundIsSet, 0);
      lowBounds.CopyTo(solverLowBounds, 0);
      upperBoundIsSet.CopyTo(solverUpperBoundIsSet, 0);
      upperBounds.CopyTo(solverUpperBounds, 0);

      Contract.Assume(nSlacks + nArtificials >= 0);  // F: It seems to be some property of the implementation
      var AOfSolver = new ExtendedConstraintMatrix(constraints, nSlacks + nArtificials);

      FillFirstStageSolverAndCosts(solverLowBoundIsSet, solverLowBounds,
                                   solverUpperBoundIsSet, solverUpperBounds, solverCosts, AOfSolver, basis, xStar);


      solver = new RevisedSolver(basis,
                                 xStar,
                                 artificialsStart,
                                 AOfSolver,
                                 solverCosts,
                                 solverLowBoundIsSet, solverLowBounds,
                                 solverUpperBoundIsSet, solverUpperBounds,
                                 ForbiddenPairs,
                                 constraints,
                                 etaEpsilon
          );

      #endregion

      ProcessKnownValues();

      solver.Solve();
      if (solver.Status == Status.FloatingPointError)
        return;
      HandleArtificialVariablesAndDecideOnStatus(artificialsStart, ref basis,
                                                 AOfSolver as ExtendedConstraintMatrix);

    }

    void IntroduceVariableScaleFactors()
    {
      varScaleFactors = new double[nVars];
      for (int i = 0; i < nVars; i++)
        IntroduceVariableScaleFactor(i);

      foreach (Constraint c in constraints)
        for (int i = 0; i < nVars; i++)
        {
          Contract.Assume(varScaleFactors[i] != 0);
          c.coeffs[i] /= varScaleFactors[i];
        }
    }

    void IntroduceVariableScaleFactor(int i)
    {
      Contract.Requires(i >= 0);

      double max = 0;
      foreach (Constraint c in constraints)
      {
        double d = Math.Abs(c.coeffs[i]);
        if (d > max)
          max = d;
      }

      if (max > maxForConstraint)
      {
        //we need to find s such that max/s<=maxInterval => s>=max/ maxForConstraint
        int s = 2;
        double d = max / maxForConstraint;
        while (s < d)
          s *= 2;
        varScaleFactors[i] = s;
      }
      else if (max > epsilon && max < minForConstraint)
      {
        //max*s>=minForConstraint
        //s>=minForConstraint/max
        double d = minForConstraint / max;
        int s = 2;
        while (s < d)
          s *= 2;
        varScaleFactors[i] = 1.0 / s;
      }
      else if (max == 0)
      {
        varScaleFactors[i] = 1;
        SetVariable(i, GetAcceptableValue(i));
      }
      else
        varScaleFactors[i] = 1;
    }


    void ProcessKnownValues()
    {
      if (knownValues != null)
        foreach (var kv in knownValues)
        {
          int var = kv.Key;
          double val = kv.Value;
          solver.index[var] = RevisedSolver.forgottenVar;
          solver.XStar[var] = val;
        }
    }

    void HandleArtificialVariablesAndDecideOnStatus(int artificialsStart, ref int[] basis,
                                                    ExtendedConstraintMatrix AOfSolver)
    {
      Contract.Requires(AOfSolver != null);

      //check that there are no artificial variables having positive values
      //see box 8.2 on page 130
      for (int k = 0; k < basis.Length; k++)
      {
        if (basis[k] >= artificialsStart)
        {
          if (Math.Abs(solver.XStar[basis[k]]) > EpsilonForArtificials)
          {
            status = Status.Infeasible;
            return;
          }

          var r = new double[basis.Length];
          r[k] = 1;
          solver.Factorization.Solve_yBEquals_cB(r);

          var cloned = r.Clone() as double[];
          Contract.Assume(cloned != null);

          var vr = new Vector(cloned);

          foreach (int j in solver.NBasis())
          {
            if (j < artificialsStart)
            {
              Contract.Assume(j >= 0);  // F: Need quantified invariants here
              Contract.Assume(j < AOfSolver.NumberOfColumns);

              var colVector = new ColumnVector(AOfSolver, j);

              Contract.Assume(vr.Length == colVector.NumberOfRows);

              if (Math.Abs(vr * colVector) > epsilon)
              {
                AOfSolver.FillColumn(j, r);
                solver.Factorization.Solve_BdEqualsa(r);
                solver.Factorization.AddEtaMatrix(new EtaMatrix(k, r));
                solver.ReplaceInBasis(j, basis[k]);
                break;
              }
            }
          }
        }
      }
      //now remove the remaining artificial variables if any
      for (int k = 0; k < basis.Length; k++)
      {
        Contract.Assume(k < constraints.Count); // F: basis's length is < of the # of constraints

        if (basis[k] >= artificialsStart)
        {
          //cross out k-th constraint
          //shrink basis
          var nBas = new int[basis.Length - 1];
          int j = 0;
          for (int i = 0; i < basis.Length; i++)
          {
            if (i != k)
            {
              Contract.Assume(j < nBas.Length);
              nBas[j++] = basis[i];
            }
          }
          //taking care of the solver extended matrix
          constraints.RemoveAt(k);
          int[] sa = AOfSolver.slacksAndArtificials;
          for (int i = 0; i < sa.Length; i++)
            if (sa[i] > k)
              sa[i]--;
            else if (sa[i] == k)
              sa[i] = -1; //this will take put the column to zero

          solver.index[basis[k]] = RevisedSolver.forgottenVar;
          solver.basis = basis = nBas;
          for (int i = k; i < basis.Length; i++)
            solver.index[basis[i]] = i;
          k--;
          solver.Factorization = null; // to ensure the refactorization

          solver.A = new ExtendedConstraintMatrix(constraints, sa);
          solver.U = new UMatrix(nBas.Length);
        }
      }
      status = Status.Feasible;
    }


    void FillFirstStageSolverAndCosts(
        bool[] solverLowBoundIsSet,
        double[] solverLowBounds,
        bool[] solverUpperBoundIsSet,
        double[] solverUpperBounds,
        double[] solverCosts,
        Matrix A,
        int[] basis,
        double[] xStar)
    {
      Contract.Requires(solverUpperBounds != null);
      Contract.Requires(solverLowBounds != null);
      Contract.Requires(solverLowBoundIsSet != null);
      Contract.Requires(solverUpperBoundIsSet != null);
      Contract.Requires(solverCosts != null);
      Contract.Requires(A != null);
      Contract.Requires(xStar != null);

      int slackVar = NVars;
      int artificialVar = NVars + nSlacks;
      int row = 0;

      foreach (Constraint c in constraints)
      {

        Contract.Assume(row < basis.Length);

        //we need to bring the program to the form Ax=b
        double rs = c.rightSide - LP.DotProduct(xStar, c.coeffs, nVars);
        switch (c.relation)
        {
          case Relation.Equal: //no slack variable here
            if (rs >= 0)
            {
              SetZeroBound(solverLowBoundIsSet, solverLowBounds, artificialVar);
              Contract.Assume(artificialVar < solverCosts.Length);
              solverCosts[artificialVar] = -1;
              //we are maximizing, so the artificial, which is non-negatiive, will be pushed to zero
            }
            else
            {
              SetZeroBound(solverUpperBoundIsSet, solverUpperBounds, artificialVar);
              Contract.Assume(artificialVar < solverCosts.Length);
              solverCosts[artificialVar] = 1;
            }
            basis[row] = artificialVar;
            A[row, artificialVar++] = 1;
            break;

          case Relation.GreaterOrEqual:
            //introduce a non-positive slack variable,
            Contract.Assume(slackVar < solverUpperBoundIsSet.Length);
            Contract.Assume(slackVar < solverUpperBounds.Length);
            SetZeroBound(solverUpperBoundIsSet, solverUpperBounds, slackVar);
            A[row, slackVar] = 1;
            if (rs > 0)
            {
              //adding one artificial which is non-negative
              SetZeroBound(solverLowBoundIsSet, solverLowBounds, artificialVar);
              A[row, artificialVar] = 1;
              solverCosts[artificialVar] = -1;
              basis[row] = artificialVar++;
            }
            else
            {
              //we can put slackVar into basis, and avoid adding an artificial variable
              //We will have an equality c.coefficients*acceptableCosts+x[slackVar]=c.rightSide, or x[slackVar]=rs<=0.
              basis[row] = slackVar;
            }
            slackVar++;

            break;

          case Relation.LessOrEqual:
            //introduce a non-negative slack variable,
            Contract.Assume(slackVar < solverLowBoundIsSet.Length);
            Contract.Assume(slackVar < solverLowBounds.Length);
            SetZeroBound(solverLowBoundIsSet, solverLowBounds, slackVar);
            A[row, slackVar] = 1;
            if (rs < 0)
            {
              //adding one artificial which is non-positive
              SetZeroBound(solverUpperBoundIsSet, solverUpperBounds, artificialVar);
              A[row, artificialVar] = 1;
              Contract.Assume(artificialVar < solverCosts.Length);
              solverCosts[artificialVar] = 1;
              basis[row] = artificialVar++;
            }
            else
            {
              //we can put slackVar into basis, and avoid adding an artificial variable
              //We will have an equality c.coefficients*acceptableCosts+x[slackVar]=c.rightSide, or x[slackVar]=rs<=0.
              basis[row] = slackVar;
            }
            slackVar++;
            break;
        }

        xStar[basis[row]] = rs;

        row++;
      }
    }

    /// <summary>
    /// fill the original values with some values from their [lowBound, upperBound] intervals
    /// </summary>
    /// <returns></returns>
    void FillAcceptableValues(double[] xStar)
    {
      Contract.Assume(xStar.Length >= nVars);

      double val;
      for (int i = 0; i < nVars; i++)
      {
        if (knownValues == null || knownValues.TryGetValue(i, out val) == false)
        {
          xStar[i] = GetAcceptableValue(i);
        }
        else
        {
          xStar[i] = val;
        }
      }
    }

    /// <summary>
    /// return a number from the variable domain
    /// </summary>
    /// <param name="x"></param>
    /// <param name="i"></param>
    [Pure]
    double GetAcceptableValue(int i)
    {
      if (lowBoundIsSet[i])
        return lowBounds[i];
      if (upperBoundIsSet[i])
        return upperBounds[i];
      return 0;
    }

    static void SetZeroBound(bool[] boundIsSet, double[] bounds, int var)
    {
      Contract.Assume(var < boundIsSet.Length);
      Contract.Assume(var < bounds.Length); 

      boundIsSet[var] = true;
      bounds[var] = 0;
    }

    void CountSlacksAndArtificials(double[] xStar)
    {
      foreach (Constraint constraint in constraints)
        CountSlacksAndArtificialsForConstraint(constraint, xStar);
    }

    void CountSlacksAndArtificialsForConstraint(Constraint constraint, double[] xStar)
    {
      double rightSide = constraint.rightSide - LP.DotProduct(constraint.coeffs, xStar);
      switch (constraint.relation)
      {
        case Relation.Equal:
          nArtificials++;
          break;
        case Relation.GreaterOrEqual:
          nSlacks++;
          if (rightSide > 0)
            nArtificials++;
          break;
        case Relation.LessOrEqual:
          nSlacks++;
          if (rightSide < 0)
            nArtificials++;
          break;
      }
    }


    //static int calls;

    void StageTwo()
    {
      PrepareStageTwo();
      solver.Solve();
      status = solver.Status;
    }

    void PrepareStageTwo()
    {
      int i;
      for (i = NVars + nSlacks; i < NVars + nSlacks + nArtificials; i++)
        solver.ForgetVariable(i);

      var solverMatrix = solver.A;
      Contract.Assume(solverMatrix != null);
      solverMatrix.NumberOfColumns = NVars + nSlacks;
      if (solver.y.Length != constraints.Count)
        solver.y = new double[constraints.Count];
      //this should be refactored somehow, y is the variable for keeping solutions of the linear systems
      for (i = 0; i < costs.Length; i++)
        solver.Costs[i] = costs[i];

      for (; i < solver.Costs.Length; i++)
        solver.Costs[i] = 0;

      solver.Status = Status.Feasible;
    }


    void ScaleConstraint(Constraint c)
    {
      Contract.Requires(c != null);

      double max = 0;
      for (int i = 0; i < c.coeffs.Length; i++)
      {
        double d = Math.Abs(c.coeffs[i]);
        if (d > max)
          max = d;
      }

      if (max > maxForConstraint)
      {
        //find first power of two k, such that max/k<=maxForConstraint,
        //that is  k >= max / maxForConstraint;
        int k = 2;
        double d = max / maxForConstraint;
        while (k < d)
          k *= 2;
        for (int i = 0; i < c.coeffs.Length; i++)
          c.coeffs[i] /= k;
        c.rightSide /= k;
      }
      else if (max < minForConstraint && max > epsilon)
      {
        //find first power of two, k , such that max*k>=minForConstraint,
        // or k>=minForConstrant/max
        double d = minForConstraint / max;
        int k = 2;
        while (k < d)
          k *= 2;

        for (int i = 0; i < c.coeffs.Length; i++)
          c.coeffs[i] *= k;
        c.rightSide *= k;
      }
    }
  }
}
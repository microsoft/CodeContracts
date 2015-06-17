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
  The algorithm is taken from book "Combinatorial Optimization. Algorithms and Complexity"
  Christos H. Papadimitriou, Kenneth Steigitz
*/

namespace Microsoft.Glee.Optimization
{



  /// <summary>
  /// Solves the general liner program but always looking for minimum
  /// </summary>
  public class LP : LinearProgramInterface
  {

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      // Commenting it out as it kill performances
      // Contract.Invariant(this.status != Status.Infeasible || this.constraintCoeffs.Count > 0);
      Contract.Invariant(this.knownValues != null);
      Contract.Invariant(this.costsOfknownValues != null);
      Contract.Invariant(this.constraintCoeffs.Count == this.relations.Count);
      Contract.Invariant(this.constraintCoeffs.Count == this.rightSides.Count);
    }


    public double Epsilon
    {
      get { return epsilon; }
      set { epsilon = value; }
    }

    readonly SortedDictionary<int, double> knownValues = new SortedDictionary<int, double>();
    readonly Dictionary<int, double> costsOfknownValues = new Dictionary<int, double>();

    SortedDictionary<int, double> KnownValues
    {
      get { return knownValues; }
    }

    int[] forbiddenPairs;

    public int[] ForbiddenPairs
    {
      get { return forbiddenPairs; }
      set { forbiddenPairs = value; }
    }

    internal double epsilon = 1.0E-8;

    int nVars;
    int nArtificials;

    double maxVal = 1;

    readonly private List<double[]> constraintCoeffs = new List<double[]>();

    readonly internal List<Relation> relations = new List<Relation>();
    readonly internal List<double> rightSides = new List<double>();

    int nSlaksAndSurpluses;

#if DEBUGGLEE
        public 
#else
    internal
#endif
 double[] costs;

    public bool CostsAreSet
    {
      get
      {
        return this.costs != null;
      }
    }

    Microsoft.Glee.Optimization.Tableau tableau;

    Solver solver;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lookForZeroColumns">the solver will try to eliminate variables that never appears with non-zero coefficients</param>
    public LP(bool lookForZeroColumns)
    {
      this.LookForZeroColumns = lookForZeroColumns;
    }

    public LP()
      : this(false)
    {

    }


#if DEBUGGLEE
        static public LP FromFile(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);

            LP lp = new LP(false);

            int nvars = Int32.Parse(sr.ReadLine());

            double[] costs = new double[nvars];

            string[] cw = sr.ReadLine().Split(new char[] { ' ' });

            for (int i = 0; i < nvars; i++)
                costs[i] = Double.Parse(cw[i]);

            lp.InitCosts(costs);


            sr.ReadLine();//swallow the line "constraints"

            int nConstraints = Int32.Parse(sr.ReadLine());

            for (int i = 0; i < nConstraints; i++)
            {
                string constr = sr.ReadLine();

                string[] words = constr.Split(new char[] { ' ' });

                List<string> ws = new List<string>();

                foreach (string s in words)
                {
                    if (s != "")
                        ws.Add(s);
                }

                double[] coeff = new double[nvars];
                for (int j = 0; j < nvars; j++)
                {
                    coeff[j] = Double.Parse((string)ws[j]);
                }
                Relation rel = (string)ws[nvars] == "=" ? Relation.Equal :
                              (string)ws[nvars] == "<=" ? Relation.LessOrEqual : Relation.GreaterOrEqual;
                double rs = Double.Parse((string)ws[nvars + 1]);
                lp.AddConstraint(coeff, rel, rs);
                sr.ReadLine(); //eat the empty line
            }

            sr.Close();

            return lp;

        }

        public void ToFileInMathFormat(string fileName) {
            LP lp = new LP(false);

            StreamWriter sw = new StreamWriter(fileName, false,System.Text.Encoding.ASCII);

            sw.Write("f=[");

            for (int i = 0; i < nVars; i++) {
                sw.Write(this.costs[i].ToString() + "; ");
            }

            sw.WriteLine("]");

         
            int k = 0;
            sw.Write("A=[");
            foreach (double[] coeff in this.constraintCoeffs) {
                k = WriteConstraint(sw, k, coeff);
            }
          sw.WriteLine("];");
          sw.Write("b=[");

          k = 0;
          foreach (double[] coeff in this.constraintCoeffs) {
              Relation r = this.relations[k];
              double rs=this.rightSides[k];
              if (r == Relation.LessOrEqual)
                  sw.Write(rs + ";");
              else if (r == Relation.GreaterOrEqual)
                  sw.Write(-rs + ";");
              else {
                  sw.Write(-rs + ";");
                  sw.Write(rs + ";");              
              }
              k++;
          }
          sw.WriteLine("];");
          sw.WriteLine("lb = zeros({0},1);",this.nVars);
            sw.Close();

        }

        private int WriteConstraint(StreamWriter sw, int k, double[] coeff) {

            if (this.relations[k] == Relation.LessOrEqual)
                WriteCoefficients(sw, coeff, false);
            else if (this.relations[k] == Relation.GreaterOrEqual)
                WriteCoefficients(sw, coeff, true);
            else {
                WriteCoefficients(sw, coeff, true);
                WriteCoefficients(sw, coeff, false);
            }
            return k + 1;
        }

        private static void WriteCoefficients(StreamWriter sw, double[] coeff, bool changeSign) {
            int sign=changeSign?-1:1;
            foreach (double c in coeff)
                sw.Write(sign*c + " ");
            sw.WriteLine();

        }

        public void ToFile(string fileName)
        {

            LP lp = new LP(false);

            StreamWriter sw = new StreamWriter(fileName);

            sw.WriteLine(costs.Length);

            for (int i = 0; i < nVars; i++)
            {
                sw.Write(this.costs[i].ToString() + " ");
            }

            sw.WriteLine("\nconstraints");

            sw.WriteLine(this.constraintCoeffs.Count);

            int k = 0;

            foreach (double[] coeff in this.constraintCoeffs)
            {
                foreach (double c in coeff)
                    sw.Write(c + " ");


                Relation r = (Relation)this.relations[k];
                string rel = r == Relation.Equal ? "  =  " : r == Relation.LessOrEqual ? " <= " : " >= ";

                sw.WriteLine(rel + " " + this.rightSides[k] + "\n");

                k++;

            }

            sw.WriteLine("end");
            sw.Close();


        }
#endif
    /// <summary>
    /// finds a solution which is feasible but not necesserily optimal
    /// </summary>

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
    public double[] FeasibleSolution()
    {
      if (status == Status.Optimal)
        return MinimalSolution();


      //will create solver and tableau
      DoStageOne();

      if (solver.status != Status.Optimal || status == Status.Infeasible)
      {
        status = Status.Infeasible;
        return null;
      }
      this.status = Status.Feasible;
      double[] ret = tableau.GetSolution(nVars);
      // CheckConstraints(ret);
      return ret;

    }


    /// <summary>
    /// returns an optimal solution: that is a solution where cx is minimal
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
    public double[] MinimalSolution()
    {
      Contract.Assume(this.CostsAreSet);
      // System.Diagnostics.Debug.Assert(costs != null);
      if (status == Status.Unknown)
      {
        this.Minimize();
      }
      else if (status == Status.Feasible)
        DoStageTwo();

      if (status == Status.Optimal)
        return ExtendVector(tableau.GetSolution(nVars));

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
      // F: the following lines are a Clousot-nice way of writing this.status == status.Infeasible ==> this.constraints.Count > 0
      //Contract.Requires(this.Status == Status.Infeasible);

      Contract.Assume(this.constraintCoeffs.Count > 0);


      //calculate matrix A row length
      int l = this.constraintCoeffs[0].Length;
      int n = l;
      foreach (Relation r in relations)
        if (r != Relation.Equal)
          n++;
      //this is code without any optimizations, well, almost any
      int m = this.relations.Count;//number of rows in A

      //if (2 * (m + n) //number of variables in the QP
      //  * (m + n) //number of constraints
      //  > Microsoft.Glee.Channel.SplitThresholdMatrixSize * 4)
      //    return null;

      double[] A = new double[m * n];
      int offset = 0;
      int slackVarOffset = 0;

      for (int i = 0; i < m; i++)
      {
        double[] coeff = this.constraintCoeffs[i];
        //copy coefficients
        for (int j = 0; j < l; j++)
          A[offset + j] = coeff[j];

        Relation r = relations[i];
        if (r == Relation.LessOrEqual)
          A[offset + l + slackVarOffset++] = 1; //c[i]*x+1*ui=b[i]
        else if (r == Relation.GreaterOrEqual)
          A[offset + l + slackVarOffset++] = -1;//c[i]*x-1*ui=b[i]
        offset += n;
      }



      QP qp = new QP();
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

      double[] linearCost = new double[n];
      for (int k = 0; k < n; k++)
      {
        double c = 0;
        offset = k;
        for (int j = 0; j < m; j++, offset += n)
        {
          Contract.Assert(j < this.rightSides.Count);
          c += this.rightSides[j] * A[offset];
        }
        linearCost[k] = c;

      }
      qp.SetLinearCosts(linearCost);

      //we have no constraints in qp except the default: solution has to be not negative
      double[] sol = qp.Solution;
      if (qp.Status != Status.Optimal)
        throw new InvalidOperationException();

      double[] ret = new double[l];
      for (int i = 0; i < l; i++)
        ret[i] = sol[i];
      return ret;
    }

    double[] ExtendVector(double[] v)
    {
      if (knownValues.Count > 0)
      {
        var ret = new double[v.Length + KnownValues.Count];
        var ofs = 0;
        var k = 0; //points to the index in the array which is to be returned

        foreach (var kv in KnownValues)
        {
          for (; k < kv.Key; k++)
          {
            Contract.Assume(k >= ofs, "Domain knowledge?");
            ret[k] = v[k - ofs];
          }
          ret[k++] = kv.Value;

          ofs++;
        }

        Contract.Assume(ofs <= k, "Weakness of numerical domains?");
        for (var i = k; i < ret.Length; i++)
        {
          Contract.Assume(i - ofs < v.Length);
          ret[i] = v[i - ofs];
        }
        return ret;
      }
      else
      {
        return v;
      }
    }

    bool lookForZeroColumns;

    public bool LookForZeroColumns
    {
      get { return lookForZeroColumns; }
      set { lookForZeroColumns = value; }
    }


    void ShrinkCostsAndCoefficients()
    {
      int[] zeroColumns = new int[nVars];
      if (LookForZeroColumns)
      {
        zeroColumns = new int[nVars];
        for (int i = 0; i < nVars; i++)
        {
          foreach (double[] d in constraintCoeffs)
          {
            if (d[i] != 0)
            {
              zeroColumns[i] = 1;
              break;
            }
          }
        }
        //insert to known values 
        for (int i = 0; i < nVars; i++)
          if (zeroColumns[i] == 0)
            KnownValues[i] = 0;
      }



      if (KnownValues.Count > 0)
      {
        //remember old costs
        foreach (int i in this.knownValues.Keys)
          costsOfknownValues[i] = this.costs[i];
        //fix the right sides
        foreach (KeyValuePair<int, double> kv in this.KnownValues)
        {
          for (int i = 0; i < constraintCoeffs.Count; i++)
            this.rightSides[i] -= constraintCoeffs[i][kv.Key] * kv.Value;
        }

        for (int i = 0; i < this.constraintCoeffs.Count; i++)
          constraintCoeffs[i] = ShrinkVector(constraintCoeffs[i]);

        costs = ShrinkVector(costs);

        nVars = costs.Length;

        //make right sides non-negative, and recalculate nSlacksAndSurpluses and nArtificials

        this.nSlaksAndSurpluses = this.nArtificials = 0;
        for (int i = 0; i < this.constraintCoeffs.Count; i++)
        {
          if (this.rightSides[i] < 0)
          {
            this.rightSides[i] *= -1;
            double[] cf = this.constraintCoeffs[i];
            Contract.Assume(cf != null);
            for (int j = 0; j < cf.Length; j++)
              cf[j] *= -1;

            this.relations[i] = OppositeRelation(this.relations[i]);
          }

          switch (relations[i])
          {
            case Relation.LessOrEqual:
              this.nSlaksAndSurpluses++;
              break;
            case Relation.GreaterOrEqual:
              this.nSlaksAndSurpluses++;
              this.nArtificials++;
              break;
            default: //equality
              this.nArtificials++;
              break;
          }
        }
      }
    }


    static private Relation OppositeRelation(Relation relation)
    {
      switch (relation)
      {
        case Relation.Equal:
          return Relation.Equal;
        case Relation.LessOrEqual:
          return Relation.GreaterOrEqual;
        case Relation.GreaterOrEqual:
          return Relation.LessOrEqual;
        default:
          throw new InvalidOperationException();
      }
    }


    double[] ShrinkVector(double[] v)
    {

      double[] ret = new double[v.Length - knownValues.Count];

      int ofs = 0;
      int i = 0;
      foreach (int j in KnownValues.Keys)
      {
        for (int k = i; k < j; k++)
        {
          Contract.Assume(k >= ofs);

          ret[k - ofs] = v[k];
        }
        i = j + 1;
        ofs++;
      }

      for (int k = i; k < v.Length; k++)
      {
        Contract.Assume(k >= ofs);
        ret[k - ofs] = v[k];
      }
      return ret;
    }

    internal static double DotProduct(double[] a, double[] b)
    {
      Contract.Requires(a.Length == b.Length);

      int dim = a.Length;
      double r = 0;
      for (int i = 0; i < dim; i++)
      {
        r += a[i] * b[i];
      }
      return r;
    }

    internal static double DotProduct(double[] a, double[] b, int dim)
    {
      Contract.Requires(0 <= dim);
      Contract.Requires(dim <= a.Length);
      Contract.Requires(dim <= b.Length);

      double r = 0;
      for (int i = 0; i < dim; i++)
        r += a[i] * b[i];

      return r;
    }
    /// <summary>
    /// The value of the cost function at an optimal solution.
    /// </summary>
    public double GetMinimalValue()
    {
      if (this.Status != Status.Optimal)
      {
        Minimize();
      }
      if (this.Status == Status.Optimal)
      {
        Contract.Assume(nVars <= costs.Length, "Should it be an object invariant?");
        return DotProduct(this.tableau.GetSolution(nVars), costs, nVars) + CostOfKnowVariables();
      }
      return 0;
    }


    private double CostOfKnowVariables()
    {
      double r = 0;
      foreach (KeyValuePair<int, double> kv in this.knownValues)
        r += kv.Value * costsOfknownValues[kv.Key];
      return r;
    }

    /// <summary>
    /// set the cost function
    /// </summary>
    /// <param name="costs">the objective vector</param>
    public void InitCosts(double[] costsParam)
    {
      if (costsParam == null)
        throw new InvalidDataException();
      if (nVars == 0)
        nVars = costsParam.Length;
      else
      {
        if (nVars != costsParam.Length)
          throw new InvalidDataException();//"wrong length of costs");
      }

      this.costs = costsParam;

    }

    /// <summary>
    /// If one just happens to know the i-th variable value it can be set
    /// </summary>
    /// <param name="i"></param>
    /// <param name="val"></param>
    public void SetVariable(int i, double val)
    {
      this.KnownValues[i] = val;
    }

    /// <summary>
    /// adds a constraint: the coeffiecents should not be very close to zero or too huge.
    /// If it is the case, as one can scale for example the whole programm to zero,
    /// then such coefficient will be treated az zeros. We are talking here about the numbers
    /// with absolute values less than 1.0E-8
    /// </summary>
    /// <param name="coeff">the constraint coefficents</param>
    /// <param name="relation">could be 'less or equal', equal or 'greater or equal'</param>
    /// <param name="rightSide">right side of the constraint</param>
    public void AddConstraint(double[] coeff, Relation relation, double rightSide)
    {

      foreach (double m in coeff)
        UpdateMaxVal(m);

      UpdateMaxVal(rightSide);

      status = Status.Unknown;

      coeff = (double[])coeff.Clone();

      if (nVars == 0)
      {
        //init nVars
        nVars = coeff.Length;
      }
      else
      {
        if (nVars != coeff.Length)
          throw new InvalidOperationException();//"Two constraints of different length have been added");
      }


      //if (relation==Relation.Equal&&LookForEqualitySingletons) {

      //    int k=-1;//the place of the singleton
      //    for (int i = 0; i < coeff.Length; i++)
      //        if (Math.Abs(coeff[i]) > Epsilon)
      //            if (k == -1)
      //                k = i;
      //            else {
      //                k = -2;
      //                break;
      //            }

      //    if (k != -2) {
      //        double oldRightSide;
      //        if (KnownValues.TryGetValue(k, out oldRightSide)) {
      //            if (Math.Abs(oldRightSide - rightSide / coeff[k]) > Epsilon) {
      //                this.Status = Status.Infeasible;
      //                return;
      //            }
      //        } else {
      //            KnownValues[k] = rightSide / coeff[k];
      //            return; // don't add this constraint
      //        }

      //    }

      //}

      //make right side not-negative

      if (rightSide < 0)
      {
        rightSide *= -1;
        for (int i = 0; i < coeff.Length; i++)
          coeff[i] *= -1;

        if (relation == Relation.LessOrEqual)
          relation = Relation.GreaterOrEqual;
        else if (relation == Relation.GreaterOrEqual)
          relation = Relation.LessOrEqual;
      }

      this.constraintCoeffs.Add(coeff);
      this.relations.Add(relation);
      this.rightSides.Add(rightSide);


      if (relation == Relation.LessOrEqual)
        this.nSlaksAndSurpluses++;
      else if (relation == Relation.GreaterOrEqual)
      {
        this.nSlaksAndSurpluses++;
        this.nArtificials++;
      }
      else //equality
        this.nArtificials++;

    }

    private void UpdateMaxVal(double m)
    {
      if (m < 0)
      {
        if (-m > maxVal)
          maxVal = -m;
      }
      else if (m > maxVal)
        maxVal = m;

    }

    void CreateStageOneSolver()
    {
      Contract.Assume(this.constraintCoeffs.Count > 0); // F: there should be at least 1 constraint

      int m = this.constraintCoeffs.Count;
      int length1 = this.nVars + this.nSlaksAndSurpluses + this.nArtificials;

      Contract.Assume(length1 >= 0);

      //will be initialized by zeroes

      double[] X = new double[m * length1];

      int[] basis = new int[m];
      double[] x = new double[m];

      nArtificials = 0;

      int localSlacksAndSurps = 0;
      int ioffset = 0;
      for (int i = 0; i < m; i++, ioffset += length1)
      {
        //copy the coeff first
        double[] coeff = (double[])constraintCoeffs[i];
        for (int j = 0; j < nVars; j++)
        {
          X[ioffset + j] = coeff[j];
        }

        Relation r = (Relation)this.relations[i];

        if (r == Relation.LessOrEqual)
        {

          //we have a slack variable
          int j = this.nVars + localSlacksAndSurps++;
          X[ioffset + j] = 1;

          basis[i] = j;

        }
        else if (r == Relation.GreaterOrEqual)
        {
          //we have surplus variable
          int j = this.nVars + localSlacksAndSurps++;
          X[ioffset + j] = -1;

          int artificial = this.nVars + this.nSlaksAndSurpluses + nArtificials++;

          X[ioffset + artificial] = 1;
          //artificial goes to the basis
          basis[i] = artificial;


        }
        else
        { //equality

          int artificial = this.nVars + this.nSlaksAndSurpluses + nArtificials++;

          X[ioffset + artificial] = 1;
          //artificial goes to the basis
          basis[i] = artificial;


        }

        x[i] = (double)rightSides[i];
      }

      double[] artifCosts = new double[X.Length / x.Length];

      Contract.Assume(0 <= this.nVars + this.nSlaksAndSurpluses);
      for (int i = this.nVars + this.nSlaksAndSurpluses; i < artifCosts.Length; i++)
      {
        artifCosts[i] = 1;
      }
      solver = new Solver(basis, X, x, artifCosts, EpsilonForReducedCosts);


    }
    /// <summary>
    /// make all coefficients not greater than 1, unless they are small aready
    /// </summary>
    void ScaleToMaxVal()
    {

      if (maxVal < 10)
        return;
      for (int i = 0; i < constraintCoeffs.Count; i++)
      {
        double[] cc = constraintCoeffs[i];
        for (int j = 0; j < cc.Length; j++)
          cc[j] /= maxVal;
      }

      for (int i = 0; i < rightSides.Count; i++)
        rightSides[i] /= maxVal;

    }
    double epsilonForArtificials = 1.0E-8;

    public double EpsilonForArtificials
    {
      get { return epsilonForArtificials; }
      set { epsilonForArtificials = value; }
    }
    double epsilonForReducedCosts = 1.0E-8;

    public double EpsilonForReducedCosts
    {
      get { return epsilonForReducedCosts; }
      set { epsilonForReducedCosts = value; }
    }

    [ContractVerification(false)]
    void DoStageOne()
    {
      ScaleToMaxVal();
      CreateStageOneSolver();
      solver.ForbiddenPairs = forbiddenPairs;
      solver.Solve();
      if (solver.status != Status.Optimal)
      {
        this.status = Status.Infeasible;
        return;
      }

      //this.CheckConstraints(this.solver.tableau.Getx());

      //drive out artificials from the basis
      tableau = solver.tableau;
      var X = tableau.ReturnMatrix();
      var x = tableau.GetSolution();
      var basis = tableau.ReturnBasis();

      Contract.Assume(X != null);
      Contract.Assume(x != null);
      Contract.Assume(basis != null);

      int n = tableau.ReturnLengthOne();

      int artificialsStart = nVars + nSlaksAndSurpluses;
      int ioffset = 0;
      Contract.Assume(x.Length <= basis.Length);
      for (int i = 0; i < x.Length; i++, ioffset += n)
      {
        Contract.Assert(x.Length <= basis.Length);
        Contract.Assert(i < basis.Length);
        if (basis[i] >= artificialsStart)
        {
          if (x[i] > EpsilonForArtificials)
          {
            this.status = Status.Infeasible;  //one of the artificials remains non-zero
            return;
          }

          //find any non-zero number in non-artificials
          int nonNulColumn = -1;
          for (int j = 0; j < artificialsStart; j++)
          {
            Contract.Assume(ioffset + j >= 0); // ioffset gets decremented sometime
            Contract.Assume(ioffset + j < X.Length); // ioffset gets decremented sometime

            if (Math.Abs(X[ioffset + j]) > Epsilon)
            {

              nonNulColumn = j;
              break;
            }
          }

          if (nonNulColumn != -1)
            tableau.Pivot(i, nonNulColumn);
          else
          {
            //we  have to cross out the i-th row because it is all zero
            var newX = new double[(x.Length - 1) * n];
            var newx = new double[x.Length - 1];
            var ioOffset = 0; 
            var Ioffset = 0;
            var io = 0;
            for (var I = 0; I < x.Length; I++, Ioffset += n)
            {
              if (I != i)
              {
                Contract.Assume(io < newx.Length);
                newx[io] = x[I];
                for (int J = 0; J < n; J++)
                {
                  Contract.Assume(Ioffset + J < X.Length);
                  var v = X[Ioffset + J];
                  Contract.Assume(ioffset + J < newX.Length);
                  newX[ioOffset + J] = v;
                }
                io++;
                ioOffset += n;
              }
            }
            var newBasis = new int[x.Length - 1];

            io = 0;
            for (var I = 0; I < x.Length; I++)
            {
              Contract.Assert(x.Length <= basis.Length);

              if (I != i)
              {
                Contract.Assume(io < newBasis.Length);
                newBasis[io] = basis[I];
                io++;
              }
            }
            //continue the loop on new X and on changed tableau
            X = newX;
            x = newx;

            tableau.UpdateMatrix(X, x.Length, n);
            tableau.SetSolution(x);
            tableau.UpdateBasis(newBasis);
            basis = newBasis;
            i--;
            ioffset -= n;
          }
        }
      }
    }

    /// <summary>
    /// Solves the linear program, minimizing, by going through stages one and two
    /// </summary>
    public void Minimize()
    {

      //solve will change the tableau 
      if (nVars == 0)
      {
        return;
      }
      if (solver == null || solver.status == Status.Unknown)
      {
        ShrinkCostsAndCoefficients();
        if (this.Status == Status.Infeasible)
        {
          return;
        }
        DoStageOne();
      }

      if (solver.status != Status.Optimal || status == Status.Infeasible)
      {
        return;
      }
      DoStageTwo();
    }

    void DoStageTwo()
    {

      var lengthWithNoArt = this.nVars + this.nSlaksAndSurpluses;
      var extendedCosts = new double[lengthWithNoArt];

      for (var i = 0; i < nVars; i++)
      {
        extendedCosts[i] = costs[i];
      }
      solver.SetCosts(extendedCosts);

      solver.status = Status.Unknown;
      solver.Solve();
      status = solver.status;

    }

    Status status;
    public Status Status
    {
      get { return status; }
      set { status = value; }
    }

    /*
    public void Test()
    {
      double[] x = MinimalSolution;
      if (x != null)
        CheckConstraints(x);
    }*/
#if DEBUGSIMPLEX
    public static double maxDelta = 0;

    void CheckConstraints(double[] x)
    {
      int i = 0;
      foreach (double[] a in this.constraintCoeffs)
      {
        double v = dot(a, x, nVars);
        double rs = (double)rightSides[i];
        double delta = 0;
        switch ((Relation)this.relations[i])
        {
          case Relation.Equal:
            delta = Math.Abs(v - rs);
            break;
          case Relation.GreaterOrEqual:
            delta = Math.Min(0, rs - v);
            break;
          case Relation.LessOrEqual:
            delta = Math.Max(0, v - rs);
            break;
        }
        if (delta > maxDelta)
          maxDelta = delta;
        i++;
      }

    }
    
#endif

    #region LinearProgramInterface Members


    public void LimitVariableFromBelow(int var, double l)
    {
      if ((double)l != 0)
        throw new InvalidOperationException();
    }

    public void LimitVariableFromAbove(int var, double l)
    {
      //throw new Exception("The method or operation is not implemented.");
    }

    #endregion
  }
}

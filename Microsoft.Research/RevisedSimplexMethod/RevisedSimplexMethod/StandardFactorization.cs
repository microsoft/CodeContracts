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
using System.Text;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Glee.Optimization
{

  /// <summary>
  /// The class keeps  a factorization of the matrix A. In this case it is an equality
  /// Lm*Pm*...*L1*P1*A=Un-1*...U0*E1*...*Ek. Li are lower triangular eta matrices, 
  /// Pi are transposition matrices,
  /// Ui are upper triangular eta matrices with ones on the diagonal everywhere, and Ei are eta matrices.
  /// </summary>
  internal class StandardFactorization : Factorization
  {

    #region Object Invariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(dim >= 0);
      Contract.Invariant(A != null);
      Contract.Invariant(LP != null);
      Contract.Invariant(etaList != null);
      Contract.Invariant(U != null);
    }
    #endregion

    readonly BMatrix A;
    readonly int dim; //A is a dimXdim matrix
    readonly List<Matrix> LP = new List<Matrix>(); //it is the list of matrix L, P
    readonly List<EtaMatrix> etaList = new List<EtaMatrix>(); //it is the list of matrix eta matrices
    //int[] q;
    // double pivotEpsilon = 1.0e-1;
    readonly UMatrix U;
    bool failure = false;

    /// <summary>
    /// create the initial factorization of the form Ln*Pn*...*L1*P1*A=Un-1*...U0
    /// </summary>
    /// <param name="APar"></param>
    internal StandardFactorization(Matrix APar, UMatrix Upar)
    {
      Contract.Requires(APar != null);
      Contract.Requires(Upar != null);

      this.A = (BMatrix)APar;
      this.dim = A.NumberOfColumns;
      this.U = Upar;
      InitMatrixUAndMarkovichNumbers();
      CalculateInitialFactorization();

    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    unsafe private void InitMatrixUAndMarkovichNumbers()
    {
      /* the safe version
      for (int i = 0; i < dim; i++)
          for (int j = 0; j < dim; j++)
              U[i, j] = A[i, j];              
       */
      fixed (int* basisPin = this.A.basis)
      fixed (double* ACoeffPin = A.A.coeffs)
      fixed (int* slacksPin = A.A.slacksAndArtificials)
      fixed (double* uPin = U.Coeffs)
      {

        int nRegVars = A.A.nRegularVars;
        int* basis = basisPin;
        int* basisEnd = basis + dim;
        double* uStart = uPin;
        double* uEnd = uPin + dim * dim;
        for (; basis < basisEnd; basis++, uStart++)
        {
          int j = *basis;//column
          double* u = uStart;
          if (j < nRegVars)
          {
            double* a = ACoeffPin + *basis;
            for (; u < uEnd; u += dim, a += nRegVars)
            {
              *u = *a;
            }
          }
          else
          {
            for (; u < uEnd; u += dim)
              *u = 0;

            int placeOfOne = *(slacksPin + j - nRegVars);
            *(uStart + placeOfOne * dim) = 1;
          }
        }
      }
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    unsafe void CalculateInitialFactorization()
    {

      fixed (double* uPin = U.Coeffs)
      {
        double* uPivot = uPin;
        for (int k = 0; k < A.NumberOfRows; k++, uPivot += dim + 1)
        {

          int pivotRow = FindPivot(k);

          Contract.Assume(k < this.dim);

          if (pivotRow == -1)
          {
            failure = true;
            return;
          }

          if (pivotRow != k)
            SwapRows(k, pivotRow);

          Contract.Assert(k <= this.dim); // SwapRows may have modified 

          double pivot = *uPivot;

          Contract.Assume((double)pivot != 0.0d); // F: need quantified invariants

          double[] column = CreateLMatrix(k, pivot);

          if (!LIsUnitMatrix(column))
            this.LP.Add(new LowerTriangEtaMatrix(k, column));

          //divide k-th row by the pivot
          DividePivotRowByPivot(k, pivot);

          //substract the multiple of the k-th row from the lower rows
          SubstractTheMultipleOfThePivotRowFromLowerRows(k);

        }
      }

    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    unsafe void SubstractTheMultipleOfThePivotRowFromLowerRows(int k)
    {

      /* the safe version
       * for (int i = k + 1; i < dim; i++) {
          double t = -U[i, k];
          U[i, k] = 0;
          if (t != 0) {
              double max = 0;
              int markovitzNumber = 0;
              for (int j = k + 1; j < dim; j++) {
                  double m = (U[i, j] += t * U[k, j]);
                  if (m != 0) {
                      markovitzNumber++;
                      max = Math.Max(max, Math.Abs(m));
                  }
              }
              p[i] = markovitzNumber;
          //    rowMax[i]=max;
          }
      }
        */

      fixed (double* uPin = U.Coeffs)
      {
        double* uPivotRowStart = uPin + dim * k + k + 1;
        double* uPivotRowEnd = uPin + dim * (k + 1);
        double* iRow = uPivotRowStart + dim - 1;
        double* iRowEnd = uPin + dim * dim;
        for (; iRow < iRowEnd; iRow += k)
        {
          double t = *iRow;
          if ((double)t != 0)
          {
            *iRow++ = 0;
            double* uPivotRow = uPivotRowStart;
            double max = 0;
            int markovitzNumber = 0;
            for (; uPivotRow < uPivotRowEnd; uPivotRow++, iRow++)
            {
              double m = (*iRow -= t * (*uPivotRow));
              if ((double)m != 0)
              {
                markovitzNumber++;
                max = Math.Max(max, Math.Abs(m));
              }
            }

          }
          else
            iRow += dim - k;
        }
      }
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    unsafe private void DividePivotRowByPivot(int k, double pivot)
    {
      Contract.Requires((double)pivot != 0.0d);

      /* the safe version
        if (pivot != 1) {
            U[k, k] = 1;
            for (int j = k + 1; j < dim; j++)
                U[k, j] /= pivot;
        }
        */
      fixed (double* uPin = this.U.Coeffs)
        if ((double)pivot != 1)
        {
          double* u = uPin + k * (dim + 1);
          double* uEnd = uPin + (k + 1) * dim;
          *u = 1;
          u++;
          for (; u < uEnd; u++)
            (*u) /= pivot;
        }

    }

    unsafe private static bool LIsUnitMatrix(double[] column)
    {
      fixed (double* colPin = column)
      {
        double* col = colPin;
        double* colEnd = col + column.Length;

        bool unitMatrix = (double)*col == 1;

        if (unitMatrix)
        {
          col++;
          for (; col < colEnd && unitMatrix; col++)
            if ((double)*col != 0)
              unitMatrix = false;
        }
        return unitMatrix;
      }
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    unsafe private double[] CreateLMatrix(int k, double pivot)
    {

      Contract.Requires(k <= dim);
      Contract.Requires(((double)pivot) != 0.0d);

      /*the safe version
         double[] column = new double[dim - k];
        column[0] = 1.0 / pivot;
        for (int i = 1; i < column.Length; i++)
            column[i] = -U[k + i, k] / pivot;
        return column;
        */
      double[] column = new double[dim - k];
      fixed (double* colPin = column)
      fixed (double* uPin = U.Coeffs)
      {
        double* col = colPin;
        double* colEnd = col + dim - k;
        *col = 1.0 / pivot;
        col++;
        double* u = uPin + dim * (k + 1) + k;
        for (; col < colEnd; col++, u += dim)
          *col = -(*u) / pivot;
      }
      return column;
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    unsafe private void SwapRows(int k, int pivotRow)
    {
      Contract.Requires(this.dim > k);
      Contract.Requires(this.dim > pivotRow);

      /* the safe version
      this.LP.Add(new TranspositionMatrix(dim, k, pivotRow));

      //swap k-th and pivotRow rows in U
      //start swapping from the k-th column since we have zeroes in the first k columns
      for (int j = k; j < dim; j++) {
          double t = U[pivotRow, j];
          U[pivotRow, j] = U[k, j];
          U[k, j] = t;
      }
       */

      this.LP.Add(new TranspositionMatrix(dim, k, pivotRow));

      //swap k-th and pivotRow rows in U
      //start swapping from the k-th column since we have zeroes in the first k columns
      fixed (double* uPin = this.U.Coeffs)
      {
        double* uPivotRow = uPin + pivotRow * dim + k;
        double* kRow = uPin + k * (dim + 1);
        double* kRowEnd = uPin + dim * (k + 1);
        for (; kRow < kRowEnd; kRow++, uPivotRow++)
        {
          double t = *uPivotRow;
          *uPivotRow = *kRow;
          *kRow = t;
        }
      }
    }


    private int FindPivot(int k)
    {
      Contract.Requires(k >= 0);

      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < this.dim);

      return FindLargestPivot(k);
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    unsafe int FindLargestPivot(int k)
    {
      Contract.Requires(k >= 0);

      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < this.dim);
        
      double maxPivot = 0;
      int pivotRow = -1;
      fixed (double* uPin = this.U.Coeffs)
      {
        double* u = uPin + (dim + 1) * k;
        for (int i = k; i < dim; u += dim, i++)
        {
          double d = *u;
          if (d < 0)
            d = -d;
          if (d > maxPivot)
          {
            pivotRow = i;
            maxPivot = d;
          }
        }
      }
      return pivotRow;
    }

    internal override void Solve_yBEquals_cB(double[] y)
    {
      /*
       * We have LB=UE or B =L(-1)UE. We have yB=c or y=cB(-1)=cE(-1)(U-1)L. 
       * First we find cE(-1)=y, then yU(-1)=y, and then y=yL 
       */
      //solving yE=cB or yE1...Ek=cB
      for (int i = this.etaList.Count - 1; i >= 0; i--)
        etaList[i].SolveLeftSystem(y);

      //solving xU=y, and putting the answer into y
      this.U.SolveLeftSystem(y);
      Vector v = new Vector(y);
      for (int i = LP.Count - 1; i > -1; i--)
        v *= LP[i];//will update coefficient of v, that is y

    }

    internal override void Solve_BdEqualsa(double[] a)
    {
      // We have LB=UE or B =L(-1)UE.  We need to solve L(-1)UEd=a, or UEd=La
      //calculating La
      foreach (Matrix m in LP)
        a = m * a;
      //solving Ud=r
      U.SolveRightSystem(a);
      //solving Ed=d
      for (int i = 0; i < this.etaList.Count; i++)
        etaList[i].SolveRightSystem(a);

    }

    internal override void AddEtaMatrix(EtaMatrix e)
    {
      this.etaList.Add(e);
    }

    internal static Factorization Create(Matrix A, UMatrix U)
    {
      Contract.Requires(A != null);
      Contract.Requires(U != null);

      StandardFactorization f = new StandardFactorization(A, U);
      if (f.failure)
        return null;
      return f;
    }
  }
}

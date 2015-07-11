// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        private void ObjectInvariant()
        {
            Contract.Invariant(_dim >= 0);
            Contract.Invariant(_A != null);
            Contract.Invariant(_LP != null);
            Contract.Invariant(_etaList != null);
            Contract.Invariant(_U != null);
        }
        #endregion

        private readonly BMatrix _A;
        private readonly int _dim; //A is a dimXdim matrix
        private readonly List<Matrix> _LP = new List<Matrix>(); //it is the list of matrix L, P
        private readonly List<EtaMatrix> _etaList = new List<EtaMatrix>(); //it is the list of matrix eta matrices
                                                                          //int[] q;
                                                                          // double pivotEpsilon = 1.0e-1;
        private readonly UMatrix _U;
        private bool _failure = false;

        /// <summary>
        /// create the initial factorization of the form Ln*Pn*...*L1*P1*A=Un-1*...U0
        /// </summary>
        /// <param name="APar"></param>
        internal StandardFactorization(Matrix APar, UMatrix Upar)
        {
            Contract.Requires(APar != null);
            Contract.Requires(Upar != null);

            _A = (BMatrix)APar;
            _dim = _A.NumberOfColumns;
            _U = Upar;
            InitMatrixUAndMarkovichNumbers();
            CalculateInitialFactorization();

#if DEBUGGLEE
         //   if(calls>=63)
       //     CheckFactorization();
#endif
        }

        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        unsafe private void InitMatrixUAndMarkovichNumbers()
        {
            /* the safe version
            for (int i = 0; i < dim; i++)
                for (int j = 0; j < dim; j++)
                    U[i, j] = A[i, j];              
             */
            fixed (int* basisPin = _A.basis)
            fixed (double* ACoeffPin = _A.A.coeffs)
            fixed (int* slacksPin = _A.A.slacksAndArtificials)
            fixed (double* uPin = _U.Coeffs)
            {
                int nRegVars = _A.A.nRegularVars;
                int* basis = basisPin;
                int* basisEnd = basis + _dim;
                double* uStart = uPin;
                double* uEnd = uPin + _dim * _dim;
                for (; basis < basisEnd; basis++, uStart++)
                {
                    int j = *basis;//column
                    double* u = uStart;
                    if (j < nRegVars)
                    {
                        double* a = ACoeffPin + *basis;
                        for (; u < uEnd; u += _dim, a += nRegVars)
                        {
                            *u = *a;
                        }
                    }
                    else
                    {
                        for (; u < uEnd; u += _dim)
                            *u = 0;

                        int placeOfOne = *(slacksPin + j - nRegVars);
                        *(uStart + placeOfOne * _dim) = 1;
                    }
                }
            }
        }

#if DEBUGGLEE
        internal void CheckFactorization(Matrix A) {
            Matrix ls = A;  
            foreach (Matrix lp in this.LP)
                ls = lp * ls;

            Matrix f = U;
            foreach (Matrix e in this.etaList)
                f = f * e;

            double dist = Matrix.Dist(ls, f);
            Console.WriteLine("dist={0}", dist);
        }

        double det() {
            double r = 1;
            for (int i = 0; i < this.dim; i++)
                r *= U[i, i];
            foreach (EtaMatrix e in this.etaList) {
                int j = e.EtaIndex;
                r *= e[j, j];
            }
            return r;
        }
#endif
        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        private unsafe void CalculateInitialFactorization()
        {
            fixed (double* uPin = _U.Coeffs)
            {
                double* uPivot = uPin;
                for (int k = 0; k < _A.NumberOfRows; k++, uPivot += _dim + 1)
                {
                    int pivotRow = FindPivot(k);

                    Contract.Assume(k < _dim);

                    if (pivotRow == -1)
                    {
                        _failure = true;
                        return;
                    }

                    if (pivotRow != k)
                        SwapRows(k, pivotRow);

                    Contract.Assert(k <= _dim); // SwapRows may have modified 

                    double pivot = *uPivot;

                    Contract.Assume((double)pivot != 0.0d); // F: need quantified invariants

                    double[] column = CreateLMatrix(k, pivot);

                    if (!LIsUnitMatrix(column))
                        _LP.Add(new LowerTriangEtaMatrix(k, column));

                    //divide k-th row by the pivot
                    DividePivotRowByPivot(k, pivot);

                    //substract the multiple of the k-th row from the lower rows
                    SubstractTheMultipleOfThePivotRowFromLowerRows(k);
                }
            }
        }

        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        private unsafe void SubstractTheMultipleOfThePivotRowFromLowerRows(int k)
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

            fixed (double* uPin = _U.Coeffs)
            {
                double* uPivotRowStart = uPin + _dim * k + k + 1;
                double* uPivotRowEnd = uPin + _dim * (k + 1);
                double* iRow = uPivotRowStart + _dim - 1;
                double* iRowEnd = uPin + _dim * _dim;
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
                        iRow += _dim - k;
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
            fixed (double* uPin = _U.Coeffs)
              if ((double)pivot != 1)
            {
                double* u = uPin + k * (_dim + 1);
                double* uEnd = uPin + (k + 1) * _dim;
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
            Contract.Requires(k <= _dim);
            Contract.Requires(((double)pivot) != 0.0d);

            /*the safe version
               double[] column = new double[dim - k];
              column[0] = 1.0 / pivot;
              for (int i = 1; i < column.Length; i++)
                  column[i] = -U[k + i, k] / pivot;
              return column;
              */
            double[] column = new double[_dim - k];
            fixed (double* colPin = column)
            fixed (double* uPin = _U.Coeffs)
            {
                double* col = colPin;
                double* colEnd = col + _dim - k;
                *col = 1.0 / pivot;
                col++;
                double* u = uPin + _dim * (k + 1) + k;
                for (; col < colEnd; col++, u += _dim)
                    *col = -(*u) / pivot;
            }
            return column;
        }

        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        unsafe private void SwapRows(int k, int pivotRow)
        {
            Contract.Requires(_dim > k);
            Contract.Requires(_dim > pivotRow);

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

            _LP.Add(new TranspositionMatrix(_dim, k, pivotRow));

            //swap k-th and pivotRow rows in U
            //start swapping from the k-th column since we have zeroes in the first k columns
            fixed (double* uPin = _U.Coeffs)
            {
                double* uPivotRow = uPin + pivotRow * _dim + k;
                double* kRow = uPin + k * (_dim + 1);
                double* kRowEnd = uPin + _dim * (k + 1);
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
            Contract.Ensures(Contract.Result<int>() < _dim);

            return FindLargestPivot(k);
        }

        #region
#if DEBUGGLEE   // debugging routines
        private void CheckMatrix(UMatrix U,int k) {
            for (int i = 0; i < this.dim; i++)
                CheckMatrixRow(i, U,k);
            CheckBelowInTheColumn(U, k);

        }

        private void CheckBelowInTheColumn(UMatrix U, int k) {
            if(U[k,k]!=1)
                Console.WriteLine();
            bool allZero=true;
            for (int j = k + 1; j < dim&&allZero; j++)
                if (U[j, k] != 0)
                    allZero = false;
            if (allZero==false)
                Console.WriteLine();
        }

        private void CheckMatrixRow(int i, UMatrix U, int k) {
            bool allZero=false;
            int j=0;
            for (; j < dim; j++)
                if (U[i, j]!= 0) {
                    allZero = false;
                    break;
                }

            if (allZero)
                Console.WriteLine("all zero row");


        }

        private void CheckBasisOnZeroColumns(Matrix B) {
            for (int i = 0; i < B.NumberOfColumns; i++)
                CheckColumnOnZero( i,B);
        }

        private bool CheckColumnOnZero(int p, Matrix B) {
            for (int i = 0; i < B.NumberOfRows; i++)
                if (B[i, p] != 0)
                    return false;
            return true;

        }
#endif
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="k">looking for the pivot in the k-th column and rows k,...,n-1</param>
        ///// <param name="pivotI"></param>
        //int FindPivotCloseToOne(int k, double eps) {

        //    double distFromOne = Double.MaxValue;
        //    int pivotRow = -1;
        //    for (int i = k; i < dim; i++) {
        //        double d = Math.Abs(U[i, k]);
        //        if (d > eps) {
        //            d = Math.Abs(Math.Log(d));
        //            if (d < distFromOne) {
        //                distFromOne = d;
        //                pivotRow = i;
        //                if (d == 0)
        //                    break;
        //            }
        //        }
        //    }
        //    return pivotRow;
        //}

        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        private unsafe int FindLargestPivot(int k)
        {
            Contract.Requires(k >= 0);

            Contract.Ensures(Contract.Result<int>() >= -1);
            Contract.Ensures(Contract.Result<int>() < _dim);

            /* the safe version
            double maxPivot = 0;
            int minP = Int32.MaxValue;//number of zeroes in the row
            int pivotRow = -1;
            for (int i = k; i < dim; i++) {
                double d = Math.Abs(U[i, k]);///rowMax[i];
                if (d > maxPivot) {
                    minP = markovitzNumbers[i];
                    pivotRow = i;
                    maxPivot = d;
                } else if (d == maxPivot && d > 0) {
                    int r = markovitzNumbers[i];//markovitz number
                    if (r < minP) {
                        pivotRow = i;
                        minP = r;
                    }
                }
            }
             */

            double maxPivot = 0;
            int pivotRow = -1;
            fixed (double* uPin = _U.Coeffs)
            {
                double* u = uPin + (_dim + 1) * k;
                for (int i = k; i < _dim; u += _dim, i++)
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
        /*
        private void InitMarkowitzNumbers() {
            markovitzNumbers = new int[dim]; 
            for (int i = 0; i < dim; i++)
                for (int j = 0; j < A.NumberOfColumns; j++)
                    if (A[i, j]!=0) 
                        markovitzNumbers[i]++;                    
        }
        */
        internal override void Solve_yBEquals_cB(double[] y)
        {
            /*
             * We have LB=UE or B =L(-1)UE. We have yB=c or y=cB(-1)=cE(-1)(U-1)L. 
             * First we find cE(-1)=y, then yU(-1)=y, and then y=yL 
             */
            //solving yE=cB or yE1...Ek=cB
            for (int i = _etaList.Count - 1; i >= 0; i--)
                _etaList[i].SolveLeftSystem(y);

            //solving xU=y, and putting the answer into y
            _U.SolveLeftSystem(y);
            Vector v = new Vector(y);
            for (int i = _LP.Count - 1; i > -1; i--)
                v *= _LP[i];//will update coefficient of v, that is y
        }

        internal override void Solve_BdEqualsa(double[] a)
        {
            // We have LB=UE or B =L(-1)UE.  We need to solve L(-1)UEd=a, or UEd=La
            //calculating La
            foreach (Matrix m in _LP)
                a = m * a;
            //solving Ud=r
            _U.SolveRightSystem(a);
            //solving Ed=d
            for (int i = 0; i < _etaList.Count; i++)
                _etaList[i].SolveRightSystem(a);
        }

        internal override void AddEtaMatrix(EtaMatrix e)
        {
            _etaList.Add(e);
        }

        internal static Factorization Create(Matrix A, UMatrix U)
        {
            Contract.Requires(A != null);
            Contract.Requires(U != null);

            StandardFactorization f = new StandardFactorization(A, U);
            if (f._failure)
                return null;
            return f;
        }
    }
}

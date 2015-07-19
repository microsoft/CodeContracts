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
    /// This matrix has ones on the diagonal and zeros under the diagonal
    /// </summary>
    public class UMatrix : FullMatrix
    {
        internal UMatrix(int dim)
            : base(dim, dim)
        {
            Contract.Requires(dim >= 0);
        }

        public int Dim
        {
            get { return this.NumberOfRows; }
            set
            {
                Contract.Requires(value >= 0);

                this.NumberOfRows = this.NumberOfColumns = value;
                this.Coeffs = new double[value * value];
            }
        }

        public UMatrix() { }

        /// <summary>
        /// solves the system x*this=y and puts the solution into y
        /// 
        /// </summary>
        /// <param name="y">at the entrance y represents the right side, and y[0] has the correct value already</param>
        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        unsafe public void SolveLeftSystem(double[] y)
        {
            /*//the "safe" version
            for (int k = 1; k < this.NumberOfRows; k++) {
                double r = 0;
                for (int j = 0; j < k; j++)
                    r += y[j] * this[j, k];
                y[k] -= r;
            }
             */
            int n = NumberOfColumns;
            fixed (double* pinCoeffs = Coeffs)
            fixed (double* pinY = y)
            {
                double* colStart = pinCoeffs + 1;
                double* colEnd = pinCoeffs + n;
                double* ypEnd = pinY + 1;
                for (; colStart < colEnd; ypEnd++)
                {
                    double* col = colStart++;
                    double* yp = pinY;
                    double r = 0;
                    for (; yp < ypEnd; col += n, yp++)
                        r += (*yp) * (*col);
                    (*yp) -= r;
                }
            }
        }
        /// <summary>
        /// solves the system this*x=y and puts the solution into y
        /// </summary>
        /// <param name="y"></param>
        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        unsafe public void SolveRightSystem(double[] y)
        {
            /*//the safe version
            int n = this.NumberOfRows;
            for (int k = n-2; k >=0; k--) {
                double r = 0;
                for (int j = k + 1; j < n; j++)
                    r += y[j] * this[k, j];
                y[k] -= r;
            }
            */
            fixed (double* pinCoeffs = Coeffs)
            fixed (double* pinY = y)
            {
                int n = this.NumberOfRows;
                double* yEnd = pinY + n;
                double* yStart = yEnd - 1;
                double* rowStart = pinCoeffs + n * (n - 1) - 1;
                for (; rowStart >= pinCoeffs; rowStart -= n + 1)
                {
                    double r = 0;
                    double* yp = yStart;
                    double* row = rowStart;
                    for (; yp < yEnd; yp++, row++)
                        r += (*yp) * (*row);
                    *((yStart--) - 1) -= r;
                }
            }
        }
    }
}

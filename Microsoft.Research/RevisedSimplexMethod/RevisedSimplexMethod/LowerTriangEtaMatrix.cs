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
    /// This kind of matrix is a sum of the unit matrix and a lower triangular matrix with non-zero elements only in one column.
    /// </summary>
    public class LowerTriangEtaMatrix : Matrix
    {
        #region ObjectInvariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_dim >= 0);
            Contract.Invariant(_etaColumnIndex >= 0);
        }

        #endregion

        public LowerTriangEtaMatrix() { }

        public LowerTriangEtaMatrix(int etaColIndex, double[] col)
        {
            Contract.Requires(etaColIndex >= 0);
            Contract.Requires(col != null);

            this.EtaColumnIndex = etaColIndex;
            this.EtaColumn = col;
            _dim = col.Length + etaColIndex;
        }

        private double[] _column;

        /// <summary>
        /// the first element of the column starts at the diagonal
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public double[] EtaColumn
        {
            get
            {
                Contract.Ensures(Contract.Result<double[]>() != null);

                return _column;
            }
            set
            {
                Contract.Requires(value != null);
                _column = value;
            }
        }

        private int _dim;

        public int Dim
        {
            get { return _dim; }
            set
            {
                Contract.Requires(value >= 0);
                _dim = value;
            }
        }

        private int _etaColumnIndex;

        public int EtaColumnIndex
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);

                return _etaColumnIndex;
            }
            set
            {
                Contract.Requires(value >= 0);

                _etaColumnIndex = value;
            }
        }

        public override int NumberOfRows
        {
            get { return Dim; }
            set
            {
#if DEBUGGLEE
                throw new Exception("The method or operation is not implemented.");
#endif
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)")]
        public override int NumberOfColumns
        {
            get { return Dim; }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public override double this[int i, int j]
        {
            get
            {
                if (j == this.EtaColumnIndex)
                {
                    if (i < j)
                        return 0;
                    return this.EtaColumn[i - j];
                }
                return i == j ? 1 : 0;
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        unsafe static public double[] operator *(LowerTriangEtaMatrix l, double[] a)
        {
            Contract.Requires(l != null);
            Contract.Requires(a != null);
            Contract.Ensures(Contract.Result<double[]>() != null);

            /*
            //the safe version
            int k = L.EtaColumnIndex;
            double t = a[k];
            double[] column = L.EtaColumn;
            a[k] = column[0] * t;
            for (int i = k + 1, j = 1; i < a.Length; i++, j++)
                a[i] += column[j] * t;
            return a; 
            */

            int k = l.EtaColumnIndex;
            fixed (double* aPinned = a)
            {
                double* ap = aPinned + k;
                double* apEnd = aPinned + a.Length;
                double t = *ap;
                double[] column = l.EtaColumn;
                fixed (double* colPinned = l.EtaColumn)
                {
                    double* col = colPinned;
                    (*ap++) = (*col++) * t;
                    for (; ap < apEnd; ap++, col++)
                        (*ap) += (*col) * t;
                }
            }
            return a;
        }
    }
}

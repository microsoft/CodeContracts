// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Glee.Optimization
{
    /// <summary>
    /// This kind of matrix is a sum of the unit matrix and a lower triangular matrix with non-zero elements only in one column.
    /// The matrix has ones everywhere on the diagonal
    /// </summary>
    public class UpperTriangEtaMatrix : Matrix
    {
        #region Object Invariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_dim >= 0);
        }
        #endregion

        public UpperTriangEtaMatrix() { }

        public UpperTriangEtaMatrix(double[] col, int dim)
        {
            Contract.Requires(col != null);
            Contract.Requires(dim >= 0);

            this.EtaColumnIndex = col.Length + 1; //the last element of the column is zero and therefor is not set
            this.EtaColumn = col;
            this.NumberOfColumns = this.NumberOfRows = dim;
        }

        private double[] _column;

        /// <summary>
        /// the first element of the column starts at the diagonal
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public double[] EtaColumn
        {
            get { return _column; }
            set { _column = value; }
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
            get { return _etaColumnIndex; }
            set { _etaColumnIndex = value; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public override int NumberOfRows
        {
            get { return Dim; }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
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
                    if (i > j)
                        return 0;
                    else if (j == i)
                        return 1;
                    return this.EtaColumn[i];
                }
                return i == j ? 1 : 0;
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
    }
}

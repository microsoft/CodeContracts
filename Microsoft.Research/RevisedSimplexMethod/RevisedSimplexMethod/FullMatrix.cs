// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Glee.Optimization
{
    public class FullMatrix : Matrix
    {
        #region ObjectInvariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_numberOfRows >= 0);
            Contract.Invariant(_numberOfColumns >= 0);
            Contract.Invariant(_coeffs != null);
        }

        #endregion

        private double[] _coeffs;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public double[] Coeffs
        {
            get
            {
                Contract.Ensures(Contract.Result<double[]>() != null);
                return _coeffs;
            }
            set
            {
                Contract.Requires(value != null);

                _coeffs = value;
                if (this.NumberOfRows != 0)
                    this.NumberOfColumns = _coeffs.Length / this.NumberOfRows;
            }
        }

        private int _numberOfRows;
        public override int NumberOfRows
        {
            get { return _numberOfRows; }
            set
            {
                _numberOfRows = value;
            }
        }

        private int _numberOfColumns;

        public override int NumberOfColumns
        {
            get
            {
                return _numberOfColumns;
            }
            set
            {
                _numberOfColumns = value;
            }
        }

        [Pure]
        public FullMatrix() { }
        public FullMatrix(int numOfRows, double[] coffs)
        {
            Contract.Requires(numOfRows > 0);
            Contract.Requires(coffs != null);

            Contract.Ensures(this.NumberOfRows == numOfRows);
            Contract.Ensures(this.NumberOfColumns == this.Coeffs.Length / this.NumberOfRows);

            this.NumberOfRows = numOfRows;
            this.Coeffs = coffs;
            this.NumberOfColumns = this.Coeffs.Length / this.NumberOfRows;
            Contract.Assert(this.NumberOfColumns >= 0);
        }

        public FullMatrix(int numOfRows, int numOfColms)
        {
            Contract.Requires(numOfRows >= 0);
            Contract.Requires(numOfColms >= 0);

            Contract.Ensures(this.NumberOfRows == numOfRows);
            Contract.Ensures(this.NumberOfColumns == numOfColms);

            this.NumberOfRows = numOfRows;
            this.NumberOfColumns = numOfColms;
            this.Coeffs = new double[numOfRows * NumberOfColumns];
        }

        public override double this[int i, int j]
        {
            get
            {
                // F: it's a precondition now
                //System.Diagnostics.Debug.Assert(i < numberOfRows&& j < numberOfColumns&&i>=0&&j>=0,String.Format("i=={0},j=={1}", i,j));

                return Coeffs[i * NumberOfColumns + j];
            }
            set
            {
                // F: it's a precondition now
                //System.Diagnostics.Debug.Assert(i < numberOfRows && j < numberOfColumns && i >= 0 && j >= 0);

                Coeffs[i * NumberOfColumns + j] = value;
            }
        }
    }
}

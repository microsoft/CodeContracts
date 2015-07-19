// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Glee.Optimization
{
    public class ColumnVector : Matrix
    {
        #region Object Invariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_matrix != null);
            Contract.Invariant(_column >= 0);
            Contract.Invariant(_column < _matrix.NumberOfColumns);
        }
        #endregion

        private readonly Matrix _matrix;
        private readonly int _column;
        internal ColumnVector(Matrix m, int col)
        {
            Contract.Requires(m != null);
            Contract.Requires(0 <= col);
            Contract.Requires(col < m.NumberOfColumns);

            _matrix = m;
            _column = col;
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)")]
        public override int NumberOfRows
        {
            get
            {
                return _matrix.NumberOfRows;
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)")]
        public override int NumberOfColumns
        {
            get
            {
                return 1;
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public override double this[int i, int j]
        {
            get
            {
                return _matrix[i, _column];
            }
            set
            {
                Contract.Assume(i == 0);  // F: this is a special case for a ColumnVector

                _matrix[i, _column] = value;
            }
        }

        public double this[int i]
        {
            get
            {
                Contract.Requires(i >= 0);

                return _matrix[i, _column];
            }
            set
            {
                Contract.Requires(i >= 0);
                _matrix[i, _column] = value;
            }
        }

        public static double operator *(Vector a, ColumnVector b)
        {
            Contract.Requires(a != null);
            Contract.Requires(b != null);
            Contract.Requires(a.Length == b.NumberOfRows);

            double ret = 0;

            for (int i = 0; i < a.Length; i++)
                ret += a[i] * b[i];

            return ret;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1818:DoNotConcatenateStringsInsideLoops")]
        public override string ToString()
        {
            string ret = "{";
            for (int i = 0; i < this.NumberOfRows; i++)
                ret += this[i] + ",";
            ret += "}";
            return ret;
        }
    }
}

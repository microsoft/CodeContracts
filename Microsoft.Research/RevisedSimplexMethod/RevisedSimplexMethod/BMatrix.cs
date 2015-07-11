// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Glee.Optimization
{
    /// <summary>
    /// Represents the B matrix of the method: the matrix defined by the basis vector and matrix A.
    /// </summary>
    internal class BMatrix : Matrix
    {
        readonly internal int[] basis;
        readonly internal ExtendedConstraintMatrix A;

        internal BMatrix(int[] bas, ExtendedConstraintMatrix Am)
        {
            this.A = Am;
            this.basis = bas;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)")]
        public override int NumberOfRows
        {
            get
            {
                return basis.Length;
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
                return basis.Length;
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
                Contract.Assert(i >= 0);
                Contract.Assume(i < A.NumberOfRows);

                Contract.Assume(basis[j] >= 0);
                Contract.Assume(basis[j] < A.NumberOfColumns);

                return A[i, basis[j]];
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
    }
}

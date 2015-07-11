// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Glee.Optimization
{
    public class UnitMatrix : Matrix
    {
        #region Object Invariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_dimension >= 0);
        }
        #endregion

        public UnitMatrix(int dim)
        {
            Contract.Requires(dim >= 0);

            _dimension = dim;
        }

        private int _dimension;

        public int Dimension
        {
            get { return _dimension; }
            set
            {
                Contract.Requires(value >= 0);
                _dimension = value;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public override int NumberOfRows
        {
            get { return Dimension; }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public override int NumberOfColumns
        {
            get { return Dimension; }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public override double this[int i, int j]
        {
            get
            {
                return i == j ? 1 : 0;
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
    }
}

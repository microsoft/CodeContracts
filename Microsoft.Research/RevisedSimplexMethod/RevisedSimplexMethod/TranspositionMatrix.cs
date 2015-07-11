// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Glee.Optimization
{
    public class TranspositionMatrix : Matrix
    {
        #region ObjectInvariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_dimension >= 0);
        }

        #endregion

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

        private int _elementI;

        public int ElementI
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);

                return _elementI;
            }
            set
            {
                Contract.Requires(value >= 0);

                _elementI = value;
            }
        }

        private int _elementJ;

        public int ElementJ
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return _elementJ;
            }
            set
            {
                Contract.Requires(value >= 0);
                _elementJ = value;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public override int NumberOfRows
        {
            get { return this.Dimension; }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public override int NumberOfColumns
        {
            get { return this.Dimension; }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public override double this[int i, int j]
        {
            get
            {
                if (i == ElementI)
                {
                    if (j == ElementJ)
                        return 1;
                    return 0;
                }
                if (i == ElementJ)
                {
                    if (j == ElementI)
                        return 1;
                    return 0;
                }

                if (j == i)
                    return 1;
                return 0;
            }


            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public TranspositionMatrix(int dim, int i, int j)
        {
            Contract.Requires(dim >= 0);
            Contract.Requires(i >= 0);
            Contract.Requires(dim > i);
            Contract.Requires(dim > j);

            this.Dimension = dim;
            this.ElementI = i;
            _elementJ = j;
        }

        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        unsafe static public double[] operator *(TranspositionMatrix b, double[] a)
        {
            Contract.Requires(a != null);
            Contract.Requires(b != null);
            Contract.Ensures(Contract.Result<double[]>() != null);

            fixed (double* ap = a)
            {
                double* ip = ap + b.ElementI;
                double* jp = ap + b.ElementJ;
                double t = *ip;
                *ip = *jp;
                *jp = t;
            }
            return a;
        }
    }
}

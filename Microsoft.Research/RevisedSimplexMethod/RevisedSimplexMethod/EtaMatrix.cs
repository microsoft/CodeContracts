// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Glee.Optimization
{
    /// <summary>
    /// Differs from the unit matrix in one arbitrary column 
    /// </summary>
#if DEBUGGLEE
    public
#else
    internal
#endif
 class EtaMatrix : Matrix
    {
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_dim >= 0);
        }

        private readonly int _dim;

        internal EtaMatrix(int etaIndex, double[] d)
        {
            Contract.Requires(d != null);

            this.EtaIndex = etaIndex;
            this.EtaColumn = d;
            _dim = this.EtaColumn.Length;
        }

        private double[] _column;

        public double[] EtaColumn
        {
            get { return _column; }
            set { _column = value; }
        }
        private int _etaIndex;

        public int EtaIndex
        {
            get { return _etaIndex; }
            set { _etaIndex = value; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public override int NumberOfRows
        {
            get
            {
                return _dim;
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public override int NumberOfColumns
        {
            get
            {
                return _dim;
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
                if (j == EtaIndex)
                    return EtaColumn[i];
                return i == j ? 1 : 0;
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        /// <summary>
        /// solve x*this=y and puts the answer into y
        /// </summary>
        /// <param name="eta"></param>
        /// <param name="y"></param>
        unsafe internal void SolveLeftSystem(double[] y)
        {
            //every x[i]=y[i] for i different from EtaIndex
            //We only need to find x[EtaIndex]. We have sum(Column[i]x[i])=y[EtaIndex], therefor x[EtaIndex]=(y[EtaIndex]-sum(Column(i)x(i):i!=EtaIndex)/Column(EtaIndex)
            /* the safe version
            double r = 0;
            int i = 0;
            for (; i < this.EtaIndex; i++)
                r += EtaColumn[i] * y[i];
            i++;
            for (; i < dim; i++)
                r += EtaColumn[i] * y[i];

            y[EtaIndex] = (y[EtaIndex] - r) / EtaColumn[EtaIndex];
            */
            fixed (double* yPin = y)
            fixed (double* ePin = EtaColumn)
            {
                double* yp = yPin;
                double* yAtIndex = yp + EtaIndex;
                double* yEnd = yp + _dim;
                double* eta = ePin;
                double r = 0;
                for (; yp < yAtIndex; yp++, eta++)
                    r += *eta * *yp;
                yp++; eta++;
                for (; yp < yEnd; yp++, eta++)
                    r += *eta * *yp;

                Contract.Assume((double)*(ePin + EtaIndex) != (double)0.0d);

                *yAtIndex = (*yAtIndex - r) / (*(ePin + EtaIndex));
            }
        }



        /// <summary>
        /// solve this*x=y and puts the answer into y
        /// </summary>
        /// <param name="eta"></param>
        /// <param name="y"></param>
        unsafe internal void SolveRightSystem(double[] y)
        {
            /* the safe version
            double f = y[EtaIndex];
            double t = y[EtaIndex] /= this.EtaColumn[EtaIndex];
            for (int i = 0; i < EtaIndex; i++) //x[i]+Column[i]*f=y[i], x[i]=y[i]-Column[i]*f
                y[i] -= t * EtaColumn[i];

            for (int i = EtaIndex + 1; i < dim; i++)
                y[i] -= t * EtaColumn[i];
            */
            fixed (double* yPin = y)
            fixed (double* ePin = EtaColumn)
            {
                double* yAtIndex = yPin + EtaIndex;
                double f = *yAtIndex;

                Contract.Assume((double)*(ePin + EtaIndex) != (double)0.0d);

                double t = *yAtIndex /= *(ePin + EtaIndex);
                double* yp = yPin;
                double* e = ePin;
                double* yEnd = yPin + _dim;
                for (; yp < yAtIndex; yp++, e++) //x[i]+Column[i]*f=y[i], x[i]=y[i]-Column[i]*f
                    *yp -= t * (*e);

                yp++; e++;

                for (; yp < yEnd; yp++, e++)
                    *yp -= t * (*e);
            }
        }
    }
}

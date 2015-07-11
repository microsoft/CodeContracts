// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Glee.Optimization
{
    [ContractClass(typeof(FactorizationContracts))]
    abstract internal class Factorization
    {
        static internal Factorization CreateFactorization(Matrix B, UMatrix U)
        {
            Contract.Requires(B != null);
            Contract.Requires(U != null);

            //if( System.Environment.GetEnvironmentVariable("BartelsGolub")=="on")
            //    return new BartelsGolubFactorization(B);
            return StandardFactorization.Create(B, U);
        }
        abstract internal void Solve_yBEquals_cB(double[] cB);
        abstract internal void Solve_BdEqualsa(double[] a);
        abstract internal void AddEtaMatrix(EtaMatrix e);
    }

    [ContractClassFor(typeof(Factorization))]
    internal abstract class FactorizationContracts : Factorization
    {
        internal override void Solve_yBEquals_cB(double[] cB)
        {
            Contract.Requires(cB != null);
        }

        internal override void Solve_BdEqualsa(double[] a)
        {
            Contract.Requires(a != null);
        }

        internal override void AddEtaMatrix(EtaMatrix e)
        {
            Contract.Requires(e != null);
        }
    }
}

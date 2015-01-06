// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

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
  abstract class FactorizationContracts : Factorization
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

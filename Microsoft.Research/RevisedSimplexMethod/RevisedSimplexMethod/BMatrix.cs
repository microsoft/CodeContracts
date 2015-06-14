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

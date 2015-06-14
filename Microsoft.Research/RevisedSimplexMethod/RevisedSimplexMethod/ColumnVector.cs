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
  public class ColumnVector : Matrix
  {

    #region Object Invariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(matrix != null);
      Contract.Invariant(column >= 0);
      Contract.Invariant(column < matrix.NumberOfColumns);
    }
    #endregion

    readonly Matrix matrix;
    readonly int column;
    internal ColumnVector(Matrix m, int col)
    {
      Contract.Requires(m != null);
      Contract.Requires(0 <= col);
      Contract.Requires(col < m.NumberOfColumns);

      this.matrix = m;
      this.column = col;
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)")]
    public override int NumberOfRows
    {
      get
      {
        return matrix.NumberOfRows;
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
        return matrix[i, column];
      }
      set
      {
        Contract.Assume(i == 0);  // F: this is a special case for a ColumnVector

        matrix[i, column] = value;

      }
    }

    public double this[int i]
    {
      get
      {
        Contract.Requires(i >= 0);

        return matrix[i, column];
      }
      set
      {
        Contract.Requires(i >= 0);
        matrix[i, column] = value;
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

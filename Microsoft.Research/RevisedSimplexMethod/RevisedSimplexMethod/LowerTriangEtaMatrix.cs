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
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Glee.Optimization
{
  /// <summary>
  /// This kind of matrix is a sum of the unit matrix and a lower triangular matrix with non-zero elements only in one column.
  /// </summary>
  public class LowerTriangEtaMatrix : Matrix
  {

    #region ObjectInvariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.dim >= 0);
      Contract.Invariant(this.etaColumnIndex >= 0);
    }

    #endregion

    public LowerTriangEtaMatrix() { }

    public LowerTriangEtaMatrix(int etaColIndex, double[] col)
    {
      Contract.Requires(etaColIndex >= 0);
      Contract.Requires(col != null);

      this.EtaColumnIndex = etaColIndex;
      this.EtaColumn = col;
      dim = col.Length + etaColIndex;
    }

    double[] column;

    /// <summary>
    /// the first element of the column starts at the diagonal
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
    public double[] EtaColumn
    {
      get
      {
        Contract.Ensures(Contract.Result<double[]>() != null);

        return column;
      }
      set
      {
        Contract.Requires(value != null);
        column = value;
      }
    }

    int dim;

    public int Dim
    {
      get { return dim; }
      set
      {
        Contract.Requires(value >= 0);
        dim = value;
      }
    }

    int etaColumnIndex;

    public int EtaColumnIndex
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return etaColumnIndex;
      }
      set
      {
        Contract.Requires(value >= 0);

        etaColumnIndex = value;
      }
    }

    public override int NumberOfRows
    {
      get { return Dim; }
      set
      {
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)")]
    public override int NumberOfColumns
    {
      get { return Dim; }
      set
      {
        throw new Exception("The method or operation is not implemented.");
      }
    }

    public override double this[int i, int j]
    {
      get
      {
        if (j == this.EtaColumnIndex)
        {
          if (i < j)
            return 0;
          return this.EtaColumn[i - j];
        }
        return i == j ? 1 : 0;
      }
      set
      {
        throw new Exception("The method or operation is not implemented.");
      }
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    unsafe static public double[] operator *(LowerTriangEtaMatrix l, double[] a)
    {
      Contract.Requires(l != null);
      Contract.Requires(a != null);
      Contract.Ensures(Contract.Result<double[]>() != null);

      /*
      //the safe version
      int k = L.EtaColumnIndex;
      double t = a[k];
      double[] column = L.EtaColumn;
      a[k] = column[0] * t;
      for (int i = k + 1, j = 1; i < a.Length; i++, j++)
          a[i] += column[j] * t;
      return a; 
      */

      int k = l.EtaColumnIndex;
      fixed (double* aPinned = a)
      {
        double* ap = aPinned + k;
        double* apEnd = aPinned + a.Length;
        double t = *ap;
        double[] column = l.EtaColumn;
        fixed (double* colPinned = l.EtaColumn)
        {
          double* col = colPinned;
          (*ap++) = (*col++) * t;
          for (; ap < apEnd; ap++, col++)
            (*ap) += (*col) * t;
        }
      }
      return a;
    }
  }
}

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
  /// This kind of matrix is a sum of the unit matrix and a lower triangular matrix with non-zero elements only in one column.
  /// The matrix has ones everywhere on the diagonal
  /// </summary>
  public class UpperTriangEtaMatrix : Matrix
  {

    #region Object Invariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(dim >= 0);
    }
    #endregion

    public UpperTriangEtaMatrix() { }

    public UpperTriangEtaMatrix(double[] col, int dim)
    {
      Contract.Requires(col != null);
      Contract.Requires(dim >= 0);

      this.EtaColumnIndex = col.Length + 1; //the last element of the column is zero and therefor is not set
      this.EtaColumn = col;
      this.NumberOfColumns = this.NumberOfRows = dim;
    }

    double[] column;

    /// <summary>
    /// the first element of the column starts at the diagonal
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
    public double[] EtaColumn
    {
      get { return column; }
      set { column = value; }
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
      get { return etaColumnIndex; }
      set { etaColumnIndex = value; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
    public override int NumberOfRows
    {
      get { return Dim; }
      set
      {
        throw new Exception("The method or operation is not implemented.");
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
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
          if (i > j)
            return 0;
          else if (j == i)
            return 1;
          return this.EtaColumn[i];
        }
        return i == j ? 1 : 0;
      }
      set
      {
        throw new Exception("The method or operation is not implemented.");
      }
    }
  }
}

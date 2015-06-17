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
  public class FullMatrix : Matrix
  {

    #region ObjectInvariant

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(numberOfRows >= 0);
      Contract.Invariant(numberOfColumns >= 0);
      Contract.Invariant(this.coeffs != null);
    }

    #endregion

    double[] coeffs;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
    public double[] Coeffs
    {
      get 
      {
        Contract.Ensures(Contract.Result<double[]>() != null);
        return coeffs; 
      }
      set
      {
        Contract.Requires(value != null);

        coeffs = value;
        if (this.NumberOfRows != 0)
          this.NumberOfColumns = this.coeffs.Length / this.NumberOfRows;
      }
    }

    int numberOfRows;
    public override int NumberOfRows
    {
      get { return numberOfRows; }
      set
      {
        numberOfRows = value;
      }
    }

    int numberOfColumns;

    public override int NumberOfColumns
    {
      get
      {
        return numberOfColumns;
      }
      set
      {
        this.numberOfColumns = value;
      }
    }

    [Pure]
    public FullMatrix() { }
    public FullMatrix(int numOfRows, double[] coffs)
    {
      Contract.Requires(numOfRows > 0);
      Contract.Requires(coffs != null);

      Contract.Ensures(this.NumberOfRows == numOfRows);
      Contract.Ensures(this.NumberOfColumns == this.Coeffs.Length / this.NumberOfRows);

      this.NumberOfRows = numOfRows;
      this.Coeffs = coffs;
      this.NumberOfColumns = this.Coeffs.Length / this.NumberOfRows;
      Contract.Assert(this.NumberOfColumns >= 0);
    }

    public FullMatrix(int numOfRows, int numOfColms)
    {
      Contract.Requires(numOfRows >= 0);
      Contract.Requires(numOfColms >= 0);

      Contract.Ensures(this.NumberOfRows == numOfRows);
      Contract.Ensures(this.NumberOfColumns == numOfColms);

      this.NumberOfRows = numOfRows;
      this.NumberOfColumns = numOfColms;
      this.Coeffs = new double[numOfRows * NumberOfColumns];
    }

    public override double this[int i, int j]
    {
      get
      {
        // F: it's a precondition now
        //System.Diagnostics.Debug.Assert(i < numberOfRows&& j < numberOfColumns&&i>=0&&j>=0,String.Format("i=={0},j=={1}", i,j));

        return Coeffs[i * NumberOfColumns + j];
      }
      set
      {
        // F: it's a precondition now
        //System.Diagnostics.Debug.Assert(i < numberOfRows && j < numberOfColumns && i >= 0 && j >= 0);

        Coeffs[i * NumberOfColumns + j] = value;
      }
    }
  }
}

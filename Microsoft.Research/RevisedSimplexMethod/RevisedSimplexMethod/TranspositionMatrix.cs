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
  public class TranspositionMatrix : Matrix
  {
    #region ObjectInvariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.dimension >= 0);
    }

    #endregion

    int dimension;

    public int Dimension
    {
      get { return dimension; }
      set
      {
        Contract.Requires(value >= 0);
        dimension = value;
      }
    }

    int elementI;

    public int ElementI
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return elementI;
      }
      set
      {
        Contract.Requires(value >= 0);

        elementI = value;
      }
    }

    int elementJ;

    public int ElementJ
    {
      get 
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return elementJ; 
      }
      set 
      {
        Contract.Requires(value >= 0);
        elementJ = value; 
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
      this.elementJ = j;
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

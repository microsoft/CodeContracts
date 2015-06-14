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
  public class Vector : Matrix
  {

    double[] coeffs;

    public double this[int i]
    {
      get
      {
        Contract.Requires(i >= 0);
        Contract.Requires(i < this.Length);

        return this.Coeffs[i];
      }
      set
      {
        Contract.Requires(i >= 0);
        Contract.Requires(i < this.Length);

        this.Coeffs[i] = value;
      }
    }

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
      }
    }

    public override int NumberOfRows
    {
      get { return 1; }
      set
      {
        // throw new Exception("The method or operation is not implemented.");
      }
    }

    public int Length
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() == this.Coeffs.Length);

        return this.Coeffs.Length;
      }
    }

    public override int NumberOfColumns
    {
      get { return coeffs.Length; }
      set
      {
        //                throw new Exception("The method or operation is not implemented.");
      }
    }

    public override double this[int i, int j]
    {
      get
      {
        return this.Coeffs[j];
      }
      set
      {
        this.Coeffs[j] = value;
      }
    }

    public Vector(double[] cfs)
    {
      Contract.Requires(cfs != null);
      this.Coeffs = cfs;
    }

    public Vector(int dim)
    {
      Contract.Requires(dim >= 0);

      Contract.Ensures(this.Length == dim);

      this.Coeffs = new double[dim];
    }

    public Vector() { }


    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    unsafe public static Vector operator *(Vector v, LowerTriangEtaMatrix ltm)
    {
      Contract.Requires(v != null);
      Contract.Requires(ltm != null);

      /* the safe version 
      double[] etaColumn = ltm.EtaColumn;
      int i = ltm.EtaColumnIndex;
      double r = 0;
      for (int k = 0; k < etaColumn.Length; k++)
          r += etaColumn[k] * v[i + k];
      v[i] = r;
      return v;
       */
      fixed (double* etaPin = ltm.EtaColumn)
      fixed (double* vPin = v.Coeffs)
      {
        int i = ltm.EtaColumnIndex;
        double* e = etaPin;
        double* eEnd = e + ltm.EtaColumn.Length;
        double* vp = vPin + i;
        double r = 0;
        for (; e < eEnd; vp++, e++)
          r += *e * *vp;
        *(vPin + i) = r;
        return v;
      }
    }

    public static Vector operator *(LowerTriangEtaMatrix lm, Vector v)
    {
      Contract.Requires(lm != null);
      Contract.Requires(v != null);

      Contract.Assume(0 < lm.EtaColumn.Length);

      int k = lm.EtaColumnIndex;

      Contract.Assume(k < v.Length);

      double t = v[k];
      double[] column = lm.EtaColumn;
      v[k] = column[0] * t;
      for (int i = k + 1, j = 1; i < v.Length; i++, j++)
        v[i] += column[j] * t;
      return v;
    }

    public static Vector operator *(Vector v, Matrix m)
    {
      Contract.Requires(v != null);
      Contract.Requires(m != null);

      LowerTriangEtaMatrix ltr = m as LowerTriangEtaMatrix;
      if (ltr != null)
        return v * ltr;

      TranspositionMatrix tr = m as TranspositionMatrix;
      if (tr != null)
        return v * tr;

      Contract.Assume(v.Length == m.NumberOfRows);

      Vector r = new Vector(m.NumberOfColumns);
      for (int i = 0; i < m.NumberOfColumns; i++)
      {
        double s = 0;
        for (int j = 0; j < v.Length; j++)
          s += v[j] * m[j, i];
        r[i] = s;
      }

      return r;
    }

    public static Vector operator *(Vector v, TranspositionMatrix tr)
    {
      Contract.Requires(v != null);
      Contract.Requires(tr != null);

      return tr * v;
    }

    public static Vector operator *(TranspositionMatrix tr, Vector v)
    {
      Contract.Requires(tr != null);
      Contract.Requires(v != null);

      Contract.Assume(tr.ElementI < v.Coeffs.Length);
      Contract.Assume(tr.ElementJ < v.Coeffs.Length);

      Contract.Assert(v.Coeffs != null);

      double[] cfs = v.Coeffs;
      double t = cfs[tr.ElementI];
      cfs[tr.ElementI] = cfs[tr.ElementJ];
      cfs[tr.ElementJ] = t;
      return v;
    }
  }
}

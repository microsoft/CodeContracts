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
  /// The base class for the hierarchy of matrix types
  /// </summary>
  [ContractClass(typeof(MatrixContracts))]
  public abstract class Matrix
  {

#if DEBUGGLEE
  

        static public  double Dist(Matrix a, Matrix b) {

            double d = 0;
            double t;
            for (int i = 0; i < a.NumberOfRows; i++) for (int j = 0; j < b.NumberOfColumns; j++)
                    if ((t = Math.Abs(a[i, j] - b[i, j])) > d)
                        d = t;
            return d;
        }
     
#endif

    abstract public int NumberOfRows
    {
      get;
      set;
    }


    abstract public int NumberOfColumns
    {
      get;
      set;
    }

    abstract public double this[int i, int j] { get; set; }

    static public double[] operator *(Matrix b, double[] a)
    {
      Contract.Requires(b != null);
      Contract.Requires(a != null);

      Contract.Ensures(Contract.Result<double[]>() != null);

      LowerTriangEtaMatrix L = b as LowerTriangEtaMatrix;
      if (L != null)
      {
        return L * a;
      }
      TranspositionMatrix P = b as TranspositionMatrix;
      if (P != null)
        return P * a;

      throw new NotImplementedException();
    }


    static public Matrix operator +(Matrix a, Matrix b)
    {
      //System.Diagnostics.Debug.Assert(a.NumberOfRows==b.NumberOfRows&&a.NumberOfColumns== b.NumberOfColumns);
      Contract.Requires(a != null);
      Contract.Requires(b != null);
      Contract.Requires(a.NumberOfRows > 0);
      Contract.Requires(a.NumberOfRows == b.NumberOfRows);
      Contract.Requires(a.NumberOfColumns == b.NumberOfColumns);

      Contract.Assume(a.NumberOfColumns > 0);

      FullMatrix ret = new FullMatrix(a.NumberOfRows, new double[a.NumberOfRows * a.NumberOfColumns]);
      for (int i = 0; i < a.NumberOfRows; i++)
      {
        for (int j = 0; j < a.NumberOfColumns; j++)
        {
          Contract.Assume(a.NumberOfColumns <= ret.NumberOfColumns); // F: cannot prove it becase it's quadratic

          ret[i, j] = a[i, j] + b[i, j];
        }
      }
      return ret;
    }
    static public Matrix operator -(Matrix a, Matrix b)
    {
      Contract.Requires(a != null);
      Contract.Requires(b != null);
      Contract.Requires(a.NumberOfRows > 0);
      Contract.Requires(a.NumberOfRows == b.NumberOfRows);
      Contract.Requires(a.NumberOfColumns == b.NumberOfColumns);

      //System.Diagnostics.Debug.Assert(a.NumberOfRows == b.NumberOfRows && a.NumberOfColumns == b.NumberOfColumns);
      FullMatrix ret = new FullMatrix(a.NumberOfRows, new double[a.NumberOfRows * a.NumberOfColumns]);
      for (int i = 0; i < a.NumberOfRows; i++)
        for (int j = 0; j < a.NumberOfColumns; j++)
        {
          Contract.Assume(a.NumberOfColumns <= ret.NumberOfColumns); // F: cannot prove it becase it's quadratic

          ret[i, j] = a[i, j] - b[i, j];
        }

      return ret;
    }

    static public Matrix operator *(Matrix a, double b)
    {
      Contract.Requires(a != null);
      Contract.Requires(a.NumberOfRows > 0);

      FullMatrix ret = new FullMatrix(a.NumberOfRows, new double[a.NumberOfRows * a.NumberOfColumns]);
      for (int i = 0; i < a.NumberOfRows; i++)
        for (int j = 0; j < a.NumberOfColumns; j++)
        {
          Contract.Assume(a.NumberOfColumns <= ret.NumberOfColumns); // F: cannot prove it becase it's quadratic

          ret[i, j] = b * a[i, j];
        }

      return ret;
    }

    static public Matrix operator *(double b, Matrix a)
    {
      Contract.Requires(a != null);
      Contract.Requires(a.NumberOfRows > 0);

      return a * b;
    }

    /// <summary>
    /// If one parameter is a Vector then it can be reused for the answer
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    static public Matrix operator *(Matrix a, Matrix b)
    {
      Contract.Requires(a != null);
      Contract.Requires(b != null);

      Contract.Requires(a.NumberOfRows > 0);

      Vector v = b as Vector;
      if (v != null)
      {
        Contract.Assume(a.NumberOfColumns == v.Length);

        TranspositionMatrix transposition = a as TranspositionMatrix;

        if (transposition != null)
          return v * transposition;
        else
        {

          LowerTriangEtaMatrix lm = a as LowerTriangEtaMatrix;
          if (lm != null)
            return lm * v;
          else
          {
            double[] cfs = new double[a.NumberOfRows];
            for (int i = 0; i < a.NumberOfRows; i++)
            {
              double r = 0;
              for (int j = 0; j < v.Length; j++)
                r += a[i, j] * v[j];
              cfs[i] = r;
            }
            return new Vector(cfs);
          }
        }
      }
      else
      {
        v = a as Vector;
        if (v != null)
          return v * b;
        else
        {
          Contract.Assert(a.NumberOfRows > 0);

          Contract.Assume(a.NumberOfColumns == b.NumberOfRows);

          FullMatrix ret = new FullMatrix(a.NumberOfRows, new double[a.NumberOfRows * b.NumberOfColumns]);
          for (int i = 0; i < a.NumberOfRows; i++)
            for (int j = 0; j < b.NumberOfColumns; j++)
            {
              double r = 0;
              for (int k = 0; k < a.NumberOfColumns; k++)
                r += a[i, k] * b[k, j];

              Contract.Assume(b.NumberOfColumns <= ret.NumberOfColumns);  // F: Cannot prove it because quadratic

              ret[i, j] = r;
            }

          return ret;
        }
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1818:DoNotConcatenateStringsInsideLoops")]
    public override string ToString()
    {
      string s = "";
      for (int i = 0; i < this.NumberOfRows; i++)
        if (i == 0)
          s = RowToString(i);
        else
          s += ",\n" + RowToString(i);
      return s;
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Double.ToString"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1818:DoNotConcatenateStringsInsideLoops")]
    string RowToString(int i)
    {
      Contract.Requires(i >= 0);
      Contract.Requires(i < this.NumberOfRows);

      Contract.Ensures(Contract.Result<string>() != null);

      string s = "";
      for (int j = 0; j < this.NumberOfColumns; j++)
        if (j == 0)
          s = this[i, j].ToString();
        else
          s += " " + this[i, j].ToString();

      return s;
    }

    /// <summary>
    /// We solve the system Ax=b, were lpList*A=uMatrix*eList
    /// </summary>
    /// <param name="lpList">the list of L or P matrix</param>
    /// <param name="uMatrix">U matrix, upper triangualar matrix with ones at the diagonal</param>
    /// <param name="b">right side, will be changed</param>
    /// <param name="x">the answer will be put into x</param>
    //        static 
    //#if DEBUGGLEE
    //            public
    //#else
    //            internal
    //#endif
    //            void SolveSystem(List<Matrix> lpList, Matrix uMatrix, List<EtaMatrix> eList, Vector b, Vector x) { 
    //            //Transform the system to U*eList*x=lpList*b by multiplying both side by lpList. 
    //            for (int i = lpList.Count - 1; i >= 0; i--)
    //                b  =(Vector) (lpList[i]*b);

    //            //to solve U*eList*x=b we first solve U*x=b
    //            for (int i = b.Length - 1; i >= 0; i--) {
    //                double r = b[i];
    //                for (int k = i + 1; k < b.Length; k++)
    //                    r -= uMatrix[i, k] * x[k];

    //                x[i] = r;
    //            }

    //            //now we solve eList*y=x
    //            for (int i = 0; i < eList.Count; i++) {
    //                Solve(eList[i], x);
    //            }
    //        }
    /// <summary>
    /// solve etaMatrix*y=x and puts the answer into x
    /// </summary>
    /// <param name="etaMatrix"></param>
    /// <param name="x"></param>
    //private static void Solve(EtaMatrix etaMatrix, Vector x) {
    //    int k = etaMatrix.EtaIndex;
    //    double[]column=etaMatrix.EtaColumn;
    //    System.Diagnostics.Debug.Assert(column[0]>10.0e-8);
    //    double t=x[k]/=column[0];
    //    for (int i = 1; i < column.Length; i++)
    //        x[k + i] -= t * column[i];
    //}

    ///// <summary>
    ///// returns the j-th column
    ///// </summary>
    ///// <param name="j"></param>
    ///// <returns></returns>
    //internal void FillColumn(int j,double[]col) {
    //    for (int i = 0; i < this.NumberOfRows; i++)
    //        col[i] = this[i, j];

    //}

  }

  [ContractClassFor(typeof(Matrix))]
  abstract class MatrixContracts : Matrix
  {

    public override int NumberOfRows
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);

        throw new NotImplementedException();
      }
    }

    public override int NumberOfColumns
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);

        throw new NotImplementedException();
      }
    }

    public override double this[int i, int j]
    {
      get
      {
        Contract.Requires(i >= 0);
        Contract.Requires(j >= 0);

        //Contract.Requires(i < this.NumberOfRows);
        //Contract.Requires(j < this.NumberOfColumns);

        return default(double);
      }
      set
      {
        Contract.Requires(i >= 0);
        Contract.Requires(j >= 0);

        //Contract.Requires(i < this.NumberOfRows);
        //Contract.Requires(j < this.NumberOfColumns);
      }
    }
  }
}

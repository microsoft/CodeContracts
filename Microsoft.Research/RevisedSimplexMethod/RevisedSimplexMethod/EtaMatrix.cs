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
  /// Differs from the unit matrix in one arbitrary column 
  /// </summary>
  internal class EtaMatrix : Matrix
  {

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(dim >= 0);
    }

    readonly int dim;

    internal EtaMatrix(int etaIndex, double[] d)
    {
      Contract.Requires(d != null);

      this.EtaIndex = etaIndex;
      this.EtaColumn = d;
      this.dim = this.EtaColumn.Length;
    }

    double[] column;

    public double[] EtaColumn
    {
      get { return column; }
      set { column = value; }
    }
    int etaIndex;

    public int EtaIndex
    {
      get { return etaIndex; }
      set { etaIndex = value; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
    public override int NumberOfRows
    {
      get
      {
        return dim;
      }
      set
      {
        throw new Exception("The method or operation is not implemented.");
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
    public override int NumberOfColumns
    {
      get
      {
        return dim;
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
        if (j == EtaIndex)
          return EtaColumn[i];
        return i == j ? 1 : 0;
      }
      set
      {
        throw new Exception("The method or operation is not implemented.");
      }
    }

    /// <summary>
    /// solve x*this=y and puts the answer into y
    /// </summary>
    /// <param name="eta"></param>
    /// <param name="y"></param>
    unsafe internal void SolveLeftSystem(double[] y)
    {
      //every x[i]=y[i] for i different from EtaIndex
      //We only need to find x[EtaIndex]. We have sum(Column[i]x[i])=y[EtaIndex], therefor x[EtaIndex]=(y[EtaIndex]-sum(Column(i)x(i):i!=EtaIndex)/Column(EtaIndex)
      /* the safe version
      double r = 0;
      int i = 0;
      for (; i < this.EtaIndex; i++)
          r += EtaColumn[i] * y[i];
      i++;
      for (; i < dim; i++)
          r += EtaColumn[i] * y[i];

      y[EtaIndex] = (y[EtaIndex] - r) / EtaColumn[EtaIndex];
      */
      fixed (double* yPin = y)
      fixed (double* ePin = EtaColumn)
      {
        double* yp = yPin;
        double* yAtIndex = yp + EtaIndex;
        double* yEnd = yp + dim;
        double* eta = ePin;
        double r = 0;
        for (; yp < yAtIndex; yp++, eta++)
          r += *eta * *yp;
        yp++; eta++;
        for (; yp < yEnd; yp++, eta++)
          r += *eta * *yp;

        Contract.Assume((double) *(ePin + EtaIndex) != (double) 0.0d);

        *yAtIndex = (*yAtIndex - r) / (*(ePin + EtaIndex));
      }
    }



    /// <summary>
    /// solve this*x=y and puts the answer into y
    /// </summary>
    /// <param name="eta"></param>
    /// <param name="y"></param>
    unsafe internal void SolveRightSystem(double[] y)
    {
      /* the safe version
      double f = y[EtaIndex];
      double t = y[EtaIndex] /= this.EtaColumn[EtaIndex];
      for (int i = 0; i < EtaIndex; i++) //x[i]+Column[i]*f=y[i], x[i]=y[i]-Column[i]*f
          y[i] -= t * EtaColumn[i];

      for (int i = EtaIndex + 1; i < dim; i++)
          y[i] -= t * EtaColumn[i];
      */
      fixed (double* yPin = y)
      fixed (double* ePin = EtaColumn)
      {
        double* yAtIndex = yPin + EtaIndex;
        double f = *yAtIndex;

        Contract.Assume((double)*(ePin + EtaIndex) != (double)0.0d);

        double t = *yAtIndex /= *(ePin + EtaIndex);
        double* yp = yPin;
        double* e = ePin;
        double* yEnd = yPin + dim;
        for (; yp < yAtIndex; yp++, e++) //x[i]+Column[i]*f=y[i], x[i]=y[i]-Column[i]*f
          *yp -= t * (*e);

        yp++; e++;

        for (; yp < yEnd; yp++, e++)
          *yp -= t * (*e);

      }

    }
  }
}

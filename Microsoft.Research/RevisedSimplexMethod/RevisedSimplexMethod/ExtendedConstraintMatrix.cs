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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Microsoft.Glee.Optimization
{
  /// <summary>
  /// This is the matrix of the solver.
  /// </summary>
  internal class ExtendedConstraintMatrix : Matrix
  {

    #region Object Invariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(nVars >= 0);
      Contract.Invariant(this.nRegularVars > 0);
      Contract.Invariant(slacksAndArtificials != null);
    }

    #endregion

    readonly internal double[] coeffs;
    readonly internal int nRegularVars;
    int nVars;
    readonly internal int[] slacksAndArtificials;

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    internal unsafe ExtendedConstraintMatrix(List<Constraint> cs, int[] slacks)
    {
      Contract.Requires(cs != null);

      slacksAndArtificials = slacks;
      if (cs.Count > 0)
      {
        nRegularVars = cs[0].coeffs.Length;

        Contract.Assume(nRegularVars > 0); // F: need quantified invariant

        //allocate a long array for the constraints coeffs
        coeffs = new double[nRegularVars * cs.Count];
        fixed (double* coeffsPin = coeffs)
        {
          double* coeffsP = coeffsPin;
          foreach (Constraint c in cs)
          {
            fixed (double* constrCoeffPin = c.coeffs)
            {
              double* constrCoeff = constrCoeffPin;
              double* constrCoeffEnd = constrCoeffPin + c.coeffs.Length;
              for (; constrCoeff < constrCoeffEnd; constrCoeff++, coeffsP++)
                *coeffsP = *constrCoeff;
            }
          }
        }

        Contract.Assert(nRegularVars > 0);
      }
      nVars = nRegularVars + slacks.Length;
    }


    internal ExtendedConstraintMatrix(List<Constraint> cs, int numberOfSlacksAndArtificials)
      : this(cs, new int[numberOfSlacksAndArtificials])
    {
      Contract.Requires(numberOfSlacksAndArtificials >= 0);
    }


    [SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters",
        MessageId = "System.Exception.#ctor(System.String)"),
     SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
    public override int NumberOfRows
    {
      get { return coeffs.Length / nRegularVars; }
      set { throw new Exception("The method or operation is not implemented."); }
    }

    public override int NumberOfColumns
    {
      get { return nVars; }
      set { nVars = value; }
    }

    public override double this[int i, int j]
    {
      get
      {
        if (j < nRegularVars)
          return coeffs[i * nRegularVars + j];

        return i == slacksAndArtificials[j - nRegularVars] ? 1 : 0;
      }
      set
      {
        Contract.Assume(j >= nRegularVars);
        Contract.Assume((double)value == 1);

        slacksAndArtificials[j - nRegularVars] = i;
      }
    }

    [Pure]
    internal unsafe double DotWithColumn(double[] y, int j)
    {
      int nOfRows = NumberOfRows;
      if (j < nRegularVars)
      {
        fixed (double* yPin = y)
        fixed (double* cPin = coeffs)
        {
          double* yp = yPin;
          double* yEnd = yp + nOfRows;
          double* c = cPin + j;
          double r = 0;
          for (; yp < yEnd; yp++, c += nRegularVars)
            r += (*yp) * (*c);
          return r;
        }
      }
      return y[slacksAndArtificials[j - nRegularVars]];
    }

    /// <summary>
    /// fills y with the j-th column
    /// </summary>
    /// <param name="j"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    internal unsafe void FillColumn(int j, double[] y)
    {
      fixed (double* yPin = y)
      {
        double* yp = yPin;
        double* yEnd = yp + NumberOfRows;
        if (j < nRegularVars)
        {
          fixed (double* cPin = coeffs)
          {
            double* c = cPin + j;
            for (; yp < yEnd; yp++, c += nRegularVars)
              *yp = *c;
          }
        }
        else
        {
          for (; yp < yEnd; yp++)
            *yp = 0;
          *(yPin + slacksAndArtificials[j - nRegularVars]) = 1;
        }
      }
    }
  }
}
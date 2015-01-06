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

#define CONTRACTS_FULL

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;


namespace VSTTE2012
{
  public class Exercise1
  {
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(a != null);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 'a'",PrimaryILOffset=24,MethodILOffset=0)]
    static public void TwoWaySortInlined(bool[] a)
    {
      int i = 0;
      int j = a.Length - 1;

      while (i <= j)
      {
        var dist = j - i;
        if (!a[i])
        {
          i++;
        }
        else if (a[j])
        {
          j--;
        }
        else
        {
          Contract.Assume(i < j); // F: cannot infer it yet
          var tmp = a[i];
          a[i] = a[j];
          a[j] = tmp;
          i++;
          j--;
        }

        Contract.Assert(dist >= 0);
        Contract.Assert(dist > (j - i));
      }
      Contract.Assert(Contract.ForAll(0, i, indx => !a[indx]));
      Contract.Assert(Contract.ForAll(j + 1, a.Length, indx => a[indx]));

    }

    [ClousotRegressionTest]
    public void SwapWithContracts(int[] a, int i, int j)
    {
      Contract.Requires(a != null);
      Contract.Requires(i >= 0);
      Contract.Requires(j >= 0);
      Contract.Requires(i < a.Length);
      Contract.Requires(j < a.Length);

      Contract.Ensures(Contract.OldValue(a[i]) == a[j]);
      Contract.Ensures(Contract.OldValue(a[j]) == a[i]);

      var tmp = a[j];
      a[j] = a[i];
      a[i] = tmp;
    }

  }
}
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

namespace PreInference
{
  public static class ExistInference
  {
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(Contract.Exists(0, arr.Length, __j0__ => arr[__j0__] == 3));")]
    [RegressionOutcome("Contract.Ensures(arr[Contract.Result<System.Int32>()] == 3);")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() < arr.Length);")]
    [RegressionOutcome("Contract.Ensures(0 <= Contract.Result<System.Int32>());")]
    [RegressionOutcome("Contract.Requires(1 <= arr.Length);")]
    public static int FirstThree(int[] arr)
    {
      var i = 0;
      while (arr[i] != 3)
      {
        i++;
      }

      return i;
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(1 <= arr.Length);")]
    [RegressionOutcome("Contract.Requires(Contract.Exists(0, arr.Length, __j__ => arr[__j__] == 3));")]
    [RegressionOutcome("Contract.Ensures(Contract.Exists(0, arr.Length, __j__ => arr[__j__] == 3));")]
    public static void UseFirstThree(int[] arr)
    {
      var index = FirstThree(arr);

      // Use the inferred postcondition to prove this
      Contract.Assert(arr[index] == 3);
    }


    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(1 <= arr.Length);")]
    [RegressionOutcome("Contract.Requires(Contract.Exists(0, arr.Length, __j__ => arr[__j__] == 3));")]
    [RegressionOutcome("Contract.Ensures(Contract.Exists(0, arr.Length, __j__ => arr[__j__] == 3));")]
    public static void FirstThreeVoid(int[] arr)
    {
      var i = 0;
      while (arr[i] != 3)
      {
        i++;
      }

      return;
    }
  }

  class MaxExample
  {
    [ClousotRegressionTest]
     // This should not show up, as we are filtering facts known from the entry state
    //    [RegressionOutcome("Contract.Ensures(z.Length >= 1);")]
    [RegressionOutcome("Contract.Ensures(Contract.Exists(0, z.Length, __j__ => z[__j__] == Contract.Result<System.Int32>()));")]
    [RegressionOutcome("Contract.Ensures(Contract.ForAll(0, z.Length, __k__ => z[__k__] <= Contract.Result<System.Int32>()));")]
    public int Max(int[] z)
    {
      Contract.Requires(z != null);
      Contract.Requires(z.Length > 0);

      var max = z[0];

      for (var i = 0; i < z.Length; i++)
      {
        if (z[i] > max)
          max = z[i];
      }
      return max;
    }

    // To infer the first postcondition below we need the refinement of the symbolic values so array index expressions
    [Pure]
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures((a[i] - Contract.Result<System.Int32>()) <= 0);")]
    [RegressionOutcome("Contract.Ensures((Contract.OldValue(max) - Contract.Result<System.Int32>()) <= 0);")]
	private static int NewMethod(int[] a, int max, int i)
    {

      Contract.Requires(a != null);
      Contract.Requires(0 <= i);
      Contract.Requires(i < a.Length);

      var tmp = a[i];
      if (tmp > max)
      {
        max = tmp;
      }
      return max;
    }
  }
}
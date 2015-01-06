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


using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;
using System;

namespace TestBinarySearch
{
  public class TestBinarySearch
  {
    // #1 One warning
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 16, MethodILOffset = 0)]
    public static int BinarySearch1_OffByOne(int[] array, int value)
    {
      int inf = 0;
      int sup = array.Length; // Bad initialization

      while (inf <= sup)
      {
        int index = (inf + sup) >> 1;

        int mid = array[index];

        if (value == mid)
          return index;
        if (mid < value)
          inf = index + 1;
        else
          sup = index - 1;
      }
      return -1;
    }

    // #2 ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 18, MethodILOffset = 0)]
    public static int BinarySearch2(int[] array, int value)
    {
      int inf = 0;
      int sup = array.Length - 1;

      while (inf <= sup)
      {
        int index = (inf + sup) >> 1;

        int mid = array[index];

        if (value == mid)
          return index;
        if (mid < value)
          inf = index + 1;
        else
          sup = index - 1;
      }
      return -1;
    }


    // #3 One wrong postcondition
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 49, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 49, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"ensures is false: Contract.Result<int>() >= 0", PrimaryILOffset = 11, MethodILOffset = 76)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 11, MethodILOffset = 56)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 26, MethodILOffset = 56)]
    [RegressionOutcome(Outcome = ProofOutcome.Bottom, Message = @"ensures unreachable", PrimaryILOffset = 26, MethodILOffset = 76)]
    public static int BinarySearch3_WrongPostcondition(int[] array, int value)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);      // Wrong postcondition
      Contract.Ensures(Contract.Result<int>() < array.Length);

      int inf = 0;
      int sup = array.Length - 1;

      while (inf <= sup)
      {
        int index = (inf + sup) >> 1;

        int mid = array[index];

        if (value == mid)
          return index;
        if (mid < value)
          inf = index + 1;
        else
          sup = index - 1;
      }

      return -1;
    }

    // #4 ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 49, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 49, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 11, MethodILOffset = 76)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 26, MethodILOffset = 76)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 11, MethodILOffset = 56)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 26, MethodILOffset = 56)]
    public static int BinarySearch4_OK(int[] array, int value)
    {
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < array.Length);

      int inf = 0;
      int sup = array.Length - 1;

      while (inf <= sup)
      {
        int index = (inf + sup) >> 1;

        int mid = array[index];

        if (value == mid)
          return index;
        if (mid < value)
          inf = index + 1;
        else
          sup = index - 1;
      }

      return -1;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 87, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 87, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 57, MethodILOffset = 94)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 57, MethodILOffset = 94)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 23, MethodILOffset = 114)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 38, MethodILOffset = 114)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 64, MethodILOffset = 114)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 23, MethodILOffset = 94)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 38, MethodILOffset = 94)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 64, MethodILOffset = 94)]
    public static int BinarySearch5(int[] array, int value)
    {
      Contract.Requires(array != null);

      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < array.Length);

      Contract.Ensures(Contract.Result<int>() == -1 || array[Contract.Result<int>()] == value);


      int inf = 0;
      int sup = array.Length - 1;

      while (inf <= sup)
      {
        int index = (inf + sup) >> 1;

        int mid = array[index];

        if (value == mid)
        {
          return index;
        }
        if (mid < value)
        {
          inf = index + 1;
        }
        else
        {
          sup = index - 1;
        }
      }

      return -1;
    }
  }
}

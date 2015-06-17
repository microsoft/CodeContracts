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
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;

namespace WebExamples
{
  public class Tutorial
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=11,MethodILOffset=42)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=27,MethodILOffset=42)]
    public static void SwapElements(ref int x, ref int y)
    {
      Contract.Ensures(x == Contract.OldValue(y));
      Contract.Ensures(y == Contract.OldValue(x));
      
      var tmp = y;
      y = x;
      x = tmp;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=9,MethodILOffset=27)]
    public static void OrderAPair(ref int x, ref int y)
    {
      Contract.Ensures(x <= y);

      if (x > y)
        SwapElements(ref x, ref y);
    }

    [ClousotRegressionTest]   
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=126,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=114,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=46,MethodILOffset=131)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=46,MethodILOffset=119)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=96,MethodILOffset=119)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=35,MethodILOffset=131)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=50,MethodILOffset=131)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=77,MethodILOffset=131)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=35,MethodILOffset=119)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=50,MethodILOffset=119)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=77,MethodILOffset=119)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=103,MethodILOffset=119)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=103,MethodILOffset=131)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=114,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=114,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=96,MethodILOffset=119)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=96,MethodILOffset=119)]
    [Pure]
    public static int LinearSearch(int[] array, int from, int value)
    {
      Contract.Requires(array != null);
      Contract.Requires(from >= 0);

      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < array.Length);

      Contract.Ensures(Contract.Result<int>() == -1 || Contract.Result<int>() >= from); // We need it!
      Contract.Ensures(Contract.Result<int>() == -1 || array[Contract.Result<int>()] == value);

      for (int i = from; i < array.Length; i++)
      {
        if (array[i] == value)
          return i;
      }
      return -1;
    }

#if !CLOUSOT2
    [ClousotRegressionTest]   
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=8,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=14,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=8,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=127,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=132,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=63,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=8,MethodILOffset=137)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=93,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=100,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=106,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=107,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=109,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=116,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=8,MethodILOffset=120)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=7,MethodILOffset=70)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=19,MethodILOffset=70)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=53,MethodILOffset=137)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=87,MethodILOffset=0)] 
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=53,MethodILOffset=120)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=106,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=106,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=107,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=107,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=116,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=116,MethodILOffset=0)]
   public static int ZerosAtBeginning(int[] array)
    {
      Contract.Requires(array != null);
      Contract.Ensures(Contract.ForAll(0, Contract.Result<int>(), index => array[index] == 0));

      var i = 0;
      while (i < array.Length)
      {
        var nextZero = LinearSearch(array, i, 0);
        if (nextZero != -1)
        {
          Contract.Assert(nextZero >= i);
          array[nextZero] = array[i];
          array[i] = 0;
        }
        else
        {
          return i;
        }

        i++;
      }

      return i;
    }
#endif
  }

  static public class RiseForFun
  {    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"assert is false",PrimaryILOffset=8,MethodILOffset=0)]
    static void Assert()
    {
      int x = 0, y = 0;
      
      Contract.Assert(x > y);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=11,MethodILOffset=22)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=11,MethodILOffset=24)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible negation of MinValue of type Int32. The static checker determined that the condition 'i != Int32.MinValue' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(i != Int32.MinValue);",PrimaryILOffset=21,MethodILOffset=0)]
    static int Abs(int i)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);
      // will this expression always return a positive value?
      return i > 0 ? i : -i;
    }

#if !CLOUSOT2
    [ClousotRegressionTest] 
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=2,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=11,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=12,MethodILOffset=11)]
//    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"requires (always false) may be reachable: HasValue",PrimaryILOffset=12,MethodILOffset=21)]
 [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"requires is false: HasValue",PrimaryILOffset=12,MethodILOffset=21)]
    static int CorrectUsage(int? opt)
    {
      if (opt.HasValue)
	return opt.Value + 1;
      
      return opt.Value;
    }
#endif
  }

  class ArrayBounds
  {
    // What can possibly go wrong here?
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 'a'. The static checker determined that the condition 'a != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(a != null);",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be below the lower bound",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=9,MethodILOffset=0)]
    static int Sum(int[] a, int start, int numItems)
    {
      int result = 0;
      for (int i = start; i < start + numItems; i++)
	result += a[i];
      return result;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=40,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=61,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=61,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=61,MethodILOffset=0)]
    static int SumFixed(int[] a, int start, int numItems)
    {
      Contract.Requires(a != null);
      Contract.Requires(0 <= start);
      Contract.Requires(0 <= numItems);
      Contract.Requires(start + numItems <= a.Length);
      
      int result = 0;
      for (int i = start; i < start + numItems; i++)
	result += a[i];
      return result;
    }
  }


  class MinIndexExample
  {
    // Compute the index of the minimal element in data, or return -1
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=73,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=59,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=62,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=34,MethodILOffset=78)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=23,MethodILOffset=78)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=38,MethodILOffset=78)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=59,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=59,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=62,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=62,MethodILOffset=0)]
    static int MinIndex(int[] data)
    {
      Contract.Requires(data != null);
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < data.Length);
      
      var result = -1;
      for (int i = 0; i < data.Length; i++)
        {
	  if (result < 0) { result = i; }
	  else if (data[i] < data[result]) { result = i; }
        }
      return result;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=7,MethodILOffset=13)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be below the lower bound",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=21,MethodILOffset=0)]
    static int BadUser(int[] data)
    {
      Contract.Requires(data != null);
      
      var minIndex = MinIndex(data);
      var minValue = data[minIndex];
      return minValue;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=25,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=7,MethodILOffset=13)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=25,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=25,MethodILOffset=0)]
    static int GoodUser(int[] data)
    {
      Contract.Requires(data != null);
      
      var minIndex = MinIndex(data);
      if (minIndex >= 0)
        {
	  var minValue = data[minIndex];
	  return minValue;
        }
      return Int32.MinValue;
    }
  }

  static class Search
  {

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=36,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=36,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=15,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=36,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=22,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible overflow in the arithmetic operation",PrimaryILOffset=22,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Division by zero ok",PrimaryILOffset=24,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=24,MethodILOffset=0)]
    public static int BinarySearch_Wrong(int[] arr, int what)
    {
      Contract.Requires(arr != null);

      var inf = 0;
      var sup = arr.Length;
      
      while (inf <= sup)
      {
        var mid = checked((inf + sup)/ 2);
        if (arr[mid] == what)
          return what;
        if (arr[mid] < what)
        {
          sup = mid - 1;
        }
        else
        {
          inf = mid+1;
        }
      }
      return -1;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=32,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=32,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=40,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=40,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=15,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=32,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=40,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=25,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=25,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Division by zero ok",PrimaryILOffset=27,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=27,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=28,MethodILOffset=0)]
    public static int BinarySearch_Correct(int[] arr, int what)
    {
      Contract.Requires(arr != null);

      var inf = 0;
      var sup = arr.Length - 1;
      
      while (inf <= sup)
      {
        var len = sup - inf;
        var mid = checked(inf + (sup - inf) / 2);
        if (arr[mid] == what)
          return what;
        if (arr[mid] < what)
        {
          sup = mid - 1;
        }
        else
        {
          inf = mid + 1;
        }
      }
      return -1;
    }
  }
}

namespace VMCAI
{
 public class VMCAI
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Array creation : ok",PrimaryILOffset=178,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=192,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=192,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=217,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=217,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=247,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=247,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Array creation : ok",PrimaryILOffset=259,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=278,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=278,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=279,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=279,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=176,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=254,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=192,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=217,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=247,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=278,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=279,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=44,MethodILOffset=293)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=66,MethodILOffset=293)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow (caused by a negative array size)",PrimaryILOffset=178,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow (caused by a negative array size)",PrimaryILOffset=259,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=20,MethodILOffset=293)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=33,MethodILOffset=293)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=51,MethodILOffset=293)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=70,MethodILOffset=293)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=115,MethodILOffset=293)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=160,MethodILOffset=293)]
    public char[] Sanitize(char[] str, ref int lower, ref int upper)
    {
      Contract.Requires(str != null);

      Contract.Ensures(upper >= 0);
      Contract.Ensures(lower >= 0);
      Contract.Ensures(lower + upper <= str.Length);
      Contract.Ensures(lower + upper == Contract.Result<char[]>().Length);
      Contract.Ensures(Contract.ForAll(0, lower+upper, index => 'a' <= Contract.Result<char[]>()[index]));
      Contract.Ensures(Contract.ForAll(0, lower + upper, index => Contract.Result<char[]>()[index] <= 'z'));

      upper = lower = 0;

      var tmp = new char[str.Length];

      int j = 0;
      for (int i = 0; i < str.Length; i++)
      {
        var ch = str[i];

        if ('a' <= ch && ch <= 'z') { lower++; tmp[j++] = ch;}
        else if ('A' <= ch && ch<= 'Z') { upper++; tmp[j++] = (char)(ch | ' ');}
      }

      var result = new char[j];
      for (int i = 0; i < j; i++)
      {
        result[i] = tmp[i];
      }
      return result;
    }
  }  
}

namespace ExamplesFromResearchPapers
{
  public class SrivastavaGulwaniPLDI09
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=48,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=64,MethodILOffset=0)]
    public void Example(bool b1, bool b2, bool b3)
    {
      int d, t, s;
      d = t = s = 0;
      while (b3)
      {
        if (b1)
        { t++; s = 0; }
        else if (b2)
        {
          if (s < 5)
          { d++; s++; }
        }
      }

      Contract.Assert(s + d + t >= 0);
      Contract.Assert(d <= s + 5 * t);
    }
  }

  public class Bagnara_LeviTalk
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=51,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=51,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=77,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=77,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=63,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=63,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=64,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=64,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=84,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=84,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=14,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=51,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=77,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=63,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=64,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=84,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message="Division by zero ok",PrimaryILOffset=95,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message="No overflow",PrimaryILOffset=95,MethodILOffset=0)]
    public void ShellSort(int n, int[] array)
    {
      Contract.Requires(array != null);
      Contract.Requires(n == array.Length); // just to adapt Bagnara example faithfully

      int h, i, j, B;

      h = 1;
      while (h * 3 + 1 < n)
      {
        h = 3 * h + 1;
      }
      while (h > 0)
      {
        i = h - 1;
        while(i < n)
        {
          B = array[i];
          j = i;
          while (j >= h && array[j - h] > B)
          {
            array[j] = array[j - h];
            j = j - h;
          }
          array[j] = B;
          i++;
        }
        h = h / 3;
      }
    }
  }  
}
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


namespace Arrays
{
  public class ArraysBasic
  {
    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 26, MethodILOffset = 0)]
    public void SetFirstElementTo_12(int[] arr, int i)
    {
      Contract.Requires(arr.Length > 0);

      arr[0] = 12;

      // {0} 12 {1} [-oo, +oo] {arr.Length}?
      if (i == 0)
      {
        Contract.Assert(arr[i] == 12);  // true
      }
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 26, MethodILOffset = 0)]
    public void SetFirstElementTo_12_Precondition(int[] arr, int i)
    {
      Contract.Requires(arr.Length > 1);

      arr[0] = 12;

      // {0} 12 {1} [-oo, +oo] {arr.Length}

      if (i == 0)
      {
        Contract.Assert(arr[0] == 12);  // true
      }
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 24, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 40, MethodILOffset = 0)]
    public void SetFifthElementTo_12(int[] arr, int i)
    {
      Contract.Requires(arr.Length > 10);

      arr[4] = 12;

      // {0} [-oo, +oo] {4} 12 {5} [-oo,+oo] {10}

      Contract.Assert(arr[0] == 12);    // top

      if (i == 4)
      {
        Contract.Assert(arr[i] == 12);    // true
      }
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 23, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 35, MethodILOffset = 0)]
    public void SetFifthElementTo_12_ArrayLength5(int[] arr)
    {
      Contract.Requires(arr.Length == 5);

      arr[4] = 12;

      // {0} [-oo, +oo] {4} 12 {5, arr.length}

      Contract.Assert(arr[0] == 12);    // top
      Contract.Assert(arr[4] == 12);    // true 
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 26, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 41, MethodILOffset = 0)]
    public void Init_NoLoop_FirstElement()
    {
      int[] a = new int[100];

      a[0] = 222;

      Contract.Assert(a[0] == 222);   // true
      Contract.Assert(a[4] == 222);   // false
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 26, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 41, MethodILOffset = 0)]
    public void Init_NoLoop_SecondElement()
    {
      int[] a = new int[100];

      a[1] = 222;

      Contract.Assert(a[1] == 222);   // true
      Contract.Assert(a[4] == 222);   // false
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 42, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 57, MethodILOffset = 0)]
    public void Init_NoLoop_ThreeElements()
    {
      int[] a = new int[100];

      a[0] = 111111;
      a[2] = 2222222;
      a[4] = 3333333;

      Contract.Assert(a[0] == 111111); // true
      Contract.Assert(a[2] == 111111); // false      
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 58, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 76, MethodILOffset = 0)]
    public void Init_NoLoop_Three_Successive_Elements(int index)
    {
      int[] a = new int[100];

      a[10] = 111111;
      a[11] = 2222222;
      a[12] = 3333333;

      if (index >= 10 && index <= 12)
      {
        Contract.Assert(a[index] >= 111111);
        Contract.Assert(a[index] <= 3333333);
      }
    }


    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 49, MethodILOffset = 0)]
    public int[] InitReverse(int index)
    {
      int[] a = new int[1000];

      a[99] = 2222222;
      a[98] = 2222222;

      if (index >= 98 && index <= 99)
      {
        Contract.Assert(a[index] == 2222222);
      }

      return a;
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 56, MethodILOffset = 0)]
    public int[] ProveAssertion(int index, int value)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index < 10);

      Contract.Requires(value < -1111);

      int[] a = new int[10];

      a[index] = value;

      Contract.Assert(a[0] <= 0); // true

      return a;
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 53, MethodILOffset = 0)]
    public int[] ProveAssertion_NotOk(int index, int value)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index < 10);

      Contract.Requires(value < -1111);

      int[] a = new int[10];

      a[index] = value;

      Contract.Assert(a[0] == 0); // top, we may have written index

      return a;
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 53, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 67, MethodILOffset = 0)]
    public int[] ProveAssertion_FirstThreeElementsZero(int index, int value)
    {
      Contract.Requires(index >= 4);
      Contract.Requires(index < 10);

      Contract.Requires(value < -1111);

      int[] a = new int[10];

      a[index] = value;

      Contract.Assert(a[0] == 0);
      Contract.Assert(a[4] <= 0);

      return a;
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 35, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 49, MethodILOffset = 0)]
    public void SetToThree_Length10()
    {
      int[] a = new int[10];

      int i;
      for (i = 0; i < a.Length; i++)
      {
        a[i] = 3;

      }

      Contract.Assert(a[7] >= 0);     // True
      Contract.Assert(a[3] >= 4);     // False
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 32, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 43, MethodILOffset = 0)]
    public void SetToThree_Length10_TestReduction()
    {
      int[] a = new int[10];

      int i;
      for (i = 0; i < a.Length; i++)
      {
        a[i] = 3;
      }

      Contract.Assert(a[7] == 3);     // True
      Contract.Assert(a[3] == 4);     // False
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 41, MethodILOffset = 0)]
    public void SetToThree_Length10_TestReduction_For()
    {
      int[] a = new int[10];

      int i;
      for (i = 0; i < a.Length; i++)
      {
        a[i] = 3;
      }
      ClousotDebug.Francesco_PrintArrayContent();

      for (i = 0; i < 5; i++)
      {
        Contract.Assert(a[i] == 3); // true
      }
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 41, MethodILOffset = 0)]
    public void InitArrayNoKnownUpperBound(int[] a)
    {
      // Here we can have a.Length == 0 so that the post-state after the first loop contains ?
      for (int i = 0; i < a.Length; i++)
      {
        a[i] = -333333;
      }

      ClousotDebug.Francesco_PrintArrayContent();

      //sv9 (15) -> {0 ,sv4 (10)} [-333333, -333333] {sv19 (32) ,sv10 (16)}? 

      for (int i = 0; i < a.Length; i++)
      {
        Contract.Assert(a[i] == -333333);
      }
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 52, MethodILOffset = 0)]
    public void InitArrayNoKnownUpperBound_AtMostOneElement(int[] a)
    {
      Contract.Requires(a.Length > 0);

      for (int i = 0; i < a.Length; i++)
      {
        a[i] = -333333;
      }

      ClousotDebug.Francesco_PrintArrayContent();
      //sv9 (15) -> {0 ,sv4 (10)} [-333333, -333333] {sv19 (32) ,sv10 (16)}? 

      for (int i = 0; i < a.Length; i++)
      {
        Contract.Assert(a[i] == -333333); // True
      }
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 42, MethodILOffset = 0)]
    public void InitArrayTo_i(int[] a)
    {
      Contract.Requires(a.Length > 0);

      for (int i = 0; i < a.Length; i++)
      {
        a[i] = i;
      }

      for (int i = 0; i < a.Length; i++)
      {
        Contract.Assert(a[i] >= 0);     // True
      }
    }

    // TODO
    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 39, MethodILOffset = 0)]
    public void InitArrayToi_NeedRelational(int[] a)
    {
      Contract.Requires(a.Length > 0);

      for (int i = 0; i < a.Length; i++)
      {
        a[i] = i;
      }

      for (int i = 0; i < a.Length; i++)
      {
        Contract.Assert(a[i] == i);         // TODO: We need relational information here
      }
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 63, MethodILOffset = 0)]
    public int[] Copy_Wrong(int[] from)
    {
      var result = new int[from.Length];
      int j = 0;
      for (int i = 0; i < from.Length; i++)
      {
        Contract.Assume(j <= i);

        if (from[i] > 0)
        {
          result[j] = from[i];

          j++;
        }
      }

      // ClousotDebug.Francesco_PrintArrayContent();

      for (int k = 0; k < result.Length; k++)
      {
        Contract.Assert(result[k] > 0); // Top
      }

      return result;
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 63, MethodILOffset = 0)]
    public int[] Copy_Ok(int[] from)
    {
      var result = new int[from.Length];
      int j = 0;
      for (int i = 0; i < from.Length; i++)
      {
        Contract.Assume(j <= i);

        if (from[i] > 0)
        {
          result[j] = from[i];

          j++;
        }
      }

      for (int k = 0; k < j; k++)
      {
        Contract.Assert(result[k] > 0); // OK
      }

      return result;
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 36, MethodILOffset = 0)]
    public void InitTo1234_With_Incrementer(int[] a)
    {
      var i = 0;
      while (i < a.Length)
      {
        a[i++] = 1234;
      }

      for (int j = 0; j < a.Length; j++)
      {
        Contract.Assert(a[j] == 1234);
      }

    }

    // TODO
    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 37, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 48, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 59, MethodILOffset = 0)]
    public void SetToThree_Length10_From2()
    {
      int[] a = new int[10];

      int i;
      for (i = 2; i < a.Length; i++)
      {
        a[i] = 3;
      }

      ClousotDebug.Francesco_PrintArrayContent();

      Contract.Assert(a[1] == 0);     // True - we can prove it
      Contract.Assert(a[7] == 3);     // True - cannot prove it yet
      Contract.Assert(a[3] == 4);     // False - we can prove it
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=99,MethodILOffset=0)]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=13,MethodILOffset=94)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: predicate != null (predicate)",PrimaryILOffset=35,MethodILOffset=94)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=3,MethodILOffset=94)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=25,MethodILOffset=94)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven",PrimaryILOffset=22,MethodILOffset=94)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven",PrimaryILOffset=44,MethodILOffset=94)]
	#endif
#endif
    public void SetFromNthElement(int[] array, int N)
    {
      Contract.Requires(N >= 0);
      Contract.Requires(N < array.Length);
      
      for (var i = N; i < array.Length; i++)
      {
        array[i] = 9876;
      }

      Contract.Assert(Contract.ForAll(N, array.Length, index => array[index] == 9876)); // ok
    }

  }

  public class ClousotDebug
  {
    static internal void Francesco_PrintArrayContent()
    {
    }
  }
}

namespace UseForAll
{
  class Assume
  {
    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 101, MethodILOffset = 0)]
    public static void Test1_Ok(int[] a, int i)
    {
      Contract.Requires(a != null);
      Contract.Requires(i >= 0);
      Contract.Requires(i < a.Length);
      Contract.Requires(Contract.ForAll(0, a.Length, j => a[j] > 100));

      Contract.Assert(a[i] > 100); // True
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 101, MethodILOffset = 0)]
    public static void Test2_NotOk(int[] a, int i)
    {
      Contract.Requires(a != null);
      Contract.Requires(i >= 0);
      Contract.Requires(i < a.Length);
      Contract.Requires(Contract.ForAll(0, a.Length, j => a[j] > 100));

      Contract.Assert(a[i] == -100); // False
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=73,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=93,MethodILOffset=0)]
    public static void Test3(int[] a)
    {
      Contract.Requires(a.Length >= 10);
      Contract.Requires(Contract.ForAll(2, 10, t => a[t] == -765));

      Contract.Assert(a[3] == -765);  // True
      Contract.Assert(a[0] == -765);  // Top
    }

    [ClousotRegressionTest("Intervals")]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=13,MethodILOffset=50)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: predicate != null (predicate)",PrimaryILOffset=35,MethodILOffset=50)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=3,MethodILOffset=50)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=25,MethodILOffset=50)]
	#else
		[RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 22, MethodILOffset = 50)]
		[RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven", PrimaryILOffset = 44, MethodILOffset = 50)]
	#endif
#endif
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 75, MethodILOffset = 0)]
    public void TestAssumeForAll(int[] arr)
    {
      Contract.Requires(arr.Length > 0);
      Contract.Assume(Contract.ForAll(0, arr.Length, i => arr[i] == -987));

      Contract.Assert(arr[0] == -987);  // true
    }
  }

  class Assert
  {
    [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=13,MethodILOffset=45)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: predicate != null (predicate)",PrimaryILOffset=35,MethodILOffset=45)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=3,MethodILOffset=45)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=25,MethodILOffset=45)]
	#else		
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=22,MethodILOffset=45)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven",PrimaryILOffset=44,MethodILOffset=45)]
	#endif
#endif
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=50,MethodILOffset=0)]
    public static string[] Test1()
    {
      var result = new string[1];
      result[0] = "Ciao";

      Contract.Assert(Contract.ForAll(0, 1, j => result[j] != null));     // We get top because we do not run the nonnull analysis in this test

      return result;
    }

    [ClousotRegressionTest("Intervals")]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=13,MethodILOffset=82)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: predicate != null (predicate)",PrimaryILOffset=35,MethodILOffset=82)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=3,MethodILOffset=82)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=25,MethodILOffset=82)]
	#else
		[RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 22, MethodILOffset = 82)]
		[RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven", PrimaryILOffset = 44, MethodILOffset = 82)]
	#endif
#endif
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 87, MethodILOffset = 0)]
    public void TestAssertForAll(int[] arr)
    {
      Contract.Requires(arr.Length > 0);

      for (int i = 0; i < arr.Length; i++)
      {
        arr[i] = -987;
      }

      Contract.Assert(Contract.ForAll(0, arr.Length, i => arr[i] == -987));
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 55, MethodILOffset = 92)]
    public void TestEnsuresForAll(int[] arr)
    {
      Contract.Requires(arr.Length > 0);

      Contract.Ensures(Contract.ForAll(0, arr.Length, i => arr[i] == -987));

      for (int i = 0; i < arr.Length; i++)
      {
        arr[i] = -987;
      }
    }
  }

  class Requires
  {
    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 101, MethodILOffset = 0)]
    public void ForEach(int[] a)
    {
      Contract.Requires(a != null);

      Contract.Requires(Contract.ForAll(0, a.Length, i => a[i] > 0));

      var sum = 1;

      foreach (var val in a)
      {
        sum += val;
      }

      // Well, up to overflows
      Contract.Assert(sum > 0);
    }
  }
}

namespace FromPapers
{
  public class KovacsVoronkov
  {
    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 83, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 111, MethodILOffset = 0)]
    public void KovacsVoronkov_NoIncrements_Wrong(int[] a)
    {
      int[] pos = new int[a.Length];
      int[] neg = new int[a.Length];

      int p = 0;
      int n = 0;

      for (int i = 0; i < a.Length; i++)
      {
        if (a[i] > 0)
        {
          pos[p] = a[i];
          p++;
        }
        else 
        {
          neg[n] = a[i];
          n++;
        }
      }

      for (int i = 0; i < p; i++)
      {
        Contract.Assert(pos[i] > 0);  // True
      }

      for (int i = 0; i < n; i++)
      {
        Contract.Assert(neg[i] < 0);  // Top: can be zero!
      }
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 61, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 102, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 130, MethodILOffset = 0)]
    public void KovacsVoronkov_NoIncrements(int[] a)
    {
      int[] pos = new int[a.Length];
      int[] neg = new int[a.Length];

      int p = 0;
      int n = 0;

      for (int i = 0; i < a.Length; i++)
      {
        if (a[i] > 0)
        {
          pos[p] = a[i];
          p++;
        }
        else if (a[i] < 0)
        {
          Contract.Assert(a[i] < 0);
          neg[n] = a[i];
          n++;
        }
      }

      for (int i = 0; i < p; i++)
      {
        Contract.Assert(pos[i] > 0);
      }

      for (int i = 0; i < n; i++)
      {
        Contract.Assert(neg[i] < 0);
      }
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 122, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 150, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 179, MethodILOffset = 0)]
    public void KovacsVoronkov_NoIncrements_Zero(int[] a)
    {
      int[] pos = new int[a.Length];
      int[] neg = new int[a.Length];
      int[] zero = new int[a.Length];

      int p = 0;
      int n = 0;
      int z = 0;

      for (int i = 0; i < a.Length; i++)
      {
        if (a[i] > 0)
        {
          pos[p] = a[i];
          p++;
        }
        else if (a[i] < 0)
        {
          neg[n] = a[i];
          n++;
        }
        else
        {
          zero[z] = a[i];
          z++;
        }
      }

      for (int i = 0; i < p; i++)
      {
        Contract.Assert(pos[i] > 0); // True
      }

      for (int i = 0; i < n; i++)
      {
        Contract.Assert(neg[i] < 0); // True
      }

      for (int i = 0; i < z; i++)
      {
        Contract.Assert(zero[i] == 0); // True
      }

    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 132, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=149,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=178,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=206,MethodILOffset=0)]
    public static void Split(int[] input)
    {
      Contract.Requires(input != null);

      int[] zero = new int[input.Length],
        pos = new int[input.Length], neg = new int[input.Length];

      int z = 0, p = 0, n = 0;

      for (int i = 0; i < input.Length; i++)
      {
        if (input[i] > 0)
        {
          pos[p++] = input[i];
        }
        else if (input[i] < 0)
        {
          neg[n++] = input[i];
        }
        else
        {
          zero[z++] = input[i];
        }
      }

      Contract.Assert(input.Length == p + n + z);

      for (int i = 0; i < p; i++)
      {
        Contract.Assert(pos[i] > 0);    // True
      }
      for (int i = 0; i < z; i++)
      {
        Contract.Assert(zero[i] == 0);  // True
      }
      for (int i = 0; i < n; i++)
      {
        Contract.Assert(neg[i] < 0);    // True
      }
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 70, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 82, MethodILOffset = 0)]
    public void GopanRepsSagiv_PartialInit_Ok(int[] a, int[] b, int[] c)
    {
      Contract.Requires(a.Length == b.Length);
      Contract.Requires(a.Length == 100);

      int j = 0;
      for (int i = 0; i < a.Length; i++)
      {
        if (a[i] == b[i])
        {
          c[j] = i;
          j++;
        }
      }

      for (int k = 0; k < j; k++)
      {
        Contract.Assert(c[k] >= 0);     // True
        Contract.Assert(c[k] < 100);    // True
      }
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=70,MethodILOffset=0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 82, MethodILOffset = 0)]
    public void GopanRepsSagiv_PartialInit_NotOk(int[] a, int[] b, int[] c)
    {
      Contract.Requires(a.Length == b.Length);
      Contract.Requires(a.Length == 100);

      int j = 0;
      for (int i = 0; i < a.Length; i++)
      {
        if (a[i] == b[i])
        {
          c[j] = i;
          j++;
        }
      }

      for (int k = 0; k < c.Length; k++)
      {
        Contract.Assert(c[k] >= 0);   // Top
        Contract.Assert(c[k] < 100);  // Top
      }
    }
  }
}

// F: We keep Mathias's test anyway , even if they duplicate some of the tests above
namespace Mathias
{
  class Program
  {    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=81,MethodILOffset=0)]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=17,MethodILOffset=76)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: predicate != null (predicate)",PrimaryILOffset=39,MethodILOffset=76)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=3,MethodILOffset=76)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=25,MethodILOffset=76)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=22,MethodILOffset=76)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven",PrimaryILOffset=44,MethodILOffset=76)]
	#endif
#endif
    public int Max(int[] arr)
    {
      Contract.Requires(arr != null);
      int max = Int32.MinValue;

      for (int i = 0; i < arr.Length; i++)
      {
        if (arr[i] > max)
        {
          max = arr[i];
        }
      }

      Contract.Assert(Contract.ForAll(arr, el => el <= max));

      return max;
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=42,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=56,MethodILOffset=0)]
    static void MathiasTest0(string[] args)
    {
      int[] a = new int[5];

      for (int i = 0; i < a.Length - 1; i = i + 1)  
      {
        a[i + 1] = 7;
      }

      // here a[i] == 0, so a[*] \in [0, 7]

      for (int i = 0; i < a.Length; i++)
      {
        Contract.Assert(a[i] >= 0);  // True
        Contract.Assert(a[i] <= 7);  // True, but we cannot prove it: we need one more join before widening (-joinsBeforeWidening >= 2)
      }
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 36, MethodILOffset = 0)]
    static void SetToSeven()
    {
      int[] a = new int[53];

      for (int i = 0; i < a.Length; i = i + 1)
      {
        a[i] = 7;
      }

      for (int i = 0; i < a.Length; i++)
      {
        Contract.Assert(a[i] == 7);
      }
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 48, MethodILOffset = 0)]
    static int[] FilterGTZero(int[] z)
    {
      int[] res = new int[z.Length];

      for (int i = 0; i < z.Length; i++)
      {
        if (z[i] >= 0)
        {
          res[i] = z[i];  
        }
      }

      for (int i = 0; i < res.Length; i++)
      {
        Contract.Assert(res[i] >= 0); // True
      }

      return res;
    }

    // TODO: We need week relational information
    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 60, MethodILOffset = 0)]
    static int[] FilterUp(int[] z, int M)
    {
      Contract.Requires(M >= 0);

      int[] res = new int[z.Length];

      for (int i = 0; i < z.Length; i++)
      {
        if (z[i] >= M)
        {
          res[i] = z[i];
        }
      }

      // It seems it does not infer res[*] >= 0

      for (int i = 0; i < res.Length; i++)
      {
        Contract.Assert(res[i] >= M); // True, but we cannot prove it yet, without relational information
      }

      return res;
    }

    // TODO: We need relational segment indexes
    [ClousotRegressionTest("Intervals")]
    static void CopyArray(int[] from, int[] to)
    {
      Contract.Requires(from.Length == to.Length);

      for (int i = 0; i < from.Length; i++)
      {
        to[i] = from[i];
      }
    }
  }
}

namespace NonConsecutiveArrayAccesses
{
  class MsCorlib_Random
  {
    int[] SeedArray;

    int inext;
    int inextp;

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 75, MethodILOffset = 0)]
    public void Random_0_OK(int Seed, int posValue)
    {
      Contract.Requires(posValue > 0);

      this.SeedArray = new int[0x38];

      for (int i = 1; i < 0x38; i++)
      {
        int index = (0x15 * i) % 0x38;    
        this.SeedArray[index] = posValue;     // Tests non consecutive array access
      }

      //  sv22 (1808) -> {0 ,sv4 (1790)} [0, +oo] {sv21 (1807) ,56}  
      ClousotDebug.Francesco_PrintArrayContent();

      for (var i = 0; i < this.SeedArray.Length; i++)
      {
        Contract.Assert(this.SeedArray[i] >= 0);    //True
      }
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 85, MethodILOffset = 0)]
    public void Random_1_OK(int Seed, int posValue, int num2)
    {
      Contract.Requires(posValue > 0);

      this.SeedArray = new int[0x38];

      this.SeedArray[0x37] = num2;  // Set the last element to some arbitrary value

      for (int i = 1; i < 0x37; i++)
      {
        int index = (0x15 * i) % 0x37;
        this.SeedArray[index] = posValue;
      }

      // sv25 (1877) -> {0 ,sv4 (1856)} [0, +oo] {55 ,sv27 (1879)} [-oo, +oo] {sv24 (1876) ,56}
      ClousotDebug.Francesco_PrintArrayContent();

      for (var i = 0; i < this.SeedArray.Length - 1; i++)
      {
        Contract.Assert(this.SeedArray[i] >= 0);    // Should be true
      }
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 85, MethodILOffset = 0)]
    public void Random_1_NOTOK(int Seed, int posValue, int num2)
    {
      Contract.Requires(posValue > 0);

      this.SeedArray = new int[0x38]; // Set the last element to some arbitrary value

      this.SeedArray[0x37] = num2;

      for (int i = 1; i < 0x37; i++)
      {
        int index = (0x15 * i) % 0x37;
        this.SeedArray[index] = posValue;
      }

      //  sv25 (1950) -> {0 ,sv4 (1929)} [0, +oo] {55 ,sv27 (1952)} [-oo, +oo] {sv24 (1949) ,56}
      ClousotDebug.Francesco_PrintArrayContent();

      for (var i = 0; i < this.SeedArray.Length; i++)
      {
        Contract.Assert(this.SeedArray[i] >= 0);    // Should be top as the last element can be any value
      }
    }

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 77, MethodILOffset = 0)]
    public void Random_2_FromZero_OK(int Seed, int num2, int val)
    {
      Contract.Requires(val > 0);

      this.SeedArray = new int[0x38];

      this.SeedArray[0x37] = num2;      // The fact we were setting the last element exposed an unsoundness in the materialization in the loop

      for (int k = 0; k < 0x38; k++)
      {
        this.SeedArray[k] = val;
      }

      //  sv25 (1950) -> {0 ,sv4 (1929)} [0, +oo] {55 ,sv27 (1952)} [-oo, +oo] {sv24 (1949) ,56}
      ClousotDebug.Francesco_PrintArrayContent();
      for (var i = 0; i < this.SeedArray.Length; i++)
      {
        Contract.Assert(this.SeedArray[i] >= 0);      // ok
      }
    }

    // TODO: need to push the info k=1
    //[ClousotRegressionTest("Intervals")]
    public void Random_2_FromOne_Ok(int Seed, int num2, int val)
    {
      Contract.Requires(val > 0);

      this.SeedArray = new int[0x38];

      this.SeedArray[0x37] = num2;

      for (int k = 1; k < 0x38; k++)      // Here k starts from 1
      {
        ClousotDebug.Francesco_PrintArrayContent();
        this.SeedArray[k] = val;
        ClousotDebug.Francesco_PrintArrayContent();
      }

      ClousotDebug.Francesco_PrintArrayContent();
      for (var i = 0; i < this.SeedArray.Length; i++)
      {
        Contract.Assert(this.SeedArray[i] >= 0);      // True, but we cannot prove it yet
      }
    }


    // The constructor of the Random class in mscorlib
    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: value != -2147483648",PrimaryILOffset=17,MethodILOffset=19)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 82, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 125, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 212, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 307, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 376, MethodILOffset = 0)]
    public void Random_3_WithManualLoopUnrolling(int Seed)
    {
      this.SeedArray = new int[0x38];
      int num2 = 0x9a4ec86 - Math.Abs(Seed);
      this.SeedArray[0x37] = num2;
      int num3 = 1;
      for (int i = 1; i < 0x37; i++)
      {
        int index = (0x15 * i) % 0x37;
        this.SeedArray[index] = num3;
        num3 = num2 - num3;
        if (num3 < 0)
        {
          num3 += 0x7fffffff;
        }

        Contract.Assert(num3 >= -1);   // ok

        num2 = this.SeedArray[index];
      }

      for (var i = 0; i < this.SeedArray.Length - 1; i++)
      {
        Contract.Assert(this.SeedArray[i] >= -1);   // ok
      }

      // F: We do one loop unrolling
      ClousotDebug.Francesco_PrintArrayContent();
      for (int k = 0; k < 0x38; k++)
      {
        var val = this.SeedArray[k] - this.SeedArray[1 + ((k + 30) % 0x37)];

        if (val < 0)
        {
          val += 0x7fffffff;
        }

        Contract.Assert(val >= -1);    // ok
        this.SeedArray[k] = val;
      }

      for (int j = 2; j < 5; j++)
      {
        ClousotDebug.Francesco_PrintArrayContent();
        for (int k = 0; k < 0x38; k++)
        {
          var val = this.SeedArray[k] - this.SeedArray[1 + ((k + 30) % 0x37)];

          if (val < 0)
          {
            val += 0x7fffffff;
          }

          Contract.Assert(val >= -1);
          this.SeedArray[k] = val;
        }
        ClousotDebug.Francesco_PrintArrayContent();
      }

      ClousotDebug.Francesco_PrintArrayContent();
      for (var i = 0; i < this.SeedArray.Length; i++)
      {
        Contract.Assert(this.SeedArray[i] >= -1);    // ok   
      }
    }

  }

  public class ClousotDebug
  {
    static internal void Francesco_PrintArrayContent()
    {
    }
  }
}

namespace BugRepros
{
  public class ThrownExceptions
  {
    public int[] SeedArray;

    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message=@"assert unreachable",PrimaryILOffset=52,MethodILOffset=0)]
    public void TestUnification_OutOfBounds(int val, bool b)
    {
      this.SeedArray = new int[0x38];

      this.SeedArray[0x38] = val; // definitely wrong indexing,  it was causing a crash in the analsys

      if (b)
      {
        this.SeedArray[0x15] = 22;
      }

      Contract.Assert(this.SeedArray[0x15] >= 0);
    }
  }

  public class Join
  {
    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=32,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=47,MethodILOffset=0)]
    public void TestUnification0(int val, bool b)
    {
      var loc = new int[0x38];

      loc[0x37] = val; // can be negative

      if (b)
      {
        loc[0x15] = 22;
      }

      Contract.Assert(loc[0x15] >= 0);    // true
      Contract.Assert(loc[0x37] >= 0);    // top
    }
  }

  public class ArrayEqualityTest
  {
    [ClousotRegressionTest("Intervals")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=25,MethodILOffset=0)]
    public void TwoArrays(int[] a, int[] b)
    {
      Contract.Requires(a.Length > 1);

      a[0] = 1;
      if (a == b)
      {
        Contract.Assert(b[0] == 1); // True because we know a and b are the same array
      }
    }
  }
}

namespace MethodCalls
{
  public class Havoc
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: a != null",PrimaryILOffset=7,MethodILOffset=10)] // We are not running -nonnull in this test
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=18,MethodILOffset=10)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=22,MethodILOffset=0)]
    public void CallWithSideEffects()
    {
      var array = new int[16];
      Write(array);                    // Here we havoc the array content
      Contract.Assert(array[3] == 12); // unproven
    }

    private void Write(int[] a)
    {
      Contract.Requires(a != null);
      Contract.Requires(a.Length > 3);
      a[3] = 12;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: input != null",PrimaryILOffset=7,MethodILOffset=13)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=25,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"assert is false",PrimaryILOffset=37,MethodILOffset=0)] 
    public void CallWithNoSideEffects()
    {
      var array = new int[256];
      Read(array);                     // no side effects
      Contract.Assert(array[10] == 0); // true
      Contract.Assert(array[12] == 1); // false
    }

    [Pure]
    private void Read(int[] input)
    {
      Contract.Requires(input != null);
      for (var i = 0; i < input.Length; i++)
      {
        Console.WriteLine(i);
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=37,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=53,MethodILOffset=0)]
    public void CallWithMixedEffects()
    {
      var read = new int[256];
      var write = new int[1024];
      Read(read, write); // no side effects on read
      Contract.Assert(read[10] == 0); // true
      Contract.Assert(write[12] == 1111); // true, we cannot prove it because we do not propagate ForAll arguments, so top is ok. 
    }

    // read is not annotated to not be modified
    private void Read([Pure] int[] read, int[] write)
    {
      var sum = 0;
      for (var i = 0; i < read.Length; i++)
      {
        sum += read[i];
      }

      for (var i = 0; i < write.Length; i++)
      {
        write[i] = 1111;
      }
    }
  }
}

namespace OutRefParameters
{
  public class TestCases
  {
    [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: collection != null (collection)",PrimaryILOffset=17,MethodILOffset=47)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: predicate != null (predicate)",PrimaryILOffset=39,MethodILOffset=47)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=3,MethodILOffset=47)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=25,MethodILOffset=47)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven",PrimaryILOffset=22,MethodILOffset=47)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven",PrimaryILOffset=44,MethodILOffset=47)]
	#endif
#endif
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=52,MethodILOffset=0)]
    public static void Example()
    {
      int[] myArray = new int[100];

      WriteSomething(myArray[10]);

      Contract.Assert(Contract.ForAll(myArray, el => el == 0)); // true
    }

    [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: collection != null (collection)",PrimaryILOffset=17,MethodILOffset=51)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: predicate != null (predicate)",PrimaryILOffset=39,MethodILOffset=51)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=3,MethodILOffset=51)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=25,MethodILOffset=51)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven",PrimaryILOffset=22,MethodILOffset=51)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven",PrimaryILOffset=44,MethodILOffset=51)]
	#endif
#endif
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=56,MethodILOffset=0)]
    public static void ExampleWithRef()
    {
      int[] myArray = new int[100];
      
      WriteSomething(ref myArray[10]);

      Contract.Assert(Contract.ForAll(myArray, el => el == 0)); // top
    }

    [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: collection != null (collection)",PrimaryILOffset=17,MethodILOffset=51)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: predicate != null (predicate)",PrimaryILOffset=39,MethodILOffset=51)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=3,MethodILOffset=51)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=25,MethodILOffset=51)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven",PrimaryILOffset=22,MethodILOffset=51)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven",PrimaryILOffset=44,MethodILOffset=51)]
	#endif
#endif
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=56,MethodILOffset=0)]
    public static void ExampleWithOut()
    {
      int[] myArray = new int[100];

      WriteSomethingOut(out myArray[10]);

      Contract.Assert(Contract.ForAll(myArray, el => el == 0));  // top

    }

    private static void WriteSomething(int x)
    {
      x = DateTime.Now.Millisecond;
    }

    private static void WriteSomething(ref int x)
    {
      x = DateTime.Now.Millisecond;
    }

    private static void WriteSomethingOut(out int x)
    {
      x = DateTime.Now.Millisecond;
    }
  }
}

namespace SymbolicPropagationOfConditions
{
  public class Tests
  {
    [ClousotRegressionTest]

#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=13,MethodILOffset=78)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: predicate != null (predicate)",PrimaryILOffset=35,MethodILOffset=78)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=3,MethodILOffset=78)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=25,MethodILOffset=78)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=22,MethodILOffset=78)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven",PrimaryILOffset=44,MethodILOffset=78)]
	#endif
#endif    
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=83,MethodILOffset=0)]
    static public int[] Filter(int[] origin)
    {
      var result = new int[origin.Length];
      int j = 0;
      for (int i = 0; i < origin.Length; i++)
      {
	// This expression is outside the expressivity of Clousot numerical domains, but we propagate it symbolically anyway
        if (origin[i] % 2345 + 2== 0)
        {
          result[j] = origin[i];
          j++;
        }
      }

      Contract.Assert(Contract.ForAll(0, j, indx => result[indx] % 2345 + 2 == 0));

      return result;
    }
  } 
}

namespace Disequalities
{
    public class Search
  {
    [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=17,MethodILOffset=96)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: predicate != null (predicate)",PrimaryILOffset=39,MethodILOffset=96)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=3,MethodILOffset=96)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=25,MethodILOffset=96)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=22,MethodILOffset=96)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven",PrimaryILOffset=44,MethodILOffset=96)]
	#endif
#endif
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=36,MethodILOffset=72)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=51,MethodILOffset=72)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=101,MethodILOffset=0)]
    public int LinearSearch(int[] a, int value)
    {
      Contract.Requires(a != null);
   
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() < a.Length);

      for (var i = 0; i < a.Length; i++)
      {
        if (a[i] == value)
        {
          return i;
        }
      }

      Contract.Assert(Contract.ForAll(a, el => el != value)); // if we reach this point, no element was found

      throw new Exception();
    }
  }
}

namespace FromMsCorlib
{
  public class SomeByteManipulation
  {
    [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=13,MethodILOffset=196)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: predicate != null (predicate)",PrimaryILOffset=35,MethodILOffset=196)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=3,MethodILOffset=196)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=25,MethodILOffset=196)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=22,MethodILOffset=196)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven",PrimaryILOffset=44,MethodILOffset=196)]
	#endif
#endif 
   [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=201,MethodILOffset=0)]
    private void MarshalHeader(byte[] binaryForm, int offset)
    {
      Contract.Requires(offset >= 0);
      Contract.Requires(offset + 8 < binaryForm.Length);
      
      Contract.Requires(Contract.ForAll(binaryForm, b => b < 10));
      
      binaryForm[offset] = 11;
      binaryForm[offset + 1] = 10;
      binaryForm[offset + 2] = 13;
      binaryForm[offset + 3] = 14;
      binaryForm[offset + 4] = 10;
      binaryForm[offset + 5] = 11;
      binaryForm[offset + 6] = 10;
      binaryForm[offset + 7] = 10;

      Contract.Assert(Contract.ForAll(0, offset, i => binaryForm[i] < 10)); // So we know we had the elements up to offset are untouched
    }
  }
}

namespace FalseRepro
{
    public class Repro
    {
        [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=23,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=34,MethodILOffset=0)]
#else
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=27,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=38,MethodILOffset=0)]
#endif
        public byte[] RemoveWhiteSpace(byte[] data1)
        {
            int count = data1.Length;
            int j = 0;
            for (int i = 0; i < data1.Length; i++, j++)
            {
                var data = new byte[j];
                for (int idx = 0; idx < data.Length; idx++)
                {
                    Contract.Assert(j== data.Length);
                // var tmp = temp[idx];
                    Contract.Assert(idx < data.Length); // We used to say false at this exp, because they array analysis thought we entered the loop at the first iteration (when j == 0)
                //data[idx] = 0;
                }
            }
            return data1;
        }
    }
}

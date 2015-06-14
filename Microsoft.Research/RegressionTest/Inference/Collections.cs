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


namespace ExamplesPurity
{
  public class Simple
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=27,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=27,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=13,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=27,MethodILOffset=0)]
    [RegressionOutcome("Consider adding the [Pure] attribute to the parameter untouched")]
    public void ChangeFirstElementNoPure([Pure] int[] arr,  object[] untouched)
    {
      Contract.Requires(arr != null);
      Contract.Requires(arr.Length > 0);

      arr[0] = 123;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=27,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=27,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=13,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=27,MethodILOffset=0)]
    // Nothing to suggest
    public void ChangeFirstElement([Pure] int[] arr, [Pure] object[] untouched)
    {
      Contract.Requires(arr != null);
      Contract.Requires(arr.Length > 0);

      arr[0] = 123;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=23,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=23,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=30,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=23,MethodILOffset=0)]
    // Nothing to suggest
    public void ChangeAllTheElements([Pure] int[] arr)
    {
      Contract.Requires(arr != null);

      for (var i = 0; i < arr.Length; i++)
      {
        arr[i] = 1234;
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 'arr'",PrimaryILOffset=18,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(arr != null);")]
    [RegressionOutcome("Consider adding the [Pure] attribute to the parameter arr")]
    public int Unchanged(int[] arr)
    {
      var sum  = 0;
      for (var i = 0; i < arr.Length; i++)
      {
        sum += arr[i];
      }

      return sum;
    }
  }
}

namespace ForAllPreconditionInference
{
  public class Tests
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 's'",PrimaryILOffset=24,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=13,MethodILOffset=0)]
    [RegressionOutcome("Consider adding the [Pure] attribute to the parameter s")]
    [RegressionOutcome("Contract.Requires(s != null);")]
    [RegressionOutcome("Contract.Requires(Contract.ForAll(0, s.Length, i => s[i] != null));")]
    public void TestAllElements(string[] s)
    {
      for (var i = 0; i < s.Length; i++)
      {
        Contract.Assert(s[i] != null);
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 's'",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=13,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(s != null);")]
    [RegressionOutcome("Contract.Requires(Contract.ForAll(0, s.Length, i => s[i] != null));")]
    public void TestAllElementsAndChangeThem(string[] s)
    {
      for (var i = 0; i < s.Length; i++)
      {
        Contract.Assert(s[i] != null);
        s[i] = null;
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 's'",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=17,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(s != null);")]
    [RegressionOutcome("Consider adding the [Pure] attribute to the parameter s")]
    [RegressionOutcome("Contract.Requires(Contract.ForAll(0, s.Length, i => s[i] != null));")]
    public void TestAllElementsBackwards(string[] s)
    {
      for (var i = s.Length - 1; i >= 0; i--)
      {
        Contract.Assert(s[i] != null);
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 's'",PrimaryILOffset=24,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(s != null);")]
    [RegressionOutcome("Consider adding the [Pure] attribute to the parameter s")]
    [RegressionOutcome("Contract.Requires(Contract.ForAll(0, s.Length, i => s[i] != null));")]
    public void AssumeAllElements(string[] s)
    {
      for (var i = 0; i < s.Length; i++)
      {
        Contract.Assume(s[i] != null);
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 's'",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(s != null);")]
    [RegressionOutcome("Contract.Requires(Contract.ForAll(0, s.Length, i => s[i] != null));")]
    public void AssumeAllElementsAndChangeThem(string[] s)
    {
      for (var i = 0; i < s.Length; i++)
      {
        Contract.Assume(s[i] != null);
        s[i] = null;
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=2,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=2,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 's'",PrimaryILOffset=2,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(3 < s.Length);")]
    [RegressionOutcome("Contract.Requires(s != null);")]
    [RegressionOutcome("Consider adding the [Pure] attribute to the parameter s")]
    [RegressionOutcome("Contract.Requires(Contract.ForAll(3, 4, i => s[i] != null));")]
    public void AssertOneElement(string[] s)
    {
      Contract.Assert(s[3] != null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=3,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=3,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 's'",PrimaryILOffset=3,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(12 < s.Length);")]
    [RegressionOutcome("Contract.Requires(s != null);")]
    [RegressionOutcome("Consider adding the [Pure] attribute to the parameter s")]
    public int DoNotTestOneElement(string[] s)
    {
      int v;
      if (s[12] == null)
      {
        v = 0;
      }
      else
      {
        v = 1;
      }

      return v;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 's'",PrimaryILOffset=23,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(s != null);")]
    [RegressionOutcome("Consider adding the [Pure] attribute to the parameter s")]
    [RegressionOutcome("Contract.Requires(Contract.ForAll(0, s.Length, i => s[i] != null));")]
    public int Sum(string[] s)
    {
      var sum = 0;
      for (var i = 0; i < s.Length; i++)
      {
        sum += s[i].Length;
      }

      return sum;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 's'",PrimaryILOffset=23,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly accessing a field on a null reference",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(s != null);")]
    [RegressionOutcome("Consider adding the [Pure] attribute to the parameter s")]
    [RegressionOutcome("Contract.Requires(Contract.ForAll(0, s.Length, i => s[i] != null));")]
    public int Sum(Dummy[] s)
    {
      var sum = 0;
      for (var i = 0; i < s.Length; i++)
      {
        sum += s[i].f;
      }

      return sum;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 's'. The static checker determined that the condition 's != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(s != null);",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly accessing a field on a null reference",PrimaryILOffset=10,MethodILOffset=0)]
      //    [RegressionOutcome("Contract.Requires(s != null);")] // unsound to infer it
    [RegressionOutcome("Consider adding the [Pure] attribute to the parameter s")]
    public int Sum(Dummy[] s, int end)
    {
      var sum = 0;
      for (var i = 0; i < end; i++)
      {
        sum += s[i].f;
      }

      return sum;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=31,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=31,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 'arr'",PrimaryILOffset=14,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=50,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=31,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly accessing a field on a null reference",PrimaryILOffset=32,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(arr != null);")]
    [RegressionOutcome("Consider adding the [Pure] attribute to the parameter arr")]
    [RegressionOutcome("Contract.Requires(Contract.ForAll(left, arr.Length, i => arr[i] != null));")]
    static public int PartitionWithLocal(Dummy[] arr, int left, int pivot)
    {
      Contract.Requires(left >= 0);
      Contract.Requires(left < arr.Length);

      var counter = left;

      var z = 0;

      while (counter < arr.Length )
      {
        if (arr[counter].f < pivot)
        {
          z++;
        }

        counter++; 
      }

      return z;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=15,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=15,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=30,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=15,MethodILOffset=0)]
    [RegressionOutcome("Consider adding the [Pure] attribute to the parameter genericArguments")]
    [RegressionOutcome("Contract.Requires(Contract.ForAll(0, genericArguments.Length, i => genericArguments[i] != null));")]
    static void SanityCheckArguments(object[] genericArguments)
    {
      if (genericArguments == null)
      {
        throw new ArgumentNullException();
      }
      for (int i = 0; i < genericArguments.Length; i++)
      {
        if (genericArguments[i] == null)
        {
          throw new ArgumentNullException();
        }
      }
    }
    /*
    [ClousotRegressionTest]
    public int TestList(List<Dummy> list)
    {
      var x = 0;
      foreach (var d in list)
      {
        x -= d.f;
      }

      return x;
    }

    [ClousotRegressionTest]
    public int TestListWithContract(List<Dummy> list)
    {
      Contract.Requires(Contract.ForAll(list, v => v != null));

      var x = 0;
      foreach (var d in list)
      {
        x -= d.f;
      }

      return x;
    }
    */
    public class Dummy
    {
      public int f;
    }
    
  }

  public class ExamplesWithControlFlow
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=12,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=12,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 's'. The static checker determined that the condition 's != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(s != null);",PrimaryILOffset=30,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=12,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=19,MethodILOffset=0)]
      //[RegressionOutcome("Contract.Requires(s != null);")] // F: this is incorrect, should not be suggested
    [RegressionOutcome("Consider adding the [Pure] attribute to the parameter s")]
    // Correct not to infer ForAll(0, s.Length, i => s[i] != null)
    public void NotAllThePaths(int x, string[] s)
    {
      if (x > 10)
        return;
      
      for (var i = 0; i < s.Length; i++)
      {
        Contract.Assert(s[i] != null);
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=37,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=37,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=50,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=37,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference",PrimaryILOffset=38,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome("Consider adding the [Pure] attribute to the parameter arrStr")]
    [RegressionOutcome("Contract.Requires(Contract.ForAll(0, arrStr.Length, i => arrStr[i] != null));")]
    public void Branches(bool b, string[] arrStr)
    {
      Contract.Requires(arrStr != null);
      int len = 0;
      for(var i = 0; i < arrStr.Length; i++)
	{
	  if(b)
	    {
	      Contract.Assert(arrStr[i] != null);
	    }
	  else
	    {
	      len = arrStr[i].Length;
	    }
	}
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=40,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=40,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=43,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=43,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 'array'",PrimaryILOffset=20,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=40,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=43,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=50,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(array != null);")]
    [RegressionOutcome("Contract.Requires(Contract.ForAll(0, 1, i => array[i] != null));")]
    public void WriteNotInTheFirstPosition(object[] array, int index)
    {
      Contract.Requires(index > 0);
      Contract.Requires(index < 10);
      Contract.Requires(array.Length > 10);

      array[index] = 34;
      Contract.Assert(array[0] != null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be below the lower bound",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=24,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=24,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 'array'",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=24,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=31,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(0 <= index);")]
    [RegressionOutcome("Contract.Requires(index < array.Length);")]
    [RegressionOutcome("Contract.Requires(array != null);")]
    // nothing to infer on array content
    public void Writesomewhere_NoPrecondition(object[] array, int index)
    {
      Contract.Requires(array.Length > 10);

      array[index] = 34;
      Contract.Assert(array[0] != null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=114,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=114,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=126,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=126,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=138,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=138,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=150,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=150,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=162,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=162,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=174,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=174,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=186,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=186,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=198,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=198,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=8,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=51,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=8,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=106,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=114,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=116,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=126,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=128,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=138,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=140,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=150,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=152,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=162,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=164,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=174,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=176,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=186,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=188,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=198,MethodILOffset=0)]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=13,MethodILOffset=213)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=35,MethodILOffset=213)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=3,MethodILOffset=213)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=25,MethodILOffset=213)]
	#else
		[RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 22, MethodILOffset = 213)]
		[RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 44, MethodILOffset = 213)]
	#endif
#endif
#if CLOUSOT2
	// nothing? 
#else
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 14, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 46, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 61, MethodILOffset = 0)]
#endif
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=218,MethodILOffset=0)]
    /*
Effect on the input array segment: Contract.ForAll(0, offset, Contract.Old(binaryForm[i]) == binaryForm[i])
Effect on the input array segment: Contract.ForAll(offset, offset + 1, changed?)
Effect on the input array segment: Contract.ForAll(offset + 1, offset + 2, changed?)
Effect on the input array segment: Contract.ForAll(offset + 2, offset + 3, changed?)
Effect on the input array segment: Contract.ForAll(offset + 3, offset + 4, changed?)
Effect on the input array segment: Contract.ForAll(offset + 4, offset + 5, changed?)
Effect on the input array segment: Contract.ForAll(offset + 5, offset + 6, changed?)
Effect on the input array segment: Contract.ForAll(offset + 6, offset + 7, changed?)
Effect on the input array segment: Contract.ForAll(offset + 7, offset + 8, changed?)
Effect on the input array segment: Contract.ForAll(offset + 8, binaryForm.Length, Contract.Old(binaryForm[i]) == binaryForm[i])
*/
    public void MarshalHeader(byte[] binaryForm, int offset)
    {
      Contract.Requires(binaryForm != null);
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

      Contract.Assert(Contract.ForAll(0, offset, i => binaryForm[i] < 10));
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=2,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=2,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=16,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=16,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=26,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=26,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 'arr'",PrimaryILOffset=2,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference",PrimaryILOffset=3,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=39,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=16,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference",PrimaryILOffset=17,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=26,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=27,MethodILOffset=0)]
    [RegressionOutcome("Consider adding the [Pure] attribute to the parameter arr")]
    [RegressionOutcome("Contract.Requires(Contract.ForAll(0, 1, i => arr[i] != null));")]
    [RegressionOutcome("Contract.Requires(Contract.ForAll(1, arr.Length, i => arr[i] != null));")]
    [RegressionOutcome("Contract.Requires(arr != null);")]
    [RegressionOutcome("Contract.Requires(0 < arr.Length);")] 
    static public int Max(string[] arr)
    {
      var max = arr[0].Length;
      for (var i = 1; i < arr.Length; i++)
	{
	  if (max < arr[i].Length)
	    {
	      max = arr[i].Length;
	    }
	}
      
      return max;
    }
  }
  
  public class FromSystemData
  {
    public int[] _columnSmiMetaData;
    
    // F: Inference not working here: we infer the right invariant, but then we cannot read
    // this._columnSmiMetaData.Length in the prestate
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=20,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=20,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=56,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 'this._columnSmiMetaData'",PrimaryILOffset=61,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=20,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(this._columnSmiMetaData != null);")]
    [RegressionOutcome("Consider adding the [Pure] attribute to the parameter metaData")]
    public void Test(params object[] metaData)
    {
      if (metaData == null)
	{
        throw new Exception("metadata");
      }
      for (int i = 0; i < this._columnSmiMetaData.Length; i++)
      {
        if (metaData[i] == null)
        {
          throw new Exception("metadata[" + i + "]");
        }
      }
    }
  }
}

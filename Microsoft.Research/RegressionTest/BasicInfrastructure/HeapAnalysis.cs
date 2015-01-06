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
using System.Text;

class ZeroBranchRefinement
{
  class TestValue {
    public int F;
  }

  TestValue DisjointEnsures(bool b)
  {
    Contract.Ensures(Contract.Result<TestValue>() == null || Contract.Result<TestValue>().F >= 5);
    if (b) return null;
    TestValue v = new TestValue();
    v.F = 5;
    return v;
  }

  [ClousotRegressionTest("regular")]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = (int)3, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = (int)23, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)34, MethodILOffset = 0)]
  public void Test(bool b)
  {
    TestValue v = DisjointEnsures(b);
    if (v == null) return;
    Contract.Assert(v.F >= 5);
  }

}

public class Recursion
{
  Recursion m_child;
  string s;

  public Recursion(string owner)
  {
    s = owner;
  }
  [ClousotRegressionTest("regular")]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 6, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 21, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference \'this.s\'", PrimaryILOffset = 26, MethodILOffset = 0)]
  public Recursion()
    : this("foo")
  {
    this.m_child = this;

    // this.m_Parent = owner;

    // Stack overflow on next line analysis   
    this.s.ToString();

    //this.m_child.IsOpen = true;
  }

}

public class FaultFinallyContexts
{
  static void M(object s)
  {
    Contract.Requires(s != null);
  }

  [ClousotRegressionTest]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 39, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: s != null", PrimaryILOffset = 8, MethodILOffset = 54)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 8, MethodILOffset = 0)]
  public static void FFCTest1(FaultFinallyContexts obj)
  {
    try
    {
      Console.WriteLine("TEST");
      if (obj == null)
      {
        goto End;
      }
    }
    finally
    {
      M(obj);
      Console.WriteLine(obj.ToString());
    }
  End:
    Console.WriteLine("TEST");
  }
}

class ArrayElements
{

  [Pure]
  static bool LessThan(int x, int y)
  {
    Contract.Ensures(Contract.Result<bool>() == (x < y));

    return x < y;
  }

  [ClousotRegressionTest]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 66, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 66, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 69, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 69, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 83, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 83, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 87, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 87, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 29, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 54, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 66, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 69, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 83, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 87, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 93, MethodILOffset = 0)]
  void Test2(int[] arr, int i, int j)
  {
    Contract.Requires(arr != null);
    Contract.Requires(i >= 0);
    Contract.Requires(i < arr.Length);
    Contract.Requires(j >= 0);
    Contract.Requires(j < arr.Length);
    Contract.Requires(LessThan(arr[i], arr[j]));

    int x = arr[i];
    int y = arr[j];
    Contract.Assert(x < y);
  }

}

namespace OldHandlingOfStack {
  public struct S
  {
    public T t;
  }

  public struct T
  {
    public int x;
  }

  public class Config
  {

    [Pure]
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=13,MethodILOffset=28)]
    public static bool PureMethod(int a, int b, int c, int d, int e, int f)
    {
      Contract.Ensures(Contract.Result<bool>() == f > b);
      return f > b;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=44,MethodILOffset=55)]
    static int Test(S s)
    {
      Contract.Requires(s.t.x > 0);
      Contract.Ensures(PureMethod(1, 0, 3, 4, 5, s.t.x));
      return 0;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=17,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=16,MethodILOffset=23)]
    static void Main()
    {
      S s = new S();
      s.t.x = 1;
      Test(s);
    }

  }
}

namespace RecursiveExpressions {
  class Repro {

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=26,MethodILOffset=71)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=57,MethodILOffset=71)]
    public static bool Test(out string result)
    {
      Contract.Ensures(!(!Contract.Result<bool>() == (Contract.ValueAtReturn(out result) != null)));
      Contract.Ensures(!(!Contract.Result<bool>() == (Contract.ValueAtReturn(out result) != null)));

      result = null;
      return false;
    }

  }
}

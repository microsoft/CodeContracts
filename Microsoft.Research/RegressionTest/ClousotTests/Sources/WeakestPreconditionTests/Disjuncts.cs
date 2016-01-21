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
using System.Diagnostics;
using Microsoft.Research.ClousotRegression;

namespace DisjunctTests
{

  public enum Case
  {
    A,
    B,
    C,
  }

  public class ForwardVsBackward
  {

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 29, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 48, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 76, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 8, MethodILOffset = 9)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 40, MethodILOffset = 0)]
    public static void Test()
    {

      int x = 0;

      for (int i = 0; i < 100; i++)
      {

        A a = GetA(i);
        // here we have a disjunctive invariant

        if (a == null) continue;
        Debug.Assert(a.f >= 0);
        x += a.f;

      }
      Debug.Assert(x >= 0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 72, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 26, MethodILOffset = 94)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 83, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 40, MethodILOffset = 94)]
    static A GetA(int i)
    {
      Contract.Requires(i >= 0);
      Contract.Ensures(Contract.Result<A>() == null ||
                        Contract.Result<A>().f >= 0);

      if (i % 2 == 0)
      {
        return null;
      }

      var a = new A(i);
      Contract.Assert(a.f >= 0);
      return a;
    }

    public class A
    {
      public int f;

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 25, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 9, MethodILOffset = 31)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 17, MethodILOffset = 31)]
      public A(int x)
      {
        Contract.Ensures(this.f == x);
        this.f = x;
      }

    }


    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 86, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 74, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 62, MethodILOffset = 0)]
    public static void Correlation(Case c, int x)
    {
      Contract.Requires(c == Case.A && x > 0 || c == Case.B && x < 0 || c == Case.C && x == 0);

      switch (c)
      {
        case Case.A:
          Contract.Assert(x > 0);
          break;

        case Case.B:
          Contract.Assert(x < 0);
          break;

        case Case.C:
          Contract.Assert(x == 0);
          break;
      }
    }

  }
}

namespace Herman
{
  [ContractClass(typeof(AContracts))]
  interface A
  {
    int foo();
  }

  interface B
  {
    void Example();
  }

  [ContractClassFor(typeof(A))]
  abstract class AContracts : A
  {
    public int foo()
    {
      Contract.Ensures(!(this is B) || Contract.Result<int>() > -234);

      return default(int);
    }
  }

  public class MyClass
    : A
  {
   [ClousotRegressionTest]
   [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=24,MethodILOffset=28)]
   public int foo()
    {
      Contract.Assume(!(this is B));

      return int.MinValue;
    }
  }
}

namespace Maf
{
    public class NormalizationBug
    {
	  // we were not normalizing 1 == (value != null) ---> (value != null)
      [Pure]
	  [ClousotRegressionTest]
	  [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=15,MethodILOffset=42)]
      private bool ValidNonNull(object value)
      {	   
        Contract.Ensures(Contract.Result<bool>() == (value != null));

        if (value == null)
        {
          return false;
        }
        return true;
      }

      [Pure]
	  [ClousotRegressionTest]
	  [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=15,MethodILOffset=42)]
      private bool ValidNonNull2(object value)
      {
        Contract.Ensures((value != null) == Contract.Result<bool>());

        if (value == null)
        {
          return false;
        }
        return true;
      }
	}
}
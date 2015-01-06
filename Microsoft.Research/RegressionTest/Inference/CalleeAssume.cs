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

namespace AssumeSuggestion
{
   public class CalleeAssumeSuggestions
  {
    [ContractVerification(false)]
    public int ShouldReturnAPositive()
    {
      return 123;
    }

    [ClousotRegressionTest]
	[RegressionOutcome("This condition involving the return value of ShouldReturnAPositive should hold to avoid an error later (obligation x > 0): Contract.Result<System.Int32>() > 0")]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=11,MethodILOffset=0)]
	public void Test0()
    {
      var x = ShouldReturnAPositive();
      Contract.Assert(x > 0);
    }

    [ClousotRegressionTest]
	[RegressionOutcome("This condition involving the return value of ShouldReturnAPositive should hold to avoid an error later (obligation false): Contract.Result<System.Int32>() > 0")]
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="assert is false",PrimaryILOffset=19,MethodILOffset=0)]
    public void Test1()
    {
      var x = ShouldReturnAPositive();

      var y = ShouldReturnAPositive();
      if (y <= 0)
      {
        Contract.Assert(false);
      }
    }

    [ClousotRegressionTest]
	[RegressionOutcome("This condition involving the return value of ShouldReturnAPositive (in this or in another invocation) should hold to avoid an error later (obligation x > 0): Contract.Result<System.Int32>() > 0")]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=23,MethodILOffset=0)]
    public void Test2(bool b)
    {
      int x;
      if (b)
      {
        x = ShouldReturnAPositive();
      }
      else
      {
        x = ShouldReturnAPositive();
      }

      Contract.Assert(x > 0);
    }

    [ContractVerification(false)]
    public object ShouldReturnANonNull()
    {
      return null;
    }

    [ClousotRegressionTest]
	[RegressionOutcome("This condition involving the return value of ShouldReturnANonNull should hold to avoid an error later (obligation obj != null): Contract.Result<System.Object>() != null")]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'obj'",PrimaryILOffset=8,MethodILOffset=0)]
    public int Test3()
    {
      var obj = ShouldReturnANonNull();
      return obj.GetHashCode();
    }
	
    [ContractVerification(false)]
    public int ShouldReturnAPositive(int z)
    {
      return 123 + z;
    }

	[ClousotRegressionTest]
	[RegressionOutcome("This condition involving the return value of ShouldReturnAPositive should hold to avoid an error later (obligation x > 0): Contract.Result<System.Int32>() > 0")]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=12,MethodILOffset=0)]
    public void TestWithAParam0(int z)
    {
      var x = ShouldReturnAPositive(z);

      Contract.Assert(x > 0);
    }

	[ClousotRegressionTest]
	[RegressionOutcome("This condition involving the return value of ShouldReturnAPositive should hold to avoid an error later (obligation (x + z) > 0): (Contract.Result<System.Int32>() + z) > 0")]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=14,MethodILOffset=0)]
    public void TestWithAParam1(int z)
    {
      var x = ShouldReturnAPositive(z);

      Contract.Assert(x + z > 0);
    }

    [ContractVerification(false)]
    [Pure]
    public int DummyFunction(int z)
    {
      return 2;
    }
	
	[ClousotRegressionTest]
	[RegressionOutcome("This condition involving the return value of ShouldReturnAPositive should hold to avoid an error later (obligation w > 0): Contract.Result<System.Int32>() > 0")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=21,MethodILOffset=0)]
	public void TestCallThroughPure()
    {
      var w = ShouldReturnAPositive(12);
      var dummy = DummyFunction(w);

      Contract.Assert(w > 0);
    }
  }
}
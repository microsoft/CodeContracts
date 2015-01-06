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
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;


namespace TestCheckFalsePostconditions
{
  class StackOverflow
  {
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="ensures is false: Contract.Result<int>() >= 0",PrimaryILOffset=11,MethodILOffset=17)]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Method ensures (and invariants) are in contradiction with the detected code behavior. If this is wanted, consider adding Contract.Ensures(false) to document that it never returns normally",PrimaryILOffset=16,MethodILOffset=0)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Method ensures (and invariants) are in contradiction with the detected code behavior. If this is wanted, consider adding Contract.Ensures(false) to document that it never returns normally",PrimaryILOffset=17,MethodILOffset=0)]
#endif
	public static int GetNonNegativeValue()
    {
      Contract.Ensures(Contract.Result<int>() >= 0);
      return -1;
    }

    [ClousotRegressionTest]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=2,MethodILOffset=0)]
#endif
    public static int GetNonNegativeValue_NoContracts(int x)
    {
      Contract.Assume(x > 0);
      Contract.Assume(x <0);

      return -1;
    }

	[ClousotRegressionTest]
	// We infer false, but we say nothing as the method has an ensures(false), so the user already documented it
    public static int GetNonNegativeValue_EnsuresFalse(int x)
    {
      Contract.Ensures(false);

      Contract.Assume(x > 0);
      Contract.Assume(x < 0);

      return -1;
    }


	[ClousotRegressionTest]
	// We infer false, but we say nothing as the method throws
    public static void ThrowAlways()
    {
      throw new Exception("I alwaysThrow");
    }

	[ClousotRegressionTest]
	// We infer false, but we say nothing as the method throws 
    public static void ThrowAlwaysWithEnsures()
    {
      Contract.Ensures(false, "Document that this method never returns normally");

      throw new Exception("I alwaysThrow");
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="assert is false",PrimaryILOffset=10,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
    public static void EnsureTest()
    {
      var i = GetNonNegativeValue();
      Contract.Assert(i < 0);
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=15,MethodILOffset=0)]
    public static void EnsureTestWithoutContractPropagation()
    {
      var z = GetNonNegativeValue_NoContracts(321);
      Contract.Assert(z == -1); // We have no inference on, so we should not prove it
    }
  }
}

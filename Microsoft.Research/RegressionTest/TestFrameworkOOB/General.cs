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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using Microsoft.Research.ClousotRegression;

namespace TestFrameworkOOB
{
  class General
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 57, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 35, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 49, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 69, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
    public static void TestContractLocals(ICollection<string> coll)
    {
      Contract.Requires(coll != null);
      Contract.Requires(!coll.IsReadOnly);

      if (coll.Contains("foo")) {
        Contract.Assert(coll.Count > 0);
        coll.Remove("foo");
      }
    }


    [ClousotRegressionTest("infer")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 6, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 31, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 12, MethodILOffset = 6)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 12, MethodILOffset = 31)]
    public static int CheckThatWePropagateGenericMethodGetterRequires(int? optX, int? optY)
    {
      var result = 0;

      result += optX.Value;

      if (optY.HasValue)
      {
        result += optY.Value;
      }
      return result;
    }

    [ClousotRegressionTest("infer")]
#if CLOUSOT2
	#if SLICING
		[RegressionOutcome(Outcome=ProofOutcome.False,Message=@"requires is false: optX.HasValue",PrimaryILOffset=0,MethodILOffset=11)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.False,Message=@"requires is false: optX.HasValue",PrimaryILOffset=1,MethodILOffset=11)]
	#endif
#else
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="requires is false: optX.HasValue. This sequence of invocations will bring to an error CheckPropagationOfGenericMethodRequires -> CheckThatWePropagateGenericMethodGetterRequires, condition optX.HasValue",PrimaryILOffset=2,MethodILOffset=11)]
//    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: optX.HasValue", PrimaryILOffset = 2, MethodILOffset = 11)]
#endif
    public static void CheckPropagationOfGenericMethodRequires()
    {
      int? opt = null;
      CheckThatWePropagateGenericMethodGetterRequires(opt, opt);
    }

  }
}

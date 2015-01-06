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

namespace TestPreconditionInference
{
  public class PreSimple
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Array creation : ok", PrimaryILOffset = 2, MethodILOffset = 0)]
    bool[] Factory(int len)
    {
      return new bool[len];
    }

    [ClousotRegressionTest]
#if CLOUSOT2
	#if SLICING 
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=0,MethodILOffset=20)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=1,MethodILOffset=20)]
	#endif	
#else
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 2, MethodILOffset = 20)]
#endif
    public bool[] PublicFactory(int l)
    {
      if (l < 0)
        throw new Exception();

      return Factory(l);
    }
  }


  public class PreConditionFromPostconditions
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 4, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 4, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 8, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 8, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 12, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 12, MethodILOffset = 0)]
    private int[] A(int[] a)
    {
      a[0] = 2;
      a[1] = 3;
      a[4] = 5;

      return a;
    }

    // From the method above we infer 4 < a.Length 
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Array creation : ok", PrimaryILOffset = 2, MethodILOffset = 0)]

#if CLOUSOT2
	#if SLICING 
//		[RegressionOutcome(Outcome=ProofOutcome.False,Message=@"requires is false: 4 < a.Length",PrimaryILOffset=0,MethodILOffset=10)]
		[RegressionOutcome(Outcome=ProofOutcome.False,Message="requires is false: 4 < a.Length. This sequence of invocations will bring to an error UseA_Wrong -> A, condition 0 < a.Length",PrimaryILOffset=0,MethodILOffset=10)]
	#else
//		[RegressionOutcome(Outcome=ProofOutcome.False,Message=@"requires is false: 4 < a.Length",PrimaryILOffset=1,MethodILOffset=10)]
		[RegressionOutcome(Outcome=ProofOutcome.False,Message="requires is false: 4 < a.Length. This sequence of invocations will bring to an error UseA_Wrong -> A, condition 0 < a.Length",PrimaryILOffset=1,MethodILOffset=10)]
	#endif
#else
//    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"requires is false: 4 < a.Length",PrimaryILOffset=4,MethodILOffset=10)]
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="requires is false: 4 < a.Length. This sequence of invocations will bring to an error UseA_Wrong -> A, condition 0 < a.Length",PrimaryILOffset=4,MethodILOffset=10)]
#endif
    public void UseA_Wrong()
    {
      int[] arr = new int[1];

      A(arr);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Array creation : ok", PrimaryILOffset = 3, MethodILOffset = 0)]
#if CLOUSOT2
	#if SLICING
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=0,MethodILOffset=11)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=1,MethodILOffset=11)]
	#endif
#else
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 4, MethodILOffset = 11)]
#endif
     public void UseA_OK()
    {
      int[] arr = new int[30];

      A(arr);
    }
  }

  public class WithThrow
  {
    [ClousotRegressionTest]
    public int[] Pos(int x)
    {
      if (x >= 0)
      {
        // ok

        return null;
      }

      throw new Exception();
    }

    [ClousotRegressionTest("disabled")] // until we enable pre from post inference
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false", PrimaryILOffset = 3, MethodILOffset = 4)]
    public void CallPos_Wrong()
    {
      Pos(-5);
    }

    [ClousotRegressionTest("disabled")] // until we enable pre from post inference
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 3, MethodILOffset = 7)]
    public void CallPos_Ok()
    {
      Pos(234);
    }
  }

}

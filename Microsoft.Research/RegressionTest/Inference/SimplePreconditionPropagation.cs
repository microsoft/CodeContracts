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
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;


namespace PreconditionInferenceTests
{
  public class SimplePreInferenceAndPropogation
  {
    public object Obj;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(this.Obj != null);")]
    public int ObjNotNull()
    {
      return this.Obj.GetHashCode();
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(nn != null);")]
    private int ShouldInferNN(object nn)
    {
      return nn.GetHashCode();
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(this.Obj != null);")]
#if CLOUSOT2
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 0, MethodILOffset = 9)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=0,MethodILOffset=1)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=1,MethodILOffset=1)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 1, MethodILOffset = 9)]
#endif
      [RegressionOutcome("Contract.Requires(nn != null);")]
    public int Sum(object nn)
    {
      var v1 = this.ObjNotNull();
      var v2 = this.ShouldInferNN(nn);

      return v1 + v2;
    }

    [ClousotRegressionTest]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="requires is false: nn != null. This sequence of invocations will bring to an error FalsePrecondition -> ShouldInferNN, condition nn != null",PrimaryILOffset=0,MethodILOffset=2)]
//    [RegressionOutcome(Outcome=ProofOutcome.False,Message="requires is false: nn != null",PrimaryILOffset=0,MethodILOffset=2)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking method 'FalsePrecondition' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="requires is false: nn != null. This sequence of invocations will bring to an error FalsePrecondition -> ShouldInferNN, condition nn != null",PrimaryILOffset=1,MethodILOffset=2)]
//  [RegressionOutcome(Outcome=ProofOutcome.False,Message="requires is false: nn != null",PrimaryILOffset=1,MethodILOffset=2)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking method 'FalsePrecondition' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=2,MethodILOffset=0)]
#endif
    private int FalsePrecondition(object nn)
    {
      // not checking nn, passing null
      return this.ShouldInferNN(null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=6,MethodILOffset=0)]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=0,MethodILOffset=6)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=1,MethodILOffset=6)]
#endif
    private void CallByRef()
    {
      int x = 11;
      ByRef(ref x);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(x > -34);")]
    private bool ByRef(ref int x)
    {
      Contract.Assert(x > -34);

      return x-- > 0;
    }
  }
}

namespace ArgExceptionThrowAsAssertFalse
{
namespace Manuel
  {

    public class Caller
    {
     [ClousotRegressionTest]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=0,MethodILOffset=8)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=0,MethodILOffset=26)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=1,MethodILOffset=8)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=1,MethodILOffset=26)]
#endif
     [RegressionOutcome("Contract.Requires(s != null);")]
     [RegressionOutcome("Contract.Requires(z >= 12345);")]
      public void callHelper(string s, int z)
      {
        if (s == null)
        {
          Helper.ArgumentException("Error!!!!");
        }
        if(z < 12345)
        {
          Helper.ArgumentException("Another Error!!!!");
        }
      }
    }

    internal class Helper
    {
      [ClousotRegressionTest]
      //[RegressionOutcome(Outcome=ProofOutcome.False,Message="assert is false",PrimaryILOffset=6,MethodILOffset=0)]
		// We show this message because in this test the "throw" instruction is removed, and we have an assert(false) instead in the IL
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=6,MethodILOffset=0)]
#if CLOUSOT2
	  [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking method 'ArgumentException' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
#else
	  [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking method 'ArgumentException' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=1,MethodILOffset=0)]
#endif
	  public static void ArgumentException(string arg)
      {
        throw new ArgumentNullException(arg);
      }
    }
  }
}

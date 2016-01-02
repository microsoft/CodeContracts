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


namespace TestPostconditionInference
{
  class Basic
  {
    public Basic()
    {
    }

    [ClousotRegressionTest("nonnull")]
    public Basic NonNull
    {
      get
      {
        return new Basic();
      }
    }

    [ClousotRegressionTest("nonnull")]
    public Basic Null
    {
      get
      {
        return null;
      }
    }

    [ClousotRegressionTest("nonnull")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 20, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 14, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 30, MethodILOffset = 0)]
    public void Test()
    {
      Basic notnull = this.NonNull;

      Contract.Assert(notnull != null);
      
      Basic isnull = this.Null;

      Contract.Assert(isnull == null);
    }
  }

  class Field
  {
    Object f;

    [ClousotRegressionTest("nonnull")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0), RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 8, MethodILOffset = 0)]
    Field()
    {
      this.f = null;
    }

    [ClousotRegressionTest("nonnull")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 6, MethodILOffset = 0)]
    public void SetField()
    {
      // Infer Contract.Ensures(this.f != null)

      this.f = new Field();
    }

    // F: TODO!!!!
   // [ClousotRegressionTest("nonnull")]
    public void SetFieldWithContract()
    {
      Contract.Ensures(this.f != null);
      // should not infer Contract.Ensures(this.f != null)

      this.f = new Field();
    }

    [ClousotRegressionTest("nonnull")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0), RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 7, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 18, MethodILOffset = 0)]
     public void Test_NotEqualsToNull()
    {
      this.SetField();
      Contract.Assert(this.f != null);
    }

    [ClousotRegressionTest("nonnull")]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=2,MethodILOffset=0)]
    public void SetFieldToNull()
    {
      // infers Contract.Ensures(this.f == null)
      this.f = null;
    }

    [ClousotRegressionTest("nonnull")]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=7,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=15,MethodILOffset=0)]
    public void Test_EqualsToNull()
    {
      this.SetFieldToNull();
      Contract.Assert(this.f == null);
    }
  }
  
    public class NullField
  {
    public string s;

    [ClousotRegressionTest("nonnull")]
    [ClousotRegressionTest("nonnullreturn")]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    public NullField()
    {
	// Infer this.s == null
    //  this.s = null;
    }

    [ClousotRegressionTest("nonnull")]
    [ClousotRegressionTest("nonnullreturn")]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=12,MethodILOffset=0)]
    public NullField(int i)
    {
      s = "hello";
    }
  }

  public class UseNullField
  {
    [ClousotRegressionTest("nonnull")]
    [ClousotRegressionTest("nonnullreturn")]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=7,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="assert is false",PrimaryILOffset=18,MethodILOffset=0)]
    public void SomeMethod()
    {
      var tmp = new NullField();

      Contract.Assert(tmp.s != null); // false
    }
  }
}

namespace TestPostconditionInferenceNonNullOnly
{
  public class NonNullInference
  {
    [ClousotRegressionTest("nonnullreturn")]
    static public string ShouldInferReturnNotNull(int x)
    {
      if (x == 0)
        return "zero";
      else
        return "NotZero";
    }

   [ClousotRegressionTest("nonnullreturn")]
   [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=14,MethodILOffset=0)]
    public void Use(int z)
    {
      var str = ShouldInferReturnNotNull(z);

      Contract.Assert(str != null);
    }
  }
}

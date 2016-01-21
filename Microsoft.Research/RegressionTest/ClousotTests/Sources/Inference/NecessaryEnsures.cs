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

namespace InferenceOfNecessaryEnsures
{
  public interface MyInterface
  {
    string InterfaceMethod();

    string InterfaceMethod(string s);

    int Count { get;  }
  }
  
  public class ImplementMyInterface : MyInterface
  {
    public ImplementMyInterface()
    { }

	[ContractVerification(false)]
    public string InterfaceMethod()
    {
      return "A non null hello world!!!";
    }

	[ContractVerification(false)]
    public string InterfaceMethod(string s)
    {
      return s;
    }

    public int Count { get { return 12; } }
  }

  public abstract class AbstractClass
  {
    public abstract int[] AbstractMethod();
  }

  public class ImplementAbstractClass : AbstractClass
  {
	[ContractVerification(false)]
    public override int[] AbstractMethod()
    {
      return new int[1234];
    }
  }

	public class Test
	{
	public Test() { }
	
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'z'",PrimaryILOffset=14,MethodILOffset=0)]
	[RegressionOutcome(@"The caller expects the postcondition Contract.Ensures(Contract.Result<System.String>() != null); to hold for the interface member InterfaceMethod. Consider adding the postcondition to enforce all implementations to guarantee it")]
	public void TestNonNullViaImplementation()
    {
      var x = new ImplementMyInterface();
      var z = x.InterfaceMethod();
      var len = z.Length;      
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'm'",PrimaryILOffset=1,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=14,MethodILOffset=0)]
	[RegressionOutcome("The caller expects the postcondition Contract.Ensures(Contract.Result<System.String>() != null); to hold for the interface member InterfaceMethod. Consider adding such postcondition to enforce all implementations to guarantee it")]
	public void TestNotNullViaInterface(MyInterface m)
    {
      var tmp = m.InterfaceMethod();
      Contract.Assert(tmp != null);
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'm'",PrimaryILOffset=6,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=19,MethodILOffset=0)]
	[RegressionOutcome("The caller expects the postcondition Contract.Ensures(Contract.Result<System.String>() != null); to hold for the interface member InterfaceMethod. Consider adding such postcondition to enforce all implementations to guarantee it")]
    public void TestNotNullViaInterface2(MyInterface m)
    {
      var tmp = m.InterfaceMethod("ciao!");
      Contract.Assert(tmp != null);
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'i'",PrimaryILOffset=1,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=14,MethodILOffset=0)]
	[RegressionOutcome(@"The caller expects the postcondition Contract.Ensures(Contract.Result<System.Int32>() >= 0); to hold for the interface member get_Count. Consider adding such postcondition to enforce all implementations to guarantee it")]
    public void TestGEQ_ZeroViaInterface(MyInterface i)
    {
      var value = i.Count;
      Contract.Assert(value >= 0);
    }

    // not working 
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=20,MethodILOffset=0)]
    public void TestGEQ_ZeroViaInstance()
    {
      var tmp = new ImplementMyInterface();
      var value = tmp.Count;
      Contract.Assert(value >= 0);
    }

    // not working 
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=18,MethodILOffset=0)]
    public void TestGEQ_ZeroViaInstance_NoName()
    {
      var tmp = new ImplementMyInterface();
      Contract.Assert(tmp.Count>= 0);
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'x'",PrimaryILOffset=14,MethodILOffset=0)]
	[RegressionOutcome(@"The caller expects the postcondition Contract.Ensures(Contract.Result<System.Int32[]>() != null); to hold for the abstract member AbstractMethod. Consider adding the postcondition to enforce all overrides to guarantee it")]
    public int TestViaInstance()
    {
      var myInstance = new ImplementAbstractClass();
      var x = myInstance.AbstractMethod();

      return x.Length;
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'x'",PrimaryILOffset=20,MethodILOffset=0)]
	[RegressionOutcome(@"The caller expects the postcondition Contract.Ensures(Contract.Result<System.Int32[]>() != null); to hold for the abstract member AbstractMethod. Consider adding the postcondition to enforce all overrides to guarantee it")]
    public int TestViaAbstractClass(AbstractClass abs)
    {
      Contract.Requires(abs != null);

      var x = abs.AbstractMethod();

      return x.Length;
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=47,MethodILOffset=0)]
	[RegressionOutcome("The caller expects the postcondition Contract.Ensures(Contract.Result<System.String>() != null); to hold for the interface member InterfaceMethod. Consider adding the postcondition to enforce all implementations to guarantee it")]
    public int TestNegative(ImplementMyInterface i1, ImplementMyInterface i2)
    {
      Contract.Requires(i1 != null);
      Contract.Requires(i2 != null);

      var tmp = i2.InterfaceMethod();
      if(i1.Count > 0)
      {
        Contract.Assert(tmp != null);
      }
      return 0;
    }
	}  
}

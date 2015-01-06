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

namespace ConsoleApplication8
{
  internal class ObjectInvariantsInferenceTest
  {
    private string myPrivateField;
    public int myNonPrivateField;
    private object _nonReadOnly;

	[ClousotRegressionTest]
	[RegressionOutcome("Field myNonPrivateField, declared in type ObjectInvariantsInferenceTest, is only updated in constructors. Consider marking it as readonly")]
	[RegressionOutcome("Field myPrivateField, declared in type ObjectInvariantsInferenceTest, is only updated in constructors. Consider marking it as readonly")]
	[RegressionOutcome("Contract.Ensures(this.myPrivateField != null);")]
	[RegressionOutcome("Contract.Ensures(this.myNonPrivateField == 12);")]
	[RegressionOutcome("Contract.Ensures(this._nonReadOnly == null);")]
	[RegressionOutcome("Consider adding an object invariant Contract.Invariant(myPrivateField != null); to the type ObjectInvariantsInferenceTest")]
    public ObjectInvariantsInferenceTest()
    {
      this.myNonPrivateField = 12;
      this.myPrivateField = "hello";
	  
    }

	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(z == this._nonReadOnly);")]
    public void M1(object z)
    {
      this._nonReadOnly = z;
    }

	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() == this.myNonPrivateField + 2);")]
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() - this.myNonPrivateField == 2);")]
	[RegressionOutcome("Contract.Ensures((this.myNonPrivateField - Contract.Result<System.Int32>()) < 0);")]
	[RegressionOutcome("Contract.Ensures(-2147483646 <= Contract.Result<System.Int32>());")]
    public int M2()
    {
      return myNonPrivateField + 2;
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'this.myPrivateField'",PrimaryILOffset=6,MethodILOffset=0)]
	[RegressionOutcome("Contract.Assume(this.myPrivateField != null);")]
	[RegressionOutcome("Contract.Ensures(this.myPrivateField != null);")]
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() == this.myPrivateField.Length);")]
	[RegressionOutcome("Contract.Ensures(0 <= Contract.Result<System.Int32>());")]
    public int M3()
    {
      return myPrivateField.Length;
    }
  }

  internal class ObjectInvariantsInferenceWithReadonly
  {
    readonly private string myPrivateField;
    public int myNonPrivateField;
    private object _nonReadOnly;

	[ClousotRegressionTest]
	[RegressionOutcome("Field myNonPrivateField, declared in type ObjectInvariantsInferenceWithReadonly, is only updated in constructors. Consider marking it as readonly")]
	[RegressionOutcome("Contract.Ensures(this.myPrivateField != null);")]
	[RegressionOutcome("Contract.Ensures(this.myNonPrivateField == 12);")]
	[RegressionOutcome("Contract.Ensures(this._nonReadOnly == null);")]
	[RegressionOutcome("Consider adding an object invariant Contract.Invariant(myPrivateField != null); to the type ObjectInvariantsInferenceWithReadonly")]
    public ObjectInvariantsInferenceWithReadonly()
    {
      this.myNonPrivateField = 12;
      this.myPrivateField = "hello";
    }
  }

  internal class ObjectInvariantsInferenceWithReadonlyAndObjectInvariant
  {
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.myPrivateField != null);
    }
   
    readonly private string myPrivateField;
    public int myNonPrivateField;
    private object _nonReadOnly;

	[ClousotRegressionTest]
	[RegressionOutcome("Field myNonPrivateField, declared in type ObjectInvariantsInferenceWithReadonlyAndObjectInvariant, is only updated in constructors. Consider marking it as readonly")]
	[RegressionOutcome("Contract.Ensures(this.myNonPrivateField == 12);")]
	[RegressionOutcome("Contract.Ensures(this._nonReadOnly == null);")]
    public ObjectInvariantsInferenceWithReadonlyAndObjectInvariant()
    {
      this.myNonPrivateField = 12;
      this.myPrivateField = "hello";
    }
  }
  
   public class FieldExternallyVisible
   {
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.myPrivateField != null);
    }
   
    readonly private string myPrivateField;
    public int myNonPrivateField; // no suggestion on this field as it is visible outside the assembly
    private object _nonReadOnly;

	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(this.myNonPrivateField == 12);")]
	[RegressionOutcome("Contract.Ensures(this._nonReadOnly == null);")]
    public FieldExternallyVisible()
    {
      this.myNonPrivateField = 12;
      this.myPrivateField = "hello";
    }
  }
  
  public class PassByReference
  {
    private int i;
	
	// Should not suggest i to be marked readonly
	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(this.i == 10);")]
	public PassByReference()
    {
      this.i = 10;
    }

	[ClousotRegressionTest]
    public void ChangeI()
    {
      Change(ref this.i);
    }

	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(x - Contract.OldValue(x) == 1);")]
    private void Change(ref int x)
    {
      x++;
    }
  }
}
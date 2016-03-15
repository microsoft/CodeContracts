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


namespace VSTTE2012
{
  public class Demo
  {
    public class PositiveBag
    {
      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.elements != null);
        Contract.Invariant(nextFree >= 0);
        Contract.Invariant(nextFree <= this.elements.Length);
        Contract.Invariant(Contract.ForAll(0, nextFree, i=> this.elements[i] > 0));
      }
            
      private int[] elements;
      private int nextFree;

      // Should infer the precondition
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The length of the array may be negative",PrimaryILOffset=8,MethodILOffset=0)]
      [RegressionOutcome("Contract.Requires(0 <= size);")]
      [RegressionOutcome("Contract.Ensures(this.nextFree == 0);")]
      [RegressionOutcome("Contract.Ensures(size == this.elements.Length);")]
      [RegressionOutcome("Contract.Ensures(Contract.ForAll(0, size, __k__ => this.elements[__k__] == 0));")]
      public PositiveBag(int size)
      {
        elements = new int[size];
        nextFree = 0;
        // invariant holds, has nextFree == 0
      }

      public int this[int index]
      {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be below the lower bound",PrimaryILOffset=7,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=7,MethodILOffset=0)]
        [RegressionOutcome("Contract.Requires(0 <= index);")]
        [RegressionOutcome("Contract.Ensures(this.elements != null);")]
        [RegressionOutcome("Contract.Ensures((index - this.elements.Length) < 0);")]
        [RegressionOutcome("Contract.Ensures(1 <= this.elements.Length);")]      
        [RegressionOutcome("Contract.Ensures(this.elements[index] == Contract.Result<System.Int32>());")]
        get
        {
            return this.elements[index];
        }
      }

      // Should suggest code fix and precondition
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Are you making some assumption on System.Array.get_Length that the static checker is unaware of? ",PrimaryILOffset=70,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.Top,Message="invariant unproven: Contract.ForAll(0, nextFree, i=> this.elements[i] > 0)",PrimaryILOffset=82,MethodILOffset=85)]
      [RegressionOutcome("Contract.Requires(x > 0);")]
      [RegressionOutcome("Contract.Ensures(this.elements != null);")]
      [RegressionOutcome("Contract.Ensures((this.nextFree - this.elements.Length) <= 0);")]
      [RegressionOutcome("Contract.Ensures(1 <= this.elements.Length);")]
      [RegressionOutcome("Contract.Ensures(1 <= this.nextFree);")]
      [RegressionOutcome("Contract.Ensures(Contract.Exists(0, this.elements.Length, __j__ => this.elements[__j__] == x));")]
      [RegressionOutcome("Consider initializing the array with a value larger than this.elements.Length * 2. Fix: this.elements.Length * 2 + 1")]
      #if CLOUSOT2
      [RegressionOutcome("This condition should hold: 0 < (int)(this.elements.Length). Add an assume, a postcondition to method System.Array.Length.get(), or consider a different initialization. Fix: Add (after) Contract.Assume(0 < (int)(this.elements.Length));")]
#else
      [RegressionOutcome("This condition should hold: 0 < (int)(this.elements.Length). Add an assume, a postcondition to method System.Array.get_Length, or consider a different initialization. Fix: Add (after) Contract.Assume(0 < (int)(this.elements.Length));")]
#endif
      public void Add(int x)
      {
        if (nextFree == elements.Length)
        {
            var tmp = new int[nextFree * 2];
            Array.Copy(elements, tmp, elements.Length);
            elements = tmp;
        }
        elements[nextFree] = x;
        nextFree++;
      }

      public int Max
      {
	[ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome("Contract.Ensures(this.elements != null);")]
    [RegressionOutcome("Contract.Ensures(1 <= this.elements.Length);")]
    [RegressionOutcome("Contract.Ensures(Contract.Exists(0, this.elements.Length, __j__ => this.elements[__j__] == Contract.Result<System.Int32>()));")]
	get 
	  {	    
	    var max = elements[0]; 
	    for (var i = 0; i < nextFree; i++)
	      { 
            if (max < elements[i])
            max = elements[i]; 
	      }

	    return max;
	  }
      }

      public int Count
      {      
        [ClousotRegressionTest]
        [RegressionOutcome("Contract.Ensures(this.elements != null);")]
        [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() == this.nextFree);")]
        get
        {
            return this.nextFree;
        }
      }
    }
  }
}

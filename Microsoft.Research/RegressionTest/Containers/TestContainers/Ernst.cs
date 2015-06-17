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
using System.Linq.Expressions;
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace VeriExamples
{
  [ContractVerification(true)]
  class ArContractTest
  {
    int topOfStack;
    Object[] theArray;
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=5,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=96,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=103,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=109,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=120,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=122,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=133,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=135,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=142,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=12,MethodILOffset=143)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=47,MethodILOffset=143)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=55,MethodILOffset=143)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=60,MethodILOffset=143)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=41,MethodILOffset=143)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=84,MethodILOffset=143)]
    public ArContractTest()
    {
      //OK
      Contract.Ensures(Contract.ForAll(0, topOfStack + 1, i => theArray[i] != null));

      //This was a bug
      Contract.Ensures(Contract.ForAll(topOfStack + 1, theArray.Length, i => theArray[i] == null));

      theArray = new Object[3];
      topOfStack = 1;

      theArray[0] = new Object();
      theArray[1] = new Object();
      theArray[2] = null;
    }
  }

  [ContractVerification(true)]
  class StackArMinimal
  {
    private Object[] theArray;
    private int topOfStack;

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(theArray != null);
      Contract.Invariant(topOfStack >= -1);
      Contract.Invariant(topOfStack < theArray.Length);

      //TWS: This is the invariant the can't be proven for Push(Object)
      Contract.Invariant(Contract.ForAll(0, topOfStack + 1, i => theArray[i] != null));
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=52,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=59,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=19,MethodILOffset=64)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=30,MethodILOffset=64)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=35,MethodILOffset=64)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=12,MethodILOffset=64)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=29,MethodILOffset=64)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=50,MethodILOffset=64)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=81,MethodILOffset=64)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=24,MethodILOffset=64)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=40,MethodILOffset=64)]
    public StackArMinimal(int capacity)
    {
      Contract.Requires(capacity >= 0);
      Contract.Ensures(Empty);
      Contract.Ensures(theArray.Length == capacity);

      theArray = new Object[capacity];
      topOfStack = -1;
    }

    /// <summary>
    /// true if empty, false otherwise
    /// </summary>
    public bool Empty
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == (topOfStack == -1));
        return topOfStack == -1;
      }
    }

    /// <summary>
    /// true if full, false otherwise.
    /// </summary>
    public bool Full
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == (topOfStack == theArray.Length - 1));
        return topOfStack == theArray.Length - 1;
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=157,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=176,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=183,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=192,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=199,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=13,MethodILOffset=200)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=27,MethodILOffset=200)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=53,MethodILOffset=200)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=59,MethodILOffset=200)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=64,MethodILOffset=200)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=12,MethodILOffset=200)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=29,MethodILOffset=200)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=50,MethodILOffset=200)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=81,MethodILOffset=200)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=21,MethodILOffset=200)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=47,MethodILOffset=200)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=68,MethodILOffset=200)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: Contract.ForAll(0, Contract.OldValue<int>(topOfStack) + 1, i => theArray[i] == Contract.OldValue<Object[]>(theArray)[i])",PrimaryILOffset=115,MethodILOffset=200)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=151,MethodILOffset=200)]
    public void Push(Object x)
    {
      Contract.Requires(x != null);

      Contract.Ensures(!Empty);
      Contract.Ensures(topOfStack == Contract.OldValue<int>(topOfStack) + 1);
      Contract.Ensures(theArray[topOfStack] == x);
      Contract.EnsuresOnThrow<Exception>(Full);

      //TWS: This can't be proven, but is what should be needed to prove the invariant
      // F: TODO!!!
      Contract.Ensures(Contract.ForAll(0, Contract.OldValue<int>(topOfStack) + 1, i => theArray[i] == Contract.OldValue<Object[]>(theArray)[i]));

      Contract.Ensures(Contract.ForAll(0, Contract.OldValue<int>(topOfStack) + 1, i => theArray[i] != null));

      if (Full)
      {
        throw new Exception("Overflow");
      }
      theArray[++topOfStack] = x;
    }
  }

  public class DisjSetsMinimal
  {
    private int[] s;

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(s != null);
      Contract.Invariant(Contract.ForAll(0, s.Length, i => -1 <= s[i]));
      Contract.Invariant(Contract.ForAll(0, s.Length, i => s[i] < s.Length));
      Contract.Invariant(Contract.ForAll(0, s.Length, i => s[i] != i));
      Contract.Invariant(s.Max() < Count); 
    }

    public int Count
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() == s.Length);
        return s.Length;
      }
    }

    /// <summary>
    /// Construct the disjoint sets object.
    /// </summary>
    /// <param name="numElements">the initial number of disjoint sets</param>
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=39,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=63,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=68,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=49,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=56,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=16,MethodILOffset=72)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=12,MethodILOffset=72)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=43,MethodILOffset=72)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=74,MethodILOffset=72)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=105,MethodILOffset=72)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=129,MethodILOffset=72)] // Can prove it thanks to the built-in support for Linq.Max
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=27,MethodILOffset=72)]
    public DisjSetsMinimal(int numElements)
    {
      Contract.Requires(numElements > 0);
      Contract.Ensures(s != null);
      s = new int[numElements];

      for (int i = 0; i < s.Length; i++)
      {
        s[i] = -1;
      }

    }
  }
}

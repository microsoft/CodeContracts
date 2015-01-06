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
using Microsoft.Research.ClousotRegression;

namespace ObjectInvariants
{
  public class SimpleInvariants
  {
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 9, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"invariant is false: this.items != null", PrimaryILOffset = 13, MethodILOffset = 23)]
    public SimpleInvariants() { }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 9, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 24, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 31, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"invariant unproven: this.items != null", PrimaryILOffset = 13, MethodILOffset = 37)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"invariant unproven: this.comparison != null", PrimaryILOffset = 31, MethodILOffset = 37)]
    public SimpleInvariants(int[] a, IComparable b)
    {
      this.items = a;
      this.comparison = b;
    }

    int[] items = null;
    IComparable comparison = null;

    [ClousotRegressionTest("regular")]
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.items != null);
      Contract.Invariant(this.comparison != null);
    }

  }

  public class CDictionary<TKey>
  {
    private IEqualityComparer<TKey> comparer;
    private int count;
    private int freeCount;

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(count >= freeCount);
      Contract.Invariant(comparer != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 15, MethodILOffset = 3)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 28, MethodILOffset = 3)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 18, MethodILOffset = 24)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 36, MethodILOffset = 24)]
    public CDictionary(IEqualityComparer<TKey> comparer)
      : this(0, comparer)
    {
      Contract.Requires(comparer != null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 65, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 73, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 35, MethodILOffset = 79)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 18, MethodILOffset = 79)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 36, MethodILOffset = 79)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 46, MethodILOffset = 79)]
    public CDictionary(int capacity, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(capacity >= 0);
      Contract.Requires(comparer != null);
      Contract.Ensures(this.comparer != null);

      if (capacity > 0)
        Initialize(capacity);
      this.comparer = comparer;
    }

    private void Initialize(int capacity)
    {
      this.freeCount = 0;
      this.count = 0;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 10, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 19, MethodILOffset = 27)]
    ~CDictionary()
    {
      this.comparer = null;
      this.count = -1;
    }

    /// <summary>
    /// Make sure we don't enforce the invariant
    /// </summary>
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 10, MethodILOffset = 0)]
    public virtual void Dispose()
    {
      this.comparer = null;
      this.count = -1;
    }
  }

  class OutgoingThis
  {
    object obj = null;
    [ContractInvariantMethod]
    private void Invariant()
    {
      Contract.Invariant(obj != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 13, MethodILOffset = 8)]
    public void S()
    {
      TestOutgoingThis.X(this);
    }
  }

  static class TestOutgoingThis
  {
    public static void X(OutgoingThis obj)
    {
    }
  }

  class TestBaseWithInv
  {
    int x = 1;

    [ContractInvariantMethod]
    private void Invariant()
    {
      Contract.Invariant(x > 0);
    }

    public virtual void M() { }
  }

  class TestDerived : TestBaseWithInv
  {
    object obj = "";

    [ContractInvariantMethod]
    private void Invariant2()
    {
      Contract.Invariant(obj != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 10, MethodILOffset = 8)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 8)]
    public override void M()
    {
      base.M();
    }
  }

  class TestBaseWithoutInv
  {
    public virtual void M() { }
  }

  class TestDerived2 : TestBaseWithoutInv
  {
    object obj = "";

    [ContractInvariantMethod]
    private void Invariant2()
    {
      Contract.Invariant(obj != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 8)]
    public override void M()
    {
      base.M();
    }
  }
}

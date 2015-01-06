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
using System.Diagnostics.Contracts;


namespace Microsoft.Research.CodeAnalysis
{
  public class SwallowedBuckets
  {
    #region Object invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.counter != null);
    }
    
    #endregion

    readonly private int[] counter;

    public SwallowedBuckets()
    {
      this.counter = new int[4];
    }

    public delegate int CounterGetter(ProofOutcome outcome);

    public SwallowedBuckets(CounterGetter counterGetter)
      : this()
    {
      Contract.Requires(counterGetter != null);

      for (int i = 0; i < this.counter.Length; i++)
      {
        this.counter[i] = counterGetter((ProofOutcome)i);
      }
    }

    public void UpdateCounter(ProofOutcome outcome)
    {
      Contract.Assert(0 <= outcome);
      Contract.Assume((int)outcome < this.counter.Length);

      this.counter[(int)outcome]++;
    }

    [Pure]
    public int GetCounter(ProofOutcome outcome)
    {
      Contract.Assert(0 <= outcome);
      Contract.Assume((int)outcome < this.counter.Length);
      return this.counter[(int)outcome];
    }

    [Pure]
    public static SwallowedBuckets operator -(SwallowedBuckets sw1, SwallowedBuckets sw2)
    {
      Contract.Requires(sw1 != null);
      Contract.Requires(sw2 != null);
      Contract.Ensures(Contract.Result<SwallowedBuckets>() != null);

      return new SwallowedBuckets(outcome => sw1.GetCounter(outcome) - sw2.GetCounter(outcome));
    }
  }
}
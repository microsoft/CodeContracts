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
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;

namespace WebExamples
{
  public class NonNegativeBag
  {
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.elements != null);
      Contract.Invariant(nextFree >= 0);
      Contract.Invariant(nextFree <= this.elements.Length);
      Contract.Invariant(Contract.ForAll(0, this.nextFree, i => this.elements[i] >= 0));
    }

    private int[] elements;
    private int nextFree;

    [ClousotRegressionTest]
    public NonNegativeBag(int size)
    {
      Contract.Requires(size >= 0);

      this.elements = new int[size];
      this.nextFree = 0;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible overflow (caused by a negative array size) in the arithmetic operation. The static checker determined that the condition '0 <= (this.elements.Length * 2 + 1)' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add an explicit assumption at entry to document it: Contract.Assume(0 <= (this.elements.Length * 2 + 1));",PrimaryILOffset=38,MethodILOffset=0)]
    public void Add(int x)
    {
      Contract.Requires(x >= 0);

      if (this.nextFree == this.elements.Length)
      {
        var tmp = new int[this.nextFree * 2+1];
        Array.Copy(this.elements, tmp, this.nextFree);
        this.elements = tmp;
      }

      this.elements[nextFree++] = x;
    }

    [ClousotRegressionTest]
    public int this[int index]
    {
      get
      {
        Contract.Requires(index >= 0);
        Contract.Requires(index < this.Count);

        return this.elements[index];
      }
    }

    [ClousotRegressionTest]
    public void RemoveAt(int index)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index < this.Count);

      this.elements[index] = this.elements[nextFree-1];
      nextFree--;
    }

    [ClousotRegressionTest]
    public bool TryContains(int value, out int index)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out index) >= 0);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out index) < this.Count);

      Contract.Ensures(!Contract.Result<bool>() || this.elements[Contract.ValueAtReturn(out index)] == value);

      for (index = 0; index < this.nextFree; index++)
      {
        if (this.elements[index] == value)
        {
          return true;
        }
      }

      return false;
    }

    public int MaxValueWrong
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: Contract.Exists(0, this.nextFree, i => this.elements[i] == Contract.Result<int>())",PrimaryILOffset=53,MethodILOffset=98)]
      get
      {
        Contract.Ensures(Contract.ForAll(0, this.nextFree, i => this.elements[i] <= Contract.Result<int>()));
        Contract.Ensures(Contract.Exists(0, this.nextFree, i => this.elements[i] == Contract.Result<int>()));  // not necessarly true as the array may be empty

        var max = 0;

        for (var i = 0; i < this.nextFree; i++)
        {
          if (this.elements[i] > max)
            max = this.elements[i];
        }

        return max;
      }
    }

    public int MaxValue
    {
    [ClousotRegressionTest]
      get
      {
        Contract.Requires(this.Count > 0);
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.ForAll(0, this.nextFree, i => this.elements[i] <= Contract.Result<int>()));
        Contract.Ensures(Contract.Exists(0, this.nextFree, i => this.elements[i] == Contract.Result<int>()));

        var max = this.elements[0];

        for (var i = 1; i < this.nextFree; i++)
        {
          if (this.elements[i] > max)
          {
            max = this.elements[i];
          }
        }

        return max;
      }
    }

    public int Sum
    {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible overflow in the arithmetic operation",PrimaryILOffset=31,MethodILOffset=0)]
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        var sum = 0;
        for (var i = 0; i < this.nextFree; i++)
        {
          checked
          {
            sum += this.elements[i];
          }
        }

        return sum;
      }
    }

    public int Count
    {
      // Here we should infer it
    [ClousotRegressionTest]
      get
      {
        return this.nextFree;
      }
    }
  }

}
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
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;

namespace SimpleSuggestions
{
  public class Simple
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'o'",PrimaryILOffset=11,MethodILOffset=0)]
    [RegressionOutcome("[SuppressMessage(\"Microsoft.Contracts\", \"Nonnull-11-0\")]")]
    [RegressionOutcome("Contract.Requires(o != null);")]
    public int Precondition(bool b, object o)
    {
      int x;
      if (b)
      {
        x = 1;
      }
      else
      {
        x = -1;
      }

      x += o.GetHashCode();


      return x;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'o'",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome("[SuppressMessage(\"Microsoft.Contracts\", \"Nonnull-1-0\")]")]
    [RegressionOutcome("Contract.Requires(o != null);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'len'",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome("[SuppressMessage(\"Microsoft.Contracts\", \"Nonnull-9-0\")]")]
    [RegressionOutcome("Contract.Requires(len != null);")]
    public int Precondition2(object o, string[] len)
    {

      int x = o.GetHashCode();
      x += len.Length;

      return x;
    }

    private int x;
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome("[SuppressMessage(\"Microsoft.Contracts\", \"Assert-10-0\")]")]
    [RegressionOutcome("Contract.Assume(this.x > 10);")]
    public int Assume(bool b, object o)
    {
      Contract.Assert(x > 10);

      return x++;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The length of the array may be negative",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome("[SuppressMessage(\"Microsoft.Contracts\", \"ArrayCreation-1-0\")]")]
    [RegressionOutcome("Contract.Requires(0 < i0);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Maybe the guard i <= i0 is too weak? ",PrimaryILOffset=15,MethodILOffset=0)]
    [RegressionOutcome("[SuppressMessage(\"Microsoft.Contracts\", \"ArrayUpperBound-15-0\")]")]
    [RegressionOutcome("Consider replacing i <= i0. Fix: i < i0")]
    [RegressionOutcome("Consider initializing the array with a value larger than 0. Fix: 1")]
    public void For(int i0)
    {
      var len = new int[i0];

      for (var i = 0; i <= len.Length; i++)
      {
        len[i] = 111;
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=63,MethodILOffset=0)]
    [RegressionOutcome("[SuppressMessage(\"Microsoft.Contracts\", \"ArrayUpperBound-63-0\")]")]
    [RegressionOutcome("Contract.Requires((list.Length != count || 0 < list.Length));")]
    [RegressionOutcome("Consider initializing the array with a value larger than list.Length * 2. Fix: list.Length * 2 + 1")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'list'",PrimaryILOffset=16,MethodILOffset=0)]
    [RegressionOutcome("[SuppressMessage(\"Microsoft.Contracts\", \"Nonnull-16-0\")]")]
    [RegressionOutcome("Contract.Requires(list != null);")]
    public object[] DoubleTheSize(object[] list, ref int count)
    {
      Contract.Requires(0 <= count);
      Contract.Requires(count <= list.Length);

      if(list.Length == count)
      {
        list = new object[count * 2];
      }
      list[count++] = count;

      return list;
    }

  }
}

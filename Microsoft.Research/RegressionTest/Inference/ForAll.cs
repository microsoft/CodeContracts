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

namespace PostInference
{
  public class PostconditionInference
{
    [ClousotRegressionTest]
  // [RegressionOutcome("Contract.Ensures(a.Length >= 0);")] // this one should not inferred, as it is a trivial one
    [RegressionOutcome("Contract.Ensures(Contract.ForAll(0, a.Length, __k__ => a[__k__] != 0));")]
    [RegressionOutcome("Contract.Ensures(Contract.ForAll(0, a.Length, __k__ => a[__k__] == value));")]

    public void SetAllToNonZero(int[] a, int value)
    {
      Contract.Requires(a != null);
      Contract.Requires(value != 0);

      for (var i = 0; i < a.Length; i++)
      {
        a[i] = value;
      }
    }

   [ClousotRegressionTest]
   [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32[]>() != null);")]
   [RegressionOutcome("Contract.Ensures(Contract.ForAll(0, Contract.Result<System.Int32[]>().Length, __k__ => Contract.Result<System.Int32[]>()[__k__] != 0));")]
   [RegressionOutcome("Contract.Ensures(Contract.ForAll(0, Contract.Result<System.Int32[]>().Length, __k__ => Contract.Result<System.Int32[]>()[__k__] == value));")]
    public int[] SetAllToNonZero(int len, int value)
    {
      Contract.Requires(len >= 0);
      Contract.Requires(value != 0);

      var a = new int[len];

      for (var i = 0; i < a.Length; i++)
      {
        a[i] = value;
      }

      return a;
    }
    
        extern int Foo();

   [ClousotRegressionTest]
   [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32[]>() != null);")]
   [RegressionOutcome("Contract.Ensures(0 <= Contract.Result<System.Int32[]>().Length);")]
   [RegressionOutcome("Contract.Ensures(Contract.ForAll(0, Contract.Result<System.Int32[]>().Length, __k__ => Contract.Result<System.Int32[]>()[__k__] != 0));")]
   [RegressionOutcome("Contract.Ensures(Contract.ForAll(0, Contract.Result<System.Int32[]>().Length, __k__ => Contract.Result<System.Int32[]>()[__k__] == value));")]
   public int[] SetAllToNonZero(int value)
    {
      Contract.Requires(value != 0);

      var len = Math.Max(0, Foo());

      var a = new int[len];

      for (var i = 0; i < a.Length; i++)
      {
        a[i] = value;
      }

      return a;
    }
} 
}

  namespace Forum
  {
    public class TomonoriMuto
    {
      public static ICollection<Byte> Value
      {
        // we were crashing here
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=55,MethodILOffset=0)]
        [RegressionOutcome("Contract.Ensures(Contract.Result<System.Collections.Generic.ICollection<System.Byte>>().Count == 0);")]
        [RegressionOutcome("Contract.Ensures((((System.Array)Contract.Result<System.Collections.Generic.ICollection<System.Byte>>()).Length - Contract.Result<System.Collections.Generic.ICollection<System.Byte>>().Count) <= 0);")]
        [RegressionOutcome("Contract.Ensures((Contract.Result<System.Collections.Generic.ICollection<System.Byte>>().Count - ((System.Array)Contract.Result<System.Collections.Generic.ICollection<System.Byte>>()).Length) <= 0);")]
        get
        {
          Contract.Ensures(Contract.Result<ICollection<Byte>>() != null);
          Contract.Ensures(Contract.Result<ICollection<Byte>>().Count == 0);
          var r = (ICollection<Byte>)new Byte[0];
          Contract.Assert(r.Count == 0);
          return r;
        }
      }
    }
  }

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
using Microsoft.Research.ClousotRegression;

class Program : Base
{
  [ClousotRegressionTest]
  [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: x", PrimaryILOffset = 8, MethodILOffset = 14)]
  static void Main(string[] args)
  {
    Program p = new Program();


    // this line should generate a contract violation warning
    // since InnerFoo requires true. 
    p.myInner.InnerFoo(false);

  }


  [ClousotRegressionTest]
  [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: x", PrimaryILOffset = 8, MethodILOffset = 9)]
  static void Main2(string[] args)
  {
    Program p = new Program();

    p.BaseFoo(false);
  }

  [ClousotRegressionTest]
  [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: x (inner gotta have x)", PrimaryILOffset = 12, MethodILOffset = 14)]
  static void Main3(string[] args)
  {
    Program p = new Program();


    // this line should generate a contract violation warning
    // since InnerFoo requires true. 
    p.myInner.InnerFoo2(false);

  }

  [ClousotRegressionTest]
  [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: x (base gotta have x)", PrimaryILOffset = 12, MethodILOffset = 9)]
  static void Main4(string[] args)
  {
    Program p = new Program();

    p.BaseFoo2(false);
  }

  [ClousotRegressionTest]
  [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "invariant unproven: data > 0", PrimaryILOffset = 16, MethodILOffset = 8)]
  [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "invariant unproven: data < 10 (upper bound)", PrimaryILOffset = 42, MethodILOffset = 8)]
  public void UpdateData(int value)
  {
    this.data = value;
  }

  [ClousotRegressionTest]
  [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"ensures is false: Contract.Result<int>() > 0", PrimaryILOffset = 15, MethodILOffset = 6)]
  public override int Bar()
  {
    return 0;
  }

  [ClousotRegressionTest]
  [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"ensures is false: Contract.Result<int>() > 0 (result positive)", PrimaryILOffset = 19, MethodILOffset = 6)]
  public override int Bar2()
  {
    return 0;
  }


  public class Inner2 : Inner
  {
    /// <summary>
    /// Looks correct
    /// </summary>
    /// <returns></returns>
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = "ensures is false: Contract.Result<int>() > 0", PrimaryILOffset = 15, MethodILOffset = 6)]
    public override int InnerBar()
    {
      return 0;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = "ensures is false: Contract.Result<int>() > 0 (result positive)", PrimaryILOffset = 19, MethodILOffset = 6)]
    public override int InnerBar2()
    {
      return 0;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "invariant unproven: InnerData > 0", PrimaryILOffset = 16, MethodILOffset = 8)]
    public void BadInnerInv(int x)
    {
      this.InnerData = x;
    }

  }
}



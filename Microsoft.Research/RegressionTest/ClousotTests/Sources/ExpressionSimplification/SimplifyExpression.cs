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


using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;
using System;

namespace ExpressionSimplification
{
  public class SimplifyExpression
  {
    // #1 ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 8, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 21, MethodILOffset = 0)]
    public void Simplify0(int x, int y)
    {
      Contract.Assert(y == y + 1 - 1);
      Contract.Assert(2 * x == x + x);
    }

    // #2 ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 10, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 19, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 28, MethodILOffset = 0)]
    public void Simplify1()
    {
      int i, j, h;

      i = 5 + 3 - 5;          // i == 3
      j = (5 * 3) - 15;       // j == 0
      h = 3 / 3 + 6;          // h == 7;

      Contract.Assert(i == 3);
      Contract.Assert(j == 0);
      Contract.Assert(h == 7);
    }

    // #3 Ok 
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 10, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 26, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 41, MethodILOffset = 0)]
    public void Simplify2(int t)
    {
      int k, r, s;

      k = 1 + t - 1;        // t == k
      Contract.Assert(t == k);
      
      r = 1 - t - 1;        // r == -t
      Contract.Assert(r == -t);

      s = 2 - t;            // s + t == 2 
      Contract.Assert(s + t == 2);

    }

    //#4 ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 14, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 29, MethodILOffset = 0)]
    public void Simplify3(int y, int z)
    {
      int x;

      x = y * (1 + z) - y * z;        // x = y
      Contract.Assert(x == y);

      x = x + 1;                      // x = y + 1 
      Contract.Assert(x == y + 1);
    }
  }
}

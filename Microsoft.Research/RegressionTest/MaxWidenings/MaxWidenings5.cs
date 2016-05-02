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

namespace WideningDepth
{
  public class WideningDepth
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=33,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="assert is false",PrimaryILOffset=42,MethodILOffset=0)]
    public void Test0(int x)
    {
      Contract.Requires(0 <= x);

      int i = 0;
      while (i < x)
      {
        i = i + 2;
      }
      Contract.Assert(i <= x + 1);  // spurious error
      Contract.Assert(0 < i);       // genuine error
    }


    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="assert is false",PrimaryILOffset=37,MethodILOffset=0)]
    public void Test1(int x)
    {
      Contract.Requires(0 <= x);

      int i = 0;
      while (i < x)
      {
        i = i + 1;
      }
      Contract.Assert(i == x);  // spurious error
      Contract.Assert(0 < i);   // genuine error
    }

    // Gulwani, S., Jojic, N.: Program verification as probabilistic inference. In: POPL. (2007) 277–289
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="assert is false",PrimaryILOffset=32,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="assert unreachable",PrimaryILOffset=47,MethodILOffset=0)]
    public void Test2()
    {
      int x = 0;
      int y = 50;
      while (x < 100) {
        x = x + 1;
        if (x > 50) {
          y = y + 1;
        }
        if (NondetBool()) {
          Contract.Assert(50 < y);  // genuine error
        }
      }
      Contract.Assert(y == 100);  // spurious error (not reported without widening)
    }

    public static bool NondetBool()
    {
      return new Random().Next(2) == 0;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="Division by zero ok",PrimaryILOffset=25,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No overflow",PrimaryILOffset=25,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=45,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="assert is false",PrimaryILOffset=54,MethodILOffset=0)]
    public void Test3(int x)
    {
      Contract.Requires(0 <= x);

      int s = 0;
      int m = 0;
      while (s < x) {
        s = s + 1;
        int t = s % 60;
        if (t == 0) {
          m = m + 1;
        }
      }
      Contract.Assert(m <= s);  // spurious error (not reported without widening)
      Contract.Assert(0 < s);  // genuine error
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=27,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="assert is false",PrimaryILOffset=36,MethodILOffset=0)]
    public void Test4()
    {
      int c = 0;
      while (NondetBool()) {
        if (c < 7) { c++; }
      }

      Contract.Assert(c < 585);  // spurious error
      Contract.Assert(0 < c);  // genuine error
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=27,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="assert is false",PrimaryILOffset=36,MethodILOffset=0)]
    public void Test5()
    {
      int c = 0;
      while (NondetBool()) {
        if (c < 7) { c++; }
      }

      Contract.Assert(c < 585);  // spurious error
      Contract.Assert(7 < c);  // genuine error
    }
  }
}

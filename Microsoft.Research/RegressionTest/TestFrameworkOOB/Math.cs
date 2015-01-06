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

namespace TestMath
{
  class TestMath
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 34, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 58, MethodILOffset = 0)]
    static public void TestSign(Int64 i, Int64 j)
    {
      Contract.Requires(i > 0);
      Contract.Requires(j > 0);

      int i_Sign = Math.Sign(i);

      Contract.Assert(i_Sign == 1);

      int sign = Math.Sign(i) * Math.Sign(j);

      Contract.Assert(sign == 1);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: value != -9223372036854775808L", PrimaryILOffset = 21, MethodILOffset = 2)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=16,MethodILOffset=0)]
    static public void TestAbs_NotOk(Int64 i)
    {
      var z = Math.Abs(i);

      Contract.Assert(z >= 0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 21, MethodILOffset = 13)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 27, MethodILOffset = 0)]
    static public void TestAbs_Ok(Int64 i)
    {
      Contract.Requires(i > 0);
      var z = Math.Abs(i);

      Contract.Assert(z >= 0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=17,MethodILOffset=28)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=39,MethodILOffset=0)]
    static public void TestAbs2(int denominator)
    {
      Contract.Requires(denominator != 0);

      if (denominator != Int32.MinValue)
      {
        denominator = Math.Abs(denominator);
        Contract.Assert(denominator > 0);
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 32, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 56, MethodILOffset = 0)]
    static public void TestSign(Int32 i, Int32 j)
    {
      Contract.Requires(i > 0);
      Contract.Requires(j > 0);

      int i_Sign = Math.Sign(i);

      Contract.Assert(i_Sign == 1);

      int sign = Math.Sign(i) * Math.Sign(j);

      Contract.Assert(sign == 1);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 23, MethodILOffset = 0)]
    static public void TestMax(int x, int y)
    {
      Contract.Requires(x > y);

      var max = Math.Max(x, y);

      Contract.Assert(max == x);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 23, MethodILOffset = 0)]
    static public void TestMin(int x, int y)
    {
      Contract.Requires(x < y);

      var min = Math.Min(x, y);

      Contract.Assert(min == x);
    }

  }
}

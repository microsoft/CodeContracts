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


namespace JonathanTapicer
{
  public class C
  {
    public static int C1;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 8, MethodILOffset = 19)]
    public void M1()
    {
      Contract.Ensures(C1 == 1);
      C1 = 1;
    }

    public static int C2;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 20, MethodILOffset = 65)]
    public void M2(int n)
    {
      Contract.Requires(n > 0);
      Contract.Ensures(C2 <= n);

      C2 = 0;
      for (int i = 0; i < n; i++)
      {
        M1();
        C2 += C1;
      }
    }

    public class C_2
    {
      public static int C1;

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 11, MethodILOffset = 22)]
      public void M1()
      {
        Contract.Ensures(C1 <= 1);
        C1 = 1;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 16, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 44, MethodILOffset = 0)]
      public void M2(int n)
      {
        Contract.Requires(n > 0);

        var C2 = 0;
        for (int i = 0; i < n; i++)
        {
          //To infer C2 <= i we need: Subpolyhedra+Octagon inference

          M1();
          C2 += C1;
        }

        Contract.Assert(C2 <= n);
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 28, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 22, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 56, MethodILOffset = 0)]
      public void M2_WithInvariant(int n)
      {
        Contract.Requires(n > 0);

        var C2 = 0;
        for (int i = 0; i < n; i++)
        {
          Contract.Assert(C2 <= i);

          M1();
          C2 += C1;
        }

        Contract.Assert(C2 <= n);
      }
    }
  }
}
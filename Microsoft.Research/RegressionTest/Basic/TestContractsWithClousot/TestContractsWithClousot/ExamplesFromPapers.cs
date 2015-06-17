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

namespace SAS07
{  
  public class SankaranarayananaEtAl
  {
    // This examples is taken by the "Program analysis using symbolic ranges" paper. We can prove it using just Pentagons + Karr 
    // (Please note that Octagons do not do the work here, as we need the loop invariant y - x = j - i) 
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)25, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)37, MethodILOffset = 0)]
    public void foo(int i, int j)
    {
      int x = i;
      int y = j;

      if (x <= 0)
        return;

      while (x > 0)
      {
        x--;
        y--;
      }
      Contract.Assert(x == 0);

      if (y == 0)
      {
        Contract.Assert(i == j);
      }
    }
  }
}

namespace McMillanMSRTalk09
{
  class McMillan
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 34, MethodILOffset = 0)]
    static public void Foo(bool b)
    {
      int x = 0;
      int y = 0;

      while (b)
      {
        x++;
        y++;
      }

      while (x != 0)
      {
        x--;
        y--;
      }

      Contract.Assert(y == 0);
    }
  }
}

namespace PLDI07
{
  class Hentzinger
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 57, MethodILOffset = 0)]
    public void HentzingerEtAl(int n, bool nonDet)
    {
      Contract.Requires(n >= 0);

      int i, a, b;

      i = 0; a = 0; b = 0;
      while (i < n)
      {
        if (nonDet)
        {
          a = a + 1;
          b = b + 2;
        }
        else
        {
          a = a + 2;
          b = b + 1;
        }
        i = i + 1;
      }
      Contract.Assert(a + b == 3 * n);
    }
  }
}

namespace APLAS06
{
  class XavierRival
  {
    // Clousot used to give a warning: it was missing a case in the normalization of expressions in WP 
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=29,MethodILOffset=0)]
    public static void Disjunction(int x)
    {
      int y;

      if (x > 0)
        y = x;
      else
        y = -x;

      if (y > 10)
      {
        Contract.Assert(x < -10 ||  x > 10);
      }
    }
  }
}


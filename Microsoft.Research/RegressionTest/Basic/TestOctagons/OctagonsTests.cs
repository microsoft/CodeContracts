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
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;
namespace TestOctagons
{

  public class BasicTests
  {
    // #1 ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 19, MethodILOffset = 0)]
    public void M1(int x)
    {
      Contract.Requires(x >= 0);
      Contract.Assert(x >= 0);
    }

    // #2 ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Array creation : ok", PrimaryILOffset = 13, MethodILOffset = 0)]
    public bool[] M2(int x)
    {
      Contract.Requires(x >= 0);

      return new bool[x];
    }

    // #3 ok 
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 39, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 59, MethodILOffset = 0)]
    public void M3(int x, int y)
    {
      Contract.Requires(x >= 0);
      Contract.Requires(y >= 0);

      if (x + y <= 20)
      {
        Contract.Assert(x <= 20);
      }
      if (x + y <= 20)
      {
        Contract.Assert(y <= 20);
      }
    }

    // #4 ok 
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven. The static checker determined that the condition 'x <= 20' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(x <= 20);", PrimaryILOffset = 39, MethodILOffset = 0)]
    public void M4(int x, int y)
    {
      Contract.Requires(x >= 0);
      Contract.Requires(y >= 0);

      if (x - y <= 20)
      {
        Contract.Assert(x <= 20); // this is not tue, e.g. x = 105, y = 100
      }
    }

    // #5 ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 25, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 25, MethodILOffset = 0)]
    public bool M5(bool[] a, int index)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index < a.Length);

      return a[index];
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 19, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 31, MethodILOffset = 0)]
    public void M6(int a)
    {
      int r;
      if (a < 0)
        r = -2;
      else
        r = 1;

      Contract.Assert(r >= -2);
      Contract.Assert(r <= 1);
    }

    //#7 
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 19, MethodILOffset = 0)]
    public void M7(int n)
    {
      int i = 0;
      while (i < n)
      {
        i++;
      }
      Contract.Assert(i >= 0);
    }

    //#8 TODO
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 22, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 22, MethodILOffset = 0)]
    public void M8(int[] array)
    {
      int index = 0;
      int length = array.Length;

      if (index < length)
      {
        index++;
      }

      if (index == length)
        return;

      array[index] = 0;
    }

    // #9 OK
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 24, MethodILOffset = 0)]
    public void M9(bool[] b)
    {
      Contract.Requires(b.Length >= 10);
      Contract.Assert(b.Length >= 5);
    }

    //[ClousotRegressionTest]
    //public int NonLinear1(int x, int y, int z)
    //{
    //  if (x % 2 < y)
    //  {
    //    Contract.Assert(x % 2 <= y);
    //  }

    //  return 1;
    //}

    //[ClousotRegressionTest]
    //public int NonLinear2(int x, int y, int z)
    //{
    //  if (x % 2 <= y)
    //  {
    //    Contract.Assert(x % 2 <= y);
    //  }

    //  return 1;
    //}
  }

  public class MyDateTime
  {
    private ulong dateData;

    public MyDateTime(ulong u)
    {
      this.dateData = u;
    }

    internal long InternalTicks
    {
      get
      {
        Contract.Ensures(Contract.Result<long>() == (((long)this.dateData) & 0x3fffffffffffffffL));

        return (((long)this.dateData) & 0x3fffffffffffffffL);
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=19,MethodILOffset=0)]
    internal static void CheckAddResult(long ticks, MyDateTime minValue, MyDateTime maxValue)
    {
      if ((ticks < minValue.InternalTicks) || (ticks > maxValue.InternalTicks))
      {
        Contract.Assert(false); // unreached, but we report as valid, as it is "assert false"
      }
    }
  }


  public class Loops
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=8,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=8,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=16,MethodILOffset=0)]
    public static void Loop0(string[] x)
    {
      int max = 0;
      for (int i = 0; i < x.Length; i++)
      {
        var z = x[i];

        Contract.Assert(max < x.Length);

        if (z == null)
        {
          max++;
        }
      }
    }
  }
}




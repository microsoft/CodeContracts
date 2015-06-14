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

using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;
using System;

namespace TestNumericalDisequalities
{
  class Simple
  {
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)28, MethodILOffset = 0)]
    public static int Test0(int flag)
    {
      int data = 5;

      if (flag == 1)
      {
        data = 5;
      }
      else
      {
        Contract.Assert(flag != 1);
      }

      return data;
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven. The static checker determined that the condition 'flag != 3' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(flag != 3);", PrimaryILOffset = (int)28, MethodILOffset = 0)]
    public static int Test1(int flag)
    {
      int data = 5;

      if (flag == 1)
      {
        data = 5;
      }
      else
      {
        Contract.Assert(flag != 3);
      }

      return data;
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)37, MethodILOffset = 0)]
    public static int Test2(bool b)
    {
      int data = 33;

      if (b)
      {
        data = 1024;
      }
      else
      {
        data = -256;
      }

      Contract.Assert(data != 0);

      return data;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)33, MethodILOffset = 0)]
    public static long Loop0(int max)
    {
      long result = 1;

      for (int i = 1; i < max; i++)
        result++;

      Contract.Assert(result != 0);

      return result;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Division by zero ok", PrimaryILOffset = (int)20, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible overflow in division (MinValue / -1)", PrimaryILOffset = 20, MethodILOffset = 0)]
    public static int Divide(int x)
    {
      int z;
      if (x > 0)
        z = -1;
      else
        z = 1;

      return x / z;
    }
  }

  public class Rational
  {
    int n, d;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 36)]
    public Rational(int n, int d)
    {
      Contract.Requires(d != 0);
      this.n = n;
      this.d = d;
    }

    [ClousotRegressionTest]
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(d != 0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 87)]
    public void AddUp(Rational other)
    {
      Contract.Requires(other != null);
      int newN = this.n * other.d + other.n * this.d;
      int newD = this.d * other.d;
      if (newD == 0)
        throw new OverflowException();
      this.n = newN;
      this.d = newD;
    }

  }

}

namespace BugRepro
{
  public class MyIntPtr
  {
    [ClousotRegressionTest]
    public static Int32 Size
    {
      get
      {
        Contract.Ensures(Contract.Result<Int32>() == 4);
        return 4;
      }
    }
  }

  class BrianMSCorlib
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 11, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 20, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 11, MethodILOffset = 0)]
    private static int GetDefaultChunkSize<TSource>()
    {      
      // This test is here because it was a bug in the SimpleDisequalities AD
      Contract.Assert((0x200 % MyIntPtr.Size) == 0, "bytes per chunk should be a multiple of pointer size");
      return (0x200 / MyIntPtr.Size);
    }
  }
}


namespace Wurz
{
  public class Multiplication
  {

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: x != 0", PrimaryILOffset = 8, MethodILOffset = 4)]
    public static void NotZero_NOT_OK(int x)
    {
      Call(x * 3);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 8, MethodILOffset = 17)]
    public static void NotZero_OK(int x)
    {
      Contract.Requires(x != 0);
      Call(x * 3);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 8, MethodILOffset = 14)]
    public static void NotZero_Intervals(int x)
    {
      Contract.Requires(x > 0);
      Call(x * 3);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: x != 0", PrimaryILOffset = 8, MethodILOffset = 17)]
    public static void NotZero_TwoParameters_NOT_OK(int x, int y)
    {
      Contract.Requires(x != 0);

      Call(x * y);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 8, MethodILOffset = 30)]
    public static void NotZero_TwoParameters_OK(int x, int y)
    {
      Contract.Requires(x != 0);
      Contract.Requires(y != 0);

      Call(x * y);
    }

    [ClousotRegressionTest]
    private static void Call(int x)
    {
      Contract.Requires(x != 0);
    }
  }
}

namespace Sychev
{
  public class InvertSign
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 25, MethodILOffset = 40)]
    public static int InvertSign1(int x)
    {
      Contract.Requires(x != 0);
      Contract.Ensures(Contract.Result<int>() != 0);
      int y = -1 * x;
      return y;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible negation of MinValue of type Int32", PrimaryILOffset = 32, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 25, MethodILOffset = 39)]
    public static int InvertSign2(int x)
    {
      Contract.Requires(x != 0);
      Contract.Ensures(Contract.Result<int>() != 0);
      int y = -x;
      return y;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible negation of MinValue of type Int32", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Negation ok (no MinValue) of type Int32", PrimaryILOffset = 31, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 24, MethodILOffset = 38)]
    public static int InvertSign3(int x)
    {
      Contract.Requires(-x * x < 0);
      Contract.Ensures(Contract.Result<int>() * x < 0);
      int y = -x;
      return y;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible negation of MinValue of type Int32", PrimaryILOffset = 31, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: Contract.Result<int>() * x < 0",PrimaryILOffset=24,MethodILOffset=38)]
    public static int InvertSign4(int x)
    {
      Contract.Requires(x != 0);
      Contract.Ensures(Contract.Result<int>() * x < 0); // Fails when x == Int32.MinValue or when there is an overflow
      int y = -x;
      return y;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible negation of MinValue of type Int32", PrimaryILOffset = 31, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible underflow in the arithmetic operation",PrimaryILOffset=20,MethodILOffset=38)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible overflow in the arithmetic operation",PrimaryILOffset=20,MethodILOffset=38)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: checked(Contract.Result<int>() * x < 0)",PrimaryILOffset=24,MethodILOffset=38)] 
    public static int InvertSign5(int x)
    {
      Contract.Requires(x != 0);
      Contract.Ensures(checked(Contract.Result<int>() * x < 0)); // Fails when x == Int32.MinValue or when there is an overflow
   
      int y = -x;
      return y;
    }
  }
}

namespace CloudDev
{
  public class Repro
  {
    [Flags]
    public enum Attributes
    {
      None = 0x0000,
      ReadOnly = 0x0001,
      Directory = 0x0002,
      SequentialOnly = 0x0004,
      AllowVolatileChildren = 0x0008,
    }

    public bool IsReadOnly;

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=36,MethodILOffset=0)]
    public void ProcessBeforeChildrenEnumeration(bool CanCreateVolatileChildren)
    {
      var attrsForFile = IsReadOnly ? Attributes.ReadOnly : Attributes.None;
      var attrsForDir = attrsForFile;
      if (CanCreateVolatileChildren)
      {
        attrsForDir |= Attributes.AllowVolatileChildren;
      }
	  
      Contract.Assert((attrsForFile & Attributes.Directory) == 0);
    }
  }
  }

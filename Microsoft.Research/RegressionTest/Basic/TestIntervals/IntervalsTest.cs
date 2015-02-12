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

namespace TestIntervals
{
  class IntervalsBasic
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 32, MethodILOffset = 0)]
    public void M0(int x, int z)
    {
      //  Contract.Requires(x + z== 5);
      Contract.Requires(x >= 0);
      Contract.Requires(x >= 12);

      Contract.Assert(x >= 0);
    }

    // two warnings
    // We check twice the postcondition, as /optimize+ generates two "ret" instructions
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possible negation of MinValue of type Int32. The static checker determined that the condition 'x != Int32.MinValue' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(x != Int32.MinValue);", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 8, MethodILOffset = 19)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: Contract.Result<int>() > 0. The static checker determined that the condition 'x > 0' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(x > 0);. Is it an off-by-one? The static checker can prove x > (0 - 1) instead", PrimaryILOffset = 8, MethodILOffset = 21)]
    public int Abs0(int x)
    {
      Contract.Ensures(Contract.Result<int>() > 0);

      if (x < 0)
        return -x;
      else
        return x;
    }

    // Ok
    // We check twice the postcondition, as /optimize+ generates two "ret" instructions
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 11, MethodILOffset = 22)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 11, MethodILOffset = 24)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possible negation of MinValue of type Int32. The static checker determined that the condition 'x != Int32.MinValue' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(x != Int32.MinValue);", PrimaryILOffset = 21, MethodILOffset = 0)]
    public int Abs1(int x)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);

      if (x < 0)
        return -x;
      else
        return x;
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 9, MethodILOffset = 29)]
    public int Arithmetics0()
    {
      Contract.Ensures(Contract.Result<int>() == -6);
      int y, z, x, t;

      y = 1;
      z = 16;
      x = 9;

      t = y - z + x;

      return t;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=21,MethodILOffset=33)]
    public int Arithmetics1_Overflow(int x)
    {
      Contract.Requires(x == 1);
      Contract.Ensures(Contract.Result<int>() == Int32.MinValue);

      return Int32.MaxValue + x; // We know it will overflow
    }

    [ClousotRegressionTest]
//    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=32,MethodILOffset=0)]
//    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"Overflow in the arithmetic operation",PrimaryILOffset=32,MethodILOffset=0)]
//    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"ensures is false: Contract.Result<int>() == Int32.MinValue",PrimaryILOffset=21,MethodILOffset=33)]

    // F: a bug fixing in the conversion in IEEE754 intervals introduced a loss of precision, so we only say "top", instead of getting the definite false 
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible underflow in the arithmetic operation",PrimaryILOffset=32,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible overflow in the arithmetic operation",PrimaryILOffset=32,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="ensures unproven: Contract.Result<int>() == Int32.MinValue",PrimaryILOffset=21,MethodILOffset=33)]
    public int Arithmetics2_CheckedOverlow(int x)
    {
      Contract.Requires(x == 1);
      Contract.Ensures(Contract.Result<int>() == Int32.MinValue);

      return checked(Int32.MaxValue + x); 
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 23, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 29, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: Contract.Result<int>() >= 0. The static checker determined that the condition '(i / 2) >= 0' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires((i / 2) >= 0);", PrimaryILOffset = 11, MethodILOffset = 24)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: Contract.Result<int>() >= 0. The static checker determined that the condition '((i - 1) / 2) >= 0' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(((i - 1) / 2) >= 0);", PrimaryILOffset = 11, MethodILOffset = 30)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 23, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 29, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 18, MethodILOffset = 0)]
    public int Rem_Wrong(int i)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);

      if (i % 2 == 0)
        return i / 2;
      else
        return (i - 1) / 2;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 30, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 35, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 41, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 23, MethodILOffset = 36)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 23, MethodILOffset = 42)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 35, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 41, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 30, MethodILOffset = 0)]
    public int Rem_Ok(int i)
    {
      Contract.Requires(i >= 0);
      Contract.Ensures(Contract.Result<int>() >= 0);

      if (i % 2 == 0)
        return i / 2;
      else
        return (i - 1) / 2;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 33, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: Contract.Result<int>() > 0. Is it an off-by-one? The static checker can prove ((value % divisor)) > (0 - 1) instead", PrimaryILOffset = 26, MethodILOffset = 34)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 33, MethodILOffset = 0)]
    public int Rem_PosPos_Wrong(int value, int divisor)
    {
      Contract.Requires(value > 0);
      Contract.Requires(divisor > 0);

      Contract.Ensures(Contract.Result<int>() > 0);

      return value % divisor;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 29, MethodILOffset = 37)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 36, MethodILOffset = 0)]
    public int Rem_PosPos_Ok(int value, int divisor)
    {
      Contract.Requires(value > 0);
      Contract.Requires(divisor > 0);

      Contract.Ensures(Contract.Result<int>() >= 0);

      return value % divisor;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 33, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: Contract.Result<int>() < 0", PrimaryILOffset = 26, MethodILOffset = 34)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 33, MethodILOffset = 0)]
    public int Rem_NegPos_Wrong(int value, int divisor)
    {
      Contract.Requires(value < 0);
      Contract.Requires(divisor > 0);

      Contract.Ensures(Contract.Result<int>() < 0);

      return value % divisor;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 29, MethodILOffset = 37)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 36, MethodILOffset = 0)]
    public int Rem_NegPos_Ok(int value, int divisor)
    {
      Contract.Requires(value < 0);
      Contract.Requires(divisor > 0);

      Contract.Ensures(Contract.Result<int>() <= 0);

      return value % divisor;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven. Is it an off-by-one? The static checker can prove x >= (0 - 1) instead", PrimaryILOffset = 20, MethodILOffset = 0)]
    public void AddMaxValue_Wrong(int x)
    {
      if (x < 0)
      {
        x += 0x7fffffff;
      }

      Contract.Assert(x >= 0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 20, MethodILOffset = 0)]
    public void AddMaxValue_ok(int x)
    {
      if (x < 0)
      {
        x += 0x7fffffff;
      }

      Contract.Assert(x >= -1);
    }
  }

  class UseGuards
  {
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 11, MethodILOffset = 36)]
    public int Guard0(int k)
    {
      Contract.Ensures(Contract.Result<int>() >= 5);

      int z;

      if (k > 0)
        z = 2 * k + 5;
      else
        z = -2 * k + 5;

      return z;
    }

    // ok 
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 24, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 37, MethodILOffset = 0)]
    public int Guard1(bool b)
    {

      int z = 25;

      if (b)
      {
        z = z - 1;
      }
      else
      {
        z = z + 1;
      }

      Contract.Assert(z >= 24);
      Contract.Assert(z <= 26);
      return z; 
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 19, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 32, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 49, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 62, MethodILOffset = 0)]
    public int Guard2(bool b)
    {
      int x;

      if (b)
        x = 100;
      else
        x = -100;

      Contract.Assert(-100<= x);
      Contract.Assert(x <= 100); 
      
      x = x + 1;

      Contract.Assert(-99<= x);
      Contract.Assert(x <= 101); 

      return x;
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 21, MethodILOffset = 0)]
    public int Guard3()
    {
      int i = 12;
      int r;

      if (i != 56)
        r = 22;
      else
        r = 123;

      Contract.Assert(r == 22);
      return r;
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 13, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 28, MethodILOffset = 0)]
    public bool Guard4(int i)
    {
      if (i < -5)
      {
        Contract.Assert(i <= -6);
        return true;
      }
      else
      {
        Contract.Assert(i >= -5);
        return false;
      }
    }
  }

  class Loops
  {
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 21, MethodILOffset = 0)]
    public void Loop0()
    {
      int i = 0;
      while (i < 100)
        i++;
     
      Contract.Assert(i >= 100);
    }

    // One warning 
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 18, MethodILOffset = 0)]
    public void Loop1()
    {
      int i = 0;
      while (i < 100)
        i++;
      
      Contract.Assert(i == 100); // Proven using widening with thresholds
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 34, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 13, MethodILOffset = 0)]
    public void Loop2()
    {
      int i = 100;
      while (i > 0)
      {
        Contract.Assert(i <= 100);
        i--;
      }

      Contract.Assert(i <= 100);
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 20, MethodILOffset = 0)]
    public void Loop3()
    {
      int i = 100;
      while (i > 0)
        i--;

      Contract.Assert(i <= 0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 15, MethodILOffset = 0)]
    public void Loop4()
    {
      int i = 100;
      do
      {
        i--;
      } while (i > 0);

      // Prove it despite of the expression refinement in the guard
      Contract.Assert(i == 0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 17, MethodILOffset = 0)]
    public void Loop5()
    {
      var x = 10;

      while (x >= 0)
      {
        x = x - 1;
      }
    
      // Prove it thanks to the threshold -1 hardcoded in Clousot
      Contract.Assert(x == -1);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 12, MethodILOffset = 0)]
    public void Loop6()
    {
      var x = 0;
      while (x != 103)
      {
        // Fixing a bug in the Threshold inference
        Contract.Assert(x < 1087);
        x++;
      }
    }
  }

  class TestNaN
  {

    #region Methods with Preconditions involving NaN

    // f == float.Nan is always false
    [ClousotRegressionTest]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="method Requires (including invariants) are unsatisfiable: TestIntervals.TestNaN.Pre_ImpossibleToSatisfy(System.Single)",PrimaryILOffset=13,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"method Requires (including invariants) are unsatisfiable: TestIntervals.TestNaN.Pre_ImpossibleToSatisfy(System.Single)",PrimaryILOffset=14,MethodILOffset=0)]
#endif
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The arguments of the comparison have a compatible precision", PrimaryILOffset = 6, MethodILOffset = 0)]
    public int Pre_ImpossibleToSatisfy(float f)
    {
      Contract.Requires(f == float.NaN);

      // F: We no not get any warning for the assert(false). is this a bug???
      // M: unless you use -show unreachable, you won't get an outcome here.

      Contract.Assert(false);

      return 3;
    }

    [ClousotRegressionTest]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="method Requires (including inherited requires and invariants) are unsatisfiable: TestIntervals.TestNaN.Pre_VirtualImpossibleToSatisfy(System.Single)",PrimaryILOffset=13,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"method Requires (including inherited requires and invariants) are unsatisfiable: TestIntervals.TestNaN.Pre_VirtualImpossibleToSatisfy(System.Single)",PrimaryILOffset=14,MethodILOffset=0)]
#endif    
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The arguments of the comparison have a compatible precision", PrimaryILOffset = 6, MethodILOffset = 0)]
    public virtual int Pre_VirtualImpossibleToSatisfy(float f)
    {
      Contract.Requires(f == float.NaN);

      // F: We no not get any warning for the assert(false). is this a bug???
      // M: unless you use -show unreachable, you won't get an outcome here.

      Contract.Assert(false);

      return 3;
    }

    // The precondition holdfs if f.IsNaN
    [ClousotRegressionTest]
    public int Pre_CanSatisfy(float f)
    {
      Contract.Requires(f.Equals(float.NaN));

      return 5;
    }

    // This always hold
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The arguments of the comparison have a compatible precision", PrimaryILOffset = 10, MethodILOffset = 0)]
    public int Pre_AlwaysSatisfied(double d)
    {
      Contract.Requires(d != double.NaN);

      return 4;
    }

    #endregion

    #region Callers

    [ClousotRegressionTest]
	#if CLOUSOT2 && NETFRAMEWORK_4_0
		[RegressionOutcome(Outcome=ProofOutcome.False,Message="requires is false: f == float.NaN",PrimaryILOffset=8,MethodILOffset=6)]
	#else
		[RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: f == float.NaN", PrimaryILOffset = 8, MethodILOffset = 6)]
	#endif
    public void Call_Pre_ImpossibleToSatisfy()
    {
      int i = Pre_ImpossibleToSatisfy(float.NaN);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 12, MethodILOffset = 6)]
    public void Call_Pre_CanSatisfy()
    {
      int i = Pre_CanSatisfy(float.NaN);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 15, MethodILOffset = 10)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 15, MethodILOffset = 26)]
    public void Call_Pre_AlwaysSatisfied()
    {
      int i = Pre_AlwaysSatisfied(double.NaN);
      int k = Pre_AlwaysSatisfied(2.34567);
    }
    #endregion
  }

  class UInts
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven. The static checker determined that the condition 'input == 0' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(input == 0);", PrimaryILOffset = (int)22, MethodILOffset = 0)]
    static void Loop_int(int input)
    {
      int i = input;
      int count = 0;
      while (i > 0)
      {
        count++;
        i--;
      }

      Contract.Assert(input == 0);    // Should fail, as input may be negative
    }

    // Cannot prove it, as bgt_un is not recognized by Clousot at the moment. Should be fixed
    //    [ClousotRegressionTest]
    static void Loop_uint(uint input)
    {
      uint i = input;
      uint count = 0;
      while (i > 0)
      {
        count++;
        i--;
      }

      Contract.Assert(i == 0);  // That's true as everything is unsigned int, but Clousot cannot prove it yet
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 11, MethodILOffset = 1)]
    static void Main()
    {
      Test(1); // <-- requires is true
    }

    static void Test(uint value)
    {
      Contract.Requires(value <= 0x80000000U);
    }
  }

  class Long
  {
    class MaxValueLong
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 23, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 37, MethodILOffset = 0)]
      public static void M(long l)
      {
        Contract.Requires(l == long.MaxValue);

        if (l == 2)
        {
          // Should be unreached, but we report to be valid, as it is an "Assert false"
          Contract.Assert(false);
        }
        else
        { // Should be ok
          Contract.Assert(l >= 0);
        }
      }
	  
	  [ClousotRegressionTest]
	  [RegressionOutcome(Outcome=ProofOutcome.False,Message="ensures is false: Contract.Result<object>() != null",PrimaryILOffset=11,MethodILOffset=36)]
      [RegressionOutcome(Outcome=ProofOutcome.Top,Message="ensures unproven: Contract.Result<object>() != null",PrimaryILOffset=11,MethodILOffset=42)]
	  public object Repro(object Value)
      {
        Contract.Ensures(Contract.Result<object>() != null);

        var l = (long)Value;
        if (l == long.MinValue) // We had an internal overflow in Clousot, which let it think l != MinValue all the times, for all longs...
        {
          return null;
        }
        return "f";
	  }
    }
  }

  class BasicRanges
  {
    public static void RequiresByte(byte b)
    {
      Contract.Requires(b >= Byte.MinValue);
      Contract.Requires(b <= Byte.MaxValue);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 7, MethodILOffset = 1)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 23, MethodILOffset = 1)]
    public static void CallRequiresByte(byte b)
    {
      RequiresByte(b);
    }
  }

  class BitwiseOperators
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 41, MethodILOffset = 0)]
    public void AlexeyR(int shift)
    {
      Contract.Requires(shift >= 0);
      Contract.Requires(shift <= 2);

      int x = int.MaxValue >> shift * 8;  // C# generarates ((shift * 8) & 31)
      
      Contract.Assert(x > 0); 
    }
  }
}

namespace FromMscorlib
{
  public class HijriCalendar
  {
    internal virtual int GetDatePart(long ticks, int part, int[] HijriMonthDays, long NumDays)
    {
      Contract.Requires(HijriMonthDays != null);
      Contract.Requires(HijriMonthDays.Length == 13);

      int HijriMonth;                  // Hijri month
      int HijriDay;                    // Hijri day

      HijriMonth = 1;

      while ((HijriMonth <= 12) && (NumDays > HijriMonthDays[HijriMonth - 1]))
      {
        HijriMonth++;
      }
      HijriMonth--;

      Contract.Assert(HijriMonth >= 0);     // Ok, can prove it
      Contract.Assert(HijriMonth <= 13);

      //
      //  Calculate the Hijri Day.
      //
      HijriDay = (int)(NumDays - HijriMonthDays[HijriMonth - 1]);

      return HijriDay;
    }
  }

  // This test exercises the "big" numbers
  public class DateTime
  {
    private static int[] DaysToMonth365, DaysToMonth366;

    static DateTime()
    {
      DaysToMonth365 = new int[] { 0, 0x1f, 0x3b, 90, 120, 0x97, 0xb5, 0xd4, 0xf3, 0x111, 0x130, 0x14e, 0x16d };
      DaysToMonth366 = new int[] { 0, 0x1f, 60, 0x5b, 0x79, 0x98, 0xb6, 0xd5, 0xf4, 0x112, 0x131, 0x14f, 0x16e };
    }

    private ulong dateData;

    public DateTime(ulong dateData)
    {
      Contract.Requires(dateData >= 0);
      this.dateData = dateData;
    }

    private long InternalTicks
    {
      // ok
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=12,MethodILOffset=33)]
      get
      {
        Contract.Ensures(Contract.Result<long>() >= 0);
        return (((long)this.dateData) & 0x3fffffffffffffffL);
      }
    }

    // All the division by zero should be ok
    // 3 (out of 4) array accesses should raise a warning
    // One assertion fails because of lack of object invariant
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be below the lower bound",PrimaryILOffset=204,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=204,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be below the lower bound",PrimaryILOffset=221,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=221,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="Division by zero ok",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="Division by zero ok",PrimaryILOffset=30,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="Division by zero ok",PrimaryILOffset=48,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="Division by zero ok",PrimaryILOffset=72,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="Division by zero ok",PrimaryILOffset=90,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 181, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=21,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=30,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=48,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=72,MethodILOffset=0)]
 [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=90,MethodILOffset=0)]
    [ClousotRegressionTest]    
    private int GetDatePart(int part)
    {
      Contract.Assert(0xc92a69c000L != 0);    // This is converted by the compiler to "1", and it undercovered a bug in the CheckIfHolds of intervals, so we keep it

      int num2 = (int)(this.InternalTicks / 0xc92a69c000L);
      int num3 = num2 / 0x23ab1;
      num2 -= num3 * 0x23ab1;
      int num4 = num2 / 0x8eac;
      if (num4 == 4)
      {
        num4 = 3;
      }
      num2 -= num4 * 0x8eac;
      int num5 = num2 / 0x5b5;
      num2 -= num5 * 0x5b5;
      int num6 = num2 / 0x16d;

      if (num6 == 4)
      {
        num6 = 3;
      }
      if (part == 0)
      {
        return (((((num3 * 400) + (num4 * 100)) + (num5 * 4)) + num6) + 1);
      }
      num2 -= num6 * 0x16d;
      if (part == 1)
      {
        return (num2 + 1);
      }

      int[] numArray = ((num6 == 3) && ((num5 != 0x18) || (num4 == 3))) ? DaysToMonth366 : DaysToMonth365;

      // TODO: With class invariants should infer DaysToMonth35?.Length == 13
      Contract.Assert(numArray.Length == 13);

      int index = num2 >> 6;
      
      // Here should prove index < 13, which is quite hard
      while (num2 >= numArray[index])
      {
        index++;
      }
      if (part == 2)
      {
        return index;
      }
      return ((num2 - numArray[index - 1]) + 1);
    } 
  }

  public class Enum
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow (caused by a negative array size)",PrimaryILOffset=3,MethodILOffset=0)]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=28,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 32, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 32, MethodILOffset = 0)]
#endif
    private static object GetHashEntry(Type enumType, bool[] flds, ulong val)
    {
      ulong[] values = new ulong[flds.Length];
      string[] names = new string[flds.Length]; // /optimize+ removes this array creation,

      for (int i = 1; i < values.Length; i++)
      {
        int j = i;

        while (values[j - 1] > val)
        {
          j--;

          if (j == 0)
          {
            break;
          }
        }
      }
      return null;
    }
  }

}

namespace Multiplication
{
  public class Exponent
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 76, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: Contract.Result<long>() >= 0L", PrimaryILOffset = 25, MethodILOffset = 82)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Division by zero ok", PrimaryILOffset = 38, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Division by zero ok", PrimaryILOffset = 51, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 51, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 38, MethodILOffset = 0)]
    public static long Exp_Reflector(long x, long y)
    {
      Contract.Requires(y >= 0L);

      Contract.Ensures(Contract.Result<long>() >= 0L);

      long num = 1L;

      while (y > 0L)
      {
        if ((y % 2L) == 0L)
        {
          x *= x;
          y /= 2L;
        }
        else
        {
          num *= x;
          y -= 1L;
        }
      }
      Contract.Assert(y == 0L);

      return num;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 76, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: Contract.Result<long>() >= 0", PrimaryILOffset = 25, MethodILOffset = 82)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Division by zero ok", PrimaryILOffset = 38, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Division by zero ok", PrimaryILOffset = 51, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 51, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 38, MethodILOffset = 0)]
    public static long Exp(long x, long y)
    {
      Contract.Requires(y >= 0);
      Contract.Ensures(Contract.Result<long>() >= 0);

      long result = 1;
      while (y > 0)
      {
        if (y % 2 == 0)
        {
          x = x * x;
          y = y / 2;
        }
        else
        {
          result *= x;
          y--;
        }
      }
      Contract.Assert(y == 0);
      return result;
    }
  }
}

namespace Rational
{
  class Rational
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 37, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 47, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 26, MethodILOffset = 44)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 26, MethodILOffset = 54)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 47, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 37, MethodILOffset = 0)]
    private static int GCD(int x, int y)
    {
      Contract.Requires(x > 0);
      Contract.Requires(y > 0);

      Contract.Ensures(Contract.Result<int>() > 0);

      while (true)
      {
        if (x < y)
        {
          y %= x;
          if (y == 0)
          {
            return x;
          }
        }
        else
        {
          x %= y;
          if (x == 0)
          {
            return y;
          }
        }
      }
    }

    int d, n;
    bool pos;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 44, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 47, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 4, MethodILOffset = 35)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 35)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 44, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 47, MethodILOffset = 0)]
    static public Rational NormalizedRational(bool pos, int x, int y)
    {
      Contract.Requires(x >= 0);
      Contract.Requires(y > 0);

      if (x == 0)
      {
        return new Rational(true, 0, 1);
      }

      int gcd = GCD(x, y);
      return new Rational(pos, x / gcd, y / gcd);
    }

    [ClousotRegressionTest]
    private Rational(bool pos, int x, int y)
    {
      this.pos = pos;
      this.d = x;
      this.n = y;
    }
  }
}

namespace Unsigned
{
  class JITStylePrecondition
  {
    public void PreconditionWithUInts(int index, int length)
    {
      Contract.Requires(length >= 0);
      Contract.Requires((uint)index <= (uint)length);



      Contract.Assert(index >= 0);
    }
  }
}

namespace BitwiseXor
{
public class DaveSexton
{
 [ClousotRegressionTest]
 [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=5,MethodILOffset=3)]
 [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=23,MethodILOffset=3)]
 [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=5,MethodILOffset=11)]
 [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=23,MethodILOffset=11)]
 [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=5,MethodILOffset=19)]
 [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=23,MethodILOffset=19)]
 [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"requires is false: first ^ second ^ third",PrimaryILOffset=5,MethodILOffset=94)]
 [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message=@"requires unreachable",PrimaryILOffset=23,MethodILOffset=94)]
 [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=5,MethodILOffset=85)]
 [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"requires is false: !(first && second && third)",PrimaryILOffset=23,MethodILOffset=85)]
 [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"requires is false: first ^ second ^ third",PrimaryILOffset=5,MethodILOffset=76)]
 [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message=@"requires unreachable",PrimaryILOffset=23,MethodILOffset=76)]
 [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"requires is false: first ^ second ^ third",PrimaryILOffset=5,MethodILOffset=67)]
   [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message=@"requires unreachable",PrimaryILOffset=23,MethodILOffset=67)]
 [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"requires is false: first ^ second ^ third",PrimaryILOffset=5,MethodILOffset=58)]
 [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message=@"requires unreachable",PrimaryILOffset=23,MethodILOffset=58)]
  static void Test(int x)
  {
    // corrects
    ExclusiveOr(true, false, false); // true ^ false -> true    
    ExclusiveOr(false, true, false); // true ^ false -> true
    ExclusiveOr(false, false, true); // false ^ true -> true

    // incorrects
    switch (x)
    {
      case 1:
        ExclusiveOr(true, true, false);    // false ^ false -> false
        break;

      case 2:
        ExclusiveOr(true, false, true);    // true ^ true -> false
        break;

      case 3:
        ExclusiveOr(false, true, true);    // true ^ true -> false
        break;

      case 4:
        ExclusiveOr(true, true, true);     // true ^ true -> false
        break;

      case 5:
        ExclusiveOr(false, false, false);  // false ^ false -> false    
        break;

      default:
        break;
    }
  }

  static void ExclusiveOr(bool first, bool second, bool third)
  {
    Contract.Requires(first ^ second ^ third);
    Contract.Requires(!(first && second && third));
  }
}
}

namespace Repro
{
  class AlexeyR
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Array creation : ok",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=9,MethodILOffset=0)]	
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=22,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow (caused by a negative array size)",PrimaryILOffset=1,MethodILOffset=0)]
    public void ldelem()
    {
      byte[] buf = new byte[1];
      byte val = buf[0];
      Contract.Assert(val <= byte.MaxValue); 
    }
  }
  
  class Eric
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: x >= 0",PrimaryILOffset=7,MethodILOffset=2)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: x <= 255",PrimaryILOffset=23,MethodILOffset=2)]
    public void Convert_NotOk(int x)
    {
      RequiresBytes(x);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=7,MethodILOffset=2)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=23,MethodILOffset=2)]
    public void Convert_Ok(byte x)
    {
      RequiresBytes(x);
    }

    public void RequiresBytes(int x)
    {
      Contract.Requires(x >= 0);
      Contract.Requires(x <= 255);
    }
  }
}


namespace ExampleFromPapers
{
  // Example taken from the paper on combination of domains in Astree:
  // "Combination of Abstractions in the ASTR�E Static Analyzer" 
  public class AstreeCombinationOfDomains
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=45,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=58,MethodILOffset=0)]
    void Main1(int z, bool b2, int y)
    {
      Contract.Assume(y == 0);
      
      var x = 0;
      while (z > 0)
	{
	  x = x + 1;
	  if (b2)
	    x = y;
	  
	  if (x == 10)
	    x = 0;
	  
	  z--;
	}

      Contract.Assert(x >= 0);
      Contract.Assert(x <= 9);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=45,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=57,MethodILOffset=0)]
    void Main1_wrong(int z, bool b2, int y)
    {
      Contract.Assume(y == 0);
      
      var x = 0;
      while (z > 0)
	{
	  x = x + 1;
	  if (b2)
	    x = y;
	  
	  if (x == 10)
	    x = 0;
	  
	  z--;
	}

      Contract.Assert(x >= 0);
      Contract.Assert(x <= 5);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=61,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=74,MethodILOffset=0)]
    void Main2(int z, bool b2, int y)
    {
      Contract.Assume(y >= 0);
      Contract.Assume(y <= 9);
      
      var x = 0;
      while (z > 0)
	{
	  x = x + 1;
	  if (b2)
	    x = y;
	  
	  if (x == 10)
	    x = 0;
	  
	  z--;
	}

      Contract.Assert(x >= 0);
      Contract.Assert(x <= 9);
    }
  }
}

namespace HermanRepro
{
  public class Bits
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=16,MethodILOffset=0)] 
    public void Bits1(uint index)
    {
      Contract.Assert(((index & 0xFF00) >> 8) < 0x100);
    }

    [ClousotRegressionTest]    
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=17,MethodILOffset=0)]
    public void Bits2(uint index)
    {
      Contract.Assert(((index & 0xFF00) >> 16) < 0x100);
    }
  
    [ClousotRegressionTest]    
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=17,MethodILOffset=0)]    
    public void Bits3(uint index)
    {
      Contract.Assert((index & 0xFF0000) >> 16 < 0x100);
    }
  }
  
    public class UnsignedRepro
   {
    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=14,MethodILOffset=0)]
    public void UInt32epro(uint unsignedValue)
    {
      int z;
      if (unsignedValue == uint.MaxValue)
      {
        z = 1;
      }
      else
      {
        z = 2;
      }

      Contract.Assert(z == 2);// should be unproven
    }

    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=15,MethodILOffset=0)]
    public void UInt64epro(ulong unsignedValue)
    {
      int z;
      if (unsignedValue == ulong.MaxValue)
      {
        z = 1;
      }
      else
      {
        z = 2;
      }

      Contract.Assert(z == 2);// should be unproven
    }

    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=19,MethodILOffset=0)]
    public void UIntRepro2(object operandValue)
    {
      var ui = (uint)operandValue;

      var z = 2;
      if (ui == uint.MaxValue)
        z = 1;

      Contract.Assert(z == 2); // should be unproven
    }
  }
}

namespace ForumRepro
{
  class Program
  {
    public static int Divide(int numerator, int denominator, out int remainder)
    {
      //Contract.Requires<ArgumentException>(denominator != 0);
      //Contract.Requires<ArgumentException>(numerator != int.MinValue || denominator != -1, "Overflow");
      Contract.Ensures(Contract.Result<int>() == numerator / denominator); // We add a bug in the division of polynomials, where we ignored the fact that integer division != rational division
      Contract.Ensures(Contract.ValueAtReturn<int>(out remainder) == numerator % denominator);

      remainder = numerator % denominator;
      return numerator / denominator;
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=24,MethodILOffset=0)]
    static void Main(string[] args)
    {
      int remainder;
      Console.WriteLine(Divide(10, 6, out remainder));

      Contract.Assert(args.Length >= 0);      
    }
  }
}

namespace BugFromVSCustomers
{
  class Program
  {
    public static void Test(double x) 
    { 
      Contract.Requires(x > 0.0d); 
    } 
    
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=12,MethodILOffset=9)]
    static void Main(string[] args) 
    { 
	  // We used to say this is false.
	  // Intervals of ints cannot prove it, we need intervals of floating points
      Test(0.25d); 
    }
  }
}

namespace BugFromCloudDev
{
  public class Messaging
  {
    public long m_totalMessages;

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Division by zero ok",PrimaryILOffset=12,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="assert is false",PrimaryILOffset=18,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=12,MethodILOffset=0)]
    public void UnreachedWithLongRem()
    {
      if ((m_totalMessages % 10000) == 0)
      {
		// It may be reached
        Contract.Assert(false);
      }
    }
  }
}

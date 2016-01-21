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

[assembly:RegressionOutcome("Method 'Repro.EmperorXLII.Reciprocal(System.Int32)' has custom parameter validation but assembly mode is not set to support this. It will be treated as Requires<E>.")]

namespace TestArithmetics
{
  class TestNeg
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possible negation of MinValue of type Int32. The static checker determined that the condition 'key.GetHashCode() != Int32.MinValue' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(key.GetHashCode() != Int32.MinValue);", PrimaryILOffset = 20, MethodILOffset = 0)]
    public void R(object key, int[] m_key)
    {
      int hashcode = key.GetHashCode();

      if (hashcode < 0)
        hashcode = -hashcode;

      // ... etc.
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possible negation of MinValue of type Int64. The static checker determined that the condition 'l != -9223372036854775808' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(l != -9223372036854775808);", PrimaryILOffset = 14, MethodILOffset = 0)]
    public long Abs_Wrong(long l)
    {
      if (l < 0)
        return -l;
      else
        return l;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Negation ok (no MinValue) of type SByte", PrimaryILOffset = 32, MethodILOffset = 0)]
    public SByte Abs_Ok(SByte l)
    {
      if (l >= 0)
        return l;

      if (l == SByte.MinValue)
        throw new Exception();

      return (sbyte)-l;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Negation ok (no MinValue) of type Int16", PrimaryILOffset = 35, MethodILOffset = 0)]
    public Int16 Abs_Ok(Int16 l)
    {
      if (l >= 0)
        return l;

      if (l == Int16.MinValue)
        throw new Exception();

      return (short)(-l);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Negation ok (no MinValue) of type Int32", PrimaryILOffset = 35, MethodILOffset = 0)]
    public Int32 Abs_Ok(Int32 l)
    {
      if (l >= 0)
        return l;

      if (l == Int32.MinValue)
        throw new Exception();

      return -l;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Negation ok (no MinValue) of type Int64", PrimaryILOffset = 40, MethodILOffset = 0)]
    public Int64 Abs_Ok(Int64 l)
    {
      if (l >= 0)
        return l;

      if (l == Int64.MinValue)
        throw new Exception();

      return -l;
    }
  }

  class DivByZero
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possible division by zero", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible overflow in division (MinValue / -1)", PrimaryILOffset = 3, MethodILOffset = 0)]
    public int Div_Wrong(int a, int b)
    {
      return a / b;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Division by zero ok", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible overflow in division (MinValue / -1)", PrimaryILOffset = 16, MethodILOffset = 0)]
    public int Div_OK(int a, int b)
    {
      Contract.Requires(b != 0);

      return a / b;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possible division by zero", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible overflow in division (MinValue / -1)", PrimaryILOffset = 3, MethodILOffset = 0)]
    public int Rem_Wrong(int a, int b)
    {
      return a % b;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Division by zero ok", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible overflow in division (MinValue / -1)", PrimaryILOffset = 16, MethodILOffset = 0)]
    public int Rem_OK(int a, int b)
    {
      Contract.Requires(b != 0);

      return a % b;
    }
  }

  class DivByZeroFloatingPoint
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The arguments of the comparison have a compatible precision", PrimaryILOffset = 12, MethodILOffset = 0)]

    // Input can be NaN
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation",PrimaryILOffset=45,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=45,MethodILOffset=0)]
    // No warning as we can divide a double by zero
    public double DivZeroTest(double intValue)
    {
      if ((double)intValue == 0.0)    
      {
        intValue = 1.0;
      }
      return (100.0 / (double) intValue);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The arguments of the comparison have a compatible precision", PrimaryILOffset = 12, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation",PrimaryILOffset=36,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=36,MethodILOffset=0)]
    // No warning as we can divide a float by zero
    public double DivZeroTest(float intValue)
    {
      if ((double)intValue == 0.0f)
      {
        intValue = 1.0f;
      }
      return (100.0f / intValue);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The arguments of the comparison have a compatible precision",PrimaryILOffset=12,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible precision mismatch for the arguments of ==",PrimaryILOffset=29,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=34,MethodILOffset=0)]
    // h can be NaN
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation. The static checker determined that the condition '-1.79769313486232E+308 <= (1 / h)' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(-1.79769313486232E+308 <= (1 / h));",PrimaryILOffset=50,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation. The static checker determined that the condition '(1 / h) <= 1.79769313486232E+308' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires((1 / h) <= 1.79769313486232E+308);",PrimaryILOffset=50,MethodILOffset=0)]
    public double DivZeroRest_WithEqEq(double h)
    {
      if ((double)h != 0.0) 
      {
        Contract.Assert(h != 0.0);
        return 1.0 / h;
      }

      return 1.0;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible precision mismatch for the arguments of ==",PrimaryILOffset=32,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=37,MethodILOffset=0)]
    // h can be NaN
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation. The static checker determined that the condition '-1.79769313486232E+308 <= (1 / h)' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(-1.79769313486232E+308 <= (1 / h));",PrimaryILOffset=53,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation. The static checker determined that the condition '(1 / h) <= 1.79769313486232E+308' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires((1 / h) <= 1.79769313486232E+308);",PrimaryILOffset=53,MethodILOffset=0)]
    public double DivZeroTest_WithEquals(double h)
    {
      if (!h.Equals(0.0))
      {
        Contract.Assert(h != 0.0);
        return  1.0 / h; 
      }

      return 1.0;
    }
  }

  class TryFloat
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=23,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=23,MethodILOffset=0)]
    public double Simple0()
    {
      double x = 22.0d;
      double y = 7.0d;

      double r = x / y;
      return r;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 7, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The arguments of the comparison have a compatible precision", PrimaryILOffset = 5, MethodILOffset = 0)]
    public void Simple(float f)
    {
      float z = f;

      Contract.Assert(z == f);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 26, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=15,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=15,MethodILOffset=0)]
    public void Simple2()
    {
      float x = 0.5f;
      float y = 0.3f;

      Contract.Assert(x + y >= 0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 35, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 55, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=23,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=23,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=43,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=43,MethodILOffset=0)]
    public void Simple3()
    {
      double x = 0.5d;
      double y = 0.3d;

      Contract.Assert(x + y > 0);
      Contract.Assert(x + y < 1);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 51, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=27,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=27,MethodILOffset=0)]
    public void Simple4(float input)
    {
      Contract.Requires(input >= 0.1f);

      while (input < 1)
      {
        input += 0.1f;
      }

      Contract.Assert(input > 0);
    }

    // F: the assertions should hold, but we need some relational info
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 98, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 119, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=45,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=45,MethodILOffset=0)]
 [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation",PrimaryILOffset=58,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=58,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation",PrimaryILOffset=59,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=59,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation",PrimaryILOffset=65,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=65,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation",PrimaryILOffset=66,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=66,MethodILOffset=0)]
    public double Sin(int N)
    {
      Contract.Requires(N >= 1);

      var c = 1.0d;
      var s = 0.0d;
      var d = 2 * 3.14 / N;

      for (int i = 0; i < N; i++)
      {
        // Very approximated formula
        // sn+1 = sn + d × cn 
        // cn+1 = cn − d × sn 

        var tmp = s;

        s = s + d * c;
        c = c - d * tmp;
      }

      Contract.Assert(s >= -1);
      Contract.Assert(c >= -1);


      return s;
    }

    // We can prove the assertions thanks to the fact that we track values to subtrees - and we use it with WPs
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible precision mismatch for the arguments of ==",PrimaryILOffset=64,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=69,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=121,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=54,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=54,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=78,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=78,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=89,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=89,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=90,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=90,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=91,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=91,MethodILOffset=0)]
     public double SquareRoot(double x, int iter)
    {
      Contract.Requires(x > 0);
      Contract.Requires(iter > 0);

      var res = 10d;
      for (int i = 0; i < iter; i++)
      {
        Contract.Assert(2 * res != 0.0);
        res = res - ((res * res) / (2 * res));
      }

      Contract.Assert(res >= 0);

      return res;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 9, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 16, MethodILOffset = 0)]
    public void Epsilon()
    {
      Contract.Assert(Double.Epsilon != Double.Epsilon * Double.Epsilon);

      Contract.Assert(1.0d == 1.0d + Double.Epsilon);

      Contract.Assert(1000000000d == 1000000000d + Double.Epsilon);
    }

    // F: It is ok that Clousot cannot prove it, because of rounding errors the postcondition does not hold in the concrete
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"ensures is false: Contract.Result<double>() == 2.0e21d", PrimaryILOffset = 17, MethodILOffset = 68)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible precision mismatch for the arguments of ==", PrimaryILOffset = 15, MethodILOffset = 68)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=43,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=43,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=55,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=55,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=59,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=59,MethodILOffset=0)]
    public double Astree_NotOk()
    {
      Contract.Ensures(Contract.Result<double>() == 2.0e21d);

      double x, y, z, r;

      x = 1.000000019e+38d;
      y = x + 1.0e21d;
      z = x - 1.0e21d;
      r = y - z;

      return r;
    }

    // F: this is ok because of rounding errors
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 18, MethodILOffset = 69)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The arguments of the comparison have a compatible precision", PrimaryILOffset = 16, MethodILOffset = 69)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=44,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=44,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=56,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=56,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=60,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=60,MethodILOffset=0)]
    public double Astree1_Ok()
    {
      Contract.Ensures((double)Contract.Result<double>() == 0.0d);

      double x, y, z, r;

      x = 1.000000019e+38d;
      y = x + 1.0e21d;
      z = x - 1.0e21d;
      r = y - z;

      return r;
    }

    // F: unfortunately this is proven (wrong!) by intervals of rational
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 17, MethodILOffset = 71)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The arguments of the comparison have a compatible precision", PrimaryILOffset = 15, MethodILOffset = 71)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=43,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=43,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=56,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=56,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=61,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=61,MethodILOffset=0)]
    public double Astree2()
    {
      // We do get the warning on the precision, because the compiler puts a cast to double to the result value
      Contract.Ensures(Contract.Result<double>() == 2.0d);

      double x; float y, z, r;
      /* x = ldexp(1.,50)+ldexp(1.,26); */
      x = 1125899973951488.0;
      y = (float)(x + 1.0d);
      z = (float)(x - 1.0d);

      r = y - z;

      return r; // here the C# compiler puts the cast to double
    }


  }

  public class Geocoordinate
  {
    [ClousotRegressionTest]
    public Geocoordinate(double latitude, double longitude)
    {
      Contract.Requires<ArgumentException>(latitude <= 90.0 && latitude >= -90.0);
      Contract.Requires<ArgumentException>(longitude <= 180.0 && longitude >= -180.0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 38, MethodILOffset = 23)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 74, MethodILOffset = 23)]
    public void Try()
    {
      double la = 10;
      double lo = 10;

      var g = new Geocoordinate(la, lo);
    }
  }

  public class Geocoordinate_WithNot
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The arguments of the comparison have a compatible precision", PrimaryILOffset = 19, MethodILOffset = 0)]
    public Geocoordinate_WithNot(double latitude, double longitude)
    {
      Contract.Requires<ArgumentException>((double)latitude != 10);
      Contract.Requires<ArgumentException>(latitude <= 90.0 && latitude >= -90.0);
      Contract.Requires<ArgumentException>(longitude <= 180.0 && longitude >= -180.0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 38, MethodILOffset = 23)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 74, MethodILOffset = 23)]
    public void Try()
    {
      double la = 10;
      double lo = 10;

      var g = new Geocoordinate(la, lo);
    }
  }

  public class Interval
  {
    public int lowerBound, upperBound;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: val != System.Int32.MinValue. The static checker determined that the condition '(int)((long)(left.upperBound) * (long)(left.upperBound)) != Int32.MinValue' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires((int)((long)(left.upperBound) * (long)(left.upperBound)) != Int32.MinValue);",PrimaryILOffset=12,MethodILOffset=34)]
    public Int32 OpenLeftOpenLeft(Interval left, Interval right)
    {
      Int64 newUpperBound = ((Int64)left.upperBound * (Int64)left.upperBound);
      if (newUpperBound != System.Int32.MinValue)
      {
	// In general the precondition is not true because even if the Int64 value != Int32.MinValue, it may be the case that once truncated is Int32.MinValue
        var abs = Abs((Int32)newUpperBound);
        return abs;
      }
      else
      {
        return 0;
      }
    }

    private Int32 Abs(Int32 val)
    {
      Contract.Requires(val != System.Int32.MinValue);

      return val < 0 ? val : -val;
    }
  }

  public class TestFloatTypesInference
  {
    private double balance;

    public TestFloatTypesInference(double balance)
    {
      this.balance = balance;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible precision mismatch for the arguments of ==", PrimaryILOffset = 8, MethodILOffset = 0)]
    public bool GuessBalance(double guess)
    {
      return this.balance == guess;
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The arguments of the comparison have a compatible precision", PrimaryILOffset = 9, MethodILOffset = 0)]
    public bool GuessBalance_WithCast(double guess)
    {
      return this.balance == (double)guess;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible precision mismatch for the arguments of ==", PrimaryILOffset = 31, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 33, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation",PrimaryILOffset=16,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=16,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation",PrimaryILOffset=30,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=30,MethodILOffset=0)]
    public void Deposit_NoEnsures(double amount)
    {
      double old = this.balance;

      this.balance = this.balance + amount;

      Contract.Assert(this.balance == old + amount);
    }

    // Should be fixed, it is not working now
    //[ClousotRegressionTest]
    public void Deposit_NoEnsures_WithCast(double amount)
    {
      double old = this.balance;

      this.balance = this.balance + amount;

      Contract.Assert(this.balance == (double)(old + amount));
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible precision mismatch for the arguments of ==", PrimaryILOffset = 20, MethodILOffset = 42)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 22, MethodILOffset = 42)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation",PrimaryILOffset=36,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=36,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation",PrimaryILOffset=19,MethodILOffset=42)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=19,MethodILOffset=42)]
    public void Deposit(double amount)
    {
      Contract.Ensures(this.balance == (Contract.OldValue(this.balance) + amount));

      this.balance = this.balance + amount;
    }

    // Should be fixed, it is not working now
    //[ClousotRegressionTest]
    public void Deposit_WithCast(double amount)
    {
      Contract.Ensures(this.balance == (double)(Contract.OldValue(this.balance) + amount));

      this.balance = this.balance + amount;
    }
  }

  public class TestFloatTypeInferenceWithMatrices
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The arguments of the comparison have a compatible precision", PrimaryILOffset = 25, MethodILOffset = 0)]
    static public bool ReadFromMatrix(double[] d)
    {
      Contract.Requires(d.Length > 0);

      return d[0] != 0.0;
    }
  }

  public class AlexanderTaeschner
  {
    // F: this test is here because the MaxValue and MinValue causes a crash in Clousot
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible precision mismatch for the arguments of ==", PrimaryILOffset = 148, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible precision mismatch for the arguments of ==", PrimaryILOffset = 197, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 104, MethodILOffset = 275)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation. The static checker determined that the condition '-1.79769313486232E+308 <= (System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1))' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(-1.79769313486232E+308 <= (System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1)));",PrimaryILOffset=171,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation. The static checker determined that the condition '(System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1)) <= 1.79769313486232E+308' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires((System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1)) <= 1.79769313486232E+308);",PrimaryILOffset=171,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation. The static checker determined that the condition '-1.79769313486232E+308 <= (System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1))' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(-1.79769313486232E+308 <= (System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1)));",PrimaryILOffset=174,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation. The static checker determined that the condition '(System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1)) <= 1.79769313486232E+308' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires((System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1)) <= 1.79769313486232E+308);",PrimaryILOffset=174,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation. The static checker determined that the condition '-1.79769313486232E+308 <= ((System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1)) * (System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1)))' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(-1.79769313486232E+308 <= ((System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1)) * (System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1))));",PrimaryILOffset=175,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation. The static checker determined that the condition '((System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1)) * (System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1))) <= 1.79769313486232E+308' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(((System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1)) * (System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1))) <= 1.79769313486232E+308);",PrimaryILOffset=175,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation. The static checker determined that the condition '-1.79769313486232E+308 <= (1 + (System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1)) * (System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1)))' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(-1.79769313486232E+308 <= (1 + (System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1)) * (System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1))));",PrimaryILOffset=176,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation. The static checker determined that the condition '(1 + (System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1)) * (System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1))) <= 1.79769313486232E+308' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires((1 + (System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1)) * (System.Math.Abs(cathetus2) / System.Math.Abs(cathetus1))) <= 1.79769313486232E+308);",PrimaryILOffset=176,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation. The static checker determined that the condition '-1.79769313486232E+308 <= (System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2))' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(-1.79769313486232E+308 <= (System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2)));",PrimaryILOffset=235,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation. The static checker determined that the condition '(System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2)) <= 1.79769313486232E+308' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires((System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2)) <= 1.79769313486232E+308);",PrimaryILOffset=235,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation. The static checker determined that the condition '-1.79769313486232E+308 <= (System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2))' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(-1.79769313486232E+308 <= (System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2)));",PrimaryILOffset=238,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation. The static checker determined that the condition '(System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2)) <= 1.79769313486232E+308' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires((System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2)) <= 1.79769313486232E+308);",PrimaryILOffset=238,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation. The static checker determined that the condition '-1.79769313486232E+308 <= ((System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2)) * (System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2)))' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(-1.79769313486232E+308 <= ((System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2)) * (System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2))));",PrimaryILOffset=239,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation. The static checker determined that the condition '((System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2)) * (System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2))) <= 1.79769313486232E+308' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(((System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2)) * (System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2))) <= 1.79769313486232E+308);",PrimaryILOffset=239,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation. The static checker determined that the condition '-1.79769313486232E+308 <= (1 + (System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2)) * (System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2)))' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(-1.79769313486232E+308 <= (1 + (System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2)) * (System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2))));",PrimaryILOffset=240,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation. The static checker determined that the condition '(1 + (System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2)) * (System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2))) <= 1.79769313486232E+308' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires((1 + (System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2)) * (System.Math.Abs(cathetus1) / System.Math.Abs(cathetus2))) <= 1.79769313486232E+308);",PrimaryILOffset=240,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation",PrimaryILOffset=246,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=246,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation",PrimaryILOffset=182,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=182,MethodILOffset=0)]
    public static double Pythagoras(double cathetus1, double cathetus2)
    {
      Contract.Requires(cathetus1 >= double.MinValue);
      Contract.Requires(cathetus1 <= double.MaxValue);
      Contract.Requires(cathetus2 >= double.MinValue);
      Contract.Requires(cathetus2 <= double.MaxValue);

      Contract.Ensures(Contract.Result<double>() >= 0.0);

      double absA = Math.Abs(cathetus1);
      double absB = Math.Abs(cathetus2);

      double c;
      if (absA > absB)
      {
        Contract.Assume(absA != 0.0);
        c = absA * Math.Sqrt(1.0 + (absB / absA) * (absB / absA));
      }
      else if (absB == 0.0)
      {
        c = 0.0;
      }
      else
      {
        c = absB * Math.Sqrt(1.0 + (absA / absB) * (absA / absB));
      }

      Contract.Assume(c >= 0.0);
      return c;
    }
  }

  public class TestDivOverlflow
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible division by zero", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible overflow in division (MinValue / -1)", PrimaryILOffset = 3, MethodILOffset = 0)]
    public static int Divide(int x, int y)
    {
      return x / y;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 46, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 46, MethodILOffset = 0)]
    public static int DivideWithContracts(int x, int y)
    {
      Contract.Requires(x != Int32.MinValue);
      Contract.Requires(y != -1);
      Contract.Requires(y != 0);

      return x / y;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Division by zero ok",PrimaryILOffset=40,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=40,MethodILOffset=0)]
    public static int DivideWithWeakerContract(int x, int y)
    {
      Contract.Requires(y != 0);
      Contract.Requires(!(x == Int32.MinValue && y == -1));
      return x /y; 
    }    
  }

  public class TestModOverflow
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible overflow in division (MinValue / -1)", PrimaryILOffset = 16, MethodILOffset = 0)]
    public static int Mod(int x, int y)
    {
      Contract.Requires(y != 0);

      return x % y;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 46, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 46, MethodILOffset = 0)]
    public static int ModWithContracts(int x, int y)
    {
      Contract.Requires(x != Int32.MinValue);
      Contract.Requires(y != -1);
      Contract.Requires(y != 0);

      return x % y;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Division by zero ok",PrimaryILOffset=40,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=40,MethodILOffset=0)]
    public static int ModWithWeakerContract(int x, int y)
    {
      Contract.Requires(y != 0);
      Contract.Requires(!(x == Int32.MinValue && y == -1));
      return x % y;
    }
  }
}

namespace ArithmeticOverflow
{
  public class Tests
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=3,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible overflow in the arithmetic operation",PrimaryILOffset=3,MethodILOffset=0)]
    public int Incr_Warning(int x)
    {
      return checked(x + 1);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=17,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"Overflow in the arithmetic operation",PrimaryILOffset=17,MethodILOffset=0)]
    public int Incr_Wrong(int x)
    {
      Contract.Requires(x == Int32.MaxValue);
      return checked(x + 1);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=30,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=30,MethodILOffset=0)]
    public int Incr_ok(int x)
    {
      Contract.Requires(x >= 0);
      Contract.Requires(x <= 100);

      return checked(x + 1);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=4,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible overflow in the arithmetic operation",PrimaryILOffset=4,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=5,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=5,MethodILOffset=0)]
    public byte Incr_Byte_Warning(byte x)
    {
      return checked(x++);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible underflow in the arithmetic operation",PrimaryILOffset=4,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=4,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=5,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No overflow",PrimaryILOffset=5,MethodILOffset=0)]
    public byte Decr_Byte_Warning(byte b)
    {
      return checked(b--);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"Underflow in the arithmetic operation",PrimaryILOffset=14,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=14,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="Underflow in the arithmetic operation",PrimaryILOffset=15,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No overflow",PrimaryILOffset=15,MethodILOffset=0)]
    public byte Decr_Byte_Wrong(byte b)
    {
      Contract.Requires(b == byte.MinValue+5);

      return checked(b-= 12);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=35,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=35,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=36,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=36,MethodILOffset=0)]    
    public byte Decr_Byte_Ok(byte b)
    {
      Contract.Requires(b >= 100);
      Contract.Requires(b <= 150);

      return checked(b -= 12);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=16,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=16,MethodILOffset=0)]
    public int Decr_Int_Ok(int x)
    {
      Contract.Requires(x >= 0);
 
      return checked(x - 1);
    }

    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible overflow (caused by a negative array size) in the arithmetic operation. The static checker determined that the condition '0 <= (len + 1)' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(0 <= (len + 1));",PrimaryILOffset=17,MethodILOffset=0)]
    public float[] ArrayCreation_Warning(int len)
    {
      Contract.Requires(len >= 0);

      return new float[len + 1];
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow (caused by a negative array size)",PrimaryILOffset=31,MethodILOffset=0)]
    public float[] ArrayCreation_Ok(int len)
    {
      Contract.Requires(len >= 0);
      Contract.Requires(len < Int32.MaxValue);

      return new float[len + 1];
    }

    // We get a true here because the bounds analysis will find that len + 1 == Int32.MinValue, and hence raise the warning
    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="No overflow (caused by a negative array size)",PrimaryILOffset=18,MethodILOffset=0)]
    public float[] ArrayCreation_Wrong(int len)
    {
      Contract.Requires(len == Int32.MaxValue);

      return new float[len + 1];
    }

    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible overflow (caused by a negative array size) in the arithmetic operation. The static checker determined that the condition '0 <= (Int32.MaxValue + z)' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(0 <= (Int32.MaxValue + z));",PrimaryILOffset=28,MethodILOffset=0)]
    public float[] ArrayCreation_Wrong(int len, int z)
    {
      Contract.Requires(len == Int32.MaxValue);
      Contract.Requires(z > 0);

      return new float[len + z];
    }
  
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow (caused by a negative array size)",PrimaryILOffset=15,MethodILOffset=0)]
    public object[] CreateArray(int len)
    {
      Contract.Requires(len >= 0);

      return new object[len];      
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=32,MethodILOffset=0)]							   
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=32,MethodILOffset=0)]
    public byte Convert_Ok(int x)
    {
      Contract.Requires(x >= 0);
      Contract.Requires(x <= 128);

      return checked((byte)x);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible underflow in the arithmetic operation",PrimaryILOffset=2,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible overflow in the arithmetic operation",PrimaryILOffset=2,MethodILOffset=0)]
    public byte Convert_Warning(int x)
    {     
      return checked((byte)x);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=19,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"Overflow in the arithmetic operation",PrimaryILOffset=19,MethodILOffset=0)]
    public byte Convert_Wrong(int x)
    {
      Contract.Requires(x >= 500);

      return checked((byte)x);
    }
  }
}

namespace NaN
{
  public class TestNaN
  {
    
    public void RequiresNotNaN(double d)
    {
      Contract.Requires(!Double.IsNaN(d));
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 10, MethodILOffset = 11)]
    public void Call()
    {
      RequiresNotNaN(0d);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: !Double.IsNaN(d)", PrimaryILOffset = 10, MethodILOffset = 3)]
    public void Call2(double d)
    {
      RequiresNotNaN(d);
    }
  }

  public class AlexeyR
  {
    static int GetInt()
    {
      return new Random().Next();
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=19,MethodILOffset=0)]
    static void IntIsNaN()
    {
      int x = GetInt();
      double d = (double)x;

      Contract.Assert(!double.IsNaN(d));
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=60,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=51,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=51,MethodILOffset=0)]
    static void DoubleMul()
    {
      int x = GetInt();
      int y = GetInt();
      double d = (double)x;
      double e = (double)y;

      Contract.Assume(!double.IsNaN(d));
      Contract.Assume(!double.IsNaN(e));
      Contract.Assert(!double.IsNaN(d * e)); 
    }
  }
}

namespace AlexeyR
{
  class DoublesTest
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven. Is it an off-by-one? The static checker can prove (x / (x + 1)) > (0 - 1) instead", PrimaryILOffset = 47, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=29,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=29,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=33,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=33,MethodILOffset=0)]
    static void Test1_NotOk(double x)
    {
      Contract.Requires(x > 0);

      double y = x + 1;
      double z = x / y;

      Contract.Assert(z > 0);     // The assertion can fail as x can be Double.PlusInfinity, and Double.PlusInfinity / (Double.PlusInfinity + 1) is Double.NaN
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 68, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 89, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=50,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=50,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=54,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=54,MethodILOffset=0)]
    static void Test1_OK(double x)
    {
      Contract.Requires(x > 0);
      Contract.Requires(x <= 1000);

      double y = x + 1;
      double z = x / y;

      Contract.Assert(z > 0);
      Contract.Assert(z <= 1);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 68, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 89, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=50,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=50,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=54,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=54,MethodILOffset=0)]
    static void Test2_OK(double x)
    {
      Contract.Requires(x > 1);
      Contract.Requires(x <= 1000); // We need it to upper bound x, and avoid a warning

      double y = x + 1;
      double z = x / y;             // We get z > 0 as we have a refined evaluation for "x / (x+1)", when x \in (1, 1000]

      Contract.Assert(z > 0);
      Contract.Assert(z <= 1);
    }
  }

  class Test
  {
    double x;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The arguments of the comparison have a compatible precision", PrimaryILOffset = 23, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 25, MethodILOffset = 0)]
    public Test(double x)
    {
      this.x = x;

      Contract.Assert(this.X == (double)x);   // ok because of casting
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The arguments of the comparison have a compatible precision", PrimaryILOffset = 22, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=24,MethodILOffset=0)] // ok, as it will be proven by some other domain
    public Test(double x, bool noCast)    
    {
      this.x = x;

      Contract.Assert(this.X == x);           // ok because we set this.x before
    }

    public double X
    {
      get { return this.x; }
    }
  }
}

namespace tehfusion
{
  class Repro
  {
// F: We know this does not compile
/*
    private static int FirstExampleChecked()
    {
      checked
      {
        return int.MinValue / -1;
      }
    }
*/

    // This compiles, and generates MinValue (because of C# constant propagation semantics)
    // as a consequence there is nothing to check
    [ClousotRegressionTest]
    private static int FirstExample()
    {
      unchecked
      {
        return int.MinValue / -1;
      }
    }
     
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Division by zero ok",PrimaryILOffset=4,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible overflow in division (MinValue / -1)",PrimaryILOffset=4,MethodILOffset=0)]
    private static int SecondExampleChecked(int test)
    {  
      // CLI does not have a div.ovf instruction    
      checked
      {
        return test / -1;
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Division by zero ok",PrimaryILOffset=4,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible overflow in division (MinValue / -1)",PrimaryILOffset=4,MethodILOffset=0)]
    private static int SecondExample(int test)
    {
      unchecked
      {
        // CIL does not contain a checked 
        return test / -1;
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible division by zero",PrimaryILOffset=4,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible overflow in division (MinValue / -1)",PrimaryILOffset=4,MethodILOffset=0)]
    private static int ThirdExample(int test1, int test2)
    {
      unchecked
      {
        return test1 / test2;
      }
    }    
  }
}

namespace Repro
{
  class EmperorXLII
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The arguments of the comparison have a compatible precision",PrimaryILOffset=35,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=40,MethodILOffset=0)]
    // Because we can pass NaN
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation",PrimaryILOffset=57,MethodILOffset=0)] 
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=57,MethodILOffset=0)]
    static double Reciprocal(int value)
    {
      if( value == 0 )
        throw new ArgumentException( ); 
      Contract.EndContractBlock();
      
      Contract.Assert(value != 0.0);
      return 1.0 / value;
    }
  }
}

namespace FactorialExample
{
  public class Example
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=51,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=51,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=60,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible overflow in the arithmetic operation",PrimaryILOffset=60,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=8,MethodILOffset=54)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=25,MethodILOffset=65)]
    public static int Fact_Overflow(int x)
    {
      Contract.Requires(x >= 0);
      Contract.Ensures(Contract.Result<int>() >= 0);
      
      if (x == 0)
	{
	  return 1;
      }
      else
	{
	  checked
	    {
	      var tmp = x - 1;
	      return Fact_Overflow(tmp) * x; // This multiplication can overflow
	    }
	}
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=88,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=88,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=101,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=101,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=125,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=125,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=8,MethodILOffset=91)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=22,MethodILOffset=91)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=39,MethodILOffset=130)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=60,MethodILOffset=130)]
    public static int Fact_Int_NoOverflow(int x)
    {
      Contract.Requires(x >= 0);
      Contract.Requires(x <= 10);
      
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() <= Int32.MaxValue);
      
      if (x == 0)
	{
	  return 1;
	}
      else
	{
	  checked
	    {
	      var tmp = x - 1;
	      var prev = Fact_Int_NoOverflow(tmp); 
	      var res = ((long) prev) * x;
	      
	      if (res >= Int32.MaxValue)
		throw new ArithmeticException();
	      
	      return (int) res;
	    }
	}
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=91,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=91,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=103,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow",PrimaryILOffset=103,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=8,MethodILOffset=94)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=22,MethodILOffset=94)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=40,MethodILOffset=131)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=62,MethodILOffset=131)]
    public static long Fact_NoOverflow(int x)
    {
      Contract.Requires(x >= 0);
      Contract.Requires(x <= 10);
      
      Contract.Ensures(Contract.Result<long>() >= 0);
      Contract.Ensures(Contract.Result<long>() <= Int32.MaxValue);
      
      if (x == 0)
	{
	  return 1;
	}
      else
	{
	  checked
	    {
	      var tmp = x - 1;
	      var prev = Fact_NoOverflow(tmp); 
	      var res = ((long) prev) * x;  // Here we have a problem with types, as Clousot thinks that the "res" is an Int32, so it emits the wrong proof obligation
	      
	      if (res >= Int32.MaxValue)
		throw new ArithmeticException();
	      
	      return (long) res;
	    }
	}
    }

  }
}

namespace Brian
{
  public class Calculator
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No underflow",PrimaryILOffset=63,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible overflow in the arithmetic operation. The static checker determined that the condition '(x + y) <= Int32.MaxValue' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires((x + y) <= Int32.MaxValue);",PrimaryILOffset=63,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=38,MethodILOffset=68)]
    public Int32 Sum(Int32 x, Int32 y)
    {
      Contract.Requires(x >= 0);
      Contract.Requires(y >= 0);
      Contract.Ensures(Contract.Result<Int32>() >= 0);

      if (x == y)
        return x << 1;  // Optimization for 2 * x

      return checked(x + y);
    }

  }
}

namespace StrilancRepro
{
  public abstract class MyStream
  {
    protected MyStream() { }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=55,MethodILOffset=66)]
    public virtual int Read(Byte[] buffer, int offset, int count)
    {
      Contract.Requires(offset >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(count <= (buffer.Length - offset));

      // We were getting an unreached here as there was a bug in the subtraction of floating points (enabled only when -arithmetic is on)

      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }

  }
}

namespace TestCornerCases
{  
  public class MathExamples
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible precision mismatch for the arguments of ==",PrimaryILOffset=49,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=51,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=37,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="Too large floating point result in the arithmetic operation",PrimaryILOffset=37,MethodILOffset=0)]
    public void PositiveInfintySum(bool b)
    {
      var pInfty = Double.PositiveInfinity;
      var z = b? 1.0 : 2.0;
      
      var sum = pInfty + z;
      
      Contract.Assert(sum == Double.PositiveInfinity);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible precision mismatch for the arguments of ==",PrimaryILOffset=49,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=51,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=37,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="Too large floating point result in the arithmetic operation",PrimaryILOffset=37,MethodILOffset=0)]
    public void PositiveInfintySub(bool b)
    {
      var pInfty = Double.PositiveInfinity;
      var z = b ? 1.0 : 2.0;
      
      var sum = pInfty - z;
      
      Contract.Assert(sum == Double.PositiveInfinity);
    }
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible precision mismatch for the arguments of ==",PrimaryILOffset=49,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=51,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="Too small floating point result in the arithmetic operation",PrimaryILOffset=37,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=37,MethodILOffset=0)]
    public void NegativeInfintyAdd(bool b)
    {
      var pInfty = Double.NegativeInfinity;
      var z = b ? 1.0 : 2.0;
      
      var sum = pInfty - z;

      Contract.Assert(sum == Double.NegativeInfinity);
    }
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible precision mismatch for the arguments of ==",PrimaryILOffset=49,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=51,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="Too small floating point result in the arithmetic operation",PrimaryILOffset=37,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=37,MethodILOffset=0)]
    public void NegativeInfintySub(bool b)
    {
      var pInfty = Double.NegativeInfinity;
      var z = b ? 1.0 : 2.0;

      var sum = pInfty - z;
      
      Contract.Assert(sum == Double.NegativeInfinity);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=55,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation",PrimaryILOffset=37,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=37,MethodILOffset=0)]
    public void NaNAdd(bool b)
    {
      var nan = Double.NaN;
      var z = b ? 1.0 : 2.0;

      var sum = nan + z;

      Contract.Assert(nan.Equals(Double.NaN));
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=45,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too small floating point result in the arithmetic operation",PrimaryILOffset=37,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=37,MethodILOffset=0)]
    public void NaNAdd_UsingIsNaN(bool b)
    {
      var nan = Double.NaN;
      var z = b ? 1.0 : 2.0;

      var sum = nan + z;

      Contract.Assert(Double.IsNaN(nan));
    }
  }
}

namespace Comparisons
{
  public class BugRepro
  {
    [ClousotRegressionTest] 
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=24,MethodILOffset=0)]
    public double UseSqrtPos_NoPre(double pos)
    {
      var x = MySqrt(pos);

      Contract.Assert(x >= 0);
      
      return x;
    }


    [ClousotRegressionTest]
   [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=45,MethodILOffset=0)]
     public double UseSqrtPos(double pos)
    {
      Contract.Requires(pos >= 0);

      var x = MySqrt(pos);

      Contract.Assert(x >= 0);

      return x;
    }

    // It seems WPs do not work with Double.IsNaN
    [ClousotRegressionTest("clousot1")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=33,MethodILOffset=0)]
    public double UseSqrtPos_NaN(double pos)
    {
      Contract.Requires(pos < 0);

      var x = MySqrt(pos);

      Contract.Assert(Double.IsNaN(x));

      return x;
    }

    [ContractVerification(false)]
    public double MySqrt(double x)
    {
      Contract.Ensures(!(x >= 0) || Contract.Result<Double>() >= 0);
      Contract.Ensures(!(x < 0) || Double.IsNaN(Contract.Result<Double>()));
      
      return Math.Sqrt(x);
    }  
  }
  
  public class Herman
  {
    // We used to get a proof obligation for floating point precision mismatch here
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="ensures unproven: Contract.Result<float[,]>() != null",PrimaryILOffset=25,MethodILOffset=42)]
    public float[,] F(int i)
    {
      Contract.Requires(i >= 0);
      Contract.Ensures(Contract.Result<float[,]>() != null);

      return new float[i, i];
    }
  }
}

namespace FloatTooBigOrTooSmall
{
  public class BasicExamples
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=62,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=45,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible too large floating point result in the arithmetic operation",PrimaryILOffset=45,MethodILOffset=0)]
    public double Add1(double x, double y)
    {
      Contract.Requires(x >= 0);
      Contract.Requires(y >= 0);
     
      var z = x + y;

      Contract.Assert(z >= 0);
      return z;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=98,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=116,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too small floating point result",PrimaryILOffset=81,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="No too large floating point result",PrimaryILOffset=81,MethodILOffset=0)]
    public double Add(double x, double y)
    {
      Contract.Requires(x >= 0);
      Contract.Requires(y >= 0);
      Contract.Requires(x < Double.MaxValue / 2);
      Contract.Requires(y < Double.MaxValue / 2);

      var z = x + y;

      Contract.Assert(z >= 0);
      Contract.Assert(z < Double.MaxValue);
      return z;
    }
  }
}

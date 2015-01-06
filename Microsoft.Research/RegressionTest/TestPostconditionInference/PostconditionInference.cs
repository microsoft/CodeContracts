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


namespace TestPostconditionInference
{
  class Intervals
  {
    // Should infer result == 20
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    private int Twenty()
    {
      int x = 20;

      return x;
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 12, MethodILOffset = 0)]
    public int Test_Twenty_Explicit()
    {
      int z = Twenty();

      Contract.Assert(z == 20);

      return z;
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Array creation : ok", PrimaryILOffset = 6, MethodILOffset = 0)]
    public int[] Test_Twenty_Implicit()
    {
      return new int[Twenty()];
    }

    // Should infer result >= 0
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    private long Positive(long x)
    {
      if (x > 0)
        return x;
      else
        return 0;
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 16, MethodILOffset = 0)]
    public long Test_Positive(long var)
    {
      long pos_var = Positive(var);

      Contract.Assert(pos_var >= 0);

      return pos_var;
    }
  }

  class UsePostcondition
  {
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    private void Error()
    {
      throw new System.Exception();
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 17, MethodILOffset = 0)]
    public void CheckGreaterThanZero(int val)
    {
      if (val < 0)
      {
        Error();
      }

      Contract.Assert(val >= 0);
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
#if false
    // The assert is false, but we generate a precondition out of it, so we mask it
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = "assert is false", PrimaryILOffset = 14, MethodILOffset = 0)]
#else
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 14, MethodILOffset = 0)]    
#endif    
    public void CheckGreaterThanZero_ShouldFail(int val)
    {
      if (val < 0)
      {
        Error();
      }

      Contract.Assert(val < 0);
    }
  }

  public class Pre
  {
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=4,MethodILOffset=0)]
    private void Char(int[] array, char index)
    {
      // The upper bound is ok, because the proof obligation is pushed to the callers
      array[index] = 12;
    }

    [ClousotRegressionTest("clousot1")]
    // Fails on clousot2
    // [ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 17, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=4,MethodILOffset=3)]  
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=4,MethodILOffset=3)]
    public void TestChar(int[] array, char index)
    {
      // The preconditions are ok as they are pushed to the caller
      Char(array, index);
      // If we reach this point is because array.Length >= 1 and array.Length > index. 
      // Otherwise we'd have thrown an exception before
      Contract.Assert(array.Length >= 1);
      Contract.Assert(array.Length > index);
    }
  }

  public class SymbolicBounds
  {
    // Should infer result <= M && result <= x
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    private static int AtMost(int x, int M)
    {
      if (x < M)
        return x;
      else
        return M;
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 15, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 27, MethodILOffset = 0)]
    public static void Test_AtMost_OK(int x, int M)
    {
      int r = AtMost(x, M);

      Contract.Assert(r <= x);
      Contract.Assert(r <= M);
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 15, MethodILOffset = 0)]
    public static void Test_AtMost_Wrong1(int x, int M)
    {
      int r = AtMost(x, M);

      Contract.Assert(r >= x);
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 15, MethodILOffset = 0)]
    public static void Test_AtMost_Wrong2(int x, int M)
    {
      int r = AtMost(x, M);

      Contract.Assert(r >= M);
    }

    // Should infer -1 <= result < Max
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    private static int Rand(int Max, int k)
    {
      Contract.Requires(Max >= 0);

      for (int i = 0; i < Max; i++)
      {
        if (i == k)
          return i;
      }

      return -1;
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 7, MethodILOffset = 14)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 27, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 36, MethodILOffset = 0)]
    public static void Test_Rand_OK(int Max, int k)
    {
      Contract.Requires(Max >= 0);

      int r = Rand(Max, k);

      Contract.Assert(r >= -1);
      Contract.Assert(r < Max);
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 7, MethodILOffset = 14)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven. Is it an off-by-one? The static checker can prove r >= (0 - 1) instead", PrimaryILOffset = 27, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 36, MethodILOffset = 0)]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=12,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=14,MethodILOffset=0)]
#endif
	public static void Test_Rand_Wrong(int Max, int k)
    {
      Contract.Requires(Max >= 0);

      int r = Rand(Max, k);

      Contract.Assert(r >= 0);
      Contract.Assert(r > Max);
    }
  }

  public class Old
  {
    // Should infer result == Old(x) + 1
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    private int Incr(int x)
    {
      int z = x;
      x = 22;
      return z + 1;
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 14, MethodILOffset = 0)]
    public void Test_Incr(int x)
    {
      int r = Incr(x);

      Contract.Assert(r == x + 1);
    }
  }

  public class Old_Wrong
  {
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    public void Incr_Wrong(int x)
    {
      // We do not want Clousot to infer this postcondition, which is false
      // Contract.Ensures(x == Contract.OldValue<int>(x) + 1);
      x++;
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 13, MethodILOffset = 0)]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=1,MethodILOffset=0)]
#endif
    public void UseIncr()
    {
      int z = 0;
      Incr_Wrong(z);
      Contract.Assert(z == 1);
    }
  }

  public class OutRef
  {
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    static void SetToFifty(out int x)
    {
      x = 50;
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 12, MethodILOffset = 0)]
    public static void TestSetToFifty()
    {
      int z;
      SetToFifty(out z);
      Contract.Assert(z == 50);
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    static void Incr(ref int x)
    {
      x++;
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 15, MethodILOffset = 0)]
    public static void TestIncr_OK()
    {
      int z = 22;

      Incr(ref z);

      Contract.Assert(z == 23);
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 15, MethodILOffset = 0)]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=2,MethodILOffset=0)]
#endif
    public static void TestIncr_Wrong()
    {
      int z = 22;

      Incr(ref z);

      Contract.Assert(z == 22);
    }
  }

  public class Point
  {
    int x, y;

    
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    public Point(int x, int y)
    {
      this.x = x;
      this.y = y;
    }

    int X
    {
      [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
      get
      {
        return this.x;
      }
    }


    int Y
    {
      [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
      get
      {
        return this.y;
      }
    }

    int XPlusY
    {
      [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
      get
      {
        return x + y;
      }
    }

    int SquareDistance
    {
      [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
      get
      {
        return x * x + y * y;
      }
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    void Move(int k)
    {
      x += k;
      y += k;
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 33, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 54, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 69, MethodILOffset = 0)]
    public static void Test_OK()
    {
      Point p = new Point(5, 10);
      Contract.Assert(p.X == 5);

      Contract.Assert(p.SquareDistance == 125);

      p.Move(2);

      Contract.Assert(p.X == 7);
      Contract.Assert(p.Y == 12);
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 33, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 54, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message=@"assert unreachable",PrimaryILOffset=69,MethodILOffset=0)]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=3,MethodILOffset=0)]
#endif
    public static void Test_Wrong()
    {
      Point p = new Point(5, 10);
      Contract.Assert(p.X == 5);

      Contract.Assert(p.SquareDistance == 125);

      p.Move(2);

      Contract.Assert(p.X == 5);
      Contract.Assert(p.Y == 11); // unreached
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 44, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 61, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 78, MethodILOffset = 0)]
    public static void Test(int x, int y)
    {
      Contract.Requires(x >= 0);
      Contract.Requires(y >= 0);

      Point p = new Point(x, y);

      Contract.Assert(p.X >= 0);
      Contract.Assert(p.Y >= 0);

      Contract.Assert(p.SquareDistance >= 0);

    }
  }

  public struct PointStruct
  {
    public int x;
    public int y;

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    public PointStruct(int x, int y)
    {
      this.x = x;
      this.y = y;
    }

    int X
    {
      [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
      get
      {
        return this.x;
      }
    }


    int Y
    {
      [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
      get
      {
        return this.y;
      }
    }

    int XPlusY
    {
      [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
      get
      {
        return x + y;
      }
    }

    int SquareDistance
    {
      [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
      get
      {
        return x * x + y * y;
      }
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    void Move(int k)
    {
      x += k;
      y += k;
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 20, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 59, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 75, MethodILOffset = 0)]
    public static void Test_OK()
    {
      PointStruct p = new PointStruct(5, 10);
      Contract.Assert(p.X == 5);

      Contract.Assert(p.SquareDistance == 125);

      p.Move(2);

      Contract.Assert(p.X == 7);
      Contract.Assert(p.Y == 12);
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 20, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 59, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message=@"assert unreachable",PrimaryILOffset=75,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
    public static void Test_Wrong()
    {
      PointStruct p = new PointStruct(5, 10);
      Contract.Assert(p.X == 5);

      Contract.Assert(p.SquareDistance == 125);

      p.Move(2);

      Contract.Assert(p.X == 5); // False
      Contract.Assert(p.Y == 11); // Unreached
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 46, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 64, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 82, MethodILOffset = 0)]
    public static void Test(int x, int y)
    {
      Contract.Requires(x >= 0);
      Contract.Requires(y >= 0);

      PointStruct p = new PointStruct(x, y);

      Contract.Assert(p.X >= 0);
      Contract.Assert(p.Y >= 0);

      Contract.Assert(p.SquareDistance >= 0);

    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    public static void InferParamInvariant(PointStruct a)
    {
      Contract.Assume(a.X > 0);
      Contract.Assume(a.y > 0);
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
  //  [RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=0,MethodILOffset=10)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: 1 <= a.x",PrimaryILOffset=0,MethodILOffset=10)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: 1 <= a.y",PrimaryILOffset=0,MethodILOffset=10)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 25, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 40, MethodILOffset = 0)]
    public static void TestInferredParameter(int x, int y)
    {
      PointStruct p = new PointStruct(x, y);
      InferParamInvariant(p);

      Contract.Assert(p.X > 0);
      Contract.Assert(p.y > 0);
    }

  }

  public class Virtual
  {
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    virtual public int GetConst()
    {
      return 15;
    }
  }

  public class SubVirtual : Virtual
  {
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    public sealed override int GetConst()
    {
      return -12;
    }
  }

  public class TestVirtual
  {
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 10, MethodILOffset = 0)]
    static public void Test(Virtual v)
    {
      // The warning here is ok, we do not know which instance is passed
      Contract.Assert(v.GetConst() == 15);
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 10, MethodILOffset = 0)]
    static public void Test_Sub(SubVirtual sv)
    {
      // The analysis does not track types
      Contract.Assert(sv.GetConst() == -12);
    }
  }

  public class UnreachableCode_Bug_FromMscorlib
  {
    public bool isReadOnly;

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    private void VerifyWritable()
    {
      if (this.isReadOnly)
      {
        throw new InvalidOperationException();
      }
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Array creation : ok", PrimaryILOffset = 30, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 44, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 44, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 54, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 54, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 15, MethodILOffset = 0)]
#if CLOUSOT2 
 [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=0,MethodILOffset=1)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=1,MethodILOffset=1)]
#endif
    public object Set_CurrencyNegativePattern(int value)
    {
      this.VerifyWritable();

      Contract.Assert(!this.isReadOnly);

      if ((value < 0) || (value > 15))
      {
        object[] tmp = new object[] { 0, 15 };
        return tmp;
      }

      return null;
    }
  }

  public class StaticField
  {
    public static int static_field;

    [ClousotRegressionTest("clousot1")] [ClousotRegressionTest("clousot2")]
    public void F(int k)
    {
      // infer: ensures( static_field == k+1 && static_field >= 1 && static_field <= 100)
      Contract.Requires(k >= 0);
      Contract.Requires(k < 100);

      static_field = k+1;
    }

    [ClousotRegressionTest("clousot1")] [ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 7, MethodILOffset = 2)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 17, MethodILOffset = 2)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 35, MethodILOffset = 0)]
    public void UseFWrong(int p)
    {
      F(p);
      Contract.Assert(static_field >= 1);
      Contract.Assert(static_field <= 10);
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 7, MethodILOffset = 2)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 17, MethodILOffset = 2)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 35, MethodILOffset = 0)]
    public void UseFOk(int p)
    {
      F(p);
      Contract.Assert(static_field >= 1);
      Contract.Assert(static_field <= 100);
    }
  }

  public class PropertyEnsuresInference
  {
    private ExceptionFileTable m_pipe;
    
    public PropertyEnsuresInference(ExceptionFileTable o)
    {
      this.m_pipe = o;
    }

    public bool IsConnected
    {
	  [ClousotRegressionTest]
	  [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=29,MethodILOffset=53)]
	  [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=29,MethodILOffset=55)]
  	 get
      {
        Contract.Ensures(Contract.Result<bool>() == (m_pipe != null && m_pipe.IsConnected)); 

        return m_pipe != null && m_pipe.IsConnected;
      }
    }

    public bool IsConnectedWithInferredContract
    {
      [ClousotRegressionTest]
      get
      {
        return m_pipe != null && m_pipe.IsConnected;
      }
    }

    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=32,MethodILOffset=0)]
	public void Test(PropertyEnsuresInference p)
    {
      Contract.Requires(p != null);

      if(p.IsConnected)
      {  // Use the contract
        Contract.Assert(p.m_pipe != null);
      }
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=32,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=48,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven. The static checker determined that the condition 'p.m_pipe.RandomBit' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add an explicit assumption at entry to document it: Contract.Assume(p.m_pipe.RandomBit);",PrimaryILOffset=64,MethodILOffset=0)]
    public void TestWithInferredContract(PropertyEnsuresInference p)
    {
      Contract.Requires(p != null);

      if(p.IsConnectedWithInferredContract)
      {
        Contract.Assert(p.m_pipe != null);
        Contract.Assert(p.m_pipe.IsConnected);
		Contract.Assert(p.m_pipe.RandomBit); // Should not prove this one
	  }
    }
  }

  public class ExceptionFileTable
  {
    public ExceptionFileTable left, right;

    public bool IsConnected;
    public bool RandomBit;
  }
}

namespace ExtractedFromMscorlib
{
  public class AuthorizationRule
  {
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    protected internal AuthorizationRule(object identity, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      if ((inheritanceFlags < InheritanceFlags.None)
        || (inheritanceFlags > (InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit)))
      {
        throw new ArgumentOutOfRangeException();
      }
      if ((propagationFlags < PropagationFlags.None)
        || (propagationFlags > (PropagationFlags.InheritOnly | PropagationFlags.NoPropagateInherit)))
      {
        throw new ArgumentOutOfRangeException();
      }

    }

    [Flags]
    public enum InheritanceFlags
    {
      None,
      ContainerInherit,
      ObjectInherit
    }

    [Flags]
    public enum PropagationFlags
    {
      None,
      NoPropagateInherit,
      InheritOnly
    }

  }

  public class AccessRule : AuthorizationRule
  {
    public object tmp;

    [ClousotRegressionTest("clousot1"),ClousotRegressionTest("clousot2")]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=0,MethodILOffset=6)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=1,MethodILOffset=6)]
#endif
    [RegressionOutcome(Outcome = ProofOutcome.Bottom, Message = @"This array creation is never executed", PrimaryILOffset = 21, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Bottom, Message = @"This array creation is never executed", PrimaryILOffset = 49, MethodILOffset = 0)]
    protected AccessRule(object identity, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
      : base(identity, isInherited, inheritanceFlags, propagationFlags)
    {
      if ((inheritanceFlags < InheritanceFlags.None)
        || (inheritanceFlags > (InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit)))
      {
        tmp = new object[1];
        throw new ArgumentOutOfRangeException();
      }
      if ((propagationFlags < PropagationFlags.None)
        || (propagationFlags > (PropagationFlags.InheritOnly | PropagationFlags.NoPropagateInherit)))
      {
        tmp = new object[1];
        throw new ArgumentOutOfRangeException();
      }
    }
  }

#if false
  public class AccessRule_ForClousot2 : AuthorizationRule
  {
    public object tmp;

    [ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 0, MethodILOffset = 6)]
    protected AccessRule_ForClousot2(object identity, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
      : base(identity, isInherited, inheritanceFlags, propagationFlags)
    {
      if ((inheritanceFlags < InheritanceFlags.None)
        || (inheritanceFlags > (InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit)))
      {
        tmp = new object[1];
        throw new ArgumentOutOfRangeException();
      }
      if ((propagationFlags < PropagationFlags.None)
        || (propagationFlags > (PropagationFlags.InheritOnly | PropagationFlags.NoPropagateInherit)))
      {
        tmp = new object[1];
        throw new ArgumentOutOfRangeException();
      }
    }
  }
#endif

  public class DuplicatedPreconditions
  {
    [ClousotRegressionTest]
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    private int F(int x)
    {
      // We want to avoid having also x >= 4
      Contract.Requires(x > 3);

      return x + 3;
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=4,MethodILOffset=3)]
    public int CallF()
    {
      // Should have just one pre-condition
      return F(15);
    }
  }
}

namespace Optimizations
{
  class RedundantPostcondition
  {
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2"), RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 23, MethodILOffset = 29)]
    public int TwoPosts_Easy(int x)
    {
      Contract.Requires(x >= 0);
      Contract.Ensures(Contract.Result<int>() >= 0);

      // Should not infer *again* Contract.Ensures(Contract.Result<int>() >= 0);
      return x;
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2"), RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 7, MethodILOffset = 14), RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 24, MethodILOffset = 0)]
    public void Test_TwoPosts_Easy(int x)
    {
      Contract.Requires(x >= 0);

      int z = TwoPosts_Easy(x);

      Contract.Assert(z == x);
    }
  }

  class RedundantPostcondition2
  {
    private bool b;

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2"), RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 17, MethodILOffset = 30)]
    public int TwoPosts_Easy(int x)
    {
      Contract.Requires(x > 0);
      Contract.Ensures(Contract.Result<int>() > 0);

      this.b = true;

      // Should not infer *again* Contract.Ensures(Contract.Result<int>() >= 0);
      return x;
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2"), RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 4, MethodILOffset = 11), RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 23, MethodILOffset = 0)]
    public void Test_TwoPosts(int x)
    {
      Contract.Requires(x > 0);

      int z = TwoPosts_Easy(x);

      Contract.Assert(this.b == true);

    }
  }

  public class Rational
  {
    //public int Numerator { get; protected set; }
    public int Denominator { get; protected set; }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2"), RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 25)]
    public Rational(int n, int d)
    {
      Contract.Requires(d != 0);

      //this.Numerator = n;
      this.Denominator = d;
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [ContractInvariantMethod]
    private void RationalInvariant()
    {
      Contract.Invariant(Denominator != 0);
    }
  }


  class RedundantPostcondition4
  {
    private bool b;

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2"), RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 17, MethodILOffset = 34)]
    public int TwoPosts_Easy(int x)
    {
      Contract.Requires(x > 0);
      Contract.Ensures(Contract.Result<int>() > 0);

      this.b = x > 22;

      // Should not infer *again* Contract.Ensures(Contract.Result<int>() >= 0);
      return x;
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2"), RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 4, MethodILOffset = 11), RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 30, MethodILOffset = 0)]
    public void Test_TwoPosts(int x)
    {
      Contract.Requires(x > 0);

      int z = TwoPosts_Easy(x);

      Contract.Assert(this.b == x > 22);

    }
  }
}

namespace ConstantEnsureInference
{
  [ContractClass(typeof(IVectorContracts))]
  interface IVector
  {
    bool IsVector { get; }

    int Rank { get; }
  }

  [ContractClassFor(typeof(IVector))]
  abstract class IVectorContracts : IVector
  {
    public bool IsVector
    {
      get
      {
        Contract.Ensures(!Contract.Result<bool>() || this.Rank == 1);

        return default(bool);
      }
    }

    public int Rank
    {
      get
      {
        throw new Exception();
      }
    }
  }

  public class Vector : IVector
  {
    public int Rank
    {
      [ClousotRegressionTest]
      get
      {
        // For some reason we are not attaching the inferred postcondition to the IsVector below
        //Contract.Ensures(Contract.Result<int>() == 1);
        return 1;
      }
    }

    public bool IsVector
    {
     [ClousotRegressionTest]
     [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=19,MethodILOffset=1)]
      get 
      {
        return true; 
      }
    }
  }
  
  public class ScottRepro
  {
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=50,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=50,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Array creation : ok",PrimaryILOffset=86,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=101,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=101,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=111,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=111,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=121,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=121,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Array creation : ok",PrimaryILOffset=246,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=263,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=263,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Array creation : ok",PrimaryILOffset=333,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=350,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=350,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=361,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=361,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Array creation : ok",PrimaryILOffset=431,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=448,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=448,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=459,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=459,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=470,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=470,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Array creation : ok",PrimaryILOffset=510,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=528,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=528,MethodILOffset=0)]
    public static object PingMethodPost(dynamic state, dynamic[] args)
    {
/* Should *NOT* infer those two   
   Contract.Ensures(ActorStressTests.PingPongActor.< PingMethodPost > o__SiteContainer0.<> p__Site1.Target != null);
                  Contract.Ensures(ActorStressTests.PingPongActor.< PingMethodPost > o__SiteContainer0.<> p__Site2.Target != null);
*/
      int remainingCount = args[0];
      state.Publish("Status", remainingCount - 1);
      if (remainingCount > 0)
      {
        string otherActorName = state.otherActorName;
        dynamic proxy = state.GetActorProxy(otherActorName);
        proxy.Command("PingMethodPost", new object[] { remainingCount - 1 });
      }
      return null;
    }
  }
}

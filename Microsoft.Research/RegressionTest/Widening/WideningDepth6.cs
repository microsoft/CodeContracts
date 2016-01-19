using System;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace WideningDepth
{
  public class WideningDepth
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=33,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven. Is it an off-by-one? The static checker can prove (0 - 1) < i instead",PrimaryILOffset=42,MethodILOffset=0)]
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
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven. Is it an off-by-one? The static checker can prove (0 - 1) < i instead",PrimaryILOffset=37,MethodILOffset=0)]
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
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven. Is it an off-by-one? The static checker can prove (0 - 1) < c instead",PrimaryILOffset=36,MethodILOffset=0)]
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

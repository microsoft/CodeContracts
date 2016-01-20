using System;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace WideningDepth
{
  public class WideningDepth
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=18,MethodILOffset=0)]
    public void Test0()
    {
      int c = Test5();
      Contract.Assert(c <= 585);
    }

    public static bool NondetBool()
    {
      return new Random().Next(2) == 0;
    }

    [ClousotRegressionTest]
    public int Test5()
    {
      int c = 0;
      while (NondetBool()) {
        if (c < 7) { c++; }
      }
      return c;
    }
  }
}

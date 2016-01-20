using System;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace MaxSteps
{
  public class MaxSteps
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="assert unreachable",PrimaryILOffset=13,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="assert unreachable",PrimaryILOffset=29,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="assert unreachable",PrimaryILOffset=45,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="assert unreachable",PrimaryILOffset=61,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="assert unreachable",PrimaryILOffset=77,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="assert unreachable",PrimaryILOffset=93,MethodILOffset=0)]
    public void Test0(int x, bool b)
    {
      int c = 0;
      if (b) { c++; }
      Contract.Assert(0 < x);
      if (b) { c++; }
      Contract.Assert(1 < x);
      if (b) { c++; }
      Contract.Assert(2 < x);
      if (b) { c++; }
      Contract.Assert(3 < x);
      if (b) { c++; }
      Contract.Assert(4 < x);
      if (b) { c++; }
      Contract.Assert(5 < x);
    }
  }
}

using System;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace MaxFieldReads
{
  public class MaxFieldReads
  {
    int f;
    static int g;
    int h { get; set; }
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="assert unreachable",PrimaryILOffset=32,MethodILOffset=0)]
    public void Test0()
    {
      int tmp = f;
      for (int i = 0; i < 10; i++) { tmp++; }
      Contract.Assert(tmp != 42);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="assert unreachable",PrimaryILOffset=31,MethodILOffset=0)]
    public void Test1()
    {
      int tmp = g;
      for (int i = 0; i < 10; i++) { tmp++; }
      Contract.Assert(tmp != 42);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="assert unreachable",PrimaryILOffset=32,MethodILOffset=0)]
    public void Test2()
    {
      int tmp = h;
      for (int i = 0; i < 10; i++) { tmp++; }
      Contract.Assert(tmp != 42);
    }
  }
}

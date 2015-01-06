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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;


namespace PreInference
{

  // F: this regression test will give some extra warnings in the console. This is ok, as those come from the re-analysis of the methods to check that the preconditions are also sufficient

  public class CheckInferredPreconditionsAreNecessary
  {
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(s != null);")]
    [RegressionOutcome("Inferred preconditions are sufficient too")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 's' (Fixing this warning may solve one additional issue in the code)",PrimaryILOffset=5,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 's'",PrimaryILOffset=12,MethodILOffset=0)]    
    public int NecessaryPrecondition(int x, string s)
    {
      if (x > 0)
        return s.Length;
      else
        return s.GetHashCode();
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Inferred preconditions are sufficient too")]
    [RegressionOutcome("Contract.Requires(s != null);")]
    [RegressionOutcome("Contract.Requires(t != null);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 's'",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 't'",PrimaryILOffset=8,MethodILOffset=0)]
    public int NecessaryPreconditions(string s, string t)
    {
      var v1 = s.Length;
      var v2 = t.Length;

      return v1 + v2;
    }

    [ClousotRegressionTest]
    [RegressionOutcome("!!! Inferred preconditions are not sufficient: 2 assertion(s) left")]    
    [RegressionOutcome("Contract.Requires(arr != null);")]
    [RegressionOutcome("Contract.Requires(0 < arr.Length);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 'arr'",PrimaryILOffset=8,MethodILOffset=0)]   
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=8,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=8,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=8,MethodILOffset=0)]
    public int NotNecessaryPrecondition(int[] arr)
    {
      int i = 0;
      while (arr[i++] > 0)
        ;
      return i;
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Inferred preconditions are sufficient too")]
#if CLOUSOT2
    [RegressionOutcome("Contract.Requires((o == null || x > 10));")]
#else
    [RegressionOutcome("Contract.Requires((!(o) || x > 10));")]
#endif
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=8,MethodILOffset=0)]
    public void Disjunctive(int x, object o)
    {
      if (o != null)
      {
        Contract.Assert(x > 10);
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Inferred preconditions are sufficient too")]
    [RegressionOutcome("Contract.Requires((x <= 12 || o != null));")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=12,MethodILOffset=0)]
    public void Disjunctive(object o, int x)
    {
      if (x > 12)
      {
        Contract.Assert(o != null);
      }
    }

  }
}

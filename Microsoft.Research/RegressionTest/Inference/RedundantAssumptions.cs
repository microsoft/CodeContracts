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
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;


namespace ExamplesRedundantAssumption
{
  public class Assume
  {

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(obj != null);
    }

    public object obj;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=20,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=12,MethodILOffset=25)]

    public Assume(object x)
    {
      // We had a bug here trying to suggest a precondition for an assumption coming from invariants
      Contract.Requires(x != null);
      obj = x;
    }
  }
  
  public class BugRepro
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=7,MethodILOffset=6)]
    void F1()
    {
        F2(new List<object>());
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=13,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=43,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=23,MethodILOffset=0)]
    void F2(List<object> list)
    {
        Contract.Requires(list != null);

        foreach (var item in list)
        {
            Contract.Assume(item != null);
        }
    }
  }
}
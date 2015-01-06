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
using System.Diagnostics.CodeAnalysis;
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;
using System.Collections.Generic;

namespace TestWarningCommonRoots
{
 public class F
  {
    public string s;
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=11,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 'this.s' (Fixing this warning may solve one additional issue in the code)",PrimaryILOffset=16,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=26,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 'this.s'",PrimaryILOffset=33,MethodILOffset=0)]
    public int  SuppressOneWarning(bool b)
    {
      if (b)
      {
        return s.Length;
      }
      else
      {
        return s.IndexOf('a');
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=11,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 'this.s'",PrimaryILOffset=16,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=27,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=32,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: s != null",PrimaryILOffset=8,MethodILOffset=32)]
    public int DoNotSuppress(bool b)
    {
      if (b)
      {
        return s.Length;
      }
      else
      {
        return Dummy(s);
      }
    }

    public int Dummy(string s)
    {
      Contract.Requires(s != null);

      return s.Length;
    }

    public string DummyNoPost(int foo)
    {
      return foo.ToString();
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=3,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 'res' (Fixing this warning may solve 3 additional issues in the code)",PrimaryILOffset=68,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 'res'",PrimaryILOffset=59,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 'res'",PrimaryILOffset=50,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 'res'",PrimaryILOffset=36,MethodILOffset=0)]
    public object ShouldSuppress(int foo)
    {
      var res = DummyNoPost(foo);
      switch (foo)
      {
        case 0:
          return res.Length;

        case 1:
          return res.ToString();

        case 2:
          return res.ToUpper();

        case 3:
          return res.GetHashCode();

        default:
          return res.Equals(foo);
      }
    }
  }
}
  
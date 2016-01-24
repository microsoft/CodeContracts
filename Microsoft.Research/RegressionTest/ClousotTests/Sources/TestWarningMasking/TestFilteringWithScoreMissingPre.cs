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
#define CODE_ANALYSIS // For the SuppressMessage attribute

// The regression defines 4 cases as a compiler switch: LOW, MEDIUMLOW, MEDIUM, FULL
// We expect to see less messages with LOW, more with MEDIUMLOW, MEDIUM and all with FULL
// This file is to test the behavior

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;
using System.Collections.Generic;

#pragma warning disable 0649
namespace Always
{
  public class Show
  {
	[ClousotRegressionTest]
	// Always show	
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(x > 0); for parameter validation",PrimaryILOffset=0,MethodILOffset=0)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(x > 0); for parameter validation",PrimaryILOffset=2,MethodILOffset=0)]
#endif

#if FULL
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=4,MethodILOffset=0)]
#endif
    static public int WeWantToAlwaysShowIt(int x)
    {
      Contract.Assert(x > 0);

      return x - 1;
    }

	string s;
	[ClousotRegressionTest]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(!string.IsNullOrEmpty(s)); for parameter validation",PrimaryILOffset=0,MethodILOffset=0)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(!string.IsNullOrEmpty(s)); for parameter validation",PrimaryILOffset=1,MethodILOffset=0)]
#endif	
#if FULL
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven. Are you making some assumption on get_Length that the static checker is unaware of? ",PrimaryILOffset=9,MethodILOffset=0)]	
#endif
	public void UpdateTheString(string s)
    {
      Contract.Assert(!string.IsNullOrEmpty(s));
      this.s = s;
    }
  }
}

namespace Others
{
  public class InferDisjunction
  {
    public string s;
    public int x;

    public InferDisjunction(string s, int x)
    {
      this.s = s;
      this.x = x;
    }

    [ClousotRegressionTest]
#if FULL
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'this.s'. The static checker determined that the condition 'this.s != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(this.s != null);",PrimaryILOffset=16,MethodILOffset=0)]
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires((this.x <= 10 || this.s != null)); for parameter validation",PrimaryILOffset=0,MethodILOffset=0)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires((this.x <= 10 || this.s != null)); for parameter validation",PrimaryILOffset=1,MethodILOffset=0)]
	#endif
#endif
    public int SomethingToInferWithDisjunction()
    {
      if (x > 10)
      {
        return s.Length + 111;
      }

      return -1;
    }
  }
}


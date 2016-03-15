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

using System;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace Test
{ 
  public class MissingPublicSurfaceValidationAsWarning
  {
	[ClousotRegressionTest]
    public void CallPublicWithValidation(string[] s)
    {
      if (s == null || s.Length == 0)
        return;
      Call1(s);
    }

	[ClousotRegressionTest]
	// [RegressionOutcome("Contract.Requires(0 < s.Length);")]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(0 < s.Length); for parameter validation",PrimaryILOffset=0,MethodILOffset=0)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(0 < s.Length); for parameter validation. Otherwise the following sequence of method calls may cause an error. Sequence: CallPublic -> Call1 -> Call2 -> Call3",PrimaryILOffset=2,MethodILOffset=0)]
#endif
    public void CallPublic(string[] s)
    {
      Call1(s);
    }

	[ClousotRegressionTest]
    private void Call3(string[] s)
    {
      var el = s[0];
      Console.WriteLine(el);
    }

	[ClousotRegressionTest]
    private void Call2(string[] s)
    {
      Call3(s);
    }

	[ClousotRegressionTest]
    private void Call1(string[] s)
    {
      Call2(s);
    }
  }
}
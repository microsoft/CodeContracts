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


namespace RecursiveCall
{
  public class Test
  {
	[ClousotRegressionTest]
#if SLICING 
	// nothing: we inferred the postcondition for N
//#elif CLOUSOT2
	// nothing: we inferred the postcondition for N
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=23,MethodILOffset=0)]
#endif
    public int M(int x)
    {
      if (x < 0)
        return 0;
      var tmp = N(x - 1);

      Contract.Assert(tmp >= 0);

      return tmp;
    }

	[ClousotRegressionTest]
#if SLICING
	//  nothing: we inferred the postcondition for M
#else
	// nothing 
#endif
    public int N(int x)
    {
      if (x < 0)
        return 0;
      var tmp = M(x - 1);

      Contract.Assert(tmp >= 0);

      return tmp;
    }
  }
}

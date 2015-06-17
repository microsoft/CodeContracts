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

namespace ObjectInvariantInference
{
  public class MainWindow
  {
    public string f = null; // using string just to make it easy

	[ClousotRegressionTest]
    public void e1()
    {
      this.f = null;
    }

	[ClousotRegressionTest]
    public void e2()
    {
      this.f = "Hello!";

      // requires this.f != null
      var d = new Dialog(this);
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(this.f != null); for parameter validation. Otherwise the following sequence of method calls may cause an error. Sequence: e2Bis -> .ctor -> e3",PrimaryILOffset=1,MethodILOffset=0)]
    public void e2Bis()
    {
      // this.f can be null

      // requires this.f != null
      var d = new Dialog(this);

    }
  }

  public class Dialog
  {
    readonly MainWindow parent;
    readonly string f;

	[ClousotRegressionTest]
	[RegressionReanalysisCount(1)] // Reanalyze the method once
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(parent.f != null); for parameter validation. Otherwise invoking e3 (in the same type) may cause an error",PrimaryILOffset=1,MethodILOffset=0)]
    public Dialog(MainWindow parent)
    {
      this.parent = parent;
      this.f = parent.f;
    }

	[ClousotRegressionTest]
    public void e3()
    {
      Contract.Assert(this.f != null);
    }
  }
}
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
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;

namespace Basic
{
  public class juaprebo
  {
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.String>() != null);")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.String>() == @\"SomeString\");")]
    public static string CCEnsures()
    {
      Contract.Ensures(Contract.Result<string>() != string.Empty);

      return "SomeString";
    }
  }
  
  public class ContainsTests
  {
    public void RequiresNoA(string s)
    {
      Contract.Requires(s != null);
      Contract.Requires(!s.Contains("a"));

      return;
    }

    [ClousotRegressionTest]
    public void CallOK()
    {
      RequiresNoA("bcde");
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="requires is false: !s.Contains(\"a\")",PrimaryILOffset=26,MethodILOffset=6)]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking method 'CallNotOK' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking method 'CallNotOK' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=6,MethodILOffset=0)]
#endif
    public void CallNotOK()
    {
      RequiresNoA("a");
    }
  }

}

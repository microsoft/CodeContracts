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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts.Regression;

namespace PostMSI
{
 

  public class TestInheritedInterface : ITestInheritedClosure, IFoo
  {
    #region IFoo Members

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = "ensures is false: Contract.Result<string>() != null", PrimaryILOffset = 35, MethodILOffset = 14)]
    public string Get(string s)
    {
      if (s == "Foo") return null;
      return s;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = "ensures is false: Contract.Result<byte[]>() != null", PrimaryILOffset = 30, MethodILOffset = 39)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "ensures unproven: Contract.ForAll(Contract.Result<byte[]>(), n => @this.IsZero(n))", PrimaryILOffset = 63, MethodILOffset = 37)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "ensures unproven: Contract.ForAll(Contract.Result<byte[]>(), n => @this.IsZero(n))", PrimaryILOffset = 63, MethodILOffset = 24)]
    public byte[] Test(int x)
    {
      switch (x)
      {
        case 0:
          return new byte[5];
        case 1:
          return new byte[] { 0, 0, 0, 0, 1 };

        default: return null;
      }
    }

    #endregion

    #region ITestInheritedClosure Members


    public bool IsZero(int arg)
    {
      return arg == 0;
    }

    #endregion

    #region ITestInheritedClosure Members


    public ITestInheritedClosure[] Prop
    {
      get
      {
        return new ITestInheritedClosure[4];
      }
    }

    public bool IsInterface
    {
      get { return true; }
    }

    #endregion
  }
}

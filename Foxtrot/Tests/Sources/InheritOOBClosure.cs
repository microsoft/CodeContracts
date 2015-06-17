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
using System.Text;
using System.Diagnostics.Contracts;

namespace Tests.Sources
{
 public class TestInheritedInterface : ITestInheritedClosure//, IFoo
  {

    public string Get(string s)
    {
      if (s == "Foo") return null;
      return s;
    }

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

  partial class TestMain
  {

    partial void Run()
    {
      var st = new TestInheritedInterface();
      if (behave)
      {
        st.Test(0);
      }
      else
      {
        st.Test(1);
      }
    }
    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Postcondition;
    public string NegativeExpectedCondition = "Contract.ForAll(Contract.Result<byte[]>(), n => @this.IsZero(n))";
    
  }
}

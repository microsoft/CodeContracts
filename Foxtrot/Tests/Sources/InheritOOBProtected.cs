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
#if LOCALBASE
  public abstract class Base
  {
    public class Inner
    {
      protected virtual int InnerBar3(int x)
      {
        Contract.Requires(x >= 0, "x must be non-negative");
        return 0;
      }
    }

    protected internal virtual int Bar3(int x)
    {
      Contract.Requires(x >= 0, "x must be non-negative");
      return 0;
    }

    protected Inner myInner = new Inner();
  }

#endif

  public class TestProtectedCallSiteRequires : Base
  {
    protected internal override int Bar3(int x)
    {
      return base.Bar3(x);
    }

    public void CallBarVirtually(int x)
    {
      this.Bar3(x);
    }

    public class DerivedInner : Base.Inner
    {

      public void CallBarVirtually()
      {
        this.InnerBar3(0);
      }
    }
  }

  partial class TestMain
  {

    partial void Run()
    {
      var st = new TestProtectedCallSiteRequires();
      if (behave)
      {
        st.CallBarVirtually(0);
        var inner = new TestProtectedCallSiteRequires.DerivedInner();
        inner.CallBarVirtually();
      }
      else
      {
        st.CallBarVirtually(-1);
      }
    }
    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "x >= 0";

  }
}

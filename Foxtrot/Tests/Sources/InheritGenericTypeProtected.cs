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
  public abstract class Base<Q>
  {
    public class Inner<R>
    {
      protected virtual int InnerBar3<S>(S s, R r, Q q)
      {
        Contract.Requires(s != null);
        Contract.Requires(r != null);
        Contract.Requires(q != null);
        return 0;
      }
    }

    protected internal virtual int Bar3<S>(S s, Q q)
    {
      Contract.Requires(s != null);
      Contract.Requires(q != null);
      return 0;
    }

  }

#endif

  public class TestProtectedCallSiteRequires<Q> : Base<Q>
  {
    protected internal override int Bar3<S>(S s, Q q)
    {
      return base.Bar3(s, q);
    }

    public void CallBarVirtually<S>(S s, Q q)
    {
      this.Bar3(s, q);
      this.Bar3(5, q);
    }

    public class DerivedInner<T> : Base<T>.Inner<string>
    {

      public void CallBarVirtually<S>(S s, string r, T t)
      {
        this.InnerBar3(s, r, t);
        this.InnerBar3(5, r, t);
      }
    }
  }

  partial class TestMain
  {

    partial void Run()
    {
      var st = new TestProtectedCallSiteRequires<int>();
      if (behave)
      {
        st.CallBarVirtually(1.5, 0);
        var inner = new TestProtectedCallSiteRequires<object>.DerivedInner<IDictionary<string,string>>();
        inner.CallBarVirtually<float>((float)1.5, "Hello", new Dictionary<string,string>());
      }
      else
      {
        st.CallBarVirtually((string)null, 0);
      }
    }
    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "s != null";

  }
}

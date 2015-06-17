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
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using Strilanc;

namespace Tests.Sources
{
  partial class TestMain {

    partial void Run()
    {
      var md = new C();
      if (this.behave) return;
      // doesn't really have a negative case, so fake it.
      Contract.Assume(false); 
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Assume;
    public string NegativeExpectedCondition = "false";
  }

  /// <summary>
  /// Testing call-site requires inheritance to see if we copy OOB correctly
  /// </summary>
  public class C {
    public object F(ICheckCopyingOOB<object> data) {
      var value = data[0];
      return new X(() => ValueToString(value));
    }
    protected virtual string ValueToString(object value) {
      return "0";
    }
  }

  public sealed class X {
    public X(Func<string> valueDescription) {}
  }


}


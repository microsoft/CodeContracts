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
  abstract class BaseClass<T>
  {
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(!this.IsDefault(this.AbstractProperty));
    }

    public abstract T AbstractProperty { get; }

    [Pure]
    public abstract bool IsDefault(T t);
  }

  class DerivedClass : BaseClass<bool>
  {
    bool finishedConstruction;

    public DerivedClass(bool behave)
    {
      finishedConstruction = behave;
    }

    public override bool AbstractProperty
    {
      get { return finishedConstruction; }
    }

    public override bool IsDefault(bool value) {
      return value == false;
    }
  }

  partial class TestMain
  {
    partial void Run() {
      var d = new DerivedClass(behave);
      var e = d;
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Invariant;
    public string NegativeExpectedCondition = "!this.IsDefault(this.AbstractProperty)";
  }
}

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
  class Base<A,B>
    where A:class
    where B:class
  {
    private A x;
    private B[] y = new B[5];

    public virtual A Test(A d)
    {
      Contract.Ensures(Contract.Result<A>() == d);
      Contract.Ensures(Contract.ForAll(0, 5, i => this.y[i] == null));
      Contract.Ensures(Contract.ForAll(0, 5, i => this.y[i] == null || d != null));
      Contract.Ensures(Contract.ForAll(0, 5, i => Contract.Exists(0, 5, j => this.y[i] == this.y[j] || d != null)));
      Contract.Ensures(this.x == null);
      this.x = d;
      return d;
    }
  }

  partial class TestMain : Base<string,TestMain>
  {
    public override string Test(string d) {
      return base.Test(d);
    }

    partial void Run()
    {
      if (behave) this.Test(null);
      else this.Test("");
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Postcondition;
    public string NegativeExpectedCondition = "this.x == null";
  }
}

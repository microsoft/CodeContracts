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
  class ClassWithExceptionCtor 
  {
    bool initialized;

    [ContractInvariantMethod]
    void Invariant() {
      Contract.Invariant(this.initialized);
    }

    public ClassWithExceptionCtor(bool behave)
    {
      Contract.EnsuresOnThrow<NotImplementedException>(true);

      if (behave) {
        throw new NotImplementedException();
      }

      this.initialized = behave;
    }

  }

  partial class TestMain
  {
    partial void Run() {
      try {
        var d = new ClassWithExceptionCtor(behave);
      }
      catch (NotImplementedException) {

      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Invariant;
    public string NegativeExpectedCondition = "this.initialized";
  }
}

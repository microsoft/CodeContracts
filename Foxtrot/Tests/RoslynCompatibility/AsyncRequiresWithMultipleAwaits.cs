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
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Sources
{
    public class Foo
    {
        public async Task Method1(string str)
        {
            Contract.Requires(str != null);
            await Task.Delay(42);
        }

        public async Task Method2(string str)
        {
            // This code lead to failure previously!
            Contract.Requires(str != null);

            await Task.Delay(42);
            await Task.Delay(43);
        }

        public async Task Method5(string str)
        {
            Contract.Requires(str != null);
            await Task.Delay(42);
            await Task.Delay(42);
            await Task.Delay(42);
            await Task.Delay(42);
            await Task.Delay(42);
        }
    }

  partial class TestMain
  {
    partial void Run()
    {
      if (behave)
      {
        new Foo().Method2("1");
      }
      else
      {
        new Foo().Method2(null);
      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "str != null";
  }
}

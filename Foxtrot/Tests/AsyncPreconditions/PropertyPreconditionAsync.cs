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
using System.Threading.Tasks;

namespace Tests.Sources
{
    struct Foo
    {
        public int Limit { get; set; }

        public Foo(int limit)
            : this()
        {
            Limit = 42;
        }
        public async Task<int> BarAsync()
        {
            Contract.Requires(Limit == 42);

            await Task.Delay(42);
            return 42;
        }

        public Task<int> BarTask()
        {
            Contract.Requires(Limit == 42);

            Task.Delay(42).Wait();
            return Task.FromResult(42);
        }

        public int Bar()
        {
            Contract.Requires(Limit == 42);

            Task.Delay(42);
            return 42;
        }
    }

    partial class TestMain
    {
        public void Test(int arg)
        {
            Contract.Requires(arg != 42);
        }

        partial void Run()
        {
            if (!behave)
            {
                Foo foo = new Foo(42);
                this.Test(foo.BarAsync().Result);
            }
        }

        public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
        public string NegativeExpectedCondition = "arg != 42";
    }
}

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
using System.Threading;

namespace Tests.Sources
{
    using System.Threading.Tasks;

    class Foo
    {
        private static int AssertPostconditionIsSynchronous(int result)
        {
            var currentThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;

            // This check is not 100% buletproof (see this blog post for some corner cases - http://blogs.msdn.com/b/pfxteam/archive/2012/02/07/10265067.aspx)
            // But it should be robust enough.
            if (fooAsyncThreadId != currentThreadId)
            {
                string message =
                    string.Format(
                        "Implicit continuation should be synchronous. FooAsync TID = {0}, PostCheck TI = {1}",
                        fooAsyncThreadId, currentThreadId);

                throw new System.Exception(message);
            }

            return result;
        }

        private static int fooAsyncThreadId;

        public async Task<int> FooAsync()
        {
            Contract.Ensures(
                AssertPostconditionIsSynchronous(
                    Contract.Result<int>()) == 42);

            await Task.Delay(42);

            // Capturing managed thread id before existing.
            fooAsyncThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            return 42;
        }
    }

    partial class TestMain
    {
        partial void Run()
        {
            if (behave)
            {
                new Foo().FooAsync().Wait();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
        public string NegativeExpectedCondition = "Value cannot be null.";
    }
}

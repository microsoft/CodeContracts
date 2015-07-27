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
    class Foo
    {
        // Check for the #172: ccrewrite fails to check following method
        // compiled by the Roslyn compiler
        public async Task FooAsync()
        {
            // ccrewrite fail only on methods with complex body statements
            // but only on methods without preconditions.
            if (System.DateTime.Now.Day == 0 && 1 == 2)
            { }
        }
        
        // This one is a sanity check!
        public async Task FooAsync(string str)
        {
            Contract.Requires(str != null);

            if (System.DateTime.Now.Day == 0 && 1 == 2)
            { }
        }
    }
 
    partial class TestMain
    {
        partial void Run()
        {
            new Foo().FooAsync().Wait();
            if (behave)
            {
                new Foo().FooAsync("foo").Wait();
            }
            else
            {
                new Foo().FooAsync(null).GetAwaiter().GetResult();
            }
        }

        public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
        public string NegativeExpectedCondition = "str != null";
    }
}

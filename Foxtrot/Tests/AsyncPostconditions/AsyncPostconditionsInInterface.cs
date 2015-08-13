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

    [ContractClass(typeof (IFooContract))]
    interface IFoo
    {
        Task<int> FooAsync(int result);
    }

    [ContractClassFor(typeof(IFoo))]
    abstract class IFooContract : IFoo
    {
        Task<int> IFoo.FooAsync(int result)
        {
            Contract.Ensures(Contract.Result<int>() == 42, "Should be 42!");
            throw new NotImplementedException();
        }
    }


    class Foo : IFoo
    {
        public async Task<int> FooAsync(int result)
        {
            await Task.Delay(42);
            return result;
        }
    }

    partial class TestMain
    {
        partial void Run()
        {
            if (behave)
            {
                // Should not fail!
                var r = new Foo().FooAsync(42).GetAwaiter().GetResult();
            }
            else
            {
                // This one should fail with postcondition violation
                var r = new Foo().FooAsync(43).GetAwaiter().GetResult();
            }
        }

        public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Postcondition;
        public string NegativeExpectedCondition = "Contract.Result<int>() == 42";
    }
}

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
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Sources
{
    [ContractClass(typeof(AContract))]
    abstract class ABase
    {
        public abstract Task<int> FooAsync(int returnValue);
    }

    [ContractClassFor(typeof(ABase))]
    abstract class AContract : ABase
    {
        // public async Task<int> FooAsync() doesn't help too
        public override Task<int> FooAsync(int returnValue)
        {
            Contract.Ensures(Contract.Result<int>() == 42);
            throw new NotImplementedException();
        }
    }

    class A : ABase
    {
        public override async Task<int> FooAsync(int returnValue)
        {
            await Task.Delay(1000);
            return returnValue;
        }
    }

    partial class TestMain
    {
        partial void Run()
        {
            if (behave)
            {
                // Should not throw
                new A().FooAsync(42).Wait();
            }
            else
            {
                // Precondition should be violated
                new A().FooAsync(43).Wait();
            }
        }

        public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Postcondition;
        public string NegativeExpectedCondition = "Contract.Result<int>() == 42";
    }
}

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

    [ContractClass(typeof(ITestInterfaceContract))]
    public interface ITestInterface
    {
        Task<T> GetAsync<T>(T value) where T : class;
    }

    [ContractClassFor(typeof(ITestInterface))]
    abstract class ITestInterfaceContract : ITestInterface
    {
        public Task<T> GetAsync<T>(T value) where T : class
        {
            Contract.Ensures(Contract.Result<T>() != null);
            return null;
        }
    }

    public class TestInterface : ITestInterface
    {
        public async Task<T> GetAsync<T>(T value) where T : class
        {
            await Task.Delay(100);
            return value;
        }
    }

    partial class TestMain
    {
        partial void Run()
        {
            if (behave)
            {
                // Should not fail!
                new TestInterface().GetAsync<object>(new object()).Wait();
            }
            else
            {
                // This one should fail with postcondition violation
                new TestInterface().GetAsync<object>(null).Wait();
            }
        }

        public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Postcondition;
        public string NegativeExpectedCondition = "Contract.Result<T>() != null";
    }
}

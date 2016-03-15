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

    class Foo<T>
    {
        private static int _staticValue = 42;
        private int _value = 1;
        private async Task<T> FooAsync(T result)
        {
            Contract.Ensures(object.ReferenceEquals(Contract.Result<T>(), default(T)) && _value.ToString() == "1" && _value == 1 && _staticValue.ToString() == "42");

            await Task.Delay(42);

            return result;
        }

        public static async Task HandleFooAsync(T result)
        {
           await new Foo<T>().FooAsync(result);
        }
    }

    partial class TestMain
    {
        partial void Run()
        {
            if (behave)
            {
                Foo<string>.HandleFooAsync(null).Wait();
            }
            else
            {
                Foo<string>.HandleFooAsync("should fail").GetAwaiter().GetResult();
            }
        }

        public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Postcondition;
        public string NegativeExpectedCondition = "object.ReferenceEquals(Contract.Result<T>(), default(T)) && _value.ToString() == \"1\" && _value == 1 && _staticValue.ToString() == \"42\"";
    }
}

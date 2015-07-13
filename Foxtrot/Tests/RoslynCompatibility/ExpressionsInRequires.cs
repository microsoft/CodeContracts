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

namespace Tests.Sources
{
    public class WithExpressions
    {
        // VS2015 changed the code that C# compiler generates for expression trees.
        // Those changes should be checked separately
        public void A(bool expected)
        {
            Contract.Requires(P1<string, string>(x => x, expected));
        }

        public void A2(bool expected)
        {
            Contract.Requires(P1<string, string>(x => x, expected));
            Contract.Requires(P1<string, string>(x => x, expected));
        }

        public void A3(bool expected)
        {
            Contract.Requires(P1<string, string>(x => x, expected));
            bool r = P1<string, string>(x => x, expected);
        }

        [Pure]
        public static bool P1<T1, T2>(Expression<Func<T1, T2>> x, bool result)
        {
            return result;
        }

        [Pure]
        public static bool P2<T1, T2>(Func<T1, T2> x, bool result)
        {
            return result;
        }
    }

    partial class TestMain
    {
        partial void Run()
        {
            if (behave)
            {
                new WithExpressions().A(true);
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

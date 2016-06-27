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
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace MaxJoins
{
    public class MaxJoins
    {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 12, MethodILOffset = 0)]
        private void Test0(bool b)
        {
            string s = Foo(b);
            Contract.Assert(s == null);
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 15, MethodILOffset = 0)]
        private void Test1(int i)
        {
            string s = Bar(i);
            Contract.Assert(s != null);
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 15, MethodILOffset = 0)]
        private void Test2(int i)
        {
            string s = FooBar(i);
            Contract.Assert(s != null);
        }

        [ClousotRegressionTest]
        private static string Foo(bool b)
        {
            string result = "non-null";
            if (b) {
                result = null;
            }
            return result;
        }

        [ClousotRegressionTest]
        private static string Bar(int i)
        {
            string x = null;
            if (i < 1) {
              x = "non-null";
            } else if (i < 3) {
              x = Bar(i - 1);
            }
            return x;
        }

        [ClousotRegressionTest]
        private static string FooBar(int i)
        {
            string x = null;
            if (i < 3) {
              x = FooBar(i - 1);
            } else if (i < 1) {
              x = "non-null";
            }
            return x;
        }
    }
}

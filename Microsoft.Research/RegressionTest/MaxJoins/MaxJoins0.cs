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
        [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 29, MethodILOffset = 0)]
        private void Test0(int x)
        {
            string s = "non-null";
            if (x < 0)
            {
                s = null;
            }
            Contract.Assert(s != null);
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 35, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.Bottom, Message = @"assert unreachable", PrimaryILOffset = 63, MethodILOffset = 0)]
        private void Test1(int x, int y)
        {
            string s0 = "non-null";
            string s1 = "non-null";
            if (x < 0)
            {
                s0 = null;
            }
            Contract.Assert(s0 != null);
            if (y < 0)
            {
                s1 = null;
            }
            Contract.Assert(s1 != null);
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.Bottom, Message = @"assert unreachable", PrimaryILOffset = 46, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 60, MethodILOffset = 0)]
        private void Test2(int x, bool b)
        {
            string s = null;
            string c = null;
            if (x < 0)
            {
                s = "non-null";
            }
            else
            {
                s = null;
                c = "non-null";
            }
            if (c != null)
            {
                Contract.Assert(b);
            }
            Contract.Assert(s != null);
        }
    }
}

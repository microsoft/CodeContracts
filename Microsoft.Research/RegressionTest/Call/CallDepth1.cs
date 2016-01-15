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

#define CONTRACTS_FULL

using System;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace CallDepth
{
    public class CallDepth
    {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 13, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 21, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.Bottom, Message = @"assert unreachable", PrimaryILOffset = 34, MethodILOffset = 0)]
        private void Test0(int x)
        {
            string s = "non-null";
            Contract.Assert(s != null);
            s = Test0Helper(x, s);
            Contract.Assert(s != null);
        }

        private string Test0Helper(int x, string s)
        {
            if (x < 0)
            {
                return null;
            }
            return s;
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 13, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 21, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.Bottom, Message = @"assert unreachable", PrimaryILOffset = 34, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.Bottom, Message = @"reference use unreached", PrimaryILOffset = 42, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.Bottom, Message = @"assert unreachable", PrimaryILOffset = 55, MethodILOffset = 0)]
        private void Test1(int x, int y)
        {
            string s = "non-null";
            Contract.Assert(s != null);
            s = Test1Helper0(x, s);
            Contract.Assert(s != null);
            s = Test1Helper1(y, s);
            Contract.Assert(s != null);
        }

        private string Test1Helper0(int x, string s)
        {
            if (x < 0)
            {
                return null;
            }
            return s;
        }

        private string Test1Helper1(int y, string s)
        {
            if (y < 0)
            {
                return null;
            }
            return s;
        }
    }
}

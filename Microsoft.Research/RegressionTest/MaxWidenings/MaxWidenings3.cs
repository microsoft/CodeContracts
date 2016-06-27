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

namespace MaxWidenings
{
    public class MaxWidenings
    {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 76, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: Contract.Result<long>() >= 0L", PrimaryILOffset = 25, MethodILOffset = 82)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Division by zero ok", PrimaryILOffset = 38, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Division by zero ok", PrimaryILOffset = 51, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 51, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 38, MethodILOffset = 0)]
        public static long Exp_Reflector(long x, long y)
        {
            Contract.Requires(y >= 0L);

            Contract.Ensures(Contract.Result<long>() >= 0L);

            long num = 1L;

            while (y > 0L)
            {
                if ((y % 2L) == 0L)
                {
                    x *= x;
                    y /= 2L;
                }
                else
                {
                    num *= x;
                    y -= 1L;
                }
            }
            Contract.Assert(y == 0L);

            return num;
        }
    }
}

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

namespace Tests.Sources
{

    partial class TestMain
    {
        public static class Things
        {
            public static class LeastFavourite
            {
                // With credits to The Chaser's War on Everything
                public static IEnumerable<string> AFewOfMyLeastFavouriteThings
                {
                    get
                    {
                        Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);

                        yield return "Prim Julie Andrews";
                        yield return "Cute Singing Kiddies";
                    }
                }
            }
        }

        private static void Test(object value)
        {
            Contract.Requires(value != null);
        }
        
        partial void Run()
        {
            if (behave)
            {
                foreach (string thing in Things.LeastFavourite.AFewOfMyLeastFavouriteThings)
                {
                    Test(thing);
                }
            }
            else
            {
                Test(null);
            }
        }

        public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
        // Condition is expected to be null because there is no pdb generated for the test assembly.
        public string NegativeExpectedCondition = null;
    }
}

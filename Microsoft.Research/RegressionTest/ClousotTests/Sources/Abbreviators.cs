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

namespace ClousotTests
{
    using System;
    using System.Diagnostics.Contracts;

    using Microsoft.Research.ClousotRegression;

    internal class Helper
    {
        [ContractAbbreviator]
        public static void EnsureNotNull<T>()
        {
            Contract.Ensures(Contract.Result<T>() != null);
        }
    }

    public class TestAbbreviations
    {
        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
        }

        public int Z
        {
            get;
            set;
        }

        [ContractAbbreviator]
        private void AdvertiseUnchanged()
        {
            Contract.Ensures(X == Contract.OldValue(X));
            Contract.Ensures(Y == Contract.OldValue(Y));
            Contract.Ensures(Z == Contract.OldValue(Z));
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 19, MethodILOffset = 6)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 43, MethodILOffset = 6)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 67, MethodILOffset = 6)]
        public void Work1()
        {
            AdvertiseUnchanged();
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 19, MethodILOffset = 18)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 43, MethodILOffset = 18)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 67, MethodILOffset = 18)]
        public void Work2()
        {
            AdvertiseUnchanged();

            X = X;
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 19, MethodILOffset = 12)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 43, MethodILOffset = 12)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 67, MethodILOffset = 12)]
        public void Work3()
        {
            AdvertiseUnchanged();

            Work2();
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.False, Message = "ensures is false: this.X == Contract.OldValue(this.X)", PrimaryILOffset = 19, MethodILOffset = 20)]
        [RegressionOutcome(Outcome = ProofOutcome.Bottom, Message = @"ensures unreachable", PrimaryILOffset = 43, MethodILOffset = 20)]
        [RegressionOutcome(Outcome = ProofOutcome.Bottom, Message = @"ensures unreachable", PrimaryILOffset = 67, MethodILOffset = 20)]
        public void Work4()
        {
            AdvertiseUnchanged();

            X++;
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 16, MethodILOffset = 10)]
        public string GetTheData0()
        {
            Helper.EnsureNotNull<string>();

            return "";
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.False, Message = "ensures is false: Contract.Result<T>() != null", PrimaryILOffset = 16, MethodILOffset = 6)]
        public string GetTheData1()
        {
            Helper.EnsureNotNull<string>();

            return null;
        }

        [ClousotRegressionTest]
        public string GetTheData2()
        {
            //Helper.EnsureNotNull<float>();

            return null;
        }
    }
}
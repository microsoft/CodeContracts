// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        [ContractAbbreviator]
        private void AdvertiseUnchanged()
        {
            Contract.Ensures(this.X == Contract.OldValue(this.X));
            Contract.Ensures(this.Y == Contract.OldValue(this.Y));
            Contract.Ensures(this.Z == Contract.OldValue(this.Z));
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
        [RegressionOutcome(Outcome = ProofOutcome.False, Message = "ensures is false: X == Contract.OldValue(X)", PrimaryILOffset = 19, MethodILOffset = 20)]
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

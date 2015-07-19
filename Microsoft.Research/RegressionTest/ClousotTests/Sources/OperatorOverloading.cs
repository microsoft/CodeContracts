// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace OperatorOverloading
{
    internal class Program
    {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 8, MethodILOffset = 11)]
        private static void Main(string[] args)
        {
            Work((string)new Class());
        }

        private static void Work(string p)
        {
            Contract.Requires(p != null);
        }
    }
}

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace UserFeedback
{
    internal class Iterators
    {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 69, MethodILOffset = 0)]
        private static void AsList()
        {
            var xs = new List<int>() { 1, 2, 3 };

            Contract.Assume(xs.Count > 0);

            foreach (var x in xs) // struct enumerator
            {
                Contract.Assert(xs.Count > 0);
            }
        }
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 68, MethodILOffset = 0)]
        private static void AsCollection()
        {
            ICollection<int> xs = new List<int>() { 1, 2, 3 };

            Contract.Assume(xs.Count > 0);

            foreach (var x in xs) // IEnumerator enumerator
            {
                Contract.Assert(xs.Count > 0);
            }
        }
    }
}

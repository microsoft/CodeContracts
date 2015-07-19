// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace Quantifiers
{
    public class ExampleWithForAll
    {
        [ClousotRegressionTest]
#if FIRST
        [RegressionOutcome("No entry found in the cache")]
#else
        [RegressionOutcome("No entry found in the cache")] // it should not be found
        [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 91, MethodILOffset = 0)]
#endif
        // no warning
        public void UseIncorrect(string[] array)
        {
            Contract.Requires(array != null);
            Contract.Requires(array.Length > 0);
#if FIRST
            Contract.Requires(Contract.ForAll(0, array.Length, i => array[i] != null));
#else
            Contract.Requires(Contract.ForAll(0, array.Length, i => array[i] == null));
#endif
            Contract.Assert(array[0] != null);
        }


        [ClousotRegressionTest]
#if FIRST
        [RegressionOutcome("No entry found in the cache")]
#else
        [RegressionOutcome("An entry has been found in the cache")]
#endif
        // no warning
        public void UseCorrect(string[] array)
        {
            Contract.Requires(array != null);
            Contract.Requires(array.Length > 0);
            Contract.Requires(Contract.ForAll(0, array.Length, i => array[i] != null));

            Contract.Assert(array[0] != null);
        }
    }
}

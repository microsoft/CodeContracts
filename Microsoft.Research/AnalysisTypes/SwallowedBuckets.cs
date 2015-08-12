// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.Contracts;


namespace Microsoft.Research.CodeAnalysis
{
    public class SwallowedBuckets
    {
        #region Object invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(counter != null);
        }

        #endregion

        readonly private int[] counter;

        public SwallowedBuckets()
        {
            counter = new int[4];
        }

        public delegate int CounterGetter(ProofOutcome outcome);

        public SwallowedBuckets(CounterGetter counterGetter)
          : this()
        {
            Contract.Requires(counterGetter != null);

            for (int i = 0; i < counter.Length; i++)
            {
                counter[i] = counterGetter((ProofOutcome)i);
            }
        }

        public void UpdateCounter(ProofOutcome outcome)
        {
            Contract.Assert(0 <= outcome);
            Contract.Assume((int)outcome < counter.Length);

            counter[(int)outcome]++;
        }

        [Pure]
        public int GetCounter(ProofOutcome outcome)
        {
            Contract.Assert(0 <= outcome);
            Contract.Assume((int)outcome < counter.Length);
            return counter[(int)outcome];
        }

        [Pure]
        public static SwallowedBuckets operator -(SwallowedBuckets sw1, SwallowedBuckets sw2)
        {
            Contract.Requires(sw1 != null);
            Contract.Requires(sw2 != null);
            Contract.Ensures(Contract.Result<SwallowedBuckets>() != null);

            return new SwallowedBuckets(outcome => sw1.GetCounter(outcome) - sw2.GetCounter(outcome));
        }
    }
}
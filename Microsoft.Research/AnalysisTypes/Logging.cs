// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

//#define TRACEPERFORMANCE

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Microsoft.Research
{
    public static class PerformanceMeasure
    {
        public enum ActionTags { CheckIfEqual, Simplex, KarrIsBottom, KarrPutIntoRowEchelonForm, KarrRemoveVars, ArraysJoin, ArraysAssignInParallel, WP, SubPolyJoin }

        private readonly static TimeSpan LongSingleOperation = new TimeSpan(0, 0, 0, 0, 500);
        private readonly static TimeSpan LongOverallOperation = new TimeSpan(10000);

        private readonly static Dictionary<ActionTags, Tuple<int, TimeSpan>> performanceCounters = new Dictionary<ActionTags, Tuple<int, TimeSpan>>();
        private static int invocationCounter = 0;

        [Pure]
        public static T Measure<T>(ActionTags slot, Func<T> action, bool warnIfOperationTakesTooLong = false)
        {
            Contract.Requires(action != null);

            var now = DateTime.Now;
            var result = action();
            var elapsed = DateTime.Now - now;
            var count = 0;

#if TRACEPERFORMANCE
            if (warnIfOperationTakesTooLong && elapsed > LongSingleOperation)
            {
                Console.WriteLine("Time performing action #{0}: {1} (invocationCounter = {2})", slot, elapsed, invocationCounter);
            }
#endif
            lock (performanceCounters)
            {
                Tuple<int, TimeSpan> prev;
                if (performanceCounters.TryGetValue(slot, out prev))
                {
                    count = prev.Item1;
                    elapsed += prev.Item2;
                }

                performanceCounters[slot] = new Tuple<int, TimeSpan>(count + 1, elapsed);
            }
            invocationCounter++;
            return result;
        }

        public static StringBuilder GetStats()
        {
            Contract.Ensures(Contract.Result<StringBuilder>() != null);

            var str = new StringBuilder();

            lock (performanceCounters) // For the Clousot2 regression test run in parallel
            {
                foreach (var pair in performanceCounters)
                {
                    str.AppendFormat("Overall time spent performing action #{0}: {1} (invoked {2} times)\n", pair.Key, pair.Value.Item2, pair.Value.Item1);
                }
            }
            return str;
        }
    }
}

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

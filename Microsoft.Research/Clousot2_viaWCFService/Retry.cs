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
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;

namespace Microsoft.Research.CodeAnalysis
{
  static class Retry<E>
    where E : Exception
  {
    private static readonly Random rand = new Random();

    private static long MulMin(long l, double d, long m)
    {
      return Math.Min((long)(d * (double)l), m);
    }

    public static bool RetryOnException<T>(Func<T> f, RetryOptions options, out T value)
    {
      var minTicks = options.FirstMinTime.Ticks;
      var maxTicks = options.FirstMaxTime.Ticks;

      for (uint remainingRetries = options.NumberOfRetries; remainingRetries > 0; remainingRetries--)
      {
        try
        {
          value = f();
          return true;
        }
        catch (E)
        { }

        var waitingTicks = rand.NextLong(minTicks, maxTicks);

        Thread.Sleep(new TimeSpan(waitingTicks));

        minTicks = MulMin(minTicks, options.MultiplyingFactor, options.MaxTime.Ticks);
        maxTicks = MulMin(maxTicks, options.MultiplyingFactor, options.MaxTime.Ticks);
      }

      value = default(T);
      return false;
    }
  }

  struct RetryOptions
  {
    public TimeSpan FirstMinTime;
    public TimeSpan FirstMaxTime;
    public TimeSpan MaxTime;
    public double MultiplyingFactor;
    public uint NumberOfRetries;

    public RetryOptions Clone()
    {
      return this;
    }
  }

  static class RandomExtension
  {
    public static long NextLong(this Random random, long min, long max)
    {
      Contract.Requires(random != null);

      return (long)(random.NextDouble() * (double)(max - min)) + min;
    }
  }
}

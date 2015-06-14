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
using System.Diagnostics.Contracts;

namespace System.Diagnostics
{
#if !SILVERLIGHT
  public class Stopwatch
  {
    public static readonly long Frequency;
    public static readonly bool IsHighResolution;

    public Stopwatch()
    {
    }

    //public TimeSpan Elapsed { get; }

    public long ElapsedMilliseconds
    {
      get
      {
        Contract.Ensures(Contract.Result<long>() >= 0);
        return 0;
      }
    }

    public long ElapsedTicks
    {
      get
      {
        Contract.Ensures(Contract.Result<long>() >= 0);
        return 0;
      }
    }

    //public bool IsRunning { get; }

    public static long GetTimestamp()
    {
        Contract.Ensures(Contract.Result<long>() >= 0);
        return 0;
    }

    //
    // Summary:
    //     Stops time interval measurement and resets the elapsed time to zero.
    public void Reset() { }

    //
    // Summary:
    //     Stops time interval measurement, resets the elapsed time to zero, and starts
    //     measuring elapsed time.
    //public void Restart() { }

    //
    // Summary:
    //     Starts, or resumes, measuring elapsed time for an interval.
    public void Start() { }
    public static Stopwatch StartNew()
    {
      Contract.Ensures(Contract.Result<Stopwatch>() != null);

      return null;
    }
    public void Stop() { }
  }
#endif
}
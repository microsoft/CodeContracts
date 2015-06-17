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
using System.Text;
using System.Runtime.InteropServices;

namespace Microsoft.Research.DataStructures
{
  /// <summary>
  /// The class for measuring the time
  /// </summary>
  public class TimeCounter
  {
    private enum State { Stopped, Started, Paused };

    private Int64 frequency;
    private Int64 ticks;
    private Int64 elapsed;
    private Int64 totalElapsed;
    private State state;
    private bool printOverallTime = true;

    [DllImport("Kernel32.dll")]
    private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
    [DllImport("Kernel32.dll")]
    private static extern bool QueryPerformanceFrequency(out long lpFrequency);

    /// <summary>
    /// In miliseconds
    /// </summary>
    private long Elapsed
    {
      get { return ToMilliseconds(this.elapsed); }
    }

    private long TotalElapsed
    {
      get { return ToMilliseconds(this.totalElapsed); }
    }

    private long ToMilliseconds(long ticks)
    {
      return (ticks * 1000) / this.frequency;
    }

    #region Getters/Setters
    public long InSeconds
    {
      get
      {
        return ((this.Elapsed / (Int64)1000));
      }
    }

    public long InMilliSeconds
    {
      get
      {
        return (this.Elapsed);
      }
    }

    #endregion

    #region Constructor
    public TimeCounter()
    {
      this.ticks = -1;
      this.elapsed = 0;
      this.totalElapsed = 0;
      this.state = State.Stopped;
      this.printOverallTime = false;
      if (!QueryPerformanceFrequency(out this.frequency))
      {
        throw new NotImplementedException("This system does not support the high precision clock of Win32");
      }
    }

    public TimeCounter(bool printOverallTime)
    {
      this.ticks = -1;
      this.elapsed = 0;
      this.totalElapsed = 0;
      this.state = State.Stopped;
      this.printOverallTime = printOverallTime;
      if (!QueryPerformanceFrequency(out this.frequency))
      {
        throw new NotImplementedException("This system does not support the high precision clock of Win32");
      }
    }

    private TimeCounter(Int64 elapsed, Int64 totalElapsed, bool printOverallTime)
    {
      this.ticks = -1;
      this.elapsed = elapsed;
      this.totalElapsed = totalElapsed;
      this.state = State.Stopped;
      this.printOverallTime = printOverallTime;
      if (!QueryPerformanceFrequency(out this.frequency))
      {
        throw new NotImplementedException("This system does not support the high precision clock of Win32");
      }
    }
    #endregion

    #region Start/Stop

    /// <summary>
    /// Start the performance counter
    /// </summary>
    public void Start()
    {
      if (this.state != State.Started)
      {
        if (this.state == State.Stopped)
        {
          this.elapsed = 0;
        }

        QueryPerformanceCounter(out this.ticks);
        this.state = State.Started;
      }
    }

    public void Pause()
    {
      if (this.state == State.Started)
      {
        Int64 now;
        QueryPerformanceCounter(out now);
        this.elapsed += now - this.ticks;
        this.state = State.Paused;
      }
    }

    /// <summary>
    /// Stop the performance counter
    /// </summary>
    public void Stop()
    {
      if (this.state == State.Started)
      {
        Int64 now;
        QueryPerformanceCounter(out now);
        this.elapsed += now - this.ticks;
        this.totalElapsed += this.elapsed;
        this.state = State.Stopped;
      }
    }

    #endregion

    public static TimeCounter operator +(TimeCounter p1, TimeCounter p2)
    {
      return new TimeCounter(p1.elapsed + p2.elapsed, p1.totalElapsed + p2.totalElapsed, p1.printOverallTime || p2.printOverallTime);
    }

    public override string ToString()
    {      
      var elap = PrettyPrint(this.Elapsed);

      if (this.printOverallTime)
      {
        var total = PrettyPrint(this.TotalElapsed);

        return string.Format("{0} (so far {1})", elap, total);
      }
      else
      {
        return elap;
      }
    }

    private static string PrettyPrint(Int64 time)
    {
      string elap;
      if (time > 60000)
      {
        long minutes = (time / 60000);
        long seconds = (time % 60000) / 1000;
        elap = String.Format("{0}:{1:00}min", minutes, seconds);
      }
      else if (time > 1000)
      {
        elap = String.Format("{0:0.000}sec", (double)time / 1000);
      }
      else
      {
        elap = time + "ms";
      }
      return elap;
    }
  }

}

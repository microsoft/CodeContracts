// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
            get { return ToMilliseconds(elapsed); }
        }

        private long TotalElapsed
        {
            get { return ToMilliseconds(totalElapsed); }
        }

        private long ToMilliseconds(long ticks)
        {
            return (ticks * 1000) / frequency;
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
            ticks = -1;
            elapsed = 0;
            totalElapsed = 0;
            state = State.Stopped;
            printOverallTime = false;
            if (!QueryPerformanceFrequency(out frequency))
            {
                throw new NotImplementedException("This system does not support the high precision clock of Win32");
            }
        }

        public TimeCounter(bool printOverallTime)
        {
            ticks = -1;
            elapsed = 0;
            totalElapsed = 0;
            state = State.Stopped;
            this.printOverallTime = printOverallTime;
            if (!QueryPerformanceFrequency(out frequency))
            {
                throw new NotImplementedException("This system does not support the high precision clock of Win32");
            }
        }

        private TimeCounter(Int64 elapsed, Int64 totalElapsed, bool printOverallTime)
        {
            ticks = -1;
            this.elapsed = elapsed;
            this.totalElapsed = totalElapsed;
            state = State.Stopped;
            this.printOverallTime = printOverallTime;
            if (!QueryPerformanceFrequency(out frequency))
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
            if (state != State.Started)
            {
                if (state == State.Stopped)
                {
                    elapsed = 0;
                }

                QueryPerformanceCounter(out ticks);
                state = State.Started;
            }
        }

        public void Pause()
        {
            if (state == State.Started)
            {
                Int64 now;
                QueryPerformanceCounter(out now);
                elapsed += now - ticks;
                state = State.Paused;
            }
        }

        /// <summary>
        /// Stop the performance counter
        /// </summary>
        public void Stop()
        {
            if (state == State.Started)
            {
                Int64 now;
                QueryPerformanceCounter(out now);
                elapsed += now - ticks;
                totalElapsed += elapsed;
                state = State.Stopped;
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

            if (printOverallTime)
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

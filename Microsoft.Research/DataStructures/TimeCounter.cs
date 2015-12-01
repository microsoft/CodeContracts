// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;

namespace Microsoft.Research.DataStructures
{
    public class CustomStopwatch : Stopwatch
    {
        long elapsedSymbolic;
        public long ElapsedSymbolic
        {
            get { return elapsedSymbolic; }
        }

        public void SpendSymbolicTime(long amount)
        {
            if (this.IsRunning)
            {
                System.Threading.Interlocked.Add(ref elapsedSymbolic, amount);
            }
        }

        public new void Reset()
        {
            System.Threading.Interlocked.Exchange(ref elapsedSymbolic, 0);
            base.Reset();
        }

        public new void Restart()
        {
            System.Threading.Interlocked.Exchange(ref elapsedSymbolic, 0);
            base.Restart();
        }
    }

    /// <summary>
    /// The class for measuring the time
    /// </summary>
    public class TimeCounter
    {
        private CustomStopwatch watch = new CustomStopwatch();

        #region Getters/Setters

        public long InMilliSeconds
        {
            get
            {
                return watch.ElapsedMilliseconds;
            }
        }

        #endregion

        public void SpendSymbolicTime(long amount)
        {
            if (watch.IsRunning)
            {
                watch.SpendSymbolicTime(amount);
            }
        }

        #region Start/Stop

        /// <summary>
        /// Start the performance counter
        /// </summary>
        public void Start()
        {
            if (!watch.IsRunning)
            {
                watch.Restart();
            }
        }

        /// <summary>
        /// Stop the performance counter
        /// </summary>
        public void Stop()
        {
            if (watch.IsRunning)
            {
                watch.Stop();
            }
        }

        #endregion

        public override string ToString()
        {
            return PrettyPrint(this.InMilliSeconds);
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

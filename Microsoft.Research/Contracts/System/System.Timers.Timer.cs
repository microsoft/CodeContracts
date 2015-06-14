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

using System.Diagnostics.Contracts;
using System;

namespace System.Timers
{

    public class Timer
    {

        public bool AutoReset
        {
          get;
          set;
        }

        public System.ComponentModel.ISite Site
        {
          get;
          set;
        }

        public bool Enabled
        {
          get;
          set;
        }

        public double Interval
        {
          get;
          set
            Contract.Requires(value > 0);
        }

        public System.ComponentModel.ISynchronizeInvoke SynchronizingObject
        {
          get;
          set;
        }

        public void Stop () {

        }
        public void Start () {

        }
        public void EndInit () {

        }
        public void Close () {

        }
        public void BeginInit () {

        }
        public void remove_Elapsed (ElapsedEventHandler value) {

        }
        public void add_Elapsed (ElapsedEventHandler value) {

        }
        public Timer (double interval) {
            Contract.Requires(interval > 0);

          return default(Timer);
        }
        public Timer () {
          return default(Timer);
        }
    }
}

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

namespace System.Threading
{

    public class Timer
    {

        public void Dispose () {

        }
        public bool Dispose (WaitHandle! notifyObject) {
            CodeContract.Requires(notifyObject != null);

          return default(bool);
        }
        public bool Change (Int64 dueTime, Int64 period) {

          return default(bool);
        }
        public bool Change (UInt32 dueTime, UInt32 period) {

          return default(bool);
        }
        public bool Change (TimeSpan dueTime, TimeSpan period) {

          return default(bool);
        }
        public bool Change (int dueTime, int period) {

          return default(bool);
        }
        public Timer (TimerCallback callback, object state, Int64 dueTime, Int64 period) {
            CodeContract.Requires(dueTime >= -1);
            CodeContract.Requires(period >= -1);
            /* NOT VALID BECAUSE MAX_SUPPORTED_TIMEOUT IS PRIVATE
            CodeContract.Requires(dueTime <= System.Threading.Timer.MAX_SUPPORTED_TIMEOUT);
            CodeContract.Requires(period <= System.Threading.Timer.MAX_SUPPORTED_TIMEOUT);
            */

          return default(Timer);
        }
        public Timer (TimerCallback callback, object state, UInt32 dueTime, UInt32 period) {

          return default(Timer);
        }
        public Timer (TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period) {

          return default(Timer);
        }
        public Timer (TimerCallback callback, object state, int dueTime, int period) {
            CodeContract.Requires(dueTime >= -1);
            CodeContract.Requires(period >= -1);
          return default(Timer);
        }
    }
}

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

    public class ThreadPool
    {

        public static bool BindHandle (int osHandle) {

          return default(bool);
        }
        public static bool UnsafeQueueUserWorkItem (WaitCallback callBack, object state) {

          return default(bool);
        }
        public static bool QueueUserWorkItem (WaitCallback callBack) {

          return default(bool);
        }
        public static bool QueueUserWorkItem (WaitCallback callBack, object state) {

          return default(bool);
        }
        public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject (WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce) {

          return default(RegisteredWaitHandle);
        }
        public static RegisteredWaitHandle RegisterWaitForSingleObject (WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce) {

          return default(RegisteredWaitHandle);
        }
        public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject (WaitHandle waitObject, WaitOrTimerCallback callBack, object state, Int64 millisecondsTimeOutInterval, bool executeOnlyOnce) {
            CodeContract.Requires(millisecondsTimeOutInterval >= -1);

          return default(RegisteredWaitHandle);
        }
        public static RegisteredWaitHandle RegisterWaitForSingleObject (WaitHandle waitObject, WaitOrTimerCallback callBack, object state, Int64 millisecondsTimeOutInterval, bool executeOnlyOnce) {
            CodeContract.Requires(millisecondsTimeOutInterval >= -1);

          return default(RegisteredWaitHandle);
        }
        public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject (WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce) {
            CodeContract.Requires(millisecondsTimeOutInterval >= -1);

          return default(RegisteredWaitHandle);
        }
        public static RegisteredWaitHandle RegisterWaitForSingleObject (WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce) {
            CodeContract.Requires(millisecondsTimeOutInterval >= -1);

          return default(RegisteredWaitHandle);
        }
        public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject (WaitHandle waitObject, WaitOrTimerCallback callBack, object state, UInt32 millisecondsTimeOutInterval, bool executeOnlyOnce) {

          return default(RegisteredWaitHandle);
        }
        public static RegisteredWaitHandle RegisterWaitForSingleObject (WaitHandle waitObject, WaitOrTimerCallback callBack, object state, UInt32 millisecondsTimeOutInterval, bool executeOnlyOnce) {

          return default(RegisteredWaitHandle);
        }
        public static void GetAvailableThreads (ref int workerThreads, ref int completionPortThreads) {

        }
        public static void GetMinThreads (ref int workerThreads, ref int completionPortThreads) {

        }
        public static bool SetMinThreads (int workerThreads, int completionPortThreads) {

          return default(bool);
        }
        public static void GetMaxThreads (ref int workerThreads, ref int completionPortThreads) {
        }
    }
}

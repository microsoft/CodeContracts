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
using System.Runtime.InteropServices;

namespace System.Threading
{
  // Summary:
  //     Specifies the execution states of a System.Threading.Thread.
  public enum ThreadState
  {
    // Summary:
    //     The thread has been started, it is not blocked, and there is no pending System.Threading.ThreadAbortException.
    Running = 0,
    //
    // Summary:
    //     The thread is being requested to stop. This is for internal use only.
    StopRequested = 1,
    //
    // Summary:
    //     The thread is being requested to suspend.
    SuspendRequested = 2,
    //
    // Summary:
    //     The thread is being executed as a background thread, as opposed to a foreground
    //     thread. This state is controlled by setting the System.Threading.Thread.IsBackground
    //     property.
    Background = 4,
    //
    // Summary:
    //     The System.Threading.Thread.Start() method has not been invoked on the thread.
    Unstarted = 8,
    //
    // Summary:
    //     The thread has stopped.
    Stopped = 16,
    //
    // Summary:
    //     The thread is blocked. This could be the result of calling System.Threading.Thread.Sleep(System.Int32)
    //     or System.Threading.Thread.Join(), of requesting a lock — for example, by
    //     calling System.Threading.Monitor.Enter(System.Object) or System.Threading.Monitor.Wait(System.Object,System.Int32,System.Boolean)
    //     — or of waiting on a thread synchronization object such as System.Threading.ManualResetEvent.
    WaitSleepJoin = 32,
    //
    // Summary:
    //     The thread has been suspended.
    Suspended = 64,
    //
    // Summary:
    //     The System.Threading.Thread.Abort(System.Object) method has been invoked
    //     on the thread, but the thread has not yet received the pending System.Threading.ThreadAbortException
    //     that will attempt to terminate it.
    AbortRequested = 128,
    //
    // Summary:
    //     The thread state includes System.Threading.ThreadState.AbortRequested and
    //     the thread is now dead, but its state has not yet changed to System.Threading.ThreadState.Stopped.
    Aborted = 256,
  }
}

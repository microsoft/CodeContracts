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


#if NETFRAMEWORK_4_0
using System;

namespace System.Threading.Tasks
{
    //
    // Summary:
    //     Represents the current stage in the lifecycle of a System.Threading.Tasks.Task.
    public enum TaskStatus
    {
      //
      // Summary:
      //     The task has been initialized but has not yet been scheduled.
      Created = 0,
      //
      // Summary:
      //     The task is waiting to be activated and scheduled internally by the .NET Framework
      //     infrastructure.
      WaitingForActivation = 1,
      //
      // Summary:
      //     The task has been scheduled for execution but has not yet begun executing.
      WaitingToRun = 2,
      //
      // Summary:
      //     The task is running but has not yet completed.
      Running = 3,
      //
      // Summary:
      //     The task has finished executing and is implicitly waiting for attached child
      //     tasks to complete.
      WaitingForChildrenToComplete = 4,
      //
      // Summary:
      //     The task completed execution successfully.
      RanToCompletion = 5,
      //
      // Summary:
      //     The task acknowledged cancellation by throwing an OperationCanceledException
      //     with its own CancellationToken while the token was in signaled state, or the
      //     task's CancellationToken was already signaled before the task started executing.
      //     For more information, see Task Cancellation.
      Canceled = 6,
      //
      // Summary:
      //     The task completed due to an unhandled exception.
      Faulted = 7
    }
}
#endif
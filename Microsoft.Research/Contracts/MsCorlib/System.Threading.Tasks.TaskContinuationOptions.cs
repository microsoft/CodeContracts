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
  // Summary:
  //     Specifies the behavior for a task that is created by using the System.Threading.Tasks.Task.ContinueWith
  //     or System.Threading.Tasks.Task`1.ContinueWith method.
//  [Serializable]
//  [Flags]
  public enum TaskContinuationOptions
  {
    // Summary:
    //     Default = "Continue on any, no task options, run asynchronously" Specifies
    //     that the default behavior should be used. Continuations, by default, will
    //     be scheduled when the antecedent task completes, regardless of the task's
    //     final System.Threading.Tasks.TaskStatus.
    None = 0,
    //
    // Summary:
    //     A hint to a System.Threading.Tasks.TaskScheduler to schedule a task in as
    //     fair a manner as possible, meaning that tasks scheduled sooner will be more
    //     likely to be run sooner, and tasks scheduled later will be more likely to
    //     be run later.
    PreferFairness = 1,
    //
    // Summary:
    //     Specifies that a task will be a long-running, course-grained operation. It
    //     provides a hint to the System.Threading.Tasks.TaskScheduler that oversubscription
    //     may be warranted.
    LongRunning = 2,
    //
    // Summary:
    //     Specifies that a task is attached to a parent in the task hierarchy.
    AttachedToParent = 4,
    //
    // Summary:
    //     Specifies that the continuation task should not be scheduled if its antecedent
    //     ran to completion. This option is not valid for multi-task continuations.
    NotOnRanToCompletion = 65536,
    //
    // Summary:
    //     Specifies that the continuation task should not be scheduled if its antecedent
    //     threw an unhandled exception. This option is not valid for multi-task continuations.
    NotOnFaulted = 131072,
    //
    // Summary:
    //     Specifies that the continuation task should be scheduled only if its antecedent
    //     was canceled. This option is not valid for multi-task continuations.
    OnlyOnCanceled = 196608,
    //
    // Summary:
    //     Specifies that the continuation task should not be scheduled if its antecedent
    //     was canceled. This option is not valid for multi-task continuations.
    NotOnCanceled = 262144,
    //
    // Summary:
    //     Specifies that the continuation task should be scheduled only if its antecedent
    //     threw an unhandled exception. This option is not valid for multi-task continuations.
    OnlyOnFaulted = 327680,
    //
    // Summary:
    //     Specifies that the continuation task should be scheduled only if its antecedent
    //     ran to completion. This option is not valid for multi-task continuations.
    OnlyOnRanToCompletion = 393216,
    //
    // Summary:
    //     Specifies that the continuation task should be executed synchronously. With
    //     this option specified, the continuation will be run on the same thread that
    //     causes the antecedent task to transition into its final state. If the antecedent
    //     is already complete when the continuation is created, the continuation will
    //     run on the thread creating the continuation. Only very short-running continuations
    //     should be executed synchronously.
    ExecuteSynchronously = 524288,
  }
}
#endif
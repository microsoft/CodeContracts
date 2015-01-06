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
using System.Diagnostics;
using System.Security;
using System.Diagnostics.Contracts;

#if NETFRAMEWORK_4_0

namespace System.Threading.Tasks
{
  // Summary:
  //     Represents an object that handles the low-level work of queuing tasks onto
  //     threads.
  //[DebuggerTypeProxy(typeof(TaskScheduler.SystemThreadingTasks_TaskSchedulerDebugView))]
  //[DebuggerDisplay("Id={Id}")]
  public abstract class TaskScheduler
  {
    // Summary:
    //     Initializes the System.Threading.Tasks.TaskScheduler.
    //protected TaskScheduler();

    // Summary:
    //     Gets the System.Threading.Tasks.TaskScheduler associated with the currently
    //     executing task.
    //
    // Returns:
    //     Returns the System.Threading.Tasks.TaskScheduler associated with the currently
    //     executing task.
    //public static TaskScheduler Current { get; }
    public static TaskScheduler Current 
    {
      get
      {
        Contract.Ensures(Contract.Result<TaskScheduler>() != null);

        return default(TaskScheduler);
      }
    }
    //
    // Summary:
    //     Gets the default System.Threading.Tasks.TaskScheduler instance that is provided
    //     by the .NET Framework.
    //
    // Returns:
    //     Returns the default System.Threading.Tasks.TaskScheduler instance.
    public static TaskScheduler Default 
    {
      get
      {
        Contract.Ensures(Contract.Result<TaskScheduler>() != null);

        return default(TaskScheduler);
      }
    }
    //
    // Summary:
    //     Gets the unique ID for this System.Threading.Tasks.TaskScheduler.
    //
    // Returns:
    //     Returns the unique ID for this System.Threading.Tasks.TaskScheduler.
    //public int Id { get; }
    //
    // Summary:
    //     Indicates the maximum concurrency level this System.Threading.Tasks.TaskScheduler
    //     is able to support.
    //
    // Returns:
    //     Returns an integer that represents the maximum concurrency level.
    //public virtual int MaximumConcurrencyLevel { get; }

    // Summary:
    //     Occurs when a faulted System.Threading.Tasks.Task's unobserved exception
    //     is about to trigger exception escalation policy, which, by default, would
    //     terminate the process.
    //public static event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException;

    // Summary:
    //     Creates a System.Threading.Tasks.TaskScheduler associated with the current
    //     System.Threading.SynchronizationContext.
    //
    // Returns:
    //     A System.Threading.Tasks.TaskScheduler associated with the current System.Threading.SynchronizationContext,
    //     as determined by System.Threading.SynchronizationContext.Current.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The current SynchronizationContext may not be used as a TaskScheduler.
    //public static TaskScheduler FromCurrentSynchronizationContext();
    //
    // Summary:
    //     Generates an enumerable of System.Threading.Tasks.Task instances currently
    //     queued to the scheduler waiting to be executed.
    //
    // Returns:
    //     An enumerable that allows traversal of tasks currently queued to this scheduler.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     This scheduler is unable to generate a list of queued tasks at this time.
    //[SecurityCritical]
    protected abstract IEnumerable<Task> GetScheduledTasks();
    //
    // Summary:
    //     Queues a System.Threading.Tasks.Task to the scheduler.
    //
    // Parameters:
    //   task:
    //     The System.Threading.Tasks.Task to be queued.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The task argument is null.
    //[SecurityCritical]
    //protected internal abstract void QueueTask(Task task);
    //
    // Summary:
    //     Attempts to dequeue a System.Threading.Tasks.Task that was previously queued
    //     to this scheduler.
    //
    // Parameters:
    //   task:
    //     The System.Threading.Tasks.Task to be dequeued.
    //
    // Returns:
    //     A Boolean denoting whether the task argument was successfully dequeued.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The task argument is null.
    //[SecurityCritical]
    protected internal virtual bool TryDequeue(Task task)
    {
      Contract.Requires(task != null);

      return default(bool);
    }
    //
    // Summary:
    //     Attempts to execute the provided System.Threading.Tasks.Task on this scheduler.
    //
    // Parameters:
    //   task:
    //     A System.Threading.Tasks.Task object to be executed.
    //
    // Returns:
    //     A Boolean that is true if task was successfully executed, false if it was
    //     not. A common reason for execution failure is that the task had previously
    //     been executed or is in the process of being executed by another thread.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The task is not associated with this scheduler.
    //[SecurityCritical]
    //protected bool TryExecuteTask(Task task);
    //
    // Summary:
    //     Determines whether the provided System.Threading.Tasks.Task can be executed
    //     synchronously in this call, and if it can, executes it.
    //
    // Parameters:
    //   task:
    //     The System.Threading.Tasks.Task to be executed.
    //
    //   taskWasPreviouslyQueued:
    //     A Boolean denoting whether or not task has previously been queued. If this
    //     parameter is True, then the task may have been previously queued (scheduled);
    //     if False, then the task is known not to have been queued, and this call is
    //     being made in order to execute the task inline without queuing it.
    //
    // Returns:
    //     A Boolean value indicating whether the task was executed inline.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The task argument is null.
    //
    //   System.InvalidOperationException:
    //     The task was already executed.
    //[SecurityCritical]
    //protected abstract bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued);
  }
}
#endif
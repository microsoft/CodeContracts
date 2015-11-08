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
//#if true
namespace System.Threading.Tasks
{
  using System.Diagnostics.Contracts;

  public class Task // : IThreadPoolWorkItem, IAsyncResult, IDisposable
  {
    protected Task() { }

    // Summary:
    //     Initializes a new System.Threading.Tasks.Task with the specified action.
    //
    // Parameters:
    //   action:
    //     The delegate that represents the code to execute in the Task.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The action argument is null.
    public Task(System.Action action)
    {
      Contract.Requires(action != null);
    }
    //
    // Summary:
    //     Initializes a new System.Threading.Tasks.Task with the specified action and
    //     state.
    //
    // Parameters:
    //   action:
    //     The delegate that represents the code to execute in the task.
    //
    //   state:
    //     An object representing data to be used by the action.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The action argument is null.
    public Task(Action<object> action, object state)
    {
      Contract.Requires(action != null);
    }

    //
    // Summary:
    //     Initializes a new System.Threading.Tasks.Task with the specified action and
    //     System.Threading.CancellationToken while observing a cancellation token.
    //
    // Parameters:
    //   action:
    //     The delegate that represents the code to execute in the Task.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that the new Task will observe.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The action argument is null.
    public Task(Action action, CancellationToken cancellationToken)
    {
      Contract.Requires(action != null);
    }
    //
    // Summary:
    //     Initializes a new System.Threading.Tasks.Task with the specified action and
    //     creation options.
    //
    // Parameters:
    //   action:
    //     The delegate that represents the code to execute in the task.
    //
    //   creationOptions:
    //     The System.Threading.Tasks.TaskCreationOptions used to customize the Task's
    //     behavior.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The action argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The creationOptions argument specifies an invalid value for System.Threading.Tasks.TaskCreationOptions.
    public Task(Action action, TaskCreationOptions creationOptions)
    {
      Contract.Requires(action != null);

      Contract.Ensures(this.CreationOptions == creationOptions);

    }
    //
    // Summary:
    //     Initializes a new System.Threading.Tasks.Task with the specified action,
    //     state, and options.
    //
    // Parameters:
    //   action:
    //     The delegate that represents the code to execute in the task.
    //
    //   state:
    //     An object representing data to be used by the action.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.Task.CancellationToken that that the new task
    //     will observe..
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The action argument is null.
    public Task(Action<object> action, object state, CancellationToken cancellationToken)
    {
      Contract.Requires(action != null);
    }
    //
    // Summary:
    //     Initializes a new System.Threading.Tasks.Task with the specified action,
    //     state, and options.
    //
    // Parameters:
    //   action:
    //     The delegate that represents the code to execute in the task.
    //
    //   state:
    //     An object representing data to be used by the action.
    //
    //   creationOptions:
    //     The System.Threading.Tasks.TaskCreationOptions used to customize the Task's
    //     behavior.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The action argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The creationOptions argument specifies an invalid value for System.Threading.Tasks.TaskCreationOptions.
    public Task(Action<object> action, object state, TaskCreationOptions creationOptions)
    {
      Contract.Requires(action != null);

      Contract.Ensures(this.CreationOptions == creationOptions);
    }
    //
    // Summary:
    //     Initializes a new System.Threading.Tasks.Task with the specified action and
    //     creation options.
    //
    // Parameters:
    //   action:
    //     The delegate that represents the code to execute in the task.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.Task.CancellationToken that the new task will
    //     observe.
    //
    //   creationOptions:
    //     The System.Threading.Tasks.TaskCreationOptions used to customize the Task's
    //     behavior.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The action argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The creationOptions argument specifies an invalid value for System.Threading.Tasks.TaskCreationOptions.
    public Task(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
    {
      Contract.Requires(action != null);

      Contract.Ensures(this.CreationOptions == creationOptions);
    }
    //
    // Summary:
    //     Initializes a new System.Threading.Tasks.Task with the specified action,
    //     state, and options.
    //
    // Parameters:
    //   action:
    //     The delegate that represents the code to execute in the task.
    //
    //   state:
    //     An object representing data to be used by the action.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.Task.CancellationToken that that the new task
    //     will observe..
    //
    //   creationOptions:
    //     The System.Threading.Tasks.TaskCreationOptions used to customize the Task's
    //     behavior.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The action argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The creationOptions argument specifies an invalid value for System.Threading.Tasks.TaskCreationOptions.
    public Task(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
    {
      Contract.Requires(action != null);

      Contract.Ensures(this.CreationOptions == creationOptions);
    }

    // Summary:
    //     Gets the state object supplied when the System.Threading.Tasks.Task was created,
    //     or null if none was supplied.
    //
    // Returns:
    //     An System.Object that represents the state data that was passed in to the
    //     task when it was created.
    //public object AsyncState { get; }
    //
    // Summary:
    //     Gets the System.Threading.Tasks.TaskCreationOptions used to create this task.
    //
    // Returns:
    //     The System.Threading.Tasks.TaskCreationOptions used to create this task.
    public TaskCreationOptions CreationOptions { get { return default(TaskCreationOptions); } }
    //
    // Summary:
    //     Returns the unique ID of the currently executing System.Threading.Tasks.Task.
    //
    // Returns:
    //     An integer that was assigned by the system to the currently-executing task.
    //public static int? CurrentId { get; }
    //
    // Summary:
    //     Gets the System.AggregateException that caused the System.Threading.Tasks.Task
    //     to end prematurely. If the System.Threading.Tasks.Task completed successfully
    //     or has not yet thrown any exceptions, this will return null.
    //
    // Returns:
    //     The System.AggregateException that caused the System.Threading.Tasks.Task
    //     to end prematurely.
    //public AggregateException Exception { get; }
    //
    // Summary:
    //     Provides access to factory methods for creating System.Threading.Tasks.Task
    //     and System.Threading.Tasks.Task<TResult> instances.
    //
    // Returns:
    //     The default System.Threading.Tasks.TaskFactory for the current task.
    public static TaskFactory Factory 
    { 
      get
      {
        Contract.Ensures(Contract.Result<TaskFactory>() != null);
        return null;
      }
    }
    //
    // Summary:
    //     Gets a unique ID for this System.Threading.Tasks.Task instance.
    //
    // Returns:
    //     An integer that was assigned by the system to this task instance.
    //public int Id { get; }
    //
    // Summary:
    //     Gets whether this System.Threading.Tasks.Task instance has completed execution
    //     due to being canceled.
    //
    // Returns:
    //     true if the task has completed due to being canceled; otherwise false.
    //public bool IsCanceled { get; }
    //
    // Summary:
    //     Gets whether this System.Threading.Tasks.Task has completed.
    //
    // Returns:
    //     true if the task has completed; otherwise false.
    //public bool IsCompleted { get; }
    //
    // Summary:
    //     Gets whether the System.Threading.Tasks.Task completed due to an unhandled
    //     exception.
    //
    // Returns:
    //     true if the task has thrown an unhandled exception; otherwise false.
    //public bool IsFaulted { get; }
    //
    // Summary:
    //     Gets the System.Threading.Tasks.TaskStatus of this Task.
    //
    // Returns:
    //     The current System.Threading.Tasks.TaskStatus of this task instance.
    //public TaskStatus Status { get; }

    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task
    //     completes.
    //
    // Parameters:
    //   continuationAction:
    //     An action to run when the System.Threading.Tasks.Task completes. When run,
    //     the delegate will be passed the completed task as an argument.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationAction argument is null.
    public Task ContinueWith(Action<Task> continuationAction)
    {
      Contract.Requires(continuationAction != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task
    //     completes.
    //
    // Parameters:
    //   continuationFunction:
    //     A function to run when the System.Threading.Tasks.Task completes. When run,
    //     the delegate will be passed the completed task as an argument.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result produced by the continuation.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task<TResult>.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationFunction argument is null.
    public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction)
    {
      Contract.Requires(continuationFunction != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
    //
    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task
    //     completes.
    //
    // Parameters:
    //   continuationAction:
    //     An action to run when the System.Threading.Tasks.Task completes. When run,
    //     the delegate will be passed the completed task as an argument.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.Task.CancellationToken that will be assigned to
    //     the new continuation task.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.-or-The System.Threading.CancellationTokenSource
    //     that created the token has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationAction argument is null.
    public Task ContinueWith(Action<Task> continuationAction, CancellationToken cancellationToken)
    {
      Contract.Requires(continuationAction != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a continuation that executes according to the specified System.Threading.Tasks.TaskContinuationOptions.
    //
    // Parameters:
    //   continuationAction:
    //     An action to run when the System.Threading.Tasks.Task completes. When run,
    //     the delegate will be passed the completed task as an argument.
    //
    //   continuationOptions:
    //     Options for when the continuation is scheduled and how it behaves. This includes
    //     criteria, such as System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled,
    //     as well as execution options, such as System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationAction argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The continuationOptions argument specifies an invalid value for System.Threading.Tasks.TaskContinuationOptions.
    public Task ContinueWith(Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
    {
      Contract.Requires(continuationAction != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task
    //     completes.
    //
    // Parameters:
    //   continuationAction:
    //     An action to run when the System.Threading.Tasks.Task completes. When run,
    //     the delegate will be passed the completed task as an argument.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler to associate with the continuation
    //     task and to use for its execution.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationAction argument is null.-or-The scheduler argument is null.
    public Task ContinueWith(Action<Task> continuationAction, TaskScheduler scheduler)
    {
      Contract.Requires(continuationAction != null);
      Contract.Requires(scheduler != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task
    //     completes.
    //
    // Parameters:
    //   continuationFunction:
    //     A function to run when the System.Threading.Tasks.Task completes. When run,
    //     the delegate will be passed the completed task as an argument.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.Task.CancellationToken that will be assigned to
    //     the new continuation task.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result produced by the continuation.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task<TResult>.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.-or-The System.Threading.CancellationTokenSource
    //     that created the token has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationFunction argument is null.
    public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
    {
      Contract.Requires(continuationFunction != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
    //
    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task
    //     completes.
    //
    // Parameters:
    //   continuationFunction:
    //     A function to run when the System.Threading.Tasks.Task completes. When run,
    //     the delegate will be passed the completed task as an argument.
    //
    //   continuationOptions:
    //     Options for when the continuation is scheduled and how it behaves. This includes
    //     criteria, such as System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled,
    //     as well as execution options, such as System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result produced by the continuation.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task<TResult>.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationFunction argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The continuationOptions argument specifies an invalid value for System.Threading.Tasks.TaskContinuationOptions.
    public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      Contract.Requires(continuationFunction != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
    //
    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task
    //     completes.
    //
    // Parameters:
    //   continuationFunction:
    //     A function to run when the System.Threading.Tasks.Task completes. When run,
    //     the delegate will be passed the completed task as an argument.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler to associate with the continuation
    //     task and to use for its execution.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result produced by the continuation.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task<TResult>.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationFunction argument is null.-or-The scheduler argument is null.
    public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskScheduler scheduler)
    {
      Contract.Requires(continuationFunction != null);
      Contract.Requires(scheduler != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
    //
    // Summary:
    //     Creates a continuation that executes according to the specified System.Threading.Tasks.TaskContinuationOptions.
    //
    // Parameters:
    //   continuationAction:
    //     An action to run when the System.Threading.Tasks.Task completes. When run,
    //     the delegate will be passed the completed task as an argument.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.Task.CancellationToken that will be assigned to
    //     the new continuation task.
    //
    //   continuationOptions:
    //     Options for when the continuation is scheduled and how it behaves. This includes
    //     criteria, such as System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled,
    //     as well as execution options, such as System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler to associate with the continuation
    //     task and to use for its execution.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.-or-The System.Threading.CancellationTokenSource
    //     that created the token has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationAction argument is null.-or-The scheduler argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The continuationOptions argument specifies an invalid value for System.Threading.Tasks.TaskContinuationOptions.
    public Task ContinueWith(Action<Task> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(continuationAction != null);
      Contract.Requires(scheduler != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task
    //     completes.
    //
    // Parameters:
    //   continuationFunction:
    //     A function to run when the System.Threading.Tasks.Task completes. When run,
    //     the delegate will be passed the completed task as an argument.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.Task.CancellationToken that will be assigned to
    //     the new continuation task.
    //
    //   continuationOptions:
    //     Options for when the continuation is scheduled and how it behaves. This includes
    //     criteria, such as System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled,
    //     as well as execution options, such as System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler to associate with the continuation
    //     task and to use for its execution.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result produced by the continuation.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task<TResult>.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.-or-The System.Threading.CancellationTokenSource
    //     that created the token has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationFunction argument is null.-or-The scheduler argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The continuationOptions argument specifies an invalid value for System.Threading.Tasks.TaskContinuationOptions.
    public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(continuationFunction != null);
      Contract.Requires(scheduler != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);

    }
    //
    // Summary:
    //     Releases all resources used by the current instance of the System.Threading.Tasks.Task
    //     class.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The exception that is thrown if the System.Threading.Tasks.Task is not in
    //     one of the final states: System.Threading.Tasks.TaskStatus.RanToCompletion,
    //     System.Threading.Tasks.TaskStatus.Faulted, or System.Threading.Tasks.TaskStatus.Canceled.
    //public void Dispose();
    //
    // Summary:
    //     Disposes the System.Threading.Tasks.Task, releasing all of its unmanaged
    //     resources.
    //
    // Parameters:
    //   disposing:
    //     A Boolean value that indicates whether this method is being called due to
    //     a call to System.Threading.Tasks.Task.Dispose().
    //protected virtual void Dispose(bool disposing);
    //
    // Summary:
    //     Runs the System.Threading.Tasks.Task synchronously on the current System.Threading.Tasks.TaskScheduler.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task instance has been disposed.
    //
    //   System.InvalidOperationException:
    //     The System.Threading.Tasks.Task is not in a valid state to be started. It
    //     may have already been started, executed, or canceled, or it may have been
    //     created in a manner that doesn't support direct scheduling.
    //public void RunSynchronously();
    //
    // Summary:
    //     Runs the System.Threading.Tasks.Task synchronously on the System.Threading.Tasks.TaskScheduler
    //     provided.
    //
    // Parameters:
    //   scheduler:
    //     The scheduler on which to attempt to run this task inline.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task instance has been disposed.
    //
    //   System.ArgumentNullException:
    //     The scheduler parameter is null.
    //
    //   System.InvalidOperationException:
    //     The System.Threading.Tasks.Task is not in a valid state to be started. It
    //     may have already been started, executed, or canceled, or it may have been
    //     created in a manner that doesn't support direct scheduling.
    public void RunSynchronously(TaskScheduler scheduler)
    {
      Contract.Requires(scheduler != null);
    }
    //
    // Summary:
    //     Starts the System.Threading.Tasks.Task, scheduling it for execution to the
    //     current System.Threading.Tasks.TaskScheduler.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task instance has been disposed.
    //
    //   System.InvalidOperationException:
    //     The System.Threading.Tasks.Task is not in a valid state to be started. It
    //     may have already been started, executed, or canceled, or it may have been
    //     created in a manner that doesn't support direct scheduling.
    //public void Start();
    //
    // Summary:
    //     Starts the System.Threading.Tasks.Task, scheduling it for execution to the
    //     specified System.Threading.Tasks.TaskScheduler.
    //
    // Parameters:
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler with which to associate and execute
    //     this task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task instance has been disposed.
    //
    //   System.ArgumentNullException:
    //     The scheduler argument is null.
    //
    //   System.InvalidOperationException:
    //     The System.Threading.Tasks.Task is not in a valid state to be started. It
    //     may have already been started, executed, or canceled, or it may have been
    //     created in a manner that doesn't support direct scheduling.
    public void Start(TaskScheduler scheduler)
    {
      Contract.Requires(scheduler != null);
    }
    //
    // Summary:
    //     Waits for the System.Threading.Tasks.Task to complete execution.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.
    //
    //   System.AggregateException:
    //     The System.Threading.Tasks.Task was canceled -or- an exception was thrown
    //     during the execution of the System.Threading.Tasks.Task. If the task was
    //     canceled, the System.AggregateException contains an System.Threading.OperationCanceledException
    //     in its System.AggregateException.InnerExceptions collection.
    //public void Wait();
    //
    // Summary:
    //     Waits for the System.Threading.Tasks.Task to complete execution.
    //
    // Parameters:
    //   cancellationToken:
    //     A System.Threading.Tasks.Task.CancellationToken to observe while waiting
    //     for the task to complete.
    //
    // Exceptions:
    //   System.OperationCanceledException:
    //     The cancellationToken was canceled.
    //
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.
    //
    //   System.AggregateException:
    //     The System.Threading.Tasks.Task was canceled -or- an exception was thrown
    //     during the execution of the System.Threading.Tasks.Task. If the task was
    //     canceled, the System.AggregateException contains an System.Threading.OperationCanceledException
    //     in its System.AggregateException.InnerExceptions collection.
    public void Wait(CancellationToken cancellationToken)
    {
    }
    //
    // Summary:
    //     Waits for the System.Threading.Tasks.Task to complete execution.
    //
    // Parameters:
    //   millisecondsTimeout:
    //     The number of milliseconds to wait, or System.Threading.Timeout.Infinite
    //     (-1) to wait indefinitely.
    //
    // Returns:
    //     true if the System.Threading.Tasks.Task completed execution within the allotted
    //     time; otherwise, false.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.
    //
    //   System.ArgumentOutOfRangeException:
    //     millisecondsTimeout is a negative number other than -1, which represents
    //     an infinite time-out.
    //
    //   System.AggregateException:
    //     The System.Threading.Tasks.Task was canceled -or- an exception was thrown
    //     during the execution of the System.Threading.Tasks.Task. If the task was
    //     canceled, the System.AggregateException contains an System.Threading.OperationCanceledException
    //     in its System.AggregateException.InnerExceptions collection.
    public bool Wait(int millisecondsTimeout)
    {
      Contract.Requires(millisecondsTimeout >= -1);

      return default(bool);
    }
    //
    // Summary:
    //     Waits for the System.Threading.Tasks.Task to complete execution.
    //
    // Parameters:
    //   timeout:
    //     A System.TimeSpan that represents the number of milliseconds to wait, or
    //     a System.TimeSpan that represents -1 milliseconds to wait indefinitely.
    //
    // Returns:
    //     true if the System.Threading.Tasks.Task completed execution within the allotted
    //     time; otherwise, false.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.
    //
    //   System.ArgumentOutOfRangeException:
    //     timeout is a negative number other than -1 milliseconds, which represents
    //     an infinite time-out -or- timeout is greater than System.Int32.MaxValue.
    //
    //   System.AggregateException:
    //     The System.Threading.Tasks.Task was canceled -or- an exception was thrown
    //     during the execution of the System.Threading.Tasks.Task.
    //public bool Wait(TimeSpan timeout);
    //
    // Summary:
    //     Waits for the System.Threading.Tasks.Task to complete execution.
    //
    // Parameters:
    //   millisecondsTimeout:
    //     The number of milliseconds to wait, or System.Threading.Timeout.Infinite
    //     (-1) to wait indefinitely.
    //
    //   cancellationToken:
    //     A System.Threading.Tasks.Task.CancellationToken to observe while waiting
    //     for the task to complete.
    //
    // Returns:
    //     true if the System.Threading.Tasks.Task completed execution within the allotted
    //     time; otherwise, false.
    //
    // Exceptions:
    //   System.OperationCanceledException:
    //     The cancellationToken was canceled.
    //
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.
    //
    //   System.ArgumentOutOfRangeException:
    //     millisecondsTimeout is a negative number other than -1, which represents
    //     an infinite time-out.
    //
    //   System.AggregateException:
    //     The System.Threading.Tasks.Task was canceled -or- an exception was thrown
    //     during the execution of the System.Threading.Tasks.Task. If the task was
    //     canceled, the System.AggregateException contains an System.Threading.OperationCanceledException
    //     in its System.AggregateException.InnerExceptions collection.
    public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
    {
      Contract.Requires(millisecondsTimeout >= -1);

      return(default(bool));
    }
    //
    // Summary:
    //     Waits for all of the provided System.Threading.Tasks.Task objects to complete
    //     execution.
    //
    // Parameters:
    //   tasks:
    //     An array of System.Threading.Tasks.Task instances on which to wait.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     One or more of the System.Threading.Tasks.Task objects in tasks has been
    //     disposed.
    //
    //   System.ArgumentNullException:
    //     The tasks argument is null.-or-The tasks argument contains a null element.
    //
    //   System.AggregateException:
    //     At least one of the System.Threading.Tasks.Task instances was canceled -or-
    //     an exception was thrown during the execution of at least one of the System.Threading.Tasks.Task
    //     instances. If a task was canceled, the System.AggregateException contains
    //     an System.Threading.OperationCanceledException in its System.AggregateException.InnerExceptions
    //     collection.
    public static void WaitAll(params Task[] tasks)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
    }
    //
    // Summary:
    //     Waits for all of the provided System.Threading.Tasks.Task objects to complete
    //     execution.
    //
    // Parameters:
    //   tasks:
    //     An array of System.Threading.Tasks.Task instances on which to wait.
    //
    //   cancellationToken:
    //     A System.Threading.Tasks.Task.CancellationToken to observe while waiting
    //     for the tasks to complete.
    //
    // Exceptions:
    //   System.OperationCanceledException:
    //     The cancellationToken was canceled.
    //
    //   System.ArgumentNullException:
    //     The tasks argument is null.
    //
    //   System.AggregateException:
    //     At least one of the System.Threading.Tasks.Task instances was canceled -or-
    //     an exception was thrown during the execution of at least one of the System.Threading.Tasks.Task
    //     instances. If a task was canceled, the System.AggregateException contains
    //     an System.Threading.OperationCanceledException in its System.AggregateException.InnerExceptions
    //     collection.
    //
    //   System.ArgumentException:
    //     The tasks argument contains a null element.
    //
    //   System.ObjectDisposedException:
    //     One or more of the System.Threading.Tasks.Task objects in tasks has been
    //     disposed.
    public static void WaitAll(Task[] tasks, CancellationToken cancellationToken)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

    }
    //
    // Summary:
    //     Waits for all of the provided System.Threading.Tasks.Task objects to complete
    //     execution.
    //
    // Parameters:
    //   tasks:
    //     An array of System.Threading.Tasks.Task instances on which to wait.
    //
    //   millisecondsTimeout:
    //     The number of milliseconds to wait, or System.Threading.Timeout.Infinite
    //     (-1) to wait indefinitely.
    //
    // Returns:
    //     true if all of the System.Threading.Tasks.Task instances completed execution
    //     within the allotted time; otherwise, false.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     One or more of the System.Threading.Tasks.Task objects in tasks has been
    //     disposed.
    //
    //   System.ArgumentNullException:
    //     The tasks argument is null.
    //
    //   System.AggregateException:
    //     At least one of the System.Threading.Tasks.Task instances was canceled -or-
    //     an exception was thrown during the execution of at least one of the System.Threading.Tasks.Task
    //     instances. If a task was canceled, the System.AggregateException contains
    //     an System.Threading.OperationCanceledException in its System.AggregateException.InnerExceptions
    //     collection.
    //
    //   System.ArgumentOutOfRangeException:
    //     millisecondsTimeout is a negative number other than -1, which represents
    //     an infinite time-out.
    //
    //   System.ArgumentException:
    //     The tasks argument contains a null element.
    public static bool WaitAll(Task[] tasks, int millisecondsTimeout)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
      Contract.Requires(millisecondsTimeout >= -1);

      return default(bool);
    }
    //
    // Summary:
    //     Waits for all of the provided System.Threading.Tasks.Task objects to complete
    //     execution.
    //
    // Parameters:
    //   tasks:
    //     An array of System.Threading.Tasks.Task instances on which to wait.
    //
    //   timeout:
    //     A System.TimeSpan that represents the number of milliseconds to wait, or
    //     a System.TimeSpan that represents -1 milliseconds to wait indefinitely.
    //
    // Returns:
    //     true if all of the System.Threading.Tasks.Task instances completed execution
    //     within the allotted time; otherwise, false.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     One or more of the System.Threading.Tasks.Task objects in tasks has been
    //     disposed.
    //
    //   System.ArgumentNullException:
    //     The tasks argument is null.
    //
    //   System.AggregateException:
    //     At least one of the System.Threading.Tasks.Task instances was canceled -or-
    //     an exception was thrown during the execution of at least one of the System.Threading.Tasks.Task
    //     instances. If a task was canceled, the System.AggregateException contains
    //     an System.Threading.OperationCanceledException in its System.AggregateException.InnerExceptions
    //     collection.
    //
    //   System.ArgumentOutOfRangeException:
    //     timeout is a negative number other than -1 milliseconds, which represents
    //     an infinite time-out -or- timeout is greater than System.Int32.MaxValue.
    //
    //   System.ArgumentException:
    //     The tasks argument contains a null element.
    public static bool WaitAll(Task[] tasks, TimeSpan timeout)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      return default(bool);
    }
    //
    // Summary:
    //     Waits for all of the provided System.Threading.Tasks.Task objects to complete
    //     execution.
    //
    // Parameters:
    //   tasks:
    //     An array of System.Threading.Tasks.Task instances on which to wait.
    //
    //   millisecondsTimeout:
    //     The number of milliseconds to wait, or System.Threading.Timeout.Infinite
    //     (-1) to wait indefinitely.
    //
    //   cancellationToken:
    //     A System.Threading.Tasks.Task.CancellationToken to observe while waiting
    //     for the tasks to complete.
    //
    // Returns:
    //     true if all of the System.Threading.Tasks.Task instances completed execution
    //     within the allotted time; otherwise, false.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     One or more of the System.Threading.Tasks.Task objects in tasks has been
    //     disposed.
    //
    //   System.ArgumentNullException:
    //     The tasks argument is null.
    //
    //   System.AggregateException:
    //     At least one of the System.Threading.Tasks.Task instances was canceled -or-
    //     an exception was thrown during the execution of at least one of the System.Threading.Tasks.Task
    //     instances. If a task was canceled, the System.AggregateException contains
    //     an System.Threading.OperationCanceledException in its System.AggregateException.InnerExceptions
    //     collection.
    //
    //   System.ArgumentOutOfRangeException:
    //     millisecondsTimeout is a negative number other than -1, which represents
    //     an infinite time-out.
    //
    //   System.ArgumentException:
    //     The tasks argument contains a null element.
    //
    //   System.OperationCanceledException:
    //     The cancellationToken was canceled.
    public static bool WaitAll(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
      Contract.Requires(millisecondsTimeout >= -1);

      return default(bool);

    }
    //
    // Summary:
    //     Waits for any of the provided System.Threading.Tasks.Task objects to complete
    //     execution.
    //
    // Parameters:
    //   tasks:
    //     An array of System.Threading.Tasks.Task instances on which to wait.
    //
    // Returns:
    //     The index of the completed task in the tasks array argument.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.
    //
    //   System.ArgumentNullException:
    //     The tasks argument is null.
    //
    //   System.ArgumentException:
    //     The tasks argument contains a null element.
    public static int WaitAny(params Task[] tasks)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() < tasks.Length);

      return default(int);
    }
    //
    // Summary:
    //     Waits for any of the provided System.Threading.Tasks.Task objects to complete
    //     execution.
    //
    // Parameters:
    //   tasks:
    //     An array of System.Threading.Tasks.Task instances on which to wait.
    //
    //   cancellationToken:
    //     A System.Threading.Tasks.Task.CancellationToken to observe while waiting
    //     for a task to complete.
    //
    // Returns:
    //     The index of the completed task in the tasks array argument.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.
    //
    //   System.ArgumentNullException:
    //     The tasks argument is null.
    //
    //   System.ArgumentException:
    //     The tasks argument contains a null element.
    //
    //   System.OperationCanceledException:
    //     The cancellationToken was canceled.
    public static int WaitAny(Task[] tasks, CancellationToken cancellationToken)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() < tasks.Length);

      return default(int);

    }
    //
    // Summary:
    //     Waits for any of the provided System.Threading.Tasks.Task objects to complete
    //     execution.
    //
    // Parameters:
    //   tasks:
    //     An array of System.Threading.Tasks.Task instances on which to wait.
    //
    //   millisecondsTimeout:
    //     The number of milliseconds to wait, or System.Threading.Timeout.Infinite
    //     (-1) to wait indefinitely.
    //
    // Returns:
    //     The index of the completed task in the tasks array argument, or -1 if the
    //     timeout occurred.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.
    //
    //   System.ArgumentNullException:
    //     The tasks argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     millisecondsTimeout is a negative number other than -1, which represents
    //     an infinite time-out.
    //
    //   System.ArgumentException:
    //     The tasks argument contains a null element.
    public static int WaitAny(Task[] tasks, int millisecondsTimeout)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
      Contract.Requires(millisecondsTimeout >= -1);

      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() < tasks.Length);

      return default(int);
     }
    //
    // Summary:
    //     Waits for any of the provided System.Threading.Tasks.Task objects to complete
    //     execution.
    //
    // Parameters:
    //   tasks:
    //     An array of System.Threading.Tasks.Task instances on which to wait.
    //
    //   timeout:
    //     A System.TimeSpan that represents the number of milliseconds to wait, or
    //     a System.TimeSpan that represents -1 milliseconds to wait indefinitely.
    //
    // Returns:
    //     The index of the completed task in the tasks array argument, or -1 if the
    //     timeout occurred.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.
    //
    //   System.ArgumentNullException:
    //     The tasks argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     timeout is a negative number other than -1 milliseconds, which represents
    //     an infinite time-out -or- timeout is greater than System.Int32.MaxValue.
    //
    //   System.ArgumentException:
    //     The tasks argument contains a null element.
    public static int WaitAny(Task[] tasks, TimeSpan timeout)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
     
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() < tasks.Length);

      return default(int);
    }
    //
    // Summary:
    //     Waits for any of the provided System.Threading.Tasks.Task objects to complete
    //     execution.
    //
    // Parameters:
    //   tasks:
    //     An array of System.Threading.Tasks.Task instances on which to wait.
    //
    //   millisecondsTimeout:
    //     The number of milliseconds to wait, or System.Threading.Timeout.Infinite
    //     (-1) to wait indefinitely.
    //
    //   cancellationToken:
    //     A System.Threading.Tasks.Task.CancellationToken to observe while waiting
    //     for a task to complete.
    //
    // Returns:
    //     The index of the completed task in the tasks array argument, or -1 if the
    //     timeout occurred.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task has been disposed.
    //
    //   System.ArgumentNullException:
    //     The tasks argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     millisecondsTimeout is a negative number other than -1, which represents
    //     an infinite time-out.
    //
    //   System.ArgumentException:
    //     The tasks argument contains a null element.
    //
    //   System.OperationCanceledException:
    //     The cancellationToken was canceled.
    public static int WaitAny(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
      Contract.Requires(millisecondsTimeout >= -1);

      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() < tasks.Length);

      return default(int);
    }

#if NETFRAMEWORK_4_5
    //
    // Summary:
    //     Queues the specified work to run on the ThreadPool and returns a Task handle
    //     for that work.
    //
    // Parameters:
    //   action:
    //     The work to execute asynchronously
    //
    // Returns:
    //     A Task that represents the work queued to execute in the ThreadPool.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The action parameter was null.
    public static Task Run(Action action)
    {
      Contract.Requires(action != null);
      Contract.Ensures(Contract.Result<Task>() != null);

      return null;
    }
    //
    // Summary:
    //     Queues the specified work to run on the ThreadPool and returns a proxy for
    //     the Task(TResult) returned by function.
    //
    // Parameters:
    //   function:
    //     The work to execute asynchronously
    //
    // Type parameters:
    //   TResult:
    //     The type of the result returned by the proxy Task.
    //
    // Returns:
    //     A Task(TResult) that represents a proxy for the Task(TResult) returned by
    //     function.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The function parameter was null.
    public static Task<TResult> Run<TResult>(Func<Task<TResult>> function)
    {
      Contract.Requires(function != null);
      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return null;
    }
    //
    // Summary:
    //     Queues the specified work to run on the ThreadPool and returns a proxy for
    //     the Task returned by function.
    //
    // Parameters:
    //   function:
    //     The work to execute asynchronously
    //
    // Returns:
    //     A Task that represents a proxy for the Task returned by function.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The function parameter was null.
    public static Task Run(Func<Task> function)
    {
      Contract.Requires(function != null);
      Contract.Ensures(Contract.Result<Task>() != null);

      return null;

    }
    //
    // Summary:
    //     Queues the specified work to run on the ThreadPool and returns a Task(TResult)
    //     handle for that work.
    //
    // Parameters:
    //   function:
    //     The work to execute asynchronously
    //
    // Type parameters:
    //   TResult:
    //     The result type of the task.
    //
    // Returns:
    //     A Task(TResult) that represents the work queued to execute in the ThreadPool.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The function parameter was null.
    public static Task<TResult> Run<TResult>(Func<TResult> function)
    {
      Contract.Requires(function != null);
      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return null;

    }
    //
    // Summary:
    //     Queues the specified work to run on the ThreadPool and returns a Task handle
    //     for that work.
    //
    // Parameters:
    //   action:
    //     The work to execute asynchronously
    //
    //   cancellationToken:
    //     A cancellation token that should be used to cancel the work
    //
    // Returns:
    //     A Task that represents the work queued to execute in the ThreadPool.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The action parameter was null.
    //
    //   System.ObjectDisposedException:
    //     The System.Threading.CancellationTokenSource associated with cancellationToken
    //     was disposed.
    public static Task Run(Action action, CancellationToken cancellationToken)
    {
      Contract.Requires(action != null);
      Contract.Ensures(Contract.Result<Task>() != null);

      return null;

    }
    //
    // Summary:
    //     Queues the specified work to run on the ThreadPool and returns a proxy for
    //     the Task(TResult) returned by function.
    //
    // Parameters:
    //   function:
    //     The work to execute asynchronously
    //
    //   cancellationToken:
    //     A cancellation token that should be used to cancel the work
    //
    // Type parameters:
    //   TResult:
    //     The type of the result returned by the proxy Task.
    //
    // Returns:
    //     A Task(TResult) that represents a proxy for the Task(TResult) returned by
    //     function.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The function parameter was null.
    //
    //   System.ObjectDisposedException:
    //     The System.Threading.CancellationTokenSource associated with cancellationToken
    //     was disposed.
    public static Task<TResult> Run<TResult>(Func<Task<TResult>> function, CancellationToken cancellationToken)
    {
      Contract.Requires(function != null);
      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return null;

    }
    //
    // Summary:
    //     Queues the specified work to run on the ThreadPool and returns a proxy for
    //     the Task returned by function.
    //
    // Parameters:
    //   function:
    //     The work to execute asynchronously
    //
    //   cancellationToken:
    //     A cancellation token that should be used to cancel the work
    //
    // Returns:
    //     A Task that represents a proxy for the Task returned by function.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The function parameter was null.
    //
    //   System.ObjectDisposedException:
    //     The System.Threading.CancellationTokenSource associated with cancellationToken
    //     was disposed.
    public static Task Run(Func<Task> function, CancellationToken cancellationToken)
    {
      Contract.Requires(function != null);
      Contract.Ensures(Contract.Result<Task>() != null);

      return null;

    }
    //
    // Summary:
    //     Queues the specified work to run on the ThreadPool and returns a Task(TResult)
    //     handle for that work.
    //
    // Parameters:
    //   function:
    //     The work to execute asynchronously
    //
    //   cancellationToken:
    //     A cancellation token that should be used to cancel the work
    //
    // Type parameters:
    //   TResult:
    //     The result type of the task.
    //
    // Returns:
    //     A Task(TResult) that represents the work queued to execute in the ThreadPool.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The function parameter was null.
    //
    //   System.ObjectDisposedException:
    //     The System.Threading.CancellationTokenSource associated with cancellationToken
    //     was disposed.
    public static Task<TResult> Run<TResult>(Func<TResult> function, CancellationToken cancellationToken)
    {
      Contract.Requires(function != null);
      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return null;

    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task`1 that's completed successfully with the
    //     specified result.
    //
    // Parameters:
    //   result:
    //     The result to store into the completed task.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result returned by the task.
    //
    // Returns:
    //     The successfully completed task.
    public static Task<TResult> FromResult<TResult>(TResult result)
    {
      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
#endif

#if NETFRAMEWORK_4_6
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task that's completed due to cancellation with
    //     a specified cancellation token.
    //
    // Parameters:
    //   cancellationToken:
    //     The cancellation token with which to complete the task.
    //
    // Returns:
    //     The canceled task.
    public static Task FromCanceled(CancellationToken cancellationToken)
    {
      Contract.Requires(cancellationToken.IsCancellationRequested);
      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task`1 that's completed due to cancellation
    //     with a specified cancellation token.
    //
    // Parameters:
    //   cancellationToken:
    //     The cancellation token with which to complete the task.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result returned by the task.
    //
    // Returns:
    //     The canceled task.
    public static Task<TResult> FromCanceled<TResult>(CancellationToken cancellationToken)
    {
      Contract.Requires(cancellationToken.IsCancellationRequested);
      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task that has completed with a specified exception.
    //
    // Parameters:
    //   exception:
    //     The exception with which to complete the task.
    //
    // Returns:
    //     The faulted task.
    public static Task FromException(Exception exception)
	{
	  Contract.Requires(exception != null);
	  Contract.Ensures(Contract.Result<Task>() != null);

	  return default(Task);
	}
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task`1 that's completed with a specified exception.
    //
    // Parameters:
    //   exception:
    //     The exception with which to complete the task.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result returned by the task.
    //
    // Returns:
    //     The faulted task.
    public static Task<TResult> FromException<TResult>(Exception exception)
    {
      Contract.Requires(exception != null);
      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
    //
    // Summary:
    //     Gets a task that has already completed successfully.
    //
    // Returns:
    //     The successfully completed task.
    public static Task CompletedTask
    {
      get
      {
        Contract.Ensures(Contract.Result<Task>() != null);

        return default(Task);
      }
    }
#endif
  }

  // Summary:
  //     Represents an asynchronous operation that can return a value.
  //
  // Type parameters:
  //   TResult:
  //     The type of the result produced by this System.Threading.Tasks.Task<TResult>.
  //[DebuggerTypeProxy(typeof(SystemThreadingTasks_FutureDebugView<>))]
  //[DebuggerDisplay("Id = {Id}, Status = {Status}, Method = {DebuggerDisplayMethodDescription}, Result = {DebuggerDisplayResultDescription}")]
  public class Task<TResult> : Task
  {
    // Summary:
    //     Initializes a new System.Threading.Tasks.Task<TResult> with the specified
    //     function.
    //
    // Parameters:
    //   function:
    //     The delegate that represents the code to execute in the task. When the function
    //     has completed, the task's System.Threading.Tasks.Task<TResult>.Result property
    //     will be set to return the result value of the function.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The function argument is null.
    public Task(Func<TResult> function)
    {
      Contract.Requires(function != null);
    }
    //
    // Summary:
    //     Initializes a new System.Threading.Tasks.Task<TResult> with the specified
    //     function and state.
    //
    // Parameters:
    //   function:
    //     The delegate that represents the code to execute in the task. When the function
    //     has completed, the task's System.Threading.Tasks.Task<TResult>.Result property
    //     will be set to return the result value of the function.
    //
    //   state:
    //     An object representing data to be used by the action.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The function argument is null.
    public Task(Func<object, TResult> function, object state)
    {
      Contract.Requires(null != null);
    }
    //
    // Summary:
    //     Initializes a new System.Threading.Tasks.Task<TResult> with the specified
    //     function.
    //
    // Parameters:
    //   function:
    //     The delegate that represents the code to execute in the task. When the function
    //     has completed, the task's System.Threading.Tasks.Task<TResult>.Result property
    //     will be set to return the result value of the function.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken to be assigned to this task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The function argument is null.
    public Task(Func<TResult> function, CancellationToken cancellationToken)
    {
      Contract.Requires(function != null);
    }
    //
    // Summary:
    //     Initializes a new System.Threading.Tasks.Task<TResult> with the specified
    //     function and creation options.
    //
    // Parameters:
    //   function:
    //     The delegate that represents the code to execute in the task. When the function
    //     has completed, the task's System.Threading.Tasks.Task<TResult>.Result property
    //     will be set to return the result value of the function.
    //
    //   creationOptions:
    //     The System.Threading.Tasks.TaskCreationOptions used to customize the task's
    //     behavior.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The creationOptions argument specifies an invalid value for System.Threading.Tasks.TaskCreationOptions.
    //
    //   System.ArgumentNullException:
    //     The function argument is null.
    public Task(Func<TResult> function, TaskCreationOptions creationOptions)
    {
      Contract.Requires(function != null);
    }
    //
    // Summary:
    //     Initializes a new System.Threading.Tasks.Task<TResult> with the specified
    //     action, state, and options.
    //
    // Parameters:
    //   function:
    //     The delegate that represents the code to execute in the task. When the function
    //     has completed, the task's System.Threading.Tasks.Task<TResult>.Result property
    //     will be set to return the result value of the function.
    //
    //   state:
    //     An object representing data to be used by the function.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken to be assigned to the new task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The function argument is null.
    public Task(Func<object, TResult> function, object state, CancellationToken cancellationToken)
    {
      Contract.Requires(function != null);
    }
    //
    // Summary:
    //     Initializes a new System.Threading.Tasks.Task<TResult> with the specified
    //     action, state, and options.
    //
    // Parameters:
    //   function:
    //     The delegate that represents the code to execute in the task. When the function
    //     has completed, the task's System.Threading.Tasks.Task<TResult>.Result property
    //     will be set to return the result value of the function.
    //
    //   state:
    //     An object representing data to be used by the function.
    //
    //   creationOptions:
    //     The System.Threading.Tasks.TaskCreationOptions used to customize the task's
    //     behavior.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The creationOptions argument specifies an invalid value for System.Threading.Tasks.TaskCreationOptions.
    //
    //   System.ArgumentNullException:
    //     The function argument is null.
    public Task(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
    {
      Contract.Requires(function != null);
    }
    //
    // Summary:
    //     Initializes a new System.Threading.Tasks.Task<TResult> with the specified
    //     function and creation options.
    //
    // Parameters:
    //   function:
    //     The delegate that represents the code to execute in the task. When the function
    //     has completed, the task's System.Threading.Tasks.Task<TResult>.Result property
    //     will be set to return the result value of the function.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new task.
    //
    //   creationOptions:
    //     The System.Threading.Tasks.TaskCreationOptions used to customize the task's
    //     behavior.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentOutOfRangeException:
    //     The creationOptions argument specifies an invalid value for System.Threading.Tasks.TaskCreationOptions.
    //
    //   System.ArgumentNullException:
    //     The function argument is null.
    public Task(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
    {
      Contract.Requires(function != null);
    }
    //
    // Summary:
    //     Initializes a new System.Threading.Tasks.Task<TResult> with the specified
    //     action, state, and options.
    //
    // Parameters:
    //   function:
    //     The delegate that represents the code to execute in the task. When the function
    //     has completed, the task's System.Threading.Tasks.Task<TResult>.Result property
    //     will be set to return the result value of the function.
    //
    //   state:
    //     An object representing data to be used by the function.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken to be assigned to the new task.
    //
    //   creationOptions:
    //     The System.Threading.Tasks.TaskCreationOptions used to customize the task's
    //     behavior.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentOutOfRangeException:
    //     The creationOptions argument specifies an invalid value for System.Threading.Tasks.TaskCreationOptions.
    //
    //   System.ArgumentNullException:
    //     The function argument is null.
    public Task(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
    {
      Contract.Requires(function != null);
    }

    // Summary:
    //     Provides access to factory methods for creating System.Threading.Tasks.Task<TResult>
    //     instances.
    //
    // Returns:
    //     A default instance of System.Threading.Tasks.TaskFactory<TResult>.
    new public static TaskFactory<TResult> Factory
    {
      get
      {
        Contract.Ensures(Contract.Result<TaskFactory<TResult>>() != null);

        return default(TaskFactory<TResult>);
      }
    }
    //
    // Summary:
    //     Gets the result value of this System.Threading.Tasks.Task<TResult>.
    //
    // Returns:
    //     The result value of this System.Threading.Tasks.Task<TResult>, which is the
    //     same type as the task's type parameter.
    //[DebuggerBrowsable(DebuggerBrowsableState.Never)]
    //public TResult Result { get; internal set; }

    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task<TResult>
    //     completes.
    //
    // Parameters:
    //   continuationAction:
    //     An action to run when the System.Threading.Tasks.Task<TResult> completes.
    //     When run, the delegate will be passed the completed task as an argument.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task<TResult> has been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationAction argument is null.
    public Task ContinueWith(Action<Task<TResult>> continuationAction)
    {
      Contract.Requires(continuationAction != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task<TResult>
    //     completes.
    //
    // Parameters:
    //   continuationFunction:
    //     A function to run when the System.Threading.Tasks.Task<TResult> completes.
    //     When run, the delegate will be passed the completed task as an argument.
    //
    // Type parameters:
    //   TNewResult:
    //     The type of the result produced by the continuation.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task<TResult>.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task<TResult> has been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationFunction argument is null.
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction)
    {
      Contract.Requires(continuationFunction != null);

      Contract.Ensures(Contract.Result<Task<TNewResult>>() != null);

      return default(Task<TNewResult>);

    }
    //
    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task<TResult>
    //     completes.
    //
    // Parameters:
    //   continuationAction:
    //     An action to run when the System.Threading.Tasks.Task<TResult> completes.
    //     When run, the delegate will be passed the completed task as an argument.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task<TResult> has been disposed.-or-The System.Threading.CancellationTokenSource
    //     that created cancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationAction argument is null.
    public Task ContinueWith(Action<Task<TResult>> continuationAction, CancellationToken cancellationToken)
    {
      Contract.Requires(continuationAction != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);

    }
    //
    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task<TResult>
    //     completes.
    //
    // Parameters:
    //   continuationAction:
    //     An action to run when the System.Threading.Tasks.Task<TResult> completes.
    //     When run, the delegate will be passed the completed task as an argument.
    //
    //   continuationOptions:
    //     Options for when the continuation is scheduled and how it behaves. This includes
    //     criteria, such as System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled,
    //     as well as execution options, such as System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task<TResult> has been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationAction argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The continuationOptions argument specifies an invalid value for System.Threading.Tasks.TaskContinuationOptions.
    public Task ContinueWith(Action<Task<TResult>> continuationAction, TaskContinuationOptions continuationOptions)
    {
      Contract.Requires(continuationAction != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task<TResult>
    //     completes.
    //
    // Parameters:
    //   continuationAction:
    //     An action to run when the System.Threading.Tasks.Task<TResult> completes.
    //     When run, the delegate will be passed the completed task as an argument.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler to associate with the continuation
    //     task and to use for its execution.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task<TResult> has been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationAction argument is null.-or-The scheduler argument is null.
    public Task ContinueWith(Action<Task<TResult>> continuationAction, TaskScheduler scheduler)
    {
      Contract.Requires(continuationAction!= null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);

    }
    //
    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task<TResult>
    //     completes.
    //
    // Parameters:
    //   continuationFunction:
    //     A function to run when the System.Threading.Tasks.Task<TResult> completes.
    //     When run, the delegate will be passed the completed task as an argument.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new task.
    //
    // Type parameters:
    //   TNewResult:
    //     The type of the result produced by the continuation.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task<TResult>.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task<TResult> has been disposed.-or-The provided
    //     System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationFunction argument is null.
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, CancellationToken cancellationToken)
    {
      Contract.Requires(continuationFunction != null);

      Contract.Ensures(Contract.Result<Task<TNewResult>>() != null);

      return default(Task<TNewResult>);
    }
    //
    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task<TResult>
    //     completes.
    //
    // Parameters:
    //   continuationFunction:
    //     A function to run when the System.Threading.Tasks.Task<TResult> completes.
    //     When run, the delegate will be passed the completed task as an argument.
    //
    //   continuationOptions:
    //     Options for when the continuation is scheduled and how it behaves. This includes
    //     criteria, such as System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled,
    //     as well as execution options, such as System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously.
    //
    // Type parameters:
    //   TNewResult:
    //     The type of the result produced by the continuation.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task<TResult>.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task<TResult> has been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationFunction argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The continuationOptions argument specifies an invalid value for System.Threading.Tasks.TaskContinuationOptions.
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      Contract.Requires(continuationFunction != null);

      Contract.Ensures(Contract.Result<Task<TNewResult>>() != null);

      return default(Task<TNewResult>);
    }
    //
    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task<TResult>
    //     completes.
    //
    // Parameters:
    //   continuationFunction:
    //     A function to run when the System.Threading.Tasks.Task<TResult> completes.
    //     When run, the delegate will be passed the completed task as an argument.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler to associate with the continuation
    //     task and to use for its execution.
    //
    // Type parameters:
    //   TNewResult:
    //     The type of the result produced by the continuation.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task<TResult>.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task<TResult> has been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationFunction argument is null.-or-The scheduler argument is null.
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskScheduler scheduler)
    {
      Contract.Requires(continuationFunction != null);
      Contract.Requires(scheduler != null);

      Contract.Ensures(Contract.Result<Task<TNewResult>>() != null);

      return default(Task<TNewResult>);

    }
    //
    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task<TResult>
    //     completes.
    //
    // Parameters:
    //   continuationAction:
    //     An action to run when the System.Threading.Tasks.Task<TResult> completes.
    //     When run, the delegate will be passed the completed task as an argument.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    //   continuationOptions:
    //     Options for when the continuation is scheduled and how it behaves. This includes
    //     criteria, such as System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled,
    //     as well as execution options, such as System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler to associate with the continuation
    //     task and to use for its execution.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task<TResult> has been disposed.-or-The System.Threading.CancellationTokenSource
    //     that created cancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationAction argument is null.-or-The scheduler argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The continuationOptions argument specifies an invalid value for System.Threading.Tasks.TaskContinuationOptions.
    public Task ContinueWith(Action<Task<TResult>> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(continuationAction!= null);
      Contract.Requires(scheduler != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a continuation that executes when the target System.Threading.Tasks.Task<TResult>
    //     completes.
    //
    // Parameters:
    //   continuationFunction:
    //     A function to run when the System.Threading.Tasks.Task<TResult> completes.
    //     When run, the delegate will be passed as an argument this completed task.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new task.
    //
    //   continuationOptions:
    //     Options for when the continuation is scheduled and how it behaves. This includes
    //     criteria, such as System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled,
    //     as well as execution options, such as System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler to associate with the continuation
    //     task and to use for its execution.
    //
    // Type parameters:
    //   TNewResult:
    //     The type of the result produced by the continuation.
    //
    // Returns:
    //     A new continuation System.Threading.Tasks.Task<TResult>.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The System.Threading.Tasks.Task<TResult> has been disposed.-or-The provided
    //     System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The continuationFunction argument is null.-or-The scheduler argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The continuationOptions argument specifies an invalid value for System.Threading.Tasks.TaskContinuationOptions.
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(continuationFunction != null);
      Contract.Requires(scheduler != null);

      Contract.Ensures(Contract.Result<Task<TNewResult>>() != null);

      return default(Task<TNewResult>);
    }
  }

}
#endif
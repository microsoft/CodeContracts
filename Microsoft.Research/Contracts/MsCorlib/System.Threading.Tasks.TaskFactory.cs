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
using System.Threading;
using System.Diagnostics.Contracts;

namespace System.Threading.Tasks
{
  // Summary:
  //     Provides support for creating and scheduling System.Threading.Tasks.Task
  //     objects.
  public class TaskFactory
  {
    // Summary:
    //     Initializes a System.Threading.Tasks.TaskFactory instance with the default
    //     configuration.
    //public TaskFactory() { }
    //
    // Summary:
    //     Initializes a System.Threading.Tasks.TaskFactory instance with the specified
    //     configuration.
    //
    // Parameters:
    //   cancellationToken:
    //     The default System.Threading.Tasks.TaskFactory.CancellationToken that will
    //     be assigned to tasks created by this System.Threading.Tasks.TaskFactory unless
    //     another CancellationToken is explicitly specified while calling the factory
    //     methods.
    //public TaskFactory(CancellationToken cancellationToken);
    //
    // Summary:
    //     Initializes a System.Threading.Tasks.TaskFactory instance with the specified
    //     configuration.
    //
    // Parameters:
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler to use to schedule any tasks created
    //     with this TaskFactory. A null value indicates that the current TaskScheduler
    //     should be used.
    //public TaskFactory(TaskScheduler scheduler);
    //
    // Summary:
    //     Initializes a System.Threading.Tasks.TaskFactory instance with the specified
    //     configuration.
    //
    // Parameters:
    //   creationOptions:
    //     The default System.Threading.Tasks.TaskCreationOptions to use when creating
    //     tasks with this TaskFactory.
    //
    //   continuationOptions:
    //     The default System.Threading.Tasks.TaskContinuationOptions to use when creating
    //     continuation tasks with this TaskFactory.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument or the continuationOptions
    //     argument specifies an invalid value.
    //public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions);
    //
    // Summary:
    //     Initializes a System.Threading.Tasks.TaskFactory instance with the specified
    //     configuration.
    //
    // Parameters:
    //   cancellationToken:
    //     The default System.Threading.Tasks.TaskFactory.CancellationToken that will
    //     be assigned to tasks created by this System.Threading.Tasks.TaskFactory unless
    //     another CancellationToken is explicitly specified while calling the factory
    //     methods.
    //
    //   creationOptions:
    //     The default System.Threading.Tasks.TaskCreationOptions to use when creating
    //     tasks with this TaskFactory.
    //
    //   continuationOptions:
    //     The default System.Threading.Tasks.TaskContinuationOptions to use when creating
    //     continuation tasks with this TaskFactory.
    //
    //   scheduler:
    //     The default System.Threading.Tasks.TaskScheduler to use to schedule any Tasks
    //     created with this TaskFactory. A null value indicates that TaskScheduler.Current
    //     should be used.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument or the continuationOptions
    //     argumentspecifies an invalid value.
    //public TaskFactory(CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions, TaskScheduler scheduler);

    // Summary:
    //     Gets the default System.Threading.CancellationToken of this TaskFactory.
    //
    // Returns:
    //     The default System.Threading.CancellationToken of this TaskFactory.
    //public CancellationToken CancellationToken { get; }
    //
    // Summary:
    //     Gets the System.Threading.Tasks.TaskCreationOptions value of this TaskFactory.
    //
    // Returns:
    //     The System.Threading.Tasks.TaskCreationOptions value of this TaskFactory.
    //public TaskContinuationOptions ContinuationOptions { get; }
    //
    // Summary:
    //     Gets the System.Threading.Tasks.TaskCreationOptions value of this TaskFactory.
    //
    // Returns:
    //     The System.Threading.Tasks.TaskCreationOptions value of this TaskFactory.
    //public TaskCreationOptions CreationOptions { get; }
    //
    // Summary:
    //     Gets the System.Threading.Tasks.TaskScheduler of this TaskFactory.
    //
    // Returns:
    //     The System.Threading.Tasks.TaskScheduler of this TaskFactory.
    //public TaskScheduler Scheduler { get; }

    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationAction:
    //     The action delegate to execute when all tasks in the tasks array have completed.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationAction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationAction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationFunction:
    //     The function delegate to execute when all tasks in the tasks array have completed.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    //   TResult:
    //     The type of the result that is returned by the continuationFunction delegate
    //     and associated with the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
    {
      {
        Contract.Requires(tasks != null);
        Contract.Requires(continuationFunction != null);
        Contract.Requires(tasks.Length > 0);
        Contract.Requires(Contract.ForAll(tasks, t => t != null));

        Contract.Ensures(Contract.Result<Task<TResult>>() != null);

        return default(Task<TResult>);
      }
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationAction:
    //     The action delegate to execute when all tasks in the tasks array have completed.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationAction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationAction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationFunction:
    //     The function delegate to execute when all tasks in the tasks array have completed.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result that is returned by the continuationFunction delegate
    //     and associated with the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationAction:
    //     The action delegate to execute when all tasks in the tasks array have completed.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.-or-The provided System.Threading.CancellationToken has
    //     already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationAction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, CancellationToken cancellationToken)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationAction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationAction:
    //     The action delegate to execute when all tasks in the tasks array have completed.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationAction argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the continuationOptions argument specifies
    //     an invalid TaskContinuationOptions value.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, TaskContinuationOptions continuationOptions)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationAction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationFunction:
    //     The function delegate to execute when all tasks in the tasks array have completed.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    //   TResult:
    //     The type of the result that is returned by the continuationFunction delegate
    //     and associated with the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.-or-The provided System.Threading.CancellationToken has
    //     already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationFunction:
    //     The function delegate to execute when all tasks in the tasks array have completed.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    //   TResult:
    //     The type of the result that is returned by the continuationFunction delegate
    //     and associated with the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the continuationOptions argument specifies
    //     an invalid TaskContinuationOptions value.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
      Contract.Requires(Enum.IsDefined(typeof(TaskContinuationOptions), continuationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }


    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationAction:
    //     The action delegate to execute when all tasks in the tasks array have completed.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.-or-The provided System.Threading.CancellationToken has
    //     already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationAction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, CancellationToken cancellationToken)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationAction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationAction:
    //     The action delegate to execute when all tasks in the tasks array have completed.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationAction argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the continuationOptions argument specifies
    //     an invalid TaskContinuationOptions value.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, TaskContinuationOptions continuationOptions)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationAction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
      Contract.Requires(Enum.IsDefined(typeof(TaskContinuationOptions), continuationOptions));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationFunction:
    //     The function delegate to execute when all tasks in the tasks array have completed.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result that is returned by the continuationFunction delegate
    //     and associated with the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.-or-The provided System.Threading.CancellationToken has
    //     already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationFunction:
    //     The function delegate to execute when all tasks in the tasks array have completed.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result that is returned by the continuationFunction delegate
    //     and associated with the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the continuationOptions argument specifies
    //     an invalid TaskContinuationOptions value.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
      Contract.Requires(Enum.IsDefined(typeof(TaskContinuationOptions), continuationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationAction:
    //     The action delegate to execute when all tasks in the tasks array have completed.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     continuation System.Threading.Tasks.Task.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationAction argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationAction != null);
      Contract.Requires(scheduler != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
      Contract.Requires(Enum.IsDefined(typeof(TaskContinuationOptions), continuationOptions));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationFunction:
    //     The function delegate to execute when all tasks in the tasks array have completed.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task{TResult}.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     continuation System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    //   TResult:
    //     The type of the result that is returned by the continuationFunction delegate
    //     and associated with the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
      Contract.Requires(Enum.IsDefined(typeof(TaskContinuationOptions), continuationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationAction:
    //     The action delegate to execute when all tasks in the tasks array have completed.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     continuation System.Threading.Tasks.Task.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationAction argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationAction != null);
      Contract.Requires(scheduler != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
      Contract.Requires(Enum.IsDefined(typeof(TaskContinuationOptions), continuationOptions));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationFunction:
    //     The function delegate to execute when all tasks in the tasks array have completed.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task{TResult}.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     continuation System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result that is returned by the continuationFunction delegate
    //     and associated with the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(scheduler != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
      Contract.Requires(Enum.IsDefined(typeof(TaskContinuationOptions), continuationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationAction:
    //     The action delegate to execute when one task in the tasks array completes.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationAction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationAction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
 
      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationFunction:
    //     The function delegate to execute when one task in the tasks array completes.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    //   TResult:
    //     The type of the result that is returned by the continuationFunction delegate
    //     and associated with the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationAction:
    //     The action delegate to execute when one task in the tasks array completes.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationAction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationAction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
 
      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationFunction:
    //     The function delegate to execute when one task in the tasks array completes.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result that is returned by the continuationFunction delegate
    //     and associated with the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationAction:
    //     The action delegate to execute when one task in the tasks array completes.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.-or-The provided System.Threading.CancellationToken has
    //     already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationAction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, CancellationToken cancellationToken)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationAction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationAction:
    //     The action delegate to execute when one task in the tasks array completes.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationAction argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the continuationOptions argument specifies
    //     an invalid TaskContinuationOptions value.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, TaskContinuationOptions continuationOptions)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationAction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationFunction:
    //     The function delegate to execute when one task in the tasks array completes.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    //   TResult:
    //     The type of the result that is returned by the continuationFunction delegate
    //     and associated with the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.-or-The provided System.Threading.CancellationToken has
    //     already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationFunction:
    //     The function delegate to execute when one task in the tasks array completes.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    //   TResult:
    //     The type of the result that is returned by the continuationFunction delegate
    //     and associated with the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the continuationOptions argument specifies
    //     an invalid TaskContinuationOptions value.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationAction:
    //     The action delegate to execute when one task in the tasks array completes.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.-or-The provided System.Threading.CancellationToken has
    //     already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationAction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, CancellationToken cancellationToken)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationAction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationAction:
    //     The action delegate to execute when one task in the tasks array completes.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationAction argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the continuationOptions argument specifies
    //     an invalid TaskContinuationOptions value.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationAction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
      Contract.Requires(Enum.IsDefined(typeof(TaskContinuationOptions), continuationOptions));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationFunction:
    //     The function delegate to execute when one task in the tasks array completes.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result that is returned by the continuationFunction delegate
    //     and associated with the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.-or-The provided System.Threading.CancellationToken has
    //     already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationFunction:
    //     The function delegate to execute when one task in the tasks array completes.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result that is returned by the continuationFunction delegate
    //     and associated with the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the continuationOptions argument specifies
    //     an invalid TaskContinuationOptions value.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
      Contract.Requires(Enum.IsDefined(typeof(TaskContinuationOptions), continuationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationAction:
    //     The action delegate to execute when one task in the tasks array completes.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     continuation System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationAction argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationAction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(scheduler != null);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationFunction:
    //     The function delegate to execute when one task in the tasks array completes.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task{TResult}.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     continuation System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    //   TResult:
    //     The type of the result that is returned by the continuationFunction delegate
    //     and associated with the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(scheduler != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task that will be started upon
    //     the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationAction:
    //     The action delegate to execute when one task in the tasks array completes.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     continuation System.Threading.Tasks.Task.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationAction argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationAction != null);
      Contract.Requires(scheduler != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationFunction:
    //     The function delegate to execute when one task in the tasks array completes.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task{TResult}.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     continuation System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result that is returned by the continuationFunction delegate
    //     and associated with the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 0);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task that executes an end method action
    //     when a specified System.IAsyncResult completes.
    //
    // Parameters:
    //   asyncResult:
    //     The IAsyncResult whose completion should trigger the processing of the endMethod.
    //
    //   endMethod:
    //     The action delegate that processes the completed asyncResult.
    //
    // Returns:
    //     A System.Threading.Tasks.Task that represents the asynchronous operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the asyncResult argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod)
    {
      Contract.Requires(asyncResult != null);
      Contract.Requires(endMethod != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that executes an end method
    //     function when a specified System.IAsyncResult completes.
    //
    // Parameters:
    //   asyncResult:
    //     The IAsyncResult whose completion should trigger the processing of the endMethod.
    //
    //   endMethod:
    //     The function delegate that processes the completed asyncResult.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     A System.Threading.Tasks.Task{TResult} that represents the asynchronous operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the asyncResult argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
    {
      Contract.Requires(asyncResult != null);
      Contract.Requires(endMethod != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task that represents a pair of begin and
    //     end methods that conform to the Asynchronous Programming Model pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    public Task FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, object state)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that represents a pair of
    //     begin and end methods that conform to the Asynchronous Programming Model
    //     pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task{TResult} that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    public Task<TResult> FromAsync<TResult>(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task that executes an end method action
    //     when a specified System.IAsyncResult completes.
    //
    // Parameters:
    //   asyncResult:
    //     The IAsyncResult whose completion should trigger the processing of the endMethod.
    //
    //   endMethod:
    //     The action delegate that processes the completed asyncResult.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task.
    //
    // Returns:
    //     A System.Threading.Tasks.Task that represents the asynchronous operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the asyncResult argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions)
    {
      Contract.Requires(asyncResult != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that executes an end method
    //     function when a specified System.IAsyncResult completes.
    //
    // Parameters:
    //   asyncResult:
    //     The IAsyncResult whose completion should trigger the processing of the endMethod.
    //
    //   endMethod:
    //     The function delegate that processes the completed asyncResult.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     A System.Threading.Tasks.Task{TResult} that represents the asynchronous operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the asyncResult argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
    {
      Contract.Requires(asyncResult != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task that represents a pair of begin and
    //     end methods that conform to the Asynchronous Programming Model pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, object state, TaskCreationOptions creationOptions)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that represents a pair of
    //     begin and end methods that conform to the Asynchronous Programming Model
    //     pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task{TResult} that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> FromAsync<TResult>(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state, TaskCreationOptions creationOptions)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task that represents a pair of begin and
    //     end methods that conform to the Asynchronous Programming Model pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, object state)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that represents a pair of
    //     begin and end methods that conform to the Asynchronous Programming Model
    //     pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task{TResult} that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    public Task<TResult> FromAsync<TArg1, TResult>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state)
    {
      Contract.Requires(beginMethod!= null);
      Contract.Requires(endMethod != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task that executes an end method action
    //     when a specified System.IAsyncResult completes.
    //
    // Parameters:
    //   asyncResult:
    //     The IAsyncResult whose completion should trigger the processing of the endMethod.
    //
    //   endMethod:
    //     The action delegate that processes the completed asyncResult.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the task
    //     that executes the end method.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the asyncResult argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(asyncResult != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(scheduler != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that executes an end method
    //     function when a specified System.IAsyncResult completes.
    //
    // Parameters:
    //   asyncResult:
    //     The IAsyncResult whose completion should trigger the processing of the endMethod.
    //
    //   endMethod:
    //     The function delegate that processes the completed asyncResult.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the task
    //     that executes the end method.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     A System.Threading.Tasks.Task{TResult} that represents the asynchronous operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the asyncResult argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(asyncResult != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(scheduler != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task that represents a pair of begin and
    //     end methods that conform to the Asynchronous Programming Model pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that represents a pair of
    //     begin and end methods that conform to the Asynchronous Programming Model
    //     pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task{TResult} that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> FromAsync<TArg1, TResult>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task that represents a pair of begin and
    //     end methods that conform to the Asynchronous Programming Model pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   arg2:
    //     The second argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    //   TArg2:
    //     The type of the second argument passed to beginMethod delegate.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that represents a pair of
    //     begin and end methods that conform to the Asynchronous Programming Model
    //     pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   arg2:
    //     The second argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    //   TArg2:
    //     The type of the second argument passed to beginMethod delegate.
    //
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task{TResult} that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    public Task<TResult> FromAsync<TArg1, TArg2, TResult>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task that represents a pair of begin and
    //     end methods that conform to the Asynchronous Programming Model pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   arg2:
    //     The second argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    //   TArg2:
    //     The type of the second argument passed to beginMethod delegate.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that represents a pair of
    //     begin and end methods that conform to the Asynchronous Programming Model
    //     pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   arg2:
    //     The second argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    //   TArg2:
    //     The type of the second argument passed to beginMethod delegate.
    //
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task{TResult} that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> FromAsync<TArg1, TArg2, TResult>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task that represents a pair of begin and
    //     end methods that conform to the Asynchronous Programming Model pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   arg2:
    //     The second argument passed to the beginMethod delegate.
    //
    //   arg3:
    //     The third argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    //   TArg2:
    //     The type of the second argument passed to beginMethod delegate.
    //
    //   TArg3:
    //     The type of the third argument passed to beginMethod delegate.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that represents a pair of
    //     begin and end methods that conform to the Asynchronous Programming Model
    //     pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   arg2:
    //     The second argument passed to the beginMethod delegate.
    //
    //   arg3:
    //     The third argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    //   TArg2:
    //     The type of the second argument passed to beginMethod delegate.
    //
    //   TArg3:
    //     The type of the third argument passed to beginMethod delegate.
    //
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task{TResult} that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    public Task<TResult> FromAsync<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task that represents a pair of begin and
    //     end methods that conform to the Asynchronous Programming Model pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   arg2:
    //     The second argument passed to the beginMethod delegate.
    //
    //   arg3:
    //     The third argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    //   TArg2:
    //     The type of the second argument passed to beginMethod delegate.
    //
    //   TArg3:
    //     The type of the third argument passed to beginMethod delegate.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that represents a pair of
    //     begin and end methods that conform to the Asynchronous Programming Model
    //     pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   arg2:
    //     The second argument passed to the beginMethod delegate.
    //
    //   arg3:
    //     The third argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    //   TArg2:
    //     The type of the second argument passed to beginMethod delegate.
    //
    //   TArg3:
    //     The type of the third argument passed to beginMethod delegate.
    //
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task{TResult} that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> FromAsync<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task.
    //
    // Parameters:
    //   action:
    //     The action delegate to execute asynchronously.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the action argument is null.
    public Task StartNew(Action action)
    {
      Contract.Requires(action != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task{TResult}.
    //
    // Parameters:
    //   function:
    //     A function delegate that returns the future result to be available through
    //     the System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the function argument is null.
    public Task<TResult> StartNew<TResult>(Func<TResult> function)
    {
      Contract.Requires(function != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task.
    //
    // Parameters:
    //   action:
    //     The action delegate to execute asynchronously.
    //
    //   state:
    //     An object containing data to be used by the action delegate.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the action argument is null.
    public Task StartNew(Action<object> action, object state)
    {
      Contract.Requires(action != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task.
    //
    // Parameters:
    //   action:
    //     The action delegate to execute asynchronously.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.TaskFactory.CancellationToken that will be assigned
    //     to the new task.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the action argument is null.
    public Task StartNew(Action action, CancellationToken cancellationToken)
    {
      Contract.Requires(action != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task.
    //
    // Parameters:
    //   action:
    //     The action delegate to execute asynchronously.
    //
    //   creationOptions:
    //     A TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task
    //
    // Returns:
    //     The started System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the action argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task StartNew(Action action, TaskCreationOptions creationOptions)
    {
      Contract.Requires(action != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task{TResult}.
    //
    // Parameters:
    //   function:
    //     A function delegate that returns the future result to be available through
    //     the System.Threading.Tasks.Task{TResult}.
    //
    //   state:
    //     An object containing data to be used by the function delegate.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the function argument is null.
    public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state)
    {
      Contract.Requires(function!= null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task{TResult}.
    //
    // Parameters:
    //   function:
    //     A function delegate that returns the future result to be available through
    //     the System.Threading.Tasks.Task{TResult}.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.TaskFactory.CancellationToken that will be assigned
    //     to the new System.Threading.Tasks.Task
    //
    // Type parameters:
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the function argument is null.
    public Task<TResult> StartNew<TResult>(Func<TResult> function, CancellationToken cancellationToken)
    {
      Contract.Requires(function != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task{TResult}.
    //
    // Parameters:
    //   function:
    //     A function delegate that returns the future result to be available through
    //     the System.Threading.Tasks.Task{TResult}.
    //
    //   creationOptions:
    //     A TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the function argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> StartNew<TResult>(Func<TResult> function, TaskCreationOptions creationOptions)
    {
      Contract.Requires(function != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task.
    //
    // Parameters:
    //   action:
    //     The action delegate to execute asynchronously.
    //
    //   state:
    //     An object containing data to be used by the action delegate.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.TaskFactory.CancellationToken that will be assigned
    //     to the new System.Threading.Tasks.Task
    //
    // Returns:
    //     The started System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the action argument is null.
    public Task StartNew(Action<object> action, object state, CancellationToken cancellationToken)
    {
      Contract.Requires(action != null);

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task.
    //
    // Parameters:
    //   action:
    //     The action delegate to execute asynchronously.
    //
    //   state:
    //     An object containing data to be used by the action delegate.
    //
    //   creationOptions:
    //     A TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task
    //
    // Returns:
    //     The started System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the action argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task StartNew(Action<object> action, object state, TaskCreationOptions creationOptions)
    {
      Contract.Requires(action != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task{TResult}.
    //
    // Parameters:
    //   function:
    //     A function delegate that returns the future result to be available through
    //     the System.Threading.Tasks.Task{TResult}.
    //
    //   state:
    //     An object containing data to be used by the function delegate.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.TaskFactory.CancellationToken that will be assigned
    //     to the new System.Threading.Tasks.Task
    //
    // Type parameters:
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the function argument is null.
    public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, CancellationToken cancellationToken)
    {
      Contract.Requires(function != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task{TResult}.
    //
    // Parameters:
    //   function:
    //     A function delegate that returns the future result to be available through
    //     the System.Threading.Tasks.Task{TResult}.
    //
    //   state:
    //     An object containing data to be used by the function delegate.
    //
    //   creationOptions:
    //     A TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the function argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
    {
      Contract.Requires(function!= null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task.
    //
    // Parameters:
    //   action:
    //     The action delegate to execute asynchronously.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.TaskFactory.CancellationToken that will be assigned
    //     to the new System.Threading.Tasks.Task
    //
    //   creationOptions:
    //     A TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     System.Threading.Tasks.Task.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the action argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task StartNew(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(action != null);
      Contract.Requires(scheduler != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }
      
    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task{TResult}.
    //
    // Parameters:
    //   function:
    //     A function delegate that returns the future result to be available through
    //     the System.Threading.Tasks.Task{TResult}.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.TaskFactory.CancellationToken that will be assigned
    //     to the new task.
    //
    //   creationOptions:
    //     A TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the function argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> StartNew<TResult>(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(function != null);
      Contract.Requires(scheduler != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task.
    //
    // Parameters:
    //   action:
    //     The action delegate to execute asynchronously.
    //
    //   state:
    //     An object containing data to be used by the action delegate.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.TaskFactory.CancellationToken that will be assigned
    //     to the new task.
    //
    //   creationOptions:
    //     A TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     System.Threading.Tasks.Task.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the action argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task StartNew(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(action != null);
      Contract.Requires(scheduler != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task>() != null);

      return default(Task);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task{TResult}.
    //
    // Parameters:
    //   function:
    //     A function delegate that returns the future result to be available through
    //     the System.Threading.Tasks.Task{TResult}.
    //
    //   state:
    //     An object containing data to be used by the function delegate.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.TaskFactory.CancellationToken that will be assigned
    //     to the new task.
    //
    //   creationOptions:
    //     A TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TResult:
    //     The type of the result available through the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the function argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(function != null);
      Contract.Requires(scheduler != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
  }

   // Summary:
  //     Provides support for creating and scheduling System.Threading.Tasks.Task<TResult>
  //     objects.
  //
  // Type parameters:
  //   TResult:
  //     The type of the results that are available though the System.Threading.Tasks.Task{TResult}
  //     objects that are associated with the methods in this class.
  public class TaskFactory<TResult>
  {
    // Summary:
    //     Initializes a System.Threading.Tasks.TaskFactory<TResult> instance with the
    //     default configuration.
    //public TaskFactory();
    //
    // Summary:
    //     Initializes a System.Threading.Tasks.TaskFactory<TResult> instance with the
    //     default configuration.
    //
    // Parameters:
    //   cancellationToken:
    //     The default System.Threading.Tasks.TaskFactory<TResult>.CancellationToken
    //     that will be assigned to tasks created by this System.Threading.Tasks.TaskFactory
    //     unless another CancellationToken is explicitly specified while calling the
    //     factory methods.
    //public TaskFactory(CancellationToken cancellationToken);
    //
    // Summary:
    //     Initializes a System.Threading.Tasks.TaskFactory<TResult> instance with the
    //     specified configuration.
    //
    // Parameters:
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler to use to schedule any tasks created
    //     with this TaskFactory{TResult}. A null value indicates that the current TaskScheduler
    //     should be used.
    //public TaskFactory(TaskScheduler scheduler);
    //
    // Summary:
    //     Initializes a System.Threading.Tasks.TaskFactory<TResult> instance with the
    //     specified configuration.
    //
    // Parameters:
    //   creationOptions:
    //     The default System.Threading.Tasks.TaskCreationOptions to use when creating
    //     tasks with this TaskFactory{TResult}.
    //
    //   continuationOptions:
    //     The default System.Threading.Tasks.TaskContinuationOptions to use when creating
    //     continuation tasks with this TaskFactory{TResult}.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument or the continuationOptions
    //     argument specifies an invalid value.
    public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions)
    {
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));
      Contract.Requires(Enum.IsDefined(typeof(TaskContinuationOptions), continuationOptions));
    }
    //
    // Summary:
    //     Initializes a System.Threading.Tasks.TaskFactory<TResult> instance with the
    //     specified configuration.
    //
    // Parameters:
    //   cancellationToken:
    //     The default System.Threading.CancellationToken that will be assigned to tasks
    //     created by this System.Threading.Tasks.TaskFactory unless another CancellationToken
    //     is explicitly specified while calling the factory methods.
    //
    //   creationOptions:
    //     The default System.Threading.Tasks.TaskCreationOptions to use when creating
    //     tasks with this System.Threading.Tasks.TaskFactory<TResult>.
    //
    //   continuationOptions:
    //     The default System.Threading.Tasks.TaskContinuationOptions to use when creating
    //     continuation tasks with this TaskFactory{TResult}.
    //
    //   scheduler:
    //     The default System.Threading.Tasks.TaskScheduler to use to schedule any Tasks
    //     created with this TaskFactory{TResult}. A null value indicates that TaskScheduler.Current
    //     should be used.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument or the continuationOptions
    //     argumentspecifies an invalid value.
    public TaskFactory(CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));
      Contract.Requires(Enum.IsDefined(typeof(TaskContinuationOptions), continuationOptions));
    }

    // Summary:
    //     Gets the default System.Threading.CancellationToken of this TaskFactory.
    //
    // Returns:
    //     Returns the default System.Threading.CancellationToken of this TaskFactory.
    //public CancellationToken CancellationToken { get; }
    //
    // Summary:
    //     Gets the System.Threading.Tasks.TaskCreationOptions value of this TaskFactory{TResult}.
    //
    // Returns:
    //     Returns the System.Threading.Tasks.TaskCreationOptions value of this System.Threading.Tasks.TaskFactory{TResult}.
    //public TaskContinuationOptions ContinuationOptions { get; }
    //
    // Summary:
    //     Gets the System.Threading.Tasks.TaskCreationOptions value of this TaskFactory{TResult}.
    //
    // Returns:
    //     Returns the System.Threading.Tasks.TaskCreationOptions value of this System.Threading.Tasks.TaskFactory{TResult}.
    //public TaskCreationOptions CreationOptions { get; }
    //
    // Summary:
    //     Gets the System.Threading.Tasks.TaskScheduler of this TaskFactory{TResult}.
    //
    // Returns:
    //     Returns the System.Threading.Tasks.TaskScheduler of this System.Threading.Tasks.TaskFactory{TResult}.
    //public TaskScheduler Scheduler { get; }

    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationFunction:
    //     The function delegate to execute when all tasks in the tasks array have completed.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 1);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationFunction:
    //     The function delegate to execute when all tasks in the tasks array have completed.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 1);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationFunction:
    //     The function delegate to execute when all tasks in the tasks array have completed.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.-or-The provided System.Threading.CancellationToken has
    //     already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 1);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationFunction:
    //     The function delegate to execute when all tasks in the tasks array have completed.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the continuationOptions argument specifies
    //     an invalid TaskContinuationOptions value.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 1);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
      Contract.Requires(Enum.IsDefined(typeof(TaskContinuationOptions), continuationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationFunction:
    //     The function delegate to execute when all tasks in the tasks array have completed.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.-or-The provided System.Threading.CancellationToken has
    //     already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 1);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationFunction:
    //     The function delegate to execute when all tasks in the tasks array have completed.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the continuationOptions argument specifies
    //     an invalid TaskContinuationOptions value.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 1);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
      Contract.Requires(Enum.IsDefined(typeof(TaskContinuationOptions), continuationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationFunction:
    //     The function delegate to execute when all tasks in the tasks array have completed.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task{TResult}.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     continuation System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(scheduler != null);
      Contract.Requires(tasks.Length > 1);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of a set of provided Tasks.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue.
    //
    //   continuationFunction:
    //     The function delegate to execute when all tasks in the tasks array have completed.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task{TResult}.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     continuation System.Threading.Tasks.Task.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(scheduler != null);
      Contract.Requires(tasks.Length > 1);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationFunction:
    //     The function delegate to execute when one task in the tasks array completes.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 1);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationFunction:
    //     The function delegate to execute when one task in the tasks array completes.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 1);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationFunction:
    //     The function delegate to execute when one task in the tasks array completes.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.-or-The provided System.Threading.CancellationToken has
    //     already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 1);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationFunction:
    //     The function delegate to execute when one task in the tasks array completes.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the continuationOptions argument specifies
    //     an invalid TaskContinuationOptions value.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 1);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
      Contract.Requires(Enum.IsDefined(typeof(TaskContinuationOptions), continuationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationFunction:
    //     The function delegate to execute when one task in the tasks array completes.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.-or-The provided System.Threading.CancellationToken has
    //     already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 1);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationFunction:
    //     The function delegate to execute when one task in the tasks array completes.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The exception that is thrown when one of the elements in the tasks array
    //     has been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the continuationOptions argument specifies
    //     an invalid TaskContinuationOptions value.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(tasks.Length > 1);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));
      Contract.Requires(Enum.IsDefined(typeof(TaskContinuationOptions), continuationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationFunction:
    //     The function delegate to execute when one task in the tasks array completes.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task{TResult}.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     continuation System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TAntecedentResult:
    //     The type of the result of the antecedent tasks.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(scheduler != null);
      Contract.Requires(tasks.Length > 1);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a continuation System.Threading.Tasks.Task{TResult} that will be
    //     started upon the completion of any Task in the provided set.
    //
    // Parameters:
    //   tasks:
    //     The array of tasks from which to continue when one task completes.
    //
    //   continuationFunction:
    //     The function delegate to execute when one task in the tasks array completes.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken that will be assigned to the new continuation
    //     task.
    //
    //   continuationOptions:
    //     The System.Threading.Tasks.TaskContinuationOptions value that controls the
    //     behavior of the created continuation System.Threading.Tasks.Task{TResult}.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     continuation System.Threading.Tasks.Task.
    //
    // Returns:
    //     The new continuation System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the tasks array is null.-or-The exception
    //     that is thrown when the continuationFunction argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentException:
    //     The exception that is thrown when the tasks array contains a null value.-or-The
    //     exception that is thrown when the tasks array is empty.
    public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(tasks != null);
      Contract.Requires(continuationFunction != null);
      Contract.Requires(scheduler != null);
      Contract.Requires(tasks.Length > 1);
      Contract.Requires(Contract.ForAll(tasks, t => t != null));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that executes an end method
    //     function when a specified System.IAsyncResult completes.
    //
    // Parameters:
    //   asyncResult:
    //     The IAsyncResult whose completion should trigger the processing of the endMethod.
    //
    //   endMethod:
    //     The function delegate that processes the completed asyncResult.
    //
    // Returns:
    //     A System.Threading.Tasks.Task{TResult} that represents the asynchronous operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the asyncResult argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
    {
      Contract.Requires(asyncResult != null);
      Contract.Requires(endMethod != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that represents a pair of
    //     begin and end methods that conform to the Asynchronous Programming Model
    //     pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task{TResult} that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    public Task<TResult> FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that executes an end method
    //     function when a specified System.IAsyncResult completes.
    //
    // Parameters:
    //   asyncResult:
    //     The IAsyncResult whose completion should trigger the processing of the endMethod.
    //
    //   endMethod:
    //     The function delegate that processes the completed asyncResult.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     A System.Threading.Tasks.Task{TResult} that represents the asynchronous operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the asyncResult argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
    {
      Contract.Requires(asyncResult != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that represents a pair of
    //     begin and end methods that conform to the Asynchronous Programming Model
    //     pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task{TResult} that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state, TaskCreationOptions creationOptions)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that represents a pair of
    //     begin and end methods that conform to the Asynchronous Programming Model
    //     pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task{TResult} that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    public Task<TResult> FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state)
    {
      Contract.Requires(beginMethod!= null);
      Contract.Requires(endMethod != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that executes an end method
    //     function when a specified System.IAsyncResult completes.
    //
    // Parameters:
    //   asyncResult:
    //     The IAsyncResult whose completion should trigger the processing of the endMethod.
    //
    //   endMethod:
    //     The function delegate that processes the completed asyncResult.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the task
    //     that executes the end method.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task{TResult} that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the asyncResult argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(asyncResult != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(scheduler != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that represents a pair of
    //     begin and end methods that conform to the Asynchronous Programming Model
    //     pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task{TResult} that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that represents a pair of
    //     begin and end methods that conform to the Asynchronous Programming Model
    //     pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   arg2:
    //     The second argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    //   TArg2:
    //     The type of the second argument passed to beginMethod delegate.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task{TResult} that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    public Task<TResult> FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that represents a pair of
    //     begin and end methods that conform to the Asynchronous Programming Model
    //     pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   arg2:
    //     The second argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    //   TArg2:
    //     The type of the second argument passed to beginMethod delegate.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task{TResult} that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that represents a pair of
    //     begin and end methods that conform to the Asynchronous Programming Model
    //     pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   arg2:
    //     The second argument passed to the beginMethod delegate.
    //
    //   arg3:
    //     The third argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    //   TArg2:
    //     The type of the second argument passed to beginMethod delegate.
    //
    //   TArg3:
    //     The type of the third argument passed to beginMethod delegate.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task{TResult} that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    public Task<TResult> FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates a System.Threading.Tasks.Task{TResult} that represents a pair of
    //     begin and end methods that conform to the Asynchronous Programming Model
    //     pattern.
    //
    // Parameters:
    //   beginMethod:
    //     The delegate that begins the asynchronous operation.
    //
    //   endMethod:
    //     The delegate that ends the asynchronous operation.
    //
    //   arg1:
    //     The first argument passed to the beginMethod delegate.
    //
    //   arg2:
    //     The second argument passed to the beginMethod delegate.
    //
    //   arg3:
    //     The third argument passed to the beginMethod delegate.
    //
    //   state:
    //     An object containing data to be used by the beginMethod delegate.
    //
    //   creationOptions:
    //     The TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    // Type parameters:
    //   TArg1:
    //     The type of the first argument passed to the beginMethod delegate.
    //
    //   TArg2:
    //     The type of the second argument passed to beginMethod delegate.
    //
    //   TArg3:
    //     The type of the third argument passed to beginMethod delegate.
    //
    // Returns:
    //     The created System.Threading.Tasks.Task{TResult} that represents the asynchronous
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the beginMethod argument is null.-or-The
    //     exception that is thrown when the endMethod argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
    {
      Contract.Requires(beginMethod != null);
      Contract.Requires(endMethod != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task{TResult}.
    //
    // Parameters:
    //   function:
    //     A function delegate that returns the future result to be available through
    //     the System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the function argument is null.
    public Task<TResult> StartNew(Func<TResult> function)
    {
      Contract.Requires(function != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task{TResult}.
    //
    // Parameters:
    //   function:
    //     A function delegate that returns the future result to be available through
    //     the System.Threading.Tasks.Task{TResult}.
    //
    //   state:
    //     An object containing data to be used by the function delegate.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the function argument is null.
    public Task<TResult> StartNew(Func<object, TResult> function, object state)
    {
      Contract.Requires(function != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task{TResult}.
    //
    // Parameters:
    //   function:
    //     A function delegate that returns the future result to be available through
    //     the System.Threading.Tasks.Task{TResult}.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.TaskFactory<TResult>.CancellationToken that will
    //     be assigned to the new task.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the function argument is null.
    public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken)
    {
      Contract.Requires(function != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task{TResult}.
    //
    // Parameters:
    //   function:
    //     A function delegate that returns the future result to be available through
    //     the System.Threading.Tasks.Task{TResult}.
    //
    //   creationOptions:
    //     A TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the function argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> StartNew(Func<TResult> function, TaskCreationOptions creationOptions)
    {
      Contract.Requires(function != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task{TResult}.
    //
    // Parameters:
    //   function:
    //     A function delegate that returns the future result to be available through
    //     the System.Threading.Tasks.Task{TResult}.
    //
    //   state:
    //     An object containing data to be used by the function delegate.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.TaskFactory<TResult>.CancellationToken that will
    //     be assigned to the new task.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the function argument is null.
    public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken cancellationToken)
    {
      Contract.Requires(function != null);

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task{TResult}.
    //
    // Parameters:
    //   function:
    //     A function delegate that returns the future result to be available through
    //     the System.Threading.Tasks.Task{TResult}.
    //
    //   state:
    //     An object containing data to be used by the function delegate.
    //
    //   creationOptions:
    //     A TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown when the function argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> StartNew(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
    {
      Contract.Requires(function != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task{TResult}.
    //
    // Parameters:
    //   function:
    //     A function delegate that returns the future result to be available through
    //     the System.Threading.Tasks.Task{TResult}.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.TaskFactory<TResult>.CancellationToken that will
    //     be assigned to the new task.
    //
    //   creationOptions:
    //     A TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the function argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(function != null);
      Contract.Requires(scheduler != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }

    //
    // Summary:
    //     Creates and starts a System.Threading.Tasks.Task{TResult}.
    //
    // Parameters:
    //   function:
    //     A function delegate that returns the future result to be available through
    //     the System.Threading.Tasks.Task{TResult}.
    //
    //   state:
    //     An object containing data to be used by the function delegate.
    //
    //   cancellationToken:
    //     The System.Threading.Tasks.TaskFactory<TResult>.CancellationToken that will
    //     be assigned to the new task.
    //
    //   creationOptions:
    //     A TaskCreationOptions value that controls the behavior of the created System.Threading.Tasks.Task{TResult}.
    //
    //   scheduler:
    //     The System.Threading.Tasks.TaskScheduler that is used to schedule the created
    //     System.Threading.Tasks.Task{TResult}.
    //
    // Returns:
    //     The started System.Threading.Tasks.Task{TResult}.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The provided System.Threading.CancellationToken has already been disposed.
    //
    //   System.ArgumentNullException:
    //     The exception that is thrown when the function argument is null.-or-The exception
    //     that is thrown when the scheduler argument is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The exception that is thrown when the creationOptions argument specifies
    //     an invalid TaskCreationOptions value.
    public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      Contract.Requires(function != null);
      Contract.Requires(scheduler != null);
      Contract.Requires(Enum.IsDefined(typeof(TaskCreationOptions), creationOptions));

      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return default(Task<TResult>);
    }
  }
}
#endif
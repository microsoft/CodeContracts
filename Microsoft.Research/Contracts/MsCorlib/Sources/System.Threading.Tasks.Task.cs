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

// File System.Threading.Tasks.Task.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Threading.Tasks
{
  public partial class Task : System.Threading.IThreadPoolWorkItem, IAsyncResult, IDisposable
  {
    #region Methods and constructors
    public System.Threading.Tasks.Task<TResult> ContinueWith<TResult>(Func<System.Threading.Tasks.Task, TResult> continuationFunction, System.Threading.CancellationToken cancellationToken)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task<TResult>>() != null);

      return default(System.Threading.Tasks.Task<TResult>);
    }

    public System.Threading.Tasks.Task ContinueWith(Action<System.Threading.Tasks.Task> continuationAction, System.Threading.CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task>() != null);

      return default(System.Threading.Tasks.Task);
    }

    public System.Threading.Tasks.Task<TResult> ContinueWith<TResult>(Func<System.Threading.Tasks.Task, TResult> continuationFunction, TaskScheduler scheduler)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task<TResult>>() != null);

      return default(System.Threading.Tasks.Task<TResult>);
    }

    public System.Threading.Tasks.Task<TResult> ContinueWith<TResult>(Func<System.Threading.Tasks.Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task<TResult>>() != null);

      return default(System.Threading.Tasks.Task<TResult>);
    }

    public System.Threading.Tasks.Task ContinueWith(Action<System.Threading.Tasks.Task> continuationAction, System.Threading.CancellationToken cancellationToken)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task>() != null);

      return default(System.Threading.Tasks.Task);
    }

    public System.Threading.Tasks.Task ContinueWith(Action<System.Threading.Tasks.Task> continuationAction)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task>() != null);

      return default(System.Threading.Tasks.Task);
    }

    public System.Threading.Tasks.Task ContinueWith(Action<System.Threading.Tasks.Task> continuationAction, TaskContinuationOptions continuationOptions)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task>() != null);

      return default(System.Threading.Tasks.Task);
    }

    public System.Threading.Tasks.Task ContinueWith(Action<System.Threading.Tasks.Task> continuationAction, TaskScheduler scheduler)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task>() != null);

      return default(System.Threading.Tasks.Task);
    }

    public System.Threading.Tasks.Task<TResult> ContinueWith<TResult>(Func<System.Threading.Tasks.Task, TResult> continuationFunction, System.Threading.CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task<TResult>>() != null);

      return default(System.Threading.Tasks.Task<TResult>);
    }

    public System.Threading.Tasks.Task<TResult> ContinueWith<TResult>(Func<System.Threading.Tasks.Task, TResult> continuationFunction)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task<TResult>>() != null);

      return default(System.Threading.Tasks.Task<TResult>);
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public void Dispose()
    {
    }

    public void RunSynchronously(TaskScheduler scheduler)
    {
    }

    public void RunSynchronously()
    {
    }

    public void Start(TaskScheduler scheduler)
    {
    }

    public void Start()
    {
    }

    void System.Threading.IThreadPoolWorkItem.ExecuteWorkItem()
    {
    }

    void System.Threading.IThreadPoolWorkItem.MarkAborted(System.Threading.ThreadAbortException tae)
    {
    }

    public Task(Action action)
    {
    }

    public Task(Action<Object> action, Object state, TaskCreationOptions creationOptions)
    {
    }

    public Task(Action<Object> action, Object state, System.Threading.CancellationToken cancellationToken, TaskCreationOptions creationOptions)
    {
    }

    public Task(Action<Object> action, Object state, System.Threading.CancellationToken cancellationToken)
    {
    }

    public Task(Action action, System.Threading.CancellationToken cancellationToken)
    {
    }

    public Task(Action action, TaskCreationOptions creationOptions)
    {
    }

    public Task(Action<Object> action, Object state)
    {
    }

    public Task(Action action, System.Threading.CancellationToken cancellationToken, TaskCreationOptions creationOptions)
    {
    }

    public void Wait(System.Threading.CancellationToken cancellationToken)
    {
    }

    public bool Wait(int millisecondsTimeout, System.Threading.CancellationToken cancellationToken)
    {
      return default(bool);
    }

    public bool Wait(TimeSpan timeout)
    {
      return default(bool);
    }

    public void Wait()
    {
    }

    public bool Wait(int millisecondsTimeout)
    {
      return default(bool);
    }

    public static void WaitAll(System.Threading.Tasks.Task[] tasks)
    {
    }

    public static bool WaitAll(System.Threading.Tasks.Task[] tasks, TimeSpan timeout)
    {
      return default(bool);
    }

    public static bool WaitAll(System.Threading.Tasks.Task[] tasks, int millisecondsTimeout)
    {
      Contract.Ensures(!System.Threading.CancellationToken.None.IsCancellationRequested);

      return default(bool);
    }

    public static bool WaitAll(System.Threading.Tasks.Task[] tasks, int millisecondsTimeout, System.Threading.CancellationToken cancellationToken)
    {
      Contract.Ensures(!cancellationToken.IsCancellationRequested);

      return default(bool);
    }

    public static void WaitAll(System.Threading.Tasks.Task[] tasks, System.Threading.CancellationToken cancellationToken)
    {
      Contract.Ensures(!cancellationToken.IsCancellationRequested);
    }

    public static int WaitAny(System.Threading.Tasks.Task[] tasks)
    {
      return default(int);
    }

    public static int WaitAny(System.Threading.Tasks.Task[] tasks, int millisecondsTimeout, System.Threading.CancellationToken cancellationToken)
    {
      Contract.Ensures(!cancellationToken.IsCancellationRequested);

      return default(int);
    }

    public static int WaitAny(System.Threading.Tasks.Task[] tasks, int millisecondsTimeout)
    {
      Contract.Ensures(!System.Threading.CancellationToken.None.IsCancellationRequested);

      return default(int);
    }

    public static int WaitAny(System.Threading.Tasks.Task[] tasks, System.Threading.CancellationToken cancellationToken)
    {
      Contract.Ensures(!cancellationToken.IsCancellationRequested);

      return default(int);
    }

    public static int WaitAny(System.Threading.Tasks.Task[] tasks, TimeSpan timeout)
    {
      return default(int);
    }
    #endregion

    #region Properties and indexers
    public Object AsyncState
    {
      get
      {
        return default(Object);
      }
    }

    public TaskCreationOptions CreationOptions
    {
      get
      {
        return default(TaskCreationOptions);
      }
    }

    public static Nullable<int> CurrentId
    {
      get
      {
        return default(Nullable<int>);
      }
    }

    public AggregateException Exception
    {
      get
      {
        return default(AggregateException);
      }
    }

    public static TaskFactory Factory
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Threading.Tasks.TaskFactory>() != null);

        return default(TaskFactory);
      }
    }

    public int Id
    {
      get
      {
        return default(int);
      }
    }

    public bool IsCanceled
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsCompleted
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsFaulted
    {
      get
      {
        return default(bool);
      }
    }

    public TaskStatus Status
    {
      get
      {
        Contract.Ensures(((System.Threading.Tasks.TaskStatus)(0)) <= Contract.Result<System.Threading.Tasks.TaskStatus>());
        Contract.Ensures(Contract.Result<System.Threading.Tasks.TaskStatus>() <= ((System.Threading.Tasks.TaskStatus)(7)));

        return default(TaskStatus);
      }
    }

    System.Threading.WaitHandle System.IAsyncResult.AsyncWaitHandle
    {
      get
      {
        return default(System.Threading.WaitHandle);
      }
    }

    bool System.IAsyncResult.CompletedSynchronously
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}

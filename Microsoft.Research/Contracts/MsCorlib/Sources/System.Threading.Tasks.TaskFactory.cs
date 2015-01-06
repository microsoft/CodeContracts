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

// File System.Threading.Tasks.TaskFactory.cs
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
  public partial class TaskFactory
  {
    #region Methods and constructors
    public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, System.Threading.CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, System.Threading.CancellationToken cancellationToken)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, System.Threading.CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, System.Threading.CancellationToken cancellationToken)
    {
      return default(Task<TResult>);
    }

    public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, TaskContinuationOptions continuationOptions)
    {
      return default(Task);
    }

    public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, System.Threading.CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      return default(Task);
    }

    public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction)
    {
      return default(Task);
    }

    public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, System.Threading.CancellationToken cancellationToken)
    {
      return default(Task);
    }

    public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, TaskContinuationOptions continuationOptions)
    {
      return default(Task);
    }

    public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, System.Threading.CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      return default(Task);
    }

    public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction)
    {
      return default(Task);
    }

    public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, System.Threading.CancellationToken cancellationToken)
    {
      return default(Task);
    }

    public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, System.Threading.CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, System.Threading.CancellationToken cancellationToken)
    {
      return default(Task<TResult>);
    }

    public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, TaskContinuationOptions continuationOptions)
    {
      return default(Task);
    }

    public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, System.Threading.CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      return default(Task);
    }

    public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction)
    {
      return default(Task);
    }

    public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, System.Threading.CancellationToken cancellationToken)
    {
      return default(Task);
    }

    public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
    {
      return default(Task);
    }

    public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, System.Threading.CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      return default(Task);
    }

    public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction)
    {
      return default(Task);
    }

    public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, System.Threading.CancellationToken cancellationToken)
    {
      return default(Task);
    }

    public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, System.Threading.CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, System.Threading.CancellationToken cancellationToken)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> FromAsync<TArg1, TArg2, TResult>(Func<TArg1, TArg2, AsyncCallback, Object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, Object state, TaskCreationOptions creationOptions)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> FromAsync<TArg1, TArg2, TResult>(Func<TArg1, TArg2, AsyncCallback, Object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, Object state)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> FromAsync<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, AsyncCallback, Object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, Object state, TaskCreationOptions creationOptions)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> FromAsync<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, AsyncCallback, Object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, Object state)
    {
      return default(Task<TResult>);
    }

    public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, Object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, Object state, TaskCreationOptions creationOptions)
    {
      return default(Task);
    }

    public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, Object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, Object state)
    {
      return default(Task);
    }

    public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, Object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, Object state, TaskCreationOptions creationOptions)
    {
      return default(Task);
    }

    public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, Object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, Object state)
    {
      return default(Task);
    }

    public Task FromAsync(Func<AsyncCallback, Object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, Object state, TaskCreationOptions creationOptions)
    {
      return default(Task);
    }

    public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions)
    {
      return default(Task);
    }

    public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod)
    {
      return default(Task);
    }

    public Task FromAsync(Func<AsyncCallback, Object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, Object state)
    {
      return default(Task);
    }

    public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      return default(Task);
    }

    public Task<TResult> FromAsync<TResult>(Func<AsyncCallback, Object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, Object state, TaskCreationOptions creationOptions)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> FromAsync<TResult>(Func<AsyncCallback, Object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, Object state)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> FromAsync<TArg1, TResult>(Func<TArg1, AsyncCallback, Object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, Object state, TaskCreationOptions creationOptions)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> FromAsync<TArg1, TResult>(Func<TArg1, AsyncCallback, Object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, Object state)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      return default(Task<TResult>);
    }

    public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, Object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, Object state, TaskCreationOptions creationOptions)
    {
      return default(Task);
    }

    public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, Object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, Object state)
    {
      return default(Task);
    }

    public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
    {
      return default(Task<TResult>);
    }

    public Task StartNew(Action<Object> action, Object state)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task>() != null);

      return default(Task);
    }

    public Task StartNew(Action<Object> action, Object state, System.Threading.CancellationToken cancellationToken)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task>() != null);

      return default(Task);
    }

    public Task StartNew(Action<Object> action, Object state, TaskCreationOptions creationOptions)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task>() != null);

      return default(Task);
    }

    public Task StartNew(Action action, System.Threading.CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task>() != null);

      return default(Task);
    }

    public Task StartNew(Action action)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task>() != null);

      return default(Task);
    }

    public Task StartNew(Action action, System.Threading.CancellationToken cancellationToken)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task>() != null);

      return default(Task);
    }

    public Task StartNew(Action action, TaskCreationOptions creationOptions)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task>() != null);

      return default(Task);
    }

    public Task StartNew(Action<Object> action, Object state, System.Threading.CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      Contract.Ensures(Contract.Result<System.Threading.Tasks.Task>() != null);

      return default(Task);
    }

    public Task<TResult> StartNew<TResult>(Func<Object, TResult> function, Object state, System.Threading.CancellationToken cancellationToken)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> StartNew<TResult>(Func<Object, TResult> function, Object state)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> StartNew<TResult>(Func<Object, TResult> function, Object state, System.Threading.CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> StartNew<TResult>(Func<Object, TResult> function, Object state, TaskCreationOptions creationOptions)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> StartNew<TResult>(Func<TResult> function, System.Threading.CancellationToken cancellationToken)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> StartNew<TResult>(Func<TResult> function)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> StartNew<TResult>(Func<TResult> function, System.Threading.CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      return default(Task<TResult>);
    }

    public Task<TResult> StartNew<TResult>(Func<TResult> function, TaskCreationOptions creationOptions)
    {
      return default(Task<TResult>);
    }

    public TaskFactory()
    {
    }

    public TaskFactory(System.Threading.CancellationToken cancellationToken)
    {
    }

    public TaskFactory(System.Threading.CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
    }

    public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions)
    {
    }

    public TaskFactory(TaskScheduler scheduler)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Threading.CancellationToken CancellationToken
    {
      get
      {
        return default(System.Threading.CancellationToken);
      }
    }

    public TaskContinuationOptions ContinuationOptions
    {
      get
      {
        return default(TaskContinuationOptions);
      }
    }

    public TaskCreationOptions CreationOptions
    {
      get
      {
        return default(TaskCreationOptions);
      }
    }

    public TaskScheduler Scheduler
    {
      get
      {
        return default(TaskScheduler);
      }
    }
    #endregion
  }
}

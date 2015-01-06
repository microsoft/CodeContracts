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
using System.Runtime.CompilerServices;
using System.Diagnostics.Contracts;


namespace System.Threading.Tasks
{
  // Summary:
  //     Provides a set of static (Shared in Visual Basic) methods for working with
  //     specific kinds of System.Threading.Tasks.Task instances.
  public static class TaskExtensions
  {
    // Summary:
    //     Creates a proxy System.Threading.Tasks.Task that represents the asynchronous
    //     operation of a Task<Task<T>> (C#) or Task (Of Task(Of T)) (Visual Basic).
    //
    // Parameters:
    //   task:
    //     The Task<Task<T>> (C#) or Task (Of Task(Of T)) (Visual Basic) to unwrap.
    //
    // Type parameters:
    //   TResult:
    //     The type of the task's result.
    //
    // Returns:
    //     A System.Threading.Tasks.Task that represents the asynchronous operation
    //     of the provided Task<Task<T>> (C#) or Task (Of Task(Of T)) (Visual Basic).
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown if the task argument is null.
    public static Task<TResult> Unwrap<TResult>(this Task<Task<TResult>> task)
    {
      Contract.Requires(task != null);
      Contract.Ensures(Contract.Result<Task<TResult>>() != null);

      return null;
    }
    //
    // Summary:
    //     Creates a proxy System.Threading.Tasks.Task that represents the asynchronous
    //     operation of a System.Threading.Tasks.TaskScheduler.TryExecuteTaskInline(System.Threading.Tasks.Task,System.Boolean).
    //
    // Parameters:
    //   task:
    //     The Task<Task> (C#) or Task (Of Task) (Visual Basic) to unwrap.
    //
    // Returns:
    //     A Task that represents the asynchronous operation of the provided System.Threading.Tasks.Task(Of
    //     Task).
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The exception that is thrown if the task argument is null.
    public static Task Unwrap(this Task<Task> task)
    {
      Contract.Requires(task != null);
      Contract.Ensures(Contract.Result<Task>() != null);

      return null;
    }
  }
}
#endif
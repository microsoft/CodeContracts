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


#if NETFRAMEWORK_4_5
namespace System.Runtime.CompilerServices
{
  using System.Diagnostics.Contracts;
  using System.Threading.Tasks;

  public struct AsyncTaskMethodBuilder
  {
      // Summary:
      //     Gets the task for this builder.
      //
      // Returns:
      //     The task for this builder.
      //
      // Exceptions:
      //   System.InvalidOperationException:
      //     The builder is not initialized.
      public Task Task
      {
          get
          {
              Contract.Ensures(Contract.Result<Task>() != null);

              return default(Task);
          }
      }
  }

  public struct AsyncTaskMethodBuilder<TResult>
  {
      // Summary:
      //     Gets the task for this builder.
      //
      // Returns:
      //     The task for this builder.
      //
      // Exceptions:
      //   System.InvalidOperationException:
      //     The builder is not initialized.
      public Task<TResult> Task
      {
          get
          {
              Contract.Ensures(Contract.Result<Task<TResult>>() != null);

              return default(Task<TResult>);
          }
      }
  }
}
#endif
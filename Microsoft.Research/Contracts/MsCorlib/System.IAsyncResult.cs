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
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Threading;

namespace System {
  // Summary:
  //     Represents the status of an asynchronous operation.

    [ContractClass(typeof(IAsyncResultContracts))]
  public interface IAsyncResult {
    // Summary:
    //     Gets a user-defined object that qualifies or contains information about an
    //     asynchronous operation.
    //
    // Returns:
    //     A user-defined object that qualifies or contains information about an asynchronous
    //     operation.
    object AsyncState { get; }
    //
    // Summary:
    //     Gets a System.Threading.WaitHandle that is used to wait for an asynchronous
    //     operation to complete.
    //
    // Returns:
    //     A System.Threading.WaitHandle that is used to wait for an asynchronous operation
    //     to complete.
    WaitHandle AsyncWaitHandle { get; }
    //
    // Summary:
    //     Gets an indication of whether the asynchronous operation completed synchronously.
    //
    // Returns:
    //     true if the asynchronous operation completed synchronously; otherwise, false.
    // bool CompletedSynchronously { get; }
    //
    // Summary:
    //     Gets an indication whether the asynchronous operation has completed.
    //
    // Returns:
    //     true if the operation is complete; otherwise, false.
    //
    // NOTE this is a property that is not pure, i.e., it may change given other threads/side effects
    // from one call to another. We should have an annotation like [Volatile] to mark such properties.
    bool IsCompleted { get; }
  }

    [ContractClassFor(typeof(IAsyncResult))]
  abstract class IAsyncResultContracts : IAsyncResult {
    public object AsyncState {
      get { throw new NotImplementedException(); }
    }

    public WaitHandle AsyncWaitHandle 
    {
      get 
      {
        Contract.Ensures(Contract.Result<WaitHandle>() != null);
            
        throw new NotImplementedException(); 
      }
    }

    public bool IsCompleted {
      get { throw new NotImplementedException(); }
    }
  }
}


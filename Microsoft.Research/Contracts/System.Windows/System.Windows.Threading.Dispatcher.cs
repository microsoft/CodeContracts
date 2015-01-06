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
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;


namespace System.Windows.Threading
{

  // Summary:
  //     Represents an operation that has been posted to the System.Windows.Threading.Dispatcher
  //     queue.
  public sealed class DispatcherOperation
  {
  }

  // Summary:
  //     Provides services for managing the queue of work items for a thread.
  public sealed class Dispatcher
  {
    private Dispatcher() { }

    // Summary:
    //     Executes the specified delegate asynchronously on the thread the System.Windows.Threading.Dispatcher
    //     is associated with.
    //
    // Parameters:
    //   a:
    //     A delegate to a method that takes no arguments and does not return a value,
    //     which is pushed onto the System.Windows.Threading.Dispatcher event queue.
    //
    // Returns:
    //     An object, which is returned immediately after Overload:System.Windows.Threading.Dispatcher.BeginInvoke
    //     is called, that represents the operation that has been posted to the System.Windows.Threading.Dispatcher
    //     queue.
    extern public DispatcherOperation BeginInvoke(Action a);
    //
    // Summary:
    //     Executes the specified delegate asynchronously with the specified array of
    //     arguments on the thread the System.Windows.Threading.Dispatcher is associated
    //     with.
    //
    // Parameters:
    //   d:
    //     A delegate to a method that takes multiple arguments, which is pushed onto
    //     the System.Windows.Threading.Dispatcher event queue.
    //
    //   args:
    //     An array of objects to pass as arguments to the specified method.
    //
    // Returns:
    //     An object, which is returned immediately after Overload:System.Windows.Threading.Dispatcher.BeginInvoke
    //     is called, that represents the operation that has been posted to the System.Windows.Threading.Dispatcher
    //     queue.
    extern public DispatcherOperation BeginInvoke(Delegate d, params object[] args);
    //
    // Summary:
    //     Determines whether the calling thread is the thread associated with this
    //     System.Windows.Threading.Dispatcher.
    //
    // Returns:
    //     true if the calling thread is the thread associated with this System.Windows.Threading.Dispatcher;
    //     otherwise, false.
    [Pure]
    extern public bool CheckAccess();
  }
}

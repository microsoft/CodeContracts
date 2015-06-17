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

using System.Runtime.InteropServices;

namespace System
{
  // Summary:
  //     Provides data for the event that is raised when there is an exception that
  //     is not handled in any application domain.
  public class UnhandledExceptionEventArgs : EventArgs
  {
    // Summary:
    //     Initializes a new instance of the System.UnhandledExceptionEventArgs class
    //     with the exception object and a common language runtime termination flag.
    //
    // Parameters:
    //   exception:
    //     The exception that is not handled.
    //
    //   isTerminating:
    //     true if the runtime is terminating; otherwise, false.
    extern public UnhandledExceptionEventArgs(object exception, bool isTerminating);

    // Summary:
    //     Gets the unhandled exception object.
    //
    // Returns:
    //     The unhandled exception object.
    extern public object ExceptionObject { get; }
    //
    // Summary:
    //     Indicates whether the common language runtime is terminating.
    //
    // Returns:
    //     true if the runtime is terminating; otherwise, false.
    extern public bool IsTerminating { get; }
  }
}

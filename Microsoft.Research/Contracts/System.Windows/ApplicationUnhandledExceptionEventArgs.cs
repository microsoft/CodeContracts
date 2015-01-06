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
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Windows
{
  // Summary:
  //     Provides data for the System.Windows.Application.UnhandledException event.
  public class ApplicationUnhandledExceptionEventArgs : EventArgs
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.ApplicationUnhandledExceptionEventArgs
    //     class.
    //
    // Parameters:
    //   ex:
    //     The exception that is being thrown as unhandled.
    //
    //   handled:
    //     A value that indicates whether the exception has been handled and should
    //     not be processed further.
    public ApplicationUnhandledExceptionEventArgs(Exception ex, bool handled)
    {
      Contract.Requires(ex != null);
    }

    // Summary:
    //     Gets or sets the unhandled exception.
    //
    // Returns:
    //     The unhandled exception.
    public Exception ExceptionObject {
      get
      {
        Contract.Ensures(Contract.Result<Exception>() != null);
        return default(Exception);
      }

      set
      {
        Contract.Requires(value != null);
      }
    }
    //
    // Summary:
    //     Gets or sets a value that indicates whether the exception is handled.
    //
    // Returns:
    //     true to mark the exception as handled, which indicates that Silverlight should
    //     not process it further; otherwise, false.
    // public bool Handled { get; set; }
  }
}

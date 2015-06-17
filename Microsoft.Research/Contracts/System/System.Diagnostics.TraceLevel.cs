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

using System.Diagnostics.Contracts;
using System;

namespace System.Diagnostics {
  // Summary:
  //     Specifies what messages to output for the System.Diagnostics.Debug, System.Diagnostics.Trace
  //     and System.Diagnostics.TraceSwitch classes.
  public enum TraceLevel {
    // Summary:
    //     Output no tracing and debugging messages.
    Off = 0,
    //
    // Summary:
    //     Output error-handling messages.
    Error = 1,
    //
    // Summary:
    //     Output warnings and error-handling messages.
    Warning = 2,
    //
    // Summary:
    //     Output informational messages, warnings, and error-handling messages.
    Info = 3,
    //
    // Summary:
    //     Output all debugging and tracing messages.
    Verbose = 4,
  }
}

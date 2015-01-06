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
//using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Threading {
  // Summary:
  //     Provides an explicit layout that is visible from unmanaged code and that
  //     will have the same layout as the Win32 OVERLAPPED structure with additional
  //     reserved fields at the end.
  [ComVisible(true)]
  public struct NativeOverlapped {
    // Summary:
    //     Specifies the handle to an event set to the signaled state when the operation
    //     is complete. The calling process must set this member either to zero or to
    //     a valid event handle before calling any overlapped functions.
    public IntPtr EventHandle;
    //
    // Summary:
    //     Specifies the length of the data transferred. Reserved for operating system
    //     use.
    public IntPtr InternalHigh;
    //
    // Summary:
    //     Specifies a system-dependent status. Reserved for operating system use.
    public IntPtr InternalLow;
    //
    // Summary:
    //     Specifies the high word of the byte offset at which to start the transfer.
    public int OffsetHigh;
    //
    // Summary:
    //     Specifies a file position at which to start the transfer.
    public int OffsetLow;
  }
}

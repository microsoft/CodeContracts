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

namespace System.IO {
  // Summary:
  //     Contains constants for controlling the kind of access other System.IO.FileStream
  //     objects can have to the same file.
  public enum FileShare {
    // Summary:
    //     Declines sharing of the current file. Any request to open the file (by this
    //     process or another process) will fail until the file is closed.
    None = 0,
    //
    // Summary:
    //     Allows subsequent opening of the file for reading. If this flag is not specified,
    //     any request to open the file for reading (by this process or another process)
    //     will fail until the file is closed. However, even if this flag is specified,
    //     additional permissions might still be needed to access the file.
    Read = 1,
    //
    // Summary:
    //     Allows subsequent opening of the file for writing. If this flag is not specified,
    //     any request to open the file for writing (by this process or another process)
    //     will fail until the file is closed. However, even if this flag is specified,
    //     additional permissions might still be needed to access the file.
    Write = 2,
    //
    // Summary:
    //     Allows subsequent opening of the file for reading or writing. If this flag
    //     is not specified, any request to open the file for reading or writing (by
    //     this process or another process) will fail until the file is closed. However,
    //     even if this flag is specified, additional permissions might still be needed
    //     to access the file.
    ReadWrite = 3,
    //
    // Summary:
    //     Allows subsequent deleting of a file.
    Delete = 4,
    //
    // Summary:
    //     Makes the file handle inheritable by child processes. This is not directly
    //     supported by Win32.
    Inheritable = 16,
  }
}

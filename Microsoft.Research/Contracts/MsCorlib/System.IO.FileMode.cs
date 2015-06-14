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
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.IO {
  // Summary:
  //     Specifies how the operating system should open a file.
  public enum FileMode {
    // Summary:
    //     Specifies that the operating system should create a new file. This requires
    //     System.Security.Permissions.FileIOPermissionAccess.Write. If the file already
    //     exists, an System.IO.IOException is thrown.
    CreateNew = 1,
    //
    // Summary:
    //     Specifies that the operating system should create a new file. If the file
    //     already exists, it will be overwritten. This requires System.Security.Permissions.FileIOPermissionAccess.Write.
    //     System.IO.FileMode.Create is equivalent to requesting that if the file does
    //     not exist, use System.IO.FileMode.CreateNew; otherwise, use System.IO.FileMode.Truncate.
    Create = 2,
    //
    // Summary:
    //     Specifies that the operating system should open an existing file. The ability
    //     to open the file is dependent on the value specified by System.IO.FileAccess.
    //     A System.IO.FileNotFoundException is thrown if the file does not exist.
    Open = 3,
    //
    // Summary:
    //     Specifies that the operating system should open a file if it exists; otherwise,
    //     a new file should be created. If the file is opened with FileAccess.Read,
    //     System.Security.Permissions.FileIOPermissionAccess.Read is required. If the
    //     file access is FileAccess.Write then System.Security.Permissions.FileIOPermissionAccess.Write
    //     is required. If the file is opened with FileAccess.ReadWrite, both System.Security.Permissions.FileIOPermissionAccess.Read
    //     and System.Security.Permissions.FileIOPermissionAccess.Write are required.
    //     If the file access is FileAccess.Append, then System.Security.Permissions.FileIOPermissionAccess.Append
    //     is required.
    OpenOrCreate = 4,
    //
    // Summary:
    //     Specifies that the operating system should open an existing file. Once opened,
    //     the file should be truncated so that its size is zero bytes. This requires
    //     System.Security.Permissions.FileIOPermissionAccess.Write. Attempts to read
    //     from a file opened with Truncate cause an exception.
    Truncate = 5,
    //
    // Summary:
    //     Opens the file if it exists and seeks to the end of the file, or creates
    //     a new file. FileMode.Append can only be used in conjunction with FileAccess.Write.
    //     Attempting to seek to a position before the end of the file will throw an
    //     System.IO.IOException and any attempt to read fails and throws an System.NotSupportedException.
    Append = 6,
  }
}

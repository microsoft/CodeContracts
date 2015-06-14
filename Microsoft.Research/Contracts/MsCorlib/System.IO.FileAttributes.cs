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
  //     Provides attributes for files and directories.
  public enum FileAttributes {
    // Summary:
    //     The file is read-only.
    ReadOnly = 1,
    //
    // Summary:
    //     The file is hidden, and thus is not included in an ordinary directory listing.
    Hidden = 2,
    //
    // Summary:
    //     The file is a system file. The file is part of the operating system or is
    //     used exclusively by the operating system.
    System = 4,
    //
    // Summary:
    //     The file is a directory.
    Directory = 16,
    //
    // Summary:
    //     The file's archive status. Applications use this attribute to mark files
    //     for backup or removal.
    Archive = 32,
    //
    // Summary:
    //     Reserved for future use.
    Device = 64,
    //
    // Summary:
    //     The file is normal and has no other attributes set. This attribute is valid
    //     only if used alone.
    Normal = 128,
    //
    // Summary:
    //     The file is temporary. File systems attempt to keep all of the data in memory
    //     for quicker access rather than flushing the data back to mass storage. A
    //     temporary file should be deleted by the application as soon as it is no longer
    //     needed.
    Temporary = 256,
    //
    // Summary:
    //     The file is a sparse file. Sparse files are typically large files whose data
    //     are mostly zeros.
    SparseFile = 512,
    //
    // Summary:
    //     The file contains a reparse point, which is a block of user-defined data
    //     associated with a file or a directory.
    ReparsePoint = 1024,
    //
    // Summary:
    //     The file is compressed.
    Compressed = 2048,
    //
    // Summary:
    //     The file is offline. The data of the file is not immediately available.
    Offline = 4096,
    //
    // Summary:
    //     The file will not be indexed by the operating system's content indexing service.
    NotContentIndexed = 8192,
    //
    // Summary:
    //     The file or directory is encrypted. For a file, this means that all data
    //     in the file is encrypted. For a directory, this means that encryption is
    //     the default for newly created files and directories.
    Encrypted = 16384,
  }
}


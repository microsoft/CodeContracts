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

// File System.IO.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.IO
{
  public enum DriveType
  {
    Unknown = 0, 
    NoRootDirectory = 1, 
    Removable = 2, 
    Fixed = 3, 
    Network = 4, 
    CDRom = 5, 
    Ram = 6, 
  }

  public enum FileAccess
  {
    Read = 1, 
    Write = 2, 
    ReadWrite = 3, 
  }

  public enum FileAttributes
  {
    ReadOnly = 1, 
    Hidden = 2, 
    System = 4, 
    Directory = 16, 
    Archive = 32, 
    Device = 64, 
    Normal = 128, 
    Temporary = 256, 
    SparseFile = 512, 
    ReparsePoint = 1024, 
    Compressed = 2048, 
    Offline = 4096, 
    NotContentIndexed = 8192, 
    Encrypted = 16384, 
  }

  public enum FileMode
  {
    CreateNew = 1, 
    Create = 2, 
    Open = 3, 
    OpenOrCreate = 4, 
    Truncate = 5, 
    Append = 6, 
  }

  public enum FileOptions
  {
    None = 0, 
    WriteThrough = -2147483648, 
    Asynchronous = 1073741824, 
    RandomAccess = 268435456, 
    DeleteOnClose = 67108864, 
    SequentialScan = 134217728, 
    Encrypted = 16384, 
  }

  public enum FileShare
  {
    None = 0, 
    Read = 1, 
    Write = 2, 
    ReadWrite = 3, 
    Delete = 4, 
    Inheritable = 16, 
  }

  public enum SearchOption
  {
    TopDirectoryOnly = 0, 
    AllDirectories = 1, 
  }

  public enum SeekOrigin
  {
    Begin = 0, 
    Current = 1, 
    End = 2, 
  }
}

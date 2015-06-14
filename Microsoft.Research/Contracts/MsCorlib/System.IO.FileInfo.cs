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

namespace System.IO
{

  public class FileInfo
  {

    extern public virtual bool Exists
    {
      get;
    }

    public virtual string Name
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public string DirectoryName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public Int64 Length
    {
      get
      {
        Contract.Ensures(Contract.Result<Int64>() >= 0);
        return default(long);
      }
    }

    public DirectoryInfo Directory
    {
      get
      {
        Contract.Ensures(Contract.Result<DirectoryInfo>() != null);
        return default(DirectoryInfo);
      }
    }

    public void MoveTo(string destFileName)
    {
      Contract.Requires(!String.IsNullOrEmpty(destFileName));
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file is not found.");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs, such as the destination file already exists or the destination device is not ready.");

    }

    [Pure]
    public FileStream OpenWrite()
    {
      Contract.Ensures(Contract.Result<FileStream>() != null);
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");

      return default(FileStream);
    }

    [Pure]
    public FileStream OpenRead()
    {
      Contract.Ensures(Contract.Result<FileStream>() != null);
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"The file is already open.");
      return default(FileStream);
    }

    [Pure]
    public FileStream Open(FileMode mode, FileAccess access, FileShare share)
    {
      Contract.Ensures(Contract.Result<FileStream>() != null);
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file is not found.");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"The file is already open.");
      return default(FileStream);
    }

    [Pure]
    public FileStream Open(FileMode mode, FileAccess access)
    {
      Contract.Ensures(Contract.Result<FileStream>() != null);
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file is not found.");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"The file is already open.");
      return default(FileStream);
    }
    [Pure]
    public FileStream Open(FileMode mode)
    {
      Contract.Ensures(Contract.Result<FileStream>() != null);
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file is not found.");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"The file is already open.");

      return default(FileStream);
    }
#if false
    public void Delete()
    {
    }
#endif
    public FileInfo CopyTo(string destFileName, bool overwrite)
    {
      Contract.Requires(!String.IsNullOrEmpty(destFileName));
      Contract.Ensures(Contract.Result<FileInfo>() != null);
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The directory specified in destFileName does not exist.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An error occurs, or the destination file already exists and overwrite is false.");
      return default(FileInfo);
    }
    public FileStream Create()
    {
      Contract.Ensures(Contract.Result<FileStream>() != null);

      return default(FileStream);
    }
    public FileInfo CopyTo(string destFileName)
    {
      Contract.Requires(!String.IsNullOrEmpty(destFileName));
      Contract.Ensures(Contract.Result<FileInfo>() != null);
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The directory specified in destFileName does not exist.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An error occurs, or the destination file already exists and overwrite is false.");

      return default(FileInfo);
    }
    public StreamWriter AppendText()
    {
      Contract.Ensures(Contract.Result<StreamWriter>() != null);

      return default(StreamWriter);
    }
    public StreamWriter CreateText()
    {
      Contract.Ensures(Contract.Result<StreamWriter>() != null);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"The disk is read-only.");

      return default(StreamWriter);
    }
    public StreamReader OpenText()
    {
      Contract.Ensures(Contract.Result<StreamReader>() != null);
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file is not found.");

      return default(StreamReader);
    }
    public FileInfo(string fileName)
    {
      Contract.Requires(!String.IsNullOrEmpty(fileName));
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
    }
  }
}

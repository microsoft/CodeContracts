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

namespace System.IO
{

  public class DirectoryInfo
  {

    public DirectoryInfo Root
    {
      get
      {
        Contract.Ensures(Contract.Result<DirectoryInfo>() != null);
        return default(DirectoryInfo);
      }
    }

    extern public virtual  bool Exists
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

    extern public DirectoryInfo Parent
    {
      get;
    }

#if false
    public void Delete(bool recursive)
    {

    }
    public void Delete()
    {

    }
#endif
    public void MoveTo(string destDirName)
    {
      Contract.Requires(!String.IsNullOrEmpty(destDirName));
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An attempt was made to move a directory to a different volume. -or- destDirName already exists. -or- You are not authorized to access this path. -or- The directory being moved and the destination directory have the same name");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The destination directory cannot be found");
    }

#if !SILVERLIGHT_4_0 && !SILVERLIGHT_5_0
    public DirectoryInfo[] GetDirectories(string searchPattern)
    {
      Contract.Requires(searchPattern != null);
      Contract.Ensures(Contract.Result<DirectoryInfo[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<DirectoryInfo[]>(), dir => dir != null));
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The path encapsulated in the DirectoryInfo object is invalid, such as being on an unmapped drive.");

      return default(DirectoryInfo[]);
    }
    public FileSystemInfo[] GetFileSystemInfos()
    {
      Contract.Ensures(Contract.Result<FileSystemInfo[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<FileSystemInfo[]>(), fs => fs != null));
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The path is invalid (for example, it is on an unmapped drive).");

      return default(FileSystemInfo[]);
    }
    public FileSystemInfo[] GetFileSystemInfos(string searchPattern)
    {
      Contract.Requires(searchPattern != null);
      Contract.Ensures(Contract.Result<FileSystemInfo[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<FileSystemInfo[]>(), fs => fs != null));
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The path is invalid (for example, it is on an unmapped drive).");

      return default(FileSystemInfo[]);
    }
    public DirectoryInfo[] GetDirectories()
    {
      Contract.Ensures(Contract.Result<DirectoryInfo[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<DirectoryInfo[]>(), dir => dir != null));
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The path encapsulated in the DirectoryInfo object is invalid, such as being on an unmapped drive.");

      return default(DirectoryInfo[]);
    }
    public FileInfo[] GetFiles()
    {
      Contract.Ensures(Contract.Result<FileInfo[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<FileInfo[]>(), file => file != null));
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The path is invalid (for example, it is on an unmapped drive).");

      return default(FileInfo[]);
    }

    public FileInfo[] GetFiles(string searchPattern)
    {
      Contract.Requires(searchPattern != null);
      Contract.Ensures(Contract.Result<FileInfo[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<FileInfo[]>(), file => file != null));
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The path is invalid (for example, it is on an unmapped drive).");

      return default(FileInfo[]);
    }
#endif

#if false
    public void Create()
    {

    }
#endif
    public DirectoryInfo CreateSubdirectory(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Ensures(Contract.Result<DirectoryInfo>() != null);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"The subdirectory cannot be created. -or- A file or directory already has the name specified by path");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");
      return default(DirectoryInfo);
    }
    public DirectoryInfo(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
    }

#if NETFRAMEWORK_4_0

    public IEnumerable<DirectoryInfo> EnumerateDirectories()
    {
      Contract.Ensures(Contract.Result<IEnumerable<DirectoryInfo>>() != null);
      return null;
    }
    public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern)
    {
      Contract.Requires(searchPattern != null);
      Contract.Ensures(Contract.Result<IEnumerable<DirectoryInfo>>() != null);
      return null;
    }
    
    //public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern, SearchOption searchOption);

    public IEnumerable<FileInfo> EnumerateFiles()
    {
      Contract.Ensures(Contract.Result<IEnumerable<FileInfo>>() != null);
      return null;
    }
    public IEnumerable<FileInfo> EnumerateFiles(string searchPattern)
    {
      Contract.Requires(searchPattern != null);
      Contract.Ensures(Contract.Result<IEnumerable<FileInfo>>() != null);
      return null;
    }

    //public IEnumerable<FileInfo> EnumerateFiles(string searchPattern, SearchOption searchOption);

    public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos()
    {
        Contract.Ensures(Contract.Result<IEnumerable<FileSystemInfo>>() != null);
        return null;
    }

    public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern)
    {
      Contract.Requires(searchPattern != null);
      Contract.Ensures(Contract.Result<IEnumerable<FileSystemInfo>>() != null);
      return null;
    }


    // public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern, SearchOption searchOption);

#endif
  }
}

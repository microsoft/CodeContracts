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

// File System.IO.DirectoryInfo.cs
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
  sealed public partial class DirectoryInfo : FileSystemInfo
  {
    #region Methods and constructors
    public void Create(System.Security.AccessControl.DirectorySecurity directorySecurity)
    {
    }

    public void Create()
    {
    }

    public System.IO.DirectoryInfo CreateSubdirectory(string path, System.Security.AccessControl.DirectorySecurity directorySecurity)
    {
      Contract.Ensures(Contract.Result<System.IO.DirectoryInfo>() != null);
      Contract.Ensures(this.FullPath != null);

      return default(System.IO.DirectoryInfo);
    }

    public System.IO.DirectoryInfo CreateSubdirectory(string path)
    {
      Contract.Ensures(Contract.Result<System.IO.DirectoryInfo>() != null);
      Contract.Ensures(this.FullPath != null);

      return default(System.IO.DirectoryInfo);
    }

    public void Delete(bool recursive)
    {
    }

    public override void Delete()
    {
    }

    public DirectoryInfo(string path)
    {
      Contract.Ensures((this.OriginalPath.Length - path.Length) <= 0);
      Contract.Ensures(this.OriginalPath != null);
    }

    public IEnumerable<System.IO.DirectoryInfo> EnumerateDirectories(string searchPattern, SearchOption searchOption)
    {
      return default(IEnumerable<System.IO.DirectoryInfo>);
    }

    public IEnumerable<System.IO.DirectoryInfo> EnumerateDirectories()
    {
      return default(IEnumerable<System.IO.DirectoryInfo>);
    }

    public IEnumerable<System.IO.DirectoryInfo> EnumerateDirectories(string searchPattern)
    {
      return default(IEnumerable<System.IO.DirectoryInfo>);
    }

    public IEnumerable<FileInfo> EnumerateFiles(string searchPattern, SearchOption searchOption)
    {
      return default(IEnumerable<FileInfo>);
    }

    public IEnumerable<FileInfo> EnumerateFiles(string searchPattern)
    {
      return default(IEnumerable<FileInfo>);
    }

    public IEnumerable<FileInfo> EnumerateFiles()
    {
      return default(IEnumerable<FileInfo>);
    }

    public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern, SearchOption searchOption)
    {
      return default(IEnumerable<FileSystemInfo>);
    }

    public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern)
    {
      return default(IEnumerable<FileSystemInfo>);
    }

    public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos()
    {
      return default(IEnumerable<FileSystemInfo>);
    }

    public System.Security.AccessControl.DirectorySecurity GetAccessControl()
    {
      return default(System.Security.AccessControl.DirectorySecurity);
    }

    public System.Security.AccessControl.DirectorySecurity GetAccessControl(System.Security.AccessControl.AccessControlSections includeSections)
    {
      return default(System.Security.AccessControl.DirectorySecurity);
    }

    public System.IO.DirectoryInfo[] GetDirectories()
    {
      Contract.Ensures(Contract.Result<System.IO.DirectoryInfo[]>() != null);

      return default(System.IO.DirectoryInfo[]);
    }

    public System.IO.DirectoryInfo[] GetDirectories(string searchPattern)
    {
      Contract.Ensures(Contract.Result<System.IO.DirectoryInfo[]>() != null);

      return default(System.IO.DirectoryInfo[]);
    }

    public System.IO.DirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption)
    {
      Contract.Ensures(Contract.Result<System.IO.DirectoryInfo[]>() != null);

      return default(System.IO.DirectoryInfo[]);
    }

    public FileInfo[] GetFiles(string searchPattern)
    {
      Contract.Ensures(Contract.Result<System.IO.FileInfo[]>() != null);

      return default(FileInfo[]);
    }

    public FileInfo[] GetFiles()
    {
      Contract.Ensures(Contract.Result<System.IO.FileInfo[]>() != null);

      return default(FileInfo[]);
    }

    public FileInfo[] GetFiles(string searchPattern, SearchOption searchOption)
    {
      Contract.Ensures(Contract.Result<System.IO.FileInfo[]>() != null);

      return default(FileInfo[]);
    }

    public FileSystemInfo[] GetFileSystemInfos(string searchPattern)
    {
      Contract.Ensures(Contract.Result<System.IO.FileSystemInfo[]>() != null);

      return default(FileSystemInfo[]);
    }

    public FileSystemInfo[] GetFileSystemInfos()
    {
      Contract.Ensures(Contract.Result<System.IO.FileSystemInfo[]>() != null);

      return default(FileSystemInfo[]);
    }

    public FileSystemInfo[] GetFileSystemInfos(string searchPattern, SearchOption searchOption)
    {
      Contract.Ensures(Contract.Result<System.IO.FileSystemInfo[]>() != null);

      return default(FileSystemInfo[]);
    }

    public void MoveTo(string destDirName)
    {
      Contract.Ensures(destDirName == this.OriginalPath);
      Contract.Ensures(this.OriginalPath != null);
    }

    public void SetAccessControl(System.Security.AccessControl.DirectorySecurity directorySecurity)
    {
    }

    public override string ToString()
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public override bool Exists
    {
      get
      {
        return default(bool);
      }
    }

    public override string Name
    {
      get
      {
        return default(string);
      }
    }

    public System.IO.DirectoryInfo Parent
    {
      get
      {
        Contract.Ensures(this.FullPath != null);

        return default(System.IO.DirectoryInfo);
      }
    }

    public System.IO.DirectoryInfo Root
    {
      get
      {
        Contract.Ensures(0 <= this.FullPath.Length);
        Contract.Ensures(Contract.Result<System.IO.DirectoryInfo>() != null);
        Contract.Ensures(this.FullPath != null);

        return default(System.IO.DirectoryInfo);
      }
    }
    #endregion
  }
}

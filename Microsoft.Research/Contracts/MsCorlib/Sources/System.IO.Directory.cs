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

// File System.IO.Directory.cs
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
  static public partial class Directory
  {
    #region Methods and constructors
    public static DirectoryInfo CreateDirectory(string path)
    {
      Contract.Ensures(Contract.Result<System.IO.DirectoryInfo>() != null);

      return default(DirectoryInfo);
    }

    public static DirectoryInfo CreateDirectory(string path, System.Security.AccessControl.DirectorySecurity directorySecurity)
    {
      Contract.Ensures(Contract.Result<System.IO.DirectoryInfo>() != null);

      return default(DirectoryInfo);
    }

    public static void Delete(string path)
    {
    }

    public static void Delete(string path, bool recursive)
    {
    }

    public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
    {
      return default(IEnumerable<string>);
    }

    public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
    {
      return default(IEnumerable<string>);
    }

    public static IEnumerable<string> EnumerateDirectories(string path)
    {
      return default(IEnumerable<string>);
    }

    public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
    {
      return default(IEnumerable<string>);
    }

    public static IEnumerable<string> EnumerateFiles(string path, string searchPattern)
    {
      return default(IEnumerable<string>);
    }

    public static IEnumerable<string> EnumerateFiles(string path)
    {
      return default(IEnumerable<string>);
    }

    public static IEnumerable<string> EnumerateFileSystemEntries(string path)
    {
      return default(IEnumerable<string>);
    }

    public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
    {
      return default(IEnumerable<string>);
    }

    public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)
    {
      return default(IEnumerable<string>);
    }

    public static bool Exists(string path)
    {
      return default(bool);
    }

    public static System.Security.AccessControl.DirectorySecurity GetAccessControl(string path)
    {
      Contract.Ensures(Contract.Result<System.Security.AccessControl.DirectorySecurity>() != null);

      return default(System.Security.AccessControl.DirectorySecurity);
    }

    public static System.Security.AccessControl.DirectorySecurity GetAccessControl(string path, System.Security.AccessControl.AccessControlSections includeSections)
    {
      Contract.Ensures(Contract.Result<System.Security.AccessControl.DirectorySecurity>() != null);

      return default(System.Security.AccessControl.DirectorySecurity);
    }

    public static DateTime GetCreationTime(string path)
    {
      return default(DateTime);
    }

    public static DateTime GetCreationTimeUtc(string path)
    {
      return default(DateTime);
    }

    public static string GetCurrentDirectory()
    {
      Contract.Ensures(0 <= string.Empty.Length);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static string[] GetDirectories(string path, string searchPattern)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public static string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public static string[] GetDirectories(string path)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public static string GetDirectoryRoot(string path)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static string[] GetFiles(string path)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public static string[] GetFiles(string path, string searchPattern)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public static string[] GetFileSystemEntries(string path)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public static string[] GetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public static string[] GetFileSystemEntries(string path, string searchPattern)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public static DateTime GetLastAccessTime(string path)
    {
      return default(DateTime);
    }

    public static DateTime GetLastAccessTimeUtc(string path)
    {
      return default(DateTime);
    }

    public static DateTime GetLastWriteTime(string path)
    {
      return default(DateTime);
    }

    public static DateTime GetLastWriteTimeUtc(string path)
    {
      return default(DateTime);
    }

    public static string[] GetLogicalDrives()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public static DirectoryInfo GetParent(string path)
    {
      return default(DirectoryInfo);
    }

    public static void Move(string sourceDirName, string destDirName)
    {
    }

    public static void SetAccessControl(string path, System.Security.AccessControl.DirectorySecurity directorySecurity)
    {
    }

    public static void SetCreationTime(string path, DateTime creationTime)
    {
    }

    public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
    {
    }

    public static void SetCurrentDirectory(string path)
    {
      Contract.Ensures(path.Length <= 259);
    }

    public static void SetLastAccessTime(string path, DateTime lastAccessTime)
    {
    }

    public static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
    {
    }

    public static void SetLastWriteTime(string path, DateTime lastWriteTime)
    {
    }

    public static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
    {
    }
    #endregion
  }
}

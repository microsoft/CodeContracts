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

// File System.IO.File.cs
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
  static public partial class File
  {
    #region Methods and constructors
    public static void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
    {
    }

    public static void AppendAllLines(string path, IEnumerable<string> contents)
    {
    }

    public static void AppendAllText(string path, string contents, Encoding encoding)
    {
    }

    public static void AppendAllText(string path, string contents)
    {
    }

    public static StreamWriter AppendText(string path)
    {
      Contract.Ensures(Contract.Result<System.IO.StreamWriter>() != null);

      return default(StreamWriter);
    }

    public static void Copy(string sourceFileName, string destFileName)
    {
    }

    public static void Copy(string sourceFileName, string destFileName, bool overwrite)
    {
    }

    public static FileStream Create(string path, int bufferSize, FileOptions options, System.Security.AccessControl.FileSecurity fileSecurity)
    {
      Contract.Ensures(Contract.Result<System.IO.FileStream>() != null);

      return default(FileStream);
    }

    public static FileStream Create(string path, int bufferSize)
    {
      Contract.Ensures(Contract.Result<System.IO.FileStream>() != null);

      return default(FileStream);
    }

    public static FileStream Create(string path)
    {
      Contract.Ensures(Contract.Result<System.IO.FileStream>() != null);

      return default(FileStream);
    }

    public static FileStream Create(string path, int bufferSize, FileOptions options)
    {
      Contract.Ensures(Contract.Result<System.IO.FileStream>() != null);

      return default(FileStream);
    }

    public static StreamWriter CreateText(string path)
    {
      Contract.Ensures(Contract.Result<System.IO.StreamWriter>() != null);

      return default(StreamWriter);
    }

    public static void Decrypt(string path)
    {
    }

    public static void Delete(string path)
    {
    }

    public static void Encrypt(string path)
    {
    }

    public static bool Exists(string path)
    {
      return default(bool);
    }

    public static System.Security.AccessControl.FileSecurity GetAccessControl(string path, System.Security.AccessControl.AccessControlSections includeSections)
    {
      Contract.Ensures(Contract.Result<System.Security.AccessControl.FileSecurity>() != null);

      return default(System.Security.AccessControl.FileSecurity);
    }

    public static System.Security.AccessControl.FileSecurity GetAccessControl(string path)
    {
      Contract.Ensures(Contract.Result<System.Security.AccessControl.FileSecurity>() != null);

      return default(System.Security.AccessControl.FileSecurity);
    }

    public static FileAttributes GetAttributes(string path)
    {
      Contract.Ensures(((System.IO.FileAttributes)(Int32.MinValue)) <= Contract.Result<System.IO.FileAttributes>());
      Contract.Ensures(Contract.Result<System.IO.FileAttributes>() <= ((System.IO.FileAttributes)(Int32.MaxValue)));

      return default(FileAttributes);
    }

    public static DateTime GetCreationTime(string path)
    {
      return default(DateTime);
    }

    public static DateTime GetCreationTimeUtc(string path)
    {
      return default(DateTime);
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

    public static void Move(string sourceFileName, string destFileName)
    {
    }

    public static FileStream Open(string path, FileMode mode, FileAccess access)
    {
      Contract.Ensures(Contract.Result<System.IO.FileStream>() != null);

      return default(FileStream);
    }

    public static FileStream Open(string path, FileMode mode)
    {
      Contract.Ensures(Contract.Result<System.IO.FileStream>() != null);

      return default(FileStream);
    }

    public static FileStream Open(string path, FileMode mode, FileAccess access, FileShare share)
    {
      Contract.Ensures(Contract.Result<System.IO.FileStream>() != null);

      return default(FileStream);
    }

    public static FileStream OpenRead(string path)
    {
      Contract.Ensures(Contract.Result<System.IO.FileStream>() != null);

      return default(FileStream);
    }

    public static StreamReader OpenText(string path)
    {
      Contract.Ensures(Contract.Result<System.IO.StreamReader>() != null);

      return default(StreamReader);
    }

    public static FileStream OpenWrite(string path)
    {
      Contract.Ensures(Contract.Result<System.IO.FileStream>() != null);

      return default(FileStream);
    }

    public static byte[] ReadAllBytes(string path)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }

    public static string[] ReadAllLines(string path)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public static string[] ReadAllLines(string path, Encoding encoding)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public static string ReadAllText(string path, Encoding encoding)
    {
      return default(string);
    }

    public static string ReadAllText(string path)
    {
      return default(string);
    }

    public static IEnumerable<string> ReadLines(string path, Encoding encoding)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.IEnumerable<string>>() != null);

      return default(IEnumerable<string>);
    }

    public static IEnumerable<string> ReadLines(string path)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.IEnumerable<string>>() != null);

      return default(IEnumerable<string>);
    }

    public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
    {
    }

    public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
    {
    }

    public static void SetAccessControl(string path, System.Security.AccessControl.FileSecurity fileSecurity)
    {
    }

    public static void SetAttributes(string path, FileAttributes fileAttributes)
    {
    }

    public static void SetCreationTime(string path, DateTime creationTime)
    {
    }

    public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
    {
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

    public static void WriteAllBytes(string path, byte[] bytes)
    {
    }

    public static void WriteAllLines(string path, string[] contents)
    {
    }

    public static void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
    {
    }

    public static void WriteAllLines(string path, IEnumerable<string> contents)
    {
    }

    public static void WriteAllLines(string path, string[] contents, Encoding encoding)
    {
    }

    public static void WriteAllText(string path, string contents)
    {
    }

    public static void WriteAllText(string path, string contents, Encoding encoding)
    {
    }
    #endregion
  }
}

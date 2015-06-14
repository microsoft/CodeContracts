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

using System.Diagnostics.Contracts;
using System;
using System.Text;
using System.Collections.Generic;

namespace System.IO
{

  public class File
  {

    public static void Move(string sourceFileName, string destFileName)
    {
      Contract.Requires(sourceFileName != null);
      Contract.Requires(destFileName != null);
      Contract.Requires(sourceFileName.Length != 0);
      Contract.Requires(destFileName.Length != 0);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"The destination file already exists. -or- sourceFileName was not found.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path in sourceFileName or destFileName is invalid, (for example, it is on an unmapped drive).");

    }
    public static FileStream OpenWrite(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Ensures(Contract.Result<FileStream>() != null);
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");
      return default(FileStream);
    }
    public static FileStream OpenRead(string path)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.Ensures(Contract.Result<FileStream>() != null);
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
        Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");
        return default(FileStream);
    }
    public static void SetAttributes(string path, FileAttributes fileAttributes)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
        Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");

    }

#if !SILVERLIGHT
    public static FileAttributes GetAttributes(string path)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
        Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"This file is being used by another process.");

      return default(FileAttributes);
    }
#endif

#if !SILVERLIGHT
    public static DateTime GetLastWriteTimeUtc(string path)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");

      return default(DateTime);
    }
#endif
    public static DateTime GetLastWriteTime(string path)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");

      return default(DateTime);
    }
#if !SILVERLIGHT
    public static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");
    }
    public static void SetLastWriteTime(string path, DateTime lastWriteTime)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");
    }
    public static DateTime GetLastAccessTimeUtc(string path)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");

      return default(DateTime);
    }
#endif
    public static DateTime GetLastAccessTime(string path)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");

      return default(DateTime);
    }
#if !SILVERLIGHT
    public static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");

    }
    public static void SetLastAccessTime(string path, DateTime lastAccessTime)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");

    }
    public static DateTime GetCreationTimeUtc(string path)
    {

      return default(DateTime);
    }
#endif
    public static DateTime GetCreationTime(string path)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");

      return default(DateTime);
    }
#if !SILVERLIGHT
    public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurred while performing the operation. ");

    }
    public static void SetCreationTime(string path, DateTime creationTime)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurred while performing the operation. ");
    }
#endif
    public static FileStream Open(string path, FileMode mode, FileAccess access, FileShare share)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
        Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurred while performing the operation. ");

      Contract.Ensures(Contract.Result<FileStream>() != null);
      return default(FileStream);
    }
    public static FileStream Open(string path, FileMode mode, FileAccess access)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
        Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurred while performing the operation. ");

      Contract.Ensures(Contract.Result<FileStream>() != null);
      return default(FileStream);
    }
    public static FileStream Open(string path, FileMode mode)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
        Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurred while performing the operation. ");
        Contract.Ensures(Contract.Result<FileStream>() != null);
        return default(FileStream);
    }
    [Pure]
    public static bool Exists(string path)
    {
      Contract.Ensures(!Contract.Result<bool>() || path != null);
      Contract.Ensures(!Contract.Result<bool>() || path.Length > 0); // trivial, but useful for clients

      return default(bool);
    }
    public static void Delete(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"The specified file is in use.");

    }
    public static FileStream Create(string path, int bufferSize)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Ensures(Contract.Result<FileStream>() != null);
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurred while creating the file.");
      return default(FileStream);
    }
    public static FileStream Create(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Ensures(Contract.Result<FileStream>() != null);
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The path specified in sourceFileName or destFileName is invalid (for example, it is on an unmapped drive). 
");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurred while creating the file.");
      return default(FileStream);
    }
    public static void Copy(string sourceFileName, string destFileName, bool overwrite)
    {
        Contract.Requires(!String.IsNullOrEmpty(sourceFileName));
        Contract.Requires(!String.IsNullOrEmpty(destFileName));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
        Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"destFileName exists and overwrite is false. -or- An I/O error has occurred.");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");
    }
    public static void Copy(string sourceFileName, string destFileName)
    {
        Contract.Requires(!String.IsNullOrEmpty(sourceFileName));
        Contract.Requires(!String.IsNullOrEmpty(destFileName));
        Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
        Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"destFileName exists and overwrite is false. -or- An I/O error has occurred.");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");
    }
    public static StreamWriter AppendText(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));

      Contract.Ensures(Contract.Result<StreamWriter>() != null);
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      return default(StreamWriter);
    }
    public static StreamWriter CreateText(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));

      Contract.Ensures(Contract.Result<StreamWriter>() != null);
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      return default(StreamWriter);
    }
    public static StreamReader OpenText(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Ensures(Contract.Result<StreamReader>() != null);
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");
      return default(StreamReader);
    }

#if !SILVERLIGHT
    public static byte[] ReadAllBytes(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Requires(path.Length > 0);
      Contract.Ensures(Contract.Result<byte[]>() != null);
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error has occurred.");
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");

      return default(byte[]);
    }


    public static string ReadAllText(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Requires(path.Length > 0);
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error has occurred.");
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");

      return default(string);
    }

    public static string ReadAllText(string path, Encoding encoding)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Requires(path.Length > 0);
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error has occurred.");
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");

      return default(string);    
    }
#endif

#if !SILVERLIGHT
    public static string[] ReadAllLines(string fileName) {
      Contract.Requires(!String.IsNullOrEmpty(fileName));
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<string[]>(), line => line != null));
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error has occurred.");
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");

      return default(string[]);
    }

    public static string[] ReadAllLines(string fileName, System.Text.Encoding encoding) {
      Contract.Requires(!String.IsNullOrEmpty(fileName));
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<string[]>(), line => line != null));
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error has occurred.");
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file specified in path was not found.");

      return default(string[]);
    }
#elif SILVERLIGHT_4_0 || SILVERLIGHT_5_0 || NETFRAMEWORK_4_0
    public static IEnumerable<string> ReadLines(string fileName) {
      Contract.Requires(fileName != null);
      Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), line => line != null));

      return default(string[]);
    }

    public static IEnumerable<string> ReadLines(string fileName, System.Text.Encoding encoding) {
      Contract.Requires(fileName != null);
      Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), line => line != null));

      return default(string[]);
    }
#endif
  }
}

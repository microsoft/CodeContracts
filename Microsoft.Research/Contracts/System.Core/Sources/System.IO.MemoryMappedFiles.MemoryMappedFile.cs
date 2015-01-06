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

// File System.IO.MemoryMappedFiles.MemoryMappedFile.cs
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


namespace System.IO.MemoryMappedFiles
{
  public partial class MemoryMappedFile : IDisposable
  {
    #region Methods and constructors
    public static MemoryMappedFile CreateFromFile(FileStream fileStream, string mapName, long capacity, MemoryMappedFileAccess access, MemoryMappedFileSecurity memoryMappedFileSecurity, HandleInheritability inheritability, bool leaveOpen)
    {
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedFile>() != null);

      return default(MemoryMappedFile);
    }

    public static MemoryMappedFile CreateFromFile(string path, FileMode mode)
    {
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedFile>() != null);

      return default(MemoryMappedFile);
    }

    public static MemoryMappedFile CreateFromFile(string path, FileMode mode, string mapName, long capacity, MemoryMappedFileAccess access)
    {
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedFile>() != null);

      return default(MemoryMappedFile);
    }

    public static MemoryMappedFile CreateFromFile(string path, FileMode mode, string mapName, long capacity)
    {
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedFile>() != null);

      return default(MemoryMappedFile);
    }

    public static MemoryMappedFile CreateFromFile(string path, FileMode mode, string mapName)
    {
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedFile>() != null);

      return default(MemoryMappedFile);
    }

    public static MemoryMappedFile CreateFromFile(string path)
    {
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedFile>() != null);

      return default(MemoryMappedFile);
    }

    public static MemoryMappedFile CreateNew(string mapName, long capacity)
    {
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedFile>() != null);

      return default(MemoryMappedFile);
    }

    public static MemoryMappedFile CreateNew(string mapName, long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options, MemoryMappedFileSecurity memoryMappedFileSecurity, HandleInheritability inheritability)
    {
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedFile>() != null);

      return default(MemoryMappedFile);
    }

    public static MemoryMappedFile CreateNew(string mapName, long capacity, MemoryMappedFileAccess access)
    {
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedFile>() != null);

      return default(MemoryMappedFile);
    }

    public static MemoryMappedFile CreateOrOpen(string mapName, long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options, MemoryMappedFileSecurity memoryMappedFileSecurity, HandleInheritability inheritability)
    {
      Contract.Ensures(1 <= mapName.Length);
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedFile>() != null);

      return default(MemoryMappedFile);
    }

    public static MemoryMappedFile CreateOrOpen(string mapName, long capacity)
    {
      Contract.Ensures(1 <= mapName.Length);
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedFile>() != null);

      return default(MemoryMappedFile);
    }

    public static MemoryMappedFile CreateOrOpen(string mapName, long capacity, MemoryMappedFileAccess access)
    {
      Contract.Ensures(1 <= mapName.Length);
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedFile>() != null);

      return default(MemoryMappedFile);
    }

    public MemoryMappedViewAccessor CreateViewAccessor()
    {
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedViewAccessor>() != null);

      return default(MemoryMappedViewAccessor);
    }

    public MemoryMappedViewAccessor CreateViewAccessor(long offset, long size)
    {
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedViewAccessor>() != null);

      return default(MemoryMappedViewAccessor);
    }

    public MemoryMappedViewAccessor CreateViewAccessor(long offset, long size, MemoryMappedFileAccess access)
    {
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedViewAccessor>() != null);

      return default(MemoryMappedViewAccessor);
    }

    public MemoryMappedViewStream CreateViewStream(long offset, long size, MemoryMappedFileAccess access)
    {
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedViewStream>() != null);

      return default(MemoryMappedViewStream);
    }

    public MemoryMappedViewStream CreateViewStream(long offset, long size)
    {
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedViewStream>() != null);

      return default(MemoryMappedViewStream);
    }

    public MemoryMappedViewStream CreateViewStream()
    {
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedViewStream>() != null);

      return default(MemoryMappedViewStream);
    }

    public void Dispose()
    {
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public MemoryMappedFileSecurity GetAccessControl()
    {
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedFileSecurity>() != null);

      return default(MemoryMappedFileSecurity);
    }

    internal MemoryMappedFile()
    {
    }

    public static MemoryMappedFile OpenExisting(string mapName, MemoryMappedFileRights desiredAccessRights)
    {
      Contract.Ensures(1 <= mapName.Length);
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedFile>() != null);

      return default(MemoryMappedFile);
    }

    public static MemoryMappedFile OpenExisting(string mapName, MemoryMappedFileRights desiredAccessRights, HandleInheritability inheritability)
    {
      Contract.Ensures(1 <= mapName.Length);
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedFile>() != null);

      return default(MemoryMappedFile);
    }

    public static MemoryMappedFile OpenExisting(string mapName)
    {
      Contract.Ensures(1 <= mapName.Length);
      Contract.Ensures(Contract.Result<System.IO.MemoryMappedFiles.MemoryMappedFile>() != null);

      return default(MemoryMappedFile);
    }

    public void SetAccessControl(MemoryMappedFileSecurity memoryMappedFileSecurity)
    {
    }
    #endregion

    #region Properties and indexers
    public Microsoft.Win32.SafeHandles.SafeMemoryMappedFileHandle SafeMemoryMappedFileHandle
    {
      get
      {
        return default(Microsoft.Win32.SafeHandles.SafeMemoryMappedFileHandle);
      }
    }
    #endregion
  }
}

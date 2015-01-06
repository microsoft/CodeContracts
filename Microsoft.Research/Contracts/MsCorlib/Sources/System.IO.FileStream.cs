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

// File System.IO.FileStream.cs
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
  public partial class FileStream : Stream
  {
    #region Methods and constructors
    public override IAsyncResult BeginRead(byte[] array, int offset, int numBytes, AsyncCallback userCallback, Object stateObject)
    {
      return default(IAsyncResult);
    }

    public override IAsyncResult BeginWrite(byte[] array, int offset, int numBytes, AsyncCallback userCallback, Object stateObject)
    {
      return default(IAsyncResult);
    }

    protected override void Dispose(bool disposing)
    {
    }

    public override int EndRead(IAsyncResult asyncResult)
    {
      return default(int);
    }

    public override void EndWrite(IAsyncResult asyncResult)
    {
    }

    public FileStream(Microsoft.Win32.SafeHandles.SafeFileHandle handle, FileAccess access, int bufferSize)
    {
      Contract.Requires(handle != null);
    }

    public FileStream(Microsoft.Win32.SafeHandles.SafeFileHandle handle, FileAccess access)
    {
      Contract.Requires(handle != null);
    }

    public FileStream(string path, FileMode mode)
    {
    }

    public FileStream(Microsoft.Win32.SafeHandles.SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync)
    {
      Contract.Requires(handle != null);
    }

    public FileStream(string path, FileMode mode, FileAccess access, FileShare share)
    {
    }

    public FileStream(string path, FileMode mode, FileAccess access)
    {
    }

    public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync)
    {
    }

    public FileStream(string path, FileMode mode, System.Security.AccessControl.FileSystemRights rights, FileShare share, int bufferSize, FileOptions options, System.Security.AccessControl.FileSecurity fileSecurity)
    {
    }

    public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options)
    {
    }

    public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)
    {
    }

    public FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize)
    {
    }

    public FileStream(IntPtr handle, FileAccess access, bool ownsHandle)
    {
    }

    public FileStream(IntPtr handle, FileAccess access)
    {
    }

    public FileStream(string path, FileMode mode, System.Security.AccessControl.FileSystemRights rights, FileShare share, int bufferSize, FileOptions options)
    {
    }

    public FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync)
    {
    }

    public virtual new void Flush(bool flushToDisk)
    {
    }

    public override void Flush()
    {
    }

    public System.Security.AccessControl.FileSecurity GetAccessControl()
    {
      Contract.Ensures(Contract.Result<System.Security.AccessControl.FileSecurity>() != null);

      return default(System.Security.AccessControl.FileSecurity);
    }

    public virtual new void Lock(long position, long length)
    {
    }

    public override int Read(byte[] array, int offset, int count)
    {
      return default(int);
    }

    public override int ReadByte()
    {
      return default(int);
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      return default(long);
    }

    public void SetAccessControl(System.Security.AccessControl.FileSecurity fileSecurity)
    {
    }

    public override void SetLength(long value)
    {
    }

    public virtual new void Unlock(long position, long length)
    {
    }

    public override void Write(byte[] array, int offset, int count)
    {
    }

    public override void WriteByte(byte value)
    {
    }
    #endregion

    #region Properties and indexers
    public override bool CanRead
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanSeek
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanWrite
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new IntPtr Handle
    {
      get
      {
        return default(IntPtr);
      }
    }

    public virtual new bool IsAsync
    {
      get
      {
        return default(bool);
      }
    }

    public override long Length
    {
      get
      {
        return default(long);
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
    }

    public override long Position
    {
      get
      {
        return default(long);
      }
      set
      {
      }
    }

    public virtual new Microsoft.Win32.SafeHandles.SafeFileHandle SafeFileHandle
    {
      get
      {
        return default(Microsoft.Win32.SafeHandles.SafeFileHandle);
      }
    }
    #endregion
  }
}

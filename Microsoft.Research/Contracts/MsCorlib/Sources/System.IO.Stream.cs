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

// File System.IO.Stream.cs
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
  abstract public partial class Stream : MarshalByRefObject, IDisposable
  {
    #region Methods and constructors
    public virtual new IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public virtual new IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public virtual new void Close()
    {
    }

    public void CopyTo(Stream destination)
    {
    }

    public void CopyTo(Stream destination, int bufferSize)
    {
    }

    protected virtual new System.Threading.WaitHandle CreateWaitHandle()
    {
      return default(System.Threading.WaitHandle);
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public void Dispose()
    {
    }

    public virtual new int EndRead(IAsyncResult asyncResult)
    {
      return default(int);
    }

    public virtual new void EndWrite(IAsyncResult asyncResult)
    {
    }

    public abstract void Flush();

    protected virtual new void ObjectInvariant()
    {
    }

    public abstract int Read(byte[] buffer, int offset, int count);

    public virtual new int ReadByte()
    {
      return default(int);
    }

    public abstract long Seek(long offset, SeekOrigin origin);

    public abstract void SetLength(long value);

    protected Stream()
    {
    }

    public static Stream Synchronized(Stream stream)
    {
      Contract.Ensures(Contract.Result<System.IO.Stream>() != null);

      return default(Stream);
    }

    public abstract void Write(byte[] buffer, int offset, int count);

    public virtual new void WriteByte(byte value)
    {
    }
    #endregion

    #region Properties and indexers
    public abstract bool CanRead
    {
      get;
    }

    public abstract bool CanSeek
    {
      get;
    }

    public virtual new bool CanTimeout
    {
      get
      {
        return default(bool);
      }
    }

    public abstract bool CanWrite
    {
      get;
    }

    public abstract long Length
    {
      get;
    }

    public abstract long Position
    {
      get;
      set;
    }

    public virtual new int ReadTimeout
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new int WriteTimeout
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.IO.Stream Null;
    #endregion
  }
}

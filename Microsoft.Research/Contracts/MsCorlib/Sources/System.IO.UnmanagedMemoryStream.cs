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

// File System.IO.UnmanagedMemoryStream.cs
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
  public partial class UnmanagedMemoryStream : Stream
  {
    #region Methods and constructors
    protected override void Dispose(bool disposing)
    {
    }

    public override void Flush()
    {
    }

    protected void Initialize(System.Runtime.InteropServices.SafeBuffer buffer, long offset, long length, FileAccess access)
    {
    }

    unsafe protected void Initialize(byte* pointer, long length, long capacity, FileAccess access)
    {
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      return default(int);
    }

    public override int ReadByte()
    {
      return default(int);
    }

    public override long Seek(long offset, SeekOrigin loc)
    {
      return default(long);
    }

    public override void SetLength(long value)
    {
    }

    protected UnmanagedMemoryStream()
    {
    }

    public UnmanagedMemoryStream(System.Runtime.InteropServices.SafeBuffer buffer, long offset, long length, FileAccess access)
    {
    }

    public UnmanagedMemoryStream(System.Runtime.InteropServices.SafeBuffer buffer, long offset, long length)
    {
    }

    unsafe public UnmanagedMemoryStream(byte* pointer, long length)
    {
    }

    unsafe public UnmanagedMemoryStream(byte* pointer, long length, long capacity, FileAccess access)
    {
    }

    public override void Write(byte[] buffer, int offset, int count)
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

    public long Capacity
    {
      get
      {
        return default(long);
      }
    }

    public override long Length
    {
      get
      {
        return default(long);
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

    unsafe public byte* PositionPointer
    {
      get
      {
        return default(byte*);
      }
      set
      {
      }
    }
    #endregion
  }
}

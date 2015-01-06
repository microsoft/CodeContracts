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

// File System.IO.Compression.GZipStream.cs
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


namespace System.IO.Compression
{
  public partial class GZipStream : Stream
  {
    #region Methods and constructors
    public override IAsyncResult BeginRead(byte[] array, int offset, int count, AsyncCallback asyncCallback, Object asyncState)
    {
      return default(IAsyncResult);
    }

    public override IAsyncResult BeginWrite(byte[] array, int offset, int count, AsyncCallback asyncCallback, Object asyncState)
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

    public override void Flush()
    {
    }

    public GZipStream(Stream stream, CompressionMode mode)
    {
    }

    public GZipStream(Stream stream, CompressionMode mode, bool leaveOpen)
    {
    }

    public override int Read(byte[] array, int offset, int count)
    {
      return default(int);
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      return default(long);
    }

    public override void SetLength(long value)
    {
    }

    public override void Write(byte[] array, int offset, int count)
    {
    }
    #endregion

    #region Properties and indexers
    public Stream BaseStream
    {
      get
      {
        return default(Stream);
      }
    }

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
    #endregion
  }
}

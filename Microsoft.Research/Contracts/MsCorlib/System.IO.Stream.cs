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

namespace System.IO
{

  public abstract class Stream
  {
    protected Stream() { }

    public virtual Int64 Position
    {
      get
      {
        // Contract.Requires(this.CanSeek);
        Contract.Ensures(Contract.Result<Int64>() >= 0);
        return default(long);
      }
      set
      {
        // Contract.Requires(this.CanSeek);
        Contract.Requires(value >= 0);
      }
    }

    extern public virtual bool CanWrite
    {
      get;
    }

    extern public virtual bool CanSeek
    {
      get;
    }

    public virtual Int64 Length
    {
      get
      {
        Contract.Ensures(Contract.Result<long>() >= 0);
        return default(long);
      }
    }

    extern public virtual bool CanRead
    {
      get;
    }


    public abstract void WriteByte(byte value);

    public virtual void Write(Byte[] buffer, int offset, int count) {
      Contract.Requires(buffer != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(count <= (buffer.Length - offset));

    }

    public virtual int ReadByte()
    {
      Contract.Ensures(Contract.Result<int>() >= -1);
      return default(int);
    }

    /// <summary>
    /// made virtual from abstract to write contracts
    /// </summary>
    public virtual int Read(Byte[] buffer, int offset, int count)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires( count <= (buffer.Length - offset));

      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() <= count);

      return default(int);
    }

    public virtual void SetLength(Int64 value)
    {
      Contract.Requires(value >= 0);

    }

    public abstract Int64 Seek(Int64 arg0, SeekOrigin arg1);


    public virtual void EndWrite(IAsyncResult asyncResult)
    {
      Contract.Requires(asyncResult != null);

    }

    public virtual IAsyncResult BeginWrite(Byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(count <= (buffer.Length - offset));

      Contract.Ensures(Contract.Result<IAsyncResult>() != null);

      return default(IAsyncResult);
    }
    public virtual int EndRead(IAsyncResult asyncResult)
    {
      Contract.Requires(asyncResult != null);

      return default(int);
    }
    public virtual IAsyncResult BeginRead(Byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(count <= (buffer.Length - offset));

      return default(IAsyncResult);
    }
    extern public virtual void Flush();

    extern public virtual void Close();
  }
}

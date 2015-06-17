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

  public class BinaryWriter
  {
    protected BinaryWriter() { }

    public BinaryWriter(Stream output, System.Text.Encoding encoding)
    {
        Contract.Requires(output != null);
        Contract.Requires(encoding != null);
    }
    public BinaryWriter(Stream output)
    {
        Contract.Requires(output != null);
    }
    
    public virtual Stream BaseStream
    {
      get
      {
        Contract.Ensures(Contract.Result<Stream>() != null);
        return default(Stream);
      }
    }

    public virtual void Write(string value)
    {
      Contract.Requires(value != null);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(Single value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(UInt64 value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(Int64 value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(UInt32 value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(int value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(UInt16 value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(Int16 value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
#if !SILVERLIGHT
    public virtual void Write(Decimal value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
#endif
      public virtual void Write(double value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(Char[] chars, int index, int count)
    {
      Contract.Requires(chars != null);
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires((chars.Length - index) >= count);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(Char[] chars)
    {
      Contract.Requires(chars != null);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(Char ch)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(Byte[] buffer, int index, int count)
    {
        Contract.Requires(buffer != null);
        Contract.Requires(index >= 0);
        Contract.Requires(count >= 0);
        Contract.Requires((buffer.Length - index) >= count);
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(Byte[] buffer)
    {
      Contract.Requires(buffer != null);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(SByte value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(byte value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(bool value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual Int64 Seek(int offset, SeekOrigin origin)
    {

      return default(Int64);
    }
    public virtual void Flush()
    {

    }
    public virtual void Close()
    {

    }

  }
}

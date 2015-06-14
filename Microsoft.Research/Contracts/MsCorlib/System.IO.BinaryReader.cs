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

  public class BinaryReader
  {

    public virtual Stream BaseStream
    {
      get
      {
        Contract.Ensures(Contract.Result<Stream>() != null);
        return default(Stream);
      }
    }

    public virtual Byte[] ReadBytes(int count)
    {
      Contract.Requires(count >= 0);
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(Byte[]);
    }

    public virtual int Read(Byte[] buffer, int index, int count)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires((buffer.Length - index) >= count);
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() <= count);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");

      return default(int);
    }
    public virtual Char[] ReadChars(int count)
    {
      Contract.Requires(count >= 0);
      Contract.Ensures(Contract.Result<char[]>() != null);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");

      return default(Char[]);
    }
    public virtual int Read(Char[] buffer, int index, int count)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires((buffer.Length - index) >= count);
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() <= count);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");

      return default(int);
    }
    public virtual string ReadString()
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");

      return default(string);
    }
#if !SILVERLIGHT
    public virtual decimal ReadDecimal()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        return default(decimal);
    }
#endif
    public virtual double ReadDouble()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        return default(double);
    }
    public virtual float ReadSingle()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        return default(float);
    }
    public virtual UInt64 ReadUInt64()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        return default(UInt64);
    }
    public virtual Int64 ReadInt64()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        return default(Int64);
    }
    public virtual UInt32 ReadUInt32()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        return default(UInt32);
    }
    public virtual int ReadInt32()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        return default(int);
    }
    public virtual UInt16 ReadUInt16()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        return default(UInt16);
    }
    public virtual Int16 ReadInt16()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        return default(Int16);
    }
    public virtual Char ReadChar()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        return default(Char);
    }
    public virtual SByte ReadSByte()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        return default(SByte);
    }
    public virtual byte ReadByte()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        return default(byte);
    }
    public virtual bool ReadBoolean()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        return default(bool);
    }
    public virtual int Read()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        return default(int);
    }
    protected virtual void FillBuffer(int numBytes)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    [Pure]
    public virtual int PeekChar()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        return default(int);
    }
    public virtual void Close()
    {

    }
    public BinaryReader(Stream input, System.Text.Encoding encoding)
    {
      Contract.Requires(input != null);
      Contract.Requires(encoding != null);
    }
    public BinaryReader(Stream input)
    {
        Contract.Requires(input != null);
    }
  }
}

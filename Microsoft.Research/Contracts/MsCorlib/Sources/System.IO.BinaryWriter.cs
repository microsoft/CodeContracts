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

// File System.IO.BinaryWriter.cs
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
  public partial class BinaryWriter : IDisposable
  {
    #region Methods and constructors
    protected BinaryWriter()
    {
      Contract.Ensures(this.OutStream != null);
    }

    public BinaryWriter(Stream output, Encoding encoding)
    {
      Contract.Ensures(output == this.OutStream);
      Contract.Ensures(output.CanWrite == true);
      Contract.Ensures(this.OutStream != null);
    }

    public BinaryWriter(Stream output)
    {
      Contract.Ensures(output == this.OutStream);
      Contract.Ensures(output.CanWrite == true);
      Contract.Ensures(this.OutStream != null);
    }

    public virtual new void Close()
    {
    }

    public void Dispose()
    {
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public virtual new void Flush()
    {
    }

    public virtual new long Seek(int offset, SeekOrigin origin)
    {
      return default(long);
    }

    public virtual new void Write(long value)
    {
    }

    public virtual new void Write(ulong value)
    {
    }

    public virtual new void Write(int value)
    {
    }

    public virtual new void Write(uint value)
    {
    }

    public virtual new void Write(bool value)
    {
    }

    public virtual new void Write(char[] chars)
    {
    }

    public virtual new void Write(float value)
    {
    }

    public virtual new void Write(string value)
    {
    }

    public virtual new void Write(ushort value)
    {
    }

    public virtual new void Write(byte[] buffer, int index, int count)
    {
    }

    public virtual new void Write(char ch)
    {
    }

    public virtual new void Write(sbyte value)
    {
    }

    public virtual new void Write(byte[] buffer)
    {
    }

    public virtual new void Write(byte value)
    {
    }

    public virtual new void Write(Decimal value)
    {
    }

    public virtual new void Write(short value)
    {
    }

    public virtual new void Write(double value)
    {
    }

    public virtual new void Write(char[] chars, int index, int count)
    {
    }

    protected void Write7BitEncodedInt(int value)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new Stream BaseStream
    {
      get
      {
        return default(Stream);
      }
    }
    #endregion

    #region Fields
    public readonly static System.IO.BinaryWriter Null;
    protected Stream OutStream;
    #endregion
  }
}

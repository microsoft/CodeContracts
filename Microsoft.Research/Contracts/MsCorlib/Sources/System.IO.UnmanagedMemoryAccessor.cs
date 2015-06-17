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

// File System.IO.UnmanagedMemoryAccessor.cs
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
  public partial class UnmanagedMemoryAccessor : IDisposable
  {
    #region Methods and constructors
    protected virtual new void Dispose(bool disposing)
    {
    }

    public void Dispose()
    {
    }

    protected void Initialize(System.Runtime.InteropServices.SafeBuffer buffer, long offset, long capacity, FileAccess access)
    {
    }

    public void Read<T>(long position, out T structure)
    {
      structure = default(T);
    }

    public int ReadArray<T>(long position, T[] array, int offset, int count)
    {
      Contract.Ensures(0 <= Contract.Result<int>());

      return default(int);
    }

    public bool ReadBoolean(long position)
    {
      return default(bool);
    }

    public byte ReadByte(long position)
    {
      return default(byte);
    }

    public char ReadChar(long position)
    {
      return default(char);
    }

    public Decimal ReadDecimal(long position)
    {
      return default(Decimal);
    }

    public double ReadDouble(long position)
    {
      return default(double);
    }

    public short ReadInt16(long position)
    {
      return default(short);
    }

    public int ReadInt32(long position)
    {
      return default(int);
    }

    public long ReadInt64(long position)
    {
      return default(long);
    }

    public sbyte ReadSByte(long position)
    {
      return default(sbyte);
    }

    public float ReadSingle(long position)
    {
      return default(float);
    }

    public ushort ReadUInt16(long position)
    {
      return default(ushort);
    }

    public uint ReadUInt32(long position)
    {
      return default(uint);
    }

    public ulong ReadUInt64(long position)
    {
      return default(ulong);
    }

    public UnmanagedMemoryAccessor(System.Runtime.InteropServices.SafeBuffer buffer, long offset, long capacity)
    {
    }

    public UnmanagedMemoryAccessor(System.Runtime.InteropServices.SafeBuffer buffer, long offset, long capacity, FileAccess access)
    {
    }

    protected UnmanagedMemoryAccessor()
    {
    }

    public void Write<T>(long position, ref T structure)
    {
    }

    public void Write(long position, ulong value)
    {
    }

    public void Write(long position, uint value)
    {
    }

    public void Write(long position, bool value)
    {
    }

    public void Write(long position, byte value)
    {
    }

    public void Write(long position, char value)
    {
    }

    public void Write(long position, ushort value)
    {
    }

    public void Write(long position, Decimal value)
    {
    }

    public void Write(long position, long value)
    {
    }

    public void Write(long position, int value)
    {
    }

    public void Write(long position, short value)
    {
    }

    public void Write(long position, sbyte value)
    {
    }

    public void Write(long position, double value)
    {
    }

    public void Write(long position, float value)
    {
    }

    public void WriteArray<T>(long position, T[] array, int offset, int count)
    {
    }
    #endregion

    #region Properties and indexers
    public bool CanRead
    {
      get
      {
        return default(bool);
      }
    }

    public bool CanWrite
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

    protected bool IsOpen
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}

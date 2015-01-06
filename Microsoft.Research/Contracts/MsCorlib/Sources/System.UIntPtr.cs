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

// File System.UIntPtr.cs
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


namespace System
{
  public partial struct UIntPtr : System.Runtime.Serialization.ISerializable
  {
    #region Methods and constructors
    public static System.UIntPtr operator - (System.UIntPtr pointer, int offset)
    {
      return default(System.UIntPtr);
    }

    public static bool operator != (System.UIntPtr value1, System.UIntPtr value2)
    {
      return default(bool);
    }

    public static System.UIntPtr operator + (System.UIntPtr pointer, int offset)
    {
      return default(System.UIntPtr);
    }

    public static bool operator == (System.UIntPtr value1, System.UIntPtr value2)
    {
      return default(bool);
    }

    public static System.UIntPtr Add(System.UIntPtr pointer, int offset)
    {
      return default(System.UIntPtr);
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public static System.UIntPtr Subtract(System.UIntPtr pointer, int offset)
    {
      return default(System.UIntPtr);
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    unsafe public static explicit operator System.UIntPtr(void* value)
    {
      return default(System.UIntPtr);
    }

    public static explicit operator System.UIntPtr(uint value)
    {
      return default(System.UIntPtr);
    }

    public static explicit operator System.UIntPtr(ulong value)
    {
      return default(System.UIntPtr);
    }

    unsafe public void* ToPointer()
    {
      return default(void*);
    }

    public override string ToString()
    {
      return default(string);
    }

    public uint ToUInt32()
    {
      Contract.Ensures(0 <= Contract.Result<uint>());
      Contract.Ensures(Contract.Result<uint>() <= 4294967295);

      return default(uint);
    }

    public ulong ToUInt64()
    {
      Contract.Ensures(0 <= Contract.Result<ulong>());
      Contract.Ensures(Contract.Result<ulong>() <= 4294967295);

      return default(ulong);
    }

    public static explicit operator uint(System.UIntPtr value)
    {
      Contract.Ensures(0 <= Contract.Result<uint>());
      Contract.Ensures(Contract.Result<uint>() <= 4294967295);

      return default(uint);
    }

    public UIntPtr(ulong value)
    {
    }

    unsafe public UIntPtr(void* value)
    {
    }

    public UIntPtr(uint value)
    {
    }

    public static explicit operator ulong(System.UIntPtr value)
    {
      Contract.Ensures(0 <= Contract.Result<ulong>());
      Contract.Ensures(Contract.Result<ulong>() <= 4294967295);

      return default(ulong);
    }

    unsafe public static explicit operator void*(System.UIntPtr value)
    {
      return default(void*);
    }
    #endregion

    #region Properties and indexers
    public static int Size
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() == 4);

        return default(int);
      }
    }
    #endregion

    #region Fields
    public readonly static System.UIntPtr Zero;
    #endregion
  }
}

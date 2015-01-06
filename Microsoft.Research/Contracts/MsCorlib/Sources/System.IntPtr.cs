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

// File System.IntPtr.cs
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
  public partial struct IntPtr : System.Runtime.Serialization.ISerializable
  {
    #region Methods and constructors
    public static System.IntPtr operator - (System.IntPtr pointer, int offset)
    {
      return default(System.IntPtr);
    }

    public static bool operator != (System.IntPtr value1, System.IntPtr value2)
    {
      return default(bool);
    }

    public static System.IntPtr operator + (System.IntPtr pointer, int offset)
    {
      return default(System.IntPtr);
    }

    public static bool operator == (System.IntPtr value1, System.IntPtr value2)
    {
      return default(bool);
    }

    public static System.IntPtr Add(System.IntPtr pointer, int offset)
    {
      return default(System.IntPtr);
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public static explicit operator int(System.IntPtr value)
    {
      Contract.Ensures(Contract.Result<int>() <= 2147483647);
      Contract.Ensures(Int32.MinValue <= Contract.Result<int>());

      return default(int);
    }

    public IntPtr(int value)
    {
    }

    public IntPtr(long value)
    {
    }

    unsafe public IntPtr(void* value)
    {
    }

    public static explicit operator long(System.IntPtr value)
    {
      Contract.Ensures(-2147483648 <= Contract.Result<long>());
      Contract.Ensures(Contract.Result<long>() <= 2147483647);

      return default(long);
    }

    public static System.IntPtr Subtract(System.IntPtr pointer, int offset)
    {
      return default(System.IntPtr);
    }

    public static explicit operator System.IntPtr(long value)
    {
      return default(System.IntPtr);
    }

    unsafe public static explicit operator System.IntPtr(void* value)
    {
      return default(System.IntPtr);
    }

    public static explicit operator System.IntPtr(int value)
    {
      return default(System.IntPtr);
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public int ToInt32()
    {
      Contract.Ensures(Contract.Result<int>() <= 2147483647);
      Contract.Ensures(Int32.MinValue <= Contract.Result<int>());

      return default(int);
    }

    public long ToInt64()
    {
      Contract.Ensures(-2147483648 <= Contract.Result<long>());
      Contract.Ensures(Contract.Result<long>() <= 2147483647);

      return default(long);
    }

    unsafe public void* ToPointer()
    {
      return default(void*);
    }

    public override string ToString()
    {
      return default(string);
    }

    public string ToString(string format)
    {
      return default(string);
    }

    unsafe public static explicit operator void*(System.IntPtr value)
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
    public readonly static System.IntPtr Zero;
    #endregion
  }
}

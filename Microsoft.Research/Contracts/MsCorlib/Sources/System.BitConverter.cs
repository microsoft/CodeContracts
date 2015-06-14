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

// File System.BitConverter.cs
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
  static public partial class BitConverter
  {
    #region Methods and constructors
    public static long DoubleToInt64Bits(double value)
    {
      return default(long);
    }

    public static byte[] GetBytes(ulong value)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }

    public static byte[] GetBytes(uint value)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }

    public static byte[] GetBytes(float value)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }

    public static byte[] GetBytes(bool value)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }

    public static byte[] GetBytes(double value)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }

    public static byte[] GetBytes(short value)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }

    public static byte[] GetBytes(char value)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }

    public static byte[] GetBytes(int value)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }

    public static byte[] GetBytes(ushort value)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }

    public static byte[] GetBytes(long value)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }

    public static double Int64BitsToDouble(long value)
    {
      return default(double);
    }

    public static bool ToBoolean(byte[] value, int startIndex)
    {
      return default(bool);
    }

    public static char ToChar(byte[] value, int startIndex)
    {
      Contract.Requires(0 <= startIndex);
      Contract.Ensures(0 <= Contract.Result<char>());
      Contract.Ensures(Contract.Result<char>() <= 32767);

      return default(char);
    }

    public static double ToDouble(byte[] value, int startIndex)
    {
      Contract.Requires(0 <= startIndex);

      return default(double);
    }

    public static short ToInt16(byte[] value, int startIndex)
    {
      Contract.Requires(0 <= startIndex);

      return default(short);
    }

    public static int ToInt32(byte[] value, int startIndex)
    {
      Contract.Requires(0 <= startIndex);

      return default(int);
    }

    public static long ToInt64(byte[] value, int startIndex)
    {
      Contract.Requires(0 <= startIndex);

      return default(long);
    }

    public static float ToSingle(byte[] value, int startIndex)
    {
      Contract.Requires(0 <= startIndex);

      return default(float);
    }

    public static string ToString(byte[] value, int startIndex, int length)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static string ToString(byte[] value, int startIndex)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static string ToString(byte[] value)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static ushort ToUInt16(byte[] value, int startIndex)
    {
      Contract.Requires(0 <= startIndex);
      Contract.Ensures(0 <= Contract.Result<ushort>());
      Contract.Ensures(Contract.Result<ushort>() <= 32767);

      return default(ushort);
    }

    public static uint ToUInt32(byte[] value, int startIndex)
    {
      Contract.Requires(0 <= startIndex);

      return default(uint);
    }

    public static ulong ToUInt64(byte[] value, int startIndex)
    {
      Contract.Requires(0 <= startIndex);
      Contract.Ensures(Contract.Result<ulong>() <= 9223372036854775807);

      return default(ulong);
    }
    #endregion

    #region Fields
    public readonly static bool IsLittleEndian;
    #endregion
  }
}

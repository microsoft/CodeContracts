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

// File System.Decimal.cs
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
  public partial struct Decimal : IFormattable, IComparable, IConvertible, System.Runtime.Serialization.IDeserializationCallback, IComparable<Decimal>, IEquatable<Decimal>
  {
    #region Methods and constructors
    public static System.Decimal operator - (System.Decimal d)
    {
      return default(System.Decimal);
    }

    public static System.Decimal operator - (System.Decimal d1, System.Decimal d2)
    {
      return default(System.Decimal);
    }

    public static System.Decimal operator -- (System.Decimal d)
    {
      return default(System.Decimal);
    }

    public static bool operator != (System.Decimal d1, System.Decimal d2)
    {
      return default(bool);
    }

    public static System.Decimal operator % (System.Decimal d1, System.Decimal d2)
    {
      return default(System.Decimal);
    }

    public static System.Decimal operator * (System.Decimal d1, System.Decimal d2)
    {
      return default(System.Decimal);
    }

    public static System.Decimal operator / (System.Decimal d1, System.Decimal d2)
    {
      return default(System.Decimal);
    }

    public static System.Decimal operator + (System.Decimal d1, System.Decimal d2)
    {
      return default(System.Decimal);
    }

    public static System.Decimal operator + (System.Decimal d)
    {
      return default(System.Decimal);
    }

    public static System.Decimal operator ++ (System.Decimal d)
    {
      return default(System.Decimal);
    }

    public static bool operator < (System.Decimal d1, System.Decimal d2)
    {
      return default(bool);
    }

    public static bool operator <=(System.Decimal d1, System.Decimal d2)
    {
      return default(bool);
    }

    public static bool operator == (System.Decimal d1, System.Decimal d2)
    {
      return default(bool);
    }

    public static bool operator > (System.Decimal d1, System.Decimal d2)
    {
      return default(bool);
    }

    public static bool operator >= (System.Decimal d1, System.Decimal d2)
    {
      return default(bool);
    }

    public static System.Decimal Add(System.Decimal d1, System.Decimal d2)
    {
      return default(System.Decimal);
    }

    public static explicit operator byte(System.Decimal value)
    {
      return default(byte);
    }

    public static System.Decimal Ceiling(System.Decimal d)
    {
      return default(System.Decimal);
    }

    public static explicit operator char(System.Decimal value)
    {
      return default(char);
    }

    public static int Compare(System.Decimal d1, System.Decimal d2)
    {
      return default(int);
    }

    public int CompareTo(System.Decimal value)
    {
      return default(int);
    }

    public int CompareTo(Object value)
    {
      return default(int);
    }

    public Decimal(ulong value)
    {
    }

    public Decimal(float value)
    {
    }

    public Decimal(uint value)
    {
    }

    public Decimal(long value)
    {
    }

    public Decimal(int lo, int mid, int hi, bool isNegative, byte scale)
    {
    }

    public Decimal(double value)
    {
    }

    public Decimal(int value)
    {
    }

    public Decimal(int[] bits)
    {
    }

    public static System.Decimal Divide(System.Decimal d1, System.Decimal d2)
    {
      return default(System.Decimal);
    }

    public static explicit operator double(System.Decimal value)
    {
      return default(double);
    }

    public static bool Equals(System.Decimal d1, System.Decimal d2)
    {
      return default(bool);
    }

    public bool Equals(System.Decimal value)
    {
      return default(bool);
    }

    public override bool Equals(Object value)
    {
      return default(bool);
    }

    public static explicit operator float(System.Decimal value)
    {
      return default(float);
    }

    public static System.Decimal Floor(System.Decimal d)
    {
      return default(System.Decimal);
    }

    public static System.Decimal FromOACurrency(long cy)
    {
      return default(System.Decimal);
    }

    public static int[] GetBits(System.Decimal d)
    {
      Contract.Ensures(Contract.Result<int[]>() != null);

      return default(int[]);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public TypeCode GetTypeCode()
    {
      return default(TypeCode);
    }

    public static explicit operator int(System.Decimal value)
    {
      Contract.Ensures(-2147483647 <= Contract.Result<int>());

      return default(int);
    }

    public static explicit operator long(System.Decimal value)
    {
      Contract.Ensures(-9223372036854775807 <= Contract.Result<long>());

      return default(long);
    }

    public static System.Decimal Multiply(System.Decimal d1, System.Decimal d2)
    {
      return default(System.Decimal);
    }

    public static System.Decimal Negate(System.Decimal d)
    {
      return default(System.Decimal);
    }

    public static System.Decimal Parse(string s, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
      Contract.Ensures(System.Runtime.CompilerServices.RuntimeHelpers.OffsetToStringData == 8);

      return default(System.Decimal);
    }

    public static System.Decimal Parse(string s)
    {
      Contract.Ensures(System.Runtime.CompilerServices.RuntimeHelpers.OffsetToStringData == 8);

      return default(System.Decimal);
    }

    public static System.Decimal Parse(string s, System.Globalization.NumberStyles style)
    {
      Contract.Ensures(System.Runtime.CompilerServices.RuntimeHelpers.OffsetToStringData == 8);

      return default(System.Decimal);
    }

    public static System.Decimal Parse(string s, IFormatProvider provider)
    {
      Contract.Ensures(System.Runtime.CompilerServices.RuntimeHelpers.OffsetToStringData == 8);

      return default(System.Decimal);
    }

    public static System.Decimal Remainder(System.Decimal d1, System.Decimal d2)
    {
      return default(System.Decimal);
    }

    public static System.Decimal Round(System.Decimal d, int decimals)
    {
      return default(System.Decimal);
    }

    public static System.Decimal Round(System.Decimal d, MidpointRounding mode)
    {
      return default(System.Decimal);
    }

    public static System.Decimal Round(System.Decimal d, int decimals, MidpointRounding mode)
    {
      return default(System.Decimal);
    }

    public static System.Decimal Round(System.Decimal d)
    {
      return default(System.Decimal);
    }

    public static explicit operator sbyte(System.Decimal value)
    {
      return default(sbyte);
    }

    public static explicit operator short(System.Decimal value)
    {
      return default(short);
    }

    public static System.Decimal Subtract(System.Decimal d1, System.Decimal d2)
    {
      return default(System.Decimal);
    }

    public static implicit operator System.Decimal(short value)
    {
      return default(System.Decimal);
    }

    public static explicit operator System.Decimal(double value)
    {
      return default(System.Decimal);
    }

    public static implicit operator System.Decimal(ushort value)
    {
      return default(System.Decimal);
    }

    public static implicit operator System.Decimal(sbyte value)
    {
      return default(System.Decimal);
    }

    public static implicit operator System.Decimal(int value)
    {
      return default(System.Decimal);
    }

    public static implicit operator System.Decimal(uint value)
    {
      return default(System.Decimal);
    }

    public static implicit operator System.Decimal(long value)
    {
      return default(System.Decimal);
    }

    public static implicit operator System.Decimal(char value)
    {
      return default(System.Decimal);
    }

    public static explicit operator System.Decimal(float value)
    {
      return default(System.Decimal);
    }

    public static implicit operator System.Decimal(byte value)
    {
      return default(System.Decimal);
    }

    public static implicit operator System.Decimal(ulong value)
    {
      return default(System.Decimal);
    }

    bool System.IConvertible.ToBoolean(IFormatProvider provider)
    {
      return default(bool);
    }

    byte System.IConvertible.ToByte(IFormatProvider provider)
    {
      return default(byte);
    }

    char System.IConvertible.ToChar(IFormatProvider provider)
    {
      return default(char);
    }

    DateTime System.IConvertible.ToDateTime(IFormatProvider provider)
    {
      return default(DateTime);
    }

    System.Decimal System.IConvertible.ToDecimal(IFormatProvider provider)
    {
      return default(System.Decimal);
    }

    double System.IConvertible.ToDouble(IFormatProvider provider)
    {
      return default(double);
    }

    short System.IConvertible.ToInt16(IFormatProvider provider)
    {
      return default(short);
    }

    int System.IConvertible.ToInt32(IFormatProvider provider)
    {
      return default(int);
    }

    long System.IConvertible.ToInt64(IFormatProvider provider)
    {
      return default(long);
    }

    sbyte System.IConvertible.ToSByte(IFormatProvider provider)
    {
      return default(sbyte);
    }

    float System.IConvertible.ToSingle(IFormatProvider provider)
    {
      return default(float);
    }

    Object System.IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return default(Object);
    }

    ushort System.IConvertible.ToUInt16(IFormatProvider provider)
    {
      return default(ushort);
    }

    uint System.IConvertible.ToUInt32(IFormatProvider provider)
    {
      return default(uint);
    }

    ulong System.IConvertible.ToUInt64(IFormatProvider provider)
    {
      return default(ulong);
    }

    void System.Runtime.Serialization.IDeserializationCallback.OnDeserialization(Object sender)
    {
    }

    public static byte ToByte(System.Decimal value)
    {
      Contract.Ensures(0 <= Contract.Result<byte>());
      Contract.Ensures(Contract.Result<byte>() <= 255);

      return default(byte);
    }

    public static double ToDouble(System.Decimal d)
    {
      return default(double);
    }

    public static short ToInt16(System.Decimal value)
    {
      Contract.Ensures(-32768 <= Contract.Result<short>());
      Contract.Ensures(Contract.Result<short>() <= 32767);

      return default(short);
    }

    public static int ToInt32(System.Decimal d)
    {
      Contract.Ensures(-2147483647 <= Contract.Result<int>());

      return default(int);
    }

    public static long ToInt64(System.Decimal d)
    {
      Contract.Ensures(-9223372036854775808 <= Contract.Result<long>());

      return default(long);
    }

    public static long ToOACurrency(System.Decimal value)
    {
      return default(long);
    }

    public static sbyte ToSByte(System.Decimal value)
    {
      Contract.Ensures(-128 <= Contract.Result<sbyte>());
      Contract.Ensures(Contract.Result<sbyte>() <= 127);

      return default(sbyte);
    }

    public static float ToSingle(System.Decimal d)
    {
      return default(float);
    }

    public string ToString(string format, IFormatProvider provider)
    {
      return default(string);
    }

    public string ToString(string format)
    {
      return default(string);
    }

    public override string ToString()
    {
      return default(string);
    }

    public string ToString(IFormatProvider provider)
    {
      return default(string);
    }

    public static ushort ToUInt16(System.Decimal value)
    {
      Contract.Ensures(0 <= Contract.Result<ushort>());
      Contract.Ensures(Contract.Result<ushort>() <= 65535);

      return default(ushort);
    }

    public static uint ToUInt32(System.Decimal d)
    {
      Contract.Ensures(Contract.Result<uint>() <= 2147483647);

      return default(uint);
    }

    public static ulong ToUInt64(System.Decimal d)
    {
      return default(ulong);
    }

    public static System.Decimal Truncate(System.Decimal d)
    {
      return default(System.Decimal);
    }

    public static bool TryParse(string s, System.Globalization.NumberStyles style, IFormatProvider provider, out System.Decimal result)
    {
      result = default(System.Decimal);

      return default(bool);
    }

    public static bool TryParse(string s, out System.Decimal result)
    {
      result = default(System.Decimal);

      return default(bool);
    }

    public static explicit operator uint(System.Decimal value)
    {
      Contract.Ensures(Contract.Result<uint>() <= 2147483647);

      return default(uint);
    }

    public static explicit operator ulong(System.Decimal value)
    {
      return default(ulong);
    }

    public static explicit operator ushort(System.Decimal value)
    {
      return default(ushort);
    }
    #endregion

    #region Fields
    public readonly static System.Decimal MaxValue;
    public readonly static System.Decimal MinusOne;
    public readonly static System.Decimal MinValue;
    public readonly static System.Decimal One;
    public readonly static System.Decimal Zero;
    #endregion
  }
}

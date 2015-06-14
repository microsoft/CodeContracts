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

// File System.Single.cs
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
  public partial struct Single : IComparable, IFormattable, IConvertible, IComparable<float>, IEquatable<float>
  {
    #region Methods and constructors
    public static bool operator != (float left, float right)
    {
      Contract.Ensures(Contract.Result<bool>() == ((left == right) == false));

      return default(bool);
    }

    public static bool operator < (float left, float right)
    {
      Contract.Ensures(Contract.Result<bool>() == (left < right));

      return default(bool);
    }

    public static bool operator <=(float left, float right)
    {
      Contract.Ensures(Contract.Result<bool>() == ((left > right) == false));

      return default(bool);
    }

    public static bool operator == (float left, float right)
    {
      Contract.Ensures(Contract.Result<bool>() == (left == right));

      return default(bool);
    }

    public static bool operator > (float left, float right)
    {
      Contract.Ensures(Contract.Result<bool>() == (left > right));

      return default(bool);
    }

    public static bool operator >= (float left, float right)
    {
      Contract.Ensures(Contract.Result<bool>() == ((left < right) == false));

      return default(bool);
    }

    public int CompareTo(float value)
    {
      return default(int);
    }

    public int CompareTo(Object value)
    {
      return default(int);
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public bool Equals(float obj)
    {
      return default(bool);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public TypeCode GetTypeCode()
    {
      return default(TypeCode);
    }

    public static bool IsInfinity(float f)
    {
      return default(bool);
    }

    public static bool IsNaN(float f)
    {
      return default(bool);
    }

    public static bool IsNegativeInfinity(float f)
    {
      return default(bool);
    }

    public static bool IsPositiveInfinity(float f)
    {
      return default(bool);
    }

    public static float Parse(string s)
    {
      return default(float);
    }

    public static float Parse(string s, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
      return default(float);
    }

    public static float Parse(string s, System.Globalization.NumberStyles style)
    {
      return default(float);
    }

    public static float Parse(string s, IFormatProvider provider)
    {
      return default(float);
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

    Decimal System.IConvertible.ToDecimal(IFormatProvider provider)
    {
      return default(Decimal);
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

    public override string ToString()
    {
      return default(string);
    }

    public string ToString(string format, IFormatProvider provider)
    {
      return default(string);
    }

    public string ToString(string format)
    {
      return default(string);
    }

    public string ToString(IFormatProvider provider)
    {
      return default(string);
    }

    public static bool TryParse(string s, out float result)
    {
      result = default(float);

      return default(bool);
    }

    public static bool TryParse(string s, System.Globalization.NumberStyles style, IFormatProvider provider, out float result)
    {
      result = default(float);

      return default(bool);
    }
    #endregion

    #region Fields
    public static float Epsilon;
    public static float MaxValue;
    public static float MinValue;
    public static float NaN;
    public static float NegativeInfinity;
    public static float PositiveInfinity;
    #endregion
  }
}

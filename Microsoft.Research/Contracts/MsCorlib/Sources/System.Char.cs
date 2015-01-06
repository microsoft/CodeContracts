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

// File System.Char.cs
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
  public partial struct Char : IComparable, IConvertible, IComparable<char>, IEquatable<char>
  {
    #region Methods and constructors
    public int CompareTo(char value)
    {
      return default(int);
    }

    public int CompareTo(Object value)
    {
      return default(int);
    }

    public static string ConvertFromUtf32(int utf32)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static int ConvertToUtf32(char highSurrogate, char lowSurrogate)
    {
      return default(int);
    }

    public static int ConvertToUtf32(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(0 <= Contract.Result<int>());
      Contract.Ensures(1 <= s.Length);

      return default(int);
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public bool Equals(char obj)
    {
      return default(bool);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public static double GetNumericValue(char c)
    {
      return default(double);
    }

    public static double GetNumericValue(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(1 <= s.Length);

      return default(double);
    }

    public TypeCode GetTypeCode()
    {
      return default(TypeCode);
    }

    public static System.Globalization.UnicodeCategory GetUnicodeCategory(char c)
    {
      return default(System.Globalization.UnicodeCategory);
    }

    public static System.Globalization.UnicodeCategory GetUnicodeCategory(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(-2147483647 <= s.Length);

      return default(System.Globalization.UnicodeCategory);
    }

    public static bool IsControl(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(-2147483647 <= s.Length);

      return default(bool);
    }

    public static bool IsControl(char c)
    {
      return default(bool);
    }

    public static bool IsDigit(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(-2147483647 <= s.Length);

      return default(bool);
    }

    public static bool IsDigit(char c)
    {
      return default(bool);
    }

    public static bool IsHighSurrogate(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(1 <= s.Length);

      return default(bool);
    }

    public static bool IsHighSurrogate(char c)
    {
      return default(bool);
    }

    public static bool IsLetter(char c)
    {
      return default(bool);
    }

    public static bool IsLetter(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(-2147483647 <= s.Length);

      return default(bool);
    }

    public static bool IsLetterOrDigit(char c)
    {
      return default(bool);
    }

    public static bool IsLetterOrDigit(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(-2147483647 <= s.Length);

      return default(bool);
    }

    public static bool IsLower(char c)
    {
      return default(bool);
    }

    public static bool IsLower(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(-2147483647 <= s.Length);

      return default(bool);
    }

    public static bool IsLowSurrogate(char c)
    {
      return default(bool);
    }

    public static bool IsLowSurrogate(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(1 <= s.Length);

      return default(bool);
    }

    public static bool IsNumber(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(-2147483647 <= s.Length);

      return default(bool);
    }

    public static bool IsNumber(char c)
    {
      return default(bool);
    }

    public static bool IsPunctuation(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(-2147483647 <= s.Length);

      return default(bool);
    }

    public static bool IsPunctuation(char c)
    {
      return default(bool);
    }

    public static bool IsSeparator(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(-2147483647 <= s.Length);

      return default(bool);
    }

    public static bool IsSeparator(char c)
    {
      return default(bool);
    }

    public static bool IsSurrogate(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(-2147483647 <= s.Length);

      return default(bool);
    }

    public static bool IsSurrogate(char c)
    {
      return default(bool);
    }

    public static bool IsSurrogatePair(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(1 <= s.Length);

      return default(bool);
    }

    public static bool IsSurrogatePair(char highSurrogate, char lowSurrogate)
    {
      return default(bool);
    }

    public static bool IsSymbol(char c)
    {
      return default(bool);
    }

    public static bool IsSymbol(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(-2147483647 <= s.Length);

      return default(bool);
    }

    public static bool IsUpper(char c)
    {
      return default(bool);
    }

    public static bool IsUpper(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(-2147483647 <= s.Length);

      return default(bool);
    }

    public static bool IsWhiteSpace(string s, int index)
    {
      Contract.Ensures((index - s.Length) < 0);
      Contract.Ensures(-2147483647 <= s.Length);

      return default(bool);
    }

    public static bool IsWhiteSpace(char c)
    {
      return default(bool);
    }

    public static char Parse(string s)
    {
      Contract.Ensures(s.Length == 1);

      return default(char);
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

    public static char ToLower(char c, System.Globalization.CultureInfo culture)
    {
      Contract.Requires(culture.TextInfo != null);

      return default(char);
    }

    public static char ToLower(char c)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(char);
    }

    public static char ToLowerInvariant(char c)
    {
      return default(char);
    }

    public static string ToString(char c)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public string ToString(IFormatProvider provider)
    {
      return default(string);
    }

    public override string ToString()
    {
      return default(string);
    }

    public static char ToUpper(char c)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(char);
    }

    public static char ToUpper(char c, System.Globalization.CultureInfo culture)
    {
      Contract.Requires(culture.TextInfo != null);

      return default(char);
    }

    public static char ToUpperInvariant(char c)
    {
      return default(char);
    }

    public static bool TryParse(string s, out char result)
    {
      result = default(char);

      return default(bool);
    }
    #endregion

    #region Fields
    public static char MaxValue;
    public static char MinValue;
    #endregion
  }
}

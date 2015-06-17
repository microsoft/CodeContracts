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

// File System.String.cs
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
  sealed public partial class String : IComparable, ICloneable, IConvertible, IComparable<string>, IEnumerable<char>, System.Collections.IEnumerable, IEquatable<string>
  {
    #region Methods and constructors
    public static bool operator != (string a, string b)
    {
      Contract.Ensures(Contract.Result<bool>() == ((a.Equals(b)) == false));

      return default(bool);
    }

    public static bool operator == (string a, string b)
    {
      Contract.Ensures(Contract.Result<bool>() == (a.Equals(b)));

      return default(bool);
    }

    public Object Clone()
    {
      return default(Object);
    }

    public static int Compare(string strA, int indexA, string strB, int indexB, int length)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(int);
    }

    public static int Compare(string strA, string strB, StringComparison comparisonType)
    {
      return default(int);
    }

    public static int Compare(string strA, string strB, System.Globalization.CultureInfo culture, System.Globalization.CompareOptions options)
    {
      Contract.Requires(culture.CompareInfo != null);

      return default(int);
    }

    public static int Compare(string strA, string strB, bool ignoreCase, System.Globalization.CultureInfo culture)
    {
      return default(int);
    }

    public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(int);
    }

    public static int Compare(string strA, string strB, bool ignoreCase)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(int);
    }

    public static int Compare(string strA, string strB)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(int);
    }

    public static int Compare(string strA, int indexA, string strB, int indexB, int length, StringComparison comparisonType)
    {
      return default(int);
    }

    public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase, System.Globalization.CultureInfo culture)
    {
      return default(int);
    }

    public static int Compare(string strA, int indexA, string strB, int indexB, int length, System.Globalization.CultureInfo culture, System.Globalization.CompareOptions options)
    {
      Contract.Requires(culture.CompareInfo != null);

      return default(int);
    }

    public static int CompareOrdinal(string strA, string strB)
    {
      return default(int);
    }

    public static int CompareOrdinal(string strA, int indexA, string strB, int indexB, int length)
    {
      return default(int);
    }

    public int CompareTo(string strB)
    {
      return default(int);
    }

    public int CompareTo(Object value)
    {
      return default(int);
    }

    public static string Concat(Object arg0, Object arg1, Object arg2, Object arg3)
    {
      return default(string);
    }

    public static string Concat(Object[] args)
    {
      return default(string);
    }

    public static string Concat(Object arg0, Object arg1, Object arg2)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static string Concat(Object arg0)
    {
      return default(string);
    }

    public static string Concat(Object arg0, Object arg1)
    {
      return default(string);
    }

    public static string Concat(string str0, string str1, string str2)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static string Concat(string str0, string str1, string str2, string str3)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static string Concat(string[] values)
    {
      return default(string);
    }

    public static string Concat<T>(IEnumerable<T> values)
    {
      Contract.Ensures(0 <= string.Empty.Length);

      return default(string);
    }

    public static string Concat(IEnumerable<string> values)
    {
      Contract.Ensures(0 <= string.Empty.Length);

      return default(string);
    }

    public static string Concat(string str0, string str1)
    {
      return default(string);
    }

    public bool Contains(string value)
    {
      Contract.Ensures(0 <= this.Length);

      return default(bool);
    }

    public static string Copy(string str)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
    {
      Contract.Ensures((count - this.Length) <= 0);
      Contract.Ensures(0 <= this.Length);
    }

    public bool EndsWith(string value)
    {
      return default(bool);
    }

    public bool EndsWith(string value, bool ignoreCase, System.Globalization.CultureInfo culture)
    {
      return default(bool);
    }

    public bool EndsWith(string value, StringComparison comparisonType)
    {
      return default(bool);
    }

    public static bool Equals(string a, string b)
    {
      return default(bool);
    }

    public static bool Equals(string a, string b, StringComparison comparisonType)
    {
      return default(bool);
    }

    public bool Equals(string value, StringComparison comparisonType)
    {
      return default(bool);
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public bool Equals(string value)
    {
      return default(bool);
    }

    public static string Format(IFormatProvider provider, string format, Object[] args)
    {
      Contract.Ensures(0 <= format.Length);
      Contract.Ensures(0 <= string.Empty.Length);

      return default(string);
    }

    public static string Format(string format, Object arg0)
    {
      Contract.Ensures(0 <= format.Length);
      Contract.Ensures(0 <= string.Empty.Length);

      return default(string);
    }

    public static string Format(string format, Object[] args)
    {
      Contract.Ensures(0 <= format.Length);
      Contract.Ensures(0 <= string.Empty.Length);

      return default(string);
    }

    public static string Format(string format, Object arg0, Object arg1)
    {
      Contract.Ensures(0 <= format.Length);
      Contract.Ensures(0 <= string.Empty.Length);

      return default(string);
    }

    public static string Format(string format, Object arg0, Object arg1, Object arg2)
    {
      Contract.Ensures(0 <= format.Length);
      Contract.Ensures(0 <= string.Empty.Length);

      return default(string);
    }

    public CharEnumerator GetEnumerator()
    {
      Contract.Ensures(Contract.Result<System.CharEnumerator>() != null);

      return default(CharEnumerator);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public TypeCode GetTypeCode()
    {
      return default(TypeCode);
    }

    public int IndexOf(string value, int startIndex, int count)
    {
      Contract.Ensures((count - this.Length) <= 0);
      Contract.Ensures((startIndex - this.Length) <= 0);
      Contract.Ensures(0 <= this.Length);

      return default(int);
    }

    public int IndexOf(char value, int startIndex)
    {
      return default(int);
    }

    public int IndexOf(char value, int startIndex, int count)
    {
      return default(int);
    }

    public int IndexOf(string value)
    {
      Contract.Ensures(0 <= this.Length);

      return default(int);
    }

    public int IndexOf(char value)
    {
      return default(int);
    }

    public int IndexOf(string value, int startIndex)
    {
      Contract.Ensures((startIndex - this.Length) <= 0);
      Contract.Ensures(0 <= this.Length);

      return default(int);
    }

    public int IndexOf(string value, StringComparison comparisonType)
    {
      Contract.Ensures(0 <= this.Length);

      return default(int);
    }

    public int IndexOf(string value, int startIndex, int count, StringComparison comparisonType)
    {
      Contract.Ensures((startIndex - this.Length) <= 0);
      Contract.Ensures(0 <= this.Length);

      return default(int);
    }

    public int IndexOf(string value, int startIndex, StringComparison comparisonType)
    {
      Contract.Ensures((startIndex - this.Length) <= 0);
      Contract.Ensures(0 <= this.Length);

      return default(int);
    }

    public int IndexOfAny(char[] anyOf)
    {
      return default(int);
    }

    public int IndexOfAny(char[] anyOf, int startIndex, int count)
    {
      return default(int);
    }

    public int IndexOfAny(char[] anyOf, int startIndex)
    {
      return default(int);
    }

    public string Insert(int startIndex, string value)
    {
      Contract.Ensures((startIndex - this.Length) <= 0);
      Contract.Ensures(0 <= this.Length);

      return default(string);
    }

    public static string Intern(string str)
    {
      return default(string);
    }

    public static string IsInterned(string str)
    {
      return default(string);
    }

    public bool IsNormalized(NormalizationForm normalizationForm)
    {
      return default(bool);
    }

    public bool IsNormalized()
    {
      return default(bool);
    }

    public static bool IsNullOrEmpty(string value)
    {
      return default(bool);
    }

    public static bool IsNullOrWhiteSpace(string value)
    {
      return default(bool);
    }

    public static string Join(string separator, string[] value)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static string Join(string separator, IEnumerable<string> values)
    {
      return default(string);
    }

    public static string Join(string separator, string[] value, int startIndex, int count)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static string Join<T>(string separator, IEnumerable<T> values)
    {
      return default(string);
    }

    public static string Join(string separator, Object[] values)
    {
      return default(string);
    }

    public int LastIndexOf(string value, StringComparison comparisonType)
    {
      Contract.Ensures(0 <= this.Length);

      return default(int);
    }

    public int LastIndexOf(string value, int startIndex, int count)
    {
      Contract.Ensures((startIndex - this.Length) <= 0);
      Contract.Ensures(0 <= this.Length);

      return default(int);
    }

    public int LastIndexOf(string value, int startIndex, StringComparison comparisonType)
    {
      Contract.Ensures((startIndex - this.Length) <= 0);
      Contract.Ensures(0 <= this.Length);

      return default(int);
    }

    public int LastIndexOf(char value, int startIndex)
    {
      return default(int);
    }

    public int LastIndexOf(string value, int startIndex, int count, StringComparison comparisonType)
    {
      Contract.Ensures((Contract.OldValue(startIndex) - this.Length) <= 0);
      Contract.Ensures(0 <= this.Length);

      return default(int);
    }

    public int LastIndexOf(string value, int startIndex)
    {
      Contract.Ensures((startIndex - this.Length) <= 0);
      Contract.Ensures(0 <= this.Length);

      return default(int);
    }

    public int LastIndexOf(char value, int startIndex, int count)
    {
      return default(int);
    }

    public int LastIndexOf(char value)
    {
      return default(int);
    }

    public int LastIndexOf(string value)
    {
      Contract.Ensures(0 <= this.Length);

      return default(int);
    }

    public int LastIndexOfAny(char[] anyOf)
    {
      return default(int);
    }

    public int LastIndexOfAny(char[] anyOf, int startIndex)
    {
      return default(int);
    }

    public int LastIndexOfAny(char[] anyOf, int startIndex, int count)
    {
      return default(int);
    }

    public string Normalize()
    {
      return default(string);
    }

    public string Normalize(NormalizationForm normalizationForm)
    {
      return default(string);
    }

    public string PadLeft(int totalWidth, char paddingChar)
    {
      return default(string);
    }

    public string PadLeft(int totalWidth)
    {
      return default(string);
    }

    public string PadRight(int totalWidth, char paddingChar)
    {
      return default(string);
    }

    public string PadRight(int totalWidth)
    {
      return default(string);
    }

    public string Remove(int startIndex)
    {
      Contract.Ensures((startIndex - this.Length) < 0);
      Contract.Ensures(1 <= this.Length);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public string Remove(int startIndex, int count)
    {
      return default(string);
    }

    public string Replace(char oldChar, char newChar)
    {
      return default(string);
    }

    public string Replace(string oldValue, string newValue)
    {
      return default(string);
    }

    public string[] Split(string[] separator, StringSplitOptions options)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public string[] Split(string[] separator, int count, StringSplitOptions options)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public string[] Split(char[] separator, int count)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public string[] Split(char[] separator, StringSplitOptions options)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public string[] Split(char[] separator, int count, StringSplitOptions options)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public string[] Split(char[] separator)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public bool StartsWith(string value)
    {
      return default(bool);
    }

    public bool StartsWith(string value, StringComparison comparisonType)
    {
      return default(bool);
    }

    public bool StartsWith(string value, bool ignoreCase, System.Globalization.CultureInfo culture)
    {
      return default(bool);
    }

    unsafe public String(char* value, int startIndex, int length)
    {
    }

    unsafe public String(char* value)
    {
    }

    public String(char c, int count)
    {
    }

    unsafe public String(sbyte* value, int startIndex, int length, Encoding enc)
    {
    }

    public String(char[] value)
    {
    }

    public String(char[] value, int startIndex, int length)
    {
    }

    unsafe public String(sbyte* value, int startIndex, int length)
    {
    }

    unsafe public String(sbyte* value)
    {
    }

    public string Substring(int startIndex)
    {
      Contract.Ensures((startIndex - this.Length) <= 0);
      Contract.Ensures(0 <= this.Length);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public string Substring(int startIndex, int length)
    {
      Contract.Ensures((startIndex - this.Length) <= 0);
      Contract.Ensures(0 <= this.Length);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    IEnumerator<char> System.Collections.Generic.IEnumerable<System.Char>.GetEnumerator()
    {
      return default(IEnumerator<char>);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
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

    public char[] ToCharArray()
    {
      Contract.Requires(0 <= this.Length);
      Contract.Ensures(0 <= this.Length);
      Contract.Ensures(Contract.Result<char[]>() != null);

      return default(char[]);
    }

    public char[] ToCharArray(int startIndex, int length)
    {
      Contract.Ensures((startIndex - this.Length) <= 0);
      Contract.Ensures(0 <= this.Length);
      Contract.Ensures(Contract.Result<char[]>() != null);

      return default(char[]);
    }

    public string ToLower(System.Globalization.CultureInfo culture)
    {
      Contract.Requires(culture.TextInfo != null);

      return default(string);
    }

    public string ToLower()
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(string);
    }

    public string ToLowerInvariant()
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

    public string ToUpper()
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(string);
    }

    public string ToUpper(System.Globalization.CultureInfo culture)
    {
      Contract.Requires(culture.TextInfo != null);

      return default(string);
    }

    public string ToUpperInvariant()
    {
      return default(string);
    }

    public string Trim()
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public string Trim(char[] trimChars)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public string TrimEnd(char[] trimChars)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public string TrimStart(char[] trimChars)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    #endregion

    #region Properties and indexers
    [System.Runtime.CompilerServices.IndexerName("Chars")]
    public char this [int index]
    {
      get
      {
        return default(char);
      }
    }

    public int Length
    {
      get
      {
        return default(int);
      }
    }
    #endregion

    #region Fields
    public readonly static string Empty;
    #endregion
  }
}

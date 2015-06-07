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

using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Collections.Generic;

namespace System
{
  [Immutable]
  public class String
  {
    [System.Runtime.CompilerServices.IndexerName("Chars")]
    public char this[int index]
    {
      get
      {
        Contract.Requires(0 <= index);
        Contract.Requires(index < this.Length);
        return default(char);
      }
    }

    public int Length
    {
      [Pure]
      [Reads(ReadsAttribute.Reads.Nothing)]
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
    }

#if !SILVERLIGHT
    [Pure]
    [GlobalAccess(false)]
    [Escapes(true, false)]
    public CharEnumerator GetEnumerator()
    {
      // Contract.Ensures(Contract.Result<string>().IsNew);

      Contract.Ensures(Contract.Result<CharEnumerator>() != null);
      return default(CharEnumerator);
    }
#endif

    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String IsInterned(String str)
    {
      Contract.Requires(str != null);
      Contract.Ensures(Contract.Result<string>() == null || Contract.Result<string>().Length == str.Length);
      Contract.Ensures(Contract.Result<string>() == null || str.Equals(Contract.Result<string>()));

      return default(String);
    }
    public static String Intern(String str)
    {
      Contract.Requires(str != null);
      Contract.Ensures(Contract.Result<string>().Length == str.Length);

      return default(String);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Concat(String[] values)
    {
      Contract.Requires(values != null);
      //Contract.Ensures(Contract.Result<string>().Length == Sum({ String s in values); s.Length }));

      Contract.Ensures(Contract.Result<String>() != null);
      return default(String);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Concat(String str0, String str1, String str2, String str3)
    {
      Contract.Ensures(Contract.Result<String>() != null);
      Contract.Ensures(Contract.Result<string>().Length ==
          (str0 == null ? 0 : str0.Length) +
          (str1 == null ? 0 : str1.Length) +
          (str2 == null ? 0 : str2.Length) +
          (str3 == null ? 0 : str3.Length));

      return default(String);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Concat(String str0, String str1, String str2)
    {
      Contract.Ensures(Contract.Result<String>() != null);
      Contract.Ensures(Contract.Result<string>().Length ==
          (str0 == null ? 0 : str0.Length) +
          (str1 == null ? 0 : str1.Length) +
          (str2 == null ? 0 : str2.Length));

      return default(String);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Concat(String str0, String str1)
    {
      Contract.Ensures(Contract.Result<String>() != null);
      Contract.Ensures(Contract.Result<string>().Length ==
          (str0 == null ? 0 : str0.Length) +
          (str1 == null ? 0 : str1.Length));

      return default(String);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Concat(object[] args)
    {
      Contract.Requires(args != null);

      Contract.Ensures(Contract.Result<String>() != null);
      return default(String);
    }
#if !SILVERLIGHT
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Concat(object arg0, object arg1, object arg2, object arg3)
    {
      Contract.Ensures(Contract.Result<String>() != null);
      return default(String);
    }
#endif
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Concat(object arg0, object arg1, object arg2)
    {

      Contract.Ensures(Contract.Result<String>() != null);
      return default(String);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Concat(object arg0, object arg1)
    {

      Contract.Ensures(Contract.Result<String>() != null);
      return default(String);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Concat(object arg0)
    {

      Contract.Ensures(Contract.Result<String>() != null);
      return default(String);
    }

#if NETFRAMEWORK_4_0 || NETFRAMEWORK_4_5

    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Concat(IEnumerable<string> args)
    {

        Contract.Ensures(Contract.Result<String>() != null);
        return default(String);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Concat<T>(IEnumerable<T> args)
    {

        Contract.Ensures(Contract.Result<String>() != null);
        return default(String);
    }
#endif


    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Copy(String str)
    {
      Contract.Requires(str != null);

      return default(String);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Format(IFormatProvider provider, String format, object[] args)
    {
      Contract.Requires(format != null);
      Contract.Requires(args != null);

      Contract.Ensures(Contract.Result<String>() != null);
      return default(String);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Format(String format, object[] args)
    {
      Contract.Requires(format != null);
      Contract.Requires(args != null);
      Contract.Ensures(Contract.Result<String>() != null);
      return default(String);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Format(String format, object arg0, object arg1, object arg2)
    {
      Contract.Requires(format != null);

      Contract.Ensures(Contract.Result<String>() != null);
      return default(String);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Format(String format, object arg0, object arg1)
    {
      Contract.Requires(format != null);

      Contract.Ensures(Contract.Result<String>() != null);
      return default(String);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Format(String format, object arg0)
    {
      Contract.Requires(format != null);

      Contract.Ensures(Contract.Result<String>() != null);
      return default(String);
    }

    [Pure]
    public String Remove(int startIndex)
    {
      Contract.Requires(startIndex >= 0);
      Contract.Requires(startIndex < this.Length);

      Contract.Ensures(Contract.Result<String>() != null);
      Contract.Ensures(Contract.Result<String>().Length == startIndex);

      return default(String);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String Remove(int index, int count)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index + count <= Length);

      Contract.Ensures(Contract.Result<String>() != null);
      Contract.Ensures(Contract.Result<string>().Length == this.Length - count);

      return default(String);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String Replace(String oldValue, String newValue)
    {
      Contract.Requires(oldValue != null);
      Contract.Requires(oldValue.Length > 0);

      Contract.Ensures(Contract.Result<String>() != null);
      return default(String);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String Replace(char oldChar, char newChar)
    {
      Contract.Ensures(Contract.Result<String>() != null);

      return default(String);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String Insert(int startIndex, String value)
    {
      Contract.Requires(value != null);

      Contract.Requires(0 <= startIndex);
      Contract.Requires(startIndex <= this.Length); // When startIndex == this.Length, then it is added at the end of the instance

      Contract.Ensures(Contract.Result<string>().Length == this.Length + value.Length);
      Contract.Ensures(Contract.Result<String>() != null);

      return default(String);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String Trim()
    {
      Contract.Ensures(Contract.Result<String>() != null);

      return default(String);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String ToUpper(System.Globalization.CultureInfo culture) {
      Contract.Requires(culture != null);

      Contract.Ensures(Contract.Result<String>() != null);
      Contract.Ensures(Contract.Result<string>().Length == this.Length, "Are there languages for which this isn't true?!?");

      return default(String);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String ToUpper()
    {
      Contract.Ensures(Contract.Result<String>() != null);
      Contract.Ensures(Contract.Result<string>().Length == this.Length);

      return default(String);
    }
#if !SILVERLIGHT
    [Pure]
    public string ToUpperInvariant() {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length == this.Length);

      return default(string);
    }
#endif

    [Pure]
    public String ToLower(System.Globalization.CultureInfo culture) {
      Contract.Requires(culture != null);

      Contract.Ensures(Contract.Result<String>() != null);
      Contract.Ensures(Contract.Result<string>().Length == this.Length);

      return default(String);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String ToLower()
    {
      Contract.Ensures(Contract.Result<String>() != null);
      Contract.Ensures(Contract.Result<string>().Length == this.Length);

      return default(String);
    }
#if !SILVERLIGHT
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String ToLowerInvariant()
    {
      Contract.Ensures(Contract.Result<String>() != null);
      Contract.Ensures(Contract.Result<String>().Length == this.Length);

      return default(String);
    }
#endif

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public bool StartsWith(String value)
    {
      Contract.Requires(value != null);

      Contract.Ensures(!Contract.Result<bool>() || value.Length <= this.Length);

      return default(bool);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public bool StartsWith(String value, StringComparison comparisonType)
    {
      Contract.Requires(value != null);
      Contract.Requires(Enum.IsDefined(typeof(StringComparison), comparisonType));

      Contract.Ensures(!Contract.Result<bool>() || value.Length <= this.Length);

      return default(bool);
    }

#if !SILVERLIGHT
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public bool StartsWith(String value, bool ignoreCase, CultureInfo culture)
    {
      Contract.Requires(value != null);
      Contract.Ensures(!Contract.Result<bool>() || value.Length <= this.Length);

      return default(bool);
    }
#endif

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String PadRight(int totalWidth)
    {
      Contract.Requires(totalWidth >= 0);

      Contract.Ensures(Contract.Result<string>().Length == totalWidth);
      Contract.Ensures(Contract.Result<String>() != null);

      return default(String);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String PadRight(int totalWidth, char paddingChar)
    {
      Contract.Requires(totalWidth >= 0);
      Contract.Ensures(Contract.Result<string>().Length == totalWidth);

      Contract.Ensures(Contract.Result<String>() != null);

      return default(String);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String PadLeft(int totalWidth, char paddingChar)
    {
      Contract.Requires(totalWidth >= 0);

      Contract.Ensures(Contract.Result<string>().Length == totalWidth);
      Contract.Ensures(Contract.Result<String>() != null);

      return default(String);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String PadLeft(int totalWidth)
    {
      Contract.Requires(totalWidth >= 0);

      Contract.Ensures(Contract.Result<string>().Length == totalWidth);
      Contract.Ensures(Contract.Result<String>() != null);

      return default(String);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int LastIndexOf(char value)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Length);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int LastIndexOf(String value)
    {
      Contract.Requires(value != null);

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Length);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int LastIndexOf(char value, int startIndex)
    {
      Contract.Requires(this == Empty || startIndex >= 0);
      Contract.Requires(this == Empty || startIndex < this.Length);

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Length);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int LastIndexOf(String value, int startIndex)
    {
      Contract.Requires(value != null);
      Contract.Requires(this == Empty || startIndex >= 0);
      Contract.Requires(this == Empty || startIndex < this.Length);

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Length);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int LastIndexOf(String value, StringComparison comparisonType)
    {
      Contract.Requires(value != null);
      Contract.Requires(Enum.IsDefined(typeof(StringComparison), comparisonType));

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Length);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int LastIndexOf(char value, int startIndex, int count)
    {
      Contract.Requires(this == String.Empty || startIndex >= 0);
      Contract.Requires(this == String.Empty || count >= 0);
      Contract.Requires(this == String.Empty || startIndex + 1 - count >= 0);

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= startIndex - count);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int LastIndexOf(String value, int startIndex, int count)
    {
      Contract.Requires(value != null);

      Contract.Requires(this == String.Empty || startIndex >= 0);
      Contract.Requires(this == String.Empty || count >= 0);
      Contract.Requires(this == String.Empty || startIndex < this.Length);
      Contract.Requires(this == String.Empty || startIndex + 1 - count >= 0);

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Length);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int LastIndexOf(String value, int startIndex, StringComparison comparisonType)
    {
      Contract.Requires(value != null);

      Contract.Requires(this == String.Empty || startIndex >= 0);
      Contract.Requires(this == String.Empty || startIndex < this.Length);
      Contract.Requires(Enum.IsDefined(typeof(StringComparison), comparisonType));

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Length);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int LastIndexOf(String value, int startIndex, int count, StringComparison comparisonType)
    {
      Contract.Requires(value != null);

      Contract.Requires(this == String.Empty || startIndex >= 0);
      Contract.Requires(this == String.Empty || count >= 0);
      Contract.Requires(this == String.Empty || startIndex < this.Length);
      Contract.Requires(this == String.Empty || startIndex + 1 - count >= 0);
      Contract.Requires(Enum.IsDefined(typeof(StringComparison), comparisonType));

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Length);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int LastIndexOfAny(char[] anyOf)
    {
      Contract.Requires(anyOf != null);

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Length);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int LastIndexOfAny(char[] anyOf, int startIndex)
    {
      Contract.Requires(anyOf != null);
      Contract.Requires(this == String.Empty || startIndex >= 0);
      Contract.Requires(this == String.Empty || startIndex < Length);

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= Length);
      Contract.Ensures(Contract.Result<int>() == -1 || Contract.Result<int>() >= startIndex);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int LastIndexOfAny(char[] anyOf, int startIndex, int count)
    {
      Contract.Requires(anyOf != null);

      Contract.Requires(this == String.Empty || startIndex >= 0);
      Contract.Requires(this == String.Empty || startIndex < this.Length);
      Contract.Requires(this == String.Empty || count >= 0);
      Contract.Requires(this == String.Empty || startIndex - count >= 0);

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= startIndex);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int IndexOf(char value)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Length);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int IndexOf(String value)
    {
      Contract.Requires(value != null);

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() == -1 || Contract.Result<int>() <= Length - value.Length);

      return default(int);
    }

    // F: Funny enough the IndexOf* family do not have the special case for the empty string...

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int IndexOf(char value, int startIndex)
    {
      Contract.Requires(startIndex >= 0);
      Contract.Requires(startIndex <= Length);

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() == -1 || Contract.Result<int>() >= startIndex);

      Contract.Ensures(Contract.Result<int>() < this.Length);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int IndexOf(String value, int startIndex)
    {
      Contract.Requires(value != null);

      Contract.Requires(startIndex >= 0);
      Contract.Requires(startIndex <= Length);

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(value == String.Empty || Contract.Result<int>() < Length);
      Contract.Ensures(Contract.Result<int>() == -1 || Contract.Result<int>() >= startIndex);
      Contract.Ensures(value != String.Empty || Contract.Result<int>() == startIndex);

      return default(int);
    }

    [Pure]
    public int IndexOf(String value, StringComparison comparisonType)
    {
      Contract.Requires(value != null);
      Contract.Requires(Enum.IsDefined(typeof(StringComparison), comparisonType));

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() == -1 || Contract.Result<int>() <= Length - value.Length);
      Contract.Ensures(value != String.Empty || Contract.Result<int>() == 0);

      return default(int);
    }

    [Pure]
    public int IndexOf(char value, int startIndex, int count)
    {
      Contract.Requires(startIndex >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(startIndex + count <= Length);

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < startIndex + count);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int IndexOf(String value, int startIndex, int count)
    {
      Contract.Requires(value != null);
      Contract.Requires(startIndex >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(startIndex + count <= Length);

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(value == String.Empty || Contract.Result<int>() < startIndex + count);

      Contract.Ensures(Contract.Result<int>() == -1 || Contract.Result<int>() >= startIndex);

      Contract.Ensures(value != String.Empty || Contract.Result<int>() == startIndex);

      return default(int);
    }


    [Pure]
    public int IndexOf(String value, int startIndex, StringComparison comparisonType)
    {
      Contract.Requires(value != null);
      Contract.Requires(startIndex >= 0);
      Contract.Requires(startIndex <= Length);
      Contract.Requires(Enum.IsDefined(typeof(StringComparison), comparisonType));

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(value == String.Empty || Contract.Result<int>() < Length);
      Contract.Ensures(Contract.Result<int>() == -1 || Contract.Result<int>() >= startIndex);

      Contract.Ensures(value != String.Empty || Contract.Result<int>() == startIndex);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int IndexOf(String value, int startIndex, int count, StringComparison comparisonType)
    {
      Contract.Requires(value != null);
      Contract.Requires(startIndex >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(startIndex + count <= Length);
      Contract.Requires(Enum.IsDefined(typeof(StringComparison), comparisonType));

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(value == String.Empty || Contract.Result<int>() < startIndex + count);
      Contract.Ensures(Contract.Result<int>() == -1 || Contract.Result<int>() >= startIndex);
      Contract.Ensures(value != String.Empty || Contract.Result<int>() == startIndex);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int IndexOfAny(char[] anyOf)
    {
      Contract.Requires(anyOf != null);

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Length);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int IndexOfAny(char[] anyOf, int startIndex)
    {
      Contract.Requires(anyOf != null);

      Contract.Requires(startIndex >= 0);
      Contract.Requires(startIndex < Length);

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Length);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public int IndexOfAny(char[] anyOf, int startIndex, int count)
    {
      Contract.Requires(anyOf != null);

      Contract.Requires(startIndex >= 0);
      Contract.Requires(count >= 0);

      Contract.Requires(startIndex + count < Length);

      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Length);

      return default(int);
    }


    public static readonly string/*!*/ Empty;

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public bool EndsWith(String value)
    {
      Contract.Requires(value != null);
      Contract.Ensures(!Contract.Result<bool>() || value.Length <= this.Length);
      return default(bool);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public bool EndsWith(String value, StringComparison comparisonType)
    {
      Contract.Requires(value != null);
      Contract.Requires(Enum.IsDefined(typeof(StringComparison), comparisonType));

      Contract.Ensures(!Contract.Result<bool>() || this.Length >= value.Length);
      return default(bool);
    }
#if !SILVERLIGHT
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public bool EndsWith(String value, bool ignoreCase, CultureInfo culture)
    {
      Contract.Requires(value != null);
      Contract.Ensures(!Contract.Result<bool>() || this.Length >= value.Length);
      return default(bool);
    }
#endif

    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static int CompareOrdinal(String strA, String strB)
    {

      return default(int);
    }

    [Pure]
    public static int CompareOrdinal(String strA, int indexA, String strB, int indexB, int length)
    {
      Contract.Requires(indexA >= 0);
      Contract.Requires(indexB >= 0);
      Contract.Requires(length >= 0);

      // From the documentation (and a quick test) one can derive that == is admissible
      Contract.Requires(indexA <= strA.Length);
      Contract.Requires(indexB <= strB.Length);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static int Compare(String strA, String strB)
    {
      return default(int);
    }

#if !SILVERLIGHT
  [Pure]
    public static int Compare(String strA, int indexA, String strB, int indexB, int length, bool ignoreCase, System.Globalization.CultureInfo culture) {
      Contract.Requires(culture != null);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static int Compare(String strA, int indexA, String strB, int indexB, int length, bool ignoreCase)
    {
      Contract.Requires(indexA >= 0);
      Contract.Requires(indexB >= 0);
      Contract.Requires(length >= 0);
      Contract.Requires(indexA <= strA.Length);
      Contract.Requires(indexB <= strB.Length);
      Contract.Requires((strA != null && strB != null) || length == 0);

      return default(int);
    }
#endif

    [Pure]
    public static int Compare(String strA, int indexA, String strB, int indexB, int length, CultureInfo culture, CompareOptions options) {
      Contract.Requires(culture != null);

      return default(int);
    }
    [Pure]
    public static int Compare(string strA, string strB, StringComparison comparisonType) {
      return default(int);
    }
    [Pure]
    public static int Compare(string strA, int indexA, string strB, int indexB, int length, StringComparison comparisonType)
    {
      Contract.Requires(indexA >= 0);
      Contract.Requires(indexB >= 0);
      Contract.Requires(length >= 0);
      Contract.Requires(indexA <= strA.Length);
      Contract.Requires(indexB <= strB.Length);
      Contract.Requires((strA != null && strB != null) || length == 0);
      Contract.Requires(Enum.IsDefined(typeof(StringComparison), comparisonType));

      return default(int);
    }


    [Pure]
    public static int Compare(String strA, String strB, CultureInfo culture, CompareOptions options) {
      Contract.Requires(culture != null);
      return default(int);
    }

    [Pure]
    public static int Compare(String strA, int indexA, String strB, int indexB, int length)
    {
      Contract.Requires(indexA >= 0);
      Contract.Requires(indexB >= 0);
      Contract.Requires(length >= 0);
      Contract.Requires(indexA <= strA.Length);
      Contract.Requires(indexB <= strB.Length);
      Contract.Requires((strA != null && strB != null) || length == 0);

      return default(int);
    }

#if !SILVERLIGHT
    [Pure]
    public static int Compare(String strA, String strB, bool ignoreCase, System.Globalization.CultureInfo culture)
    {
      Contract.Requires(culture != null);

      return default(int);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static int Compare(String strA, String strB, bool ignoreCase)
    {
      return default(int);
    }
#endif

    [Pure]
    public bool Contains(string value) {
      Contract.Requires(value != null);
      Contract.Ensures(!Contract.Result<bool>() || this.Length >= value.Length);

      return default(bool);
    }

    public String(char c, int count)
    {
      Contract.Ensures(this.Length == count);
    }
    public String(char[] array)
    {
      Contract.Ensures(array != null || this.Length == 0);
      Contract.Ensures(array == null || this.Length == array.Length);
    }

    public String(char[] value, int startIndex, int length)
    {
      Contract.Requires(value != null);
      Contract.Requires(startIndex >= 0);
      Contract.Requires(length >= 0);
      Contract.Requires(startIndex + length <= value.Length);

      Contract.Ensures(this.Length == length);
    }

    /* These should all be pointer arguments

              return default(String);
            }
            public String (ref SByte arg0, int arg1, int arg2, System.Text.Encoding arg3) {

              return default(String);
            }
            public String (ref SByte arg0, int arg1, int arg2) {

              return default(String);
            }
            public String (ref SByte arg0) {

              return default(String);
            }
            public String (ref char arg0, int arg1, int arg2) {

              return default(String);
            }
            public String (ref char arg0) {


              return default(String);
            } */

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String TrimEnd(params char[] trimChars)
    {
      Contract.Ensures(Contract.Result<String>() != null);
      return default(String);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String TrimStart(params char[] trimChars)
    {
      Contract.Ensures(Contract.Result<String>() != null);
      return default(String);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String Trim(params char[] trimChars)
    {
      Contract.Ensures(Contract.Result<String>() != null);
      return default(String);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String Substring(int startIndex, int length)
    {
      Contract.Requires(0 <= startIndex);
      Contract.Requires(0 <= length);
      Contract.Requires(startIndex <= this.Length );
      Contract.Requires(startIndex <= this.Length - length);

      Contract.Ensures(Contract.Result<String>() != null);
      Contract.Ensures(Contract.Result<string>().Length == length);

      return default(String);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String Substring(int startIndex)
    {
      Contract.Requires(0 <= startIndex);
      Contract.Requires(startIndex <= this.Length);
      Contract.Ensures(Contract.Result<string>().Length == this.Length - startIndex);

      Contract.Ensures(Contract.Result<String>() != null);
      return default(String);
    }

#if !SILVERLIGHT
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String[] Split(char[] arg0, int arg1)
    {
      Contract.Ensures(Contract.Result<String[]>() != null);
      Contract.Ensures(Contract.Result<String[]>().Length >= 1);
      Contract.Ensures(Contract.Result<String[]>()[0].Length <= this.Length);

      Contract.Ensures(Contract.ForAll(0, Contract.Result<string[]>().Length, i => Contract.Result<string[]>()[i] != null));

      return default(String[]);
    }
#endif

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public String[] Split(char[] separator)
    {
      Contract.Ensures(Contract.Result<String[]>() != null);
      Contract.Ensures(Contract.Result<String[]>().Length >= 1);
      Contract.Ensures(Contract.Result<String[]>()[0].Length <= this.Length);

      Contract.Ensures(Contract.ForAll(0, Contract.Result<string[]>().Length, i => Contract.Result<string[]>()[i] != null));

      return default(String[]);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public string[] Split(char[] separator, StringSplitOptions options)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.ForAll(0, Contract.Result<string[]>().Length, i => Contract.Result<string[]>()[i] != null));

      return default(string[]);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public string[] Split(string[] separator, StringSplitOptions options)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.ForAll(0, Contract.Result<string[]>().Length, i => Contract.Result<string[]>()[i] != null));

      return default(string[]);
    }
#if !SILVERLIGHT
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public string[] Split(char[] separator, int count, StringSplitOptions options)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.ForAll(0, Contract.Result<string[]>().Length, i => Contract.Result<string[]>()[i] != null));

      return default(string[]);
    }
#endif


#if !SILVERLIGHT_4_0_WP
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
#if !SILVERLIGHT
    public
#else
    internal
#endif
    string[] Split(string[] separator, int count, StringSplitOptions options)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.ForAll(0, Contract.Result<string[]>().Length, i => Contract.Result<string[]>()[i] != null));

      return default(string[]);
    }
#endif

#if !SILVERLIGHT
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public char[] ToCharArray(int startIndex, int length)
    {
      Contract.Requires(startIndex >= 0);
      Contract.Requires(startIndex <= this.Length);
      Contract.Requires(startIndex + length <= this.Length);
      Contract.Requires(length >= 0);

      Contract.Ensures(Contract.Result<char[]>() != null);
      return default(char[]);
    }
#endif

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public char[] ToCharArray()
    {

      Contract.Ensures(Contract.Result<char[]>() != null);
      return default(char[]);
    }
    public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
    {
      Contract.Requires(destination != null);
      Contract.Requires(count >= 0);
      Contract.Requires(sourceIndex >= 0);
      Contract.Requires(count <= (this.Length - sourceIndex));
      Contract.Requires(destinationIndex <= (destination.Length - count));
      Contract.Requires(destinationIndex >= 0);

    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static bool operator !=(String a, String b)
    {
      return default(bool);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static bool operator ==(String a, String b)
    {
      return default(bool);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static bool Equals(String a, String b)
    {
      Contract.Ensures((object)a != (object)b || Contract.Result<bool>());

      return default(bool);
    }
    [Pure]
    public virtual bool Equals(String arg0)
    {
      Contract.Ensures((object)this != (object)arg0 || Contract.Result<bool>());
      return default(bool);
    }

    [Pure]
    public bool Equals(String value, StringComparison comparisonType) {
      return default(bool);
    }

    [Pure]
    public static bool Equals(String a, String b, StringComparison comparisonType) {
      return default(bool);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Join(String separator, String[] value, int startIndex, int count)
    {
      Contract.Requires(value != null);
      Contract.Requires(startIndex >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(startIndex + count <= value.Length);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(String);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Join(String separator, String[] value)
    {
      Contract.Requires(value != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(String);
    }

#if NETFRAMEWORK_4_0 || NETFRAMEWORK_4_5

    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static String Join(String separator, Object[] value)
    {
        Contract.Requires(value != null);
        Contract.Ensures(Contract.Result<string>() != null);

        return default(String);
    }

    [Pure]
    public static string Join(string separator, IEnumerable<string> values)
    {
      Contract.Requires(values != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(String);
    }

    [Pure]
    public static string Join<T>(string separator, IEnumerable<T> values)
    {
      Contract.Requires(values != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(String);
    }
#endif

    [Pure]
    public static bool IsNullOrEmpty(string str)
    {
      Contract.Ensures( Contract.Result<bool>() && (str == null || str.Length == 0) ||
                       !Contract.Result<bool>() && str != null && str.Length > 0);

#if NETFRAMEWORK_4_5 || NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
      Contract.Ensures(!Contract.Result<bool>() || IsNullOrWhiteSpace(str));
#endif
      return default(bool);
    }

#if NETFRAMEWORK_4_5 || NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
    [Pure]
    public static bool IsNullOrWhiteSpace(string str)
    {
      Contract.Ensures(Contract.Result<bool>() && (str == null || str.Length == 0) ||
                       !Contract.Result<bool>() && str != null && str.Length > 0);
      return default(bool);
    }
#endif


  }
}

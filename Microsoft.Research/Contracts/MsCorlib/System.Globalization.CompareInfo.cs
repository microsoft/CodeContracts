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

namespace System.Globalization
{

  public enum CompareOptions {
    // Summary:
    //     Indicates the default option settings for string comparisons.
    None = 0,
    //
    // Summary:
    //     Indicates that the string comparison must ignore case.
    IgnoreCase = 1,
    //
    // Summary:
    //     Indicates that the string comparison must ignore nonspacing combining characters,
    //     such as diacritics. The Unicode Standard defines combining characters as
    //     characters that are combined with base characters to produce a new character.
    //     Nonspacing combining characters do not occupy a spacing position by themselves
    //     when rendered. For more information on nonspacing combining characters, see
    //     The Unicode Standard at the Unicode home page.
    IgnoreNonSpace = 2,
    //
    // Summary:
    //     Indicates that the string comparison must ignore symbols, such as white-space
    //     characters, punctuation, currency symbols, the percent sign, mathematical
    //     symbols, the ampersand, and so on.
    IgnoreSymbols = 4,
    //
    // Summary:
    //     Indicates that the string comparison must ignore the Kana type. Kana type
    //     refers to Japanese hiragana and katakana characters, which represent phonetic
    //     sounds in the Japanese language. Hiragana is used for native Japanese expressions
    //     and words, while katakana is used for words borrowed from other languages,
    //     such as "computer" or "Internet". A phonetic sound can be expressed in both
    //     hiragana and katakana. If this value is selected, the hiragana character
    //     for one sound is considered equal to the katakana character for the same
    //     sound.
    IgnoreKanaType = 8,
    //
    // Summary:
    //     Indicates that the string comparison must ignore the character width. For
    //     example, Japanese katakana characters can be written as full-width or half-width.
    //     If this value is selected, the katakana characters written as full-width
    //     are considered equal to the same characters written as half-width.
    IgnoreWidth = 16,
    //
    // Summary:
    //     String comparison must ignore case, then perform an ordinal comparison. This
    //     technique is equivalent to converting the string to uppercase using the invariant
    //     culture and then performing an ordinal comparison on the result.
    OrdinalIgnoreCase = 268435456,
    //
    // Summary:
    //     Indicates that the string comparison must use the string sort algorithm.
    //     In a string sort, the hyphen and the apostrophe, as well as other nonalphanumeric
    //     symbols, come before alphanumeric characters.
    StringSort = 536870912,
    //
    // Summary:
    //     Indicates that the string comparison must use the Unicode values of each
    //     character, leading to a fast comparison but one that is culture-insensitive.
    //     A string starting with "U+xxxx" comes before a string starting with "U+yyyy",
    //     if xxxx is less than yyyy. This value cannot be combined with other System.Globalization.CompareOptions
    //     values and must be used alone.
    Ordinal = 1073741824,
  }

  public class CompareInfo
  {
#if false
    public int LCID
    {
      get;
    }

    public SortKey GetSortKey(string source)
    {

      return default(SortKey);
    }

    public SortKey GetSortKey(string source, CompareOptions options)
    {

      return default(SortKey);
    }

    public int LastIndexOf(string source, string value, int startIndex, int count, CompareOptions options)
    {
      Contract.Requires(count >= 0);

      return default(int);
    }
    public int LastIndexOf(string source, Char value, int startIndex, int count, CompareOptions options)
    {
      Contract.Requires(count >= 0);

      return default(int);
    }

    public int LastIndexOf(string source, string value, int startIndex, int count)
    {
      Contract.Requires(count >= 0);

      return default(int);
    }
    public int LastIndexOf(string source, string value, int startIndex, CompareOptions options)
    {

      return default(int);
    }
    public int LastIndexOf(string source, string value, int startIndex)
    {

      return default(int);
    }
    public int LastIndexOf(string source, Char value, int startIndex, int count)
    {
      Contract.Requires(count >= 0);

      return default(int);
    }
    public int LastIndexOf(string source, Char value, int startIndex, CompareOptions options)
    {

      return default(int);
    }
    public int LastIndexOf(string source, Char value, int startIndex)
    {

      return default(int);
    }
    public int LastIndexOf(string source, string value, CompareOptions options)
    {
      Contract.Requires(source != null);

      return default(int);
    }
    public int LastIndexOf(string source, Char value, CompareOptions options)
    {
      Contract.Requires(source != null);

      return default(int);
    }
    public int LastIndexOf(string source, string value)
    {
      Contract.Requires(source != null);

      return default(int);
    }
    public int LastIndexOf(string source, Char value)
    {
      Contract.Requires(source != null);

      return default(int);
    }
    public int IndexOf(string source, string value, int startIndex, int count, CompareOptions options)
    {
      Contract.Requires(count >= 0);

      return default(int);
    }
    public int IndexOf(string source, Char value, int startIndex, int count, CompareOptions options)
    {
      Contract.Requires(count >= 0);

      return default(int);
    }
    public int IndexOf(string source, string value, int startIndex, int count)
    {
      Contract.Requires(count >= 0);

      return default(int);
    }
    public int IndexOf(string source, string value, int startIndex, CompareOptions options)
    {

      return default(int);
    }
    public int IndexOf(string source, string value, int startIndex)
    {
      Contract.Requires(source != null);
      Contract.Requires(startIndex <= source.Length);

      return default(int);
    }
    public int IndexOf(string source, Char value, int startIndex, int count)
    {
      Contract.Requires(count >= 0);

      return default(int);
    }
    public int IndexOf(string source, Char value, int startIndex, CompareOptions options)
    {

      return default(int);
    }
    public int IndexOf(string source, Char value, int startIndex)
    {
      Contract.Requires(source != null);
      Contract.Requires(startIndex <= source.Length);

      return default(int);
    }
    public int IndexOf(string source, string value, CompareOptions options)
    {

      return default(int);
    }
    public int IndexOf(string source, Char value, CompareOptions options)
    {

      return default(int);
    }
    public int IndexOf(string source, string value)
    {

      return default(int);
    }
    public int IndexOf(string source, Char value)
    {

      return default(int);
    }
    public bool IsSuffix(string source, string suffix)
    {

      return default(bool);
    }
    public bool IsSuffix(string source, string suffix, CompareOptions options)
    {
      Contract.Requires(source != null);
      Contract.Requires(suffix != null);
      Contract.Requires((int)((int)options & -32) == 0 || (int)options == 1073741824);

      return default(bool);
    }
    public bool IsPrefix(string source, string prefix)
    {

      return default(bool);
    }
    public bool IsPrefix(string source, string prefix, CompareOptions options)
    {
      Contract.Requires(source != null);
      Contract.Requires(prefix != null);
      Contract.Requires((int)((int)options & -32) == 0 || (int)options == 1073741824);

      return default(bool);
    }
    public int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2, CompareOptions options)
    {
      Contract.Requires(length1 >= 0);
      Contract.Requires(length2 >= 0);

      return default(int);
    }
    public int Compare(string string1, int offset1, string string2, int offset2)
    {

      return default(int);
    }
    public int Compare(string string1, int offset1, string string2, int offset2, CompareOptions options)
    {

      return default(int);
    }
    public int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2)
    {

      return default(int);
    }
    public int Compare(string string1, string string2, CompareOptions options)
    {

      return default(int);
    }
    public int Compare(string string1, string string2)
    {

      return default(int);
    }
    public static CompareInfo GetCompareInfo(string name)
    {
      Contract.Requires(name != null);

      return default(CompareInfo);
    }
    public static CompareInfo GetCompareInfo(int culture)
    {

      return default(CompareInfo);
    }
    public static CompareInfo GetCompareInfo(string name, System.Reflection.Assembly assembly)
    {
      Contract.Requires(name != null);
      Contract.Requires(assembly != null);

      return default(CompareInfo);
    }
    public static CompareInfo GetCompareInfo(int culture, System.Reflection.Assembly assembly)
    {
      Contract.Requires(assembly != null);
      return default(CompareInfo);
    }
#endif
  }
}
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
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Globalization {
  // Summary:
  //     Defines the string comparison options to use with System.Globalization.CompareInfo.
  [Serializable]
  [Flags]
  [ComVisible(true)]
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
    //     The Unicode Standard at the Unicode home pagehttp://go.microsoft.com/fwlink/?LinkId=37123.
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
}

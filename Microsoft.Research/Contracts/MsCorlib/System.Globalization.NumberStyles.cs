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
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Globalization {
  // Summary:
  //     Determines the styles permitted in numeric string arguments that are passed
  //     to the Parse methods of the numeric base type classes.
  public enum NumberStyles {
    // Summary:
    //     Indicates that none of the bit styles are allowed.
    None = 0,
    //
    // Summary:
    //     Indicates that a leading white-space character must be ignored during parsing.
    //     Valid white-space characters have the Unicode values U+0009, U+000A, U+000B,
    //     U+000C, U+000D, and U+0020.
    AllowLeadingWhite = 1,
    //
    // Summary:
    //     Indicates that trailing white-space character must be ignored during parsing.
    //     Valid white-space characters have the Unicode values U+0009, U+000A, U+000B,
    //     U+000C, U+000D, and U+0020.
    AllowTrailingWhite = 2,
    //
    // Summary:
    //     Indicates that the numeric string can have a leading sign. Valid leading
    //     sign characters are determined by the System.Globalization.NumberFormatInfo.PositiveSign
    //     and System.Globalization.NumberFormatInfo.NegativeSign properties of System.Globalization.NumberFormatInfo.
    AllowLeadingSign = 4,
    //
    // Summary:
    //     Indicates that the AllowLeadingWhite, AllowTrailingWhite, and AllowLeadingSign
    //     styles are used. This is a composite number style.
    Integer = 7,
    //
    // Summary:
    //     Indicates that the numeric string can have a trailing sign. Valid trailing
    //     sign characters are determined by the System.Globalization.NumberFormatInfo.PositiveSign
    //     and System.Globalization.NumberFormatInfo.NegativeSign properties of System.Globalization.NumberFormatInfo.
    AllowTrailingSign = 8,
    //
    // Summary:
    //     Indicates that the numeric string can have one pair of parentheses enclosing
    //     the number.
    AllowParentheses = 16,
    //
    // Summary:
    //     Indicates that the numeric string can have a decimal point. Valid decimal
    //     point characters are determined by the System.Globalization.NumberFormatInfo.NumberDecimalSeparator
    //     and System.Globalization.NumberFormatInfo.CurrencyDecimalSeparator properties
    //     of System.Globalization.NumberFormatInfo.
    AllowDecimalPoint = 32,
    //
    // Summary:
    //     Indicates that the numeric string can have group separators, for example,
    //     separating the hundreds from the thousands. Valid group separator characters
    //     are determined by the System.Globalization.NumberFormatInfo.NumberGroupSeparator
    //     and System.Globalization.NumberFormatInfo.CurrencyGroupSeparator properties
    //     of System.Globalization.NumberFormatInfo and the number of digits in each
    //     group is determined by the System.Globalization.NumberFormatInfo.NumberGroupSizes
    //     and System.Globalization.NumberFormatInfo.CurrencyGroupSizes properties of
    //     System.Globalization.NumberFormatInfo.
    AllowThousands = 64,
    //
    // Summary:
    //     Indicates that the AllowLeadingWhite, AllowTrailingWhite, AllowLeadingSign,
    //     AllowTrailingSign, AllowDecimalPoint, and AllowThousands styles are used.
    //     This is a composite number style.
    Number = 111,
    //
    // Summary:
    //     Indicates that the numeric string can be in exponential notation.
    AllowExponent = 128,
    //
    // Summary:
    //     Indicates that the AllowLeadingWhite, AllowTrailingWhite, AllowLeadingSign,
    //     AllowDecimalPoint, and AllowExponent styles are used. This is a composite
    //     number style.
    Float = 167,
    //
    // Summary:
    //     Indicates that the numeric string is parsed as currency if it contains a
    //     currency symbol. Otherwise, it is parsed as a number. Valid currency symbols
    //     are determined by the System.Globalization.NumberFormatInfo.CurrencySymbol
    //     property of System.Globalization.NumberFormatInfo.
    AllowCurrencySymbol = 256,
    //
    // Summary:
    //     Indicates that all styles, except AllowExponent and AllowHexSpecifier, are
    //     used. This is a composite number style.
    Currency = 383,
    //
    // Summary:
    //     Indicates that all styles, except AllowHexSpecifier, are used. This is a
    //     composite number style.
    Any = 511,
    //
    // Summary:
    //     Indicates that the numeric string represents a hexadecimal value. Valid hexadecimal
    //     values include the numeric digits 0-9 and the hexadecimal digits A-F and
    //     a-f. Hexadecimal values can be left-padded with zeros. Strings parsed using
    //     this style are not permitted to be prefixed with "0x".
    AllowHexSpecifier = 512,
    //
    // Summary:
    //     Indicates that the AllowLeadingWhite, AllowTrailingWhite, and AllowHexSpecifier
    //     styles are used. This is a composite number style.
    HexNumber = 515,
  }
}

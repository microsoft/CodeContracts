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
  //     Defines the Unicode category of a character.
  [Serializable]
  [ComVisible(true)]
  public enum UnicodeCategory {
    // Summary:
    //     Indicates that the character is an uppercase letter. Signified by the Unicode
    //     designation "Lu" (letter, uppercase). The value is 0.
    UppercaseLetter = 0,
    //
    // Summary:
    //     Indicates that the character is a lowercase letter. Signified by the Unicode
    //     designation "Ll" (letter, lowercase). The value is 1.
    LowercaseLetter = 1,
    //
    // Summary:
    //     Indicates that the character is a titlecase letter. Signified by the Unicode
    //     designation "Lt" (letter, titlecase). The value is 2.
    TitlecaseLetter = 2,
    //
    // Summary:
    //     Indicates that the character is a modifier letter, which is free-standing
    //     spacing character that indicates modifications of a preceding letter. Signified
    //     by the Unicode designation "Lm" (letter, modifier). The value is 3.
    ModifierLetter = 3,
    //
    // Summary:
    //     Indicates that the character is a letter that is not an uppercase letter,
    //     a lowercase letter, a titlecase letter, or a modifier letter. Signified by
    //     the Unicode designation "Lo" (letter, other). The value is 4.
    OtherLetter = 4,
    //
    // Summary:
    //     Indicates that the character is a nonspacing character, which indicates modifications
    //     of a base character. Signified by the Unicode designation "Mn" (mark, nonspacing).
    //     The value is 5.
    NonSpacingMark = 5,
    //
    // Summary:
    //     Indicates that the character is a spacing character, which indicates modifications
    //     of a base character and affects the width of the glyph for that base character.
    //     Signified by the Unicode designation "Mc" (mark, spacing combining). The
    //     value is 6.
    SpacingCombiningMark = 6,
    //
    // Summary:
    //     Indicates that the character is an enclosing mark, which is a nonspacing
    //     combining character that surrounds all previous characters up to and including
    //     a base character. Signified by the Unicode designation "Me" (mark, enclosing).
    //     The value is 7.
    EnclosingMark = 7,
    //
    // Summary:
    //     Indicates that the character is a decimal digit, that is, in the range 0
    //     through 9. Signified by the Unicode designation "Nd" (number, decimal digit).
    //     The value is 8.
    DecimalDigitNumber = 8,
    //
    // Summary:
    //     Indicates that the character is a number represented by a letter, instead
    //     of a decimal digit, for example, the Roman numeral for five, which is "V".
    //     The indicator is signified by the Unicode designation "Nl" (number, letter).
    //     The value is 9.
    LetterNumber = 9,
    //
    // Summary:
    //     Indicates that the character is a number that is neither a decimal digit
    //     nor a letter number, for example, the fraction 1/2. The indicator is signified
    //     by the Unicode designation "No" (number, other). The value is 10.
    OtherNumber = 10,
    //
    // Summary:
    //     Indicates that the character is a space character, which has no glyph but
    //     is not a control or format character. Signified by the Unicode designation
    //     "Zs" (separator, space). The value is 11.
    SpaceSeparator = 11,
    //
    // Summary:
    //     Indicates that the character is used to separate lines of text. Signified
    //     by the Unicode designation "Zl" (separator, line). The value is 12.
    LineSeparator = 12,
    //
    // Summary:
    //     Indicates that the character is used to separate paragraphs. Signified by
    //     the Unicode designation "Zp" (separator, paragraph). The value is 13.
    ParagraphSeparator = 13,
    //
    // Summary:
    //     Indicates that the character is a control code, with a Unicode value of U+007F
    //     or in the range U+0000 through U+001F or U+0080 through U+009F. Signified
    //     by the Unicode designation "Cc" (other, control). The value is 14.
    Control = 14,
    //
    // Summary:
    //     Indicates that the character is a format character, which is not normally
    //     rendered but affects the layout of text or the operation of text processes.
    //     Signified by the Unicode designation "Cf" (other, format). The value is 15.
    Format = 15,
    //
    // Summary:
    //     Indicates that the character is a high surrogate or a low surrogate. Surrogate
    //     code values are in the range U+D800 through U+DFFF. Signified by the Unicode
    //     designation "Cs" (other, surrogate). The value is 16.
    Surrogate = 16,
    //
    // Summary:
    //     Indicates that the character is a private-use character, with a Unicode value
    //     in the range U+E000 through U+F8FF. Signified by the Unicode designation
    //     "Co" (other, private use). The value is 17.
    PrivateUse = 17,
    //
    // Summary:
    //     Indicates that the character is a connector punctuation, which connects two
    //     characters. Signified by the Unicode designation "Pc" (punctuation, connector).
    //     The value is 18.
    ConnectorPunctuation = 18,
    //
    // Summary:
    //     Indicates that the character is a dash or a hyphen. Signified by the Unicode
    //     designation "Pd" (punctuation, dash). The value is 19.
    DashPunctuation = 19,
    //
    // Summary:
    //     Indicates that the character is the opening character of one of the paired
    //     punctuation marks, such as parentheses, square brackets, and braces. Signified
    //     by the Unicode designation "Ps" (punctuation, open). The value is 20.
    OpenPunctuation = 20,
    //
    // Summary:
    //     Indicates that the character is the closing character of one of the paired
    //     punctuation marks, such as parentheses, square brackets, and braces. Signified
    //     by the Unicode designation "Pe" (punctuation, close). The value is 21.
    ClosePunctuation = 21,
    //
    // Summary:
    //     Indicates that the character is an opening or initial quotation mark. Signified
    //     by the Unicode designation "Pi" (punctuation, initial quote). The value is
    //     22.
    InitialQuotePunctuation = 22,
    //
    // Summary:
    //     Indicates that the character is a closing or final quotation mark. Signified
    //     by the Unicode designation "Pf" (punctuation, final quote). The value is
    //     23.
    FinalQuotePunctuation = 23,
    //
    // Summary:
    //     Indicates that the character is a punctuation that is not a connector punctuation,
    //     a dash punctuation, an open punctuation, a close punctuation, an initial
    //     quote punctuation, or a final quote punctuation. Signified by the Unicode
    //     designation "Po" (punctuation, other). The value is 24.
    OtherPunctuation = 24,
    //
    // Summary:
    //     Indicates that the character is a mathematical symbol, such as "+" or "=
    //     ". Signified by the Unicode designation "Sm" (symbol, math). The value is
    //     25.
    MathSymbol = 25,
    //
    // Summary:
    //     Indicates that the character is a currency symbol. Signified by the Unicode
    //     designation "Sc" (symbol, currency). The value is 26.
    CurrencySymbol = 26,
    //
    // Summary:
    //     Indicates that the character is a modifier symbol, which indicates modifications
    //     of surrounding characters. For example, the fraction slash indicates that
    //     the number to the left is the numerator and the number to the right is the
    //     denominator. The indicator is signified by the Unicode designation "Sk" (symbol,
    //     modifier). The value is 27.
    ModifierSymbol = 27,
    //
    // Summary:
    //     Indicates that the character is a symbol that is not a mathematical symbol,
    //     a currency symbol or a modifier symbol. Signified by the Unicode designation
    //     "So" (symbol, other). The value is 28.
    OtherSymbol = 28,
    //
    // Summary:
    //     Indicates that the character is not assigned to any Unicode category. Signified
    //     by the Unicode designation "Cn" (other, not assigned). The value is 29.
    OtherNotAssigned = 29,
  }
}

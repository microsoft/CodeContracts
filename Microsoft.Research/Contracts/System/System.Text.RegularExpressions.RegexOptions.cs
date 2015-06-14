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

using System.Diagnostics.Contracts;
using System;

namespace System.Text.RegularExpressions {
  // Summary:
  //     Provides enumerated values to use to set regular expression options.
  [Flags]
  public enum RegexOptions {
    // Summary:
    //     Specifies that no options are set.
    None = 0,
    //
    // Summary:
    //     Specifies case-insensitive matching.
    IgnoreCase = 1,
    //
    // Summary:
    //     Multiline mode. Changes the meaning of ^ and $ so they match at the beginning
    //     and end, respectively, of any line, and not just the beginning and end of
    //     the entire string.
    Multiline = 2,
    //
    // Summary:
    //     Specifies that the only valid captures are explicitly named or numbered groups
    //     of the form (?<name>�). This allows unnamed parentheses to act as noncapturing
    //     groups without the syntactic clumsiness of the expression (?:�).
    ExplicitCapture = 4,
    //
    // Summary:
    //     Specifies that the regular expression is compiled to an assembly. This yields
    //     faster execution but increases startup time.
    Compiled = 8,
    //
    // Summary:
    //     Specifies single-line mode. Changes the meaning of the dot (.) so it matches
    //     every character (instead of every character except \n).
    Singleline = 16,
    //
    // Summary:
    //     Eliminates unescaped white space from the pattern and enables comments marked
    //     with #. However, the System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace
    //     value does not affect or eliminate white space in character classes.
    IgnorePatternWhitespace = 32,
    //
    // Summary:
    //     Specifies that the search will be from right to left instead of from left
    //     to right.
    RightToLeft = 64,
    //
    // Summary:
    //     Enables ECMAScript-compliant behavior for the expression. This value can
    //     be used only in conjunction with the System.Text.RegularExpressions.RegexOptions.IgnoreCase,
    //     System.Text.RegularExpressions.RegexOptions.Multiline, and System.Text.RegularExpressions.RegexOptions.Compiled
    //     values. The use of this value with any other values results in an exception.
    ECMAScript = 256,
    //
    // Summary:
    //     Specifies that cultural differences in language is ignored. See [<topic://cpconPerformingCulture-InsensitiveOperationsInRegularExpressionsNamespace>]
    //     for more information.
    CultureInvariant = 512,
  }
}

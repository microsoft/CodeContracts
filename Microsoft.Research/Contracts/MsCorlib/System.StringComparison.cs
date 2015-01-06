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

namespace System {
  // Summary:
  //     Specifies the culture, case, and sort rules to be used by certain overloads
  //     of the System.String.Compare(System.String,System.String) and System.String.Equals(System.Object)
  //     methods.
  public enum StringComparison {
    // Summary:
    //     Compare strings using culture-sensitive sort rules and the current culture.
    CurrentCulture = 0,
    //
    // Summary:
    //     Compare strings using culture-sensitive sort rules, the current culture,
    //     and ignoring the case of the strings being compared.
    CurrentCultureIgnoreCase = 1,
    //
    // Summary:
    //     Compare strings using culture-sensitive sort rules and the invariant culture.
    InvariantCulture = 2,
    //
    // Summary:
    //     Compare strings using culture-sensitive sort rules, the invariant culture,
    //     and ignoring the case of the strings being compared.
    InvariantCultureIgnoreCase = 3,
    //
    // Summary:
    //     Compare strings using ordinal sort rules.
    Ordinal = 4,
    //
    // Summary:
    //     Compare strings using ordinal sort rules and ignoring the case of the strings
    //     being compared.
    OrdinalIgnoreCase = 5,
  }
}

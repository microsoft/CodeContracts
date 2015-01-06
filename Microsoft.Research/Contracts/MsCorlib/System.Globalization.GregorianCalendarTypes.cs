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
  //     Defines the different language versions of the Gregorian calendar.
  [Serializable]
  [ComVisible(true)]
  public enum GregorianCalendarTypes {
    // Summary:
    //     Refers to the localized version of the Gregorian calendar, based on the language
    //     of the System.Globalization.CultureInfo that uses the System.Globalization.DateTimeFormatInfo.
    Localized = 1,
    //
    // Summary:
    //     Refers to the U.S. English version of the Gregorian calendar.
    USEnglish = 2,
    //
    // Summary:
    //     Refers to the Middle East French version of the Gregorian calendar.
    MiddleEastFrench = 9,
    //
    // Summary:
    //     Refers to the Arabic version of the Gregorian calendar.
    Arabic = 10,
    //
    // Summary:
    //     Refers to the transliterated English version of the Gregorian calendar.
    TransliteratedEnglish = 11,
    //
    // Summary:
    //     Refers to the transliterated French version of the Gregorian calendar.
    TransliteratedFrench = 12,
  }
}

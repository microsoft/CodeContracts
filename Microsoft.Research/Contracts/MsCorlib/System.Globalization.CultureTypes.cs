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
using System.Runtime.InteropServices;

namespace System.Globalization
{
  // Summary:
  //     Defines the types of culture lists that can be retrieved using System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes).
  public enum CultureTypes
  {
    // Summary:
    //     Cultures that are associated with a language but are not specific to a country/region.
    //     The names of .NET Framework cultures consist of the lowercase two-letter
    //     code derived from ISO 639-1. For example: "en" (English) is a neutral culture.
    NeutralCultures = 1,
    //
    // Summary:
    //     Cultures that are specific to a country/region. The names of these cultures
    //     follow RFC 4646 (Windows Vista and later). The format is "<languagecode2>-<country/regioncode2>",
    //     where <languagecode2> is a lowercase two-letter code derived from ISO 639-1
    //     and <country/regioncode2> is an uppercase two-letter code derived from ISO
    //     3166. For example, "en-US" for English (United States) is a specific culture.
    SpecificCultures = 2,
    //
    // Summary:
    //     All cultures that are installed in the Windows operating system. Note that
    //     not all cultures supported by the .NET Framework are installed in the operating
    //     system.
    InstalledWin32Cultures = 4,
    //
    // Summary:
    //     All cultures that ship with the .NET Framework, including neutral and specific
    //     cultures, cultures installed in the Windows operating system, and custom
    //     cultures created by the user.
    AllCultures = 7,
    //
    // Summary:
    //     Custom cultures created by the user.
    UserCustomCulture = 8,
    //
    // Summary:
    //     Custom cultures created by the user that replace cultures shipped with the
    //     .NET Framework.
    ReplacementCultures = 16,
    //
    // Summary:
    //     Cultures installed in the Windows operating system but not the .NET Framework.
    WindowsOnlyCultures = 32,
    //
    // Summary:
    //     Neutral and specific cultures shipped with the .NET Framework.
    FrameworkCultures = 64,
  }
}

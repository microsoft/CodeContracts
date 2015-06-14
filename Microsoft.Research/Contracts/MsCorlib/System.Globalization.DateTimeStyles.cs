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
  //     Defines the formatting options that customize string parsing for the System.DateTime.Parse()
  //     and System.DateTime.ParseExact() methods.
  public enum DateTimeStyles {
    // Summary:
    //     Default formatting options must be used. This value represents the default
    //     style for System.DateTime.Parse(System.String), System.DateTime.ParseExact(System.String,System.String,System.IFormatProvider),
    //     and System.DateTime.TryParse(System.String,System.DateTime@).
    None = 0,
    //
    // Summary:
    //     Leading white-space characters must be ignored during parsing, except if
    //     they occur in the System.Globalization.DateTimeFormatInfo format patterns.
    AllowLeadingWhite = 1,
    //
    // Summary:
    //     Trailing white-space characters must be ignored during parsing, except if
    //     they occur in the System.Globalization.DateTimeFormatInfo format patterns.
    AllowTrailingWhite = 2,
    //
    // Summary:
    //     Extra white-space characters in the middle of the string must be ignored
    //     during parsing, except if they occur in the System.Globalization.DateTimeFormatInfo
    //     format patterns.
    AllowInnerWhite = 4,
    //
    // Summary:
    //     Extra white-space characters anywhere in the string must be ignored during
    //     parsing, except if they occur in the System.Globalization.DateTimeFormatInfo
    //     format patterns. This value is a combination of the System.Globalization.DateTimeStyles.AllowLeadingWhite,
    //     System.Globalization.DateTimeStyles.AllowTrailingWhite, and System.Globalization.DateTimeStyles.AllowInnerWhite
    //     values.
    AllowWhiteSpaces = 7,
    //
    // Summary:
    //     If the parsed string contains only the time and not the date, the parsing
    //     methods assume the Gregorian date with year = 1, month = 1, and day = 1.
    //     If this value is not used, the current date is assumed.
    NoCurrentDateDefault = 8,
    //
    // Summary:
    //     Date and time are returned as a Coordinated Universal Time (UTC). If the
    //     input string denotes a local time, through a time zone specifier or System.Globalization.DateTimeStyles.AssumeLocal,
    //     the date and time are converted from the local time to UTC. If the input
    //     string denotes a UTC time, through a time zone specifier or System.Globalization.DateTimeStyles.AssumeUniversal,
    //     no conversion occurs. If the input string does not denote a local or UTC
    //     time, no conversion occurs and the resulting System.DateTime.Kind property
    //     is System.DateTimeKind.Unspecified.
    AdjustToUniversal = 16,
    //
    // Summary:
    //     If no time zone is specified in the parsed string, the string is assumed
    //     to denote a local time.
    AssumeLocal = 32,
    //
    // Summary:
    //     If no time zone is specified in the parsed string, the string is assumed
    //     to denote a UTC.
    AssumeUniversal = 64,
    //
    // Summary:
    //     The System.DateTimeKind field of a date is preserved when a System.DateTime
    //     object is converted to a string and the string is then converted back to
    //     a System.DateTime object.
    RoundtripKind = 128,
  }
}

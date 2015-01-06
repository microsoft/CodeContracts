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
  //     Defines different rules for determining the first week of the year.
  [Serializable]
  [ComVisible(true)]
  public enum CalendarWeekRule {
    // Summary:
    //     Indicates that the first week of the year starts on the first day of the
    //     year and ends before the following designated first day of the week. The
    //     value is 0.
    FirstDay = 0,
    //
    // Summary:
    //     Indicates that the first week of the year begins on the first occurrence
    //     of the designated first day of the week on or after the first day of the
    //     year. The value is 1.
    FirstFullWeek = 1,
    //
    // Summary:
    //     Indicates that the first week of the year is the first week with four or
    //     more days before the designated first day of the week. The value is 2.
    FirstFourDayWeek = 2,
  }
}


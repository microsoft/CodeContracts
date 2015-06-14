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

// File System.Globalization.DateTimeFormatInfo.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Globalization
{
  sealed public partial class DateTimeFormatInfo : ICloneable, IFormatProvider
  {
    #region Methods and constructors
    public Object Clone()
    {
      return default(Object);
    }

    public DateTimeFormatInfo()
    {
    }

    public string GetAbbreviatedDayName(DayOfWeek dayofweek)
    {
      return default(string);
    }

    public string GetAbbreviatedEraName(int era)
    {
      return default(string);
    }

    public string GetAbbreviatedMonthName(int month)
    {
      return default(string);
    }

    public string[] GetAllDateTimePatterns(char format)
    {
      return default(string[]);
    }

    public string[] GetAllDateTimePatterns()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public string GetDayName(DayOfWeek dayofweek)
    {
      return default(string);
    }

    public int GetEra(string eraName)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public string GetEraName(int era)
    {
      return default(string);
    }

    public Object GetFormat(Type formatType)
    {
      return default(Object);
    }

    public static System.Globalization.DateTimeFormatInfo GetInstance(IFormatProvider provider)
    {
      return default(System.Globalization.DateTimeFormatInfo);
    }

    public string GetMonthName(int month)
    {
      return default(string);
    }

    public string GetShortestDayName(DayOfWeek dayOfWeek)
    {
      return default(string);
    }

    public static System.Globalization.DateTimeFormatInfo ReadOnly(System.Globalization.DateTimeFormatInfo dtfi)
    {
      Contract.Ensures(Contract.Result<System.Globalization.DateTimeFormatInfo>() != null);

      return default(System.Globalization.DateTimeFormatInfo);
    }

    public void SetAllDateTimePatterns(string[] patterns, char format)
    {
    }
    #endregion

    #region Properties and indexers
    public string[] AbbreviatedDayNames
    {
      get
      {
        return default(string[]);
      }
      set
      {
      }
    }

    public string[] AbbreviatedMonthGenitiveNames
    {
      get
      {
        return default(string[]);
      }
      set
      {
      }
    }

    public string[] AbbreviatedMonthNames
    {
      get
      {
        return default(string[]);
      }
      set
      {
      }
    }

    public string AMDesignator
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Calendar Calendar
    {
      get
      {
        return default(Calendar);
      }
      set
      {
      }
    }

    public CalendarWeekRule CalendarWeekRule
    {
      get
      {
        return default(CalendarWeekRule);
      }
      set
      {
      }
    }

    public static System.Globalization.DateTimeFormatInfo CurrentInfo
    {
      get
      {
        return default(System.Globalization.DateTimeFormatInfo);
      }
    }

    public string DateSeparator
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string[] DayNames
    {
      get
      {
        return default(string[]);
      }
      set
      {
      }
    }

    public DayOfWeek FirstDayOfWeek
    {
      get
      {
        return default(DayOfWeek);
      }
      set
      {
      }
    }

    public string FullDateTimePattern
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
      }
    }

    public static System.Globalization.DateTimeFormatInfo InvariantInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Globalization.DateTimeFormatInfo>() != null);

        return default(System.Globalization.DateTimeFormatInfo);
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    public string LongDatePattern
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string LongTimePattern
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string MonthDayPattern
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string[] MonthGenitiveNames
    {
      get
      {
        return default(string[]);
      }
      set
      {
      }
    }

    public string[] MonthNames
    {
      get
      {
        return default(string[]);
      }
      set
      {
      }
    }

    public string NativeCalendarName
    {
      get
      {
        Contract.Requires(this.Calendar != null);

        return default(string);
      }
    }

    public string PMDesignator
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string RFC1123Pattern
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        Contract.Ensures(Contract.Result<string>() == @"ddd, dd MMM yyyy HH':'mm':'ss 'GMT'");

        return default(string);
      }
    }

    public string ShortDatePattern
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string[] ShortestDayNames
    {
      get
      {
        return default(string[]);
      }
      set
      {
      }
    }

    public string ShortTimePattern
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string SortableDateTimePattern
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        Contract.Ensures(Contract.Result<string>() == @"yyyy'-'MM'-'dd'T'HH':'mm':'ss");

        return default(string);
      }
    }

    public string TimeSeparator
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string UniversalSortableDateTimePattern
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        Contract.Ensures(Contract.Result<string>() == @"yyyy'-'MM'-'dd HH':'mm':'ss'Z'");

        return default(string);
      }
    }

    public string YearMonthPattern
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
    #endregion
  }
}

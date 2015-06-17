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

namespace System.Globalization
{

  public class DateTimeFormatInfo
  {

    extern public string UniversalSortableDateTimePattern
    {
      get;
    }

    public CalendarWeekRule CalendarWeekRule
    {
      get { return default(CalendarWeekRule); }
      set
      {
        Contract.Requires((int)value >= 0);
        Contract.Requires((int)value <= 2);
      }
    }

    public string AMDesignator
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public String[] DayNames
    {
      get
      {
        Contract.Ensures(Contract.Result<string[]>() != null);
        Contract.Ensures(Contract.Result<string[]>().Length == 7);
        return default(string[]);
      }

      set
      {
        Contract.Requires(value != null);
        Contract.Requires(value.Length == 7);
      }
    }

    public string ShortTimePattern
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public string ShortDatePattern
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public DayOfWeek FirstDayOfWeek
    {
      get
      {
        Contract.Ensures((int)Contract.Result<DayOfWeek>() >= 0);
        Contract.Ensures((int)Contract.Result<DayOfWeek>() <= 6);
        return default(DayOfWeek);
      }
      set
      {
        Contract.Requires((int)value >= 0);
        Contract.Requires((int)value <= 6);
      }
    }

    extern public static DateTimeFormatInfo CurrentInfo
    {
      get;
    }

    public String[] MonthNames
    {
      get
      {
        Contract.Ensures(Contract.Result<string[]>() != null);
        Contract.Ensures(Contract.Result<string[]>().Length == 13);
        return default(string[]);
      }
      set
      {
        Contract.Requires(value != null);
        Contract.Requires(value.Length == 13);
      }
    }

    public String[] AbbreviatedMonthNames
    {
      get
      {
        Contract.Ensures(Contract.Result<string[]>() != null);
        Contract.Ensures(Contract.Result<string[]>().Length == 13);
        return default(string[]);
      }
      set
      {
        Contract.Requires(value != null);
        Contract.Requires(value.Length == 13);
      }
    }

    public string PMDesignator
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }

      set
      {
        Contract.Requires(value != null);
      }
    }

    public String[] AbbreviatedDayNames
    {
      get
      {
        Contract.Ensures(Contract.Result<string[]>() != null);
        Contract.Ensures(Contract.Result<string[]>().Length == 7);
        return default(string[]);
      }
      set
      {
        Contract.Requires(value != null);
        Contract.Requires(value.Length == 7);
      }
    }

    public string DateSeparator
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }

      set
      {
        Contract.Requires(value != null);
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
        Contract.Requires(value != null);
      }
    }

    public string TimeSeparator
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }

      set
      {
        Contract.Requires(value != null);
      }
    }

    public Calendar Calendar
    {
      get
      {
        Contract.Ensures(Contract.Result<Calendar>() != null);
        return default(string);
      }

      set
      {
        Contract.Requires(value != null);
      }
    }

    public string LongTimePattern
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }

      set
      {
        Contract.Requires(value != null);
      }
    }

    extern public static DateTimeFormatInfo InvariantInfo
    {
      get;
    }

    public string MonthDayPattern
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }

      set
      {
        Contract.Requires(value != null);
      }
    }

    public string YearMonthPattern
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }

      set
      {
        Contract.Requires(value != null);
      }
    }

    public string SortableDateTimePattern
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }

    }

    public string LongDatePattern
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }

      set
      {
        Contract.Requires(value != null);
      }
    }

    extern public string RFC1123Pattern
    {
      get;
    }

    extern public bool IsReadOnly
    {
      get;
    }

    public static DateTimeFormatInfo ReadOnly(DateTimeFormatInfo dtfi)
    {
      Contract.Requires(dtfi != null);

      return default(DateTimeFormatInfo);
    }
    public string GetMonthName(int month)
    {
      Contract.Requires(month >= 1);
      Contract.Requires(month <= 13);

      return default(string);
    }
    public string GetAbbreviatedMonthName(int month)
    {
      Contract.Requires(month >= 1);
      Contract.Requires(month <= 13);

      return default(string);
    }
    public string GetDayName(DayOfWeek dayofweek)
    {
      Contract.Requires((int)dayofweek >= 0);
      Contract.Requires((int)dayofweek <= 6);

      return default(string);
    }
    public String[] GetAllDateTimePatterns(Char format)
    {

      return default(String[]);
    }
    public String[] GetAllDateTimePatterns()
    {

      return default(String[]);
    }
    public string GetAbbreviatedDayName(DayOfWeek dayofweek)
    {
      Contract.Requires((int)dayofweek >= 0);
      Contract.Requires((int)dayofweek <= 6);

      return default(string);
    }
    public string GetAbbreviatedEraName(int era)
    {
      Contract.Requires(era >= 0);

      return default(string);
    }
    public string GetEraName(int era)
    {
      Contract.Requires(era >= 0);

      return default(string);
    }
    public int GetEra(string eraName)
    {
      Contract.Requires(eraName != null);

      return default(int);
    }
    public object Clone()
    {

      return default(object);
    }
    public object GetFormat(Type formatType)
    {

      return default(object);
    }
    public static DateTimeFormatInfo GetInstance(IFormatProvider provider)
    {

      return default(DateTimeFormatInfo);
    }
    public DateTimeFormatInfo()
    {
      return default(DateTimeFormatInfo);
    }
  }
}

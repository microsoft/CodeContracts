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

        public string UniversalSortableDateTimePattern
        {
          get;
        }

        public CalendarWeekRule CalendarWeekRule
        {
          get;
          set
            CodeContract.Requires((int)value >= 0);
            CodeContract.Requires((int)value <= 2);
        }

        public string! AMDesignator
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public String[]! DayNames
        {
          get;
          set
            CodeContract.Requires(value != null);
            CodeContract.Requires(value.Length == 7);
        }

        public string! ShortTimePattern
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public string! ShortDatePattern
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public DayOfWeek FirstDayOfWeek
        {
          get;
          set
            CodeContract.Requires((int)value >= 0);
            CodeContract.Requires((int)value <= 6);
        }

        public static DateTimeFormatInfo CurrentInfo
        {
          get;
        }

        public String[]! MonthNames
        {
          get;
          set
            CodeContract.Requires(value != null);
            CodeContract.Requires(value.Length == 13);
        }

        public String[]! AbbreviatedMonthNames
        {
          get;
          set
            CodeContract.Requires(value != null);
            CodeContract.Requires(value.Length == 13);
        }

        public string! PMDesignator
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public String[]! AbbreviatedDayNames
        {
          get;
          set
            CodeContract.Requires(value != null);
            CodeContract.Requires(value.Length == 7);
        }

        public string! DateSeparator
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public string! FullDateTimePattern
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public string! TimeSeparator
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public Calendar! Calendar
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public string! LongTimePattern
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public static DateTimeFormatInfo InvariantInfo
        {
          get;
        }

        public string! MonthDayPattern
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public string! YearMonthPattern
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public string SortableDateTimePattern
        {
          get;
        }

        public string! LongDatePattern
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public string RFC1123Pattern
        {
          get;
        }

        public bool IsReadOnly
        {
          get;
        }

        public static DateTimeFormatInfo ReadOnly (DateTimeFormatInfo! dtfi) {
            CodeContract.Requires(dtfi != null);

          return default(DateTimeFormatInfo);
        }
        public string GetMonthName (int month) {
            CodeContract.Requires(month >= 1);
            CodeContract.Requires(month <= 13);

          return default(string);
        }
        public string GetAbbreviatedMonthName (int month) {
            CodeContract.Requires(month >= 1);
            CodeContract.Requires(month <= 13);

          return default(string);
        }
        public string GetDayName (DayOfWeek dayofweek) {
            CodeContract.Requires((int)dayofweek >= 0);
            CodeContract.Requires((int)dayofweek <= 6);

          return default(string);
        }
        public String[] GetAllDateTimePatterns (Char format) {

          return default(String[]);
        }
        public String[] GetAllDateTimePatterns () {

          return default(String[]);
        }
        public string GetAbbreviatedDayName (DayOfWeek dayofweek) {
            CodeContract.Requires((int)dayofweek >= 0);
            CodeContract.Requires((int)dayofweek <= 6);

          return default(string);
        }
        public string GetAbbreviatedEraName (int era) {
            CodeContract.Requires(era >= 0);

          return default(string);
        }
        public string GetEraName (int era) {
            CodeContract.Requires(era >= 0);

          return default(string);
        }
        public int GetEra (string! eraName) {
            CodeContract.Requires(eraName != null);

          return default(int);
        }
        public object Clone () {

          return default(object);
        }
        public object GetFormat (Type formatType) {

          return default(object);
        }
        public static DateTimeFormatInfo GetInstance (IFormatProvider provider) {

          return default(DateTimeFormatInfo);
        }
        public DateTimeFormatInfo () {
          return default(DateTimeFormatInfo);
        }
    }
}

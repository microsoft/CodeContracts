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

// File System.Globalization.TaiwanCalendar.cs
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
  public partial class TaiwanCalendar : Calendar
  {
    #region Methods and constructors
    public override DateTime AddMonths(DateTime time, int months)
    {
      return default(DateTime);
    }

    public override DateTime AddYears(DateTime time, int years)
    {
      return default(DateTime);
    }

    public override int GetDayOfMonth(DateTime time)
    {
      return default(int);
    }

    public override DayOfWeek GetDayOfWeek(DateTime time)
    {
      return default(DayOfWeek);
    }

    public override int GetDayOfYear(DateTime time)
    {
      return default(int);
    }

    public override int GetDaysInMonth(int year, int month, int era)
    {
      return default(int);
    }

    public override int GetDaysInYear(int year, int era)
    {
      return default(int);
    }

    public override int GetEra(DateTime time)
    {
      return default(int);
    }

    public override int GetLeapMonth(int year, int era)
    {
      return default(int);
    }

    public override int GetMonth(DateTime time)
    {
      return default(int);
    }

    public override int GetMonthsInYear(int year, int era)
    {
      return default(int);
    }

    public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
      return default(int);
    }

    public override int GetYear(DateTime time)
    {
      return default(int);
    }

    public override bool IsLeapDay(int year, int month, int day, int era)
    {
      return default(bool);
    }

    public override bool IsLeapMonth(int year, int month, int era)
    {
      return default(bool);
    }

    public override bool IsLeapYear(int year, int era)
    {
      return default(bool);
    }

    public TaiwanCalendar()
    {
    }

    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
      return default(DateTime);
    }

    public override int ToFourDigitYear(int year)
    {
      return default(int);
    }
    #endregion

    #region Properties and indexers
    public override CalendarAlgorithmType AlgorithmType
    {
      get
      {
        return default(CalendarAlgorithmType);
      }
    }

    public override int[] Eras
    {
      get
      {
        return default(int[]);
      }
    }

    public override DateTime MaxSupportedDateTime
    {
      get
      {
        return default(DateTime);
      }
    }

    public override DateTime MinSupportedDateTime
    {
      get
      {
        return default(DateTime);
      }
    }

    public override int TwoDigitYearMax
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }
    #endregion
  }
}

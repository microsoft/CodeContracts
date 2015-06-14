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

// File System.Globalization.Calendar.cs
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
  abstract public partial class Calendar : ICloneable
  {
    #region Methods and constructors
    public virtual new DateTime AddDays(DateTime time, int days)
    {
      return default(DateTime);
    }

    public virtual new DateTime AddHours(DateTime time, int hours)
    {
      return default(DateTime);
    }

    public virtual new DateTime AddMilliseconds(DateTime time, double milliseconds)
    {
      return default(DateTime);
    }

    public virtual new DateTime AddMinutes(DateTime time, int minutes)
    {
      return default(DateTime);
    }

    public abstract DateTime AddMonths(DateTime time, int months);

    public virtual new DateTime AddSeconds(DateTime time, int seconds)
    {
      return default(DateTime);
    }

    public virtual new DateTime AddWeeks(DateTime time, int weeks)
    {
      return default(DateTime);
    }

    public abstract DateTime AddYears(DateTime time, int years);

    protected Calendar()
    {
    }

    public virtual new Object Clone()
    {
      return default(Object);
    }

    public abstract int GetDayOfMonth(DateTime time);

    public abstract DayOfWeek GetDayOfWeek(DateTime time);

    public abstract int GetDayOfYear(DateTime time);

    public abstract int GetDaysInMonth(int year, int month, int era);

    public virtual new int GetDaysInMonth(int year, int month)
    {
      return default(int);
    }

    public virtual new int GetDaysInYear(int year)
    {
      return default(int);
    }

    public abstract int GetDaysInYear(int year, int era);

    public abstract int GetEra(DateTime time);

    public virtual new int GetHour(DateTime time)
    {
      return default(int);
    }

    public virtual new int GetLeapMonth(int year)
    {
      return default(int);
    }

    public virtual new int GetLeapMonth(int year, int era)
    {
      return default(int);
    }

    public virtual new double GetMilliseconds(DateTime time)
    {
      return default(double);
    }

    public virtual new int GetMinute(DateTime time)
    {
      return default(int);
    }

    public abstract int GetMonth(DateTime time);

    public abstract int GetMonthsInYear(int year, int era);

    public virtual new int GetMonthsInYear(int year)
    {
      return default(int);
    }

    public virtual new int GetSecond(DateTime time)
    {
      return default(int);
    }

    public virtual new int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
      return default(int);
    }

    public abstract int GetYear(DateTime time);

    public virtual new bool IsLeapDay(int year, int month, int day)
    {
      return default(bool);
    }

    public abstract bool IsLeapDay(int year, int month, int day, int era);

    public virtual new bool IsLeapMonth(int year, int month)
    {
      return default(bool);
    }

    public abstract bool IsLeapMonth(int year, int month, int era);

    public abstract bool IsLeapYear(int year, int era);

    public virtual new bool IsLeapYear(int year)
    {
      return default(bool);
    }

    public static System.Globalization.Calendar ReadOnly(System.Globalization.Calendar calendar)
    {
      Contract.Ensures(Contract.Result<System.Globalization.Calendar>() != null);

      return default(System.Globalization.Calendar);
    }

    public abstract DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era);

    public virtual new DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
    {
      return default(DateTime);
    }

    public virtual new int ToFourDigitYear(int year)
    {
      return default(int);
    }
    #endregion

    #region Properties and indexers
    public virtual new CalendarAlgorithmType AlgorithmType
    {
      get
      {
        return default(CalendarAlgorithmType);
      }
    }

    public abstract int[] Eras
    {
      get;
    }

    public bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new DateTime MaxSupportedDateTime
    {
      get
      {
        return default(DateTime);
      }
    }

    public virtual new DateTime MinSupportedDateTime
    {
      get
      {
        return default(DateTime);
      }
    }

    public virtual new int TwoDigitYearMax
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

    #region Fields
    public static int CurrentEra;
    #endregion
  }
}

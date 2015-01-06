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

  public class Calendar
  {
    protected Calendar() { }

    public virtual Int32[] Eras
    {
      get
      {
        Contract.Ensures(Contract.Result<Int32[]>() != null);
        return default(Int32[]);
      }
    }

#if false
    public int TwoDigitYearMax
    {
      get;
      set;
    }

#endif
    [Pure]
    public virtual int ToFourDigitYear(int year)
    {
      Contract.Requires(year >= 0);

      return default(int);
    }
#if false
    public DateTime ToDateTime(int arg0, int arg1, int arg2, int arg3, int arg4, int arg5, int arg6, int arg7)
    {

      return default(DateTime);
    }
    public DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
    {

      return default(DateTime);
    }
#endif
    [Pure]
    public virtual bool IsLeapYear(int arg0, int arg1)
    {

      return default(bool);
    }
    [Pure]
    public virtual bool IsLeapYear(int year)
    {

      return default(bool);
    }
    [Pure]
    public virtual bool IsLeapMonth(int arg0, int arg1, int arg2)
    {

      return default(bool);
    }
    [Pure]
    public virtual bool IsLeapMonth(int year, int month)
    {

      return default(bool);
    }
    [Pure]
    public virtual bool IsLeapDay(int arg0, int arg1, int arg2, int arg3)
    {

      return default(bool);
    }
    [Pure]
    public virtual bool IsLeapDay(int year, int month, int day)
    {

      return default(bool);
    }
    [Pure]
    public virtual int GetYear(DateTime arg0)
    {

      return default(int);
    }
#if false
    [Pure]
    public int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
      Contract.Requires((int)firstDayOfWeek >= 0);
      Contract.Requires((int)firstDayOfWeek <= 6);

      return default(int);
    }
    [Pure]
    public int GetSecond(DateTime time)
    {

      return default(int);
    }
#endif
    [Pure]
    public virtual int GetMonthsInYear(int arg0, int arg1)
    {

      return default(int);
    }
    [Pure]
    public virtual int GetMonthsInYear(int year)
    {

      return default(int);
    }
    [Pure]
    public virtual int GetMonth(DateTime arg0)
    {

      return default(int);
    }
    [Pure]
    public virtual int GetMinute(DateTime time)
    {

      return default(int);
    }
    [Pure]
    public virtual double GetMilliseconds(DateTime time)
    {

      return default(double);
    }
    [Pure]
    public virtual int GetHour(DateTime time)
    {

      return default(int);
    }
    [Pure]
    public virtual int GetEra(DateTime arg0)
    {

      return default(int);
    }
    [Pure]
    public virtual int GetDaysInYear(int arg0, int arg1)
    {

      return default(int);
    }
    [Pure]
    public virtual int GetDaysInYear(int year)
    {

      return default(int);
    }
    [Pure]
    public virtual int GetDaysInMonth(int arg0, int arg1, int arg2)
    {

      return default(int);
    }
    [Pure]
    public virtual int GetDaysInMonth(int year, int month)
    {

      return default(int);
    }
    [Pure]
    public virtual int GetDayOfYear(DateTime arg0)
    {

      return default(int);
    }
    [Pure]
    public virtual DayOfWeek GetDayOfWeek(DateTime arg0)
    {

      return default(DayOfWeek);
    }
    [Pure]
    public virtual int GetDayOfMonth(DateTime arg0)
    {

      return default(int);
    }
#if false
    public DateTime AddYears(DateTime arg0, int arg1)
    {

      return default(DateTime);
    }
    public DateTime AddWeeks(DateTime time, int weeks)
    {

      return default(DateTime);
    }
    public DateTime AddSeconds(DateTime time, int seconds)
    {

      return default(DateTime);
    }
    public DateTime AddMonths(DateTime arg0, int arg1)
    {

      return default(DateTime);
    }
    public DateTime AddMinutes(DateTime time, int minutes)
    {

      return default(DateTime);
    }
    public DateTime AddMilliseconds(DateTime time, double milliseconds)
    {

      return default(DateTime);
    }
    public DateTime AddHours(DateTime time, int hours)
    {

      return default(DateTime);
    }
    public DateTime AddDays(DateTime time, int days)
    {
      return default(DateTime);
    }
#endif
  }
}
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

namespace System
{

  public enum DateTimeKind
  {
    Unspecified,
    Utc,
    Local
  }


  public struct DateTime
  {
    static public readonly DateTime MaxValue = new DateTime(3155378975999999999L, DateTimeKind.Unspecified);
    static public readonly DateTime MinValue = new DateTime(0L, DateTimeKind.Unspecified);

    public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, System.Globalization.Calendar calendar)
    {
      Contract.Requires(year >= 1);
      Contract.Requires(year <= 0x270f);
      Contract.Requires(month >= 1);
      Contract.Requires(month <= 12);
      Contract.Requires(day >= 1);
      Contract.Requires(hour >= 0);
      Contract.Requires(hour <= 24);
      Contract.Requires(minute >= 0);
      Contract.Requires(minute <= 60);
      Contract.Requires(second >= 0);
      Contract.Requires(second <= 60);
      Contract.Requires(millisecond >= 0);
      Contract.Requires(millisecond < 1000);

      Contract.Requires(calendar != null);
    }

    public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
    {
      Contract.Requires(year >= 1);
      Contract.Requires(year <= 0x270f);
      Contract.Requires(month >= 1);
      Contract.Requires(month <= 12);
      Contract.Requires(day >= 1);
      Contract.Requires(hour >= 0);
      Contract.Requires(hour <= 24);
      Contract.Requires(minute >= 0);
      Contract.Requires(minute <= 60);
      Contract.Requires(second >= 0);
      Contract.Requires(second <= 60);
      Contract.Requires(millisecond >= 0);
      Contract.Requires(millisecond < 1000);
    }

    public DateTime(int year, int month, int day, int hour, int minute, int second, System.Globalization.Calendar calendar)
    {
      Contract.Requires(year >= 1);
      Contract.Requires(year <= 0x270f);
      Contract.Requires(month >= 1);
      Contract.Requires(month <= 12);
      Contract.Requires(day >= 1);
      Contract.Requires(hour >= 0);
      Contract.Requires(hour <= 24);
      Contract.Requires(minute >= 0);
      Contract.Requires(minute <= 60);
      Contract.Requires(second >= 0);
      Contract.Requires(second <= 60);

      Contract.Requires(calendar != null);
    }

    public DateTime(int year, int month, int day, int hour, int minute, int second)
    {
      Contract.Requires(year >= 1);
      Contract.Requires(year <= 0x270f);
      Contract.Requires(month >= 1);
      Contract.Requires(month <= 12);
      Contract.Requires(day >= 1);
    }

    public DateTime(int year, int month, int day, System.Globalization.Calendar calendar)
    {
      Contract.Requires(year >= 1);
      Contract.Requires(year <= 0x270f);
      Contract.Requires(month >= 1);
      Contract.Requires(month <= 12);
      Contract.Requires(day >= 1);

      Contract.Requires(calendar != null);
    }

    public DateTime(int year, int month, int day)
    {
      Contract.Requires(year >= 1);
      Contract.Requires(year <= 0x270f);
      Contract.Requires(month >= 1);
      Contract.Requires(month <= 12);
      Contract.Requires(day >= 1);
    }

    public DateTime(Int64 ticks)
    {
      Contract.Requires(ticks >= 0);
      Contract.Requires(ticks <= 0x2bca2875f4373fffL);
    }

    public DateTime(long ticks, DateTimeKind kind)
    {
      Contract.Requires(ticks >= 0);
      Contract.Requires(ticks <= 0x2bca2875f4373fffL);

      Contract.Requires(kind >= DateTimeKind.Unspecified);
      Contract.Requires(kind <= DateTimeKind.Local);
    }

    public static DateTime Now
    {
      get
      {
        Contract.Ensures(Contract.Result<DateTime>().Kind == DateTimeKind.Local);
      
        return default(DateTime);
      }
    }

    public DayOfWeek DayOfWeek
    {
      get 
      {
        return default(DayOfWeek);
      }
    }

    public int Second
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() < 60);
     
        return default(int);
      }
    }


    public static DateTime UtcNow
    {
      get
      {
        Contract.Ensures(Contract.Result<DateTime>().Kind == DateTimeKind.Utc);
        
        return default(DateTime);
      }
    }

    public DateTime Date
    {
      get
      {
        return default(DateTime);
      }
    }

    public int Hour
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() < 24);
        
        return default(int);
      }
    }

    public static DateTime Today
    {
      get
      {
        Contract.Ensures(Contract.Result<DateTime>().Kind == DateTimeKind.Local);

        return default(DateTime);
      }
    }

    public int Day
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() > 0);
        Contract.Ensures(Contract.Result<int>() <= 31);

        return default(int);
      }
    }

#if false
    public int Millisecond
    {
      get;
    }
#endif

    public int DayOfYear
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() > 0);
        Contract.Ensures(Contract.Result<int>() <= 366);
        
        return default(int);
      }
    }

    public DateTimeKind Kind
    {
      get
      {
        return default(DateTimeKind);
      }
    }

    public int Year
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() > 0);
        Contract.Ensures(Contract.Result<int>() <= 9999);

        return default(int);
      }
    }

    public int Minute
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() < 60);
        
        return default(int);
      }
    }

    public int Month
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() > 0);
        Contract.Ensures(Contract.Result<int>() <= 12);
        
        return default(int);
      }
    }

    public Int64 Ticks
    {
      get
      {
        Contract.Ensures(Contract.Result<Int64>() >= 0);
        

        return default(Int64);
      }
    }

#if false
    public TimeSpan TimeOfDay
    {
      get;
    }

    public TypeCode GetTypeCode()
    {

      return default(TypeCode);
    }
#endif

#if !SILVERLIGHT
    [Pure]
    public String[] GetDateTimeFormats(Char format, IFormatProvider provider)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(String[]);
    }

    [Pure]
    public String[] GetDateTimeFormats(Char format)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(String[]);
    }

    [Pure]
    public String[] GetDateTimeFormats(IFormatProvider provider)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(String[]);
    }

    [Pure]
    public String[] GetDateTimeFormats()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(String[]);
    }
#endif

#if false // operators are pure by default
    public static bool operator >=(DateTime t1, DateTime t2)
    {

      return default(bool);
    }
    public static bool operator >(DateTime t1, DateTime t2)
    {

      return default(bool);
    }
    public static bool operator <=(DateTime t1, DateTime t2)
    {

      return default(bool);
    }
    public static bool operator <(DateTime t1, DateTime t2)
    {

      return default(bool);
    }
    public static bool operator !=(DateTime d1, DateTime d2)
    {

      return default(bool);
    }
    public static bool operator ==(DateTime d1, DateTime d2)
    {

      return default(bool);
    }
    public static TimeSpan operator -(DateTime d1, DateTime d2)
    {

      return default(TimeSpan);
    }
    public static DateTime operator -(DateTime d, TimeSpan t)
    {

      return default(DateTime);
    }
    public static DateTime operator +(DateTime d, TimeSpan t)
    {

      return default(DateTime);
    }
#endif

    [Pure]
    public DateTime ToUniversalTime()
    {
      Contract.Ensures(Contract.Result<DateTime>().Kind == DateTimeKind.Utc);

      return default(DateTime);
    }

    [Pure]
    public string ToString(string format)
    {
      Contract.Ensures(Contract.Result<string>() != null);
     
      return default(string);
    }

    [Pure]
    public string ToShortTimeString()
    {
      Contract.Ensures(Contract.Result<string>() != null);
      
      return default(string);
    }

    [Pure]
    public string ToShortDateString()
    {
      Contract.Ensures(Contract.Result<string>() != null);
      
      return default(string);
    }

    [Pure]
    public string ToLongTimeString()
    {
      Contract.Ensures(Contract.Result<string>() != null);
      
      return default(string);
    }

    [Pure]
    public string ToLongDateString()
    {
      Contract.Ensures(Contract.Result<string>() != null);
      
      return default(string);
    }

    [Pure]
    public DateTime ToLocalTime()
    {
      return default(DateTime);
    }
    
    [Pure]
    public Int64 ToFileTimeUtc()
    {

      return default(Int64);
    }
    
    [Pure]
    public Int64 ToFileTime()
    {

      return default(Int64);
    }
    
    [Pure]
    public double ToOADate()
    {

      return default(double);
    }
    
    [Pure]
    public DateTime Subtract(TimeSpan value)
    {
      return default(DateTime);
    }
    
    [Pure]
    public TimeSpan Subtract(DateTime value)
    {
      return default(TimeSpan);
    }
    
    [Pure]
    public static DateTime ParseExact(string s, String[] formats, IFormatProvider provider, System.Globalization.DateTimeStyles style)
    {
      return default(DateTime);
    }

    [Pure]
    public static DateTime ParseExact(string s, string format, IFormatProvider provider, System.Globalization.DateTimeStyles style)
    {
      return default(DateTime);
    }
    
    [Pure]
    public static DateTime ParseExact(string s, string format, IFormatProvider provider)
    {
      return default(DateTime);
    }

    [Pure]
    public static DateTime Parse(string s, IFormatProvider provider, System.Globalization.DateTimeStyles styles)
    {
      return default(DateTime);
    }
    
    [Pure]
    public static DateTime Parse(string s, IFormatProvider provider)
    {
      return default(DateTime);
    }

    [Pure]
    public static DateTime Parse(string s)
    {
      return default(DateTime);
    }

    [Pure]
    public static bool IsLeapYear(int year)
    {
      Contract.Requires(year >= 1);
      Contract.Requires(year <= 9999);

      return default(bool);
    }

    [Pure]
    public static DateTime FromOADate(double d)
    {
      Contract.Requires(d > -657435.0);
      Contract.Requires(d < 2958466.0);
      
      return default(DateTime);
    }

    [Pure]
    public static DateTime FromFileTimeUtc(Int64 fileTime)
    {
      Contract.Requires(fileTime >= 0);
      Contract.Requires(fileTime <=  0x24c85a5ed1c03fffL);

      return default(DateTime);
    }

    [Pure]
    public static DateTime FromFileTime(Int64 fileTime)
    {
      Contract.Requires(fileTime >= 0);
      Contract.Requires(fileTime <= 0x24c85a5ed1c03fffL);

      return default(DateTime);
    }

    [Pure]
    public static bool Equals(DateTime t1, DateTime t2)
    {
      return default(bool);
    }

    [Pure]
    public static int DaysInMonth(int year, int month)
    {
      Contract.Requires(month >= 1);
      Contract.Requires(month <= 12);
     
      Contract.Ensures(Contract.Result<int>() > 0);
      Contract.Ensures(Contract.Result<int>() <= 31);

      return default(int);
    }

    [Pure]
    public static int Compare(DateTime t1, DateTime t2)
    {
      return default(int);
    }

    [Pure]
    public DateTime AddYears(int value)
    {
      Contract.Requires(value >= -10000);
      Contract.Requires(value <= 0x2710);

      return default(DateTime);
    }
    
    [Pure]
    public DateTime AddTicks(Int64 value)
    {
      return default(DateTime);
    }

    [Pure]
    public DateTime AddSeconds(double value)
    {
      return default(DateTime);
    }

    [Pure]
    public DateTime AddMonths(int months)
    {
      Contract.Requires(months >= -120000);
      Contract.Requires(months <= 0x1d4c0);

      return default(DateTime);
    }
    
    [Pure]
    public DateTime AddMinutes(double value)
    {
      return default(DateTime);
    }

    [Pure]
    public DateTime AddMilliseconds(double value)
    {
      return default(DateTime);
    }

    [Pure]
    public DateTime AddHours(double value)
    {
      return default(DateTime);
    }
    
    [Pure]
    public DateTime AddDays(double value)
    {
      return default(DateTime);
    }
    
    [Pure]
    public DateTime Add(TimeSpan value)
    {
      return default(DateTime);
    }
       
  }
}
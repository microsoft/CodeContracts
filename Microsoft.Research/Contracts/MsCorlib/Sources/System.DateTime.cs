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

// File System.DateTime.cs
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


namespace System
{
  public partial struct DateTime : IComparable, IFormattable, IConvertible, System.Runtime.Serialization.ISerializable, IComparable<DateTime>, IEquatable<DateTime>
  {
    #region Methods and constructors
    public static TimeSpan operator - (System.DateTime d1, System.DateTime d2)
    {
      return default(TimeSpan);
    }

    public static System.DateTime operator - (System.DateTime d, TimeSpan t)
    {
      return default(System.DateTime);
    }

    public static bool operator != (System.DateTime d1, System.DateTime d2)
    {
      return default(bool);
    }

    public static System.DateTime operator + (System.DateTime d, TimeSpan t)
    {
      return default(System.DateTime);
    }

    public static bool operator < (System.DateTime t1, System.DateTime t2)
    {
      return default(bool);
    }

    public static bool operator <=(System.DateTime t1, System.DateTime t2)
    {
      return default(bool);
    }

    public static bool operator == (System.DateTime d1, System.DateTime d2)
    {
      return default(bool);
    }

    public static bool operator > (System.DateTime t1, System.DateTime t2)
    {
      return default(bool);
    }

    public static bool operator >= (System.DateTime t1, System.DateTime t2)
    {
      return default(bool);
    }

    public System.DateTime Add(TimeSpan value)
    {
      return default(System.DateTime);
    }

    public System.DateTime AddDays(double value)
    {
      return default(System.DateTime);
    }

    public System.DateTime AddHours(double value)
    {
      return default(System.DateTime);
    }

    public System.DateTime AddMilliseconds(double value)
    {
      return default(System.DateTime);
    }

    public System.DateTime AddMinutes(double value)
    {
      return default(System.DateTime);
    }

    public System.DateTime AddMonths(int months)
    {
      return default(System.DateTime);
    }

    public System.DateTime AddSeconds(double value)
    {
      return default(System.DateTime);
    }

    public System.DateTime AddTicks(long value)
    {
      return default(System.DateTime);
    }

    public System.DateTime AddYears(int value)
    {
      return default(System.DateTime);
    }

    public static int Compare(System.DateTime t1, System.DateTime t2)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 1);

      return default(int);
    }

    public int CompareTo(Object value)
    {
      return default(int);
    }

    public int CompareTo(System.DateTime value)
    {
      return default(int);
    }

    public DateTime(long ticks)
    {
    }

    public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, System.Globalization.Calendar calendar)
    {
    }

    public DateTime(int year, int month, int day, int hour, int minute, int second)
    {
    }

    public DateTime(int year, int month, int day, int hour, int minute, int second, System.Globalization.Calendar calendar)
    {
    }

    public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, DateTimeKind kind)
    {
    }

    public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
    {
    }

    public DateTime(long ticks, DateTimeKind kind)
    {
    }

    public DateTime(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
    {
    }

    public DateTime(int year, int month, int day)
    {
    }

    public DateTime(int year, int month, int day, System.Globalization.Calendar calendar)
    {
    }

    public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, System.Globalization.Calendar calendar, DateTimeKind kind)
    {
    }

    public static int DaysInMonth(int year, int month)
    {
      return default(int);
    }

    public override bool Equals(Object value)
    {
      return default(bool);
    }

    public bool Equals(System.DateTime value)
    {
      return default(bool);
    }

    public static bool Equals(System.DateTime t1, System.DateTime t2)
    {
      return default(bool);
    }

    public static System.DateTime FromBinary(long dateData)
    {
      return default(System.DateTime);
    }

    public static System.DateTime FromFileTime(long fileTime)
    {
      return default(System.DateTime);
    }

    public static System.DateTime FromFileTimeUtc(long fileTime)
    {
      return default(System.DateTime);
    }

    public static System.DateTime FromOADate(double d)
    {
      return default(System.DateTime);
    }

    public string[] GetDateTimeFormats(IFormatProvider provider)
    {
      return default(string[]);
    }

    public string[] GetDateTimeFormats(char format, IFormatProvider provider)
    {
      return default(string[]);
    }

    public string[] GetDateTimeFormats(char format)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(string[]);
    }

    public string[] GetDateTimeFormats()
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(string[]);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public TypeCode GetTypeCode()
    {
      return default(TypeCode);
    }

    public bool IsDaylightSavingTime()
    {
      return default(bool);
    }

    public static bool IsLeapYear(int year)
    {
      return default(bool);
    }

    public static System.DateTime Parse(string s)
    {
      return default(System.DateTime);
    }

    public static System.DateTime Parse(string s, IFormatProvider provider, System.Globalization.DateTimeStyles styles)
    {
      return default(System.DateTime);
    }

    public static System.DateTime Parse(string s, IFormatProvider provider)
    {
      return default(System.DateTime);
    }

    public static System.DateTime ParseExact(string s, string format, IFormatProvider provider)
    {
      return default(System.DateTime);
    }

    public static System.DateTime ParseExact(string s, string format, IFormatProvider provider, System.Globalization.DateTimeStyles style)
    {
      return default(System.DateTime);
    }

    public static System.DateTime ParseExact(string s, string[] formats, IFormatProvider provider, System.Globalization.DateTimeStyles style)
    {
      return default(System.DateTime);
    }

    public static System.DateTime SpecifyKind(System.DateTime value, DateTimeKind kind)
    {
      return default(System.DateTime);
    }

    public TimeSpan Subtract(System.DateTime value)
    {
      return default(TimeSpan);
    }

    public System.DateTime Subtract(TimeSpan value)
    {
      return default(System.DateTime);
    }

    bool System.IConvertible.ToBoolean(IFormatProvider provider)
    {
      return default(bool);
    }

    byte System.IConvertible.ToByte(IFormatProvider provider)
    {
      return default(byte);
    }

    char System.IConvertible.ToChar(IFormatProvider provider)
    {
      return default(char);
    }

    System.DateTime System.IConvertible.ToDateTime(IFormatProvider provider)
    {
      return default(System.DateTime);
    }

    Decimal System.IConvertible.ToDecimal(IFormatProvider provider)
    {
      return default(Decimal);
    }

    double System.IConvertible.ToDouble(IFormatProvider provider)
    {
      return default(double);
    }

    short System.IConvertible.ToInt16(IFormatProvider provider)
    {
      return default(short);
    }

    int System.IConvertible.ToInt32(IFormatProvider provider)
    {
      return default(int);
    }

    long System.IConvertible.ToInt64(IFormatProvider provider)
    {
      return default(long);
    }

    sbyte System.IConvertible.ToSByte(IFormatProvider provider)
    {
      return default(sbyte);
    }

    float System.IConvertible.ToSingle(IFormatProvider provider)
    {
      return default(float);
    }

    Object System.IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return default(Object);
    }

    ushort System.IConvertible.ToUInt16(IFormatProvider provider)
    {
      return default(ushort);
    }

    uint System.IConvertible.ToUInt32(IFormatProvider provider)
    {
      return default(uint);
    }

    ulong System.IConvertible.ToUInt64(IFormatProvider provider)
    {
      return default(ulong);
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public long ToBinary()
    {
      return default(long);
    }

    public long ToFileTime()
    {
      Contract.Ensures(0 <= Contract.Result<long>());
      Contract.Ensures(Contract.Result<long>() <= 8718460804854775808);

      return default(long);
    }

    public long ToFileTimeUtc()
    {
      Contract.Ensures(0 <= Contract.Result<long>());
      Contract.Ensures(Contract.Result<long>() <= 8718460804854775808);

      return default(long);
    }

    public System.DateTime ToLocalTime()
    {
      return default(System.DateTime);
    }

    public string ToLongDateString()
    {
      return default(string);
    }

    public string ToLongTimeString()
    {
      return default(string);
    }

    public double ToOADate()
    {
      Contract.Ensures(((-28401321599999 / 43200000)) <= Contract.Result<double>());
      Contract.Ensures(-(Contract.Result<double>()) <= ((28401321599999 / 43200000)));

      return default(double);
    }

    public string ToShortDateString()
    {
      return default(string);
    }

    public string ToShortTimeString()
    {
      return default(string);
    }

    public string ToString(string format)
    {
      return default(string);
    }

    public string ToString(string format, IFormatProvider provider)
    {
      return default(string);
    }

    public override string ToString()
    {
      return default(string);
    }

    public string ToString(IFormatProvider provider)
    {
      return default(string);
    }

    public System.DateTime ToUniversalTime()
    {
      return default(System.DateTime);
    }

    public static bool TryParse(string s, out System.DateTime result)
    {
      result = default(System.DateTime);

      return default(bool);
    }

    public static bool TryParse(string s, IFormatProvider provider, System.Globalization.DateTimeStyles styles, out System.DateTime result)
    {
      result = default(System.DateTime);

      return default(bool);
    }

    public static bool TryParseExact(string s, string[] formats, IFormatProvider provider, System.Globalization.DateTimeStyles style, out System.DateTime result)
    {
      result = default(System.DateTime);

      return default(bool);
    }

    public static bool TryParseExact(string s, string format, IFormatProvider provider, System.Globalization.DateTimeStyles style, out System.DateTime result)
    {
      result = default(System.DateTime);

      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public System.DateTime Date
    {
      get
      {
        return default(System.DateTime);
      }
    }

    public int Day
    {
      get
      {
        return default(int);
      }
    }

    public DayOfWeek DayOfWeek
    {
      get
      {
        Contract.Ensures(Contract.Result<System.DayOfWeek>() <= ((System.DayOfWeek)(6)));

        return default(DayOfWeek);
      }
    }

    public int DayOfYear
    {
      get
      {
        return default(int);
      }
    }

    public int Hour
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 23);

        return default(int);
      }
    }

    public DateTimeKind Kind
    {
      get
      {
        Contract.Ensures(((System.DateTimeKind)(0)) <= Contract.Result<System.DateTimeKind>());
        Contract.Ensures(Contract.Result<System.DateTimeKind>() <= ((System.DateTimeKind)(2)));

        return default(DateTimeKind);
      }
    }

    public int Millisecond
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 999);

        return default(int);
      }
    }

    public int Minute
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 59);

        return default(int);
      }
    }

    public int Month
    {
      get
      {
        return default(int);
      }
    }

    public static System.DateTime Now
    {
      get
      {
        Contract.Ensures(0 <= System.DateTime.UtcNow.Ticks);

        return default(System.DateTime);
      }
    }

    public int Second
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 59);

        return default(int);
      }
    }

    public long Ticks
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<long>());

        return default(long);
      }
    }

    public TimeSpan TimeOfDay
    {
      get
      {
        return default(TimeSpan);
      }
    }

    public static System.DateTime Today
    {
      get
      {
        return default(System.DateTime);
      }
    }

    public static System.DateTime UtcNow
    {
      get
      {
        return default(System.DateTime);
      }
    }

    public int Year
    {
      get
      {
        return default(int);
      }
    }
    #endregion

    #region Fields
    public readonly static System.DateTime MaxValue;
    public readonly static System.DateTime MinValue;
    #endregion
  }
}

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

// File System.DateTimeOffset.cs
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
  public partial struct DateTimeOffset : IComparable, IFormattable, System.Runtime.Serialization.ISerializable, System.Runtime.Serialization.IDeserializationCallback, IComparable<DateTimeOffset>, IEquatable<DateTimeOffset>
  {
    #region Methods and constructors
    public static System.DateTimeOffset operator - (System.DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public static TimeSpan operator - (System.DateTimeOffset left, System.DateTimeOffset right)
    {
      return default(TimeSpan);
    }

    public static bool operator != (System.DateTimeOffset left, System.DateTimeOffset right)
    {
      return default(bool);
    }

    public static System.DateTimeOffset operator + (System.DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public static bool operator < (System.DateTimeOffset left, System.DateTimeOffset right)
    {
      return default(bool);
    }

    public static bool operator <=(System.DateTimeOffset left, System.DateTimeOffset right)
    {
      return default(bool);
    }

    public static bool operator == (System.DateTimeOffset left, System.DateTimeOffset right)
    {
      return default(bool);
    }

    public static bool operator > (System.DateTimeOffset left, System.DateTimeOffset right)
    {
      return default(bool);
    }

    public static bool operator >= (System.DateTimeOffset left, System.DateTimeOffset right)
    {
      return default(bool);
    }

    public System.DateTimeOffset Add(TimeSpan timeSpan)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public System.DateTimeOffset AddDays(double days)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public System.DateTimeOffset AddHours(double hours)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public System.DateTimeOffset AddMilliseconds(double milliseconds)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public System.DateTimeOffset AddMinutes(double minutes)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public System.DateTimeOffset AddMonths(int months)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public System.DateTimeOffset AddSeconds(double seconds)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public System.DateTimeOffset AddTicks(long ticks)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public System.DateTimeOffset AddYears(int years)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public static int Compare(System.DateTimeOffset first, System.DateTimeOffset second)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 1);

      return default(int);
    }

    public int CompareTo(System.DateTimeOffset other)
    {
      return default(int);
    }

    public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, TimeSpan offset)
    {
      Contract.Ensures(false);
    }

    public DateTimeOffset(DateTime dateTime, TimeSpan offset)
    {
      Contract.Ensures(false);
    }

    public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, System.Globalization.Calendar calendar, TimeSpan offset)
    {
      Contract.Ensures(false);
    }

    public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, TimeSpan offset)
    {
      Contract.Ensures(false);
    }

    public DateTimeOffset(DateTime dateTime)
    {
      Contract.Ensures(false);
    }

    public DateTimeOffset(long ticks, TimeSpan offset)
    {
      Contract.Ensures(false);
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public static bool Equals(System.DateTimeOffset first, System.DateTimeOffset second)
    {
      return default(bool);
    }

    public bool Equals(System.DateTimeOffset other)
    {
      return default(bool);
    }

    public bool EqualsExact(System.DateTimeOffset other)
    {
      return default(bool);
    }

    public static System.DateTimeOffset FromFileTime(long fileTime)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public static System.DateTimeOffset Parse(string input)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public static System.DateTimeOffset Parse(string input, IFormatProvider formatProvider)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public static System.DateTimeOffset Parse(string input, IFormatProvider formatProvider, System.Globalization.DateTimeStyles styles)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public static System.DateTimeOffset ParseExact(string input, string format, IFormatProvider formatProvider, System.Globalization.DateTimeStyles styles)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public static System.DateTimeOffset ParseExact(string input, string[] formats, IFormatProvider formatProvider, System.Globalization.DateTimeStyles styles)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public static System.DateTimeOffset ParseExact(string input, string format, IFormatProvider formatProvider)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public System.DateTimeOffset Subtract(TimeSpan value)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public TimeSpan Subtract(System.DateTimeOffset value)
    {
      return default(TimeSpan);
    }

    public static implicit operator System.DateTimeOffset(DateTime dateTime)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    int System.IComparable.CompareTo(Object obj)
    {
      return default(int);
    }

    void System.Runtime.Serialization.IDeserializationCallback.OnDeserialization(Object sender)
    {
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public long ToFileTime()
    {
      Contract.Ensures(0 <= Contract.Result<long>());
      Contract.Ensures(Contract.Result<long>() <= 8718460804854775808);

      return default(long);
    }

    public System.DateTimeOffset ToLocalTime()
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public System.DateTimeOffset ToOffset(TimeSpan offset)
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public override string ToString()
    {
      return default(string);
    }

    public string ToString(IFormatProvider formatProvider)
    {
      Contract.Ensures(0 <= string.Empty.Length);

      return default(string);
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
      return default(string);
    }

    public string ToString(string format)
    {
      Contract.Ensures(0 <= string.Empty.Length);

      return default(string);
    }

    public System.DateTimeOffset ToUniversalTime()
    {
      Contract.Ensures(false);

      return default(System.DateTimeOffset);
    }

    public static bool TryParse(string input, IFormatProvider formatProvider, System.Globalization.DateTimeStyles styles, out System.DateTimeOffset result)
    {
      Contract.Ensures(false);

      result = default(System.DateTimeOffset);

      return default(bool);
    }

    public static bool TryParse(string input, out System.DateTimeOffset result)
    {
      Contract.Ensures(false);

      result = default(System.DateTimeOffset);

      return default(bool);
    }

    public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, System.Globalization.DateTimeStyles styles, out System.DateTimeOffset result)
    {
      Contract.Ensures(false);

      result = default(System.DateTimeOffset);

      return default(bool);
    }

    public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, System.Globalization.DateTimeStyles styles, out System.DateTimeOffset result)
    {
      Contract.Ensures(false);

      result = default(System.DateTimeOffset);

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

    public DateTime DateTime
    {
      get
      {
        return default(DateTime);
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

    public System.DateTime LocalDateTime
    {
      get
      {
        return default(System.DateTime);
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

    public static System.DateTimeOffset Now
    {
      get
      {
        Contract.Ensures(false);

        return default(System.DateTimeOffset);
      }
    }

    public TimeSpan Offset
    {
      get
      {
        return default(TimeSpan);
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

    public System.DateTime UtcDateTime
    {
      get
      {
        return default(System.DateTime);
      }
    }

    public static System.DateTimeOffset UtcNow
    {
      get
      {
        Contract.Ensures(false);

        return default(System.DateTimeOffset);
      }
    }

    public long UtcTicks
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<long>());

        return default(long);
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
    public readonly static System.DateTimeOffset MaxValue;
    public readonly static System.DateTimeOffset MinValue;
    #endregion
  }
}

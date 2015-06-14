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

// File System.TimeSpan.cs
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
  public partial struct TimeSpan : IComparable, IComparable<TimeSpan>, IEquatable<TimeSpan>, IFormattable
  {
    #region Methods and constructors
    public static System.TimeSpan operator - (System.TimeSpan t1, System.TimeSpan t2)
    {
      return default(System.TimeSpan);
    }

    public static System.TimeSpan operator - (System.TimeSpan t)
    {
      return default(System.TimeSpan);
    }

    public static bool operator != (System.TimeSpan t1, System.TimeSpan t2)
    {
      return default(bool);
    }

    public static System.TimeSpan operator + (System.TimeSpan t)
    {
      return default(System.TimeSpan);
    }

    public static System.TimeSpan operator + (System.TimeSpan t1, System.TimeSpan t2)
    {
      return default(System.TimeSpan);
    }

    public static bool operator < (System.TimeSpan t1, System.TimeSpan t2)
    {
      return default(bool);
    }

    public static bool operator <=(System.TimeSpan t1, System.TimeSpan t2)
    {
      return default(bool);
    }

    public static bool operator == (System.TimeSpan t1, System.TimeSpan t2)
    {
      return default(bool);
    }

    public static bool operator > (System.TimeSpan t1, System.TimeSpan t2)
    {
      return default(bool);
    }

    public static bool operator >= (System.TimeSpan t1, System.TimeSpan t2)
    {
      return default(bool);
    }

    public System.TimeSpan Add(System.TimeSpan ts)
    {
      return default(System.TimeSpan);
    }

    public static int Compare(System.TimeSpan t1, System.TimeSpan t2)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 1);

      return default(int);
    }

    public int CompareTo(Object value)
    {
      return default(int);
    }

    public int CompareTo(System.TimeSpan value)
    {
      return default(int);
    }

    public System.TimeSpan Duration()
    {
      return default(System.TimeSpan);
    }

    public bool Equals(System.TimeSpan obj)
    {
      return default(bool);
    }

    public override bool Equals(Object value)
    {
      return default(bool);
    }

    public static bool Equals(System.TimeSpan t1, System.TimeSpan t2)
    {
      return default(bool);
    }

    public static System.TimeSpan FromDays(double value)
    {
      return default(System.TimeSpan);
    }

    public static System.TimeSpan FromHours(double value)
    {
      return default(System.TimeSpan);
    }

    public static System.TimeSpan FromMilliseconds(double value)
    {
      return default(System.TimeSpan);
    }

    public static System.TimeSpan FromMinutes(double value)
    {
      return default(System.TimeSpan);
    }

    public static System.TimeSpan FromSeconds(double value)
    {
      return default(System.TimeSpan);
    }

    public static System.TimeSpan FromTicks(long value)
    {
      return default(System.TimeSpan);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public System.TimeSpan Negate()
    {
      return default(System.TimeSpan);
    }

    public static System.TimeSpan Parse(string s)
    {
      return default(System.TimeSpan);
    }

    public static System.TimeSpan Parse(string input, IFormatProvider formatProvider)
    {
      return default(System.TimeSpan);
    }

    public static System.TimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider)
    {
      return default(System.TimeSpan);
    }

    public static System.TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider)
    {
      return default(System.TimeSpan);
    }

    public static System.TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider, System.Globalization.TimeSpanStyles styles)
    {
      return default(System.TimeSpan);
    }

    public static System.TimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider, System.Globalization.TimeSpanStyles styles)
    {
      return default(System.TimeSpan);
    }

    public System.TimeSpan Subtract(System.TimeSpan ts)
    {
      return default(System.TimeSpan);
    }

    public TimeSpan(long ticks)
    {
      TimeSpan.TicksPerMillisecond = default(long);
      TimeSpan.TicksPerSecond = default(long);
      TimeSpan.TicksPerMinute = default(long);
      TimeSpan.TicksPerHour = default(long);
      TimeSpan.TicksPerDay = default(long);
    }

    public TimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
    {
      TimeSpan.TicksPerMillisecond = default(long);
      TimeSpan.TicksPerSecond = default(long);
      TimeSpan.TicksPerMinute = default(long);
      TimeSpan.TicksPerHour = default(long);
      TimeSpan.TicksPerDay = default(long);
    }

    public TimeSpan(int days, int hours, int minutes, int seconds)
    {
      TimeSpan.TicksPerMillisecond = default(long);
      TimeSpan.TicksPerSecond = default(long);
      TimeSpan.TicksPerMinute = default(long);
      TimeSpan.TicksPerHour = default(long);
      TimeSpan.TicksPerDay = default(long);
    }

    public TimeSpan(int hours, int minutes, int seconds)
    {
      TimeSpan.TicksPerMillisecond = default(long);
      TimeSpan.TicksPerSecond = default(long);
      TimeSpan.TicksPerMinute = default(long);
      TimeSpan.TicksPerHour = default(long);
      TimeSpan.TicksPerDay = default(long);
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
      return default(string);
    }

    public override string ToString()
    {
      return default(string);
    }

    public string ToString(string format)
    {
      return default(string);
    }

    public static bool TryParse(string s, out System.TimeSpan result)
    {
      result = default(System.TimeSpan);

      return default(bool);
    }

    public static bool TryParse(string input, IFormatProvider formatProvider, out System.TimeSpan result)
    {
      result = default(System.TimeSpan);

      return default(bool);
    }

    public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, System.Globalization.TimeSpanStyles styles, out System.TimeSpan result)
    {
      result = default(System.TimeSpan);

      return default(bool);
    }

    public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, System.Globalization.TimeSpanStyles styles, out System.TimeSpan result)
    {
      result = default(System.TimeSpan);

      return default(bool);
    }

    public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, out System.TimeSpan result)
    {
      result = default(System.TimeSpan);

      return default(bool);
    }

    public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, out System.TimeSpan result)
    {
      result = default(System.TimeSpan);

      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public int Days
    {
      get
      {
        Contract.Ensures(-10675200 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 10675200);

        return default(int);
      }
    }

    public int Hours
    {
      get
      {
        Contract.Ensures(-3 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 23);

        return default(int);
      }
    }

    public int Milliseconds
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() == -648);

        return default(int);
      }
    }

    public int Minutes
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() == -8);

        return default(int);
      }
    }

    public int Seconds
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() == -8);

        return default(int);
      }
    }

    public long Ticks
    {
      get
      {
        return default(long);
      }
    }

    public double TotalDays
    {
      get
      {
        Contract.Ensures(-9223372036854775808 <= Contract.Result<double>());
        Contract.Ensures(Contract.Result<double>() <= 9223372036854775807);

        return default(double);
      }
    }

    public double TotalHours
    {
      get
      {
        Contract.Ensures(-9223372036854775808 <= Contract.Result<double>());
        Contract.Ensures(Contract.Result<double>() <= 9223372036854775807);

        return default(double);
      }
    }

    public double TotalMilliseconds
    {
      get
      {
        Contract.Ensures(-9223372036854775807 <= Contract.Result<double>());
        Contract.Ensures(Contract.Result<double>() <= 9223372036854775807);

        return default(double);
      }
    }

    public double TotalMinutes
    {
      get
      {
        Contract.Ensures(-9223372036854775808 <= Contract.Result<double>());
        Contract.Ensures(Contract.Result<double>() <= 9223372036854775807);

        return default(double);
      }
    }

    public double TotalSeconds
    {
      get
      {
        Contract.Ensures(-9223372036854775808 <= Contract.Result<double>());
        Contract.Ensures(Contract.Result<double>() <= 9223372036854775807);

        return default(double);
      }
    }
    #endregion

    #region Fields
    public readonly static System.TimeSpan MaxValue;
    public readonly static System.TimeSpan MinValue;
    public static long TicksPerDay;
    public static long TicksPerHour;
    public static long TicksPerMillisecond;
    public static long TicksPerMinute;
    public static long TicksPerSecond;
    public readonly static System.TimeSpan Zero;
    #endregion
  }
}

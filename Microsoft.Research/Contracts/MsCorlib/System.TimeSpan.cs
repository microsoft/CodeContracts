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

using System; 
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System {
  // Summary:
  //     Represents a time interval.

  public struct TimeSpan {
#if false
    // Summary:
    //     Represents the number of ticks in 1 day. This field is constant.
    public const long TicksPerDay = 864000000000;
    //
    // Summary:
    //     Represents the number of ticks in 1 hour. This field is constant.
    public const long TicksPerHour = 36000000000;
    //
    // Summary:
    //     Represents the number of ticks in 1 millisecond. This field is constant.
    public const long TicksPerMillisecond = 10000;
    //
    // Summary:
    //     Represents the number of ticks in 1 minute. This field is constant.
    public const long TicksPerMinute = 600000000;
    //
    // Summary:
    //     Represents the number of ticks in 1 second.
    public const long TicksPerSecond = 10000000;

    // Summary:
    //     Represents the maximum System.TimeSpan value. This field is read-only.
    public static readonly TimeSpan MaxValue;
    //
    // Summary:
    //     Represents the minimum System.TimeSpan value. This field is read-only.
    public static readonly TimeSpan MinValue;
    //
    // Summary:
    //     Represents the zero System.TimeSpan value. This field is read-only.
    public static readonly TimeSpan Zero;
#endif
    //
    // Summary:
    //     Initializes a new System.TimeSpan to the specified number of ticks.
    //
    // Parameters:
    //   ticks:
    //     A time period expressed in 100-nanosecond units.
    public TimeSpan(long ticks)
    {
      Contract.Ensures(this.Ticks == ticks);
    }
    //
    // Summary:
    //     Initializes a new System.TimeSpan to a specified number of hours, minutes,
    //     and seconds.
    //
    // Parameters:
    //   hours:
    //     Number of hours.
    //
    //   minutes:
    //     Number of minutes.
    //
    //   seconds:
    //     Number of seconds.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The parameters specify a System.TimeSpan value less than System.TimeSpan.MinValue
    //     or greater than System.TimeSpan.MaxValue.
    // public TimeSpan(int hours, int minutes, int seconds);
    //
    // Summary:
    //     Initializes a new System.TimeSpan to a specified number of days, hours, minutes,
    //     and seconds.
    //
    // Parameters:
    //   days:
    //     Number of days.
    //
    //   hours:
    //     Number of hours.
    //
    //   minutes:
    //     Number of minutes.
    //
    //   seconds:
    //     Number of seconds.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The parameters specify a System.TimeSpan value less than System.TimeSpan.MinValue
    //     or greater than System.TimeSpan.MaxValue.
    // public TimeSpan(int days, int hours, int minutes, int seconds);
    //
    // Summary:
    //     Initializes a new System.TimeSpan to a specified number of days, hours, minutes,
    //     seconds, and milliseconds.
    //
    // Parameters:
    //   days:
    //     Number of days.
    //
    //   hours:
    //     Number of hours.
    //
    //   minutes:
    //     Number of minutes.
    //
    //   seconds:
    //     Number of seconds.
    //
    //   milliseconds:
    //     Number of milliseconds.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The parameters specify a System.TimeSpan value less than System.TimeSpan.MinValue
    //     or greater than System.TimeSpan.MaxValue.
    // public TimeSpan(int days, int hours, int minutes, int seconds, int milliseconds);

    // Summary:
    //     Returns a System.TimeSpan whose value is the negated value of the specified
    //     instance.
    //
    // Parameters:
    //   t:
    //     A System.TimeSpan.
    //
    // Returns:
    //     A System.TimeSpan with the same numeric value as this instance, but the opposite
    //     sign.
    //
    // Exceptions:
    //   System.OverflowException:
    //     The negated value of this instance cannot be represented by a System.TimeSpan;
    //     that is, the value of this instance is System.TimeSpan.MinValue.
   // public static TimeSpan operator -(TimeSpan t);
    //
    // Summary:
    //     Subtracts a specified System.TimeSpan from another specified System.TimeSpan.
    //
    // Parameters:
    //   t1:
    //     A System.TimeSpan.
    //
    //   t2:
    //     A TimeSpan.
    //
    // Returns:
    //     A TimeSpan whose value is the result of the value of t1 minus the value of
    //     t2.
    //
    // Exceptions:
    //   System.OverflowException:
    //     The return value is less than System.TimeSpan.MinValue or greater than System.TimeSpan.MaxValue.
    // public static TimeSpan operator -(TimeSpan t1, TimeSpan t2);
    //
    // Summary:
    //     Indicates whether two System.TimeSpan instances are not equal.
    //
    // Parameters:
    //   t1:
    //     A System.TimeSpan.
    //
    //   t2:
    //     A TimeSpan.
    //
    // Returns:
    //     true if the values of t1 and t2 are not equal; otherwise, false.
    // public static bool operator !=(TimeSpan t1, TimeSpan t2);
    //
    // Summary:
    //     Returns the specified instance of System.TimeSpan.
    //
    // Parameters:
    //   t:
    //     A System.TimeSpan.
    //
    // Returns:
    //     Returns t.
    //public static TimeSpan operator +(TimeSpan t);
    //
    // Summary:
    //     Adds two specified System.TimeSpan instances.
    //
    // Parameters:
    //   t1:
    //     A System.TimeSpan.
    //
    //   t2:
    //     A TimeSpan.
    //
    // Returns:
    //     A System.TimeSpan whose value is the sum of the values of t1 and t2.
    //
    // Exceptions:
    //   System.OverflowException:
    //     The resulting System.TimeSpan is less than System.TimeSpan.MinValue or greater
    //     than System.TimeSpan.MaxValue.
    //public static TimeSpan operator +(TimeSpan t1, TimeSpan t2);
    //
    // Summary:
    //     Indicates whether a specified System.TimeSpan is less than another specified
    //     System.TimeSpan.
    //
    // Parameters:
    //   t1:
    //     A System.TimeSpan.
    //
    //   t2:
    //     A TimeSpan.
    //
    // Returns:
    //     true if the value of t1 is less than the value of t2; otherwise, false.
   // public static bool operator <(TimeSpan t1, TimeSpan t2);
    //
    // Summary:
    //     Indicates whether a specified System.TimeSpan is less than or equal to another
    //     specified System.TimeSpan.
    //
    // Parameters:
    //   t1:
    //     A System.TimeSpan.
    //
    //   t2:
    //     A TimeSpan.
    //
    // Returns:
    //     true if the value of t1 is less than or equal to the value of t2; otherwise,
    //     false.
    //public static bool operator <=(TimeSpan t1, TimeSpan t2);
    //
    // Summary:
    //     Indicates whether two System.TimeSpan instances are equal.
    //
    // Parameters:
    //   t1:
    //     A System.TimeSpan.
    //
    //   t2:
    //     A TimeSpan.
    //
    // Returns:
    //     true if the values of t1 and t2 are equal; otherwise, false.
    //public static bool operator ==(TimeSpan t1, TimeSpan t2);
    //
    // Summary:
    //     Indicates whether a specified System.TimeSpan is greater than another specified
    //     System.TimeSpan.
    //
    // Parameters:
    //   t1:
    //     A System.TimeSpan.
    //
    //   t2:
    //     A TimeSpan.
    //
    // Returns:
    //     true if the value of t1 is greater than the value of t2; otherwise, false.
    //public static bool operator >(TimeSpan t1, TimeSpan t2);
    //
    // Summary:
    //     Indicates whether a specified System.TimeSpan is greater than or equal to
    //     another specified System.TimeSpan.
    //
    // Parameters:
    //   t1:
    //     A System.TimeSpan.
    //
    //   t2:
    //     A TimeSpan.
    //
    // Returns:
    //     true if the value of t1 is greater than or equal to the value of t2; otherwise,
    //     false.
    //public static bool operator >=(TimeSpan t1, TimeSpan t2);

    // Summary:
    //     Gets the number of whole days represented by the current System.TimeSpan
    //     structure.
    //
    // Returns:
    //     The day component of this instance. The return value can be positive or negative.
    //public int Days { get; }
    //
    // Summary:
    //     Gets the number of whole hours represented by the current System.TimeSpan
    //     structure.
    //
    // Returns:
    //     The hour component of the current System.TimeSpan structure. The return value
    //     ranges from -23 through 23.
    public int Hours
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() > -24);
        Contract.Ensures(Contract.Result<int>() < 24);
        return default(int);
      }
    }
    //
    // Summary:
    //     Gets the number of whole milliseconds represented by the current System.TimeSpan
    //     structure.
    //
    // Returns:
    //     The millisecond component of the current System.TimeSpan structure. The return
    //     value ranges from -999 through 999.
    public int Milliseconds
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() > -1000);
        Contract.Ensures(Contract.Result<int>() < 1000);
        return default(int);
      }
    }
    //
    // Summary:
    //     Gets the number of whole minutes represented by the current System.TimeSpan
    //     structure.
    //
    // Returns:
    //     The minute component of the current System.TimeSpan structure. The return
    //     value ranges from -59 through 59.
    public int Minutes
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() > -60);
        Contract.Ensures(Contract.Result<int>() < 60);
        return default(int);
      }
    }
    //
    // Summary:
    //     Gets the number of whole seconds represented by the current System.TimeSpan
    //     structure.
    //
    // Returns:
    //     The second component of the current System.TimeSpan structure. The return
    //     value ranges from -59 through 59.
    public int Seconds
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() > -60);
        Contract.Ensures(Contract.Result<int>() < 60);
        return default(int);
      }
    }
    //
    // Summary:
    //     Gets the number of ticks that represent the value of the current System.TimeSpan
    //     structure.
    //
    // Returns:
    //     The number of ticks contained in this instance.
    public long Ticks 
    {
      get
      {
        return default(long);
      }
    }
    //
    // Summary:
    //     Gets the value of the current System.TimeSpan structure expressed in whole
    //     and fractional days.
    //
    // Returns:
    //     The total number of days represented by this instance.
    // public double TotalDays { get; }
    //
    // Summary:
    //     Gets the value of the current System.TimeSpan structure expressed in whole
    //     and fractional hours.
    //
    // Returns:
    //     The total number of hours represented by this instance.
    // public double TotalHours { get; }
    //
    // Summary:
    //     Gets the value of the current System.TimeSpan structure expressed in whole
    //     and fractional milliseconds.
    //
    // Returns:
    //     The total number of milliseconds represented by this instance.
    // public double TotalMilliseconds { get; }
    //
    // Summary:
    //     Gets the value of the current System.TimeSpan structure expressed in whole
    //     and fractional minutes.
    //
    // Returns:
    //     The total number of minutes represented by this instance.
    // public double TotalMinutes { get; }
    //
    // Summary:
    //     Gets the value of the current System.TimeSpan structure expressed in whole
    //     and fractional seconds.
    //
    // Returns:
    //     The total number of seconds represented by this instance.
    // public double TotalSeconds { get; }

    // Summary:
    //     Adds the specified System.TimeSpan to this instance.
    //
    // Parameters:
    //   ts:
    //     A System.TimeSpan.
    //
    // Returns:
    //     A System.TimeSpan that represents the value of this instance plus the value
    //     of ts.
    //
    // Exceptions:
    //   System.OverflowException:
    //     The resulting System.TimeSpan is less than System.TimeSpan.MinValue or greater
    //     than System.TimeSpan.MaxValue.
    // public TimeSpan Add(TimeSpan ts);
    //
    // Summary:
    //     Compares two System.TimeSpan values and returns an integer that indicates
    //     their relationship.
    //
    // Parameters:
    //   t1:
    //     A System.TimeSpan.
    //
    //   t2:
    //     A System.TimeSpan.
    //
    // Returns:
    //     Value Condition -1 t1 is less than t20 t1 is equal to t21 t1 is greater than
    //     t2
    [Pure]
    public static int Compare(TimeSpan t1, TimeSpan t2) {
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() <= 1);
      return default(int);
    }
    //
    // Summary:
    //     Returns a new System.TimeSpan object whose value is the absolute value of
    //     the current System.TimeSpan object.
    //
    // Returns:
    //     A new System.TimeSpan whose value is the absolute value of the current System.TimeSpan
    //     object.
    //
    // Exceptions:
    //   System.OverflowException:
    //     The value of this instance is System.TimeSpan.MinValue.
    [Pure]
    public TimeSpan Duration()
    {
      return default(TimeSpan);
    }

    //
    // Summary:
    //     Returns a value indicating whether two specified instances of System.TimeSpan
    //     are equal.
    //
    // Parameters:
    //   t1:
    //     A System.TimeSpan.
    //
    //   t2:
    //     A TimeSpan.
    //
    // Returns:
    //     true if the values of t1 and t2 are equal; otherwise, false.
    [Pure]
    public static bool Equals(TimeSpan t1, TimeSpan t2)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Returns a System.TimeSpan that represents a specified number of days, where
    //     the specification is accurate to the nearest millisecond.
    //
    // Parameters:
    //   value:
    //     A number of days, accurate to the nearest millisecond.
    //
    // Returns:
    //     A System.TimeSpan that represents value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.TimeSpan.MinValue or greater than System.TimeSpan.MaxValue.
    //     -or-value is System.Double.PositiveInfinity.-or-value is System.Double.NegativeInfinity.
    //
    //   System.ArgumentException:
    //     value is equal to System.Double.NaN.
    [Pure]
    public static TimeSpan FromDays(double value)
    {
      return default(TimeSpan);
    }
    //
    // Summary:
    //     Returns a System.TimeSpan that represents a specified number of hours, where
    //     the specification is accurate to the nearest millisecond.
    //
    // Parameters:
    //   value:
    //     A number of hours accurate to the nearest millisecond.
    //
    // Returns:
    //     A System.TimeSpan that represents value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.TimeSpan.MinValue or greater than System.TimeSpan.MaxValue.
    //     -or-value is System.Double.PositiveInfinity.-or-value is System.Double.NegativeInfinity.
    //
    //   System.ArgumentException:
    //     value is equal to System.Double.NaN.
    [Pure]
    public static TimeSpan FromHours(double value)
    {
      return default(TimeSpan);
    }
    //
    // Summary:
    //     Returns a System.TimeSpan that represents a specified number of milliseconds.
    //
    // Parameters:
    //   value:
    //     A number of milliseconds.
    //
    // Returns:
    //     A System.TimeSpan that represents value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.TimeSpan.MinValue or greater than System.TimeSpan.MaxValue.-or-value
    //     is System.Double.PositiveInfinity.-or-value is System.Double.NegativeInfinity.
    //
    //   System.ArgumentException:
    //     value is equal to System.Double.NaN.
    [Pure]
    public static TimeSpan FromMilliseconds(double value)
    {
      return default(TimeSpan);
    }
    //
    // Summary:
    //     Returns a System.TimeSpan that represents a specified number of minutes,
    //     where the specification is accurate to the nearest millisecond.
    //
    // Parameters:
    //   value:
    //     A number of minutes, accurate to the nearest millisecond.
    //
    // Returns:
    //     A System.TimeSpan that represents value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.TimeSpan.MinValue or greater than System.TimeSpan.MaxValue.-or-value
    //     is System.Double.PositiveInfinity.-or-value is System.Double.NegativeInfinity.
    //
    //   System.ArgumentException:
    //     value is equal to System.Double.NaN.
    [Pure]
    public static TimeSpan FromMinutes(double value)
    {
      return default(TimeSpan);
    }
    //
    // Summary:
    //     Returns a System.TimeSpan that represents a specified number of seconds,
    //     where the specification is accurate to the nearest millisecond.
    //
    // Parameters:
    //   value:
    //     A number of seconds, accurate to the nearest millisecond.
    //
    // Returns:
    //     A System.TimeSpan that represents value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.TimeSpan.MinValue or greater than System.TimeSpan.MaxValue.-or-value
    //     is System.Double.PositiveInfinity.-or-value is System.Double.NegativeInfinity.
    //
    //   System.ArgumentException:
    //     value is equal to System.Double.NaN.
    [Pure]
    public static TimeSpan FromSeconds(double value)
    {
      return default(TimeSpan);
    }
    //
    // Summary:
    //     Returns a System.TimeSpan that represents a specified time, where the specification
    //     is in units of ticks.
    //
    // Parameters:
    //   value:
    //     A number of ticks that represent a time.
    //
    // Returns:
    //     A System.TimeSpan with a value of value.
    [Pure]
    public static TimeSpan FromTicks(long value)
    {
      return default(TimeSpan);
    }

    //
    // Summary:
    //     Returns a System.TimeSpan whose value is the negated value of this instance.
    //
    // Returns:
    //     The same numeric value as this instance, but with the opposite sign.
    //
    // Exceptions:
    //   System.OverflowException:
    //     The negated value of this instance cannot be represented by a System.TimeSpan;
    //     that is, the value of this instance is System.TimeSpan.MinValue.
    [Pure]
    public TimeSpan Negate()
    {
      return default(TimeSpan);
    }
    //
    // Summary:
    //     Constructs a new System.TimeSpan object from a time interval specified in
    //     a string.
    //
    // Parameters:
    //   s:
    //     A string that specifies a time interval.
    //
    // Returns:
    //     A System.TimeSpan that corresponds to s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s has an invalid format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.TimeSpan.MinValue or greater than
    //     System.TimeSpan.MaxValue.-or- At least one of the days, hours, minutes, or
    //     seconds components is outside its valid range.
    [Pure]
    public static TimeSpan Parse(string s)
    {
      Contract.Requires(s != null);

      return default(TimeSpan);
    }
    //
    // Summary:
    //     Subtracts the specified System.TimeSpan from this instance.
    //
    // Parameters:
    //   ts:
    //     A System.TimeSpan.
    //
    // Returns:
    //     A System.TimeSpan whose value is the result of the value of this instance
    //     minus the value of ts.
    //
    // Exceptions:
    //   System.OverflowException:
    //     The return value is less than System.TimeSpan.MinValue or greater than System.TimeSpan.MaxValue.
    [Pure]
    public TimeSpan Subtract(TimeSpan ts)
    {
      return default(TimeSpan);
    }

    //
    // Summary:
    //     Constructs a new System.TimeSpan object from a time interval specified in
    //     a string. Parameters specify the time interval and the variable where the
    //     new System.TimeSpan object is returned.
    //
    // Parameters:
    //   s:
    //     A string that specifies a time interval.
    //
    //   result:
    //     When this method returns, contains an object that represents the time interval
    //     specified by s, or System.TimeSpan.Zero if the conversion failed. This parameter
    //     is passed uninitialized.
    //
    // Returns:
    //     true if s was converted successfully; otherwise, false. This operation returns
    //     false if the s parameter is null, has an invalid format, represents a time
    //     interval less than System.TimeSpan.MinValue or greater than System.TimeSpan.MaxValue,
    //     or has at least one days, hours, minutes, or seconds component outside its
    //     valid range.
    [Pure]
    extern public static bool TryParse(string s, out TimeSpan result);

  }
}

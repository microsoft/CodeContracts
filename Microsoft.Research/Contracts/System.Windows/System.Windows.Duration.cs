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

#region Assembly System.Windows.dll, v2.0.50727
// C:\Program Files\Reference Assemblies\Microsoft\Framework\Silverlight\v4.0\Profile\WindowsPhone\System.Windows.dll
#endregion

using System;
using System.Diagnostics.Contracts;

namespace System.Windows
{
  // Summary:
  //     Represents the duration of time that a System.Windows.Media.Animation.Timeline
  //     is active.
  public struct Duration
  {
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Duration structure with
    //     the supplied System.TimeSpan value.
    //
    // Parameters:
    //   timeSpan:
    //     Represents the initial time interval of this duration.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     timeSpan evaluates as less than System.TimeSpan.Zero.
    extern public Duration(TimeSpan timeSpan);

    // Summary:
    //     Subtracts the value of one System.Windows.Duration from another.
    //
    // Parameters:
    //   t1:
    //     The first System.Windows.Duration.
    //
    //   t2:
    //     The System.Windows.Duration to subtract.
    //
    // Returns:
    //     If each System.Windows.Duration has values, a System.Windows.Duration that
    //     represents the value of t1 minus t2. If t1 has a value of System.Windows.Duration.Forever
    //     and t2 has a value of System.Windows.Duration.TimeSpan, this method returns
    //     System.Windows.Duration.Forever. Otherwise this method returns null.
    extern public static Duration operator -(Duration t1, Duration t2);
    //
    // Summary:
    //     Determines if two System.Windows.Duration cases are not equal.
    //
    // Parameters:
    //   t1:
    //     The first System.Windows.Duration to compare.
    //
    //   t2:
    //     The second System.Windows.Duration to compare.
    //
    // Returns:
    //     true if exactly one of t1 or t2 represent a value, or if they both represent
    //     values that are not equal; otherwise, false.
    extern public static bool operator !=(Duration t1, Duration t2);
    //
    // Summary:
    //     Returns the specified System.Windows.Duration.
    //
    // Parameters:
    //   duration:
    //     The System.Windows.Duration to get.
    //
    // Returns:
    //     A System.Windows.Duration.
    extern public static Duration operator +(Duration duration);
    //
    // Summary:
    //     Adds two System.Windows.Duration values together.
    //
    // Parameters:
    //   t1:
    //     The first System.Windows.Duration to add.
    //
    //   t2:
    //     The second System.Windows.Duration to add.
    //
    // Returns:
    //     If both System.Windows.Duration values have System.TimeSpan values, this
    //     method returns the sum of those two values. If either value is set to System.Windows.Duration.Automatic,
    //     the method returns System.Windows.Duration.Automatic. If either value is
    //     set to System.Windows.Duration.Forever, the method returns System.Windows.Duration.Forever.If
    //     either t1 or t2 has no value, this method returns null.
    extern public static Duration operator +(Duration t1, Duration t2);
    //
    // Summary:
    //     Determines if a System.Windows.Duration is less than the value of another
    //     instance.
    //
    // Parameters:
    //   t1:
    //     The first System.Windows.Duration to compare.
    //
    //   t2:
    //     The second System.Windows.Duration to compare.
    //
    // Returns:
    //     true if both t1 and t2 have values and t1 is less than t2; otherwise, false.
    extern public static bool operator <(Duration t1, Duration t2);
    //
    // Summary:
    //     Determines if a System.Windows.Duration is less than or equal to another.
    //
    // Parameters:
    //   t1:
    //     The System.Windows.Duration to compare.
    //
    //   t2:
    //     The System.Windows.Duration to compare.
    //
    // Returns:
    //     true if both t1 and t2 have values and t1 is less than or equal to t2; otherwise,
    //     false.
    extern public static bool operator <=(Duration t1, Duration t2);
    //
    // Summary:
    //     Determines whether two System.Windows.Duration cases are equal.
    //
    // Parameters:
    //   t1:
    //     The first System.Windows.Duration to compare.
    //
    //   t2:
    //     The second System.Windows.Duration to compare.
    //
    // Returns:
    //     true if both System.Windows.Duration values have equal property values, or
    //     if all System.Windows.Duration values are null. Otherwise, this method returns
    //     false.
    extern public static bool operator ==(Duration t1, Duration t2);
    //
    // Summary:
    //     Determines if one System.Windows.Duration is greater than another.
    //
    // Parameters:
    //   t1:
    //     The System.Windows.Duration value to compare.
    //
    //   t2:
    //     The second System.Windows.Duration value to compare.
    //
    // Returns:
    //     true if both t1 and t2 have values and t1 is greater than t2; otherwise,
    //     false.
    extern public static bool operator >(Duration t1, Duration t2);
    //
    // Summary:
    //     Determines whether a System.Windows.Duration is greater than or equal to
    //     another.
    //
    // Parameters:
    //   t1:
    //     The first instance of System.Windows.Duration to compare.
    //
    //   t2:
    //     The second instance of System.Windows.Duration to compare.
    //
    // Returns:
    //     true if both t1 and t2 have values and t1 is greater than or equal to t2;
    //     otherwise, false.
    extern public static bool operator >=(Duration t1, Duration t2);
    //
    // Summary:
    //     Implicitly creates a System.Windows.Duration from a given System.TimeSpan.
    //
    // Parameters:
    //   timeSpan:
    //     System.TimeSpan from which a System.Windows.Duration is implicitly created.
    //
    // Returns:
    //     A created System.Windows.Duration.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     timeSpan evaluates as less than System.TimeSpan.Zero.
    extern public static implicit operator Duration(TimeSpan timeSpan);

    // Summary:
    //     Gets a System.Windows.Duration value that is automatically determined.
    //
    // Returns:
    //     A System.Windows.Duration initialized to an automatic value.
    public static Duration Automatic
    {
      get
      {
        return default(Duration);
      }
    }
    //
    // Summary:
    //     Gets a System.Windows.Duration value that represents an infinite interval.
    //
    // Returns:
    //     A System.Windows.Duration initialized to a forever value.
    public static Duration Forever
    {
      get
      {
        return default(Duration);
      }
    }
    //
    // Summary:
    //     Gets a value that indicates if this System.Windows.Duration represents a
    //     System.TimeSpan value.
    //
    // Returns:
    //     true if this System.Windows.Duration is a System.TimeSpan value; otherwise,
    //     false.
    //public bool HasTimeSpan { get; }
    //
    // Summary:
    //     Gets the System.TimeSpan value that this System.Windows.Duration represents.
    //
    // Returns:
    //     The System.TimeSpan value that this System.Windows.Duration represents.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Windows.Duration does not represent a System.TimeSpan.
    //public TimeSpan TimeSpan { get; }

    // Summary:
    //     Adds the value of the specified System.Windows.Duration to this System.Windows.Duration.
    //
    // Parameters:
    //   duration:
    //     An instance of System.Windows.Duration that represents the value of the current
    //     instance plus duration.
    //
    // Returns:
    //     If each involved System.Windows.Duration has values, a System.Windows.Duration
    //     that represents the combined values. Otherwise this method returns null.
    extern public Duration Add(Duration duration);
    //
    // Summary:
    //     Compares one System.Windows.Duration value to another.
    //
    // Parameters:
    //   t1:
    //     The first instance of System.Windows.Duration to compare.
    //
    //   t2:
    //     The second instance of System.Windows.Duration to compare.
    //
    // Returns:
    //     If t1 is less than t2, a negative value that represents the difference. If
    //     t1 is equal to t2, a value of 0. If t1 is greater than t2, a positive value
    //     that represents the difference.
    extern public static int Compare(Duration t1, Duration t2);
    //
    // Summary:
    //     Determines whether a specified System.Windows.Duration is equal to this System.Windows.Duration.
    //
    // Parameters:
    //   duration:
    //     The System.Windows.Duration to check for equality.
    //
    // Returns:
    //     true if duration is equal to this System.Windows.Duration; otherwise, false.
    extern public bool Equals(Duration duration);
    //
    // Summary:
    //     Determines whether two System.Windows.Duration values are equal.
    //
    // Parameters:
    //   t1:
    //     First System.Windows.Duration to compare.
    //
    //   t2:
    //     Second System.Windows.Duration to compare.
    //
    // Returns:
    //     true if t1 is equal to t2; otherwise, false.
    extern public static bool Equals(Duration t1, Duration t2);
    //
    // Summary:
    //     Adds one System.Windows.Duration to this System.Windows.Duration.
    //
    // Parameters:
    //   duration:
    //     The System.Windows.Duration to add.
    //
    // Returns:
    //     The summed System.Windows.Duration.
    extern public static Duration Plus(Duration duration);
    //
    // Summary:
    //     Subtracts the specified System.Windows.Duration from this System.Windows.Duration.
    //
    // Parameters:
    //   duration:
    //     The System.Windows.Duration to subtract from this System.Windows.Duration.
    //
    // Returns:
    //     The subtracted System.Windows.Duration.
    extern public Duration Subtract(Duration duration);
  }
}

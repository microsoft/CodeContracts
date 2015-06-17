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
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace System
{
  // Summary:
  //     Represents a single-precision floating-point number.
  public struct Single
  {
    // Summary:
    //     Represents the smallest positive System.Single value greater than zero. This
    //     field is constant.
    public const float Epsilon = 1.4013e-045f;
    //
    // Summary:
    //     Represents the largest possible value of System.Single. This field is constant.
    public const float MaxValue = 3.40282e+038f;
    //
    // Summary:
    //     Represents the smallest possible value of System.Single. This field is constant.
    public const float MinValue = -3.40282e+038f;
    //
    // Summary:
    //     Represents not a number (NaN). This field is constant.
    public const float NaN = 0.0f / 0.0f;
    //
    // Summary:
    //     Represents negative infinity. This field is constant.
    public const float NegativeInfinity = -1.0f / 0.0f;
    //
    // Summary:
    //     Represents positive infinity. This field is constant.
    public const float PositiveInfinity = 1.0f / 0.0f;


    //
    // Summary:
    //     Returns a value indicating whether the specified number evaluates to negative
    //     or positive infinity.
    //
    // Parameters:
    //   f:
    //     A single-precision floating-point number.
    //
    // Returns:
    //     true if f evaluates to System.Single.PositiveInfinity or System.Single.NegativeInfinity;
    //     otherwise, false.
    [Pure]
    public static bool IsInfinity(float f)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Returns a value indicating whether the specified number evaluates to not
    //     a number (System.Single.NaN).
    //
    // Parameters:
    //   f:
    //     A single-precision floating-point number.
    //
    // Returns:
    //     true if f evaluates to not a number (System.Single.NaN); otherwise, false.
    [Pure]
    public static bool IsNaN(float f)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Returns a value indicating whether the specified number evaluates to negative
    //     infinity.
    //
    // Parameters:
    //   f:
    //     A single-precision floating-point number.
    //
    // Returns:
    //     true if f evaluates to System.Single.NegativeInfinity; otherwise, false.
    [Pure]
    public static bool IsNegativeInfinity(float f)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Returns a value indicating whether the specified number evaluates to positive
    //     infinity.
    //
    // Parameters:
    //   f:
    //     A single-precision floating-point number.
    //
    // Returns:
    //     true if f evaluates to System.Single.PositiveInfinity; otherwise, false.
    [Pure]
    public static bool IsPositiveInfinity(float f)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Converts the string representation of a number to its single-precision floating-point
    //     number equivalent.
    //
    // Parameters:
    //   s:
    //     A string representing a number to convert.
    //
    // Returns:
    //     A single-precision floating-point number equivalent to the numeric value
    //     or symbol specified in s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not a number in a valid format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.Single.MinValue or greater than System.Single.MaxValue.
    [Pure]
    public static float Parse(string s)
    {
      Contract.Requires(s != null);
      return default(float);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified culture-specific
    //     format to its single-precision floating-point number equivalent.
    //
    // Parameters:
    //   s:
    //     A string representing a number to convert.
    //
    //   provider:
    //     An System.IFormatProvider that supplies culture-specific formatting information
    //     about s.
    //
    // Returns:
    //     A single-precision floating-point number equivalent to the numeric value
    //     or symbol specified in s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not a number in a valid format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.Single.MinValue or greater than System.Single.MaxValue.
    [Pure]
    public static float Parse(string s, IFormatProvider provider)
    {
      Contract.Requires(s != null);
      return default(float);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified style to its
    //     single-precision floating-point number equivalent.
    //
    // Parameters:
    //   s:
    //     A string representing a number to convert.
    //
    //   style:
    //     A bitwise combination of System.Globalization.NumberStyles values that indicates
    //     the permitted format of s. A typical value to specify is System.Globalization.NumberStyles.Float
    //     combined with System.Globalization.NumberStyles.AllowThousands.
    //
    // Returns:
    //     A single-precision floating-point number equivalent to the numeric value
    //     or symbol specified in s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not a number in a valid format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.Single.MinValue or greater than System.Single.MaxValue.
    //
    //   System.ArgumentException:
    //     style is not a System.Globalization.NumberStyles value. -or-style is the
    //     System.Globalization.NumberStyles.AllowHexSpecifier value.
    [Pure]
    public static float Parse(string s, NumberStyles style)
    {
      Contract.Requires(s != null);
      return default(float);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified style and culture-specific
    //     format to its single-precision floating-point number equivalent.
    //
    // Parameters:
    //   s:
    //     A string representing a number to convert.
    //
    //   style:
    //     A bitwise combination of System.Globalization.NumberStyles values that indicates
    //     the permitted format of s. A typical value to specify is System.Globalization.NumberStyles.Float
    //     combined with System.Globalization.NumberStyles.AllowThousands.
    //
    //   provider:
    //     An System.IFormatProvider that supplies culture-specific formatting information
    //     about s.
    //
    // Returns:
    //     A single-precision floating-point number equivalent to the numeric value
    //     or symbol specified in s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not a numeric value.
    //
    //   System.ArgumentException:
    //     style is not a System.Globalization.NumberStyles value. -or-style is the
    //     System.Globalization.NumberStyles.AllowHexSpecifier value.
    [Pure]
    public static float Parse(string s, NumberStyles style, IFormatProvider provider)
    {
      Contract.Requires(s != null);
      return default(float);
    }

    //
    // Summary:
    //     Converts the numeric value of this instance to its equivalent string representation,
    //     using the specified format.
    //
    // Parameters:
    //   format:
    //     A numeric format string.
    //
    // Returns:
    //     The string representation of the value of this instance as specified by format.
    //
    // Exceptions:
    //   System.FormatException:
    //     format is invalid.
    [Pure]
    public string ToString(string format)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the string representation of a number to its single-precision floating-point
    //     number equivalent. A return code indicates whether the conversion succeeded
    //     or failed.
    //
    // Parameters:
    //   s:
    //     A string representing a number to convert.
    //
    //   result:
    //     When this method returns, contains single-precision floating-point number
    //     equivalent to the numeric value or symbol contained in s, if the conversion
    //     succeeded, or zero if the conversion failed. The conversion fails if the
    //     s parameter is null, is not a number in a valid format, or represents a number
    //     less than System.Single.MinValue or greater than System.Single.MaxValue.
    //     This parameter is passed uninitialized.
    //
    // Returns:
    //     true if s was converted successfully; otherwise, false.
    [Pure]
    public static bool TryParse(string s, out float result)
    {
      result = default(float);
      return default(bool);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified style and culture-specific
    //     format to its single-precision floating-point number equivalent. A return
    //     code indicates whether the conversion succeeded or failed.
    //
    // Parameters:
    //   s:
    //     A string representing a number to convert.
    //
    //   style:
    //     A bitwise combination of System.Globalization.NumberStyles values that indicates
    //     the permitted format of s. A typical value to specify is System.Globalization.NumberStyles.Float
    //     combined with System.Globalization.NumberStyles.AllowThousands.
    //
    //   provider:
    //     An System.IFormatProvider object that supplies culture-specific formatting
    //     information about s.
    //
    //   result:
    //     When this method returns, contains the single-precision floating-point number
    //     equivalent to the numeric value or symbol contained in s, if the conversion
    //     succeeded, or zero if the conversion failed. The conversion fails if the
    //     s parameter is null, is not in a format compliant with style, represents
    //     a number less than System.Single.MinValue or greater than System.Single.MaxValue,
    //     or style is not a valid combination of System.Globalization.NumberStyles
    //     enumerated constants. This parameter is passed uninitialized.
    //
    // Returns:
    //     true if s was converted successfully; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     style is not a System.Globalization.NumberStyles value. -or-style is the
    //     System.Globalization.NumberStyles.AllowHexSpecifier value.
    [Pure]
    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out float result)
    {
      result = default(float);
      return default(bool);
    }
  }
}


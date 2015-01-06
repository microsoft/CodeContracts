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
//using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace System
{
  // Summary:
  //     Represents a double-precision floating-point number.
  public struct Double
  {
    // Summary:
    //     Represents the smallest positive System.Double value greater than zero. This
    //     field is constant.
    public const double Epsilon = 4.94066e-324;
    //
    // Summary:
    //     Represents the largest possible value of a System.Double. This field is constant.
    public const double MaxValue = 1.79769e+308;
    //
    // Summary:
    //     Represents the smallest possible value of a System.Double. This field is
    //     constant.
    public const double MinValue = -1.79769e+308;
    //
    // Summary:
    //     Represents a value that is not a number (NaN). This field is constant.
    public const double NaN = 0.0 / 0.0;
    //
    // Summary:
    //     Represents negative infinity. This field is constant.
    public const double NegativeInfinity = -1.0 / 0.0;
    //
    // Summary:
    //     Represents positive infinity. This field is constant.
    public const double PositiveInfinity = 1.0 / 0.0;

    //
    // Summary:
    //     Returns a value indicating whether the specified number evaluates to negative
    //     or positive infinity
    //
    // Parameters:
    //   d:
    //     A double-precision floating-point number.
    //
    // Returns:
    //     true if d evaluates to System.Double.PositiveInfinity or System.Double.NegativeInfinity;
    //     otherwise, false.
    [Pure]
    public static bool IsInfinity(double d)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Returns a value indicating whether the specified number evaluates to a value
    //     that is not a number (System.Double.NaN).
    //
    // Parameters:
    //   d:
    //     A double-precision floating-point number.
    //
    // Returns:
    //     true if d evaluates to System.Double.NaN; otherwise, false.
    [Pure]
    public static bool IsNaN(double d)
    {
      // F: I am commenting it until we have implemented a better handling of the equalities for floats
      
      // Disabling the warning from the C# compiler. (d != d) in .NET if and only if it is Double.IsNan(d)
//#pragma warning disable 1718
//      Contract.Ensures(Contract.Result<bool>() == (d != d));
//#pragma warning restore 1718 

      return default(bool);
    }
    //
    // Summary:
    //     Returns a value indicating whether the specified number evaluates to negative
    //     infinity.
    //
    // Parameters:
    //   d:
    //     A double-precision floating-point number.
    //
    // Returns:
    //     true if d evaluates to System.Double.NegativeInfinity; otherwise, false.
    [Pure]
    public static bool IsNegativeInfinity(double d)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Returns a value indicating whether the specified number evaluates to positive
    //     infinity.
    //
    // Parameters:
    //   d:
    //     A double-precision floating-point number.
    //
    // Returns:
    //     true if d evaluates to System.Double.PositiveInfinity; otherwise, false.
    [Pure]
    public static bool IsPositiveInfinity(double d)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Converts the string representation of a number to its double-precision floating-point
    //     number equivalent.
    //
    // Parameters:
    //   s:
    //     A string containing a number to convert.
    //
    // Returns:
    //     A double-precision floating-point number equivalent to the numeric value
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
    //     s represents a number less than System.Double.MinValue or greater than System.Double.MaxValue.
    [Pure]
    public static double Parse(string s)
    {
      Contract.Requires(s != null);
      return default(float);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified culture-specific
    //     format to its double-precision floating-point number equivalent.
    //
    // Parameters:
    //   s:
    //     A string containing a number to convert.
    //
    //   provider:
    //     An System.IFormatProvider that supplies culture-specific formatting information
    //     about s.
    //
    // Returns:
    //     A double-precision floating-point number equivalent to the numeric value
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
    //     s represents a number less than System.Double.MinValue or greater than System.Double.MaxValue.
    [Pure]
    public static double Parse(string s, IFormatProvider provider)
    {
      Contract.Requires(s != null);
      return default(float);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified style to its
    //     double-precision floating-point number equivalent.
    //
    // Parameters:
    //   s:
    //     A string containing a number to convert.
    //
    //   style:
    //     A bitwise combination of System.Globalization.NumberStyles values that indicates
    //     the permitted format of s. A typical value to specify is System.Globalization.NumberStyles.Float
    //     combined with System.Globalization.NumberStyles.AllowThousands.
    //
    // Returns:
    //     A double-precision floating-point number equivalent to the numeric value
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
    //     s represents a number less than System.Double.MinValue or greater than System.Double.MaxValue.
    //
    //   System.ArgumentException:
    //     style is not a System.Globalization.NumberStyles value. -or-style is the
    //     System.Globalization.NumberStyles.AllowHexSpecifier value.
    [Pure]
    public static double Parse(string s, NumberStyles style)
    {
      Contract.Requires(s != null);
      return default(float);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified style and culture-specific
    //     format to its double-precision floating-point number equivalent.
    //
    // Parameters:
    //   s:
    //     A string containing a number to convert.
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
    //     A double-precision floating-point number equivalent to the numeric value
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
    //
    //   System.OverflowException:
    //     s represents a number less than System.Double.MinValue or greater than System.Double.MaxValue.
    [Pure]
    public static double Parse(string s, NumberStyles style, IFormatProvider provider)
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
    //     Converts the string representation of a number to its double-precision floating-point
    //     number equivalent. A return value indicates whether the conversion succeeded
    //     or failed.
    //
    // Parameters:
    //   s:
    //     A string containing a number to convert.
    //
    //   result:
    //     When this method returns, contains the double-precision floating-point number
    //     equivalent to the s parameter, if the conversion succeeded, or zero if the
    //     conversion failed. The conversion fails if the s parameter is null, is not
    //     a number in a valid format, or represents a number less than System.SByte.MinValue
    //     or greater than System.SByte.MaxValue. This parameter is passed uninitialized.
    //
    // Returns:
    //     true if s was converted successfully; otherwise, false.
    [Pure]
    public static bool TryParse(string s, out double result)
    {
      result = default(double);
      return default(bool);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified style and culture-specific
    //     format to its double-precision floating-point number equivalent. A return
    //     value indicates whether the conversion succeeded or failed.
    //
    // Parameters:
    //   s:
    //     A string containing a number to convert.
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
    //   result:
    //     When this method returns, contains a double-precision floating-point number
    //     equivalent to the numeric value or symbol contained in s, if the conversion
    //     succeeded, or zero if the conversion failed. The conversion fails if the
    //     s parameter is null, is not in a format compliant with style, represents
    //     a number less than System.SByte.MinValue or greater than System.SByte.MaxValue,
    //     or if style is not a valid combination of System.Globalization.NumberStyles
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
    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out double result)
    {
      result = default(double);
      return default(bool);
    }
  }
}

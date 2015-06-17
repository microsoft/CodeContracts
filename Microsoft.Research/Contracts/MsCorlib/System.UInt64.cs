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
  //     Represents a 64-bit unsigned integer.
  public struct UInt64
  {
    // Summary:
    //     Represents the largest possible value of System.UInt64. This field is constant.
    public const ulong MaxValue = 18446744073709551615;
    //
    // Summary:
    //     Represents the smallest possible value of System.UInt64. This field is constant.
    public const ulong MinValue = 0;

    //
    // Summary:
    //     Converts the string representation of a number to its 64-bit unsigned integer
    //     equivalent.
    //
    // Parameters:
    //   s:
    //     A string representing the number to convert.
    //
    // Returns:
    //     A 64-bit unsigned integer equivalent to the number contained in s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The s parameter is null.
    //
    //   System.FormatException:
    //     The s parameter is not in the correct format.
    //
    //   System.OverflowException:
    //     The s parameter represents a number less than System.UInt64.MinValue or greater
    //     than System.UInt64.MaxValue.
    [Pure]
    public static ulong Parse(string s)
    {
      Contract.Requires(s != null);
      return default(ulong);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified culture-specific
    //     format to its 64-bit unsigned integer equivalent.
    //
    // Parameters:
    //   s:
    //     A string representing the number to convert.
    //
    //   provider:
    //     An System.IFormatProvider that supplies culture-specific formatting information
    //     about s.
    //
    // Returns:
    //     A 64-bit unsigned integer equivalent to the number specified in s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The s parameter is null.
    //
    //   System.FormatException:
    //     The s parameter is not in the correct style.
    //
    //   System.OverflowException:
    //     The s parameter represents a number less than System.UInt64.MinValue or greater
    //     than System.UInt64.MaxValue.
    [Pure]
    public static ulong Parse(string s, IFormatProvider provider)
    {
      Contract.Requires(s != null);
      return default(ulong);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified style to its
    //     64-bit unsigned integer equivalent.
    //
    // Parameters:
    //   s:
    //     A string representing the number to convert.
    //
    //   style:
    //     A bitwise combination of System.Globalization.NumberStyles values that indicates
    //     the permitted format of s. A typical value to specify is System.Globalization.NumberStyles.Integer.
    //
    // Returns:
    //     A 64-bit unsigned integer equivalent to the number specified in s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The s parameter is null.
    //
    //   System.ArgumentException:
    //     style is not a System.Globalization.NumberStyles value. -or-style is not
    //     a combination of System.Globalization.NumberStyles.AllowHexSpecifier and
    //     System.Globalization.NumberStyles.HexNumber values.
    //
    //   System.FormatException:
    //     The s parameter is not in a format compliant with style.
    //
    //   System.OverflowException:
    //     The s parameter represents a number less than System.UInt64.MinValue or greater
    //     than System.UInt64.MaxValue.
    [Pure]
    public static ulong Parse(string s, NumberStyles style)
    {
      Contract.Requires(s != null);
      return default(ulong);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified style and culture-specific
    //     format to its 64-bit unsigned integer equivalent.
    //
    // Parameters:
    //   s:
    //     A string representing the number to convert.
    //
    //   style:
    //     A bitwise combination of System.Globalization.NumberStyles values that indicates
    //     the permitted format of s. A typical value to specify is System.Globalization.NumberStyles.Integer.
    //
    //   provider:
    //     An System.IFormatProvider that supplies culture-specific formatting information
    //     about s.
    //
    // Returns:
    //     A 64-bit unsigned integer equivalent to the number specified in s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The s parameter is null.
    //
    //   System.ArgumentException:
    //     style is not a System.Globalization.NumberStyles value. -or-style is not
    //     a combination of System.Globalization.NumberStyles.AllowHexSpecifier and
    //     System.Globalization.NumberStyles.HexNumber values.
    //
    //   System.FormatException:
    //     The s parameter is not in a format compliant with style.
    //
    //   System.OverflowException:
    //     The s parameter represents a number less than System.UInt64.MinValue or greater
    //     than System.UInt64.MaxValue.
    [Pure]
    public static ulong Parse(string s, NumberStyles style, IFormatProvider provider)
    {
      Contract.Requires(s != null);
      return default(ulong);
    }

    //
    // Summary:
    //     Converts the numeric value of this instance to its equivalent string representation
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
    //     The format parameter is invalid.
    public string ToString(string format)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the string representation of a number to its 64-bit unsigned integer
    //     equivalent. A return value indicates whether the conversion succeeded or
    //     failed.
    //
    // Parameters:
    //   s:
    //     A string representing the number to convert.
    //
    //   result:
    //     When this method returns, contains the 64-bit unsigned integer value equivalent
    //     to the number contained in s, if the conversion succeeded, or zero if the
    //     conversion failed. The conversion fails if the s parameter is null, is not
    //     of the correct format, or represents a number less than System.UInt64.MinValue
    //     or greater than System.UInt64.MaxValue. This parameter is passed uninitialized.
    //
    // Returns:
    //     true if s was converted successfully; otherwise, false.
    [Pure]
    public static bool TryParse(string s, out ulong result)
    {
      result = default(ulong);
      return default(bool);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified style and culture-specific
    //     format to its 64-bit unsigned integer equivalent. A return value indicates
    //     whether the conversion succeeded or failed.
    //
    // Parameters:
    //   s:
    //     A string representing the number to convert.
    //
    //   style:
    //     A bitwise combination of System.Globalization.NumberStyles values that indicates
    //     the permitted format of s. A typical value to specify is System.Globalization.NumberStyles.Integer.
    //
    //   provider:
    //     An System.IFormatProvider object that supplies culture-specific formatting
    //     information about s.
    //
    //   result:
    //     When this method returns, contains the 64-bit unsigned integer value equivalent
    //     to the number contained in s, if the conversion succeeded, or zero if the
    //     conversion failed. The conversion fails if the s parameter is null, is not
    //     in a format compliant with style, or represents a number less than System.UInt64.MinValue
    //     or greater than System.UInt64.MaxValue. This parameter is passed uninitialized.
    //
    // Returns:
    //     true if s was converted successfully; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     style is not a System.Globalization.NumberStyles value. -or-style is not
    //     a combination of System.Globalization.NumberStyles.AllowHexSpecifier and
    //     System.Globalization.NumberStyles.HexNumber values.
    [Pure]
    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out ulong result)
    {
      result = default(ulong);
      return default(bool);
    }
  }
}


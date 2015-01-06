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
  //     Represents a 16-bit unsigned integer.
  public struct UInt16 // :  /*[t-cyrils] IComparable,*/ IFormattable, IConvertible, IComparable<ushort>, IEquatable<ushort>
  {
    // Summary:
    //     Represents the largest possible value of System.UInt16. This field is constant.
    public const ushort MaxValue = 65535;
    //
    // Summary:
    //     Represents the smallest possible value of System.UInt16. This field is constant.
    public const ushort MinValue = 0;

    //
    // Summary:
    //     Converts the string representation of a number to its 16-bit unsigned integer
    //     equivalent.
    //
    // Parameters:
    //   s:
    //     A string representing the number to convert.
    //
    // Returns:
    //     A 16-bit unsigned integer equivalent to the number contained in s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not in the correct format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.UInt16.MinValue or greater than System.UInt16.MaxValue.
    [Pure]
    public static ushort Parse(string s)
    {
      Contract.Requires(s != null);
      return default(ushort);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified culture-specific
    //     format to its 16-bit unsigned integer equivalent.
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
    //     A 16-bit unsigned integer equivalent to the number specified in s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not in the correct format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.UInt16.MinValue or greater than System.UInt16.MaxValue.
    [Pure]
    public static ushort Parse(string s, IFormatProvider provider)
    {
      Contract.Requires(s != null);
      return default(ushort);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified style to its
    //     16-bit unsigned integer equivalent.
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
    //     A 16-bit unsigned integer equivalent to the number specified in s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.ArgumentException:
    //     style is not a System.Globalization.NumberStyles value. -or-style is not
    //     a combination of System.Globalization.NumberStyles.AllowHexSpecifier and
    //     System.Globalization.NumberStyles.HexNumber values.
    //
    //   System.FormatException:
    //     s is not in a format compliant with style.
    //
    //   System.OverflowException:
    //     s represents a number less than System.UInt16.MinValue or greater than System.UInt16.MaxValue.
    [Pure]
    public static ushort Parse(string s, NumberStyles style)
    {
      Contract.Requires(s != null);
      return default(ushort);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified style and culture-specific
    //     format to its 16-bit unsigned integer equivalent.
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
    //     A 16-bit unsigned integer equivalent to the number specified in s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.ArgumentException:
    //     style is not a System.Globalization.NumberStyles value. -or-style is not
    //     a combination of System.Globalization.NumberStyles.AllowHexSpecifier and
    //     System.Globalization.NumberStyles.HexNumber values.
    //
    //   System.FormatException:
    //     s is not in a format compliant with style.
    //
    //   System.OverflowException:
    //     s represents a number less than System.UInt16.MinValue or greater than System.UInt16.MaxValue.
    [Pure]
    public static ushort Parse(string s, NumberStyles style, IFormatProvider provider)
    {
      Contract.Requires(s != null);
      return default(ushort);
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
    //     Converts the string representation of a number to its 16-bit unsigned integer
    //     equivalent. A return value indicates whether the conversion succeeded or
    //     failed.
    //
    // Parameters:
    //   s:
    //     A string representing the number to convert.
    //
    //   result:
    //     When this method returns, contains the 16-bit unsigned integer value equivalent
    //     to the number contained in s, if the conversion succeeded, or zero if the
    //     conversion failed. The conversion fails if the s parameter is null, is not
    //     in the correct format. , or represents a number less than System.UInt16.MinValue
    //     or greater than System.UInt16.MaxValue. This parameter is passed uninitialized.
    //
    // Returns:
    //     true if s was converted successfully; otherwise, false.
    [Pure]
    public static bool TryParse(string s, out ushort result)
    {
      result = default(ushort);
      return default(bool);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified style and culture-specific
    //     format to its 16-bit unsigned integer equivalent. A return value indicates
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
    //     When this method returns, contains the 16-bit unsigned integer value equivalent
    //     to the number contained in s, if the conversion succeeded, or zero if the
    //     conversion failed. The conversion fails if the s parameter is null, is not
    //     in a format compliant with style, or represents a number less than System.UInt16.MinValue
    //     or greater than System.UInt16.MaxValue. This parameter is passed uninitialized.
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
    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out ushort result)
    {
      result = default(ushort);
      return default(bool);
    }
  }
}


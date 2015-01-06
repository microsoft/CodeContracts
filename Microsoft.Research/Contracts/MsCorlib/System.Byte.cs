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
  //     Represents an 8-bit unsigned integer.
  public struct Byte
  {
    // Summary:
    //     Represents the largest possible value of a System.Byte. This field is constant.
    public const byte MaxValue = 255;
    //
    // Summary:
    //     Represents the smallest possible value of a System.Byte. This field is constant.
    public const byte MinValue = 0;
    //
    // Summary:
    //     Converts the string representation of a number to its System.Byte equivalent.
    //
    // Parameters:
    //   s:
    //     A string containing a number to convert. The string is interpreted using
    //     the System.Globalization.NumberStyles.Integer style.
    //
    // Returns:
    //     The System.Byte value equivalent to the number contained in s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not of the correct format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.Byte.MinValue or greater than System.Byte.MaxValue.
    [Pure]
    public static byte Parse(string s)
    {
      Contract.Requires(s != null);
      return default(byte);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified culture-specific
    //     format to its System.Byte equivalent.
    //
    // Parameters:
    //   s:
    //     A string containing a number to convert. The string is interpreted using
    //     the System.Globalization.NumberStyles.Integer style.
    //
    //   provider:
    //     An System.IFormatProvider that supplies culture-specific parsing information
    //     about s. If provider is null, the thread current culture is used.
    //
    // Returns:
    //     The System.Byte value equivalent to the number contained in s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not of the correct format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.Byte.MinValue or greater than System.Byte.MaxValue.
    [Pure]
    public static byte Parse(string s, IFormatProvider provider)
    {
      Contract.Requires(s != null);
      return default(byte);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified style to its
    //     System.Byte equivalent.
    //
    // Parameters:
    //   s:
    //     A string containing a number to convert. The string is interpreted using
    //     the style specified by style.
    //
    //   style:
    //     A bitwise combination of System.Globalization.NumberStyles values that indicates
    //     the permitted format of s. A typical value to specify is System.Globalization.NumberStyles.Integer.
    //
    // Returns:
    //     The System.Byte value equivalent to the number contained in s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not of the correct format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.Byte.MinValue or greater than System.Byte.MaxValue.
    //
    //   System.ArgumentException:
    //     style is not a System.Globalization.NumberStyles value. -or-style is not
    //     a combination of System.Globalization.NumberStyles.AllowHexSpecifier and
    //     System.Globalization.NumberStyles.HexNumber values.
    [Pure]
    public static byte Parse(string s, NumberStyles style)
    {
      Contract.Requires(s != null);
      return default(byte);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified style and culture-specific
    //     format to its System.Byte equivalent.
    //
    // Parameters:
    //   s:
    //     A string containing a number to convert. The string is interpreted using
    //     the style specified by style.
    //
    //   style:
    //     A bitwise combination of System.Globalization.NumberStyles values that indicates
    //     the permitted format of s. A typical value to specify is System.Globalization.NumberStyles.Integer.
    //
    //   provider:
    //     An System.IFormatProvider that supplies culture-specific parsing information
    //     about s. If provider is null, the thread current culture is used.
    //
    // Returns:
    //     The System.Byte value equivalent to the number contained in s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not of the correct format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.Byte.MinValue or greater than System.Byte.MaxValue.
    //
    //   System.ArgumentException:
    //     style is not a System.Globalization.NumberStyles value. -or-style is not
    //     a combination of System.Globalization.NumberStyles.AllowHexSpecifier and
    //     System.Globalization.NumberStyles.HexNumber values.
    [Pure]
    public static byte Parse(string s, NumberStyles style, IFormatProvider provider)
    {
      Contract.Requires(s != null);
      return default(byte);
    }
    //
    // Summary:
    //     Converts the value of the current System.Byte object to its equivalent string
    //     representation using the specified format.
    //
    // Parameters:
    //   format:
    //     A numeric format string.
    //
    // Returns:
    //     The string representation of the current System.Byte object, formatted as
    //     specified by the format parameter.
    //
    // Exceptions:
    //   System.FormatException:
    //     format includes an unsupported specifier. Supported format specifiers are
    //     listed in the Remarks section.
    [Pure]
    public string ToString(string format)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Tries to convert the string representation of a number to its System.Byte
    //     equivalent, and returns a value that indicates whether the conversion succeeded.
    //
    // Parameters:
    //   s:
    //     A string that contains a number to convert. The string is interpreted using
    //     the System.Globalization.NumberStyles.Integer style.
    //
    //   result:
    //     When this method returns, contains the System.Byte value equivalent to the
    //     number contained in s if the conversion succeeded, or zero if the conversion
    //     failed. This parameter is passed uninitialized.
    //
    // Returns:
    //     true if s was converted successfully; otherwise, false.
    [Pure]
    public static bool TryParse(string s, out byte result)
    {
      result = default(byte);
      return default(bool);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified style and culture-specific
    //     format to its System.Byte equivalent. A return value indicates whether the
    //     conversion succeeded or failed.
    //
    // Parameters:
    //   s:
    //     A string containing a number to convert. The string is interpreted using
    //     the style specified by style.
    //
    //   style:
    //     A bitwise combination of System.Globalization.NumberStyles values that indicates
    //     the permitted format of s. A typical value to specify is System.Globalization.NumberStyles.Integer.
    //
    //   provider:
    //     An System.IFormatProvider object that supplies culture-specific formatting
    //     information about s. If provider is null, the thread current culture is used.
    //
    //   result:
    //     When this method returns, contains the 8-bit unsigned integer value equivalent
    //     to the number contained in s if the conversion succeeded, or zero if the
    //     conversion failed. The conversion fails if the s parameter is null, is not
    //     of the correct format, or represents a number less than System.Byte.MinValue
    //     or greater than System.Byte.MaxValue. This parameter is passed uninitialized.
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
    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out byte result)
    {
      result = default(byte);
      return default(bool);
    }
  }
}


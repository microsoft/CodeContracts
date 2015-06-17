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

using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System
{
#if !SILVERLIGHT_4_0_WP
  // Summary:
  //     Specifies whether relevant Overload:System.Convert.ToBase64CharArray and
  //     Overload:System.Convert.ToBase64String methods insert line breaks in their
  //     output.
  public enum Base64FormattingOptions
  {
    // Summary:
    //     Does not insert line breaks after every 76 characters in the string representation.
    None = 0,
    //
    // Summary:
    //     Inserts line breaks after every 76 characters in the string representation.
    InsertLineBreaks = 1,
  }
#endif

  // Summary:
  //     Converts a base data type to another base data type.
  public static class Convert
  {
#if !SILVERLIGHT
    // Summary:
    //     Returns an System.Object with the specified System.Type and whose value is
    //     equivalent to the specified object.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    //   conversionType:
    //     A System.Type.
    //
    // Returns:
    //     An object whose System.Type is conversionType and whose value is equivalent
    //     to value.  -or- null, if value is null and conversionType is not a value
    //     type.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported. -or- value is null and conversionType is
    //     a value type.
    //
    //   System.ArgumentNullException:
    //     conversionType is null.
    [Pure]
    public static object ChangeType(object value, Type conversionType)
    {
      Contract.Requires(conversionType != null);
      return default(object);
    }
    //
    // Summary:
    //     Returns an System.Object with the specified System.TypeCode and whose value
    //     is equivalent to the specified object.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    //   typeCode:
    //     A System.TypeCode
    //
    // Returns:
    //     An object whose underlying System.TypeCode is typeCode and whose value is
    //     equivalent to value.  -or- null, if value equals null and typeCode equals
    //     System.TypeCode.Empty, System.TypeCode.String, or System.TypeCode.Object.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported. -or- value is null and typeCode specifies
    //     a value type.
    //
    //   System.ArgumentException:
    //     typeCode is invalid.
    [Pure]
    public static object ChangeType(object value, TypeCode typeCode)
    {
      return default(object);
    }
#endif

    //
    // Summary:
    //     Returns an System.Object with the specified System.Type and whose value is
    //     equivalent to the specified object. A parameter supplies culture-specific
    //     formatting information.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    //   conversionType:
    //     A System.Type.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     An object whose System.Type is conversionType and whose value is equivalent
    //     to value.  -or- value, if the System.Type of value and conversionType are
    //     equal.  -or- null, if value is null and conversionType is not a value type.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported. -or- value is null and conversionType is
    //     a value type.
    //
    //   System.ArgumentNullException:
    //     conversionType is null.
    [Pure]
    public static object ChangeType(object value, Type conversionType, IFormatProvider provider)
    {
      Contract.Requires(conversionType != null);
      return default(object);
    }
    //
    // Summary:
    //     Returns an System.Object with the specified System.TypeCode and whose value
    //     is equivalent to the specified object. A parameter supplies culture-specific
    //     formatting information.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    //   typeCode:
    //     A System.TypeCode.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     An object whose underlying System.TypeCode is typeCode and whose value is
    //     equivalent to value.  -or- null, if value equals null and typeCode equals
    //     System.TypeCode.Empty, System.TypeCode.String, or System.TypeCode.Object.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported. -or- value is null and typeCode specifies
    //     a value type.
    //
    //   System.ArgumentException:
    //     typeCode is invalid.
    [Pure]
    extern public static object ChangeType(object value, TypeCode typeCode, IFormatProvider provider);

    //
    // Summary:
    //     Converts a subset of a Unicode character array, which encodes binary data
    //     as base 64 digits, to an equivalent 8-bit unsigned integer array. Parameters
    //     specify the subset in the input array and the number of elements to convert.
    //
    // Parameters:
    //   inArray:
    //     A Unicode character array.
    //
    //   offset:
    //     A position within inArray.
    //
    //   length:
    //     The number of elements in inArray to convert.
    //
    // Returns:
    //     An array of 8-bit unsigned integers equivalent to length elements at position
    //     offset in inArray.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     inArray is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     offset or length is less than 0.  -or- offset plus length indicates a position
    //     not within inArray.
    //
    //   System.FormatException:
    //     The length of inArray, ignoring white space characters, is not zero or a
    //     multiple of 4. -or- The format of inArray is invalid. inArray contains a
    //     non-base 64 character, more than two padding characters, or a non-white space
    //     character among the padding characters.
    [Pure]
    public static byte[] FromBase64CharArray(char[] inArray, int offset, int length)
    {
      Contract.Requires(inArray != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(length >= 0);
      Contract.Requires(offset + length <= inArray.Length);

      Contract.Ensures(Contract.Result<Byte[]>() != null);
      return default(Byte[]);
    }
    //
    // Summary:
    //     Converts the specified System.String, which encodes binary data as base 64
    //     digits, to an equivalent 8-bit unsigned integer array.
    //
    // Parameters:
    //   s:
    //     A System.String.
    //
    // Returns:
    //     An array of 8-bit unsigned integers equivalent to s.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     The length of s, ignoring white space characters, is not zero or a multiple
    //     of 4. -or- The format of s is invalid. s contains a non-base 64 character,
    //     more than two padding characters, or a non-white space character among the
    //     padding characters.
    [Pure]
    public static byte[] FromBase64String(string s)
    {
      Contract.Requires(s != null);
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(Byte[]);
    }
    //
    // Summary:
    //     Returns the System.TypeCode for the specified object.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    // Returns:
    //     The System.TypeCode for value, or System.TypeCode.Empty if value is null.
    [Pure]
    extern public static TypeCode GetTypeCode(object value);
    //
    // Summary:
    //     Returns an indication whether the specified object is of type System.DBNull.
    //
    // Parameters:
    //   value:
    //     An object.
    //
    // Returns:
    //     true if value is of type System.DBNull; otherwise, false.
    [Pure]
    extern public static bool IsDBNull(object value);
    //
    // Summary:
    //     Converts a subset of an 8-bit unsigned integer array to an equivalent subset
    //     of a Unicode character array encoded with base 64 digits. Parameters specify
    //     the subsets as offsets in the input and output arrays, and the number of
    //     elements in the input array to convert.
    //
    // Parameters:
    //   inArray:
    //     An input array of 8-bit unsigned integers.
    //
    //   offsetIn:
    //     A position within inArray.
    //
    //   length:
    //     The number of elements of inArray to convert.
    //
    //   outArray:
    //     An output array of Unicode characters.
    //
    //   offsetOut:
    //     A position within outArray.
    //
    // Returns:
    //     A 32-bit signed integer containing the number of bytes in outArray.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     inArray or outArray is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     offsetIn, offsetOut, or length is negative.  -or- offsetIn plus length is
    //     greater than the length of inArray.  -or- offsetOut plus the number of elements
    //     to return is greater than the length of outArray.
    public static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut)
    {
      Contract.Requires(inArray != null);
      Contract.Requires(outArray != null);
      Contract.Requires(offsetIn >= 0);
      Contract.Requires(offsetOut >= 0);
      Contract.Requires(length >= 0);
      Contract.Requires(offsetIn + length <= inArray.Length);

      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() <= outArray.Length);

      return default(int);
    }

#if !SILVERLIGHT
    //
    // Summary:
    //     Converts a subset of an 8-bit unsigned integer array to an equivalent subset
    //     of a Unicode character array encoded with base 64 digits. Parameters specify
    //     the subsets as offsets in the input and output arrays, the number of elements
    //     in the input array to convert, and whether line breaks are inserted in the
    //     output array.
    //
    // Parameters:
    //   inArray:
    //     An input array of 8-bit unsigned integers.
    //
    //   offsetIn:
    //     A position within inArray.
    //
    //   length:
    //     The number of elements of inArray to convert.
    //
    //   outArray:
    //     An output array of Unicode characters.
    //
    //   offsetOut:
    //     A position within outArray.
    //
    //   options:
    //     System.Base64FormattingOptions.InsertLineBreaks to insert a line break every
    //     76 characters, or System.Base64FormattingOptions.None to not insert line
    //     breaks.
    //
    // Returns:
    //     A 32-bit signed integer containing the number of bytes in outArray.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     inArray or outArray is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     offsetIn, offsetOut, or length is negative.  -or- offsetIn plus length is
    //     greater than the length of inArray.  -or- offsetOut plus the number of elements
    //     to return is greater than the length of outArray.
    //
    //   System.ArgumentException:
    //     options is not a valid System.Base64FormattingOptions value.
    public static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut, Base64FormattingOptions options)
    {
      Contract.Requires(inArray != null);
      Contract.Requires(outArray != null);
      Contract.Requires(offsetIn >= 0);
      Contract.Requires(offsetOut >= 0);
      Contract.Requires(length >= 0);
      Contract.Requires(offsetIn + length <= inArray.Length);

      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() <= outArray.Length);

      return default(int);
    }
#endif

    //
    // Summary:
    //     Converts an array of 8-bit unsigned integers to its equivalent System.String
    //     representation encoded with base 64 digits.
    //
    // Parameters:
    //   inArray:
    //     An array of 8-bit unsigned integers.
    //
    // Returns:
    //     The System.String representation, in base 64, of the contents of inArray.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     inArray is null.
    [Pure]
    public static string ToBase64String(byte[] inArray)
    {
      Contract.Requires(inArray != null);
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

#if !SILVERLIGHT
    //
    // Summary:
    //     Converts an array of 8-bit unsigned integers to its equivalent System.String
    //     representation encoded with base 64 digits. A parameter specifies whether
    //     to insert line breaks in the return value.
    //
    // Parameters:
    //   inArray:
    //     An array of 8-bit unsigned integers.
    //
    //   options:
    //     System.Base64FormattingOptions.InsertLineBreaks to insert a line break every
    //     76 characters, or System.Base64FormattingOptions.None to not insert line
    //     breaks.
    //
    // Returns:
    //     The System.String representation in base 64 of the elements in inArray.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     inArray is null.
    //
    //   System.ArgumentException:
    //     options is not a valid System.Base64FormattingOptions value.
    [Pure]
    public static string ToBase64String(byte[] inArray, Base64FormattingOptions options)
    {
      Contract.Requires(inArray != null);
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
#endif

    //
    // Summary:
    //     Converts a subset of an array of 8-bit unsigned integers to its equivalent
    //     System.String representation encoded with base 64 digits. Parameters specify
    //     the subset as an offset in the input array, and the number of elements in
    //     the array to convert.
    //
    // Parameters:
    //   inArray:
    //     An array of 8-bit unsigned integers.
    //
    //   offset:
    //     An offset in inArray.
    //
    //   length:
    //     The number of elements of inArray to convert.
    //
    // Returns:
    //     The System.String representation in base 64 of length elements of inArray
    //     starting at position offset.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     inArray is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     offset or length is negative.  -or- offset plus length is greater than the
    //     length of inArray.
    [Pure]
    public static string ToBase64String(byte[] inArray, int offset, int length)
    {
      Contract.Requires(inArray != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(length >= 0);
      Contract.Requires(offset + length <= inArray.Length);

      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

#if !SILVERLIGHT
    //
    // Summary:
    //     Converts a subset of an array of 8-bit unsigned integers to its equivalent
    //     System.String representation encoded with base 64 digits. Parameters specify
    //     the subset as an offset in the input array, the number of elements in the
    //     array to convert, and whether to insert line breaks in the return value.
    //
    // Parameters:
    //   inArray:
    //     An array of 8-bit unsigned integers.
    //
    //   offset:
    //     An offset in inArray.
    //
    //   length:
    //     The number of elements of inArray to convert.
    //
    //   options:
    //     System.Base64FormattingOptions.InsertLineBreaks to insert a line break every
    //     76 characters, or System.Base64FormattingOptions.None to not insert line
    //     breaks.
    //
    // Returns:
    //     The System.String representation in base 64 of length elements of inArray
    //     starting at position offset.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     inArray is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     offset or length is negative.  -or- offset plus length is greater than the
    //     length of inArray.
    //
    //   System.ArgumentException:
    //     options is not a valid System.Base64FormattingOptions value.
    [Pure]
    public static string ToBase64String(byte[] inArray, int offset, int length, Base64FormattingOptions options)
    {
      Contract.Requires(inArray != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(length >= 0);
      Contract.Requires(offset + length <= inArray.Length);

      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
#endif

    //
    // Summary:
    //     Returns the specified Boolean value; no actual conversion is performed.
    //
    // Parameters:
    //   value:
    //     A Boolean.
    //
    // Returns:
    //     Parameter value is returned unchanged.
    [Pure]
    extern public static bool ToBoolean(bool value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit unsigned integer to an equivalent
    //     Boolean value.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer.
    //
    // Returns:
    //     true if value is not zero; otherwise, false.
    [Pure]
    extern public static bool ToBoolean(byte value);
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A Unicode character.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static bool ToBoolean(char value);

#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A System.DateTime.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static bool ToBoolean(DateTime value);
#endif

    //
    // Summary:
    //     Converts the value of the specified System.Decimal number to an equivalent
    //     Boolean value.
    //
    // Parameters:
    //   value:
    //     A System.Decimal number.
    //
    // Returns:
    //     true if value is not zero; otherwise, false.
    [Pure]
    extern public static bool ToBoolean(decimal value);
    //
    // Summary:
    //     Converts the value of the specified double-precision floating point number
    //     to an equivalent Boolean value.
    //
    // Parameters:
    //   value:
    //     A double-precision floating point number.
    //
    // Returns:
    //     true if value is not zero; otherwise, false.
    [Pure]
    extern public static bool ToBoolean(double value);
    //
    // Summary:
    //     Converts the value of the specified single-precision floating point number
    //     to an equivalent Boolean value.
    //
    // Parameters:
    //   value:
    //     A single-precision floating point number.
    //
    // Returns:
    //     true if value is not zero; otherwise, false.
    [Pure]
    extern public static bool ToBoolean(float value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit signed integer to an equivalent
    //     Boolean value.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    //
    // Returns:
    //     true if value is not zero; otherwise, false.
    [Pure]
    extern public static bool ToBoolean(int value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit signed integer to an equivalent
    //     Boolean value.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    // Returns:
    //     true if value is not zero; otherwise, false.
    [Pure]
    extern public static bool ToBoolean(long value);
    //
    // Summary:
    //     Converts the value of a specified System.Object to an equivalent Boolean
    //     value.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface or null.
    //
    // Returns:
    //     true or false, which reflects the value returned by invoking the System.IConvertible.ToBoolean(System.IFormatProvider)
    //     method for the underlying type of value. If value is null, the method returns
    //     false.
    [Pure]
    extern public static bool ToBoolean(object value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit signed integer to an equivalent
    //     Boolean value.
    //
    // Parameters:
    //   value:
    //     An 8-bit signed integer.
    //
    // Returns:
    //     true if value is not zero; otherwise, false.
    [Pure]
    extern public static bool ToBoolean(sbyte value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit signed integer to an equivalent
    //     Boolean value.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer.
    //
    // Returns:
    //     true if value is not zero; otherwise, false.
    [Pure]
    extern public static bool ToBoolean(short value);
    //
    // Summary:
    //     Converts the specified System.String representation of a logical value to
    //     its Boolean equivalent.
    //
    // Parameters:
    //   value:
    //     A System.String that contains the value of either System.Boolean.TrueString
    //     or System.Boolean.FalseString.
    //
    // Returns:
    //     true if value equals System.Boolean.TrueString, or false if value equals
    //     System.Boolean.FalseString or null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value is not equal to System.Boolean.TrueString or System.Boolean.FalseString.
    [Pure]
    extern public static bool ToBoolean(string value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit unsigned integer to an equivalent
    //     Boolean value.
    //
    // Parameters:
    //   value:
    //     A 32-bit unsigned integer.
    //
    // Returns:
    //     true if value is not zero; otherwise, false.
    [Pure]
    extern public static bool ToBoolean(uint value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit unsigned integer to an equivalent
    //     Boolean value.
    //
    // Parameters:
    //   value:
    //     A 64-bit unsigned integer.
    //
    // Returns:
    //     true if value is not zero; otherwise, false.
    [Pure]
    extern public static bool ToBoolean(ulong value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit unsigned integer to an equivalent
    //     Boolean value.
    //
    // Parameters:
    //   value:
    //     A 16-bit unsigned integer.
    //
    // Returns:
    //     true if value is not zero; otherwise, false.
    [Pure]
    extern public static bool ToBoolean(ushort value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to an equivalent Boolean
    //     value using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface or null.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     true or false, which reflects the value returned by invoking the System.IConvertible.ToBoolean(System.IFormatProvider)
    //     method for the underlying type of value. If value is null, the method returns
    //     false.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement the System.IConvertible interface.
    [Pure]
    extern public static bool ToBoolean(object value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the specified System.String representation of a logical value to
    //     its Boolean equivalent using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     A string that contains the value of either System.Boolean.TrueString or System.Boolean.FalseString.
    //
    //   provider:
    //     (Reserved) An System.IFormatProvider interface implementation that supplies
    //     culture-specific formatting information.
    //
    // Returns:
    //     true if value equals System.Boolean.TrueString, or false if value equals
    //     System.Boolean.FalseString or null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value is not equal to System.Boolean.TrueString or System.Boolean.FalseString.
    [Pure]
    extern public static bool ToBoolean(string value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of the specified Boolean value to the equivalent 8-bit
    //     unsigned integer.
    //
    // Parameters:
    //   value:
    //     A Boolean value.
    //
    // Returns:
    //     The number 1 if value is true; otherwise, 0.
    [Pure]
    extern public static byte ToByte(bool value);
    //
    // Summary:
    //     Returns the specified 8-bit unsigned integer; no actual conversion is performed.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer.
    //
    // Returns:
    //     Parameter value is returned unchanged.
    [Pure]
    extern public static byte ToByte(byte value);
    //
    // Summary:
    //     Converts the value of the specified Unicode character to the equivalent 8-bit
    //     unsigned integer.
    //
    // Parameters:
    //   value:
    //     A Unicode character.
    //
    // Returns:
    //     The 8-bit unsigned integer equivalent to value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Byte.MaxValue.
    [Pure]
    extern public static byte ToByte(char value);

#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A System.DateTime.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static byte ToByte(DateTime value);
#endif
    //
    // Summary:
    //     Converts the value of the specified System.Decimal number to an equivalent
    //     8-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A System.Decimal number.
    //
    // Returns:
    //     value rounded to the nearest 8-bit signed integer. If value is halfway between
    //     two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Byte.MaxValue or less than System.Byte.MinValue.
    [Pure]
    extern public static byte ToByte(decimal value);
    //
    // Summary:
    //     Converts the value of the specified double-precision floating point number
    //     to an equivalent 8-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A double-precision floating point number.
    //
    // Returns:
    //     value rounded to the nearest 8-bit signed integer. If value is halfway between
    //     two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Byte.MaxValue or less than System.Byte.MinValue.
    [Pure]
    extern public static byte ToByte(double value);
    //
    // Summary:
    //     Converts the value of the specified single-precision floating point number
    //     to an equivalent 8-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A single-precision floating point number.
    //
    // Returns:
    //     value rounded to the nearest 8-bit signed integer. If value is halfway between
    //     two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Byte.MaxValue or less than System.Byte.MinValue.
    [Pure]
    extern public static byte ToByte(float value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit signed integer to an equivalent
    //     8-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    //
    // Returns:
    //     An 8-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.Byte.MinValue or greater than System.Byte.MaxValue.
    [Pure]
    extern public static byte ToByte(int value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit signed integer to an equivalent
    //     8-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    // Returns:
    //     An 8-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.Byte.MinValue or greater than System.Byte.MaxValue.
    [Pure]
    extern public static byte ToByte(long value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to an 8-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface or null.
    //
    // Returns:
    //     An 8-bit unsigned integer equivalent to the value of value, or zero if value
    //     is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static byte ToByte(object value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit signed integer to an equivalent
    //     8-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     An 8-bit signed integer.
    //
    // Returns:
    //     An 8-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.Byte.MinValue.
    [Pure]
    extern public static byte ToByte(sbyte value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit signed integer to an equivalent
    //     8-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer.
    //
    // Returns:
    //     An 8-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.Byte.MinValue or greater than System.Byte.MaxValue.
    [Pure]
    extern public static byte ToByte(short value);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     8-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    // Returns:
    //     An 8-bit unsigned integer equivalent to the value of value.  -or- Zero if
    //     value is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value does not consist of an optional sign followed by a sequence of digits
    //     (zero through nine).
    //
    //   System.OverflowException:
    //     value represents a number less than System.Byte.MinValue or greater than
    //     System.Byte.MaxValue.
    [Pure]
    extern public static byte ToByte(string value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit unsigned integer to an equivalent
    //     8-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 32-bit unsigned integer.
    //
    // Returns:
    //     An 8-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Byte.MaxValue.
    [Pure]
    extern public static byte ToByte(uint value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit unsigned integer to an equivalent
    //     8-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 64-bit unsigned integer.
    //
    // Returns:
    //     An 8-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Byte.MaxValue.
    [Pure]
    extern public static byte ToByte(ulong value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit unsigned integer to an equivalent
    //     8-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 16-bit unsigned integer.
    //
    // Returns:
    //     An 8-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Byte.MaxValue.
    [Pure]
    extern public static byte ToByte(ushort value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to an 8-bit unsigned integer
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     An 8-bit unsigned integer equivalent to the value of value, or zero if value
    //     is null.
    [Pure]
    extern public static byte ToByte(object value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     8-bit signed integer using specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     An 8-bit signed integer equivalent to the value of value.  -or- Zero if value
    //     is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value does not consist of an optional sign followed by a sequence of digits
    //     (zero through nine).
    //
    //   System.OverflowException:
    //     value represents a number less than System.Byte.MinValue or greater than
    //     System.Byte.MaxValue.
    [Pure]
    extern public static byte ToByte(string value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the string representation of a number in a specified base to an
    //     equivalent 8-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number.
    //
    //   fromBase:
    //     The base of the number in value, which must be 2, 8, 10, or 16.
    //
    // Returns:
    //     An 8-bit unsigned integer equivalent to the number in value.  -or- Zero if
    //     value is null.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     fromBase is not 2, 8, 10, or 16. -or- value, which represents a non-base
    //     10 unsigned number, is prefixed with a negative sign.
    //
    //   System.FormatException:
    //     value contains a character that is not a valid digit in the base specified
    //     by fromBase. The exception message indicates that there are no digits to
    //     convert if the first character in value is invalid; otherwise, the message
    //     indicates that value contains invalid trailing characters.
    //
    //   System.OverflowException:
    //     value, which represents a base 10 unsigned number, is prefixed with a negative
    //     sign.  -or- The return value is less than System.Byte.MinValue or larger
    //     than System.Byte.MaxValue.
    [Pure]
    public static byte ToByte(string value, int fromBase)
    {
      Contract.Requires(fromBase == 2 || fromBase == 8 || fromBase == 10 || fromBase == 16);
      return default(byte);
    }

#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A System.Boolean value.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static char ToChar(bool value);
#endif

    //
    // Summary:
    //     Converts the value of the specified 8-bit unsigned integer to its equivalent
    //     Unicode character.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer.
    //
    // Returns:
    //     The Unicode character equivalent to the value of value.
    [Pure]
    extern public static char ToChar(byte value);
    //
    // Summary:
    //     Returns the specified Unicode character value; no actual conversion is performed.
    //
    // Parameters:
    //   value:
    //     A Unicode character.
    //
    // Returns:
    //     Parameter value is returned unchanged.
    [Pure]
    extern public static char ToChar(char value);

#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A System.DateTime.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static char ToChar(DateTime value);
#endif

    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A System.Decimal number.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static char ToChar(decimal value);
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A double-precision floating-point number.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static char ToChar(double value);
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A single-precision floating-point number.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static char ToChar(float value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit signed integer to its equivalent
    //     Unicode character.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    //
    // Returns:
    //     The Unicode character equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.Char.MinValue or greater than System.Char.MaxValue.
    [Pure]
    extern public static char ToChar(int value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit signed integer to its equivalent
    //     Unicode character.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    // Returns:
    //     The Unicode character equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.Char.MinValue or greater than System.Char.MaxValue.
    [Pure]
    extern public static char ToChar(long value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to a Unicode character.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    // Returns:
    //     The Unicode character equivalent to the value of value.  -or- System.Char.MinValue
    //     if value equals null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement the System.IConvertible interface.
    [Pure]
    extern public static char ToChar(object value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit signed integer to its equivalent
    //     Unicode character.
    //
    // Parameters:
    //   value:
    //     An 8-bit signed integer.
    //
    // Returns:
    //     The Unicode character equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.Char.MinValue.
    [Pure]
    extern public static char ToChar(sbyte value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit signed integer to its equivalent
    //     Unicode character.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer.
    //
    // Returns:
    //     The Unicode character equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.Char.MinValue.
    [Pure]
    extern public static char ToChar(short value);
    //
    // Summary:
    //     Converts the first character of a System.String to a Unicode character.
    //
    // Parameters:
    //   value:
    //     A System.String of length 1 or null.
    //
    // Returns:
    //     The Unicode character equivalent to the first and only character in value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     value is null.
    //
    //   System.FormatException:
    //     The length of value is not 1.
    [Pure]
    extern public static char ToChar(string value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit unsigned integer to its equivalent
    //     Unicode character.
    //
    // Parameters:
    //   value:
    //     A 32-bit unsigned integer.
    //
    // Returns:
    //     The Unicode character equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Char.MaxValue.
    [Pure]
    extern public static char ToChar(uint value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit unsigned integer to its equivalent
    //     Unicode character.
    //
    // Parameters:
    //   value:
    //     A 64-bit unsigned integer.
    //
    // Returns:
    //     The Unicode character equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Char.MaxValue.
    [Pure]
    extern public static char ToChar(ulong value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit unsigned integer to its equivalent
    //     Unicode character.
    //
    // Parameters:
    //   value:
    //     A 16-bit unsigned integer.
    //
    // Returns:
    //     The Unicode character equivalent to the value of value.
    [Pure]
    extern public static char ToChar(ushort value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to its equivalent Unicode
    //     character using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     The Unicode character equivalent to the value of value.  -or- System.Char.MinValue
    //     if value equals null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement the System.IConvertible interface.
    [Pure]
    extern public static char ToChar(object value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the first character of a System.String to a Unicode character using
    //     specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     A System.String of length 1 or null.
    //
    //   provider:
    //     (Reserved) An System.IFormatProvider interface implementation that supplies
    //     culture-specific formatting information.
    //
    // Returns:
    //     The Unicode character equivalent to the first and only character in value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     value is null.
    //
    //   System.FormatException:
    //     The length of value is not 1.
    [Pure]
    public static char ToChar(string value, IFormatProvider provider)
    {
      Contract.Requires(value != null);
      return default(char);
    }

#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A Boolean value.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static DateTime ToDateTime(bool value);
#endif
#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static DateTime ToDateTime(byte value);
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A Unicode character.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static DateTime ToDateTime(char value);
#endif

#if !SILVERLIGHT
    //
    // Summary:
    //     Returns the specified System.DateTime; no actual conversion is performed.
    //
    // Parameters:
    //   value:
    //     A System.DateTime.
    //
    // Returns:
    //     Parameter value is returned unchanged.
    [Pure]
    extern public static DateTime ToDateTime(DateTime value);

    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A System.Decimal value.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static DateTime ToDateTime(decimal value);

    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A double-precision floating point value.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static DateTime ToDateTime(double value);

    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A single-precision floating point value.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static DateTime ToDateTime(float value);

    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static DateTime ToDateTime(int value);

    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static DateTime ToDateTime(long value);
#endif

    //
    // Summary:
    //     Converts the value of the specified System.Object to a System.DateTime.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface or null.
    //
    // Returns:
    //     A System.DateTime equivalent to the value of value.  -or- A System.DateTime
    //     equivalent to System.DateTime.MinValue if value is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value is not a valid System.DateTime value.
    //
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible. -or- The conversion is not
    //     supported.
    [Pure]
    extern public static DateTime ToDateTime(object value);

#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     An 8-bit signed integer.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static DateTime ToDateTime(sbyte value);

    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static DateTime ToDateTime(short value);
#endif

    //
    // Summary:
    //     Converts the specified System.String representation of a date and time to
    //     an equivalent System.DateTime.
    //
    // Parameters:
    //   value:
    //     The string representation of a date and time.
    //
    // Returns:
    //     A System.DateTime equivalent to the value of value.  -or- A System.DateTime
    //     equivalent to System.DateTime.MinValue if value is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value is not a properly formatted date and time string.
    [Pure]
    extern public static DateTime ToDateTime(string value);

#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A 32-bit unsigned integer.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static DateTime ToDateTime(uint value);

    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A 64-bit unsigned integer.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static DateTime ToDateTime(ulong value);
#endif

#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A 16-bit unsigned integer.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static DateTime ToDateTime(ushort value);
#endif

    //
    // Summary:
    //     Converts the value of the specified System.Object to a System.DateTime using
    //     the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A System.DateTime equivalent to the value of value.  -or- A System.DateTime
    //     equivalent to System.DateTime.MinValue if value is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static DateTime ToDateTime(object value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     System.DateTime using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     A System.String containing a date and time to convert.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A System.DateTime equivalent to the value of value.  -or- A System.DateTime
    //     equivalent to System.DateTime.MinValue if value is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value is not a properly formatted date and time string.
    [Pure]
    extern public static DateTime ToDateTime(string value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of the specified Boolean value to the equivalent System.Decimal
    //     number.
    //
    // Parameters:
    //   value:
    //     A Boolean value.
    //
    // Returns:
    //     The number 1 if value is true; otherwise, 0.
    [Pure]
    extern public static decimal ToDecimal(bool value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit unsigned integer to the equivalent
    //     System.Decimal number.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer.
    //
    // Returns:
    //     The System.Decimal number equivalent to the value of value.
    [Pure]
    extern public static decimal ToDecimal(byte value);
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A Unicode character.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static decimal ToDecimal(char value);


    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A System.DateTime.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static decimal ToDecimal(DateTime value);


    //
    // Summary:
    //     Returns the specified System.Decimal number; no actual conversion is performed.
    //
    // Parameters:
    //   value:
    //     A System.Decimal number.
    //
    // Returns:
    //     Parameter value is returned unchanged.
    [Pure]
    extern public static decimal ToDecimal(decimal value);
    //
    // Summary:
    //     Converts the value of the specified double-precision floating point number
    //     to an equivalent System.Decimal number.
    //
    // Parameters:
    //   value:
    //     A double-precision floating point number.
    //
    // Returns:
    //     A System.Decimal number equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     The numeric value of value is greater than System.Decimal.MaxValue or less
    //     than System.Decimal.MinValue.
    [Pure]
    extern public static decimal ToDecimal(double value);
    //
    // Summary:
    //     Converts the value of the specified single-precision floating point number
    //     to the equivalent System.Decimal number.
    //
    // Parameters:
    //   value:
    //     A single-precision floating point number.
    //
    // Returns:
    //     A System.Decimal number equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Decimal.MaxValue or less than System.Decimal.MinValue.
    [Pure]
    extern public static decimal ToDecimal(float value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit signed integer to an equivalent
    //     System.Decimal number.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    //
    // Returns:
    //     A System.Decimal number equivalent to the value of value.
    [Pure]
    extern public static decimal ToDecimal(int value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit signed integer to an equivalent
    //     System.Decimal number.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    // Returns:
    //     A System.Decimal number equivalent to the value of value.
    [Pure]
    extern public static decimal ToDecimal(long value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to a System.Decimal number.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface or null.
    //
    // Returns:
    //     A System.Decimal number equivalent to the value of value, or zero if value
    //     is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static decimal ToDecimal(object value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit signed integer to the equivalent
    //     System.Decimal number.
    //
    // Parameters:
    //   value:
    //     An 8-bit signed integer.
    //
    // Returns:
    //     The 8-bit signed integer equivalent to the value of value.
    [Pure]
    extern public static decimal ToDecimal(sbyte value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit signed integer to an equivalent
    //     System.Decimal number.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer.
    //
    // Returns:
    //     A System.Decimal number equivalent to the value of value.
    [Pure]
    extern public static decimal ToDecimal(short value);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     System.Decimal number.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    // Returns:
    //     A System.Decimal number equivalent to the value of value.  -or- Zero if value
    //     is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value is not a number in a valid format.
    //
    //   System.OverflowException:
    //     value represents a number less than System.Decimal.MinValue or greater than
    //     System.Decimal.MaxValue.
    [Pure]
    extern public static decimal ToDecimal(string value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit unsigned integer to an equivalent
    //     System.Decimal number.
    //
    // Parameters:
    //   value:
    //     A 32-bit unsigned integer.
    //
    // Returns:
    //     A System.Decimal number equivalent to the value of value.
    [Pure]
    extern public static decimal ToDecimal(uint value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit unsigned integer to an equivalent
    //     System.Decimal number.
    //
    // Parameters:
    //   value:
    //     A 64-bit unsigned integer.
    //
    // Returns:
    //     A System.Decimal number equivalent to the value of value.
    [Pure]
    extern public static decimal ToDecimal(ulong value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit unsigned integer to the equivalent
    //     System.Decimal number.
    //
    // Parameters:
    //   value:
    //     A 16-bit unsigned integer.
    //
    // Returns:
    //     The System.Decimal number equivalent to the value of value.
    [Pure]
    extern public static decimal ToDecimal(ushort value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to an System.Decimal number
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A System.Decimal number equivalent to the value of value, or zero if value
    //     is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static decimal ToDecimal(object value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     System.Decimal number using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A System.Decimal number equivalent to the value of value.  -or- Zero if value
    //     is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value is not a number in a valid format.
    //
    //   System.OverflowException:
    //     value represents a number less than System.Decimal.MinValue or greater than
    //     System.Decimal.MaxValue.
    [Pure]
    extern public static decimal ToDecimal(string value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of the specified Boolean value to the equivalent double-precision
    //     floating point number.
    //
    // Parameters:
    //   value:
    //     A Boolean value.
    //
    // Returns:
    //     The number 1 if value is true; otherwise, 0.
    [Pure]
    extern public static double ToDouble(bool value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit unsigned integer to the equivalent
    //     double-precision floating point number.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer.
    //
    // Returns:
    //     The double-precision floating point number equivalent to the value of value.
    [Pure]
    extern public static double ToDouble(byte value);
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A Unicode character.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static double ToDouble(char value);

#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A System.DateTime.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static double ToDouble(DateTime value);
#endif

    //
    // Summary:
    //     Converts the value of the specified System.Decimal number to an equivalent
    //     double-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A System.Decimal number.
    //
    // Returns:
    //     A double-precision floating point number equivalent to the value of value.
    [Pure]
    extern public static double ToDouble(decimal value);
    //
    // Summary:
    //     Returns the specified double-precision floating point number; no actual conversion
    //     is performed.
    //
    // Parameters:
    //   value:
    //     A double-precision floating point number.
    //
    // Returns:
    //     Parameter value is returned unchanged.
    [Pure]
    extern public static double ToDouble(double value);
    //
    // Summary:
    //     Converts the value of the specified single-precision floating point number
    //     to an equivalent double-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A single-precision floating point number.
    //
    // Returns:
    //     A double-precision floating point number equivalent to the value of value.
    [Pure]
    extern public static double ToDouble(float value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit signed integer to an equivalent
    //     double-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    //
    // Returns:
    //     A double-precision floating point number equivalent to the value of value.
    [Pure]
    extern public static double ToDouble(int value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit signed integer to an equivalent
    //     double-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    // Returns:
    //     A double-precision floating point number equivalent to the value of value.
    [Pure]
    extern public static double ToDouble(long value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to a double-precision floating
    //     point number.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface or null.
    //
    // Returns:
    //     A double-precision floating point number equivalent to the value of value,
    //     or zero if value is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static double ToDouble(object value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit signed integer to the equivalent
    //     double-precision floating point number.
    //
    // Parameters:
    //   value:
    //     An 8-bit signed integer.
    //
    // Returns:
    //     The 8-bit signed integer equivalent to the value of value.
    [Pure]
    extern public static double ToDouble(sbyte value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit signed integer to an equivalent
    //     double-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer.
    //
    // Returns:
    //     A double-precision floating point number equivalent to the value of value.
    [Pure]
    extern public static double ToDouble(short value);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     double-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    // Returns:
    //     A double-precision floating point number equivalent to the value of value.
    //      -or- Zero if value is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value is not a number in a valid format.
    //
    //   System.OverflowException:
    //     value represents a number less than System.Double.MinValue or greater than
    //     System.Double.MaxValue.
    [Pure]
    extern public static double ToDouble(string value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit unsigned integer to an equivalent
    //     double-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A 32-bit unsigned integer.
    //
    // Returns:
    //     A double-precision floating point number equivalent to the value of value.
    [Pure]
    extern public static double ToDouble(uint value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit unsigned integer to an equivalent
    //     double-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A 64-bit unsigned integer.
    //
    // Returns:
    //     A double-precision floating point number equivalent to the value of value.
    [Pure]
    extern public static double ToDouble(ulong value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit unsigned integer to the equivalent
    //     double-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A 16-bit unsigned integer.
    //
    // Returns:
    //     The double-precision floating point number equivalent to the value of value.
    [Pure]
    extern public static double ToDouble(ushort value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to an double-precision
    //     floating point number using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A double-precision floating point number equivalent to the value of value,
    //     or zero if value is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static double ToDouble(object value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     double-precision floating point number using the specified culture-specific
    //     formatting information.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A double-precision floating point number equivalent to the value of value.
    //      -or- Zero if value is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value is not a number in a valid format.
    //
    //   System.OverflowException:
    //     value represents a number less than System.Double.MinValue or greater than
    //     System.Double.MaxValue.
    [Pure]
    extern public static double ToDouble(string value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of the specified Boolean value to the equivalent 16-bit
    //     signed integer.
    //
    // Parameters:
    //   value:
    //     A Boolean value.
    //
    // Returns:
    //     The number 1 if value is true; otherwise, 0.
    [Pure]
    extern public static short ToInt16(bool value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit unsigned integer to the equivalent
    //     16-bit signed integer.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer.
    //
    // Returns:
    //     The 16-bit signed integer equivalent to the value of value.
    [Pure]
    extern public static short ToInt16(byte value);
    //
    // Summary:
    //     Converts the value of the specified Unicode character to the equivalent 16-bit
    //     signed integer.
    //
    // Parameters:
    //   value:
    //     A Unicode character.
    //
    // Returns:
    //     The 16-bit signed integer equivalent to value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int16.MaxValue.
    [Pure]
    extern public static short ToInt16(char value);

#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A System.DateTime.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static short ToInt16(DateTime value);
#endif

    //
    // Summary:
    //     Converts the value of the specified System.Decimal number to an equivalent
    //     16-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A System.Decimal number.
    //
    // Returns:
    //     value rounded to the nearest 16-bit signed integer. If value is halfway between
    //     two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int16.MaxValue or less than System.Int16.MinValue.
    [Pure]
    extern public static short ToInt16(decimal value);
    //
    // Summary:
    //     Converts the value of the specified double-precision floating point number
    //     to an equivalent 16-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A double-precision floating point number.
    //
    // Returns:
    //     value rounded to the nearest 16-bit signed integer. If value is halfway between
    //     two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int16.MaxValue or less than System.Int16.MinValue.
    [Pure]
    extern public static short ToInt16(double value);
    //
    // Summary:
    //     Converts the value of the specified single-precision floating point number
    //     to an equivalent 16-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A single-precision floating point number.
    //
    // Returns:
    //     value rounded to the nearest 16-bit signed integer. If value is halfway between
    //     two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int16.MaxValue or less than System.Int16.MinValue.
    [Pure]
    extern public static short ToInt16(float value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit signed integer to an equivalent
    //     16-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    //
    // Returns:
    //     The 16-bit signed integer equivalent of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int16.MaxValue or less than System.Int16.MinValue.
    [Pure]
    extern public static short ToInt16(int value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit signed integer to an equivalent
    //     16-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    // Returns:
    //     A 16-bit signed integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int16.MaxValue or less than System.Int16.MinValue.
    [Pure]
    extern public static short ToInt16(long value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to a 16-bit signed integer.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface or null.
    //
    // Returns:
    //     A 16-bit signed integer equivalent to the value of value, or zero if value
    //     is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static short ToInt16(object value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit signed integer to the equivalent
    //     16-bit signed integer.
    //
    // Parameters:
    //   value:
    //     An 8-bit signed integer.
    //
    // Returns:
    //     The 8-bit signed integer equivalent to the value of value.
    [Pure]
    extern public static short ToInt16(sbyte value);
    //
    // Summary:
    //     Returns the specified 16-bit signed integer; no actual conversion is performed.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer.
    //
    // Returns:
    //     Parameter value is returned unchanged.
    [Pure]
    extern public static short ToInt16(short value);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     16-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    // Returns:
    //     A 16-bit signed integer equivalent to the value of value.  -or- Zero if value
    //     is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value does not consist of an optional sign followed by a sequence of digits
    //     (zero through nine).
    //
    //   System.OverflowException:
    //     value represents a number less than System.Int16.MinValue or greater than
    //     System.Int16.MaxValue.
    [Pure]
    extern public static short ToInt16(string value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit unsigned integer to an equivalent
    //     16-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 32-bit unsigned integer.
    //
    // Returns:
    //     A 16-bit signed integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int16.MaxValue.
    [Pure]
    extern public static short ToInt16(uint value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit unsigned integer to an equivalent
    //     16-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 64-bit unsigned integer.
    //
    // Returns:
    //     A 16-bit signed integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int16.MaxValue.
    [Pure]
    extern public static short ToInt16(ulong value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit unsigned integer to the equivalent
    //     16-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 16-bit unsigned integer.
    //
    // Returns:
    //     The 16-bit signed integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int16.MaxValue.
    [Pure]
    extern public static short ToInt16(ushort value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to a 16-bit signed integer
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A 16-bit signed integer equivalent to the value of value, or zero if value
    //     is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static short ToInt16(object value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     16-bit signed integer using specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A 16-bit signed integer equivalent to the value of value.  -or- Zero if value
    //     is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value does not consist of an optional sign followed by a sequence of digits
    //     (zero through nine).
    //
    //   System.OverflowException:
    //     value represents a number less than System.Int16.MinValue or greater than
    //     System.Int16.MaxValue.
    [Pure]
    extern public static short ToInt16(string value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the string representation of a number in a specified base to an
    //     equivalent 16-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A string containing a number.
    //
    //   fromBase:
    //     The base of the number in value, which must be 2, 8, 10, or 16.
    //
    // Returns:
    //     A 16-bit signed integer equivalent to the number in value.  -or- Zero if
    //     value is null.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     fromBase is not 2, 8, 10, or 16. -or- value, which represents a non-base
    //     10 signed number, is prefixed with a negative sign.
    //
    //   System.FormatException:
    //     value contains a character that is not a valid digit in the base specified
    //     by fromBase. The exception message indicates that there are no digits to
    //     convert if the first character in value is invalid; otherwise, the message
    //     indicates that value contains invalid trailing characters.
    //
    //   System.OverflowException:
    //     value, which represents a non-base 10 signed number, is prefixed with a negative
    //     sign.  -or- The return value is less than System.Int16.MinValue or larger
    //     than System.Int16.MaxValue.
    [Pure]
    public static short ToInt16(string value, int fromBase)
    {
      Contract.Requires(fromBase == 2 || fromBase == 8 || fromBase == 10 || fromBase == 16);
      return default(short);
    }
    //
    // Summary:
    //     Converts the value of the specified Boolean value to the equivalent 32-bit
    //     signed integer.
    //
    // Parameters:
    //   value:
    //     A Boolean value.
    //
    // Returns:
    //     The number 1 if value is true; otherwise, 0.
    [Pure]
    extern public static int ToInt32(bool value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit unsigned integer to the equivalent
    //     32-bit signed integer.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer.
    //
    // Returns:
    //     The 32-bit signed integer equivalent to the value of value.
    [Pure]
    extern public static int ToInt32(byte value);
    //
    // Summary:
    //     Converts the value of the specified Unicode character to the equivalent 32-bit
    //     signed integer.
    //
    // Parameters:
    //   value:
    //     A Unicode character.
    //
    // Returns:
    //     The 32-bit signed integer equivalent to value.
    [Pure]
    extern public static int ToInt32(char value);

#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A System.DateTime.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static int ToInt32(DateTime value);
#endif

    //
    // Summary:
    //     Converts the value of the specified System.Decimal number to an equivalent
    //     32-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A System.Decimal number.
    //
    // Returns:
    //     value rounded to the nearest 32-bit signed integer. If value is halfway between
    //     two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int32.MaxValue or less than System.Int32.MinValue.
    [Pure]
    extern public static int ToInt32(decimal value);
    //
    // Summary:
    //     Converts the value of the specified double-precision floating point number
    //     to an equivalent 32-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A double-precision floating point number.
    //
    // Returns:
    //     value rounded to the nearest 32-bit signed integer. If value is halfway between
    //     two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int32.MaxValue or less than System.Int32.MinValue.
    [Pure]
    extern public static int ToInt32(double value);
    //
    // Summary:
    //     Converts the value of the specified single-precision floating point number
    //     to an equivalent 32-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A single-precision floating point number.
    //
    // Returns:
    //     value rounded to the nearest 32-bit signed integer. If value is halfway between
    //     two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int32.MaxValue or less than System.Int32.MinValue.
    [Pure]
    extern public static int ToInt32(float value);
    //
    // Summary:
    //     Returns the specified 32-bit signed integer; no actual conversion is performed.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    //
    // Returns:
    //     Parameter value is returned unchanged.
    [Pure]
    extern public static int ToInt32(int value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit signed integer to an equivalent
    //     32-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    // Returns:
    //     A 32-bit signed integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int32.MaxValue or less than System.Int32.MinValue.
    [Pure]
    extern public static int ToInt32(long value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to a 32-bit signed integer.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface or null.
    //
    // Returns:
    //     A 32-bit signed integer equivalent to the value of value, or zero if value
    //     is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static int ToInt32(object value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit signed integer to the equivalent
    //     32-bit signed integer.
    //
    // Parameters:
    //   value:
    //     An 8-bit signed integer.
    //
    // Returns:
    //     The 8-bit signed integer equivalent to the value of value.
    [Pure]
    extern public static int ToInt32(sbyte value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit signed integer to an equivalent
    //     32-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer.
    //
    // Returns:
    //     A 32-bit signed integer equivalent to the value of value.
    [Pure]
    extern public static int ToInt32(short value);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     32-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    // Returns:
    //     A 32-bit signed integer equivalent to the value of value.  -or- Zero if value
    //     is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value does not consist of an optional sign followed by a sequence of digits
    //     (zero through nine).
    //
    //   System.OverflowException:
    //     value represents a number less than System.Int32.MinValue or greater than
    //     System.Int32.MaxValue.
    [Pure]
    extern public static int ToInt32(string value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit unsigned integer to an equivalent
    //     32-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 32-bit unsigned integer.
    //
    // Returns:
    //     A 32-bit signed integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int32.MaxValue.
    [Pure]
    extern public static int ToInt32(uint value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit unsigned integer to an equivalent
    //     32-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 64-bit unsigned integer.
    //
    // Returns:
    //     A 32-bit signed integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int32.MaxValue.
    [Pure]
    extern public static int ToInt32(ulong value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit unsigned integer to the equivalent
    //     32-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 16-bit unsigned integer.
    //
    // Returns:
    //     The 32-bit signed integer equivalent to the value of value.
    [Pure]
    extern public static int ToInt32(ushort value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to a 32-bit signed integer
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A 32-bit signed integer equivalent to the value of value, or zero if value
    //     is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static int ToInt32(object value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     32-bit signed integer using specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A 32-bit signed integer equivalent to the value of value.  -or- Zero if value
    //     is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value does not consist of an optional sign followed by a sequence of digits
    //     (zero through nine).
    //
    //   System.OverflowException:
    //     value represents a number less than System.Int32.MinValue or greater than
    //     System.Int32.MaxValue.
    [Pure]
    extern public static int ToInt32(string value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the System.String representation of a number in a specified base
    //     to an equivalent 32-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number.
    //
    //   fromBase:
    //     The base of the number in value, which must be 2, 8, 10, or 16.
    //
    // Returns:
    //     A 32-bit signed integer equivalent to the number in value.  -or- Zero if
    //     value is null.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     fromBase is not 2, 8, 10, or 16. -or- value, which represents a non-base
    //     10 signed number, is prefixed with a negative sign.
    //
    //   System.FormatException:
    //     value contains a character that is not a valid digit in the base specified
    //     by fromBase. The exception message indicates that there are no digits to
    //     convert if the first character in value is invalid; otherwise, the message
    //     indicates that value contains invalid trailing characters.
    //
    //   System.OverflowException:
    //     value, which represents a non-base 10 signed number, is prefixed with a negative
    //     sign.  -or- The return value is less than System.Int32.MinValue or larger
    //     than System.Int32.MaxValue.
    [Pure]
    public static int ToInt32(string value, int fromBase)
    {
      Contract.Requires(fromBase == 2 || fromBase == 8 || fromBase == 10 || fromBase == 16);
      return default(int);
    }
    //
    // Summary:
    //     Converts the value of the specified Boolean value to the equivalent 64-bit
    //     signed integer.
    //
    // Parameters:
    //   value:
    //     A Boolean value.
    //
    // Returns:
    //     The number 1 if value is true; otherwise, 0.
    [Pure]
    extern public static long ToInt64(bool value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit unsigned integer to the equivalent
    //     64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer.
    //
    // Returns:
    //     The 64-bit signed integer equivalent to the value of value.
    [Pure]
    extern public static long ToInt64(byte value);
    //
    // Summary:
    //     Converts the value of the specified Unicode character to the equivalent 64-bit
    //     signed integer.
    //
    // Parameters:
    //   value:
    //     A Unicode character.
    //
    // Returns:
    //     The 64-bit signed integer equivalent to value.
    [Pure]
    extern public static long ToInt64(char value);

#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A System.DateTime.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static long ToInt64(DateTime value);
#endif

    //
    // Summary:
    //     Converts the value of the specified System.Decimal number to an equivalent
    //     64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A System.Decimal number.
    //
    // Returns:
    //     value rounded to the nearest 64-bit signed integer. If value is halfway between
    //     two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int64.MaxValue or less than System.Int64.MinValue.
    [Pure]
    extern public static long ToInt64(decimal value);
    //
    // Summary:
    //     Converts the value of the specified double-precision floating point number
    //     to an equivalent 64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A double-precision floating point number.
    //
    // Returns:
    //     value rounded to the nearest 64-bit signed integer. If value is halfway between
    //     two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int64.MaxValue or less than System.Int64.MinValue.
    [Pure]
    extern public static long ToInt64(double value);
    //
    // Summary:
    //     Converts the value of the specified single-precision floating point number
    //     to an equivalent 64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A single-precision floating point number.
    //
    // Returns:
    //     value rounded to the nearest 64-bit signed integer. If value is halfway between
    //     two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int64.MaxValue or less than System.Int64.MinValue.
    [Pure]
    extern public static long ToInt64(float value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit signed integer to an equivalent
    //     64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 32-signed integer.
    //
    // Returns:
    //     The 64-bit signed integer equivalent to the value of value.
    [Pure]
    extern public static long ToInt64(int value);
    //
    // Summary:
    //     Returns the specified 64-bit signed integer; no actual conversion is performed.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    // Returns:
    //     Parameter value is returned unchanged.
    [Pure]
    extern public static long ToInt64(long value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to a 64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface or null.
    //
    // Returns:
    //     A 64-bit signed integer equivalent to the value of value, or zero if value
    //     is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static long ToInt64(object value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit signed integer to the equivalent
    //     64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     An 8-bit signed integer.
    //
    // Returns:
    //     The 64-bit signed integer equivalent to the value of value.
    [Pure]
    extern public static long ToInt64(sbyte value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit signed integer to an equivalent
    //     64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer.
    //
    // Returns:
    //     A 64-bit signed integer equivalent to the value of value.
    [Pure]
    extern public static long ToInt64(short value);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    // Returns:
    //     A 64-bit signed integer equivalent to the value of value.  -or- Zero if value
    //     is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value does not consist of an optional sign followed by a sequence of digits
    //     (zero through nine).
    //
    //   System.OverflowException:
    //     value represents a number less than System.Int64.MinValue or greater than
    //     System.Int64.MaxValue.
    [Pure]
    extern public static long ToInt64(string value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit unsigned integer to an equivalent
    //     64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 32-bit unsigned integer.
    //
    // Returns:
    //     A 64-bit signed integer equivalent to the value of value.
    [Pure]
    extern public static long ToInt64(uint value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit unsigned integer to an equivalent
    //     64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 64-bit unsigned integer.
    //
    // Returns:
    //     A 64-bit signed integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.Int64.MaxValue.
    [Pure]
    extern public static long ToInt64(ulong value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit unsigned integer to the equivalent
    //     64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 16-bit unsigned integer.
    //
    // Returns:
    //     The 64-bit signed integer equivalent to the value of value.
    [Pure]
    extern public static long ToInt64(ushort value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to a 64-bit signed integer
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A 64-bit signed integer equivalent to the value of value, or zero if value
    //     is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static long ToInt64(object value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     64-bit signed integer using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A 64-bit signed integer equivalent to the value of value.  -or- Zero if value
    //     is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value does not consist of an optional sign followed by a sequence of digits
    //     (zero through nine).
    //
    //   System.OverflowException:
    //     value represents a number less than System.Int64.MinValue or greater than
    //     System.Int64.MaxValue.
    [Pure]
    extern public static long ToInt64(string value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the string representation of a number in a specified base to an
    //     equivalent 64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A string containing a number.
    //
    //   fromBase:
    //     The base of the number in value, which must be 2, 8, 10, or 16.
    //
    // Returns:
    //     A 64-bit signed integer equivalent to the number in value.  -or- Zero if
    //     value is null.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     fromBase is not 2, 8, 10, or 16. -or- value, which represents a non-base
    //     10 signed number, is prefixed with a negative sign.
    //
    //   System.FormatException:
    //     value contains a character that is not a valid digit in the base specified
    //     by fromBase. The exception message indicates that there are no digits to
    //     convert if the first character in value is invalid; otherwise, the message
    //     indicates that value contains invalid trailing characters.
    //
    //   System.OverflowException:
    //     value, which represents a non-base 10 signed number, is prefixed with a negative
    //     sign.  -or- The return value is less than System.Int64.MinValue or larger
    //     than System.Int64.MaxValue.
    [Pure]
    public static long ToInt64(string value, int fromBase)
    {
      Contract.Requires(fromBase == 2 || fromBase == 8 || fromBase == 10 || fromBase == 16);
      return default(long);
    }
    //
    // Summary:
    //     Converts the value of the specified Boolean value to the equivalent 8-bit
    //     signed integer.
    //
    // Parameters:
    //   value:
    //     A Boolean value.
    //
    // Returns:
    //     The number 1 if value is true; otherwise, 0.
    [Pure]
    extern public static sbyte ToSByte(bool value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit unsigned integer to the equivalent
    //     8-bit signed integer.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer.
    //
    // Returns:
    //     The 8-bit signed integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.SByte.MaxValue.
    [Pure]
    extern public static sbyte ToSByte(byte value);
    //
    // Summary:
    //     Converts the value of the specified Unicode character to the equivalent 8-bit
    //     signed integer.
    //
    // Parameters:
    //   value:
    //     A Unicode character.
    //
    // Returns:
    //     The 8-bit signed integer equivalent to value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.SByte.MaxValue.
    [Pure]
    extern public static sbyte ToSByte(char value);

#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A System.DateTime.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static sbyte ToSByte(DateTime value);
#endif

    //
    // Summary:
    //     Converts the value of the specified System.Decimal number to an equivalent
    //     8-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A System.Decimal number.
    //
    // Returns:
    //     value rounded to the nearest 8-bit signed integer. If value is halfway between
    //     two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.SByte.MaxValue or less than System.SByte.MinValue.
    [Pure]
    extern public static sbyte ToSByte(decimal value);
    //
    // Summary:
    //     Converts the value of the specified double-precision floating point number
    //     to an equivalent 8-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A double-precision floating point number.
    //
    // Returns:
    //     value rounded to the nearest 8-bit signed integer. If value is halfway between
    //     two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.SByte.MaxValue or less than System.SByte.MinValue.
    [Pure]
    extern public static sbyte ToSByte(double value);
    //
    // Summary:
    //     Converts the value of the specified single-precision floating point number
    //     to an equivalent 8-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A single-precision floating point number.
    //
    // Returns:
    //     value rounded to the nearest 8-bit signed integer. If value is halfway between
    //     two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.SByte.MaxValue or less than System.SByte.MinValue.
    [Pure]
    extern public static sbyte ToSByte(float value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit signed integer to an equivalent
    //     8-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    //
    // Returns:
    //     The 8-bit signed integer equivalent of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.SByte.MaxValue or less than System.SByte.MinValue.
    [Pure]
    extern public static sbyte ToSByte(int value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit signed integer to an equivalent
    //     8-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    // Returns:
    //     An 8-bit signed integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.SByte.MaxValue or less than System.SByte.MinValue.
    [Pure]
    extern public static sbyte ToSByte(long value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to an 8-bit signed integer.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface or null.
    //
    // Returns:
    //     An 8-bit signed integer equivalent to the value of value, or zero if value
    //     is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static sbyte ToSByte(object value);
    //
    // Summary:
    //     Returns the specified 8-bit signed integer; no actual conversion is performed.
    //
    // Parameters:
    //   value:
    //     An 8-bit signed integer.
    //
    // Returns:
    //     Parameter value is returned unchanged.
    [Pure]
    extern public static sbyte ToSByte(sbyte value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit signed integer to the equivalent
    //     8-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer.
    //
    // Returns:
    //     The 8-bit signed integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.SByte.MaxValue or less than System.SByte.MinValue.
    [Pure]
    extern public static sbyte ToSByte(short value);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     8-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    // Returns:
    //     An 8-bit signed integer equivalent to the value of value.  -or- Zero if value
    //     is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value does not consist of an optional sign followed by a sequence of digits
    //     (zero through nine).
    //
    //   System.OverflowException:
    //     value represents a number less than System.SByte.MinValue or greater than
    //     System.SByte.MaxValue.
    [Pure]
    extern public static sbyte ToSByte(string value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit unsigned integer to an equivalent
    //     8-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 32-bit unsigned integer.
    //
    // Returns:
    //     An 8-bit signed integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.SByte.MaxValue or less than System.SByte.MinValue.
    [Pure]
    extern public static sbyte ToSByte(uint value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit unsigned integer to an equivalent
    //     8-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 64-bit unsigned integer.
    //
    // Returns:
    //     An 8-bit signed integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.SByte.MaxValue or less than System.SByte.MinValue.

    [Pure]
    extern public static sbyte ToSByte(ulong value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit unsigned integer to the equivalent
    //     8-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 16-bit unsigned integer.
    //
    // Returns:
    //     The 8-bit signed integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.SByte.MaxValue.
    [Pure]
    extern public static sbyte ToSByte(ushort value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to an 8-bit signed integer
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     An 8-bit signed integer equivalent to the value of value, or zero if value
    //     is null.
    [Pure]
    extern public static sbyte ToSByte(object value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     8-bit signed integer using specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     An 8-bit signed integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     value is null.
    //
    //   System.FormatException:
    //     value does not consist of an optional sign followed by a sequence of digits
    //     (zero through nine).
    //
    //   System.OverflowException:
    //     value represents a number less than System.SByte.MinValue or greater than
    //     System.SByte.MaxValue.
    [Pure]
    public static sbyte ToSByte(string value, IFormatProvider provider)
    {
      Contract.Requires(value != null);
      return default(sbyte);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified base to an
    //     equivalent 8-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A string containing a number.
    //
    //   fromBase:
    //     The base of the number in value, which must be 2, 8, 10, or 16.
    //
    // Returns:
    //     An 8-bit signed integer equivalent to the number in value.  -or- Zero if
    //     value is null.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     fromBase is not 2, 8, 10, or 16. -or- value, which represents a non-base
    //     10 signed number, is prefixed with a negative sign.
    //
    //   System.FormatException:
    //     value contains a character that is not a valid digit in the base specified
    //     by fromBase. The exception message indicates that there are no digits to
    //     convert if the first character in value is invalid; otherwise, the message
    //     indicates that value contains invalid trailing characters.
    //
    //   System.OverflowException:
    //     value, which represents a non-base 10 signed number, is prefixed with a negative
    //     sign.  -or- The return value is less than System.SByte.MinValue or larger
    //     than System.SByte.MaxValue.
    [Pure]
    public static sbyte ToSByte(string value, int fromBase)
    {
      Contract.Requires(fromBase == 2 || fromBase == 8 || fromBase == 10 || fromBase == 16);
      return default(sbyte);
    }
    //
    // Summary:
    //     Converts the value of the specified Boolean value to the equivalent single-precision
    //     floating point number.
    //
    // Parameters:
    //   value:
    //     A Boolean value.
    //
    // Returns:
    //     The number 1 if value is true; otherwise, 0.
    [Pure]
    extern public static float ToSingle(bool value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit unsigned integer to the equivalent
    //     single-precision floating point number.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer.
    //
    // Returns:
    //     The single-precision floating point number equivalent to the value of value.
    [Pure]
    extern public static float ToSingle(byte value);
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A Unicode character.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static float ToSingle(char value);

#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A System.DateTime.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static float ToSingle(DateTime value);
#endif

    //
    // Summary:
    //     Converts the value of the specified System.Decimal number to an equivalent
    //     single-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A System.Decimal number.
    //
    // Returns:
    //     A single-precision floating point number equivalent to the value of value.
    //      value is rounded using rounding to nearest. For example, when rounded to
    //     two decimals, the value 2.345 becomes 2.34 and the value 2.355 becomes 2.36.
    [Pure]
    extern public static float ToSingle(decimal value);
    //
    // Summary:
    //     Converts the value of the specified double-precision floating point number
    //     to an equivalent single-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A double-precision floating point number.
    //
    // Returns:
    //     A single-precision floating point number equivalent to the value of value.
    //      value is rounded using rounding to nearest. For example, when rounded to
    //     two decimals, the value 2.345 becomes 2.34 and the value 2.355 becomes 2.36.
    [Pure]
    extern public static float ToSingle(double value);
    //
    // Summary:
    //     Returns the specified single-precision floating point number; no actual conversion
    //     is performed.
    //
    // Parameters:
    //   value:
    //     A single-precision floating point number.
    //
    // Returns:
    //     Parameter value is returned unchanged.
    [Pure]
    extern public static float ToSingle(float value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit signed integer to an equivalent
    //     single-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    //
    // Returns:
    //     A single-precision floating point number equivalent to the value of value.
    [Pure]
    extern public static float ToSingle(int value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit signed integer to an equivalent
    //     single-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    // Returns:
    //     A single-precision floating point number equivalent to the value of value.
    [Pure]
    extern public static float ToSingle(long value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to a single-precision floating
    //     point number.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface or null.
    //
    // Returns:
    //     A single-precision floating point number equivalent to the value of value,
    //     or zero if value is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static float ToSingle(object value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit signed integer to the equivalent
    //     single-precision floating point number.
    //
    // Parameters:
    //   value:
    //     An 8-bit signed integer.
    //
    // Returns:
    //     The 8-bit signed integer equivalent to the value of value.
    [Pure]
    extern public static float ToSingle(sbyte value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit signed integer to an equivalent
    //     single-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer.
    //
    // Returns:
    //     A single-precision floating point number equivalent to the value of value.
    [Pure]
    extern public static float ToSingle(short value);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     single-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    // Returns:
    //     A single-precision floating point number equivalent to the value of value.
    //      -or- Zero if value is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value is not a number in a valid format.
    //
    //   System.OverflowException:
    //     value represents a number less than System.Single.MinValue or greater than
    //     System.Single.MaxValue.
    [Pure]
    extern public static float ToSingle(string value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit unsigned integer to an equivalent
    //     single-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A 32-bit unsigned integer.
    //
    // Returns:
    //     A single-precision floating point number equivalent to the value of value.
    [Pure]
    extern public static float ToSingle(uint value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit unsigned integer to an equivalent
    //     single-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A 64-bit unsigned integer.
    //
    // Returns:
    //     A single-precision floating point number equivalent to the value of value.
    [Pure]
    extern public static float ToSingle(ulong value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit unsigned integer to the equivalent
    //     single-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A 16-bit unsigned integer.
    //
    // Returns:
    //     The single-precision floating point number equivalent to the value of value.
    [Pure]
    extern public static float ToSingle(ushort value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to an single-precision
    //     floating point number using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A single-precision floating point number equivalent to the value of value,
    //     or zero if value is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static float ToSingle(object value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     single-precision floating point number using the specified culture-specific
    //     formatting information.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A single-precision floating point number equivalent to the value of value.
    //      -or- Zero if value is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value is not a number in a valid format.
    //
    //   System.OverflowException:
    //     value represents a number less than System.Single.MinValue or greater than
    //     System.Single.MaxValue.
    [Pure]
    extern public static float ToSingle(string value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of the specified Boolean to its equivalent System.String
    //     representation.
    //
    // Parameters:
    //   value:
    //     A Boolean value.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(bool value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    //
    // Summary:
    //     Converts the value of the specified 8-bit unsigned integer to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(byte value) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    //
    // Summary:
    //     Converts the value of the specified Unicode character to its equivalent System.String
    //     representation.
    //
    // Parameters:
    //   value:
    //     A Unicode character.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(char value) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    //
    // Summary:
    //     Converts the value of the specified System.DateTime to its equivalent System.String
    //     representation.
    //
    // Parameters:
    //   value:
    //     A System.DateTime.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(DateTime value) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    //
    // Summary:
    //     Converts the value of the specified System.Decimal number to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     A System.Decimal number.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(decimal value) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified double-precision floating point number
    //     to its equivalent System.String representation.
    //
    // Parameters:
    //   value:
    //     A double-precision floating point number.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(double value) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    //
    // Summary:
    //     Converts the value of the specified single-precision floating point number
    //     to its equivalent System.String representation.
    //
    // Parameters:
    //   value:
    //     A single-precision floating point number.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(float value) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    //
    // Summary:
    //     Converts the value of the specified 32-bit signed integer to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(int value) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    //
    // Summary:
    //     Converts the value of the specified 64-bit signed integer to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(long value) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    //
    // Summary:
    //     Converts the value of the specified System.Object to its System.String representation.
    //
    // Parameters:
    //   value:
    //     An System.Object or null.
    //
    // Returns:
    //     The System.String representation of the value of value, or System.String.Empty
    //     if value is null.
    [Pure]
    public static string ToString(object value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified 8-bit signed integer to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     An 8-bit signed integer.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(sbyte value) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified 16-bit signed integer to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(short value) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

#if !SILVERLIGHT
    //
    // Summary:
    //     Returns the specified instance of System.String; no actual conversion is
    //     performed.
    //
    // Parameters:
    //   value:
    //     A System.String.
    //
    // Returns:
    //     Parameter value is returned unchanged.
    [Pure]
    public static string ToString(string value)
    {
      Contract.Ensures(Contract.Result<string>() == value);
      return default(string);
    }
#endif

    //
    // Summary:
    //     Converts the value of the specified 32-bit unsigned integer to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     A 32-bit unsigned integer.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(uint value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified 64-bit unsigned integer to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     A 64-bit unsigned integer.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(ulong value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified 16-bit unsigned integer to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     A 16-bit unsigned integer.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(ushort value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified Boolean to its equivalent System.String
    //     representation.
    //
    // Parameters:
    //   value:
    //     A Boolean value.
    //
    //   provider:
    //     (Reserved) An instance of an System.IFormatProvider interface implementation.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(bool value, IFormatProvider provider)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified 8-bit unsigned integer to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(byte value, IFormatProvider provider) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of an 8-bit unsigned integer to its equivalent string
    //     representation in a specified base.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer.
    //
    //   toBase:
    //     The base of the return value, which must be 2, 8, 10, or 16.
    //
    // Returns:
    //     The string representation of value in base toBase.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     toBase is not 2, 8, 10, or 16.
    [Pure]
    public static string ToString(byte value, int toBase)
    {
      Contract.Requires(toBase == 2 || toBase == 8 || toBase == 10 || toBase == 16);
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified Unicode character to its equivalent System.String
    //     representation.
    //
    // Parameters:
    //   value:
    //     A Unicode character.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(char value, IFormatProvider provider) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified System.DateTime to its equivalent System.String
    //     representation.
    //
    // Parameters:
    //   value:
    //     A System.DateTime.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(DateTime value, IFormatProvider provider) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified System.Decimal number to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     A System.Decimal number.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(decimal value, IFormatProvider provider) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified double-precision floating point number
    //     to its equivalent System.String representation.
    //
    // Parameters:
    //   value:
    //     A double-precision floating point number.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     The System.String equivalent of the value of value.  provider is ignored;
    //     it does not participate in this operation.
    [Pure]
    public static string ToString(double value, IFormatProvider provider) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified single-precision floating point number
    //     to its equivalent System.String representation.
    //
    // Parameters:
    //   value:
    //     A single-precision floating point number.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(float value, IFormatProvider provider) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified 32-bit signed integer to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(int value, IFormatProvider provider) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of a 32-bit signed integer to its equivalent System.String
    //     representation in a specified base.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    //
    //   toBase:
    //     The base of the return value, which must be 2, 8, 10, or 16.
    //
    // Returns:
    //     The System.String representation of value in base toBase.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     toBase is not 2, 8, 10, or 16.
    [Pure]
    public static string ToString(int value, int toBase) {
      Contract.Requires(toBase == 2 || toBase == 8 || toBase == 10 || toBase == 16);
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified 64-bit signed integer to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(long value, IFormatProvider provider) {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of a 64-bit signed integer to its equivalent System.String
    //     representation in a specified base.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    //   toBase:
    //     The base of the return value, which must be 2, 8, 10, or 16.
    //
    // Returns:
    //     The System.String representation of value in base toBase.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     toBase is not 2, 8, 10, or 16.
    [Pure]
    public static string ToString(long value, int toBase)
    {
      Contract.Requires(toBase == 2 || toBase == 8 || toBase == 10 || toBase == 16);
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified System.Object to its equivalent System.String
    //     representation using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     An System.Object or null.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     The System.String representation of the value of value, or System.String.Empty
    //     if value is null.
    [Pure]
    public static string ToString(object value, IFormatProvider provider)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified 8-bit signed integer to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     An 8-bit signed integer.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(sbyte value, IFormatProvider provider)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified 16-bit signed integer to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(short value, IFormatProvider provider)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of a 16-bit signed integer to its equivalent System.String
    //     representation in a specified base.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer.
    //
    //   toBase:
    //     The base of the return value, which must be 2, 8, 10, or 16.
    //
    // Returns:
    //     The System.String representation of value in base toBase.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     toBase is not 2, 8, 10, or 16.
    [Pure]
    public static string ToString(short value, int toBase)
    {
      Contract.Requires(toBase == 2 || toBase == 8 || toBase == 10 || toBase == 16);

      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
#if !SILVERLIGHT
    //
    // Summary:
    //     Returns the specified instance of System.String; no actual conversion is
    //     performed.
    //
    // Parameters:
    //   value:
    //     A System.String.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     Parameter value is returned unchanged.
    [Pure]
    extern public static string ToString(string value, IFormatProvider provider);
#endif
    //
    // Summary:
    //     Converts the value of the specified 32-bit unsigned integer to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     A 32-bit unsigned integer.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(uint value, IFormatProvider provider)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified 64-bit unsigned integer to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     A 64-bit unsigned integer.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(ulong value, IFormatProvider provider)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified 16-bit unsigned integer to its equivalent
    //     System.String representation.
    //
    // Parameters:
    //   value:
    //     A 16-bit unsigned integer.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     The System.String equivalent of the value of value.
    [Pure]
    public static string ToString(ushort value, IFormatProvider provider)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Converts the value of the specified Boolean value to the equivalent 16-bit
    //     unsigned integer.
    //
    // Parameters:
    //   value:
    //     A Boolean value.
    //
    // Returns:
    //     The number 1 if value is true; otherwise, 0.
    [Pure]
    extern public static ushort ToUInt16(bool value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit unsigned integer to the equivalent
    //     16-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer.
    //
    // Returns:
    //     The 16-bit unsigned integer equivalent to the value of value.
    [Pure]
    extern public static ushort ToUInt16(byte value);
    //
    // Summary:
    //     Converts the value of the specified Unicode character to the equivalent 16-bit
    //     unsigned integer.
    //
    // Parameters:
    //   value:
    //     A Unicode character.
    //
    // Returns:
    //     The 16-bit unsigned integer equivalent to value.
    [Pure]
    extern public static ushort ToUInt16(char value);
#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A System.DateTime.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static ushort ToUInt16(DateTime value);
#endif
    //
    // Summary:
    //     Converts the value of the specified System.Decimal number to an equivalent
    //     16-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A System.Decimal number.
    //
    // Returns:
    //     value rounded to the nearest 16-bit unsigned integer. If value is halfway
    //     between two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero or greater than System.UInt16.MaxValue.
    [Pure]
    extern public static ushort ToUInt16(decimal value);
    //
    // Summary:
    //     Converts the value of the specified double-precision floating point number
    //     to an equivalent 16-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A double-precision floating point number.
    //
    // Returns:
    //     value rounded to the nearest 16-bit unsigned integer. If value is halfway
    //     between two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero or greater than System.UInt16.MaxValue.
    [Pure]
    extern public static ushort ToUInt16(double value);
    //
    // Summary:
    //     Converts the value of the specified single-precision floating point number
    //     to an equivalent 16-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A single-precision floating point number.
    //
    // Returns:
    //     value rounded to the nearest 16-bit unsigned integer. If value is halfway
    //     between two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero or greater than System.UInt16.MaxValue.
    [Pure]
    extern public static ushort ToUInt16(float value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit signed integer to an equivalent
    //     16-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    //
    // Returns:
    //     The 16-bit unsigned integer equivalent of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero or greater than System.UInt16.MaxValue.
    [Pure]
    extern public static ushort ToUInt16(int value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit signed integer to an equivalent
    //     16-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    // Returns:
    //     A 16-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero or greater than System.UInt16.MaxValue.
    [Pure]
    extern public static ushort ToUInt16(long value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to a 16-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface or null.
    //
    // Returns:
    //     A 16-bit unsigned integer equivalent to the value of value, or zero if value
    //     is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static ushort ToUInt16(object value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit signed integer to the equivalent
    //     16-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     An 8-bit signed integer.
    //
    // Returns:
    //     The 16-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero.
    [Pure]
    extern public static ushort ToUInt16(sbyte value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit signed integer to the equivalent
    //     16-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer.
    //
    // Returns:
    //     The 16-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero.
    [Pure]
    extern public static ushort ToUInt16(short value);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     16-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    // Returns:
    //     A 16-bit unsigned integer equivalent to the value of value.  -or- Zero if
    //     value is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value does not consist of an optional sign followed by a sequence of digits
    //     (zero through nine).
    //
    //   System.OverflowException:
    //     value represents a number less than System.Int16.MinValue or greater than
    //     System.Int16.MaxValue.
    [Pure]
    extern public static ushort ToUInt16(string value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit unsigned integer to an equivalent
    //     16-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 32-bit unsigned integer.
    //
    // Returns:
    //     A 16-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.UInt16.MaxValue.
    [Pure]
    extern public static ushort ToUInt16(uint value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit unsigned integer to an equivalent
    //     16-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 64-bit unsigned integer.
    //
    // Returns:
    //     A 16-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.UInt16.MaxValue.
    [Pure]
    extern public static ushort ToUInt16(ulong value);
    //
    // Summary:
    //     Returns the specified 16-bit unsigned integer; no actual conversion is performed.
    //
    // Parameters:
    //   value:
    //     A 16-bit unsigned integer.
    //
    // Returns:
    //     Parameter value is returned unchanged.
    [Pure]
    extern public static ushort ToUInt16(ushort value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to a 16-bit unsigned integer
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A 16-bit unsigned integer equivalent to the value of value, or zero if value
    //     is null.
    [Pure]
    extern public static ushort ToUInt16(object value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     16-bit unsigned integer using specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A 16-bit unsigned integer equivalent to the value of value.  -or- Zero if
    //     value is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value does not consist of an optional sign followed by a sequence of digits
    //     (zero through nine).
    //
    //   System.OverflowException:
    //     value represents a number less than System.Int16.MinValue or greater than
    //     System.Int16.MaxValue.
    [Pure]
    extern public static ushort ToUInt16(string value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the string representation of a number in a specified base to an
    //     equivalent 16-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A string containing a number.
    //
    //   fromBase:
    //     The base of the number in value, which must be 2, 8, 10, or 16.
    //
    // Returns:
    //     A 16-bit unsigned integer equivalent to the number in value.  -or- Zero if
    //     value is null.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     fromBase is not 2, 8, 10, or 16. -or- value, which represents a non-base
    //     10 unsigned number, is prefixed with a negative sign.
    //
    //   System.FormatException:
    //     value contains a character that is not a valid digit in the base specified
    //     by fromBase. The exception message indicates that there are no digits to
    //     convert if the first character in value is invalid; otherwise, the message
    //     indicates that value contains invalid trailing characters.
    //
    //   System.OverflowException:
    //     value, which represents a non-base 10 unsigned number, is prefixed with a
    //     negative sign.  -or- The return value is less than System.UInt16.MinValue
    //     or larger than System.UInt16.MaxValue.
    [Pure]
    public static ushort ToUInt16(string value, int fromBase)
    {
      Contract.Requires(fromBase == 2 || fromBase == 8 || fromBase == 10 || fromBase == 16);
      return default(ushort);
    }
    //
    // Summary:
    //     Converts the value of the specified Boolean value to the equivalent 32-bit
    //     unsigned integer.
    //
    // Parameters:
    //   value:
    //     A Boolean value.
    //
    // Returns:
    //     The number 1 if value is true; otherwise, 0.
    [Pure]
    extern public static uint ToUInt32(bool value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit unsigned integer to the equivalent
    //     32-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer.
    //
    // Returns:
    //     The 32-bit unsigned integer equivalent to the value of value.
    [Pure]
    extern public static uint ToUInt32(byte value);
    //
    // Summary:
    //     Converts the value of the specified Unicode character to the equivalent 32-bit
    //     unsigned integer.
    //
    // Parameters:
    //   value:
    //     A Unicode character.
    //
    // Returns:
    //     The 32-bit unsigned integer equivalent to value.
    [Pure]
    extern public static uint ToUInt32(char value);
#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A System.DateTime.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static uint ToUInt32(DateTime value);
#endif
    //
    // Summary:
    //     Converts the value of the specified System.Decimal number to an equivalent
    //     32-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A System.Decimal number.
    //
    // Returns:
    //     value rounded to the nearest 32-bit unsigned integer. If value is halfway
    //     between two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero or greater than System.UInt32.MaxValue.
    [Pure]
    extern public static uint ToUInt32(decimal value);
    //
    // Summary:
    //     Converts the value of the specified double-precision floating point number
    //     to an equivalent 32-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A double-precision floating point number.
    //
    // Returns:
    //     value rounded to the nearest 32-bit unsigned integer. If value is halfway
    //     between two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero or greater than System.UInt32.MaxValue.
    [Pure]
    extern public static uint ToUInt32(double value);
    //
    // Summary:
    //     Converts the value of the specified single-precision floating point number
    //     to an equivalent 32-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A single-precision floating point number.
    //
    // Returns:
    //     value rounded to the nearest 32-bit unsigned integer. If value is halfway
    //     between two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero or greater than System.UInt32.MaxValue.
    [Pure]
    extern public static uint ToUInt32(float value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit signed integer to an equivalent
    //     32-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    //
    // Returns:
    //     The 32-bit unsigned integer equivalent of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero.
    [Pure]
    extern public static uint ToUInt32(int value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit signed integer to an equivalent
    //     32-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    // Returns:
    //     A 32-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero or greater than System.UInt32.MaxValue.
    [Pure]
    extern public static uint ToUInt32(long value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to a 32-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface or null.
    //
    // Returns:
    //     A 32-bit unsigned integer equivalent to the value of value, or zero if value
    //     is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static uint ToUInt32(object value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit signed integer to the equivalent
    //     32-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     An 8-bit signed integer.
    //
    // Returns:
    //     The 8-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero.
    [Pure]
    extern public static uint ToUInt32(sbyte value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit signed integer to the equivalent
    //     32-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer.
    //
    // Returns:
    //     The 32-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero.
    [Pure]
    extern public static uint ToUInt32(short value);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     32-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    // Returns:
    //     A 32-bit unsigned integer equivalent to the value of value.  -or- Zero if
    //     value is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value does not consist of an optional sign followed by a sequence of digits
    //     (zero through nine).
    //
    //   System.OverflowException:
    //     value represents a number less than System.Int32.MinValue or greater than
    //     System.Int32.MaxValue.
    [Pure]
    extern public static uint ToUInt32(string value);
    //
    // Summary:
    //     Returns the specified 32-bit unsigned integer; no actual conversion is performed.
    //
    // Parameters:
    //   value:
    //     A 32-bit unsigned integer.
    //
    // Returns:
    //     Parameter value is returned unchanged.
    [Pure]
    extern public static uint ToUInt32(uint value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit unsigned integer to an equivalent
    //     32-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 64-bit unsigned integer.
    //
    // Returns:
    //     A 32-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is greater than System.UInt32.MaxValue.
    [Pure]
    extern public static uint ToUInt32(ulong value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit unsigned integer to the equivalent
    //     32-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 16-bit unsigned integer.
    //
    // Returns:
    //     The 32-bit unsigned integer equivalent to the value of value.
    [Pure]
    extern public static uint ToUInt32(ushort value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to a 32-bit unsigned integer
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A 32-bit unsigned integer equivalent to the value of value, or zero if value
    //     is null.
    [Pure]
    extern public static uint ToUInt32(object value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     32-bit unsigned integer using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A 32-bit unsigned integer equivalent to the value of value.  -or- Zero if
    //     value is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value does not consist of an optional sign followed by a sequence of digits
    //     (zero through nine).
    //
    //   System.OverflowException:
    //     value represents a number less than System.Int32.MinValue or greater than
    //     System.Int32.MaxValue.
    [Pure]
    extern public static uint ToUInt32(string value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the string representation of a number in a specified base to an
    //     equivalent 32-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A string containing a number.
    //
    //   fromBase:
    //     The base of the number in value, which must be 2, 8, 10, or 16.
    //
    // Returns:
    //     A 32-bit unsigned integer equivalent to the number in value.  -or- Zero if
    //     value is null.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     fromBase is not 2, 8, 10, or 16. -or- value, which represents a non-base
    //     10 unsigned number, is prefixed with a negative sign.
    //
    //   System.FormatException:
    //     value contains a character that is not a valid digit in the base specified
    //     by fromBase. The exception message indicates that there are no digits to
    //     convert if the first character in value is invalid; otherwise, the message
    //     indicates that value contains invalid trailing characters.
    //
    //   System.OverflowException:
    //     value, which represents a non-base 10 unsigned number, is prefixed with a
    //     negative sign.  -or- The return value is less than System.UInt32.MinValue
    //     or larger than System.UInt32.MaxValue.
    [Pure]
    public static uint ToUInt32(string value, int fromBase)
    {
      Contract.Requires(fromBase == 2 || fromBase == 8 || fromBase == 10 || fromBase == 16);
      return default(uint);
    }
    //
    // Summary:
    //     Converts the value of the specified Boolean value to the equivalent 64-bit
    //     unsigned integer.
    //
    // Parameters:
    //   value:
    //     A Boolean value.
    //
    // Returns:
    //     The number 1 if value is true; otherwise, 0.
    [Pure]
    extern public static ulong ToUInt64(bool value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit unsigned integer to the equivalent
    //     64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer.
    //
    // Returns:
    //     The 64-bit signed integer equivalent to the value of value.
    [Pure]
    extern public static ulong ToUInt64(byte value);
    //
    // Summary:
    //     Converts the value of the specified Unicode character to the equivalent 64-bit
    //     unsigned integer.
    //
    // Parameters:
    //   value:
    //     A Unicode character.
    //
    // Returns:
    //     The 64-bit unsigned integer equivalent to value.
    [Pure]
    extern public static ulong ToUInt64(char value);
#if !SILVERLIGHT
    //
    // Summary:
    //     Calling this method always throws System.InvalidCastException.
    //
    // Parameters:
    //   value:
    //     A System.DateTime.
    //
    // Returns:
    //     This conversion is not supported. No value is returned.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     This conversion is not supported.
    [Pure]
    extern public static ulong ToUInt64(DateTime value);
#endif
    //
    // Summary:
    //     Converts the value of the specified System.Decimal number to an equivalent
    //     64-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A System.Decimal number.
    //
    // Returns:
    //     value rounded to the nearest 64-bit unsigned integer. If value is halfway
    //     between two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero or greater than System.UInt64.MaxValue.
    [Pure]
    extern public static ulong ToUInt64(decimal value);
    //
    // Summary:
    //     Converts the value of the specified double-precision floating point number
    //     to an equivalent 64-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A double-precision floating point number.
    //
    // Returns:
    //     value rounded to the nearest 64-bit unsigned integer. If value is halfway
    //     between two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero or greater than System.UInt64.MaxValue.
    [Pure]
    extern public static ulong ToUInt64(double value);
    //
    // Summary:
    //     Converts the value of the specified single-precision floating point number
    //     to an equivalent 64-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A single-precision floating point number.
    //
    // Returns:
    //     value rounded to the nearest 64-bit unsigned integer. If value is halfway
    //     between two whole numbers, the even number is returned; that is, 4.5 is converted
    //     to 4, and 5.5 is converted to 6.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero or greater than System.UInt64.MaxValue.
    [Pure]
    extern public static ulong ToUInt64(float value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit signed integer to an equivalent
    //     64-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    //
    // Returns:
    //     The 64-bit unsigned integer equivalent of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero.
    [Pure]
    extern public static ulong ToUInt64(int value);
    //
    // Summary:
    //     Converts the value of the specified 64-bit signed integer to an equivalent
    //     64-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    // Returns:
    //     A 64-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero.
    [Pure]
    extern public static ulong ToUInt64(long value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to a 64-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface or null.
    //
    // Returns:
    //     A 64-bit unsigned integer equivalent to the value of value, or zero if value
    //     is null.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     value does not implement System.IConvertible.
    [Pure]
    extern public static ulong ToUInt64(object value);
    //
    // Summary:
    //     Converts the value of the specified 8-bit signed integer to the equivalent
    //     64-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     An 8-bit signed integer.
    //
    // Returns:
    //     The 64-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero.
    [Pure]
    extern public static ulong ToUInt64(sbyte value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit signed integer to the equivalent
    //     64-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer.
    //
    // Returns:
    //     The 64-bit unsigned integer equivalent to the value of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than zero.
    [Pure]
    extern public static ulong ToUInt64(short value);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    // Returns:
    //     A 64-bit signed integer equivalent to the value of value.  -or- Zero if value
    //     is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value does not consist of an optional sign followed by a sequence of digits
    //     (zero through nine).
    //
    //   System.OverflowException:
    //     value represents a number less than System.Int64.MinValue or greater than
    //     System.Int64.MaxValue.
    [Pure]
    extern public static ulong ToUInt64(string value);
    //
    // Summary:
    //     Converts the value of the specified 32-bit unsigned integer to an equivalent
    //     64-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 32-bit unsigned integer.
    //
    // Returns:
    //     The 64-bit unsigned integer equivalent of value.
    [Pure]
    extern public static ulong ToUInt64(uint value);
    //
    // Summary:
    //     Returns the specified 64-bit unsigned integer; no actual conversion is performed.
    //
    // Parameters:
    //   value:
    //     A 64-bit unsigned integer.
    //
    // Returns:
    //     Parameter value is returned unchanged.
    [Pure]
    extern public static ulong ToUInt64(ulong value);
    //
    // Summary:
    //     Converts the value of the specified 16-bit unsigned integer to the equivalent
    //     64-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A 16-bit unsigned integer.
    //
    // Returns:
    //     The 64-bit unsigned integer equivalent to the value of value.
    [Pure]
    extern public static ulong ToUInt64(ushort value);
    //
    // Summary:
    //     Converts the value of the specified System.Object to a 64-bit unsigned integer
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     An System.Object that implements the System.IConvertible interface.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A 64-bit unsigned integer equivalent to the value of value, or zero if value
    //     is null.
    [Pure]
    extern public static ulong ToUInt64(object value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the specified System.String representation of a number to an equivalent
    //     64-bit unsigned integer using the specified culture-specific formatting information.
    //
    // Parameters:
    //   value:
    //     A System.String containing a number to convert.
    //
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A 64-bit unsigned integer equivalent to the value of value.  -or- Zero if
    //     value is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     value does not consist of an optional sign followed by a sequence of digits
    //     (zero through nine).
    //
    //   System.OverflowException:
    //     value represents a number less than System.Int64.MinValue or greater than
    //     System.Int64.MaxValue.
    [Pure]
    extern public static ulong ToUInt64(string value, IFormatProvider provider);
    //
    // Summary:
    //     Converts the string representation of a number in a specified base to an
    //     equivalent 64-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A string containing a number.
    //
    //   fromBase:
    //     The base of the number in value, which must be 2, 8, 10, or 16.
    //
    // Returns:
    //     A 64-bit unsigned integer equivalent to the number in value.  -or- Zero if
    //     value is null.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     fromBase is not 2, 8, 10, or 16. -or- value, which represents a non-base
    //     10 unsigned number, is prefixed with a negative sign.
    //
    //   System.FormatException:
    //     value contains a character that is not a valid digit in the base specified
    //     by fromBase. The exception message indicates that there are no digits to
    //     convert if the first character in value is invalid; otherwise, the message
    //     indicates that value contains invalid trailing characters.
    //
    //   System.OverflowException:
    //     value, which represents a non-base 10 unsigned number, is prefixed with a
    //     negative sign.  -or- The return value is less than System.UInt64.MinValue
    //     or larger than System.UInt64.MaxValue.
    [Pure]
    public static ulong ToUInt64(string value, int fromBase)
    {
      Contract.Requires(fromBase == 2 || fromBase == 8 || fromBase == 10 || fromBase == 16);
      return default(ulong);
    }
  }
}

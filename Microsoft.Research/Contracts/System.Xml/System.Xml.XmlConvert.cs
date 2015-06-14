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
using System.Diagnostics.Contracts;

namespace System.Xml
{
  // Summary:
  //     Encodes and decodes XML names and provides methods for converting between
  //     common language runtime types and XML Schema definition language (XSD) types.
  //     When converting data types the values returned are locale independent.
  public class XmlConvert
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.XmlConvert class.
    //public XmlConvert();

    // Summary:
    //     Decodes a name. This method does the reverse of the System.Xml.XmlConvert.EncodeName(System.String)
    //     and System.Xml.XmlConvert.EncodeLocalName(System.String) methods.
    //
    // Parameters:
    //   name:
    //     The name to be transformed.
    //
    // Returns:
    //     The decoded name.
    //extern public static string DecodeName(string name);
    //
    // Summary:
    //     Converts the name to a valid XML local name.
    //
    // Parameters:
    //   name:
    //     The name to be encoded.
    //
    // Returns:
    //     The encoded name.
    //public static string EncodeLocalName(string name);
    //
    // Summary:
    //     Converts the name to a valid XML name.
    //
    // Parameters:
    //   name:
    //     A name to be translated.
    //
    // Returns:
    //     Returns the name with any invalid characters replaced by an escape string.
    //extern public static string EncodeName(string name);
    //
    // Summary:
    //     Verifies the name is valid according to the XML specification.
    //
    // Parameters:
    //   name:
    //     The name to be encoded.
    //
    // Returns:
    //     The encoded name.
    //extern public static string EncodeNmToken(string name);
    //
    // Summary:
    //     Converts the System.String to a System.Boolean equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    // Returns:
    //     A Boolean value, that is, true or false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s does not represent a Boolean value.
    public static bool ToBoolean(string s)
    {
      Contract.Requires(s != null);

      return default(bool);
    }
    //
    // Summary:
    //     Converts the System.String to a System.Byte equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    // Returns:
    //     A Byte equivalent of the string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not in the correct format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.Byte.MinValue or greater than System.Byte.MaxValue.
    public static byte ToByte(string s)
    {
      Contract.Requires(s != null);

      return default(byte);
    }
    //
    // Summary:
    //     Converts the System.String to a System.Char equivalent.
    //
    // Parameters:
    //   s:
    //     The string containing a single character to convert.
    //
    // Returns:
    //     A Char representing the single character.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The value of the s parameter is null.
    //
    //   System.FormatException:
    //     The s parameter contains more than one character.
    public static char ToChar(string s)
    {
      Contract.Requires(s != null);
      Contract.Requires(s.Length == 1);

      return default(char);
    }
    //
    // Summary:
    //     Converts the System.String to a System.DateTime equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    // Returns:
    //     A DateTime equivalent of the string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is an empty string or is not in the correct format.
    //[Obsolete("Use XmlConvert.ToDateTime() that takes in XmlDateTimeSerializationMode")]
#if !SILVERLIGHT
    public static DateTime ToDateTime(string s)
    {
      Contract.Requires(s != null);

      return default(DateTime);
    }
#endif
    //
    // Summary:
    //     Converts the System.String to a System.DateTime equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    //   format:
    //     The format structure to apply to the converted DateTime. Valid formats include
    //     "yyyy-MM-ddTHH:mm:sszzzzzz" and its subsets. The string is validated against
    //     this format.
    //
    // Returns:
    //     A DateTime equivalent of the string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s or format is String.Empty -or- s does not contain a date and time that
    //     corresponds to format.
    public static DateTime ToDateTime(string s, string format)
    {
      Contract.Requires(s != null);
      Contract.Requires(s.Length != 0);
      Contract.Requires(format.Length != 0);

      return default(DateTime);
    }
    //
    // Summary:
    //     Converts the System.String to a System.DateTime equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    //   formats:
    //     An array containing the format structures to apply to the converted DateTime.
    //     Valid formats include "yyyy-MM-ddTHH:mm:sszzzzzz" and its subsets.
    //
    // Returns:
    //     A DateTime equivalent of the string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s or an element of formats is String.Empty -or- s does not contain a date
    //     and time that corresponds to any of the elements of formats.
    public static DateTime ToDateTime(string s, string[] formats)
    {
      Contract.Requires(s != null);
      Contract.Requires(s.Length != 0);
      Contract.Requires(Contract.ForAll(formats, str => str != String.Empty));

      return default(DateTime);
    }
    //
    // Summary:
    //     Converts the System.String to a System.DateTime using the System.Xml.XmlDateTimeSerializationMode
    //     specified
    //
    // Parameters:
    //   s:
    //     The System.String value to convert.
    //
    //   dateTimeOption:
    //     One of the System.Xml.XmlDateTimeSerializationMode values that specify whether
    //     the date should be converted to local time or preserved as Coordinated Universal
    //     Time (UTC), if it is a UTC date.
    //
    // Returns:
    //     A System.DateTime equivalent of the System.String.
    //
    // Exceptions:
    //   System.NullReferenceException:
    //     s is null.
    //
    //   System.ArgumentNullException:
    //     The dateTimeOption value is null.
    //
    //   System.FormatException:
    //     s is an empty string or is not in a valid format.
    //public static DateTime ToDateTime(string s, XmlDateTimeSerializationMode dateTimeOption);
    //
    // Summary:
    //     Converts the supplied System.String to a System.DateTimeOffset equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.  Note: The string must conform to a subset of the
    //     W3C Recommendation for the XML dateTime type. For more information see http://www.w3.org/TR/xmlschema-2/#dateTime.
    //
    // Returns:
    //     The System.DateTimeOffset equivalent of the supplied string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     The argument passed to this method is outside the range of allowable values.
    //     For information about allowable values, see System.DateTimeOffset.
    //
    //   System.FormatException:
    //     The argument passed to this method does not conform to a subset of the W3C
    //     Recommendations for the XML dateTime type. For more information see http://www.w3.org/TR/xmlschema-2/#dateTime.
    public static DateTimeOffset ToDateTimeOffset(string s)
    {
      Contract.Requires(s != null);

      return default(DateTimeOffset);
    }
    //
    // Summary:
    //     Converts the supplied System.String to a System.DateTimeOffset equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    //   format:
    //     The format from which s is converted. The format parameter can be any subset
    //     of the W3C Recommendation for the XML dateTime type. (For more information
    //     see http://www.w3.org/TR/xmlschema-2/#dateTime.) The string s is validated
    //     against this format.
    //
    // Returns:
    //     The System.DateTimeOffset equivalent of the supplied string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s or format is an empty string or is not in the specified format.
    public static DateTimeOffset ToDateTimeOffset(string s, string format)
    {
      Contract.Requires(s != null);
      Contract.Requires(s != String.Empty);
      Contract.Requires(format != String.Empty);

      return default(DateTimeOffset);
    }
    //
    // Summary:
    //     Converts the supplied System.String to a System.DateTimeOffset equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    //   formats:
    //     An array of formats from which s can be converted. Each format in formats
    //     can be any subset of the W3C Recommendation for the XML dateTime type. (For
    //     more information see http://www.w3.org/TR/xmlschema-2/#dateTime.) The string
    //     s is validated against one of these formats.
    //
    // Returns:
    //     The System.DateTimeOffset equivalent of the supplied string.
    public static DateTimeOffset ToDateTimeOffset(string s, string[] formats)
    {
      Contract.Requires(s != null);

      return default(DateTimeOffset);
    }
    //
    // Summary:
    //     Converts the System.String to a System.Decimal equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    // Returns:
    //     A Decimal equivalent of the string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not in the correct format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.Decimal.MinValue or greater than System.Decimal.MaxValue.
    public static decimal ToDecimal(string s)
    {
      Contract.Requires(s != null);

      return default(decimal);
    }
    //
    // Summary:
    //     Converts the System.String to a System.Double equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    // Returns:
    //     A Double equivalent of the string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not in the correct format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.Double.MinValue or greater than System.Double.MaxValue.
    public static double ToDouble(string s)
    {
      Contract.Requires(s != null);

      return default(double);
    }

    //
    // Summary:
    //     Converts the System.String to a System.Guid equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    // Returns:
    //     A Guid equivalent of the string.
    public static Guid ToGuid(string s)
    {
      Contract.Requires(s != null);

      return default(Guid);
    }

    //
    // Summary:
    //     Converts the System.String to a System.Int16 equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    // Returns:
    //     An Int16 equivalent of the string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not in the correct format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.Int16.MinValue or greater than System.Int16.MaxValue.
    public static short ToInt16(string s)
    {
      Contract.Requires(s != null);

      return default(short);
    }

    //
    // Summary:
    //     Converts the System.String to a System.Int32 equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    // Returns:
    //     An Int32 equivalent of the string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not in the correct format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.Int32.MinValue or greater than System.Int32.MaxValue.
    public static int ToInt32(string s)
    {
      Contract.Requires(s != null);

      return default(int);
    }

    //
    // Summary:
    //     Converts the System.String to a System.Int64 equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    // Returns:
    //     An Int64 equivalent of the string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not in the correct format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.Int64.MinValue or greater than System.Int64.MaxValue.
    public static long ToInt64(string s)
    {
      Contract.Requires(s != null);

      return default(long);
    }

    //
    // Summary:
    //     Converts the System.String to a System.SByte equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    // Returns:
    //     An SByte equivalent of the string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not in the correct format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.SByte.MinValue or greater than System.SByte.MaxValue.
    //[CLSCompliant(false)]
    public static sbyte ToSByte(string s)
    {
      Contract.Requires(s != null);

      return default(sbyte);
    }

    //
    // Summary:
    //     Converts the System.String to a System.Single equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    // Returns:
    //     A Single equivalent of the string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not in the correct format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.Single.MinValue or greater than System.Single.MaxValue.
    public static float ToSingle(string s)
    {
      Contract.Requires(s != null);

      return default(float);
    }

    //
    // Summary:
    //     Converts the System.Boolean to a System.String.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    //
    // Returns:
    //     A string representation of the Boolean, that is, "true" or "false".
    public static string ToString(bool value)
    {
      Contract.Ensures(!value || Contract.Result<string>() == "true");
      Contract.Ensures(value || Contract.Result<string>() == "false");

      return default(string);
    }
    //
    // Summary:
    //     Converts the System.Byte to a System.String.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    //
    // Returns:
    //     A string representation of the Byte.
#if !SILVERLIGHT
    public static string ToString(byte value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);


      return default(string);
    }
#endif
    //
    // Summary:
    //     Converts the System.Char to a System.String.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    //
    // Returns:
    //     A string representation of the Char.
    public static string ToString(char value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);

      return default(string);
    }
    //
    // Summary:
    //     Converts the System.DateTime to a System.String.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    //
    // Returns:
    //     A string representation of the DateTime in the format yyyy-MM-ddTHH:mm:ss
    //     where 'T' is a constant literal.
#if !SILVERLIGHT
    [Obsolete("Use XmlConvert.ToString() that takes in XmlDateTimeSerializationMode")]
    public static string ToString(DateTime value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);

      return default(string);
    }
#endif
    
    //
    // Summary:
    //     Converts the supplied System.DateTimeOffset to a System.String.
    //
    // Parameters:
    //   value:
    //     The System.DateTimeOffset to be converted.
    //
    // Returns:
    //     A System.String representation of the supplied System.DateTimeOffset.
    public static string ToString(DateTimeOffset value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);

      return default(string);
    }

    //
    // Summary:
    //     Converts the System.Decimal to a System.String.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    //
    // Returns:
    //     A string representation of the Decimal.
    public static string ToString(decimal value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);

      return default(string);
    }

    //
    // Summary:
    //     Converts the System.Double to a System.String.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    //
    // Returns:
    //     A string representation of the Double.
    public static string ToString(double value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);

      return default(string);
    }

    //
    // Summary:
    //     Converts the System.Single to a System.String.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    //
    // Returns:
    //     A string representation of the Single.
    public static string ToString(float value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);

      return default(string);
    }

    //
    // Summary:
    //     Converts the System.Guid to a System.String.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    //
    // Returns:
    //     A string representation of the Guid.
    public static string ToString(Guid value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);

      return default(string);
    }

    //
    // Summary:
    //     Converts the System.Int32 to a System.String.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    //
    // Returns:
    //     A string representation of the Int32.
    public static string ToString(int value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);

      return default(string);
    }

    //
    // Summary:
    //     Converts the System.Int64 to a System.String.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    //
    // Returns:
    //     A string representation of the Int64.
    public static string ToString(long value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);

      return default(string);
    }

    //
    // Summary:
    //     Converts the System.SByte to a System.String.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    //
    // Returns:
    //     A string representation of the SByte.
    //[CLSCompliant(false)]
    public static string ToString(sbyte value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);

      return default(string);
    }

    //
    // Summary:
    //     Converts the System.Int16 to a System.String.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    //
    // Returns:
    //     A string representation of the Int16.
    public static string ToString(short value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);

      return default(string);
    }

    //
    // Summary:
    //     Converts the System.TimeSpan to a System.String.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    //
    // Returns:
    //     A string representation of the TimeSpan.
    public static string ToString(TimeSpan value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);

      return default(string);
    }
    //
    // Summary:
    //     Converts the System.UInt32 to a System.String.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    //
    // Returns:
    //     A string representation of the UInt32.
    //[CLSCompliant(false)]
    #if !SILVERLIGHT
    public static string ToString(uint value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);

      return default(string);
    }
#endif
    //
    // Summary:
    //     Converts the System.UInt64 to a System.String.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    //
    // Returns:
    //     A string representation of the UInt64.
    //[CLSCompliant(false)]
    public static string ToString(ulong value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);

      return default(string);
    }

    //
    // Summary:
    //     Converts the System.UInt16 to a System.String.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    //
    // Returns:
    //     A string representation of the UInt16.
    //[CLSCompliant(false)]
#if !SILVERLIGHT
    public static string ToString(ushort value)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);

      return default(string);
    }
#endif
    //
    // Summary:
    //     Converts the System.DateTime to a System.String.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    //
    //   format:
    //     The format structure that defines how to display the converted string. Valid
    //     formats include "yyyy-MM-ddTHH:mm:sszzzzzz" and its subsets.
    //
    // Returns:
    //     A string representation of the DateTime in the specified format.
#if !SILVERLIGHT
    public static string ToString(DateTime value, string format)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);

      return default(string);
    }
#endif
    //
    // Summary:
    //     Converts the System.DateTime to a System.String using the System.Xml.XmlDateTimeSerializationMode
    //     specified.
    //
    // Parameters:
    //   value:
    //     The System.DateTime value to convert.
    //
    //   dateTimeOption:
    //     One of the System.Xml.XmlDateTimeSerializationMode values that specify how
    //     to treat the System.DateTime value.
    //
    // Returns:
    //     A System.String equivalent of the System.DateTime.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The dateTimeOption value is not valid.
    //
    //   System.ArgumentNullException:
    //     The value or dateTimeOption value is null.
    //public static string ToString(DateTime value, XmlDateTimeSerializationMode dateTimeOption);
    //
    // Summary:
    //     Converts the supplied System.DateTimeOffset to a System.String in the specified
    //     format.
    //
    // Parameters:
    //   value:
    //     The System.DateTimeOffset to be converted.
    //
    //   format:
    //     The format to which s is converted. The format parameter can be any subset
    //     of the W3C Recommendation for the XML dateTime type. (For more information
    //     see http://www.w3.org/TR/xmlschema-2/#dateTime.)
    //
    // Returns:
    //     A System.String representation in the specified format of the supplied System.DateTimeOffset.
    public static string ToString(DateTimeOffset value, string format)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length > 0);

      return default(string);
    }

    //
    // Summary:
    //     Converts the System.String to a System.TimeSpan equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert. The string format must conform to the W3C XML Schema
    //     Part 2: Datatypes recommendation for duration.
    //
    // Returns:
    //     A TimeSpan equivalent of the string.
    //
    // Exceptions:
    //   System.FormatException:
    //     s is not in correct format to represent a TimeSpan value.
    //extern public static TimeSpan ToTimeSpan(string s);

    //
    // Summary:
    //     Converts the System.String to a System.UInt16 equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    // Returns:
    //     A UInt16 equivalent of the string.
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
    public static ushort ToUInt16(string s)
    {
      Contract.Requires(s != null);

      return default(ushort);
    }

    //
    // Summary:
    //     Converts the System.String to a System.UInt32 equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    // Returns:
    //     A UInt32 equivalent of the string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not in the correct format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.UInt32.MinValue or greater than System.UInt32.MaxValue.
////    [CLSCompliant(false)]
    public static uint ToUInt32(string s)
    {
      Contract.Requires(s != null);

      return default(ushort);
    }

    //
    // Summary:
    //     Converts the System.String to a System.UInt64 equivalent.
    //
    // Parameters:
    //   s:
    //     The string to convert.
    //
    // Returns:
    //     A UInt64 equivalent of the string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     s is null.
    //
    //   System.FormatException:
    //     s is not in the correct format.
    //
    //   System.OverflowException:
    //     s represents a number less than System.UInt64.MinValue or greater than System.UInt64.MaxValue.
////    [CLSCompliant(false)]
    public static ulong ToUInt64(string s)
    {
      Contract.Requires(s != null);

      return default(ulong);
    }

    //
    // Summary:
    //     Verifies that the name is a valid name according to the W3C Extended Markup
    //     Language recommendation.
    //
    // Parameters:
    //   name:
    //     The name to verify.
    //
    // Returns:
    //     The name, if it is a valid XML name.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     name is not a valid XML name.
    //
    //   System.ArgumentNullException:
    //     name is null or String.Empty.
    public static string VerifyName(string name)
    {
      Contract.Requires(!string.IsNullOrEmpty(name));
      Contract.Ensures(Contract.Result<string>() == name);

      return default(string);
    }

    //
    // Summary:
    //     Verifies that the name is a valid NCName according to the W3C Extended Markup
    //     Language recommendation.
    //
    // Parameters:
    //   name:
    //     The name to verify.
    //
    // Returns:
    //     The name, if it is a valid NCName.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     name is null or String.Empty.
    //
    //   System.Xml.XmlException:
    //     name is not a valid NCName.
    public static string VerifyNCName(string name)
    {
      Contract.Requires(!string.IsNullOrEmpty(name));
      Contract.Ensures(Contract.Result<string>() == name);

      return default(string);

    }
    //
    // Summary:
    //     Verifies that the string is a valid NMTOKEN according to the W3C XML Schema
    //     Part2: Datatypes recommendation
    //
    // Parameters:
    //   name:
    //     The string you wish to verify.
    //
    // Returns:
    //     The name token, if it is a valid NMTOKEN.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     The string is not a valid name token.
    //
    //   System.ArgumentNullException:
    //     name is null.
    public static string VerifyNMTOKEN(string name)
    {
      Contract.Requires(name != null);

      return default(string);
    }
    //
    // Summary:
    //     Verifies that the string is a valid token according to the W3C XML Schema
    //     Part2: Datatypes recommendation.
    //
    // Parameters:
    //   token:
    //     The string value you wish to verify.
    //
    // Returns:
    //     The token, if it is a valid token.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     The string value is not a valid token.
    //extern public static string VerifyTOKEN(string token);
  }
}

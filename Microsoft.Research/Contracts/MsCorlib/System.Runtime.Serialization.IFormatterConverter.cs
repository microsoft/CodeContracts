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


#if !SILVERLIGHT

using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  // Summary:
  //     Provides the connection between an instance of System.Runtime.Serialization.SerializationInfo
  //     and the formatter-provided class best suited to parse the data inside the
  //     System.Runtime.Serialization.SerializationInfo.
  public interface IFormatterConverter
  {
    // Summary:
    //     Converts a value to the given System.Type.
    //
    // Parameters:
    //   value:
    //     The object to be converted.
    //
    //   type:
    //     The System.Type into which value is to be converted.
    //
    // Returns:
    //     The converted value.
    object Convert(object value, Type type);
    //
    // Summary:
    //     Converts a value to the given System.TypeCode.
    //
    // Parameters:
    //   value:
    //     The object to be converted.
    //
    //   typeCode:
    //     The System.TypeCode into which value is to be converted.
    //
    // Returns:
    //     The converted value.
    object Convert(object value, TypeCode typeCode);
    //
    // Summary:
    //     Converts a value to a System.Boolean.
    //
    // Parameters:
    //   value:
    //     The object to be converted.
    //
    // Returns:
    //     The converted value.
    bool ToBoolean(object value);
    //
    // Summary:
    //     Converts a value to an 8-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     The object to be converted.
    //
    // Returns:
    //     The converted value.
    byte ToByte(object value);
    //
    // Summary:
    //     Converts a value to a Unicode character.
    //
    // Parameters:
    //   value:
    //     The object to be converted.
    //
    // Returns:
    //     The converted value.
    char ToChar(object value);
    //
    // Summary:
    //     Converts a value to a System.DateTime.
    //
    // Parameters:
    //   value:
    //     The object to be converted.
    //
    // Returns:
    //     The converted value.
    DateTime ToDateTime(object value);
    //
    // Summary:
    //     Converts a value to a System.Decimal.
    //
    // Parameters:
    //   value:
    //     The object to be converted.
    //
    // Returns:
    //     The converted value.
    decimal ToDecimal(object value);
    //
    // Summary:
    //     Converts a value to a double-precision floating-point number.
    //
    // Parameters:
    //   value:
    //     The object to be converted.
    //
    // Returns:
    //     The converted value.
    double ToDouble(object value);
    //
    // Summary:
    //     Converts a value to a 16-bit signed integer.
    //
    // Parameters:
    //   value:
    //     The object to be converted.
    //
    // Returns:
    //     The converted value.
    short ToInt16(object value);
    //
    // Summary:
    //     Converts a value to a 32-bit signed integer.
    //
    // Parameters:
    //   value:
    //     The object to be converted.
    //
    // Returns:
    //     The converted value.
    int ToInt32(object value);
    //
    // Summary:
    //     Converts a value to a 64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     The object to be converted.
    //
    // Returns:
    //     The converted value.
    long ToInt64(object value);
    //
    // Summary:
    //     Converts a value to a System.SByte.
    //
    // Parameters:
    //   value:
    //     The object to be converted.
    //
    // Returns:
    //     The converted value.
    sbyte ToSByte(object value);
    //
    // Summary:
    //     Converts a value to a single-precision floating-point number.
    //
    // Parameters:
    //   value:
    //     The object to be converted.
    //
    // Returns:
    //     The converted value.
    float ToSingle(object value);
    //
    // Summary:
    //     Converts a value to a System.String.
    //
    // Parameters:
    //   value:
    //     The object to be converted.
    //
    // Returns:
    //     The converted value.
    string ToString(object value);
    //
    // Summary:
    //     Converts a value to a 16-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     The object to be converted.
    //
    // Returns:
    //     The converted value.
    ushort ToUInt16(object value);
    //
    // Summary:
    //     Converts a value to a 32-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     The object to be converted.
    //
    // Returns:
    //     The converted value.
    uint ToUInt32(object value);
    //
    // Summary:
    //     Converts a value to a 64-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     The object to be converted.
    //
    // Returns:
    //     The converted value.
    ulong ToUInt64(object value);
  }
}

#endif
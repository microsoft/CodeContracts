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

// File System.Convert.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System
{
  static public partial class Convert
  {
    #region Methods and constructors
    public static Object ChangeType(Object value, TypeCode typeCode)
    {
      return default(Object);
    }

    public static Object ChangeType(Object value, Type conversionType)
    {
      return default(Object);
    }

    public static Object ChangeType(Object value, TypeCode typeCode, IFormatProvider provider)
    {
      return default(Object);
    }

    public static Object ChangeType(Object value, Type conversionType, IFormatProvider provider)
    {
      return default(Object);
    }

    public static byte[] FromBase64CharArray(char[] inArray, int offset, int length)
    {
      return default(byte[]);
    }

    public static byte[] FromBase64String(string s)
    {
      return default(byte[]);
    }

    public static TypeCode GetTypeCode(Object value)
    {
      Contract.Ensures(((System.TypeCode)(0)) <= Contract.Result<System.TypeCode>());
      Contract.Ensures(Contract.Result<System.TypeCode>() <= ((System.TypeCode)(18)));

      return default(TypeCode);
    }

    public static bool IsDBNull(Object value)
    {
      return default(bool);
    }

    public static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut)
    {
      Contract.Ensures(0 <= Contract.Result<int>());

      return default(int);
    }

    public static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut, Base64FormattingOptions options)
    {
      Contract.Ensures(0 <= Contract.Result<int>());

      return default(int);
    }

    public static string ToBase64String(byte[] inArray, Base64FormattingOptions options)
    {
      return default(string);
    }

    public static string ToBase64String(byte[] inArray)
    {
      return default(string);
    }

    public static string ToBase64String(byte[] inArray, int offset, int length)
    {
      return default(string);
    }

    public static string ToBase64String(byte[] inArray, int offset, int length, Base64FormattingOptions options)
    {
      return default(string);
    }

    public static bool ToBoolean(Decimal value)
    {
      return default(bool);
    }

    public static bool ToBoolean(double value)
    {
      Contract.Ensures(Contract.Result<bool>() == ((value == 0) == false));

      return default(bool);
    }

    public static bool ToBoolean(DateTime value)
    {
      return default(bool);
    }

    public static bool ToBoolean(Object value)
    {
      return default(bool);
    }

    public static bool ToBoolean(Object value, IFormatProvider provider)
    {
      return default(bool);
    }

    public static bool ToBoolean(float value)
    {
      Contract.Ensures(Contract.Result<bool>() == ((value == 0) == false));

      return default(bool);
    }

    public static bool ToBoolean(byte value)
    {
      Contract.Ensures(Contract.Result<bool>() == ((value == 0) == false));

      return default(bool);
    }

    public static bool ToBoolean(short value)
    {
      Contract.Ensures(Contract.Result<bool>() == ((value == 0) == false));

      return default(bool);
    }

    public static bool ToBoolean(ushort value)
    {
      Contract.Ensures(Contract.Result<bool>() == ((value == 0) == false));

      return default(bool);
    }

    public static bool ToBoolean(bool value)
    {
      Contract.Ensures(Contract.Result<bool>() == value);

      return default(bool);
    }

    public static bool ToBoolean(sbyte value)
    {
      Contract.Ensures(Contract.Result<bool>() == ((value == 0) == false));

      return default(bool);
    }

    public static bool ToBoolean(char value)
    {
      return default(bool);
    }

    public static bool ToBoolean(int value)
    {
      Contract.Ensures(Contract.Result<bool>() == ((value == 0) == false));

      return default(bool);
    }

    public static bool ToBoolean(ulong value)
    {
      return default(bool);
    }

    public static bool ToBoolean(string value)
    {
      return default(bool);
    }

    public static bool ToBoolean(string value, IFormatProvider provider)
    {
      return default(bool);
    }

    public static bool ToBoolean(uint value)
    {
      Contract.Ensures(Contract.Result<bool>() == ((value == 0) == false));

      return default(bool);
    }

    public static bool ToBoolean(long value)
    {
      Contract.Ensures(Contract.Result<bool>() == ((value == (long)(0)) == false));

      return default(bool);
    }

    public static byte ToByte(bool value)
    {
      Contract.Ensures(0 <= Contract.Result<byte>());
      Contract.Ensures(Contract.Result<byte>() <= 1);

      return default(byte);
    }

    public static byte ToByte(byte value)
    {
      Contract.Ensures(Contract.Result<byte>() == value);

      return default(byte);
    }

    public static byte ToByte(Object value, IFormatProvider provider)
    {
      return default(byte);
    }

    public static byte ToByte(string value, int fromBase)
    {
      Contract.Ensures(0 <= Contract.Result<byte>());
      Contract.Ensures(Contract.Result<byte>() <= 255);

      return default(byte);
    }

    public static byte ToByte(Object value)
    {
      return default(byte);
    }

    public static byte ToByte(char value)
    {
      Contract.Ensures(Contract.Result<byte>() == (byte)(value));

      return default(byte);
    }

    public static byte ToByte(Decimal value)
    {
      return default(byte);
    }

    public static byte ToByte(double value)
    {
      return default(byte);
    }

    public static byte ToByte(float value)
    {
      return default(byte);
    }

    public static byte ToByte(DateTime value)
    {
      return default(byte);
    }

    public static byte ToByte(string value, IFormatProvider provider)
    {
      return default(byte);
    }

    public static byte ToByte(string value)
    {
      return default(byte);
    }

    public static byte ToByte(ulong value)
    {
      Contract.Ensures(Contract.Result<byte>() == (byte)(value));

      return default(byte);
    }

    public static byte ToByte(ushort value)
    {
      Contract.Ensures(Contract.Result<byte>() == (byte)(value));

      return default(byte);
    }

    public static byte ToByte(short value)
    {
      Contract.Ensures(Contract.Result<byte>() == (byte)(value));

      return default(byte);
    }

    public static byte ToByte(sbyte value)
    {
      Contract.Ensures(Contract.Result<byte>() == (byte)(value));

      return default(byte);
    }

    public static byte ToByte(long value)
    {
      Contract.Ensures(0 <= Contract.Result<byte>());
      Contract.Ensures(Contract.Result<byte>() == (byte)(value));

      return default(byte);
    }

    public static byte ToByte(uint value)
    {
      Contract.Ensures(Contract.Result<byte>() == (byte)(value));

      return default(byte);
    }

    public static byte ToByte(int value)
    {
      Contract.Ensures(Contract.Result<byte>() == (byte)(value));

      return default(byte);
    }

    public static char ToChar(long value)
    {
      return default(char);
    }

    public static char ToChar(uint value)
    {
      return default(char);
    }

    public static char ToChar(int value)
    {
      Contract.Ensures(Contract.Result<char>() == (unchecked(char)(value)));

      return default(char);
    }

    public static char ToChar(string value, IFormatProvider provider)
    {
      Contract.Ensures(value.Length == 1);

      return default(char);
    }

    public static char ToChar(string value)
    {
      Contract.Ensures(value.Length == 1);

      return default(char);
    }

    public static char ToChar(ulong value)
    {
      return default(char);
    }

    public static char ToChar(char value)
    {
      Contract.Ensures(Contract.Result<char>() == value);

      return default(char);
    }

    public static char ToChar(bool value)
    {
      return default(char);
    }

    public static char ToChar(Object value, IFormatProvider provider)
    {
      return default(char);
    }

    public static char ToChar(short value)
    {
      return default(char);
    }

    public static char ToChar(byte value)
    {
      return default(char);
    }

    public static char ToChar(sbyte value)
    {
      return default(char);
    }

    public static char ToChar(float value)
    {
      return default(char);
    }

    public static char ToChar(Object value)
    {
      return default(char);
    }

    public static char ToChar(ushort value)
    {
      return default(char);
    }

    public static char ToChar(DateTime value)
    {
      return default(char);
    }

    public static char ToChar(Decimal value)
    {
      return default(char);
    }

    public static char ToChar(double value)
    {
      return default(char);
    }

    public static DateTime ToDateTime(Object value)
    {
      return default(DateTime);
    }

    public static DateTime ToDateTime(ulong value)
    {
      return default(DateTime);
    }

    public static DateTime ToDateTime(byte value)
    {
      return default(DateTime);
    }

    public static DateTime ToDateTime(DateTime value)
    {
      return default(DateTime);
    }

    public static DateTime ToDateTime(int value)
    {
      return default(DateTime);
    }

    public static DateTime ToDateTime(ushort value)
    {
      return default(DateTime);
    }

    public static DateTime ToDateTime(short value)
    {
      return default(DateTime);
    }

    public static DateTime ToDateTime(long value)
    {
      return default(DateTime);
    }

    public static DateTime ToDateTime(uint value)
    {
      return default(DateTime);
    }

    public static DateTime ToDateTime(double value)
    {
      return default(DateTime);
    }

    public static DateTime ToDateTime(float value)
    {
      return default(DateTime);
    }

    public static DateTime ToDateTime(string value)
    {
      return default(DateTime);
    }

    public static DateTime ToDateTime(Decimal value)
    {
      return default(DateTime);
    }

    public static DateTime ToDateTime(char value)
    {
      return default(DateTime);
    }

    public static DateTime ToDateTime(Object value, IFormatProvider provider)
    {
      return default(DateTime);
    }

    public static DateTime ToDateTime(sbyte value)
    {
      return default(DateTime);
    }

    public static DateTime ToDateTime(bool value)
    {
      return default(DateTime);
    }

    public static DateTime ToDateTime(string value, IFormatProvider provider)
    {
      return default(DateTime);
    }

    public static Decimal ToDecimal(double value)
    {
      return default(Decimal);
    }

    public static Decimal ToDecimal(Object value)
    {
      return default(Decimal);
    }

    public static Decimal ToDecimal(sbyte value)
    {
      return default(Decimal);
    }

    public static Decimal ToDecimal(Object value, IFormatProvider provider)
    {
      return default(Decimal);
    }

    public static Decimal ToDecimal(int value)
    {
      return default(Decimal);
    }

    public static Decimal ToDecimal(ushort value)
    {
      return default(Decimal);
    }

    public static Decimal ToDecimal(short value)
    {
      return default(Decimal);
    }

    public static Decimal ToDecimal(uint value)
    {
      return default(Decimal);
    }

    public static Decimal ToDecimal(float value)
    {
      return default(Decimal);
    }

    public static Decimal ToDecimal(ulong value)
    {
      return default(Decimal);
    }

    public static Decimal ToDecimal(long value)
    {
      return default(Decimal);
    }

    public static Decimal ToDecimal(Decimal value)
    {
      return default(Decimal);
    }

    public static Decimal ToDecimal(bool value)
    {
      return default(Decimal);
    }

    public static Decimal ToDecimal(DateTime value)
    {
      return default(Decimal);
    }

    public static Decimal ToDecimal(string value, IFormatProvider provider)
    {
      return default(Decimal);
    }

    public static Decimal ToDecimal(char value)
    {
      return default(Decimal);
    }

    public static Decimal ToDecimal(byte value)
    {
      return default(Decimal);
    }

    public static Decimal ToDecimal(string value)
    {
      return default(Decimal);
    }

    public static double ToDouble(sbyte value)
    {
      Contract.Ensures(Contract.Result<double>() == (double)(value));

      return default(double);
    }

    public static double ToDouble(ushort value)
    {
      Contract.Ensures(Contract.Result<double>() == (double)(value));

      return default(double);
    }

    public static double ToDouble(char value)
    {
      return default(double);
    }

    public static double ToDouble(uint value)
    {
      Contract.Ensures(Contract.Result<double>() == (double)((double)(value)));

      return default(double);
    }

    public static double ToDouble(int value)
    {
      Contract.Ensures(Contract.Result<double>() == (double)(value));

      return default(double);
    }

    public static double ToDouble(Object value, IFormatProvider provider)
    {
      return default(double);
    }

    public static double ToDouble(byte value)
    {
      Contract.Ensures(Contract.Result<double>() == (double)(value));

      return default(double);
    }

    public static double ToDouble(short value)
    {
      Contract.Ensures(Contract.Result<double>() == (double)(value));

      return default(double);
    }

    public static double ToDouble(Object value)
    {
      return default(double);
    }

    public static double ToDouble(long value)
    {
      Contract.Ensures(Contract.Result<double>() == (double)(value));

      return default(double);
    }

    public static double ToDouble(string value, IFormatProvider provider)
    {
      return default(double);
    }

    public static double ToDouble(string value)
    {
      return default(double);
    }

    public static double ToDouble(DateTime value)
    {
      return default(double);
    }

    public static double ToDouble(bool value)
    {
      return default(double);
    }

    public static double ToDouble(float value)
    {
      Contract.Ensures(Contract.Result<double>() == (double)(value));

      return default(double);
    }

    public static double ToDouble(ulong value)
    {
      Contract.Ensures(Contract.Result<double>() == (double)((double)(value)));

      return default(double);
    }

    public static double ToDouble(Decimal value)
    {
      return default(double);
    }

    public static double ToDouble(double value)
    {
      Contract.Ensures(Contract.Result<double>() == value);

      return default(double);
    }

    public static short ToInt16(long value)
    {
      Contract.Ensures(Contract.Result<short>() == (short)(value));

      return default(short);
    }

    public static short ToInt16(ulong value)
    {
      Contract.Ensures(Contract.Result<short>() == (short)(value));

      return default(short);
    }

    public static short ToInt16(short value)
    {
      Contract.Ensures(Contract.Result<short>() == value);

      return default(short);
    }

    public static short ToInt16(int value)
    {
      Contract.Ensures(Contract.Result<short>() == ((short)(value)));
      Contract.Ensures(Contract.Result<short>() == (short)(value));

      return default(short);
    }

    public static short ToInt16(uint value)
    {
      Contract.Ensures(Contract.Result<short>() == (short)(value));

      return default(short);
    }

    public static short ToInt16(DateTime value)
    {
      return default(short);
    }

    public static short ToInt16(string value, IFormatProvider provider)
    {
      return default(short);
    }

    public static short ToInt16(Decimal value)
    {
      return default(short);
    }

    public static short ToInt16(float value)
    {
      return default(short);
    }

    public static short ToInt16(double value)
    {
      return default(short);
    }

    public static short ToInt16(Object value)
    {
      return default(short);
    }

    public static short ToInt16(Object value, IFormatProvider provider)
    {
      return default(short);
    }

    public static short ToInt16(string value)
    {
      return default(short);
    }

    public static short ToInt16(string value, int fromBase)
    {
      Contract.Ensures(-32768 <= Contract.Result<short>());
      Contract.Ensures(Contract.Result<short>() <= 32767);

      return default(short);
    }

    public static short ToInt16(bool value)
    {
      Contract.Ensures(0 <= Contract.Result<short>());
      Contract.Ensures(Contract.Result<short>() <= 1);

      return default(short);
    }

    public static short ToInt16(byte value)
    {
      return default(short);
    }

    public static short ToInt16(ushort value)
    {
      Contract.Ensures(Contract.Result<short>() == (short)(value));

      return default(short);
    }

    public static short ToInt16(char value)
    {
      Contract.Ensures(Contract.Result<short>() == (short)(value));

      return default(short);
    }

    public static short ToInt16(sbyte value)
    {
      return default(short);
    }

    public static int ToInt32(string value, int fromBase)
    {
      return default(int);
    }

    public static int ToInt32(long value)
    {
      Contract.Ensures(Contract.Result<int>() == (int)(value));
      Contract.Ensures(Contract.Result<int>() == value);

      return default(int);
    }

    public static int ToInt32(string value, IFormatProvider provider)
    {
      return default(int);
    }

    public static int ToInt32(string value)
    {
      return default(int);
    }

    public static int ToInt32(int value)
    {
      Contract.Ensures(Contract.Result<int>() == value);

      return default(int);
    }

    public static int ToInt32(DateTime value)
    {
      return default(int);
    }

    public static int ToInt32(float value)
    {
      return default(int);
    }

    public static int ToInt32(ulong value)
    {
      Contract.Ensures(Contract.Result<int>() == (int)(value));
      Contract.Ensures(Contract.Result<int>() == value);

      return default(int);
    }

    public static int ToInt32(Decimal value)
    {
      return default(int);
    }

    public static int ToInt32(double value)
    {
      return default(int);
    }

    public static int ToInt32(uint value)
    {
      Contract.Ensures(Contract.Result<int>() <= 2147483647);
      Contract.Ensures(Contract.Result<int>() == value);

      return default(int);
    }

    public static int ToInt32(bool value)
    {
      Contract.Ensures(0 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 1);

      return default(int);
    }

    public static int ToInt32(char value)
    {
      Contract.Ensures(Contract.Result<int>() == value);

      return default(int);
    }

    public static int ToInt32(Object value)
    {
      return default(int);
    }

    public static int ToInt32(Object value, IFormatProvider provider)
    {
      return default(int);
    }

    public static int ToInt32(short value)
    {
      Contract.Ensures(Contract.Result<int>() == value);

      return default(int);
    }

    public static int ToInt32(ushort value)
    {
      Contract.Ensures(Contract.Result<int>() == value);

      return default(int);
    }

    public static int ToInt32(sbyte value)
    {
      Contract.Ensures(Contract.Result<int>() == value);

      return default(int);
    }

    public static int ToInt32(byte value)
    {
      Contract.Ensures(Contract.Result<int>() == value);

      return default(int);
    }

    public static long ToInt64(DateTime value)
    {
      return default(long);
    }

    public static long ToInt64(string value, IFormatProvider provider)
    {
      Contract.Ensures(-9223372036854775808 <= Contract.Result<long>());
      Contract.Ensures(Contract.Result<long>() <= 9223372036854775807);

      return default(long);
    }

    public static long ToInt64(string value, int fromBase)
    {
      return default(long);
    }

    public static long ToInt64(byte value)
    {
      return default(long);
    }

    public static long ToInt64(bool value)
    {
      Contract.Ensures(0 <= Contract.Result<long>());
      Contract.Ensures(Contract.Result<long>() <= 1);

      return default(long);
    }

    public static long ToInt64(char value)
    {
      return default(long);
    }

    public static long ToInt64(sbyte value)
    {
      Contract.Ensures(Contract.Result<long>() == (long)(value));

      return default(long);
    }

    public static long ToInt64(double value)
    {
      Contract.Ensures(-9223372036854775807 <= Contract.Result<long>());
      Contract.Ensures(-9223372036854775808 <= Contract.Result<long>());
      Contract.Ensures(Contract.Result<long>() <= 9223372036854775807);

      return default(long);
    }

    public static long ToInt64(Object value)
    {
      Contract.Ensures(-9223372036854775808 <= Contract.Result<long>());
      Contract.Ensures(Contract.Result<long>() <= 9223372036854775807);

      return default(long);
    }

    public static long ToInt64(Object value, IFormatProvider provider)
    {
      Contract.Ensures(-9223372036854775808 <= Contract.Result<long>());
      Contract.Ensures(Contract.Result<long>() <= 9223372036854775807);

      return default(long);
    }

    public static long ToInt64(short value)
    {
      Contract.Ensures(Contract.Result<long>() == (long)(value));

      return default(long);
    }

    public static long ToInt64(ulong value)
    {
      Contract.Ensures(Contract.Result<long>() <= 9223372036854775807);

      return default(long);
    }

    public static long ToInt64(long value)
    {
      Contract.Ensures(Contract.Result<long>() == value);

      return default(long);
    }

    public static long ToInt64(float value)
    {
      Contract.Ensures(-9223372036854775807 <= Contract.Result<long>());

      return default(long);
    }

    public static long ToInt64(ushort value)
    {
      return default(long);
    }

    public static long ToInt64(int value)
    {
      Contract.Ensures(-2147483648 <= Contract.Result<long>());
      Contract.Ensures(Contract.Result<long>() <= 2147483647);
      Contract.Ensures(Contract.Result<long>() == (long)(value));
      Contract.Ensures(Contract.Result<long>() == value);

      return default(long);
    }

    public static long ToInt64(uint value)
    {
      return default(long);
    }

    public static long ToInt64(string value)
    {
      Contract.Ensures(-9223372036854775808 <= Contract.Result<long>());
      Contract.Ensures(Contract.Result<long>() <= 9223372036854775807);

      return default(long);
    }

    public static long ToInt64(Decimal value)
    {
      return default(long);
    }

    public static sbyte ToSByte(byte value)
    {
      Contract.Ensures(Contract.Result<sbyte>() <= 127);
      Contract.Ensures(Contract.Result<sbyte>() == (sbyte)(value));

      return default(sbyte);
    }

    public static sbyte ToSByte(char value)
    {
      Contract.Ensures(Contract.Result<sbyte>() <= 127);
      Contract.Ensures(Contract.Result<sbyte>() == (sbyte)(value));

      return default(sbyte);
    }

    public static sbyte ToSByte(ushort value)
    {
      Contract.Ensures(Contract.Result<sbyte>() <= 127);
      Contract.Ensures(Contract.Result<sbyte>() == (sbyte)(value));

      return default(sbyte);
    }

    public static sbyte ToSByte(short value)
    {
      Contract.Ensures(-128 <= Contract.Result<sbyte>());
      Contract.Ensures(Contract.Result<sbyte>() <= 127);
      Contract.Ensures(Contract.Result<sbyte>() == (sbyte)(value));

      return default(sbyte);
    }

    public static sbyte ToSByte(Object value, IFormatProvider provider)
    {
      return default(sbyte);
    }

    public static sbyte ToSByte(Decimal value)
    {
      return default(sbyte);
    }

    public static sbyte ToSByte(sbyte value)
    {
      Contract.Ensures(Contract.Result<sbyte>() == value);

      return default(sbyte);
    }

    public static sbyte ToSByte(bool value)
    {
      Contract.Ensures(0 <= Contract.Result<sbyte>());
      Contract.Ensures(Contract.Result<sbyte>() <= 1);

      return default(sbyte);
    }

    public static sbyte ToSByte(int value)
    {
      Contract.Ensures(-128 <= Contract.Result<sbyte>());
      Contract.Ensures(Contract.Result<sbyte>() <= 127);
      Contract.Ensures(Contract.Result<sbyte>() == (sbyte)(value));

      return default(sbyte);
    }

    public static sbyte ToSByte(ulong value)
    {
      Contract.Ensures(Contract.Result<sbyte>() <= 127);
      Contract.Ensures(Contract.Result<sbyte>() == (sbyte)(value));

      return default(sbyte);
    }

    public static sbyte ToSByte(string value)
    {
      return default(sbyte);
    }

    public static sbyte ToSByte(double value)
    {
      return default(sbyte);
    }

    public static sbyte ToSByte(string value, int fromBase)
    {
      Contract.Ensures(-128 <= Contract.Result<sbyte>());
      Contract.Ensures(Contract.Result<sbyte>() <= 127);

      return default(sbyte);
    }

    public static sbyte ToSByte(float value)
    {
      return default(sbyte);
    }

    public static sbyte ToSByte(long value)
    {
      Contract.Ensures(-128 <= Contract.Result<sbyte>());
      Contract.Ensures(Contract.Result<sbyte>() <= 127);
      Contract.Ensures(Contract.Result<sbyte>() == (sbyte)(value));

      return default(sbyte);
    }

    public static sbyte ToSByte(uint value)
    {
      Contract.Ensures(Contract.Result<sbyte>() <= 127);
      Contract.Ensures(Contract.Result<sbyte>() == (sbyte)(value));

      return default(sbyte);
    }

    public static sbyte ToSByte(Object value)
    {
      return default(sbyte);
    }

    public static sbyte ToSByte(string value, IFormatProvider provider)
    {
      return default(sbyte);
    }

    public static sbyte ToSByte(DateTime value)
    {
      return default(sbyte);
    }

    public static float ToSingle(ushort value)
    {
      Contract.Ensures(Contract.Result<float>() == (float)(value));

      return default(float);
    }

    public static float ToSingle(short value)
    {
      Contract.Ensures(Contract.Result<float>() == (float)(value));

      return default(float);
    }

    public static float ToSingle(uint value)
    {
      Contract.Ensures(Contract.Result<float>() == (float)((double)(value)));

      return default(float);
    }

    public static float ToSingle(int value)
    {
      Contract.Ensures(Contract.Result<float>() == (float)(value));

      return default(float);
    }

    public static float ToSingle(char value)
    {
      return default(float);
    }

    public static float ToSingle(Object value, IFormatProvider provider)
    {
      return default(float);
    }

    public static float ToSingle(Object value)
    {
      return default(float);
    }

    public static float ToSingle(byte value)
    {
      Contract.Ensures(Contract.Result<float>() == (float)(value));

      return default(float);
    }

    public static float ToSingle(sbyte value)
    {
      Contract.Ensures(Contract.Result<float>() == (float)(value));

      return default(float);
    }

    public static float ToSingle(bool value)
    {
      return default(float);
    }

    public static float ToSingle(DateTime value)
    {
      return default(float);
    }

    public static float ToSingle(string value)
    {
      return default(float);
    }

    public static float ToSingle(string value, IFormatProvider provider)
    {
      return default(float);
    }

    public static float ToSingle(Decimal value)
    {
      return default(float);
    }

    public static float ToSingle(ulong value)
    {
      Contract.Ensures(Contract.Result<float>() == (float)((double)(value)));

      return default(float);
    }

    public static float ToSingle(long value)
    {
      Contract.Ensures(Contract.Result<float>() == (float)(value));

      return default(float);
    }

    public static float ToSingle(double value)
    {
      Contract.Ensures(Contract.Result<float>() == (float)(value));

      return default(float);
    }

    public static float ToSingle(float value)
    {
      Contract.Ensures(Contract.Result<float>() == value);

      return default(float);
    }

    public static string ToString(ushort value, IFormatProvider provider)
    {
      return default(string);
    }

    public static string ToString(ushort value)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(string);
    }

    public static string ToString(short value)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(string);
    }

    public static string ToString(short value, IFormatProvider provider)
    {
      return default(string);
    }

    public static string ToString(uint value)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(string);
    }

    public static string ToString(uint value, IFormatProvider provider)
    {
      return default(string);
    }

    public static string ToString(int value)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(string);
    }

    public static string ToString(int value, IFormatProvider provider)
    {
      return default(string);
    }

    public static string ToString(byte value, IFormatProvider provider)
    {
      return default(string);
    }

    public static string ToString(bool value)
    {
      return default(string);
    }

    public static string ToString(bool value, IFormatProvider provider)
    {
      return default(string);
    }

    public static string ToString(Object value)
    {
      return default(string);
    }

    public static string ToString(Object value, IFormatProvider provider)
    {
      return default(string);
    }

    public static string ToString(char value)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static string ToString(sbyte value, IFormatProvider provider)
    {
      return default(string);
    }

    public static string ToString(byte value)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(string);
    }

    public static string ToString(char value, IFormatProvider provider)
    {
      return default(string);
    }

    public static string ToString(sbyte value)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(string);
    }

    public static string ToString(DateTime value, IFormatProvider provider)
    {
      return default(string);
    }

    public static string ToString(string value)
    {
      Contract.Ensures(Contract.Result<string>() == value);

      return default(string);
    }

    public static string ToString(Decimal value, IFormatProvider provider)
    {
      return default(string);
    }

    public static string ToString(DateTime value)
    {
      return default(string);
    }

    public static string ToString(string value, IFormatProvider provider)
    {
      Contract.Ensures(Contract.Result<string>() == value);

      return default(string);
    }

    public static string ToString(int value, int toBase)
    {
      return default(string);
    }

    public static string ToString(long value, int toBase)
    {
      return default(string);
    }

    public static string ToString(byte value, int toBase)
    {
      return default(string);
    }

    public static string ToString(short value, int toBase)
    {
      return default(string);
    }

    public static string ToString(ulong value)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(string);
    }

    public static string ToString(ulong value, IFormatProvider provider)
    {
      return default(string);
    }

    public static string ToString(long value)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(string);
    }

    public static string ToString(long value, IFormatProvider provider)
    {
      return default(string);
    }

    public static string ToString(float value)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(string);
    }

    public static string ToString(Decimal value)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(string);
    }

    public static string ToString(double value, IFormatProvider provider)
    {
      return default(string);
    }

    public static string ToString(double value)
    {
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(string);
    }

    public static string ToString(float value, IFormatProvider provider)
    {
      return default(string);
    }

    public static ushort ToUInt16(string value, int fromBase)
    {
      Contract.Ensures(0 <= Contract.Result<ushort>());
      Contract.Ensures(Contract.Result<ushort>() <= 65535);

      return default(ushort);
    }

    public static ushort ToUInt16(Decimal value)
    {
      return default(ushort);
    }

    public static ushort ToUInt16(long value)
    {
      Contract.Ensures(Contract.Result<ushort>() == (ushort)(value));

      return default(ushort);
    }

    public static ushort ToUInt16(ulong value)
    {
      Contract.Ensures(Contract.Result<ushort>() == (ushort)(value));

      return default(ushort);
    }

    public static ushort ToUInt16(ushort value)
    {
      Contract.Ensures(Contract.Result<ushort>() == value);

      return default(ushort);
    }

    public static ushort ToUInt16(short value)
    {
      Contract.Ensures(Contract.Result<ushort>() == (ushort)(value));

      return default(ushort);
    }

    public static ushort ToUInt16(int value)
    {
      Contract.Ensures(Contract.Result<ushort>() == (unchecked(ushort)(value)));
      Contract.Ensures(Contract.Result<ushort>() == (ushort)(value));

      return default(ushort);
    }

    public static ushort ToUInt16(string value, IFormatProvider provider)
    {
      return default(ushort);
    }

    public static ushort ToUInt16(DateTime value)
    {
      return default(ushort);
    }

    public static ushort ToUInt16(string value)
    {
      return default(ushort);
    }

    public static ushort ToUInt16(float value)
    {
      return default(ushort);
    }

    public static ushort ToUInt16(double value)
    {
      return default(ushort);
    }

    public static ushort ToUInt16(Object value, IFormatProvider provider)
    {
      return default(ushort);
    }

    public static ushort ToUInt16(bool value)
    {
      Contract.Ensures(0 <= Contract.Result<ushort>());
      Contract.Ensures(Contract.Result<ushort>() <= 1);

      return default(ushort);
    }

    public static ushort ToUInt16(uint value)
    {
      Contract.Ensures(Contract.Result<ushort>() == (ushort)(value));

      return default(ushort);
    }

    public static ushort ToUInt16(Object value)
    {
      return default(ushort);
    }

    public static ushort ToUInt16(byte value)
    {
      return default(ushort);
    }

    public static ushort ToUInt16(char value)
    {
      return default(ushort);
    }

    public static ushort ToUInt16(sbyte value)
    {
      Contract.Ensures(Contract.Result<ushort>() == (ushort)(value));

      return default(ushort);
    }

    public static uint ToUInt32(int value)
    {
      Contract.Ensures(0 <= Contract.Result<uint>());
      Contract.Ensures(Contract.Result<uint>() <= 2147483647);
      Contract.Ensures(Contract.Result<uint>() == (unchecked(uint)(value)));

      return default(uint);
    }

    public static uint ToUInt32(float value)
    {
      return default(uint);
    }

    public static uint ToUInt32(ulong value)
    {
      Contract.Ensures(Contract.Result<uint>() <= 4294967295);
      Contract.Ensures(Contract.Result<uint>() == (uint)(value));

      return default(uint);
    }

    public static uint ToUInt32(long value)
    {
      Contract.Ensures(Contract.Result<uint>() <= 4294967295);
      Contract.Ensures(Contract.Result<uint>() == (uint)(value));

      return default(uint);
    }

    public static uint ToUInt32(uint value)
    {
      Contract.Ensures(Contract.Result<uint>() == value);

      return default(uint);
    }

    public static uint ToUInt32(double value)
    {
      return default(uint);
    }

    public static uint ToUInt32(Decimal value)
    {
      return default(uint);
    }

    public static uint ToUInt32(string value)
    {
      return default(uint);
    }

    public static uint ToUInt32(DateTime value)
    {
      return default(uint);
    }

    public static uint ToUInt32(string value, int fromBase)
    {
      return default(uint);
    }

    public static uint ToUInt32(string value, IFormatProvider provider)
    {
      return default(uint);
    }

    public static uint ToUInt32(ushort value)
    {
      return default(uint);
    }

    public static uint ToUInt32(bool value)
    {
      Contract.Ensures(0 <= Contract.Result<uint>());
      Contract.Ensures(Contract.Result<uint>() <= 1);

      return default(uint);
    }

    public static uint ToUInt32(Object value)
    {
      return default(uint);
    }

    public static uint ToUInt32(Object value, IFormatProvider provider)
    {
      return default(uint);
    }

    public static uint ToUInt32(char value)
    {
      return default(uint);
    }

    public static uint ToUInt32(short value)
    {
      Contract.Ensures(0 <= Contract.Result<uint>());

      return default(uint);
    }

    public static uint ToUInt32(byte value)
    {
      return default(uint);
    }

    public static uint ToUInt32(sbyte value)
    {
      Contract.Ensures(0 <= Contract.Result<uint>());

      return default(uint);
    }

    public static ulong ToUInt64(bool value)
    {
      Contract.Ensures(0 <= Contract.Result<ulong>());
      Contract.Ensures(Contract.Result<ulong>() <= 1);

      return default(ulong);
    }

    public static ulong ToUInt64(Object value, IFormatProvider provider)
    {
      Contract.Ensures(0 <= Contract.Result<ulong>());

      return default(ulong);
    }

    public static ulong ToUInt64(string value)
    {
      Contract.Ensures(0 <= Contract.Result<ulong>());

      return default(ulong);
    }

    public static ulong ToUInt64(Object value)
    {
      Contract.Ensures(0 <= Contract.Result<ulong>());

      return default(ulong);
    }

    public static ulong ToUInt64(ushort value)
    {
      Contract.Ensures(Contract.Result<ulong>() == (ulong)(value));

      return default(ulong);
    }

    public static ulong ToUInt64(sbyte value)
    {
      return default(ulong);
    }

    public static ulong ToUInt64(char value)
    {
      Contract.Ensures(Contract.Result<ulong>() == (ulong)(value));

      return default(ulong);
    }

    public static ulong ToUInt64(short value)
    {
      return default(ulong);
    }

    public static ulong ToUInt64(byte value)
    {
      Contract.Ensures(Contract.Result<ulong>() == (ulong)(value));

      return default(ulong);
    }

    public static ulong ToUInt64(Decimal value)
    {
      return default(ulong);
    }

    public static ulong ToUInt64(float value)
    {
      Contract.Ensures(Contract.Result<ulong>() <= 4294967295);

      return default(ulong);
    }

    public static ulong ToUInt64(DateTime value)
    {
      return default(ulong);
    }

    public static ulong ToUInt64(double value)
    {
      Contract.Ensures(0 <= Contract.Result<ulong>());
      Contract.Ensures(Contract.Result<ulong>() <= 4294967295);

      return default(ulong);
    }

    public static ulong ToUInt64(string value, IFormatProvider provider)
    {
      Contract.Ensures(0 <= Contract.Result<ulong>());

      return default(ulong);
    }

    public static ulong ToUInt64(int value)
    {
      Contract.Ensures(Contract.Result<ulong>() <= 2147483647);
      Contract.Ensures(Contract.Result<ulong>() == (unchecked(ulong)(value)));

      return default(ulong);
    }

    public static ulong ToUInt64(string value, int fromBase)
    {
      Contract.Ensures(Contract.Result<ulong>() <= 9223372036854775807);

      return default(ulong);
    }

    public static ulong ToUInt64(uint value)
    {
      Contract.Ensures(Contract.Result<ulong>() == (ulong)(value));

      return default(ulong);
    }

    public static ulong ToUInt64(ulong value)
    {
      Contract.Ensures(Contract.Result<ulong>() == value);

      return default(ulong);
    }

    public static ulong ToUInt64(long value)
    {
      Contract.Ensures(Contract.Result<ulong>() <= 9223372036854775807);

      return default(ulong);
    }
    #endregion

    #region Fields
    public readonly static Object DBNull;
    #endregion
  }
}

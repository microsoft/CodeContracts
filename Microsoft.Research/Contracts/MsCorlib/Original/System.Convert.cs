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

using System.Diagnostics.Contracts;
using System;

namespace System
{

    public class Convert
    {

        public static Byte[] FromBase64CharArray (Char[] arg0, int arg1, int arg2) {

          return default(Byte[]);
        }
        public static int ToBase64CharArray (Byte[] arg0, int arg1, int arg2, Char[] arg3, int arg4) {

          return default(int);
        }
        public static Byte[] FromBase64String (string arg0) {

          return default(Byte[]);
        }
        public static string ToBase64String (Byte[] arg0, int arg1, int arg2) {

          return default(string);
        }
        public static string ToBase64String (Byte[]! inArray) {
            CodeContract.Requires(inArray != null);

          return default(string);
        }
        public static string ToString (Int64 value, int toBase) {
            CodeContract.Requires(toBase == 2 || toBase == 8 || toBase == 10 || toBase == 16);

          return default(string);
        }
        public static string ToString (int value, int toBase) {
            CodeContract.Requires(toBase == 2 || toBase == 8 || toBase == 10 || toBase == 16);

          return default(string);
        }
        public static string ToString (Int16 value, int toBase) {
            CodeContract.Requires(toBase == 2 || toBase == 8 || toBase == 10 || toBase == 16);

          return default(string);
        }
        public static string ToString (byte value, int toBase) {
            CodeContract.Requires(toBase == 2 || toBase == 8 || toBase == 10 || toBase == 16);

          return default(string);
        }
        public static UInt64 ToUInt64 (string value, int fromBase) {
            CodeContract.Requires(fromBase == 2 || fromBase == 8 || fromBase == 10 || fromBase == 16);

          return default(UInt64);
        }
        public static Int64 ToInt64 (string value, int fromBase) {
            CodeContract.Requires(fromBase == 2 || fromBase == 8 || fromBase == 10 || fromBase == 16);

          return default(Int64);
        }
        public static UInt32 ToUInt32 (string value, int fromBase) {
            CodeContract.Requires(fromBase == 2 || fromBase == 8 || fromBase == 10 || fromBase == 16);

          return default(UInt32);
        }
        public static int ToInt32 (string value, int fromBase) {
            CodeContract.Requires(fromBase == 2 || fromBase == 8 || fromBase == 10 || fromBase == 16);

          return default(int);
        }
        public static UInt16 ToUInt16 (string value, int fromBase) {
            CodeContract.Requires(fromBase == 2 || fromBase == 8 || fromBase == 10 || fromBase == 16);

          return default(UInt16);
        }
        public static Int16 ToInt16 (string value, int fromBase) {
            CodeContract.Requires(fromBase == 2 || fromBase == 8 || fromBase == 10 || fromBase == 16);

          return default(Int16);
        }
        public static SByte ToSByte (string value, int fromBase) {
            CodeContract.Requires(fromBase == 2 || fromBase == 8 || fromBase == 10 || fromBase == 16);

          return default(SByte);
        }
        public static byte ToByte (string value, int fromBase) {
            CodeContract.Requires(fromBase == 2 || fromBase == 8 || fromBase == 10 || fromBase == 16);

          return default(byte);
        }
        public static string ToString (string value, IFormatProvider provider) {

          return default(string);
        }
        public static string ToString (string value) {

          return default(string);
        }
        public static string ToString (DateTime value, IFormatProvider provider) {

          return default(string);
        }
        public static string ToString (DateTime value) {

          return default(string);
        }
        public static string ToString (Decimal value, IFormatProvider provider) {

          return default(string);
        }
        public static string ToString (Decimal value) {

          return default(string);
        }
        public static string ToString (double value, IFormatProvider provider) {

          return default(string);
        }
        public static string ToString (double value) {

          return default(string);
        }
        public static string ToString (Single value, IFormatProvider provider) {

          return default(string);
        }
        public static string ToString (Single value) {

          return default(string);
        }
        public static string ToString (UInt64 value, IFormatProvider provider) {

          return default(string);
        }
        public static string ToString (UInt64 value) {

          return default(string);
        }
        public static string ToString (Int64 value, IFormatProvider provider) {

          return default(string);
        }
        public static string ToString (Int64 value) {

          return default(string);
        }
        public static string ToString (UInt32 value, IFormatProvider provider) {

          return default(string);
        }
        public static string ToString (UInt32 value) {

          return default(string);
        }
        public static string ToString (int value, IFormatProvider provider) {

          return default(string);
        }
        public static string ToString (int value) {

          return default(string);
        }
        public static string ToString (UInt16 value, IFormatProvider provider) {

          return default(string);
        }
        public static string ToString (UInt16 value) {

          return default(string);
        }
        public static string ToString (Int16 value, IFormatProvider provider) {

          return default(string);
        }
        public static string ToString (Int16 value) {

          return default(string);
        }
        public static string ToString (byte value, IFormatProvider provider) {

          return default(string);
        }
        public static string ToString (byte value) {

          return default(string);
        }
        public static string ToString (SByte value, IFormatProvider provider) {

          return default(string);
        }
        public static string ToString (SByte value) {

          return default(string);
        }
        public static string ToString (Char value, IFormatProvider provider) {

          return default(string);
        }
        public static string ToString (Char value) {

          return default(string);
        }
        public static string ToString (bool value, IFormatProvider provider) {

          return default(string);
        }
        public static string ToString (bool value) {

          return default(string);
        }
        public static string ToString (object value, IFormatProvider provider) {

          return default(string);
        }
        public static string ToString (object value) {

          return default(string);
        }
        public static DateTime ToDateTime (Decimal value) {

          return default(DateTime);
        }
        public static DateTime ToDateTime (double value) {

          return default(DateTime);
        }
        public static DateTime ToDateTime (Single value) {

          return default(DateTime);
        }
        public static DateTime ToDateTime (Char value) {

          return default(DateTime);
        }
        public static DateTime ToDateTime (bool value) {

          return default(DateTime);
        }
        public static DateTime ToDateTime (UInt64 value) {

          return default(DateTime);
        }
        public static DateTime ToDateTime (Int64 value) {

          return default(DateTime);
        }
        public static DateTime ToDateTime (UInt32 value) {

          return default(DateTime);
        }
        public static DateTime ToDateTime (int value) {

          return default(DateTime);
        }
        public static DateTime ToDateTime (UInt16 value) {

          return default(DateTime);
        }
        public static DateTime ToDateTime (Int16 value) {

          return default(DateTime);
        }
        public static DateTime ToDateTime (byte value) {

          return default(DateTime);
        }
        public static DateTime ToDateTime (SByte value) {

          return default(DateTime);
        }
        public static DateTime ToDateTime (string value, IFormatProvider provider) {

          return default(DateTime);
        }
        public static DateTime ToDateTime (string value) {

          return default(DateTime);
        }
        public static DateTime ToDateTime (object value, IFormatProvider provider) {

          return default(DateTime);
        }
        public static DateTime ToDateTime (object value) {

          return default(DateTime);
        }
        public static DateTime ToDateTime (DateTime value) {

          return default(DateTime);
        }
        public static Decimal ToDecimal (DateTime value) {

          return default(Decimal);
        }
        public static Decimal ToDecimal (bool value) {

          return default(Decimal);
        }
        public static Decimal ToDecimal (Decimal value) {

          return default(Decimal);
        }
        public static Decimal ToDecimal (string value, IFormatProvider provider) {

          return default(Decimal);
        }
        public static Decimal ToDecimal (string value) {

          return default(Decimal);
        }
        public static Decimal ToDecimal (double value) {

          return default(Decimal);
        }
        public static Decimal ToDecimal (Single value) {

          return default(Decimal);
        }
        public static Decimal ToDecimal (UInt64 value) {

          return default(Decimal);
        }
        public static Decimal ToDecimal (Int64 value) {

          return default(Decimal);
        }
        public static Decimal ToDecimal (UInt32 value) {

          return default(Decimal);
        }
        public static Decimal ToDecimal (int value) {

          return default(Decimal);
        }
        public static Decimal ToDecimal (UInt16 value) {

          return default(Decimal);
        }
        public static Decimal ToDecimal (Int16 value) {

          return default(Decimal);
        }
        public static Decimal ToDecimal (Char value) {

          return default(Decimal);
        }
        public static Decimal ToDecimal (byte value) {

          return default(Decimal);
        }
        public static Decimal ToDecimal (SByte value) {

          return default(Decimal);
        }
        public static Decimal ToDecimal (object value, IFormatProvider provider) {

          return default(Decimal);
        }
        public static Decimal ToDecimal (object value) {

          return default(Decimal);
        }
        public static double ToDouble (DateTime value) {

          return default(double);
        }
        public static double ToDouble (bool value) {

          return default(double);
        }
        public static double ToDouble (string value, IFormatProvider provider) {

          return default(double);
        }
        public static double ToDouble (string value) {

          return default(double);
        }
        public static double ToDouble (Decimal value) {

          return default(double);
        }
        public static double ToDouble (double value) {

          return default(double);
        }
        public static double ToDouble (Single value) {

          return default(double);
        }
        public static double ToDouble (UInt64 value) {

          return default(double);
        }
        public static double ToDouble (Int64 value) {

          return default(double);
        }
        public static double ToDouble (UInt32 value) {

          return default(double);
        }
        public static double ToDouble (int value) {

          return default(double);
        }
        public static double ToDouble (UInt16 value) {

          return default(double);
        }
        public static double ToDouble (Char value) {

          return default(double);
        }
        public static double ToDouble (Int16 value) {

          return default(double);
        }
        public static double ToDouble (byte value) {

          return default(double);
        }
        public static double ToDouble (SByte value) {

          return default(double);
        }
        public static double ToDouble (object value, IFormatProvider provider) {

          return default(double);
        }
        public static double ToDouble (object value) {

          return default(double);
        }
        public static Single ToSingle (DateTime value) {

          return default(Single);
        }
        public static Single ToSingle (bool value) {

          return default(Single);
        }
        public static Single ToSingle (string value, IFormatProvider provider) {

          return default(Single);
        }
        public static Single ToSingle (string value) {

          return default(Single);
        }
        public static Single ToSingle (Decimal value) {

          return default(Single);
        }
        public static Single ToSingle (double value) {

          return default(Single);
        }
        public static Single ToSingle (Single value) {

          return default(Single);
        }
        public static Single ToSingle (UInt64 value) {

          return default(Single);
        }
        public static Single ToSingle (Int64 value) {

          return default(Single);
        }
        public static Single ToSingle (UInt32 value) {

          return default(Single);
        }
        public static Single ToSingle (int value) {

          return default(Single);
        }
        public static Single ToSingle (UInt16 value) {

          return default(Single);
        }
        public static Single ToSingle (Int16 value) {

          return default(Single);
        }
        public static Single ToSingle (Char value) {

          return default(Single);
        }
        public static Single ToSingle (byte value) {

          return default(Single);
        }
        public static Single ToSingle (SByte value) {

          return default(Single);
        }
        public static Single ToSingle (object value, IFormatProvider provider) {

          return default(Single);
        }
        public static Single ToSingle (object value) {

          return default(Single);
        }
        public static UInt64 ToUInt64 (DateTime value) {

          return default(UInt64);
        }
        public static UInt64 ToUInt64 (string value, IFormatProvider provider) {

          return default(UInt64);
        }
        public static UInt64 ToUInt64 (string value) {

          return default(UInt64);
        }
        public static UInt64 ToUInt64 (Decimal value) {

          return default(UInt64);
        }
        public static UInt64 ToUInt64 (double value) {
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value < 0);

          return default(UInt64);
        }
        public static UInt64 ToUInt64 (Single value) {

          return default(UInt64);
        }
        public static UInt64 ToUInt64 (UInt64 value) {

          return default(UInt64);
        }
        public static UInt64 ToUInt64 (Int64 value) {
            CodeContract.Requires(value >= 0);

          return default(UInt64);
        }
        public static UInt64 ToUInt64 (UInt32 value) {

          return default(UInt64);
        }
        public static UInt64 ToUInt64 (int value) {
            CodeContract.Requires(value >= 0);

          return default(UInt64);
        }
        public static UInt64 ToUInt64 (UInt16 value) {

          return default(UInt64);
        }
        public static UInt64 ToUInt64 (Int16 value) {
            CodeContract.Requires(value >= 0);

          return default(UInt64);
        }
        public static UInt64 ToUInt64 (byte value) {

          return default(UInt64);
        }
        public static UInt64 ToUInt64 (SByte value) {
            CodeContract.Requires(value >= 0);

          return default(UInt64);
        }
        public static UInt64 ToUInt64 (Char value) {

          return default(UInt64);
        }
        public static UInt64 ToUInt64 (bool value) {

          return default(UInt64);
        }
        public static UInt64 ToUInt64 (object value, IFormatProvider provider) {

          return default(UInt64);
        }
        public static UInt64 ToUInt64 (object value) {

          return default(UInt64);
        }
        public static Int64 ToInt64 (DateTime value) {

          return default(Int64);
        }
        public static Int64 ToInt64 (string value, IFormatProvider provider) {

          return default(Int64);
        }
        public static Int64 ToInt64 (string value) {

          return default(Int64);
        }
        public static Int64 ToInt64 (Decimal value) {

          return default(Int64);
        }
        public static Int64 ToInt64 (double value) {
            CodeContract.Requires(value < 0 || value < 0);
            CodeContract.Requires(value > 0);

          return default(Int64);
        }
        public static Int64 ToInt64 (Single value) {

          return default(Int64);
        }
        public static Int64 ToInt64 (Int64 value) {

          return default(Int64);
        }
        public static Int64 ToInt64 (UInt64 value) {
            CodeContract.Requires(value <= 4294967295);

          return default(Int64);
        }
        public static Int64 ToInt64 (UInt32 value) {

          return default(Int64);
        }
        public static Int64 ToInt64 (int value) {

          return default(Int64);
        }
        public static Int64 ToInt64 (UInt16 value) {

          return default(Int64);
        }
        public static Int64 ToInt64 (Int16 value) {

          return default(Int64);
        }
        public static Int64 ToInt64 (byte value) {

          return default(Int64);
        }
        public static Int64 ToInt64 (SByte value) {

          return default(Int64);
        }
        public static Int64 ToInt64 (Char value) {

          return default(Int64);
        }
        public static Int64 ToInt64 (bool value) {

          return default(Int64);
        }
        public static Int64 ToInt64 (object value, IFormatProvider provider) {

          return default(Int64);
        }
        public static Int64 ToInt64 (object value) {

          return default(Int64);
        }
        public static UInt32 ToUInt32 (DateTime value) {

          return default(UInt32);
        }
        public static UInt32 ToUInt32 (string value, IFormatProvider provider) {

          return default(UInt32);
        }
        public static UInt32 ToUInt32 (string value) {

          return default(UInt32);
        }
        public static UInt32 ToUInt32 (Decimal value) {

          return default(UInt32);
        }
        public static UInt32 ToUInt32 (double value) {
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value < 0);

          return default(UInt32);
        }
        public static UInt32 ToUInt32 (Single value) {

          return default(UInt32);
        }
        public static UInt32 ToUInt32 (UInt64 value) {
            CodeContract.Requires(value <= 0xFFFFFFFF);

          return default(UInt32);
        }
        public static UInt32 ToUInt32 (Int64 value) {
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value <= 0xFFFFFFFF);

          return default(UInt32);
        }
        public static UInt32 ToUInt32 (UInt32 value) {

          return default(UInt32);
        }
        public static UInt32 ToUInt32 (int value) {
            CodeContract.Requires(value >= 0);

          return default(UInt32);
        }
        public static UInt32 ToUInt32 (UInt16 value) {

          return default(UInt32);
        }
        public static UInt32 ToUInt32 (Int16 value) {
            CodeContract.Requires(value >= 0);

          return default(UInt32);
        }
        public static UInt32 ToUInt32 (byte value) {

          return default(UInt32);
        }
        public static UInt32 ToUInt32 (SByte value) {
            CodeContract.Requires(value >= 0);

          return default(UInt32);
        }
        public static UInt32 ToUInt32 (Char value) {

          return default(UInt32);
        }
        public static UInt32 ToUInt32 (bool value) {

          return default(UInt32);
        }
        public static UInt32 ToUInt32 (object value, IFormatProvider provider) {

          return default(UInt32);
        }
        public static UInt32 ToUInt32 (object value) {

          return default(UInt32);
        }
        public static int ToInt32 (DateTime value) {

          return default(int);
        }
        public static int ToInt32 (string value, IFormatProvider provider) {

          return default(int);
        }
        public static int ToInt32 (string value) {

          return default(int);
        }
        public static int ToInt32 (Decimal value) {

          return default(int);
        }
        public static int ToInt32 (double value) {
            CodeContract.Requires(value < 0 || value < 0);
            CodeContract.Requires(value >= 0);

          return default(int);
        }
        public static int ToInt32 (Single value) {

          return default(int);
        }
        public static int ToInt32 (UInt64 value) {
            CodeContract.Requires(value <= 2147483647);

          return default(int);
        }
        public static int ToInt32 (Int64 value) {
            CodeContract.Requires(value >= -2147483648);
            CodeContract.Requires(value <= 2147483647);

          return default(int);
        }
        public static int ToInt32 (int value) {

          return default(int);
        }
        public static int ToInt32 (UInt32 value) {
            CodeContract.Requires(value <= 2147483647);

          return default(int);
        }
        public static int ToInt32 (UInt16 value) {

          return default(int);
        }
        public static int ToInt32 (Int16 value) {

          return default(int);
        }
        public static int ToInt32 (byte value) {

          return default(int);
        }
        public static int ToInt32 (SByte value) {

          return default(int);
        }
        public static int ToInt32 (Char value) {

          return default(int);
        }
        public static int ToInt32 (bool value) {

          return default(int);
        }
        public static int ToInt32 (object value, IFormatProvider provider) {

          return default(int);
        }
        public static int ToInt32 (object value) {

          return default(int);
        }
        public static UInt16 ToUInt16 (DateTime value) {

          return default(UInt16);
        }
        public static UInt16 ToUInt16 (string value, IFormatProvider provider) {

          return default(UInt16);
        }
        public static UInt16 ToUInt16 (string value) {

          return default(UInt16);
        }
        public static UInt16 ToUInt16 (Decimal value) {

          return default(UInt16);
        }
        public static UInt16 ToUInt16 (double value) {

          return default(UInt16);
        }
        public static UInt16 ToUInt16 (Single value) {

          return default(UInt16);
        }
        public static UInt16 ToUInt16 (UInt64 value) {
            CodeContract.Requires(value <= 65535);

          return default(UInt16);
        }
        public static UInt16 ToUInt16 (Int64 value) {
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value <= 65535);

          return default(UInt16);
        }
        public static UInt16 ToUInt16 (UInt32 value) {
            CodeContract.Requires(value <= 65535);

          return default(UInt16);
        }
        public static UInt16 ToUInt16 (UInt16 value) {

          return default(UInt16);
        }
        public static UInt16 ToUInt16 (int value) {
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value <= 65535);

          return default(UInt16);
        }
        public static UInt16 ToUInt16 (Int16 value) {
            CodeContract.Requires(value >= 0);

          return default(UInt16);
        }
        public static UInt16 ToUInt16 (byte value) {

          return default(UInt16);
        }
        public static UInt16 ToUInt16 (SByte value) {
            CodeContract.Requires(value >= 0);

          return default(UInt16);
        }
        public static UInt16 ToUInt16 (Char value) {

          return default(UInt16);
        }
        public static UInt16 ToUInt16 (bool value) {

          return default(UInt16);
        }
        public static UInt16 ToUInt16 (object value, IFormatProvider provider) {

          return default(UInt16);
        }
        public static UInt16 ToUInt16 (object value) {

          return default(UInt16);
        }
        public static Int16 ToInt16 (DateTime value) {

          return default(Int16);
        }
        public static Int16 ToInt16 (string value, IFormatProvider provider) {

          return default(Int16);
        }
        public static Int16 ToInt16 (string value) {

          return default(Int16);
        }
        public static Int16 ToInt16 (Decimal value) {

          return default(Int16);
        }
        public static Int16 ToInt16 (double value) {

          return default(Int16);
        }
        public static Int16 ToInt16 (Single value) {

          return default(Int16);
        }
        public static Int16 ToInt16 (UInt64 value) {
            CodeContract.Requires(value <= 32767);

          return default(Int16);
        }
        public static Int16 ToInt16 (Int64 value) {
            CodeContract.Requires(value >= -32768);
            CodeContract.Requires(value <= 32767);

          return default(Int16);
        }
        public static Int16 ToInt16 (Int16 value) {

          return default(Int16);
        }
        public static Int16 ToInt16 (UInt32 value) {
            CodeContract.Requires(value <= 32767);

          return default(Int16);
        }
        public static Int16 ToInt16 (int value) {
            CodeContract.Requires(value >= -32768);
            CodeContract.Requires(value <= 32767);

          return default(Int16);
        }
        public static Int16 ToInt16 (UInt16 value) {
            CodeContract.Requires(value <= 32767);

          return default(Int16);
        }
        public static Int16 ToInt16 (byte value) {

          return default(Int16);
        }
        public static Int16 ToInt16 (SByte value) {

          return default(Int16);
        }
        public static Int16 ToInt16 (char value) {
            CodeContract.Requires(value <= (char)32767);

          return default(Int16);
        }
        public static Int16 ToInt16 (bool value) {

          return default(Int16);
        }
        public static Int16 ToInt16 (object value, IFormatProvider provider) {

          return default(Int16);
        }
        public static Int16 ToInt16 (object value) {

          return default(Int16);
        }
        public static byte ToByte (DateTime value) {

          return default(byte);
        }
        public static byte ToByte (string value, IFormatProvider provider) {

          return default(byte);
        }
        public static byte ToByte (string value) {

          return default(byte);
        }
        public static byte ToByte (Decimal value) {

          return default(byte);
        }
        public static byte ToByte (double value) {

          return default(byte);
        }
        public static byte ToByte (Single value) {

          return default(byte);
        }
        public static byte ToByte (UInt64 value) {
            CodeContract.Requires(value <= 255);

          return default(byte);
        }
        public static byte ToByte (Int64 value) {
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value <= 255);

          return default(byte);
        }
        public static byte ToByte (UInt32 value) {
            CodeContract.Requires(value <= 255);

          return default(byte);
        }
        public static byte ToByte (int value) {
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value <= 255);

          return default(byte);
        }
        public static byte ToByte (UInt16 value) {
            CodeContract.Requires(value <= 255);

          return default(byte);
        }
        public static byte ToByte (Int16 value) {
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value <= 255);

          return default(byte);
        }
        public static byte ToByte (SByte value) {
            CodeContract.Requires(value >= 0);

          return default(byte);
        }
        public static byte ToByte (char value) {
            CodeContract.Requires(value <= (char)255);

          return default(byte);
        }
        public static byte ToByte (byte value) {

          return default(byte);
        }
        public static byte ToByte (bool value) {

          return default(byte);
        }
        public static byte ToByte (object value, IFormatProvider provider) {

          return default(byte);
        }
        public static byte ToByte (object value) {

          return default(byte);
        }
        public static SByte ToSByte (DateTime value) {

          return default(SByte);
        }
        public static SByte ToSByte (string value, IFormatProvider provider) {

          return default(SByte);
        }
        public static SByte ToSByte (string value) {

          return default(SByte);
        }
        public static SByte ToSByte (Decimal value) {

          return default(SByte);
        }
        public static SByte ToSByte (double value) {

          return default(SByte);
        }
        public static SByte ToSByte (Single value) {

          return default(SByte);
        }
        public static SByte ToSByte (UInt64 value) {
            CodeContract.Requires(value <= 127);

          return default(SByte);
        }
        public static SByte ToSByte (Int64 value) {
            CodeContract.Requires(value >= -128);
            CodeContract.Requires(value <= 127);

          return default(SByte);
        }
        public static SByte ToSByte (UInt32 value) {
            CodeContract.Requires(value <= 127);

          return default(SByte);
        }
        public static SByte ToSByte (int value) {
            CodeContract.Requires(value >= -128);
            CodeContract.Requires(value <= 127);

          return default(SByte);
        }
        public static SByte ToSByte (UInt16 value) {
            CodeContract.Requires(value <= 127);

          return default(SByte);
        }
        public static SByte ToSByte (Int16 value) {
            CodeContract.Requires(value >= -128);
            CodeContract.Requires(value <= 127);

          return default(SByte);
        }
        public static SByte ToSByte (byte value) {
            CodeContract.Requires(value <= 127);

          return default(SByte);
        }
        public static SByte ToSByte (char value) {
            CodeContract.Requires(value <= (char)127);

          return default(SByte);
        }
        public static SByte ToSByte (SByte value) {

          return default(SByte);
        }
        public static SByte ToSByte (bool value) {

          return default(SByte);
        }
        public static SByte ToSByte (object value, IFormatProvider provider) {

          return default(SByte);
        }
        public static SByte ToSByte (object value) {

          return default(SByte);
        }
        public static Char ToChar (DateTime value) {

          return default(Char);
        }
        public static Char ToChar (Decimal value) {

          return default(Char);
        }
        public static Char ToChar (double value) {

          return default(Char);
        }
        public static Char ToChar (Single value) {

          return default(Char);
        }
        public static Char ToChar (string value, IFormatProvider provider) {

          return default(Char);
        }
        public static Char ToChar (string! value) {
            CodeContract.Requires(value != null);
            CodeContract.Requires(value.Length == 1);

          return default(Char);
        }
        public static Char ToChar (UInt64 value) {
            CodeContract.Requires(value <= 65535);

          return default(Char);
        }
        public static Char ToChar (Int64 value) {
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value <= 65535);

          return default(Char);
        }
        public static Char ToChar (UInt32 value) {
            CodeContract.Requires(value <= 65535);

          return default(Char);
        }
        public static Char ToChar (int value) {
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value <= 65535);

          return default(Char);
        }
        public static Char ToChar (UInt16 value) {

          return default(Char);
        }
        public static Char ToChar (Int16 value) {
            CodeContract.Requires(value >= 0);

          return default(Char);
        }
        public static Char ToChar (byte value) {

          return default(Char);
        }
        public static Char ToChar (SByte value) {
            CodeContract.Requires(value >= 0);

          return default(Char);
        }
        public static Char ToChar (Char value) {

          return default(Char);
        }
        public static Char ToChar (bool value) {

          return default(Char);
        }
        public static Char ToChar (object value, IFormatProvider provider) {

          return default(Char);
        }
        public static Char ToChar (object value) {

          return default(Char);
        }
        public static bool ToBoolean (DateTime value) {

          return default(bool);
        }
        public static bool ToBoolean (Decimal value) {

          return default(bool);
        }
        public static bool ToBoolean (double value) {

          return default(bool);
        }
        public static bool ToBoolean (Single value) {

          return default(bool);
        }
        public static bool ToBoolean (string value, IFormatProvider provider) {

          return default(bool);
        }
        public static bool ToBoolean (string value) {

          return default(bool);
        }
        public static bool ToBoolean (UInt64 value) {

          return default(bool);
        }
        public static bool ToBoolean (Int64 value) {

          return default(bool);
        }
        public static bool ToBoolean (UInt32 value) {

          return default(bool);
        }
        public static bool ToBoolean (int value) {

          return default(bool);
        }
        public static bool ToBoolean (UInt16 value) {

          return default(bool);
        }
        public static bool ToBoolean (Int16 value) {

          return default(bool);
        }
        public static bool ToBoolean (byte value) {

          return default(bool);
        }
        public static bool ToBoolean (Char value) {

          return default(bool);
        }
        public static bool ToBoolean (SByte value) {

          return default(bool);
        }
        public static bool ToBoolean (bool value) {

          return default(bool);
        }
        public static bool ToBoolean (object value, IFormatProvider provider) {

          return default(bool);
        }
        public static bool ToBoolean (object value) {

          return default(bool);
        }
        public static object ChangeType (object value, Type! conversionType, IFormatProvider provider) {

          CodeContract.Ensures(CodeContract.Result<object>() != null);
          return default(object);
        }
        public static object ChangeType (object value, Type! conversionType) {

          CodeContract.Ensures(CodeContract.Result<object>() != null);
          return default(object);
        }
        public static object ChangeType (object value, TypeCode typeCode, IFormatProvider provider) {
            CodeContract.Requires((int)typeCode != 0);

          CodeContract.Ensures(CodeContract.Result<object>() != null);
          return default(object);
        }
        public static object ChangeType (object value, TypeCode typeCode) {

          CodeContract.Ensures(CodeContract.Result<object>() != null);
          return default(object);
        }
        public static bool IsDBNull (object value) {

          return default(bool);
        }
        public static TypeCode GetTypeCode (object value) {
          return default(TypeCode);
        }
    }
}

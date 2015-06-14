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

    public class BitConverter
    {

        public static double Int64BitsToDouble (Int64 value) {

          return default(double);
        }
        public static Int64 DoubleToInt64Bits (double value) {

          return default(Int64);
        }
        public static bool ToBoolean (Byte[]! value, int startIndex) {
            CodeContract.Requires(value != null);
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(startIndex <= (value.Length - 1));

          return default(bool);
        }
        public static string ToString (Byte[]! value, int startIndex) {
            CodeContract.Requires(value != null);

          return default(string);
        }
        public static string ToString (Byte[]! value) {
            CodeContract.Requires(value != null);

          return default(string);
        }
        public static string ToString (Byte[] arg0, int arg1, int arg2) {

          return default(string);
        }
        public static double ToDouble (Byte[] arg0, int arg1) {

          return default(double);
        }
        public static Single ToSingle (Byte[] arg0, int arg1) {

          return default(Single);
        }
        public static UInt64 ToUInt64 (Byte[] arg0, int arg1) {

          return default(UInt64);
        }
        public static UInt32 ToUInt32 (Byte[] arg0, int arg1) {

          return default(UInt32);
        }
        public static UInt16 ToUInt16 (Byte[] arg0, int arg1) {

          return default(UInt16);
        }
        public static Int64 ToInt64 (Byte[] arg0, int arg1) {

          return default(Int64);
        }
        public static int ToInt32 (Byte[] arg0, int arg1) {

          return default(int);
        }
        public static Int16 ToInt16 (Byte[] arg0, int arg1) {

          return default(Int16);
        }
        public static Char ToChar (Byte[] arg0, int arg1) {

          return default(Char);
        }
        public static Byte[] GetBytes (double value) {

          CodeContract.Ensures(CodeContract.Result<Byte[]>() != null);
          return default(Byte[]);
        }
        public static Byte[] GetBytes (Single value) {

          CodeContract.Ensures(CodeContract.Result<Byte[]>() != null);
          return default(Byte[]);
        }
        public static Byte[] GetBytes (UInt64 value) {

          CodeContract.Ensures(CodeContract.Result<Byte[]>() != null);
          return default(Byte[]);
        }
        public static Byte[] GetBytes (UInt32 value) {

          CodeContract.Ensures(CodeContract.Result<Byte[]>() != null);
          return default(Byte[]);
        }
        public static Byte[] GetBytes (UInt16 value) {

          CodeContract.Ensures(CodeContract.Result<Byte[]>() != null);
          return default(Byte[]);
        }
        public static Byte[] GetBytes (Int64 arg0) {

          CodeContract.Ensures(CodeContract.Result<Byte[]>() != null);
          return default(Byte[]);
        }
        public static Byte[] GetBytes (int arg0) {

          CodeContract.Ensures(CodeContract.Result<Byte[]>() != null);
          return default(Byte[]);
        }
        public static Byte[] GetBytes (Int16 arg0) {

          CodeContract.Ensures(CodeContract.Result<Byte[]>() != null);
          return default(Byte[]);
        }
        public static Byte[] GetBytes (Char arg0) {

          CodeContract.Ensures(CodeContract.Result<Byte[]>() != null);
          return default(Byte[]);
        }
          CodeContract.Ensures(CodeContract.Result<Byte[]>() != null);
          return default(Byte[]);
        }
        public static Byte[] GetBytes (bool value) {
    }
}

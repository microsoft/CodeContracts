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

namespace System.Runtime.Serialization
{

    public class FormatterConverter
    {

        public string ToString (object! value) {
            CodeContract.Requires(value != null);

          return default(string);
        }
        public DateTime ToDateTime (object! value) {
            CodeContract.Requires(value != null);

          return default(DateTime);
        }
        public Decimal ToDecimal (object! value) {
            CodeContract.Requires(value != null);

          return default(Decimal);
        }
        public double ToDouble (object! value) {
            CodeContract.Requires(value != null);

          return default(double);
        }
        public Single ToSingle (object! value) {
            CodeContract.Requires(value != null);

          return default(Single);
        }
        public UInt64 ToUInt64 (object! value) {
            CodeContract.Requires(value != null);

          return default(UInt64);
        }
        public Int64 ToInt64 (object! value) {
            CodeContract.Requires(value != null);

          return default(Int64);
        }
        public UInt32 ToUInt32 (object! value) {
            CodeContract.Requires(value != null);

          return default(UInt32);
        }
        public int ToInt32 (object! value) {
            CodeContract.Requires(value != null);

          return default(int);
        }
        public UInt16 ToUInt16 (object! value) {
            CodeContract.Requires(value != null);

          return default(UInt16);
        }
        public Int16 ToInt16 (object! value) {
            CodeContract.Requires(value != null);

          return default(Int16);
        }
        public byte ToByte (object! value) {
            CodeContract.Requires(value != null);

          return default(byte);
        }
        public SByte ToSByte (object! value) {
            CodeContract.Requires(value != null);

          return default(SByte);
        }
        public Char ToChar (object! value) {
            CodeContract.Requires(value != null);

          return default(Char);
        }
        public bool ToBoolean (object! value) {
            CodeContract.Requires(value != null);

          return default(bool);
        }
        public object Convert (object! value, TypeCode typeCode) {
            CodeContract.Requires(value != null);

          return default(object);
        }
        public object Convert (object! value, Type type) {
            CodeContract.Requires(value != null);

          return default(object);
        }
        public FormatterConverter () {
          return default(FormatterConverter);
        }
    }
}

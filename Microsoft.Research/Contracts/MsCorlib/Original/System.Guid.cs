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

namespace System
{
    
    public struct Guid
    {

        [Pure]
        public string ToString (string format, IFormatProvider provider) {

          return default(string);
        }
        [Pure]
        public string ToString (string format) {

          return default(string);
        }
        public static Guid NewGuid () {

          return default(Guid);
        }
        [Pure]
        public static bool operator != (Guid a, Guid b) {

          return default(bool);
        }
        [Pure]
        public static bool operator == (Guid a, Guid b) {

          return default(bool);
        }
        [Pure]
        public int CompareTo (object value) {

          return default(int);
        }
        [Pure]
        public bool Equals (object o) {

          return default(bool);
        }
        public int GetHashCode () {

          return default(int);
        }
        [Pure]
        public string ToString () {

          return default(string);
        }
        [Pure]
        public Byte[] ToByteArray () {

          return default(Byte[]);
        }
        public Guid (int a, Int16 b, Int16 c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k) {

          return default(Guid);
        }
        public Guid (int a, Int16 b, Int16 c, Byte[]! d) {
            CodeContract.Requires(d != null);
            CodeContract.Requires(d.Length == 8);

          return default(Guid);
        }
        public Guid (string! g) {
            CodeContract.Requires(g != null);

          return default(Guid);
        }
        public Guid (UInt32 a, UInt16 b, UInt16 c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k) {

          return default(Guid);
        }
        public Guid (Byte[]! b) {
            CodeContract.Requires(b != null);
            CodeContract.Requires(b.Length == 16);
          return default(Guid);
        }
    }
}

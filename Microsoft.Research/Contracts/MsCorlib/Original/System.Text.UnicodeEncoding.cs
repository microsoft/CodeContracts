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

namespace System.Text
{

    public class UnicodeEncoding
    {

        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int GetHashCode () {

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public bool Equals (object value) {

          return default(bool);
        }
        public int GetMaxCharCount (int byteCount) {
            CodeContract.Requires(byteCount >= 0);

          return default(int);
        }
        public int GetMaxByteCount (int charCount) {
            CodeContract.Requires(charCount >= 0);

          return default(int);
        }
        public Byte[] GetPreamble () {

          return default(Byte[]);
        }
        public Decoder GetDecoder () {

          return default(Decoder);
        }
        public int GetChars (Byte[]! bytes, int byteIndex, int byteCount, Char[]! chars, int charIndex) {
            CodeContract.Requires(bytes != null);
            CodeContract.Requires(chars != null);
            CodeContract.Requires(byteIndex >= 0);
            CodeContract.Requires(byteCount >= 0);
            CodeContract.Requires((bytes.Length - byteIndex) >= byteCount);
            CodeContract.Requires(charIndex >= 0);
            CodeContract.Requires(charIndex <= chars.Length);

          return default(int);
        }
        public int GetCharCount (Byte[]! bytes, int index, int count) {
            CodeContract.Requires(bytes != null);
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires((bytes.Length - index) >= count);

          return default(int);
        }
        public int GetBytes (string! s, int charIndex, int charCount, Byte[]! bytes, int byteIndex) {
            CodeContract.Requires(s != null);
            CodeContract.Requires(bytes != null);
            CodeContract.Requires(charIndex >= 0);
            CodeContract.Requires(charCount >= 0);
            CodeContract.Requires((s.Length - charIndex) >= charCount);
            CodeContract.Requires(byteIndex >= 0);
            CodeContract.Requires(byteIndex <= bytes.Length);

          return default(int);
        }
        public Byte[] GetBytes (string! s) {
            CodeContract.Requires(s != null);

          return default(Byte[]);
        }
        public int GetBytes (Char[]! chars, int charIndex, int charCount, Byte[]! bytes, int byteIndex) {
            CodeContract.Requires(chars != null);
            CodeContract.Requires(bytes != null);
            CodeContract.Requires(charIndex >= 0);
            CodeContract.Requires(charCount >= 0);
            CodeContract.Requires((chars.Length - charIndex) >= charCount);
            CodeContract.Requires(byteIndex >= 0);
            CodeContract.Requires(byteIndex <= bytes.Length);

          return default(int);
        }
        public int GetByteCount (string! s) {
            CodeContract.Requires(s != null);

          return default(int);
        }
        public int GetByteCount (Char[]! chars, int index, int count) {
            CodeContract.Requires(chars != null);
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires((chars.Length - index) >= count);

          return default(int);
        }
        public UnicodeEncoding (bool bigEndian, bool byteOrderMark) {

          return default(UnicodeEncoding);
        }
        public UnicodeEncoding () {
          return default(UnicodeEncoding);
        }
    }
}

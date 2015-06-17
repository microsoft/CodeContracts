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

namespace System.Text
{

    public class UTF7Encoding
    {

        public int GetMaxCharCount (int byteCount) {
            Contract.Requires(byteCount >= 0);

          return default(int);
        }
        public int GetMaxByteCount (int charCount) {
            Contract.Requires(charCount >= 0);

          return default(int);
        }
        public Encoder GetEncoder () {

          return default(Encoder);
        }
        public Decoder GetDecoder () {

          return default(Decoder);
        }
        public int GetChars (Byte[] bytes, int byteIndex, int byteCount, Char[] chars, int charIndex) {

          return default(int);
        }
        public int GetCharCount (Byte[] bytes, int index, int count) {

          return default(int);
        }
        public int GetBytes (Char[] chars, int charIndex, int charCount, Byte[] bytes, int byteIndex) {

          return default(int);
        }
        public int GetByteCount (Char[] chars, int index, int count) {

          return default(int);
        }
        public UTF7Encoding (bool allowOptionals) {

          return default(UTF7Encoding);
        }
        public UTF7Encoding () {
          return default(UTF7Encoding);
        }
    }
}

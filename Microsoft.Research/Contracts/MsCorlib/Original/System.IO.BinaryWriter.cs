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

namespace System.IO
{

    public class BinaryWriter
    {

        public Stream BaseStream
        {
          get;
        }

        public void Write (string! value) {
            CodeContract.Requires(value != null);

        }
        public void Write (Single value) {

        }
        public void Write (UInt64 value) {

        }
        public void Write (Int64 value) {

        }
        public void Write (UInt32 value) {

        }
        public void Write (int value) {

        }
        public void Write (UInt16 value) {

        }
        public void Write (Int16 value) {

        }
        public void Write (Decimal value) {

        }
        public void Write (double value) {

        }
        public void Write (Char[] chars, int index, int count) {

        }
        public void Write (Char[]! chars) {
            CodeContract.Requires(chars != null);

        }
        public void Write (Char ch) {

        }
        public void Write (Byte[] buffer, int index, int count) {

        }
        public void Write (Byte[]! buffer) {
            CodeContract.Requires(buffer != null);

        }
        public void Write (SByte value) {

        }
        public void Write (byte value) {

        }
        public void Write (bool value) {

        }
        public Int64 Seek (int offset, SeekOrigin origin) {

          return default(Int64);
        }
        public void Flush () {

        }
        public void Close () {

        }
        public BinaryWriter (Stream! output, System.Text.Encoding! encoding) {
            CodeContract.Requires(output != null);
            CodeContract.Requires(encoding != null);

          return default(BinaryWriter);
        }
        public BinaryWriter (Stream output) {
          return default(BinaryWriter);
        }
    }
}

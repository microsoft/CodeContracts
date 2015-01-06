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

    public class BinaryReader
    {

        public Stream BaseStream
        {
          get;
        }

        public Byte[] ReadBytes (int count) {
            CodeContract.Requires(count >= 0);

          return default(Byte[]);
        }
        public int Read (Byte[]! buffer, int index, int count) {
            CodeContract.Requires(buffer != null);
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires((buffer.Length - index) >= count);

          return default(int);
        }
        public Char[] ReadChars (int count) {
            CodeContract.Requires(count >= 0);

          return default(Char[]);
        }
        public int Read (Char[]! buffer, int index, int count) {
            CodeContract.Requires(buffer != null);
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires((buffer.Length - index) >= count);

          return default(int);
        }
        public string ReadString () {

          return default(string);
        }
        public Decimal ReadDecimal () {

          return default(Decimal);
        }
        public double ReadDouble () {

          return default(double);
        }
        public Single ReadSingle () {

          return default(Single);
        }
        public UInt64 ReadUInt64 () {

          return default(UInt64);
        }
        public Int64 ReadInt64 () {

          return default(Int64);
        }
        public UInt32 ReadUInt32 () {

          return default(UInt32);
        }
        public int ReadInt32 () {

          return default(int);
        }
        public UInt16 ReadUInt16 () {

          return default(UInt16);
        }
        public Int16 ReadInt16 () {

          return default(Int16);
        }
        public Char ReadChar () {

          return default(Char);
        }
        public SByte ReadSByte () {

          return default(SByte);
        }
        public byte ReadByte () {

          return default(byte);
        }
        public bool ReadBoolean () {

          return default(bool);
        }
        public int Read () {

          return default(int);
        }
        public int PeekChar () {

          return default(int);
        }
        public void Close () {

        }
        public BinaryReader (Stream! input, System.Text.Encoding! encoding) {
            CodeContract.Requires(input != null);
            CodeContract.Requires(encoding != null);

          return default(BinaryReader);
        }
        public BinaryReader (Stream input) {
          return default(BinaryReader);
        }
    }
}

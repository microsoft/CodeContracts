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

    public class MemoryStream
    {

        public Int64 Position
        {
          get;
          set
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value <= 2147483647);
        }

        public bool CanWrite
        {
          get;
        }

        public bool CanSeek
        {
          get;
        }

        public Int64 Length
        {
          get;
        }

        public int Capacity
        {
          get;
          set;
        }

        public bool CanRead
        {
          get;
        }

        public void WriteTo (Stream! stream) {
            CodeContract.Requires(stream != null);

        }
        public void WriteByte (byte value) {

        }
        public void Write (Byte[]! buffer, int offset, int count) {
            CodeContract.Requires(buffer != null);
            CodeContract.Requires(offset >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires((buffer.Length - offset) >= count);

        }
        public Byte[] ToArray () {

          return default(Byte[]);
        }
        public void SetLength (Int64 value) {
            CodeContract.Requires(value <= 2147483647);
            CodeContract.Requires(value >= 0);

        }
        public Int64 Seek (Int64 offset, SeekOrigin loc) {
            CodeContract.Requires(offset <= 2147483647);
            CodeContract.Requires(offset >= 0);

          return default(Int64);
        }
        public int ReadByte () {

          return default(int);
        }
        public int Read (Byte[]! buffer, int offset, int count) {
            CodeContract.Requires(buffer != null);
            CodeContract.Requires(offset >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires((buffer.Length - offset) >= count);

          return default(int);
        }
        public Byte[] GetBuffer () {

          return default(Byte[]);
        }
        public void Flush () {

        }
        public void Close () {

        }
        public MemoryStream (Byte[]! buffer, int index, int count, bool writable, bool publiclyVisible) {
            CodeContract.Requires(buffer != null);
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires((buffer.Length - index) >= count);

          return default(MemoryStream);
        }
        public MemoryStream (Byte[] buffer, int index, int count, bool writable) {

          return default(MemoryStream);
        }
        public MemoryStream (Byte[] buffer, int index, int count) {

          return default(MemoryStream);
        }
        public MemoryStream (Byte[]! buffer, bool writable) {
            CodeContract.Requires(buffer != null);

          return default(MemoryStream);
        }
        public MemoryStream (Byte[] buffer) {

          return default(MemoryStream);
        }
        public MemoryStream (int capacity) {
            CodeContract.Requires(capacity >= 0);

          return default(MemoryStream);
        }
        public MemoryStream () {
          return default(MemoryStream);
        }
    }
}

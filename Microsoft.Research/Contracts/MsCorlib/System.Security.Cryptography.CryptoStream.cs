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

namespace System.Security.Cryptography
{

    public class CryptoStream
    {

        public Int64 Position
        {
          get;
          set;
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

        public bool CanRead
        {
          get;
        }

        public void Clear () {

        }
        public void Write (Byte[] buffer, int offset, int count) {
            Contract.Requires(offset >= 0);
            Contract.Requires(count >= 0);
            Contract.Requires((buffer.Length - offset) >= count);

        }
        public int Read (Byte[] buffer, int offset, int count) {
            Contract.Requires(offset >= 0);
            Contract.Requires(count >= 0);
            Contract.Requires((buffer.Length - offset) >= count);

          return default(int);
        }
        public void SetLength (Int64 value) {

        }
        public Int64 Seek (Int64 offset, System.IO.SeekOrigin origin) {

          return default(Int64);
        }
        public void Flush () {

        }
        public void Close () {

        }
        public void FlushFinalBlock () {

        }
        public CryptoStream (System.IO.Stream stream, ICryptoTransform transform, CryptoStreamMode mode) {
          return default(CryptoStream);
        }
    }
}

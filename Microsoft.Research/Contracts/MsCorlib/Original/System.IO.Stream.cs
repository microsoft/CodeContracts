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

    public class Stream
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

        public void WriteByte (byte value) {

        }
        public void Write (Byte[]! arg0, int arg1, int arg2) {

        }
        public int ReadByte () {

          return default(int);
        }
        public int Read (Byte[]! arg0, int arg1, int arg2) {

          return default(int);
        }
        public void SetLength (Int64 arg0) {

        }
        public Int64 Seek (Int64 arg0, SeekOrigin arg1) {

          return default(Int64);
        }
        public void EndWrite (IAsyncResult! asyncResult) {
            CodeContract.Requires(asyncResult != null);

        }
        public IAsyncResult BeginWrite (Byte[]! buffer, int offset, int count, AsyncCallback! callback, object state) {

          return default(IAsyncResult);
        }
        public int EndRead (IAsyncResult! asyncResult) {
            CodeContract.Requires(asyncResult != null);

          return default(int);
        }
        public IAsyncResult BeginRead (Byte[]! buffer, int offset, int count, AsyncCallback! callback, object state) {

          return default(IAsyncResult);
        }
        public void Flush () {

        }
        public void Close () {
        }
    }
}

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

namespace System.IO.IsolatedStorage
{

    public class IsolatedStorageFileStream
    {

        public bool CanWrite
        {
          get;
        }

        public Int64 Position
        {
          get;
          set
            CodeContract.Requires(value >= 0);
        }

        public bool CanSeek
        {
          get;
        }

        public bool IsAsync
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

        public int Handle
        {
          get;
        }

        public void EndWrite (IAsyncResult asyncResult) {

        }
        public IAsyncResult BeginWrite (Byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject) {

          return default(IAsyncResult);
        }
        public int EndRead (IAsyncResult asyncResult) {

          return default(int);
        }
        public IAsyncResult BeginRead (Byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject) {

          return default(IAsyncResult);
        }
        public void WriteByte (byte value) {

        }
        public void Write (Byte[] buffer, int offset, int count) {

        }
        public Int64 Seek (Int64 offset, System.IO.SeekOrigin origin) {

          return default(Int64);
        }
        public int ReadByte () {

          return default(int);
        }
        public int Read (Byte[] buffer, int offset, int count) {

          return default(int);
        }
        public void SetLength (Int64 value) {

        }
        public void Flush () {

        }
        public void Close () {

        }
        public IsolatedStorageFileStream (string! path, System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share, int bufferSize, IsolatedStorageFile isf) {
            CodeContract.Requires(path != null);
            CodeContract.Requires(path.Length != 0);

          return default(IsolatedStorageFileStream);
        }
        public IsolatedStorageFileStream (string path, System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share, int bufferSize) {

          return default(IsolatedStorageFileStream);
        }
        public IsolatedStorageFileStream (string path, System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share, IsolatedStorageFile isf) {

          return default(IsolatedStorageFileStream);
        }
        public IsolatedStorageFileStream (string path, System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share) {

          return default(IsolatedStorageFileStream);
        }
        public IsolatedStorageFileStream (string path, System.IO.FileMode mode, System.IO.FileAccess access, IsolatedStorageFile isf) {

          return default(IsolatedStorageFileStream);
        }
        public IsolatedStorageFileStream (string path, System.IO.FileMode mode, System.IO.FileAccess access) {

          return default(IsolatedStorageFileStream);
        }
        public IsolatedStorageFileStream (string path, System.IO.FileMode mode, IsolatedStorageFile isf) {

          return default(IsolatedStorageFileStream);
        }
        public IsolatedStorageFileStream (string path, System.IO.FileMode mode) {
          return default(IsolatedStorageFileStream);
        }
    }
}

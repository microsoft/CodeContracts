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
using Microsoft.Win32.SafeHandles;

namespace System.IO
{

    public class FileStream
    {
#if WHIDBEY
        public virtual SafeFileHandle! SafeFileHandle { get; }
#endif
        
        public bool CanSeek
        {
          get;
        }

        public bool CanRead
        {
          get;
        }

        public bool CanWrite
        {
          get;
        }

        public int Handle
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

        public Int64 Position
        {
          get;
          set
            CodeContract.Requires(value >= 0);
        }

        public string Name
        {
          get;
        }

        public void Unlock (Int64 position, Int64 length) {
            CodeContract.Requires(position >= 0);
            CodeContract.Requires(length >= 0);

        }
        public void Lock (Int64 position, Int64 length) {
            CodeContract.Requires(position >= 0);
            CodeContract.Requires(length >= 0);

        }
        public void WriteByte (byte value) {

        }
        public void EndWrite (IAsyncResult! asyncResult) {
            CodeContract.Requires(asyncResult != null);

        }
        public IAsyncResult BeginWrite (Byte[]! array, int offset, int numBytes, AsyncCallback userCallback, object stateObject) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(offset >= 0);
            CodeContract.Requires(numBytes >= 0);
            CodeContract.Requires((array.Length - offset) >= numBytes);

          return default(IAsyncResult);
        }
        public int ReadByte () {

          return default(int);
        }
        public int EndRead (IAsyncResult! asyncResult) {
            CodeContract.Requires(asyncResult != null);

          return default(int);
        }
        public IAsyncResult BeginRead (Byte[]! array, int offset, int numBytes, AsyncCallback userCallback, object stateObject) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(offset >= 0);
            CodeContract.Requires(numBytes >= 0);
            CodeContract.Requires((array.Length - offset) >= numBytes);

          return default(IAsyncResult);
        }
        public void Write (Byte[]! array, int offset, int count) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(offset >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires((array.Length - offset) >= count);

        }
        public Int64 Seek (Int64 offset, SeekOrigin origin) {
            CodeContract.Requires((int)origin >= 0);
            CodeContract.Requires((int)origin <= 2);

          return default(Int64);
        }
        public int Read (Byte[]! array, int offset, int count) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(offset >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires((array.Length - offset) >= count);

          return default(int);
        }
        public void SetLength (Int64 value) {
            CodeContract.Requires(value >= 0);

        }
        public void Flush () {

        }
        public void Close () {

        }
        public FileStream (int handle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync) {
            CodeContract.Requires((int)access >= 1);
            CodeContract.Requires((int)access <= 3);
            CodeContract.Requires(bufferSize > 0);

          return default(FileStream);
        }
        public FileStream (int handle, FileAccess access, bool ownsHandle, int bufferSize) {

          return default(FileStream);
        }
        public FileStream (int handle, FileAccess access, bool ownsHandle) {

          return default(FileStream);
        }
        public FileStream (int handle, FileAccess access) {

          return default(FileStream);
        }
        public FileStream (string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync) {

          return default(FileStream);
        }
        public FileStream (string path, FileMode mode, FileAccess access, FileShare share, int bufferSize) {

          return default(FileStream);
        }
        public FileStream (string path, FileMode mode, FileAccess access, FileShare share) {

          return default(FileStream);
        }
        public FileStream (string path, FileMode mode, FileAccess access) {

          return default(FileStream);
        }
        public FileStream (string path, FileMode mode) {
          return default(FileStream);
        }
    }
}

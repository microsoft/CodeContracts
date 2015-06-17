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

namespace System.IO
{

    public class StreamWriter
    {

        public bool AutoFlush
        {
          get;
          set;
        }

        public Stream BaseStream
        {
          get;
        }

        public System.Text.Encoding Encoding
        {
          get;
        }

        public void Write (string value) {

        }
        public void Write (char[]! buffer, int index, int count) {
            CodeContract.Requires(buffer != null);
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires(index + count <= buffer.Length);

        }
        public void Write (char[] buffer) {

        }
        public void Write (char value) {

        }
        public void Flush () {

        }
        public void Close () {

        }
        public StreamWriter (string! path, bool append, System.Text.Encoding! encoding, int bufferSize) {
            CodeContract.Requires(path != null);
            CodeContract.Requires(encoding != null);
            CodeContract.Requires(bufferSize > 0);

          return default(StreamWriter);
        }
        public StreamWriter (string! path, bool append, System.Text.Encoding! encoding) {
            CodeContract.Requires(path != null);
            CodeContract.Requires(encoding != null);

          return default(StreamWriter);
        }
        public StreamWriter (string! path, bool append) {
            CodeContract.Requires(path != null);

          return default(StreamWriter);
        }
        public StreamWriter (string! path) {
            CodeContract.Requires(path != null);

          return default(StreamWriter);
        }
        public StreamWriter (Stream! stream, System.Text.Encoding! encoding, int bufferSize) {
            CodeContract.Requires(stream != null);
            CodeContract.Requires(encoding != null);
            CodeContract.Requires(bufferSize > 0);

          return default(StreamWriter);
        }
        public StreamWriter (Stream! stream, System.Text.Encoding! encoding) {
            CodeContract.Requires(stream != null);
            CodeContract.Requires(encoding != null);

          return default(StreamWriter);
        }
        public StreamWriter (Stream! stream) {
            CodeContract.Requires(stream != null);
          return default(StreamWriter);
        }
    }
}

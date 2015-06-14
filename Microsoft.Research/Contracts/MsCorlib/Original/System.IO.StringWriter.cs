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

    public class StringWriter
    {

        public System.Text.Encoding Encoding
        {
          get;
        }

        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string ToString () {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public void Write (string value) {

        }
        public void Write (Char[]! buffer, int index, int count) {
            CodeContract.Requires(buffer != null);
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires((buffer.Length - index) >= count);

        }
        public void Write (Char value) {

        }
        public System.Text.StringBuilder GetStringBuilder () {

          CodeContract.Ensures(CodeContract.Result<System.Text.StringBuilder>() != null);
          return default(System.Text.StringBuilder);
        }
        public void Close () {

        }
        public StringWriter (System.Text.StringBuilder! sb, IFormatProvider formatProvider) {
            CodeContract.Requires(sb != null);

          return default(StringWriter);
        }
        public StringWriter (System.Text.StringBuilder! sb) {

          return default(StringWriter);
        }
        public StringWriter (IFormatProvider formatProvider) {

          return default(StringWriter);
        }
        public StringWriter () {
          return default(StringWriter);
        }
    }
}

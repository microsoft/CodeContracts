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

    public class TextWriter
    {

        public IFormatProvider FormatProvider
        {
          get;
        }

        public string NewLine
        {
          get;
          set;
        }

        public System.Text.Encoding Encoding
        {
          get;
        }

        public void WriteLine (string! format, params object[]! args) {
            CodeContract.Requires(format != null);
            CodeContract.Requires(args != null);

        }
        public void WriteLine (string! format, object arg0, object arg1, object arg2) {
            CodeContract.Requires(format != null);

        }
        public void WriteLine (string! format, object arg0, object arg1) {
            CodeContract.Requires(format != null);

        }
        public void WriteLine (string! format, object arg0) {
            CodeContract.Requires(format != null);

        }
        public void WriteLine (object value) {

        }
        public void WriteLine (string value) {

        }
        public void WriteLine (Decimal value) {

        }
        public void WriteLine (double value) {

        }
        public void WriteLine (Single value) {

        }
        public void WriteLine (ulong value) {

        }
        public void WriteLine (long value) {

        }
        public void WriteLine (uint value) {

        }
        public void WriteLine (int value) {

        }
        public void WriteLine (bool value) {

        }
        public void WriteLine (char[] buffer, int index, int count) {

        }
        public void WriteLine (char[] buffer) {

        }
        public void WriteLine (char value) {

        }
        public void WriteLine () {

        }
        public void Write (string! format, params object[]! args) {
            CodeContract.Requires(format != null);
            CodeContract.Requires(args != null);

        }
        public void Write (string! format, object arg0, object arg1, object arg2) {
            CodeContract.Requires(format != null);

        }
        public void Write (string! format, object arg0, object arg1) {
            CodeContract.Requires(format != null);

        }
        public void Write (string! format, object arg0) {
            CodeContract.Requires(format != null);

        }
        public void Write (object value) {

        }
        public void Write (string value) {

        }
        public void Write (Decimal value) {

        }
        public void Write (double value) {

        }
        public void Write (Single value) {

        }
        public void Write (ulong value) {

        }
        public void Write (long value) {

        }
        public void Write (uint value) {

        }
        public void Write (int value) {

        }
        public void Write (bool value) {

        }
        public void Write (char[]! buffer, int index, int count) {
            CodeContract.Requires(buffer != null);
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires((buffer.Length - index) >= count);

        }
        public void Write (char[] buffer) {

        }
        public void Write (char value) {

        }
        public static TextWriter Synchronized (TextWriter! writer) {
            CodeContract.Requires(writer != null);

          return default(TextWriter);
        }
        public void Flush () {

        }
        public void Close () {
        }
    }
}

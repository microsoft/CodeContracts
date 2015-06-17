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

namespace System
{

    public class Console
    {

        public static System.IO.TextWriter! Error
        {
          [Pure][Reads(ReadsAttribute.Reads.Nothing)]  // the getter always returns the same result (this is the best way we have now to specify that)
          get;
            CodeContract.Ensures(result.IsPeerConsistent);
        }

        public static System.IO.TextReader! In
        {
          [Pure][Reads(ReadsAttribute.Reads.Nothing)]  // the getter always returns the same result (this is the best way we have now to specify that)
          get;
            CodeContract.Ensures(result.IsPeerConsistent);
        }

        public static System.IO.TextWriter! Out
        {
          [Pure][Reads(ReadsAttribute.Reads.Nothing)]  // the getter always returns the same result (this is the best way we have now to specify that)
          get;
            CodeContract.Ensures(result.IsPeerConsistent);
        }

        public static void Write (string value) {

        }
        public static void Write (object value) {

        }
        public static void Write (UInt64 value) {

        }
        public static void Write (Int64 value) {

        }
        public static void Write (UInt32 value) {

        }
        public static void Write (int value) {

        }
        public static void Write (Single value) {

        }
        public static void Write (Decimal value) {

        }
        public static void Write (double value) {

        }
        public static void Write (Char[] buffer, int index, int count) {

        }
        public static void Write (Char[] buffer) {

        }
        public static void Write (Char value) {

        }
        public static void Write (bool value) {

        }
        public static void Write (string format, Object[] arg) {

        }
        public static void Write (string format, object arg0, object arg1, object arg2, object arg3) {

        }
        public static void Write (string format, object arg0, object arg1, object arg2) {

        }
        public static void Write (string format, object arg0, object arg1) {

        }
        public static void Write (string format, object arg0) {

        }
        public static void WriteLine (string format, Object[] arg) {

        }
        public static void WriteLine (string format, object arg0, object arg1, object arg2, object arg3) {

        }
        public static void WriteLine (string format, object arg0, object arg1, object arg2) {

        }
        public static void WriteLine (string format, object arg0, object arg1) {

        }
        public static void WriteLine (string format, object arg0) {

        }
        public static void WriteLine (string value) {

        }
        public static void WriteLine (object value) {

        }
        public static void WriteLine (UInt64 value) {

        }
        public static void WriteLine (Int64 value) {

        }
        public static void WriteLine (UInt32 value) {

        }
        public static void WriteLine (int value) {

        }
        public static void WriteLine (Single value) {

        }
        public static void WriteLine (double value) {

        }
        public static void WriteLine (Decimal value) {

        }
        public static void WriteLine (Char[] buffer, int index, int count) {

        }
        public static void WriteLine (Char[] buffer) {

        }
        public static void WriteLine (Char value) {

        }
        public static void WriteLine (bool value) {

        }
        public static void WriteLine () {

        }
        public static string ReadLine () {

          return default(string);
        }
        public static int Read () {

          return default(int);
        }
        public static void SetError (System.IO.TextWriter! newError) {
            CodeContract.Requires(newError != null);

        }
        public static void SetOut (System.IO.TextWriter! newOut) {
            CodeContract.Requires(newOut != null);

        }
        public static void SetIn (System.IO.TextReader! newIn) {
            CodeContract.Requires(newIn != null);

        }
        public static System.IO.Stream OpenStandardOutput (int bufferSize) {

          return default(System.IO.Stream);
        }
        public static System.IO.Stream OpenStandardOutput () {

          return default(System.IO.Stream);
        }
        public static System.IO.Stream OpenStandardInput (int bufferSize) {

          return default(System.IO.Stream);
        }
        public static System.IO.Stream OpenStandardInput () {

          return default(System.IO.Stream);
        }
        public static System.IO.Stream OpenStandardError (int bufferSize) {

          return default(System.IO.Stream);
        }
        public static System.IO.Stream OpenStandardError () {
          return default(System.IO.Stream);
        }
    }
}

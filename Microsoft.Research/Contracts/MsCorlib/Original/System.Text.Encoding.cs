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

namespace System.Text
{

    public class Encoding
    {

        public int WindowsCodePage
        {
          get;
        }

        public bool IsMailNewsDisplay
        {
          get;
        }

        public static Encoding! UTF8
        {
          get;
        }

        public static Encoding! Default
        {
          get;
        }

        public bool IsMailNewsSave
        {
          get;
        }

        public string EncodingName
        {
          get;
        }

        public string BodyName
        {
          get;
        }

        public static Encoding! UTF7
        {
          get;
        }

        public string HeaderName
        {
          get;
        }

        public int CodePage
        {
          get;
        }

        public bool IsBrowserSave
        {
          get;
        }

        public static Encoding! Unicode
        {
          get;
        }

        public string WebName
        {
          get;
        }

        public static Encoding! BigEndianUnicode
        {
          get;
        }

        public static Encoding! ASCII
        {
          get;
        }

        public bool IsBrowserDisplay
        {
          get;
        }

        public string GetString (byte[] bytes, int index, int count) {

          return default(string);
        }
        public string GetString (byte[]! bytes) {
            CodeContract.Requires(bytes != null);

          return default(string);
        }
        public int GetMaxCharCount (int arg0) {

          return default(int);
        }
        public int GetMaxByteCount (int arg0) {

          return default(int);
        }
        public Encoder GetEncoder () {

          return default(Encoder);
        }
        public Decoder GetDecoder () {

          return default(Decoder);
        }
        public int GetChars (byte[] arg0, int arg1, int arg2, char[] arg3, int arg4) {

          return default(int);
        }
        public char[] GetChars (byte[] bytes, int index, int count) {

          return default(char[]);
        }
        public char[] GetChars (byte[]! bytes) {
            CodeContract.Requires(bytes != null);

          return default(char[]);
        }
        public int GetCharCount (byte[] arg0, int arg1, int arg2) {

          return default(int);
        }
        public int GetCharCount (byte[]! bytes) {
            CodeContract.Requires(bytes != null);

          return default(int);
        }
        public int GetBytes (string! s, int charIndex, int charCount, byte[] bytes, int byteIndex) {
            CodeContract.Requires(s != null);

          return default(int);
        }
        public byte[] GetBytes (string! s) {
            CodeContract.Requires(s != null);

          return default(byte[]);
        }
        public int GetBytes (char[] arg0, int arg1, int arg2, byte[] arg3, int arg4) {

          return default(int);
        }
        public byte[] GetBytes (char[] chars, int index, int count) {

          return default(byte[]);
        }
        public byte[] GetBytes (char[]! chars) {
            CodeContract.Requires(chars != null);

          return default(byte[]);
        }
        public int GetByteCount (char[] arg0, int arg1, int arg2) {

          return default(int);
        }
        public int GetByteCount (string! s) {
            CodeContract.Requires(s != null);

          return default(int);
        }
        public int GetByteCount (char[]! chars) {
            CodeContract.Requires(chars != null);

          return default(int);
        }
        public byte[] GetPreamble () {

          return default(byte[]);
        }
        public static Encoding GetEncoding (string name) {

          CodeContract.Ensures(CodeContract.Result<Encoding>() != null);
          return default(Encoding);
        }
        public static Encoding GetEncoding (int codepage) {
            CodeContract.Requires(codepage >= 0);
            CodeContract.Requires(codepage <= 65535);

          CodeContract.Ensures(CodeContract.Result<Encoding>() != null);
          return default(Encoding);
        }
        public static byte[] Convert (Encoding! srcEncoding, Encoding! dstEncoding, byte[]! bytes, int index, int count) {
            CodeContract.Requires(srcEncoding != null);
            CodeContract.Requires(dstEncoding != null);
            CodeContract.Requires(bytes != null);

          return default(byte[]);
        }
        public static byte[] Convert (Encoding srcEncoding, Encoding dstEncoding, byte[]! bytes) {
            CodeContract.Requires(bytes != null);
          return default(byte[]);
        }
    }
}

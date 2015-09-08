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

    public struct Char
    {

        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static double GetNumericValue (string! s, int index) {
            CodeContract.Requires(s != null);
            CodeContract.Requires(index < s.Length);

          return default(double);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static double GetNumericValue (Char c) {

          return default(double);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static System.Globalization.UnicodeCategory GetUnicodeCategory (string! s, int index) {
            CodeContract.Requires(s != null);
            CodeContract.Requires(index < s.Length);

          return default(System.Globalization.UnicodeCategory);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static System.Globalization.UnicodeCategory GetUnicodeCategory (Char c) {

          return default(System.Globalization.UnicodeCategory);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsWhiteSpace (string! s, int index) {
            CodeContract.Requires(s != null);
            CodeContract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsUpper (string! s, int index) {
            CodeContract.Requires(s != null);
            CodeContract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsSymbol (string! s, int index) {
            CodeContract.Requires(s != null);
            CodeContract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsSymbol (Char c) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsSurrogate (string! s, int index) {
            CodeContract.Requires(s != null);
            CodeContract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsSurrogate (Char c) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsSeparator (string! s, int index) {
            CodeContract.Requires(s != null);
            CodeContract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsSeparator (Char c) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsPunctuation (string! s, int index) {
            CodeContract.Requires(s != null);
            CodeContract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsNumber (string! s, int index) {
            CodeContract.Requires(s != null);
            CodeContract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsNumber (Char c) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsLower (string! s, int index) {
            CodeContract.Requires(s != null);
            CodeContract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsLetterOrDigit (string! s, int index) {
            CodeContract.Requires(s != null);
            CodeContract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsLetter (string! s, int index) {
            CodeContract.Requires(s != null);
            CodeContract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsDigit (string! s, int index) {
            CodeContract.Requires(s != null);
            CodeContract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsControl (string! s, int index) {
            CodeContract.Requires(s != null);
            CodeContract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsControl (Char c) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public TypeCode GetTypeCode () {

          return default(TypeCode);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static Char ToLower (Char c) {

          return default(Char);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static Char ToLower (Char c, System.Globalization.CultureInfo! culture) {
            CodeContract.Requires(culture != null);

          return default(Char);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static Char ToUpper (Char c) {

          return default(Char);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static Char ToUpper (Char c, System.Globalization.CultureInfo! culture) {
            CodeContract.Requires(culture != null);

          return default(Char);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsLetterOrDigit (Char c) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsPunctuation (Char c) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsLower (Char c) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsUpper (Char c) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsWhiteSpace (Char c) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsLetter (Char c) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsDigit (Char c) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static Char Parse (string! s) {
            CodeContract.Requires(s != null);
            CodeContract.Requires(s.Length == 1);

          return default(Char);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static string ToString (Char arg0) {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public string ToString (IFormatProvider provider) {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public string ToString () {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public int CompareTo (object value) {

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public bool Equals (object obj) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public int GetHashCode () {
          return default(int);
        }
    }
}

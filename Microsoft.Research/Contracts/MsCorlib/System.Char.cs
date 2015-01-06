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
using System.Globalization;

namespace System
{

    public struct Char
    {

        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static double GetNumericValue (string s, int index) {
            Contract.Requires(s != null);
            Contract.Requires(index < s.Length);

          return default(double);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static double GetNumericValue (Char c) {

          return default(double);
        }
#if !SILVERLIGHT
        public static string ConvertFromUtf32(int utf32) {
            Contract.Ensures(Contract.Result<string>() != null);
            Contract.Ensures(Contract.Result<string>().Length == 1 ||
                Contract.Result<string>().Length == 2);
            return default(string);
        }
#endif
#if false
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static System.Globalization.UnicodeCategory GetUnicodeCategory (string s, int index) {
            Contract.Requires(s != null);
            Contract.Requires(index < s.Length);

          return default(System.Globalization.UnicodeCategory);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static System.Globalization.UnicodeCategory GetUnicodeCategory (Char c) {

          return default(System.Globalization.UnicodeCategory);
        }
#endif
        [Pure]
        [Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsWhiteSpace (string s, int index) {
            Contract.Requires(s != null);
            Contract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsUpper (string s, int index) {
            Contract.Requires(s != null);
            Contract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsSymbol (string s, int index) {
            Contract.Requires(s != null);
            Contract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsSymbol (Char c) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsSurrogate (string s, int index) {
            Contract.Requires(s != null);
            Contract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsSurrogate (Char c) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsSeparator (string s, int index) {
            Contract.Requires(s != null);
            Contract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsSeparator (Char c) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsPunctuation (string s, int index) {
            Contract.Requires(s != null);
            Contract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsNumber (string s, int index) {
            Contract.Requires(s != null);
            Contract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsNumber (Char c) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsLower (string s, int index) {
            Contract.Requires(s != null);
            Contract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsLetterOrDigit (string s, int index) {
            Contract.Requires(s != null);
            Contract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsLetter (string s, int index) {
            Contract.Requires(s != null);
            Contract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsDigit (string s, int index) {
            Contract.Requires(s != null);
            Contract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsControl (string s, int index) {
            Contract.Requires(s != null);
            Contract.Requires(index < s.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool IsControl (Char c) {

          return default(bool);
        }

        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static Char ToLower (Char c) {

          return default(Char);
        }
#if !SILVERLIGHT_3_0
        //
        // Summary:
        //     Converts the value of a specified Unicode character to its lowercase equivalent
        //     using specified culture-specific formatting information.
        //
        // Parameters:
        //   c:
        //     The Unicode character to convert.
        //
        //   culture:
        //     An object that supplies culture-specific casing rules.
        //
        // Returns:
        //     The lowercase equivalent of c, modified according to culture, or the unchanged
        //     value of c, if c is already lowercase or not alphabetic.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     culture is null.
        [Pure]
        public static char ToLower(char c, CultureInfo culture)
        {
          return default(Char);
        }
#endif
#if !SILVERLIGHT_3_0 && !SILVERLIGHT_4_0
        //
        // Summary:
        //     Converts the value of a Unicode character to its lowercase equivalent using
        //     the casing rules of the invariant culture.
        //
        // Parameters:
        //   c:
        //     The Unicode character to convert.
        //
        // Returns:
        //     The lowercase equivalent of the c parameter, or the unchanged value of c,
        //     if c is already lowercase or not alphabetic.
        [Pure]
        public static char ToLowerInvariant(char c) {
          return default(Char);
        }
#endif

        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static Char ToUpper (Char c) {

          return default(Char);
        }
#if false
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static Char ToUpper (Char c, System.Globalization.CultureInfo culture) {
            Contract.Requires(culture != null);

          return default(Char);
        }
#endif
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
#if !SILVERLIGHT
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static Char Parse (string s) {
            Contract.Requires(s != null);
            Contract.Requires(s.Length == 1);

          return default(Char);
        }
#endif
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static string ToString (Char arg0) {

          Contract.Ensures(Contract.Result<string>() != null);
          Contract.Ensures(Contract.Result<string>().Length == 1);

          return default(string);
        }

    }
}

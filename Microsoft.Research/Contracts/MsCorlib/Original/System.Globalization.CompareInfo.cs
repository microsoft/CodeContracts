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

namespace System.Globalization
{

    public class CompareInfo
    {

        public int LCID
        {
          get;
        }

        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string ToString () {

          return default(string);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int GetHashCode () {

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public bool Equals (object value) {

          return default(bool);
        }
        public SortKey GetSortKey (string source) {

          return default(SortKey);
        }
        public SortKey GetSortKey (string source, CompareOptions options) {

          return default(SortKey);
        }
        public int LastIndexOf (string source, string value, int startIndex, int count, CompareOptions options) {
            CodeContract.Requires(count >= 0);

          return default(int);
        }
        public int LastIndexOf (string source, Char value, int startIndex, int count, CompareOptions options) {
            CodeContract.Requires(count >= 0);

          return default(int);
        }
        public int LastIndexOf (string source, string value, int startIndex, int count) {
            CodeContract.Requires(count >= 0);

          return default(int);
        }
        public int LastIndexOf (string source, string value, int startIndex, CompareOptions options) {

          return default(int);
        }
        public int LastIndexOf (string source, string value, int startIndex) {

          return default(int);
        }
        public int LastIndexOf (string source, Char value, int startIndex, int count) {
            CodeContract.Requires(count >= 0);

          return default(int);
        }
        public int LastIndexOf (string source, Char value, int startIndex, CompareOptions options) {

          return default(int);
        }
        public int LastIndexOf (string source, Char value, int startIndex) {

          return default(int);
        }
        public int LastIndexOf (string! source, string value, CompareOptions options) {
            CodeContract.Requires(source != null);

          return default(int);
        }
        public int LastIndexOf (string! source, Char value, CompareOptions options) {
            CodeContract.Requires(source != null);

          return default(int);
        }
        public int LastIndexOf (string! source, string value) {
            CodeContract.Requires(source != null);

          return default(int);
        }
        public int LastIndexOf (string! source, Char value) {
            CodeContract.Requires(source != null);

          return default(int);
        }
        public int IndexOf (string source, string value, int startIndex, int count, CompareOptions options) {
            CodeContract.Requires(count >= 0);

          return default(int);
        }
        public int IndexOf (string source, Char value, int startIndex, int count, CompareOptions options) {
            CodeContract.Requires(count >= 0);

          return default(int);
        }
        public int IndexOf (string source, string value, int startIndex, int count) {
            CodeContract.Requires(count >= 0);

          return default(int);
        }
        public int IndexOf (string source, string value, int startIndex, CompareOptions options) {

          return default(int);
        }
        public int IndexOf (string! source, string value, int startIndex) {
            CodeContract.Requires(source != null);
            CodeContract.Requires(startIndex <= source.Length);

          return default(int);
        }
        public int IndexOf (string source, Char value, int startIndex, int count) {
            CodeContract.Requires(count >= 0);

          return default(int);
        }
        public int IndexOf (string source, Char value, int startIndex, CompareOptions options) {

          return default(int);
        }
        public int IndexOf (string! source, Char value, int startIndex) {
            CodeContract.Requires(source != null);
            CodeContract.Requires(startIndex <= source.Length);

          return default(int);
        }
        public int IndexOf (string source, string value, CompareOptions options) {

          return default(int);
        }
        public int IndexOf (string source, Char value, CompareOptions options) {

          return default(int);
        }
        public int IndexOf (string source, string value) {

          return default(int);
        }
        public int IndexOf (string source, Char value) {

          return default(int);
        }
        public bool IsSuffix (string source, string suffix) {

          return default(bool);
        }
        public bool IsSuffix (string! source, string! suffix, CompareOptions options) {
            CodeContract.Requires(source != null);
            CodeContract.Requires(suffix != null);
            CodeContract.Requires((int)((int)options & -32) == 0 || (int)options == 1073741824);

          return default(bool);
        }
        public bool IsPrefix (string source, string prefix) {

          return default(bool);
        }
        public bool IsPrefix (string! source, string! prefix, CompareOptions options) {
            CodeContract.Requires(source != null);
            CodeContract.Requires(prefix != null);
            CodeContract.Requires((int)((int)options & -32) == 0 || (int)options == 1073741824);

          return default(bool);
        }
        public int Compare (string string1, int offset1, int length1, string string2, int offset2, int length2, CompareOptions options) {
            CodeContract.Requires(length1 >= 0);
            CodeContract.Requires(length2 >= 0);

          return default(int);
        }
        public int Compare (string string1, int offset1, string string2, int offset2) {

          return default(int);
        }
        public int Compare (string string1, int offset1, string string2, int offset2, CompareOptions options) {

          return default(int);
        }
        public int Compare (string string1, int offset1, int length1, string string2, int offset2, int length2) {

          return default(int);
        }
        public int Compare (string string1, string string2, CompareOptions options) {

          return default(int);
        }
        public int Compare (string string1, string string2) {

          return default(int);
        }
        public static CompareInfo GetCompareInfo (string! name) {
            CodeContract.Requires(name != null);

          return default(CompareInfo);
        }
        public static CompareInfo GetCompareInfo (int culture) {

          return default(CompareInfo);
        }
        public static CompareInfo GetCompareInfo (string! name, System.Reflection.Assembly! assembly) {
            CodeContract.Requires(name != null);
            CodeContract.Requires(assembly != null);

          return default(CompareInfo);
        }
        public static CompareInfo GetCompareInfo (int culture, System.Reflection.Assembly! assembly) {
            CodeContract.Requires(assembly != null);
          return default(CompareInfo);
        }
    }
}

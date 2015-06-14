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

    [Immutable]
    public class Version
    {

        public int Minor
        {
          get;
        }

        public int Major
        {
          get;
        }

        public int Revision
        {
          get;
        }

        public int Build
        {
          get;
        }

        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool operator >= (Version v1, Version v2) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool operator > (Version v1, Version v2) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool operator <= (Version! v1, Version v2) {
            CodeContract.Requires(v1 != null);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool operator < (Version! v1, Version v2) {
            CodeContract.Requires(v1 != null);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool operator != (Version v1, Version v2) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool operator == (Version v1, Version v2) {

          return default(bool);
        }
        public string ToString (int fieldCount) {
            CodeContract.Requires(fieldCount == 4);

          return default(string);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public string ToString () {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public int GetHashCode () {

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public bool Equals (object obj) {

          return default(bool);
        }
        public int CompareTo (object version) {

          return default(int);
        }
        public object Clone () {

          return default(object);
        }
        public Version () {

          return default(Version);
        }
        public Version (string! version) {
            CodeContract.Requires(version != null);

          return default(Version);
        }
        public Version (int major, int minor) {
            CodeContract.Requires(major >= 0);
            CodeContract.Requires(minor >= 0);

          return default(Version);
        }
        public Version (int major, int minor, int build) {
            CodeContract.Requires(major >= 0);
            CodeContract.Requires(minor >= 0);
            CodeContract.Requires(build >= 0);

          return default(Version);
        }
        public Version (int major, int minor, int build, int revision) {
            CodeContract.Requires(major >= 0);
            CodeContract.Requires(minor >= 0);
            CodeContract.Requires(build >= 0);
            CodeContract.Requires(revision >= 0);
          return default(Version);
        }
    }
}

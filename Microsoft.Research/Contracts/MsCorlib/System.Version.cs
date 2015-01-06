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
    public class Version: ICloneable, IComparable
    {

        public int Minor
        {
            get { return default(int); }
        }

        public int Major
        {
            get { return default(int); }
        }

        public int Revision
        {
            get { return default(int); }
        }

        public int Build
        {
            get { return default(int); }
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
        public static bool operator <= (Version v1, Version v2) {
          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool operator < (Version v1, Version v2) {
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
          Contract.Requires(fieldCount >= 0);
          Contract.Requires(fieldCount <= 4);
          Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()) || fieldCount == 0);
          return default(string);
        }

        public int CompareTo (object version) {

          return default(int);
        }
        public object Clone () {

          return default(object);
        }
#if !SILVERLIGHT
        public Version () {

        }
#endif
        public Version (string version) {
            Contract.Requires(version != null);

        }
        public Version (int major, int minor) {
            Contract.Requires(major >= 0);
            Contract.Requires(minor >= 0);

        }
        public Version (int major, int minor, int build) {
            Contract.Requires(major >= 0);
            Contract.Requires(minor >= 0);
            Contract.Requires(build >= 0);

        }
        public Version (int major, int minor, int build, int revision) {
            Contract.Requires(major >= 0);
            Contract.Requires(minor >= 0);
            Contract.Requires(build >= 0);
            Contract.Requires(revision >= 0);
        }
    }
}

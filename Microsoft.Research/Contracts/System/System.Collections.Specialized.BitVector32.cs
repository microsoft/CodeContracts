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

#if !SILVERLIGHT

using System.Diagnostics.Contracts;
using System;

namespace System.Collections.Specialized
{

    public struct BitVector32
    {
        public struct Section { }

        public int Data
        {
          get;
        }

        public bool this [int bit]
        {
          get;
          set;
        }

        public static string ToString (BitVector32 value) {

          return default(string);
        }
        public static Section CreateSection (Int16 maxValue, Section previous) {

          return default(Section);
        }
        public static Section CreateSection (Int16 maxValue) {

          return default(Section);
        }
        public static int CreateMask (int previous) {
            Contract.Requires(previous == 0 || previous != -2147483648);

          return default(int);
        }
        public static int CreateMask () {

          return default(int);
        }
        public BitVector32 (BitVector32 value) {

          return default(BitVector32);
        }
        public BitVector32 (int data) {
          return default(BitVector32);
        }
    }
}

#endif

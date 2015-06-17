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

namespace System.Collections
{

    public class BitArray
    {

        public bool IsReadOnly
        {
          get;
        }

        public object SyncRoot
        {
          get;
        }

        public bool IsSynchronized
        {
          get;
        }

        public int Length
        {
          get;
          set
            CodeContract.Requires(value >= 0);
        }

        public bool this [int index]
        {
          get;
          set;
        }

        public int Count
        {
          get;
        }

        [Pure] [GlobalAccess(false)] [Escapes(true,false)]
        public IEnumerator GetEnumerator () {
            CodeContract.Ensures(result.IsNew);

          CodeContract.Ensures(CodeContract.Result<IEnumerator>() != null);
          return default(IEnumerator);
        }
        public object Clone () {

          return default(object);
        }
        public void CopyTo (Array! array, int index) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(index >= 0);

        }
        public BitArray Not () {

          return default(BitArray);
        }
        public BitArray Xor (BitArray! value) {
            CodeContract.Requires(value != null);

          return default(BitArray);
        }
        public BitArray Or (BitArray! value) {
            CodeContract.Requires(value != null);

          return default(BitArray);
        }
        public BitArray And (BitArray! value) {
            CodeContract.Requires(value != null);

          return default(BitArray);
        }
        public void SetAll (bool value) {

        }
        public void Set (int index, bool value) {
            CodeContract.Requires(index >= 0);

        }
        public bool Get (int index) {
            CodeContract.Requires(index >= 0);

          return default(bool);
        }
        public BitArray (BitArray! bits) {
            CodeContract.Requires(bits != null);

          return default(BitArray);
        }
        public BitArray (Int32[]! values) {
            CodeContract.Requires(values != null);

          return default(BitArray);
        }
        public BitArray (Boolean[]! values) {
            CodeContract.Requires(values != null);

          return default(BitArray);
        }
        public BitArray (Byte[]! bytes) {
            CodeContract.Requires(bytes != null);

          return default(BitArray);
        }
        public BitArray (int length, bool defaultValue) {
            CodeContract.Requires(length >= 0);

          return default(BitArray);
        }
        public BitArray (int length) {
          return default(BitArray);
        }
    }
}

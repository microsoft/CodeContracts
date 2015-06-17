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

    public class Stack
    {

        public object SyncRoot
        {
          get;
        }

        public bool IsSynchronized
        {
          get;
        }

        public int Count
        {
          get;
            CodeContract.Ensures(result >= 0);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public Object[] ToArray () {

          CodeContract.Ensures(CodeContract.Result<Object[]>() != null);
          return default(Object[]);
        }
        public static Stack Synchronized (Stack! stack) {
            CodeContract.Requires(stack != null);

          return default(Stack);
        }
        public void Push (object obj) {
            CodeContract.Ensures(this.Count == old(this.Count)+1);

        }
        public object Pop () {
            CodeContract.Requires(this.Count > 0);
            CodeContract.Ensures(this.Count == old(this.Count)-1);

          return default(object);
        }
        public object Peek () {
            CodeContract.Requires(this.Count > 0);

          return default(object);
        }
        [Pure] [GlobalAccess(false)] [Escapes(true,false)]
        public IEnumerator GetEnumerator () {
            CodeContract.Ensures(result.IsNew);

          CodeContract.Ensures(CodeContract.Result<IEnumerator>() != null);
          return default(IEnumerator);
        }
        public void CopyTo (Array! array, int index) {
          CodeContract.Requires(0 <= index && index < array.Length);
          CodeContract.Requires((array.Length - index) >= this.Count);            

        }
        public bool Contains (object obj) {

          return default(bool);
        }
        public object Clone () {

          return default(object);
        }
        public void Clear () {

        }
        public Stack (ICollection! col) {
            CodeContract.Requires(col != null);

          return default(Stack);
        }
        public Stack (int initialCapacity) {
            CodeContract.Requires(initialCapacity >= 0);

          return default(Stack);
        }
        public Stack () {
          return default(Stack);
        }
    }
}

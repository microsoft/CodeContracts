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

    public class Queue
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
        }

        public void TrimToSize () {

        }
        public Object[] ToArray () {

          return default(Object[]);
        }
        public bool Contains (object obj) {

          return default(bool);
        }
        public static Queue Synchronized (Queue queue) {
            Contract.Requires(queue != null);

          return default(Queue);
        }
        public object Peek () {

          return default(object);
        }
        public object Dequeue () {

          return default(object);
        }
        [Pure] [GlobalAccess(false)] [Escapes(true,false)]
        public IEnumerator GetEnumerator () {
            Contract.Ensures(result.IsNew);

          Contract.Ensures(Contract.Result<IEnumerator>() != null);
          return default(IEnumerator);
        }
        public void Enqueue (object obj) {

        }
        public void CopyTo (Array array, int index) {
            Contract.Requires(array != null);
            Contract.Requires(index >= 0);

        }
        public void Clear () {

        }
        public object Clone () {

          return default(object);
        }
        public Queue (ICollection col) {
            Contract.Requires(col != null);

          return default(Queue);
        }
        public Queue (int capacity, Single growFactor) {
            Contract.Requires(capacity >= 0);
            Contract.Requires(growFactor >= 0);
            Contract.Requires(growFactor <= 0);

          return default(Queue);
        }
        public Queue (int capacity) {

          return default(Queue);
        }
        public Queue () {
          return default(Queue);
        }
    }
}

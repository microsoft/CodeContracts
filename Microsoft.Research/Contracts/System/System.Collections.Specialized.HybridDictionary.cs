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

    public class HybridDictionary
    {

        public object this [object! key]
        {
          get
            Contract.Requires(key != null);
          set;
        }

        public int Count
        {
          get;
        }

        public System.Collections.ICollection Values
        { [ElementCollection] 
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

        public bool IsFixedSize
        {
          get;
        }

        public System.Collections.ICollection Keys
        { [ElementCollection] 
          get;
        }

        public bool IsReadOnly
        {
          get;
        }

        public void Remove (object key) {

        }
        public System.Collections.IDictionaryEnumerator GetEnumerator () {

          return default(System.Collections.IDictionaryEnumerator);
        }
        public void CopyTo (Array array, int index) {

        }
        public bool Contains (object key) {

          return default(bool);
        }
        public void Clear () {

        }
        public void Add (object key, object value) {

        }
        public HybridDictionary (int initialSize, bool caseInsensitive) {

          return default(HybridDictionary);
        }
        public HybridDictionary (bool caseInsensitive) {

          return default(HybridDictionary);
        }
        public HybridDictionary (int initialSize) {

          return default(HybridDictionary);
        }
        public HybridDictionary () {
          return default(HybridDictionary);
        }
    }
}

#endif

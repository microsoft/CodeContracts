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

namespace System.Net
{

    public class CookieCollection
    {

        extern public bool IsReadOnly
        {
          get;
        }

        extern virtual public object SyncRoot
        {
          get;
        }

        extern virtual public bool IsSynchronized
        {
          get;
        }

        public Cookie this [int index]
        {
          get {
            Contract.Requires(index >= 0);
            return default(Cookie);
          }
        }

        extern public virtual int Count
        {
          get;
        }

        public virtual System.Collections.IEnumerator GetEnumerator () {

          return default(System.Collections.IEnumerator);
        }
        public virtual void CopyTo (Array array, int index) {

        }
        public void Add (CookieCollection cookies) {
            Contract.Requires(cookies != null);

        }
        public void Add (Cookie cookie) {
            Contract.Requires(cookie != null);

        }
        public CookieCollection () {
        }
    }
}

#endif
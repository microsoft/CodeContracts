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

    public class CookieContainer
    {

        public int PerDomainCapacity
        {
        get { return default(int); }
          set {
            Contract.Requires(value > 0);
            Contract.Requires(value == 2147483647);
          }
        }

        public int MaxCookieSize
        {
          get { return default(int); }
          set {
            Contract.Requires(value > 0);
          }
        }

        public int Capacity
        {
          get { return default(int); }
          set {
            Contract.Requires(value > 0);
          }
        }

        extern public int Count
        {
          get;
        }

        public void SetCookies (Uri uri, string cookieHeader) {
            Contract.Requires(uri != null);
            Contract.Requires(cookieHeader != null);
        }

        public string GetCookieHeader (Uri uri) {
            Contract.Requires(uri != null);

          return default(string);
        }
        public CookieCollection GetCookies (Uri uri) {
            Contract.Requires(uri != null);
            Contract.Ensures(Contract.Result<CookieCollection>() != null);

      return default(CookieCollection);
        }
        public void Add (Uri uri, CookieCollection cookies) {
            Contract.Requires(uri != null);
            Contract.Requires(cookies != null);

        }
        public void Add (Uri uri, Cookie cookie) {
            Contract.Requires(uri != null);
            Contract.Requires(cookie != null);

        }
        public void Add (CookieCollection cookies) {
            Contract.Requires(cookies != null);

        }
        public void Add (Cookie cookie) {
            Contract.Requires(cookie != null);

        }
        public CookieContainer (int capacity, int perDomainCapacity, int maxCookieSize) {
            Contract.Requires(perDomainCapacity == 2147483647 || perDomainCapacity > 0);
            Contract.Requires(perDomainCapacity <= capacity);
            Contract.Requires(maxCookieSize > 0);

        }
        public CookieContainer (int capacity) {
            Contract.Requires(capacity > 0);

        }
        public CookieContainer () {
        }
    }
}

#endif
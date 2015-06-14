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

using System.Diagnostics.Contracts;
using System;

namespace System.Net
{

    public class FileWebRequest
    {

        public string ContentType
        {
          get;
          set;
        }

        public string Method
        {
          get;
          set;
        }

        public ICredentials Credentials
        {
          get;
          set;
        }

        public string ConnectionGroupName
        {
          get;
          set;
        }

        public WebHeaderCollection Headers
        {
          get;
        }

        public Int64 ContentLength
        {
          get;
          set
            Contract.Requires(value >= 0);
        }

        public IWebProxy Proxy
        {
          get;
          set;
        }

        public bool PreAuthenticate
        {
          get;
          set;
        }

        public int Timeout
        {
          get;
          set
            Contract.Requires(value >= 0 || value == -1);
        }

        public Uri RequestUri
        {
          get;
        }

        public WebResponse GetResponse () {

          return default(WebResponse);
        }
        public System.IO.Stream GetRequestStream () {

          return default(System.IO.Stream);
        }
        public WebResponse EndGetResponse (IAsyncResult! asyncResult) {
            Contract.Requires(asyncResult != null);

          return default(WebResponse);
        }
        public System.IO.Stream EndGetRequestStream (IAsyncResult! asyncResult) {
            Contract.Requires(asyncResult != null);

          return default(System.IO.Stream);
        }
        public IAsyncResult BeginGetResponse (AsyncCallback callback, object state) {

          return default(IAsyncResult);
        }
        public IAsyncResult BeginGetRequestStream (AsyncCallback callback, object state) {
          return default(IAsyncResult);
        }
    }
}

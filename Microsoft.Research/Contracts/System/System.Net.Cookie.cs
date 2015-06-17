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

using System;
using System.Diagnostics.Contracts;

namespace System.Net
{

    public class Cookie
    {

      extern public DateTime TimeStamp {
        get;
      }

        public string Value
        {
          get;
          set;
        }

        public string Comment
        {
          get;
          set;
        }

        public bool Secure
        {
          get;
          set;
        }

        public string Port
        {
          get;
          set;
        }

        public DateTime Expires
        {
          get;
          set;
        }

        public bool Discard
        {
          get;
          set;
        }

        public string Name {
          get {
            Contract.Ensures(Contract.Result<string>() != null);
            return default(string);
          }
          set {
            Contract.Requires(value != null);
          }
        }

        public int Version
        {
          get;
          set;
        }

        public bool Expired
        {
          get;
          set;
        }

        public string Path
        {
          get;
          set;
        }

        public Uri CommentUri
        {
          get;
          set;
        }

        public string Domain
        {
          get;
          set;
        }

        public Cookie (string name, string value, string path, string domain) {

        }
        public Cookie (string name, string value, string path) {

        }
        public Cookie (string name, string value) {

        }
        public Cookie () {
        }
    }
}

#endif
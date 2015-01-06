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

    public class AuthenticationManager
    {

        public static System.Collections.IEnumerator RegisteredModules
        {
          get;
        }

        public static void Unregister (string! authenticationScheme) {
            Contract.Requires(authenticationScheme != null);

        }
        public static void Unregister (IAuthenticationModule! authenticationModule) {
            Contract.Requires(authenticationModule != null);

        }
        public static void Register (IAuthenticationModule! authenticationModule) {
            Contract.Requires(authenticationModule != null);

        }
        public static Authorization PreAuthenticate (WebRequest! request, ICredentials credentials) {
            Contract.Requires(request != null);

          return default(Authorization);
        }
        public static Authorization Authenticate (string! challenge, WebRequest! request, ICredentials! credentials) {
            Contract.Requires(request != null);
            Contract.Requires(credentials != null);
            Contract.Requires(challenge != null);
          return default(Authorization);
        }
    }
}

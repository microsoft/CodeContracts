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

namespace System.Security.Permissions
{

    public class PublisherIdentityPermission
    {

        public System.Security.Cryptography.X509Certificates.X509Certificate Certificate
        {
          get;
          set;
        }

        public System.Security.SecurityElement ToXml () {

          return default(System.Security.SecurityElement);
        }
        public void FromXml (System.Security.SecurityElement esd) {

        }
        public System.Security.IPermission Union (System.Security.IPermission target) {

          return default(System.Security.IPermission);
        }
        public System.Security.IPermission Intersect (System.Security.IPermission target) {

          return default(System.Security.IPermission);
        }
        public bool IsSubsetOf (System.Security.IPermission target) {

          return default(bool);
        }
        public System.Security.IPermission Copy () {

          return default(System.Security.IPermission);
        }
        public PublisherIdentityPermission (System.Security.Cryptography.X509Certificates.X509Certificate certificate) {

          return default(PublisherIdentityPermission);
        }
        public PublisherIdentityPermission (PermissionState state) {
            CodeContract.Requires((int)state != 1);
            CodeContract.Requires((int)state == 0);
          return default(PublisherIdentityPermission);
        }
    }
}

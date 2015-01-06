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

namespace System.Security.Policy
{

    public class PolicyStatement
    {

        public PolicyStatementAttribute Attributes
        {
          get;
          set;
        }

        public System.Security.PermissionSet PermissionSet
        {
          get;
          set;
        }

        public string AttributeString
        {
          get;
        }

        public void FromXml (System.Security.SecurityElement et, PolicyLevel level) {
            Contract.Requires(et != null);

        }
        public System.Security.SecurityElement ToXml (PolicyLevel level) {

          return default(System.Security.SecurityElement);
        }
        public void FromXml (System.Security.SecurityElement et) {

        }
        public System.Security.SecurityElement ToXml () {

          return default(System.Security.SecurityElement);
        }
        public PolicyStatement Copy () {

          return default(PolicyStatement);
        }
        public PolicyStatement (System.Security.PermissionSet permSet, PolicyStatementAttribute attributes) {

          return default(PolicyStatement);
        }
        public PolicyStatement (System.Security.PermissionSet permSet) {
          return default(PolicyStatement);
        }
    }
}

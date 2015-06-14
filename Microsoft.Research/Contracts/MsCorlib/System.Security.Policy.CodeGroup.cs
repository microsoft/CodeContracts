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

namespace System.Security.Policy
{

    public class CodeGroup
    {

        public string PermissionSetName
        {
          get;
        }

        public PolicyStatement PolicyStatement
        {
          get;
          set;
        }

        public string AttributeString
        {
          get;
        }

        public string Description
        {
          get;
          set;
        }

        public IMembershipCondition MembershipCondition
        {
          get;
          set{
            Contract.Requires(value != null);
          }
        }

        public System.Collections.IList Children
        {
          get;
            set
            {
                Contract.Requires(value != null);
            }
        }

        public string Name
        {
          get;
          set;
        }

        public string MergeLogic
        {
          get;
        }

        public bool Equals (CodeGroup cg, bool compareChildren) {

          return default(bool);
        }

        public void FromXml (System.Security.SecurityElement e, PolicyLevel level) {
            Contract.Requires(e != null);

        }
        public System.Security.SecurityElement ToXml (PolicyLevel level) {

          return default(System.Security.SecurityElement);
        }
        public void FromXml (System.Security.SecurityElement e) {

        }
        public System.Security.SecurityElement ToXml () {

          return default(System.Security.SecurityElement);
        }
        public CodeGroup Copy () {

          return default(CodeGroup);
        }
        public CodeGroup ResolveMatchingCodeGroups (Evidence arg0) {

          return default(CodeGroup);
        }
        public PolicyStatement Resolve (Evidence arg0) {

          return default(PolicyStatement);
        }
        public void RemoveChild (CodeGroup group) {

        }
        public void AddChild (CodeGroup group) {
            Contract.Requires(group != null);

        }
        public CodeGroup (IMembershipCondition membershipCondition, PolicyStatement policy) {
            Contract.Requires(membershipCondition != null);
          return default(CodeGroup);
        }
    }
}

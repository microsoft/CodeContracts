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

    public class PolicyLevel
    {

        extern public System.Collections.IList NamedPermissionSets
        {
          get;
        }

        extern public System.Collections.IList FullTrustAssemblies
        {
          get;
        }

        public CodeGroup RootCodeGroup
        {
          get { return default(CodeGroup); }
          set
          {
            Contract.Requires(value != null);
          }
        }

        extern public string StoreLocation
        {
          get;
        }

        extern public string Label
        {
          get;
        }

        public void FromXml (System.Security.SecurityElement e) {
            Contract.Requires(e != null);

        }
        public System.Security.SecurityElement ToXml () {

          return default(System.Security.SecurityElement);
        }
        public CodeGroup ResolveMatchingCodeGroups (Evidence evidence) {
            Contract.Requires(evidence != null);

          return default(CodeGroup);
        }
        public PolicyStatement Resolve (Evidence evidence) {
            Contract.Requires(evidence != null);

          return default(PolicyStatement);
        }
        public System.Security.NamedPermissionSet GetNamedPermissionSet (string name) {
            Contract.Requires(name != null);

          return default(System.Security.NamedPermissionSet);
        }
        public System.Security.NamedPermissionSet ChangeNamedPermissionSet (string name, System.Security.PermissionSet pSet) {
            Contract.Requires(name != null);
            Contract.Requires(pSet != null);

          return default(System.Security.NamedPermissionSet);
        }
        public System.Security.NamedPermissionSet RemoveNamedPermissionSet (string name) {
            Contract.Requires(name != null);

          return default(System.Security.NamedPermissionSet);
        }
        public System.Security.NamedPermissionSet RemoveNamedPermissionSet (System.Security.NamedPermissionSet permSet) {
            Contract.Requires(permSet != null);

          return default(System.Security.NamedPermissionSet);
        }
        public void AddNamedPermissionSet (System.Security.NamedPermissionSet permSet) {

        }
        public void Reset () {

        }
        public void Recover () {

        }
        public void RemoveFullTrustAssembly (StrongNameMembershipCondition snMC) {
            Contract.Requires(snMC != null);

        }
        public void RemoveFullTrustAssembly (StrongName sn) {
            Contract.Requires(sn != null);

        }
        public void AddFullTrustAssembly (StrongNameMembershipCondition snMC) {
            Contract.Requires(snMC != null);

        }
        public void AddFullTrustAssembly (StrongName sn) {
            Contract.Requires(sn != null);

        }
        public static PolicyLevel CreateAppDomainLevel () {
          return default(PolicyLevel);
        }
    }
}

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

namespace System.Security
{

    public class SecurityManager
    {

        public static bool SecurityEnabled
        {
          get;
          set;
        }

        public static bool CheckExecutionRights
        {
          get;
          set;
        }

        public static void SavePolicy () {

        }
        public static System.Collections.IEnumerator PolicyHierarchy () {

          return default(System.Collections.IEnumerator);
        }
        public static System.Collections.IEnumerator ResolvePolicyGroups (System.Security.Policy.Evidence evidence) {

          return default(System.Collections.IEnumerator);
        }
        public static PermissionSet ResolvePolicy (System.Security.Policy.Evidence evidence) {

          return default(PermissionSet);
        }
        public static PermissionSet ResolvePolicy (System.Security.Policy.Evidence evidence, PermissionSet reqdPset, PermissionSet optPset, PermissionSet denyPset, ref PermissionSet denied) {

          return default(PermissionSet);
        }
        public static void SavePolicyLevel (System.Security.Policy.PolicyLevel level) {

        }
        public static System.Security.Policy.PolicyLevel LoadPolicyLevelFromString (string! str, PolicyLevelType type) {
            CodeContract.Requires(str != null);

          return default(System.Security.Policy.PolicyLevel);
        }
        public static System.Security.Policy.PolicyLevel LoadPolicyLevelFromFile (string! path, PolicyLevelType type) {
            CodeContract.Requires(path != null);

          return default(System.Security.Policy.PolicyLevel);
        }
        public static void GetZoneAndOrigin (ref System.Collections.ArrayList zone, ref System.Collections.ArrayList origin) {

        }
        public static bool IsGranted (IPermission perm) {
          return default(bool);
        }
    }
}

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

// File System.Security.SecurityManager.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Security
{
  static public partial class SecurityManager
  {
    #region Methods and constructors
    public static bool CurrentThreadRequiresSecurityContextCapture()
    {
      return default(bool);
    }

    public static PermissionSet GetStandardSandbox(System.Security.Policy.Evidence evidence)
    {
      return default(PermissionSet);
    }

    public static void GetZoneAndOrigin(out System.Collections.ArrayList zone, out System.Collections.ArrayList origin)
    {
      zone = default(System.Collections.ArrayList);
      origin = default(System.Collections.ArrayList);
    }

    public static bool IsGranted(IPermission perm)
    {
      return default(bool);
    }

    public static System.Security.Policy.PolicyLevel LoadPolicyLevelFromFile(string path, PolicyLevelType type)
    {
      Contract.Ensures(Contract.Result<System.Security.Policy.PolicyLevel>() != null);

      return default(System.Security.Policy.PolicyLevel);
    }

    public static System.Security.Policy.PolicyLevel LoadPolicyLevelFromString(string str, PolicyLevelType type)
    {
      Contract.Ensures(Contract.Result<System.Security.Policy.PolicyLevel>() != null);

      return default(System.Security.Policy.PolicyLevel);
    }

    public static System.Collections.IEnumerator PolicyHierarchy()
    {
      return default(System.Collections.IEnumerator);
    }

    public static PermissionSet ResolvePolicy(System.Security.Policy.Evidence[] evidences)
    {
      Contract.Ensures(1 <= evidences.Length);

      return default(PermissionSet);
    }

    public static PermissionSet ResolvePolicy(System.Security.Policy.Evidence evidence, PermissionSet reqdPset, PermissionSet optPset, PermissionSet denyPset, out PermissionSet denied)
    {
      Contract.Ensures(Contract.Result<System.Security.PermissionSet>() != null);

      denied = default(PermissionSet);

      return default(PermissionSet);
    }

    public static PermissionSet ResolvePolicy(System.Security.Policy.Evidence evidence)
    {
      return default(PermissionSet);
    }

    public static System.Collections.IEnumerator ResolvePolicyGroups(System.Security.Policy.Evidence evidence)
    {
      return default(System.Collections.IEnumerator);
    }

    public static PermissionSet ResolveSystemPolicy(System.Security.Policy.Evidence evidence)
    {
      return default(PermissionSet);
    }

    public static void SavePolicy()
    {
    }

    public static void SavePolicyLevel(System.Security.Policy.PolicyLevel level)
    {
    }
    #endregion

    #region Properties and indexers
    public static bool CheckExecutionRights
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == true);

        return default(bool);
      }
      set
      {
      }
    }

    public static bool SecurityEnabled
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == true);

        return default(bool);
      }
      set
      {
      }
    }
    #endregion
  }
}

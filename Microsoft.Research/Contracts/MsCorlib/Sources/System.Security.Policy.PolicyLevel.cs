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

// File System.Security.Policy.PolicyLevel.cs
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


namespace System.Security.Policy
{
  sealed public partial class PolicyLevel
  {
    #region Methods and constructors
    public void AddFullTrustAssembly(StrongName sn)
    {
    }

    public void AddFullTrustAssembly(StrongNameMembershipCondition snMC)
    {
    }

    public void AddNamedPermissionSet(System.Security.NamedPermissionSet permSet)
    {
    }

    public System.Security.NamedPermissionSet ChangeNamedPermissionSet(string name, System.Security.PermissionSet pSet)
    {
      return default(System.Security.NamedPermissionSet);
    }

    public static PolicyLevel CreateAppDomainLevel()
    {
      Contract.Ensures(Contract.Result<System.Security.Policy.PolicyLevel>() != null);

      return default(PolicyLevel);
    }

    public void FromXml(System.Security.SecurityElement e)
    {
    }

    public System.Security.NamedPermissionSet GetNamedPermissionSet(string name)
    {
      return default(System.Security.NamedPermissionSet);
    }

    internal PolicyLevel()
    {
    }

    public void Recover()
    {
    }

    public void RemoveFullTrustAssembly(StrongName sn)
    {
    }

    public void RemoveFullTrustAssembly(StrongNameMembershipCondition snMC)
    {
    }

    public System.Security.NamedPermissionSet RemoveNamedPermissionSet(System.Security.NamedPermissionSet permSet)
    {
      return default(System.Security.NamedPermissionSet);
    }

    public System.Security.NamedPermissionSet RemoveNamedPermissionSet(string name)
    {
      return default(System.Security.NamedPermissionSet);
    }

    public void Reset()
    {
    }

    public PolicyStatement Resolve(Evidence evidence)
    {
      Contract.Ensures(Contract.Result<System.Security.Policy.PolicyStatement>() != null);

      return default(PolicyStatement);
    }

    public CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
    {
      Contract.Requires(this.RootCodeGroup != null);

      return default(CodeGroup);
    }

    public System.Security.SecurityElement ToXml()
    {
      Contract.Ensures(Contract.Result<System.Security.SecurityElement>() != null);

      return default(System.Security.SecurityElement);
    }
    #endregion

    #region Properties and indexers
    public System.Collections.IList FullTrustAssemblies
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.IList>() != null);

        return default(System.Collections.IList);
      }
    }

    public string Label
    {
      get
      {
        return default(string);
      }
    }

    public System.Collections.IList NamedPermissionSets
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.IList>() != null);

        return default(System.Collections.IList);
      }
    }

    public CodeGroup RootCodeGroup
    {
      get
      {
        return default(CodeGroup);
      }
      set
      {
      }
    }

    public string StoreLocation
    {
      get
      {
        return default(string);
      }
    }

    public System.Security.PolicyLevelType Type
    {
      get
      {
        return default(System.Security.PolicyLevelType);
      }
    }
    #endregion
  }
}

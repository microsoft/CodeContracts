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

// File System.Security.AccessControl.ObjectSecurity.cs
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


namespace System.Security.AccessControl
{
  abstract public partial class ObjectSecurity
  {
    #region Methods and constructors
    public abstract AccessRule AccessRuleFactory(System.Security.Principal.IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type);

    public abstract AuditRule AuditRuleFactory(System.Security.Principal.IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags);

    public System.Security.Principal.IdentityReference GetGroup(Type targetType)
    {
      return default(System.Security.Principal.IdentityReference);
    }

    public System.Security.Principal.IdentityReference GetOwner(Type targetType)
    {
      return default(System.Security.Principal.IdentityReference);
    }

    public byte[] GetSecurityDescriptorBinaryForm()
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);
      Contract.Ensures(System.Security.AccessControl.GenericSecurityDescriptor.Revision == 1);

      return default(byte[]);
    }

    public string GetSecurityDescriptorSddlForm(AccessControlSections includeSections)
    {
      Contract.Ensures(System.Security.AccessControl.GenericSecurityDescriptor.Revision == 1);

      return default(string);
    }

    public static bool IsSddlConversionSupported()
    {
      Contract.Ensures(Contract.Result<bool>() == true);

      return default(bool);
    }

    protected abstract bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified);

    public virtual new bool ModifyAccessRule(AccessControlModification modification, AccessRule rule, out bool modified)
    {
      Contract.Requires(this.AccessRuleType != null);

      modified = default(bool);

      return default(bool);
    }

    protected abstract bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified);

    public virtual new bool ModifyAuditRule(AccessControlModification modification, AuditRule rule, out bool modified)
    {
      Contract.Requires(this.AuditRuleType != null);

      modified = default(bool);

      return default(bool);
    }

    protected ObjectSecurity(bool isContainer, bool isDS)
    {
    }

    protected virtual new void Persist(System.Runtime.InteropServices.SafeHandle handle, AccessControlSections includeSections)
    {
    }

    protected virtual new void Persist(string name, AccessControlSections includeSections)
    {
    }

    protected virtual new void Persist(bool enableOwnershipPrivilege, string name, AccessControlSections includeSections)
    {
    }

    public virtual new void PurgeAccessRules(System.Security.Principal.IdentityReference identity)
    {
    }

    public virtual new void PurgeAuditRules(System.Security.Principal.IdentityReference identity)
    {
    }

    protected void ReadLock()
    {
    }

    protected void ReadUnlock()
    {
    }

    public void SetAccessRuleProtection(bool isProtected, bool preserveInheritance)
    {
    }

    public void SetAuditRuleProtection(bool isProtected, bool preserveInheritance)
    {
    }

    public void SetGroup(System.Security.Principal.IdentityReference identity)
    {
    }

    public void SetOwner(System.Security.Principal.IdentityReference identity)
    {
    }

    public void SetSecurityDescriptorBinaryForm(byte[] binaryForm, AccessControlSections includeSections)
    {
    }

    public void SetSecurityDescriptorBinaryForm(byte[] binaryForm)
    {
    }

    public void SetSecurityDescriptorSddlForm(string sddlForm, AccessControlSections includeSections)
    {
    }

    public void SetSecurityDescriptorSddlForm(string sddlForm)
    {
    }

    protected void WriteLock()
    {
    }

    protected void WriteUnlock()
    {
    }
    #endregion

    #region Properties and indexers
    public abstract Type AccessRightType
    {
      get;
    }

    protected bool AccessRulesModified
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public abstract Type AccessRuleType
    {
      get;
    }

    public bool AreAccessRulesCanonical
    {
      get
      {
        return default(bool);
      }
    }

    public bool AreAccessRulesProtected
    {
      get
      {
        return default(bool);
      }
    }

    public bool AreAuditRulesCanonical
    {
      get
      {
        return default(bool);
      }
    }

    public bool AreAuditRulesProtected
    {
      get
      {
        return default(bool);
      }
    }

    protected bool AuditRulesModified
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public abstract Type AuditRuleType
    {
      get;
    }

    protected bool GroupModified
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    protected bool IsContainer
    {
      get
      {
        return default(bool);
      }
    }

    protected bool IsDS
    {
      get
      {
        return default(bool);
      }
    }

    protected bool OwnerModified
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }
    #endregion
  }
}

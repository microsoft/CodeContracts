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

// File System.Security.AccessControl.DirectoryObjectSecurity.cs
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
  abstract public partial class DirectoryObjectSecurity : ObjectSecurity
  {
    #region Methods and constructors
    public virtual new AccessRule AccessRuleFactory(System.Security.Principal.IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type, Guid objectType, Guid inheritedObjectType)
    {
      return default(AccessRule);
    }

    protected void AddAccessRule(ObjectAccessRule rule)
    {
    }

    protected void AddAuditRule(ObjectAuditRule rule)
    {
    }

    public virtual new AuditRule AuditRuleFactory(System.Security.Principal.IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags, Guid objectType, Guid inheritedObjectType)
    {
      return default(AuditRule);
    }

    protected DirectoryObjectSecurity(CommonSecurityDescriptor securityDescriptor) : base (default(CommonSecurityDescriptor))
    {
    }

    protected DirectoryObjectSecurity() : base (default(CommonSecurityDescriptor))
    {
    }

    public AuthorizationRuleCollection GetAccessRules(bool includeExplicit, bool includeInherited, Type targetType)
    {
      Contract.Ensures(Contract.Result<System.Security.AccessControl.AuthorizationRuleCollection>() != null);

      return default(AuthorizationRuleCollection);
    }

    public AuthorizationRuleCollection GetAuditRules(bool includeExplicit, bool includeInherited, Type targetType)
    {
      Contract.Ensures(Contract.Result<System.Security.AccessControl.AuthorizationRuleCollection>() != null);

      return default(AuthorizationRuleCollection);
    }

    protected override bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified)
    {
      modified = default(bool);

      return default(bool);
    }

    protected override bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified)
    {
      modified = default(bool);

      return default(bool);
    }

    protected bool RemoveAccessRule(ObjectAccessRule rule)
    {
      return default(bool);
    }

    protected void RemoveAccessRuleAll(ObjectAccessRule rule)
    {
    }

    protected void RemoveAccessRuleSpecific(ObjectAccessRule rule)
    {
    }

    protected bool RemoveAuditRule(ObjectAuditRule rule)
    {
      return default(bool);
    }

    protected void RemoveAuditRuleAll(ObjectAuditRule rule)
    {
    }

    protected void RemoveAuditRuleSpecific(ObjectAuditRule rule)
    {
    }

    protected void ResetAccessRule(ObjectAccessRule rule)
    {
    }

    protected void SetAccessRule(ObjectAccessRule rule)
    {
    }

    protected void SetAuditRule(ObjectAuditRule rule)
    {
    }
    #endregion
  }
}

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

// File System.Security.AccessControl.ObjectSecurity_1.cs
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
  abstract public partial class ObjectSecurity<T> : NativeObjectSecurity
  {
    #region Methods and constructors
    public override AccessRule AccessRuleFactory(System.Security.Principal.IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
    {
      return default(AccessRule);
    }

    public virtual new void AddAccessRule(AccessRule<T> rule)
    {
    }

    public virtual new void AddAuditRule(AuditRule<T> rule)
    {
    }

    public override AuditRule AuditRuleFactory(System.Security.Principal.IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
    {
      return default(AuditRule);
    }

    protected ObjectSecurity(bool isContainer, ResourceType resourceType) : base (default(bool), default(ResourceType))
    {
      Contract.Ensures(false);
    }

    protected ObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, Object exceptionContext) : base (default(bool), default(ResourceType))
    {
      Contract.Ensures(false);
    }

    protected ObjectSecurity(bool isContainer, ResourceType resourceType, System.Runtime.InteropServices.SafeHandle safeHandle, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, Object exceptionContext) : base (default(bool), default(ResourceType))
    {
      Contract.Ensures(false);
    }

    protected ObjectSecurity(bool isContainer, ResourceType resourceType, System.Runtime.InteropServices.SafeHandle safeHandle, AccessControlSections includeSections) : base (default(bool), default(ResourceType))
    {
      Contract.Ensures(false);
    }

    protected ObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections) : base (default(bool), default(ResourceType))
    {
      Contract.Ensures(false);
    }

    protected internal void Persist(string name)
    {
    }

    protected internal void Persist(System.Runtime.InteropServices.SafeHandle handle)
    {
    }

    public virtual new bool RemoveAccessRule(AccessRule<T> rule)
    {
      return default(bool);
    }

    public virtual new void RemoveAccessRuleAll(AccessRule<T> rule)
    {
    }

    public virtual new void RemoveAccessRuleSpecific(AccessRule<T> rule)
    {
    }

    public virtual new bool RemoveAuditRule(AuditRule<T> rule)
    {
      return default(bool);
    }

    public virtual new void RemoveAuditRuleAll(AuditRule<T> rule)
    {
    }

    public virtual new void RemoveAuditRuleSpecific(AuditRule<T> rule)
    {
    }

    public virtual new void ResetAccessRule(AccessRule<T> rule)
    {
    }

    public virtual new void SetAccessRule(AccessRule<T> rule)
    {
    }

    public virtual new void SetAuditRule(AuditRule<T> rule)
    {
    }
    #endregion

    #region Properties and indexers
    public override Type AccessRightType
    {
      get
      {
        return default(Type);
      }
    }

    public override Type AccessRuleType
    {
      get
      {
        return default(Type);
      }
    }

    public override Type AuditRuleType
    {
      get
      {
        return default(Type);
      }
    }
    #endregion
  }
}

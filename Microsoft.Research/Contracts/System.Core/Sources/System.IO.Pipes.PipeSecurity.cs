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

// File System.IO.Pipes.PipeSecurity.cs
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


namespace System.IO.Pipes
{
  public partial class PipeSecurity : System.Security.AccessControl.NativeObjectSecurity
  {
    #region Methods and constructors
    public override System.Security.AccessControl.AccessRule AccessRuleFactory(System.Security.Principal.IdentityReference identityReference, int accessMask, bool isInherited, System.Security.AccessControl.InheritanceFlags inheritanceFlags, System.Security.AccessControl.PropagationFlags propagationFlags, System.Security.AccessControl.AccessControlType type)
    {
      return default(System.Security.AccessControl.AccessRule);
    }

    public void AddAccessRule(PipeAccessRule rule)
    {
    }

    public void AddAuditRule(PipeAuditRule rule)
    {
    }

    public sealed override System.Security.AccessControl.AuditRule AuditRuleFactory(System.Security.Principal.IdentityReference identityReference, int accessMask, bool isInherited, System.Security.AccessControl.InheritanceFlags inheritanceFlags, System.Security.AccessControl.PropagationFlags propagationFlags, System.Security.AccessControl.AuditFlags flags)
    {
      return default(System.Security.AccessControl.AuditRule);
    }

    protected internal void Persist(string name)
    {
    }

    protected internal void Persist(System.Runtime.InteropServices.SafeHandle handle)
    {
    }

    public PipeSecurity() : base (default(bool), default(System.Security.AccessControl.ResourceType))
    {
    }

    public bool RemoveAccessRule(PipeAccessRule rule)
    {
      Contract.Requires(rule.IdentityReference != null);

      return default(bool);
    }

    public void RemoveAccessRuleSpecific(PipeAccessRule rule)
    {
      Contract.Requires(rule.IdentityReference != null);
    }

    public bool RemoveAuditRule(PipeAuditRule rule)
    {
      return default(bool);
    }

    public void RemoveAuditRuleAll(PipeAuditRule rule)
    {
    }

    public void RemoveAuditRuleSpecific(PipeAuditRule rule)
    {
    }

    public void ResetAccessRule(PipeAccessRule rule)
    {
    }

    public void SetAccessRule(PipeAccessRule rule)
    {
    }

    public void SetAuditRule(PipeAuditRule rule)
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

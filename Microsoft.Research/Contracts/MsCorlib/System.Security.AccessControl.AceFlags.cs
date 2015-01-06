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

namespace System.Security.AccessControl
{
  // Summary:
  //     Specifies the inheritance and auditing behavior of an access control entry
  //     (ACE).
  public enum AceFlags
  {
    // Summary:
    //     No ACE flags are set.
    None = 0,
    //
    // Summary:
    //     The access mask is propagated onto child leaf objects.
    ObjectInherit = 1,
    //
    // Summary:
    //     The access mask is propagated to child container objects.
    ContainerInherit = 2,
    //
    // Summary:
    //     The access checks do not apply to the object; they only apply to its children.
    NoPropagateInherit = 4,
    //
    // Summary:
    //     The access mask is propagated only to child objects. This includes both container
    //     and leaf child objects.
    InheritOnly = 8,
    //
    // Summary:
    //     A logical OR of System.Security.AccessControl.AceFlags.ObjectInherit, System.Security.AccessControl.AceFlags.ContainerInherit,
    //     System.Security.AccessControl.AceFlags.NoPropagateInherit, and System.Security.AccessControl.AceFlags.InheritOnly.
    InheritanceFlags = 15,
    //
    // Summary:
    //     An ACE is inherited from a parent container rather than being explicitly
    //     set for an object.
    Inherited = 16,
    //
    // Summary:
    //     Successful access attempts are audited.
    SuccessfulAccess = 64,
    //
    // Summary:
    //     Failed access attempts are audited.
    FailedAccess = 128,
    //
    // Summary:
    //     All access attempts are audited.
    AuditFlags = 192,
  }
}

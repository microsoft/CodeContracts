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
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Security.Principal;

namespace System.Security.AccessControl {
  // Summary:
  //     Represents a combination of a user's identity and an access mask. An System.Security.AccessControl.AuditRule
  //     object also contains information about how the rule is inherited by child
  //     objects, how that inheritance is propagated, and for what conditions it is
  //     audited.
  public abstract class AuditRule : AuthorizationRule {
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.AuditRule
    //     class by using the specified values.
    //
    // Parameters:
    //   identity:
    //     The identity to which the audit rule applies. It must be an object that can
    //     be cast as a System.Security.Principal.SecurityIdentifier.
    //
    //   accessMask:
    //     The access mask of this rule. The access mask is a 32-bit collection of anonymous
    //     bits, the meaning of which is defined by the individual integrators.
    //
    //   isInherited:
    //     true to inherit this rule from a parent container.
    //
    //   inheritanceFlags:
    //     The inheritance properties of the audit rule.
    //
    //   propagationFlags:
    //     Whether inherited audit rules are automatically propagated. The propagation
    //     flags are ignored if inheritanceFlags is set to System.Security.AccessControl.InheritanceFlags.None.
    //
    //   auditFlags:
    //     The conditions for which the rule is audited.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value of the identity parameter cannot be cast as a System.Security.Principal.SecurityIdentifier,
    //     or the auditFlags parameter contains an invalid value.
    //
    //   System.ArgumentOutOfRangeException:
    //     The value of the accessMask parameter is zero, or the inheritanceFlags or
    //     propagationFlags parameters contain unrecognized flag values.
    extern protected AuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags);

    // Summary:
    //     Gets the audit flags for this audit rule.
    //
    // Returns:
    //     A bitwise combination of the enumeration values. This combination specifies
    //     the audit conditions for this audit rule.
    extern public AuditFlags AuditFlags { get; }
  }
}

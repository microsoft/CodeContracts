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
using System.Security.Principal;

namespace System.Security.AccessControl {
  // Summary:
  //     Determines access to securable objects. The derived classes System.Security.AccessControl.AccessRule
  //     and System.Security.AccessControl.AuditRule offer specializations for access
  //     and audit functionality.
  public abstract class AuthorizationRule {
    // Summary:
    //     Initializes a new instance of the System.Security.AuthorizationControl.AccessRule
    //     class by using the specified values.
    //
    // Parameters:
    //   identity:
    //     The identity to which the access rule applies. This parameter must be an
    //     object that can be cast as a System.Security.Principal.SecurityIdentifier.
    //
    //   accessMask:
    //     The access mask of this rule. The access mask is a 32-bit collection of anonymous
    //     bits, the meaning of which is defined by the individual integrators.
    //
    //   isInherited:
    //     true to inherit this rule from a parent container.
    //
    //   inheritanceFlags:
    //     The inheritance properties of the access rule.
    //
    //   propagationFlags:
    //     Whether inherited access rules are automatically propagated. The propagation
    //     flags are ignored if inheritanceFlags is set to System.Security.AccessControl.InheritanceFlags.None.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value of the identity parameter cannot be cast as a System.Security.Principal.SecurityIdentifier.
    //
    //   System.ArgumentOutOfRangeException:
    //     The value of the accessMask parameter is zero, or the inheritanceFlags or
    //     propagationFlags parameters contain unrecognized flag values.
    extern protected internal AuthorizationRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags);

    // Summary:
    //     Gets the access mask for this rule.
    //
    // Returns:
    //     The access mask for this rule.
    extern protected internal int AccessMask { get; }
    //
    // Summary:
    //     Gets the System.Security.Principal.IdentityReference to which this rule applies.
    //
    // Returns:
    //     The System.Security.Principal.IdentityReference to which this rule applies.
    extern public IdentityReference IdentityReference { get; }
    //
    // Summary:
    //     Gets the value of flags that determine how this rule is inherited by child
    //     objects.
    //
    // Returns:
    //     A bitwise combination of the enumeration values.
    extern public InheritanceFlags InheritanceFlags { get; }
    //
    // Summary:
    //     Gets a value indicating whether this rule is explicitly set or is inherited
    //     from a parent container object.
    //
    // Returns:
    //     true if this rule is not explicitly set but is instead inherited from a parent
    //     container.
    extern public bool IsInherited { get; }
    //
    // Summary:
    //     Gets the value of the propagation flags, which determine how inheritance
    //     of this rule is propagated to child objects. This property is significant
    //     only when the value of the System.Security.AccessControl.InheritanceFlags
    //     enumeration is not System.Security.AccessControl.InheritanceFlags.None.
    //
    // Returns:
    //     A bitwise combination of the enumeration values.
    extern public PropagationFlags PropagationFlags { get; }
  }
}

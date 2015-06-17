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

namespace System.Security.AccessControl
{
  // Summary:
  //     Represents a combination of a user's identity, an access mask, and an access
  //     control type (allow or deny). An System.Security.AccessControl.AccessRule
  //     object also contains information about the how the rule is inherited by child
  //     objects and how that inheritance is propagated.
  public abstract class AccessRule : AuthorizationRule
  {
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.AccessRule
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
    //     true if this rule is inherited from a parent container.
    //
    //   inheritanceFlags:
    //     The inheritance properties of the access rule.
    //
    //   propagationFlags:
    //     Whether inherited access rules are automatically propagated. The propagation
    //     flags are ignored if inheritanceFlags is set to System.Security.AccessControl.InheritanceFlags.None.
    //
    //   type:
    //     The valid access control type.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value of the identity parameter cannot be cast as a System.Security.Principal.SecurityIdentifier,
    //     or the type parameter contains an invalid value.
    //
    //   System.ArgumentOutOfRangeException:
    //     The value of the accessMask parameter is zero, or the inheritanceFlags or
    //     propagationFlags parameters contain unrecognized flag values.
    extern protected AccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type);

    // Summary:
    //     Gets the System.Security.AccessControl.AccessControlType value associated
    //     with this System.Security.AccessControl.AccessRule object.
    //
    // Returns:
    //     The System.Security.AccessControl.AccessControlType value associated with
    //     this System.Security.AccessControl.AccessRule object.
    extern public AccessControlType AccessControlType { get; }
  }
}

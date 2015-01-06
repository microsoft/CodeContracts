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
  //     Represents a security descriptor. A security descriptor includes an owner,
  //     a primary group, a Discretionary Access Control List (DACL), and a System
  //     Access Control List (SACL).
  public sealed class CommonSecurityDescriptor
  {
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.CommonSecurityDescriptor
    //     class from the specified System.Security.AccessControl.RawSecurityDescriptor
    //     object.
    //
    // Parameters:
    //   isContainer:
    //     true if the new security descriptor is associated with a container object.
    //
    //   isDS:
    //     true if the new security descriptor is associated with a directory object.
    //
    //   rawSecurityDescriptor:
    //     The System.Security.AccessControl.RawSecurityDescriptor object from which
    //     to create the new System.Security.AccessControl.CommonSecurityDescriptor
    //     object.
    extern public CommonSecurityDescriptor(bool isContainer, bool isDS, RawSecurityDescriptor rawSecurityDescriptor);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.CommonSecurityDescriptor
    //     class from the specified Security Descriptor Definition Language (SDDL) string.
    //
    // Parameters:
    //   isContainer:
    //     true if the new security descriptor is associated with a container object.
    //
    //   isDS:
    //     true if the new security descriptor is associated with a directory object.
    //
    //   sddlForm:
    //     The SDDL string from which to create the new System.Security.AccessControl.CommonSecurityDescriptor
    //     object.
    extern public CommonSecurityDescriptor(bool isContainer, bool isDS, string sddlForm);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.CommonSecurityDescriptor
    //     class from the specified array of byte values.
    //
    // Parameters:
    //   isContainer:
    //     true if the new security descriptor is associated with a container object.
    //
    //   isDS:
    //     true if the new security descriptor is associated with a directory object.
    //
    //   binaryForm:
    //     The array of byte values from which to create the new System.Security.AccessControl.CommonSecurityDescriptor
    //     object.
    //
    //   offset:
    //     The offset in the binaryForm array at which to begin copying.
    extern public CommonSecurityDescriptor(bool isContainer, bool isDS, byte[] binaryForm, int offset);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.CommonSecurityDescriptor
    //     class from the specified information.
    //
    // Parameters:
    //   isContainer:
    //     true if the new security descriptor is associated with a container object.
    //
    //   isDS:
    //     true if the new security descriptor is associated with a directory object.
    //
    //   flags:
    //     Flags that specify behavior of the new System.Security.AccessControl.CommonSecurityDescriptor
    //     object.
    //
    //   owner:
    //     The owner for the new System.Security.AccessControl.CommonSecurityDescriptor
    //     object.
    //
    //   group:
    //     The primary group for the new System.Security.AccessControl.CommonSecurityDescriptor
    //     object.
    //
    //   systemAcl:
    //     The System Access Control List (SACL) for the new System.Security.AccessControl.CommonSecurityDescriptor
    //     object.
    //
    //   discretionaryAcl:
    //     The Discretionary Access Control List (DACL) for the new System.Security.AccessControl.CommonSecurityDescriptor
    //     object.
    extern public CommonSecurityDescriptor(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, SystemAcl systemAcl, DiscretionaryAcl discretionaryAcl);

    //
    // Summary:
    //     Gets or sets the discretionary access control list (DACL) for this System.Security.AccessControl.CommonSecurityDescriptor
    //     object. The DACL contains access rules.
    //
    // Returns:
    //     The DACL for this System.Security.AccessControl.CommonSecurityDescriptor
    //     object.
    extern public DiscretionaryAcl DiscretionaryAcl { get; set; }

    //
    // Summary:
    //     Gets a Boolean value that specifies whether the object associated with this
    //     System.Security.AccessControl.CommonSecurityDescriptor object is a container
    //     object.
    //
    // Returns:
    //     true if the object associated with this System.Security.AccessControl.CommonSecurityDescriptor
    //     object is a container object; otherwise, false.
    extern public bool IsContainer { get; }
    //
    // Summary:
    //     Gets a Boolean value that specifies whether the Discretionary Access Control
    //     List (DACL) associated with this System.Security.AccessControl.CommonSecurityDescriptor
    //     object is in canonical order.
    //
    // Returns:
    //     true if the DACL associated with this System.Security.AccessControl.CommonSecurityDescriptor
    //     object is in canonical order; otherwise, false.
    extern public bool IsDiscretionaryAclCanonical { get; }
    //
    // Summary:
    //     Gets a Boolean value that specifies whether the object associated with this
    //     System.Security.AccessControl.CommonSecurityDescriptor object is a directory
    //     object.
    //
    // Returns:
    //     true if the object associated with this System.Security.AccessControl.CommonSecurityDescriptor
    //     object is a directory object; otherwise, false.
    extern public bool IsDS { get; }
    //
    // Summary:
    //     Gets a Boolean value that specifies whether the System Access Control List
    //     (SACL) associated with this System.Security.AccessControl.CommonSecurityDescriptor
    //     object is in canonical order.
    //
    // Returns:
    //     true if the SACL associated with this System.Security.AccessControl.CommonSecurityDescriptor
    //     object is in canonical order; otherwise, false.
    extern public bool IsSystemAclCanonical { get; }

    //
    // Summary:
    //     Gets or sets the System Access Control List (SACL) for this System.Security.AccessControl.CommonSecurityDescriptor
    //     object. The SACL contains audit rules.
    //
    // Returns:
    //     The SACL for this System.Security.AccessControl.CommonSecurityDescriptor
    //     object.
    extern public SystemAcl SystemAcl { get; set; }

    // Summary:
    //     Removes all access rules for the specified security identifier from the Discretionary
    //     Access Control List (DACL) associated with this System.Security.AccessControl.CommonSecurityDescriptor
    //     object.
    //
    // Parameters:
    //   sid:
    //     The security identifier for which to remove access rules.
    extern public void PurgeAccessControl(SecurityIdentifier sid);
    //
    // Summary:
    //     Removes all audit rules for the specified security identifier from the System
    //     Access Control List (SACL) associated with this System.Security.AccessControl.CommonSecurityDescriptor
    //     object.
    //
    // Parameters:
    //   sid:
    //     The security identifier for which to remove audit rules.
    extern public void PurgeAudit(SecurityIdentifier sid);
    //
    // Summary:
    //     Sets the inheritance protection for the Discretionary Access Control List
    //     (DACL) associated with this System.Security.AccessControl.CommonSecurityDescriptor
    //     object. DACLs that are protected do not inherit access rules from parent
    //     containers.
    //
    // Parameters:
    //   isProtected:
    //     true to protect the DACL from inheritance.
    //
    //   preserveInheritance:
    //     true to keep inherited access rules in the DACL; false to remove inherited
    //     access rules from the DACL.
    extern public void SetDiscretionaryAclProtection(bool isProtected, bool preserveInheritance);
    //
    // Summary:
    //     Sets the inheritance protection for the System Access Control List (SACL)
    //     associated with this System.Security.AccessControl.CommonSecurityDescriptor
    //     object. SACLs that are protected do not inherit audit rules from parent containers.
    //
    // Parameters:
    //   isProtected:
    //     true to protect the SACL from inheritance.
    //
    //   preserveInheritance:
    //     true to keep inherited audit rules in the SACL; false to remove inherited
    //     audit rules from the SACL.
    extern public void SetSystemAclProtection(bool isProtected, bool preserveInheritance);
  }
}

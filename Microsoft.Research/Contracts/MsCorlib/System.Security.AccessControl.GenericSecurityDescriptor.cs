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
  public abstract class GenericSecurityDescriptor
  {
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.GenericSecurity
    //     class.
    extern protected GenericSecurityDescriptor();

    // Summary:
    //     Gets the length, in bytes, of the binary representation of the current System.Security.AccessControl.GenericSecurityDescriptor
    //     object. This length should be used before marshaling the ACL into a binary
    //     array with the System.Security.AccessControl.GenericSecurityDescriptor.GetBinaryForm()
    //     method.
    //
    // Returns:
    //     The length, in bytes, of the binary representation of the current System.Security.AccessControl.GenericSecurityDescriptor
    //     object.
    extern public int BinaryLength { get; }
    //
    // Summary:
    //     Gets values that specify behavior of the System.Security.AccessControl.GenericSecurityDescriptor
    //     object.
    //
    // Returns:
    //     One or more values of the System.Security.AccessControl.ControlFlags enumeration
    //     combined with a logical OR operation.
#if false
    public abstract ControlFlags ControlFlags { get; }
#endif
    //
    // Summary:
    //     Gets or sets the primary group for this System.Security.AccessControl.GenericSecurityDescriptor
    //     object.
    //
    // Returns:
    //     The primary group for this System.Security.AccessControl.GenericSecurityDescriptor
    //     object.
#if false
    public abstract SecurityIdentifier Group { get; set; }
#endif
    //
    // Summary:
    //     Gets or sets the owner of the object associated with this System.Security.AccessControl.GenericSecurityDescriptor
    //     object.
    //
    // Returns:
    //     The owner of the object associated with this System.Security.AccessControl.GenericSecurityDescriptor
    //     object.
    public abstract SecurityIdentifier Owner { get; set; }
    //
    // Summary:
    //     Gets the revision level of the System.Security.AccessControl.GenericSecurityDescriptor
    //     object.
    //
    // Returns:
    //     A byte value that specifies the revision level of the System.Security.AccessControl.GenericSecurityDescriptor.
    extern public static byte Revision { get; }

    // Summary:
    //     Returns an array of byte values that represents the information contained
    //     in this System.Security.AccessControl.GenericSecurityDescriptor object.
    //
    // Parameters:
    //   binaryForm:
    //     The byte array into which the contents of the System.Security.AccessControl.GenericSecurityDescriptor
    //     is marshaled.
    //
    //   offset:
    //     The offset at which to start marshaling.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     offset is negative or too high to allow the entire System.Security.AccessControl.GenericSecurityDescriptor
    //     to be copied into array.
    extern public void GetBinaryForm(byte[] binaryForm, int offset);
    //
    // Summary:
    //     Returns the Security Descriptor Definition Language (SDDL) representation
    //     of the specified sections of the security descriptor that this System.Security.AccessControl.GenericSecurityDescriptor
    //     object represents.
    //
    // Parameters:
    //   includeSections:
    //     Specifies which sections (access rules, audit rules, primary group, owner)
    //     of the security descriptor to get.
    //
    // Returns:
    //     The SDDL representation of the specified sections of the security descriptor
    //     associated with this System.Security.AccessControl.GenericSecurityDescriptor
    //     object.
    extern public string GetSddlForm(AccessControlSections includeSections);
    //
    // Summary:
    //     Returns a boolean value that specifies whether the security descriptor associated
    //     with this System.Security.AccessControl.GenericSecurityDescriptor object
    //     can be converted to the Security Descriptor Definition Language (SDDL) format.
    //
    // Returns:
    //     true if the security descriptor associated with this System.Security.AccessControl.GenericSecurityDescriptor
    //     object can be converted to the Security Descriptor Definition Language (SDDL)
    //     format; otherwise, false.
    extern public static bool IsSddlConversionSupported();
  }
}

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
  public sealed class RawSecurityDescriptor : GenericSecurityDescriptor
  {
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.RawSecurityDescriptor
    //     class from the specified Security Descriptor Definition Language (SDDL) string.
    //
    // Parameters:
    //   sddlForm:
    //     The SDDL string from which to create the new System.Security.AccessControl.RawSecurityDescriptor
    //     object.
    public RawSecurityDescriptor(string sddlForm);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.RawSecurityDescriptor
    //     class from the specified array of byte values.
    //
    // Parameters:
    //   binaryForm:
    //     The array of byte values from which to create the new System.Security.AccessControl.RawSecurityDescriptor
    //     object.
    //
    //   offset:
    //     The offset in the binaryForm array at which to begin copying.
    public RawSecurityDescriptor(byte[] binaryForm, int offset);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.RawSecurityDescriptor
    //     class with the specified values.
    //
    // Parameters:
    //   flags:
    //     Flags that specify behavior of the new System.Security.AccessControl.RawSecurityDescriptor
    //     object.
    //
    //   owner:
    //     The owner for the new System.Security.AccessControl.RawSecurityDescriptor
    //     object.
    //
    //   group:
    //     The primary group for the new System.Security.AccessControl.RawSecurityDescriptor
    //     object.
    //
    //   systemAcl:
    //     The System Access Control List (SACL) for the new System.Security.AccessControl.RawSecurityDescriptor
    //     object.
    //
    //   discretionaryAcl:
    //     The Discretionary Access Control List (DACL) for the new System.Security.AccessControl.RawSecurityDescriptor
    //     object.
    public RawSecurityDescriptor(ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, RawAcl systemAcl, RawAcl discretionaryAcl);

    //
    // Summary:
    //     Gets or sets the Discretionary Access Control List (DACL) for this System.Security.AccessControl.RawSecurityDescriptor
    //     object. The DACL contains access rules.
    //
    // Returns:
    //     The DACL for this System.Security.AccessControl.RawSecurityDescriptor object.
    public RawAcl DiscretionaryAcl { get; set; }

    //
    // Summary:
    //     Gets or sets a byte value that represents the resource manager control bits
    //     associated with this System.Security.AccessControl.RawSecurityDescriptor
    //     object.
    //
    // Returns:
    //     A byte value that represents the resource manager control bits associated
    //     with this System.Security.AccessControl.RawSecurityDescriptor object.
    public byte ResourceManagerControl { get; set; }
    //
    // Summary:
    //     Gets or sets the System Access Control List (SACL) for this System.Security.AccessControl.RawSecurityDescriptor
    //     object. The SACL contains audit rules.
    //
    // Returns:
    //     The SACL for this System.Security.AccessControl.RawSecurityDescriptor object.
    public RawAcl SystemAcl { get; set; }

    // Summary:
    //     Sets the System.Security.AccessControl.RawSecurityDescriptor.ControlFlags
    //     property of this System.Security.AccessControl.RawSecurityDescriptor object
    //     to the specified value.
    //
    // Parameters:
    //   flags:
    //     One or more values of the System.Security.AccessControl.ControlFlags enumeration
    //     combined with a logical OR operation.
    public void SetFlags(ControlFlags flags);
  }
}

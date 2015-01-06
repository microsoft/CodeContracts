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
  //     Represents an Access Control Entry (ACE), and is the base class for all other
  //     ACE classes.
  public abstract class GenericAce
  {
    // Summary:
    //     Determines whether the specified System.Security.AccessControl.GenericAce
    //     objects are considered unequal.
    //
    // Parameters:
    //   left:
    //     The first System.Security.AccessControl.GenericAce object to compare.
    //
    //   right:
    //     The second System.Security.AccessControl.GenericAce to compare.
    //
    // Returns:
    //     true if the two System.Security.AccessControl.GenericAce objects are unequal;
    //     otherwise, false.
    public static bool operator !=(GenericAce left, GenericAce right);
    //
    // Summary:
    //     Determines whether the specified System.Security.AccessControl.GenericAce
    //     objects are considered equal.
    //
    // Parameters:
    //   left:
    //     The first System.Security.AccessControl.GenericAce object to compare.
    //
    //   right:
    //     The second System.Security.AccessControl.GenericAce to compare.
    //
    // Returns:
    //     true if the two System.Security.AccessControl.GenericAce objects are equal;
    //     otherwise, false.
    public static bool operator ==(GenericAce left, GenericAce right);

    // Summary:
    //     Gets or sets the System.Security.AccessControl.AceFlags associated with this
    //     System.Security.AccessControl.GenericAce object.
    //
    // Returns:
    //     The System.Security.AccessControl.AceFlags associated with this System.Security.AccessControl.GenericAce
    //     object.
    public AceFlags AceFlags { get; set; }
    //
    // Summary:
    //     Gets the type of this Access Control Entry (ACE).
    //
    // Returns:
    //     The type of this ACE.
    public AceType AceType { get; }
    //
    // Summary:
    //     Gets the audit information associated with this Access Control Entry (ACE).
    //
    // Returns:
    //     The audit information associated with this Access Control Entry (ACE).
    public AuditFlags AuditFlags { get; }
    //
    // Summary:
    //     Gets the length, in bytes, of the binary representation of the current System.Security.AccessControl.GenericAce
    //     object. This length should be used before marshaling the ACL into a binary
    //     array with the System.Security.AccessControl.GenericAce.GetBinaryForm() method.
    //
    // Returns:
    //     The length, in bytes, of the binary representation of the current System.Security.AccessControl.GenericAce
    //     object.
    public abstract int BinaryLength { get; }
    //
    // Summary:
    //     Gets flags that specify the inheritance properties of this Access Control
    //     Entry (ACE).
    //
    // Returns:
    //     Flags that specify the inheritance properties of this ACE.
    public InheritanceFlags InheritanceFlags { get; }
    //
    // Summary:
    //     Gets a Boolean value that specifies whether this Access Control Entry (ACE)
    //     is inherited or is set explicitly.
    //
    // Returns:
    //     true if this ACE is inherited; otherwise, false.
    public bool IsInherited { get; }
    //
    // Summary:
    //     Gets flags that specify the inheritance propagation properties of this Access
    //     Control Entry (ACE).
    //
    // Returns:
    //     Flags that specify the inheritance propagation properties of this ACE.
    public PropagationFlags PropagationFlags { get; }

    // Summary:
    //     Creates a deep copy of this Access Control Entry (ACE).
    //
    // Returns:
    //     The System.Security.AccessControl.GenericAce object that this method creates.
    public GenericAce Copy();
    //
    // Summary:
    //     Creates a System.Security.AccessControl.GenericAce object from the specified
    //     binary data.
    //
    // Parameters:
    //   binaryForm:
    //     The binary data from which to create the new System.Security.AccessControl.GenericAce
    //     object.
    //
    //   offset:
    //     The offset at which to begin unmarshaling.
    //
    // Returns:
    //     The System.Security.AccessControl.GenericAce object this method creates.
    public static GenericAce CreateFromBinaryForm(byte[] binaryForm, int offset);

    //
    // Summary:
    //     Marshals the contents of the System.Security.AccessControl.GenericAce object
    //     into the specified byte array beginning at the specified offset.
    //
    // Parameters:
    //   binaryForm:
    //     The byte array into which the contents of the System.Security.AccessControl.GenericAce
    //     is marshaled.
    //
    //   offset:
    //     The offset at which to start marshaling.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     offset is negative or too high to allow the entire System.Security.AccessControl.GenericAcl
    //     to be copied into array.
    public abstract void GetBinaryForm(byte[] binaryForm, int offset);
  }
}

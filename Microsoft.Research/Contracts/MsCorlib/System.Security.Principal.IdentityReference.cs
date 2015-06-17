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
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Security.Principal {
  // Summary:
  //     Represents an identity and is the base class for the System.Security.Principal.NTAccount
  //     and System.Security.Principal.SecurityIdentifier classes. This class does
  //     not provide a public constructor, and therefore cannot be inherited.
  public abstract class IdentityReference {
    // Summary:
    //     Compares two System.Security.Principal.IdentityReference objects to determine
    //     whether they are not equal. They are considered not equal if they have different
    //     canonical name representations than the one returned by the System.Security.Principal.IdentityReference.Value
    //     property or if one of the objects is null and the other is not.
    //
    // Parameters:
    //   left:
    //     The left System.Security.Principal.IdentityReference operand to use for the
    //     inequality comparison. This parameter can be null.
    //
    //   right:
    //     The right System.Security.Principal.IdentityReference operand to use for
    //     the inequality comparison. This parameter can be null.
    //
    // Returns:
    //     true if left and right are not equal; otherwise, false.
    extern public static bool operator !=(IdentityReference left, IdentityReference right);
    //
    // Summary:
    //     Compares two System.Security.Principal.IdentityReference objects to determine
    //     whether they are equal. They are considered equal if they have the same canonical
    //     name representation as the one returned by the System.Security.Principal.IdentityReference.Value
    //     property or if they are both null.
    //
    // Parameters:
    //   left:
    //     The left System.Security.Principal.IdentityReference operand to use for the
    //     equality comparison. This parameter can be null.
    //
    //   right:
    //     The right System.Security.Principal.IdentityReference operand to use for
    //     the equality comparison. This parameter can be null.
    //
    // Returns:
    //     true if left and right are equal; otherwise, false.
    extern public static bool operator ==(IdentityReference left, IdentityReference right);

    // Summary:
    //     Gets the string value of the identity represented by the System.Security.Principal.IdentityReference
    //     object.
    //
    // Returns:
    //     The string value of the identity represented by the System.Security.Principal.IdentityReference
    //     object.
    public abstract string Value { get; }

    //
    // Summary:
    //     Returns a value that indicates whether the specified type is a valid translation
    //     type for the System.Security.Principal.IdentityReference class.
    //
    // Parameters:
    //   targetType:
    //     The type being queried for validity to serve as a conversion from System.Security.Principal.IdentityReference.
    //     The following target types are valid:System.Security.Principal.NTAccountSystem.Security.Principal.SecurityIdentifier
    //
    // Returns:
    //     true if targetType is a valid translation type for the System.Security.Principal.IdentityReference
    //     class; otherwise, false.
    public abstract bool IsValidTargetType(Type targetType);
    //
    // Summary:
    //     Translates the account name represented by the System.Security.Principal.IdentityReference
    //     object into another System.Security.Principal.IdentityReference-derived type.
    //
    // Parameters:
    //   targetType:
    //     The target type for the conversion from System.Security.Principal.IdentityReference.
    //
    // Returns:
    //     The converted identity.
    public abstract IdentityReference Translate(Type targetType);
  }
}

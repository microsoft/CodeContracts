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
using System.Security.Principal;

namespace System.Security.AccessControl {
  // Summary:
  //     Represents an access rule for a cryptographic key. An access rule represents
  //     a combination of a user's identity, an access mask, and an access control
  //     type (allow or deny). An access rule object also contains information about
  //     the how the rule is inherited by child objects and how that inheritance is
  //     propagated.
  public sealed class CryptoKeyAccessRule : AccessRule {
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.CryptoKeyAccessRule
    //     class using the specified values.
    //
    // Parameters:
    //   identity:
    //     The identity to which the access rule applies. This parameter must be an
    //     object that can be cast as a System.Security.Principal.SecurityIdentifier.
    //
    //   cryptoKeyRights:
    //     The cryptographic key operation to which this access rule controls access.
    //
    //   type:
    //     The valid access control type.
    extern public CryptoKeyAccessRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AccessControlType type);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.CryptoKeyAccessRule
    //     class using the specified values.
    //
    // Parameters:
    //   identity:
    //     The identity to which the access rule applies.
    //
    //   cryptoKeyRights:
    //     The cryptographic key operation to which this access rule controls access.
    //
    //   type:
    //     The valid access control type.
    extern public CryptoKeyAccessRule(string identity, CryptoKeyRights cryptoKeyRights, AccessControlType type);

    // Summary:
    //     Gets the cryptographic key operation to which this access rule controls access.
    //
    // Returns:
    //     The cryptographic key operation to which this access rule controls access.
    extern public CryptoKeyRights CryptoKeyRights { get; }
  }
}

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
  //     Represents an audit rule for a cryptographic key. An audit rule represents
  //     a combination of a user's identity and an access mask. An audit rule also
  //     contains information about the how the rule is inherited by child objects,
  //     how that inheritance is propagated, and for what conditions it is audited.
  public sealed class CryptoKeyAuditRule : AuditRule {
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.CryptoKeyAuditRule
    //     class using the specified values.
    //
    // Parameters:
    //   identity:
    //     The identity to which the audit rule applies. This parameter must be an object
    //     that can be cast as a System.Security.Principal.SecurityIdentifier.
    //
    //   cryptoKeyRights:
    //     The cryptographic key operation for which this audit rule generates audits.
    //
    //   flags:
    //     The conditions that generate audits.
    extern public CryptoKeyAuditRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.CryptoKeyAuditRule
    //     class using the specified values.
    //
    // Parameters:
    //   identity:
    //     The identity to which the audit rule applies.
    //
    //   cryptoKeyRights:
    //     The cryptographic key operation for which this audit rule generates audits.
    //
    //   flags:
    //     The conditions that generate audits.
    extern public CryptoKeyAuditRule(string identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags);

    // Summary:
    //     Gets the cryptographic key operation for which this audit rule generates
    //     audits.
    //
    // Returns:
    //     The cryptographic key operation for which this audit rule generates audits.
    extern public CryptoKeyRights CryptoKeyRights { get; }
  }
}

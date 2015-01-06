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
  //     Provides the ability to control access to a cryptographic key object without
  //     direct manipulation of an Access Control List (ACL).
  public sealed class CryptoKeySecurity { //  : NativeObjectSecurity
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.CryptoKeySecurity
    //     class.
    extern public CryptoKeySecurity();
    //
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.CryptoKeySecurity
    //     class by using the specified security descriptor.
    //
    // Parameters:
    //   secuburityDescriptor:
    //     The security descriptor from which to create the new System.Security.AccessControl.CryptoKeySecurity
    //     object.
    extern public CryptoKeySecurity(CommonSecurityDescriptor securityDescriptor);

    //
    // Summary:
    //     Adds the specified access rule to the Discretionary Access Control List (DACL)
    //     associated with this System.Security.AccessControl.CryptoKeySecurity object.
    //
    // Parameters:
    //   rule:
    //     The access rule to add.
    extern public void AddAccessRule(CryptoKeyAccessRule rule);
    //
    // Summary:
    //     Adds the specified audit rule to the System Access Control List (SACL) associated
    //     with this System.Security.AccessControl.CryptoKeySecurity object.
    //
    // Parameters:
    //   rule:
    //     The audit rule to add.
    extern public void AddAuditRule(CryptoKeyAuditRule rule);

    //
    // Summary:
    //     Removes access rules that contain the same security identifier and access
    //     mask as the specified access rule from the Discretionary Access Control List
    //     (DACL) associated with this System.Security.AccessControl.CryptoKeySecurity
    //     object.
    //
    // Parameters:
    //   rule:
    //     The access rule to remove.
    //
    // Returns:
    //     true if the access rule was successfully removed; otherwise, false.
    extern public bool RemoveAccessRule(CryptoKeyAccessRule rule);
    //
    // Summary:
    //     Removes all access rules that have the same security identifier as the specified
    //     access rule from the Discretionary Access Control List (DACL) associated
    //     with this System.Security.AccessControl.CryptoKeySecurity object.
    //
    // Parameters:
    //   rule:
    //     The access rule to remove.
    extern public void RemoveAccessRuleAll(CryptoKeyAccessRule rule);
    //
    // Summary:
    //     Removes all access rules that exactly match the specified access rule from
    //     the Discretionary Access Control List (DACL) associated with this System.Security.AccessControl.CryptoKeySecurity
    //     object.
    //
    // Parameters:
    //   rule:
    //     The access rule to remove.
    extern public void RemoveAccessRuleSpecific(CryptoKeyAccessRule rule);
    //
    // Summary:
    //     Removes audit rules that contain the same security identifier and access
    //     mask as the specified audit rule from the System Access Control List (SACL)
    //     associated with this System.Security.AccessControl.CryptoKeySecurity object.
    //
    // Parameters:
    //   rule:
    //     The audit rule to remove.
    //
    // Returns:
    //     true if the audit rule was successfully removed; otherwise, false.
    extern public bool RemoveAuditRule(CryptoKeyAuditRule rule);
    //
    // Summary:
    //     Removes all audit rules that have the same security identifier as the specified
    //     audit rule from the System Access Control List (SACL) associated with this
    //     System.Security.AccessControl.CryptoKeySecurity object.
    //
    // Parameters:
    //   rule:
    //     The audit rule to remove.
    extern public void RemoveAuditRuleAll(CryptoKeyAuditRule rule);
    //
    // Summary:
    //     Removes all audit rules that exactly match the specified audit rule from
    //     the System Access Control List (SACL) associated with this System.Security.AccessControl.CryptoKeySecurity
    //     object.
    //
    // Parameters:
    //   rule:
    //     The audit rule to remove.
    extern public void RemoveAuditRuleSpecific(CryptoKeyAuditRule rule);
    //
    // Summary:
    //     Removes all access rules in the Discretionary Access Control List (DACL)
    //     associated with this System.Security.AccessControl.CryptoKeySecurity object
    //     and then adds the specified access rule.
    //
    // Parameters:
    //   rule:
    //     The access rule to reset.
    extern public void ResetAccessRule(CryptoKeyAccessRule rule);
    //
    // Summary:
    //     Removes all access rules that contain the same security identifier and qualifier
    //     as the specified access rule in the Discretionary Access Control List (DACL)
    //     associated with this System.Security.AccessControl.CryptoKeySecurity object
    //     and then adds the specified access rule.
    //
    // Parameters:
    //   rule:
    //     The access rule to set.
    extern public void SetAccessRule(CryptoKeyAccessRule rule);
    //
    // Summary:
    //     Removes all audit rules that contain the same security identifier and qualifier
    //     as the specified audit rule in the System Access Control List (SACL) associated
    //     with this System.Security.AccessControl.CryptoKeySecurity object and then
    //     adds the specified audit rule.
    //
    // Parameters:
    //   rule:
    //     The audit rule to set.
    extern public void SetAuditRule(CryptoKeyAuditRule rule);
  }
}

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

#if NETFRAMEWORK_4_0

using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  // Summary:
  //     Defines a set of commonly used security identifiers (SIDs).
  public enum WellKnownSidType
  {
    // Summary:
    //     Indicates a null SID.
    NullSid = 0,
    //
    // Summary:
    //     Indicates a SID that matches everyone.
    WorldSid = 1,
    //
    // Summary:
    //     Indicates a local SID.
    LocalSid = 2,
    //
    // Summary:
    //     Indicates a SID that matches the owner or creator of an object.
    CreatorOwnerSid = 3,
    //
    // Summary:
    //     Indicates a SID that matches the creator group of an object.
    CreatorGroupSid = 4,
    //
    // Summary:
    //     Indicates a creator owner server SID.
    CreatorOwnerServerSid = 5,
    //
    // Summary:
    //     Indicates a creator group server SID.
    CreatorGroupServerSid = 6,
    //
    // Summary:
    //     Indicates a SID for the Windows NT authority.
    NTAuthoritySid = 7,
    //
    // Summary:
    //     Indicates a SID for a dial-up account.
    DialupSid = 8,
    //
    // Summary:
    //     Indicates a SID for a network account. This SID is added to the process of
    //     a token when it logs on across a network.
    NetworkSid = 9,
    //
    // Summary:
    //     Indicates a SID for a batch process. This SID is added to the process of
    //     a token when it logs on as a batch job.
    BatchSid = 10,
    //
    // Summary:
    //     Indicates a SID for an interactive account. This SID is added to the process
    //     of a token when it logs on interactively.
    InteractiveSid = 11,
    //
    // Summary:
    //     Indicates a SID for a service. This SID is added to the process of a token
    //     when it logs on as a service.
    ServiceSid = 12,
    //
    // Summary:
    //     Indicates a SID for the anonymous account.
    AnonymousSid = 13,
    //
    // Summary:
    //     Indicates a proxy SID.
    ProxySid = 14,
    //
    // Summary:
    //     Indicates a SID for an enterprise controller.
    EnterpriseControllersSid = 15,
    //
    // Summary:
    //     Indicates a SID for self.
    SelfSid = 16,
    //
    // Summary:
    //     Indicates a SID for an authenticated user.
    AuthenticatedUserSid = 17,
    //
    // Summary:
    //     Indicates a SID for restricted code.
    RestrictedCodeSid = 18,
    //
    // Summary:
    //     Indicates a SID that matches a terminal server account.
    TerminalServerSid = 19,
    //
    // Summary:
    //     Indicates a SID that matches remote logons.
    RemoteLogonIdSid = 20,
    //
    // Summary:
    //     Indicates a SID that matches logon IDs.
    LogonIdsSid = 21,
    //
    // Summary:
    //     Indicates a SID that matches the local system.
    LocalSystemSid = 22,
    //
    // Summary:
    //     Indicates a SID that matches a local service.
    LocalServiceSid = 23,
    //
    // Summary:
    //     Indicates a SID that matches a network service.
    NetworkServiceSid = 24,
    //
    // Summary:
    //     Indicates a SID that matches the domain account.
    BuiltinDomainSid = 25,
    //
    // Summary:
    //     Indicates a SID that matches the administrator account.
    BuiltinAdministratorsSid = 26,
    //
    // Summary:
    //     Indicates a SID that matches built-in user accounts.
    BuiltinUsersSid = 27,
    //
    // Summary:
    //     Indicates a SID that matches the guest account.
    BuiltinGuestsSid = 28,
    //
    // Summary:
    //     Indicates a SID that matches the power users group.
    BuiltinPowerUsersSid = 29,
    //
    // Summary:
    //     Indicates a SID that matches the account operators account.
    BuiltinAccountOperatorsSid = 30,
    //
    // Summary:
    //     Indicates a SID that matches the system operators group.
    BuiltinSystemOperatorsSid = 31,
    //
    // Summary:
    //     Indicates a SID that matches the print operators group.
    BuiltinPrintOperatorsSid = 32,
    //
    // Summary:
    //     Indicates a SID that matches the backup operators group.
    BuiltinBackupOperatorsSid = 33,
    //
    // Summary:
    //     Indicates a SID that matches the replicator account.
    BuiltinReplicatorSid = 34,
    //
    // Summary:
    //     Indicates a SID that matches pre-Windows 2000 compatible accounts.
    BuiltinPreWindows2000CompatibleAccessSid = 35,
    //
    // Summary:
    //     Indicates a SID that matches remote desktop users.
    BuiltinRemoteDesktopUsersSid = 36,
    //
    // Summary:
    //     Indicates a SID that matches the network operators group.
    BuiltinNetworkConfigurationOperatorsSid = 37,
    //
    // Summary:
    //     Indicates a SID that matches the account administrators group.
    AccountAdministratorSid = 38,
    //
    // Summary:
    //     Indicates a SID that matches the account guest group.
    AccountGuestSid = 39,
    //
    // Summary:
    //     Indicates a SID that matches the account Kerberos target group.
    AccountKrbtgtSid = 40,
    //
    // Summary:
    //     Indicates a SID that matches the account domain administrator group.
    AccountDomainAdminsSid = 41,
    //
    // Summary:
    //     Indicates a SID that matches the account domain users group.
    AccountDomainUsersSid = 42,
    //
    // Summary:
    //     Indicates a SID that matches the account domain guests group.
    AccountDomainGuestsSid = 43,
    //
    // Summary:
    //     Indicates a SID that matches the account computer group.
    AccountComputersSid = 44,
    //
    // Summary:
    //     Indicates a SID that matches the account controller group.
    AccountControllersSid = 45,
    //
    // Summary:
    //     Indicates a SID that matches the certificate administrators group.
    AccountCertAdminsSid = 46,
    //
    // Summary:
    //     Indicates a SID that matches the schema administrators group.
    AccountSchemaAdminsSid = 47,
    //
    // Summary:
    //     Indicates a SID that matches the enterprise administrators group.
    AccountEnterpriseAdminsSid = 48,
    //
    // Summary:
    //     Indicates a SID that matches the policy administrators group.
    AccountPolicyAdminsSid = 49,
    //
    // Summary:
    //     Indicates a SID that matches the RAS and IAS server account.
    AccountRasAndIasServersSid = 50,
    //
    // Summary:
    //     Indicates a SID present when the Microsoft NTLM authentication package authenticated
    //     the client.
    NtlmAuthenticationSid = 51,
    //
    // Summary:
    //     Indicates a SID present when the Microsoft Digest authentication package
    //     authenticated the client.
    DigestAuthenticationSid = 52,
    //
    // Summary:
    //     Indicates a SID present when the Secure Channel (SSL/TLS) authentication
    //     package authenticated the client.
    SChannelAuthenticationSid = 53,
    //
    // Summary:
    //     Indicates a SID present when the user authenticated from within the forest
    //     or across a trust that does not have the selective authentication option
    //     enabled. If this SID is present, then System.Security.Principal.WellKnownSidType.OtherOrganizationSid
    //     cannot be present.
    ThisOrganizationSid = 54,
    //
    // Summary:
    //     Indicates a SID present when the user authenticated across a forest with
    //     the selective authentication option enabled. If this SID is present, then
    //     System.Security.Principal.WellKnownSidType.ThisOrganizationSid cannot be
    //     present.
    OtherOrganizationSid = 55,
    //
    // Summary:
    //     Indicates a SID that allows a user to create incoming forest trusts. It is
    //     added to the token of users who are a member of the Incoming Forest Trust
    //     Builders built-in group in the root domain of the forest.
    BuiltinIncomingForestTrustBuildersSid = 56,
    BuiltinPerformanceMonitoringUsersSid = 57,
    BuiltinPerformanceLoggingUsersSid = 58,
    //
    // Summary:
    //     Indicates a SID that matches the Windows Authorization Access group.
    BuiltinAuthorizationAccessSid = 59,
    //
    // Summary:
    //     Indicates the maximum defined SID in the System.Security.Principal.WellKnownSidType
    //     enumeration.
    MaxDefined = 60,
    //
    // Summary:
    //     Indicates a SID is present in a server that can issue Terminal Server licenses.
    WinBuiltinTerminalServerLicenseServersSid = 60,
  }
}
#endif
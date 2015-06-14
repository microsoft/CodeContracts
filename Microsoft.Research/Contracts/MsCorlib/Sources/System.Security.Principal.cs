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

// File System.Security.Principal.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Security.Principal
{
  public enum PrincipalPolicy
  {
    UnauthenticatedPrincipal = 0, 
    NoPrincipal = 1, 
    WindowsPrincipal = 2, 
  }

  public enum TokenAccessLevels
  {
    AssignPrimary = 1, 
    Duplicate = 2, 
    Impersonate = 4, 
    Query = 8, 
    QuerySource = 16, 
    AdjustPrivileges = 32, 
    AdjustGroups = 64, 
    AdjustDefault = 128, 
    AdjustSessionId = 256, 
    Read = 131080, 
    Write = 131296, 
    AllAccess = 983551, 
    MaximumAllowed = 33554432, 
  }

  public enum TokenImpersonationLevel
  {
    None = 0, 
    Anonymous = 1, 
    Identification = 2, 
    Impersonation = 3, 
    Delegation = 4, 
  }

  public enum WellKnownSidType
  {
    NullSid = 0, 
    WorldSid = 1, 
    LocalSid = 2, 
    CreatorOwnerSid = 3, 
    CreatorGroupSid = 4, 
    CreatorOwnerServerSid = 5, 
    CreatorGroupServerSid = 6, 
    NTAuthoritySid = 7, 
    DialupSid = 8, 
    NetworkSid = 9, 
    BatchSid = 10, 
    InteractiveSid = 11, 
    ServiceSid = 12, 
    AnonymousSid = 13, 
    ProxySid = 14, 
    EnterpriseControllersSid = 15, 
    SelfSid = 16, 
    AuthenticatedUserSid = 17, 
    RestrictedCodeSid = 18, 
    TerminalServerSid = 19, 
    RemoteLogonIdSid = 20, 
    LogonIdsSid = 21, 
    LocalSystemSid = 22, 
    LocalServiceSid = 23, 
    NetworkServiceSid = 24, 
    BuiltinDomainSid = 25, 
    BuiltinAdministratorsSid = 26, 
    BuiltinUsersSid = 27, 
    BuiltinGuestsSid = 28, 
    BuiltinPowerUsersSid = 29, 
    BuiltinAccountOperatorsSid = 30, 
    BuiltinSystemOperatorsSid = 31, 
    BuiltinPrintOperatorsSid = 32, 
    BuiltinBackupOperatorsSid = 33, 
    BuiltinReplicatorSid = 34, 
    BuiltinPreWindows2000CompatibleAccessSid = 35, 
    BuiltinRemoteDesktopUsersSid = 36, 
    BuiltinNetworkConfigurationOperatorsSid = 37, 
    AccountAdministratorSid = 38, 
    AccountGuestSid = 39, 
    AccountKrbtgtSid = 40, 
    AccountDomainAdminsSid = 41, 
    AccountDomainUsersSid = 42, 
    AccountDomainGuestsSid = 43, 
    AccountComputersSid = 44, 
    AccountControllersSid = 45, 
    AccountCertAdminsSid = 46, 
    AccountSchemaAdminsSid = 47, 
    AccountEnterpriseAdminsSid = 48, 
    AccountPolicyAdminsSid = 49, 
    AccountRasAndIasServersSid = 50, 
    NtlmAuthenticationSid = 51, 
    DigestAuthenticationSid = 52, 
    SChannelAuthenticationSid = 53, 
    ThisOrganizationSid = 54, 
    OtherOrganizationSid = 55, 
    BuiltinIncomingForestTrustBuildersSid = 56, 
    BuiltinPerformanceMonitoringUsersSid = 57, 
    BuiltinPerformanceLoggingUsersSid = 58, 
    BuiltinAuthorizationAccessSid = 59, 
    WinBuiltinTerminalServerLicenseServersSid = 60, 
    MaxDefined = 60, 
  }

  public enum WindowsAccountType
  {
    Normal = 0, 
    Guest = 1, 
    System = 2, 
    Anonymous = 3, 
  }

  public enum WindowsBuiltInRole
  {
    Administrator = 544, 
    User = 545, 
    Guest = 546, 
    PowerUser = 547, 
    AccountOperator = 548, 
    SystemOperator = 549, 
    PrintOperator = 550, 
    BackupOperator = 551, 
    Replicator = 552, 
  }
}

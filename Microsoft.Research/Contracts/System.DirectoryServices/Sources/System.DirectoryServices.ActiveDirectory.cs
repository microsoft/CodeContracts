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

// File System.DirectoryServices.ActiveDirectory.cs
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


namespace System.DirectoryServices.ActiveDirectory
{
  public enum ActiveDirectoryRole
  {
    SchemaRole = 0, 
    NamingRole = 1, 
    PdcRole = 2, 
    RidRole = 3, 
    InfrastructureRole = 4, 
  }

  public enum ActiveDirectorySiteOptions
  {
    None = 0, 
    AutoTopologyDisabled = 1, 
    TopologyCleanupDisabled = 2, 
    AutoMinimumHopDisabled = 4, 
    StaleServerDetectDisabled = 8, 
    AutoInterSiteTopologyDisabled = 16, 
    GroupMembershipCachingEnabled = 32, 
    ForceKccWindows2003Behavior = 64, 
    UseWindows2000IstgElection = 128, 
    RandomBridgeHeaderServerSelectionDisabled = 256, 
    UseHashingForReplicationSchedule = 512, 
    RedundantServerTopologyEnabled = 1024, 
  }

  public enum ActiveDirectorySyntax
  {
    CaseExactString = 0, 
    CaseIgnoreString = 1, 
    NumericString = 2, 
    DirectoryString = 3, 
    OctetString = 4, 
    SecurityDescriptor = 5, 
    Int = 6, 
    Int64 = 7, 
    Bool = 8, 
    Oid = 9, 
    GeneralizedTime = 10, 
    UtcTime = 11, 
    DN = 12, 
    DNWithBinary = 13, 
    DNWithString = 14, 
    Enumeration = 15, 
    IA5String = 16, 
    PrintableString = 17, 
    Sid = 18, 
    AccessPointDN = 19, 
    ORName = 20, 
    PresentationAddress = 21, 
    ReplicaLink = 22, 
  }

  public enum ActiveDirectoryTransportType
  {
    Rpc = 0, 
    Smtp = 1, 
  }

  public enum AdamRole
  {
    SchemaRole = 0, 
    NamingRole = 1, 
  }

  public enum DirectoryContextType
  {
    Domain = 0, 
    Forest = 1, 
    DirectoryServer = 2, 
    ConfigurationSet = 3, 
    ApplicationPartition = 4, 
  }

  public enum DomainCollisionOptions
  {
    None = 0, 
    SidDisabledByAdmin = 1, 
    SidDisabledByConflict = 2, 
    NetBiosNameDisabledByAdmin = 4, 
    NetBiosNameDisabledByConflict = 8, 
  }

  public enum DomainMode
  {
    Windows2000MixedDomain = 0, 
    Windows2000NativeDomain = 1, 
    Windows2003InterimDomain = 2, 
    Windows2003Domain = 3, 
    Windows2008Domain = 4,
#if NETFRAMEWORK_4_0
    Windows2008R2Domain = 5, 
#endif
  }

  public enum ForestMode
  {
    Windows2000Forest = 0, 
    Windows2003InterimForest = 1, 
    Windows2003Forest = 2, 
    Windows2008Forest = 3, 
#if NETFRAMEWORK_4_0
    Windows2008R2Forest = 4, 
#endif
  }

  public enum ForestTrustCollisionType
  {
    TopLevelName = 0, 
    Domain = 1, 
    Other = 2, 
  }

  public enum ForestTrustDomainStatus
  {
    Enabled = 0, 
    SidAdminDisabled = 1, 
    SidConflictDisabled = 2, 
    NetBiosNameAdminDisabled = 4, 
    NetBiosNameConflictDisabled = 8, 
  }

  public enum HourOfDay
  {
    Zero = 0, 
    One = 1, 
    Two = 2, 
    Three = 3, 
    Four = 4, 
    Five = 5, 
    Six = 6, 
    Seven = 7, 
    Eight = 8, 
    Nine = 9, 
    Ten = 10, 
    Eleven = 11, 
    Twelve = 12, 
    Thirteen = 13, 
    Fourteen = 14, 
    Fifteen = 15, 
    Sixteen = 16, 
    Seventeen = 17, 
    Eighteen = 18, 
    Nineteen = 19, 
    Twenty = 20, 
    TwentyOne = 21, 
    TwentyTwo = 22, 
    TwentyThree = 23, 
  }

  public enum LocatorOptions : long
  {
    ForceRediscovery = 1, 
    KdcRequired = 1024, 
    TimeServerRequired = 2048, 
    WriteableRequired = 4096, 
    AvoidSelf = 16384, 
  }

  public enum MinuteOfHour
  {
    Zero = 0, 
    Fifteen = 15, 
    Thirty = 30, 
    FortyFive = 45, 
  }

  public enum NotificationStatus
  {
    NoNotification = 0, 
    IntraSiteOnly = 1, 
    NotificationAlways = 2, 
  }

  public enum PropertyTypes
  {
    Indexed = 2, 
    InGlobalCatalog = 4, 
  }

  public enum ReplicationOperationType
  {
    Sync = 0, 
    Add = 1, 
    Delete = 2, 
    Modify = 3, 
    UpdateReference = 4, 
  }

  public enum ReplicationSecurityLevel
  {
    MutualAuthentication = 2, 
    Negotiate = 1, 
    NegotiatePassThrough = 0, 
  }

  public enum ReplicationSpan
  {
    IntraSite = 0, 
    InterSite = 1, 
  }

  public enum SchemaClassType
  {
    Type88 = 0, 
    Structural = 1, 
    Abstract = 2, 
    Auxiliary = 3, 
  }

  public enum SyncFromAllServersErrorCategory
  {
    ErrorContactingServer = 0, 
    ErrorReplicating = 1, 
    ServerUnreachable = 2, 
  }

  public enum SyncFromAllServersEvent
  {
    Error = 0, 
    SyncStarted = 1, 
    SyncCompleted = 2, 
    Finished = 3, 
  }

  public enum SyncFromAllServersOptions
  {
    None = 0, 
    AbortIfServerUnavailable = 1, 
    SyncAdjacentServerOnly = 2, 
    CheckServerAlivenessOnly = 8, 
    SkipInitialCheck = 16, 
    PushChangeOutward = 32, 
    CrossSite = 64, 
  }

  public delegate bool SyncUpdateCallback(SyncFromAllServersEvent eventType, string targetServer, string sourceServer, SyncFromAllServersOperationException exception);

  public enum TopLevelNameCollisionOptions
  {
    None = 0, 
    NewlyCreated = 1, 
    DisabledByAdmin = 2, 
    DisabledByConflict = 4, 
  }

  public enum TopLevelNameStatus
  {
    Enabled = 0, 
    NewlyCreated = 1, 
    AdminDisabled = 2, 
    ConflictDisabled = 4, 
  }

  public enum TrustDirection
  {
    Inbound = 1, 
    Outbound = 2, 
    Bidirectional = 3, 
  }

  public enum TrustType
  {
    TreeRoot = 0, 
    ParentChild = 1, 
    CrossLink = 2, 
    External = 3, 
    Forest = 4, 
    Kerberos = 5, 
    Unknown = 6, 
  }
}

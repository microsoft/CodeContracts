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

// File System.Net.Sockets.cs
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


namespace System.Net.Sockets
{
  public enum AddressFamily
  {
    Unknown = -1, 
    Unspecified = 0, 
    Unix = 1, 
    InterNetwork = 2, 
    ImpLink = 3, 
    Pup = 4, 
    Chaos = 5, 
    NS = 6, 
    Ipx = 6, 
    Iso = 7, 
    Osi = 7, 
    Ecma = 8, 
    DataKit = 9, 
    Ccitt = 10, 
    Sna = 11, 
    DecNet = 12, 
    DataLink = 13, 
    Lat = 14, 
    HyperChannel = 15, 
    AppleTalk = 16, 
    NetBios = 17, 
    VoiceView = 18, 
    FireFox = 19, 
    Banyan = 21, 
    Atm = 22, 
    InterNetworkV6 = 23, 
    Cluster = 24, 
    Ieee12844 = 25, 
    Irda = 26, 
    NetworkDesigners = 28, 
    Max = 29, 
  }

  public enum IOControlCode : long
  {
    AsyncIO = 2147772029, 
    NonBlockingIO = 2147772030, 
    DataToRead = 1074030207, 
    OobDataRead = 1074033415, 
    AssociateHandle = 2281701377, 
    EnableCircularQueuing = 671088642, 
    Flush = 671088644, 
    GetBroadcastAddress = 1207959557, 
    GetExtensionFunctionPointer = 3355443206, 
    GetQos = 3355443207, 
    GetGroupQos = 3355443208, 
    MultipointLoopback = 2281701385, 
    MulticastScope = 2281701386, 
    SetQos = 2281701387, 
    SetGroupQos = 2281701388, 
    TranslateHandle = 3355443213, 
    RoutingInterfaceQuery = 3355443220, 
    RoutingInterfaceChange = 2281701397, 
    AddressListQuery = 1207959574, 
    AddressListChange = 671088663, 
    QueryTargetPnpHandle = 1207959576, 
    NamespaceChange = 2281701401, 
    AddressListSort = 3355443225, 
    ReceiveAll = 2550136833, 
    ReceiveAllMulticast = 2550136834, 
    ReceiveAllIgmpMulticast = 2550136835, 
    KeepAliveValues = 2550136836, 
    AbsorbRouterAlert = 2550136837, 
    UnicastInterface = 2550136838, 
    LimitBroadcasts = 2550136839, 
    BindToInterface = 2550136840, 
    MulticastInterface = 2550136841, 
    AddMulticastGroupOnInterface = 2550136842, 
    DeleteMulticastGroupFromInterface = 2550136843, 
  }

  public enum IPProtectionLevel
  {
    Unspecified = -1, 
    Unrestricted = 10, 
    EdgeRestricted = 20, 
    Restricted = 30, 
  }

  public enum ProtocolFamily
  {
    Unknown = -1, 
    Unspecified = 0, 
    Unix = 1, 
    InterNetwork = 2, 
    ImpLink = 3, 
    Pup = 4, 
    Chaos = 5, 
    NS = 6, 
    Ipx = 6, 
    Iso = 7, 
    Osi = 7, 
    Ecma = 8, 
    DataKit = 9, 
    Ccitt = 10, 
    Sna = 11, 
    DecNet = 12, 
    DataLink = 13, 
    Lat = 14, 
    HyperChannel = 15, 
    AppleTalk = 16, 
    NetBios = 17, 
    VoiceView = 18, 
    FireFox = 19, 
    Banyan = 21, 
    Atm = 22, 
    InterNetworkV6 = 23, 
    Cluster = 24, 
    Ieee12844 = 25, 
    Irda = 26, 
    NetworkDesigners = 28, 
    Max = 29, 
  }

  public enum ProtocolType
  {
    IP = 0, 
    IPv6HopByHopOptions = 0, 
    Icmp = 1, 
    Igmp = 2, 
    Ggp = 3, 
    IPv4 = 4, 
    Tcp = 6, 
    Pup = 12, 
    Udp = 17, 
    Idp = 22, 
    IPv6 = 41, 
    IPv6RoutingHeader = 43, 
    IPv6FragmentHeader = 44, 
    IPSecEncapsulatingSecurityPayload = 50, 
    IPSecAuthenticationHeader = 51, 
    IcmpV6 = 58, 
    IPv6NoNextHeader = 59, 
    IPv6DestinationOptions = 60, 
    ND = 77, 
    Raw = 255, 
    Unspecified = 0, 
    Ipx = 1000, 
    Spx = 1256, 
    SpxII = 1257, 
    Unknown = -1, 
  }

  public enum SelectMode
  {
    SelectRead = 0, 
    SelectWrite = 1, 
    SelectError = 2, 
  }

  public enum SocketAsyncOperation
  {
    None = 0, 
    Accept = 1, 
    Connect = 2, 
    Disconnect = 3, 
    Receive = 4, 
    ReceiveFrom = 5, 
    ReceiveMessageFrom = 6, 
    Send = 7, 
    SendPackets = 8, 
    SendTo = 9, 
  }

  public enum SocketError
  {
    Success = 0, 
    SocketError = -1, 
    Interrupted = 10004, 
    AccessDenied = 10013, 
    Fault = 10014, 
    InvalidArgument = 10022, 
    TooManyOpenSockets = 10024, 
    WouldBlock = 10035, 
    InProgress = 10036, 
    AlreadyInProgress = 10037, 
    NotSocket = 10038, 
    DestinationAddressRequired = 10039, 
    MessageSize = 10040, 
    ProtocolType = 10041, 
    ProtocolOption = 10042, 
    ProtocolNotSupported = 10043, 
    SocketNotSupported = 10044, 
    OperationNotSupported = 10045, 
    ProtocolFamilyNotSupported = 10046, 
    AddressFamilyNotSupported = 10047, 
    AddressAlreadyInUse = 10048, 
    AddressNotAvailable = 10049, 
    NetworkDown = 10050, 
    NetworkUnreachable = 10051, 
    NetworkReset = 10052, 
    ConnectionAborted = 10053, 
    ConnectionReset = 10054, 
    NoBufferSpaceAvailable = 10055, 
    IsConnected = 10056, 
    NotConnected = 10057, 
    Shutdown = 10058, 
    TimedOut = 10060, 
    ConnectionRefused = 10061, 
    HostDown = 10064, 
    HostUnreachable = 10065, 
    ProcessLimit = 10067, 
    SystemNotReady = 10091, 
    VersionNotSupported = 10092, 
    NotInitialized = 10093, 
    Disconnecting = 10101, 
    TypeNotFound = 10109, 
    HostNotFound = 11001, 
    TryAgain = 11002, 
    NoRecovery = 11003, 
    NoData = 11004, 
    IOPending = 997, 
    OperationAborted = 995, 
  }

  public enum SocketFlags
  {
    None = 0, 
    OutOfBand = 1, 
    Peek = 2, 
    DontRoute = 4, 
    MaxIOVectorLength = 16, 
    Truncated = 256, 
    ControlDataTruncated = 512, 
    Broadcast = 1024, 
    Multicast = 2048, 
    Partial = 32768, 
  }

  public enum SocketInformationOptions
  {
    NonBlocking = 1, 
    Connected = 2, 
    Listening = 4, 
    UseOnlyOverlappedIO = 8, 
  }

  public enum SocketOptionLevel
  {
    Socket = 65535, 
    IP = 0, 
    IPv6 = 41, 
    Tcp = 6, 
    Udp = 17, 
  }

  public enum SocketOptionName
  {
    Debug = 1, 
    AcceptConnection = 2, 
    ReuseAddress = 4, 
    KeepAlive = 8, 
    DontRoute = 16, 
    Broadcast = 32, 
    UseLoopback = 64, 
    Linger = 128, 
    OutOfBandInline = 256, 
    DontLinger = -129, 
    ExclusiveAddressUse = -5, 
    SendBuffer = 4097, 
    ReceiveBuffer = 4098, 
    SendLowWater = 4099, 
    ReceiveLowWater = 4100, 
    SendTimeout = 4101, 
    ReceiveTimeout = 4102, 
    Error = 4103, 
    Type = 4104, 
    MaxConnections = 2147483647, 
    IPOptions = 1, 
    HeaderIncluded = 2, 
    TypeOfService = 3, 
    IpTimeToLive = 4, 
    MulticastInterface = 9, 
    MulticastTimeToLive = 10, 
    MulticastLoopback = 11, 
    AddMembership = 12, 
    DropMembership = 13, 
    DontFragment = 14, 
    AddSourceMembership = 15, 
    DropSourceMembership = 16, 
    BlockSource = 17, 
    UnblockSource = 18, 
    PacketInformation = 19, 
    HopLimit = 21, 
    IPProtectionLevel = 23, 
    IPv6Only = 27, 
    NoDelay = 1, 
    BsdUrgent = 2, 
    Expedited = 2, 
    NoChecksum = 1, 
    ChecksumCoverage = 20, 
    UpdateAcceptContext = 28683, 
    UpdateConnectContext = 28688, 
  }

  public enum SocketShutdown
  {
    Receive = 0, 
    Send = 1, 
    Both = 2, 
  }

  public enum SocketType
  {
    Stream = 1, 
    Dgram = 2, 
    Raw = 3, 
    Rdm = 4, 
    Seqpacket = 5, 
    Unknown = -1, 
  }

  public enum TransmitFileOptions
  {
    UseDefaultWorkerThread = 0, 
    Disconnect = 1, 
    ReuseSocket = 2, 
    WriteBehind = 4, 
    UseSystemThread = 16, 
    UseKernelApc = 32, 
  }
}

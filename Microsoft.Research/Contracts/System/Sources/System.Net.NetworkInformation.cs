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

// File System.Net.NetworkInformation.cs
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


namespace System.Net.NetworkInformation
{
  public enum DuplicateAddressDetectionState
  {
    Invalid = 0, 
    Tentative = 1, 
    Duplicate = 2, 
    Deprecated = 3, 
    Preferred = 4, 
  }

  public enum IPStatus
  {
    Success = 0, 
    DestinationNetworkUnreachable = 11002, 
    DestinationHostUnreachable = 11003, 
    DestinationProtocolUnreachable = 11004, 
    DestinationPortUnreachable = 11005, 
    DestinationProhibited = 11004, 
    NoResources = 11006, 
    BadOption = 11007, 
    HardwareError = 11008, 
    PacketTooBig = 11009, 
    TimedOut = 11010, 
    BadRoute = 11012, 
    TtlExpired = 11013, 
    TtlReassemblyTimeExceeded = 11014, 
    ParameterProblem = 11015, 
    SourceQuench = 11016, 
    BadDestination = 11018, 
    DestinationUnreachable = 11040, 
    TimeExceeded = 11041, 
    BadHeader = 11042, 
    UnrecognizedNextHeader = 11043, 
    IcmpError = 11044, 
    DestinationScopeMismatch = 11045, 
    Unknown = -1, 
  }

  public enum NetBiosNodeType
  {
    Unknown = 0, 
    Broadcast = 1, 
    Peer2Peer = 2, 
    Mixed = 4, 
    Hybrid = 8, 
  }

  public delegate void NetworkAddressChangedEventHandler(Object sender, EventArgs e);

  public delegate void NetworkAvailabilityChangedEventHandler(Object sender, NetworkAvailabilityEventArgs e);

  public enum NetworkInformationAccess
  {
    None = 0, 
    Read = 1, 
    Ping = 4, 
  }

  public enum NetworkInterfaceComponent
  {
    IPv4 = 0, 
    IPv6 = 1, 
  }

  public enum NetworkInterfaceType
  {
    Unknown = 1, 
    Ethernet = 6, 
    TokenRing = 9, 
    Fddi = 15, 
    BasicIsdn = 20, 
    PrimaryIsdn = 21, 
    Ppp = 23, 
    Loopback = 24, 
    Ethernet3Megabit = 26, 
    Slip = 28, 
    Atm = 37, 
    GenericModem = 48, 
    FastEthernetT = 62, 
    Isdn = 63, 
    FastEthernetFx = 69, 
    Wireless80211 = 71, 
    AsymmetricDsl = 94, 
    RateAdaptDsl = 95, 
    SymmetricDsl = 96, 
    VeryHighSpeedDsl = 97, 
    IPOverAtm = 114, 
    GigabitEthernet = 117, 
    Tunnel = 131, 
    MultiRateSymmetricDsl = 143, 
    HighPerformanceSerialBus = 144, 
  }

  public enum OperationalStatus
  {
    Up = 1, 
    Down = 2, 
    Testing = 3, 
    Unknown = 4, 
    Dormant = 5, 
    NotPresent = 6, 
    LowerLayerDown = 7, 
  }

  public delegate void PingCompletedEventHandler(Object sender, PingCompletedEventArgs e);

  public enum PrefixOrigin
  {
    Other = 0, 
    Manual = 1, 
    WellKnown = 2, 
    Dhcp = 3, 
    RouterAdvertisement = 4, 
  }

  public enum SuffixOrigin
  {
    Other = 0, 
    Manual = 1, 
    WellKnown = 2, 
    OriginDhcp = 3, 
    LinkLayerAddress = 4, 
    Random = 5, 
  }

  public enum TcpState
  {
    Unknown = 0, 
    Closed = 1, 
    Listen = 2, 
    SynSent = 3, 
    SynReceived = 4, 
    Established = 5, 
    FinWait1 = 6, 
    FinWait2 = 7, 
    CloseWait = 8, 
    Closing = 9, 
    LastAck = 10, 
    TimeWait = 11, 
    DeleteTcb = 12, 
  }
}

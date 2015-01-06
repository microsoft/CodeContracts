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

using System.Diagnostics.Contracts;
using System;

namespace System.Net.Sockets {
  // Summary:
  //     Specifies the addressing scheme that an instance of the System.Net.Sockets.Socket
  //     class can use.
  public enum AddressFamily {
    // Summary:
    //     Unknown address family.
    Unknown = -1,
    //
    // Summary:
    //     Unspecified address family.
    Unspecified = 0,
    //
    // Summary:
    //     Unix local to host address.
    Unix = 1,
    //
    // Summary:
    //     Address for IP version 4.
    InterNetwork = 2,
    //
    // Summary:
    //     ARPANET IMP address.
    ImpLink = 3,
    //
    // Summary:
    //     Address for PUP protocols.
    Pup = 4,
    //
    // Summary:
    //     Address for MIT CHAOS protocols.
    Chaos = 5,
    //
    // Summary:
    //     IPX or SPX address.
    Ipx = 6,
    //
    // Summary:
    //     Address for Xerox NS protocols.
    NS = 6,
    //
    // Summary:
    //     Address for ISO protocols.
    Osi = 7,
    //
    // Summary:
    //     Address for ISO protocols.
    Iso = 7,
    //
    // Summary:
    //     European Computer Manufacturers Association (ECMA) address.
    Ecma = 8,
    //
    // Summary:
    //     Address for Datakit protocols.
    DataKit = 9,
    //
    // Summary:
    //     Addresses for CCITT protocols, such as X.25.
    Ccitt = 10,
    //
    // Summary:
    //     IBM SNA address.
    Sna = 11,
    //
    // Summary:
    //     DECnet address.
    DecNet = 12,
    //
    // Summary:
    //     Direct data-link interface address.
    DataLink = 13,
    //
    // Summary:
    //     LAT address.
    Lat = 14,
    //
    // Summary:
    //     NSC Hyperchannel address.
    HyperChannel = 15,
    //
    // Summary:
    //     AppleTalk address.
    AppleTalk = 16,
    //
    // Summary:
    //     NetBios address.
    NetBios = 17,
    //
    // Summary:
    //     VoiceView address.
    VoiceView = 18,
    //
    // Summary:
    //     FireFox address.
    FireFox = 19,
    //
    // Summary:
    //     Banyan address.
    Banyan = 21,
    //
    // Summary:
    //     Native ATM services address.
    Atm = 22,
    //
    // Summary:
    //     Address for IP version 6.
    InterNetworkV6 = 23,
    //
    // Summary:
    //     Address for Microsoft cluster products.
    Cluster = 24,
    //
    // Summary:
    //     IEEE 1284.4 workgroup address.
    Ieee12844 = 25,
    //
    // Summary:
    //     IrDA address.
    Irda = 26,
    //
    // Summary:
    //     Address for Network Designers OSI gateway-enabled protocols.
    NetworkDesigners = 28,
    //
    // Summary:
    //     MAX address.
    Max = 29,
  }
}

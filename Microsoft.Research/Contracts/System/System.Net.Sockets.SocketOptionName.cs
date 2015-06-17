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
  //     Defines configuration option names.
  public enum SocketOptionName {
    // Summary:
    //     Close the socket gracefully without lingering.
    DontLinger = -129,
    //
    // Summary:
    //     Enables a socket to be bound for exclusive access.
    ExclusiveAddressUse = -5,
    //
    // Summary:
    //     Specifies the IP options to be inserted into outgoing datagrams.
    IPOptions = 1,
    //
    // Summary:
    //     Disables the Nagle algorithm for send coalescing.
    NoDelay = 1,
    //
    // Summary:
    //     Record debugging information.
    Debug = 1,
    //
    // Summary:
    //     Send UDP datagrams with checksum set to zero.
    NoChecksum = 1,
    //
    // Summary:
    //     Use expedited data as defined in RFC-1222. This option can be set only once;
    //     after it is set, it cannot be turned off.
    Expedited = 2,
    //
    // Summary:
    //     Indicates that the application provides the IP header for outgoing datagrams.
    HeaderIncluded = 2,
    //
    // Summary:
    //     Use urgent data as defined in RFC-1222. This option can be set only once;
    //     after it is set, it cannot be turned off.
    BsdUrgent = 2,
    //
    // Summary:
    //     The socket is listening.
    AcceptConnection = 2,
    //
    // Summary:
    //     Change the IP header type of the service field.
    TypeOfService = 3,
    //
    // Summary:
    //     Allows the socket to be bound to an address that is already in use.
    ReuseAddress = 4,
    //
    // Summary:
    //     Set the IP header Time-to-Live field.
    IpTimeToLive = 4,
    //
    // Summary:
    //     Use keep-alives.
    KeepAlive = 8,
    //
    // Summary:
    //     Set the interface for outgoing multicast packets.
    MulticastInterface = 9,
    //
    // Summary:
    //     An IP multicast Time to Live.
    MulticastTimeToLive = 10,
    //
    // Summary:
    //     An IP multicast loopback.
    MulticastLoopback = 11,
    //
    // Summary:
    //     Add an IP group membership.
    AddMembership = 12,
    //
    // Summary:
    //     Drop an IP group membership.
    DropMembership = 13,
    //
    // Summary:
    //     Do not fragment IP datagrams.
    DontFragment = 14,
    //
    // Summary:
    //     Join a source group.
    AddSourceMembership = 15,
    //
    // Summary:
    //     Drop a source group.
    DropSourceMembership = 16,
    //
    // Summary:
    //     Do not route; send the packet directly to the interface addresses.
    DontRoute = 16,
    //
    // Summary:
    //     Block data from a source.
    BlockSource = 17,
    //
    // Summary:
    //     Unblock a previously blocked source.
    UnblockSource = 18,
    //
    // Summary:
    //     Return information about received packets.
    PacketInformation = 19,
    //
    // Summary:
    //     Set or get the UDP checksum coverage.
    ChecksumCoverage = 20,
    //
    // Summary:
    //     Specifies the maximum number of router hops for an Internet Protocol version
    //     6 (IPv6) packet. This is similar to Time to Live (TTL) for Internet Protocol
    //     version 4.
    HopLimit = 21,
    //
    // Summary:
    //     Permit sending broadcast messages on the socket.
    Broadcast = 32,
    //
    // Summary:
    //     Bypass hardware when possible.
    UseLoopback = 64,
    //
    // Summary:
    //     Linger on close if unsent data is present.
    Linger = 128,
    //
    // Summary:
    //     Receives out-of-band data in the normal data stream.
    OutOfBandInline = 256,
    //
    // Summary:
    //     Specifies the total per-socket buffer space reserved for sends. This is unrelated
    //     to the maximum message size or the size of a TCP window.
    SendBuffer = 4097,
    //
    // Summary:
    //     Specifies the total per-socket buffer space reserved for receives. This is
    //     unrelated to the maximum message size or the size of a TCP window.
    ReceiveBuffer = 4098,
    //
    // Summary:
    //     Specifies the low water mark for Overload:System.Net.Sockets.Socket.Send
    //     operations.
    SendLowWater = 4099,
    //
    // Summary:
    //     Specifies the low water mark for Overload:System.Net.Sockets.Socket.Receive
    //     operations.
    ReceiveLowWater = 4100,
    //
    // Summary:
    //     Send a time-out. This option applies only to synchronous methods; it has
    //     no effect on asynchronous methods such as the System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)
    //     method.
    SendTimeout = 4101,
    //
    // Summary:
    //     Receive a time-out. This option applies only to synchronous methods; it has
    //     no effect on asynchronous methods such as the System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)
    //     method.
    ReceiveTimeout = 4102,
    //
    // Summary:
    //     Get the error status and clear.
    Error = 4103,
    //
    // Summary:
    //     Get the socket type.
    Type = 4104,
    //
    // Summary:
    //     Updates an accepted socket's properties by using those of an existing socket.
    //     This is equivalent to using the Winsock2 SO_UPDATE_ACCEPT_CONTEXT socket
    //     option and is supported only on connection-oriented sockets.
    UpdateAcceptContext = 28683,
    //
    // Summary:
    //     Updates a connected socket's properties by using those of an existing socket.
    //     This is equivalent to using the Winsock2 SO_UPDATE_CONNECT_CONTEXT socket
    //     option and is supported only on connection-oriented sockets.
    UpdateConnectContext = 28688,
    //
    // Summary:
    //     Not supported; will throw a System.Net.Sockets.SocketException if used.
    MaxConnections = 2147483647,
  }
}

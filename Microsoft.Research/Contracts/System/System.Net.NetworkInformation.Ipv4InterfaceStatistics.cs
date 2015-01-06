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


#if !SILVERLIGHT

using System;

namespace System.Net.NetworkInformation
{
  // Summary:
  //     Provides statistical data for a network interface on the local computer.
  public abstract class IPv4InterfaceStatistics
  {
    // Summary:
    //     Initializes a new instance of the System.Net.NetworkInformation.IPv4InterfaceStatistics
    //     class.
    extern protected IPv4InterfaceStatistics();

    // Summary:
    //     Gets the number of bytes that were received on the interface.
    //
    // Returns:
    //     An System.Int64 value that specifies the total number of bytes that were
    //     received on the interface.
    public abstract long BytesReceived { get; }
    //
    // Summary:
    //     Gets the number of bytes that were sent on the interface.
    //
    // Returns:
    //     An System.Int64 value that specifies the total number of bytes that were
    //     transmitted on the interface.
    public abstract long BytesSent { get; }
    //
    // Summary:
    //     Gets the number of incoming packets that were discarded.
    //
    // Returns:
    //     An System.Int64 value that specifies the total number of discarded incoming
    //     packets.
    public abstract long IncomingPacketsDiscarded { get; }
    //
    // Summary:
    //     Gets the number of incoming packets with errors.
    //
    // Returns:
    //     An System.Int64 value that specifies the total number of incoming packets
    //     with errors.
    public abstract long IncomingPacketsWithErrors { get; }
    //
    // Summary:
    //     Gets the number of incoming packets with an unknown protocol.
    //
    // Returns:
    //     An System.Int64 value that specifies the total number of incoming packets
    //     with an unknown protocol.
    public abstract long IncomingUnknownProtocolPackets { get; }
    //
    // Summary:
    //     Gets the number of non-unicast packets that were received on the interface.
    //
    // Returns:
    //     An System.Int64 value that specifies the total number of non-unicast packets
    //     that were received on the interface.
    public abstract long NonUnicastPacketsReceived { get; }
    //
    // Summary:
    //     Gets the number of non-unicast packets that were sent on the interface.
    //
    // Returns:
    //     An System.Int64 value that specifies the total number of non-unicast packets
    //     that were sent on the interface.
    public abstract long NonUnicastPacketsSent { get; }
    //
    // Summary:
    //     Gets the number of outgoing packets that were discarded.
    //
    // Returns:
    //     An System.Int64 value that specifies the total number of discarded outgoing
    //     packets.
    public abstract long OutgoingPacketsDiscarded { get; }
    //
    // Summary:
    //     Gets the number of outgoing packets with errors.
    //
    // Returns:
    //     An System.Int64 value that specifies the total number of outgoing packets
    //     with errors.
    public abstract long OutgoingPacketsWithErrors { get; }
    //
    // Summary:
    //     Gets the length of the output queue.
    //
    // Returns:
    //     An System.Int64 value that specifies the total number of packets in the output
    //     queue.
    public abstract long OutputQueueLength { get; }
    //
    // Summary:
    //     Gets the number of unicast packets that were received on the interface.
    //
    // Returns:
    //     An System.Int64 value that specifies the total number of unicast packets
    //     that were received on the interface.
    public abstract long UnicastPacketsReceived { get; }
    //
    // Summary:
    //     Gets the number of unicast packets that were sent on the interface.
    //
    // Returns:
    //     An System.Int64 value that specifies the total number of unicast packets
    //     that were sent on the interface.
    public abstract long UnicastPacketsSent { get; }
  }
}

#endif
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
  //     Provides information about network interfaces that support Internet Protocol
  //     version 4 (IPv4).
  public abstract class IPv4InterfaceProperties
  {
    // Summary:
    //     Initializes a new instance of the System.Net.NetworkInformation.IPv4InterfaceProperties
    //     class.
    extern protected IPv4InterfaceProperties();

    // Summary:
    //     Gets the interface index for the Internet Protocol version 4 (IPv4) address.
    //
    // Returns:
    //     An System.Int64 that contains the index of the IPv4 interface.
    public abstract int Index { get; }
    //
    // Summary:
    //     Gets a System.Boolean value that indicates whether this interface has an
    //     automatic private IP addressing (APIPA) address.
    //
    // Returns:
    //     true if the interface uses an APIPA address; otherwise, false.
    public abstract bool IsAutomaticPrivateAddressingActive { get; }
    //
    // Summary:
    //     Gets a System.Boolean value that indicates whether this interface has automatic
    //     private IP addressing (APIPA) enabled.
    //
    // Returns:
    //     true if the interface uses APIPA; otherwise, false.
    public abstract bool IsAutomaticPrivateAddressingEnabled { get; }
    //
    // Summary:
    //     Gets a System.Boolean value that indicates whether the interface is configured
    //     to use a Dynamic Host Configuration Protocol (DHCP) server to obtain an IP
    //     address.
    //
    // Returns:
    //     true if the interface is configured to obtain an IP address from a DHCP server;
    //     otherwise, false.
    public abstract bool IsDhcpEnabled { get; }
    //
    // Summary:
    //     Gets a System.Boolean value that indicates whether this interface can forward
    //     (route) packets.
    //
    // Returns:
    //     true if this interface routes packets; otherwise false.
    public abstract bool IsForwardingEnabled { get; }
    //
    // Summary:
    //     Gets the maximum transmission unit (MTU) for this network interface.
    //
    // Returns:
    //     An System.Int64 value that specifies the MTU.
    public abstract int Mtu { get; }
    //
    // Summary:
    //     Gets a System.Boolean value that indicates whether an interface uses Windows
    //     Internet Name Service (WINS).
    //
    // Returns:
    //     true if the interface uses WINS; otherwise, false.
    public abstract bool UsesWins { get; }
  }
}

#endif
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
  //     version 4 (IPv4) or Internet Protocol version 6 (IPv6).
  public abstract class IPInterfaceProperties
  {
    // Summary:
    //     Initializes a new instance of the System.Net.NetworkInformation.IPInterfaceProperties
    //     class.
    extern protected IPInterfaceProperties();

    // Summary:
    //     Gets the anycast IP addresses assigned to this interface.
    //
    // Returns:
    //     An System.Net.NetworkInformation.IPAddressInformationCollection that contains
    //     the anycast addresses for this interface.
    public abstract IPAddressInformationCollection AnycastAddresses { get; }
    //
    // Summary:
    //     Gets the addresses of Dynamic Host Configuration Protocol (DHCP) servers
    //     for this interface.
    //
    // Returns:
    //     An System.Net.NetworkInformation.IPAddressCollection that contains the address
    //     information for DHCP servers, or an empty array if no servers are found.
    public abstract IPAddressCollection DhcpServerAddresses { get; }
    //
    // Summary:
    //     Gets the addresses of Domain Name System (DNS) servers for this interface.
    //
    // Returns:
    //     A System.Net.NetworkInformation.IPAddressInformationCollection that contains
    //     the DNS server addresses.
    public abstract IPAddressCollection DnsAddresses { get; }
    //
    // Summary:
    //     Gets the Domain Name System (DNS) suffix associated with this interface.
    //
    // Returns:
    //     A System.String that contains the DNS suffix for this interface, or System.String.Empty
    //     if there is no DNS suffix for the interface.
    //
    // Exceptions:
    //   System.PlatformNotSupportedException:
    //     This property is not valid on computers running operating systems earlier
    //     than Windows 2000.
    public abstract string DnsSuffix { get; }
    //
    // Summary:
    //     Gets the network gateway addresses for this interface.
    //
    // Returns:
    //     An System.Net.NetworkInformation.IPAddressCollection that contains the address
    //     information for network gateways, or an empty array if no gateways are found.
    public abstract GatewayIPAddressInformationCollection GatewayAddresses { get; }
    //
    // Summary:
    //     Gets a System.Boolean value that indicates whether NetBt is configured to
    //     use DNS name resolution on this interface.
    //
    // Returns:
    //     true if NetBt is configured to use DNS name resolution on this interface;
    //     otherwise, false.
    public abstract bool IsDnsEnabled { get; }
    //
    // Summary:
    //     Gets a System.Boolean value that indicates whether this interface is configured
    //     to automatically register its IP address information with the Domain Name
    //     System (DNS).
    //
    // Returns:
    //     true if this interface is configured to automatically register a mapping
    //     between its dynamic IP address and static domain names; otherwise, false.
    public abstract bool IsDynamicDnsEnabled { get; }
    //
    // Summary:
    //     Gets the multicast addresses assigned to this interface.
    //
    // Returns:
    //     An System.Net.NetworkInformation.IPAddressInformationCollection that contains
    //     the multicast addresses for this interface.
    public abstract MulticastIPAddressInformationCollection MulticastAddresses { get; }
    //
    // Summary:
    //     Gets the unicast addresses assigned to this interface.
    //
    // Returns:
    //     An System.Net.NetworkInformation.UnicastIPAddressInformationCollection that
    //     contains the unicast addresses for this interface.
    public abstract UnicastIPAddressInformationCollection UnicastAddresses { get; }
    //
    // Summary:
    //     Gets the addresses of Windows Internet Name Service (WINS) servers.
    //
    // Returns:
    //     An System.Net.NetworkInformation.IPAddressCollection that contains the address
    //     information for WINS servers, or an empty array if no servers are found.
    public abstract IPAddressCollection WinsServersAddresses { get; }

    // Summary:
    //     Provides Internet Protocol version 4 (IPv4) configuration data for this network
    //     interface.
    //
    // Returns:
    //     An System.Net.NetworkInformation.IPv4InterfaceProperties object that contains
    //     IPv4 configuration data, or null if no data is available for the interface.
    //
    // Exceptions:
    //   System.Net.NetworkInformation.NetworkInformationException:
    //     The interface does not support the IPv4 protocol.
    public abstract IPv4InterfaceProperties GetIPv4Properties();
    //
    // Summary:
    //     Provides Internet Protocol version 6 (IPv6) configuration data for this network
    //     interface.
    //
    // Returns:
    //     An System.Net.NetworkInformation.IPv6InterfaceProperties object that contains
    //     IPv6 configuration data.
    //
    // Exceptions:
    //   System.Net.NetworkInformation.NetworkInformationException:
    //     The interface does not support the IPv6 protocol.
    public abstract IPv6InterfaceProperties GetIPv6Properties();
  }
}

#endif
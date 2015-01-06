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
using System.Net.Sockets;
using System.Diagnostics.Contracts;

namespace System.Net.Sockets
{
  // Summary:
  //     Specifies the addressing scheme that an instance of the System.Net.Sockets.Socket
  //     class can use.
  public enum AddressFamily
  {
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
    //     Address for OSI protocols.
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

namespace System.Net
{

  // Summary:
  //     Provides an Internet Protocol (IP) address.
  public class IPAddress
  {
    // Summary:
    //     Provides an IP address that indicates that the server must listen for client
    //     activity on all network interfaces. This field is read-only.
    public static readonly IPAddress Any;
    //
    // Summary:
    //     Provides the IP broadcast address. This field is read-only.
    public static readonly IPAddress Broadcast;
    //
    // Summary:
    //     The System.Net.Sockets.Socket.Bind(System.Net.EndPoint) method uses the System.Net.IPAddress.IPv6Any
    //     field to indicate that a System.Net.Sockets.Socket must listen for client
    //     activity on all network interfaces.
    public static readonly IPAddress IPv6Any;
    //
    // Summary:
    //     Provides the IP loopback address. This property is read-only.
    public static readonly IPAddress IPv6Loopback;
    //
    // Summary:
    //     Provides an IP address that indicates that no network interface should be
    //     used. This property is read-only.
    public static readonly IPAddress IPv6None;
    //
    // Summary:
    //     Provides the IP loopback address. This field is read-only.
    public static readonly IPAddress Loopback;
    //
    // Summary:
    //     Provides an IP address that indicates that no network interface should be
    //     used. This field is read-only.
    public static readonly IPAddress None;

    // Summary:
    //     Initializes a new instance of the System.Net.IPAddress class with the address
    //     specified as a System.Byte array.
    //
    // Parameters:
    //   address:
    //     The byte array value of the IP address.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     address is null.
    public IPAddress(byte[] address)
    {
      Contract.Requires(address != null);
    }

    //
    // Summary:
    //     Initializes a new instance of the System.Net.IPAddress class with the address
    //     specified as an System.Int64.
    //
    // Parameters:
    //   newAddress:
    //     The long value of the IP address. For example, the value 0x2414188f in big-endian
    //     format would be the IP address "143.24.20.36".
    public IPAddress(long newAddress)
    {
      Contract.Requires(newAddress >= 0);
      Contract.Requires(newAddress <= 0xffffffffL);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Net.IPAddress class with the address
    //     specified as a System.Byte array and the specified scope identifier.
    //
    // Parameters:
    //   address:
    //     The byte array value of the IP address.
    //
    //   scopeid:
    //     The long value of the scope identifier.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     address is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     scopeid < 0 or scopeid > 0x00000000FFFFFFFF
    public IPAddress(byte[] address, long scopeid)
    {
      Contract.Requires(address != null);
      Contract.Requires(scopeid >= 0);
      Contract.Requires(scopeid <= 0x00000000FFFFFFFF);
    }

    // Summary:
    //     An Internet Protocol (IP) address.
    //
    // Returns:
    //     The long value of the IP address.
    [Obsolete("This property has been deprecated. It is address family dependent. Please use IPAddress.Equals method to perform comparisons. http://go.microsoft.com/fwlink/?linkid=14202")]
    extern public long Address { get; set; }
    //
    // Summary:
    //     Gets the address family of the IP address.
    //
    // Returns:
    //     Returns System.Net.Sockets.AddressFamily.InterNetwork for IPv4 or System.Net.Sockets.AddressFamily.InterNetworkV6
    //     for IPv6.
    //extern public AddressFamily AddressFamily { get; }
    //
    // Summary:
    //     Gets whether the address is an IPv6 link local address.
    //
    // Returns:
    //     true if the IP address is an IPv6 link local address; otherwise, false.
    extern public bool IsIPv6LinkLocal { get; }
    //
    // Summary:
    //     Gets whether the address is an IPv6 multicast global address.
    //
    // Returns:
    //     true if the IP address is an IPv6 multicast global address; otherwise, false.
    extern public bool IsIPv6Multicast { get; }
    //
    // Summary:
    //     Gets whether the address is an IPv6 site local address.
    //
    // Returns:
    //     true if the IP address is an IPv6 site local address; otherwise, false.
    extern public bool IsIPv6SiteLocal { get; }
    //
    // Summary:
    //     Gets or sets the IPv6 address scope identifier.
    //
    // Returns:
    //     A long integer that specifies the scope of the address.
    //
    // Exceptions:
    //   System.Net.Sockets.SocketException:
    //     AddressFamily = InterNetwork.
    //
    //   System.ArgumentOutOfRangeException:
    //     scopeId < 0- or -scopeId > 0x00000000FFFFFFFF
    public long ScopeId 
    {
      get
      {
        return default(long);
      }
      set
      {
        Contract.Requires(value >= 0L);
        Contract.Requires(value <= 0x00000000FFFFFFFF);
      }
    }

    //
    // Summary:
    //     Provides a copy of the System.Net.IPAddress as an array of bytes.
    //
    // Returns:
    //     A System.Byte array.
    [Pure]
    public byte[] GetAddressBytes()
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);
      Contract.Ensures(
        Contract.Result<byte[]>().Length == 4 || Contract.Result<byte[]>().Length == 16);

      return default(byte[]);
    }
    //
    // Summary:
    //     Converts an integer value from host byte order to network byte order.
    //
    // Parameters:
    //   host:
    //     The number to convert, expressed in host byte order.
    //
    // Returns:
    //     An integer value, expressed in network byte order.
    extern public static int HostToNetworkOrder(int host);
    //
    // Summary:
    //     Converts a long value from host byte order to network byte order.
    //
    // Parameters:
    //   host:
    //     The number to convert, expressed in host byte order.
    //
    // Returns:
    //     A long value, expressed in network byte order.
    extern public static long HostToNetworkOrder(long host);
    //
    // Summary:
    //     Converts a short value from host byte order to network byte order.
    //
    // Parameters:
    //   host:
    //     The number to convert, expressed in host byte order.
    //
    // Returns:
    //     A short value, expressed in network byte order.
    extern public static short HostToNetworkOrder(short host);
    //
    // Summary:
    //     Indicates whether the specified IP address is the loopback address.
    //
    // Parameters:
    //   address:
    //     An IP address.
    //
    // Returns:
    //     true if address is the loopback address; otherwise, false.
    extern public static bool IsLoopback(IPAddress address);
    //
    // Summary:
    //     Converts an integer value from network byte order to host byte order.
    //
    // Parameters:
    //   network:
    //     The number to convert, expressed in network byte order.
    //
    // Returns:
    //     An integer value, expressed in host byte order.
    extern public static int NetworkToHostOrder(int network);
    //
    // Summary:
    //     Converts a long value from network byte order to host byte order.
    //
    // Parameters:
    //   network:
    //     The number to convert, expressed in network byte order.
    //
    // Returns:
    //     A long value, expressed in host byte order.
    extern public static long NetworkToHostOrder(long network);
    //
    // Summary:
    //     Converts a short value from network byte order to host byte order.
    //
    // Parameters:
    //   network:
    //     The number to convert, expressed in network byte order.
    //
    // Returns:
    //     A short value, expressed in host byte order.
    extern public static short NetworkToHostOrder(short network);
    //
    // Summary:
    //     Converts an IP address string to an System.Net.IPAddress instance.
    //
    // Parameters:
    //   ipString:
    //     A string that contains an IP address in dotted-quad notation for IPv4 and
    //     in colon-hexadecimal notation for IPv6.
    //
    // Returns:
    //     An System.Net.IPAddress instance.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     ipString is null.
    //
    //   System.FormatException:
    //     ipString is not a valid IP address.
    extern public static IPAddress Parse(string ipString);
    //
    // Summary:
    //     Determines whether a string is a valid IP address.
    //
    // Parameters:
    //   ipString:
    //     The string to validate.
    //
    //   address:
    //     The System.Net.IPAddress version of the string.
    //
    // Returns:
    //     true if ipString is a valid IP address; otherwise, false.
    public static bool TryParse(string ipString, out IPAddress address)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out address) != null);

      address = default(IPAddress);
      return default(bool);
    }
  }
}

#endif
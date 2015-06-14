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
  //     Specifies how an IP address network prefix was located.
  public enum PrefixOrigin
  {
    // Summary:
    //     The prefix was located using an unspecified source.
    Other = 0,
    //
    // Summary:
    //     The prefix was manually configured.
    Manual = 1,
    //
    // Summary:
    //     The prefix is a well-known prefix. Well-known prefixes are specified in standard-track
    //     Request for Comments (RFC) documents and assigned by the Internet Assigned
    //     Numbers Authority (Iana) or an address registry. Such prefixes are reserved
    //     for special purposes.
    WellKnown = 2,
    //
    // Summary:
    //     The prefix was supplied by a Dynamic Host Configuration Protocol (DHCP) server.
    Dhcp = 3,
    //
    // Summary:
    //     The prefix was supplied by a router advertisement.
    RouterAdvertisement = 4,
  }

  // Summary:
  //     Specifies the current state of an IP address.
  public enum DuplicateAddressDetectionState
  {
    // Summary:
    //     The address is not valid. A nonvalid address is expired and no longer assigned
    //     to an interface; applications should not send data packets to it.
    Invalid = 0,
    //
    // Summary:
    //     The duplicate address detection procedure's evaluation of the address has
    //     not completed successfully. Applications should not use the address because
    //     it is not yet valid and packets sent to it are discarded.
    Tentative = 1,
    //
    // Summary:
    //     The address is not unique. This address should not be assigned to the network
    //     interface.
    Duplicate = 2,
    //
    // Summary:
    //     The address is valid, but it is nearing its lease lifetime and should not
    //     be used by applications.
    Deprecated = 3,
    //
    // Summary:
    //     The address is valid and its use is unrestricted.
    Preferred = 4,
  }

  // Summary:
  //     Provides information about a network interface's multicast address.
  public abstract class MulticastIPAddressInformation : IPAddressInformation
  {
    // Summary:
    //     Initializes a new instance of the System.Net.NetworkInformation.MulticastIPAddressInformation
    //     class.
    extern protected MulticastIPAddressInformation();

    // Summary:
    //     Gets the number of seconds remaining during which this address is the preferred
    //     address.
    //
    // Returns:
    //     An System.Int64 value that specifies the number of seconds left for this
    //     address to remain preferred.
    //
    // Exceptions:
    //   System.PlatformNotSupportedException:
    //     This property is not valid on computers running operating systems earlier
    //     than Windows XP.
    public abstract long AddressPreferredLifetime { get; }
    //
    // Summary:
    //     Gets the number of seconds remaining during which this address is valid.
    //
    // Returns:
    //     An System.Int64 value that specifies the number of seconds left for this
    //     address to remain assigned.
    //
    // Exceptions:
    //   System.PlatformNotSupportedException:
    //     This property is not valid on computers running operating systems earlier
    //     than Windows XP.
    public abstract long AddressValidLifetime { get; }
    //
    // Summary:
    //     Specifies the amount of time remaining on the Dynamic Host Configuration
    //     Protocol (DHCP) lease for this IP address.
    //
    // Returns:
    //     An System.Int64 value that contains the number of seconds remaining before
    //     the computer must release the System.Net.IPAddress instance.
    public abstract long DhcpLeaseLifetime { get; }
    //
    // Summary:
    //     Gets a value that indicates the state of the duplicate address detection
    //     algorithm.
    //
    // Returns:
    //     One of the System.Net.NetworkInformation.DuplicateAddressDetectionState values
    //     that indicates the progress of the algorithm in determining the uniqueness
    //     of this IP address.
    //
    // Exceptions:
    //   System.PlatformNotSupportedException:
    //     This property is not valid on computers running operating systems earlier
    //     than Windows XP.
    public abstract DuplicateAddressDetectionState DuplicateAddressDetectionState { get; }
    //
    // Summary:
    //     Gets a value that identifies the source of a Multicast Internet Protocol
    //     (IP) address prefix.
    //
    // Returns:
    //     One of the System.Net.NetworkInformation.PrefixOrigin values that identifies
    //     how the prefix information was obtained.
    //
    // Exceptions:
    //   System.PlatformNotSupportedException:
    //     This property is not valid on computers running operating systems earlier
    //     than Windows XP.
    public abstract PrefixOrigin PrefixOrigin { get; }
    //
    // Summary:
    //     Gets a value that identifies the source of a Multicast Internet Protocol
    //     (IP) address suffix.
    //
    // Returns:
    //     One of the System.Net.NetworkInformation.SuffixOrigin values that identifies
    //     how the suffix information was obtained.
    //
    // Exceptions:
    //   System.PlatformNotSupportedException:
    //     This property is not valid on computers running operating systems earlier
    //     than Windows XP.
    public abstract SuffixOrigin SuffixOrigin { get; }
  }
}

#endif
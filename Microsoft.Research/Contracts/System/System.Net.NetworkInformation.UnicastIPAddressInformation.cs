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
using System.Net;

namespace System.Net.NetworkInformation
{
  // Summary:
  //     Specifies how an IP address host suffix was located.
  public enum SuffixOrigin
  {
    // Summary:
    //     The suffix was located using an unspecified source.
    Other = 0,
    //
    // Summary:
    //     The suffix was manually configured.
    Manual = 1,
    //
    // Summary:
    //     The suffix is a well-known suffix. Well-known suffixes are specified in standard-track
    //     Request for Comments (RFC) documents and assigned by the Internet Assigned
    //     Numbers Authority (Iana) or an address registry. Such suffixes are reserved
    //     for special purposes.
    WellKnown = 2,
    //
    // Summary:
    //     The suffix was supplied by a Dynamic Host Configuration Protocol (DHCP) server.
    OriginDhcp = 3,
    //
    // Summary:
    //     The suffix is a link-local suffix.
    LinkLayerAddress = 4,
    //
    // Summary:
    //     The suffix was randomly assigned.
    Random = 5,
  }

  // Summary:
  //     Provides information about a network interface's unicast address.
  public abstract class UnicastIPAddressInformation : IPAddressInformation
  {
    // Summary:
    //     Initializes a new instance of the System.Net.NetworkInformation.UnicastIPAddressInformation
    //     class.
    extern protected UnicastIPAddressInformation();

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
    //     Gets the IPv4 mask.
    //
    // Returns:
    //     An System.Net.IPAddress object that contains the IPv4 mask.
    public abstract IPAddress IPv4Mask { get; }
    //
    // Summary:
    //     Gets a value that identifies the source of a unicast Internet Protocol (IP)
    //     address prefix.
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
    //     Gets a value that identifies the source of a unicast Internet Protocol (IP)
    //     address suffix.
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
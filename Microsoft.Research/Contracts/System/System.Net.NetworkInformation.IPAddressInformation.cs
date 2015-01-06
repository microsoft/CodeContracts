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
  //     Provides information about a network interface address.
  public abstract class IPAddressInformation
  {
    // Summary:
    //     Initializes a new instance of the System.Net.NetworkInformation.IPAddressInformation
    //     class.
    extern protected IPAddressInformation();

    // Summary:
    //     Gets the Internet Protocol (IP) address.
    //
    // Returns:
    //     An System.Net.IPAddress instance that contains the IP address of an interface.
    public abstract IPAddress Address { get; }
    //
    // Summary:
    //     Gets a System.Boolean value that indicates whether the Internet Protocol
    //     (IP) address is valid to appear in a Domain Name System (DNS) server database.
    //
    // Returns:
    //     true if the address can appear in a DNS database; otherwise, false.
    public abstract bool IsDnsEligible { get; }
    //
    // Summary:
    //     Gets a System.Boolean value that indicates whether the Internet Protocol
    //     (IP) address is transient (a cluster address).
    //
    // Returns:
    //     true if the address is transient; otherwise, false.
    public abstract bool IsTransient { get; }
  }
}

#endif
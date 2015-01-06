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
  //     Provides the Media Access Control (MAC) address for a network interface (adapter).
  public class PhysicalAddress
  {
    // Summary:
    //     Returns a new System.Net.NetworkInformation.PhysicalAddress instance with
    //     a zero length address. This field is read-only.
    public static readonly PhysicalAddress None;

    // Summary:
    //     Initializes a new instance of the System.Net.NetworkInformation.PhysicalAddress
    //     class.
    //
    // Parameters:
    //   address:
    //     A System.Byte array containing the address.
    extern public PhysicalAddress(byte[] address);

    //
    // Summary:
    //     Returns the address of the current instance.
    //
    // Returns:
    //     A System.Byte array containing the address.
    extern public byte[] GetAddressBytes();
    //
    // Summary:
    //     Parses the specified System.String and stores its contents as the address
    //     bytes of the System.Net.NetworkInformation.PhysicalAddress returned by this
    //     method.
    //
    // Parameters:
    //   address:
    //     A System.String containing the address that will be used to initialize the
    //     System.Net.NetworkInformation.PhysicalAddress instance returned by this method.
    //
    // Returns:
    //     A System.Net.NetworkInformation.PhysicalAddress instance with the specified
    //     address.
    extern public static PhysicalAddress Parse(string address);
  }
}

#endif
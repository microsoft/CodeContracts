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


namespace System.Net
{
  // Summary:
  //     Provides a container class for Internet host address information.
  public class IPHostEntry
  {
    // Summary:
    //     Initializes a new instance of the System.Net.IPHostEntry class.
    public IPHostEntry() { }

    // Summary:
    //     Gets or sets a list of IP addresses that are associated with a host.
    //
    // Returns:
    //     An array of type System.Net.IPAddress that contains IP addresses that resolve
    //     to the host names that are contained in the System.Net.IPHostEntry.Aliases
    //     property.
    public IPAddress[] AddressList { get; set; }
    //
    // Summary:
    //     Gets or sets a list of aliases that are associated with a host.
    //
    // Returns:
    //     An array of strings that contain DNS names that resolve to the IP addresses
    //     in the System.Net.IPHostEntry.AddressList property.
    public string[] Aliases { get; set; }
    //
    // Summary:
    //     Gets or sets the DNS name of the host.
    //
    // Returns:
    //     A string that contains the primary host name for the server.
    public string HostName { get; set; }
  }
}

#endif
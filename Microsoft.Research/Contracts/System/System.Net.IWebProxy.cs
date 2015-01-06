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

namespace System.Net
{
  // Summary:
  //     Provides the base interface for implementation of proxy access for the System.Net.WebRequest
  //     class.
  public interface IWebProxy
  {
    // Summary:
    //     The credentials to submit to the proxy server for authentication.
    //
    // Returns:
    //     An System.Net.ICredentials instance that contains the credentials that are
    //     needed to authenticate a request to the proxy server.
    //ICredentials Credentials { get; set; }

    // Summary:
    //     Returns the URI of a proxy.
    //
    // Parameters:
    //   destination:
    //     A System.Uri that specifies the requested Internet resource.
    //
    // Returns:
    //     A System.Uri instance that contains the URI of the proxy used to contact
    //     destination.
    //Uri GetProxy(Uri destination);
    //
    // Summary:
    //     Indicates that the proxy should not be used for the specified host.
    //
    // Parameters:
    //   host:
    //     The System.Uri of the host to check for proxy use.
    //
    // Returns:
    //     true if the proxy server should not be used for host; otherwise, false.
    //bool IsBypassed(Uri host);
  }
}
#endif
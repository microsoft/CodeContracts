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

#region Assembly System.dll, v4.0.30319
// C:\Program Files\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.dll
#endregion

using System;

namespace System.Net {
  // Summary:
  //     Provides the interface for retrieving credentials for a host, port, and authentication
  //     type.
  public interface ICredentialsByHost {
    // Summary:
    //     Returns the credential for the specified host, port, and authentication protocol.
    //
    // Parameters:
    //   host:
    //     The host computer that is authenticating the client.
    //
    //   port:
    //     The port on host that the client will communicate with.
    //
    //   authenticationType:
    //     The authentication protocol.
    //
    // Returns:
    //     A System.Net.NetworkCredential for the specified host, port, and authentication
    //     protocol, or null if there are no credentials available for the specified
    //     host, port, and authentication protocol.
    NetworkCredential GetCredential(string host, int port, string authenticationType);
  }
}

#endif
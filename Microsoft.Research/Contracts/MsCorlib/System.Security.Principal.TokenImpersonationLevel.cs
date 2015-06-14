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

using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  // Summary:
  //     Defines security impersonation levels. Security impersonation levels govern
  //     the degree to which a server process can act on behalf of a client process.
  public enum TokenImpersonationLevel
  {
    // Summary:
    //     An impersonation level is not assigned.
    None = 0,
    //
    // Summary:
    //     The server process cannot obtain identification information about the client,
    //     and it cannot impersonate the client.
    Anonymous = 1,
    //
    // Summary:
    //     The server process can obtain information about the client, such as security
    //     identifiers and privileges, but it cannot impersonate the client. This is
    //     useful for servers that export their own objects, for example, database products
    //     that export tables and views. Using the retrieved client-security information,
    //     the server can make access-validation decisions without being able to use
    //     other services that are using the client's security context.
    Identification = 2,
    //
    // Summary:
    //     The server process can impersonate the client's security context on its local
    //     system. The server cannot impersonate the client on remote systems.
    Impersonation = 3,
    //
    // Summary:
    //     The server process can impersonate the client's security context on remote
    //     systems.
    Delegation = 4,
  }
}

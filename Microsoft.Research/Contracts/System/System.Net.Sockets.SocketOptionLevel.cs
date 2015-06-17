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

using System.Diagnostics.Contracts;
using System;

namespace System.Net.Sockets {
  // Summary:
  //     Defines socket option levels for the System.Net.Sockets.Socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel,System.Net.Sockets.SocketOptionName,System.Int32)
  //     and System.Net.Sockets.Socket.GetSocketOption(System.Net.Sockets.SocketOptionLevel,System.Net.Sockets.SocketOptionName)
  //     methods.
  public enum SocketOptionLevel {
    // Summary:
    //     System.Net.Sockets.Socket options apply only to IP sockets.
    IP = 0,
    //
    // Summary:
    //     System.Net.Sockets.Socket options apply only to TCP sockets.
    Tcp = 6,
    //
    // Summary:
    //     System.Net.Sockets.Socket options apply only to UDP sockets.
    Udp = 17,
    //
    // Summary:
    //     System.Net.Sockets.Socket options apply only to IPv6 sockets.
    IPv6 = 41,
    //
    // Summary:
    //     System.Net.Sockets.Socket options apply to all sockets.
    Socket = 65535,
  }
}

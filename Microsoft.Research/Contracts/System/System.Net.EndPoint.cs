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

namespace System.Net
{
  // Summary:
  //     Identifies a network address. This is an abstract class.
  //[Serializable]
  public abstract class EndPoint
  {   
    // Summary:
    //     Gets the address family to which the endpoint belongs.
    //
    // Returns:
    //     One of the System.Net.Sockets.AddressFamily values.
    //
    // Exceptions:
    //   System.NotImplementedException:
    //     Any attempt is made to get or set the property when the property is not overridden
    //     in a descendant class.
    extern public virtual AddressFamily AddressFamily { get; }

    // Summary:
    //     Creates an System.Net.EndPoint instance from a System.Net.SocketAddress instance.
    //
    // Parameters:
    //   socketAddress:
    //     The socket address that serves as the endpoint for a connection.
    //
    // Returns:
    //     A new System.Net.EndPoint instance that is initialized from the specified
    //     System.Net.SocketAddress instance.
    //
    // Exceptions:
    //   System.NotImplementedException:
    //     Any attempt is made to access the method when the method is not overridden
    //     in a descendant class.
    //extern public virtual EndPoint Create(SocketAddress socketAddress);
    //
    // Summary:
    //     Serializes endpoint information into a System.Net.SocketAddress instance.
    //
    // Returns:
    //     A System.Net.SocketAddress instance that contains the endpoint information.
    //
    // Exceptions:
    //   System.NotImplementedException:
    //     Any attempt is made to access the method when the method is not overridden
    //     in a descendant class.
    extern public virtual SocketAddress Serialize();
  }
}

#endif
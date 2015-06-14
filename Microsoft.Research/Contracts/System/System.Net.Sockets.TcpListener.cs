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

#if !SILVERLIGHT

namespace System.Net.Sockets
{

  public class TcpListener
  {

    public TcpListener(int port) { }

    public TcpListener(IPEndPoint localEP)
    {
      Contract.Requires(localEP != null);
    }

    public TcpListener(IPAddress localaddr, int port)
    {
      Contract.Requires(localaddr != null);
    }

    extern public bool ExclusiveAddressUse { get; set; }
    //
    // Summary:
    //     Gets the underlying System.Net.EndPoint of the current System.Net.Sockets.TcpListener.
    //
    // Returns:
    //     The System.Net.EndPoint to which the System.Net.Sockets.Socket is bound.
    extern public EndPoint LocalEndpoint { get; }
    //
    // Summary:
    //     Gets the underlying network System.Net.Sockets.Socket.
    //
    // Returns:
    //     The underlying System.Net.Sockets.Socket.
    extern public Socket Server { get; }

    // Summary:
    //     Accepts a pending connection request.
    //
    // Returns:
    //     A System.Net.Sockets.Socket used to send and receive data.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The listener has not been started with a call to System.Net.Sockets.TcpListener.Start().
    public Socket AcceptSocket()
    {
      Contract.Ensures(Contract.Result<Socket>() != null);
      return null;
    }
//    public Task<Socket> AcceptSocketAsync();
    //
    // Summary:
    //     Accepts a pending connection request
    //
    // Returns:
    //     A System.Net.Sockets.TcpClient used to send and receive data.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The listener has not been started with a call to System.Net.Sockets.TcpListener.Start().
    //
    //   System.Net.Sockets.SocketException:
    //     Use the System.Net.Sockets.SocketException.ErrorCode property to obtain the
    //     specific error code. When you have obtained this code, you can refer to the
    //     Windows�Sockets version 2 API error code documentation in MSDN for a detailed
    //     description of the error.
    public TcpClient AcceptTcpClient()
    {
      Contract.Ensures(Contract.Result<TcpClient>() != null);
      return null;

    }
//    public Task<TcpClient> AcceptTcpClientAsync();
    //
    // Summary:
    //     Enables or disables Network Address Translation (NAT) traversal on a System.Net.Sockets.TcpListener
    //     instance.
    //
    // Parameters:
    //   allowed:
    //     A Boolean value that specifies whether to enable or disable NAT traversal.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Net.Sockets.TcpListener.AllowNatTraversal(System.Boolean) method
    //     was called after calling the System.Net.Sockets.TcpListener.Start() method
//    public void AllowNatTraversal(bool allowed)
 //   {
  //  }
    //
    // Summary:
    //     Begins an asynchronous operation to accept an incoming connection attempt.
    //
    // Parameters:
    //   callback:
    //     An System.AsyncCallback delegate that references the method to invoke when
    //     the operation is complete.
    //
    //   state:
    //     A user-defined object containing information about the accept operation.
    //     This object is passed to the callback delegate when the operation is complete.
    //
    // Returns:
    //     An System.IAsyncResult that references the asynchronous creation of the System.Net.Sockets.Socket.
    //
    // Exceptions:
    //   System.Net.Sockets.SocketException:
    //     An error occurred while attempting to access the socket. See the Remarks
    //     section for more information.
    //
    //   System.ObjectDisposedException:
    //     The System.Net.Sockets.Socket has been closed.
    public IAsyncResult BeginAcceptSocket(AsyncCallback callback, object state)
    {
      Contract.Ensures(Contract.Result<IAsyncResult>() != null);
      return null;
    }
    //
    // Summary:
    //     Begins an asynchronous operation to accept an incoming connection attempt.
    //
    // Parameters:
    //   callback:
    //     An System.AsyncCallback delegate that references the method to invoke when
    //     the operation is complete.
    //
    //   state:
    //     A user-defined object containing information about the accept operation.
    //     This object is passed to the callback delegate when the operation is complete.
    //
    // Returns:
    //     An System.IAsyncResult that references the asynchronous creation of the System.Net.Sockets.TcpClient.
    //
    // Exceptions:
    //   System.Net.Sockets.SocketException:
    //     An error occurred while attempting to access the socket. See the Remarks
    //     section for more information.
    //
    //   System.ObjectDisposedException:
    //     The System.Net.Sockets.Socket has been closed.
    public IAsyncResult BeginAcceptTcpClient(AsyncCallback callback, object state)
    {
      Contract.Ensures(Contract.Result<IAsyncResult>() != null);
      return null;

    }

#if NETFRAMEWORK_4_5
    public static TcpListener Create(int port)
    {
      Contract.Ensures(Contract.Result<TcpListener>() != null);
      return null;
    }
#endif
    //
    // Summary:
    //     Asynchronously accepts an incoming connection attempt and creates a new System.Net.Sockets.Socket
    //     to handle remote host communication.
    //
    // Parameters:
    //   asyncResult:
    //     An System.IAsyncResult returned by a call to the System.Net.Sockets.TcpListener.BeginAcceptSocket(System.AsyncCallback,System.Object)
    //     method.
    //
    // Returns:
    //     A System.Net.Sockets.Socket.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The underlying System.Net.Sockets.Socket has been closed.
    //
    //   System.ArgumentNullException:
    //     The asyncResult parameter is null.
    //
    //   System.ArgumentException:
    //     The asyncResult parameter was not created by a call to the System.Net.Sockets.TcpListener.BeginAcceptSocket(System.AsyncCallback,System.Object)
    //     method.
    //
    //   System.InvalidOperationException:
    //     The System.Net.Sockets.TcpListener.EndAcceptSocket(System.IAsyncResult) method
    //     was previously called.
    //
    //   System.Net.Sockets.SocketException:
    //     An error occurred while attempting to access the System.Net.Sockets.Socket.
    //     See the Remarks section for more information.
    public Socket EndAcceptSocket(IAsyncResult asyncResult)
    {
      Contract.Requires(asyncResult != null);
      Contract.Ensures(Contract.Result<Socket>() != null);

      return null;
    }
    //
    // Summary:
    //     Asynchronously accepts an incoming connection attempt and creates a new System.Net.Sockets.TcpClient
    //     to handle remote host communication.
    //
    // Parameters:
    //   asyncResult:
    //     An System.IAsyncResult returned by a call to the System.Net.Sockets.TcpListener.BeginAcceptTcpClient(System.AsyncCallback,System.Object)
    //     method.
    //
    // Returns:
    //     A System.Net.Sockets.TcpClient.
    public TcpClient EndAcceptTcpClient(IAsyncResult asyncResult)
    {
      Contract.Ensures(Contract.Result<TcpClient>() != null);

      return null;

    }
    //
    // Summary:
    //     Determines if there are pending connection requests.
    //
    // Returns:
    //     true if connections are pending; otherwise, false.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The listener has not been started with a call to System.Net.Sockets.TcpListener.Start().
    public bool Pending() { return false; }
    //
    // Summary:
    //     Starts listening for incoming connection requests.
    //
    // Exceptions:
    //   System.Net.Sockets.SocketException:
    //     Use the System.Net.Sockets.SocketException.ErrorCode property to obtain the
    //     specific error code. When you have obtained this code, you can refer to the
    //     Windows�Sockets version 2 API error code documentation in MSDN for a detailed
    //     description of the error.
    public void Start() { }
    //
    // Summary:
    //     Starts listening for incoming connection requests with a maximum number of
    //     pending connection.
    //
    // Parameters:
    //   backlog:
    //     The maximum length of the pending connections queue.
    //
    // Exceptions:
    //   System.Net.Sockets.SocketException:
    //     An error occurred while accessing the socket. See the Remarks section for
    //     more information.
    //
    //   System.ArgumentOutOfRangeException:
    //     The backlog parameter is less than zero or exceeds the maximum number of
    //     permitted connections.
    //
    //   System.InvalidOperationException:
    //     The underlying System.Net.Sockets.Socket is null.
    public void Start(int backlog) 
    {
      Contract.Requires(backlog >= 0);
    }
    //
    // Summary:
    //     Closes the listener.
    //
    // Exceptions:
    //   System.Net.Sockets.SocketException:
    //     Use the System.Net.Sockets.SocketException.ErrorCode property to obtain the
    //     specific error code. When you have obtained this code, you can refer to the
    //     Windows�Sockets version 2 API error code documentation in MSDN for a detailed
    //     description of the error.
    public void Stop() { }
  }
}
#endif
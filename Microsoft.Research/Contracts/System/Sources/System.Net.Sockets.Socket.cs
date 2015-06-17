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

// File System.Net.Sockets.Socket.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Net.Sockets
{
  public partial class Socket : IDisposable
  {
    #region Methods and constructors
    public System.Net.Sockets.Socket Accept()
    {
      Contract.Ensures(Contract.Result<System.Net.Sockets.Socket>() != null);

      return default(System.Net.Sockets.Socket);
    }

    public bool AcceptAsync(SocketAsyncEventArgs e)
    {
      Contract.Requires(e != null);

      return default(bool);
    }

    public IAsyncResult BeginAccept(AsyncCallback callback, Object state)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginAccept(int receiveSize, AsyncCallback callback, Object state)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginAccept(System.Net.Sockets.Socket acceptSocket, int receiveSize, AsyncCallback callback, Object state)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginConnect(System.Net.IPAddress[] addresses, int port, AsyncCallback requestCallback, Object state)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginConnect(string host, int port, AsyncCallback requestCallback, Object state)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginConnect(System.Net.IPAddress address, int port, AsyncCallback requestCallback, Object state)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginConnect(System.Net.EndPoint remoteEP, AsyncCallback callback, Object state)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginDisconnect(bool reuseSocket, AsyncCallback callback, Object state)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginReceive(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, Object state)
    {
      errorCode = default(SocketError);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginReceive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, Object state)
    {
      errorCode = default(SocketError);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginReceive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public IAsyncResult BeginReceive(byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public IAsyncResult BeginReceiveFrom(byte[] buffer, int offset, int size, SocketFlags socketFlags, ref System.Net.EndPoint remoteEP, AsyncCallback callback, Object state)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginReceiveMessageFrom(byte[] buffer, int offset, int size, SocketFlags socketFlags, ref System.Net.EndPoint remoteEP, AsyncCallback callback, Object state)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginSend(byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public IAsyncResult BeginSend(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, Object state)
    {
      errorCode = default(SocketError);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginSend(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, Object state)
    {
      errorCode = default(SocketError);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginSend(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public IAsyncResult BeginSendFile(string fileName, AsyncCallback callback, Object state)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginSendFile(string fileName, byte[] preBuffer, byte[] postBuffer, TransmitFileOptions flags, AsyncCallback callback, Object state)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginSendTo(byte[] buffer, int offset, int size, SocketFlags socketFlags, System.Net.EndPoint remoteEP, AsyncCallback callback, Object state)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public void Bind(System.Net.EndPoint localEP)
    {
    }

    public static void CancelConnectAsync(SocketAsyncEventArgs e)
    {
    }

    public void Close()
    {
    }

    public void Close(int timeout)
    {
    }

    public void Connect(System.Net.EndPoint remoteEP)
    {
    }

    public void Connect(System.Net.IPAddress[] addresses, int port)
    {
      Contract.Ensures(this.Connected == true);
    }

    public void Connect(System.Net.IPAddress address, int port)
    {
    }

    public void Connect(string host, int port)
    {
      Contract.Ensures(this.Connected == true);
    }

    public bool ConnectAsync(SocketAsyncEventArgs e)
    {
      Contract.Requires(e != null);

      return default(bool);
    }

    public static bool ConnectAsync(SocketType socketType, ProtocolType protocolType, SocketAsyncEventArgs e)
    {
      Contract.Requires(e != null);

      return default(bool);
    }

    public void Disconnect(bool reuseSocket)
    {
    }

    public bool DisconnectAsync(SocketAsyncEventArgs e)
    {
      Contract.Requires(e != null);

      return default(bool);
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public void Dispose()
    {
    }

    public SocketInformation DuplicateAndClose(int targetProcessId)
    {
      return default(SocketInformation);
    }

    public System.Net.Sockets.Socket EndAccept(out byte[] buffer, out int bytesTransferred, IAsyncResult asyncResult)
    {
      buffer = default(byte[]);
      bytesTransferred = default(int);

      return default(System.Net.Sockets.Socket);
    }

    public System.Net.Sockets.Socket EndAccept(IAsyncResult asyncResult)
    {
      return default(System.Net.Sockets.Socket);
    }

    public System.Net.Sockets.Socket EndAccept(out byte[] buffer, IAsyncResult asyncResult)
    {
      Contract.Ensures(0 <= Contract.ValueAtReturn(out buffer).Length);

      buffer = default(byte[]);

      return default(System.Net.Sockets.Socket);
    }

    public void EndConnect(IAsyncResult asyncResult)
    {
    }

    public void EndDisconnect(IAsyncResult asyncResult)
    {
    }

    public int EndReceive(IAsyncResult asyncResult, out SocketError errorCode)
    {
      errorCode = default(SocketError);

      return default(int);
    }

    public int EndReceive(IAsyncResult asyncResult)
    {
      return default(int);
    }

    public int EndReceiveFrom(IAsyncResult asyncResult, ref System.Net.EndPoint endPoint)
    {
      return default(int);
    }

    public int EndReceiveMessageFrom(IAsyncResult asyncResult, ref SocketFlags socketFlags, ref System.Net.EndPoint endPoint, out IPPacketInformation ipPacketInformation)
    {
      ipPacketInformation = default(IPPacketInformation);

      return default(int);
    }

    public int EndSend(IAsyncResult asyncResult, out SocketError errorCode)
    {
      errorCode = default(SocketError);

      return default(int);
    }

    public int EndSend(IAsyncResult asyncResult)
    {
      return default(int);
    }

    public void EndSendFile(IAsyncResult asyncResult)
    {
    }

    public int EndSendTo(IAsyncResult asyncResult)
    {
      return default(int);
    }

    public Object GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName)
    {
      Contract.Ensures(Contract.Result<System.Object>() != null);

      return default(Object);
    }

    public void GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, byte[] optionValue)
    {
    }

    public byte[] GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionLength)
    {
      Contract.Requires(0 <= optionLength);
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }

    public int IOControl(IOControlCode ioControlCode, byte[] optionInValue, byte[] optionOutValue)
    {
      return default(int);
    }

    public int IOControl(int ioControlCode, byte[] optionInValue, byte[] optionOutValue)
    {
      return default(int);
    }

    public void Listen(int backlog)
    {
    }

    public bool Poll(int microSeconds, SelectMode mode)
    {
      return default(bool);
    }

    public int Receive(IList<ArraySegment<byte>> buffers)
    {
      Contract.Requires(0 <= buffers.Count);
      Contract.Ensures(1 <= buffers.Count);

      return default(int);
    }

    public int Receive(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode)
    {
      errorCode = default(SocketError);

      return default(int);
    }

    public int Receive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode)
    {
      Contract.Requires(0 <= buffers.Count);
      Contract.Ensures(1 <= buffers.Count);

      errorCode = default(SocketError);

      return default(int);
    }

    public int Receive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
    {
      Contract.Requires(0 <= buffers.Count);
      Contract.Ensures(1 <= buffers.Count);

      return default(int);
    }

    public int Receive(byte[] buffer, SocketFlags socketFlags)
    {
      return default(int);
    }

    public int Receive(byte[] buffer)
    {
      return default(int);
    }

    public int Receive(byte[] buffer, int offset, int size, SocketFlags socketFlags)
    {
      return default(int);
    }

    public int Receive(byte[] buffer, int size, SocketFlags socketFlags)
    {
      return default(int);
    }

    public bool ReceiveAsync(SocketAsyncEventArgs e)
    {
      Contract.Requires(e != null);

      return default(bool);
    }

    public int ReceiveFrom(byte[] buffer, SocketFlags socketFlags, ref System.Net.EndPoint remoteEP)
    {
      return default(int);
    }

    public int ReceiveFrom(byte[] buffer, int offset, int size, SocketFlags socketFlags, ref System.Net.EndPoint remoteEP)
    {
      return default(int);
    }

    public int ReceiveFrom(byte[] buffer, int size, SocketFlags socketFlags, ref System.Net.EndPoint remoteEP)
    {
      return default(int);
    }

    public int ReceiveFrom(byte[] buffer, ref System.Net.EndPoint remoteEP)
    {
      return default(int);
    }

    public bool ReceiveFromAsync(SocketAsyncEventArgs e)
    {
      Contract.Requires(e != null);

      return default(bool);
    }

    public int ReceiveMessageFrom(byte[] buffer, int offset, int size, ref SocketFlags socketFlags, ref System.Net.EndPoint remoteEP, out IPPacketInformation ipPacketInformation)
    {
      ipPacketInformation = default(IPPacketInformation);

      return default(int);
    }

    public bool ReceiveMessageFromAsync(SocketAsyncEventArgs e)
    {
      Contract.Requires(e != null);

      return default(bool);
    }

    public static void Select(System.Collections.IList checkRead, System.Collections.IList checkWrite, System.Collections.IList checkError, int microSeconds)
    {
    }

    public int Send(byte[] buffer)
    {
      return default(int);
    }

    public int Send(byte[] buffer, int size, SocketFlags socketFlags)
    {
      return default(int);
    }

    public int Send(byte[] buffer, SocketFlags socketFlags)
    {
      return default(int);
    }

    public int Send(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode)
    {
      Contract.Requires(0 <= buffers.Count);
      Contract.Ensures(1 <= buffers.Count);

      errorCode = default(SocketError);

      return default(int);
    }

    public int Send(IList<ArraySegment<byte>> buffers)
    {
      Contract.Requires(0 <= buffers.Count);
      Contract.Ensures(1 <= buffers.Count);

      return default(int);
    }

    public int Send(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
    {
      Contract.Requires(0 <= buffers.Count);
      Contract.Ensures(1 <= buffers.Count);

      return default(int);
    }

    public int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags)
    {
      return default(int);
    }

    public int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode)
    {
      errorCode = default(SocketError);

      return default(int);
    }

    public bool SendAsync(SocketAsyncEventArgs e)
    {
      Contract.Requires(e != null);

      return default(bool);
    }

    public void SendFile(string fileName, byte[] preBuffer, byte[] postBuffer, TransmitFileOptions flags)
    {
    }

    public void SendFile(string fileName)
    {
    }

    public bool SendPacketsAsync(SocketAsyncEventArgs e)
    {
      Contract.Requires(e != null);

      return default(bool);
    }

    public int SendTo(byte[] buffer, SocketFlags socketFlags, System.Net.EndPoint remoteEP)
    {
      return default(int);
    }

    public int SendTo(byte[] buffer, System.Net.EndPoint remoteEP)
    {
      return default(int);
    }

    public int SendTo(byte[] buffer, int size, SocketFlags socketFlags, System.Net.EndPoint remoteEP)
    {
      return default(int);
    }

    public int SendTo(byte[] buffer, int offset, int size, SocketFlags socketFlags, System.Net.EndPoint remoteEP)
    {
      return default(int);
    }

    public bool SendToAsync(SocketAsyncEventArgs e)
    {
      Contract.Requires(e != null);

      return default(bool);
    }

    public void SetIPProtectionLevel(IPProtectionLevel level)
    {
    }

    public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionValue)
    {
    }

    public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, byte[] optionValue)
    {
    }

    public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, Object optionValue)
    {
    }

    public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, bool optionValue)
    {
    }

    public void Shutdown(SocketShutdown how)
    {
    }

    public Socket(SocketInformation socketInformation)
    {
    }

    public Socket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
    {
    }
    #endregion

    #region Properties and indexers
    public AddressFamily AddressFamily
    {
      get
      {
        return default(AddressFamily);
      }
    }

    public int Available
    {
      get
      {
        return default(int);
      }
    }

    public bool Blocking
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool Connected
    {
      get
      {
        return default(bool);
      }
    }

    public bool DontFragment
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool EnableBroadcast
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool ExclusiveAddressUse
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public IntPtr Handle
    {
      get
      {
        return default(IntPtr);
      }
    }

    public bool IsBound
    {
      get
      {
        return default(bool);
      }
    }

    public LingerOption LingerState
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Net.Sockets.LingerOption>() != null);

        return default(LingerOption);
      }
      set
      {
      }
    }

    public System.Net.EndPoint LocalEndPoint
    {
      get
      {
        return default(System.Net.EndPoint);
      }
    }

    public bool MulticastLoopback
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool NoDelay
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public static bool OSSupportsIPv4
    {
      get
      {
        return default(bool);
      }
    }

    public static bool OSSupportsIPv6
    {
      get
      {
        return default(bool);
      }
    }

    public ProtocolType ProtocolType
    {
      get
      {
        return default(ProtocolType);
      }
    }

    public int ReceiveBufferSize
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int ReceiveTimeout
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public System.Net.EndPoint RemoteEndPoint
    {
      get
      {
        return default(System.Net.EndPoint);
      }
    }

    public int SendBufferSize
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int SendTimeout
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public SocketType SocketType
    {
      get
      {
        return default(SocketType);
      }
    }

    public static bool SupportsIPv4
    {
      get
      {
        return default(bool);
      }
    }

    public static bool SupportsIPv6
    {
      get
      {
        return default(bool);
      }
    }

    public short Ttl
    {
      get
      {
        Contract.Ensures(-32768 <= Contract.Result<short>());
        Contract.Ensures(Contract.Result<short>() <= 32767);

        return default(short);
      }
      set
      {
      }
    }

    public bool UseOnlyOverlappedIO
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }
    #endregion
  }
}

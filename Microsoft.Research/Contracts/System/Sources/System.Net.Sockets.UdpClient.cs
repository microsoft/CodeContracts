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

// File System.Net.Sockets.UdpClient.cs
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
  public partial class UdpClient : IDisposable
  {
    #region Methods and constructors
    public void AllowNatTraversal(bool allowed)
    {
    }

    public IAsyncResult BeginReceive(AsyncCallback requestCallback, Object state)
    {
      Contract.Requires(this.Client != null);
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginSend(byte[] datagram, int bytes, AsyncCallback requestCallback, Object state)
    {
      return default(IAsyncResult);
    }

    public IAsyncResult BeginSend(byte[] datagram, int bytes, System.Net.IPEndPoint endPoint, AsyncCallback requestCallback, Object state)
    {
      return default(IAsyncResult);
    }

    public IAsyncResult BeginSend(byte[] datagram, int bytes, string hostname, int port, AsyncCallback requestCallback, Object state)
    {
      return default(IAsyncResult);
    }

    public void Close()
    {
    }

    public void Connect(System.Net.IPAddress addr, int port)
    {
    }

    public void Connect(string hostname, int port)
    {
    }

    public void Connect(System.Net.IPEndPoint endPoint)
    {
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public void DropMulticastGroup(System.Net.IPAddress multicastAddr, int ifindex)
    {
      Contract.Requires(this.Client != null);
    }

    public void DropMulticastGroup(System.Net.IPAddress multicastAddr)
    {
    }

    public byte[] EndReceive(IAsyncResult asyncResult, ref System.Net.IPEndPoint remoteEP)
    {
      Contract.Requires(this.Client != null);

      return default(byte[]);
    }

    public int EndSend(IAsyncResult asyncResult)
    {
      return default(int);
    }

    public void JoinMulticastGroup(System.Net.IPAddress multicastAddr)
    {
    }

    public void JoinMulticastGroup(System.Net.IPAddress multicastAddr, System.Net.IPAddress localAddress)
    {
      Contract.Requires(this.Client != null);
    }

    public void JoinMulticastGroup(int ifindex, System.Net.IPAddress multicastAddr)
    {
      Contract.Requires(this.Client != null);
    }

    public void JoinMulticastGroup(System.Net.IPAddress multicastAddr, int timeToLive)
    {
    }

    public byte[] Receive(ref System.Net.IPEndPoint remoteEP)
    {
      Contract.Requires(this.Client != null);

      return default(byte[]);
    }

    public int Send(byte[] dgram, int bytes, System.Net.IPEndPoint endPoint)
    {
      return default(int);
    }

    public int Send(byte[] dgram, int bytes)
    {
      Contract.Requires(this.Client != null);

      return default(int);
    }

    public int Send(byte[] dgram, int bytes, string hostname, int port)
    {
      return default(int);
    }

    void System.IDisposable.Dispose()
    {
    }

    public UdpClient()
    {
    }

    public UdpClient(AddressFamily family)
    {
    }

    public UdpClient(int port)
    {
    }

    public UdpClient(string hostname, int port)
    {
    }

    public UdpClient(System.Net.IPEndPoint localEP)
    {
    }

    public UdpClient(int port, AddressFamily family)
    {
    }
    #endregion

    #region Properties and indexers
    protected bool Active
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public int Available
    {
      get
      {
        return default(int);
      }
    }

    public Socket Client
    {
      get
      {
        return default(Socket);
      }
      set
      {
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

    public short Ttl
    {
      get
      {
        return default(short);
      }
      set
      {
      }
    }
    #endregion
  }
}

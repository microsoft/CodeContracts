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

// File System.Net.Sockets.TcpClient.cs
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
  public partial class TcpClient : IDisposable
  {
    #region Methods and constructors
    public IAsyncResult BeginConnect(System.Net.IPAddress address, int port, AsyncCallback requestCallback, Object state)
    {
      Contract.Requires(this.Client != null);
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginConnect(System.Net.IPAddress[] addresses, int port, AsyncCallback requestCallback, Object state)
    {
      Contract.Requires(this.Client != null);
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginConnect(string host, int port, AsyncCallback requestCallback, Object state)
    {
      Contract.Requires(this.Client != null);
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public void Close()
    {
    }

    public void Connect(string hostname, int port)
    {
    }

    public void Connect(System.Net.IPEndPoint remoteEP)
    {
      Contract.Requires(this.Client != null);
    }

    public void Connect(System.Net.IPAddress[] ipAddresses, int port)
    {
      Contract.Requires(this.Client != null);
    }

    public void Connect(System.Net.IPAddress address, int port)
    {
      Contract.Requires(this.Client != null);
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public void EndConnect(IAsyncResult asyncResult)
    {
      Contract.Requires(this.Client != null);
    }

    public NetworkStream GetStream()
    {
      Contract.Requires(this.Client != null);
      Contract.Ensures(Contract.Result<System.Net.Sockets.NetworkStream>() != null);

      return default(NetworkStream);
    }

    void System.IDisposable.Dispose()
    {
    }

    public TcpClient()
    {
    }

    public TcpClient(System.Net.IPEndPoint localEP)
    {
    }

    public TcpClient(AddressFamily family)
    {
    }

    public TcpClient(string hostname, int port)
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

    public bool Connected
    {
      get
      {
        return default(bool);
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

    public LingerOption LingerState
    {
      get
      {
        Contract.Requires(this.Client != null);
        Contract.Ensures(Contract.Result<System.Net.Sockets.LingerOption>() != null);

        return default(LingerOption);
      }
      set
      {
        Contract.Requires(this.Client != null);
      }
    }

    public bool NoDelay
    {
      get
      {
        Contract.Requires(this.Client != null);

        return default(bool);
      }
      set
      {
        Contract.Requires(this.Client != null);
      }
    }

    public int ReceiveBufferSize
    {
      get
      {
        Contract.Requires(this.Client != null);

        return default(int);
      }
      set
      {
        Contract.Requires(this.Client != null);
      }
    }

    public int ReceiveTimeout
    {
      get
      {
        Contract.Requires(this.Client != null);

        return default(int);
      }
      set
      {
        Contract.Requires(this.Client != null);
      }
    }

    public int SendBufferSize
    {
      get
      {
        Contract.Requires(this.Client != null);

        return default(int);
      }
      set
      {
        Contract.Requires(this.Client != null);
      }
    }

    public int SendTimeout
    {
      get
      {
        Contract.Requires(this.Client != null);

        return default(int);
      }
      set
      {
        Contract.Requires(this.Client != null);
      }
    }
    #endregion
  }
}

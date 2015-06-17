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
using System.Net;
using System.Diagnostics.Contracts;

namespace System.Net.Sockets
{
  public class TcpClient // : IDisposable
  {
    public TcpClient() { }

    public TcpClient(AddressFamily family)
    {
      Contract.Requires(family == AddressFamily.InterNetwork || family == AddressFamily.InterNetworkV6);
    }

    public TcpClient(IPEndPoint localEP)
    {
      Contract.Requires(localEP != null);
    }

    public TcpClient(string hostname, int port)
    {
      Contract.Requires(hostname != null);
      Contract.Requires(port >= 0);
      Contract.Requires(port <= 0xffff);
    }

    // protected bool Active { get; set; }
    // public int Available { get; }

    // public Socket Client { get; set; }
   //    public bool Connected { get; }
   // public bool ExclusiveAddressUse { get; set; }
//    public LingerOption LingerState { get; set; }
  //  public bool NoDelay { get; set; }
    
    public int ReceiveBufferSize
    {
      get
      { 
        Contract.Ensures(Contract.Result<int>() >= 0);

        return 0;
      }
      set
      {
        Contract.Requires(value >= 0);
      }
    }

    public int ReceiveTimeout
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return 0;
      }
      set
      {
        Contract.Requires(value >= 0);
      }
    }

    
    public int SendBufferSize
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return 0;
      }
      set
      {
        Contract.Requires(value >= 0);
      }
    }

    public int SendTimeout
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return 0;
      }
      set
      {
        Contract.Requires(value >= 0);
      }
    }

    public IAsyncResult BeginConnect(IPAddress address, int port, AsyncCallback requestCallback, object state)
    {
      Contract.Requires(address != null);
      Contract.Requires(port >= 0);
      Contract.Requires(port <= 0xffff);

      Contract.Ensures(Contract.Result<IAsyncResult>() != null);

      return null;
    }
    public IAsyncResult BeginConnect(IPAddress[] addresses, int port, AsyncCallback requestCallback, object state)
    {
      Contract.Requires(addresses != null);
      Contract.Requires(port >= 0);
      Contract.Requires(port <= 0xffff);

      Contract.Ensures(Contract.Result<IAsyncResult>() != null);

      return null;
    }
    
    public IAsyncResult BeginConnect(string host, int port, AsyncCallback requestCallback, object state)
    {
      Contract.Requires(host!= null);
      Contract.Requires(port >= 0);
      Contract.Requires(port <= 0xffff);

      Contract.Ensures(Contract.Result<IAsyncResult>() != null);

      return null;
    }

    extern public void Close();

    public void Connect(IPEndPoint remoteEP)
    {
      Contract.Requires(remoteEP != null);
    }

    public void Connect(IPAddress address, int port)
    {
      Contract.Requires(address != null); 
      Contract.Requires(port >= 0);
      Contract.Requires(port <= 0xffff);
    }

    public void Connect(IPAddress[] ipAddresses, int port)
    {
      Contract.Requires(ipAddresses != null);
      Contract.Requires(port >= 0);
      Contract.Requires(port <= 0xffff);
    }

    public void Connect(string hostname, int port)
    {
      Contract.Requires(hostname != null);
      Contract.Requires(port >= 0);
      Contract.Requires(port <= 0xffff);
    }
    //
    protected virtual void Dispose(bool disposing) { }
    
    public void EndConnect(IAsyncResult asyncResult)
    {
      Contract.Requires(asyncResult != null);
    }

    public NetworkStream GetStream()
    {
      Contract.Ensures(Contract.Result<NetworkStream>() != null);

      return null;
    }
  }
}
#endif
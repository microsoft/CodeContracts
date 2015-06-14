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
using System.Diagnostics.Contracts;
using System;

namespace System.Net.Sockets
{

  public class UdpClient
  {

    public void DropMulticastGroup(IPAddress multicastAddr, int ifindex)
    {
      Contract.Requires(multicastAddr != null);
      Contract.Requires(ifindex >= 0);

    }
    public void DropMulticastGroup(IPAddress multicastAddr)
    {
      Contract.Requires(multicastAddr != null);

    }
    public void JoinMulticastGroup(IPAddress multicastAddr, int timeToLive)
    {
      Contract.Requires(multicastAddr != null);

    }
    public void JoinMulticastGroup(int ifindex, IPAddress multicastAddr)
    {
      Contract.Requires(multicastAddr != null);
      Contract.Requires(ifindex >= 0);

    }
    public void JoinMulticastGroup(IPAddress multicastAddr)
    {

    }
    public Byte[] Receive(ref IPEndPoint remoteEP)
    {
      Contract.Ensures(Contract.Result<Byte[]>() != null);
      Contract.Ensures(Contract.Result<Byte[]>().Length > 0); // after reading the code

      return default(Byte[]);
    }
    public int Send(Byte[] dgram, int bytes)
    {
      Contract.Requires(dgram != null);

      return default(int);
    }
    public int Send(Byte[] dgram, int bytes, string hostname, int port)
    {
      Contract.Requires(dgram != null);
     
      return default(int);
    }

    public int Send(Byte[] dgram, int bytes, IPEndPoint endPoint)
    {
      Contract.Requires(dgram != null);

      return default(int);
    }
    public void Connect(IPEndPoint endPoint)
    {
      Contract.Requires(endPoint != null);

    }
    public void Connect(IPAddress addr, int port)
    {
      Contract.Requires(addr != null);

    }
    public void Connect(string hostname, int port)
    {

    }
    public void Close()
    {

    }
    public UdpClient(string hostname, int port)
    {
      Contract.Requires(hostname != null);

    }
    public UdpClient(IPEndPoint localEP)
    {
      Contract.Requires(localEP != null);

    }
    public UdpClient(int port, AddressFamily family)
    {
      Contract.Requires((int)family == 2 || (int)family == 23);

    }
    public UdpClient(int port)
    {
    }
    public UdpClient(AddressFamily family)
    {
      Contract.Requires((int)family == 2 || (int)family == 23);
    }
    public UdpClient()
    {
    }
  }
}
#endif
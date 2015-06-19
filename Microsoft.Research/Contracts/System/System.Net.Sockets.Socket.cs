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
using System.Diagnostics.Contracts;

namespace System.Net.Sockets
{
  [Flags]
  public enum SocketFlags
  {
    None = 0,
    OutOfBand = 1,
    Peek = 2,
    DontRoute = 4,
    MaxIOVectorLength = 16,
    Truncated = 256,
    ControlDataTruncated = 512,
    Broadcast = 1024,
    Multicast = 2048,
    Partial = 32768,
  }

  public enum SocketType
  {
    Unknown = -1,
    Stream = 1,
    Dgram = 2,
    Raw = 3,
    Rdm = 4,
    Seqpacket = 5,
  }

  public enum SelectMode
  {
    SelectRead = 0,
    SelectWrite = 1,
    SelectError = 2,
  }

  public enum SocketOptionLevel
  {
    IP = 0,
    Tcp = 6,
    Udp = 17,
    IPv6 = 41,
    Socket = 65535,
  }

  public enum ProtocolType
  {
    Unknown = -1,
    IPv6HopByHopOptions = 0,
    Unspecified = 0,
    IP = 0,
    Icmp = 1,
    Igmp = 2,
    Ggp = 3,
    IPv4 = 4,
    Tcp = 6,
    Pup = 12,
    Udp = 17,
    Idp = 22,
    IPv6 = 41,
    IPv6RoutingHeader = 43,
    IPv6FragmentHeader = 44,
    IPSecEncapsulatingSecurityPayload = 50,
    IPSecAuthenticationHeader = 51,
    IcmpV6 = 58,
    IPv6NoNextHeader = 59,
    IPv6DestinationOptions = 60,
    ND = 77,
    Raw = 255,
    Ipx = 1000,
    Spx = 1256,
    SpxII = 1257,
  }

  public enum SocketOptionName
  {
    DontLinger = -129,
    ExclusiveAddressUse = -5,
    IPOptions = 1,
    NoDelay = 1,
    NoChecksum = 1,
    Debug = 1,
    BsdUrgent = 2,
    Expedited = 2,
    HeaderIncluded = 2,
    AcceptConnection = 2,
    TypeOfService = 3,
    ReuseAddress = 4,
    IpTimeToLive = 4,
    KeepAlive = 8,
    MulticastInterface = 9,
    MulticastTimeToLive = 10,
    MulticastLoopback = 11,
    AddMembership = 12,
    DropMembership = 13,
    DontFragment = 14,
    AddSourceMembership = 15,
    DropSourceMembership = 16,
    DontRoute = 16,
    BlockSource = 17,
    UnblockSource = 18,
    PacketInformation = 19,
    ChecksumCoverage = 20,
    HopLimit = 21,
#if NETFRAMEWORK_4_0
    IPProtectionLevel = 23,
    IPv6Only = 27,
#endif
    Broadcast = 32,
    UseLoopback = 64,
    Linger = 128,
    OutOfBandInline = 256,
    SendBuffer = 4097,
    ReceiveBuffer = 4098,
    SendLowWater = 4099,
    ReceiveLowWater = 4100,
    SendTimeout = 4101,
    ReceiveTimeout = 4102,
    Error = 4103,
    Type = 4104,
    UpdateAcceptContext = 28683,
    UpdateConnectContext = 28688,
    MaxConnections = 2147483647,
  }


  public class Socket
  {
    extern public int Available
    {
      get;
    }

    extern public ProtocolType ProtocolType
    {
      get;
    }

    extern public System.Net.EndPoint RemoteEndPoint
    {
      get;
    }


    extern public System.Net.EndPoint LocalEndPoint
    {
      get;
    }

    extern public static bool SupportsIPv4
    {
      get;
    }

    extern public static bool SupportsIPv6
    {
      get;
    }

    extern public bool Blocking
    {
      get;
      set;
    }

    extern public bool Connected
    {
      get;
    }

    extern public SocketType SocketType
    {
      get;
    }

    extern public AddressFamily AddressFamily
    {
      get;
    }

    public Socket EndAccept(IAsyncResult asyncResult)
    {
      Contract.Requires(asyncResult != null);

      return default(Socket);
    }

    public IAsyncResult BeginAccept(AsyncCallback callback, object state)
    {
      return default(IAsyncResult);
    }

    public int EndReceiveFrom(IAsyncResult asyncResult, ref System.Net.EndPoint endPoint)
    {
      Contract.Requires(asyncResult != null);

      return default(int);
    }
    public IAsyncResult BeginReceiveFrom(Byte[] buffer, int offset, int size, SocketFlags socketFlags, ref System.Net.EndPoint remoteEP, AsyncCallback callback, object state)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(offset <= buffer.Length);
      Contract.Requires(size >= 0);
      Contract.Requires(size <= (buffer.Length - offset));

      return default(IAsyncResult);
    }
    public int EndReceive(IAsyncResult asyncResult)
    {
      Contract.Requires(asyncResult != null);

      return default(int);
    }
    public IAsyncResult BeginReceive(Byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, object state)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(offset <= buffer.Length);
      Contract.Requires(size >= 0);
      Contract.Requires(size <= (buffer.Length - offset));

      return default(IAsyncResult);
    }
    public int EndSendTo(IAsyncResult asyncResult)
    {
      Contract.Requires(asyncResult != null);

      return default(int);
    }
    public IAsyncResult BeginSendTo(Byte[] buffer, int offset, int size, SocketFlags socketFlags,
      System.Net.EndPoint remoteEP, AsyncCallback callback, object state)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(remoteEP != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(offset <= buffer.Length);
      Contract.Requires(size >= 0);
      Contract.Requires(size <= (buffer.Length - offset));

      return default(IAsyncResult);
    }
    public int EndSend(IAsyncResult asyncResult)
    {
      Contract.Requires(asyncResult != null);

      return default(int);
    }
    public IAsyncResult BeginSend(Byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, object state)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(offset <= buffer.Length);
      Contract.Requires(size >= 0);
      Contract.Requires(size <= (buffer.Length - offset));

      return default(IAsyncResult);
    }
    public void EndConnect(IAsyncResult asyncResult)
    {
      Contract.Requires(asyncResult != null);

    }
    public IAsyncResult BeginConnect(System.Net.EndPoint remoteEP, AsyncCallback callback, object state)
    {
      Contract.Requires(remoteEP != null);

      return default(IAsyncResult);
    }
    public static void Select(System.Collections.IList checkRead, System.Collections.IList checkWrite,
      System.Collections.IList checkError, int microSeconds)
    {
      Contract.Requires(checkError != null);

    }
    public bool Poll(int microSeconds, SelectMode mode)
    {

      return default(bool);
    }
    public Byte[] GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionLength)
    {

      return default(Byte[]);
    }
    public void GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, Byte[] optionValue)
    {

    }
    public object GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName)
    {

      return default(object);
    }
    public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, object optionValue)
    {
      Contract.Requires(optionValue != null);
    }
    public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, Byte[] optionValue)
    {

    }
    public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionValue)
    {

    }
    public int IOControl(int ioControlCode, Byte[] optionInValue, Byte[] optionOutValue)
    {
      Contract.Requires(ioControlCode != -2147195266);

      return default(int);
    }
    public int ReceiveFrom(Byte[] buffer, ref System.Net.EndPoint remoteEP)
    {

      return default(int);
    }
    public int ReceiveFrom(Byte[] buffer, SocketFlags socketFlags, ref System.Net.EndPoint remoteEP)
    {

      return default(int);
    }
    public int ReceiveFrom(Byte[] buffer, int size, SocketFlags socketFlags, ref System.Net.EndPoint remoteEP)
    {

      return default(int);
    }
    public int ReceiveFrom(Byte[] buffer, int offset, int size, SocketFlags socketFlags, ref System.Net.EndPoint remoteEP)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(offset <= buffer.Length);
      Contract.Requires(size >= 0);
      Contract.Requires(size <= (buffer.Length - offset));

      return default(int);
    }
    public int Receive(Byte[] buffer, int offset, int size, SocketFlags socketFlags)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(offset <= buffer.Length);
      Contract.Requires(size >= 0);
      Contract.Requires(size <= (buffer.Length - offset));

      return default(int);
    }
    public int Receive(Byte[] buffer)
    {

      return default(int);
    }
    public int Receive(Byte[] buffer, SocketFlags socketFlags)
    {

      return default(int);
    }
    public int Receive(Byte[] buffer, int size, SocketFlags socketFlags)
    {

      return default(int);
    }
    public int SendTo(Byte[] buffer, System.Net.EndPoint remoteEP)
    {

      return default(int);
    }
    public int SendTo(Byte[] buffer, SocketFlags socketFlags, System.Net.EndPoint remoteEP)
    {

      return default(int);
    }
    public int SendTo(Byte[] buffer, int size, SocketFlags socketFlags, System.Net.EndPoint remoteEP)
    {

      return default(int);
    }
    public int SendTo(Byte[] buffer, int offset, int size, SocketFlags socketFlags, System.Net.EndPoint remoteEP)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(remoteEP != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(offset <= buffer.Length);
      Contract.Requires(size >= 0);
      Contract.Requires(size <= (buffer.Length - offset));

      return default(int);
    }
    public int Send(Byte[] buffer, int offset, int size, SocketFlags socketFlags)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(offset <= buffer.Length);
      Contract.Requires(size >= 0);
      Contract.Requires(size <= (buffer.Length - offset));

      return default(int);
    }
    public int Send(Byte[] buffer)
    {

      return default(int);
    }
    public int Send(Byte[] buffer, SocketFlags socketFlags)
    {

      return default(int);
    }
    public int Send(Byte[] buffer, int size, SocketFlags socketFlags)
    {

      return default(int);
    }
    public Socket Accept()
    {

      return default(Socket);
    }
    public void Listen(int backlog)
    {

    }
    //public void Shutdown(SocketShutdown how)
    //{

    //}
    public void Close()
    {

    }
    public void Connect(System.Net.EndPoint remoteEP)
    {
      Contract.Requires(remoteEP != null);

    }
    public void Bind(System.Net.EndPoint localEP)
    {
      Contract.Requires(localEP != null);

    }
    public Socket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
    {
    }
  }
}

#endif
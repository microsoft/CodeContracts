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

// File System.Net.IPAddress.cs
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


namespace System.Net
{
  public partial class IPAddress
  {
    #region Methods and constructors
    public override bool Equals(Object comparand)
    {
      return default(bool);
    }

    public byte[] GetAddressBytes()
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public static short HostToNetworkOrder(short host)
    {
      Contract.Ensures(Contract.Result<short>() <= 32767);
      Contract.Ensures(Contract.Result<short>() == (short)((((host & 255) << 8) | ((host >> 8) & 255))));

      return default(short);
    }

    public static int HostToNetworkOrder(int host)
    {
      Contract.Ensures(0 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 2147450879);
      Contract.Ensures(Contract.Result<int>() == ((((((short)(((((short)(host) & 255) << 8) | (((short)(host) >> 8) & 255))) & 65535)) != 0 << 16) | (((short)(((((short)((host >> 16)) & 255) << 8) | (((short)((host >> 16)) >> 8) & 255))) & 65535)) != 0)));

      return default(int);
    }

    public static long HostToNetworkOrder(long host)
    {
      return default(long);
    }

    public IPAddress(long newAddress)
    {
    }

    public IPAddress(byte[] address)
    {
    }

    public IPAddress(byte[] address, long scopeid)
    {
    }

    public static bool IsLoopback(System.Net.IPAddress address)
    {
      Contract.Requires(address != null);

      return default(bool);
    }

    public static int NetworkToHostOrder(int network)
    {
      Contract.Ensures(0 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 2147450879);

      return default(int);
    }

    public static long NetworkToHostOrder(long network)
    {
      return default(long);
    }

    public static short NetworkToHostOrder(short network)
    {
      Contract.Ensures(Contract.Result<short>() == (short)((((network & 255) << 8) | ((network >> 8) & 255))));

      return default(short);
    }

    public static System.Net.IPAddress Parse(string ipString)
    {
      return default(System.Net.IPAddress);
    }

    public override string ToString()
    {
      return default(string);
    }

    public static bool TryParse(string ipString, out System.Net.IPAddress address)
    {
      Contract.Ensures(Contract.Result<bool>() == ((Contract.ValueAtReturn(out address) == null) == false));

      address = default(System.Net.IPAddress);

      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public long Address
    {
      get
      {
        return default(long);
      }
      set
      {
      }
    }

    public System.Net.Sockets.AddressFamily AddressFamily
    {
      get
      {
        return default(System.Net.Sockets.AddressFamily);
      }
    }

    public bool IsIPv6LinkLocal
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsIPv6Multicast
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsIPv6SiteLocal
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsIPv6Teredo
    {
      get
      {
        return default(bool);
      }
    }

    public long ScopeId
    {
      get
      {
        return default(long);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Net.IPAddress Any;
    public readonly static System.Net.IPAddress Broadcast;
    public readonly static System.Net.IPAddress IPv6Any;
    public readonly static System.Net.IPAddress IPv6Loopback;
    public readonly static System.Net.IPAddress IPv6None;
    public readonly static System.Net.IPAddress Loopback;
    public readonly static System.Net.IPAddress None;
    #endregion
  }
}

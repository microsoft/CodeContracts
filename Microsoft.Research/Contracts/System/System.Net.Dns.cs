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

namespace System.Net
{
  public class Dns
  {

    public static IPHostEntry EndResolve(IAsyncResult asyncResult)
    {
      Contract.Requires(asyncResult != null);
      Contract.Ensures(Contract.Result<IPHostEntry>() != null);

      return default(IPHostEntry);
    }

    public static IAsyncResult BeginResolve(string hostName, AsyncCallback requestCallback, object stateObject)
    {
      Contract.Requires(hostName != null);
      Contract.Ensures(Contract.Result<IAsyncResult>() != null);
      Contract.EnsuresOnThrow<System.Net.Sockets.SocketException>(true);

      return default(IAsyncResult);
    }

    public static IPHostEntry EndGetHostByName(IAsyncResult asyncResult)
    {
      Contract.Requires(asyncResult != null);
      Contract.Ensures(Contract.Result<IPHostEntry>() != null);

      return default(IPHostEntry);
    }

    public static IAsyncResult BeginGetHostByName(string hostName, AsyncCallback requestCallback, object stateObject)
    {
      Contract.Requires(hostName != null);
      Contract.Ensures(Contract.Result<IAsyncResult>() != null);
      Contract.EnsuresOnThrow<System.Net.Sockets.SocketException>(true);

      return default(IAsyncResult);
    }

    public static IPHostEntry Resolve(string hostName)
    {
      Contract.Ensures(Contract.Result<IPHostEntry>() != null);
      Contract.EnsuresOnThrow<System.Net.Sockets.SocketException>(true);

      return default(IPHostEntry);
    }
    
    public static string GetHostName()
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.EnsuresOnThrow<System.Net.Sockets.SocketException>(true);

      return default(string);
    }
    
    public static IPHostEntry GetHostByAddress(IPAddress address)
    {
      Contract.Requires(address != null);
      Contract.Ensures(Contract.Result<IPHostEntry>() != null);
      Contract.EnsuresOnThrow<System.Net.Sockets.SocketException>(true);

      return default(IPHostEntry);
    }
    
    public static IPHostEntry GetHostByAddress(string address)
    {
      Contract.Requires(address != null);
      Contract.Ensures(Contract.Result<IPHostEntry>() != null);
      Contract.EnsuresOnThrow<System.Net.Sockets.SocketException>(true);
      Contract.EnsuresOnThrow<System.FormatException>(true);

      return default(IPHostEntry);
    }
    
    public static IPHostEntry GetHostByName(string hostName)
    {
      Contract.Requires(hostName != null);
      Contract.Requires(hostName.Length <= 126);
      Contract.Ensures(Contract.Result<IPHostEntry>() != null);
      Contract.EnsuresOnThrow<System.Net.Sockets.SocketException>(true);

      return default(IPHostEntry);
    }

#if NETFRAMEWORK_4_0
    public static IPAddress[] GetHostAddresses(string hostNameOrAddress)
    {
      Contract.Requires(hostNameOrAddress != null);
      Contract.Ensures(Contract.Result<IPAddress[]>() != null);
      return null;
    }
#endif
  }
}
#endif
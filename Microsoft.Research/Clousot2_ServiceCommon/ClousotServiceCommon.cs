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

#define NET_TCP_BINDING

using System;
using System.Diagnostics.Contracts;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.Research.Cloudot.Common
{
  public static class ClousotWindowsServiceCommon
  {
    public const string ServiceName = "Cloudot";
  }

  // Notes:
  // With the HTTP binding the callbacks do not work when we do not have the administrator privileges. This was the first one that Mehdi tried
  // With the NET_TCP binding, it works well and when we use the service for the very first time it will ask if we want to add it to the firewall (it is not the case for HTTP binding which throws an exception)
  public static class ClousotWCFServiceCommon
  {
    public static readonly string BaseAddress;
    public static readonly Uri BaseUri;
    public static readonly EndpointIdentity Identity;
    public static readonly SecurityMode SecurityMode;

    public static Binding NewBinding(SecurityMode securityMode)
    {
      Contract.Ensures(Contract.Result<Binding>() != null);

#if WS_HTTP_BINDING
      return new WSDualHttpBinding(securityMode);
#elif NET_TCP_BINDING
      return new NetTcpBinding(securityMode);
#endif
    }

    static ClousotWCFServiceCommon()
    {
      Identity = new DnsEndpointIdentity("research.microsoft.com"); // Used by the client to be sure that we are who we say we are

#if DEBUG
      SecurityMode = SecurityMode.None;  // Mehdi: "Very complex things", we ignore them
#else
      SecurityMode = SecurityMode.None; // TODO
#endif

#if WS_HTTP_BINDING
#if DEBUG
      // This address does not need administrator privilege
      // Namespace created during the installation of Visual Studio
      BaseAddress = "http://localhost:8732/Design_Time_Addresses/ClousotService/";
#else
      // Need administrator privilege
      BaseAddress = "http://localhost:8732/Microsoft.Research/ClousotService/";
#endif
#elif NET_TCP_BINDING
      BaseAddress = "net.tcp://localhost:9922/ClousotService/";
#endif
      BaseUri = new Uri(BaseAddress);
    }
  }
}

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

// File System.Net.HttpListenerRequest.cs
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
  sealed public partial class HttpListenerRequest
  {
    #region Methods and constructors
    public IAsyncResult BeginGetClientCertificate(AsyncCallback requestCallback, Object state)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public System.Security.Cryptography.X509Certificates.X509Certificate2 EndGetClientCertificate(IAsyncResult asyncResult)
    {
      return default(System.Security.Cryptography.X509Certificates.X509Certificate2);
    }

    public System.Security.Cryptography.X509Certificates.X509Certificate2 GetClientCertificate()
    {
      return default(System.Security.Cryptography.X509Certificates.X509Certificate2);
    }

    internal HttpListenerRequest()
    {
    }
    #endregion

    #region Properties and indexers
    public string[] AcceptTypes
    {
      get
      {
        return default(string[]);
      }
    }

    public int ClientCertificateError
    {
      get
      {
        return default(int);
      }
    }

    public Encoding ContentEncoding
    {
      get
      {
        return default(Encoding);
      }
    }

    public long ContentLength64
    {
      get
      {
        return default(long);
      }
    }

    public string ContentType
    {
      get
      {
        return default(string);
      }
    }

    public CookieCollection Cookies
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Net.CookieCollection>() != null);

        return default(CookieCollection);
      }
    }

    public bool HasEntityBody
    {
      get
      {
        return default(bool);
      }
    }

    public System.Collections.Specialized.NameValueCollection Headers
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.Specialized.NameValueCollection>() != null);

        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public string HttpMethod
    {
      get
      {
        return default(string);
      }
    }

    public Stream InputStream
    {
      get
      {
        Contract.Ensures(Contract.Result<System.IO.Stream>() != null);

        return default(Stream);
      }
    }

    public bool IsAuthenticated
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsLocal
    {
      get
      {
        Contract.Requires(this.LocalEndPoint != null);
        Contract.Requires(this.RemoteEndPoint != null);
        Contract.Ensures(Contract.Result<bool>() == (this.LocalEndPoint.Address.Equals(this.RemoteEndPoint.Address)));

        return default(bool);
      }
    }

    public bool IsSecureConnection
    {
      get
      {
        return default(bool);
      }
    }

    public bool KeepAlive
    {
      get
      {
        return default(bool);
      }
    }

    public IPEndPoint LocalEndPoint
    {
      get
      {
        return default(IPEndPoint);
      }
    }

    public Version ProtocolVersion
    {
      get
      {
        return default(Version);
      }
    }

    public System.Collections.Specialized.NameValueCollection QueryString
    {
      get
      {
        Contract.Requires(this.Url != null);
        Contract.Requires(this.Url.Query != null);
        Contract.Ensures(Contract.Result<System.Collections.Specialized.NameValueCollection>() != null);

        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public string RawUrl
    {
      get
      {
        return default(string);
      }
    }

    public IPEndPoint RemoteEndPoint
    {
      get
      {
        return default(IPEndPoint);
      }
    }

    public Guid RequestTraceIdentifier
    {
      get
      {
        return default(Guid);
      }
    }

    public string ServiceName
    {
      get
      {
        return default(string);
      }
      internal set
      {
      }
    }

    public TransportContext TransportContext
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Net.TransportContext>() != null);

        return default(TransportContext);
      }
    }

    public Uri Url
    {
      get
      {
        return default(Uri);
      }
    }

    public Uri UrlReferrer
    {
      get
      {
        return default(Uri);
      }
    }

    public string UserAgent
    {
      get
      {
        return default(string);
      }
    }

    public string UserHostAddress
    {
      get
      {
        Contract.Requires(this.LocalEndPoint != null);

        return default(string);
      }
    }

    public string UserHostName
    {
      get
      {
        return default(string);
      }
    }

    public string[] UserLanguages
    {
      get
      {
        return default(string[]);
      }
    }
    #endregion
  }
}

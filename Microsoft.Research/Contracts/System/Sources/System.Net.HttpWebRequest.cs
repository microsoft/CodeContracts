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

// File System.Net.HttpWebRequest.cs
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
  public partial class HttpWebRequest : WebRequest, System.Runtime.Serialization.ISerializable
  {
    #region Methods and constructors
    public override void Abort()
    {
    }

    public void AddRange(string rangeSpecifier, int from, int to)
    {
    }

    public void AddRange(string rangeSpecifier, long from, long to)
    {
    }

    public void AddRange(long from, long to)
    {
    }

    public void AddRange(int range)
    {
    }

    public void AddRange(long range)
    {
    }

    public void AddRange(int from, int to)
    {
    }

    public void AddRange(string rangeSpecifier, long range)
    {
    }

    public void AddRange(string rangeSpecifier, int range)
    {
    }

    public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public override IAsyncResult BeginGetResponse(AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public override Stream EndGetRequestStream(IAsyncResult asyncResult)
    {
      return default(Stream);
    }

    public Stream EndGetRequestStream(IAsyncResult asyncResult, out TransportContext context)
    {
      Contract.Ensures(Contract.Result<System.IO.Stream>() != null);

      context = default(TransportContext);

      return default(Stream);
    }

    public override WebResponse EndGetResponse(IAsyncResult asyncResult)
    {
      return default(WebResponse);
    }

    protected override void GetObjectData(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
    }

    public override Stream GetRequestStream()
    {
      return default(Stream);
    }

    public Stream GetRequestStream(out TransportContext context)
    {
      Contract.Ensures(Contract.Result<System.IO.Stream>() != null);

      context = default(TransportContext);

      return default(Stream);
    }

    public override WebResponse GetResponse()
    {
      return default(WebResponse);
    }

    protected HttpWebRequest(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
      Contract.Requires(serializationInfo != null);
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
    }
    #endregion

    #region Properties and indexers
    public string Accept
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Uri Address
    {
      get
      {
        return default(Uri);
      }
    }

    public bool AllowAutoRedirect
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool AllowReadStreamBuffering
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == false);

        return default(bool);
      }
      set
      {
      }
    }

    public bool AllowWriteStreamBuffering
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public DecompressionMethods AutomaticDecompression
    {
      get
      {
        return default(DecompressionMethods);
      }
      set
      {
      }
    }

    public System.Security.Cryptography.X509Certificates.X509CertificateCollection ClientCertificates
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Security.Cryptography.X509Certificates.X509CertificateCollection>() != null);

        return default(System.Security.Cryptography.X509Certificates.X509CertificateCollection);
      }
      set
      {
      }
    }

    public string Connection
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override string ConnectionGroupName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override long ContentLength
    {
      get
      {
        return default(long);
      }
      set
      {
      }
    }

    public override string ContentType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public HttpContinueDelegate ContinueDelegate
    {
      get
      {
        return default(HttpContinueDelegate);
      }
      set
      {
      }
    }

    public CookieContainer CookieContainer
    {
      get
      {
        return default(CookieContainer);
      }
      set
      {
      }
    }

    public override ICredentials Credentials
    {
      get
      {
        return default(ICredentials);
      }
      set
      {
      }
    }

    public DateTime Date
    {
      get
      {
        return default(DateTime);
      }
      set
      {
      }
    }

    public static System.Net.Cache.RequestCachePolicy DefaultCachePolicy
    {
      get
      {
        return default(System.Net.Cache.RequestCachePolicy);
      }
      set
      {
      }
    }

    public static int DefaultMaximumErrorResponseLength
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public static int DefaultMaximumResponseHeadersLength
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string Expect
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool HaveResponse
    {
      get
      {
        return default(bool);
      }
    }

    public override WebHeaderCollection Headers
    {
      get
      {
        return default(WebHeaderCollection);
      }
      set
      {
      }
    }

    public string Host
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public DateTime IfModifiedSince
    {
      get
      {
        return default(DateTime);
      }
      set
      {
      }
    }

    public bool KeepAlive
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public int MaximumAutomaticRedirections
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int MaximumResponseHeadersLength
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string MediaType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override string Method
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool Pipelined
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override bool PreAuthenticate
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public Version ProtocolVersion
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Version>() != null);

        return default(Version);
      }
      set
      {
      }
    }

    public override IWebProxy Proxy
    {
      get
      {
        return default(IWebProxy);
      }
      set
      {
      }
    }

    public int ReadWriteTimeout
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string Referer
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override Uri RequestUri
    {
      get
      {
        return default(Uri);
      }
    }

    public bool SendChunked
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public ServicePoint ServicePoint
    {
      get
      {
        return default(ServicePoint);
      }
    }

    public bool SupportsCookieContainer
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == true);

        return default(bool);
      }
    }

    public override int Timeout
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string TransferEncoding
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool UnsafeAuthenticatedConnectionSharing
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override bool UseDefaultCredentials
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string UserAgent
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
    #endregion
  }
}

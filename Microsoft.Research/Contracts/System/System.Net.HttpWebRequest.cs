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
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Contracts;

namespace System.Net {

  public class HttpWebRequest {

    public bool PreAuthenticate {
      get;
      set;
    }

    public string MediaType {
      get;
      set;
    }

    public string Referer {
      get;
      set;
    }

    extern public Uri Address {
      get;
    }

    public string ConnectionGroupName {
      get;
      set;
    }

    public string Expect {
      get;
      set;
    }

    public string Method {
      get;
      set;
    }

    public string UserAgent {
      get;
      set;
    }

    extern virtual public Uri RequestUri {
      get;
    }

    public DateTime IfModifiedSince {
      get;
      set;
    }

    public bool SendChunked {
      get;
      set;
    }

    extern public ServicePoint ServicePoint {
      get;
    }

    public WebHeaderCollection Headers {
      get;
      set;
    }

    public bool AllowAutoRedirect {
      get;
      set;
    }

    public Version ProtocolVersion {
      get;
      set;
    }

    public virtual Int64 ContentLength {
      get { return default(Int64); }
      set {
        Contract.Requires(value >= 0);
      }
    }

    public ICredentials Credentials {
      get;
      set;
    }

    public virtual int Timeout {
      get { return default(int); }
      set {
        Contract.Requires(value >= 0 || value == -1);
      }
    }

    public HttpContinueDelegate ContinueDelegate {
      get;
      set;
    }

    public string Connection {
      get;
      set;
    }

    public string ContentType {
      get;
      set;
    }

    public static int DefaultMaximumResponseHeadersLength {
      get { return default(int); }
      set {
        Contract.Requires(value >= 0 || value == -1);
      }
    }

#if NETFRAMEWORK_4_6
    public virtual bool HaveResponse {
#else
    public bool HaveResponse {
#endif
      get { return default(bool); }
    }

    public bool UnsafeAuthenticatedConnectionSharing {
      get;
      set;
    }

    public CookieContainer CookieContainer {
      get;
      set;
    }

    public string Accept {
      get;
      set;
    }

    public int MaximumResponseHeadersLength {
      get { return default(int); }
      set {
        Contract.Requires(value >= 0 || value == -1);
      }
    }

    public bool Pipelined {
      get;
      set;
    }

    public bool AllowWriteStreamBuffering {
      get;
      set;
    }

    public int ReadWriteTimeout {
      get { return default(int); }
      set {
        Contract.Requires(value >= 0 || value == -1);
      }
    }

    public bool KeepAlive {
      get;
      set;
    }

    public string TransferEncoding {
      get;
      set;
    }

    extern public System.Security.Cryptography.X509Certificates.X509CertificateCollection ClientCertificates {
      get;
    }

    public virtual IWebProxy Proxy {
      get {
        Contract.Ensures(Contract.Result<IWebProxy>() != null);
        return default(IWebProxy);
      }
      set {
        Contract.Requires(value != null);
      }
    }

    public int MaximumAutomaticRedirections {
      get { return default(int); }
      set {
        Contract.Requires(value > 0);
      }
    }

    //[Pure][Reads(ReadsAttribute.Reads.Owned)]
    //public int GetHashCode() {

    //  return default(int);
    //}
    public void AddRange(string rangeSpecifier, int range) {
      Contract.Requires(rangeSpecifier != null);

    }
    public void AddRange(string rangeSpecifier, int from, int to) {
      Contract.Requires(rangeSpecifier != null);
      Contract.Requires(from >= 0);
      Contract.Requires(to >= 0);
      Contract.Requires(from <= to);

    }
    public void AddRange(int range) {

    }
    public void AddRange(int from, int to) {

    }
    public virtual void Abort() {

    }
    public virtual WebResponse GetResponse() {

      return default(WebResponse);
    }
    public virtual WebResponse EndGetResponse(IAsyncResult asyncResult) {
      Contract.Requires(asyncResult != null);

      return default(WebResponse);
    }

    public virtual IAsyncResult BeginGetResponse(AsyncCallback callback, object state) {

      return default(IAsyncResult);
    }
    public virtual System.IO.Stream GetRequestStream() {
      Contract.Ensures(Contract.Result<System.IO.Stream>() != null);
      return default(System.IO.Stream);
    }
    public virtual System.IO.Stream EndGetRequestStream(IAsyncResult asyncResult) {
      Contract.Requires(asyncResult != null);

      return default(System.IO.Stream);
    }
    public virtual IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state) {
      return default(IAsyncResult);
    }
  }
}
#endif
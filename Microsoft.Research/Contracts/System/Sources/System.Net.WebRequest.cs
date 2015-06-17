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

// File System.Net.WebRequest.cs
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
  abstract public partial class WebRequest : MarshalByRefObject, System.Runtime.Serialization.ISerializable
  {
    #region Methods and constructors
    public virtual new void Abort()
    {
    }

    public virtual new IAsyncResult BeginGetRequestStream(AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public virtual new IAsyncResult BeginGetResponse(AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public static WebRequest Create(string requestUriString)
    {
      return default(WebRequest);
    }

    public static WebRequest Create(Uri requestUri)
    {
      return default(WebRequest);
    }

    public static WebRequest CreateDefault(Uri requestUri)
    {
      return default(WebRequest);
    }

    public static HttpWebRequest CreateHttp(string requestUriString)
    {
      return default(HttpWebRequest);
    }

    public static HttpWebRequest CreateHttp(Uri requestUri)
    {
      return default(HttpWebRequest);
    }

    public virtual new Stream EndGetRequestStream(IAsyncResult asyncResult)
    {
      return default(Stream);
    }

    public virtual new WebResponse EndGetResponse(IAsyncResult asyncResult)
    {
      return default(WebResponse);
    }

    protected virtual new void GetObjectData(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
    }

    public virtual new Stream GetRequestStream()
    {
      return default(Stream);
    }

    public virtual new WebResponse GetResponse()
    {
      return default(WebResponse);
    }

    public static IWebProxy GetSystemWebProxy()
    {
      Contract.Ensures(Contract.Result<System.Net.IWebProxy>() != null);

      return default(IWebProxy);
    }

    public static bool RegisterPrefix(string prefix, IWebRequestCreate creator)
    {
      return default(bool);
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
    }

    protected WebRequest(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
    }

    protected WebRequest()
    {
    }
    #endregion

    #region Properties and indexers
    public System.Net.Security.AuthenticationLevel AuthenticationLevel
    {
      get
      {
        return default(System.Net.Security.AuthenticationLevel);
      }
      set
      {
      }
    }

    public virtual new System.Net.Cache.RequestCachePolicy CachePolicy
    {
      get
      {
        return default(System.Net.Cache.RequestCachePolicy);
      }
      set
      {
      }
    }

    public virtual new string ConnectionGroupName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new long ContentLength
    {
      get
      {
        return default(long);
      }
      set
      {
      }
    }

    public virtual new string ContentType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new ICredentials Credentials
    {
      get
      {
        return default(ICredentials);
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

    public static IWebProxy DefaultWebProxy
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Net.IWebProxy>() != null);

        return default(IWebProxy);
      }
      set
      {
      }
    }

    public virtual new WebHeaderCollection Headers
    {
      get
      {
        return default(WebHeaderCollection);
      }
      set
      {
      }
    }

    public System.Security.Principal.TokenImpersonationLevel ImpersonationLevel
    {
      get
      {
        return default(System.Security.Principal.TokenImpersonationLevel);
      }
      set
      {
      }
    }

    public virtual new string Method
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new bool PreAuthenticate
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new IWebProxy Proxy
    {
      get
      {
        return default(IWebProxy);
      }
      set
      {
      }
    }

    public virtual new Uri RequestUri
    {
      get
      {
        return default(Uri);
      }
    }

    public virtual new int Timeout
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new bool UseDefaultCredentials
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }
    #endregion
  }
}

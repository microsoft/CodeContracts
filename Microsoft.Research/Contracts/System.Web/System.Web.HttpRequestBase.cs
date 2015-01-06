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

#if NETFRAMEWORK_4_0

// File System.Web.HttpRequestBase.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Web
{
  abstract public partial class HttpRequestBase
  {
    #region Methods and constructors
    public virtual new byte[] BinaryRead (int count)
    {
      return default(byte[]);
    }

    protected HttpRequestBase ()
    {
    }

    public virtual new int[] MapImageCoordinates (string imageFieldName)
    {
      return default(int[]);
    }

    public virtual new string MapPath (string virtualPath)
    {
      return default(string);
    }

    public virtual new string MapPath (string virtualPath, string baseVirtualDir, bool allowCrossAppMapping)
    {
      return default(string);
    }

    public virtual new void SaveAs (string filename, bool includeHeaders)
    {
    }

    public virtual new void ValidateInput ()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new string[] AcceptTypes
    {
      get
      {
        return default(string[]);
      }
    }

    public virtual new string AnonymousID
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string ApplicationPath
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string AppRelativeCurrentExecutionFilePath
    {
      get
      {
        return default(string);
      }
    }

    public virtual new HttpBrowserCapabilitiesBase Browser
    {
      get
      {
        return default(HttpBrowserCapabilitiesBase);
      }
    }

    public virtual new HttpClientCertificate ClientCertificate
    {
      get
      {
        return default(HttpClientCertificate);
      }
    }

    public virtual new Encoding ContentEncoding
    {
      get
      {
        return default(Encoding);
      }
      set
      {
      }
    }

    public virtual new int ContentLength
    {
      get
      {
        return default(int);
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

    public virtual new HttpCookieCollection Cookies
    {
      get
      {
        Contract.Ensures(Contract.Result<HttpCookieCollection>() != null);
        return default(HttpCookieCollection);
      }
    }

    public virtual new string CurrentExecutionFilePath
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string FilePath
    {
      get
      {
        return default(string);
      }
    }

    public virtual new HttpFileCollectionBase Files
    {
      get
      {
        return default(HttpFileCollectionBase);
      }
    }

    public virtual new Stream Filter
    {
      get
      {
        return default(Stream);
      }
      set
      {
      }
    }

    public virtual new System.Collections.Specialized.NameValueCollection Form
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public virtual new System.Collections.Specialized.NameValueCollection Headers
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public virtual new System.Security.Authentication.ExtendedProtection.ChannelBinding HttpChannelBinding
    {
      get
      {
        return default(System.Security.Authentication.ExtendedProtection.ChannelBinding);
      }
    }

    public virtual new string HttpMethod
    {
      get
      {
        return default(string);
      }
    }

    public virtual new Stream InputStream
    {
      get
      {
        return default(Stream);
      }
    }

    public virtual new bool IsAuthenticated
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsLocal
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsSecureConnection
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new string this [string key]
    {
      get
      {
        return default(string);
      }
    }

    public virtual new System.Security.Principal.WindowsIdentity LogonUserIdentity
    {
      get
      {
        return default(System.Security.Principal.WindowsIdentity);
      }
    }

    public virtual new System.Collections.Specialized.NameValueCollection Params
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public virtual new string Path
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string PathInfo
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string PhysicalApplicationPath
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string PhysicalPath
    {
      get
      {
        return default(string);
      }
    }

    public virtual new System.Collections.Specialized.NameValueCollection QueryString
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public virtual new string RawUrl
    {
      get
      {
        return default(string);
      }
    }

    public virtual new System.Web.Routing.RequestContext RequestContext
    {
      get
      {
        return default(System.Web.Routing.RequestContext);
      }
#if NETFRAMEWORK_4_5
#else
      internal
#endif
      set
      {
      }
    }

    public virtual new string RequestType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new System.Collections.Specialized.NameValueCollection ServerVariables
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public virtual new int TotalBytes
    {
      get
      {
        return default(int);
      }
    }

    public virtual new Uri Url
    {
      get
      {
        return default(Uri);
      }
    }

    public virtual new Uri UrlReferrer
    {
      get
      {
        return default(Uri);
      }
    }

    public virtual new string UserAgent
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string UserHostAddress
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string UserHostName
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string[] UserLanguages
    {
      get
      {
        return default(string[]);
      }
    }
    #endregion
  }
}

#endif
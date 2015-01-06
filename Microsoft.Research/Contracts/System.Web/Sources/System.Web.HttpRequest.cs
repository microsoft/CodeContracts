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

// File System.Web.HttpRequest.cs
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


namespace System.Web
{
  sealed public partial class HttpRequest
  {
    #region Methods and constructors
    public byte[] BinaryRead(int count)
    {
      return default(byte[]);
    }

    public void GetChannelBindingToken(out IntPtr token, out int size)
    {
      token = default(IntPtr);
      size = default(int);
    }

    public HttpRequest(string filename, string url, string queryString)
    {
    }

    public void InsertEntityBody(byte[] buffer, int offset, int count)
    {
    }

    public void InsertEntityBody()
    {
    }

    public int[] MapImageCoordinates(string imageFieldName)
    {
      return default(int[]);
    }

    public string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping)
    {
      return default(string);
    }

    public string MapPath(string virtualPath)
    {
      return default(string);
    }

    public void SaveAs(string filename, bool includeHeaders)
    {
    }

    public void ValidateInput()
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

    public string AnonymousID
    {
      get
      {
        return default(string);
      }
    }

    public string ApplicationPath
    {
      get
      {
        return default(string);
      }
    }

    public string AppRelativeCurrentExecutionFilePath
    {
      get
      {
        return default(string);
      }
    }

    public HttpBrowserCapabilities Browser
    {
      get
      {
        return default(HttpBrowserCapabilities);
      }
      set
      {
      }
    }

    public Stream BufferlessInputStream
    {
      get
      {
        return default(Stream);
      }
    }

    public HttpClientCertificate ClientCertificate
    {
      get
      {
        return default(HttpClientCertificate);
      }
    }

    public Encoding ContentEncoding
    {
      get
      {
        return default(Encoding);
      }
      set
      {
      }
    }

    public int ContentLength
    {
      get
      {
        return default(int);
      }
    }

    public string ContentType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public HttpCookieCollection Cookies
    {
      get
      {
        return default(HttpCookieCollection);
      }
    }

    public string CurrentExecutionFilePath
    {
      get
      {
        return default(string);
      }
    }

    public string CurrentExecutionFilePathExtension
    {
      get
      {
        return default(string);
      }
    }

    public string FilePath
    {
      get
      {
        return default(string);
      }
    }

    public HttpFileCollection Files
    {
      get
      {
        return default(HttpFileCollection);
      }
    }

    public Stream Filter
    {
      get
      {
        return default(Stream);
      }
      set
      {
      }
    }

    public System.Collections.Specialized.NameValueCollection Form
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public System.Collections.Specialized.NameValueCollection Headers
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public System.Security.Authentication.ExtendedProtection.ChannelBinding HttpChannelBinding
    {
      get
      {
        return default(System.Security.Authentication.ExtendedProtection.ChannelBinding);
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

    public string this [string key]
    {
      get
      {
        return default(string);
      }
    }

    public System.Security.Principal.WindowsIdentity LogonUserIdentity
    {
      get
      {
        return default(System.Security.Principal.WindowsIdentity);
      }
    }

    public System.Collections.Specialized.NameValueCollection Params
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public string Path
    {
      get
      {
        return default(string);
      }
    }

    public string PathInfo
    {
      get
      {
        return default(string);
      }
    }

    public string PhysicalApplicationPath
    {
      get
      {
        return default(string);
      }
    }

    public string PhysicalPath
    {
      get
      {
        return default(string);
      }
    }

    public System.Collections.Specialized.NameValueCollection QueryString
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public string RawUrl
    {
      get
      {
        return default(string);
      }
      internal set
      {
      }
    }

    public System.Web.Routing.RequestContext RequestContext
    {
      get
      {
        return default(System.Web.Routing.RequestContext);
      }
      internal set
      {
      }
    }

    public string RequestType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Collections.Specialized.NameValueCollection ServerVariables
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public int TotalBytes
    {
      get
      {
        return default(int);
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

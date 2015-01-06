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

// File System.Web.HttpRequestWrapper.cs
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
  public partial class HttpRequestWrapper : HttpRequestBase
  {
    #region Methods and constructors
    public override byte[] BinaryRead(int count)
    {
      return default(byte[]);
    }

    public HttpRequestWrapper(HttpRequest httpRequest)
    {
    }

    public override int[] MapImageCoordinates(string imageFieldName)
    {
      return default(int[]);
    }

    public override string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping)
    {
      return default(string);
    }

    public override string MapPath(string virtualPath)
    {
      return default(string);
    }

    public override void SaveAs(string filename, bool includeHeaders)
    {
    }

    public override void ValidateInput()
    {
    }
    #endregion

    #region Properties and indexers
    public override string[] AcceptTypes
    {
      get
      {
        return default(string[]);
      }
    }

    public override string AnonymousID
    {
      get
      {
        return default(string);
      }
    }

    public override string ApplicationPath
    {
      get
      {
        return default(string);
      }
    }

    public override string AppRelativeCurrentExecutionFilePath
    {
      get
      {
        return default(string);
      }
    }

    public override HttpBrowserCapabilitiesBase Browser
    {
      get
      {
        return default(HttpBrowserCapabilitiesBase);
      }
    }

    public override HttpClientCertificate ClientCertificate
    {
      get
      {
        return default(HttpClientCertificate);
      }
    }

    public override Encoding ContentEncoding
    {
      get
      {
        return default(Encoding);
      }
      set
      {
      }
    }

    public override int ContentLength
    {
      get
      {
        return default(int);
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

    public override HttpCookieCollection Cookies
    {
      get
      {
        return default(HttpCookieCollection);
      }
    }

    public override string CurrentExecutionFilePath
    {
      get
      {
        return default(string);
      }
    }

    public override string FilePath
    {
      get
      {
        return default(string);
      }
    }

    public override HttpFileCollectionBase Files
    {
      get
      {
        return default(HttpFileCollectionBase);
      }
    }

    public override Stream Filter
    {
      get
      {
        return default(Stream);
      }
      set
      {
      }
    }

    public override System.Collections.Specialized.NameValueCollection Form
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public override System.Collections.Specialized.NameValueCollection Headers
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public override System.Security.Authentication.ExtendedProtection.ChannelBinding HttpChannelBinding
    {
      get
      {
        return default(System.Security.Authentication.ExtendedProtection.ChannelBinding);
      }
    }

    public override string HttpMethod
    {
      get
      {
        return default(string);
      }
    }

    public override Stream InputStream
    {
      get
      {
        return default(Stream);
      }
    }

    public override bool IsAuthenticated
    {
      get
      {
        return default(bool);
      }
    }

    public override bool IsLocal
    {
      get
      {
        return default(bool);
      }
    }

    public override bool IsSecureConnection
    {
      get
      {
        return default(bool);
      }
    }

    public override string this [string key]
    {
      get
      {
        return default(string);
      }
    }

    public override System.Security.Principal.WindowsIdentity LogonUserIdentity
    {
      get
      {
        return default(System.Security.Principal.WindowsIdentity);
      }
    }

    public override System.Collections.Specialized.NameValueCollection Params
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public override string Path
    {
      get
      {
        return default(string);
      }
    }

    public override string PathInfo
    {
      get
      {
        return default(string);
      }
    }

    public override string PhysicalApplicationPath
    {
      get
      {
        return default(string);
      }
    }

    public override string PhysicalPath
    {
      get
      {
        return default(string);
      }
    }

    public override System.Collections.Specialized.NameValueCollection QueryString
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public override string RawUrl
    {
      get
      {
        return default(string);
      }
    }

    public override System.Web.Routing.RequestContext RequestContext
    {
      get
      {
        return default(System.Web.Routing.RequestContext);
      }
      internal set
      {
      }
    }

    public override string RequestType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override System.Collections.Specialized.NameValueCollection ServerVariables
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public override int TotalBytes
    {
      get
      {
        return default(int);
      }
    }

    public override Uri Url
    {
      get
      {
        return default(Uri);
      }
    }

    public override Uri UrlReferrer
    {
      get
      {
        return default(Uri);
      }
    }

    public override string UserAgent
    {
      get
      {
        return default(string);
      }
    }

    public override string UserHostAddress
    {
      get
      {
        return default(string);
      }
    }

    public override string UserHostName
    {
      get
      {
        return default(string);
      }
    }

    public override string[] UserLanguages
    {
      get
      {
        return default(string[]);
      }
    }
    #endregion
  }
}

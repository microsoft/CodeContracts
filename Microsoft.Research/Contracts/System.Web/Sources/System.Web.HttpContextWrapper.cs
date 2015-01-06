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

// File System.Web.HttpContextWrapper.cs
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
  public partial class HttpContextWrapper : HttpContextBase
  {
    #region Methods and constructors
    public override void AddError(Exception errorInfo)
    {
    }

    public override void ClearError()
    {
    }

    public override Object GetGlobalResourceObject(string classKey, string resourceKey)
    {
      return default(Object);
    }

    public override Object GetGlobalResourceObject(string classKey, string resourceKey, System.Globalization.CultureInfo culture)
    {
      return default(Object);
    }

    public override Object GetLocalResourceObject(string virtualPath, string resourceKey, System.Globalization.CultureInfo culture)
    {
      return default(Object);
    }

    public override Object GetLocalResourceObject(string virtualPath, string resourceKey)
    {
      return default(Object);
    }

    public override Object GetSection(string sectionName)
    {
      return default(Object);
    }

    public override Object GetService(Type serviceType)
    {
      return default(Object);
    }

    public HttpContextWrapper(HttpContext httpContext)
    {
    }

    public override void RemapHandler(IHttpHandler handler)
    {
    }

    public override void RewritePath(string path)
    {
    }

    public override void RewritePath(string path, bool rebaseClientPath)
    {
    }

    public override void RewritePath(string filePath, string pathInfo, string queryString)
    {
    }

    public override void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath)
    {
    }

    public override void SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior sessionStateBehavior)
    {
    }
    #endregion

    #region Properties and indexers
    public override Exception[] AllErrors
    {
      get
      {
        return default(Exception[]);
      }
    }

    public override HttpApplicationStateBase Application
    {
      get
      {
        return default(HttpApplicationStateBase);
      }
    }

    public override HttpApplication ApplicationInstance
    {
      get
      {
        return default(HttpApplication);
      }
      set
      {
      }
    }

    public override System.Web.Caching.Cache Cache
    {
      get
      {
        return default(System.Web.Caching.Cache);
      }
    }

    public override IHttpHandler CurrentHandler
    {
      get
      {
        return default(IHttpHandler);
      }
    }

    public override RequestNotification CurrentNotification
    {
      get
      {
        return default(RequestNotification);
      }
    }

    public override Exception Error
    {
      get
      {
        return default(Exception);
      }
    }

    public override IHttpHandler Handler
    {
      get
      {
        return default(IHttpHandler);
      }
      set
      {
      }
    }

    public override bool IsCustomErrorEnabled
    {
      get
      {
        return default(bool);
      }
    }

    public override bool IsDebuggingEnabled
    {
      get
      {
        return default(bool);
      }
    }

    public override bool IsPostNotification
    {
      get
      {
        return default(bool);
      }
    }

    public override System.Collections.IDictionary Items
    {
      get
      {
        return default(System.Collections.IDictionary);
      }
    }

    public override IHttpHandler PreviousHandler
    {
      get
      {
        return default(IHttpHandler);
      }
    }

    public override System.Web.Profile.ProfileBase Profile
    {
      get
      {
        return default(System.Web.Profile.ProfileBase);
      }
    }

    public override HttpRequestBase Request
    {
      get
      {
        return default(HttpRequestBase);
      }
    }

    public override HttpResponseBase Response
    {
      get
      {
        return default(HttpResponseBase);
      }
    }

    public override HttpServerUtilityBase Server
    {
      get
      {
        return default(HttpServerUtilityBase);
      }
    }

    public override HttpSessionStateBase Session
    {
      get
      {
        return default(HttpSessionStateBase);
      }
    }

    public override bool SkipAuthorization
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override DateTime Timestamp
    {
      get
      {
        return default(DateTime);
      }
    }

    public override TraceContext Trace
    {
      get
      {
        return default(TraceContext);
      }
    }

    public override System.Security.Principal.IPrincipal User
    {
      get
      {
        return default(System.Security.Principal.IPrincipal);
      }
      set
      {
      }
    }
    #endregion
  }
}

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

// File System.Web.HttpContext.cs
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
  sealed public partial class HttpContext : IServiceProvider
  {
    #region Methods and constructors
    public void AddError(Exception errorInfo)
    {
    }

    public void ClearError()
    {
    }

    public static Object GetAppConfig(string name)
    {
      return default(Object);
    }

    public Object GetConfig(string name)
    {
      return default(Object);
    }

    public static Object GetGlobalResourceObject(string classKey, string resourceKey)
    {
      return default(Object);
    }

    public static Object GetGlobalResourceObject(string classKey, string resourceKey, System.Globalization.CultureInfo culture)
    {
      return default(Object);
    }

    public static Object GetLocalResourceObject(string virtualPath, string resourceKey, System.Globalization.CultureInfo culture)
    {
      return default(Object);
    }

    public static Object GetLocalResourceObject(string virtualPath, string resourceKey)
    {
      return default(Object);
    }

    public Object GetSection(string sectionName)
    {
      return default(Object);
    }

    public HttpContext(HttpRequest request, HttpResponse response)
    {
    }

    public HttpContext(HttpWorkerRequest wr)
    {
    }

    public void RemapHandler(IHttpHandler handler)
    {
    }

    public void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath)
    {
    }

    public void RewritePath(string path, bool rebaseClientPath)
    {
    }

    public void RewritePath(string filePath, string pathInfo, string queryString)
    {
    }

    public void RewritePath(string path)
    {
    }

    public void SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior sessionStateBehavior)
    {
    }

    Object System.IServiceProvider.GetService(Type service)
    {
      return default(Object);
    }
    #endregion

    #region Properties and indexers
    public Exception[] AllErrors
    {
      get
      {
        return default(Exception[]);
      }
    }

    public HttpApplicationState Application
    {
      get
      {
        return default(HttpApplicationState);
      }
    }

    public HttpApplication ApplicationInstance
    {
      get
      {
        return default(HttpApplication);
      }
      set
      {
      }
    }

    public System.Web.Caching.Cache Cache
    {
      get
      {
        return default(System.Web.Caching.Cache);
      }
    }

    public static System.Web.HttpContext Current
    {
      get
      {
        return default(System.Web.HttpContext);
      }
      set
      {
      }
    }

    public IHttpHandler CurrentHandler
    {
      get
      {
        return default(IHttpHandler);
      }
    }

    public RequestNotification CurrentNotification
    {
      get
      {
        return default(RequestNotification);
      }
      internal set
      {
      }
    }

    public Exception Error
    {
      get
      {
        return default(Exception);
      }
    }

    public IHttpHandler Handler
    {
      get
      {
        return default(IHttpHandler);
      }
      set
      {
      }
    }

    public bool IsCustomErrorEnabled
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsDebuggingEnabled
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsPostNotification
    {
      get
      {
        return default(bool);
      }
      internal set
      {
      }
    }

    public System.Collections.IDictionary Items
    {
      get
      {
        return default(System.Collections.IDictionary);
      }
    }

    public IHttpHandler PreviousHandler
    {
      get
      {
        return default(IHttpHandler);
      }
    }

    public System.Web.Profile.ProfileBase Profile
    {
      get
      {
        return default(System.Web.Profile.ProfileBase);
      }
    }

    public HttpRequest Request
    {
      get
      {
        return default(HttpRequest);
      }
    }

    public HttpResponse Response
    {
      get
      {
        return default(HttpResponse);
      }
    }

    public HttpServerUtility Server
    {
      get
      {
        return default(HttpServerUtility);
      }
    }

    public System.Web.SessionState.HttpSessionState Session
    {
      get
      {
        return default(System.Web.SessionState.HttpSessionState);
      }
    }

    public bool SkipAuthorization
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public DateTime Timestamp
    {
      get
      {
        return default(DateTime);
      }
    }

    public TraceContext Trace
    {
      get
      {
        return default(TraceContext);
      }
    }

    public System.Security.Principal.IPrincipal User
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

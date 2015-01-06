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

// File System.Web.HttpContextBase.cs
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
  abstract public partial class HttpContextBase : IServiceProvider
  {
    #region Methods and constructors
    public virtual new void AddError(Exception errorInfo)
    {
    }

    public virtual new void ClearError()
    {
    }

    public virtual new Object GetGlobalResourceObject(string classKey, string resourceKey, System.Globalization.CultureInfo culture)
    {
      return default(Object);
    }

    public virtual new Object GetGlobalResourceObject(string classKey, string resourceKey)
    {
      return default(Object);
    }

    public virtual new Object GetLocalResourceObject(string virtualPath, string resourceKey)
    {
      return default(Object);
    }

    public virtual new Object GetLocalResourceObject(string virtualPath, string resourceKey, System.Globalization.CultureInfo culture)
    {
      return default(Object);
    }

    public virtual new Object GetSection(string sectionName)
    {
      return default(Object);
    }

    public virtual new Object GetService(Type serviceType)
    {
      return default(Object);
    }

    protected HttpContextBase()
    {
    }

    public virtual new void RemapHandler(IHttpHandler handler)
    {
    }

    public virtual new void RewritePath(string path)
    {
    }

    public virtual new void RewritePath(string path, bool rebaseClientPath)
    {
    }

    public virtual new void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath)
    {
    }

    public virtual new void RewritePath(string filePath, string pathInfo, string queryString)
    {
    }

    public virtual new void SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior sessionStateBehavior)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new Exception[] AllErrors
    {
      get
      {
        return default(Exception[]);
      }
    }

    public virtual new HttpApplicationStateBase Application
    {
      get
      {
        return default(HttpApplicationStateBase);
      }
    }

    public virtual new HttpApplication ApplicationInstance
    {
      get
      {
        return default(HttpApplication);
      }
      set
      {
      }
    }

    public virtual new System.Web.Caching.Cache Cache
    {
      get
      {
        return default(System.Web.Caching.Cache);
      }
    }

    public virtual new IHttpHandler CurrentHandler
    {
      get
      {
        return default(IHttpHandler);
      }
    }

    public virtual new RequestNotification CurrentNotification
    {
      get
      {
        return default(RequestNotification);
      }
    }

    public virtual new Exception Error
    {
      get
      {
        return default(Exception);
      }
    }

    public virtual new IHttpHandler Handler
    {
      get
      {
        return default(IHttpHandler);
      }
      set
      {
      }
    }

    public virtual new bool IsCustomErrorEnabled
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsDebuggingEnabled
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsPostNotification
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new System.Collections.IDictionary Items
    {
      get
      {
        return default(System.Collections.IDictionary);
      }
    }

    public virtual new IHttpHandler PreviousHandler
    {
      get
      {
        return default(IHttpHandler);
      }
    }

    public virtual new System.Web.Profile.ProfileBase Profile
    {
      get
      {
        return default(System.Web.Profile.ProfileBase);
      }
    }

    public virtual new HttpRequestBase Request
    {
      get
      {
        return default(HttpRequestBase);
      }
    }

    public virtual new HttpResponseBase Response
    {
      get
      {
        return default(HttpResponseBase);
      }
    }

    public virtual new HttpServerUtilityBase Server
    {
      get
      {
        return default(HttpServerUtilityBase);
      }
    }

    public virtual new HttpSessionStateBase Session
    {
      get
      {
        return default(HttpSessionStateBase);
      }
    }

    public virtual new bool SkipAuthorization
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new DateTime Timestamp
    {
      get
      {
        return default(DateTime);
      }
    }

    public virtual new TraceContext Trace
    {
      get
      {
        return default(TraceContext);
      }
    }

    public virtual new System.Security.Principal.IPrincipal User
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

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

// File System.Web.HttpApplication.cs
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
#pragma warning disable 0067
// Disable the "this event is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Web
{
  public partial class HttpApplication : IHttpAsyncHandler, IHttpHandler, System.ComponentModel.IComponent, IDisposable
  {
    #region Methods and constructors
    public void AddOnAcquireRequestStateAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnAcquireRequestStateAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnAuthenticateRequestAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnAuthenticateRequestAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnAuthorizeRequestAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnAuthorizeRequestAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnBeginRequestAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnBeginRequestAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnEndRequestAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnEndRequestAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnLogRequestAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnLogRequestAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnMapRequestHandlerAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnMapRequestHandlerAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnPostAcquireRequestStateAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnPostAcquireRequestStateAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnPostAuthenticateRequestAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnPostAuthenticateRequestAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnPostAuthorizeRequestAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnPostAuthorizeRequestAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnPostLogRequestAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnPostLogRequestAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnPostMapRequestHandlerAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnPostMapRequestHandlerAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnPostReleaseRequestStateAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnPostReleaseRequestStateAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnPostRequestHandlerExecuteAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnPostRequestHandlerExecuteAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnPostResolveRequestCacheAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnPostResolveRequestCacheAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnPostUpdateRequestCacheAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnPostUpdateRequestCacheAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnPreRequestHandlerExecuteAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnPreRequestHandlerExecuteAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnReleaseRequestStateAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnReleaseRequestStateAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnResolveRequestCacheAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnResolveRequestCacheAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void AddOnUpdateRequestCacheAsync (BeginEventHandler bh, EndEventHandler eh)
    {
    }

    public void AddOnUpdateRequestCacheAsync (BeginEventHandler beginHandler, EndEventHandler endHandler, Object state)
    {
    }

    public void CompleteRequest ()
    {
    }

    public virtual new void Dispose ()
    {
    }
#if NETFRAMEWORK_4_0
    public virtual new string GetOutputCacheProviderName (HttpContext context)
    {
      return default(string);
    }
#endif

    public virtual new string GetVaryByCustomString (HttpContext context, string custom)
    {
      Contract.Requires (context != null);

      return default(string);
    }

    public HttpApplication ()
    {
    }

    public virtual new void Init ()
    {
    }

    IAsyncResult System.Web.IHttpAsyncHandler.BeginProcessRequest (HttpContext context, AsyncCallback cb, Object extraData)
    {
      return default(IAsyncResult);
    }

    void System.Web.IHttpAsyncHandler.EndProcessRequest (IAsyncResult result)
    {
    }

    void System.Web.IHttpHandler.ProcessRequest (HttpContext context)
    {
    }
    #endregion

    #region Properties and indexers
    public HttpApplicationState Application
    {
      get
      {
        return default(HttpApplicationState);
      }
    }

    public HttpContext Context
    {
      get
      {
        Contract.Ensures(Contract.Result<HttpContext>() != null);

        return default(HttpContext);
      }
    }

    protected System.ComponentModel.EventHandlerList Events
    {
      get
      {
        Contract.Ensures (Contract.Result<System.ComponentModel.EventHandlerList>() != null);

        return default(System.ComponentModel.EventHandlerList);
      }
    }

    public HttpModuleCollection Modules
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.HttpModuleCollection>() != null);

        return default(HttpModuleCollection);
      }
    }

    public HttpRequest Request
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.HttpRequest>() != null);

        return default(HttpRequest);
      }
    }

    public HttpResponse Response
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.HttpResponse>() != null);

        return default(HttpResponse);
      }
    }

    public HttpServerUtility Server
    {
      get
      {
        Contract.Ensures(Contract.Result<HttpServerUtility>() != null);

        return default(HttpServerUtility);
      }
    }

    public System.Web.SessionState.HttpSessionState Session
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.SessionState.HttpSessionState>() != null);

        return default(System.Web.SessionState.HttpSessionState);
      }
    }

    public System.ComponentModel.ISite Site
    {
      get
      {
        return default(System.ComponentModel.ISite);
      }
      set
      {
      }
    }

    bool System.Web.IHttpHandler.IsReusable
    {
      get
      {
        return default(bool);
      }
    }

    public System.Security.Principal.IPrincipal User
    {
      get
      {
        return default(System.Security.Principal.IPrincipal);
      }
    }
    #endregion

    #region IComponent Members

    //extern event EventHandler Disposed;

    #endregion

    #region IComponent Members

    extern public event EventHandler Disposed;

    #endregion
  }
}

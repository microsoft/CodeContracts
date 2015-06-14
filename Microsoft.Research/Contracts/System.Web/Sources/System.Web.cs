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

// File System.Web.cs
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
  public enum ApplicationShutdownReason
  {
    None = 0, 
    HostingEnvironment = 1, 
    ChangeInGlobalAsax = 2, 
    ConfigurationChange = 3, 
    UnloadAppDomainCalled = 4, 
    ChangeInSecurityPolicyFile = 5, 
    BinDirChangeOrDirectoryRename = 6, 
    BrowsersDirChangeOrDirectoryRename = 7, 
    CodeDirChangeOrDirectoryRename = 8, 
    ResourcesDirChangeOrDirectoryRename = 9, 
    IdleTimeout = 10, 
    PhysicalApplicationPathChanged = 11, 
    HttpRuntimeClose = 12, 
    InitializationError = 13, 
    MaxRecompilationsReached = 14, 
    BuildManagerChange = 15, 
  }

  public delegate IAsyncResult BeginEventHandler(Object sender, EventArgs e, AsyncCallback cb, Object extraData);

  public delegate void EndEventHandler(IAsyncResult ar);

  public enum HttpCacheability
  {
    NoCache = 1, 
    Private = 2, 
    Server = 3, 
    ServerAndNoCache = 3, 
    Public = 4, 
    ServerAndPrivate = 5, 
  }

  public enum HttpCacheRevalidation
  {
    AllCaches = 1, 
    ProxyCaches = 2, 
    None = 3, 
  }

  public delegate void HttpCacheValidateHandler(HttpContext context, Object data, ref HttpValidationStatus validationStatus);

  public enum HttpCookieMode
  {
    UseUri = 0, 
    UseCookies = 1, 
    AutoDetect = 2, 
    UseDeviceProfile = 3, 
  }

  public delegate string HttpResponseSubstitutionCallback(HttpContext context);

  public enum HttpValidationStatus
  {
    Invalid = 1, 
    IgnoreThisRequest = 2, 
    Valid = 3, 
  }

  public enum ProcessShutdownReason
  {
    None = 0, 
    Unexpected = 1, 
    RequestsLimit = 2, 
    RequestQueueLimit = 3, 
    Timeout = 4, 
    IdleTimeout = 5, 
    MemoryLimitExceeded = 6, 
    PingFailed = 7, 
    DeadlockSuspected = 8, 
  }

  public enum ProcessStatus
  {
    Alive = 1, 
    ShuttingDown = 2, 
    ShutDown = 3, 
    Terminated = 4, 
  }

  public enum RequestNotification
  {
    BeginRequest = 1, 
    AuthenticateRequest = 2, 
    AuthorizeRequest = 4, 
    ResolveRequestCache = 8, 
    MapRequestHandler = 16, 
    AcquireRequestState = 32, 
    PreExecuteRequestHandler = 64, 
    ExecuteRequestHandler = 128, 
    ReleaseRequestState = 256, 
    UpdateRequestCache = 512, 
    LogRequest = 1024, 
    EndRequest = 2048, 
    SendResponse = 536870912, 
  }

  public enum RequestNotificationStatus
  {
    Continue = 0, 
    Pending = 1, 
    FinishRequest = 2, 
  }

  public delegate SiteMapNode SiteMapResolveEventHandler(Object sender, SiteMapResolveEventArgs e);

  public delegate void TraceContextEventHandler(Object sender, TraceContextEventArgs e);

  public enum TraceMode
  {
    SortByTime = 0, 
    SortByCategory = 1, 
    Default = 2, 
  }
}

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

// File System.Web.Hosting.HostingEnvironment.cs
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


namespace System.Web.Hosting
{
  sealed public partial class HostingEnvironment : MarshalByRefObject
  {
    #region Methods and constructors
    public static void DecrementBusyCount()
    {
    }

    public HostingEnvironment()
    {
    }

    public static IDisposable Impersonate(IntPtr userToken, string virtualPath)
    {
      return default(IDisposable);
    }

    public static IDisposable Impersonate(IntPtr token)
    {
      return default(IDisposable);
    }

    public static IDisposable Impersonate()
    {
      return default(IDisposable);
    }

    public static void IncrementBusyCount()
    {
    }

    public override Object InitializeLifetimeService()
    {
      return default(Object);
    }

    public static void InitiateShutdown()
    {
    }

    public static string MapPath(string virtualPath)
    {
      return default(string);
    }

    public static void MessageReceived()
    {
    }

    public static void RegisterObject(IRegisteredObject obj)
    {
    }

    public static void RegisterVirtualPathProvider(VirtualPathProvider virtualPathProvider)
    {
    }

    public static IDisposable SetCultures(string virtualPath)
    {
      return default(IDisposable);
    }

    public static IDisposable SetCultures()
    {
      return default(IDisposable);
    }

    public static void UnregisterObject(IRegisteredObject obj)
    {
    }
    #endregion

    #region Properties and indexers
    public static IApplicationHost ApplicationHost
    {
      get
      {
        return default(IApplicationHost);
      }
    }

    public static string ApplicationID
    {
      get
      {
        return default(string);
      }
    }

    public static string ApplicationPhysicalPath
    {
      get
      {
        return default(string);
      }
    }

    public static string ApplicationVirtualPath
    {
      get
      {
        return default(string);
      }
    }

    public static System.Web.Caching.Cache Cache
    {
      get
      {
        return default(System.Web.Caching.Cache);
      }
    }

    public static bool InClientBuildManager
    {
      get
      {
        return default(bool);
      }
    }

    public static Exception InitializationException
    {
      get
      {
        return default(Exception);
      }
    }

    public static bool IsHosted
    {
      get
      {
        return default(bool);
      }
    }

    public static int MaxConcurrentRequestsPerCPU
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public static int MaxConcurrentThreadsPerCPU
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public static System.Web.ApplicationShutdownReason ShutdownReason
    {
      get
      {
        return default(System.Web.ApplicationShutdownReason);
      }
    }

    public static string SiteName
    {
      get
      {
        return default(string);
      }
    }

    public static VirtualPathProvider VirtualPathProvider
    {
      get
      {
        return default(VirtualPathProvider);
      }
    }
    #endregion
  }
}

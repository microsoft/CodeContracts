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

// File System.Web.HttpRuntime.cs
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
  sealed public partial class HttpRuntime
  {
    #region Methods and constructors
    public static void Close()
    {
    }

    public static System.Security.NamedPermissionSet GetNamedPermissionSet()
    {
      return default(System.Security.NamedPermissionSet);
    }

    public HttpRuntime()
    {
    }

    public static void ProcessRequest(HttpWorkerRequest wr)
    {
    }

    public static void UnloadAppDomain()
    {
    }
    #endregion

    #region Properties and indexers
    public static string AppDomainAppId
    {
      get
      {
        return default(string);
      }
    }

    public static string AppDomainAppPath
    {
      get
      {
        return default(string);
      }
    }

    public static string AppDomainAppVirtualPath
    {
      get
      {
        return default(string);
      }
    }

    public static string AppDomainId
    {
      get
      {
        return default(string);
      }
    }

    public static string AspClientScriptPhysicalPath
    {
      get
      {
        return default(string);
      }
    }

    public static string AspClientScriptVirtualPath
    {
      get
      {
        return default(string);
      }
    }

    public static string AspInstallDirectory
    {
      get
      {
        return default(string);
      }
    }

    public static string BinDirectory
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

    public static string ClrInstallDirectory
    {
      get
      {
        return default(string);
      }
    }

    public static string CodegenDir
    {
      get
      {
        return default(string);
      }
    }

    public static bool IsOnUNCShare
    {
      get
      {
        return default(bool);
      }
    }

    public static string MachineConfigurationDirectory
    {
      get
      {
        return default(string);
      }
    }

    public static bool UsingIntegratedPipeline
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}

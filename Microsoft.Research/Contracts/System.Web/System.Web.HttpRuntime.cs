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

#region Assembly System.Web.dll, v4.0.30319
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Web.dll
#endregion

using System;
using System.Configuration;
using System.Data.Common;
using System.Globalization;
using System.Runtime;
using System.Security;
using System.Security.Policy;
using System.Web.Caching;
//using System.Web.Compilation;
//using System.Web.Hosting;
//using System.Web.Util;
using System.Xml;
using System.Diagnostics.Contracts;

namespace System.Web
{
  // Summary:
  //     Provides a set of ASP.NET run-time services for the current application.
  public sealed class HttpRuntime
  {
    // Summary:
    //     Initializes a new instance of the System.Web.HttpRuntime class.
    public extern HttpRuntime();

    // Summary:
    //     Gets the application identification of the application domain where the System.Web.HttpRuntime
    //     exists.
    //
    // Returns:
    //     The application identification of the application domain where the System.Web.HttpRuntime
    //     exists.
    public static string AppDomainAppId
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the physical drive path of the application directory for the application
    //     hosted in the current application domain.
    //
    // Returns:
    //     The physical drive path of the application directory for the application
    //     hosted in the current application domain.
    public static string AppDomainAppPath
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the virtual path of the directory that contains the application hosted
    //     in the current application domain.
    //
    // Returns:
    //     The virtual path of the directory that contains the application hosted in
    //     the current application domain.
    public static string AppDomainAppVirtualPath
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the domain identification of the application domain where the System.Web.HttpRuntime
    //     instance exists.
    //
    // Returns:
    //     The unique application domain identifier.
    public static string AppDomainId
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the folder path for the ASP.NET client script files.
    //
    // Returns:
    //     The folder path for the ASP.NET client script files.
    //
    // Exceptions:
    //   System.Web.HttpException:
    //     ASP.NET is not installed.
    public static string AspClientScriptPhysicalPath
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the virtual path for the ASP.NET client script files.
    //
    // Returns:
    //     The virtual path for the ASP.NET client script files.
    public static string AspClientScriptVirtualPath
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the physical path of the directory where the ASP.NET executable files
    //     are installed.
    //
    // Returns:
    //     The physical path to the ASP.NET executable files.
    //
    // Exceptions:
    //   System.Web.HttpException:
    //     ASP.NET is not installed on this computer.
    public static string AspInstallDirectory
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the physical path to the /bin directory for the current application.
    //
    // Returns:
    //     The path to the current application's /bin directory.
    public static string BinDirectory
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the System.Web.Caching.Cache for the current application.
    //
    // Returns:
    //     The current System.Web.Caching.Cache.
    //
    // Exceptions:
    //   System.Web.HttpException:
    //     ASP.NET is not installed.
    public static Cache Cache
    {
      get
      {
        Contract.Ensures(Contract.Result<Cache>() != null);
        return default(Cache);
      }
    }
    //
    // Summary:
    //     Gets the physical path to the directory where the common language runtime
    //     executable files are installed.
    //
    // Returns:
    //     The physical path to the common language runtime executable files.
    public static string ClrInstallDirectory
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the physical path to the directory where ASP.NET stores temporary files
    //     (generated sources, compiled assemblies, and so on) for the current application.
    //
    // Returns:
    //     The physical path to the application's temporary file storage directory.
    public static string CodegenDir
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets a value that indicates whether the application is mapped to a universal
    //     naming convention (UNC) share.
    //
    // Returns:
    //     true if the application is mapped to a UNC share; otherwise, false.
    public extern static bool IsOnUNCShare { get; }
    //
    // Summary:
    //     Gets the physical path to the directory where the Machine.config file for
    //     the current application is located.
    //
    // Returns:
    //     The physical path to the Machine.config file for the current application.
    public static string MachineConfigurationDirectory
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets a value that indicates whether the current application is running in
    //     the integrated-pipeline mode of IIS 7.0.
    //
    // Returns:
    //     true if the application is running in integrated-pipeline mode; otherwise,
    //     false.
    extern public static bool UsingIntegratedPipeline { get; }

    // Summary:
    //     Removes all items from the cache.
    extern public static void Close();
    //
    // Summary:
    //     Returns the set of permissions associated with code groups.
    //
    // Returns:
    //     A System.Security.NamedPermissionSet object containing the names and descriptions
    //     of permissions, or null if none exists.
    extern public static NamedPermissionSet GetNamedPermissionSet();
    //
    // Summary:
    //     Drives all ASP.NET Web processing execution.
    //
    // Parameters:
    //   wr:
    //     An System.Web.HttpWorkerRequest for the current application.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The wr parameter is null.
    //
    //   System.PlatformNotSupportedException:
    //     The Web application is running under IIS 7 in Integrated mode.
    public static void ProcessRequest(HttpWorkerRequest wr)
    {
      Contract.Requires(wr != null);
    }
    //
    // Summary:
    //     Terminates the current application. The application restarts the next time
    //     a request is received for it.
    extern public static void UnloadAppDomain();
  }
}

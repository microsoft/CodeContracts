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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics.Contracts;

namespace System.Windows
{
  // Summary:
  //     Stores a collection of System.Windows.AssemblyPart objects. Provides collection
  //     support for the System.Windows.Deployment.Parts property.
  public sealed class AssemblyPartCollection
  {
  }

  // Summary:
  //     Provides application part and localization information in the application
  //     manifest when deploying a Silverlight-based application.
  public sealed class Deployment : DependencyObject
  {
    // Summary:
    //     Identifies the System.Windows.Deployment.EntryPointAssembly dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.Deployment.EntryPointAssembly dependency
    //     property.
    //public static readonly DependencyProperty EntryPointAssemblyProperty;
    //
    // Summary:
    //     Identifies the System.Windows.Deployment.EntryPointType dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.Deployment.EntryPointType dependency
    //     property.
    //public static readonly DependencyProperty EntryPointTypeProperty;
    //
    // Summary:
    //     Identifies the System.Windows.Deployment.ExternalCallersFromCrossDomain dependency
    //     property.
    //
    // Returns:
    //     The identifier for the System.Windows.Deployment.ExternalCallersFromCrossDomain
    //     dependency property.
    //public static readonly DependencyProperty ExternalCallersFromCrossDomainProperty;
    //
    // Summary:
    //     Identifies the System.Windows.Deployment.Parts dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.Deployment.Parts dependency property.
    //public static readonly DependencyProperty PartsProperty;
    //
    // Summary:
    //     Identifies the System.Windows.Deployment.RuntimeVersion dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.Deployment.RuntimeVersion dependency
    //     property.
    //public static readonly DependencyProperty RuntimeVersionProperty;


    // Summary:
    //     Gets the current System.Windows.Deployment object.
    //
    // Returns:
    //     The current System.Windows.Deployment object.
    public static Deployment Current
    {
      get
      {
        Contract.Ensures(Contract.Result<Deployment>() != null);
        return default(Deployment);
      }
    }
    //
    // Summary:
    //     Gets a string name that identifies which part in the System.Windows.Deployment
    //     is the entry point assembly.
    //
    // Returns:
    //     A string that names the assembly that should be used as the entry point assembly.
    //     This is expected to be the name of one of the assemblies you specified as
    //     an System.Windows.AssemblyPart.
    public string EntryPointAssembly
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets a string that identifies the namespace and type name of the class that
    //     contains the System.Windows.Application entry point for your application.
    //
    // Returns:
    //     The namespace and type name of the class that contains the System.Windows.Application
    //     entry point.
    public string EntryPointType
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets a value that indicates the level of access that cross-domain callers
    //     have to the Silverlight-based application in this deployment.
    //
    // Returns:
    //     A value that indicates the access level of cross-domain callers.
    // public CrossDomainAccess ExternalCallersFromCrossDomain { get; }
    //
    // Summary:
    //     Gets a collection of assembly parts that are included in the deployment.
    //
    // Returns:
    //     The collection of assembly parts. The default is an empty collection.
    public AssemblyPartCollection Parts
    {
      get
      {
        Contract.Ensures(Contract.Result<AssemblyPartCollection>() != null);
        return default(AssemblyPartCollection);
      }
    }
    //
    // Summary:
    //     Gets the Silverlight runtime version that this deployment supports.
    //
    // Returns:
    //     The Silverlight runtime version that this deployment supports.
    public string RuntimeVersion
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

#if !SILVERLIGHT_4_0_WP
    // Summary:
    //     [SECURITY CRITICAL] Enables a native Silverlight host, such as Expression
    //     Blend or Visual Studio, to instruct Silverlight to register an assembly that
    //     the Silverlight host has separately loaded into the host-managed application
    //     domain in which a Silverlight application is running.
    //
    // Parameters:
    //   assembly:
    //     The assembly that the Silverlight host has separately loaded.
    public static void RegisterAssembly(Assembly assembly)
    {
      Contract.Requires(assembly != null);
    }
#endif

    //
    // Summary:
    //     [SECURITY CRITICAL] Allows a native host of the Silverlight plug-in to specify
    //     the current System.Windows.Application object of the running Silverlight
    //     application.
    //
    // Parameters:
    //   application:
    //     The System.Windows.Application object that the native host is setting as
    //     the current System.Windows.Application.

    // public static void SetCurrentApplication(System.Net.Mime.MediaTypeNames.Application application);
  }

}

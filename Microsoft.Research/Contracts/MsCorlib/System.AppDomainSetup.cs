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

using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System
{
  // Summary:
  //     Represents assembly binding information that can be added to an instance
  //     of System.AppDomain.
  public sealed class AppDomainSetup
  {
#if false
    public AppDomainInitializer AppDomainInitializer { get; set; }
#endif
    //
    // Summary:
    //     Gets or sets the arguments passed to the callback method represented by the
    //     System.AppDomainInitializer delegate. The callback method is invoked when
    //     the application domain is initialized.
    //
    // Returns:
    //     An array of strings that is passed to the callback method represented by
    //     the System.AppDomainInitializer delegate, when the callback method is invoked
    //     during System.AppDomain initialization.
    // public string[] AppDomainInitializerArguments { get; set; }
    //
    //
    // Summary:
    //     Gets or sets an object containing security and trust information.
    //
    // Returns:
    //     An System.Security.Policy.ApplicationTrust object representing security and
    //     trust information.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The property is set to an System.Security.Policy.ApplicationTrust object
    //     whose application identity does not match the application identity of the
    //     System.Runtime.Hosting.ActivationArguments object returned by the System.AppDomainSetup.ActivationArguments
    //     property. No exception is thrown if the System.AppDomainSetup.ActivationArguments
    //     property is null.
    //
    //   System.ArgumentNullException:
    //     The property is set to null.
    // extern public ApplicationTrust ApplicationTrust { get; set; }

#if !SILVERLIGHT
    //
    // Summary:
    //     Specifies whether the application base path and private binary path are probed
    //     when searching for assemblies to load.
    //
    // Returns:
    //     true if probing is not allowed; otherwise false. The default is false.
    extern public bool DisallowApplicationBaseProbing
    {
      get;
      set;
    }
#endif

#if !SILVERLIGHT
    //
    // Summary:
    //     Gets or sets a value indicating if an application domain allows assembly
    //     binding redirection.
    //
    // Returns:
    //     true if redirection of assemblies is disallowed; otherwise it is allowed.
    extern public bool DisallowBindingRedirects { get; set; }
#endif

#if !SILVERLIGHT
    //
    // Summary:
    //     Gets or sets a value indicating whether HTTP download of assemblies is allowed
    //     for an application domain.
    //
    // Returns:
    //     true if HTTP download of assemblies is to be disallowed; otherwise, it is
    //     allowed.
    extern public bool DisallowCodeDownload { get; set; }
#endif

#if !SILVERLIGHT
    //
    // Summary:
    //     Gets or sets a value indicating whether the publisher policy section of the
    //     configuration file is applied to an application domain.
    //
    // Returns:
    //     true if the publisherPolicy section of the configuration file for an application
    //     domain is ignored; otherwise, the declared publisher policy is honored.
    extern public bool DisallowPublisherPolicy { get; set; }
#endif

    //
    // Summary:
    //     Specifies the optimization policy used to load an executable.
    //
    // Returns:
    //     A System.LoaderOptimization enumerated constant used with the System.LoaderOptimizationAttribute.
#if false
    public virtual  LoaderOptimization LoaderOptimization { get; set; }
#endif

#if !SILVERLIGHT
    //
    // Summary:
    //     Gets or sets a value indicating whether interface caching is disabled for
    //     interop calls in the application domain, so that a QueryInterface is performed
    //     on each call.
    //
    // Returns:
    //     true if interface caching is disabled for interop calls in application domains
    //     created with the current System.AppDomainSetup object; otherwise, false.
    extern public bool SandboxInterop { get; set; }
#endif

#if !SILVERLIGHT

    // Summary:
    //     Gets the XML configuration information set by the System.AppDomainSetup.SetConfigurationBytes(System.Byte[])
    //     method, which overrides the application's XML configuration information.
    //
    // Returns:
    //     A System.Byte array containing the XML configuration information that was
    //     set by the System.AppDomainSetup.SetConfigurationBytes(System.Byte[]) method,
    //     or null if the System.AppDomainSetup.SetConfigurationBytes(System.Byte[])
    //     method has not been called.
    extern public byte[] GetConfigurationBytes();

    //
    // Summary:
    //     Provides XML configuration information for the application domain, overriding
    //     the application's XML configuration information.
    //
    // Parameters:
    //   value:
    //     A System.Byte array containing the XML configuration information to be used
    //     for the application domain.
    extern public void SetConfigurationBytes(byte[] value);
#endif
  }

#if !SILVERLIGHT
  [ContractClass(typeof(IAppDomainSetupContract))]
  public interface IAppDomainSetup
  {
    string ApplicationBase { get; set; }
    string ApplicationName { get; set; }
    string CachePath { get; set; }
    string ConfigurationFile { get; set; }
    string DynamicBase { get; set; }
    string LicenseFile { get; set; }
    string PrivateBinPath { get; set; }
    string PrivateBinPathProbe { get; set; }
    string ShadowCopyDirectories { get; set; }
    string ShadowCopyFiles { get; set; }


  }

  [ContractClassFor(typeof(IAppDomainSetup))]
  abstract class IAppDomainSetupContract : IAppDomainSetup
  {
    #region IAppDomainSetup Members

    string IAppDomainSetup.ApplicationBase
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    string IAppDomainSetup.ApplicationName
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    string IAppDomainSetup.CachePath
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    string IAppDomainSetup.ConfigurationFile
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    string IAppDomainSetup.DynamicBase
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    string IAppDomainSetup.LicenseFile
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    string IAppDomainSetup.PrivateBinPath
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    string IAppDomainSetup.PrivateBinPathProbe
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    string IAppDomainSetup.ShadowCopyDirectories
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    string IAppDomainSetup.ShadowCopyFiles
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    #endregion
  }
#endif
  
}

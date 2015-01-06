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

// File System.AppDomainSetup.cs
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


namespace System
{
  sealed public partial class AppDomainSetup : IAppDomainSetup
  {
    #region Methods and constructors
    public AppDomainSetup()
    {
    }

    public AppDomainSetup(ActivationContext activationContext)
    {
      Contract.Requires(activationContext.Identity != null);
    }

    public AppDomainSetup(System.Runtime.Hosting.ActivationArguments activationArguments)
    {
      Contract.Requires(activationArguments.ActivationContext != null);
    }

    public byte[] GetConfigurationBytes()
    {
      return default(byte[]);
    }

    public void SetCompatibilitySwitches(IEnumerable<string> switches)
    {
    }

    public void SetConfigurationBytes(byte[] value)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Runtime.Hosting.ActivationArguments ActivationArguments
    {
      get
      {
        return default(System.Runtime.Hosting.ActivationArguments);
      }
      set
      {
      }
    }

    public AppDomainInitializer AppDomainInitializer
    {
      get
      {
        return default(AppDomainInitializer);
      }
      set
      {
      }
    }

    public string[] AppDomainInitializerArguments
    {
      get
      {
        return default(string[]);
      }
      set
      {
      }
    }

    public string AppDomainManagerAssembly
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string AppDomainManagerType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string ApplicationBase
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string ApplicationName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Security.Policy.ApplicationTrust ApplicationTrust
    {
      get
      {
        return default(System.Security.Policy.ApplicationTrust);
      }
      set
      {
      }
    }

    public string CachePath
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string ConfigurationFile
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool DisallowApplicationBaseProbing
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool DisallowBindingRedirects
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool DisallowCodeDownload
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool DisallowPublisherPolicy
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string DynamicBase
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string LicenseFile
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public LoaderOptimization LoaderOptimization
    {
      get
      {
        return default(LoaderOptimization);
      }
      set
      {
      }
    }

    public string[] PartialTrustVisibleAssemblies
    {
      get
      {
        return default(string[]);
      }
      set
      {
      }
    }

    public string PrivateBinPath
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string PrivateBinPathProbe
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool SandboxInterop
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string ShadowCopyDirectories
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string ShadowCopyFiles
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
    #endregion
  }
}

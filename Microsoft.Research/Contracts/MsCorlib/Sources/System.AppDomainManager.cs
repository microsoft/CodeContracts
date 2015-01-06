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

// File System.AppDomainManager.cs
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
  public partial class AppDomainManager : MarshalByRefObject
  {
    #region Methods and constructors
    public AppDomainManager()
    {
    }

    public virtual new bool CheckSecuritySettings(System.Security.SecurityState state)
    {
      return default(bool);
    }

    public virtual new AppDomain CreateDomain(string friendlyName, System.Security.Policy.Evidence securityInfo, AppDomainSetup appDomainInfo)
    {
      return default(AppDomain);
    }

    protected static AppDomain CreateDomainHelper(string friendlyName, System.Security.Policy.Evidence securityInfo, AppDomainSetup appDomainInfo)
    {
      return default(AppDomain);
    }

    public virtual new void InitializeNewDomain(AppDomainSetup appDomainInfo)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new System.Runtime.Hosting.ApplicationActivator ApplicationActivator
    {
      get
      {
        return default(System.Runtime.Hosting.ApplicationActivator);
      }
    }

    public virtual new System.Reflection.Assembly EntryAssembly
    {
      get
      {
        return default(System.Reflection.Assembly);
      }
    }

    public virtual new System.Threading.HostExecutionContextManager HostExecutionContextManager
    {
      get
      {
        return default(System.Threading.HostExecutionContextManager);
      }
    }

    public virtual new System.Security.HostSecurityManager HostSecurityManager
    {
      get
      {
        return default(System.Security.HostSecurityManager);
      }
    }

    public AppDomainManagerInitializationOptions InitializationFlags
    {
      get
      {
        return default(AppDomainManagerInitializationOptions);
      }
      set
      {
      }
    }
    #endregion
  }
}

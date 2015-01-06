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

// File System.ServiceProcess.ServiceController.cs
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


namespace System.ServiceProcess
{
  public partial class ServiceController : System.ComponentModel.Component
  {
    #region Methods and constructors
    public void Close()
    {
    }

    public void Continue()
    {
    }

    protected override void Dispose(bool disposing)
    {
    }

    public void ExecuteCommand(int command)
    {
    }

    public static System.ServiceProcess.ServiceController[] GetDevices(string machineName)
    {
      Contract.Requires(System.ComponentModel.SyntaxCheck.CheckMachineName(machineName));
      Contract.Ensures(Contract.Result<System.ServiceProcess.ServiceController[]>() != null);
      return default(System.ServiceProcess.ServiceController[]);
    }

    public static System.ServiceProcess.ServiceController[] GetDevices()
    {
      Contract.Ensures(Contract.Result<System.ServiceProcess.ServiceController[]>() != null);
      return default(System.ServiceProcess.ServiceController[]);
    }

    public static System.ServiceProcess.ServiceController[] GetServices()
    {
      Contract.Ensures(Contract.Result<System.ServiceProcess.ServiceController[]>() != null);
      return default(System.ServiceProcess.ServiceController[]);
    }

    public static System.ServiceProcess.ServiceController[] GetServices(string machineName) {
      Contract.Requires(!String.IsNullOrEmpty(machineName));
      return default(System.ServiceProcess.ServiceController[]);
    }

    public void Pause()
    {
    }

    public void Refresh()
    {
    }

    public ServiceController(string name, string machineName)
    {
      Contract.Requires(!String.IsNullOrEmpty(name));
      Contract.Requires(!String.IsNullOrEmpty(machineName));
    }

    public ServiceController(string name)
    {
      Contract.Requires(!String.IsNullOrEmpty(name));
    }

    public ServiceController()
    {
    }

    public void Start(string[] args)
    {
      Contract.Requires(args != null);
      Contract.Requires(Contract.ForAll(0, args.Length, i => args[i] != null));
    }

    public void Start()
    {
    }

    public void Stop()
    {
    }

    public void WaitForStatus(ServiceControllerStatus desiredStatus, TimeSpan timeout)
    {
    }

    public void WaitForStatus(ServiceControllerStatus desiredStatus)
    {
    }
    #endregion

    #region Properties and indexers
    public bool CanPauseAndContinue
    {
      get
      {
        return default(bool);
      }
    }

    public bool CanShutdown
    {
      get
      {
        return default(bool);
      }
    }

    public bool CanStop
    {
      get
      {
        return default(bool);
      }
    }

    public System.ServiceProcess.ServiceController[] DependentServices
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceProcess.ServiceController[]>() != null);
        return default(System.ServiceProcess.ServiceController[]);
      }
    }

    public string DisplayName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public string MachineName
    {
      get
      {
        Contract.Ensures(System.ComponentModel.SyntaxCheck.CheckMachineName(Contract.Result<string>()));
        return default(string);
      }
      set
      {
        Contract.Requires(System.ComponentModel.SyntaxCheck.CheckMachineName(value));
      }
    }

    public System.Runtime.InteropServices.SafeHandle ServiceHandle
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Runtime.InteropServices.SafeHandle>() != null);
        return default(System.Runtime.InteropServices.SafeHandle);
      }
    }

    public string ServiceName
    {
      get
      {
        Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));
        Contract.Ensures(Contract.Result<string>().Length <= ServiceBase.MaxNameLength);
        Contract.Ensures(!Contract.Result<string>().Contains("/"));
        Contract.Ensures(!Contract.Result<string>().Contains("\\"));
        return default(string);
      }
      set
      {
        Contract.Requires(!String.IsNullOrEmpty(value));
        Contract.Requires(value.Length <= ServiceBase.MaxNameLength);
        Contract.Requires(!value.Contains("/"));
        Contract.Requires(!value.Contains("\\"));
      }
    }

    public System.ServiceProcess.ServiceController[] ServicesDependedOn
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceProcess.ServiceController[]>() != null);
        return default(System.ServiceProcess.ServiceController[]);
      }
    }

    public ServiceType ServiceType
    {
      get
      {
        return default(ServiceType);
      }
    }

    public ServiceControllerStatus Status
    {
      get
      {
        return default(ServiceControllerStatus);
      }
    }
    #endregion
  }
}

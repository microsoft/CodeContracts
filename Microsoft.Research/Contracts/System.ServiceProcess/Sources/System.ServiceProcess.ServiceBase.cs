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

// File System.ServiceProcess.ServiceBase.cs
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
  public partial class ServiceBase : System.ComponentModel.Component
  {
    #region Methods and constructors
    protected override void Dispose(bool disposing)
    {
    }

    protected virtual new void OnContinue()
    {
    }

    protected virtual new void OnCustomCommand(int command)
    {
    }

    protected virtual new void OnPause()
    {
    }

    protected virtual new bool OnPowerEvent(PowerBroadcastStatus powerStatus)
    {
      return default(bool);
    }

    protected virtual new void OnSessionChange(SessionChangeDescription changeDescription)
    {
    }

    protected virtual new void OnShutdown()
    {
    }

    protected virtual new void OnStart(string[] args)
    {
    }

    protected virtual new void OnStop()
    {
    }

    public void RequestAdditionalTime(int milliseconds)
    {
    }

    public static void Run(System.ServiceProcess.ServiceBase service)
    {
    }

    public static void Run(System.ServiceProcess.ServiceBase[] services)
    {
    }

    public ServiceBase()
    {
    }

    public void ServiceMainCallback(int argCount, IntPtr argPointer)
    {
    }

    public void Stop()
    {
    }
    #endregion

    #region Properties and indexers
    public bool AutoLog
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool CanHandlePowerEvent
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool CanHandleSessionChangeEvent
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool CanPauseAndContinue
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool CanShutdown
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool CanStop
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new System.Diagnostics.EventLog EventLog
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Diagnostics.EventLog>() != null);
        return default(System.Diagnostics.EventLog);
      }
    }

    public int ExitCode
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    protected IntPtr ServiceHandle
    {
      get
      {
        return default(IntPtr);
      }
    }

    public string ServiceName
    {
      get
      {
        Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));
        Contract.Ensures(Contract.Result<string>().Length <= MaxNameLength);
        Contract.Ensures(!Contract.Result<string>().Contains("/"));
        Contract.Ensures(!Contract.Result<string>().Contains("\\"));
        return default(string);
      }
      set
      {
        Contract.Requires(!String.IsNullOrEmpty(value));
        Contract.Requires(value.Length <= MaxNameLength);
        Contract.Requires(!value.Contains("/"));
        Contract.Requires(!value.Contains("\\"));
      }
    }
    #endregion

    #region Fields
    public const int MaxNameLength = 0x00000050;
    #endregion
  }
}

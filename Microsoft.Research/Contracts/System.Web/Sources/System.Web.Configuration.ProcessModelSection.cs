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

// File System.Web.Configuration.ProcessModelSection.cs
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


namespace System.Web.Configuration
{
  sealed public partial class ProcessModelSection : System.Configuration.ConfigurationSection
  {
    #region Methods and constructors
    public ProcessModelSection()
    {
    }
    #endregion

    #region Properties and indexers
    public bool AutoConfig
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public TimeSpan ClientConnectedCheck
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public ProcessModelComAuthenticationLevel ComAuthenticationLevel
    {
      get
      {
        return default(ProcessModelComAuthenticationLevel);
      }
      set
      {
      }
    }

    public ProcessModelComImpersonationLevel ComImpersonationLevel
    {
      get
      {
        return default(ProcessModelComImpersonationLevel);
      }
      set
      {
      }
    }

    public int CpuMask
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    protected override System.Configuration.ConfigurationElementProperty ElementProperty
    {
      get
      {
        return default(System.Configuration.ConfigurationElementProperty);
      }
    }

    public bool Enable
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public TimeSpan IdleTimeout
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public ProcessModelLogLevel LogLevel
    {
      get
      {
        return default(ProcessModelLogLevel);
      }
      set
      {
      }
    }

    public int MaxAppDomains
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int MaxIOThreads
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int MaxWorkerThreads
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int MemoryLimit
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int MinIOThreads
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int MinWorkerThreads
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string Password
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public TimeSpan PingFrequency
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public TimeSpan PingTimeout
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    protected override System.Configuration.ConfigurationPropertyCollection Properties
    {
      get
      {
        return default(System.Configuration.ConfigurationPropertyCollection);
      }
    }

    public int RequestLimit
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int RequestQueueLimit
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public TimeSpan ResponseDeadlockInterval
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public TimeSpan ResponseRestartDeadlockInterval
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public int RestartQueueLimit
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string ServerErrorMessageFile
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public TimeSpan ShutdownTimeout
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public TimeSpan Timeout
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public string UserName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool WebGarden
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }
    #endregion
  }
}

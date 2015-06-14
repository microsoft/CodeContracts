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

// File System.Diagnostics.Eventing.Reader.EventLogConfiguration.cs
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


namespace System.Diagnostics.Eventing.Reader
{
  public partial class EventLogConfiguration : IDisposable
  {
    #region Methods and constructors
    protected virtual new void Dispose(bool disposing)
    {
    }

    public void Dispose()
    {
    }

    public EventLogConfiguration(string logName)
    {
    }

    public EventLogConfiguration(string logName, EventLogSession session)
    {
    }

    public void SaveChanges()
    {
    }
    #endregion

    #region Properties and indexers
    public bool IsClassicLog
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string LogFilePath
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public EventLogIsolation LogIsolation
    {
      get
      {
        return default(EventLogIsolation);
      }
    }

    public EventLogMode LogMode
    {
      get
      {
        Contract.Ensures(((System.Diagnostics.Eventing.Reader.EventLogMode)(0)) <= Contract.Result<System.Diagnostics.Eventing.Reader.EventLogMode>());
        Contract.Ensures(Contract.Result<System.Diagnostics.Eventing.Reader.EventLogMode>() <= ((System.Diagnostics.Eventing.Reader.EventLogMode)(2)));

        return default(EventLogMode);
      }
      set
      {
      }
    }

    public string LogName
    {
      get
      {
        return default(string);
      }
    }

    public EventLogType LogType
    {
      get
      {
        return default(EventLogType);
      }
    }

    public long MaximumSizeInBytes
    {
      get
      {
        return default(long);
      }
      set
      {
      }
    }

    public string OwningProviderName
    {
      get
      {
        return default(string);
      }
    }

    public Nullable<int> ProviderBufferSize
    {
      get
      {
        return default(Nullable<int>);
      }
    }

    public Nullable<Guid> ProviderControlGuid
    {
      get
      {
        return default(Nullable<Guid>);
      }
    }

    public Nullable<long> ProviderKeywords
    {
      get
      {
        return default(Nullable<long>);
      }
      set
      {
      }
    }

    public Nullable<int> ProviderLatency
    {
      get
      {
        return default(Nullable<int>);
      }
    }

    public Nullable<int> ProviderLevel
    {
      get
      {
        return default(Nullable<int>);
      }
      set
      {
      }
    }

    public Nullable<int> ProviderMaximumNumberOfBuffers
    {
      get
      {
        return default(Nullable<int>);
      }
    }

    public Nullable<int> ProviderMinimumNumberOfBuffers
    {
      get
      {
        return default(Nullable<int>);
      }
    }

    public IEnumerable<string> ProviderNames
    {
      get
      {
        return default(IEnumerable<string>);
      }
    }

    public string SecurityDescriptor
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

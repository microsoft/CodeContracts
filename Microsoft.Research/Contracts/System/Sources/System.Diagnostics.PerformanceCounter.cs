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

// File System.Diagnostics.PerformanceCounter.cs
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


namespace System.Diagnostics
{
  sealed public partial class PerformanceCounter : System.ComponentModel.Component, System.ComponentModel.ISupportInitialize
  {
    #region Methods and constructors
    public void BeginInit()
    {
    }

    public void Close()
    {
    }

    public static void CloseSharedResources()
    {
    }

    public long Decrement()
    {
      Contract.Ensures(-9223372036854775807 <= Contract.Result<long>());

      return default(long);
    }

    protected override void Dispose(bool disposing)
    {
    }

    public void EndInit()
    {
    }

    public long Increment()
    {
      Contract.Ensures(-9223372036854775807 <= Contract.Result<long>());

      return default(long);
    }

    public long IncrementBy(long value)
    {
      Contract.Ensures(-9223372036854775807 <= Contract.Result<long>());

      return default(long);
    }

    public CounterSample NextSample()
    {
      return default(CounterSample);
    }

    public float NextValue()
    {
      return default(float);
    }

    public PerformanceCounter(string categoryName, string counterName, string instanceName, string machineName)
    {
    }

    public PerformanceCounter()
    {
    }

    public PerformanceCounter(string categoryName, string counterName)
    {
    }

    public PerformanceCounter(string categoryName, string counterName, bool readOnly)
    {
    }

    public PerformanceCounter(string categoryName, string counterName, string instanceName)
    {
    }

    public PerformanceCounter(string categoryName, string counterName, string instanceName, bool readOnly)
    {
    }

    public void RemoveInstance()
    {
    }
    #endregion

    #region Properties and indexers
    public string CategoryName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string CounterHelp
    {
      get
      {
        return default(string);
      }
    }

    public string CounterName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public PerformanceCounterType CounterType
    {
      get
      {
        return default(PerformanceCounterType);
      }
    }

    public PerformanceCounterInstanceLifetime InstanceLifetime
    {
      get
      {
        return default(PerformanceCounterInstanceLifetime);
      }
      set
      {
      }
    }

    public string InstanceName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string MachineName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public long RawValue
    {
      get
      {
        Contract.Ensures(-9223372036854775807 <= Contract.Result<long>());

        return default(long);
      }
      set
      {
      }
    }

    public bool ReadOnly
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

    #region Fields
    public static int DefaultFileMappingSize;
    #endregion
  }
}

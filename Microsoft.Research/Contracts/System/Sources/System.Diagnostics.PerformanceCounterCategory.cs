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

// File System.Diagnostics.PerformanceCounterCategory.cs
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
  sealed public partial class PerformanceCounterCategory
  {
    #region Methods and constructors
    public static bool CounterExists(string counterName, string categoryName, string machineName)
    {
      return default(bool);
    }

    public static bool CounterExists(string counterName, string categoryName)
    {
      return default(bool);
    }

    public bool CounterExists(string counterName)
    {
      return default(bool);
    }

    public static System.Diagnostics.PerformanceCounterCategory Create(string categoryName, string categoryHelp, CounterCreationDataCollection counterData)
    {
      Contract.Ensures(0 <= categoryName.Length);
      Contract.Ensures(categoryName.Length <= 997);
      Contract.Ensures(Contract.Result<System.Diagnostics.PerformanceCounterCategory>() != null);

      return default(System.Diagnostics.PerformanceCounterCategory);
    }

    public static System.Diagnostics.PerformanceCounterCategory Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, CounterCreationDataCollection counterData)
    {
      Contract.Ensures(0 <= categoryName.Length);
      Contract.Ensures(categoryName.Length <= 997);
      Contract.Ensures(Contract.Result<System.Diagnostics.PerformanceCounterCategory>() != null);

      return default(System.Diagnostics.PerformanceCounterCategory);
    }

    public static System.Diagnostics.PerformanceCounterCategory Create(string categoryName, string categoryHelp, string counterName, string counterHelp)
    {
      Contract.Ensures(0 <= categoryName.Length);
      Contract.Ensures(categoryName.Length <= 997);
      Contract.Ensures(Contract.Result<System.Diagnostics.PerformanceCounterCategory>() != null);

      return default(System.Diagnostics.PerformanceCounterCategory);
    }

    public static System.Diagnostics.PerformanceCounterCategory Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, string counterName, string counterHelp)
    {
      Contract.Ensures(0 <= categoryName.Length);
      Contract.Ensures(categoryName.Length <= 997);
      Contract.Ensures(Contract.Result<System.Diagnostics.PerformanceCounterCategory>() != null);

      return default(System.Diagnostics.PerformanceCounterCategory);
    }

    public static void Delete(string categoryName)
    {
    }

    public static bool Exists(string categoryName)
    {
      return default(bool);
    }

    public static bool Exists(string categoryName, string machineName)
    {
      return default(bool);
    }

    public static System.Diagnostics.PerformanceCounterCategory[] GetCategories()
    {
      Contract.Ensures(Contract.Result<System.Diagnostics.PerformanceCounterCategory[]>() != null);

      return default(System.Diagnostics.PerformanceCounterCategory[]);
    }

    public static System.Diagnostics.PerformanceCounterCategory[] GetCategories(string machineName)
    {
      Contract.Ensures(Contract.Result<System.Diagnostics.PerformanceCounterCategory[]>() != null);

      return default(System.Diagnostics.PerformanceCounterCategory[]);
    }

    public PerformanceCounter[] GetCounters()
    {
      Contract.Ensures(Contract.Result<System.Diagnostics.PerformanceCounter[]>() != null);

      return default(PerformanceCounter[]);
    }

    public PerformanceCounter[] GetCounters(string instanceName)
    {
      Contract.Ensures(Contract.Result<System.Diagnostics.PerformanceCounter[]>() != null);

      return default(PerformanceCounter[]);
    }

    public string[] GetInstanceNames()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public static bool InstanceExists(string instanceName, string categoryName, string machineName)
    {
      return default(bool);
    }

    public static bool InstanceExists(string instanceName, string categoryName)
    {
      return default(bool);
    }

    public bool InstanceExists(string instanceName)
    {
      return default(bool);
    }

    public PerformanceCounterCategory(string categoryName)
    {
    }

    public PerformanceCounterCategory()
    {
    }

    public PerformanceCounterCategory(string categoryName, string machineName)
    {
    }

    public InstanceDataCollectionCollection ReadCategory()
    {
      return default(InstanceDataCollectionCollection);
    }
    #endregion

    #region Properties and indexers
    public string CategoryHelp
    {
      get
      {
        return default(string);
      }
    }

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

    public PerformanceCounterCategoryType CategoryType
    {
      get
      {
        Contract.Ensures(((System.Diagnostics.PerformanceCounterCategoryType)(-1)) <= Contract.Result<System.Diagnostics.PerformanceCounterCategoryType>());
        Contract.Ensures(Contract.Result<System.Diagnostics.PerformanceCounterCategoryType>() <= ((System.Diagnostics.PerformanceCounterCategoryType)(1)));

        return default(PerformanceCounterCategoryType);
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
    #endregion
  }
}

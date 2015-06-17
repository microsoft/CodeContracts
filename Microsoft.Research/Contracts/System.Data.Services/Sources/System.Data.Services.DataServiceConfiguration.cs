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

// File System.Data.Services.DataServiceConfiguration.cs
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


namespace System.Data.Services
{
  sealed public partial class DataServiceConfiguration : IDataServiceConfiguration
  {
    #region Methods and constructors
    private DataServiceConfiguration()
    {
    }

    public void EnableTypeAccess(string typeName)
    {
      Contract.Requires(typeName != null);
      Contract.Ensures(!string.IsNullOrEmpty(typeName));
    }

    public void RegisterKnownType(Type type)
    {
    }

    public void SetEntitySetAccessRule(string name, EntitySetRights rights)
    {
      Contract.Requires(name != null);

    }

    public void SetEntitySetPageSize(string name, int size)
    {
      Contract.Requires(size >= 0);

    }

    public void SetServiceOperationAccessRule(string name, ServiceOperationRights rights)
    {
      Contract.Requires(name != null);
    }
    #endregion

    #region Properties and indexers
    public DataServiceBehavior DataServiceBehavior
    {
      get
      {
        Contract.Ensures(Contract.Result<DataServiceBehavior>() != null);
        
        return default(DataServiceBehavior);
      }
    }

    public bool EnableTypeConversion
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public int MaxBatchCount
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0); 
      }
    }

    public int MaxChangesetCount
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0); 
      }
    }

    public int MaxExpandCount
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
      }
    }

    public int MaxExpandDepth
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
      }
    }

    public int MaxObjectCountOnInsert
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);

      }
    }

    public int MaxResultsPerCollection
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
      }
    }

    public bool UseVerboseErrors
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

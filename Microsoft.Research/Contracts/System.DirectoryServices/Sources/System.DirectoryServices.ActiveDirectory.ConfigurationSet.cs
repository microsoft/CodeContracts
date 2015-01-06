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

// File System.DirectoryServices.ActiveDirectory.ConfigurationSet.cs
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


namespace System.DirectoryServices.ActiveDirectory
{
  public partial class ConfigurationSet
  {
    #region Methods and constructors
    private ConfigurationSet()
    {
    }

    public void Dispose()
    {
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public AdamInstance FindAdamInstance(string partitionName)
    {
      Contract.Requires(partitionName != null);
      Contract.Ensures(Contract.Result<AdamInstance>() != null);

      return default(AdamInstance);
    }

    public AdamInstance FindAdamInstance()
    {
      Contract.Ensures(Contract.Result<AdamInstance>() != null);
      
      return default(AdamInstance);
    }

    public AdamInstance FindAdamInstance(string partitionName, string siteName)
    {
      Contract.Requires(partitionName != null);

      Contract.Ensures(Contract.Result<AdamInstance>() != null);

      return default(AdamInstance);
    }

    public AdamInstanceCollection FindAllAdamInstances()
    {
      return default(AdamInstanceCollection);
    }

    public AdamInstanceCollection FindAllAdamInstances(string partitionName, string siteName)
    {
      return default(AdamInstanceCollection);
    }

    public AdamInstanceCollection FindAllAdamInstances(string partitionName)
    {
      return default(AdamInstanceCollection);
    }

    public static ConfigurationSet GetConfigurationSet(DirectoryContext context)
    {
      return default(ConfigurationSet);
    }

    public System.DirectoryServices.DirectoryEntry GetDirectoryEntry()
    {
      return default(System.DirectoryServices.DirectoryEntry);
    }

    public ReplicationSecurityLevel GetSecurityLevel()
    {
      return default(ReplicationSecurityLevel);
    }

    public void SetSecurityLevel(ReplicationSecurityLevel securityLevel)
    {
    }

    #endregion

    #region Properties and indexers
    public AdamInstanceCollection AdamInstances
    {
      get
      {
        return default(AdamInstanceCollection);
      }
    }

    public ApplicationPartitionCollection ApplicationPartitions
    {
      get
      {
        return default(ApplicationPartitionCollection);
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
    }

    public AdamInstance NamingRoleOwner
    {
      get
      {
        return default(AdamInstance);
      }
    }

    public ActiveDirectorySchema Schema
    {
      get
      {
        return default(ActiveDirectorySchema);
      }
    }

    public AdamInstance SchemaRoleOwner
    {
      get
      {
        return default(AdamInstance);
      }
    }

    public ReadOnlySiteCollection Sites
    {
      get
      {
        return default(ReadOnlySiteCollection);
      }
    }
    #endregion
  }
}

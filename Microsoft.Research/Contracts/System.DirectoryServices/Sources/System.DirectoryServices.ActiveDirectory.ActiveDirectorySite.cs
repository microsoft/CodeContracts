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

// File System.DirectoryServices.ActiveDirectory.ActiveDirectorySite.cs
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
  public partial class ActiveDirectorySite : IDisposable
  {
    #region Methods and constructors
    public ActiveDirectorySite(DirectoryContext context, string siteName)
    {
    }

    public void Delete()
    {
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public void Dispose()
    {
    }

    public static ActiveDirectorySite FindByName(DirectoryContext context, string siteName)
    {
      Contract.Requires(context != null);
      Contract.Requires(siteName != null);

      Contract.Ensures(Contract.Result<ActiveDirectorySite>() != null);

      return default(ActiveDirectorySite);
    }

    public static System.DirectoryServices.ActiveDirectory.ActiveDirectorySite GetComputerSite()
    {
      Contract.Ensures(Contract.Result<ActiveDirectorySite>() != null);

      return default(System.DirectoryServices.ActiveDirectory.ActiveDirectorySite);
    }

    public System.DirectoryServices.DirectoryEntry GetDirectoryEntry()
    {
      Contract.Ensures(Contract.Result<ActiveDirectorySite>() != null);

      return default(System.DirectoryServices.DirectoryEntry);
    }

    public void Save()
    {
    }

    #endregion

    #region Properties and indexers
    public ReadOnlySiteCollection AdjacentSites
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlySiteCollection>() != null);

        return default(ReadOnlySiteCollection);
      }
    }

    public ReadOnlyDirectoryServerCollection BridgeheadServers
    {
      get
      {
        return default(ReadOnlyDirectoryServerCollection);
      }
    }

    public DomainCollection Domains
    {
      get
      {
        Contract.Ensures(Contract.Result<DomainCollection>() != null);

        return default(DomainCollection);
      }
    }

    public DirectoryServer InterSiteTopologyGenerator
    {
      // Can return null
      get
      {
        return default(DirectoryServer);
      }
      set
      {
      }
    }

    public ActiveDirectorySchedule IntraSiteReplicationSchedule
    {
      get
      {
        return default(ActiveDirectorySchedule);
      }
      set
      {
      }
    }

    // can be null
    public string Location
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string Name
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public ActiveDirectorySiteOptions Options
    {
      get
      {
        return default(ActiveDirectorySiteOptions);
      }
      set
      {
      }
    }

    public DirectoryServerCollection PreferredRpcBridgeheadServers
    {
      get
      {
        Contract.Ensures(Contract.Result<DirectoryServerCollection>() != null);

        return default(DirectoryServerCollection);
      }
    }

    public DirectoryServerCollection PreferredSmtpBridgeheadServers
    {
      get
      {
        Contract.Ensures(Contract.Result<DirectoryServerCollection>() != null);

        return default(DirectoryServerCollection);
      }
    }

    public ReadOnlyDirectoryServerCollection Servers
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyDirectoryServerCollection>() != null);
        
        return default(ReadOnlyDirectoryServerCollection);
      }
    }

    public ReadOnlySiteLinkCollection SiteLinks
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlySiteLinkCollection>() != null);

        return default(ReadOnlySiteLinkCollection);
      }
    }

    public ActiveDirectorySubnetCollection Subnets
    {
      get
      {
        Contract.Ensures(Contract.Result<ActiveDirectorySubnetCollection>() != null);
        return default(ActiveDirectorySubnetCollection);
      }
    }
    #endregion
  }
}

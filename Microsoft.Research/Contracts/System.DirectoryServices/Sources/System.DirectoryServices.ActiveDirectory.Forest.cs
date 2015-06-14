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

// File System.DirectoryServices.ActiveDirectory.Forest.cs
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
  public partial class Forest : IDisposable
  {
    #region Methods and constructors
    public void CreateLocalSideOfTrustRelationship(string targetForestName, TrustDirection direction, string trustPassword)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetForestName));
      Contract.Requires(!string.IsNullOrEmpty(trustPassword));

    }

    public void CreateTrustRelationship(Forest targetForest, TrustDirection direction)
    {
      Contract.Requires(targetForest != null);
    }

    public void DeleteLocalSideOfTrustRelationship(string targetForestName)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetForestName));
    }

    public void DeleteTrustRelationship(Forest targetForest)
    {
      Contract.Requires(targetForest != null);
    }

    public void Dispose()
    {
    }

    protected void Dispose(bool disposing)
    {
    }

    public GlobalCatalogCollection FindAllDiscoverableGlobalCatalogs()
    {
      Contract.Ensures(Contract.Result<GlobalCatalogCollection>() != null);
      return default(GlobalCatalogCollection);
    }

    public GlobalCatalogCollection FindAllDiscoverableGlobalCatalogs(string siteName)
    {
      Contract.Requires(!string.IsNullOrEmpty(siteName));
      Contract.Ensures(Contract.Result<GlobalCatalogCollection>() != null);
      return default(GlobalCatalogCollection);
    }

    public GlobalCatalogCollection FindAllGlobalCatalogs()
    {
      Contract.Ensures(Contract.Result<GlobalCatalogCollection>() != null);

      return default(GlobalCatalogCollection);
    }

    public GlobalCatalogCollection FindAllGlobalCatalogs(string siteName)
    {
      Contract.Requires(!string.IsNullOrEmpty(siteName));
      Contract.Ensures(Contract.Result<GlobalCatalogCollection>() != null);

      return default(GlobalCatalogCollection);
    }

    public GlobalCatalog FindGlobalCatalog(LocatorOptions flag)
    {
      Contract.Ensures(Contract.Result<GlobalCatalog>() != null);

      return default(GlobalCatalog);
    }

    public GlobalCatalog FindGlobalCatalog()
    {
      Contract.Ensures(Contract.Result<GlobalCatalog>() != null);
      return default(GlobalCatalog);
    }

    public GlobalCatalog FindGlobalCatalog(string siteName)
    {
      Contract.Requires(siteName != null);
      Contract.Ensures(Contract.Result<GlobalCatalog>() != null);

      return default(GlobalCatalog);
    }

    public GlobalCatalog FindGlobalCatalog(string siteName, LocatorOptions flag)
    {
      Contract.Requires(siteName != null);
      Contract.Ensures(Contract.Result<GlobalCatalog>() != null);
      
      return default(GlobalCatalog);
    }

    private Forest()
    {
    }

    public TrustRelationshipInformationCollection GetAllTrustRelationships()
    {
      Contract.Ensures(Contract.Result<TrustRelationshipInformationCollection>() != null);

      return default(TrustRelationshipInformationCollection);
    }

    public static Forest GetCurrentForest()
    {
      Contract.Ensures(Contract.Result<Forest>() != null);

      return default(Forest);
    }

    public static Forest GetForest(DirectoryContext context)
    {
      Contract.Requires(context != null);

      Contract.Ensures(Contract.Result<Forest>() != null);

      return default(Forest);
    }

    public bool GetSelectiveAuthenticationStatus(string targetForestName)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetForestName));
      return default(bool);
    }

    public bool GetSidFilteringStatus(string targetForestName)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetForestName));
      return default(bool);
    }

    public ForestTrustRelationshipInformation GetTrustRelationship(string targetForestName)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetForestName));      

      return default(ForestTrustRelationshipInformation);
    }

    public void RaiseForestFunctionality(ForestMode forestMode)
    {
    }

    public void RepairTrustRelationship(Forest targetForest)
    {
    }

    public void SetSelectiveAuthenticationStatus(string targetForestName, bool enable)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetForestName));

    }

    public void SetSidFilteringStatus(string targetForestName, bool enable)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetForestName));

    }

    public void UpdateLocalSideOfTrustRelationship(string targetForestName, string newTrustPassword)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetForestName));
      Contract.Requires(!string.IsNullOrEmpty(newTrustPassword));

    }

    public void UpdateLocalSideOfTrustRelationship(string targetForestName, TrustDirection newTrustDirection, string newTrustPassword)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetForestName));
      Contract.Requires(!string.IsNullOrEmpty(newTrustPassword));
    }

    public void UpdateTrustRelationship(Forest targetForest, TrustDirection newTrustDirection)
    {
      Contract.Requires(targetForest != null);
    }

    public void VerifyOutboundTrustRelationship(string targetForestName)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetForestName));

    }

    public void VerifyTrustRelationship(Forest targetForest, TrustDirection direction)
    {
      Contract.Requires(targetForest != null);
    }
    #endregion

    #region Properties and indexers
    public ApplicationPartitionCollection ApplicationPartitions
    {
      get
      {
        Contract.Ensures(Contract.Result<ApplicationPartitionCollection>() != null);

        return default(ApplicationPartitionCollection);
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

    public ForestMode ForestMode
    {
      get
      {
        return default(ForestMode);
      }
    }

    public GlobalCatalogCollection GlobalCatalogs
    {
      get
      {
        Contract.Ensures(Contract.Result<GlobalCatalogCollection>() != null);

        return default(GlobalCatalogCollection);
      }
    }

    public string Name
    {
      // can be null? It seems the constructor may assign any value
      get
      {
        return default(string);
      }
    }

    public DomainController NamingRoleOwner
    {
      get
      {
        Contract.Ensures(Contract.Result<DomainController>() != null);

        return default(DomainController);
      }
    }

    public Domain RootDomain
    {
      get
      {
        Contract.Ensures(Contract.Result<Domain>() != null);
        return default(Domain);
      }
    }

    public ActiveDirectorySchema Schema
    {
      get
      {
        Contract.Ensures(Contract.Result<ActiveDirectorySchema>() != null);

        return default(ActiveDirectorySchema);
      }
    }

    public DomainController SchemaRoleOwner
    {
      get
      {
        Contract.Ensures(Contract.Result<DomainController>() != null);
        return default(DomainController);
      }
    }

    public ReadOnlySiteCollection Sites
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlySiteCollection>() != null);
        return default(ReadOnlySiteCollection);
      }
    }
    #endregion
  }
}

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

// File System.DirectoryServices.ActiveDirectory.Domain.cs
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
  public partial class Domain : ActiveDirectoryPartition
  {
    #region Methods and constructors
    public void CreateLocalSideOfTrustRelationship(string targetDomainName, TrustDirection direction, string trustPassword)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetDomainName));
      Contract.Requires(!string.IsNullOrEmpty(trustPassword));
    }

    public void CreateTrustRelationship(Domain targetDomain, TrustDirection direction)
    {
      Contract.Requires(targetDomain != null);
    }

    public void DeleteLocalSideOfTrustRelationship(string targetDomainName)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetDomainName));
    }

    public void DeleteTrustRelationship(Domain targetDomain)
    {
      Contract.Requires(targetDomain != null);
    }

    private Domain()
    {
    }

    public DomainControllerCollection FindAllDiscoverableDomainControllers()
    {
      Contract.Ensures(Contract.Result<DomainControllerCollection>() != null);

      return default(DomainControllerCollection);
    }

    public DomainControllerCollection FindAllDiscoverableDomainControllers(string siteName)
    {
      Contract.Requires(!string.IsNullOrEmpty(siteName));
      Contract.Ensures(Contract.Result<DomainControllerCollection>() != null);

      return default(DomainControllerCollection);
    }

    public DomainControllerCollection FindAllDomainControllers()
    {
      Contract.Ensures(Contract.Result<DomainControllerCollection>() != null);

      return default(DomainControllerCollection);
    }

    public DomainControllerCollection FindAllDomainControllers(string siteName)
    {
      Contract.Requires(!string.IsNullOrEmpty(siteName));
      Contract.Ensures(Contract.Result<DomainControllerCollection>() != null);
      
      return default(DomainControllerCollection);
    }

    public DomainController FindDomainController(string siteName)
    {
      Contract.Requires(!string.IsNullOrEmpty(siteName));

      Contract.Ensures(Contract.Result<DomainController>() != null);
      return default(DomainController);
    }

    public DomainController FindDomainController(LocatorOptions flag)
    {
      Contract.Ensures(Contract.Result<DomainController>() != null);

      return default(DomainController);
    }

    public DomainController FindDomainController()
    {
      Contract.Ensures(Contract.Result<DomainController>() != null);

      return default(DomainController);
    }

    public DomainController FindDomainController(string siteName, LocatorOptions flag)
    {
      Contract.Ensures(Contract.Result<DomainController>() != null);

      return default(DomainController);
    }

    public TrustRelationshipInformationCollection GetAllTrustRelationships()
    {
      Contract.Ensures(Contract.Result<TrustRelationshipInformationCollection>() != null);

      return default(TrustRelationshipInformationCollection);
    }

    public static Domain GetComputerDomain()
    {
      Contract.Ensures(Contract.Result<Domain>() != null);

      return default(Domain);
    }

    public static Domain GetCurrentDomain()
    {
      Contract.Ensures(Contract.Result<Domain>() != null);

      return default(Domain);
    }

    public override System.DirectoryServices.DirectoryEntry GetDirectoryEntry()
    {
      Contract.Ensures(Contract.Result<DirectoryEntry>() != null);

      return default(System.DirectoryServices.DirectoryEntry);
    }

    public static Domain GetDomain(DirectoryContext context)
    {
      Contract.Requires(context != null);
      Contract.Ensures(Contract.Result<Domain>() != null);

      return default(Domain);
    }

    public bool GetSelectiveAuthenticationStatus(string targetDomainName)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetDomainName));

      return default(bool);
    }

    public bool GetSidFilteringStatus(string targetDomainName)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetDomainName));

      return default(bool);
    }

    public TrustRelationshipInformation GetTrustRelationship(string targetDomainName)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetDomainName));

      return default(TrustRelationshipInformation);
    }

    public void RaiseDomainFunctionality(DomainMode domainMode)
    {
    }

    public void RepairTrustRelationship(Domain targetDomain)
    {
    }

    public void SetSelectiveAuthenticationStatus(string targetDomainName, bool enable)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetDomainName));

    }

    public void SetSidFilteringStatus(string targetDomainName, bool enable)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetDomainName));
    }

    public void UpdateLocalSideOfTrustRelationship(string targetDomainName, string newTrustPassword)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetDomainName));
    }

    public void UpdateLocalSideOfTrustRelationship(string targetDomainName, TrustDirection newTrustDirection, string newTrustPassword)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetDomainName));
    }

    public void UpdateTrustRelationship(Domain targetDomain, TrustDirection newTrustDirection)
    {
      Contract.Requires(targetDomain != null);
    }

    public void VerifyOutboundTrustRelationship(string targetDomainName)
    {
      Contract.Requires(!string.IsNullOrEmpty(targetDomainName));
    }

    public void VerifyTrustRelationship(Domain targetDomain, TrustDirection direction)
    {
      Contract.Requires(targetDomain != null);
    }
    #endregion

    #region Properties and indexers
    public DomainCollection Children
    {
      get
      {
        Contract.Ensures(Contract.Result<DomainCollection>() != null);

        return default(DomainCollection);
      }
    }

    public DomainControllerCollection DomainControllers
    {
      get
      {
        Contract.Ensures(Contract.Result<DomainCollection>() != null);

        return default(DomainControllerCollection);
      }
    }

    public DomainMode DomainMode
    {
      get
      {
        return default(DomainMode);
      }
    }

    public Forest Forest
    {
      get
      {
        Contract.Ensures(Contract.Result<Forest>() != null);

        return default(Forest);
      }
    }

    public DomainController InfrastructureRoleOwner
    {
      get
      {
        Contract.Ensures(Contract.Result<DomainController>() != null);

        return default(DomainController);
      }
    }

    public Domain Parent
    {
      get
      {
        Contract.Ensures(Contract.Result<Domain>() != null);

        return default(System.DirectoryServices.ActiveDirectory.Domain);
      }
    }

    public DomainController PdcRoleOwner
    {
      get
      {
        Contract.Ensures(Contract.Result<DomainController>() != null);

        return default(DomainController);
      }
    }

    public DomainController RidRoleOwner
    {
      get
      {
        Contract.Ensures(Contract.Result<DomainController>() != null);

        return default(DomainController);
      }
    }
    #endregion
  }
}

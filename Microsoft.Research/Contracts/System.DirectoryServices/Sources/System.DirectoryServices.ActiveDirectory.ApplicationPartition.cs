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

// File System.DirectoryServices.ActiveDirectory.ApplicationPartition.cs
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
  public partial class ApplicationPartition : ActiveDirectoryPartition
  {
    #region Methods and constructors
    public ApplicationPartition(DirectoryContext context, string distinguishedName, string objectClass)
    {
      Contract.Requires(context != null);
      Contract.Requires(!string.IsNullOrEmpty(distinguishedName));
      Contract.Requires(!string.IsNullOrEmpty(objectClass));
    }

    public ApplicationPartition(DirectoryContext context, string distinguishedName)
    {
      Contract.Requires(context != null);
      Contract.Requires(!string.IsNullOrEmpty(distinguishedName));
    }

    public void Delete()
    {
    }

    protected override void Dispose(bool disposing)
    {
    }

    public ReadOnlyDirectoryServerCollection FindAllDirectoryServers(string siteName)
    {
      Contract.Requires(siteName != null);
      Contract.Ensures(Contract.Result<ReadOnlyDirectoryServerCollection>() != null);

      return default(ReadOnlyDirectoryServerCollection);
    }

    public ReadOnlyDirectoryServerCollection FindAllDirectoryServers()
    {
      Contract.Ensures(Contract.Result<ReadOnlyDirectoryServerCollection>() != null);

      return default(ReadOnlyDirectoryServerCollection);
    }

    public ReadOnlyDirectoryServerCollection FindAllDiscoverableDirectoryServers()
    {

      Contract.Ensures(Contract.Result<ReadOnlyDirectoryServerCollection>() != null);

      return default(ReadOnlyDirectoryServerCollection);
    }

    public ReadOnlyDirectoryServerCollection FindAllDiscoverableDirectoryServers(string siteName)
    {
      Contract.Requires(!string.IsNullOrEmpty(siteName));

      Contract.Ensures(Contract.Result<ReadOnlyDirectoryServerCollection>() != null);

      return default(ReadOnlyDirectoryServerCollection);
    }

    public static System.DirectoryServices.ActiveDirectory.ApplicationPartition FindByName(DirectoryContext context, string distinguishedName)
    {
      Contract.Requires(context != null);
      Contract.Requires(!string.IsNullOrEmpty(distinguishedName));

      Contract.Ensures(Contract.Result<ApplicationPartition>() != null);


      return default(System.DirectoryServices.ActiveDirectory.ApplicationPartition);
    }

    public DirectoryServer FindDirectoryServer(string siteName)
    {
      Contract.Requires(siteName != null);
      Contract.Ensures(Contract.Result<DirectoryServer>() != null);

      return default(DirectoryServer);
    }

    public DirectoryServer FindDirectoryServer()
    {
      Contract.Ensures(Contract.Result<DirectoryServer>() != null);

      return default(DirectoryServer);
    }

    public DirectoryServer FindDirectoryServer(string siteName, bool forceRediscovery)
    {
      Contract.Requires(!string.IsNullOrEmpty(siteName));
      Contract.Ensures(Contract.Result<DirectoryServer>() != null);

      return default(DirectoryServer);
    }

    public DirectoryServer FindDirectoryServer(bool forceRediscovery)
    {
      Contract.Ensures(Contract.Result<DirectoryServer>() != null);

      return default(DirectoryServer);
    }

    public static System.DirectoryServices.ActiveDirectory.ApplicationPartition GetApplicationPartition(DirectoryContext context)
    {
      Contract.Requires(context != null);

      Contract.Ensures(Contract.Result<ApplicationPartition>() != null);

      return default(System.DirectoryServices.ActiveDirectory.ApplicationPartition);
    }

    public override System.DirectoryServices.DirectoryEntry GetDirectoryEntry()
    {
      return default(System.DirectoryServices.DirectoryEntry);
    }

    public void Save()
    {
    }
    #endregion

    #region Properties and indexers
    public DirectoryServerCollection DirectoryServers
    {
      get
      {
        return default(DirectoryServerCollection);
      }
    }

    public string SecurityReferenceDomain
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

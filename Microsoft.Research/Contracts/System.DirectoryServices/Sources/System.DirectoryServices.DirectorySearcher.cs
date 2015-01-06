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

// File System.DirectoryServices.DirectorySearcher.cs
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


namespace System.DirectoryServices
{
  public partial class DirectorySearcher : System.ComponentModel.Component
  {
    #region Methods and constructors
    public DirectorySearcher(DirectoryEntry searchRoot, string filter, string[] propertiesToLoad, SearchScope scope)
    {
    }

    public DirectorySearcher()
    {
    }

    public DirectorySearcher(DirectoryEntry searchRoot, string filter, string[] propertiesToLoad)
    {
    }

    public DirectorySearcher(string filter)
    {
    }

    public DirectorySearcher(DirectoryEntry searchRoot)
    {
    }

    public DirectorySearcher(DirectoryEntry searchRoot, string filter)
    {
    }

    public DirectorySearcher(string filter, string[] propertiesToLoad, SearchScope scope)
    {
    }

    public DirectorySearcher(string filter, string[] propertiesToLoad)
    {
    }

    protected override void Dispose(bool disposing)
    {
    }

    public SearchResultCollection FindAll()
    {
      Contract.Ensures(Contract.Result<SearchResultCollection>() != null);

      return default(SearchResultCollection);
    }

    public SearchResult FindOne()
    {
      Contract.Ensures(Contract.Result<SearchResult>() != null);

      return default(SearchResult);
    }
    #endregion

    #region Properties and indexers
    public bool Asynchronous
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string AttributeScopeQuery
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool CacheResults
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public TimeSpan ClientTimeout
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public DereferenceAlias DerefAlias
    {
      get
      {
        return default(DereferenceAlias);
      }
      set
      {
      }
    }

    public DirectorySynchronization DirectorySynchronization
    {
      get
      {
        return default(DirectorySynchronization);
      }
      set
      {
      }
    }

    public ExtendedDN ExtendedDN
    {
      get
      {
        return default(ExtendedDN);
      }
      set
      {
      }
    }

    public string Filter
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public int PageSize
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public System.Collections.Specialized.StringCollection PropertiesToLoad
    {
      get
      {
        return default(System.Collections.Specialized.StringCollection);
      }
    }

    public bool PropertyNamesOnly
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public ReferralChasingOption ReferralChasing
    {
      get
      {
        return default(ReferralChasingOption);
      }
      set
      {
      }
    }

    public DirectoryEntry SearchRoot
    {
      get
      {
        return default(DirectoryEntry);
      }
      set
      {
      }
    }

    public SearchScope SearchScope
    {
      get
      {
        return default(SearchScope);
      }
      set
      {
      }
    }

    public SecurityMasks SecurityMasks
    {
      get
      {
        return default(SecurityMasks);
      }
      set
      {
      }
    }

    public TimeSpan ServerPageTimeLimit
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public TimeSpan ServerTimeLimit
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public int SizeLimit
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public SortOption Sort
    {
      get
      {
        return default(SortOption);
      }
      set
      {
      }
    }

    public bool Tombstone
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public DirectoryVirtualListView VirtualListView
    {
      get
      {
        return default(DirectoryVirtualListView);
      }
      set
      {
      }
    }
    #endregion
  }
}

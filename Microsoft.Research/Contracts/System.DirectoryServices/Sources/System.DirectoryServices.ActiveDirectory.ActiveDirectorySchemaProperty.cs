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

// File System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty.cs
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
  public partial class ActiveDirectorySchemaProperty : IDisposable
  {
    #region Methods and constructors
    public ActiveDirectorySchemaProperty(DirectoryContext context, string ldapDisplayName)
    {
      Contract.Requires(context != null);
      Contract.Requires(!string.IsNullOrEmpty(ldapDisplayName));
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public void Dispose()
    {
    }

    public static ActiveDirectorySchemaProperty FindByName(DirectoryContext context, string ldapDisplayName)
    {
      Contract.Requires(context != null);
      Contract.Requires(!string.IsNullOrEmpty(ldapDisplayName));

      Contract.Ensures(Contract.Result<ActiveDirectorySchemaProperty>() != null);

      return default(System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty);
    }

    public System.DirectoryServices.DirectoryEntry GetDirectoryEntry()
    {
      Contract.Ensures(Contract.Result<DirectoryEntry>() != null);

      return default(System.DirectoryServices.DirectoryEntry);
    }

    public void Save()
    {
    }

    #endregion

    #region Properties and indexers
    public string CommonName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string Description
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool IsDefunct
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsInAnr
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsIndexed
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsIndexedOverContainer
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsInGlobalCatalog
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsOnTombstonedObject
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsSingleValued
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsTupleIndexed
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty Link
    {
      get
      {
        return default(System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty);
      }
    }

    public Nullable<int> LinkId
    {
      get
      {
        return default(Nullable<int>);
      }
      set
      {
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
    }

    public string Oid
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Nullable<int> RangeLower
    {
      get
      {
        return default(Nullable<int>);
      }
      set
      {
      }
    }

    public Nullable<int> RangeUpper
    {
      get
      {
        return default(Nullable<int>);
      }
      set
      {
      }
    }

    public Guid SchemaGuid
    {
      get
      {
        return default(Guid);
      }
      set
      {
      }
    }

    public ActiveDirectorySyntax Syntax
    {
      get
      {
        return default(ActiveDirectorySyntax);
      }
      set
      {
      }
    }
    #endregion
  }
}

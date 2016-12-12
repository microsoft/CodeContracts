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

// File System.DirectoryServices.DirectoryEntry.cs
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
  public partial class DirectoryEntry : System.ComponentModel.Component
  {
    #region Methods and constructors
    public void Close()
    {
    }

    public void CommitChanges()
    {
    }

    public System.DirectoryServices.DirectoryEntry CopyTo(System.DirectoryServices.DirectoryEntry newParent)
    {
      Contract.Requires(newParent != null);
      Contract.Ensures(Contract.Result<DirectoryEntry>() != null);
      return default(System.DirectoryServices.DirectoryEntry);
    }

    // newname can be null
    public System.DirectoryServices.DirectoryEntry CopyTo(System.DirectoryServices.DirectoryEntry newParent, string newName)
    {
      Contract.Requires(newParent != null);
      Contract.Ensures(Contract.Result<DirectoryEntry>() != null);
      return default(System.DirectoryServices.DirectoryEntry);
    }

    public void DeleteTree()
    {
    }

    public DirectoryEntry(string path)
    {
    }

    public DirectoryEntry()
    {
    }

    public DirectoryEntry(string path, string username, string password)
    {
    }

    public DirectoryEntry(string path, string username, string password, AuthenticationTypes authenticationType)
    {
    }

    public DirectoryEntry(Object adsObject)
    {
    }

    protected override void Dispose(bool disposing)
    {
    }

    public static bool Exists(string path)
    {
      return default(bool);
    }

    public Object Invoke(string methodName, Object[] args)
    {
      Contract.Ensures(Contract.Result<object>() != null);
      return default(Object);
    }

    public Object InvokeGet(string propertyName)
    {
      return default(Object);
    }

    public void InvokeSet(string propertyName, Object[] args)
    {
    }

    public void MoveTo(System.DirectoryServices.DirectoryEntry newParent, string newName)
    {
    }

    public void MoveTo(System.DirectoryServices.DirectoryEntry newParent)
    {
    }

    public void RefreshCache()
    {
    }

    public void RefreshCache(string[] propertyNames)
    {
    }

    public void Rename(string newName)
    {
    }
    #endregion

    #region Properties and indexers
    public AuthenticationTypes AuthenticationType
    {
      get
      {
        return default(AuthenticationTypes);
      }
      set
      {
      }
    }

    public DirectoryEntries Children
    {
      get
      {
        Contract.Ensures(Contract.Result<DirectoryEntries>() != null);

        return default(DirectoryEntries);
      }
    }

    public Guid Guid
    {
      get
      {
        return default(Guid);
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
    }

    public string NativeGuid
    {
      get
      {
        return default(string);
      }
    }

    public Object NativeObject
    {
      get
      {
        return default(Object);
      }
    }

    public ActiveDirectorySecurity ObjectSecurity
    {
      get
      {
        return default(ActiveDirectorySecurity);
      }
      set
      {
      }
    }

    public DirectoryEntryConfiguration Options
    {
      get
      {
        return default(DirectoryEntryConfiguration);
      }
    }

    public System.DirectoryServices.DirectoryEntry Parent
    {
      get
      {
        Contract.Ensures(Contract.Result<DirectoryEntry>() != null);
        return default(System.DirectoryServices.DirectoryEntry);
      }
    }

    public string Password
    {
      set
      {
      }
    }

    public string Path
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public PropertyCollection Properties
    {
      get
      {
        return default(PropertyCollection);
      }
    }

    public string SchemaClassName
    {
      get
      {
        return default(string);
      }
    }

    public System.DirectoryServices.DirectoryEntry SchemaEntry
    {
      get
      {
        return default(System.DirectoryServices.DirectoryEntry);
      }
    }

    public bool UsePropertyCache
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    // may return nul
    public string Username
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

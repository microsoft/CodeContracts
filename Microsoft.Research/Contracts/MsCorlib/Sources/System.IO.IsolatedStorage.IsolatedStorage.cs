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

// File System.IO.IsolatedStorage.IsolatedStorage.cs
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


namespace System.IO.IsolatedStorage
{
  abstract public partial class IsolatedStorage : MarshalByRefObject
  {
    #region Methods and constructors
    protected abstract System.Security.Permissions.IsolatedStoragePermission GetPermission(System.Security.PermissionSet ps);

    public virtual new bool IncreaseQuotaTo(long newQuotaSize)
    {
      return default(bool);
    }

    protected void InitStore(IsolatedStorageScope scope, Type domainEvidenceType, Type assemblyEvidenceType)
    {
      Contract.Ensures(0 <= string.Empty.Length);
    }

    protected void InitStore(IsolatedStorageScope scope, Type appEvidenceType)
    {
    }

    protected IsolatedStorage()
    {
    }

    public abstract void Remove();
    #endregion

    #region Properties and indexers
    public Object ApplicationIdentity
    {
      get
      {
        return default(Object);
      }
    }

    public Object AssemblyIdentity
    {
      get
      {
        return default(Object);
      }
    }

    public virtual new long AvailableFreeSpace
    {
      get
      {
        return default(long);
      }
    }

    public virtual new ulong CurrentSize
    {
      get
      {
        return default(ulong);
      }
    }

    public Object DomainIdentity
    {
      get
      {
        return default(Object);
      }
    }

    public virtual new ulong MaximumSize
    {
      get
      {
        return default(ulong);
      }
    }

    public virtual new long Quota
    {
      get
      {
        return default(long);
      }
      internal set
      {
      }
    }

    public IsolatedStorageScope Scope
    {
      get
      {
        return default(IsolatedStorageScope);
      }
    }

    protected virtual new char SeparatorExternal
    {
      get
      {
        return default(char);
      }
    }

    protected virtual new char SeparatorInternal
    {
      get
      {
        return default(char);
      }
    }

    public virtual new long UsedSize
    {
      get
      {
        return default(long);
      }
    }
    #endregion
  }
}

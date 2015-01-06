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

// File System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaPropertyCollection.cs
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
  public partial class ActiveDirectorySchemaPropertyCollection : System.Collections.CollectionBase
  {
    #region Methods and constructors
    private ActiveDirectorySchemaPropertyCollection()
    {
    }

    public int Add(ActiveDirectorySchemaProperty schemaProperty)
    {
      Contract.Requires(schemaProperty != null);

      return default(int);
    }

    public void AddRange(ReadOnlyActiveDirectorySchemaPropertyCollection properties)
    {
      Contract.Requires(properties != null);
    }

    public void AddRange(ActiveDirectorySchemaProperty[] properties)
    {
      Contract.Requires(properties != null);
      Contract.Requires(Contract.ForAll(properties, x => x != null));
    }

    public void AddRange(ActiveDirectorySchemaPropertyCollection properties)
    {
      Contract.Requires(properties != null);
    }

    public bool Contains(ActiveDirectorySchemaProperty schemaProperty)
    {
      Contract.Requires(schemaProperty != null);
      return default(bool);
    }

    public void CopyTo(ActiveDirectorySchemaProperty[] properties, int index)
    {
      Contract.Requires(properties != null);
      Contract.Requires(index >= 0);
      Contract.Requires(index < properties.Length);
    }

    public int IndexOf(ActiveDirectorySchemaProperty schemaProperty)
    {
      Contract.Requires(schemaProperty != null);
      Contract.Ensures(Contract.Result<int>() >= -1);

      return default(int);
    }

    public void Insert(int index, ActiveDirectorySchemaProperty schemaProperty)
    {
      Contract.Requires(schemaProperty != null);
      Contract.Requires(index >= 0);
    }

    protected override void OnClearComplete()
    {
    }

    protected override void OnInsertComplete(int index, Object value)
    {
    }

    protected override void OnRemoveComplete(int index, Object value)
    {
    }

    protected override void OnSetComplete(int index, Object oldValue, Object newValue)
    {
    }

    protected override void OnValidate(Object value)
    {
    }

    public void Remove(ActiveDirectorySchemaProperty schemaProperty)
    {
    }
    #endregion

    #region Properties and indexers
    public ActiveDirectorySchemaProperty this [int index]
    {
      get
      {
        Contract.Ensures(Contract.Result<ActiveDirectorySchemaProperty>() != null);

        return default(ActiveDirectorySchemaProperty);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }
    #endregion
  }
}

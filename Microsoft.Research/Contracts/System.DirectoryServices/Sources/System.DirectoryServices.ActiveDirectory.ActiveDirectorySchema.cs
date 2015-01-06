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

// File System.DirectoryServices.ActiveDirectory.ActiveDirectorySchema.cs
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
  public partial class ActiveDirectorySchema : ActiveDirectoryPartition
  {
    #region Methods and constructors
    private ActiveDirectorySchema()
    {
    }

    protected override void Dispose(bool disposing)
    {
    }

    public ReadOnlyActiveDirectorySchemaClassCollection FindAllClasses()
    {
      Contract.Ensures(Contract.Result<ReadOnlyActiveDirectorySchemaClassCollection>() != null);

      return default(ReadOnlyActiveDirectorySchemaClassCollection);
    }

    public ReadOnlyActiveDirectorySchemaClassCollection FindAllClasses(SchemaClassType type)
    {
      Contract.Ensures(Contract.Result<ReadOnlyActiveDirectorySchemaClassCollection>() != null);

      return default(ReadOnlyActiveDirectorySchemaClassCollection);
    }

    public ReadOnlyActiveDirectorySchemaClassCollection FindAllDefunctClasses()
    {
      Contract.Ensures(Contract.Result<ReadOnlyActiveDirectorySchemaClassCollection>() != null);

      return default(ReadOnlyActiveDirectorySchemaClassCollection);
    }

    public ReadOnlyActiveDirectorySchemaPropertyCollection FindAllDefunctProperties()
    {
      Contract.Ensures(Contract.Result<ReadOnlyActiveDirectorySchemaClassCollection>() != null);

      return default(ReadOnlyActiveDirectorySchemaPropertyCollection);
    }

    public ReadOnlyActiveDirectorySchemaPropertyCollection FindAllProperties(PropertyTypes type)
    {
      Contract.Ensures(Contract.Result<ReadOnlyActiveDirectorySchemaClassCollection>() != null);

      return default(ReadOnlyActiveDirectorySchemaPropertyCollection);
    }

    public ReadOnlyActiveDirectorySchemaPropertyCollection FindAllProperties()
    {
      Contract.Ensures(Contract.Result<ReadOnlyActiveDirectorySchemaClassCollection>() != null);

      return default(ReadOnlyActiveDirectorySchemaPropertyCollection);
    }

    public ActiveDirectorySchemaClass FindClass(string ldapDisplayName)
    {
      Contract.Requires(ldapDisplayName != null);
      Contract.Requires(ldapDisplayName.Length > 0);

      Contract.Ensures(Contract.Result<ActiveDirectorySchema>() != null);
      
      return default(ActiveDirectorySchemaClass);
    }

    public ActiveDirectorySchemaClass FindDefunctClass(string commonName)
    {
      Contract.Requires(!string.IsNullOrEmpty(commonName));

      Contract.Ensures(Contract.Result<ActiveDirectorySchemaClass>() != null);

      return default(ActiveDirectorySchemaClass);
    }

    public ActiveDirectorySchemaProperty FindDefunctProperty(string commonName)
    {
      Contract.Requires(!string.IsNullOrEmpty(commonName));

      Contract.Ensures(Contract.Result<ActiveDirectorySchemaClass>() != null);
      
      return default(ActiveDirectorySchemaProperty);
    }

    public ActiveDirectorySchemaProperty FindProperty(string ldapDisplayName)
    {
      Contract.Requires(!string.IsNullOrEmpty(ldapDisplayName));

      Contract.Ensures(Contract.Result<ActiveDirectorySchemaClass>() != null);

      return default(ActiveDirectorySchemaProperty);
    }

    public static ActiveDirectorySchema GetCurrentSchema()
    {
      Contract.Ensures(Contract.Result<ActiveDirectorySchema>() != null);

      return default(ActiveDirectorySchema);
    }

    public override System.DirectoryServices.DirectoryEntry GetDirectoryEntry()
    {
      return default(System.DirectoryServices.DirectoryEntry);
    }

    public static ActiveDirectorySchema GetSchema(DirectoryContext context)
    {
      Contract.Requires(context != null);

      Contract.Ensures(Contract.Result<ActiveDirectorySchema>() != null);

      return default(ActiveDirectorySchema);
    }

    public void RefreshSchema()
    {
    }
    #endregion

    #region Properties and indexers
    public DirectoryServer SchemaRoleOwner
    {
      get
      {
        Contract.Ensures(Contract.Result<DirectoryServer>() != null);

        return default(DirectoryServer);
      }
    }
    #endregion
  }
}

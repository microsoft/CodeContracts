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

using System;
using System.Collections;

namespace System.Data
{
    using System.Diagnostics.Contracts;

    /// <summary>
  /// Represents a collection of rows for a <see cref="T:System.Data.DataTable"/>.
  /// </summary>
  /// <filterpriority>1</filterpriority>
  public sealed class DataRowCollection // : InternalDataCollectionBase
  {
    /// <summary>
    /// Gets the total number of <see cref="T:System.Data.DataRow"/> objects in this collection.
    /// </summary>
    /// 
    /// <returns>
    /// The total number of <see cref="T:System.Data.DataRow"/> objects in this collection.
    /// </returns>
    //public override int Count
    //{
    //  get
    //  {
    //    return 0;
    //  }
    //}

    /// <summary>
    /// Gets the row at the specified index.
    /// </summary>
    /// 
    /// <returns>
    /// The specified <see cref="T:System.Data.DataRow"/>.
    /// </returns>
    /// <param name="index">The zero-based index of the row to return. </param><exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*"/></PermissionSet>
    public DataRow this[int index]
    {
      get
      {
          Contract.Ensures(Contract.Result<DataRow>() != null);
          return null;
      }
    }

    /// <summary>
    /// Adds the specified <see cref="T:System.Data.DataRow"/> to the <see cref="T:System.Data.DataRowCollection"/> object.
    /// </summary>
    /// <param name="row">The <see cref="T:System.Data.DataRow"/> to add. </param><exception cref="T:System.ArgumentNullException">The row is null. </exception><exception cref="T:System.ArgumentException">The row either belongs to another table or already belongs to this table. </exception><exception cref="T:System.Data.ConstraintException">The addition invalidates a constraint. </exception><exception cref="T:System.Data.NoNullAllowedException">The addition tries to put a null in a <see cref="T:System.Data.DataColumn"/> where <see cref="P:System.Data.DataColumn.AllowDBNull"/> is false </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence"/></PermissionSet>
    public void Add(DataRow row)
    {
        Contract.Requires(row != null);
    }

    /// <summary>
    /// Inserts a new row into the collection at the specified location.
    /// </summary>
    /// <param name="row">The <see cref="T:System.Data.DataRow"/> to add. </param><param name="pos">The (zero-based) location in the collection where you want to add the DataRow. </param><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence"/></PermissionSet>
    public void InsertAt(DataRow row, int pos)
    {
        Contract.Requires(row != null);
    }

    /// <summary>
    /// Gets the index of the specified <see cref="T:System.Data.DataRow"/> object.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index of the row, or -1 if the row is not found in the collection.
    /// </returns>
    /// <param name="row">The DataRow to search for.</param><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*"/></PermissionSet>
    public int IndexOf(DataRow row)
    {
        Contract.Requires(row != null);
        Contract.Ensures(Contract.Result<int>() >= -1);
        return 0;
    }

    /// <summary>
    /// Creates a row using specified values and adds it to the <see cref="T:System.Data.DataRowCollection"/>.
    /// </summary>
    /// 
    /// <returns>
    /// None.
    /// </returns>
    /// <param name="values">The array of values that are used to create the new row. </param><exception cref="T:System.ArgumentException">The array is larger than the number of columns in the table. </exception><exception cref="T:System.InvalidCastException">A value does not match its respective column type. </exception><exception cref="T:System.Data.ConstraintException">Adding the row invalidates a constraint. </exception><exception cref="T:System.Data.NoNullAllowedException">Trying to put a null in a column where <see cref="P:System.Data.DataColumn.AllowDBNull"/> is false. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence"/></PermissionSet>
    public DataRow Add(params object[] values)
    {
        Contract.Requires(values != null);
        Contract.Ensures(Contract.Result<DataRow>() != null);
        return null;
    }

    /// <summary>
    /// Gets the row specified by the primary key value.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Data.DataRow"/> that contains the primary key value specified; otherwise a null value if the primary key value does not exist in the <see cref="T:System.Data.DataRowCollection"/>.
    /// </returns>
    /// <param name="key">The primary key value of the <see cref="T:System.Data.DataRow"/> to find. </param>
    /// <exception cref="T:System.Data.MissingPrimaryKeyException">The table does not have a primary key. </exception>
    public DataRow Find(object key)
    {
        Contract.Requires(key != null);
        return null;
    }

        /// <summary>
    /// Gets the row that contains the specified primary key values.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Data.DataRow"/> object that contains the primary key values specified; otherwise a null value if the primary key value does not exist in the <see cref="T:System.Data.DataRowCollection"/>.
    /// </returns>
    /// <param name="keys">An array of primary key values to find. The type of the array is Object. </param>
    /// <exception cref="T:System.IndexOutOfRangeException">No row corresponds to that index value. </exception>
    /// <exception cref="T:System.Data.MissingPrimaryKeyException">The table does not have a primary key. </exception>
    public DataRow Find(object[] keys)
    {
        Contract.Requires(keys != null);
        return null;
    }

        /// <summary>
    /// Clears the collection of all rows.
    /// </summary>
    /// <exception cref="T:System.Data.InvalidConstraintException">A <see cref="T:System.Data.ForeignKeyConstraint"/> is enforced on the <see cref="T:System.Data.DataRowCollection"/>. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*"/></PermissionSet>
    public void Clear()
    {
    }

    /// <summary>
    /// Gets a value that indicates whether the primary key of any row in the collection contains the specified value.
    /// </summary>
    /// 
    /// <returns>
    /// true if the collection contains a <see cref="T:System.Data.DataRow"/> with the specified primary key value; otherwise, false.
    /// </returns>
    /// <param name="key">The value of the primary key to test for. </param><exception cref="T:System.Data.MissingPrimaryKeyException">The table does not have a primary key. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*"/></PermissionSet>
    public bool Contains(object key)
    {
        Contract.Requires(key != null);
        return false;
    }

    /// <summary>
    /// Gets a value that indicates whether the primary key columns of any row in the collection contain the values specified in the object array.
    /// </summary>
    /// 
    /// <returns>
    /// true if the <see cref="T:System.Data.DataRowCollection"/> contains a <see cref="T:System.Data.DataRow"/> with the specified key values; otherwise, false.
    /// </returns>
    /// <param name="keys">An array of primary key values to test for. </param><exception cref="T:System.Data.MissingPrimaryKeyException">The table does not have a primary key. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*"/></PermissionSet>
    public bool Contains(object[] keys)
    {
        Contract.Requires(keys != null);
        return false;
    }

    /// <summary>
    /// Copies all the <see cref="T:System.Data.DataRow"/> objects from the collection into the given array, starting at the given destination array index.
    /// </summary>
    /// <param name="ar">The one-dimensional array that is the destination of the elements copied from the DataRowCollection. The array must have zero-based indexing.</param><param name="index">The zero-based index in the array at which copying begins.</param>
    //public override void CopyTo(Array ar, int index)
    //{
    //}

    /// <summary>
    /// Copies all the <see cref="T:System.Data.DataRow"/> objects from the collection into the given array, starting at the given destination array index.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied from the DataRowCollection. The array must have zero-based indexing.</param><param name="index">The zero-based index in the array at which copying begins.</param><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*"/></PermissionSet>
    public void CopyTo(DataRow[] array, int index)
    {
        Contract.Requires(array != null);
    }

        /// <summary>
    /// Gets an <see cref="T:System.Collections.IEnumerator"/> for this collection.
    /// </summary>
    /// 
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerator"/> for this collection.
    /// </returns>
    //public override IEnumerator GetEnumerator()
    //{
    //}

    /// <summary>
    /// Removes the specified <see cref="T:System.Data.DataRow"/> from the collection.
    /// </summary>
    /// <param name="row">The <see cref="T:System.Data.DataRow"/> to remove. </param><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*"/></PermissionSet>
    public void Remove(DataRow row)
    {
        Contract.Requires(row != null);
    }

    /// <summary>
    /// Removes the row at the specified index from the collection.
    /// </summary>
    /// <param name="index">The index of the row to remove. </param><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*"/></PermissionSet>
    public void RemoveAt(int index)
    {
    }
  }
}

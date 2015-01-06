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
using System.Diagnostics.Contracts;


namespace System.Data
{
  // Summary:
  //     Represents a collection of System.Data.DataColumn objects for a System.Data.DataTable.
  //[DefaultEvent("CollectionChanged")]
  //[Editor("Microsoft.VSDesigner.Data.Design.ColumnsCollectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  public sealed class DataColumnCollection //: InternalDataCollectionBase
  {
    //protected override ArrayList List { get; }

    // Summary:
    //     Gets the System.Data.DataColumn from the collection at the specified index.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the column to return.
    //
    // Returns:
    //     The System.Data.DataColumn at the specified index.
    //
    // Exceptions:
    //   System.IndexOutOfRangeException:
    //     The index value is greater than the number of items in the collection.
    //public DataColumn this//[int index] { get; }
    //
    // Summary:
    //     Gets the System.Data.DataColumn from the collection with the specified name.
    //
    // Parameters:
    //   name:
    //     The System.Data.DataColumn.ColumnName of the column to return.
    //
    // Returns:
    //     The System.Data.DataColumn in the collection with the specified System.Data.DataColumn.ColumnName;
    //     otherwise a null value if the System.Data.DataColumn does not exist.
    //public DataColumn this[string name] { get; }

    // Summary:
    //     Occurs when the columns collection changes, either by adding or removing
    //     a column.
    //[ResDescription("collectionChangedEventDescr")]
    //public event CollectionChangeEventHandler CollectionChanged;

    // Summary:
    //     Creates and adds a System.Data.DataColumn object to the System.Data.DataColumnCollection.
    //
    // Returns:
    //     The newly created System.Data.DataColumn.
    //public DataColumn Add();
    //
    // Summary:
    //     Creates and adds the specified System.Data.DataColumn object to the System.Data.DataColumnCollection.
    //
    // Parameters:
    //   column:
    //     The System.Data.DataColumn to add.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The column parameter is null.
    //
    //   System.ArgumentException:
    //     The column already belongs to this collection, or to another collection.
    //
    //   System.Data.DuplicateNameException:
    //     The collection already has a column with the specified name. (The comparison
    //     is not case-sensitive.)
    //
    //   System.Data.InvalidExpressionException:
    //     The expression is invalid. See the System.Data.DataColumn.Expression property
    //     for more information about how to create expressions.
    public void Add(DataColumn column)
    {
      Contract.Requires(column != null);
    }
    //
    // Summary:
    //     Creates and adds a System.Data.DataColumn object that has the specified name
    //     to the System.Data.DataColumnCollection.
    //
    // Parameters:
    //   columnName:
    //     The name of the column.
    //
    // Returns:
    //     The newly created System.Data.DataColumn.
    //
    // Exceptions:
    //   System.Data.DuplicateNameException:
    //     The collection already has a column with the specified name. (The comparison
    //     is not case-sensitive.)
    public DataColumn Add(string columnName)
    {
      Contract.Ensures(Contract.Result<DataColumn>() != null);

      return default(DataColumn);
    }
    //
    // Summary:
    //     Creates and adds a System.Data.DataColumn object that has the specified name
    //     and type to the System.Data.DataColumnCollection.
    //
    // Parameters:
    //   columnName:
    //     The System.Data.DataColumn.ColumnName to use when you create the column.
    //
    //   type:
    //     The System.Data.DataColumn.DataType of the new column.
    //
    // Returns:
    //     The newly created System.Data.DataColumn.
    //
    // Exceptions:
    //   System.Data.DuplicateNameException:
    //     The collection already has a column with the specified name. (The comparison
    //     is not case-sensitive.)
    //
    //   System.Data.InvalidExpressionException:
    //     The expression is invalid. See the System.Data.DataColumn.Expression property
    //     for more information about how to create expressions.
    public DataColumn Add(string columnName, Type type)
          {
      Contract.Ensures(Contract.Result<DataColumn>() != null);

      return default(DataColumn);
    }
    //
    // Summary:
    //     Creates and adds a System.Data.DataColumn object that has the specified name,
    //     type, and expression to the System.Data.DataColumnCollection.
    //
    // Parameters:
    //   columnName:
    //     The name to use when you create the column.
    //
    //   type:
    //     The System.Data.DataColumn.DataType of the new column.
    //
    //   expression:
    //     The expression to assign to the System.Data.DataColumn.Expression property.
    //
    // Returns:
    //     The newly created System.Data.DataColumn.
    //
    // Exceptions:
    //   System.Data.DuplicateNameException:
    //     The collection already has a column with the specified name. (The comparison
    //     is not case-sensitive.)
    //
    //   System.Data.InvalidExpressionException:
    //     The expression is invalid. See the System.Data.DataColumn.Expression property
    //     for more information about how to create expressions.
    public DataColumn Add(string columnName, Type type, string expression)
          {
      Contract.Ensures(Contract.Result<DataColumn>() != null);

      return default(DataColumn);
    }
    //
    // Summary:
    //     Copies the elements of the specified System.Data.DataColumn array to the
    //     end of the collection.
    //
    // Parameters:
    //   columns:
    //     The array of System.Data.DataColumn objects to add to the collection.
    //public void AddRange(DataColumn[] columns);
    //
    // Summary:
    //     Checks whether a specific column can be removed from the collection.
    //
    // Parameters:
    //   column:
    //     A System.Data.DataColumn in the collection.
    //
    // Returns:
    //     true if the column can be removed; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The column parameter is null.
    //
    //   System.ArgumentException:
    //     The column does not belong to this collection.-Or- The column is part of
    //     a relationship.-Or- Another column's expression depends on this column.
    public bool CanRemove(DataColumn column)
    {
      Contract.Requires(column != null);

      return default(bool);
    }
    //
    // Summary:
    //     Clears the collection of any columns.
    //public void Clear();
    //
    // Summary:
    //     Checks whether the collection contains a column with the specified name.
    //
    // Parameters:
    //   name:
    //     The System.Data.DataColumn.ColumnName of the column to look for.
    //
    // Returns:
    //     true if a column exists with this name; otherwise, false.
    //public bool Contains(string name);
    //
    // Summary:
    //     Copies the entire collection into an existing array, starting at a specified
    //     index within the array.
    //
    // Parameters:
    //   array:
    //     An array of System.Data.DataColumn objects to copy the collection into.
    //
    //   index:
    //     The index to start from.
    //public void CopyTo(DataColumn//[] array, int index);
    //
    // Summary:
    //     Gets the index of a column specified by name.
    //
    // Parameters:
    //   column:
    //     The name of the column to return.
    //
    // Returns:
    //     The index of the column specified by column if it is found; otherwise, -1.
    //public int IndexOf(DataColumn column);
    //
    // Summary:
    //     Gets the index of the column with the specific name (the name is not case
    //     sensitive).
    //
    // Parameters:
    //   columnName:
    //     The name of the column to find.
    //
    // Returns:
    //     The zero-based index of the column with the specified name, or -1 if the
    //     column does not exist in the collection.
    //public int IndexOf(string columnName);
    //
    // Summary:
    //     Removes the specified System.Data.DataColumn object from the collection.
    //
    // Parameters:
    //   column:
    //     The System.Data.DataColumn to remove.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The column parameter is null.
    //
    //   System.ArgumentException:
    //     The column does not belong to this collection.-Or- The column is part of
    //     a relationship.-Or- Another column's expression depends on this column.
    //public void Remove(DataColumn column);
    //
    // Summary:
    //     Removes the System.Data.DataColumn object that has the specified name from
    //     the collection.
    //
    // Parameters:
    //   name:
    //     The name of the column to remove.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The collection does not have a column with the specified name.
    //public void Remove(string name);
    //
    // Summary:
    //     Removes the column at the specified index from the collection.
    //
    // Parameters:
    //   index:
    //     The index of the column to remove.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The collection does not have a column at the specified index.
    //public void RemoveAt(int index);
  }
}

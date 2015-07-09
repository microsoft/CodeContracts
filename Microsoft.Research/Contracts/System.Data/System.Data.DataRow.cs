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
using System.Xml;
using System.Diagnostics.Contracts;

namespace System.Data
{
  // Summary:
  //     Represents a row of data in a System.Data.DataTable.
  public class DataRow
  {
    // Summary:
    //     Initializes a new instance of the DataRow. Constructs a row from the builder.
    //     Only for internal usage..
    //
    // Parameters:
    //   builder:
    //     builder
    //protected internal DataRow(DataRowBuilder builder);

    // Summary:
    //     Gets a value that indicates whether there are errors in a row.
    //
    // Returns:
    //     true if the row contains an error; otherwise, false.
    //public bool HasErrors { get; }
    //
    // Summary:
    //     Gets or sets all the values for this row through an array.
    //
    // Returns:
    //     An array of type System.Object.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The array is larger than the number of columns in the table.
    //
    //   System.InvalidCastException:
    //     A value in the array does not match its System.Data.DataColumn.DataType in
    //     its respective System.Data.DataColumn.
    //
    //   System.Data.ConstraintException:
    //     An edit broke a constraint.
    //
    //   System.Data.ReadOnlyException:
    //     An edit tried to change the value of a read-only column.
    //
    //   System.Data.NoNullAllowedException:
    //     An edit tried to put a null value in a column where System.Data.DataColumn.AllowDBNull
    //     of the System.Data.DataColumn object is false.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     The row has been deleted.
    public object[] ItemArray
    {
      get
      {
        Contract.Ensures(Contract.Result<object[]>() != null);

        return default(object[]);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }
    //
    // Summary:
    //     Gets or sets the custom error description for a row.
    //
    // Returns:
    //     The text describing an error.
    //public string RowError { get; set; }
    //
    // Summary:
    //     Gets the current state of the row with regard to its relationship to the
    //     System.Data.DataRowCollection.
    //
    // Returns:
    //     One of the System.Data.DataRowState values.
    //public DataRowState RowState { get; }
    //
    // Summary:
    //     Gets the System.Data.DataTable for which this row has a schema.
    //
    // Returns:
    //     The System.Data.DataTable to which this row belongs.
    public DataTable Table
    {
      get
      {
        Contract.Ensures(Contract.Result<DataTable>() != null);
        return null;
      }
    }

    // Summary:
    //     Gets or sets the data stored in the specified System.Data.DataColumn.
    //
    // Parameters:
    //   column:
    //     A System.Data.DataColumn that contains the data.
    //
    // Returns:
    //     An System.Object that contains the data.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The column does not belong to this table.
    //
    //   System.ArgumentNullException:
    //     The column is null.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to set a value on a deleted row.
    //
    //   System.InvalidCastException:
    //     The data types of the value and the column do not match.
    //public object this//[DataColumn column] { get; set; }
    //
    // Summary:
    //     Gets or sets the data stored in the column specified by index.
    //
    // Parameters:
    //   columnIndex:
    //     The zero-based index of the column.
    //
    // Returns:
    //     An System.Object that contains the data.
    //
    // Exceptions:
    //   System.Data.DeletedRowInaccessibleException:
    //     Occurs when you try to set a value on a deleted row.
    //
    //   System.IndexOutOfRangeException:
    //     The columnIndex argument is out of range.
    //
    //   System.InvalidCastException:
    //     Occurs when you set the value and the new value's System.Type does not match
    //     System.Data.DataColumn.DataType.
    //public object this//[int columnIndex] { get; set; }
    //
    // Summary:
    //     Gets or sets the data stored in the column specified by name.
    //
    // Parameters:
    //   columnName:
    //     The name of the column.
    //
    // Returns:
    //     An System.Object that contains the data.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The column specified by columnName cannot be found.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     Occurs when you try to set a value on a deleted row.
    //
    //   System.InvalidCastException:
    //     Occurs when you set a value and its System.Type does not match System.Data.DataColumn.DataType.
    //public object this//[string columnName] { get; set; }
    //
    // Summary:
    //     Gets the specified version of data stored in the specified System.Data.DataColumn.
    //
    // Parameters:
    //   column:
    //     A System.Data.DataColumn that contains information about the column.
    //
    //   version:
    //     One of the System.Data.DataRowVersion values that specifies the row version
    //     that you want. Possible values are Default, Original, Current, and Proposed.
    //
    // Returns:
    //     An System.Object that contains the data.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The column does not belong to the table.
    //
    //   System.ArgumentNullException:
    //     The column argument contains null.
    //
    //   System.Data.VersionNotFoundException:
    //     The row does not have this version of data.
    //public object this//[DataColumn column, DataRowVersion version] { get; }
    //
    // Summary:
    //     Gets the data stored in the column, specified by index and version of the
    //     data to retrieve.
    //
    // Parameters:
    //   columnIndex:
    //     The zero-based index of the column.
    //
    //   version:
    //     One of the System.Data.DataRowVersion values that specifies the row version
    //     that you want. Possible values are Default, Original, Current, and Proposed.
    //
    // Returns:
    //     An System.Object that contains the data.
    //
    // Exceptions:
    //   System.IndexOutOfRangeException:
    //     The columnIndex argument is out of range.
    //
    //   System.InvalidCastException:
    //     The data types of the value and the column do not match.
    //
    //   System.Data.VersionNotFoundException:
    //     The row does not have this version of data.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to set a value on a deleted row.
    //public object this//[int columnIndex, DataRowVersion version] { get; }
    //
    // Summary:
    //     Gets the specified version of data stored in the named column.
    //
    // Parameters:
    //   columnName:
    //     The name of the column.
    //
    //   version:
    //     One of the System.Data.DataRowVersion values that specifies the row version
    //     that you want. Possible values are Default, Original, Current, and Proposed.
    //
    // Returns:
    //     An System.Object that contains the data.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The column specified by columnName cannot be found.
    //
    //   System.InvalidCastException:
    //     The data types of the value and the column do not match.
    //
    //   System.Data.VersionNotFoundException:
    //     The row does not have this version of data.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     The row was deleted.
    //public object this//[string columnName, DataRowVersion version] { get; }

    // Summary:
    //     Commits all the changes made to this row since the last time System.Data.DataRow.AcceptChanges()
    //     was called.
    //
    // Exceptions:
    //   System.Data.RowNotInTableException:
    //     The row does not belong to the table.
    //public void AcceptChanges();
    //
    // Summary:
    //     Starts an edit operation on a System.Data.DataRow object.
    //
    // Exceptions:
    //   System.Data.InRowChangingEventException:
    //     The method was called inside the System.Data.DataTable.RowChanging event.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     The method was called upon a deleted row.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public void BeginEdit();
    //
    // Summary:
    //     Cancels the current edit on the row.
    //
    // Exceptions:
    //   System.Data.InRowChangingEventException:
    //     The method was called inside the System.Data.DataTable.RowChanging event.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public void CancelEdit();
    //
    // Summary:
    //     Clears the errors for the row. This includes the System.Data.DataRow.RowError
    //     and errors set with System.Data.DataRow.SetColumnError(System.Int32,System.String).
    //public void ClearErrors();
    //
    // Summary:
    //     Deletes the System.Data.DataRow.
    //
    // Exceptions:
    //   System.Data.DeletedRowInaccessibleException:
    //     The System.Data.DataRow has already been deleted.
    //public void Delete();
    //
    // Summary:
    //     Ends the edit occurring on the row.
    //
    // Exceptions:
    //   System.Data.InRowChangingEventException:
    //     The method was called inside the System.Data.DataTable.RowChanging event.
    //
    //   System.Data.ConstraintException:
    //     The edit broke a constraint.
    //
    //   System.Data.ReadOnlyException:
    //     The row belongs to the table and the edit tried to change the value of a
    //     read-only column.
    //
    //   System.Data.NoNullAllowedException:
    //     The edit tried to put a null value into a column where System.Data.DataColumn.AllowDBNull
    //     is false.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public void EndEdit();
    //
    // Summary:
    //     Gets the child rows of this System.Data.DataRow using the specified System.Data.DataRelation.
    //
    // Parameters:
    //   relation:
    //     The System.Data.DataRelation to use.
    //
    // Returns:
    //     An array of System.Data.DataRow objects or an array of length zero.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The relation and row do not belong to the same table.
    //
    //   System.ArgumentNullException:
    //     The relation is null.
    //
    //   System.Data.RowNotInTableException:
    //     The row does not belong to the table.
    //
    //   System.Data.VersionNotFoundException:
    //     The row does not have this version of data.
    public DataRow[] GetChildRows(DataRelation relation)
    {
      Contract.Ensures(Contract.Result<DataRow[]>() != null);

      return default(DataRow[]);
    }
    //
    // Summary:
    //     Gets the child rows of a System.Data.DataRow using the specified System.Data.DataRelation.RelationName
    //     of a System.Data.DataRelation.
    //
    // Parameters:
    //   relationName:
    //     The System.Data.DataRelation.RelationName of the System.Data.DataRelation
    //     to use.
    //
    // Returns:
    //     An array of System.Data.DataRow objects or an array of length zero.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The relation and row do not belong to the same table.
    //
    //   System.Data.RowNotInTableException:
    //     The row does not belong to the table.
    public DataRow[] GetChildRows(string relationName)
          {
      Contract.Ensures(Contract.Result<DataRow[]>() != null);

      return default(DataRow[]);
    }
    //
    // Summary:
    //     Gets the child rows of a System.Data.DataRow using the specified System.Data.DataRelation,
    //     and System.Data.DataRowVersion.
    //
    // Parameters:
    //   relation:
    //     The System.Data.DataRelation to use.
    //
    //   version:
    //     One of the System.Data.DataRowVersion values specifying the version of the
    //     data to get. Possible values are Default, Original, Current, and Proposed.
    //
    // Returns:
    //     An array of System.Data.DataRow objects.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The relation and row do not belong to the same table.
    //
    //   System.ArgumentNullException:
    //     The relation is null.
    //
    //   System.Data.RowNotInTableException:
    //     The row does not belong to the table.
    //
    //   System.Data.VersionNotFoundException:
    //     The row does not have the requested System.Data.DataRowVersion.
    public DataRow[] GetChildRows(DataRelation relation, DataRowVersion version)
          {
      Contract.Ensures(Contract.Result<DataRow[]>() != null);

      return default(DataRow[]);
    }
    //
    // Summary:
    //     Gets the child rows of a System.Data.DataRow using the specified System.Data.DataRelation.RelationName
    //     of a System.Data.DataRelation, and System.Data.DataRowVersion.
    //
    // Parameters:
    //   relationName:
    //     The System.Data.DataRelation.RelationName of the System.Data.DataRelation
    //     to use.
    //
    //   version:
    //     One of the System.Data.DataRowVersion values specifying the version of the
    //     data to get. Possible values are Default, Original, Current, and Proposed.
    //
    // Returns:
    //     An array of System.Data.DataRow objects or an array of length zero.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The relation and row do not belong to the same table.
    //
    //   System.ArgumentNullException:
    //     The relation is null.
    //
    //   System.Data.RowNotInTableException:
    //     The row does not belong to the table.
    //
    //   System.Data.VersionNotFoundException:
    //     The row does not have the requested System.Data.DataRowVersion.
    public DataRow[] GetChildRows(string relationName, DataRowVersion version)
          {
      Contract.Ensures(Contract.Result<DataRow[]>() != null);

      return default(DataRow[]);
    }

    //
    // Summary:
    //     Gets the error description of the specified System.Data.DataColumn.
    //
    // Parameters:
    //   column:
    //     A System.Data.DataColumn.
    //
    // Returns:
    //     The text of the error description.
    public string GetColumnError(DataColumn column)
    {     
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    //
    // Summary:
    //     Gets the error description for the column specified by index.
    //
    // Parameters:
    //   columnIndex:
    //     The zero-based index of the column.
    //
    // Returns:
    //     The text of the error description.
    //
    // Exceptions:
    //   System.IndexOutOfRangeException:
    //     The columnIndex argument is out of range.
    public string GetColumnError(int columnIndex)
          {     
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    //
    // Summary:
    //     Gets the error description for a column, specified by name.
    //
    // Parameters:
    //   columnName:
    //     The name of the column.
    //
    // Returns:
    //     The text of the error description.
    public string GetColumnError(string columnName)
          {     
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    //
    // Summary:
    //     Gets an array of columns that have errors.
    //
    // Returns:
    //     An array of System.Data.DataColumn objects that contain errors.
    public DataColumn[] GetColumnsInError()
          {     
      Contract.Ensures(Contract.Result<DataColumn[]>() != null);

      return default(DataColumn[]);
    }
    //
    // Summary:
    //     Gets the parent row of a System.Data.DataRow using the specified System.Data.DataRelation.
    //
    // Parameters:
    //   relation:
    //     The System.Data.DataRelation to use.
    //
    // Returns:
    //     The parent System.Data.DataRow of the current row.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The relation does not belong to the System.Data.DataTable.The row is null.
    //
    //   System.Data.InvalidConstraintException:
    //     This row does not belong to the child table of the System.Data.DataRelation
    //     object.
    //
    //   System.Data.RowNotInTableException:
    //     The row does not belong to a table.
    //public DataRow GetParentRow(DataRelation relation);
    //
    // Summary:
    //     Gets the parent row of a System.Data.DataRow using the specified System.Data.DataRelation.RelationName
    //     of a System.Data.DataRelation.
    //
    // Parameters:
    //   relationName:
    //     The System.Data.DataRelation.RelationName of a System.Data.DataRelation.
    //
    // Returns:
    //     The parent System.Data.DataRow of the current row.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The relation and row do not belong to the same table.
    //
    //   System.Data.RowNotInTableException:
    //     The row does not belong to the table.
    //public DataRow GetParentRow(string relationName);
    //
    // Summary:
    //     Gets the parent row of a System.Data.DataRow using the specified System.Data.DataRelation,
    //     and System.Data.DataRowVersion.
    //
    // Parameters:
    //   relation:
    //     The System.Data.DataRelation to use.
    //
    //   version:
    //     One of the System.Data.DataRowVersion values specifying the version of the
    //     data to get.
    //
    // Returns:
    //     The parent System.Data.DataRow of the current row.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The row is null.The relation does not belong to this table's parent relations.
    //
    //   System.Data.InvalidConstraintException:
    //     The relation's child table is not the table the row belongs to.
    //
    //   System.Data.RowNotInTableException:
    //     The row does not belong to a table.
    //
    //   System.Data.VersionNotFoundException:
    //     The row does not have this version of data.
    //public DataRow GetParentRow(DataRelation relation, DataRowVersion version);
    //
    // Summary:
    //     Gets the parent row of a System.Data.DataRow using the specified System.Data.DataRelation.RelationName
    //     of a System.Data.DataRelation, and System.Data.DataRowVersion.
    //
    // Parameters:
    //   relationName:
    //     The System.Data.DataRelation.RelationName of a System.Data.DataRelation.
    //
    //   version:
    //     One of the System.Data.DataRowVersion values.
    //
    // Returns:
    //     The parent System.Data.DataRow of the current row.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The relation and row do not belong to the same table.
    //
    //   System.ArgumentNullException:
    //     The relation is null.
    //
    //   System.Data.RowNotInTableException:
    //     The row does not belong to the table.
    //
    //   System.Data.VersionNotFoundException:
    //     The row does not have the requested System.Data.DataRowVersion.
    //public DataRow GetParentRow(string relationName, DataRowVersion version);
    //
    // Summary:
    //     Gets the parent rows of a System.Data.DataRow using the specified System.Data.DataRelation.
    //
    // Parameters:
    //   relation:
    //     The System.Data.DataRelation to use.
    //
    // Returns:
    //     An array of System.Data.DataRow objects or an array of length zero.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Data.DataRelation does not belong to this row's System.Data.DataSet.
    //
    //   System.ArgumentNullException:
    //     The row is null.
    //
    //   System.Data.InvalidConstraintException:
    //     The relation's child table is not the table the row belongs to.
    //
    //   System.Data.RowNotInTableException:
    //     The row does not belong to a System.Data.DataTable.
    //public DataRow[] GetParentRows(DataRelation relation);
    //
    // Summary:
    //     Gets the parent rows of a System.Data.DataRow using the specified System.Data.DataRelation.RelationName
    //     of a System.Data.DataRelation.
    //
    // Parameters:
    //   relationName:
    //     The System.Data.DataRelation.RelationName of a System.Data.DataRelation.
    //
    // Returns:
    //     An array of System.Data.DataRow objects or an array of length zero.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The relation and row do not belong to the same table.
    //
    //   System.Data.RowNotInTableException:
    //     The row does not belong to the table.
    //public DataRow[] GetParentRows(string relationName);
    //
    // Summary:
    //     Gets the parent rows of a System.Data.DataRow using the specified System.Data.DataRelation,
    //     and System.Data.DataRowVersion.
    //
    // Parameters:
    //   relation:
    //     The System.Data.DataRelation to use.
    //
    //   version:
    //     One of the System.Data.DataRowVersion values specifying the version of the
    //     data to get.
    //
    // Returns:
    //     An array of System.Data.DataRow objects or an array of length zero.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Data.DataRelation does not belong to this row's System.Data.DataSet.
    //
    //   System.ArgumentNullException:
    //     The row is null.
    //
    //   System.Data.InvalidConstraintException:
    //     The relation's child table is not the table the row belongs to.
    //
    //   System.Data.RowNotInTableException:
    //     The row does not belong to a System.Data.DataTable.
    //
    //   System.Data.VersionNotFoundException:
    //     The row does not have the requested System.Data.DataRowVersion.
    //public DataRow[] GetParentRows(DataRelation relation, DataRowVersion version);
    //
    // Summary:
    //     Gets the parent rows of a System.Data.DataRow using the specified System.Data.DataRelation.RelationName
    //     of a System.Data.DataRelation, and System.Data.DataRowVersion.
    //
    // Parameters:
    //   relationName:
    //     The System.Data.DataRelation.RelationName of a System.Data.DataRelation.
    //
    //   version:
    //     One of the System.Data.DataRowVersion values specifying the version of the
    //     data to get. Possible values are Default, Original, Current, and Proposed.
    //
    // Returns:
    //     An array of System.Data.DataRow objects or an array of length zero.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The relation and row do not belong to the same table.
    //
    //   System.ArgumentNullException:
    //     The relation is null.
    //
    //   System.Data.RowNotInTableException:
    //     The row does not belong to the table.
    //
    //   System.Data.VersionNotFoundException:
    //     The row does not have the requested System.Data.DataRowVersion.
    //public DataRow[] GetParentRows(string relationName, DataRowVersion version);
    //
    // Summary:
    //     Gets a value that indicates whether a specified version exists.
    //
    // Parameters:
    //   version:
    //     One of the System.Data.DataRowVersion values that specifies the row version.
    //
    // Returns:
    //     true if the version exists; otherwise, false.
    //public bool HasVersion(DataRowVersion version);
    //
    // Summary:
    //     Gets a value that indicates whether the specified System.Data.DataColumn
    //     contains a null value.
    //
    // Parameters:
    //   column:
    //     A System.Data.DataColumn.
    //
    // Returns:
    //     true if the column contains a null value; otherwise, false.
    //public bool IsNull(DataColumn column);
    //
    // Summary:
    //     Gets a value that indicates whether the column at the specified index contains
    //     a null value.
    //
    // Parameters:
    //   columnIndex:
    //     The zero-based index of the column.
    //
    // Returns:
    //     true if the column contains a null value; otherwise, false.
    //public bool IsNull(int columnIndex);
    //
    // Summary:
    //     Gets a value that indicates whether the named column contains a null value.
    //
    // Parameters:
    //   columnName:
    //     The name of the column.
    //
    // Returns:
    //     true if the column contains a null value; otherwise, false.
    //public bool IsNull(string columnName);
    //
    // Summary:
    //     Gets a value that indicates whether the specified System.Data.DataColumn
    //     and System.Data.DataRowVersion contains a null value.
    //
    // Parameters:
    //   column:
    //     A System.Data.DataColumn.
    //
    //   version:
    //     One of the System.Data.DataRowVersion values that specifies the row version.
    //     Possible values are Default, Original, Current, and Proposed.
    //
    // Returns:
    //     true if the column contains a null value; otherwise, false.
    //public bool IsNull(DataColumn column, DataRowVersion version);
    //
    // Summary:
    //     Rejects all changes made to the row since System.Data.DataRow.AcceptChanges()
    //     was last called.
    //
    // Exceptions:
    //   System.Data.RowNotInTableException:
    //     The row does not belong to the table.
    //public void RejectChanges();
    //
    // Summary:
    //     Changes the System.Data.DataRow.Rowstate of a System.Data.DataRow to Added.
    //public void SetAdded();
    //
    // Summary:
    //     Sets the error description for a column specified as a System.Data.DataColumn.
    //
    // Parameters:
    //   column:
    //     The System.Data.DataColumn to set the error description for.
    //
    //   error:
    //     The error description.
    //public void SetColumnError(DataColumn column, string error);
    //
    // Summary:
    //     Sets the error description for a column specified by index.
    //
    // Parameters:
    //   columnIndex:
    //     The zero-based index of the column.
    //
    //   error:
    //     The error description.
    //
    // Exceptions:
    //   System.IndexOutOfRangeException:
    //     The columnIndex argument is out of range
    //public void SetColumnError(int columnIndex, string error);
    //
    // Summary:
    //     Sets the error description for a column specified by name.
    //
    // Parameters:
    //   columnName:
    //     The name of the column.
    //
    //   error:
    //     The error description.
    //public void SetColumnError(string columnName, string error);
    //
    // Summary:
    //     Changes the System.Data.DataRow.Rowstate of a System.Data.DataRow to Modified.
    //public void SetModified();
    //
    // Summary:
    //     Sets the value of the specified System.Data.DataColumn to a null value.
    //
    // Parameters:
    //   column:
    //     A System.Data.DataColumn.
    //protected void SetNull(DataColumn column);
    //
    // Summary:
    //     Sets the parent row of a System.Data.DataRow with specified new parent System.Data.DataRow.
    //
    // Parameters:
    //   parentRow:
    //     The new parent System.Data.DataRow.
    //public void SetParentRow(DataRow parentRow);
    //
    // Summary:
    //     Sets the parent row of a System.Data.DataRow with specified new parent System.Data.DataRow
    //     and System.Data.DataRelation.
    //
    // Parameters:
    //   parentRow:
    //     The new parent System.Data.DataRow.
    //
    //   relation:
    //     The relation System.Data.DataRelation to use.
    //
    // Exceptions:
    //   System.Data.RowNotInTableException:
    //     One of the rows does not belong to a table
    //
    //   System.ArgumentNullException:
    //     One of the rows is null.
    //
    //   System.ArgumentException:
    //     The relation does not belong to the System.Data.DataRelationCollection of
    //     the System.Data.DataSet object.
    //
    //   System.Data.InvalidConstraintException:
    //     The relation's child System.Data.DataTable is not the table this row belongs
    //     to.
    //public void SetParentRow(DataRow parentRow, DataRelation relation);
  }
}


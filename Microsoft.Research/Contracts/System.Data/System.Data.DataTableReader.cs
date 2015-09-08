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

namespace System.Data
{
  // Summary:
  //     The System.Data.DataTableReader obtains the contents of one or more System.Data.DataTable
  //     objects in the form of one or more read-only, forward-only result sets.
  public sealed class DataTableReader// : DbDataReader
  {
    // Summary:
    //     Initializes a new instance of the System.Data.DataTableReader class by using
    //     data from the supplied System.Data.DataTable.
    //
    // Parameters:
    //   dataTable:
    //     The System.Data.DataTable from which the new System.Data.DataTableReader
    //     obtains its result set.
    //public DataTableReader(DataTable dataTable);
    //
    // Summary:
    //     Initializes a new instance of the System.Data.DataTableReader class using
    //     the supplied array of System.Data.DataTable objects.
    //
    // Parameters:
    //   dataTables:
    //     The array of System.Data.DataTable objects that supplies the results for
    //     the new System.Data.DataTableReader object.
    //public DataTableReader(DataTable[] dataTables);

    // Summary:
    //     The depth of nesting for the current row of the System.Data.DataTableReader.
    //
    // Returns:
    //     The depth of nesting for the current row; always zero.
    // public override int Depth { get; }
    //
    // Summary:
    //     Returns the number of columns in the current row.
    //
    // Returns:
    //     When not positioned in a valid result set, 0; otherwise the number of columns
    //     in the current row.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     An attempt was made to retrieve the field count in a closed System.Data.DataTableReader.
    // public override int FieldCount { get; }
    //
    // Summary:
    //     Gets a value that indicates whether the System.Data.DataTableReader contains
    //     one or more rows.
    //
    // Returns:
    //     true if the System.Data.DataTableReader contains one or more rows; otherwise
    //     false.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     An attempt was made to retrieve information about a closed System.Data.DataTableReader.
    // public override bool HasRows { get; }
    //
    // Summary:
    //     Gets a value that indicates whether the System.Data.DataTableReader is closed.
    //
    // Returns:
    //     Returns true if the System.Data.DataTableReader is closed; otherwise, false.
    // public override bool IsClosed { get; }
    //
    // Summary:
    //     Gets the number of rows inserted, changed, or deleted by execution of the
    //     SQL statement.
    //
    // Returns:
    //     The System.Data.DataTableReader does not support this property and always
    //     returns 0.
    // public override int RecordsAffected { get; }

    // Summary:
    //     Gets the value of the specified column in its native format given the column
    //     ordinal.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column in its native format.
    //
    // Exceptions:
    //   System.IndexOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    // public override object this[int ordinal] { get; }
    //
    // Summary:
    //     Gets the value of the specified column in its native format given the column
    //     name.
    //
    // Parameters:
    //   name:
    //     The name of the column.
    //
    // Returns:
    //     The value of the specified column in its native format.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The name specified is not a valid column name.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader.
    // public override object this[string name] { get; }

    // Summary:
    //     Closes the current System.Data.DataTableReader.
    // public override void Close();
    //
    // Summary:
    //     Gets the value of the specified column as a System.Boolean.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader.
    //
    //   System.InvalidCastException:
    //     The specified column does not contain a Boolean.
    // public override bool GetBoolean(int ordinal);
    //
    // Summary:
    //     Gets the value of the specified column as a byte.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed DataTableReader.
    //
    //   System.InvalidCastException:
    //     The specified column does not contain a byte.
    // public override byte GetByte(int ordinal);
    //
    // Summary:
    //     Reads a stream of bytes starting at the specified column offset into the
    //     buffer as an array starting at the specified buffer offset.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    //   dataIndex:
    //     The index within the field from which to start the read operation.
    //
    //   buffer:
    //     The buffer into which to read the stream of bytes.
    //
    //   bufferIndex:
    //     The index within the buffer at which to start placing the data.
    //
    //   length:
    //     The maximum length to copy into the buffer.
    //
    // Returns:
    //     The actual number of bytes read.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed DataTableReader.
    //
    //   System.InvalidCastException:
    //     The specified column does not contain a byte array.
    // public override long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length);
    //
    // Summary:
    //     Gets the value of the specified column as a character.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the column.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed DataTableReader.
    //
    //   System.InvalidCastException:
    //     The specified field does not contain a character.
    // public override char GetChar(int ordinal);
    //
    // Summary:
    //     Returns the value of the specified column as a character array.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    //   dataIndex:
    //     The index within the field from which to start the read operation.
    //
    //   buffer:
    //     The buffer into which to read the stream of chars.
    //
    //   bufferIndex:
    //     The index within the buffer at which to start placing the data.
    //
    //   length:
    //     The maximum length to copy into the buffer.
    //
    // Returns:
    //     The actual number of characters read.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed DataTableReader.
    //
    //   System.InvalidCastException:
    //     The specified column does not contain a character array.
    // public override long GetChars(int ordinal, long dataIndex, char[] buffer, int bufferIndex, int length);
    //
    // Summary:
    //     Gets a string representing the data type of the specified column.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A string representing the column's data type.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader.
    // public override string GetDataTypeName(int ordinal);
    //
    // Summary:
    //     Gets the value of the specified column as a System.DateTime object.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed DataTableReader.
    //
    //   System.InvalidCastException:
    //     The specified column does not contain a DateTime value.
    // public override DateTime GetDateTime(int ordinal);
    //
    // Summary:
    //     Gets the value of the specified column as a System.Decimal.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed DataTableReader.
    //
    //   System.InvalidCastException:
    //     The specified column does not contain a Decimal value.
    // public override decimal GetDecimal(int ordinal);
    //
    // Summary:
    //     Gets the value of the column as a double-precision floating point number.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based ordinal of the column.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed DataTableReader.
    //
    //   System.InvalidCastException:
    //     The specified column does not contain a double-precision floating point number.
    // public override double GetDouble(int ordinal);
    //
    // Summary:
    //     Returns an enumerator that can be used to iterate through the item collection.
    //
    // Returns:
    //     An System.Collections.IEnumerator object that represents the item collection.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader.
    // public override IEnumerator GetEnumerator();
    //
    // Summary:
    //     Gets the System.Type that is the data type of the object.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The System.Type that is the data type of the object.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader
    //     .
    // public override Type GetFieldType(int ordinal);
    //
    // Summary:
    //     Gets the value of the specified column as a single-precision floating point
    //     number.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the column.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader.
    //
    //   System.InvalidCastException:
    //     The specified column does not contain a single-precision floating point number.
    // public override float GetFloat(int ordinal);
    //
    // Summary:
    //     Gets the value of the specified column as a globally-unique identifier (GUID).
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader.
    //
    //   System.InvalidCastException:
    //     The specified column does not contain a GUID.
    // public override Guid GetGuid(int ordinal);
    //
    // Summary:
    //     Gets the value of the specified column as a 16-bit signed integer.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader.
    //
    //   System.InvalidCastException:
    //     The specified column does not contain a 16-bit signed integer.
    // public override short GetInt16(int ordinal);
    //
    // Summary:
    //     Gets the value of the specified column as a 32-bit signed integer.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader
    //     .
    //
    //   System.InvalidCastException:
    //     The specified column does not contain a 32-bit signed integer value.
    // public override int GetInt32(int ordinal);
    //
    // Summary:
    //     Gets the value of the specified column as a 64-bit signed integer.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader
    //     .
    //
    //   System.InvalidCastException:
    //     The specified column does not contain a 64-bit signed integer value.
    // public override long GetInt64(int ordinal);
    //
    // Summary:
    //     Gets the value of the specified column as a System.String.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal
    //
    // Returns:
    //     The name of the specified column.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader.
    // public override string GetName(int ordinal);
    //
    // Summary:
    //     Gets the column ordinal, given the name of the column.
    //
    // Parameters:
    //   name:
    //     The name of the column.
    //
    // Returns:
    //     The zero-based column ordinal.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader.
    //
    //   System.ArgumentException:
    //     The name specified is not a valid column name.
    // public override int GetOrdinal(string name);
    //
    // Summary:
    //     Gets the type of the specified column in provider-specific format.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The System.Type that is the data type of the object.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader.
    // public override Type GetProviderSpecificFieldType(int ordinal);
    //
    // Summary:
    //     Gets the value of the specified column in provider-specific format.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based number of the column whose value is retrieved.
    //
    // Returns:
    //     The value of the specified column in provider-specific format.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader
    // public override object GetProviderSpecificValue(int ordinal);
    //
    // Summary:
    //     Fills the supplied array with provider-specific type information for all
    //     the columns in the System.Data.DataTableReader.
    //
    // Parameters:
    //   values:
    //     An array of objects to be filled in with type information for the columns
    //     in the System.Data.DataTableReader.
    //
    // Returns:
    //     The number of column values copied into the array.
    //
    // Exceptions:
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader.
    // public override int GetProviderSpecificValues(object[] values);
    //
    // Summary:
    //     Returns a System.Data.DataTable that describes the column metadata of the
    //     System.Data.DataTableReader.
    //
    // Returns:
    //     A System.Data.DataTable that describes the column metadata.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Data.DataTableReader is closed.
    // public override DataTable GetSchemaTable();
    //
    // Summary:
    //     Gets the value of the specified column as a string.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader.
    //
    //   System.InvalidCastException:
    //     The specified column does not contain a string.
    // public override string GetString(int ordinal);
    //
    // Summary:
    //     Gets the value of the specified column in its native format.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal
    //
    // Returns:
    //     The value of the specified column. This method returns DBNull for null columns.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access columns in a closed System.Data.DataTableReader
    //     .
    // public override object GetValue(int ordinal);
    //
    // Summary:
    //     Gets an array of Objects that contains the values from the current row in
    //     the DataTableReader.
    //
    // Parameters:
    //   values:
    //     An array of System.Object into which to copy the column values from the System.Data.DataTableReader.
    //
    // Returns:
    //     The number of column values copied into the array.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader
    //     .
    // public override int GetValues(object[] values);
    //
    // Summary:
    //     Gets a value that indicates whether the column contains non-existent or missing
    //     values.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal
    //
    // Returns:
    //     true if the specified column value is equivalent to System.DBNull; otherwise,
    //     false.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1.
    //
    //   System.Data.DeletedRowInaccessibleException:
    //     An attempt was made to retrieve data from a deleted row.
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader
    //     .
    // public override bool IsDBNull(int ordinal);
    //
    // Summary:
    //     Advances the System.Data.DataTableReader to the next result set, if any.
    //
    // Returns:
    //     true if there was another result set; otherwise false.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     An attempt was made to navigate within a closed System.Data.DataTableReader.
    // public override bool NextResult();
    //
    // Summary:
    //     Advances the System.Data.DataTableReader to the next record.
    //
    // Returns:
    //     true if there was another row to read; otherwise false.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     An attempt was made to read or access a column in a closed System.Data.DataTableReader
    //     .
    // public override bool Read();
  }
}

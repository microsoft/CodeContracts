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
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Reflection;
using System.Diagnostics.Contracts;

namespace System.Data.SqlClient
{
  // Summary:
  //     Provides a way of reading a forward-only stream of rows from a SQL Server
  //     database. This class cannot be inherited.
  public class SqlDataReader//  : DbDataReader, IDataReader, IDisposable, IDataRecord
  {
    // Summary:
    //     Gets the System.Data.SqlClient.SqlConnection associated with the System.Data.SqlClient.SqlDataReader.
    //
    // Returns:
    //     The System.Data.SqlClient.SqlConnection associated with the System.Data.SqlClient.SqlDataReader.
    //protected SqlConnection Connection { get; }
    //
    // Summary:
    //     Gets a value that indicates the depth of nesting for the current row.
    //
    // Returns:
    //     The depth of nesting for the current row.
    ////public override int Depth { get; }
    //
    // Summary:
    //     Gets the number of columns in the current row.
    //
    // Returns:
    //     When not positioned in a valid recordset, 0; otherwise the number of columns
    //     in the current row. The default is -1.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     There is no current connection to an instance of SQL Server.
    ////public override int FieldCount { get; }
    //
    // Summary:
    //     Gets a value that indicates whether the System.Data.SqlClient.SqlDataReader
    //     contains one or more rows.
    //
    // Returns:
    //     true if the System.Data.SqlClient.SqlDataReader contains one or more rows;
    //     otherwise false.
    ////public override bool HasRows { get; }
    //
    // Summary:
    //     Retrieves a Boolean value that indicates whether the specified System.Data.SqlClient.SqlDataReader
    //     instance has been closed.
    //
    // Returns:
    //     true if the specified System.Data.SqlClient.SqlDataReader instance is closed;
    //     otherwise false.
    ////public override bool IsClosed { get; }
    //
    // Summary:
    //     Gets the number of rows changed, inserted, or deleted by execution of the
    //     Transact-SQL statement.
    //
    // Returns:
    //     The number of rows changed, inserted, or deleted; 0 if no rows were affected
    //     or the statement failed; and -1 for SELECT statements.
    ////public override int RecordsAffected { get; }
    //
    // Summary:
    //     Gets the number of fields in the System.Data.SqlClient.SqlDataReader that
    //     are not hidden.
    //
    // Returns:
    //     The number of fields that are not hidden.
    ////public override int VisibleFieldCount { get; }

    // Summary:
    //     Gets the value of the specified column in its native format given the column
    //     ordinal.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column in its native format.
    //
    // Exceptions:
    //   System.IndexOutOfRangeException:
    //     The index passed was outside the range of 0 through System.Data.IDataRecord.FieldCount.
    ////public override object this[int i] { get; }
    //
    // Summary:
    //     Gets the value of the specified column in its native format given the column
    //     name.
    //
    // Parameters:
    //   name:
    //     The column name.
    //
    // Returns:
    //     The value of the specified column in its native format.
    //
    // Exceptions:
    //   System.IndexOutOfRangeException:
    //     No column with the specified name was found.
    ////public override object this[string name] { get; }

    // Summary:
    //     Closes the System.Data.SqlClient.SqlDataReader object.
    ////public override void Close();
    //
    // Summary:
    //     Gets the value of the specified column as a Boolean.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the column.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     The specified cast is not valid.
    ////public override bool GetBoolean(int i);
    //
    // Summary:
    //     Gets the value of the specified column as a byte.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column as a byte.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     The specified cast is not valid.
    ////public override byte GetByte(int i);
    //
    // Summary:
    //     Reads a stream of bytes from the specified column offset into the buffer
    //     an array starting at the given buffer offset.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    //   dataIndex:
    //     The index within the field from which to begin the read operation.
    //
    //   buffer:
    //     The buffer into which to read the stream of bytes.
    //
    //   bufferIndex:
    //     The index within the buffer where the write operation is to start.
    //
    //   length:
    //     The maximum length to copy into the buffer.
    //
    // Returns:
    //     The actual number of bytes read.
    ////public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length);
    //
    // Summary:
    //     Gets the value of the specified column as a single character.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     The specified cast is not valid.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    ////public override char GetChar(int i);
    //
    // Summary:
    //     Reads a stream of characters from the specified column offset into the buffer
    //     as an array starting at the given buffer offset.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    //   dataIndex:
    //     The index within the field from which to begin the read operation.
    //
    //   buffer:
    //     The buffer into which to read the stream of bytes.
    //
    //   bufferIndex:
    //     The index within the buffer where the write operation is to start.
    //
    //   length:
    //     The maximum length to copy into the buffer.
    //
    // Returns:
    //     The actual number of characters read.
    //public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length);
    //
    // Summary:
    //     Gets a string representing the data type of the specified column.
    //
    // Parameters:
    //   i:
    //     The zero-based ordinal position of the column to find.
    //public override string GetDataTypeName(int i);
    //
    // Summary:
    //     Gets the value of the specified column as a System.DateTime object.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     The specified cast is not valid.
    //public override DateTime GetDateTime(int i);
    //
    // Summary:
    //     Retrieves the value of the specified column as a System.DateTimeOffset object.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     The specified cast is not valid.
    //public virtual DateTimeOffset GetDateTimeOffset(int i);
    //
    // Summary:
    //     Gets the value of the specified column as a System.Decimal object.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     The specified cast is not valid.
    //public override decimal GetDecimal(int i);
    //
    // Summary:
    //     Gets the value of the specified column as a double-precision floating point
    //     number.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     The specified cast is not valid.
    //public override double GetDouble(int i);
    //
    // Summary:
    //     Returns an System.Collections.IEnumerator that iterates through the System.Data.SqlClient.SqlDataReader.
    //
    // Returns:
    //     An System.Collections.IEnumerator for the System.Data.SqlClient.SqlDataReader.
    //public override IEnumerator GetEnumerator();
    //
    // Summary:
    //     Gets the System.Type that is the data type of the object.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The System.Type that is the data type of the object. If the type does not
    //     exist on the client, in the case of a User-Defined Type (UDT) returned from
    //     the database, GetFieldType returns null.
    //public override Type GetFieldType(int i);
    //
    // Summary:
    //     Gets the value of the specified column as a single-precision floating point
    //     number.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     The specified cast is not valid.
    //public override float GetFloat(int i);
    //
    // Summary:
    //     Gets the value of the specified column as a globally unique identifier (GUID).
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     The specified cast is not valid.
    //public override Guid GetGuid(int i);
    //
    // Summary:
    //     Gets the value of the specified column as a 16-bit signed integer.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     The specified cast is not valid.
    //public override short GetInt16(int i);
    //
    // Summary:
    //     Gets the value of the specified column as a 32-bit signed integer.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     The specified cast is not valid.
    //public override int GetInt32(int i);
    //
    // Summary:
    //     Gets the value of the specified column as a 64-bit signed integer.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     The specified cast is not valid.
    //public override long GetInt64(int i);
    //
    // Summary:
    //     Gets the name of the specified column.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The name of the specified column.
    //public override string GetName(int i);
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
    //   System.IndexOutOfRangeException:
    //     The name specified is not a valid column name.
    //public override int GetOrdinal(string name);
    //
    // Summary:
    //     Gets an Object that is a representation of the underlying provider-specific
    //     field type.
    //
    // Parameters:
    //   i:
    //     An System.Int32 representing the column ordinal.
    //
    // Returns:
    //     Gets an System.Object that is a representation of the underlying provider-specific
    //     field type.
    //public override Type GetProviderSpecificFieldType(int i);
    //
    // Summary:
    //     Gets an Object that is a representation of the underlying provider specific
    //     value.
    //
    // Parameters:
    //   i:
    //     An System.Int32 representing the column ordinal.
    //
    // Returns:
    //     An System.Object that is a representation of the underlying provider specific
    //     value.
    //public override object GetProviderSpecificValue(int i);
    //
    // Summary:
    //     Gets an array of objects that are a representation of the underlying provider
    //     specific values.
    //
    // Parameters:
    //   values:
    //     An array of System.Object into which to copy the column values.
    //
    // Returns:
    //     The array of objects that are a representation of the underlying provider
    //     specific values.
    //public override int GetProviderSpecificValues(object[] values);
    //
    // Summary:
    //     Returns a System.Data.DataTable that describes the column metadata of the
    //     System.Data.SqlClient.SqlDataReader.
    //
    // Returns:
    //     A System.Data.DataTable that describes the column metadata.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Data.SqlClient.SqlDataReader is closed.
    //public override DataTable GetSchemaTable();
    //
    // Summary:
    //     Gets the value of the specified column as a System.Data.SqlTypes.SqlBinary.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlBinary.
    public virtual SqlBinary GetSqlBinary(int i)
    {
      Contract.Requires(i >= 0);

      return default(SqlBinary);
    }
    //
    // Summary:
    //     Gets the value of the specified column as a System.Data.SqlTypes.SqlBoolean.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the column.
    public virtual SqlBoolean GetSqlBoolean(int i)
    {
      Contract.Requires(i >= 0);

      return default(SqlBoolean);
    }

    // Summary:
    //     Gets the value of the specified column as a System.Data.SqlTypes.SqlByte.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlByte.
    public virtual SqlByte GetSqlByte(int i)
    {
      Contract.Requires(i >= 0);

      return default(SqlByte);
    }
      
      //
    // Summary:
    //     Gets the value of the specified column as System.Data.SqlTypes.SqlBytes.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlBytes.
    public virtual SqlBytes GetSqlBytes(int i)
  {
    {
      Contract.Requires(i >= 0);

      return default(SqlBytes);
    }
  }
    //
    // Summary:
    //     Gets the value of the specified column as System.Data.SqlTypes.SqlChars.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlChars.
    public virtual SqlChars GetSqlChars(int i)
    {
      {
        Contract.Requires(i >= 0);

        return default(SqlChars);
      }
    }
    //
    // Summary:
    //     Gets the value of the specified column as a System.Data.SqlTypes.SqlDateTime.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlDateTime.
    public virtual SqlDateTime GetSqlDateTime(int i)
        {
      Contract.Requires(i >= 0);

      return default(SqlDateTime);
    }
    
    //
    // Summary:
    //     Gets the value of the specified column as a System.Data.SqlTypes.SqlDecimal.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlDecimal.
    public virtual SqlDecimal GetSqlDecimal(int i)
    {
        Contract.Requires(i >= 0);

        return default(SqlDecimal);
    }
    
    // Summary:
    //     Gets the value of the specified column as a System.Data.SqlTypes.SqlDouble.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlDouble.
    public virtual SqlDouble GetSqlDouble(int i)
    {
      Contract.Requires(i >= 0);

      return default(SqlDouble);
    }
    
    //
    // Summary:
    //     Gets the value of the specified column as a System.Data.SqlTypes.SqlGuid.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlGuid.
    public virtual SqlGuid GetSqlGuid(int i)
    {
      Contract.Requires(i >= 0);

      return default(SqlGuid);
    }
        
    //
    // Summary:
    //     Gets the value of the specified column as a System.Data.SqlTypes.SqlInt16.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlInt16.
    public virtual SqlInt16 GetSqlInt16(int i)
    {
      Contract.Requires(i >= 0);

      return default(SqlInt16);
    }
    
    //
    // Summary:
    //     Gets the value of the specified column as a System.Data.SqlTypes.SqlInt32.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlInt32.
    public virtual SqlInt32 GetSqlInt32(int i)
    {
      Contract.Requires(i >= 0);

      return default(SqlInt32);
    }
    
    
    //
    // Summary:
    //     Gets the value of the specified column as a System.Data.SqlTypes.SqlInt64.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlInt64.
    public virtual SqlInt64 GetSqlInt64(int i)
    {
      Contract.Requires(i >= 0);

      return default(SqlInt64);
    }

    //
    // Summary:
    //     Gets the value of the specified column as a System.Data.SqlTypes.SqlMoney.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlMoney.
    public virtual SqlMoney GetSqlMoney(int i)
    {
      Contract.Requires(i >= 0);

      return default(SqlMoney);
    }
    //
    // Summary:
    //     Gets the value of the specified column as a System.Data.SqlTypes.SqlSingle.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlSingle.
    public virtual SqlSingle GetSqlSingle(int i)
    {
      Contract.Requires(i >= 0);

      return default(SqlSingle);
    }
    //
    // Summary:
    //     Gets the value of the specified column as a System.Data.SqlTypes.SqlString.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlString.
    public virtual SqlString GetSqlString(int i)
    {
      Contract.Requires(i >= 0);

      return default(SqlString);
    }
    //
    // Summary:
    //     Returns the data value in the specified column as a SQL Server type.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the column expressed as a System.Data.SqlDbType.
    public virtual object GetSqlValue(int i)
    {
      {
        Contract.Requires(i >= 0);

        return default(object);
      }
    }
    //
    // Summary:
    //     Fills an array of System.Object that contains the values for all the columns
    //     in the record, expressed as SQL Server types.
    //
    // Parameters:
    //   values:
    //     An array of System.Object into which to copy the values. The column values
    //     are expressed as SQL Server types.
    //
    // Returns:
    //     An integer indicating the number of columns copied.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     values is null.
    public virtual int GetSqlValues(object[] values)
    {
      Contract.Requires(values != null);

      return default(int);
    }
    //
    // Summary:
    //     Gets the value of the specified column as an XML value.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlXml value that contains the XML stored within the
    //     corresponding field.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index passed was outside the range of 0 to System.Data.DataTableReader.FieldCount
    //     - 1
    //
    //   System.InvalidOperationException:
    //     An attempt was made to read or access columns in a closed System.Data.SqlClient.SqlDataReader.
    //
    //   System.InvalidCastException:
    //     The retrieved data is not compatible with the System.Data.SqlTypes.SqlXml
    //     type.
    public virtual SqlXml GetSqlXml(int i)
    {
      Contract.Requires(i >= 0);

      return default(SqlXml);
    }
    //
    // Summary:
    //     Gets the value of the specified column as a string.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     The specified cast is not valid.
    //public override string GetString(int i);
    //
    // Summary:
    //     Retrieves the value of the specified column as a System.TimeSpan object.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     The specified cast is not valid.
    public virtual TimeSpan GetTimeSpan(int i)
    {
      Contract.Requires(i >= 0);

      return default(TimeSpan);
    }
    //
    // Summary:
    //     Gets the value of the specified column in its native format.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     This method returns System.DBNull for null database columns.
    //public override object GetValue(int i);
    //
    // Summary:
    //     Gets all attribute columns in the collection for the current row.
    //
    // Parameters:
    //   values:
    //     An array of System.Object into which to copy the attribute columns.
    //
    // Returns:
    //     The number of instances of System.Object in the array.
    //public override int GetValues(object[] values);
    //
    // Summary:
    //     Determines whether the specified System.Data.CommandBehavior matches that
    //     of the System.Data.SqlClient.SqlDataReader .
    //
    // Parameters:
    //   condition:
    //     A System.Data.CommandBehavior enumeration.
    //
    // Returns:
    //     true if the specified System.Data.CommandBehavior is true, false otherwise.
    //protected bool IsCommandBehavior(CommandBehavior condition);
    //
    // Summary:
    //     Gets a value that indicates whether the column contains non-existent or missing
    //     values.
    //
    // Parameters:
    //   i:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     true if the specified column value is equivalent to System.DBNull; otherwise
    //     false.
    //public override bool IsDBNull(int i);
    //
    // Summary:
    //     Advances the data reader to the next result, when reading the results of
    //     batch Transact-SQL statements.
    //
    // Returns:
    //     true if there are more result sets; otherwise false.
    //public override bool NextResult();
    //
    // Summary:
    //     Advances the System.Data.SqlClient.SqlDataReader to the next record.
    //
    // Returns:
    //     true if there are more rows; otherwise false.
    //public override bool Read();
  }
}

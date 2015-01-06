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
using System.Reflection;
using System.Diagnostics.Contracts;

namespace System.Data.Common
{
  // Summary:
  //     Reads a forward-only stream of rows from a data source.
  public /*abstract*/ class DbDataReader// : MarshalByRefObject, IDataReader, IDisposable, IDataRecord, IEnumerable
  {
    protected DbDataReader() { }

    // Summary:
    //     Initializes a new instance of the System.Data.Common.DbDataReader class.
    //protected DbDataReader();

    // Summary:
    //     Gets a value indicating the depth of nesting for the current row.
    //
    // Returns:
    //     The depth of nesting for the current row.
    public virtual int Depth
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
    }
    //
    // Summary:
    //     Gets the number of columns in the current row.
    //
    // Returns:
    //     The number of columns in the current row.
    public virtual int FieldCount
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
    }
    
    //
    // Summary:
    //     Gets a value that indicates whether this System.Data.Common.DbDataReader
    //     contains one or more rows.
    //
    // Returns:
    //     true if the System.Data.Common.DbDataReader contains one or more rows; otherwise
    //     false.
    //public virtual bool HasRows { get; }
    //
    // Summary:
    //     Gets a value indicating whether the System.Data.Common.DbDataReader is closed.
    //
    // Returns:
    //     true if the System.Data.Common.DbDataReader is closed; otherwise false.
    //public virtual bool IsClosed { get; }
    //
    // Summary:
    //     Gets the number of rows changed, inserted, or deleted by execution of the
    //     SQL statement.
    //
    // Returns:
    //     The number of rows changed, inserted, or deleted. -1 for SELECT statements;
    //     0 if no rows were affected or the statement failed.
    public virtual int RecordsAffected
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= -1);

        return default(int);
      }
    }
    //
    // Summary:
    //     Gets the number of fields in the System.Data.Common.DbDataReader that are
    //     not hidden.
    //
    // Returns:
    //     The number of fields that are not hidden.
    public virtual int VisibleFieldCount
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
    }

    // Summary:
    //     Gets the value of the specified column as an instance of System.Object.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    public virtual object this[int ordinal]
    {
      [Pure]
      get
      {
        Contract.Requires(ordinal >= 0);

        return default(object);
      }
    }  
    
    //
    // Summary:
    //     Gets the value of the specified column as an instance of System.Object.
    //
    // Parameters:
    //   name:
    //     The name of the column.
    //
    // Returns:
    //     The value of the specified column.
    //public virtual object this[string name] { get; }

    // Summary:
    //     Closes the System.Data.Common.DbDataReader object.
    //public virtual void Close();
    //
    // Summary:
    //     Releases all resources used by the current instance of the System.Data.Common.DbDataReader
    //     class.
   // [EditorBrowsable(EditorBrowsableState.Never)]
    //public void Dispose();
    //
    // Summary:
    //     Releases the managed resources used by the System.Data.Common.DbDataReader
    //     and optionally releases the unmanaged resources.
    //
    // Parameters:
    //   disposing:
    //     true to release managed and unmanaged resources; false to release only unmanaged
    //     resources.
    //protected virtual void Dispose(bool disposing);
    //
    // Summary:
    //     Gets the value of the specified column as a Boolean.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    [Pure]
    public virtual bool GetBoolean(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);

      return default(bool);
    }
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
    [Pure]
    public virtual  byte GetByte(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);

      return default(byte);
    }
    //
    // Summary:
    //     Reads a stream of bytes from the specified column, starting at location indicated
    //     by dataOffset, into the buffer, starting at the location indicated by bufferOffset.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    //   dataOffset:
    //     The index within the row from which to begin the read operation.
    //
    //   buffer:
    //     The buffer into which to copy the data.
    //
    //   bufferOffset:
    //     The index with the buffer to which the data will be copied.
    //
    //   length:
    //     The maximum number of characters to read.
    //
    // Returns:
    //     The actual number of bytes read.
    [Pure]
    public virtual long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);
      Contract.Requires(dataOffset >= 0);
      Contract.Requires(bufferOffset >= 0);

      return default(long);
    }
    //
    // Summary:
    //     Gets the value of the specified column as a single character.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    [Pure]
    public virtual char GetChar(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);

      return default(char);
    }
    //
    // Summary:
    //     Reads a stream of characters from the specified column, starting at location
    //     indicated by dataIndex, into the buffer, starting at the location indicated
    //     by bufferIndex.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    //   dataOffset:
    //     The index within the row from which to begin the read operation.
    //
    //   buffer:
    //     The buffer into which to copy the data.
    //
    //   bufferOffset:
    //     The index with the buffer to which the data will be copied.
    //
    //   length:
    //     The maximum number of characters to read.
    //
    // Returns:
    //     The actual number of characters read.
    [Pure]
    public virtual long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);
      Contract.Requires(dataOffset >= 0);
      Contract.Requires(bufferOffset >= 0);

      return default(long);
    }
    //
    // Summary:
    //     Returns a System.Data.Common.DbDataReader object for the requested column
    //     ordinal.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A System.Data.Common.DbDataReader object.
   // [EditorBrowsable(EditorBrowsableState.Never)]
    [Pure]
    public DbDataReader GetData(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
      // F: not sure of the ordinal < this.FieldCount here

      return default(DbDataReader);
    }
    //
    // Summary:
    //     Gets name of the data type of the specified column.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A string representing the name of the data type.
    [Pure]
    public virtual string GetDataTypeName(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);

      return default(string);
    }
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
    [Pure]
    public virtual DateTime GetDateTime(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);

      return default(DateTime);
    }
    //
    // Summary:
    //     Returns a System.Data.Common.DbDataReader object for the requested column
    //     ordinal that can be overridden with a provider-specific implementation.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     A System.Data.Common.DbDataReader object.
    //protected virtual DbDataReader GetDbDataReader(int ordinal);
    //
    // Summary:
    //     Gets the value of the specified column as a System.Decimal object.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    [Pure]
    public virtual decimal GetDecimal(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);

      return default(decimal);
    }
    //
    // Summary:
    //     Gets the value of the specified column as a double-precision floating point
    //     number.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    [Pure]
    public virtual double GetDouble(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);

      return default(double);
    }
    //
    // Summary:
    //     Returns an System.Collections.IEnumerator that can be used to iterate through
    //     the rows in the data reader.
    //
    // Returns:
    //     An System.Collections.IEnumerator that can be used to iterate through the
    //     rows in the data reader.
   // [EditorBrowsable(EditorBrowsableState.Never)]
    [Pure]
    public virtual IEnumerator GetEnumerator()
    {
      Contract.Ensures(Contract.Result<IEnumerator>() != null);

      return default(IEnumerator);
    }
    //
    // Summary:
    //     Gets the data type of the specified column.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The data type of the specified column.
    [Pure]
    public virtual Type GetFieldType(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);

      return default(Type);
    }
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
    //     The value of the specified column.
    [Pure]
    public virtual float GetFloat(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);

      return default(float);
    }
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
    [Pure]
    public virtual Guid GetGuid(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);

      return default(Guid);
    }
    //
    // Summary:
    //     Gets the value of the specified column as a 16-bit signed integer.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    [Pure]
    public virtual short GetInt16(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);

      return default(short);
    }
    //
    // Summary:
    //     Gets the value of the specified column as a 32-bit signed integer.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    [Pure]
    public virtual int GetInt32(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);

      return default(Int32);
    }
    //
    // Summary:
    //     Gets the value of the specified column as a 64-bit signed integer.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    [Pure]
    public virtual long GetInt64(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);

      return default(Int64);
    }
    //
    // Summary:
    //     Gets the name of the column, given the zero-based column ordinal.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The name of the specified column.
    [Pure]
    public virtual string GetName(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);

      return default(string);
    }
    //
    // Summary:
    //     Gets the column ordinal given the name of the column.
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
    [Pure]
    public virtual int GetOrdinal(string name)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() < this.FieldCount);


      return default(int);
    }
    //
    // Summary:
    //     Returns the provider-specific field type of the specified column.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The System.Type object that describes the data type of the specified column.
   // [EditorBrowsable(EditorBrowsableState.Never)]
    [Pure]
    public virtual Type GetProviderSpecificFieldType(int ordinal)
    {
      {
        Contract.Requires(ordinal >= 0);
        Contract.Requires(ordinal < this.FieldCount);

        return default(Type);
      }
    }
    //
    // Summary:
    //     Gets the value of the specified column as an instance of System.Object.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
   // [EditorBrowsable(EditorBrowsableState.Never)]
    [Pure]
    public virtual object GetProviderSpecificValue(int ordinal)
    {
        Contract.Requires(ordinal >= 0);
        Contract.Requires(ordinal < this.FieldCount);

        return default(string);
    }
    //
    // Summary:
    //     Gets all provider-specific attribute columns in the collection for the current
    //     row.
    //
    // Parameters:
    //   values:
    //     An array of System.Object into which to copy the attribute columns.
    //
    // Returns:
    //     The number of instances of System.Object in the array.
   // [EditorBrowsable(EditorBrowsableState.Never)]
    //public virtual int GetProviderSpecificValues(object[] values);
    //
    // Summary:
    //     Returns a System.Data.DataTable that describes the column metadata of the
    //     System.Data.Common.DbDataReader.
    //
    // Returns:
    //     A System.Data.DataTable that describes the column metadata.
    //public virtual DataTable GetSchemaTable();
    //
    // Summary:
    //     Gets the value of the specified column as an instance of System.String.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    [Pure]
    public virtual string GetString(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);

      return default(string);
    }
    //
    // Summary:
    //     Gets the value of the specified column as an instance of System.Object.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     The value of the specified column.
    [Pure]
    public virtual object GetValue(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
      Contract.Requires(ordinal < this.FieldCount);

      return default(object);
    }
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
    [Pure]
    public virtual int GetValues(object[] values)
    {
      Contract.Requires(values != null);

      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    //
    // Summary:
    //     Gets a value that indicates whether the column contains nonexistent or missing
    //     values.
    //
    // Parameters:
    //   ordinal:
    //     The zero-based column ordinal.
    //
    // Returns:
    //     true if the specified column is equivalent to System.DBNull; otherwise false.
    [Pure]
    public virtual bool IsDBNull(int ordinal)
    {
      return default(bool);
    }
    
    //
    // Summary:
    //     Advances the reader to the next result when reading the results of a batch
    //     of statements.
    //
    // Returns:
    //     true if there are more result sets; otherwise false.
    //public virtual bool NextResult();
    //
    // Summary:
    //     Advances the reader to the next record in a result set.
    //
    // Returns:
    //     true if there are more rows; otherwise false.
    //public virtual bool Read();
  }
}

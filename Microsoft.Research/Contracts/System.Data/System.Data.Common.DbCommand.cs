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
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;

namespace System.Data.Common
{
  // Summary:
  //     Represents an SQL statement or stored procedure to execute against a data
  //     source. Provides a base class for database-specific classes that represent
  //     commands.
  public abstract class DbCommand // : Component, IDbCommand, IDisposable
  {
    // Summary:
    //     Gets or sets the text command to run against the data source.
    //
    // Returns:
    //     The text command to execute. The default value is an empty string ("").
    //[ResDescription("DbCommand_CommandText")]
    //[ResCategory("DataCategory_Data")]
    //[DefaultValue("")]
    //[RefreshProperties(RefreshProperties.All)]
    //public abstract string CommandText { get; set; }
    //
    // Summary:
    //     Gets or sets the wait time before terminating the attempt to execute a command
    //     and generating an error.
    //
    // Returns:
    //     The time in seconds to wait for the command to execute.
    //[ResDescription("DbCommand_CommandTimeout")]
    //[ResCategory("DataCategory_Data")]
    //public abstract int CommandTimeout { get; set; }
    //
    // Summary:
    //     Indicates or specifies how the System.Data.Common.DbCommand.CommandText property
    //     is interpreted.
    //
    // Returns:
    //     One of the System.Data.CommandType values. The default is Text.
    //[ResCategory("DataCategory_Data")]
    //[RefreshProperties(RefreshProperties.All)]
    //[ResDescription("DbCommand_CommandType")]
    //public abstract CommandType CommandType { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Data.Common.DbConnection used by this System.Data.Common.DbCommand.
    //
    // Returns:
    //     The connection to the data source.
    //[ResCategory("DataCategory_Data")]
    //[Browsable(false)]
    //[DefaultValue("")]
    //[ResDescription("DbCommand_Connection")]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public DbConnection Connection { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Data.Common.DbConnection used by this System.Data.Common.DbCommand.
    //
    // Returns:
    //     The connection to the data source.
    //protected abstract DbConnection DbConnection { get; set; }
    //
    // Summary:
    //     Gets the collection of System.Data.Common.DbParameter objects.
    //
    // Returns:
    //     The parameters of the SQL statement or stored procedure.
    //protected abstract DbParameterCollection DbParameterCollection { get; }
    //
    // Summary:
    //     Gets or sets the System.Data.Common.DbCommand.DbTransaction within which
    //     this System.Data.Common.DbCommand object executes.
    //
    // Returns:
    //     The transaction within which a Command object of a .NET Framework data provider
    //     executes. The default value is a null reference (Nothing in Visual Basic).
    //protected abstract DbTransaction DbTransaction { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the command object should be visible
    //     in a customized interface control.
    //
    // Returns:
    //     true, if the command object should be visible in a control; otherwise false.
    //     The default is true.
    //[DefaultValue(true)]
    //[DesignOnly(true)]
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public abstract bool DesignTimeVisible { get; set; }
    //
    // Summary:
    //     Gets the collection of System.Data.Common.DbParameter objects.
    //
    // Returns:
    //     The parameters of the SQL statement or stored procedure.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[ResDescription("DbCommand_Parameters")]
    //[Browsable(false)]
    //[ResCategory("DataCategory_Data")]
    public DbParameterCollection Parameters 
    {
      get
      {
        Contract.Ensures(Contract.Result<DbParameterCollection>() != null);

        return default(DbParameterCollection);
      }
    }
    
    
    //
    // Summary:
    //     Gets or sets the System.Data.Common.DbTransaction within which this System.Data.Common.DbCommand
    //     object executes.
    //
    // Returns:
    //     The transaction within which a Command object of a .NET Framework data provider
    //     executes. The default value is a null reference (Nothing in Visual Basic).
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //[DefaultValue("")]
    //[ResDescription("DbCommand_Transaction")]
    //public DbTransaction Transaction { get; set; }
    //
    // Summary:
    //     Gets or sets how command results are applied to the System.Data.DataRow when
    //     used by the Update method of a System.Data.Common.DbDataAdapter.
    //
    // Returns:
    //     One of the System.Data.UpdateRowSource values. The default is Both unless
    //     the command is automatically generated. Then the default is None.
    //[ResCategory("DataCategory_Update")]
    //[ResDescription("DbCommand_UpdatedRowSource")]
    //public abstract UpdateRowSource UpdatedRowSource { get; set; }

    // Summary:
    //     Attempts to cancels the execution of a System.Data.Common.DbCommand.
    //public abstract void Cancel();
    //
    // Summary:
    //     Creates a new instance of a System.Data.Common.DbParameter object.
    //
    // Returns:
    //     A System.Data.Common.DbParameter object.
    protected virtual DbParameter CreateDbParameter()
    {
      Contract.Ensures(Contract.Result<DbParameter>() != null);

      return default(DbParameter);
    }
    //
    // Summary:
    //     Creates a new instance of a System.Data.Common.DbParameter object.
    //
    // Returns:
    //     A System.Data.Common.DbParameter object.
    public DbParameter CreateParameter()
    {
      Contract.Ensures(Contract.Result<DbParameter>() != null);

      return default(DbParameter);
    }
    //
    // Summary:
    //     Executes the command text against the connection.
    //
    // Parameters:
    //   behavior:
    //     An instance of System.Data.CommandBehavior.
    //
    // Returns:
    //     A System.Data.Common.DbDataReader.
    //protected abstract DbDataReader ExecuteDbDataReader(CommandBehavior behavior);
    //
    // Summary:
    //     Executes a SQL statement against a connection object.
    //
    // Returns:
    //     The number of rows affected.
    //public abstract int ExecuteNonQuery();
    //
    // Summary:
    //     Executes the System.Data.Common.DbCommand.CommandText against the System.Data.Common.DbCommand.Connection,
    //     and returns an System.Data.Common.DbDataReader.
    //
    // Returns:
    //     A System.Data.Common.DbDataReader object.
    public DbDataReader ExecuteReader()
    {
      Contract.Ensures(Contract.Result<DbDataReader>() != null);

      return default(DbDataReader);
    }
    //
    // Summary:
    //     Executes the System.Data.Common.DbCommand.CommandText against the System.Data.Common.DbCommand.Connection,
    //     and returns an System.Data.Common.DbDataReader using one of the System.Data.CommandBehavior
    //     values.
    //
    // Parameters:
    //   behavior:
    //     One of the System.Data.CommandBehavior values.
    //
    // Returns:
    //     An System.Data.Common.DbDataReader object.
    public DbDataReader ExecuteReader(CommandBehavior behavior)
    {
      Contract.Ensures(Contract.Result<DbDataReader>() != null);

      return default(DbDataReader);
    }
    //
    // Summary:
    //     Executes the query and returns the first column of the first row in the result
    //     set returned by the query. All other columns and rows are ignored.
    //
    // Returns:
    //     The first column of the first row in the result set.
    //public abstract object ExecuteScalar();
    //
    // Summary:
    //     Creates a prepared (or compiled) version of the command on the data source.
    //public abstract void Prepare();
  }
}

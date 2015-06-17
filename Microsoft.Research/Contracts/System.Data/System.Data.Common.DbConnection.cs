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
  //     Represents a connection to a database.
  public abstract class DbConnection //: Component, IDbConnection, IDisposable
  {

    // Summary:
    //     Gets or sets the string used to open the connection.
    //
    // Returns:
    //     The connection string used to establish the initial connection. The exact
    //     contents of the connection string depend on the specific data source for
    //     this connection. The default value is an empty string.
    //[DefaultValue("")]
    //[RecommendedAsConfigurable(true)]
    //[ResCategory("DataCategory_Data")]
    //[RefreshProperties(RefreshProperties.All)]
    //public abstract string ConnectionString { get; set; }
    //
    // Summary:
    //     Gets the time to wait while establishing a connection before terminating
    //     the attempt and generating an error.
    //
    // Returns:
    //     The time (in seconds) to wait for a connection to open. The default value
    //     is determined by the specific type of connection that you are using.
    //[ResCategory("DataCategory_Data")]
    //public virtual int ConnectionTimeout { get; }
    //
    // Summary:
    //     Gets the name of the current database after a connection is opened, or the
    //     database name specified in the connection string before the connection is
    //     opened.
    //
    // Returns:
    //     The name of the current database or the name of the database to be used after
    //     a connection is opened. The default value is an empty string.
    //[ResCategory("DataCategory_Data")]
    //public abstract string Database { get; }
    //
    // Summary:
    //     Gets the name of the database server to which to connect.
    //
    // Returns:
    //     The name of the database server to which to connect. The default value is
    //     an empty string.
    //[ResCategory("DataCategory_Data")]
    //public abstract string DataSource { get; }
    //
    // Summary:
    //     Gets the System.Data.Common.DbProviderFactory for this System.Data.Common.DbConnection.
    //
    // Returns:
    //     A System.Data.Common.DbProviderFactory.
    //protected virtual DbProviderFactory DbProviderFactory { get; }
    //
    // Summary:
    //     Gets a string that represents the version of the server to which the object
    //     is connected.
    //
    // Returns:
    //     The version of the database. The format of the string returned depends on
    //     the specific type of connection you are using.
    //[Browsable(false)]
    //public abstract string ServerVersion { get; }
    //
    // Summary:
    //     Gets a string that describes the state of the connection.
    //
    // Returns:
    //     The state of the connection. The format of the string returned depends on
    //     the specific type of connection you are using.
    //[Browsable(false)]
    //[ResDescription("DbConnection_State")]
    //public abstract ConnectionState State { get; }

    // Summary:
    //     Occurs when the state of the event changes.
    //[ResDescription("DbConnection_StateChange")]
    //[ResCategory("DataCategory_StateChange")]
    //public virtual event StateChangeEventHandler StateChange;

    // Summary:
    //     Starts a database transaction.
    //
    // Parameters:
    //   isolationLevel:
    //     Specifies the isolation level for the transaction.
    //
    // Returns:
    //     An object representing the new transaction.
    protected virtual DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
    {
      Contract.Ensures(Contract.Result<DbTransaction>() != null);

      return default(DbTransaction);
    }
    //
    // Summary:
    //     Starts a database transaction.
    //
    // Returns:
    //     An object representing the new transaction.
    public DbTransaction BeginTransaction()
    {
      Contract.Ensures(Contract.Result<DbTransaction>() != null);

      return default(DbTransaction);
    }
    //
    // Summary:
    //     Starts a database transaction with the specified isolation level.
    //
    // Parameters:
    //   isolationLevel:
    //     Specifies the isolation level for the transaction.
    //
    // Returns:
    //     An object representing the new transaction.
    public DbTransaction BeginTransaction(IsolationLevel isolationLevel)
    {
      Contract.Ensures(Contract.Result<DbTransaction>() != null);

      return default(DbTransaction);
    }
    //
    // Summary:
    //     Changes the current database for an open connection.
    //
    // Parameters:
    //   databaseName:
    //     Specifies the name of the database for the connection to use.
    //public abstract void ChangeDatabase(string databaseName);
    //
    // Summary:
    //     Closes the connection to the database. This is the preferred method of closing
    //     any open connection.
    //
    // Exceptions:
    //   System.Data.Common.DbException:
    //     The connection-level error that occurred while opening the connection.
    //public abstract void Close();
    //
    // Summary:
    //     Creates and returns a System.Data.Common.DbCommand object associated with
    //     the current connection.
    //
    // Returns:
    //     A System.Data.Common.DbCommand object.
    public DbCommand CreateCommand()
    {
      Contract.Ensures(Contract.Result<DbCommand>() != null);

      return default(DbCommand);
    }
    //
    // Summary:
    //     Creates and returns a System.Data.Common.DbCommand object associated with
    //     the current connection.
    //
    // Returns:
    //     A System.Data.Common.DbCommand object.
    protected virtual DbCommand CreateDbCommand()
    {
      Contract.Ensures(Contract.Result<DbCommand>() != null);

      return default(DbCommand);
    }

    //
    // Summary:
    //     Enlists in the specified transaction.
    //
    // Parameters:
    //   transaction:
    //     A reference to an existing System.Transactions.Transaction in which to enlist.
    //public virtual void EnlistTransaction(System.Transactions.Transaction transaction);
    //
    // Summary:
    //     Returns schema information for the data source of this System.Data.Common.DbConnection.
    //
    // Returns:
    //     A System.Data.DataTable that contains schema information.
    //public virtual DataTable GetSchema();
    //
    // Summary:
    //     Returns schema information for the data source of this System.Data.Common.DbConnection
    //     using the specified string for the schema name.
    //
    // Parameters:
    //   collectionName:
    //     Specifies the name of the schema to return.
    //
    // Returns:
    //     A System.Data.DataTable that contains schema information.
    //public virtual DataTable GetSchema(string collectionName);
    //
    // Summary:
    //     Returns schema information for the data source of this System.Data.Common.DbConnection
    //     using the specified string for the schema name and the specified string array
    //     for the restriction values.
    //
    // Parameters:
    //   collectionName:
    //     Specifies the name of the schema to return.
    //
    //   restrictionValues:
    //     Specifies a set of restriction values for the requested schema.
    //
    // Returns:
    //     A System.Data.DataTable that contains schema information.
    //public virtual DataTable GetSchema(string collectionName, string//[] restrictionValues);
    //
    // Summary:
    //     Raises the System.Data.Common.DbConnection.StateChange event.
    //
    // Parameters:
    //   stateChange:
    //     A System.Data.StateChangeEventArgs that contains the event data.
    //protected virtual void OnStateChange(StateChangeEventArgs stateChange);
    //
    // Summary:
    //     Opens a database connection with the settings specified by the System.Data.Common.DbConnection.ConnectionString.
    //public abstract void Open();
  }
}

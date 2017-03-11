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
using System.Data.Common;
using System.Xml;
using System.Diagnostics.Contracts;

namespace System.Data.SqlClient
{
  // Summary:
  //     Represents a Transact-SQL statement or stored procedure to execute against
  //     a SQL Server database. This class cannot be inherited.
  //[Designer("Microsoft.VSDesigner.Data.VS.SqlCommandDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  //[DefaultEvent("RecordsAffected")]
  //[ToolboxItem(true)]
  public /*sealed*/ class SqlCommand // : DbCommand, ICloneable
  {
    // Summary:
    //     Initializes a new instance of the System.Data.SqlClient.SqlCommand class.
    //public SqlCommand();
    //
    // Summary:
    //     Initializes a new instance of the System.Data.SqlClient.SqlCommand class
    //     with the text of the query.
    //
    // Parameters:
    //   cmdText:
    //     The text of the query.
    //public SqlCommand(string cmdText);
    //
    // Summary:
    //     Initializes a new instance of the System.Data.SqlClient.SqlCommand class
    //     with the text of the query and a System.Data.SqlClient.SqlConnection.
    //
    // Parameters:
    //   cmdText:
    //     The text of the query.
    //
    //   connection:
    //     A System.Data.SqlClient.SqlConnection that represents the connection to an
    //     instance of SQL Server.
    //public SqlCommand(string cmdText, SqlConnection connection);
    //
    // Summary:
    //     Initializes a new instance of the System.Data.SqlClient.SqlCommand class
    //     with the text of the query, a System.Data.SqlClient.SqlConnection, and the
    //     System.Data.SqlClient.SqlTransaction.
    //
    // Parameters:
    //   cmdText:
    //     The text of the query.
    //
    //   connection:
    //     A System.Data.SqlClient.SqlConnection that represents the connection to an
    //     instance of SQL Server.
    //
    //   transaction:
    //     The System.Data.SqlClient.SqlTransaction in which the System.Data.SqlClient.SqlCommand
    //     executes.
    //public SqlCommand(string cmdText, SqlConnection connection, SqlTransaction transaction);

    // Summary:
    //     Gets or sets the Transact-SQL statement, table name or stored procedure to
    //     execute at the data source.
    //
    // Returns:
    //     The Transact-SQL statement or stored procedure to execute. The default is
    //     an empty string.
    //[ResDescription("DbCommand_CommandText")]
    //[ResCategory("DataCategory_Data")]
    //[RefreshProperties(RefreshProperties.All)]
    //[Editor("Microsoft.VSDesigner.Data.SQL.Design.SqlCommandTextEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    //[DefaultValue("")]
    //public override string CommandText { get; set; }
    //
    // Summary:
    //     Gets or sets the wait time before terminating the attempt to execute a command
    //     and generating an error.
    //
    // Returns:
    //     The time in seconds to wait for the command to execute. The default is 30
    //     seconds.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DbCommand_CommandTimeout")]
    //public override int CommandTimeout { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating how the System.Data.SqlClient.SqlCommand.CommandText
    //     property is to be interpreted.
    //
    // Returns:
    //     One of the System.Data.CommandType values. The default is Text.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value was not a valid System.Data.CommandType.
    //[RefreshProperties(RefreshProperties.All)]
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DbCommand_CommandType")]
    //public override CommandType CommandType { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Data.SqlClient.SqlConnection used by this instance
    //     of the System.Data.SqlClient.SqlCommand.
    //
    // Returns:
    //     The connection to a data source. The default value is null.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Data.SqlClient.SqlCommand.Connection property was changed while
    //     a transaction was in progress.
    //[Editor("Microsoft.VSDesigner.Data.Design.DbConnectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    //[ResDescription("DbCommand_Connection")]
    //[DefaultValue("")]
    //[ResCategory("DataCategory_Data")]
    //public SqlConnection Connection { get; set; }
    //protected override DbConnection DbConnection { get; set; }
    //protected override DbParameterCollection DbParameterCollection { get; }
    //protected override DbTransaction DbTransaction { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the command object should be visible
    //     in a Windows Form Designer control.
    //
    // Returns:
    //     A value indicating whether the command object should be visible in a control.
    //     The default is true.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //[DesignOnly(true)]
    //[DefaultValue(true)]
    //public override bool DesignTimeVisible { get; set; }
    //
    // Summary:
    //     Gets or sets a value that specifies the System.Data.Sql.SqlNotificationRequest
    //     object bound to this command.
    //
    // Returns:
    //     When set to null (default), no notification should be requested.
    //[Browsable(false)]
    //[ResDescription("SqlCommand_Notification")]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[ResCategory("DataCategory_Notification")]
    //public SqlNotificationRequest Notification { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the application should automatically
    //     receive query notifications from a common System.Data.SqlClient.SqlDependency
    //     object.
    //
    // Returns:
    //     true if the application should automatically receive query notifications;
    //     otherwise false. The default value is true.
    //[ResDescription("SqlCommand_NotificationAutoEnlist")]
    //[DefaultValue(true)]
    //[ResCategory("DataCategory_Notification")]
    //public bool NotificationAutoEnlist { get; set; }
    //
    // Summary:
    //     Gets the System.Data.SqlClient.SqlParameterCollection.
    //
    // Returns:
    //     The parameters of the Transact-SQL statement or stored procedure. The default
    //     is an empty collection.
    //[ResCategory("DataCategory_Data")]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    //[ResDescription("DbCommand_Parameters")]
    public SqlParameterCollection Parameters
	{
		get
		{
				Contract.Ensures(Contract.Result<SqlParameterCollection>() != null);
				return default(SqlParameterCollection);
		}
	}
    //
    // Summary:
    //     Gets or sets the System.Data.SqlClient.SqlTransaction within which the System.Data.SqlClient.SqlCommand
    //     executes.
    //
    // Returns:
    //     The System.Data.SqlClient.SqlTransaction. The default value is null.
    //[Browsable(false)]
    //[ResDescription("DbCommand_Transaction")]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public SqlTransaction Transaction { get; set; }
    //
    // Summary:
    //     Gets or sets how command results are applied to the System.Data.DataRow when
    //     used by the Update method of the System.Data.Common.DbDataAdapter.
    //
    // Returns:
    //     One of the System.Data.UpdateRowSource values.
    //[ResDescription("DbCommand_UpdatedRowSource")]
    //[ResCategory("DataCategory_Update")]
    //public override UpdateRowSource UpdatedRowSource { get; set; }

    // Summary:
    //     Occurs when the execution of a Transact-SQL statement completes.
    //[ResCategory("DataCategory_StatementCompleted")]
    //[ResDescription("DbCommand_StatementCompleted")]
    //public event StatementCompletedEventHandler StatementCompleted;

    // Summary:
    //     Initiates the asynchronous execution of the Transact-SQL statement or stored
    //     procedure that is described by this System.Data.SqlClient.SqlCommand.
    //
    // Returns:
    //     An System.IAsyncResult that can be used to poll or wait for results, or both;
    //     this value is also needed when invoking System.Data.SqlClient.SqlCommand.EndExecuteNonQuery(System.IAsyncResult),
    //     which returns the number of affected rows.
    //
    // Exceptions:
    //   System.Data.SqlClient.SqlException:
    //     Any error that occurred while executing the command text.
    //
    //   System.InvalidOperationException:
    //     The name/value pair "Asynchronous Processing=true" was not included within
    //     the connection string defining the connection for this System.Data.SqlClient.SqlCommand.
    public IAsyncResult BeginExecuteNonQuery()
    {
      Contract.Ensures(Contract.Result<IAsyncResult>() != null);

      return default(IAsyncResult);
    }
    //
    // Summary:
    //     Initiates the asynchronous execution of the Transact-SQL statement or stored
    //     procedure that is described by this System.Data.SqlClient.SqlCommand, given
    //     a callback procedure and state information.
    //
    // Parameters:
    //   callback:
    //     An System.AsyncCallback delegate that is invoked when the command's execution
    //     has completed. Pass null (Nothing in Microsoft Visual Basic) to indicate
    //     that no callback is required.
    //
    //   stateObject:
    //     A user-defined state object that is passed to the callback procedure. Retrieve
    //     this object from within the callback procedure using the System.IAsyncResult.AsyncState
    //     property.
    //
    // Returns:
    //     An System.IAsyncResult that can be used to poll or wait for results, or both;
    //     this value is also needed when invoking System.Data.SqlClient.SqlCommand.EndExecuteNonQuery(System.IAsyncResult),
    //     which returns the number of affected rows.
    //
    // Exceptions:
    //   System.Data.SqlClient.SqlException:
    //     Any error that occurred while executing the command text.
    //
    //   System.InvalidOperationException:
    //     The name/value pair "Asynchronous Processing=true" was not included within
    //     the connection string defining the connection for this System.Data.SqlClient.SqlCommand.
    public IAsyncResult BeginExecuteNonQuery(AsyncCallback callback, object stateObject)
    {
      Contract.Ensures(Contract.Result<IAsyncResult>() != null);

      return default(IAsyncResult);
    }
    //
    // Summary:
    //     Initiates the asynchronous execution of the Transact-SQL statement or stored
    //     procedure that is described by this System.Data.SqlClient.SqlCommand, and
    //     retrieves one or more result sets from the server.
    //
    // Returns:
    //     An System.IAsyncResult that can be used to poll or wait for results, or both;
    //     this value is also needed when invoking System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult),
    //     which returns a System.Data.SqlClient.SqlDataReader instance that can be
    //     used to retrieve the returned rows.
    //
    // Exceptions:
    //   System.Data.SqlClient.SqlException:
    //     Any error that occurred while executing the command text.
    //
    //   System.InvalidOperationException:
    //     The name/value pair "Asynchronous Processing=true" was not included within
    //     the connection string defining the connection for this System.Data.SqlClient.SqlCommand.
    public IAsyncResult BeginExecuteReader()
    {
      Contract.Ensures(Contract.Result<IAsyncResult>() != null);

      return default(IAsyncResult);
    }
    //
    // Summary:
    //     Initiates the asynchronous execution of the Transact-SQL statement or stored
    //     procedure that is described by this System.Data.SqlClient.SqlCommand using
    //     one of the System.Data.CommandBehavior values.
    //
    // Parameters:
    //   behavior:
    //     One of the System.Data.CommandBehavior values, indicating options for statement
    //     execution and data retrieval.
    //
    // Returns:
    //     An System.IAsyncResult that can be used to poll, wait for results, or both;
    //     this value is also needed when invoking System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult),
    //     which returns a System.Data.SqlClient.SqlDataReader instance that can be
    //     used to retrieve the returned rows.
    //
    // Exceptions:
    //   System.Data.SqlClient.SqlException:
    //     Any error that occurred while executing the command text.
    //
    //   System.InvalidOperationException:
    //     The name/value pair "Asynchronous Processing=true" was not included within
    //     the connection string defining the connection for this System.Data.SqlClient.SqlCommand.
    public IAsyncResult BeginExecuteReader(CommandBehavior behavior)
    {
      Contract.Ensures(Contract.Result<IAsyncResult>() != null);

      return default(IAsyncResult);
    }
    //
    // Summary:
    //     Initiates the asynchronous execution of the Transact-SQL statement or stored
    //     procedure that is described by this System.Data.SqlClient.SqlCommand and
    //     retrieves one or more result sets from the server, given a callback procedure
    //     and state information.
    //
    // Parameters:
    //   callback:
    //     An System.AsyncCallback delegate that is invoked when the command's execution
    //     has completed. Pass null (Nothing in Microsoft Visual Basic) to indicate
    //     that no callback is required.
    //
    //   stateObject:
    //     A user-defined state object that is passed to the callback procedure. Retrieve
    //     this object from within the callback procedure using the System.IAsyncResult.AsyncState
    //     property.
    //
    // Returns:
    //     An System.IAsyncResult that can be used to poll, wait for results, or both;
    //     this value is also needed when invoking System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult),
    //     which returns a System.Data.SqlClient.SqlDataReader instance which can be
    //     used to retrieve the returned rows.
    //
    // Exceptions:
    //   System.Data.SqlClient.SqlException:
    //     Any error that occurred while executing the command text.
    //
    //   System.InvalidOperationException:
    //     The name/value pair "Asynchronous Processing=true" was not included within
    //     the connection string defining the connection for this System.Data.SqlClient.SqlCommand.
    public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject)
    {
      Contract.Ensures(Contract.Result<IAsyncResult>() != null);

      return default(IAsyncResult);
    }
    //
    // Summary:
    //     Initiates the asynchronous execution of the Transact-SQL statement or stored
    //     procedure that is described by this System.Data.SqlClient.SqlCommand, using
    //     one of the CommandBehavior values, and retrieving one or more result sets
    //     from the server, given a callback procedure and state information.
    //
    // Parameters:
    //   callback:
    //     An System.AsyncCallback delegate that is invoked when the command's execution
    //     has completed. Pass null (Nothing in Microsoft Visual Basic) to indicate
    //     that no callback is required.
    //
    //   stateObject:
    //     A user-defined state object that is passed to the callback procedure. Retrieve
    //     this object from within the callback procedure using the System.IAsyncResult.AsyncState
    //     property.
    //
    //   behavior:
    //     One of the System.Data.CommandBehavior values, indicating options for statement
    //     execution and data retrieval.
    //
    // Returns:
    //     An System.IAsyncResult that can be used to poll or wait for results, or both;
    //     this value is also needed when invoking System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult),
    //     which returns a System.Data.SqlClient.SqlDataReader instance which can be
    //     used to retrieve the returned rows.
    //
    // Exceptions:
    //   System.Data.SqlClient.SqlException:
    //     Any error that occurred while executing the command text.
    //
    //   System.InvalidOperationException:
    //     The name/value pair "Asynchronous Processing=true" was not included within
    //     the connection string defining the connection for this System.Data.SqlClient.SqlCommand.
    public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject, CommandBehavior behavior)
    {
      Contract.Ensures(Contract.Result<IAsyncResult>() != null);

      return default(IAsyncResult);
    }
    //
    // Summary:
    //     Initiates the asynchronous execution of the Transact-SQL statement or stored
    //     procedure that is described by this System.Data.SqlClient.SqlCommand and
    //     returns results as an System.Xml.XmlReader object.
    //
    // Returns:
    //     An System.IAsyncResult that can be used to poll or wait for results, or both;
    //     this value is also needed when invoking EndExecuteXmlReader, which returns
    //     a single XML value.
    //
    // Exceptions:
    //   System.Data.SqlClient.SqlException:
    //     Any error that occurred while executing the command text.
    //
    //   System.InvalidOperationException:
    //     The name/value pair "Asynchronous Processing=true" was not included within
    //     the connection string defining the connection for this System.Data.SqlClient.SqlCommand.
    public IAsyncResult BeginExecuteXmlReader()
    {
      Contract.Ensures(Contract.Result<IAsyncResult>() != null);

      return default(IAsyncResult);
    }
    //
    // Summary:
    //     Initiates the asynchronous execution of the Transact-SQL statement or stored
    //     procedure that is described by this System.Data.SqlClient.SqlCommand and
    //     returns results as an System.Xml.XmlReader object, using a callback procedure.
    //
    // Parameters:
    //   callback:
    //     An System.AsyncCallback delegate that is invoked when the command's execution
    //     has completed. Pass null (Nothing in Microsoft Visual Basic) to indicate
    //     that no callback is required.
    //
    //   stateObject:
    //     A user-defined state object that is passed to the callback procedure. Retrieve
    //     this object from within the callback procedure using the System.IAsyncResult.AsyncState
    //     property.
    //
    // Returns:
    //     An System.IAsyncResult that can be used to poll, wait for results, or both;
    //     this value is also needed when the System.Data.SqlClient.SqlCommand.EndExecuteXmlReader(System.IAsyncResult)
    //     is called, which returns the results of the command as XML.
    //
    // Exceptions:
    //   System.Data.SqlClient.SqlException:
    //     Any error that occurred while executing the command text.
    //
    //   System.InvalidOperationException:
    //     The name/value pair "Asynchronous Processing=true" was not included within
    //     the connection string defining the connection for this System.Data.SqlClient.SqlCommand.
    public IAsyncResult BeginExecuteXmlReader(AsyncCallback callback, object stateObject)
    {
      Contract.Ensures(Contract.Result<IAsyncResult>() != null);

      return default(IAsyncResult);
    }
    //
    // Summary:
    //     Tries to cancel the execution of a System.Data.SqlClient.SqlCommand.
    //public override void Cancel();
    //
    // Summary:
    //     Creates a new System.Data.SqlClient.SqlCommand object that is a copy of the
    //     current instance.
    //
    // Returns:
    //     A new System.Data.SqlClient.SqlCommand object that is a copy of this instance.
    public SqlCommand Clone()
    {
      Contract.Ensures(Contract.Result<SqlCommand>() != null);

      return default(SqlCommand);
    }
    //protected override DbParameter CreateDbParameter();
    //
    // Summary:
    //     Creates a new instance of a System.Data.SqlClient.SqlParameter object.
    //
    // Returns:
    //     A System.Data.SqlClient.SqlParameter object.
    public SqlParameter CreateParameter()
    {
      Contract.Ensures(Contract.Result<SqlParameter>() != null);

      return default(SqlParameter);
    }

    //protected override void Dispose(bool disposing);
    //
    // Summary:
    //     Finishes asynchronous execution of a Transact-SQL statement.
    //
    // Parameters:
    //   asyncResult:
    //     The System.IAsyncResult returned by the call to System.Data.SqlClient.SqlCommand.BeginExecuteNonQuery().
    //
    // Returns:
    //     The number of rows affected (the same behavior as System.Data.SqlClient.SqlCommand.ExecuteNonQuery()).
    //
    // Exceptions:
    //   System.ArgumentException:
    //     asyncResult parameter is null (Nothing in Microsoft Visual Basic)
    //
    //   System.InvalidOperationException:
    //     System.Data.SqlClient.SqlCommand.EndExecuteNonQuery(System.IAsyncResult)
    //     was called more than once for a single command execution, or the method was
    //     mismatched against its execution method (for example, the code called System.Data.SqlClient.SqlCommand.EndExecuteNonQuery(System.IAsyncResult)
    //     to complete execution of a call to System.Data.SqlClient.SqlCommand.BeginExecuteXmlReader().
    public int EndExecuteNonQuery(IAsyncResult asyncResult)
    {
      Contract.Requires(asyncResult != null);

      return default(int);
    }
    //
    // Summary:
    //     Finishes asynchronous execution of a Transact-SQL statement, returning the
    //     requested System.Data.SqlClient.SqlDataReader.
    //
    // Parameters:
    //   asyncResult:
    //     The System.IAsyncResult returned by the call to System.Data.SqlClient.SqlCommand.BeginExecuteReader().
    //
    // Returns:
    //     A System.Data.SqlClient.SqlDataReader object that can be used to retrieve
    //     the requested rows.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     asyncResult parameter is null (Nothing in Microsoft Visual Basic)
    //
    //   System.InvalidOperationException:
    //     System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult) was
    //     called more than once for a single command execution, or the method was mismatched
    //     against its execution method (for example, the code called System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult)
    //     to complete execution of a call to System.Data.SqlClient.SqlCommand.BeginExecuteXmlReader().
    public SqlDataReader EndExecuteReader(IAsyncResult asyncResult)
    {
      Contract.Requires(asyncResult != null);

      return default(SqlDataReader);
    }
    //
    // Summary:
    //     Finishes asynchronous execution of a Transact-SQL statement, returning the
    //     requested data as XML.
    //
    // Parameters:
    //   asyncResult:
    //     The System.IAsyncResult returned by the call to System.Data.SqlClient.SqlCommand.BeginExecuteXmlReader().
    //
    // Returns:
    //     An System.Xml.XmlReader object that can be used to fetch the resulting XML
    //     data.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     asyncResult parameter is null (Nothing in Microsoft Visual Basic)
    //
    //   System.InvalidOperationException:
    //     System.Data.SqlClient.SqlCommand.EndExecuteXmlReader(System.IAsyncResult)
    //     was called more than once for a single command execution, or the method was
    //     mismatched against its execution method (for example, the code called System.Data.SqlClient.SqlCommand.EndExecuteXmlReader(System.IAsyncResult)
    //     to complete execution of a call to System.Data.SqlClient.SqlCommand.BeginExecuteNonQuery().
    public XmlReader EndExecuteXmlReader(IAsyncResult asyncResult)
    {
      Contract.Requires(asyncResult != null);
      Contract.Ensures(Contract.Result<XmlReader>() != null);

      return default(XmlReader);
    }


    //protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior);
    //
    // Summary:
    //     Executes a Transact-SQL statement against the connection and returns the
    //     number of rows affected.
    //
    // Returns:
    //     The number of rows affected.
    //
    // Exceptions:
    //   System.Data.SqlClient.SqlException:
    //     An exception occurred while executing the command against a locked row. This
    //     exception is not generated when you are using Microsoft .NET Framework version
    //     1.0.
    //public override int ExecuteNonQuery();
    //
    // Summary:
    //     Sends the System.Data.SqlClient.SqlCommand.CommandText to the System.Data.SqlClient.SqlCommand.Connection
    //     and builds a System.Data.SqlClient.SqlDataReader.
    //
    // Returns:
    //     A System.Data.SqlClient.SqlDataReader object.
    //
    // Exceptions:
    //   System.Data.SqlClient.SqlException:
    //     An exception occurred while executing the command against a locked row. This
    //     exception is not generated when you are using Microsoft .NET Framework version
    //     1.0.
    //
    //   System.InvalidOperationException:
    //     The current state of the connection is closed. System.Data.SqlClient.SqlCommand.ExecuteReader()
    //     requires an open System.Data.SqlClient.SqlConnection.
    public SqlDataReader ExecuteReader()
    {
      Contract.Ensures(Contract.Result<SqlDataReader >() != null);

      return default(SqlDataReader);
    }
    //
    // Summary:
    //     Sends the System.Data.SqlClient.SqlCommand.CommandText to the System.Data.SqlClient.SqlCommand.Connection,
    //     and builds a System.Data.SqlClient.SqlDataReader using one of the System.Data.CommandBehavior
    //     values.
    //
    // Parameters:
    //   behavior:
    //     One of the System.Data.CommandBehavior values.
    //
    // Returns:
    //     A System.Data.SqlClient.SqlDataReader object.
    public SqlDataReader ExecuteReader(CommandBehavior behavior)
    {
      Contract.Ensures(Contract.Result<SqlDataReader>() != null);

      return default(SqlDataReader);
    }
    //
    // Summary:
    //     Executes the query, and returns the first column of the first row in the
    //     result set returned by the query. Additional columns or rows are ignored.
    //
    // Returns:
    //     The first column of the first row in the result set, or a null reference
    //     (Nothing in Visual Basic) if the result set is empty.
    //
    // Exceptions:
    //   System.Data.SqlClient.SqlException:
    //     An exception occurred while executing the command against a locked row. This
    //     exception is not generated when you are using Microsoft .NET Framework version
    //     1.0.
    //public override object ExecuteScalar();
    //
    // Summary:
    //     Sends the System.Data.SqlClient.SqlCommand.CommandText to the System.Data.SqlClient.SqlCommand.Connection
    //     and builds an System.Xml.XmlReader object.
    //
    // Returns:
    //     An System.Xml.XmlReader object.
    //
    // Exceptions:
    //   System.Data.SqlClient.SqlException:
    //     An exception occurred while executing the command against a locked row. This
    //     exception is not generated when you are using Microsoft .NET Framework version
    //     1.0.
    public XmlReader ExecuteXmlReader()
    {
      Contract.Ensures(Contract.Result<XmlReader>() != null);

      return default(XmlReader);
    }
    //
    // Summary:
    //     Creates a prepared version of the command on an instance of SQL Server.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Data.SqlClient.SqlCommand.Connection is not set.-or- The System.Data.SqlClient.SqlCommand.Connection
    //     is not System.Data.SqlClient.SqlConnection.Open().
    //public override void Prepare();
    //
    // Summary:
    //     Resets the System.Data.SqlClient.SqlCommand.CommandTimeout property to its
    //     default value.
    //public void ResetCommandTimeout();
  }
}

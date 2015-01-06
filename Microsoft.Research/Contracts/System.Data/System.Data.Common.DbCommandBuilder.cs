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
  //     Automatically generates single-table commands used to reconcile changes made
  //     to a System.Data.DataSet with the associated database. This is an abstract
  //     class that can only be inherited.
  public abstract class DbCommandBuilder // : Component
  {
    // Summary:
    //     Sets or gets the System.Data.Common.CatalogLocation for an instance of the
    //     System.Data.Common.DbCommandBuilder class.
    //
    // Returns:
    //     A System.Data.Common.CatalogLocation object.
    //[ResCategory("DataCategory_Schema")]
    //[ResDescription("DbCommandBuilder_CatalogLocation")]
    //public virtual CatalogLocation CatalogLocation { get; set; }
    //
    // Summary:
    //     Sets or gets a string used as the catalog separator for an instance of the
    //     System.Data.Common.DbCommandBuilder class.
    //
    // Returns:
    //     A string indicating the catalog separator for use with an instance of the
    //     System.Data.Common.DbCommandBuilder class.
    //[ResDescription("DbCommandBuilder_CatalogSeparator")]
    //[ResCategory("DataCategory_Schema")]
    //[DefaultValue(".")]
    //public virtual string CatalogSeparator { get; set; }
    //
    // Summary:
    //     Specifies which System.Data.ConflictOption is to be used by the System.Data.Common.DbCommandBuilder.
    //
    // Returns:
    //     Returns one of the System.Data.ConflictOption values describing the behavior
    //     of this System.Data.Common.DbCommandBuilder.
    //[ResDescription("DbCommandBuilder_ConflictOption")]
    //[ResCategory("DataCategory_Update")]
    //public virtual ConflictOption ConflictOption { get; set; }
    ////
    // Summary:
    //     Gets or sets a System.Data.Common.DbDataAdapter object for which Transact-SQL
    //     statements are automatically generated.
    //
    // Returns:
    //     A System.Data.Common.DbDataAdapter object.
    //[Browsable(false)]
    //[ResDescription("DbCommandBuilder_DataAdapter")]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public DbDataAdapter DataAdapter { get; set; }
    //
    // Summary:
    //     Gets or sets the beginning character or characters to use when specifying
    //     database objects (for example, tables or columns) whose names contain characters
    //     such as spaces or reserved tokens.
    //
    // Returns:
    //     The beginning character or characters to use. The default is an empty string.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     This property cannot be changed after an insert, update, or delete command
    //     has been generated.
    //[ResCategory("DataCategory_Schema")]
    //[DefaultValue("")]
    //[ResDescription("DbCommandBuilder_QuotePrefix")]
    //public virtual string QuotePrefix { get; set; }
    //
    // Summary:
    //     Gets or sets the beginning character or characters to use when specifying
    //     database objects (for example, tables or columns) whose names contain characters
    //     such as spaces or reserved tokens.
    //
    // Returns:
    //     The ending character or characters to use. The default is an empty string.
    //[DefaultValue("")]
    //[ResCategory("DataCategory_Schema")]
    //[ResDescription("DbCommandBuilder_QuoteSuffix")]
    //public virtual string QuoteSuffix { get; set; }
    //
    // Summary:
    //     Gets or sets the character to be used for the separator between the schema
    //     identifier and any other identifiers.
    //
    // Returns:
    //     The character to be used as the schema separator.
    //[ResCategory("DataCategory_Schema")]
    //[DefaultValue(".")]
    //[ResDescription("DbCommandBuilder_SchemaSeparator")]
    //public virtual string SchemaSeparator { get; set; }
    //
    // Summary:
    //     Specifies whether all column values in an update statement are included or
    //     only changed ones.
    //
    // Returns:
    //     true if the UPDATE statement generated by the System.Data.Common.DbCommandBuilder
    //     includes all columns; false if it includes only changed columns.
    //[ResCategory("DataCategory_Schema")]
    //[ResDescription("DbCommandBuilder_SetAllValues")]
    //[DefaultValue(false)]
    //public bool SetAllValues { get; set; }

    // Summary:
    //     Allows the provider implementation of the System.Data.Common.DbCommandBuilder
    //     class to handle additional parameter properties.
    //
    // Parameters:
    //   parameter:
    //     A System.Data.Common.DbParameter to which the additional modifications are
    //     applied.
    //
    //   row:
    //     The System.Data.DataRow from the schema table provided by System.Data.Common.DbDataReader.GetSchemaTable().
    //
    //   statementType:
    //     The type of command being generated; INSERT, UPDATE or DELETE.
    //
    //   whereClause:
    //     true if the parameter is part of the update or delete WHERE clause, false
    //     if it is part of the insert or update values.
    //protected abstract void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause);
    
    //
    // Summary:
    //     Gets the automatically generated System.Data.Common.DbCommand object required
    //     to perform deletions at the data source.
    //
    // Returns:
    //     The automatically generated System.Data.Common.DbCommand object required
    //     to perform deletions.
    public DbCommand GetDeleteCommand()
    {
      Contract.Ensures(Contract.Result<DbCommand>() != null);

      return default(DbCommand);
    }
    //
    // Summary:
    //     Gets the automatically generated System.Data.Common.DbCommand object required
    //     to perform deletions at the data source, optionally using columns for parameter
    //     names.
    //
    // Parameters:
    //   useColumnsForParameterNames:
    //     If true, generate parameter names matching column names, if possible. If
    //     false, generate @p1, @p2, and so on.
    //
    // Returns:
    //     The automatically generated System.Data.Common.DbCommand object required
    //     to perform deletions.
    public DbCommand GetDeleteCommand(bool useColumnsForParameterNames)
    {
      Contract.Ensures(Contract.Result<DbCommand>() != null);

      return default(DbCommand);
    }
    //
    // Summary:
    //     Gets the automatically generated System.Data.Common.DbCommand object required
    //     to perform insertions at the data source.
    //
    // Returns:
    //     The automatically generated System.Data.Common.DbCommand object required
    //     to perform insertions.
    public DbCommand GetInsertCommand()
    {
      Contract.Ensures(Contract.Result<DbCommand>() != null);

      return default(DbCommand);
    }
    //
    // Summary:
    //     Gets the automatically generated System.Data.Common.DbCommand object required
    //     to perform insertions at the data source, optionally using columns for parameter
    //     names.
    //
    // Parameters:
    //   useColumnsForParameterNames:
    //     If true, generate parameter names matching column names, if possible. If
    //     false, generate @p1, @p2, and so on.
    //
    // Returns:
    //     The automatically generated System.Data.Common.DbCommand object required
    //     to perform insertions.
    public DbCommand GetInsertCommand(bool useColumnsForParameterNames)
    {
      Contract.Ensures(Contract.Result<DbCommand>() != null);

      return default(DbCommand);
    }
    //
    // Summary:
    //     Returns the name of the specified parameter in the format of @p#. Use when
    //     building a custom command builder.
    //
    // Parameters:
    //   parameterOrdinal:
    //     The number to be included as part of the parameter's name..
    //
    // Returns:
    //     The name of the parameter with the specified number appended as part of the
    //     parameter name.
    //protected abstract string GetParameterName(int parameterOrdinal);
    //
    // Summary:
    //     Returns the full parameter name, given the partial parameter name.
    //
    // Parameters:
    //   parameterName:
    //     The partial name of the parameter.
    //
    // Returns:
    //     The full parameter name corresponding to the partial parameter name requested.
    //protected abstract string GetParameterName(string parameterName);
    //
    // Summary:
    //     Returns the placeholder for the parameter in the associated SQL statement.
    //
    // Parameters:
    //   parameterOrdinal:
    //     The number to be included as part of the parameter's name.
    //
    // Returns:
    //     The name of the parameter with the specified number appended.
    //protected abstract string GetParameterPlaceholder(int parameterOrdinal);
    //
    // Summary:
    //     Returns the schema table for the System.Data.Common.DbCommandBuilder.
    //
    // Parameters:
    //   sourceCommand:
    //     The System.Data.Common.DbCommand for which to retrieve the corresponding
    //     schema table.
    //
    // Returns:
    //     A System.Data.DataTable that represents the schema for the specific System.Data.Common.DbCommand.
    //protected virtual DataTable GetSchemaTable(DbCommand sourceCommand);
    //
    // Summary:
    //     Gets the automatically generated System.Data.Common.DbCommand object required
    //     to perform updates at the data source.
    //
    // Returns:
    //     The automatically generated System.Data.Common.DbCommand object required
    //     to perform updates.
    public DbCommand GetUpdateCommand()
    {
      Contract.Ensures(Contract.Result<DbCommand>() != null);

      return default(DbCommand);
    }
    //
    // Summary:
    //     Gets the automatically generated System.Data.Common.DbCommand object required
    //     to perform updates at the data source, optionally using columns for parameter
    //     names.
    //
    // Parameters:
    //   useColumnsForParameterNames:
    //     If true, generate parameter names matching column names, if possible. If
    //     false, generate @p1, @p2, and so on.
    //
    // Returns:
    //     The automatically generated System.Data.Common.DbCommand object required
    //     to perform updates.
    public DbCommand GetUpdateCommand(bool useColumnsForParameterNames)
    {
      Contract.Ensures(Contract.Result<DbCommand>() != null);

      return default(DbCommand);
    }
    //
    // Summary:
    //     Resets the System.Data.Common.DbCommand.CommandTimeout, System.Data.Common.DbCommand.Transaction,
    //     System.Data.Common.DbCommand.CommandType, and System.Data.UpdateRowSource
    //     properties on the System.Data.Common.DbCommand.
    //
    // Parameters:
    //   command:
    //     The System.Data.Common.DbCommand to be used by the command builder for the
    //     corresponding insert, update, or delete command.
    //
    // Returns:
    //     A System.Data.Common.DbCommand instance to use for each insert, update, or
    //     delete operation. Passing a null value allows the System.Data.Common.DbCommandBuilder.InitializeCommand(System.Data.Common.DbCommand)
    //     method to create a System.Data.Common.DbCommand object based on the Select
    //     command associated with the System.Data.Common.DbCommandBuilder.
    protected virtual DbCommand InitializeCommand(DbCommand command)
    {
      // command can be null
      Contract.Ensures(Contract.Result<DbCommand>() != null);

      return default(DbCommand);
    }
    //
    // Summary:
    //     Given an unquoted identifier in the correct catalog case, returns the correct
    //     quoted form of that identifier, including properly escaping any embedded
    //     quotes in the identifier.
    //
    // Parameters:
    //   unquotedIdentifier:
    //     The original unquoted identifier.
    //
    // Returns:
    //     The quoted version of the identifier. Embedded quotes within the identifier
    //     are properly escaped.
    //public virtual string QuoteIdentifier(string unquotedIdentifier);
    //
    // Summary:
    //     Clears the commands associated with this System.Data.Common.DbCommandBuilder.
    //public virtual void RefreshSchema();
    //
    // Summary:
    //     Adds an event handler for the System.Data.OleDb.OleDbDataAdapter.RowUpdating
    //     event.
    //
    // Parameters:
    //   rowUpdatingEvent:
    //     A System.Data.Common.RowUpdatingEventArgs instance containing information
    //     about the event.
    //protected void RowUpdatingHandler(RowUpdatingEventArgs rowUpdatingEvent);
    //
    // Summary:
    //     Registers the System.Data.Common.DbCommandBuilder to handle the System.Data.OleDb.OleDbDataAdapter.RowUpdating
    //     event for a System.Data.Common.DbDataAdapter.
    //
    // Parameters:
    //   adapter:
    //     The System.Data.Common.DbDataAdapter to be used for the update.
    //protected abstract void SetRowUpdatingHandler(DbDataAdapter adapter);
    //
    // Summary:
    //     Given a quoted identifier, returns the correct unquoted form of that identifier,
    //     including properly un-escaping any embedded quotes in the identifier.
    //
    // Parameters:
    //   quotedIdentifier:
    //     The identifier that will have its embedded quotes removed.
    //
    // Returns:
    //     The unquoted identifier, with embedded quotes properly un-escaped.
    //public virtual string UnquoteIdentifier(string quotedIdentifier);
  }
}

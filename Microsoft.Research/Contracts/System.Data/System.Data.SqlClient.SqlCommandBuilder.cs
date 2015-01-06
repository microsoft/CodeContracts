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
using System.Diagnostics.Contracts;

namespace System.Data.SqlClient
{
  // Summary:
  //     Automatically generates single-table commands that are used to reconcile
  //     changes made to a System.Data.DataSet with the associated SQL Server database.
  //     This class cannot be inherited.
  public sealed class SqlCommandBuilder // : DbCommandBuilder
  {
    // Summary:
    //     Initializes a new instance of the System.Data.SqlClient.SqlCommandBuilder
    //     class.
    //public SqlCommandBuilder();
    //
    // Summary:
    //     Initializes a new instance of the System.Data.SqlClient.SqlCommandBuilder
    //     class with the associated System.Data.SqlClient.SqlDataAdapter object.
    //
    // Parameters:
    //   adapter:
    //     The name of the System.Data.SqlClient.SqlDataAdapter.
    //public SqlCommandBuilder(SqlDataAdapter adapter);

    // Summary:
    //     Sets or gets the System.Data.Common.CatalogLocation for an instance of the
    //     System.Data.SqlClient.SqlCommandBuilder class.
    //
    // Returns:
    //     A System.Data.Common.CatalogLocation object.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public override CatalogLocation CatalogLocation { get; set; }
    //
    // Summary:
    //     Sets or gets a string used as the catalog separator for an instance of the
    //     System.Data.SqlClient.SqlCommandBuilder class.
    //
    // Returns:
    //     A string that indicates the catalog separator for use with an instance of
    //     the System.Data.SqlClient.SqlCommandBuilder class.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public override string CatalogSeparator { get; set; }
    //
    // Summary:
    //     Gets or sets a System.Data.SqlClient.SqlDataAdapter object for which Transact-SQL
    //     statements are automatically generated.
    //
    // Returns:
    //     A System.Data.SqlClient.SqlDataAdapter object.
    //[DefaultValue("")]
    //[ResCategory("DataCategory_Update")]
    //[ResDescription("SqlCommandBuilder_DataAdapter")]
    // F: it can be null, as I can pass null to the constructor
    //public SqlDataAdapter DataAdapter { get; set; }

    //
    // Summary:
    //     Gets or sets the starting character or characters to use when specifying
    //     SQL Server database objects, such as tables or columns, whose names contain
    //     characters such as spaces or reserved tokens.
    //
    // Returns:
    //     The starting character or characters to use. The default is an empty string.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     This property cannot be changed after an INSERT, UPDATE, or DELETE command
    //     has been generated.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public override string QuotePrefix { get; set; }
    //
    // Summary:
    //     Gets or sets the ending character or characters to use when specifying SQL
    //     Server database objects, such as tables or columns, whose names contain characters
    //     such as spaces or reserved tokens.
    //
    // Returns:
    //     The ending character or characters to use. The default is an empty string.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     This property cannot be changed after an insert, update, or delete command
    //     has been generated.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public override string QuoteSuffix { get; set; }
    //
    // Summary:
    //     Gets or sets the character to be used for the separator between the schema
    //     identifier and any other identifiers.
    //
    // Returns:
    //     The character to be used as the schema separator.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public override string SchemaSeparator { get; set; }

    //protected override void ApplyParameterInfo(DbParameter parameter, DataRow datarow, StatementType statementType, bool whereClause);
    //
    // Summary:
    //     Retrieves parameter information from the stored procedure specified in the
    //     System.Data.SqlClient.SqlCommand and populates the System.Data.SqlClient.SqlCommand.Parameters
    //     collection of the specified System.Data.SqlClient.SqlCommand object.
    //
    // Parameters:
    //   command:
    //     The System.Data.SqlClient.SqlCommand referencing the stored procedure from
    //     which the parameter information is to be derived. The derived parameters
    //     are added to the System.Data.SqlClient.SqlCommand.Parameters collection of
    //     the System.Data.SqlClient.SqlCommand.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The command text is not a valid stored procedure name.
    public static void DeriveParameters(SqlCommand command)
    {
      Contract.Requires(command != null);

    }
    //
    // Summary:
    //     Gets the automatically generated System.Data.SqlClient.SqlCommand object
    //     required to perform deletions on the database.
    //
    // Returns:
    //     The automatically generated System.Data.SqlClient.SqlCommand object required
    //     to perform deletions.
    public SqlCommand GetDeleteCommand()
    {
      Contract.Ensures(Contract.Result<SqlCommand>() != null);

      return default(SqlCommand);

    }
    //
    // Summary:
    //     Gets the automatically generated System.Data.SqlClient.SqlCommand object
    //     that is required to perform deletions on the database.
    //
    // Parameters:
    //   useColumnsForParameterNames:
    //     If true, generate parameter names matching column names if possible. If false,
    //     generate @p1, @p2, and so on.
    //
    // Returns:
    //     The automatically generated System.Data.SqlClient.SqlCommand object that
    //     is required to perform deletions.
    public SqlCommand GetDeleteCommand(bool useColumnsForParameterNames)
    {
      Contract.Ensures(Contract.Result<SqlCommand>() != null);

      return default(SqlCommand);
    }
    //
    // Summary:
    //     Gets the automatically generated System.Data.SqlClient.SqlCommand object
    //     required to perform insertions on the database.
    //
    // Returns:
    //     The automatically generated System.Data.SqlClient.SqlCommand object required
    //     to perform insertions.
    public SqlCommand GetInsertCommand()
    {
      Contract.Ensures(Contract.Result<SqlCommand>() != null);

      return default(SqlCommand);
    }
    //
    // Summary:
    //     Gets the automatically generated System.Data.SqlClient.SqlCommand object
    //     that is required to perform insertions on the database.
    //
    // Parameters:
    //   useColumnsForParameterNames:
    //     If true, generate parameter names matching column names if possible. If false,
    //     generate @p1, @p2, and so on.
    //
    // Returns:
    //     The automatically generated System.Data.SqlClient.SqlCommand object that
    //     is required to perform insertions.
    public SqlCommand GetInsertCommand(bool useColumnsForParameterNames)
    {
      Contract.Ensures(Contract.Result<SqlCommand>() != null);

      return default(SqlCommand);
    }
    //protected override string GetParameterName(int parameterOrdinal);
    //protected override string GetParameterName(string parameterName);
    //protected override string GetParameterPlaceholder(int parameterOrdinal);
    //protected override DataTable GetSchemaTable(DbCommand srcCommand);
    //
    // Summary:
    //     Gets the automatically generated System.Data.SqlClient.SqlCommand object
    //     required to perform updates on the database.
    //
    // Returns:
    //     The automatically generated System.Data.SqlClient.SqlCommand object that
    //     is required to perform updates.
    public SqlCommand GetUpdateCommand()
    {
      Contract.Ensures(Contract.Result<SqlCommand>() != null);

      return default(SqlCommand);
    }
    //
    // Summary:
    //     Gets the automatically generated System.Data.SqlClient.SqlCommand object
    //     required to perform updates on the database.
    //
    // Parameters:
    //   useColumnsForParameterNames:
    //     If true, generate parameter names matching column names if possible. If false,
    //     generate @p1, @p2, and so on.
    //
    // Returns:
    //     The automatically generated System.Data.SqlClient.SqlCommand object required
    //     to perform updates.
    public SqlCommand GetUpdateCommand(bool useColumnsForParameterNames)
    {
      Contract.Ensures(Contract.Result<SqlCommand>() != null);

      return default(SqlCommand);
    }

    //protected override DbCommand InitializeCommand(DbCommand command);
    //
    // Summary:
    //     Given an unquoted identifier in the correct catalog case, returns the correct
    //     quoted form of that identifier. This includes correctly escaping any embedded
    //     quotes in the identifier.
    //
    // Parameters:
    //   unquotedIdentifier:
    //     The original unquoted identifier.
    //
    // Returns:
    //     The quoted version of the identifier. Embedded quotes within the identifier
    //     are correctly escaped.
    //public override string QuoteIdentifier(string unquotedIdentifier);
    //protected override void SetRowUpdatingHandler(DbDataAdapter adapter);
    //
    // Summary:
    //     Given a quoted identifier, returns the correct unquoted form of that identifier.
    //     This includes correctly unescaping any embedded quotes in the identifier.
    //
    // Parameters:
    //   quotedIdentifier:
    //     The identifier that will have its embedded quotes removed.
    //
    // Returns:
    //     The unquoted identifier, with embedded quotes properly unescaped.
    //public override string UnquoteIdentifier(string quotedIdentifier);
  }
}

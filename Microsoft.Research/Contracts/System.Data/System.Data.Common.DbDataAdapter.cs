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
  //     Aids implementation of the System.Data.IDbDataAdapter interface. Inheritors
  //     of System.Data.Common.DbDataAdapter implement a set of functions to provide
  //     strong typing, but inherit most of the functionality needed to fully implement
  //     a DataAdapter.
  public abstract class DbDataAdapter//  : DataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
  {
    // Summary:
    //     The default name used by the System.Data.Common.DataAdapter object for table
    //     mappings.
    public const string DefaultSourceTableName = "Table";

    // Summary:
    //     Initializes a new instance of a DataAdapter class.
    //protected DbDataAdapter();
    //
    // Summary:
    //     Initializes a new instance of a DataAdapter class from an existing object
    //     of the same type.
    //
    // Parameters:
    //   adapter:
    //     A DataAdapter object used to create the new DataAdapter.
    //protected DbDataAdapter(DbDataAdapter adapter);

    // Summary:
    //     Gets or sets a command for deleting records from the data set.
    //
    // Returns:
    //     An System.Data.IDbCommand used during System.Data.IDataAdapter.Update(System.Data.DataSet)
    //     to delete records in the data source for deleted rows in the data set.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public DbCommand DeleteCommand { get; set; }
    //
    // Summary:
    //     Gets or sets the behavior of the command used to fill the data adapter.
    //
    // Returns:
    //     The System.Data.CommandBehavior of the command used to fill the data adapter.
    //protected internal CommandBehavior FillCommandBehavior { get; set; }
    //
    // Summary:
    //     Gets or sets a command used to insert new records into the data source.
    //
    // Returns:
    //     A System.Data.IDbCommand used during System.Data.IDataAdapter.Update(System.Data.DataSet)
    //     to insert records in the data source for new rows in the data set.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public DbCommand InsertCommand { get; set; }
    //
    // Summary:
    //     Gets or sets a command used to select records in the data source.
    //
    // Returns:
    //     A System.Data.IDbCommand that is used during System.Data.IDataAdapter.Update(System.Data.DataSet)
    //     to select records from data source for placement in the data set.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public DbCommand SelectCommand { get; set; }
    //
    // Summary:
    //     Gets or sets a value that enables or disables batch processing support, and
    //     specifies the number of commands that can be executed in a batch.
    //
    // Returns:
    //     The number of rows to process per batch. Value isEffect0There is no limit
    //     on the batch size.1Disables batch updating.> 1Changes are sent using batches
    //     of System.Data.Common.DbDataAdapter.UpdateBatchSize operations at a time.When
    //     setting this to a value other than 1 ,all the commands associated with the
    //     System.Data.Common.DbDataAdapter must have their System.Data.IDbCommand.UpdatedRowSource
    //     property set to None or OutputParameters. An exception will be thrown otherwise.
    //[ResDescription("DbDataAdapter_UpdateBatchSize")]
    //[DefaultValue(1)]
    //[ResCategory("DataCategory_Update")]
    //public virtual int UpdateBatchSize { get; set; }
    //
    // Summary:
    //     Gets or sets a command used to update records in the data source.
    //
    // Returns:
    //     A System.Data.IDbCommand used during System.Data.IDataAdapter.Update(System.Data.DataSet)
    //     to update records in the data source for modified rows in the data set.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public DbCommand UpdateCommand { get; set; }

    // Summary:
    //     Adds a System.Data.IDbCommand to the current batch.
    //
    // Parameters:
    //   command:
    //     The System.Data.IDbCommand to add to the batch.
    //
    // Returns:
    //     The number of commands in the batch before adding the System.Data.IDbCommand.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The adapter does not support batches.
    //protected virtual int AddToBatch(IDbCommand command);
    //
    // Summary:
    //     Removes all System.Data.IDbCommand objects from the batch.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The adapter does not support batches.
    //protected virtual void ClearBatch();
    //
    // Summary:
    //     Initializes a new instance of the System.Data.Common.RowUpdatedEventArgs
    //     class.
    //
    // Parameters:
    //   dataRow:
    //     The System.Data.DataRow used to update the data source.
    //
    //   command:
    //     The System.Data.IDbCommand executed during the System.Data.IDataAdapter.Update(System.Data.DataSet).
    //
    //   statementType:
    //     Whether the command is an UPDATE, INSERT, DELETE, or SELECT statement.
    //
    //   tableMapping:
    //     A System.Data.Common.DataTableMapping object.
    //
    // Returns:
    //     A new instance of the System.Data.Common.RowUpdatedEventArgs class.
    //protected virtual RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping);
    //
    // Summary:
    //     Initializes a new instance of the System.Data.Common.RowUpdatingEventArgs
    //     class.
    //
    // Parameters:
    //   dataRow:
    //     The System.Data.DataRow that updates the data source.
    //
    //   command:
    //     The System.Data.IDbCommand to execute during the System.Data.IDataAdapter.Update(System.Data.DataSet).
    //
    //   statementType:
    //     Whether the command is an UPDATE, INSERT, DELETE, or SELECT statement.
    //
    //   tableMapping:
    //     A System.Data.Common.DataTableMapping object.
    //
    // Returns:
    //     A new instance of the System.Data.Common.RowUpdatingEventArgs class.
    //protected virtual RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping);
    //
    // Summary:
    //     Releases the unmanaged resources used by the System.Data.Common.DbDataAdapter
    //     and optionally releases the managed resources.
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    //protected override void Dispose(bool disposing);
    //
    // Summary:
    //     Executes the current batch.
    //
    // Returns:
    //     The return value from the last command in the batch.
    //protected virtual int ExecuteBatch();
    //
    // Summary:
    //     Adds or refreshes rows in the System.Data.DataSet.
    //
    // Parameters:
    //   dataSet:
    //     A System.Data.DataSet to fill with records and, if necessary, schema.
    //
    // Returns:
    //     The number of rows successfully added to or refreshed in the System.Data.DataSet.
    //     This does not include rows affected by statements that do not return rows.
    //public override int Fill(DataSet dataSet);
    //
    // Summary:
    //     Adds or refreshes rows in a specified range in the System.Data.DataSet to
    //     match those in the data source using the System.Data.DataTable name.
    //
    // Parameters:
    //   dataTable:
    //     The name of the System.Data.DataTable to use for table mapping.
    //
    // Returns:
    //     The number of rows successfully added to or refreshed in the System.Data.DataSet.
    //     This does not include rows affected by statements that do not return rows.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The source table is invalid.
    public int Fill(DataTable dataTable)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    //
    // Summary:
    //     Adds or refreshes rows in the System.Data.DataSet to match those in the data
    //     source using the System.Data.DataSet and System.Data.DataTable names.
    //
    // Parameters:
    //   dataSet:
    //     A System.Data.DataSet to fill with records and, if necessary, schema.
    //
    //   srcTable:
    //     The name of the source table to use for table mapping.
    //
    // Returns:
    //     The number of rows successfully added to or refreshed in the System.Data.DataSet.
    //     This does not include rows affected by statements that do not return rows.
    //
    // Exceptions:
    //   System.SystemException:
    //     The source table is invalid.
    public int Fill(DataSet dataSet, string srcTable)
          {
      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    //
    // Summary:
    //     Adds or refreshes rows in a System.Data.DataTable to match those in the data
    //     source using the specified System.Data.DataTable, System.Data.IDbCommand
    //     and System.Data.CommandBehavior.
    //
    // Parameters:
    //   dataTable:
    //     A System.Data.DataTable to fill with records and, if necessary, schema.
    //
    //   command:
    //     The SQL SELECT statement used to retrieve rows from the data source.
    //
    //   behavior:
    //     One of the System.Data.CommandBehavior values.
    //
    // Returns:
    //     The number of rows successfully added to or refreshed in the System.Data.DataTable.
    //     This does not include rows affected by statements that do not return rows.
    //protected virtual int Fill(DataTable dataTable, IDbCommand command, CommandBehavior behavior)
    //{
    //  Contract.Ensures(Contract.Result<int>() >= 0);

    //  return default(int);
    //}
    //
    // Summary:
    //     Adds or refreshes rows in a System.Data.DataTable to match those in the data
    //     source starting at the specified record and retrieving up to the specified
    //     maximum number of records.
    //
    // Parameters:
    //   startRecord:
    //     The zero-based record number to start with.
    //
    //   maxRecords:
    //     The maximum number of records to retrieve.
    //
    //   dataTables:
    //     The System.Data.DataTable objects to fill from the data source.
    //
    // Returns:
    //     The number of rows successfully added to or refreshed in the System.Data.DataTable.
    //     This value does not include rows affected by statements that do not return
    //     rows.
    public int Fill(int startRecord, int maxRecords, params DataTable[] dataTables)
    {
      Contract.Requires(dataTables != null);
      Contract.Requires(startRecord >= 0);
      Contract.Requires(maxRecords >= 0);

      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    //
    // Summary:
    //     Adds or refreshes rows in a specified range in the System.Data.DataSet to
    //     match those in the data source using the System.Data.DataSet and System.Data.DataTable
    //     names.
    //
    // Parameters:
    //   dataSet:
    //     A System.Data.DataSet to fill with records and, if necessary, schema.
    //
    //   startRecord:
    //     The zero-based record number to start with.
    //
    //   maxRecords:
    //     The maximum number of records to retrieve.
    //
    //   srcTable:
    //     The name of the source table to use for table mapping.
    //
    // Returns:
    //     The number of rows successfully added to or refreshed in the System.Data.DataSet.
    //     This does not include rows affected by statements that do not return rows.
    //
    // Exceptions:
    //   System.SystemException:
    //     The System.Data.DataSet is invalid.
    //
    //   System.InvalidOperationException:
    //     The source table is invalid.-or- The connection is invalid.
    //
    //   System.InvalidCastException:
    //     The connection could not be found.
    //
    //   System.ArgumentException:
    //     The startRecord parameter is less than 0.-or- The maxRecords parameter is
    //     less than 0.
    public int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable)
    {
      Contract.Requires(dataSet != null);
      Contract.Requires(startRecord >= 0);
      Contract.Requires(maxRecords >= 0);

      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    //
    // Summary:
    //     Adds or refreshes rows in a specified range in the System.Data.DataSet to
    //     match those in the data source using the System.Data.DataSet and System.Data.DataTable
    //     names.
    //
    // Parameters:
    //   dataTables:
    //     The System.Data.DataTable objects to fill from the data source.
    //
    //   startRecord:
    //     The zero-based record number to start with.
    //
    //   maxRecords:
    //     The maximum number of records to retrieve.
    //
    //   command:
    //     The System.Data.IDbCommand executed to fill the System.Data.DataTable objects.
    //
    //   behavior:
    //     One of the System.Data.CommandBehavior values.
    //
    // Returns:
    //     The number of rows added to or refreshed in the data tables.
    //
    // Exceptions:
    //   System.SystemException:
    //     The System.Data.DataSet is invalid.
    //
    //   System.InvalidOperationException:
    //     The source table is invalid.-or- The connection is invalid.
    //
    //   System.InvalidCastException:
    //     The connection could not be found.
    //
    //   System.ArgumentException:
    //     The startRecord parameter is less than 0.-or- The maxRecords parameter is
    //     less than 0.
    //protected virtual int Fill(DataTable[] dataTables, int startRecord, int maxRecords, IDbCommand command, CommandBehavior behavior)

    //
    // Summary:
    //     Adds or refreshes rows in a specified range in the System.Data.DataSet to
    //     match those in the data source using the System.Data.DataSet and source table
    //     names, command string, and command behavior.
    //
    // Parameters:
    //   dataSet:
    //     A System.Data.DataSet to fill with records and, if necessary, schema.
    //
    //   startRecord:
    //     The zero-based record number to start with.
    //
    //   maxRecords:
    //     The maximum number of records to retrieve.
    //
    //   srcTable:
    //     The name of the source table to use for table mapping.
    //
    //   command:
    //     The SQL SELECT statement used to retrieve rows from the data source.
    //
    //   behavior:
    //     One of the System.Data.CommandBehavior values.
    //
    // Returns:
    //     The number of rows successfully added to or refreshed in the System.Data.DataSet.
    //     This does not include rows affected by statements that do not return rows.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The source table is invalid.
    //
    //   System.ArgumentException:
    //     The startRecord parameter is less than 0.-or- The maxRecords parameter is
    //     less than 0.
    //protected virtual int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable, IDbCommand command, CommandBehavior behavior);
    //
    // Summary:
    //     Adds a System.Data.DataTable named "Table" to the specified System.Data.DataSet
    //     and configures the schema to match that in the data source based on the specified
    //     System.Data.SchemaType.
    //
    // Parameters:
    //   dataSet:
    //     A System.Data.DataSet to insert the schema in.
    //
    //   schemaType:
    //     One of the System.Data.SchemaType values that specify how to insert the schema.
    //
    // Returns:
    //     A reference to a collection of System.Data.DataTable objects that were added
    //     to the System.Data.DataSet.
    //public override DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType);
    //
    // Summary:
    //     Configures the schema of the specified System.Data.DataTable based on the
    //     specified System.Data.SchemaType.
    //
    // Parameters:
    //   dataTable:
    //     The System.Data.DataTable to be filled with the schema from the data source.
    //
    //   schemaType:
    //     One of the System.Data.SchemaType values.
    //
    // Returns:
    //     A System.Data.DataTable that contains schema information returned from the
    //     data source.
    //public DataTable FillSchema(DataTable dataTable, SchemaType schemaType)
    //{
    //  Contract.Requires(dataTable != null);

    //  Contract.Ensures(Contract.Result<DataTable>() != null);


    //  return default(DataTable);
    //}
    //
    // Summary:
    //     Adds a System.Data.DataTable to the specified System.Data.DataSet and configures
    //     the schema to match that in the data source based upon the specified System.Data.SchemaType
    //     and System.Data.DataTable.
    //
    // Parameters:
    //   dataSet:
    //     A System.Data.DataSet to insert the schema in.
    //
    //   schemaType:
    //     One of the System.Data.SchemaType values that specify how to insert the schema.
    //
    //   srcTable:
    //     The name of the source table to use for table mapping.
    //
    // Returns:
    //     A reference to a collection of System.Data.DataTable objects that were added
    //     to the System.Data.DataSet.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     A source table from which to get the schema could not be found.
    //public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable)
    

    //
    // Summary:
    //     Configures the schema of the specified System.Data.DataTable based on the
    //     specified System.Data.SchemaType, command string, and System.Data.CommandBehavior
    //     values.
    //
    // Parameters:
    //   dataTable:
    //     The System.Data.DataTable to be filled with the schema from the data source.
    //
    //   schemaType:
    //     One of the System.Data.SchemaType values.
    //
    //   command:
    //     The SQL SELECT statement used to retrieve rows from the data source.
    //
    //   behavior:
    //     One of the System.Data.CommandBehavior values.
    //
    // Returns:
    //     A of System.Data.DataTable object that contains schema information returned
    //     from the data source.
    //protected virtual DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDbCommand command, CommandBehavior behavior);
    //
    // Summary:
    //     Adds a System.Data.DataTable to the specified System.Data.DataSet and configures
    //     the schema to match that in the data source based on the specified System.Data.SchemaType.
    //
    // Parameters:
    //   dataSet:
    //     The System.Data.DataSet to be filled with the schema from the data source.
    //
    //   schemaType:
    //     One of the System.Data.SchemaType values.
    //
    //   command:
    //     The SQL SELECT statement used to retrieve rows from the data source.
    //
    //   srcTable:
    //     The name of the source table to use for table mapping.
    //
    //   behavior:
    //     One of the System.Data.CommandBehavior values.
    //
    // Returns:
    //     An array of System.Data.DataTable objects that contain schema information
    //     returned from the data source.
    //protected virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, IDbCommand command, string srcTable, CommandBehavior behavior);
    //
    // Summary:
    //     Returns a System.Data.IDataParameter from one of the commands in the current
    //     batch.
    //
    // Parameters:
    //   commandIdentifier:
    //     The index of the command to retrieve the parameter from.
    //
    //   parameterIndex:
    //     The index of the parameter within the command.
    //
    // Returns:
    //     The System.Data.IDataParameter specified.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The adapter does not support batches.
    //protected virtual IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex);
    //
    // Summary:
    //     Returns information about an individual update attempt within a larger batched
    //     update.
    //
    // Parameters:
    //   commandIdentifier:
    //     The zero-based column ordinal of the individual command within the batch.
    //
    //   recordsAffected:
    //     The number of rows affected in the data store by the specified command within
    //     the batch.
    //
    //   error:
    //     An System.Exception thrown during execution of the specified command. Returns
    //     null (Nothing in Visual Basic) if no exception is thrown.
    //protected virtual bool GetBatchedRecordsAffected(int commandIdentifier, out int recordsAffected, out Exception error);
    //
    // Summary:
    //     Gets the parameters set by the user when executing an SQL SELECT statement.
    //
    // Returns:
    //     An array of System.Data.IDataParameter objects that contains the parameters
    //     set by the user.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public override IDataParameter[] GetFillParameters();
    //
    // Summary:
    //     Initializes batching for the System.Data.Common.DbDataAdapter.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The adapter does not support batches.
    //protected virtual void InitializeBatching();
    //
    // Summary:
    //     Raises the RowUpdated event of a .NET Framework data provider.
    //
    // Parameters:
    //   value:
    //     A System.Data.Common.RowUpdatedEventArgs that contains the event data.
    //protected virtual void OnRowUpdated(RowUpdatedEventArgs value);
    //
    // Summary:
    //     Raises the RowUpdating event of a .NET Framework data provider.
    //
    // Parameters:
    //   value:
    //     An System.Data.Common.RowUpdatingEventArgs that contains the event data.
    //protected virtual void OnRowUpdating(RowUpdatingEventArgs value);
    //
    // Summary:
    //     Ends batching for the System.Data.Common.DbDataAdapter.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The adapter does not support batches.
    //protected virtual void TerminateBatching();
    //
    // Summary:
    //     Calls the respective INSERT, UPDATE, or DELETE statements for each inserted,
    //     updated, or deleted row in the specified array of System.Data.DataRow objects.
    //
    // Parameters:
    //   dataRows:
    //     An array of System.Data.DataRow objects used to update the data source.
    //
    // Returns:
    //     The number of rows successfully updated from the System.Data.DataSet.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The System.Data.DataSet is invalid.
    //
    //   System.InvalidOperationException:
    //     The source table is invalid.
    //
    //   System.SystemException:
    //     No System.Data.DataRow exists to update.-or- No System.Data.DataTable exists
    //     to update.-or- No System.Data.DataSet exists to use as a source.
    //
    //   System.Data.DBConcurrencyException:
    //     An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in
    //     zero records affected.
    public int Update(DataRow[] dataRows)
    {
      Contract.Requires(dataRows != null);

      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    //
    // Summary:
    //     Calls the respective INSERT, UPDATE, or DELETE statements for each inserted,
    //     updated, or deleted row in the specified System.Data.DataSet.
    //
    // Parameters:
    //   dataSet:
    //     The System.Data.DataSet used to update the data source.
    //
    // Returns:
    //     The number of rows successfully updated from the System.Data.DataSet.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The source table is invalid.
    //
    //   System.Data.DBConcurrencyException:
    //     An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in
    //     zero records affected.
    //public override int Update(DataSet dataSet);
    //
    // Summary:
    //     Calls the respective INSERT, UPDATE, or DELETE statements for each inserted,
    //     updated, or deleted row in the specified System.Data.DataTable.
    //
    // Parameters:
    //   dataTable:
    //     The System.Data.DataTable used to update the data source.
    //
    // Returns:
    //     The number of rows successfully updated from the System.Data.DataTable.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The System.Data.DataSet is invalid.
    //
    //   System.InvalidOperationException:
    //     The source table is invalid.
    //
    //   System.SystemException:
    //     No System.Data.DataRow exists to update.-or- No System.Data.DataTable exists
    //     to update.-or- No System.Data.DataSet exists to use as a source.
    //
    //   System.Data.DBConcurrencyException:
    //     An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in
    //     zero records affected.
    public int Update(DataTable dataTable)
    {
      Contract.Requires(dataTable != null);

      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    //
    // Summary:
    //     Calls the respective INSERT, UPDATE, or DELETE statements for each inserted,
    //     updated, or deleted row in the specified array of System.Data.DataRow objects.
    //
    // Parameters:
    //   dataRows:
    //     An array of System.Data.DataRow objects used to update the data source.
    //
    //   tableMapping:
    //     The System.Data.IDataAdapter.TableMappings collection to use.
    //
    // Returns:
    //     The number of rows successfully updated from the System.Data.DataSet.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The System.Data.DataSet is invalid.
    //
    //   System.InvalidOperationException:
    //     The source table is invalid.
    //
    //   System.SystemException:
    //     No System.Data.DataRow exists to update.-or- No System.Data.DataTable exists
    //     to update.-or- No System.Data.DataSet exists to use as a source.
    //
    //   System.Data.DBConcurrencyException:
    //     An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in
    //     zero records affected.
    //protected virtual int Update(DataRow[] dataRows, DataTableMapping tableMapping);
    //
    // Summary:
    //     Calls the respective INSERT, UPDATE, or DELETE statements for each inserted,
    //     updated, or deleted row in the System.Data.DataSet with the specified System.Data.DataTable
    //     name.
    //
    // Parameters:
    //   dataSet:
    //     The System.Data.DataSet to use to update the data source.
    //
    //   srcTable:
    //     The name of the source table to use for table mapping.
    //
    // Returns:
    //     The number of rows successfully updated from the System.Data.DataSet.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The System.Data.DataSet is invalid.
    //
    //   System.InvalidOperationException:
    //     The source table is invalid.
    //
    //   System.Data.DBConcurrencyException:
    //     An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in
    //     zero records affected.
    public int Update(DataSet dataSet, string srcTable)
    {
      Contract.Requires(dataSet != null);
      Contract.Requires(srcTable != null);

      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
  }
}

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
  //     Represents a set of SQL commands and a database connection that are used
  //     to fill the System.Data.DataSet and update the data source.
  public class DataAdapter // : Component, IDataAdapter
  {
    protected DataAdapter() { }

    // Summary:
    //     Gets or sets a value indicating whether System.Data.DataRow.AcceptChanges()
    //     is called on a System.Data.DataRow after it is added to the System.Data.DataTable
    //     during any of the Fill operations.
    //
    // Returns:
    //     true if System.Data.DataRow.AcceptChanges() is called on the System.Data.DataRow;
    //     otherwise false. The default is true.
    //[ResDescription("DataAdapter_AcceptChangesDuringFill")]
    //[ResCategory("DataCategory_Fill")]
    //[DefaultValue(true)]
    //public bool AcceptChangesDuringFill { get; set; }
    //
    // Summary:
    //     Gets or sets whether System.Data.DataRow.AcceptChanges() is called during
    //     a System.Data.Common.DataAdapter.Update(System.Data.DataSet).
    //
    // Returns:
    //     true if System.Data.DataRow.AcceptChanges() is called during an System.Data.Common.DataAdapter.Update(System.Data.DataSet);
    //     otherwise false. The default is true.
    //[ResDescription("DataAdapter_AcceptChangesDuringUpdate")]
    //[DefaultValue(true)]
    //[ResCategory("DataCategory_Update")]
    //public bool AcceptChangesDuringUpdate { get; set; }
    //
    // Summary:
    //     Gets or sets a value that specifies whether to generate an exception when
    //     an error is encountered during a row update.
    //
    // Returns:
    //     true to continue the update without generating an exception; otherwise false.
    //     The default is false.
    //[DefaultValue(false)]
    //[ResCategory("DataCategory_Update")]
    //[ResDescription("DataAdapter_ContinueUpdateOnError")]
    //public bool ContinueUpdateOnError { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Data.LoadOption that determines how the adapter fills
    //     the System.Data.DataTable from the System.Data.Common.DbDataReader.
    //
    // Returns:
    //     A System.Data.LoadOption value.
    //[ResDescription("DataAdapter_FillLoadOption")]
    //[ResCategory("DataCategory_Fill")]
    //[RefreshProperties(RefreshProperties.All)]
    //public LoadOption FillLoadOption { get; set; }
    //
    // Summary:
    //     Determines the action to take when incoming data does not have a matching
    //     table or column.
    //
    // Returns:
    //     One of the System.Data.MissingMappingAction values. The default is Passthrough.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value set is not one of the System.Data.MissingMappingAction values.
    //[ResDescription("DataAdapter_MissingMappingAction")]
    //[ResCategory("DataCategory_Mapping")]
    //public MissingMappingAction MissingMappingAction { get; set; }
    //
    // Summary:
    //     Determines the action to take when existing System.Data.DataSet schema does
    //     not match incoming data.
    //
    // Returns:
    //     One of the System.Data.MissingSchemaAction values. The default is Add.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value set is not one of the System.Data.MissingSchemaAction values.
    //[ResDescription("DataAdapter_MissingSchemaAction")]
    //[ResCategory("DataCategory_Mapping")]
    //public MissingSchemaAction MissingSchemaAction { get; set; }
    //
    // Summary:
    //     Gets or sets whether the Fill method should return provider-specific values
    //     or common CLS-compliant values.
    //
    // Returns:
    //     true if the Fill method should return provider-specific values; otherwise
    //     false to return common CLS-compliant values.
    //[ResDescription("DataAdapter_ReturnProviderSpecificTypes")]
    //[ResCategory("DataCategory_Fill")]
    //[DefaultValue(false)]
    //public virtual bool ReturnProviderSpecificTypes { get; set; }
    //
    // Summary:
    //     Gets a collection that provides the master mapping between a source table
    //     and a System.Data.DataTable.
    //
    // Returns:
    //     A collection that provides the master mapping between the returned records
    //     and the System.Data.DataSet. The default value is an empty collection.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    //[ResCategory("DataCategory_Mapping")]
    //[ResDescription("DataAdapter_TableMappings")]
    //public DataTableMappingCollection TableMappings { get; }

    // Summary:
    //     Returned when an error occurs during a fill operation.
    //[ResCategory("DataCategory_Fill")]
    //[ResDescription("DataAdapter_FillError")]
    //public event FillErrorEventHandler FillError;

    // Summary:
    //     Creates a copy of this instance of System.Data.Common.DataAdapter.
    //
    // Returns:
    //     The cloned instance of System.Data.Common.DataAdapter.
    //[Obsolete("CloneInternals() has been deprecated.  Use the DataAdapter(DataAdapter from) constructor.  http://go.microsoft.com/fwlink/?linkid=14202")]
    //protected virtual DataAdapter CloneInternals();
    //
    // Summary:
    //     Creates a new System.Data.Common.DataTableMappingCollection.
    //
    // Returns:
    //     A new System.Data.Common.DataTableMappingCollection.
    //protected virtual DataTableMappingCollection CreateTableMappings();
    ////
    // Summary:
    //     Releases the unmanaged resources used by the System.Data.Common.DataAdapter
    //     and optionally releases the managed resources.
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    //protected override void Dispose(bool disposing);
    //
    // Summary:
    //     Adds or refreshes rows in the System.Data.DataSet to match those in the data
    //     source.
    //
    // Parameters:
    //   dataSet:
    //     A System.Data.DataSet to fill with records and, if necessary, schema.
    //
    // Returns:
    //     The number of rows successfully added to or refreshed in the System.Data.DataSet.
    //     This does not include rows affected by statements that do not return rows.
    //public virtual int Fill(DataSet dataSet);
    //
    // Summary:
    //     Adds or refreshes rows in the System.Data.DataTable to match those in the
    //     data source using the System.Data.DataTable name and the specified System.Data.IDataReader.
    //
    // Parameters:
    //   dataTable:
    //     A System.Data.DataTable to fill with records.
    //
    //   dataReader:
    //     An instance of System.Data.IDataReader.
    //
    // Returns:
    //     The number of rows successfully added to or refreshed in the System.Data.DataTable.
    //     This does not include rows affected by statements that do not return rows.
    //protected virtual int Fill(DataTable dataTable, IDataReader dataReader);
    //
    // Summary:
    //     Adds or refreshes rows in a specified range in the collection of System.Data.DataTable
    //     objects to match those in the data source.
    //
    // Parameters:
    //   dataTables:
    //     A collection of System.Data.DataTable objects to fill with records.
    //
    //   dataReader:
    //     An instance of System.Data.IDataReader.
    //
    //   startRecord:
    //     An integer indicating the location of the starting record.
    //
    //   maxRecords:
    //     An integer indicating the maximum number of records.
    //
    // Returns:
    //     The number of rows successfully added to or refreshed in the System.Data.DataTable.
    //     This does not include rows affected by statements that do not return rows.
    //protected virtual int Fill(DataTable[] dataTables, IDataReader dataReader, int startRecord, int maxRecords);
    //
    // Summary:
    //     Adds or refreshes rows in a specified range in the System.Data.DataSet to
    //     match those in the data source using the System.Data.DataSet and System.Data.DataTable
    //     names.
    //
    // Parameters:
    //   dataSet:
    //     A System.Data.DataSet to fill with records.
    //
    //   srcTable:
    //     A string indicating the name of the source table.
    //
    //   dataReader:
    //     An instance of System.Data.IDataReader.
    //
    //   startRecord:
    //     An integer indicating the location of the starting record.
    //
    //   maxRecords:
    //     An integer indicating the maximum number of records.
    //
    // Returns:
    //     The number of rows successfully added to or refreshed in the System.Data.DataSet.
    //     This does not include rows affected by statements that do not return rows.
    //protected virtual int Fill(DataSet dataSet, string srcTable, IDataReader dataReader, int startRecord, int maxRecords);
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
    // Returns:
    //     A System.Data.DataTable object that contains schema information returned
    //     from the data source.
    //public virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType);
    //
    // Summary:
    //     Adds a System.Data.DataTable to the specified System.Data.DataSet.
    //
    // Parameters:
    //   dataTable:
    //     The System.Data.DataTable to be filled from the System.Data.IDataReader.
    //
    //   schemaType:
    //     One of the System.Data.SchemaType values.
    //
    //   dataReader:
    //     The System.Data.IDataReader to be used as the data source when filling the
    //     System.Data.DataTable.
    //
    // Returns:
    //     A System.Data.DataTable object that contains schema information returned
    //     from the data source.
    //protected virtual DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDataReader dataReader);
    //
    // Summary:
    //     Adds a System.Data.DataTable to the specified System.Data.DataSet.
    //
    // Parameters:
    //   dataSet:
    //     The System.Data.DataTable to be filled from the System.Data.IDataReader.
    //
    //   schemaType:
    //     One of the System.Data.SchemaType values.
    //
    //   srcTable:
    //     The name of the source table to use for table mapping.
    //
    //   dataReader:
    //     The System.Data.IDataReader to be used as the data source when filling the
    //     System.Data.DataTable.
    //
    // Returns:
    //     A reference to a collection of System.Data.DataTable objects that were added
    //     to the System.Data.DataSet.
    //protected virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable, IDataReader dataReader);
    //
    // Summary:
    //     Gets the parameters set by the user when executing an SQL SELECT statement.
    //
    // Returns:
    //     An array of System.Data.IDataParameter objects that contains the parameters
    //     set by the user.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public virtual IDataParameter[] GetFillParameters();
    //
    // Summary:
    //     Indicates whether a System.Data.Common.DataTableMappingCollection has been
    //     created.
    //
    // Returns:
    //     true if a System.Data.Common.DataTableMappingCollection has been created;
    //     otherwise false.
    //protected bool HasTableMappings();
    //
    // Summary:
    //     Invoked when an error occurs during a Fill.
    //
    // Parameters:
    //   value:
    //     A System.Data.FillErrorEventArgs object.
    //protected virtual void OnFillError(FillErrorEventArgs value);
    //
    // Summary:
    //     Resets System.Data.Common.DataAdapter.FillLoadOption to its default state
    //     and causes System.Data.Common.DataAdapter.Fill(System.Data.DataSet) to honor
    //     System.Data.Common.DataAdapter.AcceptChangesDuringFill.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public void ResetFillLoadOption();
    //
    // Summary:
    //     Determines whether the System.Data.Common.DataAdapter.AcceptChangesDuringFill
    //     property should be persisted.
    //
    // Returns:
    //     true if the System.Data.Common.DataAdapter.AcceptChangesDuringFill property
    //     is persisted; otherwise false.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public virtual bool ShouldSerializeAcceptChangesDuringFill();
    //
    // Summary:
    //     Determines whether the System.Data.Common.DataAdapter.FillLoadOption property
    //     should be persisted.
    //
    // Returns:
    //     true if the System.Data.Common.DataAdapter.FillLoadOption property is persisted;
    //     otherwise false.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public virtual bool ShouldSerializeFillLoadOption();
    //
    // Summary:
    //     Determines whether one or more System.Data.Common.DataTableMapping objects
    //     exist and they should be persisted.
    //
    // Returns:
    //     true if one or more System.Data.Common.DataTableMapping objects exist; otherwise
    //     false.
    //protected virtual bool ShouldSerializeTableMappings();
    //
    // Summary:
    //     Calls the respective INSERT, UPDATE, or DELETE statements for each inserted,
    //     updated, or deleted row in the specified System.Data.DataSet from a System.Data.DataTable
    //     named "Table."
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
    public virtual int Update(DataSet dataSet)
    {
      Contract.Requires(dataSet != null);

      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
  }
}

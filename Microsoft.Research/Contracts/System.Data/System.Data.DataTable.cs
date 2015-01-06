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

using System.Diagnostics.Contracts;
using System.Xml.Schema;
using System.Runtime.Serialization;

namespace System.Data
{
  // Summary:
  //     Represents one table of in-memory data.
  //[Serializable]
  //[DefaultProperty("TableName")]
  //[DesignTimeVisible(false)]
  //[DefaultEvent("RowChanging")]
  //[Editor("Microsoft.VSDesigner.Data.Design.DataTableEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  //[XmlSchemaProvider("GetDataTableSchema")]
  //[ToolboxItem(false)]
  public class DataTable //: MarshalByValueComponent, IListSource, ISupportInitializeNotification, ISupportInitialize, ISerializable, IXmlSerializable
  {
    //protected internal bool fInitInProgress;

    // Summary:
    //     Initializes a new instance of the System.Data.DataTable class with no arguments.
    //public DataTable();
    //
    // Summary:
    //     Initializes a new instance of the System.Data.DataTable class with the specified
    //     table name.
    //
    // Parameters:
    //   tableName:
    //     The name to give the table. If tableName is null or an empty string, a default
    //     name is given when added to the System.Data.DataTableCollection.
    //public DataTable(string tableName);
    //
    // Summary:
    //     Initializes a new instance of the System.Data.DataTable class with the System.Runtime.Serialization.SerializationInfo
    //     and the System.Runtime.Serialization.StreamingContext.
    //
    // Parameters:
    //   info:
    //     The data needed to serialize or deserialize an object.
    //
    //   context:
    //     The source and destination of a given serialized stream.
    //protected DataTable(SerializationInfo info, StreamingContext context);
    //
    // Summary:
    //     Initializes a new instance of the System.Data.DataTable class using the specified
    //     table name and namespace.
    //
    // Parameters:
    //   tableName:
    //     The name to give the table. If tableName is null or an empty string, a default
    //     name is given when added to the System.Data.DataTableCollection.
    //
    //   tableNamespace:
    //     The namespace for the XML representation of the data stored in the DataTable.
    //public DataTable(string tableName, string tableNamespace);

    // Summary:
    //     Indicates whether string comparisons within the table are case-sensitive.
    //
    // Returns:
    //     true if the comparison is case-sensitive; otherwise false. The default is
    //     set to the parent System.Data.DataSet object's System.Data.DataSet.CaseSensitive
    //     property, or false if the System.Data.DataTable was created independently
    //     of a System.Data.DataSet.
    //[ResDescription("DataTableCaseSensitiveDescr")]
    //public bool CaseSensitive { get; set; }
    //
    // Summary:
    //     Gets the collection of child relations for this System.Data.DataTable.
    //
    // Returns:
    //     A System.Data.DataRelationCollection that contains the child relations for
    //     the table. An empty collection is returned if no System.Data.DataRelation
    //     objects exist.
    //[ResDescription("DataTableChildRelationsDescr")]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public DataRelationCollection ChildRelations { get; }
    //
    // Summary:
    //     Gets the collection of columns that belong to this table.
    //
    // Returns:
    //     A System.Data.DataColumnCollection that contains the collection of System.Data.DataColumn
    //     objects for the table. An empty collection is returned if no System.Data.DataColumn
    //     objects exist.
    //[ResCategory("DataCategory_Data")]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    //[ResDescription("DataTableColumnsDescr")]
    public DataColumnCollection Columns
    {
      get
      {
        Contract.Ensures(Contract.Result<DataColumnCollection>() != null);

        return default(DataColumnCollection);
      }
    }
    //
    // Summary:
    //     Gets the collection of constraints maintained by this table.
    //
    // Returns:
    //     A System.Data.ConstraintCollection that contains the collection of System.Data.Constraint
    //     objects for the table. An empty collection is returned if no System.Data.Constraint
    //     objects exist.
    //[ResDescription("DataTableConstraintsDescr")]
    //[ResCategory("DataCategory_Data")]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ConstraintCollection Constraints
    {
      get
      {
        Contract.Ensures(Contract.Result<ConstraintCollection>() != null);

        return default(ConstraintCollection);
      }
    }


    //
    // Summary:
    //     Gets the System.Data.DataSet to which this table belongs.
    //
    // Returns:
    //     The System.Data.DataSet to which this table belongs.
    //[Browsable(false)]
    //[ResDescription("DataTableDataSetDescr")]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public DataSet DataSet { get; }


    //
    // Summary:
    //     Gets a customized view of the table that may include a filtered view, or
    //     a cursor position.
    //
    // Returns:
    //     The System.Data.DataView associated with the System.Data.DataTable.
    //[Browsable(false)]
    //[ResDescription("DataTableDefaultViewDescr")]
    //public DataView DefaultView { get; }
    //
    // Summary:
    //     Gets or sets the expression that returns a value used to represent this table
    //     in the user interface. The DisplayExpression property lets you display the
    //     name of this table in a user interface.
    //
    // Returns:
    //     A display string.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DataTableDisplayExpressionDescr")]
    //[DefaultValue("")]
    //public string DisplayExpression { get; set; }
    //
    // Summary:
    //     Gets the collection of customized user information.
    //
    // Returns:
    //     A System.Data.PropertyCollection that contains custom user information.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("ExtendedPropertiesDescr")]
    //[Browsable(false)]
    public PropertyCollection ExtendedProperties
    {
      get
      {
        Contract.Ensures(Contract.Result<PropertyCollection>() != null);

        return default(PropertyCollection);
      }
    }

    //
    // Summary:
    //     Gets a value indicating whether there are errors in any of the rows in any
    //     of the tables of the System.Data.DataSet to which the table belongs.
    //
    // Returns:
    //     true if errors exist; otherwise false.
    //[Browsable(false)]
    //[ResDescription("DataTableHasErrorsDescr")]
    //public bool HasErrors { get; }
    //
    // Summary:
    //     Gets a value that indicates whether the System.Data.DataTable is initialized.
    //
    // Returns:
    //     true to indicate the component has completed initialization; otherwise false.
    //[Browsable(false)]
    //public bool IsInitialized { get; }
    //
    // Summary:
    //     Gets or sets the locale information used to compare strings within the table.
    //
    // Returns:
    //     A System.Globalization.CultureInfo that contains data about the user's machine
    //     locale. The default is the System.Data.DataSet object's System.Globalization.CultureInfo
    //     (returned by the System.Data.DataSet.Locale property) to which the System.Data.DataTable
    //     belongs; if the table doesn't belong to a System.Data.DataSet, the default
    //     is the current system System.Globalization.CultureInfo.
    //[ResDescription("DataTableLocaleDescr")]
    //public CultureInfo Locale { get; set; }
    //
    // Summary:
    //     Gets or sets the initial starting size for this table.
    //
    // Returns:
    //     The initial starting size in rows of this table. The default is 50.
    //[ResCategory("DataCategory_Data")]
    //[DefaultValue(50)]
    //[ResDescription("DataTableMinimumCapacityDescr")]
    public int MinimumCapacity
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
      }
    }


    //
    // Summary:
    //     Gets or sets the namespace for the XML representation of the data stored
    //     in the System.Data.DataTable.
    //
    // Returns:
    //     The namespace of the System.Data.DataTable.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DataTableNamespaceDescr")]

    // ** F: Needs some more reflector inspection to "prove" that Namespace != null

    //public string Namespace { get; set; }

    //
    // Summary:
    //     Gets the collection of parent relations for this System.Data.DataTable.
    //
    // Returns:
    //     A System.Data.DataRelationCollection that contains the parent relations for
    //     the table. An empty collection is returned if no System.Data.DataRelation
    //     objects exist.
    //[ResDescription("DataTableParentRelationsDescr")]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    public DataRelationCollection ParentRelations
    {
      get
      {
        Contract.Ensures(Contract.Result<DataRelationCollection>() != null);

        return default(DataRelationCollection);
      }
    }


    //
    // Summary:
    //     Gets or sets the namespace for the XML representation of the data stored
    //     in the System.Data.DataTable.
    //
    // Returns:
    //     The prefix of the System.Data.DataTable.
    //[DefaultValue("")]
    //[ResDescription("DataTablePrefixDescr")]
    //[ResCategory("DataCategory_Data")]
    //public string Prefix { get; set; }
    //
    // Summary:
    //     Gets or sets an array of columns that function as primary keys for the data
    //     table.
    //
    // Returns:
    //     An array of System.Data.DataColumn objects.
    //
    // Exceptions:
    //   System.Data.DataException:
    //     The key is a foreign key.
    //[Editor("Microsoft.VSDesigner.Data.Design.PrimaryKeyEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    //[ResDescription("DataTablePrimaryKeyDescr")]
    //[TypeConverter(typeof(PrimaryKeyTypeConverter))]
    //[ResCategory("DataCategory_Data")]
    public DataColumn[] PrimaryKey
    {
      get
      {
        Contract.Ensures(Contract.Result<DataColumn[]>() != null);

        return default(DataColumn[]);
      }
      set { }
    }
    //
    // Summary:
    //     Gets or sets the serialization format.
    //
    // Returns:
    //     A System.Data.SerializationFormat enumeration specifying either Binary or
    //     Xml serialization.
    //public SerializationFormat RemotingFormat { get; set; }
    //
    // Summary:
    //     Gets the collection of rows that belong to this table.
    //
    // Returns:
    //     A System.Data.DataRowCollection that contains System.Data.DataRow objects;
    //     otherwise a null value if no System.Data.DataRow objects exist.
    //[Browsable(false)]
    //[ResDescription("DataTableRowsDescr")]
    //public DataRowCollection Rows { get; }
    //
    // Summary:
    //     Gets or sets an System.ComponentModel.ISite for the System.Data.DataTable.
    //
    // Returns:
    //     An System.ComponentModel.ISite for the System.Data.DataTable.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public override ISite Site { get; set; }
    //
    // Summary:
    //     Gets or sets the name of the System.Data.DataTable.
    //
    // Returns:
    //     The name of the System.Data.DataTable.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     null or empty string ("") is passed in and this table belongs to a collection.
    //
    //   System.Data.DuplicateNameException:
    //     The table belongs to a collection that already has a table with the same
    //     name. (Comparison is case-sensitive).
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DataTableTableNameDescr")]
    //[DefaultValue("")]
    //[RefreshProperties(RefreshProperties.All)]
    //public string TableName { get; set; }

    // Summary:
    //     Occurs after a value has been changed for the specified System.Data.DataColumn
    //     in a System.Data.DataRow.
    //[ResDescription("DataTableColumnChangedDescr")]
    //[ResCategory("DataCategory_Data")]
    //public event DataColumnChangeEventHandler ColumnChanged;
    //
    // Summary:
    //     Occurs when a value is being changed for the specified System.Data.DataColumn
    //     in a System.Data.DataRow.
    //[ResDescription("DataTableColumnChangingDescr")]
    //[ResCategory("DataCategory_Data")]
    //public event DataColumnChangeEventHandler ColumnChanging;
    //
    // Summary:
    //     Occurs after the System.Data.DataTable is initialized.
    //[ResDescription("DataSetInitializedDescr")]
    //[ResCategory("DataCategory_Action")]
    //public event EventHandler Initialized;
    //
    // Summary:
    //     Occurs after a System.Data.DataRow has been changed successfully.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DataTableRowChangedDescr")]
    //public event DataRowChangeEventHandler RowChanged;
    //
    // Summary:
    //     Occurs when a System.Data.DataRow is changing.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DataTableRowChangingDescr")]
    //public event DataRowChangeEventHandler RowChanging;
    //
    // Summary:
    //     Occurs after a row in the table has been deleted.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DataTableRowDeletedDescr")]
    //public event DataRowChangeEventHandler RowDeleted;
    //
    // Summary:
    //     Occurs before a row in the table is about to be deleted.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DataTableRowDeletingDescr")]
    //public event DataRowChangeEventHandler RowDeleting;
    //
    // Summary:
    //     Occurs after a System.Data.DataTable is cleared.
    //[ResDescription("DataTableRowsClearedDescr")]
    //[ResCategory("DataCategory_Data")]
    //public event DataTableClearEventHandler TableCleared;
    //
    // Summary:
    //     Occurs when a System.Data.DataTable is cleared.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DataTableRowsClearingDescr")]
    //public event DataTableClearEventHandler TableClearing;
    //
    // Summary:
    //     Occurs when a new System.Data.DataRow is inserted.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DataTableRowsNewRowDescr")]
    //public event DataTableNewRowEventHandler TableNewRow;

    // Summary:
    //     Commits all the changes made to this table since the last time System.Data.DataTable.AcceptChanges()
    //     was called.
    //public void AcceptChanges();
    //
    // Summary:
    //     Begins the initialization of a System.Data.DataTable that is used on a form
    //     or used by another component. The initialization occurs at run time.
    //public virtual void BeginInit();
    //
    // Summary:
    //     Turns off notifications, index maintenance, and constraints while loading
    //     data.
    //public void BeginLoadData();
    //
    // Summary:
    //     Clears the System.Data.DataTable of all data.
    //public void Clear();
    //
    // Summary:
    //     Clones the structure of the System.Data.DataTable, including all System.Data.DataTable
    //     schemas and constraints.
    //
    // Returns:
    //     A new System.Data.DataTable with the same schema as the current System.Data.DataTable.
    public virtual DataTable Clone()
    {
      Contract.Ensures(Contract.Result<DataTable>() != null);

      return default(DataTable);
    }

    //
    // Summary:
    //     Computes the given expression on the current rows that pass the filter criteria.
    //
    // Parameters:
    //   expression:
    //     The expression to compute.
    //
    //   filter:
    //     The filter to limit the rows that evaluate in the expression.
    //
    // Returns:
    //     An System.Object, set to the result of the computation.
    //public object Compute(string expression, string filter);
    //
    // Summary:
    //     Copies both the structure and data for this System.Data.DataTable.
    //
    // Returns:
    //     A new System.Data.DataTable with the same structure (table schemas and constraints)
    //     and data as this System.Data.DataTable.If these classes have been derived,
    //     the copy will also be of the same derived classes.Both the System.Data.DataTable.Copy()
    //     and the System.Data.DataTable.Clone() methods create a new DataTable with
    //     the same structure as the original DataTable. The new DataTable created by
    //     the System.Data.DataTable.Copy() method has the same set of DataRows as the
    //     original table, but the new DataTable created by the System.Data.DataTable.Clone()
    //     method does not contain any DataRows.
    public DataTable Copy()
    {
      Contract.Ensures(Contract.Result<DataTable>() != null);

      return default(DataTable);
    }

    //
    // Summary:
    //     Returns a System.Data.DataTableReader corresponding to the data within this
    //     System.Data.DataTable.
    //
    // Returns:
    //     A System.Data.DataTableReader containing one result set, corresponding to
    //     the source System.Data.DataTable instance.
    public DataTableReader CreateDataReader()
    {
      Contract.Ensures(Contract.Result<DataTableReader>() != null);

      return default(DataTableReader);
    }

    //
    //protected virtual DataTable CreateInstance();
    //
    // Summary:
    //     Ends the initialization of a System.Data.DataTable that is used on a form
    //     or used by another component. The initialization occurs at run time.
    //public virtual void EndInit();
    //
    // Summary:
    //     Turns on notifications, index maintenance, and constraints after loading
    //     data.
    //public void EndLoadData();
    //
    // Summary:
    //     Gets a copy of the System.Data.DataTable that contains all changes made to
    //     it since it was loaded or System.Data.DataTable.AcceptChanges() was last
    //     called.
    //
    // Returns:
    //     A copy of the changes from this System.Data.DataTable, or null if no changes
    //     are found.
    [Pure]
    extern public DataTable GetChanges();
    //
    // Summary:
    //     Gets a copy of the System.Data.DataTable containing all changes made to it
    //     since it was last loaded, or since System.Data.DataTable.AcceptChanges()
    //     was called, filtered by System.Data.DataRowState.
    //
    // Parameters:
    //   rowStates:
    //     One of the System.Data.DataRowState values.
    //
    // Returns:
    //     A filtered copy of the System.Data.DataTable that can have actions performed
    //     on it, and later be merged back in the System.Data.DataTable using System.Data.DataSet.Merge(System.Data.DataSet).
    //     If no rows of the desired System.Data.DataRowState are found, the method
    //     returns null.
    [Pure]
    extern public DataTable GetChanges(DataRowState rowStates);
    //
    // Summary:
    //     This method returns an System.Xml.Schema.XmlSchemaSet instance containing
    //     the Web Services Description Language (WSDL) that describes the System.Data.DataTable
    //     for Web Services.
    //
    // Parameters:
    //   schemaSet:
    //     An System.Xml.Schema.XmlSchemaSet instance.
    [Pure]
    public static XmlSchemaComplexType GetDataTableSchema(XmlSchemaSet schemaSet)
    {
      Contract.Ensures(Contract.Result<XmlSchemaComplexType>() != null);

      return default(XmlSchemaComplexType);
    }
    //
    // Summary:
    //     Gets an array of System.Data.DataRow objects that contain errors.
    //
    // Returns:
    //     An array of System.Data.DataRow objects that have errors.
    [Pure]
    public DataRow[] GetErrors()
    {
      Contract.Ensures(Contract.Result<DataRow[]>() != null);

      return default(DataRow[]);
    }
    //
    // Summary:
    //     Populates a serialization information object with the data needed to serialize
    //     the System.Data.DataTable.
    //
    // Parameters:
    //   info:
    //     A System.Runtime.Serialization.SerializationInfo object that holds the serialized
    //     data associated with the System.Data.DataTable.
    //
    //   context:
    //     A System.Runtime.Serialization.StreamingContext object that contains the
    //     source and destination of the serialized stream associated with the System.Data.DataTable.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The info parameter is a null reference (Nothing in Visual Basic).
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      Contract.Requires(info != null);
    }
    //
    //protected virtual Type GetRowType();
    //

    // ** F : It can be null
    //protected virtual XmlSchema GetSchema();

    // Summary:
    //     Copies a System.Data.DataRow into a System.Data.DataTable, preserving any
    //     property settings, as well as original and current values.
    //
    // Parameters:
    //   row:
    //     The System.Data.DataRow to be imported.

    // ** F: row can be null
    //public void ImportRow(DataRow row);
    //
    // Summary:
    //     Fills a System.Data.DataTable with values from a data source using the supplied
    //     System.Data.IDataReader. If the System.Data.DataTable already contains rows,
    //     the incoming data from the data source is merged with the existing rows.
    //
    // Parameters:
    //   reader:
    //     An System.Data.IDataReader that provides a result set.
    //public void Load(IDataReader reader);
    //
    // Summary:
    //     Fills a System.Data.DataTable with values from a data source using the supplied
    //     System.Data.IDataReader. If the DataTable already contains rows, the incoming
    //     data from the data source is merged with the existing rows according to the
    //     value of the loadOption parameter.
    //
    // Parameters:
    //   reader:
    //     An System.Data.IDataReader that provides one or more result sets.
    //
    //   loadOption:
    //     A value from the System.Data.LoadOption enumeration that indicates how rows
    //     already in the System.Data.DataTable are combined with incoming rows that
    //     share the same primary key.
    //public void Load(IDataReader reader, LoadOption loadOption);
    //
    // Summary:
    //     Fills a System.Data.DataTable with values from a data source using the supplied
    //     System.Data.IDataReader using an error-handling delegate.
    //
    // Parameters:
    //   reader:
    //     A System.Data.IDataReader that provides a result set.
    //
    //   loadOption:
    //     A value from the System.Data.LoadOption enumeration that indicates how rows
    //     already in the System.Data.DataTable are combined with incoming rows that
    //     share the same primary key.
    //
    //   errorHandler:
    //     A System.Data.FillErrorEventHandler delegate to call when an error occurs
    //     while loading data.
    //public virtual void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler errorHandler);
    //
    // Summary:
    //     Finds and updates a specific row. If no matching row is found, a new row
    //     is created using the given values.
    //
    // Parameters:
    //   values:
    //     An array of values used to create the new row.
    //
    //   fAcceptChanges:
    //     true to accept changes; otherwise false.
    //
    // Returns:
    //     The new System.Data.DataRow.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The array is larger than the number of columns in the table.
    //
    //   System.InvalidCastException:
    //     A value doesn't match its respective column type.
    //
    //   System.Data.ConstraintException:
    //     Adding the row invalidates a constraint.
    //
    //   System.Data.NoNullAllowedException:
    //     Attempting to put a null in a column where System.Data.DataColumn.AllowDBNull
    //     is false.
    public DataRow LoadDataRow(object[] values, bool fAcceptChanges)
    {
      // F: I do not know how to "express" the number of Columns
      //Contract.Requires(values.Length <= this.Columns.Count);

      return default(DataRow);
    }

    //
    // Summary:
    //     Finds and updates a specific row. If no matching row is found, a new row
    //     is created using the given values.
    //
    // Parameters:
    //   values:
    //     An array of values used to create the new row.
    //
    //   loadOption:
    //     Used to determine how the array values are applied to the corresponding values
    //     in an existing row.
    //
    // Returns:
    //     The new System.Data.DataRow.
    //public DataRow LoadDataRow(object[] values, LoadOption loadOption);
    //
    // Summary:
    //     Merge the specified System.Data.DataTable with the current System.Data.DataTable.
    //
    // Parameters:
    //   table:
    //     The System.Data.DataTable to be merged with the current System.Data.DataTable.
    //public void Merge(DataTable table);
    //
    // Summary:
    //     Merge the specified System.Data.DataTable with the current DataTable, indicating
    //     whether to preserve changes in the current DataTable.
    //
    // Parameters:
    //   table:
    //     The DataTable to be merged with the current DataTable.
    //
    //   preserveChanges:
    //     true, to preserve changes in the current DataTable; otherwise false.
    //public void Merge(DataTable table, bool preserveChanges);
    //
    // Summary:
    //     Merge the specified System.Data.DataTable with the current DataTable, indicating
    //     whether to preserve changes and how to handle missing schema in the current
    //     DataTable.
    //
    // Parameters:
    //   table:
    //     The System.Data.DataTable to be merged with the current System.Data.DataTable.
    //
    //   preserveChanges:
    //     true, to preserve changes in the current System.Data.DataTable; otherwise
    //     false.
    //
    //   missingSchemaAction:
    //     One of the System.Data.MissingSchemaAction values.
    //public void Merge(DataTable table, bool preserveChanges, MissingSchemaAction missingSchemaAction);
    //
    // Summary:
    //     Creates a new System.Data.DataRow with the same schema as the table.
    //
    // Returns:
    //     A System.Data.DataRow with the same schema as the System.Data.DataTable.
    public DataRow NewRow()
    {
      Contract.Ensures(Contract.Result<DataRow>() != null);

      return default(DataRow);
    }
    //
    //protected internal DataRow[] NewRowArray(int size);
    //
    // Summary:
    //     Creates a new row from an existing row.
    //
    // Parameters:
    //   builder:
    //     A System.Data.DataRowBuilder object.
    //
    // Returns:
    //     A System.Data.DataRow derived class.
    //protected virtual DataRow NewRowFromBuilder(DataRowBuilder builder);
    //
    // Summary:
    //     Raises the System.Data.DataTable.ColumnChanged event.
    //
    // Parameters:
    //   e:
    //     A System.Data.DataColumnChangeEventArgs that contains the event data.
    //protected internal virtual void OnColumnChanged(DataColumnChangeEventArgs e);
    //
    // Summary:
    //     Raises the System.Data.DataTable.ColumnChanging event.
    //
    // Parameters:
    //   e:
    //     A System.Data.DataColumnChangeEventArgs that contains the event data.
    //protected internal virtual void OnColumnChanging(DataColumnChangeEventArgs e);
    //
    // Summary:
    //     Raises the System.ComponentModel.INotifyPropertyChanged.PropertyChanged event.
    //
    // Parameters:
    //   pcevent:
    //     A System.ComponentModel.PropertyChangedEventArgs that contains the event
    //     data.
    //protected virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent);
    //
    // Summary:
    //     Notifies the System.Data.DataTable that a System.Data.DataColumn is being
    //     removed.
    //
    // Parameters:
    //   column:
    //     The System.Data.DataColumn being removed.
    //protected virtual void OnRemoveColumn(DataColumn column);
    //
    // Summary:
    //     Raises the System.Data.DataTable.RowChanged event.
    //
    // Parameters:
    //   e:
    //     A System.Data.DataRowChangeEventArgs that contains the event data.
    //protected virtual void OnRowChanged(DataRowChangeEventArgs e);
    //
    // Summary:
    //     Raises the System.Data.DataTable.RowChanging event.
    //
    // Parameters:
    //   e:
    //     A System.Data.DataRowChangeEventArgs that contains the event data.
    //protected virtual void OnRowChanging(DataRowChangeEventArgs e);
    //
    // Summary:
    //     Raises the System.Data.DataTable.RowDeleted event.
    //
    // Parameters:
    //   e:
    //     A System.Data.DataRowChangeEventArgs that contains the event data.
    //protected virtual void OnRowDeleted(DataRowChangeEventArgs e);
    //
    // Summary:
    //     Raises the System.Data.DataTable.RowDeleting event.
    //
    // Parameters:
    //   e:
    //     A System.Data.DataRowChangeEventArgs that contains the event data.
    //protected virtual void OnRowDeleting(DataRowChangeEventArgs e);
    //
    // Summary:
    //     Raises the System.Data.DataTable.TableCleared event.
    //
    // Parameters:
    //   e:
    //     A System.Data.DataTableClearEventArgs that contains the event data.
    //protected virtual void OnTableCleared(DataTableClearEventArgs e);
    //
    // Summary:
    //     Raises the System.Data.DataTable.TableClearing event.
    //
    // Parameters:
    //   e:
    //     A System.Data.DataTableClearEventArgs that contains the event data.
    //protected virtual void OnTableClearing(DataTableClearEventArgs e);
    //
    // Summary:
    //     Raises the System.Data.DataTable.TableNewRow event.
    //
    // Parameters:
    //   e:
    //     A System.Data.DataTableNewRowEventArgs that contains the event data.
    //protected virtual void OnTableNewRow(DataTableNewRowEventArgs e);
    //
    // Summary:
    //     Reads XML schema and data into the System.Data.DataTable using the specified
    //     System.IO.Stream.
    //
    // Parameters:
    //   stream:
    //     An object that derives from System.IO.Stream
    //
    // Returns:
    //     The System.Data.XmlReadMode used to read the data.
    //public XmlReadMode ReadXml(Stream stream);
    //
    // Summary:
    //     Reads XML schema and data into the System.Data.DataTable from the specified
    //     file.
    //
    // Parameters:
    //   fileName:
    //     The name of the file from which to read the data.
    //
    // Returns:
    //     The System.Data.XmlReadMode used to read the data.
    //public XmlReadMode ReadXml(string fileName);
    //
    // Summary:
    //     Reads XML schema and data into the System.Data.DataTable using the specified
    //     System.IO.TextReader.
    //
    // Parameters:
    //   reader:
    //     The System.IO.TextReader that will be used to read the data.
    //
    // Returns:
    //     The System.Data.XmlReadMode used to read the data.
    //public XmlReadMode ReadXml(TextReader reader);
    //
    // Summary:
    //     Reads XML Schema and Data into the System.Data.DataTable using the specified
    //     System.Xml.XmlReader.
    //
    // Parameters:
    //   reader:
    //     The System.Xml.XmlReader that will be used to read the data.
    //
    // Returns:
    //     The System.Data.XmlReadMode used to read the data.
    //public XmlReadMode ReadXml(XmlReader reader);
    //
    // Summary:
    //     Reads an XML schema into the System.Data.DataTable using the specified stream.
    //
    // Parameters:
    //   stream:
    //     The stream used to read the schema.
    //public void ReadXmlSchema(Stream stream);
    //
    // Summary:
    //     Reads an XML schema into the System.Data.DataTable from the specified file.
    //
    // Parameters:
    //   fileName:
    //     The name of the file from which to read the schema information.
    public void ReadXmlSchema(string fileName)
    {
      Contract.Requires(fileName != null);
    }
    //
    // Summary:
    //     Reads an XML schema into the System.Data.DataTable using the specified System.IO.TextReader.
    //
    // Parameters:
    //   reader:
    //     The System.IO.TextReader used to read the schema information.

    // ** F: It seems readerc can be == null
    //public void ReadXmlSchema(TextReader reader);
    //
    // Summary:
    //     Reads an XML schema into the System.Data.DataTable using the specified System.Xml.XmlReader.
    //
    // Parameters:
    //   reader:
    //     The System.Xml.XmlReader used to read the schema information.
    //public void ReadXmlSchema(XmlReader reader);
    //
    //protected virtual void ReadXmlSerializable(XmlReader reader);
    //
    // Summary:
    //     Rolls back all changes that have been made to the table since it was loaded,
    //     or the last time System.Data.DataTable.AcceptChanges() was called.
    //public void RejectChanges();
    //
    // Summary:
    //     Resets the System.Data.DataTable to its original state.
    //public virtual void Reset();
    //
    // Summary:
    //     Gets an array of all System.Data.DataRow objects.
    //
    // Returns:
    //     An array of System.Data.DataRow objects.
    public DataRow[] Select()
    {
      Contract.Ensures(Contract.Result<DataRow[]>() != null);

      return default(DataRow[]);
    }

    //
    // Summary:
    //     Gets an array of all System.Data.DataRow objects that match the filter criteria
    //     in order of primary key (or lacking one, order of addition.)
    //
    // Parameters:
    //   filterExpression:
    //     The criteria to use to filter the rows.
    //
    // Returns:
    //     An array of System.Data.DataRow objects.
    public DataRow[] Select(string filterExpression)
    {
      Contract.Ensures(Contract.Result<DataRow[]>() != null);

      return default(DataRow[]);
    }

    //
    // Summary:
    //     Gets an array of all System.Data.DataRow objects that match the filter criteria,
    //     in the specified sort order.
    //
    // Parameters:
    //   filterExpression:
    //     The criteria to use to filter the rows.
    //
    //   sort:
    //     A string specifying the column and sort direction.
    //
    // Returns:
    //     An array of System.Data.DataRow objects matching the filter expression.
    public DataRow[] Select(string filterExpression, string sort)
    {
      Contract.Ensures(Contract.Result<DataRow[]>() != null);

      return default(DataRow[]);
    }

    //
    // Summary:
    //     Gets an array of all System.Data.DataRow objects that match the filter in
    //     the order of the sort that match the specified state.
    //
    // Parameters:
    //   filterExpression:
    //     The criteria to use to filter the rows.
    //
    //   sort:
    //     A string specifying the column and sort direction.
    //
    //   recordStates:
    //     One of the System.Data.DataViewRowState values.
    //
    // Returns:
    //     An array of System.Data.DataRow objects.
    public DataRow[] Select(string filterExpression, string sort, DataViewRowState recordStates)
    {
      Contract.Ensures(Contract.Result<DataRow[]>() != null);

      return default(DataRow[]);
    }

    //
    // Summary:
    //     Gets the System.Data.DataTable.TableName and System.Data.DataTable.DisplayExpression,
    //     if there is one as a concatenated string.
    //
    // Returns:
    //     A string consisting of the System.Data.DataTable.TableName and the System.Data.DataTable.DisplayExpression
    //     values.
    //public override string ToString();
    //
    // Summary:
    //     Writes the current contents of the System.Data.DataTable as XML using the
    //     specified System.IO.Stream.
    //
    // Parameters:
    //   stream:
    //     The stream to which the data will be written.
    //public void WriteXml(Stream stream);
    //
    // Summary:
    //     Writes the current contents of the System.Data.DataTable as XML using the
    //     specified file.
    //
    // Parameters:
    //   fileName:
    //     The file to which to write the XML data.

    // ** F: Probablu here we need the precondition fileName != null, but it needs some more careful inspection with Reflector
    //public void WriteXml(string fileName);
    //
    // Summary:
    //     Writes the current contents of the System.Data.DataTable as XML using the
    //     specified System.IO.TextWriter.
    //
    // Parameters:
    //   writer:
    //     The System.IO.TextWriterwith which to write the content.
    //public void WriteXml(TextWritcer writer);
    //
    // Summary:
    //     Writes the current contents of the System.Data.DataTable as XML using the
    //     specified System.Xml.XmlWriter.
    //
    // Parameters:
    //   writer:
    //     The System.Xml.XmlWriter with which to write the contents.
    //public void WriteXml(XmlWriter writer);
    //
    // Summary:
    //     Writes the current contents of the System.Data.DataTable as XML using the
    //     specified System.IO.Stream. To save the data for the table and all its descendants,
    //     set the writeHierarchy parameter to true.
    //
    // Parameters:
    //   stream:
    //     The stream to which the data will be written.
    //
    //   writeHierarchy:
    //     If true, write the contents of the current table and all its descendants.
    //     If false (the default value), write the data for the current table only.
    //public void WriteXml(Stream stream, bool writeHierarchy);
    //
    // Summary:
    //     Writes the current data, and optionally the schema, for the System.Data.DataTable
    //     to the specified file using the specified System.Data.XmlWriteMode. To write
    //     the schema, set the value for the mode parameter to WriteSchema.
    //
    // Parameters:
    //   stream:
    //     The stream to which the data will be written.
    //
    //   mode:
    //     One of the System.Data.XmlWriteMode values.
    //public void WriteXml(Stream stream, XmlWriteMode mode);
    //
    // Summary:
    //     Writes the current contents of the System.Data.DataTable as XML using the
    //     specified file. To save the data for the table and all its descendants, set
    //     the writeHierarchy parameter to true.
    //
    // Parameters:
    //   fileName:
    //     The file to which to write the XML data.
    //
    //   writeHierarchy:
    //     If true, write the contents of the current table and all its descendants.
    //     If false (the default value), write the data for the current table only.
    //public void WriteXml(string fileName, bool writeHierarchy);
    //
    // Summary:
    //     Writes the current data, and optionally the schema, for the System.Data.DataTable
    //     using the specified file and System.Data.XmlWriteMode. To write the schema,
    //     set the value for the mode parameter to WriteSchema.
    //
    // Parameters:
    //   fileName:
    //     The name of the file to which the data will be written.
    //
    //   mode:
    //     One of the System.Data.XmlWriteMode values.
    //public void WriteXml(string fileName, XmlWriteMode mode);
    //
    // Summary:
    //     Writes the current contents of the System.Data.DataTable as XML using the
    //     specified System.IO.TextWriter. To save the data for the table and all its
    //     descendants, set the writeHierarchy parameter to true.
    //
    // Parameters:
    //   writer:
    //     The System.IO.TextWriter with which to write the content.
    //
    //   writeHierarchy:
    //     If true, write the contents of the current table and all its descendants.
    //     If false (the default value), write the data for the current table only.
    //public void WriteXml(TextWriter writer, bool writeHierarchy);
    //
    // Summary:
    //     Writes the current data, and optionally the schema, for the System.Data.DataTable
    //     using the specified System.IO.TextWriter and System.Data.XmlWriteMode. To
    //     write the schema, set the value for the mode parameter to WriteSchema.
    //
    // Parameters:
    //   writer:
    //     The System.IO.TextWriter used to write the document.
    //
    //   mode:
    //     One of the System.Data.XmlWriteMode values.
    //public void WriteXml(TextWriter writer, XmlWriteMode mode);
    //
    // Summary:
    //     Writes the current contents of the System.Data.DataTable as XML using the
    //     specified System.Xml.XmlWriter.
    //
    // Parameters:
    //   writer:
    //     The System.Xml.XmlWriter with which to write the contents.
    //
    //   writeHierarchy:
    //     If true, write the contents of the current table and all its descendants.
    //     If false (the default value), write the data for the current table only.
    //public void WriteXml(XmlWriter writer, bool writeHierarchy);
    //
    // Summary:
    //     Writes the current data, and optionally the schema, for the System.Data.DataTable
    //     using the specified System.Xml.XmlWriter and System.Data.XmlWriteMode. To
    //     write the schema, set the value for the mode parameter to WriteSchema.
    //
    // Parameters:
    //   writer:
    //     The System.Xml.XmlWriter used to write the document.
    //
    //   mode:
    //     One of the System.Data.XmlWriteMode values.
    //public void WriteXml(XmlWriter writer, XmlWriteMode mode);
    //
    // Summary:
    //     Writes the current data, and optionally the schema, for the System.Data.DataTable
    //     to the specified file using the specified System.Data.XmlWriteMode. To write
    //     the schema, set the value for the mode parameter to WriteSchema. To save
    //     the data for the table and all its descendants, set the writeHierarchy parameter
    //     to true.
    //
    // Parameters:
    //   stream:
    //     The stream to which the data will be written.
    //
    //   mode:
    //     One of the System.Data.XmlWriteMode values.
    //
    //   writeHierarchy:
    //     If true, write the contents of the current table and all its descendants.
    //     If false (the default value), write the data for the current table only.
    //public void WriteXml(Stream stream, XmlWriteMode mode, bool writeHierarchy);
    //
    // Summary:
    //     Writes the current data, and optionally the schema, for the System.Data.DataTable
    //     using the specified file and System.Data.XmlWriteMode. To write the schema,
    //     set the value for the mode parameter to WriteSchema. To save the data for
    //     the table and all its descendants, set the writeHierarchy parameter to true.
    //
    // Parameters:
    //   fileName:
    //     The name of the file to which the data will be written.
    //
    //   mode:
    //     One of the System.Data.XmlWriteMode values.
    //
    //   writeHierarchy:
    //     If true, write the contents of the current table and all its descendants.
    //     If false (the default value), write the data for the current table only.
    //public void WriteXml(string fileName, XmlWriteMode mode, bool writeHierarchy);
    //
    // Summary:
    //     Writes the current data, and optionally the schema, for the System.Data.DataTable
    //     using the specified System.IO.TextWriter and System.Data.XmlWriteMode. To
    //     write the schema, set the value for the mode parameter to WriteSchema. To
    //     save the data for the table and all its descendants, set the writeHierarchy
    //     parameter to true.
    //
    // Parameters:
    //   writer:
    //     The System.IO.TextWriter used to write the document.
    //
    //   mode:
    //     One of the System.Data.XmlWriteMode values.
    //
    //   writeHierarchy:
    //     If true, write the contents of the current table and all its descendants.
    //     If false (the default value), write the data for the current table only.
    //public void WriteXml(TextWriter writer, XmlWriteMode mode, bool writeHierarchy);
    //
    // Summary:
    //     Writes the current data, and optionally the schema, for the System.Data.DataTable
    //     using the specified System.Xml.XmlWriter and System.Data.XmlWriteMode. To
    //     write the schema, set the value for the mode parameter to WriteSchema. To
    //     save the data for the table and all its descendants, set the writeHierarchy
    //     parameter to true.
    //
    // Parameters:
    //   writer:
    //     The System.Xml.XmlWriter used to write the document.
    //
    //   mode:
    //     One of the System.Data.XmlWriteMode values.
    //
    //   writeHierarchy:
    //     If true, write the contents of the current table and all its descendants.
    //     If false (the default value), write the data for the current table only.
    //public void WriteXml(XmlWriter writer, XmlWriteMode mode, bool writeHierarchy);
    //
    // Summary:
    //     Writes the current data structure of the System.Data.DataTable as an XML
    //     schema to the specified stream.
    //
    // Parameters:
    //   stream:
    //     The stream to which the XML schema will be written.
    //public void WriteXmlSchema(Stream stream);
    //
    // Summary:
    //     Writes the current data structure of the System.Data.DataTable as an XML
    //     schema to the specified file.
    //
    // Parameters:
    //   fileName:
    //     The name of the file to use.
    //public void WriteXmlSchema(string fileName);
    //
    // Summary:
    //     Writes the current data structure of the System.Data.DataTable as an XML
    //     schema using the specified System.IO.TextWriter.
    //
    // Parameters:
    //   writer:
    //     The System.IO.TextWriter with which to write.
    //public void WriteXmlSchema(TextWriter writer);
    //
    // Summary:
    //     Writes the current data structure of the System.Data.DataTable as an XML
    //     schema using the specified System.Xml.XmlWriter.
    //
    // Parameters:
    //   writer:
    //     The System.Xml.XmlWriter to use.
    //public void WriteXmlSchema(XmlWriter writer);
    //
    // Summary:
    //     Writes the current data structure of the System.Data.DataTable as an XML
    //     schema to the specified stream. To save the schema for the table and all
    //     its descendants, set the writeHierarchy parameter to true.
    //
    // Parameters:
    //   stream:
    //     The stream to which the XML schema will be written.
    //
    //   writeHierarchy:
    //     If true, write the schema of the current table and all its descendants. If
    //     false (the default value), write the schema for the current table only.
    //public void WriteXmlSchema(Stream stream, bool writeHierarchy);
    //
    // Summary:
    //     Writes the current data structure of the System.Data.DataTable as an XML
    //     schema to the specified file. To save the schema for the table and all its
    //     descendants, set the writeHierarchy parameter to true.
    //
    // Parameters:
    //   fileName:
    //     The name of the file to use.
    //
    //   writeHierarchy:
    //     If true, write the schema of the current table and all its descendants. If
    ////     false (the default value), write the schema for the current table only.
    //public void WriteXmlSchema(string fileName, bool writeHierarchy);
    //
    // Summary:
    //     Writes the current data structure of the System.Data.DataTable as an XML
    //     schema using the specified System.IO.TextWriter. To save the schema for the
    //     table and all its descendants, set the writeHierarchy parameter to true.
    //
    // Parameters:
    //   writer:
    //     The System.IO.TextWriter with which to write.
    //
    //   writeHierarchy:
    //     If true, write the schema of the current table and all its descendants. If
    //     false (the default value), write the schema for the current table only.
    //public void WriteXmlSchema(TextWriter writer, bool writeHierarchy);
    //
    // Summary:
    //     Writes the current data structure of the System.Data.DataTable as an XML
    //     schema using the specified System.Xml.XmlWriter. To save the schema for the
    //     table and all its descendants, set the writeHierarchy parameter to true.
    //
    // Parameters:
    //   writer:
    //     The System.Xml.XmlWriter used to write the document.
    //
    //   writeHierarchy:
    //     If true, write the schema of the current table and all its descendants. If
    //     false (the default value), write the schema for the current table only.
    //public void WriteXmlSchema(XmlWriter writer, bool writeHierarchy);
  }
}
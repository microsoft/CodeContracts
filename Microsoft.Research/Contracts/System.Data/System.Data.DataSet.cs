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
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Diagnostics.Contracts;

namespace System.Data
{
  // Summary:
  //     Gets the state of a System.Data.DataRow object.
  [Flags]
  public enum DataRowState
  {
    // Summary:
    //     The row has been created but is not part of any System.Data.DataRowCollection.
    //     A System.Data.DataRow is in this state immediately after it has been created
    //     and before it is added to a collection, or if it has been removed from a
    //     collection.
    Detached = 1,
    //
    // Summary:
    //     The row has not changed since System.Data.DataRow.AcceptChanges() was last
    //     called.
    Unchanged = 2,
    //
    // Summary:
    //     The row has been added to a System.Data.DataRowCollection, and System.Data.DataRow.AcceptChanges()
    //     has not been called.
    Added = 4,
    //
    // Summary:
    //     The row was deleted using the System.Data.DataRow.Delete() method of the
    //     System.Data.DataRow.
    Deleted = 8,
    //
    // Summary:
    //     The row has been modified and System.Data.DataRow.AcceptChanges() has not
    //     been called.
    Modified = 16,
  }

  // Summary:
  //     Represents an in-memory cache of data.
  // [Serializable]
  // [DefaultProperty("DataSetName")]
  // [ToolboxItem("Microsoft.VSDesigner.Data.VS.DataSetToolboxItem, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  // [XmlSchemaProvider("GetDataSetSchema")]
  // [Designer("Microsoft.VSDesigner.Data.VS.DataSetDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  // [XmlRoot("DataSet")]
  // [ResDescription("DataSetDescr")]
  public class DataSet // : MarshalByValueComponent, IListSource, IXmlSerializable, ISupportInitializeNotification, ISupportInitialize, ISerializable
  {
    // Summary:
    //     Initializes a new instance of the System.Data.DataSet class.
    //public DataSet();
    //
    // Summary:
    //     Initializes a new instance of a System.Data.DataSet class with the given
    //     name.
    //
    // Parameters:
    //   dataSetName:
    //     The name of the System.Data.DataSet.
    //public DataSet(string dataSetName);
    //
    //
    // Parameters:
    //   info:
    //     The data needed to serialize or deserialize an object.
    //
    //   context:
    //     The source and destination of a given serialized stream.
    //// protected DataSet(SerializationInfo info, StreamingContext context);
    //// protected DataSet(SerializationInfo info, StreamingContext context, bool ConstructSchema);

    // Summary:
    //     Gets or sets a value indicating whether string comparisons within System.Data.DataTable
    //     objects are case-sensitive.
    //
    // Returns:
    //     true if string comparisons are case-sensitive; otherwise false. The default
    //     is false.
    // [ResDescription("DataSetCaseSensitiveDescr")]
    // [ResCategory("DataCategory_Data")]
    // [DefaultValue(false)]
    //public bool CaseSensitive { get; set; }
    //
    // Summary:
    //     Gets or sets the name of the current System.Data.DataSet.
    //
    // Returns:
    //     The name of the System.Data.DataSet.
    // [ResDescription("DataSetDataSetNameDescr")]
    // [ResCategory("DataCategory_Data")]
    // [DefaultValue("")]
    public string DataSetName 
    { 
      get
    {
     Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }    
      set
      {
        Contract.Requires(value != null);
        Contract.Requires(value.Length > 0);

      }
    }
    
    //
    // Summary:
    //     Gets a custom view of the data contained in the System.Data.DataSet to allow
    //     filtering, searching, and navigating using a custom System.Data.DataViewManager.
    //
    // Returns:
    //     A System.Data.DataViewManager.
    // [Browsable(false)]
    // [ResDescription("DataSetDefaultViewDescr")]
    public DataViewManager DefaultViewManager 
    { 
      get
      {
        Contract.Ensures(Contract.Result<DataViewManager>() != null);

        return default(DataViewManager);
      }
    }
    //
    // Summary:
    //     Gets or sets a value indicating whether constraint rules are followed when
    //     attempting any update operation.
    //
    // Returns:
    //     true if rules are enforced; otherwise false. The default is true.
    //
    // Exceptions:
    //   System.Data.ConstraintException:
    //     One or more constraints cannot be enforced.
    // [ResDescription("DataSetEnforceConstraintsDescr")]
    // [DefaultValue(true)]
    //public bool EnforceConstraints { get; set; }
    //
    // Summary:
    //     Gets the collection of customized user information associated with the DataSet.
    //
    // Returns:
    //     A System.Data.PropertyCollection with all custom user information.
    // [ResDescription("ExtendedPropertiesDescr")]
    // [ResCategory("DataCategory_Data")]
    // [Browsable(false)]
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
    //     Gets a value indicating whether there are errors in any of the System.Data.DataTable
    //     objects within this System.Data.DataSet.
    //
    // Returns:
    //     true if any table contains an error;otherwise false.
    // [ResDescription("DataSetHasErrorsDescr")]
    // [Browsable(false)]
    //public bool HasErrors { get; }
    //
    // Summary:
    //     Gets a value that indicates whether the System.Data.DataSet is initialized.
    //
    // Returns:
    //     true to indicate the component has completed initialization; otherwise false.
    // [Browsable(false)]
    //public bool IsInitialized { get; }
    //
    // Summary:
    //     Gets or sets the locale information used to compare strings within the table.
    //
    // Returns:
    //     A System.Globalization.CultureInfo that contains data about the user's machine
    //     locale. The default is null.
    // [ResDescription("DataSetLocaleDescr")]
    // [ResCategory("DataCategory_Data")]
    //public CultureInfo Locale { get; set; }
    //
    // Summary:
    //     Gets or sets the namespace of the System.Data.DataSet.
    //
    // Returns:
    //     The namespace of the System.Data.DataSet.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The namespace already has data.
    // [ResDescription("DataSetNamespaceDescr")]
    // [ResCategory("DataCategory_Data")]
    // [DefaultValue("")]
    //public string Namespace { get; set; }
    //
    // Summary:
    //     Gets or sets an XML prefix that aliases the namespace of the System.Data.DataSet.
    //
    // Returns:
    //     The XML prefix for the System.Data.DataSet namespace.
    // [DefaultValue("")]
    // [ResCategory("DataCategory_Data")]
    // [ResDescription("DataSetPrefixDescr")]
    public string Prefix 
    { 
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      //set; 
    }
    
    //
    // Summary:
    //     Get the collection of relations that link tables and allow navigation from
    //     parent tables to child tables.
    //
    // Returns:
    //     A System.Data.DataRelationCollection that contains a collection of System.Data.DataRelation
    //     objects. An empty collection is returned if no System.Data.DataRelation objects
    //     exist.
    // [ResDescription("DataSetRelationsDescr")]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    // [ResCategory("DataCategory_Data")]
    public DataRelationCollection Relations 
    { 
      get
    {
        Contract.Ensures(Contract.Result<DataRelationCollection>() != null);

        return default(DataRelationCollection);
    }
    }
    
    
    //
    // Summary:
    //     Gets or sets a System.Data.SerializationFormat for the System.Data.DataSet
    //     used during remoting.
    //
    // Returns:
    //     A System.Data.SerializationFormat object.
    //public SerializationFormat RemotingFormat { get; set; }
    //
    // Summary:
    //     Gets or sets a System.Data.SchemaSerializationMode for a System.Data.DataSet.
    //
    // Returns:
    //     Gets or sets a System.Data.SchemaSerializationMode for a System.Data.DataSet.
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [Browsable(false)]
    //public virtual SchemaSerializationMode SchemaSerializationMode { get; set; }
    //
    // Summary:
    //     Gets or sets an System.ComponentModel.ISite for the System.Data.DataSet.
    //
    // Returns:
    //     An System.ComponentModel.ISite for the System.Data.DataSet.
    // [Browsable(false)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public override ISite Site { get; set; }
    //
    // Summary:
    //     Gets the collection of tables contained in the System.Data.DataSet.
    //
    // Returns:
    //     The System.Data.DataTableCollection contained by this System.Data.DataSet.
    //     An empty collection is returned if no System.Data.DataTable objects exist.
    // [ResDescription("DataSetTablesDescr")]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    // [ResCategory("DataCategory_Data")]
    public DataTableCollection Tables 
    { 
      get 
      {
        Contract.Ensures(Contract.Result<DataTableCollection >() != null);

        return default(DataTableCollection);
      }
    }

    // Summary:
    //     Occurs after the System.Data.DataSet is initialized.
    // [ResDescription("DataSetInitializedDescr")]
    // [ResCategory("DataCategory_Action")]
    //public event EventHandler Initialized;
    //
    // Summary:
    //     Occurs when a target and source System.Data.DataRow have the same primary
    //     key value, and System.Data.DataSet.EnforceConstraints is set to true.
    // [ResCategory("DataCategory_Action")]
    // [ResDescription("DataSetMergeFailedDescr")]
    //public event MergeFailedEventHandler MergeFailed;

    // Summary:
    //     Commits all the changes made to this System.Data.DataSet since it was loaded
    //     or since the last time System.Data.DataSet.AcceptChanges() was called.
    //public void AcceptChanges();
    //
    // Summary:
    //     Begins the initialization of a System.Data.DataSet that is used on a form
    //     or used by another component. The initialization occurs at run time.
    //public void BeginInit();
    //
    // Summary:
    //     Clears the System.Data.DataSet of any data by removing all rows in all tables.
    //public void Clear();
    //
    // Summary:
    //     Copies the structure of the System.Data.DataSet, including all System.Data.DataTable
    //     schemas, relations, and constraints. Does not copy any data.
    //
    // Returns:
    //     A new System.Data.DataSet with the same schema as the current System.Data.DataSet,
    //     but none of the data.
    public virtual DataSet Clone()
    {
      Contract.Ensures(Contract.Result<DataSet>() != null);

      return default(DataSet);
    }
    //
    // Summary:
    //     Copies both the structure and data for this System.Data.DataSet.
    //
    // Returns:
    //     A new System.Data.DataSet with the same structure (table schemas, relations,
    //     and constraints) and data as this System.Data.DataSet.  Note: If these classes
    //     have been subclassed , the copy will also be of the same subclasses.
    public DataSet Copy()
    {
      Contract.Ensures(Contract.Result<DataSet>() != null);

      return default(DataSet); 
    }
    //
    // Summary:
    //     Returns a System.Data.DataTableReader with one result set per System.Data.DataTable,
    //     in the same sequence as the tables appear in the System.Data.DataSet.Tables
    //     collection.
    //
    // Returns:
    //     A System.Data.DataTableReader containing one or more result sets, corresponding
    //     to the System.Data.DataTable instances contained within the source System.Data.DataSet.
    public DataTableReader CreateDataReader()
    {
      Contract.Ensures(Contract.Result<DataTableReader>() != null);

      return default(DataTableReader);
    }
    //
    // Summary:
    //     Returns a System.Data.DataTableReader with one result set per System.Data.DataTable.
    //
    // Parameters:
    //   dataTables:
    //     An array of DataTables providing the order of the result sets to be returned
    //     in the System.Data.DataTableReader.
    //
    // Returns:
    //     A System.Data.DataTableReader containing one or more result sets, corresponding
    //     to the System.Data.DataTable instances contained within the source System.Data.DataSet.
    //     The returned result sets are in the order specified by the dataTables parameter.
    public DataTableReader CreateDataReader(params DataTable [] dataTables)
    {
      Contract.Ensures(Contract.Result<DataTableReader>() != null);

      return default(DataTableReader);
    }
    //
    // Summary:
    //     Determines the System.Data.DataSet.SchemaSerializationMode for a System.Data.DataSet.
    //
    // Parameters:
    //   reader:
    //     The System.Xml.XmlReader instance that is passed during deserialization of
    //     the System.Data.DataSet.
    //
    // Returns:
    //     An System.Data.SchemaSerializationMode enumeration indicating whether schema
    //     information has been omitted from the payload.
    //// protected SchemaSerializationMode DetermineSchemaSerializationMode(XmlReader reader);
    //
    // Summary:
    //     Determines the System.Data.DataSet.SchemaSerializationMode for a System.Data.DataSet.
    //
    // Parameters:
    //   info:
    //     The System.Runtime.Serialization.SerializationInfo that a DataSet’s // protected
    //     constructor System.Data.DataSet.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)
    //     is invoked with during deserialization in remoting scenarios.
    //
    //   context:
    //     The System.Runtime.Serialization.StreamingContext that a DataSet’s // protected
    //     constructor System.Data.DataSet.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)
    //     is invoked with during deserialization in remoting scenarios.
    //
    // Returns:
    //     An System.Data.SchemaSerializationMode enumeration indicating whether schema
    //     information has been omitted from the payload.
    //// protected SchemaSerializationMode DetermineSchemaSerializationMode(SerializationInfo info, StreamingContext context);
    //
    // Summary:
    //     Ends the initialization of a System.Data.DataSet that is used on a form or
    //     used by another component. The initialization occurs at run time.
    //public void EndInit();
    //
    // Summary:
    //     Gets a copy of the System.Data.DataSet that contains all changes made to
    //     it since it was loaded or since System.Data.DataSet.AcceptChanges() was last
    //     called.
    //
    // Returns:
    //     A copy of the changes from this System.Data.DataSet that can have actions
    //     performed on it and later be merged back in using System.Data.DataSet.Merge(System.Data.DataSet).
    //     If no changed rows are found, the method returns null.
    [Pure]
    public DataSet GetChanges()
    {
      return default(DataSet);
    }
    //
    // Summary:
    //     Gets a copy of the System.Data.DataSet containing all changes made to it
    //     since it was last loaded, or since System.Data.DataSet.AcceptChanges() was
    //     called, filtered by System.Data.DataRowState.
    //
    // Parameters:
    //   rowStates:
    //     One of the System.Data.DataRowState values.
    //
    // Returns:
    //     A filtered copy of the System.Data.DataSet that can have actions performed
    //     on it, and subsequently be merged back in using System.Data.DataSet.Merge(System.Data.DataSet).
    //     If no rows of the desired System.Data.DataRowState are found, the method
    //     returns null.
    //public DataSet GetChanges(DataRowState rowStates);
    //
    public static XmlSchemaComplexType GetDataSetSchema(XmlSchemaSet schemaSet)
    {
      Contract.Ensures(Contract.Result<XmlSchemaComplexType>() != null);

      return default(XmlSchemaComplexType);
    }
    //
    // Summary:
    //     Populates a serialization information object with the data needed to serialize
    //     the System.Data.DataSet.
    //
    // Parameters:
    //   info:
    //     A System.Runtime.Serialization.SerializationInfo that holds the serialized
    //     data associated with the System.Data.DataSet.
    //
    //   context:
    //     A System.Runtime.Serialization.StreamingContext that contains the source
    //     and destination of the serialized stream associated with the System.Data.DataSet.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The info parameter is null.
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      Contract.Requires(info != null);

    }


    //// protected virtual XmlSchema GetSchemaSerializable();
    //// protected void GetSerializationData(SerializationInfo info, StreamingContext context);
    //
    // Summary:
    //     Returns the XML representation of the data stored in the System.Data.DataSet.
    //
    // Returns:
    //     A string that is a representation of the data stored in the System.Data.DataSet.
    public string GetXml()
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    //
    // Summary:
    //     Returns the XML Schema for the XML representation of the data stored in the
    //     System.Data.DataSet.
    //
    // Returns:
    //     String that is the XML Schema for the XML representation of the data stored
    //     in the System.Data.DataSet.
    public string GetXmlSchema()
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    //
    // Summary:
    //     Gets a value indicating whether the System.Data.DataSet has changes, including
    //     new, deleted, or modified rows.
    //
    // Returns:
    //     true if the System.Data.DataSet has changes; otherwise false.
    [Pure]
    public bool HasChanges()
    {
      return default(bool);
    }
    
    // Summary:
    //     Gets a value indicating whether the System.Data.DataSet has changes, including
    //     new, deleted, or modified rows, filtered by System.Data.DataRowState.
    //
    // Parameters:
    //   rowStates:
    //     One of the System.Data.DataRowState values.
    //
    // Returns:
    //     true if the System.Data.DataSet has changes; otherwise false.
    [Pure]
    public bool HasChanges(DataRowState rowStates)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Applies the XML schema from the specified System.IO.Stream to the System.Data.DataSet.
    //
    // Parameters:
    //   stream:
    //     The Stream from which to read the schema.
    //
    //   nsArray:
    //     An array of namespace Uniform Resource Identifier (URI) strings to be excluded
    //     from schema inference.
    //public void InferXmlSchema(Stream stream, string [] nsArray);
    //
    // Summary:
    //     Applies the XML schema from the specified file to the System.Data.DataSet.
    //
    // Parameters:
    //   fileName:
    //     The name of the file (including the path) from which to read the schema.
    //
    //   nsArray:
    //     An array of namespace Uniform Resource Identifier (URI) strings to be excluded
    //     from schema inference.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     System.Security.Permissions.FileIOPermission is not set to System.Security.Permissions.FileIOPermissionAccess.Read.
    //public void InferXmlSchema(string fileName, string [] nsArray);
    //
    // Summary:
    //     Applies the XML schema from the specified System.IO.TextReader to the System.Data.DataSet.
    //
    // Parameters:
    //   reader:
    //     The TextReader from which to read the schema.
    //
    //   nsArray:
    //     An array of namespace Uniform Resource Identifier (URI) strings to be excluded
    //     from schema inference.
    //public void InferXmlSchema(TextReader reader, string [] nsArray);
    //
    // Summary:
    //     Applies the XML schema from the specified System.Xml.XmlReader to the System.Data.DataSet.
    //
    // Parameters:
    //   reader:
    //     The XMLReader from which to read the schema.
    //
    //   nsArray:
    //     An array of namespace Uniform Resource Identifier (URI) strings to be excluded
    //     from schema inference.
    //public void InferXmlSchema(XmlReader reader, string [] nsArray);
    //// protected virtual void InitializeDerivedDataSet();
    //
    // Summary:
    //     Inspects the format of the serialized representation of the DataSet.
    //
    // Parameters:
    //   info:
    //     The System.Runtime.Serialization.SerializationInfo object.
    //
    //   context:
    //     The System.Runtime.Serialization.StreamingContext object.
    //
    // Returns:
    //     true if the specified System.Runtime.Serialization.SerializationInfo represents
    //     a DataSet serialized in its binary format, false otherwise.
    //// protected bool IsBinarySerialized(SerializationInfo info, StreamingContext context);
    //
    // Summary:
    //     Fills a System.Data.DataSet with values from a data source using the supplied
    //     System.Data.IDataReader, using an array of System.Data.DataTable instances
    //     to supply the schema and namespace information.
    //
    // Parameters:
    //   reader:
    //     An System.Data.IDataReader that provides one or more result sets.
    //
    //   loadOption:
    //     A value from the System.Data.LoadOption enumeration that indicates how rows
    //     already in the System.Data.DataTable instances within the System.Data.DataSet
    //     will be combined with incoming rows that share the same primary key.
    //
    //   tables:
    //     An array of System.Data.DataTable instances, from which the System.Data.DataSet.Load(System.Data.IDataReader,System.Data.LoadOption,System.Data.DataTable// [])
    //     method retrieves name and namespace information. Each of these tables must
    //     be a member of the System.Data.DataTableCollection contained by this System.Data.DataSet.
    //public void Load(IDataReader reader, LoadOption loadOption, params DataTable [] tables);
    //
    // Summary:
    //     Fills a System.Data.DataSet with values from a data source using the supplied
    //     System.Data.IDataReader, using an array of strings to supply the names for
    //     the tables within the DataSet.
    //
    // Parameters:
    //   reader:
    //     An System.Data.IDataReader that provides one or more result sets.
    //
    //   loadOption:
    //     A value from the System.Data.LoadOption enumeration that indicates how rows
    //     already in the System.Data.DataTable instances within the DataSet will be
    //     combined with incoming rows that share the same primary key.
    //
    //   tables:
    //     An array of strings, from which the Load method retrieves table name information.
    //public void Load(IDataReader reader, LoadOption loadOption, params string [] tables);
    //
    // Summary:
    //     Fills a System.Data.DataSet with values from a data source using the supplied
    //     System.Data.IDataReader, using an array of System.Data.DataTable instances
    //     to supply the schema and namespace information.
    //
    // Parameters:
    //   reader:
    //     An System.Data.IDataReader that provides one or more result sets.
    //
    //   loadOption:
    //     A value from the System.Data.LoadOption enumeration that indicates how rows
    //     already in the System.Data.DataTable instances within the System.Data.DataSet
    //     will be combined with incoming rows that share the same primary key.
    //
    //   errorHandler:
    //     A System.Data.FillErrorEventHandler delegate to call when an error occurs
    //     while loading data.
    //
    //   tables:
    //     An array of System.Data.DataTable instances, from which the System.Data.DataSet.Load(System.Data.IDataReader,System.Data.LoadOption,System.Data.FillErrorEventHandler,System.Data.DataTable// [])
    //     method retrieves name and namespace information.
    //public virtual void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler errorHandler, params DataTable// [] tables);
    //
    // Summary:
    //     Merges an array of System.Data.DataRow objects into the current System.Data.DataSet.
    //
    // Parameters:
    //   rows:
    //     The array of DataRow objects to be merged into the DataSet.
    public void Merge(DataRow [] rows)
    {
      Contract.Requires(rows != null);

    }
    //
    // Summary:
    //     Merges a specified System.Data.DataSet and its schema into the current DataSet.
    //
    // Parameters:
    //   dataSet:
    //     The DataSet whose data and schema will be merged.
    //
    // Exceptions:
    //   System.Data.ConstraintException:
    //     One or more constraints cannot be enabled.
    //
    //   System.ArgumentNullException:
    //     The dataSet is null.
    public void Merge(DataSet dataSet)
    {
      Contract.Requires(dataSet != null);

    }
    //
    // Summary:
    //     Merges a specified System.Data.DataTable and its schema into the current
    //     System.Data.DataSet.
    //
    // Parameters:
    //   table:
    //     The System.Data.DataTable whose data and schema will be merged.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The dataSet is null.
    public void Merge(DataTable table)
    {
      Contract.Requires(table != null);

    }
    //
    // Summary:
    //     Merges a specified System.Data.DataSet and its schema into the current DataSet,
    //     preserving or discarding any changes in this DataSet according to the given
    //     argument.
    //
    // Parameters:
    //   dataSet: 
    //     The DataSet whose data and schema will be merged.
    //
    //   preserveChanges:
    //     true to preserve changes in the current DataSet; otherwise false.
    public void Merge(DataSet dataSet, bool preserveChanges)
    {
      Contract.Requires(dataSet != null);

    }
    //
    // Summary:
    //     Merges an array of System.Data.DataRow objects into the current System.Data.DataSet,
    //     preserving or discarding changes in the DataSet and handling an incompatible
    //     schema according to the given arguments.
    //
    // Parameters:
    //   rows:
    //     The array of System.Data.DataRow objects to be merged into the DataSet.
    //
    //   preserveChanges:
    //     true to preserve changes in the DataSet; otherwise false.
    //
    //   missingSchemaAction:
    //     One of the System.Data.MissingSchemaAction values.
    //public void Merge(DataRow[] rows, bool preserveChanges, MissingSchemaAction missingSchemaAction);
    //
    // Summary:
    //     Merges a specified System.Data.DataSet and its schema with the current DataSet,
    //     preserving or discarding changes in the current DataSet and handling an incompatible
    //     schema according to the given arguments.
    //
    // Parameters:
    //   dataSet:
    //     The DataSet whose data and schema will be merged.
    //
    //   preserveChanges:
    //     true to preserve changes in the current DataSet; otherwise false.
    //
    //   missingSchemaAction:
    //     One of the System.Data.MissingSchemaAction values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The dataSet is null.
    //public void Merge(DataSet dataSet, bool preserveChanges, MissingSchemaAction missingSchemaAction);
    //
    // Summary:
    //     Merges a specified System.Data.DataTable and its schema into the current
    //     DataSet, preserving or discarding changes in the DataSet and handling an
    //     incompatible schema according to the given arguments.
    //
    // Parameters:
    //   table:
    //     The DataTable whose data and schema will be merged.
    //
    //   preserveChanges:
    //     One of the System.Data.MissingSchemaAction values.
    //
    //   missingSchemaAction:
    //     true to preserve changes in the DataSet; otherwise false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The dataSet is null.
    //public void Merge(DataTable table, bool preserveChanges, MissingSchemaAction missingSchemaAction);
    //
    // Summary:
    //     Raises the System.Data.DataSet.OnPropertyChanging(System.ComponentModel.PropertyChangedEventArgs)
    //     event.
    //
    // Parameters:
    //   pcevent:
    //     A System.ComponentModel.PropertyChangedEventArgs that contains the event
    //     data.
    // protected virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent);
    //
    // Summary:
    //     Occurs when a System.Data.DataRelation object is removed from a System.Data.DataTable.
    //
    // Parameters:
    //   relation:
    //     The System.Data.DataRelation being removed.
    // protected virtual void OnRemoveRelation(DataRelation relation);
    //
    // Summary:
    //     Occurs when a System.Data.DataTable is removed from a System.Data.DataSet.
    //
    // Parameters:
    //   table:
    //     The System.Data.DataTable being removed.
    // protected internal virtual void OnRemoveTable(DataTable table);
    //
    // Summary:
    //     Sends a notification that the specified System.Data.DataSet property is about
    //     to change.
    //
    // Parameters:
    //   name:
    //     The name of the property that is about to change.
    // protected internal void RaisePropertyChanging(string name);
    //
    // Summary:
    //     Reads XML schema and data into the System.Data.DataSet using the specified
    //     System.IO.Stream.
    //
    // Parameters:
    //   stream:
    //     An object that derives from System.IO.Stream.
    //
    // Returns:
    //     The System.Data.XmlReadMode used to read the data.
    //public XmlReadMode ReadXml(Stream stream);
    //
    // Summary:
    //     Reads XML schema and data into the System.Data.DataSet using the specified
    //     file.
    //
    // Parameters:
    //   fileName:
    //     The filename (including the path) from which to read.
    //
    // Returns:
    //     The XmlReadMode used to read the data.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     System.Security.Permissions.FileIOPermission is not set to System.Security.Permissions.FileIOPermissionAccess.Read.
    //public XmlReadMode ReadXml(string fileName);
    //
    // Summary:
    //     Reads XML schema and data into the System.Data.DataSet using the specified
    //     System.IO.TextReader.
    //
    // Parameters:
    //   reader:
    //     The TextReader from which to read the schema and data.
    //
    // Returns:
    //     The System.Data.XmlReadMode used to read the data.
    //public XmlReadMode ReadXml(TextReader reader);
    //
    // Summary:
    //     Reads XML schema and data into the System.Data.DataSet using the specified
    //     System.Xml.XmlReader.
    //
    // Parameters:
    //   reader:
    //     The System.Xml.XmlReader from which to read.
    //
    // Returns:
    //     The XmlReadMode used to read the data.
    //public XmlReadMode ReadXml(XmlReader reader);
    //
    // Summary:
    //     Reads XML schema and data into the System.Data.DataSet using the specified
    //     System.IO.Stream and System.Data.XmlReadMode.
    //
    // Parameters:
    //   stream:
    //     The System.IO.Stream from which to read.
    //
    //   mode:
    //     One of the System.Data.XmlReadMode values.
    //
    // Returns:
    //     The XmlReadMode used to read the data.
    //public XmlReadMode ReadXml(Stream stream, XmlReadMode mode);
    //
    // Summary:
    //     Reads XML schema and data into the System.Data.DataSet using the specified
    //     file and System.Data.XmlReadMode.
    //
    // Parameters:
    //   fileName:
    //     The filename (including the path) from which to read.
    //
    //   mode:
    //     One of the System.Data.XmlReadMode values.
    //
    // Returns:
    //     The XmlReadMode used to read the data.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     System.Security.Permissions.FileIOPermission is not set to System.Security.Permissions.FileIOPermissionAccess.Read.
    //public XmlReadMode ReadXml(string fileName, XmlReadMode mode);
    //
    // Summary:
    //     Reads XML schema and data into the System.Data.DataSet using the specified
    //     System.IO.TextReader and System.Data.XmlReadMode.
    //
    // Parameters:
    //   reader:
    //     The System.IO.TextReader from which to read.
    //
    //   mode:
    //     One of the System.Data.XmlReadMode values.
    //
    // Returns:
    //     The XmlReadMode used to read the data.
    //public XmlReadMode ReadXml(TextReader reader, XmlReadMode mode);
    //
    // Summary:
    //     Reads XML schema and data into the System.Data.DataSet using the specified
    //     System.Xml.XmlReader and System.Data.XmlReadMode.
    //
    // Parameters:
    //   reader:
    //     The System.Xml.XmlReader from which to read.
    //
    //   mode:
    //     One of the System.Data.XmlReadMode values.
    //
    // Returns:
    //     The XmlReadMode used to read the data.
    //public XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode);
    //
    // Summary:
    //     Reads the XML schema from the specified System.IO.Stream into the System.Data.DataSet.
    //
    // Parameters:
    //   stream:
    //     The System.IO.Stream from which to read.
    //public void ReadXmlSchema(Stream stream);
    //
    // Summary:
    //     Reads the XML schema from the specified file into the System.Data.DataSet.
    //
    // Parameters:
    //   fileName:
    //     The file name (including the path) from which to read.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     System.Security.Permissions.FileIOPermission is not set to System.Security.Permissions.FileIOPermissionAccess.Read.
    //public void ReadXmlSchema(string fileName);
    //
    // Summary:
    //     Reads the XML schema from the specified System.IO.TextReader into the System.Data.DataSet.
    //
    // Parameters:
    //   reader:
    //     The System.IO.TextReader from which to read.
    //public void ReadXmlSchema(TextReader reader);
    //
    // Summary:
    //     Reads the XML schema from the specified System.Xml.XmlReader into the System.Data.DataSet.
    //
    // Parameters:
    //   reader:
    //     The System.Xml.XmlReader from which to read.
    //public void ReadXmlSchema(XmlReader reader);
    // protected virtual void ReadXmlSerializable(XmlReader reader);
    //
    // Summary:
    //     Rolls back all the changes made to the System.Data.DataSet since it was created,
    //     or since the last time System.Data.DataSet.AcceptChanges() was called.
    //public virtual void RejectChanges();
    //
    // Summary:
    //     Resets the System.Data.DataSet to its original state. Subclasses should override
    //     System.Data.DataSet.Reset() to restore a System.Data.DataSet to its original
    //     state.
    //public virtual void Reset();
    //
    // Summary:
    //     Gets a value indicating whether System.Data.DataSet.Relations property should
    //     be persisted.
    //
    // Returns:
    //     true if the property value has been changed from its default; otherwise false.
    // protected virtual bool ShouldSerializeRelations();
    //
    // Summary:
    //     Gets a value indicating whether System.Data.DataSet.Tables property should
    //     be persisted.
    //
    // Returns:
    //     true if the property value has been changed from its default; otherwise false.
    // protected virtual bool ShouldSerializeTables();
    //
    // Summary:
    //     Writes the current data for the System.Data.DataSet using the specified System.IO.Stream.
    //
    // Parameters:
    //   stream:
    //     A System.IO.Stream object used to write to a file.
    //public void WriteXml(Stream stream);
    //
    // Summary:
    //     Writes the current data for the System.Data.DataSet to the specified file.
    //
    // Parameters:
    //   fileName:
    //     The file name (including the path) to which to write.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     System.Security.Permissions.FileIOPermission is not set to System.Security.Permissions.FileIOPermissionAccess.Write.
    //public void WriteXml(string fileName);
    //
    // Summary:
    //     Writes the current data for the System.Data.DataSet using the specified System.IO.TextWriter.
    //
    // Parameters:
    //   writer:
    //     The System.IO.TextWriter object with which to write.
    //public void WriteXml(TextWriter writer);
    //
    // Summary:
    //     Writes the current data for the System.Data.DataSet to the specified System.Xml.XmlWriter.
    //
    // Parameters:
    //   writer:
    //     The System.Xml.XmlWriter with which to write.
    //public void WriteXml(XmlWriter writer);
    //
    // Summary:
    //     Writes the current data, and optionally the schema, for the System.Data.DataSet
    //     using the specified System.IO.Stream and System.Data.XmlWriteMode. To write
    //     the schema, set the value for the mode parameter to WriteSchema.
    //
    // Parameters:
    //   stream:
    //     A System.IO.Stream object used to write to a file.
    //
    //   mode:
    //     One of the System.Data.XmlWriteMode values.
    //public void WriteXml(Stream stream, XmlWriteMode mode);
    //
    // Summary:
    //     Writes the current data, and optionally the schema, for the System.Data.DataSet
    //     to the specified file using the specified System.Data.XmlWriteMode. To write
    //     the schema, set the value for the mode parameter to WriteSchema.
    //
    // Parameters:
    //   fileName:
    //     The file name (including the path) to which to write.
    //
    //   mode:
    //     One of the System.Data.XmlWriteMode values.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     System.Security.Permissions.FileIOPermission is not set to System.Security.Permissions.FileIOPermissionAccess.Write.
    //public void WriteXml(string fileName, XmlWriteMode mode);
    //
    // Summary:
    //     Writes the current data, and optionally the schema, for the System.Data.DataSet
    //     using the specified System.IO.TextWriter and System.Data.XmlWriteMode. To
    //     write the schema, set the value for the mode parameter to WriteSchema.
    //
    // Parameters:
    //   writer:
    //     A System.IO.TextWriter object used to write the document.
    //
    //   mode:
    //     One of the System.Data.XmlWriteMode values.
    //public void WriteXml(TextWriter writer, XmlWriteMode mode);
    //
    // Summary:
    //     Writes the current data, and optionally the schema, for the System.Data.DataSet
    //     using the specified System.Xml.XmlWriter and System.Data.XmlWriteMode. To
    //     write the schema, set the value for the mode parameter to WriteSchema.
    //
    // Parameters:
    //   writer:
    //     The System.Xml.XmlWriter with which to write.
    //
    //   mode:
    //     One of the System.Data.XmlWriteMode values.
    //public void WriteXml(XmlWriter writer, XmlWriteMode mode);
    //
    // Summary:
    //     Writes the System.Data.DataSet structure as an XML schema to using the specified
    //     System.IO.Stream object.
    //
    // Parameters:
    //   stream:
    //     A System.IO.Stream object used to write to a file.
    //public void WriteXmlSchema(Stream stream);
    //
    // Summary:
    //     Writes the System.Data.DataSet structure as an XML schema to a file.
    //
    // Parameters:
    //   fileName:
    //     The file name (including the path) to which to write.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     System.Security.Permissions.FileIOPermission is not set to System.Security.Permissions.FileIOPermissionAccess.Write.
    //public void WriteXmlSchema(string fileName);
    //
    // Summary:
    //     Writes the System.Data.DataSet structure as an XML schema to a System.IO.TextWriter
    //     object.
    //
    // Parameters:
    //   writer:
    //     The System.IO.TextWriter object with which to write.
    //public void WriteXmlSchema(TextWriter writer);
    //
    // Summary:
    //     Writes the System.Data.DataSet structure as an XML schema to an System.Xml.XmlWriter
    //     object.
    //
    // Parameters:
    //   writer:
    //     The System.Xml.XmlWriter with which to write.
    //public void WriteXmlSchema(XmlWriter writer);
  }
}

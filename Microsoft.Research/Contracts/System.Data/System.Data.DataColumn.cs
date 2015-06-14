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
using System.Diagnostics.Contracts;


namespace System.Data
{
  // Summary:
  //     Represents the schema of a column in a System.Data.DataTable.
  //[ToolboxItem(false)]
  //[Editor("Microsoft.VSDesigner.Data.Design.DataColumnEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  //[DefaultProperty("ColumnName")]
  //[DesignTimeVisible(false)]
  public class DataColumn //: MarshalByValueComponent
  {
    // Summary:
    //     Initializes a new instance of a System.Data.DataColumn class as type string.
    //public DataColumn();
    //
    // Summary:
    //     Inititalizes a new instance of the System.Data.DataColumn class, as type
    //     string, using the specified column name.
    //
    // Parameters:
    //   columnName:
    //     A string that represents the name of the column to be created. If set to
    //     null or an empty string (""), a default name will be specified when added
    //     to the columns collection.
    //public DataColumn(string columnName);
    //
    // Summary:
    //     Inititalizes a new instance of the System.Data.DataColumn class using the
    //     specified column name and data type.
    //
    // Parameters:
    //   columnName:
    //     A string that represents the name of the column to be created. If set to
    //     null or an empty string (""), a default name will be specified when added
    //     to the columns collection.
    //
    //   dataType:
    //     A supported System.Data.DataColumn.DataType.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     No dataType was specified.
    public DataColumn(string columnName, Type dataType)
    {
      Contract.Requires(dataType != null);
    }

    //
    // Summary:
    //     Initializes a new instance of the System.Data.DataColumn class using the
    //     specified name, data type, and expression.
    //
    // Parameters:
    //   columnName:
    //     A string that represents the name of the column to be created. If set to
    //     null or an empty string (""), a default name will be specified when added
    //     to the columns collection.
    //
    //   dataType:
    //     A supported System.Data.DataColumn.DataType.
    //
    //   expr:
    //     The expression used to create this column. For more information, see the
    //     System.Data.DataColumn.Expression property.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     No dataType was specified.
    public DataColumn(string columnName, Type dataType, string expr)
    {
      Contract.Requires(dataType != null);
    }

    //
    // Summary:
    //     Initializes a new instance of the System.Data.DataColumn class using the
    //     specified name, data type, expression, and value that determines whether
    //     the column is an attribute.
    //
    // Parameters:
    //   columnName:
    //     A string that represents the name of the column to be created. If set to
    //     null or an empty string (""), a default name will be specified when added
    //     to the columns collection.
    //
    //   dataType:
    //     A supported System.Data.DataColumn.DataType.
    //
    //   expr:
    //     The expression used to create this column. For more information, see the
    //     System.Data.DataColumn.Expression property.
    //
    //   type:
    //     One of the System.Data.MappingType values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     No dataType was specified.
    public DataColumn(string columnName, Type dataType, string expr, MappingType type)
    {
      Contract.Requires(dataType != null);
    }


    // Summary:
    //     Gets or sets a value that indicates whether null values are allowed in this
    //     column for rows that belong to the table.
    //
    // Returns:
    //     true if null values values are allowed; otherwise, false. The default is
    //     true.
    //[ResDescription("DataColumnAllowNullDescr")]
    //[ResCategory("DataCategory_Data")]
    //[DefaultValue(true)]
    //public bool AllowDBNull { get; set; }
    //
    // Summary:
    //     Gets or sets a value that indicates whether the column automatically increments
    //     the value of the column for new rows added to the table.
    //
    // Returns:
    //     true if the value of the column increments automatically; otherwise, false.
    //     The default is false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The column is a computed column.
    //[RefreshProperties(RefreshProperties.All)]
    //[ResCategory("DataCategory_Data")]
    //[DefaultValue(false)]
    //[ResDescription("DataColumnAutoIncrementDescr")]
    //public bool AutoIncrement { get; set; }
    //
    // Summary:
    //     Gets or sets the starting value for a column that has its System.Data.DataColumn.AutoIncrement
    //     property set to true.
    //
    // Returns:
    //     The starting value for the System.Data.DataColumn.AutoIncrement feature.
    //[ResDescription("DataColumnAutoIncrementSeedDescr")]
    //[DefaultValue(0)]
    //[ResCategory("DataCategory_Data")]
    //public long AutoIncrementSeed { get; set; }
    //
    // Summary:
    //     Gets or sets the increment used by a column with its System.Data.DataColumn.AutoIncrement
    //     property set to true.
    //
    // Returns:
    //     The number by which the value of the column is automatically incremented.
    //     The default is 1.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value set is zero.
    //[ResDescription("DataColumnAutoIncrementStepDescr")]
    //[ResCategory("DataCategory_Data")]
    //[DefaultValue(1)]
    //public long AutoIncrementStep { get; set; }
    //
    // Summary:
    //     Gets or sets the caption for the column.
    //
    // Returns:
    //     The caption of the column. If not set, returns the System.Data.DataColumn.ColumnName
    //     value.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DataColumnCaptionDescr")]
    //public string Caption { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Data.MappingType of the column.
    //
    // Returns:
    //     One of the System.Data.MappingType values.
    //[ResDescription("DataColumnMappingDescr")]
    //public virtual MappingType ColumnMapping { get; set; }
    //
    // Summary:
    //     Gets or sets the name of the column in the System.Data.DataColumnCollection.
    //
    // Returns:
    //     The name of the column.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The property is set to null or an empty string and the column belongs to
    //     a collection.
    //
    //   System.Data.DuplicateNameException:
    //     A column with the same name already exists in the collection. The name comparison
    //     is not case sensitive.
    //[ResDescription("DataColumnColumnNameDescr")]
    //[RefreshProperties(RefreshProperties.All)]
    //[DefaultValue("")]
    //[ResCategory("DataCategory_Data")]
    //public string ColumnName { get; set; }
    //
    // Summary:
    //     Gets or sets the type of data stored in the column.
    //
    // Returns:
    //     A System.Type object that represents the column data type.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The column already has data stored. - or -System.Data.DataColumn.AutoIncrement
    //     is true, but the value is set to a type a unsupported by System.Data.DataColumn.AutoIncrement.
    //[ResCategory("DataCategory_Data")]
    //[RefreshProperties(RefreshProperties.All)]
    //[TypeConverter(typeof(ColumnTypeConverter))]
    //[ResDescription("DataColumnDataTypeDescr")]
    //public Type DataType { get; set; }
    //
    // Summary:
    //     Gets or sets the DateTimeMode for the column.
    //
    // Returns:
    //     The System.Data.DataSetDateTime for the specified column.
    //[ResDescription("DataColumnDateTimeModeDescr")]
    //[ResCategory("DataCategory_Data")]
    //[RefreshProperties(RefreshProperties.All)]
    //public DataSetDateTime DateTimeMode { get; set; }
    //
    // Summary:
    //     Gets or sets the default value for the column when you are creating new rows.
    //
    // Returns:
    //     A value appropriate to the column's System.Data.DataColumn.DataType.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     When you are adding a row, the default value is not an instance of the column's
    //     data type.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DataColumnDefaultValueDescr")]
    //[TypeConverter(typeof(DefaultValueTypeConverter))]
    //public object DefaultValue { get; set; }
    //
    // Summary:
    //     Gets or sets the expression used to filter rows, calculate the values in
    //     a column, or create an aggregate column.
    //
    // Returns:
    //     An expression to calculate the value of a column, or create an aggregate
    //     column. The return type of an expression is determined by the System.Data.DataColumn.DataType
    //     of the column.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Data.DataColumn.AutoIncrement or System.Data.DataColumn.Unique
    //     property is set to true.
    //
    //   System.FormatException:
    //     When you are using the CONVERT function, the expression evaluates to a string,
    //     but the string does not contain a representation that can be converted to
    //     the type parameter.
    //
    //   System.InvalidCastException:
    //     When you are using the CONVERT function, the requested cast is not possible.
    //     See the Conversion function in the following section for detailed information
    //     about possible casts.
    //
    //   System.ArgumentOutOfRangeException:
    //     When you use the SUBSTRING function, the start argument is out of range.-Or-
    //     When you use the SUBSTRING function, the length argument is out of range.
    //
    //   System.Exception:
    //     When you use the LEN function or the TRIM function, the expression does not
    //     evaluate to a string. This includes expressions that evaluate to System.Char.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DataColumnExpressionDescr")]
    //[RefreshProperties(RefreshProperties.All)]
    //[DefaultValue("")]
    //public string Expression { get; set; }
    //
    // Summary:
    //     Gets the collection of custom user information associated with a System.Data.DataColumn.
    //
    // Returns:
    //     A System.Data.PropertyCollection of custom information.
    //[ResDescription("ExtendedPropertiesDescr")]
    //[ResCategory("DataCategory_Data")]
    //[Browsable(false)]
    //public PropertyCollection ExtendedProperties { get; }
    //
    // Summary:
    //     Gets or sets the maximum length of a text column.
    //
    // Returns:
    //     The maximum length of the column in characters. If the column has no maximum
    //     length, the value is –1 (default).
    //[ResDescription("DataColumnMaxLengthDescr")]
    //[ResCategory("DataCategory_Data")]
    //[DefaultValue(-1)]
    public int MaxLength 
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= -1);

        return default(int);
      }
      //set 
    }
    
    
    //
    // Summary:
    //     Gets or sets the namespace of the System.Data.DataColumn.
    //
    // Returns:
    //     The namespace of the System.Data.DataColumn.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The namespace already has data.
    //[ResDescription("DataColumnNamespaceDescr")]
    //[ResCategory("DataCategory_Data")]
    //public string Namespace { get; set; }
    //
    // Summary:
    //     Gets the position of the column in the System.Data.DataColumnCollection collection.
    //
    // Returns:
    //     The position of the column. Gets -1 if the column is not a member of a collection.
    //[Browsable(false)]
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DataColumnOrdinalDescr")]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Ordinal 
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0 - 1);

        return default(int);
      }
    }

    //
    // Summary:
    //     Gets or sets an XML prefix that aliases the namespace of the System.Data.DataTable.
    //
    // Returns:
    //     The XML prefix for the System.Data.DataTable namespace.
    //[ResDescription("DataColumnPrefixDescr")]
    //[ResCategory("DataCategory_Data")]
    //[DefaultValue("")]
    //public string Prefix { get; set; }
    //
    // Summary:
    //     Gets or sets a value that indicates whether the column allows for changes
    //     as soon as a row has been added to the table.
    //
    // Returns:
    //     true if the column is read only; otherwise, false. The default is false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The property is set to false on a computed column.
    //[ResDescription("DataColumnReadOnlyDescr")]
    //[ResCategory("DataCategory_Data")]
    //[DefaultValue(false)]
    //public bool ReadOnly { get; set; }
    //
    // Summary:
    //     Gets the System.Data.DataTable to which the column belongs to.
    //
    // Returns:
    //     The System.Data.DataTable that the System.Data.DataColumn belongs to.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DataColumnDataTableDescr")]
    //[Browsable(false)]
    //public DataTable Table { get; }
    //
    // Summary:
    //     Gets or sets a value that indicates whether the values in each row of the
    //     column must be unique.
    //
    // Returns:
    //     true if the value must be unique; otherwise, false. The default is false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The column is a calculated column.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[ResDescription("DataColumnUniqueDescr")]
    //[ResCategory("DataCategory_Data")]
    //[DefaultValue(false)]
    //public bool Unique { get; set; }

    // Summary:
    //     This member supports the .NET Framework infrastructure and is not intended
    //     to be used directly from your code.
    //protected internal void CheckNotAllowNull();
    //
    // Summary:
    //     This member supports the .NET Framework infrastructure and is not intended
    //     to be used directly from your code.
    //protected void CheckUnique();
    //
    // Summary:
    //     This member supports the .NET Framework infrastructure and is not intended
    //     to be used directly from your code.
    //
    // Parameters:
    //   pcevent:
    //     Parameter reference.
    //protected virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent);
    //
    // Summary:
    //     This member supports the .NET Framework infrastructure and is not intended
    //     to be used directly from your code.
    //
    // Parameters:
    //   name:
    //     Parameter reference.
    //protected internal void RaisePropertyChanging(string name);
    //
    // Summary:
    //     Changes the ordinal or position of the System.Data.DataColumn to the specified
    //     ordinal or position.
    //
    // Parameters:
    //   ordinal:
    //     The specified ordinal.
    public void SetOrdinal(int ordinal)
    {
      Contract.Requires(ordinal >= 0);
    }
    //
    // Summary:
    //     Gets the System.Data.DataColumn.Expression of the column, if one exists.
    //
    // Returns:
    //     The System.Data.DataColumn.Expression value, if the property is set; otherwise,
    //     the System.Data.DataColumn.ColumnName property.
    //public override string ToString();
  }
}

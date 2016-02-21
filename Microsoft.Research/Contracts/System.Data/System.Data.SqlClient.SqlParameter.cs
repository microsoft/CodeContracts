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
using System.Data.SqlTypes;
using System.Globalization;
using System.Diagnostics.Contracts;

namespace System.Data.SqlClient
{
  // Summary:
  //     Represents a parameter to a System.Data.SqlClient.SqlCommand and optionally
  //     its mapping to System.Data.DataSet columns. This class cannot be inherited.
  //[TypeConverter(typeof(SqlParameter.SqlParameterConverter))]
  public /*sealed*/ class SqlParameter //: DbParameter, IDbDataParameter, IDataParameter, ICloneable
  {
    // Summary:
    //     Initializes a new instance of the System.Data.SqlClient.SqlParameter class.
    public SqlParameter()
	{
		
	}
    //
    // Summary:
    //     Initializes a new instance of the System.Data.SqlClient.SqlParameter class
    //     that uses the parameter name and a value of the new System.Data.SqlClient.SqlParameter.
    //
    // Parameters:
    //   parameterName:
    //     The name of the parameter to map.
    //
    //   value:
    //     An System.Object that is the value of the System.Data.SqlClient.SqlParameter.
    public SqlParameter(string parameterName, object value)
	{
		
	}
    //
    // Summary:
    //     Initializes a new instance of the System.Data.SqlClient.SqlParameter class
    //     that uses the parameter name and the data type.
    //
    // Parameters:
    //   parameterName:
    //     The name of the parameter to map.
    //
    //   dbType:
    //     One of the System.Data.SqlDbType values.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value supplied in the dbType parameter is an invalid back-end data type.
    public SqlParameter(string parameterName, SqlDbType dbType)
	{
		
	}
    //
    // Summary:
    //     Initializes a new instance of the System.Data.SqlClient.SqlParameter class
    //     that uses the parameter name, the System.Data.SqlDbType, and the size.
    //
    // Parameters:
    //   parameterName:
    //     The name of the parameter to map.
    //
    //   dbType:
    //     One of the System.Data.SqlDbType values.
    //
    //   size:
    //     The length of the parameter.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value supplied in the dbType parameter is an invalid back-end data type.
    public SqlParameter(string parameterName, SqlDbType dbType, int size)
	{
		
	}
    //
    // Summary:
    //     Initializes a new instance of the System.Data.SqlClient.SqlParameter class
    //     that uses the parameter name, the System.Data.SqlDbType, the size, and the
    //     source column name.
    //
    // Parameters:
    //   parameterName:
    //     The name of the parameter to map.
    //
    //   dbType:
    //     One of the System.Data.SqlDbType values.
    //
    //   size:
    //     The length of the parameter.
    //
    //   sourceColumn:
    //     The name of the source column.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value supplied in the dbType parameter is an invalid back-end data type.
    public SqlParameter(string parameterName, SqlDbType dbType, int size, string sourceColumn)
	{
		
	}
    //
    // Summary:
    //     Initializes a new instance of the System.Data.SqlClient.SqlParameter class
    //     that uses the parameter name, the type of the parameter, the size of the
    //     parameter, a System.Data.ParameterDirection, the precision of the parameter,
    //     the scale of the parameter, the source column, a System.Data.DataRowVersion
    //     to use, and the value of the parameter.
    //
    // Parameters:
    //   parameterName:
    //     The name of the parameter to map.
    //
    //   dbType:
    //     One of the System.Data.SqlDbType values.
    //
    //   size:
    //     The length of the parameter.
    //
    //   direction:
    //     One of the System.Data.ParameterDirection values.
    //
    //   isNullable:
    //     true if the value of the field can be null; otherwise false.
    //
    //   precision:
    //     The total number of digits to the left and right of the decimal point to
    //     which System.Data.SqlClient.SqlParameter.Value is resolved.
    //
    //   scale:
    //     The total number of decimal places to which System.Data.SqlClient.SqlParameter.Value
    //     is resolved.
    //
    //   sourceColumn:
    //     The name of the source column.
    //
    //   sourceVersion:
    //     One of the System.Data.DataRowVersion values.
    //
    //   value:
    //     An System.Object that is the value of the System.Data.SqlClient.SqlParameter.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value supplied in the dbType parameter is an invalid back-end data type.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public SqlParameter(string parameterName, SqlDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value);
    //
    // Summary:
    //     Initializes a new instance of the System.Data.SqlClient.SqlParameter class
    //     that uses the parameter name, the type of the parameter, the length of the
    //     parameter the direction, the precision, the scale, the name of the source
    //     column, one of the System.Data.DataRowVersion values, a Boolean for source
    //     column mapping, the value of the SqlParameter, the name of the database where
    //     the schema collection for this XML instance is located, the owning relational
    //     schema where the schema collection for this XML instance is located, and
    //     the name of the schema collection for this parameter.
    //
    // Parameters:
    //   parameterName:
    //     The name of the parameter to map.
    //
    //   dbType:
    //     One of the System.Data.SqlDbType values.
    //
    //   size:
    //     The length of the parameter.
    //
    //   direction:
    //     One of the System.Data.ParameterDirection values.
    //
    //   precision:
    //     The total number of digits to the left and right of the decimal point to
    //     which System.Data.SqlClient.SqlParameter.Value is resolved.
    //
    //   scale:
    //     The total number of decimal places to which System.Data.SqlClient.SqlParameter.Value
    //     is resolved.
    //
    //   sourceColumn:
    //     The name of the source column.
    //
    //   sourceVersion:
    //     One of the System.Data.DataRowVersion values.
    //
    //   sourceColumnNullMapping:
    //     true if the source column is nullable; false if it is not.
    //
    //   value:
    //     An System.Object that is the value of the System.Data.SqlClient.SqlParameter.
    //
    //   xmlSchemaCollectionDatabase:
    //     The name of the database where the schema collection for this XML instance
    //     is located.
    //
    //   xmlSchemaCollectionOwningSchema:
    //     The owning relational schema where the schema collection for this XML instance
    //     is located.
    //
    //   xmlSchemaCollectionName:
    //     The name of the schema collection for this parameter.
    //public SqlParameter(string parameterName, SqlDbType dbType, int size, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value, string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName);

    // Summary:
    //     Gets or sets the System.Globalization.CompareInfo object that defines how
    //     string comparisons should be performed for this parameter.
    //
    // Returns:
    //     A System.Globalization.CompareInfo object that defines string comparison
    //     for this parameter.
    //[Browsable(false)]
    //public SqlCompareOptions CompareInfo { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Data.SqlDbType of the parameter.
    //
    // Returns:
    //     One of the System.Data.SqlDbType values. The default is NVarChar.
    //public override DbType DbType { get; set; }
    //
    // Summary:
    //     Gets or sets a value that indicates whether the parameter is input-only,
    //     output-only, bidirectional, or a stored procedure return value parameter.
    //
    // Returns:
    //     One of the System.Data.ParameterDirection values. The default is Input.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The property was not set to one of the valid System.Data.ParameterDirection
    //     values.
    //[ResCategory("DataCategory_Data")]
    //[RefreshProperties(RefreshProperties.All)]
    //[ResDescription("DbParameter_Direction")]
    //public override ParameterDirection Direction { get; set; }
    //
    // Summary:
    //     Gets or sets a value that indicates whether the parameter accepts null values.
    //
    // Returns:
    //     true if null values are accepted; otherwise false. The default is false.
    //public override bool IsNullable { get; set; }
    //
    // Summary:
    //     Gets or sets the locale identifier that determines conventions and language
    //     for a particular region.
    //
    // Returns:
    //     Returns the locale identifier associated with the parameter.
    //[Browsable(false)]
    //public int LocaleId { get; set; }
    //
    // Summary:
    //     Gets or sets the offset to the System.Data.SqlClient.SqlParameter.Value property.
    //
    // Returns:
    //     The offset to the System.Data.SqlClient.SqlParameter.Value. The default is
    //     0.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DbParameter_Offset")]
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public int Offset { get; set; }
    //
    // Summary:
    //     Gets or sets the name of the System.Data.SqlClient.SqlParameter.
    //
    // Returns:
    //     The name of the System.Data.SqlClient.SqlParameter. The default is an empty
    //     string.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("SqlParameter_ParameterName")]
    //public override string ParameterName { get; set; }
    //
    // Summary:
    //     Gets or sets the maximum number of digits used to represent the System.Data.SqlClient.SqlParameter.Value
    //     property.
    //
    // Returns:
    //     The maximum number of digits used to represent the System.Data.SqlClient.SqlParameter.Value
    //     property. The default value is 0. This indicates that the data provider sets
    //     the precision for System.Data.SqlClient.SqlParameter.Value.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DbDataParameter_Precision")]
    //[DefaultValue(0)]
    public virtual byte Precision
    {
      get
      {
        Contract.Ensures(Contract.Result<byte>() >= 0);

        return default(byte);
      }
      //set; 
    }
    //
    // Summary:
    //     Gets or sets the number of decimal places to which System.Data.SqlClient.SqlParameter.Value
    //     is resolved.
    //
    // Returns:
    //     The number of decimal places to which System.Data.SqlClient.SqlParameter.Value
    //     is resolved. The default is 0.
    //[ResDescription("DbDataParameter_Scale")]
    //[ResCategory("DataCategory_Data")]
    //[DefaultValue(0)]
    public virtual byte Scale
    {
      get
      {
        Contract.Ensures(Contract.Result<byte>() >= 0);

        return default(byte);
      }
      //set; 
    }
    //
    // Summary:
    //     Gets or sets the maximum size, in bytes, of the data within the column.
    //
    // Returns:
    //     The maximum size, in bytes, of the data within the column. The default value
    //     is inferred from the parameter value.
    //[ResDescription("DbParameter_Size")]
    //[ResCategory("DataCategory_Data")]
    //public override int Size { get; set; }
    //
    // Summary:
    //     Gets or sets the name of the source column mapped to the System.Data.DataSet
    //     and used for loading or returning the System.Data.SqlClient.SqlParameter.Value
    //
    // Returns:
    //     The name of the source column mapped to the System.Data.DataSet. The default
    //     is an empty string.
    //[ResDescription("DbParameter_SourceColumn")]
    //[ResCategory("DataCategory_Update")]
    //public override string SourceColumn { get; set; }
    //
    // Summary:
    //     Sets or gets a value which indicates whether the source column is nullable.
    //     This allows System.Data.SqlClient.SqlCommandBuilder to correctly generate
    //     Update statements for nullable columns.
    //
    // Returns:
    //     true if the source column is nullable; false if it is not.
    //public override bool SourceColumnNullMapping { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Data.DataRowVersion to use when you load System.Data.SqlClient.SqlParameter.Value
    //
    // Returns:
    //     One of the System.Data.DataRowVersion values. The default is Current.
    //[ResCategory("DataCategory_Update")]
    //[ResDescription("DbParameter_SourceVersion")]
    //public override DataRowVersion SourceVersion { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Data.SqlDbType of the parameter.
    //
    // Returns:
    //     One of the System.Data.SqlDbType values. The default is NVarChar.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("SqlParameter_SqlDbType")]
    //[RefreshProperties(RefreshProperties.All)]
    //[DbProviderSpecificTypeProperty(true)]
    //public SqlDbType SqlDbType { get; set; }
    //
    // Summary:
    //     Gets or sets the value of the parameter as an SQL type.
    //
    // Returns:
    //     An System.Object that is the value of the parameter, using SQL types. The
    //     default value is null.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public object SqlValue { get; set; }
    //
    // Summary:
    //     Gets or sets the type name for a table-valued parameter.
    //
    // Returns:
    //     The type name of the specified table-valued parameter.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    public string TypeName
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
    //     Gets or sets a string that represents a user-defined type as a parameter.
    //
    // Returns:
    //     A string that represents the fully qualified name of a user-defined type.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public string UdtTypeName
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
    //     Gets or sets the value of the parameter.
    //
    // Returns:
    //     An System.Object that is the value of the parameter. The default value is
    //     null.
    //[TypeConverter(typeof(StringConverter))]
    //[ResCategory("DataCategory_Data")]
    //[RefreshProperties(RefreshProperties.All)]
    //[ResDescription("DbParameter_Value")]
    //public override object Value { get; set; }
    //
    // Summary:
    //     Gets the name of the database where the schema collection for this XML instance
    //     is located.
    //
    // Returns:
    //     The name of the database where the schema collection for this XML instance
    //     is located.
    //[ResDescription("SqlParameter_XmlSchemaCollectionDatabase")]
    //[ResCategory("DataCategory_Xml")]
    public string XmlSchemaCollectionDatabase
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
    //     Gets the name of the schema collection for this XML instance.
    //
    // Returns:
    //     The name of the schema collection for this XML instance.
    //[ResDescription("SqlParameter_XmlSchemaCollectionName")]
    //[ResCategory("DataCategory_Xml")]
    public string XmlSchemaCollectionName
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
    //     The owning relational schema where the schema collection for this XML instance
    //     is located.
    //
    // Returns:
    //     An System.Data.SqlClient.SqlParameter.XmlSchemaCollectionOwningSchema.
    //[ResCategory("DataCategory_Xml")]
    //[ResDescription("SqlParameter_XmlSchemaCollectionOwningSchema")]
    public string XmlSchemaCollectionOwningSchema
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      //set; 
    }

    // Summary:
    //     Resets the type associated with this System.Data.SqlClient.SqlParameter.
    //public override void ResetDbType();
    //
    // Summary:
    //     Resets the type associated with this System.Data.SqlClient.SqlParameter.
    //public void ResetSqlDbType();
    
  }
}

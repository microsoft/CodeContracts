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

namespace System.Data.Common
{
  // Summary:
  //     Represents a parameter to a System.Data.Common.DbCommand and optionally,
  //     its mapping to a System.Data.DataSet column.
  public abstract class DbParameter // : MarshalByRefObject, IDbDataParameter, IDataParameter
  {


    // Summary:
    //     Gets or sets the System.Data.DbType of the parameter.
    //
    // Returns:
    //     One of the System.Data.DbType values. The default is System.Data.DbType.String.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The property is not set to a valid System.Data.DbType.
    //[ResCategory("DataCategory_Data")]
    //[RefreshProperties(RefreshProperties.All)]
    //[ResDescription("DbParameter_DbType")]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public abstract DbType DbType { get; set; }
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
    //     The property is not set to one of the valid System.Data.ParameterDirection
    //     values.
    //[ResDescription("DbParameter_Direction")]
    //[ResCategory("DataCategory_Data")]
    //[RefreshProperties(RefreshProperties.All)]
    //public abstract ParameterDirection Direction { get; set; }
    //
    // Summary:
    //     Gets or sets a value that indicates whether the parameter accepts null values.
    //
    // Returns:
    //     true if null values are accepted; otherwise false. The default is false.
    //[Browsable(false)]
    //[DesignOnly(true)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public abstract bool IsNullable { get; set; }
    //
    // Summary:
    //     Gets or sets the name of the System.Data.Common.DbParameter.
    //
    // Returns:
    //     The name of the System.Data.Common.DbParameter. The default is an empty string
    //     ("").
    //[ResDescription("DbParameter_ParameterName")]
    //[DefaultValue("")]
    //[ResCategory("DataCategory_Data")]
    //public abstract string ParameterName { get; set; }
    //
    // Summary:
    //     Gets or sets the maximum size, in bytes, of the data within the column.
    //
    // Returns:
    //     The maximum size, in bytes, of the data within the column. The default value
    //     is inferred from the parameter value.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DbParameter_Size")]
    //public abstract int Size { get; set; }
    //
    // Summary:
    //     Gets or sets the name of the source column mapped to the System.Data.DataSet
    //     and used for loading or returning the System.Data.Common.DbParameter.Value.
    //
    // Returns:
    //     The name of the source column mapped to the System.Data.DataSet. The default
    //     is an empty string.
    //[ResDescription("DbParameter_SourceColumn")]
    //[DefaultValue("")]
    //[ResCategory("DataCategory_Update")]
    //public abstract string SourceColumn { get; set; }
    //
    // Summary:
    //     Sets or gets a value which indicates whether the source column is nullable.
    //     This allows System.Data.Common.DbCommandBuilder to correctly generate Update
    //     statements for nullable columns.
    //
    // Returns:
    //     true if the source column is nullable; false if it is not.
    //[ResCategory("DataCategory_Update")]
    //[ResDescription("DbParameter_SourceColumnNullMapping")]
    //[RefreshProperties(RefreshProperties.All)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[DefaultValue(false)]
    //public abstract bool SourceColumnNullMapping { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Data.DataRowVersion to use when you load System.Data.Common.DbParameter.Value.
    //
    // Returns:
    //     One of the System.Data.DataRowVersion values. The default is Current.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The property is not set to one of the System.Data.DataRowVersion values.
    //[ResDescription("DbParameter_SourceVersion")]
    //[ResCategory("DataCategory_Update")]
    //public abstract DataRowVersion SourceVersion { get; set; }
    //
    // Summary:
    //     Gets or sets the value of the parameter.
    //
    // Returns:
    //     An System.Object that is the value of the parameter. The default value is
    //     null.
    //[ResCategory("DataCategory_Data")]
    //[RefreshProperties(RefreshProperties.All)]
    //[ResDescription("DbParameter_Value")]
    //[DefaultValue("")]
    //public abstract object Value { get; set; }

    // Summary:
    //     Resets the DbType property to its original settings.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public abstract void ResetDbType();
  }
}

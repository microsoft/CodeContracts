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
  //     Represents a constraint that can be enforced on one or more System.Data.DataColumn
  //     objects.
  //[DefaultProperty("ConstraintName")]
  //[TypeConverter(typeof(ConstraintConverter))]
  public abstract class Constraint
  {
    // Summary:
    //     Initializes a new instance of the System.Data.Constraint class.
    //protected Constraint();

    // Summary:
    //     Gets the System.Data.DataSet to which this constraint belongs.
    //
    // Returns:
    //     The System.Data.DataSet to which the constraint belongs.
    //[CLSCompliant(false)]
     
    //** f: It seems it can be null
    //protected virtual DataSet _DataSet { get; }
    //
    // Summary:
    //     The name of a constraint in the System.Data.ConstraintCollection.
    //
    // Returns:
    //     The name of the System.Data.Constraint.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Data.Constraint name is a null value or empty string.
    //
    //   System.Data.DuplicateNameException:
    //     The System.Data.ConstraintCollection already contains a System.Data.Constraint
    //     with the same name (The comparison is not case-sensitive.).
    //[ResCategory("DataCategory_Data")]
    //[DefaultValue("")]
    //[ResDescription("ConstraintNameDescr")]
    public virtual string ConstraintName  
  {
    set
    {
      Contract.Requires(!string.IsNullOrEmpty(value));
    }

  }
    //
    // Summary:
    //     Gets the collection of user-defined constraint properties.
    //
    // Returns:
    //     A System.Data.PropertyCollection of custom information.
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
    //     Gets the System.Data.DataTable to which the constraint applies.
    //
    // Returns:
    //     A System.Data.DataTable to which the constraint applies.
    //[ResDescription("ConstraintTableDescr")]
    //public abstract DataTable Table { get; }

    //protected void CheckStateForProperty();
    //
    // Summary:
    //     Sets the constraint's System.Data.DataSet.
    //
    // Parameters:
    //   dataSet:
    //     The System.Data.DataSet to which this constraint will belong.
    //protected internal void SetDataSet(DataSet dataSet);
    //
    // Summary:
    //     Gets the System.Data.Constraint.ConstraintName, if there is one, as a string.
    //
    // Returns:
    //     The string value of the System.Data.Constraint.ConstraintName.
    //public override string ToString();
  }
}

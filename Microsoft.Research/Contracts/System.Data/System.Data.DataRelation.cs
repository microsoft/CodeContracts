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


namespace System.Data
{
  // Summary:
  //     Represents a parent/child relationship between two System.Data.DataTable
  //     objects.
  //[DefaultProperty("RelationName")]
  //[Editor("Microsoft.VSDesigner.Data.Design.DataRelationEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  //[TypeConverter(typeof(RelationshipConverter))]
  public class DataRelation
  {
    // Summary:
    //     Initializes a new instance of the System.Data.DataRelation class using the
    //     specified System.Data.DataRelation name, and parent and child System.Data.DataColumn
    //     objects.
    //
    // Parameters:
    //   relationName:
    //     The name of the System.Data.DataRelation. If null or an empty string (""),
    //     a default name will be given when the created object is added to the System.Data.DataRelationCollection.
    //
    //   parentColumn:
    //     The parent System.Data.DataColumn in the relationship.
    //
    //   childColumn:
    //     The child System.Data.DataColumn in the relationship.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     One or both of the System.Data.DataColumn objects contains null.
    //
    //   System.Data.InvalidConstraintException:
    //     The columns have different data types -Or- The tables do not belong to the
    //     same System.Data.DataSet.
    //public DataRelation(string relationName, DataColumn parentColumn, DataColumn childColumn);
    //
    // Summary:
    //     Initializes a new instance of the System.Data.DataRelation class using the
    //     specified System.Data.DataRelation name and matched arrays of parent and
    //     child System.Data.DataColumn objects.
    //
    // Parameters:
    //   relationName:
    //     The name of the relation. If null or an empty string (""), a default name
    //     will be given when the created object is added to the System.Data.DataRelationCollection.
    //
    //   parentColumns:
    //     An array of parent System.Data.DataColumn objects.
    //
    //   childColumns:
    //     An array of child System.Data.DataColumn objects.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     One or both of the System.Data.DataColumn objects contains null.
    //
    //   System.Data.InvalidConstraintException:
    //     The System.Data.DataColumn objects have different data types -Or- One or
    //     both of the arrays are not composed of distinct columns from the same table.-Or-
    //     The tables do not belong to the same System.Data.DataSet.
    //public DataRelation(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns);
    //
    // Summary:
    //     Initializes a new instance of the System.Data.DataRelation class using the
    //     specified name, parent and child System.Data.DataColumn objects, and a value
    //     that indicates whether to create constraints.
    //
    // Parameters:
    //   relationName:
    //     The name of the relation. If null or an empty string (""), a default name
    //     will be given when the created object is added to the System.Data.DataRelationCollection.
    //
    //   parentColumn:
    //     The parent System.Data.DataColumn in the relation.
    //
    //   childColumn:
    //     The child System.Data.DataColumn in the relation.
    //
    //   createConstraints:
    //     A value that indicates whether constraints are created. true, if constraints
    //     are created. Otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     One or both of the System.Data.DataColumn objects contains null.
    //
    //   System.Data.InvalidConstraintException:
    //     The columns have different data types -Or- The tables do not belong to the
    //     same System.Data.DataSet.
    //public DataRelation(string relationName, DataColumn parentColumn, DataColumn childColumn, bool createConstraints);
    //
    // Summary:
    //     Initializes a new instance of the System.Data.DataRelation class using the
    //     specified name, matched arrays of parent and child System.Data.DataColumn
    //     objects, and value that indicates whether to create constraints.
    //
    // Parameters:
    //   relationName:
    //     The name of the relation. If null or an empty string (""), a default name
    //     will be given when the created object is added to the System.Data.DataRelationCollection.
    //
    //   parentColumns:
    //     An array of parent System.Data.DataColumn objects.
    //
    //   childColumns:
    //     An array of child System.Data.DataColumn objects.
    //
    //   createConstraints:
    //     A value that indicates whether to create constraints. true, if constraints
    //     are created. Otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     One or both of the System.Data.DataColumn objects is null.
    //
    //   System.Data.InvalidConstraintException:
    //     The columns have different data types -Or- The tables do not belong to the
    //     same System.Data.DataSet.
    //public DataRelation(string relationName, DataColumn//[] parentColumns, DataColumn//[] childColumns, bool createConstraints);
    //
    // Summary:
    //     This constructor is provided for design time support in the Visual Studio
    //     environment.
    //
    // Parameters:
    //   relationName:
    //     The name of the relation. If null or an empty string (""), a default name
    //     will be given when the created object is added to the System.Data.DataRelationCollection.
    //
    //   parentTableName:
    //     The name of the System.Data.DataTable that is the parent table of the relation.
    //
    //   childTableName:
    //     The name of the System.Data.DataTable that is the child table of the relation.
    //
    //   parentColumnNames:
    //     An array of System.Data.DataColumn object names in the parent System.Data.DataTable
    //     of the relation.
    //
    //   childColumnNames:
    //     An array of System.Data.DataColumn object names in the child System.Data.DataTable
    //     of the relation.
    //
    //   nested:
    //     A value that indicates whether relationships are nested.
    //[Browsable(false)]
    //public DataRelation(string relationName, string parentTableName, string childTableName, string//[] parentColumnNames, string//[] childColumnNames, bool nested);
    //
    // Summary:
    //     This constructor is provided for design time support in the Visual Studio
    //     environment.
    //
    // Parameters:
    //   relationName:
    //     The name of the System.Data.DataRelation. If null or an empty string (""),
    //     a default name will be given when the created object is added to the System.Data.DataRelationCollection.
    //
    //   parentTableName:
    //     The name of the System.Data.DataTable that is the parent table of the relation.
    //
    //   parentTableNamespace:
    //     The name of the parent table namespace.
    //
    //   childTableName:
    //     The name of the System.Data.DataTable that is the child table of the relation.
    //
    //   childTableNamespace:
    //     The name of the child table namespace.
    //
    //   parentColumnNames:
    //     An array of System.Data.DataColumn object names in the parent System.Data.DataTable
    //     of the relation.
    //
    //   childColumnNames:
    //     An array of System.Data.DataColumn object names in the child System.Data.DataTable
    //     of the relation.
    //
    //   nested:
    //     A value that indicates whether relationships are nested.
    //[Browsable(false)]
    //public DataRelation(string relationName, string parentTableName, string parentTableNamespace, string childTableName, string childTableNamespace, string//[] parentColumnNames, string//[] childColumnNames, bool nested);

    // Summary:
    //     Gets the child System.Data.DataColumn objects of this relation.
    //
    // Returns:
    //     An array of System.Data.DataColumn objects.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DataRelationChildColumnsDescr")]
    //public virtual DataColumn[] ChildColumns { get; }
    //
    // Summary:
    //     Gets the System.Data.ForeignKeyConstraint for the relation.
    //
    // Returns:
    //     A System.Data.ForeignKeyConstraint.
    //public virtual ForeignKeyConstraint ChildKeyConstraint { get; }
    //
    // Summary:
    //     Gets the child table of this relation.
    //
    // Returns:
    //     A System.Data.DataTable that is the child table of the relation.
    //public virtual DataTable ChildTable { get; }
    //
    // Summary:
    //     Gets the System.Data.DataSet to which the System.Data.DataRelation belongs.
    //
    // Returns:
    //     A System.Data.DataSet to which the System.Data.DataRelation belongs.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public virtual DataSet DataSet { get; }
    //
    // Summary:
    //     Gets the collection that stores customized properties.
    //
    // Returns:
    //     A System.Data.PropertyCollection that contains customized properties.
    //[ResDescription("ExtendedPropertiesDescr")]
    //[ResCategory("DataCategory_Data")]
    //[Browsable(false)]
    //public PropertyCollection ExtendedProperties { get; }
    //
    // Summary:
    //     Gets or sets a value that indicates whether System.Data.DataRelation objects
    //     are nested.
    //
    // Returns:
    //     true, if System.Data.DataRelation objects are nested; otherwise, false.
    //[ResDescription("DataRelationNested")]
    //[ResCategory("DataCategory_Data")]
    //[DefaultValue(false)]
    //public virtual bool Nested { get; set; }
    //
    // Summary:
    //     Gets an array of System.Data.DataColumn objects that are the parent columns
    //     of this System.Data.DataRelation.
    //
    // Returns:
    //     An array of System.Data.DataColumn objects that are the parent columns of
    //     this System.Data.DataRelation.
    //[ResCategory("DataCategory_Data")]
    //[ResDescription("DataRelationParentColumnsDescr")]
    //public virtual DataColumn[] ParentColumns { get; }
    //
    // Summary:
    //     Gets the System.Data.UniqueConstraint that guarantees that values in the
    //     parent column of a System.Data.DataRelation are unique.
    //
    // Returns:
    //     A System.Data.UniqueConstraint that makes sure that values in a parent column
    //     are unique.
    //public virtual UniqueConstraint ParentKeyConstraint { get; }
    //
    // Summary:
    //     Gets the parent System.Data.DataTable of this System.Data.DataRelation.
    //
    // Returns:
    //     A System.Data.DataTable that is the parent table of this relation.
    //public virtual DataTable ParentTable { get; }
    //
    // Summary:
    //     Gets or sets the name used to retrieve a System.Data.DataRelation from the
    //     System.Data.DataRelationCollection.
    //
    // Returns:
    //     The name of the a System.Data.DataRelation.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     null or empty string ("") was passed into a System.Data.DataColumn that is
    //     a System.Data.DataRelation.
    //
    //   System.Data.DuplicateNameException:
    //     The System.Data.DataRelation belongs to a collection that already contains
    //     a System.Data.DataRelation with the same name.
    //[ResDescription("DataRelationRelationNameDescr")]
    //[DefaultValue("")]
    //[ResCategory("DataCategory_Data")]
    //public virtual string RelationName { get; set; }

    //
    // Exceptions:
    //   System.Data.DataException:
    //     The parent and child tables belong to different System.Data.DataSet objects.-Or-
    //     One or more pairs of parent and child System.Data.DataColumn objects have
    //     mismatched data types.-Or- The parent and child System.Data.DataColumn objects
    //     are identical.
    //protected void CheckStateForProperty();
    //
    // Summary:
    //     This member supports the .NET Framework infrastructure and is not intended
    //     to be used directly from your code.
    //
    // Parameters:
    //   pcevent:
    //     Parameter reference.
    //protected internal void OnPropertyChanging(PropertyChangedEventArgs pcevent);
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
    //     Gets the System.Data.DataRelation.RelationName, if one exists.
    //
    // Returns:
    //     The value of the System.Data.DataRelation.RelationName property.
    //public override string ToString();
  }
}

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
  //     Represents a collection of constraints for a System.Data.DataTable.
  //[Editor("Microsoft.VSDesigner.Data.Design.ConstraintsCollectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  //[DefaultEvent("CollectionChanged")]
  public sealed class ConstraintCollection //: InternalDataCollectionBase
  {
    //protected override ArrayList List { get; }

    // Summary:
    //     Gets the System.Data.Constraint from the collection at the specified index.
    //
    // Parameters:
    //   index:
    //     The index of the constraint to return.
    //
    // Returns:
    //     The System.Data.Constraint at the specified index.
    //
    // Exceptions:
    //   System.IndexOutOfRangeException:
    //     The index value is greater than the number of items in the collection.
    //public Constraint this[int index] { get; }
    //
    // Summary:
    //     Gets the System.Data.Constraint from the collection with the specified name.
    //
    // Parameters:
    //   name:
    //     The System.Data.Constraint.ConstraintName of the constraint to return.
    //
    // Returns:
    //     The System.Data.Constraint with the specified name; otherwise a null value
    //     if the System.Data.Constraint does not exist.
    //public Constraint this[string name] { get; }

    // Summary:
    //     Occurs whenever the System.Data.ConstraintCollection is changed because of
    //     System.Data.Constraint objects being added or removed.
    //public event CollectionChangeEventHandler CollectionChanged;

    // Summary:
    //     Adds the specified System.Data.Constraint object to the collection.
    //
    // Parameters:
    //   constraint:
    //     The Constraint to add.
    //
    // Returns:
    //     A new System.Data.UniqueConstraint or System.Data.ForeignKeyConstraint.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The constraint argument is null.
    //
    //   System.ArgumentException:
    //     The constraint already belongs to this collection, or belongs to another
    //     collection.
    //
    //   System.Data.DuplicateNameException:
    //     The collection already has a constraint with the same name. (The comparison
    //     is not case-sensitive.)
    public void Add(Constraint constraint)
    {
      Contract.Requires(constraint != null);
    }
    //
    // Summary:
    //     Constructs a new System.Data.UniqueConstraint with the specified name, System.Data.DataColumn,
    //     and value that indicates whether the column is a primary key, and adds it
    //     to the collection.
    //
    // Parameters:
    //   name:
    //     The name of the UniqueConstraint.
    //
    //   column:
    //     The System.Data.DataColumn to which the constraint applies.
    //
    //   primaryKey:
    //     Specifies whether the column should be the primary key. If true, the column
    //     will be a primary key column.
    //
    // Returns:
    //     A new UniqueConstraint.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The constraint already belongs to this collection.-Or- The constraint belongs
    //     to another collection.
    //
    //   System.Data.DuplicateNameException:
    //     The collection already has a constraint with the specified name. (The comparison
    //     is not case-sensitive.)
    public Constraint Add(string name, DataColumn column, bool primaryKey)
    {
      Contract.Ensures(Contract.Result<Constraint>() != null);

      return default(Constraint);
    }
    //
    // Summary:
    //     Constructs a new System.Data.ForeignKeyConstraint with the specified name,
    //     parent column, and child column, and adds the constraint to the collection.
    //
    // Parameters:
    //   name:
    //     The name of the System.Data.ForeignKeyConstraint.
    //
    //   primaryKeyColumn:
    //     The primary key, or parent, System.Data.DataColumn.
    //
    //   foreignKeyColumn:
    //     The foreign key, or child, System.Data.DataColumn.
    //
    // Returns:
    //     A new System.Data.ForeignKeyConstraint.
    public Constraint Add(string name, DataColumn primaryKeyColumn, DataColumn foreignKeyColumn)
          {
      Contract.Ensures(Contract.Result<Constraint>() != null);

      return default(Constraint);
    }

    //
    // Summary:
    //     Constructs a new System.Data.UniqueConstraint with the specified name, array
    //     of System.Data.DataColumn objects, and value that indicates whether the column
    //     is a primary key, and adds it to the collection.
    //
    // Parameters:
    //   name:
    //     The name of the System.Data.UniqueConstraint.
    //
    //   columns:
    //     An array of System.Data.DataColumn objects to which the constraint applies.
    //
    //   primaryKey:
    //     Specifies whether the column should be the primary key. If true, the column
    //     will be a primary key column.
    //
    // Returns:
    //     A new System.Data.UniqueConstraint.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The constraint already belongs to this collection.-Or- The constraint belongs
    //     to another collection.
    //
    //   System.Data.DuplicateNameException:
    //     The collection already has a constraint with the specified name. (The comparison
    //     is not case-sensitive.)
    public Constraint Add(string name, DataColumn[] columns, bool primaryKey)
          {
      Contract.Ensures(Contract.Result<Constraint>() != null);

      return default(Constraint);
    }

    //
    // Summary:
    //     Constructs a new System.Data.ForeignKeyConstraint, with the specified arrays
    //     of parent columns and child columns, and adds the constraint to the collection.
    //
    // Parameters:
    //   name:
    //     The name of the System.Data.ForeignKeyConstraint.
    //
    //   primaryKeyColumns:
    //     An array of System.Data.DataColumn objects that are the primary key, or parent,
    //     columns.
    //
    //   foreignKeyColumns:
    //     An array of System.Data.DataColumn objects that are the foreign key, or child,
    //     columns.
    //
    // Returns:
    //     A new System.Data.ForeignKeyConstraint.
    public Constraint Add(string name, DataColumn[] primaryKeyColumns, DataColumn[] foreignKeyColumns)
          {
      Contract.Ensures(Contract.Result<Constraint>() != null);

      return default(Constraint);
    }
    //
    // Summary:
    //     Copies the elements of the specified System.Data.ConstraintCollection array
    //     to the end of the collection.
    //
    // Parameters:
    //   constraints:
    //     An array of System.Data.ConstraintCollection objects to add to the collection.
    //public void AddRange(Constraint[] constraints);
    //
    // Summary:
    //     Indicates whether a System.Data.Constraint can be removed.
    //
    // Parameters:
    //   constraint:
    //     The System.Data.Constraint to be tested for removal from the collection.
    //
    // Returns:
    //     true if the System.Data.Constraint can be removed from collection; otherwise,
    //     false.
    //public bool CanRemove(Constraint constraint);
    //
    // Summary:
    //     Clears the collection of any System.Data.Constraint objects.
    //public void Clear();
    //
    // Summary:
    //     Indicates whether the System.Data.Constraint object specified by name exists
    //     in the collection.
    //
    // Parameters:
    //   name:
    //     The System.Data.Constraint.ConstraintName of the constraint.
    //
    // Returns:
    //     true if the collection contains the specified constraint; otherwise, false.
    //public bool Contains(string name);
    //
    // Summary:
    //     Copies the collection objects to a one-dimensional System.Array instance
    //     starting at the specified index.
    //
    // Parameters:
    //   array:
    //     The one-dimensional System.Array that is the destination of the values copied
    //     from the collection.
    //
    //   index:
    //     The index of the array at which to start inserting.
    //public void CopyTo(Constraint//[] array, int index);
    //
    // Summary:
    //     Gets the index of the specified System.Data.Constraint.
    //
    // Parameters:
    //   constraint:
    //     The System.Data.Constraint to search for.
    //
    // Returns:
    //     The zero-based index of the System.Data.Constraint if it is in the collection;
    //     otherwise, -1.
    //public int IndexOf(Constraint constraint);
    //
    // Summary:
    //     Gets the index of the System.Data.Constraint specified by name.
    //
    // Parameters:
    //   constraintName:
    //     The name of the System.Data.Constraint.
    //
    // Returns:
    //     The index of the System.Data.Constraint if it is in the collection; otherwise,
    //     -1.
    //public int IndexOf(string constraintName);
    //
    // Summary:
    //     Removes the specified System.Data.Constraint from the collection.
    //
    // Parameters:
    //   constraint:
    //     The System.Data.Constraint to remove.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The constraint argument is null.
    //
    //   System.ArgumentException:
    //     The constraint does not belong to the collection.
    //public void Remove(Constraint constraint);
    //
    // Summary:
    //     Removes the System.Data.Constraint object specified by name from the collection.
    //
    // Parameters:
    //   name:
    //     The name of the System.Data.Constraint to remove.
    //public void Remove(string name);
    //
    // Summary:
    //     Removes the System.Data.Constraint object at the specified index from the
    //     collection.
    //
    // Parameters:
    //   index:
    //     The index of the System.Data.Constraint to remove.
    //
    // Exceptions:
    //   System.IndexOutOfRangeException:
    //     The collection does not have a constraint at this index.
    //public void RemoveAt(int index);
  }
}

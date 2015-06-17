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
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Diagnostics.Contracts;

namespace System.Data
{
  // Summary:
  //     Represents the collection of System.Data.DataRelation objects for this System.Data.DataSet.
  //[DefaultProperty("Table")]
  //[DefaultEvent("CollectionChanged")]
  //[Editor("Microsoft.VSDesigner.Data.Design.DataRelationCollectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  public abstract class DataRelationCollection //: InternalDataCollectionBase
  {
    // Summary:
    //     Initializes a new instance of the System.Data.DataRelationCollection class.
    //protected DataRelationCollection();

    // Summary:
    //     Gets the System.Data.DataRelation object at the specified index.
    //
    // Parameters:
    //   index:
    //     The zero-based index to find.
    //
    // Returns:
    //     The System.Data.DataRelation, or a null value if the specified System.Data.DataRelation
    //     does not exist.
    //
    // Exceptions:
    //   System.IndexOutOfRangeException:
    //     The index value is greater than the number of items in the collection.
    //public abstract DataRelation this[int index] { get; }
    //
    // Summary:
    //     Gets the System.Data.DataRelation object specified by name.
    //
    // Parameters:
    //   name:
    //     The name of the relation to find.
    //
    // Returns:
    //     The named System.Data.DataRelation, or a null value if the specified System.Data.DataRelation
    //     does not exist.
    //public abstract DataRelation this[string name] { get; }

    // Summary:
    //     Occurs when the collection has changed.
    //[ResDescription("collectionChangedEventDescr")]
    //public event CollectionChangeEventHandler CollectionChanged;

    // Summary:
    //     Adds a System.Data.DataRelation to the System.Data.DataRelationCollection.
    //
    // Parameters:
    //   relation:
    //     The DataRelation to add to the collection.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The relation parameter is a null value.
    //
    //   System.ArgumentException:
    //     The relation already belongs to this collection, or it belongs to another
    //     collection.
    //
    //   System.Data.DuplicateNameException:
    //     The collection already has a relation with the specified name. (The comparison
    //     is not case sensitive.)
    //
    //   System.Data.InvalidConstraintException:
    //     The relation has entered an invalid state since it was created.
    public void Add(DataRelation relation)
    {
      Contract.Requires(relation != null);
    }
    
    // Summary:
    //     Creates a System.Data.DataRelation with a specified parent and child column,
    //     and adds it to the collection.
    //
    // Parameters:
    //   parentColumn:
    //     The parent column of the relation.
    //
    //   childColumn:
    //     The child column of the relation.
    //
    // Returns:
    //     The created relation.
    public virtual DataRelation Add(DataColumn parentColumn, DataColumn childColumn)
    {
      Contract.Ensures(Contract.Result<DataRelation>() != null);

      return default(DataRelation);
    }
    //
    // Summary:
    //     Creates a System.Data.DataRelation with the specified parent and child columns,
    //     and adds it to the collection.
    //
    // Parameters:
    //   parentColumns:
    //     The parent columns of the relation.
    //
    //   childColumns:
    //     The child columns of the relation.
    //
    // Returns:
    //     The created relation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The relation argument is a null value.
    //
    //   System.ArgumentException:
    //     The relation already belongs to this collection, or it belongs to another
    //     collection.
    //
    //   System.Data.DuplicateNameException:
    //     The collection already has a relation with the same name. (The comparison
    //     is not case sensitive.)
    //
    //   System.Data.InvalidConstraintException:
    //     The relation has entered an invalid state since it was created.
    public virtual DataRelation Add(DataColumn[] parentColumns, DataColumn[] childColumns)
    {
            Contract.Ensures(Contract.Result<DataRelation>() != null);

      return default(DataRelation);
    }

    //
    // Summary:
    //     Creates a System.Data.DataRelation with the specified name, and parent and
    //     child columns, and adds it to the collection.
    //
    // Parameters:
    //   name:
    //     The name of the relation.
    //
    //   parentColumn:
    //     The parent column of the relation.
    //
    //   childColumn:
    //     The child column of the relation.
    //
    // Returns:
    //     The created relation.
    public virtual DataRelation Add(string name, DataColumn parentColumn, DataColumn childColumn)
    {
            Contract.Ensures(Contract.Result<DataRelation>() != null);

      return default(DataRelation);
    }
    //
    // Summary:
    //     Creates a System.Data.DataRelation with the specified name and arrays of
    //     parent and child columns, and adds it to the collection.
    //
    // Parameters:
    //   name:
    //     The name of the DataRelation to create.
    //
    //   parentColumns:
    //     An array of parent System.Data.DataColumn objects.
    //
    //   childColumns:
    //     An array of child DataColumn objects.
    //
    // Returns:
    //     The created DataRelation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The relation name is a null value.
    //
    //   System.ArgumentException:
    //     The relation already belongs to this collection, or it belongs to another
    //     collection.
    //
    //   System.Data.DuplicateNameException:
    //     The collection already has a relation with the same name. (The comparison
    //     is not case sensitive.)
    //
    //   System.Data.InvalidConstraintException:
    //     The relation has entered an invalid state since it was created.
    public virtual DataRelation Add(string name, DataColumn[] parentColumns, DataColumn[] childColumns)
        {
    Contract.Requires(name != null);
            Contract.Ensures(Contract.Result<DataRelation>() != null);

      return default(DataRelation);
    }
      
      //
    // Summary:
    //     Creates a System.Data.DataRelation with the specified name, parent and child
    //     columns, with optional constraints according to the value of the createConstraints
    //     parameter, and adds it to the collection.
    //
    // Parameters:
    //   name:
    //     The name of the relation.
    //
    //   parentColumn:
    //     The parent column of the relation.
    //
    //   childColumn:
    //     The child column of the relation.
    //
    //   createConstraints:
    //     true to create constraints; otherwise false. (The default is true).
    //
    // Returns:
    //     The created relation.
    public virtual DataRelation Add(string name, DataColumn parentColumn, DataColumn childColumn, bool createConstraints)
              {
            Contract.Ensures(Contract.Result<DataRelation>() != null);

      return default(DataRelation);
    }

    //
    // Summary:
    //     Creates a System.Data.DataRelation with the specified name, arrays of parent
    //     and child columns, and value specifying whether to create a constraint, and
    //     adds it to the collection.
    //
    // Parameters:
    //   name:
    //     The name of the DataRelation to create.
    //
    //   parentColumns:
    //     An array of parent System.Data.DataColumn objects.
    //
    //   childColumns:
    //     An array of child DataColumn objects.
    //
    //   createConstraints:
    //     true to create a constraint; otherwise false.
    //
    // Returns:
    //     The created relation.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The relation name is a null value.
    //
    //   System.ArgumentException:
    //     The relation already belongs to this collection, or it belongs to another
    //     collection.
    //
    //   System.Data.DuplicateNameException:
    //     The collection already has a relation with the same name. (The comparison
    //     is not case sensitive.)
    //
    //   System.Data.InvalidConstraintException:
    //     The relation has entered an invalid state since it was created.
    public virtual DataRelation Add(string name, DataColumn[] parentColumns, DataColumn[] childColumns, bool createConstraints)
              {
    Contract.Requires(name != null);
            Contract.Ensures(Contract.Result<DataRelation>() != null);

      return default(DataRelation);
    }
    //
    // Summary:
    //     Performs verification on the table.
    //
    // Parameters:
    //   relation:
    //     The relation to check.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The relation is null.
    //
    //   System.ArgumentException:
    //     The relation already belongs to this collection, or it belongs to another
    //     collection.
    //
    //   System.Data.DuplicateNameException:
    //     The collection already has a relation with the same name. (The comparison
    //     is not case sensitive.)
    protected virtual void AddCore(DataRelation relation)
    {
      Contract.Requires(relation != null);
    }
    //
    // Summary:
    //     Copies the elements of the specified System.Data.DataRelation array to the
    //     end of the collection.
    //
    // Parameters:
    //   relations:
    //     The array of System.Data.DataRelation objects to add to the collection.
    //public virtual void AddRange(DataRelation[] relations);
    //
  
  
    //
    // Summary:
    //     Raises the System.Data.DataRelationCollection.CollectionChanged event.
    //
    // Parameters:
    //   ccevent:
    //     A System.ComponentModel.CollectionChangeEventArgs that contains the event
    //     data.
    //protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent);
    //
    // Summary:
    //     Raises the System.Data.DataRelationCollection.CollectionChanged event.
    //
    // Parameters:
    //   ccevent:
    //     A System.ComponentModel.CollectionChangeEventArgs that contains the event
    //     data.
    //protected virtual void OnCollectionChanging(CollectionChangeEventArgs ccevent);
    //
    // Summary:
    //     Removes the specified relation from the collection.
    //
    // Parameters:
    //   relation:
    //     The relation to remove.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The relation is a null value.
    //
    //   System.ArgumentException:
    ////     The relation does not belong to the collection.
    //public void Remove(DataRelation relation);
    ////
    //// Summary:
    ////     Removes the relation with the specified name from the collection.
    ////
    //// Parameters:
    ////   name:
    ////     The name of the relation to remove.
    ////
    //// Exceptions:
    ////   System.IndexOutOfRangeException:
    ////     The collection does not have a relation with the specified name.
    //public void Remove(string name);
    ////
    //// Summary:
    ////     Removes the relation at the specified index from the collection.
    ////
    //// Parameters:
    ////   index:
    ////     The index of the relation to remove.
    ////
    //// Exceptions:
    ////   System.ArgumentException:
    ////     The collection does not have a relation at the specified index.
    //public void RemoveAt(int index);
    ////
    //// Summary:
    ////     Performs a verification on the specified System.Data.DataRelation object.
    ////
    //// Parameters:
    ////   relation:
    ////     The DataRelation object to verify.
    ////
    //// Exceptions:
    ////   System.ArgumentNullException:
    ////     The collection does not have a relation at the specified index.
    ////
    ////   System.ArgumentException:
    ////     The specified relation does not belong to this collection, or it belongs
    ////     to another collection.
    //protected virtual void RemoveCore(DataRelation relation);
  }
}

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
using System.Data;
using System.Reflection;
using System.Diagnostics.Contracts;

namespace System.Data.Common
{
  // Summary:
  //     The base class for a collection of parameters relevant to a System.Data.Common.DbCommand.
  public abstract class DbParameterCollection //: MarshalByRefObject, IDataParameterCollection, IList, ICollection, IEnumerable
  {
    // Summary:
    //     Initializes a new instance of the System.Data.Common.DbParameterCollection
    //     class.
    //protected DbParameterCollection();

    // Summary:
    //     Specifies the number of items in the collection.
    //
    // Returns:
    //     The number of items in the collection.
    //rowsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public abstract int Count { get; }
    //
    // Summary:
    //     Specifies whether the collection is a fixed size.
    //
    // Returns:
    //     true if the collection is a fixed size; otherwise false.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //rowsable(false)]
    //public abstract bool IsFixedSize { get; }
    //
    // Summary:
    //     Specifies whether the collection is read-only.
    //
    // Returns:
    //     true if the collection is read-only; otherwise false.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //rowsable(false)]
    //public abstract bool IsReadOnly { get; }
    //
    // Summary:
    //     Specifies whether the collection is synchronized.
    //
    // Returns:
    //     true if the collection is synchronized; otherwise false.
    //rowsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public abstract bool IsSynchronized { get; }
    //
    // Summary:
    //     Specifies the System.Object to be used to synchronize access to the collection.
    //
    // Returns:
    //     A System.Object to be used to synchronize access to the System.Data.Common.DbParameterCollection.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //rowsable(false)]
    //public abstract object SyncRoot { get; }

    // Summary:
    //     Gets and sets the System.Data.Common.DbParameter at the specified index.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the parameter.
    //
    // Returns:
    //     The System.Data.Common.DbParameter at the specified index.
    //
    // Exceptions:
    //   System.IndexOutOfRangeException:
    //     The specified index does not exist.
    public DbParameter this[int index]
    {
      get
      {
        Contract.Requires(index >= 0);

        return default(DbParameter);
      }
      set
      {
        Contract.Requires(index >= 0);
      }
    }
    
    
    //
    // Summary:
    //     Gets and sets the System.Data.Common.DbParameter with the specified name.
    //
    // Parameters:
    //   parameterName:
    //     The name of the parameter.
    //
    // Returns:
    //     The System.Data.Common.DbParameter with the specified name.
    //
    // Exceptions:
    //   System.IndexOutOfRangeException:
    //     The specified index does not exist.
    //public DbParameter this[string parameterName] { get; set; }

    // Summary:
    //     Adds a System.Data.Common.DbParameter item with the specified value to the
    //     System.Data.Common.DbParameterCollection.
    //
    // Parameters:
    //   value:
    //     The System.Data.Common.DbParameter.Value of the System.Data.Common.DbParameter
    //     to add to the collection.
    //
    // Returns:
    //     The index of the System.Data.Common.DbParameter object in the collection.
    //public abstract int Add(object value);
    //
    // Summary:
    //     Adds an array of items with the specified values to the System.Data.Common.DbParameterCollection.
    //
    // Parameters:
    //   values:
    //     An array of values of type System.Data.Common.DbParameter to add to the collection.
    //public abstract void AddRange(Array values);
    //
    // Summary:
    //     Removes all System.Data.Common.DbParameter values from the System.Data.Common.DbParameterCollection.
    //public abstract void Clear();
    //
    // Summary:
    //     Indicates whether a System.Data.Common.DbParameter with the specified System.Data.Common.DbParameter.Value
    //     is contained in the collection.
    //
    // Parameters:
    //   value:
    //     The System.Data.Common.DbParameter.Value of the System.Data.Common.DbParameter
    //     to look for in the collection.
    //
    // Returns:
    //     true if the System.Data.Common.DbParameter is in the collection; otherwise
    //     false.
    //public abstract bool Contains(object value);
    //
    // Summary:
    //     Indicates whether a System.Data.Common.DbParameter with the specified name
    //     exists in the collection.
    //
    // Parameters:
    //   value:
    //     The name of the System.Data.Common.DbParameter to look for in the collection.
    //
    // Returns:
    //     true if the System.Data.Common.DbParameter is in the collection; otherwise
    //     false.
    //public abstract bool Contains(string value);
    //
    // Summary:
    //     Copies an array of items to the collection starting at the specified index.
    //
    // Parameters:
    //   array:
    //     The array of items to copy to the collection.
    //
    //   index:
    //     The index in the collection to copy the items.
    //public abstract void CopyTo(Array array, int index);
    //
    // Summary:
    //     Exposes the System.Collections.IEnumerable.GetEnumerator() method, which
    //     supports a simple iteration over a collection by a .NET Framework data provider.
    //
    // Returns:
    //     An System.Collections.IEnumerator that can be used to iterate through the
    //     collection.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public abstract IEnumerator GetEnumerator();
    //
    // Summary:
    //     Returns the System.Data.Common.DbParameter object at the specified index
    //     in the collection.
    //
    // Parameters:
    //   index:
    //     The index of the System.Data.Common.DbParameter in the collection.
    //
    // Returns:
    //     The System.Data.Common.DbParameter object at the specified index in the collection.
    //protected abstract DbParameter GetParameter(int index);
    //
    // Summary:
    //     Returns System.Data.Common.DbParameter the object with the specified name.
    //
    // Parameters:
    //   parameterName:
    //     The name of the System.Data.Common.DbParameter in the collection.
    //
    // Returns:
    //     The System.Data.Common.DbParameter the object with the specified name.
    //protected abstract DbParameter GetParameter(string parameterName);
    //
    // Summary:
    //     Returns the index of the specified System.Data.Common.DbParameter object.
    //
    // Parameters:
    //   value:
    //     The System.Data.Common.DbParameter object in the collection.
    //
    // Returns:
    //     The index of the specified System.Data.Common.DbParameter object.
    //public abstract int IndexOf(object value);
    ////
    // Summary:
    //     Returns the index of the System.Data.Common.DbParameter object with the specified
    //     name.
    //
    // Parameters:
    //   parameterName:
    //     The name of the System.Data.Common.DbParameter object in the collection.
    //
    // Returns:
    //     The index of the System.Data.Common.DbParameter object with the specified
    //     name.
    //public abstract int IndexOf(string parameterName);
    //
    // Summary:
    //     Inserts the specified index of the System.Data.Common.DbParameter object
    //     with the specified name into the collection at the specified index.
    //
    // Parameters:
    //   index:
    //     The index at which to insert the System.Data.Common.DbParameter object.
    //
    //   value:
    //     The System.Data.Common.DbParameter object to insert into the collection.
    //public abstract void Insert(int index, object value);
    //
    // Summary:
    //     Removes the specified System.Data.Common.DbParameter object from the collection.
    //
    // Parameters:
    //   value:
    //     The System.Data.Common.DbParameter object to remove.
    //public abstract void Remove(object value);
    //
    // Summary:
    //     Removes the System.Data.Common.DbParameter object at the specified from the
    //     collection.
    //
    // Parameters:
    //   index:
    //     The index where the System.Data.Common.DbParameter object is located.
    //public abstract void RemoveAt(int index);
    //
    // Summary:
    //     Removes the System.Data.Common.DbParameter object with the specified name
    //     from the collection.
    //
    // Parameters:
    //   parameterName:
    //     The name of the System.Data.Common.DbParameter object to remove.
    //public abstract void RemoveAt(string parameterName);
    //
    // Summary:
    //     Sets the System.Data.Common.DbParameter object at the specified index to
    //     a new value.
    //
    // Parameters:
    //   index:
    //     The index where the System.Data.Common.DbParameter object is located.
    //
    //   value:
    //     The new System.Data.Common.DbParameter value.
    //protected abstract void SetParameter(int index, DbParameter value);
    //
    // Summary:
    //     Sets the System.Data.Common.DbParameter object with the specified name to
    //     a new value.
    //
    // Parameters:
    //   parameterName:
    //     The name of the System.Data.Common.DbParameter object in the collection.
    //
    //   value:
    //     The new System.Data.Common.DbParameter value.
    //protected abstract void SetParameter(string parameterName, DbParameter value);
  }
}

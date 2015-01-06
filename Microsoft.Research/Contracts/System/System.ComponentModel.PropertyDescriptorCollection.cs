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

#if !SILVERLIGHT

using System;
using System.Collections;
using System.Reflection;

namespace System.ComponentModel
{
  // Summary:
  //     Represents a collection of System.ComponentModel.PropertyDescriptor objects.
  public class PropertyDescriptorCollection //: IList, IDictionary, ICollection, IEnumerable
  {
    // Summary:
    //     Specifies an empty collection that you can use instead of creating a new
    //     one with no items. This static field is read-only.
    //public static readonly PropertyDescriptorCollection Empty;

    // Summary:
    //     Initializes a new instance of the System.ComponentModel.PropertyDescriptorCollection
    //     class.
    //
    // Parameters:
    //   properties:
    //     An array of type System.ComponentModel.PropertyDescriptor that provides the
    //     properties for this collection.
    //public PropertyDescriptorCollection(PropertyDescriptor[] properties);
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.PropertyDescriptorCollection
    //     class, which is optionally read-only.
    //
    // Parameters:
    //   properties:
    //     An array of type System.ComponentModel.PropertyDescriptor that provides the
    //     properties for this collection.
    //
    //   readOnly:
    //     If true, specifies that the collection cannot be modified.
    //public PropertyDescriptorCollection(PropertyDescriptor[] properties, bool readOnly);

    // Summary:
    //     Gets the number of property descriptors in the collection.
    //
    // Returns:
    //     The number of property descriptors in the collection.
    //public int Count { get; }

    // Summary:
    //     Gets or sets the System.ComponentModel.PropertyDescriptor at the specified
    //     index number.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the System.ComponentModel.PropertyDescriptor to get
    //     or set.
    //
    // Returns:
    //     The System.ComponentModel.PropertyDescriptor with the specified index number.
    //
    // Exceptions:
    //   System.IndexOutOfRangeException:
    //     The index parameter is not a valid index for System.ComponentModel.PropertyDescriptorCollection.this[System.Int32].
    //public virtual PropertyDescriptor this[int index] { get; }
    //
    // Summary:
    //     Gets or sets the System.ComponentModel.PropertyDescriptor with the specified
    //     name.
    //
    // Parameters:
    //   name:
    //     The name of the System.ComponentModel.PropertyDescriptor to get from the
    //     collection.
    //
    // Returns:
    //     The System.ComponentModel.PropertyDescriptor with the specified name, or
    //     null if the property does not exist.
    //public virtual PropertyDescriptor this[string name] { get; }

    // Summary:
    //     Adds the specified System.ComponentModel.PropertyDescriptor to the collection.
    //
    // Parameters:
    //   value:
    //     The System.ComponentModel.PropertyDescriptor to add to the collection.
    //
    // Returns:
    //     The index of the System.ComponentModel.PropertyDescriptor that was added
    //     to the collection.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    //public int Add(PropertyDescriptor value);
    //
    // Summary:
    //     Removes all System.ComponentModel.PropertyDescriptor objects from the collection.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    //public void Clear();
    //
    // Summary:
    //     Returns whether the collection contains the given System.ComponentModel.PropertyDescriptor.
    //
    // Parameters:
    //   value:
    //     The System.ComponentModel.PropertyDescriptor to find in the collection.
    //
    // Returns:
    //     true if the collection contains the given System.ComponentModel.PropertyDescriptor;
    //     otherwise, false.
    //public bool Contains(PropertyDescriptor value);
    //
    // Summary:
    //     Copies the entire collection to an array, starting at the specified index
    //     number.
    //
    // Parameters:
    //   array:
    //     An array of System.ComponentModel.PropertyDescriptor objects to copy elements
    //     of the collection to.
    //
    //   index:
    //     The index of the array parameter at which copying begins.
    //public void CopyTo(Array array, int index);
    //
    // Summary:
    //     Returns the System.ComponentModel.PropertyDescriptor with the specified name,
    //     using a Boolean to indicate whether to ignore case.
    //
    // Parameters:
    //   name:
    //     The name of the System.ComponentModel.PropertyDescriptor to return from the
    //     collection.
    //
    //   ignoreCase:
    //     true if you want to ignore the case of the property name; otherwise, false.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptor with the specified name, or null
    //     if the property does not exist.
    //public virtual PropertyDescriptor Find(string name, bool ignoreCase);
    //
    // Summary:
    //     Returns an enumerator for this class.
    //
    // Returns:
    //     An enumerator of type System.Collections.IEnumerator.
    //public virtual IEnumerator GetEnumerator();
    //
    // Summary:
    //     Returns the index of the given System.ComponentModel.PropertyDescriptor.
    //
    // Parameters:
    //   value:
    //     The System.ComponentModel.PropertyDescriptor to return the index of.
    //
    // Returns:
    //     The index of the given System.ComponentModel.PropertyDescriptor.
    //public int IndexOf(PropertyDescriptor value);
    //
    // Summary:
    //     Adds the System.ComponentModel.PropertyDescriptor to the collection at the
    //     specified index number.
    //
    // Parameters:
    //   index:
    //     The index at which to add the value parameter to the collection.
    //
    //   value:
    //     The System.ComponentModel.PropertyDescriptor to add to the collection.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    //public void Insert(int index, PropertyDescriptor value);
    //
    // Summary:
    //     Sorts the members of this collection, using the specified System.Collections.IComparer.
    //
    // Parameters:
    //   sorter:
    //     A comparer to use to sort the System.ComponentModel.PropertyDescriptor objects
    //     in this collection.
    //protected void InternalSort(IComparer sorter);
    //
    // Summary:
    //     Sorts the members of this collection. The specified order is applied first,
    //     followed by the default sort for this collection, which is usually alphabetical.
    //
    // Parameters:
    //   names:
    //     An array of strings describing the order in which to sort the System.ComponentModel.PropertyDescriptor
    //     objects in this collection.
    //protected void InternalSort(string[] names);
    //
    // Summary:
    //     Removes the specified System.ComponentModel.PropertyDescriptor from the collection.
    //
    // Parameters:
    //   value:
    //     The System.ComponentModel.PropertyDescriptor to remove from the collection.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    //public void Remove(PropertyDescriptor value);
    //
    // Summary:
    //     Removes the System.ComponentModel.PropertyDescriptor at the specified index
    //     from the collection.
    //
    // Parameters:
    //   index:
    //     The index of the System.ComponentModel.PropertyDescriptor to remove from
    //     the collection.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    //public void RemoveAt(int index);
    //
    // Summary:
    //     Sorts the members of this collection, using the default sort for this collection,
    //     which is usually alphabetical.
    //
    // Returns:
    //     A new System.ComponentModel.PropertyDescriptorCollection that contains the
    //     sorted System.ComponentModel.PropertyDescriptor objects.
    //public virtual PropertyDescriptorCollection Sort();
    //
    // Summary:
    //     Sorts the members of this collection, using the specified System.Collections.IComparer.
    //
    // Parameters:
    //   comparer:
    //     A comparer to use to sort the System.ComponentModel.PropertyDescriptor objects
    //     in this collection.
    //
    // Returns:
    //     A new System.ComponentModel.PropertyDescriptorCollection that contains the
    //     sorted System.ComponentModel.PropertyDescriptor objects.
    //public virtual PropertyDescriptorCollection Sort(IComparer comparer);
    //
    // Summary:
    //     Sorts the members of this collection. The specified order is applied first,
    //     followed by the default sort for this collection, which is usually alphabetical.
    //
    // Parameters:
    //   names:
    //     An array of strings describing the order in which to sort the System.ComponentModel.PropertyDescriptor
    //     objects in this collection.
    //
    // Returns:
    //     A new System.ComponentModel.PropertyDescriptorCollection that contains the
    //     sorted System.ComponentModel.PropertyDescriptor objects.
    //public virtual PropertyDescriptorCollection Sort(string[] names);
    //
    // Summary:
    //     Sorts the members of this collection. The specified order is applied first,
    //     followed by the sort using the specified System.Collections.IComparer.
    //
    // Parameters:
    //   names:
    //     An array of strings describing the order in which to sort the System.ComponentModel.PropertyDescriptor
    //     objects in this collection.
    //
    //   comparer:
    //     A comparer to use to sort the System.ComponentModel.PropertyDescriptor objects
    //     in this collection.
    //
    // Returns:
    //     A new System.ComponentModel.PropertyDescriptorCollection that contains the
    //     sorted System.ComponentModel.PropertyDescriptor objects.
    //public virtual PropertyDescriptorCollection Sort(string[] names, IComparer comparer);
  }
}

#endif
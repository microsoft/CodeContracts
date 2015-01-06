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
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
  // Summary:
  //     Represents a collection of System.ComponentModel.EventDescriptor objects.
  //[ComVisible(true)]
  public class EventDescriptorCollection //: IList, ICollection, IEnumerable
  {
#if false
    // Summary:
    //     Specifies an empty collection to use, rather than creating a new one with
    //     no items. This static field is read-only.
    public static readonly EventDescriptorCollection Empty;

    // Summary:
    //     Initializes a new instance of the System.ComponentModel.EventDescriptorCollection
    //     class with the given array of System.ComponentModel.EventDescriptor objects.
    //
    // Parameters:
    //   events:
    //     An array of type System.ComponentModel.EventDescriptor that provides the
    //     events for this collection.
    public EventDescriptorCollection(EventDescriptor[] events);
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.EventDescriptorCollection
    //     class with the given array of System.ComponentModel.EventDescriptor objects.
    //     The collection is optionally read-only.
    //
    // Parameters:
    //   events:
    //     An array of type System.ComponentModel.EventDescriptor that provides the
    //     events for this collection.
    //
    //   readOnly:
    //     true to specify a read-only collection; otherwise, false.
    public EventDescriptorCollection(EventDescriptor[] events, bool readOnly);

    // Summary:
    //     Gets the number of event descriptors in the collection.
    //
    // Returns:
    //     The number of event descriptors in the collection.
    public int Count { get; }

    // Summary:
    //     Gets or sets the event with the specified index number.
    //
    // Parameters:
    //   index:
    //     The zero-based index number of the System.ComponentModel.EventDescriptor
    //     to get or set.
    //
    // Returns:
    //     The System.ComponentModel.EventDescriptor with the specified index number.
    //
    // Exceptions:
    //   System.IndexOutOfRangeException:
    //     index is not a valid index for System.ComponentModel.EventDescriptorCollection.this[System.Int32].
    public virtual EventDescriptor this[int index] { get; }
    //
    // Summary:
    //     Gets or sets the event with the specified name.
    //
    // Parameters:
    //   name:
    //     The name of the System.ComponentModel.EventDescriptor to get or set.
    //
    // Returns:
    //     The System.ComponentModel.EventDescriptor with the specified name, or null
    //     if the event does not exist.
    public virtual EventDescriptor this[string name] { get; }

    // Summary:
    //     Adds an System.ComponentModel.EventDescriptor to the end of the collection.
    //
    // Parameters:
    //   value:
    //     An System.ComponentModel.EventDescriptor to add to the collection.
    //
    // Returns:
    //     The position of the System.ComponentModel.EventDescriptor within the collection.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    public int Add(EventDescriptor value);
    //
    // Summary:
    //     Removes all objects from the collection.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    public void Clear();
    //
    // Summary:
    //     Returns whether the collection contains the given System.ComponentModel.EventDescriptor.
    //
    // Parameters:
    //   value:
    //     The System.ComponentModel.EventDescriptor to find within the collection.
    //
    // Returns:
    //     true if the collection contains the value parameter given; otherwise, false.
    public bool Contains(EventDescriptor value);
    //
    // Summary:
    //     Gets the description of the event with the specified name in the collection.
    //
    // Parameters:
    //   name:
    //     The name of the event to get from the collection.
    //
    //   ignoreCase:
    //     true if you want to ignore the case of the event; otherwise, false.
    //
    // Returns:
    //     The System.ComponentModel.EventDescriptor with the specified name, or null
    //     if the event does not exist.
    public virtual EventDescriptor Find(string name, bool ignoreCase);
    //
    // Summary:
    //     Gets an enumerator for this System.ComponentModel.EventDescriptorCollection.
    //
    // Returns:
    //     An enumerator that implements System.Collections.IEnumerator.
    public IEnumerator GetEnumerator();
    //
    // Summary:
    //     Returns the index of the given System.ComponentModel.EventDescriptor.
    //
    // Parameters:
    //   value:
    //     The System.ComponentModel.EventDescriptor to find within the collection.
    //
    // Returns:
    //     The index of the given System.ComponentModel.EventDescriptor within the collection.
    public int IndexOf(EventDescriptor value);
    //
    // Summary:
    //     Inserts an System.ComponentModel.EventDescriptor to the collection at a specified
    //     index.
    //
    // Parameters:
    //   index:
    //     The index within the collection in which to insert the value parameter.
    //
    //   value:
    //     An System.ComponentModel.EventDescriptor to insert into the collection.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    public void Insert(int index, EventDescriptor value);
    //
    // Summary:
    //     Sorts the members of this System.ComponentModel.EventDescriptorCollection,
    //     using the specified System.Collections.IComparer.
    //
    // Parameters:
    //   sorter:
    //     A comparer to use to sort the System.ComponentModel.EventDescriptor objects
    //     in this collection.
    protected void InternalSort(IComparer sorter);
    //
    // Summary:
    //     Sorts the members of this System.ComponentModel.EventDescriptorCollection.
    //     The specified order is applied first, followed by the default sort for this
    //     collection, which is usually alphabetical.
    //
    // Parameters:
    //   names:
    //     An array of strings describing the order in which to sort the System.ComponentModel.EventDescriptor
    //     objects in this collection.
    protected void InternalSort(string[] names);
    //
    // Summary:
    //     Removes the specified System.ComponentModel.EventDescriptor from the collection.
    //
    // Parameters:
    //   value:
    //     The System.ComponentModel.EventDescriptor to remove from the collection.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    public void Remove(EventDescriptor value);
    //
    // Summary:
    //     Removes the System.ComponentModel.EventDescriptor at the specified index
    //     from the collection.
    //
    // Parameters:
    //   index:
    //     The index of the System.ComponentModel.EventDescriptor to remove.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    public void RemoveAt(int index);
    //
    // Summary:
    //     Sorts the members of this System.ComponentModel.EventDescriptorCollection,
    //     using the default sort for this collection, which is usually alphabetical.
    //
    // Returns:
    //     The new System.ComponentModel.EventDescriptorCollection.
    public virtual EventDescriptorCollection Sort();
    //
    // Summary:
    //     Sorts the members of this System.ComponentModel.EventDescriptorCollection,
    //     using the specified System.Collections.IComparer.
    //
    // Parameters:
    //   comparer:
    //     An System.Collections.IComparer to use to sort the System.ComponentModel.EventDescriptor
    //     objects in this collection.
    //
    // Returns:
    //     The new System.ComponentModel.EventDescriptorCollection.
    public virtual EventDescriptorCollection Sort(IComparer comparer);
    //
    // Summary:
    //     Sorts the members of this System.ComponentModel.EventDescriptorCollection,
    //     given a specified sort order.
    //
    // Parameters:
    //   names:
    //     An array of strings describing the order in which to sort the System.ComponentModel.EventDescriptor
    //     objects in the collection.
    //
    // Returns:
    //     The new System.ComponentModel.EventDescriptorCollection.
    public virtual EventDescriptorCollection Sort(string[] names);
    //
    // Summary:
    //     Sorts the members of this System.ComponentModel.EventDescriptorCollection,
    //     given a specified sort order and an System.Collections.IComparer.
    //
    // Parameters:
    //   names:
    //     An array of strings describing the order in which to sort the System.ComponentModel.EventDescriptor
    //     objects in the collection.
    //
    //   comparer:
    //     An System.Collections.IComparer to use to sort the System.ComponentModel.EventDescriptor
    //     objects in this collection.
    //
    // Returns:
    //     The new System.ComponentModel.EventDescriptorCollection.
    public virtual EventDescriptorCollection Sort(string[] names, IComparer comparer);
#endif
  }
}

#endif
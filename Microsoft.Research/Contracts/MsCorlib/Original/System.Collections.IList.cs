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

namespace System.Collections
{
    public interface IList : ICollection, IEnumerable
    {
    // Summary:
    //     Gets a value indicating whether the System.Collections.IList has a fixed
    //     size.
    //
    // Returns:
    //     true if the System.Collections.IList has a fixed size; otherwise, false.
    bool IsFixedSize { get; }
    //
    // Summary:
    //     Gets a value indicating whether the System.Collections.IList is read-only.
    //
    // Returns:
    //     true if the System.Collections.IList is read-only; otherwise, false.
    bool IsReadOnly { get; }

    // Summary:
    //     Gets or sets the element at the specified index.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the element to get or set.
    //
    // Returns:
    //     The element at the specified index.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is not a valid index in the System.Collections.IList.
    //
    //   System.NotSupportedException:
    //     The property is set and the System.Collections.IList is read-only.
    object this[int index] { get; set; }

    // Summary:
    //     Adds an item to the System.Collections.IList.
    //
    // Parameters:
    //   value:
    //     The System.Object to add to the System.Collections.IList.
    //
    // Returns:
    //     The position into which the new element was inserted.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The System.Collections.IList is read-only.-or- The System.Collections.IList
    //     has a fixed size.
    [WriteConfined]
    int Add([Escapes(true,false)] object value);
    //
    // Summary:
    //     Removes all items from the System.Collections.IList.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The System.Collections.IList is read-only.
    void Clear();
    //
    // Summary:
    //     Determines whether the System.Collections.IList contains a specific value.
    //
    // Parameters:
    //   value:
    //     The System.Object to locate in the System.Collections.IList.
    //
    // Returns:
    //     true if the System.Object is found in the System.Collections.IList; otherwise,
    //     false.
    [Pure][Reads(ReadsAttribute.Reads.Owned)]
    bool Contains(object value);
    //
    // Summary:
    //     Determines the index of a specific item in the System.Collections.IList.
    //
    // Parameters:
    //   value:
    //     The System.Object to locate in the System.Collections.IList.
    //
    // Returns:
    //     The index of value if found in the list; otherwise, -1.
    int IndexOf(object value);
    //
    // Summary:
    //     Inserts an item to the System.Collections.IList at the specified index.
    //
    // Parameters:
    //   value:
    //     The System.Object to insert into the System.Collections.IList.
    //
    //   index:
    //     The zero-based index at which value should be inserted.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is not a valid index in the System.Collections.IList.
    //
    //   System.NotSupportedException:
    //     The System.Collections.IList is read-only.-or- The System.Collections.IList
    //     has a fixed size.
    //
    //   System.NullReferenceException:
    //     value is null reference in the System.Collections.IList.
    void Insert(int index, object value);
    //
    // Summary:
    //     Removes the first occurrence of a specific object from the System.Collections.IList.
    //
    // Parameters:
    //   value:
    //     The System.Object to remove from the System.Collections.IList.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The System.Collections.IList is read-only.-or- The System.Collections.IList
    //     has a fixed size.
    void Remove(object value);
    //
    // Summary:
    //     Removes the System.Collections.IList item at the specified index.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the item to remove.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is not a valid index in the System.Collections.IList.
    //
    //   System.NotSupportedException:
    //     The System.Collections.IList is read-only.-or- The System.Collections.IList
    //     has a fixed size.
    void RemoveAt(int index);
    }
}

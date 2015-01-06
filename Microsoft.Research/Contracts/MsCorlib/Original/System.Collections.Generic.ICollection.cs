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
using System.Runtime.CompilerServices;
using System.Diagnostics.Contracts;

namespace System.Collections.Generic {
  // Summary:
  //     Defines methods to manipulate generic collections.
  public interface ICollection<T> : IEnumerable<T>, IEnumerable {
    // Summary:
    //     Gets the number of elements contained in the System.Collections.Generic.ICollection<T>.
    //
    // Returns:
    //     The number of elements contained in the System.Collections.Generic.ICollection<T>.
    int Count { get; }
    //
    // Summary:
    //     Gets a value indicating whether the System.Collections.Generic.ICollection<T>
    //     is read-only.
    //
    // Returns:
    //     true if the System.Collections.Generic.ICollection<T> is read-only; otherwise,
    //     false.
    bool IsReadOnly { get; }

    // Summary:
    //     Adds an item to the System.Collections.Generic.ICollection<T>.
    //
    // Parameters:
    //   item:
    //     The object to add to the System.Collections.Generic.ICollection<T>.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The System.Collections.Generic.ICollection<T> is read-only.
    void Add(T item);
      CodeContract.Requires(!this.IsReadOnly);
    //
    // Summary:
    //     Removes all items from the System.Collections.Generic.ICollection<T>.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The System.Collections.Generic.ICollection<T> is read-only.
    void Clear();
      CodeContract.Requires(!this.IsReadOnly);
    //
    // Summary:
    //     Determines whether the System.Collections.Generic.ICollection<T> contains
    //     a specific value.
    //
    // Parameters:
    //   item:
    //     The object to locate in the System.Collections.Generic.ICollection<T>.
    //
    // Returns:
    //     true if item is found in the System.Collections.Generic.ICollection<T>; otherwise,
    //     false.
    [Pure][Reads(ReadsAttribute.Reads.Owned)]
    bool Contains(T item);
    //
    // Summary:
    //     Copies the elements of the System.Collections.Generic.ICollection<T> to an
    //     System.Array, starting at a particular System.Array index.
    //
    // Parameters:
    //   array:
    //     The one-dimensional System.Array that is the destination of the elements
    //     copied from System.Collections.Generic.ICollection<T>. The System.Array must
    //     have zero-based indexing.
    //
    //   arrayIndex:
    //     The zero-based index in array at which copying begins.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     arrayIndex is less than 0.
    //
    //   System.ArgumentNullException:
    //     array is null.
    //
    //   System.ArgumentException:
    //     array is multidimensional.-or-arrayIndex is equal to or greater than the
    //     length of array.-or-The number of elements in the source System.Collections.Generic.ICollection<T>
    //     is greater than the available space from arrayIndex to the end of the destination
    //     array.-or-Type T cannot be cast automatically to the type of the destination
    //     array.
    void CopyTo(T[]! array, int arrayIndex);
    //
    // Summary:
    //     Removes the first occurrence of a specific object from the System.Collections.Generic.ICollection<T>.
    //
    // Parameters:
    //   item:
    //     The object to remove from the System.Collections.Generic.ICollection<T>.
    //
    // Returns:
    //     true if item was successfully removed from the System.Collections.Generic.ICollection<T>;
    //     otherwise, false. This method also returns false if item is not found in
    //     the original System.Collections.Generic.ICollection<T>.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The System.Collections.Generic.ICollection<T> is read-only.
    bool Remove(T item);
      CodeContract.Requires(!this.IsReadOnly);
  }
}

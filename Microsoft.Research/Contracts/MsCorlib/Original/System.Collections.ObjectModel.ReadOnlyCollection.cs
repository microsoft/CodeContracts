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
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Collections.ObjectModel {
  public class ReadOnlyCollection<T> {
    // Summary:
    //     Initializes a new instance of the System.Collections.ObjectModel.ReadOnlyCollection<T>
    //     class that is a read-only wrapper around the specified list.
    //
    // Parameters:
    //   list:
    //     The list to wrap.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     list is null.
    public ReadOnlyCollection(IList<T> list) {

    // Summary:
    //     Gets the number of elements contained in the System.Collections.ObjectModel.ReadOnlyCollection<T>
    //     instance.
    //
    // Returns:
    //     The number of elements contained in the System.Collections.ObjectModel.ReadOnlyCollection<T>
    //     instance.
      CodeContract.Ensures(CodeContract.Result<ReadOnlyCollection(IList<T>>() != null);
      return default(ReadOnlyCollection(IList<T>);
    }
    public int Count { get; }
    //
    // Summary:
    //     Returns the System.Collections.Generic.IList<T> that the System.Collections.ObjectModel.ReadOnlyCollection<T>
    //     wraps.
    //
    // Returns:
    //     The System.Collections.Generic.IList<T> that the System.Collections.ObjectModel.ReadOnlyCollection<T>
    //     wraps.
    protected IList<T>! Items { get; }

    // Summary:
    //     Gets the element at the specified index.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the element to get.
    //
    // Returns:
    //     The element at the specified index.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero.-or-index is equal to or greater than System.Collections.ObjectModel.ReadOnlyCollection<T>.Count.
    public T this[int index] { get; }

    // Summary:
    //     Determines whether an element is in the System.Collections.ObjectModel.ReadOnlyCollection<T>.
    //
    // Parameters:
    //   value:
    //     The object to locate in the System.Collections.ObjectModel.ReadOnlyCollection<T>.
    //     The value can be null for reference types.
    //
    // Returns:
    //     true if value is found in the System.Collections.ObjectModel.ReadOnlyCollection<T>;
    //     otherwise, false.
    public bool Contains(T value) {
    //
    // Summary:
    //     Copies the entire System.Collections.ObjectModel.ReadOnlyCollection<T> to
    //     a compatible one-dimensional System.Array, starting at the specified index
    //     of the target array.
    //
    // Parameters:
    //   array:
    //     The one-dimensional System.Array that is the destination of the elements
    //     copied from System.Collections.ObjectModel.ReadOnlyCollection<T>. The System.Array
    //     must have zero-based indexing.
    //
    //   index:
    //     The zero-based index in array at which copying begins.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     index is equal to or greater than the length of array.-or-The number of elements
    //     in the source System.Collections.ObjectModel.ReadOnlyCollection<T> is greater
    //     than the available space from index to the end of the destination array.
    //
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero.
    //
    //   System.ArgumentNullException:
    //     array is null.
      return default(bool);
    }
    public void CopyTo(T[]! array, int index) {
    //
    // Summary:
    //     Returns an enumerator that iterates through the System.Collections.ObjectModel.ReadOnlyCollection<T>.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerator<T> for the System.Collections.ObjectModel.ReadOnlyCollection<T>.
    }
    [Pure] [GlobalAccess(false)] [Escapes(true,false)]
    public IEnumerator<T> GetEnumerator() {
      CodeContract.Ensures(result.IsNew);
    //
    // Summary:
    //     Searches for the specified object and returns the zero-based index of the
    //     first occurrence within the entire System.Collections.ObjectModel.ReadOnlyCollection<T>.
    //
    // Parameters:
    //   value:
    //     The object to locate in the System.Collections.Generic.List<T>. The value
    //     can be null for reference types.
    //
    // Returns:
    //     The zero-based index of the first occurrence of item within the entire System.Collections.ObjectModel.ReadOnlyCollection<T>,
    //     if found; otherwise, -1.
      CodeContract.Ensures(CodeContract.Result<IEnumerator<T>>() != null);
      return default(IEnumerator<T>);
    }
    public int IndexOf(T value) {
      return default(int);
    }
  }
}

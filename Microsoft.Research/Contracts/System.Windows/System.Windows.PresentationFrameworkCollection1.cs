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

#region Assembly System.Windows.dll, v2.0.50727
// C:\Program Files\Reference Assemblies\Microsoft\Framework\Silverlight\v4.0\Profile\WindowsPhone\System.Windows.dll
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics.Contracts;

namespace System.Windows
{
  // Summary:
  //     Provides a common collection class for Silverlight collections.
  //
  // Type parameters:
  //   T:
  //     Type constraint for type safety of the constrained collection implementation.
  public abstract class PresentationFrameworkCollection<T> : DependencyObject, IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable
  {
    internal PresentationFrameworkCollection() { }

    // Summary:
    //     Identifies the System.Windows.PresentationFrameworkCollection<T>.Count dependency
    //     property.
    //
    // Returns:
    //     The identifier for the System.Windows.PresentationFrameworkCollection<T>.Count
    //     dependency property.
    public static readonly DependencyProperty CountProperty;

    // Summary:
    //     Gets the number of elements contained in the System.Windows.PresentationFrameworkCollection<T>.
    //
    // Returns:
    //     The number of elements contained in the System.Windows.PresentationFrameworkCollection<T>.
    extern public int Count { get; }
    //
    // Summary:
    //     Gets a value indicating whether the System.Windows.PresentationFrameworkCollection<T>
    //     has a fixed size.
    //
    // Returns:
    //     true if the System.Windows.PresentationFrameworkCollection<T> has a fixed
    //     size; otherwise, false.
    extern public bool IsFixedSize { get; }
    //
    // Summary:
    //     Gets a value indicating whether the System.Windows.PresentationFrameworkCollection<T>
    //     is read-only.
    //
    // Returns:
    //     true if the System.Windows.PresentationFrameworkCollection<T> is read-only;
    //     otherwise, false.
    extern public bool IsReadOnly { get; }
    //
    // Summary:
    //     Gets a value indicating whether access to the System.Windows.PresentationFrameworkCollection<T>
    //     is synchronized (thread safe).
    //
    // Returns:
    //     true if access to the System.Windows.PresentationFrameworkCollection<T> is
    //     synchronized (thread safe); otherwise, false.
    extern public bool IsSynchronized { get; }
    //
    // Summary:
    //     Gets an object that can be used to synchronize access to the System.Windows.PresentationFrameworkCollection<T>
    //     .
    //
    // Returns:
    //     An object that can be used to synchronize access to the System.Windows.PresentationFrameworkCollection<T>.
    extern public object SyncRoot { get; }

    // Summary:
    //     Gets or sets the element at the specified index.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the element to get or set.
    //
    // Returns:
    //     The element at the specified index.
    extern public T this[int index] { get; set; }

    // Summary:
    //     Adds an item to the System.Windows.PresentationFrameworkCollection<T>.
    //
    // Parameters:
    //   value:
    //     The object to add.
    extern public void Add(T value);
    //
    // Summary:
    //     Removes all items from the System.Windows.PresentationFrameworkCollection<T>.
    extern public void Clear();
    //
    // Summary:
    //     Determines whether the System.Windows.PresentationFrameworkCollection<T>
    //     contains a specific value.
    //
    // Parameters:
    //   value:
    //     The object to locate in the System.Windows.PresentationFrameworkCollection<T>.
    //
    // Returns:
    //     true if the object is found in the System.Windows.PresentationFrameworkCollection<T>;
    //     otherwise, false.
    extern public bool Contains(T value);
    //
    // Summary:
    //     Copies the elements of the System.Windows.PresentationFrameworkCollection<T>
    //     to an System.Array, starting at a particular System.Array index.
    //
    // Parameters:
    //   array:
    //     The one-dimensional System.Array that is the destination of the elements
    //     copied from the System.Windows.PresentationFrameworkCollection<T>. The System.Array
    //     must have zero-based indexing.
    //
    //   index:
    //     The zero-based index in array at which copying begins.
    extern public void CopyTo(Array array, int index);
    //
    // Summary:
    //     Copies the elements of the System.Windows.PresentationFrameworkCollection<T>
    //     to an System.Array, starting at a particular System.Array index.
    //
    // Parameters:
    //   array:
    //     The one-dimensional System.Array that is the destination of the elements
    //     copied from the System.Windows.PresentationFrameworkCollection<T>. The System.Array
    //     must have zero-based indexing.
    //
    //   index:
    //     The zero-based index in array at which copying begins.
    extern public void CopyTo(T[] array, int index);
    //
    // Summary:
    //     Returns an enumerator that iterates through a collection.
    //
    // Returns:
    //     An System.Collections.IEnumerator object that can be used to iterate through
    //     the collection.
    extern public IEnumerator<T> GetEnumerator();
    //
    // Summary:
    //     Determines the index of a specific item in the System.Windows.PresentationFrameworkCollection<T>.
    //
    // Parameters:
    //   value:
    //     The object to locate in the System.Windows.PresentationFrameworkCollection<T>.
    //
    // Returns:
    //     The index of value if found in the list; otherwise, -1.
    extern public int IndexOf(T value);
    //
    // Summary:
    //     Inserts an item to the System.Windows.PresentationFrameworkCollection<T>
    //     at the specified index.
    //
    // Parameters:
    //   index:
    //     The zero-based index at which value should be inserted.
    //
    //   value:
    //     The object to insert into the System.Windows.PresentationFrameworkCollection<T>.
    extern public void Insert(int index, T value);
    //
    // Summary:
    //     Removes the first occurrence of a specific object from the System.Windows.PresentationFrameworkCollection<T>.
    //
    // Parameters:
    //   value:
    //     The object to remove from the System.Windows.PresentationFrameworkCollection<T>.
    //
    // Returns:
    //     true if an object was removed; otherwise, false.
    extern public bool Remove(T value);
    //
    // Summary:
    //     Removes the item at the specified index.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the item to remove.
    extern public void RemoveAt(int index);

    #region IList Members

    extern int System.Collections.IList.Add(object value);

    extern bool System.Collections.IList.Contains(object value);

    extern int System.Collections.IList.IndexOf(object value);

    extern void System.Collections.IList.Insert(int index, object value);

    extern void System.Collections.IList.Remove(object value);

    object IList.this[int index]
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    #endregion

    #region IEnumerable Members

    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}

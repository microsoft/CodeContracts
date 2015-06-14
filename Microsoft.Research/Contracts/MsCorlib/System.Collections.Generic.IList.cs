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
using System.Reflection;
using System.Diagnostics.Contracts;
//using System.Runtime.CompilerServices;

namespace System.Collections.Generic {
  // Summary:
  //     Represents a collection of objects that can be individually accessed by index.
  //
  // Type parameters:
  //   T:
  //     The type of elements in the list.
  [ContractClass(typeof(IListContract<>))]
  public interface IList<T> : ICollection<T> {
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
    //     index is not a valid index in the System.Collections.Generic.IList<T>.
    //
    //   System.NotSupportedException:
    //     The property is set and the System.Collections.Generic.IList<T> is read-only.
    T this[int index] { get; set; }

    // Summary:
    //     Determines the index of a specific item in the System.Collections.Generic.IList<T>.
    //
    // Parameters:
    //   item:
    //     The object to locate in the System.Collections.Generic.IList<T>.
    //
    // Returns:
    //     The index of item if found in the list; otherwise, -1.
    [Pure]
    int IndexOf(T item);
    //
    // Summary:
    //     Inserts an item to the System.Collections.Generic.IList<T> at the specified
    //     index.
    //
    // Parameters:
    //   index:
    //     The zero-based index at which item should be inserted.
    //
    //   item:
    //     The object to insert into the System.Collections.Generic.IList<T>.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is not a valid index in the System.Collections.Generic.IList<T>.
    //
    //   System.NotSupportedException:
    //     The System.Collections.Generic.IList<T> is read-only.
    void Insert(int index, T item);
    //
    // Summary:
    //     Removes the System.Collections.Generic.IList<T> item at the specified index.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the item to remove.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is not a valid index in the System.Collections.Generic.IList<T>.
    //
    //   System.NotSupportedException:
    //     The System.Collections.Generic.IList<T> is read-only.
    void RemoveAt(int index);
  }

  [ContractClassFor(typeof(IList<>))]
  abstract class IListContract<T> : IList<T>
  {
    #region IList<T> Members

    T IList<T>.this[int index]
    {
      get
      {
        Contract.Requires(index >= 0);
        Contract.Requires(index < this.Count);
        return default(T);
      }
      set
      {
        Contract.Requires(index >= 0);
        Contract.Requires(index < this.Count);
      }
    }

    [Pure]
    int IList<T>.IndexOf(T item)
    {
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < this.Count);

      throw new NotImplementedException();
    }

    void IList<T>.Insert(int index, T item)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index <= this.Count);
    }

    void IList<T>.RemoveAt(int index)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index < this.Count);

      Contract.Ensures(this.Count == Contract.OldValue(this.Count) - 1);
    }

    #endregion


    #region ICollection<T> Members

    public int Count
    {
      get { throw new NotImplementedException(); }
    }

    bool ICollection<T>.IsReadOnly
    {
      get { throw new NotImplementedException(); }
    }

    void ICollection<T>.Add(T item)
    {
      // Contract.Ensures(Count == Contract.OldValue(Count) + 1); // cannot be seen by our tools as there is no IList<T>.Add
      throw new NotImplementedException();
    }

    void ICollection<T>.Clear()
    {
      throw new NotImplementedException();
    }

    bool ICollection<T>.Contains(T item)
    {
      throw new NotImplementedException();
    }

    void ICollection<T>.CopyTo(T[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }

    bool ICollection<T>.Remove(T item)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable<T> Members

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable Members

    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable Members

    public object[] Model {
      get { throw new NotImplementedException(); }
    }

    #endregion
  }

}

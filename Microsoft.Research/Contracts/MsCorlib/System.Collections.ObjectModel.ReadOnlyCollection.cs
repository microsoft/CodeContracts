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
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Collections.ObjectModel
{
  public class ReadOnlyCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, System.Collections.IList, System.Collections.ICollection, System.Collections.IEnumerable
  {
    /// <summary>
    /// Implementation of ICollection.get_Count
    /// </summary>
    public extern virtual int Count { get; }

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
    public ReadOnlyCollection(IList<T> list)
    {
      Contract.Requires(list != null);
      Contract.Ensures(Count == list.Count);
    }

    //
    // Summary:
    //     Returns the System.Collections.Generic.IList<T> that the System.Collections.ObjectModel.ReadOnlyCollection<T>
    //     wraps.
    //
    // Returns:
    //     The System.Collections.Generic.IList<T> that the System.Collections.ObjectModel.ReadOnlyCollection<T>
    //     wraps.
    protected IList<T> Items
    {
      get
      {
        Contract.Ensures(Contract.Result<IList<T>>() != null);
        return default(IList<T>);
      }
    }

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
    //     index is less than zero.  -or- index is equal to or greater than System.Collections.ObjectModel.ReadOnlyCollection<T>.Count.
#if SILVERLIGHT_5_0 || NETFRAMEWORK_4_5
    virtual
#endif
    public T this[int index] {
      get {
        Contract.Requires(index >= 0);
        Contract.Requires(index < this.Count);
        return default(T);
      }
    }


    #region IList<T> Members

    T System.Collections.Generic.IList<T>.this[int index]
    {
      get
      {
        return default(T);
      }
      set
      {
      }
    }

    public int IndexOf(T item)
    {
      throw new NotImplementedException();
    }

    void System.Collections.Generic.IList<T>.Insert(int index, T item)
    {
      throw new NotImplementedException();
    }

    void System.Collections.Generic.IList<T>.RemoveAt(int index)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region ICollection<T> Members


    bool System.Collections.Generic.ICollection<T>.IsReadOnly
    {
      get { throw new NotImplementedException(); }
    }

    void System.Collections.Generic.ICollection<T>.Add(T item)
    {
      throw new NotImplementedException();
    }


    void System.Collections.Generic.ICollection<T>.Clear()
    {
      throw new NotImplementedException();
    }

    [Pure]
    public bool Contains(T item)
    {
      throw new NotImplementedException();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }

    bool System.Collections.Generic.ICollection<T>.Remove(T item)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable<T> Members

    public IEnumerator<T> GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable Members

    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }

    [ContractModel]
    public object[] Model
    {
      [ContractRuntimeIgnored]
      get { throw new NotImplementedException(); }
    }

    #endregion


    #region IEnumerable Members

    [ContractModel]
    object[] IEnumerable.Model
    {
      [ContractRuntimeIgnored]
      get { throw new NotImplementedException(); }
    }

    #endregion


    #region IList Members

    bool System.Collections.IList.IsFixedSize
    {
      get { throw new NotImplementedException(); }
    }

    bool System.Collections.IList.IsReadOnly
    {
      get { throw new NotImplementedException(); }
    }

    object System.Collections.IList.this[int index]
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

    int System.Collections.IList.Add(object value)
    {
      throw new NotImplementedException();
    }

    void System.Collections.IList.Clear()
    {
      throw new NotImplementedException();
    }

    bool System.Collections.IList.Contains(object value)
    {
      throw new NotImplementedException();
    }

    int System.Collections.IList.IndexOf(object value)
    {
      throw new NotImplementedException();
    }

    void System.Collections.IList.Insert(int index, object value)
    {
      throw new NotImplementedException();
    }

    void System.Collections.IList.Remove(object value)
    {
      throw new NotImplementedException();
    }

    void System.Collections.IList.RemoveAt(int index)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region ICollection Members


    bool System.Collections.ICollection.IsSynchronized
    {
      get { throw new NotImplementedException(); }
    }

    object System.Collections.ICollection.SyncRoot
    {
      get { throw new NotImplementedException(); }
    }

    void System.Collections.ICollection.CopyTo(Array array, int index)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
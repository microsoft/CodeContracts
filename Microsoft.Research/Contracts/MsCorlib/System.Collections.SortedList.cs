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
using System.Diagnostics.Contracts;

namespace System.Collections
{

  public class SortedList : IDictionary
  {

    public object this[object key]
    {
      get {
        Contract.Requires(key != null);
        return default(object);
      }
      set
      {
        Contract.Requires(key != null);
      }
    }

    public virtual int Capacity
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
      set
      {
        Contract.Requires(value >= this.Count);
      }
    }

    extern public bool IsFixedSize
    {
      get;
    }

    extern public ICollection Values
    {
      get;
    }

    extern public object SyncRoot
    {
      get;
    }

    extern public bool IsSynchronized
    {
      get;
    }

    extern public ICollection Keys
    {
      get;
    }

    extern public int Count
    {
      get;
    }

    extern public bool IsReadOnly
    {
      get;
    }

    public virtual void TrimToSize()
    {
    }

    public static SortedList Synchronized(SortedList list)
    {
      Contract.Requires(list != null);

      return default(SortedList);
    }

    public virtual void SetByIndex(int index, object value)
    {
      Contract.Requires(index >= 0);

    }
    public void Remove(object key)
    {

    }

    public virtual void RemoveAt(int index)
    {
      Contract.Requires(index >= 0);

    }

    [Pure]
    public virtual int IndexOfValue(object value)
    {

      return default(int);
    }

    [Pure]
    public virtual int IndexOfKey(object key)
    {
      Contract.Requires(key != null);

      return default(int);
    }
    [Pure]
    public virtual IList GetValueList()
    {
      return default(IList);
    }

    [Pure]
    public virtual IList GetKeyList()
    {
      return default(IList);
    }

    [Pure]
    public virtual object GetKey(int index)
    {
      Contract.Requires(index >= 0);

      return default(object);
    }

    [Pure]
    public IDictionaryEnumerator GetEnumerator()
    {
      Contract.Ensures(Contract.Result<IDictionaryEnumerator>() != null);
      return default(IDictionaryEnumerator);
    }

    [Pure]
    public virtual object GetByIndex(int index)
    {
      Contract.Requires(index >= 0);

      return default(object);
    }

    public void CopyTo(Array array, int arrayIndex)
    {
      Contract.Requires(array != null);
      Contract.Requires(arrayIndex >= 0);

    }

    [Pure]
    public virtual bool ContainsValue(object value)
    {
      return default(bool);
    }

    [Pure]
    public virtual bool ContainsKey(object key)
    {

      return default(bool);
    }

    [Pure]
    public bool Contains(object key)
    {

      return default(bool);
    }

    public void Add(object key, object value)
    {
      Contract.Requires(key != null);
    }

    public SortedList(IDictionary d, IComparer comparer)
    {
      Contract.Requires(d != null);
      Contract.Ensures(this.Count == d.Count);
      Contract.Ensures(this.Capacity == this.Count);
    }

    public SortedList(IDictionary d)
    {
      Contract.Requires(d != null);
      Contract.Ensures(this.Count == d.Count);
      Contract.Ensures(this.Capacity == this.Count);
    }

    public SortedList(IComparer comparer, int capacity)
    {
      Contract.Requires(capacity >= 0);
      Contract.Ensures(this.Count == 0);
    }

    public SortedList(IComparer comparer)
    {
      Contract.Ensures(this.Count == 0);
    }

    public SortedList(int initialCapacity)
    {
      Contract.Requires(initialCapacity >= 0);
      Contract.Ensures(this.Count == 0);
    }


    #region IDictionary Members


    public void Clear()
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

    [ContractModel]
    object[] IEnumerable.Model {
      get { throw new NotImplementedException(); }
    }

    #endregion
  }
}

#endif
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

// File System.Collections.Generic.SortedList_2.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Collections.Generic
{
  public partial class SortedList<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, System.Collections.IDictionary, System.Collections.ICollection, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public void Add(TKey key, TValue value)
    {
    }

    public void Clear()
    {
    }

    public bool ContainsKey(TKey key)
    {
      return default(bool);
    }

    public bool ContainsValue(TValue value)
    {
      return default(bool);
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>>>() != null);

      return default(IEnumerator<KeyValuePair<TKey, TValue>>);
    }

    public int IndexOfKey(TKey key)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public int IndexOfValue(TValue value)
    {
      return default(int);
    }

    public bool Remove(TKey key)
    {
      return default(bool);
    }

    public void RemoveAt(int index)
    {
    }

    public SortedList()
    {
    }

    public SortedList(int capacity, IComparer<TKey> comparer)
    {
    }

    public SortedList(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
    {
      Contract.Requires(dictionary.Keys != null);
    }

    public SortedList(IComparer<TKey> comparer)
    {
    }

    public SortedList(IDictionary<TKey, TValue> dictionary)
    {
      Contract.Requires(dictionary.Keys != null);
    }

    public SortedList(int capacity)
    {
    }

    void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
    {
    }

    bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
    {
      return default(bool);
    }

    void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
    }

    bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
    {
      return default(bool);
    }

    IEnumerator<KeyValuePair<TKey, TValue>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey,TValue>>.GetEnumerator()
    {
      return default(IEnumerator<KeyValuePair<TKey, TValue>>);
    }

    void System.Collections.ICollection.CopyTo(Array array, int arrayIndex)
    {
    }

    void System.Collections.IDictionary.Add(Object key, Object value)
    {
    }

    bool System.Collections.IDictionary.Contains(Object key)
    {
      return default(bool);
    }

    System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator()
    {
      return default(System.Collections.IDictionaryEnumerator);
    }

    void System.Collections.IDictionary.Remove(Object key)
    {
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public void TrimExcess()
    {
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      value = default(TValue);

      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public int Capacity
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 2147483647);

        return default(int);
      }
      set
      {
      }
    }

    public IComparer<TKey> Comparer
    {
      get
      {
        return default(IComparer<TKey>);
      }
    }

    public int Count
    {
      get
      {
        return default(int);
      }
    }

    public TValue this [TKey key]
    {
      get
      {
        return default(TValue);
      }
      set
      {
      }
    }

    public IList<TKey> Keys
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.Generic.IList<TKey>>() != null);

        return default(IList<TKey>);
      }
    }

    bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    ICollection<TKey> System.Collections.Generic.IDictionary<TKey,TValue>.Keys
    {
      get
      {
        return default(ICollection<TKey>);
      }
    }

    ICollection<TValue> System.Collections.Generic.IDictionary<TKey,TValue>.Values
    {
      get
      {
        return default(ICollection<TValue>);
      }
    }

    bool System.Collections.ICollection.IsSynchronized
    {
      get
      {
        return default(bool);
      }
    }

    Object System.Collections.ICollection.SyncRoot
    {
      get
      {
        return default(Object);
      }
    }

    bool System.Collections.IDictionary.IsFixedSize
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Collections.IDictionary.IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    Object System.Collections.IDictionary.this [Object key]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    System.Collections.ICollection System.Collections.IDictionary.Keys
    {
      get
      {
        return default(System.Collections.ICollection);
      }
    }

    System.Collections.ICollection System.Collections.IDictionary.Values
    {
      get
      {
        return default(System.Collections.ICollection);
      }
    }

    public IList<TValue> Values
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.Generic.IList<TValue>>() != null);

        return default(IList<TValue>);
      }
    }
    #endregion
  }
}

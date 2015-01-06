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

// File System.Collections.Generic.Dictionary_2.cs
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
  public partial class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, System.Collections.IDictionary, System.Collections.ICollection, System.Collections.IEnumerable, System.Runtime.Serialization.ISerializable, System.Runtime.Serialization.IDeserializationCallback
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

    public Dictionary()
    {
    }

    public Dictionary(int capacity, IEqualityComparer<TKey> comparer)
    {
    }

    public Dictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(dictionary != null);
    }

    public Dictionary(IDictionary<TKey, TValue> dictionary)
    {
      Contract.Requires(dictionary != null);
    }

    protected Dictionary(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public Dictionary(int capacity)
    {
    }

    public Dictionary(IEqualityComparer<TKey> comparer)
    {
    }

    public System.Collections.Generic.Dictionary<TKey, TValue>.Enumerator GetEnumerator()
    {
      return default(System.Collections.Generic.Dictionary<TKey, TValue>.Enumerator);
    }

    public virtual new void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public virtual new void OnDeserialization(Object sender)
    {
    }

    public bool Remove(TKey key)
    {
      return default(bool);
    }

    void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
    {
    }

    bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
    {
      return default(bool);
    }

    void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
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

    void System.Collections.ICollection.CopyTo(Array array, int index)
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

    public bool TryGetValue(TKey key, out TValue value)
    {
      value = default(TValue);

      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public IEqualityComparer<TKey> Comparer
    {
      get
      {
        return default(IEqualityComparer<TKey>);
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

    public System.Collections.Generic.Dictionary<TKey, TValue>.KeyCollection Keys
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.Generic.Dictionary<TKey, TValue>.KeyCollection>() != null);

        return default(System.Collections.Generic.Dictionary<TKey, TValue>.KeyCollection);
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

    public System.Collections.Generic.Dictionary<TKey, TValue>.ValueCollection Values
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.Generic.Dictionary<TKey, TValue>.ValueCollection>() != null);

        return default(System.Collections.Generic.Dictionary<TKey, TValue>.ValueCollection);
      }
    }
    #endregion
  }
}

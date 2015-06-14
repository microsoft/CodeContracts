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

// File System.Collections.Hashtable.cs
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


namespace System.Collections
{
  public partial class Hashtable : IDictionary, ICollection, IEnumerable, System.Runtime.Serialization.ISerializable, System.Runtime.Serialization.IDeserializationCallback, ICloneable
  {
    #region Methods and constructors
    public virtual new void Add(Object key, Object value)
    {
    }

    public virtual new void Clear()
    {
    }

    public virtual new Object Clone()
    {
      return default(Object);
    }

    public virtual new bool Contains(Object key)
    {
      return default(bool);
    }

    public virtual new bool ContainsKey(Object key)
    {
      return default(bool);
    }

    public virtual new bool ContainsValue(Object value)
    {
      return default(bool);
    }

    public virtual new void CopyTo(Array array, int arrayIndex)
    {
    }

    public virtual new IDictionaryEnumerator GetEnumerator()
    {
      return default(IDictionaryEnumerator);
    }

    protected virtual new int GetHash(Object key)
    {
      return default(int);
    }

    public virtual new void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public Hashtable(int capacity, float loadFactor)
    {
    }

    public Hashtable(int capacity, float loadFactor, IHashCodeProvider hcp, IComparer comparer)
    {
    }

    public Hashtable(int capacity, float loadFactor, IEqualityComparer equalityComparer)
    {
    }

    public Hashtable(int capacity)
    {
    }

    public Hashtable(IDictionary d)
    {
    }

    public Hashtable()
    {
    }

    public Hashtable(IHashCodeProvider hcp, IComparer comparer)
    {
    }

    public Hashtable(IDictionary d, float loadFactor, IHashCodeProvider hcp, IComparer comparer)
    {
    }

    public Hashtable(IDictionary d, float loadFactor, IEqualityComparer equalityComparer)
    {
    }

    public Hashtable(IDictionary d, float loadFactor)
    {
    }

    public Hashtable(IDictionary d, IHashCodeProvider hcp, IComparer comparer)
    {
    }

    public Hashtable(IDictionary d, IEqualityComparer equalityComparer)
    {
    }

    public Hashtable(int capacity, IEqualityComparer equalityComparer)
    {
    }

    public Hashtable(int capacity, IHashCodeProvider hcp, IComparer comparer)
    {
    }

    public Hashtable(IEqualityComparer equalityComparer)
    {
    }

    protected Hashtable(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    protected virtual new bool KeyEquals(Object item, Object key)
    {
      return default(bool);
    }

    public virtual new void OnDeserialization(Object sender)
    {
    }

    public virtual new void Remove(Object key)
    {
    }

    public static System.Collections.Hashtable Synchronized(System.Collections.Hashtable table)
    {
      Contract.Ensures(Contract.Result<System.Collections.Hashtable>() != null);

      return default(System.Collections.Hashtable);
    }

    IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(IEnumerator);
    }
    #endregion

    #region Properties and indexers
    protected IComparer comparer
    {
      get
      {
        return default(IComparer);
      }
      set
      {
      }
    }

    public virtual new int Count
    {
      get
      {
        return default(int);
      }
    }

    protected IEqualityComparer EqualityComparer
    {
      get
      {
        return default(IEqualityComparer);
      }
    }

    protected IHashCodeProvider hcp
    {
      get
      {
        return default(IHashCodeProvider);
      }
      set
      {
      }
    }

    public virtual new bool IsFixedSize
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsSynchronized
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new Object this [Object key]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public virtual new ICollection Keys
    {
      get
      {
        return default(ICollection);
      }
    }

    public virtual new Object SyncRoot
    {
      get
      {
        return default(Object);
      }
    }

    public virtual new ICollection Values
    {
      get
      {
        return default(ICollection);
      }
    }
    #endregion
  }
}

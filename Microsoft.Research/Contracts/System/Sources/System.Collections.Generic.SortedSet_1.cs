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

// File System.Collections.Generic.SortedSet_1.cs
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
  public partial class SortedSet<T> : ISet<T>, ICollection<T>, IEnumerable<T>, System.Collections.ICollection, System.Collections.IEnumerable, System.Runtime.Serialization.ISerializable, System.Runtime.Serialization.IDeserializationCallback
  {
    #region Methods and constructors
    public bool Add(T item)
    {
      return default(bool);
    }

    public virtual new void Clear()
    {
    }

    public virtual new bool Contains(T item)
    {
      return default(bool);
    }

    public void CopyTo(T[] array, int index, int count)
    {
    }

    public void CopyTo(T[] array)
    {
    }

    public void CopyTo(T[] array, int index)
    {
    }

    public static IEqualityComparer<System.Collections.Generic.SortedSet<T>> CreateSetComparer(IEqualityComparer<T> memberEqualityComparer)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.IEqualityComparer<System.Collections.Generic.SortedSet<T>>>() != null);

      return default(IEqualityComparer<System.Collections.Generic.SortedSet<T>>);
    }

    public static IEqualityComparer<System.Collections.Generic.SortedSet<T>> CreateSetComparer()
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.IEqualityComparer<System.Collections.Generic.SortedSet<T>>>() != null);

      return default(IEqualityComparer<System.Collections.Generic.SortedSet<T>>);
    }

    public void ExceptWith(IEnumerable<T> other)
    {
    }

    public System.Collections.Generic.SortedSet<T>.Enumerator GetEnumerator()
    {
      return default(System.Collections.Generic.SortedSet<T>.Enumerator);
    }

    protected virtual new void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public virtual new System.Collections.Generic.SortedSet<T> GetViewBetween(T lowerValue, T upperValue)
    {
      Contract.Requires(this.Comparer != null);

      return default(System.Collections.Generic.SortedSet<T>);
    }

    public virtual new void IntersectWith(IEnumerable<T> other)
    {
    }

    public bool IsProperSubsetOf(IEnumerable<T> other)
    {
      return default(bool);
    }

    public bool IsProperSupersetOf(IEnumerable<T> other)
    {
      return default(bool);
    }

    public bool IsSubsetOf(IEnumerable<T> other)
    {
      return default(bool);
    }

    public bool IsSupersetOf(IEnumerable<T> other)
    {
      return default(bool);
    }

    protected virtual new void OnDeserialization(Object sender)
    {
    }

    public bool Overlaps(IEnumerable<T> other)
    {
      return default(bool);
    }

    public bool Remove(T item)
    {
      return default(bool);
    }

    public int RemoveWhere(Predicate<T> match)
    {
      Contract.Ensures(0 <= Contract.Result<int>());

      return default(int);
    }

    public IEnumerable<T> Reverse()
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.IEnumerable<T>>() != null);

      return default(IEnumerable<T>);
    }

    public bool SetEquals(IEnumerable<T> other)
    {
      return default(bool);
    }

    protected SortedSet(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public SortedSet(IEnumerable<T> collection)
    {
    }

    public SortedSet(IComparer<T> comparer)
    {
    }

    public SortedSet()
    {
    }

    public SortedSet(IEnumerable<T> collection, IComparer<T> comparer)
    {
    }

    public void SymmetricExceptWith(IEnumerable<T> other)
    {
    }

    void System.Collections.Generic.ICollection<T>.Add(T item)
    {
    }

    IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
    {
      return default(IEnumerator<T>);
    }

    void System.Collections.ICollection.CopyTo(Array array, int index)
    {
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    void System.Runtime.Serialization.IDeserializationCallback.OnDeserialization(Object sender)
    {
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public void UnionWith(IEnumerable<T> other)
    {
    }
    #endregion

    #region Properties and indexers
    public IComparer<T> Comparer
    {
      get
      {
        return default(IComparer<T>);
      }
    }

    public int Count
    {
      get
      {
        return default(int);
      }
    }

    public T Max
    {
      get
      {
        return default(T);
      }
    }

    public T Min
    {
      get
      {
        return default(T);
      }
    }

    bool System.Collections.Generic.ICollection<T>.IsReadOnly
    {
      get
      {
        return default(bool);
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
    #endregion
  }
}

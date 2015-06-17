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

// File System.Collections.Generic.HashSet_1.cs
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
  public partial class HashSet<T> : System.Runtime.Serialization.ISerializable, System.Runtime.Serialization.IDeserializationCallback, ISet<T>, ICollection<T>, IEnumerable<T>, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public bool Add(T item)
    {
      return default(bool);
    }

    public void Clear()
    {
    }

    public bool Contains(T item)
    {
      return default(bool);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
    }

    public void CopyTo(T[] array)
    {
    }

    public void CopyTo(T[] array, int arrayIndex, int count)
    {
    }

    public static IEqualityComparer<System.Collections.Generic.HashSet<T>> CreateSetComparer()
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.IEqualityComparer<System.Collections.Generic.HashSet<T>>>() != null);

      return default(IEqualityComparer<System.Collections.Generic.HashSet<T>>);
    }

    public void ExceptWith(IEnumerable<T> other)
    {
    }

    public System.Collections.Generic.HashSet<T>.Enumerator GetEnumerator()
    {
      return default(System.Collections.Generic.HashSet<T>.Enumerator);
    }

    public virtual new void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public HashSet()
    {
    }

    public HashSet(IEnumerable<T> collection)
    {
    }

    public HashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
    {
    }

    protected HashSet(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public HashSet(IEqualityComparer<T> comparer)
    {
    }

    public void IntersectWith(IEnumerable<T> other)
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

    public virtual new void OnDeserialization(Object sender)
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

    public bool SetEquals(IEnumerable<T> other)
    {
      return default(bool);
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

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public void TrimExcess()
    {
    }

    public void UnionWith(IEnumerable<T> other)
    {
    }
    #endregion

    #region Properties and indexers
    public IEqualityComparer<T> Comparer
    {
      get
      {
        return default(IEqualityComparer<T>);
      }
    }

    public int Count
    {
      get
      {
        return default(int);
      }
    }

    bool System.Collections.Generic.ICollection<T>.IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}

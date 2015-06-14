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

// File System.Collections.Generic.List_1.cs
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
  public partial class List<T> : IList<T>, ICollection<T>, IEnumerable<T>, System.Collections.IList, System.Collections.ICollection, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public void Add(T item)
    {
    }

    public void AddRange(IEnumerable<T> collection)
    {
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<T> AsReadOnly()
    {
      Contract.Ensures(Contract.Result<System.Collections.ObjectModel.ReadOnlyCollection<T>>() != null);

      return default(System.Collections.ObjectModel.ReadOnlyCollection<T>);
    }

    public int BinarySearch(T item, IComparer<T> comparer)
    {
      return default(int);
    }

    public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
    {
      return default(int);
    }

    public int BinarySearch(T item)
    {
      return default(int);
    }

    public void Clear()
    {
    }

    public bool Contains(T item)
    {
      return default(bool);
    }

    public System.Collections.Generic.List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.List<TOutput>>() != null);

      return default(System.Collections.Generic.List<TOutput>);
    }

    public void CopyTo(int index, T[] array, int arrayIndex, int count)
    {
    }

    public void CopyTo(T[] array)
    {
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
    }

    public bool Exists(Predicate<T> match)
    {
      return default(bool);
    }

    public T Find(Predicate<T> match)
    {
      return default(T);
    }

    public System.Collections.Generic.List<T> FindAll(Predicate<T> match)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.List<T>>() != null);

      return default(System.Collections.Generic.List<T>);
    }

    public int FindIndex(int startIndex, int count, Predicate<T> match)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public int FindIndex(Predicate<T> match)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public int FindIndex(int startIndex, Predicate<T> match)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public T FindLast(Predicate<T> match)
    {
      return default(T);
    }

    public int FindLastIndex(Predicate<T> match)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 2147483646);

      return default(int);
    }

    public int FindLastIndex(int startIndex, int count, Predicate<T> match)
    {
      Contract.Ensures((Contract.Result<int>() - startIndex) <= 0);
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 2147483646);

      return default(int);
    }

    public int FindLastIndex(int startIndex, Predicate<T> match)
    {
      Contract.Ensures((Contract.Result<int>() - startIndex) <= 0);
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 2147483646);

      return default(int);
    }

    public void ForEach(Action<T> action)
    {
    }

    public System.Collections.Generic.List<T>.Enumerator GetEnumerator()
    {
      return default(System.Collections.Generic.List<T>.Enumerator);
    }

    public System.Collections.Generic.List<T> GetRange(int index, int count)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.List<T>>() != null);

      return default(System.Collections.Generic.List<T>);
    }

    public int IndexOf(T item)
    {
      return default(int);
    }

    public int IndexOf(T item, int index, int count)
    {
      return default(int);
    }

    public int IndexOf(T item, int index)
    {
      return default(int);
    }

    public void Insert(int index, T item)
    {
    }

    public void InsertRange(int index, IEnumerable<T> collection)
    {
    }

    public int LastIndexOf(T item, int index, int count)
    {
      return default(int);
    }

    public int LastIndexOf(T item)
    {
      return default(int);
    }

    public int LastIndexOf(T item, int index)
    {
      return default(int);
    }

    public List(int capacity)
    {
    }

    public List(IEnumerable<T> collection)
    {
    }

    public List()
    {
    }

    public bool Remove(T item)
    {
      return default(bool);
    }

    public int RemoveAll(Predicate<T> match)
    {
      return default(int);
    }

    public void RemoveAt(int index)
    {
    }

    public void RemoveRange(int index, int count)
    {
    }

    public void Reverse(int index, int count)
    {
    }

    public void Reverse()
    {
    }

    public void Sort(int index, int count, IComparer<T> comparer)
    {
    }

    public void Sort(IComparer<T> comparer)
    {
    }

    public void Sort(Comparison<T> comparison)
    {
    }

    public void Sort()
    {
    }

    IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
    {
      return default(IEnumerator<T>);
    }

    void System.Collections.ICollection.CopyTo(Array array, int arrayIndex)
    {
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    int System.Collections.IList.Add(Object item)
    {
      return default(int);
    }

    bool System.Collections.IList.Contains(Object item)
    {
      return default(bool);
    }

    int System.Collections.IList.IndexOf(Object item)
    {
      return default(int);
    }

    void System.Collections.IList.Insert(int index, Object item)
    {
    }

    void System.Collections.IList.Remove(Object item)
    {
    }

    public T[] ToArray()
    {
      Contract.Ensures(Contract.Result<T[]>() != null);

      return default(T[]);
    }

    public void TrimExcess()
    {
    }

    public bool TrueForAll(Predicate<T> match)
    {
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

    public int Count
    {
      get
      {
        return default(int);
      }
    }

    public T this [int index]
    {
      get
      {
        return default(T);
      }
      set
      {
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

    bool System.Collections.IList.IsFixedSize
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Collections.IList.IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    Object System.Collections.IList.this [int index]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }
    #endregion
  }
}

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

// File System.Array.cs
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


namespace System
{
  abstract public partial class Array : ICloneable, System.Collections.IList, System.Collections.ICollection, System.Collections.IEnumerable, System.Collections.IStructuralComparable, System.Collections.IStructuralEquatable
  {
    #region Methods and constructors
    internal Array()
    {
    }

    public static System.Collections.ObjectModel.ReadOnlyCollection<T> AsReadOnly<T>(T[] array)
    {
      Contract.Ensures(Contract.Result<System.Collections.ObjectModel.ReadOnlyCollection<T>>() != null);

      return default(System.Collections.ObjectModel.ReadOnlyCollection<T>);
    }

    public static int BinarySearch<T>(T[] array, T value, IComparer<T> comparer)
    {
      return default(int);
    }

    public static int BinarySearch<T>(T[] array, int index, int length, T value)
    {
      return default(int);
    }

    public static int BinarySearch<T>(T[] array, T value)
    {
      return default(int);
    }

    public static int BinarySearch(Array array, int index, int length, Object value, System.Collections.IComparer comparer)
    {
      Contract.Ensures((length - ((System.Array)array).Length) <= 0);
      Contract.Ensures(0 <= ((System.Array)array).Length);

      return default(int);
    }

    public static int BinarySearch(Array array, int index, int length, Object value)
    {
      Contract.Ensures((length - ((System.Array)array).Length) <= 0);
      Contract.Ensures(0 <= ((System.Array)array).Length);

      return default(int);
    }

    public static int BinarySearch(Array array, Object value)
    {
      Contract.Ensures(0 <= ((System.Array)array).Length);

      return default(int);
    }

    public static int BinarySearch<T>(T[] array, int index, int length, T value, IComparer<T> comparer)
    {
      return default(int);
    }

    public static int BinarySearch(Array array, Object value, System.Collections.IComparer comparer)
    {
      Contract.Ensures(0 <= ((System.Array)array).Length);

      return default(int);
    }

    public static void Clear(Array array, int index, int length)
    {
    }

    public Object Clone()
    {
      return default(Object);
    }

    public static void ConstrainedCopy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
    {
    }

    public static TOutput[] ConvertAll<TInput, TOutput>(TInput[] array, Converter<TInput, TOutput> converter)
    {
      Contract.Ensures(Contract.Result<TOutput[]>() != null);

      return default(TOutput[]);
    }

    public static void Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
    {
    }

    public static void Copy(Array sourceArray, Array destinationArray, int length)
    {
    }

    public static void Copy(Array sourceArray, Array destinationArray, long length)
    {
    }

    public static void Copy(Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length)
    {
    }

    public void CopyTo(Array array, int index)
    {
    }

    public void CopyTo(Array array, long index)
    {
    }

    public static Array CreateInstance(Type elementType, int[] lengths, int[] lowerBounds)
    {
      return default(Array);
    }

    public static Array CreateInstance(Type elementType, int length)
    {
      return default(Array);
    }

    public static Array CreateInstance(Type elementType, long[] lengths)
    {
      return default(Array);
    }

    public static Array CreateInstance(Type elementType, int[] lengths)
    {
      return default(Array);
    }

    public static Array CreateInstance(Type elementType, int length1, int length2, int length3)
    {
      return default(Array);
    }

    public static Array CreateInstance(Type elementType, int length1, int length2)
    {
      return default(Array);
    }

    public static bool Exists<T>(T[] array, Predicate<T> match)
    {
      return default(bool);
    }

    public static T Find<T>(T[] array, Predicate<T> match)
    {
      return default(T);
    }

    public static T[] FindAll<T>(T[] array, Predicate<T> match)
    {
      return default(T[]);
    }

    public static int FindIndex<T>(T[] array, Predicate<T> match)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < array.Length);
      Contract.Ensures(Contract.Result<int>() <= 2147483646);

      return default(int);
    }

    public static int FindIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < array.Length);
      Contract.Ensures(Contract.Result<int>() <= 2147483646);

      return default(int);
    }

    public static int FindIndex<T>(T[] array, int startIndex, Predicate<T> match)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < array.Length);
      Contract.Ensures(Contract.Result<int>() <= 2147483646);

      return default(int);
    }

    public static T FindLast<T>(T[] array, Predicate<T> match)
    {
      return default(T);
    }

    public static int FindLastIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
    {
      Contract.Ensures((Contract.Result<int>() - startIndex) <= 0);
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < array.Length);
      Contract.Ensures(Contract.Result<int>() <= 2147483646);

      return default(int);
    }

    public static int FindLastIndex<T>(T[] array, int startIndex, Predicate<T> match)
    {
      Contract.Ensures((Contract.Result<int>() - startIndex) <= 0);
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < array.Length);
      Contract.Ensures(Contract.Result<int>() <= 2147483646);

      return default(int);
    }

    public static int FindLastIndex<T>(T[] array, Predicate<T> match)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < array.Length);
      Contract.Ensures(Contract.Result<int>() <= 2147483646);

      return default(int);
    }

    public static void ForEach<T>(T[] array, Action<T> action)
    {
    }

    public System.Collections.IEnumerator GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public int GetLength(int dimension)
    {
      return default(int);
    }

    public long GetLongLength(int dimension)
    {
      Contract.Ensures(-2147483648 <= Contract.Result<long>());
      Contract.Ensures(Contract.Result<long>() <= 2147483647);

      return default(long);
    }

    public int GetLowerBound(int dimension)
    {
      return default(int);
    }

    public int GetUpperBound(int dimension)
    {
      return default(int);
    }

    public Object GetValue(int index)
    {
      return default(Object);
    }

    public Object GetValue(int index1, int index2)
    {
      return default(Object);
    }

    public Object GetValue(long index1, long index2, long index3)
    {
      return default(Object);
    }

    public Object GetValue(int[] indices)
    {
      Contract.Ensures((Contract.OldValue(this.Rank) - indices.Length) <= 0);
      Contract.Ensures((indices.Length - Contract.OldValue(this.Rank)) <= 0);

      return default(Object);
    }

    public Object GetValue(int index1, int index2, int index3)
    {
      return default(Object);
    }

    public Object GetValue(long index1, long index2)
    {
      return default(Object);
    }

    public Object GetValue(long[] indices)
    {
      Contract.Ensures((Contract.OldValue(this.Rank) - indices.Length) <= 0);
      Contract.Ensures((indices.Length - Contract.OldValue(this.Rank)) <= 0);

      return default(Object);
    }

    public Object GetValue(long index)
    {
      return default(Object);
    }

    public static int IndexOf(Array array, Object value, int startIndex)
    {
      Contract.Ensures((startIndex - ((System.Array)array).Length) <= 0);
      Contract.Ensures(0 <= ((System.Array)array).Length);

      return default(int);
    }

    public static int IndexOf(Array array, Object value, int startIndex, int count)
    {
      Contract.Ensures((startIndex - ((System.Array)array).Length) <= 0);
      Contract.Ensures(0 <= ((System.Array)array).Length);

      return default(int);
    }

    public static int IndexOf<T>(T[] array, T value, int startIndex, int count)
    {
      return default(int);
    }

    public static int IndexOf<T>(T[] array, T value)
    {
      return default(int);
    }

    public static int IndexOf<T>(T[] array, T value, int startIndex)
    {
      return default(int);
    }

    public static int IndexOf(Array array, Object value)
    {
      Contract.Ensures(0 <= ((System.Array)array).Length);

      return default(int);
    }

    public void Initialize()
    {
    }

    public static int LastIndexOf(Array array, Object value, int startIndex, int count)
    {
      Contract.Ensures(0 <= ((System.Array)array).Length);

      return default(int);
    }

    public static int LastIndexOf<T>(T[] array, T value, int startIndex)
    {
      return default(int);
    }

    public static int LastIndexOf<T>(T[] array, T value, int startIndex, int count)
    {
      return default(int);
    }

    public static int LastIndexOf<T>(T[] array, T value)
    {
      return default(int);
    }

    public static int LastIndexOf(Array array, Object value)
    {
      Contract.Ensures(0 <= ((System.Array)array).Length);

      return default(int);
    }

    public static int LastIndexOf(Array array, Object value, int startIndex)
    {
      Contract.Ensures(0 <= ((System.Array)array).Length);

      return default(int);
    }

    public static void Resize<T>(ref T[] array, int newSize)
    {
      Contract.Ensures(0 <= array.Length);
      Contract.Ensures(array.Length == newSize);
    }

    public static void Reverse(Array array, int index, int length)
    {
      Contract.Ensures((length - ((System.Array)array).Length) <= 0);
    }

    public static void Reverse(Array array)
    {
      Contract.Ensures(0 <= ((System.Array)array).Length);
    }

    public void SetValue(Object value, int index)
    {
    }

    public void SetValue(Object value, long[] indices)
    {
      Contract.Ensures((Contract.OldValue(this.Rank) - indices.Length) <= 0);
      Contract.Ensures((indices.Length - Contract.OldValue(this.Rank)) <= 0);
    }

    public void SetValue(Object value, int[] indices)
    {
      Contract.Ensures((Contract.OldValue(this.Rank) - indices.Length) <= 0);
      Contract.Ensures((indices.Length - Contract.OldValue(this.Rank)) <= 0);
    }

    public void SetValue(Object value, int index1, int index2, int index3)
    {
    }

    public void SetValue(Object value, int index1, int index2)
    {
    }

    public void SetValue(Object value, long index1, long index2)
    {
    }

    public void SetValue(Object value, long index1, long index2, long index3)
    {
    }

    public void SetValue(Object value, long index)
    {
    }

    public static void Sort<T>(T[] array, int index, int length)
    {
    }

    public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, int index, int length)
    {
    }

    public static void Sort<T>(T[] array)
    {
    }

    public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items)
    {
    }

    public static void Sort<T>(T[] array, IComparer<T> comparer)
    {
    }

    public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, int index, int length, IComparer<TKey> comparer)
    {
    }

    public static void Sort<T>(T[] array, Comparison<T> comparison)
    {
    }

    public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, IComparer<TKey> comparer)
    {
    }

    public static void Sort<T>(T[] array, int index, int length, IComparer<T> comparer)
    {
    }

    public static void Sort(Array keys, Array items, int index, int length, System.Collections.IComparer comparer)
    {
      Contract.Ensures((length - ((System.Array)keys).Length) <= 0);
      Contract.Ensures(0 <= ((System.Array)keys).Length);
    }

    public static void Sort(Array array, int index, int length)
    {
      Contract.Ensures((length - ((System.Array)array).Length) <= 0);
      Contract.Ensures(0 <= ((System.Array)array).Length);
    }

    public static void Sort(Array keys, Array items)
    {
      Contract.Ensures(0 <= ((System.Array)keys).Length);
    }

    public static void Sort(Array array)
    {
      Contract.Ensures(0 <= ((System.Array)array).Length);
    }

    public static void Sort(Array keys, Array items, int index, int length)
    {
      Contract.Ensures((length - ((System.Array)keys).Length) <= 0);
      Contract.Ensures(0 <= ((System.Array)keys).Length);
    }

    public static void Sort(Array array, int index, int length, System.Collections.IComparer comparer)
    {
      Contract.Ensures((length - ((System.Array)array).Length) <= 0);
      Contract.Ensures(0 <= ((System.Array)array).Length);
    }

    public static void Sort(Array keys, Array items, System.Collections.IComparer comparer)
    {
      Contract.Ensures(0 <= ((System.Array)keys).Length);
    }

    public static void Sort(Array array, System.Collections.IComparer comparer)
    {
      Contract.Ensures(0 <= ((System.Array)array).Length);
    }

    int System.Collections.IList.Add(Object value)
    {
      return default(int);
    }

    void System.Collections.IList.Clear()
    {
    }

    bool System.Collections.IList.Contains(Object value)
    {
      return default(bool);
    }

    int System.Collections.IList.IndexOf(Object value)
    {
      return default(int);
    }

    void System.Collections.IList.Insert(int index, Object value)
    {
    }

    void System.Collections.IList.Remove(Object value)
    {
    }

    void System.Collections.IList.RemoveAt(int index)
    {
    }

    int System.Collections.IStructuralComparable.CompareTo(Object other, System.Collections.IComparer comparer)
    {
      return default(int);
    }

    bool System.Collections.IStructuralEquatable.Equals(Object other, System.Collections.IEqualityComparer comparer)
    {
      return default(bool);
    }

    int System.Collections.IStructuralEquatable.GetHashCode(System.Collections.IEqualityComparer comparer)
    {
      return default(int);
    }

    public static bool TrueForAll<T>(T[] array, Predicate<T> match)
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public bool IsFixedSize
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsSynchronized
    {
      get
      {
        return default(bool);
      }
    }

    public int Length
    {
      get
      {
        return default(int);
      }
    }

    public long LongLength
    {
      get
      {
        Contract.Ensures(-2147483648 <= Contract.Result<long>());
        Contract.Ensures(Contract.Result<long>() <= 2147483647);
        Contract.Ensures(Contract.Result<long>() == ((System.Array)this).Length);
        Contract.Ensures(Contract.Result<long>() == (long)(((System.Array)this).Length));

        return default(long);
      }
    }

    public int Rank
    {
      get
      {
        return default(int);
      }
    }

    public Object SyncRoot
    {
      get
      {
        return default(Object);
      }
    }

    int System.Collections.ICollection.Count
    {
      get
      {
        return default(int);
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

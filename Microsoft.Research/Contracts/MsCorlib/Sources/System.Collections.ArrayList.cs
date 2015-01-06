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

// File System.Collections.ArrayList.cs
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
  public partial class ArrayList : IList, ICollection, IEnumerable, ICloneable
  {
    #region Methods and constructors
    public static System.Collections.ArrayList Adapter(IList list)
    {
      Contract.Ensures(Contract.Result<System.Collections.ArrayList>() != null);

      return default(System.Collections.ArrayList);
    }

    public virtual new int Add(Object value)
    {
      return default(int);
    }

    public virtual new void AddRange(ICollection c)
    {
    }

    public ArrayList()
    {
    }

    public ArrayList(int capacity)
    {
    }

    public ArrayList(ICollection c)
    {
      Contract.Requires(0 <= c.Count);
    }

    public virtual new int BinarySearch(Object value, IComparer comparer)
    {
      return default(int);
    }

    public virtual new int BinarySearch(Object value)
    {
      return default(int);
    }

    public virtual new int BinarySearch(int index, int count, Object value, IComparer comparer)
    {
      return default(int);
    }

    public virtual new void Clear()
    {
    }

    public virtual new Object Clone()
    {
      return default(Object);
    }

    public virtual new bool Contains(Object item)
    {
      return default(bool);
    }

    public virtual new void CopyTo(int index, Array array, int arrayIndex, int count)
    {
    }

    public virtual new void CopyTo(Array array)
    {
    }

    public virtual new void CopyTo(Array array, int arrayIndex)
    {
    }

    public static IList FixedSize(IList list)
    {
      Contract.Ensures(Contract.Result<System.Collections.IList>() != null);

      return default(IList);
    }

    public static System.Collections.ArrayList FixedSize(System.Collections.ArrayList list)
    {
      Contract.Ensures(Contract.Result<System.Collections.ArrayList>() != null);

      return default(System.Collections.ArrayList);
    }

    public virtual new IEnumerator GetEnumerator()
    {
      return default(IEnumerator);
    }

    public virtual new IEnumerator GetEnumerator(int index, int count)
    {
      return default(IEnumerator);
    }

    public virtual new System.Collections.ArrayList GetRange(int index, int count)
    {
      return default(System.Collections.ArrayList);
    }

    public virtual new int IndexOf(Object value, int startIndex)
    {
      return default(int);
    }

    public virtual new int IndexOf(Object value)
    {
      return default(int);
    }

    public virtual new int IndexOf(Object value, int startIndex, int count)
    {
      return default(int);
    }

    public virtual new void Insert(int index, Object value)
    {
    }

    public virtual new void InsertRange(int index, ICollection c)
    {
    }

    public virtual new int LastIndexOf(Object value, int startIndex, int count)
    {
      return default(int);
    }

    public virtual new int LastIndexOf(Object value, int startIndex)
    {
      return default(int);
    }

    public virtual new int LastIndexOf(Object value)
    {
      return default(int);
    }

    public static System.Collections.ArrayList ReadOnly(System.Collections.ArrayList list)
    {
      Contract.Ensures(Contract.Result<System.Collections.ArrayList>() != null);

      return default(System.Collections.ArrayList);
    }

    public static IList ReadOnly(IList list)
    {
      Contract.Ensures(Contract.Result<System.Collections.IList>() != null);

      return default(IList);
    }

    public virtual new void Remove(Object obj)
    {
    }

    public virtual new void RemoveAt(int index)
    {
    }

    public virtual new void RemoveRange(int index, int count)
    {
    }

    public static System.Collections.ArrayList Repeat(Object value, int count)
    {
      Contract.Ensures(Contract.Result<System.Collections.ArrayList>() != null);

      return default(System.Collections.ArrayList);
    }

    public virtual new void Reverse(int index, int count)
    {
    }

    public virtual new void Reverse()
    {
    }

    public virtual new void SetRange(int index, ICollection c)
    {
    }

    public virtual new void Sort(IComparer comparer)
    {
    }

    public virtual new void Sort(int index, int count, IComparer comparer)
    {
    }

    public virtual new void Sort()
    {
    }

    public static System.Collections.ArrayList Synchronized(System.Collections.ArrayList list)
    {
      Contract.Ensures(Contract.Result<System.Collections.ArrayList>() != null);

      return default(System.Collections.ArrayList);
    }

    public static IList Synchronized(IList list)
    {
      Contract.Ensures(Contract.Result<System.Collections.IList>() != null);

      return default(IList);
    }

    public virtual new Array ToArray(Type type)
    {
      return default(Array);
    }

    public virtual new Object[] ToArray()
    {
      return default(Object[]);
    }

    public virtual new void TrimToSize()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new int Capacity
    {
      get
      {
        return default(int);
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

    public virtual new Object this [int index]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public virtual new Object SyncRoot
    {
      get
      {
        return default(Object);
      }
    }
    #endregion
  }
}

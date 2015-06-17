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
using System.Diagnostics.Contracts;

namespace System.Collections
{
#if !SILVERLIGHT
  public class ArrayList
  {
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

    extern public virtual int Count { get; }
    extern public virtual bool IsReadOnly { get; }
    extern public virtual bool IsFixedSize { get; }

    public virtual void TrimToSize()
    {
      //Contract.Requires(!this.IsReadOnly);
      //Contract.Requires(!this.IsFixedSize);

    }
    public virtual Array ToArray(Type type)
    {
      Contract.Requires(type != null);

      Contract.Ensures(Contract.Result<Array>() != null);
      return default(Array);
    }

    public virtual object[] ToArray()
    {
      Contract.Ensures(Contract.Result<object[]>() != null);
      return default(object[]);
    }

    public static ArrayList Synchronized(ArrayList list)
    {
      Contract.Requires(list != null);
      //Contract.Ensures(result.IsSynchronized);

      return default(ArrayList);
    }
#if false
        public static IList Synchronized (IList list) {
            Contract.Requires(list != null);
            Contract.Ensures(result.IsSynchronized);

          return default(IList);
        }
#endif
    public virtual void Sort(int index, int count, IComparer comparer)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index + count <= Count);
      //Contract.Requires(!this.IsReadOnly);

    }
    public virtual void Sort(IComparer comparer)
    {
      //Contract.Requires(!this.IsReadOnly);

    }
    public virtual void Sort()
    {
      //Contract.Requires(!this.IsReadOnly);

    }
    public virtual ArrayList GetRange(int index, int count)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index + count <= Count);

      return default(ArrayList);
    }
    public virtual void SetRange(int index, ICollection c)
    {
      Contract.Requires(c != null);
      Contract.Requires(index >= 0);
      Contract.Requires(index + c.Count <= Count);
      //Contract.Requires(!this.IsReadOnly);

    }
    public virtual void Reverse(int index, int count)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index + count <= Count);
      //Contract.Requires(!this.IsReadOnly);

    }
    public virtual void Reverse()
    {
      //Contract.Requires(!this.IsReadOnly);

    }
    [Pure]
    public static ArrayList Repeat(object value, int count)
    {
      Contract.Requires(count >= 0);

      return default(ArrayList);
    }
    public virtual void RemoveRange(int index, int count)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index + count <= Count);
      //Contract.Requires(!this.IsReadOnly);
      //Contract.Requires(!this.IsFixedSize);

    }
    public static ArrayList ReadOnly(ArrayList list)
    {
      Contract.Requires(list != null);
      Contract.Ensures(Contract.Result<ArrayList>().IsReadOnly);

      Contract.Ensures(Contract.Result<ArrayList>() != null);
      return default(ArrayList);
    }
    public static IList ReadOnly(IList list)
    {
      Contract.Requires(list != null);
      Contract.Ensures(Contract.Result<IList>().IsReadOnly);

      Contract.Ensures(Contract.Result<IList>() != null);
      return default(IList);
    }
    [Pure]
    public virtual int LastIndexOf(object value, int startIndex, int count)
    {
      Contract.Requires(0 <= count);
      Contract.Requires(0 <= startIndex);
      Contract.Requires(startIndex + count <= Count);
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Count);

      return default(int);
    }
    [Pure]
    public virtual int LastIndexOf(object value, int startIndex)
    {
      Contract.Requires(startIndex >= 0);
      Contract.Requires(startIndex < this.Count);
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Count);

      return default(int);
    }
    [Pure]
    public virtual int LastIndexOf(object value)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Count);

      return default(int);
    }
    public virtual void InsertRange(int index, ICollection c)
    {
      Contract.Requires(c != null);
      Contract.Requires(index >= 0);
      Contract.Requires(index <= Count);
      //Contract.Requires(!this.IsReadOnly);
      //Contract.Requires(!this.IsFixedSize);

    }
    [Pure]
    public virtual int IndexOf(object value, int startIndex, int count)
    {
      Contract.Requires(0 <= count);
      Contract.Requires(0 <= startIndex);
      Contract.Requires(startIndex + count <= Count);
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Count);

      return default(int);
    }
    [Pure]
    public virtual int IndexOf(object value, int startIndex)
    {
      Contract.Requires(startIndex >= 0);
      Contract.Requires(startIndex < this.Count);
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() < this.Count);

      return default(int);
    }
    [Pure]
    public virtual IEnumerator GetEnumerator(int index, int count)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);

      Contract.Ensures(Contract.Result<IEnumerator>() != null);
      return default(IEnumerator);
    }
    public static ArrayList FixedSize(ArrayList list)
    {
      Contract.Requires(list != null);
      Contract.Ensures(Contract.Result<ArrayList>().IsFixedSize);

      return default(ArrayList);
    }
    public static IList FixedSize(IList list)
    {
      Contract.Requires(list != null);
      Contract.Ensures(Contract.Result<IList>().IsFixedSize);

      return default(IList);
    }
    public virtual void CopyTo(int index, Array array, int arrayIndex, int count)
    {
      Contract.Requires(array != null);
      Contract.Requires(array.Rank == 1);
      Contract.Requires(index >= 0);
      Contract.Requires(index < this.Count);
      Contract.Requires(arrayIndex >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index + count <= this.Count);
      Contract.Requires(arrayIndex <= array.Length - count);

    }
    public virtual void CopyTo(Array array)
    {
      Contract.Requires(array != null);
      Contract.Requires(array.Rank == 1);
      Contract.Requires(this.Count <= array.Length);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public virtual int BinarySearch(object value, IComparer comparer)
    {
      Contract.Ensures(Contract.Result<int>() < this.Count);
      Contract.Ensures(~Contract.Result<int>() <= this.Count);

      return default(int);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public virtual int BinarySearch(object value)
    {
      Contract.Ensures(Contract.Result<int>() < this.Count);
      Contract.Ensures(~Contract.Result<int>() <= this.Count);

      return default(int);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    public virtual int BinarySearch(int index, int count, object value, IComparer comparer)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index <= this.Count - count);
      Contract.Ensures(Contract.Result<int>() - index < count);
      Contract.Ensures(~Contract.Result<int>() - index <= count);
      Contract.Ensures(Contract.Result<int>() >= index || ~Contract.Result<int>() >= index);

      return default(int);
    }
    public virtual void AddRange(ICollection c)
    {
      Contract.Requires(c != null);
      //Contract.Requires(!this.IsReadOnly);
      //Contract.Requires(!this.IsFixedSize);

    }
    public static ArrayList Adapter(IList list)
    {
      Contract.Requires(list != null);

      return default(ArrayList);
    }
    public ArrayList(ICollection c)
    {
      Contract.Requires(c != null);
      Contract.Ensures(this.Capacity == c.Count);
      Contract.Ensures(this.Count == c.Count);
      Contract.Ensures(!this.IsReadOnly);
      Contract.Ensures(!this.IsFixedSize);

    }
    public ArrayList(int capacity)
    {
      Contract.Requires(capacity >= 0);
      Contract.Ensures(capacity < 0 || this.Capacity >= capacity);
      Contract.Ensures(!this.IsReadOnly);
      Contract.Ensures(!this.IsFixedSize);
    }
    public ArrayList()
    {
      Contract.Ensures(this.Count == 0);
      Contract.Ensures(!this.IsReadOnly);
      Contract.Ensures(!this.IsFixedSize);
    }
  }
#endif
}
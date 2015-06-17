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

#define EXPENSIVE

namespace Collections {
  using System;
  using Microsoft.Contracts;
  using System.Collections;


  public class ArrayList {
    private object[] _items;
    private int _size;

    private const int _defaultCapacity = 16;

    void ObjectInvariant() {
      Contract.Invariant(0 <= _size && _size <= _items.Length);
      Contract.Invariant( Contract.ForAll(_size, _items.Length, (i) =>  _items[i] == null));  // all unused slots are null
    }

    public ArrayList() {
      Contract.Ensures(Count == 0);
      Contract.Ensures(Capacity == 16);

      _items = new object[_defaultCapacity];
    }


    public ArrayList(int capacity) {
      Contract.Requires(0 <= capacity);
      Contract.Ensures(Count == 0);
      Contract.Ensures(Capacity == capacity);

      _items = new object[capacity];
    }


    public ArrayList(ICollection c) {
      Contract.Requires(c != null);
      Contract.Ensures(Capacity == c.Count);
      Contract.Ensures(Count == c.Count);

      _items = new object[c.Count];
      AddRange(c);
    }



    public virtual int Capacity {
      [Pure]
      get {
        Contract.Ensures(0 <= Contract.Result<int>());
        return _items.Length;
      }

      set {
        Contract.Requires(Count <= value);

        if (value != _items.Length) {
          if (value > 0) {
            object[] newItems = new object[value];
            if (_size > 0) Array.Copy(_items, 0, newItems, 0, _size);
            _items = newItems;
          } else {
            _items = new object[_defaultCapacity];
          }
        }
      }
    }


    public virtual int Count {
      [Pure]
      get {
        Contract.Ensures(0 <= Contract.Result<int>());
        
        return _size;
      }
    }


    public virtual object this[int index] {
      [Pure]
      get {
        Contract.Requires(0 <= index && index < Count);

        return _items[index];
      }
      set {
        Contract.Requires(0 <= index && index < Count);

        _items[index] = value;
      }
    }


    public virtual int Add(object value) {
      Contract.Ensures(Count == Contract.Old(Count) + 1);
      Contract.Ensures(this[Contract.Result<int>()] == value);
      Contract.Ensures(Contract.Result<int>() == Contract.Old(Count));

      if (_size == _items.Length) EnsureCapacity(_size + 1);
      _items[_size] = value;
      return _size++;
    }


    private void EnsureCapacity(int min) {
      Contract.Ensures(min <= Capacity);
      
      if (_items.Length < min) {
        int newCapacity = _items.Length == 0 ? 16 : _items.Length * 2;
        if (newCapacity < min) newCapacity = min;
        Capacity = newCapacity;
      }
    }


    public virtual void AddRange(ICollection c) {
      Contract.Requires(c != null);

      InsertRange(_size, c);
    }


    [Pure]
    public virtual int BinarySearch(int index, int count, object value, IComparer comparer) {
      Contract.Requires(0 <= index);
      Contract.Requires(0 <= count);
      Contract.Requires(index + count <= Count);
      Contract.Ensures(Contract.Result<int>() < Count);
      Contract.Ensures(!(0 <= Contract.Result<int>() && Contract.Result<int>() < Count) || Equals(this[Contract.Result<int>()], value));

      return Array.BinarySearch(_items, index, count, value, comparer);
    }


    [Pure]
    public virtual int BinarySearch(object value) {
      Contract.Ensures(Contract.Result<int>() < Count);
      Contract.Ensures(!(0 <= Contract.Result<int>() && Contract.Result<int>() < Count) || Equals(this[Contract.Result<int>()], value));
      
      return BinarySearch(0, Count, value, null);
    }


    [Pure]
    public virtual int BinarySearch(object value, IComparer comparer) {
      Contract.Ensures(Contract.Result<int>() < Count);
      Contract.Ensures(!(0 <= Contract.Result<int>() && Contract.Result<int>() < Count) || Equals(this[Contract.Result<int>()], value));

      return BinarySearch(0, Count, value, comparer);
    }


    public virtual void Clear() {
      Contract.Ensures(Count == 0);

      Array.Clear(_items, 0, _size); // Don't need to doc this but we clear the elements so that the gc can reclaim the references.
      _size = 0;
    }


    public virtual object Clone() {
      ArrayList la = new ArrayList(_size);
      la._size = _size;
      Array.Copy(_items, 0, la._items, 0, _size);
      return la;
    }


    public virtual bool Contains(object item) {
      if (item == null) {
        for (int i = 0; i < _size; i++)
          if (_items[i] == null)
            return true;
        return false;
      } else {
        for (int i = 0; i < _size; i++)
          if (item.Equals(_items[i]))
            return true;
        return false;
      }
    }


    public virtual void CopyTo(Array array) {
      Contract.Requires(array != null);
      Contract.Requires(array.Rank == 1);

      CopyTo(array, 0);
    }


    public virtual void CopyTo(Array array, int arrayIndex) {
      Contract.Requires(array != null);
      Contract.Requires(array.Rank == 1);
      Contract.Requires(0 <= arrayIndex && arrayIndex < Count);

      Array.Copy(_items, 0, array, arrayIndex, _size);
    }


    public virtual void CopyTo(int index, Array array, int arrayIndex, int count) {
      Contract.Requires(array != null);
      Contract.Requires(array.Rank == 1);
      Contract.Requires(0 <= index && index < Count);
      Contract.Requires(0 <= arrayIndex && arrayIndex < array.Length);
      Contract.Requires(0 <= count);
      Contract.Requires(index + count <= Count);
      Contract.Requires(arrayIndex + count <= array.Length);

      Array.Copy(_items, index, array, arrayIndex, count);
    }


    [Pure]
    public virtual int IndexOf(object value) {
      Contract.Ensures(-1 <= Contract.Result<int>() && Contract.Result<int>() < Count);
      //#if EXPENSIVE
      //            Contract.Ensures( (Contract.Result<int>() == -1 && ! exists { int i in (0:Count); Equals(this[i],value) })
      //                     || (0 <= Contract.Result<int>() && Contract.Result<int>() < Count && Equals(this[Contract.Result<int>()], value));
      //#endif                   

      return Array.IndexOf(_items, value, 0, _size);
    }


    [Pure]
    public virtual int IndexOf(object value, int startIndex) {
      Contract.Requires(0 <= startIndex && startIndex <= Count);
      Contract.Ensures(-1 <= Contract.Result<int>() && Contract.Result<int>() < Count);
      //#if EXPENSIVE
      //            Contract.Ensures( (Contract.Result<int>() == -1 && ! exists { int i in (0:Count); Equals(this[i],value) })
      //                     || (0 <= Contract.Result<int>() && Contract.Result<int>() < Count && Equals(this[Contract.Result<int>()], value));
      //#endif

      return Array.IndexOf(_items, value, startIndex, _size - startIndex);
    }


    [Pure]
    public virtual int IndexOf(object value, int startIndex, int count) {
      Contract.Requires(0 <= startIndex);
      Contract.Requires(0 <= count);
      Contract.Requires(startIndex + count <= Count);
      Contract.Ensures(-1 <= Contract.Result<int>() && Contract.Result<int>() < Count);
      //#if EXPENSIVE
      //            Contract.Ensures( (Contract.Result<int>() == -1 && ! exists { int i in (0:Count); Equals(this[i],value) })
      //                     || (0 <= Contract.Result<int>() && Contract.Result<int>() < Count && Equals(this[Contract.Result<int>()], value));
      //#endif                   
      return Array.IndexOf(_items, value, startIndex, count);
    }


    public virtual void Insert(int index, object value) {
      Contract.Requires(0 <= index && index <= Count);

      if (_size == _items.Length) EnsureCapacity(_size + 1);
      if (index < _size) {
        Array.Copy(_items, index, _items, index + 1, _size - index);
      }
      _items[index] = value;
      _size++;
    }


    public virtual void InsertRange(int index, ICollection c) {
      Contract.Requires(c != null);
      Contract.Requires(0 <= index && index <= Count);

      int count = c.Count;
      if (count > 0) {
        EnsureCapacity(_size + count);
        if (index < _size) {
          Array.Copy(_items, index, _items, index + count, _size - index);
        }
        c.CopyTo(_items, index);
        _size += count;
      }
    }


    [Pure]
    public virtual int LastIndexOf(object value) {
      Contract.Ensures(-1 <= Contract.Result<int>() && Contract.Result<int>() < Count);
      //#if EXPENSIVE
      //            Contract.Ensures( (Contract.Result<int>() == -1 && ! exists { int i in (0:Count); Equals(this[i],value) })
      //                     || (0 <= Contract.Result<int>() && Contract.Result<int>() < Count && Equals(this[Contract.Result<int>()], value));
      //#endif

      return LastIndexOf(value, _size - 1, _size);
    }


    [Pure]
    public virtual int LastIndexOf(object value, int startIndex) {
      Contract.Requires(0 <= startIndex && startIndex < Count);
      Contract.Ensures(-1 <= Contract.Result<int>() && Contract.Result<int>() < Count);
      //#if EXPENSIVE
      //            Contract.Ensures( (Contract.Result<int>() == -1 && ! exists { int i in (0:Count); Equals(this[i],value) })
      //                    || (0 <= Contract.Result<int>() && Contract.Result<int>() < Count && Equals(this[Contract.Result<int>()], value));
      //#endif
                   
      return LastIndexOf(value, startIndex, startIndex + 1);
    }


    [Pure]
    public virtual int LastIndexOf(object value, int startIndex, int count) {
      Contract.Requires(0 <= count);
      Contract.Requires(0 <= startIndex);
      Contract.Requires(startIndex + count <= Count);
      Contract.Ensures(-1 <= Contract.Result<int>() && Contract.Result<int>() < Count);
      //#if EXPENSIVE
      //            Contract.Ensures( (Contract.Result<int>() == -1 && ! exists { int i in (0:Count); Equals(this[i],value) })
      //                     || (0 <= Contract.Result<int>() && Contract.Result<int>() < Count && Equals(this[Contract.Result<int>()], value));
      //#endif

      if (_size == 0)
        return -1;
      return Array.LastIndexOf(_items, value, startIndex, count);
    }


    public virtual void Remove(object obj) {
      int index = IndexOf(obj);
      if (index >= 0)
        RemoveAt(index);
    }


    public virtual void RemoveAt(int index) {
      Contract.Requires(0 <= index);
      Contract.Requires(index < Count);
//      Contract.Ensures(Contract.Old<int>(Count) == Count + 1);

      _size--;
      if (index < _size) {
        Array.Copy(_items, index + 1, _items, index, _size - index);
      }
      _items[_size] = null;
    }


    public virtual void RemoveRange(int index, int count) {
      Contract.Requires(0 <= index);
      Contract.Requires(0 <= count);
      Contract.Requires(index + count <= Count);

      if (count > 0) {
        int i = _size;
        _size -= count;
        if (index < _size) {
          Array.Copy(_items, index + count, _items, index, _size - index);
        }
        while (i > _size) _items[--i] = null;
      }
    }


    [Pure]
    public static ArrayList Repeat(object value, int count) {
      Contract.Requires(0 <= count);
      Contract.Ensures(Contract.Result<ArrayList>() != null);

      ArrayList list = new ArrayList((count > _defaultCapacity) ? count : _defaultCapacity);
      for (int i = 0; i < count; i++)
        list.Add(value);
      return list;
    }


    public virtual void Reverse() {
      Reverse(0, Count);
    }


    public virtual void Reverse(int index, int count) {
      Contract.Requires(0 <= index);
      Contract.Requires(0 <= count);
      Contract.Requires(index + count <= Count);

      Array.Reverse(_items, index, count);
    }


    public virtual void Sort() {
      Sort(0, Count, Comparer.Default);
    }


    public virtual void Sort(IComparer comparer) {
      Sort(0, Count, comparer);
    }


    public virtual void Sort(int index, int count, IComparer comparer) {
      Contract.Requires(0 <= index);
      Contract.Requires(0 <= count);
      Contract.Requires(index + count <= Count);

      Array.Sort(_items, index, count, comparer);
    }


    public virtual object[] ToArray() {
      object[] array = new object[_size];
      Array.Copy(_items, 0, array, 0, _size);
      return array;
    }


    public virtual Array ToArray(Type type) {
      Contract.Requires(type != null);

      Array array = Array.CreateInstance(type, _size);
      Array.Copy(_items, 0, array, 0, _size);
      return array;
    }


    public virtual void TrimToSize() {
      Capacity = _size;
    }
  }




  public class ArrayList2 : ArrayList
  {
    public void ObjectInvariant()
    {
      Contract.Invariant(true);
    }
    public ArrayList2(int capacity) : base(capacity) { }

    public override int Add(object o) { return 0; }

  }








  public class TestArrayList {
    public static void Main(string[] args) {
      ArrayList a = new ArrayList2(10);
      Contract.Assert(a.Count == 0);

      a.Add("apple");
      a.Add("cranberry");
      a.Add("banana");
      Contract.Assert(a.Count == 3);
      Contract.Assert(Equals(a[0], "apple"));
      Contract.Assert(Equals(a[1], "cranberry"));
      Contract.Assert(Equals(a[2], "banana"));

      Contract.Assert(a.IndexOf("apple") == 0);
      Contract.Assert(a.IndexOf("cranberry") == 1);
      Contract.Assert(a.IndexOf("banana") == 2);
      Contract.Assert(a.IndexOf("donut") == -1);

      a.Sort();
      Contract.Assert(a.Count == 3);
      Contract.Assert(Equals(a[0], "apple"));
      Contract.Assert(Equals(a[1], "banana"));
      Contract.Assert(Equals(a[2], "cranberry"));

      Contract.Assert(a.BinarySearch("apple") == 0);
      Contract.Assert(a.BinarySearch("banana") == 1);
      Contract.Assert(a.BinarySearch("cranberry") == 2);
      Contract.Assert(a.BinarySearch("donut") < 0);

      a.Reverse();
      Contract.Assert(a.Count == 3);
      Contract.Assert(Equals(a[2], "apple"));
      Contract.Assert(Equals(a[1], "banana"));
      Contract.Assert(Equals(a[0], "cranberry"));

      a.Remove("apple");
      Contract.Assert(a.Count == 2);
      Contract.Assert(Equals(a[0], "cranberry"));
      Contract.Assert(Equals(a[1], "banana"));

      a.RemoveAt(5);
      a.RemoveAt(0);
      Contract.Assert(a.Count == 1);
      Contract.Assert(Equals(a[0], "banana"));

      a.Clear();
      Contract.Assert(a.Count == 0);

      Contract.Assert(ArrayList.Repeat("carrot", 3).Count == 3);
    }
  }

 
}
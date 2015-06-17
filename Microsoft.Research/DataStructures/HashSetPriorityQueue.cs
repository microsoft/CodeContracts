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
using System.Collections.Generic;

namespace Microsoft.Research.DataStructures
{
  /// <summary>
  /// A heap-based priority queue with a Remove operation ruled by a customizable dictionary
  /// </summary>
  public class HashSetPriorityQueue<T>
  {
    protected static readonly T[] emptyArray = new T[0];

    protected T[] heap = emptyArray;
    protected int count = 0;
    protected readonly IDictionary<T, int> indexInHeap;
    protected readonly IComparer<T> priorityComparer;
    
    private HashSetPriorityQueue(IComparer<T> priorityComparer, IDictionary<T, int> indexInHeap)
    {
      this.priorityComparer = priorityComparer;
      this.indexInHeap = indexInHeap;
    }

    public HashSetPriorityQueue(IComparer<T> priorityComparer, IEqualityComparer<T> hashComparer)
      : this(priorityComparer, new Dictionary<T, int>(hashComparer))
    { }

    public HashSetPriorityQueue()
      : this(Comparer<T>.Default, new Dictionary<T, int>())
    { }

    public static HashSetPriorityQueue<T> Create<TDic>(IComparer<T> priorityComparer)
      where TDic : IDictionary<T, int>, new()
    {
      return new HashSetPriorityQueue<T>(priorityComparer, new TDic());
    }

    public static HashSetPriorityQueue<T> Create<TDic>()
      where TDic : IDictionary<T, int>, new()
    {
      return Create<TDic>(Comparer<T>.Default);
    }

    public int Count { get { return this.count; } }

    public bool Add(T item)
    {
      if (this.indexInHeap.ContainsKey(item))
        return false;

      this.EnsureCapacity(this.count + 2);
      var index = ++this.count;

      while (index > 1)
      {
        var parent = this.heap[index >> 1];
        if (!this.GreaterThan(item, parent))
          break;
        this.heap[index] = parent;
        this.indexInHeap[parent] = index;
        index >>= 1;
      }
      this.heap[index] = item;
      this.indexInHeap[item] = index;

      return true;
    }

    public T Top
    {
      get
      {
        if (this.count == 0)
          throw new InvalidOperationException("SortedHashSet is empty");
        return this.heap[1];
      }
    }

    public bool Remove(T item)
    {
      int index;
      if (!this.indexInHeap.TryGetValue(item, out index))
        return false;
      this.indexInHeap.Remove(item);
      var last = this.heap[this.count];
      this.heap[this.count] = default(T);
      if (index == this.count--)
        return true;
      var indexLeft = index << 1;
      while (indexLeft <= this.count)
      {
        var indexRight = indexLeft + 1;
        var indexLargest = indexRight;
        if (this.GreaterThan(this.heap[indexLeft], last))
        {
          if (indexRight > this.count || !this.GreaterThan(this.heap[indexRight], this.heap[indexLeft]))
            indexLargest = indexLeft;
        }
        else if (indexRight > this.count || !this.GreaterThan(this.heap[indexRight], last))
          break;
        this.heap[index] = this.heap[indexLargest];
        this.indexInHeap[this.heap[index]] = index;
        index = indexLargest;
        indexLeft = index << 1;
      }
      this.heap[index] = last;
      this.indexInHeap[last] = index;
      return true;
    }

    public T Pop()
    {
      var top = this.Top;
      this.Remove(top);
      return top;
    }

    protected void EnsureCapacity(int min)
    {
      if (this.heap.Length >= min)
        return;

      Array.Resize(ref this.heap, Math.Max(min, this.heap.Length * 2));
    }

    protected bool GreaterThan(T x, T y)
    {
      return this.priorityComparer.Compare(x, y) > 0;
    }
  }
}

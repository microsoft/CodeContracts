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

namespace Microsoft.Research.DataStructures
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Text;
  using System.IO;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// A priority queue based on heaps
  /// </summary>
  [ContractVerification(true)]
  public class PriorityQueue<T>
  {

    #region Object invariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.array != null);
      Contract.Invariant(this.basis != null);
      Contract.Invariant(this.compare != null);
    }

    #endregion

    readonly protected List<T> array = new List<T>();
    readonly protected Set<T> basis = new Set<T>();
    readonly Comparison<T> compare;

    /// <summary>
    /// A priority queue where larger elements according to compare come out first
    /// </summary>
    public PriorityQueue(Comparison<T> compare) {
      Contract.Requires(compare  != null);

      this.compare = compare;
    }

    // algorithm is written with array of base 1, so adjust here
    private T this[int index] {
      get {
        Contract.Requires(index >= 1);
        Contract.Requires(index <= array.Count);

        return array[index - 1];
      }

      set {
        Contract.Requires(index >= 1);
        Contract.Requires(index <= array.Count);

        array[index - 1] = value;
      }
    }

    [Pure]
    private bool GreaterThan(T o1, T o2) {
      return this.compare(o1, o2) > 0;
    }

    [Pure]
    private static int Left(int i) {
      Contract.Ensures(Contract.Result<int>() == 2 * i);      

      return 2 * i;
    }

    [Pure]
    private static int Right(int i) {
      Contract.Ensures(Contract.Result<int>() == 2 * i +1);      

      return 2 * i + 1;
    }

    [Pure]
    private static int Parent(int i) {
      Contract.Ensures(Contract.Result<int>() == i/2);      

      return i / 2;
    }

    private int HeapSize {
      get {
        Contract.Ensures(Contract.Result<int>() == this.array.Count);
        return this.array.Count; }
    }

    private void Heapify(int i) {
      Contract.Requires(i>= 1);
      
      int l = Left(i);
      int r = Right(i);
      int largest;

      if (l <= HeapSize && GreaterThan(this[l], this[i])) {
        largest = l;
      }
      else {
        largest = i;
      }
      if (r <= HeapSize && GreaterThan(this[r], this[largest])) {
        largest = r;
      }
      if (largest != i) {
        T tmp = this[largest];
        this[largest] = this[i];
        this[i] = tmp;
        Heapify(largest);
      }
    }

    public void Dump(TextWriter tw)
    {
      Contract.Requires(tw != null);

      tw.Write("  basis: ");
      foreach (var elem in this.basis)
      {
        tw.Write("{0} ", elem.ToString());
      }
      tw.WriteLine();
      tw.Write("  array: ");
      for (int i = 0; i < this.HeapSize; i++)
      {
        tw.Write("{0} ", this.array[i].ToString());
      }
      tw.WriteLine();
    }

    public int Count { get { return this.basis.Count; } }

    public bool Add(T o) {
      if (basis.AddQ(o)) {
        array.Add(o);
        int i = HeapSize;
        while (i > 1 && GreaterThan(o, this[Parent(i)])) {
          this[i] = this[Parent(i)];
          i = Parent(i);
        }
        this[i] = o;

        return true;
      }
      return false;
    }

    public T Pull() {
#if false
      Console.WriteLine("Priority Queue Pull");
      Dump(Console.Out);
#endif
      if (HeapSize < 1) throw new InvalidOperationException("priority queue is empty");

      T max = this[1];
      this[1] = this[HeapSize];
      array.RemoveAt(HeapSize - 1); // remove last element.

      Heapify(1);
      basis.Remove(max);
#if false
      Console.WriteLine("  returning {0}", max.ToString());
#endif
      return max;
    }


  }


}
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

// File System.Collections.Concurrent.BlockingCollection_1.cs
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


namespace System.Collections.Concurrent
{
  public partial class BlockingCollection<T> : IEnumerable<T>, System.Collections.ICollection, System.Collections.IEnumerable, IDisposable
  {
    #region Methods and constructors
    public void Add(T item, System.Threading.CancellationToken cancellationToken)
    {
    }

    public void Add(T item)
    {
    }

    public static int AddToAny(System.Collections.Concurrent.BlockingCollection<T>[] collections, T item)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public static int AddToAny(System.Collections.Concurrent.BlockingCollection<T>[] collections, T item, System.Threading.CancellationToken cancellationToken)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public BlockingCollection()
    {
    }

    public BlockingCollection(int boundedCapacity)
    {
    }

    public BlockingCollection(IProducerConsumerCollection<T> collection, int boundedCapacity)
    {
      Contract.Ensures((Contract.OldValue(collection.Count) - boundedCapacity) <= 0);
    }

    public BlockingCollection(IProducerConsumerCollection<T> collection)
    {
    }

    public void CompleteAdding()
    {
    }

    public void CopyTo(T[] array, int index)
    {
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public void Dispose()
    {
    }

    public IEnumerable<T> GetConsumingEnumerable(System.Threading.CancellationToken cancellationToken)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.IEnumerable<T>>() != null);

      return default(IEnumerable<T>);
    }

    public IEnumerable<T> GetConsumingEnumerable()
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.IEnumerable<T>>() != null);

      return default(IEnumerable<T>);
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

    public T Take()
    {
      return default(T);
    }

    public T Take(System.Threading.CancellationToken cancellationToken)
    {
      return default(T);
    }

    public static int TakeFromAny(System.Collections.Concurrent.BlockingCollection<T>[] collections, out T item)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      item = default(T);

      return default(int);
    }

    public static int TakeFromAny(System.Collections.Concurrent.BlockingCollection<T>[] collections, out T item, System.Threading.CancellationToken cancellationToken)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      item = default(T);

      return default(int);
    }

    public T[] ToArray()
    {
      return default(T[]);
    }

    public bool TryAdd(T item)
    {
      return default(bool);
    }

    public bool TryAdd(T item, int millisecondsTimeout)
    {
      return default(bool);
    }

    public bool TryAdd(T item, int millisecondsTimeout, System.Threading.CancellationToken cancellationToken)
    {
      return default(bool);
    }

    public bool TryAdd(T item, TimeSpan timeout)
    {
      return default(bool);
    }

    public static int TryAddToAny(System.Collections.Concurrent.BlockingCollection<T>[] collections, T item, int millisecondsTimeout, System.Threading.CancellationToken cancellationToken)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public static int TryAddToAny(System.Collections.Concurrent.BlockingCollection<T>[] collections, T item, TimeSpan timeout)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public static int TryAddToAny(System.Collections.Concurrent.BlockingCollection<T>[] collections, T item)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public static int TryAddToAny(System.Collections.Concurrent.BlockingCollection<T>[] collections, T item, int millisecondsTimeout)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public bool TryTake(out T item, int millisecondsTimeout)
    {
      item = default(T);

      return default(bool);
    }

    public bool TryTake(out T item, int millisecondsTimeout, System.Threading.CancellationToken cancellationToken)
    {
      item = default(T);

      return default(bool);
    }

    public bool TryTake(out T item, TimeSpan timeout)
    {
      item = default(T);

      return default(bool);
    }

    public bool TryTake(out T item)
    {
      item = default(T);

      return default(bool);
    }

    public static int TryTakeFromAny(System.Collections.Concurrent.BlockingCollection<T>[] collections, out T item)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      item = default(T);

      return default(int);
    }

    public static int TryTakeFromAny(System.Collections.Concurrent.BlockingCollection<T>[] collections, out T item, int millisecondsTimeout)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      item = default(T);

      return default(int);
    }

    public static int TryTakeFromAny(System.Collections.Concurrent.BlockingCollection<T>[] collections, out T item, int millisecondsTimeout, System.Threading.CancellationToken cancellationToken)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      item = default(T);

      return default(int);
    }

    public static int TryTakeFromAny(System.Collections.Concurrent.BlockingCollection<T>[] collections, out T item, TimeSpan timeout)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      item = default(T);

      return default(int);
    }
    #endregion

    #region Properties and indexers
    public int BoundedCapacity
    {
      get
      {
        return default(int);
      }
    }

    public int Count
    {
      get
      {
        return default(int);
      }
    }

    public bool IsAddingCompleted
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsCompleted
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

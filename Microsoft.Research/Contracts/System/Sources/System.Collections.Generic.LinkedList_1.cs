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

// File System.Collections.Generic.LinkedList_1.cs
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
  public partial class LinkedList<T> : ICollection<T>, IEnumerable<T>, System.Collections.ICollection, System.Collections.IEnumerable, System.Runtime.Serialization.ISerializable, System.Runtime.Serialization.IDeserializationCallback
  {
    #region Methods and constructors
    public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
    {
    }

    public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.LinkedListNode<T>>() != null);

      return default(LinkedListNode<T>);
    }

    public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
    {
    }

    public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.LinkedListNode<T>>() != null);

      return default(LinkedListNode<T>);
    }

    public LinkedListNode<T> AddFirst(T value)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.LinkedListNode<T>>() != null);

      return default(LinkedListNode<T>);
    }

    public void AddFirst(LinkedListNode<T> node)
    {
    }

    public LinkedListNode<T> AddLast(T value)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.LinkedListNode<T>>() != null);

      return default(LinkedListNode<T>);
    }

    public void AddLast(LinkedListNode<T> node)
    {
    }

    public void Clear()
    {
    }

    public bool Contains(T value)
    {
      return default(bool);
    }

    public void CopyTo(T[] array, int index)
    {
    }

    public LinkedListNode<T> Find(T value)
    {
      return default(LinkedListNode<T>);
    }

    public LinkedListNode<T> FindLast(T value)
    {
      return default(LinkedListNode<T>);
    }

    public System.Collections.Generic.LinkedList<T>.Enumerator GetEnumerator()
    {
      return default(System.Collections.Generic.LinkedList<T>.Enumerator);
    }

    public virtual new void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public LinkedList()
    {
    }

    public LinkedList(IEnumerable<T> collection)
    {
    }

    protected LinkedList(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public virtual new void OnDeserialization(Object sender)
    {
    }

    public void Remove(LinkedListNode<T> node)
    {
    }

    public bool Remove(T value)
    {
      return default(bool);
    }

    public void RemoveFirst()
    {
    }

    public void RemoveLast()
    {
    }

    void System.Collections.Generic.ICollection<T>.Add(T value)
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
    #endregion

    #region Properties and indexers
    public int Count
    {
      get
      {
        return default(int);
      }
    }

    public LinkedListNode<T> First
    {
      get
      {
        return default(LinkedListNode<T>);
      }
    }

    public LinkedListNode<T> Last
    {
      get
      {
        return default(LinkedListNode<T>);
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

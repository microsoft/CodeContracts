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

// File System.Windows.Data.CompositeCollection.cs
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


namespace System.Windows.Data
{
  public partial class CompositeCollection : System.Collections.IList, System.Collections.ICollection, System.Collections.IEnumerable, System.Collections.Specialized.INotifyCollectionChanged, System.ComponentModel.ICollectionViewFactory, System.Windows.IWeakEventListener
  {
    #region Methods and constructors
    public int Add(Object newItem)
    {
      return default(int);
    }

    public void Clear()
    {
    }

    public CompositeCollection()
    {
    }

    public CompositeCollection(int capacity)
    {
      Contract.Requires(capacity >= 0);
    }

    public bool Contains(Object containItem)
    {
      return default(bool);
    }

    public void CopyTo(Array array, int index)
    {
    }

    public int IndexOf(Object indexItem)
    {
      return default(int);
    }

    public void Insert(int insertIndex, Object insertItem)
    {
    }

    protected virtual new bool ReceiveWeakEvent(Type managerType, Object sender, EventArgs e)
    {
      Contract.Requires(e != null);
      return default(bool);
    }

    public void Remove(Object removeItem)
    {
    }

    public void RemoveAt(int removeIndex)
    {
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    System.ComponentModel.ICollectionView System.ComponentModel.ICollectionViewFactory.CreateView()
    {
      return default(System.ComponentModel.ICollectionView);
    }

    bool System.Windows.IWeakEventListener.ReceiveWeakEvent(Type managerType, Object sender, EventArgs e)
    {
      return default(bool);
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

    public Object this [int itemIndex]
    {
      get
      {
        return default(Object);
      }
      set
      {
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
    #endregion

    #region Events
    protected event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    event System.Collections.Specialized.NotifyCollectionChangedEventHandler System.Collections.Specialized.INotifyCollectionChanged.CollectionChanged
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}

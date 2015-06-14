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

// File System.Windows.FreezableCollection_1.cs
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


namespace System.Windows
{
  public partial class FreezableCollection<T> : System.Windows.Media.Animation.Animatable, System.Collections.IList, System.Collections.ICollection, IList<T>, ICollection<T>, IEnumerable<T>, System.Collections.IEnumerable, System.Collections.Specialized.INotifyCollectionChanged, System.ComponentModel.INotifyPropertyChanged
  {
    #region Methods and constructors
    public void Add(T value)
    {
    }

    public void Clear()
    {
    }

    public System.Windows.FreezableCollection<T> Clone()
    {
      return default(System.Windows.FreezableCollection<T>);
    }

    protected override void CloneCore(Freezable source)
    {
    }

    public System.Windows.FreezableCollection<T> CloneCurrentValue()
    {
      return default(System.Windows.FreezableCollection<T>);
    }

    protected override void CloneCurrentValueCore(Freezable source)
    {
    }

    public bool Contains(T value)
    {
      return default(bool);
    }

    public void CopyTo(T[] array, int index)
    {
    }

    protected override Freezable CreateInstanceCore()
    {
      return default(Freezable);
    }

    public FreezableCollection()
    {
    }

    public FreezableCollection(int capacity)
    {
    }

    public FreezableCollection(IEnumerable<T> collection)
    {
    }

    protected override bool FreezeCore(bool isChecking)
    {
      return default(bool);
    }

    protected override void GetAsFrozenCore(Freezable source)
    {
    }

    protected override void GetCurrentValueAsFrozenCore(Freezable source)
    {
    }

    public System.Windows.FreezableCollection<T>.Enumerator GetEnumerator()
    {
      return default(System.Windows.FreezableCollection<T>.Enumerator);
    }

    public int IndexOf(T value)
    {
      return default(int);
    }

    public void Insert(int index, T value)
    {
    }

    public bool Remove(T value)
    {
      return default(bool);
    }

    public void RemoveAt(int index)
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

    int System.Collections.IList.Add(Object value)
    {
      return default(int);
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
    #endregion

    #region Properties and indexers
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

    #region Events
    event System.Collections.Specialized.NotifyCollectionChangedEventHandler System.Collections.Specialized.INotifyCollectionChanged.CollectionChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    event System.ComponentModel.PropertyChangedEventHandler System.ComponentModel.INotifyPropertyChanged.PropertyChanged
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

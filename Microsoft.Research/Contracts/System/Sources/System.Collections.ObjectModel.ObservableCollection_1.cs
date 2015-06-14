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

// File System.Collections.ObjectModel.ObservableCollection_1.cs
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


namespace System.Collections.ObjectModel
{
  public partial class ObservableCollection<T> : Collection<T>, System.Collections.Specialized.INotifyCollectionChanged, System.ComponentModel.INotifyPropertyChanged
  {
    #region Methods and constructors
    protected IDisposable BlockReentrancy()
    {
      Contract.Ensures(Contract.Result<System.IDisposable>() != null);

      return default(IDisposable);
    }

    protected void CheckReentrancy()
    {
    }

    protected override void ClearItems()
    {
    }

    protected override void InsertItem(int index, T item)
    {
    }

    public void Move(int oldIndex, int newIndex)
    {
    }

    protected virtual new void MoveItem(int oldIndex, int newIndex)
    {
    }

    public ObservableCollection()
    {
    }

    public ObservableCollection(IEnumerable<T> collection)
    {
    }

    public ObservableCollection(List<T> list)
    {
    }

    protected virtual new void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
    }

    protected virtual new void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
    {
    }

    protected override void RemoveItem(int index)
    {
    }

    protected override void SetItem(int index, T item)
    {
    }
    #endregion

    #region Events
    public virtual new event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    protected virtual new event System.ComponentModel.PropertyChangedEventHandler PropertyChanged
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

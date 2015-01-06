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

// File System.ComponentModel.BindingList_1.cs
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


namespace System.ComponentModel
{
  public partial class BindingList<T> : System.Collections.ObjectModel.Collection<T>, IBindingList, System.Collections.IList, System.Collections.ICollection, System.Collections.IEnumerable, ICancelAddNew, IRaiseItemChangedEvents
  {
    #region Methods and constructors
    public T AddNew()
    {
      Contract.Ensures(Contract.Result<T>() != null);

      return default(T);
    }

    protected virtual new Object AddNewCore()
    {
      return default(Object);
    }

    protected virtual new void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    {
    }

    public BindingList()
    {
    }

    public BindingList(IList<T> list)
    {
    }

    public virtual new void CancelNew(int itemIndex)
    {
    }

    protected override void ClearItems()
    {
    }

    public virtual new void EndNew(int itemIndex)
    {
    }

    protected virtual new int FindCore(PropertyDescriptor prop, Object key)
    {
      return default(int);
    }

    protected override void InsertItem(int index, T item)
    {
    }

    protected virtual new void OnAddingNew(AddingNewEventArgs e)
    {
    }

    protected virtual new void OnListChanged(ListChangedEventArgs e)
    {
    }

    protected override void RemoveItem(int index)
    {
    }

    protected virtual new void RemoveSortCore()
    {
    }

    public void ResetBindings()
    {
    }

    public void ResetItem(int position)
    {
    }

    protected override void SetItem(int index, T item)
    {
    }

    void System.ComponentModel.IBindingList.AddIndex(PropertyDescriptor prop)
    {
    }

    Object System.ComponentModel.IBindingList.AddNew()
    {
      return default(Object);
    }

    void System.ComponentModel.IBindingList.ApplySort(PropertyDescriptor prop, ListSortDirection direction)
    {
    }

    int System.ComponentModel.IBindingList.Find(PropertyDescriptor prop, Object key)
    {
      return default(int);
    }

    void System.ComponentModel.IBindingList.RemoveIndex(PropertyDescriptor prop)
    {
    }

    void System.ComponentModel.IBindingList.RemoveSort()
    {
    }
    #endregion

    #region Properties and indexers
    public bool AllowEdit
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool AllowNew
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool AllowRemove
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    protected virtual new bool IsSortedCore
    {
      get
      {
        return default(bool);
      }
    }

    public bool RaiseListChangedEvents
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    protected virtual new ListSortDirection SortDirectionCore
    {
      get
      {
        return default(ListSortDirection);
      }
    }

    protected virtual new PropertyDescriptor SortPropertyCore
    {
      get
      {
        return default(PropertyDescriptor);
      }
    }

    protected virtual new bool SupportsChangeNotificationCore
    {
      get
      {
        return default(bool);
      }
    }

    protected virtual new bool SupportsSearchingCore
    {
      get
      {
        return default(bool);
      }
    }

    protected virtual new bool SupportsSortingCore
    {
      get
      {
        return default(bool);
      }
    }

    bool System.ComponentModel.IBindingList.AllowEdit
    {
      get
      {
        return default(bool);
      }
    }

    bool System.ComponentModel.IBindingList.AllowNew
    {
      get
      {
        return default(bool);
      }
    }

    bool System.ComponentModel.IBindingList.AllowRemove
    {
      get
      {
        return default(bool);
      }
    }

    bool System.ComponentModel.IBindingList.IsSorted
    {
      get
      {
        return default(bool);
      }
    }

    ListSortDirection System.ComponentModel.IBindingList.SortDirection
    {
      get
      {
        return default(ListSortDirection);
      }
    }

    PropertyDescriptor System.ComponentModel.IBindingList.SortProperty
    {
      get
      {
        return default(PropertyDescriptor);
      }
    }

    bool System.ComponentModel.IBindingList.SupportsChangeNotification
    {
      get
      {
        return default(bool);
      }
    }

    bool System.ComponentModel.IBindingList.SupportsSearching
    {
      get
      {
        return default(bool);
      }
    }

    bool System.ComponentModel.IBindingList.SupportsSorting
    {
      get
      {
        return default(bool);
      }
    }

    bool System.ComponentModel.IRaiseItemChangedEvents.RaisesItemChangedEvents
    {
      get
      {
        return default(bool);
      }
    }
    #endregion

    #region Events
    public event AddingNewEventHandler AddingNew
    {
      add
      {
      }
      remove
      {
      }
    }

    public event ListChangedEventHandler ListChanged
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

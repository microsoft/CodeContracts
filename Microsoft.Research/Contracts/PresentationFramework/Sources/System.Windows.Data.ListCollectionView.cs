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

// File System.Windows.Data.ListCollectionView.cs
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
  public partial class ListCollectionView : CollectionView, System.Collections.IComparer, System.ComponentModel.IEditableCollectionViewAddNewItem, System.ComponentModel.IEditableCollectionView, System.ComponentModel.IItemProperties
  {
    #region Methods and constructors
    public Object AddNew()
    {
      return default(Object);
    }

    public Object AddNewItem(Object newItem)
    {
      return default(Object);
    }

    public void CancelEdit()
    {
    }

    public void CancelNew()
    {
    }

    public void CommitEdit()
    {
    }

    public void CommitNew()
    {
    }

    protected virtual new int Compare(Object o1, Object o2)
    {
      return default(int);
    }

    public override bool Contains(Object item)
    {
      return default(bool);
    }

    public void EditItem(Object item)
    {
    }

    protected override System.Collections.IEnumerator GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public override Object GetItemAt(int index)
    {
      return default(Object);
    }

    public override int IndexOf(Object item)
    {
      return default(int);
    }

    protected bool InternalContains(Object item)
    {
      return default(bool);
    }

    protected System.Collections.IEnumerator InternalGetEnumerator()
    {
      Contract.Ensures(Contract.Result<System.Collections.IEnumerator>() != null);

      return default(System.Collections.IEnumerator);
    }

    protected int InternalIndexOf(Object item)
    {
      return default(int);
    }

    protected Object InternalItemAt(int index)
    {
      return default(Object);
    }

    public ListCollectionView(System.Collections.IList list) : base (default(System.Collections.IEnumerable))
    {
    }

    public override bool MoveCurrentToPosition(int position)
    {
      return default(bool);
    }

    protected override void OnBeginChangeLogging(System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
    {
    }

    public override bool PassesFilter(Object item)
    {
      return default(bool);
    }

    protected override void ProcessCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
    {
    }

    protected override void RefreshOverride()
    {
    }

    public void Remove(Object item)
    {
    }

    public void RemoveAt(int index)
    {
    }

    int System.Collections.IComparer.Compare(Object o1, Object o2)
    {
      return default(int);
    }
    #endregion

    #region Properties and indexers
    protected System.Collections.IComparer ActiveComparer
    {
      get
      {
        return default(System.Collections.IComparer);
      }
      set
      {
      }
    }

    protected Predicate<Object> ActiveFilter
    {
      get
      {
        return default(Predicate<Object>);
      }
      set
      {
      }
    }

    public bool CanAddNew
    {
      get
      {
        return default(bool);
      }
    }

    public bool CanAddNewItem
    {
      get
      {
        return default(bool);
      }
    }

    public bool CanCancelEdit
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanFilter
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanGroup
    {
      get
      {
        return default(bool);
      }
    }

    public bool CanRemove
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanSort
    {
      get
      {
        return default(bool);
      }
    }

    public override int Count
    {
      get
      {
        return default(int);
      }
    }

    public Object CurrentAddItem
    {
      get
      {
        return default(Object);
      }
    }

    public Object CurrentEditItem
    {
      get
      {
        return default(Object);
      }
    }

    public System.Collections.IComparer CustomSort
    {
      get
      {
        return default(System.Collections.IComparer);
      }
      set
      {
      }
    }

    public override Predicate<Object> Filter
    {
      get
      {
        return default(Predicate<Object>);
      }
      set
      {
      }
    }

    public virtual new GroupDescriptionSelectorCallback GroupBySelector
    {
      get
      {
        return default(GroupDescriptionSelectorCallback);
      }
      set
      {
      }
    }

    public override System.Collections.ObjectModel.ObservableCollection<System.ComponentModel.GroupDescription> GroupDescriptions
    {
      get
      {
        return default(System.Collections.ObjectModel.ObservableCollection<System.ComponentModel.GroupDescription>);
      }
    }

    public override System.Collections.ObjectModel.ReadOnlyObservableCollection<Object> Groups
    {
      get
      {
        return default(System.Collections.ObjectModel.ReadOnlyObservableCollection<Object>);
      }
    }

    protected int InternalCount
    {
      get
      {
        return default(int);
      }
    }

    protected System.Collections.IList InternalList
    {
      get
      {
        return default(System.Collections.IList);
      }
    }

    public bool IsAddingNew
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsDataInGroupOrder
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsEditingItem
    {
      get
      {
        return default(bool);
      }
    }

    public override bool IsEmpty
    {
      get
      {
        return default(bool);
      }
    }

    protected bool IsGrouping
    {
      get
      {
        return default(bool);
      }
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<System.ComponentModel.ItemPropertyInfo> ItemProperties
    {
      get
      {
        return default(System.Collections.ObjectModel.ReadOnlyCollection<System.ComponentModel.ItemPropertyInfo>);
      }
    }

    public System.ComponentModel.NewItemPlaceholderPosition NewItemPlaceholderPosition
    {
      get
      {
        return default(System.ComponentModel.NewItemPlaceholderPosition);
      }
      set
      {
      }
    }

    public override System.ComponentModel.SortDescriptionCollection SortDescriptions
    {
      get
      {
        return default(System.ComponentModel.SortDescriptionCollection);
      }
    }

    protected bool UsesLocalArray
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}

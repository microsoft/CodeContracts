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

// File System.Windows.Controls.ItemCollection.cs
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


namespace System.Windows.Controls
{
  sealed public partial class ItemCollection : System.Windows.Data.CollectionView, System.Collections.IList, System.Collections.ICollection, System.Collections.IEnumerable, System.ComponentModel.IEditableCollectionViewAddNewItem, System.ComponentModel.IEditableCollectionView, System.ComponentModel.IItemProperties, System.Windows.IWeakEventListener
  {
    #region Methods and constructors
    public int Add(Object newItem)
    {
      return default(int);
    }

    public void Clear()
    {
    }

    public override bool Contains(Object containItem)
    {
      return default(bool);
    }

    public void CopyTo(Array array, int index)
    {
    }

    public override IDisposable DeferRefresh()
    {
      return default(IDisposable);
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

    public void Insert(int insertIndex, Object insertItem)
    {
    }

    internal ItemCollection() : base (default(System.Collections.IEnumerable))
    {
    }

    public override bool MoveCurrentTo(Object item)
    {
      return default(bool);
    }

    public override bool MoveCurrentToFirst()
    {
      return default(bool);
    }

    public override bool MoveCurrentToLast()
    {
      return default(bool);
    }

    public override bool MoveCurrentToNext()
    {
      return default(bool);
    }

    public override bool MoveCurrentToPosition(int position)
    {
      return default(bool);
    }

    public override bool MoveCurrentToPrevious()
    {
      return default(bool);
    }

    public override bool PassesFilter(Object item)
    {
      return default(bool);
    }

    protected override void RefreshOverride()
    {
    }

    public void Remove(Object removeItem)
    {
    }

    public void RemoveAt(int removeIndex)
    {
    }

    Object System.ComponentModel.IEditableCollectionView.AddNew()
    {
      return default(Object);
    }

    void System.ComponentModel.IEditableCollectionView.CancelEdit()
    {
    }

    void System.ComponentModel.IEditableCollectionView.CancelNew()
    {
    }

    void System.ComponentModel.IEditableCollectionView.CommitEdit()
    {
    }

    void System.ComponentModel.IEditableCollectionView.CommitNew()
    {
    }

    void System.ComponentModel.IEditableCollectionView.EditItem(Object item)
    {
    }

    void System.ComponentModel.IEditableCollectionView.Remove(Object item)
    {
    }

    void System.ComponentModel.IEditableCollectionView.RemoveAt(int index)
    {
    }

    Object System.ComponentModel.IEditableCollectionViewAddNewItem.AddNewItem(Object newItem)
    {
      return default(Object);
    }

    bool System.Windows.IWeakEventListener.ReceiveWeakEvent(Type managerType, Object sender, EventArgs e)
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
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

    public override Object CurrentItem
    {
      get
      {
        return default(Object);
      }
    }

    public override int CurrentPosition
    {
      get
      {
        return default(int);
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

    public override bool IsCurrentAfterLast
    {
      get
      {
        return default(bool);
      }
    }

    public override bool IsCurrentBeforeFirst
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

    public Object this [int index]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public override bool NeedsRefresh
    {
      get
      {
        return default(bool);
      }
    }

    public override System.ComponentModel.SortDescriptionCollection SortDescriptions
    {
      get
      {
        return default(System.ComponentModel.SortDescriptionCollection);
      }
    }

    public override System.Collections.IEnumerable SourceCollection
    {
      get
      {
        return default(System.Collections.IEnumerable);
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

    bool System.ComponentModel.IEditableCollectionView.CanAddNew
    {
      get
      {
        return default(bool);
      }
    }

    bool System.ComponentModel.IEditableCollectionView.CanCancelEdit
    {
      get
      {
        return default(bool);
      }
    }

    bool System.ComponentModel.IEditableCollectionView.CanRemove
    {
      get
      {
        return default(bool);
      }
    }

    Object System.ComponentModel.IEditableCollectionView.CurrentAddItem
    {
      get
      {
        return default(Object);
      }
    }

    Object System.ComponentModel.IEditableCollectionView.CurrentEditItem
    {
      get
      {
        return default(Object);
      }
    }

    bool System.ComponentModel.IEditableCollectionView.IsAddingNew
    {
      get
      {
        return default(bool);
      }
    }

    bool System.ComponentModel.IEditableCollectionView.IsEditingItem
    {
      get
      {
        return default(bool);
      }
    }

    System.ComponentModel.NewItemPlaceholderPosition System.ComponentModel.IEditableCollectionView.NewItemPlaceholderPosition
    {
      get
      {
        return default(System.ComponentModel.NewItemPlaceholderPosition);
      }
      set
      {
      }
    }

    bool System.ComponentModel.IEditableCollectionViewAddNewItem.CanAddNewItem
    {
      get
      {
        return default(bool);
      }
    }

    System.Collections.ObjectModel.ReadOnlyCollection<System.ComponentModel.ItemPropertyInfo> System.ComponentModel.IItemProperties.ItemProperties
    {
      get
      {
        return default(System.Collections.ObjectModel.ReadOnlyCollection<System.ComponentModel.ItemPropertyInfo>);
      }
    }
    #endregion
  }
}

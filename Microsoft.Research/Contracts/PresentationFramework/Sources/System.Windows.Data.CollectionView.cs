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

// File System.Windows.Data.CollectionView.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;
using System.ComponentModel;

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
  public partial class CollectionView : System.Windows.Threading.DispatcherObject, System.ComponentModel.ICollectionView, System.Collections.IEnumerable, System.Collections.Specialized.INotifyCollectionChanged, System.ComponentModel.INotifyPropertyChanged
  {
    #region Methods and constructors
    protected void ClearChangeLog()
    {
    }

    public CollectionView(System.Collections.IEnumerable collection)
    {
    }

    public virtual new bool Contains(Object item)
    {
      return default(bool);
    }

    public virtual new IDisposable DeferRefresh()
    {
      return default(IDisposable);
    }

    protected virtual new System.Collections.IEnumerator GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public virtual new Object GetItemAt(int index)
    {
      return default(Object);
    }

    public virtual new int IndexOf(Object item)
    {
      return default(int);
    }

    public virtual new bool MoveCurrentTo(Object item)
    {
      return default(bool);
    }

    public virtual new bool MoveCurrentToFirst()
    {
      return default(bool);
    }

    public virtual new bool MoveCurrentToLast()
    {
      return default(bool);
    }

    public virtual new bool MoveCurrentToNext()
    {
      return default(bool);
    }

    public virtual new bool MoveCurrentToPosition(int position)
    {
      return default(bool);
    }

    public virtual new bool MoveCurrentToPrevious()
    {
      return default(bool);
    }

    protected bool OKToChangeCurrent()
    {
      return default(bool);
    }

    protected virtual new void OnBeginChangeLogging(System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
    {
    }

    protected virtual new void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
    {
    }

    protected void OnCollectionChanged(Object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
    {
    }

    protected virtual new void OnCurrentChanged()
    {
    }

    protected virtual new void OnCurrentChanging(System.ComponentModel.CurrentChangingEventArgs args)
    {
    }

    protected void OnCurrentChanging()
    {
    }

    protected virtual new void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
    {
    }

    public virtual new bool PassesFilter(Object item)
    {
      return default(bool);
    }

    protected virtual new void ProcessCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
    {
      Contract.Requires(args != null);
    }

    public virtual new void Refresh()
    {
    }

    protected void RefreshOrDefer()
    {
    }

    protected virtual new void RefreshOverride()
    {
    }

    protected void SetCurrent(Object newItem, int newPosition)
    {
    }

    protected void SetCurrent(Object newItem, int newPosition, int count)
    {
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }
    #endregion

    #region Properties and indexers
    public virtual new bool CanFilter
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanGroup
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanSort
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new System.Collections.IComparer Comparer
    {
      get
      {
        return default(System.Collections.IComparer);
      }
    }

    public virtual new int Count
    {
      get
      {
        return default(int);
      }
    }

    public virtual new System.Globalization.CultureInfo Culture
    {
      get
      {
        return default(System.Globalization.CultureInfo);
      }
      set
      {
      }
    }

    public virtual new Object CurrentItem
    {
      get
      {
        return default(Object);
      }
    }

    public virtual new int CurrentPosition
    {
      get
      {
        return default(int);
      }
    }

    public virtual new Predicate<Object> Filter
    {
      get
      {
        return default(Predicate<Object>);
      }
      set
      {
      }
    }

    public virtual new System.Collections.ObjectModel.ObservableCollection<System.ComponentModel.GroupDescription> GroupDescriptions
    {
      get
      {
        return default(System.Collections.ObjectModel.ObservableCollection<System.ComponentModel.GroupDescription>);
      }
    }

    public virtual new System.Collections.ObjectModel.ReadOnlyObservableCollection<Object> Groups
    {
      get
      {
        return default(System.Collections.ObjectModel.ReadOnlyObservableCollection<Object>);
      }
    }

    public virtual new bool IsCurrentAfterLast
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsCurrentBeforeFirst
    {
      get
      {
        return default(bool);
      }
    }

    protected bool IsCurrentInSync
    {
      get
      {
        return default(bool);
      }
    }

    protected bool IsDynamic
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsEmpty
    {
      get
      {
        return default(bool);
      }
    }

    protected bool IsRefreshDeferred
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool NeedsRefresh
    {
      get
      {
        return default(bool);
      }
    }

    public static Object NewItemPlaceholder
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Object>() != null);

        return default(Object);
      }
    }

    public virtual new System.ComponentModel.SortDescriptionCollection SortDescriptions
    {
      get
      {
        Contract.Ensures(Contract.Result<SortDescriptionCollection>() != null);

        return default(System.ComponentModel.SortDescriptionCollection);
      }
    }

    public virtual new System.Collections.IEnumerable SourceCollection
    {
      get
      {
        return default(System.Collections.IEnumerable);
      }
    }

    protected bool UpdatedOutsideDispatcher
    {
      get
      {
        return default(bool);
      }
    }
    #endregion

    #region Events
    protected virtual new event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public virtual new event EventHandler CurrentChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public virtual new event System.ComponentModel.CurrentChangingEventHandler CurrentChanging
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

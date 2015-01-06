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

// File System.Windows.Data.CollectionViewSource.cs
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
  public partial class CollectionViewSource : System.Windows.DependencyObject, System.ComponentModel.ISupportInitialize, System.Windows.IWeakEventListener
  {
    #region Methods and constructors
    public CollectionViewSource()
    {
    }

    public IDisposable DeferRefresh()
    {
      Contract.Ensures(Contract.Result<System.IDisposable>() != null);

      return default(IDisposable);
    }

    public static System.ComponentModel.ICollectionView GetDefaultView(Object source)
    {
      return default(System.ComponentModel.ICollectionView);
    }

    public static bool IsDefaultView(System.ComponentModel.ICollectionView view)
    {
      return default(bool);
    }

    protected virtual new void OnCollectionViewTypeChanged(Type oldCollectionViewType, Type newCollectionViewType)
    {
    }

    protected virtual new void OnSourceChanged(Object oldSource, Object newSource)
    {
    }

    protected virtual new bool ReceiveWeakEvent(Type managerType, Object sender, EventArgs e)
    {
      return default(bool);
    }

    void System.ComponentModel.ISupportInitialize.BeginInit()
    {
    }

    void System.ComponentModel.ISupportInitialize.EndInit()
    {
    }

    bool System.Windows.IWeakEventListener.ReceiveWeakEvent(Type managerType, Object sender, EventArgs e)
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public Type CollectionViewType
    {
      get
      {
        return default(Type);
      }
      set
      {
      }
    }

    public System.Globalization.CultureInfo Culture
    {
      get
      {
        return default(System.Globalization.CultureInfo);
      }
      set
      {
      }
    }

    public System.Collections.ObjectModel.ObservableCollection<System.ComponentModel.GroupDescription> GroupDescriptions
    {
      get
      {
        return default(System.Collections.ObjectModel.ObservableCollection<System.ComponentModel.GroupDescription>);
      }
    }

    public System.ComponentModel.SortDescriptionCollection SortDescriptions
    {
      get
      {
        return default(System.ComponentModel.SortDescriptionCollection);
      }
    }

    public Object Source
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public System.ComponentModel.ICollectionView View
    {
      get
      {
        return default(System.ComponentModel.ICollectionView);
      }
    }
    #endregion

    #region Events
    public event FilterEventHandler Filter
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty CollectionViewTypeProperty;
    public readonly static System.Windows.DependencyProperty SourceProperty;
    public readonly static System.Windows.DependencyProperty ViewProperty;
    #endregion
  }
}

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

// File System.Windows.Controls.DataGridColumn.cs
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
  abstract public partial class DataGridColumn : System.Windows.DependencyObject
  {
    #region Methods and constructors
    protected virtual new void CancelCellEdit(System.Windows.FrameworkElement editingElement, Object uneditedValue)
    {
      Contract.Requires(editingElement != null);
    }

    protected virtual new bool CommitCellEdit(System.Windows.FrameworkElement editingElement)
    {
      Contract.Requires(editingElement != null);

      return default(bool);
    }

    protected DataGridColumn()
    {
    }

    protected abstract System.Windows.FrameworkElement GenerateEditingElement(DataGridCell cell, Object dataItem);

    protected abstract System.Windows.FrameworkElement GenerateElement(DataGridCell cell, Object dataItem);

    public System.Windows.FrameworkElement GetCellContent(Object dataItem)
    {
      return default(System.Windows.FrameworkElement);
    }

    public System.Windows.FrameworkElement GetCellContent(DataGridRow dataGridRow)
    {
      return default(System.Windows.FrameworkElement);
    }

    protected void NotifyPropertyChanged(string propertyName)
    {
    }

    protected virtual new bool OnCoerceIsReadOnly(bool baseValue)
    {
      return default(bool);
    }

    public virtual new Object OnCopyingCellClipboardContent(Object item)
    {
      return default(Object);
    }

    public virtual new void OnPastingCellClipboardContent(Object item, Object cellContent)
    {
    }

    protected virtual new Object PrepareCellForEdit(System.Windows.FrameworkElement editingElement, System.Windows.RoutedEventArgs editingEventArgs)
    {
      return default(Object);
    }

    protected internal virtual new void RefreshCellContent(System.Windows.FrameworkElement element, string propertyName)
    {
    }
    #endregion

    #region Properties and indexers
    public double ActualWidth
    {
      get
      {
        return default(double);
      }
      private set
      {
      }
    }

    public bool CanUserReorder
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool CanUserResize
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool CanUserSort
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Windows.Style CellStyle
    {
      get
      {
        return default(System.Windows.Style);
      }
      set
      {
      }
    }

    public virtual new System.Windows.Data.BindingBase ClipboardContentBinding
    {
      get
      {
        return default(System.Windows.Data.BindingBase);
      }
      set
      {
      }
    }

    internal protected DataGrid DataGridOwner
    {
      get
      {
        return default(DataGrid);
      }
      internal set
      {
      }
    }

    public int DisplayIndex
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public System.Windows.Style DragIndicatorStyle
    {
      get
      {
        return default(System.Windows.Style);
      }
      set
      {
      }
    }

    public Object Header
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public string HeaderStringFormat
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Windows.Style HeaderStyle
    {
      get
      {
        return default(System.Windows.Style);
      }
      set
      {
      }
    }

    public System.Windows.DataTemplate HeaderTemplate
    {
      get
      {
        return default(System.Windows.DataTemplate);
      }
      set
      {
      }
    }

    public DataTemplateSelector HeaderTemplateSelector
    {
      get
      {
        return default(DataTemplateSelector);
      }
      set
      {
      }
    }

    public bool IsAutoGenerated
    {
      get
      {
        return default(bool);
      }
      internal set
      {
      }
    }

    public bool IsFrozen
    {
      get
      {
        return default(bool);
      }
      internal set
      {
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public double MaxWidth
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double MinWidth
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public Nullable<System.ComponentModel.ListSortDirection> SortDirection
    {
      get
      {
        return default(Nullable<System.ComponentModel.ListSortDirection>);
      }
      set
      {
      }
    }

    public string SortMemberPath
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Windows.Visibility Visibility
    {
      get
      {
        return default(System.Windows.Visibility);
      }
      set
      {
      }
    }

    public DataGridLength Width
    {
      get
      {
        return default(DataGridLength);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event EventHandler<DataGridCellClipboardEventArgs> CopyingCellClipboardContent
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler<DataGridCellClipboardEventArgs> PastingCellClipboardContent
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
    public readonly static System.Windows.DependencyProperty ActualWidthProperty;
    public readonly static System.Windows.DependencyProperty CanUserReorderProperty;
    public readonly static System.Windows.DependencyProperty CanUserResizeProperty;
    public readonly static System.Windows.DependencyProperty CanUserSortProperty;
    public readonly static System.Windows.DependencyProperty CellStyleProperty;
    public readonly static System.Windows.DependencyProperty DisplayIndexProperty;
    public readonly static System.Windows.DependencyProperty DragIndicatorStyleProperty;
    public readonly static System.Windows.DependencyProperty HeaderProperty;
    public readonly static System.Windows.DependencyProperty HeaderStringFormatProperty;
    public readonly static System.Windows.DependencyProperty HeaderStyleProperty;
    public readonly static System.Windows.DependencyProperty HeaderTemplateProperty;
    public readonly static System.Windows.DependencyProperty HeaderTemplateSelectorProperty;
    public readonly static System.Windows.DependencyProperty IsAutoGeneratedProperty;
    public readonly static System.Windows.DependencyProperty IsFrozenProperty;
    public readonly static System.Windows.DependencyProperty IsReadOnlyProperty;
    public readonly static System.Windows.DependencyProperty MaxWidthProperty;
    public readonly static System.Windows.DependencyProperty MinWidthProperty;
    public readonly static System.Windows.DependencyProperty SortDirectionProperty;
    public readonly static System.Windows.DependencyProperty SortMemberPathProperty;
    public readonly static System.Windows.DependencyProperty VisibilityProperty;
    public readonly static System.Windows.DependencyProperty WidthProperty;
    #endregion
  }
}

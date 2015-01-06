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

// File System.Windows.Controls.DataGridRow.cs
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
  public partial class DataGridRow : Control
  {
    #region Methods and constructors
    protected override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeBounds)
    {
      return default(System.Windows.Size);
    }

    public DataGridRow()
    {
    }

    public int GetIndex()
    {
      return default(int);
    }

    public static System.Windows.Controls.DataGridRow GetRowContainingElement(System.Windows.FrameworkElement element)
    {
      return default(System.Windows.Controls.DataGridRow);
    }

    protected internal virtual new void OnColumnsChanged(System.Collections.ObjectModel.ObservableCollection<DataGridColumn> columns, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected virtual new void OnHeaderChanged(Object oldHeader, Object newHeader)
    {
    }

    protected virtual new void OnItemChanged(Object oldItem, Object newItem)
    {
    }

    protected override void OnPropertyChanged(System.Windows.DependencyPropertyChangedEventArgs e)
    {
    }

    protected virtual new void OnSelected(System.Windows.RoutedEventArgs e)
    {
    }

    protected override void OnTemplateChanged(ControlTemplate oldTemplate, ControlTemplate newTemplate)
    {
    }

    protected virtual new void OnUnselected(System.Windows.RoutedEventArgs e)
    {
    }
    #endregion

    #region Properties and indexers
    public int AlternationIndex
    {
      get
      {
        return default(int);
      }
    }

    public System.Windows.DataTemplate DetailsTemplate
    {
      get
      {
        return default(System.Windows.DataTemplate);
      }
      set
      {
      }
    }

    public DataTemplateSelector DetailsTemplateSelector
    {
      get
      {
        return default(DataTemplateSelector);
      }
      set
      {
      }
    }

    public System.Windows.Visibility DetailsVisibility
    {
      get
      {
        return default(System.Windows.Visibility);
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

    public bool IsEditing
    {
      get
      {
        return default(bool);
      }
      internal set
      {
      }
    }

    public bool IsSelected
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public Object Item
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public ItemsPanelTemplate ItemsPanel
    {
      get
      {
        return default(ItemsPanelTemplate);
      }
      set
      {
      }
    }

    public ControlTemplate ValidationErrorTemplate
    {
      get
      {
        return default(ControlTemplate);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event System.Windows.RoutedEventHandler Selected
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.RoutedEventHandler Unselected
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
    public readonly static System.Windows.DependencyProperty AlternationIndexProperty;
    public readonly static System.Windows.DependencyProperty DetailsTemplateProperty;
    public readonly static System.Windows.DependencyProperty DetailsTemplateSelectorProperty;
    public readonly static System.Windows.DependencyProperty DetailsVisibilityProperty;
    public readonly static System.Windows.DependencyProperty HeaderProperty;
    public readonly static System.Windows.DependencyProperty HeaderStyleProperty;
    public readonly static System.Windows.DependencyProperty HeaderTemplateProperty;
    public readonly static System.Windows.DependencyProperty HeaderTemplateSelectorProperty;
    public readonly static System.Windows.DependencyProperty IsEditingProperty;
    public readonly static System.Windows.DependencyProperty IsSelectedProperty;
    public readonly static System.Windows.DependencyProperty ItemProperty;
    public readonly static System.Windows.DependencyProperty ItemsPanelProperty;
    public readonly static System.Windows.RoutedEvent SelectedEvent;
    public readonly static System.Windows.RoutedEvent UnselectedEvent;
    public readonly static System.Windows.DependencyProperty ValidationErrorTemplateProperty;
    #endregion
  }
}

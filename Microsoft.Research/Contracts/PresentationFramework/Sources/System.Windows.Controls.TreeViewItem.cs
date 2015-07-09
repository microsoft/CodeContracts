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

// File System.Windows.Controls.TreeViewItem.cs
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
  public partial class TreeViewItem : HeaderedItemsControl, VirtualizingStackPanel.IProvideStackingSize
  {
    #region Methods and constructors
    public void ExpandSubtree()
    {
    }

    protected override System.Windows.DependencyObject GetContainerForItemOverride()
    {
      return default(System.Windows.DependencyObject);
    }

    protected virtual new void OnCollapsed(System.Windows.RoutedEventArgs e)
    {
      Contract.Requires(e != null);
    }

    protected virtual new void OnExpanded(System.Windows.RoutedEventArgs e)
    {
      Contract.Requires(e != null);
    }


    protected virtual new void OnSelected(System.Windows.RoutedEventArgs e)
    {
      Contract.Requires(e != null);
    }

    protected virtual new void OnUnselected(System.Windows.RoutedEventArgs e)
    {
      Contract.Requires(e != null);
    }


    double System.Windows.Controls.VirtualizingStackPanel.IProvideStackingSize.EstimatedContainerSize(bool isHorizontal)
    {
      return default(double);
    }

    double System.Windows.Controls.VirtualizingStackPanel.IProvideStackingSize.HeaderSize(bool isHorizontal)
    {
      return default(double);
    }

    public TreeViewItem()
    {
    }
    #endregion

    #region Properties and indexers
    public bool IsExpanded
    {
      get
      {
        return default(bool);
      }
      set
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

    public bool IsSelectionActive
    {
      get
      {
        return default(bool);
      }
    }
    #endregion

    #region Events
    public event System.Windows.RoutedEventHandler Collapsed
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.RoutedEventHandler Expanded
    {
      add
      {
      }
      remove
      {
      }
    }

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
    public readonly static System.Windows.RoutedEvent CollapsedEvent;
    public readonly static System.Windows.RoutedEvent ExpandedEvent;
    public readonly static System.Windows.DependencyProperty IsExpandedProperty;
    public readonly static System.Windows.DependencyProperty IsSelectedProperty;
    public readonly static System.Windows.DependencyProperty IsSelectionActiveProperty;
    public readonly static System.Windows.RoutedEvent SelectedEvent;
    public readonly static System.Windows.RoutedEvent UnselectedEvent;
    #endregion
  }
}

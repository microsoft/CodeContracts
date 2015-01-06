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

// File System.Windows.Controls.GridView.cs
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
  public partial class GridView : ViewBase, System.Windows.Markup.IAddChild
  {
    #region Methods and constructors
    protected virtual new void AddChild(Object column)
    {
    }

    protected virtual new void AddText(string text)
    {
    }

    protected internal override void ClearItem(ListViewItem item)
    {
    }

    protected internal override System.Windows.Automation.Peers.IViewAutomationPeer GetAutomationPeer(ListView parent)
    {
      return default(System.Windows.Automation.Peers.IViewAutomationPeer);
    }

    public static GridViewColumnCollection GetColumnCollection(System.Windows.DependencyObject element)
    {
      return default(GridViewColumnCollection);
    }

    public GridView()
    {
    }

    protected internal override void PrepareItem(ListViewItem item)
    {
    }

    public static void SetColumnCollection(System.Windows.DependencyObject element, GridViewColumnCollection collection)
    {
    }

    public static bool ShouldSerializeColumnCollection(System.Windows.DependencyObject obj)
    {
      return default(bool);
    }

    void System.Windows.Markup.IAddChild.AddChild(Object column)
    {
    }

    void System.Windows.Markup.IAddChild.AddText(string text)
    {
    }

    #endregion

    #region Properties and indexers
    public bool AllowsColumnReorder
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Windows.Style ColumnHeaderContainerStyle
    {
      get
      {
        return default(System.Windows.Style);
      }
      set
      {
      }
    }

    public ContextMenu ColumnHeaderContextMenu
    {
      get
      {
        return default(ContextMenu);
      }
      set
      {
      }
    }

    public string ColumnHeaderStringFormat
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Windows.DataTemplate ColumnHeaderTemplate
    {
      get
      {
        return default(System.Windows.DataTemplate);
      }
      set
      {
      }
    }

    public DataTemplateSelector ColumnHeaderTemplateSelector
    {
      get
      {
        return default(DataTemplateSelector);
      }
      set
      {
      }
    }

    public Object ColumnHeaderToolTip
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public GridViewColumnCollection Columns
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Controls.GridViewColumnCollection>() != null);

        return default(GridViewColumnCollection);
      }
    }

    internal protected override Object DefaultStyleKey
    {
      get
      {
        return default(Object);
      }
    }

    public static System.Windows.ResourceKey GridViewItemContainerStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(System.Windows.ResourceKey);
      }
    }

    public static System.Windows.ResourceKey GridViewScrollViewerStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(System.Windows.ResourceKey);
      }
    }

    public static System.Windows.ResourceKey GridViewStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(System.Windows.ResourceKey);
      }
    }

    internal protected override Object ItemContainerDefaultStyleKey
    {
      get
      {
        return default(Object);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty AllowsColumnReorderProperty;
    public readonly static System.Windows.DependencyProperty ColumnCollectionProperty;
    public readonly static System.Windows.DependencyProperty ColumnHeaderContainerStyleProperty;
    public readonly static System.Windows.DependencyProperty ColumnHeaderContextMenuProperty;
    public readonly static System.Windows.DependencyProperty ColumnHeaderStringFormatProperty;
    public readonly static System.Windows.DependencyProperty ColumnHeaderTemplateProperty;
    public readonly static System.Windows.DependencyProperty ColumnHeaderTemplateSelectorProperty;
    public readonly static System.Windows.DependencyProperty ColumnHeaderToolTipProperty;
    #endregion
  }
}

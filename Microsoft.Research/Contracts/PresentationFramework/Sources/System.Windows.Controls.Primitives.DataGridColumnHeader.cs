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

// File System.Windows.Controls.Primitives.DataGridColumnHeader.cs
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


namespace System.Windows.Controls.Primitives
{
  public partial class DataGridColumnHeader : ButtonBase, System.Windows.Controls.IProvideDataGridColumn
  {
    #region Methods and constructors
    public DataGridColumnHeader()
    {
    }

    public override void OnApplyTemplate()
    {
    }

    protected override void OnClick()
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected override void OnLostMouseCapture(System.Windows.Input.MouseEventArgs e)
    {
    }

    protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected override void OnMouseLeftButtonUp(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
    {
    }
    #endregion

    #region Properties and indexers
    public bool CanUserSort
    {
      get
      {
        return default(bool);
      }
    }

    public System.Windows.Controls.DataGridColumn Column
    {
      get
      {
        return default(System.Windows.Controls.DataGridColumn);
      }
    }

    public static System.Windows.ComponentResourceKey ColumnFloatingHeaderStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ComponentResourceKey>() != null);

        return default(System.Windows.ComponentResourceKey);
      }
    }

    public static System.Windows.ComponentResourceKey ColumnHeaderDropSeparatorStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ComponentResourceKey>() != null);

        return default(System.Windows.ComponentResourceKey);
      }
    }

    public int DisplayIndex
    {
      get
      {
        return default(int);
      }
    }

    public bool IsFrozen
    {
      get
      {
        return default(bool);
      }
    }

    public System.Windows.Media.Brush SeparatorBrush
    {
      get
      {
        return default(System.Windows.Media.Brush);
      }
      set
      {
      }
    }

    public System.Windows.Visibility SeparatorVisibility
    {
      get
      {
        return default(System.Windows.Visibility);
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
    }

    System.Windows.Controls.DataGridColumn System.Windows.Controls.IProvideDataGridColumn.Column
    {
      get
      {
        return default(System.Windows.Controls.DataGridColumn);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty CanUserSortProperty;
    public readonly static System.Windows.DependencyProperty DisplayIndexProperty;
    public readonly static System.Windows.DependencyProperty IsFrozenProperty;
    public readonly static System.Windows.DependencyProperty SeparatorBrushProperty;
    public readonly static System.Windows.DependencyProperty SeparatorVisibilityProperty;
    public readonly static System.Windows.DependencyProperty SortDirectionProperty;
    #endregion
  }
}

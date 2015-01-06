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

// File System.Windows.Controls.DataGridComboBoxColumn.cs
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
  public partial class DataGridComboBoxColumn : DataGridColumn
  {
    #region Methods and constructors
    public DataGridComboBoxColumn()
    {
    }

    protected override System.Windows.FrameworkElement GenerateEditingElement(DataGridCell cell, Object dataItem)
    {
      return default(System.Windows.FrameworkElement);
    }

    protected override System.Windows.FrameworkElement GenerateElement(DataGridCell cell, Object dataItem)
    {
      return default(System.Windows.FrameworkElement);
    }

    protected override bool OnCoerceIsReadOnly(bool baseValue)
    {
      return default(bool);
    }

    protected virtual new void OnSelectedItemBindingChanged(System.Windows.Data.BindingBase oldBinding, System.Windows.Data.BindingBase newBinding)
    {
    }

    protected virtual new void OnSelectedValueBindingChanged(System.Windows.Data.BindingBase oldBinding, System.Windows.Data.BindingBase newBinding)
    {
    }

    protected virtual new void OnTextBindingChanged(System.Windows.Data.BindingBase oldBinding, System.Windows.Data.BindingBase newBinding)
    {
    }

    protected override Object PrepareCellForEdit(System.Windows.FrameworkElement editingElement, System.Windows.RoutedEventArgs editingEventArgs)
    {
      return default(Object);
    }

    protected internal override void RefreshCellContent(System.Windows.FrameworkElement element, string propertyName)
    {
    }
    #endregion

    #region Properties and indexers
    public override System.Windows.Data.BindingBase ClipboardContentBinding
    {
      get
      {
        return default(System.Windows.Data.BindingBase);
      }
      set
      {
      }
    }

    public static System.Windows.Style DefaultEditingElementStyle
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Style>() != null);
        Contract.Ensures(Contract.Result<System.Windows.Style>() == System.Windows.Controls.DataGridComboBoxColumn.DefaultElementStyle);

        return default(System.Windows.Style);
      }
    }

    public static System.Windows.Style DefaultElementStyle
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Style>() != null);

        return default(System.Windows.Style);
      }
    }

    public string DisplayMemberPath
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Windows.Style EditingElementStyle
    {
      get
      {
        return default(System.Windows.Style);
      }
      set
      {
      }
    }

    public System.Windows.Style ElementStyle
    {
      get
      {
        return default(System.Windows.Style);
      }
      set
      {
      }
    }

    public System.Collections.IEnumerable ItemsSource
    {
      get
      {
        return default(System.Collections.IEnumerable);
      }
      set
      {
      }
    }

    public virtual new System.Windows.Data.BindingBase SelectedItemBinding
    {
      get
      {
        return default(System.Windows.Data.BindingBase);
      }
      set
      {
      }
    }

    public virtual new System.Windows.Data.BindingBase SelectedValueBinding
    {
      get
      {
        return default(System.Windows.Data.BindingBase);
      }
      set
      {
      }
    }

    public string SelectedValuePath
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new System.Windows.Data.BindingBase TextBinding
    {
      get
      {
        return default(System.Windows.Data.BindingBase);
      }
      set
      {
      }
    }

    public static System.Windows.ComponentResourceKey TextBlockComboBoxStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ComponentResourceKey>() != null);

        return default(System.Windows.ComponentResourceKey);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty DisplayMemberPathProperty;
    public readonly static System.Windows.DependencyProperty EditingElementStyleProperty;
    public readonly static System.Windows.DependencyProperty ElementStyleProperty;
    public readonly static System.Windows.DependencyProperty ItemsSourceProperty;
    public readonly static System.Windows.DependencyProperty SelectedValuePathProperty;
    #endregion
  }
}

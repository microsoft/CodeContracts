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

// File System.Windows.Controls.DataGridHyperlinkColumn.cs
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
  public partial class DataGridHyperlinkColumn : DataGridBoundColumn
  {
    #region Methods and constructors
    public DataGridHyperlinkColumn()
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

    protected virtual new void OnContentBindingChanged(System.Windows.Data.BindingBase oldBinding, System.Windows.Data.BindingBase newBinding)
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
    public System.Windows.Data.BindingBase ContentBinding
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
        Contract.Ensures(Contract.Result<System.Windows.Style>() == System.Windows.Controls.DataGridTextColumn.DefaultEditingElementStyle);

        return default(System.Windows.Style);
      }
    }

    public static System.Windows.Style DefaultElementStyle
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Style>() != null);
        Contract.Ensures(Contract.Result<System.Windows.Style>() == System.Windows.Controls.DataGridTextColumn.DefaultElementStyle);

        return default(System.Windows.Style);
      }
    }

    public string TargetName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty TargetNameProperty;
    #endregion
  }
}

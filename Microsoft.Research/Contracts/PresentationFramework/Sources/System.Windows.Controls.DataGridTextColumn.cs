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

// File System.Windows.Controls.DataGridTextColumn.cs
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
  public partial class DataGridTextColumn : DataGridBoundColumn
  {
    #region Methods and constructors
    public DataGridTextColumn()
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

    protected override Object PrepareCellForEdit(System.Windows.FrameworkElement editingElement, System.Windows.RoutedEventArgs editingEventArgs)
    {
      return default(Object);
    }

    protected internal override void RefreshCellContent(System.Windows.FrameworkElement element, string propertyName)
    {
    }
    #endregion

    #region Properties and indexers
    public static System.Windows.Style DefaultEditingElementStyle
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Style>() != null);

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

    public System.Windows.Media.FontFamily FontFamily
    {
      get
      {
        return default(System.Windows.Media.FontFamily);
      }
      set
      {
      }
    }

    public double FontSize
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public System.Windows.FontStyle FontStyle
    {
      get
      {
        return default(System.Windows.FontStyle);
      }
      set
      {
      }
    }

    public System.Windows.FontWeight FontWeight
    {
      get
      {
        return default(System.Windows.FontWeight);
      }
      set
      {
      }
    }

    public System.Windows.Media.Brush Foreground
    {
      get
      {
        return default(System.Windows.Media.Brush);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty FontFamilyProperty;
    public readonly static System.Windows.DependencyProperty FontSizeProperty;
    public readonly static System.Windows.DependencyProperty FontStyleProperty;
    public readonly static System.Windows.DependencyProperty FontWeightProperty;
    public readonly static System.Windows.DependencyProperty ForegroundProperty;
    #endregion
  }
}

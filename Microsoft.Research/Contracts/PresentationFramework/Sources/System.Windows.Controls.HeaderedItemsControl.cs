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

// File System.Windows.Controls.HeaderedItemsControl.cs
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
  public partial class HeaderedItemsControl : ItemsControl
  {
    #region Methods and constructors
    public HeaderedItemsControl()
    {
    }

    protected virtual new void OnHeaderChanged(Object oldHeader, Object newHeader)
    {
    }

    protected virtual new void OnHeaderStringFormatChanged(string oldHeaderStringFormat, string newHeaderStringFormat)
    {
    }

    protected virtual new void OnHeaderTemplateChanged(System.Windows.DataTemplate oldHeaderTemplate, System.Windows.DataTemplate newHeaderTemplate)
    {
    }

    protected virtual new void OnHeaderTemplateSelectorChanged(DataTemplateSelector oldHeaderTemplateSelector, DataTemplateSelector newHeaderTemplateSelector)
    {
    }

    #endregion

    #region Properties and indexers
    public bool HasHeader
    {
      get
      {
        return default(bool);
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

    internal protected override System.Collections.IEnumerator LogicalChildren
    {
      get
      {
        return default(System.Collections.IEnumerator);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty HasHeaderProperty;
    public readonly static System.Windows.DependencyProperty HeaderProperty;
    public readonly static System.Windows.DependencyProperty HeaderStringFormatProperty;
    public readonly static System.Windows.DependencyProperty HeaderTemplateProperty;
    public readonly static System.Windows.DependencyProperty HeaderTemplateSelectorProperty;
    #endregion
  }
}

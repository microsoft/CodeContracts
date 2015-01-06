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

// File System.Windows.Controls.ContentPresenter.cs
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
  public partial class ContentPresenter : System.Windows.FrameworkElement
  {
    #region Methods and constructors
    protected override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeSize)
    {
      return default(System.Windows.Size);
    }

    protected virtual new System.Windows.DataTemplate ChooseTemplate()
    {
      return default(System.Windows.DataTemplate);
    }

    public ContentPresenter()
    {
    }

    protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
    {
      return default(System.Windows.Size);
    }

    protected virtual new void OnContentStringFormatChanged(string oldContentStringFormat, string newContentStringFormat)
    {
    }

    protected virtual new void OnContentTemplateChanged(System.Windows.DataTemplate oldContentTemplate, System.Windows.DataTemplate newContentTemplate)
    {
    }

    protected virtual new void OnContentTemplateSelectorChanged(DataTemplateSelector oldContentTemplateSelector, DataTemplateSelector newContentTemplateSelector)
    {
    }

    protected virtual new void OnTemplateChanged(System.Windows.DataTemplate oldTemplate, System.Windows.DataTemplate newTemplate)
    {
    }

    public bool ShouldSerializeContentTemplateSelector()
    {
      Contract.Ensures(Contract.Result<bool>() == false);

      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public Object Content
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public string ContentSource
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string ContentStringFormat
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Windows.DataTemplate ContentTemplate
    {
      get
      {
        return default(System.Windows.DataTemplate);
      }
      set
      {
      }
    }

    public DataTemplateSelector ContentTemplateSelector
    {
      get
      {
        return default(DataTemplateSelector);
      }
      set
      {
      }
    }

    public bool RecognizesAccessKey
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty ContentProperty;
    public readonly static System.Windows.DependencyProperty ContentSourceProperty;
    public readonly static System.Windows.DependencyProperty ContentStringFormatProperty;
    public readonly static System.Windows.DependencyProperty ContentTemplateProperty;
    public readonly static System.Windows.DependencyProperty ContentTemplateSelectorProperty;
    public readonly static System.Windows.DependencyProperty RecognizesAccessKeyProperty;
    #endregion
  }
}

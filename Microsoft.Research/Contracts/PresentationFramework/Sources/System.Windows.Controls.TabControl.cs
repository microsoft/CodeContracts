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

// File System.Windows.Controls.TabControl.cs
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
  public partial class TabControl : System.Windows.Controls.Primitives.Selector
  {
    #region Methods and constructors
    protected override System.Windows.DependencyObject GetContainerForItemOverride()
    {
      return default(System.Windows.DependencyObject);
    }

    protected override bool IsItemItsOwnContainerOverride(Object item)
    {
      return default(bool);
    }

    public override void OnApplyTemplate()
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected override void OnInitialized(EventArgs e)
    {
    }

    protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
    }

    protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
    {
    }

    protected override void OnSelectionChanged(SelectionChangedEventArgs e)
    {
    }

    public TabControl()
    {
    }
    #endregion

    #region Properties and indexers
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

    public Object SelectedContent
    {
      get
      {
        return default(Object);
      }
      internal set
      {
      }
    }

    public string SelectedContentStringFormat
    {
      get
      {
        return default(string);
      }
      internal set
      {
      }
    }

    public System.Windows.DataTemplate SelectedContentTemplate
    {
      get
      {
        return default(System.Windows.DataTemplate);
      }
      internal set
      {
      }
    }

    public DataTemplateSelector SelectedContentTemplateSelector
    {
      get
      {
        return default(DataTemplateSelector);
      }
      internal set
      {
      }
    }

    public Dock TabStripPlacement
    {
      get
      {
        return default(Dock);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty ContentStringFormatProperty;
    public readonly static System.Windows.DependencyProperty ContentTemplateProperty;
    public readonly static System.Windows.DependencyProperty ContentTemplateSelectorProperty;
    public readonly static System.Windows.DependencyProperty SelectedContentProperty;
    public readonly static System.Windows.DependencyProperty SelectedContentStringFormatProperty;
    public readonly static System.Windows.DependencyProperty SelectedContentTemplateProperty;
    public readonly static System.Windows.DependencyProperty SelectedContentTemplateSelectorProperty;
    public readonly static System.Windows.DependencyProperty TabStripPlacementProperty;
    #endregion
  }
}

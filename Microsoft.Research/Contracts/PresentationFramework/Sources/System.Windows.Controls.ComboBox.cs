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

// File System.Windows.Controls.ComboBox.cs
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
  public partial class ComboBox : System.Windows.Controls.Primitives.Selector
  {
    #region Methods and constructors
    public ComboBox()
    {
    }

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

    protected virtual new void OnDropDownClosed(EventArgs e)
    {
    }

    protected virtual new void OnDropDownOpened(EventArgs e)
    {
    }

    protected override void OnIsKeyboardFocusWithinChanged(System.Windows.DependencyPropertyChangedEventArgs e)
    {
    }

    protected override void OnIsMouseCapturedChanged(System.Windows.DependencyPropertyChangedEventArgs e)
    {
    }

    protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
    {
    }

    protected override void OnMouseLeftButtonUp(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected override void OnPreviewKeyDown(System.Windows.Input.KeyEventArgs e)
    {
    }

    protected override void OnSelectionChanged(SelectionChangedEventArgs e)
    {
    }

    protected override void PrepareContainerForItemOverride(System.Windows.DependencyObject element, Object item)
    {
    }
    #endregion

    #region Properties and indexers
    internal protected override bool HandlesScrolling
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsDropDownOpen
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsEditable
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsSelectionBoxHighlighted
    {
      get
      {
        return default(bool);
      }
    }

    public double MaxDropDownHeight
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public Object SelectionBoxItem
    {
      get
      {
        return default(Object);
      }
      private set
      {
      }
    }

    public string SelectionBoxItemStringFormat
    {
      get
      {
        return default(string);
      }
      private set
      {
      }
    }

    public System.Windows.DataTemplate SelectionBoxItemTemplate
    {
      get
      {
        return default(System.Windows.DataTemplate);
      }
      private set
      {
      }
    }

    public bool StaysOpenOnEdit
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string Text
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

    #region Events
    public event EventHandler DropDownClosed
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler DropDownOpened
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
    public readonly static System.Windows.DependencyProperty IsDropDownOpenProperty;
    public readonly static System.Windows.DependencyProperty IsEditableProperty;
    public readonly static System.Windows.DependencyProperty IsReadOnlyProperty;
    public readonly static System.Windows.DependencyProperty MaxDropDownHeightProperty;
    public readonly static System.Windows.DependencyProperty SelectionBoxItemProperty;
    public readonly static System.Windows.DependencyProperty SelectionBoxItemStringFormatProperty;
    public readonly static System.Windows.DependencyProperty SelectionBoxItemTemplateProperty;
    public readonly static System.Windows.DependencyProperty StaysOpenOnEditProperty;
    public readonly static System.Windows.DependencyProperty TextProperty;
    #endregion
  }
}

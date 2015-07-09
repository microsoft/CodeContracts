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

// File System.Windows.Controls.Panel.cs
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
  abstract public partial class Panel : System.Windows.FrameworkElement, System.Windows.Markup.IAddChild
  {
    #region Methods and constructors
    protected virtual new UIElementCollection CreateUIElementCollection(System.Windows.FrameworkElement logicalParent)
    {
      Contract.Ensures(Contract.Result<UIElementCollection>() != null);
      return default(UIElementCollection);
    }

    protected override System.Windows.Media.Visual GetVisualChild(int index)
    {
      return default(System.Windows.Media.Visual);
    }

    public static int GetZIndex(System.Windows.UIElement element)
    {
      return default(int);
    }

    protected virtual new void OnIsItemsHostChanged(bool oldIsItemsHost, bool newIsItemsHost)
    {
    }

    protected override void OnRender(System.Windows.Media.DrawingContext dc)
    {
    }

    protected override void OnVisualChildrenChanged(System.Windows.DependencyObject visualAdded, System.Windows.DependencyObject visualRemoved)
    {
    }

    protected Panel()
    {
    }

    public static void SetZIndex(System.Windows.UIElement element, int value)
    {
    }

    public bool ShouldSerializeChildren()
    {
      return default(bool);
    }

    void System.Windows.Markup.IAddChild.AddChild(Object value)
    {
    }

    void System.Windows.Markup.IAddChild.AddText(string text)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Media.Brush Background
    {
      get
      {
        return default(System.Windows.Media.Brush);
      }
      set
      {
      }
    }

    public UIElementCollection Children
    {
      get
      {
        Contract.Ensures(Contract.Result<UIElementCollection>() != null);
        return default(UIElementCollection);
      }
    }

    protected internal UIElementCollection InternalChildren
    {
      get
      {
        Contract.Ensures(Contract.Result<UIElementCollection>() != null);
        return default(UIElementCollection);
      }
    }


    internal protected virtual new bool HasLogicalOrientation
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsItemsHost
    {
      get
      {
        return default(bool);
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

    internal protected virtual new Orientation LogicalOrientation
    {
      get
      {
        return default(Orientation);
      }
    }

    protected override int VisualChildrenCount
    {
      get
      {
        return default(int);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty BackgroundProperty;
    public readonly static System.Windows.DependencyProperty IsItemsHostProperty;
    public readonly static System.Windows.DependencyProperty ZIndexProperty;
    #endregion
  }
}

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

// File System.Windows.Controls.Primitives.Popup.cs
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
  public partial class Popup : System.Windows.FrameworkElement, System.Windows.Markup.IAddChild
  {
    #region Methods and constructors
    public static void CreateRootPopup(System.Windows.Controls.Primitives.Popup popup, System.Windows.UIElement child)
    {
    }

    protected override System.Windows.DependencyObject GetUIParentCore()
    {
      return default(System.Windows.DependencyObject);
    }

    protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
    {
      return default(System.Windows.Size);
    }

    protected virtual new void OnClosed(EventArgs e)
    {
    }

    protected virtual new void OnOpened(EventArgs e)
    {
    }

    protected override void OnPreviewMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected override void OnPreviewMouseLeftButtonUp(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected override void OnPreviewMouseRightButtonDown(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected override void OnPreviewMouseRightButtonUp(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    public Popup()
    {
    }

    void System.Windows.Markup.IAddChild.AddChild(Object value)
    {
    }

    void System.Windows.Markup.IAddChild.AddText(string text)
    {
    }
    #endregion

    #region Properties and indexers
    public bool AllowsTransparency
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Windows.UIElement Child
    {
      get
      {
        return default(System.Windows.UIElement);
      }
      set
      {
      }
    }

    public CustomPopupPlacementCallback CustomPopupPlacementCallback
    {
      get
      {
        return default(CustomPopupPlacementCallback);
      }
      set
      {
      }
    }

    public bool HasDropShadow
    {
      get
      {
        return default(bool);
      }
    }

    public double HorizontalOffset
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public bool IsOpen
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

    public PlacementMode Placement
    {
      get
      {
        return default(PlacementMode);
      }
      set
      {
      }
    }

    public System.Windows.Rect PlacementRectangle
    {
      get
      {
        return default(System.Windows.Rect);
      }
      set
      {
      }
    }

    public System.Windows.UIElement PlacementTarget
    {
      get
      {
        return default(System.Windows.UIElement);
      }
      set
      {
      }
    }

    public PopupAnimation PopupAnimation
    {
      get
      {
        return default(PopupAnimation);
      }
      set
      {
      }
    }

    public bool StaysOpen
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public double VerticalOffset
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event EventHandler Closed
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler Opened
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
    public readonly static System.Windows.DependencyProperty AllowsTransparencyProperty;
    public readonly static System.Windows.DependencyProperty ChildProperty;
    public readonly static System.Windows.DependencyProperty CustomPopupPlacementCallbackProperty;
    public readonly static System.Windows.DependencyProperty HasDropShadowProperty;
    public readonly static System.Windows.DependencyProperty HorizontalOffsetProperty;
    public readonly static System.Windows.DependencyProperty IsOpenProperty;
    public readonly static System.Windows.DependencyProperty PlacementProperty;
    public readonly static System.Windows.DependencyProperty PlacementRectangleProperty;
    public readonly static System.Windows.DependencyProperty PlacementTargetProperty;
    public readonly static System.Windows.DependencyProperty PopupAnimationProperty;
    public readonly static System.Windows.DependencyProperty StaysOpenProperty;
    public readonly static System.Windows.DependencyProperty VerticalOffsetProperty;
    #endregion
  }
}

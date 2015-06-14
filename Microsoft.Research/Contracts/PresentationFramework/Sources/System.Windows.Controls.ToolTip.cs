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

// File System.Windows.Controls.ToolTip.cs
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
  public partial class ToolTip : ContentControl
  {
    #region Methods and constructors
    protected virtual new void OnClosed(System.Windows.RoutedEventArgs e)
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected virtual new void OnOpened(System.Windows.RoutedEventArgs e)
    {
    }

    protected override void OnVisualParentChanged(System.Windows.DependencyObject oldParent)
    {
    }

    public ToolTip()
    {
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Controls.Primitives.CustomPopupPlacementCallback CustomPopupPlacementCallback
    {
      get
      {
        return default(System.Windows.Controls.Primitives.CustomPopupPlacementCallback);
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
      set
      {
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

    public System.Windows.Controls.Primitives.PlacementMode Placement
    {
      get
      {
        return default(System.Windows.Controls.Primitives.PlacementMode);
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
    public event System.Windows.RoutedEventHandler Closed
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.RoutedEventHandler Opened
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
    public readonly static System.Windows.RoutedEvent ClosedEvent;
    public readonly static System.Windows.DependencyProperty CustomPopupPlacementCallbackProperty;
    public readonly static System.Windows.DependencyProperty HasDropShadowProperty;
    public readonly static System.Windows.DependencyProperty HorizontalOffsetProperty;
    public readonly static System.Windows.DependencyProperty IsOpenProperty;
    public readonly static System.Windows.RoutedEvent OpenedEvent;
    public readonly static System.Windows.DependencyProperty PlacementProperty;
    public readonly static System.Windows.DependencyProperty PlacementRectangleProperty;
    public readonly static System.Windows.DependencyProperty PlacementTargetProperty;
    public readonly static System.Windows.DependencyProperty StaysOpenProperty;
    public readonly static System.Windows.DependencyProperty VerticalOffsetProperty;
    #endregion
  }
}

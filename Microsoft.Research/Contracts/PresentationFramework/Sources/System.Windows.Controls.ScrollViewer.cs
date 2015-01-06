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

// File System.Windows.Controls.ScrollViewer.cs
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
  public partial class ScrollViewer : ContentControl
  {
    #region Methods and constructors
    public static bool GetCanContentScroll(System.Windows.DependencyObject element)
    {
      return default(bool);
    }

    public static ScrollBarVisibility GetHorizontalScrollBarVisibility(System.Windows.DependencyObject element)
    {
      return default(ScrollBarVisibility);
    }

    public static bool GetIsDeferredScrollingEnabled(System.Windows.DependencyObject element)
    {
      return default(bool);
    }

    public static double GetPanningDeceleration(System.Windows.DependencyObject element)
    {
      return default(double);
    }

    public static PanningMode GetPanningMode(System.Windows.DependencyObject element)
    {
      return default(PanningMode);
    }

    public static double GetPanningRatio(System.Windows.DependencyObject element)
    {
      return default(double);
    }

    public static ScrollBarVisibility GetVerticalScrollBarVisibility(System.Windows.DependencyObject element)
    {
      return default(ScrollBarVisibility);
    }

    protected override System.Windows.Media.HitTestResult HitTestCore(System.Windows.Media.PointHitTestParameters hitTestParameters)
    {
      return default(System.Windows.Media.HitTestResult);
    }

    public void InvalidateScrollInfo()
    {
    }

    public void LineDown()
    {
    }

    public void LineLeft()
    {
    }

    public void LineRight()
    {
    }

    public void LineUp()
    {
    }

    protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
    {
      return default(System.Windows.Size);
    }

    public override void OnApplyTemplate()
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
    {
    }

    protected override void OnManipulationCompleted(System.Windows.Input.ManipulationCompletedEventArgs e)
    {
    }

    protected override void OnManipulationDelta(System.Windows.Input.ManipulationDeltaEventArgs e)
    {
    }

    protected override void OnManipulationInertiaStarting(System.Windows.Input.ManipulationInertiaStartingEventArgs e)
    {
    }

    protected override void OnManipulationStarting(System.Windows.Input.ManipulationStartingEventArgs e)
    {
    }

    protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected override void OnMouseWheel(System.Windows.Input.MouseWheelEventArgs e)
    {
    }

    protected virtual new void OnScrollChanged(ScrollChangedEventArgs e)
    {
    }

    public void PageDown()
    {
    }

    public void PageLeft()
    {
    }

    public void PageRight()
    {
    }

    public void PageUp()
    {
    }

    public void ScrollToBottom()
    {
    }

    public void ScrollToEnd()
    {
    }

    public void ScrollToHome()
    {
    }

    public void ScrollToHorizontalOffset(double offset)
    {
    }

    public void ScrollToLeftEnd()
    {
    }

    public void ScrollToRightEnd()
    {
    }

    public void ScrollToTop()
    {
    }

    public void ScrollToVerticalOffset(double offset)
    {
    }

    public ScrollViewer()
    {
    }

    public static void SetCanContentScroll(System.Windows.DependencyObject element, bool canContentScroll)
    {
    }

    public static void SetHorizontalScrollBarVisibility(System.Windows.DependencyObject element, ScrollBarVisibility horizontalScrollBarVisibility)
    {
    }

    public static void SetIsDeferredScrollingEnabled(System.Windows.DependencyObject element, bool value)
    {
    }

    public static void SetPanningDeceleration(System.Windows.DependencyObject element, double value)
    {
    }

    public static void SetPanningMode(System.Windows.DependencyObject element, PanningMode panningMode)
    {
    }

    public static void SetPanningRatio(System.Windows.DependencyObject element, double value)
    {
    }

    public static void SetVerticalScrollBarVisibility(System.Windows.DependencyObject element, ScrollBarVisibility verticalScrollBarVisibility)
    {
    }
    #endregion

    #region Properties and indexers
    public bool CanContentScroll
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Windows.Visibility ComputedHorizontalScrollBarVisibility
    {
      get
      {
        return default(System.Windows.Visibility);
      }
    }

    public System.Windows.Visibility ComputedVerticalScrollBarVisibility
    {
      get
      {
        return default(System.Windows.Visibility);
      }
    }

    public double ContentHorizontalOffset
    {
      get
      {
        return default(double);
      }
      private set
      {
      }
    }

    public double ContentVerticalOffset
    {
      get
      {
        return default(double);
      }
      private set
      {
      }
    }

    public double ExtentHeight
    {
      get
      {
        return default(double);
      }
    }

    public double ExtentWidth
    {
      get
      {
        return default(double);
      }
    }

    internal protected override bool HandlesScrolling
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
      private set
      {
      }
    }

    public ScrollBarVisibility HorizontalScrollBarVisibility
    {
      get
      {
        return default(ScrollBarVisibility);
      }
      set
      {
      }
    }

    public bool IsDeferredScrollingEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public double PanningDeceleration
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public PanningMode PanningMode
    {
      get
      {
        return default(PanningMode);
      }
      set
      {
      }
    }

    public double PanningRatio
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double ScrollableHeight
    {
      get
      {
        return default(double);
      }
    }

    public double ScrollableWidth
    {
      get
      {
        return default(double);
      }
    }

    internal protected System.Windows.Controls.Primitives.IScrollInfo ScrollInfo
    {
      get
      {
        return default(System.Windows.Controls.Primitives.IScrollInfo);
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
      private set
      {
      }
    }

    public ScrollBarVisibility VerticalScrollBarVisibility
    {
      get
      {
        return default(ScrollBarVisibility);
      }
      set
      {
      }
    }

    public double ViewportHeight
    {
      get
      {
        return default(double);
      }
    }

    public double ViewportWidth
    {
      get
      {
        return default(double);
      }
    }
    #endregion

    #region Events
    public event ScrollChangedEventHandler ScrollChanged
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
    public readonly static System.Windows.DependencyProperty CanContentScrollProperty;
    public readonly static System.Windows.DependencyProperty ComputedHorizontalScrollBarVisibilityProperty;
    public readonly static System.Windows.DependencyProperty ComputedVerticalScrollBarVisibilityProperty;
    public readonly static System.Windows.DependencyProperty ContentHorizontalOffsetProperty;
    public readonly static System.Windows.DependencyProperty ContentVerticalOffsetProperty;
    public readonly static System.Windows.DependencyProperty ExtentHeightProperty;
    public readonly static System.Windows.DependencyProperty ExtentWidthProperty;
    public readonly static System.Windows.DependencyProperty HorizontalOffsetProperty;
    public readonly static System.Windows.DependencyProperty HorizontalScrollBarVisibilityProperty;
    public readonly static System.Windows.DependencyProperty IsDeferredScrollingEnabledProperty;
    public readonly static System.Windows.DependencyProperty PanningDecelerationProperty;
    public readonly static System.Windows.DependencyProperty PanningModeProperty;
    public readonly static System.Windows.DependencyProperty PanningRatioProperty;
    public readonly static System.Windows.DependencyProperty ScrollableHeightProperty;
    public readonly static System.Windows.DependencyProperty ScrollableWidthProperty;
    public readonly static System.Windows.RoutedEvent ScrollChangedEvent;
    public readonly static System.Windows.DependencyProperty VerticalOffsetProperty;
    public readonly static System.Windows.DependencyProperty VerticalScrollBarVisibilityProperty;
    public readonly static System.Windows.DependencyProperty ViewportHeightProperty;
    public readonly static System.Windows.DependencyProperty ViewportWidthProperty;
    #endregion
  }
}

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

// File System.Windows.Controls.VirtualizingStackPanel.cs
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
  public partial class VirtualizingStackPanel : VirtualizingPanel, System.Windows.Controls.Primitives.IScrollInfo
#if NETFRAMEWORK_4_5
      , IStackMeasure
#endif

  {
    #region Methods and constructors
    public static void AddCleanUpVirtualizedItemHandler(System.Windows.DependencyObject element, CleanUpVirtualizedItemEventHandler handler)
    {
    }

    protected override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeSize)
    {
      return default(System.Windows.Size);
    }

    protected internal override void BringIndexIntoView(int index)
    {
    }

#if !NETFRAMEWORK_4_5
    public static bool GetIsVirtualizing(System.Windows.DependencyObject o)
    {
      return default(bool);
    }

    public static VirtualizationMode GetVirtualizationMode(System.Windows.DependencyObject element)
    {
      return default(VirtualizationMode);
    }

    public static void SetIsVirtualizing(System.Windows.DependencyObject element, bool value)
    {
    }

    public static void SetVirtualizationMode(System.Windows.DependencyObject element, VirtualizationMode value)
    {
    }


#endif

    public virtual new void LineDown()
    {
    }

    public virtual new void LineLeft()
    {
    }

    public virtual new void LineRight()
    {
    }

    public virtual new void LineUp()
    {
    }

    public System.Windows.Rect MakeVisible(System.Windows.Media.Visual visual, System.Windows.Rect rectangle)
    {
      return default(System.Windows.Rect);
    }

    protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
    {
      return default(System.Windows.Size);
    }

    public virtual new void MouseWheelDown()
    {
    }

    public virtual new void MouseWheelLeft()
    {
    }

    public virtual new void MouseWheelRight()
    {
    }

    public virtual new void MouseWheelUp()
    {
    }

    protected virtual new void OnCleanUpVirtualizedItem(CleanUpVirtualizedItemEventArgs e)
    {
      Contract.Requires(e != null);
    }

    protected override void OnClearChildren()
    {
    }

    protected override void OnItemsChanged(Object sender, System.Windows.Controls.Primitives.ItemsChangedEventArgs args)
    {
    }

    protected virtual new void OnViewportOffsetChanged(System.Windows.Vector oldViewportOffset, System.Windows.Vector newViewportOffset)
    {
    }

    protected virtual new void OnViewportSizeChanged(System.Windows.Size oldViewportSize, System.Windows.Size newViewportSize)
    {
    }

    public virtual new void PageDown()
    {
    }

    public virtual new void PageLeft()
    {
    }

    public virtual new void PageRight()
    {
    }

    public virtual new void PageUp()
    {
    }

    public static void RemoveCleanUpVirtualizedItemHandler(System.Windows.DependencyObject element, CleanUpVirtualizedItemEventHandler handler)
    {
    }

    public void SetHorizontalOffset(double offset)
    {
    }

    public void SetVerticalOffset(double offset)
    {
    }

    public VirtualizingStackPanel()
    {
    }
    #endregion

    #region Properties and indexers
    public bool CanHorizontallyScroll
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool CanVerticallyScroll
    {
      get
      {
        return default(bool);
      }
      set
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

    internal protected override bool HasLogicalOrientation
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
    }

    internal protected override System.Windows.Controls.Orientation LogicalOrientation
    {
      get
      {
        return default(System.Windows.Controls.Orientation);
      }
    }

    public Orientation Orientation
    {
      get
      {
        return default(Orientation);
      }
      set
      {
      }
    }

    public ScrollViewer ScrollOwner
    {
      get
      {
        return default(ScrollViewer);
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

    #region Fields
    public readonly static System.Windows.RoutedEvent CleanUpVirtualizedItemEvent;
    public readonly static System.Windows.DependencyProperty IsVirtualizingProperty;
    public readonly static System.Windows.DependencyProperty OrientationProperty;
    public readonly static System.Windows.DependencyProperty VirtualizationModeProperty;
    #endregion
  }
}

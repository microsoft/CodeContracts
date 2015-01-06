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

// File System.Windows.Controls.StackPanel.cs
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
  public partial class StackPanel : Panel, System.Windows.Controls.Primitives.IScrollInfo
#if NETFRAMEWORK_4_5
      , IStackMeasure
#endif
  {
    #region Methods and constructors
    protected override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeSize)
    {
      return default(System.Windows.Size);
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

    public System.Windows.Rect MakeVisible(System.Windows.Media.Visual visual, System.Windows.Rect rectangle)
    {
      return default(System.Windows.Rect);
    }

    protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
    {
      return default(System.Windows.Size);
    }

    public void MouseWheelDown()
    {
    }

    public void MouseWheelLeft()
    {
    }

    public void MouseWheelRight()
    {
    }

    public void MouseWheelUp()
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

    public void SetHorizontalOffset(double offset)
    {
    }

    public void SetVerticalOffset(double offset)
    {
    }

    public StackPanel()
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
    public readonly static System.Windows.DependencyProperty OrientationProperty;
    #endregion
  }
}

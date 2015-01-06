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

// File System.Windows.Controls.ScrollContentPresenter.cs
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
  sealed public partial class ScrollContentPresenter : ContentPresenter, System.Windows.Controls.Primitives.IScrollInfo
  {
    #region Methods and constructors
    protected override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeSize)
    {
      return default(System.Windows.Size);
    }

    protected override System.Windows.Media.Geometry GetLayoutClip(System.Windows.Size layoutSlotSize)
    {
      return default(System.Windows.Media.Geometry);
    }

    protected override System.Windows.Media.Visual GetVisualChild(int index)
    {
      return default(System.Windows.Media.Visual);
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

    public override void OnApplyTemplate()
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

    public ScrollContentPresenter()
    {
    }

    public void SetHorizontalOffset(double offset)
    {
    }

    public void SetVerticalOffset(double offset)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Documents.AdornerLayer AdornerLayer
    {
      get
      {
        return default(System.Windows.Documents.AdornerLayer);
      }
    }

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

    public double HorizontalOffset
    {
      get
      {
        return default(double);
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

    protected override int VisualChildrenCount
    {
      get
      {
        return default(int);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty CanContentScrollProperty;
    #endregion
  }
}

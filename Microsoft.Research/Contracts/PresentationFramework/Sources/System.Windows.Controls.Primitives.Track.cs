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

// File System.Windows.Controls.Primitives.Track.cs
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
  public partial class Track : System.Windows.FrameworkElement
  {
    #region Methods and constructors
    protected override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeSize)
    {
      return default(System.Windows.Size);
    }

    protected override System.Windows.Media.Visual GetVisualChild(int index)
    {
      return default(System.Windows.Media.Visual);
    }

    protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
    {
      return default(System.Windows.Size);
    }

    public Track()
    {
    }

    public virtual new double ValueFromDistance(double horizontal, double vertical)
    {
      return default(double);
    }

    public virtual new double ValueFromPoint(System.Windows.Point pt)
    {
      return default(double);
    }
    #endregion

    #region Properties and indexers
    public RepeatButton DecreaseRepeatButton
    {
      get
      {
        return default(RepeatButton);
      }
      set
      {
      }
    }

    public RepeatButton IncreaseRepeatButton
    {
      get
      {
        return default(RepeatButton);
      }
      set
      {
      }
    }

    public bool IsDirectionReversed
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public double Maximum
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double Minimum
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public System.Windows.Controls.Orientation Orientation
    {
      get
      {
        return default(System.Windows.Controls.Orientation);
      }
      set
      {
      }
    }

    public Thumb Thumb
    {
      get
      {
        return default(Thumb);
      }
      set
      {
      }
    }

    public double Value
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double ViewportSize
    {
      get
      {
        return default(double);
      }
      set
      {
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
    public readonly static System.Windows.DependencyProperty IsDirectionReversedProperty;
    public readonly static System.Windows.DependencyProperty MaximumProperty;
    public readonly static System.Windows.DependencyProperty MinimumProperty;
    public readonly static System.Windows.DependencyProperty OrientationProperty;
    public readonly static System.Windows.DependencyProperty ValueProperty;
    public readonly static System.Windows.DependencyProperty ViewportSizeProperty;
    #endregion
  }
}

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

// File System.Windows.Controls.Primitives.TickBar.cs
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
  public partial class TickBar : System.Windows.FrameworkElement
  {
    #region Methods and constructors
    protected override void OnRender(System.Windows.Media.DrawingContext dc)
    {
    }

    public TickBar()
    {
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Media.Brush Fill
    {
      get
      {
        return default(System.Windows.Media.Brush);
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

    public bool IsSelectionRangeEnabled
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

    public TickBarPlacement Placement
    {
      get
      {
        return default(TickBarPlacement);
      }
      set
      {
      }
    }

    public double ReservedSpace
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double SelectionEnd
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double SelectionStart
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double TickFrequency
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public System.Windows.Media.DoubleCollection Ticks
    {
      get
      {
        return default(System.Windows.Media.DoubleCollection);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty FillProperty;
    public readonly static System.Windows.DependencyProperty IsDirectionReversedProperty;
    public readonly static System.Windows.DependencyProperty IsSelectionRangeEnabledProperty;
    public readonly static System.Windows.DependencyProperty MaximumProperty;
    public readonly static System.Windows.DependencyProperty MinimumProperty;
    public readonly static System.Windows.DependencyProperty PlacementProperty;
    public readonly static System.Windows.DependencyProperty ReservedSpaceProperty;
    public readonly static System.Windows.DependencyProperty SelectionEndProperty;
    public readonly static System.Windows.DependencyProperty SelectionStartProperty;
    public readonly static System.Windows.DependencyProperty TickFrequencyProperty;
    public readonly static System.Windows.DependencyProperty TicksProperty;
    #endregion
  }
}

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

// File System.Windows.Controls.Slider.cs
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
  public partial class Slider : System.Windows.Controls.Primitives.RangeBase
  {
    #region Methods and constructors
    protected override System.Windows.Size ArrangeOverride(System.Windows.Size finalSize)
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

    protected virtual new void OnDecreaseLarge()
    {
    }

    protected virtual new void OnDecreaseSmall()
    {
    }

    protected virtual new void OnIncreaseLarge()
    {
    }

    protected virtual new void OnIncreaseSmall()
    {
    }

    protected virtual new void OnMaximizeValue()
    {
    }

    protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
    {
    }

    protected virtual new void OnMinimizeValue()
    {
    }

    protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
    {
    }

    protected override void OnPreviewMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected virtual new void OnThumbDragCompleted(System.Windows.Controls.Primitives.DragCompletedEventArgs e)
    {
      Contract.Requires(e != null);
    }

    protected virtual new void OnThumbDragDelta(System.Windows.Controls.Primitives.DragDeltaEventArgs e)
    {
      Contract.Requires(e != null);
    }

    protected virtual new void OnThumbDragStarted(System.Windows.Controls.Primitives.DragStartedEventArgs e)
    {
      Contract.Requires(e != null);
    }

    protected override void OnValueChanged(double oldValue, double newValue)
    {
    }

    public Slider()
    {
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Controls.Primitives.AutoToolTipPlacement AutoToolTipPlacement
    {
      get
      {
        return default(System.Windows.Controls.Primitives.AutoToolTipPlacement);
      }
      set
      {
      }
    }

    public int AutoToolTipPrecision
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public static System.Windows.Input.RoutedCommand DecreaseLarge
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedCommand>() != null);

        return default(System.Windows.Input.RoutedCommand);
      }
    }

    public static System.Windows.Input.RoutedCommand DecreaseSmall
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedCommand>() != null);

        return default(System.Windows.Input.RoutedCommand);
      }
    }

    public int Delay
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public static System.Windows.Input.RoutedCommand IncreaseLarge
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedCommand>() != null);

        return default(System.Windows.Input.RoutedCommand);
      }
    }

    public static System.Windows.Input.RoutedCommand IncreaseSmall
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedCommand>() != null);

        return default(System.Windows.Input.RoutedCommand);
      }
    }

    public int Interval
    {
      get
      {
        return default(int);
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

    public bool IsMoveToPointEnabled
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

    public bool IsSnapToTickEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public static System.Windows.Input.RoutedCommand MaximizeValue
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedCommand>() != null);

        return default(System.Windows.Input.RoutedCommand);
      }
    }

    public static System.Windows.Input.RoutedCommand MinimizeValue
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedCommand>() != null);

        return default(System.Windows.Input.RoutedCommand);
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

    public System.Windows.Controls.Primitives.TickPlacement TickPlacement
    {
      get
      {
        return default(System.Windows.Controls.Primitives.TickPlacement);
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
    public readonly static System.Windows.DependencyProperty AutoToolTipPlacementProperty;
    public readonly static System.Windows.DependencyProperty AutoToolTipPrecisionProperty;
    public readonly static System.Windows.DependencyProperty DelayProperty;
    public readonly static System.Windows.DependencyProperty IntervalProperty;
    public readonly static System.Windows.DependencyProperty IsDirectionReversedProperty;
    public readonly static System.Windows.DependencyProperty IsMoveToPointEnabledProperty;
    public readonly static System.Windows.DependencyProperty IsSelectionRangeEnabledProperty;
    public readonly static System.Windows.DependencyProperty IsSnapToTickEnabledProperty;
    public readonly static System.Windows.DependencyProperty OrientationProperty;
    public readonly static System.Windows.DependencyProperty SelectionEndProperty;
    public readonly static System.Windows.DependencyProperty SelectionStartProperty;
    public readonly static System.Windows.DependencyProperty TickFrequencyProperty;
    public readonly static System.Windows.DependencyProperty TickPlacementProperty;
    public readonly static System.Windows.DependencyProperty TicksProperty;
    #endregion
  }
}

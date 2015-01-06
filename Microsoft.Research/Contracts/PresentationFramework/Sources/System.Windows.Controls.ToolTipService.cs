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

// File System.Windows.Controls.ToolTipService.cs
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
  static public partial class ToolTipService
  {
    #region Methods and constructors
    public static int GetBetweenShowDelay(System.Windows.DependencyObject element)
    {
      return default(int);
    }

    public static bool GetHasDropShadow(System.Windows.DependencyObject element)
    {
      return default(bool);
    }

    public static double GetHorizontalOffset(System.Windows.DependencyObject element)
    {
      return default(double);
    }

    public static int GetInitialShowDelay(System.Windows.DependencyObject element)
    {
      return default(int);
    }

    public static bool GetIsEnabled(System.Windows.DependencyObject element)
    {
      return default(bool);
    }

    public static bool GetIsOpen(System.Windows.DependencyObject element)
    {
      return default(bool);
    }

    public static System.Windows.Controls.Primitives.PlacementMode GetPlacement(System.Windows.DependencyObject element)
    {
      return default(System.Windows.Controls.Primitives.PlacementMode);
    }

    public static System.Windows.Rect GetPlacementRectangle(System.Windows.DependencyObject element)
    {
      return default(System.Windows.Rect);
    }

    public static System.Windows.UIElement GetPlacementTarget(System.Windows.DependencyObject element)
    {
      return default(System.Windows.UIElement);
    }

    public static int GetShowDuration(System.Windows.DependencyObject element)
    {
      return default(int);
    }

    public static bool GetShowOnDisabled(System.Windows.DependencyObject element)
    {
      return default(bool);
    }

    public static Object GetToolTip(System.Windows.DependencyObject element)
    {
      return default(Object);
    }

    public static double GetVerticalOffset(System.Windows.DependencyObject element)
    {
      return default(double);
    }

    public static void SetBetweenShowDelay(System.Windows.DependencyObject element, int value)
    {
    }

    public static void SetHasDropShadow(System.Windows.DependencyObject element, bool value)
    {
    }

    public static void SetHorizontalOffset(System.Windows.DependencyObject element, double value)
    {
    }

    public static void SetInitialShowDelay(System.Windows.DependencyObject element, int value)
    {
    }

    public static void SetIsEnabled(System.Windows.DependencyObject element, bool value)
    {
    }

    public static void SetPlacement(System.Windows.DependencyObject element, System.Windows.Controls.Primitives.PlacementMode value)
    {
    }

    public static void SetPlacementRectangle(System.Windows.DependencyObject element, System.Windows.Rect value)
    {
    }

    public static void SetPlacementTarget(System.Windows.DependencyObject element, System.Windows.UIElement value)
    {
    }

    public static void SetShowDuration(System.Windows.DependencyObject element, int value)
    {
    }

    public static void SetShowOnDisabled(System.Windows.DependencyObject element, bool value)
    {
    }

    public static void SetToolTip(System.Windows.DependencyObject element, Object value)
    {
    }

    public static void SetVerticalOffset(System.Windows.DependencyObject element, double value)
    {
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty BetweenShowDelayProperty;
    public readonly static System.Windows.DependencyProperty HasDropShadowProperty;
    public readonly static System.Windows.DependencyProperty HorizontalOffsetProperty;
    public readonly static System.Windows.DependencyProperty InitialShowDelayProperty;
    public readonly static System.Windows.DependencyProperty IsEnabledProperty;
    public readonly static System.Windows.DependencyProperty IsOpenProperty;
    public readonly static System.Windows.DependencyProperty PlacementProperty;
    public readonly static System.Windows.DependencyProperty PlacementRectangleProperty;
    public readonly static System.Windows.DependencyProperty PlacementTargetProperty;
    public readonly static System.Windows.DependencyProperty ShowDurationProperty;
    public readonly static System.Windows.DependencyProperty ShowOnDisabledProperty;
    public readonly static System.Windows.RoutedEvent ToolTipClosingEvent;
    public readonly static System.Windows.RoutedEvent ToolTipOpeningEvent;
    public readonly static System.Windows.DependencyProperty ToolTipProperty;
    public readonly static System.Windows.DependencyProperty VerticalOffsetProperty;
    #endregion
  }
}

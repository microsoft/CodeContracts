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

// File System.Windows.Input.Stylus.cs
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


namespace System.Windows.Input
{
  static public partial class Stylus
  {
    #region Methods and constructors
    public static void AddGotStylusCaptureHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void AddLostStylusCaptureHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void AddPreviewStylusButtonDownHandler(System.Windows.DependencyObject element, StylusButtonEventHandler handler)
    {
    }

    public static void AddPreviewStylusButtonUpHandler(System.Windows.DependencyObject element, StylusButtonEventHandler handler)
    {
    }

    public static void AddPreviewStylusDownHandler(System.Windows.DependencyObject element, StylusDownEventHandler handler)
    {
    }

    public static void AddPreviewStylusInAirMoveHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void AddPreviewStylusInRangeHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void AddPreviewStylusMoveHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void AddPreviewStylusOutOfRangeHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void AddPreviewStylusSystemGestureHandler(System.Windows.DependencyObject element, StylusSystemGestureEventHandler handler)
    {
    }

    public static void AddPreviewStylusUpHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void AddStylusButtonDownHandler(System.Windows.DependencyObject element, StylusButtonEventHandler handler)
    {
    }

    public static void AddStylusButtonUpHandler(System.Windows.DependencyObject element, StylusButtonEventHandler handler)
    {
    }

    public static void AddStylusDownHandler(System.Windows.DependencyObject element, StylusDownEventHandler handler)
    {
    }

    public static void AddStylusEnterHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void AddStylusInAirMoveHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void AddStylusInRangeHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void AddStylusLeaveHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void AddStylusMoveHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void AddStylusOutOfRangeHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void AddStylusSystemGestureHandler(System.Windows.DependencyObject element, StylusSystemGestureEventHandler handler)
    {
    }

    public static void AddStylusUpHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static bool Capture(System.Windows.IInputElement element)
    {
      return default(bool);
    }

    public static bool Capture(System.Windows.IInputElement element, CaptureMode captureMode)
    {
      return default(bool);
    }

    public static bool GetIsFlicksEnabled(System.Windows.DependencyObject element)
    {
      return default(bool);
    }

    public static bool GetIsPressAndHoldEnabled(System.Windows.DependencyObject element)
    {
      return default(bool);
    }

    public static bool GetIsTapFeedbackEnabled(System.Windows.DependencyObject element)
    {
      return default(bool);
    }

    public static bool GetIsTouchFeedbackEnabled(System.Windows.DependencyObject element)
    {
      return default(bool);
    }

    public static void RemoveGotStylusCaptureHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void RemoveLostStylusCaptureHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void RemovePreviewStylusButtonDownHandler(System.Windows.DependencyObject element, StylusButtonEventHandler handler)
    {
    }

    public static void RemovePreviewStylusButtonUpHandler(System.Windows.DependencyObject element, StylusButtonEventHandler handler)
    {
    }

    public static void RemovePreviewStylusDownHandler(System.Windows.DependencyObject element, StylusDownEventHandler handler)
    {
    }

    public static void RemovePreviewStylusInAirMoveHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void RemovePreviewStylusInRangeHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void RemovePreviewStylusMoveHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void RemovePreviewStylusOutOfRangeHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void RemovePreviewStylusSystemGestureHandler(System.Windows.DependencyObject element, StylusSystemGestureEventHandler handler)
    {
    }

    public static void RemovePreviewStylusUpHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void RemoveStylusButtonDownHandler(System.Windows.DependencyObject element, StylusButtonEventHandler handler)
    {
    }

    public static void RemoveStylusButtonUpHandler(System.Windows.DependencyObject element, StylusButtonEventHandler handler)
    {
    }

    public static void RemoveStylusDownHandler(System.Windows.DependencyObject element, StylusDownEventHandler handler)
    {
    }

    public static void RemoveStylusEnterHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void RemoveStylusInAirMoveHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void RemoveStylusInRangeHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void RemoveStylusLeaveHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void RemoveStylusMoveHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void RemoveStylusOutOfRangeHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void RemoveStylusSystemGestureHandler(System.Windows.DependencyObject element, StylusSystemGestureEventHandler handler)
    {
    }

    public static void RemoveStylusUpHandler(System.Windows.DependencyObject element, StylusEventHandler handler)
    {
    }

    public static void SetIsFlicksEnabled(System.Windows.DependencyObject element, bool enabled)
    {
    }

    public static void SetIsPressAndHoldEnabled(System.Windows.DependencyObject element, bool enabled)
    {
    }

    public static void SetIsTapFeedbackEnabled(System.Windows.DependencyObject element, bool enabled)
    {
    }

    public static void SetIsTouchFeedbackEnabled(System.Windows.DependencyObject element, bool enabled)
    {
    }

    public static void Synchronize()
    {
    }
    #endregion

    #region Properties and indexers
    public static System.Windows.IInputElement Captured
    {
      get
      {
        return default(System.Windows.IInputElement);
      }
    }

    public static StylusDevice CurrentStylusDevice
    {
      get
      {
        return default(StylusDevice);
      }
    }

    public static System.Windows.IInputElement DirectlyOver
    {
      get
      {
        return default(System.Windows.IInputElement);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.RoutedEvent GotStylusCaptureEvent;
    public readonly static System.Windows.DependencyProperty IsFlicksEnabledProperty;
    public readonly static System.Windows.DependencyProperty IsPressAndHoldEnabledProperty;
    public readonly static System.Windows.DependencyProperty IsTapFeedbackEnabledProperty;
    public readonly static System.Windows.DependencyProperty IsTouchFeedbackEnabledProperty;
    public readonly static System.Windows.RoutedEvent LostStylusCaptureEvent;
    public readonly static System.Windows.RoutedEvent PreviewStylusButtonDownEvent;
    public readonly static System.Windows.RoutedEvent PreviewStylusButtonUpEvent;
    public readonly static System.Windows.RoutedEvent PreviewStylusDownEvent;
    public readonly static System.Windows.RoutedEvent PreviewStylusInAirMoveEvent;
    public readonly static System.Windows.RoutedEvent PreviewStylusInRangeEvent;
    public readonly static System.Windows.RoutedEvent PreviewStylusMoveEvent;
    public readonly static System.Windows.RoutedEvent PreviewStylusOutOfRangeEvent;
    public readonly static System.Windows.RoutedEvent PreviewStylusSystemGestureEvent;
    public readonly static System.Windows.RoutedEvent PreviewStylusUpEvent;
    public readonly static System.Windows.RoutedEvent StylusButtonDownEvent;
    public readonly static System.Windows.RoutedEvent StylusButtonUpEvent;
    public readonly static System.Windows.RoutedEvent StylusDownEvent;
    public readonly static System.Windows.RoutedEvent StylusEnterEvent;
    public readonly static System.Windows.RoutedEvent StylusInAirMoveEvent;
    public readonly static System.Windows.RoutedEvent StylusInRangeEvent;
    public readonly static System.Windows.RoutedEvent StylusLeaveEvent;
    public readonly static System.Windows.RoutedEvent StylusMoveEvent;
    public readonly static System.Windows.RoutedEvent StylusOutOfRangeEvent;
    public readonly static System.Windows.RoutedEvent StylusSystemGestureEvent;
    public readonly static System.Windows.RoutedEvent StylusUpEvent;
    #endregion
  }
}

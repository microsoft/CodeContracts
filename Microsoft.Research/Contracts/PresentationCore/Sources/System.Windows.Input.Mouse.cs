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

// File System.Windows.Input.Mouse.cs
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
  static public partial class Mouse
  {
    #region Methods and constructors
    public static void AddGotMouseCaptureHandler(System.Windows.DependencyObject element, MouseEventHandler handler)
    {
    }

    public static void AddLostMouseCaptureHandler(System.Windows.DependencyObject element, MouseEventHandler handler)
    {
    }

    public static void AddMouseDownHandler(System.Windows.DependencyObject element, MouseButtonEventHandler handler)
    {
    }

    public static void AddMouseEnterHandler(System.Windows.DependencyObject element, MouseEventHandler handler)
    {
    }

    public static void AddMouseLeaveHandler(System.Windows.DependencyObject element, MouseEventHandler handler)
    {
    }

    public static void AddMouseMoveHandler(System.Windows.DependencyObject element, MouseEventHandler handler)
    {
    }

    public static void AddMouseUpHandler(System.Windows.DependencyObject element, MouseButtonEventHandler handler)
    {
    }

    public static void AddMouseWheelHandler(System.Windows.DependencyObject element, MouseWheelEventHandler handler)
    {
    }

    public static void AddPreviewMouseDownHandler(System.Windows.DependencyObject element, MouseButtonEventHandler handler)
    {
    }

    public static void AddPreviewMouseDownOutsideCapturedElementHandler(System.Windows.DependencyObject element, MouseButtonEventHandler handler)
    {
    }

    public static void AddPreviewMouseMoveHandler(System.Windows.DependencyObject element, MouseEventHandler handler)
    {
    }

    public static void AddPreviewMouseUpHandler(System.Windows.DependencyObject element, MouseButtonEventHandler handler)
    {
    }

    public static void AddPreviewMouseUpOutsideCapturedElementHandler(System.Windows.DependencyObject element, MouseButtonEventHandler handler)
    {
    }

    public static void AddPreviewMouseWheelHandler(System.Windows.DependencyObject element, MouseWheelEventHandler handler)
    {
    }

    public static void AddQueryCursorHandler(System.Windows.DependencyObject element, QueryCursorEventHandler handler)
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

    public static int GetIntermediatePoints(System.Windows.IInputElement relativeTo, System.Windows.Point[] points)
    {
      return default(int);
    }

    public static System.Windows.Point GetPosition(System.Windows.IInputElement relativeTo)
    {
      return default(System.Windows.Point);
    }

    public static void RemoveGotMouseCaptureHandler(System.Windows.DependencyObject element, MouseEventHandler handler)
    {
    }

    public static void RemoveLostMouseCaptureHandler(System.Windows.DependencyObject element, MouseEventHandler handler)
    {
    }

    public static void RemoveMouseDownHandler(System.Windows.DependencyObject element, MouseButtonEventHandler handler)
    {
    }

    public static void RemoveMouseEnterHandler(System.Windows.DependencyObject element, MouseEventHandler handler)
    {
    }

    public static void RemoveMouseLeaveHandler(System.Windows.DependencyObject element, MouseEventHandler handler)
    {
    }

    public static void RemoveMouseMoveHandler(System.Windows.DependencyObject element, MouseEventHandler handler)
    {
    }

    public static void RemoveMouseUpHandler(System.Windows.DependencyObject element, MouseButtonEventHandler handler)
    {
    }

    public static void RemoveMouseWheelHandler(System.Windows.DependencyObject element, MouseWheelEventHandler handler)
    {
    }

    public static void RemovePreviewMouseDownHandler(System.Windows.DependencyObject element, MouseButtonEventHandler handler)
    {
    }

    public static void RemovePreviewMouseDownOutsideCapturedElementHandler(System.Windows.DependencyObject element, MouseButtonEventHandler handler)
    {
    }

    public static void RemovePreviewMouseMoveHandler(System.Windows.DependencyObject element, MouseEventHandler handler)
    {
    }

    public static void RemovePreviewMouseUpHandler(System.Windows.DependencyObject element, MouseButtonEventHandler handler)
    {
    }

    public static void RemovePreviewMouseUpOutsideCapturedElementHandler(System.Windows.DependencyObject element, MouseButtonEventHandler handler)
    {
    }

    public static void RemovePreviewMouseWheelHandler(System.Windows.DependencyObject element, MouseWheelEventHandler handler)
    {
    }

    public static void RemoveQueryCursorHandler(System.Windows.DependencyObject element, QueryCursorEventHandler handler)
    {
    }

    public static bool SetCursor(Cursor cursor)
    {
      return default(bool);
    }

    public static void Synchronize()
    {
    }

    public static void UpdateCursor()
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

    public static System.Windows.IInputElement DirectlyOver
    {
      get
      {
        return default(System.Windows.IInputElement);
      }
    }

    public static MouseButtonState LeftButton
    {
      get
      {
        return default(MouseButtonState);
      }
    }

    public static MouseButtonState MiddleButton
    {
      get
      {
        return default(MouseButtonState);
      }
    }

    public static Cursor OverrideCursor
    {
      get
      {
        return default(Cursor);
      }
      set
      {
      }
    }

    public static MouseDevice PrimaryDevice
    {
      get
      {
        return default(MouseDevice);
      }
    }

    public static MouseButtonState RightButton
    {
      get
      {
        return default(MouseButtonState);
      }
    }

    public static MouseButtonState XButton1
    {
      get
      {
        return default(MouseButtonState);
      }
    }

    public static MouseButtonState XButton2
    {
      get
      {
        return default(MouseButtonState);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.RoutedEvent GotMouseCaptureEvent;
    public readonly static System.Windows.RoutedEvent LostMouseCaptureEvent;
    public readonly static System.Windows.RoutedEvent MouseDownEvent;
    public readonly static System.Windows.RoutedEvent MouseEnterEvent;
    public readonly static System.Windows.RoutedEvent MouseLeaveEvent;
    public readonly static System.Windows.RoutedEvent MouseMoveEvent;
    public readonly static System.Windows.RoutedEvent MouseUpEvent;
    public readonly static System.Windows.RoutedEvent MouseWheelEvent;
    public readonly static System.Windows.RoutedEvent PreviewMouseDownEvent;
    public readonly static System.Windows.RoutedEvent PreviewMouseDownOutsideCapturedElementEvent;
    public readonly static System.Windows.RoutedEvent PreviewMouseMoveEvent;
    public readonly static System.Windows.RoutedEvent PreviewMouseUpEvent;
    public readonly static System.Windows.RoutedEvent PreviewMouseUpOutsideCapturedElementEvent;
    public readonly static System.Windows.RoutedEvent PreviewMouseWheelEvent;
    public readonly static System.Windows.RoutedEvent QueryCursorEvent;
    #endregion
  }
}

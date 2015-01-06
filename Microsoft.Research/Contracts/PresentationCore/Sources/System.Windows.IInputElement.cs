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

// File System.Windows.IInputElement.cs
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


namespace System.Windows
{
  public partial interface IInputElement
  {
    #region Methods and constructors
    void AddHandler(RoutedEvent routedEvent, Delegate handler);

    bool CaptureMouse();

    bool CaptureStylus();

    bool Focus();

    void RaiseEvent(RoutedEventArgs e);

    void ReleaseMouseCapture();

    void ReleaseStylusCapture();

    void RemoveHandler(RoutedEvent routedEvent, Delegate handler);
    #endregion

    #region Properties and indexers
    bool Focusable
    {
      get;
      set;
    }

    bool IsEnabled
    {
      get;
    }

    bool IsKeyboardFocused
    {
      get;
    }

    bool IsKeyboardFocusWithin
    {
      get;
    }

    bool IsMouseCaptured
    {
      get;
    }

    bool IsMouseDirectlyOver
    {
      get;
    }

    bool IsMouseOver
    {
      get;
    }

    bool IsStylusCaptured
    {
      get;
    }

    bool IsStylusDirectlyOver
    {
      get;
    }

    bool IsStylusOver
    {
      get;
    }
    #endregion

    #region Events
    event System.Windows.Input.KeyboardFocusChangedEventHandler GotKeyboardFocus
    ;

    event System.Windows.Input.MouseEventHandler GotMouseCapture
    ;

    event System.Windows.Input.StylusEventHandler GotStylusCapture
    ;

    event System.Windows.Input.KeyEventHandler KeyDown
    ;

    event System.Windows.Input.KeyEventHandler KeyUp
    ;

    event System.Windows.Input.KeyboardFocusChangedEventHandler LostKeyboardFocus
    ;

    event System.Windows.Input.MouseEventHandler LostMouseCapture
    ;

    event System.Windows.Input.StylusEventHandler LostStylusCapture
    ;

    event System.Windows.Input.MouseEventHandler MouseEnter
    ;

    event System.Windows.Input.MouseEventHandler MouseLeave
    ;

    event System.Windows.Input.MouseButtonEventHandler MouseLeftButtonDown
    ;

    event System.Windows.Input.MouseButtonEventHandler MouseLeftButtonUp
    ;

    event System.Windows.Input.MouseEventHandler MouseMove
    ;

    event System.Windows.Input.MouseButtonEventHandler MouseRightButtonDown
    ;

    event System.Windows.Input.MouseButtonEventHandler MouseRightButtonUp
    ;

    event System.Windows.Input.MouseWheelEventHandler MouseWheel
    ;

    event System.Windows.Input.KeyboardFocusChangedEventHandler PreviewGotKeyboardFocus
    ;

    event System.Windows.Input.KeyEventHandler PreviewKeyDown
    ;

    event System.Windows.Input.KeyEventHandler PreviewKeyUp
    ;

    event System.Windows.Input.KeyboardFocusChangedEventHandler PreviewLostKeyboardFocus
    ;

    event System.Windows.Input.MouseButtonEventHandler PreviewMouseLeftButtonDown
    ;

    event System.Windows.Input.MouseButtonEventHandler PreviewMouseLeftButtonUp
    ;

    event System.Windows.Input.MouseEventHandler PreviewMouseMove
    ;

    event System.Windows.Input.MouseButtonEventHandler PreviewMouseRightButtonDown
    ;

    event System.Windows.Input.MouseButtonEventHandler PreviewMouseRightButtonUp
    ;

    event System.Windows.Input.MouseWheelEventHandler PreviewMouseWheel
    ;

    event System.Windows.Input.StylusButtonEventHandler PreviewStylusButtonDown
    ;

    event System.Windows.Input.StylusButtonEventHandler PreviewStylusButtonUp
    ;

    event System.Windows.Input.StylusDownEventHandler PreviewStylusDown
    ;

    event System.Windows.Input.StylusEventHandler PreviewStylusInAirMove
    ;

    event System.Windows.Input.StylusEventHandler PreviewStylusInRange
    ;

    event System.Windows.Input.StylusEventHandler PreviewStylusMove
    ;

    event System.Windows.Input.StylusEventHandler PreviewStylusOutOfRange
    ;

    event System.Windows.Input.StylusSystemGestureEventHandler PreviewStylusSystemGesture
    ;

    event System.Windows.Input.StylusEventHandler PreviewStylusUp
    ;

    event System.Windows.Input.TextCompositionEventHandler PreviewTextInput
    ;

    event System.Windows.Input.StylusButtonEventHandler StylusButtonDown
    ;

    event System.Windows.Input.StylusButtonEventHandler StylusButtonUp
    ;

    event System.Windows.Input.StylusDownEventHandler StylusDown
    ;

    event System.Windows.Input.StylusEventHandler StylusEnter
    ;

    event System.Windows.Input.StylusEventHandler StylusInAirMove
    ;

    event System.Windows.Input.StylusEventHandler StylusInRange
    ;

    event System.Windows.Input.StylusEventHandler StylusLeave
    ;

    event System.Windows.Input.StylusEventHandler StylusMove
    ;

    event System.Windows.Input.StylusEventHandler StylusOutOfRange
    ;

    event System.Windows.Input.StylusSystemGestureEventHandler StylusSystemGesture
    ;

    event System.Windows.Input.StylusEventHandler StylusUp
    ;

    event System.Windows.Input.TextCompositionEventHandler TextInput
    ;
    #endregion
  }
}

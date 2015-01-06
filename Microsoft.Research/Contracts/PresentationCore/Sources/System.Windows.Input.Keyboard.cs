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

// File System.Windows.Input.Keyboard.cs
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
  static public partial class Keyboard
  {
    #region Methods and constructors
    public static void AddGotKeyboardFocusHandler(System.Windows.DependencyObject element, KeyboardFocusChangedEventHandler handler)
    {
    }

    public static void AddKeyboardInputProviderAcquireFocusHandler(System.Windows.DependencyObject element, KeyboardInputProviderAcquireFocusEventHandler handler)
    {
    }

    public static void AddKeyDownHandler(System.Windows.DependencyObject element, KeyEventHandler handler)
    {
    }

    public static void AddKeyUpHandler(System.Windows.DependencyObject element, KeyEventHandler handler)
    {
    }

    public static void AddLostKeyboardFocusHandler(System.Windows.DependencyObject element, KeyboardFocusChangedEventHandler handler)
    {
    }

    public static void AddPreviewGotKeyboardFocusHandler(System.Windows.DependencyObject element, KeyboardFocusChangedEventHandler handler)
    {
    }

    public static void AddPreviewKeyboardInputProviderAcquireFocusHandler(System.Windows.DependencyObject element, KeyboardInputProviderAcquireFocusEventHandler handler)
    {
    }

    public static void AddPreviewKeyDownHandler(System.Windows.DependencyObject element, KeyEventHandler handler)
    {
    }

    public static void AddPreviewKeyUpHandler(System.Windows.DependencyObject element, KeyEventHandler handler)
    {
    }

    public static void AddPreviewLostKeyboardFocusHandler(System.Windows.DependencyObject element, KeyboardFocusChangedEventHandler handler)
    {
    }

    public static void ClearFocus()
    {
    }

    public static System.Windows.IInputElement Focus(System.Windows.IInputElement element)
    {
      return default(System.Windows.IInputElement);
    }

    public static KeyStates GetKeyStates(Key key)
    {
      return default(KeyStates);
    }

    public static bool IsKeyDown(Key key)
    {
      return default(bool);
    }

    public static bool IsKeyToggled(Key key)
    {
      return default(bool);
    }

    public static bool IsKeyUp(Key key)
    {
      return default(bool);
    }

    public static void RemoveGotKeyboardFocusHandler(System.Windows.DependencyObject element, KeyboardFocusChangedEventHandler handler)
    {
    }

    public static void RemoveKeyboardInputProviderAcquireFocusHandler(System.Windows.DependencyObject element, KeyboardInputProviderAcquireFocusEventHandler handler)
    {
    }

    public static void RemoveKeyDownHandler(System.Windows.DependencyObject element, KeyEventHandler handler)
    {
    }

    public static void RemoveKeyUpHandler(System.Windows.DependencyObject element, KeyEventHandler handler)
    {
    }

    public static void RemoveLostKeyboardFocusHandler(System.Windows.DependencyObject element, KeyboardFocusChangedEventHandler handler)
    {
    }

    public static void RemovePreviewGotKeyboardFocusHandler(System.Windows.DependencyObject element, KeyboardFocusChangedEventHandler handler)
    {
    }

    public static void RemovePreviewKeyboardInputProviderAcquireFocusHandler(System.Windows.DependencyObject element, KeyboardInputProviderAcquireFocusEventHandler handler)
    {
    }

    public static void RemovePreviewKeyDownHandler(System.Windows.DependencyObject element, KeyEventHandler handler)
    {
    }

    public static void RemovePreviewKeyUpHandler(System.Windows.DependencyObject element, KeyEventHandler handler)
    {
    }

    public static void RemovePreviewLostKeyboardFocusHandler(System.Windows.DependencyObject element, KeyboardFocusChangedEventHandler handler)
    {
    }
    #endregion

    #region Properties and indexers
    public static RestoreFocusMode DefaultRestoreFocusMode
    {
      get
      {
        return default(RestoreFocusMode);
      }
      set
      {
      }
    }

    public static System.Windows.IInputElement FocusedElement
    {
      get
      {
        return default(System.Windows.IInputElement);
      }
    }

    public static ModifierKeys Modifiers
    {
      get
      {
        return default(ModifierKeys);
      }
    }

    public static KeyboardDevice PrimaryDevice
    {
      get
      {
        return default(KeyboardDevice);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.RoutedEvent GotKeyboardFocusEvent;
    public readonly static System.Windows.RoutedEvent KeyboardInputProviderAcquireFocusEvent;
    public readonly static System.Windows.RoutedEvent KeyDownEvent;
    public readonly static System.Windows.RoutedEvent KeyUpEvent;
    public readonly static System.Windows.RoutedEvent LostKeyboardFocusEvent;
    public readonly static System.Windows.RoutedEvent PreviewGotKeyboardFocusEvent;
    public readonly static System.Windows.RoutedEvent PreviewKeyboardInputProviderAcquireFocusEvent;
    public readonly static System.Windows.RoutedEvent PreviewKeyDownEvent;
    public readonly static System.Windows.RoutedEvent PreviewKeyUpEvent;
    public readonly static System.Windows.RoutedEvent PreviewLostKeyboardFocusEvent;
    #endregion
  }
}

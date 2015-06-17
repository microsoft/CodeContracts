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

// File System.Windows.Controls.Primitives.ButtonBase.cs
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
  abstract public partial class ButtonBase : System.Windows.Controls.ContentControl, System.Windows.Input.ICommandSource
  {
    #region Methods and constructors
    protected ButtonBase()
    {
    }

    protected override void OnAccessKey(System.Windows.Input.AccessKeyEventArgs e)
    {
    }

    protected virtual new void OnClick()
    {
    }

    protected virtual new void OnIsPressedChanged(System.Windows.DependencyPropertyChangedEventArgs e)
    {
    }

    protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
    {
    }

    protected override void OnKeyUp(System.Windows.Input.KeyEventArgs e)
    {
    }

    protected override void OnLostKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs e)
    {
    }

    protected override void OnLostMouseCapture(System.Windows.Input.MouseEventArgs e)
    {
    }

    protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
    {
    }

    protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
    {
    }

    protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected override void OnMouseLeftButtonUp(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
    {
    }

    protected override void OnRenderSizeChanged(System.Windows.SizeChangedInfo sizeInfo)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Controls.ClickMode ClickMode
    {
      get
      {
        return default(System.Windows.Controls.ClickMode);
      }
      set
      {
      }
    }

    public System.Windows.Input.ICommand Command
    {
      get
      {
        return default(System.Windows.Input.ICommand);
      }
      set
      {
      }
    }

    public Object CommandParameter
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public System.Windows.IInputElement CommandTarget
    {
      get
      {
        return default(System.Windows.IInputElement);
      }
      set
      {
      }
    }

    protected override bool IsEnabledCore
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsPressed
    {
      get
      {
        return default(bool);
      }
      protected set
      {
      }
    }
    #endregion

    #region Events
    public event System.Windows.RoutedEventHandler Click
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.RoutedEvent ClickEvent;
    public readonly static System.Windows.DependencyProperty ClickModeProperty;
    public readonly static System.Windows.DependencyProperty CommandParameterProperty;
    public readonly static System.Windows.DependencyProperty CommandProperty;
    public readonly static System.Windows.DependencyProperty CommandTargetProperty;
    public readonly static System.Windows.DependencyProperty IsPressedProperty;
    #endregion
  }
}

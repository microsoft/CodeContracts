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

// File System.Windows.Controls.PasswordBox.cs
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
  sealed public partial class PasswordBox : Control, ITextBoxViewHost
  {
    #region Methods and constructors
    public void Clear()
    {
      Contract.Ensures(0 <= this.Password.Length);
    }

    public override void OnApplyTemplate()
    {
    }

    protected override void OnContextMenuOpening(ContextMenuEventArgs e)
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected override void OnDragEnter(System.Windows.DragEventArgs e)
    {
    }

    protected override void OnDragLeave(System.Windows.DragEventArgs e)
    {
    }

    protected override void OnDragOver(System.Windows.DragEventArgs e)
    {
    }

    protected override void OnDrop(System.Windows.DragEventArgs e)
    {
    }

    protected override void OnGiveFeedback(System.Windows.GiveFeedbackEventArgs e)
    {
    }

    protected override void OnGotKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs e)
    {
    }

    protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
    {
    }

    protected override void OnKeyUp(System.Windows.Input.KeyEventArgs e)
    {
    }

    protected override void OnLostFocus(System.Windows.RoutedEventArgs e)
    {
    }

    protected override void OnLostKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs e)
    {
    }

    protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
    {
    }

    protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected override void OnPropertyChanged(System.Windows.DependencyPropertyChangedEventArgs e)
    {
    }

    protected override void OnQueryContinueDrag(System.Windows.QueryContinueDragEventArgs e)
    {
    }

    protected override void OnQueryCursor(System.Windows.Input.QueryCursorEventArgs e)
    {
    }

    protected override void OnTemplateChanged(ControlTemplate oldTemplate, ControlTemplate newTemplate)
    {
    }

    protected override void OnTextInput(System.Windows.Input.TextCompositionEventArgs e)
    {
    }

    public PasswordBox()
    {
    }

    public void Paste()
    {
    }

    public void SelectAll()
    {
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Media.Brush CaretBrush
    {
      get
      {
        return default(System.Windows.Media.Brush);
      }
      set
      {
      }
    }

    public int MaxLength
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string Password
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
        Contract.Ensures(0 <= value.Length);
      }
    }

    public char PasswordChar
    {
      get
      {
        return default(char);
      }
      set
      {
      }
    }

    public System.Security.SecureString SecurePassword
    {
      get
      {
        return default(System.Security.SecureString);
      }
    }

    public System.Windows.Media.Brush SelectionBrush
    {
      get
      {
        return default(System.Windows.Media.Brush);
      }
      set
      {
      }
    }

    public double SelectionOpacity
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    bool System.Windows.Controls.ITextBoxViewHost.IsTypographyDefaultValue
    {
      get
      {
        return default(bool);
      }
    }

    System.Windows.Documents.ITextContainer System.Windows.Controls.ITextBoxViewHost.TextContainer
    {
      get
      {
        return default(System.Windows.Documents.ITextContainer);
      }
    }
    #endregion

    #region Events
    public event System.Windows.RoutedEventHandler PasswordChanged
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
    public readonly static System.Windows.DependencyProperty CaretBrushProperty;
    public readonly static System.Windows.DependencyProperty MaxLengthProperty;
    public readonly static System.Windows.RoutedEvent PasswordChangedEvent;
    public readonly static System.Windows.DependencyProperty PasswordCharProperty;
    public readonly static System.Windows.DependencyProperty SelectionBrushProperty;
    public readonly static System.Windows.DependencyProperty SelectionOpacityProperty;
    #endregion
  }
}

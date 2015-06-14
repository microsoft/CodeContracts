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

// File System.Windows.Controls.Primitives.TextBoxBase.cs
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
  abstract public partial class TextBoxBase : System.Windows.Controls.Control
  {
    #region Methods and constructors
    public void AppendText(string textData)
    {
    }

    public void BeginChange()
    {
    }

    public void Copy()
    {
    }

    public void Cut()
    {
    }

    public IDisposable DeclareChangeBlock()
    {
      return default(IDisposable);
    }

    public void EndChange()
    {
    }

    public void LineDown()
    {
    }

    public void LineLeft()
    {
    }

    public void LineRight()
    {
    }

    public void LineUp()
    {
    }

    public void LockCurrentUndoUnit()
    {
    }

    public override void OnApplyTemplate()
    {
    }

    protected override void OnContextMenuOpening(System.Windows.Controls.ContextMenuEventArgs e)
    {
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

    protected override void OnMouseWheel(System.Windows.Input.MouseWheelEventArgs e)
    {
    }

    protected override void OnPreviewKeyDown(System.Windows.Input.KeyEventArgs e)
    {
    }

    protected override void OnQueryContinueDrag(System.Windows.QueryContinueDragEventArgs e)
    {
    }

    protected override void OnQueryCursor(System.Windows.Input.QueryCursorEventArgs e)
    {
    }

    protected virtual new void OnSelectionChanged(System.Windows.RoutedEventArgs e)
    {
    }

    protected override void OnTemplateChanged(System.Windows.Controls.ControlTemplate oldTemplate, System.Windows.Controls.ControlTemplate newTemplate)
    {
    }

    protected virtual new void OnTextChanged(System.Windows.Controls.TextChangedEventArgs e)
    {
    }

    protected override void OnTextInput(System.Windows.Input.TextCompositionEventArgs e)
    {
    }

    public void PageDown()
    {
    }

    public void PageLeft()
    {
    }

    public void PageRight()
    {
    }

    public void PageUp()
    {
    }

    public void Paste()
    {
    }

    public bool Redo()
    {
      return default(bool);
    }

    public void ScrollToEnd()
    {
    }

    public void ScrollToHome()
    {
    }

    public void ScrollToHorizontalOffset(double offset)
    {
      Contract.Ensures(!double.IsNaN(offset));
    }

    public void ScrollToVerticalOffset(double offset)
    {
      Contract.Ensures(!double.IsNaN(offset));
    }

    public void SelectAll()
    {
    }

    internal TextBoxBase()
    {
    }

    public bool Undo()
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public bool AcceptsReturn
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool AcceptsTab
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool AutoWordSelection
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool CanRedo
    {
      get
      {
        return default(bool);
      }
    }

    public bool CanUndo
    {
      get
      {
        return default(bool);
      }
    }

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

    public double ExtentHeight
    {
      get
      {
        return default(double);
      }
    }

    public double ExtentWidth
    {
      get
      {
        return default(double);
      }
    }

    public double HorizontalOffset
    {
      get
      {
        return default(double);
      }
    }

    public System.Windows.Controls.ScrollBarVisibility HorizontalScrollBarVisibility
    {
      get
      {
        return default(System.Windows.Controls.ScrollBarVisibility);
      }
      set
      {
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsReadOnlyCaretVisible
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsUndoEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
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

    public System.Windows.Controls.SpellCheck SpellCheck
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Controls.SpellCheck>() != null);

        return default(System.Windows.Controls.SpellCheck);
      }
    }

    public int UndoLimit
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public double VerticalOffset
    {
      get
      {
        return default(double);
      }
    }

    public System.Windows.Controls.ScrollBarVisibility VerticalScrollBarVisibility
    {
      get
      {
        return default(System.Windows.Controls.ScrollBarVisibility);
      }
      set
      {
      }
    }

    public double ViewportHeight
    {
      get
      {
        return default(double);
      }
    }

    public double ViewportWidth
    {
      get
      {
        return default(double);
      }
    }
    #endregion

    #region Events
    public event System.Windows.RoutedEventHandler SelectionChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.Controls.TextChangedEventHandler TextChanged
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
    public readonly static System.Windows.DependencyProperty AcceptsReturnProperty;
    public readonly static System.Windows.DependencyProperty AcceptsTabProperty;
    public readonly static System.Windows.DependencyProperty AutoWordSelectionProperty;
    public readonly static System.Windows.DependencyProperty CaretBrushProperty;
    public readonly static System.Windows.DependencyProperty HorizontalScrollBarVisibilityProperty;
    public readonly static System.Windows.DependencyProperty IsReadOnlyCaretVisibleProperty;
    public readonly static System.Windows.DependencyProperty IsReadOnlyProperty;
    public readonly static System.Windows.DependencyProperty IsUndoEnabledProperty;
    public readonly static System.Windows.DependencyProperty SelectionBrushProperty;
    public readonly static System.Windows.RoutedEvent SelectionChangedEvent;
    public readonly static System.Windows.DependencyProperty SelectionOpacityProperty;
    public readonly static System.Windows.RoutedEvent TextChangedEvent;
    public readonly static System.Windows.DependencyProperty UndoLimitProperty;
    public readonly static System.Windows.DependencyProperty VerticalScrollBarVisibilityProperty;
    #endregion
  }
}

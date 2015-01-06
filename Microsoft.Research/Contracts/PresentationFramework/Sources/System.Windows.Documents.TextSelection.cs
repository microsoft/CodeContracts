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

// File System.Windows.Documents.TextSelection.cs
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


namespace System.Windows.Documents
{
  sealed public partial class TextSelection : TextRange, ITextSelection, ITextRange
  {
    #region Methods and constructors
    void System.Windows.Documents.ITextRange.ApplyTypingHeuristics(bool overType)
    {
    }

    Object System.Windows.Documents.ITextRange.GetPropertyValue(System.Windows.DependencyProperty formattingProperty)
    {
      return default(Object);
    }

    void System.Windows.Documents.ITextRange.NotifyChanged(bool disableScroll, bool skipEvents)
    {
    }

    void System.Windows.Documents.ITextRange.Select(ITextPointer anchorPosition, ITextPointer movingPosition)
    {
    }

    void System.Windows.Documents.ITextRange.SelectParagraph(ITextPointer position)
    {
    }

    void System.Windows.Documents.ITextRange.SelectWord(ITextPointer position)
    {
    }

    bool System.Windows.Documents.ITextSelection.Contains(System.Windows.Point point)
    {
      return default(bool);
    }

    void System.Windows.Documents.ITextSelection.DetachFromVisualTree()
    {
    }

    void System.Windows.Documents.ITextSelection.ExtendSelectionByMouse(ITextPointer cursorPosition, bool forceWordSelection, bool forceParagraphSelection)
    {
    }

    bool System.Windows.Documents.ITextSelection.ExtendToNextInsertionPosition(LogicalDirection direction)
    {
      return default(bool);
    }

    bool System.Windows.Documents.ITextSelection.ExtendToNextTableRow(LogicalDirection direction)
    {
      return default(bool);
    }

    void System.Windows.Documents.ITextSelection.ExtendToPosition(ITextPointer position)
    {
    }

    void System.Windows.Documents.ITextSelection.OnCaretNavigation()
    {
    }

    void System.Windows.Documents.ITextSelection.OnDetach()
    {
    }

    void System.Windows.Documents.ITextSelection.OnInterimSelectionChanged(bool interimSelection)
    {
    }

    void System.Windows.Documents.ITextSelection.OnTextViewUpdated()
    {
    }

    void System.Windows.Documents.ITextSelection.RefreshCaret()
    {
    }

    void System.Windows.Documents.ITextSelection.SetCaretToPosition(ITextPointer caretPosition, LogicalDirection direction, bool allowStopAtLineEnd, bool allowStopNearSpace)
    {
    }

    void System.Windows.Documents.ITextSelection.SetSelectionByMouse(ITextPointer cursorPosition, System.Windows.Point cursorMousePoint)
    {
    }

    void System.Windows.Documents.ITextSelection.UpdateCaretAndHighlight()
    {
    }

    void System.Windows.Documents.ITextSelection.ValidateLayout()
    {
    }

    internal TextSelection() : base (default(TextPointer), default(TextPointer))
    {
    }
    #endregion

    #region Properties and indexers
    bool System.Windows.Documents.ITextRange._IsChanged
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    string System.Windows.Documents.ITextRange.Text
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    ITextPointer System.Windows.Documents.ITextSelection.AnchorPosition
    {
      get
      {
        return default(ITextPointer);
      }
    }

    bool System.Windows.Documents.ITextSelection.CoversEntireContent
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Windows.Documents.ITextSelection.IsInterimSelection
    {
      get
      {
        return default(bool);
      }
    }

    ITextPointer System.Windows.Documents.ITextSelection.MovingPosition
    {
      get
      {
        return default(ITextPointer);
      }
    }

    ITextView System.Windows.Documents.ITextSelection.TextView
    {
      get
      {
        return default(ITextView);
      }
    }
    #endregion
  }
}

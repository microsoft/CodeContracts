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

// File System.Windows.Documents.TextPointer.cs
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
  public partial class TextPointer : ContentPosition, ITextPointer
  {
    #region Methods and constructors
    public int CompareTo(TextPointer position)
    {
      Contract.Requires(position != null);
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 1);

      return default(int);
    }

    public int DeleteTextInRun(int count)
    {
      return default(int);
    }

    public System.Windows.DependencyObject GetAdjacentElement(LogicalDirection direction)
    {
      return default(System.Windows.DependencyObject);
    }

    public System.Windows.Rect GetCharacterRect(LogicalDirection direction)
    {
      return default(System.Windows.Rect);
    }

    public TextPointer GetInsertionPosition(LogicalDirection direction)
    {
      return default(TextPointer);
    }

    public TextPointer GetLineStartPosition(int count, out int actualCount)
    {
      Contract.Ensures(Contract.Result<System.Windows.Documents.TextPointer>() != null);

      actualCount = default(int);

      return default(TextPointer);
    }

    public TextPointer GetLineStartPosition(int count)
    {
      return default(TextPointer);
    }

    public TextPointer GetNextContextPosition(LogicalDirection direction)
    {
      return default(TextPointer);
    }

    public TextPointer GetNextInsertionPosition(LogicalDirection direction)
    {
      return default(TextPointer);
    }

    public int GetOffsetToPosition(TextPointer position)
    {
      Contract.Requires(position != null);

      return default(int);
    }

    public TextPointerContext GetPointerContext(LogicalDirection direction)
    {
      return default(TextPointerContext);
    }

    public TextPointer GetPositionAtOffset(int offset, LogicalDirection direction)
    {
      return default(TextPointer);
    }

    public TextPointer GetPositionAtOffset(int offset)
    {
      return default(TextPointer);
    }

    public int GetTextInRun(LogicalDirection direction, char[] textBuffer, int startIndex, int count)
    {
      return default(int);
    }

    public string GetTextInRun(LogicalDirection direction)
    {
      return default(string);
    }

    public int GetTextRunLength(LogicalDirection direction)
    {
      return default(int);
    }

    public TextPointer InsertLineBreak()
    {
      return default(TextPointer);
    }

    public TextPointer InsertParagraphBreak()
    {
      return default(TextPointer);
    }

    public void InsertTextInRun(string textData)
    {
    }

    public bool IsInSameDocument(TextPointer textPosition)
    {
      return default(bool);
    }

    int System.Windows.Documents.ITextPointer.CompareTo(ITextPointer position)
    {
      return default(int);
    }

    ITextPointer System.Windows.Documents.ITextPointer.CreatePointer(LogicalDirection gravity)
    {
      return default(ITextPointer);
    }

    ITextPointer System.Windows.Documents.ITextPointer.CreatePointer()
    {
      return default(ITextPointer);
    }

    ITextPointer System.Windows.Documents.ITextPointer.CreatePointer(int offset)
    {
      return default(ITextPointer);
    }

    ITextPointer System.Windows.Documents.ITextPointer.CreatePointer(int offset, LogicalDirection gravity)
    {
      return default(ITextPointer);
    }

    void System.Windows.Documents.ITextPointer.DeleteContentToPosition(ITextPointer limit)
    {
    }

    void System.Windows.Documents.ITextPointer.Freeze()
    {
    }

    Object System.Windows.Documents.ITextPointer.GetAdjacentElement(LogicalDirection direction)
    {
      return default(Object);
    }

    System.Windows.Rect System.Windows.Documents.ITextPointer.GetCharacterRect(LogicalDirection direction)
    {
      return default(System.Windows.Rect);
    }

    Type System.Windows.Documents.ITextPointer.GetElementType(LogicalDirection direction)
    {
      return default(Type);
    }

    ITextPointer System.Windows.Documents.ITextPointer.GetFormatNormalizedPosition(LogicalDirection direction)
    {
      return default(ITextPointer);
    }

    ITextPointer System.Windows.Documents.ITextPointer.GetFrozenPointer(LogicalDirection logicalDirection)
    {
      return default(ITextPointer);
    }

    ITextPointer System.Windows.Documents.ITextPointer.GetInsertionPosition(LogicalDirection direction)
    {
      return default(ITextPointer);
    }

    System.Windows.LocalValueEnumerator System.Windows.Documents.ITextPointer.GetLocalValueEnumerator()
    {
      return default(System.Windows.LocalValueEnumerator);
    }

    ITextPointer System.Windows.Documents.ITextPointer.GetNextContextPosition(LogicalDirection direction)
    {
      return default(ITextPointer);
    }

    ITextPointer System.Windows.Documents.ITextPointer.GetNextInsertionPosition(LogicalDirection direction)
    {
      return default(ITextPointer);
    }

    int System.Windows.Documents.ITextPointer.GetOffsetToPosition(ITextPointer position)
    {
      return default(int);
    }

    TextPointerContext System.Windows.Documents.ITextPointer.GetPointerContext(LogicalDirection direction)
    {
      return default(TextPointerContext);
    }

    int System.Windows.Documents.ITextPointer.GetTextInRun(LogicalDirection direction, char[] textBuffer, int startIndex, int count)
    {
      return default(int);
    }

    string System.Windows.Documents.ITextPointer.GetTextInRun(LogicalDirection direction)
    {
      return default(string);
    }

    int System.Windows.Documents.ITextPointer.GetTextRunLength(LogicalDirection direction)
    {
      return default(int);
    }

    Object System.Windows.Documents.ITextPointer.GetValue(System.Windows.DependencyProperty formattingProperty)
    {
      return default(Object);
    }

    bool System.Windows.Documents.ITextPointer.HasEqualScope(ITextPointer position)
    {
      return default(bool);
    }

    void System.Windows.Documents.ITextPointer.InsertTextInRun(string textData)
    {
    }

    int System.Windows.Documents.ITextPointer.MoveByOffset(int offset)
    {
      return default(int);
    }

    bool System.Windows.Documents.ITextPointer.MoveToInsertionPosition(LogicalDirection direction)
    {
      return default(bool);
    }

    int System.Windows.Documents.ITextPointer.MoveToLineBoundary(int count)
    {
      return default(int);
    }

    bool System.Windows.Documents.ITextPointer.MoveToNextContextPosition(LogicalDirection direction)
    {
      return default(bool);
    }

    bool System.Windows.Documents.ITextPointer.MoveToNextInsertionPosition(LogicalDirection direction)
    {
      return default(bool);
    }

    void System.Windows.Documents.ITextPointer.MoveToPosition(ITextPointer position)
    {
    }

    Object System.Windows.Documents.ITextPointer.ReadLocalValue(System.Windows.DependencyProperty formattingProperty)
    {
      return default(Object);
    }

    void System.Windows.Documents.ITextPointer.SetLogicalDirection(LogicalDirection direction)
    {
    }

    bool System.Windows.Documents.ITextPointer.ValidateLayout()
    {
      return default(bool);
    }

    internal TextPointer()
    {
    }

    #endregion

    #region Properties and indexers
    public System.Windows.Documents.TextPointer DocumentEnd
    {
      get
      {
        return default(System.Windows.Documents.TextPointer);
      }
    }

    public System.Windows.Documents.TextPointer DocumentStart
    {
      get
      {
        return default(System.Windows.Documents.TextPointer);
      }
    }

    public bool HasValidLayout
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsAtInsertionPosition
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsAtLineStartPosition
    {
      get
      {
        return default(bool);
      }
    }

    public LogicalDirection LogicalDirection
    {
      get
      {
        return default(LogicalDirection);
      }
    }

    public Paragraph Paragraph
    {
      get
      {
        return default(Paragraph);
      }
    }

    public System.Windows.DependencyObject Parent
    {
      get
      {
        return default(System.Windows.DependencyObject);
      }
    }

    int System.Windows.Documents.ITextPointer.CharOffset
    {
      get
      {
        return default(int);
      }
    }

    bool System.Windows.Documents.ITextPointer.HasValidLayout
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Windows.Documents.ITextPointer.IsAtCaretUnitBoundary
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Windows.Documents.ITextPointer.IsAtInsertionPosition
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Windows.Documents.ITextPointer.IsFrozen
    {
      get
      {
        return default(bool);
      }
    }

    System.Windows.Documents.LogicalDirection System.Windows.Documents.ITextPointer.LogicalDirection
    {
      get
      {
        return default(System.Windows.Documents.LogicalDirection);
      }
    }

    int System.Windows.Documents.ITextPointer.Offset
    {
      get
      {
        return default(int);
      }
    }

    Type System.Windows.Documents.ITextPointer.ParentType
    {
      get
      {
        return default(Type);
      }
    }

    ITextContainer System.Windows.Documents.ITextPointer.TextContainer
    {
      get
      {
        return default(ITextContainer);
      }
    }
    #endregion
  }
}

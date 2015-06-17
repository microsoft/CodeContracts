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

// File System.Windows.Documents.ITextPointer.cs
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
  internal partial interface ITextPointer
  {
    #region Methods and constructors
    int CompareTo(ITextPointer position);

    ITextPointer CreatePointer();

    ITextPointer CreatePointer(int offset, LogicalDirection gravity);

    ITextPointer CreatePointer(int offset);

    ITextPointer CreatePointer(LogicalDirection gravity);

    void DeleteContentToPosition(ITextPointer limit);

    void Freeze();

    Object GetAdjacentElement(LogicalDirection direction);

    System.Windows.Rect GetCharacterRect(LogicalDirection direction);

    Type GetElementType(LogicalDirection direction);

    ITextPointer GetFormatNormalizedPosition(LogicalDirection direction);

    ITextPointer GetFrozenPointer(LogicalDirection logicalDirection);

    ITextPointer GetInsertionPosition(LogicalDirection direction);

    System.Windows.LocalValueEnumerator GetLocalValueEnumerator();

    ITextPointer GetNextContextPosition(LogicalDirection direction);

    ITextPointer GetNextInsertionPosition(LogicalDirection direction);

    int GetOffsetToPosition(ITextPointer position);

    TextPointerContext GetPointerContext(LogicalDirection direction);

    string GetTextInRun(LogicalDirection direction);

    int GetTextInRun(LogicalDirection direction, char[] textBuffer, int startIndex, int count);

    int GetTextRunLength(LogicalDirection direction);

    Object GetValue(System.Windows.DependencyProperty formattingProperty);

    bool HasEqualScope(ITextPointer position);

    void InsertTextInRun(string textData);

    int MoveByOffset(int offset);

    bool MoveToInsertionPosition(LogicalDirection direction);

    int MoveToLineBoundary(int count);

    bool MoveToNextContextPosition(LogicalDirection direction);

    bool MoveToNextInsertionPosition(LogicalDirection direction);

    void MoveToPosition(ITextPointer position);

    Object ReadLocalValue(System.Windows.DependencyProperty formattingProperty);

    void SetLogicalDirection(LogicalDirection direction);

    bool ValidateLayout();
    #endregion

    #region Properties and indexers
    int CharOffset
    {
      get;
    }

    bool HasValidLayout
    {
      get;
    }

    bool IsAtCaretUnitBoundary
    {
      get;
    }

    bool IsAtInsertionPosition
    {
      get;
    }

    bool IsFrozen
    {
      get;
    }

    LogicalDirection LogicalDirection
    {
      get;
    }

    int Offset
    {
      get;
    }

    Type ParentType
    {
      get;
    }

    ITextContainer TextContainer
    {
      get;
    }
    #endregion
  }
}

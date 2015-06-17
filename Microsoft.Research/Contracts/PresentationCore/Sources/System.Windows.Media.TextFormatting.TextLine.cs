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

// File System.Windows.Media.TextFormatting.TextLine.cs
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


namespace System.Windows.Media.TextFormatting
{
  abstract public partial class TextLine : MS.Internal.TextFormatting.ITextMetrics, IDisposable
  {
    #region Methods and constructors
    public abstract TextLine Collapse(TextCollapsingProperties[] collapsingPropertiesList);

    public abstract void Dispose();

    public abstract void Draw(System.Windows.Media.DrawingContext drawingContext, System.Windows.Point origin, InvertAxes inversion);

    public abstract CharacterHit GetBackspaceCaretCharacterHit(CharacterHit characterHit);

    public abstract CharacterHit GetCharacterHitFromDistance(double distance);

    public abstract double GetDistanceFromCharacterHit(CharacterHit characterHit);

    public abstract IEnumerable<IndexedGlyphRun> GetIndexedGlyphRuns();

    public abstract CharacterHit GetNextCaretCharacterHit(CharacterHit characterHit);

    public abstract CharacterHit GetPreviousCaretCharacterHit(CharacterHit characterHit);

    public abstract IList<TextBounds> GetTextBounds(int firstTextSourceCharacterIndex, int textLength);

    public abstract IList<TextCollapsedRange> GetTextCollapsedRanges();

    public abstract TextLineBreak GetTextLineBreak();

    public abstract IList<TextSpan<TextRun>> GetTextRunSpans();

    protected TextLine()
    {
    }
    #endregion

    #region Properties and indexers
    public abstract double Baseline
    {
      get;
    }

    public abstract int DependentLength
    {
      get;
    }

    public abstract double Extent
    {
      get;
    }

    public abstract bool HasCollapsed
    {
      get;
    }

    public abstract bool HasOverflowed
    {
      get;
    }

    public abstract double Height
    {
      get;
    }

    public virtual new bool IsTruncated
    {
      get
      {
        return default(bool);
      }
    }

    public abstract int Length
    {
      get;
    }

    public abstract double MarkerBaseline
    {
      get;
    }

    public abstract double MarkerHeight
    {
      get;
    }

    public abstract int NewlineLength
    {
      get;
    }

    public abstract double OverhangAfter
    {
      get;
    }

    public abstract double OverhangLeading
    {
      get;
    }

    public abstract double OverhangTrailing
    {
      get;
    }

    public abstract double Start
    {
      get;
    }

    public abstract double TextBaseline
    {
      get;
    }

    public abstract double TextHeight
    {
      get;
    }

    public abstract int TrailingWhitespaceLength
    {
      get;
    }

    public abstract double Width
    {
      get;
    }

    public abstract double WidthIncludingTrailingWhitespace
    {
      get;
    }
    #endregion
  }
}

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

// File System.Windows.Controls.TextBox.cs
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
  public partial class TextBox : System.Windows.Controls.Primitives.TextBoxBase, System.Windows.Markup.IAddChild, ITextBoxViewHost
  {
    #region Methods and constructors
    public void Clear()
    {
    }

    public int GetCharacterIndexFromLineIndex(int lineIndex)
    {
      return default(int);
    }

    public int GetCharacterIndexFromPoint(System.Windows.Point point, bool snapToText)
    {
      return default(int);
    }

    public int GetFirstVisibleLineIndex()
    {
      Contract.Ensures(Contract.Result<int>() <= 2147483647);
      Contract.Ensures(Int32.MinValue <= Contract.Result<int>());

      return default(int);
    }

    public int GetLastVisibleLineIndex()
    {
      return default(int);
    }

    public int GetLineIndexFromCharacterIndex(int charIndex)
    {
      return default(int);
    }

    public int GetLineLength(int lineIndex)
    {
      return default(int);
    }

    public string GetLineText(int lineIndex)
    {
      return default(string);
    }

    public int GetNextSpellingErrorCharacterIndex(int charIndex, System.Windows.Documents.LogicalDirection direction)
    {
      return default(int);
    }

    public System.Windows.Rect GetRectFromCharacterIndex(int charIndex, bool trailingEdge)
    {
      return default(System.Windows.Rect);
    }

    public System.Windows.Rect GetRectFromCharacterIndex(int charIndex)
    {
      return default(System.Windows.Rect);
    }

    public SpellingError GetSpellingError(int charIndex)
    {
      return default(SpellingError);
    }

    public int GetSpellingErrorLength(int charIndex)
    {
      return default(int);
    }

    public int GetSpellingErrorStart(int charIndex)
    {
      return default(int);
    }

    protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
    {
      return default(System.Windows.Size);
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected override void OnPropertyChanged(System.Windows.DependencyPropertyChangedEventArgs e)
    {
    }

    public void ScrollToLine(int lineIndex)
    {
    }

    public void Select(int start, int length)
    {
    }

    public bool ShouldSerializeText(System.Windows.Markup.XamlDesignerSerializationManager manager)
    {
      Contract.Requires(manager != null);

      return default(bool);
    }

    void System.Windows.Markup.IAddChild.AddChild(Object value)
    {
    }

    void System.Windows.Markup.IAddChild.AddText(string text)
    {
    }

    public TextBox()
    {
    }
    #endregion

    #region Properties and indexers
    public int CaretIndex
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() == this.SelectionStart);

        return default(int);
      }
      set
      {
      }
    }

    public CharacterCasing CharacterCasing
    {
      get
      {
        return default(CharacterCasing);
      }
      set
      {
      }
    }

    public int LineCount
    {
      get
      {
        return default(int);
      }
    }

    internal protected override System.Collections.IEnumerator LogicalChildren
    {
      get
      {
        return default(System.Collections.IEnumerator);
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

    public int MaxLines
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int MinLines
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string SelectedText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public int SelectionLength
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int SelectionStart
    {
      get
      {
        return default(int);
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

    public string Text
    {
      get
      {
        // => TextBox.CoerceText will ensure value is never null.
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
      }
    }

    public System.Windows.TextAlignment TextAlignment
    {
      get
      {
        return default(System.Windows.TextAlignment);
      }
      set
      {
      }
    }

    public System.Windows.TextDecorationCollection TextDecorations
    {
      get
      {
        return default(System.Windows.TextDecorationCollection);
      }
      set
      {
      }
    }

    public System.Windows.TextWrapping TextWrapping
    {
      get
      {
        return default(System.Windows.TextWrapping);
      }
      set
      {
      }
    }

    public System.Windows.Documents.Typography Typography
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Documents.Typography>() != null);

        return default(System.Windows.Documents.Typography);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty CharacterCasingProperty;
    public readonly static System.Windows.DependencyProperty MaxLengthProperty;
    public readonly static System.Windows.DependencyProperty MaxLinesProperty;
    public readonly static System.Windows.DependencyProperty MinLinesProperty;
    public readonly static System.Windows.DependencyProperty TextAlignmentProperty;
    public readonly static System.Windows.DependencyProperty TextDecorationsProperty;
    public readonly static System.Windows.DependencyProperty TextProperty;
    public readonly static System.Windows.DependencyProperty TextWrappingProperty;
    #endregion
  }
}

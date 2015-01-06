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

// File System.Windows.Documents.TextRange.cs
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
  public partial class TextRange : ITextRange
  {
    #region Methods and constructors
    public void ApplyPropertyValue(System.Windows.DependencyProperty formattingProperty, Object value)
    {
    }

    public bool CanLoad(string dataFormat)
    {
      return default(bool);
    }

    public bool CanSave(string dataFormat)
    {
      return default(bool);
    }

    public void ClearAllProperties()
    {
    }

    public bool Contains(TextPointer textPointer)
    {
      return default(bool);
    }

    public Object GetPropertyValue(System.Windows.DependencyProperty formattingProperty)
    {
      return default(Object);
    }

    public void Load(Stream stream, string dataFormat)
    {
    }

    public void Save(Stream stream, string dataFormat)
    {
    }

    public void Save(Stream stream, string dataFormat, bool preserveTextElements)
    {
    }

    public void Select(TextPointer position1, TextPointer position2)
    {
    }

    void System.Windows.Documents.ITextRange.ApplyTypingHeuristics(bool overType)
    {
    }

    void System.Windows.Documents.ITextRange.BeginChange()
    {
    }

    void System.Windows.Documents.ITextRange.BeginChangeNoUndo()
    {
    }

    bool System.Windows.Documents.ITextRange.CanSave(string dataFormat)
    {
      return default(bool);
    }

    bool System.Windows.Documents.ITextRange.Contains(ITextPointer position)
    {
      return default(bool);
    }

    IDisposable System.Windows.Documents.ITextRange.DeclareChangeBlock(bool disableScroll)
    {
      return default(IDisposable);
    }

    IDisposable System.Windows.Documents.ITextRange.DeclareChangeBlock()
    {
      return default(IDisposable);
    }

    void System.Windows.Documents.ITextRange.EndChange(bool disableScroll, bool skipEvents)
    {
    }

    void System.Windows.Documents.ITextRange.EndChange()
    {
    }

    void System.Windows.Documents.ITextRange.FireChanged()
    {
    }

    Object System.Windows.Documents.ITextRange.GetPropertyValue(System.Windows.DependencyProperty formattingProperty)
    {
      return default(Object);
    }

    System.Windows.UIElement System.Windows.Documents.ITextRange.GetUIElementSelected()
    {
      return default(System.Windows.UIElement);
    }

    void System.Windows.Documents.ITextRange.NotifyChanged(bool disableScroll, bool skipEvents)
    {
    }

    void System.Windows.Documents.ITextRange.Save(Stream stream, string dataFormat, bool preserveTextElements)
    {
    }

    void System.Windows.Documents.ITextRange.Save(Stream stream, string dataFormat)
    {
    }

    void System.Windows.Documents.ITextRange.Select(ITextPointer position1, ITextPointer position2)
    {
    }

    void System.Windows.Documents.ITextRange.SelectParagraph(ITextPointer position)
    {
    }

    void System.Windows.Documents.ITextRange.SelectWord(ITextPointer position)
    {
    }

    public TextRange(TextPointer position1, TextPointer position2)
    {
    }
    #endregion

    #region Properties and indexers
    public TextPointer End
    {
      get
      {
        return default(TextPointer);
      }
    }

    public bool IsEmpty
    {
      get
      {
        return default(bool);
      }
    }

    public TextPointer Start
    {
      get
      {
        return default(TextPointer);
      }
    }

    int System.Windows.Documents.ITextRange._ChangeBlockLevel
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    uint System.Windows.Documents.ITextRange._ContentGeneration
    {
      get
      {
        return default(uint);
      }
      set
      {
      }
    }

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

    bool System.Windows.Documents.ITextRange._IsTableCellRange
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    int System.Windows.Documents.ITextRange.ChangeBlockLevel
    {
      get
      {
        return default(int);
      }
    }

    ITextPointer System.Windows.Documents.ITextRange.End
    {
      get
      {
        return default(ITextPointer);
      }
    }

    bool System.Windows.Documents.ITextRange.HasConcreteTextContainer
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Windows.Documents.ITextRange.IgnoreTextUnitBoundaries
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Windows.Documents.ITextRange.IsEmpty
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Windows.Documents.ITextRange.IsTableCellRange
    {
      get
      {
        return default(bool);
      }
    }

    ITextPointer System.Windows.Documents.ITextRange.Start
    {
      get
      {
        return default(ITextPointer);
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

    string System.Windows.Documents.ITextRange.Xml
    {
      get
      {
        return default(string);
      }
    }

    public string Text
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event EventHandler Changed
    {
      add
      {
      }
      remove
      {
      }
    }

    event EventHandler System.Windows.Documents.ITextRange.Changed
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}

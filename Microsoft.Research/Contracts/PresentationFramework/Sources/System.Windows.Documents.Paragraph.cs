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

// File System.Windows.Documents.Paragraph.cs
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
  public partial class Paragraph : Block
  {
    #region Methods and constructors
    public Paragraph(Inline inline)
    {
      Contract.Ensures(0 <= this.Inlines.Count);
    }

    public Paragraph()
    {
    }

    public bool ShouldSerializeInlines(System.Windows.Markup.XamlDesignerSerializationManager manager)
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public InlineCollection Inlines
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Documents.InlineCollection>() != null);

        return default(InlineCollection);
      }
    }

    public bool KeepTogether
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool KeepWithNext
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public int MinOrphanLines
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int MinWidowLines
    {
      get
      {
        return default(int);
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

    public double TextIndent
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty KeepTogetherProperty;
    public readonly static System.Windows.DependencyProperty KeepWithNextProperty;
    public readonly static System.Windows.DependencyProperty MinOrphanLinesProperty;
    public readonly static System.Windows.DependencyProperty MinWidowLinesProperty;
    public readonly static System.Windows.DependencyProperty TextDecorationsProperty;
    public readonly static System.Windows.DependencyProperty TextIndentProperty;
    #endregion
  }
}

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

// File System.Windows.Annotations.AnnotationHelper.cs
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


namespace System.Windows.Annotations
{
  static public partial class AnnotationHelper
  {
    #region Methods and constructors
    public static void ClearHighlightsForSelection(AnnotationService service)
    {
    }

    public static Annotation CreateHighlightForSelection(AnnotationService service, string author, System.Windows.Media.Brush highlightBrush)
    {
      return default(Annotation);
    }

    public static Annotation CreateInkStickyNoteForSelection(AnnotationService service, string author)
    {
      Contract.Ensures(!string.IsNullOrEmpty(System.Windows.Controls.StickyNoteControl.InkSchemaName.Name));
      Contract.Ensures(!string.IsNullOrEmpty(System.Windows.Controls.StickyNoteControl.InkSchemaName.Namespace));
      Contract.Ensures(Contract.Result<System.Windows.Annotations.Annotation>() != null);
      Contract.Ensures(System.DateTime.Now.Kind == ((System.DateTimeKind)(2)));

      return default(Annotation);
    }

    public static Annotation CreateTextStickyNoteForSelection(AnnotationService service, string author)
    {
      Contract.Ensures(!string.IsNullOrEmpty(System.Windows.Controls.StickyNoteControl.TextSchemaName.Name));
      Contract.Ensures(!string.IsNullOrEmpty(System.Windows.Controls.StickyNoteControl.TextSchemaName.Namespace));
      Contract.Ensures(Contract.Result<System.Windows.Annotations.Annotation>() != null);
      Contract.Ensures(System.DateTime.Now.Kind == ((System.DateTimeKind)(2)));

      return default(Annotation);
    }

    public static void DeleteInkStickyNotesForSelection(AnnotationService service)
    {
    }

    public static void DeleteTextStickyNotesForSelection(AnnotationService service)
    {
    }

    public static IAnchorInfo GetAnchorInfo(AnnotationService service, Annotation annotation)
    {
      return default(IAnchorInfo);
    }
    #endregion
  }
}

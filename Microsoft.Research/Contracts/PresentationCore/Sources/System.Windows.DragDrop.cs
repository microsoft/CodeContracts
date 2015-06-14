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

// File System.Windows.DragDrop.cs
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


namespace System.Windows
{
  static public partial class DragDrop
  {
    #region Methods and constructors
    public static void AddDragEnterHandler(DependencyObject element, DragEventHandler handler)
    {
    }

    public static void AddDragLeaveHandler(DependencyObject element, DragEventHandler handler)
    {
    }

    public static void AddDragOverHandler(DependencyObject element, DragEventHandler handler)
    {
    }

    public static void AddDropHandler(DependencyObject element, DragEventHandler handler)
    {
    }

    public static void AddGiveFeedbackHandler(DependencyObject element, GiveFeedbackEventHandler handler)
    {
    }

    public static void AddPreviewDragEnterHandler(DependencyObject element, DragEventHandler handler)
    {
    }

    public static void AddPreviewDragLeaveHandler(DependencyObject element, DragEventHandler handler)
    {
    }

    public static void AddPreviewDragOverHandler(DependencyObject element, DragEventHandler handler)
    {
    }

    public static void AddPreviewDropHandler(DependencyObject element, DragEventHandler handler)
    {
    }

    public static void AddPreviewGiveFeedbackHandler(DependencyObject element, GiveFeedbackEventHandler handler)
    {
    }

    public static void AddPreviewQueryContinueDragHandler(DependencyObject element, QueryContinueDragEventHandler handler)
    {
    }

    public static void AddQueryContinueDragHandler(DependencyObject element, QueryContinueDragEventHandler handler)
    {
    }

    public static DragDropEffects DoDragDrop(DependencyObject dragSource, Object data, DragDropEffects allowedEffects)
    {
      return default(DragDropEffects);
    }

    public static void RemoveDragEnterHandler(DependencyObject element, DragEventHandler handler)
    {
    }

    public static void RemoveDragLeaveHandler(DependencyObject element, DragEventHandler handler)
    {
    }

    public static void RemoveDragOverHandler(DependencyObject element, DragEventHandler handler)
    {
    }

    public static void RemoveDropHandler(DependencyObject element, DragEventHandler handler)
    {
    }

    public static void RemoveGiveFeedbackHandler(DependencyObject element, GiveFeedbackEventHandler handler)
    {
    }

    public static void RemovePreviewDragEnterHandler(DependencyObject element, DragEventHandler handler)
    {
    }

    public static void RemovePreviewDragLeaveHandler(DependencyObject element, DragEventHandler handler)
    {
    }

    public static void RemovePreviewDragOverHandler(DependencyObject element, DragEventHandler handler)
    {
    }

    public static void RemovePreviewDropHandler(DependencyObject element, DragEventHandler handler)
    {
    }

    public static void RemovePreviewGiveFeedbackHandler(DependencyObject element, GiveFeedbackEventHandler handler)
    {
    }

    public static void RemovePreviewQueryContinueDragHandler(DependencyObject element, QueryContinueDragEventHandler handler)
    {
    }

    public static void RemoveQueryContinueDragHandler(DependencyObject element, QueryContinueDragEventHandler handler)
    {
    }
    #endregion

    #region Fields
    public readonly static RoutedEvent DragEnterEvent;
    public readonly static RoutedEvent DragLeaveEvent;
    public readonly static RoutedEvent DragOverEvent;
    public readonly static RoutedEvent DropEvent;
    public readonly static RoutedEvent GiveFeedbackEvent;
    public readonly static RoutedEvent PreviewDragEnterEvent;
    public readonly static RoutedEvent PreviewDragLeaveEvent;
    public readonly static RoutedEvent PreviewDragOverEvent;
    public readonly static RoutedEvent PreviewDropEvent;
    public readonly static RoutedEvent PreviewGiveFeedbackEvent;
    public readonly static RoutedEvent PreviewQueryContinueDragEvent;
    public readonly static RoutedEvent QueryContinueDragEvent;
    #endregion
  }
}

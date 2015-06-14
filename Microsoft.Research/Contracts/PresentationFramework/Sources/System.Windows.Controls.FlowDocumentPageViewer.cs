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

// File System.Windows.Controls.FlowDocumentPageViewer.cs
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
  public partial class FlowDocumentPageViewer : System.Windows.Controls.Primitives.DocumentViewerBase, MS.Internal.AppModel.IJournalState
  {
    #region Methods and constructors
    public void DecreaseZoom()
    {
    }

    public void Find()
    {
    }

    public FlowDocumentPageViewer()
    {
    }

    public void IncreaseZoom()
    {
    }

    public override void OnApplyTemplate()
    {
    }

    protected override void OnCancelPrintCommand()
    {
    }

    protected override void OnContextMenuOpening(ContextMenuEventArgs e)
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected virtual new void OnDecreaseZoomCommand()
    {
    }

    protected override void OnDocumentChanged()
    {
    }

    protected virtual new void OnFindCommand()
    {
    }

    protected override void OnFirstPageCommand()
    {
    }

    protected override void OnGoToPageCommand(int pageNumber)
    {
    }

    protected virtual new void OnIncreaseZoomCommand()
    {
    }

    protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
    {
    }

    protected override void OnLastPageCommand()
    {
    }

    protected override void OnMouseWheel(System.Windows.Input.MouseWheelEventArgs e)
    {
    }

    protected override void OnNextPageCommand()
    {
    }

    protected override void OnPageViewsChanged()
    {
    }

    protected override void OnPreviousPageCommand()
    {
    }

    protected override void OnPrintCommand()
    {
    }

    protected virtual new void OnPrintCompleted()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new bool CanDecreaseZoom
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanIncreaseZoom
    {
      get
      {
        return default(bool);
      }
    }

    public double MaxZoom
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double MinZoom
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public System.Windows.Documents.TextSelection Selection
    {
      get
      {
        return default(System.Windows.Documents.TextSelection);
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

    public double Zoom
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double ZoomIncrement
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
    public readonly static System.Windows.DependencyProperty CanDecreaseZoomProperty;
    protected readonly static System.Windows.DependencyPropertyKey CanDecreaseZoomPropertyKey;
    public readonly static System.Windows.DependencyProperty CanIncreaseZoomProperty;
    protected readonly static System.Windows.DependencyPropertyKey CanIncreaseZoomPropertyKey;
    public readonly static System.Windows.DependencyProperty MaxZoomProperty;
    public readonly static System.Windows.DependencyProperty MinZoomProperty;
    public readonly static System.Windows.DependencyProperty SelectionBrushProperty;
    public readonly static System.Windows.DependencyProperty SelectionOpacityProperty;
    public readonly static System.Windows.DependencyProperty ZoomIncrementProperty;
    public readonly static System.Windows.DependencyProperty ZoomProperty;
    #endregion
  }
}

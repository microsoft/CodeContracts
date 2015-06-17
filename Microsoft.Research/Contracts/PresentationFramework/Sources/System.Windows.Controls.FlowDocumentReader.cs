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

// File System.Windows.Controls.FlowDocumentReader.cs
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
  public partial class FlowDocumentReader : Control, System.Windows.Markup.IAddChild, MS.Internal.AppModel.IJournalState
  {
    #region Methods and constructors
    public void CancelPrint()
    {
    }

    public bool CanGoToPage(int pageNumber)
    {
      return default(bool);
    }

    public void DecreaseZoom()
    {
    }

    public void Find()
    {
    }

    public FlowDocumentReader()
    {
    }

    public void IncreaseZoom()
    {
    }

    public override void OnApplyTemplate()
    {
    }

    protected virtual new void OnCancelPrintCommand()
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected virtual new void OnDecreaseZoomCommand()
    {
    }

    protected virtual new void OnFindCommand()
    {
    }

    protected virtual new void OnIncreaseZoomCommand()
    {
    }

    protected override void OnInitialized(EventArgs e)
    {
    }

    protected override void OnIsKeyboardFocusWithinChanged(System.Windows.DependencyPropertyChangedEventArgs e)
    {
    }

    protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
    {
    }

    protected virtual new void OnPrintCommand()
    {
    }

    protected virtual new void OnPrintCompleted()
    {
    }

    protected virtual new void OnSwitchViewingModeCommand(FlowDocumentReaderViewingMode viewingMode)
    {
    }

    public void Print()
    {
    }

    public void SwitchViewingMode(FlowDocumentReaderViewingMode viewingMode)
    {
    }

    protected virtual new void SwitchViewingModeCore(FlowDocumentReaderViewingMode viewingMode)
    {
    }

    void System.Windows.Markup.IAddChild.AddChild(Object value)
    {
    }

    void System.Windows.Markup.IAddChild.AddText(string text)
    {
    }
    #endregion

    #region Properties and indexers
    public bool CanDecreaseZoom
    {
      get
      {
        return default(bool);
      }
    }

    public bool CanGoToNextPage
    {
      get
      {
        return default(bool);
      }
    }

    public bool CanGoToPreviousPage
    {
      get
      {
        return default(bool);
      }
    }

    public bool CanIncreaseZoom
    {
      get
      {
        return default(bool);
      }
    }

    public System.Windows.Documents.FlowDocument Document
    {
      get
      {
        return default(System.Windows.Documents.FlowDocument);
      }
      set
      {
      }
    }

    public bool IsFindEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsPageViewEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsPrintEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsScrollViewEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsTwoPageViewEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    internal protected override System.Collections.IEnumerator LogicalChildren
    {
      get
      {
        return default(System.Collections.IEnumerator);
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

    public int PageCount
    {
      get
      {
        return default(int);
      }
    }

    public int PageNumber
    {
      get
      {
        return default(int);
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

    public FlowDocumentReaderViewingMode ViewingMode
    {
      get
      {
        return default(FlowDocumentReaderViewingMode);
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
    public readonly static System.Windows.DependencyProperty CanGoToNextPageProperty;
    public readonly static System.Windows.DependencyProperty CanGoToPreviousPageProperty;
    public readonly static System.Windows.DependencyProperty CanIncreaseZoomProperty;
    public readonly static System.Windows.DependencyProperty DocumentProperty;
    public readonly static System.Windows.DependencyProperty IsFindEnabledProperty;
    public readonly static System.Windows.DependencyProperty IsPageViewEnabledProperty;
    public readonly static System.Windows.DependencyProperty IsPrintEnabledProperty;
    public readonly static System.Windows.DependencyProperty IsScrollViewEnabledProperty;
    public readonly static System.Windows.DependencyProperty IsTwoPageViewEnabledProperty;
    public readonly static System.Windows.DependencyProperty MaxZoomProperty;
    public readonly static System.Windows.DependencyProperty MinZoomProperty;
    public readonly static System.Windows.DependencyProperty PageCountProperty;
    public readonly static System.Windows.DependencyProperty PageNumberProperty;
    public readonly static System.Windows.DependencyProperty SelectionBrushProperty;
    public readonly static System.Windows.DependencyProperty SelectionOpacityProperty;
    public readonly static System.Windows.Input.RoutedUICommand SwitchViewingModeCommand;
    public readonly static System.Windows.DependencyProperty ViewingModeProperty;
    public readonly static System.Windows.DependencyProperty ZoomIncrementProperty;
    public readonly static System.Windows.DependencyProperty ZoomProperty;
    #endregion
  }
}

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

// File System.Windows.Controls.DocumentViewer.cs
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
  public partial class DocumentViewer : System.Windows.Controls.Primitives.DocumentViewerBase
  {
    #region Methods and constructors
    public void DecreaseZoom()
    {
    }

    public DocumentViewer()
    {
    }

    public void Find()
    {
    }

    public void FitToHeight()
    {
    }

    public void FitToMaxPagesAcross()
    {
    }

    public void FitToMaxPagesAcross(int pagesAcross)
    {
    }

    public void FitToWidth()
    {
    }

    protected override System.Collections.ObjectModel.ReadOnlyCollection<System.Windows.Controls.Primitives.DocumentPageView> GetPageViewsCollection(out bool changed)
    {
      changed = default(bool);

      return default(System.Collections.ObjectModel.ReadOnlyCollection<System.Windows.Controls.Primitives.DocumentPageView>);
    }

    public void IncreaseZoom()
    {
    }

    public void MoveDown()
    {
    }

    public void MoveLeft()
    {
    }

    public void MoveRight()
    {
    }

    public void MoveUp()
    {
    }

    public override void OnApplyTemplate()
    {
    }

    protected override void OnBringIntoView(System.Windows.DependencyObject element, System.Windows.Rect rect, int pageNumber)
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

    protected virtual new void OnFitToHeightCommand()
    {
    }

    protected virtual new void OnFitToMaxPagesAcrossCommand()
    {
    }

    protected virtual new void OnFitToMaxPagesAcrossCommand(int pagesAcross)
    {
    }

    protected virtual new void OnFitToWidthCommand()
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

    protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected virtual new void OnMoveDownCommand()
    {
    }

    protected virtual new void OnMoveLeftCommand()
    {
    }

    protected virtual new void OnMoveRightCommand()
    {
    }

    protected virtual new void OnMoveUpCommand()
    {
    }

    protected override void OnNextPageCommand()
    {
    }

    protected override void OnPreviewMouseWheel(System.Windows.Input.MouseWheelEventArgs e)
    {
    }

    protected override void OnPreviousPageCommand()
    {
    }

    protected virtual new void OnScrollPageDownCommand()
    {
    }

    protected virtual new void OnScrollPageLeftCommand()
    {
    }

    protected virtual new void OnScrollPageRightCommand()
    {
    }

    protected virtual new void OnScrollPageUpCommand()
    {
    }

    protected virtual new void OnViewThumbnailsCommand()
    {
    }

    public void ScrollPageDown()
    {
    }

    public void ScrollPageLeft()
    {
    }

    public void ScrollPageRight()
    {
    }

    public void ScrollPageUp()
    {
    }

    public void ViewThumbnails()
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

    public bool CanIncreaseZoom
    {
      get
      {
        return default(bool);
      }
    }

    public bool CanMoveDown
    {
      get
      {
        return default(bool);
      }
    }

    public bool CanMoveLeft
    {
      get
      {
        return default(bool);
      }
    }

    public bool CanMoveRight
    {
      get
      {
        return default(bool);
      }
    }

    public bool CanMoveUp
    {
      get
      {
        return default(bool);
      }
    }

    public double ExtentHeight
    {
      get
      {
        return default(double);
      }
    }

    public double ExtentWidth
    {
      get
      {
        return default(double);
      }
    }

    public static System.Windows.Input.RoutedUICommand FitToHeightCommand
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand FitToMaxPagesAcrossCommand
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand FitToWidthCommand
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public double HorizontalOffset
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double HorizontalPageSpacing
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public int MaxPagesAcross
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public bool ShowPageBorders
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public double VerticalOffset
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double VerticalPageSpacing
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double ViewportHeight
    {
      get
      {
        return default(double);
      }
    }

    public double ViewportWidth
    {
      get
      {
        return default(double);
      }
    }

    public static System.Windows.Input.RoutedUICommand ViewThumbnailsCommand
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
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
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty CanDecreaseZoomProperty;
    public readonly static System.Windows.DependencyProperty CanIncreaseZoomProperty;
    public readonly static System.Windows.DependencyProperty CanMoveDownProperty;
    public readonly static System.Windows.DependencyProperty CanMoveLeftProperty;
    public readonly static System.Windows.DependencyProperty CanMoveRightProperty;
    public readonly static System.Windows.DependencyProperty CanMoveUpProperty;
    public readonly static System.Windows.DependencyProperty ExtentHeightProperty;
    public readonly static System.Windows.DependencyProperty ExtentWidthProperty;
    public readonly static System.Windows.DependencyProperty HorizontalOffsetProperty;
    public readonly static System.Windows.DependencyProperty HorizontalPageSpacingProperty;
    public readonly static System.Windows.DependencyProperty MaxPagesAcrossProperty;
    public readonly static System.Windows.DependencyProperty ShowPageBordersProperty;
    public readonly static System.Windows.DependencyProperty VerticalOffsetProperty;
    public readonly static System.Windows.DependencyProperty VerticalPageSpacingProperty;
    public readonly static System.Windows.DependencyProperty ViewportHeightProperty;
    public readonly static System.Windows.DependencyProperty ViewportWidthProperty;
    public readonly static System.Windows.DependencyProperty ZoomProperty;
    #endregion
  }
}

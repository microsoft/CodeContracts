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

// File System.Windows.Controls.Primitives.ScrollBar.cs
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


namespace System.Windows.Controls.Primitives
{
  public partial class ScrollBar : RangeBase
  {
    #region Methods and constructors
    public override void OnApplyTemplate()
    {
    }

    protected override void OnContextMenuClosing(System.Windows.Controls.ContextMenuEventArgs e)
    {
    }

    protected override void OnContextMenuOpening(System.Windows.Controls.ContextMenuEventArgs e)
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected override void OnPreviewMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected override void OnPreviewMouseRightButtonUp(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    public ScrollBar()
    {
    }
    #endregion

    #region Properties and indexers
    protected override bool IsEnabledCore
    {
      get
      {
        return default(bool);
      }
    }

    public System.Windows.Controls.Orientation Orientation
    {
      get
      {
        return default(System.Windows.Controls.Orientation);
      }
      set
      {
      }
    }

    public Track Track
    {
      get
      {
        return default(Track);
      }
    }

    public double ViewportSize
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

    #region Events
    public event ScrollEventHandler Scroll
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.Input.RoutedCommand DeferScrollToHorizontalOffsetCommand;
    public readonly static System.Windows.Input.RoutedCommand DeferScrollToVerticalOffsetCommand;
    public readonly static System.Windows.Input.RoutedCommand LineDownCommand;
    public readonly static System.Windows.Input.RoutedCommand LineLeftCommand;
    public readonly static System.Windows.Input.RoutedCommand LineRightCommand;
    public readonly static System.Windows.Input.RoutedCommand LineUpCommand;
    public readonly static System.Windows.DependencyProperty OrientationProperty;
    public readonly static System.Windows.Input.RoutedCommand PageDownCommand;
    public readonly static System.Windows.Input.RoutedCommand PageLeftCommand;
    public readonly static System.Windows.Input.RoutedCommand PageRightCommand;
    public readonly static System.Windows.Input.RoutedCommand PageUpCommand;
    public readonly static System.Windows.RoutedEvent ScrollEvent;
    public readonly static System.Windows.Input.RoutedCommand ScrollHereCommand;
    public readonly static System.Windows.Input.RoutedCommand ScrollToBottomCommand;
    public readonly static System.Windows.Input.RoutedCommand ScrollToEndCommand;
    public readonly static System.Windows.Input.RoutedCommand ScrollToHomeCommand;
    public readonly static System.Windows.Input.RoutedCommand ScrollToHorizontalOffsetCommand;
    public readonly static System.Windows.Input.RoutedCommand ScrollToLeftEndCommand;
    public readonly static System.Windows.Input.RoutedCommand ScrollToRightEndCommand;
    public readonly static System.Windows.Input.RoutedCommand ScrollToTopCommand;
    public readonly static System.Windows.Input.RoutedCommand ScrollToVerticalOffsetCommand;
    public readonly static System.Windows.DependencyProperty ViewportSizeProperty;
    #endregion
  }
}

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

// File System.Windows.Window.cs
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
  public partial class Window : System.Windows.Controls.ContentControl, IWindowService
  {
    #region Methods and constructors
    public bool Activate()
    {
      return default(bool);
    }

    protected override Size ArrangeOverride(Size arrangeBounds)
    {
      return default(Size);
    }

    public void Close()
    {
    }

    public void DragMove()
    {
    }

    public static System.Windows.Window GetWindow(DependencyObject dependencyObject)
    {
      return default(System.Windows.Window);
    }

    public void Hide()
    {
    }

    protected override Size MeasureOverride(Size availableSize)
    {
      return default(Size);
    }

    protected virtual new void OnActivated(EventArgs e)
    {
    }

    protected virtual new void OnClosed(EventArgs e)
    {
    }

    protected virtual new void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
    }

    protected override void OnContentChanged(Object oldContent, Object newContent)
    {
    }

    protected virtual new void OnContentRendered(EventArgs e)
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected virtual new void OnDeactivated(EventArgs e)
    {
    }

    protected virtual new void OnLocationChanged(EventArgs e)
    {
    }

    protected override void OnManipulationBoundaryFeedback(System.Windows.Input.ManipulationBoundaryFeedbackEventArgs e)
    {
    }

    protected virtual new void OnSourceInitialized(EventArgs e)
    {
    }

    protected virtual new void OnStateChanged(EventArgs e)
    {
    }

    protected sealed override void OnVisualParentChanged(DependencyObject oldParent)
    {
    }

    public void Show()
    {
    }

    public Nullable<bool> ShowDialog()
    {
      return default(Nullable<bool>);
    }

    public Window()
    {
      Contract.Ensures(false);
    }
    #endregion

    #region Properties and indexers
    public bool AllowsTransparency
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public Nullable<bool> DialogResult
    {
      get
      {
        return default(Nullable<bool>);
      }
      set
      {
      }
    }

    public System.Windows.Media.ImageSource Icon
    {
      get
      {
        return default(System.Windows.Media.ImageSource);
      }
      set
      {
      }
    }

    public bool IsActive
    {
      get
      {
        return default(bool);
      }
    }

    public double Left
    {
      get
      {
        return default(double);
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

    public WindowCollection OwnedWindows
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.WindowCollection>() != null);

        return default(WindowCollection);
      }
    }

    public System.Windows.Window Owner
    {
      get
      {
        return default(System.Windows.Window);
      }
      set
      {
      }
    }

    public ResizeMode ResizeMode
    {
      get
      {
        return default(ResizeMode);
      }
      set
      {
      }
    }

    public Rect RestoreBounds
    {
      get
      {
        return default(Rect);
      }
    }

    public bool ShowActivated
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool ShowInTaskbar
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public SizeToContent SizeToContent
    {
      get
      {
        return default(SizeToContent);
      }
      set
      {
      }
    }

    bool System.Windows.IWindowService.UserResized
    {
      get
      {
        return default(bool);
      }
    }

    public System.Windows.Shell.TaskbarItemInfo TaskbarItemInfo
    {
      get
      {
        return default(System.Windows.Shell.TaskbarItemInfo);
      }
      set
      {
      }
    }

    public string Title
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public double Top
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public bool Topmost
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public WindowStartupLocation WindowStartupLocation
    {
      get
      {
        return default(WindowStartupLocation);
      }
      set
      {
      }
    }

    public WindowState WindowState
    {
      get
      {
        return default(WindowState);
      }
      set
      {
      }
    }

    public WindowStyle WindowStyle
    {
      get
      {
        return default(WindowStyle);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event EventHandler Activated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler Closed
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.ComponentModel.CancelEventHandler Closing
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler ContentRendered
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler Deactivated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler LocationChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler SourceInitialized
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler StateChanged
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
    public readonly static DependencyProperty AllowsTransparencyProperty;
    public readonly static DependencyProperty IconProperty;
    public readonly static DependencyProperty IsActiveProperty;
    public readonly static DependencyProperty LeftProperty;
    public readonly static DependencyProperty ResizeModeProperty;
    public readonly static DependencyProperty ShowActivatedProperty;
    public readonly static DependencyProperty ShowInTaskbarProperty;
    public readonly static DependencyProperty SizeToContentProperty;
    public readonly static DependencyProperty TaskbarItemInfoProperty;
    public readonly static DependencyProperty TitleProperty;
    public readonly static DependencyProperty TopmostProperty;
    public readonly static DependencyProperty TopProperty;
    public readonly static DependencyProperty WindowStateProperty;
    public readonly static DependencyProperty WindowStyleProperty;
    #endregion
  }
}

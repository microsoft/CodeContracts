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

// File System.Windows.Interop.HwndHost.cs
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


namespace System.Windows.Interop
{
  abstract public partial class HwndHost : System.Windows.FrameworkElement, IDisposable, IWin32Window, IKeyboardInputSink
  {
    #region Methods and constructors
    protected abstract System.Runtime.InteropServices.HandleRef BuildWindowCore(System.Runtime.InteropServices.HandleRef hwndParent);

    protected abstract void DestroyWindowCore(System.Runtime.InteropServices.HandleRef hwnd);

    protected virtual new void Dispose(bool disposing)
    {
    }

    public void Dispose()
    {
    }

    protected virtual new bool HasFocusWithinCore()
    {
      return default(bool);
    }

    protected HwndHost()
    {
    }

    protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
    {
      return default(System.Windows.Size);
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
    {
        Contract.Requires(e != null);
    }

    protected override void OnKeyUp(System.Windows.Input.KeyEventArgs e)
    {
        Contract.Requires(e != null);
    }

    protected virtual new bool OnMnemonicCore(ref MSG msg, System.Windows.Input.ModifierKeys modifiers)
    {
      return default(bool);
    }

    protected virtual new void OnWindowPositionChanged(System.Windows.Rect rcBoundingBox)
    {
    }

    protected virtual new IKeyboardInputSite RegisterKeyboardInputSinkCore(IKeyboardInputSink sink)
    {
      return default(IKeyboardInputSite);
    }

    bool System.Windows.Interop.IKeyboardInputSink.HasFocusWithin()
    {
      return default(bool);
    }

    bool System.Windows.Interop.IKeyboardInputSink.OnMnemonic(ref MSG msg, System.Windows.Input.ModifierKeys modifiers)
    {
      return default(bool);
    }

    IKeyboardInputSite System.Windows.Interop.IKeyboardInputSink.RegisterKeyboardInputSink(IKeyboardInputSink sink)
    {
      return default(IKeyboardInputSite);
    }

    bool System.Windows.Interop.IKeyboardInputSink.TabInto(System.Windows.Input.TraversalRequest request)
    {
      return default(bool);
    }

    bool System.Windows.Interop.IKeyboardInputSink.TranslateAccelerator(ref MSG msg, System.Windows.Input.ModifierKeys modifiers)
    {
      return default(bool);
    }

    bool System.Windows.Interop.IKeyboardInputSink.TranslateChar(ref MSG msg, System.Windows.Input.ModifierKeys modifiers)
    {
      return default(bool);
    }

    protected virtual new bool TabIntoCore(System.Windows.Input.TraversalRequest request)
    {
      return default(bool);
    }

    protected virtual new bool TranslateAcceleratorCore(ref MSG msg, System.Windows.Input.ModifierKeys modifiers)
    {
      return default(bool);
    }

    protected virtual new bool TranslateCharCore(ref MSG msg, System.Windows.Input.ModifierKeys modifiers)
    {
      return default(bool);
    }

    public void UpdateWindowPos()
    {
    }

    protected virtual new IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
      return default(IntPtr);
    }
    #endregion

    #region Properties and indexers
    public IntPtr Handle
    {
      get
      {
        return default(IntPtr);
      }
    }

    IKeyboardInputSite System.Windows.Interop.IKeyboardInputSink.KeyboardInputSite
    {
      get
      {
        return default(IKeyboardInputSite);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event HwndSourceHook MessageHook
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

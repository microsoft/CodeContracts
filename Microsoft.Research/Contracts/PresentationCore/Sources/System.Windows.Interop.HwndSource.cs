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

// File System.Windows.Interop.HwndSource.cs
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
  public partial class HwndSource : System.Windows.PresentationSource, IDisposable, IWin32Window, IKeyboardInputSink
  {
    #region Methods and constructors
    public void AddHook(HwndSourceHook hook)
    {
    }

    public System.Runtime.InteropServices.HandleRef CreateHandleRef()
    {
      return default(System.Runtime.InteropServices.HandleRef);
    }

    public void Dispose()
    {
    }

    public static System.Windows.Interop.HwndSource FromHwnd(IntPtr hwnd)
    {
      return default(System.Windows.Interop.HwndSource);
    }

    protected override System.Windows.Media.CompositionTarget GetCompositionTargetCore()
    {
      return default(System.Windows.Media.CompositionTarget);
    }

    protected virtual new bool HasFocusWithinCore()
    {
      return default(bool);
    }

    public HwndSource(int classStyle, int style, int exStyle, int x, int y, string name, IntPtr parent)
    {
    }

    public HwndSource(int classStyle, int style, int exStyle, int x, int y, int width, int height, string name, IntPtr parent)
    {
    }

    public HwndSource(int classStyle, int style, int exStyle, int x, int y, int width, int height, string name, IntPtr parent, bool adjustSizingForNonClientArea)
    {
    }

    public HwndSource(HwndSourceParameters parameters)
    {
    }

    protected virtual new bool OnMnemonicCore(ref MSG msg, System.Windows.Input.ModifierKeys modifiers)
    {
      return default(bool);
    }

    protected IKeyboardInputSite RegisterKeyboardInputSinkCore(IKeyboardInputSink sink)
    {
      return default(IKeyboardInputSite);
    }

    public void RemoveHook(HwndSourceHook hook)
    {
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
    #endregion

    #region Properties and indexers
    public bool AcquireHwndFocusInMenuMode
    {
      get
      {
        return default(bool);
      }
    }

    public IEnumerable<IKeyboardInputSink> ChildKeyboardInputSinks
    {
      get
      {
        return default(IEnumerable<IKeyboardInputSink>);
      }
    }

    public HwndTarget CompositionTarget
    {
      get
      {
        return default(HwndTarget);
      }
    }

    public static bool DefaultAcquireHwndFocusInMenuMode
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public IntPtr Handle
    {
      get
      {
        return default(IntPtr);
      }
    }

    public override bool IsDisposed
    {
      get
      {
        return default(bool);
      }
    }

    protected IKeyboardInputSite KeyboardInputSiteCore
    {
      get
      {
        return default(IKeyboardInputSite);
      }
      set
      {
      }
    }

    public System.Windows.Input.RestoreFocusMode RestoreFocusMode
    {
      get
      {
        return default(System.Windows.Input.RestoreFocusMode);
      }
    }

    public override System.Windows.Media.Visual RootVisual
    {
      get
      {
        return default(System.Windows.Media.Visual);
      }
      set
      {
      }
    }

    public System.Windows.SizeToContent SizeToContent
    {
      get
      {
        return default(System.Windows.SizeToContent);
      }
      set
      {
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

    public bool UsesPerPixelOpacity
    {
      get
      {
        return default(bool);
      }
    }
    #endregion

    #region Events
    public event System.Windows.AutoResizedEventHandler AutoResized
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler Disposed
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler SizeToContentChanged
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

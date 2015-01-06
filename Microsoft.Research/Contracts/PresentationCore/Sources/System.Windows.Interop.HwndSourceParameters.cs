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

// File System.Windows.Interop.HwndSourceParameters.cs
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
  public partial struct HwndSourceParameters
  {
    #region Methods and constructors
    public static bool operator != (System.Windows.Interop.HwndSourceParameters a, System.Windows.Interop.HwndSourceParameters b)
    {
      return default(bool);
    }

    public static bool operator == (System.Windows.Interop.HwndSourceParameters a, System.Windows.Interop.HwndSourceParameters b)
    {
      return default(bool);
    }

    public bool Equals(System.Windows.Interop.HwndSourceParameters obj)
    {
      return default(bool);
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public HwndSourceParameters(string name)
    {
    }

    public HwndSourceParameters(string name, int width, int height)
    {
    }

    public void SetPosition(int x, int y)
    {
    }

    public void SetSize(int width, int height)
    {
    }
    #endregion

    #region Properties and indexers
    public bool AcquireHwndFocusInMenuMode
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool AdjustSizingForNonClientArea
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public int ExtendedWindowStyle
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public bool HasAssignedSize
    {
      get
      {
        return default(bool);
      }
    }

    public int Height
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public HwndSourceHook HwndSourceHook
    {
      get
      {
        return default(HwndSourceHook);
      }
      set
      {
      }
    }

    public IntPtr ParentWindow
    {
      get
      {
        return default(IntPtr);
      }
      set
      {
      }
    }

    public int PositionX
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int PositionY
    {
      get
      {
        return default(int);
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
      set
      {
      }
    }

    public int Width
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int WindowClassStyle
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string WindowName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public int WindowStyle
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }
    #endregion
  }
}

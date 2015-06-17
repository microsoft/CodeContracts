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

// File System.Windows.Input.InputManager.cs
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


namespace System.Windows.Input
{
  sealed public partial class InputManager : System.Windows.Threading.DispatcherObject
  {
    #region Methods and constructors
    private InputManager()
    {
    }

    public void PopMenuMode(System.Windows.PresentationSource menuSite)
    {
    }

    public bool ProcessInput(InputEventArgs input)
    {
      return default(bool);
    }

    public void PushMenuMode(System.Windows.PresentationSource menuSite)
    {
    }
    #endregion

    #region Properties and indexers
    public static System.Windows.Input.InputManager Current
    {
      get
      {
        return default(System.Windows.Input.InputManager);
      }
    }

    public System.Collections.ICollection InputProviders
    {
      get
      {
        return default(System.Collections.ICollection);
      }
    }

    public bool IsInMenuMode
    {
      get
      {
        return default(bool);
      }
    }

    public InputDevice MostRecentInputDevice
    {
      get
      {
        return default(InputDevice);
      }
      internal set
      {
      }
    }

    public KeyboardDevice PrimaryKeyboardDevice
    {
      get
      {
        return default(KeyboardDevice);
      }
    }

    public MouseDevice PrimaryMouseDevice
    {
      get
      {
        return default(MouseDevice);
      }
    }
    #endregion

    #region Events
    public event EventHandler EnterMenuMode
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler HitTestInvalidatedAsync
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler LeaveMenuMode
    {
      add
      {
      }
      remove
      {
      }
    }

    public event NotifyInputEventHandler PostNotifyInput
    {
      add
      {
      }
      remove
      {
      }
    }

    public event ProcessInputEventHandler PostProcessInput
    {
      add
      {
      }
      remove
      {
      }
    }

    public event NotifyInputEventHandler PreNotifyInput
    {
      add
      {
      }
      remove
      {
      }
    }

    public event PreProcessInputEventHandler PreProcessInput
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

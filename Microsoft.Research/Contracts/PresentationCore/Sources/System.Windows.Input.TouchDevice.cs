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

// File System.Windows.Input.TouchDevice.cs
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
  abstract public partial class TouchDevice : InputDevice, IManipulator
  {
    #region Methods and constructors
    protected void Activate()
    {
    }

    public bool Capture(System.Windows.IInputElement element, CaptureMode captureMode)
    {
      return default(bool);
    }

    public bool Capture(System.Windows.IInputElement element)
    {
      return default(bool);
    }

    protected void Deactivate()
    {
    }

    public abstract TouchPointCollection GetIntermediateTouchPoints(System.Windows.IInputElement relativeTo);

    public abstract TouchPoint GetTouchPoint(System.Windows.IInputElement relativeTo);

    protected virtual new void OnCapture(System.Windows.IInputElement element, CaptureMode captureMode)
    {
    }

    protected virtual new void OnManipulationEnded(bool cancel)
    {
    }

    protected virtual new void OnManipulationStarted()
    {
    }

    protected bool ReportDown()
    {
      return default(bool);
    }

    protected bool ReportMove()
    {
      return default(bool);
    }

    protected bool ReportUp()
    {
      return default(bool);
    }

    protected void SetActiveSource(System.Windows.PresentationSource activeSource)
    {
    }

    public void Synchronize()
    {
    }

    System.Windows.Point System.Windows.Input.IManipulator.GetPosition(System.Windows.IInputElement relativeTo)
    {
      return default(System.Windows.Point);
    }

    void System.Windows.Input.IManipulator.ManipulationEnded(bool cancel)
    {
    }

    protected TouchDevice(int deviceId)
    {
    }
    #endregion

    #region Properties and indexers
    public override sealed System.Windows.PresentationSource ActiveSource
    {
      get
      {
        return default(System.Windows.PresentationSource);
      }
    }

    public System.Windows.IInputElement Captured
    {
      get
      {
        return default(System.Windows.IInputElement);
      }
    }

    public CaptureMode CaptureMode
    {
      get
      {
        return default(CaptureMode);
      }
    }

    public System.Windows.IInputElement DirectlyOver
    {
      get
      {
        return default(System.Windows.IInputElement);
      }
    }

    public int Id
    {
      get
      {
        return default(int);
      }
    }

    public bool IsActive
    {
      get
      {
        return default(bool);
      }
    }

    int System.Windows.Input.IManipulator.Id
    {
      get
      {
        return default(int);
      }
    }

    public override sealed System.Windows.IInputElement Target
    {
      get
      {
        return default(System.Windows.IInputElement);
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

    public event EventHandler Deactivated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler Updated
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

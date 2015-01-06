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

// File System.Windows.Media.Animation.Clock.cs
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


namespace System.Windows.Media.Animation
{
  public partial class Clock : System.Windows.Threading.DispatcherObject
  {
    #region Methods and constructors
    protected internal Clock(Timeline timeline)
    {
    }

    protected virtual new void DiscontinuousTimeMovement()
    {
    }

    protected virtual new bool GetCanSlip()
    {
      return default(bool);
    }

    protected virtual new TimeSpan GetCurrentTimeCore()
    {
      return default(TimeSpan);
    }

    protected virtual new void SpeedChanged()
    {
    }

    protected virtual new void Stopped()
    {
    }
    #endregion

    #region Properties and indexers
    public ClockController Controller
    {
      get
      {
        return default(ClockController);
      }
    }

    public Nullable<double> CurrentGlobalSpeed
    {
      get
      {
        return default(Nullable<double>);
      }
    }

    protected TimeSpan CurrentGlobalTime
    {
      get
      {
        return default(TimeSpan);
      }
    }

    public Nullable<int> CurrentIteration
    {
      get
      {
        return default(Nullable<int>);
      }
    }

    public Nullable<double> CurrentProgress
    {
      get
      {
        return default(Nullable<double>);
      }
    }

    public ClockState CurrentState
    {
      get
      {
        return default(ClockState);
      }
    }

    public Nullable<TimeSpan> CurrentTime
    {
      get
      {
        return default(Nullable<TimeSpan>);
      }
    }

    public bool HasControllableRoot
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsPaused
    {
      get
      {
        return default(bool);
      }
    }

    public System.Windows.Duration NaturalDuration
    {
      get
      {
        return default(System.Windows.Duration);
      }
    }

    public System.Windows.Media.Animation.Clock Parent
    {
      get
      {
        return default(System.Windows.Media.Animation.Clock);
      }
    }

    public Timeline Timeline
    {
      get
      {
        return default(Timeline);
      }
    }
    #endregion

    #region Events
    public event EventHandler Completed
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler CurrentGlobalSpeedInvalidated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler CurrentStateInvalidated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler CurrentTimeInvalidated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler RemoveRequested
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

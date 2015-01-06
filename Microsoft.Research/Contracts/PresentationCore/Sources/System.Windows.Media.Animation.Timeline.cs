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

// File System.Windows.Media.Animation.Timeline.cs
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
  abstract public partial class Timeline : Animatable
  {
    #region Methods and constructors
    protected internal virtual new Clock AllocateClock()
    {
      return default(Clock);
    }

    public Timeline Clone()
    {
      return default(Timeline);
    }

    public Timeline CloneCurrentValue()
    {
      return default(Timeline);
    }

    public Clock CreateClock(bool hasControllableRoot)
    {
      return default(Clock);
    }

    public Clock CreateClock()
    {
      return default(Clock);
    }

    protected override bool FreezeCore(bool isChecking)
    {
      return default(bool);
    }

    protected override void GetAsFrozenCore(System.Windows.Freezable sourceFreezable)
    {
    }

    protected override void GetCurrentValueAsFrozenCore(System.Windows.Freezable sourceFreezable)
    {
    }

    public static Nullable<int> GetDesiredFrameRate(System.Windows.Media.Animation.Timeline timeline)
    {
      return default(Nullable<int>);
    }

    protected internal System.Windows.Duration GetNaturalDuration(Clock clock)
    {
      return default(System.Windows.Duration);
    }

    protected virtual new System.Windows.Duration GetNaturalDurationCore(Clock clock)
    {
      return default(System.Windows.Duration);
    }

    public static void SetDesiredFrameRate(System.Windows.Media.Animation.Timeline timeline, Nullable<int> desiredFrameRate)
    {
    }

    protected Timeline(Nullable<TimeSpan> beginTime, System.Windows.Duration duration)
    {
    }

    protected Timeline(Nullable<TimeSpan> beginTime, System.Windows.Duration duration, RepeatBehavior repeatBehavior)
    {
    }

    protected Timeline()
    {
    }

    protected Timeline(Nullable<TimeSpan> beginTime)
    {
    }
    #endregion

    #region Properties and indexers
    public double AccelerationRatio
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public bool AutoReverse
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public Nullable<TimeSpan> BeginTime
    {
      get
      {
        return default(Nullable<TimeSpan>);
      }
      set
      {
      }
    }

    public double DecelerationRatio
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public System.Windows.Duration Duration
    {
      get
      {
        return default(System.Windows.Duration);
      }
      set
      {
      }
    }

    public FillBehavior FillBehavior
    {
      get
      {
        return default(FillBehavior);
      }
      set
      {
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public RepeatBehavior RepeatBehavior
    {
      get
      {
        return default(RepeatBehavior);
      }
      set
      {
      }
    }

    public double SpeedRatio
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

    #region Fields
    public readonly static System.Windows.DependencyProperty AccelerationRatioProperty;
    public readonly static System.Windows.DependencyProperty AutoReverseProperty;
    public readonly static System.Windows.DependencyProperty BeginTimeProperty;
    public readonly static System.Windows.DependencyProperty DecelerationRatioProperty;
    public readonly static System.Windows.DependencyProperty DesiredFrameRateProperty;
    public readonly static System.Windows.DependencyProperty DurationProperty;
    public readonly static System.Windows.DependencyProperty FillBehaviorProperty;
    public readonly static System.Windows.DependencyProperty NameProperty;
    public readonly static System.Windows.DependencyProperty RepeatBehaviorProperty;
    public readonly static System.Windows.DependencyProperty SpeedRatioProperty;
    #endregion
  }
}

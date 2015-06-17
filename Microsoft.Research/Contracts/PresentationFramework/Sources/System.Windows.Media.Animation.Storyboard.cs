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

// File System.Windows.Media.Animation.Storyboard.cs
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
  public partial class Storyboard : ParallelTimeline
  {
    #region Methods and constructors
    public void Begin(System.Windows.FrameworkElement containingObject, System.Windows.FrameworkTemplate frameworkTemplate, bool isControllable)
    {
    }

    public void Begin(System.Windows.FrameworkElement containingObject, System.Windows.FrameworkTemplate frameworkTemplate, HandoffBehavior handoffBehavior, bool isControllable)
    {
    }

    public void Begin(System.Windows.FrameworkElement containingObject, System.Windows.FrameworkTemplate frameworkTemplate)
    {
    }

    public void Begin(System.Windows.FrameworkElement containingObject, bool isControllable)
    {
    }

    public void Begin(System.Windows.FrameworkElement containingObject, HandoffBehavior handoffBehavior, bool isControllable)
    {
    }

    public void Begin(System.Windows.FrameworkContentElement containingObject, HandoffBehavior handoffBehavior, bool isControllable)
    {
    }

    public void Begin()
    {
    }

    public void Begin(System.Windows.FrameworkContentElement containingObject, bool isControllable)
    {
    }

    public void Begin(System.Windows.FrameworkContentElement containingObject)
    {
    }

    public void Begin(System.Windows.FrameworkContentElement containingObject, HandoffBehavior handoffBehavior)
    {
    }

    public void Begin(System.Windows.FrameworkElement containingObject, HandoffBehavior handoffBehavior)
    {
    }

    public void Begin(System.Windows.FrameworkElement containingObject, System.Windows.FrameworkTemplate frameworkTemplate, HandoffBehavior handoffBehavior)
    {
    }

    public void Begin(System.Windows.FrameworkElement containingObject)
    {
    }

    public System.Windows.Media.Animation.Storyboard Clone()
    {
      return default(System.Windows.Media.Animation.Storyboard);
    }

    protected override System.Windows.Freezable CreateInstanceCore()
    {
      return default(System.Windows.Freezable);
    }

    public Nullable<double> GetCurrentGlobalSpeed(System.Windows.FrameworkContentElement containingObject)
    {
      return default(Nullable<double>);
    }

    public Nullable<double> GetCurrentGlobalSpeed(System.Windows.FrameworkElement containingObject)
    {
      return default(Nullable<double>);
    }

    public double GetCurrentGlobalSpeed()
    {
      return default(double);
    }

    public Nullable<int> GetCurrentIteration(System.Windows.FrameworkContentElement containingObject)
    {
      return default(Nullable<int>);
    }

    public Nullable<int> GetCurrentIteration(System.Windows.FrameworkElement containingObject)
    {
      return default(Nullable<int>);
    }

    public int GetCurrentIteration()
    {
      return default(int);
    }

    public Nullable<double> GetCurrentProgress(System.Windows.FrameworkElement containingObject)
    {
      return default(Nullable<double>);
    }

    public double GetCurrentProgress()
    {
      return default(double);
    }

    public Nullable<double> GetCurrentProgress(System.Windows.FrameworkContentElement containingObject)
    {
      return default(Nullable<double>);
    }

    public ClockState GetCurrentState()
    {
      return default(ClockState);
    }

    public ClockState GetCurrentState(System.Windows.FrameworkContentElement containingObject)
    {
      return default(ClockState);
    }

    public ClockState GetCurrentState(System.Windows.FrameworkElement containingObject)
    {
      return default(ClockState);
    }

    public Nullable<TimeSpan> GetCurrentTime(System.Windows.FrameworkElement containingObject)
    {
      return default(Nullable<TimeSpan>);
    }

    public TimeSpan GetCurrentTime()
    {
      return default(TimeSpan);
    }

    public Nullable<TimeSpan> GetCurrentTime(System.Windows.FrameworkContentElement containingObject)
    {
      return default(Nullable<TimeSpan>);
    }

    public bool GetIsPaused(System.Windows.FrameworkContentElement containingObject)
    {
      return default(bool);
    }

    public bool GetIsPaused()
    {
      return default(bool);
    }

    public bool GetIsPaused(System.Windows.FrameworkElement containingObject)
    {
      return default(bool);
    }

    public static System.Windows.DependencyObject GetTarget(System.Windows.DependencyObject element)
    {
      return default(System.Windows.DependencyObject);
    }

    public static string GetTargetName(System.Windows.DependencyObject element)
    {
      return default(string);
    }

    public static System.Windows.PropertyPath GetTargetProperty(System.Windows.DependencyObject element)
    {
      return default(System.Windows.PropertyPath);
    }

    public void Pause(System.Windows.FrameworkContentElement containingObject)
    {
    }

    public void Pause()
    {
    }

    public void Pause(System.Windows.FrameworkElement containingObject)
    {
    }

    public void Remove(System.Windows.FrameworkElement containingObject)
    {
    }

    public void Remove(System.Windows.FrameworkContentElement containingObject)
    {
    }

    public void Remove()
    {
    }

    public void Resume(System.Windows.FrameworkElement containingObject)
    {
    }

    public void Resume(System.Windows.FrameworkContentElement containingObject)
    {
    }

    public void Resume()
    {
    }

    public void Seek(TimeSpan offset, TimeSeekOrigin origin)
    {
    }

    public void Seek(System.Windows.FrameworkElement containingObject, TimeSpan offset, TimeSeekOrigin origin)
    {
    }

    public void Seek(TimeSpan offset)
    {
    }

    public void Seek(System.Windows.FrameworkContentElement containingObject, TimeSpan offset, TimeSeekOrigin origin)
    {
    }

    public void SeekAlignedToLastTick(System.Windows.FrameworkElement containingObject, TimeSpan offset, TimeSeekOrigin origin)
    {
    }

    public void SeekAlignedToLastTick(TimeSpan offset)
    {
    }

    public void SeekAlignedToLastTick(TimeSpan offset, TimeSeekOrigin origin)
    {
    }

    public void SeekAlignedToLastTick(System.Windows.FrameworkContentElement containingObject, TimeSpan offset, TimeSeekOrigin origin)
    {
    }

    public void SetSpeedRatio(double speedRatio)
    {
    }

    public void SetSpeedRatio(System.Windows.FrameworkContentElement containingObject, double speedRatio)
    {
    }

    public void SetSpeedRatio(System.Windows.FrameworkElement containingObject, double speedRatio)
    {
    }

    public static void SetTarget(System.Windows.DependencyObject element, System.Windows.DependencyObject value)
    {
    }

    public static void SetTargetName(System.Windows.DependencyObject element, string name)
    {
    }

    public static void SetTargetProperty(System.Windows.DependencyObject element, System.Windows.PropertyPath path)
    {
    }

    public void SkipToFill(System.Windows.FrameworkContentElement containingObject)
    {
    }

    public void SkipToFill()
    {
    }

    public void SkipToFill(System.Windows.FrameworkElement containingObject)
    {
    }

    public void Stop(System.Windows.FrameworkContentElement containingObject)
    {
    }

    public void Stop(System.Windows.FrameworkElement containingObject)
    {
    }

    public void Stop()
    {
    }

    public Storyboard()
    {
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty TargetNameProperty;
    public readonly static System.Windows.DependencyProperty TargetProperty;
    public readonly static System.Windows.DependencyProperty TargetPropertyProperty;
    #endregion
  }
}

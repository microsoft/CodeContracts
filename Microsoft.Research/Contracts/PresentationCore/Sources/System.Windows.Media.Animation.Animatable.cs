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

// File System.Windows.Media.Animation.Animatable.cs
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
  abstract public partial class Animatable : System.Windows.Freezable, IAnimatable, System.Windows.Media.Composition.DUCE.IResource
  {
    #region Methods and constructors
    protected Animatable()
    {
    }

    public void ApplyAnimationClock(System.Windows.DependencyProperty dp, AnimationClock clock)
    {
    }

    public void ApplyAnimationClock(System.Windows.DependencyProperty dp, AnimationClock clock, HandoffBehavior handoffBehavior)
    {
    }

    public void BeginAnimation(System.Windows.DependencyProperty dp, AnimationTimeline animation, HandoffBehavior handoffBehavior)
    {
    }

    public void BeginAnimation(System.Windows.DependencyProperty dp, AnimationTimeline animation)
    {
    }

    public System.Windows.Media.Animation.Animatable Clone()
    {
      return default(System.Windows.Media.Animation.Animatable);
    }

    protected override bool FreezeCore(bool isChecking)
    {
      return default(bool);
    }

    public Object GetAnimationBaseValue(System.Windows.DependencyProperty dp)
    {
      return default(Object);
    }

    public static bool ShouldSerializeStoredWeakReference(System.Windows.DependencyObject target)
    {
      return default(bool);
    }

    int System.Windows.Media.Composition.DUCE.IResource.GetChannelCount()
    {
      return default(int);
    }
    #endregion

    #region Properties and indexers
    public bool HasAnimatedProperties
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}

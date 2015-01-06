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

// File System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames.cs
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
  public partial class QuaternionAnimationUsingKeyFrames : QuaternionAnimationBase, IKeyFrameAnimation, System.Windows.Markup.IAddChild
  {
    #region Methods and constructors
    protected virtual new void AddChild(Object child)
    {
    }

    protected virtual new void AddText(string childText)
    {
    }

    public System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames Clone()
    {
      return default(System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames);
    }

    protected override void CloneCore(System.Windows.Freezable sourceFreezable)
    {
    }

    public System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames CloneCurrentValue()
    {
      return default(System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames);
    }

    protected override void CloneCurrentValueCore(System.Windows.Freezable sourceFreezable)
    {
    }

    protected override System.Windows.Freezable CreateInstanceCore()
    {
      return default(System.Windows.Freezable);
    }

    protected override bool FreezeCore(bool isChecking)
    {
      return default(bool);
    }

    protected override void GetAsFrozenCore(System.Windows.Freezable source)
    {
    }

    protected override void GetCurrentValueAsFrozenCore(System.Windows.Freezable source)
    {
    }

    protected sealed override System.Windows.Media.Media3D.Quaternion GetCurrentValueCore(System.Windows.Media.Media3D.Quaternion defaultOriginValue, System.Windows.Media.Media3D.Quaternion defaultDestinationValue, AnimationClock animationClock)
    {
      return default(System.Windows.Media.Media3D.Quaternion);
    }

    protected sealed override System.Windows.Duration GetNaturalDurationCore(Clock clock)
    {
      return default(System.Windows.Duration);
    }

    protected override void OnChanged()
    {
    }

    public QuaternionAnimationUsingKeyFrames()
    {
    }

    public bool ShouldSerializeKeyFrames()
    {
      return default(bool);
    }

    void System.Windows.Markup.IAddChild.AddChild(Object child)
    {
    }

    void System.Windows.Markup.IAddChild.AddText(string childText)
    {
    }
    #endregion

    #region Properties and indexers
    public bool IsAdditive
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsCumulative
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public QuaternionKeyFrameCollection KeyFrames
    {
      get
      {
        return default(QuaternionKeyFrameCollection);
      }
      set
      {
      }
    }

    System.Collections.IList System.Windows.Media.Animation.IKeyFrameAnimation.KeyFrames
    {
      get
      {
        return default(System.Collections.IList);
      }
      set
      {
      }
    }
    #endregion
  }
}

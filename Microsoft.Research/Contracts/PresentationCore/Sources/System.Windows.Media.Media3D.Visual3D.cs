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

// File System.Windows.Media.Media3D.Visual3D.cs
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


namespace System.Windows.Media.Media3D
{
  abstract public partial class Visual3D : System.Windows.DependencyObject, System.Windows.Media.Composition.DUCE.IResource, MS.Internal.IVisual3DContainer, System.Windows.Media.Animation.IAnimatable
  {
    #region Methods and constructors
    protected void AddVisual3DChild(Visual3D child)
    {
    }

    public void ApplyAnimationClock(System.Windows.DependencyProperty dp, System.Windows.Media.Animation.AnimationClock clock)
    {
    }

    public void ApplyAnimationClock(System.Windows.DependencyProperty dp, System.Windows.Media.Animation.AnimationClock clock, System.Windows.Media.Animation.HandoffBehavior handoffBehavior)
    {
    }

    public void BeginAnimation(System.Windows.DependencyProperty dp, System.Windows.Media.Animation.AnimationTimeline animation, System.Windows.Media.Animation.HandoffBehavior handoffBehavior)
    {
    }

    public void BeginAnimation(System.Windows.DependencyProperty dp, System.Windows.Media.Animation.AnimationTimeline animation)
    {
    }

    public System.Windows.DependencyObject FindCommonVisualAncestor(System.Windows.DependencyObject otherVisual)
    {
      return default(System.Windows.DependencyObject);
    }

    public Object GetAnimationBaseValue(System.Windows.DependencyProperty dp)
    {
      return default(Object);
    }

    protected virtual new Visual3D GetVisual3DChild(int index)
    {
      return default(Visual3D);
    }

    public bool IsAncestorOf(System.Windows.DependencyObject descendant)
    {
      return default(bool);
    }

    public bool IsDescendantOf(System.Windows.DependencyObject ancestor)
    {
      return default(bool);
    }

    void MS.Internal.IVisual3DContainer.AddChild(Visual3D child)
    {
    }

    Visual3D MS.Internal.IVisual3DContainer.GetChild(int index)
    {
      return default(Visual3D);
    }

    int MS.Internal.IVisual3DContainer.GetChildrenCount()
    {
      return default(int);
    }

    void MS.Internal.IVisual3DContainer.RemoveChild(Visual3D child)
    {
    }

    void MS.Internal.IVisual3DContainer.VerifyAPIReadOnly()
    {
    }

    void MS.Internal.IVisual3DContainer.VerifyAPIReadOnly(System.Windows.DependencyObject other)
    {
    }

    void MS.Internal.IVisual3DContainer.VerifyAPIReadWrite()
    {
    }

    void MS.Internal.IVisual3DContainer.VerifyAPIReadWrite(System.Windows.DependencyObject other)
    {
    }

    protected internal virtual new void OnVisualChildrenChanged(System.Windows.DependencyObject visualAdded, System.Windows.DependencyObject visualRemoved)
    {
    }

    protected internal virtual new void OnVisualParentChanged(System.Windows.DependencyObject oldParent)
    {
    }

    protected void RemoveVisual3DChild(Visual3D child)
    {
    }

    int System.Windows.Media.Composition.DUCE.IResource.GetChannelCount()
    {
      return default(int);
    }

    public GeneralTransform3D TransformToAncestor(Visual3D ancestor)
    {
      return default(GeneralTransform3D);
    }

    public GeneralTransform3DTo2D TransformToAncestor(System.Windows.Media.Visual ancestor)
    {
      return default(GeneralTransform3DTo2D);
    }

    public GeneralTransform3D TransformToDescendant(Visual3D descendant)
    {
      return default(GeneralTransform3D);
    }

    internal Visual3D()
    {
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

    public Transform3D Transform
    {
      get
      {
        return default(Transform3D);
      }
      set
      {
      }
    }

    protected virtual new int Visual3DChildrenCount
    {
      get
      {
        return default(int);
      }
    }

    protected Model3D Visual3DModel
    {
      get
      {
        return default(Model3D);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty TransformProperty;
    #endregion
  }
}

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

// File System.Windows.Media.Visual.cs
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


namespace System.Windows.Media
{
  abstract public partial class Visual : System.Windows.DependencyObject, System.Windows.Media.Composition.DUCE.IResource
  {
    #region Methods and constructors
    protected void AddVisualChild(System.Windows.Media.Visual child)
    {
    }

    public System.Windows.DependencyObject FindCommonVisualAncestor(System.Windows.DependencyObject otherVisual)
    {
      return default(System.Windows.DependencyObject);
    }

    protected virtual new System.Windows.Media.Visual GetVisualChild(int index)
    {
      return default(System.Windows.Media.Visual);
    }

    protected virtual new GeometryHitTestResult HitTestCore(GeometryHitTestParameters hitTestParameters)
    {
      return default(GeometryHitTestResult);
    }

    protected virtual new HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
    {
      return default(HitTestResult);
    }

    public bool IsAncestorOf(System.Windows.DependencyObject descendant)
    {
      return default(bool);
    }

    public bool IsDescendantOf(System.Windows.DependencyObject ancestor)
    {
      return default(bool);
    }

    protected internal virtual new void OnVisualChildrenChanged(System.Windows.DependencyObject visualAdded, System.Windows.DependencyObject visualRemoved)
    {
    }

    protected internal virtual new void OnVisualParentChanged(System.Windows.DependencyObject oldParent)
    {
    }

    public System.Windows.Point PointFromScreen(System.Windows.Point point)
    {
      return default(System.Windows.Point);
    }

    public System.Windows.Point PointToScreen(System.Windows.Point point)
    {
      return default(System.Windows.Point);
    }

    protected void RemoveVisualChild(System.Windows.Media.Visual child)
    {
    }

    int System.Windows.Media.Composition.DUCE.IResource.GetChannelCount()
    {
      return default(int);
    }

    public GeneralTransform TransformToAncestor(System.Windows.Media.Visual ancestor)
    {
      return default(GeneralTransform);
    }

    public System.Windows.Media.Media3D.GeneralTransform2DTo3D TransformToAncestor(System.Windows.Media.Media3D.Visual3D ancestor)
    {
      return default(System.Windows.Media.Media3D.GeneralTransform2DTo3D);
    }

    public GeneralTransform TransformToDescendant(System.Windows.Media.Visual descendant)
    {
      return default(GeneralTransform);
    }

    public GeneralTransform TransformToVisual(System.Windows.Media.Visual visual)
    {
      return default(GeneralTransform);
    }

    protected Visual()
    {
    }
    #endregion

    #region Properties and indexers
    internal protected System.Windows.Media.Effects.BitmapEffect VisualBitmapEffect
    {
      get
      {
        return default(System.Windows.Media.Effects.BitmapEffect);
      }
      protected set
      {
      }
    }

    internal protected System.Windows.Media.Effects.BitmapEffectInput VisualBitmapEffectInput
    {
      get
      {
        return default(System.Windows.Media.Effects.BitmapEffectInput);
      }
      protected set
      {
      }
    }

    internal protected BitmapScalingMode VisualBitmapScalingMode
    {
      get
      {
        return default(BitmapScalingMode);
      }
      protected set
      {
      }
    }

    internal protected CacheMode VisualCacheMode
    {
      get
      {
        return default(CacheMode);
      }
      protected set
      {
      }
    }

    protected virtual new int VisualChildrenCount
    {
      get
      {
        return default(int);
      }
    }

    internal protected ClearTypeHint VisualClearTypeHint
    {
      get
      {
        return default(ClearTypeHint);
      }
      set
      {
      }
    }

    internal protected Geometry VisualClip
    {
      get
      {
        return default(Geometry);
      }
      protected set
      {
      }
    }

    internal protected EdgeMode VisualEdgeMode
    {
      get
      {
        return default(EdgeMode);
      }
      protected set
      {
      }
    }

    internal protected System.Windows.Media.Effects.Effect VisualEffect
    {
      get
      {
        return default(System.Windows.Media.Effects.Effect);
      }
      protected set
      {
      }
    }

    internal protected System.Windows.Vector VisualOffset
    {
      get
      {
        return default(System.Windows.Vector);
      }
      protected set
      {
      }
    }

    internal protected double VisualOpacity
    {
      get
      {
        return default(double);
      }
      protected set
      {
      }
    }

    internal protected Brush VisualOpacityMask
    {
      get
      {
        return default(Brush);
      }
      protected set
      {
      }
    }

    protected System.Windows.DependencyObject VisualParent
    {
      get
      {
        return default(System.Windows.DependencyObject);
      }
    }

    internal protected Nullable<System.Windows.Rect> VisualScrollableAreaClip
    {
      get
      {
        return default(Nullable<System.Windows.Rect>);
      }
      protected set
      {
      }
    }

    internal protected TextHintingMode VisualTextHintingMode
    {
      get
      {
        return default(TextHintingMode);
      }
      set
      {
      }
    }

    internal protected TextRenderingMode VisualTextRenderingMode
    {
      get
      {
        return default(TextRenderingMode);
      }
      set
      {
      }
    }

    internal protected Transform VisualTransform
    {
      get
      {
        return default(Transform);
      }
      protected set
      {
      }
    }

    internal protected DoubleCollection VisualXSnappingGuidelines
    {
      get
      {
        return default(DoubleCollection);
      }
      protected set
      {
      }
    }

    internal protected DoubleCollection VisualYSnappingGuidelines
    {
      get
      {
        return default(DoubleCollection);
      }
      protected set
      {
      }
    }
    #endregion
  }
}

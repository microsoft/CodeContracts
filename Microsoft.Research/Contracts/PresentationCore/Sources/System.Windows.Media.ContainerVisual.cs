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

// File System.Windows.Media.ContainerVisual.cs
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
  public partial class ContainerVisual : Visual
  {
    #region Methods and constructors
    public ContainerVisual()
    {
    }

    protected sealed override Visual GetVisualChild(int index)
    {
      return default(Visual);
    }

    public void HitTest(HitTestFilterCallback filterCallback, HitTestResultCallback resultCallback, HitTestParameters hitTestParameters)
    {
    }

    public HitTestResult HitTest(System.Windows.Point point)
    {
      return default(HitTestResult);
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Media.Effects.BitmapEffect BitmapEffect
    {
      get
      {
        return default(System.Windows.Media.Effects.BitmapEffect);
      }
      set
      {
      }
    }

    public System.Windows.Media.Effects.BitmapEffectInput BitmapEffectInput
    {
      get
      {
        return default(System.Windows.Media.Effects.BitmapEffectInput);
      }
      set
      {
      }
    }

    public CacheMode CacheMode
    {
      get
      {
        return default(CacheMode);
      }
      set
      {
      }
    }

    public VisualCollection Children
    {
      get
      {
        return default(VisualCollection);
      }
    }

    public Geometry Clip
    {
      get
      {
        return default(Geometry);
      }
      set
      {
      }
    }

    public System.Windows.Rect ContentBounds
    {
      get
      {
        return default(System.Windows.Rect);
      }
    }

    public System.Windows.Rect DescendantBounds
    {
      get
      {
        return default(System.Windows.Rect);
      }
    }

    public System.Windows.Media.Effects.Effect Effect
    {
      get
      {
        return default(System.Windows.Media.Effects.Effect);
      }
      set
      {
      }
    }

    public System.Windows.Vector Offset
    {
      get
      {
        return default(System.Windows.Vector);
      }
      set
      {
      }
    }

    public double Opacity
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public Brush OpacityMask
    {
      get
      {
        return default(Brush);
      }
      set
      {
      }
    }

    public System.Windows.DependencyObject Parent
    {
      get
      {
        return default(System.Windows.DependencyObject);
      }
    }

    public Transform Transform
    {
      get
      {
        return default(Transform);
      }
      set
      {
      }
    }

    protected override sealed int VisualChildrenCount
    {
      get
      {
        return default(int);
      }
    }

    public DoubleCollection XSnappingGuidelines
    {
      get
      {
        return default(DoubleCollection);
      }
      set
      {
      }
    }

    public DoubleCollection YSnappingGuidelines
    {
      get
      {
        return default(DoubleCollection);
      }
      set
      {
      }
    }
    #endregion
  }
}

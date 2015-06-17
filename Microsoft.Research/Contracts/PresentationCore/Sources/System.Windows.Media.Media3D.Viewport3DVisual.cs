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

// File System.Windows.Media.Media3D.Viewport3DVisual.cs
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
  sealed public partial class Viewport3DVisual : System.Windows.Media.Visual, System.Windows.Media.Composition.DUCE.IResource, MS.Internal.IVisual3DContainer
  {
    #region Methods and constructors
    public System.Windows.Media.HitTestResult HitTest(System.Windows.Point point)
    {
      return default(System.Windows.Media.HitTestResult);
    }

    public void HitTest(System.Windows.Media.HitTestFilterCallback filterCallback, System.Windows.Media.HitTestResultCallback resultCallback, System.Windows.Media.HitTestParameters hitTestParameters)
    {
    }

    protected override System.Windows.Media.GeometryHitTestResult HitTestCore(System.Windows.Media.GeometryHitTestParameters hitTestParameters)
    {
      return default(System.Windows.Media.GeometryHitTestResult);
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

    void MS.Internal.IVisual3DContainer.VerifyAPIReadOnly(System.Windows.DependencyObject other)
    {
    }

    void MS.Internal.IVisual3DContainer.VerifyAPIReadOnly()
    {
    }

    void MS.Internal.IVisual3DContainer.VerifyAPIReadWrite()
    {
    }

    void MS.Internal.IVisual3DContainer.VerifyAPIReadWrite(System.Windows.DependencyObject other)
    {
    }

    int System.Windows.Media.Composition.DUCE.IResource.GetChannelCount()
    {
      return default(int);
    }

    public Viewport3DVisual()
    {
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

    public Camera Camera
    {
      get
      {
        return default(Camera);
      }
      set
      {
      }
    }

    public Visual3DCollection Children
    {
      get
      {
        return default(Visual3DCollection);
      }
    }

    public System.Windows.Media.Geometry Clip
    {
      get
      {
        return default(System.Windows.Media.Geometry);
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

    public System.Windows.Media.Brush OpacityMask
    {
      get
      {
        return default(System.Windows.Media.Brush);
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

    public System.Windows.Media.Transform Transform
    {
      get
      {
        return default(System.Windows.Media.Transform);
      }
      set
      {
      }
    }

    public System.Windows.Rect Viewport
    {
      get
      {
        return default(System.Windows.Rect);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty CameraProperty;
    public readonly static System.Windows.DependencyProperty ViewportProperty;
    #endregion
  }
}

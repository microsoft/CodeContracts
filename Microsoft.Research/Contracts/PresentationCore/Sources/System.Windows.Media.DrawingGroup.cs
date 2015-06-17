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

// File System.Windows.Media.DrawingGroup.cs
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
  sealed public partial class DrawingGroup : Drawing
  {
    #region Methods and constructors
    public DrawingContext Append()
    {
      return default(DrawingContext);
    }

    public System.Windows.Media.DrawingGroup Clone()
    {
      return default(System.Windows.Media.DrawingGroup);
    }

    public System.Windows.Media.DrawingGroup CloneCurrentValue()
    {
      return default(System.Windows.Media.DrawingGroup);
    }

    protected override System.Windows.Freezable CreateInstanceCore()
    {
      return default(System.Windows.Freezable);
    }

    public DrawingGroup()
    {
    }

    public DrawingContext Open()
    {
      return default(DrawingContext);
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

    public DrawingCollection Children
    {
      get
      {
        return default(DrawingCollection);
      }
      set
      {
      }
    }

    public Geometry ClipGeometry
    {
      get
      {
        return default(Geometry);
      }
      set
      {
      }
    }

    public GuidelineSet GuidelineSet
    {
      get
      {
        return default(GuidelineSet);
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
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty BitmapEffectInputProperty;
    public readonly static System.Windows.DependencyProperty BitmapEffectProperty;
    public readonly static System.Windows.DependencyProperty ChildrenProperty;
    public readonly static System.Windows.DependencyProperty ClipGeometryProperty;
    public readonly static System.Windows.DependencyProperty GuidelineSetProperty;
    public readonly static System.Windows.DependencyProperty OpacityMaskProperty;
    public readonly static System.Windows.DependencyProperty OpacityProperty;
    public readonly static System.Windows.DependencyProperty TransformProperty;
    #endregion
  }
}

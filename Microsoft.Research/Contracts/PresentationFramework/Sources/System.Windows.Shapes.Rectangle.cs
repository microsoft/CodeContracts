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

// File System.Windows.Shapes.Rectangle.cs
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


namespace System.Windows.Shapes
{
  sealed public partial class Rectangle : Shape
  {
    #region Methods and constructors
    protected override System.Windows.Size ArrangeOverride(System.Windows.Size finalSize)
    {
      return default(System.Windows.Size);
    }

    protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
    {
      return default(System.Windows.Size);
    }

    protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
    {
    }

    public Rectangle()
    {
    }
    #endregion

    #region Properties and indexers
    protected override System.Windows.Media.Geometry DefiningGeometry
    {
      get
      {
        return default(System.Windows.Media.Geometry);
      }
    }

    public override System.Windows.Media.Transform GeometryTransform
    {
      get
      {
        return default(System.Windows.Media.Transform);
      }
    }

    public double RadiusX
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double RadiusY
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public override System.Windows.Media.Geometry RenderedGeometry
    {
      get
      {
        return default(System.Windows.Media.Geometry);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty RadiusXProperty;
    public readonly static System.Windows.DependencyProperty RadiusYProperty;
    #endregion
  }
}

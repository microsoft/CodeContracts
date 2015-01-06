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

// File System.Windows.Media.Geometry.cs
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
  abstract public partial class Geometry : System.Windows.Media.Animation.Animatable, IFormattable, System.Windows.Media.Composition.DUCE.IResource
  {
    #region Methods and constructors
    public Geometry Clone()
    {
      return default(Geometry);
    }

    public Geometry CloneCurrentValue()
    {
      return default(Geometry);
    }

    public static PathGeometry Combine(Geometry geometry1, Geometry geometry2, GeometryCombineMode mode, Transform transform, double tolerance, ToleranceType type)
    {
      return default(PathGeometry);
    }

    public static PathGeometry Combine(Geometry geometry1, Geometry geometry2, GeometryCombineMode mode, Transform transform)
    {
      return default(PathGeometry);
    }

    public bool FillContains(System.Windows.Point hitPoint)
    {
      return default(bool);
    }

    public bool FillContains(System.Windows.Point hitPoint, double tolerance, ToleranceType type)
    {
      return default(bool);
    }

    public bool FillContains(Geometry geometry)
    {
      return default(bool);
    }

    public bool FillContains(Geometry geometry, double tolerance, ToleranceType type)
    {
      return default(bool);
    }

    public IntersectionDetail FillContainsWithDetail(Geometry geometry)
    {
      return default(IntersectionDetail);
    }

    public virtual new IntersectionDetail FillContainsWithDetail(Geometry geometry, double tolerance, ToleranceType type)
    {
      return default(IntersectionDetail);
    }

    internal Geometry()
    {
    }

    public virtual new double GetArea(double tolerance, ToleranceType type)
    {
      return default(double);
    }

    public double GetArea()
    {
      return default(double);
    }

    public PathGeometry GetFlattenedPathGeometry()
    {
      return default(PathGeometry);
    }

    public virtual new PathGeometry GetFlattenedPathGeometry(double tolerance, ToleranceType type)
    {
      return default(PathGeometry);
    }

    public virtual new PathGeometry GetOutlinedPathGeometry(double tolerance, ToleranceType type)
    {
      return default(PathGeometry);
    }

    public PathGeometry GetOutlinedPathGeometry()
    {
      return default(PathGeometry);
    }

    public System.Windows.Rect GetRenderBounds(Pen pen)
    {
      return default(System.Windows.Rect);
    }

    public virtual new System.Windows.Rect GetRenderBounds(Pen pen, double tolerance, ToleranceType type)
    {
      return default(System.Windows.Rect);
    }

    public virtual new PathGeometry GetWidenedPathGeometry(Pen pen, double tolerance, ToleranceType type)
    {
      return default(PathGeometry);
    }

    public PathGeometry GetWidenedPathGeometry(Pen pen)
    {
      return default(PathGeometry);
    }

    public abstract bool IsEmpty();

    public abstract bool MayHaveCurves();

    public static Geometry Parse(string source)
    {
      return default(Geometry);
    }

    public bool ShouldSerializeTransform()
    {
      return default(bool);
    }

    public bool StrokeContains(Pen pen, System.Windows.Point hitPoint, double tolerance, ToleranceType type)
    {
      return default(bool);
    }

    public bool StrokeContains(Pen pen, System.Windows.Point hitPoint)
    {
      return default(bool);
    }

    public IntersectionDetail StrokeContainsWithDetail(Pen pen, Geometry geometry, double tolerance, ToleranceType type)
    {
      return default(IntersectionDetail);
    }

    public IntersectionDetail StrokeContainsWithDetail(Pen pen, Geometry geometry)
    {
      return default(IntersectionDetail);
    }

    string System.IFormattable.ToString(string format, IFormatProvider provider)
    {
      return default(string);
    }

    int System.Windows.Media.Composition.DUCE.IResource.GetChannelCount()
    {
      return default(int);
    }

    public string ToString(IFormatProvider provider)
    {
      return default(string);
    }

    #endregion

    #region Properties and indexers
    public virtual new System.Windows.Rect Bounds
    {
      get
      {
        return default(System.Windows.Rect);
      }
    }

    public static System.Windows.Media.Geometry Empty
    {
      get
      {
        return default(System.Windows.Media.Geometry);
      }
    }

    public static double StandardFlatteningTolerance
    {
      get
      {
        return default(double);
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
    public readonly static System.Windows.DependencyProperty TransformProperty;
    #endregion
  }
}

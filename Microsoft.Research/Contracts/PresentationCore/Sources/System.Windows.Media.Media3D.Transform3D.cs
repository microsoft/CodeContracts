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

// File System.Windows.Media.Media3D.Transform3D.cs
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
  abstract public partial class Transform3D : GeneralTransform3D, System.Windows.Media.Composition.DUCE.IResource
  {
    #region Methods and constructors
    public Transform3D Clone()
    {
      return default(Transform3D);
    }

    public Transform3D CloneCurrentValue()
    {
      return default(Transform3D);
    }

    int System.Windows.Media.Composition.DUCE.IResource.GetChannelCount()
    {
      return default(int);
    }

    public Vector3D Transform(Vector3D vector)
    {
      return default(Vector3D);
    }

    public Point3D Transform(Point3D point)
    {
      return default(Point3D);
    }

    public Point4D Transform(Point4D point)
    {
      return default(Point4D);
    }

    public void Transform(Point4D[] points)
    {
    }

    public void Transform(Vector3D[] vectors)
    {
    }

    public void Transform(Point3D[] points)
    {
    }

    internal Transform3D()
    {
    }

    public override Rect3D TransformBounds(Rect3D rect)
    {
      return default(Rect3D);
    }

    public override bool TryTransform(Point3D inPoint, out Point3D result)
    {
      result = default(Point3D);

      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public static System.Windows.Media.Media3D.Transform3D Identity
    {
      get
      {
        return default(System.Windows.Media.Media3D.Transform3D);
      }
    }

    public override GeneralTransform3D Inverse
    {
      get
      {
        return default(GeneralTransform3D);
      }
    }

    public abstract bool IsAffine
    {
      get;
    }

    public abstract Matrix3D Value
    {
      get;
    }
    #endregion
  }
}

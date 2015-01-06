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

// File System.Windows.Media.Media3D.Matrix3D.cs
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
  public partial struct Matrix3D : IFormattable
  {
    #region Methods and constructors
    public static bool operator != (System.Windows.Media.Media3D.Matrix3D matrix1, System.Windows.Media.Media3D.Matrix3D matrix2)
    {
      return default(bool);
    }

    public static System.Windows.Media.Media3D.Matrix3D operator * (System.Windows.Media.Media3D.Matrix3D matrix1, System.Windows.Media.Media3D.Matrix3D matrix2)
    {
      return default(System.Windows.Media.Media3D.Matrix3D);
    }

    public static bool operator == (System.Windows.Media.Media3D.Matrix3D matrix1, System.Windows.Media.Media3D.Matrix3D matrix2)
    {
      return default(bool);
    }

    public void Append(System.Windows.Media.Media3D.Matrix3D matrix)
    {
    }

    public static bool Equals(System.Windows.Media.Media3D.Matrix3D matrix1, System.Windows.Media.Media3D.Matrix3D matrix2)
    {
      return default(bool);
    }

    public bool Equals(System.Windows.Media.Media3D.Matrix3D value)
    {
      return default(bool);
    }

    public override bool Equals(Object o)
    {
      return default(bool);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public void Invert()
    {
    }

    public Matrix3D(double m11, double m12, double m13, double m14, double m21, double m22, double m23, double m24, double m31, double m32, double m33, double m34, double offsetX, double offsetY, double offsetZ, double m44)
    {
    }

    public static System.Windows.Media.Media3D.Matrix3D Multiply(System.Windows.Media.Media3D.Matrix3D matrix1, System.Windows.Media.Media3D.Matrix3D matrix2)
    {
      return default(System.Windows.Media.Media3D.Matrix3D);
    }

    public static System.Windows.Media.Media3D.Matrix3D Parse(string source)
    {
      return default(System.Windows.Media.Media3D.Matrix3D);
    }

    public void Prepend(System.Windows.Media.Media3D.Matrix3D matrix)
    {
    }

    public void Rotate(Quaternion quaternion)
    {
    }

    public void RotateAt(Quaternion quaternion, Point3D center)
    {
    }

    public void RotateAtPrepend(Quaternion quaternion, Point3D center)
    {
    }

    public void RotatePrepend(Quaternion quaternion)
    {
    }

    public void Scale(Vector3D scale)
    {
    }

    public void ScaleAt(Vector3D scale, Point3D center)
    {
    }

    public void ScaleAtPrepend(Vector3D scale, Point3D center)
    {
    }

    public void ScalePrepend(Vector3D scale)
    {
    }

    public void SetIdentity()
    {
    }

    string System.IFormattable.ToString(string format, IFormatProvider provider)
    {
      return default(string);
    }

    public string ToString(IFormatProvider provider)
    {
      return default(string);
    }

    public Vector3D Transform(Vector3D vector)
    {
      return default(Vector3D);
    }

    public void Transform(Vector3D[] vectors)
    {
    }

    public Point3D Transform(Point3D point)
    {
      return default(Point3D);
    }

    public void Transform(Point4D[] points)
    {
    }

    public Point4D Transform(Point4D point)
    {
      return default(Point4D);
    }

    public void Transform(Point3D[] points)
    {
    }

    public void Translate(Vector3D offset)
    {
    }

    public void TranslatePrepend(Vector3D offset)
    {
    }
    #endregion

    #region Properties and indexers
    public double Determinant
    {
      get
      {
        return default(double);
      }
    }

    public bool HasInverse
    {
      get
      {
        return default(bool);
      }
    }

    public static System.Windows.Media.Media3D.Matrix3D Identity
    {
      get
      {
        return default(System.Windows.Media.Media3D.Matrix3D);
      }
    }

    public bool IsAffine
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsIdentity
    {
      get
      {
        return default(bool);
      }
    }

    public double M11
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double M12
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double M13
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double M14
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double M21
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double M22
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double M23
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double M24
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double M31
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double M32
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double M33
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double M34
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double M44
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double OffsetX
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double OffsetY
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double OffsetZ
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }
    #endregion
  }
}

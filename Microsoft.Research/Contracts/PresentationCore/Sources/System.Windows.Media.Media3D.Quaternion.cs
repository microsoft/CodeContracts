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

// File System.Windows.Media.Media3D.Quaternion.cs
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
  public partial struct Quaternion : IFormattable
  {
    #region Methods and constructors
    public static System.Windows.Media.Media3D.Quaternion operator - (System.Windows.Media.Media3D.Quaternion left, System.Windows.Media.Media3D.Quaternion right)
    {
      return default(System.Windows.Media.Media3D.Quaternion);
    }

    public static bool operator != (System.Windows.Media.Media3D.Quaternion quaternion1, System.Windows.Media.Media3D.Quaternion quaternion2)
    {
      return default(bool);
    }

    public static System.Windows.Media.Media3D.Quaternion operator * (System.Windows.Media.Media3D.Quaternion left, System.Windows.Media.Media3D.Quaternion right)
    {
      return default(System.Windows.Media.Media3D.Quaternion);
    }

    public static System.Windows.Media.Media3D.Quaternion operator + (System.Windows.Media.Media3D.Quaternion left, System.Windows.Media.Media3D.Quaternion right)
    {
      return default(System.Windows.Media.Media3D.Quaternion);
    }

    public static bool operator == (System.Windows.Media.Media3D.Quaternion quaternion1, System.Windows.Media.Media3D.Quaternion quaternion2)
    {
      return default(bool);
    }

    public static System.Windows.Media.Media3D.Quaternion Add(System.Windows.Media.Media3D.Quaternion left, System.Windows.Media.Media3D.Quaternion right)
    {
      return default(System.Windows.Media.Media3D.Quaternion);
    }

    public void Conjugate()
    {
    }

    public override bool Equals(Object o)
    {
      return default(bool);
    }

    public bool Equals(System.Windows.Media.Media3D.Quaternion value)
    {
      return default(bool);
    }

    public static bool Equals(System.Windows.Media.Media3D.Quaternion quaternion1, System.Windows.Media.Media3D.Quaternion quaternion2)
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

    public static System.Windows.Media.Media3D.Quaternion Multiply(System.Windows.Media.Media3D.Quaternion left, System.Windows.Media.Media3D.Quaternion right)
    {
      return default(System.Windows.Media.Media3D.Quaternion);
    }

    public void Normalize()
    {
    }

    public static System.Windows.Media.Media3D.Quaternion Parse(string source)
    {
      return default(System.Windows.Media.Media3D.Quaternion);
    }

    public Quaternion(double x, double y, double z, double w)
    {
    }

    public Quaternion(Vector3D axisOfRotation, double angleInDegrees)
    {
    }

    public static System.Windows.Media.Media3D.Quaternion Slerp(System.Windows.Media.Media3D.Quaternion from, System.Windows.Media.Media3D.Quaternion to, double t)
    {
      return default(System.Windows.Media.Media3D.Quaternion);
    }

    public static System.Windows.Media.Media3D.Quaternion Slerp(System.Windows.Media.Media3D.Quaternion from, System.Windows.Media.Media3D.Quaternion to, double t, bool useShortestPath)
    {
      return default(System.Windows.Media.Media3D.Quaternion);
    }

    public static System.Windows.Media.Media3D.Quaternion Subtract(System.Windows.Media.Media3D.Quaternion left, System.Windows.Media.Media3D.Quaternion right)
    {
      return default(System.Windows.Media.Media3D.Quaternion);
    }

    string System.IFormattable.ToString(string format, IFormatProvider provider)
    {
      return default(string);
    }

    public string ToString(IFormatProvider provider)
    {
      return default(string);
    }

    #endregion

    #region Properties and indexers
    public double Angle
    {
      get
      {
        return default(double);
      }
    }

    public Vector3D Axis
    {
      get
      {
        return default(Vector3D);
      }
    }

    public static System.Windows.Media.Media3D.Quaternion Identity
    {
      get
      {
        return default(System.Windows.Media.Media3D.Quaternion);
      }
    }

    public bool IsIdentity
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsNormalized
    {
      get
      {
        return default(bool);
      }
    }

    public double W
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double X
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double Y
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double Z
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

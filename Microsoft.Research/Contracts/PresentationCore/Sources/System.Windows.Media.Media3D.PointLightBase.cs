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

// File System.Windows.Media.Media3D.PointLightBase.cs
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
  abstract public partial class PointLightBase : Light
  {
    #region Methods and constructors
    public PointLightBase Clone()
    {
      return default(PointLightBase);
    }

    public PointLightBase CloneCurrentValue()
    {
      return default(PointLightBase);
    }

    internal PointLightBase()
    {
    }
    #endregion

    #region Properties and indexers
    public double ConstantAttenuation
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double LinearAttenuation
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public Point3D Position
    {
      get
      {
        return default(Point3D);
      }
      set
      {
      }
    }

    public double QuadraticAttenuation
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double Range
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

    #region Fields
    public readonly static System.Windows.DependencyProperty ConstantAttenuationProperty;
    public readonly static System.Windows.DependencyProperty LinearAttenuationProperty;
    public readonly static System.Windows.DependencyProperty PositionProperty;
    public readonly static System.Windows.DependencyProperty QuadraticAttenuationProperty;
    public readonly static System.Windows.DependencyProperty RangeProperty;
    #endregion
  }
}

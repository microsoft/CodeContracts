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

// File System.Windows.Media.Pen.cs
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
  sealed public partial class Pen : System.Windows.Media.Animation.Animatable, System.Windows.Media.Composition.DUCE.IResource
  {
    #region Methods and constructors
    public Pen Clone()
    {
      return default(Pen);
    }

    public Pen CloneCurrentValue()
    {
      return default(Pen);
    }

    protected override System.Windows.Freezable CreateInstanceCore()
    {
      return default(System.Windows.Freezable);
    }

    public Pen(Brush brush, double thickness)
    {
    }

    public Pen()
    {
    }

    int System.Windows.Media.Composition.DUCE.IResource.GetChannelCount()
    {
      return default(int);
    }
    #endregion

    #region Properties and indexers
    public Brush Brush
    {
      get
      {
        return default(Brush);
      }
      set
      {
      }
    }

    public PenLineCap DashCap
    {
      get
      {
        return default(PenLineCap);
      }
      set
      {
      }
    }

    public DashStyle DashStyle
    {
      get
      {
        return default(DashStyle);
      }
      set
      {
      }
    }

    public PenLineCap EndLineCap
    {
      get
      {
        return default(PenLineCap);
      }
      set
      {
      }
    }

    public PenLineJoin LineJoin
    {
      get
      {
        return default(PenLineJoin);
      }
      set
      {
      }
    }

    public double MiterLimit
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public PenLineCap StartLineCap
    {
      get
      {
        return default(PenLineCap);
      }
      set
      {
      }
    }

    public double Thickness
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
    public readonly static System.Windows.DependencyProperty BrushProperty;
    public readonly static System.Windows.DependencyProperty DashCapProperty;
    public readonly static System.Windows.DependencyProperty DashStyleProperty;
    public readonly static System.Windows.DependencyProperty EndLineCapProperty;
    public readonly static System.Windows.DependencyProperty LineJoinProperty;
    public readonly static System.Windows.DependencyProperty MiterLimitProperty;
    public readonly static System.Windows.DependencyProperty StartLineCapProperty;
    public readonly static System.Windows.DependencyProperty ThicknessProperty;
    #endregion
  }
}

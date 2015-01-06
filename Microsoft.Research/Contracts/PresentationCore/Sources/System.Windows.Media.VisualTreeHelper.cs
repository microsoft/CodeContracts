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

// File System.Windows.Media.VisualTreeHelper.cs
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
  static public partial class VisualTreeHelper
  {
    #region Methods and constructors
    public static System.Windows.Media.Effects.BitmapEffect GetBitmapEffect(Visual reference)
    {
      Contract.Requires(reference != null);

      // May return null

      return default(System.Windows.Media.Effects.BitmapEffect);
    }

    public static System.Windows.Media.Effects.BitmapEffectInput GetBitmapEffectInput(Visual reference)
    {
      Contract.Requires(reference != null);
      
      // May return null
      return default(System.Windows.Media.Effects.BitmapEffectInput);
    }

    public static CacheMode GetCacheMode(Visual reference)
    {
      return default(CacheMode);
    }

    public static System.Windows.DependencyObject GetChild(System.Windows.DependencyObject reference, int childIndex)
    {
      Contract.Requires(reference != null);

      Contract.Ensures(Contract.Result<System.Windows.DependencyObject>() != null);

      return default(System.Windows.DependencyObject);
    }

    public static int GetChildrenCount(System.Windows.DependencyObject reference)
    {
      Contract.Requires(reference != null);

      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }

    public static Geometry GetClip(Visual reference)
    {
      Contract.Requires(reference != null);

      // May return null

      return default(Geometry);
    }

    public static System.Windows.Rect GetContentBounds(Visual reference)
    {
      Contract.Requires(reference != null);

      return default(System.Windows.Rect);
    }

    public static System.Windows.Media.Media3D.Rect3D GetContentBounds(System.Windows.Media.Media3D.Visual3D reference)
    {
      Contract.Requires(reference != null);

      return default(System.Windows.Media.Media3D.Rect3D);
    }

    public static System.Windows.Media.Media3D.Rect3D GetDescendantBounds(System.Windows.Media.Media3D.Visual3D reference)
    {
      Contract.Requires(reference != null);

      return default(System.Windows.Media.Media3D.Rect3D);
    }

    public static System.Windows.Rect GetDescendantBounds(Visual reference)
    {
      Contract.Requires(reference != null);

      return default(System.Windows.Rect);
    }

    public static DrawingGroup GetDrawing(Visual reference)
    {
      Contract.Requires(reference != null);

      return default(DrawingGroup);
    }

    public static EdgeMode GetEdgeMode(Visual reference)
    {
      Contract.Requires(reference != null);

      return default(EdgeMode);
    }

    public static System.Windows.Media.Effects.Effect GetEffect(Visual reference)
    {
      Contract.Requires(reference != null);

      return default(System.Windows.Media.Effects.Effect);
    }

    public static System.Windows.Vector GetOffset(Visual reference)
    {
      Contract.Requires(reference != null);

      return default(System.Windows.Vector);
    }

    public static double GetOpacity(Visual reference)
    {
      Contract.Requires(reference != null);

      return default(double);
    }

    public static Brush GetOpacityMask(Visual reference)
    {
      Contract.Requires(reference != null);

      return default(Brush);
    }

    public static System.Windows.DependencyObject GetParent(System.Windows.DependencyObject reference)
    {
      Contract.Requires(reference != null);

      return default(System.Windows.DependencyObject);
    }

    public static Transform GetTransform(Visual reference)
    {
      Contract.Requires(reference != null);

      return default(Transform);
    }

    public static DoubleCollection GetXSnappingGuidelines(Visual reference)
    {
      Contract.Requires(reference != null);
      
      return default(DoubleCollection);
    }

    public static DoubleCollection GetYSnappingGuidelines(Visual reference)
    {
      Contract.Requires(reference != null);
      
      return default(DoubleCollection);
    }

    public static void HitTest(System.Windows.Media.Media3D.Visual3D reference, HitTestFilterCallback filterCallback, HitTestResultCallback resultCallback, System.Windows.Media.Media3D.HitTestParameters3D hitTestParameters)
    {
      Contract.Requires(reference != null);

    }

    public static void HitTest(Visual reference, HitTestFilterCallback filterCallback, HitTestResultCallback resultCallback, HitTestParameters hitTestParameters)
    {
      Contract.Requires(reference != null);

    }

    public static HitTestResult HitTest(Visual reference, System.Windows.Point point)
    {
      Contract.Requires(reference != null);

      return default(HitTestResult);
    }
    #endregion
  }
}

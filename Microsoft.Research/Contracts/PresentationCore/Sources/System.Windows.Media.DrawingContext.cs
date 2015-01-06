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

// File System.Windows.Media.DrawingContext.cs
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
  abstract public partial class DrawingContext : System.Windows.Threading.DispatcherObject, IDisposable
  {
    #region Methods and constructors
    public abstract void Close();

    protected abstract void DisposeCore();

    public abstract void DrawDrawing(Drawing drawing);

    public abstract void DrawEllipse(Brush brush, Pen pen, System.Windows.Point center, System.Windows.Media.Animation.AnimationClock centerAnimations, double radiusX, System.Windows.Media.Animation.AnimationClock radiusXAnimations, double radiusY, System.Windows.Media.Animation.AnimationClock radiusYAnimations);

    public abstract void DrawEllipse(Brush brush, Pen pen, System.Windows.Point center, double radiusX, double radiusY);

    public abstract void DrawGeometry(Brush brush, Pen pen, Geometry geometry);

    public abstract void DrawGlyphRun(Brush foregroundBrush, GlyphRun glyphRun);

    public abstract void DrawImage(ImageSource imageSource, System.Windows.Rect rectangle);

    public abstract void DrawImage(ImageSource imageSource, System.Windows.Rect rectangle, System.Windows.Media.Animation.AnimationClock rectangleAnimations);

    internal DrawingContext()
    {
    }

    public abstract void DrawLine(Pen pen, System.Windows.Point point0, System.Windows.Media.Animation.AnimationClock point0Animations, System.Windows.Point point1, System.Windows.Media.Animation.AnimationClock point1Animations);

    public abstract void DrawLine(Pen pen, System.Windows.Point point0, System.Windows.Point point1);

    public abstract void DrawRectangle(Brush brush, Pen pen, System.Windows.Rect rectangle);

    public abstract void DrawRectangle(Brush brush, Pen pen, System.Windows.Rect rectangle, System.Windows.Media.Animation.AnimationClock rectangleAnimations);

    public abstract void DrawRoundedRectangle(Brush brush, Pen pen, System.Windows.Rect rectangle, double radiusX, double radiusY);

    public abstract void DrawRoundedRectangle(Brush brush, Pen pen, System.Windows.Rect rectangle, System.Windows.Media.Animation.AnimationClock rectangleAnimations, double radiusX, System.Windows.Media.Animation.AnimationClock radiusXAnimations, double radiusY, System.Windows.Media.Animation.AnimationClock radiusYAnimations);

    public void DrawText(FormattedText formattedText, System.Windows.Point origin)
    {
    }

    public abstract void DrawVideo(MediaPlayer player, System.Windows.Rect rectangle);

    public abstract void DrawVideo(MediaPlayer player, System.Windows.Rect rectangle, System.Windows.Media.Animation.AnimationClock rectangleAnimations);

    public abstract void Pop();

    public abstract void PushClip(Geometry clipGeometry);

    public abstract void PushEffect(System.Windows.Media.Effects.BitmapEffect effect, System.Windows.Media.Effects.BitmapEffectInput effectInput);

    public abstract void PushGuidelineSet(GuidelineSet guidelines);

    public abstract void PushOpacity(double opacity, System.Windows.Media.Animation.AnimationClock opacityAnimations);

    public abstract void PushOpacity(double opacity);

    public abstract void PushOpacityMask(Brush opacityMask);

    public abstract void PushTransform(Transform transform);

    void System.IDisposable.Dispose()
    {
    }

    protected virtual new void VerifyApiNonstructuralChange()
    {
    }
    #endregion
  }
}

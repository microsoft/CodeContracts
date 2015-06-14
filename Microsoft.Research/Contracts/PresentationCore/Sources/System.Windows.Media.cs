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

// File System.Windows.Media.cs
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
  public enum AlignmentX
  {
    Left = 0, 
    Center = 1, 
    Right = 2, 
  }

  public enum AlignmentY
  {
    Top = 0, 
    Center = 1, 
    Bottom = 2, 
  }

  public enum BitmapScalingMode
  {
    Unspecified = 0, 
    LowQuality = 1, 
    HighQuality = 2, 
    Linear = 1, 
    Fant = 2, 
    NearestNeighbor = 3, 
  }

  public enum BrushMappingMode
  {
    Absolute = 0, 
    RelativeToBoundingBox = 1, 
  }

  public enum CachingHint
  {
    Unspecified = 0, 
    Cache = 1, 
  }

  public enum ClearTypeHint
  {
    Auto = 0, 
    Enabled = 1, 
  }

  public enum ColorInterpolationMode
  {
    ScRgbLinearInterpolation = 0, 
    SRgbLinearInterpolation = 1, 
  }

  public enum EdgeMode
  {
    Unspecified = 0, 
    Aliased = 1, 
  }

  public enum FillRule
  {
    EvenOdd = 0, 
    Nonzero = 1, 
  }

  public enum FontEmbeddingRight
  {
    Installable = 0, 
    InstallableButNoSubsetting = 1, 
    InstallableButWithBitmapsOnly = 2, 
    InstallableButNoSubsettingAndWithBitmapsOnly = 3, 
    RestrictedLicense = 4, 
    PreviewAndPrint = 5, 
    PreviewAndPrintButNoSubsetting = 6, 
    PreviewAndPrintButWithBitmapsOnly = 7, 
    PreviewAndPrintButNoSubsettingAndWithBitmapsOnly = 8, 
    Editable = 9, 
    EditableButNoSubsetting = 10, 
    EditableButWithBitmapsOnly = 11, 
    EditableButNoSubsettingAndWithBitmapsOnly = 12, 
  }

  public enum GeometryCombineMode
  {
    Union = 0, 
    Intersect = 1, 
    Xor = 2, 
    Exclude = 3, 
  }

  public enum GradientSpreadMethod
  {
    Pad = 0, 
    Reflect = 1, 
    Repeat = 2, 
  }

  public enum HitTestFilterBehavior
  {
    ContinueSkipChildren = 2, 
    ContinueSkipSelfAndChildren = 0, 
    ContinueSkipSelf = 4, 
    Continue = 6, 
    Stop = 8, 
  }

  public delegate HitTestFilterBehavior HitTestFilterCallback(System.Windows.DependencyObject potentialHitTestTarget);

  public enum HitTestResultBehavior
  {
    Stop = 0, 
    Continue = 1, 
  }

  public delegate HitTestResultBehavior HitTestResultCallback(HitTestResult result);

  public enum IntersectionDetail
  {
    NotCalculated = 0, 
    Empty = 1, 
    FullyInside = 2, 
    FullyContains = 3, 
    Intersects = 4, 
  }

  public enum NumberCultureSource
  {
    Text = 0, 
    User = 1, 
    Override = 2, 
  }

  public enum NumberSubstitutionMethod
  {
    AsCulture = 0, 
    Context = 1, 
    European = 2, 
    NativeNational = 3, 
    Traditional = 4, 
  }

  public enum PenLineCap
  {
    Flat = 0, 
    Square = 1, 
    Round = 2, 
    Triangle = 3, 
  }

  public enum PenLineJoin
  {
    Miter = 0, 
    Bevel = 1, 
    Round = 2, 
  }

  public enum Stretch
  {
    None = 0, 
    Fill = 1, 
    Uniform = 2, 
    UniformToFill = 3, 
  }

  public enum StyleSimulations
  {
    None = 0, 
    BoldSimulation = 1, 
    ItalicSimulation = 2, 
    BoldItalicSimulation = 3, 
  }

  public enum SweepDirection
  {
    Counterclockwise = 0, 
    Clockwise = 1, 
  }

  public enum TextFormattingMode
  {
    Display = 1, 
    Ideal = 0, 
  }

  public enum TextHintingMode
  {
    Auto = 0, 
    Fixed = 1, 
    Animated = 2, 
  }

  public enum TextRenderingMode
  {
    Auto = 0, 
    Aliased = 1, 
    Grayscale = 2, 
    ClearType = 3, 
  }

  public enum TileMode
  {
    None = 0, 
    Tile = 4, 
    FlipX = 1, 
    FlipY = 2, 
    FlipXY = 3, 
  }

  public enum ToleranceType
  {
    Absolute = 0, 
    Relative = 1, 
  }
}

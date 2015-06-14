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

// File System.Windows.Ink.cs
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


namespace System.Windows.Ink
{
  public enum ApplicationGesture
  {
    AllGestures = 0, 
    ArrowDown = 61497, 
    ArrowLeft = 61498, 
    ArrowRight = 61499, 
    ArrowUp = 61496, 
    Check = 61445, 
    ChevronDown = 61489, 
    ChevronLeft = 61490, 
    ChevronRight = 61491, 
    ChevronUp = 61488, 
    Circle = 61472, 
    Curlicue = 61456, 
    DoubleCircle = 61473, 
    DoubleCurlicue = 61457, 
    DoubleTap = 61681, 
    Down = 61529, 
    DownLeft = 61546, 
    DownLeftLong = 61542, 
    DownRight = 61547, 
    DownRightLong = 61543, 
    DownUp = 61537, 
    Exclamation = 61604, 
    Left = 61530, 
    LeftDown = 61549, 
    LeftRight = 61538, 
    LeftUp = 61548, 
    NoGesture = 61440, 
    Right = 61531, 
    RightDown = 61551, 
    RightLeft = 61539, 
    RightUp = 61550, 
    ScratchOut = 61441, 
    SemicircleLeft = 61480, 
    SemicircleRight = 61481, 
    Square = 61443, 
    Star = 61444, 
    Tap = 61680, 
    Triangle = 61442, 
    Up = 61528, 
    UpDown = 61536, 
    UpLeft = 61544, 
    UpLeftLong = 61540, 
    UpRight = 61545, 
    UpRightLong = 61541, 
  }

  public delegate void DrawingAttributesReplacedEventHandler(Object sender, DrawingAttributesReplacedEventArgs e);

  public delegate void LassoSelectionChangedEventHandler(Object sender, LassoSelectionChangedEventArgs e);

  public delegate void PropertyDataChangedEventHandler(Object sender, PropertyDataChangedEventArgs e);

  public enum RecognitionConfidence
  {
    Strong = 0, 
    Intermediate = 1, 
    Poor = 2, 
  }

  public delegate void StrokeCollectionChangedEventHandler(Object sender, StrokeCollectionChangedEventArgs e);

  public delegate void StrokeHitEventHandler(Object sender, StrokeHitEventArgs e);

  public delegate void StylusPointsReplacedEventHandler(Object sender, StylusPointsReplacedEventArgs e);

  public enum StylusTip
  {
    Rectangle = 0, 
    Ellipse = 1, 
  }
}

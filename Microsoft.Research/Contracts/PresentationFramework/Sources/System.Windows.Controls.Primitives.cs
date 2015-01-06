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

// File System.Windows.Controls.Primitives.cs
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


namespace System.Windows.Controls.Primitives
{
  public enum AutoToolTipPlacement
  {
    None = 0, 
    TopLeft = 1, 
    BottomRight = 2, 
  }

  public delegate CustomPopupPlacement[] CustomPopupPlacementCallback(System.Windows.Size popupSize, System.Windows.Size targetSize, System.Windows.Point offset);

  public delegate void DragCompletedEventHandler(Object sender, DragCompletedEventArgs e);

  public delegate void DragDeltaEventHandler(Object sender, DragDeltaEventArgs e);

  public delegate void DragStartedEventHandler(Object sender, DragStartedEventArgs e);

  public enum GeneratorDirection
  {
    Forward = 0, 
    Backward = 1, 
  }

  public enum GeneratorStatus
  {
    NotStarted = 0, 
    GeneratingContainers = 1, 
    ContainersGenerated = 2, 
    Error = 3, 
  }

  public delegate void ItemsChangedEventHandler(Object sender, ItemsChangedEventArgs e);

  public enum PlacementMode
  {
    Absolute = 0, 
    Relative = 1, 
    Bottom = 2, 
    Center = 3, 
    Right = 4, 
    AbsolutePoint = 5, 
    RelativePoint = 6, 
    Mouse = 7, 
    MousePoint = 8, 
    Left = 9, 
    Top = 10, 
    Custom = 11, 
  }

  public enum PopupAnimation
  {
    None = 0, 
    Fade = 1, 
    Slide = 2, 
    Scroll = 3, 
  }

  public enum PopupPrimaryAxis
  {
    None = 0, 
    Horizontal = 1, 
    Vertical = 2, 
  }

  public delegate void ScrollEventHandler(Object sender, ScrollEventArgs e);

  public enum ScrollEventType
  {
    EndScroll = 0, 
    First = 1, 
    LargeDecrement = 2, 
    LargeIncrement = 3, 
    Last = 4, 
    SmallDecrement = 5, 
    SmallIncrement = 6, 
    ThumbPosition = 7, 
    ThumbTrack = 8, 
  }

  public enum TickBarPlacement
  {
    Left = 0, 
    Top = 1, 
    Right = 2, 
    Bottom = 3, 
  }

  public enum TickPlacement
  {
    None = 0, 
    TopLeft = 1, 
    BottomRight = 2, 
    Both = 3, 
  }
}

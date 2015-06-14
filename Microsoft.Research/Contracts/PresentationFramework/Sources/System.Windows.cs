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

// File System.Windows.cs
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


namespace System.Windows
{
  public enum BaseValueSource
  {
    Unknown = 0, 
    Default = 1, 
    Inherited = 2, 
    DefaultStyle = 3, 
    DefaultStyleTrigger = 4, 
    Style = 5, 
    TemplateTrigger = 6, 
    StyleTrigger = 7, 
    ImplicitStyleReference = 8, 
    ParentTemplate = 9, 
    ParentTemplateTrigger = 10, 
    Local = 11, 
  }

  public enum ColumnSpaceDistribution
  {
    Left = 0, 
    Right = 1, 
    Between = 2, 
  }

  public delegate void ExitEventHandler(Object sender, ExitEventArgs e);

  public enum FigureHorizontalAnchor
  {
    PageLeft = 0, 
    PageCenter = 1, 
    PageRight = 2, 
    ContentLeft = 3, 
    ContentCenter = 4, 
    ContentRight = 5, 
    ColumnLeft = 6, 
    ColumnCenter = 7, 
    ColumnRight = 8, 
  }

  public enum FigureUnitType
  {
    Auto = 0, 
    Pixel = 1, 
    Column = 2, 
    Content = 3, 
    Page = 4, 
  }

  public enum FigureVerticalAnchor
  {
    PageTop = 0, 
    PageCenter = 1, 
    PageBottom = 2, 
    ContentTop = 3, 
    ContentCenter = 4, 
    ContentBottom = 5, 
    ParagraphTop = 6, 
  }

  public enum FrameworkPropertyMetadataOptions
  {
    None = 0, 
    AffectsMeasure = 1, 
    AffectsArrange = 2, 
    AffectsParentMeasure = 4, 
    AffectsParentArrange = 8, 
    AffectsRender = 16, 
    Inherits = 32, 
    OverridesInheritanceBehavior = 64, 
    NotDataBindable = 128, 
    BindsTwoWayByDefault = 256, 
    Journal = 1024, 
    SubPropertiesDoNotAffectRender = 2048, 
  }

  public enum GridUnitType
  {
    Auto = 0, 
    Pixel = 1, 
    Star = 2, 
  }

  public enum HorizontalAlignment
  {
    Left = 0, 
    Center = 1, 
    Right = 2, 
    Stretch = 3, 
  }

  public enum InheritanceBehavior
  {
    Default = 0, 
    SkipToAppNow = 1, 
    SkipToAppNext = 2, 
    SkipToThemeNow = 3, 
    SkipToThemeNext = 4, 
    SkipAllNow = 5, 
    SkipAllNext = 6, 
  }

  public enum LineStackingStrategy
  {
    BlockLineHeight = 0, 
    MaxHeight = 1, 
  }

  public enum MessageBoxButton
  {
    OK = 0, 
    OKCancel = 1, 
    YesNoCancel = 3, 
    YesNo = 4, 
  }

  public enum MessageBoxImage
  {
    None = 0, 
    Hand = 16, 
    Question = 32, 
    Exclamation = 48, 
    Asterisk = 64, 
    Stop = 16, 
    Error = 16, 
    Warning = 48, 
    Information = 64, 
  }

  public enum MessageBoxOptions
  {
    None = 0, 
    ServiceNotification = 2097152, 
    DefaultDesktopOnly = 131072, 
    RightAlign = 524288, 
    RtlReading = 1048576, 
  }

  public enum MessageBoxResult
  {
    None = 0, 
    OK = 1, 
    Cancel = 2, 
    Yes = 6, 
    No = 7, 
  }

  public enum PowerLineStatus
  {
    Offline = 0, 
    Online = 1, 
    Unknown = 255, 
  }

  public enum ReasonSessionEnding : byte
  {
    Logoff = 0, 
    Shutdown = 1, 
  }

  public delegate void RequestBringIntoViewEventHandler(Object sender, RequestBringIntoViewEventArgs e);

  public enum ResizeMode
  {
    NoResize = 0, 
    CanMinimize = 1, 
    CanResize = 2, 
    CanResizeWithGrip = 3, 
  }

  public enum ResourceDictionaryLocation
  {
    None = 0, 
    SourceAssembly = 1, 
    ExternalAssembly = 2, 
  }

  public delegate void RoutedPropertyChangedEventHandler<T>(Object sender, RoutedPropertyChangedEventArgs<T> e);

  public delegate void SessionEndingCancelEventHandler(Object sender, SessionEndingCancelEventArgs e);

  public enum ShutdownMode : byte
  {
    OnLastWindowClose = 0, 
    OnMainWindowClose = 1, 
    OnExplicitShutdown = 2, 
  }

  public delegate void SizeChangedEventHandler(Object sender, SizeChangedEventArgs e);

  public delegate void StartupEventHandler(Object sender, StartupEventArgs e);

  public enum VerticalAlignment
  {
    Top = 0, 
    Center = 1, 
    Bottom = 2, 
    Stretch = 3, 
  }

  public enum WindowStartupLocation
  {
    Manual = 0, 
    CenterScreen = 1, 
    CenterOwner = 2, 
  }

  public enum WindowState
  {
    Normal = 0, 
    Minimized = 1, 
    Maximized = 2, 
  }

  public enum WindowStyle
  {
    None = 0, 
    SingleBorderWindow = 1, 
    ThreeDBorderWindow = 2, 
    ToolWindow = 3, 
  }

  public enum WrapDirection
  {
    None = 0, 
    Left = 1, 
    Right = 2, 
    Both = 3, 
  }
}

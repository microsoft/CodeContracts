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
  public delegate void AutoResizedEventHandler(Object sender, AutoResizedEventArgs e);

  public enum BaselineAlignment
  {
    Top = 0, 
    Center = 1, 
    Bottom = 2, 
    Baseline = 3, 
    TextTop = 4, 
    TextBottom = 5, 
    Subscript = 6, 
    Superscript = 7, 
  }

  public delegate void DataObjectCopyingEventHandler(Object sender, DataObjectCopyingEventArgs e);

  public delegate void DataObjectPastingEventHandler(Object sender, DataObjectPastingEventArgs e);

  public delegate void DataObjectSettingDataEventHandler(Object sender, DataObjectSettingDataEventArgs e);

  public enum DragAction
  {
    Continue = 0, 
    Drop = 1, 
    Cancel = 2, 
  }

  public enum DragDropEffects
  {
    None = 0, 
    Copy = 1, 
    Move = 2, 
    Link = 4, 
    Scroll = -2147483648, 
    All = -2147483645, 
  }

  public enum DragDropKeyStates
  {
    None = 0, 
    LeftMouseButton = 1, 
    RightMouseButton = 2, 
    ShiftKey = 4, 
    ControlKey = 8, 
    MiddleMouseButton = 16, 
    AltKey = 32, 
  }

  public delegate void DragEventHandler(Object sender, DragEventArgs e);

  public enum FlowDirection
  {
    LeftToRight = 0, 
    RightToLeft = 1, 
  }

  public enum FontCapitals
  {
    Normal = 0, 
    AllSmallCaps = 1, 
    SmallCaps = 2, 
    AllPetiteCaps = 3, 
    PetiteCaps = 4, 
    Unicase = 5, 
    Titling = 6, 
  }

  public enum FontEastAsianLanguage
  {
    Normal = 0, 
    Jis78 = 1, 
    Jis83 = 2, 
    Jis90 = 3, 
    Jis04 = 4, 
    HojoKanji = 5, 
    NlcKanji = 6, 
    Simplified = 7, 
    Traditional = 8, 
    TraditionalNames = 9, 
  }

  public enum FontEastAsianWidths
  {
    Normal = 0, 
    Proportional = 1, 
    Full = 2, 
    Half = 3, 
    Third = 4, 
    Quarter = 5, 
  }

  public enum FontFraction
  {
    Normal = 0, 
    Slashed = 1, 
    Stacked = 2, 
  }

  public enum FontNumeralAlignment
  {
    Normal = 0, 
    Proportional = 1, 
    Tabular = 2, 
  }

  public enum FontNumeralStyle
  {
    Normal = 0, 
    Lining = 1, 
    OldStyle = 2, 
  }

  public enum FontVariants
  {
    Normal = 0, 
    Superscript = 1, 
    Subscript = 2, 
    Ordinal = 3, 
    Inferior = 4, 
    Ruby = 5, 
  }

  public delegate void GiveFeedbackEventHandler(Object sender, GiveFeedbackEventArgs e);

  public enum LineBreakCondition
  {
    BreakDesired = 0, 
    BreakPossible = 1, 
    BreakRestrained = 2, 
    BreakAlways = 3, 
  }

  public enum LocalizationCategory
  {
    None = 0, 
    Text = 1, 
    Title = 2, 
    Label = 3, 
    Button = 4, 
    CheckBox = 5, 
    ComboBox = 6, 
    ListBox = 7, 
    Menu = 8, 
    RadioButton = 9, 
    ToolTip = 10, 
    Hyperlink = 11, 
    TextFlow = 12, 
    XmlData = 13, 
    Font = 14, 
    Inherit = 15, 
    Ignore = 16, 
    NeverLocalize = 17, 
  }

  public enum Modifiability
  {
    Unmodifiable = 0, 
    Modifiable = 1, 
    Inherit = 2, 
  }

  public delegate void QueryContinueDragEventHandler(Object sender, QueryContinueDragEventArgs e);

  public enum Readability
  {
    Unreadable = 0, 
    Readable = 1, 
    Inherit = 2, 
  }

  public delegate void RoutedEventHandler(Object sender, RoutedEventArgs e);

  public enum RoutingStrategy
  {
    Tunnel = 0, 
    Bubble = 1, 
    Direct = 2, 
  }

  public enum SizeToContent
  {
    Manual = 0, 
    Width = 1, 
    Height = 2, 
    WidthAndHeight = 3, 
  }

  public delegate void SourceChangedEventHandler(Object sender, SourceChangedEventArgs e);

  public enum TextAlignment
  {
    Left = 0, 
    Right = 1, 
    Center = 2, 
    Justify = 3, 
  }

  public enum TextDataFormat
  {
    Text = 0, 
    UnicodeText = 1, 
    Rtf = 2, 
    Html = 3, 
    CommaSeparatedValue = 4, 
    Xaml = 5, 
  }

  public enum TextDecorationLocation
  {
    Underline = 0, 
    OverLine = 1, 
    Strikethrough = 2, 
    Baseline = 3, 
  }

  public enum TextDecorationUnit
  {
    FontRecommended = 0, 
    FontRenderingEmSize = 1, 
    Pixel = 2, 
  }

  public enum TextMarkerStyle
  {
    None = 0, 
    Disc = 1, 
    Circle = 2, 
    Square = 3, 
    Box = 4, 
    LowerRoman = 5, 
    UpperRoman = 6, 
    LowerLatin = 7, 
    UpperLatin = 8, 
    Decimal = 9, 
  }

  public enum TextTrimming
  {
    None = 0, 
    CharacterEllipsis = 1, 
    WordEllipsis = 2, 
  }

  public enum TextWrapping
  {
    WrapWithOverflow = 0, 
    NoWrap = 1, 
    Wrap = 2, 
  }

  public enum Visibility : byte
  {
    Visible = 0, 
    Hidden = 1, 
    Collapsed = 2, 
  }
}

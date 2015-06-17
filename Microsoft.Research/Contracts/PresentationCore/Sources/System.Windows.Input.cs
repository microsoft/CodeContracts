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

// File System.Windows.Input.cs
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


namespace System.Windows.Input
{
  public delegate void AccessKeyPressedEventHandler(Object sender, AccessKeyPressedEventArgs e);

  public delegate void CanExecuteRoutedEventHandler(Object sender, CanExecuteRoutedEventArgs e);

  public enum CaptureMode
  {
    None = 0, 
    Element = 1, 
    SubTree = 2, 
  }

  public enum CursorType
  {
    None = 0, 
    No = 1, 
    Arrow = 2, 
    AppStarting = 3, 
    Cross = 4, 
    Help = 5, 
    IBeam = 6, 
    SizeAll = 7, 
    SizeNESW = 8, 
    SizeNS = 9, 
    SizeNWSE = 10, 
    SizeWE = 11, 
    UpArrow = 12, 
    Wait = 13, 
    Hand = 14, 
    Pen = 15, 
    ScrollNS = 16, 
    ScrollWE = 17, 
    ScrollAll = 18, 
    ScrollN = 19, 
    ScrollS = 20, 
    ScrollW = 21, 
    ScrollE = 22, 
    ScrollNW = 23, 
    ScrollNE = 24, 
    ScrollSW = 25, 
    ScrollSE = 26, 
    ArrowCD = 27, 
  }

  public delegate void ExecutedRoutedEventHandler(Object sender, ExecutedRoutedEventArgs e);

  public enum ImeConversionModeValues
  {
    Native = 1, 
    Katakana = 2, 
    FullShape = 4, 
    Roman = 8, 
    CharCode = 16, 
    NoConversion = 32, 
    Eudc = 64, 
    Symbol = 128, 
    Fixed = 256, 
    Alphanumeric = 512, 
    DoNotCare = -2147483648, 
  }

  public enum ImeSentenceModeValues
  {
    None = 0, 
    PluralClause = 1, 
    SingleConversion = 2, 
    Automatic = 4, 
    PhrasePrediction = 8, 
    Conversation = 16, 
    DoNotCare = -2147483648, 
  }

  public delegate void InputEventHandler(Object sender, InputEventArgs e);

  public delegate void InputLanguageEventHandler(Object sender, InputLanguageEventArgs e);

  public enum InputMethodState
  {
    Off = 0, 
    On = 1, 
    DoNotCare = 2, 
  }

  public delegate void InputMethodStateChangedEventHandler(Object sender, InputMethodStateChangedEventArgs e);

  public enum InputMode
  {
    Foreground = 0, 
    Sink = 1, 
  }

  public enum InputScopeNameValue
  {
    Default = 0, 
    Url = 1, 
    FullFilePath = 2, 
    FileName = 3, 
    EmailUserName = 4, 
    EmailSmtpAddress = 5, 
    LogOnName = 6, 
    PersonalFullName = 7, 
    PersonalNamePrefix = 8, 
    PersonalGivenName = 9, 
    PersonalMiddleName = 10, 
    PersonalSurname = 11, 
    PersonalNameSuffix = 12, 
    PostalAddress = 13, 
    PostalCode = 14, 
    AddressStreet = 15, 
    AddressStateOrProvince = 16, 
    AddressCity = 17, 
    AddressCountryName = 18, 
    AddressCountryShortName = 19, 
    CurrencyAmountAndSymbol = 20, 
    CurrencyAmount = 21, 
    Date = 22, 
    DateMonth = 23, 
    DateDay = 24, 
    DateYear = 25, 
    DateMonthName = 26, 
    DateDayName = 27, 
    Digits = 28, 
    Number = 29, 
    OneChar = 30, 
    Password = 31, 
    TelephoneNumber = 32, 
    TelephoneCountryCode = 33, 
    TelephoneAreaCode = 34, 
    TelephoneLocalNumber = 35, 
    Time = 36, 
    TimeHour = 37, 
    TimeMinorSec = 38, 
    NumberFullWidth = 39, 
    AlphanumericHalfWidth = 40, 
    AlphanumericFullWidth = 41, 
    CurrencyChinese = 42, 
    Bopomofo = 43, 
    Hiragana = 44, 
    KatakanaHalfWidth = 45, 
    KatakanaFullWidth = 46, 
    Hanja = 47, 
    PhraseList = -1, 
    RegularExpression = -2, 
    Srgs = -3, 
    Xml = -4, 
  }

  public enum InputType
  {
    Keyboard = 0, 
    Mouse = 1, 
    Stylus = 2, 
    Hid = 3, 
    Text = 4, 
    Command = 5, 
  }

  public delegate void KeyboardEventHandler(Object sender, KeyboardEventArgs e);

  public delegate void KeyboardFocusChangedEventHandler(Object sender, KeyboardFocusChangedEventArgs e);

  public delegate void KeyboardInputProviderAcquireFocusEventHandler(Object sender, KeyboardInputProviderAcquireFocusEventArgs e);

  public delegate void KeyEventHandler(Object sender, KeyEventArgs e);

  public enum KeyStates : byte
  {
    None = 0, 
    Down = 1, 
    Toggled = 2, 
  }

  public enum ManipulationModes
  {
    None = 0, 
    TranslateX = 1, 
    TranslateY = 2, 
    Translate = 3, 
    Rotate = 4, 
    Scale = 8, 
    All = 15, 
  }

  public enum MouseAction : byte
  {
    None = 0, 
    LeftClick = 1, 
    RightClick = 2, 
    MiddleClick = 3, 
    WheelClick = 4, 
    LeftDoubleClick = 5, 
    RightDoubleClick = 6, 
    MiddleDoubleClick = 7, 
  }

  public enum MouseButton
  {
    Left = 0, 
    Middle = 1, 
    Right = 2, 
    XButton1 = 3, 
    XButton2 = 4, 
  }

  public delegate void MouseButtonEventHandler(Object sender, MouseButtonEventArgs e);

  public enum MouseButtonState
  {
    Released = 0, 
    Pressed = 1, 
  }

  public delegate void MouseEventHandler(Object sender, MouseEventArgs e);

  public delegate void MouseWheelEventHandler(Object sender, MouseWheelEventArgs e);

  public delegate void NotifyInputEventHandler(Object sender, NotifyInputEventArgs e);

  public delegate void PreProcessInputEventHandler(Object sender, PreProcessInputEventArgs e);

  public delegate void ProcessInputEventHandler(Object sender, ProcessInputEventArgs e);

  public delegate void QueryCursorEventHandler(Object sender, QueryCursorEventArgs e);

  public enum RestoreFocusMode
  {
    Auto = 0, 
    None = 1, 
  }

  public enum SpeechMode
  {
    Dictation = 0, 
    Command = 1, 
    Indeterminate = 2, 
  }

  public delegate void StylusButtonEventHandler(Object sender, StylusButtonEventArgs e);

  public enum StylusButtonState
  {
    Up = 0, 
    Down = 1, 
  }

  public delegate void StylusDownEventHandler(Object sender, StylusDownEventArgs e);

  public delegate void StylusEventHandler(Object sender, StylusEventArgs e);

  public enum StylusPointPropertyUnit
  {
    None = 0, 
    Inches = 1, 
    Centimeters = 2, 
    Degrees = 3, 
    Radians = 4, 
    Seconds = 5, 
    Pounds = 6, 
    Grams = 7, 
  }

  public delegate void StylusSystemGestureEventHandler(Object sender, StylusSystemGestureEventArgs e);

  public enum SystemGesture
  {
    None = 0, 
    Tap = 16, 
    RightTap = 18, 
    Drag = 19, 
    RightDrag = 20, 
    HoldEnter = 21, 
    HoldLeave = 22, 
    HoverEnter = 23, 
    HoverLeave = 24, 
    Flick = 31, 
    TwoFingerTap = 4352, 
  }

  public enum TabletDeviceType
  {
    Stylus = 0, 
    Touch = 1, 
  }

  public enum TabletHardwareCapabilities
  {
    None = 0, 
    Integrated = 1, 
    StylusMustTouch = 2, 
    HardProximity = 4, 
    StylusHasPhysicalIds = 8, 
    SupportsPressure = 1073741824, 
  }

  public enum TextCompositionAutoComplete
  {
    Off = 0, 
    On = 1, 
  }

  public delegate void TextCompositionEventHandler(Object sender, TextCompositionEventArgs e);

  public enum TouchAction
  {
    Down = 0, 
    Move = 1, 
    Up = 2, 
  }

  public delegate void TouchFrameEventHandler(Object sender, TouchFrameEventArgs e);
}

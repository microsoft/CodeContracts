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

// File System.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System
{
#if NETFRAMEWORK_4_0 || SILVERLIGHT_5_0
  public delegate void Action<T1, T2, T3, T4, T5, T6> (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

  public delegate void Action<T1, T2, T3, T4, T5> (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

  public delegate void Action<T1, T2, T3, T4, T5, T6, T7, T8> (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);

  public delegate void Action<T1, T2, T3, T4, T5, T6, T7> (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);

  public delegate void Action<T1, T2, T3> (T1 arg1, T2 arg2, T3 arg3);

  public delegate void Action<T1, T2, T3, T4> (T1 arg1, T2 arg2, T3 arg3, T4 arg4);

  public delegate void Action<T1, T2> (T1 arg1, T2 arg2);

  public delegate void Action ();
#endif

#if !SILVERLIGHT_4_0_WP
  public delegate void AppDomainInitializer (string[] args);
#endif

#if !SILVERLIGHT
  public enum AppDomainManagerInitializationOptions
  {
    None = 0, 
    RegisterWithHost = 1, 
  }

  public enum ConsoleColor
  {
    Black = 0, 
    DarkBlue = 1, 
    DarkGreen = 2, 
    DarkCyan = 3, 
    DarkRed = 4, 
    DarkMagenta = 5, 
    DarkYellow = 6, 
    Gray = 7, 
    DarkGray = 8, 
    Blue = 9, 
    Green = 10, 
    Cyan = 11, 
    Red = 12, 
    Magenta = 13, 
    Yellow = 14, 
    White = 15,
  }

  public enum ConsoleKey
  {
    Backspace = 8, 
    Tab = 9, 
    Clear = 12, 
    Enter = 13, 
    Pause = 19, 
    Escape = 27, 
    Spacebar = 32, 
    PageUp = 33, 
    PageDown = 34, 
    End = 35, 
    Home = 36, 
    LeftArrow = 37, 
    UpArrow = 38, 
    RightArrow = 39, 
    DownArrow = 40, 
    Select = 41, 
    Print = 42, 
    Execute = 43, 
    PrintScreen = 44, 
    Insert = 45, 
    Delete = 46, 
    Help = 47, 
    D0 = 48, 
    D1 = 49, 
    D2 = 50, 
    D3 = 51, 
    D4 = 52, 
    D5 = 53, 
    D6 = 54, 
    D7 = 55, 
    D8 = 56, 
    D9 = 57, 
    A = 65, 
    B = 66, 
    C = 67, 
    D = 68, 
    E = 69, 
    F = 70, 
    G = 71, 
    H = 72, 
    I = 73, 
    J = 74, 
    K = 75, 
    L = 76, 
    M = 77, 
    N = 78, 
    O = 79, 
    P = 80, 
    Q = 81, 
    R = 82, 
    S = 83, 
    T = 84, 
    U = 85, 
    V = 86, 
    W = 87, 
    X = 88, 
    Y = 89, 
    Z = 90, 
    LeftWindows = 91, 
    RightWindows = 92, 
    Applications = 93, 
    Sleep = 95, 
    NumPad0 = 96, 
    NumPad1 = 97, 
    NumPad2 = 98, 
    NumPad3 = 99, 
    NumPad4 = 100, 
    NumPad5 = 101, 
    NumPad6 = 102, 
    NumPad7 = 103, 
    NumPad8 = 104, 
    NumPad9 = 105, 
    Multiply = 106, 
    Add = 107, 
    Separator = 108, 
    Subtract = 109, 
    Decimal = 110, 
    Divide = 111, 
    F1 = 112, 
    F2 = 113, 
    F3 = 114, 
    F4 = 115, 
    F5 = 116, 
    F6 = 117, 
    F7 = 118, 
    F8 = 119, 
    F9 = 120, 
    F10 = 121, 
    F11 = 122, 
    F12 = 123, 
    F13 = 124, 
    F14 = 125, 
    F15 = 126, 
    F16 = 127, 
    F17 = 128, 
    F18 = 129, 
    F19 = 130, 
    F20 = 131, 
    F21 = 132, 
    F22 = 133, 
    F23 = 134, 
    F24 = 135, 
    BrowserBack = 166, 
    BrowserForward = 167, 
    BrowserRefresh = 168, 
    BrowserStop = 169, 
    BrowserSearch = 170, 
    BrowserFavorites = 171, 
    BrowserHome = 172, 
    VolumeMute = 173, 
    VolumeDown = 174, 
    VolumeUp = 175, 
    MediaNext = 176, 
    MediaPrevious = 177, 
    MediaStop = 178, 
    MediaPlay = 179, 
    LaunchMail = 180, 
    LaunchMediaSelect = 181, 
    LaunchApp1 = 182, 
    LaunchApp2 = 183, 
    Oem1 = 186, 
    OemPlus = 187, 
    OemComma = 188, 
    OemMinus = 189, 
    OemPeriod = 190, 
    Oem2 = 191, 
    Oem3 = 192, 
    Oem4 = 219, 
    Oem5 = 220, 
    Oem6 = 221, 
    Oem7 = 222, 
    Oem8 = 223, 
    Oem102 = 226, 
    Process = 229, 
    Packet = 231, 
    Attention = 246, 
    CrSel = 247, 
    ExSel = 248, 
    EraseEndOfFile = 249, 
    Play = 250, 
    Zoom = 251, 
    NoName = 252, 
    Pa1 = 253, 
    OemClear = 254, 
  }

  public enum ConsoleModifiers
  {
    Alt = 1, 
    Shift = 2, 
    Control = 4, 
  }

  public enum ConsoleSpecialKey
  {
    ControlC = 0, 
    ControlBreak = 1,
  }


  public delegate void CrossAppDomainDelegate ();


  public enum EnvironmentVariableTarget
  {
    Process = 0, 
    User = 1, 
    Machine = 2,
  }

#endif

#if NETFRAMEWORK_4_0
  public delegate TResult Func<T1, T2, T3, T4, T5, TResult> (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

  public delegate TResult Func<T1, T2, T3, TResult> (T1 arg1, T2 arg2, T3 arg3);

  public delegate TResult Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);

  public delegate TResult Func<T1, T2, T3, T4, TResult> (T1 arg1, T2 arg2, T3 arg3, T4 arg4);

  public delegate TResult Func<T1, T2, T3, T4, T5, T6, TResult> (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

  public delegate TResult Func<T1, T2, T3, T4, T5, T6, T7, TResult> (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);

  public delegate TResult Func<TResult> ();

  public delegate TResult Func<T1, T2, TResult> (T1 arg1, T2 arg2);

  public delegate TResult Func<T, TResult> (T arg);
#endif

#if !SILVERLIGHT_4_0_WP
  public enum GCCollectionMode
  {
    Default = 0, 
    Forced = 1, 
    Optimized = 2, 
  }
#endif

#if !SILVERLIGHT
  public enum GCNotificationStatus
  {
    Succeeded = 0, 
    Failed = 1, 
    Canceled = 2, 
    Timeout = 3, 
    NotApplicable = 4, 
  }
#endif

  public enum LoaderOptimization
  {
    NotSpecified = 0, 
    SingleDomain = 1, 
    MultiDomain = 2, 
    MultiDomainHost = 3, 
#if !SILVERLIGHT
    DomainMask = 3, 
    DisallowBindings = 4,
#endif

  }

#if !SILVERLIGHT_4_0_WP
  public enum MidpointRounding
  {
    ToEven = 0, 
    AwayFromZero = 1, 
  }
#endif

  public enum PlatformID
  {
    Win32S = 0, 
    Win32Windows = 1, 
    Win32NT = 2, 
    WinCE = 3, 
    Unix = 4, 
    Xbox = 5, 
#if !SILVERLIGHT_4_0_WP
    MacOSX = 6, 
#endif
  }


  public enum StringSplitOptions
  {
    None = 0, 
    RemoveEmptyEntries = 1, 
  }




}

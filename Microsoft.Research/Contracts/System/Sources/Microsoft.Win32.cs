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

// File Microsoft.Win32.cs
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


namespace Microsoft.Win32
{
  public delegate void PowerModeChangedEventHandler(Object sender, PowerModeChangedEventArgs e);

  public enum PowerModes
  {
    Resume = 1, 
    StatusChange = 2, 
    Suspend = 3, 
  }

  public delegate void SessionEndedEventHandler(Object sender, SessionEndedEventArgs e);

  public delegate void SessionEndingEventHandler(Object sender, SessionEndingEventArgs e);

  public enum SessionEndReasons
  {
    Logoff = 1, 
    SystemShutdown = 2, 
  }

  public delegate void SessionSwitchEventHandler(Object sender, SessionSwitchEventArgs e);

  public enum SessionSwitchReason
  {
    ConsoleConnect = 1, 
    ConsoleDisconnect = 2, 
    RemoteConnect = 3, 
    RemoteDisconnect = 4, 
    SessionLogon = 5, 
    SessionLogoff = 6, 
    SessionLock = 7, 
    SessionUnlock = 8, 
    SessionRemoteControl = 9, 
  }

  public delegate void TimerElapsedEventHandler(Object sender, TimerElapsedEventArgs e);

  public enum UserPreferenceCategory
  {
    Accessibility = 1, 
    Color = 2, 
    Desktop = 3, 
    General = 4, 
    Icon = 5, 
    Keyboard = 6, 
    Menu = 7, 
    Mouse = 8, 
    Policy = 9, 
    Power = 10, 
    Screensaver = 11, 
    Window = 12, 
    Locale = 13, 
    VisualStyle = 14, 
  }

  public delegate void UserPreferenceChangedEventHandler(Object sender, UserPreferenceChangedEventArgs e);

  public delegate void UserPreferenceChangingEventHandler(Object sender, UserPreferenceChangingEventArgs e);
}

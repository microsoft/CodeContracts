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

// File System.ServiceProcess.cs
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


namespace System.ServiceProcess
{
  public enum PowerBroadcastStatus
  {
    BatteryLow = 9, 
    OemEvent = 11, 
    PowerStatusChange = 10, 
    QuerySuspend = 0, 
    QuerySuspendFailed = 2, 
    ResumeAutomatic = 18, 
    ResumeCritical = 6, 
    ResumeSuspend = 7, 
    Suspend = 4, 
  }

  public enum ServiceAccount
  {
    LocalService = 0, 
    NetworkService = 1, 
    LocalSystem = 2, 
    User = 3, 
  }

  public enum ServiceControllerPermissionAccess
  {
    None = 0, 
    Browse = 2, 
    Control = 6, 
  }

  public enum ServiceControllerStatus
  {
    ContinuePending = 5, 
    Paused = 7, 
    PausePending = 6, 
    Running = 4, 
    StartPending = 2, 
    Stopped = 1, 
    StopPending = 3, 
  }

  public enum ServiceStartMode
  {
    Manual = 3, 
    Automatic = 2, 
    Disabled = 4, 
  }

  public enum ServiceType
  {
    Adapter = 4, 
    FileSystemDriver = 2, 
    InteractiveProcess = 256, 
    KernelDriver = 1, 
    RecognizerDriver = 8, 
    Win32OwnProcess = 16, 
    Win32ShareProcess = 32, 
  }

  public enum SessionChangeReason
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
}

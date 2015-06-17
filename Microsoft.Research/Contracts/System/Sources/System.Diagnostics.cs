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

// File System.Diagnostics.cs
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


namespace System.Diagnostics
{
  public delegate void DataReceivedEventHandler(Object sender, DataReceivedEventArgs e);

  public delegate void EntryWrittenEventHandler(Object sender, EntryWrittenEventArgs e);

  public enum EventLogEntryType
  {
    Error = 1, 
    Warning = 2, 
    Information = 4, 
    SuccessAudit = 8, 
    FailureAudit = 16, 
  }

  public enum EventLogPermissionAccess
  {
    None = 0, 
    Write = 16, 
    Administer = 48, 
    Browse = 2, 
    Instrument = 6, 
    Audit = 10, 
  }

  public enum OverflowAction
  {
    DoNotOverwrite = -1, 
    OverwriteAsNeeded = 0, 
    OverwriteOlder = 1, 
  }

  public enum PerformanceCounterCategoryType
  {
    Unknown = -1, 
    SingleInstance = 0, 
    MultiInstance = 1, 
  }

  public enum PerformanceCounterInstanceLifetime
  {
    Global = 0, 
    Process = 1, 
  }

  public enum PerformanceCounterPermissionAccess
  {
    Browse = 1, 
    Instrument = 3, 
    None = 0, 
    Read = 1, 
    Write = 2, 
    Administer = 7, 
  }

  public enum PerformanceCounterType
  {
    NumberOfItems32 = 65536, 
    NumberOfItems64 = 65792, 
    NumberOfItemsHEX32 = 0, 
    NumberOfItemsHEX64 = 256, 
    RateOfCountsPerSecond32 = 272696320, 
    RateOfCountsPerSecond64 = 272696576, 
    CountPerTimeInterval32 = 4523008, 
    CountPerTimeInterval64 = 4523264, 
    RawFraction = 537003008, 
    RawBase = 1073939459, 
    AverageTimer32 = 805438464, 
    AverageBase = 1073939458, 
    AverageCount64 = 1073874176, 
    SampleFraction = 549585920, 
    SampleCounter = 4260864, 
    SampleBase = 1073939457, 
    CounterTimer = 541132032, 
    CounterTimerInverse = 557909248, 
    Timer100Ns = 542180608, 
    Timer100NsInverse = 558957824, 
    ElapsedTime = 807666944, 
    CounterMultiTimer = 574686464, 
    CounterMultiTimerInverse = 591463680, 
    CounterMultiTimer100Ns = 575735040, 
    CounterMultiTimer100NsInverse = 592512256, 
    CounterMultiBase = 1107494144, 
    CounterDelta32 = 4195328, 
    CounterDelta64 = 4195584, 
  }

  public enum ProcessPriorityClass
  {
    Normal = 32, 
    Idle = 64, 
    High = 128, 
    RealTime = 256, 
    BelowNormal = 16384, 
    AboveNormal = 32768, 
  }

  public enum ProcessWindowStyle
  {
    Normal = 0, 
    Hidden = 1, 
    Minimized = 2, 
    Maximized = 3, 
  }

  public enum SourceLevels
  {
    Off = 0, 
    Critical = 1, 
    Error = 3, 
    Warning = 7, 
    Information = 15, 
    Verbose = 31, 
    ActivityTracing = 65280, 
    All = -1, 
  }

  public enum ThreadPriorityLevel
  {
    Idle = -15, 
    Lowest = -2, 
    BelowNormal = -1, 
    Normal = 0, 
    AboveNormal = 1, 
    Highest = 2, 
    TimeCritical = 15, 
  }

  public enum ThreadState
  {
    Initialized = 0, 
    Ready = 1, 
    Running = 2, 
    Standby = 3, 
    Terminated = 4, 
    Wait = 5, 
    Transition = 6, 
    Unknown = 7, 
  }

  public enum ThreadWaitReason
  {
    Executive = 0, 
    FreePage = 1, 
    PageIn = 2, 
    SystemAllocation = 3, 
    ExecutionDelay = 4, 
    Suspended = 5, 
    UserRequest = 6, 
    EventPairHigh = 7, 
    EventPairLow = 8, 
    LpcReceive = 9, 
    LpcReply = 10, 
    VirtualMemory = 11, 
    PageOut = 12, 
    Unknown = 13, 
  }

  public enum TraceEventType
  {
    Critical = 1, 
    Error = 2, 
    Warning = 4, 
    Information = 8, 
    Verbose = 16, 
    Start = 256, 
    Stop = 512, 
    Suspend = 1024, 
    Resume = 2048, 
    Transfer = 4096, 
  }

  public enum TraceLevel
  {
    Off = 0, 
    Error = 1, 
    Warning = 2, 
    Info = 3, 
    Verbose = 4, 
  }

  public enum TraceOptions
  {
    None = 0, 
    LogicalOperationStack = 1, 
    DateTime = 2, 
    Timestamp = 4, 
    ProcessId = 8, 
    ThreadId = 16, 
    Callstack = 32, 
  }
}

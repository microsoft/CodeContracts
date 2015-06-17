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

// File System.Diagnostics.PerformanceData.cs
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


namespace System.Diagnostics.PerformanceData
{
  public enum CounterSetInstanceType
  {
    Single = 0, 
    Multiple = 2, 
    GlobalAggregate = 4, 
    GlobalAggregateWithHistory = 11, 
    MultipleAggregate = 6, 
    InstanceAggregate = 22, 
  }

  public enum CounterType
  {
    QueueLength = 4523008, 
    LargeQueueLength = 4523264, 
    QueueLength100Ns = 5571840, 
    QueueLengthObjectTime = 6620416, 
    RawData32 = 65536, 
    RawData64 = 65792, 
    RawDataHex32 = 0, 
    RawDataHex64 = 256, 
    RateOfCountPerSecond32 = 272696320, 
    RateOfCountPerSecond64 = 272696576, 
    RawFraction32 = 537003008, 
    RawFraction64 = 537003264, 
    RawBase32 = 1073939459, 
    RawBase64 = 1073939712, 
    SampleFraction = 549585920, 
    SampleCounter = 4260864, 
    SampleBase = 1073939457, 
    AverageTimer32 = 805438464, 
    AverageBase = 1073939458, 
    AverageCount64 = 1073874176, 
    PercentageActive = 541132032, 
    PercentageNotActive = 557909248, 
    PercentageActive100Ns = 542180608, 
    PercentageNotActive100Ns = 558957824, 
    ElapsedTime = 807666944, 
    MultiTimerPercentageActive = 574686464, 
    MultiTimerPercentageNotActive = 591463680, 
    MultiTimerPercentageActive100Ns = 575735040, 
    MultiTimerPercentageNotActive100Ns = 592512256, 
    MultiTimerBase = 1107494144, 
    Delta32 = 4195328, 
    Delta64 = 4195584, 
    ObjectSpecificTimer = 543229184, 
    PrecisionSystemTimer = 541525248, 
    PrecisionTimer100Ns = 542573824, 
    PrecisionObjectSpecificTimer = 543622400, 
  }
}

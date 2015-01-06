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

// File System.Diagnostics.CounterSample.cs
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
  public partial struct CounterSample
  {
    #region Methods and constructors
    public static bool operator != (System.Diagnostics.CounterSample a, System.Diagnostics.CounterSample b)
    {
      return default(bool);
    }

    public static bool operator == (System.Diagnostics.CounterSample a, System.Diagnostics.CounterSample b)
    {
      return default(bool);
    }

    public static float Calculate(System.Diagnostics.CounterSample counterSample, System.Diagnostics.CounterSample nextCounterSample)
    {
      return default(float);
    }

    public static float Calculate(System.Diagnostics.CounterSample counterSample)
    {
      return default(float);
    }

    public CounterSample(long rawValue, long baseValue, long counterFrequency, long systemFrequency, long timeStamp, long timeStamp100nSec, PerformanceCounterType counterType)
    {
      CounterSample.Empty = default(CounterSample);
    }

    public CounterSample(long rawValue, long baseValue, long counterFrequency, long systemFrequency, long timeStamp, long timeStamp100nSec, PerformanceCounterType counterType, long counterTimeStamp)
    {
      CounterSample.Empty = default(System.Diagnostics.CounterSample);
    }

    public bool Equals(System.Diagnostics.CounterSample sample)
    {
      return default(bool);
    }

    public override bool Equals(Object o)
    {
      return default(bool);
    }

    public override int GetHashCode()
    {
      return default(int);
    }
    #endregion

    #region Properties and indexers
    public long BaseValue
    {
      get
      {
        return default(long);
      }
    }

    public long CounterFrequency
    {
      get
      {
        return default(long);
      }
    }

    public long CounterTimeStamp
    {
      get
      {
        return default(long);
      }
    }

    public PerformanceCounterType CounterType
    {
      get
      {
        return default(PerformanceCounterType);
      }
    }

    public long RawValue
    {
      get
      {
        return default(long);
      }
    }

    public long SystemFrequency
    {
      get
      {
        return default(long);
      }
    }

    public long TimeStamp
    {
      get
      {
        return default(long);
      }
    }

    public long TimeStamp100nSec
    {
      get
      {
        return default(long);
      }
    }
    #endregion

    #region Fields
    public static System.Diagnostics.CounterSample Empty;
    #endregion
  }
}

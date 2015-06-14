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

// File System.Threading.Interlocked.cs
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


namespace System.Threading
{
  static public partial class Interlocked
  {
    #region Methods and constructors
    public static long Add(ref long location1, long value)
    {
      return default(long);
    }

    public static int Add(ref int location1, int value)
    {
      return default(int);
    }

    public static float CompareExchange(ref float location1, float value, float comparand)
    {
      return default(float);
    }

    public static double CompareExchange(ref double location1, double value, double comparand)
    {
      return default(double);
    }

    public static int CompareExchange(ref int location1, int value, int comparand)
    {
      return default(int);
    }

    public static long CompareExchange(ref long location1, long value, long comparand)
    {
      return default(long);
    }

    public static Object CompareExchange(ref Object location1, Object value, Object comparand)
    {
      return default(Object);
    }

    public static T CompareExchange<T>(ref T location1, T value, T comparand)
    {
      Contract.Ensures(Contract.Result<T>() == value);

      return default(T);
    }

    public static IntPtr CompareExchange(ref IntPtr location1, IntPtr value, IntPtr comparand)
    {
      return default(IntPtr);
    }

    public static int Decrement(ref int location)
    {
      return default(int);
    }

    public static long Decrement(ref long location)
    {
      return default(long);
    }

    public static T Exchange<T>(ref T location1, T value)
    {
      Contract.Ensures(Contract.Result<T>() == value);

      return default(T);
    }

    public static float Exchange(ref float location1, float value)
    {
      return default(float);
    }

    public static long Exchange(ref long location1, long value)
    {
      return default(long);
    }

    public static int Exchange(ref int location1, int value)
    {
      return default(int);
    }

    public static IntPtr Exchange(ref IntPtr location1, IntPtr value)
    {
      return default(IntPtr);
    }

    public static Object Exchange(ref Object location1, Object value)
    {
      return default(Object);
    }

    public static double Exchange(ref double location1, double value)
    {
      return default(double);
    }

    public static long Increment(ref long location)
    {
      return default(long);
    }

    public static int Increment(ref int location)
    {
      return default(int);
    }

    public static long Read(ref long location)
    {
      return default(long);
    }
    #endregion
  }
}

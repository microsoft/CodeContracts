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

// File System.Threading.Tasks.Parallel.cs
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


namespace System.Threading.Tasks
{
  static public partial class Parallel
  {
    #region Methods and constructors
    public static ParallelLoopResult For(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long, ParallelLoopState> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult For(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int, ParallelLoopState> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult For(long fromInclusive, long toExclusive, Action<long, ParallelLoopState> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult For<TLocal>(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<long, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult For<TLocal>(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<int, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult For<TLocal>(long fromInclusive, long toExclusive, Func<TLocal> localInit, Func<long, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult For(int fromInclusive, int toExclusive, Action<int, ParallelLoopState> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult For(int fromInclusive, int toExclusive, Action<int> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult For<TLocal>(int fromInclusive, int toExclusive, Func<TLocal> localInit, Func<int, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult For(long fromInclusive, long toExclusive, Action<long> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult For(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult For(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource>(System.Collections.Concurrent.OrderablePartitioner<TSource> source, Action<TSource, ParallelLoopState, long> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource, TLocal>(System.Collections.Concurrent.Partitioner<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource>(System.Collections.Concurrent.Partitioner<TSource> source, Action<TSource, ParallelLoopState> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource>(System.Collections.Concurrent.Partitioner<TSource> source, Action<TSource> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource, TLocal>(System.Collections.Concurrent.Partitioner<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource>(System.Collections.Concurrent.OrderablePartitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState, long> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource>(System.Collections.Concurrent.Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource, TLocal>(System.Collections.Concurrent.OrderablePartitioner<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource>(System.Collections.Concurrent.Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource, TLocal>(System.Collections.Concurrent.OrderablePartitioner<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource, ParallelLoopState> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource, ParallelLoopState, long> body)
    {
      return default(ParallelLoopResult);
    }

    public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState, long> body)
    {
      return default(ParallelLoopResult);
    }

    public static void Invoke(Action[] actions)
    {
    }

    public static void Invoke(ParallelOptions parallelOptions, Action[] actions)
    {
    }
    #endregion
  }
}

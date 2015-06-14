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

// File System.Linq.ParallelEnumerable.cs
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


namespace System.Linq
{
  static public partial class ParallelEnumerable
  {
    #region Methods and constructors
    public static TResult Aggregate<TSource, TAccumulate, TResult>(ParallelQuery<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
    {
      return default(TResult);
    }

    public static TResult Aggregate<TSource, TAccumulate, TResult>(ParallelQuery<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> updateAccumulatorFunc, Func<TAccumulate, TAccumulate, TAccumulate> combineAccumulatorsFunc, Func<TAccumulate, TResult> resultSelector)
    {
      return default(TResult);
    }

    public static TResult Aggregate<TSource, TAccumulate, TResult>(ParallelQuery<TSource> source, Func<TAccumulate> seedFactory, Func<TAccumulate, TSource, TAccumulate> updateAccumulatorFunc, Func<TAccumulate, TAccumulate, TAccumulate> combineAccumulatorsFunc, Func<TAccumulate, TResult> resultSelector)
    {
      return default(TResult);
    }

    public static TSource Aggregate<TSource>(ParallelQuery<TSource> source, Func<TSource, TSource, TSource> func)
    {
      return default(TSource);
    }

    public static TAccumulate Aggregate<TSource, TAccumulate>(ParallelQuery<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
    {
      return default(TAccumulate);
    }

    public static bool All<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      return default(bool);
    }

    public static bool Any<TSource>(ParallelQuery<TSource> source)
    {
      return default(bool);
    }

    public static bool Any<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      return default(bool);
    }

    public static IEnumerable<TSource> AsEnumerable<TSource>(ParallelQuery<TSource> source)
    {
      return default(IEnumerable<TSource>);
    }

    public static ParallelQuery<TSource> AsOrdered<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery AsOrdered(ParallelQuery source)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery>() != null);

      return default(ParallelQuery);
    }

    public static ParallelQuery<TSource> AsParallel<TSource>(IEnumerable<TSource> source)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery AsParallel(System.Collections.IEnumerable source)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery>() != null);

      return default(ParallelQuery);
    }

    public static ParallelQuery<TSource> AsParallel<TSource>(System.Collections.Concurrent.Partitioner<TSource> source)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static IEnumerable<TSource> AsSequential<TSource>(ParallelQuery<TSource> source)
    {
      return default(IEnumerable<TSource>);
    }

    public static ParallelQuery<TSource> AsUnordered<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static Decimal Average(ParallelQuery<Decimal> source)
    {
      return default(Decimal);
    }

    public static Nullable<double> Average(ParallelQuery<Nullable<double>> source)
    {
      return default(Nullable<double>);
    }

    public static double Average(ParallelQuery<double> source)
    {
      return default(double);
    }

    public static Nullable<double> Average<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<int>> selector)
    {
      return default(Nullable<double>);
    }

    public static double Average<TSource>(ParallelQuery<TSource> source, Func<TSource, double> selector)
    {
      return default(double);
    }

    public static Nullable<Decimal> Average(ParallelQuery<Nullable<Decimal>> source)
    {
      return default(Nullable<Decimal>);
    }

    public static double Average(ParallelQuery<long> source)
    {
      return default(double);
    }

    public static Nullable<double> Average(ParallelQuery<Nullable<int>> source)
    {
      return default(Nullable<double>);
    }

    public static double Average(ParallelQuery<int> source)
    {
      return default(double);
    }

    public static Nullable<float> Average(ParallelQuery<Nullable<float>> source)
    {
      return default(Nullable<float>);
    }

    public static float Average(ParallelQuery<float> source)
    {
      return default(float);
    }

    public static Nullable<double> Average(ParallelQuery<Nullable<long>> source)
    {
      return default(Nullable<double>);
    }

    public static double Average<TSource>(ParallelQuery<TSource> source, Func<TSource, long> selector)
    {
      return default(double);
    }

    public static Nullable<double> Average<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<double>> selector)
    {
      return default(Nullable<double>);
    }

    public static Decimal Average<TSource>(ParallelQuery<TSource> source, Func<TSource, Decimal> selector)
    {
      return default(Decimal);
    }

    public static Nullable<Decimal> Average<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<Decimal>> selector)
    {
      return default(Nullable<Decimal>);
    }

    public static double Average<TSource>(ParallelQuery<TSource> source, Func<TSource, int> selector)
    {
      return default(double);
    }

    public static Nullable<double> Average<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<long>> selector)
    {
      return default(Nullable<double>);
    }

    public static float Average<TSource>(ParallelQuery<TSource> source, Func<TSource, float> selector)
    {
      return default(float);
    }

    public static Nullable<float> Average<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<float>> selector)
    {
      return default(Nullable<float>);
    }

    public static ParallelQuery<TResult> Cast<TResult>(ParallelQuery source)
    {
      Contract.Requires(source != null);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TSource> Concat<TSource>(ParallelQuery<TSource> first, IEnumerable<TSource> second)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> Concat<TSource>(ParallelQuery<TSource> first, ParallelQuery<TSource> second)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static bool Contains<TSource>(ParallelQuery<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
    {
      return default(bool);
    }

    public static bool Contains<TSource>(ParallelQuery<TSource> source, TSource value)
    {
      return default(bool);
    }

    public static int Count<TSource>(ParallelQuery<TSource> source)
    {
      return default(int);
    }

    public static int Count<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      return default(int);
    }

    public static ParallelQuery<TSource> DefaultIfEmpty<TSource>(ParallelQuery<TSource> source, TSource defaultValue)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> DefaultIfEmpty<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> Distinct<TSource>(ParallelQuery<TSource> source, IEqualityComparer<TSource> comparer)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> Distinct<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static TSource ElementAt<TSource>(ParallelQuery<TSource> source, int index)
    {
      return default(TSource);
    }

    public static TSource ElementAtOrDefault<TSource>(ParallelQuery<TSource> source, int index)
    {
      return default(TSource);
    }

    public static ParallelQuery<TResult> Empty<TResult>()
    {
      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TSource> Except<TSource>(ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> Except<TSource>(ParallelQuery<TSource> first, IEnumerable<TSource> second)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> Except<TSource>(ParallelQuery<TSource> first, ParallelQuery<TSource> second)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> Except<TSource>(ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static TSource First<TSource>(ParallelQuery<TSource> source)
    {
      return default(TSource);
    }

    public static TSource First<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      return default(TSource);
    }

    public static TSource FirstOrDefault<TSource>(ParallelQuery<TSource> source)
    {
      return default(TSource);
    }

    public static TSource FirstOrDefault<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      return default(TSource);
    }

    public static void ForAll<TSource>(ParallelQuery<TSource> source, Action<TSource> action)
    {
    }

    public static ParallelQuery<TResult> GroupBy<TSource, TKey, TElement, TResult>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<System.Linq.IGrouping<TKey, TSource>>>() != null);

      return default(ParallelQuery<IGrouping<TKey, TSource>>);
    }

    public static ParallelQuery<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<System.Linq.IGrouping<TKey, TSource>>>() != null);

      return default(ParallelQuery<IGrouping<TKey, TSource>>);
    }

    public static ParallelQuery<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<System.Linq.IGrouping<TKey, TElement>>>() != null);

      return default(ParallelQuery<IGrouping<TKey, TElement>>);
    }

    public static ParallelQuery<TResult> GroupBy<TSource, TKey, TResult>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TResult> GroupBy<TSource, TKey, TElement, TResult>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<System.Linq.IGrouping<TKey, TElement>>>() != null);

      return default(ParallelQuery<IGrouping<TKey, TElement>>);
    }

    public static ParallelQuery<TResult> GroupBy<TSource, TKey, TResult>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TSource> Intersect<TSource>(ParallelQuery<TSource> first, ParallelQuery<TSource> second)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> Intersect<TSource>(ParallelQuery<TSource> first, IEnumerable<TSource> second)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> Intersect<TSource>(ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> Intersect<TSource>(ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    public static TSource Last<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      return default(TSource);
    }

    public static TSource Last<TSource>(ParallelQuery<TSource> source)
    {
      return default(TSource);
    }

    public static TSource LastOrDefault<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      return default(TSource);
    }

    public static TSource LastOrDefault<TSource>(ParallelQuery<TSource> source)
    {
      return default(TSource);
    }

    public static long LongCount<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Ensures(-9223372036854775808 <= Contract.Result<long>());
      Contract.Ensures(Contract.Result<long>() <= 9223372036854775807);

      return default(long);
    }

    public static long LongCount<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      return default(long);
    }

    public static TSource Max<TSource>(ParallelQuery<TSource> source)
    {
      return default(TSource);
    }

    public static Nullable<Decimal> Max(ParallelQuery<Nullable<Decimal>> source)
    {
      return default(Nullable<Decimal>);
    }

    public static int Max<TSource>(ParallelQuery<TSource> source, Func<TSource, int> selector)
    {
      return default(int);
    }

    public static Nullable<int> Max<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<int>> selector)
    {
      return default(Nullable<int>);
    }

    public static Decimal Max(ParallelQuery<Decimal> source)
    {
      return default(Decimal);
    }

    public static Nullable<float> Max(ParallelQuery<Nullable<float>> source)
    {
      return default(Nullable<float>);
    }

    public static float Max(ParallelQuery<float> source)
    {
      return default(float);
    }

    public static Nullable<double> Max(ParallelQuery<Nullable<double>> source)
    {
      return default(Nullable<double>);
    }

    public static double Max(ParallelQuery<double> source)
    {
      return default(double);
    }

    public static Decimal Max<TSource>(ParallelQuery<TSource> source, Func<TSource, Decimal> selector)
    {
      return default(Decimal);
    }

    public static Nullable<double> Max<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<double>> selector)
    {
      return default(Nullable<double>);
    }

    public static Nullable<Decimal> Max<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<Decimal>> selector)
    {
      return default(Nullable<Decimal>);
    }

    public static Nullable<int> Max(ParallelQuery<Nullable<int>> source)
    {
      return default(Nullable<int>);
    }

    public static TResult Max<TSource, TResult>(ParallelQuery<TSource> source, Func<TSource, TResult> selector)
    {
      return default(TResult);
    }

    public static Nullable<long> Max<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<long>> selector)
    {
      return default(Nullable<long>);
    }

    public static long Max<TSource>(ParallelQuery<TSource> source, Func<TSource, long> selector)
    {
      return default(long);
    }

    public static float Max<TSource>(ParallelQuery<TSource> source, Func<TSource, float> selector)
    {
      return default(float);
    }

    public static double Max<TSource>(ParallelQuery<TSource> source, Func<TSource, double> selector)
    {
      return default(double);
    }

    public static Nullable<float> Max<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<float>> selector)
    {
      return default(Nullable<float>);
    }

    public static int Max(ParallelQuery<int> source)
    {
      return default(int);
    }

    public static long Max(ParallelQuery<long> source)
    {
      return default(long);
    }

    public static Nullable<long> Max(ParallelQuery<Nullable<long>> source)
    {
      return default(Nullable<long>);
    }

    public static Nullable<double> Min(ParallelQuery<Nullable<double>> source)
    {
      return default(Nullable<double>);
    }

    public static double Min(ParallelQuery<double> source)
    {
      return default(double);
    }

    public static Decimal Min(ParallelQuery<Decimal> source)
    {
      return default(Decimal);
    }

    public static TSource Min<TSource>(ParallelQuery<TSource> source)
    {
      return default(TSource);
    }

    public static Nullable<Decimal> Min(ParallelQuery<Nullable<Decimal>> source)
    {
      return default(Nullable<Decimal>);
    }

    public static int Min(ParallelQuery<int> source)
    {
      return default(int);
    }

    public static float Min(ParallelQuery<float> source)
    {
      return default(float);
    }

    public static Nullable<long> Min(ParallelQuery<Nullable<long>> source)
    {
      return default(Nullable<long>);
    }

    public static Nullable<float> Min(ParallelQuery<Nullable<float>> source)
    {
      return default(Nullable<float>);
    }

    public static Nullable<int> Min(ParallelQuery<Nullable<int>> source)
    {
      return default(Nullable<int>);
    }

    public static long Min(ParallelQuery<long> source)
    {
      return default(long);
    }

    public static Nullable<float> Min<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<float>> selector)
    {
      return default(Nullable<float>);
    }

    public static TResult Min<TSource, TResult>(ParallelQuery<TSource> source, Func<TSource, TResult> selector)
    {
      return default(TResult);
    }

    public static float Min<TSource>(ParallelQuery<TSource> source, Func<TSource, float> selector)
    {
      return default(float);
    }

    public static double Min<TSource>(ParallelQuery<TSource> source, Func<TSource, double> selector)
    {
      return default(double);
    }

    public static Nullable<Decimal> Min<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<Decimal>> selector)
    {
      return default(Nullable<Decimal>);
    }

    public static Decimal Min<TSource>(ParallelQuery<TSource> source, Func<TSource, Decimal> selector)
    {
      return default(Decimal);
    }

    public static Nullable<double> Min<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<double>> selector)
    {
      return default(Nullable<double>);
    }

    public static Nullable<long> Min<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<long>> selector)
    {
      return default(Nullable<long>);
    }

    public static int Min<TSource>(ParallelQuery<TSource> source, Func<TSource, int> selector)
    {
      return default(int);
    }

    public static Nullable<int> Min<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<int>> selector)
    {
      return default(Nullable<int>);
    }

    public static long Min<TSource>(ParallelQuery<TSource> source, Func<TSource, long> selector)
    {
      return default(long);
    }

    public static ParallelQuery<TResult> OfType<TResult>(ParallelQuery source)
    {
      return default(ParallelQuery<TResult>);
    }

    public static OrderedParallelQuery<TSource> OrderBy<TSource, TKey>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
    {
      Contract.Ensures(Contract.Result<System.Linq.OrderedParallelQuery<TSource>>() != null);

      return default(OrderedParallelQuery<TSource>);
    }

    public static OrderedParallelQuery<TSource> OrderBy<TSource, TKey>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
    {
      Contract.Ensures(Contract.Result<System.Linq.OrderedParallelQuery<TSource>>() != null);

      return default(OrderedParallelQuery<TSource>);
    }

    public static OrderedParallelQuery<TSource> OrderByDescending<TSource, TKey>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
    {
      Contract.Ensures(Contract.Result<System.Linq.OrderedParallelQuery<TSource>>() != null);

      return default(OrderedParallelQuery<TSource>);
    }

    public static OrderedParallelQuery<TSource> OrderByDescending<TSource, TKey>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
    {
      Contract.Ensures(Contract.Result<System.Linq.OrderedParallelQuery<TSource>>() != null);

      return default(OrderedParallelQuery<TSource>);
    }

    public static ParallelQuery<int> Range(int start, int count)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<int>>() != null);

      return default(ParallelQuery<int>);
    }

    public static ParallelQuery<TResult> Repeat<TResult>(TResult element, int count)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TSource> Reverse<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TResult> Select<TSource, TResult>(ParallelQuery<TSource> source, Func<TSource, TResult> selector)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TResult> Select<TSource, TResult>(ParallelQuery<TSource> source, Func<TSource, int, TResult> selector)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TResult> SelectMany<TSource, TResult>(ParallelQuery<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TResult> SelectMany<TSource, TCollection, TResult>(ParallelQuery<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TResult> SelectMany<TSource, TResult>(ParallelQuery<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TResult> SelectMany<TSource, TCollection, TResult>(ParallelQuery<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    public static bool SequenceEqual<TSource>(ParallelQuery<TSource> first, ParallelQuery<TSource> second)
    {
      return default(bool);
    }

    public static bool SequenceEqual<TSource>(ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
    {
      Contract.Ensures(false);

      return default(bool);
    }

    public static bool SequenceEqual<TSource>(ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
    {
      return default(bool);
    }

    public static bool SequenceEqual<TSource>(ParallelQuery<TSource> first, IEnumerable<TSource> second)
    {
      Contract.Ensures(false);

      return default(bool);
    }

    public static TSource Single<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      return default(TSource);
    }

    public static TSource Single<TSource>(ParallelQuery<TSource> source)
    {
      return default(TSource);
    }

    public static TSource SingleOrDefault<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      return default(TSource);
    }

    public static TSource SingleOrDefault<TSource>(ParallelQuery<TSource> source)
    {
      return default(TSource);
    }

    public static ParallelQuery<TSource> Skip<TSource>(ParallelQuery<TSource> source, int count)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> SkipWhile<TSource>(ParallelQuery<TSource> source, Func<TSource, int, bool> predicate)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> SkipWhile<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static Nullable<long> Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<long>> selector)
    {
      return default(Nullable<long>);
    }

    public static Nullable<float> Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<float>> selector)
    {
      return default(Nullable<float>);
    }

    public static float Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, float> selector)
    {
      return default(float);
    }

    public static double Sum(ParallelQuery<double> source)
    {
      return default(double);
    }

    public static long Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, long> selector)
    {
      return default(long);
    }

    public static Decimal Sum(ParallelQuery<Decimal> source)
    {
      return default(Decimal);
    }

    public static Nullable<double> Sum(ParallelQuery<Nullable<double>> source)
    {
      return default(Nullable<double>);
    }

    public static Nullable<Decimal> Sum(ParallelQuery<Nullable<Decimal>> source)
    {
      return default(Nullable<Decimal>);
    }

    public static Nullable<int> Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<int>> selector)
    {
      return default(Nullable<int>);
    }

    public static int Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, int> selector)
    {
      return default(int);
    }

    public static Nullable<int> Sum(ParallelQuery<Nullable<int>> source)
    {
      return default(Nullable<int>);
    }

    public static int Sum(ParallelQuery<int> source)
    {
      return default(int);
    }

    public static Nullable<double> Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<double>> selector)
    {
      return default(Nullable<double>);
    }

    public static Nullable<long> Sum(ParallelQuery<Nullable<long>> source)
    {
      return default(Nullable<long>);
    }

    public static long Sum(ParallelQuery<long> source)
    {
      return default(long);
    }

    public static float Sum(ParallelQuery<float> source)
    {
      return default(float);
    }

    public static Nullable<float> Sum(ParallelQuery<Nullable<float>> source)
    {
      return default(Nullable<float>);
    }

    public static Nullable<Decimal> Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<Decimal>> selector)
    {
      return default(Nullable<Decimal>);
    }

    public static Decimal Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, Decimal> selector)
    {
      return default(Decimal);
    }

    public static double Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, double> selector)
    {
      return default(double);
    }

    public static ParallelQuery<TSource> Take<TSource>(ParallelQuery<TSource> source, int count)
    {
      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> TakeWhile<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> TakeWhile<TSource>(ParallelQuery<TSource> source, Func<TSource, int, bool> predicate)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static OrderedParallelQuery<TSource> ThenBy<TSource, TKey>(OrderedParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
    {
      Contract.Ensures(Contract.Result<System.Linq.OrderedParallelQuery<TSource>>() != null);

      return default(OrderedParallelQuery<TSource>);
    }

    public static OrderedParallelQuery<TSource> ThenBy<TSource, TKey>(OrderedParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
    {
      Contract.Ensures(Contract.Result<System.Linq.OrderedParallelQuery<TSource>>() != null);

      return default(OrderedParallelQuery<TSource>);
    }

    public static OrderedParallelQuery<TSource> ThenByDescending<TSource, TKey>(OrderedParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
    {
      Contract.Ensures(Contract.Result<System.Linq.OrderedParallelQuery<TSource>>() != null);

      return default(OrderedParallelQuery<TSource>);
    }

    public static OrderedParallelQuery<TSource> ThenByDescending<TSource, TKey>(OrderedParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
    {
      Contract.Ensures(Contract.Result<System.Linq.OrderedParallelQuery<TSource>>() != null);

      return default(OrderedParallelQuery<TSource>);
    }

    public static TSource[] ToArray<TSource>(ParallelQuery<TSource> source)
    {
      return default(TSource[]);
    }

    public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.Dictionary<TKey, TSource>>() != null);

      return default(Dictionary<TKey, TSource>);
    }

    public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.Dictionary<TKey, TSource>>() != null);

      return default(Dictionary<TKey, TSource>);
    }

    public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.Dictionary<TKey, TElement>>() != null);

      return default(Dictionary<TKey, TElement>);
    }

    public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.Dictionary<TKey, TElement>>() != null);

      return default(Dictionary<TKey, TElement>);
    }

    public static List<TSource> ToList<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.List<TSource>>() != null);

      return default(List<TSource>);
    }

    public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Ensures(Contract.Result<System.Linq.ILookup<TKey, TElement>>() != null);

      return default(ILookup<TKey, TElement>);
    }

    public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
    {
      Contract.Ensures(Contract.Result<System.Linq.ILookup<TKey, TElement>>() != null);

      return default(ILookup<TKey, TElement>);
    }

    public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Ensures(Contract.Result<System.Linq.ILookup<TKey, TSource>>() != null);

      return default(ILookup<TKey, TSource>);
    }

    public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
    {
      Contract.Ensures(Contract.Result<System.Linq.ILookup<TKey, TSource>>() != null);

      return default(ILookup<TKey, TSource>);
    }

    public static ParallelQuery<TSource> Union<TSource>(ParallelQuery<TSource> first, ParallelQuery<TSource> second)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> Union<TSource>(ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> Union<TSource>(ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> Union<TSource>(ParallelQuery<TSource> first, IEnumerable<TSource> second)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> Where<TSource>(ParallelQuery<TSource> source, Func<TSource, int, bool> predicate)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> Where<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> WithCancellation<TSource>(ParallelQuery<TSource> source, System.Threading.CancellationToken cancellationToken)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> WithDegreeOfParallelism<TSource>(ParallelQuery<TSource> source, int degreeOfParallelism)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> WithExecutionMode<TSource>(ParallelQuery<TSource> source, ParallelExecutionMode executionMode)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TSource> WithMergeOptions<TSource>(ParallelQuery<TSource> source, ParallelMergeOptions mergeOptions)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    public static ParallelQuery<TResult> Zip<TFirst, TSecond, TResult>(ParallelQuery<TFirst> first, ParallelQuery<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TResult> Zip<TFirst, TSecond, TResult>(ParallelQuery<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TResult>);
    }
    #endregion
  }
}

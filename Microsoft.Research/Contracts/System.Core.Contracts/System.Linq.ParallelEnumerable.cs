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
#if NETFRAMEWORK_4_0
  static public partial class ParallelEnumerable
  {
    #region Methods and constructors
    [Pure]
    public static TResult Aggregate<TSource, TAccumulate, TResult>(
      ParallelQuery<TSource> source, 
      TAccumulate seed, 
      Func<TAccumulate, TSource, TAccumulate> func, 
      Func<TAccumulate, TResult> resultSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(func != null);
      Contract.Requires(resultSelector != null);
      return default(TResult);
    }

    [Pure]
    public static TResult Aggregate<TSource, TAccumulate, TResult>(
      ParallelQuery<TSource> source, 
      TAccumulate seed, 
      Func<TAccumulate, TSource, TAccumulate> updateAccumulatorFunc, 
      Func<TAccumulate, TAccumulate, TAccumulate> combineAccumulatorsFunc, 
      Func<TAccumulate, TResult> resultSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(combineAccumulatorsFunc != null);
      Contract.Requires(resultSelector != null);
      return default(TResult);
    }

    [Pure]
    public static TResult Aggregate<TSource, TAccumulate, TResult>(
      ParallelQuery<TSource> source, 
      Func<TAccumulate> seedFactory, 
      Func<TAccumulate, TSource, TAccumulate> updateAccumulatorFunc, 
      Func<TAccumulate, TAccumulate, TAccumulate> combineAccumulatorsFunc, 
      Func<TAccumulate, TResult> resultSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(seedFactory != null);
      Contract.Requires(updateAccumulatorFunc != null);
      Contract.Requires(combineAccumulatorsFunc != null);
      Contract.Requires(resultSelector != null);
      return default(TResult);
    }

    [Pure]
    public static TSource Aggregate<TSource>(ParallelQuery<TSource> source, Func<TSource, TSource, TSource> func)
    {
      Contract.Requires(source != null);
      Contract.Requires(func != null);
      return default(TSource);
    }

    [Pure]
    public static TAccumulate Aggregate<TSource, TAccumulate>(
      ParallelQuery<TSource> source, 
      TAccumulate seed, 
      Func<TAccumulate, TSource, TAccumulate> func)
    {
      Contract.Requires(source != null);
      Contract.Requires(func != null);
      return default(TAccumulate);
    }

    [Pure]
    public static bool All<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      return default(bool);
    }

    [Pure]
    public static bool Any<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      return default(bool);
    }

    [Pure]
    public static bool Any<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      return default(bool);
    }

    [Pure]
    public static IEnumerable<TSource> AsEnumerable<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      return default(IEnumerable<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> AsOrdered<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery AsOrdered(ParallelQuery source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery>() != null);

      return default(ParallelQuery);
    }

    [Pure]
    public static ParallelQuery<TSource> AsParallel<TSource>(IEnumerable<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery AsParallel(System.Collections.IEnumerable source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery>() != null);

      return default(ParallelQuery);
    }

    [Pure]
    public static IEnumerable<TSource> AsSequential<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      return default(IEnumerable<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> AsUnordered<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static Decimal Average(ParallelQuery<Decimal> source)
    {
      Contract.Requires(source != null);
      return default(Decimal);
    }

    [Pure]
    public static Nullable<double> Average(ParallelQuery<Nullable<double>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<double>);
    }

    [Pure]
    public static double Average(ParallelQuery<double> source)
    {
      Contract.Requires(source != null);
      return default(double);
    }

    [Pure]
    public static Nullable<double> Average<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<int>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<double>);
    }

    [Pure]
    public static double Average<TSource>(ParallelQuery<TSource> source, Func<TSource, double> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(double);
    }

    [Pure]
    public static Nullable<Decimal> Average(ParallelQuery<Nullable<Decimal>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<Decimal>);
    }

    [Pure]
    public static double Average(ParallelQuery<long> source)
    {
      Contract.Requires(source != null);
      return default(double);
    }

    [Pure]
    public static Nullable<double> Average(ParallelQuery<Nullable<int>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<double>);
    }

    [Pure]
    public static double Average(ParallelQuery<int> source)
    {
      Contract.Requires(source != null);
      return default(double);
    }

    [Pure]
    public static Nullable<float> Average(ParallelQuery<Nullable<float>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<float>);
    }

    [Pure]
    public static float Average(ParallelQuery<float> source)
    {
      Contract.Requires(source != null);
      return default(float);
    }

    [Pure]
    public static Nullable<double> Average(ParallelQuery<Nullable<long>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<double>);
    }

    [Pure]
    public static double Average<TSource>(ParallelQuery<TSource> source, Func<TSource, long> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(double);
    }

    [Pure]
    public static Nullable<double> Average<TSource>(
      ParallelQuery<TSource> source, 
      Func<TSource, Nullable<double>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<double>);
    }

    [Pure]
    public static Decimal Average<TSource>(
      ParallelQuery<TSource> source, 
      Func<TSource, Decimal> selector)
    {
      Contract.Requires(source != null);
      return default(Decimal);
    }

    [Pure]
    public static Nullable<Decimal> Average<TSource>(
      ParallelQuery<TSource> source, 
      Func<TSource, Nullable<Decimal>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<Decimal>);
    }

    [Pure]
    public static double Average<TSource>(
      ParallelQuery<TSource> source, 
      Func<TSource, int> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(double);
    }

    [Pure]
    public static Nullable<double> Average<TSource>(
      ParallelQuery<TSource> source, 
      Func<TSource, Nullable<long>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<double>);
    }

    [Pure]
    public static float Average<TSource>(ParallelQuery<TSource> source, Func<TSource, float> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(float);
    }

    [Pure]
    public static Nullable<float> Average<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<float>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<float>);
    }

    [Pure]
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

    [Pure]
    public static ParallelQuery<TSource> Concat<TSource>(ParallelQuery<TSource> first, ParallelQuery<TSource> second)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static bool Contains<TSource>(ParallelQuery<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(source != null);
      return default(bool);
    }

    [Pure]
    public static bool Contains<TSource>(ParallelQuery<TSource> source, TSource value)
    {
      Contract.Requires(source != null);
      return default(bool);
    }

    [Pure]
    public static int Count<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<int>() >= 0);
      return default(int);
    }

    [Pure]
    public static int Count<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      Contract.Ensures(Contract.Result<int>() >= 0);
      return default(int);
    }

    [Pure]
    public static ParallelQuery<TSource> DefaultIfEmpty<TSource>(ParallelQuery<TSource> source, TSource defaultValue)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>().Any());

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> DefaultIfEmpty<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>().Any());

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> Distinct<TSource>(ParallelQuery<TSource> source, IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> Distinct<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static TSource ElementAt<TSource>(ParallelQuery<TSource> source, int index)
    {
      Contract.Requires(source != null);
      Contract.Requires(index >= 0);
      return default(TSource);
    }

    [Pure]
    public static TSource ElementAtOrDefault<TSource>(ParallelQuery<TSource> source, int index)
    {
      Contract.Requires(source != null);
      return default(TSource);
    }

    [Pure]
    public static ParallelQuery<TResult> Empty<TResult>()
    {
      Contract.Ensures(Contract.Result<ParallelQuery<TResult>>() != null);
      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static ParallelQuery<TSource> Except<TSource>(ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> Except<TSource>(ParallelQuery<TSource> first, IEnumerable<TSource> second)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> Except<TSource>(ParallelQuery<TSource> first, ParallelQuery<TSource> second)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> Except<TSource>(ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static TSource First<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Requires(ParallelEnumerable.Count<TSource>(source) > 0);
      return default(TSource);
    }

    [Pure]
    public static TSource First<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      Contract.Requires(ParallelEnumerable.Count<TSource>(source) > 0);
      return default(TSource);
    }

    [Pure]
    public static TSource FirstOrDefault<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      return default(TSource);
    }

    [Pure]
    public static TSource FirstOrDefault<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      return default(TSource);
    }

    public static void ForAll<TSource>(ParallelQuery<TSource> source, Action<TSource> action)
    {
      Contract.Requires(source != null);
      Contract.Requires(action != null);
    }

    [Pure]
    public static ParallelQuery<TResult> GroupBy<TSource, TKey, TElement, TResult>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector, 
      Func<TSource, TElement> elementSelector, 
      Func<TKey, IEnumerable<TElement>, TResult> resultSelector, 
      IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static ParallelQuery<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<System.Linq.IGrouping<TKey, TSource>>>() != null);

      return default(ParallelQuery<IGrouping<TKey, TSource>>);
    }

    [Pure]
    public static ParallelQuery<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector, 
      IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<System.Linq.IGrouping<TKey, TSource>>>() != null);

      return default(ParallelQuery<IGrouping<TKey, TSource>>);
    }

    [Pure]
    public static ParallelQuery<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector, 
      Func<TSource, TElement> elementSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<System.Linq.IGrouping<TKey, TElement>>>() != null);

      return default(ParallelQuery<IGrouping<TKey, TElement>>);
    }

    [Pure]
    public static ParallelQuery<TResult> GroupBy<TSource, TKey, TResult>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector, 
      Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static ParallelQuery<TResult> GroupBy<TSource, TKey, TElement, TResult>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector, 
      Func<TSource, TElement> elementSelector, 
      Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static ParallelQuery<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector, 
      Func<TSource, TElement> elementSelector, 
      IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<System.Linq.IGrouping<TKey, TElement>>>() != null);

      return default(ParallelQuery<IGrouping<TKey, TElement>>);
    }

    [Pure]
    public static ParallelQuery<TResult> GroupBy<TSource, TKey, TResult>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector, 
      Func<TKey, IEnumerable<TSource>, TResult> resultSelector, 
      IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(
      ParallelQuery<TOuter> outer, 
      ParallelQuery<TInner> inner, 
      Func<TOuter, TKey> outerKeySelector, 
      Func<TInner, TKey> innerKeySelector, 
      Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
    {
      Contract.Requires(outer != null);
      Contract.Requires(inner != null);
      Contract.Requires(outerKeySelector != null);
      Contract.Requires(innerKeySelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(
      ParallelQuery<TOuter> outer, 
      IEnumerable<TInner> inner, 
      Func<TOuter, TKey> outerKeySelector, 
      Func<TInner, TKey> innerKeySelector, 
      Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(
      ParallelQuery<TOuter> outer, 
      ParallelQuery<TInner> inner, 
      Func<TOuter, TKey> outerKeySelector, 
      Func<TInner, TKey> innerKeySelector, 
      Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, 
      IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(outer != null);
      Contract.Requires(inner != null);
      Contract.Requires(outerKeySelector != null);
      Contract.Requires(innerKeySelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(
      ParallelQuery<TOuter> outer, 
      IEnumerable<TInner> inner, 
      Func<TOuter, TKey> outerKeySelector, 
      Func<TInner, TKey> innerKeySelector, 
      Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, 
      IEqualityComparer<TKey> comparer)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static ParallelQuery<TSource> Intersect<TSource>(
      ParallelQuery<TSource> first, 
      ParallelQuery<TSource> second)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> Intersect<TSource>(
      ParallelQuery<TSource> first, 
      IEnumerable<TSource> second)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Ensures(false);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> Intersect<TSource>(
      ParallelQuery<TSource> first, 
      IEnumerable<TSource> second, 
      IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Ensures(false);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> Intersect<TSource>(
      ParallelQuery<TSource> first, 
      ParallelQuery<TSource> second, 
      IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(
      ParallelQuery<TOuter> outer, 
      ParallelQuery<TInner> inner, 
      Func<TOuter, TKey> outerKeySelector, 
      Func<TInner, TKey> innerKeySelector, 
      Func<TOuter, TInner, TResult> resultSelector, 
      IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(outer != null);
      Contract.Requires(inner != null);
      Contract.Requires(outerKeySelector != null);
      Contract.Requires(innerKeySelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(
      ParallelQuery<TOuter> outer, 
      IEnumerable<TInner> inner, 
      Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TResult>);
    }

    public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(
      ParallelQuery<TOuter> outer, 
      ParallelQuery<TInner> inner, 
      Func<TOuter, TKey> outerKeySelector, 
      Func<TInner, TKey> innerKeySelector, 
      Func<TOuter, TInner, TResult> resultSelector)
    {
      Contract.Requires(outer != null);
      Contract.Requires(inner != null);
      Contract.Requires(outerKeySelector != null);
      Contract.Requires(innerKeySelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static TSource Last<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      Contract.Requires(Count(source) > 0);
      return default(TSource);
    }

    [Pure]
    public static TSource Last<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Requires(Count(source) > 0);
      return default(TSource);
    }

    [Pure]
    public static TSource LastOrDefault<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      return default(TSource);
    }

    [Pure]
    public static TSource LastOrDefault<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      return default(TSource);
    }

    [Pure]
    public static long LongCount<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);

      return default(long);
    }

    [Pure]
    public static long LongCount<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      return default(long);
    }

    [Pure]
    public static TSource Max<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      return default(TSource);
    }

    [Pure]
    public static Nullable<Decimal> Max(ParallelQuery<Nullable<Decimal>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<Decimal>);
    }

    [Pure]
    public static int Max<TSource>(ParallelQuery<TSource> source, Func<TSource, int> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(int);
    }

    [Pure]
    public static Nullable<int> Max<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<int>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<int>);
    }

    [Pure]
    public static Decimal Max(ParallelQuery<Decimal> source)
    {
      Contract.Requires(source != null);
      return default(Decimal);
    }

    [Pure]
    public static Nullable<float> Max(ParallelQuery<Nullable<float>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<float>);
    }

    [Pure]
    public static float Max(ParallelQuery<float> source)
    {
      Contract.Requires(source != null);
      return default(float);
    }

    [Pure]
    public static Nullable<double> Max(ParallelQuery<Nullable<double>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<double>);
    }

    [Pure]
    public static double Max(ParallelQuery<double> source)
    {
      Contract.Requires(source != null);
      return default(double);
    }

    [Pure]
    public static Decimal Max<TSource>(ParallelQuery<TSource> source, Func<TSource, Decimal> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Decimal);
    }

    [Pure]
    public static Nullable<double> Max<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<double>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<double>);
    }

    [Pure]
    public static Nullable<Decimal> Max<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<Decimal>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<Decimal>);
    }

    [Pure]
    public static Nullable<int> Max(ParallelQuery<Nullable<int>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<int>);
    }

    [Pure]
    public static TResult Max<TSource, TResult>(ParallelQuery<TSource> source, Func<TSource, TResult> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(TResult);
    }

    [Pure]
    public static Nullable<long> Max<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<long>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<long>);
    }

    [Pure]
    public static long Max<TSource>(ParallelQuery<TSource> source, Func<TSource, long> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(long);
    }

    [Pure]
    public static float Max<TSource>(ParallelQuery<TSource> source, Func<TSource, float> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(float);
    }

    [Pure]
    public static double Max<TSource>(ParallelQuery<TSource> source, Func<TSource, double> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(double);
    }

    [Pure]
    public static Nullable<float> Max<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<float>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<float>);
    }

    [Pure]
    public static int Max(ParallelQuery<int> source)
    {
      Contract.Requires(source != null);
      return default(int);
    }

    [Pure]
    public static long Max(ParallelQuery<long> source)
    {
      Contract.Requires(source != null);
      return default(long);
    }

    [Pure]
    public static Nullable<long> Max(ParallelQuery<Nullable<long>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<long>);
    }

    [Pure]
    public static Nullable<double> Min(ParallelQuery<Nullable<double>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<double>);
    }

    [Pure]
    public static double Min(ParallelQuery<double> source)
    {
      Contract.Requires(source != null);
      return default(double);
    }

    [Pure]
    public static Decimal Min(ParallelQuery<Decimal> source)
    {
      Contract.Requires(source != null);
      return default(Decimal);
    }

    [Pure]
    public static TSource Min<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      return default(TSource);
    }

    [Pure]
    public static Nullable<Decimal> Min(ParallelQuery<Nullable<Decimal>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<Decimal>);
    }

    [Pure]
    public static int Min(ParallelQuery<int> source)
    {
      Contract.Requires(source != null);
      return default(int);
    }

    [Pure]
    public static float Min(ParallelQuery<float> source)
    {
      Contract.Requires(source != null);
      return default(float);
    }

    [Pure]
    public static Nullable<long> Min(ParallelQuery<Nullable<long>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<long>);
    }

    [Pure]
    public static Nullable<float> Min(ParallelQuery<Nullable<float>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<float>);
    }

    [Pure]
    public static Nullable<int> Min(ParallelQuery<Nullable<int>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<int>);
    }

    [Pure]
    public static long Min(ParallelQuery<long> source)
    {
      Contract.Requires(source != null);
      return default(long);
    }

    [Pure]
    public static Nullable<float> Min<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<float>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<float>);
    }

    [Pure]
    public static TResult Min<TSource, TResult>(ParallelQuery<TSource> source, Func<TSource, TResult> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(TResult);
    }

    [Pure]
    public static float Min<TSource>(ParallelQuery<TSource> source, Func<TSource, float> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(float);
    }

    [Pure]
    public static double Min<TSource>(ParallelQuery<TSource> source, Func<TSource, double> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(double);
    }

    [Pure]
    public static Nullable<Decimal> Min<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<Decimal>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<Decimal>);
    }

    [Pure]
    public static Decimal Min<TSource>(ParallelQuery<TSource> source, Func<TSource, Decimal> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Decimal);
    }

    [Pure]
    public static Nullable<double> Min<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<double>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<double>);
    }

    [Pure]
    public static Nullable<long> Min<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<long>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<long>);
    }

    [Pure]
    public static int Min<TSource>(ParallelQuery<TSource> source, Func<TSource, int> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(int);
    }

    [Pure]
    public static Nullable<int> Min<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<int>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<int>);
    }

    [Pure]
    public static long Min<TSource>(ParallelQuery<TSource> source, Func<TSource, long> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(long);
    }

    [Pure]
    public static ParallelQuery<TResult> OfType<TResult>(ParallelQuery source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<ParallelQuery<TResult>>() != null);
      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static OrderedParallelQuery<TSource> OrderBy<TSource, TKey>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector, 
      IComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<System.Linq.OrderedParallelQuery<TSource>>() != null);

      return default(OrderedParallelQuery<TSource>);
    }

    [Pure]
    public static OrderedParallelQuery<TSource> OrderBy<TSource, TKey>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<System.Linq.OrderedParallelQuery<TSource>>() != null);

      return default(OrderedParallelQuery<TSource>);
    }

    [Pure]
    public static OrderedParallelQuery<TSource> OrderByDescending<TSource, TKey>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector, 
      IComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<System.Linq.OrderedParallelQuery<TSource>>() != null);

      return default(OrderedParallelQuery<TSource>);
    }

    [Pure]
    public static OrderedParallelQuery<TSource> OrderByDescending<TSource, TKey>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<System.Linq.OrderedParallelQuery<TSource>>() != null);

      return default(OrderedParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<int> Range(int start, int count)
    {
      Contract.Requires(count >= 0);
      Contract.Requires(0x7fffffff >= start + count - 1);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<int>>() != null);

      return default(ParallelQuery<int>);
    }

    [Pure]
    public static ParallelQuery<TResult> Repeat<TResult>(TResult element, int count)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static ParallelQuery<TSource> Reverse<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TResult> Select<TSource, TResult>(
      ParallelQuery<TSource> source, 
      Func<TSource, TResult> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static ParallelQuery<TResult> Select<TSource, TResult>(
      ParallelQuery<TSource> source, 
      Func<TSource, int, TResult> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static ParallelQuery<TResult> SelectMany<TSource, TResult>(
      ParallelQuery<TSource> source, 
      Func<TSource, IEnumerable<TResult>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static ParallelQuery<TResult> SelectMany<TSource, TCollection, TResult>(
      ParallelQuery<TSource> source, 
      Func<TSource, int, IEnumerable<TCollection>> collectionSelector, 
      Func<TSource, TCollection, TResult> resultSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(collectionSelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static ParallelQuery<TResult> SelectMany<TSource, TResult>(
      ParallelQuery<TSource> source, 
      Func<TSource, int, IEnumerable<TResult>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static ParallelQuery<TResult> SelectMany<TSource, TCollection, TResult>(
      ParallelQuery<TSource> source, 
      Func<TSource, IEnumerable<TCollection>> collectionSelector, 
      Func<TSource, TCollection, TResult> resultSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(collectionSelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static bool SequenceEqual<TSource>(ParallelQuery<TSource> first, ParallelQuery<TSource> second)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      return default(bool);
    }

    [Pure]
    public static bool SequenceEqual<TSource>(
      ParallelQuery<TSource> first, 
      IEnumerable<TSource> second, 
      IEqualityComparer<TSource> comparer)
    {
      Contract.Ensures(false);

      return default(bool);
    }

    [Pure]
    public static bool SequenceEqual<TSource>(ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      return default(bool);
    }

    [Pure]
    public static bool SequenceEqual<TSource>(ParallelQuery<TSource> first, IEnumerable<TSource> second)
    {
      Contract.Ensures(false);

      return default(bool);
    }

    [Pure]
    public static TSource Single<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      return default(TSource);
    }

    [Pure]
    public static TSource Single<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      return default(TSource);
    }

    [Pure]
    public static TSource SingleOrDefault<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      return default(TSource);
    }

    [Pure]
    public static TSource SingleOrDefault<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      return default(TSource);
    }

    [Pure]
    public static ParallelQuery<TSource> Skip<TSource>(ParallelQuery<TSource> source, int count)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> SkipWhile<TSource>(ParallelQuery<TSource> source, Func<TSource, int, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> SkipWhile<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static Nullable<long> Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<long>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<long>);
    }

    [Pure]
    public static Nullable<float> Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<float>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<float>);
    }

    [Pure]
    public static float Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, float> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(float);
    }

    [Pure]
    public static double Sum(ParallelQuery<double> source)
    {
      Contract.Requires(source != null);
      return default(double);
    }

    [Pure]
    public static long Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, long> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(long);
    }

    [Pure]
    public static Decimal Sum(ParallelQuery<Decimal> source)
    {
      Contract.Requires(source != null);
      return default(Decimal);
    }

    [Pure]
    public static Nullable<double> Sum(ParallelQuery<Nullable<double>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<double>);
    }

    [Pure]
    public static Nullable<Decimal> Sum(ParallelQuery<Nullable<Decimal>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<Decimal>);
    }

    [Pure]
    public static Nullable<int> Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<int>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<int>);
    }

    [Pure]
    public static int Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, int> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(int);
    }

    [Pure]
    public static Nullable<int> Sum(ParallelQuery<Nullable<int>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<int>);
    }

    [Pure]
    public static int Sum(ParallelQuery<int> source)
    {
      Contract.Requires(source != null);
      return default(int);
    }

    [Pure]
    public static Nullable<double> Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<double>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<double>);
    }

    [Pure]
    public static Nullable<long> Sum(ParallelQuery<Nullable<long>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<long>);
    }

    [Pure]
    public static long Sum(ParallelQuery<long> source)
    {
      Contract.Requires(source != null);
      return default(long);
    }

    [Pure]
    public static float Sum(ParallelQuery<float> source)
    {
      Contract.Requires(source != null);
      return default(float);
    }

    [Pure]
    public static Nullable<float> Sum(ParallelQuery<Nullable<float>> source)
    {
      Contract.Requires(source != null);
      return default(Nullable<float>);
    }

    [Pure]
    public static Nullable<Decimal> Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, Nullable<Decimal>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Nullable<Decimal>);
    }

    [Pure]
    public static Decimal Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, Decimal> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(Decimal);
    }

    [Pure]
    public static double Sum<TSource>(ParallelQuery<TSource> source, Func<TSource, double> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(double);
    }

    [Pure]
    public static ParallelQuery<TSource> Take<TSource>(ParallelQuery<TSource> source, int count)
    {
      Contract.Requires(source != null);
      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> TakeWhile<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> TakeWhile<TSource>(ParallelQuery<TSource> source, Func<TSource, int, bool> predicate)
    {
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static OrderedParallelQuery<TSource> ThenBy<TSource, TKey>(
      OrderedParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(comparer != null);
      Contract.Ensures(Contract.Result<System.Linq.OrderedParallelQuery<TSource>>() != null);

      return default(OrderedParallelQuery<TSource>);
    }

    [Pure]
    public static OrderedParallelQuery<TSource> ThenBy<TSource, TKey>(
      OrderedParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<System.Linq.OrderedParallelQuery<TSource>>() != null);

      return default(OrderedParallelQuery<TSource>);
    }

    [Pure]
    public static OrderedParallelQuery<TSource> ThenByDescending<TSource, TKey>(
      OrderedParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<System.Linq.OrderedParallelQuery<TSource>>() != null);

      return default(OrderedParallelQuery<TSource>);
    }

    [Pure]
    public static OrderedParallelQuery<TSource> ThenByDescending<TSource, TKey>(
      OrderedParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<System.Linq.OrderedParallelQuery<TSource>>() != null);

      return default(OrderedParallelQuery<TSource>);
    }

    [Pure]
    public static TSource[] ToArray<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      return default(TSource[]);
    }

    [Pure]
    public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<System.Collections.Generic.Dictionary<TKey, TSource>>() != null);

      return default(Dictionary<TKey, TSource>);
    }

    [Pure]
    public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector, 
      IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<System.Collections.Generic.Dictionary<TKey, TSource>>() != null);

      return default(Dictionary<TKey, TSource>);
    }

    [Pure]
    public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector, 
      Func<TSource, TElement> elementSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);
      Contract.Ensures(Contract.Result<System.Collections.Generic.Dictionary<TKey, TElement>>() != null);

      return default(Dictionary<TKey, TElement>);
    }

    [Pure]
    public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector, 
      Func<TSource, TElement> elementSelector, 
      IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);
      Contract.Ensures(Contract.Result<System.Collections.Generic.Dictionary<TKey, TElement>>() != null);

      return default(Dictionary<TKey, TElement>);
    }

    [Pure]
    public static List<TSource> ToList<TSource>(ParallelQuery<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<System.Collections.Generic.List<TSource>>() != null);

      return default(List<TSource>);
    }

    [Pure]
    public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector, 
      Func<TSource, TElement> elementSelector, 
      IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ILookup<TKey, TElement>>() != null);

      return default(ILookup<TKey, TElement>);
    }

    [Pure]
    public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector, 
      Func<TSource, TElement> elementSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ILookup<TKey, TElement>>() != null);

      return default(ILookup<TKey, TElement>);
    }

    [Pure]
    public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector, 
      IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ILookup<TKey, TSource>>() != null);

      return default(ILookup<TKey, TSource>);
    }

    [Pure]
    public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(
      ParallelQuery<TSource> source, 
      Func<TSource, TKey> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ILookup<TKey, TSource>>() != null);

      return default(ILookup<TKey, TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> Union<TSource>(ParallelQuery<TSource> first, ParallelQuery<TSource> second)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> Union<TSource>(
      ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> Union<TSource>(
      ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> Union<TSource>(ParallelQuery<TSource> first, IEnumerable<TSource> second)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> Where<TSource>(
      ParallelQuery<TSource> source, Func<TSource, int, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> Where<TSource>(ParallelQuery<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> WithCancellation<TSource>(
      ParallelQuery<TSource> source, System.Threading.CancellationToken cancellationToken)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> WithDegreeOfParallelism<TSource>(
      ParallelQuery<TSource> source, int degreeOfParallelism)
    {
      Contract.Requires(source != null);
      Contract.Requires(degreeOfParallelism >= 0);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> WithExecutionMode<TSource>(ParallelQuery<TSource> source, ParallelExecutionMode executionMode)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TSource> WithMergeOptions<TSource>(ParallelQuery<TSource> source, ParallelMergeOptions mergeOptions)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TSource>>() != null);

      return default(ParallelQuery<TSource>);
    }

    [Pure]
    public static ParallelQuery<TResult> Zip<TFirst, TSecond, TResult>(
      ParallelQuery<TFirst> first, ParallelQuery<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<System.Linq.ParallelQuery<TResult>>() != null);

      return default(ParallelQuery<TResult>);
    }

    [Pure]
    public static ParallelQuery<TResult> Zip<TFirst, TSecond, TResult>(
      ParallelQuery<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
    {
      Contract.Ensures(false);

      return default(ParallelQuery<TResult>);
    }
    #endregion
  }
#endif
}

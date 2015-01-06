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

// File System.Linq.Enumerable.cs
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
  static public partial class Enumerable
  {
    #region Methods and constructors
    public static TAccumulate Aggregate<TSource, TAccumulate>(IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
    {
      return default(TAccumulate);
    }

    public static TSource Aggregate<TSource>(IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
    {
      return default(TSource);
    }

    public static TResult Aggregate<TSource, TAccumulate, TResult>(IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
    {
      return default(TResult);
    }

    public static bool All<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      return default(bool);
    }

    public static bool Any<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      return default(bool);
    }

    public static bool Any<TSource>(IEnumerable<TSource> source)
    {
      return default(bool);
    }

    public static IEnumerable<TSource> AsEnumerable<TSource>(IEnumerable<TSource> source)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.IEnumerable<TSource>>() == source);

      return default(IEnumerable<TSource>);
    }

    public static Nullable<double> Average(IEnumerable<Nullable<long>> source)
    {
      return default(Nullable<double>);
    }

    public static float Average(IEnumerable<float> source)
    {
      return default(float);
    }

    public static Nullable<float> Average(IEnumerable<Nullable<float>> source)
    {
      return default(Nullable<float>);
    }

    public static double Average(IEnumerable<int> source)
    {
      Contract.Ensures(-9223372036854775808 <= Contract.Result<double>());
      Contract.Ensures(Contract.Result<double>() <= 9223372036854775807);

      return default(double);
    }

    public static Nullable<double> Average(IEnumerable<Nullable<int>> source)
    {
      return default(Nullable<double>);
    }

    public static double Average(IEnumerable<long> source)
    {
      Contract.Ensures(-9223372036854775808 <= Contract.Result<double>());
      Contract.Ensures(Contract.Result<double>() <= 9223372036854775807);

      return default(double);
    }

    public static double Average(IEnumerable<double> source)
    {
      return default(double);
    }

    public static double Average<TSource>(IEnumerable<TSource> source, Func<TSource, double> selector)
    {
      Contract.Requires(selector != null);
      Contract.Requires(source != null);

      return default(double);
    }

    public static Nullable<float> Average<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<float>> selector)
    {
      Contract.Requires(selector != null);
      Contract.Requires(source != null);

      return default(Nullable<float>);
    }

    public static float Average<TSource>(IEnumerable<TSource> source, Func<TSource, float> selector)
    {
      Contract.Requires(selector != null);
      Contract.Requires(source != null);

      return default(float);
    }

    public static Nullable<Decimal> Average<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<Decimal>> selector)
    {
      Contract.Requires(selector != null);
      Contract.Requires(source != null);

      return default(Nullable<Decimal>);
    }

    public static Decimal Average<TSource>(IEnumerable<TSource> source, Func<TSource, Decimal> selector)
    {
      Contract.Requires(selector != null);
      Contract.Requires(source != null);

      return default(Decimal);
    }

    public static Nullable<double> Average<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<double>> selector)
    {
      Contract.Requires(selector != null);
      Contract.Requires(source != null);

      return default(Nullable<double>);
    }

    public static Nullable<double> Average<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<long>> selector)
    {
      Contract.Requires(selector != null);
      Contract.Requires(source != null);

      return default(Nullable<double>);
    }

    public static Nullable<Decimal> Average(IEnumerable<Nullable<Decimal>> source)
    {
      return default(Nullable<Decimal>);
    }

    public static double Average<TSource>(IEnumerable<TSource> source, Func<TSource, int> selector)
    {
      Contract.Requires(selector != null);
      Contract.Requires(source != null);

      return default(double);
    }

    public static Nullable<double> Average(IEnumerable<Nullable<double>> source)
    {
      return default(Nullable<double>);
    }

    public static Decimal Average(IEnumerable<Decimal> source)
    {
      return default(Decimal);
    }

    public static double Average<TSource>(IEnumerable<TSource> source, Func<TSource, long> selector)
    {
      Contract.Requires(selector != null);
      Contract.Requires(source != null);

      return default(double);
    }

    public static Nullable<double> Average<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<int>> selector)
    {
      Contract.Requires(selector != null);
      Contract.Requires(source != null);

      return default(Nullable<double>);
    }

    public static IEnumerable<TResult> Cast<TResult>(System.Collections.IEnumerable source)
    {
      return default(IEnumerable<TResult>);
    }

    public static IEnumerable<TSource> Concat<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
    {
      return default(IEnumerable<TSource>);
    }

    public static bool Contains<TSource>(IEnumerable<TSource> source, TSource value)
    {
      return default(bool);
    }

    public static bool Contains<TSource>(IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
    {
      return default(bool);
    }

    public static int Count<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      return default(int);
    }

    public static int Count<TSource>(IEnumerable<TSource> source)
    {
      return default(int);
    }

    public static IEnumerable<TSource> DefaultIfEmpty<TSource>(IEnumerable<TSource> source)
    {
      return default(IEnumerable<TSource>);
    }

    public static IEnumerable<TSource> DefaultIfEmpty<TSource>(IEnumerable<TSource> source, TSource defaultValue)
    {
      return default(IEnumerable<TSource>);
    }

    public static IEnumerable<TSource> Distinct<TSource>(IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
    {
      return default(IEnumerable<TSource>);
    }

    public static IEnumerable<TSource> Distinct<TSource>(IEnumerable<TSource> source)
    {
      return default(IEnumerable<TSource>);
    }

    public static TSource ElementAt<TSource>(IEnumerable<TSource> source, int index)
    {
      return default(TSource);
    }

    public static TSource ElementAtOrDefault<TSource>(IEnumerable<TSource> source, int index)
    {
      return default(TSource);
    }

    public static IEnumerable<TResult> Empty<TResult>()
    {
      return default(IEnumerable<TResult>);
    }

    public static IEnumerable<TSource> Except<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
    {
      return default(IEnumerable<TSource>);
    }

    public static IEnumerable<TSource> Except<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
    {
      return default(IEnumerable<TSource>);
    }

    public static TSource First<TSource>(IEnumerable<TSource> source)
    {
      return default(TSource);
    }

    public static TSource First<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      return default(TSource);
    }

    public static TSource FirstOrDefault<TSource>(IEnumerable<TSource> source)
    {
      return default(TSource);
    }

    public static TSource FirstOrDefault<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      return default(TSource);
    }

    public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
    {
      return default(IEnumerable<IGrouping<TKey, TElement>>);
    }

    public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
    {
      return default(IEnumerable<TResult>);
    }

    public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
    {
      return default(IEnumerable<TResult>);
    }

    public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
    {
      return default(IEnumerable<IGrouping<TKey, TSource>>);
    }

    public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
      return default(IEnumerable<IGrouping<TKey, TSource>>);
    }

    public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
    {
      return default(IEnumerable<TResult>);
    }

    public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
    {
      return default(IEnumerable<TResult>);
    }

    public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
    {
      return default(IEnumerable<IGrouping<TKey, TElement>>);
    }

    public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
    {
      return default(IEnumerable<TResult>);
    }

    public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
    {
      return default(IEnumerable<TResult>);
    }

    public static IEnumerable<TSource> Intersect<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
    {
      return default(IEnumerable<TSource>);
    }

    public static IEnumerable<TSource> Intersect<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
    {
      return default(IEnumerable<TSource>);
    }

    public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
    {
      return default(IEnumerable<TResult>);
    }

    public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
    {
      return default(IEnumerable<TResult>);
    }

    public static TSource Last<TSource>(IEnumerable<TSource> source)
    {
      return default(TSource);
    }

    public static TSource Last<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      return default(TSource);
    }

    public static TSource LastOrDefault<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      return default(TSource);
    }

    public static TSource LastOrDefault<TSource>(IEnumerable<TSource> source)
    {
      return default(TSource);
    }

    public static long LongCount<TSource>(IEnumerable<TSource> source)
    {
      Contract.Ensures(0 <= Contract.Result<long>());

      return default(long);
    }

    public static long LongCount<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      return default(long);
    }

    public static Nullable<double> Max<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<double>> selector)
    {
      return default(Nullable<double>);
    }

    public static double Max<TSource>(IEnumerable<TSource> source, Func<TSource, double> selector)
    {
      return default(double);
    }

    public static float Max<TSource>(IEnumerable<TSource> source, Func<TSource, float> selector)
    {
      return default(float);
    }

    public static Nullable<float> Max<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<float>> selector)
    {
      return default(Nullable<float>);
    }

    public static Nullable<long> Max<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<long>> selector)
    {
      return default(Nullable<long>);
    }

    public static TResult Max<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
      return default(TResult);
    }

    public static Nullable<Decimal> Max<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<Decimal>> selector)
    {
      return default(Nullable<Decimal>);
    }

    public static int Max(IEnumerable<int> source)
    {
      return default(int);
    }

    public static Decimal Max<TSource>(IEnumerable<TSource> source, Func<TSource, Decimal> selector)
    {
      return default(Decimal);
    }

    public static Nullable<int> Max(IEnumerable<Nullable<int>> source)
    {
      return default(Nullable<int>);
    }

    public static long Max<TSource>(IEnumerable<TSource> source, Func<TSource, long> selector)
    {
      return default(long);
    }

    public static Nullable<double> Max(IEnumerable<Nullable<double>> source)
    {
      return default(Nullable<double>);
    }

    public static float Max(IEnumerable<float> source)
    {
      return default(float);
    }

    public static double Max(IEnumerable<double> source)
    {
      return default(double);
    }

    public static long Max(IEnumerable<long> source)
    {
      return default(long);
    }

    public static Nullable<long> Max(IEnumerable<Nullable<long>> source)
    {
      return default(Nullable<long>);
    }

    public static Nullable<float> Max(IEnumerable<Nullable<float>> source)
    {
      return default(Nullable<float>);
    }

    public static int Max<TSource>(IEnumerable<TSource> source, Func<TSource, int> selector)
    {
      return default(int);
    }

    public static Nullable<int> Max<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<int>> selector)
    {
      return default(Nullable<int>);
    }

    public static TSource Max<TSource>(IEnumerable<TSource> source)
    {
      return default(TSource);
    }

    public static Decimal Max(IEnumerable<Decimal> source)
    {
      return default(Decimal);
    }

    public static Nullable<Decimal> Max(IEnumerable<Nullable<Decimal>> source)
    {
      return default(Nullable<Decimal>);
    }

    public static Decimal Min(IEnumerable<Decimal> source)
    {
      return default(Decimal);
    }

    public static double Min(IEnumerable<double> source)
    {
      return default(double);
    }

    public static Nullable<double> Min(IEnumerable<Nullable<double>> source)
    {
      return default(Nullable<double>);
    }

    public static Nullable<float> Min(IEnumerable<Nullable<float>> source)
    {
      return default(Nullable<float>);
    }

    public static Nullable<long> Min<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<long>> selector)
    {
      return default(Nullable<long>);
    }

    public static float Min(IEnumerable<float> source)
    {
      return default(float);
    }

    public static TSource Min<TSource>(IEnumerable<TSource> source)
    {
      return default(TSource);
    }

    public static Nullable<Decimal> Min(IEnumerable<Nullable<Decimal>> source)
    {
      return default(Nullable<Decimal>);
    }

    public static int Min<TSource>(IEnumerable<TSource> source, Func<TSource, int> selector)
    {
      return default(int);
    }

    public static long Min<TSource>(IEnumerable<TSource> source, Func<TSource, long> selector)
    {
      return default(long);
    }

    public static Nullable<int> Min<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<int>> selector)
    {
      return default(Nullable<int>);
    }

    public static Nullable<int> Min(IEnumerable<Nullable<int>> source)
    {
      return default(Nullable<int>);
    }

    public static Nullable<double> Min<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<double>> selector)
    {
      return default(Nullable<double>);
    }

    public static Decimal Min<TSource>(IEnumerable<TSource> source, Func<TSource, Decimal> selector)
    {
      return default(Decimal);
    }

    public static int Min(IEnumerable<int> source)
    {
      return default(int);
    }

    public static Nullable<Decimal> Min<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<Decimal>> selector)
    {
      return default(Nullable<Decimal>);
    }

    public static Nullable<long> Min(IEnumerable<Nullable<long>> source)
    {
      return default(Nullable<long>);
    }

    public static float Min<TSource>(IEnumerable<TSource> source, Func<TSource, float> selector)
    {
      return default(float);
    }

    public static long Min(IEnumerable<long> source)
    {
      return default(long);
    }

    public static double Min<TSource>(IEnumerable<TSource> source, Func<TSource, double> selector)
    {
      return default(double);
    }

    public static Nullable<float> Min<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<float>> selector)
    {
      return default(Nullable<float>);
    }

    public static TResult Min<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
      return default(TResult);
    }

    public static IEnumerable<TResult> OfType<TResult>(System.Collections.IEnumerable source)
    {
      return default(IEnumerable<TResult>);
    }

    public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
      return default(IOrderedEnumerable<TSource>);
    }

    public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
    {
      return default(IOrderedEnumerable<TSource>);
    }

    public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
      return default(IOrderedEnumerable<TSource>);
    }

    public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
    {
      return default(IOrderedEnumerable<TSource>);
    }

    public static IEnumerable<int> Range(int start, int count)
    {
      return default(IEnumerable<int>);
    }

    public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
    {
      return default(IEnumerable<TResult>);
    }

    public static IEnumerable<TSource> Reverse<TSource>(IEnumerable<TSource> source)
    {
      return default(IEnumerable<TSource>);
    }

    public static IEnumerable<TResult> Select<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
    {
      return default(IEnumerable<TResult>);
    }

    public static IEnumerable<TResult> Select<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
      return default(IEnumerable<TResult>);
    }

    public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
    {
      return default(IEnumerable<TResult>);
    }

    public static IEnumerable<TResult> SelectMany<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
    {
      return default(IEnumerable<TResult>);
    }

    public static IEnumerable<TResult> SelectMany<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
    {
      return default(IEnumerable<TResult>);
    }

    public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
    {
      return default(IEnumerable<TResult>);
    }

    public static bool SequenceEqual<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
    {
      return default(bool);
    }

    public static bool SequenceEqual<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
    {
      return default(bool);
    }

    public static TSource Single<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      return default(TSource);
    }

    public static TSource Single<TSource>(IEnumerable<TSource> source)
    {
      return default(TSource);
    }

    public static TSource SingleOrDefault<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      return default(TSource);
    }

    public static TSource SingleOrDefault<TSource>(IEnumerable<TSource> source)
    {
      return default(TSource);
    }

    public static IEnumerable<TSource> Skip<TSource>(IEnumerable<TSource> source, int count)
    {
      return default(IEnumerable<TSource>);
    }

    public static IEnumerable<TSource> SkipWhile<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
    {
      return default(IEnumerable<TSource>);
    }

    public static IEnumerable<TSource> SkipWhile<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      return default(IEnumerable<TSource>);
    }

    public static double Sum(IEnumerable<double> source)
    {
      return default(double);
    }

    public static Nullable<float> Sum(IEnumerable<Nullable<float>> source)
    {
      return default(Nullable<float>);
    }

    public static Nullable<double> Sum(IEnumerable<Nullable<double>> source)
    {
      return default(Nullable<double>);
    }

    public static Nullable<Decimal> Sum(IEnumerable<Nullable<Decimal>> source)
    {
      return default(Nullable<Decimal>);
    }

    public static Decimal Sum(IEnumerable<Decimal> source)
    {
      return default(Decimal);
    }

    public static Nullable<int> Sum(IEnumerable<Nullable<int>> source)
    {
      return default(Nullable<int>);
    }

    public static int Sum(IEnumerable<int> source)
    {
      return default(int);
    }

    public static long Sum(IEnumerable<long> source)
    {
      return default(long);
    }

    public static float Sum(IEnumerable<float> source)
    {
      return default(float);
    }

    public static Nullable<long> Sum(IEnumerable<Nullable<long>> source)
    {
      return default(Nullable<long>);
    }

    public static double Sum<TSource>(IEnumerable<TSource> source, Func<TSource, double> selector)
    {
      Contract.Requires(selector != null);

      return default(double);
    }

    public static Nullable<float> Sum<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<float>> selector)
    {
      Contract.Requires(selector != null);

      return default(Nullable<float>);
    }

    public static Nullable<double> Sum<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<double>> selector)
    {
      Contract.Requires(selector != null);

      return default(Nullable<double>);
    }

    public static Nullable<Decimal> Sum<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<Decimal>> selector)
    {
      return default(Nullable<Decimal>);
    }

    public static Decimal Sum<TSource>(IEnumerable<TSource> source, Func<TSource, Decimal> selector)
    {
      Contract.Requires(selector != null);

      return default(Decimal);
    }

    public static Nullable<int> Sum<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<int>> selector)
    {
      Contract.Requires(selector != null);

      return default(Nullable<int>);
    }

    public static int Sum<TSource>(IEnumerable<TSource> source, Func<TSource, int> selector)
    {
      Contract.Requires(selector != null);

      return default(int);
    }

    public static long Sum<TSource>(IEnumerable<TSource> source, Func<TSource, long> selector)
    {
      return default(long);
    }

    public static float Sum<TSource>(IEnumerable<TSource> source, Func<TSource, float> selector)
    {
      Contract.Requires(selector != null);

      return default(float);
    }

    public static Nullable<long> Sum<TSource>(IEnumerable<TSource> source, Func<TSource, Nullable<long>> selector)
    {
      Contract.Requires(selector != null);

      return default(Nullable<long>);
    }

    public static IEnumerable<TSource> Take<TSource>(IEnumerable<TSource> source, int count)
    {
      return default(IEnumerable<TSource>);
    }

    public static IEnumerable<TSource> TakeWhile<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
    {
      return default(IEnumerable<TSource>);
    }

    public static IEnumerable<TSource> TakeWhile<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      return default(IEnumerable<TSource>);
    }

    public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
      return default(IOrderedEnumerable<TSource>);
    }

    public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
    {
      return default(IOrderedEnumerable<TSource>);
    }

    public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
      return default(IOrderedEnumerable<TSource>);
    }

    public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
    {
      return default(IOrderedEnumerable<TSource>);
    }

    public static TSource[] ToArray<TSource>(IEnumerable<TSource> source)
    {
      return default(TSource[]);
    }

    public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
    {
      return default(Dictionary<TKey, TSource>);
    }

    public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
    {
      return default(Dictionary<TKey, TElement>);
    }

    public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
      return default(Dictionary<TKey, TSource>);
    }

    public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
    {
      return default(Dictionary<TKey, TElement>);
    }

    public static List<TSource> ToList<TSource>(IEnumerable<TSource> source)
    {
      return default(List<TSource>);
    }

    public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
      return default(ILookup<TKey, TSource>);
    }

    public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
    {
      return default(ILookup<TKey, TSource>);
    }

    public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
    {
      return default(ILookup<TKey, TElement>);
    }

    public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
    {
      return default(ILookup<TKey, TElement>);
    }

    public static IEnumerable<TSource> Union<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
    {
      return default(IEnumerable<TSource>);
    }

    public static IEnumerable<TSource> Union<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
    {
      return default(IEnumerable<TSource>);
    }

    public static IEnumerable<TSource> Where<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
    {
      return default(IEnumerable<TSource>);
    }

    public static IEnumerable<TSource> Where<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      return default(IEnumerable<TSource>);
    }

    public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
    {
      return default(IEnumerable<TResult>);
    }
    #endregion
  }
}

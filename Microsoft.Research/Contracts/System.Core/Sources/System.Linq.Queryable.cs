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

// File System.Linq.Queryable.cs
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
  static public partial class Queryable
  {
    #region Methods and constructors
    public static TSource Aggregate<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TSource, TSource>> func)
    {
      return default(TSource);
    }

    public static TAccumulate Aggregate<TSource, TAccumulate>(IQueryable<TSource> source, TAccumulate seed, System.Linq.Expressions.Expression<Func<TAccumulate, TSource, TAccumulate>> func)
    {
      return default(TAccumulate);
    }

    public static TResult Aggregate<TSource, TAccumulate, TResult>(IQueryable<TSource> source, TAccumulate seed, System.Linq.Expressions.Expression<Func<TAccumulate, TSource, TAccumulate>> func, System.Linq.Expressions.Expression<Func<TAccumulate, TResult>> selector)
    {
      return default(TResult);
    }

    public static bool All<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, bool>> predicate)
    {
      return default(bool);
    }

    public static bool Any<TSource>(IQueryable<TSource> source)
    {
      return default(bool);
    }

    public static bool Any<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, bool>> predicate)
    {
      return default(bool);
    }

    public static IQueryable<TElement> AsQueryable<TElement>(IEnumerable<TElement> source)
    {
      return default(IQueryable<TElement>);
    }

    public static IQueryable AsQueryable(System.Collections.IEnumerable source)
    {
      return default(IQueryable);
    }

    public static Nullable<double> Average(IQueryable<Nullable<double>> source)
    {
      return default(Nullable<double>);
    }

    public static double Average(IQueryable<double> source)
    {
      return default(double);
    }

    public static Decimal Average(IQueryable<Decimal> source)
    {
      return default(Decimal);
    }

    public static double Average<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, int>> selector)
    {
      return default(double);
    }

    public static Nullable<Decimal> Average(IQueryable<Nullable<Decimal>> source)
    {
      return default(Nullable<Decimal>);
    }

    public static Nullable<double> Average(IQueryable<Nullable<int>> source)
    {
      return default(Nullable<double>);
    }

    public static double Average(IQueryable<int> source)
    {
      return default(double);
    }

    public static double Average(IQueryable<long> source)
    {
      return default(double);
    }

    public static float Average(IQueryable<float> source)
    {
      return default(float);
    }

    public static Nullable<double> Average(IQueryable<Nullable<long>> source)
    {
      return default(Nullable<double>);
    }

    public static Nullable<double> Average<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, Nullable<double>>> selector)
    {
      return default(Nullable<double>);
    }

    public static double Average<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, double>> selector)
    {
      return default(double);
    }

    public static Decimal Average<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, Decimal>> selector)
    {
      return default(Decimal);
    }

    public static Nullable<float> Average(IQueryable<Nullable<float>> source)
    {
      return default(Nullable<float>);
    }

    public static Nullable<Decimal> Average<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, Nullable<Decimal>>> selector)
    {
      return default(Nullable<Decimal>);
    }

    public static Nullable<double> Average<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, Nullable<int>>> selector)
    {
      return default(Nullable<double>);
    }

    public static float Average<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, float>> selector)
    {
      return default(float);
    }

    public static double Average<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, long>> selector)
    {
      return default(double);
    }

    public static Nullable<float> Average<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, Nullable<float>>> selector)
    {
      return default(Nullable<float>);
    }

    public static Nullable<double> Average<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, Nullable<long>>> selector)
    {
      return default(Nullable<double>);
    }

    public static IQueryable<TResult> Cast<TResult>(IQueryable source)
    {
      return default(IQueryable<TResult>);
    }

    public static IQueryable<TSource> Concat<TSource>(IQueryable<TSource> source1, IEnumerable<TSource> source2)
    {
      return default(IQueryable<TSource>);
    }

    public static bool Contains<TSource>(IQueryable<TSource> source, TSource item, IEqualityComparer<TSource> comparer)
    {
      return default(bool);
    }

    public static bool Contains<TSource>(IQueryable<TSource> source, TSource item)
    {
      return default(bool);
    }

    public static int Count<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, bool>> predicate)
    {
      return default(int);
    }

    public static int Count<TSource>(IQueryable<TSource> source)
    {
      return default(int);
    }

    public static IQueryable<TSource> DefaultIfEmpty<TSource>(IQueryable<TSource> source)
    {
      return default(IQueryable<TSource>);
    }

    public static IQueryable<TSource> DefaultIfEmpty<TSource>(IQueryable<TSource> source, TSource defaultValue)
    {
      return default(IQueryable<TSource>);
    }

    public static IQueryable<TSource> Distinct<TSource>(IQueryable<TSource> source)
    {
      return default(IQueryable<TSource>);
    }

    public static IQueryable<TSource> Distinct<TSource>(IQueryable<TSource> source, IEqualityComparer<TSource> comparer)
    {
      return default(IQueryable<TSource>);
    }

    public static TSource ElementAt<TSource>(IQueryable<TSource> source, int index)
    {
      return default(TSource);
    }

    public static TSource ElementAtOrDefault<TSource>(IQueryable<TSource> source, int index)
    {
      return default(TSource);
    }

    public static IQueryable<TSource> Except<TSource>(IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
    {
      return default(IQueryable<TSource>);
    }

    public static IQueryable<TSource> Except<TSource>(IQueryable<TSource> source1, IEnumerable<TSource> source2)
    {
      return default(IQueryable<TSource>);
    }

    public static TSource First<TSource>(IQueryable<TSource> source)
    {
      return default(TSource);
    }

    public static TSource First<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, bool>> predicate)
    {
      return default(TSource);
    }

    public static TSource FirstOrDefault<TSource>(IQueryable<TSource> source)
    {
      return default(TSource);
    }

    public static TSource FirstOrDefault<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, bool>> predicate)
    {
      return default(TSource);
    }

    public static IQueryable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector, IEqualityComparer<TKey> comparer)
    {
      return default(IQueryable<IGrouping<TKey, TSource>>);
    }

    public static IQueryable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector)
    {
      return default(IQueryable<IGrouping<TKey, TSource>>);
    }

    public static IQueryable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector, System.Linq.Expressions.Expression<Func<TSource, TElement>> elementSelector)
    {
      return default(IQueryable<IGrouping<TKey, TElement>>);
    }

    public static IQueryable<TResult> GroupBy<TSource, TKey, TResult>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector, System.Linq.Expressions.Expression<Func<TKey, IEnumerable<TSource>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
    {
      return default(IQueryable<TResult>);
    }

    public static IQueryable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector, System.Linq.Expressions.Expression<Func<TSource, TElement>> elementSelector, IEqualityComparer<TKey> comparer)
    {
      return default(IQueryable<IGrouping<TKey, TElement>>);
    }

    public static IQueryable<TResult> GroupBy<TSource, TKey, TElement, TResult>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector, System.Linq.Expressions.Expression<Func<TSource, TElement>> elementSelector, System.Linq.Expressions.Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector)
    {
      return default(IQueryable<TResult>);
    }

    public static IQueryable<TResult> GroupBy<TSource, TKey, TResult>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector, System.Linq.Expressions.Expression<Func<TKey, IEnumerable<TSource>, TResult>> resultSelector)
    {
      return default(IQueryable<TResult>);
    }

    public static IQueryable<TResult> GroupBy<TSource, TKey, TElement, TResult>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector, System.Linq.Expressions.Expression<Func<TSource, TElement>> elementSelector, System.Linq.Expressions.Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
    {
      return default(IQueryable<TResult>);
    }

    public static IQueryable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(IQueryable<TOuter> outer, IEnumerable<TInner> inner, System.Linq.Expressions.Expression<Func<TOuter, TKey>> outerKeySelector, System.Linq.Expressions.Expression<Func<TInner, TKey>> innerKeySelector, System.Linq.Expressions.Expression<Func<TOuter, IEnumerable<TInner>, TResult>> resultSelector)
    {
      return default(IQueryable<TResult>);
    }

    public static IQueryable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(IQueryable<TOuter> outer, IEnumerable<TInner> inner, System.Linq.Expressions.Expression<Func<TOuter, TKey>> outerKeySelector, System.Linq.Expressions.Expression<Func<TInner, TKey>> innerKeySelector, System.Linq.Expressions.Expression<Func<TOuter, IEnumerable<TInner>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
    {
      return default(IQueryable<TResult>);
    }

    public static IQueryable<TSource> Intersect<TSource>(IQueryable<TSource> source1, IEnumerable<TSource> source2)
    {
      return default(IQueryable<TSource>);
    }

    public static IQueryable<TSource> Intersect<TSource>(IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
    {
      return default(IQueryable<TSource>);
    }

    public static IQueryable<TResult> Join<TOuter, TInner, TKey, TResult>(IQueryable<TOuter> outer, IEnumerable<TInner> inner, System.Linq.Expressions.Expression<Func<TOuter, TKey>> outerKeySelector, System.Linq.Expressions.Expression<Func<TInner, TKey>> innerKeySelector, System.Linq.Expressions.Expression<Func<TOuter, TInner, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
    {
      return default(IQueryable<TResult>);
    }

    public static IQueryable<TResult> Join<TOuter, TInner, TKey, TResult>(IQueryable<TOuter> outer, IEnumerable<TInner> inner, System.Linq.Expressions.Expression<Func<TOuter, TKey>> outerKeySelector, System.Linq.Expressions.Expression<Func<TInner, TKey>> innerKeySelector, System.Linq.Expressions.Expression<Func<TOuter, TInner, TResult>> resultSelector)
    {
      return default(IQueryable<TResult>);
    }

    public static TSource Last<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, bool>> predicate)
    {
      return default(TSource);
    }

    public static TSource Last<TSource>(IQueryable<TSource> source)
    {
      return default(TSource);
    }

    public static TSource LastOrDefault<TSource>(IQueryable<TSource> source)
    {
      return default(TSource);
    }

    public static TSource LastOrDefault<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, bool>> predicate)
    {
      return default(TSource);
    }

    public static long LongCount<TSource>(IQueryable<TSource> source)
    {
      return default(long);
    }

    public static long LongCount<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, bool>> predicate)
    {
      return default(long);
    }

    public static TSource Max<TSource>(IQueryable<TSource> source)
    {
      return default(TSource);
    }

    public static TResult Max<TSource, TResult>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TResult>> selector)
    {
      return default(TResult);
    }

    public static TResult Min<TSource, TResult>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TResult>> selector)
    {
      return default(TResult);
    }

    public static TSource Min<TSource>(IQueryable<TSource> source)
    {
      return default(TSource);
    }

    public static IQueryable<TResult> OfType<TResult>(IQueryable source)
    {
      return default(IQueryable<TResult>);
    }

    public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
    {
      return default(IOrderedQueryable<TSource>);
    }

    public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector)
    {
      return default(IOrderedQueryable<TSource>);
    }

    public static IOrderedQueryable<TSource> OrderByDescending<TSource, TKey>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector)
    {
      return default(IOrderedQueryable<TSource>);
    }

    public static IOrderedQueryable<TSource> OrderByDescending<TSource, TKey>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
    {
      return default(IOrderedQueryable<TSource>);
    }

    public static IQueryable<TSource> Reverse<TSource>(IQueryable<TSource> source)
    {
      return default(IQueryable<TSource>);
    }

    public static IQueryable<TResult> Select<TSource, TResult>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, int, TResult>> selector)
    {
      return default(IQueryable<TResult>);
    }

    public static IQueryable<TResult> Select<TSource, TResult>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TResult>> selector)
    {
      return default(IQueryable<TResult>);
    }

    public static IQueryable<TResult> SelectMany<TSource, TResult>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, int, IEnumerable<TResult>>> selector)
    {
      return default(IQueryable<TResult>);
    }

    public static IQueryable<TResult> SelectMany<TSource, TCollection, TResult>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, int, IEnumerable<TCollection>>> collectionSelector, System.Linq.Expressions.Expression<Func<TSource, TCollection, TResult>> resultSelector)
    {
      return default(IQueryable<TResult>);
    }

    public static IQueryable<TResult> SelectMany<TSource, TCollection, TResult>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, IEnumerable<TCollection>>> collectionSelector, System.Linq.Expressions.Expression<Func<TSource, TCollection, TResult>> resultSelector)
    {
      return default(IQueryable<TResult>);
    }

    public static IQueryable<TResult> SelectMany<TSource, TResult>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, IEnumerable<TResult>>> selector)
    {
      return default(IQueryable<TResult>);
    }

    public static bool SequenceEqual<TSource>(IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
    {
      return default(bool);
    }

    public static bool SequenceEqual<TSource>(IQueryable<TSource> source1, IEnumerable<TSource> source2)
    {
      return default(bool);
    }

    public static TSource Single<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, bool>> predicate)
    {
      return default(TSource);
    }

    public static TSource Single<TSource>(IQueryable<TSource> source)
    {
      return default(TSource);
    }

    public static TSource SingleOrDefault<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, bool>> predicate)
    {
      return default(TSource);
    }

    public static TSource SingleOrDefault<TSource>(IQueryable<TSource> source)
    {
      return default(TSource);
    }

    public static IQueryable<TSource> Skip<TSource>(IQueryable<TSource> source, int count)
    {
      return default(IQueryable<TSource>);
    }

    public static IQueryable<TSource> SkipWhile<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, bool>> predicate)
    {
      return default(IQueryable<TSource>);
    }

    public static IQueryable<TSource> SkipWhile<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, int, bool>> predicate)
    {
      return default(IQueryable<TSource>);
    }

    public static Nullable<double> Sum<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, Nullable<double>>> selector)
    {
      return default(Nullable<double>);
    }

    public static double Sum<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, double>> selector)
    {
      return default(double);
    }

    public static Decimal Sum<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, Decimal>> selector)
    {
      return default(Decimal);
    }

    public static Nullable<int> Sum(IQueryable<Nullable<int>> source)
    {
      return default(Nullable<int>);
    }

    public static Nullable<Decimal> Sum<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, Nullable<Decimal>>> selector)
    {
      return default(Nullable<Decimal>);
    }

    public static Nullable<double> Sum(IQueryable<Nullable<double>> source)
    {
      return default(Nullable<double>);
    }

    public static double Sum(IQueryable<double> source)
    {
      return default(double);
    }

    public static Nullable<Decimal> Sum(IQueryable<Nullable<Decimal>> source)
    {
      return default(Nullable<Decimal>);
    }

    public static Decimal Sum(IQueryable<Decimal> source)
    {
      return default(Decimal);
    }

    public static Nullable<float> Sum(IQueryable<Nullable<float>> source)
    {
      return default(Nullable<float>);
    }

    public static long Sum(IQueryable<long> source)
    {
      return default(long);
    }

    public static int Sum(IQueryable<int> source)
    {
      return default(int);
    }

    public static float Sum(IQueryable<float> source)
    {
      return default(float);
    }

    public static Nullable<long> Sum(IQueryable<Nullable<long>> source)
    {
      return default(Nullable<long>);
    }

    public static int Sum<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, int>> selector)
    {
      return default(int);
    }

    public static long Sum<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, long>> selector)
    {
      return default(long);
    }

    public static Nullable<long> Sum<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, Nullable<long>>> selector)
    {
      return default(Nullable<long>);
    }

    public static float Sum<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, float>> selector)
    {
      return default(float);
    }

    public static Nullable<float> Sum<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, Nullable<float>>> selector)
    {
      return default(Nullable<float>);
    }

    public static Nullable<int> Sum<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, Nullable<int>>> selector)
    {
      return default(Nullable<int>);
    }

    public static IQueryable<TSource> Take<TSource>(IQueryable<TSource> source, int count)
    {
      return default(IQueryable<TSource>);
    }

    public static IQueryable<TSource> TakeWhile<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, bool>> predicate)
    {
      return default(IQueryable<TSource>);
    }

    public static IQueryable<TSource> TakeWhile<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, int, bool>> predicate)
    {
      return default(IQueryable<TSource>);
    }

    public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(IOrderedQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
    {
      return default(IOrderedQueryable<TSource>);
    }

    public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(IOrderedQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector)
    {
      return default(IOrderedQueryable<TSource>);
    }

    public static IOrderedQueryable<TSource> ThenByDescending<TSource, TKey>(IOrderedQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector)
    {
      return default(IOrderedQueryable<TSource>);
    }

    public static IOrderedQueryable<TSource> ThenByDescending<TSource, TKey>(IOrderedQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
    {
      return default(IOrderedQueryable<TSource>);
    }

    public static IQueryable<TSource> Union<TSource>(IQueryable<TSource> source1, IEnumerable<TSource> source2)
    {
      return default(IQueryable<TSource>);
    }

    public static IQueryable<TSource> Union<TSource>(IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
    {
      return default(IQueryable<TSource>);
    }

    public static IQueryable<TSource> Where<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, bool>> predicate)
    {
      return default(IQueryable<TSource>);
    }

    public static IQueryable<TSource> Where<TSource>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, int, bool>> predicate)
    {
      return default(IQueryable<TSource>);
    }

    public static IQueryable<TResult> Zip<TFirst, TSecond, TResult>(IQueryable<TFirst> source1, IEnumerable<TSecond> source2, System.Linq.Expressions.Expression<Func<TFirst, TSecond, TResult>> resultSelector)
    {
      return default(IQueryable<TResult>);
    }
    #endregion
  }
}

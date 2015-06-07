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

#if !SILVERLIGHT_4_0_WP

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Linq
{
  // Summary:
  //     Provides a set of static (Shared in Visual Basic) methods for querying data
  //     structures that implement System.Linq.IQueryable<T>.
  public static class Queryable
  {
    // Summary:
    //     Applies an accumulator function over a sequence.
    //
    // Parameters:
    //   source:
    //     A sequence to aggregate over.
    //
    //   func:
    //     An accumulator function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The final accumulator value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or func is null.
    [Pure]
    public static TSource Aggregate<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, TSource, TSource>> func)
    {
      return default(TSource);
    }
    //
    // Summary:
    //     Applies an accumulator function over a sequence. The specified seed value
    //     is used as the initial accumulator value.
    //
    // Parameters:
    //   source:
    //     A sequence to aggregate over.
    //
    //   seed:
    //     The initial accumulator value.
    //
    //   func:
    //     An accumulator function to invoke on each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TAccumulate:
    //     The type of the accumulator value.
    //
    // Returns:
    //     The final accumulator value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or func is null.
    [Pure]
    public static TAccumulate Aggregate<TSource, TAccumulate>(this IQueryable<TSource> source, TAccumulate seed, Expression<Func<TAccumulate, TSource, TAccumulate>> func)
    {
      return default(TAccumulate);
    }
    //
    // Summary:
    //     Applies an accumulator function over a sequence. The specified seed value
    //     is used as the initial accumulator value, and the specified function is used
    //     to select the result value.
    //
    // Parameters:
    //   source:
    //     A sequence to aggregate over.
    //
    //   seed:
    //     The initial accumulator value.
    //
    //   func:
    //     An accumulator function to invoke on each element.
    //
    //   selector:
    //     A function to transform the final accumulator value into the result value.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TAccumulate:
    //     The type of the accumulator value.
    //
    //   TResult:
    //     The type of the resulting value.
    //
    // Returns:
    //     The transformed final accumulator value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or func or selector is null.
    [Pure]
    public static TResult Aggregate<TSource, TAccumulate, TResult>(this IQueryable<TSource> source, TAccumulate seed, Expression<Func<TAccumulate, TSource, TAccumulate>> func, Expression<Func<TAccumulate, TResult>> selector)
    {
      return default(TResult);
    }
    //
    // Summary:
    //     Determines whether all the elements of a sequence satisfy a condition.
    //
    // Parameters:
    //   source:
    //     A sequence whose elements to test for a condition.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     true if every element of the source sequence passes the test in the specified
    //     predicate, or if the sequence is empty; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static bool All<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Determines whether a sequence contains any elements.
    //
    // Parameters:
    //   source:
    //     A sequence to check for being empty.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     true if the source sequence contains any elements; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static bool Any<TSource>(this IQueryable<TSource> source)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Determines whether any element of a sequence satisfies a condition.
    //
    // Parameters:
    //   source:
    //     A sequence whose elements to test for a condition.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     true if any elements in the source sequence pass the test in the specified
    //     predicate; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static bool Any<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Converts a generic System.Collections.Generic.IEnumerable<T> to a generic
    //     System.Linq.IQueryable<T>.
    //
    // Parameters:
    //   source:
    //     A sequence to convert.
    //
    // Type parameters:
    //   TElement:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that represents the input sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static IQueryable<TElement> AsQueryable<TElement>(this IEnumerable<TElement> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IQueryable<TElement>>() != null);
      return default(IQueryable<TElement>);
    }
    //
    // Summary:
    //     Converts an System.Collections.IEnumerable to an System.Linq.IQueryable.
    //
    // Parameters:
    //   source:
    //     A sequence to convert.
    //
    // Returns:
    //     An System.Linq.IQueryable that represents the input sequence.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     source does not implement System.Collections.Generic.IEnumerable<T> for some
    //     T.
    //
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static IQueryable AsQueryable(this IEnumerable source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IQueryable>() != null);
      return default(IQueryable);
    }
    //
    // Summary:
    //     Computes the average of a sequence of nullable System.Decimal values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Decimal values to calculate the average of.
    //
    // Returns:
    //     The average of the sequence of values, or null if the source sequence is
    //     empty or contains only null values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static decimal? Average(this IQueryable<decimal?> source)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the average of a sequence of System.Decimal values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Decimal values to calculate the average of.
    //
    // Returns:
    //     The average of the sequence of values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static decimal Average(this IQueryable<decimal> source)
    {
      return default(decimal);
    }
    //
    // Summary:
    //     Computes the average of a sequence of nullable System.Double values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Double values to calculate the average of.
    //
    // Returns:
    //     The average of the sequence of values, or null if the source sequence is
    //     empty or contains only null values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static double? Average(this IQueryable<double?> source)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the average of a sequence of System.Double values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Double values to calculate the average of.
    //
    // Returns:
    //     The average of the sequence of values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static double Average(this IQueryable<double> source)
    {
      return default(double);
    }
    //
    // Summary:
    //     Computes the average of a sequence of nullable System.Single values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Single values to calculate the average of.
    //
    // Returns:
    //     The average of the sequence of values, or null if the source sequence is
    //     empty or contains only null values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static float? Average(this IQueryable<float?> source)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the average of a sequence of System.Single values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Single values to calculate the average of.
    //
    // Returns:
    //     The average of the sequence of values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static float Average(this IQueryable<float> source)
    {
      return default(float);
    }
    //
    // Summary:
    //     Computes the average of a sequence of nullable System.Int32 values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Int32values to calculate the average of.
    //
    // Returns:
    //     The average of the sequence of values, or null if the source sequence is
    //     empty or contains only null values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static double? Average(this IQueryable<int?> source)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the average of a sequence of System.Int32 values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Int32 values to calculate the average of.
    //
    // Returns:
    //     The average of the sequence of values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static double Average(this IQueryable<int> source)
    {
      return default(double);
    }
    //
    // Summary:
    //     Computes the average of a sequence of nullable System.Int64 values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Int64 values to calculate the average of.
    //
    // Returns:
    //     The average of the sequence of values, or null if the source sequence is
    //     empty or contains only null values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static double? Average(this IQueryable<long?> source)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the average of a sequence of System.Int64 values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Int64 values to calculate the average of.
    //
    // Returns:
    //     The average of the sequence of values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static double Average(this IQueryable<long> source)
    {
      return default(double);
    }
    //
    // Summary:
    //     Computes the average of a sequence of nullable System.Decimal values that
    //     is obtained by invoking a projection function on each element of the input
    //     sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values to calculate the average of.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The average of the sequence of values, or null if the source sequence is
    //     empty or contains only null values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static decimal? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the average of a sequence of System.Decimal values that is obtained
    //     by invoking a projection function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values that are used to calculate an average.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The average of the sequence of values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static decimal Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
    {
      return default(decimal);
    }
    //
    // Summary:
    //     Computes the average of a sequence of nullable System.Double values that
    //     is obtained by invoking a projection function on each element of the input
    //     sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values to calculate the average of.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The average of the sequence of values, or null if the source sequence is
    //     empty or contains only null values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static double? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the average of a sequence of System.Double values that is obtained
    //     by invoking a projection function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values to calculate the average of.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The average of the sequence of values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static double Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector)
    {
      return default(double);
    }
    //
    // Summary:
    //     Computes the average of a sequence of nullable System.Single values that
    //     is obtained by invoking a projection function on each element of the input
    //     sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values to calculate the average of.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The average of the sequence of values, or null if the source sequence is
    //     empty or contains only null values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static float? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the average of a sequence of System.Single values that is obtained
    //     by invoking a projection function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values to calculate the average of.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The average of the sequence of values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static float Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector)
    {
      return default(float);
    }
    //
    // Summary:
    //     Computes the average of a sequence of nullable System.Int32 values that is
    //     obtained by invoking a projection function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values to calculate the average of.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The average of the sequence of values, or null if the source sequence is
    //     empty or contains only null values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static double? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the average of a sequence of System.Int32 values that is obtained
    //     by invoking a projection function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values to calculate the average of.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The average of the sequence of values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static double Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector)
    {
      return default(double);
    }
    //
    // Summary:
    //     Computes the average of a sequence of nullable System.Int64 values that is
    //     obtained by invoking a projection function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values to calculate the average of.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The average of the sequence of values, or null if the source sequence is
    //     empty or contains only null values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static double? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the average of a sequence of System.Int64 values that is obtained
    //     by invoking a projection function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values to calculate the average of.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The average of the sequence of values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static double Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector)
    {
      return default(double);
    }
    //
    // Summary:
    //     Converts the elements of an System.Linq.IQueryable to the specified type.
    //
    // Parameters:
    //   source:
    //     The System.Linq.IQueryable that contains the elements to be converted.
    //
    // Type parameters:
    //   TResult:
    //     The type to convert the elements of source to.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains each element of the source sequence
    //     converted to the specified type.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static IQueryable<TResult> Cast<TResult>(this IQueryable source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IQueryable<TResult>>() != null);
      return default(IQueryable<TResult>);
    }
    //
    // Summary:
    //     Concatenates two sequences.
    //
    // Parameters:
    //   source1:
    //     The first sequence to concatenate.
    //
    //   source2:
    //     The sequence to concatenate to the first sequence.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of the input sequences.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains the concatenated elements of the
    //     two input sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source1 or source2 is null.
    [Pure]
    public static IQueryable<TSource> Concat<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
    {
      Contract.Requires(source1 != null);
      Contract.Requires(source2 != null);
      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }
    //
    // Summary:
    //     Determines whether a sequence contains a specified element by using the default
    //     equality comparer.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> in which to locate item.
    //
    //   item:
    //     The object to locate in the sequence.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     true if the input sequence contains an element that has the specified value;
    //     otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static bool Contains<TSource>(this IQueryable<TSource> source, TSource item)
    {
      Contract.Requires(source != null);

      return default(bool);
    }
    //
    // Summary:
    //     Determines whether a sequence contains a specified element by using a specified
    //     System.Collections.Generic.IEqualityComparer<T>.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> in which to locate item.
    //
    //   item:
    //     The object to locate in the sequence.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to compare values.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     true if the input sequence contains an element that has the specified value;
    //     otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static bool Contains<TSource>(this IQueryable<TSource> source, TSource item, IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(source != null);
      return default(bool);
    }
    //
    // Summary:
    //     Returns the number of elements in a sequence.
    //
    // Parameters:
    //   source:
    //     The System.Linq.IQueryable<T> that contains the elements to be counted.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The number of elements in the input sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static int Count<TSource>(this IQueryable<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<int>() >= 0);
      return default(int);
    }
    //
    // Summary:
    //     Returns the number of elements in the specified sequence that satisfies a
    //     condition.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> that contains the elements to be counted.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The number of elements in the sequence that satisfies the condition in the
    //     predicate function.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static int Count<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      Contract.Ensures(Contract.Result<int>() >= 0);
      return default(int);
    }
    //
    // Summary:
    //     Returns the elements of the specified sequence or the type parameter's default
    //     value in a singleton collection if the sequence is empty.
    //
    // Parameters:
    //   source:
    //     The System.Linq.IQueryable<T> to return a default value for if empty.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains default(TSource) if source is
    //     empty; otherwise, source.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static IQueryable<TSource> DefaultIfEmpty<TSource>(this IQueryable<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      Contract.Ensures(Contract.Result<IQueryable<TSource>>().Any());
      return default(IQueryable<TSource>);
    }
    //
    // Summary:
    //     Returns the elements of the specified sequence or the specified value in
    //     a singleton collection if the sequence is empty.
    //
    // Parameters:
    //   source:
    //     The System.Linq.IQueryable<T> to return the specified value for if empty.
    //
    //   defaultValue:
    //     The value to return if the sequence is empty.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains defaultValue if source is empty;
    //     otherwise, source.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static IQueryable<TSource> DefaultIfEmpty<TSource>(this IQueryable<TSource> source, TSource defaultValue)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      Contract.Ensures(Contract.Result<IQueryable<TSource>>().Any());
      return default(IQueryable<TSource>);
    }
    //
    // Summary:
    //     Returns distinct elements from a sequence by using the default equality comparer
    //     to compare values.
    //
    // Parameters:
    //   source:
    //     The System.Linq.IQueryable<T> to remove duplicates from.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains distinct elements from source.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static IQueryable<TSource> Distinct<TSource>(this IQueryable<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }
    //
    // Summary:
    //     Returns distinct elements from a sequence by using a specified System.Collections.Generic.IEqualityComparer<T>
    //     to compare values.
    //
    // Parameters:
    //   source:
    //     The System.Linq.IQueryable<T> to remove duplicates from.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to compare values.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains distinct elements from source.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or comparer is null.
    [Pure]
    public static IQueryable<TSource> Distinct<TSource>(this IQueryable<TSource> source, IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(comparer != null);

      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }
    //
    // Summary:
    //     Returns the element at a specified index in a sequence.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> to return an element from.
    //
    //   index:
    //     The zero-based index of the element to retrieve.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The element at the specified position in source.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero.
    [Pure]
    public static TSource ElementAt<TSource>(this IQueryable<TSource> source, int index)
    {
      return default(TSource);
    }
    //
    // Summary:
    //     Returns the element at a specified index in a sequence or a default value
    //     if the index is out of range.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> to return an element from.
    //
    //   index:
    //     The zero-based index of the element to retrieve.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     default(TSource) if index is outside the bounds of source; otherwise, the
    //     element at the specified position in source.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero.
    [Pure]
    public static TSource ElementAtOrDefault<TSource>(this IQueryable<TSource> source, int index)
    {
      return default(TSource);
    }
    //
    // Summary:
    //     Produces the set difference of two sequences by using the default equality
    //     comparer to compare values.
    //
    // Parameters:
    //   source1:
    //     An System.Linq.IQueryable<T> whose elements that are not also in source2
    //     will be returned.
    //
    //   source2:
    //     An System.Collections.Generic.IEnumerable<T> whose elements that also occur
    //     in the first sequence will not appear in the returned sequence.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of the input sequences.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains the set difference of the two
    //     sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source1 or source2 is null.
    [Pure]
    public static IQueryable<TSource> Except<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
    {
      Contract.Requires(source1 != null);
      Contract.Requires(source2 != null);
      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }
    //
    // Summary:
    //     Produces the set difference of two sequences by using the specified System.Collections.Generic.IEqualityComparer<T>
    //     to compare values.
    //
    // Parameters:
    //   source1:
    //     An System.Linq.IQueryable<T> whose elements that are not also in source2
    //     will be returned.
    //
    //   source2:
    //     An System.Collections.Generic.IEnumerable<T> whose elements that also occur
    //     in the first sequence will not appear in the returned sequence.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to compare values.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of the input sequences.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains the set difference of the two
    //     sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source1 or source2 is null.
    [Pure]
    public static IQueryable<TSource> Except<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(source1 != null);
      Contract.Requires(source2 != null);
      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }
    //
    // Summary:
    //     Returns the first element of a sequence.
    //
    // Parameters:
    //   source:
    //     The System.Linq.IQueryable<T> to return the first element of.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The first element in source.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static TSource First<TSource>(this IQueryable<TSource> source)
    {
      return default(TSource);
    }
    //
    // Summary:
    //     Returns the first element of a sequence that satisfies a specified condition.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> to return an element from.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The first element in source that passes the test in predicate.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static TSource First<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
      return default(TSource);
    }
    //
    // Summary:
    //     Returns the first element of a sequence, or a default value if the sequence
    //     contains no elements.
    //
    // Parameters:
    //   source:
    //     The System.Linq.IQueryable<T> to return the first element of.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     default(TSource) if source is empty; otherwise, the first element in source.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static TSource FirstOrDefault<TSource>(this IQueryable<TSource> source)
    {
      return default(TSource);
    }
    //
    // Summary:
    //     Returns the first element of a sequence that satisfies a specified condition
    //     or a default value if no such element is found.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> to return an element from.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     default(TSource) if source is empty or if no element passes the test specified
    //     by predicate; otherwise, the first element in source that passes the test
    //     specified by predicate.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static TSource FirstOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
      return default(TSource);
    }
    //
    // Summary:
    //     Groups the elements of a sequence according to a specified key selector function.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> whose elements to group.
    //
    //   keySelector:
    //     A function to extract the key for each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by the function represented in keySelector.
    //
    // Returns:
    //     An IQueryable<IGrouping<TKey, TSource>> in C# or IQueryable(Of IGrouping(Of
    //     TKey, TSource)) in Visual Basic where each System.Linq.IGrouping<TKey,TElement>
    //     object contains a sequence of objects and a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.
    [Pure]
    public static IQueryable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<IQueryable<IGrouping<TKey,TSource>>>() != null);
      return default(IQueryable<IGrouping<TKey, TSource>>);
    }
    //
    // Summary:
    //     Groups the elements of a sequence according to a specified key selector function
    //     and creates a result value from each group and its key.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> whose elements to group.
    //
    //   keySelector:
    //     A function to extract the key for each element.
    //
    //   resultSelector:
    //     A function to create a result value from each group.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by the function represented in keySelector.
    //
    //   TResult:
    //     The type of the result value returned by resultSelector.
    //
    // Returns:
    //     An T:System.Linq.IQueryable`1 that has a type argument of TResult and where
    //     each element represents a projection over a group and its key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector or resultSelector is null.
    [Pure]
    public static IQueryable<TResult> GroupBy<TSource, TKey, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TKey, IEnumerable<TSource>, TResult>> resultSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(resultSelector != null);

      Contract.Ensures(Contract.Result<IQueryable<TResult>>() != null);
      return default(IQueryable<TResult>);
    }
    //
    // Summary:
    //     Groups the elements of a sequence according to a specified key selector function
    //     and projects the elements for each group by using a specified function.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> whose elements to group.
    //
    //   keySelector:
    //     A function to extract the key for each element.
    //
    //   elementSelector:
    //     A function to map each source element to an element in an System.Linq.IGrouping<TKey,TElement>.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by the function represented in keySelector.
    //
    //   TElement:
    //     The type of the elements in each System.Linq.IGrouping<TKey,TElement>.
    //
    // Returns:
    //     An IQueryable<IGrouping<TKey, TElement>> in C# or IQueryable(Of IGrouping(Of
    //     TKey, TElement)) in Visual Basic where each System.Linq.IGrouping<TKey,TElement>
    //     contains a sequence of objects of type TElement and a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector or elementSelector is null.
    [Pure]
    public static IQueryable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);

      Contract.Ensures(Contract.Result<IQueryable<IGrouping<TKey, TElement>>>() != null);
      return default(IQueryable<IGrouping<TKey, TElement>>);
    }
    //
    // Summary:
    //     Groups the elements of a sequence according to a specified key selector function
    //     and compares the keys by using a specified comparer.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> whose elements to group.
    //
    //   keySelector:
    //     A function to extract the key for each element.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to compare keys.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by the function represented in keySelector.
    //
    // Returns:
    //     An IQueryable<IGrouping<TKey, TSource>> in C# or IQueryable(Of IGrouping(Of
    //     TKey, TSource)) in Visual Basic where each System.Linq.IGrouping<TKey,TElement>
    //     contains a sequence of objects and a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector or comparer is null.
    [Pure]
    public static IQueryable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(comparer != null);
      Contract.Ensures(Contract.Result<IQueryable<IGrouping<TKey,TSource>>>() != null);
      return default(IQueryable<IGrouping<TKey, TSource>>);
    }
    //
    // Summary:
    //     Groups the elements of a sequence according to a specified key selector function
    //     and creates a result value from each group and its key. Keys are compared
    //     by using a specified comparer.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> whose elements to group.
    //
    //   keySelector:
    //     A function to extract the key for each element.
    //
    //   resultSelector:
    //     A function to create a result value from each group.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to compare keys.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by the function represented in keySelector.
    //
    //   TResult:
    //     The type of the result value returned by resultSelector.
    //
    // Returns:
    //     An T:System.Linq.IQueryable`1 that has a type argument of TResult and where
    //     each element represents a projection over a group and its key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector or resultSelector or comparer is null.
    [Pure]
    public static IQueryable<TResult> GroupBy<TSource, TKey, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TKey, IEnumerable<TSource>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(resultSelector != null);

      Contract.Ensures(Contract.Result<IQueryable<TResult>>() != null);
      return default(IQueryable<TResult>);
    }
    //
    // Summary:
    //     Groups the elements of a sequence according to a specified key selector function
    //     and creates a result value from each group and its key. The elements of each
    //     group are projected by using a specified function.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> whose elements to group.
    //
    //   keySelector:
    //     A function to extract the key for each element.
    //
    //   elementSelector:
    //     A function to map each source element to an element in an System.Linq.IGrouping<TKey,TElement>.
    //
    //   resultSelector:
    //     A function to create a result value from each group.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by the function represented in keySelector.
    //
    //   TElement:
    //     The type of the elements in each System.Linq.IGrouping<TKey,TElement>.
    //
    //   TResult:
    //     The type of the result value returned by resultSelector.
    //
    // Returns:
    //     An T:System.Linq.IQueryable`1 that has a type argument of TResult and where
    //     each element represents a projection over a group and its key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector or elementSelector or resultSelector is null.
    [Pure]
    public static IQueryable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector, Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);
      Contract.Requires(resultSelector != null);

      Contract.Ensures(Contract.Result<IQueryable<TResult>>() != null);
      return default(IQueryable<TResult>);
    }
    //
    // Summary:
    //     Groups the elements of a sequence and projects the elements for each group
    //     by using a specified function. Key values are compared by using a specified
    //     comparer.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> whose elements to group.
    //
    //   keySelector:
    //     A function to extract the key for each element.
    //
    //   elementSelector:
    //     A function to map each source element to an element in an System.Linq.IGrouping<TKey,TElement>.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to compare keys.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by the function represented in keySelector.
    //
    //   TElement:
    //     The type of the elements in each System.Linq.IGrouping<TKey,TElement>.
    //
    // Returns:
    //     An IQueryable<IGrouping<TKey, TElement>> in C# or IQueryable(Of IGrouping(Of
    //     TKey, TElement)) in Visual Basic where each System.Linq.IGrouping<TKey,TElement>
    //     contains a sequence of objects of type TElement and a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector or elementSelector or comparer is null.
    [Pure]
    public static IQueryable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);
      Contract.Requires(comparer != null);
      Contract.Ensures(Contract.Result<IQueryable<IGrouping<TKey,TElement>>>() != null);
      return default(IQueryable<IGrouping<TKey, TElement>>);
    }
    //
    // Summary:
    //     Groups the elements of a sequence according to a specified key selector function
    //     and creates a result value from each group and its key. Keys are compared
    //     by using a specified comparer and the elements of each group are projected
    //     by using a specified function.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> whose elements to group.
    //
    //   keySelector:
    //     A function to extract the key for each element.
    //
    //   elementSelector:
    //     A function to map each source element to an element in an System.Linq.IGrouping<TKey,TElement>.
    //
    //   resultSelector:
    //     A function to create a result value from each group.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to compare keys.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by the function represented in keySelector.
    //
    //   TElement:
    //     The type of the elements in each System.Linq.IGrouping<TKey,TElement>.
    //
    //   TResult:
    //     The type of the result value returned by resultSelector.
    //
    // Returns:
    //     An T:System.Linq.IQueryable`1 that has a type argument of TResult and where
    //     each element represents a projection over a group and its key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector or elementSelector or resultSelector or comparer is
    //     null.
    [Pure]
    public static IQueryable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector, Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Requires(comparer != null);

      Contract.Ensures(Contract.Result<IQueryable<TResult>>() != null);
      return default(IQueryable<TResult>);
    }

    //
    // Summary:
    //     Correlates the elements of two sequences based on key equality and groups
    //     the results. The default equality comparer is used to compare keys.
    //
    // Parameters:
    //   outer:
    //     The first sequence to join.
    //
    //   inner:
    //     The sequence to join to the first sequence.
    //
    //   outerKeySelector:
    //     A function to extract the join key from each element of the first sequence.
    //
    //   innerKeySelector:
    //     A function to extract the join key from each element of the second sequence.
    //
    //   resultSelector:
    //     A function to create a result element from an element from the first sequence
    //     and a collection of matching elements from the second sequence.
    //
    // Type parameters:
    //   TOuter:
    //     The type of the elements of the first sequence.
    //
    //   TInner:
    //     The type of the elements of the second sequence.
    //
    //   TKey:
    //     The type of the keys returned by the key selector functions.
    //
    //   TResult:
    //     The type of the result elements.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains elements of type TResult obtained
    //     by performing a grouped join on two sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     outer or inner or outerKeySelector or innerKeySelector or resultSelector
    //     is null.
    [Pure]
    public static IQueryable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, IEnumerable<TInner>, TResult>> resultSelector)
    {
      Contract.Requires(outer != null);
      Contract.Requires(inner != null);
      Contract.Requires(outerKeySelector != null);
      Contract.Requires(innerKeySelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<IQueryable<TResult>>() != null);
      return default(IQueryable<TResult>);
    }
    //
    // Summary:
    //     Correlates the elements of two sequences based on key equality and groups
    //     the results. A specified System.Collections.Generic.IEqualityComparer<T>
    //     is used to compare keys.
    //
    // Parameters:
    //   outer:
    //     The first sequence to join.
    //
    //   inner:
    //     The sequence to join to the first sequence.
    //
    //   outerKeySelector:
    //     A function to extract the join key from each element of the first sequence.
    //
    //   innerKeySelector:
    //     A function to extract the join key from each element of the second sequence.
    //
    //   resultSelector:
    //     A function to create a result element from an element from the first sequence
    //     and a collection of matching elements from the second sequence.
    //
    //   comparer:
    //     A comparer to hash and compare keys.
    //
    // Type parameters:
    //   TOuter:
    //     The type of the elements of the first sequence.
    //
    //   TInner:
    //     The type of the elements of the second sequence.
    //
    //   TKey:
    //     The type of the keys returned by the key selector functions.
    //
    //   TResult:
    //     The type of the result elements.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains elements of type TResult obtained
    //     by performing a grouped join on two sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     outer or inner or outerKeySelector or innerKeySelector or resultSelector
    //     is null.
    [Pure]
    public static IQueryable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, IEnumerable<TInner>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(outer != null);
      Contract.Requires(inner != null);
      Contract.Requires(outerKeySelector != null);
      Contract.Requires(innerKeySelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<IQueryable<TResult>>() != null);
      return default(IQueryable<TResult>);
    }
    //
    // Summary:
    //     Produces the set intersection of two sequences by using the default equality
    //     comparer to compare values.
    //
    // Parameters:
    //   source1:
    //     A sequence whose distinct elements that also appear in source2 are returned.
    //
    //   source2:
    //     A sequence whose distinct elements that also appear in the first sequence
    //     are returned.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of the input sequences.
    //
    // Returns:
    //     A sequence that contains the set intersection of the two sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source1 or source2 is null.
    [Pure]
    public static IQueryable<TSource> Intersect<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
    {
      Contract.Requires(source1 != null);
      Contract.Requires(source2 != null);
      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }
    //
    // Summary:
    //     Produces the set intersection of two sequences by using the specified System.Collections.Generic.IEqualityComparer<T>
    //     to compare values.
    //
    // Parameters:
    //   source1:
    //     An System.Linq.IQueryable<T> whose distinct elements that also appear in
    //     source2 are returned.
    //
    //   source2:
    //     An System.Collections.Generic.IEnumerable<T> whose distinct elements that
    //     also appear in the first sequence are returned.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to compare values.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of the input sequences.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains the set intersection of the two
    //     sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source1 or source2 is null.
    [Pure]
    public static IQueryable<TSource> Intersect<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(source1 != null);
      Contract.Requires(source2 != null);
      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }
    //
    // Summary:
    //     Correlates the elements of two sequences based on matching keys. The default
    //     equality comparer is used to compare keys.
    //
    // Parameters:
    //   outer:
    //     The first sequence to join.
    //
    //   inner:
    //     The sequence to join to the first sequence.
    //
    //   outerKeySelector:
    //     A function to extract the join key from each element of the first sequence.
    //
    //   innerKeySelector:
    //     A function to extract the join key from each element of the second sequence.
    //
    //   resultSelector:
    //     A function to create a result element from two matching elements.
    //
    // Type parameters:
    //   TOuter:
    //     The type of the elements of the first sequence.
    //
    //   TInner:
    //     The type of the elements of the second sequence.
    //
    //   TKey:
    //     The type of the keys returned by the key selector functions.
    //
    //   TResult:
    //     The type of the result elements.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that has elements of type TResult obtained by
    //     performing an inner join on two sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     outer or inner or outerKeySelector or innerKeySelector or resultSelector
    //     is null.
    [Pure]
    public static IQueryable<TResult> Join<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TInner, TResult>> resultSelector)
    {
      Contract.Requires(outer != null);
      Contract.Requires(inner != null);
      Contract.Requires(outerKeySelector != null);
      Contract.Requires(innerKeySelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<IQueryable<TResult>>() != null);
      return default(IQueryable<TResult>);
    }
    //
    // Summary:
    //     Correlates the elements of two sequences based on matching keys. A specified
    //     System.Collections.Generic.IEqualityComparer<T> is used to compare keys.
    //
    // Parameters:
    //   outer:
    //     The first sequence to join.
    //
    //   inner:
    //     The sequence to join to the first sequence.
    //
    //   outerKeySelector:
    //     A function to extract the join key from each element of the first sequence.
    //
    //   innerKeySelector:
    //     A function to extract the join key from each element of the second sequence.
    //
    //   resultSelector:
    //     A function to create a result element from two matching elements.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to hash and compare keys.
    //
    // Type parameters:
    //   TOuter:
    //     The type of the elements of the first sequence.
    //
    //   TInner:
    //     The type of the elements of the second sequence.
    //
    //   TKey:
    //     The type of the keys returned by the key selector functions.
    //
    //   TResult:
    //     The type of the result elements.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that has elements of type TResult obtained by
    //     performing an inner join on two sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     outer or inner or outerKeySelector or innerKeySelector or resultSelector
    //     is null.
    [Pure]
    public static IQueryable<TResult> Join<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TInner, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(outer != null);
      Contract.Requires(inner != null);
      Contract.Requires(outerKeySelector != null);
      Contract.Requires(innerKeySelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<IQueryable<TResult>>() != null);
      return default(IQueryable<TResult>);
    }
    //
    // Summary:
    //     Returns the last element in a sequence.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> to return the last element of.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The value at the last position in source.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static TSource Last<TSource>(this IQueryable<TSource> source)
    {
      return default(TSource);
    }
    //
    // Summary:
    //     Returns the last element of a sequence that satisfies a specified condition.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> to return an element from.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The last element in source that passes the test specified by predicate.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static TSource Last<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
      return default(TSource);
    }
    //
    // Summary:
    //     Returns the last element in a sequence, or a default value if the sequence
    //     contains no elements.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> to return the last element of.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     default(TSource) if source is empty; otherwise, the last element in source.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static TSource LastOrDefault<TSource>(this IQueryable<TSource> source)
    {
      return default(TSource);
    }
    //
    // Summary:
    //     Returns the last element of a sequence that satisfies a condition or a default
    //     value if no such element is found.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> to return an element from.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     default(TSource) if source is empty or if no elements pass the test in the
    //     predicate function; otherwise, the last element of source that passes the
    //     test in the predicate function.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static TSource LastOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
      return default(TSource);
    }
    //
    // Summary:
    //     Returns an System.Int64 that represents the total number of elements in a
    //     sequence.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> that contains the elements to be counted.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The number of elements in source.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static long LongCount<TSource>(this IQueryable<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<long>() >= 0);
      return default(long);
    }
    //
    // Summary:
    //     Returns an System.Int64 that represents the number of elements in a sequence
    //     that satisfy a condition.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> that contains the elements to be counted.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The number of elements in source that satisfy the condition in the predicate
    //     function.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static long LongCount<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      Contract.Ensures(Contract.Result<long>() >= 0);
      return default(long);
    }
    //
    // Summary:
    //     Returns the maximum value in a generic System.Linq.IQueryable<T>.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the maximum of.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static TSource Max<TSource>(this IQueryable<TSource> source)
    {
      return default(TSource);
    }
    //
    // Summary:
    //     Invokes a projection function on each element of a generic System.Linq.IQueryable<T>
    //     and returns the maximum resulting value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the maximum of.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TResult:
    //     The type of the value returned by the function represented by selector.
    //
    // Returns:
    //     The maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static TResult Max<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
    {
      return default(TResult);
    }
    //
    // Summary:
    //     Returns the minimum value of a generic System.Linq.IQueryable<T>.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the minimum of.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static TSource Min<TSource>(this IQueryable<TSource> source)
    {
      return default(TSource);
    }
    //
    // Summary:
    //     Invokes a projection function on each element of a generic System.Linq.IQueryable<T>
    //     and returns the minimum resulting value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the minimum of.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TResult:
    //     The type of the value returned by the function represented by selector.
    //
    // Returns:
    //     The minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static TResult Min<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
    {
      return default(TResult);
    }
    //
    // Summary:
    //     Filters the elements of an System.Linq.IQueryable based on a specified type.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable whose elements to filter.
    //
    // Type parameters:
    //   TResult:
    //     The type to filter the elements of the sequence on.
    //
    // Returns:
    //     A collection that contains the elements from source that have type TResult.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    public static IQueryable<TResult> OfType<TResult>(this IQueryable source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IQueryable<TResult>>() != null);
      return default(IQueryable<TResult>);
    }
    //
    // Summary:
    //     Sorts the elements of a sequence in ascending order according to a key.
    //
    // Parameters:
    //   source:
    //     A sequence of values to order.
    //
    //   keySelector:
    //     A function to extract a key from an element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by the function that is represented by keySelector.
    //
    // Returns:
    //     An System.Linq.IOrderedQueryable<T> whose elements are sorted according to
    //     a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.
    [Pure]
    public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<IOrderedQueryable<TSource>>() != null);
      return default(IOrderedQueryable<TSource>);
    }
    //
    // Summary:
    //     Sorts the elements of a sequence in ascending order by using a specified
    //     comparer.
    //
    // Parameters:
    //   source:
    //     A sequence of values to order.
    //
    //   keySelector:
    //     A function to extract a key from an element.
    //
    //   comparer:
    //     An System.Collections.Generic.IComparer<T> to compare keys.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by the function that is represented by keySelector.
    //
    // Returns:
    //     An System.Linq.IOrderedQueryable<T> whose elements are sorted according to
    //     a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector or comparer is null.
    [Pure]
    public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(comparer != null);
      Contract.Ensures(Contract.Result<IOrderedQueryable<TSource>>() != null);
      return default(IOrderedQueryable<TSource>);
    }
    //
    // Summary:
    //     Sorts the elements of a sequence in descending order according to a key.
    //
    // Parameters:
    //   source:
    //     A sequence of values to order.
    //
    //   keySelector:
    //     A function to extract a key from an element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by the function that is represented by keySelector.
    //
    // Returns:
    //     An System.Linq.IOrderedQueryable<T> whose elements are sorted in descending
    //     order according to a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.
    [Pure]
    public static IOrderedQueryable<TSource> OrderByDescending<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<IOrderedQueryable<TSource>>() != null);
      return default(IOrderedQueryable<TSource>);
    }
    //
    // Summary:
    //     Sorts the elements of a sequence in descending order by using a specified
    //     comparer.
    //
    // Parameters:
    //   source:
    //     A sequence of values to order.
    //
    //   keySelector:
    //     A function to extract a key from an element.
    //
    //   comparer:
    //     An System.Collections.Generic.IComparer<T> to compare keys.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by the function that is represented by keySelector.
    //
    // Returns:
    //     An System.Linq.IOrderedQueryable<T> whose elements are sorted in descending
    //     order according to a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector or comparer is null.
    [Pure]
    public static IOrderedQueryable<TSource> OrderByDescending<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(comparer != null);
      Contract.Ensures(Contract.Result<IOrderedQueryable<TSource>>() != null);
      return default(IOrderedQueryable<TSource>);
    }
    //
    // Summary:
    //     Inverts the order of the elements in a sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values to reverse.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> whose elements correspond to those of the input
    //     sequence in reverse order.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static IQueryable<TSource> Reverse<TSource>(this IQueryable<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }
    //
    // Summary:
    //     Projects each element of a sequence into a new form by incorporating the
    //     element's index.
    //
    // Parameters:
    //   source:
    //     A sequence of values to project.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TResult:
    //     The type of the value returned by the function represented by selector.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> whose elements are the result of invoking a
    //     projection function on each element of source.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static IQueryable<TResult> Select<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, int, TResult>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      Contract.Ensures(Contract.Result<IQueryable<TResult>>() != null);
      return default(IQueryable<TResult>);
    }
    //
    // Summary:
    //     Projects each element of a sequence into a new form.
    //
    // Parameters:
    //   source:
    //     A sequence of values to project.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TResult:
    //     The type of the value returned by the function represented by selector.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> whose elements are the result of invoking a
    //     projection function on each element of source.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static IQueryable<TResult> Select<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      Contract.Ensures(Contract.Result<IQueryable<TResult>>() != null);
      return default(IQueryable<TResult>);
    }
    //
    // Summary:
    //     Projects each element of a sequence to an System.Collections.Generic.IEnumerable<T>
    //     and combines the resulting sequences into one sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values to project.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TResult:
    //     The type of the elements of the sequence returned by the function represented
    //     by selector.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> whose elements are the result of invoking a
    //     one-to-many projection function on each element of the input sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static IQueryable<TResult> SelectMany<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, IEnumerable<TResult>>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      Contract.Ensures(Contract.Result<IQueryable<TResult>>() != null);
      return default(IQueryable<TResult>);
    }
    //
    // Summary:
    //     Projects each element of a sequence to an System.Collections.Generic.IEnumerable<T>
    //     and combines the resulting sequences into one sequence. The index of each
    //     source element is used in the projected form of that element.
    //
    // Parameters:
    //   source:
    //     A sequence of values to project.
    //
    //   selector:
    //     A projection function to apply to each element; the second parameter of this
    //     function represents the index of the source element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TResult:
    //     The type of the elements of the sequence returned by the function represented
    //     by selector.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> whose elements are the result of invoking a
    //     one-to-many projection function on each element of the input sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static IQueryable<TResult> SelectMany<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, int, IEnumerable<TResult>>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      Contract.Ensures(Contract.Result<IQueryable<TResult>>() != null);
      return default(IQueryable<TResult>);
    }
    //
    // Summary:
    //     Projects each element of a sequence to an System.Collections.Generic.IEnumerable<T>
    //     and invokes a result selector function on each element therein. The resulting
    //     values from each intermediate sequence are combined into a single, one-dimensional
    //     sequence and returned.
    //
    // Parameters:
    //   source:
    //     A sequence of values to project.
    //
    //   collectionSelector:
    //     A projection function to apply to each element of the input sequence.
    //
    //   resultSelector:
    //     A projection function to apply to each element of each intermediate sequence.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TCollection:
    //     The type of the intermediate elements collected by the function represented
    //     by collectionSelector.
    //
    //   TResult:
    //     The type of the elements of the resulting sequence.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> whose elements are the result of invoking the
    //     one-to-many projection function collectionSelector on each element of source
    //     and then mapping each of those sequence elements and their corresponding
    //     source element to a result element.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or collectionSelector or resultSelector is null.
    [Pure]
    public static IQueryable<TResult> SelectMany<TSource, TCollection, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, IEnumerable<TCollection>>> collectionSelector, Expression<Func<TSource, TCollection, TResult>> resultSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(collectionSelector != null);
      Contract.Requires(resultSelector != null);

      Contract.Ensures(Contract.Result<IQueryable<TResult>>() != null);
      return default(IQueryable<TResult>);
    }
    //
    // Summary:
    //     Projects each element of a sequence to an System.Collections.Generic.IEnumerable<T>
    //     that incorporates the index of the source element that produced it. A result
    //     selector function is invoked on each element of each intermediate sequence,
    //     and the resulting values are combined into a single, one-dimensional sequence
    //     and returned.
    //
    // Parameters:
    //   source:
    //     A sequence of values to project.
    //
    //   collectionSelector:
    //     A projection function to apply to each element of the input sequence; the
    //     second parameter of this function represents the index of the source element.
    //
    //   resultSelector:
    //     A projection function to apply to each element of each intermediate sequence.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TCollection:
    //     The type of the intermediate elements collected by the function represented
    //     by collectionSelector.
    //
    //   TResult:
    //     The type of the elements of the resulting sequence.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> whose elements are the result of invoking the
    //     one-to-many projection function collectionSelector on each element of source
    //     and then mapping each of those sequence elements and their corresponding
    //     source element to a result element.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or collectionSelector or resultSelector is null.
    [Pure]
    public static IQueryable<TResult> SelectMany<TSource, TCollection, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, int, IEnumerable<TCollection>>> collectionSelector, Expression<Func<TSource, TCollection, TResult>> resultSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(collectionSelector != null);
      Contract.Requires(resultSelector != null);

      Contract.Ensures(Contract.Result<IQueryable<TResult>>() != null);
      return default(IQueryable<TResult>);
    }
    //
    // Summary:
    //     Determines whether two sequences are equal by using the default equality
    //     comparer to compare elements.
    //
    // Parameters:
    //   source1:
    //     An System.Linq.IQueryable<T> whose elements to compare to those of source2.
    //
    //   source2:
    //     An System.Collections.Generic.IEnumerable<T> whose elements to compare to
    //     those of the first sequence.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of the input sequences.
    //
    // Returns:
    //     true if the two source sequences are of equal length and their corresponding
    //     elements compare equal; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source1 or source2 is null.
    [Pure]
    public static bool SequenceEqual<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
    {
      Contract.Requires(source1 != null);
      Contract.Requires(source2 != null);

      return default(bool);
    }
    //
    // Summary:
    //     Determines whether two sequences are equal by using a specified System.Collections.Generic.IEqualityComparer<T>
    //     to compare elements.
    //
    // Parameters:
    //   source1:
    //     An System.Linq.IQueryable<T> whose elements to compare to those of source2.
    //
    //   source2:
    //     An System.Collections.Generic.IEnumerable<T> whose elements to compare to
    //     those of the first sequence.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to use to compare elements.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of the input sequences.
    //
    // Returns:
    //     true if the two source sequences are of equal length and their corresponding
    //     elements compare equal; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source1 or source2 is null.
    [Pure]
    public static bool SequenceEqual<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(source1 != null);
      Contract.Requires(source2 != null);

      return default(bool);
    }
    //
    // Summary:
    //     Returns the only element of a sequence, and throws an exception if there
    //     is not exactly one element in the sequence.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> to return the single element of.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The single element of the input sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static TSource Single<TSource>(this IQueryable<TSource> source)
    {
      return default(TSource);
    }
    //
    // Summary:
    //     Returns the only element of a sequence that satisfies a specified condition,
    //     and throws an exception if more than one such element exists.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> to return a single element from.
    //
    //   predicate:
    //     A function to test an element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The single element of the input sequence that satisfies the condition in
    //     predicate.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    //
    //   System.InvalidOperationException:
    //     No element satisfies the condition in predicate.  -or- More than one element
    //     satisfies the condition in predicate.  -or- The source sequence is empty.
    [Pure]
    public static TSource Single<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
      return default(TSource);
    }
    //
    // Summary:
    //     Returns the only element of a sequence, or a default value if the sequence
    //     is empty; this method throws an exception if there is more than one element
    //     in the sequence.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> to return the single element of.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The single element of the input sequence, or default(TSource) if the sequence
    //     contains no elements.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static TSource SingleOrDefault<TSource>(this IQueryable<TSource> source)
    {
      return default(TSource);
    }
    //
    // Summary:
    //     Returns the only element of a sequence that satisfies a specified condition
    //     or a default value if no such element exists; this method throws an exception
    //     if more than one element satisfies the condition.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> to return a single element from.
    //
    //   predicate:
    //     A function to test an element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The single element of the input sequence that satisfies the condition in
    //     predicate, or default(TSource) if no such element is found.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static TSource SingleOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
      return default(TSource);
    }
    //
    // Summary:
    //     Bypasses a specified number of elements in a sequence and then returns the
    //     remaining elements.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> to return elements from.
    //
    //   count:
    //     The number of elements to skip before returning the remaining elements.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains elements that occur after the
    //     specified index in the input sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static IQueryable<TSource> Skip<TSource>(this IQueryable<TSource> source, int count)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }

    //
    // Summary:
    //     Bypasses elements in a sequence as long as a specified condition is true
    //     and then returns the remaining elements.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> to return elements from.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains elements from source starting
    //     at the first element in the linear series that does not pass the test specified
    //     by predicate.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static IQueryable<TSource> SkipWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);

      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }
    //
    // Summary:
    //     Bypasses elements in a sequence as long as a specified condition is true
    //     and then returns the remaining elements. The element's index is used in the
    //     logic of the predicate function.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> to return elements from.
    //
    //   predicate:
    //     A function to test each element for a condition; the second parameter of
    //     this function represents the index of the source element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains elements from source starting
    //     at the first element in the linear series that does not pass the test specified
    //     by predicate.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static IQueryable<TSource> SkipWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);

      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }
    //
    // Summary:
    //     Computes the sum of a sequence of nullable System.Decimal values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Decimal values to calculate the sum of.
    //
    // Returns:
    //     The sum of the values in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static decimal? Sum(this IQueryable<decimal?> source)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the sum of a sequence of System.Decimal values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Decimal values to calculate the sum of.
    //
    // Returns:
    //     The sum of the values in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static decimal Sum(this IQueryable<decimal> source)
    {
      return default(decimal);
    }
    //
    // Summary:
    //     Computes the sum of a sequence of nullable System.Double values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Double values to calculate the sum of.
    //
    // Returns:
    //     The sum of the values in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static double? Sum(this IQueryable<double?> source)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the sum of a sequence of System.Double values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Double values to calculate the sum of.
    //
    // Returns:
    //     The sum of the values in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static double Sum(this IQueryable<double> source)
    {
      return default(double);
    }
    //
    // Summary:
    //     Computes the sum of a sequence of nullable System.Single values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Single values to calculate the sum of.
    //
    // Returns:
    //     The sum of the values in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static float? Sum(this IQueryable<float?> source)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the sum of a sequence of System.Single values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Single values to calculate the sum of.
    //
    // Returns:
    //     The sum of the values in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static float Sum(this IQueryable<float> source)
    {
      return default(float);
    }
    //
    // Summary:
    //     Computes the sum of a sequence of nullable System.Int32 values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Int32 values to calculate the sum of.
    //
    // Returns:
    //     The sum of the values in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static int? Sum(this IQueryable<int?> source)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the sum of a sequence of System.Int32 values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Int32 values to calculate the sum of.
    //
    // Returns:
    //     The sum of the values in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static int Sum(this IQueryable<int> source)
    {
      return default(int);
    }
    //
    // Summary:
    //     Computes the sum of a sequence of nullable System.Int64 values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Int64 values to calculate the sum of.
    //
    // Returns:
    //     The sum of the values in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static long? Sum(this IQueryable<long?> source)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the sum of a sequence of System.Int64 values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Int64 values to calculate the sum of.
    //
    // Returns:
    //     The sum of the values in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static long Sum(this IQueryable<long> source)
    {
      return default(long);
    }
    //
    // Summary:
    //     Computes the sum of the sequence of nullable System.Decimal values that is
    //     obtained by invoking a projection function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values of type TSource.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The sum of the projected values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static decimal? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the sum of the sequence of System.Decimal values that is obtained
    //     by invoking a projection function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values of type TSource.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The sum of the projected values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static decimal Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
    {
      return default(decimal);
    }
    //
    // Summary:
    //     Computes the sum of the sequence of nullable System.Double values that is
    //     obtained by invoking a projection function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values of type TSource.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The sum of the projected values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static double? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the sum of the sequence of System.Double values that is obtained
    //     by invoking a projection function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values of type TSource.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The sum of the projected values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static double Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector)
    {
      return default(double);
    }
    //
    // Summary:
    //     Computes the sum of the sequence of nullable System.Single values that is
    //     obtained by invoking a projection function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values of type TSource.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The sum of the projected values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static float? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the sum of the sequence of System.Single values that is obtained
    //     by invoking a projection function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values of type TSource.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The sum of the projected values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static float Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector)
    {
      return default(float);
    }
    //
    // Summary:
    //     Computes the sum of the sequence of nullable System.Int32 values that is
    //     obtained by invoking a projection function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values of type TSource.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The sum of the projected values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static int? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the sum of the sequence of System.Int32 values that is obtained
    //     by invoking a projection function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values of type TSource.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The sum of the projected values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static int Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector)
    {
      return default(int);
    }
    //
    // Summary:
    //     Computes the sum of the sequence of nullable System.Int64 values that is
    //     obtained by invoking a projection function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values of type TSource.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The sum of the projected values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static long? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector)
    {
      return null;
    }
    //
    // Summary:
    //     Computes the sum of the sequence of System.Int64 values that is obtained
    //     by invoking a projection function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values of type TSource.
    //
    //   selector:
    //     A projection function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The sum of the projected values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static long Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector)
    {
      return default(long);
    }
    //
    // Summary:
    //     Returns a specified number of contiguous elements from the start of a sequence.
    //
    // Parameters:
    //   source:
    //     The sequence to return elements from.
    //
    //   count:
    //     The number of elements to return.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains the specified number of elements
    //     from the start of source.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static IQueryable<TSource> Take<TSource>(this IQueryable<TSource> source, int count)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }

    //
    // Summary:
    //     Returns elements from a sequence as long as a specified condition is true.
    //
    // Parameters:
    //   source:
    //     The sequence to return elements from.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains elements from the input sequence
    //     occurring before the element at which the test specified by predicate no
    //     longer passes.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static IQueryable<TSource> TakeWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);

      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }
    //
    // Summary:
    //     Returns elements from a sequence as long as a specified condition is true.
    //     The element's index is used in the logic of the predicate function.
    //
    // Parameters:
    //   source:
    //     The sequence to return elements from.
    //
    //   predicate:
    //     A function to test each element for a condition; the second parameter of
    //     the function represents the index of the element in the source sequence.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains elements from the input sequence
    //     occurring before the element at which the test specified by predicate no
    //     longer passes.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static IQueryable<TSource> TakeWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);

      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }
    //
    // Summary:
    //     Performs a subsequent ordering of the elements in a sequence in ascending
    //     order according to a key.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IOrderedQueryable<T> that contains elements to sort.
    //
    //   keySelector:
    //     A function to extract a key from each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by the function represented by keySelector.
    //
    // Returns:
    //     An System.Linq.IOrderedQueryable<T> whose elements are sorted according to
    //     a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.
    [Pure]
    public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<IOrderedQueryable<TSource>>() != null);
      return default(IOrderedQueryable<TSource>);
    }
    //
    // Summary:
    //     Performs a subsequent ordering of the elements in a sequence in ascending
    //     order by using a specified comparer.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IOrderedQueryable<T> that contains elements to sort.
    //
    //   keySelector:
    //     A function to extract a key from each element.
    //
    //   comparer:
    //     An System.Collections.Generic.IComparer<T> to compare keys.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by the function represented by keySelector.
    //
    // Returns:
    //     An System.Linq.IOrderedQueryable<T> whose elements are sorted according to
    //     a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector or comparer is null.
    [Pure]
    public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(comparer != null);
      Contract.Ensures(Contract.Result<IOrderedQueryable<TSource>>() != null);
      return default(IOrderedQueryable<TSource>);
    }
    //
    // Summary:
    //     Performs a subsequent ordering of the elements in a sequence in descending
    //     order, according to a key.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IOrderedQueryable<T> that contains elements to sort.
    //
    //   keySelector:
    //     A function to extract a key from each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by the function represented by keySelector.
    //
    // Returns:
    //     An System.Linq.IOrderedQueryable<T> whose elements are sorted in descending
    //     order according to a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.
    [Pure]
    public static IOrderedQueryable<TSource> ThenByDescending<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<IOrderedQueryable<TSource>>() != null);
      return default(IOrderedQueryable<TSource>);
    }
    //
    // Summary:
    //     Performs a subsequent ordering of the elements in a sequence in descending
    //     order by using a specified comparer.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IOrderedQueryable<T> that contains elements to sort.
    //
    //   keySelector:
    //     A function to extract a key from each element.
    //
    //   comparer:
    //     An System.Collections.Generic.IComparer<T> to compare keys.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key that is returned by the keySelector function.
    //
    // Returns:
    //     A collection whose elements are sorted in descending order according to a
    //     key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector or comparer is null.
    [Pure]
    public static IOrderedQueryable<TSource> ThenByDescending<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(comparer != null);
      Contract.Ensures(Contract.Result<IOrderedQueryable<TSource>>() != null);
      return default(IOrderedQueryable<TSource>);
    }

    //
    // Summary:
    //     Produces the set union of two sequences by using the default equality comparer.
    //
    // Parameters:
    //   source1:
    //     A sequence whose distinct elements form the first set for the union operation.
    //
    //   source2:
    //     A sequence whose distinct elements form the second set for the union operation.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of the input sequences.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains the elements from both input sequences,
    //     excluding duplicates.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source1 or source2 is null.
    [Pure]
    public static IQueryable<TSource> Union<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
    {
      Contract.Requires(source1 != null);
      Contract.Requires(source2 != null);

      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }
    //
    // Summary:
    //     Produces the set union of two sequences by using a specified System.Collections.Generic.IEqualityComparer<T>.
    //
    // Parameters:
    //   source1:
    //     A sequence whose distinct elements form the first set for the union operation.
    //
    //   source2:
    //     A sequence whose distinct elements form the second set for the union operation.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to compare values.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of the input sequences.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains the elements from both input sequences,
    //     excluding duplicates.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source1 or source2 is null.
    [Pure]
    public static IQueryable<TSource> Union<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(source1 != null);
      Contract.Requires(source2 != null);

      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }
    //
    // Summary:
    //     Filters a sequence of values based on a predicate.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> to filter.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains elements from the input sequence
    //     that satisfy the condition specified by predicate.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);

      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }

    //
    // Summary:
    //     Filters a sequence of values based on a predicate. Each element's index is
    //     used in the logic of the predicate function.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IQueryable<T> to filter.
    //
    //   predicate:
    //     A function to test each element for a condition; the second parameter of
    //     the function represents the index of the element in the source sequence.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that contains elements from the input sequence
    //     that satisfy the condition specified by predicate.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);

      Contract.Ensures(Contract.Result<IQueryable<TSource>>() != null);
      return default(IQueryable<TSource>);
    }
  }
}

#endif
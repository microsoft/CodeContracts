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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Diagnostics.Contracts;

namespace System.Linq
{
  public static class Enumerable
  {
    [Pure]
    public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      return default(bool);
    }
    [Pure]
    public static bool Any<TSource>(this IEnumerable<TSource> source) {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<bool>() == Count(source) > 0);
      return default(bool);
    }
    [Pure]
    public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      return default(bool);
    }

    //
    // Summary:
    //     Returns the input typed as System.Collections.Generic.IEnumerable<T>.
    //
    // Parameters:
    //   source:
    //     The sequence to type as System.Collections.Generic.IEnumerable<T>.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The input sequence typed as System.Collections.Generic.IEnumerable<T>.
    [Pure]
    public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source)
    {
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() == source);
      return default(IEnumerable<TSource>);
    }

    [Pure]
    public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      return default(IEnumerable<TResult>);
    }

    //
    // Summary:
    //     Concatenates two sequences.
    //
    // Parameters:
    //   first:
    //     The first sequence to concatenate.
    //
    //   second:
    //     The sequence to concatenate to the first sequence.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of the input sequences.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains the concatenated
    //     elements of the two input sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     first or second is null.
    [Pure]
    public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      Contract.Ensures(Count(Contract.Result<IEnumerable<TSource>>()) == Count(first) + Count(second));
      return default(IEnumerable<TSource>);
    }
    //
    // Summary:
    //     Determines whether a sequence contains a specified element by using the default
    //     equality comparer.
    //
    // Parameters:
    //   source:
    //     A sequence in which to locate a value.
    //
    //   value:
    //     The value to locate in the sequence.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     true if the source sequence contains an element that has the specified value;
    //     otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value)
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
    //     A sequence in which to locate a value.
    //
    //   value:
    //     The value to locate in the sequence.
    //
    //   comparer:
    //     An equality comparer to compare values.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     true if the source sequence contains an element that has the specified value;
    //     otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
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
    //     A sequence that contains elements to be counted.
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
    //
    //   System.OverflowException:
    //     The number of elements in source is larger than System.Int32.MaxValue.
    [Pure]
    public static int Count<TSource>(this IEnumerable<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures((source as ICollection<TSource>) == null || Contract.Result<int>() == ((ICollection<TSource>)source).Count);
      return default(int);
    }
    //
    // Summary:
    //     Returns a number that represents how many elements in the specified sequence
    //     satisfy a condition.
    //
    // Parameters:
    //   source:
    //     A sequence that contains elements to be tested and counted.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     A number that represents how many elements in the sequence satisfy the condition
    //     in the predicate function.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    //
    //   System.OverflowException:
    //     The number of elements in source is larger than System.Int32.MaxValue.
    [Pure]
    public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() <= Count(source));
      //Contract.Ensures(Contract.ForAll(source, _ => predicate(_)));
      return default(int);
    }
    //
    // Summary:
    //     Returns the elements of the specified sequence or the type parameter's default
    //     value in a singleton collection if the sequence is empty.
    //
    // Parameters:
    //   source:
    //     The sequence to return a default value for if it is empty.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains default(TSource)
    //     if source is empty; otherwise, source.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      return default(IEnumerable<TSource>);
    }

    //
    // Summary:
    //     Returns the elements of the specified sequence or the specified value in
    //     a singleton collection if the sequence is empty.
    //
    // Parameters:
    //   source:
    //     The sequence to return the specified value for if it is empty.
    //
    //   defaultValue:
    //     The value to return if the sequence is empty.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains defaultValue if
    //     source is empty; otherwise, source.
    [Pure]
    public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      return default(IEnumerable<TSource>);
    }
    //
    // Summary:
    //     Returns distinct elements from a sequence by using the default equality comparer
    //     to compare values.
    //
    // Parameters:
    //   source:
    //     The sequence to remove duplicate elements from.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains distinct elements
    //     from the source sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      Contract.Ensures(Count(Contract.Result<IEnumerable<TSource>>()) <= Count(source));
      return default(IEnumerable<TSource>);
    }
    //
    // Summary:
    //     Returns distinct elements from a sequence by using a specified System.Collections.Generic.IEqualityComparer<T>
    //     to compare values.
    //
    // Parameters:
    //   source:
    //     The sequence to remove duplicate elements from.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to compare values.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains distinct elements
    //     from the source sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      Contract.Ensures(Count(Contract.Result<IEnumerable<TSource>>()) <= Count(source));
      return default(IEnumerable<TSource>);
    }
    //
    // Summary:
    //     Returns the element at a specified index in a sequence.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to return an element from.
    //
    //   index:
    //     The zero-based index of the element to retrieve.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The element at the specified position in the source sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     index is less than 0 or greater than or equal to the number of elements in
    //     source.
    [Pure]
    public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, int index)
    {
      Contract.Requires(source != null);
      Contract.Requires(index >= 0);
      Contract.Requires(index < Count(source));
      return default(TSource);
    }

    //
    // Summary:
    //     Returns the element at a specified index in a sequence or a default value
    //     if the index is out of range.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to return an element from.
    //
    //   index:
    //     The zero-based index of the element to retrieve.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     default(TSource) if the index is outside the bounds of the source sequence;
    //     otherwise, the element at the specified position in the source sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, int index)
    {
      Contract.Requires(source != null);
      return default(TSource);
    }

    //
    // Summary:
    //     Returns an empty System.Collections.Generic.IEnumerable<T> that has the specified
    //     type argument.
    //
    // Type parameters:
    //   TResult:
    //     The type to assign to the type parameter of the returned generic System.Collections.Generic.IEnumerable<T>.
    //
    // Returns:
    //     An empty System.Collections.Generic.IEnumerable<T> whose type argument is
    //     TResult.
    [Pure]
    public static IEnumerable<TResult> Empty<TResult>()
    {
      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      Contract.Ensures(Count(Contract.Result<IEnumerable<TResult>>()) == 0);
      return default(IEnumerable<TResult>);
    }
    //
    // Summary:
    //     Produces the set difference of two sequences by using the default equality
    //     comparer to compare values.
    //
    // Parameters:
    //   first:
    //     An System.Collections.Generic.IEnumerable<T> whose elements that are not
    //     also in second will be returned.
    //
    //   second:
    //     An System.Collections.Generic.IEnumerable<T> whose elements that also occur
    //     in the first sequence will cause those elements to be removed from the returned
    //     sequence.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of the input sequences.
    //
    // Returns:
    //     A sequence that contains the set difference of the elements of two sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     first or second is null.
    [Pure]
    public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      Contract.Ensures(Count(Contract.Result<IEnumerable<TSource>>()) <= Count(first));

      return default(IEnumerable<TSource>);
    }
    //
    // Summary:
    //     Produces the set difference of two sequences by using the specified System.Collections.Generic.IEqualityComparer<T>
    //     to compare values.
    //
    // Parameters:
    //   first:
    //     An System.Collections.Generic.IEnumerable<T> whose elements that are not
    //     also in second will be returned.
    //
    //   second:
    //     An System.Collections.Generic.IEnumerable<T> whose elements that also occur
    //     in the first sequence will cause those elements to be removed from the returned
    //     sequence.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to compare values.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of the input sequences.
    //
    // Returns:
    //     A sequence that contains the set difference of the elements of two sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     first or second is null.
    [Pure]
    public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      Contract.Ensures(Count(Contract.Result<IEnumerable<TSource>>()) <= Count(first));

      return default(IEnumerable<TSource>);
    }
    //
    // Summary:
    //     Returns the first element of a sequence.
    //
    // Parameters:
    //   source:
    //     The System.Collections.Generic.IEnumerable<T> to return the first element
    //     of.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The first element in the specified sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    //
    //   System.InvalidOperationException:
    //     The source sequence is empty.
    [Pure]
    public static TSource First<TSource>(this IEnumerable<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Requires(Any(source));
      Contract.Ensures((source as ICollection<TSource>) == null || ((ICollection<TSource>)source).Count > 0);

      return default(TSource);
    }

    //
    // Summary:
    //     Returns the first element in a sequence that satisfies a specified condition.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to return an element from.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The first element in the sequence that passes the test in the specified predicate
    //     function.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    //
    //   System.InvalidOperationException:
    //     No element satisfies the condition in predicate.  -or- The source sequence
    //     is empty.
    [Pure]
    public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(Any(source));
      Contract.Requires(predicate != null);

      return default(TSource);
    }

    //
    // Summary:
    //     Returns the first element of a sequence, or a default value if the sequence
    //     contains no elements.
    //
    // Parameters:
    //   source:
    //     The System.Collections.Generic.IEnumerable<T> to return the first element
    //     of.
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
    public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source) {
      Contract.Requires(source != null);
      
      return default(TSource);
    }

    //
    // Summary:
    //     Returns the first element of the sequence that satisfies a condition or a
    //     default value if no such element is found.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to return an element from.
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
    public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      return default(TSource);
    }

    //
    // Summary:
    //     Groups the elements of a sequence according to a specified key selector function.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> whose elements to group.
    //
    //   keySelector:
    //     A function to extract the key for each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by keySelector.
    //
    // Returns:
    //     An IEnumerable<IGrouping<TKey, TSource>> in C# or IEnumerable(Of IGrouping(Of
    //     TKey, TSource)) in Visual Basic where each System.Linq.IGrouping<TKey,TElement>
    //     object contains a sequence of objects and a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.
    [Pure]
    public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<IEnumerable<IGrouping<TKey,TSource>>>() != null);
      return null;
    }

    //
    // Summary:
    //     Groups the elements of a sequence according to a specified key selector function
    //     and creates a result value from each group and its key.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> whose elements to group.
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
    //     The type of the key returned by keySelector.
    //
    //   TResult:
    //     The type of the result value returned by resultSelector.
    //
    // Returns:
    //     A collection of elements of type TResult where each element represents a
    //     projection over a group and its key.
    [Pure]
    public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      return default(IEnumerable<TResult>);
    }
    //
    // Summary:
    //     Groups the elements of a sequence according to a specified key selector function
    //     and projects the elements for each group by using a specified function.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> whose elements to group.
    //
    //   keySelector:
    //     A function to extract the key for each element.
    //
    //   elementSelector:
    //     A function to map each source element to an element in the System.Linq.IGrouping<TKey,TElement>.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by keySelector.
    //
    //   TElement:
    //     The type of the elements in the System.Linq.IGrouping<TKey,TElement>.
    //
    // Returns:
    //     An IEnumerable<IGrouping<TKey, TElement>> in C# or IEnumerable(Of IGrouping(Of
    //     TKey, TElement)) in Visual Basic where each System.Linq.IGrouping<TKey,TElement>
    //     object contains a collection of objects of type TElement and a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector or elementSelector is null.
    [Pure]
    public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);
      Contract.Ensures(Contract.Result<IEnumerable<IGrouping<TKey,TElement>>>() != null);

      throw new NotImplementedException();
    }
    //
    // Summary:
    //     Groups the elements of a sequence according to a specified key selector function
    //     and compares the keys by using a specified comparer.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> whose elements to group.
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
    //     The type of the key returned by keySelector.
    //
    // Returns:
    //     An IEnumerable<IGrouping<TKey, TSource>> in C# or IEnumerable(Of IGrouping(Of
    //     TKey, TSource)) in Visual Basic where each System.Linq.IGrouping<TKey,TElement>
    //     object contains a collection of objects and a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.
    [Pure]
    public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<IEnumerable<IGrouping<TKey,TSource>>>() != null);
      return null;
    }
    //
    // Summary:
    //     Groups the elements of a sequence according to a specified key selector function
    //     and creates a result value from each group and its key. The keys are compared
    //     by using a specified comparer.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> whose elements to group.
    //
    //   keySelector:
    //     A function to extract the key for each element.
    //
    //   resultSelector:
    //     A function to create a result value from each group.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to compare keys with.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by keySelector.
    //
    //   TResult:
    //     The type of the result value returned by resultSelector.
    //
    // Returns:
    //     A collection of elements of type TResult where each element represents a
    //     projection over a group and its key.
    [Pure]
    public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);

      return default(IEnumerable<TResult>);
    }
    //
    // Summary:
    //     Groups the elements of a sequence according to a specified key selector function
    //     and creates a result value from each group and its key. The elements of each
    //     group are projected by using a specified function.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> whose elements to group.
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
    //     The type of the key returned by keySelector.
    //
    //   TElement:
    //     The type of the elements in each System.Linq.IGrouping<TKey,TElement>.
    //
    //   TResult:
    //     The type of the result value returned by resultSelector.
    //
    // Returns:
    //     A collection of elements of type TResult where each element represents a
    //     projection over a group and its key.
    [Pure]
    public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);
      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      return default(IEnumerable<TResult>);
    }
    //
    // Summary:
    //     Groups the elements of a sequence according to a key selector function. The
    //     keys are compared by using a comparer and each group's elements are projected
    //     by using a specified function.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> whose elements to group.
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
    //     The type of the key returned by keySelector.
    //
    //   TElement:
    //     The type of the elements in the System.Linq.IGrouping<TKey,TElement>.
    //
    // Returns:
    //     An IEnumerable<IGrouping<TKey, TElement>> in C# or IEnumerable(Of IGrouping(Of
    //     TKey, TElement)) in Visual Basic where each System.Linq.IGrouping<TKey,TElement>
    //     object contains a collection of objects of type TElement and a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector or elementSelector is null.
    [Pure]
    public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);
      Contract.Ensures(Contract.Result<IEnumerable<IGrouping<TKey,TElement>>>() != null);
      return default(IEnumerable<IGrouping<TKey, TElement>>);
    }
    //
    // Summary:
    //     Groups the elements of a sequence according to a specified key selector function
    //     and creates a result value from each group and its key. Key values are compared
    //     by using a specified comparer, and the elements of each group are projected
    //     by using a specified function.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> whose elements to group.
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
    //     An System.Collections.Generic.IEqualityComparer<T> to compare keys with.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by keySelector.
    //
    //   TElement:
    //     The type of the elements in each System.Linq.IGrouping<TKey,TElement>.
    //
    //   TResult:
    //     The type of the result value returned by resultSelector.
    //
    // Returns:
    //     A collection of elements of type TResult where each element represents a
    //     projection over a group and its key.
    [Pure]
    public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      return default(IEnumerable<TResult>);
    }
    //
    // Summary:
    //     Correlates the elements of two sequences based on equality of keys and groups
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
    //     An System.Collections.Generic.IEnumerable<T> that contains elements of type
    //     TResult that are obtained by performing a grouped join on two sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     outer or inner or outerKeySelector or innerKeySelector or resultSelector
    //     is null.
    [Pure]
    public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
    {
      Contract.Requires(outer != null);
      Contract.Requires(inner != null);
      Contract.Requires(outerKeySelector != null);
      Contract.Requires(innerKeySelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      return default(IEnumerable<TResult>);
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
    //     An System.Collections.Generic.IEnumerable<T> that contains elements of type
    //     TResult that are obtained by performing a grouped join on two sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     outer or inner or outerKeySelector or innerKeySelector or resultSelector
    //     is null.
    [Pure]
    public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(outer != null);
      Contract.Requires(inner != null);
      Contract.Requires(outerKeySelector != null);
      Contract.Requires(innerKeySelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      return default(IEnumerable<TResult>);
    }
    //
    // Summary:
    //     Produces the set intersection of two sequences by using the default equality
    //     comparer to compare values.
    //
    // Parameters:
    //   first:
    //     An System.Collections.Generic.IEnumerable<T> whose distinct elements that
    //     also appear in second will be returned.
    //
    //   second:
    //     An System.Collections.Generic.IEnumerable<T> whose distinct elements that
    //     also appear in the first sequence will be returned.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of the input sequences.
    //
    // Returns:
    //     A sequence that contains the elements that form the set intersection of two
    //     sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     first or second is null.
    [Pure]
    public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      return default(IEnumerable<TSource>);
    }
    //
    // Summary:
    //     Produces the set intersection of two sequences by using the specified System.Collections.Generic.IEqualityComparer<T>
    //     to compare values.
    //
    // Parameters:
    //   first:
    //     An System.Collections.Generic.IEnumerable<T> whose distinct elements that
    //     also appear in second will be returned.
    //
    //   second:
    //     An System.Collections.Generic.IEnumerable<T> whose distinct elements that
    //     also appear in the first sequence will be returned.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to compare values.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of the input sequences.
    //
    // Returns:
    //     A sequence that contains the elements that form the set intersection of two
    //     sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     first or second is null.
    [Pure]
    public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      return default(IEnumerable<TSource>);
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
    //     An System.Collections.Generic.IEnumerable<T> that has elements of type TResult
    //     that are obtained by performing an inner join on two sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     outer or inner or outerKeySelector or innerKeySelector or resultSelector
    //     is null.
    [Pure]
    public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
    {
      Contract.Requires(outer != null);
      Contract.Requires(inner != null);
      Contract.Requires(outerKeySelector != null);
      Contract.Requires(innerKeySelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      return default(IEnumerable<TResult>);
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
    //     An System.Collections.Generic.IEnumerable<T> that has elements of type TResult
    //     that are obtained by performing an inner join on two sequences.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     outer or inner or outerKeySelector or innerKeySelector or resultSelector
    //     is null.
    [Pure]
    public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(outer != null);
      Contract.Requires(inner != null);
      Contract.Requires(outerKeySelector != null);
      Contract.Requires(innerKeySelector != null);
      Contract.Requires(resultSelector != null);
      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      return default(IEnumerable<TResult>);
    }
    //
    // Summary:
    //     Returns the last element of a sequence.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to return the last element of.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The value at the last position in the source sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    //
    //   System.InvalidOperationException:
    //     The source sequence is empty.
    [Pure]
    public static TSource Last<TSource>(this IEnumerable<TSource> source) {
      Contract.Requires(source != null);
      Contract.Requires(Count(source) > 0);
      
      return default(TSource);
    }
    //
    // Summary:
    //     Returns the last element of a sequence that satisfies a specified condition.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to return an element from.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The last element in the sequence that passes the test in the specified predicate
    //     function.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    //
    //   System.InvalidOperationException:
    //     No element satisfies the condition in predicate.  -or- The source sequence
    //     is empty.
    [Pure]
    public static TSource Last<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(Count(source) > 0);
      Contract.Requires(predicate != null);
      return default(TSource);
    }
    //
    // Summary:
    //     Returns the last element of a sequence, or a default value if the sequence
    //     contains no elements.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to return the last element of.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     default(TSource) if the source sequence is empty; otherwise, the last element
    //     in the System.Collections.Generic.IEnumerable<T>.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source) {
      Contract.Requires(source != null);
      
      return default(TSource);
    }
    //
    // Summary:
    //     Returns the last element of a sequence that satisfies a condition or a default
    //     value if no such element is found.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to return an element from.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     default(TSource) if the sequence is empty or if no elements pass the test
    //     in the predicate function; otherwise, the last element that passes the test
    //     in the predicate function.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      return default(TSource);
    }

    //
    // Summary:
    //     Returns an System.Int64 that represents the total number of elements in a
    //     sequence.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> that contains the elements to
    //     be counted.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The number of elements in the source sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    //
    //   System.OverflowException:
    //     The number of elements exceeds System.Int64.MaxValue.
    [Pure]
    public static long LongCount<TSource>(this IEnumerable<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<long>() >= 0);
      return default(long);
    }
    //
    // Summary:
    //     Returns an System.Int64 that represents how many elements in a sequence satisfy
    //     a condition.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> that contains the elements to
    //     be counted.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     A number that represents how many elements in the sequence satisfy the condition
    //     in the predicate function.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    //
    //   System.OverflowException:
    //     The number of matching elements exceeds System.Int64.MaxValue.
    [Pure]
    public static long LongCount<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      Contract.Ensures(Contract.Result<long>() >= 0);
      return default(long);
    }
    //
    // Summary:
    //     Returns the maximum value in a sequence of nullable System.Decimal values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Decimal values to determine the maximum value
    //     of.
    //
    // Returns:
    //     A value of type Nullable<Decimal> in C# or Nullable(Of Decimal) in Visual
    //     Basic that corresponds to the maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static decimal? Max(this IEnumerable<decimal?> source){
      Contract.Requires(source != null);
      return default(decimal?);
    }

    //
    // Summary:
    //     Returns the maximum value in a sequence of System.Decimal values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Decimal values to determine the maximum value of.
    //
    // Returns:
    //     The maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static decimal Max(this IEnumerable<decimal> source){
      Contract.Requires(source != null);
      return default(decimal);
    }

    //
    // Summary:
    //     Returns the maximum value in a sequence of nullable System.Double values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Double values to determine the maximum value
    //     of.
    //
    // Returns:
    //     A value of type Nullable<Double> in C# or Nullable(Of Double) in Visual Basic
    //     that corresponds to the maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static double? Max(this IEnumerable<double?> source){
      return default(double?);
    }

    //
    // Summary:
    //     Returns the maximum value in a sequence of System.Double values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Double values to determine the maximum value of.
    //
    // Returns:
    //     The maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static double Max(this IEnumerable<double> source) {
      Contract.Requires(source != null);
      return default(double);
    }

    //
    // Summary:
    //     Returns the maximum value in a sequence of nullable System.Single values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Single values to determine the maximum value
    //     of.
    //
    // Returns:
    //     A value of type Nullable<Single> in C# or Nullable(Of Single) in Visual Basic
    //     that corresponds to the maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static float? Max(this IEnumerable<float?> source){
      Contract.Requires(source != null);
      return default(float?);
    }

    //
    // Summary:
    //     Returns the maximum value in a sequence of System.Single values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Single values to determine the maximum value of.
    //
    // Returns:
    //     The maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static float Max(this IEnumerable<float> source) 
    {
      Contract.Requires(source != null);

      return default(float);
    }

    //
    // Summary:
    //     Returns the maximum value in a sequence of nullable System.Int32 values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Int32 values to determine the maximum value
    //     of.
    //
    // Returns:
    //     A value of type Nullable<Int32> in C# or Nullable(Of Int32) in Visual Basic
    //     that corresponds to the maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static int? Max(this IEnumerable<int?> source) 
    {
      Contract.Requires(source != null);

      return default(int?);
    }

    //
    // Summary:
    //     Returns the maximum value in a sequence of System.Int32 values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Int32 values to determine the maximum value of.
    //
    // Returns:
    //     The maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static int Max(this IEnumerable<int> source)
    {
      Contract.Requires(source != null);

      return default(int);
    }
    //
    // Summary:
    //     Returns the maximum value in a sequence of nullable System.Int64 values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Int64 values to determine the maximum value
    //     of.
    //
    // Returns:
    //     A value of type Nullable<Int64> in C# or Nullable(Of Int64) in Visual Basic that
    //     corresponds to the maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static long? Max(this IEnumerable<long?> source)
    {
      Contract.Requires(source != null);
      return default(long?);
    }
    //
    // Summary:
    //     Returns the maximum value in a sequence of System.Int64 values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Int64 values to determine the maximum value of.
    //
    // Returns:
    //     The maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static long Max(this IEnumerable<long> source)
    {
      Contract.Requires(source != null);

      return default(long); 
    }
    //
    // Summary:
    //     Returns the maximum value in a generic sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the maximum value of.
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
    public static TSource Max<TSource>(this IEnumerable<TSource> source)
    {
      Contract.Requires(source != null);

      return default(TSource); 
    }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     maximum nullable System.Decimal value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the maximum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The value of type Nullable<Decimal> in C# or Nullable(Of Decimal) in Visual
    //     Basic that corresponds to the maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static decimal? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return null; }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     maximum System.Decimal value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the maximum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    //     source or selector is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static decimal Max<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(decimal); }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     maximum nullable System.Double value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the maximum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The value of type Nullable<Double> in C# or Nullable(Of Double) in Visual
    //     Basic that corresponds to the maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static double? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);

      return null; }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     maximum System.Double value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the maximum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    //     source or selector is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static double Max<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);

      return default(double); }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     maximum nullable System.Single value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the maximum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The value of type Nullable<Single> in C# or Nullable(Of Single) in Visual
    //     Basic that corresponds to the maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static float? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);

      return null; }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     maximum System.Single value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the maximum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    //     source or selector is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static float Max<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);

      return default(float);
    }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     maximum nullable System.Int32 value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the maximum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The value of type Nullable<Int32> in C# or Nullable(Of Int32) in Visual Basic
    //     that corresponds to the maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static int? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);

      return null; 
    }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     maximum System.Int32 value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the maximum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    //     source or selector is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static int Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);

      return default(int); 
    }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     maximum nullable System.Int64 value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the maximum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The value of type Nullable<Int64> in C# or Nullable(Of Int64) in Visual Basic that
    //     corresponds to the maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static long? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);


      return null; 
    }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     maximum System.Int64 value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the maximum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    //     source or selector is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static long Max<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);

      return default(long); 
    }
    //
    // Summary:
    //     Invokes a transform function on each element of a generic sequence and returns
    //     the maximum resulting value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the maximum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TResult:
    //     The type of the value returned by selector.
    //
    // Returns:
    //     The maximum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static TResult Max<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
  
      return default(TResult); 
    }
    //
    // Summary:
    //     Returns the minimum value in a sequence of nullable System.Decimal values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Decimal values to determine the minimum value
    //     of.
    //
    // Returns:
    //     A value of type Nullable<Decimal> in C# or Nullable(Of Decimal) in Visual
    //     Basic that corresponds to the minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static decimal? Min(this IEnumerable<decimal?> source)
    {
      Contract.Requires(source != null);
     
      return null; 
    }
    //
    // Summary:
    //     Returns the minimum value in a sequence of System.Decimal values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Decimal values to determine the minimum value of.
    //
    // Returns:
    //     The minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static decimal Min(this IEnumerable<decimal> source)
    {
      Contract.Requires(source != null);

      return default(decimal);
    }
    //
    // Summary:
    //     Returns the minimum value in a sequence of nullable System.Double values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Double values to determine the minimum value
    //     of.
    //
    // Returns:
    //     A value of type Nullable<Double> in C# or Nullable(Of Double) in Visual Basic
    //     that corresponds to the minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static double? Min(this IEnumerable<double?> source)
    {
      Contract.Requires(source != null);

      return null;
    }
    //
    // Summary:
    //     Returns the minimum value in a sequence of System.Double values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Double values to determine the minimum value of.
    //
    // Returns:
    //     The minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static double Min(this IEnumerable<double> source)
    {
      Contract.Requires(source != null);

      return default(double);
    }
    //
    // Summary:
    //     Returns the minimum value in a sequence of nullable System.Single values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Single values to determine the minimum value
    //     of.
    //
    // Returns:
    //     A value of type Nullable<Single> in C# or Nullable(Of Single) in Visual Basic
    //     that corresponds to the minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static float? Min(this IEnumerable<float?> source)
    {
      Contract.Requires(source != null);

      return null;
    }
    //
    // Summary:
    //     Returns the minimum value in a sequence of System.Single values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Single values to determine the minimum value of.
    //
    // Returns:
    //     The minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static float Min(this IEnumerable<float> source)
    {
      Contract.Requires(source != null);
      return default(float); }
    //
    // Summary:
    //     Returns the minimum value in a sequence of nullable System.Int32 values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Int32 values to determine the minimum value
    //     of.
    //
    // Returns:
    //     A value of type Nullable<Int32> in C# or Nullable(Of Int32) in Visual Basic
    //     that corresponds to the minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static int? Min(this IEnumerable<int?> source)
    {
      Contract.Requires(source != null);

      return null;
    }
    //
    // Summary:
    //     Returns the minimum value in a sequence of System.Int32 values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Int32 values to determine the minimum value of.
    //
    // Returns:
    //     The minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static int Min(this IEnumerable<int> source)
    {
        Contract.Requires(source != null);

      return default(int);
    }
    //
    // Summary:
    //     Returns the minimum value in a sequence of nullable System.Int64 values.
    //
    // Parameters:
    //   source:
    //     A sequence of nullable System.Int64 values to determine the minimum value
    //     of.
    //
    // Returns:
    //     A value of type Nullable<Int64> in C# or Nullable(Of Int64) in Visual Basic
    //     that corresponds to the minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static long? Min(this IEnumerable<long?> source)
    {
      Contract.Requires(source != null);

      return null;
    }
    //
    // Summary:
    //     Returns the minimum value in a sequence of System.Int64 values.
    //
    // Parameters:
    //   source:
    //     A sequence of System.Int64 values to determine the minimum value of.
    //
    // Returns:
    //     The minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static long Min(this IEnumerable<long> source)
    {
      Contract.Requires(source != null);

      return default(long);
    }
    //
    // Summary:
    //     Returns the minimum value in a generic sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the minimum value of.
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
    public static TSource Min<TSource>(this IEnumerable<TSource> source)
    {
      Contract.Requires(source != null);
      return default(TSource); }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     minimum nullable System.Decimal value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the minimum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The value of type Nullable<Decimal> in C# or Nullable(Of Decimal) in Visual
    //     Basic that corresponds to the minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static decimal? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);

      return null;
    }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     minimum System.Decimal value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the minimum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    //     source or selector is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static decimal Min<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
    { 
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(decimal); 
    }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     minimum nullable System.Double value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the minimum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The value of type Nullable<Double> in C# or Nullable(Of Double) in Visual
    //     Basic that corresponds to the minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static double? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);

      return null;
    }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     minimum System.Double value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the minimum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    //     source or selector is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static double Min<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      return default(double); }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     minimum nullable System.Single value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the minimum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The value of type Nullable<Single> in C# or Nullable(Of Single) in Visual
    //     Basic that corresponds to the minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static float? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);

      return null;
    }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     minimum System.Single value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the minimum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    //     source or selector is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static float Min<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
    { Contract.Requires(source != null); Contract.Requires(selector != null); return default(float); }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     minimum nullable System.Int32 value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the minimum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The value of type Nullable<Int32> in C# or Nullable(Of Int32) in Visual Basic
    //     that corresponds to the minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static int? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);

      return null;
    }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     minimum System.Int32 value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the minimum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    //     source or selector is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static int Min<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
    { Contract.Requires(source != null); Contract.Requires(selector != null); return default(int); }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     minimum nullable System.Int64 value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the minimum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The value of type Nullable<Int64> in C# or Nullable(Of Int64) in Visual Basic
    //     that corresponds to the minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static long? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
    { Contract.Requires(source != null); Contract.Requires(selector != null); return null; }
    //
    // Summary:
    //     Invokes a transform function on each element of a sequence and returns the
    //     minimum System.Int64 value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the minimum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    //     source or selector is null.
    //
    //   System.InvalidOperationException:
    //     source contains no elements.
    [Pure]
    public static long Min<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
    { Contract.Requires(source != null); Contract.Requires(selector != null); return default(long); }

      //
    // Summary:
    //     Invokes a transform function on each element of a generic sequence and returns
    //     the minimum resulting value.
    //
    // Parameters:
    //   source:
    //     A sequence of values to determine the minimum value of.
    //
    //   selector:
    //     A transform function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TResult:
    //     The type of the value returned by selector.
    //
    // Returns:
    //     The minimum value in the sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static TResult Min<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    { Contract.Requires(source != null); Contract.Requires(selector != null); return default(TResult); }
    //
    // Summary:
    //     Filters the elements of an System.Collections.IEnumerable based on a specified
    //     type.
    //
    // Parameters:
    //   source:
    //     The System.Collections.IEnumerable whose elements to filter.
    //
    // Type parameters:
    //   TResult:
    //     The type to filter the elements of the sequence on.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains elements from
    //     the input sequence of type TResult.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static IEnumerable<TResult> OfType<TResult>(this IEnumerable source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<TResult>>(), _ => _ != null));

      return default(IEnumerable<TResult>);
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
    //     The type of the key returned by keySelector.
    //
    // Returns:
    //     An System.Linq.IOrderedEnumerable<TElement> whose elements are sorted according
    //     to a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.
    [Pure]
    public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<IOrderedEnumerable<TSource>>() != null);
      return default(IOrderedEnumerable<TSource>);
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
    //     The type of the key returned by keySelector.
    //
    // Returns:
    //     An System.Linq.IOrderedEnumerable<TElement> whose elements are sorted according
    //     to a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.
    [Pure]
    public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<IOrderedEnumerable<TSource>>() != null);
      return default(IOrderedEnumerable<TSource>);
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
    //     The type of the key returned by keySelector.
    //
    // Returns:
    //     An System.Linq.IOrderedEnumerable<TElement> whose elements are sorted in
    //     descending order according to a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.
    [Pure]
    public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<IOrderedEnumerable<TSource>>() != null);
      return default(IOrderedEnumerable<TSource>);
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
    //     The type of the key returned by keySelector.
    //
    // Returns:
    //     An System.Linq.IOrderedEnumerable<TElement> whose elements are sorted in
    //     descending order according to a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.
    [Pure]
    public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<IOrderedEnumerable<TSource>>() != null);
      return default(IOrderedEnumerable<TSource>);
    }
    //
    // Summary:
    //     Generates a sequence of integral numbers within a specified range.
    //
    // Parameters:
    //   start:
    //     The value of the first integer in the sequence.
    //
    //   count:
    //     The number of sequential integers to generate.
    //
    // Returns:
    //     An IEnumerable<Int32> in C# or IEnumerable(Of Int32) in Visual Basic that
    //     contains a range of sequential integral numbers.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     count is less than 0.  -or- start + count -1 is larger than System.Int32.MaxValue.
    [Pure]
    public static IEnumerable<int> Range(int start, int count)
    {
      Contract.Requires(count >= 0);
      Contract.Requires(((long)start + count -1) <= Int32.MaxValue);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<int>>(), x => x >= start));
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<int>>(), x => x < start + count));

      Contract.Ensures(Contract.Result<IEnumerable<int>>() != null);
      Contract.Ensures(Count(Contract.Result<IEnumerable<int>>()) == count);
      return default(IEnumerable<int>);
    }
    //
    // Summary:
    //     Generates a sequence that contains one repeated value.
    //
    // Parameters:
    //   element:
    //     The value to be repeated.
    //
    //   count:
    //     The number of times to repeat the value in the generated sequence.
    //
    // Type parameters:
    //   TResult:
    //     The type of the value to be repeated in the result sequence.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains a repeated value.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     count is less than 0.
    [Pure]
    public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
    {
      Contract.Requires(count >= 0);
      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      Contract.Ensures(Count(Contract.Result<IEnumerable<TResult>>()) == count);
      return default(IEnumerable<TResult>);
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
    //     A sequence whose elements correspond to those of the input sequence in reverse
    //     order.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      Contract.Ensures(Count(Contract.Result<IEnumerable<TSource>>()) == Count(source));
      return default(IEnumerable<TSource>);
    }
    //
    // Summary:
    //     Projects each element of a sequence into a new form by incorporating the
    //     element's index.
    //
    // Parameters:
    //   source:
    //     A sequence of values to invoke a transform function on.
    //
    //   selector:
    //     A transform function to apply to each source element; the second parameter
    //     of the function represents the index of the source element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TResult:
    //     The type of the value returned by selector.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> whose elements are the result
    //     of invoking the transform function on each element of source.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      Contract.Ensures(Count(Contract.Result<IEnumerable<TResult>>()) == Count(source));
      return default(IEnumerable<TResult>);
    }
    //
    // Summary:
    //     Projects each element of a sequence into a new form.
    //
    // Parameters:
    //   source:
    //     A sequence of values to invoke a transform function on.
    //
    //   selector:
    //     A transform function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TResult:
    //     The type of the value returned by selector.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> whose elements are the result
    //     of invoking the transform function on each element of source.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      return default(IEnumerable<TResult>);
    }
    //
    // Summary:
    //     Projects each element of a sequence to an System.Collections.Generic.IEnumerable<T>
    //     and flattens the resulting sequences into one sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values to project.
    //
    //   selector:
    //     A transform function to apply to each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TResult:
    //     The type of the elements of the sequence returned by selector.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> whose elements are the result
    //     of invoking the one-to-many transform function on each element of the input
    //     sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      return default(IEnumerable<TResult>);
    }
    //
    // Summary:
    //     Projects each element of a sequence to an System.Collections.Generic.IEnumerable<T>,
    //     and flattens the resulting sequences into one sequence. The index of each
    //     source element is used in the projected form of that element.
    //
    // Parameters:
    //   source:
    //     A sequence of values to project.
    //
    //   selector:
    //     A transform function to apply to each source element; the second parameter
    //     of the function represents the index of the source element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TResult:
    //     The type of the elements of the sequence returned by selector.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> whose elements are the result
    //     of invoking the one-to-many transform function on each element of an input
    //     sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or selector is null.
    [Pure]
    public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
    {
      Contract.Requires(source != null);
      Contract.Requires(selector != null);
      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      return default(IEnumerable<TResult>);
    }
    //
    // Summary:
    //     Projects each element of a sequence to an System.Collections.Generic.IEnumerable<T>,
    //     flattens the resulting sequences into one sequence, and invokes a result
    //     selector function on each element therein.
    //
    // Parameters:
    //   source:
    //     A sequence of values to project.
    //
    //   collectionSelector:
    //     A transform function to apply to each element of the input sequence.
    //
    //   resultSelector:
    //     A transform function to apply to each element of the intermediate sequence.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TCollection:
    //     The type of the intermediate elements collected by collectionSelector.
    //
    //   TResult:
    //     The type of the elements of the resulting sequence.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> whose elements are the result
    //     of invoking the one-to-many transform function collectionSelector on each
    //     element of source and then mapping each of those sequence elements and their
    //     corresponding source element to a result element.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or collectionSelector or resultSelector is null.
    [Pure]
    public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(collectionSelector != null);
      Contract.Requires(resultSelector != null);

      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      return default(IEnumerable<TResult>);
    }
    //
    // Summary:
    //     Projects each element of a sequence to an System.Collections.Generic.IEnumerable<T>,
    //     flattens the resulting sequences into one sequence, and invokes a result
    //     selector function on each element therein. The index of each source element
    //     is used in the intermediate projected form of that element.
    //
    // Parameters:
    //   source:
    //     A sequence of values to project.
    //
    //   collectionSelector:
    //     A transform function to apply to each source element; the second parameter
    //     of the function represents the index of the source element.
    //
    //   resultSelector:
    //     A transform function to apply to each element of the intermediate sequence.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TCollection:
    //     The type of the intermediate elements collected by collectionSelector.
    //
    //   TResult:
    //     The type of the elements of the resulting sequence.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> whose elements are the result
    //     of invoking the one-to-many transform function collectionSelector on each
    //     element of source and then mapping each of those sequence elements and their
    //     corresponding source element to a result element.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or collectionSelector or resultSelector is null.
    [Pure]
    public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(collectionSelector != null);
      Contract.Requires(resultSelector != null);

      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      return default(IEnumerable<TResult>);
    }
    //
    // Summary:
    //     Determines whether two sequences are equal by comparing the elements by using
    //     the default equality comparer for their type.
    //
    // Parameters:
    //   first:
    //     An System.Collections.Generic.IEnumerable<T> to compare to second.
    //
    //   second:
    //     An System.Collections.Generic.IEnumerable<T> to compare to the first sequence.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of the input sequences.
    //
    // Returns:
    //     true if the two source sequences are of equal length and their corresponding
    //     elements are equal according to the default equality comparer for their type;
    //     otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     first or second is null.
    [Pure]
    public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      return default(bool);
    }
    //
    // Summary:
    //     Determines whether two sequences are equal by comparing their elements by
    //     using a specified System.Collections.Generic.IEqualityComparer<T>.
    //
    // Parameters:
    //   first:
    //     An System.Collections.Generic.IEnumerable<T> to compare to second.
    //
    //   second:
    //     An System.Collections.Generic.IEnumerable<T> to compare to the first sequence.
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
    //     elements compare equal according to comparer; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     first or second is null.
    [Pure]
    public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      return default(bool);
    }
    //
    // Summary:
    //     Returns the only element of a sequence, and throws an exception if there
    //     is not exactly one element in the sequence.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to return the single element
    //     of.
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
    //
    //   System.InvalidOperationException:
    //     The input sequence contains more than one element.  -or- The input sequence
    //     is empty.
    [Pure]
    public static TSource Single<TSource>(this IEnumerable<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Requires(Count(source) == 1);
      return default(TSource);
    }
    //
    // Summary:
    //     Returns the only element of a sequence that satisfies a specified condition,
    //     and throws an exception if more than one such element exists.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to return a single element from.
    //
    //   predicate:
    //     A function to test an element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The single element of the input sequence that satisfies a condition.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    //
    //   System.InvalidOperationException:
    //     No element satisfies the condition in predicate.  -or- More than one element
    //     satisfies the condition in predicate.  -or- The source sequence is empty.
    [Pure]
    public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(Count(source) > 0);
      Contract.Requires(predicate != null);
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
    //     An System.Collections.Generic.IEnumerable<T> to return the single element
    //     of.
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
    //
    //   System.InvalidOperationException:
    //     The input sequence contains more than one element.
    [Pure]
    public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source)
    {
      Contract.Requires(source != null);
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
    //     An System.Collections.Generic.IEnumerable<T> to return a single element from.
    //
    //   predicate:
    //     A function to test an element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     The single element of the input sequence that satisfies the condition, or
    //     default(TSource) if no such element is found.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    //
    //   System.InvalidOperationException:
    //     More than one element satisfies the condition in predicate.
    [Pure]
    public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      return default(TSource);
    }
    //
    // Summary:
    //     Bypasses a specified number of elements in a sequence and then returns the
    //     remaining elements.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to return elements from.
    //
    //   count:
    //     The number of elements to skip before returning the remaining elements.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains the elements that
    //     occur after the specified index in the input sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source, int count)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      Contract.Ensures(Count(Contract.Result<IEnumerable<TSource>>()) <= Count(source));
      return default(IEnumerable<TSource>);
    }
    //
    // Summary:
    //     Bypasses elements in a sequence as long as a specified condition is true
    //     and then returns the remaining elements.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to return elements from.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains the elements from
    //     the input sequence starting at the first element in the linear series that
    //     does not pass the test specified by predicate.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);

      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      Contract.Ensures(Count(Contract.Result<IEnumerable<TSource>>()) <= Count(source));
      return default(IEnumerable<TSource>);
    }
    //
    // Summary:
    //     Bypasses elements in a sequence as long as a specified condition is true
    //     and then returns the remaining elements. The element's index is used in the
    //     logic of the predicate function.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to return elements from.
    //
    //   predicate:
    //     A function to test each source element for a condition; the second parameter
    //     of the function represents the index of the source element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains the elements from
    //     the input sequence starting at the first element in the linear series that
    //     does not pass the test specified by predicate.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);

      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      Contract.Ensures(Count(Contract.Result<IEnumerable<TSource>>()) <= Count(source));
      return default(IEnumerable<TSource>);
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
    //
    //   System.OverflowException:
    //     The sum is larger than System.Decimal.MaxValue.
    [Pure]
    public static decimal? Sum(this IEnumerable<decimal?> source)
    {
      Contract.Requires(source != null);
      return null; }
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
    //
    //   System.OverflowException:
    //     The sum is larger than System.Decimal.MaxValue.
    [Pure]
    public static decimal Sum(this IEnumerable<decimal> source)
    { Contract.Requires(source != null); return default(decimal); }
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
    public static double? Sum(this IEnumerable<double?> source)
    { Contract.Requires(source != null); return null; }
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
    public static double Sum(this IEnumerable<double> source)
    { Contract.Requires(source != null); return default(double); }
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
    public static float? Sum(this IEnumerable<float?> source)
    { Contract.Requires(source != null); return null; }
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
    public static float Sum(this IEnumerable<float> source)
    { Contract.Requires(source != null); return default(float); }
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
    //
    //   System.OverflowException:
    //     The sum is larger than System.Int32.MaxValue.
    [Pure]
    public static int? Sum(this IEnumerable<int?> source)
    { Contract.Requires(source != null); return null; }
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
    //
    //   System.OverflowException:
    //     The sum is larger than System.Int32.MaxValue.
    [Pure]
    public static int Sum(this IEnumerable<int> source)
    { Contract.Requires(source != null); return default(int); }
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
    //
    //   System.OverflowException:
    //     The sum is larger than System.Int64.MaxValue.
    [Pure]
    public static long? Sum(this IEnumerable<long?> source)
    { Contract.Requires(source != null); return null; }
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
    //
    //   System.OverflowException:
    //     The sum is larger than System.Int64.MaxValue.
    [Pure]
    public static long Sum(this IEnumerable<long> source)
    { Contract.Requires(source != null); return default(long); }
    //
    // Summary:
    //     Computes the sum of the sequence of nullable System.Decimal values that are
    //     obtained by invoking a transform function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values that are used to calculate a sum.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    //
    //   System.OverflowException:
    //     The sum is larger than System.Decimal.MaxValue.
    [Pure]
    public static decimal? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
    { Contract.Requires(source != null); Contract.Requires(selector != null); return null; }
    //
    // Summary:
    //     Computes the sum of the sequence of System.Decimal values that are obtained
    //     by invoking a transform function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values that are used to calculate a sum.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    //
    //   System.OverflowException:
    //     The sum is larger than System.Decimal.MaxValue.
    [Pure]
    public static decimal Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
    { Contract.Requires(source != null); return default(decimal); }
    //
    // Summary:
    //     Computes the sum of the sequence of nullable System.Double values that are
    //     obtained by invoking a transform function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values that are used to calculate a sum.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    public static double? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
    { Contract.Requires(source != null); return null; }
    //
    // Summary:
    //     Computes the sum of the sequence of System.Double values that are obtained
    //     by invoking a transform function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values that are used to calculate a sum.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    public static double Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
    { Contract.Requires(source != null); return default(double); }
    //
    // Summary:
    //     Computes the sum of the sequence of nullable System.Single values that are
    //     obtained by invoking a transform function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values that are used to calculate a sum.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    public static float? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
    { Contract.Requires(source != null); return null; }
    //
    // Summary:
    //     Computes the sum of the sequence of System.Single values that are obtained
    //     by invoking a transform function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values that are used to calculate a sum.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    public static float Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
    { Contract.Requires(source != null); return default(float); }
    //
    // Summary:
    //     Computes the sum of the sequence of nullable System.Int32 values that are
    //     obtained by invoking a transform function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values that are used to calculate a sum.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    //
    //   System.OverflowException:
    //     The sum is larger than System.Int32.MaxValue.
    [Pure]
    public static int? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
    { Contract.Requires(source != null); return null; }
    //
    // Summary:
    //     Computes the sum of the sequence of System.Int32 values that are obtained
    //     by invoking a transform function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values that are used to calculate a sum.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    //
    //   System.OverflowException:
    //     The sum is larger than System.Int32.MaxValue.
    [Pure]
    public static int Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
    { Contract.Requires(source != null); return default(int); }
    //
    // Summary:
    //     Computes the sum of the sequence of nullable System.Int64 values that are
    //     obtained by invoking a transform function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values that are used to calculate a sum.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    //
    //   System.OverflowException:
    //     The sum is larger than System.Int64.MaxValue.
    [Pure]
    public static long? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
    { Contract.Requires(source != null); return null; }
    //
    // Summary:
    //     Computes the sum of the sequence of System.Int64 values that are obtained
    //     by invoking a transform function on each element of the input sequence.
    //
    // Parameters:
    //   source:
    //     A sequence of values that are used to calculate a sum.
    //
    //   selector:
    //     A transform function to apply to each element.
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
    //
    //   System.OverflowException:
    //     The sum is larger than System.Int64.MaxValue.
    [Pure]
    public static long Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
    { Contract.Requires(source != null); Contract.Requires(selector != null); return default(long); }
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
    //     An System.Collections.Generic.IEnumerable<T> that contains the specified
    //     number of elements from the start of the input sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, int count)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      return default(IEnumerable<TSource>);
    }
    //
    // Summary:
    //     Returns elements from a sequence as long as a specified condition is true.
    //
    // Parameters:
    //   source:
    //     A sequence to return elements from.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains the elements from
    //     the input sequence that occur before the element at which the test no longer
    //     passes.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      return default(IEnumerable<TSource>);
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
    //     A function to test each source element for a condition; the second parameter
    //     of the function represents the index of the source element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains elements from
    //     the input sequence that occur before the element at which the test no longer
    //     passes.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      return default(IEnumerable<TSource>);
    }
    //
    // Summary:
    //     Performs a subsequent ordering of the elements in a sequence in ascending
    //     order according to a key.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IOrderedEnumerable<TElement> that contains elements to sort.
    //
    //   keySelector:
    //     A function to extract a key from each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by keySelector.
    //
    // Returns:
    //     An System.Linq.IOrderedEnumerable<TElement> whose elements are sorted according
    //     to a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.
    [Pure]
    public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<IOrderedEnumerable<TSource>>() != null);
      return default(IOrderedEnumerable<TSource>);
    }
    //
    // Summary:
    //     Performs a subsequent ordering of the elements in a sequence in ascending
    //     order by using a specified comparer.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IOrderedEnumerable<TElement> that contains elements to sort.
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
    //     The type of the key returned by keySelector.
    //
    // Returns:
    //     An System.Linq.IOrderedEnumerable<TElement> whose elements are sorted according
    //     to a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.
    [Pure]
    public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<IOrderedEnumerable<TSource>>() != null);
      return default(IOrderedEnumerable<TSource>);
    }
    //
    // Summary:
    //     Performs a subsequent ordering of the elements in a sequence in descending
    //     order, according to a key.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IOrderedEnumerable<TElement> that contains elements to sort.
    //
    //   keySelector:
    //     A function to extract a key from each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by keySelector.
    //
    // Returns:
    //     An System.Linq.IOrderedEnumerable<TElement> whose elements are sorted in
    //     descending order according to a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.
    [Pure]
    public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<IOrderedEnumerable<TSource>>() != null);
      return default(IOrderedEnumerable<TSource>);
    }
    //
    // Summary:
    //     Performs a subsequent ordering of the elements in a sequence in descending
    //     order by using a specified comparer.
    //
    // Parameters:
    //   source:
    //     An System.Linq.IOrderedEnumerable<TElement> that contains elements to sort.
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
    //     The type of the key returned by keySelector.
    //
    // Returns:
    //     An System.Linq.IOrderedEnumerable<TElement> whose elements are sorted in
    //     descending order according to a key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.
    [Pure]
    public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<IOrderedEnumerable<TSource>>() != null);
      return default(IOrderedEnumerable<TSource>);
    }

    [Pure]
    public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<TSource[]>() != null);
      Contract.Ensures(Contract.Result<TSource[]>().Length == source.Count());
      
      return default(TSource[]);
    }
    //
    // Summary:
    //     Creates a System.Collections.Generic.Dictionary<TKey,TValue> from an System.Collections.Generic.IEnumerable<T>
    //     according to a specified key selector function.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to create a System.Collections.Generic.Dictionary<TKey,TValue>
    //     from.
    //
    //   keySelector:
    //     A function to extract a key from each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by keySelector.
    //
    // Returns:
    //     A System.Collections.Generic.Dictionary<TKey,TValue> that contains keys and
    //     values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.  -or- keySelector produces a key that is null.
    //
    //   System.ArgumentException:
    //     keySelector produces duplicate keys for two elements.
    [Pure]
    public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<Dictionary<TKey,TSource>>() != null);
      Contract.Ensures(Contract.Result<Dictionary<TKey, TSource>>().Count == Count(source));
      return default(Dictionary<TKey, TSource>);
    }
    //
    // Summary:
    //     Creates a System.Collections.Generic.Dictionary<TKey,TValue> from an System.Collections.Generic.IEnumerable<T>
    //     according to specified key selector and element selector functions.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to create a System.Collections.Generic.Dictionary<TKey,TValue>
    //     from.
    //
    //   keySelector:
    //     A function to extract a key from each element.
    //
    //   elementSelector:
    //     A transform function to produce a result element value from each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by keySelector.
    //
    //   TElement:
    //     The type of the value returned by elementSelector.
    //
    // Returns:
    //     A System.Collections.Generic.Dictionary<TKey,TValue> that contains values
    //     of type TElement selected from the input sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector or elementSelector is null.  -or- keySelector produces
    //     a key that is null.
    //
    //   System.ArgumentException:
    //     keySelector produces duplicate keys for two elements.
    [Pure]
    public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);

      Contract.Ensures(Contract.Result<Dictionary<TKey, TElement>>() != null);
      Contract.Ensures(Contract.Result<Dictionary<TKey, TSource>>().Count == Count(source));
      return default(Dictionary<TKey, TElement>);
    }
    //
    // Summary:
    //     Creates a System.Collections.Generic.Dictionary<TKey,TValue> from an System.Collections.Generic.IEnumerable<T>
    //     according to a specified key selector function and key comparer.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to create a System.Collections.Generic.Dictionary<TKey,TValue>
    //     from.
    //
    //   keySelector:
    //     A function to extract a key from each element.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to compare keys.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the keys returned by keySelector.
    //
    // Returns:
    //     A System.Collections.Generic.Dictionary<TKey,TValue> that contains keys and
    //     values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.  -or- keySelector produces a key that is null.
    //
    //   System.ArgumentException:
    //     keySelector produces duplicate keys for two elements.
    [Pure]
    public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);

      Contract.Ensures(Contract.Result<Dictionary<TKey, TSource>>() != null);
      Contract.Ensures(Contract.Result<Dictionary<TKey, TSource>>().Count == Count(source));
      return default(Dictionary<TKey, TSource>);
    }
    //
    // Summary:
    //     Creates a System.Collections.Generic.Dictionary<TKey,TValue> from an System.Collections.Generic.IEnumerable<T>
    //     according to a specified key selector function, a comparer, and an element
    //     selector function.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to create a System.Collections.Generic.Dictionary<TKey,TValue>
    //     from.
    //
    //   keySelector:
    //     A function to extract a key from each element.
    //
    //   elementSelector:
    //     A transform function to produce a result element value from each element.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to compare keys.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by keySelector.
    //
    //   TElement:
    //     The type of the value returned by elementSelector.
    //
    // Returns:
    //     A System.Collections.Generic.Dictionary<TKey,TValue> that contains values
    //     of type TElement selected from the input sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector or elementSelector is null.  -or- keySelector produces
    //     a key that is null.
    //
    //   System.ArgumentException:
    //     keySelector produces duplicate keys for two elements.
    [Pure]
    public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);

      Contract.Ensures(Contract.Result<Dictionary<TKey, TElement>>() != null);
      Contract.Ensures(Contract.Result<Dictionary<TKey, TSource>>().Count == Count(source));
      return default(Dictionary<TKey, TElement>);
    }
    //
    // Summary:
    //     Creates a System.Collections.Generic.List<T> from an System.Collections.Generic.IEnumerable<T>.
    //
    // Parameters:
    //   source:
    //     The System.Collections.Generic.IEnumerable<T> to create a System.Collections.Generic.List<T>
    //     from.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     A System.Collections.Generic.List<T> that contains elements from the input
    //     sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source is null.
    [Pure]
    public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
    {
      Contract.Requires(source != null);
      Contract.Ensures(Contract.Result<List<TSource>>() != null);
      return default(List<TSource>);
    }
    //
    // Summary:
    //     Creates a System.Linq.Lookup<TKey,TElement> from an System.Collections.Generic.IEnumerable<T>
    //     according to a specified key selector function.
    //
    // Parameters:
    //   source:
    //     The System.Collections.Generic.IEnumerable<T> to create a System.Linq.Lookup<TKey,TElement>
    //     from.
    //
    //   keySelector:
    //     A function to extract a key from each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by keySelector.
    //
    // Returns:
    //     A System.Linq.Lookup<TKey,TElement> that contains keys and values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.
    [Pure]
    public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<ILookup<TKey,TSource>>() != null);
      return default(ILookup<TKey, TSource>);
    }
    //
    // Summary:
    //     Creates a System.Linq.Lookup<TKey,TElement> from an System.Collections.Generic.IEnumerable<T>
    //     according to specified key selector and element selector functions.
    //
    // Parameters:
    //   source:
    //     The System.Collections.Generic.IEnumerable<T> to create a System.Linq.Lookup<TKey,TElement>
    //     from.
    //
    //   keySelector:
    //     A function to extract a key from each element.
    //
    //   elementSelector:
    //     A transform function to produce a result element value from each element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by keySelector.
    //
    //   TElement:
    //     The type of the value returned by elementSelector.
    //
    // Returns:
    //     A System.Linq.Lookup<TKey,TElement> that contains values of type TElement
    //     selected from the input sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector or elementSelector is null.
    [Pure]
    public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);
      Contract.Ensures(Contract.Result<ILookup<TKey,TElement>>() != null);
      return default(ILookup<TKey, TElement>);
    }
    //
    // Summary:
    //     Creates a System.Linq.Lookup<TKey,TElement> from an System.Collections.Generic.IEnumerable<T>
    //     according to a specified key selector function and key comparer.
    //
    // Parameters:
    //   source:
    //     The System.Collections.Generic.IEnumerable<T> to create a System.Linq.Lookup<TKey,TElement>
    //     from.
    //
    //   keySelector:
    //     A function to extract a key from each element.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to compare keys.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by keySelector.
    //
    // Returns:
    //     A System.Linq.Lookup<TKey,TElement> that contains keys and values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or keySelector is null.
    [Pure]
    public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Ensures(Contract.Result<ILookup<TKey, TSource>>() != null);
      return default(ILookup<TKey, TSource>);
    }
    //
    // Summary:
    //     Creates a System.Linq.Lookup<TKey,TElement> from an System.Collections.Generic.IEnumerable<T>
    //     according to a specified key selector function, a comparer and an element
    //     selector function.
    //
    // Parameters:
    //   source:
    //     The System.Collections.Generic.IEnumerable<T> to create a System.Linq.Lookup<TKey,TElement>
    //     from.
    //
    //   keySelector:
    //     A function to extract a key from each element.
    //
    //   elementSelector:
    //     A transform function to produce a result element value from each element.
    //
    //   comparer:
    //     An System.Collections.Generic.IEqualityComparer<T> to compare keys.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    //   TKey:
    //     The type of the key returned by keySelector.
    //
    //   TElement:
    //     The type of the value returned by elementSelector.
    //
    // Returns:
    //     A System.Linq.Lookup<TKey,TElement> that contains values of type TElement
    //     selected from the input sequence.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    // source or keySelector or elementSelector is null.
    [Pure]
    public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(source != null);
      Contract.Requires(keySelector != null);
      Contract.Requires(elementSelector != null);
      Contract.Ensures(Contract.Result<ILookup<TKey, TElement>>() != null);
      return default(ILookup<TKey, TElement>);
    }
    //
    // Summary:
    //     Produces the set union of two sequences by using the default equality comparer.
    //
    // Parameters:
    //   first:
    //     An System.Collections.Generic.IEnumerable<T> whose distinct elements form
    //     the first set for the union.
    //
    //   second:
    //     An System.Collections.Generic.IEnumerable<T> whose distinct elements form
    //     the second set for the union.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of the input sequences.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains the elements from
    //     both input sequences, excluding duplicates.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     first or second is null.
    [Pure]
    public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      Contract.Ensures(Count(Contract.Result<IEnumerable<TSource>>()) >= Math.Max(Count(first), Count(second)));
      return default(IEnumerable<TSource>);
    }
    //
    // Summary:
    //     Produces the set union of two sequences by using a specified System.Collections.Generic.IEqualityComparer<T>.
    //
    // Parameters:
    //   first:
    //     An System.Collections.Generic.IEnumerable<T> whose distinct elements form
    //     the first set for the union.
    //
    //   second:
    //     An System.Collections.Generic.IEnumerable<T> whose distinct elements form
    //     the second set for the union.
    //
    //   comparer:
    //     The System.Collections.Generic.IEqualityComparer<T> to compare values.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of the input sequences.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains the elements from
    //     both input sequences, excluding duplicates.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     first or second is null.
    [Pure]
    public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      Contract.Ensures(Count(Contract.Result<IEnumerable<TSource>>()) >= Math.Max(Count(first), Count(second)));
      return default(IEnumerable<TSource>);
    }
    //
    // Summary:
    //     Filters a sequence of values based on a predicate.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to filter.
    //
    //   predicate:
    //     A function to test each element for a condition.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains elements from
    //     the input sequence that satisfy the condition.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      Contract.Ensures(Count(Contract.Result<IEnumerable<TSource>>()) <= Count(source));
      return default(IEnumerable<TSource>);
    }
    //
    // Summary:
    //     Filters a sequence of values based on a predicate. Each element's index is
    //     used in the logic of the predicate function.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> to filter.
    //
    //   predicate:
    //     A function to test each source element for a condition; the second parameter
    //     of the function represents the index of the source element.
    //
    // Type parameters:
    //   TSource:
    //     The type of the elements of source.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains elements from
    //     the input sequence that satisfy the condition.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source or predicate is null.
    [Pure]
    public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
    {
      Contract.Requires(source != null);
      Contract.Requires(predicate != null);
      Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);
      Contract.Ensures(Count(Contract.Result<IEnumerable<TSource>>()) <= Count(source));
      return default(IEnumerable<TSource>);
    }
#if  NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
    //
    // Summary:
    //     Merges two sequences by using the specified predicate function.
    //
    // Parameters:
    //   first:
    //     The first sequence to merge.
    //
    //   second:
    //     The second sequence to merge.
    //
    //   resultSelector:
    //     A function that specifies how to merge the elements from the two sequences.
    //
    // Type parameters:
    //   TFirst:
    //     The type of the elements of the first input sequence.
    //
    //   TSecond:
    //     The type of the elements of the second input sequence.
    //
    //   TResult:
    //     The type of the elements of the result sequence.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains merged elements
    //     of two input sequences.
    [Pure]
    public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);
      Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);
      return default(IEnumerable<TResult>);
    }
#endif

  }
}

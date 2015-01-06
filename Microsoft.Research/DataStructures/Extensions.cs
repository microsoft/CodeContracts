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
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.DataStructures
{
  public static class SetExtensions
  {
    [ContractVerification(true)]
    static public Set<U> ConvertIf<T, U>(this Set<T> original, Predicate<T> condition, Converter<T, U> converter)
    {
      Contract.Requires(condition != null);
      Contract.Requires(converter != null);

      if (original != null)
      {
        var result = new Set<U>();

        foreach (var element in original)
        {
          if (condition(element))
            result.Add(converter(element));
        }

        return result;
      }

      return default(Set<U>);
    }
  }

  [ContractVerification(true)]
  public static class IEnumerableExtensions
  {
    [Pure]
    public static FList<T> ToFList<T>(this IEnumerable<T> coll)
    {
      Contract.Requires(coll != null);
      // Contract.Ensures(Contract.Result<FList<T>>() != null);  // too strong postcondition, if coll is empty, it returns null

      var result = FList<T>.Empty;

      foreach (var x in coll)
      {
        result = result.Cons(x);
      }

      return result = result.Reverse();
    }

    [Pure]
    public static Set<T> ToSet<T>(this IEnumerable<T> coll)
    {
      Contract.Requires(coll != null);
      Contract.Ensures(Contract.Result<Set<T>>() != null);

      return new Set<T>(coll);
    }



    // equivalent to Linq IEnumerable<Out>.Select
    public static IEnumerable<Out> ApplyToAll<In, Out>(this IEnumerable<In> input, Func<In, Out> map)
    {
      Contract.Requires(input != null);
      Contract.Requires(map != null);
      Contract.Ensures(Contract.Result<IEnumerable<Out>>() != null);

      foreach (var i in input)
      {
        yield return map(i);
      }
    }

    public static int GetStructuralHashCode<T>(this IEnumerable<T> input)
    {
      Contract.Requires(input != null);

      return HashHelpers.CombineHashCodes(input.ApplyToAll(x => x.GetHashCode()));
    }

    /// <summary>
    /// Splits a sequence of values based on a predicate (One: true, Two: false)
    /// </summary>
    /// <returns>a pair of IEnumerable whose first elements verify the predicate and second elements don't</returns>
    public static Pair<IEnumerable<T>, IEnumerable<T>> Split<T>(this IEnumerable<T> input, Predicate<T> predicate)
    {
      Contract.Requires(input != null);
      Contract.Requires(predicate != null);

      // TOCHECK: predicate is called only once per element
      var inputWithBool = input.Select(x => Pair.For(x, predicate(x)));
      return Pair.For(inputWithBool.Where(Pair.Two).Select(Pair.One), inputWithBool.Where(Pair.NotTwo).Select(Pair.One));
    }
  }

  public static class ICollectionExtensions
  {
    private class CastCollection<TSource, TResult> : ICollection<TResult>
    {
      private readonly ICollection<TSource> source;

      public CastCollection(ICollection<TSource> source)
      {
        this.source = source;
      }

      int ICollection<TResult>.Count { get { return source.Count; } }
      bool ICollection<TResult>.IsReadOnly { get { return source.IsReadOnly; } }
      void ICollection<TResult>.Add(TResult item) { this.source.Add((TSource)(object)item); }
      void ICollection<TResult>.Clear() { this.source.Clear(); }
      bool ICollection<TResult>.Contains(TResult item) { return this.source.Contains((TSource)(object)item); }
      void ICollection<TResult>.CopyTo(TResult[] array, int arrayIndex) { this.source.Cast<TResult>().ToArray().CopyTo(array, arrayIndex); }
      IEnumerator IEnumerable.GetEnumerator() { return this.source.GetEnumerator(); }
      IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator() { return this.source.Cast<TResult>().GetEnumerator(); }
      bool ICollection<TResult>.Remove(TResult item) { return this.source.Remove((TSource)(object)item); }
    }

    public static ICollection<TResult> CollectionCast<TSource, TResult>(this ICollection<TSource> source)
    {
      var typedSource = source as ICollection<TResult>;
      if (typedSource != null)
        return typedSource;
      if (source == null) throw new ArgumentNullException("source");
      return new CastCollection<TSource, TResult>(source);
    }    
  }

  [ContractVerification(true)]
  public static class DictionaryExtensions
  {
    public static bool AddToValues<TKey, TValue>(this Dictionary<TKey, List<TValue>> dictionary, TKey key, TValue value)
    {
      Contract.Requires(dictionary != null);
      Contract.Requires(key != null);

      List<TValue> values;
      if (!dictionary.TryGetValue(key, out values) || values == null)
      {
        values = new List<TValue>() { value };
        dictionary[key] = values;

        return false;
      }
      else
      {
        // works with side effects
        values.Add(value);

        return true;
      }
    }
  }

  public static class SortedListExtensions
  {
    /// <summary>
    /// Finds the last element whose key is not greater than <paramref name="key"/>
    /// </summary>
    /// <returns>false if all elements are greater than <paramref name="key"/></returns>
    public static bool TryUpperBound<TKey, TValue>(this SortedList<TKey, TValue> sortedList, TKey key, out TKey upperBoundKey, out TValue upperBoundValue)
    {
      int low = 0;
      int high = sortedList.Count - 1;
      while (low <= high)
      {
        int med = low + ((high - low) >> 1);
        var medKey = sortedList.Keys[med];
        int cmp = sortedList.Comparer.Compare(medKey, key);
        if (cmp == 0)
        {
          upperBoundKey = medKey;
          upperBoundValue = sortedList.Values[med];
          return true;
        }
        if (cmp < 0)
          low = med + 1;
        else
          high = med - 1;
      }
      if (high >= 0)
      {
        upperBoundKey = sortedList.Keys[high];
        upperBoundValue = sortedList.Values[high];
        return true;
      }
      upperBoundKey = default(TKey);
      upperBoundValue = default(TValue);
      return false;
    }

    public static bool TryUpperBound<TKey, TValue>(this SortedList<TKey, TValue> sortedList, TKey key, out TKey upperBoundKey)
    {
      TValue upperBoundValue;
      return sortedList.TryUpperBound(key, out upperBoundKey, out upperBoundValue);
    }

    public static bool TryUpperBound<TKey, TValue>(this SortedList<TKey, TValue> sortedList, TKey key, out TValue upperBoundValue)
    {
      TKey upperBoundKey;
      return sortedList.TryUpperBound(key, out upperBoundKey, out upperBoundValue);
    }
  }

  public static class KeyValuePair
  {
    public static KeyValuePair<TKey, TValue> For<TKey, TValue>(TKey key, TValue value)
    {
      return new KeyValuePair<TKey, TValue>(key, value);
    }
  }

  [ContractVerification(true)]
  public static class StringExtensions
  {
    [Pure]
    public static string PrefixWithCurrentTime(this string str, string prefix = "")
    {
      var currTime = DateTime.Now.TimeOfDay.ToString();

      return string.Format("[{0}] {1}",
        String.IsNullOrEmpty(prefix) ? currTime : prefix + " -- " + currTime,
        str);
    }
  }

  public static class ContractExtensions
  {
    [Pure]
    /// <summary>Simple identity function checking the extra assumption that obj != null</summary>
    public static T AssumeNotNull<T>(this T obj, string msgIfFails = "")
       where T: class
    {
      Contract.Ensures(Contract.Result<T>() != null);
      Contract.Ensures(Contract.Result<T>() == obj);

      Contract.Assume(obj != null, msgIfFails);
      return obj;
    }
  }

  public class ExitRequestedException : Exception
  {
    public readonly int ExitCode;

    public ExitRequestedException(int exitCode)
    {
      this.ExitCode = exitCode;
    }
  }

}

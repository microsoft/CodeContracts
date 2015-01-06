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
using System.Diagnostics.Contracts;
using System.Collections.ObjectModel;

namespace System
{

  public class Array : System.Collections.ICollection
  {
#if SILVERLIGHT_3_0 || NETFRAMEWORK_3_5 || SILVERLIGHT_4_0_WP
    private Array() { }
#else
    internal Array() { }
#endif

    // Summary:
    //     Returns a read-only wrapper for the specified array.
    //
    // Parameters:
    //   array:
    //     The one-dimensional, zero-based array to wrap in a read-only System.Collections.ObjectModel.ReadOnlyCollection<T>
    //     wrapper.
    //
    // Type parameters:
    //   T:
    //     The type of the elements of the array.
    //
    // Returns:
    //     A read-only System.Collections.ObjectModel.ReadOnlyCollection<T> wrapper
    //     for the specified array.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array is null.
    public static ReadOnlyCollection<T> AsReadOnly<T>(T[] array)
    {
      Contract.Requires(array != null);
      Contract.Ensures(Contract.Result<ReadOnlyCollection<T>>() != null);

      return default(ReadOnlyCollection<T>);
    }
 
    int System.Collections.ICollection.Count
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() == this.Length);

        return default(int);
      }
    }

    public int Rank
    {
      [Pure]
      [ContractOption("reads", "mutable", false)]
      [Reads(ReadsAttribute.Reads.Nothing)]
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
    }

    public int Length
    {
      [Pure]
      [ContractOption("reads", "mutable", false)]
      [Reads(ReadsAttribute.Reads.Nothing)]
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() == GetUpperBound(0) + 1);

        return default(int);
      }
    }

#if !SILVERLIGHT
    public long LongLength
    {
      [Pure]
      [ContractOption("reads", "mutable", false)]
      [Reads(ReadsAttribute.Reads.Nothing)]
      get
      {
        Contract.Ensures(0 <= Contract.Result<long>());
        return default(long);
      }
    }

    [Pure]
    public static bool TrueForAll<T>(T[] array, Predicate<T> match) {
      Contract.Requires(array != null);
      Contract.Requires(match != null);
      return default(bool);
    }
#endif

    public static void Resize<T>(ref T[] array, int newSize)
    {
      Contract.Requires(newSize >= 0);
      Contract.Ensures(array != null);
      Contract.Ensures(array.Length == newSize);
    }

    public static void Sort(Array keys, Array items, int index, int length, System.Collections.IComparer comparer)
    {
      Contract.Requires(keys != null);
      // Contract.Requires(keys.Rank == 1);
      // Contract.Requires(items == null || items.Rank == 1);
      Contract.Requires(items == null || keys.GetLowerBound(0) == items.GetLowerBound(0));
      Contract.Requires(index >= keys.GetLowerBound(0));
      Contract.Requires(length >= 0);
      Contract.Requires(keys.GetLowerBound(0) + index + length <= keys.Length);
      Contract.Requires(items == null || index + length <= items.Length);

    }
    public static void Sort(Array array, int index, int length, System.Collections.IComparer comparer)
    {
      Contract.Requires(array != null);
      // Contract.Requires(array.Rank == 1);
      Contract.Requires(index >= array.GetLowerBound(0));
      Contract.Requires(length >= 0);
      Contract.Requires(array.GetLowerBound(0) + index + length <= array.Length);

    }
    public static void Sort(Array keys, Array items, System.Collections.IComparer comparer)
    {
      Contract.Requires(keys != null);
      // Contract.Requires(keys.Rank == 1);
      // Contract.Requires(items == null || items.Rank == 1);
      Contract.Requires(items == null || keys.GetLowerBound(0) == items.GetLowerBound(0));

    }
    public static void Sort(Array array, System.Collections.IComparer comparer)
    {
      Contract.Requires(array != null);
      // Contract.Requires(array.Rank == 1);

    }
#if !SILVERLIGHT
    public static void Sort(Array keys, Array items, int index, int length)
    {
      Contract.Requires(keys != null);
      // Contract.Requires(keys.Rank == 1);
      // Contract.Requires(items == null || items.Rank == 1);
      Contract.Requires(items == null || keys.GetLowerBound(0) == items.GetLowerBound(0));
      Contract.Requires(index >= keys.GetLowerBound(0));
      Contract.Requires(length >= 0);
      Contract.Requires(keys.GetLowerBound(0) + index + length <= keys.Length);
      Contract.Requires(items == null || index + length <= items.Length);

    }

    public static void Sort(Array array, int index, int length)
    {
      Contract.Requires(array != null);
      // Contract.Requires(array.Rank == 1);
      Contract.Requires(index >= array.GetLowerBound(0));
      Contract.Requires(length >= 0);
      Contract.Requires(array.GetLowerBound(0) + index + length <= array.Length);

    }

    public static void Sort(Array keys, Array items)
    {
      Contract.Requires(keys != null);
      // Contract.Requires(keys.Rank == 1);
      // Contract.Requires(items == null || items.Rank == 1);
      Contract.Requires(items == null || keys.GetLowerBound(0) == items.GetLowerBound(0));
    }
#endif
    public static void Sort(Array array)
    {
      Contract.Requires(array != null);
      // Contract.Requires(array.Rank == 1);

    }
    public static void Reverse(Array array, int index, int length)
    {
      Contract.Requires(array != null);
      // Contract.Requires(array.Rank == 1);
      Contract.Requires(length >= 0);
      Contract.Requires(index >= array.GetLowerBound(0));
      Contract.Requires(array.GetLowerBound(0) + index + length <= array.Length);

    }
    public static void Reverse(Array array)
    {
      Contract.Requires(array != null);
      // Contract.Requires(array.Rank == 1);

    }
    public static int LastIndexOf([Pure] Array array, object value, int startIndex, int count)
    {
      Contract.Requires(array != null);
      // Contract.Requires(array.Rank == 1);
      Contract.Requires(count >= 0);
      Contract.Requires(count <= startIndex - array.GetLowerBound(0) + 1);
      Contract.Requires(startIndex >= array.GetLowerBound(0));
      Contract.Requires(startIndex < array.Length + array.GetLowerBound(0));
      Contract.Ensures(Contract.Result<int>() == array.GetLowerBound(0) - 1 || (startIndex + 1 - count <= Contract.Result<int>() && Contract.Result<int>() <= startIndex));

      return default(int);
    }
#if !SILVERLIGHT
    public static int LastIndexOf([Pure] Array array, object value, int startIndex)
    {
      Contract.Requires(array != null);
      // Contract.Requires(array.Rank == 1);
      Contract.Requires(startIndex >= array.GetLowerBound(0));
      Contract.Requires(startIndex < array.Length + array.GetLowerBound(0));
      Contract.Ensures(array.GetLowerBound(0) - 1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= startIndex);

      return default(int);
    }
#endif

#if !SILVERLIGHT
    public static int LastIndexOf([Pure] Array array, object value)
    {
      Contract.Requires(array != null);
      // Contract.Requires(array.Rank == 1);
      Contract.Ensures(array.GetLowerBound(0) - 1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= array.GetUpperBound(0));

      return default(int);
    }
#endif

    public static int IndexOf([Pure] Array array, object value, int startIndex, int count)
    {
      Contract.Requires(array != null);
      // Contract.Requires(array.Rank == 1);
      Contract.Requires(startIndex >= array.GetLowerBound(0));
      Contract.Requires(startIndex <= array.GetLowerBound(0) + array.Length);
      Contract.Requires(count >= 0);
      Contract.Requires(startIndex + count <= array.GetLowerBound(0) + array.Length);
      Contract.Ensures(Contract.Result<int>() == array.GetLowerBound(0) - 1
                      || (startIndex <= Contract.Result<int>() && Contract.Result<int>() < startIndex + count));

      return default(int);
    }
#if !SILVERLIGHT
    public static int IndexOf([Pure] Array array, object value, int startIndex)
    {
      Contract.Requires(array != null);
      // Contract.Requires(array.Rank == 1);
      Contract.Requires(startIndex >= array.GetLowerBound(0));
      Contract.Requires(startIndex <= array.GetLowerBound(0) + array.Length);
      Contract.Ensures(Contract.Result<int>() == array.GetLowerBound(0) - 1 || (startIndex <= Contract.Result<int>() && Contract.Result<int>() <= array.GetUpperBound(0)));

      return default(int);
    }

    public static int IndexOf([Pure] Array array, object value)
    {
      Contract.Requires(array != null);
      // Contract.Requires(array.Rank == 1);
      Contract.Ensures(array.GetLowerBound(0) - 1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= array.GetUpperBound(0));

      return default(int);
    }
#endif

#if !SILVERLIGHT

    public void CopyTo(Array array, long index)
    {
      Contract.Requires(array != null);
      Contract.Requires(array.GetLowerBound(0) <= index);
      // Contract.Requires(this.Rank == 1);
      // Contract.Requires(array.Rank == 1);
      Contract.Requires(index <= array.GetUpperBound(0) + 1 - this.Length);
      //modifies this.0, array.*;

    }
#endif
    // this method is implementing ICollection.CopyTo and the contracts are there
    public void CopyTo(Array array, int index)
    {
    }

    [Pure]
    public static int BinarySearch(Array array, int index, int length, object value, System.Collections.IComparer comparer)
    {
      Contract.Requires(array != null);
      // Contract.Requires(array.Rank == 1);
      Contract.Requires(index >= array.GetLowerBound(0));
      Contract.Requires(length >= 0);
      Contract.Requires(length <= array.Length - index);
      Contract.Ensures(Contract.Result<int>() == array.GetLowerBound(0) - 1 || (index <= Contract.Result<int>() && Contract.Result<int>() < index + length));

      return default(int);
    }
#if !SILVERLIGHT
    [Pure]
    public static int BinarySearch(Array array, object value, System.Collections.IComparer comparer)
    {
      Contract.Requires(array != null);
      // Contract.Requires(array.Rank == 1);
      Contract.Ensures(array.GetLowerBound(0) - 1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= array.GetUpperBound(0));

      return default(int);
    }

    [Pure]
    public static int BinarySearch(Array array, int index, int length, object value)
    {
      Contract.Requires(array != null);
      // Contract.Requires(array.Rank == 1);
      Contract.Requires(index >= array.GetLowerBound(0));
      Contract.Requires(length >= 0);
      Contract.Requires(length <= array.Length - index);
      Contract.Ensures(Contract.Result<int>() <= array.GetUpperBound(0));

      return default(int);
    }
#endif
    [Pure] 
    public static int BinarySearch(Array array, object value)
    {
      Contract.Requires(array != null);
      // Contract.Requires(array.Rank == 1);
      Contract.Ensures(array.GetLowerBound(0) - 1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= array.GetUpperBound(0));

      return default(int);
    }
    [Pure]
    public static int BinarySearch<T>(T[] array, T value)
    {
      Contract.Requires(array != null);
      Contract.Ensures(Contract.Result<int>() < array.Length);
      Contract.Ensures(~Contract.Result<int>() <= array.Length);

      return default(int);
    }
    [Pure]
    public static int BinarySearch<T>(T[] array, T value, System.Collections.Generic.IComparer<T> comparer)
    {
      Contract.Requires(array != null);
      Contract.Ensures(Contract.Result<int>() < array.Length);
      Contract.Ensures(~Contract.Result<int>() <= array.Length);

      return default(int);
    }

    [Pure]
    public static int BinarySearch<T>(T[] array, int index, int length, T value)
    {
      Contract.Requires(array != null);
      Contract.Requires(index >= 0);
      Contract.Requires(length >= 0);
      Contract.Requires(index <= array.Length - length);
      Contract.Ensures(Contract.Result<int>() - index < length);
      Contract.Ensures(~Contract.Result<int>() - index <= length);
      Contract.Ensures(Contract.Result<int>() >= index || ~Contract.Result<int>() >= index);

      return default(int);
    }
    [Pure]
    public static int BinarySearch<T>(T[] array, int index, int length, T value, System.Collections.Generic.IComparer<T> comparer)
    {
      Contract.Requires(array != null);
      Contract.Requires(index >= 0);
      Contract.Requires(length >= 0);
      Contract.Requires(index <= array.Length - length);
      Contract.Ensures(Contract.Result<int>() - index < length);
      Contract.Ensures(~Contract.Result<int>() - index <= length);
      Contract.Ensures(Contract.Result<int>() >= index || ~Contract.Result<int>() >= index);

      return default(int);
    }

    [Pure]
    [ContractOption("reads", "mutable", false)]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public int GetLowerBound(int dimension)
    {
      Contract.Requires(dimension >= 0);
      Contract.Requires(dimension < this.Rank);

      return default(int);
    }
    [Pure]
    [ContractOption("reads", "mutable", false)]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public int GetUpperBound(int dimension)
    {
      Contract.Requires(dimension >= 0);
      Contract.Requires(dimension < this.Rank);

      return default(int);
    }
#if !SILVERLIGHT
    [Pure]
    [ContractOption("reads", "mutable", false)]
    //[Reads(ReadsAttribute.Reads.Nothing)]
    public long GetLongLength(int dimension)
    {
      return default(long);
    }
#endif
    [Pure]
    [ContractOption("reads", "mutable", false)]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public int GetLength(int arg0)
    {

      return default(int);
    }
#if !SILVERLIGHT
    public void SetValue(object value, long[] indices)
    {

    }
    public void SetValue(object value, long index)
    {
      // Contract.Requires(index <= 2147483647);
      // Contract.Requires(index >= -2147483648);
      // Contract.Requires(this.Rank == 1);
      Contract.Requires(index >= this.GetLowerBound(0));
      Contract.Requires(index <= this.GetUpperBound(0));
    }
#endif
    public void SetValue(object value, int[] indices)
    {
      Contract.Requires(indices != null);
      //Contract.Requires(Forall { int i in indices; this.GetLowerBound(i) <= indices[i] && indices[i] <= this.GetUpperBound(i) });

    }
    public void SetValue(object value, int index)
    {
      // Contract.Requires(this.Rank == 1);
      Contract.Requires(index >= this.GetLowerBound(0));
      Contract.Requires(index <= this.GetUpperBound(0));

    }
#if !SILVERLIGHT
    [Pure]
    public object GetValue(long index)
    {
      // Contract.Requires(index <= 2147483647);
      // Contract.Requires(index >= -2147483648);
      // Contract.Requires(this.Rank == 1);
      Contract.Requires(index >= this.GetLowerBound(0));
      Contract.Requires(index <= this.GetUpperBound(0));

      return default(object);
    }
#endif
    [Pure]
    public object GetValue(int index)
    {
      // Contract.Requires(this.Rank == 1);
      Contract.Requires(index >= this.GetLowerBound(0));
      Contract.Requires(index <= this.GetUpperBound(0));


      return default(object);
    }
    [Pure]
    public object GetValue(int[] indices)
    {
      Contract.Requires(indices != null);

      return default(object);
    }

#if !SILVERLIGHT
    [Pure]
    public static T FindLast<T>(T[] array, Predicate<T> match) {
      Contract.Requires(array != null);
      Contract.Requires(match != null);
      return default(T);
    }
    //
    // Summary:
    //     Searches for an element that matches the conditions defined by the specified
    //     predicate, and returns the zero-based index of the last occurrence within
    //     the entire System.Array.
    //
    // Parameters:
    //   array:
    //     The one-dimensional, zero-based System.Array to search.
    //
    //   match:
    //     The System.Predicate<T> that defines the conditions of the element to search
    //     for.
    //
    // Type parameters:
    //   T:
    //     The type of the elements of the array.
    //
    // Returns:
    //     The zero-based index of the last occurrence of an element that matches the
    //     conditions defined by match, if found; otherwise, �1.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array is null.-or-match is null.
    [Pure]
    public static int FindLastIndex<T>(T[] array, Predicate<T> match)
    {
      Contract.Requires(array != null);
      Contract.Requires(match != null);
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < array.Length);

      return default(int);
    }
    //
    // Summary:
    //     Searches for an element that matches the conditions defined by the specified
    //     predicate, and returns the zero-based index of the last occurrence within
    //     the range of elements in the System.Array that extends from the first element
    //     to the specified index.
    //
    // Parameters:
    //   array:
    //     The one-dimensional, zero-based System.Array to search.
    //
    //   startIndex:
    //     The zero-based starting index of the backward search.
    //
    //   match:
    //     The System.Predicate<T> that defines the conditions of the element to search
    //     for.
    //
    // Type parameters:
    //   T:
    //     The type of the elements of the array.
    //
    // Returns:
    //     The zero-based index of the last occurrence of an element that matches the
    //     conditions defined by match, if found; otherwise, �1.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array is null.-or-match is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     startIndex is outside the range of valid indexes for array.
    [Pure]
    public static int FindLastIndex<T>(T[] array, int startIndex, Predicate<T> match)
    {
      Contract.Requires(array != null);
      Contract.Requires(match != null);
      Contract.Requires(startIndex >= 0);
      Contract.Requires(startIndex < array.Length);
      Contract.Ensures(Contract.Result<int>() == -1 || Contract.Result<int>() >= startIndex);
      Contract.Ensures(Contract.Result<int>() < array.Length);

      return default(int);
    }
    //
    // Summary:
    //     Searches for an element that matches the conditions defined by the specified
    //     predicate, and returns the zero-based index of the last occurrence within
    //     the range of elements in the System.Array that contains the specified number
    //     of elements and ends at the specified index.
    //
    // Parameters:
    //   array:
    //     The one-dimensional, zero-based System.Array to search.
    //
    //   startIndex:
    //     The zero-based starting index of the backward search.
    //
    //   count:
    //     The number of elements in the section to search.
    //
    //   match:
    //     The System.Predicate<T> that defines the conditions of the element to search
    //     for.
    //
    // Type parameters:
    //   T:
    //     The type of the elements of the array.
    //
    // Returns:
    //     The zero-based index of the last occurrence of an element that matches the
    //     conditions defined by match, if found; otherwise, �1.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array is null.-or-match is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     startIndex is outside the range of valid indexes for array.-or-count is less
    //     than zero.-or-startIndex and count do not specify a valid section in array.
    [Pure]
    public static int FindLastIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
    {
      Contract.Requires(array != null);
      Contract.Requires(match != null);
      Contract.Requires(startIndex >= 0);
      Contract.Requires(startIndex <= array.Length - count);
      Contract.Ensures(Contract.Result<int>() == -1 || Contract.Result<int>() >= startIndex);
      Contract.Ensures(Contract.Result<int>() - startIndex < count);

      return default(int);
    }
    [Pure]
    public static T[] FindAll<T>(T[] array, Predicate<T> match) {
      Contract.Requires(array != null);
      Contract.Requires(match != null);
      return default(T[]);
    }

    [Pure]
    public static T Find<T>(T[] array, Predicate<T> match) {
      Contract.Requires(array != null);
      Contract.Requires(match != null);
      return default(T);
    }
    //
    // Summary:
    //     Searches for an element that matches the conditions defined by the specified
    //     predicate, and returns the zero-based index of the first occurrence within
    //     the entire System.Array.
    //
    // Parameters:
    //   array:
    //     The one-dimensional, zero-based System.Array to search.
    //
    //   match:
    //     The System.Predicate<T> that defines the conditions of the element to search
    //     for.
    //
    // Type parameters:
    //   T:
    //     The type of the elements of the array.
    //
    // Returns:
    //     The zero-based index of the first occurrence of an element that matches the
    //     conditions defined by match, if found; otherwise, �1.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array is null.-or-match is null.
    [Pure]
    public static int FindIndex<T>(T[] array, Predicate<T> match)
    {
      Contract.Requires(array != null);
      Contract.Requires(match != null);
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < array.Length);

      return default(int);
    }
    //
    // Summary:
    //     Searches for an element that matches the conditions defined by the specified
    //     predicate, and returns the zero-based index of the first occurrence within
    //     the range of elements in the System.Array that extends from the specified
    //     index to the last element.
    //
    // Parameters:
    //   array:
    //     The one-dimensional, zero-based System.Array to search.
    //
    //   startIndex:
    //     The zero-based starting index of the search.
    //
    //   match:
    //     The System.Predicate<T> that defines the conditions of the element to search
    //     for.
    //
    // Type parameters:
    //   T:
    //     The type of the elements of the array.
    //
    // Returns:
    //     The zero-based index of the first occurrence of an element that matches the
    //     conditions defined by match, if found; otherwise, �1.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array is null.-or-match is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     startIndex is outside the range of valid indexes for array.
    [Pure]
    public static int FindIndex<T>(T[] array, int startIndex, Predicate<T> match)
    {
      Contract.Requires(array != null);
      Contract.Requires(match != null);
      Contract.Requires(startIndex >= 0);
      Contract.Requires(startIndex < array.Length);
      Contract.Ensures(Contract.Result<int>() == -1 || Contract.Result<int>() >= startIndex);
      Contract.Ensures(Contract.Result<int>() < array.Length);

      return default(int);
    }
    //
    // Summary:
    //     Searches for an element that matches the conditions defined by the specified
    //     predicate, and returns the zero-based index of the first occurrence within
    //     the range of elements in the System.Array that starts at the specified index
    //     and contains the specified number of elements.
    //
    // Parameters:
    //   array:
    //     The one-dimensional, zero-based System.Array to search.
    //
    //   startIndex:
    //     The zero-based starting index of the search.
    //
    //   count:
    //     The number of elements in the section to search.
    //
    //   match:
    //     The System.Predicate<T> that defines the conditions of the element to search
    //     for.
    //
    // Type parameters:
    //   T:
    //     The type of the elements of the array.
    //
    // Returns:
    //     The zero-based index of the first occurrence of an element that matches the
    //     conditions defined by match, if found; otherwise, �1.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array is null.-or-match is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     startIndex is outside the range of valid indexes for array.-or-count is less
    //     than zero.-or-startIndex and count do not specify a valid section in array.
    [Pure]
    public static int FindIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
    {
      Contract.Requires(array != null);
      Contract.Requires(match != null);
      Contract.Requires(startIndex >= 0);
      Contract.Requires(startIndex <= array.Length - count);
      Contract.Ensures(Contract.Result<int>() == -1 || Contract.Result<int>() >= startIndex);
      Contract.Ensures(Contract.Result<int>() - startIndex < count);

      return default(int);
    }
    [Pure]
    public static bool Exists<T>(T[] array, Predicate<T> match) {
      Contract.Requires(array != null);
      Contract.Requires(match != null);
      return default(bool);
    }
#endif

    public static void Clear(Array array, int index, int length)
    {
      Contract.Requires(array != null);
      // Contract.Requires(array.Rank == 1);
      // Contract.Requires(index >= array.GetLowerBound(0));
      Contract.Requires(length >= 0);
      Contract.Requires(array.Length - (index + array.GetLowerBound(0)) >= length);

    }
#if !SILVERLIGHT
    public static void Copy([Pure] Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length)
    {
      Contract.Requires(sourceArray != null);
      Contract.Requires(destinationArray != null);
      // Contract.Requires(sourceArray.Rank == destinationArray.Rank);
      Contract.Requires(sourceIndex >= sourceArray.GetLowerBound(0));
      Contract.Requires(destinationIndex >= destinationArray.GetLowerBound(0));
      Contract.Requires(length >= 0);
      Contract.Requires(sourceIndex + length <= sourceArray.GetLowerBound(0) + sourceArray.Length);
      Contract.Requires(destinationIndex + length <= destinationArray.GetLowerBound(0) + destinationArray.Length);
      // Contract.Requires(sourceIndex <= 2147483647);
      // Contract.Requires(sourceIndex >= -2147483648);
      // Contract.Requires(destinationIndex <= 2147483647);
      // Contract.Requires(destinationIndex >= -2147483648);
      // Contract.Requires(length <= 2147483647);
      // Contract.Requires(length >= -2147483648);
      //modifies destinationArray.*;
    }

    public static void Copy([Pure] Array sourceArray, Array destinationArray, long length)
    {
      Contract.Requires(sourceArray != null);
      Contract.Requires(destinationArray != null);
      // Contract.Requires(sourceArray.Rank == destinationArray.Rank);
      Contract.Requires(length >= 0);
      Contract.Requires(length <= sourceArray.GetLowerBound(0) + sourceArray.Length);
      Contract.Requires(length <= destinationArray.GetLowerBound(0) + destinationArray.Length);
      // Contract.Requires(length <= 2147483647);
      // Contract.Requires(length >= -2147483648);
      //modifies destinationArray.*;
    }
#endif

    public static void Copy([Pure] Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
    {
      Contract.Requires(sourceArray != null);
      Contract.Requires(destinationArray != null);
      // Contract.Requires(sourceArray.Rank == destinationArray.Rank);
      Contract.Requires(sourceIndex >= sourceArray.GetLowerBound(0));
      Contract.Requires(destinationIndex >= destinationArray.GetLowerBound(0));
      Contract.Requires(length >= 0);
      Contract.Requires(sourceIndex + length <= sourceArray.GetLowerBound(0) + sourceArray.Length);
      Contract.Requires(destinationIndex + length <= destinationArray.GetLowerBound(0) + destinationArray.Length);
      //modifies destinationArray.*;

    }
    public static void Copy([Pure] Array sourceArray, Array destinationArray, int length)
    {
      Contract.Requires(sourceArray != null);
      Contract.Requires(destinationArray != null);
      // Contract.Requires(sourceArray.Rank == destinationArray.Rank);
      Contract.Requires(length >= 0);
      Contract.Requires(length <= sourceArray.GetLowerBound(0) + sourceArray.Length);
      Contract.Requires(length <= destinationArray.GetLowerBound(0) + destinationArray.Length);
      //modifies destinationArray.*;

    }
#if !SILVERLIGHT
    public static Array CreateInstance(Type elementType, int[] lengths, int[] lowerBounds)
    {
      Contract.Requires(elementType != null);
      Contract.Requires(lengths != null);
      Contract.Requires(lowerBounds != null);
      Contract.Requires(lengths.Length == lowerBounds.Length);
      Contract.Requires(lengths.Length != 0);

      Contract.Ensures(Contract.Result<Array>() != null);
      return default(Array);
    }
    public static Array CreateInstance(Type elementType, long[] lengths)
    {
      Contract.Requires(elementType != null);
      Contract.Requires(lengths != null);
      Contract.Requires(lengths.Length > 0);
      // Contract.Requires(Forall { int i in lengths; lengths[i] >= 0; });
      Contract.Ensures(Contract.Result<Array>().Rank == lengths.Length);
      // Contract.Ensures(Forall { int i in lengths); result.GetLength(i) == length[i]); }

      Contract.Ensures(Contract.Result<Array>() != null);
      return default(Array);
    }
#endif
    public static Array CreateInstance(Type elementType, int[] lengths)
    {
      Contract.Requires(elementType != null);
      Contract.Requires(lengths != null);
      Contract.Requires(lengths.Length > 0);
      // Contract.Requires(Forall { int i in lengths; lengths[i] >= 0; });
      Contract.Ensures(Contract.Result<Array>().Rank == lengths.Length);
      // Contract.Ensures(Forall { int i in lengths); result.GetLength(i) == length[i]); }

      Contract.Ensures(Contract.Result<Array>() != null);
      return default(Array);
    }
#if !SILVERLIGHT
    public static Array CreateInstance(Type elementType, int length1, int length2, int length3)
    {
      Contract.Requires(elementType != null);
      Contract.Requires(length1 >= 0);
      Contract.Requires(length2 >= 0);
      Contract.Requires(length3 >= 0);
      Contract.Ensures(Contract.Result<Array>() != null);
      Contract.Ensures(Contract.Result<Array>().Rank == 3);
      Contract.Ensures(Contract.Result<Array>().GetLength(0) == length1);
      Contract.Ensures(Contract.Result<Array>().GetLength(1) == length2);
      Contract.Ensures(Contract.Result<Array>().GetLength(2) == length3);

      return default(Array);
    }

    public static Array CreateInstance(Type elementType, int length1, int length2)
    {
      Contract.Requires(elementType != null);
      Contract.Requires(length1 >= 0);
      Contract.Requires(length2 >= 0);

      Contract.Ensures(Contract.Result<Array>() != null);
      Contract.Ensures(Contract.Result<Array>().Rank == 2);
      Contract.Ensures(Contract.Result<Array>().GetLength(0) == length1);
      Contract.Ensures(Contract.Result<Array>().GetLength(1) == length2);

      return default(Array);
    }
#endif

    public static Array CreateInstance(Type elementType, int length)
    {
      Contract.Requires(elementType != null);
      Contract.Requires(length >= 0);

      Contract.Ensures(Contract.Result<Array>() != null);
      Contract.Ensures(Contract.Result<Array>().Rank == 1);
      Contract.Ensures(Contract.Result<Array>().GetLength(0) == length);
      Contract.Ensures(Contract.Result<Array>().GetUpperBound(0) == length - 1);

      return default(Array);
    }

    public virtual object Clone()
    {
      Contract.Ensures(((Array)Contract.Result<object>()).Length == this.Length);

      return default(Array);
    }

#if NETFRAMEWORK_4_0
    public static TOutput[] ConvertAll<TInput, TOutput>(
    	TInput[] array,
	    Converter<TInput, TOutput> converter)
    {
        Contract.Requires(array != null);
        Contract.Requires(converter != null);
        Contract.Ensures(Contract.Result<TOutput[]>() != null);

        return default(TOutput[]);
    }
#endif

    #region ICollection Members


    public virtual bool IsSynchronized
    {
      get { throw new NotImplementedException(); }
    }

    public virtual object SyncRoot
    {
      get { throw new NotImplementedException(); }
    }

    #endregion
  }
}

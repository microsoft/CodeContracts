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

using System.Diagnostics.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Collections.ObjectModel;

namespace System.Collections.Generic {
  public class List<T> :IList<T> {
    // Summary:
    // Initializes a new instance of the System.Collections.Generic.List<T> class
    // that is empty and has the default initial capacity.
    public List()
    {
      Contract.Ensures(this.Count == 0);
    }
    //
    // Summary:
    // Initializes a new instance of the System.Collections.Generic.List<T> class
    // that contains elements copied from the specified collection and has
    // sufficient capacity to accommodate the number of elements copied.
    // 
    // Parameters:
    // collection: 
    //    The collection whose elements are copied to the new list.
    // 
    // Exceptions:
    //    System.ArgumentNullException
    // 
    public List(IEnumerable<T> collection) {

    }
    //
    // Summary:
    // Initializes a new instance of the System.Collections.Generic.List<T> class
    // that is empty and has the specified initial capacity.
    // 
    // Parameters:
    // capacity: 
    //    The number of elements that the new list can initially store.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    // 
    public List(int capacity) {
      Contract.Requires(capacity >= 0);
      Contract.Ensures(this.Count == 0);
      Contract.Ensures(this.Capacity == capacity);
    }

    // Summary:
    // Gets or sets the number of elements that the
    // System.Collections.Generic.List<T> can contain.
    // 
    // Returns:
    // The number of elements that the System.Collections.Generic.List<T> can
    // contain.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    // 
    public int Capacity
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() >= this.Count);

        return 0;
      }
      set
      {
        Contract.Requires(this.Count <= value);
      }
    }

    // Implements ICollection
    extern public virtual int Count { get; }


    //
    // Summary:
    // Adds the elements of the specified collection to the end of the
    // System.Collections.Generic.List<T>.
    // 
    // Parameters:
    // collection: 
    //    The collection whose elements should be added to the end of the
    //    System.Collections.Generic.List<T>. The collection itself cannot be null,
    //    but it can contain elements that are null, if type T is a reference type.
    // 
    // Exceptions:
    //    System.ArgumentNullException
    // 
    public void AddRange(IEnumerable<T> collection)
    {
      Contract.Requires(collection != null);
    }

    //
    // Summary:
    // Returns a read-only System.Collections.Generic.IList<T> wrapper.
    // 
    // Returns:
    // A System.Collections.Generic.ReadOnlyCollection`1 that acts as a read-only
    // wrapper around the current System.Collections.Generic.List<T>.
    [Pure]
    public ReadOnlyCollection<T> AsReadOnly() {
      Contract.Ensures(Contract.Result<ReadOnlyCollection<T>>() != null);
      Contract.Ensures(((IList)Contract.Result<ReadOnlyCollection<T>>()).IsReadOnly);
      return default(ReadOnlyCollection<T>);
    }

    //
    // Summary:
    // Searches the entire sorted System.Collections.Generic.List<T> for an element
    // using the default comparer and returns the zero-based index of the element.
    // 
    // Parameters:
    // item: 
    //    The object to locate. The value can be null for reference types.
    // 
    // Returns:
    // The zero-based index of item in the sorted
    // System.Collections.Generic.List<T>, if item is found; otherwise, a negative
    // number, which is the bitwise complement of the index of the next element
    // that is larger than item or, if there is no larger element, the bitwise
    // complement of System.Collections.Generic.List<T>.Count.
    // 
    // Exceptions:
    //    System.InvalidOperationException
    // 
    [Pure]
    public int BinarySearch(T item)
    {
      Contract.Ensures(Contract.Result<int>() < this.Count);
      Contract.Ensures(~Contract.Result<int>() <= this.Count);

      return default(int);
    }
    //
    // Summary:
    // Searches the entire sorted System.Collections.Generic.List<T> for an element
    // using the specified comparer and returns the zero-based index of the
    // element.
    // 
    // Parameters:
    // item: 
    //    The object to locate. The value can be null for reference types.
    // comparer: 
    //    The System.Collections.Generic.IComparer<T> implementation to use when
    //    comparing elements.-or-null to use the default comparer
    //    System.Collections.Generic.Comparer<T>.Default.
    // 
    // Returns:
    // The zero-based index of item in the sorted
    // System.Collections.Generic.List<T>, if item is found; otherwise, a negative
    // number, which is the bitwise complement of the index of the next element
    // that is larger than item or, if there is no larger element, the bitwise
    // complement of System.Collections.Generic.List<T>.Count.
    // 
    // Exceptions:
    //    System.InvalidOperationException
    // 
    [Pure]
    public int BinarySearch(T item, IComparer<T> comparer)
    {
      Contract.Ensures(Contract.Result<int>() < this.Count);
      Contract.Ensures(~Contract.Result<int>() <= this.Count);

      return default(int);
    }
    //
    // Summary:
    // Searches a range of elements in the sorted
    // System.Collections.Generic.List<T> for an element using the specified
    // comparer and returns the zero-based index of the element.
    // 
    // Parameters:
    // count: 
    //    The length of the range to search.
    // item: 
    //    The object to locate. The value can be null for reference types.
    // index: 
    //    The zero-based starting index of the range to search.
    // comparer: 
    //    The System.Collections.Generic.IComparer<T> implementation to use when
    //    comparing elements.-or-null to use the default comparer
    //    System.Collections.Generic.Comparer<T>.Default.
    // 
    // Returns:
    // The zero-based index of item in the sorted
    // System.Collections.Generic.List<T>, if item is found; otherwise, a negative
    // number, which is the bitwise complement of the index of the next element
    // that is larger than item or, if there is no larger element, the bitwise
    // complement of System.Collections.Generic.List<T>.Count.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    // 
    //    System.InvalidOperationException
    // 
    //    System.ArgumentException
    // 
    [Pure]
    public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index <= this.Count - count);
      Contract.Ensures(Contract.Result<int>() - index < count);
      Contract.Ensures(~Contract.Result<int>() - index <= count);
      Contract.Ensures(Contract.Result<int>() >= index || ~Contract.Result<int>() >= index);
      return default(int);
    }

#if !SILVERLIGHT
    //
    // Summary:
    // Converts the current System.Collections.Generic.List<T> to another type.
    // 
    // Parameters:
    // converter: 
    //    A System.Converter<TInput,TOutput> delegate that converts each element
    //    from one type to another type.
    // 
    // Returns:
    // A System.Collections.Generic.List<T> of the target type containing the
    // converted elements from the current System.Collections.Generic.List<T>.
    // 
    // Exceptions:
    //    System.ArgumentNullException
    // 
    [Pure]
    public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
    {
      Contract.Requires(converter != null);

      Contract.Ensures(Contract.Result<List<TOutput>>() != null);
      Contract.Ensures(Contract.Result<List<TOutput>>().Count == this.Count);

      return default(List<TOutput>);
    }
#endif

    public void CopyTo(T[] array)
    {
      Contract.Requires(array != null);
      Contract.Requires(array.Length >= this.Count);
    }
 
    public void CopyTo(int index, T[] array, int arrayIndex, int count)
    {
      Contract.Requires(array != null);
      Contract.Requires(index >= 0);
      Contract.Requires(index < this.Count);
      Contract.Requires(arrayIndex >= 0);
      Contract.Requires(arrayIndex <= array.Length - count);
    }

#if !SILVERLIGHT
    //
    // Summary:
    // Determines whether the System.Collections.Generic.List<T> contains elements
    // that match the conditions defined by the specified predicate.
    // 
    // Parameters:
    // match: 
    //    The System.Predicate<T> delegate that defines the conditions of the
    //    elements to search for.
    // 
    // Returns:
    // true, if the System.Collections.Generic.List<T> contains one or more
    // elements that match the conditions defined by the specified predicate;
    // otherwise, false.
    // 
    // Exceptions:
    //    System.ArgumentNullException
    // 
    [Pure]
    public bool Exists(Predicate<T> match)
    {
      Contract.Requires(match != null);

      return default(bool);
    }
#endif

#if !SILVERLIGHT
    //
    // Summary:
    // Searches for an element that matches the conditions defined by the specified
    // predicate, and returns the first occurrence within the entire
    // System.Collections.Generic.List<T>.
    // 
    // Parameters:
    // match: 
    //    The System.Predicate<T> delegate that defines the conditions of the
    //    element to search for.
    // 
    // Returns:
    // The first element that matches the conditions defined by the specified
    // predicate, if found; otherwise, the default value for type T.
    // 
    // Exceptions:
    //    System.ArgumentNullException
    // 
    [Pure]
    public T Find(Predicate<T> match)
    {
      Contract.Requires(match != null);
      return default(T);
    }
#endif

#if !SILVERLIGHT
    //
    // Summary:
    // Retrieves the all the elements that match the conditions defined by the
    // specified predicate.
    // 
    // Parameters:
    // match: 
    //    The System.Predicate<T> delegate that defines the conditions of the
    //    elements to search for.
    // 
    // Returns:
    // A System.Collections.Generic.List<T> containing all the elements that match
    // the conditions defined by the specified predicate, if found; otherwise, an
    // empty System.Collections.Generic.List<T>.
    // 
    // Exceptions:
    //    System.ArgumentNullException
    // 
    [Pure]
    public List<T> FindAll(Predicate<T> match)
    {
      Contract.Requires(match != null);
      Contract.Ensures(Contract.Result<List<T>>() != null);

      return default(List<T>);
    }
#endif

#if !SILVERLIGHT
    //
    // Summary:
    // Searches for an element that matches the conditions defined by the specified
    // predicate, and returns the zero-based index of the first occurrence within
    // the entire System.Collections.Generic.List<T>.
    // 
    // Parameters:
    // match: 
    //    The System.Predicate<T> delegate that defines the conditions of the
    //    element to search for.
    // 
    // Returns:
    // The zero-based index of the first occurrence of an element that matches the
    // conditions defined by match, if found; otherwise, -1.
    // 
    // Exceptions:
    //    System.ArgumentNullException
    // 
    [Pure]
    public int FindIndex(Predicate<T> match)
    {
      Contract.Requires(match != null);
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < Count);
      return default(int);
    }

    //
    // Summary:
    // Searches for an element that matches the conditions defined by the specified
    // predicate, and returns the zero-based index of the first occurrence within
    // the range of elements in the System.Collections.Generic.List<T> that extends
    // from the specified index to the last element.
    // 
    // Parameters:
    // startIndex: 
    //    The zero-based starting index of the search.
    // match: 
    //    The System.Predicate<T> delegate that defines the conditions of the
    //    element to search for.
    // 
    // Returns:
    // The zero-based index of the first occurrence of an element that matches the
    // conditions defined by match, if found; otherwise, -1.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    // 
    //    System.ArgumentNullException
    // 
    [Pure]
    public int FindIndex(int startIndex, Predicate<T> match)
    {
      Contract.Requires(match != null);
      Contract.Requires(startIndex >= 0);
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < Count);
      return default(int);
    }

    //
    // Summary:
    // Searches for an element that matches the conditions defined by the specified
    // predicate, and returns the zero-based index of the first occurrence within
    // the range of elements in the System.Collections.Generic.List<T> that starts
    // at the specified index and contains the specified number of elements.
    // 
    // Parameters:
    // count: 
    //    The number of elements in the section to search.
    // startIndex: 
    //    The zero-based starting index of the search.
    // match: 
    //    The System.Predicate<T> delegate that defines the conditions of the
    //    element to search for.
    // 
    // Returns:
    // The zero-based index of the first occurrence of an element that matches the
    // conditions defined by match, if found; otherwise, -1.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    // 
    //    System.ArgumentNullException
    // 
    [Pure]
    public int FindIndex(int startIndex, int count, Predicate<T> match)
    {
      Contract.Requires(match != null);
      Contract.Requires(startIndex >= 0);
      Contract.Requires(count >= 0);

      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < Count);
      return default(int);
    }
#endif

#if !SILVERLIGHT
    //
    // Summary:
    // Searches for an element that matches the conditions defined by the specified
    // predicate, and returns the last occurrence within the entire
    // System.Collections.Generic.List<T>.
    // 
    // Parameters:
    // match: 
    //    The System.Predicate<T> delegate that defines the conditions of the
    //    element to search for.
    // 
    // Returns:
    // The last element that matches the conditions defined by the specified
    // predicate, if found; otherwise, the default value for type T.
    // 
    // Exceptions:
    //    System.ArgumentNullException
    // 
    [Pure]
    public T FindLast(Predicate<T> match)
    {
      Contract.Requires(match != null);
      return default(T);
    }
#endif

#if !SILVERLIGHT
    //
    // Summary:
    // Searches for an element that matches the conditions defined by the specified
    // predicate, and returns the zero-based index of the last occurrence within
    // the entire System.Collections.Generic.List<T>.
    // 
    // Parameters:
    // match: 
    //    The System.Predicate<T> delegate that defines the conditions of the
    //    element to search for.
    // 
    // Returns:
    // The zero-based index of the last occurrence of an element that matches the
    // conditions defined by match, if found; otherwise, -1.
    // 
    // Exceptions:
    //    System.ArgumentNullException
    // 
    [Pure]
    public int FindLastIndex(Predicate<T> match)
    {
      Contract.Requires(match != null);
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < Count);

      return default(int);
    }
    //
    // Summary:
    // Searches for an element that matches the conditions defined by the specified
    // predicate, and returns the zero-based index of the last occurrence within
    // the range of elements in the System.Collections.Generic.List<T> that extends
    // from the first element to the specified index.
    // 
    // Parameters:
    // startIndex: 
    //    The zero-based starting index of the backward search.
    // match: 
    //    The System.Predicate<T> delegate that defines the conditions of the
    //    element to search for.
    // 
    // Returns:
    // The zero-based index of the last occurrence of an element that matches the
    // conditions defined by match, if found; otherwise, -1.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    // 
    //    System.ArgumentNullException
    // 
    [Pure]
    public int FindLastIndex(int startIndex, Predicate<T> match)
    {
      Contract.Requires(startIndex >= 0);
      Contract.Requires(match != null);
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() <= startIndex);

      return default(int);
    }
    //
    // Summary:
    // Searches for an element that matches the conditions defined by the specified
    // predicate, and returns the zero-based index of the last occurrence within
    // the range of elements in the System.Collections.Generic.List<T> that
    // contains the specified number of elements and ends at the specified index.
    // 
    // Parameters:
    // count: 
    //    The number of elements in the section to search.
    // startIndex: 
    //    The zero-based starting index of the backward search.
    // match: 
    //    The System.Predicate<T> delegate that defines the conditions of the
    //    element to search for.
    // 
    // Returns:
    // The zero-based index of the last occurrence of an element that matches the
    // conditions defined by match, if found; otherwise, -1.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    // 
    //    System.ArgumentNullException
    // 
    [Pure]
    public int FindLastIndex(int startIndex, int count, Predicate<T> match)
    {
      Contract.Requires(startIndex >= 0);
      Contract.Requires(count >= 0);

      Contract.Requires(match != null);
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() <= startIndex);

      return default(int);
    }
#endif

    //
    // Summary:
    // Performs the specified action on each element of the
    // System.Collections.Generic.List<T>.
    // 
    // Parameters:
    // action: 
    //    The System.Action<T> delegate to perform on each element of the
    //    System.Collections.Generic.List<T>.
    // 
    // Exceptions:
    //    System.ArgumentNullException
    // 
    public void ForEach(Action<T> action)
    {
      Contract.Requires(action != null);
    }
    //
    // Summary:
    //     Returns an enumerator that iterates through the System.Collections.Generic.List<T>.
    //
    // Returns:
    //     A System.Collections.Generic.List<T>.Enumerator for the System.Collections.Generic.List<T>.
    [Pure]
    [GlobalAccess(false)]
    [Escapes(true, false)]
    public List<T>.Enumerator GetEnumerator() {
      // since this is not related to the interfae implementation, we have to repeat the interface implementation post condition.
      Contract.Ensures(Contract.Result<List<T>.Enumerator>().Model == this.Model);
      Contract.Ensures(Contract.Result<List<T>.Enumerator>().CurrentIndex == -1);
      return default(List<T>.Enumerator);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    [ContractModel]
    public object[] Model {
      get { throw new NotImplementedException(); }
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      return this.GetEnumerator();
    }
    //
    // Summary:
    // Creates a shallow copy of a range of elements in the source
    // System.Collections.Generic.List<T>.
    // 
    // Parameters:
    // count: 
    //    The number of elements in the range.
    // index: 
    //    The zero-based System.Collections.Generic.List<T> index at which the
    //    range starts.
    // 
    // Returns:
    // A shallow copy of a range of elements in the source
    // System.Collections.Generic.List<T>.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    // 
    //    System.ArgumentException
    // 
    [Pure]
    public List<T> GetRange(int index, int count)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index + count <= Count);

      Contract.Ensures(Contract.Result<List<T>>() != null);

      return default(List<T>);
    }
    //
    // Summary:
    // Searches for the specified object and returns the zero-based index of the
    // first occurrence within the range of elements in the
    // System.Collections.Generic.List<T> that extends from the specified index to
    // the last element.
    // 
    // Parameters:
    // item: 
    //    The object to locate in the System.Collections.Generic.List<T>. The value
    //    can be null for reference types.
    // index: 
    //    The zero-based starting index of the search.
    // 
    // Returns:
    // The zero-based index of the first occurrence of item within the range of
    // elements in the System.Collections.Generic.List<T> that extends from index
    // to the last element, if found; otherwise, -1.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    // 
    [Pure]
    public int IndexOf(T item, int index)
    {
      Contract.Requires(index >= 0);

      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < Count);

      return default(int);
    }
    //
    // Summary:
    // Searches for the specified object and returns the zero-based index of the
    // first occurrence within the range of elements in the
    // System.Collections.Generic.List<T> that starts at the specified index and
    // contains the specified number of elements.
    // 
    // Parameters:
    // count: 
    //    The number of elements in the section to search.
    // item: 
    //    The object to locate in the System.Collections.Generic.List<T>. The value
    //    can be null for reference types.
    // index: 
    //    The zero-based starting index of the search.
    // 
    // Returns:
    // The zero-based index of the first occurrence of item within the range of
    // elements in the System.Collections.Generic.List<T> that starts at index and
    // contains count number of elements, if found; otherwise, -1.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    // 
    [Pure]
    public int IndexOf(T item, int index, int count)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);

      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < index + count);

      return default(int);
    }
    //
    // Summary:
    // Inserts the elements of a collection into the
    // System.Collections.Generic.List<T> at the specified index.
    // 
    // Parameters:
    // collection: 
    //    The collection whose elements should be inserted into the
    //    System.Collections.Generic.List<T>. The collection itself cannot be null,
    //    but it can contain elements that are null, if type T is a reference type.
    // index: 
    //    The zero-based index at which the new elements should be inserted.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    // 
    //    System.ArgumentNullException
    // 
    public void InsertRange(int index, IEnumerable<T> collection)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index <= this.Count);

      Contract.Requires(collection != null);
    }
    //
    // Summary:
    // Searches for the specified object and returns the zero-based index of the
    // last occurrence within the entire System.Collections.Generic.List<T>.
    // 
    // Parameters:
    // item: 
    //    The object to locate in the System.Collections.Generic.List<T>. The value
    //    can be null for reference types.
    // 
    // Returns:
    // The zero-based index of the last occurrence of item within the entire the
    // System.Collections.Generic.List<T>, if found; otherwise, -1.
    [Pure]
    public int LastIndexOf(T item)
    {
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < Count);

      return default(int);
    }
    //
    // Summary:
    // Searches for the specified object and returns the zero-based index of the
    // last occurrence within the range of elements in the
    // System.Collections.Generic.List<T> that extends from the first element to
    // the specified index.
    // 
    // Parameters:
    // item: 
    //    The object to locate in the System.Collections.Generic.List<T>. The value
    //    can be null for reference types.
    // index: 
    //    The zero-based starting index of the backward search.
    // 
    // Returns:
    // The zero-based index of the last occurrence of item within the range of
    // elements in the System.Collections.Generic.List<T> that extends from the
    // first element to index, if found; otherwise, -1.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    //
    [Pure]
    public int LastIndexOf(T item, int index)
    {
      Contract.Requires(index >= 0);

      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() <= index);
      return default(int);
    }


    //
    // Summary:
    // Searches for the specified object and returns the zero-based index of the
    // last occurrence within the range of elements in the
    // System.Collections.Generic.List<T> that contains the specified number of
    // elements and ends at the specified index.
    // 
    // Parameters:
    // count: 
    //    The number of elements in the section to search.
    // item: 
    //    The object to locate in the System.Collections.Generic.List<T>. The value
    //    can be null for reference types.
    // index: 
    //    The zero-based starting index of the backward search.
    // 
    // Returns:
    // The zero-based index of the last occurrence of item within the range of
    // elements in the System.Collections.Generic.List<T> that contains count
    // number of elements and ends at index, if found; otherwise, -1.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    // 
    [Pure]
    public int LastIndexOf(T item, int index, int count)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index >= count);

      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() <= index);
      return default(int);
    }

#if !SILVERLIGHT
    //
    // Summary:
    // Removes the all the elements that match the conditions defined by the
    // specified predicate.
    // 
    // Parameters:
    // match: 
    //    The System.Predicate<T> delegate that defines the conditions of the
    //    elements to remove.
    // 
    // Returns:
    // The number of elements removed from the System.Collections.Generic.List<T> .
    // 
    // Exceptions:
    //    System.ArgumentNullException
    // 
    public int RemoveAll(Predicate<T> match)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Count <= Contract.OldValue(Count));
      
      return default(int);
    }
#endif

    //
    // Summary:
    // Removes a range of elements from the System.Collections.Generic.List<T>.
    // 
    // Parameters:
    // count: 
    //    The number of elements to remove.
    // index: 
    //    The zero-based starting index of the range of elements to remove.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    // 
    //    System.ArgumentException
    // 
    public void RemoveRange(int index, int count)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);

      Contract.Ensures(Count <= Contract.OldValue(Count));
    }

    //
    // Summary:
    // Reverses the order of the elements in the entire
    // System.Collections.Generic.List<T>.
    public void Reverse()
    {
      Contract.Ensures(Count == Contract.OldValue(Count));
    }
    //
    // Summary:
    // Reverses the order of the elements in the specified range.
    // 
    // Parameters:
    // count: 
    //    The number of elements in the range to reverse.
    // index: 
    //    The zero-based starting index of the range to reverse.
    // 
    // Exceptions:
    //    System.ArgumentException
    // 
    //    System.ArgumentOutOfRangeException
    // 
    public void Reverse(int index, int count)
    {
      Contract.Ensures(Count == Contract.OldValue(Count));
    }
    //
    // Summary:
    // Sorts the elements in the entire System.Collections.Generic.List<T> using
    // the default comparer.
    // 
    // Exceptions:
    //    System.InvalidOperationException
    // 
    public void Sort()
    {
      Contract.Ensures(Count == Contract.OldValue(Count));
    }
    //
    // Summary:
    // Sorts the elements in the entire System.Collections.Generic.List<T> using
    // the specified System.Comparison<T>.
    // 
    // Parameters:
    // comparison: 
    //    The System.Comparison<T> to use when comparing elements.
    // 
    // Exceptions:
    //    System.ArgumentNullException
    // 
    public void Sort(Comparison<T> comparison)
    {
      Contract.Ensures(Count == Contract.OldValue(Count));
    }
    //
    // Summary:
    // Sorts the elements in the entire System.Collections.Generic.List<T> using
    // the specified comparer.
    // 
    // Parameters:
    // comparer: 
    //    The System.Collections.Generic.IComparer<T> implementation to use when
    //    comparing elements.-or-null to use the default comparer
    //    System.Collections.Generic.Comparer<T>.Default.
    // 
    // Exceptions:
    //    System.InvalidOperationException
    // 
    public void Sort(IComparer<T> comparer)
    {
      Contract.Ensures(Count == Contract.OldValue(Count));
    }
    //
    // Summary:
    // Sorts the elements in a range of elements in
    // System.Collections.Generic.List<T> using the specified comparer.
    // 
    // Parameters:
    // count: 
    //    The length of the range to sort.
    // index: 
    //    The zero-based starting index of the range to sort.
    // comparer: 
    //    The System.Collections.Generic.IComparer<T> implementation to use when
    //    comparing elements.-or-null to use the default comparer
    //    System.Collections.Generic.Comparer<T>.Default.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    // 
    //    System.ArgumentException
    // 
    //    System.InvalidOperationException
    // 
    public void Sort(int index, int count, IComparer<T> comparer)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Ensures(Count == Contract.OldValue(Count));
    }
    //
    // Summary:
    // Copies the elements of the System.Collections.Generic.List<T> to a new
    // array.
    // 
    // Returns:
    // An array containing copies of the elements of the
    // System.Collections.Generic.List<T>.
    [Pure]
    public T[] ToArray()
    {
      Contract.Ensures(Contract.Result<T[]>() != null);
      Contract.Ensures(Contract.Result<T[]>().Length == Count);

      return default(T[]);
    }

    public void TrimExcess() {
    }

#if !SILVERLIGHT
    //
    // Summary:
    // Determines whether every element in the System.Collections.Generic.List<T>
    // matches the conditions defined by the specified predicate.
    // 
    // Parameters:
    // match: 
    //    The System.Predicate<T> delegate that defines the conditions to check
    //    against the elements.
    // 
    // Returns:
    // true, if every element in the System.Collections.Generic.List<T> matches the
    // conditions defined by the specified predicate; otherwise, false.
    // 
    // Exceptions:
    //    System.ArgumentNullException
    // 
    [Pure]
    public bool TrueForAll(Predicate<T> match)
    {
      Contract.Requires(match != null);
      return default(bool);
    }
#endif

    // Summary:
    //     Enumerates the elements of a System.Collections.Generic.List<T>.
    public struct Enumerator : IEnumerator<T>
    {
      #region IEnumerator<T> Members

      public T Current
      {
        get { throw new NotImplementedException(); }
      }

      #endregion

      #region IDisposable Members

      public void Dispose()
      {
        throw new NotImplementedException();
      }

      #endregion

      #region IEnumerator Members

      object IEnumerator.Current
      {
        get { throw new NotImplementedException(); }
      }

      public bool MoveNext()
      {
        throw new NotImplementedException();
      }

      void IEnumerator.Reset()
      {
        throw new NotImplementedException();
      }

      #endregion

      #region IEnumerator Members

      [ContractModel]
      public object[] Model {
        get { throw new NotImplementedException(); }
      }

      #endregion

      #region IEnumerator Members

      [ContractModel]
      public int CurrentIndex {
        get { throw new NotImplementedException(); }
      }

      #endregion
    }

    #region IList<T> Members

    public T this[int index]
    {
      get
      {
        Contract.Ensures(Contract.Result<T>().Equals((T)this.Model[index]));

        throw new Exception();
      }
      set
      {
        throw new Exception();
      }
    }

    extern public int IndexOf(T item);

    public void Insert(int index, T item)
    {
      Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);
    }

    extern public void RemoveAt(int index);

    #endregion

    #region ICollection<T> Members


    extern bool ICollection<T>.IsReadOnly
    {
      get;
    }

    public void Add(T item)
    {
      Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);
    }

    public void Clear()
    {
      Contract.Ensures(this.Count == 0);
    }

    extern public bool Contains(T item);

    public void CopyTo(T[] array, int arrayIndex)
    {
    }

    extern public bool Remove(T item);

    #endregion

  }
}

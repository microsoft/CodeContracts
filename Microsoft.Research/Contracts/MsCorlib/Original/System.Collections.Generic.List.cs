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
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;

using Microsoft.Contracts;

namespace System.Collections.Generic {
  public class List<T>  {
    // Summary:
    // Initializes a new instance of the System.Collections.Generic.List<T> class
    // that is empty and has the default initial capacity.
    public List();
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
      return default(List(IEnumerable<T>);
    }
    public List(int capacity) {

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
      return default(List(int);
    }
    public int Capacity { get; set; }
    //
    // Summary:
    // Gets the number of elements actually contained in the
    // System.Collections.Generic.List<T>.
    // 
    // Returns:
    // The number of elements actually contained in the
    // System.Collections.Generic.List<T>.
    public int Count { 
      get; 
        CodeContract.Ensures(result >= 0);
    }

    // Summary:
    // Gets or sets the element at the specified index.
    // 
    // Parameters:
    // index: 
    //    The zero-based index of the element to get or set.
    // 
    // Returns:
    // The element at the specified index.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    // 
    public T this[int index] {
	    [ResultNotNewlyAllocated]
	    get;
	      CodeContract.Requires(0 <= index && index < Count);
	    set;
	      CodeContract.Requires(0 <= index && index < Count);
	  }

    // Summary:
    // Adds an object to the end of the System.Collections.Generic.List<T>.
    // 
    // Parameters:
    // item: 
    //    The object to be added to the end of the
    //    System.Collections.Generic.List<T>. The value can be null for reference
    //    types.
    [WriteConfined]
    public void Add(T item) {
      CodeContract.Ensures(Count == old(Count) + 1);
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
    }
    public void AddRange(IEnumerable<T>! collection) {
    //
    // Summary:
    // Returns a read-only System.Collections.Generic.IList<T> wrapper.
    // 
    // Returns:
    // A System.Collections.Generic.ReadOnlyCollection`1 that acts as a read-only
    // wrapper around the current System.Collections.Generic.List<T>.
    }
    [Pure]
    public ReadOnlyCollection<T> AsReadOnly() {
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
      CodeContract.Ensures(CodeContract.Result<ReadOnlyCollection<T>>() != null);
      return default(ReadOnlyCollection<T>);
    }
    public int BinarySearch(T item) {
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
      return default(int);
    }
    public int BinarySearch(T item, IComparer<T> comparer) {
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
      return default(int);
    }
    public int BinarySearch(int index, int count, T item, IComparer<T> comparer) {
    //
    // Summary:
    // Removes all elements from the System.Collections.Generic.List<T>.
      return default(int);
    }
    [WriteConfined]
    public void Clear() {
    //
    // Summary:
    // Determines whether an element is in the System.Collections.Generic.List<T>.
    // 
    // Parameters:
    // item: 
    //    The object to locate in the System.Collections.Generic.List<T>. The value
    //    can be null for reference types.
    // 
    // Returns:
    // true if item is found in the System.Collections.Generic.List<T>; otherwise,
    // false.
    }
    [Pure]
    public bool Contains(T item) {
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
      return default(bool);
    }
    public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter) {
    //
    // Summary:
    // Copies the entire System.Collections.Generic.List<T> to a compatible
    // one-dimensional array, starting at the beginning of the target array.
    // 
    // Parameters:
    // array: 
    //    The one-dimensional System.Array that is the destination of the elements
    //    copied from System.Collections.Generic.List<T>. The System.Array must
    //    have zero-based indexing.
    // 
    // Exceptions:
    //    System.ArgumentException
    // 
    //    System.ArgumentNullException
    // 
      return default(List<TOutput>);
    }
    public void CopyTo(T[] array) {
    //
    // Summary:
    // Copies the entire System.Collections.Generic.List<T> to a compatible
    // one-dimensional System.Array, starting at the specified index of the target
    // array.
    // 
    // Parameters:
    // array: 
    //    The one-dimensional System.Array that is the destination of the elements
    //    copied from System.Collections.Generic.List<T>. The System.Array must
    //    have zero-based indexing.
    // arrayIndex: 
    //    The zero-based index in array at which copying begins.
    // 
    // Exceptions:
    //    System.ArgumentException
    // 
    //    System.ArgumentOutOfRangeException
    // 
    //    System.ArgumentNullException
    // 
    }
    public void CopyTo(T[] array, int arrayIndex) {
    //
    // Summary:
    // Copies a range of elements from the System.Collections.Generic.List<T> to a
    // compatible one-dimensional System.Array, starting at the specified index of
    // the target array.
    // 
    // Parameters:
    // array: 
    //    The one-dimensional System.Array that is the destination of the elements
    //    copied from System.Collections.Generic.List<T>. The System.Array must
    //    have zero-based indexing.
    // count: 
    //    The number of elements to copy.
    // arrayIndex: 
    //    The zero-based index in array at which copying begins.
    // index: 
    //    The zero-based index in the source System.Collections.Generic.List<T> at
    //    which copying begins.
    // 
    // Exceptions:
    //    System.ArgumentNullException
    // 
    //    System.ArgumentException
    // 
    //    System.ArgumentOutOfRangeException
    // 
    }
    public void CopyTo(int index, T[] array, int arrayIndex, int count) {
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
    }
    public bool Exists(Predicate<T> match) {
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
      return default(bool);
    }
    public T Find(Predicate<T> match) {
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
      return default(T);
    }
    public List<T> FindAll(Predicate<T> match) {
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
      return default(List<T>);
    }
    public int FindIndex(Predicate<T> match) {
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
      return default(int);
    }
    public int FindIndex(int startIndex, Predicate<T> match) {
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
      return default(int);
    }
    public int FindIndex(int startIndex, int count, Predicate<T> match) {
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
      return default(int);
    }
    public T FindLast(Predicate<T> match) {
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
      return default(T);
    }
    public int FindLastIndex(Predicate<T> match) {
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
      return default(int);
    }
    public int FindLastIndex(int startIndex, Predicate<T> match) {
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
      return default(int);
    }
    public int FindLastIndex(int startIndex, int count, Predicate<T> match) {
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
      return default(int);
    }
    public void ForEach(Action<T> action) {
    //
    // Summary:
    //     Returns an enumerator that iterates through the System.Collections.Generic.List<T>.
    //
    // Returns:
    //     A System.Collections.Generic.List<T>.Enumerator for the System.Collections.Generic.List<T>.
    }
    [Pure] [GlobalAccess(false)] [Escapes(true,false)]
    public List<T>.Enumerator GetEnumerator() {
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
      return default(List<T>.Enumerator);
    }
    public List<T> GetRange(int index, int count) {
    //
    // Summary:
    // Searches for the specified object and returns the zero-based index of the
    // first occurrence within the entire System.Collections.Generic.List<T>.
    // 
    // Parameters:
    // item: 
    //    The object to locate in the System.Collections.Generic.List<T>. The value
    //    can be null for reference types.
    // 
    // Returns:
    // The zero-based index of the first occurrence of item within the entire
    // System.Collections.Generic.List<T>, if found; otherwise, -1.
      return default(List<T>);
    }
    public int IndexOf(T item) {
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
      return default(int);
    }
    public int IndexOf(T item, int index) {
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
      return default(int);
    }
    public int IndexOf(T item, int index, int count) {
    //
    // Summary:
    // Inserts an element into the System.Collections.Generic.List<T> at the
    // specified index.
    // 
    // Parameters:
    // item: 
    //    The object to insert. The value can be null for reference types.
    // index: 
    //    The zero-based index at which item should be inserted.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    // 
      return default(int);
    }
    public void Insert(int index, T item) {
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
    }
    public void InsertRange(int index, IEnumerable<T> collection) {
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
    }
    public int LastIndexOf(T item) {
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
      return default(int);
    }
    public int LastIndexOf(T item, int index) {
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
      return default(int);
    }
    public int LastIndexOf(T item, int index, int count) {
    //
    // Summary:
    // Removes the first occurrence of a specific object from the
    // System.Collections.Generic.List<T>.
    // 
    // Parameters:
    // item: 
    //    The object to remove from the System.Collections.Generic.List<T>. The
    //    value can be null for reference types.
    // 
    // Returns:
    // true if item is successfully removed; otherwise, false.  This method also
    // returns false if item was not found in the original
    // System.Collections.Generic.List<T>.
      return default(int);
    }
    public bool Remove(T item) {
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
      return default(bool);
    }
    public int RemoveAll(Predicate<T> match) {
    //
    // Summary:
    // Removes the element at the specified index of the
    // System.Collections.Generic.List<T>.
    // 
    // Parameters:
    // index: 
    //    The zero-based index of the element to remove.
    // 
    // Exceptions:
    //    System.ArgumentOutOfRangeException
    // 
      return default(int);
    }
    public void RemoveAt(int index) {
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
    }
    public void RemoveRange(int index, int count) {
    //
    // Summary:
    // Reverses the order of the elements in the entire
    // System.Collections.Generic.List<T>.
    }
    public void Reverse() {
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
    }
    public void Reverse(int index, int count) {
    //
    // Summary:
    // Sorts the elements in the entire System.Collections.Generic.List<T> using
    // the default comparer.
    // 
    // Exceptions:
    //    System.InvalidOperationException
    // 
    }
    public void Sort() {
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
    }
    public void Sort(Comparison<T> comparison) {
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
    }
    public void Sort(IComparer<T> comparer) {
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
    }
    public void Sort(int index, int count, IComparer<T> comparer) {
    //
    // Summary:
    // Copies the elements of the System.Collections.Generic.List<T> to a new
    // array.
    // 
    // Returns:
    // An array containing copies of the elements of the
    // System.Collections.Generic.List<T>.
    }
    public T[] ToArray() {
      CodeContract.Ensures(Owner.None(result));
      
      CodeContract.Ensures(CodeContract.Result<T[]>() != null);
      return default(T[]);
    }
    public void TrimExcess() {
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
    }
    public bool TrueForAll(Predicate<T> match) {

    // Summary:
    //     Enumerates the elements of a System.Collections.Generic.List<T>.
      return default(bool);
    }
    [Serializable]
    public struct Enumerator { //: IEnumerator<T>, IDisposable, IEnumerator {

      // Summary:
      //     Gets the element at the current position of the enumerator.
      //
      // Returns:
      //     The element in the System.Collections.Generic.List<T> at the current position
      //     of the enumerator.
      public T Current { get; }

      // Summary:
      //     Releases all resources used by the System.Collections.Generic.List<T>.Enumerator.
      public void Dispose() {
      //
      // Summary:
      //     Advances the enumerator to the next element of the System.Collections.Generic.List<T>.
      //
      // Returns:
      //     true if the enumerator was successfully advanced to the next element; false
      //     if the enumerator has passed the end of the collection.
      //
      // Exceptions:
      //   System.InvalidOperationException:
      //     The collection was modified after the enumerator was created.
      }
      public bool MoveNext() {
        return default(bool);
      }
    }
  }
}

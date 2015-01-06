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

#if NETFRAMEWORK_4_0
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.Serialization;
using System.Diagnostics.Contracts;

namespace System.Collections.Generic
{
  public class SortedSet<T> // : ISet<T>, ICollection<T>, IEnumerable<T>, ICollection, IEnumerable, ISerializable, IDeserializationCallback
  {
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.SortedSet<T>
    //     class.
    public SortedSet()
    {
      Contract.Ensures(this.Count == 0);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.SortedSet<T>
    //     class that uses a specified comparer.
    //
    // Parameters:
    //   comparer:
    //     The default comparer to use for comparing objects.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     comparer is null.
    public SortedSet(IComparer<T> comparer)
    {
      Contract.Requires(comparer != null);
      Contract.Ensures(this.Count == 0);
    }

    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.SortedSet<T>
    //     class that contains elements copied from a specified enumerable collection.
    //
    // Parameters:
    //   collection:
    //     The enumerable collection to be copied.
    public SortedSet(IEnumerable<T> collection)
    {
      Contract.Requires(collection != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.SortedSet<T>
    //     class that contains elements copied from a specified enumerable collection
    //     and that uses a specified comparer.
    //
    // Parameters:
    //   collection:
    //     The enumerable collection to be copied.
    //
    //   comparer:
    //     The default comparer to use for comparing objects.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     collection is null.
    public SortedSet(IEnumerable<T> collection, IComparer<T> comparer)
    {
      Contract.Requires(collection != null);
    }

    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.SortedSet<T>
    //     class that contains serialized data.
    //
    // Parameters:
    //   info:
    //     The object that contains the information that is required to serialize the
    //     System.Collections.Generic.SortedSet<T> object.
    //
    //   context:
    //     The structure that contains the source and destination of the serialized
    //     stream associated with the System.Collections.Generic.SortedSet<T> object.
    //protected SortedSet(SerializationInfo info, StreamingContext context);

    // Summary:
    //     Gets the System.Collections.Generic.IEqualityComparer<T> object that is used
    //     to determine equality for the values in the System.Collections.Generic.SortedSet<T>.
    //
    // Returns:
    //     The comparer that is used to determine equality for the values in the System.Collections.Generic.SortedSet<T>.
    public IComparer<T> Comparer
    {
      get
      {
        Contract.Ensures(Contract.Result<IComparer<T>>() != null);

        return default(IComparer<T>);
      }
    }
    //
    // Summary:
    //     Gets the number of elements in the System.Collections.Generic.SortedSet<T>.
    //
    // Returns:
    //     The number of elements in the System.Collections.Generic.SortedSet<T>.
    virtual public int Count
    {
      get { return default(int); }
    }

    //
    // Summary:
    //     Gets the maximum value in the System.Collections.Generic.SortedSet<T>, as
    //     defined by the comparer.
    //
    // Returns:
    //     The maximum value in the set.
    //public T Max { get; }
    //
    // Summary:
    //     Gets the minimum value in the System.Collections.Generic.SortedSet<T>, as
    //     defined by the comparer.
    //
    // Returns:
    //     The minimum value in the set.
    //public T Min { get; }

    // Summary:
    //     Adds an element to the set and returns a value that indicates if it was successfully
    //     added.
    //
    // Parameters:
    //   item:
    //     The element to add to the set.
    //
    // Returns:
    //     true if item is added to the set; otherwise, false.
    //public bool Add(T item);
    //
    // Summary:
    //     Removes all elements from the set.
    //public virtual void Clear();
    //
    // Summary:
    //     Determines whether the set contains a specific element.
    //
    // Parameters:
    //   item:
    //     The element to locate in the set.
    //
    // Returns:
    //     true if the set contains item; otherwise, false.
    //public virtual bool Contains(T item);
    //
    // Summary:
    //     Copies the complete System.Collections.Generic.SortedSet<T> to a compatible
    //     one-dimensional array, starting at the beginning of the target array.
    //
    // Parameters:
    //   array:
    //     A one-dimensional array that is the destination of the elements copied from
    //     the System.Collections.Generic.SortedSet<T>.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The number of elements in the source System.Collections.Generic.SortedSet<T>
    //     exceeds the number of elements that the destination array can contain.
    //
    //   System.ArgumentNullException:
    //     array is null.
    //public void CopyTo(T[] array);
    //
    // Summary:
    //     Copies the complete System.Collections.Generic.SortedSet<T> to a compatible
    //     one-dimensional array, starting at the specified array index.
    //
    // Parameters:
    //   array:
    //     A one-dimensional array that is the destination of the elements copied from
    //     the System.Collections.Generic.SortedSet<T>. The array must have zero-based
    //     indexing.
    //
    //   index:
    //     The zero-based index in array at which copying begins.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The number of elements in the source array is greater than the available
    //     space from index to the end of the destination array.
    //
    //   System.ArgumentNullException:
    //     array is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero.
    //public void CopyTo(T[] array, int index);
    //
    // Summary:
    //     Copies a specified number of elements from System.Collections.Generic.SortedSet<T>
    //     to a compatible one-dimensional array, starting at the specified array index.
    //
    // Parameters:
    //   array:
    //     A one-dimensional array that is the destination of the elements copied from
    //     the System.Collections.Generic.SortedSet<T>. The array must have zero-based
    //     indexing.
    //
    //   index:
    //     The zero-based index in array at which copying begins.
    //
    //   count:
    //     The number of elements to copy.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The number of elements in the source array is greater than the available
    //     space from index to the end of the destination array.
    //
    //   System.ArgumentNullException:
    //     array is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero.-or-count is less than zero.
    //public void CopyTo(T[] array, int index, int count);
    //
    // Summary:
    //     Returns an System.Collections.IEqualityComparer object that can be used to
    //     create a collection that contains individual sets.
    //
    // Returns:
    //     A comparer for creating a collection of sets.
    public static IEqualityComparer<SortedSet<T>> CreateSetComparer()
    {
      Contract.Ensures(Contract.Result<IEqualityComparer<SortedSet<T>>>() != null);

      return default(IEqualityComparer<SortedSet<T>>);
    }
    //
    // Summary:
    //     Returns an System.Collections.IEqualityComparer object, according to a specified
    //     comparer, that can be used to create a collection that contains individual
    //     sets.
    //
    // Parameters:
    //   memberEqualityComparer:
    //     The comparer to use for creating the returned comparer.
    //
    // Returns:
    //     A comparer for creating a collection of sets.
    public static IEqualityComparer<SortedSet<T>> CreateSetComparer(IEqualityComparer<T> memberEqualityComparer)
    {
      Contract.Ensures(Contract.Result<IEqualityComparer<SortedSet<T>>>() != null);

      return default(IEqualityComparer<SortedSet<T>>);
    }

    //
    // Summary:
    //     Removes all elements that are in a specified collection from the current
    //     System.Collections.Generic.SortedSet<T> object.
    //
    // Parameters:
    //   other:
    //     The collection of items to remove from the System.Collections.Generic.SortedSet<T>
    //     object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    //public void ExceptWith(IEnumerable<T> other)
    //{
    //}
    //
    // Summary:
    //     Returns an enumerator that iterates through the System.Collections.Generic.SortedSet<T>.
    //
    // Returns:
    //     An enumerator that iterates through the System.Collections.Generic.SortedSet<T>.
    //public SortedSet<T>.Enumerator GetEnumerator();
    //
    // Summary:
    //     Implements the System.Runtime.Serialization.ISerializable interface and returns
    //     the data that you must have to serialize a System.Collections.Generic.SortedSet<T>
    //     object.
    //
    // Parameters:
    //   info:
    //     A System.Runtime.Serialization.SerializationInfo object that contains the
    //     information that is required to serialize the System.Collections.Generic.SortedSet<T>
    //     object.
    //
    //   context:
    //     A System.Runtime.Serialization.StreamingContext structure that contains the
    //     source and destination of the serialized stream associated with the System.Collections.Generic.SortedSet<T>
    //     object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     info is null.
    //protected virtual void GetObjectData(SerializationInfo info, StreamingContext context);
    //
    // Summary:
    //     Returns a view of a subset in a System.Collections.Generic.SortedSet<T>.
    //
    // Parameters:
    //   lowerValue:
    //     The lowest desired value in the view.
    //
    //   upperValue:
    //     The highest desired value in the view.
    //
    // Returns:
    //     A subset view that contains only the values in the specified range.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     lowerValue is more than upperValue according to the comparer.
    //
    //   System.ArgumentOutOfRangeException:
    //     A tried operation on the view was outside the range specified by lowerValue
    //     and upperValue.
    [Pure]
    public virtual SortedSet<T> GetViewBetween(T lowerValue, T upperValue)
    {
      Contract.Ensures(Contract.Result<SortedSet<T>>() != null);

      return default(SortedSet<T>);
    }
    //
    // Summary:
    //     Modifies the current System.Collections.Generic.SortedSet<T> object so that
    //     it contains only elements that are also in a specified collection.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current System.Collections.Generic.SortedSet<T>
    //     object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    //public virtual void IntersectWith(IEnumerable<T> other);
    //
    // Summary:
    //     Determines whether a System.Collections.Generic.SortedSet<T> object is a
    //     proper subset of the specified collection.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current System.Collections.Generic.SortedSet<T>
    //     object.
    //
    // Returns:
    //     true if the System.Collections.Generic.SortedSet<T> object is a proper subset
    //     of other; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    //public bool IsProperSubsetOf(IEnumerable<T> other);
    //
    // Summary:
    //     Determines whether a System.Collections.Generic.SortedSet<T> object is a
    //     proper superset of the specified collection.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current System.Collections.Generic.SortedSet<T>
    //     object.
    //
    // Returns:
    //     true if the System.Collections.Generic.SortedSet<T> object is a proper superset
    //     of other; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    //public bool IsProperSupersetOf(IEnumerable<T> other);
    //
    // Summary:
    //     Determines whether a System.Collections.Generic.SortedSet<T> object is a
    //     subset of the specified collection.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current System.Collections.Generic.SortedSet<T>
    //     object.
    //
    // Returns:
    //     true if the current System.Collections.Generic.SortedSet<T> object is a subset
    //     of other; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    //public bool IsSubsetOf(IEnumerable<T> other);
    //
    // Summary:
    //     Determines whether a System.Collections.Generic.SortedSet<T> object is a
    //     superset of the specified collection.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current System.Collections.Generic.SortedSet<T>
    //     object.
    //
    // Returns:
    //     true if the System.Collections.Generic.SortedSet<T> object is a superset
    //     of other; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    //public bool IsSupersetOf(IEnumerable<T> other);
    //
    // Summary:
    //     Implements the System.Runtime.Serialization.ISerializable interface, and
    //     raises the deserialization event when the deserialization is completed.
    //
    // Parameters:
    //   sender:
    //     The source of the deserialization event.
    //
    // Exceptions:
    //   System.Runtime.Serialization.SerializationException:
    //     The System.Runtime.Serialization.SerializationInfo object associated with
    //     the current System.Collections.Generic.SortedSet<T> object is invalid.
    //protected virtual void OnDeserialization(object sender);
    //
    // Summary:
    //     Determines whether the current System.Collections.Generic.SortedSet<T> object
    //     and a specified collection share common elements.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current System.Collections.Generic.SortedSet<T>
    //     object.
    //
    // Returns:
    //     true if the System.Collections.Generic.SortedSet<T> object and other share
    //     at least one common element; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    //public bool Overlaps(IEnumerable<T> other);
    //
    // Summary:
    //     Removes a specified item from the System.Collections.Generic.SortedSet<T>.
    //
    // Parameters:
    //   item:
    //     The element to remove.
    //
    // Returns:
    //     true if the element is found and successfully removed; otherwise, false.
    //public bool Remove(T item);
    //
    // Summary:
    //     Removes all elements that match the conditions defined by the specified predicate
    //     from a System.Collections.Generic.SortedSet<T>.
    //
    // Parameters:
    //   match:
    //     The delegate that defines the conditions of the elements to remove.
    //
    // Returns:
    //     The number of elements that were removed from the System.Collections.Generic.SortedSet<T>
    //     collection..
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     match is null.
    public int RemoveWhere(Predicate<T> match)
    {
      Contract.Requires(match != null);

      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    //
    // Summary:
    //     Returns an System.Collections.Generic.IEnumerable<T> that iterates over the
    //     System.Collections.Generic.SortedSet<T> in reverse order.
    //
    // Returns:
    //     An enumerator that iterates over the System.Collections.Generic.SortedSet<T>
    //     in reverse order.
    //public IEnumerable<T> Reverse();
    //
    // Summary:
    //     Determines whether the current System.Collections.Generic.SortedSet<T> object
    //     and the specified collection contain the same elements.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current System.Collections.Generic.SortedSet<T>
    //     object.
    //
    // Returns:
    //     true if the current System.Collections.Generic.SortedSet<T> object is equal
    //     to other; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    //public bool SetEquals(IEnumerable<T> other);
    //
    // Summary:
    //     Modifies the current System.Collections.Generic.SortedSet<T> object so that
    //     it contains only elements that are present either in the current object or
    //     in the specified collection, but not both.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current System.Collections.Generic.SortedSet<T>
    //     object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    //public void SymmetricExceptWith(IEnumerable<T> other);
    //
    // Summary:
    //     Modifies the current System.Collections.Generic.SortedSet<T> object so that
    //     it contains all elements that are present in both the current object and
    //     in the specified collection.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current System.Collections.Generic.SortedSet<T>
    //     object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    //public void UnionWith(IEnumerable<T> other);

    
    // Summary:
    //     Enumerates the elements of a System.Collections.Generic.SortedSet<T> object.
    //[Serializable]
    //public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator, ISerializable, IDeserializationCallback
    //{

      // Summary:
      //     Gets the element at the current position of the enumerator.
      //
      // Returns:
      //     The element in the collection at the current position of the enumerator.
      //public T Current { get; }

      // Summary:
      //     Releases all resources used by the System.Collections.Generic.SortedSet<T>.Enumerator.
      //public void Dispose();
      //
      // Summary:
      //     Advances the enumerator to the next element of the System.Collections.Generic.SortedSet<T>
      //     collection.
      //
      // Returns:
      //     true if the enumerator was successfully advanced to the next element; false
      //     if the enumerator has passed the end of the collection.
      //
      // Exceptions:
      //   System.InvalidOperationException:
      //     The collection was modified after the enumerator was created.
      //public bool MoveNext();
    //}
  }
}
#endif
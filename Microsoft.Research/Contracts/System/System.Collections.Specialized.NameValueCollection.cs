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

#if !SILVERLIGHT

using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;
using System.Diagnostics.Contracts;

namespace System.Collections.Specialized
{
  // Summary:
  //     Represents a collection of associated System.String keys and System.String
  //     values that can be accessed either with the key or with the index.
  public class NameValueCollection : NameObjectCollectionBase
  {
    // Summary:
    //     Initializes a new instance of the System.Collections.Specialized.NameValueCollection
    //     class that is empty, has the default initial capacity and uses the default
    //     case-insensitive hash code provider and the default case-insensitive comparer.
    extern public NameValueCollection();
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Specialized.NameValueCollection
    //     class that is empty, has the default initial capacity, and uses the specified
    //     System.Collections.IEqualityComparer object.
    //
    // Parameters:
    //   equalityComparer:
    //     The System.Collections.IEqualityComparer object to use to determine whether
    //     two keys are equal and to generate hash codes for the keys in the collection.
    extern public NameValueCollection(IEqualityComparer equalityComparer);
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Specialized.NameValueCollection
    //     class that is empty, has the specified initial capacity and uses the default
    //     case-insensitive hash code provider and the default case-insensitive comparer.
    //
    // Parameters:
    //   capacity:
    //     The initial number of entries that the System.Collections.Specialized.NameValueCollection
    //     can contain.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     capacity is less than zero.
    public NameValueCollection(int capacity)
    {
      Contract.Requires(capacity >= 0);
    }
    //
    // Summary:
    //     Copies the entries from the specified System.Collections.Specialized.NameValueCollection
    //     to a new System.Collections.Specialized.NameValueCollection with the same
    //     initial capacity as the number of entries copied and using the same hash
    //     code provider and the same comparer as the source collection.
    //
    // Parameters:
    //   col:
    //     The System.Collections.Specialized.NameValueCollection to copy to the new
    //     System.Collections.Specialized.NameValueCollection instance.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     col is null.
    public NameValueCollection(NameValueCollection col)
    {
      Contract.Requires(col != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Specialized.NameValueCollection
    //     class that is empty, has the specified initial capacity, and uses the specified
    //     System.Collections.IEqualityComparer object.
    //
    // Parameters:
    //   capacity:
    //     The initial number of entries that the System.Collections.Specialized.NameValueCollection
    //     object can contain.
    //
    //   equalityComparer:
    //     The System.Collections.IEqualityComparer object to use to determine whether
    //     two keys are equal and to generate hash codes for the keys in the collection.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     capacity is less than zero.
    public NameValueCollection(int capacity, IEqualityComparer equalityComparer)
    {
      Contract.Requires(capacity >= 0);
    }
    //
    // Summary:
    //     Copies the entries from the specified System.Collections.Specialized.NameValueCollection
    //     to a new System.Collections.Specialized.NameValueCollection with the specified
    //     initial capacity or the same initial capacity as the number of entries copied,
    //     whichever is greater, and using the default case-insensitive hash code provider
    //     and the default case-insensitive comparer.
    //
    // Parameters:
    //   capacity:
    //     The initial number of entries that the System.Collections.Specialized.NameValueCollection
    //     can contain.
    //
    //   col:
    //     The System.Collections.Specialized.NameValueCollection to copy to the new
    //     System.Collections.Specialized.NameValueCollection instance.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     capacity is less than zero.
    //
    //   System.ArgumentNullException:
    //     col is null.
    public NameValueCollection(int capacity, NameValueCollection col)
    {
      Contract.Requires(capacity >= 0);
      Contract.Requires(col != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Specialized.NameValueCollection
    //     class that is serializable and uses the specified System.Runtime.Serialization.SerializationInfo
    //     and System.Runtime.Serialization.StreamingContext.
    //
    // Parameters:
    //   info:
    //     A System.Runtime.Serialization.SerializationInfo object that contains the
    //     information required to serialize the new System.Collections.Specialized.NameValueCollection
    //     instance.
    //
    //   context:
    //     A System.Runtime.Serialization.StreamingContext object that contains the
    //     source and destination of the serialized stream associated with the new System.Collections.Specialized.NameValueCollection
    //     instance.
    extern protected NameValueCollection(SerializationInfo info, StreamingContext context);

    // Summary:
    //     Gets all the keys in the System.Collections.Specialized.NameValueCollection.
    //
    // Returns:
    //     A System.String array that contains all the keys of the System.Collections.Specialized.NameValueCollection.
    public virtual string[] AllKeys
    {
      get
      {
        Contract.Ensures(Contract.Result<string[]>() != null);
        return default(string[]);
      }
    }

    // Summary:
    //     Gets the entry at the specified index of the System.Collections.Specialized.NameValueCollection.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the entry to locate in the collection.
    //
    // Returns:
    //     A System.String that contains the comma-separated list of values at the specified
    //     index of the collection.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is outside the valid range of indexes for the collection.
    public string this[int index]
    {
      get
      {
        Contract.Requires(index >= 0);
        Contract.Requires(index < this.Count);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets or sets the entry with the specified key in the System.Collections.Specialized.NameValueCollection.
    //
    // Parameters:
    //   name:
    //     The System.String key of the entry to locate. The key can be null.
    //
    // Returns:
    //     A System.String that contains the comma-separated list of values associated
    //     with the specified key, if found; otherwise, null.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only and the operation attempts to modify the collection.
    extern public string this[string name] { get; set; }

    // Summary:
    //     Copies the entries in the specified System.Collections.Specialized.NameValueCollection
    //     to the current System.Collections.Specialized.NameValueCollection.
    //
    // Parameters:
    //   c:
    //     The System.Collections.Specialized.NameValueCollection to copy to the current
    //     System.Collections.Specialized.NameValueCollection.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    //
    //   System.ArgumentNullException:
    //     c is null.
    public void Add(NameValueCollection c)
    {
      Contract.Requires(c != null);
    }
    //
    // Summary:
    //     Adds an entry with the specified name and value to the System.Collections.Specialized.NameValueCollection.
    //
    // Parameters:
    //   name:
    //     The System.String key of the entry to add. The key can be null.
    //
    //   value:
    //     The System.String value of the entry to add. The value can be null.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    extern public virtual void Add(string name, string value);
    //
    // Summary:
    //     Invalidates the cached arrays and removes all entries from the System.Collections.Specialized.NameValueCollection.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    public virtual void Clear()
    {
      Contract.Ensures(Count == 0);
    }
    //
    // Summary:
    //     Copies the entire System.Collections.Specialized.NameValueCollection to a
    //     compatible one-dimensional System.Array, starting at the specified index
    //     of the target array.
    //
    // Parameters:
    //   dest:
    //     The one-dimensional System.Array that is the destination of the elements
    //     copied from System.Collections.Specialized.NameValueCollection. The System.Array
    //     must have zero-based indexing.
    //
    //   index:
    //     The zero-based index in dest at which copying begins.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     dest is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero.
    //
    //   System.ArgumentException:
    //     dest is multidimensional.  -or- index is equal to or greater than the length
    //     of dest.  -or- The number of elements in the source System.Collections.Specialized.NameValueCollection
    //     is greater than the available space from index to the end of the destination
    //     dest.
    //
    //   System.InvalidCastException:
    //     The type of the source System.Collections.Specialized.NameValueCollection
    //     cannot be cast automatically to the type of the destination dest.
    public void CopyTo(Array dest, int index)
    {
      Contract.Requires(dest != null);
      Contract.Requires(dest.Rank == 1);
      Contract.Requires(index >= 0);
      Contract.Requires(this.Count + index < dest.Length);
    }
    //
    // Summary:
    //     Gets the values at the specified index of the System.Collections.Specialized.NameValueCollection
    //     combined into one comma-separated list.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the entry that contains the values to get from the
    //     collection.
    //
    // Returns:
    //     A System.String that contains a comma-separated list of the values at the
    //     specified index of the System.Collections.Specialized.NameValueCollection,
    //     if found; otherwise, null.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is outside the valid range of indexes for the collection.
    [Pure]
    public virtual string Get(int index)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index < Count);
      return default(string);
    }
    //
    // Summary:
    //     Gets the values associated with the specified key from the System.Collections.Specialized.NameValueCollection
    //     combined into one comma-separated list.
    //
    // Parameters:
    //   name:
    //     The System.String key of the entry that contains the values to get. The key
    //     can be null.
    //
    // Returns:
    //     A System.String that contains a comma-separated list of the values associated
    //     with the specified key from the System.Collections.Specialized.NameValueCollection,
    //     if found; otherwise, null.
    [Pure]
    extern public virtual string Get(string name);
    //
    // Summary:
    //     Gets the key at the specified index of the System.Collections.Specialized.NameValueCollection.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the key to get from the collection.
    //
    // Returns:
    //     A System.String that contains the key at the specified index of the System.Collections.Specialized.NameValueCollection,
    //     if found; otherwise, null.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is outside the valid range of indexes for the collection.
    [Pure]
    public virtual string GetKey(int index)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index < Count);
      return default(string);
    }
    //
    // Summary:
    //     Gets the values at the specified index of the System.Collections.Specialized.NameValueCollection.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the entry that contains the values to get from the
    //     collection.
    //
    // Returns:
    //     A System.String array that contains the values at the specified index of
    //     the System.Collections.Specialized.NameValueCollection, if found; otherwise,
    //     null.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is outside the valid range of indexes for the collection.
    [Pure]
    public virtual string[] GetValues(int index)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index < Count);
      return default(string[]);
    }
    //
    // Summary:
    //     Gets the values associated with the specified key from the System.Collections.Specialized.NameValueCollection.
    //
    // Parameters:
    //   name:
    //     The System.String key of the entry that contains the values to get. The key
    //     can be null.
    //
    // Returns:
    //     A System.String array that contains the values associated with the specified
    //     key from the System.Collections.Specialized.NameValueCollection, if found;
    //     otherwise, null.
    [Pure]
    extern public virtual string[] GetValues(string name);
    //
    // Summary:
    //     Gets a value indicating whether the System.Collections.Specialized.NameValueCollection
    //     contains keys that are not null.
    //
    // Returns:
    //     true if the System.Collections.Specialized.NameValueCollection contains keys
    //     that are not null; otherwise, false.
    [Pure]
    extern public bool HasKeys();
    //
    // Summary:
    //     Resets the cached arrays of the collection to null.
    extern protected void InvalidateCachedArrays();
    //
    // Summary:
    //     Removes the entries with the specified key from the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    //
    // Parameters:
    //   name:
    //     The System.String key of the entry to remove. The key can be null.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    extern public virtual void Remove(string name);
    //
    // Summary:
    //     Sets the value of an entry in the System.Collections.Specialized.NameValueCollection.
    //
    // Parameters:
    //   name:
    //     The System.String key of the entry to add the new value to. The key can be
    //     null.
    //
    //   value:
    //     The System.Object that represents the new value to add to the specified entry.
    //     The value can be null.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    extern public virtual void Set(string name, string value);
  }
}

#endif

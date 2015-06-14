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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Collections.Concurrent
{
  public class ConcurrentDictionary<TKey, TValue> 
    //: IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary, ICollection, IEnumerable
  {
    // Summary:
    //     Initializes a new instance of the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     class that is empty, has the default concurrency level, has the default initial
    //     capacity, and uses the default comparer for the key type.
    public ConcurrentDictionary() { }
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     class that contains elements copied from the specified System.Collections.Generic.IEnumerable<T>,
    //     has the default concurrency level, has the default initial capacity, and
    //     uses the default comparer for the key type.
    //
    // Parameters:
    //   collection:
    //     The System.Collections.Generic.IEnumerable<T> whose elements are copied to
    //     the new System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     collection or any of its keys is a null reference (Nothing in Visual Basic)
    //
    //   System.ArgumentException:
    //     collection contains one or more duplicate keys.
    public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
    {
      Contract.Requires(collection != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     class that is empty, has the default concurrency level and capacity, and
    //     uses the specified System.Collections.Generic.IEqualityComparer<T>.
    //
    // Parameters:
    //   comparer:
    //     The System.Collections.Generic.IEqualityComparer<T> implementation to use
    //     when comparing keys.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     comparer is a null reference (Nothing in Visual Basic).
    public ConcurrentDictionary(IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(comparer != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     class that contains elements copied from the specified System.Collections.IEnumerable
    //     has the default concurrency level, has the default initial capacity, and
    //     uses the specified System.Collections.Generic.IEqualityComparer<T>.
    //
    // Parameters:
    //   collection:
    //     The System.Collections.Generic.IEnumerable<T> whose elements are copied to
    //     the new System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>.
    //
    //   comparer:
    //     The System.Collections.Generic.IEqualityComparer<T> implementation to use
    //     when comparing keys.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     collection is a null reference (Nothing in Visual Basic). -or- comparer is
    //     a null reference (Nothing in Visual Basic).
    public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(collection != null);
      Contract.Requires(comparer != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     class that is empty, has the specified concurrency level and capacity, and
    //     uses the default comparer for the key type.
    //
    // Parameters:
    //   concurrencyLevel:
    //     The estimated number of threads that will update the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     concurrently.
    //
    //   capacity:
    //     The initial number of elements that the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     can contain.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     concurrencyLevel is less than 1.-or-capacity is less than 0.
    public ConcurrentDictionary(int concurrencyLevel, int capacity)
    {
      Contract.Requires(concurrencyLevel >= 1);
      Contract.Requires(capacity >= 0);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     class that contains elements copied from the specified System.Collections.IEnumerable,
    //     and uses the specified System.Collections.Generic.IEqualityComparer<T>.
    //
    // Parameters:
    //   concurrencyLevel:
    //     The estimated number of threads that will update the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     concurrently.
    //
    //   collection:
    //     The System.Collections.Generic.IEnumerable<T> whose elements are copied to
    //     the new System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>.
    //
    //   comparer:
    //     The System.Collections.Generic.IEqualityComparer<T> implementation to use
    //     when comparing keys.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     collection is a null reference (Nothing in Visual Basic). -or- comparer is
    //     a null reference (Nothing in Visual Basic).
    //
    //   System.ArgumentOutOfRangeException:
    //     concurrencyLevel is less than 1.
    //
    //   System.ArgumentException:
    //     collection contains one or more duplicate keys.
    public ConcurrentDictionary(int concurrencyLevel, IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
    {
        Contract.Requires(concurrencyLevel >= 1);
        Contract.Requires(collection != null);
        Contract.Requires(comparer != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     class that is empty, has the specified concurrency level, has the specified
    //     initial capacity, and uses the specified System.Collections.Generic.IEqualityComparer<T>.
    //
    // Parameters:
    //   concurrencyLevel:
    //     The estimated number of threads that will update the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     concurrently.
    //
    //   capacity:
    //     The initial number of elements that the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     can contain.
    //
    //   comparer:
    //     The System.Collections.Generic.IEqualityComparer<T> implementation to use
    //     when comparing keys.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     comparer is a null reference (Nothing in Visual Basic).
    //
    //   System.ArgumentOutOfRangeException:
    //     concurrencyLevel is less than 1. -or- capacity is less than 0.
    public ConcurrentDictionary(int concurrencyLevel, int capacity, IEqualityComparer<TKey> comparer)
    {
      Contract.Requires(concurrencyLevel >= 1);
      Contract.Requires(capacity >= 0);
      Contract.Requires(comparer != null);
    }

    // Summary:
    //     Gets the number of key/value pairs contained in the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>.
    //
    // Returns:
    //     The number of key/value pairs contained in the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>.
    //
    // Exceptions:
    //   System.OverflowException:
    //     The dictionary already contains the maximum number of elements, System.Int32.MaxValue.
    //public int Count { get; }

    //
    // Summary:
    //     Gets a value that indicates whether the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     is empty.
    //
    // Returns:
    //     true if the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     is empty; otherwise, false.
    
    //public bool IsEmpty { get; }
    //
    // Summary:
    //     Gets a collection containing the keys in the System.Collections.Generic.Dictionary<TKey,TValue>.
    //
    // Returns:
    //     A System.Collections.Generic.ICollection<T> containing the keys in the System.Collections.Generic.Dictionary<TKey,TValue>.
    //public ICollection<TKey> Keys { get; }
    //
    // Summary:
    //     Gets a collection containing the values in the System.Collections.Generic.Dictionary<TKey,TValue>.
    //
    // Returns:
    //     A System.Collections.Generic.ICollection<T> containing the values in the
    //     System.Collections.Generic.Dictionary<TKey,TValue>
    //public ICollection<TValue> Values { get; }

    // Summary:
    //     Gets or sets the value associated with the specified key.
    //
    // Parameters:
    //   key:
    //     The key of the value to get or set.
    //
    // Returns:
    //     Returns the Value property of the System.Collections.Generic.KeyValuePair<TKey,TValue>
    //     at the specified index.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is a null reference (Nothing in Visual Basic).
    //
    //   System.Collections.Generic.KeyNotFoundException:
    //     The property is retrieved and key does not exist in the collection.
    //public TValue this[TKey key] { get; set; }

    // Summary:
    //     Adds a key/value pair to the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     if the key does not already exist, or updates a key/value pair in the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     if the key already exists.
    //
    // Parameters:
    //   key:
    //     The key to be added or whose value should be updated
    //
    //   addValueFactory:
    //     The function used to generate a value for an absent key
    //
    //   updateValueFactory:
    //     The function used to generate a new value for an existing key based on the
    //     key's existing value
    //
    // Returns:
    //     The new value for the key. This will be either be the result of addValueFactory
    //     (if the key was absent) or the result of updateValueFactory (if the key was
    //     present).
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is a null reference (Nothing in Visual Basic).-or-addValueFactory is
    //     a null reference (Nothing in Visual Basic).-or-updateValueFactory is a null
    //     reference (Nothing in Visual Basic).
    //
    //   System.OverflowException:
    //     The dictionary already contains the maximum number of elements, System.Int32.MaxValue.
    public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
    {
      return default(TValue);
    }
    //
    // Summary:
    //     Adds a key/value pair to the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     if the key does not already exist, or updates a key/value pair in the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     if the key already exists.
    //
    // Parameters:
    //   key:
    //     The key to be added or whose value should be updated
    //
    //   addValue:
    //     The value to be added for an absent key
    //
    //   updateValueFactory:
    //     The function used to generate a new value for an existing key based on the
    //     key's existing value
    //
    // Returns:
    //     The new value for the key. This will be either be addValue (if the key was
    //     absent) or the result of updateValueFactory (if the key was present).
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is a null reference (Nothing in Visual Basic).-or-updateValueFactory
    //     is a null reference (Nothing in Visual Basic).
    //
    //   System.OverflowException:
    //     The dictionary already contains the maximum number of elements, System.Int32.MaxValue.
    public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
    {
      Contract.Ensures(object.Equals(Contract.Result<TValue>(), addValue));

      return default(TValue);
    }
    //
    // Summary:
    //     Removes all keys and values from the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>.
    //public void Clear();
    //
    // Summary:
    //     Determines whether the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     contains the specified key.
    //
    // Parameters:
    //   key:
    //     The key to locate in the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>.
    //
    // Returns:
    //     true if the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     contains an element with the specified key; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is a null reference (Nothing in Visual Basic).
    //public bool ContainsKey(TKey key);
    //
    // Summary:
    //     Returns an enumerator that iterates through the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>.
    //
    // Returns:
    //     An enumerator for the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>.
    //public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator();
    //
    // Summary:
    //     Adds a key/value pair to the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     if the key does not already exist.
    //
    // Parameters:
    //   key:
    //     The key of the element to add.
    //
    //   valueFactory:
    //     The function used to generate a value for the key
    //
    // Returns:
    //     The value for the key. This will be either the existing value for the key
    //     if the key is already in the dictionary, or the new value for the key as
    //     returned by valueFactory if the key was not in the dictionary.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is a null reference (Nothing in Visual Basic).-or-valueFactory is a null
    //     reference (Nothing in Visual Basic).
    //
    //   System.OverflowException:
    //     The dictionary already contains the maximum number of elements, System.Int32.MaxValue.
    public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
    {
      Contract.Requires(key != null);

      return default(TValue);

    }
    //
    // Summary:
    //     Adds a key/value pair to the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     if the key does not already exist.
    //
    // Parameters:
    //   key:
    //     The key of the element to add.
    //
    //   value:
    //     the value to be added, if the key does not already exist
    //
    // Returns:
    //     The value for the key. This will be either the existing value for the key
    //     if the key is already in the dictionary, or the new value if the key was
    //     not in the dictionary.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is a null reference (Nothing in Visual Basic).
    //
    //   System.OverflowException:
    //     The dictionary already contains the maximum number of elements, System.Int32.MaxValue.
    public TValue GetOrAdd(TKey key, TValue value)
    {
      Contract.Requires(key != null);
      Contract.Ensures(object.Equals(Contract.Result<TValue>(), value));

      return default(TValue);

    }
    //
    // Summary:
    //     Copies the key and value pairs stored in the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     to a new array.
    //
    // Returns:
    //     A new array containing a snapshot of key and value pairs copied from the
    //     System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>.
    //public KeyValuePair<TKey, TValue>[] ToArray();
    //
    // Summary:
    //     Attempts to add the specified key and value to the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>.
    //
    // Parameters:
    //   key:
    //     The key of the element to add.
    //
    //   value:
    //     The value of the element to add. The value can be a null reference (Nothing
    //     in Visual Basic) for reference types.
    //
    // Returns:
    //     true if the key/value pair was added to the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     successfully. If the key already exists, this method returns false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is null reference (Nothing in Visual Basic).
    //
    //   System.OverflowException:
    //     The dictionary already contains the maximum number of elements, System.Int32.MaxValue.
    public bool TryAdd(TKey key, TValue value)
    {
      Contract.Requires(key != null);

      return false;
    }
    //
    // Summary:
    //     Attempts to get the value associated with the specified key from the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>.
    //
    // Parameters:
    //   key:
    //     The key of the value to get.
    //
    //   value:
    //     When this method returns, value contains the object from the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     with the specified key or the default value of , if the operation failed.
    //
    // Returns:
    //     true if the key was found in the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>;
    //     otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is a null reference (Nothing in Visual Basic).
//    public bool TryGetValue(TKey key, out TValue value);
    //
    // Summary:
    //     Attempts to remove and return the value with the specified key from the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>.
    //
    // Parameters:
    //   key:
    //     The key of the element to remove and return.
    //
    //   value:
    //     When this method returns, value contains the object removed from the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
    //     or the default value of if the operation failed.
    //
    // Returns:
    //     true if an object was removed successfully; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is a null reference (Nothing in Visual Basic).
  //  public bool TryRemove(TKey key, out TValue value);
    //
    // Summary:
    //     Compares the existing value for the specified key with a specified value,
    //     and if they are equal, updates the key with a third value.
    //
    // Parameters:
    //   key:
    //     The key whose value is compared with comparisonValue and possibly replaced.
    //
    //   newValue:
    //     The value that replaces the value of the element with key if the comparison
    //     results in equality.
    //
    //   comparisonValue:
    //     The value that is compared to the value of the element with key.
    //
    // Returns:
    //     true if the value with key was equal to comparisonValue and replaced with
    //     newValue; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is a null reference.
  //  public bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue);
  }
}
#endif
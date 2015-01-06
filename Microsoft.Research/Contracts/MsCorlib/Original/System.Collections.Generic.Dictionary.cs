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
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

using Microsoft.Contracts;

namespace System.Collections.Generic {
  // Summary:
  //     Represents a collection of keys and values.
  public class Dictionary<TKey, TValue> {
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.Dictionary<TKey,TValue>
    //     class that is empty, has the default initial capacity, and uses the default
    //     equality comparer for the key type.
    public Dictionary()
      CodeContract.Ensures(Count == 0);
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.Dictionary<TKey,TValue>
    //     class that contains elements copied from the specified System.Collections.Generic.IDictionary<TKey,TValue>
    //     and uses the default equality comparer for the key type.
    //
    // Parameters:
    //   dictionary:
    //     The System.Collections.Generic.IDictionary<TKey,TValue> whose elements are
    //     copied to the new System.Collections.Generic.Dictionary<TKey,TValue>.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     dictionary contains one or more duplicate keys.
    //
    //   System.ArgumentNullException:
    //     dictionary is null.
    public Dictionary(IDictionary<TKey, TValue> dictionary) {
      CodeContract.Ensures(Count == dictionary.Count);
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.Dictionary<TKey,TValue>
    //     class that is empty, has the default initial capacity, and uses the specified
    //     System.Collections.Generic.IEqualityComparer<T>.
    //
    // Parameters:
    //   comparer:
    //     The System.Collections.Generic.IEqualityComparer<T> implementation to use
    //     when comparing keys, or null to use the default System.Collections.Generic.EqualityComparer<T>
    //     for the type of the key.
      return default(Dictionary(IDictionary<TKey,);
    }
    public Dictionary(IEqualityComparer<TKey> comparer) {
      CodeContract.Ensures(Count == 0);
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.Dictionary<TKey,TValue>
    //     class that is empty, has the specified initial capacity, and uses the default
    //     equality comparer for the key type.
    //
    // Parameters:
    //   capacity:
    //     The initial number of elements that the System.Collections.Generic.Dictionary<TKey,TValue>
    //     can contain.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     capacity is less than 0.
      return default(Dictionary(IEqualityComparer<TKey>);
    }
    public Dictionary(int capacity) {
      CodeContract.Ensures(Count == 0);
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.Dictionary<TKey,TValue>
    //     class that contains elements copied from the specified System.Collections.Generic.IDictionary<TKey,TValue>
    //     and uses the specified System.Collections.Generic.IEqualityComparer<T>.
    //
    // Parameters:
    //   dictionary:
    //     The System.Collections.Generic.IDictionary<TKey,TValue> whose elements are
    //     copied to the new System.Collections.Generic.Dictionary<TKey,TValue>.
    //
    //   comparer:
    //     The System.Collections.Generic.IEqualityComparer<T> implementation to use
    //     when comparing keys, or null to use the default System.Collections.Generic.EqualityComparer<T>
    //     for the type of the key.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     dictionary contains one or more duplicate keys.
    //
    //   System.ArgumentNullException:
    //     dictionary is null.
      return default(Dictionary(int);
    }
    public Dictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) {
      CodeContract.Ensures(Count == dictionary.Count);
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.Dictionary<TKey,TValue>
    //     class that is empty, has the specified initial capacity, and uses the specified
    //     System.Collections.Generic.IEqualityComparer<T>.
    //
    // Parameters:
    //   capacity:
    //     The initial number of elements that the System.Collections.Generic.Dictionary<TKey,TValue>
    //     can contain.
    //
    //   comparer:
    //     The System.Collections.Generic.IEqualityComparer<T> implementation to use
    //     when comparing keys, or null to use the default System.Collections.Generic.EqualityComparer<T>
    //     for the type of the key.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     capacity is less than 0.
      return default(Dictionary(IDictionary<TKey,);
    }
    public Dictionary(int capacity, IEqualityComparer<TKey> comparer) {
      CodeContract.Ensures(Count == 0);
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.Dictionary<TKey,TValue>
    //     class with serialized data.
    //
    // Parameters:
    //   context:
    //     A System.Runtime.Serialization.StreamingContext structure containing the
    //     source and destination of the serialized stream associated with the System.Collections.Generic.Dictionary<TKey,TValue>.
    //
    //   info:
    //     A System.Runtime.Serialization.SerializationInfo object containing the information
    //     required to serialize the System.Collections.Generic.Dictionary<TKey,TValue>.
    protected Dictionary(SerializationInfo info, StreamingContext context);

    // Summary:
    //     Gets the System.Collections.Generic.IEqualityComparer<T> that is used to
    //     determine equality of keys for the dictionary.
    //
    // Returns:
    //     The System.Collections.Generic.IEqualityComparer<T> generic interface implementation
    //     that is used to determine equality of keys for the current System.Collections.Generic.Dictionary<TKey,TValue>
    //     and to provide hash values for the keys.
      return default(Dictionary(int);
    }
    public IEqualityComparer<TKey> Comparer { get; }
    //
    // Summary:
    //     Gets the number of key/value pairs contained in the System.Collections.Generic.Dictionary<TKey,TValue>.
    //
    // Returns:
    //     The number of key/value pairs contained in the System.Collections.Generic.Dictionary<TKey,TValue>.
    public int Count { get; }
    //
    // Summary:
    //     Gets a collection containing the keys in the System.Collections.Generic.Dictionary<TKey,TValue>.
    //
    // Returns:
    //     A System.Collections.Generic.Dictionary<TKey,TValue>.KeyCollection containing
    //     the keys in the System.Collections.Generic.Dictionary<TKey,TValue>.
    public Dictionary<TKey, TValue>.KeyCollection! Keys { [ElementCollection]  get; }
    //
    // Summary:
    //     Gets a collection containing the values in the System.Collections.Generic.Dictionary<TKey,TValue>.
    //
    // Returns:
    //     A System.Collections.Generic.Dictionary<TKey,TValue>.ValueCollection containing
    //     the values in the System.Collections.Generic.Dictionary<TKey,TValue>.
    public Dictionary<TKey, TValue>.ValueCollection! Values { [ElementCollection] get; }

    // Summary:
    //     Gets or sets the value associated with the specified key.
    //
    // Parameters:
    //   key:
    //     The key of the value to get or set.
    //
    // Returns:
    //     The value associated with the specified key. If the specified key is not
    //     found, a get operation throws a System.Collections.Generic.KeyNotFoundException,
    //     and a set operation creates a new element with the specified key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is null.
    //
    //   System.Collections.Generic.KeyNotFoundException:
    //     The property is retrieved and key does not exist in the collection.
    public TValue this[TKey key]
    {
      [ResultNotNewlyAllocated]
      get;
        CodeContract.Requires(ContainsKey(key));
      set;
        modifies this.*;
        CodeContract.Ensures(ContainsKey(key));
        CodeContract.Ensures(old(ContainsKey(key)) ==> Count == old(Count));
        CodeContract.Ensures(!old(ContainsKey(key)) ==> Count == old(Count) + 1);
    }

    // Summary:
    //     Adds the specified key and value to the dictionary.
    //
    // Parameters:
    //   value:
    //     The value of the element to add. The value can be null for reference types.
    //
    //   key:
    //     The key of the element to add.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An element with the same key already exists in the System.Collections.Generic.Dictionary<TKey,TValue>.
    //
    //   System.ArgumentNullException:
    //     key is null.
    [WriteConfined]
    public void Add(TKey key, TValue value) {
      CodeContract.Requires(!ContainsKey(key));
      modifies this.*;
      CodeContract.Ensures(ContainsKey(key));
      CodeContract.Ensures(Count == old(Count) + 1);
    //
    // Summary:
    //     Removes all keys and values from the System.Collections.Generic.Dictionary<TKey,TValue>.
    }
    [WriteConfined]
    public void Clear() {
      modifies this.*;
      CodeContract.Ensures(Count == 0);
    //
    // Summary:
    //     Determines whether the System.Collections.Generic.Dictionary<TKey,TValue>
    //     contains the specified key.
    //
    // Parameters:
    //   key:
    //     The key to locate in the System.Collections.Generic.Dictionary<TKey,TValue>.
    //
    // Returns:
    //     true if the System.Collections.Generic.Dictionary<TKey,TValue> contains an
    //     element with the specified key; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is null.
    }
    [Pure][Reads(ReadsAttribute.Reads.Owned)]
    public bool ContainsKey(TKey key) {
      CodeContract.Ensures(result ==> Count > 0);
    //
    // Summary:
    //     Determines whether the System.Collections.Generic.Dictionary<TKey,TValue>
    //     contains a specific value.
    //
    // Parameters:
    //   value:
    //     The value to locate in the System.Collections.Generic.Dictionary<TKey,TValue>.
    //     The value can be null for reference types.
    //
    // Returns:
    //     true if the System.Collections.Generic.Dictionary<TKey,TValue> contains an
    //     element with the specified value; otherwise, false.
      return default(bool);
    }
    [Pure][Reads(ReadsAttribute.Reads.Owned)]
    public bool ContainsValue(TValue value) {
      CodeContract.Ensures(result ==> Count > 0);
    //
    // Summary:
    //     Returns an enumerator that iterates through the System.Collections.Generic.Dictionary<TKey,TValue>.
    //
    // Returns:
    //     A System.Collections.Generic.Dictionary<TKey,TValue>.Enumerator structure
    //     for the System.Collections.Generic.Dictionary<TKey,TValue>.
      return default(bool);
    }
    [Pure] [GlobalAccess(false)] [Escapes(true,false)]
    public Dictionary<TKey, TValue>.Enumerator GetEnumerator() {
    //
    // Summary:
    //     Implements the System.Runtime.Serialization.ISerializable interface and returns
    //     the data needed to serialize the System.Collections.Generic.Dictionary<TKey,TValue>
    //     instance.
    //
    // Parameters:
    //   context:
    //     A System.Runtime.Serialization.StreamingContext structure that contains the
    //     source and destination of the serialized stream associated with the System.Collections.Generic.Dictionary<TKey,TValue>
    //     instance.
    //
    //   info:
    //     A System.Runtime.Serialization.SerializationInfo object that contains the
    //     information required to serialize the System.Collections.Generic.Dictionary<TKey,TValue>
    //     instance.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     info is null.
      return default(Dictionary<TKey,);
    }
    [Pure][Reads(ReadsAttribute.Reads.Owned)]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
    //
    // Summary:
    //     Implements the System.Runtime.Serialization.ISerializable interface and raises
    //     the deserialization event when the deserialization is complete.
    //
    // Parameters:
    //   sender:
    //     The source of the deserialization event.
    //
    // Exceptions:
    //   System.Runtime.Serialization.SerializationException:
    //     The System.Runtime.Serialization.SerializationInfo object associated with
    //     the current System.Collections.Generic.Dictionary<TKey,TValue> instance is
    //     invalid.
      return default(virtual);
    }
    public virtual void OnDeserialization(object sender) {
    //
    // Summary:
    //     Removes the value with the specified key from the System.Collections.Generic.Dictionary<TKey,TValue>.
    //
    // Parameters:
    //   key:
    //     The key of the element to remove.
    //
    // Returns:
    //     true if the element is successfully found and removed; otherwise, false.
    //      This method returns false if key is not found in the System.Collections.Generic.Dictionary<TKey,TValue>.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is null.
      return default(virtual);
    }
    [WriteConfined]
    public bool Remove(TKey key) {
      modifies this.*;
      CodeContract.Ensures(result ==> old(ContainsKey(key)) && !ContainsKey(key));
      CodeContract.Ensures(result ==> Count == old(Count) - 1);
      CodeContract.Ensures(!result ==> Count == old(Count));

      return default(bool);
    }
    //[Pure][Reads(ReadsAttribute.Reads.Owned)]
    public bool TryGetValue(TKey key, out TValue? value) {
      CodeContract.Ensures(result == ContainsKey(key));
      CodeContract.Ensures(Count == old(Count));

    // Summary:
    //     Represents the collection of keys in a System.Collections.Generic.Dictionary<TKey,TValue>.
    //     This class cannot be inherited.
    [Serializable]
      return default(bool);
    }
    [DebuggerDisplay("Count = {Count}")]
    public sealed class KeyCollection {
      // Summary:
      //     Initializes a new instance of the System.Collections.Generic.Dictionary<TKey,TValue>.KeyCollection
      //     class that reflects the keys in the specified System.Collections.Generic.Dictionary<TKey,TValue>.
      //
      // Parameters:
      //   dictionary:
      //     The System.Collections.Generic.Dictionary<TKey,TValue> whose keys are reflected
      //     in the new System.Collections.Generic.Dictionary<TKey,TValue>.KeyCollection.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     dictionary is null.
      public KeyCollection(Dictionary<TKey, TValue> dictionary) {

      // Summary:
      //     Gets the number of elements contained in the System.Collections.Generic.Dictionary<TKey,TValue>.KeyCollection.
      //
      // Returns:
      //     The number of elements contained in the System.Collections.Generic.Dictionary<TKey,TValue>.KeyCollection.Retrieving
      //     the value of this property is an O(1) operation.
        return default(KeyCollection(Dictionary<TKey,);
      }
      public int Count { get; }

      public void CopyTo(TKey[] array, int index) {
        CodeContract.Requires(0 <= index);
        CodeContract.Requires(index + Count <= array.Length);
        modifies array[*];
      //
      // Summary:
      //     Returns an enumerator that iterates through the System.Collections.Generic.Dictionary<TKey,TValue>.KeyCollection.
      //
      // Returns:
      //     A System.Collections.Generic.Dictionary<TKey,TValue>.KeyCollection.Enumerator
      //     for the System.Collections.Generic.Dictionary<TKey,TValue>.KeyCollection.
      }
      [Pure] [GlobalAccess(false)] [Escapes(true,false)]
      public Dictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator() {

      // Summary:
      //     Enumerates the elements of a System.Collections.Generic.Dictionary<TKey,TValue>.KeyCollection.
        return default(Dictionary<TKey,);
      }
      [Serializable]
      public struct Enumerator {

        // Summary:
        //     Gets the element at the current position of the enumerator.
        //
        // Returns:
        //     The element in the System.Collections.Generic.Dictionary<TKey,TValue>.KeyCollection
        //     at the current position of the enumerator.
        public TKey Current { get; }

        // Summary:
        //     Releases all resources used by the System.Collections.Generic.Dictionary<TKey,TValue>.KeyCollection.Enumerator.
        public void Dispose() {
        //
        // Summary:
        //     Advances the enumerator to the next element of the System.Collections.Generic.Dictionary<TKey,TValue>.KeyCollection.
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

    // Summary:
    //     Represents the collection of values in a System.Collections.Generic.Dictionary<TKey,TValue>.
    //     This class cannot be inherited.
    [Serializable]
    [DebuggerDisplay("Count = {Count}")]
    public sealed class ValueCollection {
      // Summary:
      //     Initializes a new instance of the System.Collections.Generic.Dictionary<TKey,TValue>.ValueCollection
      //     class that reflects the values in the specified System.Collections.Generic.Dictionary<TKey,TValue>.
      //
      // Parameters:
      //   dictionary:
      //     The System.Collections.Generic.Dictionary<TKey,TValue> whose values are reflected
      //     in the new System.Collections.Generic.Dictionary<TKey,TValue>.ValueCollection.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     dictionary is null.
      public ValueCollection(Dictionary<TKey, TValue> dictionary) {

      // Summary:
      //     Gets the number of elements contained in the System.Collections.Generic.Dictionary<TKey,TValue>.ValueCollection.
      //
      // Returns:
      //     The number of elements contained in the System.Collections.Generic.Dictionary<TKey,TValue>.ValueCollection.
        return default(ValueCollection(Dictionary<TKey,);
      }
      public int Count { get) { CodeContract.Ensures(0 <= result) { }

        return default(int);
      }
      public void CopyTo(TValue[] array, int index) {
        CodeContract.Requires(0 <= index);
        CodeContract.Requires(index + Count <= array.Length);
        modifies array[*];
      //
      // Summary:
      //     Returns an enumerator that iterates through the System.Collections.Generic.Dictionary<TKey,TValue>.ValueCollection.
      //
      // Returns:
      //     A System.Collections.Generic.Dictionary<TKey,TValue>.ValueCollection.Enumerator
      //     for the System.Collections.Generic.Dictionary<TKey,TValue>.ValueCollection.
      }
      [Pure] [GlobalAccess(false)] [Escapes(true,false)]
      public Dictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator() {

      // Summary:
      //     Enumerates the elements of a System.Collections.Generic.Dictionary<TKey,TValue>.ValueCollection.
        return default(Dictionary<TKey,);
      }
      [Serializable]
      public struct Enumerator {

        // Summary:
        //     Gets the element at the current position of the enumerator.
        //
        // Returns:
        //     The element in the System.Collections.Generic.Dictionary<TKey,TValue>.ValueCollection
        //     at the current position of the enumerator.
        public TValue Current { get; }

        // Summary:
        //     Releases all resources used by the System.Collections.Generic.Dictionary<TKey,TValue>.ValueCollection.Enumerator.
        public void Dispose() {
        //
        // Summary:
        //     Advances the enumerator to the next element of the System.Collections.Generic.Dictionary<TKey,TValue>.ValueCollection.
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

    // Summary:
    //     Enumerates the elements of a System.Collections.Generic.Dictionary<TKey,TValue>.
    [Serializable]
    public struct Enumerator  {

      // Summary:
      //     Gets the element at the current position of the enumerator.
      //
      // Returns:
      //     The element in the System.Collections.Generic.Dictionary<TKey,TValue> at
      //     the current position of the enumerator.
      public KeyValuePair<TKey, TValue> Current { get; }

      // Summary:
      //     Releases all resources used by the System.Collections.Generic.Dictionary<TKey,TValue>.Enumerator.
      public void Dispose() {
      //
      // Summary:
      //     Advances the enumerator to the next element of the System.Collections.Generic.Dictionary<TKey,TValue>.
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

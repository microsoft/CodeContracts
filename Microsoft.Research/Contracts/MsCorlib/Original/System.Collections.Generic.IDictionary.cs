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
  //     Represents a generic collection of key/value pairs.
  public interface IDictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable {
    // Summary:
    //     Gets an System.Collections.Generic.ICollection<T> containing the keys of
    //     the System.Collections.Generic.IDictionary<TKey,TValue>.
    //
    // Returns:
    //     An System.Collections.Generic.ICollection<T> containing the keys of the object
    //     that implements System.Collections.Generic.IDictionary<TKey,TValue>.
    ICollection<TKey>! Keys { get; }
    //
    // Summary:
    //     Gets an System.Collections.Generic.ICollection<T> containing the values in
    //     the System.Collections.Generic.IDictionary<TKey,TValue>.
    //
    // Returns:
    //     An System.Collections.Generic.ICollection<T> containing the values in the
    //     object that implements System.Collections.Generic.IDictionary<TKey,TValue>.
    ICollection<TValue>! Values { get; }

    // Summary:
    //     Gets or sets the element with the specified key.
    //
    // Parameters:
    //   key:
    //     The key of the element to get or set.
    //
    // Returns:
    //     The element with the specified key.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The property is set and the System.Collections.Generic.IDictionary<TKey,TValue>
    //     is read-only.
    //
    //   System.ArgumentNullException:
    //     key is null.
    //
    //   System.Collections.Generic.KeyNotFoundException:
    //     The property is retrieved and key is not found.
    TValue this[TKey key] {
      [ResultNotNewlyAllocated]
      get;
        CodeContract.Requires(ContainsKey(key));
      set;
        modifies this.*;
        CodeContract.Ensures(ContainsKey(key));
    }

    // Summary:
    //     Adds an element with the provided key and value to the System.Collections.Generic.IDictionary<TKey,TValue>.
    //
    // Parameters:
    //   value:
    //     The object to use as the value of the element to add.
    //
    //   key:
    //     The object to use as the key of the element to add.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The System.Collections.Generic.IDictionary<TKey,TValue> is read-only.
    //
    //   System.ArgumentException:
    //     An element with the same key already exists in the System.Collections.Generic.IDictionary<TKey,TValue>.
    //
    //   System.ArgumentNullException:
    //     key is null.
    [WriteConfined]
    void Add(TKey key, TValue value);
      CodeContract.Requires(!ContainsKey(key));
      modifies this.*;
      CodeContract.Ensures(ContainsKey(key));
    //
    // Summary:
    //     Determines whether the System.Collections.Generic.IDictionary<TKey,TValue>
    //     contains an element with the specified key.
    //
    // Parameters:
    //   key:
    //     The key to locate in the System.Collections.Generic.IDictionary<TKey,TValue>.
    //
    // Returns:
    //     true if the System.Collections.Generic.IDictionary<TKey,TValue> contains
    //     an element with the key; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is null.
    [Pure][Reads(ReadsAttribute.Reads.Owned)]
    bool ContainsKey(TKey key);
    //
    // Summary:
    //     Removes the element with the specified key from the System.Collections.Generic.IDictionary<TKey,TValue>.
    //
    // Parameters:
    //   key:
    //     The key of the element to remove.
    //
    // Returns:
    //     true if the element is successfully removed; otherwise, false.  This method
    //     also returns false if key was not found in the original System.Collections.Generic.IDictionary<TKey,TValue>.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The System.Collections.Generic.IDictionary<TKey,TValue> is read-only.
    //
    //   System.ArgumentNullException:
    //     key is null.
    [WriteConfined]
    bool Remove(TKey key);
      modifies this.*;
      CodeContract.Ensures(result ==> old(ContainsKey(key)) && !ContainsKey(key));
    
    //[Pure][Reads(ReadsAttribute.Reads.Owned)]
    bool TryGetValue(TKey key, out TValue? value);
      CodeContract.Ensures(result == ContainsKey(key));
  }
}

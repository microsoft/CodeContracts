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


namespace System.Collections.Generic
{
  // Summary:
  //     Represents a generic collection of key/value pairs.
  [ContractClass(typeof(IDictionaryContract<,>))]
  public interface IDictionary<TKey, TValue> : ICollection<KeyValuePair<TKey,TValue>>
  {
    // Summary:
    //     Gets an System.Collections.Generic.ICollection<T> containing the keys of
    //     the System.Collections.Generic.IDictionary<TKey,TValue>.
    //
    // Returns:
    //     An System.Collections.Generic.ICollection<T> containing the keys of the object
    //     that implements System.Collections.Generic.IDictionary<TKey,TValue>.
    ICollection<TKey> Keys
    {
      get;
    }
    //
    // Summary:
    //     Gets an System.Collections.Generic.ICollection<T> containing the values in
    //     the System.Collections.Generic.IDictionary<TKey,TValue>.
    //
    // Returns:
    //     An System.Collections.Generic.ICollection<T> containing the values in the
    //     object that implements System.Collections.Generic.IDictionary<TKey,TValue>.
    ICollection<TValue> Values
    {
      get;
    }

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
    TValue this[TKey key]
    {
      get;
      set;
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
    void Add(TKey key, TValue value);
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
    [Pure]
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
    bool Remove(TKey key);

    [Pure]
    bool TryGetValue(TKey key, out TValue value);

  }

  [ContractClassFor(typeof(IDictionary<,>))]
  abstract class IDictionaryContract<TKey, TValue> : IDictionary<TKey,TValue>
  {
    #region IDictionary<TKey,TValue> Members

    ICollection<TKey> IDictionary<TKey, TValue>.Keys
    {
      get
      {
        Contract.Ensures(Contract.Result<ICollection<TKey>>() != null);
        throw new NotImplementedException();
      }
    }

    ICollection<TValue> IDictionary<TKey, TValue>.Values
    {
      get
      {
        Contract.Ensures(Contract.Result<ICollection<TValue>>() != null);
        throw new NotImplementedException();
      }
    }

    TValue IDictionary<TKey, TValue>.this[TKey key]
    {
      get
      {
        Contract.Requires(!ReferenceEquals(key, null));
        // Contract.Requires(ContainsKey(key)); ==> can't require, it's impossible to get rid of "requires unproven: ContainsKey(key)"
        throw new NotImplementedException();
      }
      set
      {
        Contract.Requires(!ReferenceEquals(key, null));
        //Contract.Ensures(ContainsKey(key)); 
        //Contract.Ensures(old(ContainsKey(key)) ==> Count == old(Count));
        //Contract.Ensures(!old(ContainsKey(key)) ==> Count == old(Count) + 1);
        throw new NotImplementedException();
      }
    }

    void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
    {
      Contract.Requires(!ReferenceEquals(key, null));
      //  - correct, but probably too anoying to proof always. If you can't assume that key does not exist, you would call "this[key] = value".
      // Contract.Requires(!ContainsKey(key));
      //modifies this.*;
      // Contract.Ensures(ContainsKey(key)); ==> is of no use, CC would not use this.
    }

    public bool ContainsKey(TKey key)
    {
      Contract.Requires(!ReferenceEquals(key, null));
      Contract.Ensures(!Contract.Result<bool>() || (Count > 0));

      throw new NotImplementedException();
    }

    bool IDictionary<TKey, TValue>.Remove(TKey key)
    {
      Contract.Requires(!ReferenceEquals(key, null));
      // Contract.Ensures(!Contract.Result<bool>() || Contract.OldValue(ContainsKey(key)) && !ContainsKey(key));  ==> is of no use, CC would not use this.
      throw new NotImplementedException();
    }

    bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
    {
      Contract.Requires(!ReferenceEquals(key, null));
      Contract.Ensures(Contract.Result<bool>() == ContainsKey(key));
      throw new NotImplementedException();
    }

    #endregion

    #region ICollection<KeyValuePair<TKey,TValue>> Members

    public int Count
    {
      get { throw new NotImplementedException(); }
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
    {
      get { throw new NotImplementedException(); }
    }

    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
    {
      throw new NotImplementedException();
    }

    void ICollection<KeyValuePair<TKey, TValue>>.Clear()
    {
      throw new NotImplementedException();
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
    {
      throw new NotImplementedException();
    }

    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable<KeyValuePair<TKey,TValue>> Members

    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable Members

    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable Members

    public object[] Model {
      get { throw new NotImplementedException(); }
    }

    #endregion
  }
}

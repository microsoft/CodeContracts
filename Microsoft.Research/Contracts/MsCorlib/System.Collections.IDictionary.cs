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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Collections
{
  [ContractClass(typeof(CDictionary))]
  public interface IDictionary : ICollection, IEnumerable
  {
    // Summary:
    //     Gets a value indicating whether the System.Collections.IDictionary object
    //     has a fixed size.
    //
    // Returns:
    //     true if the System.Collections.IDictionary object has a fixed size; otherwise,
    //     false.
    bool IsFixedSize { get; }
    //
    // Summary:
    //     Gets a value indicating whether the System.Collections.IDictionary object
    //     is read-only.
    //
    // Returns:
    //     true if the System.Collections.IDictionary object is read-only; otherwise,
    //     false.
    bool IsReadOnly { get; }
    //
    // Summary:
    //     Gets an System.Collections.ICollection object containing the keys of the
    //     System.Collections.IDictionary object.
    //
    // Returns:
    //     An System.Collections.ICollection object containing the keys of the System.Collections.IDictionary
    //     object.
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    ICollection Keys
    {
      get;
    }

    //
    // Summary:
    //     Gets an System.Collections.ICollection object containing the values in the
    //     System.Collections.IDictionary object.
    //
    // Returns:
    //     An System.Collections.ICollection object containing the values in the System.Collections.IDictionary
    //     object.
    [Pure]
    [Reads(ReadsAttribute.Reads.Owned)]
    ICollection Values
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
    //     The property is set and the System.Collections.IDictionary object is read-only.-or-
    //     The property is set, key does not exist in the collection, and the System.Collections.IDictionary
    //     has a fixed size.
    //
    //   System.ArgumentNullException:
    //     key is null.
    object this[object key]
    {
      get;
      set;
    }

    // Summary:
    //     Adds an element with the provided key and value to the System.Collections.IDictionary
    //     object.
    //
    // Parameters:
    //   value:
    //     The System.Object to use as the value of the element to add.
    //
    //   key:
    //     The System.Object to use as the key of the element to add.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An element with the same key already exists in the System.Collections.IDictionary
    //     object.
    //
    //   System.ArgumentNullException:
    //     key is null.
    //
    //   System.NotSupportedException:
    //     The System.Collections.IDictionary is read-only.-or- The System.Collections.IDictionary
    //     has a fixed size.
    [WriteConfined]
    void Add(object key, object value);
    //
    // Summary:
    //     Removes all elements from the System.Collections.IDictionary object.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The System.Collections.IDictionary object is read-only.
    [WriteConfined]
    void Clear();
    //
    // Summary:
    //     Determines whether the System.Collections.IDictionary object contains an
    //     element with the specified key.
    //
    // Parameters:
    //   key:
    //     The key to locate in the System.Collections.IDictionary object.
    //
    // Returns:
    //     true if the System.Collections.IDictionary contains an element with the key;
    //     otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is null.
    [Pure]
    bool Contains(object key);
    //
    // Summary:
    //     Returns an System.Collections.IDictionaryEnumerator object for the System.Collections.IDictionary
    //     object.
    //
    // Returns:
    //     An System.Collections.IDictionaryEnumerator object for the System.Collections.IDictionary
    //     object.

    [Pure]
    [GlobalAccess(false)]
    [Escapes(true, false)]
    [return: Fresh]
    new IDictionaryEnumerator GetEnumerator();
    //
    // Summary:
    //     Removes the element with the specified key from the System.Collections.IDictionary
    //     object.
    //
    // Parameters:
    //   key:
    //     The key of the element to remove.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The System.Collections.IDictionary object is read-only.-or- The System.Collections.IDictionary
    //     has a fixed size.
    //
    //   System.ArgumentNullException:
    //     key is null.
    [WriteConfined]
    void Remove(object key);
  }

  [ContractClassFor(typeof(IDictionary))]
  abstract class CDictionary : IDictionary
  {
    #region IDictionary Members

    bool IDictionary.IsFixedSize
    {
      get { throw new global::System.NotImplementedException(); }
    }

    bool IDictionary.IsReadOnly
    {
      get { throw new global::System.NotImplementedException(); }
    }

    ICollection IDictionary.Keys
    {
      get
      {
        Contract.Ensures(Contract.Result<ICollection>() != null);
        throw new global::System.NotImplementedException();
      }
    }

    ICollection IDictionary.Values
    {
      get {
        Contract.Ensures(Contract.Result<ICollection>() != null);

        throw new global::System.NotImplementedException();
      }
    }

    object IDictionary.this[object key]
    {
      get
      {
        Contract.Requires(key != null);
        throw new global::System.NotImplementedException();
      }
      set
      {
        Contract.Requires(key != null);
        throw new global::System.NotImplementedException();
      }
    }

    void IDictionary.Add(object key, object value)
    {
      Contract.Requires(key != null);
      throw new global::System.NotImplementedException();
    }

    void IDictionary.Clear()
    {
      throw new global::System.NotImplementedException();
    }

    [Pure]
    bool IDictionary.Contains(object key)
    {
      Contract.Requires(key != null);
      throw new global::System.NotImplementedException();
    }

    [Pure]
    IDictionaryEnumerator IDictionary.GetEnumerator()
    {
      Contract.Ensures(Contract.Result<IDictionaryEnumerator>() != null);

      throw new global::System.NotImplementedException();
    }

    void IDictionary.Remove(object key)
    {
      Contract.Requires(key != null);

      throw new global::System.NotImplementedException();
    }

    #endregion

    #region ICollection Members

    int ICollection.Count
    {
      [Pure]
      get { throw new global::System.NotImplementedException(); }
    }

    bool ICollection.IsSynchronized
    {
      get { throw new global::System.NotImplementedException(); }
    }

    object ICollection.SyncRoot
    {
      get { throw new global::System.NotImplementedException(); }
    }

    void ICollection.CopyTo(Array array, int index)
    {
      throw new global::System.NotImplementedException();
    }

    #endregion

    #region IEnumerable Members

    [Pure]
    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new global::System.NotImplementedException();
    }

    #endregion

    #region IEnumerable Members

    object[] IEnumerable.Model {
      get { throw new NotImplementedException(); }
    }

    #endregion
  }
}
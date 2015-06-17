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
  //     Provides the abstract base class for a collection of associated System.String
  //     keys and System.Object values that can be accessed either with the key or
  //     with the index.
  public abstract class NameObjectCollectionBase : ICollection //, IEnumerable, ISerializable, IDeserializationCallback
  {
    // Summary:
    //     Initializes a new instance of the System.Collections.Specialized.NameObjectCollectionBase
    //     class that is empty.
    extern protected NameObjectCollectionBase();
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Specialized.NameObjectCollectionBase
    //     class that is empty, has the default initial capacity, and uses the specified
    //     System.Collections.IEqualityComparer object.
    //
    // Parameters:
    //   equalityComparer:
    //     The System.Collections.IEqualityComparer object to use to determine whether
    //     two keys are equal and to generate hash codes for the keys in the collection.
    extern protected NameObjectCollectionBase(IEqualityComparer equalityComparer);
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Specialized.NameObjectCollectionBase
    //     class that is empty, has the specified initial capacity, and uses the default
    //     hash code provider and the default comparer.
    //
    // Parameters:
    //   capacity:
    //     The approximate number of entries that the System.Collections.Specialized.NameObjectCollectionBase
    //     instance can initially contain.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     capacity is less than zero.
    protected NameObjectCollectionBase(int capacity)
    {
      Contract.Requires(capacity >= 0);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Specialized.NameObjectCollectionBase
    //     class that is empty, has the specified initial capacity, and uses the specified
    //     System.Collections.IEqualityComparer object.
    //
    // Parameters:
    //   capacity:
    //     The approximate number of entries that the System.Collections.Specialized.NameObjectCollectionBase
    //     object can initially contain.
    //
    //   equalityComparer:
    //     The System.Collections.IEqualityComparer object to use to determine whether
    //     two keys are equal and to generate hash codes for the keys in the collection.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     capacity is less than zero.
    protected NameObjectCollectionBase(int capacity, IEqualityComparer equalityComparer)
    {
      Contract.Requires(capacity >= 0);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Specialized.NameObjectCollectionBase
    //     class that is serializable and uses the specified System.Runtime.Serialization.SerializationInfo
    //     and System.Runtime.Serialization.StreamingContext.
    //
    // Parameters:
    //   info:
    //     A System.Runtime.Serialization.SerializationInfo object that contains the
    //     information required to serialize the new System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    //
    //   context:
    //     A System.Runtime.Serialization.StreamingContext object that contains the
    //     source and destination of the serialized stream associated with the new System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    extern protected NameObjectCollectionBase(SerializationInfo info, StreamingContext context);

    // Summary:
    //     Gets the number of key/value pairs contained in the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    //
    // Returns:
    //     The number of key/value pairs contained in the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    extern public virtual int Count { get; } // implements ICollection.Count
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Collections.Specialized.NameObjectCollectionBase
    //     instance is read-only.
    //
    // Returns:
    //     true if the System.Collections.Specialized.NameObjectCollectionBase instance
    //     is read-only; otherwise, false.
    extern protected bool IsReadOnly { get; set; }
    //
    // Summary:
    //     Gets a System.Collections.Specialized.NameObjectCollectionBase.KeysCollection
    //     instance that contains all the keys in the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    //
    // Returns:
    //     A System.Collections.Specialized.NameObjectCollectionBase.KeysCollection
    //     instance that contains all the keys in the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    public virtual KeysCollection Keys
    {
      get
      {
        Contract.Ensures(Contract.Result<KeysCollection>() != null);
        return default(KeysCollection);
      }
    }

    // Summary:
    //     Adds an entry with the specified key and value into the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    //
    // Parameters:
    //   name:
    //     The System.String key of the entry to add. The key can be null.
    //
    //   value:
    //     The System.Object value of the entry to add. The value can be null.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    extern protected void BaseAdd(string name, object value);
    //
    // Summary:
    //     Removes all entries from the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    extern protected void BaseClear();
    //
    // Summary:
    //     Gets the value of the entry at the specified index of the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the value to get.
    //
    // Returns:
    //     An System.Object that represents the value of the entry at the specified
    //     index.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is outside the valid range of indexes for the collection.
    [Pure]
    protected object BaseGet(int index)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index < Count);
      return default(object);
    }
    //
    // Summary:
    //     Gets the value of the first entry with the specified key from the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    //
    // Parameters:
    //   name:
    //     The System.String key of the entry to get. The key can be null.
    //
    // Returns:
    //     An System.Object that represents the value of the first entry with the specified
    //     key, if found; otherwise, null.
    [Pure]
    extern protected object BaseGet(string name);
    //
    // Summary:
    //     Returns a System.String array that contains all the keys in the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    //
    // Returns:
    //     A System.String array that contains all the keys in the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    [Pure]
    protected string[] BaseGetAllKeys()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);
      return default(string[]);
    }
    //
    // Summary:
    //     Returns an System.Object array that contains all the values in the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    //
    // Returns:
    //     An System.Object array that contains all the values in the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    [Pure]
    protected object[] BaseGetAllValues()
    {
      Contract.Ensures(Contract.Result<object[]>() != null);
      return default(object[]);
    }
    //
    // Summary:
    //     Returns an array of the specified type that contains all the values in the
    //     System.Collections.Specialized.NameObjectCollectionBase instance.
    //
    // Parameters:
    //   type:
    //     A System.Type that represents the type of array to return.
    //
    // Returns:
    //     An array of the specified type that contains all the values in the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     type is null.
    //
    //   System.ArgumentException:
    //     type is not a valid System.Type.
    [Pure]
    protected object[] BaseGetAllValues(Type type)
    {
      Contract.Ensures(Contract.Result<object[]>() != null);
      return default(object[]);
    }
    //
    // Summary:
    //     Gets the key of the entry at the specified index of the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the key to get.
    //
    // Returns:
    //     A System.String that represents the key of the entry at the specified index.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is outside the valid range of indexes for the collection.
    [Pure]
    protected string BaseGetKey(int index)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index < Count);
      return default(string);
    }
    //
    // Summary:
    //     Gets a value indicating whether the System.Collections.Specialized.NameObjectCollectionBase
    //     instance contains entries whose keys are not null.
    //
    // Returns:
    //     true if the System.Collections.Specialized.NameObjectCollectionBase instance
    //     contains entries whose keys are not null; otherwise, false.
    [Pure]
    extern protected bool BaseHasKeys();
    //
    // Summary:
    //     Removes the entries with the specified key from the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    //
    // Parameters:
    //   name:
    //     The System.String key of the entries to remove. The key can be null.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    extern protected void BaseRemove(string name);
    //
    // Summary:
    //     Removes the entry at the specified index of the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the entry to remove.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is outside the valid range of indexes for the collection.
    //
    //   System.NotSupportedException:
    //     The collection is read-only.
    protected void BaseRemoveAt(int index)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index < Count);
    }
    //
    // Summary:
    //     Sets the value of the entry at the specified index of the System.Collections.Specialized.NameObjectCollectionBase
    //     instance.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the entry to set.
    //
    //   value:
    //     The System.Object that represents the new value of the entry to set. The
    //     value can be null.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    //
    //   System.ArgumentOutOfRangeException:
    //     index is outside the valid range of indexes for the collection.
    protected void BaseSet(int index, object value)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index < Count);
    }
    //
    // Summary:
    //     Sets the value of the first entry with the specified key in the System.Collections.Specialized.NameObjectCollectionBase
    //     instance, if found; otherwise, adds an entry with the specified key and value
    //     into the System.Collections.Specialized.NameObjectCollectionBase instance.
    //
    // Parameters:
    //   name:
    //     The System.String key of the entry to set. The key can be null.
    //
    //   value:
    //     The System.Object that represents the new value of the entry to set. The
    //     value can be null.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The collection is read-only.
    extern protected void BaseSet(string name, object value);

    // Summary:
    //     Represents a collection of the System.String keys of a collection.
    public class KeysCollection
    {
    }

    #region ICollection Members

    void ICollection.CopyTo(Array array, int index)
    {
      throw new NotImplementedException();
    }


    bool ICollection.IsSynchronized
    {
      get { throw new NotImplementedException(); }
    }

    object ICollection.SyncRoot
    {
      get { throw new NotImplementedException(); }
    }

    #endregion

    #region IEnumerable Members

    public virtual IEnumerator GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}

#endif

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
  //     Represents a collection of keys and values.
  public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
  {

    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.Dictionary<TKey,TValue>
    //     class that is empty, has the default initial capacity, and uses the default
    //     equality comparer for the key type.
    public Dictionary()
    {
      Contract.Ensures(Count == 0);
    }
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
    public Dictionary(IDictionary<TKey, TValue> dictionary)
    {
      Contract.Ensures(Count == dictionary.Count);
    }
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

    public Dictionary(IEqualityComparer<TKey> comparer)
    {
      Contract.Ensures(Count == 0);
    }
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

    public Dictionary(int capacity)
    {
      Contract.Ensures(Count == 0);
    }
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

    public Dictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
    {
      Contract.Ensures(Count == dictionary.Count);
    }

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

    public Dictionary(int capacity, IEqualityComparer<TKey> comparer)
    {
      Contract.Ensures(Count == 0);
    }
    //
    // Summary:
    //     Gets the System.Collections.Generic.IEqualityComparer<T> that is used to
    //     determine equality of keys for the dictionary.
    //
    // Returns:
    //     The System.Collections.Generic.IEqualityComparer<T> generic interface implementation
    //     that is used to determine equality of keys for the current System.Collections.Generic.Dictionary<TKey,TValue>
    //     and to provide hash values for the keys.
    public IEqualityComparer<TKey> Comparer
    {
      get
      {
        Contract.Ensures(Contract.Result<IEqualityComparer<TKey>>() != null);
        return default(IEqualityComparer<TKey>);
      }
    }
    //
    // Summary:
    //     Gets the number of key/value pairs contained in the System.Collections.Generic.Dictionary<TKey,TValue>.
    //
    // Returns:
    //     The number of key/value pairs contained in the System.Collections.Generic.Dictionary<TKey,TValue>.
    //
    public virtual int Count
    {
      get
      {
        return default(int);
      }
    }

    [Pure]
    public virtual bool ContainsKey(TKey key) {

      throw new NotImplementedException();
    }

    [Pure]
    public bool ContainsValue(TValue value)
    {
      var @this = (IDictionary<TKey, TValue>)this;
      Contract.Ensures(!Contract.Result<bool>() || @this.Count > 0);

      throw new NotImplementedException();
    }

    //
    // Summary:
    //     Gets a collection containing the keys in the System.Collections.Generic.Dictionary<TKey,TValue>.
    //
    // Returns:
    //     A System.Collections.Generic.Dictionary<TKey,TValue>.KeyCollection containing
    //     the keys in the System.Collections.Generic.Dictionary<TKey,TValue>.
    public Dictionary<TKey, TValue>.KeyCollection Keys
    {
      get
      {
        Contract.Ensures(Contract.Result<Dictionary<TKey, TValue>.KeyCollection>() != null);
        return default(Dictionary<TKey, TValue>.KeyCollection);
      }
    }
    //
    // Summary:
    //     Gets a collection containing the values in the System.Collections.Generic.Dictionary<TKey,TValue>.
    //
    // Returns:
    //     A System.Collections.Generic.Dictionary<TKey,TValue>.ValueCollection containing
    //     the values in the System.Collections.Generic.Dictionary<TKey,TValue>.
    public Dictionary<TKey, TValue>.ValueCollection Values
    {
      get
      {
        Contract.Ensures(Contract.Result<Dictionary<TKey, TValue>.ValueCollection>() != null);
        return default(Dictionary<TKey, TValue>.ValueCollection);
      }
    }

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

#if !SILVERLIGHT
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
    extern public virtual void OnDeserialization(object sender);
#endif

    // Summary:
    //     Represents the collection of keys in a System.Collections.Generic.Dictionary<TKey,TValue>.
    //     This class cannot be inherited.
    public sealed class KeyCollection : IEnumerable<TKey>
    {
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
      public KeyCollection(Dictionary<TKey, TValue> dictionary)
      {
        //return default(KeyCollection(Dictionary<TKey,TValue>)); should this return anything?
      }

      IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
      {
        throw new NotImplementedException();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        throw new NotImplementedException();
      }

      [ContractModel]
      public object[] Model
      {
        get { throw new NotImplementedException(); }
      }

      public Dictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
      {
        // since this is not related to the interfae implementation, we have to repeat the interface implementation post condition.
        Contract.Ensures(Contract.Result<Dictionary<TKey, TValue>.KeyCollection.Enumerator>().Model == this.Model);
        Contract.Ensures(Contract.Result<Dictionary<TKey, TValue>.KeyCollection.Enumerator>().CurrentIndex == -1);
        return default(Dictionary<TKey, TValue>.KeyCollection.Enumerator);
      }

      public struct Enumerator : IEnumerator<TKey>
      {
        public TKey Current
        {
          get {
            Contract.Ensures((object)Contract.Result<TKey>() == this.Model[this.CurrentIndex]);
            throw new NotImplementedException();
          }
        }

        public void Dispose()
        {
          throw new NotImplementedException();
        }

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

        [ContractModel]
        public object[] Model
        {
          get { throw new NotImplementedException(); }
        }

        [ContractModel]
        public int CurrentIndex
        {
          get { throw new NotImplementedException(); }
        }
      }
    }

    // Summary:
    //     Represents the collection of values in a System.Collections.Generic.Dictionary<TKey,TValue>.
    //     This class cannot be inherited.
    public sealed class ValueCollection : ICollection<TValue>
    {
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
      public ValueCollection(Dictionary<TKey, TValue> dictionary)
      {
      }


      IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
      {
        throw new NotImplementedException();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        throw new NotImplementedException();
      }

      public Dictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
      {
        // since this is not related to the interfae implementation, we have to repeat the interface implementation post condition.
        Contract.Ensures(Contract.Result<Dictionary<TKey, TValue>.ValueCollection.Enumerator>().Model == this.Model);
        Contract.Ensures(Contract.Result<Dictionary<TKey, TValue>.ValueCollection.Enumerator>().CurrentIndex == -1);
        return default(Dictionary<TKey, TValue>.ValueCollection.Enumerator);
      }


      [ContractModel]
      public object[] Model
      {
        get { throw new NotImplementedException(); }
      }

      public struct Enumerator : IEnumerator<TValue>
      {
        public TValue Current
        {
          get {
            Contract.Ensures((object)Contract.Result<TValue>() == this.Model[this.CurrentIndex]);
            throw new NotImplementedException();
          }
        }

        public void Dispose()
        {
          throw new NotImplementedException();
        }

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

        [ContractModel]
        public object[] Model
        {
          get { throw new NotImplementedException(); }
        }

        [ContractModel]
        public int CurrentIndex
        {
          get { throw new NotImplementedException(); }
        }
      }

      public int Count
      {
        get { throw new NotImplementedException(); }
      }

      bool ICollection<TValue>.IsReadOnly
      {
        get { throw new NotImplementedException(); }
      }

      void ICollection<TValue>.Add(TValue item)
      {
        throw new NotImplementedException();
      }

      void ICollection<TValue>.Clear()
      {
        throw new NotImplementedException();
      }

      bool ICollection<TValue>.Contains(TValue item)
      {
        throw new NotImplementedException();
      }

      public void CopyTo(TValue[] array, int arrayIndex)
      {
        throw new NotImplementedException();
      }

      bool ICollection<TValue>.Remove(TValue item)
      {
        throw new NotImplementedException();
      }
    }



    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
    {
      throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
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
    public Dictionary<TKey,TValue>.Enumerator GetEnumerator()
    {
      // since this is not related to the interfae implementation, we have to repeat the interface implementation post condition.
      Contract.Ensures(Contract.Result<Dictionary<TKey, TValue>.Enumerator>().Model == this.Model);
      Contract.Ensures(Contract.Result<Dictionary<TKey, TValue>.Enumerator>().CurrentIndex == -1);
      return default(Dictionary<TKey,TValue>.Enumerator);
    }

    [ContractModel]
    public object[] Model
    {
      get { throw new NotImplementedException(); }
    }

    public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
    {
      public void Dispose()
      {
        throw new NotImplementedException();
      }

      public KeyValuePair<TKey,TValue> Current
      {
        get
        {
          Contract.Ensures((object)Contract.Result<KeyValuePair<TKey, TValue>>() == this.Model[this.CurrentIndex]);
          throw new NotImplementedException();
        }
      }

      object IEnumerator.Current
      {
        get {
          throw new NotImplementedException();
        }
      }

      public bool MoveNext()
      {
        throw new NotImplementedException();
      }

      void IEnumerator.Reset()
      {
        throw new NotImplementedException();
      }


      #region IEnumerator Members

      [ContractModel]
      public object[] Model
      {
        get { throw new NotImplementedException(); }
      }

      [ContractModel]
      public int CurrentIndex
      {
        get { throw new NotImplementedException(); }
      }

      #endregion
    }



    ICollection<TKey> IDictionary<TKey, TValue>.Keys
    {
      get { throw new NotImplementedException(); }
    }

    ICollection<TValue> IDictionary<TKey, TValue>.Values
    {
      get { throw new NotImplementedException(); }
    }

    public TValue this[TKey key]
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public void Add(TKey key, TValue value)
    {
      throw new NotImplementedException();
    }

    public bool Remove(TKey key)
    {
      Contract.Ensures(!this.ContainsKey(key));

      return true;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      throw new NotImplementedException();
    }


    bool ICollection<KeyValuePair<TKey,TValue>>.IsReadOnly
    {
      get { throw new NotImplementedException(); }
    }

    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
    {
      throw new NotImplementedException();
    }

    public void Clear()
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
  }
}
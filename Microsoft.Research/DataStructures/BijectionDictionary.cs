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

// The implementation for a bijection map

using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace System.Collections.Generic
{
  [ContractVerification(true)]
  public sealed class BijectiveMap<TKey, TValue>
    : IDictionary<TKey, TValue>
  {
    #region Object Invariant
    [Pure]
    public bool IsConsistent()
    {
      if (this.directMap.Count != this.inverseMap.Count)
        return false;

      foreach (var key in this.DirectMap.Keys)
      {
        if (!this.InverseMap.ContainsKey(this.DirectMap[key]))
          return false;
      }

      foreach (var key in this.InverseMap.Keys)
      {
        if (!this.DirectMap.ContainsKey(this.InverseMap[key]))
          return false;
      }

      return true;
    }


    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.inverseMap != null);
      Contract.Invariant(this.directMap != null);
      //Contract.Invariant(this.IsConsistent());
    }
    #endregion

    #region Private state
    private Dictionary<TKey, TValue> directMap;                // The direct map
    private Dictionary<TValue, TKey> inverseMap;               // The inverse map
    #endregion

    #region Constructors
    /// <summary>
    /// Construct a Bijective Map of default size
    /// </summary>
    public BijectiveMap()
    {
      this.directMap = new Dictionary<TKey, TValue>();
      this.inverseMap = new Dictionary<TValue, TKey>();
    }

    /// <summary>
    /// Construct a Bijective Map of size <code>n</code>
    /// </summary>
    public BijectiveMap(int size)
    {
      Contract.Requires(size >= 0);

      this.directMap = new Dictionary<TKey, TValue>(size);
      this.inverseMap = new Dictionary<TValue, TKey>(size);
    }

    /// <summary>
    /// Copy constructor
    /// </summary>
    public BijectiveMap(BijectiveMap<TKey, TValue> toClone)
    {
      Contract.Requires(toClone != null);

      this.directMap = new Dictionary<TKey, TValue>(toClone.DirectMap);
      this.inverseMap = new Dictionary<TValue, TKey>(toClone.InverseMap);
    }
    #endregion

    #region IBijectiveMap<TKey,TValue> Members

    /// <returns>The key associated with <code>value</code></returns>
    public TKey KeyForValue(TValue value)
    {
      return inverseMap[value];
    }

    /// <returns>
    /// <code>true</code> iff the co-domain of the map contains the value <code>value</code>
    /// </returns>
    public bool ContainsValue(TValue value)
    {
      return this.inverseMap.ContainsKey(value);
    }

    /// <summary>
    /// Get the direct map
    /// </summary>
    public Dictionary<TKey, TValue> DirectMap
    {
      get
      {
        Contract.Ensures(Contract.Result<Dictionary<TKey, TValue>>() != null);
        
        return this.directMap;
      }
    }

    /// <summary>
    /// Get the inverse map
    /// </summary>
    public Dictionary<TValue, TKey> InverseMap
    {
      get
      {
        Contract.Ensures(Contract.Result<Dictionary<TValue, TKey>>() != null);
        
        return this.inverseMap;
      }
    }

    public BijectiveMap<TKey, TValue> Duplicate()
    {
      var result = new BijectiveMap<TKey, TValue>();

      foreach (var pair in this.directMap)
      {
        result[pair.Key] = pair.Value;
      }

      return result;
    }

    #endregion

    #region IDictionary<TKey,TValue> Members

    /// <summary>
    /// Add the pair <code>(key, value)</code> to the map
    /// </summary>
    public void Add(TKey key, TValue value)
    {
      this.directMap.Add(key, value);
      if (!inverseMap.ContainsKey(value)) 
        this.inverseMap.Add(value, key);
    }

    /// <returns><code>true</code> iff <code>key</code> is in the map</returns>
    public bool ContainsKey(TKey key)
    {
      return this.directMap.ContainsKey(key);
    }

    /// <summary>
    /// Gets the keys in this map
    /// </summary>
    public ICollection<TKey> Keys
    {
      get
      {
        return this.directMap.Keys;
      }
    }

    /// <summary>
    /// Remove the entry corresponding to <code>key</code>
    /// </summary>
    /// <returns>true if the element is successfully removed; otherwise, false. This method also returns false if key was not found in the IDictionary</returns> 
    // [SuppressMessage("Microsoft.Contracts", "Ensures-31-220")] // It seems we can prove it now
    public bool Remove(TKey key)
    {
      Contract.Ensures(!Contract.Result<bool>() || this.Count == Contract.OldValue(this.Count) - 1);

      TValue valForKey = this.directMap[key];
      bool bDirect = this.directMap.Remove(key);
      bool bInverse = inverseMap[valForKey].Equals(key) ? this.inverseMap.Remove(valForKey) : false;

      if (bInverse)
      {
        foreach (TKey otherKey in this.directMap.Keys)
        {
          if (directMap[otherKey].Equals(valForKey))
          {
            inverseMap[valForKey] = otherKey;
            break;
          }
        }
      }

      return bDirect;
    }

    /// <summary>
    /// Tries to get a value corresponding to <code>key</code>. If found, the output is in value;
    /// </summary>
    /// <returns>true if the object that implements IDictionary contains an element with the specified key; otherwise, false. </returns>
    [SuppressMessage("Microsoft.Contracts", "Ensures-Contract.Result<bool>() == @this.ContainsKey(key)")]
    public bool TryGetValue(TKey key, out TValue value)
    {
      return this.directMap.TryGetValue(key, out value);
    }

    /// <summary>
    /// Get the values in this map
    /// </summary>
    public ICollection<TValue> Values
    {
      get
      {
        return this.directMap.Values;
      }
    }

    /// <summary>
    /// Get/Sets values in this map
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public TValue this[TKey key]
    {
      get
      {
        return this.directMap[key];
      }
      set
      {
        TValue oldVal;
        if (this.directMap.TryGetValue(key, out oldVal))
        {
          this.inverseMap.Remove(oldVal);
        }
        
        TKey oldKey;
        if (this.inverseMap.TryGetValue(value, out oldKey))
        {
          this.directMap.Remove(oldKey);
        }

        this.directMap[key] = value;
        this.inverseMap[value] = key;
      }
    }

    #endregion

    #region ICollection<KeyValuePair<TKey,TValue>> Members

    /// <summary>
    /// Add the KeyValuePair <code>item</code>
    /// </summary>
    public void Add(KeyValuePair<TKey, TValue> item)
    {
      this[item.Key] = item.Value;
    }

    /// <summary>
    /// Clear the map
    /// </summary>
    public void Clear()
    {
      this.directMap.Clear();
      this.inverseMap.Clear();
    }

    /// <summary>
    /// Does this map contain the <code>item</code>?
    /// </summary>
    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
      TValue val;
      if (this.directMap.TryGetValue(item.Key, out val) && val.Equals(item.Value))
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// Copy all the elements of this collection into the <code>array</code>, starting from <code>arrayIndex</code>
    /// </summary>
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
      int i = 0;
      foreach (var pair in this.directMap)
      {// F: we do not know that i ranges over the elements of the collection
        Contract.Assume(arrayIndex + i < array.Length);
        array[arrayIndex + (i++)] = pair;
      }
    }

    /// <summary>
    /// The number of items in the map
    /// </summary>
    public int Count
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() == this.directMap.Count);

        return this.directMap.Count;
      }
    }

    /// <summary>
    /// Always <code>false</code>
    /// </summary>
    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// Remove the <code>item</code> from the collection
    /// </summary>
    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
      var b1 = this.directMap.Remove(item.Key);
      var b2 = this.inverseMap.Remove(item.Value);

      return b1 && b2;
    }

    #endregion

    #region IEnumerable<KeyValuePair<TKey,TValue>> Members

    [Pure]
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return this.directMap.GetEnumerator();
    }

    #endregion

    #region IEnumerable Members

    //^ [Pure]
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return this.directMap.GetEnumerator();
    }

    #endregion

    #region Overridden
    public override string ToString()
    {
      var consistent = IsConsistent() ? "Map Consistent" : "WARNING: Map inconsistent";
      var direct = ToString<TKey, TValue>(this.directMap);
      var indirect = ToString<TValue, TKey>(this.inverseMap);

      return string.Format("{0}" + Environment.NewLine + "({1}, {2})", consistent, direct, indirect);
    }

    static private string ToString<A, B>(IDictionary<A, B> s)
    {
      Contract.Requires(s != null);
      Contract.Ensures(Contract.Result<string>() != null);

      var result = new StringBuilder();

      foreach (var key in s.Keys)
      {
        result.Append("(" + key.ToString() + "," + s[key] + ")");
      }

      return result.ToString();
    }
    #endregion
  }
}
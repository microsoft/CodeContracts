// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        [ContractVerification(false)]
        public bool IsConsistent()
        {
            if (directMap.Count != inverseMap.Count)
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
        private void ObjectInvariant()
        {
            Contract.Invariant(inverseMap != null);
            Contract.Invariant(directMap != null);
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
            directMap = new Dictionary<TKey, TValue>();
            inverseMap = new Dictionary<TValue, TKey>();
        }

        /// <summary>
        /// Construct a Bijective Map of size <code>n</code>
        /// </summary>
        public BijectiveMap(int size)
        {
            Contract.Requires(size >= 0);

            directMap = new Dictionary<TKey, TValue>(size);
            inverseMap = new Dictionary<TValue, TKey>(size);
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public BijectiveMap(BijectiveMap<TKey, TValue> toClone)
        {
            Contract.Requires(toClone != null);

            directMap = new Dictionary<TKey, TValue>(toClone.DirectMap);
            inverseMap = new Dictionary<TValue, TKey>(toClone.InverseMap);
        }
        #endregion

        #region IBijectiveMap<TKey,TValue> Members

        /// <returns>The key associated with <code>value</code></returns>
        [ContractVerification(false)]
        public TKey KeyForValue(TValue value)
        {
            return inverseMap[value];
        }

        /// <returns>
        /// <code>true</code> iff the co-domain of the map contains the value <code>value</code>
        /// </returns>
        [ContractVerification(false)]
        public bool ContainsValue(TValue value)
        {
            return inverseMap.ContainsKey(value);
        }

        /// <summary>
        /// Get the direct map
        /// </summary>
        public Dictionary<TKey, TValue> DirectMap
        {
            get
            {
                Contract.Ensures(Contract.Result<Dictionary<TKey, TValue>>() != null);

                return directMap;
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

                return inverseMap;
            }
        }

        [ContractVerification(false)]
        public BijectiveMap<TKey, TValue> Duplicate()
        {
            var result = new BijectiveMap<TKey, TValue>();

            foreach (var pair in directMap)
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
        [ContractVerification(false)]
        public void Add(TKey key, TValue value)
        {
            directMap.Add(key, value);
            if (!inverseMap.ContainsKey(value))
                inverseMap.Add(value, key);
        }

        /// <returns><code>true</code> iff <code>key</code> is in the map</returns>
        public bool ContainsKey(TKey key)
        {
            return directMap.ContainsKey(key);
        }

        /// <summary>
        /// Gets the keys in this map
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                return directMap.Keys;
            }
        }

        /// <summary>
        /// Remove the entry corresponding to <code>key</code>
        /// </summary>
        /// <returns>true if the element is successfully removed; otherwise, false. This method also returns false if key was not found in the IDictionary</returns> 
        // [SuppressMessage("Microsoft.Contracts", "Ensures-31-220")] // It seems we can prove it now
        [ContractVerification(false)]
        public bool Remove(TKey key)
        {
            Contract.Ensures(!Contract.Result<bool>() || this.Count == Contract.OldValue(this.Count) - 1);

            TValue valForKey = directMap[key];
            bool bDirect = directMap.Remove(key);
            bool bInverse = inverseMap[valForKey].Equals(key) ? inverseMap.Remove(valForKey) : false;

            if (bInverse)
            {
                foreach (TKey otherKey in directMap.Keys)
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
        [ContractVerification(false)]
        public bool TryGetValue(TKey key, out TValue value)
        {
            return directMap.TryGetValue(key, out value);
        }

        /// <summary>
        /// Get the values in this map
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                return directMap.Values;
            }
        }

        /// <summary>
        /// Get/Sets values in this map
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [ContractVerification(false)]
        public TValue this[TKey key]
        {
            get
            {
                return directMap[key];
            }
            set
            {
                TValue oldVal;
                if (directMap.TryGetValue(key, out oldVal))
                {
                    inverseMap.Remove(oldVal);
                }

                TKey oldKey;
                if (inverseMap.TryGetValue(value, out oldKey))
                {
                    directMap.Remove(oldKey);
                }

                directMap[key] = value;
                inverseMap[value] = key;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>> Members

        /// <summary>
        /// Add the KeyValuePair <code>item</code>
        /// </summary>
        [ContractVerification(false)]
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this[item.Key] = item.Value;
        }

        /// <summary>
        /// Clear the map
        /// </summary>
        public void Clear()
        {
            directMap.Clear();
            inverseMap.Clear();
        }

        /// <summary>
        /// Does this map contain the <code>item</code>?
        /// </summary>
        [ContractVerification(false)]
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            TValue val;
            if (directMap.TryGetValue(item.Key, out val) && val.Equals(item.Value))
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
            foreach (var pair in directMap)
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
                Contract.Ensures(Contract.Result<int>() == directMap.Count);

                return directMap.Count;
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
        [ContractVerification(false)]
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            var b1 = directMap.Remove(item.Key);
            var b2 = inverseMap.Remove(item.Value);

            return b1 && b2;
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        [Pure]
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return directMap.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        //^ [Pure]
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return directMap.GetEnumerator();
        }

        #endregion

        #region Overridden
        public override string ToString()
        {
            var consistent = IsConsistent() ? "Map Consistent" : "WARNING: Map inconsistent";
            var direct = ToString<TKey, TValue>(directMap);
            var indirect = ToString<TValue, TKey>(inverseMap);

            return string.Format("{0}" + Environment.NewLine + "({1}, {2})", consistent, direct, indirect);
        }

        [ContractVerification(false)]
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
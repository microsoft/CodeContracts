// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.DataStructures
{
    /// <summary>
    /// A dictionary.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [DebuggerDisplay("Count = {Count}")]
    [System.Runtime.InteropServices.ComVisible(false)]
    [ContractVerification(false)]
    public sealed class CDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        [Serializable]
        [StructLayout(LayoutKind.Auto)]
        private struct Entry
        {
            public int hashCode;    // Lower 31 bits of hash code, -1 if unused
            public int next;        // Index of next entry, -1 if last
            public TKey key;           // Key of entry
            public TValue value;         // Value of entry
        }

        private int[] buckets;
        private Entry[] entries;
        private int count;
#if DEBUG
        private int version;
#endif
        private int freeList;
        private int freeCount;
        private readonly IEqualityComparer<TKey> comparer;
        private KeyCollection keys;
        private ValueCollection values;

        // constants for serialization
        private const String VersionName = "Version";
        private const String HashSizeName = "HashSize";  // Must save buckets.Length
        private const String KeyValuePairsName = "KeyValuePairs";
        private const String ComparerName = "Comparer";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparer"></param>
        public CDictionary(IEqualityComparer<TKey> comparer)
            : this(0, comparer)
        {
            Contract.Requires(comparer != null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="comparer"></param>
        public CDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            Contract.Requires(capacity >= 0);
            Contract.Requires(comparer != null);
            if (capacity > 0)
                Initialize(capacity);
            this.comparer = comparer;
            Contract.Assert(this.comparer != null);
        }

        /// <summary>
        /// 
        /// </summary>
        public IEqualityComparer<TKey> Comparer
        {
            get
            {
                return comparer;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return count - freeCount; }
        }

        /// <summary>
        /// 
        /// </summary>
        public KeyCollection Keys
        {
            get
            {
                if (keys == null) keys = new KeyCollection(this);
                return keys;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ValueCollection Values
        {
            get
            {
                if (values == null) values = new ValueCollection(this);
                return values;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get
            {
                Contract.Requires(key != null);

                if (buckets != null)
                {
                    int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
                    for (int i = buckets[hashCode % buckets.Length]; i >= 0; i = entries[i].next)
                    {
                        if (entries[i].hashCode == hashCode && comparer.Equals(entries[i].key, key))
                            return entries[i].value;
                    }
                }

                Contract.Assert(false, "key not found");
                return default(TValue);
            }
            set
            {
                Contract.Requires(key != null);
                Insert(key, value, false);
            }
        }

        /// <summary>
        /// Value creator delegate.
        /// </summary>
        public delegate TValue ValueCreator<TContext>(TKey key, TContext context);
        /// <summary>
        /// Gets or creates the value.
        /// </summary>
        /// <remarks>
        /// The purpose of the context argument is to enable statically allocated delegates.
        /// To this end, non-trivial delegates passed to this method should always be declared as
        /// a static method, to prevent accidental closures.
        /// </remarks>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="context">The context.</param>
        /// <param name="valueCreator">The value creator.</param>
        /// <returns></returns>
        public TValue GetOrCreateValue<TContext>(TKey key, TContext context, ValueCreator<TContext> valueCreator)
        {
            Contract.Requires(key != null);
            TValue res;
            if (!this.TryGetValue(key, out res))
            {
                res = valueCreator(key, context);
                this.Insert(key, res, true);
            }
            return res;
        }
        /// <summary>
        /// Gets or creates the value.
        /// </summary>
        /// <remarks>
        /// The purpose of the context argument is to enable statically allocated delegates.
        /// To this end, non-trivial delegates passed to this method should always be declared as
        /// a static method, to prevent accidental closures.
        /// </remarks>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="context">The context.</param>
        /// <param name="valueCreator">The value creator.</param>
        /// <returns></returns>
        public TValue SynchronizedGetOrCreateValue<TContext>(TKey key, TContext context, ValueCreator<TContext> valueCreator)
        {
            Contract.Requires(key != null);
            lock (this)
            {
                return this.GetOrCreateValue<TContext>(key, context, valueCreator);
            }
        }

        /// <summary>
        /// Value creator delegate.
        /// </summary>
        public delegate TValue ValueCreator(TKey key);
        /// <summary>
        /// Gets or creates the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="valueCreator">The value creator.</param>
        /// <returns></returns>
        /// <remarks>
        /// This method should be used with statically allocated delegates.
        /// To this end, non-trivial delegates passed to this method should always be declared as
        /// a static method, to prevent accidental closures.
        /// </remarks>
        public TValue GetOrCreateValue(TKey key, ValueCreator valueCreator)
        {
            Contract.Requires(key != null);
            // TODO: Profile and consider merging TryGetValue and Insert
            TValue res;
            if (!this.TryGetValue(key, out res))
            {
                res = valueCreator(key);
                this.Insert(key, res, true);
            }
            return res;
        }
        /// <summary>
        /// Gets or creates the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="valueCreator">The value creator.</param>
        /// <returns></returns>
        /// <remarks>
        /// This method should be used with statically allocated delegates.
        /// To this end, non-trivial delegates passed to this method should always be declared as
        /// a static method, to prevent accidental closures.
        /// </remarks>
        public TValue SynchronizedGetOrCreateValue(TKey key, ValueCreator valueCreator)
        {
            lock (this)
            {
                return this.GetOrCreateValue(key, valueCreator);
            }
        }

        /// <summary>
        /// This method expect the key to be new.
        /// In other words, there should not be a mapping for this key already in the dictionary.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            Contract.Requires(key != null);
            this.Insert(key, value, true);
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="entries">The entries.</param>
        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> entries)
        {
            Contract.Requires(entries != null);

            KeyValuePair<TKey, TValue>[] array = entries as KeyValuePair<TKey, TValue>[];
            if (array != null)
                this.AddRange(array);
            else
                foreach (var kvp in entries)
                {
                    Contract.Assume(kvp.Key != null);
                    this.Insert(kvp.Key, kvp.Value, true);
                }
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="entries">The entries.</param>
        public void AddRange(KeyValuePair<TKey, TValue>[] entries)
        {
            Contract.Requires(entries != null);

            foreach (var kvp in entries)
            {
                Contract.Assume(kvp.Key != null);
                this.Insert(kvp.Key, kvp.Value, true);
            }
        }

        /// <summary>
        /// Overrides the range.
        /// </summary>
        /// <param name="entries">The entries.</param>
        public void OverrideRange(IEnumerable<KeyValuePair<TKey, TValue>> entries)
        {
            Contract.Requires(entries != null);

            KeyValuePair<TKey, TValue>[] array = entries as KeyValuePair<TKey, TValue>[];
            if (array != null)
                this.OverrideRange(array);
            else
                foreach (var kvp in entries)
                {
                    Contract.Assume(kvp.Key != null);
                    this.Insert(kvp.Key, kvp.Value, false);
                }
        }

        /// <summary>
        /// Overrides the range.
        /// </summary>
        /// <param name="entries">The entries.</param>
        public void OverrideRange(KeyValuePair<TKey, TValue>[] entries)
        {
            Contract.Requires(entries != null);

            foreach (var kvp in entries)
            {
                Contract.Assume(kvp.Key != null);
                this.Insert(kvp.Key, kvp.Value, false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            if (count > 0)
            {
                for (int i = 0; i < buckets.Length; i++) buckets[i] = -1;
                Array.Clear(entries, 0, count);
                freeList = -1;
                count = 0;
                freeCount = 0;
#if DEBUG
                version++;
#endif
            }
        }

        /// <summary>
        /// Removes all elements, and trims the size of the dictionary.
        /// </summary>
        public void ClearAndTrim()
        {
            if (count > 0)
            {
                buckets = null;
                entries = null;
                freeList = 0;
                count = 0;
                freeCount = 0;
                keys = null;
                values = null;
#if DEBUG
                version++;
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Pure]
        public bool ContainsKey(TKey key)
        {
            Contract.Requires(key != null);

            if (buckets != null)
            {
                int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
                for (int i = buckets[hashCode % buckets.Length]; i >= 0; i = entries[i].next)
                {
                    if (entries[i].hashCode == hashCode && comparer.Equals(entries[i].key, key))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<TKey, TValue>[] ToArray()
        {
            if (this.Count == 0)
                return CArray.Empty<KeyValuePair<TKey, TValue>>();
            else
            {
                KeyValuePair<TKey, TValue>[] res = new KeyValuePair<TKey, TValue>[this.Count];
                CopyTo(res, 0);
                return res;
            }
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            Contract.Requires(array != null);
            Contract.Requires((uint)index <= (uint)array.Length);
            Contract.Requires(array.Length - index >= Count);

            int count = this.count;
            Entry[] entries = this.entries;
            for (int i = 0; i < count; i++)
            {
                if (entries[i].hashCode >= 0)
                {
                    array[index++] = new KeyValuePair<TKey, TValue>(entries[i].key, entries[i].value);
                }
            }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public CDictionary<TKey, TValue> Clone()
        {
            int count = this.count;
            CDictionary<TKey, TValue> res = new CDictionary<TKey, TValue>(this.Count, comparer);
            Entry[] entries = this.entries;
            for (int i = 0; i < count; i++)
            {
                if (entries[i].hashCode >= 0)
                    res.Add(entries[i].key, entries[i].value);
            }
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this, Enumerator.KeyValuePair);
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return new Enumerator(this, Enumerator.KeyValuePair);
        }

        private void Initialize(int capacity)
        {
            Contract.Requires(capacity >= 0);
            int size = HashHelpers.GetPrime(capacity);
            buckets = new int[size];
            for (int i = 0; i < buckets.Length; i++) buckets[i] = -1;
            entries = new Entry[size];
            freeList = -1;
        }

        private void Insert(TKey key, TValue value, bool add)
        {
            Contract.Requires(key != null);

            int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
            if (buckets == null)
                Initialize(0);
            else
                for (int i = buckets[hashCode % buckets.Length]; i >= 0; i = entries[i].next)
                {
                    if (entries[i].hashCode == hashCode && comparer.Equals(entries[i].key, key))
                    {
                        Contract.Assert(!add);
                        entries[i].value = value;
#if DEBUG
                        version++;
#endif
                        return;
                    }
                }
            int index;
            if (freeCount > 0)
            {
                index = freeList;
                freeList = entries[index].next;
                freeCount--;
            }
            else
            {
                if (count == entries.Length) Grow();
                index = count;
                count++;
            }
            int bucket = hashCode % buckets.Length;
            entries[index].hashCode = hashCode;
            entries[index].next = buckets[bucket];
            entries[index].key = key;
            entries[index].value = value;
            buckets[bucket] = index;
#if DEBUG
            version++;
#endif
        }

        private void Shrink()
        {
            int newSize = HashHelpers.GetPrime(this.count / 2);
            int[] newBuckets = new int[newSize];
            for (int i = 0; i < newBuckets.Length; i++) newBuckets[i] = -1;
            Entry[] newEntries = new Entry[newSize];
            int count = this.count;
            int newCount = 0;
            for (int i = 0; i < count; i++)
            {
                int hashCode = entries[i].hashCode;
                if (hashCode >= 0)
                {
                    newEntries[newCount].key = entries[i].key;
                    newEntries[newCount].hashCode = hashCode;
                    int bucket = hashCode % newSize;
                    newEntries[newCount].value = entries[i].value;
                    newEntries[newCount].next = newBuckets[bucket];
                    newBuckets[bucket] = newCount++;
                }
            }
            this.count = newCount;
            buckets = newBuckets;
            entries = newEntries;
            freeList = -1;
            freeCount = 0;
        }

        private void Grow()
        {
            int newSize = HashHelpers.GetPrime(this.count * 2);
            int[] newBuckets = new int[newSize];
            for (int i = 0; i < newBuckets.Length; i++) newBuckets[i] = -1;
            int count = this.count;
            Entry[] newEntries = new Entry[newSize];
            Array.Copy(entries, 0, newEntries, 0, count);
            for (int i = 0; i < count; i++)
            {
                int bucket = newEntries[i].hashCode % newSize;
                newEntries[i].next = newBuckets[bucket];
                newBuckets[bucket] = i;
            }
            buckets = newBuckets;
            entries = newEntries;
        }

        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <param name="keys">The keys.</param>
        public void RemoveRange(IEnumerable<TKey> keys)
        {
            Contract.Requires(keys != null);

            TKey[] array = keys as TKey[];
            if (array != null)
                this.RemoveRange(array);
            else
                foreach (var key in keys)
                {
                    Contract.Assume(key != null);
                    this.Remove(key);
                }
        }

        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <param name="keys">The keys.</param>
        public void RemoveRange(TKey[] keys)
        {
            Contract.Requires(keys != null);

            foreach (var key in keys)
            {
                Contract.Assume(key != null);
                this.Remove(key);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            Contract.Requires(key != null);

            if (buckets != null)
            {
                int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
                int bucket = hashCode % buckets.Length;
                int last = -1;
                for (int i = buckets[bucket]; i >= 0; last = i, i = entries[i].next)
                {
                    if (entries[i].hashCode == hashCode && comparer.Equals(entries[i].key, key))
                    {
                        if (last < 0)
                        {
                            buckets[bucket] = entries[i].next;
                        }
                        else
                        {
                            entries[last].next = entries[i].next;
                        }
                        entries[i].hashCode = -1;
                        entries[i].next = freeList;
                        entries[i].key = default(TKey);
                        entries[i].value = default(TValue);
                        freeList = i;
                        freeCount++;
#if DEBUG
                        version++;
#endif
                        if (freeCount > count / 2)
                            Shrink();
                        return true;
                    }
                }
            }
            return false;
        }

        internal void InternalRemoveOrFail(TKey key)
        {
            Contract.Requires(key != null);
            if (!this.Remove(key))
                Contract.Assert(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            Contract.Requires(key != null);

            if (buckets != null)
            {
                int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
                for (int i = buckets[hashCode % buckets.Length]; i >= 0; i = entries[i].next)
                {
                    if (entries[i].hashCode == hashCode && comparer.Equals(entries[i].key, key))
                    {
                        value = entries[i].value;
                        return true;
                    }
                }
            }
            value = default(TValue);
            return false;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this, Enumerator.KeyValuePair);
        }

        [Conditional("DEBUG")]
        private static void VerifyKey(object key)
        {
            Contract.Assert(key != null);
            Contract.Assert(key is TKey, "bad key");
        }

        private static bool IsCompatibleKey(object key)
        {
            Contract.Assert(key != null);
            return (key is TKey);
        }

        [Conditional("DEBUG")]
        private static void VerifyValueType(object value)
        {
            Contract.Assert(
                (value is TValue) || (value == null && !typeof(TValue).IsValueType),
                "bad value");
        }

        /// <summary>
        /// 
        /// </summary>
        [Serializable()]
        [StructLayout(LayoutKind.Auto)]
        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>,
            IDictionaryEnumerator
        {
            private CDictionary<TKey, TValue> dictionary; // null after Dispose()
#if DEBUG
            private int version;
#endif
            private int index;
            private KeyValuePair<TKey, TValue> current;
            private int getEnumeratorRetType;  // What should Enumerator.Current return?

            internal const int DictEntry = 1;
            internal const int KeyValuePair = 2;

            internal Enumerator(CDictionary<TKey, TValue> dictionary, int getEnumeratorRetType)
            {
                Contract.Requires(dictionary != null);
                this.dictionary = dictionary;
#if DEBUG
                version = dictionary.version;
#endif
                index = 0;
                this.getEnumeratorRetType = getEnumeratorRetType;
                current = new KeyValuePair<TKey, TValue>();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                Contract.Assume(dictionary != null);
#if DEBUG
                Contract.Assert(version == dictionary.version, "invalid operation");
#endif

                // Use unsigned comparison since we set index to dictionary.count+1 when the enumeration ends.
                // dictionary.count+1 could be negative if dictionary.count is Int32.MaxValue
                var count = dictionary.count;
                var entries = dictionary.entries;
                while ((uint)index < (uint)count)
                {
                    if (entries[index].hashCode >= 0)
                    {
                        current = new KeyValuePair<TKey, TValue>(entries[index].key, entries[index].value);
                        index++;
                        return true;
                    }
                    index++;
                }

                index = count + 1;
                current = new KeyValuePair<TKey, TValue>();
                return false;
            }

            /// <summary>
            /// 
            /// </summary>
            public KeyValuePair<TKey, TValue> Current
            {
                get { return current; }
            }

            /// <summary>
            /// 
            /// </summary>
            public void Dispose()
            {
                if (dictionary != null)
                    dictionary = null;
            }

            object IEnumerator.Current
            {
                get
                {
                    Contract.Assert(index != 0 && (index != dictionary.count + 1), "invalid operation");

                    if (getEnumeratorRetType == DictEntry)
                    {
                        Contract.Assume(current.Key != null);
                        return new System.Collections.DictionaryEntry(current.Key, current.Value);
                    }
                    else
                    {
                        return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
                    }
                }
            }

            void IEnumerator.Reset()
            {
                Contract.Assume(dictionary != null);
#if DEBUG
                Contract.Assert(version == dictionary.version, "invalid operation");
#endif

                index = 0;
                current = new KeyValuePair<TKey, TValue>();
            }

            DictionaryEntry IDictionaryEnumerator.Entry
            {
                get
                {
                    Contract.Assume(current.Key != null);
                    Contract.Assert(dictionary != null);
                    Contract.Assert(index != 0 && (index != dictionary.count + 1), "invalid operation");

                    return new DictionaryEntry(current.Key, current.Value);
                }
            }

            object IDictionaryEnumerator.Key
            {
                get
                {
                    Contract.Assert(dictionary != null);
                    Contract.Assert(index != 0 && (index != dictionary.count + 1), "invalid operation");

                    return current.Key;
                }
            }

            object IDictionaryEnumerator.Value
            {
                get
                {
                    Contract.Assert(dictionary != null);
                    Contract.Assert(index != 0 && (index != dictionary.count + 1), "invalid operation");

                    return current.Value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DebuggerDisplay("Count = {Count}")]
        [Serializable()]
        public sealed class KeyCollection : ICollection<TKey>, ICollection
        {
            private CDictionary<TKey, TValue> dictionary;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="dictionary"></param>
            public KeyCollection(CDictionary<TKey, TValue> dictionary)
            {
                Contract.Requires(dictionary != null);
                this.dictionary = dictionary;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public Enumerator GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="array"></param>
            /// <param name="index"></param>
            public void CopyTo(TKey[] array, int index)
            {
                Contract.Assert(array != null);
                Contract.Assert((uint)index <= (uint)array.Length, "index out of range");
                Contract.Assert(array.Length - index >= dictionary.Count, "array too small");

                int count = dictionary.count;
                Entry[] entries = dictionary.entries;
                for (int i = 0; i < count; i++)
                {
                    if (entries[i].hashCode >= 0) array[index++] = entries[i].key;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public int Count
            {
                get { return dictionary.Count; }
            }

            bool ICollection<TKey>.IsReadOnly
            {
                get { return true; }
            }

            void ICollection<TKey>.Add(TKey item)
            {
                Contract.Assert(false, "not supported");
            }

            void ICollection<TKey>.Clear()
            {
                Contract.Assert(false, "not supported");
            }

            /// <summary>
            /// 
            /// </summary>
            public bool Contains(TKey item)
            {
                return dictionary.ContainsKey(item);
            }

            bool ICollection<TKey>.Remove(TKey item)
            {
                Contract.Assert(false, "not supported");
                return false;
            }

            IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            void ICollection.CopyTo(Array array, int index)
            {
                Contract.Assert(array != null);
                Contract.Assert(array.Rank == 1, "rank multi dim not supported");
                Contract.Assert(array.GetLowerBound(0) == 0, "non zero lower bound");
                Contract.Assert((uint)index <= (uint)array.Length, "index out of bounds");
                Contract.Assert(array.Length - index >= dictionary.Count, "array too small");

                TKey[] keys = array as TKey[];
                if (keys != null)
                {
                    CopyTo(keys, index);
                }
                else
                {
                    object[] objects = array as object[];
                    Contract.Assert(objects != null);

                    int count = dictionary.count;
                    Entry[] entries = dictionary.entries;
                    try
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if (entries[i].hashCode >= 0) objects[index++] = entries[i].key;
                        }
                    }
                    catch (ArrayTypeMismatchException)
                    {
                        Contract.Assert(false, "invalid array type");
                    }
                }
            }

            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            Object ICollection.SyncRoot
            {
                get { return this; }
            }

            /// <summary>
            /// 
            /// </summary>
            [Serializable()]
            [StructLayout(LayoutKind.Auto)]
            public struct Enumerator : IEnumerator<TKey>, System.Collections.IEnumerator
            {
                private CDictionary<TKey, TValue> dictionary;
                private int index;
#if DEBUG
                private int version;
#endif
                private TKey currentKey;

                internal Enumerator(CDictionary<TKey, TValue> dictionary)
                {
                    this.dictionary = dictionary;
#if DEBUG
                    version = dictionary.version;
#endif
                    index = 0;
                    currentKey = default(TKey);
                }

                /// <summary>
                /// 
                /// </summary>
                public void Dispose()
                {
                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public bool MoveNext()
                {
#if DEBUG
                    Contract.Assert(version == dictionary.version, "invalid operation");
#endif

                    var count = dictionary.count;
                    var entries = dictionary.entries;
                    while ((uint)index < (uint)count)
                    {
                        if (entries[index].hashCode >= 0)
                        {
                            currentKey = entries[index].key;
                            index++;
                            return true;
                        }
                        index++;
                    }

                    index = count + 1;
                    currentKey = default(TKey);
                    return false;
                }

                /// <summary>
                /// 
                /// </summary>
                public TKey Current
                {
                    get
                    {
                        return currentKey;
                    }
                }

                Object System.Collections.IEnumerator.Current
                {
                    get
                    {
                        Contract.Assume(index != 0 && (index != dictionary.count + 1), "invalid operation");

                        return currentKey;
                    }
                }

                void System.Collections.IEnumerator.Reset()
                {
#if DEBUG
                    Contract.Assert(version == dictionary.version, "invalid operation");
#endif

                    index = 0;
                    currentKey = default(TKey);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DebuggerDisplay("Count = {Count}")]
        [Serializable()]
        public sealed class ValueCollection : ICollection<TValue>, ICollection
        {
            private CDictionary<TKey, TValue> dictionary;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="dictionary"></param>
            public ValueCollection(CDictionary<TKey, TValue> dictionary)
            {
                Contract.Requires(dictionary != null);
                this.dictionary = dictionary;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public Enumerator GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="array"></param>
            /// <param name="index"></param>
            public void CopyTo(TValue[] array, int index)
            {
                Contract.Assert(array != null);
                Contract.Assert((uint)index <= (uint)array.Length, "index out of range");
                Contract.Assert(array.Length - index >= dictionary.Count, "array too small");

                int count = dictionary.count;
                Entry[] entries = dictionary.entries;
                for (int i = 0; i < count; i++)
                {
                    if (entries[i].hashCode >= 0) array[index++] = entries[i].value;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public int Count
            {
                get { return dictionary.Count; }
            }

            bool ICollection<TValue>.IsReadOnly
            {
                get { return true; }
            }

            void ICollection<TValue>.Add(TValue item)
            {
                Contract.Assert(false, "not supported");
            }

            bool ICollection<TValue>.Remove(TValue item)
            {
                Contract.Assert(false, "not supported");
                return false;
            }

            void ICollection<TValue>.Clear()
            {
                Contract.Assert(false, "not supported");
            }

            bool ICollection<TValue>.Contains(TValue item)
            {
                Contract.Assert(false, "not supported");
                return false;
            }

            IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            void ICollection.CopyTo(Array array, int index)
            {
                Contract.Assert(array != null);
                Contract.Assert(array.Rank == 1, "rank multi dim not supported");
                Contract.Assert(array.GetLowerBound(0) == 0, "non zero lower bound");
                Contract.Assert((uint)index <= (uint)array.Length, "index out of bounds");
                Contract.Assert(array.Length - index >= dictionary.Count, "array too small");

                TValue[] values = array as TValue[];
                if (values != null)
                {
                    CopyTo(values, index);
                }
                else
                {
                    object[] objects = array as object[];
                    Contract.Assert(objects != null);

                    int count = dictionary.count;
                    Entry[] entries = dictionary.entries;
                    try
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if (entries[i].hashCode >= 0) objects[index++] = entries[i].value;
                        }
                    }
                    catch (ArrayTypeMismatchException)
                    {
                        Contract.Assert(false, "invalid array type");
                    }
                }
            }

            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            Object ICollection.SyncRoot
            {
                get { return this; }
            }

            /// <summary>
            /// 
            /// </summary>
            [Serializable()]
            [StructLayout(LayoutKind.Auto)]
            public struct Enumerator : IEnumerator<TValue>, System.Collections.IEnumerator
            {
                private CDictionary<TKey, TValue> dictionary;
                private int index;
#if DEBUG
                private int version;
#endif
                private TValue currentValue;

                internal Enumerator(CDictionary<TKey, TValue> dictionary)
                {
                    this.dictionary = dictionary;
#if DEBUG
                    version = dictionary.version;
#endif
                    index = 0;
                    currentValue = default(TValue);
                }

                /// <summary>
                /// 
                /// </summary>
                public void Dispose()
                {
                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public bool MoveNext()
                {
                    Contract.Assume(dictionary != null);
#if DEBUG
                    Contract.Assert(version == dictionary.version, "invalid operation");
#endif

                    var count = dictionary.count;
                    var entries = dictionary.entries;
                    while ((uint)index < (uint)count)
                    {
                        if (entries[index].hashCode >= 0)
                        {
                            currentValue = entries[index].value;
                            index++;
                            return true;
                        }
                        index++;
                    }
                    index = count + 1;
                    currentValue = default(TValue);
                    return false;
                }

                /// <summary>
                /// 
                /// </summary>
                public TValue Current
                {
                    get
                    {
                        return currentValue;
                    }
                }

                Object System.Collections.IEnumerator.Current
                {
                    get
                    {
                        Contract.Assert(index != 0 && (index != dictionary.count + 1), "invalid operation");

                        return currentValue;
                    }
                }

                void System.Collections.IEnumerator.Reset()
                {
                    Contract.Assume(dictionary != null);
#if DEBUG
                    Contract.Assert(version == dictionary.version, "invalid operation");
#endif
                    index = 0;
                    currentValue = default(TValue);
                }
            }
        }

        /// <summary>
        /// enumerate key/value pairs sorted by key
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<TKey, TValue>> EnumerateInOrder()
        {
            List<TKey> keys = new List<TKey>(this.Keys);
            keys.Sort();
            foreach (var key in keys)
                yield return new KeyValuePair<TKey, TValue>(key, this[key]);
        }
    }
}

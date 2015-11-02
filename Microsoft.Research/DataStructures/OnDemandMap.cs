// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.DataStructures
{
    public struct OnDemandMap<Key, Value>
    {
        private Dictionary<Key, Value> dictionary;

        private Dictionary<Key, Value> Ensure()
        {
            if (dictionary == null)
            {
                dictionary = new Dictionary<Key, Value>();
            }
            return dictionary;
        }

        public void Add(Key key, Value value)
        {
            var dict = Ensure();
            dict.Add(key, value);
        }

        public Value this[Key key]
        {
            get
            {
                if (dictionary == null)
                {
                    throw new KeyNotFoundException(key.ToString());
                }
                return dictionary[key];
            }
            set
            {
                var dict = Ensure();
                dict[key] = value;
            }
        }

        public bool TryGetValue(Key key, out Value value)
        {
            if (dictionary == null)
            {
                value = default(Value); return false;
            }
            return dictionary.TryGetValue(key, out value);
        }

        public IEnumerable<Value> Values
        {
            get
            {
                if (dictionary == null) { return new Value[0]; }
                return dictionary.Values;
            }
        }

        public bool ContainsKey(Key key)
        {
            if (dictionary == null) return false;
            return dictionary.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            if (dictionary == null) yield break;
            foreach (var kv in dictionary)
            {
                yield return kv;
            }
        }
    }

    public struct OnDemandSet<Key>
    {
        private Set<Key> set;

        private Set<Key> Ensure()
        {
            if (set == null)
            {
                set = new Set<Key>();
            }
            return set;
        }

        public void Add(Key key)
        {
            var set = Ensure();
            set.Add(key);
        }

        public bool Contains(Key key)
        {
            if (set == null)
            {
                return false;
            }
            return set.Contains(key);
        }

        public IEnumerable<Key> Elements
        {
            get
            {
                if (set == null) { return new Key[0]; }
                return set;
            }
        }
    }

    [ContractVerification(true)]
    public struct OnDemandCache<Key, Value>
    {
        private Dictionary<Key, Value> dictionary;
        private WrapQueue<KeyValuePair<Key, Value>> hitQueue;

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            Contract.Invariant((hitQueue == null) == (dictionary == null));
        }

        public int Capacity { get; set; }

        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        private Dictionary<Key, Value> Ensure()
        {
            if (dictionary == null)
            {
                dictionary = new Dictionary<Key, Value>();
            }
            if (Capacity < 10)
            {
                Capacity = 1000;
            }
            if (hitQueue == null || hitQueue.Capacity != this.Capacity / 2)
            {
                hitQueue = new WrapQueue<KeyValuePair<Key, Value>>(this.Capacity / 2);
            }
            return dictionary;
        }

        [ContractVerification(false)]
        public void Add(Key key, Value value)
        {
            var dict = Ensure();
            dict.Add(key, value);
            LimitCapacity(dict);
        }

        [ContractVerification(false)]
        private void LimitCapacity(Dictionary<Key, Value> dict)
        {
            Contract.Requires(dict != null);
            Contract.Requires(hitQueue != null);

            if (dict.Count > this.Capacity)
            {
                dict.Clear();
                foreach (var kv in hitQueue.Elements())
                {
                    dict[kv.Key] = kv.Value;
                }
            }
        }

#if false
        public Value this[Key key]
        {
            get
            {
                if (dictionary == null)
                {
                    throw new KeyNotFoundException(key.ToString());
                }
                return dictionary[key];
            }
            set
            {
                var dict = Ensure();
                dict[key] = value;
            }
        }
#endif
        [ContractVerification(false)]
        public bool TryGetValue(Key key, out Value value)
        {
            if (dictionary == null)
            {
                value = default(Value); return false;
            }
            var result = dictionary.TryGetValue(key, out value);
            if (result)
            {
                // hit
                hitQueue.Push(new KeyValuePair<Key, Value>(key, value));
            }
            return result;
        }

        public IEnumerable<Value> Values
        {
            get
            {
                if (dictionary == null) { return new Value[0]; }
                return dictionary.Values;
            }
        }

        [ContractVerification(false)]
        public bool ContainsKey(Key key)
        {
            if (dictionary == null) return false;
            return dictionary.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            if (dictionary == null) yield break;
            foreach (var kv in dictionary)
            {
                yield return kv;
            }
        }
    }

    [ContractVerification(true)]
    public class WrapQueue<T>
    {
        private T[] queue;
        private int insertAt;
        private int removeAt;

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(queue != null);
            Contract.Invariant(insertAt >= 0);
            Contract.Invariant(insertAt <= queue.Length);
            Contract.Invariant(removeAt >= 0);
            Contract.Invariant(removeAt <= queue.Length);
            Contract.Invariant(queue.Length > 0);
        }

        public WrapQueue(int capacity)
        {
            Contract.Requires(capacity > 0);

            queue = new T[capacity];
        }

        public int Capacity { get { return queue.Length; } }

        public int Count
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                Contract.Ensures(Contract.Result<int>() <= this.Capacity);

                if (insertAt >= removeAt) return insertAt - removeAt;
                return queue.Length - removeAt + insertAt;
            }
        }

        public void Push(T item)
        {
            if (insertAt >= queue.Length)
            {
                insertAt = 0;
            }
            if (this.Count == queue.Length)
            { // at capacity
                this.Pop();
            }
            queue[insertAt++] = item;
        }


        public T Pop()
        {
            Contract.Ensures(insertAt == Contract.OldValue(insertAt));
            Contract.Ensures(queue == Contract.OldValue(queue));

            if (this.Count > 0)
            {
                if (removeAt >= queue.Length)
                {
                    removeAt = 0;
                }
                return queue[removeAt++];
            }
            throw new InvalidOperationException("queue empty");
        }

        public IEnumerable<T> Elements()
        {
            var limit = removeAt > insertAt ? insertAt + this.Capacity : insertAt;

            for (int i = removeAt; i < limit; i++)
            {
                yield return queue[i % this.Capacity];
            }
        }
    }
}

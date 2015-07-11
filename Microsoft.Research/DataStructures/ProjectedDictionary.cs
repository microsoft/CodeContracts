// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Research.DataStructures
{
    internal class ProjectionEqualityComparer<T, TSub> : EqualityComparer<T>
    {
        private readonly Func<T, TSub> projection;

        public ProjectionEqualityComparer(Func<T, TSub> projection)
        {
            this.projection = projection;
        }

        public override bool Equals(T x, T y)
        {
            return projection(x).Equals(projection(y));
        }

        public override int GetHashCode(T obj)
        {
            return projection(obj).GetHashCode();
        }
    }

    internal interface IProjector<T1, T2>
    {
        T2 Project(T1 x);
    }

    internal class ProjectionDictionary<TKey, TSubKey, TValue> : Dictionary<TKey, TValue>
    {
        public ProjectionDictionary(Func<TKey, TSubKey> projection)
          : base(new ProjectionEqualityComparer<TKey, TSubKey>(projection))
        { }

        public class For<TProjector> : ProjectionDictionary<TKey, TSubKey, TValue>
          where TProjector : IProjector<TKey, TSubKey>, new()
        {
            public For() : base((new TProjector()).Project) { }
        }
    }

    public class ProjectedDictionary<TKey, TSubKey, TValue> : Dictionary<TSubKey, KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        private readonly Func<TKey, TSubKey> projection;

        /// <summary>
        /// Wrapper so we get contract check.
        /// </summary>
        new public int Count { get { return base.Count; } }

        /// <summary>
        /// Wrapper so we get contract check.
        /// </summary>
        new public void Clear() { base.Clear(); }

        public ProjectedDictionary(Func<TKey, TSubKey> projection)
        {
            this.projection = projection;
        }

        public virtual void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(projection(item.Key), item);
        }

        public virtual void Add(TKey key, TValue value)
        {
            this.Add(projection(key), new KeyValuePair<TKey, TValue>(key, value));
        }

        public virtual bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.Contains(new KeyValuePair<TSubKey, KeyValuePair<TKey, TValue>>(projection(item.Key), item));
        }

        public virtual bool ContainsKey(TKey key)
        {
            return this.ContainsKey(projection(key));
        }

        public virtual bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TSubKey, KeyValuePair<TKey, TValue>>)this).Remove(new KeyValuePair<TSubKey, KeyValuePair<TKey, TValue>>(projection(item.Key), item));
        }

        public virtual bool Remove(TKey key)
        {
            return this.Remove(projection(key));
        }

        public virtual bool TryGetValue(TKey key, out TValue value)
        {
            KeyValuePair<TKey, TValue> p;
            if (!this.TryGetValue(projection(key), out p))
            {
                value = default(TValue);
                return false;
            }
            value = p.Value;
            return true;
        }

        private Dictionary<TSubKey, KeyValuePair<TKey, TValue>> Base { get { return this; } }
        private Dictionary<TKey, TValue> BaseDictionary { get { return this.Base.Select(p => p.Value).ToDictionary(p => p.Key, p => p.Value); } }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return this.Base.Select(p => p.Value).GetEnumerator();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly { get { return false; } }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            this.Base.Select(p => p.Value).ToArray().CopyTo(array, index);
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values { get { return this.BaseDictionary.Values; } }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys { get { return this.BaseDictionary.Keys; } }

        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get
            {
                return this[projection(key)].Value;
            }
            set
            {
                this[projection(key)] = new KeyValuePair<TKey, TValue>(key, value);
            }
        }
    }

    public class ProjectedDictionary<TKey, TSubKey1, TSubKey2, TValue> : ProjectedDictionary<TKey, TSubKey1, TValue>
    {
        private readonly ProjectedDictionary<TKey, TSubKey2, TValue> dict2;

        public ProjectedDictionary(Func<TKey, TSubKey1> proj1, Func<TKey, TSubKey2> proj2)
          : base(proj1)
        {
            dict2 = new ProjectedDictionary<TKey, TSubKey2, TValue>(proj2);
        }

        public override void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            dict2.Add(key, value);
        }

        public new void Clear()
        {
            base.Clear();
            dict2.Clear();
        }

        public virtual bool ContainsKey(TSubKey2 subkey2)
        {
            return dict2.ContainsKey(subkey2);
        }

        public override bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return base.Remove(item) || dict2.Remove(item);
        }

        public override bool Remove(TKey key)
        {
            return base.Remove(key) || dict2.Remove(key);
        }

        public new bool Remove(TSubKey1 subkey1)
        {
            KeyValuePair<TKey, TValue> item;
            if (!base.TryGetValue(subkey1, out item))
                return false;
            base.Remove(subkey1);
            dict2.Remove(item.Key);
            return true;
        }

        public virtual bool Remove(TSubKey2 subkey2)
        {
            KeyValuePair<TKey, TValue> item;
            if (!dict2.TryGetValue(subkey2, out item))
                return false;
            base.Remove(item.Key);
            dict2.Remove(subkey2);
            return true;
        }

        public bool TryGetValue(TSubKey1 subkey1, out TValue value)
        {
            KeyValuePair<TKey, TValue> item;
            if (!base.TryGetValue(subkey1, out item))
            {
                value = default(TValue);
                return false;
            }
            value = item.Value;
            return true;
        }

        public virtual bool TryGetValue(TSubKey2 subkey2, out TValue value)
        {
            KeyValuePair<TKey, TValue> item;
            if (!dict2.TryGetValue(subkey2, out item))
            {
                value = default(TValue);
                return false;
            }
            value = item.Value;
            return true;
        }
    }
}

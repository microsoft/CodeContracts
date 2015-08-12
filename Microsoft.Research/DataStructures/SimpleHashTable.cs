// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Microsoft.Research.DataStructures
{
    internal sealed class HashtableEntry<KeyType, ValueType>
    {
        internal KeyType Key;
        internal ValueType Value;
        internal uint hashCode;
        internal HashtableEntry<KeyType, ValueType> next;

        internal HashtableEntry(KeyType key, ValueType value, uint hashCode, HashtableEntry<KeyType, ValueType> next)
        {
            this.Key = key;
            this.Value = value;
            this.hashCode = hashCode;
            this.next = next;
        }

        internal HashtableEntry(HashtableEntry<KeyType, ValueType> template)
        {
            this.Key = template.Key;
            this.Value = template.Value;
            this.hashCode = template.hashCode;
            if (template.next != null)
                this.next = new HashtableEntry<KeyType, ValueType>(template.next);
        }
    }

    internal struct HashtableEntryEnumerator<KeyType, ValueType>
    {
        private HashtableEntry<KeyType, ValueType>[] table;
        private int tableIndex;
        private HashtableEntry<KeyType, ValueType> currentEntry;

        internal HashtableEntryEnumerator(HashtableEntry<KeyType, ValueType>[] table)
        {
            this.table = table;
            tableIndex = 0;
            currentEntry = null;
        }

        public HashtableEntry<KeyType, ValueType> Current
        {
            get { return currentEntry; }
        }

        public bool MoveNext()
        {
            if (currentEntry != null)
                currentEntry = currentEntry.next;
            while (currentEntry == null)
            {
                if (tableIndex >= table.Length)
                    return false;
                currentEntry = table[tableIndex++];
            }
            return true;
        }
    }

    public sealed class SimpleHashtable<KeyType, ValueType>
    // where KeyType : class
    {
        private HashtableEntry<KeyType, ValueType>[] table;
        public int Count;
        private uint threshold;

        public SimpleHashtable()
          : this(8)
        {
        }

        public SimpleHashtable(uint threshold)
        {
            if (threshold < 8)
                threshold = 8;
            table = new HashtableEntry<KeyType, ValueType>[(int)(threshold * 2 - 1)];
            this.threshold = threshold;
        }

        public SimpleHashtable(SimpleHashtable<KeyType, ValueType> template)
        {
            this.Count = template.Count;
            threshold = template.threshold;
            int n = template.table.Length;
            table = new HashtableEntry<KeyType, ValueType>[n];
            for (int i = 0; i < n; i++)
            {
                var templateEntry = template.table[i];
                if (templateEntry != null)
                    table[i] = new HashtableEntry<KeyType, ValueType>(templateEntry);
            }
        }

        public void Add(KeyType key, ValueType value)
        {
            uint hashCode = (uint)key.GetHashCode();
            var e = this.GetHashtableEntry(key, hashCode);
            if (e != null) throw new InvalidOperationException();
            if (++this.Count >= threshold) this.Rehash();
            uint index = hashCode % (uint)table.Length;
            table[index] = new HashtableEntry<KeyType, ValueType>(key, value, hashCode, table[index]);
        }

        private HashtableEntry<KeyType, ValueType>/*?*/ GetHashtableEntry(KeyType key, uint hashCode)
        {
            uint index = hashCode % (uint)table.Length;
            var e = table[index];
            if (e == null) return null;
            //Run down chain, using only object identity. Perhaps we get lucky. 
            //At any rate, each loop is as cheap as it gets and the chain should be short.
            if (Object.Equals(e.Key, key)) return e;
            var curr = e.next;
            while (curr != null)
            {
                if (Object.Equals(curr.Key, key)) return curr;
                curr = curr.next;
            }
            //now check hash codes and call Equals
            if (e.hashCode == hashCode && e.Key.Equals(key)) return e;
            curr = e.next;
            while (curr != null)
            {
                if (curr.hashCode == hashCode && curr.Key.Equals(key)) return curr;
                curr = curr.next;
            }
            return null;
        }

        public void Clear()
        {
            this.Count = 0;
            int n = table.Length;
            for (int i = 0; i < n; i++) table[i] = null;
        }

        public bool ContainsKey(KeyType key)
        {
            return this.GetHashtableEntry(key, (uint)key.GetHashCode()) != null;
        }

        public IEnumerable<KeyType> Keys
        {
            get
            {
                foreach (var entry in table)
                {
                    var e = entry;
                    while (e != null)
                    {
                        yield return e.Key;
                        e = e.next;
                    }
                }
            }
        }

        public IEnumerable<KeyValuePair<KeyType, ValueType>> Elements
        {
            get
            {
                foreach (var entry in table)
                {
                    var e = entry;
                    while (e != null)
                    {
                        yield return new KeyValuePair<KeyType, ValueType>(e.Key, e.Value);
                        e = e.next;
                    }
                }
            }
        }

        private void Rehash()
        {
            var oldTable = table;
            uint threshold = this.threshold = (uint)(oldTable.Length + 1);
            uint newCapacity = threshold * 2 - 1;
            var newTable = table = new HashtableEntry<KeyType, ValueType>[newCapacity];
            for (uint i = threshold - 1; i-- > 0;)
            {
                for (var old = oldTable[(int)i]; old != null;)
                {
                    var e = old; old = old.next;
                    uint index = e.hashCode % newCapacity;
                    e.next = newTable[index];
                    newTable[index] = e;
                }
            }
        }

        public bool Remove(KeyType key)
        {
            uint hashCode = (uint)key.GetHashCode();
            uint index = hashCode % (uint)table.Length;
            var e = table[index];
            if (e != null && e.hashCode == hashCode && (Object.Equals(e.Key, key) || e.Key.Equals(key)))
            {
                e = e.next;
                table[index] = e;
                this.Count--;
                return true;
            }
            do
            {
                if (e == null) return false;
                var f = e.next;
                if (f != null && f.hashCode == hashCode && (Object.Equals(f.Key, key) || f.Key.Equals(key)))
                {
                    // f = f.next;
                    e.next = f.next;   // F : Changed
                    this.Count--;
                    return true;
                }
                e = f;
            } while (true);
        }


        public bool TryGetValue(KeyType key, out ValueType result)
        {
            HashtableEntry<KeyType, ValueType> e = this.GetHashtableEntry(key, (uint)key.GetHashCode());
            if (e == null)
            {
                result = default(ValueType);
                return false;
            }
            else
            {
                result = e.Value;
                return true;
            }
        }

        public ValueType this[KeyType key]
        {
            get
            {
                var e = this.GetHashtableEntry(key, (uint)key.GetHashCode());
                if (e == null) return default(ValueType);
                return e.Value;
            }
            set
            {
                uint hashCode = (uint)key.GetHashCode();
                var e = this.GetHashtableEntry(key, hashCode);
                if (e != null)
                {
                    e.Key = key;
                    e.Value = value;
                    return;
                }
                if (++this.Count >= threshold) this.Rehash();
                uint index = hashCode % (uint)table.Length;
                table[index] = new HashtableEntry<KeyType, ValueType>(key, value, hashCode, table[index]);
            }
        }
    }
}

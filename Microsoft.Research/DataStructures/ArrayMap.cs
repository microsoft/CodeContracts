// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.DataStructures
{
    [ContractVerification(true)]
    public class ArrayMap<T>
    {
        private T[] data;
        private bool[] present;

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(data != null);
            Contract.Invariant(present != null);
            Contract.Invariant(data.Length == present.Length);
        }

        public int Count
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() == data.Length);
                Contract.Ensures(Contract.Result<int>() >= 0);

                return data.Length;
            }
        }

        public ArrayMap(int capacity)
        {
            Contract.Requires(capacity >= 0);

            data = new T[capacity];
            present = new bool[capacity];
        }

        [Pure]
        public bool ContainsKey(int key)
        {
            Contract.Requires(key >= 0);
            Contract.Requires(key < Count);

            Contract.Ensures(Contract.Result<bool>() == present[key]);

            return present[key];
        }


        public T this[int key]
        {
            get
            {
                Contract.Requires(key >= 0);
                Contract.Requires(key < this.Count);
                Contract.Requires(ContainsKey(key));

                if (present[key]) return data[key];
                throw new NotSupportedException("key not in map");
            }

            set
            {
                Contract.Requires(key >= 0);
                Contract.Requires(key < Count);

                Contract.Ensures(ContainsKey(key));

                data[key] = value;
                present[key] = true;
            }
        }

        public bool TryGetValue(int key, out T value)
        {
            Contract.Requires(key >= 0);
            Contract.Requires(key < Count);
            Contract.Ensures(Contract.Result<bool>() || !ContainsKey(key));

            if (present[key]) { value = data[key]; return true; }
            value = default(T);
            return false;
        }

        public void Add(int key, T value)
        {
            Contract.Requires(key >= 0);
            Contract.Requires(key < Count);
            Contract.Requires(!ContainsKey(key));

            if (present[key]) throw new NotSupportedException("key already present");
            present[key] = true;
            data[key] = value;
        }
    }
}

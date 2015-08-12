// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Research.DataStructures
{
    public struct TypedKey<T>
    {
        private string key;
        public TypedKey(string key)
        {
            this.key = key;
        }
    }

    public interface ITypedProperties
    {
        bool Contains<T>(TypedKey<T> key);

        void Add<T>(TypedKey<T> key, T value);

        bool TryGetValue<T>(TypedKey<T> key, out T value);
    }

    [Serializable]
    public class TypedProperties : ITypedProperties
    {
        private System.Collections.Hashtable table = new System.Collections.Hashtable();

        public bool Contains<T>(TypedKey<T> key)
        {
            return table.Contains(key);
        }

        public void Add<T>(TypedKey<T> key, T value)
        {
            table.Add(key, value);
        }

        public bool TryGetValue<T>(TypedKey<T> key, out T value)
        {
            object result = table[key];
            if (result == null)
            {
                value = default(T); return false;
            }
            value = (T)result;
            return true;
        }
    }
}

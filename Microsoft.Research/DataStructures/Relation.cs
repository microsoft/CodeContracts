// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Research.DataStructures
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.IO;


    public class Relation<S, T>
    {
        private DoubleTable<S, T, bool> representation = new DoubleTable<S, T, bool>();

        public void Add(S s, T t)
        {
            representation.Add(s, t, true);
        }

        private static readonly ICollection<T> EmptyRange = new T[0];

        public ICollection<T> this[S key]
        {
            get
            {
                Dictionary<T, bool>/*?*/ range;
                if (representation.TryGetValue(key, out range))
                {
                    //^ assume range != null;
                    return range.Keys;
                }
                return EmptyRange;
            }
        }
    }
}
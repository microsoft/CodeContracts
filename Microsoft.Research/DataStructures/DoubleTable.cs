// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Research.DataStructures
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.IO;



    public class DoubleTable<KEY1, KEY2, ELEM> : Dictionary<KEY1, Dictionary<KEY2, ELEM>>
    {
        public DoubleTable() : base() { }

        public void Add(KEY1 key1, KEY2 key2, ELEM element)
        {
            Dictionary<KEY2, ELEM>/*?*/ table;
            if (!(this.TryGetValue(key1, out table)))
            {
                table = new Dictionary<KEY2, ELEM>();
                this.Add(key1, table);
            }
            //^ assert table != null;
            table.Add(key2, element);
        }

        public ELEM this[KEY1 key1, KEY2 key2]
        {
            get
            {
                return this[key1][key2];
            }
            set
            {
                Dictionary<KEY2, ELEM>/*?*/ table;
                if (!(this.TryGetValue(key1, out table)))
                {
                    table = new Dictionary<KEY2, ELEM>();
                    this.Add(key1, table);
                }
                //^ assert table != null;
                table[key2] = value;
            }
        }


        public IEnumerable<KEY1> Keys1 { get { return this.Keys; } }

        public IEnumerable<KEY2> Keys2(KEY1 key1)
        {
            Dictionary<KEY2, ELEM>/*?*/ range;
            if (this.TryGetValue(key1, out range))
            {
                //^ assume range != null;
                return range.Keys;
            }
            return new KEY2[0];
        }

        public bool ContainsKey(KEY1 key1, KEY2 key2)
        {
            Dictionary<KEY2, ELEM>/*?*/ range;
            if (this.TryGetValue(key1, out range))
            {
                //^ assume range != null;
                return range.ContainsKey(key2);
            }
            return false;
        }

        public bool TryGetValue(KEY1 key1, KEY2 key2, out ELEM/*?*/ result)
        {
            Dictionary<KEY2, ELEM>/*?*/ range;
            if (this.TryGetValue(key1, out range))
            {
                //^ assume range != null;
                return range.TryGetValue(key2, out result);
            }
            result = default(ELEM);
            return false;
        }
    }
}
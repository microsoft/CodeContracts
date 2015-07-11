// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace Microsoft.Research.DataStructures
{
    public class MemoryObjectSet<T> : IObjectSet<T>
      where T : class
    {
        private readonly HashSet<T> data;
        private readonly IQueryable queryable;

        public MemoryObjectSet() : this(new HashSet<T>()) { }

        public MemoryObjectSet(HashSet<T> data)
        {
            this.data = data;
            queryable = data.AsQueryable();
        }

        public void AddObject(T item) { data.Add(item); }
        public void DeleteObject(T item) { data.Remove(item); }
        public void Attach(T item) { data.Add(item); throw new NotImplementedException("Need to also add a link from item to the set, right?"); }
        public void Detach(T item) { data.Remove(item); throw new NotImplementedException("Need to also remove the link from item to the set, right?"); }
        IEnumerator IEnumerable.GetEnumerator() { return data.GetEnumerator(); }
        IEnumerator<T> IEnumerable<T>.GetEnumerator() { return data.GetEnumerator(); }
        Type IQueryable.ElementType { get { return queryable.ElementType; } }
        Expression IQueryable.Expression { get { return queryable.Expression; } }
        IQueryProvider IQueryable.Provider { get { return queryable.Provider; } }
    }
}

// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

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
      this.queryable = data.AsQueryable();
    }

    public void AddObject(T item) { this.data.Add(item); }
    public void DeleteObject(T item) { this.data.Remove(item); }
    public void Attach(T item) { this.data.Add(item); throw new NotImplementedException("Need to also add a link from item to the set, right?"); }
    public void Detach(T item) { this.data.Remove(item); throw new NotImplementedException("Need to also remove the link from item to the set, right?"); }
    IEnumerator IEnumerable.GetEnumerator() { return this.data.GetEnumerator(); }
    IEnumerator<T> IEnumerable<T>.GetEnumerator() { return this.data.GetEnumerator(); }
    Type IQueryable.ElementType { get { return this.queryable.ElementType; } }
    Expression IQueryable.Expression { get { return this.queryable.Expression; } }
    IQueryProvider IQueryable.Provider { get { return this.queryable.Provider; } }
  }
}

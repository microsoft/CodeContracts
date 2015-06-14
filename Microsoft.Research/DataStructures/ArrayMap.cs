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
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.DataStructures {

  [ContractVerification(true)]
  public class ArrayMap<T> {

    T[] data;
    bool[] present;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.data != null);
      Contract.Invariant(this.present != null);
      Contract.Invariant(data.Length == present.Length);
    }

    public int Count { get {
      Contract.Ensures(Contract.Result<int>() == this.data.Length);
      Contract.Ensures(Contract.Result<int>() >= 0);

      return this.data.Length; 
    } }

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

      Contract.Ensures(Contract.Result<bool>() == this.present[key]);

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

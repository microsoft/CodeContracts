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
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.DataStructures
{
  public struct OnDemandMap<Key, Value>
  {
    Dictionary<Key, Value> dictionary;

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
      if (this.dictionary == null)
      {
        value = default(Value); return false;
      }
      return this.dictionary.TryGetValue(key, out value);
    }

    public IEnumerable<Value> Values
    {
      get
      {
        if (this.dictionary == null) { return new Value[0]; }
        return this.dictionary.Values;
      }
    }

    public bool ContainsKey(Key key)
    {
      if (this.dictionary == null) return false;
      return dictionary.ContainsKey(key);
    }

    public IEnumerator<KeyValuePair<Key,Value>> GetEnumerator()
    {
      if (this.dictionary == null) yield break;
      foreach (var kv in this.dictionary)
      {
        yield return kv;
      }
    }
  }

  public struct OnDemandSet<Key>
  {
    Set<Key> set;

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
      if (this.set == null)
      {
        return false;
      }
      return this.set.Contains(key);
    }

    public IEnumerable<Key> Elements
    {
      get
      {
        if (this.set == null) { return new Key[0]; }
        return this.set;
      }
    }

  }

  [ContractVerification(true)]
  public struct OnDemandCache<Key, Value>
  {
    Dictionary<Key, Value> dictionary;
    WrapQueue<KeyValuePair<Key, Value>> hitQueue;

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
      if (this.hitQueue == null || this.hitQueue.Capacity != this.Capacity / 2)
      {
        this.hitQueue = new WrapQueue<KeyValuePair<Key, Value>>(this.Capacity / 2);
      }
      return dictionary;
    }

    public void Add(Key key, Value value)
    {
      var dict = Ensure();
      dict.Add(key, value);
      LimitCapacity(dict);
    }

    private void LimitCapacity(Dictionary<Key, Value> dict)
    {
      Contract.Requires(dict != null);
      Contract.Requires(this.hitQueue != null);

      if (dict.Count > this.Capacity)
      {
        dict.Clear();
        foreach (var kv in this.hitQueue.Elements())
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

    public bool TryGetValue(Key key, out Value value)
    {
      if (this.dictionary == null)
      {
        value = default(Value); return false;
      }
      var result = this.dictionary.TryGetValue(key, out value);
      if (result)
      {
        // hit
        this.hitQueue.Push(new KeyValuePair<Key, Value>(key, value));
      }
      return result;
    }

    public IEnumerable<Value> Values
    {
      get
      {
        if (this.dictionary == null) { return new Value[0]; }
        return this.dictionary.Values;
      }
    }

    public bool ContainsKey(Key key)
    {
      if (this.dictionary == null) return false;
      return dictionary.ContainsKey(key);
    }

    public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
    {
      if (this.dictionary == null) yield break;
      foreach (var kv in this.dictionary)
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

      this.queue = new T[capacity];
    }

    public int Capacity { get { return this.queue.Length; } }

    public int Count
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() <= this.Capacity);

        if (this.insertAt >= this.removeAt) return this.insertAt - this.removeAt;
        return this.queue.Length - this.removeAt + this.insertAt;
      }
    }

    public void Push(T item)
    {
      if (this.insertAt >= queue.Length)
      {
        this.insertAt = 0;
      }
      if (this.Count == this.queue.Length)
      { // at capacity
        this.Pop();
      }
      queue[this.insertAt++] = item;
    }


    public T Pop()
    {
      Contract.Ensures(this.insertAt == Contract.OldValue(this.insertAt));
      Contract.Ensures(this.queue == Contract.OldValue(this.queue));

      if (this.Count > 0)
      {
        if (this.removeAt >= queue.Length)
        {
          this.removeAt = 0;
        }
        return this.queue[this.removeAt++];
      }
      throw new InvalidOperationException("queue empty");
    }

    public IEnumerable<T> Elements()
    {
      var limit = this.removeAt > this.insertAt ? this.insertAt + this.Capacity : this.insertAt;

      for (int i = this.removeAt; i < limit; i++)
      {
        yield return this.queue[i % this.Capacity];
      }
    }
  }
}

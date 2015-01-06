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
using System.Linq;

namespace Microsoft.Research.DataStructures
{
  public class SubKeyDictionary<TKey, TSubKey, TValue> : Dictionary<TKey, TValue>
  {
    protected static readonly KeyValuePair<TKey, TValue>[] emptyKVArray = new KeyValuePair<TKey, TValue>[0];

    protected readonly Dictionary<TSubKey, HashSet<TKey>> sub;
    protected readonly Func<TKey, TSubKey> proj;

    public SubKeyDictionary(Func<TKey, TSubKey> projection)
    {
      this.proj = projection;
      this.sub = new Dictionary<TSubKey, HashSet<TKey>>();
    }

    public virtual new void Add(TKey key, TValue value)
    {
      base.Add(key, value);
      this.SubAdd(this.proj(key), key);
    }

    private void SubAdd(TSubKey subkey, TKey key)
    {
      HashSet<TKey> s;
      if (this.sub.TryGetValue(subkey, out s))
        s.Add(key);
      else
        this.sub[subkey] = new HashSet<TKey> { key };
    }

    public virtual new void Clear()
    {
      base.Clear();
      this.sub.Clear();
    }

    public bool ContainsKey(TSubKey subkey)
    {
      return this.sub.ContainsKey(subkey);
    }

    public virtual new bool Remove(TKey key)
    {
      return base.Remove(key) && this.SubRemove(this.proj(key), key);
    }

    public virtual bool Remove(TKey key, out TValue value)
    {
      return base.TryGetValue(key, out value) && this.Remove(key);
    }

    protected bool BaseRemove(TKey key)
    {
      return base.Remove(key);
    }

    protected virtual bool BaseRemove(TKey key, out TValue value)
    {
      return base.TryGetValue(key, out value) && base.Remove(key);
    }

    private bool SubRemove(TSubKey subkey, TKey key)
    {
      HashSet<TKey> s;
      return this.sub.TryGetValue(subkey, out s) && s.Remove(key) && (s.Any() || this.sub.Remove(subkey));
    }

    private bool SubRemove(TSubKey subkey, out HashSet<TKey> keys)
    {
      return this.sub.TryGetValue(subkey, out keys) && this.sub.Remove(subkey);
    }

    public bool Remove(TSubKey subkey)
    {
      HashSet<TKey> s;
      return this.sub.TryGetValue(subkey, out s) && s.All(base.Remove) && this.sub.Remove(subkey);
    }

    public bool Remove(TSubKey subkey, out IEnumerable<KeyValuePair<TKey, TValue>> elements)
    {
      HashSet<TKey> s;
      if (this.SubRemove(subkey, out s))
      {
        elements = s.SelectMany(key =>
        {
          TValue value;
          return this.BaseRemove(key, out value) ? new KeyValuePair<TKey, TValue>[] { new KeyValuePair<TKey, TValue>(key, value) } : emptyKVArray;
        });
        return true;
      }
      elements = null;
      return false;
    }

    public bool TryGetValue(TSubKey subkey, out IEnumerable<KeyValuePair<TKey, TValue>> elements)
    {
      HashSet<TKey> s;
      if (this.sub.TryGetValue(subkey, out s))
      {
        elements = s.SelectMany(key =>
        {
          TValue value;
          return base.TryGetValue(key, out value) ? new KeyValuePair<TKey, TValue>[] { new KeyValuePair<TKey, TValue>(key, value) } : emptyKVArray;
        });
        return true;
      }
      elements = null;
      return false;
    }

    public TValue GetValueOrDefault(TKey key)
    {
      TValue value;
      return base.TryGetValue(key, out value) ? value : default(TValue);
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> GetValueOrEmpty(TSubKey subkey)
    {
      IEnumerable<KeyValuePair<TKey, TValue>> elements;
      return this.TryGetValue(subkey, out elements) ? elements : emptyKVArray;
    }

    public int SubCount { get { return this.sub.Count; } }

    public Dictionary<TSubKey, HashSet<TKey>>.KeyCollection SubKeys { get { return this.sub.Keys; } }
  }

  public class SubKeyDictionary<TKey, TSubKey1, TSubKey2, TValue> : SubKeyDictionary<TKey, TSubKey1, TValue>
  {
    private readonly Dictionary<TSubKey2, HashSet<TKey>> sub2;
    private readonly Func<TKey, TSubKey2> proj2;

    public SubKeyDictionary(Func<TKey, TSubKey1> projection1, Func<TKey, TSubKey2> projection2)
      : base(projection1)
    {
      this.proj2 = projection2;
      this.sub2 = new Dictionary<TSubKey2, HashSet<TKey>>();
    }

    public override void Add(TKey key, TValue value)
    {
      base.Add(key, value);
      this.SubAdd(this.proj2(key), key);
    }

    private void SubAdd(TSubKey2 subkey2, TKey key)
    {
      HashSet<TKey> s;
      if (this.sub2.TryGetValue(subkey2, out s))
        s.Add(key);
      else
        this.sub2[subkey2] = new HashSet<TKey> { key };
    }

    public override void Clear()
    {
      base.Clear();
      this.sub2.Clear();
    }

    public bool ContainsKey(TSubKey2 subkey2)
    {
      return this.sub2.ContainsKey(subkey2);
    }

    public override bool Remove(TKey key)
    {
      return base.Remove(key) && this.SubRemove(this.proj2(key), key);
    }

    public override bool Remove(TKey key, out TValue value)
    {
      return base.TryGetValue(key, out value) && this.Remove(key);
    }

    protected override bool BaseRemove(TKey key, out TValue value)
    {
      return base.TryGetValue(key, out value) && base.Remove(key);
    }

    private bool SubRemove(TSubKey2 subkey2, TKey key)
    {
      HashSet<TKey> s;
      return this.sub2.TryGetValue(subkey2, out s) && s.Remove(key) && (s.Any() || this.sub2.Remove(subkey2));
    }

    private bool SubRemove(TSubKey2 subkey2, out HashSet<TKey> keys)
    {
      return this.sub2.TryGetValue(subkey2, out keys) && this.sub2.Remove(subkey2);
    }

    public bool Remove(TSubKey2 subkey2)
    {
      HashSet<TKey> s;
      return this.sub2.TryGetValue(subkey2, out s) && s.All(base.Remove) && this.sub2.Remove(subkey2);
    }

    public bool Remove(TSubKey2 subkey2, out IEnumerable<KeyValuePair<TKey, TValue>> elements)
    {
      HashSet<TKey> s;
      if (this.SubRemove(subkey2, out s))
      {
        elements = s.SelectMany(key =>
        {
          TValue value;
          return this.BaseRemove(key, out value) ? new KeyValuePair<TKey, TValue>[] { new KeyValuePair<TKey, TValue>(key, value) } : emptyKVArray;
        });
        return true;
      }
      elements = null;
      return false;
    }

    public bool Remove(TSubKey1 subkey1, TSubKey2 subkey2)
    {
      HashSet<TKey> s1, s2;
      if (!this.sub.TryGetValue(subkey1, out s1))
        return false;
      if (!this.sub2.TryGetValue(subkey2, out s2))
        return false;
      var s = s1.Intersect(s2).ToArray(); // force evaluation
      s1.ExceptWith(s);
      if (!s1.Any())
        this.sub.Remove(subkey1);
      s2.ExceptWith(s);
      if (!s2.Any())
        this.sub2.Remove(subkey2);
      return s.All(base.BaseRemove);
    }

    public bool TryGetValue(TSubKey2 subkey2, out IEnumerable<KeyValuePair<TKey, TValue>> elements)
    {
      HashSet<TKey> s;
      if (this.sub2.TryGetValue(subkey2, out s))
      {
        elements = s.SelectMany(key =>
        {
          TValue value;
          return base.TryGetValue(key, out value) ? new KeyValuePair<TKey, TValue>[] { new KeyValuePair<TKey, TValue>(key, value) } : emptyKVArray;
        });
        return true;
      }
      elements = null;
      return false;
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> GetValueOrEmpty(TSubKey2 subkey2)
    {
      IEnumerable<KeyValuePair<TKey, TValue>> elements;
      return this.TryGetValue(subkey2, out elements) ? elements : emptyKVArray;
    }

    public int Sub1Count { get { return base.SubCount; } }

    public int Sub2Count { get { return this.sub2.Count; } }

    public Dictionary<TSubKey1, HashSet<TKey>>.KeyCollection Sub1Keys { get { return base.SubKeys; } }

    public Dictionary<TSubKey2, HashSet<TKey>>.KeyCollection Sub2Keys { get { return this.sub2.Keys; } }
  }

  public class MultiKeyDictionary<TKey1, TKey2, TValue> : SubKeyDictionary<Pair<TKey1, TKey2>, TKey1, TKey2, TValue>
  {
    public MultiKeyDictionary()
      : base(p => p.One, p => p.Two)
    { }

    public void Add(TKey1 key1, TKey2 key2, TValue value)
    {
      this.Add(Pair.For(key1, key2), value);
    }
  }
}

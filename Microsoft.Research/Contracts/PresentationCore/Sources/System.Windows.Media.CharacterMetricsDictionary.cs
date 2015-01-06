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

// File System.Windows.Media.CharacterMetricsDictionary.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Windows.Media
{
  sealed public partial class CharacterMetricsDictionary : IDictionary<int, CharacterMetrics>, ICollection<KeyValuePair<int, CharacterMetrics>>, IEnumerable<KeyValuePair<int, CharacterMetrics>>, System.Collections.IDictionary, System.Collections.ICollection, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public void Add(KeyValuePair<int, CharacterMetrics> item)
    {
    }

    public void Add(int key, CharacterMetrics value)
    {
    }

    internal CharacterMetricsDictionary()
    {
    }

    public void Clear()
    {
    }

    public bool Contains(KeyValuePair<int, CharacterMetrics> item)
    {
      return default(bool);
    }

    public bool ContainsKey(int key)
    {
      return default(bool);
    }

    public void CopyTo(KeyValuePair<int, CharacterMetrics>[] array, int index)
    {
    }

    public IEnumerator<KeyValuePair<int, CharacterMetrics>> GetEnumerator()
    {
      return default(IEnumerator<KeyValuePair<int, CharacterMetrics>>);
    }

    public bool Remove(KeyValuePair<int, CharacterMetrics> item)
    {
      return default(bool);
    }

    public bool Remove(int key)
    {
      return default(bool);
    }

    void System.Collections.ICollection.CopyTo(Array array, int index)
    {
    }

    void System.Collections.IDictionary.Add(Object key, Object value)
    {
    }

    bool System.Collections.IDictionary.Contains(Object key)
    {
      return default(bool);
    }

    System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator()
    {
      return default(System.Collections.IDictionaryEnumerator);
    }

    void System.Collections.IDictionary.Remove(Object key)
    {
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public bool TryGetValue(int key, out CharacterMetrics value)
    {
      value = default(CharacterMetrics);

      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public int Count
    {
      get
      {
        return default(int);
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    public CharacterMetrics this [int key]
    {
      get
      {
        return default(CharacterMetrics);
      }
      set
      {
      }
    }

    public ICollection<int> Keys
    {
      get
      {
        return default(ICollection<int>);
      }
    }

    bool System.Collections.ICollection.IsSynchronized
    {
      get
      {
        return default(bool);
      }
    }

    Object System.Collections.ICollection.SyncRoot
    {
      get
      {
        return default(Object);
      }
    }

    bool System.Collections.IDictionary.IsFixedSize
    {
      get
      {
        return default(bool);
      }
    }

    Object System.Collections.IDictionary.this [Object key]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    System.Collections.ICollection System.Collections.IDictionary.Keys
    {
      get
      {
        return default(System.Collections.ICollection);
      }
    }

    System.Collections.ICollection System.Collections.IDictionary.Values
    {
      get
      {
        return default(System.Collections.ICollection);
      }
    }

    public ICollection<CharacterMetrics> Values
    {
      get
      {
        return default(ICollection<CharacterMetrics>);
      }
    }
    #endregion
  }
}
